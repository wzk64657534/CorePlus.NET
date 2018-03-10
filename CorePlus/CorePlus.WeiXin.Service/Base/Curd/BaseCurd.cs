using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Web;
using Core;
using CorePlus.WeiXin.Entity;
using CorePlus.WeiXin.Repository;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public abstract class BaseCurd : ICurd
    {
        protected abstract string GetUrl();
        protected abstract string GetMethod();

        public string Operation(NameValueCollection parameters)
        {
            try
            {
                if (ConfigurationHelper.IsLog)
                {
                    foreach (var item in parameters.AllKeys)
                    {
                        LogCommonHelper.WriteLog(string.Format("参数{0} / 值{1}", item, parameters[item]));
                    }
                }

                string[] names = GetNamesOfMustParamter();

                foreach (var item in names)
                {
                    if (parameters[item] == null)
                    {
                        return string.Format(ConstHelper.ERROR_100003, item);
                    }
                }


                dynamic token = GetAccessToken(parameters);
                if (token is AccessTokenEntity)
                {
                    AccessTokenEntity entity = token as AccessTokenEntity;
                    string data = Working(entity.access_token, parameters);
                    try
                    {
                        var model = JsonHelper.Deserialize<ErrorEntity>(data);
                        return JsonHelper.Serialize(GetErrorChineseMessage(model));
                    }
                    catch
                    {
                        return data;
                    }
                }
                else if (token is ErrorEntity)
                {
                    return JsonHelper.Serialize(GetErrorChineseMessage(token as ErrorEntity));
                }
                else if (token is string)
                {
                    return token;
                }
                else
                {
                    LogCommonHelper.WriteLog("Error:" + ConstHelper.ERROR_999998);
                    return ConstHelper.ERROR_999998;
                }
            }
            catch (Exception ex)
            {
                return string.Format(ConstHelper.ERROR_999999, ex.InnerException.Message);
            }
        }

        protected virtual string Working(string token, NameValueCollection parameters)
        {
            LogCommonHelper.WriteLog("Working开始处理");

            parameters.Remove("key");
            parameters.Add("access_token", token);
            UpdateParameters(parameters);
            string url = GetUrl();
            LogCommonHelper.WriteLog("url：" + url);

            string method = GetMethod();
            LogCommonHelper.WriteLog("method：" + method);

            string data = GetFromWebRequest(url, method, parameters);
            LogCommonHelper.WriteLog("data：" + data);

            return data;
        }

        protected virtual void UpdateParameters(NameValueCollection parameters)
        {

        }

        protected virtual string[] GetNamesOfMustParamter()
        {
            return new string[] { };
        }

        private string GetFromWebRequest(string url, string method, NameValueCollection parameters)
        {
            // HTTP模拟对象
            HttpWebRequest request = null;
            Stream requestStream = null;
            // 根据不同的提交方式
            switch (method)
            {
                case "GET":
                    // 组装参数
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            sb.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            sb.AppendFormat("?{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    string query = sb.ToString().Replace("\t", "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
                    url += query;
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "GET";
                    break;
                case "POST":
                    string token = parameters["access_token"];
                    url += "?access_token=" + token;

                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    string body = parameters["body"].Replace("\t", "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
                    byte[] data = Encoding.UTF8.GetBytes(body);

                    request.ContentLength = data.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(data, 0, data.Length);
                    break;
            }
            // 接收服务传回的数据
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            string html = string.Empty;

            try
            {
                html = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                html = string.Format(ConstHelper.ERROR_999999, ex.Message);
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
                if (responseStream != null)
                    responseStream.Close();
                if (sr != null)
                    sr.Close();
                if (request != null)
                    request.Abort();
                if (response != null)
                    response.Close();
            }

            return html;
        }

        private dynamic GetAccessToken(NameValueCollection parameters)
        {
            string un = parameters["un"];
            string unkey = parameters["unkey"];
            WxAccountRepository cr = new WxAccountRepository();
            var user = cr.FindByExpression(x => x.UserName == un).FirstOrDefault();
            if (user == null) { return ConstHelper.ERROR_100007; }
            string cryptKey = CryptHelper.MD5(un + user.Token);
            if (!Equals(unkey, cryptKey)) { return ConstHelper.ERROR_100007; }

            string KEY_ACCESS_TOKEN = user.UserName + "ACCESSTOKEN";

            if (!ApplicationHelper.Exists(KEY_ACCESS_TOKEN))
            {
                return RequestNewToken(user, KEY_ACCESS_TOKEN);
            }
            else
            {
                AccessTokenCacheEntity accesstoken = ApplicationHelper.GetValue(KEY_ACCESS_TOKEN) as AccessTokenCacheEntity;
                TimeSpan ts = DateTime.Now - accesstoken.RequstTime;
                // 提前5秒过期
                if (ts.TotalSeconds < (user.ExpiresIn ?? 0) - 5)
                {
                    AccessTokenEntity entity = new AccessTokenEntity();
                    entity.access_token = accesstoken.AccessToken;
                    entity.expires_in = user.ExpiresIn ?? 0;
                    return entity;
                }
                else
                {
                    return RequestNewToken(user, KEY_ACCESS_TOKEN);
                }
            }
        }

        private dynamic RequestNewToken(WxAccountEntity user, string appKey)
        {
            int times = 0;
            AccessTokenEntity entity = null;
            string data = string.Empty;
            while (times < 3)
            {
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("appid", user.AppId);
                parameters.Add("secret", user.Secret);
                parameters.Add("grant_type", user.GrantType);
                // 发送Token请求
                data = GetFromWebRequest(ConstHelper.WEIXIN_ACCESS_TOKEN, "GET", parameters);

                if (data.Contains("access_token"))
                {
                    entity = JsonHelper.Deserialize<AccessTokenEntity>(data);
                    break;
                }
                else
                {
                    Thread.Sleep(1000 * 1);
                    times++;
                }
            }

            if (entity != null)
            {
                ApplicationHelper.SetValue(appKey,
                    new AccessTokenCacheEntity { AccessToken = entity.access_token, RequstTime = DateTime.Now });
                return entity;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    return JsonHelper.Deserialize<ErrorEntity>(ConstHelper.ERROR_100004);
                }
                else
                {
                    var model = JsonHelper.Deserialize<ErrorEntity>(data);
                    return GetErrorChineseMessage(model);
                }
            }
        }

        private ErrorEntity GetErrorChineseMessage(ErrorEntity entity)
        {
            var query = (from x in XmlHelper.GetErrorFromXML()
                         where x.errcode == entity.errcode
                         select x).FirstOrDefault();

            return query ?? entity;
        }
    }
}
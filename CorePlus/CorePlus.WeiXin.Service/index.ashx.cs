using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using Core;
using CorePlus.WeiXin.Entity;
using CorePlus.WeiXin.Repository;
using CorePlus.Common;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class index : BaseHandler
    {
        string postStr = "";

        protected override void RequestInfomation(HttpContext context)
        {
            LogCommonHelper.WriteLog("已经接收到数据");
            LogCommonHelper.WriteLog("IP：" + context.Request.UserHostAddress);

            //foreach (var item in context.Request.Params.AllKeys)
            //{
            //    LogCommonHelper.WriteLog(item + "：" + context.Request.Params[item]);
            //}

            string method = context.Request.HttpMethod.ToString();

            LogCommonHelper.WriteLog("Method：" + method);

            if (Equals(method.ToUpper(), "POST"))
            {
                System.IO.Stream s = context.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = System.Text.Encoding.UTF8.GetString(b);

                LogCommonHelper.WriteLog("接收到的信息：" + postStr);

                if (!string.IsNullOrEmpty(postStr))
                {
                    ResponseMsg(postStr, context);
                }
                else
                {
                    LogCommonHelper.WriteLog("接收到空数据");
                }
            }
            else
            {
                LogCommonHelper.WriteLog("开始验证");

                var echostr = context.Request.QueryString.Get("echoStr");
                LogCommonHelper.WriteLog("echostr：" + echostr);

                var signature = context.Request.QueryString.Get("signature");
                LogCommonHelper.WriteLog("signature：" + signature);

                var timestamp = context.Request.QueryString.Get("timestamp");
                LogCommonHelper.WriteLog("timestamp：" + timestamp);

                var nonce = context.Request.QueryString.Get("nonce");
                LogCommonHelper.WriteLog("nonce：" + nonce);

                bool flag = false;
                try
                {
                    WxIndexRepository repository = new WxIndexRepository();
                    var companys = repository.GetManyCompanyTokenFalse();

                    foreach (WxAccountEntity user in companys)
                    {
                        string token = user.Token;
                        LogCommonHelper.WriteLog("token：" + token);

                        string[] ArrTmp = { token, timestamp, nonce };

                        Array.Sort(ArrTmp);
                        string tmpStr = string.Join("", ArrTmp);
                        tmpStr = CryptHelper.SHA1(tmpStr);
                        tmpStr = tmpStr.ToLower();
                        LogCommonHelper.WriteLog("tmpStr：" + tmpStr);

                        if (tmpStr == signature)
                        {
                            LogCommonHelper.WriteLog("验证成功");
                            flag = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogCommonHelper.WriteLog("Exception：" + ex.InnerException.Message);
                }

                if (flag)
                {
                    context.Response.Write(echostr);
                }
                else
                {
                    context.Response.Write(string.Empty);
                }

                context.Response.End();
            }
        }

        public void ResponseMsg(string postStr, HttpContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(postStr))
                {
                    LogCommonHelper.WriteLog("接收到空值");
                    context.Response.Write(string.Empty);
                    context.Response.End();
                }
                else
                {
                    var signature = context.Request.QueryString.Get("signature");
                    LogCommonHelper.WriteLog("signature：" + signature);

                    var timestamp = context.Request.QueryString.Get("timestamp");
                    LogCommonHelper.WriteLog("timestamp：" + timestamp);

                    var nonce = context.Request.QueryString.Get("nonce");
                    LogCommonHelper.WriteLog("nonce：" + nonce);

                    string result = MessageProvider.Save(postStr, signature, timestamp, nonce);
                    LogCommonHelper.WriteLog(result);
                    context.Response.Write(result);
                    context.Response.End();
                }
            }
            catch (Exception ex)
            {
                LogCommonHelper.WriteLog(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Core;

namespace CorePlus.AiXin.Web
{
    public class index : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string body = context.Request.Form["body"] == null ? null : context.Request.Form["body"].ToString();
                string rt = context.Request.Form["rt"] == null ? "json" : context.Request.Form["rt"].ToString();
                string HMACMD5KEY = context.Request.Form["hmac"] == null ? null : context.Request.Form["hmac"].ToString();

                if (string.IsNullOrEmpty(body) == false && string.IsNullOrEmpty(HMACMD5KEY) == false)
                {
                    string dataTag = string.Empty;
                    Regex regexJson = new Regex(ConstHelper.RegexJson);
                    if (regexJson.IsMatch(body)) { dataTag = "json"; }

                    Regex regexXml = new Regex(ConstHelper.RegexHtml);
                    if (regexXml.IsMatch(body)) { dataTag = "xml"; }

                    if (string.IsNullOrEmpty(dataTag)) { context.Response.Write("body是未知类型"); return; }

                    ChangeDataProvider provider = new ChangeDataProvider(dataTag);
                    string data = provider.Change(body);
                    string mt = StringHelper.Extract(data, "<ActionName>", "</ActionName>");

                    CryptDataProvider cryptDataProvider = new CryptDataProvider();
                    data = cryptDataProvider.Crypt(data, HMACMD5KEY);

                    data = data.Replace("<Root>", "").Replace("</Root>", "");

                    DimooActionToExecute dimoo = new DimooActionToExecute();
                    string APassword = CryptHelper.HMAC_MD5(data, HMACMD5KEY);
                    string result = string.Empty;

                    switch (mt.ToLower())
                    {
                        case "loginsign": result = dimoo.ActionUserLogin(data, APassword); break;
                        default: result = dimoo.ActionToExecute(data, APassword); break;
                    }

                    if (string.IsNullOrEmpty(result) == false)
                    {
                        result = provider.Return(result, rt);
                    }

                    context.Response.Write(result);
                }
                else
                {
                    context.Response.Write("error：请检查必要参数是否准确");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
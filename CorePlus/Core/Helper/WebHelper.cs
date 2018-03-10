using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace Core
{
    public class WebHelper
    {
        public static string GetFormWebRequest(string url, string method = "GET", string body = null)
        {
            // HTTP模拟对象
            HttpWebRequest request = null;
            Stream requestStream = null;
            // 根据不同的提交方式
            switch (method)
            {
                case "GET":
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "GET";
                    break;
                case "POST":
                    if (body == null) { return "body must has data"; }

                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    body = body.Replace("\t", "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
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
                html = ex.Message;
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
    }
}
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net;

namespace CorePlus.WeiXin.Service.test
{
    public partial class menu_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string weixin1 = "";
            //weixin1 += "{\n";
            //weixin1 += "\"button\":[\n";
            //weixin1 += "{\n";
            //weixin1 += "\"type\":\"click\",\n";
            //weixin1 += "\"name\":\"今日歌曲\",\n";
            //weixin1 += "\"key\":\"V1001_TODAY_MUSIC123eee\"\n";
            //weixin1 += "},\n";
            //weixin1 += "{\n";
            //weixin1 += "\"type\":\"click\",\n";
            //weixin1 += "\"name\":\"歌手简介\",\n";
            //weixin1 += "\"key\":\"V1001_TODAY_SINGER123eee\"\n";
            //weixin1 += "},\n";
            //weixin1 += "{\n";
            //weixin1 += "\"name\":\"菜单\",\n";
            //weixin1 += "\"sub_button\":[\n";
            //weixin1 += "{\n";
            //weixin1 += "\"type\":\"click\",\n";
            //weixin1 += "\"name\":\"hello word\",\n";
            //weixin1 += "\"key\":\"V1001_HELLO_WORLD123eee\"\n";
            //weixin1 += "},\n";
            //weixin1 += "{\n";
            //weixin1 += "\"type\":\"click\",\n";
            //weixin1 += "\"name\":\"赞一下\",\n";
            //weixin1 += "\"key\":\"V1001_GOOD123eee\"\n";
            //weixin1 += "}]\n";
            //weixin1 += "}]\n";
            //weixin1 += "}\n";

            weixin1 = "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"type\":\"click\",\"name\":\"歌手简介\",\"key\":\"V1001_TODAY_SINGER\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"搜索\",\"url\":\"http://www.soso.com/\"},{\"type\":\"view\",\"name\":\"视频\",\"url\":\"http://v.qq.com/\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}";
            string i = GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=kk9tdWuPeresP11YAQUqlfD6Nc0Xrc_yjo3diDe7mIn0RNAdVxJyM9Gf_NewCEWBvgMJtgzSQcxRP-fTQJurC_bdeZ2NKUlioUocavXtlcVJau6fPavrUV5U9tFyUtqPXPeO9FOa5k8e_pJc779ctw", weixin1);

            Response.Write(i);
        }

        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
            finally
            {
                outstream.Close();
                instream.Close();
                sr.Close();
                response.Close();
                request.Abort();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using CorePlus.WeiXin.Entity;

namespace CorePlus.WeiXin.Service
{
    public class XmlHelper
    {
        public static string GetValueFromNode(string key, string xml)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(xml);

            var list = x.GetElementsByTagName(key);
            string kv = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ChildNodes[0].NodeType == System.Xml.XmlNodeType.CDATA)
                {
                    kv = list[i].ChildNodes[0].Value;
                }
                else
                {
                    kv = list[i].InnerText;
                }
            }

            return kv.ToLower();
        }

        public static string GetValueFromNode(string key, XmlDocument xml)
        {
            return GetValueFromNode(key, xml);
        }

        public static bool HasColumn(string key, string xml)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(xml);

            var list = x.GetElementsByTagName(key);
            return list.Count > 0;
        }

        public static List<ErrorEntity> GetErrorFromXML()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Error.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            List<ErrorEntity> list = new List<ErrorEntity>();
            var nodes = xml.GetElementsByTagName("error");
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                ErrorEntity entity = new ErrorEntity();
                entity.errcode = node.ChildNodes[0].InnerText;
                entity.errmsg = node.ChildNodes[1].InnerText;
                list.Add(entity);
            }

            return list;
        }
    }
}
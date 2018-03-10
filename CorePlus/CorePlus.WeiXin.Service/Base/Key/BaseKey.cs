using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.WeiXin.Service
{
    public class BaseKey : IKey
    {
        public string GetValueOfColumns(string xml, string[] cols)
        {
            string key = string.Empty;
            foreach (var item in cols)
            {
                string kv = XmlHelper.GetValueFromNode(item, xml);

                key += kv;

                if (Equals(kv, "subscribe"))
                {
                    string ek = XmlHelper.GetValueFromNode("EventKey", xml);
                    key += ek.Split(new char[] { '_' })[0];
                }
            }

            return key;
        }

        public string HasColumn(string xml, string col)
        {
            bool b = XmlHelper.HasColumn(col, xml);
            return b ? col.ToLower() : string.Empty;
        }
    }
}
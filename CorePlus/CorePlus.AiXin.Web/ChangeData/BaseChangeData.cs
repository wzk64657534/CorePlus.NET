using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public abstract class BaseChangeData : IChangeData
    {
        public virtual string Change(string data)
        {
            return "<Root>" + data + "</Root>";
        }

        public virtual string Return(string data, string type)
        {
            if (Equals(type, "json"))
            {
                data = string.Format("<Root>{0}</Root>", data.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>", ""));
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(data);

                    return StringHelper.XmlToJson(xml);
                }
                catch
                {
                    return data;
                }
            }

            return data;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public class BaseCryptData : ICryptData
    {
        public string Crypt(XmlDocument input, string hmac)
        {
            try
            {
                CryptElement(input, hmac);
                return input.InnerXml;
            }
            catch
            {

            }

            return string.Empty;
        }

        protected virtual void CryptElement(XmlDocument xml, string hmac)
        {

        }
    }
}
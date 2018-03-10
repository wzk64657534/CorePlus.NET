using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public class MemberBindAllFullCardCryptData : BaseCryptData
    {
        protected override void CryptElement(System.Xml.XmlDocument xml, string hmac)
        {
            try
            {
                XmlNode node = xml.SelectSingleNode("//AllFullCardPassword");
                node.InnerText = CryptHelper.HMAC_DES(node.InnerText, StringHelper.DESKey);
            }
            catch
            {

            }
        }
    }
}
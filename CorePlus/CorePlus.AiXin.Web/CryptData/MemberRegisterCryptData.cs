using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public class MemberRegisterCryptData : BaseCryptData
    {
        protected override void CryptElement(System.Xml.XmlDocument xml, string hmac)
        {
            try
            {
                string memberCode = xml.SelectSingleNode("//MemberCode").InnerText;
                XmlNode node = xml.SelectSingleNode("//LoginPassword");
                //node.InnerText = CryptHelper.HMAC_MD5(node.InnerText, hmac);
                node.InnerText = CryptHelper.MD5(memberCode.Trim().ToLower() + "DimooFighter" + node.InnerText.Trim());
            }
            catch
            {

            }
        }
    }
}
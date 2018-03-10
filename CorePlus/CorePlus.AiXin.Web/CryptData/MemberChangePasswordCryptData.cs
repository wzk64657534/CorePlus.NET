using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public class MemberChangePasswordCryptData : BaseCryptData
    {
        protected override void CryptElement(System.Xml.XmlDocument xml, string hmac)
        {
            try
            {
                string memberCode = xml.SelectSingleNode("//MemberCode").InnerText;

                XmlNode LoginPassword = xml.SelectSingleNode("//LoginPassword");
                //LoginPassword.InnerText = CryptHelper.HMAC_MD5(LoginPassword.InnerText, hmac);
                LoginPassword.InnerText = CryptHelper.MD5(memberCode.Trim().ToLower() + "DimooFighter" + LoginPassword.InnerText.Trim());

                XmlNode NewPassword = xml.SelectSingleNode("//NewPassword");
                //NewPassword.InnerText = CryptHelper.HMAC_MD5(NewPassword.InnerText, hmac);
                NewPassword.InnerText = CryptHelper.MD5(memberCode.Trim().ToLower() + "DimooFighter" + NewPassword.InnerText.Trim());
            }
            catch
            {

            }
        }
    }
}
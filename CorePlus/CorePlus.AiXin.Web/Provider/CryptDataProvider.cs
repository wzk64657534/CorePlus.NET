using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public class CryptDataProvider
    {
        Dictionary<string, ICryptData> manager = null;
        public CryptDataProvider()
        {
            manager = new Dictionary<string, ICryptData>();
            manager.Add("MEMBER_REGISTER@DIMOOAPP", new MemberRegisterCryptData());
            manager.Add("MEMBER_LOGIN@DIMOOAPP", new MemberLoginCryptData());
            manager.Add("MEMBER_CHANGEPASSWORD@DIMOOAPP", new MemberChangePasswordCryptData());
            manager.Add("MEMBER_BINDALLFULLCARD@DIMOOAPP", new MemberBindAllFullCardCryptData());
        }

        public string Crypt(string input, string hmac)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(input);
                XmlNode node = xml.SelectSingleNode("//ActionName");
                if (manager.ContainsKey(node.InnerText))
                {
                    return manager[node.InnerText].Crypt(xml, hmac);
                }
            }
            catch
            {

            }

            return input;
        }
    }
}
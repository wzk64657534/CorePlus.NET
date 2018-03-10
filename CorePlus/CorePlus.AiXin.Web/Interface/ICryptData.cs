using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.AiXin.Web
{
    public interface ICryptData
    {
        string Crypt(XmlDocument input, string hmac);
    }
}
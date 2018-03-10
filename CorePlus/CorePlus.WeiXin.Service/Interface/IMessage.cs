using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public interface IMessage
    {
        string Save(string postStr, string signature, string timestamp, string nonce);
    }
}
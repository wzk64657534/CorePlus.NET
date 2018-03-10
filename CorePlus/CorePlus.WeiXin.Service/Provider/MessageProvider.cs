using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public class MessageProvider
    {
        public static string Save(string postStr, string signature, string timestamp, string nonce)
        {
            ManagerMessage manager = ManagerMessage.CreateInstance();
            string key = KeyBuilder.GetKey(postStr);
            LogCommonHelper.WriteLog("Key：" + key);
            return manager.Save(key, postStr, signature, timestamp, nonce);
        }
    }
}
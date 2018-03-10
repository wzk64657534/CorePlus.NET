using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class KeyBuilder : BaseKey
    {
        public static string GetKey(string postStr)
        {
            KeyBuilder builder = new KeyBuilder();
            string key = string.Empty;
            key += builder.GetValueOfColumns(postStr, new string[] { "MsgType", "Event" });
            key += builder.HasColumn(postStr, "Recognition");
            return key;
        }
    }
}
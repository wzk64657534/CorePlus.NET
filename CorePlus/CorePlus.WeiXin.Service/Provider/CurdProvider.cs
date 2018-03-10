using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdProvider
    {
        public static string Operation(string key, NameValueCollection parameters)
        {
            ManagerCurd manager = ManagerCurd.CreateInstance();
            return manager.Operation(key, parameters);
        }
    }
}
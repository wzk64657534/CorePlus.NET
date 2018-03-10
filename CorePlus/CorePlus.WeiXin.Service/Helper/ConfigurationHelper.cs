using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class ConfigurationHelper : Core.ConfigurationHelper
    {
        public static bool IsLog { get { return bool.Parse(Get("IsLog")); } }
    }
}
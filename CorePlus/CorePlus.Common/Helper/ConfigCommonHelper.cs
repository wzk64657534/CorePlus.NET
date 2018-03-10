using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CorePlus.Common
{
    public class ConfigCommonHelper
    {
        public static int GetDefaultPageSize()
        {
            return ConfigurationManager.AppSettings["PageSize"] == null ? 0 : int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        }

        public static string GetFromAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? "" : ConfigurationManager.AppSettings[key].ToString();
        }
    }
}
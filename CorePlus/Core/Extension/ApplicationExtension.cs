using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Core
{
    public static class ApplicationExtension
    {
        public static bool HasKey(this HttpApplicationState application, string key)
        {
            return application[key] != null;
        }
    }
}
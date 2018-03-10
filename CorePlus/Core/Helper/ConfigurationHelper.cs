using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Core
{
    public class ConfigurationHelper
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CoreDbContext"].ConnectionString;
            }
        }

        public static string FilePassword
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["FilePassword"];
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public static string RootUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RootUrl"];
            }
        }

        public static string Namespaces
        {
            get
            {
                return ConfigurationManager.AppSettings["Namespaces"];
            }
        }

        public static string ServerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerUrl"];
            }
        }

        public static string AttachmentPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AttachmentPath"];
            }
        }

        public static string CatalogPath
        {
            get
            {
                return ConfigurationManager.AppSettings["CatalogPath"];
            }
        }

        public static string Email
        {
            get
            {
                return ConfigurationManager.AppSettings["Email"];
            }
        }

        public static string PreviewUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PreviewUrl"];
            }
        }

        public static string EmailUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailUrl"];
            }
        }

        public static string EmailHost
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailHost"];
            }
        }

        public static string EmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailPassword"];
            }
        }

        public static int DefaultWindowDataLimit
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultWindowDataLimit"]);
            }
        }

        public static int DefaultGridDataLimit
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultGridDataLimit"]);
            }
        }

        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}

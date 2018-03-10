using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;


namespace CorePlus.Silverlight
{
    public class CookieHelper
    {
        public static void SetCookie(string key, string value)
        {
            string oldCookie = HtmlPage.Document.GetProperty("cookie") as string;
            string cookie = string.Format("{0}={1}", key, value);
            HtmlPage.Document.SetProperty("cookie", cookie);
        }

        public static string GetCookie(string key)
        {
            string[] cookies = HtmlPage.Document.Cookies.Split(';');
            key += '=';
            foreach (string cookie in cookies)
            {
                string cookieStr = cookie.Trim();
                if (cookieStr.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                {
                    string[] vals = cookieStr.Split('=');
                    if (vals.Length >= 2)
                    {
                        return vals[1];
                    }

                    break;
                }
            }

            return string.Empty;
        }

        public static bool Exists(string key)
        {
            return HtmlPage.Document.Cookies.Contains(key);
        }

        public static bool Exists(string key, string value)
        {
            return HtmlPage.Document.Cookies.Contains(string.Format("{0}={1}", key, value));
        }
    }
}
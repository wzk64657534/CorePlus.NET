using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core
{
    public class CookieHelper
    {
        public static void SetCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string name)
        {
            return HttpContext.Current.Request.Cookies[name] == null ? null : HttpContext.Current.Request.Cookies[name].Value;
        }

        public static void RemoveCookie(string name)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Clear()
        {
            HttpContext.Current.Response.Cookies.Clear();
        }
    }
}
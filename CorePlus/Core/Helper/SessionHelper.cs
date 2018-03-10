using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;

namespace Core
{
    public class SessionHelper
    {
        public static string UserName
        {
            get { return Convert.ToString(Session["UserName"]); }
        }

        public static string ChnName
        {
            get { return Convert.ToString(Session["ChnName"]); }
        }

        public static string Email
        {
            get { return (string)SessionHelper.GetValue("Email"); }
        }

        public static string UserType
        {
            get { return Convert.ToString(Session["UserType"]); }
        }

        public static long RoleID
        {
            get { return Convert.ToInt32(Session["RoleID"]); }
        }

        public static string RoleName
        {
            get { return Convert.ToString(Session["RoleName"]); }
        }

        public static long ID
        {
            get { return Convert.ToInt64(Session["ID"]); }
        }

        public static object GetValue(string key)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Session[key];
            }
            return null;
        }

        public static void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Session["UserName"] == null)
                {
                    throw new SessionLostException();
                }
                return HttpContext.Current.Session;
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public static bool IsValid
        {
            get { return HttpContext.Current.Session != null && HttpContext.Current.Session["UserName"] != null; }
        }
    }
}

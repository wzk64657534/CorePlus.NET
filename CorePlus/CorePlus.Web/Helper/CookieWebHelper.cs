using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Common;

namespace CorePlus.Web
{
    public class CookieWebHelper : CookieCommonHelper
    {
        public static long GetAccountId
        {
            get
            {
                string value = GetCookie("AccountId");
                return string.IsNullOrEmpty(value) ? 0 : long.Parse(value);
            }
        }

        public static string GetAccountName
        {
            get
            {
                return GetCookie("AccountName");
            }
        }

        public static long GetUserId
        {
            get { return long.Parse(GetCookie("UserId") ?? "0"); }
        }
    }
}
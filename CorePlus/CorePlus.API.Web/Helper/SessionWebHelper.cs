using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Common;

namespace CorePlus.API.Web
{
    public class SessionWebHelper : SessionCommonHelper
    {
        public static long GetAccountId()
        {
            return (long)(GetValue("AccountId") ?? -1);
        }

        public static string GetAccountName()
        {
            return GetValue("AccountName").ToString();
        }
    }
}
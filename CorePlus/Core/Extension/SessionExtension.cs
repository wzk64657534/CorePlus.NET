using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;

namespace Core
{
    public static class SessionExtension
    {
        public static bool HasKey(this HttpSessionState session, string key)
        {
            return session[key] != null;
        }
    }
}
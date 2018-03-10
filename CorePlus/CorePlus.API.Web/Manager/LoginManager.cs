using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.API.Web
{
    public class LoginManager
    {
        Dictionary<string, ILogin> manager = null;

        public LoginManager()
        {
            manager = new Dictionary<string, ILogin>();
            manager.Add("UserInfoEntity", new CustomerLogin());
        }

        public string[] Login(string key, string name, string pwd)
        {
            if (manager.ContainsKey(key))
            {
                return manager[key].Login(name, pwd);
            }

            return null;
        }
    }
}
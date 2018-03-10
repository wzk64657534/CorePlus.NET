using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.Web
{
    public interface ILogin
    {
        string[] Login(string name, string pwd);
    }
}
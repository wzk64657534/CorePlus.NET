using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Core;


namespace CorePlus.Visit
{
    public class Global : BaseApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            InitNameSpaces();
        }
    }
}
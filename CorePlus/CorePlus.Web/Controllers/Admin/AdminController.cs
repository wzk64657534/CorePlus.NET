using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using CorePlus.Entity;
using Core;

namespace CorePlus.Web
{
    [Authorize(Roles = "administrator")]
    public class AdminController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
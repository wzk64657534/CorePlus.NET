using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class SideMenuController : AdminBaseController<SideMenuRepository, SideMenuInfoEntity>
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
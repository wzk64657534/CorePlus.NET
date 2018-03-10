using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class MenuController : AdminBaseController<MenuRepository, MenuInfoEntity>
    {
        [ChildActionOnly]
        public ActionResult AdminMenu()
        {
            var menus = Repository.FindAll().Where(x => x.IsToUser == false);

            return PartialView(menus);
        }
    }
}
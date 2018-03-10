using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
      public class WxMenuController : WxUserNameController<WxMenuRepository, WxMenuEntity>
      {
            public virtual ActionResult GetFirstMenu()
            {
                  var query = Repository.GetInFirstLevel(User.Identity.Name);
                  return Json(query, JsonRequestBehavior.AllowGet);
            }

            public override ActionResult Add()
            {
                  ViewBag.FirstMenus = Repository.GetInFirstLevel(User.Identity.Name);
                  return base.Add();
            }

            public override ActionResult Add(WxMenuEntity model)
            {
                  ViewBag.FirstMenus = Repository.GetInFirstLevel(User.Identity.Name);
                  if (Repository.CheckMenuRole(model, "add"))
                  {
                        return base.Add(model);
                  }
                  else
                  {
                        return Content("不符合规则，主菜单不超过3个，子菜单不超过5个");
                  }
            }

            public override ActionResult Edit(long id)
            {
                  ViewBag.FirstMenus = Repository.GetInFirstLevel(User.Identity.Name);
                  return base.Edit(id);
            }

            public override ActionResult Edit(long id, WxMenuEntity model)
            {
                  ViewBag.FirstMenus = Repository.GetInFirstLevel(User.Identity.Name);
                  if (Repository.CheckMenuRole(model, "edit"))
                  {
                        return base.Edit(id, model);
                  }
                  else
                  {
                        return Content("不符合规则，主菜单不超过3个，子菜单不超过5个");
                  }
            }

            public ActionResult Use()
            {
                  string result = Repository.Use(User.Identity.Name);
                  return Content(result);
            }
      }
}
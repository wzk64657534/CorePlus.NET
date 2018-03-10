using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public class WxAccountController : WxUserNameController<WxAccountRepository, WxAccountEntity>
    {
        public override ActionResult Index(long? id, long? arg)
        {
            string username = User.Identity.Name;
            var entity = Repository.FindByExpression(x => x.UserName == username).FirstOrDefault();
            if (entity == null)
            {
                entity = Repository.NewEntity(username);
            }

            return View(entity);
        }

        public override ActionResult Edit(long id)
        {
            string username = User.Identity.Name;
            var entity = Repository.FindByExpression(x => x.UserName == username).FirstOrDefault();
            if (entity == null)
            {
                entity = Repository.NewEntity(username);
            }

            return View(entity);
        }

        [HttpPost]
        public override ActionResult Edit(long id, WxAccountEntity uiEntity)
        {
            if (CheckError())
            {
                var entity = Repository.Update(id, uiEntity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(uiEntity);
            }
        }

        public ActionResult ResetWeiXinValidating()
        {
            var result = Repository.ResetWeiXinValidating(User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
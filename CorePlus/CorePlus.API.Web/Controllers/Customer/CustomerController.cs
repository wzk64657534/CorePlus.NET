using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.Entity;
using CorePlus.Repository;
using Core;

namespace CorePlus.API.Web
{
    [Authorize(Roles = "customer")]
    public class CustomerController : FrontUserController<UserRepository, UserInfoEntity>
    {
        public ActionResult EditInfo()
        {
            var user = Repository.FindByExpression(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "用户不存在");
                return View();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditInfo(UserInfoEntity model)
        {
            try
            {
                model = Repository.Update(model.ID, model);
                ModelState.AddModelError("", "修改成功");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }
    }
}
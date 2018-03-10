using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Entity;
using CorePlus.Entity;

namespace CorePlus.Web
{
    public class FrontUserController<TRepository, TEntity> : FrontBaseController<TRepository, TEntity>
        where TRepository : SimpleRepository<TEntity>, new()
        where TEntity : UserEntity, new()
    {
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordEntity model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    var user = Repository.FindByExpression(x => x.UserName == User.Identity.Name).FirstOrDefault();

                    if (user == null)
                    {
                        ModelState.AddModelError("", "当前用户不存在");
                    }

                    if (!Equals(user.UserPwd, CryptHelper.MD5(model.OldPassword)))
                    {
                        ModelState.AddModelError("", "旧密码不正确");
                    }

                    user.UserPwd = CryptHelper.MD5(model.NewPassword);
                    Repository.Update(user);
                    changePasswordSucceeded = true;
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    ModelState.AddModelError("", "修改成功");
                }

                ModelState.AddModelError("", "修改失败，请检查后重试");
            }
            else
            {
                ModelState.AddModelError("", "系统错误，检查数据后再提交");
            }

            return View(model);
        }

        public override List<TEntity> GetSelectedData(List<TEntity> query)
        {
            return (from x in query
                    where x.UserName == User.Identity.Name
                    select x).ToList();
        }
    }
}
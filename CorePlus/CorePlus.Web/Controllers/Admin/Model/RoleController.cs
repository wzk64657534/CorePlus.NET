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
    public class RoleController : AdminBaseController<RoleRepository, RoleInfoEntity>
    {
        public override ActionResult Create()
        {
            ViewBag.MenuAll = Repository.GetCurrentRoleMenu(0);
            return base.Create();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RoleInfoEntity entity, long[] menuIds)
        {
            if (entity.RoleName.ToLower().Contains(ConstWebHelper.RoleAdministratorForShort))
            {
                ModelState.AddModelError("", "超级管理员角色不可重复新增");
                return RedirectToAction("Index");
            }

            if (CheckError())
            {
                if (Repository.SetRoleMenu(entity, menuIds))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "新增失败");
                    return View(entity);
                }
            }
            else
            {
                return View(entity);
            }
        }

        public ActionResult EditWithMenuIds(long id)
        {
            if (Repository.IsAdministrator(id))
            {
                ModelState.AddModelError("", "超级管理员角色不可编辑");
                return RedirectToAction("Index");
            }

            ViewBag.MenuAll = Repository.GetCurrentRoleMenu(id);
            return Edit(id);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWithMenuIds(RoleInfoEntity entity, long[] menuIds)
        {
            ViewBag.MenuAll = Repository.GetCurrentRoleMenu(entity.ID);

            if (CheckError())
            {
                if (Repository.UpdateRoleMenu(entity, menuIds))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "更新失败");
                    return View(entity);
                }
            }
            else
            {
                return View(entity);
            }
        }

        public override ActionResult Delete(long id)
        {
            if (!Repository.IsAdministrator(id))
            {
                ModelState.AddModelError("", "超级管理员角色不可删除");
                return RedirectToAction("Index");
            }

            return base.Delete(id);
        }
    }
}
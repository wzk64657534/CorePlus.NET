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
    public class ParamController : SimpleDetailController<ParamRepository, ParamInfoEntity, ParamDtsInfoEntity>
    {
        public ActionResult ParamDtsIndex(long? id)
        {
            ViewBag.ParamInfoEntity = Repository.FindByID(id ?? 0);
            return GetDetail(id);
        }

        public ActionResult DeleteDts(long id, int seq)
        {
            Repository.DeleteDts(id, seq);

            return RedirectToAction("ParamDtsIndex", new { id = id });
        }

        public ActionResult ParamDtsUpdate(long id, int seq)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ParamDtsUpdate(ParamDtsInfoEntity entity)
        {
            try
            {
                Repository.SaveDts(entity);
                ModelState.AddModelError("", "保存成功");
                return View(new ParamDtsInfoEntity());
            }
            catch
            {
                ModelState.AddModelError("", "序号重复");
            }

            return View(entity);
        }
    }
}
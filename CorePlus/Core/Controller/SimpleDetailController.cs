using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core
{
    public class SimpleDetailController<TRepository, TEntity, TDetail> : SimpleController<TRepository, TEntity>
        where TRepository : SimpleDetailRepository<TEntity, TDetail>, new()
        where TEntity : SimpleDetailEntity<TDetail>, new()
        where TDetail : DetailEntity, new()
    {
        public ActionResult GetDetail(Nullable<long> id)
        {
            //return new JsonResult() { Data = Repository.GetDetail(id), JsonRequestBehavior = JsonRequestBehavior.AllowGet, ContentType = "application/json" };
            var list = Repository.GetDetail(id);
            return View(list);
        }
    }
}
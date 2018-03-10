using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class VisitorComparisonController : BaseVisitController<VisitorComparisonRepository, ResourceClassificationBannerEntity>
    {
        public JsonResult GetListVisitor(DateTime start, DateTime end)
        {
            var query = Repository.GetListVisitor(start, end);

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}

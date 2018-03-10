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
    [Authorize(Roles = "customer")]
    public class BaseVisitController<TRepository, TBannerEntity> : BaseController<TRepository, VisitInfoEntity>
        where TRepository : VisitRepository<TBannerEntity>, new()
        where TBannerEntity : VisitBannerEntity, new()
    {
        public override ActionResult Index(long? id, long? arg)
        {
            return View();
        }

        public virtual JsonResult Banner(DateTime start, DateTime end)
        {
            var query = Repository.GetDataOfBanner(start, end);
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetDataOfChartForXY(DateTime start, DateTime end, int? sourceType)
        {
            var query = Repository.GetDataOfChartForXY(start, end, sourceType);
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetDataOfGrid(int? page, int? rows, DateTime start, DateTime end, int? tag, string title)
        {
            int recordCount = 0;
            var query = Repository.GetDataOfGrid(page ?? 1, rows ?? 10, start, end, tag ?? 1, title, out recordCount);
            VisitGridEntity entity = new VisitGridEntity();
            entity.total = recordCount;
            entity.rows = query;
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
    }
}
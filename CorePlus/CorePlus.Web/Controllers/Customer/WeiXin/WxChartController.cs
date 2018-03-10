using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using CorePlus.WeiXin.Repository;

namespace CorePlus.Web
{
    [Authorize(Roles = "customer")]
    public class WxChartController : BaseController<WxChartRepository, BaseEntity>
    {
        public override ActionResult Index(long? id, long? arg)
        {
            return View();
        }

        public JsonResult GetChartMessage(DateTime start, DateTime end)
        {
            var query = Repository.GetChartMessage(User.Identity.Name, start, end);
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetChartSubscribe(DateTime start, DateTime end)
        {
            var query = Repository.GetChartSubscribe(User.Identity.Name, start, end);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
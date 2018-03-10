using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;
using Core;
using CorePlusEntity;
using CorePlusRepository;

namespace CorePlusWeb
{
    public class ResourceAnalyseController : VisitController<ResourceAnalyseRepository>
    {
        #region =========来源分类===========

        public ActionResult ResourceAnalyseIndex()
        {
            return View();
        }

        //public JsonResult ResourceAnalyseBanner(DateTime start, DateTime end)
        //{
        //    VisitDomainBannereEntity entity = Repository.GetDataOfVisitDomainBanner(start, end);
        //    return Json(entity, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}
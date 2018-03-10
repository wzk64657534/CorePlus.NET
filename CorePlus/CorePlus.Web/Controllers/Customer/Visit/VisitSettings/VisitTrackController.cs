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
    public class VisitTrackController : BaseVisitController<VisitTrackRepository, VisitBannerEntity>
    {
        public ActionResult GetTrackJs()
        {
            var query = Repository.GetTrackJs();
            return Content(query);
        }
    }
}
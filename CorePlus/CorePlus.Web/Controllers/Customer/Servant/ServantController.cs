using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;
using System.Reflection;

namespace CorePlus.Web
{
    [Authorize(Roles = "customer")]
    public class ServantController : FrontUserController<ServantRepository, ServantInfoEntity>
    {
        public override ActionResult Create()
        {
            long userId = long.Parse(CookieHelper.GetCookie("UserId"));
            var entity = Repository.NewEntity(userId);
            return View(entity);
        }

        public override List<ServantInfoEntity> GetSelectedData(List<ServantInfoEntity> query)
        {
            long userId = long.Parse(CookieHelper.GetCookie("UserId"));
            return (from x in query
                    where x.UserId == userId
                    select x).ToList();
        }
    }
}
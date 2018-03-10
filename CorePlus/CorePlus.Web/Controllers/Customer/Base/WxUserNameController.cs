using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Entity;
using CorePlus.WeiXin.Entity;
using CorePlus.Repository;
using CorePlus.WeiXin.Repository;

namespace CorePlus.Web
{
    public class WxUserNameController<TRepository, TEntity> : FrontBaseController<TRepository, TEntity>
        where TRepository : WxUserNameRepository<TEntity>, new()
        where TEntity : WxUserEntity, new()
    {
        public override List<TEntity> GetSelectedData(List<TEntity> query)
        {
            var data = from x in query
                       where x.UserName == User.Identity.Name
                       select x;

            return data.ToList();
        }

        public ActionResult GetMatchTypeList()
        {
            return Json(EntityWebHelper.GetMatchTypeList(), JsonRequestBehavior.AllowGet);
        }
    }
}
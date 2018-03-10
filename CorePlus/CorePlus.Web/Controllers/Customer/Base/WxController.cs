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
    [Authorize(Roles = "customer")]
    public class WxController<TRepository, TEntity> : FrontBaseController<TRepository, TEntity>
        where TRepository : WxRepository<TEntity>, new()
        where TEntity : BaseEntity, new()
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public class WxReplyImageTextController : WxUserNameController<WxReplyImageTextRepository, WxReplyImageTextEntity>
    {
        public override List<WxReplyImageTextEntity> GetSelectedData(List<WxReplyImageTextEntity> query)
        {
            return base.GetSelectedData(query).Where(x => x.Keyword != "subscribe").ToList();
        }

        public override ActionResult Add()
        {
            ViewBag.All = Repository.FindAllOfSelf(User.Identity.Name);
            return base.Add();
        }

        public override ActionResult Edit(long id)
        {
            var entity = Repository.FindByID(id);
            ViewBag.All = Repository.CheckSelected(User.Identity.Name, id, entity);
            return View(entity);
        }
    }
}
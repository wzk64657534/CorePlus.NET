using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public class WxSubscribeConfigController : WxUserNameController<WxSubscribeConfigRepository, WxSubscribeConfigEntity>
    {
        public override ActionResult Index(long? id, long? arg)
        {
            string username = User.Identity.Name;
            var entity = Repository.FindByExpression(x => x.UserName == username).FirstOrDefault();
            if (entity == null)
            {
                entity = Repository.NewEntity(username);
            }

            return View(entity);
        }

        public override ActionResult Edit(long id)
        {
            string username = User.Identity.Name;
            var entity = Repository.FindByExpression(x => x.UserName == username).FirstOrDefault();
            if (entity == null)
            {
                entity = Repository.NewEntity(username);
            }

            return View(entity);
        }

        [HttpPost]
        public override ActionResult Edit(long id, WxSubscribeConfigEntity uiEntity)
        {
            if (CheckError())
            {
                var entity = Repository.Update(id, uiEntity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(uiEntity);
            }
        }

        public ActionResult EditText()
        {
            var entity = Repository.EditText(User.Identity.Name);
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditText(WxReplyTextEntity entity)
        {
            try
            {
                WxReplyTextRepository repository = new WxReplyTextRepository();
                if (entity.ID == 0)
                {
                    repository.Add(entity);
                }
                else
                {
                    repository.Update(entity.ID, entity);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(entity);
            }
        }

        public ActionResult EditImageText()
        {
            string username = User.Identity.Name;
            var list = Repository.GetAllWithSubscribe(username);
            var entity = Repository.EditImageText(username, list);
            ViewBag.All = list;
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditImageText(WxReplyImageTextEntity entity)
        {
            try
            {
                string username = User.Identity.Name;
                ViewBag.All = Repository.GetAllWithSubscribe(username);

                Repository.EditImageText(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(entity);
            }
        }
    }
}
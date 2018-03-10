using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.Entity;
using Core;
using CorePlus.Repository;
using System.Web.Security;
using System.Reflection;
using Core.Entity;
using System.IO;

namespace CorePlus.API.Web
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Menu = MenuHelper.GetMainMenu(User.Identity.Name);
            return View();
        }

        [Authorize]
        public ActionResult SubIndex(int id)
        {
            IndexManager manager = new IndexManager();
            ViewBag.Left = manager.GetLeftMenu(id, new string[] { User.Identity.Name });
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginEntity model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 验证码校验
                    if (Equals(Session["ValidateCode"], model.ValidateCode) == false)
                    {
                        ModelState.AddModelError("", "验证码不正确");
                        model.ValidateCode = string.Empty;
                        return View(model);
                    }

                    var types = Assembly.Load("CorePlus.Entity").GetTypes().Where(x => x.IsSubclassOf(typeof(UserEntity)));
                    string[] rtn = null;
                    LoginManager manager = new LoginManager();
                    foreach (var item in types)
                    {
                        rtn = manager.Login(item.Name, model.UserName, model.Password);
                        if (rtn != null) { break; }
                    }

                    if (rtn != null)
                    {
                        return RedirectToAction(rtn[0], rtn[1]);
                    }
                    else
                    {
                        ModelState.AddModelError("", "提供的用户名或密码不正确。");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            CookieHelper.RemoveCookie("UserId");
            CookieHelper.RemoveCookie("UserName");
            CookieHelper.RemoveCookie("RoleName");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetValidateCode()
        {
            string code = ValidationHelper.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = ValidationHelper.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public virtual ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return Content("没有文件");
            }
            else if (file.ContentLength > (1024 * 1024))
            {
                return Content("文件大小不超过1M");
            }

            var fileName = Path.Combine(Request.MapPath("~/Upload"), Path.GetFileName(file.FileName));
            string url = string.Format("http://{0}{1}/Upload/{2}",
                Request.Url.Host,
                Request.Url.IsDefaultPort ? string.Empty : ":" + Request.Url.Port.ToString(),
                Path.GetFileName(file.FileName));

            try
            {
                file.SaveAs(fileName);
                return Content(url);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
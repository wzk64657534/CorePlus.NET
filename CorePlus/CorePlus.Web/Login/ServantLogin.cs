using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using Core;

namespace CorePlus.Web
{
    public class ServantLogin : BaseLogin<ServantInfoEntity>
    {
        protected override string[] RedirectToAction()
        {
            return new string[] { "Index", "Worker" };
        }

        protected override void ExtraCookies(ServantInfoEntity entity)
        {
            CookieHelper.SetCookie("HighId", entity.UserId.ToString());
        }
    }
}
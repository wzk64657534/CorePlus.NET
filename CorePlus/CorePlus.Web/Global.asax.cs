using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;
using Core;
using CorePlus.Entity;
using System.Web.Security;

namespace CorePlus.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : BaseApplication
    {
        public MvcApplication()
            : base()
        {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }

        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            IIdentity id = Context.User.Identity;
            if (id.IsAuthenticated)
            {
                string roleName = CookieHelper.GetCookie("RoleName");
                if (!string.IsNullOrWhiteSpace(roleName))
                {
                    Context.User = new GenericPrincipal(id, new string[] { roleName });
                }
            }
        }

        protected override string DefaultController
        {
            get
            {
                return "Home";
            }
        }

        protected override void RegisterOtherRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Admin", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Log", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Log", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Baidu", // 路由名称
                "{controller}/{action}/{id}/{arg}", // 带有参数的 URL
                new { controller = "Baidu", action = "Index", id = UrlParameter.Optional, arg = UrlParameter.Optional });

            base.RegisterOtherRoutes(routes);
        }
    }
}
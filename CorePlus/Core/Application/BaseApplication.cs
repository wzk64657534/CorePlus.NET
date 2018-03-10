using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;


namespace Core
{
    public class BaseApplication : System.Web.HttpApplication
    {
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            InitNameSpaces();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelValidatorProviders.Providers.Add(new DbValueOnlyValidatorProvider());
        }

        protected virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                DefaultController, // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = DefaultController, action = DefaultAction, id = UrlParameter.Optional } // Parameter defaults
            );

            RegisterOtherRoutes(routes);
        }

        protected virtual string DefaultController
        {
            get { return "Home"; }
        }

        protected virtual string DefaultAction
        {
            get { return "Index"; }
        }

        protected virtual void RegisterOtherRoutes(RouteCollection routes)
        {

        }

        protected virtual void InitNameSpaces()
        {
            var namespaces = ConfigurationHelper.Namespaces;
            if (!string.IsNullOrEmpty(namespaces))
            {
                try
                {
                    var ns = StringHelper.Split(namespaces, ',');
                    foreach (var item in ns)
                    {
                        Assembly.Load(string.Format("{0}", item));
                    }
                }
                catch
                {

                }
            }
        }

        protected virtual void Application_End()
        {

        }
    }
}
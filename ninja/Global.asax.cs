using ninja.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ninja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterBinders();
        }

        /// <summary>
        /// Registers the custom application model binders.
        /// </summary>
        private void RegisterBinders()
        {
            ModelBinders.Binders.Add(typeof(System.DateTime), new DateTimeModelBinder());
        }
    }
}

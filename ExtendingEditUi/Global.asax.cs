using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExtendingEditUi
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
               name: "AlloyApiMethodCall",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional });

            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            routes.MapRoute(
                     "ProfileAdmin",
                     "profileadmin/{action}",
                new { controller = "ProfileAdmin", action = "index" });
        }
    }
}
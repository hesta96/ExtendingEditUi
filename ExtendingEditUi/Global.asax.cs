using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ExtendingEditUi.Business;
using ExtendingEditUi.Business.Initialization;
using StructureMap;


namespace ExtendingEditUi
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //ConfigureWebApi();
        }

        //private void ConfigureWebApi()
        //{
        //    GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(new Container(DependencyResolverInitialization.ConfigureContainer));
        //}

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            routes.MapHttpRoute(name: "v", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });

            routes.MapRoute(
                     "ProfileAdmin",
                     "profileadmin/{action}",
                new { controller = "ProfileAdmin", action = "index" });
        }
    }
}
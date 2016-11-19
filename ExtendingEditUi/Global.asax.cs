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
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            routes.MapHttpRoute("webapi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

            routes.MapRoute("ProfileAdmin", "profileadmin/{action}", new { controller = "ProfileAdmin", action = "index" });
            routes.MapRoute("ExistingPagesReport", "existingpagesreport/{action}", new { controller = "ExistingPagesReport", action = "Index" });
        }
    }
}
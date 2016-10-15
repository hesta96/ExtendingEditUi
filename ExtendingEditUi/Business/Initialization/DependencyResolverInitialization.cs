using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using ExtendingEditUi.Business.Rendering;
using ExtendingEditUi.Helpers;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using ExtendingEditUi.Business.Repositories;
using StructureMap;

namespace ExtendingEditUi.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(ConfigureContainer);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
        }

        public static void ConfigureContainer(ConfigurationExpression container)
        {
            //Swap out the default ContentRenderer for our custom
            container.For<IContentRenderer>().Use<ErrorHandlingContentRenderer>();
            container.For<ContentAreaRenderer>().Use<AlloyContentAreaRenderer>();

            container.Scan(c =>
            {
                c.AssemblyContainingType<IUserProfileRepository>();
                c.WithDefaultConventions();
            });
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}

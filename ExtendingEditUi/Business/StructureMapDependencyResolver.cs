using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace ExtendingEditUi.Business
{
    public class StructureMapDependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return GetInterfaceService(serviceType);
            }
            return GetConcreteService(serviceType); 
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                // Can't use TryGetInstance here because it won’t create concrete types
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        #region Http IDependencyResolver

        public IDependencyScope BeginScope()
        {
            var childContainer = _container.GetNestedContainer();
            return new StructureMapScope(childContainer);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        #endregion
    }
}

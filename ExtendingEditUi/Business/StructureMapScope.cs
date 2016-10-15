using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace ExtendingEditUi.Business
{
    public class StructureMapScope : IDependencyScope
    {
        private readonly IContainer container;

        public StructureMapScope(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container cannot be null");
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            if (serviceType.IsAbstract || serviceType.IsInterface) return container.TryGetInstance(serviceType);

            return container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
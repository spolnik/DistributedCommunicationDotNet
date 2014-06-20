using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace NProg.Distributed.Service.Composition
{
    [Export]
    public sealed class ServiceComposer
    {
        [ImportMany]
        public IEnumerable<Lazy<IServiceFactory, INamedMetadata>> ServiceFactories { get; set; }

        public IServiceFactory GetFactory(string name)
        {
            return ServiceFactories
              .Where(l => l.Metadata.Name.Equals(name))
              .Select(l => l.Value)
              .FirstOrDefault();
        }

        public static IServiceFactory GetServiceFactory(string framework)
        {
            var mainDirectoryCatalog = new DirectoryCatalog(".");
            var compositionContainer = new CompositionContainer(mainDirectoryCatalog);
            var export = compositionContainer.GetExport<ServiceComposer>();

            return export == null
                ? null
                : export.Value.GetFactory(framework);
        }
    }
}
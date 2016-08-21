
using Enterprise.Model;
using Enterprise.Overspesification.Services;
using Enterprise.Services.Common;
using Ninject;
using Ninject.Modules;

namespace Enterprise.Services.ServiceLocator
{
    public class ServiceContainerNinjectModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICatalogDataSetService>().To<CatalogServiceDataSet>();
            this.Bind<ILibraryDataParser>().To<LibraryDataParser>();
        }
    }
}

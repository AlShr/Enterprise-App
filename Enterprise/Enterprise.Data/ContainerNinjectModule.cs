using Enterprise.CoreData.DataInterfaces;
using Ninject.Modules;

namespace Enterprise.Data
{
    public class ContainerNinjectModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBookDao>().To<BookDao>();
            this.Bind<IAuthorDao>().To<AuthorDao>();
            this.Bind<IPublisherDao>().To<PublisherDao>();
            this.Bind<IItemDao>().To<ItemDao>();
            this.Bind<IInventoryItemDao>().To<InventoryItemDao>();
        }
    }
}

using System;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;
using Ninject;

namespace Enterprise.Data
{
    public class OrderDao : AbstractNHibernateDao<Order, long>, IOrdreDao
    {
        /// <summary>
        /// Reader make ordering readingcart selection
        /// Make record in inventroyitems that item of readercart is ordered
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public Order MakeOrder(ReadingCart cart)
        {
            Check.Require(cart != null, "Readingcart must be provided to make order");
            Order order = new Order();
            order.OrderedByReader = cart.CartOfReader;
            order.OrderNumber = cart.CartNumber;
            order.OrderDate = DateTime.Now;

            IInventoryItemDao tmpinventoryItem = this.InventoryItemDao;
            foreach (ReaderCartSelection selection in cart.CartSelections)
            {
                InventoryItem inventoryItem = tmpinventoryItem.GetBy(x => x.BookItem.ID == selection.CurrentBook.ID);
                if (inventoryItem.IsOrdered == false)
                {
                    Item item = selection.Item;
                    if (item.ApprovedOrder == null)
                    {
                        SeItemAsOrdered(item);
                        SetInventoryItemAsOrdered(inventoryItem);
                        order.OrderItems.Add(item);
                    }                 
                }              
            }
            return order;
        }

        public void SeItemAsOrdered(Item item)
        {
            Check.Require(item != null, "Item must be provided");
            item.IsReadCarted = false;
            item.IsOrdered = true;
            ItemDao.SaveOrUpdate(item);
        }

        public void SetInventoryItemAsOrdered(InventoryItem item)
        {
            Check.Require(item != null, "Item must be provided");
            item.IsReadCarted = false;
            item.IsOrdered = true;
            InventoryItemDao.SaveOrUpdate(item);
        }

        public IInventoryItemDao InventoryItemDao
        {
            get 
            {
                if (inventoryItemDao == null)
                {
                    inventoryItemDao = DaoLocatorNinject.AppKernel.Get<InventoryItemDao>();
                }
                return inventoryItemDao;
            }
            set 
            {
                inventoryItemDao = value;
            }
        }

        public IItemDao ItemDao
        {
            get
            {
                if (itemDao == null)
                {
                    itemDao = DaoLocatorNinject.AppKernel.Get<ItemDao>();
                }
                return itemDao;
            }
            set
            {
                itemDao = value;
            }
        }

        public IReaderCartSelectionDao ReaderCartSelectioDao
        {
            get 
            {
                if (readerCartSelectionDao == null)
                {
                    readerCartSelectionDao = DaoLocatorNinject.AppKernel.Get<ReaderCartSelectionDao>();
                }
                return readerCartSelectionDao;
            }
            set 
            {
                readerCartSelectionDao = value;
            }
        }

        private IItemDao itemDao;
        private IInventoryItemDao inventoryItemDao;
        private IReaderCartSelectionDao readerCartSelectionDao;
    }
}

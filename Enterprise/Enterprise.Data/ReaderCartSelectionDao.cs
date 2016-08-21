using System;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;
using Ninject;

namespace Enterprise.Data
{
    public class ReaderCartSelectionDao : AbstractNHibernateDao<ReaderCartSelection, long>, IReaderCartSelectionDao
    {
        /// <summary>
        /// reader make select bookdescription to add item to his readingcart
        /// Make record in inventoryitems that item of readercart is readcarted if pre state not readcarted
        /// </summary>
        public ReaderCartSelection AddSelectionToReaderCart(Book bookDescription, ReadingCart cart)
        {
            Check.Require(bookDescription != null, "Book must be provided");
            Check.Require(cart != null, "ReadingCart must be provided");
            var inventotyItem = this.InventoryItemDao.GetBy(x => x.BookItem.ID == bookDescription.ID);
            if (!inventotyItem.IsOrdered)
            {
                inventotyItem.IsReadCarted = true;
                this.InventoryItemDao.SaveOrUpdate(inventotyItem);
                Item item = CreateItem(inventotyItem);
                this.ItemDao.Save(item);
                return new ReaderCartSelection(bookDescription, cart, item);
            }
            return null;         
        }

        public Item CreateItem(InventoryItem inventoryItem)
        {
            return new Item(inventoryItem);
        }

        public IInventoryItemDao InventoryItemDao
        {
            get 
            {
                if (inventoryItemDao == null)
                {
                    inventoryItemDao=DaoLocatorNinject.AppKernel.Get<InventoryItemDao>();
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

        private IInventoryItemDao inventoryItemDao;
        private IItemDao itemDao;
    }
}

using System;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;
using Ninject;

namespace Enterprise.Data
{
    public class ApprovedOrderDao : AbstractNHibernateDao<ApprovedOrder, long>, IApprovedOrderDao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ApprovedOrder MakeApprovedOrder(Order order)
        {
            Check.Require(order != null, "Order must be provided to make approved order");
            return CreateApprovedOrder(order);
        }

        public ApprovedOrder CreateApprovedOrder(Order order)
        {
            Check.Require(order != null, "Order must be provided");
            ApprovedOrder approvedOrder = new ApprovedOrder(order);
            approvedOrder.OrderedDate = order.OrderDate;
            approvedOrder.ApprovedNumber = OrderNumberHelper.GenerateOrderNumber();
            this.Save(approvedOrder);
            return approvedOrder;
        }

        public void MakeInventoryItemAsOrdered(Order order)
        {
            Check.Require(order != null, "Order must be provided");
            IInventoryItemDao tmpInventoryItemDao = this.InventoryItemDao;
            foreach (InventoryItem item in order.OrderInventoryItems)
            {
                item.IsReadCarted = false;
                item.IsOrdered = true;             
                tmpInventoryItemDao.SaveOrUpdate(item);
            }
        }

        public void MakeItemAsOrdered(Order order)
        {
            Check.Require(order != null, "Order must be provided");
            IItemDao tmpItemDao = this.ItemDao;
            foreach (Item item in order.OrderItems)
            {
                item.IsReadCarted = false;
                item.IsOrdered = true;
                tmpItemDao.SaveOrUpdate(item);
            }
        }

        public void MakeItemRecovering(Book book, ApprovedOrder order, DateTime date)
        {
            var item = this.ItemDao.GetBy(x => x.ItemDescription.ID == book.ID && x.ApprovedOrder.ID == order.ID);
            item.IsOrdered = false;
            item.RecoveredDate = date;
            this.ItemDao.SaveOrUpdate(item);
        }

        public void MakeInventoryItemRecovering(Book book)
        {
            var inventoryItem = this.InventoryItemDao.GetBy(x => x.BookItem.ID == book.ID);
            inventoryItem.IsOrdered = false;
            this.InventoryItemDao.SaveOrUpdate(inventoryItem);
        }

        public void RecoveringApprovedOrderItem(Book book, ApprovedOrder order, DateTime date)
        {
            Check.Require(book != null, "Book must be provided ");
            MakeItemRecovering(book, order, date);
            MakeInventoryItemRecovering(book);
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

        private IReadingCartDao ReadingCartDao
        {
            get 
            {
                if (readingCartDao == null)
                {
                    readingCartDao = DaoLocatorNinject.AppKernel.Get<ReadingCartDao>();
                }
                return readingCartDao;
            }
            set 
            {
                readingCartDao = value;
            }
        }

        private IReadingCartDao readingCartDao;
        private IItemDao itemDao;
        private IInventoryItemDao inventoryItemDao;
    }
    class OrderNumberHelper
    {
        public static string GenerateOrderNumber()
        {
            // GetTodaysDate and make orderno from it
            //by concatenating orderno with it

            orderNo++;
            return ((DateTime.Now.ToShortDateString()).Replace("/", "") + orderNo.ToString());
        }
        private static int orderNo;
    }
}

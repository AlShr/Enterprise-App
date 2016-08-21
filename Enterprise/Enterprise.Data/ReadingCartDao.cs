using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;
using System.Collections.Generic;
using Ninject;
using Enterprise.CoreData.LuceneIndex;
using System;


namespace Enterprise.Data
{
    public class ReadingCartDao : AbstractNHibernateDao<ReadingCart, long>, IReadingCartDao
    {
        /// <summary>
        /// Check List BookId if Inventory Item not readcarted or not ordered
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="bookIds"></param>
        /// <returns></returns>
        public List<long> SetReaderCartSelections(Reader reader, List<long> bookIds)
        {
            Check.Require(reader != null, "reader must be provided");
            ReadingCart cart = GetReaderCart(reader);
            IReaderCartSelectionDao readercartDao = this.ReaderCartSelectionDao;
            IBookDao bookDao = this.BookDao;
            List<long> readcartedIds = new List<long>();
            foreach (long bookId in bookIds)
            {
                var book = bookDao.GetBy(x => x.ID == bookId);
                var selection = readercartDao.AddSelectionToReaderCart(book, cart);
                Check.Ensure(selection != null, "select not avaliable");
                readercartDao.Save(selection);
                readcartedIds.Add(bookId);
            }
            return readcartedIds;
        }

        public void SetReaderCartSelection(Reader reader, Book book)
        {
            Check.Require(reader != null, "Reader must be provided");
            ReadingCart cart = GetReaderCart(reader);
            var select = this.ReaderCartSelectionDao.AddSelectionToReaderCart(book, cart);
            this.ReaderCartSelectionDao.Save(select);
        }

        public void SetReaderCartSelection(Reader reader, long bookId)
        {
            Check.Require(reader != null, "Reader must be provided");
            ReadingCart cart = GetReaderCart(reader);
            var book = this.BookDao.GetBy(x => x.ID == bookId);
            var select = this.ReaderCartSelectionDao.AddSelectionToReaderCart(book, cart);
            this.ReaderCartSelectionDao.Save(select);

        }

        public void RemoveReaderCartSelection(Reader reader, long bookId, long readercartselectionId)
        {
            Check.Require(reader != null, "reader must be provided");
            ReaderCartSelection select = this.ReaderCartSelectionDao.GetBy(x => x.ID == readercartselectionId);
            var item = select.Item;
            var inventoryitem = this.InventoryItemDao.GetBy(x => x.BookItem.ID == bookId);
            if (item.IsReadCarted)
            {
                inventoryitem.IsReadCarted = false;
                this.InventoryItemDao.SaveOrUpdate(inventoryitem);
                this.ItemDao.Delete(item);
            }
        }

        public ReadingCart GetReaderCart(Reader reader)
        {
            Check.Require(reader != null, "Reader must be provided");
            ReadingCart cart = this.ReaderCartDao.GetBy(x => x.CartOfReader.ID == reader.ID);
            if (cart == null)
            {
                cart = new ReadingCart(reader);
                cart.CartNumber = DateTime.Now.ToString();
                this.Save(cart);
            }
            return cart;
        }

        public virtual IReaderDao ReaderDao
        {
            get
            {
                if(readerDao==null)
                {
                    readerDao=DaoLocatorNinject.AppKernel.Get<ReaderDao>();
                }
                return readerDao;
            }
        }
        public virtual IBookDao BookDao
        {
            get
            {
                if (bookDao == null)
                {
                    bookDao = DaoLocatorNinject.AppKernel.Get<BookDao>();
                }
                return bookDao;
            }
        }

        public virtual IReaderCartSelectionDao ReaderCartSelectionDao
        {
            get 
            {
                if (readerCartSelectionDao == null)
                {
                    readerCartSelectionDao = DaoLocatorNinject.AppKernel.Get<ReaderCartSelectionDao>();
                }
                return readerCartSelectionDao;
            }
        }

        public virtual IItemDao ItemDao
        {
            get 
            {

                if (itemDao == null)
                {
                    itemDao = DaoLocatorNinject.AppKernel.Get<ItemDao>();
                }
                return itemDao;
            }
        }

        public virtual IInventoryItemDao InventoryItemDao
        {
            get 
            {
                if (inventoryItemDao == null)
                {
                    inventoryItemDao = DaoLocatorNinject.AppKernel.Get<IInventoryItemDao>();
                }
                return inventoryItemDao;
            }
            set 
            {
                inventoryItemDao = value;
            }
        }

        public virtual IReadingCartDao ReaderCartDao
        {
            get 
            {
                if (readerCartDao == null)
                {
                    readerCartDao = DaoLocatorNinject.AppKernel.Get<ReadingCartDao>();
                }
                return readerCartDao;
            }
        }

        private IReadingCartDao readerCartDao;
        private IReaderCartSelectionDao readerCartSelectionDao;
        private IBookDao bookDao;
        private IReaderDao readerDao;
        private IItemDao itemDao;
        private IInventoryItemDao inventoryItemDao;
    }   
}

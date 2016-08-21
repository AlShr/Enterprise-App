using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;
using System.Collections.Generic;
using Ninject;
using Enterprise.CoreData.LuceneIndex;


namespace Enterprise.Data
{
    /// <summary>
    /// Exposess to access to NHibernate Dao classes
    /// </summary>
    public class NHibernateDaoFactory : IDaoFactory
    {       
        public IItemDao GetItemDao()
        {
            return new ItemDao();
        }

        public IOrdreDao GetOrderDao()
        {
            return new OrderDao();
        }

        public IApprovedOrderDao GetApprovedOrderDao()
        {
            return new ApprovedOrderDao();
        }

        public IBookDao GetBookDao()
        {
            return new BookDao();
        }

        public IPublisherDao GetPublisherDao()
        {
            return new PublisherDao();
        }

        public IReaderCartSelectionDao GetReaderCartSelectionDao()
        {
            return new ReaderCartSelectionDao();
        }

        public IReaderDao GetReaderDao()
        {
            return new ReaderDao();
        }

        public IReadingCartDao GetReadingCartDao()
        {
            return new ReadingCartDao();
        }

        public IBookToAuthorDao GetBookToAuthorDao()
        {
            return new BookToAuthorDao();
        }

        public IAuthorDao GetAuthorDao()
        {
            return new AuthorDao();
        }

        public IInventoryItemDao GetInventoryItemDao()
        {
            return new InventoryItemDao();
        }
    }

    public class ItemDao : AbstractNHibernateDao<Item, long>, IItemDao
    { }
   
    public class BookDao : AbstractNHibernateDao<Book, long>, IBookDao
    { }

    public class PublisherDao : AbstractNHibernateDao<Publisher, long>, IPublisherDao
    { }

    public class ReaderDao : AbstractNHibernateDao<Reader, long>, IReaderDao
    { }

    public class InventoryItemDao : AbstractNHibernateDao<InventoryItem, long>, IInventoryItemDao
    { }

    public class AuthorDao : AbstractNHibernateDao<Author, long>, IAuthorDao 
    {
        public void InsertToAuthors(IList<Author> authors)
        {
            Check.Require(authors != null, "Authors collection must be provided");
            foreach (var author in authors)
            {
                this.Save(author);
            }
        }
    }
}

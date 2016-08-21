using System;
using System.Collections.Generic;
using Enterprise.CoreData.Domain;
using Enterprise.CoreData.DataInterfaces;
using System.Linq.Expressions;
using Enterprise.NUnitTests.Data.TestFactories;


namespace Enterprise.NUnitTests.Data.DaoTestDoubles
{
    /// <summary>
    /// Stub DAO that can be used in place of OrderDao to simulate communications with DB without actually talking to the Db.
    /// </summary>
    public class BookDaoStub:IBookDao
    {
        public IList<Book> Get(Expression<Func<Book, bool>> filter)
        {
            return new TestBooksFactory().CreateBooks();
        }

        #region Not-Implemented Dao methods
        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Book, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public IList<Book> Find(Action<NHibernate.IQueryOver<Book, Book>> builder)
        {
            throw new NotImplementedException();
        }

        public IList<Book> Get()
        {
            throw new NotImplementedException();
        }

        public Book GetBy(Expression<Func<Book, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Book Save(Book entity)
        {
            throw new NotImplementedException();
        }

        public Book SaveOrUpdate(Book entity)
        {
            throw new NotImplementedException();
        }
        #endregion


        public IList<Book> CreateFullTextQuery(string queryString)
        {
            throw new NotImplementedException();
        }


        public IList<Book> Get(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }


        public IList<Book> Find(Action<NHibernate.IQueryOver<Book, Book>> builder, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IList<Book> Get(int pageNumber, int pageSize, Expression<Func<Book, object>> filter)
        {
            throw new NotImplementedException();
        }

        void ProjectBase.Data.IDao<Book, long>.CommitChanges()
        {
            throw new NotImplementedException();
        }

        void ProjectBase.Data.IDao<Book, long>.Delete(Expression<Func<Book, bool>> filter)
        {
            throw new NotImplementedException();
        }

        void ProjectBase.Data.IDao<Book, long>.Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Find(Action<NHibernate.IQueryOver<Book, Book>> builder, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Find(Action<NHibernate.IQueryOver<Book, Book>> builder)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Get(int pageNumber, int pageSize, Expression<Func<Book, object>> filter)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Get(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Get(Expression<Func<Book, bool>> filter)
        {
            throw new NotImplementedException();
        }

        IList<Book> ProjectBase.Data.IDao<Book, long>.Get()
        {
            throw new NotImplementedException();
        }

        Book ProjectBase.Data.IDao<Book, long>.GetBy(Expression<Func<Book, bool>> filter)
        {
            throw new NotImplementedException();
        }

        Book ProjectBase.Data.IDao<Book, long>.Save(Book entity)
        {
            throw new NotImplementedException();
        }

        Book ProjectBase.Data.IDao<Book, long>.SaveOrUpdate(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}

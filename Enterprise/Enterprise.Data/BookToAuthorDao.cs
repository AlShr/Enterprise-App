using System.Collections.Generic;
using Ninject;
using Enterprise.CoreData.LuceneIndex;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.Data
{
    public class BookToAuthorDao : AbstractNHibernateDao<BookToAuthor, long>, IBookToAuthorDao
    {

        public IList<BookToAuthor> GetRelationsInSearchIndexes(List<long> booktoauthorIndexes)
        {
            Check.Require(booktoauthorIndexes != null, "BookToAuthorIndexes must be provided");

            IList<BookToAuthor> relations = new List<BookToAuthor>();
            foreach (var index in booktoauthorIndexes)
            {
                BookToAuthor booktoauthor = this.GetBy(x => x.ID == index);
                if (booktoauthor != null)
                {
                    relations.Add(booktoauthor);
                }
            }
            return relations;
        }

        private void UpdatePublisherInRelation(Publisher publisher)
        {
            this.PublisherDao.SaveOrUpdate(publisher);
        }

        private void UpdateBookInRelation(Book book, Publisher publisher)
        {
            book.Publisher = publisher;
            this.BookDao.SaveOrUpdate(book);
        }

        private void UpdateAuthorInRelation(List<BookToAuthor> bookToAuthors)
        {
            foreach (var relation in bookToAuthors)
            {
                this.AuthorDao.SaveOrUpdate(relation.Author);
                this.SaveOrUpdate(relation);
                LuceneIndexer.Instance.ClearLuceneIndexRecord(relation.ID);
                LuceneIndexer.Instance.Add(relation);
            }
        }

        public void UpdateRelations(List<BookToAuthor> bookToAuthors)
        {
            var publisher = bookToAuthors[0].Book.Publisher;
            UpdatePublisherInRelation(publisher);
            var book = bookToAuthors[0].Book;
            UpdateBookInRelation(book, publisher);
            UpdateAuthorInRelation(bookToAuthors);
        }

        public IList<BookToAuthor> GetRelation(int pageNumber, int pageSize)
        {
            IList<BookToAuthor> bookToauthors = this.Find(x => x.JoinQueryOver(y => y.Book)
                .OrderBy(y => y.Description), pageNumber, pageSize);
            return bookToauthors;
        }

        public IList<BookToAuthor> GetRelationBooks(int pageNumber, int pageSize)
        {
            IList<Book> books = this.BookDao.Get(pageNumber, pageSize, x => x.Description);
            List<BookToAuthor> bookToauthors = new List<BookToAuthor>();
            foreach (var book in books)
            {
                bookToauthors.AddRange(book.Authors);
            }
            return bookToauthors;
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
            set 
            {
                bookDao = value;
            }

        }

        public virtual IAuthorDao AuthorDao
        {
            get
            {
                if (authorDao == null)
                {
                    authorDao = DaoLocatorNinject.AppKernel.Get<AuthorDao>();
                }
                return authorDao;
            }
        }

        public virtual IPublisherDao PublisherDao
        {
            get
            {
                if (publisherDao == null)
                {
                    publisherDao = DaoLocatorNinject.AppKernel.Get<PublisherDao>();
                }
                return publisherDao;
            }

        }

        private IBookDao bookDao;
        private IAuthorDao authorDao;
        private IPublisherDao publisherDao;
    }
}

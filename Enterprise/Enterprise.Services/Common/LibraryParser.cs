using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Overspesification.Services;
using Enterprise.Model;
using ProjectBase.Utils;

namespace Enterprise.Services.Common
{
    public class LibraryDataParser : ILibraryDataParser
    {
        public IDictionary<BookModel, List<AuthorModel>> BookAuthorsParse(IEnumerable<BookToAuthorModel> bookToauthors)
        {
            if (bookToauthors != null)
            {
                bookcatalog = new Dictionary<BookModel, List<AuthorModel>>();
                foreach (var relation in bookToauthors)
                {
                    BookModel book = relation.Book;
                    AuthorModel author = relation.Author;
                    author.PosAuthList = relation.PosAuthorList;
                    if (book != null)
                    {
                        if (!bookcatalog.ContainsKey(book))
                        {
                            List<AuthorModel> authors = new List<AuthorModel>();
                            bookcatalog.Add(book, authors);
                        }
                        bookcatalog[book].Add(author);
                    }
                }
            }            
            return bookcatalog;
        }

        public IDictionary<PublisherModel, List<BookModel>> PublisherBooksParse(IEnumerable<BookToAuthorModel> bookToauthors)
        {
            if (bookToauthors != null)
            {
                publishercatalog = new Dictionary<PublisherModel, List<BookModel>>();
                foreach (var relation in bookToauthors)
                {
                    BookModel book = relation.Book;
                    PublisherModel publisher = book.Publisher;
                    if (publisher != null)
                    {
                        List<BookModel> books;
                        if (!publishercatalog.ContainsKey(publisher))
                        {
                            books = new List<BookModel>();
                            publishercatalog.Add(publisher, books);
                        }
                        publishercatalog[publisher].Sort();
                        if (publishercatalog[publisher].BinarySearch(book) < 0)
                        {
                            publishercatalog[publisher].Add(book);
                        }
                    }
                }
            }
            return publishercatalog;
        }

        public IList<PublisherModel> PublishesrParse(
            List<PublisherModel> publishers,
            IDictionary<PublisherModel, List<BookModel>> publishercatalog,
            IDictionary<BookModel, List<AuthorModel>> bookcatalog)
        {
            publisherModel = new List<PublisherModel>();
            if (bookcatalog != null && publishercatalog != null)
            {
                publisherModel = InitPublisherCatalog(publisherModel, publishercatalog, bookcatalog);

            }
            return publisherModel;
        }

        public List<AuthorModel> AuthorParse(IEnumerable<BookToAuthorModel> bookToauthors)
        {
            authorLookUp = new List<AuthorModel>();
            bookToauthors.OrderBy(x => x.Author);
            foreach (var relation in bookToauthors)
            {
                AuthorModel author = relation.Author;
                if (!authorLookUp.Contains(author))
                {
                    authorLookUp.Add(author);
                }
            }
            return authorLookUp;
        }


        public void LibraryParser(IEnumerable<BookToAuthorModel> bookToAuthors)
        {
            bookcatalog = BookAuthorsParse(bookToAuthors);
            publishercatalog = PublisherBooksParse(bookToAuthors);
            publisherModel = new List<PublisherModel>();
            if (bookcatalog != null && publishercatalog != null)
            {
                PublishesrParse(publisherModel, publishercatalog, bookcatalog);
            }
            authorLookUp = AuthorParse(bookToAuthors);

        }

        public List<BookModel> BookParse(List<BookModel> books, IDictionary<BookModel, List<AuthorModel>> bookcatalog)
        {
            Check.Require(books != null, "books must be provided");
            Check.Require(bookcatalog != null, "bookcatalog must be provided");
            foreach (var book in books)
            {
                book.Authors = bookcatalog[book];
            }
            return books;
        }

        private List<PublisherModel> InitPublisherCatalog(List<PublisherModel> publishers, IDictionary<PublisherModel,
            List<BookModel>> publishercatalog,
            IDictionary<BookModel, List<AuthorModel>> bookcatalog)
        {
            Check.Require(publishers != null, "publishers must be provided");
            Check.Require(bookcatalog != null, "bookcatalog must be provided");
            Check.Require(publishercatalog != null, "publishercatalog must be provided");
            foreach (var publisher in publishercatalog.Keys)
            {
                List<BookModel> pubbooks = publishercatalog[publisher];
                bookLookUp = BookParse(pubbooks, bookcatalog);
                publisher.Books.AddRange(bookLookUp);
                publishers.Add(publisher);
            }
            return publishers;
        }

        public List<Model.PublisherModel> PublisherModel
        {
            get { return publisherModel; }
            set { publisherModel = value; }
        }
        public List<Model.AuthorModel> AuthorLookUp
        {
            get { return authorLookUp; }
            set { authorLookUp = value; }
        }

        public List<Model.PublisherModel> PublisherLookUp
        {
            get { return publisherLookUp; }
            set { publisherLookUp = value; }
        }

        public List<Model.BookModel> BookLookUp
        {
            get { return bookLookUp; }
            set { bookLookUp = value; }
        }

        public IDictionary<PublisherModel, List<BookModel>> PublisherCatalog
        {
            get { return publishercatalog; }
            set { publishercatalog = value; }
        }

        public IDictionary<BookModel, List<AuthorModel>> BookCatalog
        {
            get { return bookcatalog; }
            set { bookcatalog = value; }
        }

        private List<Model.AuthorModel> authorLookUp;
        private List<Model.BookModel> bookLookUp;
        private List<Model.PublisherModel> publisherLookUp;
        private List<Model.PublisherModel> publisherModel;
        private IDictionary<BookModel, List<AuthorModel>> bookcatalog;
        private IDictionary<PublisherModel, List<BookModel>> publishercatalog = new Dictionary<PublisherModel, List<BookModel>>();

    }
}

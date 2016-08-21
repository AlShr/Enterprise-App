using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services;
using System.Data.OracleClient;
using System.ServiceModel;
using Enterprise.CoreData.Converters;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Domain;
using Enterprise.CoreData.Dto;
using Enterprise.CoreData.LuceneIndex;
using System.Web.Services.Protocols;
using ProjectBase.Utils;
using ProjectBase.Data;



namespace Enterprise.Web
{
    /// <summary>
    /// Summary description for LibraryReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LibraryReport :BaseWebService
    {
        [WebMethod]
        public void CreateLuceneIndexDocument()
        {
            var directory = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Lucene.dir");
            if (!System.IO.Directory.Exists(directory))
            {
                IBookToAuthorDao bookToAuthorDao = DaoFactory.GetBookToAuthorDao();
                var bookToauthors = bookToAuthorDao.Get();
                foreach (var relation in bookToauthors)
                {
                    LuceneIndexer.Instance.Add(relation);
                }
            }           
        }

        #region Provide Common DaoBisnessLogic  

        [WebMethod]
        public PublisherDto GetPublisherBy(long publisherId)
        {
            try 
            {
                IPublisherDao publisherDao = DaoFactory.GetPublisherDao();
                PublisherDto publisher = ConverterFactory.GetConverter<Publisher, PublisherDto>().ToProxyItem(
                    publisherDao.GetBy(x => x.ID == publisherId));
                return publisher;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }          
        }

        
        [WebMethod]
        public List<PublisherDto> GetPublishers()
        {
            try
            {
                IPublisherDao publisherDao = DaoFactory.GetPublisherDao();
                var publishersDb = publisherDao.Get();
                List<PublisherDto> publishers = publishersDb.Select<Publisher, PublisherDto>(
                    x => ConverterFactory.GetConverter<Publisher, PublisherDto>().ToProxyItem(x, true)).ToList();
                return publishers;
            }          
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }      
        }

        [WebMethod]
        public ReaderDto GetReader(string emailAddress)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                ReaderDto reader = ConverterFactory.GetConverter<Reader, ReaderDto>().ToProxyItem(
                    readerDao.GetBy(x => x.EmailIdentity.EmailAddress == emailAddress), true);
                return reader;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }
            
        }

       
        [WebMethod]
        public List<BookDto> GetBooks()
        {
            try 
            {
                IBookDao bookDao = DaoFactory.GetBookDao();
                List<BookDto> books = bookDao.Get().Select<Book, BookDto>(
                    x => ConverterFactory.GetConverter<Book, BookDto>().ToProxyItem(x, true)).ToList();
                return books;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }           
        }

        [WebMethod]
        public List<AuthorDto> GetAuthors()
        {
            try 
            {
                IAuthorDao authorDao = DaoFactory.GetAuthorDao();
                IList<Author> authorsDb = authorDao.Get();
                List<AuthorDto> authors = authorsDb.Select<Author, AuthorDto>(
                    x => ConverterFactory.GetConverter<Author, AuthorDto>().ToProxyItem(x)).ToList();
                return authors;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }               

        }

        [WebMethod]
        public List<ReaderDto> GetReaders()
        {
            try 
            {
                IReaderDao readerdao = DaoFactory.GetReaderDao();
                IList<Reader> readerDbs = readerdao.Get();
                List<ReaderDto> readers = readerDbs.Select<Reader, ReaderDto>(
                    x => ConverterFactory.GetConverter<Reader, ReaderDto>().ToProxyItem(x, true)).ToList();
                return readers;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                     
        }

        [WebMethod]
        public bool UpdatePublisher(PublisherDto publisher)
        {
            try 
            {
                IPublisherDao publisherDao = DaoFactory.GetPublisherDao();
                Publisher publisherDb = ConverterFactory.GetConverter<Publisher, PublisherDto>().ToRepoItem(publisher);
                publisherDao.SaveOrUpdate(publisherDb);
                return true;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                return false;
                throw se;               
            }          
        }

        [WebMethod]
        public bool UpdateAuthor(AuthorDto author)
        {
            try 
            {
                IAuthorDao authorDao = DaoFactory.GetAuthorDao();
                Author authorDb = ConverterFactory.GetConverter<Author, AuthorDto>().ToRepoItem(author);
                authorDao.SaveOrUpdate(authorDb);
                return true;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                return false;
                throw se;
            }                   
        }

        [WebMethod]
        public List<BookToAuthorDto> GetCatalogGrafbyBookId(long bookId)
        {
            try 
            {
                IBookToAuthorDao bookToauthordao = DaoFactory.GetBookToAuthorDao();
                IList<BookToAuthor> bookToauthorDb = bookToauthordao.Get(x => x.Book.ID == bookId);
                List<BookToAuthorDto> catalogGraf = bookToauthorDb.Select<BookToAuthor, BookToAuthorDto>(
                    x => ConverterFactory.GetConverter<BookToAuthor, BookToAuthorDto>().ToProxyItem(x, true)).ToList();
                return catalogGraf;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }              
        }

        [WebMethod]
        public ItemDto[] GetItems(long readerId)
        {
            try 
            {
                IItemDao itemDao = DaoFactory.GetItemDao();
                IList<Item> itemsDb = itemDao.Get(x => x.ApprovedOrder.ID == null);
                ItemDto[] items = itemsDb.Select<Item, ItemDto>(
                     x => ConverterFactory.GetConverter<Item, ItemDto>().ToProxyItem(x, true)).ToArray();
                return items;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                  
        }

        [WebMethod]
        public ReaderCartSelectionDto[] GetReaderCartSelections()
        {
            try 
            {
                IReaderCartSelectionDao readercartselectDao = DaoFactory.GetReaderCartSelectionDao();
                IList<ReaderCartSelection> readercartDb = readercartselectDao.Get();
                ReaderCartSelectionDto[] readercartselectsDto = readercartDb
                    .Select<ReaderCartSelection, ReaderCartSelectionDto>(
                    x => ConverterFactory.GetConverter<ReaderCartSelection, ReaderCartSelectionDto>().ToProxyItem(x)).ToArray();
                return readercartselectsDto;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }             
        }

        [WebMethod]
        public List<ItemDto> GetItemsByBooks(List<long> bookIds, long readerId)
        {
            try 
            {
                IReadingCartDao readingCartDao = DaoFactory.GetReadingCartDao();
                ReadingCart cart = readingCartDao.GetBy(x => x.CartOfReader.ID == readerId);
                IItemDao itemDao = DaoFactory.GetItemDao();
                List<Item> itemsDb = new List<Item>();
                bookIds.ForEach(delegate(long id)
                {
                    var item = itemDao.Get(x => x.ItemDescription.ID == id && x.ApprovedOrder.ID == null);
                    itemsDb.AddRange(item);
                });
                List<ItemDto> items = itemsDb.Select<Item, ItemDto>(
                    x => ConverterFactory.GetConverter<Item, ItemDto>().ToProxyItem(x, true)).ToList();
                return items;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }         
          
        }


        #endregion

        #region Provide Catalog Service
        [WebMethod]
        public List<BookToAuthorDto> GetRelationBookToAuthor()
        {
            try 
            {
                IBookToAuthorDao bookToAuthorDao = DaoFactory.GetBookToAuthorDao();
                IList<BookToAuthor> bookToAuthorsDb = bookToAuthorDao.Get();
                List<BookToAuthorDto> bookToauthors = bookToAuthorsDb.Select<BookToAuthor, BookToAuthorDto>(
                    x => ConverterFactory.GetConverter<BookToAuthor, BookToAuthorDto>().ToProxyItem(x, true)).ToList();
                return bookToauthors;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                       
        }

        [WebMethod]
        public List<BookToAuthorDto> GetRelationBookToAuthorPagination(int pageNumber, int pageSize = 200)
        {
            try
            {
                IBookToAuthorDao bookToAuthorDao = DaoFactory.GetBookToAuthorDao();
                IList<BookToAuthor> bookToAuthorsDb = bookToAuthorDao.GetRelationBooks(pageNumber, pageSize);
                List<BookToAuthorDto> bookToAuthors = bookToAuthorsDb.Select<BookToAuthor, BookToAuthorDto>(
                    x => ConverterFactory.GetConverter<BookToAuthor, BookToAuthorDto>().ToProxyItem(x, true)).ToList();
                return bookToAuthors;
            }
            catch (Exception ex)
            {

                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }
        }

        [WebMethod]
        public List<AuthorDto> GetAuthorCatalogPagination(int pageNumber, int pageSize = 200)
        {
            try
            {
                IAuthorDao authorDao = DaoFactory.GetAuthorDao();
                IList<Author> authorsDb = authorDao.Get(pageNumber, pageSize).OrderBy(x => x.LastName).ToList();
                List<AuthorDto> authors = authorsDb.Select<Author, AuthorDto>(
                    x => ConverterFactory.GetConverter<Author, AuthorDto>().ToProxyItem(x)).ToList();
                return authors;

            }
            catch (Exception ex)
            {
                
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw (se);
            }
        }

        [WebMethod]
        public List<PublisherDto> GetPublisehrCatalogPagination(int pageNumber, int pageSize = 200)
        {
            try 
            {
                IPublisherDao publisherDao = DaoFactory.GetPublisherDao();
                IList<Publisher> publishersDb = publisherDao.Get(pageNumber, pageSize).OrderBy(x => x.Title).ToList();
                List<PublisherDto> publishers = publishersDb.Select<Publisher, PublisherDto>(
                    x => ConverterFactory.GetConverter<Publisher, PublisherDto>().ToProxyItem(x)).ToList();
                return publishers;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw (se);
            }
        }

        [WebMethod]
        public long[] GetIndexesByLucene(string filter)
        {
            try
            {
                IEnumerable<long> bookToauthorIndexes = LuceneIndexer.Instance.Search(filter, new string[] { "Book", "Author", "Publisher" });
                return bookToauthorIndexes.ToArray();
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }              
        }
       
        [WebMethod]
        public List<BookToAuthorDto> GetRelationBookToAuthorBySearch(List<long> bookToauthorIndexes)
        {
            try 
            {
                IBookToAuthorDao bookToauthorDao = DaoFactory.GetBookToAuthorDao();
                IList<BookToAuthor> bookToAuthorsDb = bookToauthorDao.GetRelationsInSearchIndexes(bookToauthorIndexes);
                List<BookToAuthorDto> bookToAuthors = bookToAuthorsDb.Select<BookToAuthor, BookToAuthorDto>(
                    x => ConverterFactory.GetConverter<BookToAuthor, BookToAuthorDto>().ToProxyItem(x, true)).ToList();
                return bookToAuthors;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }          
        }
        #endregion

        #region Provide ManagerStrorage

        [WebMethod]
        public void UpdateRelationBookToAuthor(List<BookToAuthorDto> relations)
        {
            try 
            {
                IBookToAuthorDao bookToAuthorDao = DaoFactory.GetBookToAuthorDao();
                List<BookToAuthor> bookToAuthors = relations.Select<BookToAuthorDto, BookToAuthor>(
                    x => ConverterFactory.GetConverter<BookToAuthor, BookToAuthorDto>().ToRepoItem(x, true)).ToList();
                bookToAuthorDao.UpdateRelations(bookToAuthors);
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }          
          
        }
        #endregion

        [WebMethod]
        public List<ApprovedOrderDto> GetApprovedOrdersByReader(long readerId)
        {
            try 
            {
                IApprovedOrderDao approvedorderDao = DaoFactory.GetApprovedOrderDao();
                IList<ApprovedOrder> approvedorders = approvedorderDao.Get(x => x.ApprovedByReader.ID == readerId);
                List<ApprovedOrderDto> approvedorderDtos = approvedorders.Select<ApprovedOrder, ApprovedOrderDto>(
                    x => ConverterFactory.GetConverter<ApprovedOrder, ApprovedOrderDto>().ToProxyItem(x, true)).ToList();
                return approvedorderDtos;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                    
        }

        #region Reader Cart service

        [WebMethod]
        public List<long> SetReaderCartSelections(long readerId, List<long> bookIds)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                Reader reader = readerDao.GetBy(x => x.ID == readerId);
                IReadingCartDao readerCartDao = DaoFactory.GetReadingCartDao();
                List<long> readcartedId = readerCartDao.SetReaderCartSelections(reader, bookIds);
                return readcartedId;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }           
           
        }

        [WebMethod]
        public void SetReaderCartSelection(long readerId, long bookId)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                Reader reader = readerDao.GetBy(x => x.ID == readerId);
                IReadingCartDao readerCartDao = DaoFactory.GetReadingCartDao();
                readerCartDao.SetReaderCartSelection(reader, bookId);
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                     
        }

        [WebMethod]
        public void RemoveReaderCartSelection(long readerId, long bookId ,long readercartselectionId)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                Reader reader = readerDao.GetBy(x => x.ID == readerId);
                IReadingCartDao readerCartDao = DaoFactory.GetReadingCartDao();
                readerCartDao.RemoveReaderCartSelection(reader, bookId, readercartselectionId);
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }            
        }

        [WebMethod]
        public ReaderDto GetReaderBy(long readerId)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                Reader reader = readerDao.GetBy(x => x.ID == readerId);
                ReaderDto readerDto = ConverterFactory.GetConverter<Reader, ReaderDto>().ToProxyItem(reader);
                return readerDto;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }               
        }

        [WebMethod]
        public ReadingCartDto GetReaderCartSelectionByOrder(long readerId)
        {
            try 
            {
                IReaderDao readerDao = DaoFactory.GetReaderDao();
                Reader reader = readerDao.GetBy(x => x.ID == readerId);
                IReadingCartDao readerCartDao = DaoFactory.GetReadingCartDao();
                ReadingCart cart = readerCartDao.GetBy(x => x.CartOfReader.ID == reader.ID);
                ReadingCartDto cartDto = ConverterFactory.GetConverter<ReadingCart, ReadingCartDto>().ToProxyItem(cart, true);
                return cartDto;
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }             
        }

        #endregion

        [WebMethod]
        public void SavePublisher(string title)
        {
            try 
            {
                IPublisherDao publisherDao = DaoFactory.GetPublisherDao();
                Publisher publisher = new Publisher() { Title = title };
                publisherDao.SaveOrUpdate(publisher);
                publisherDao.CommitChanges();
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                
        }

        [WebMethod]
        public void OrderedReadingCartBy(long readerId)
        {
            try 
            {
                IReadingCartDao readingcartDao = DaoFactory.GetReadingCartDao();
                ReadingCart cart = readingcartDao.GetBy(x => x.CartOfReader.ID == readerId);
                IOrdreDao orderDao = DaoFactory.GetOrderDao();
                Order order = orderDao.MakeOrder(cart);
                IApprovedOrderDao approvedOrderDao = DaoFactory.GetApprovedOrderDao();
                approvedOrderDao.MakeApprovedOrder(order);
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                
        }

        [WebMethod]
        public void SetOrderItemRecoveryDate(long bookId, long approvedOrderId, DateTime date)
        {
            try 
            {
                IApprovedOrderDao approvedOrderDao = DaoFactory.GetApprovedOrderDao();
                IBookDao bookDao = DaoFactory.GetBookDao();
                var book = bookDao.GetBy(x => x.ID == bookId);
                var approvedOrder = approvedOrderDao.GetBy(x => x.ID == approvedOrderId);
                approvedOrderDao.RecoveringApprovedOrderItem(book, approvedOrder, date);
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }                
        }
    }
}

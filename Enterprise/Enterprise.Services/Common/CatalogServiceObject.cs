using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.CoreData.Domain;
using Enterprise.CoreData.LuceneIndex;
using Enterprise.Model;
using Enterprise.Services.Converters;
using Enterprise.Services.Providers;
using Enterprise.Services.WebServices;
using Enterprise.Overspesification.Services;
using ProjectBase.Data;
using ProjectBase.Utils;
using ProjectBase.ErrorHandle;
using Enterprise.Services.ServiceLocator;
using Ninject;


namespace Enterprise.Services.Common
{
    public class CatalogServiceObject : ICatalogServiceObject
    {
        public List<ReaderModel> GetCatalogReaders()
        {
            IList<ReaderDto> readerdtos = ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetReadersAsync(),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            List<ReaderModel> readers = readerdtos.Select<ReaderDto, ReaderModel>(
                x => ConverterFactory.GetConverter<ReaderModel, ReaderDto>().ToRepoItem(x, true)).ToList();
            return readers;
        }

        public async Task<List<BookModel>> GetCatalogBooks()
        {
            IList<BookDto> bookdtos = await ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetBooks(),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            List<BookModel> books = bookdtos.Select<BookDto, BookModel>(
                x => ConverterFactory.GetConverter<BookModel, BookDto>().ToRepoItem(x)).ToList();
            return books;
        }

        public async Task<List<AuthorModel>> GetCatalogAuthors()
        {
            IList<AuthorDto> authordtos = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetAuthors(),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            List<AuthorModel> authors = authordtos.Select<AuthorDto, AuthorModel>(
                x => ConverterFactory.GetConverter<AuthorModel, AuthorDto>().ToRepoItem(x)).ToList();
            return authors;
        }

        public async Task<List<PublisherModel>> GetCatalogPublishers()
        {
            IList<PublisherDto> publisherdtos = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetPublishers(),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            List<PublisherModel> publishers = publisherdtos.Select<PublisherDto, PublisherModel>(
                x => ConverterFactory.GetConverter<PublisherModel, PublisherDto>().ToRepoItem(x)).ToList();
            return publishers;
        }

        public async Task<List<ItemModel>> GetItems(long readerId)
        {
            IList<ItemDto> itemdtos = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetItems(readerId),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));

            List<ItemModel> items = itemdtos.Select<ItemDto, ItemModel>(
                x => ConverterFactory.GetConverter<ItemModel, ItemDto>().ToRepoItem(x, true)).ToList();
            return items;
        }

        public async Task<List<ItemModel>> GetItems(long[] bookIds, long readerId)
        {
            IList<ItemDto> itemdtos = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetItems(bookIds, readerId),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            List<ItemModel> items = itemdtos.Select<ItemDto, ItemModel>(
                x => ConverterFactory.GetConverter<ItemModel, ItemDto>().ToRepoItem(x, true)).ToList();
            return items;
        }

        public async Task<PublisherModel> GetPublisherBy(long publisherId)
        {
            PublisherDto publisherdto = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetPublisherBy(publisherId),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            PublisherModel publisher = ConverterFactory.GetConverter<PublisherModel, PublisherDto>().ToRepoItem(publisherdto);
            return publisher;
        }    

        /// <summary>
        /// Get full Catalog Data
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<BookToAuthorModel>> GetRelationsBookToAuthor()
        {
            IList<BookToAuthorDto> relationdtos = await ExceptionManager.Process(
               ()=> ObjectServiceProvider.DoGetRelations(),
               ExceptionManager.IsFatal,
               ex=>Logger.Instance.Error(ex));
            List<BookToAuthorModel> bookToAuhor = relationdtos.Select<BookToAuthorDto, BookToAuthorModel>(
                x => ConverterFactory.GetConverter<BookToAuthorModel, BookToAuthorDto>().ToRepoItem(x, true)).ToList();
            return bookToAuhor;
        }

        /// <summary>
        /// Get Catalog Data provided by Lucene Search Index
        /// </summary>
        /// <param name="searchmodel"></param>
        /// <returns></returns>
        public async Task<List<BookToAuthorModel>> GetRelationsBookToAuthor(SearchModel searchmodel)
        {
            Check.Require(searchmodel != null, "SearhModel must be provided");
            long[] bookToauthorIndexes = await GetSearchIndexes(searchmodel);
            IList<BookToAuthorDto> relationdtos = await ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetRelations(bookToauthorIndexes),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            List<BookToAuthorModel> bookToAuthor = relationdtos.Select<BookToAuthorDto, BookToAuthorModel>(
                x => ConverterFactory.GetConverter<BookToAuthorModel, BookToAuthorDto>().ToRepoItem(x, true)).ToList();
            return bookToAuthor;
        }

        /// <summary>
        /// Get Graf catalog cheked rowobject by Lucene Index
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<List<BookToAuthorModel>> GetRelationsBookToAuthor(long bookId)
        {
            IList<BookToAuthorDto> relationsdtos = await ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetRelationsBookToAuthor(bookId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            List<BookToAuthorModel> bookToAuthor = relationsdtos.Select<BookToAuthorDto, BookToAuthorModel>(
                x => ConverterFactory.GetConverter<BookToAuthorModel, BookToAuthorDto>().ToRepoItem(x, true)).ToList();
            return bookToAuthor;
        }


        public List<BookToAuthorModel> GetRelationsBookToAuthor(int pageNumber, int pageSize)
        {
            IList<BookToAuthorDto> relationsdtos = ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetRelationsBookToAuthorPaginationSync(pageNumber, pageSize),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            List<BookToAuthorModel> bookToAuthor = relationsdtos.Select<BookToAuthorDto, BookToAuthorModel>(
                x => ConverterFactory.GetConverter<BookToAuthorModel, BookToAuthorDto>().ToRepoItem(x, true)).ToList();
            return bookToAuthor;
        }

        public async Task<List<BookToAuthorModel>> AddToBookToAthorModel(List<BookToAuthorModel> bookToAuthor, BookModel book)
        {
            Check.Require(bookToAuthor != null, "BookToAuthorModel maust be provided");
            Check.Require(book != null, "BookModel must be provided");
            IList<BookToAuthorModel> temp = await ExceptionManager.Process(
                () => GetBookToAuthorModel(book),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            if (temp != null)
            {
                bookToAuthor.AddRange(temp);
            }
            return bookToAuthor;
        }

        public async Task<long[]> GetSearchIndexes(SearchModel searchmodel)
        {
            long[] bookToauthorsIndexes = await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoGetRelationsBookToAuthor(searchmodel.SearchFilter),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
            return bookToauthorsIndexes;
        }

        public async Task<List<BookToAuthorModel>> GetRelationByBook(long bookId)
        {
            List<BookToAuthorModel> relations = await ExceptionManager.Process(
                () => GetRelationsBookToAuthor(bookId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            return relations;
        }

        /// <summary>
        /// Set Relations For new Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private List<BookToAuthorModel> SetBookToAuthorModel(BookModel book)
        {
            Check.Require(book != null, "BookModel must be provided");
            List<BookToAuthorModel> relations = new List<BookToAuthorModel>();
            for (int i = 0; i < book.Authors.Count; i++)
            {
                relations.Add(new BookToAuthorModel());
                relations[i].Author = book.Authors[i];
                relations[i].Book = book;
                relations[i].Book.Publisher = book.Publisher;
            }
            return relations;
        }

        /// <summary>
        /// Get Relations For exists Book and Update
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private async Task<List<BookToAuthorModel>> GetBookToAuthorModel(BookModel book)
        {
            Check.Require(book != null, "BookModel must be provided");
            List<BookToAuthorModel> relations = await ExceptionManager.Process(
                () => GetRelationByBook(book.ID),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            for (int i = 0; i < book.Authors.Count; i++)
            {
                relations[i].Book = book;
                relations[i].Book.Publisher = book.Publisher;
                relations[book.Authors[i].PosAuthList].Author = book.Authors[i];
            }
            return relations;
        }

        private async void SetBookToAuthorRelations(IList<BookToAuthorModel> relations)
        {
            Check.Require(relations != null, "Collection must be provided ");

            BookToAuthorDto[] bookToauthorDtos = relations.Select<BookToAuthorModel, BookToAuthorDto>(
                x => ConverterFactory.GetConverter<BookToAuthorModel, BookToAuthorDto>().ToProxyItem(x, true)).ToArray();
            await ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetUpdateRelationsBookToAuthor(bookToauthorDtos),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task SetBookToAuthorRelations(BookModel book)
        {
            Check.Require(book != null, "Book must be provided");

            IList<BookToAuthorModel> relations = await ExceptionManager.Process(
                () => GetBookToAuthorModel(book),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            if (book.ID == default(long))
            {
                relations = SetBookToAuthorModel(book);
            }
            SetBookToAuthorRelations(relations);
        }

        public void CatalogData(IList<BookToAuthorModel> bookToauthor)
        {
            LibraryDataParser.LibraryParser(bookToauthor);
        }

        #region Update
        public async Task<bool> UpdatePublisher(PublisherModel publisher)
        {
            PublisherDto publisherdto = ConverterFactory.GetConverter<PublisherModel, PublisherDto>().ToProxyItem(publisher);
            return await ExceptionManager.Process(
                () => ObjectServiceProvider.DoUpdatePublisher(publisherdto),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task<bool> UpdateAuthor(AuthorModel author)
        {
            AuthorDto authordto = ConverterFactory.GetConverter<AuthorModel, AuthorDto>().ToProxyItem(author);
            return await ExceptionManager.Process(
                ()=>ObjectServiceProvider.DoUpdateAuthor(authordto),
                ExceptionManager.IsFatal,
                ex=>Logger.Instance.Error(ex));
        }
        #endregion

        public ILibraryDataParser LibraryDataParser
        {
            get 
            {
                if(libraryDataParser == null)
                {
                    libraryDataParser = ServiceLocatorNinject.AppKernel.Get<LibraryDataParser>();
                }
                return libraryDataParser;
            }
            set 
            {
                libraryDataParser = value;
            }
        }

        private ILibraryDataParser libraryDataParser;
    }
}

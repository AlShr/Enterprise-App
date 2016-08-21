using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enterprise.Model;


namespace Enterprise.Overspesification.Services
{
    public interface ICatalogServiceObject
    {
        Task<List<BookToAuthorModel>> GetRelationsBookToAuthor();
        Task<List<BookToAuthorModel>> GetRelationsBookToAuthor(SearchModel searchmodel);
        List<BookToAuthorModel> GetRelationsBookToAuthor(int pageNumber, int pageSize);  
        Task<List<PublisherModel>> GetCatalogPublishers();
        Task<List<BookModel>> GetCatalogBooks();
        Task<List<AuthorModel>> GetCatalogAuthors();
        List<ReaderModel> GetCatalogReaders();
        Task<List<ItemModel>> GetItems(long readerId);
        Task<List<ItemModel>> GetItems(long[] bookIds, long readerId);
        Task<PublisherModel> GetPublisherBy(long publisherId);
        Task<bool> UpdatePublisher(PublisherModel publisher);
        Task<bool> UpdateAuthor(AuthorModel author);    
        Task SetBookToAuthorRelations(BookModel book);
        void CatalogData(IList<BookToAuthorModel> bookToauthor);
        ILibraryDataParser LibraryDataParser { get; set; }
    } 
}

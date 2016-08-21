using ProjectBase.Data;
using Enterprise.CoreData.Domain;
using System.Collections.Generic;

namespace Enterprise.CoreData.DataInterfaces
{
    public interface IDaoFactory
    {
        IOrdreDao GetOrderDao();
        IItemDao GetItemDao();
        IApprovedOrderDao GetApprovedOrderDao();
        IBookDao GetBookDao();
        IPublisherDao GetPublisherDao();
        IReaderCartSelectionDao GetReaderCartSelectionDao();
        IReaderDao GetReaderDao();
        IReadingCartDao GetReadingCartDao();
        IBookToAuthorDao GetBookToAuthorDao();
        IAuthorDao GetAuthorDao();
        IInventoryItemDao GetInventoryItemDao();

    }

    #region inline interface declaration    
    
    public interface IItemDao : IDao<Item, long> { } 
    public interface IBookDao : IDao<Book, long> { }
    public interface IPublisherDao : IDao<Publisher, long> { }
    public interface IReaderDao : IDao<Reader, long> { }
    public interface IInventoryItemDao : IDao<InventoryItem, long> { }
    public interface IReadingCartDao : IDao<ReadingCart, long>
    {
        List<long> SetReaderCartSelections(Reader reader, List<long> bookIds);
        void SetReaderCartSelection(Reader reader, long bookId);
        void SetReaderCartSelection(Reader reader, Book book);
        void RemoveReaderCartSelection(Reader reader, long bookId, long readercartselectionId);
    }

    public interface IBookToAuthorDao : IDao<BookToAuthor, long>
    {
        IList<BookToAuthor> GetRelationsInSearchIndexes(List<long> booktoauthorIndexes);
        IList<BookToAuthor> GetRelation(int pageNumber, int pageSize);
        void UpdateRelations(List<BookToAuthor> bookToAuthors);
        IList<BookToAuthor> GetRelationBooks(int pageNumber, int pageSize);

    }

    public interface IAuthorDao : IDao<Author, long> 
    {
        void InsertToAuthors(IList<Author> authors);
    }
    #endregion
}

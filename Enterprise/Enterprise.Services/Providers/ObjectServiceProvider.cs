using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Services.WebServices;
using System.Web.Services.Protocols;
using System.Net;
using System.ServiceModel;
using ProjectBase.ErrorHandle;
using ProjectBase.Data;

namespace Enterprise.Services.Providers
{
    public sealed class ObjectServiceProvider
    {
        public static ObjectServiceProvider Instance
        {
            get
            {
                return Nested.ObjectServiceProvider;
            }
        }

        private ObjectServiceProvider()
        { }

        private class Nested
        {
            internal static readonly ObjectServiceProvider ObjectServiceProvider
                = new ObjectServiceProvider();
            static Nested() { }
        }

        public async static Task<BookToAuthorDto[]> DoGetRelations()
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<BookToAuthorDto[]>.Factory.FromAsync(client.BeginGetRelationBookToAuthor, client.EndGetRelationBookToAuthor, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<BookToAuthorDto[]> DoGetRelations(long[] indexes)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<BookToAuthorDto[]>.Factory.FromAsync(client.BeginGetRelationBookToAuthorBySearch, client.EndGetRelationBookToAuthorBySearch, indexes, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<PublisherDto[]> DoGetPublishers()
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<PublisherDto[]>.Factory.FromAsync(client.BeginGetPublishers, client.EndGetPublishers, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<AuthorDto[]> DoGetAuthors()
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<AuthorDto[]>.Factory.FromAsync(client.BeginGetAuthors, client.EndGetAuthors, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<BookDto[]> DoGetBooks()
        {
            
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<BookDto[]>.Factory.FromAsync(client.BeginGetBooks, client.EndGetBooks, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<ReaderDto[]> DoGetReaders()
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<ReaderDto[]>.Factory.FromAsync(client.BeginGetReaders, client.EndGetReaders, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public static ReaderDto[] DoGetReadersAsync()
        {
            LibraryReport client;
            try 
            {
                client = new LibraryReport();
                var res = client.GetReaders();
                return res;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<ItemDto[]> DoGetItems(long readerId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<ItemDto[]>.Factory.FromAsync(client.BeginGetItems, client.EndGetItems, readerId, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<ItemDto[]> DoGetItems(long[] bookIds, long readerId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<ItemDto[]>.Factory.FromAsync(client.BeginGetItemsByBooks,
                    client.EndGetItemsByBooks, bookIds, readerId, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<PublisherDto> DoGetPublisherBy(long publisherId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<PublisherDto>.Factory.FromAsync(client.BeginGetPublisherBy, client.EndGetPublisherBy, publisherId, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<long[]> DoGetRelationsBookToAuthor(string filter)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<long[]>.Factory.FromAsync(client.BeginGetIndexesByLucene, client.EndGetIndexesByLucene, filter, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task DoGetUpdateRelationsBookToAuthor(BookToAuthorDto[] relations)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                await Task.Factory.FromAsync(client.BeginUpdateRelationBookToAuthor,
                    client.EndUpdateRelationBookToAuthor, relations, null);
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<BookToAuthorDto[]> DoGetRelationsBookToAuthorPagination(int pageNumber, int pageSize)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<BookToAuthorDto[]>.Factory.FromAsync(client.BeginGetRelationBookToAuthorPagination,
                    client.EndGetRelationBookToAuthorPagination, pageNumber, pageSize, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public static BookToAuthorDto[] DoGetRelationsBookToAuthorPaginationSync(int pageNumber, int pageSize)
        {
            LibraryReport client;
            try 
            {
                client = new LibraryReport();
                var res = client.GetRelationBookToAuthorPagination(pageNumber, pageSize);
                return res;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<BookToAuthorDto[]> DoGetRelationsBookToAuthor(long bookId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<BookToAuthorDto[]>.Factory.FromAsync(client.BeginGetCatalogGrafbyBookId, client.EndGetCatalogGrafbyBookId, bookId, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<long[]> DoSetReaderCartSelection(long readerId, long[] bookIds)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<long[]>.Factory.FromAsync(client.BeginSetReaderCartSelections, client.EndSetReaderCartSelections, readerId, bookIds, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task DoSetReaderCartSelection(long readerId, long bookId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                await Task.Factory.FromAsync(client.BeginSetReaderCartSelection, client.EndSetReaderCartSelection, readerId, bookId, null);
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task DoRemoveReaderCartSelection(long readerId, long bookId, long readercartselectionId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                await Task.Factory.FromAsync(client.BeginRemoveReaderCartSelection, client.EndRemoveReaderCartSelection, readerId, bookId, readercartselectionId, null);
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<ApprovedOrderDto[]> DoGetApprovedOrdersByReader(long readerId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<ApprovedOrderDto[]>.Factory.FromAsync(client.BeginGetApprovedOrdersByReader, client.EndGetApprovedOrdersByReader, readerId, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task DoOrderedReaderCartSelection(long readerId)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                await Task.Factory.FromAsync(client.BeginOrderedReadingCartBy, client.EndOrderedReadingCartBy, readerId, null);
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task DoSetOrderItemRecoveryDate(long bookId, long approvedorderId, DateTime date)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                await Task.Factory.FromAsync(client.BeginSetOrderItemRecoveryDate, client.EndSetOrderItemRecoveryDate, bookId, approvedorderId, date, null);
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public async static Task<bool> DoUpdatePublisher(PublisherDto publisher)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res = Task<bool>.Factory.FromAsync(client.BeginUpdatePublisher, client.EndUpdatePublisher, publisher, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                return false;
                throw;
            }
        }

        public async static Task<bool> DoUpdateAuthor(AuthorDto author)
        {
            LibraryReport client;
            try
            {
                client = new LibraryReport();
                var res =  Task<bool>.Factory.FromAsync(client.BeginUpdateAuthor, client.EndUpdateAuthor, author, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                return false;
                throw;
            }
        }
    }
}

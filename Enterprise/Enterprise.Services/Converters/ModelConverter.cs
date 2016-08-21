using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Enterprise.Services.WebServices;
using Enterprise.CoreData.ConverterFactory;
using Enterprise.Model;
using ProjectBase.Utils;



namespace Enterprise.Services.Converters
{
    public class AuthorModelConverter : IConverterItem<AuthorModel, AuthorDto>
    {
        public AuthorDto ToProxyItem(AuthorModel param, bool isBase = false)
        {
            Check.Require(param != null, "Author param must be provided");

            AuthorDto author = new AuthorDto();
            author.ID = param.ID;
            author.FirstName = param.FirstName;
            author.LastName = param.LastName;
            return author;
        }

        public AuthorModel ToRepoItem(AuthorDto param, bool isBase = false)
        {
            Check.Require(param != null, "AuthorDto must be provided");

            AuthorModel author = new AuthorModel();
            author.SetAssignedIdTo(param.ID);
            author.FirstName = param.FirstName;
            author.LastName = param.LastName;
            return author;
        }

    }

    public class BookModelConverter : IConverterItem<BookModel, BookDto>
    {
        public BookDto ToProxyItem(BookModel param, bool isBase = false)
        {
            Check.Require(param != null, "Book must be provided");

            BookDto book = new BookDto();
            book.ID = param.ID;
            book.Description = param.Description;
            book.Isbn = param.ISBN;
            book.PenaltyPrice = param.PenaltyPrice;
            
            if (isBase)
            {
                book.Publisher = ConverterFactory.GetConverter<PublisherModel, PublisherDto>()
                    .ToProxyItem(param.Publisher);
                book.PublisherId = param.PublisherId;
            }
            return book;

        }

        public BookModel ToRepoItem(BookDto param, bool isBase = false)
        {
            Check.Require(param != null, "BooDto must be provided");

            BookModel book = new BookModel();
            book.SetAssignedIdTo(param.ID);
            book.Description = param.Description;
            book.ISBN = param.Isbn;
            book.PenaltyPrice = param.PenaltyPrice;
            book.PublisherId = param.PublisherId;
            if (isBase)
            {
                book.Publisher = ConverterFactory.GetConverter<PublisherModel, PublisherDto>()
                    .ToRepoItem(param.Publisher);
                book.PublisherId = param.Publisher.ID;
                
            }
            return book;
        }
    }

    public class BookToAuthorModelConverter : IConverterItem<BookToAuthorModel, BookToAuthorDto>
    {
        public BookToAuthorDto ToProxyItem(BookToAuthorModel param, bool isBase = false)
        {
            Check.Require(param != null, "BookToAuthor must be provided");
            BookToAuthorDto bookToauthor = new BookToAuthorDto();
            bookToauthor.ID = param.ID;
            if (isBase)
            {
                bookToauthor.Author = ConverterFactory.GetConverter<AuthorModel, AuthorDto>()
                    .ToProxyItem(param.Author);
                bookToauthor.PosAuthorList = param.PosAuthorList;
                bookToauthor.Book = ConverterFactory.GetConverter<BookModel, BookDto>()
                    .ToProxyItem(param.Book, true);
            }
            return bookToauthor;
        }

        public BookToAuthorModel ToRepoItem(BookToAuthorDto param, bool isBase = false)
        {
            Check.Require(param != null, "BookToAuthorDto must be provided");
            BookToAuthorModel bookToauthor = new BookToAuthorModel();
            bookToauthor.SetAssignedIdTo(param.ID);
            if (isBase)
            {
                bookToauthor.Book = ConverterFactory.GetConverter<BookModel, BookDto>()
                    .ToRepoItem(param.Book, true);
                bookToauthor.PosAuthorList = param.PosAuthorList;
                bookToauthor.Author = ConverterFactory.GetConverter<AuthorModel, AuthorDto>()
                    .ToRepoItem(param.Author);
            }
            return bookToauthor;
        }


    }
    public class PublisherModelConverter : IConverterItem<PublisherModel, PublisherDto>
    {
        public PublisherDto ToProxyItem(PublisherModel param, bool isBase = false)
        {
            Check.Require(param != null, "Publisher must be provided");

            PublisherDto publisher = new PublisherDto();
            publisher.ID = param.ID;
            publisher.Title = param.Title;
            if (isBase)
            {
                publisher.Book = param.Books.Select<BookModel,BookDto>(
                    x=>ConverterFactory.GetConverter<BookModel, BookDto>().ToProxyItem(x)).ToArray();
               
            }
            return publisher;
        }
        public PublisherModel ToRepoItem(PublisherDto param, bool isBase = false)
        {
            Check.Require(param != null, "PublisherDto must be provided");

            PublisherModel publisher = new PublisherModel();
            publisher.SetAssignedIdTo(param.ID);
            publisher.Title = param.Title;
            if (isBase)
            {
                publisher.Books = param.Book.Select<BookDto, BookModel>(
                    x => ConverterFactory.GetConverter<BookModel, BookDto>().ToRepoItem(x)).ToList();
                
            }
            return publisher;
        }
    }

    public class ItemModelConverter : IConverterItem<ItemModel, ItemDto>
    {
        public ItemModel ToRepoItem(ItemDto param, bool isBase = false)
        {
            Check.Require(param != null, "ItremDto must be provided");
            ItemModel item = new ItemModel();
            item.SetAssignedIdTo(param.ID);
            item.InventorySerialCode = param.InventotySerialCode;
            item.IsOrdered = param.IsOrdered;
            item.IsReadCarted = param.IsReadCarted;
            item.BookId = param.Book.ID;
            item.RecoveredDate = param.RecoveredDate;
            item.PlanedRecoveringDate = param.PlanedRecoveringDate;
            if (isBase)
            {
                item.Book = ConverterFactory.GetConverter<BookModel, BookDto>().ToRepoItem(param.Book, true);
                item.ReaderCartSelection = ConverterFactory.GetConverter<ReaderCartSelectionModel, ReaderCartSelectionDto>().ToRepoItem(param.ReaderCartSelection);
            }
            return item;
        }

        public ItemDto ToProxyItem(ItemModel param, bool isBase = false)
        {
            Check.Require(param != null, "Item must be provided");

            ItemDto item = new ItemDto();
            item.ID = param.ID;
            item.InventotySerialCode = param.InventorySerialCode;
            item.IsOrdered = param.IsOrdered;
            item.IsReadCarted = param.IsReadCarted;
            item.RecoveredDate = param.RecoveredDate;
            item.PlanedRecoveringDate = param.PlanedRecoveringDate;
            if (isBase)
            {
                item.Book = ConverterFactory.GetConverter<BookModel, BookDto>().ToProxyItem(param.Book, true);
            }
            return item;
        }
    }

    public class EmailModelConverter : IConverterItem<EmailModel, EmailDto>
    {
        public EmailDto ToProxyItem(EmailModel param, bool isBase = false)
        {
            Check.Require(param != null, "EmailModel must be provided");
            EmailDto email = new EmailDto();
            email.EmailAddress = param.EmailAddress;
            return email;
        }

        public EmailModel ToRepoItem(EmailDto param, bool isBase = false)
        {
            Check.Require(param != null, "EmailDto  must be provided");
            EmailModel email = new EmailModel();
            email.EmailAddress = param.EmailAddress;
            return email;
        }
    }

    public class ReadingCartSelectionModelConverter : IConverterItem<ReaderCartSelectionModel, ReaderCartSelectionDto>
    {
        public ReaderCartSelectionDto ToProxyItem(ReaderCartSelectionModel param, bool isBase = false)
        {
            Check.Require(param != null, "ReaderCartSelectionModel must be provided");
            ReaderCartSelectionDto readerCartSelect = new ReaderCartSelectionDto();
            readerCartSelect.ID = param.ID;
            return readerCartSelect;
        }

        public ReaderCartSelectionModel ToRepoItem(ReaderCartSelectionDto param, bool isBase = false)
        {
            Check.Require(param != null, "ReaderCartSelectionDto must be provided");
            ReaderCartSelectionModel readerCartSelect = new ReaderCartSelectionModel();
            readerCartSelect.ID = param.ID;
            return readerCartSelect;
        }
    }

    public class ReaderModelConverter : IConverterItem<ReaderModel, ReaderDto>
    {
        public ReaderModel ToRepoItem(ReaderDto param, bool isBase = false)
        {
            ReaderModel reader = new ReaderModel();
            reader.SetAssignedIdTo(param.ID);
            if (isBase)
            {
                reader.EmailIdentity = ConverterFactory.GetConverter<EmailModel, EmailDto>()
                    .ToRepoItem(param.EmailIdentity);
            }
            return reader;
        }
        public ReaderDto ToProxyItem(ReaderModel param, bool isBase = false)
        {
            ReaderDto reader = new ReaderDto();
            reader.ID = param.ID;
            if (isBase)
            {
                reader.EmailIdentity = ConverterFactory.GetConverter<EmailModel, EmailDto>()
                    .ToProxyItem(param.EmailIdentity);
            }
            return reader;
        }
    }

    public class ApprovedOrderConverter : IConverterItem<ApprovedOrderModel, ApprovedOrderDto>
    {
        public ApprovedOrderModel ToRepoItem(ApprovedOrderDto param, bool isBase = false)
        {
            ApprovedOrderModel approvedOrder = new ApprovedOrderModel();
            approvedOrder.ID = param.ID;
            approvedOrder.OrderedDate = param.OrderedDate;
            approvedOrder.ApprovedNumber = param.ApprovedNumber;
            approvedOrder.RecoveredDate = param.RecoveredDate;
            
            if (isBase)
            {
                approvedOrder.OrderItems = param.OrderItems.Select<ItemDto, ItemModel>(
                    x => ConverterFactory.GetConverter<ItemModel, ItemDto>().ToRepoItem(x, true)).ToList();

            }
            return approvedOrder;
        }

        public ApprovedOrderDto ToProxyItem(ApprovedOrderModel param, bool isBase = false)
        {
            ApprovedOrderDto approvedOrder = new ApprovedOrderDto();
            approvedOrder.ID = param.ID;
            approvedOrder.OrderedDate = param.OrderedDate;
            approvedOrder.ApprovedNumber = param.ApprovedNumber;
            approvedOrder.RecoveredDate = param.RecoveredDate;
            if (isBase)
            {
                approvedOrder.OrderItems = param.OrderItems.Select<ItemModel, ItemDto>(
                    x => ConverterFactory.GetConverter<ItemModel, ItemDto>().ToProxyItem(x, true)).ToArray();
            }
            return approvedOrder;
        }
    }
}

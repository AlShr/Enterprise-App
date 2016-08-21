using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Enterprise.CoreData.Domain;
using Enterprise.CoreData.ConverterFactory;
using Enterprise.CoreData.Dto;
using ProjectBase.Utils;
using ProjectBase.Data;

namespace Enterprise.CoreData.Converters
{

    public class AuthorConverter : IConverterItem<Author, AuthorDto>
    {
        public AuthorDto ToProxyItem(Author param, bool isBase = false)
        {
            Check.Require(param != null, "Author param must be provided");

            AuthorDto author = new AuthorDto();
            author.ID = param.ID;
            author.FirstName = param.FirstName;
            author.LastName = param.LastName;
            return author;
        }

        public Author ToRepoItem(AuthorDto param, bool isBase = false)
        {
            Check.Require(param != null, "AuthorDto must be provided");

            Author author = new Author();
            author.SetAssignedIdTo(param.ID);
            author.FirstName = param.FirstName;
            author.LastName = param.LastName;
            return author;
        }

    }

    public class BookConverter : IConverterItem<Book, BookDto>
    {
        public BookDto ToProxyItem(Book param, bool isBase = false)
        {
            Check.Require(param != null, "Book must be provided");

            BookDto book = new BookDto();
            book.ID = param.ID;
            book.Description = param.Description;
            book.Isbn = param.ISBN;
            book.PenaltyPrice = param.PenaltyPrice;
            if (isBase)
            {
                book.Publisher = ConverterFactory.GetConverter<Publisher, PublisherDto>()
                    .ToProxyItem(param.Publisher, true);
                book.PublisherId = param.Publisher.ID;
              
            }
            return book;

        }

        public Book ToRepoItem(BookDto param, bool isBase = false)
        {
            Check.Require(param != null, "BooDto must be provided");

            Book book = new Book();
            book.SetAssignedIdTo(param.ID);
            book.Description = param.Description;
            book.ISBN = param.Isbn;
            book.PenaltyPrice = param.PenaltyPrice;
            if (isBase)
            {
                book.Publisher = ConverterFactory.GetConverter<Publisher, PublisherDto>()
                    .ToRepoItem(param.Publisher, true);
            
            }
            return book;
        }

    }

    

    public class PublisherConverter : IConverterItem<Publisher, PublisherDto>
    {
        public PublisherDto ToProxyItem(Publisher param, bool isBase = false)
        {
            Check.Require(param != null, "Publisher must be provided");

            PublisherDto publisher = new PublisherDto();
            publisher.ID = param.ID;
            publisher.Title = param.Title;          
            return publisher;
        }

        public Publisher ToRepoItem(PublisherDto param, bool isBase = false)
        {
            Check.Require(param != null, "PublisherDto must be provided");

            Publisher publisher = new Publisher();
            publisher.SetAssignedIdTo(param.ID);
            publisher.Title = param.Title;
        
            return publisher;
        }
    }

   
    public class BookToAuthorConverter : IConverterItem<BookToAuthor, BookToAuthorDto>
    {
        public BookToAuthorDto ToProxyItem(BookToAuthor param, bool isBase = false)
        {
            Check.Require(param != null, "BookToAuthor must be provided");
            BookToAuthorDto bookToauthor = new BookToAuthorDto();
            bookToauthor.ID = param.ID;
            if (isBase)
            {
                bookToauthor.Author = ConverterFactory.GetConverter<Author, AuthorDto>()
                    .ToProxyItem(param.Author);
                bookToauthor.PosAuthorList = param.PosAuthorList;
                bookToauthor.Book = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToProxyItem(param.Book, true);
            }
            return bookToauthor;
        }

        public BookToAuthor ToRepoItem(BookToAuthorDto param, bool isBase = false)
        {
            Check.Require(param != null, "BookToAuthorDto must be provided");
            BookToAuthor bookToauthor = new BookToAuthor();
            bookToauthor.SetAssignedIdTo(param.ID);
            if (isBase)
            {
                bookToauthor.Book = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToRepoItem(param.Book, true);
                bookToauthor.Author = ConverterFactory.GetConverter<Author, AuthorDto>()
                    .ToRepoItem(param.Author);
                bookToauthor.PosAuthorList = param.PosAuthorList;
            }
            return bookToauthor;
        }

    }

    public class EmailConverter : IConverterItem<Email, EmailDto>
    {
        public EmailDto ToProxyItem(Email param, bool isBase = false)
        {
            Check.Require(param != null, "Email must be provided");

            EmailDto email = new EmailDto();
            email.EmailAddress = param.EmailAddress;
            return email;
        }
        public Email ToRepoItem(EmailDto param, bool isBase = false)
        {
            Check.Require(param != null, "EmailDto must be provided");

            Email email = new Email();
            email.EmailAddress = param.EmailAddress;
            return email;
        }
    }

    public class ReaderConverter : IConverterItem<Reader, ReaderDto>
    {
        public ReaderDto ToProxyItem(Reader param, bool isBase = false)
        {
            Check.Require(param != null, "Reader must be provided");

            ReaderDto reader = new ReaderDto();
            reader.ID = param.ID;
            if (isBase)
            {
                reader.EmailIdentity = ConverterFactory.GetConverter<Email, EmailDto>()
                    .ToProxyItem(param.EmailIdentity);
            }
            return reader;
        }
        public Reader ToRepoItem(ReaderDto param, bool isBase = false)
        {
            Check.Require(param != null, "ReaderDto must be provided");

            Reader reader = new Reader();
            reader.SetAssignedIdTo(param.ID);
            if (isBase)
            {
                reader.EmailIdentity = ConverterFactory.GetConverter<Email, EmailDto>()
                    .ToRepoItem(param.EmailIdentity);
            }
            return reader;
        }
    }

    public class ReadingCartConverter : IConverterItem<ReadingCart, ReadingCartDto>
    {
        public ReadingCartDto ToProxyItem(ReadingCart param, bool isBase = false)
        {
            Check.Require(param != null, "ReadingCart must be provided");

            ReadingCartDto readingCart = new ReadingCartDto();
            readingCart.ID = param.ID;
            if (isBase)
            {
                readingCart.CartOfReader = ConverterFactory.GetConverter<Reader, ReaderDto>()
                    .ToProxyItem(param.CartOfReader, true);
                readingCart.CartSelections = param.CartSelections.Select<ReaderCartSelection, ReaderCartSelectionDto>(
                    x => ConverterFactory.GetConverter<ReaderCartSelection, ReaderCartSelectionDto>().ToProxyItem(x, true)).ToList();
            }
            return readingCart;
        }

        public ReadingCart ToRepoItem(ReadingCartDto param, bool isBase = false)
        {
            Check.Require(param != null, "ReadingCartDto must be provided");

            ReadingCart readingCart = new ReadingCart();

            if (isBase)
            {
                readingCart.CartOfReader = ConverterFactory.GetConverter<Reader, ReaderDto>()
                    .ToRepoItem(param.CartOfReader, true);
            }
            return readingCart;
        }
    }

    public class ReadingCartSelectionConverter : IConverterItem<ReaderCartSelection, ReaderCartSelectionDto>
    {
        public ReaderCartSelectionDto ToProxyItem(ReaderCartSelection param, bool isBase = false)
        {
            Check.Require(param != null, "ReaderCartSelection must be provided");

            ReaderCartSelectionDto readerCartSelection = new ReaderCartSelectionDto();
            readerCartSelection.ID = param.ID;
            if (isBase)
            {
                readerCartSelection.CurrentBook = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToProxyItem(param.CurrentBook, true);            
            }
            return readerCartSelection;
        }

        public ReaderCartSelection ToRepoItem(ReaderCartSelectionDto param, bool isBase = false)
        {
            Check.Require(param != null, "ReaderCartSelectionDto must be provided");

            ReaderCartSelection readerCartSelection = new ReaderCartSelection();
            readerCartSelection.SetAssignedIdTo(param.ID);
            if (isBase)
            {
                readerCartSelection.CurrentBook = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToRepoItem(param.CurrentBook, true);              
            }
            return readerCartSelection;
        }
    }

    public class ItemConverter : IConverterItem<Item, ItemDto>
    {
        public ItemDto ToProxyItem(Item param, bool isBase = false)
        {
            Check.Require(param != null, "Item must be provided");

            ItemDto item = new ItemDto();
            item.ID = param.ID;
            item.IsOrdered = param.IsOrdered;
            item.IsReadCarted = param.IsReadCarted;
            item.InventotySerialCode = param.InventorySerialCode;
            item.RecoveredDate = param.RecoveredDate;
            item.PlanedRecoveringDate = param.PlanedRecoveringDate;
            if (isBase)
            {
                item.Book = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToProxyItem(param.ItemDescription, true);
                item.ReaderCartSelection = ConverterFactory.GetConverter<ReaderCartSelection, ReaderCartSelectionDto>()
                    .ToProxyItem(param.ReaderCartSelection);
                
            }
            return item;
        }

        public Item ToRepoItem(ItemDto param, bool isBase = false)
        {
            Check.Require(param != null, "Item must be provided");

            Item item = new Item();
            item.SetAssignedIdTo(param.ID);
            item.IsOrdered = param.IsOrdered;
            item.IsReadCarted = param.IsReadCarted;
            item.InventorySerialCode = param.InventotySerialCode;
            item.RecoveredDate = param.RecoveredDate;
            item.PlanedRecoveringDate = param.PlanedRecoveringDate;
            if (isBase)
            {
                item.ItemDescription = ConverterFactory.GetConverter<Book, BookDto>()
                    .ToRepoItem(param.Book, true);
               
            }
            return item;
        }
    }
   
    public class OrderConverter : IConverterItem<Order, OrderDto>
    {
        public OrderDto ToProxyItem(Order param, bool isBase = false)
        {
            Check.Require(param != null, "Order must be provided");

            OrderDto order = new OrderDto();
            order.ID = param.ID;
            order.OrderDate = param.OrderDate;
            order.OrderNumber = param.OrderNumber;
            if (isBase)
            {
                order.OrderItems = param.OrderItems.Select<Item, ItemDto>(
                              x => ConverterFactory.GetConverter<Item, ItemDto>().ToProxyItem(x, true)).ToList();
            }
            return order;
        }

        public Order ToRepoItem(OrderDto param, bool isBase = false)
        {
            Check.Require(param != null, "OrderDto must be provided");

            Order order = new Order();
            order.SetAssignedIdTo(param.ID);
            order.OrderDate = param.OrderDate;
            order.OrderNumber = param.OrderNumber;
            if (isBase)
            {
                order.OrderItems = param.OrderItems.Select<ItemDto, Item>(
                               x => ConverterFactory.GetConverter<Item, ItemDto>().ToRepoItem(x, true)).ToList();
            }
            return order;
        }
    }

    public class ApprovedOrderConverter : IConverterItem<ApprovedOrder, ApprovedOrderDto>
    {
        public ApprovedOrderDto ToProxyItem(ApprovedOrder param, bool isBase = false)
        {
            Check.Require(param != null, "ApprovedOrder must be provided");

            ApprovedOrderDto approvedorder = new ApprovedOrderDto();
            approvedorder.ID = param.ID;
            approvedorder.ApprovedNumber = param.ApprovedNumber;
            approvedorder.OrderedDate = param.OrderedDate;
            approvedorder.RecoveredDate = param.RecoveredDate;
            if (isBase) 
            {
                approvedorder.OrderItems = param.OrderItems.Select<Item, ItemDto>(
                    x => ConverterFactory.GetConverter<Item, ItemDto>().ToProxyItem(x, true)).ToList();
            }
            return approvedorder;
        }

        public ApprovedOrder ToRepoItem(ApprovedOrderDto param, bool isBase = false)
        {
            Check.Require(param != null, "ApprovedOrderDto must be provided");

            ApprovedOrder approvedorder = new ApprovedOrder();
            approvedorder.SetAssignedIdTo(param.ID);
            approvedorder.OrderedDate = param.OrderedDate;
            approvedorder.ApprovedNumber = param.ApprovedNumber;
            approvedorder.RecoveredDate = param.RecoveredDate;
            if (isBase)
            {
                approvedorder.OrderItems = param.OrderItems.Select<ItemDto, Item>(
                    x => ConverterFactory.GetConverter<Item, ItemDto>().ToRepoItem(x, true)).ToArray();
            }
            return approvedorder;
        }
    }
  
}

using System;
using System.Collections.Generic;
using Enterprise.CoreData.Domain;


namespace Enterprise.NUnitTests.Data.TestFactories
{
    public class TestBooksFactory
    {
        public List<Book> CreateBooks()
        {
            List<Book> bookListing = new List<Book>();
            bookListing.Add(Book2);
            bookListing.Add(Book3);
            return bookListing;
        }

        public Book CreateBook()
        {
            return Book1;
        }

        private Book Book1
        {
            get 
            {
                List<Author> authorsOfBook = new TestAuthorsFactory().CreateAuthors();
                Book book = new Book(Publisher2) { Description = "Training", ISBN = "5-02-013850-9", PenaltyPrice = 200 };
                book.SetAssignedIdTo(1);            
                return book;
            }
        }
        private Book Book2
        {
            get
            {
                List<Author> authorsOfBook = new TestAuthorsFactory().CreateAuthors();

                Book book = new Book(Publisher1) { Description = "CookBook", ISBN = "5-02-013850-11", PenaltyPrice = 200 };
                book.SetAssignedIdTo(2);
              
                return book;
            }
        }
        private Book Book3
        {
            get
            {
                List<Author> authorsOfBook = new TestAuthorsFactory().CreateAuthors();

                Book book = new Book(Publisher1) { Description = "Example", ISBN = "5-02-013850-12", PenaltyPrice = 200 };
                book.SetAssignedIdTo(3);
            
                return book;
            }
        }
        private Publisher Publisher1
        {
            get 
            { 
                Publisher publisher = new Publisher("Piter");
                publisher.SetAssignedIdTo(1);
                return publisher;
            }           
        }
        private Publisher Publisher2
        {
            get
            {
                Publisher publisher = new Publisher("Syncfusion");
                publisher.SetAssignedIdTo(2);
                return publisher;
            }
        }
    }
}

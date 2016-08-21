using System;
using System.Collections.Generic;
using Enterprise.Model;

namespace Enterprise.NUnitTests.Data.TestFactories
{
    public class TestBookModelFactory
    {
        
        public BookModel CreateBook()
        {         
            return Book1;
        }

        private BookModel Book1
        {
            get 
            {
                List<AuthorModel> authorsOfBook = new TestAuthorModelFactory().CrateAuthors();
                BookModel book = new BookModel(Publisher1);
                book.SetAssignedIdTo(1);
                return book;
            }
        }

        private PublisherModel Publisher1
        {
            get 
            {
                PublisherModel publisher = new PublisherModel("Piter");
                publisher.SetAssignedIdTo(1);
                return publisher;
            }
        }
    }
}

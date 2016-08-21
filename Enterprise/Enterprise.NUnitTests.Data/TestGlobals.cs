using Enterprise.CoreData.Domain;

namespace Enterprise.NUnitTests.Data
{
    public class TestGlobals
    {
        public static Reader TestReader
        {
            get 
            {
                Email email = new Email ("example@gamil.com");               
                Reader reader = new Reader(email);
                return reader;
            }
        }
        public static Publisher Publisher
        {
            get
            {
                Publisher publisher = new Publisher("Piter");
                publisher.SetAssignedIdTo(1);
                return publisher;
            }
        }
        public static Book Book
        {
            get
            {
                Book book = new Book(Publisher) { Description = "Training", ISBN = "5-02-013850-9", PenaltyPrice = 100 };
                book.SetAssignedIdTo(1);
                //book.AddAuthor(Author);
                return book;
            }
        }
        public static Author Author
        {
            get
            {
                Author author = new Author("Mihail", "Bulgakov");
                author.SetAssignedIdTo(2);
                return author;
            }
        }
    }
}

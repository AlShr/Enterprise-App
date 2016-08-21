using System.Linq;
using System.Linq.Expressions;
using ProjectBase.Utils;
using Enterprise.CoreData.Domain;
using NUnit.Framework;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.NUnitTests.Data.DaoTestDoubles;
using System.Collections.Generic;


namespace Enterprise.NUnitTests.Data.Domain
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void CanCreateBook()
        {
            Book book = new Book(new Publisher("Piter"));
            Assert.AreEqual("Piter", book.Publisher.Title);
            book.Publisher.Title = "Syncfusion";
            Assert.AreEqual("Syncfusion", book.Publisher.Title);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CanCreateBookWithoutPublisher()
        {
            new Book(null);
        }

        [Test]
        public void CanCompareBooks()
        {
            Book bookFirst = new Book(new Publisher("Piter"));
            bookFirst.Description = "Pro";
            bookFirst.ISBN = "5-02-013850-11";
            Book bookSecond = new Book(new Publisher("Piter"));
            bookSecond.Description = "About";
            bookSecond.ISBN = "5-02-013850-12";
            Assert.AreNotEqual(bookFirst, null);
            Assert.AreNotEqual(bookFirst, bookSecond);

            bookFirst.SetAssignedIdTo(5);
            bookSecond.SetAssignedIdTo(5);

            Assert.AreEqual(bookFirst, bookSecond);
        }

        [Test]
        public void CanGetBookByFilterUsingMockedDao()
        {
            IBookDao bookDao = new MockBookDaoFactory().CreateMockBookDao();
            Assert.IsNotNull(bookDao);

            Book book = bookDao.GetBy(x => x.ISBN == "5-02-013850-9");
            Assert.IsNotNull(book);
        }

        [Test]
        public void CanGetBooksByFilterUsingMockedDao()
        {
            IBookDao bookDao = new MockBookDaoFactory().CreateMockBookDao();
            Assert.IsNotNull(bookDao);
            IList<Book> books = bookDao.Get(x => x.Publisher.Title == "Piter");
            Assert.AreEqual(2, books.Count);           
        }
     
        
    }
}

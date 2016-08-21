using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectBase.Utils;
using Enterprise.CoreData.Domain;

namespace Enterprise.NUnitTests.Data.Domain
{
    [TestFixture]
    public class AuthorTests
    {
        [Test]
        public void CanCreateAuthor()
        {
            Author author = new Author("Ivan","Bulgakov");
            author.SetAssignedIdTo(1);
            Assert.AreEqual("Bulgakov", author.LastName);
            author.LastName = "Dostoevskii";
            Assert.AreEqual("Dostoevskii", author.LastName);
            author.FirstName = "Fedor";
            Assert.AreEqual("Fedor", author.FirstName);
        }
        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateAuthorWithoutFirstLastName()
        {
            new Author("", "");
        }
        [Test]
        public void CanCompareAuthors()
        {
            Author authorFirst = new Author("Fedor", "Dostoevskii");            
            Author authorSecond = new Author("Mihail", "Bulgakov");
           
            Assert.AreNotEqual(authorFirst, null);
            Assert.AreNotEqual(authorFirst, authorSecond);

            authorFirst.SetAssignedIdTo(5);
            authorSecond.SetAssignedIdTo(5);

            Assert.AreEqual(authorFirst, authorSecond);
        }
      
    }
}

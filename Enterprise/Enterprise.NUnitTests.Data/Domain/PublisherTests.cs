using NUnit.Framework;
using ProjectBase.Utils;
using Enterprise.CoreData.Domain;

namespace Enterprise.NUnitTests.Data.Domain
{
    [TestFixture]
    public class PublisherTests
    {
        [Test]
        public void CanCreatePublisher()
        {
            Publisher publisher = new Publisher("Piter");
            Assert.AreEqual("Piter", publisher.Title);
            publisher.Title = "Syncfusion";
            Assert.AreEqual("Syncfusion", publisher.Title);
        }
        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePublisherWithoutTitle()
        {
            new Publisher("");
        }
    }
}

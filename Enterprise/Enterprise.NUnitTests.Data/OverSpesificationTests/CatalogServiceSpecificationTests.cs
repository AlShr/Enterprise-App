using System;
using NUnit.Framework;
using Moq;
using Enterprise.Model;
using Enterprise.Services.Common;
using Enterprise.Overspesification.Services;
using Enterprise.NUnitTests.Data.TestFactories;

namespace Enterprise.NUnitTests.Data.OverSpesificationTests
{
    public class CatalogServiceSpecificationTests
    {
        [Test]
        public void TestGetCatalogBooks()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetCatalogBooks());
        }

        [Test]
        public void TestGetCatalogAuthors()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetCatalogAuthors());
        }

        [Test]
        public void TestGetCatalogPublishers()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetCatalogPublishers());
        }

        [Test]
        public void TestGetCatalogReaders()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetCatalogReaders());
        }

        [Test]
        public void TestGetRelationsBookToAuthor()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetRelationsBookToAuthor()); 
        }

        [Test]
        public void TestGetItems()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetItems(1));
        }

        [Test]
        public void TestGetItemsSecond()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetItems(new long[] { 1 }, 1));
        }

        [Test]
        public void TestGetPublisherBy()
        {
            var mockcatalogService = new Mock<ICatalogServiceObject>();
            mockcatalogService.Setup(service => service.GetPublisherBy(1));
        }


       

       
    }
}

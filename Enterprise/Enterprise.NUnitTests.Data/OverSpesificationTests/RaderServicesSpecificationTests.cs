using System;
using NUnit.Framework;
using Moq;
using Enterprise.Model;
using Enterprise.Services.Common;
using Enterprise.Overspesification.Services;
using Enterprise.NUnitTests.Data.TestFactories;

namespace Enterprise.NUnitTests.Data.OverSpesificationTests
{
    public class RaderServicesSpecificationTests
    {
        [Test]
        public void TestOrderedReadingCartSelectins()
        {
            var mockCatalogService = new Mock<IReaderCartServiceObject>();
            
        }
    }
}

using System;
using NUnit.Framework;
using Moq;
using NSubstitute;
using Enterprise.Overspesification.Services;
using Enterprise.Services.Common;
using LibraryClient.Common;
using LibraryClient.Presenter;
using LibraryClient.Views;


namespace Enterprise.NUnitTests.Data.PresentersTests
{
    [TestFixture]
    public class ChildPresenterTests
    {
        private IApplicationController controller;
        private ChildPresenter presenter;
        private IChildView view;
        private long readerId;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            readerId = 1;
            view = Substitute.For<IChildView>();
            var service1 = Substitute.For<ICatalogServiceObject>();
            var service2 = Substitute.For<IReaderCartServiceObject>();
            presenter = new ChildPresenter(controller, view, service1, service2);
            presenter.Run();

        }

        [Test]
        public void GoToOrderedReport()
        {
            view.GoToOrderReport += Raise.Event<Action>();
            controller.Received().Run<ReportPreviewPresenter>();
        }

        [Test]
        public void GoToInventory()
        {
            view.GoToOrderedInventory += Raise.Event<Action>();
            controller.Received().Run<InventoryPresenter, long>(readerId);
        }

        [Test]
        public void GotoItemsInventory()
        {
            view.CheckedItemsInventory += Raise.Event<Action>();
            controller.Received().Run<ReaderCartPresenter, long[], long>(new long[] { 1 }, 1);
        }

       

        [Test]
        public void TestInitLookUpData()
        {
            var mockCatalogService = new Mock<ICatalogServiceObject>();
            mockCatalogService.Setup(service => service.GetCatalogAuthors());
        }

        [Test]
        public void TestInitCatalogBooks()
        {
            var mockBookCatalogService = new Mock<ICatalogServiceObject>();
            mockBookCatalogService.Setup(service => service.GetCatalogBooks());
        }
    }
}

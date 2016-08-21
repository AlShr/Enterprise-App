using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using NSubstitute;
using Enterprise.Model;
using Enterprise.NUnitTests.Data.DaoTestDoubles;
using Enterprise.Overspesification.Services;
using LibraryClient.Views;
using LibraryClient.Presenter;

namespace Enterprise.NUnitTests.Data.PresentersTests
{
    [TestFixture]
    public class SearchLookUpPresenterTests
    {
        [Test]
        public void TestInitView()
        {
            SearchLookUpViewStub view = new SearchLookUpViewStub();
            var service1 = Substitute.For<ICatalogServiceObject>();
            SearchLookUpPresenter presenter = new SearchLookUpPresenter(view, service1);

        }

        private class SearchLookUpViewStub : ISearchLookUpView
        {
            public IList<PublisherModel> PublisherCatalog
            {
                set { publisherCatalog = value; }
                get { return publisherCatalog; }
            }

            public IList<AuthorModel> AuthorCatalog
            {
                set { authorCatalog = value; }
                get { return authorCatalog; }
            }

            public IList<BookModel> BookCatalog
            {
                set { bookCatalog = value; }
                get { return bookCatalog; }

            }

            public void BindServiceData()
            {

            }

            private IList<BookModel> bookCatalog;
            private IList<AuthorModel> authorCatalog;
            private IList<PublisherModel> publisherCatalog;
            public event EventHandler Load;           
        } 
    }
}

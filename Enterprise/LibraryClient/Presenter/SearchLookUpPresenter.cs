using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using LibraryClient.Common;
using LibraryClient.Views;
using Enterprise.Overspesification.Services;
using Enterprise.Model;
using Enterprise.CoreData.Domain;
using ProjectBase.Utils;


namespace LibraryClient.Presenter
{
    public class SearchLookUpPresenter : Presenter<ISearchLookUpView>
    {
        public SearchLookUpPresenter(ISearchLookUpView view, ICatalogServiceObject catalogservice)
            : base(view)
        {
            Check.Require(view != null, "SearchLookUpView must be provided");
            Check.Require(catalogservice != null, "ObjectService must be provided");
            this.view = view;
            this.catalogservice = catalogservice;           
        }

        public void InitSearchLookUpView(IList<PublisherModel> publisherCatalog,
            IList<BookModel> bookCatalog, IList<AuthorModel> authorCatalog)
        {
            view.PublisherCatalog = publisherCatalog;
            view.BookCatalog = bookCatalog;
            view.AuthorCatalog = authorCatalog;
            view.BindServiceData();
        }

        private ICatalogServiceObject catalogservice;
        private ISearchLookUpView view;
       
    }
}

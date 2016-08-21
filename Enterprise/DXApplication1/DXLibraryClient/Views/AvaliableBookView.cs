using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Ninject;
using Ninject.Modules;
using LibraryClient.Common;
using Enterprise.Services.Common;
using Enterprise.Services.ServiceLocator;
using Enterprise.Overspesification.Services;
using LibraryClient.Presenter;
using LibraryClient.Views;
using ProjectBase.Utils;




namespace DXLibraryClient.Views
{
    public partial class AvaliableBookView : DevExpress.XtraReports.UI.XtraReport, IReportView
    {
        public AvaliableBookView()
        {
            InitializeComponent();  
            this.presenterCore = CreatePresenter();
            Presenter.InitReportView();
            BindServiceData();
        }

        protected new virtual AvaliableBookReportPresenter CreatePresenter()
        {
            return new AvaliableBookReportPresenter(this, this.Service);
        }

        public ICatalogDataSetService Service
        {
            get 
            {
                if (service == null)
                {
                    service = ServiceLocatorNinject.AppKernel.Get<CatalogServiceDataSet>();
                }
                return service;
            }
            set 
            {
                service = value;
            }
        }

        public virtual DataSet ReportData
        {
            get;
            set;
        }

        public virtual AvaliableBookReportPresenter Presenter
        {
            get { return presenterCore; }
        }

        public void BindServiceData()
        {
            this.DataSource = ReportData;

            this.xrTcBookDescription.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsBooks.DESCRIPTION")});
            
            this.xrTcBookId.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsBooks.ID)")});

            this.xrTcBookIsbn.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsBooks.ISBN")});

            this.xrTCBookPenaltyPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsBooks.Penalty_Price")});

            this.xrTcAuthorId.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsAuthors.AUTHOR_ID")});

            this.xrTcAuthorFirstName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Autohrs.Books_To_AuthorsAuthors.FirstName")});

            this.xrTcAuthorLastName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsAuthors.LastName")});

            this.xrTcPublisherId.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsPublishers.PUBLISHER_ID")});

            this.xrTcPublisherTitle.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"Books_To_Authors.Books_To_AuthorsPublishers.Publisher_Title")});

        }

        private ICatalogDataSetService service;
        private AvaliableBookReportPresenter presenterCore;
        public event EventHandler Load;
    }
}

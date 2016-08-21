using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Ninject;
using Ninject.Modules;
using Enterprise.Services;
using Enterprise.Services.Common;
using Enterprise.Services.ServiceLocator;
using Enterprise.Overspesification.Services;
using LibraryClient.Common;
using LibraryClient.Presenter;
using LibraryClient.Views;
using ProjectBase.Utils;






namespace DXLibraryClient.Views
{
    public partial class OverdueRecoverOrderView : DevExpress.XtraReports.UI.XtraReport, IReportView
    {      
        public OverdueRecoverOrderView()
        {
            InitializeComponent();           
            this.presenterCore = CreatePresenter();          
            Presenter.InitReportView();
            BindServiceData();
        }

        protected new virtual OverdueReportPresenter CreatePresenter()
        {
            return new OverdueReportPresenter(this, this.Service);
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

        public virtual OverdueReportPresenter Presenter
        {
            get { return presenterCore; }
        }

        public void BindServiceData()
        {
            this.DataSource = ReportData;

            this.xrTcApprovedOrderId.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.ID")});

            this.xrTcApprovedNumber.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.APPROVEDNUMBER")});

            this.xrTcAppItemDescription.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.DESCRIPTION")});

            this.xrTcApprovedOrdersOrderDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.ORDEREDDATE")});

            this.xrTcApprovedItemRecoveringDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEORDERS.PLANEDRECOVERINGDATE")});

            this.xrTcReaderEmailAddress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.ApprovedordersReader.EMAIL_ADDRESS")});

            this.xrTcReaderReaderId.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { 
            new DevExpress.XtraReports.UI.XRBinding("Text",null,"APPROVEDORDERS.ApprovedordersReader.READER_ID")});
        }

        private ICatalogDataSetService service; 
        private OverdueReportPresenter presenterCore;
        public event EventHandler Load;
    }
}

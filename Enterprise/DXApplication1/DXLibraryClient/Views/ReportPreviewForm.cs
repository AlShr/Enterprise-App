using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.Views.Base;
using Enterprise.Model;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using DXLibraryClient.Common;



namespace DXLibraryClient.Views
{
    public partial class ReportPreviewForm : DevExpress.XtraEditors.XtraForm, IReportPreview
    {
        public ReportPreviewForm()
        {
            InitializeComponent();
            bIClose.ItemClick += (sender, args) => InvokeAction(CloseShowDialog);
            this.reportFactory = GetReportFactory() as ReportPreviewFactory;
            this.overdueReport = GetOverdueReportView(reportFactory) as OverdueRecoverOrderView;          
            this.Loading += ViewLoadingDataChanged;
            this.bIAvaliableBook.ItemClick += SetAvaliableBookDocumentSource;
            this.bIOverdueOrders.ItemClick += SetOverdueRecoveringOrderDocumentSource;
            DataSource();
        }

        private ReportFactory<OverdueRecoverOrderView, AvaliableBookView> GetReportFactory()
        {
            return new ReportPreviewFactory();
        }

        public IReportControl GetOverdueReportView(IReportFactory reportFactory)
        {
            IReportControl overdueReportView = reportFactory.CreateReportControl();
            return overdueReportView;
        }

        public IReportControl GetAvaliableReportView(IReportFactory reportFactory)
        {
            IReportControl avaliableReportView = reportFactory.CreateReportControl1();
            return avaliableReportView;
        }

        private void DataSource()
        {          
            documentViewer1.DocumentSource = this.overdueReport;   
        }

        private void SetAvaliableBookDocumentSource(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.avaliableBookReport = GetAvaliableReportView(reportFactory) as AvaliableBookView;
            documentViewer1.DocumentSource = this.avaliableBookReport;
            this.avaliableBookReport.CreateDocument();
           
        }

        private void SetOverdueRecoveringOrderDocumentSource(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.overdueReport = GetOverdueReportView(reportFactory) as OverdueRecoverOrderView;
            documentViewer1.DocumentSource = this.overdueReport;
            this.overdueReport.CreateDocument();
        }

        private void InvokeAction(Action action)
        {
            if (action != null) action();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void RaiseLoadDataChanged()
        {
            EventHandler handler = Loading;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public virtual void OnLoadingDataChanged()
        {
            RaiseLoadDataChanged();
        }

        public void ViewLoadingDataChanged(object sender, EventArgs e)
        {
            base.OnLoad(e);
        }

        public new void Show()
        {
            ShowDialog();
        }

        private ReportPreviewFactory reportFactory;
        private OverdueRecoverOrderView overdueReport;
        private AvaliableBookView avaliableBookReport;
        public event Action CloseShowDialog;
        public event EventHandler Loading;
    }
}
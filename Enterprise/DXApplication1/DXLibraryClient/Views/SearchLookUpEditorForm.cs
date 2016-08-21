using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Win;
using DevExpress.XtraLayout;
using Ninject;
using Ninject.Modules;
using Enterprise.Model;
using Enterprise.Services;
using Enterprise.Services.Common;
using Enterprise.Services.ServiceLocator;
using Enterprise.Overspesification.Services;
using LibraryClient.Presenter;
using LibraryClient.Common;
using LibraryClient.Views;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;

namespace DXLibraryClient.Views
{
    [DefaultEvent("ValueChanged")]
    public partial class SearchLookUpEditorForm : UserControl, ISearchLookUpView, IDisposable
    {
        public SearchLookUpEditorForm()
        {
            InitializeComponent();
            this.presenterCore = CreatePresenter();

            this.sLookUpEdit1.EditValueChanged += searchLookUpOnEditValueChanged;          
            this.sLookUpEdit2.EditValueChanged += searchLookUpOnEditValueChanged;
            this.sLookUpEdit3.EditValueChanged += searchLookUpOnEditValueChanged;                       
            this.sBRefresh.Click += SbRefreshOnClicked;
          
        }

        protected virtual SearchLookUpPresenter CreatePresenter()
        {
            return new SearchLookUpPresenter(this, this.Service);
        }

        public ICatalogServiceObject Service
        {
            get
            {
                if (service == null)
                {
                    service = ServiceLocatorNinject.AppKernel.Get<CatalogServiceObject>();
                }
                return service;
            }
            set
            {
                service = value;
            }
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public virtual IList<PublisherModel> PublisherCatalog
        {
            get;
            set;
        }

        public virtual IList<AuthorModel> AuthorCatalog
        {
            get;
            set;
        }

        public virtual IList<BookModel> BookCatalog
        {
            get;
            set;
        }

        public virtual SearchLookUpPresenter Presenter
        {
            get { return presenterCore; }
        }
     
        public void BindServiceData()
        {
            sLookUpEdit1.Properties.DataSource = PublisherCatalog;
            sLookUpEdit1.Properties.DisplayMember = "Title";
            sLookUpEdit1.Properties.ValueMember = "ID";

            sLookUpEdit2.Properties.DataSource = AuthorCatalog;
            sLookUpEdit2.Properties.DisplayMember = "LastName";
            sLookUpEdit2.Properties.ValueMember = "ID";

            sLookUpEdit3.Properties.DataSource = BookCatalog;
            sLookUpEdit3.Properties.DisplayMember = "Description";
            sLookUpEdit2.Properties.ValueMember = "ID";
            repositoryItemLookUpEdit1.DataSource = PublisherCatalog;
        }

        private void SbRefreshOnClicked(object sender, EventArgs e)
        {
            sLookUpEdit1.EditValue = "";
            sLookUpEdit2.EditValue = "";
            sLookUpEdit3.EditValue = "";
            OnClickedChanged(e);
        }


        private void sLookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SearchLookUpEdit sLookup = sender as SearchLookUpEdit;
            if (sender != null)
            {
                foreach (Control control in sLookup.Controls)
                {
                    if (!(control is EditFormContainer))
                    {
                        continue;

                    }
                    foreach (Control nestedControl in control.Controls)
                    {
                        if (!(nestedControl is PanelControl))
                        {
                            continue;
                        }

                        foreach (Control button in nestedControl.Controls)
                        {
                            if (!(button is SimpleButton))
                            {
                                continue;
                            }
                            var simpleButton = button as SimpleButton;
                            MessageBox.Show("Test");
                            if (simpleButton.Text == "Clear" || simpleButton.Text == "Очистить")
                            {
                                MessageBox.Show("Test");
                            }
                        }
                    }
                }
            }
        }

        private void searchLookUpOnEditValueChanged(object sender, EventArgs e)
        {
            NewSearchCriteriaEventArgs args = new NewSearchCriteriaEventArgs();
            SearchLookUpEdit sledit = sender as SearchLookUpEdit;
            if (sledit == sLookUpEdit1)
            {
                if (sledit.EditValue == "")
                {
                    args.PublisherFilter = "";
                    return;
                }
                GridView view = sLookUpEdit1.Properties.View;
                object value = view.GetFocusedRowCellValue("Title");
                args.PublisherFilter = value as string;
            }
            if (sledit == sLookUpEdit2)
            {
                if (sledit.EditValue == "")
                {
                    args.AuthorFilter = "";
                    return;
                }
                GridView view = sLookUpEdit2.Properties.View;
                object value = view.GetFocusedRowCellValue("LastName");
                args.AuthorFilter = value as string;
            }
            if (sledit == sLookUpEdit3)
            {
                if (sledit.EditValue == "")
                {
                    args.BookFilter = "";
                    return;
                }
                GridView view = sLookUpEdit3.Properties.View;
                object value = view.GetFocusedRowCellValue("Description");
                args.BookFilter = value as string;
            }
            OnValueChanged(args);
        }

        public virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanded != null)
            {
                CheckedChanded(this, e);
            }              
        }

        public virtual void OnClickedChanged(EventArgs e)
        {
            if (RefreshClicked != null)
            {
                RefreshClicked(this, e);
            }
        }

        public virtual void OnValueChanged(NewSearchCriteriaEventArgs e)
        {
            EventHandler<NewSearchCriteriaEventArgs> handler = Volatile.Read(ref ValueChanged);
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }              
        }

        void IDisposable.Dispose()
        {
            this.sLookUpEdit1.EditValueChanged -= searchLookUpOnEditValueChanged;
            this.sLookUpEdit2.EditValueChanged -= searchLookUpOnEditValueChanged;
            this.sLookUpEdit3.EditValueChanged -= searchLookUpOnEditValueChanged;
            this.sBRefresh.Click -= SbRefreshOnClicked;
          
        }

        public event EventHandler<NewSearchCriteriaEventArgs> ValueChanged;
        public event EventHandler CheckedChanded;
        public event EventHandler RefreshClicked;
        private SearchLookUpPresenter presenterCore;
        private ICatalogServiceObject service;

        private void sLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void sBRefresh_Click(object sender, EventArgs e)
        {

        }
    }

    public class NewSearchCriteriaEventArgs : EventArgs
    {
        private string bookfilter = "";
        private string authorfilter = "";
        private string publisherfilter = "";
        public string BookFilter 
        {            
            get { return bookfilter; }
            set { bookfilter = value; }
        }
        public string AuthorFilter 
        {           
            get { return authorfilter; }
            set { authorfilter = value; }
        }
        public string PublisherFilter 
        { 
            get { return publisherfilter; }
            set { bookfilter = value; }
        }
    }
}

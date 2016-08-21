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
using ProjectBase.Utils;



namespace DXLibraryClient.Views
{
    public partial class InventoryForm : DevExpress.XtraEditors.XtraForm, IInventoryView
    {
        public InventoryForm(ICatalogManager<BookModel> catalogManager)
        {
            Check.Require(catalogManager != null, "ICatalogManager<BookModel> must be provided");
            InitializeComponent();
            this.catalogManager = catalogManager;
            barbtClose.ItemClick += (sender, args) => InvokeAction(CloseShowDialog);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public new void Show()
        {         
            ShowDialog();   
        }

        private void InvokeAction(Action action)
        {
            if (action != null) action();
        }

        #region Select Row

        private void ShowingPopUpEditor(object sender, ShowingPopupEditFormEventArgs e)
        {
            GridView gridView = sender as GridView;
            foreach (Control control in e.EditForm.Controls)
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
                        if (gridView.Tag.ToString() == "itemView")
                        {
                            simpleButton.Tag = "itemViewEdit";
                            if (simpleButton.Text == "Update" || simpleButton.Text == "Обновить")
                            {
                                simpleButton.Click -= EditFormUpdateButtonClick;
                                simpleButton.Click += EditFormUpdateButtonClick;
                            }                            
                        }                     
                    }
                }
            }
        }

        private void EditFormUpdateButtonClick(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button.Tag.ToString() == "itemViewEdit")
            {
                BookModel current = SelectRowObject as BookModel;
                OrderedBookModel approvedorder = SelectRowObjectMainView as OrderedBookModel;
                this.CatalogManager.SimulateNewRowCellEdit(current, SelectRowHandle, approvedorder.ID);
            }
        }

        private void orderedViewFocusedObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            GridView gridVew = sender as GridView;
            if (gridVew.Tag.ToString() == "approvedOrderView")
            {
                rowObjectMainview = e.Row;
            }
            if (gridVew.Tag.ToString() == "itemView")
            {
                rowObject = e.Row;
                rowHandleObjectSubView = e.FocusedRowHandle;
            }                    
        }   

        #endregion

        public void BindServiceData()
        {
            repositoryItemLookUpEdit5.DataSource = PublisherCatalog;
            (orderDetailGrid.DataSource as BindingSource).DataSource = MasterData;
        }

        public virtual IList<OrderedBookModel> MasterData
        {
            get;
            set;
        }

        public virtual IList<ItemModel> MasterDetailData
        {
            get;
            set;
        }

        public virtual IList<BookModel> BookCatalog
        {
            get;
            set;
        }

        public virtual IList<PublisherModel> PublisherCatalog
        {
            get;
            set;
        }

        public int[] SelectedRows
        {
            get { return selectedRows; }
            set
            {
                selectedRows = value;
            }

        }

        public int SelectRowHandle
        {
            get { return rowHandleObjectSubView; }
            set { rowHandleObjectSubView = value; }
        }

        public object SelectRowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        public object SelectRowObjectMainView
        {
            get { return rowObjectMainview; }
            set { rowObjectMainview = value; }
        }

        public void OnSelectedRowChanged()
        {
            throw new NotImplementedException();
        }

        public ICatalogManager<BookModel> CatalogManager
        {
            get
            {
                return catalogManager as CatalogManager<BookModel>;
            }
        }

        private object rowObject;
        private int rowHandleObjectSubView;
        private object rowObjectMainview;        
        private ICatalogManager<BookModel> catalogManager;     
        private int[] selectedRows;
        public event Action CloseShowDialog;          
    }
}
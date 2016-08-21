using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using Ninject;
using Ninject.Modules;
using Enterprise.Model;
using Enterprise.Services.ServiceLocator;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using ProjectBase.Utils;



namespace DXLibraryClient.Views
{
    public partial class ReaderCartForm : DevExpress.XtraEditors.XtraForm, IReaderCartView
    {
        public ReaderCartForm(ICatalogManager<ItemModel> catalogManager)
        {
            Check.Require(catalogManager != null, "ICatalogManager<ItemModel> must be provided");
            this.catalogManager = catalogManager;
            InitializeComponent();
          
            this.inventoryGrid.ForceInitialize();
            this.inventoryView.ShowingPopupEditForm += ShowingPopUpEditor;                      
            this.bItemClose.ItemClick += (sender, args) => InvokeAction(CloseShowDialog);
            this.bIOrdered.ItemClick += (sender, args) => InvokeAction(Order);          
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
                        if (gridView.Tag.ToString() == "inventoryView")
                        {
                            simpleButton.Tag = "inventoryViewEdit";
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
            if (button.Tag.ToString() == "inventoryViewEdit") 
            {
                ItemModel selectRowItem = SelectRowObject as ItemModel;
                CatalogManager.SimulateNewSelectRow(selectRowItem);
            }               
        }

        #endregion

        public void BindServiceData()
        {
            cartViewrepositoryItemLookUpEdit.DataSource = BookCatalog;
            (inventoryView.DataSource as BindingSource).DataSource = MasterDetailData;

        }

        private void inventoryViewFocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            rowObject = e.Row;
            rowHandleObjectView = e.FocusedRowHandle;
        }

        public ICatalogManager<ItemModel> CatalogManager
        {
            get 
            {
                return catalogManager as CatalogManager<ItemModel>; 
            }
        }
       
        public virtual IList<BookModel> BookCatalog
        {
            get;
            set;
        }

        public virtual IList<ItemModel> MasterDetailData
        {
            get;
            set;
        }

        public int SelectRowHandle
        {
            get { return rowHandleObjectView; }
            set { rowHandleObjectView = value; }
        }

        public object SelectRowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        private object rowObject;
        private int rowHandleObjectView;      
        public event Action Order;            
        public event Action CloseShowDialog;
        public ICatalogManager<ItemModel> catalogManager;
    }
}
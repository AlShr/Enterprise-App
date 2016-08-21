using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DXLibraryClient.Common;
using Enterprise.Model;
using LibraryClient.Common;
using LibraryClient.Presenter;
using LibraryClient.Managers;
using LibraryClient.Views;
using ProjectBase.Utils;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Docking2010.Customization;
using Enterprise.Services.Validators;
using DXLibraryClient.Validating;


namespace DXLibraryClient.Views
{
    public partial class ChildForm : DevExpress.XtraEditors.XtraForm, IChildView, IDisposable
    {
        public ChildForm(ApplicationContext context, ICatalogManager<BookModel> catalogManager,
            ICatalogManager<AuthorModel> authcatalogManager, ICatalogManager<PublisherModel> pubcatalogManager,
            ICatalogManager<long> catalogUserManager)
        {           
            Check.Require(context != null, "ApplicationContext must be provided");
            Check.Require(catalogManager != null, "ICatalogManager<BookModel> must be provided");
            Check.Require(authcatalogManager != null, "ICatalogManager<AuthorModel> must be provided");
            Check.Require(pubcatalogManager != null, "ICatalogManager<PublisherModel> must be provided");
            Check.Require(catalogUserManager != null, "ICatalogManager<long> must be provided");

            this.context = context;
            this.catalogManager = catalogManager;
            this.authcatalogManager = authcatalogManager;
            this.pubcatalogManager = pubcatalogManager;
            this.catalogUserManager = catalogUserManager; 

            InitializeComponent();
            bookgridView.IndicatorWidth = 40;
            pageSelector = new PageSelector();
            int temp = pageSelector.CurrentPage + 1;
            labelControl2.Text = "Page " + temp.ToString();

            this.detailGrid.ForceInitialize();
            this.button1.Click += (sender, args) => InvokeAction(Save);

            #region Init Search & Update MasterDetailView
            this.guiFactory = GetGuiFactory() as SearchLookUpGuiFactory;
            this.searchLookUp = GetSearchLookUp(guiFactory) as SearchLookUpEditorForm;
            
    
            this.searchLookUp.Parent = flowLayoutPanel1;
            this.searchLookUp.ValueChanged += ViewCriteriaCatalogDataChanged;
            this.searchLookUp.RefreshClicked += (sender, args) => Invoke(CheckedCatalogChanged);
            this.Loading += ViewLoadingDataChanged;
            #endregion

            #region Init Select Row MasterDetailView & Edit Book
            this.sbShowDetail.Enabled = false;
            this.sbShowDetail.Click += (sender, args) => InvokeAction(SelectCurrent);
            this.CatalogManager.SelectedRowChanged += ViewSelectedRowChanged;
            this.CatalogManager.NewRowObjectSelect += ShowDetailSelectObject;
            
            this.sBEditBack.Click += new EventHandler(this.DetailShowHideBtnClick);
            
            this.pbEdit.Click += (sender, args) => InvokeAction(EditCurrent);
            this.authorManualGridView.DoubleClick += (sender, args) => InvokeAction(SubViewEditCurrent);
            #endregion

            #region Edit Row Object
            this.CatalogManager.EditedRowChanged += ViewEditedRowChanged;
            this.CatalogManager.EditedRowSubViewChanged += ViewEditedRowSubViewChanged;
            this.repositoryItemLookUpEdit1.EditValueChanged += repoItemLookUpEditEditValueChanged;
            #endregion

            #region Add New Book Object
            this.sbNewBook.Enabled = false;
            this.sbNewBook.Click += (sender, args) => InvokeAction(NewChildObjectAdded);
            this.CatalogManager.EditedRowNewChildObjectAdded += ViewAddedNewChildRowChanged;
            #endregion

            #region GoTo Inventory
            this.biInventory.ItemClick += biInventoryItemClick;
            this.biInventory.ItemClick += (sender, arg) => InvokeAction(CheckedItemsInventory);
            this.biOrderInventory.ItemClick += (sender, arg) => InvokeAction(GoToOrderedInventory);
            #endregion

            #region GoTo OrderReport
            this.biOrderReport.ItemClick += (sender, args) => InvokeAction(GoToOrderReport);
            #endregion
            
            InitEditLookUpState();
           
            #region Select Currnet UserData
            InitLookUpUserDataView();
            this.lookUpEdit.EditValueChanged += LookUpUserDataEditValueChanged;
            this.lookUpEdit.EditValueChanged += (sender, args) => InvokeAction(SelectCurrentUser);
            this.CatalogUserManager.SelectedRowChanged += ViewUserDataSelectedRowChanged;
            #endregion

            #region Page Selector Set Pagination
            this.sBbackward.Click += SBbackwardClick;
            this.sBforward.Click += SBforwardClick;
            #endregion
            
        }

        private GuiFactory<SearchLookUpEditorForm> GetGuiFactory()
        {
            return new SearchLookUpGuiFactory();
        }

        public ILookUpControl GetSearchLookUp(IGuiFactory guiFactory)
        {
            ILookUpControl searchLookUpView = guiFactory.CreateLookUpControl();
            return searchLookUpView;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public new void Show()
        {
            context.MainForm = this;    
            Application.Run(context);
        }

        private void InvokeAction(Action action)
        {
            if (action != null) action();
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
            this.OnLoad(e);
        }

        #region Initiation Search

        /// <summary>
        ///Reaise Search by criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ViewCriteriaCatalogDataChanged(object sender, NewSearchCriteriaEventArgs e)
        {
            string filter = e.AuthorFilter + " " + e.BookFilter + " " + e.PublisherFilter;
            SearchModel searchModel = new SearchModel(filter);
            CatalogManager.SimulateNewCriteriaSearch(searchModel);
        }

        #endregion

        /// <summary>
        /// Rasise NewRowObjectSelect event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ViewSelectedRowChanged(object sender, EventArgs e)
        {
            BookModel selectRowBook = SelectRowObject as BookModel;
            CatalogManager.SimulateNewSelectRow(selectRowBook);
        }

        /// <summary>
        /// Raiser Edited Row Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ViewEditedRowChanged(object sender, EventArgs e)
        {
            BookModel selectRowBook = SelectRowObject as BookModel;
            CatalogManager.SimulateEditRow(selectRowBook);
        }

        /// <summary>
        /// Raise SubView Changed Show SubView Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ViewEditedRowSubViewChanged(object sender, EventArgs e)
        {
            BookModel selectRowBook = SelectRowObject as BookModel;
            int selectHandleObject = rowHandleObjectSubView;
            CatalogManager.SimulateEditRowSubView(selectRowBook, selectHandleObject);
        }

        /// <summary>
        /// Raise event NewRowObjectEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ViewAddedNewChildRowChanged(object sender, EventArgs e)
        {
            BookModel newrowobject = new BookModel();
            CatalogManager.SimulateNewSelectRow(newrowobject);
            SelectRowObject = newrowobject;
        }

        #region Select Current UserData
        public void ViewUserDataSelectedRowChanged(object sender, EventArgs e)
        {
            CatalogUserManager.SimulateNewSelectRow(SelectEditValue);
        }

        #endregion

        #region PagePagination

        private void SBbackwardClick(object sender, EventArgs e)
        {
            if (pageSelector.CurrentPage == 0) return;
            
            pageSelector.CurrentPage--;
            int temp = pageSelector.CurrentPage + 1;
            labelControl2.Text = "Page " + temp.ToString();
            CatalogManager.SimulateNewSetPagination(pageSelector);          
        }

        private void SBforwardClick(object sender, EventArgs e)
        {
            if (pageSelector.CurrentPage == pageSelector.PageCount - 1) return;
            
            pageSelector.CurrentPage++;
            int temp = pageSelector.CurrentPage + 1;
            labelControl2.Text = "Page " + temp.ToString();
            CatalogManager.SimulateNewSetPagination(pageSelector);
        }

        #endregion

      

        protected bool ValidateAddedNewRowBook(BookModel book)
        {
            if (book == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(book.ISBN))
            {
                return false;
            }
            if (book.PenaltyPrice <= 0)
            {
                return false;
            }
            return true;
        }

        private void biInventoryItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MasterDetailData = (detailGrid.DataSource as BindingSource).DataSource as IList<PublisherModel>;
        }

        /// <summary>
        /// Focused RowObject BookView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView.Tag.ToString() == "bookView")
            {               
                sbShowDetail.Enabled = true;
                rowObject = e.Row;
            }
            if (gridView.Tag.ToString() == "authorManualView")
            {
                rowHandleObjectSubView = e.FocusedRowHandle;
                rowObjectSubView = e.Row;
            }
            if (gridView.Tag.ToString() == "authorView")
            {
                rowObjectDetailView = e.Row;
            }
            if (gridView.Tag.ToString() == "publisherView")
            {
                sbNewBook.Enabled = true;
                rowObjectMainView = e.Row;
            }
        }

        /// <summary>
        /// Raise Edit Cell MainView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repoItemLookUpEditEditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit edit = bookgridView.ActiveEditor as LookUpEdit;
            if (edit != null)
            {
                string editValueToString = edit.EditValue.ToString();
                long publisherId;
                long.TryParse(editValueToString, out publisherId);
                BookModel book = SelectRowObject as BookModel;
                CatalogManager.SimulateNewRowCellEdit(book, SelectRowHandle, publisherId);
            }
        }

        private void ViewValidationgEditorPublisherView(object sender, BaseContainerValidateEditorEventArgs e)
        {
            currentPublisher = SelectRowObjectMainView as PublisherModel;
            BookEditValidatorEventArgs<BookModel> arg = new BookEditValidatorEventArgs<BookModel>(currentBook, e);
            switch ((e as EditFormValidateEditorEventArgs).Column.Name)
            {
                case "pvtitle":
                    string cellvalue1 = e.Value.ToString();
                    if (string.IsNullOrEmpty(cellvalue1))
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not empty";
                        break;
                    }
                    currentPublisher.Title = cellvalue1;
                    break;
            }
        }

        private void AuthorViewValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            currentAuthor = SelectRowObjectDetailView as AuthorModel;
            switch ((e as EditFormValidateEditorEventArgs).Column.Name)
            {
                case "authvfirstname":
                    string cellvalue1 = e.Value.ToString();
                    if (string.IsNullOrEmpty(cellvalue1))
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not empty";
                        break;
                    }
                    currentAuthor.FirstName = cellvalue1;
                    break;
                case "authvlastname":
                    string cellvalue2 = e.Value.ToString();
                    if (string.IsNullOrEmpty(cellvalue2))
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not empty";
                        break;
                    }
                    currentAuthor.LastName = cellvalue2;
                    break;
            }
        }

        private void ViewValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            currentBook = SelectRowObject as BookModel;
            switch ((e as EditFormValidateEditorEventArgs).Column.FieldName)
            {
                case "Descritpion":
                    string cellvalue1 = e.Value.ToString();
                    if (string.IsNullOrEmpty(cellvalue1))
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not emprty";
                        break;
                    }
                    currentBook.Description = cellvalue1;
                    break;
                case "ISBN":
                    string cellvalue2 = e.Value.ToString();
                    if (string.IsNullOrEmpty(cellvalue2))
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not emprty";
                        break;
                    }
                    currentBook.ISBN = cellvalue2;
                    break;
                case "PenaltyPrice":
                    double cellvalue3;
                    double.TryParse(e.Value.ToString(), out cellvalue3);
                    if (cellvalue3 < 0)
                    {
                        e.Valid = false;
                        e.ErrorText = "The value should be not emprty";
                        break;
                    }
                    currentBook.PenaltyPrice = cellvalue3;
                    break;
                case "PublisherId":
                    long cellvalue4;
                    long.TryParse(e.Value.ToString(), out cellvalue4);
                    this.CatalogManager.SimulateNewRowCellEdit(currentBook, SelectRowHandle, cellvalue4);
                    break;
            }
        }

        private void ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
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
                        if (gridView.Tag.ToString() == "bookView")
                        {
                            simpleButton.Tag = "bookViewEdit";
                            if (simpleButton.Text == "Update" || simpleButton.Text == "Обновить")
                            {
                                simpleButton.Click -= EditFormUpdateButtonClick;
                                simpleButton.Click += EditFormUpdateButtonClick;
                            }

                        }
                        if (gridView.Tag.ToString() == "publisherView")
                        {
                            simpleButton.Tag = "publisherViewEdit";
                            if (simpleButton.Text == "Update" || simpleButton.Text == "Обновить")
                            {
                                simpleButton.Click -= PubViewEditFormUpdateButtonClick;
                                simpleButton.Click += PubViewEditFormUpdateButtonClick;
                            }

                        }
                        if (gridView.Tag.ToString() == "authorView")
                        {
                            simpleButton.Tag = "authorViewEdit";
                            if (simpleButton.Text == "Update" || simpleButton.Text == "Обновить")
                            {
                                simpleButton.Click -= AuthorViewEditFormUpdateButtonClick;
                                simpleButton.Click += AuthorViewEditFormUpdateButtonClick;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// EditorForm reaise Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PubViewEditFormUpdateButtonClick(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button.Tag.ToString() == "publisherViewEdit")
            {
                this.PubCatalogManager.SimulateNewRowChange(currentPublisher);
            }
        }

        private void EditFormUpdateButtonClick(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button.Tag.ToString() == "bookViewEdit")
            {
                this.CatalogManager.SimulateNewRowChange(currentBook);
            }
        }

        private void AuthorViewEditFormUpdateButtonClick(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button.Tag.ToString() == "authorViewEdit")
            {
                this.AuthCatalogManager.SimulateNewRowChange(currentAuthor);
            }
        }

        private void bookgridView_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.DisplayError;
            e.WindowCaption = "Input Error";
            e.ErrorText = "The value should be greater than 0 and less than 1,000,000";
            bookgridView.HideEditor();
        }


        /// <summary>
        /// Show EditBookLayoutView with current RowObject
        /// </summary>
        /// <param name="row"></param>
        protected void ShowDetailSelectObject(object sender, NewRowObjectArgs<BookModel> e)
        {
            flowLayoutPanel1.Hide();
            detailGrid.MainView = editorlayoutView;
            BookModel book = e.RowObject as BookModel;
            (detailGrid.DataSource as BindingSource).DataSource = book;
            VisibleEditLookUpState(book);
        }

        private static bool canCloseFunc(DialogResult parameter)
        {
            return parameter != DialogResult.Cancel;
        }

        private void MessageBoxDialogShow()
        {                       
            if (XtraMessageBox.Show(this, "Save changes?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.Yes)
            {
                SaveChanges();
            }
        }

        /// <summary>
        /// Raise Save Changes SubView
        /// </summary>
        protected void SaveChanges()
        {
            BookModel book = (detailGrid.DataSource as BindingSource).DataSource as BookModel;
            Check.Require(book != null, "Book not selected for editing");
            if (book.ID != 0 && ValidateAddedNewRowBook(book))
            {
                CatalogManager.SimulateNewRowChange(book);
            }
            if (book.ID == 0 && ValidateAddedNewRowBook(book))
            {
                CatalogManager.SimulateNewRowObjectAdd(book);
            }
        }       

        /// <summary>
        /// Handler end edit book 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DetailShowHideBtnClick(object sender, EventArgs e)
        {
            MessageBoxDialogShow();
            flowLayoutPanel1.Show();
            detailGrid.MainView = publishergridView;
            BindServiceData();
            authorsGrid.Visible = false;           
            pbEdit.Visible = false;
            sBEditBack.Visible = false;             
        }

        public void VisibleEditLookUpState(BookModel book)
        {
            authorsGrid.Visible = true;
            book.Authors.Add(new AuthorModel() { ID = -1, FirstName = "", LastName = "", PosAuthList = 0 });
            (authorsGrid.DataSource as BindingSource).DataSource = book.Authors;
            pbEdit.Visible = true;
            sBEditBack.Visible = true;           
           
        }

        private void InitEditLookUpState()
        {
            authorsGrid.Visible = false;
            pbEdit.Visible = false;
            sBEditBack.Visible = false;
            
        }

        #region Select Current User Data

      //Reader LookUp Init
        public void BindServiceDataLookUpEditUserData()
        {        
            this.lookUpEdit.Properties.DataSource = ReaderCatalog;
        }

        private void InitLookUpUserDataView()
        {
            
            this.lookUpEdit.Properties.DisplayMember = "EmailIdentity.EmailAddress";
            this.lookUpEdit.Properties.ValueMember = "ID";
            LookUpColumnInfoCollection coll = this.lookUpEdit.Properties.Columns;
            coll.Add(new LookUpColumnInfo("ID", 0));
            coll.Add(new LookUpColumnInfo("EmailIdentity.EmailAddress", 0));
            this.lookUpEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            this.lookUpEdit.Properties.SearchMode = SearchMode.AutoComplete;
            this.lookUpEdit.Properties.AutoSearchColumnIndex = 1;
            
        }
        public void InitServiceDataSearchLookUp(IList<PublisherModel> publishers,
            IList<BookModel> books, IList<AuthorModel> authors)
        {
            this.searchLookUp.Presenter.InitSearchLookUpView(publishers, books, authors);
        }
       
        #endregion

        public void BindServiceData()
        {
            (detailGrid.DataSource as BindingSource).DataSource = MasterDetailData;
            repositoryItemLookUpEdit1.DataSource = PublisherCatalog;
        }

        private void bookgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void bookgridView_RowCountChanged(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = ((DevExpress.XtraGrid.Views.Grid.GridView)sender);
            if (!gridView.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gridView.GridControl.Handle);
            SizeF size = gr.MeasureString(gridView.RowCount.ToString(), gridView.PaintAppearance.Row.GetFont());
            gridView.IndicatorWidth = Convert.ToInt32(size.Width + 0.999f)
                + DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter.Indicator.ImageSize.Width + 10;
        }

        public virtual IList<PublisherModel> MasterDetailData
        {
            get;
            set;
        }

        public virtual IList<BookModel> DetailData
        {
            get;
            set;
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

        public PageSelector PageSelector
        {
            get;
            set;
        }
        #region Select Current UserData

        public virtual IList<ReaderModel> ReaderCatalog
        {
            get;
            set;
        }

        #endregion

        public ICatalogManager<BookModel> CatalogManager
        {
            get
            {
                return catalogManager as CatalogManager<BookModel>;
            }
        }

        public ICatalogManager<PublisherModel> PubCatalogManager
        {
            get
            {
                return pubcatalogManager as CatalogManager<PublisherModel>;
            }
        }

        public ICatalogManager<AuthorModel> AuthCatalogManager
        {
            get 
            {
                return authcatalogManager as CatalogManager<AuthorModel>; 
            }
            
        }

        public ICatalogManager<long> CatalogUserManager
        {
            get
            {
                return catalogUserManager as CatalogManager<long>;
            }         
        }

        
        /// <summary>
        /// Current editable object
        /// </summary>
        public object SelectRowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        public object SelectRowObjectSubView
        {
            get { return rowObjectSubView; }
        }

        public object SelectRowObjectDetailView
        {
            get { return rowObjectDetailView; }
            set { rowObjectDetailView = value; }
        }

        public int SelectRowHandle
        {
            get { return rowHandleObjectSubView; }
            set { rowHandleObjectSubView = value; }
        }

        public object SelectRowObjectMainView
        {
            get { return rowObjectMainView; }
            set { rowObjectMainView = value; }
        }

        #region Select Current User

        private void LookUpUserDataEditValueChanged(object sender, EventArgs e)
        {
            string editValueToString = lookUpEdit.EditValue.ToString();
            long.TryParse(editValueToString, out selectEditValue);
        }

       

        public long SelectEditValue
        {
            get { return selectEditValue; }
            set { selectEditValue = value; }
        }
        #endregion


        void IDisposable.Dispose()
        {
            this.searchLookUp.ValueChanged -= ViewCriteriaCatalogDataChanged;
            this.CatalogManager.SelectedRowChanged -= ViewSelectedRowChanged;
            this.CatalogManager.NewRowObjectSelect -= ShowDetailSelectObject;
            this.CatalogManager.EditedRowChanged -= ViewEditedRowChanged;
            this.CatalogManager.EditedRowSubViewChanged -= ViewEditedRowSubViewChanged;
            this.CatalogManager.EditedRowNewChildObjectAdded -= ViewAddedNewChildRowChanged;
        }

        public void ResetUi()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)this.ResetUi);
            }
            else 
            {
                Application.UseWaitCursor = false;
            }
        }

        private AuthorModel currentAuthor;
        private BookModel currentBook;
        private PublisherModel currentPublisher;
        private object rowObjectDetailView;
        private object rowObjectMainView;
        private object rowObject;
        private object rowObjectSubView;
        private int rowHandleObjectSubView;
        private PageSelector pageSelector;
        private SearchLookUpEditorForm searchLookUp;
        private SearchLookUpGuiFactory guiFactory;
        public event Action Save;
        public event Action NewChildObjectAdded;
        public event Action SelectCurrent;
        public event Action EditCurrent;
        public event Action SubViewEditCurrent;
        public event Action CheckedItemsInventory;
        public event Action GoToOrderedInventory;
        public event Action GoToOrderReport;
        public event EventHandler CheckedCatalogChanged;
        public event EventHandler Loading;
        public ICatalogManager<BookModel> catalogManager;
        public ICatalogManager<PublisherModel> pubcatalogManager;
        public ICatalogManager<AuthorModel> authcatalogManager;
        private readonly ApplicationContext context;

        #region Select Current User
        private long selectEditValue;
        public event Action SelectCurrentUser;
        public ICatalogManager<long> catalogUserManager;
        #endregion
    }
}
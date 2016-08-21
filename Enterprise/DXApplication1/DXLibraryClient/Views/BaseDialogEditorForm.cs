
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraEditors;
using Enterprise.Model;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using ProjectBase.Utils;



namespace DXLibraryClient.Views
{
    public partial class DialogEditorForm<T,TId>
        : DevExpress.XtraEditors.XtraForm, IShowDialogResult<T,TId> where T : BaseModel<TId>
    {
        public DialogEditorForm(ICatalogManager<T> catalogManager)
        {
            Check.Require(catalogManager != null, "ICatalogManager<T> must be provided");
            this.catalogManager = catalogManager;
            InitializeComponent();

            pageSelector = new PageSelector();
            this.bindingSource1.DataSource = typeof(T);
            button1.Click += (sender, args) => InvokeAction(CloseShowDialog);
            #region Init Select Row
            this.gridView.DoubleClick += (sender, args) => InvokeAction(SelectCurrent);
            this.SelectedRowChanged += ViewSelectedRowChanged;
            #endregion
        }

        public new void Show()
        {
            ShowDialog();
        }

        #region Init Select Row
        protected void InvokeAction(Action action)
        {
            if (action != null) action();
        }

        public void RaiseSelectedRowChanged()
        {
            EventHandler handler = SelectedRowChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public virtual void OnSelectedRowChanged()
        {
            RaiseSelectedRowChanged();
        }
        #endregion

        public virtual IList<T> BindCollection
        {
            get { return bindCollection; }
            set { bindCollection = value; }
        }

        public void BindServiceData()
        {
            (gridEditor.DataSource as BindingSource).DataSource = BindCollection;
        }

        protected virtual void OnNewSelectRowObject(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectSelect);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void ViewSelectedRowChanged(object sender, EventArgs e)
        {
            T selectRowobject = SelectRowObject as T;
            NewRowObjectArgs<T> args = new NewRowObjectArgs<T>(selectRowobject);
            OnNewSelectRowObject(args);
        }

        protected void GridRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            rowObject = e.Row;
        }

        private void ShowingPopUpEditor(object sender, ShowingPopupEditFormEventArgs e)
        {
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
                        simpleButton.Tag = "ViewEdit";
                        if (simpleButton.Text == "Update") 
                        {
                            simpleButton.Click -= EditFormUpdateButtonClick;
                            simpleButton.Click += EditFormUpdateButtonClick;
                        }                    
                    }
                }
            }
        }

        private void EditFormUpdateButtonClick(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            T selectRowItem = SelectRowObject as T;
            CatalogManager.SimulateEditRow(selectRowItem);
        }

        #region PagePaggination
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

        public object SelectRowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        public ICatalogManager<T> CatalogManager
        {
            get { return catalogManager as CatalogManager<T>; }
           
        }

        protected object rowObject;
        private GridControl gridEditor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.ComponentModel.IContainer components;
        private Button button1;
        private BindingSource bindingSource1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        public event Action CloseShowDialog;
        public event Action SelectCurrent;
        public event EventHandler SelectedRowChanged;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectSelect;
        private IList<T> bindCollection = default(IList<T>);
        private FlowLayoutPanel flowLayoutPanel3;
        private SimpleButton sBbackward;
        private LabelControl labelControl2;
        private SimpleButton sBforward;
        private ICatalogManager<T> catalogManager;
        private PageSelector pageSelector;



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridEditor = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.sBbackward = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.sBforward = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridEditor
            // 
            this.gridEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEditor.DataSource = this.bindingSource1;
            this.gridEditor.Location = new System.Drawing.Point(0, 0);
            this.gridEditor.MainView = this.gridView;
            this.gridEditor.Name = "gridEditor";
            this.gridEditor.Size = new System.Drawing.Size(658, 403);
            this.gridEditor.TabIndex = 0;
            this.gridEditor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridEditor;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridView.ShowingPopupEditForm += new DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventHandler(this.ShowingPopUpEditor);
            this.gridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.GridRowObjectChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(583, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(658, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 432);
            this.barDockControlBottom.Size = new System.Drawing.Size(658, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 432);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(658, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 432);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel3.Controls.Add(this.sBbackward);
            this.flowLayoutPanel3.Controls.Add(this.labelControl2);
            this.flowLayoutPanel3.Controls.Add(this.sBforward);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 406);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(196, 26);
            this.flowLayoutPanel3.TabIndex = 23;
            // 
            // sBbackward
            // 
            this.sBbackward.Location = new System.Drawing.Point(3, 3);
            this.sBbackward.Name = "sBbackward";
            this.sBbackward.Size = new System.Drawing.Size(43, 15);
            this.sBbackward.TabIndex = 15;
            this.sBbackward.Text = "<<";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(52, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "Pagination";
            // 
            // sBforward
            // 
            this.sBforward.Location = new System.Drawing.Point(108, 3);
            this.sBforward.Name = "sBforward";
            this.sBforward.Size = new System.Drawing.Size(43, 15);
            this.sBforward.TabIndex = 16;
            this.sBforward.Text = ">>";
            // 
            // DialogEditorForm
            // 
            this.ClientSize = new System.Drawing.Size(658, 432);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridEditor);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DialogEditorForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

namespace DXLibraryClient.Views
{
    partial class ReaderCartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cartViewrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cartViewrepositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cartViewrepositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.inventoryGrid = new DevExpress.XtraGrid.GridControl();
            this.ItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inventoryView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bItemClose = new DevExpress.XtraBars.BarButtonItem();
            this.bIOrdered = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.biRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bIRemoveItem = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // cartViewrepositoryItemLookUpEdit
            // 
            this.cartViewrepositoryItemLookUpEdit.AutoHeight = false;
            this.cartViewrepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cartViewrepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Book Description"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ISBN", "Book ISBN"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PenaltyPrice", "Book PenaltyPrice")});
            this.cartViewrepositoryItemLookUpEdit.DisplayMember = "Description";
            this.cartViewrepositoryItemLookUpEdit.Name = "cartViewrepositoryItemLookUpEdit";
            this.cartViewrepositoryItemLookUpEdit.NullText = "";
            this.cartViewrepositoryItemLookUpEdit.ValueMember = "ID";
            // 
            // cartViewrepositoryItemCheckEdit1
            // 
            this.cartViewrepositoryItemCheckEdit1.AutoHeight = false;
            this.cartViewrepositoryItemCheckEdit1.Name = "cartViewrepositoryItemCheckEdit1";
            // 
            // cartViewrepositoryItemCheckEdit2
            // 
            this.cartViewrepositoryItemCheckEdit2.AutoHeight = false;
            this.cartViewrepositoryItemCheckEdit2.Name = "cartViewrepositoryItemCheckEdit2";
            // 
            // inventoryGrid
            // 
            this.inventoryGrid.DataSource = this.ItemBindingSource;
            this.inventoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inventoryGrid.Location = new System.Drawing.Point(0, 29);
            this.inventoryGrid.MainView = this.inventoryView;
            this.inventoryGrid.Name = "inventoryGrid";
            this.inventoryGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cartViewrepositoryItemLookUpEdit,
            this.cartViewrepositoryItemCheckEdit1,
            this.cartViewrepositoryItemCheckEdit2});
            this.inventoryGrid.Size = new System.Drawing.Size(746, 414);
            this.inventoryGrid.TabIndex = 2;
            this.inventoryGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.inventoryView});
            // 
            // ItemBindingSource
            // 
            this.ItemBindingSource.DataSource = typeof(Enterprise.Model.ItemModel);
            // 
            // inventoryView
            // 
            this.inventoryView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.inventoryView.GridControl = this.inventoryGrid;
            this.inventoryView.Name = "inventoryView";
            this.inventoryView.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.inventoryView.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Cached;
            this.inventoryView.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.inventoryView.OptionsSelection.MultiSelect = true;
            this.inventoryView.Tag = "inventoryView";
            this.inventoryView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.inventoryViewFocusedRowObjectChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "IremID";
            this.gridColumn1.CustomizationCaption = "ItemID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Book";
            this.gridColumn2.ColumnEdit = this.cartViewrepositoryItemLookUpEdit;
            this.gridColumn2.FieldName = "BookId";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "IsOrdered";
            this.gridColumn7.ColumnEdit = this.cartViewrepositoryItemCheckEdit1;
            this.gridColumn7.FieldName = "IsOrdered";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "IsReadCarted";
            this.gridColumn8.ColumnEdit = this.cartViewrepositoryItemCheckEdit2;
            this.gridColumn8.FieldName = "IsReadCarted";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Inventory SerialCode";
            this.gridColumn9.FieldName = "InventorySerialCode";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biRefresh,
            this.bItemClose,
            this.bIOrdered,
            this.bIRemoveItem});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.bIOrdered)});
            this.bar1.Text = "Tools";
            // 
            // bItemClose
            // 
            this.bItemClose.Caption = "Close";
            this.bItemClose.Id = 1;
            this.bItemClose.Name = "bItemClose";
            // 
            // bIOrdered
            // 
            this.bIOrdered.Caption = "Order";
            this.bIOrdered.Id = 2;
            this.bIOrdered.Name = "bIOrdered";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(746, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 443);
            this.barDockControlBottom.Size = new System.Drawing.Size(746, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 414);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(746, 29);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 414);
            // 
            // biRefresh
            // 
            this.biRefresh.Caption = "Refresh";
            this.biRefresh.Id = 0;
            this.biRefresh.Name = "biRefresh";
            // 
            // bIRemoveItem
            // 
            this.bIRemoveItem.Caption = "Remove Item";
            this.bIRemoveItem.Id = 3;
            this.bIRemoveItem.Name = "bIRemoveItem";
            // 
            // ReaderCartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 443);
            this.Controls.Add(this.inventoryGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ReaderCartForm";
            this.Text = "ReaderCartForm";
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartViewrepositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl inventoryGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView inventoryView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cartViewrepositoryItemLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cartViewrepositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cartViewrepositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.BindingSource ItemBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem biRefresh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bItemClose;
        private DevExpress.XtraBars.BarButtonItem bIOrdered;
        private DevExpress.XtraBars.BarButtonItem bIRemoveItem;
    }
}
namespace DXLibraryClient.Views
{
    partial class SearchLookUpEditorForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.publisherModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sLookUpEdit2 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sLookUpEdit3 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.bookModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPenaltyPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublisher = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCheked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublisherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecoveredDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanedRecoveringDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sBRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.publisherModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sLookUpEdit1
            // 
            this.sLookUpEdit1.EditValue = "";
            this.sLookUpEdit1.Location = new System.Drawing.Point(3, 22);
            this.sLookUpEdit1.Name = "sLookUpEdit1";
            this.sLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUpEdit1.Properties.DataSource = this.publisherModelBindingSource;
            this.sLookUpEdit1.Properties.DisplayMember = "Title";
            this.sLookUpEdit1.Properties.ValueMember = "ID";
            this.sLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.sLookUpEdit1.Size = new System.Drawing.Size(202, 20);
            this.sLookUpEdit1.TabIndex = 0;
            this.sLookUpEdit1.ButtonClick += sLookUpEdit1_ButtonClick;

            // 
            // publisherModelBindingSource
            // 
            this.publisherModelBindingSource.DataSource = typeof(Enterprise.Model.PublisherModel);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.Tag = "";
            // 
            // sLookUpEdit2
            // 
            this.sLookUpEdit2.EditValue = "  ";
            this.sLookUpEdit2.Location = new System.Drawing.Point(3, 48);
            this.sLookUpEdit2.Name = "sLookUpEdit2";
            this.sLookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUpEdit2.Properties.View = this.gridView1;
            this.sLookUpEdit2.Size = new System.Drawing.Size(202, 20);
            this.sLookUpEdit2.TabIndex = 1;
            this.sLookUpEdit2.EditValueChanged += new System.EventHandler(this.sLookUpEdit2_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // sLookUpEdit3
            // 
            this.sLookUpEdit3.EditValue = " ";
            this.sLookUpEdit3.Location = new System.Drawing.Point(3, 74);
            this.sLookUpEdit3.Name = "sLookUpEdit3";
            this.sLookUpEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sLookUpEdit3.Properties.DataSource = this.bookModelBindingSource;
            this.sLookUpEdit3.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.sLookUpEdit3.Properties.View = this.gridView2;
            this.sLookUpEdit3.Size = new System.Drawing.Size(202, 20);
            this.sLookUpEdit3.TabIndex = 2;
            // 
            // bookModelBindingSource
            // 
            this.bookModelBindingSource.DataSource = typeof(Enterprise.Model.BookModel);
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DataSource = this.publisherModelBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "Title";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ValueMember = "ID";
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDescription,
            this.colISBN,
            this.colPenaltyPrice,
            this.colPublisher,
            this.colIsCheked,
            this.colPublisherId,
            this.colDetailCheck,
            this.colRecoveredDate,
            this.colPlanedRecoveringDate,
            this.colID});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 0;
            // 
            // colISBN
            // 
            this.colISBN.FieldName = "ISBN";
            this.colISBN.Name = "colISBN";
            this.colISBN.Visible = true;
            this.colISBN.VisibleIndex = 1;
            // 
            // colPenaltyPrice
            // 
            this.colPenaltyPrice.FieldName = "PenaltyPrice";
            this.colPenaltyPrice.Name = "colPenaltyPrice";
            this.colPenaltyPrice.Visible = true;
            this.colPenaltyPrice.VisibleIndex = 2;
            // 
            // colPublisher
            // 
            this.colPublisher.Caption = "Publisher";
            this.colPublisher.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colPublisher.FieldName = "PublisherId";
            this.colPublisher.Name = "colPublisher";
            this.colPublisher.Visible = true;
            this.colPublisher.VisibleIndex = 3;
            // 
            // colIsCheked
            // 
            this.colIsCheked.FieldName = "IsCheked";
            this.colIsCheked.Name = "colIsCheked";
            // 
            // colPublisherId
            // 
            this.colPublisherId.FieldName = "PublisherId";
            this.colPublisherId.Name = "colPublisherId";
            this.colPublisherId.OptionsColumn.ReadOnly = true;
            // 
            // colDetailCheck
            // 
            this.colDetailCheck.FieldName = "DetailCheck";
            this.colDetailCheck.Name = "colDetailCheck";
            // 
            // colRecoveredDate
            // 
            this.colRecoveredDate.FieldName = "RecoveredDate";
            this.colRecoveredDate.Name = "colRecoveredDate";
            // 
            // colPlanedRecoveringDate
            // 
            this.colPlanedRecoveringDate.FieldName = "PlanedRecoveringDate";
            this.colPlanedRecoveringDate.Name = "colPlanedRecoveringDate";
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelControl1);
            this.flowLayoutPanel1.Controls.Add(this.sLookUpEdit1);
            this.flowLayoutPanel1.Controls.Add(this.sLookUpEdit2);
            this.flowLayoutPanel1.Controls.Add(this.sLookUpEdit3);
            this.flowLayoutPanel1.Controls.Add(this.sBRefresh);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(232, 140);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "MasterDetail Search";
            // 
            // sBRefresh
            // 
            this.sBRefresh.Location = new System.Drawing.Point(3, 100);
            this.sBRefresh.Name = "sBRefresh";
            this.sBRefresh.Size = new System.Drawing.Size(61, 22);
            this.sBRefresh.TabIndex = 7;
            this.sBRefresh.Text = "Refresh";
            this.sBRefresh.Click += new System.EventHandler(this.sBRefresh_Click);
            // 
            // SearchLookUpEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SearchLookUpEditorForm";
            this.Size = new System.Drawing.Size(232, 140);
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.publisherModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sLookUpEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        

        

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit sLookUpEdit3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit sLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton sBRefresh;
        private System.Windows.Forms.BindingSource bookModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colISBN;
        private DevExpress.XtraGrid.Columns.GridColumn colPenaltyPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPublisher;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCheked;
        private DevExpress.XtraGrid.Columns.GridColumn colPublisherId;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colRecoveredDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanedRecoveringDate;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.BindingSource publisherModelBindingSource;
    }
}

namespace ADD9902E
{
    partial class ADD9902E_C
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
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAddHx0 = new System.Windows.Forms.Button();
            this.btnAddHx1 = new System.Windows.Forms.Button();
            this.btnAddHx2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 45);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(990, 483);
            this.grdMain.TabIndex = 43;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "적용일자";
            this.gridColumn1.FieldName = "MST4CD";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "종별";
            this.gridColumn2.FieldName = "JONG";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 35;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "종별명칭";
            this.gridColumn3.FieldName = "JONGNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "요양기관기호";
            this.gridColumn4.FieldName = "HOSID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 85;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "산재지정기호";
            this.gridColumn5.FieldName = "HOSID2";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 85;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "대표자";
            this.gridColumn6.FieldName = "HOSCEO";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "대표자주민번호";
            this.gridColumn7.FieldName = "HOSCEORID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 110;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "작성자";
            this.gridColumn8.FieldName = "WORKNM";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "작성자주민번호";
            this.gridColumn9.FieldName = "WORKRID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 110;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "산재청구인";
            this.gridColumn10.FieldName = "DEMNMSANJE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 44;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnAddHx0
            // 
            this.btnAddHx0.Location = new System.Drawing.Point(213, 12);
            this.btnAddHx0.Name = "btnAddHx0";
            this.btnAddHx0.Size = new System.Drawing.Size(138, 23);
            this.btnAddHx0.TabIndex = 45;
            this.btnAddHx0.Text = "요양기관기호 추가";
            this.btnAddHx0.UseVisualStyleBackColor = true;
            this.btnAddHx0.Click += new System.EventHandler(this.btnAddHx0_Click);
            // 
            // btnAddHx1
            // 
            this.btnAddHx1.Location = new System.Drawing.Point(357, 12);
            this.btnAddHx1.Name = "btnAddHx1";
            this.btnAddHx1.Size = new System.Drawing.Size(138, 23);
            this.btnAddHx1.TabIndex = 46;
            this.btnAddHx1.Text = "대표자 추가";
            this.btnAddHx1.UseVisualStyleBackColor = true;
            this.btnAddHx1.Click += new System.EventHandler(this.btnAddHx1_Click);
            // 
            // btnAddHx2
            // 
            this.btnAddHx2.Location = new System.Drawing.Point(501, 12);
            this.btnAddHx2.Name = "btnAddHx2";
            this.btnAddHx2.Size = new System.Drawing.Size(138, 23);
            this.btnAddHx2.TabIndex = 47;
            this.btnAddHx2.Text = "작성자 추가";
            this.btnAddHx2.UseVisualStyleBackColor = true;
            this.btnAddHx2.Click += new System.EventHandler(this.btnAddHx2_Click);
            // 
            // ADD9902E_C
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 545);
            this.Controls.Add(this.btnAddHx2);
            this.Controls.Add(this.btnAddHx1);
            this.Controls.Add(this.btnAddHx0);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD9902E_C";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "요양기관이력(ADD9902E_C)";
            this.Shown += new System.EventHandler(this.ADD9902E_C_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnAddHx0;
        private System.Windows.Forms.Button btnAddHx1;
        private System.Windows.Forms.Button btnAddHx2;
    }
}
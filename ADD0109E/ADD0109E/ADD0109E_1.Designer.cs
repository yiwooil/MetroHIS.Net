namespace ADD0109E
{
    partial class ADD0109E_1
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
            this.txtPricd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrknm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPRICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRKNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPricd
            // 
            this.txtPricd.Location = new System.Drawing.Point(76, 6);
            this.txtPricd.Name = "txtPricd";
            this.txtPricd.Size = new System.Drawing.Size(82, 21);
            this.txtPricd.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "수가코드 :";
            // 
            // txtPrknm
            // 
            this.txtPrknm.Location = new System.Drawing.Point(76, 31);
            this.txtPrknm.Name = "txtPrknm";
            this.txtPrknm.Size = new System.Drawing.Size(267, 21);
            this.txtPrknm.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "수가명칭 :";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(14, 58);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(469, 284);
            this.grdMain.TabIndex = 13;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView,
            this.gridView2});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPRICD,
            this.gcPRKNM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcPRICD
            // 
            this.gcPRICD.Caption = "수가코드";
            this.gcPRICD.FieldName = "PRICD";
            this.gcPRICD.Name = "gcPRICD";
            this.gcPRICD.Visible = true;
            this.gcPRICD.VisibleIndex = 0;
            // 
            // gcPRKNM
            // 
            this.gcPRKNM.Caption = "수가명";
            this.gcPRKNM.FieldName = "PRKNM";
            this.gcPRKNM.Name = "gcPRKNM";
            this.gcPRKNM.OptionsColumn.AllowEdit = false;
            this.gcPRKNM.Visible = true;
            this.gcPRKNM.VisibleIndex = 1;
            this.gcPRKNM.Width = 350;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdMain;
            this.gridView2.Name = "gridView2";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(408, 29);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 73;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ADD0109E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 351);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtPrknm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPricd);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD0109E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "수가코드검색(ADD0109E_1)";
            this.Load += new System.EventHandler(this.ADD0109E_1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPricd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrknm;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRKNM;
    }
}
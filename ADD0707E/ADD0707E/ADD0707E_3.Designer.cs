namespace ADD0707E
{
    partial class ADD0707E_3
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtBussnm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusscd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdBuss = new DevExpress.XtraGrid.GridControl();
            this.grdBussView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBussView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.Location = new System.Drawing.Point(202, 356);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 96;
            this.btnDel.Text = "삭제";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(202, 331);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 95;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtBussnm
            // 
            this.txtBussnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBussnm.Location = new System.Drawing.Point(76, 356);
            this.txtBussnm.Name = "txtBussnm";
            this.txtBussnm.Size = new System.Drawing.Size(117, 21);
            this.txtBussnm.TabIndex = 93;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 94;
            this.label2.Text = "사업자상호";
            // 
            // txtBusscd
            // 
            this.txtBusscd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBusscd.Location = new System.Drawing.Point(76, 332);
            this.txtBusscd.Name = "txtBusscd";
            this.txtBusscd.Size = new System.Drawing.Size(117, 21);
            this.txtBusscd.TabIndex = 91;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 92;
            this.label3.Text = "사업자번호";
            // 
            // grdBuss
            // 
            this.grdBuss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBuss.Location = new System.Drawing.Point(10, 10);
            this.grdBuss.MainView = this.grdBussView;
            this.grdBuss.Name = "grdBuss";
            this.grdBuss.Size = new System.Drawing.Size(265, 311);
            this.grdBuss.TabIndex = 90;
            this.grdBuss.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdBussView});
            // 
            // grdBussView
            // 
            this.grdBussView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9});
            this.grdBussView.GridControl = this.grdBuss;
            this.grdBussView.Name = "grdBussView";
            this.grdBussView.OptionsView.ColumnAutoWidth = false;
            this.grdBussView.OptionsView.ShowGroupPanel = false;
            this.grdBussView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdBussView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdBussView_RowCellClick);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "사업자번호";
            this.gridColumn8.FieldName = "BUSINESSCD";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 90;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "구입기관상호";
            this.gridColumn9.FieldName = "TRADENM";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 130;
            // 
            // ADD0707E_3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 384);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBussnm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBusscd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdBuss);
            this.Name = "ADD0707E_3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "거래처등록(ADD0707E_3)";
            this.Load += new System.EventHandler(this.ADD0707E_3_Load);
            this.Activated += new System.EventHandler(this.ADD0707E_3_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdBuss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBussView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtBussnm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBusscd;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl grdBuss;
        private DevExpress.XtraGrid.Views.Grid.GridView grdBussView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
    }
}
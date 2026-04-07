namespace ADD0707E
{
    partial class ADD0707E_2
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
            this.txtBusscd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdBuss = new DevExpress.XtraGrid.GridControl();
            this.grdBussView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBussView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBusscd
            // 
            this.txtBusscd.Location = new System.Drawing.Point(80, 12);
            this.txtBusscd.Name = "txtBusscd";
            this.txtBusscd.Size = new System.Drawing.Size(86, 21);
            this.txtBusscd.TabIndex = 81;
            this.txtBusscd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusscd_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 80;
            this.label1.Text = "사업자번호";
            // 
            // grdBuss
            // 
            this.grdBuss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBuss.Location = new System.Drawing.Point(10, 39);
            this.grdBuss.MainView = this.grdBussView;
            this.grdBuss.Name = "grdBuss";
            this.grdBuss.Size = new System.Drawing.Size(265, 319);
            this.grdBuss.TabIndex = 83;
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
            this.grdBussView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdBussView_KeyPress);
            this.grdBussView.DoubleClick += new System.EventHandler(this.grdBussView_DoubleClick);
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
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(172, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 116;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ADD0707E_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 369);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdBuss);
            this.Controls.Add(this.txtBusscd);
            this.Controls.Add(this.label1);
            this.Name = "ADD0707E_2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "구입기관(ADD0707E_2)";
            this.Load += new System.EventHandler(this.ADD0707E_2_Load);
            this.Activated += new System.EventHandler(this.ADD0707E_2_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdBuss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBussView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusscd;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl grdBuss;
        private DevExpress.XtraGrid.Views.Grid.GridView grdBussView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.Button btnQuery;
    }
}
namespace ADD0707E
{
    partial class ADD0707E_4
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
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grdMainHx = new DevExpress.XtraGrid.GridControl();
            this.grdMainHxView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainHx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainHxView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(12, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(86, 21);
            this.txtCode.TabIndex = 84;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(102, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(273, 21);
            this.txtName.TabIndex = 85;
            // 
            // grdMainHx
            // 
            this.grdMainHx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMainHx.Location = new System.Drawing.Point(12, 39);
            this.grdMainHx.MainView = this.grdMainHxView;
            this.grdMainHx.Name = "grdMainHx";
            this.grdMainHx.Size = new System.Drawing.Size(444, 378);
            this.grdMainHx.TabIndex = 123;
            this.grdMainHx.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainHxView});
            // 
            // grdMainHxView
            // 
            this.grdMainHxView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn21,
            this.gridColumn32,
            this.gridColumn33,
            this.gridColumn34,
            this.gridColumn22});
            this.grdMainHxView.GridControl = this.grdMainHx;
            this.grdMainHxView.Name = "grdMainHxView";
            this.grdMainHxView.OptionsView.ColumnAutoWidth = false;
            this.grdMainHxView.OptionsView.ShowGroupPanel = false;
            this.grdMainHxView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "신청번호";
            this.gridColumn21.FieldName = "DEMNO";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 0;
            this.gridColumn21.Width = 100;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "구입일자";
            this.gridColumn32.FieldName = "BUYDT";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.AllowEdit = false;
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 1;
            this.gridColumn32.Width = 65;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "구입량";
            this.gridColumn33.DisplayFormat.FormatString = "#,##0";
            this.gridColumn33.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn33.FieldName = "BUYQTY";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.AllowEdit = false;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 2;
            this.gridColumn33.Width = 50;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "구입가";
            this.gridColumn34.DisplayFormat.FormatString = "#,##0";
            this.gridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn34.FieldName = "BUYAMT";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.OptionsColumn.AllowEdit = false;
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 3;
            this.gridColumn34.Width = 70;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "상호명";
            this.gridColumn22.FieldName = "TRADENM";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 4;
            this.gridColumn22.Width = 120;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(381, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 124;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ADD0707E_4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 429);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMainHx);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Name = "ADD0707E_4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "신고History조회(ADD0707E_4)";
            this.Load += new System.EventHandler(this.ADD0707E_4_Load);
            this.Activated += new System.EventHandler(this.ADD0707E_4_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMainHx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainHxView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private DevExpress.XtraGrid.GridControl grdMainHx;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainHxView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private System.Windows.Forms.Button btnQuery;
    }
}
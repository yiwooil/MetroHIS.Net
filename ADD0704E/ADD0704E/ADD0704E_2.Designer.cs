namespace ADD0704E
{
    partial class ADD0704E_2
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
            this.grdTI09 = new DevExpress.XtraGrid.GridControl();
            this.grdTI09View = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.txtEdicd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuydt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chkByak = new System.Windows.Forms.CheckBox();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdTI09)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTI09View)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTI09
            // 
            this.grdTI09.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTI09.Location = new System.Drawing.Point(12, 39);
            this.grdTI09.MainView = this.grdTI09View;
            this.grdTI09.Name = "grdTI09";
            this.grdTI09.Size = new System.Drawing.Size(802, 349);
            this.grdTI09.TabIndex = 0;
            this.grdTI09.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdTI09View});
            // 
            // grdTI09View
            // 
            this.grdTI09View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.grdTI09View.GridControl = this.grdTI09;
            this.grdTI09View.Name = "grdTI09View";
            this.grdTI09View.OptionsView.ColumnAutoWidth = false;
            this.grdTI09View.OptionsView.ShowGroupPanel = false;
            this.grdTI09View.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdTI09View.DoubleClick += new System.EventHandler(this.grdTI09View_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "EDI코드";
            this.gridColumn1.FieldName = "PCODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "적용일";
            this.gridColumn2.FieldName = "ADTDT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "코드명";
            this.gridColumn3.FieldName = "PCODENM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 140;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "제조업체명";
            this.gridColumn4.FieldName = "MKCNM";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "수입업소명";
            this.gridColumn5.FieldName = "MKCNMK";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "규격";
            this.gridColumn6.FieldName = "PTYPE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 50;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "단위";
            this.gridColumn7.FieldName = "PDUT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 50;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "금액";
            this.gridColumn8.FieldName = "KUMAK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "비고";
            this.gridColumn9.FieldName = "RMK";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "수가코드";
            this.gridColumn10.FieldName = "PRICD";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            // 
            // txtEdicd
            // 
            this.txtEdicd.Location = new System.Drawing.Point(66, 12);
            this.txtEdicd.Name = "txtEdicd";
            this.txtEdicd.Size = new System.Drawing.Size(97, 21);
            this.txtEdicd.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 37;
            this.label2.Text = "EDI코드";
            // 
            // txtBuydt
            // 
            this.txtBuydt.Location = new System.Drawing.Point(743, 12);
            this.txtBuydt.Name = "txtBuydt";
            this.txtBuydt.Size = new System.Drawing.Size(71, 21);
            this.txtBuydt.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(687, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "기준일자";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(169, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 40;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "<- 코드를 두 자 이상 입력하여야 검색합니다.";
            // 
            // chkByak
            // 
            this.chkByak.AutoSize = true;
            this.chkByak.Location = new System.Drawing.Point(250, 16);
            this.chkByak.Name = "chkByak";
            this.chkByak.Size = new System.Drawing.Size(84, 16);
            this.chkByak.TabIndex = 42;
            this.chkByak.Text = "비급여약제";
            this.chkByak.UseVisualStyleBackColor = true;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "수가금액";
            this.gridColumn11.FieldName = "IPAMT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            // 
            // ADD0704E_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 400);
            this.Controls.Add(this.chkByak);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtBuydt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEdicd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdTI09);
            this.Name = "ADD0704E_2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "의약품조회(ADD0704E_2)";
            this.Load += new System.EventHandler(this.ADD0704E_2_Load);
            this.Activated += new System.EventHandler(this.ADD0704E_2_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdTI09)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTI09View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdTI09;
        private DevExpress.XtraGrid.Views.Grid.GridView grdTI09View;
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
        private System.Windows.Forms.TextBox txtEdicd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuydt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkByak;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    }
}
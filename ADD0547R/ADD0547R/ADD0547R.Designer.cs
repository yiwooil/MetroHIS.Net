namespace ADD0547R
{
    partial class ADD0547R
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbIofg2 = new System.Windows.Forms.RadioButton();
            this.rbIofg1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.chkComp = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy51 = new System.Windows.Forms.RadioButton();
            this.rbQfy5 = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboDemgb = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbIofg2);
            this.panel1.Controls.Add(this.rbIofg1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 23);
            this.panel1.TabIndex = 0;
            // 
            // rbIofg2
            // 
            this.rbIofg2.AutoSize = true;
            this.rbIofg2.Location = new System.Drawing.Point(56, 3);
            this.rbIofg2.Name = "rbIofg2";
            this.rbIofg2.Size = new System.Drawing.Size(47, 16);
            this.rbIofg2.TabIndex = 1;
            this.rbIofg2.Text = "입원";
            this.rbIofg2.UseVisualStyleBackColor = true;
            this.rbIofg2.CheckedChanged += new System.EventHandler(this.rbIofg2_CheckedChanged);
            // 
            // rbIofg1
            // 
            this.rbIofg1.AutoSize = true;
            this.rbIofg1.Checked = true;
            this.rbIofg1.Location = new System.Drawing.Point(8, 3);
            this.rbIofg1.Name = "rbIofg1";
            this.rbIofg1.Size = new System.Drawing.Size(47, 16);
            this.rbIofg1.TabIndex = 0;
            this.rbIofg1.TabStop = true;
            this.rbIofg1.Text = "외래";
            this.rbIofg1.UseVisualStyleBackColor = true;
            this.rbIofg1.CheckedChanged += new System.EventHandler(this.rbIofg1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "청구월";
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(177, 14);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(60, 21);
            this.txtFrdt.TabIndex = 1;
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(239, 14);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(60, 21);
            this.txtTodt.TabIndex = 2;
            // 
            // chkComp
            // 
            this.chkComp.AutoSize = true;
            this.chkComp.Location = new System.Drawing.Point(304, 17);
            this.chkComp.Name = "chkComp";
            this.chkComp.Size = new System.Drawing.Size(100, 16);
            this.chkComp.TabIndex = 3;
            this.chkComp.Text = "완료일로 조회";
            this.chkComp.UseVisualStyleBackColor = true;
            this.chkComp.CheckedChanged += new System.EventHandler(this.chkComp_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbQfy6);
            this.panel2.Controls.Add(this.rbQfy51);
            this.panel2.Controls.Add(this.rbQfy5);
            this.panel2.Location = new System.Drawing.Point(410, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 23);
            this.panel2.TabIndex = 4;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(128, 2);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 2;
            this.rbQfy6.Text = "자보";
            this.rbQfy6.UseVisualStyleBackColor = true;
            this.rbQfy6.CheckedChanged += new System.EventHandler(this.rbQfy6_CheckedChanged);
            // 
            // rbQfy51
            // 
            this.rbQfy51.AutoSize = true;
            this.rbQfy51.Location = new System.Drawing.Point(56, 3);
            this.rbQfy51.Name = "rbQfy51";
            this.rbQfy51.Size = new System.Drawing.Size(71, 16);
            this.rbQfy51.TabIndex = 1;
            this.rbQfy51.Text = "산재후유";
            this.rbQfy51.UseVisualStyleBackColor = true;
            this.rbQfy51.CheckedChanged += new System.EventHandler(this.rbQfy51_CheckedChanged);
            // 
            // rbQfy5
            // 
            this.rbQfy5.AutoSize = true;
            this.rbQfy5.Checked = true;
            this.rbQfy5.Location = new System.Drawing.Point(8, 3);
            this.rbQfy5.Name = "rbQfy5";
            this.rbQfy5.Size = new System.Drawing.Size(47, 16);
            this.rbQfy5.TabIndex = 0;
            this.rbQfy5.TabStop = true;
            this.rbQfy5.Text = "산재";
            this.rbQfy5.UseVisualStyleBackColor = true;
            this.rbQfy5.CheckedChanged += new System.EventHandler(this.rbQfy5_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(802, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(881, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(79, 23);
            this.btnExcel.TabIndex = 8;
            this.btnExcel.Text = "엑셀";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(723, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 41);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(991, 404);
            this.grdMain.TabIndex = 9;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn11,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.grdMainView.OptionsSelection.MultiSelect = true;
            this.grdMainView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "명일련";
            this.gridColumn1.FieldName = "EPRTNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 45;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "환자ID";
            this.gridColumn2.FieldName = "PID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "환자명";
            this.gridColumn3.FieldName = "PNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "주민번호";
            this.gridColumn11.FieldName = "RESID";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "재해발생일";
            this.gridColumn4.FieldName = "GENDT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "진료기간(from)";
            this.gridColumn5.FieldName = "STARTDT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 95;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "진료기간(to)";
            this.gridColumn6.FieldName = "ENDDT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "사업장기호";
            this.gridColumn7.FieldName = "UNICD";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 95;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "사고접수번호";
            this.gridColumn8.FieldName = "APPRNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 145;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "사업체명";
            this.gridColumn9.FieldName = "UNINM";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 100;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "청구액";
            this.gridColumn10.DisplayFormat.FormatString = "#,##0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "UNAMT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 95;
            // 
            // cboDemgb
            // 
            this.cboDemgb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDemgb.FormattingEnabled = true;
            this.cboDemgb.Items.AddRange(new object[] {
            "전체",
            "원청구",
            "보완청구",
            "추가청구",
            "약제추가"});
            this.cboDemgb.Location = new System.Drawing.Point(603, 13);
            this.cboDemgb.Name = "cboDemgb";
            this.cboDemgb.Size = new System.Drawing.Size(82, 20);
            this.cboDemgb.TabIndex = 5;
            this.cboDemgb.SelectedIndexChanged += new System.EventHandler(this.cboDemgb_SelectedIndexChanged);
            // 
            // ADD0547R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 452);
            this.Controls.Add(this.cboDemgb);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtFrdt);
            this.Controls.Add(this.txtTodt);
            this.Controls.Add(this.chkComp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "ADD0547R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "산재자보집계표(ADD0547R)";
            this.Load += new System.EventHandler(this.ADD0547R_Load);
            this.Activated += new System.EventHandler(this.ADD0547R_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbIofg2;
        private System.Windows.Forms.RadioButton rbIofg1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.CheckBox chkComp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.RadioButton rbQfy51;
        private System.Windows.Forms.RadioButton rbQfy5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnQuery;
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
        private System.Windows.Forms.ComboBox cboDemgb;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    }
}


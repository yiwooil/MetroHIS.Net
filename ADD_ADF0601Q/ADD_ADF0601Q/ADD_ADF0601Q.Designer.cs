namespace ADD_ADF0601Q
{
    partial class ADD_ADF0601Q
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
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.cboBededt = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPDRID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXDRID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcISPCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRINM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHRLT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUTAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTTAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXMM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(61, 12);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(86, 21);
            this.txtPid.TabIndex = 5;
            this.txtPid.TextChanged += new System.EventHandler(this.txtPid_TextChanged);
            this.txtPid.Leave += new System.EventHandler(this.txtPid_Leave);
            this.txtPid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPid_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "환자ID :";
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(147, 12);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.ReadOnly = true;
            this.txtPnm.Size = new System.Drawing.Size(86, 21);
            this.txtPnm.TabIndex = 7;
            // 
            // cboBededt
            // 
            this.cboBededt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBededt.FormattingEnabled = true;
            this.cboBededt.Location = new System.Drawing.Point(295, 13);
            this.cboBededt.Name = "cboBededt";
            this.cboBededt.Size = new System.Drawing.Size(121, 20);
            this.cboBededt.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "입원일 :";
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(471, 13);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(86, 21);
            this.txtFrdt.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "기간 :";
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(559, 13);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(86, 21);
            this.txtTodt.TabIndex = 20;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(801, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 72;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(13, 40);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(941, 462);
            this.grdMain.TabIndex = 73;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPDRID,
            this.gcPDRNM,
            this.gcEXDRID,
            this.gcEXDRNM,
            this.gcITEMNAME,
            this.gcPRICD,
            this.gcISPCD,
            this.gcPRINM,
            this.gcQFYCD,
            this.gcCHRLT,
            this.gcUTAMT,
            this.gcTQTY,
            this.gcTTAMT,
            this.gcEXMM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcPDRID
            // 
            this.gcPDRID.Caption = "주치의";
            this.gcPDRID.FieldName = "PDRID";
            this.gcPDRID.Name = "gcPDRID";
            this.gcPDRID.OptionsColumn.AllowEdit = false;
            this.gcPDRID.Visible = true;
            this.gcPDRID.VisibleIndex = 0;
            this.gcPDRID.Width = 85;
            // 
            // gcPDRNM
            // 
            this.gcPDRNM.Caption = "주치의명";
            this.gcPDRNM.FieldName = "PDRNM";
            this.gcPDRNM.Name = "gcPDRNM";
            this.gcPDRNM.OptionsColumn.AllowEdit = false;
            this.gcPDRNM.Visible = true;
            this.gcPDRNM.VisibleIndex = 1;
            this.gcPDRNM.Width = 65;
            // 
            // gcEXDRID
            // 
            this.gcEXDRID.Caption = "처방의";
            this.gcEXDRID.FieldName = "EXDRID";
            this.gcEXDRID.Name = "gcEXDRID";
            this.gcEXDRID.OptionsColumn.AllowEdit = false;
            this.gcEXDRID.Visible = true;
            this.gcEXDRID.VisibleIndex = 2;
            // 
            // gcEXDRNM
            // 
            this.gcEXDRNM.Caption = "처방의명";
            this.gcEXDRNM.FieldName = "EXDRNM";
            this.gcEXDRNM.Name = "gcEXDRNM";
            this.gcEXDRNM.OptionsColumn.AllowEdit = false;
            this.gcEXDRNM.Visible = true;
            this.gcEXDRNM.VisibleIndex = 3;
            this.gcEXDRNM.Width = 65;
            // 
            // gcITEMNAME
            // 
            this.gcITEMNAME.Caption = "항목";
            this.gcITEMNAME.FieldName = "ITEMNAME";
            this.gcITEMNAME.Name = "gcITEMNAME";
            this.gcITEMNAME.OptionsColumn.AllowEdit = false;
            this.gcITEMNAME.Visible = true;
            this.gcITEMNAME.VisibleIndex = 4;
            // 
            // gcPRICD
            // 
            this.gcPRICD.Caption = "수가코드";
            this.gcPRICD.FieldName = "PRICD";
            this.gcPRICD.Name = "gcPRICD";
            this.gcPRICD.OptionsColumn.AllowEdit = false;
            this.gcPRICD.Visible = true;
            this.gcPRICD.VisibleIndex = 5;
            // 
            // gcISPCD
            // 
            this.gcISPCD.Caption = "EDI코드";
            this.gcISPCD.FieldName = "ISPCD";
            this.gcISPCD.Name = "gcISPCD";
            this.gcISPCD.OptionsColumn.AllowEdit = false;
            this.gcISPCD.Visible = true;
            this.gcISPCD.VisibleIndex = 6;
            // 
            // gcPRINM
            // 
            this.gcPRINM.Caption = "수가명";
            this.gcPRINM.FieldName = "PRINM";
            this.gcPRINM.Name = "gcPRINM";
            this.gcPRINM.OptionsColumn.AllowEdit = false;
            this.gcPRINM.Visible = true;
            this.gcPRINM.VisibleIndex = 7;
            // 
            // gcQFYCD
            // 
            this.gcQFYCD.Caption = "자격";
            this.gcQFYCD.FieldName = "QFYCD";
            this.gcQFYCD.Name = "gcQFYCD";
            this.gcQFYCD.OptionsColumn.AllowEdit = false;
            this.gcQFYCD.Visible = true;
            this.gcQFYCD.VisibleIndex = 8;
            this.gcQFYCD.Width = 40;
            // 
            // gcCHRLT
            // 
            this.gcCHRLT.Caption = "부담";
            this.gcCHRLT.FieldName = "CHRLT";
            this.gcCHRLT.Name = "gcCHRLT";
            this.gcCHRLT.OptionsColumn.AllowEdit = false;
            this.gcCHRLT.Visible = true;
            this.gcCHRLT.VisibleIndex = 9;
            this.gcCHRLT.Width = 40;
            // 
            // gcUTAMT
            // 
            this.gcUTAMT.Caption = "단가";
            this.gcUTAMT.DisplayFormat.FormatString = "#,##0";
            this.gcUTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcUTAMT.FieldName = "UTAMT";
            this.gcUTAMT.Name = "gcUTAMT";
            this.gcUTAMT.OptionsColumn.AllowEdit = false;
            this.gcUTAMT.Visible = true;
            this.gcUTAMT.VisibleIndex = 10;
            // 
            // gcTQTY
            // 
            this.gcTQTY.Caption = "총량";
            this.gcTQTY.FieldName = "TQTY";
            this.gcTQTY.Name = "gcTQTY";
            this.gcTQTY.OptionsColumn.AllowEdit = false;
            this.gcTQTY.Visible = true;
            this.gcTQTY.VisibleIndex = 11;
            this.gcTQTY.Width = 55;
            // 
            // gcTTAMT
            // 
            this.gcTTAMT.Caption = "총액";
            this.gcTTAMT.DisplayFormat.FormatString = "#,##0";
            this.gcTTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcTTAMT.FieldName = "TTAMT";
            this.gcTTAMT.Name = "gcTTAMT";
            this.gcTTAMT.OptionsColumn.AllowEdit = false;
            this.gcTTAMT.Visible = true;
            this.gcTTAMT.VisibleIndex = 12;
            // 
            // gcEXMM
            // 
            this.gcEXMM.Caption = "월";
            this.gcEXMM.FieldName = "EXMM";
            this.gcEXMM.Name = "gcEXMM";
            this.gcEXMM.OptionsColumn.AllowEdit = false;
            this.gcEXMM.Visible = true;
            this.gcEXMM.VisibleIndex = 13;
            this.gcEXMM.Width = 55;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(879, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 74;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ADD_ADF0601Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 514);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtTodt);
            this.Controls.Add(this.txtFrdt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboBededt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label1);
            this.Name = "ADD_ADF0601Q";
            this.Text = "수가조견표(ADD_ADF0601Q)";
            this.Load += new System.EventHandler(this.ADD_ADF0601Q_Load);
            this.Activated += new System.EventHandler(this.ADD_ADF0601Q_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.ComboBox cboBededt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDRID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXDRID;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHRLT;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMNAME;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcISPCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRINM;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcUTAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcTTAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXMM;
    }
}


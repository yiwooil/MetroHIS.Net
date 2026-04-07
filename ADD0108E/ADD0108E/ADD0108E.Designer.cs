namespace ADD0108E
{
    partial class ADD0108E
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
            this.txtYYMM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcQFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXMM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcEXDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcFINDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcDPTCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBDIVFG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGONSGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDAETC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTTAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcISPAM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUISAM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRETYN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSUMYN = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYYMM
            // 
            this.txtYYMM.Location = new System.Drawing.Point(76, 10);
            this.txtYYMM.Name = "txtYYMM";
            this.txtYYMM.Size = new System.Drawing.Size(70, 21);
            this.txtYYMM.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "수납년월 :";
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.Location = new System.Drawing.Point(220, 12);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(153, 20);
            this.cboQfycd.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(157, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 12);
            this.label15.TabIndex = 38;
            this.label15.Text = "진료자격 :";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(831, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(750, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(14, 41);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(896, 385);
            this.grdMain.TabIndex = 3;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcQFYCD,
            this.gcEXMM,
            this.gcPID,
            this.gcPNM,
            this.tcEXDT,
            this.tcFINDT,
            this.tcDPTCD,
            this.gcBDIVFG,
            this.gcPDIV,
            this.gcPDIVNM,
            this.gcGONSGB,
            this.gcDAETC,
            this.gcTTAMT,
            this.gcUNAMT,
            this.gcISPAM,
            this.gcUISAM,
            this.gcRETYN,
            this.gcSUMYN});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcQFYCD
            // 
            this.gcQFYCD.Caption = "자격";
            this.gcQFYCD.FieldName = "QFYCD";
            this.gcQFYCD.Name = "gcQFYCD";
            this.gcQFYCD.OptionsColumn.ReadOnly = true;
            this.gcQFYCD.Visible = true;
            this.gcQFYCD.VisibleIndex = 0;
            this.gcQFYCD.Width = 35;
            // 
            // gcEXMM
            // 
            this.gcEXMM.Caption = "진료월";
            this.gcEXMM.FieldName = "EXMM";
            this.gcEXMM.Name = "gcEXMM";
            this.gcEXMM.OptionsColumn.ReadOnly = true;
            this.gcEXMM.Visible = true;
            this.gcEXMM.VisibleIndex = 1;
            this.gcEXMM.Width = 50;
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 2;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 3;
            // 
            // tcEXDT
            // 
            this.tcEXDT.Caption = "진료일자";
            this.tcEXDT.FieldName = "EXDT";
            this.tcEXDT.Name = "tcEXDT";
            this.tcEXDT.OptionsColumn.ReadOnly = true;
            this.tcEXDT.Visible = true;
            this.tcEXDT.VisibleIndex = 4;
            this.tcEXDT.Width = 65;
            // 
            // tcFINDT
            // 
            this.tcFINDT.Caption = "수납일자";
            this.tcFINDT.FieldName = "FINDT";
            this.tcFINDT.Name = "tcFINDT";
            this.tcFINDT.OptionsColumn.ReadOnly = true;
            this.tcFINDT.Visible = true;
            this.tcFINDT.VisibleIndex = 5;
            this.tcFINDT.Width = 65;
            // 
            // tcDPTCD
            // 
            this.tcDPTCD.Caption = "진료과";
            this.tcDPTCD.FieldName = "DPTCD";
            this.tcDPTCD.Name = "tcDPTCD";
            this.tcDPTCD.OptionsColumn.ReadOnly = true;
            this.tcDPTCD.Visible = true;
            this.tcDPTCD.VisibleIndex = 6;
            this.tcDPTCD.Width = 60;
            // 
            // gcBDIVFG
            // 
            this.gcBDIVFG.Caption = "응급";
            this.gcBDIVFG.FieldName = "BDIVFG";
            this.gcBDIVFG.Name = "gcBDIVFG";
            this.gcBDIVFG.OptionsColumn.ReadOnly = true;
            this.gcBDIVFG.Visible = true;
            this.gcBDIVFG.VisibleIndex = 7;
            this.gcBDIVFG.Width = 35;
            // 
            // gcPDIV
            // 
            this.gcPDIV.Caption = "구분";
            this.gcPDIV.FieldName = "PDIV";
            this.gcPDIV.Name = "gcPDIV";
            this.gcPDIV.OptionsColumn.ReadOnly = true;
            this.gcPDIV.Visible = true;
            this.gcPDIV.VisibleIndex = 8;
            this.gcPDIV.Width = 35;
            // 
            // gcPDIVNM
            // 
            this.gcPDIVNM.Caption = "구분명";
            this.gcPDIVNM.FieldName = "PDIVNM";
            this.gcPDIVNM.Name = "gcPDIVNM";
            this.gcPDIVNM.OptionsColumn.ReadOnly = true;
            this.gcPDIVNM.Visible = true;
            this.gcPDIVNM.VisibleIndex = 9;
            // 
            // gcGONSGB
            // 
            this.gcGONSGB.Caption = "공상";
            this.gcGONSGB.FieldName = "GONSGB";
            this.gcGONSGB.Name = "gcGONSGB";
            this.gcGONSGB.OptionsColumn.ReadOnly = true;
            this.gcGONSGB.Visible = true;
            this.gcGONSGB.VisibleIndex = 10;
            this.gcGONSGB.Width = 35;
            // 
            // gcDAETC
            // 
            this.gcDAETC.Caption = "상해";
            this.gcDAETC.FieldName = "DAETC";
            this.gcDAETC.Name = "gcDAETC";
            this.gcDAETC.OptionsColumn.ReadOnly = true;
            this.gcDAETC.Visible = true;
            this.gcDAETC.VisibleIndex = 11;
            this.gcDAETC.Width = 35;
            // 
            // gcTTAMT
            // 
            this.gcTTAMT.Caption = "총진료비";
            this.gcTTAMT.FieldName = "TTAMT";
            this.gcTTAMT.Name = "gcTTAMT";
            this.gcTTAMT.OptionsColumn.ReadOnly = true;
            this.gcTTAMT.Visible = true;
            this.gcTTAMT.VisibleIndex = 12;
            this.gcTTAMT.Width = 65;
            // 
            // gcUNAMT
            // 
            this.gcUNAMT.Caption = "조합부담";
            this.gcUNAMT.FieldName = "UNAMT";
            this.gcUNAMT.Name = "gcUNAMT";
            this.gcUNAMT.OptionsColumn.ReadOnly = true;
            this.gcUNAMT.Visible = true;
            this.gcUNAMT.VisibleIndex = 13;
            this.gcUNAMT.Width = 65;
            // 
            // gcISPAM
            // 
            this.gcISPAM.Caption = "본인부담";
            this.gcISPAM.FieldName = "ISPAM";
            this.gcISPAM.Name = "gcISPAM";
            this.gcISPAM.OptionsColumn.ReadOnly = true;
            this.gcISPAM.Visible = true;
            this.gcISPAM.VisibleIndex = 14;
            this.gcISPAM.Width = 65;
            // 
            // gcUISAM
            // 
            this.gcUISAM.Caption = "비급여";
            this.gcUISAM.FieldName = "UISAM";
            this.gcUISAM.Name = "gcUISAM";
            this.gcUISAM.OptionsColumn.ReadOnly = true;
            this.gcUISAM.Visible = true;
            this.gcUISAM.VisibleIndex = 15;
            this.gcUISAM.Width = 65;
            // 
            // gcRETYN
            // 
            this.gcRETYN.Caption = "환";
            this.gcRETYN.FieldName = "RETYN";
            this.gcRETYN.Name = "gcRETYN";
            this.gcRETYN.Visible = true;
            this.gcRETYN.VisibleIndex = 16;
            this.gcRETYN.Width = 35;
            // 
            // gcSUMYN
            // 
            this.gcSUMYN.Caption = "sum";
            this.gcSUMYN.FieldName = "SUMYN";
            this.gcSUMYN.Name = "gcSUMYN";
            this.gcSUMYN.Visible = true;
            this.gcSUMYN.VisibleIndex = 17;
            this.gcSUMYN.Width = 35;
            // 
            // ADD0108E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 438);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.cboQfycd);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtYYMM);
            this.Controls.Add(this.label2);
            this.Name = "ADD0108E";
            this.Text = "전월진료수납내역(ADD0108E)";
            this.Load += new System.EventHandler(this.ADD0108E_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtYYMM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXMM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn tcEXDT;
        private DevExpress.XtraGrid.Columns.GridColumn tcFINDT;
        private DevExpress.XtraGrid.Columns.GridColumn tcDPTCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBDIVFG;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcGONSGB;
        private DevExpress.XtraGrid.Columns.GridColumn gcDAETC;
        private DevExpress.XtraGrid.Columns.GridColumn gcTTAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcISPAM;
        private DevExpress.XtraGrid.Columns.GridColumn gcUISAM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRETYN;
        private DevExpress.XtraGrid.Columns.GridColumn gcSUMYN;
    }
}


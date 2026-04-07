namespace ADD0109E
{
    partial class ADD0109E
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
            this.tabJobdiv = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cboPrimdptcd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTdate = new System.Windows.Forms.TextBox();
            this.txtFdate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBededt = new System.Windows.Forms.TextBox();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtTbdodt = new System.Windows.Forms.TextBox();
            this.txtFbdodt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPricd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcERFG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGONSGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDAETC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRINM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHRLT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCALQY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDDAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFINDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcODT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGRPCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.chkOCheckfg = new System.Windows.Forms.CheckBox();
            this.chkQCheckfg = new System.Windows.Forms.CheckBox();
            this.chkRCheckfg = new System.Windows.Forms.CheckBox();
            this.chkLCheckfg = new System.Windows.Forms.CheckBox();
            this.chkNoExecfg = new System.Windows.Forms.CheckBox();
            this.btnPricd = new System.Windows.Forms.Button();
            this.btnSavePricd = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tabJobdiv.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabJobdiv
            // 
            this.tabJobdiv.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabJobdiv.Controls.Add(this.tabPage1);
            this.tabJobdiv.Controls.Add(this.tabPage2);
            this.tabJobdiv.Controls.Add(this.tabPage3);
            this.tabJobdiv.Controls.Add(this.tabPage4);
            this.tabJobdiv.Location = new System.Drawing.Point(9, 4);
            this.tabJobdiv.Name = "tabJobdiv";
            this.tabJobdiv.SelectedIndex = 0;
            this.tabJobdiv.Size = new System.Drawing.Size(726, 54);
            this.tabJobdiv.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabJobdiv.TabIndex = 0;
            this.tabJobdiv.SelectedIndexChanged += new System.EventHandler(this.tabJobdiv_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.cboPrimdptcd);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cboQfycd);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.txtTdate);
            this.tabPage1.Controls.Add(this.txtFdate);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(718, 25);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "외래";
            // 
            // cboPrimdptcd
            // 
            this.cboPrimdptcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrimdptcd.FormattingEnabled = true;
            this.cboPrimdptcd.Location = new System.Drawing.Point(549, 3);
            this.cboPrimdptcd.Name = "cboPrimdptcd";
            this.cboPrimdptcd.Size = new System.Drawing.Size(159, 20);
            this.cboPrimdptcd.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "진료분야 :";
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.Location = new System.Drawing.Point(315, 3);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(153, 20);
            this.cboQfycd.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(250, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 12);
            this.label15.TabIndex = 38;
            this.label15.Text = "환자자격 :";
            // 
            // txtTdate
            // 
            this.txtTdate.Location = new System.Drawing.Point(150, 2);
            this.txtTdate.Name = "txtTdate";
            this.txtTdate.Size = new System.Drawing.Size(82, 21);
            this.txtTdate.TabIndex = 9;
            // 
            // txtFdate
            // 
            this.txtFdate.Location = new System.Drawing.Point(62, 2);
            this.txtFdate.Name = "txtFdate";
            this.txtFdate.Size = new System.Drawing.Size(82, 21);
            this.txtFdate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "진료기간 :";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(718, 25);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "재원환자전체";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-2, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "현재 재원중인 환자가 대상입니다.";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.txtBededt);
            this.tabPage3.Controls.Add(this.txtPid);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(718, 25);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "재원환자특정";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "입원일자 :";
            // 
            // txtBededt
            // 
            this.txtBededt.Location = new System.Drawing.Point(224, 2);
            this.txtBededt.Name = "txtBededt";
            this.txtBededt.Size = new System.Drawing.Size(82, 21);
            this.txtBededt.TabIndex = 12;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(63, 2);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(82, 21);
            this.txtPid.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "환자ID :";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.txtTbdodt);
            this.tabPage4.Controls.Add(this.txtFbdodt);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(718, 25);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "퇴원환자전체";
            // 
            // txtTbdodt
            // 
            this.txtTbdodt.Location = new System.Drawing.Point(150, 2);
            this.txtTbdodt.Name = "txtTbdodt";
            this.txtTbdodt.Size = new System.Drawing.Size(82, 21);
            this.txtTbdodt.TabIndex = 12;
            // 
            // txtFbdodt
            // 
            this.txtFbdodt.Location = new System.Drawing.Point(63, 2);
            this.txtFbdodt.Name = "txtFbdodt";
            this.txtFbdodt.Size = new System.Drawing.Size(82, 21);
            this.txtFbdodt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-2, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "퇴원기간 :";
            // 
            // txtPricd
            // 
            this.txtPricd.Location = new System.Drawing.Point(76, 60);
            this.txtPricd.Name = "txtPricd";
            this.txtPricd.Size = new System.Drawing.Size(403, 21);
            this.txtPricd.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "예외코드 :";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(10, 88);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(887, 394);
            this.grdMain.TabIndex = 11;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPID,
            this.gcPNM,
            this.gcBEDEDT,
            this.gcEXDT,
            this.gcDPTCD,
            this.gcDRNM,
            this.gcERFG,
            this.gcQFYCD,
            this.gcPDIV,
            this.gcPDIVNM,
            this.gcGONSGB,
            this.gcDAETC,
            this.gcPRICD,
            this.gcPRINM,
            this.gcCHRLT,
            this.gcCALQY,
            this.gcDDAY,
            this.gcFINDT,
            this.gcODT,
            this.gcONO,
            this.gcGRPCD});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsSelection.MultiSelect = true;
            this.grdMainView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdMainView_RowCellStyle);
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.AllowEdit = false;
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 0;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.AllowEdit = false;
            this.gcBEDEDT.OptionsColumn.ReadOnly = true;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 2;
            this.gcBEDEDT.Width = 65;
            // 
            // gcEXDT
            // 
            this.gcEXDT.Caption = "진료일자";
            this.gcEXDT.FieldName = "EXDT";
            this.gcEXDT.Name = "gcEXDT";
            this.gcEXDT.OptionsColumn.AllowEdit = false;
            this.gcEXDT.OptionsColumn.ReadOnly = true;
            this.gcEXDT.Visible = true;
            this.gcEXDT.VisibleIndex = 3;
            this.gcEXDT.Width = 65;
            // 
            // gcDPTCD
            // 
            this.gcDPTCD.Caption = "진료과";
            this.gcDPTCD.FieldName = "DPTCD";
            this.gcDPTCD.Name = "gcDPTCD";
            this.gcDPTCD.OptionsColumn.AllowEdit = false;
            this.gcDPTCD.OptionsColumn.ReadOnly = true;
            this.gcDPTCD.Visible = true;
            this.gcDPTCD.VisibleIndex = 4;
            this.gcDPTCD.Width = 55;
            // 
            // gcDRNM
            // 
            this.gcDRNM.Caption = "주치의";
            this.gcDRNM.FieldName = "DRNM";
            this.gcDRNM.Name = "gcDRNM";
            this.gcDRNM.OptionsColumn.AllowEdit = false;
            this.gcDRNM.OptionsColumn.ReadOnly = true;
            this.gcDRNM.Visible = true;
            this.gcDRNM.VisibleIndex = 5;
            this.gcDRNM.Width = 55;
            // 
            // gcERFG
            // 
            this.gcERFG.Caption = "응급";
            this.gcERFG.FieldName = "ERFG";
            this.gcERFG.Name = "gcERFG";
            this.gcERFG.OptionsColumn.AllowEdit = false;
            this.gcERFG.OptionsColumn.ReadOnly = true;
            this.gcERFG.Visible = true;
            this.gcERFG.VisibleIndex = 6;
            this.gcERFG.Width = 35;
            // 
            // gcQFYCD
            // 
            this.gcQFYCD.Caption = "자격";
            this.gcQFYCD.FieldName = "QFYCD";
            this.gcQFYCD.Name = "gcQFYCD";
            this.gcQFYCD.OptionsColumn.AllowEdit = false;
            this.gcQFYCD.OptionsColumn.ReadOnly = true;
            this.gcQFYCD.Visible = true;
            this.gcQFYCD.VisibleIndex = 7;
            this.gcQFYCD.Width = 35;
            // 
            // gcPDIV
            // 
            this.gcPDIV.Caption = "구분";
            this.gcPDIV.FieldName = "PDIV";
            this.gcPDIV.Name = "gcPDIV";
            this.gcPDIV.OptionsColumn.AllowEdit = false;
            this.gcPDIV.OptionsColumn.ReadOnly = true;
            this.gcPDIV.Visible = true;
            this.gcPDIV.VisibleIndex = 8;
            this.gcPDIV.Width = 40;
            // 
            // gcPDIVNM
            // 
            this.gcPDIVNM.Caption = "구분명";
            this.gcPDIVNM.FieldName = "PDIVNM";
            this.gcPDIVNM.Name = "gcPDIVNM";
            this.gcPDIVNM.OptionsColumn.AllowEdit = false;
            this.gcPDIVNM.OptionsColumn.ReadOnly = true;
            this.gcPDIVNM.Visible = true;
            this.gcPDIVNM.VisibleIndex = 9;
            // 
            // gcGONSGB
            // 
            this.gcGONSGB.Caption = "공상";
            this.gcGONSGB.FieldName = "GONSGB";
            this.gcGONSGB.Name = "gcGONSGB";
            this.gcGONSGB.OptionsColumn.AllowEdit = false;
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
            this.gcDAETC.OptionsColumn.AllowEdit = false;
            this.gcDAETC.OptionsColumn.ReadOnly = true;
            this.gcDAETC.Visible = true;
            this.gcDAETC.VisibleIndex = 11;
            this.gcDAETC.Width = 35;
            // 
            // gcPRICD
            // 
            this.gcPRICD.Caption = "수가코드";
            this.gcPRICD.FieldName = "PRICD";
            this.gcPRICD.Name = "gcPRICD";
            this.gcPRICD.OptionsColumn.AllowEdit = false;
            this.gcPRICD.OptionsColumn.ReadOnly = true;
            this.gcPRICD.Visible = true;
            this.gcPRICD.VisibleIndex = 12;
            // 
            // gcPRINM
            // 
            this.gcPRINM.Caption = "수가명";
            this.gcPRINM.FieldName = "PRINM";
            this.gcPRINM.Name = "gcPRINM";
            this.gcPRINM.OptionsColumn.AllowEdit = false;
            this.gcPRINM.OptionsColumn.ReadOnly = true;
            this.gcPRINM.Visible = true;
            this.gcPRINM.VisibleIndex = 13;
            this.gcPRINM.Width = 140;
            // 
            // gcCHRLT
            // 
            this.gcCHRLT.Caption = "R";
            this.gcCHRLT.FieldName = "CHRLT";
            this.gcCHRLT.Name = "gcCHRLT";
            this.gcCHRLT.OptionsColumn.AllowEdit = false;
            this.gcCHRLT.OptionsColumn.ReadOnly = true;
            this.gcCHRLT.Visible = true;
            this.gcCHRLT.VisibleIndex = 14;
            this.gcCHRLT.Width = 25;
            // 
            // gcCALQY
            // 
            this.gcCALQY.Caption = "투여량";
            this.gcCALQY.FieldName = "CALQY";
            this.gcCALQY.Name = "gcCALQY";
            this.gcCALQY.OptionsColumn.AllowEdit = false;
            this.gcCALQY.OptionsColumn.ReadOnly = true;
            this.gcCALQY.Visible = true;
            this.gcCALQY.VisibleIndex = 15;
            this.gcCALQY.Width = 65;
            // 
            // gcDDAY
            // 
            this.gcDDAY.Caption = "일수";
            this.gcDDAY.FieldName = "DDAY";
            this.gcDDAY.Name = "gcDDAY";
            this.gcDDAY.OptionsColumn.AllowEdit = false;
            this.gcDDAY.OptionsColumn.ReadOnly = true;
            this.gcDDAY.Visible = true;
            this.gcDDAY.VisibleIndex = 16;
            this.gcDDAY.Width = 50;
            // 
            // gcFINDT
            // 
            this.gcFINDT.Caption = "수납일자";
            this.gcFINDT.FieldName = "FINDT";
            this.gcFINDT.Name = "gcFINDT";
            this.gcFINDT.OptionsColumn.AllowEdit = false;
            this.gcFINDT.OptionsColumn.ReadOnly = true;
            this.gcFINDT.Visible = true;
            this.gcFINDT.VisibleIndex = 17;
            this.gcFINDT.Width = 65;
            // 
            // gcODT
            // 
            this.gcODT.Caption = "처방일자";
            this.gcODT.FieldName = "ODT";
            this.gcODT.Name = "gcODT";
            this.gcODT.OptionsColumn.AllowEdit = false;
            this.gcODT.OptionsColumn.ReadOnly = true;
            this.gcODT.Visible = true;
            this.gcODT.VisibleIndex = 18;
            this.gcODT.Width = 65;
            // 
            // gcONO
            // 
            this.gcONO.Caption = "처방번호";
            this.gcONO.FieldName = "ONO";
            this.gcONO.Name = "gcONO";
            this.gcONO.OptionsColumn.AllowEdit = false;
            this.gcONO.OptionsColumn.ReadOnly = true;
            this.gcONO.Visible = true;
            this.gcONO.VisibleIndex = 19;
            this.gcONO.Width = 60;
            // 
            // gcGRPCD
            // 
            this.gcGRPCD.Caption = "그룹수가";
            this.gcGRPCD.FieldName = "GRPCD";
            this.gcGRPCD.Name = "gcGRPCD";
            this.gcGRPCD.OptionsColumn.AllowEdit = false;
            this.gcGRPCD.OptionsColumn.ReadOnly = true;
            this.gcGRPCD.Visible = true;
            this.gcGRPCD.VisibleIndex = 20;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(747, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 72;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(822, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 73;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 489);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(610, 12);
            this.label8.TabIndex = 74;
            this.label8.Text = "수납한 자료에서 검색합니다. 기능검사,방사선,임상병리검사가 대상입니다. 환자ID+진료일자 순으로 정렬됩니다.";
            // 
            // chkOCheckfg
            // 
            this.chkOCheckfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOCheckfg.AutoSize = true;
            this.chkOCheckfg.Checked = true;
            this.chkOCheckfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOCheckfg.Location = new System.Drawing.Point(630, 487);
            this.chkOCheckfg.Name = "chkOCheckfg";
            this.chkOCheckfg.Size = new System.Drawing.Size(48, 16);
            this.chkOCheckfg.TabIndex = 75;
            this.chkOCheckfg.Text = "물리";
            this.chkOCheckfg.UseVisualStyleBackColor = true;
            // 
            // chkQCheckfg
            // 
            this.chkQCheckfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkQCheckfg.AutoSize = true;
            this.chkQCheckfg.Checked = true;
            this.chkQCheckfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQCheckfg.Location = new System.Drawing.Point(683, 487);
            this.chkQCheckfg.Name = "chkQCheckfg";
            this.chkQCheckfg.Size = new System.Drawing.Size(48, 16);
            this.chkQCheckfg.TabIndex = 76;
            this.chkQCheckfg.Text = "기능";
            this.chkQCheckfg.UseVisualStyleBackColor = true;
            // 
            // chkRCheckfg
            // 
            this.chkRCheckfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRCheckfg.AutoSize = true;
            this.chkRCheckfg.Checked = true;
            this.chkRCheckfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRCheckfg.Location = new System.Drawing.Point(738, 487);
            this.chkRCheckfg.Name = "chkRCheckfg";
            this.chkRCheckfg.Size = new System.Drawing.Size(60, 16);
            this.chkRCheckfg.TabIndex = 77;
            this.chkRCheckfg.Text = "방사선";
            this.chkRCheckfg.UseVisualStyleBackColor = true;
            // 
            // chkLCheckfg
            // 
            this.chkLCheckfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLCheckfg.AutoSize = true;
            this.chkLCheckfg.Checked = true;
            this.chkLCheckfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLCheckfg.Location = new System.Drawing.Point(804, 487);
            this.chkLCheckfg.Name = "chkLCheckfg";
            this.chkLCheckfg.Size = new System.Drawing.Size(72, 16);
            this.chkLCheckfg.TabIndex = 78;
            this.chkLCheckfg.Text = "임상병리";
            this.chkLCheckfg.UseVisualStyleBackColor = true;
            // 
            // chkNoExecfg
            // 
            this.chkNoExecfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNoExecfg.AutoSize = true;
            this.chkNoExecfg.Checked = true;
            this.chkNoExecfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNoExecfg.Location = new System.Drawing.Point(880, 487);
            this.chkNoExecfg.Name = "chkNoExecfg";
            this.chkNoExecfg.Size = new System.Drawing.Size(15, 14);
            this.chkNoExecfg.TabIndex = 79;
            this.chkNoExecfg.UseVisualStyleBackColor = true;
            // 
            // btnPricd
            // 
            this.btnPricd.Location = new System.Drawing.Point(485, 59);
            this.btnPricd.Name = "btnPricd";
            this.btnPricd.Size = new System.Drawing.Size(54, 23);
            this.btnPricd.TabIndex = 80;
            this.btnPricd.Text = "검색";
            this.btnPricd.UseVisualStyleBackColor = true;
            this.btnPricd.Click += new System.EventHandler(this.btnPricd_Click);
            // 
            // btnSavePricd
            // 
            this.btnSavePricd.Location = new System.Drawing.Point(539, 59);
            this.btnSavePricd.Name = "btnSavePricd";
            this.btnSavePricd.Size = new System.Drawing.Size(54, 23);
            this.btnSavePricd.TabIndex = 81;
            this.btnSavePricd.Text = "저장";
            this.btnSavePricd.UseVisualStyleBackColor = true;
            this.btnSavePricd.Click += new System.EventHandler(this.btnSavePricd_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(600, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(301, 12);
            this.label9.TabIndex = 82;
            this.label9.Text = "← 예외코드가 다수일 경우 \",\" 로 구분하여 입력하세요.";
            // 
            // ADD0109E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 509);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSavePricd);
            this.Controls.Add(this.btnPricd);
            this.Controls.Add(this.chkNoExecfg);
            this.Controls.Add(this.chkLCheckfg);
            this.Controls.Add(this.chkRCheckfg);
            this.Controls.Add(this.chkQCheckfg);
            this.Controls.Add(this.chkOCheckfg);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtPricd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tabJobdiv);
            this.Name = "ADD0109E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "미실시처방조회(ADD0109E)";
            this.Load += new System.EventHandler(this.ADD0109E_Load);
            this.Activated += new System.EventHandler(this.ADD0109E_Activated);
            this.tabJobdiv.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabJobdiv;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtTdate;
        private System.Windows.Forms.TextBox txtFdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPrimdptcd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTbdodt;
        private System.Windows.Forms.TextBox txtFbdodt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBededt;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPricd;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkOCheckfg;
        private System.Windows.Forms.CheckBox chkQCheckfg;
        private System.Windows.Forms.CheckBox chkRCheckfg;
        private System.Windows.Forms.CheckBox chkLCheckfg;
        private System.Windows.Forms.CheckBox chkNoExecfg;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcERFG;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcGONSGB;
        private DevExpress.XtraGrid.Columns.GridColumn gcDAETC;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRINM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHRLT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCALQY;
        private DevExpress.XtraGrid.Columns.GridColumn gcDDAY;
        private DevExpress.XtraGrid.Columns.GridColumn gcFINDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcODT;
        private DevExpress.XtraGrid.Columns.GridColumn gcONO;
        private DevExpress.XtraGrid.Columns.GridColumn gcGRPCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private System.Windows.Forms.Button btnPricd;
        private System.Windows.Forms.Button btnSavePricd;
        private System.Windows.Forms.Label label9;
    }
}


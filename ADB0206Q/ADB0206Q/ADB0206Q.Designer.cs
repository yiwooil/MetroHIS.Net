namespace ADB0206Q
{
    partial class ADB0206Q
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFamnm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRESID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPSEXAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWARD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDGRD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDSTATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDODT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcILSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDIPTHNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCASEWORKERNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNINM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHTELNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFAMNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFTEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDISENM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJINRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOPDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRSVOP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOTELNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcILLST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPSTS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDINTENTDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDINTENTDT2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADLRT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcDRRMK2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWONRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSCHBEDODT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYSBNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSIMRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnColumnSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(822, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(743, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(73, 35);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.Size = new System.Drawing.Size(62, 21);
            this.txtPnm.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "환자이름 :";
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(202, 35);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(62, 21);
            this.txtPid.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "환자ID :";
            // 
            // txtResid
            // 
            this.txtResid.Location = new System.Drawing.Point(347, 35);
            this.txtResid.Name = "txtResid";
            this.txtResid.Size = new System.Drawing.Size(62, 21);
            this.txtResid.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "주민번호 :";
            // 
            // txtFamnm
            // 
            this.txtFamnm.Location = new System.Drawing.Point(509, 35);
            this.txtFamnm.Name = "txtFamnm";
            this.txtFamnm.Size = new System.Drawing.Size(62, 21);
            this.txtFamnm.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "보호자이름 :";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(10, 63);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(889, 350);
            this.grdMain.TabIndex = 24;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPID,
            this.gcPNM,
            this.gcRESID,
            this.gcPSEXAGE,
            this.gcWARD,
            this.gcBEDGRD,
            this.gcBEDSTATUS,
            this.gcTEL,
            this.gcBEDEDT,
            this.gcBEDODT,
            this.gcILSU,
            this.gcBEDIPTHNM,
            this.gcDPTNM,
            this.gcPDRNM,
            this.gcCASEWORKERNM,
            this.gcQFYNM,
            this.gcUNINM,
            this.gcADDR,
            this.gcHTELNO,
            this.gcDRRMK,
            this.gcFAMNM,
            this.gcFTEL,
            this.gcFADDR,
            this.gcDISENM,
            this.gcJINRMK,
            this.gcOPDT,
            this.gcRSVOP,
            this.gcOTELNO,
            this.gcILLST,
            this.gcPSTS,
            this.gcBEDINTENTDT,
            this.gcBEDINTENTDT2,
            this.gcADLRT,
            this.tcDRRMK2,
            this.gcWONRMK,
            this.gcSCHBEDODT,
            this.gcQFYSBNM,
            this.gcSIMRMK});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grdMainView_CustomDrawRowIndicator);
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 0;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcRESID
            // 
            this.gcRESID.Caption = "주민번호";
            this.gcRESID.FieldName = "RESID";
            this.gcRESID.Name = "gcRESID";
            this.gcRESID.OptionsColumn.ReadOnly = true;
            this.gcRESID.Visible = true;
            this.gcRESID.VisibleIndex = 2;
            // 
            // gcPSEXAGE
            // 
            this.gcPSEXAGE.Caption = "성/나이";
            this.gcPSEXAGE.FieldName = "PSEXAGE";
            this.gcPSEXAGE.Name = "gcPSEXAGE";
            this.gcPSEXAGE.OptionsColumn.ReadOnly = true;
            this.gcPSEXAGE.Visible = true;
            this.gcPSEXAGE.VisibleIndex = 3;
            // 
            // gcWARD
            // 
            this.gcWARD.Caption = "병실";
            this.gcWARD.FieldName = "WARD";
            this.gcWARD.Name = "gcWARD";
            this.gcWARD.OptionsColumn.ReadOnly = true;
            this.gcWARD.Visible = true;
            this.gcWARD.VisibleIndex = 4;
            // 
            // gcBEDGRD
            // 
            this.gcBEDGRD.Caption = "등급";
            this.gcBEDGRD.FieldName = "BEDGRD";
            this.gcBEDGRD.Name = "gcBEDGRD";
            this.gcBEDGRD.OptionsColumn.ReadOnly = true;
            this.gcBEDGRD.Visible = true;
            this.gcBEDGRD.VisibleIndex = 5;
            // 
            // gcBEDSTATUS
            // 
            this.gcBEDSTATUS.Caption = "병실상태";
            this.gcBEDSTATUS.FieldName = "BEDSTATUS";
            this.gcBEDSTATUS.Name = "gcBEDSTATUS";
            this.gcBEDSTATUS.OptionsColumn.ReadOnly = true;
            this.gcBEDSTATUS.Visible = true;
            this.gcBEDSTATUS.VisibleIndex = 6;
            // 
            // gcTEL
            // 
            this.gcTEL.Caption = "TEL";
            this.gcTEL.FieldName = "TEL";
            this.gcTEL.Name = "gcTEL";
            this.gcTEL.OptionsColumn.ReadOnly = true;
            this.gcTEL.Visible = true;
            this.gcTEL.VisibleIndex = 7;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.ReadOnly = true;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 8;
            // 
            // gcBEDODT
            // 
            this.gcBEDODT.Caption = "퇴원일";
            this.gcBEDODT.FieldName = "BEDODT";
            this.gcBEDODT.Name = "gcBEDODT";
            this.gcBEDODT.OptionsColumn.ReadOnly = true;
            this.gcBEDODT.Visible = true;
            this.gcBEDODT.VisibleIndex = 9;
            // 
            // gcILSU
            // 
            this.gcILSU.AppearanceCell.Options.UseTextOptions = true;
            this.gcILSU.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcILSU.Caption = "재원일수";
            this.gcILSU.FieldName = "ILSU";
            this.gcILSU.Name = "gcILSU";
            this.gcILSU.OptionsColumn.ReadOnly = true;
            this.gcILSU.Visible = true;
            this.gcILSU.VisibleIndex = 10;
            // 
            // gcBEDIPTHNM
            // 
            this.gcBEDIPTHNM.Caption = "입원경로";
            this.gcBEDIPTHNM.FieldName = "BEDIPTHNM";
            this.gcBEDIPTHNM.Name = "gcBEDIPTHNM";
            this.gcBEDIPTHNM.OptionsColumn.ReadOnly = true;
            this.gcBEDIPTHNM.Visible = true;
            this.gcBEDIPTHNM.VisibleIndex = 11;
            // 
            // gcDPTNM
            // 
            this.gcDPTNM.Caption = "진료과";
            this.gcDPTNM.FieldName = "DPTNM";
            this.gcDPTNM.Name = "gcDPTNM";
            this.gcDPTNM.OptionsColumn.ReadOnly = true;
            this.gcDPTNM.Visible = true;
            this.gcDPTNM.VisibleIndex = 12;
            // 
            // gcPDRNM
            // 
            this.gcPDRNM.Caption = "주치의";
            this.gcPDRNM.FieldName = "PDRNM";
            this.gcPDRNM.Name = "gcPDRNM";
            this.gcPDRNM.OptionsColumn.ReadOnly = true;
            this.gcPDRNM.Visible = true;
            this.gcPDRNM.VisibleIndex = 13;
            // 
            // gcCASEWORKERNM
            // 
            this.gcCASEWORKERNM.Caption = "C.W";
            this.gcCASEWORKERNM.FieldName = "CASEWORKERNM";
            this.gcCASEWORKERNM.Name = "gcCASEWORKERNM";
            this.gcCASEWORKERNM.OptionsColumn.ReadOnly = true;
            this.gcCASEWORKERNM.Visible = true;
            this.gcCASEWORKERNM.VisibleIndex = 14;
            // 
            // gcQFYNM
            // 
            this.gcQFYNM.Caption = "자격";
            this.gcQFYNM.FieldName = "QFYNM";
            this.gcQFYNM.Name = "gcQFYNM";
            this.gcQFYNM.OptionsColumn.ReadOnly = true;
            this.gcQFYNM.Visible = true;
            this.gcQFYNM.VisibleIndex = 15;
            // 
            // gcUNINM
            // 
            this.gcUNINM.Caption = "조합명";
            this.gcUNINM.FieldName = "UNINM";
            this.gcUNINM.Name = "gcUNINM";
            this.gcUNINM.OptionsColumn.ReadOnly = true;
            this.gcUNINM.Visible = true;
            this.gcUNINM.VisibleIndex = 16;
            // 
            // gcADDR
            // 
            this.gcADDR.Caption = "주소";
            this.gcADDR.FieldName = "ADDR";
            this.gcADDR.Name = "gcADDR";
            this.gcADDR.OptionsColumn.ReadOnly = true;
            this.gcADDR.Visible = true;
            this.gcADDR.VisibleIndex = 17;
            // 
            // gcHTELNO
            // 
            this.gcHTELNO.Caption = "전화번호";
            this.gcHTELNO.FieldName = "HTELNO";
            this.gcHTELNO.Name = "gcHTELNO";
            this.gcHTELNO.OptionsColumn.ReadOnly = true;
            this.gcHTELNO.Visible = true;
            this.gcHTELNO.VisibleIndex = 18;
            // 
            // gcDRRMK
            // 
            this.gcDRRMK.Caption = "ETC";
            this.gcDRRMK.FieldName = "DRRMK";
            this.gcDRRMK.Name = "gcDRRMK";
            this.gcDRRMK.OptionsColumn.ReadOnly = true;
            this.gcDRRMK.Visible = true;
            this.gcDRRMK.VisibleIndex = 19;
            // 
            // gcFAMNM
            // 
            this.gcFAMNM.Caption = "보호자성명";
            this.gcFAMNM.FieldName = "FAMNM";
            this.gcFAMNM.Name = "gcFAMNM";
            this.gcFAMNM.OptionsColumn.ReadOnly = true;
            this.gcFAMNM.Visible = true;
            this.gcFAMNM.VisibleIndex = 20;
            // 
            // gcFTEL
            // 
            this.gcFTEL.Caption = "보호자전화";
            this.gcFTEL.FieldName = "FTEL";
            this.gcFTEL.Name = "gcFTEL";
            this.gcFTEL.OptionsColumn.ReadOnly = true;
            this.gcFTEL.Visible = true;
            this.gcFTEL.VisibleIndex = 21;
            // 
            // gcFADDR
            // 
            this.gcFADDR.Caption = "보호자주소";
            this.gcFADDR.FieldName = "FADDR";
            this.gcFADDR.Name = "gcFADDR";
            this.gcFADDR.OptionsColumn.ReadOnly = true;
            this.gcFADDR.Visible = true;
            this.gcFADDR.VisibleIndex = 22;
            // 
            // gcDISENM
            // 
            this.gcDISENM.Caption = "상병";
            this.gcDISENM.FieldName = "DISENM";
            this.gcDISENM.Name = "gcDISENM";
            this.gcDISENM.OptionsColumn.ReadOnly = true;
            this.gcDISENM.Visible = true;
            this.gcDISENM.VisibleIndex = 23;
            // 
            // gcJINRMK
            // 
            this.gcJINRMK.Caption = "입원비고";
            this.gcJINRMK.FieldName = "JINRMK";
            this.gcJINRMK.Name = "gcJINRMK";
            this.gcJINRMK.OptionsColumn.ReadOnly = true;
            this.gcJINRMK.Visible = true;
            this.gcJINRMK.VisibleIndex = 24;
            // 
            // gcOPDT
            // 
            this.gcOPDT.Caption = "수술일";
            this.gcOPDT.FieldName = "OPDT";
            this.gcOPDT.Name = "gcOPDT";
            this.gcOPDT.OptionsColumn.ReadOnly = true;
            this.gcOPDT.Visible = true;
            this.gcOPDT.VisibleIndex = 25;
            // 
            // gcRSVOP
            // 
            this.gcRSVOP.Caption = "수술명";
            this.gcRSVOP.FieldName = "RSVOP";
            this.gcRSVOP.Name = "gcRSVOP";
            this.gcRSVOP.OptionsColumn.ReadOnly = true;
            this.gcRSVOP.Visible = true;
            this.gcRSVOP.VisibleIndex = 26;
            // 
            // gcOTELNO
            // 
            this.gcOTELNO.Caption = "전화번호2";
            this.gcOTELNO.FieldName = "OTELNO";
            this.gcOTELNO.Name = "gcOTELNO";
            this.gcOTELNO.OptionsColumn.ReadOnly = true;
            this.gcOTELNO.Visible = true;
            this.gcOTELNO.VisibleIndex = 27;
            // 
            // gcILLST
            // 
            this.gcILLST.Caption = "발병일";
            this.gcILLST.FieldName = "ILLST";
            this.gcILLST.Name = "gcILLST";
            this.gcILLST.OptionsColumn.ReadOnly = true;
            this.gcILLST.Visible = true;
            this.gcILLST.VisibleIndex = 28;
            // 
            // gcPSTS
            // 
            this.gcPSTS.Caption = "환자상태";
            this.gcPSTS.FieldName = "PSTS";
            this.gcPSTS.Name = "gcPSTS";
            this.gcPSTS.OptionsColumn.ReadOnly = true;
            this.gcPSTS.Visible = true;
            this.gcPSTS.VisibleIndex = 29;
            // 
            // gcBEDINTENTDT
            // 
            this.gcBEDINTENTDT.Caption = "퇴원의사확인일";
            this.gcBEDINTENTDT.FieldName = "BEDINTENTDT";
            this.gcBEDINTENTDT.Name = "gcBEDINTENTDT";
            this.gcBEDINTENTDT.OptionsColumn.ReadOnly = true;
            this.gcBEDINTENTDT.Visible = true;
            this.gcBEDINTENTDT.VisibleIndex = 30;
            // 
            // gcBEDINTENTDT2
            // 
            this.gcBEDINTENTDT2.Caption = "퇴원의사한도일";
            this.gcBEDINTENTDT2.FieldName = "BEDINTENTDT2";
            this.gcBEDINTENTDT2.Name = "gcBEDINTENTDT2";
            this.gcBEDINTENTDT2.OptionsColumn.ReadOnly = true;
            this.gcBEDINTENTDT2.Visible = true;
            this.gcBEDINTENTDT2.VisibleIndex = 31;
            // 
            // gcADLRT
            // 
            this.gcADLRT.Caption = "요양병원평가등급";
            this.gcADLRT.FieldName = "ADLRT";
            this.gcADLRT.Name = "gcADLRT";
            this.gcADLRT.OptionsColumn.ReadOnly = true;
            this.gcADLRT.Visible = true;
            this.gcADLRT.VisibleIndex = 32;
            // 
            // tcDRRMK2
            // 
            this.tcDRRMK2.Caption = "공통RMK";
            this.tcDRRMK2.FieldName = "DRRMK2";
            this.tcDRRMK2.Name = "tcDRRMK2";
            this.tcDRRMK2.OptionsColumn.ReadOnly = true;
            this.tcDRRMK2.Visible = true;
            this.tcDRRMK2.VisibleIndex = 33;
            // 
            // gcWONRMK
            // 
            this.gcWONRMK.Caption = "원무RMK";
            this.gcWONRMK.FieldName = "WONRMK";
            this.gcWONRMK.Name = "gcWONRMK";
            this.gcWONRMK.OptionsColumn.ReadOnly = true;
            this.gcWONRMK.Visible = true;
            this.gcWONRMK.VisibleIndex = 34;
            // 
            // gcSCHBEDODT
            // 
            this.gcSCHBEDODT.Caption = "퇴원예정일";
            this.gcSCHBEDODT.FieldName = "SCHBEDODT";
            this.gcSCHBEDODT.Name = "gcSCHBEDODT";
            this.gcSCHBEDODT.OptionsColumn.ReadOnly = true;
            this.gcSCHBEDODT.Visible = true;
            this.gcSCHBEDODT.VisibleIndex = 35;
            // 
            // gcQFYSBNM
            // 
            this.gcQFYSBNM.Caption = "부자격";
            this.gcQFYSBNM.FieldName = "QFYSBNM";
            this.gcQFYSBNM.Name = "gcQFYSBNM";
            this.gcQFYSBNM.OptionsColumn.ReadOnly = true;
            this.gcQFYSBNM.Visible = true;
            this.gcQFYSBNM.VisibleIndex = 36;
            // 
            // gcSIMRMK
            // 
            this.gcSIMRMK.Caption = "심사RMK";
            this.gcSIMRMK.FieldName = "SIMRMK";
            this.gcSIMRMK.Name = "gcSIMRMK";
            this.gcSIMRMK.OptionsColumn.ReadOnly = true;
            this.gcSIMRMK.Visible = true;
            this.gcSIMRMK.VisibleIndex = 37;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(73, 10);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(62, 21);
            this.txtDate.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "기준일자 :";
            // 
            // btnColumnSetting
            // 
            this.btnColumnSetting.Location = new System.Drawing.Point(615, 8);
            this.btnColumnSetting.Name = "btnColumnSetting";
            this.btnColumnSetting.Size = new System.Drawing.Size(79, 23);
            this.btnColumnSetting.TabIndex = 27;
            this.btnColumnSetting.Text = "컬럼 설정";
            this.btnColumnSetting.UseVisualStyleBackColor = true;
            this.btnColumnSetting.Click += new System.EventHandler(this.btnColumnSetting_Click);
            // 
            // ADB0206Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 420);
            this.Controls.Add(this.btnColumnSetting);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtFamnm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtResid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Name = "ADB0206Q";
            this.Text = "재원환자리스트(ADB0206Q)";
            this.Activated += new System.EventHandler(this.ADB0206Q_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFamnm;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcRESID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPSEXAGE;
        private DevExpress.XtraGrid.Columns.GridColumn gcWARD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDGRD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDSTATUS;
        private DevExpress.XtraGrid.Columns.GridColumn gcTEL;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCASEWORKERNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDODT;
        private DevExpress.XtraGrid.Columns.GridColumn gcILSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDIPTHNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNINM;
        private DevExpress.XtraGrid.Columns.GridColumn gcADDR;
        private DevExpress.XtraGrid.Columns.GridColumn gcHTELNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRRMK;
        private DevExpress.XtraGrid.Columns.GridColumn gcFAMNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcFTEL;
        private DevExpress.XtraGrid.Columns.GridColumn gcFADDR;
        private DevExpress.XtraGrid.Columns.GridColumn gcDISENM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJINRMK;
        private DevExpress.XtraGrid.Columns.GridColumn gcOPDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcRSVOP;
        private DevExpress.XtraGrid.Columns.GridColumn gcOTELNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcILLST;
        private DevExpress.XtraGrid.Columns.GridColumn gcPSTS;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDINTENTDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDINTENTDT2;
        private DevExpress.XtraGrid.Columns.GridColumn gcADLRT;
        private DevExpress.XtraGrid.Columns.GridColumn tcDRRMK2;
        private DevExpress.XtraGrid.Columns.GridColumn gcWONRMK;
        private DevExpress.XtraGrid.Columns.GridColumn gcSCHBEDODT;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYSBNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSIMRMK;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private System.Windows.Forms.Button btnColumnSetting;
    }
}


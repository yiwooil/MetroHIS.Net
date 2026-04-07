namespace ADD7002E
{
    partial class ADD7002E
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtRDRG = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAPPLDT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRDRG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcILSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPPLDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAADRG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIMOSGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBTMILSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tcTOPILSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTDILSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDANGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTDSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDAYSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINCENTIVE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMADJFACTOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMSTDSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMDAYSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMSTDSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMDAYSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMINCLSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHMSUGA2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHOADJFACTOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHOSTDSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHODAYSCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHOSTDSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHODAYSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHOINCLSUGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBHOSUGA2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRG7GROUPYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtRDRG);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtAPPLDT);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 43);
            this.panel1.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(367, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtRDRG
            // 
            this.txtRDRG.Location = new System.Drawing.Point(251, 12);
            this.txtRDRG.Name = "txtRDRG";
            this.txtRDRG.Size = new System.Drawing.Size(89, 21);
            this.txtRDRG.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "DRG번호:";
            // 
            // txtAPPLDT
            // 
            this.txtAPPLDT.Location = new System.Drawing.Point(83, 11);
            this.txtAPPLDT.Name = "txtAPPLDT";
            this.txtAPPLDT.Size = new System.Drawing.Size(89, 21);
            this.txtAPPLDT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "적용일자:";
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 43);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(966, 415);
            this.grdMain.TabIndex = 1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRDRG,
            this.gcILSU,
            this.gcAPPLDT,
            this.gcAADRG,
            this.tcNAME,
            this.gcIMOSGB,
            this.gcIB,
            this.gcBTMILSU,
            this.tcTOPILSU,
            this.gcSTDILSU,
            this.gcDANGA,
            this.gcSTDSCR,
            this.gcDAYSCR,
            this.gcINCENTIVE,
            this.gcBHMADJFACTOR,
            this.gcBHMSTDSCR,
            this.gcBHMDAYSCR,
            this.gcBHMSTDSUGA,
            this.gcBHMDAYSUGA,
            this.gcBHMINCLSUGA,
            this.gcBHMSUGA2,
            this.gcBHOADJFACTOR,
            this.gcBHOSTDSCR,
            this.gcBHODAYSCR,
            this.gcBHOSTDSUGA,
            this.gcBHODAYSUGA,
            this.gcBHOINCLSUGA,
            this.gcBHOSUGA2,
            this.gcDRG7GROUPYM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowGroup = false;
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcRDRG
            // 
            this.gcRDRG.Caption = "DRG번호";
            this.gcRDRG.FieldName = "RDRG";
            this.gcRDRG.Name = "gcRDRG";
            this.gcRDRG.OptionsColumn.AllowEdit = false;
            this.gcRDRG.OptionsColumn.ReadOnly = true;
            this.gcRDRG.Visible = true;
            this.gcRDRG.VisibleIndex = 0;
            // 
            // gcILSU
            // 
            this.gcILSU.Caption = "일수";
            this.gcILSU.FieldName = "ILSU";
            this.gcILSU.Name = "gcILSU";
            this.gcILSU.OptionsColumn.AllowEdit = false;
            this.gcILSU.OptionsColumn.ReadOnly = true;
            this.gcILSU.Visible = true;
            this.gcILSU.VisibleIndex = 1;
            // 
            // gcAPPLDT
            // 
            this.gcAPPLDT.Caption = "적용일자";
            this.gcAPPLDT.FieldName = "APPLDT";
            this.gcAPPLDT.Name = "gcAPPLDT";
            this.gcAPPLDT.OptionsColumn.AllowEdit = false;
            this.gcAPPLDT.OptionsColumn.ReadOnly = true;
            this.gcAPPLDT.Visible = true;
            this.gcAPPLDT.VisibleIndex = 2;
            // 
            // gcAADRG
            // 
            this.gcAADRG.Caption = "AADRG";
            this.gcAADRG.FieldName = "AADRG";
            this.gcAADRG.Name = "gcAADRG";
            this.gcAADRG.OptionsColumn.AllowEdit = false;
            this.gcAADRG.OptionsColumn.ReadOnly = true;
            this.gcAADRG.Visible = true;
            this.gcAADRG.VisibleIndex = 3;
            // 
            // tcNAME
            // 
            this.tcNAME.Caption = "명칭";
            this.tcNAME.FieldName = "NAME";
            this.tcNAME.Name = "tcNAME";
            this.tcNAME.OptionsColumn.AllowEdit = false;
            this.tcNAME.OptionsColumn.ReadOnly = true;
            this.tcNAME.Visible = true;
            this.tcNAME.VisibleIndex = 4;
            // 
            // gcIMOSGB
            // 
            this.gcIMOSGB.Caption = "내외정구분";
            this.gcIMOSGB.FieldName = "IMOSGB";
            this.gcIMOSGB.Name = "gcIMOSGB";
            this.gcIMOSGB.OptionsColumn.AllowEdit = false;
            this.gcIMOSGB.OptionsColumn.ReadOnly = true;
            this.gcIMOSGB.Visible = true;
            this.gcIMOSGB.VisibleIndex = 5;
            // 
            // gcIB
            // 
            this.gcIB.Caption = "IB";
            this.gcIB.FieldName = "IB";
            this.gcIB.Name = "gcIB";
            this.gcIB.OptionsColumn.AllowEdit = false;
            this.gcIB.OptionsColumn.ReadOnly = true;
            this.gcIB.Visible = true;
            this.gcIB.VisibleIndex = 6;
            // 
            // gcBTMILSU
            // 
            this.gcBTMILSU.Caption = "하단일수";
            this.gcBTMILSU.FieldName = "BTMILSU";
            this.gcBTMILSU.Name = "gcBTMILSU";
            this.gcBTMILSU.OptionsColumn.AllowEdit = false;
            this.gcBTMILSU.OptionsColumn.ReadOnly = true;
            this.gcBTMILSU.Visible = true;
            this.gcBTMILSU.VisibleIndex = 7;
            // 
            // tcTOPILSU
            // 
            this.tcTOPILSU.Caption = "상단일수";
            this.tcTOPILSU.FieldName = "TOPILSU";
            this.tcTOPILSU.Name = "tcTOPILSU";
            this.tcTOPILSU.OptionsColumn.AllowEdit = false;
            this.tcTOPILSU.OptionsColumn.ReadOnly = true;
            this.tcTOPILSU.Visible = true;
            this.tcTOPILSU.VisibleIndex = 8;
            // 
            // gcSTDILSU
            // 
            this.gcSTDILSU.Caption = "기준재원일";
            this.gcSTDILSU.FieldName = "STDILSU";
            this.gcSTDILSU.Name = "gcSTDILSU";
            this.gcSTDILSU.OptionsColumn.AllowEdit = false;
            this.gcSTDILSU.OptionsColumn.ReadOnly = true;
            this.gcSTDILSU.Visible = true;
            this.gcSTDILSU.VisibleIndex = 9;
            // 
            // gcDANGA
            // 
            this.gcDANGA.Caption = "점수당단가";
            this.gcDANGA.FieldName = "DANGA";
            this.gcDANGA.Name = "gcDANGA";
            this.gcDANGA.OptionsColumn.AllowEdit = false;
            this.gcDANGA.OptionsColumn.ReadOnly = true;
            this.gcDANGA.Visible = true;
            this.gcDANGA.VisibleIndex = 10;
            // 
            // gcSTDSCR
            // 
            this.gcSTDSCR.Caption = "기준점수";
            this.gcSTDSCR.FieldName = "STDSCR";
            this.gcSTDSCR.Name = "gcSTDSCR";
            this.gcSTDSCR.OptionsColumn.AllowEdit = false;
            this.gcSTDSCR.OptionsColumn.ReadOnly = true;
            this.gcSTDSCR.Visible = true;
            this.gcSTDSCR.VisibleIndex = 11;
            // 
            // gcDAYSCR
            // 
            this.gcDAYSCR.Caption = "일당점수";
            this.gcDAYSCR.Name = "gcDAYSCR";
            this.gcDAYSCR.OptionsColumn.AllowEdit = false;
            this.gcDAYSCR.OptionsColumn.ReadOnly = true;
            this.gcDAYSCR.Visible = true;
            this.gcDAYSCR.VisibleIndex = 12;
            // 
            // gcINCENTIVE
            // 
            this.gcINCENTIVE.Caption = "정책인센티브";
            this.gcINCENTIVE.FieldName = "INCENTIVE";
            this.gcINCENTIVE.Name = "gcINCENTIVE";
            this.gcINCENTIVE.OptionsColumn.AllowEdit = false;
            this.gcINCENTIVE.OptionsColumn.ReadOnly = true;
            this.gcINCENTIVE.Visible = true;
            this.gcINCENTIVE.VisibleIndex = 13;
            // 
            // gcBHMADJFACTOR
            // 
            this.gcBHMADJFACTOR.Caption = "건강보험조정계수";
            this.gcBHMADJFACTOR.FieldName = "BHMADJFACTOR";
            this.gcBHMADJFACTOR.Name = "gcBHMADJFACTOR";
            this.gcBHMADJFACTOR.OptionsColumn.AllowEdit = false;
            this.gcBHMADJFACTOR.OptionsColumn.ReadOnly = true;
            this.gcBHMADJFACTOR.Visible = true;
            this.gcBHMADJFACTOR.VisibleIndex = 14;
            // 
            // gcBHMSTDSCR
            // 
            this.gcBHMSTDSCR.Caption = "건강보험기준점수(전)";
            this.gcBHMSTDSCR.FieldName = "BHMSTDSCR";
            this.gcBHMSTDSCR.Name = "gcBHMSTDSCR";
            this.gcBHMSTDSCR.OptionsColumn.AllowEdit = false;
            this.gcBHMSTDSCR.OptionsColumn.ReadOnly = true;
            this.gcBHMSTDSCR.Visible = true;
            this.gcBHMSTDSCR.VisibleIndex = 15;
            // 
            // gcBHMDAYSCR
            // 
            this.gcBHMDAYSCR.Caption = "건강보험일당점수(전)";
            this.gcBHMDAYSCR.FieldName = "BHMDAYSCR";
            this.gcBHMDAYSCR.Name = "gcBHMDAYSCR";
            this.gcBHMDAYSCR.OptionsColumn.AllowEdit = false;
            this.gcBHMDAYSCR.OptionsColumn.ReadOnly = true;
            this.gcBHMDAYSCR.Visible = true;
            this.gcBHMDAYSCR.VisibleIndex = 16;
            // 
            // gcBHMSTDSUGA
            // 
            this.gcBHMSTDSUGA.Caption = "건강보험기준수가(전)";
            this.gcBHMSTDSUGA.FieldName = "BHMSTDSUGA";
            this.gcBHMSTDSUGA.Name = "gcBHMSTDSUGA";
            this.gcBHMSTDSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHMSTDSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHMSTDSUGA.Visible = true;
            this.gcBHMSTDSUGA.VisibleIndex = 17;
            // 
            // gcBHMDAYSUGA
            // 
            this.gcBHMDAYSUGA.Caption = "건강보험일당수가(전)";
            this.gcBHMDAYSUGA.FieldName = "BHMDAYSUGA";
            this.gcBHMDAYSUGA.Name = "gcBHMDAYSUGA";
            this.gcBHMDAYSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHMDAYSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHMDAYSUGA.Visible = true;
            this.gcBHMDAYSUGA.VisibleIndex = 18;
            // 
            // gcBHMINCLSUGA
            // 
            this.gcBHMINCLSUGA.Caption = "건강보험포괄수가(전)";
            this.gcBHMINCLSUGA.FieldName = "BHMINCLSUGA";
            this.gcBHMINCLSUGA.Name = "gcBHMINCLSUGA";
            this.gcBHMINCLSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHMINCLSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHMINCLSUGA.Visible = true;
            this.gcBHMINCLSUGA.VisibleIndex = 19;
            // 
            // gcBHMSUGA2
            // 
            this.gcBHMSUGA2.Caption = "건강보험포괄수가(후)";
            this.gcBHMSUGA2.FieldName = "BHMSUGA2";
            this.gcBHMSUGA2.Name = "gcBHMSUGA2";
            this.gcBHMSUGA2.OptionsColumn.AllowEdit = false;
            this.gcBHMSUGA2.OptionsColumn.ReadOnly = true;
            this.gcBHMSUGA2.Visible = true;
            this.gcBHMSUGA2.VisibleIndex = 20;
            // 
            // gcBHOADJFACTOR
            // 
            this.gcBHOADJFACTOR.Caption = "의료급여조정게수";
            this.gcBHOADJFACTOR.FieldName = "BHOADJFACTOR";
            this.gcBHOADJFACTOR.Name = "gcBHOADJFACTOR";
            this.gcBHOADJFACTOR.OptionsColumn.AllowEdit = false;
            this.gcBHOADJFACTOR.OptionsColumn.ReadOnly = true;
            this.gcBHOADJFACTOR.Visible = true;
            this.gcBHOADJFACTOR.VisibleIndex = 21;
            // 
            // gcBHOSTDSCR
            // 
            this.gcBHOSTDSCR.Caption = "의료급여기준점수(전)";
            this.gcBHOSTDSCR.FieldName = "BHOSTDSCR";
            this.gcBHOSTDSCR.Name = "gcBHOSTDSCR";
            this.gcBHOSTDSCR.OptionsColumn.AllowEdit = false;
            this.gcBHOSTDSCR.OptionsColumn.ReadOnly = true;
            this.gcBHOSTDSCR.Visible = true;
            this.gcBHOSTDSCR.VisibleIndex = 22;
            // 
            // gcBHODAYSCR
            // 
            this.gcBHODAYSCR.Caption = "의료급여일당점수(전)";
            this.gcBHODAYSCR.FieldName = "BHODAYSCR";
            this.gcBHODAYSCR.Name = "gcBHODAYSCR";
            this.gcBHODAYSCR.OptionsColumn.AllowEdit = false;
            this.gcBHODAYSCR.OptionsColumn.ReadOnly = true;
            this.gcBHODAYSCR.Visible = true;
            this.gcBHODAYSCR.VisibleIndex = 23;
            // 
            // gcBHOSTDSUGA
            // 
            this.gcBHOSTDSUGA.Caption = "의료급여기준수가(전)";
            this.gcBHOSTDSUGA.FieldName = "BHOSTDSUGA";
            this.gcBHOSTDSUGA.Name = "gcBHOSTDSUGA";
            this.gcBHOSTDSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHOSTDSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHOSTDSUGA.Visible = true;
            this.gcBHOSTDSUGA.VisibleIndex = 24;
            // 
            // gcBHODAYSUGA
            // 
            this.gcBHODAYSUGA.Caption = "의료급여일당수가(전)";
            this.gcBHODAYSUGA.FieldName = "BHODAYSUGA";
            this.gcBHODAYSUGA.Name = "gcBHODAYSUGA";
            this.gcBHODAYSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHODAYSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHODAYSUGA.Visible = true;
            this.gcBHODAYSUGA.VisibleIndex = 25;
            // 
            // gcBHOINCLSUGA
            // 
            this.gcBHOINCLSUGA.Caption = "의료급여포괄수가(전)";
            this.gcBHOINCLSUGA.FieldName = "BHOINCLSUGA";
            this.gcBHOINCLSUGA.Name = "gcBHOINCLSUGA";
            this.gcBHOINCLSUGA.OptionsColumn.AllowEdit = false;
            this.gcBHOINCLSUGA.OptionsColumn.ReadOnly = true;
            this.gcBHOINCLSUGA.Visible = true;
            this.gcBHOINCLSUGA.VisibleIndex = 26;
            // 
            // gcBHOSUGA2
            // 
            this.gcBHOSUGA2.Caption = "의료급여포괄수가(후)";
            this.gcBHOSUGA2.FieldName = "BHOSUGA2";
            this.gcBHOSUGA2.Name = "gcBHOSUGA2";
            this.gcBHOSUGA2.OptionsColumn.AllowEdit = false;
            this.gcBHOSUGA2.OptionsColumn.ReadOnly = true;
            this.gcBHOSUGA2.Visible = true;
            this.gcBHOSUGA2.VisibleIndex = 27;
            // 
            // gcDRG7GROUPYM
            // 
            this.gcDRG7GROUPYM.Caption = "7개질병군여부";
            this.gcDRG7GROUPYM.FieldName = "DRG7GROUPYM";
            this.gcDRG7GROUPYM.Name = "gcDRG7GROUPYM";
            this.gcDRG7GROUPYM.OptionsColumn.AllowEdit = false;
            this.gcDRG7GROUPYM.OptionsColumn.ReadOnly = true;
            this.gcDRG7GROUPYM.Visible = true;
            this.gcDRG7GROUPYM.VisibleIndex = 28;
            // 
            // ADD7002E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 458);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.panel1);
            this.Name = "ADD7002E";
            this.Text = "신포괄수가조회(ADD7002E)";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAPPLDT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtRDRG;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcRDRG;
        private DevExpress.XtraGrid.Columns.GridColumn gcILSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPPLDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAADRG;
        private DevExpress.XtraGrid.Columns.GridColumn tcNAME;
        private DevExpress.XtraGrid.Columns.GridColumn gcIMOSGB;
        private DevExpress.XtraGrid.Columns.GridColumn gcBTMILSU;
        private DevExpress.XtraGrid.Columns.GridColumn tcTOPILSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDILSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcDANGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcDAYSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcINCENTIVE;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMADJFACTOR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMSTDSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMDAYSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMSTDSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMDAYSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMINCLSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHMSUGA2;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHOADJFACTOR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHOSTDSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHODAYSCR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHOSTDSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHODAYSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHOINCLSUGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcBHOSUGA2;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRG7GROUPYM;
        private DevExpress.XtraGrid.Columns.GridColumn gcIB;
    }
}


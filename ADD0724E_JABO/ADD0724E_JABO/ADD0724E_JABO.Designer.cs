namespace ADD0724E_JABO
{
    partial class ADD0724E_JABO
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
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.txtCnecno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQueryOneYear = new System.Windows.Forms.Button();
            this.btnQuerySixMonth = new System.Windows.Forms.Button();
            this.btnQueryThreeMonth = new System.Windows.Forms.Button();
            this.btnQueryOneMonth = new System.Windows.Forms.Button();
            this.btnQueryOneWeek = new System.Windows.Forms.Button();
            this.btnQueryNoLimit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtJsdemseq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbFmgbnN150 = new System.Windows.Forms.RadioButton();
            this.rbFmgbnN130 = new System.Windows.Forms.RadioButton();
            this.rbFmgbnAll = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnPrint = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcJSDEMSEQ = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSREDAY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSTOTAMT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSTTTAMT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSJBPTAMT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcCNECNO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcDEMSEQ = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcDEMNO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSREDEPT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSSAU_MEMO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcFMGBNNM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJRFG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcVERSION = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcFMGBN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcDCOUNT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcJSYYSEQ = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnPrintSub = new System.Windows.Forms.Button();
            this.grdSub = new DevExpress.XtraGrid.GridControl();
            this.grdSubView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEPRTNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPPRNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJSAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJSTTTAMT_SUB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJSJBPTAMT_SUB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJSAMT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJSAMT2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMEMO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gcMAINROW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(67, 7);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(60, 21);
            this.txtFrdt.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "통보일자";
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(128, 7);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(60, 21);
            this.txtTodt.TabIndex = 27;
            // 
            // txtCnecno
            // 
            this.txtCnecno.Location = new System.Drawing.Point(67, 31);
            this.txtCnecno.Name = "txtCnecno";
            this.txtCnecno.Size = new System.Drawing.Size(60, 21);
            this.txtCnecno.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "접수번호";
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(191, 32);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(60, 21);
            this.txtDemno.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "청구번호";
            // 
            // btnQueryOneYear
            // 
            this.btnQueryOneYear.Location = new System.Drawing.Point(256, 7);
            this.btnQueryOneYear.Name = "btnQueryOneYear";
            this.btnQueryOneYear.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneYear.TabIndex = 32;
            this.btnQueryOneYear.Text = "최근1년";
            this.btnQueryOneYear.UseVisualStyleBackColor = true;
            this.btnQueryOneYear.Click += new System.EventHandler(this.btnQueryOneYear_Click);
            // 
            // btnQuerySixMonth
            // 
            this.btnQuerySixMonth.Location = new System.Drawing.Point(330, 7);
            this.btnQuerySixMonth.Name = "btnQuerySixMonth";
            this.btnQuerySixMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQuerySixMonth.TabIndex = 33;
            this.btnQuerySixMonth.Text = "최근6개월";
            this.btnQuerySixMonth.UseVisualStyleBackColor = true;
            this.btnQuerySixMonth.Click += new System.EventHandler(this.btnQuerySixMonth_Click);
            // 
            // btnQueryThreeMonth
            // 
            this.btnQueryThreeMonth.Location = new System.Drawing.Point(404, 7);
            this.btnQueryThreeMonth.Name = "btnQueryThreeMonth";
            this.btnQueryThreeMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQueryThreeMonth.TabIndex = 34;
            this.btnQueryThreeMonth.Text = "최근3개월";
            this.btnQueryThreeMonth.UseVisualStyleBackColor = true;
            this.btnQueryThreeMonth.Click += new System.EventHandler(this.btnQueryThreeMonth_Click);
            // 
            // btnQueryOneMonth
            // 
            this.btnQueryOneMonth.Location = new System.Drawing.Point(478, 7);
            this.btnQueryOneMonth.Name = "btnQueryOneMonth";
            this.btnQueryOneMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneMonth.TabIndex = 35;
            this.btnQueryOneMonth.Text = "최근1개월";
            this.btnQueryOneMonth.UseVisualStyleBackColor = true;
            this.btnQueryOneMonth.Click += new System.EventHandler(this.btnQueryOneMonth_Click);
            // 
            // btnQueryOneWeek
            // 
            this.btnQueryOneWeek.Location = new System.Drawing.Point(552, 7);
            this.btnQueryOneWeek.Name = "btnQueryOneWeek";
            this.btnQueryOneWeek.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneWeek.TabIndex = 36;
            this.btnQueryOneWeek.Text = "최근1주일";
            this.btnQueryOneWeek.UseVisualStyleBackColor = true;
            this.btnQueryOneWeek.Click += new System.EventHandler(this.btnQueryOneWeek_Click);
            // 
            // btnQueryNoLimit
            // 
            this.btnQueryNoLimit.Location = new System.Drawing.Point(626, 7);
            this.btnQueryNoLimit.Name = "btnQueryNoLimit";
            this.btnQueryNoLimit.Size = new System.Drawing.Size(75, 23);
            this.btnQueryNoLimit.TabIndex = 37;
            this.btnQueryNoLimit.Text = "제한없음";
            this.btnQueryNoLimit.UseVisualStyleBackColor = true;
            this.btnQueryNoLimit.Click += new System.EventHandler(this.btnQueryNoLimit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(753, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 38;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtJsdemseq
            // 
            this.txtJsdemseq.Location = new System.Drawing.Point(337, 32);
            this.txtJsdemseq.Name = "txtJsdemseq";
            this.txtJsdemseq.Size = new System.Drawing.Size(60, 21);
            this.txtJsdemseq.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "정산심사차수";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbFmgbnN150);
            this.panel1.Controls.Add(this.rbFmgbnN130);
            this.panel1.Controls.Add(this.rbFmgbnAll);
            this.panel1.Location = new System.Drawing.Point(399, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 25);
            this.panel1.TabIndex = 41;
            // 
            // rbFmgbnN150
            // 
            this.rbFmgbnN150.AutoSize = true;
            this.rbFmgbnN150.Location = new System.Drawing.Point(125, 4);
            this.rbFmgbnN150.Name = "rbFmgbnN150";
            this.rbFmgbnN150.Size = new System.Drawing.Size(71, 16);
            this.rbFmgbnN150.TabIndex = 2;
            this.rbFmgbnN150.Text = "정산심사";
            this.rbFmgbnN150.UseVisualStyleBackColor = true;
            // 
            // rbFmgbnN130
            // 
            this.rbFmgbnN130.AutoSize = true;
            this.rbFmgbnN130.Location = new System.Drawing.Point(53, 4);
            this.rbFmgbnN130.Name = "rbFmgbnN130";
            this.rbFmgbnN130.Size = new System.Drawing.Size(71, 16);
            this.rbFmgbnN130.TabIndex = 1;
            this.rbFmgbnN130.Text = "이의제기";
            this.rbFmgbnN130.UseVisualStyleBackColor = true;
            // 
            // rbFmgbnAll
            // 
            this.rbFmgbnAll.AutoSize = true;
            this.rbFmgbnAll.Checked = true;
            this.rbFmgbnAll.Location = new System.Drawing.Point(5, 4);
            this.rbFmgbnAll.Name = "rbFmgbnAll";
            this.rbFmgbnAll.Size = new System.Drawing.Size(47, 16);
            this.rbFmgbnAll.TabIndex = 0;
            this.rbFmgbnAll.TabStop = true;
            this.rbFmgbnAll.Text = "전체";
            this.rbFmgbnAll.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 58);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel1.Controls.Add(this.grdMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnPrintSub);
            this.splitContainer1.Panel2.Controls.Add(this.grdSub);
            this.splitContainer1.Size = new System.Drawing.Size(967, 418);
            this.splitContainer1.SplitterDistance = 216;
            this.splitContainer1.TabIndex = 42;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(870, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 43;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(967, 216);
            this.grdMain.TabIndex = 1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2});
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gcJSDEMSEQ,
            this.gcJSREDAY,
            this.gcJSTOTAMT,
            this.gcJSTTTAMT,
            this.gcJSJBPTAMT,
            this.gcCNECNO,
            this.gcDEMSEQ,
            this.gcDEMNO,
            this.gcJSREDEPT,
            this.gcJSSAU_MEMO,
            this.gcFMGBNNM,
            this.gcJRFG,
            this.gcVERSION,
            this.gcFMGBN,
            this.gcDCOUNT,
            this.gcJSYYSEQ});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "정산심사";
            this.gridBand1.Columns.Add(this.gcJSDEMSEQ);
            this.gridBand1.Columns.Add(this.gcJSREDAY);
            this.gridBand1.Columns.Add(this.gcJSTOTAMT);
            this.gridBand1.Columns.Add(this.gcJSTTTAMT);
            this.gridBand1.Columns.Add(this.gcJSJBPTAMT);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 375;
            // 
            // gcJSDEMSEQ
            // 
            this.gcJSDEMSEQ.Caption = "차수";
            this.gcJSDEMSEQ.FieldName = "JSDEMSEQ";
            this.gcJSDEMSEQ.Name = "gcJSDEMSEQ";
            this.gcJSDEMSEQ.OptionsColumn.AllowEdit = false;
            this.gcJSDEMSEQ.Visible = true;
            // 
            // gcJSREDAY
            // 
            this.gcJSREDAY.Caption = "통보일자";
            this.gcJSREDAY.FieldName = "JSREDAY";
            this.gcJSREDAY.Name = "gcJSREDAY";
            this.gcJSREDAY.OptionsColumn.AllowEdit = false;
            this.gcJSREDAY.Visible = true;
            this.gcJSREDAY.Width = 65;
            // 
            // gcJSTOTAMT
            // 
            this.gcJSTOTAMT.Caption = "결정차액";
            this.gcJSTOTAMT.DisplayFormat.FormatString = "#,###";
            this.gcJSTOTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSTOTAMT.FieldName = "JSTOTAMT";
            this.gcJSTOTAMT.Name = "gcJSTOTAMT";
            this.gcJSTOTAMT.OptionsColumn.AllowEdit = false;
            this.gcJSTOTAMT.Visible = true;
            // 
            // gcJSTTTAMT
            // 
            this.gcJSTTTAMT.Caption = "진료비총액";
            this.gcJSTTTAMT.DisplayFormat.FormatString = "#,###";
            this.gcJSTTTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSTTTAMT.FieldName = "JSTTTAMT";
            this.gcJSTTTAMT.Name = "gcJSTTTAMT";
            this.gcJSTTTAMT.OptionsColumn.AllowEdit = false;
            this.gcJSTTTAMT.Visible = true;
            // 
            // gcJSJBPTAMT
            // 
            this.gcJSJBPTAMT.Caption = "환자납부총액";
            this.gcJSJBPTAMT.DisplayFormat.FormatString = "#,###";
            this.gcJSJBPTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSJBPTAMT.FieldName = "JSJBPTAMT";
            this.gcJSJBPTAMT.Name = "gcJSJBPTAMT";
            this.gcJSJBPTAMT.OptionsColumn.AllowEdit = false;
            this.gcJSJBPTAMT.Visible = true;
            this.gcJSJBPTAMT.Width = 85;
            // 
            // gridBand2
            // 
            this.gridBand2.Columns.Add(this.gcCNECNO);
            this.gridBand2.Columns.Add(this.gcDEMSEQ);
            this.gridBand2.Columns.Add(this.gcDEMNO);
            this.gridBand2.Columns.Add(this.gcJSREDEPT);
            this.gridBand2.Columns.Add(this.gcJSSAU_MEMO);
            this.gridBand2.Columns.Add(this.gcFMGBNNM);
            this.gridBand2.Columns.Add(this.gcJRFG);
            this.gridBand2.Columns.Add(this.gcVERSION);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 555;
            // 
            // gcCNECNO
            // 
            this.gcCNECNO.Caption = "접수번호";
            this.gcCNECNO.FieldName = "CNECNO";
            this.gcCNECNO.Name = "gcCNECNO";
            this.gcCNECNO.OptionsColumn.AllowEdit = false;
            this.gcCNECNO.Visible = true;
            this.gcCNECNO.Width = 65;
            // 
            // gcDEMSEQ
            // 
            this.gcDEMSEQ.Caption = "심사차수";
            this.gcDEMSEQ.FieldName = "DEMSEQ";
            this.gcDEMSEQ.Name = "gcDEMSEQ";
            this.gcDEMSEQ.OptionsColumn.AllowEdit = false;
            this.gcDEMSEQ.Visible = true;
            this.gcDEMSEQ.Width = 65;
            // 
            // gcDEMNO
            // 
            this.gcDEMNO.Caption = "청구번호";
            this.gcDEMNO.FieldName = "DEMNO";
            this.gcDEMNO.Name = "gcDEMNO";
            this.gcDEMNO.OptionsColumn.AllowEdit = false;
            this.gcDEMNO.Visible = true;
            this.gcDEMNO.Width = 85;
            // 
            // gcJSREDEPT
            // 
            this.gcJSREDEPT.Caption = "담당자";
            this.gcJSREDEPT.FieldName = "JSREDEPT";
            this.gcJSREDEPT.Name = "gcJSREDEPT";
            this.gcJSREDEPT.OptionsColumn.AllowEdit = false;
            this.gcJSREDEPT.Visible = true;
            this.gcJSREDEPT.Width = 210;
            // 
            // gcJSSAU_MEMO
            // 
            this.gcJSSAU_MEMO.Caption = "참조란";
            this.gcJSSAU_MEMO.FieldName = "JSSAU_MEMO";
            this.gcJSSAU_MEMO.Name = "gcJSSAU_MEMO";
            this.gcJSSAU_MEMO.OptionsColumn.AllowEdit = false;
            this.gcJSSAU_MEMO.Visible = true;
            // 
            // gcFMGBNNM
            // 
            this.gcFMGBNNM.Caption = "서식구분";
            this.gcFMGBNNM.FieldName = "FMGBNNM";
            this.gcFMGBNNM.Name = "gcFMGBNNM";
            this.gcFMGBNNM.OptionsColumn.AllowEdit = false;
            this.gcFMGBNNM.Visible = true;
            this.gcFMGBNNM.Width = 55;
            // 
            // gcJRFG
            // 
            this.gcJRFG.Caption = "종별구분";
            this.gcJRFG.FieldName = "JRFG";
            this.gcJRFG.Name = "gcJRFG";
            this.gcJRFG.OptionsColumn.AllowEdit = false;
            // 
            // gcVERSION
            // 
            this.gcVERSION.Caption = "버전";
            this.gcVERSION.FieldName = "VERSION";
            this.gcVERSION.Name = "gcVERSION";
            this.gcVERSION.OptionsColumn.AllowEdit = false;
            // 
            // gcFMGBN
            // 
            this.gcFMGBN.Caption = "FMGBN";
            this.gcFMGBN.FieldName = "FMGBN";
            this.gcFMGBN.Name = "gcFMGBN";
            // 
            // gcDCOUNT
            // 
            this.gcDCOUNT.Caption = "DCOUNT";
            this.gcDCOUNT.FieldName = "DCOUNT";
            this.gcDCOUNT.Name = "gcDCOUNT";
            // 
            // gcJSYYSEQ
            // 
            this.gcJSYYSEQ.Caption = "JSYYSEQ";
            this.gcJSYYSEQ.FieldName = "JSYYSEQ";
            this.gcJSYYSEQ.Name = "gcJSYYSEQ";
            // 
            // btnPrintSub
            // 
            this.btnPrintSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintSub.Location = new System.Drawing.Point(870, 1);
            this.btnPrintSub.Name = "btnPrintSub";
            this.btnPrintSub.Size = new System.Drawing.Size(75, 23);
            this.btnPrintSub.TabIndex = 44;
            this.btnPrintSub.Text = "출력";
            this.btnPrintSub.UseVisualStyleBackColor = true;
            this.btnPrintSub.Click += new System.EventHandler(this.btnPrintSub_Click);
            // 
            // grdSub
            // 
            this.grdSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSub.Location = new System.Drawing.Point(0, 0);
            this.grdSub.MainView = this.grdSubView;
            this.grdSub.Name = "grdSub";
            this.grdSub.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdSub.Size = new System.Drawing.Size(967, 198);
            this.grdSub.TabIndex = 0;
            this.grdSub.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSubView});
            // 
            // grdSubView
            // 
            this.grdSubView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEPRTNO,
            this.gcPNM,
            this.gcAPPRNO,
            this.gcJSAMT,
            this.gcJSTTTAMT_SUB,
            this.gcJSJBPTAMT_SUB,
            this.gcJSAMT1,
            this.gcJSAMT2,
            this.gcMEMO,
            this.gcMAINROW});
            this.grdSubView.GridControl = this.grdSub;
            this.grdSubView.Name = "grdSubView";
            this.grdSubView.OptionsView.ColumnAutoWidth = false;
            this.grdSubView.OptionsView.RowAutoHeight = true;
            this.grdSubView.OptionsView.ShowGroupPanel = false;
            this.grdSubView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcEPRTNO
            // 
            this.gcEPRTNO.Caption = "명일련";
            this.gcEPRTNO.DisplayFormat.FormatString = "#,###";
            this.gcEPRTNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcEPRTNO.FieldName = "EPRTNO";
            this.gcEPRTNO.Name = "gcEPRTNO";
            this.gcEPRTNO.OptionsColumn.AllowEdit = false;
            this.gcEPRTNO.Visible = true;
            this.gcEPRTNO.VisibleIndex = 0;
            this.gcEPRTNO.Width = 55;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcAPPRNO
            // 
            this.gcAPPRNO.Caption = "사고접수번호";
            this.gcAPPRNO.FieldName = "APPRNO";
            this.gcAPPRNO.Name = "gcAPPRNO";
            this.gcAPPRNO.OptionsColumn.AllowEdit = false;
            this.gcAPPRNO.Visible = true;
            this.gcAPPRNO.VisibleIndex = 2;
            this.gcAPPRNO.Width = 85;
            // 
            // gcJSAMT
            // 
            this.gcJSAMT.Caption = "결정차액";
            this.gcJSAMT.DisplayFormat.FormatString = "#,##0";
            this.gcJSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSAMT.FieldName = "JSAMT";
            this.gcJSAMT.Name = "gcJSAMT";
            this.gcJSAMT.OptionsColumn.AllowEdit = false;
            this.gcJSAMT.Visible = true;
            this.gcJSAMT.VisibleIndex = 3;
            // 
            // gcJSTTTAMT_SUB
            // 
            this.gcJSTTTAMT_SUB.Caption = "진료비총액";
            this.gcJSTTTAMT_SUB.DisplayFormat.FormatString = "#,##0";
            this.gcJSTTTAMT_SUB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSTTTAMT_SUB.FieldName = "JSTTTAMT";
            this.gcJSTTTAMT_SUB.Name = "gcJSTTTAMT_SUB";
            this.gcJSTTTAMT_SUB.OptionsColumn.AllowEdit = false;
            this.gcJSTTTAMT_SUB.Visible = true;
            this.gcJSTTTAMT_SUB.VisibleIndex = 4;
            // 
            // gcJSJBPTAMT_SUB
            // 
            this.gcJSJBPTAMT_SUB.Caption = "환자납부총액";
            this.gcJSJBPTAMT_SUB.DisplayFormat.FormatString = "#,##0";
            this.gcJSJBPTAMT_SUB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSJBPTAMT_SUB.FieldName = "JSJBPTAMT";
            this.gcJSJBPTAMT_SUB.Name = "gcJSJBPTAMT_SUB";
            this.gcJSJBPTAMT_SUB.OptionsColumn.AllowEdit = false;
            this.gcJSJBPTAMT_SUB.Visible = true;
            this.gcJSJBPTAMT_SUB.VisibleIndex = 5;
            this.gcJSJBPTAMT_SUB.Width = 85;
            // 
            // gcJSAMT1
            // 
            this.gcJSAMT1.Caption = "1항 결정금액";
            this.gcJSAMT1.DisplayFormat.FormatString = "#,##0";
            this.gcJSAMT1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSAMT1.FieldName = "JSAMT1";
            this.gcJSAMT1.Name = "gcJSAMT1";
            this.gcJSAMT1.OptionsColumn.AllowEdit = false;
            this.gcJSAMT1.Visible = true;
            this.gcJSAMT1.VisibleIndex = 6;
            this.gcJSAMT1.Width = 85;
            // 
            // gcJSAMT2
            // 
            this.gcJSAMT2.Caption = "2항 결정금액";
            this.gcJSAMT2.DisplayFormat.FormatString = "#,##0";
            this.gcJSAMT2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJSAMT2.FieldName = "JSAMT2";
            this.gcJSAMT2.Name = "gcJSAMT2";
            this.gcJSAMT2.OptionsColumn.AllowEdit = false;
            this.gcJSAMT2.Visible = true;
            this.gcJSAMT2.VisibleIndex = 7;
            this.gcJSAMT2.Width = 85;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "결정내용";
            this.gcMEMO.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.OptionsColumn.AllowEdit = false;
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 8;
            this.gcMEMO.Width = 310;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gcMAINROW
            // 
            this.gcMAINROW.Caption = "MAINROW";
            this.gcMAINROW.FieldName = "MAINROW";
            this.gcMAINROW.Name = "gcMAINROW";
            // 
            // ADD0724E_JABO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 479);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtJsdemseq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnQueryNoLimit);
            this.Controls.Add(this.btnQueryOneWeek);
            this.Controls.Add(this.btnQueryOneMonth);
            this.Controls.Add(this.btnQueryThreeMonth);
            this.Controls.Add(this.btnQuerySixMonth);
            this.Controls.Add(this.btnQueryOneYear);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCnecno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTodt);
            this.Controls.Add(this.txtFrdt);
            this.Controls.Add(this.label1);
            this.Name = "ADD0724E_JABO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[자보]이의제기결과통보서(ADD0724E_JABO)";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.TextBox txtCnecno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQueryOneYear;
        private System.Windows.Forms.Button btnQuerySixMonth;
        private System.Windows.Forms.Button btnQueryThreeMonth;
        private System.Windows.Forms.Button btnQueryOneMonth;
        private System.Windows.Forms.Button btnQueryOneWeek;
        private System.Windows.Forms.Button btnQueryNoLimit;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtJsdemseq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbFmgbnN150;
        private System.Windows.Forms.RadioButton rbFmgbnN130;
        private System.Windows.Forms.RadioButton rbFmgbnAll;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grdMainView;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSDEMSEQ;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSREDAY;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSTOTAMT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSTTTAMT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSJBPTAMT;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcCNECNO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcDEMSEQ;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcDEMNO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSREDEPT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSSAU_MEMO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcFMGBNNM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJRFG;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcVERSION;
        private DevExpress.XtraGrid.GridControl grdSub;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSubView;
        private DevExpress.XtraGrid.Columns.GridColumn gcEPRTNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPPRNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcJSAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcJSTTTAMT_SUB;
        private DevExpress.XtraGrid.Columns.GridColumn gcJSJBPTAMT_SUB;
        private DevExpress.XtraGrid.Columns.GridColumn gcJSAMT1;
        private DevExpress.XtraGrid.Columns.GridColumn gcJSAMT2;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcFMGBN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcDCOUNT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcJSYYSEQ;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.Columns.GridColumn gcMAINROW;
        private System.Windows.Forms.Button btnPrintSub;
    }
}


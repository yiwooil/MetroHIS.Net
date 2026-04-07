namespace ADD0711E
{
    partial class ADD0711E
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
            this.components = new System.ComponentModel.Container();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnQueryNoLimit = new System.Windows.Forms.Button();
            this.btnQueryOneWeek = new System.Windows.Forms.Button();
            this.btnQueryOneMonth = new System.Windows.Forms.Button();
            this.btnQueryThreeMonth = new System.Windows.Forms.Button();
            this.btnQuerySixMonth = new System.Windows.Forms.Button();
            this.btnQueryOneYear = new System.Windows.Forms.Button();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCnecno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnPrintSub = new System.Windows.Forms.Button();
            this.grdSub = new DevExpress.XtraGrid.GridControl();
            this.grdSubView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.txtInsmm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(868, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 186;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(794, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 185;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnQueryNoLimit
            // 
            this.btnQueryNoLimit.Location = new System.Drawing.Point(625, 6);
            this.btnQueryNoLimit.Name = "btnQueryNoLimit";
            this.btnQueryNoLimit.Size = new System.Drawing.Size(75, 23);
            this.btnQueryNoLimit.TabIndex = 184;
            this.btnQueryNoLimit.Text = "제한없음";
            this.btnQueryNoLimit.UseVisualStyleBackColor = true;
            this.btnQueryNoLimit.Click += new System.EventHandler(this.btnQueryNoLimit_Click);
            // 
            // btnQueryOneWeek
            // 
            this.btnQueryOneWeek.Location = new System.Drawing.Point(551, 6);
            this.btnQueryOneWeek.Name = "btnQueryOneWeek";
            this.btnQueryOneWeek.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneWeek.TabIndex = 183;
            this.btnQueryOneWeek.Text = "최근1주일";
            this.btnQueryOneWeek.UseVisualStyleBackColor = true;
            this.btnQueryOneWeek.Click += new System.EventHandler(this.btnQueryOneWeek_Click);
            // 
            // btnQueryOneMonth
            // 
            this.btnQueryOneMonth.Location = new System.Drawing.Point(477, 6);
            this.btnQueryOneMonth.Name = "btnQueryOneMonth";
            this.btnQueryOneMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneMonth.TabIndex = 182;
            this.btnQueryOneMonth.Text = "최근1개월";
            this.btnQueryOneMonth.UseVisualStyleBackColor = true;
            this.btnQueryOneMonth.Click += new System.EventHandler(this.btnQueryOneMonth_Click);
            // 
            // btnQueryThreeMonth
            // 
            this.btnQueryThreeMonth.Location = new System.Drawing.Point(403, 6);
            this.btnQueryThreeMonth.Name = "btnQueryThreeMonth";
            this.btnQueryThreeMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQueryThreeMonth.TabIndex = 181;
            this.btnQueryThreeMonth.Text = "최근3개월";
            this.btnQueryThreeMonth.UseVisualStyleBackColor = true;
            this.btnQueryThreeMonth.Click += new System.EventHandler(this.btnQueryThreeMonth_Click);
            // 
            // btnQuerySixMonth
            // 
            this.btnQuerySixMonth.Location = new System.Drawing.Point(329, 6);
            this.btnQuerySixMonth.Name = "btnQuerySixMonth";
            this.btnQuerySixMonth.Size = new System.Drawing.Size(75, 23);
            this.btnQuerySixMonth.TabIndex = 180;
            this.btnQuerySixMonth.Text = "최근6개월";
            this.btnQuerySixMonth.UseVisualStyleBackColor = true;
            this.btnQuerySixMonth.Click += new System.EventHandler(this.btnQuerySixMonth_Click);
            // 
            // btnQueryOneYear
            // 
            this.btnQueryOneYear.Location = new System.Drawing.Point(255, 6);
            this.btnQueryOneYear.Name = "btnQueryOneYear";
            this.btnQueryOneYear.Size = new System.Drawing.Size(75, 23);
            this.btnQueryOneYear.TabIndex = 179;
            this.btnQueryOneYear.Text = "최근1년";
            this.btnQueryOneYear.UseVisualStyleBackColor = true;
            this.btnQueryOneYear.Click += new System.EventHandler(this.btnQueryOneYear_Click);
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(198, 39);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(82, 21);
            this.txtDemno.TabIndex = 178;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 177;
            this.label3.Text = "청구번호";
            // 
            // txtCnecno
            // 
            this.txtCnecno.Location = new System.Drawing.Point(74, 38);
            this.txtCnecno.Name = "txtCnecno";
            this.txtCnecno.Size = new System.Drawing.Size(60, 21);
            this.txtCnecno.TabIndex = 176;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 175;
            this.label2.Text = "접수번호";
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(135, 6);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(60, 21);
            this.txtTodt.TabIndex = 174;
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(74, 6);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(60, 21);
            this.txtFrdt.TabIndex = 173;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 172;
            this.label1.Text = "접수일자";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(20, 69);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(935, 495);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 190;
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(935, 279);
            this.grdMain.TabIndex = 0;
            this.grdMain.ToolTipController = this.toolTipController1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn36,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25,
            this.gridColumn26,
            this.gridColumn27,
            this.gridColumn28,
            this.gridColumn37,
            this.gridColumn38});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.grdMainView.OptionsPrint.AutoWidth = false;
            this.grdMainView.OptionsSelection.MultiSelect = true;
            this.grdMainView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdMainView.OptionsView.AutoCalcPreviewLineCount = true;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            this.grdMainView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdMainView_RowCellStyle);
            this.grdMainView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdMainView_RowCellClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "신청구분";
            this.gridColumn1.FieldName = "CALLFGNM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "접수일자";
            this.gridColumn2.FieldName = "CNECTDD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 65;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "구분";
            this.gridColumn3.FieldName = "JSBSNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 35;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "접수번호";
            this.gridColumn4.FieldName = "CNECTNO";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "자격";
            this.gridColumn5.FieldName = "JBFGNM";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 35;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "분류";
            this.gridColumn36.FieldName = "JRDIVNM";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.AllowEdit = false;
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 5;
            this.gridColumn36.Width = 45;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "입외";
            this.gridColumn6.FieldName = "IOFGNM";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 35;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "분야";
            this.gridColumn7.FieldName = "JRKWAFGNM";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 35;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "청구번호";
            this.gridColumn8.FieldName = "DEMNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 95;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "의뢰기관";
            this.gridColumn9.FieldName = "SPTID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "접수구분";
            this.gridColumn10.FieldName = "CNECTFG";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 55;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "청구건수";
            this.gridColumn11.DisplayFormat.FormatString = "#,##0";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "DEMCNT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 60;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "청구금액";
            this.gridColumn12.DisplayFormat.FormatString = "#,##0";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn12.FieldName = "UNAMT";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "백미만청구*";
            this.gridColumn13.DisplayFormat.FormatString = "#,###";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn13.FieldName = "H010_BAKDNUNAMT_2";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 12;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "진료년월";
            this.gridColumn14.FieldName = "YYMM";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 13;
            this.gridColumn14.Width = 55;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "청구구분";
            this.gridColumn15.FieldName = "ADDZ1NM";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 14;
            this.gridColumn15.Width = 55;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "송신건수";
            this.gridColumn16.DisplayFormat.FormatString = "#,##0";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn16.FieldName = "H010_DEMCNT";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 15;
            this.gridColumn16.Width = 60;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "급여비용총액";
            this.gridColumn17.DisplayFormat.FormatString = "#,###";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn17.FieldName = "H010_TTAMT";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 16;
            this.gridColumn17.Width = 85;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "본인부담금";
            this.gridColumn18.DisplayFormat.FormatString = "#,###";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn18.FieldName = "H010_PTAMT";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 17;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "조합부담금";
            this.gridColumn19.DisplayFormat.FormatString = "#,###";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn19.FieldName = "H010_UNAMT";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 18;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "백미만총액";
            this.gridColumn20.DisplayFormat.FormatString = "#,###";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn20.FieldName = "H010_BAKDNTTAMT";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 19;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "백미만본인";
            this.gridColumn21.DisplayFormat.FormatString = "#,###";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn21.FieldName = "H010_BAKDNPTAMT";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 20;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "백미만조합";
            this.gridColumn22.DisplayFormat.FormatString = "#,###";
            this.gridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn22.FieldName = "H010_BAKDNUNAMT";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 21;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "참조란";
            this.gridColumn23.FieldName = "CNECTMM";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 22;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "요양기관";
            this.gridColumn24.FieldName = "HOSID";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "버전";
            this.gridColumn25.FieldName = "VERSION";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsColumn.AllowEdit = false;
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 23;
            this.gridColumn25.Width = 35;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "지원";
            this.gridColumn26.FieldName = "JIWONCD";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "서석번호";
            this.gridColumn27.FieldName = "FMNO";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "CALLFG";
            this.gridColumn28.FieldName = "CALLFG";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "H0601_CNT";
            this.gridColumn37.FieldName = "H0601_CNT";
            this.gridColumn37.Name = "gridColumn37";
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "H0801_CNT";
            this.gridColumn38.FieldName = "H0801_CNT";
            this.gridColumn38.Name = "gridColumn38";
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtMemo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnPrintSub);
            this.splitContainer2.Panel2.Controls.Add(this.grdSub);
            this.splitContainer2.Size = new System.Drawing.Size(935, 210);
            this.splitContainer2.SplitterDistance = 74;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Location = new System.Drawing.Point(0, 0);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(935, 74);
            this.txtMemo.TabIndex = 0;
            // 
            // btnPrintSub
            // 
            this.btnPrintSub.Location = new System.Drawing.Point(842, 0);
            this.btnPrintSub.Name = "btnPrintSub";
            this.btnPrintSub.Size = new System.Drawing.Size(75, 23);
            this.btnPrintSub.TabIndex = 187;
            this.btnPrintSub.Text = "출력";
            this.btnPrintSub.UseVisualStyleBackColor = true;
            this.btnPrintSub.Visible = false;
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
            this.grdSub.Size = new System.Drawing.Size(935, 130);
            this.grdSub.TabIndex = 1;
            this.grdSub.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSubView});
            // 
            // grdSubView
            // 
            this.grdSubView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn32,
            this.gridColumn33,
            this.gridColumn34,
            this.gridColumn35});
            this.grdSubView.GridControl = this.grdSub;
            this.grdSubView.Name = "grdSubView";
            this.grdSubView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.grdSubView.OptionsPrint.AutoWidth = false;
            this.grdSubView.OptionsSelection.MultiSelect = true;
            this.grdSubView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdSubView.OptionsView.AutoCalcPreviewLineCount = true;
            this.grdSubView.OptionsView.ColumnAutoWidth = false;
            this.grdSubView.OptionsView.RowAutoHeight = true;
            this.grdSubView.OptionsView.ShowGroupPanel = false;
            this.grdSubView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "명일련";
            this.gridColumn32.FieldName = "EPRTNO";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.AllowEdit = false;
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 0;
            this.gridColumn32.Width = 65;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "반송사유";
            this.gridColumn33.FieldName = "RCODE";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.OptionsColumn.AllowEdit = false;
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 1;
            this.gridColumn33.Width = 55;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "순번";
            this.gridColumn34.FieldName = "SEQ";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.OptionsColumn.AllowEdit = false;
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 2;
            this.gridColumn34.Width = 45;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "비고";
            this.gridColumn35.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn35.FieldName = "REMARK";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsColumn.AllowEdit = false;
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 3;
            this.gridColumn35.Width = 650;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // txtInsmm
            // 
            this.txtInsmm.Location = new System.Drawing.Point(334, 38);
            this.txtInsmm.Name = "txtInsmm";
            this.txtInsmm.Size = new System.Drawing.Size(82, 21);
            this.txtInsmm.TabIndex = 192;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 191;
            this.label4.Text = "청구월";
            // 
            // ADD0711E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 576);
            this.Controls.Add(this.txtInsmm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnPrint);
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
            this.Name = "ADD0711E";
            this.Text = "청구명세서등접수반송증(ADD0711E)";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnQueryNoLimit;
        private System.Windows.Forms.Button btnQueryOneWeek;
        private System.Windows.Forms.Button btnQueryOneMonth;
        private System.Windows.Forms.Button btnQueryThreeMonth;
        private System.Windows.Forms.Button btnQuerySixMonth;
        private System.Windows.Forms.Button btnQueryOneYear;
        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCnecno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtMemo;
        private DevExpress.XtraGrid.GridControl grdSub;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSubView;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn34;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private System.Windows.Forms.TextBox txtInsmm;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private System.Windows.Forms.Button btnPrintSub;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}


namespace ADD7005E
{
    partial class ADD7005E
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSub = new DevExpress.XtraGrid.GridControl();
            this.grdSubView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.btnTmpSend = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtCnecno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBillSno = new System.Windows.Forms.TextBox();
            this.txtCnectdd = new System.Windows.Forms.TextBox();
            this.chkNoSendFg = new System.Windows.Forms.CheckBox();
            this.cboRDate = new System.Windows.Forms.ComboBox();
            this.txtIofg = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkSendAll = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "청구번호";
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(70, 6);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(91, 21);
            this.txtDemno.TabIndex = 1;
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdMain.Location = new System.Drawing.Point(11, 33);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(650, 392);
            this.grdMain.TabIndex = 2;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn12,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.AllowCellMerge = true;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grdMainView_CustomDrawRowIndicator);
            this.grdMainView.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.grdMainView_CellMerge);
            this.grdMainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            this.grdMainView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdMainView_CustomDrawCell);
            this.grdMainView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grdMainView_PopupMenuShowing);
            this.grdMainView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdMainView_RowCellStyle);
            this.grdMainView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdMainView_RowCellClick);
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "S";
            this.gridColumn13.FieldName = "SEL";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 25;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "순번";
            this.gridColumn12.FieldName = "NO";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            this.gridColumn12.Width = 35;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "명일련";
            this.gridColumn1.FieldName = "EPRTNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "환자ID";
            this.gridColumn2.FieldName = "PID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "환자명";
            this.gridColumn3.FieldName = "PNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "성별";
            this.gridColumn4.FieldName = "PSEX_MF";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 45;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "생년월일";
            this.gridColumn7.FieldName = "RESID1";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "진료개시일";
            this.gridColumn8.FieldName = "STEDT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "진료과";
            this.gridColumn9.FieldName = "DPTCD";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 55;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "자격";
            this.gridColumn10.FieldName = "QFYCD";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 45;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "처리상태";
            this.gridColumn11.FieldName = "STATUS_NM";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 55;
            // 
            // grdSub
            // 
            this.grdSub.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSub.Location = new System.Drawing.Point(666, 33);
            this.grdSub.MainView = this.grdSubView;
            this.grdSub.Name = "grdSub";
            this.grdSub.Size = new System.Drawing.Size(379, 392);
            this.grdSub.TabIndex = 3;
            this.grdSub.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSubView});
            // 
            // grdSubView
            // 
            this.grdSubView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6});
            this.grdSubView.GridControl = this.grdSub;
            this.grdSubView.Name = "grdSubView";
            this.grdSubView.OptionsView.ColumnAutoWidth = false;
            this.grdSubView.OptionsView.ShowGroupPanel = false;
            this.grdSubView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdSubView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "검사명";
            this.gridColumn5.FieldName = "TEST_NM";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 230;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "검사결과";
            this.gridColumn6.FieldName = "TEST_VALUE_NM";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 105;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(526, 5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(98, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "대상자 조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(698, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "전송";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHosid.Location = new System.Drawing.Point(928, 431);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.ReadOnly = true;
            this.txtHosid.Size = new System.Drawing.Size(100, 21);
            this.txtHosid.TabIndex = 14;
            this.txtHosid.DoubleClick += new System.EventHandler(this.txtHosid_DoubleClick);
            // 
            // btnTmpSend
            // 
            this.btnTmpSend.Location = new System.Drawing.Point(623, 5);
            this.btnTmpSend.Name = "btnTmpSend";
            this.btnTmpSend.Size = new System.Drawing.Size(75, 23);
            this.btnTmpSend.TabIndex = 15;
            this.btnTmpSend.Text = "임시 전송";
            this.btnTmpSend.UseVisualStyleBackColor = true;
            this.btnTmpSend.Click += new System.EventHandler(this.btnTmpSend_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.Location = new System.Drawing.Point(12, 431);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(830, 41);
            this.txtMsg.TabIndex = 19;
            // 
            // txtCnecno
            // 
            this.txtCnecno.Location = new System.Drawing.Point(222, 6);
            this.txtCnecno.Name = "txtCnecno";
            this.txtCnecno.ReadOnly = true;
            this.txtCnecno.Size = new System.Drawing.Size(60, 21);
            this.txtCnecno.TabIndex = 21;
            this.txtCnecno.DoubleClick += new System.EventHandler(this.txtCnecno_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "접수번호";
            // 
            // txtBillSno
            // 
            this.txtBillSno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillSno.Location = new System.Drawing.Point(911, 431);
            this.txtBillSno.Name = "txtBillSno";
            this.txtBillSno.ReadOnly = true;
            this.txtBillSno.Size = new System.Drawing.Size(16, 21);
            this.txtBillSno.TabIndex = 22;
            this.txtBillSno.DoubleClick += new System.EventHandler(this.txtBillSno_DoubleClick);
            // 
            // txtCnectdd
            // 
            this.txtCnectdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCnectdd.Location = new System.Drawing.Point(848, 431);
            this.txtCnectdd.Name = "txtCnectdd";
            this.txtCnectdd.ReadOnly = true;
            this.txtCnectdd.Size = new System.Drawing.Size(61, 21);
            this.txtCnectdd.TabIndex = 23;
            this.txtCnectdd.DoubleClick += new System.EventHandler(this.txtCnectdd_DoubleClick);
            // 
            // chkNoSendFg
            // 
            this.chkNoSendFg.AutoSize = true;
            this.chkNoSendFg.Location = new System.Drawing.Point(430, 9);
            this.chkNoSendFg.Name = "chkNoSendFg";
            this.chkNoSendFg.Size = new System.Drawing.Size(96, 16);
            this.chkNoSendFg.TabIndex = 24;
            this.chkNoSendFg.Text = "전송제외포함";
            this.chkNoSendFg.UseVisualStyleBackColor = true;
            this.chkNoSendFg.CheckedChanged += new System.EventHandler(this.chkNoSendFg_CheckedChanged);
            // 
            // cboRDate
            // 
            this.cboRDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRDate.FormattingEnabled = true;
            this.cboRDate.Location = new System.Drawing.Point(779, 6);
            this.cboRDate.Name = "cboRDate";
            this.cboRDate.Size = new System.Drawing.Size(73, 20);
            this.cboRDate.TabIndex = 25;
            // 
            // txtIofg
            // 
            this.txtIofg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIofg.Location = new System.Drawing.Point(1029, 431);
            this.txtIofg.Name = "txtIofg";
            this.txtIofg.ReadOnly = true;
            this.txtIofg.Size = new System.Drawing.Size(16, 21);
            this.txtIofg.TabIndex = 26;
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(854, 5);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(39, 23);
            this.btnChange.TabIndex = 27;
            this.btnChange.Text = "변경";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(894, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(39, 23);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(285, 10);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(144, 16);
            this.chkAll.TabIndex = 29;
            this.chkAll.Text = "결과지없는환자도조회";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkSendAll
            // 
            this.chkSendAll.AutoSize = true;
            this.chkSendAll.Location = new System.Drawing.Point(32, 37);
            this.chkSendAll.Name = "chkSendAll";
            this.chkSendAll.Size = new System.Drawing.Size(15, 14);
            this.chkSendAll.TabIndex = 30;
            this.chkSendAll.UseVisualStyleBackColor = true;
            this.chkSendAll.CheckedChanged += new System.EventHandler(this.chkSendAll_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // ADD7005E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 476);
            this.Controls.Add(this.chkSendAll);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtIofg);
            this.Controls.Add(this.cboRDate);
            this.Controls.Add(this.chkNoSendFg);
            this.Controls.Add(this.txtCnectdd);
            this.Controls.Add(this.txtBillSno);
            this.Controls.Add(this.txtCnecno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnTmpSend);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdSub);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label1);
            this.Name = "ADD7005E";
            this.Text = "신경학적검사결과지제출(ADD7005E)";
            this.Load += new System.EventHandler(this.ADD7005E_Load);
            this.Activated += new System.EventHandler(this.ADD7005E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDemno;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.GridControl grdSub;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSubView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Button btnTmpSend;
        private System.Windows.Forms.TextBox txtMsg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.TextBox txtCnecno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBillSno;
        private System.Windows.Forms.TextBox txtCnectdd;
        private System.Windows.Forms.CheckBox chkNoSendFg;
        private System.Windows.Forms.ComboBox cboRDate;
        private System.Windows.Forms.TextBox txtIofg;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkAll;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private System.Windows.Forms.CheckBox chkSendAll;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}


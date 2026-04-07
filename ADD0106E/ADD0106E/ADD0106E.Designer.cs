namespace ADD0106E
{
    partial class ADD0106E
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
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkBoRyu = new System.Windows.Forms.CheckBox();
            this.txtAfterPid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAfterDptcd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.txtChangQF = new System.Windows.Forms.TextBox();
            this.txtAfterQfycd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAfterBdodt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYYMM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbQfy29 = new System.Windows.Forms.RadioButton();
            this.rbQfy38 = new System.Windows.Forms.RadioButton();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy3 = new System.Windows.Forms.RadioButton();
            this.rbQfy5 = new System.Windows.Forms.RadioButton();
            this.rbQfy2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYHgbn2 = new System.Windows.Forms.RadioButton();
            this.rbYHgbn1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Location = new System.Drawing.Point(10, 71);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(659, 365);
            this.grdMain.TabIndex = 58;
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
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn8,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdMainView_RowCellClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "청구월";
            this.gridColumn1.FieldName = "BDODT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "심번";
            this.gridColumn2.FieldName = "SIMNO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 55;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "명일련";
            this.gridColumn3.FieldName = "EPRTNO";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 55;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "환자ID";
            this.gridColumn4.FieldName = "PID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "환자명";
            this.gridColumn5.FieldName = "PNM";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "자격";
            this.gridColumn6.FieldName = "QFYCD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 35;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "진료과";
            this.gridColumn7.FieldName = "DPTCD";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 55;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "당월개시일";
            this.gridColumn9.FieldName = "STEDT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "상태";
            this.gridColumn10.FieldName = "DONFG";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 8;
            this.gridColumn10.Width = 45;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "청구번호";
            this.gridColumn8.FieldName = "DEMNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "JRBY";
            this.gridColumn11.FieldName = "JRBY";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "UNISQ";
            this.gridColumn12.FieldName = "UNISQ";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "SIMCS";
            this.gridColumn13.FieldName = "SIMCS";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.chkBoRyu);
            this.panel4.Controls.Add(this.txtAfterPid);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.txtAfterDptcd);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.cboQfycd);
            this.panel4.Controls.Add(this.txtChangQF);
            this.panel4.Controls.Add(this.txtAfterQfycd);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txtAfterBdodt);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(675, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(211, 365);
            this.panel4.TabIndex = 57;
            // 
            // chkBoRyu
            // 
            this.chkBoRyu.AutoSize = true;
            this.chkBoRyu.Location = new System.Drawing.Point(12, 173);
            this.chkBoRyu.Name = "chkBoRyu";
            this.chkBoRyu.Size = new System.Drawing.Size(192, 16);
            this.chkBoRyu.TabIndex = 21;
            this.chkBoRyu.Text = "원본을 [보류]상태로 만듭니다.";
            this.chkBoRyu.UseVisualStyleBackColor = true;
            // 
            // txtAfterPid
            // 
            this.txtAfterPid.Location = new System.Drawing.Point(60, 125);
            this.txtAfterPid.Name = "txtAfterPid";
            this.txtAfterPid.Size = new System.Drawing.Size(119, 21);
            this.txtAfterPid.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "환자ID";
            // 
            // txtAfterDptcd
            // 
            this.txtAfterDptcd.Location = new System.Drawing.Point(60, 98);
            this.txtAfterDptcd.Name = "txtAfterDptcd";
            this.txtAfterDptcd.Size = new System.Drawing.Size(119, 21);
            this.txtAfterDptcd.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "진료과";
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownHeight = 300;
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.DropDownWidth = 200;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.IntegralHeight = false;
            this.cboQfycd.ItemHeight = 12;
            this.cboQfycd.Location = new System.Drawing.Point(60, 72);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(119, 20);
            this.cboQfycd.TabIndex = 16;
            this.cboQfycd.SelectedIndexChanged += new System.EventHandler(this.cboQfycd_SelectedIndexChanged);
            // 
            // txtChangQF
            // 
            this.txtChangQF.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtChangQF.Location = new System.Drawing.Point(131, 45);
            this.txtChangQF.Name = "txtChangQF";
            this.txtChangQF.Size = new System.Drawing.Size(48, 21);
            this.txtChangQF.TabIndex = 15;
            // 
            // txtAfterQfycd
            // 
            this.txtAfterQfycd.Location = new System.Drawing.Point(60, 45);
            this.txtAfterQfycd.Name = "txtAfterQfycd";
            this.txtAfterQfycd.Size = new System.Drawing.Size(48, 21);
            this.txtAfterQfycd.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "자격";
            // 
            // txtAfterBdodt
            // 
            this.txtAfterBdodt.Location = new System.Drawing.Point(60, 18);
            this.txtAfterBdodt.Name = "txtAfterBdodt";
            this.txtAfterBdodt.Size = new System.Drawing.Size(119, 21);
            this.txtAfterBdodt.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "청구월";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(594, 42);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "실행";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(517, 42);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 55;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(268, 44);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.ReadOnly = true;
            this.txtPnm.Size = new System.Drawing.Size(69, 21);
            this.txtPnm.TabIndex = 54;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(200, 44);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(67, 21);
            this.txtPid.TabIndex = 52;
            this.txtPid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPid_KeyDown);
            this.txtPid.Leave += new System.EventHandler(this.txtPid_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "환자ID :";
            // 
            // txtYYMM
            // 
            this.txtYYMM.Location = new System.Drawing.Point(75, 44);
            this.txtYYMM.Name = "txtYYMM";
            this.txtYYMM.Size = new System.Drawing.Size(67, 21);
            this.txtYYMM.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "청구년월 :";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbQfy29);
            this.panel3.Controls.Add(this.rbQfy38);
            this.panel3.Controls.Add(this.rbQfy6);
            this.panel3.Controls.Add(this.rbQfy3);
            this.panel3.Controls.Add(this.rbQfy5);
            this.panel3.Controls.Add(this.rbQfy2);
            this.panel3.Location = new System.Drawing.Point(123, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(427, 27);
            this.panel3.TabIndex = 49;
            // 
            // rbQfy29
            // 
            this.rbQfy29.AutoSize = true;
            this.rbQfy29.Location = new System.Drawing.Point(339, 4);
            this.rbQfy29.Name = "rbQfy29";
            this.rbQfy29.Size = new System.Drawing.Size(71, 16);
            this.rbQfy29.TabIndex = 5;
            this.rbQfy29.Text = "보훈일반";
            this.rbQfy29.UseVisualStyleBackColor = true;
            // 
            // rbQfy38
            // 
            this.rbQfy38.AutoSize = true;
            this.rbQfy38.Location = new System.Drawing.Point(250, 4);
            this.rbQfy38.Name = "rbQfy38";
            this.rbQfy38.Size = new System.Drawing.Size(83, 16);
            this.rbQfy38.TabIndex = 4;
            this.rbQfy38.Text = "보호정신과";
            this.rbQfy38.UseVisualStyleBackColor = true;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(194, 4);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 3;
            this.rbQfy6.Text = "자보";
            this.rbQfy6.UseVisualStyleBackColor = true;
            // 
            // rbQfy3
            // 
            this.rbQfy3.AutoSize = true;
            this.rbQfy3.Location = new System.Drawing.Point(91, 4);
            this.rbQfy3.Name = "rbQfy3";
            this.rbQfy3.Size = new System.Drawing.Size(47, 16);
            this.rbQfy3.TabIndex = 1;
            this.rbQfy3.Text = "보호";
            this.rbQfy3.UseVisualStyleBackColor = true;
            // 
            // rbQfy5
            // 
            this.rbQfy5.AutoSize = true;
            this.rbQfy5.Location = new System.Drawing.Point(142, 4);
            this.rbQfy5.Name = "rbQfy5";
            this.rbQfy5.Size = new System.Drawing.Size(47, 16);
            this.rbQfy5.TabIndex = 2;
            this.rbQfy5.Text = "산재";
            this.rbQfy5.UseVisualStyleBackColor = true;
            // 
            // rbQfy2
            // 
            this.rbQfy2.AutoSize = true;
            this.rbQfy2.Checked = true;
            this.rbQfy2.Location = new System.Drawing.Point(10, 4);
            this.rbQfy2.Name = "rbQfy2";
            this.rbQfy2.Size = new System.Drawing.Size(75, 16);
            this.rbQfy2.TabIndex = 0;
            this.rbQfy2.TabStop = true;
            this.rbQfy2.Text = "보험,공상";
            this.rbQfy2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbYHgbn2);
            this.panel1.Controls.Add(this.rbYHgbn1);
            this.panel1.Location = new System.Drawing.Point(10, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 27);
            this.panel1.TabIndex = 48;
            // 
            // rbYHgbn2
            // 
            this.rbYHgbn2.AutoSize = true;
            this.rbYHgbn2.Location = new System.Drawing.Point(56, 4);
            this.rbYHgbn2.Name = "rbYHgbn2";
            this.rbYHgbn2.Size = new System.Drawing.Size(47, 16);
            this.rbYHgbn2.TabIndex = 1;
            this.rbYHgbn2.Text = "한방";
            this.rbYHgbn2.UseVisualStyleBackColor = true;
            // 
            // rbYHgbn1
            // 
            this.rbYHgbn1.AutoSize = true;
            this.rbYHgbn1.Checked = true;
            this.rbYHgbn1.Location = new System.Drawing.Point(8, 4);
            this.rbYHgbn1.Name = "rbYHgbn1";
            this.rbYHgbn1.Size = new System.Drawing.Size(47, 16);
            this.rbYHgbn1.TabIndex = 0;
            this.rbYHgbn1.TabStop = true;
            this.rbYHgbn1.Text = "양방";
            this.rbYHgbn1.UseVisualStyleBackColor = true;
            // 
            // ADD0106E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 448);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtYYMM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "ADD0106E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "외래SUMMARY외래대체(ADD0106E)";
            this.Load += new System.EventHandler(this.ADD0106E_Load);
            this.Activated += new System.EventHandler(this.ADD0106E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkBoRyu;
        private System.Windows.Forms.TextBox txtAfterPid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAfterDptcd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.TextBox txtChangQF;
        private System.Windows.Forms.TextBox txtAfterQfycd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAfterBdodt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYYMM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbQfy29;
        private System.Windows.Forms.RadioButton rbQfy38;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.RadioButton rbQfy3;
        private System.Windows.Forms.RadioButton rbQfy5;
        private System.Windows.Forms.RadioButton rbQfy2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbYHgbn2;
        private System.Windows.Forms.RadioButton rbYHgbn1;

    }
}


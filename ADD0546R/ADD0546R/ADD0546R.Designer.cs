namespace ADD0546R
{
    partial class ADD0546R
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.grdPtnt = new DevExpress.XtraGrid.GridControl();
            this.grdPtntView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbQfy51 = new System.Windows.Forms.RadioButton();
            this.rbQfy29 = new System.Windows.Forms.RadioButton();
            this.rbQfy38 = new System.Windows.Forms.RadioButton();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy3 = new System.Windows.Forms.RadioButton();
            this.rbQfy5 = new System.Windows.Forms.RadioButton();
            this.rbQfy2 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbIofg2 = new System.Windows.Forms.RadioButton();
            this.rbIofg1 = new System.Windows.Forms.RadioButton();
            this.txtTdate = new System.Windows.Forms.TextBox();
            this.txtFdate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDemfg2 = new System.Windows.Forms.CheckBox();
            this.chkDemfg1 = new System.Windows.Forms.CheckBox();
            this.chkDemfg0 = new System.Windows.Forms.CheckBox();
            this.chkSingo = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPtnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPtntView)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(9, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 431);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.grdMain);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(880, 402);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "코드별";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(3, 3);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(874, 396);
            this.grdMain.TabIndex = 0;
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
            this.gridColumn5});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "청구코드";
            this.gridColumn1.FieldName = "BGIHO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "수가코드";
            this.gridColumn2.FieldName = "PRICD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "수가명";
            this.gridColumn3.FieldName = "PRKNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 500;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "사용량";
            this.gridColumn4.DisplayFormat.FormatString = "#,###.##";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn4.FieldName = "TQTY";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "사용금액";
            this.gridColumn5.DisplayFormat.FormatString = "#,###";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "GUMAK";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 90;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.grdPtnt);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(880, 402);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "환자별";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // grdPtnt
            // 
            this.grdPtnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPtnt.Location = new System.Drawing.Point(3, 3);
            this.grdPtnt.MainView = this.grdPtntView;
            this.grdPtnt.Name = "grdPtnt";
            this.grdPtnt.Size = new System.Drawing.Size(874, 396);
            this.grdPtnt.TabIndex = 1;
            this.grdPtnt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPtntView});
            // 
            // grdPtntView
            // 
            this.grdPtntView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.grdPtntView.GridControl = this.grdPtnt;
            this.grdPtntView.Name = "grdPtntView";
            this.grdPtntView.OptionsView.ColumnAutoWidth = false;
            this.grdPtntView.OptionsView.ShowGroupPanel = false;
            this.grdPtntView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "환자ID";
            this.gridColumn6.FieldName = "PID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "환자명";
            this.gridColumn7.FieldName = "PNM";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "청구코드";
            this.gridColumn8.FieldName = "BGIHO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "수가코드";
            this.gridColumn9.FieldName = "PRICD";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "수가명";
            this.gridColumn10.FieldName = "PRKNM";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 350;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "사용량";
            this.gridColumn11.DisplayFormat.FormatString = "#,###.##";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "TQTY";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "사용금액";
            this.gridColumn12.DisplayFormat.FormatString = "#,###";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn12.FieldName = "GUMAK";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 90;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbQfy51);
            this.panel3.Controls.Add(this.rbQfy29);
            this.panel3.Controls.Add(this.rbQfy38);
            this.panel3.Controls.Add(this.rbQfy6);
            this.panel3.Controls.Add(this.rbQfy3);
            this.panel3.Controls.Add(this.rbQfy5);
            this.panel3.Controls.Add(this.rbQfy2);
            this.panel3.Location = new System.Drawing.Point(135, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(557, 27);
            this.panel3.TabIndex = 1;
            // 
            // rbQfy51
            // 
            this.rbQfy51.AutoSize = true;
            this.rbQfy51.Location = new System.Drawing.Point(194, 4);
            this.rbQfy51.Name = "rbQfy51";
            this.rbQfy51.Size = new System.Drawing.Size(65, 16);
            this.rbQfy51.TabIndex = 3;
            this.rbQfy51.Text = "산재100";
            this.rbQfy51.UseVisualStyleBackColor = true;
            // 
            // rbQfy29
            // 
            this.rbQfy29.AutoSize = true;
            this.rbQfy29.Location = new System.Drawing.Point(410, 4);
            this.rbQfy29.Name = "rbQfy29";
            this.rbQfy29.Size = new System.Drawing.Size(71, 16);
            this.rbQfy29.TabIndex = 6;
            this.rbQfy29.Text = "보훈일반";
            this.rbQfy29.UseVisualStyleBackColor = true;
            // 
            // rbQfy38
            // 
            this.rbQfy38.AutoSize = true;
            this.rbQfy38.Location = new System.Drawing.Point(321, 4);
            this.rbQfy38.Name = "rbQfy38";
            this.rbQfy38.Size = new System.Drawing.Size(83, 16);
            this.rbQfy38.TabIndex = 5;
            this.rbQfy38.Text = "보호정신과";
            this.rbQfy38.UseVisualStyleBackColor = true;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(265, 4);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 4;
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
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbIofg2);
            this.panel2.Controls.Add(this.rbIofg1);
            this.panel2.Location = new System.Drawing.Point(13, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(118, 27);
            this.panel2.TabIndex = 0;
            // 
            // rbIofg2
            // 
            this.rbIofg2.AutoSize = true;
            this.rbIofg2.Location = new System.Drawing.Point(57, 4);
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
            this.rbIofg1.Location = new System.Drawing.Point(8, 4);
            this.rbIofg1.Name = "rbIofg1";
            this.rbIofg1.Size = new System.Drawing.Size(47, 16);
            this.rbIofg1.TabIndex = 0;
            this.rbIofg1.TabStop = true;
            this.rbIofg1.Text = "외래";
            this.rbIofg1.UseVisualStyleBackColor = true;
            this.rbIofg1.CheckedChanged += new System.EventHandler(this.rbIofg1_CheckedChanged);
            // 
            // txtTdate
            // 
            this.txtTdate.Location = new System.Drawing.Point(164, 39);
            this.txtTdate.Name = "txtTdate";
            this.txtTdate.Size = new System.Drawing.Size(82, 21);
            this.txtTdate.TabIndex = 3;
            // 
            // txtFdate
            // 
            this.txtFdate.Location = new System.Drawing.Point(76, 39);
            this.txtFdate.Name = "txtFdate";
            this.txtFdate.Size = new System.Drawing.Size(82, 21);
            this.txtFdate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "청구년월";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(818, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 21;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(737, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkDemfg2);
            this.panel1.Controls.Add(this.chkDemfg1);
            this.panel1.Controls.Add(this.chkDemfg0);
            this.panel1.Location = new System.Drawing.Point(716, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 27);
            this.panel1.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = ")";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "(";
            // 
            // chkDemfg2
            // 
            this.chkDemfg2.AutoSize = true;
            this.chkDemfg2.Location = new System.Drawing.Point(124, 5);
            this.chkDemfg2.Name = "chkDemfg2";
            this.chkDemfg2.Size = new System.Drawing.Size(48, 16);
            this.chkDemfg2.TabIndex = 2;
            this.chkDemfg2.Text = "추가";
            this.chkDemfg2.UseVisualStyleBackColor = true;
            // 
            // chkDemfg1
            // 
            this.chkDemfg1.AutoSize = true;
            this.chkDemfg1.Location = new System.Drawing.Point(74, 5);
            this.chkDemfg1.Name = "chkDemfg1";
            this.chkDemfg1.Size = new System.Drawing.Size(48, 16);
            this.chkDemfg1.TabIndex = 1;
            this.chkDemfg1.Text = "보완";
            this.chkDemfg1.UseVisualStyleBackColor = true;
            // 
            // chkDemfg0
            // 
            this.chkDemfg0.AutoSize = true;
            this.chkDemfg0.Checked = true;
            this.chkDemfg0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDemfg0.Location = new System.Drawing.Point(10, 5);
            this.chkDemfg0.Name = "chkDemfg0";
            this.chkDemfg0.Size = new System.Drawing.Size(60, 16);
            this.chkDemfg0.TabIndex = 0;
            this.chkDemfg0.Text = "원청구";
            this.chkDemfg0.UseVisualStyleBackColor = true;
            // 
            // chkSingo
            // 
            this.chkSingo.AutoSize = true;
            this.chkSingo.Location = new System.Drawing.Point(401, 44);
            this.chkSingo.Name = "chkSingo";
            this.chkSingo.Size = new System.Drawing.Size(294, 16);
            this.chkSingo.TabIndex = 25;
            this.chkSingo.Text = "최근 2년이내에 신고되지 않은 재료만 조회합니다.";
            this.chkSingo.UseVisualStyleBackColor = true;
            // 
            // ADD0546R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 509);
            this.Controls.Add(this.chkSingo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtTdate);
            this.Controls.Add(this.txtFdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabControl1);
            this.Name = "ADD0546R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "재료대사용현황(ADD0546R)";
            this.Load += new System.EventHandler(this.ADD0546R_Load);
            this.Activated += new System.EventHandler(this.ADD0546R_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPtnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPtntView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbQfy29;
        private System.Windows.Forms.RadioButton rbQfy38;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.RadioButton rbQfy3;
        private System.Windows.Forms.RadioButton rbQfy5;
        private System.Windows.Forms.RadioButton rbQfy2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbIofg2;
        private System.Windows.Forms.RadioButton rbIofg1;
        private System.Windows.Forms.TextBox txtTdate;
        private System.Windows.Forms.TextBox txtFdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.GridControl grdPtnt;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPtntView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDemfg2;
        private System.Windows.Forms.CheckBox chkDemfg1;
        private System.Windows.Forms.CheckBox chkDemfg0;
        private System.Windows.Forms.CheckBox chkSingo;
        private System.Windows.Forms.RadioButton rbQfy51;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
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
    }
}


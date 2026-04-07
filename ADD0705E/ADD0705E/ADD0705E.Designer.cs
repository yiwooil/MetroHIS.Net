namespace ADD0705E
{
    partial class ADD0705E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD0705E));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYHgbn2 = new System.Windows.Forms.RadioButton();
            this.rbYHgbn1 = new System.Windows.Forms.RadioButton();
            this.txtDemdd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDemdd = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDemnm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAcpnm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRcvid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSnddt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.cboPosOption = new System.Windows.Forms.ComboBox();
            this.btnRcvid = new System.Windows.Forms.Button();
            this.panRcvid = new System.Windows.Forms.Panel();
            this.btnRcvidCancel = new System.Windows.Forms.Button();
            this.btnRcvidSel = new System.Windows.Forms.Button();
            this.cboRcvid = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.panRcvid.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbYHgbn2);
            this.panel1.Controls.Add(this.rbYHgbn1);
            this.panel1.Location = new System.Drawing.Point(231, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 27);
            this.panel1.TabIndex = 7;
            // 
            // rbYHgbn2
            // 
            this.rbYHgbn2.AutoSize = true;
            this.rbYHgbn2.Location = new System.Drawing.Point(61, 4);
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
            // txtDemdd
            // 
            this.txtDemdd.Location = new System.Drawing.Point(74, 9);
            this.txtDemdd.Name = "txtDemdd";
            this.txtDemdd.Size = new System.Drawing.Size(53, 21);
            this.txtDemdd.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "구입년도 :";
            // 
            // cboDemdd
            // 
            this.cboDemdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDemdd.FormattingEnabled = true;
            this.cboDemdd.Items.AddRange(new object[] {
            "1/4 분기",
            "2/4 분기",
            "3/4 분기",
            "4/4 분기"});
            this.cboDemdd.Location = new System.Drawing.Point(128, 9);
            this.cboDemdd.Name = "cboDemdd";
            this.cboDemdd.Size = new System.Drawing.Size(97, 20);
            this.cboDemdd.TabIndex = 16;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(454, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(14, 38);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(972, 410);
            this.grdMain.TabIndex = 18;
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
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "S";
            this.gridColumn1.FieldName = "SEL";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 25;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "코드";
            this.gridColumn2.FieldName = "ITEMCD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "품명";
            this.gridColumn3.FieldName = "ITEMINFO";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 130;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "규격";
            this.gridColumn4.FieldName = "STDSIZE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 55;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "단위";
            this.gridColumn5.FieldName = "UNIT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 55;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "구입일자";
            this.gridColumn6.FieldName = "BUYDT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 65;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "최초";
            this.gridColumn7.FieldName = "FSTBUYFG";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 35;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "수량";
            this.gridColumn8.DisplayFormat.FormatString = "#,###.##";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn8.FieldName = "BUYQTY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 55;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "금액";
            this.gridColumn9.DisplayFormat.FormatString = "#,###";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn9.FieldName = "BUYTOTAMT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "단가";
            this.gridColumn10.DisplayFormat.FormatString = "#,###";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "BUYAMT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 65;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "가중평가";
            this.gridColumn11.DisplayFormat.FormatString = "#,###.######";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn11.FieldName = "ADDAVR";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 65;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "구입업소";
            this.gridColumn12.FieldName = "BUSSCD";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            this.gridColumn12.Width = 85;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "구입업소명";
            this.gridColumn13.FieldName = "BUSSNM";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 12;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "참고사항";
            this.gridColumn14.FieldName = "MEMO";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 13;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "gridColumn15";
            this.gridColumn15.FieldName = "PRODCM";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHosid.Location = new System.Drawing.Point(456, 454);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(53, 21);
            this.txtHosid.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "요양기관기호 :";
            // 
            // txtDemnm
            // 
            this.txtDemnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDemnm.Location = new System.Drawing.Point(565, 454);
            this.txtDemnm.Name = "txtDemnm";
            this.txtDemnm.Size = new System.Drawing.Size(53, 21);
            this.txtDemnm.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "작성자 :";
            // 
            // txtAcpnm
            // 
            this.txtAcpnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcpnm.Location = new System.Drawing.Point(673, 454);
            this.txtAcpnm.Name = "txtAcpnm";
            this.txtAcpnm.Size = new System.Drawing.Size(53, 21);
            this.txtAcpnm.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "확인자 :";
            // 
            // txtRcvid
            // 
            this.txtRcvid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRcvid.Location = new System.Drawing.Point(806, 455);
            this.txtRcvid.Name = "txtRcvid";
            this.txtRcvid.Size = new System.Drawing.Size(53, 21);
            this.txtRcvid.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(731, 459);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "수신인 :";
            // 
            // txtSnddt
            // 
            this.txtSnddt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSnddt.Location = new System.Drawing.Point(932, 454);
            this.txtSnddt.Name = "txtSnddt";
            this.txtSnddt.Size = new System.Drawing.Size(53, 21);
            this.txtSnddt.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(868, 458);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 12);
            this.label6.TabIndex = 28;
            this.label6.Text = "송신일자 :";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(34, 41);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 29;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(529, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "생성";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 457);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "폴더 :";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFolder.BackColor = System.Drawing.SystemColors.Window;
            this.txtFolder.Location = new System.Drawing.Point(148, 454);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(160, 21);
            this.txtFolder.TabIndex = 82;
            // 
            // cboPosOption
            // 
            this.cboPosOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboPosOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPosOption.FormattingEnabled = true;
            this.cboPosOption.Items.AddRange(new object[] {
            "자동",
            "심평원",
            "KT"});
            this.cboPosOption.Location = new System.Drawing.Point(15, 454);
            this.cboPosOption.Name = "cboPosOption";
            this.cboPosOption.Size = new System.Drawing.Size(89, 20);
            this.cboPosOption.TabIndex = 84;
            this.cboPosOption.SelectedIndexChanged += new System.EventHandler(this.cboPosOption_SelectedIndexChanged);
            // 
            // btnRcvid
            // 
            this.btnRcvid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRcvid.Image = ((System.Drawing.Image)(resources.GetObject("btnRcvid.Image")));
            this.btnRcvid.Location = new System.Drawing.Point(781, 454);
            this.btnRcvid.Name = "btnRcvid";
            this.btnRcvid.Size = new System.Drawing.Size(23, 23);
            this.btnRcvid.TabIndex = 85;
            this.btnRcvid.UseVisualStyleBackColor = true;
            this.btnRcvid.Click += new System.EventHandler(this.btnRcvid_Click);
            // 
            // panRcvid
            // 
            this.panRcvid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panRcvid.Controls.Add(this.btnRcvidCancel);
            this.panRcvid.Controls.Add(this.btnRcvidSel);
            this.panRcvid.Controls.Add(this.cboRcvid);
            this.panRcvid.Location = new System.Drawing.Point(401, 336);
            this.panRcvid.Name = "panRcvid";
            this.panRcvid.Size = new System.Drawing.Size(225, 82);
            this.panRcvid.TabIndex = 86;
            this.panRcvid.Visible = false;
            // 
            // btnRcvidCancel
            // 
            this.btnRcvidCancel.Location = new System.Drawing.Point(135, 48);
            this.btnRcvidCancel.Name = "btnRcvidCancel";
            this.btnRcvidCancel.Size = new System.Drawing.Size(75, 23);
            this.btnRcvidCancel.TabIndex = 88;
            this.btnRcvidCancel.Text = "취소";
            this.btnRcvidCancel.UseVisualStyleBackColor = true;
            this.btnRcvidCancel.Click += new System.EventHandler(this.btnRcvidCancel_Click);
            // 
            // btnRcvidSel
            // 
            this.btnRcvidSel.Location = new System.Drawing.Point(12, 48);
            this.btnRcvidSel.Name = "btnRcvidSel";
            this.btnRcvidSel.Size = new System.Drawing.Size(120, 23);
            this.btnRcvidSel.TabIndex = 87;
            this.btnRcvidSel.Text = "선택";
            this.btnRcvidSel.UseVisualStyleBackColor = true;
            this.btnRcvidSel.Click += new System.EventHandler(this.btnRcvidSel_Click);
            // 
            // cboRcvid
            // 
            this.cboRcvid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRcvid.FormattingEnabled = true;
            this.cboRcvid.Location = new System.Drawing.Point(12, 13);
            this.cboRcvid.Name = "cboRcvid";
            this.cboRcvid.Size = new System.Drawing.Size(200, 20);
            this.cboRcvid.TabIndex = 86;
            // 
            // ADD0705E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 488);
            this.Controls.Add(this.panRcvid);
            this.Controls.Add(this.btnRcvid);
            this.Controls.Add(this.cboPosOption);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.txtSnddt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRcvid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAcpnm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDemnm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.cboDemdd);
            this.Controls.Add(this.txtDemdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "ADD0705E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "의약품구입내역목록표생성(ADD0705E)";
            this.Load += new System.EventHandler(this.ADD0705E_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.panRcvid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbYHgbn2;
        private System.Windows.Forms.RadioButton rbYHgbn1;
        private System.Windows.Forms.TextBox txtDemdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDemdd;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
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
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDemnm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAcpnm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRcvid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSnddt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ComboBox cboPosOption;
        private System.Windows.Forms.Button btnRcvid;
        private System.Windows.Forms.Panel panRcvid;
        private System.Windows.Forms.ComboBox cboRcvid;
        private System.Windows.Forms.Button btnRcvidCancel;
        private System.Windows.Forms.Button btnRcvidSel;
    }
}


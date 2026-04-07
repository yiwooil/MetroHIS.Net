namespace ADD8002E
{
    partial class ADD8002E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD8002E));
            this.btnNewReqno = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScnt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDdnm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFmno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReqno = new System.Windows.Forms.Button();
            this.txtReqno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSEL_GRDMAIN = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcITEMCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMINFO1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMINFO2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMINFO3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTDSIZE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUSINESSCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGRADENM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRESNDDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUYDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUYQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUYAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNITCOST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSaveReqno = new System.Windows.Forms.Button();
            this.btnDelReqno = new System.Windows.Forms.Button();
            this.btnDelRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.cboReqdiv = new System.Windows.Forms.ComboBox();
            this.lstReqno = new System.Windows.Forms.ListBox();
            this.cboCodegb = new System.Windows.Forms.ComboBox();
            this.btnMake = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSEL_GRDMAIN)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewReqno
            // 
            this.btnNewReqno.Location = new System.Drawing.Point(210, 11);
            this.btnNewReqno.Name = "btnNewReqno";
            this.btnNewReqno.Size = new System.Drawing.Size(114, 23);
            this.btnNewReqno.TabIndex = 87;
            this.btnNewReqno.Text = "신청번호 만들기";
            this.btnNewReqno.UseVisualStyleBackColor = true;
            this.btnNewReqno.Click += new System.EventHandler(this.btnNewReqno_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(403, 38);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(575, 21);
            this.txtMemo.TabIndex = 86;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 85;
            this.label7.Text = "참조란 :";
            // 
            // txtScnt
            // 
            this.txtScnt.Location = new System.Drawing.Point(911, 14);
            this.txtScnt.Name = "txtScnt";
            this.txtScnt.ReadOnly = true;
            this.txtScnt.Size = new System.Drawing.Size(68, 21);
            this.txtScnt.TabIndex = 84;
            this.txtScnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(829, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 83;
            this.label6.Text = "신고 총건수 :";
            // 
            // txtDdnm
            // 
            this.txtDdnm.Location = new System.Drawing.Point(730, 14);
            this.txtDdnm.Name = "txtDdnm";
            this.txtDdnm.Size = new System.Drawing.Size(70, 21);
            this.txtDdnm.TabIndex = 82;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(677, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 81;
            this.label5.Text = "담당자 :";
            // 
            // txtHosid
            // 
            this.txtHosid.Location = new System.Drawing.Point(581, 13);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(73, 21);
            this.txtHosid.TabIndex = 80;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(490, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "요양기관기호 :";
            // 
            // txtFmno
            // 
            this.txtFmno.Location = new System.Drawing.Point(403, 12);
            this.txtFmno.Name = "txtFmno";
            this.txtFmno.Size = new System.Drawing.Size(70, 21);
            this.txtFmno.TabIndex = 78;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 77;
            this.label3.Text = "서식번호 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "신청번호 :";
            // 
            // btnReqno
            // 
            this.btnReqno.Image = ((System.Drawing.Image)(resources.GetObject("btnReqno.Image")));
            this.btnReqno.Location = new System.Drawing.Point(181, 11);
            this.btnReqno.Name = "btnReqno";
            this.btnReqno.Size = new System.Drawing.Size(23, 23);
            this.btnReqno.TabIndex = 75;
            this.btnReqno.UseVisualStyleBackColor = true;
            this.btnReqno.Click += new System.EventHandler(this.btnReqno_Click);
            // 
            // txtReqno
            // 
            this.txtReqno.Location = new System.Drawing.Point(78, 11);
            this.txtReqno.Name = "txtReqno";
            this.txtReqno.Size = new System.Drawing.Size(102, 21);
            this.txtReqno.TabIndex = 74;
            this.txtReqno.TextChanged += new System.EventHandler(this.txtReqno_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 88;
            this.label2.Text = "신고구분 :";
            // 
            // grdMain
            // 
            this.grdMain.Location = new System.Drawing.Point(13, 94);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkSEL_GRDMAIN});
            this.grdMain.Size = new System.Drawing.Size(967, 397);
            this.grdMain.TabIndex = 90;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSEL,
            this.gcITEMCD,
            this.gcITEMINFO1,
            this.gcITEMINFO2,
            this.gcITEMINFO3,
            this.gcSTDSIZE,
            this.gcUNIT,
            this.gcBUSINESSCD,
            this.gcGRADENM,
            this.gcPRESNDDIV,
            this.gcBUYDT,
            this.gcBUYQTY,
            this.gcBUYAMT,
            this.gcUNITCOST});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grdMainView_ValidatingEditor);
            // 
            // gcSEL
            // 
            this.gcSEL.Caption = "S";
            this.gcSEL.ColumnEdit = this.chkSEL_GRDMAIN;
            this.gcSEL.FieldName = "SEL";
            this.gcSEL.Name = "gcSEL";
            this.gcSEL.Visible = true;
            this.gcSEL.VisibleIndex = 0;
            this.gcSEL.Width = 25;
            // 
            // chkSEL_GRDMAIN
            // 
            this.chkSEL_GRDMAIN.AutoHeight = false;
            this.chkSEL_GRDMAIN.Caption = "";
            this.chkSEL_GRDMAIN.Name = "chkSEL_GRDMAIN";
            // 
            // gcITEMCD
            // 
            this.gcITEMCD.Caption = "품목코드";
            this.gcITEMCD.FieldName = "ITEMCD";
            this.gcITEMCD.Name = "gcITEMCD";
            this.gcITEMCD.Visible = true;
            this.gcITEMCD.VisibleIndex = 1;
            // 
            // gcITEMINFO1
            // 
            this.gcITEMINFO1.Caption = "품명";
            this.gcITEMINFO1.FieldName = "ITEMINFO1";
            this.gcITEMINFO1.Name = "gcITEMINFO1";
            this.gcITEMINFO1.Visible = true;
            this.gcITEMINFO1.VisibleIndex = 2;
            // 
            // gcITEMINFO2
            // 
            this.gcITEMINFO2.Caption = "제조회사명";
            this.gcITEMINFO2.FieldName = "ITEMINFO2";
            this.gcITEMINFO2.Name = "gcITEMINFO2";
            this.gcITEMINFO2.Visible = true;
            this.gcITEMINFO2.VisibleIndex = 3;
            // 
            // gcITEMINFO3
            // 
            this.gcITEMINFO3.Caption = "수입업소명";
            this.gcITEMINFO3.FieldName = "ITEMINFO3";
            this.gcITEMINFO3.Name = "gcITEMINFO3";
            this.gcITEMINFO3.Visible = true;
            this.gcITEMINFO3.VisibleIndex = 4;
            // 
            // gcSTDSIZE
            // 
            this.gcSTDSIZE.Caption = "규격";
            this.gcSTDSIZE.FieldName = "STDSIZE";
            this.gcSTDSIZE.Name = "gcSTDSIZE";
            this.gcSTDSIZE.Visible = true;
            this.gcSTDSIZE.VisibleIndex = 5;
            this.gcSTDSIZE.Width = 55;
            // 
            // gcUNIT
            // 
            this.gcUNIT.Caption = "단위";
            this.gcUNIT.FieldName = "UNIT";
            this.gcUNIT.Name = "gcUNIT";
            this.gcUNIT.Visible = true;
            this.gcUNIT.VisibleIndex = 6;
            this.gcUNIT.Width = 45;
            // 
            // gcBUSINESSCD
            // 
            this.gcBUSINESSCD.Caption = "사업자번호";
            this.gcBUSINESSCD.FieldName = "BUSINESSCD";
            this.gcBUSINESSCD.Name = "gcBUSINESSCD";
            this.gcBUSINESSCD.Visible = true;
            this.gcBUSINESSCD.VisibleIndex = 7;
            // 
            // gcGRADENM
            // 
            this.gcGRADENM.Caption = "구입기관상호";
            this.gcGRADENM.FieldName = "TRADENM";
            this.gcGRADENM.Name = "gcGRADENM";
            this.gcGRADENM.Visible = true;
            this.gcGRADENM.VisibleIndex = 8;
            this.gcGRADENM.Width = 85;
            // 
            // gcPRESNDDIV
            // 
            this.gcPRESNDDIV.Caption = "선납품구분";
            this.gcPRESNDDIV.FieldName = "PRESNDDIV";
            this.gcPRESNDDIV.Name = "gcPRESNDDIV";
            this.gcPRESNDDIV.Visible = true;
            this.gcPRESNDDIV.VisibleIndex = 9;
            this.gcPRESNDDIV.Width = 70;
            // 
            // gcBUYDT
            // 
            this.gcBUYDT.Caption = "구입일자";
            this.gcBUYDT.FieldName = "BUYDT";
            this.gcBUYDT.Name = "gcBUYDT";
            this.gcBUYDT.Visible = true;
            this.gcBUYDT.VisibleIndex = 10;
            this.gcBUYDT.Width = 70;
            // 
            // gcBUYQTY
            // 
            this.gcBUYQTY.Caption = "구입량";
            this.gcBUYQTY.FieldName = "BUYQTY";
            this.gcBUYQTY.Name = "gcBUYQTY";
            this.gcBUYQTY.Visible = true;
            this.gcBUYQTY.VisibleIndex = 11;
            this.gcBUYQTY.Width = 55;
            // 
            // gcBUYAMT
            // 
            this.gcBUYAMT.Caption = "구입가";
            this.gcBUYAMT.FieldName = "BUYAMT";
            this.gcBUYAMT.Name = "gcBUYAMT";
            this.gcBUYAMT.Visible = true;
            this.gcBUYAMT.VisibleIndex = 12;
            // 
            // gcUNITCOST
            // 
            this.gcUNITCOST.Caption = "개당단가";
            this.gcUNITCOST.FieldName = "UNITCOST";
            this.gcUNITCOST.Name = "gcUNITCOST";
            this.gcUNITCOST.Visible = true;
            this.gcUNITCOST.VisibleIndex = 13;
            this.gcUNITCOST.Width = 65;
            // 
            // btnSaveReqno
            // 
            this.btnSaveReqno.Location = new System.Drawing.Point(819, 65);
            this.btnSaveReqno.Name = "btnSaveReqno";
            this.btnSaveReqno.Size = new System.Drawing.Size(79, 23);
            this.btnSaveReqno.TabIndex = 94;
            this.btnSaveReqno.Text = "저장";
            this.btnSaveReqno.UseVisualStyleBackColor = true;
            this.btnSaveReqno.Click += new System.EventHandler(this.btnSaveReqno_Click);
            // 
            // btnDelReqno
            // 
            this.btnDelReqno.Location = new System.Drawing.Point(899, 65);
            this.btnDelReqno.Name = "btnDelReqno";
            this.btnDelReqno.Size = new System.Drawing.Size(79, 23);
            this.btnDelReqno.TabIndex = 93;
            this.btnDelReqno.Text = "삭제";
            this.btnDelReqno.UseVisualStyleBackColor = true;
            this.btnDelReqno.Click += new System.EventHandler(this.btnDelReqno_Click);
            // 
            // btnDelRow
            // 
            this.btnDelRow.Location = new System.Drawing.Point(93, 64);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(79, 23);
            this.btnDelRow.TabIndex = 92;
            this.btnDelRow.Text = "행삭제";
            this.btnDelRow.UseVisualStyleBackColor = true;
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(12, 64);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(79, 23);
            this.btnAddRow.TabIndex = 91;
            this.btnAddRow.Text = "행추가";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // cboReqdiv
            // 
            this.cboReqdiv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReqdiv.FormattingEnabled = true;
            this.cboReqdiv.Items.AddRange(new object[] {
            "진료재료",
            "수입,원료약"});
            this.cboReqdiv.Location = new System.Drawing.Point(78, 38);
            this.cboReqdiv.Name = "cboReqdiv";
            this.cboReqdiv.Size = new System.Drawing.Size(125, 20);
            this.cboReqdiv.TabIndex = 95;
            // 
            // lstReqno
            // 
            this.lstReqno.FormattingEnabled = true;
            this.lstReqno.ItemHeight = 12;
            this.lstReqno.Location = new System.Drawing.Point(78, 120);
            this.lstReqno.Name = "lstReqno";
            this.lstReqno.Size = new System.Drawing.Size(126, 196);
            this.lstReqno.TabIndex = 96;
            this.lstReqno.Visible = false;
            this.lstReqno.DoubleClick += new System.EventHandler(this.lstReqno_DoubleClick);
            // 
            // cboCodegb
            // 
            this.cboCodegb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCodegb.FormattingEnabled = true;
            this.cboCodegb.Items.AddRange(new object[] {
            "국산보험등재약",
            "협약재료",
            "일반재료",
            "수입,원료약"});
            this.cboCodegb.Location = new System.Drawing.Point(210, 38);
            this.cboCodegb.Name = "cboCodegb";
            this.cboCodegb.Size = new System.Drawing.Size(115, 20);
            this.cboCodegb.TabIndex = 97;
            // 
            // btnMake
            // 
            this.btnMake.Location = new System.Drawing.Point(706, 65);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(112, 23);
            this.btnMake.TabIndex = 98;
            this.btnMake.Text = "송신 파일 생성";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // ADD8002E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 503);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.cboCodegb);
            this.Controls.Add(this.lstReqno);
            this.Controls.Add(this.cboReqdiv);
            this.Controls.Add(this.btnSaveReqno);
            this.Controls.Add(this.btnDelReqno);
            this.Controls.Add(this.btnDelRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNewReqno);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtScnt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDdnm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFmno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReqno);
            this.Controls.Add(this.txtReqno);
            this.Name = "ADD8002E";
            this.Text = "산재보험구입신고서(ADD8002E)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSEL_GRDMAIN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewReqno;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtScnt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDdnm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFmno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReqno;
        private System.Windows.Forms.TextBox txtReqno;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Button btnSaveReqno;
        private System.Windows.Forms.Button btnDelReqno;
        private System.Windows.Forms.Button btnDelRow;
        private System.Windows.Forms.Button btnAddRow;
        private DevExpress.XtraGrid.Columns.GridColumn gcSEL;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMINFO1;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMINFO2;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMINFO3;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDSIZE;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNIT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUSINESSCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcGRADENM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRESNDDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUYDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUYQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUYAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNITCOST;
        private System.Windows.Forms.ComboBox cboReqdiv;
        private System.Windows.Forms.ListBox lstReqno;
        private System.Windows.Forms.ComboBox cboCodegb;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSEL_GRDMAIN;
        private System.Windows.Forms.Button btnMake;
    }
}


namespace ADD0348Q
{
    partial class ADD0348Q
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
            this.txtFrDt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToDt = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDODT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcS41QFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOUTSEQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEDICODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRKNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDANGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDDAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.chkOpt1 = new System.Windows.Forms.CheckBox();
            this.chkDcSkip = new System.Windows.Forms.CheckBox();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFrDt
            // 
            this.txtFrDt.Location = new System.Drawing.Point(74, 12);
            this.txtFrDt.Name = "txtFrDt";
            this.txtFrDt.Size = new System.Drawing.Size(70, 21);
            this.txtFrDt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "조회기간 :";
            // 
            // txtToDt
            // 
            this.txtToDt.Location = new System.Drawing.Point(146, 12);
            this.txtToDt.Name = "txtToDt";
            this.txtToDt.Size = new System.Drawing.Size(70, 21);
            this.txtToDt.TabIndex = 6;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(851, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(773, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 39);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(995, 450);
            this.grdMain.TabIndex = 17;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPID,
            this.gcPNM,
            this.gcBEDEDT,
            this.gcBEDODT,
            this.gcQFYCD,
            this.gcS41QFYCD,
            this.gridColumn1,
            this.gridColumn2,
            this.gcOUTSEQ,
            this.gcPRICD,
            this.gcEDICODE,
            this.gcPRKNM,
            this.gcDANGA,
            this.gcDQTY,
            this.gcDDAY});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.IndicatorWidth = 40;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grdMainView_CustomDrawRowIndicator);
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.AllowEdit = false;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 0;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            this.gcPNM.Width = 65;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.AllowEdit = false;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 2;
            this.gcBEDEDT.Width = 65;
            // 
            // gcBEDODT
            // 
            this.gcBEDODT.Caption = "퇴원일";
            this.gcBEDODT.FieldName = "BEDODT";
            this.gcBEDODT.Name = "gcBEDODT";
            this.gcBEDODT.OptionsColumn.AllowEdit = false;
            this.gcBEDODT.Visible = true;
            this.gcBEDODT.VisibleIndex = 3;
            this.gcBEDODT.Width = 65;
            // 
            // gcQFYCD
            // 
            this.gcQFYCD.AppearanceCell.Options.UseTextOptions = true;
            this.gcQFYCD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcQFYCD.Caption = "자격";
            this.gcQFYCD.FieldName = "QFYCD";
            this.gcQFYCD.Name = "gcQFYCD";
            this.gcQFYCD.OptionsColumn.AllowEdit = false;
            this.gcQFYCD.Visible = true;
            this.gcQFYCD.VisibleIndex = 4;
            this.gcQFYCD.Width = 35;
            // 
            // gcS41QFYCD
            // 
            this.gcS41QFYCD.AppearanceCell.Options.UseTextOptions = true;
            this.gcS41QFYCD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcS41QFYCD.Caption = "외래자격";
            this.gcS41QFYCD.FieldName = "S41QFYCD";
            this.gcS41QFYCD.Name = "gcS41QFYCD";
            this.gcS41QFYCD.OptionsColumn.AllowEdit = false;
            this.gcS41QFYCD.Visible = true;
            this.gcS41QFYCD.VisibleIndex = 5;
            this.gcS41QFYCD.Width = 55;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "처방의";
            this.gridColumn1.FieldName = "S41DRID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "처방의명";
            this.gridColumn2.FieldName = "S41DRNM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 7;
            this.gridColumn2.Width = 60;
            // 
            // gcOUTSEQ
            // 
            this.gcOUTSEQ.Caption = "처방전교부번호";
            this.gcOUTSEQ.FieldName = "OUTSEQ";
            this.gcOUTSEQ.Name = "gcOUTSEQ";
            this.gcOUTSEQ.OptionsColumn.AllowEdit = false;
            this.gcOUTSEQ.Visible = true;
            this.gcOUTSEQ.VisibleIndex = 8;
            this.gcOUTSEQ.Width = 110;
            // 
            // gcPRICD
            // 
            this.gcPRICD.Caption = "수가코드";
            this.gcPRICD.FieldName = "PRICD";
            this.gcPRICD.Name = "gcPRICD";
            this.gcPRICD.OptionsColumn.AllowEdit = false;
            this.gcPRICD.Visible = true;
            this.gcPRICD.VisibleIndex = 9;
            // 
            // gcEDICODE
            // 
            this.gcEDICODE.Caption = "EDI코드";
            this.gcEDICODE.FieldName = "EDICODE";
            this.gcEDICODE.Name = "gcEDICODE";
            this.gcEDICODE.OptionsColumn.AllowEdit = false;
            this.gcEDICODE.Visible = true;
            this.gcEDICODE.VisibleIndex = 10;
            // 
            // gcPRKNM
            // 
            this.gcPRKNM.Caption = "수가명";
            this.gcPRKNM.FieldName = "PRINM";
            this.gcPRKNM.Name = "gcPRKNM";
            this.gcPRKNM.OptionsColumn.AllowEdit = false;
            this.gcPRKNM.Visible = true;
            this.gcPRKNM.VisibleIndex = 11;
            this.gcPRKNM.Width = 85;
            // 
            // gcDANGA
            // 
            this.gcDANGA.AppearanceCell.Options.UseTextOptions = true;
            this.gcDANGA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDANGA.Caption = "단가";
            this.gcDANGA.FieldName = "DANGA";
            this.gcDANGA.Name = "gcDANGA";
            this.gcDANGA.OptionsColumn.AllowEdit = false;
            this.gcDANGA.Visible = true;
            this.gcDANGA.VisibleIndex = 12;
            this.gcDANGA.Width = 65;
            // 
            // gcDQTY
            // 
            this.gcDQTY.AppearanceCell.Options.UseTextOptions = true;
            this.gcDQTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDQTY.Caption = "투여량";
            this.gcDQTY.FieldName = "DQTY";
            this.gcDQTY.Name = "gcDQTY";
            this.gcDQTY.OptionsColumn.AllowEdit = false;
            this.gcDQTY.Visible = true;
            this.gcDQTY.VisibleIndex = 13;
            this.gcDQTY.Width = 60;
            // 
            // gcDDAY
            // 
            this.gcDDAY.AppearanceCell.Options.UseTextOptions = true;
            this.gcDDAY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDDAY.Caption = "일수";
            this.gcDDAY.FieldName = "DDAY";
            this.gcDDAY.Name = "gcDDAY";
            this.gcDDAY.OptionsColumn.AllowEdit = false;
            this.gcDDAY.Visible = true;
            this.gcDDAY.VisibleIndex = 14;
            this.gcDDAY.Width = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "자격 :";
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.Location = new System.Drawing.Point(267, 12);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(121, 20);
            this.cboQfycd.TabIndex = 19;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(447, 11);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(70, 21);
            this.txtPid.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "환자ID :";
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(519, 11);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.ReadOnly = true;
            this.txtPnm.Size = new System.Drawing.Size(70, 21);
            this.txtPnm.TabIndex = 22;
            // 
            // chkOpt1
            // 
            this.chkOpt1.AutoSize = true;
            this.chkOpt1.Location = new System.Drawing.Point(597, 14);
            this.chkOpt1.Name = "chkOpt1";
            this.chkOpt1.Size = new System.Drawing.Size(96, 16);
            this.chkOpt1.TabIndex = 23;
            this.chkOpt1.Text = "입원당일제외";
            this.chkOpt1.UseVisualStyleBackColor = true;
            // 
            // chkDcSkip
            // 
            this.chkDcSkip.AutoSize = true;
            this.chkDcSkip.Location = new System.Drawing.Point(697, 14);
            this.chkDcSkip.Name = "chkDcSkip";
            this.chkDcSkip.Size = new System.Drawing.Size(71, 16);
            this.chkDcSkip.TabIndex = 24;
            this.chkDcSkip.Text = "D/C제외";
            this.chkDcSkip.UseVisualStyleBackColor = true;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(929, 10);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(79, 23);
            this.btnExcel.TabIndex = 25;
            this.btnExcel.Text = "엑셀";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // ADD0348Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 501);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.chkDcSkip);
            this.Controls.Add(this.chkOpt1);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboQfycd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtToDt);
            this.Controls.Add(this.txtFrDt);
            this.Controls.Add(this.label1);
            this.Name = "ADD0348Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "입원중원외처방발생환자리스트(ADD0348Q)";
            this.Load += new System.EventHandler(this.ADD0348Q_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFrDt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtToDt;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDODT;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcS41QFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcOUTSEQ;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEDICODE;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRKNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDANGA;
        private DevExpress.XtraGrid.Columns.GridColumn gcDQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcDDAY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.CheckBox chkOpt1;
        private System.Windows.Forms.CheckBox chkDcSkip;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Button btnExcel;
    }
}


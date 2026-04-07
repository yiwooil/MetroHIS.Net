namespace ADD0251Q
{
    partial class ADD0251Q
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
            this.txtYYMM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcCANCELDT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcPID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcBEDODT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcQFYCD_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcDPTCD_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcRPDT_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcRPID_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcUNAMT_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcSIMNO_B = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcQFYCD_A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcDPTCD_A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcRPDT_A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcRPID_A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcUNAMT_A = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYYMM
            // 
            this.txtYYMM.Location = new System.Drawing.Point(72, 12);
            this.txtYYMM.Name = "txtYYMM";
            this.txtYYMM.Size = new System.Drawing.Size(62, 21);
            this.txtYYMM.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "청구월 :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(911, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(830, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 13;
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
            this.grdMain.Size = new System.Drawing.Size(978, 420);
            this.grdMain.TabIndex = 15;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gcCANCELDT,
            this.gcPID,
            this.gcPNM,
            this.gcBEDEDT,
            this.gcBEDODT,
            this.gcQFYCD_B,
            this.gcDPTCD_B,
            this.gcRPDT_B,
            this.gcRPID_B,
            this.gcUNAMT_B,
            this.gcSIMNO_B,
            this.gcQFYCD_A,
            this.gcDPTCD_A,
            this.gcRPDT_A,
            this.gcRPID_A,
            this.gcUNAMT_A});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.IndicatorWidth = 30;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grdMainView_CustomDrawRowIndicator);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.gcCANCELDT);
            this.gridBand1.Columns.Add(this.gcPID);
            this.gridBand1.Columns.Add(this.gcPNM);
            this.gridBand1.Columns.Add(this.gcBEDEDT);
            this.gridBand1.Columns.Add(this.gcBEDODT);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 360;
            // 
            // gcCANCELDT
            // 
            this.gcCANCELDT.Caption = "취소일";
            this.gcCANCELDT.FieldName = "CANCELDT";
            this.gcCANCELDT.Name = "gcCANCELDT";
            this.gcCANCELDT.OptionsColumn.ReadOnly = true;
            this.gcCANCELDT.Visible = true;
            this.gcCANCELDT.Width = 70;
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.ReadOnly = true;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.Width = 70;
            // 
            // gcBEDODT
            // 
            this.gcBEDODT.Caption = "퇴원일";
            this.gcBEDODT.FieldName = "BEDODT";
            this.gcBEDODT.Name = "gcBEDODT";
            this.gcBEDODT.OptionsColumn.ReadOnly = true;
            this.gcBEDODT.Visible = true;
            this.gcBEDODT.Width = 70;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "변경전";
            this.gridBand2.Columns.Add(this.gcQFYCD_B);
            this.gridBand2.Columns.Add(this.gcDPTCD_B);
            this.gridBand2.Columns.Add(this.gcRPDT_B);
            this.gridBand2.Columns.Add(this.gcRPID_B);
            this.gridBand2.Columns.Add(this.gcUNAMT_B);
            this.gridBand2.Columns.Add(this.gcSIMNO_B);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 311;
            // 
            // gcQFYCD_B
            // 
            this.gcQFYCD_B.AppearanceCell.Options.UseTextOptions = true;
            this.gcQFYCD_B.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcQFYCD_B.Caption = "자격";
            this.gcQFYCD_B.FieldName = "QFYCD_B";
            this.gcQFYCD_B.Name = "gcQFYCD_B";
            this.gcQFYCD_B.OptionsColumn.ReadOnly = true;
            this.gcQFYCD_B.Visible = true;
            this.gcQFYCD_B.Width = 33;
            // 
            // gcDPTCD_B
            // 
            this.gcDPTCD_B.AppearanceCell.Options.UseTextOptions = true;
            this.gcDPTCD_B.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDPTCD_B.Caption = "진료과";
            this.gcDPTCD_B.FieldName = "DPTCD_B";
            this.gcDPTCD_B.Name = "gcDPTCD_B";
            this.gcDPTCD_B.OptionsColumn.ReadOnly = true;
            this.gcDPTCD_B.Visible = true;
            this.gcDPTCD_B.Width = 50;
            // 
            // gcRPDT_B
            // 
            this.gcRPDT_B.Caption = "수납일";
            this.gcRPDT_B.FieldName = "RPDT_B";
            this.gcRPDT_B.Name = "gcRPDT_B";
            this.gcRPDT_B.OptionsColumn.ReadOnly = true;
            this.gcRPDT_B.Visible = true;
            this.gcRPDT_B.Width = 70;
            // 
            // gcRPID_B
            // 
            this.gcRPID_B.AppearanceCell.Options.UseTextOptions = true;
            this.gcRPID_B.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcRPID_B.Caption = "구분";
            this.gcRPID_B.FieldName = "RPID_B";
            this.gcRPID_B.Name = "gcRPID_B";
            this.gcRPID_B.OptionsColumn.ReadOnly = true;
            this.gcRPID_B.Visible = true;
            this.gcRPID_B.Width = 33;
            // 
            // gcUNAMT_B
            // 
            this.gcUNAMT_B.AppearanceCell.Options.UseTextOptions = true;
            this.gcUNAMT_B.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcUNAMT_B.Caption = "조합부담금";
            this.gcUNAMT_B.FieldName = "UNAMT_B";
            this.gcUNAMT_B.Name = "gcUNAMT_B";
            this.gcUNAMT_B.OptionsColumn.ReadOnly = true;
            this.gcUNAMT_B.Visible = true;
            this.gcUNAMT_B.Width = 70;
            // 
            // gcSIMNO_B
            // 
            this.gcSIMNO_B.AppearanceCell.Options.UseTextOptions = true;
            this.gcSIMNO_B.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSIMNO_B.Caption = "심사번호";
            this.gcSIMNO_B.FieldName = "SIMNO_B";
            this.gcSIMNO_B.Name = "gcSIMNO_B";
            this.gcSIMNO_B.OptionsColumn.ReadOnly = true;
            this.gcSIMNO_B.Visible = true;
            this.gcSIMNO_B.Width = 55;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "변경후";
            this.gridBand3.Columns.Add(this.gcQFYCD_A);
            this.gridBand3.Columns.Add(this.gcDPTCD_A);
            this.gridBand3.Columns.Add(this.gcRPDT_A);
            this.gridBand3.Columns.Add(this.gcRPID_A);
            this.gridBand3.Columns.Add(this.gcUNAMT_A);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 256;
            // 
            // gcQFYCD_A
            // 
            this.gcQFYCD_A.AppearanceCell.Options.UseTextOptions = true;
            this.gcQFYCD_A.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcQFYCD_A.Caption = "자격";
            this.gcQFYCD_A.FieldName = "QFYCD_A";
            this.gcQFYCD_A.Name = "gcQFYCD_A";
            this.gcQFYCD_A.OptionsColumn.ReadOnly = true;
            this.gcQFYCD_A.Visible = true;
            this.gcQFYCD_A.Width = 33;
            // 
            // gcDPTCD_A
            // 
            this.gcDPTCD_A.AppearanceCell.Options.UseTextOptions = true;
            this.gcDPTCD_A.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDPTCD_A.Caption = "진료과";
            this.gcDPTCD_A.FieldName = "DPTCD_A";
            this.gcDPTCD_A.Name = "gcDPTCD_A";
            this.gcDPTCD_A.OptionsColumn.ReadOnly = true;
            this.gcDPTCD_A.Visible = true;
            this.gcDPTCD_A.Width = 50;
            // 
            // gcRPDT_A
            // 
            this.gcRPDT_A.Caption = "수납일";
            this.gcRPDT_A.FieldName = "RPDT_A";
            this.gcRPDT_A.Name = "gcRPDT_A";
            this.gcRPDT_A.OptionsColumn.ReadOnly = true;
            this.gcRPDT_A.Visible = true;
            this.gcRPDT_A.Width = 70;
            // 
            // gcRPID_A
            // 
            this.gcRPID_A.AppearanceCell.Options.UseTextOptions = true;
            this.gcRPID_A.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcRPID_A.Caption = "구분";
            this.gcRPID_A.FieldName = "RPID_A";
            this.gcRPID_A.Name = "gcRPID_A";
            this.gcRPID_A.OptionsColumn.ReadOnly = true;
            this.gcRPID_A.Visible = true;
            this.gcRPID_A.Width = 33;
            // 
            // gcUNAMT_A
            // 
            this.gcUNAMT_A.AppearanceCell.Options.UseTextOptions = true;
            this.gcUNAMT_A.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcUNAMT_A.Caption = "조합부담금";
            this.gcUNAMT_A.FieldName = "UNAMT_A";
            this.gcUNAMT_A.Name = "gcUNAMT_A";
            this.gcUNAMT_A.OptionsColumn.ReadOnly = true;
            this.gcUNAMT_A.Visible = true;
            this.gcUNAMT_A.Width = 70;
            // 
            // ADD0251Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 471);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtYYMM);
            this.Controls.Add(this.label1);
            this.Name = "ADD0251Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "퇴원재수납환자리스트(ADD0251Q)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtYYMM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grdMainView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcCANCELDT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcPID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcPNM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcBEDODT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcQFYCD_B;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcDPTCD_B;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcRPDT_B;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcRPID_B;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcUNAMT_B;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcSIMNO_B;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcQFYCD_A;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcDPTCD_A;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcRPDT_A;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcRPID_A;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcUNAMT_A;
    }
}


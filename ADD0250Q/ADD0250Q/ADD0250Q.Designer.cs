namespace ADD0250Q
{
    partial class ADD0250Q
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtYYMM = new System.Windows.Forms.TextBox();
            this.chkQfycd2 = new System.Windows.Forms.CheckBox();
            this.chkQfycd3 = new System.Windows.Forms.CheckBox();
            this.chkQfycd6 = new System.Windows.Forms.CheckBox();
            this.chkQfycd5 = new System.Windows.Forms.CheckBox();
            this.chkQfycd38 = new System.Windows.Forms.CheckBox();
            this.chkQfycd4 = new System.Windows.Forms.CheckBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPSEXAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDODT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCREDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYCDNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRPOK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYSBNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDIVSNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRPOKS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWARD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDODIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSIMSANM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "퇴원월 :";
            // 
            // txtYYMM
            // 
            this.txtYYMM.Location = new System.Drawing.Point(67, 7);
            this.txtYYMM.Name = "txtYYMM";
            this.txtYYMM.Size = new System.Drawing.Size(62, 21);
            this.txtYYMM.TabIndex = 1;
            // 
            // chkQfycd2
            // 
            this.chkQfycd2.AutoSize = true;
            this.chkQfycd2.Location = new System.Drawing.Point(298, 11);
            this.chkQfycd2.Name = "chkQfycd2";
            this.chkQfycd2.Size = new System.Drawing.Size(48, 16);
            this.chkQfycd2.TabIndex = 4;
            this.chkQfycd2.Text = "보험";
            this.chkQfycd2.UseVisualStyleBackColor = true;
            // 
            // chkQfycd3
            // 
            this.chkQfycd3.AutoSize = true;
            this.chkQfycd3.Location = new System.Drawing.Point(349, 11);
            this.chkQfycd3.Name = "chkQfycd3";
            this.chkQfycd3.Size = new System.Drawing.Size(48, 16);
            this.chkQfycd3.TabIndex = 5;
            this.chkQfycd3.Text = "보호";
            this.chkQfycd3.UseVisualStyleBackColor = true;
            // 
            // chkQfycd6
            // 
            this.chkQfycd6.AutoSize = true;
            this.chkQfycd6.Location = new System.Drawing.Point(453, 11);
            this.chkQfycd6.Name = "chkQfycd6";
            this.chkQfycd6.Size = new System.Drawing.Size(48, 16);
            this.chkQfycd6.TabIndex = 7;
            this.chkQfycd6.Text = "자보";
            this.chkQfycd6.UseVisualStyleBackColor = true;
            // 
            // chkQfycd5
            // 
            this.chkQfycd5.AutoSize = true;
            this.chkQfycd5.Location = new System.Drawing.Point(402, 11);
            this.chkQfycd5.Name = "chkQfycd5";
            this.chkQfycd5.Size = new System.Drawing.Size(48, 16);
            this.chkQfycd5.TabIndex = 6;
            this.chkQfycd5.Text = "산재";
            this.chkQfycd5.UseVisualStyleBackColor = true;
            // 
            // chkQfycd38
            // 
            this.chkQfycd38.AutoSize = true;
            this.chkQfycd38.Location = new System.Drawing.Point(555, 11);
            this.chkQfycd38.Name = "chkQfycd38";
            this.chkQfycd38.Size = new System.Drawing.Size(84, 16);
            this.chkQfycd38.TabIndex = 9;
            this.chkQfycd38.Text = "보호정신과";
            this.chkQfycd38.UseVisualStyleBackColor = true;
            // 
            // chkQfycd4
            // 
            this.chkQfycd4.AutoSize = true;
            this.chkQfycd4.Location = new System.Drawing.Point(504, 11);
            this.chkQfycd4.Name = "chkQfycd4";
            this.chkQfycd4.Size = new System.Drawing.Size(48, 16);
            this.chkQfycd4.TabIndex = 8;
            this.chkQfycd4.Text = "공상";
            this.chkQfycd4.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(664, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(822, 6);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(79, 23);
            this.btnExcel.TabIndex = 11;
            this.btnExcel.Text = "엑셀";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(743, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 36);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(889, 372);
            this.grdMain.TabIndex = 13;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNO,
            this.gcPID,
            this.gcPNM,
            this.gcPSEXAGE,
            this.gcBEDEDT,
            this.gcBEDODT,
            this.gridColumn1,
            this.gcCREDT,
            this.gcQFYCDNM,
            this.gcPDIVNM,
            this.gcRPOK,
            this.gcQFYSBNM,
            this.gcPDIVSNM,
            this.gcRPOKS,
            this.gcDPTCD,
            this.gcPDRNM,
            this.gcWARD,
            this.gcBEDODIVNM,
            this.gcSIMSANM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowGroup = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.EndSorting += new System.EventHandler(this.grdMainView_EndSorting);
            this.grdMainView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grdMainView_CustomDrawRowIndicator);
            // 
            // gcNO
            // 
            this.gcNO.AppearanceCell.Options.UseTextOptions = true;
            this.gcNO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcNO.Caption = "No";
            this.gcNO.FieldName = "NO";
            this.gcNO.Name = "gcNO";
            this.gcNO.OptionsColumn.ReadOnly = true;
            this.gcNO.Visible = true;
            this.gcNO.VisibleIndex = 0;
            this.gcNO.Width = 40;
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 1;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 2;
            // 
            // gcPSEXAGE
            // 
            this.gcPSEXAGE.Caption = "성/나이";
            this.gcPSEXAGE.FieldName = "PSEXAGE";
            this.gcPSEXAGE.Name = "gcPSEXAGE";
            this.gcPSEXAGE.OptionsColumn.ReadOnly = true;
            this.gcPSEXAGE.Visible = true;
            this.gcPSEXAGE.VisibleIndex = 3;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.ReadOnly = true;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 4;
            // 
            // gcBEDODT
            // 
            this.gcBEDODT.Caption = "퇴원일";
            this.gcBEDODT.FieldName = "BEDODT";
            this.gcBEDODT.Name = "gcBEDODT";
            this.gcBEDODT.OptionsColumn.ReadOnly = true;
            this.gcBEDODT.Visible = true;
            this.gcBEDODT.VisibleIndex = 5;
            // 
            // gcCREDT
            // 
            this.gcCREDT.Caption = "자격적용일";
            this.gcCREDT.FieldName = "CREDT";
            this.gcCREDT.Name = "gcCREDT";
            this.gcCREDT.OptionsColumn.ReadOnly = true;
            this.gcCREDT.Visible = true;
            this.gcCREDT.VisibleIndex = 7;
            // 
            // gcQFYCDNM
            // 
            this.gcQFYCDNM.Caption = "자격";
            this.gcQFYCDNM.FieldName = "QFYCDNM";
            this.gcQFYCDNM.Name = "gcQFYCDNM";
            this.gcQFYCDNM.OptionsColumn.ReadOnly = true;
            this.gcQFYCDNM.Visible = true;
            this.gcQFYCDNM.VisibleIndex = 8;
            // 
            // gcPDIVNM
            // 
            this.gcPDIVNM.Caption = "환자구분";
            this.gcPDIVNM.FieldName = "PDIVNM";
            this.gcPDIVNM.Name = "gcPDIVNM";
            this.gcPDIVNM.OptionsColumn.ReadOnly = true;
            this.gcPDIVNM.Visible = true;
            this.gcPDIVNM.VisibleIndex = 9;
            // 
            // gcRPOK
            // 
            this.gcRPOK.Caption = "I";
            this.gcRPOK.FieldName = "RPOK";
            this.gcRPOK.Name = "gcRPOK";
            this.gcRPOK.OptionsColumn.ReadOnly = true;
            this.gcRPOK.Visible = true;
            this.gcRPOK.VisibleIndex = 10;
            this.gcRPOK.Width = 40;
            // 
            // gcQFYSBNM
            // 
            this.gcQFYSBNM.Caption = "부자격";
            this.gcQFYSBNM.FieldName = "QFYSBNM";
            this.gcQFYSBNM.Name = "gcQFYSBNM";
            this.gcQFYSBNM.OptionsColumn.ReadOnly = true;
            this.gcQFYSBNM.Visible = true;
            this.gcQFYSBNM.VisibleIndex = 11;
            // 
            // gcPDIVSNM
            // 
            this.gcPDIVSNM.Caption = "환자구분";
            this.gcPDIVSNM.FieldName = "PDIVSNM";
            this.gcPDIVSNM.Name = "gcPDIVSNM";
            this.gcPDIVSNM.OptionsColumn.ReadOnly = true;
            this.gcPDIVSNM.Visible = true;
            this.gcPDIVSNM.VisibleIndex = 12;
            // 
            // gcRPOKS
            // 
            this.gcRPOKS.Caption = "L";
            this.gcRPOKS.FieldName = "RPOKS";
            this.gcRPOKS.Name = "gcRPOKS";
            this.gcRPOKS.OptionsColumn.ReadOnly = true;
            this.gcRPOKS.Visible = true;
            this.gcRPOKS.VisibleIndex = 13;
            this.gcRPOKS.Width = 40;
            // 
            // gcDPTCD
            // 
            this.gcDPTCD.Caption = "진료과";
            this.gcDPTCD.FieldName = "DPTCD";
            this.gcDPTCD.Name = "gcDPTCD";
            this.gcDPTCD.OptionsColumn.ReadOnly = true;
            this.gcDPTCD.Visible = true;
            this.gcDPTCD.VisibleIndex = 14;
            // 
            // gcPDRNM
            // 
            this.gcPDRNM.Caption = "주치의";
            this.gcPDRNM.FieldName = "PDRNM";
            this.gcPDRNM.Name = "gcPDRNM";
            this.gcPDRNM.OptionsColumn.ReadOnly = true;
            this.gcPDRNM.Visible = true;
            this.gcPDRNM.VisibleIndex = 15;
            // 
            // gcWARD
            // 
            this.gcWARD.Caption = "병동";
            this.gcWARD.FieldName = "WARD";
            this.gcWARD.Name = "gcWARD";
            this.gcWARD.OptionsColumn.ReadOnly = true;
            this.gcWARD.Visible = true;
            this.gcWARD.VisibleIndex = 16;
            // 
            // gcBEDODIVNM
            // 
            this.gcBEDODIVNM.Caption = "퇴원구분";
            this.gcBEDODIVNM.FieldName = "BEDODIVNM";
            this.gcBEDODIVNM.Name = "gcBEDODIVNM";
            this.gcBEDODIVNM.OptionsColumn.ReadOnly = true;
            this.gcBEDODIVNM.Visible = true;
            this.gcBEDODIVNM.VisibleIndex = 17;
            // 
            // gcSIMSANM
            // 
            this.gcSIMSANM.Caption = "심사자";
            this.gcSIMSANM.FieldName = "SIMSANM";
            this.gcSIMSANM.Name = "gcSIMSANM";
            this.gcSIMSANM.OptionsColumn.ReadOnly = true;
            this.gcSIMSANM.Visible = true;
            this.gcSIMSANM.VisibleIndex = 18;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "DRG번호";
            this.gridColumn1.FieldName = "DRGNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            // 
            // ADD0250Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 420);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.chkQfycd38);
            this.Controls.Add(this.chkQfycd4);
            this.Controls.Add(this.chkQfycd6);
            this.Controls.Add(this.chkQfycd5);
            this.Controls.Add(this.chkQfycd3);
            this.Controls.Add(this.chkQfycd2);
            this.Controls.Add(this.txtYYMM);
            this.Controls.Add(this.label1);
            this.Name = "ADD0250Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "퇴원환자리스트(ADD0250Q)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYYMM;
        private System.Windows.Forms.CheckBox chkQfycd2;
        private System.Windows.Forms.CheckBox chkQfycd3;
        private System.Windows.Forms.CheckBox chkQfycd6;
        private System.Windows.Forms.CheckBox chkQfycd5;
        private System.Windows.Forms.CheckBox chkQfycd38;
        private System.Windows.Forms.CheckBox chkQfycd4;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPSEXAGE;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDODT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCREDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCDNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRPOK;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYSBNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDIVSNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRPOKS;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcWARD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDODIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSIMSANM;
        private DevExpress.XtraGrid.Columns.GridColumn gcNO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}


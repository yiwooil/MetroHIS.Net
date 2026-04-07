namespace ADD7000E
{
    partial class ADD7000E
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
            this.gcDEMNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEPRTNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGONSGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHECK_ORM001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHECK_RID001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHECK_OCQ001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCNECNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDCOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSendOne = new System.Windows.Forms.Button();
            this.btnSendTmp = new System.Windows.Forms.Button();
            this.btnSample = new System.Windows.Forms.Button();
            this.btnMakeAndCheckAndSend = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnMakeAndCheck = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtDEMNO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Margin = new System.Windows.Forms.Padding(10);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(970, 338);
            this.grdMain.TabIndex = 2;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDEMNO,
            this.gcEPRTNO,
            this.gcPID,
            this.gcPNM,
            this.gcDPTCD,
            this.gcQFYCD,
            this.gcGONSGB,
            this.gcCHECK_ORM001,
            this.gcCHECK_RID001,
            this.gcCHECK_OCQ001,
            this.gcCNECNO,
            this.gcDCOUNT});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdMainView_RowCellStyle);
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            this.grdMainView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdMainView_RowCellClick);
            // 
            // gcDEMNO
            // 
            this.gcDEMNO.Caption = "청구번호";
            this.gcDEMNO.FieldName = "DEMNO";
            this.gcDEMNO.Name = "gcDEMNO";
            this.gcDEMNO.OptionsColumn.AllowEdit = false;
            this.gcDEMNO.Visible = true;
            this.gcDEMNO.VisibleIndex = 0;
            // 
            // gcEPRTNO
            // 
            this.gcEPRTNO.Caption = "명일련";
            this.gcEPRTNO.FieldName = "EPRTNO";
            this.gcEPRTNO.Name = "gcEPRTNO";
            this.gcEPRTNO.OptionsColumn.AllowEdit = false;
            this.gcEPRTNO.Visible = true;
            this.gcEPRTNO.VisibleIndex = 1;
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.AllowEdit = false;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 2;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 3;
            // 
            // gcDPTCD
            // 
            this.gcDPTCD.Caption = "진료과";
            this.gcDPTCD.FieldName = "DPTCD";
            this.gcDPTCD.Name = "gcDPTCD";
            this.gcDPTCD.OptionsColumn.AllowEdit = false;
            this.gcDPTCD.Visible = true;
            this.gcDPTCD.VisibleIndex = 4;
            // 
            // gcQFYCD
            // 
            this.gcQFYCD.Caption = "자격";
            this.gcQFYCD.FieldName = "QFYCD";
            this.gcQFYCD.Name = "gcQFYCD";
            this.gcQFYCD.OptionsColumn.AllowEdit = false;
            this.gcQFYCD.Visible = true;
            this.gcQFYCD.VisibleIndex = 5;
            // 
            // gcGONSGB
            // 
            this.gcGONSGB.Caption = "공상";
            this.gcGONSGB.FieldName = "GONSGB";
            this.gcGONSGB.Name = "gcGONSGB";
            this.gcGONSGB.OptionsColumn.AllowEdit = false;
            this.gcGONSGB.Visible = true;
            this.gcGONSGB.VisibleIndex = 6;
            // 
            // gcCHECK_ORM001
            // 
            this.gcCHECK_ORM001.Caption = "영수증";
            this.gcCHECK_ORM001.FieldName = "CHECK_ORM001";
            this.gcCHECK_ORM001.Name = "gcCHECK_ORM001";
            this.gcCHECK_ORM001.OptionsColumn.AllowEdit = false;
            this.gcCHECK_ORM001.Visible = true;
            this.gcCHECK_ORM001.VisibleIndex = 7;
            // 
            // gcCHECK_RID001
            // 
            this.gcCHECK_RID001.Caption = "퇴원요약지";
            this.gcCHECK_RID001.FieldName = "CHECK_RID001";
            this.gcCHECK_RID001.Name = "gcCHECK_RID001";
            this.gcCHECK_RID001.OptionsColumn.AllowEdit = false;
            this.gcCHECK_RID001.Visible = true;
            this.gcCHECK_RID001.VisibleIndex = 8;
            // 
            // gcCHECK_OCQ001
            // 
            this.gcCHECK_OCQ001.Caption = "점검표";
            this.gcCHECK_OCQ001.FieldName = "CHECK_OCQ001";
            this.gcCHECK_OCQ001.Name = "gcCHECK_OCQ001";
            this.gcCHECK_OCQ001.OptionsColumn.AllowEdit = false;
            this.gcCHECK_OCQ001.Visible = true;
            this.gcCHECK_OCQ001.VisibleIndex = 9;
            // 
            // gcCNECNO
            // 
            this.gcCNECNO.Caption = "접수번호";
            this.gcCNECNO.FieldName = "CNECNO";
            this.gcCNECNO.Name = "gcCNECNO";
            this.gcCNECNO.OptionsColumn.AllowEdit = false;
            this.gcCNECNO.Visible = true;
            this.gcCNECNO.VisibleIndex = 10;
            // 
            // gcDCOUNT
            // 
            this.gcDCOUNT.Caption = "청일련";
            this.gcDCOUNT.FieldName = "DCOUNT";
            this.gcDCOUNT.Name = "gcDCOUNT";
            this.gcDCOUNT.OptionsColumn.AllowEdit = false;
            this.gcDCOUNT.Visible = true;
            this.gcDCOUNT.VisibleIndex = 11;
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMsg.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMsg.Location = new System.Drawing.Point(0, 377);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(970, 44);
            this.txtMsg.TabIndex = 5;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 372);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(970, 5);
            this.splitterControl1.TabIndex = 8;
            this.splitterControl1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSendOne);
            this.panel1.Controls.Add(this.btnSendTmp);
            this.panel1.Controls.Add(this.btnSample);
            this.panel1.Controls.Add(this.btnMakeAndCheckAndSend);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.btnMakeAndCheck);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtDEMNO);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 34);
            this.panel1.TabIndex = 9;
            // 
            // btnSendOne
            // 
            this.btnSendOne.Location = new System.Drawing.Point(763, 6);
            this.btnSendOne.Name = "btnSendOne";
            this.btnSendOne.Size = new System.Drawing.Size(114, 23);
            this.btnSendOne.TabIndex = 16;
            this.btnSendOne.Text = "선택된 환자 전송";
            this.btnSendOne.UseVisualStyleBackColor = true;
            this.btnSendOne.Click += new System.EventHandler(this.btnSendOne_Click);
            // 
            // btnSendTmp
            // 
            this.btnSendTmp.Location = new System.Drawing.Point(501, 6);
            this.btnSendTmp.Name = "btnSendTmp";
            this.btnSendTmp.Size = new System.Drawing.Size(75, 23);
            this.btnSendTmp.TabIndex = 15;
            this.btnSendTmp.Text = "임시 전송";
            this.btnSendTmp.UseVisualStyleBackColor = true;
            this.btnSendTmp.Click += new System.EventHandler(this.btnSendTmp_Click);
            // 
            // btnSample
            // 
            this.btnSample.Location = new System.Drawing.Point(899, 5);
            this.btnSample.Name = "btnSample";
            this.btnSample.Size = new System.Drawing.Size(58, 25);
            this.btnSample.TabIndex = 14;
            this.btnSample.Text = "샘플";
            this.btnSample.UseVisualStyleBackColor = true;
            this.btnSample.Visible = false;
            this.btnSample.Click += new System.EventHandler(this.btnSample_Click);
            // 
            // btnMakeAndCheckAndSend
            // 
            this.btnMakeAndCheckAndSend.Location = new System.Drawing.Point(266, 6);
            this.btnMakeAndCheckAndSend.Name = "btnMakeAndCheckAndSend";
            this.btnMakeAndCheckAndSend.Size = new System.Drawing.Size(128, 23);
            this.btnMakeAndCheckAndSend.TabIndex = 13;
            this.btnMakeAndCheckAndSend.Text = "생성, 점검 및 전송";
            this.btnMakeAndCheckAndSend.UseVisualStyleBackColor = true;
            this.btnMakeAndCheckAndSend.Visible = false;
            this.btnMakeAndCheckAndSend.Click += new System.EventHandler(this.btnMakeAndCheckAndSend_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(579, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "전송";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnMakeAndCheck
            // 
            this.btnMakeAndCheck.Location = new System.Drawing.Point(397, 6);
            this.btnMakeAndCheck.Name = "btnMakeAndCheck";
            this.btnMakeAndCheck.Size = new System.Drawing.Size(102, 23);
            this.btnMakeAndCheck.TabIndex = 11;
            this.btnMakeAndCheck.Text = "생성 및 점검";
            this.btnMakeAndCheck.UseVisualStyleBackColor = true;
            this.btnMakeAndCheck.Click += new System.EventHandler(this.btnMakeAndCheck_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(189, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtDEMNO
            // 
            this.txtDEMNO.Location = new System.Drawing.Point(83, 8);
            this.txtDEMNO.Name = "txtDEMNO";
            this.txtDEMNO.Size = new System.Drawing.Size(100, 21);
            this.txtDEMNO.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "청구번호 :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(970, 372);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(970, 338);
            this.panel3.TabIndex = 10;
            // 
            // ADD7000E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 421);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.txtMsg);
            this.Name = "ADD7000E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "표준3종서식전송(ADD7000E)";
            this.Load += new System.EventHandler(this.ADD7000E_Load);
            this.Activated += new System.EventHandler(this.ADD7000E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcEPRTNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcGONSGB;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHECK_ORM001;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHECK_RID001;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHECK_OCQ001;
        private System.Windows.Forms.TextBox txtMsg;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMakeAndCheckAndSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnMakeAndCheck;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtDEMNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSample;
        private DevExpress.XtraGrid.Columns.GridColumn gcCNECNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcDCOUNT;
        private System.Windows.Forms.Button btnSendTmp;
        private System.Windows.Forms.Button btnSendOne;
    }
}


namespace ADD0714E
{
    partial class ADD0714E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD0714E));
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCNECNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGRPNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHOSID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGUBUN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcACNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcATTAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPTAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAJAM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAUNAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPMGUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcASTGUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAGUMAK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAMPAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMEMO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.txtReday = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtDemseq = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(7, 37);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3});
            this.grdMain.Size = new System.Drawing.Size(973, 405);
            this.grdMain.TabIndex = 73;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCNECNO,
            this.gcGRPNO,
            this.gcHOSID,
            this.gcGUBUN,
            this.gcACNT,
            this.gcATTAMT,
            this.gcAPTAMT,
            this.gcAJAM,
            this.gcAUNAMT,
            this.gcAPMGUM,
            this.gcASTGUM,
            this.gcAGUMAK,
            this.gcAMPAMT,
            this.gcMEMO});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.RowAutoHeight = true;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcCNECNO
            // 
            this.gcCNECNO.Caption = "접수번호";
            this.gcCNECNO.FieldName = "CNECNO";
            this.gcCNECNO.Name = "gcCNECNO";
            this.gcCNECNO.Visible = true;
            this.gcCNECNO.VisibleIndex = 0;
            // 
            // gcGRPNO
            // 
            this.gcGRPNO.Caption = "묶음번호";
            this.gcGRPNO.FieldName = "GRPNO";
            this.gcGRPNO.Name = "gcGRPNO";
            this.gcGRPNO.Visible = true;
            this.gcGRPNO.VisibleIndex = 1;
            this.gcGRPNO.Width = 55;
            // 
            // gcHOSID
            // 
            this.gcHOSID.Caption = "기관기호";
            this.gcHOSID.FieldName = "HOSID";
            this.gcHOSID.Name = "gcHOSID";
            this.gcHOSID.Visible = true;
            this.gcHOSID.VisibleIndex = 2;
            this.gcHOSID.Width = 55;
            // 
            // gcGUBUN
            // 
            this.gcGUBUN.Caption = "구분";
            this.gcGUBUN.FieldName = "GUBUN";
            this.gcGUBUN.Name = "gcGUBUN";
            this.gcGUBUN.Visible = true;
            this.gcGUBUN.VisibleIndex = 3;
            this.gcGUBUN.Width = 35;
            // 
            // gcACNT
            // 
            this.gcACNT.Caption = "건수";
            this.gcACNT.FieldName = "ACNT";
            this.gcACNT.Name = "gcACNT";
            this.gcACNT.Visible = true;
            this.gcACNT.VisibleIndex = 4;
            this.gcACNT.Width = 55;
            // 
            // gcATTAMT
            // 
            this.gcATTAMT.Caption = "총진료비";
            this.gcATTAMT.FieldName = "ATTAMT";
            this.gcATTAMT.Name = "gcATTAMT";
            this.gcATTAMT.Visible = true;
            this.gcATTAMT.VisibleIndex = 5;
            // 
            // gcAPTAMT
            // 
            this.gcAPTAMT.Caption = "본인부담금";
            this.gcAPTAMT.FieldName = "APTAMT";
            this.gcAPTAMT.Name = "gcAPTAMT";
            this.gcAPTAMT.Visible = true;
            this.gcAPTAMT.VisibleIndex = 6;
            // 
            // gcAJAM
            // 
            this.gcAJAM.Caption = "장애인";
            this.gcAJAM.FieldName = "AJAM";
            this.gcAJAM.Name = "gcAJAM";
            this.gcAJAM.Visible = true;
            this.gcAJAM.VisibleIndex = 7;
            this.gcAJAM.Width = 65;
            // 
            // gcAUNAMT
            // 
            this.gcAUNAMT.Caption = "기관부담";
            this.gcAUNAMT.FieldName = "AUNAMT";
            this.gcAUNAMT.Name = "gcAUNAMT";
            this.gcAUNAMT.Visible = true;
            this.gcAUNAMT.VisibleIndex = 8;
            // 
            // gcAPMGUM
            // 
            this.gcAPMGUM.Caption = "본인환급금";
            this.gcAPMGUM.FieldName = "APMGUM";
            this.gcAPMGUM.Name = "gcAPMGUM";
            this.gcAPMGUM.Visible = true;
            this.gcAPMGUM.VisibleIndex = 9;
            // 
            // gcASTGUM
            // 
            this.gcASTGUM.Caption = "수탁검사";
            this.gcASTGUM.FieldName = "ASTGUM";
            this.gcASTGUM.Name = "gcASTGUM";
            this.gcASTGUM.Visible = true;
            this.gcASTGUM.VisibleIndex = 10;
            this.gcASTGUM.Width = 65;
            // 
            // gcAGUMAK
            // 
            this.gcAGUMAK.Caption = "실지급액";
            this.gcAGUMAK.FieldName = "AGUMAK";
            this.gcAGUMAK.Name = "gcAGUMAK";
            this.gcAGUMAK.Visible = true;
            this.gcAGUMAK.VisibleIndex = 11;
            // 
            // gcAMPAMT
            // 
            this.gcAMPAMT.Caption = "증감액";
            this.gcAMPAMT.FieldName = "AMPAMT";
            this.gcAMPAMT.Name = "gcAMPAMT";
            this.gcAMPAMT.Visible = true;
            this.gcAMPAMT.VisibleIndex = 12;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "비고";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 13;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // txtReday
            // 
            this.txtReday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtReday.Location = new System.Drawing.Point(224, 449);
            this.txtReday.Name = "txtReday";
            this.txtReday.Size = new System.Drawing.Size(76, 21);
            this.txtReday.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 453);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 71;
            this.label3.Text = "통보일자";
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHosid.Location = new System.Drawing.Point(90, 449);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(76, 21);
            this.txtHosid.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 453);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 69;
            this.label2.Text = "요양기관기호";
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(357, 449);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(623, 21);
            this.txtMemo.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 453);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 67;
            this.label5.Text = "참조란";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(320, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 66;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(246, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 65;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtDemseq
            // 
            this.txtDemseq.Location = new System.Drawing.Point(65, 9);
            this.txtDemseq.Name = "txtDemseq";
            this.txtDemseq.Size = new System.Drawing.Size(86, 21);
            this.txtDemseq.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 62;
            this.label1.Text = "심사차수";
            // 
            // btnList
            // 
            this.btnList.Image = ((System.Drawing.Image)(resources.GetObject("btnList.Image")));
            this.btnList.Location = new System.Drawing.Point(152, 7);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(23, 23);
            this.btnList.TabIndex = 74;
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // ADD0714E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 479);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtReday);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtDemseq);
            this.Controls.Add(this.label1);
            this.Name = "ADD0714E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[보호]심사결과총괄표(ADD0714E)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcCNECNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcGRPNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcHOSID;
        private DevExpress.XtraGrid.Columns.GridColumn gcGUBUN;
        private DevExpress.XtraGrid.Columns.GridColumn gcACNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcATTAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPTAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAJAM;
        private DevExpress.XtraGrid.Columns.GridColumn gcAUNAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPMGUM;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private System.Windows.Forms.TextBox txtReday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtDemseq;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gcASTGUM;
        private DevExpress.XtraGrid.Columns.GridColumn gcAGUMAK;
        private DevExpress.XtraGrid.Columns.GridColumn gcAMPAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
        private System.Windows.Forms.Button btnList;

    }
}


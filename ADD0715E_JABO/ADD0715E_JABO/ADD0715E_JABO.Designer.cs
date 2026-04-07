namespace ADD0715E_JABO
{
    partial class ADD0715E_JABO
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
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDEMNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCALLNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCALLPAFGNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMKDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcABTFGNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gcABTTXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gcCALLTXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gcBYEQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRedpt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(65, 7);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(86, 21);
            this.txtDemno.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "통보번호";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(246, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 27;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(157, 6);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 26;
            this.btnList.Text = "리스트";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(320, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 28;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(7, 35);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3});
            this.grdMain.Size = new System.Drawing.Size(973, 405);
            this.grdMain.TabIndex = 29;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDEMNO,
            this.gcCALLNO,
            this.gcCALLPAFGNM,
            this.gcLNO,
            this.gcMKDIVNM,
            this.gcABTFGNM,
            this.gcITEMCD,
            this.gcITEMNM,
            this.gcABTTXT,
            this.gcCALLTXT,
            this.gcBYEQTY});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.RowAutoHeight = true;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDEMNO
            // 
            this.gcDEMNO.Caption = "통보번호";
            this.gcDEMNO.FieldName = "DEMNO";
            this.gcDEMNO.Name = "gcDEMNO";
            this.gcDEMNO.OptionsColumn.AllowEdit = false;
            this.gcDEMNO.Visible = true;
            this.gcDEMNO.VisibleIndex = 0;
            this.gcDEMNO.Width = 95;
            // 
            // gcCALLNO
            // 
            this.gcCALLNO.Caption = "신청번호";
            this.gcCALLNO.FieldName = "CALLNO";
            this.gcCALLNO.Name = "gcCALLNO";
            this.gcCALLNO.OptionsColumn.AllowEdit = false;
            this.gcCALLNO.Visible = true;
            this.gcCALLNO.VisibleIndex = 1;
            this.gcCALLNO.Width = 95;
            // 
            // gcCALLPAFGNM
            // 
            this.gcCALLPAFGNM.Caption = "신고서구분";
            this.gcCALLPAFGNM.FieldName = "CALLPAFGNM";
            this.gcCALLPAFGNM.Name = "gcCALLPAFGNM";
            this.gcCALLPAFGNM.OptionsColumn.AllowEdit = false;
            this.gcCALLPAFGNM.Visible = true;
            this.gcCALLPAFGNM.VisibleIndex = 2;
            // 
            // gcLNO
            // 
            this.gcLNO.Caption = "줄번호";
            this.gcLNO.DisplayFormat.FormatString = "#,###";
            this.gcLNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcLNO.FieldName = "LNO";
            this.gcLNO.Name = "gcLNO";
            this.gcLNO.OptionsColumn.AllowEdit = false;
            this.gcLNO.Visible = true;
            this.gcLNO.VisibleIndex = 3;
            this.gcLNO.Width = 45;
            // 
            // gcMKDIVNM
            // 
            this.gcMKDIVNM.Caption = "조제구분";
            this.gcMKDIVNM.FieldName = "MKDIVNM";
            this.gcMKDIVNM.Name = "gcMKDIVNM";
            this.gcMKDIVNM.OptionsColumn.AllowEdit = false;
            this.gcMKDIVNM.Visible = true;
            this.gcMKDIVNM.VisibleIndex = 4;
            this.gcMKDIVNM.Width = 55;
            // 
            // gcABTFGNM
            // 
            this.gcABTFGNM.Caption = "안내사항구분";
            this.gcABTFGNM.FieldName = "ABTFGNM";
            this.gcABTFGNM.Name = "gcABTFGNM";
            this.gcABTFGNM.OptionsColumn.AllowEdit = false;
            this.gcABTFGNM.Visible = true;
            this.gcABTFGNM.VisibleIndex = 5;
            this.gcABTFGNM.Width = 80;
            // 
            // gcITEMCD
            // 
            this.gcITEMCD.Caption = "신고품목";
            this.gcITEMCD.FieldName = "ITEMCD";
            this.gcITEMCD.Name = "gcITEMCD";
            this.gcITEMCD.OptionsColumn.AllowEdit = false;
            this.gcITEMCD.Visible = true;
            this.gcITEMCD.VisibleIndex = 6;
            // 
            // gcITEMNM
            // 
            this.gcITEMNM.Caption = "품명";
            this.gcITEMNM.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gcITEMNM.FieldName = "ITEMNM";
            this.gcITEMNM.Name = "gcITEMNM";
            this.gcITEMNM.OptionsColumn.AllowEdit = false;
            this.gcITEMNM.Visible = true;
            this.gcITEMNM.VisibleIndex = 7;
            this.gcITEMNM.Width = 350;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // gcABTTXT
            // 
            this.gcABTTXT.Caption = "안내사항내역";
            this.gcABTTXT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gcABTTXT.FieldName = "ABTTXT";
            this.gcABTTXT.Name = "gcABTTXT";
            this.gcABTTXT.OptionsColumn.AllowEdit = false;
            this.gcABTTXT.Width = 85;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gcCALLTXT
            // 
            this.gcCALLTXT.Caption = "자료요청내역";
            this.gcCALLTXT.ColumnEdit = this.repositoryItemMemoEdit3;
            this.gcCALLTXT.FieldName = "CALLTXT";
            this.gcCALLTXT.Name = "gcCALLTXT";
            this.gcCALLTXT.OptionsColumn.AllowEdit = false;
            this.gcCALLTXT.Width = 85;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // gcBYEQTY
            // 
            this.gcBYEQTY.Caption = "구입량";
            this.gcBYEQTY.DisplayFormat.FormatString = "#,###";
            this.gcBYEQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcBYEQTY.FieldName = "BYEQTY";
            this.gcBYEQTY.Name = "gcBYEQTY";
            this.gcBYEQTY.OptionsColumn.AllowEdit = false;
            this.gcBYEQTY.Visible = true;
            this.gcBYEQTY.VisibleIndex = 8;
            this.gcBYEQTY.Width = 50;
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(357, 447);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(623, 21);
            this.txtMemo.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "참조란";
            // 
            // txtRedpt
            // 
            this.txtRedpt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRedpt.Location = new System.Drawing.Point(224, 447);
            this.txtRedpt.Name = "txtRedpt";
            this.txtRedpt.Size = new System.Drawing.Size(76, 21);
            this.txtRedpt.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 451);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "담당자";
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHosid.Location = new System.Drawing.Point(90, 447);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(76, 21);
            this.txtHosid.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 451);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "요양기관기호";
            // 
            // ADD0715E_JABO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 479);
            this.Controls.Add(this.txtRedpt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label1);
            this.Name = "ADD0715E_JABO";
            this.Text = "[자보]보완자료요청내역서(ADD0715E_JABO)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcCALLNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcCALLPAFGNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcLNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcMKDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcABTFGNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcABTTXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCALLTXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBYEQTY;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRedpt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
    }
}


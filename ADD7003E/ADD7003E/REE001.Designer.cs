namespace ADD7003E
{
    partial class REE001
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdSdr = new DevExpress.XtraGrid.GridControl();
            this.grdSdrView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSDR_DIAG_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSDR_DGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSDR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSDR_LCS_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRTP_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRT_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRBM_LIST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTRET_PLAN_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDiag = new DevExpress.XtraGrid.GridControl();
            this.grdDiagView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcFDEC_DIAG_YN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDIAG_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDIAG_SICK_SYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSopr = new DevExpress.XtraGrid.GridControl();
            this.grdSoprView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSOPR_ENFC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSOPR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSdrView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(174, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdSdr
            // 
            this.grdSdr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSdr.Location = new System.Drawing.Point(430, 361);
            this.grdSdr.MainView = this.grdSdrView;
            this.grdSdr.Name = "grdSdr";
            this.grdSdr.Size = new System.Drawing.Size(442, 159);
            this.grdSdr.TabIndex = 8;
            this.grdSdr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSdrView});
            // 
            // grdSdrView
            // 
            this.grdSdrView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSDR_DIAG_DT,
            this.gcSDR_DGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcSDR_NM,
            this.gcSDR_LCS_NO,
            this.gcWRTP_NM,
            this.gcWRT_DT,
            this.gcPRBM_LIST_TXT,
            this.gcTRET_PLAN_TXT});
            this.grdSdrView.GridControl = this.grdSdr;
            this.grdSdrView.Name = "grdSdrView";
            this.grdSdrView.OptionsCustomization.AllowSort = false;
            this.grdSdrView.OptionsView.ColumnAutoWidth = false;
            this.grdSdrView.OptionsView.ShowGroupPanel = false;
            this.grdSdrView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcSDR_DIAG_DT
            // 
            this.gcSDR_DIAG_DT.Caption = "진료일시";
            this.gcSDR_DIAG_DT.FieldName = "SDR_DIAG_DT";
            this.gcSDR_DIAG_DT.Name = "gcSDR_DIAG_DT";
            this.gcSDR_DIAG_DT.OptionsColumn.ReadOnly = true;
            this.gcSDR_DIAG_DT.Visible = true;
            this.gcSDR_DIAG_DT.VisibleIndex = 0;
            this.gcSDR_DIAG_DT.Width = 95;
            // 
            // gcSDR_DGSBJT_CD
            // 
            this.gcSDR_DGSBJT_CD.Caption = "진료과";
            this.gcSDR_DGSBJT_CD.FieldName = "SDR_DGSBJT_CD";
            this.gcSDR_DGSBJT_CD.Name = "gcSDR_DGSBJT_CD";
            this.gcSDR_DGSBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcSDR_DGSBJT_CD.Visible = true;
            this.gcSDR_DGSBJT_CD.VisibleIndex = 1;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 2;
            // 
            // gcSDR_NM
            // 
            this.gcSDR_NM.Caption = "진료의";
            this.gcSDR_NM.FieldName = "SDR_NM";
            this.gcSDR_NM.Name = "gcSDR_NM";
            this.gcSDR_NM.OptionsColumn.ReadOnly = true;
            this.gcSDR_NM.Visible = true;
            this.gcSDR_NM.VisibleIndex = 3;
            // 
            // gcSDR_LCS_NO
            // 
            this.gcSDR_LCS_NO.Caption = "면허번호";
            this.gcSDR_LCS_NO.FieldName = "SDR_LCS_NO";
            this.gcSDR_LCS_NO.Name = "gcSDR_LCS_NO";
            this.gcSDR_LCS_NO.OptionsColumn.ReadOnly = true;
            this.gcSDR_LCS_NO.Visible = true;
            this.gcSDR_LCS_NO.VisibleIndex = 4;
            // 
            // gcWRTP_NM
            // 
            this.gcWRTP_NM.Caption = "작성자";
            this.gcWRTP_NM.FieldName = "WRTP_NM";
            this.gcWRTP_NM.Name = "gcWRTP_NM";
            this.gcWRTP_NM.OptionsColumn.ReadOnly = true;
            this.gcWRTP_NM.Visible = true;
            this.gcWRTP_NM.VisibleIndex = 5;
            // 
            // gcWRT_DT
            // 
            this.gcWRT_DT.Caption = "작성일시";
            this.gcWRT_DT.FieldName = "WRT_DT";
            this.gcWRT_DT.Name = "gcWRT_DT";
            this.gcWRT_DT.OptionsColumn.ReadOnly = true;
            this.gcWRT_DT.Visible = true;
            this.gcWRT_DT.VisibleIndex = 6;
            this.gcWRT_DT.Width = 95;
            // 
            // gcPRBM_LIST_TXT
            // 
            this.gcPRBM_LIST_TXT.Caption = "문제목록";
            this.gcPRBM_LIST_TXT.FieldName = "PRBM_LIST_TXT";
            this.gcPRBM_LIST_TXT.Name = "gcPRBM_LIST_TXT";
            this.gcPRBM_LIST_TXT.OptionsColumn.ReadOnly = true;
            this.gcPRBM_LIST_TXT.Visible = true;
            this.gcPRBM_LIST_TXT.VisibleIndex = 7;
            // 
            // gcTRET_PLAN_TXT
            // 
            this.gcTRET_PLAN_TXT.Caption = "치료계획";
            this.gcTRET_PLAN_TXT.FieldName = "TRET_PLAN_TXT";
            this.gcTRET_PLAN_TXT.Name = "gcTRET_PLAN_TXT";
            this.gcTRET_PLAN_TXT.OptionsColumn.ReadOnly = true;
            this.gcTRET_PLAN_TXT.Visible = true;
            this.gcTRET_PLAN_TXT.VisibleIndex = 8;
            // 
            // grdDiag
            // 
            this.grdDiag.Location = new System.Drawing.Point(430, 41);
            this.grdDiag.MainView = this.grdDiagView;
            this.grdDiag.Name = "grdDiag";
            this.grdDiag.Size = new System.Drawing.Size(346, 314);
            this.grdDiag.TabIndex = 10;
            this.grdDiag.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagView});
            // 
            // grdDiagView
            // 
            this.grdDiagView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcFDEC_DIAG_YN,
            this.gcDIAG_NM,
            this.gcDIAG_SICK_SYM});
            this.grdDiagView.GridControl = this.grdDiag;
            this.grdDiagView.Name = "grdDiagView";
            this.grdDiagView.OptionsCustomization.AllowSort = false;
            this.grdDiagView.OptionsView.ColumnAutoWidth = false;
            this.grdDiagView.OptionsView.ShowGroupPanel = false;
            this.grdDiagView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcFDEC_DIAG_YN
            // 
            this.gcFDEC_DIAG_YN.Caption = "확진여부";
            this.gcFDEC_DIAG_YN.FieldName = "FDEC_DIAG_YN";
            this.gcFDEC_DIAG_YN.Name = "gcFDEC_DIAG_YN";
            this.gcFDEC_DIAG_YN.OptionsColumn.ReadOnly = true;
            this.gcFDEC_DIAG_YN.Visible = true;
            this.gcFDEC_DIAG_YN.VisibleIndex = 0;
            this.gcFDEC_DIAG_YN.Width = 55;
            // 
            // gcDIAG_NM
            // 
            this.gcDIAG_NM.Caption = "진단명";
            this.gcDIAG_NM.FieldName = "DIAG_NM";
            this.gcDIAG_NM.Name = "gcDIAG_NM";
            this.gcDIAG_NM.OptionsColumn.ReadOnly = true;
            this.gcDIAG_NM.Visible = true;
            this.gcDIAG_NM.VisibleIndex = 1;
            this.gcDIAG_NM.Width = 185;
            // 
            // gcDIAG_SICK_SYM
            // 
            this.gcDIAG_SICK_SYM.Caption = "상병코드";
            this.gcDIAG_SICK_SYM.FieldName = "DIAG_SICK_SYM";
            this.gcDIAG_SICK_SYM.Name = "gcDIAG_SICK_SYM";
            this.gcDIAG_SICK_SYM.OptionsColumn.ReadOnly = true;
            this.gcDIAG_SICK_SYM.Visible = true;
            this.gcDIAG_SICK_SYM.VisibleIndex = 2;
            this.gcDIAG_SICK_SYM.Width = 65;
            // 
            // grdSopr
            // 
            this.grdSopr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSopr.Location = new System.Drawing.Point(782, 41);
            this.grdSopr.MainView = this.grdSoprView;
            this.grdSopr.Name = "grdSopr";
            this.grdSopr.Size = new System.Drawing.Size(90, 314);
            this.grdSopr.TabIndex = 11;
            this.grdSopr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSoprView});
            // 
            // grdSoprView
            // 
            this.grdSoprView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSOPR_ENFC_DT,
            this.gcSOPR_NM});
            this.grdSoprView.GridControl = this.grdSopr;
            this.grdSoprView.Name = "grdSoprView";
            this.grdSoprView.OptionsCustomization.AllowSort = false;
            this.grdSoprView.OptionsView.ColumnAutoWidth = false;
            this.grdSoprView.OptionsView.ShowGroupPanel = false;
            this.grdSoprView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcSOPR_ENFC_DT
            // 
            this.gcSOPR_ENFC_DT.Caption = "시행일시";
            this.gcSOPR_ENFC_DT.FieldName = "SOPR_ENFC_DT";
            this.gcSOPR_ENFC_DT.Name = "gcSOPR_ENFC_DT";
            this.gcSOPR_ENFC_DT.OptionsColumn.ReadOnly = true;
            this.gcSOPR_ENFC_DT.Visible = true;
            this.gcSOPR_ENFC_DT.VisibleIndex = 0;
            this.gcSOPR_ENFC_DT.Width = 95;
            // 
            // gcSOPR_NM
            // 
            this.gcSOPR_NM.Caption = "수술및처치";
            this.gcSOPR_NM.FieldName = "SOPR_NM";
            this.gcSOPR_NM.Name = "gcSOPR_NM";
            this.gcSOPR_NM.OptionsColumn.ReadOnly = true;
            this.gcSOPR_NM.Visible = true;
            this.gcSOPR_NM.VisibleIndex = 1;
            this.gcSOPR_NM.Width = 330;
            // 
            // grdInfo
            // 
            this.grdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(412, 479);
            this.grdInfo.TabIndex = 12;
            this.grdInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdInfoView});
            // 
            // grdInfoView
            // 
            this.grdInfoView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcITEM,
            this.gcCONTENT});
            this.grdInfoView.GridControl = this.grdInfo;
            this.grdInfoView.Name = "grdInfoView";
            this.grdInfoView.OptionsCustomization.AllowSort = false;
            this.grdInfoView.OptionsView.ColumnAutoWidth = false;
            this.grdInfoView.OptionsView.ShowGroupPanel = false;
            this.grdInfoView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdInfoView.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.grdInfoView_CalcRowHeight);
            // 
            // gcITEM
            // 
            this.gcITEM.Caption = "항목";
            this.gcITEM.FieldName = "ITEM";
            this.gcITEM.Name = "gcITEM";
            this.gcITEM.OptionsColumn.ReadOnly = true;
            this.gcITEM.Visible = true;
            this.gcITEM.VisibleIndex = 0;
            this.gcITEM.Width = 120;
            // 
            // gcCONTENT
            // 
            this.gcCONTENT.Caption = "내용";
            this.gcCONTENT.FieldName = "CONTENT";
            this.gcCONTENT.Name = "gcCONTENT";
            this.gcCONTENT.OptionsColumn.ReadOnly = true;
            this.gcCONTENT.Visible = true;
            this.gcCONTENT.VisibleIndex = 1;
            this.gcCONTENT.Width = 250;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(740, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "응급기록자료(REE001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 40;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // REE001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.grdSopr);
            this.Controls.Add(this.grdDiag);
            this.Controls.Add(this.grdSdr);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "REE001";
            this.Text = "응급기록자료(REE001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdSdr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSdrView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdSdr;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSdrView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSDR_DIAG_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSDR_DGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcSDR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSDR_LCS_NO;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRTP_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRT_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRBM_LIST_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTRET_PLAN_TXT;
        private DevExpress.XtraGrid.GridControl grdDiag;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagView;
        private DevExpress.XtraGrid.Columns.GridColumn gcFDEC_DIAG_YN;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_SICK_SYM;
        private DevExpress.XtraGrid.GridControl grdSopr;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSoprView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_ENFC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_NM;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
namespace ADD7003E
{
    partial class RIP001
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
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDiag = new DevExpress.XtraGrid.GridControl();
            this.grdDiagView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSICK_TP_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDIAG_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDIAG_SICK_SYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSopr = new DevExpress.XtraGrid.GridControl();
            this.grdSoprView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSOPR_ENFC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSOPR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdElaps = new DevExpress.XtraGrid.GridControl();
            this.grdElapsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDIAG_DD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHRG_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRTP_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRT_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRBM_LIST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTRET_PLAN_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcELAPS_TEXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIS_SEND_DATA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdElaps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdElapsView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(174, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdInfo
            // 
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(436, 210);
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
            this.grdInfoView.OptionsBehavior.ReadOnly = true;
            this.grdInfoView.OptionsCustomization.AllowSort = false;
            this.grdInfoView.OptionsView.ColumnAutoWidth = false;
            this.grdInfoView.OptionsView.ShowGroupPanel = false;
            this.grdInfoView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcITEM
            // 
            this.gcITEM.Caption = "항목";
            this.gcITEM.FieldName = "ITEM";
            this.gcITEM.Name = "gcITEM";
            this.gcITEM.Visible = true;
            this.gcITEM.VisibleIndex = 0;
            this.gcITEM.Width = 95;
            // 
            // gcCONTENT
            // 
            this.gcCONTENT.Caption = "내용";
            this.gcCONTENT.FieldName = "CONTENT";
            this.gcCONTENT.Name = "gcCONTENT";
            this.gcCONTENT.Visible = true;
            this.gcCONTENT.VisibleIndex = 1;
            this.gcCONTENT.Width = 300;
            // 
            // grdDiag
            // 
            this.grdDiag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDiag.Location = new System.Drawing.Point(454, 41);
            this.grdDiag.MainView = this.grdDiagView;
            this.grdDiag.Name = "grdDiag";
            this.grdDiag.Size = new System.Drawing.Size(418, 210);
            this.grdDiag.TabIndex = 13;
            this.grdDiag.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagView});
            // 
            // grdDiagView
            // 
            this.grdDiagView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSICK_TP_CD,
            this.gcDIAG_NM,
            this.gcDIAG_SICK_SYM});
            this.grdDiagView.GridControl = this.grdDiag;
            this.grdDiagView.Name = "grdDiagView";
            this.grdDiagView.OptionsBehavior.ReadOnly = true;
            this.grdDiagView.OptionsCustomization.AllowSort = false;
            this.grdDiagView.OptionsView.ColumnAutoWidth = false;
            this.grdDiagView.OptionsView.ShowGroupPanel = false;
            this.grdDiagView.OptionsView.ShowViewCaption = true;
            this.grdDiagView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdDiagView.ViewCaption = "진단";
            // 
            // gcSICK_TP_CD
            // 
            this.gcSICK_TP_CD.Caption = "구분";
            this.gcSICK_TP_CD.FieldName = "SICK_TP_CD";
            this.gcSICK_TP_CD.Name = "gcSICK_TP_CD";
            this.gcSICK_TP_CD.Visible = true;
            this.gcSICK_TP_CD.VisibleIndex = 0;
            this.gcSICK_TP_CD.Width = 45;
            // 
            // gcDIAG_NM
            // 
            this.gcDIAG_NM.Caption = "진단명";
            this.gcDIAG_NM.FieldName = "DIAG_NM";
            this.gcDIAG_NM.Name = "gcDIAG_NM";
            this.gcDIAG_NM.Visible = true;
            this.gcDIAG_NM.VisibleIndex = 1;
            this.gcDIAG_NM.Width = 350;
            // 
            // gcDIAG_SICK_SYM
            // 
            this.gcDIAG_SICK_SYM.Caption = "상병코드";
            this.gcDIAG_SICK_SYM.FieldName = "DIAG_SICK_SYM";
            this.gcDIAG_SICK_SYM.Name = "gcDIAG_SICK_SYM";
            this.gcDIAG_SICK_SYM.Visible = true;
            this.gcDIAG_SICK_SYM.VisibleIndex = 2;
            // 
            // grdSopr
            // 
            this.grdSopr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdSopr.Location = new System.Drawing.Point(12, 257);
            this.grdSopr.MainView = this.grdSoprView;
            this.grdSopr.Name = "grdSopr";
            this.grdSopr.Size = new System.Drawing.Size(436, 263);
            this.grdSopr.TabIndex = 14;
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
            this.grdSoprView.OptionsBehavior.ReadOnly = true;
            this.grdSoprView.OptionsCustomization.AllowSort = false;
            this.grdSoprView.OptionsView.ColumnAutoWidth = false;
            this.grdSoprView.OptionsView.ShowGroupPanel = false;
            this.grdSoprView.OptionsView.ShowViewCaption = true;
            this.grdSoprView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdSoprView.ViewCaption = "시술,처치,수술";
            // 
            // gcSOPR_ENFC_DT
            // 
            this.gcSOPR_ENFC_DT.Caption = "시행일시";
            this.gcSOPR_ENFC_DT.FieldName = "SOPR_ENFC_DT";
            this.gcSOPR_ENFC_DT.Name = "gcSOPR_ENFC_DT";
            this.gcSOPR_ENFC_DT.Visible = true;
            this.gcSOPR_ENFC_DT.VisibleIndex = 0;
            this.gcSOPR_ENFC_DT.Width = 95;
            // 
            // gcSOPR_NM
            // 
            this.gcSOPR_NM.Caption = "명칭";
            this.gcSOPR_NM.FieldName = "SOPR_NM";
            this.gcSOPR_NM.Name = "gcSOPR_NM";
            this.gcSOPR_NM.Visible = true;
            this.gcSOPR_NM.VisibleIndex = 1;
            this.gcSOPR_NM.Width = 300;
            // 
            // grdElaps
            // 
            this.grdElaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdElaps.Location = new System.Drawing.Point(454, 257);
            this.grdElaps.MainView = this.grdElapsView;
            this.grdElaps.Name = "grdElaps";
            this.grdElaps.Size = new System.Drawing.Size(418, 263);
            this.grdElaps.TabIndex = 15;
            this.grdElaps.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdElapsView});
            // 
            // grdElapsView
            // 
            this.grdElapsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDIAG_DD,
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcCHRG_DR_NM,
            this.gcWRTP_NM,
            this.gcWRT_DT,
            this.gcPRBM_LIST_TXT,
            this.gcTRET_PLAN_TXT,
            this.gcELAPS_TEXT,
            this.gcIS_SEND_DATA});
            this.grdElapsView.GridControl = this.grdElaps;
            this.grdElapsView.Name = "grdElapsView";
            this.grdElapsView.OptionsBehavior.ReadOnly = true;
            this.grdElapsView.OptionsCustomization.AllowSort = false;
            this.grdElapsView.OptionsView.ColumnAutoWidth = false;
            this.grdElapsView.OptionsView.ShowGroupPanel = false;
            this.grdElapsView.OptionsView.ShowViewCaption = true;
            this.grdElapsView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdElapsView.ViewCaption = "입원경과";
            // 
            // gcDIAG_DD
            // 
            this.gcDIAG_DD.Caption = "진료일자";
            this.gcDIAG_DD.FieldName = "DIAG_DD";
            this.gcDIAG_DD.Name = "gcDIAG_DD";
            this.gcDIAG_DD.Visible = true;
            this.gcDIAG_DD.VisibleIndex = 0;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "진료과목";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 1;
            this.gcDGSBJT_CD.Width = 55;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 2;
            this.gcIFLD_DTL_SPC_SBJT_CD.Width = 55;
            // 
            // gcCHRG_DR_NM
            // 
            this.gcCHRG_DR_NM.Caption = "담당의사";
            this.gcCHRG_DR_NM.FieldName = "CHRG_DR_NM";
            this.gcCHRG_DR_NM.Name = "gcCHRG_DR_NM";
            this.gcCHRG_DR_NM.Visible = true;
            this.gcCHRG_DR_NM.VisibleIndex = 3;
            this.gcCHRG_DR_NM.Width = 55;
            // 
            // gcWRTP_NM
            // 
            this.gcWRTP_NM.Caption = "작성자";
            this.gcWRTP_NM.FieldName = "WRTP_NM";
            this.gcWRTP_NM.Name = "gcWRTP_NM";
            this.gcWRTP_NM.Visible = true;
            this.gcWRTP_NM.VisibleIndex = 4;
            this.gcWRTP_NM.Width = 55;
            // 
            // gcWRT_DT
            // 
            this.gcWRT_DT.Caption = "작성일시";
            this.gcWRT_DT.FieldName = "WRT_DT";
            this.gcWRT_DT.Name = "gcWRT_DT";
            this.gcWRT_DT.Visible = true;
            this.gcWRT_DT.VisibleIndex = 5;
            this.gcWRT_DT.Width = 95;
            // 
            // gcPRBM_LIST_TXT
            // 
            this.gcPRBM_LIST_TXT.Caption = "정보및평가";
            this.gcPRBM_LIST_TXT.FieldName = "PRBM_LIST_TXT";
            this.gcPRBM_LIST_TXT.Name = "gcPRBM_LIST_TXT";
            // 
            // gcTRET_PLAN_TXT
            // 
            this.gcTRET_PLAN_TXT.Caption = "치료계획";
            this.gcTRET_PLAN_TXT.FieldName = "TRET_PLAN_TXT";
            this.gcTRET_PLAN_TXT.Name = "gcTRET_PLAN_TXT";
            // 
            // gcELAPS_TEXT
            // 
            this.gcELAPS_TEXT.Caption = "정보,평가,치료계획";
            this.gcELAPS_TEXT.FieldName = "ELAPS_TEXT";
            this.gcELAPS_TEXT.Name = "gcELAPS_TEXT";
            this.gcELAPS_TEXT.Visible = true;
            this.gcELAPS_TEXT.VisibleIndex = 6;
            this.gcELAPS_TEXT.Width = 300;
            // 
            // gcIS_SEND_DATA
            // 
            this.gcIS_SEND_DATA.Caption = "전송자료여부";
            this.gcIS_SEND_DATA.FieldName = "IS_SEND_DATA";
            this.gcIS_SEND_DATA.Name = "gcIS_SEND_DATA";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(723, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "입원경과기록자료(RIP001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 42;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RIP001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdElaps);
            this.Controls.Add(this.grdSopr);
            this.Controls.Add(this.grdDiag);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RIP001";
            this.Text = "입원경과기록자료(RIP001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdElaps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdElapsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.GridControl grdDiag;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagView;
        private DevExpress.XtraGrid.GridControl grdSopr;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSoprView;
        private DevExpress.XtraGrid.GridControl grdElaps;
        private DevExpress.XtraGrid.Views.Grid.GridView grdElapsView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_TP_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_SICK_SYM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_ENFC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_DD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHRG_DR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRTP_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRT_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRBM_LIST_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTRET_PLAN_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
        private DevExpress.XtraGrid.Columns.GridColumn gcELAPS_TEXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcIS_SEND_DATA;
    }
}
namespace ADD7003E
{
    partial class RAA001
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
            this.grdDiag = new DevExpress.XtraGrid.GridControl();
            this.grdDiagView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDIAG_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSICK_SYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdVtsg = new DevExpress.XtraGrid.GridControl();
            this.grdVtsgView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMASR_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBPRSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPULS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBRT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTMPR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdMdct = new DevExpress.XtraGrid.GridControl();
            this.grdMdctView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNCT_KND_CD_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDCT_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFQ1_MDCT_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_UNIT_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSopr = new DevExpress.XtraGrid.GridControl();
            this.grdSoprView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSOPR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDFEE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            this.gcNCT_KND_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsgView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdctView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDiag
            // 
            this.grdDiag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDiag.Location = new System.Drawing.Point(12, 389);
            this.grdDiag.MainView = this.grdDiagView;
            this.grdDiag.Name = "grdDiag";
            this.grdDiag.Size = new System.Drawing.Size(436, 131);
            this.grdDiag.TabIndex = 23;
            this.grdDiag.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagView});
            // 
            // grdDiagView
            // 
            this.grdDiagView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDIAG_NM,
            this.gcSICK_SYM});
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
            // gcDIAG_NM
            // 
            this.gcDIAG_NM.Caption = "진단명";
            this.gcDIAG_NM.FieldName = "DIAG_NM";
            this.gcDIAG_NM.Name = "gcDIAG_NM";
            this.gcDIAG_NM.Visible = true;
            this.gcDIAG_NM.VisibleIndex = 0;
            this.gcDIAG_NM.Width = 320;
            // 
            // gcSICK_SYM
            // 
            this.gcSICK_SYM.Caption = "상병코드";
            this.gcSICK_SYM.FieldName = "SICK_SYM";
            this.gcSICK_SYM.Name = "gcSICK_SYM";
            this.gcSICK_SYM.Visible = true;
            this.gcSICK_SYM.VisibleIndex = 1;
            // 
            // grdInfo
            // 
            this.grdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(436, 205);
            this.grdInfo.TabIndex = 22;
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
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(174, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 21;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 20;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdVtsg
            // 
            this.grdVtsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVtsg.Location = new System.Drawing.Point(454, 41);
            this.grdVtsg.MainView = this.grdVtsgView;
            this.grdVtsg.Name = "grdVtsg";
            this.grdVtsg.Size = new System.Drawing.Size(418, 342);
            this.grdVtsg.TabIndex = 24;
            this.grdVtsg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdVtsgView});
            // 
            // grdVtsgView
            // 
            this.grdVtsgView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMASR_DT,
            this.gcBPRSU,
            this.gcPULS,
            this.gcBRT,
            this.gcTMPR});
            this.grdVtsgView.GridControl = this.grdVtsg;
            this.grdVtsgView.Name = "grdVtsgView";
            this.grdVtsgView.OptionsBehavior.ReadOnly = true;
            this.grdVtsgView.OptionsCustomization.AllowSort = false;
            this.grdVtsgView.OptionsView.ColumnAutoWidth = false;
            this.grdVtsgView.OptionsView.ShowGroupPanel = false;
            this.grdVtsgView.OptionsView.ShowViewCaption = true;
            this.grdVtsgView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdVtsgView.ViewCaption = "활력징후";
            // 
            // gcMASR_DT
            // 
            this.gcMASR_DT.Caption = "측정일시";
            this.gcMASR_DT.FieldName = "MASR_DT";
            this.gcMASR_DT.Name = "gcMASR_DT";
            this.gcMASR_DT.Visible = true;
            this.gcMASR_DT.VisibleIndex = 0;
            this.gcMASR_DT.Width = 95;
            // 
            // gcBPRSU
            // 
            this.gcBPRSU.Caption = "혈압";
            this.gcBPRSU.FieldName = "BPRSU";
            this.gcBPRSU.Name = "gcBPRSU";
            this.gcBPRSU.Visible = true;
            this.gcBPRSU.VisibleIndex = 1;
            // 
            // gcPULS
            // 
            this.gcPULS.Caption = "맥박";
            this.gcPULS.FieldName = "PULS";
            this.gcPULS.Name = "gcPULS";
            this.gcPULS.Visible = true;
            this.gcPULS.VisibleIndex = 2;
            // 
            // gcBRT
            // 
            this.gcBRT.Caption = "호흡";
            this.gcBRT.FieldName = "BRT";
            this.gcBRT.Name = "gcBRT";
            this.gcBRT.Visible = true;
            this.gcBRT.VisibleIndex = 3;
            // 
            // gcTMPR
            // 
            this.gcTMPR.Caption = "체온";
            this.gcTMPR.FieldName = "TMPR";
            this.gcTMPR.Name = "gcTMPR";
            this.gcTMPR.Visible = true;
            this.gcTMPR.VisibleIndex = 4;
            // 
            // grdMdct
            // 
            this.grdMdct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMdct.Location = new System.Drawing.Point(454, 389);
            this.grdMdct.MainView = this.grdMdctView;
            this.grdMdct.Name = "grdMdct";
            this.grdMdct.Size = new System.Drawing.Size(418, 131);
            this.grdMdct.TabIndex = 26;
            this.grdMdct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMdctView});
            // 
            // grdMdctView
            // 
            this.grdMdctView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNCT_KND_CD_NM,
            this.gcMDCT_DT,
            this.gcMDS_NM,
            this.gcFQ1_MDCT_QTY,
            this.gcMDS_UNIT_TXT,
            this.gcNCT_KND_CD});
            this.grdMdctView.GridControl = this.grdMdct;
            this.grdMdctView.Name = "grdMdctView";
            this.grdMdctView.OptionsBehavior.ReadOnly = true;
            this.grdMdctView.OptionsCustomization.AllowSort = false;
            this.grdMdctView.OptionsView.ColumnAutoWidth = false;
            this.grdMdctView.OptionsView.ShowGroupPanel = false;
            this.grdMdctView.OptionsView.ShowViewCaption = true;
            this.grdMdctView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMdctView.ViewCaption = "마취중 투약";
            // 
            // gcNCT_KND_CD_NM
            // 
            this.gcNCT_KND_CD_NM.Caption = "분류";
            this.gcNCT_KND_CD_NM.FieldName = "NCT_KND_CD_NM";
            this.gcNCT_KND_CD_NM.Name = "gcNCT_KND_CD_NM";
            this.gcNCT_KND_CD_NM.Visible = true;
            this.gcNCT_KND_CD_NM.VisibleIndex = 0;
            this.gcNCT_KND_CD_NM.Width = 45;
            // 
            // gcMDCT_DT
            // 
            this.gcMDCT_DT.Caption = "투약일시";
            this.gcMDCT_DT.FieldName = "MDCT_DT";
            this.gcMDCT_DT.Name = "gcMDCT_DT";
            this.gcMDCT_DT.Visible = true;
            this.gcMDCT_DT.VisibleIndex = 1;
            this.gcMDCT_DT.Width = 95;
            // 
            // gcMDS_NM
            // 
            this.gcMDS_NM.Caption = "약품명";
            this.gcMDS_NM.FieldName = "MDS_NM";
            this.gcMDS_NM.Name = "gcMDS_NM";
            this.gcMDS_NM.Visible = true;
            this.gcMDS_NM.VisibleIndex = 2;
            this.gcMDS_NM.Width = 250;
            // 
            // gcFQ1_MDCT_QTY
            // 
            this.gcFQ1_MDCT_QTY.Caption = "1회투약량";
            this.gcFQ1_MDCT_QTY.FieldName = "FQ1_MDCT_QTY";
            this.gcFQ1_MDCT_QTY.Name = "gcFQ1_MDCT_QTY";
            this.gcFQ1_MDCT_QTY.Visible = true;
            this.gcFQ1_MDCT_QTY.VisibleIndex = 3;
            // 
            // gcMDS_UNIT_TXT
            // 
            this.gcMDS_UNIT_TXT.Caption = "단위";
            this.gcMDS_UNIT_TXT.FieldName = "MDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.Name = "gcMDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.Visible = true;
            this.gcMDS_UNIT_TXT.VisibleIndex = 4;
            // 
            // grdSopr
            // 
            this.grdSopr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdSopr.Location = new System.Drawing.Point(12, 252);
            this.grdSopr.MainView = this.grdSoprView;
            this.grdSopr.Name = "grdSopr";
            this.grdSopr.Size = new System.Drawing.Size(436, 131);
            this.grdSopr.TabIndex = 28;
            this.grdSopr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSoprView});
            // 
            // grdSoprView
            // 
            this.grdSoprView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSOPR_NM,
            this.gcMDFEE_CD});
            this.grdSoprView.GridControl = this.grdSopr;
            this.grdSoprView.Name = "grdSoprView";
            this.grdSoprView.OptionsBehavior.ReadOnly = true;
            this.grdSoprView.OptionsCustomization.AllowSort = false;
            this.grdSoprView.OptionsView.ColumnAutoWidth = false;
            this.grdSoprView.OptionsView.ShowGroupPanel = false;
            this.grdSoprView.OptionsView.ShowViewCaption = true;
            this.grdSoprView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdSoprView.ViewCaption = "수술";
            // 
            // gcSOPR_NM
            // 
            this.gcSOPR_NM.Caption = "수술명";
            this.gcSOPR_NM.FieldName = "SOPR_NM";
            this.gcSOPR_NM.Name = "gcSOPR_NM";
            this.gcSOPR_NM.Visible = true;
            this.gcSOPR_NM.VisibleIndex = 0;
            this.gcSOPR_NM.Width = 320;
            // 
            // gcMDFEE_CD
            // 
            this.gcMDFEE_CD.Caption = "수가코드";
            this.gcMDFEE_CD.FieldName = "MDFEE_CD";
            this.gcMDFEE_CD.Name = "gcMDFEE_CD";
            this.gcMDFEE_CD.Visible = true;
            this.gcMDFEE_CD.VisibleIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(741, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "마취기록자료(RAA001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 30;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // gcNCT_KND_CD
            // 
            this.gcNCT_KND_CD.Caption = "분류코드";
            this.gcNCT_KND_CD.FieldName = "NCT_KND_CD";
            this.gcNCT_KND_CD.Name = "gcNCT_KND_CD";
            // 
            // RAA001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdSopr);
            this.Controls.Add(this.grdMdct);
            this.Controls.Add(this.grdVtsg);
            this.Controls.Add(this.grdDiag);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RAA001";
            this.Text = "마취기록자료(RAA001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsgView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdctView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdDiag;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_SYM;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdVtsg;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVtsgView;
        private DevExpress.XtraGrid.GridControl grdMdct;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMdctView;
        private DevExpress.XtraGrid.GridControl grdSopr;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSoprView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDFEE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBPRSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcPULS;
        private DevExpress.XtraGrid.Columns.GridColumn gcBRT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTMPR;
        private DevExpress.XtraGrid.Columns.GridColumn gcNCT_KND_CD_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDCT_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcFQ1_MDCT_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_UNIT_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
        private DevExpress.XtraGrid.Columns.GridColumn gcNCT_KND_CD;
    }
}
namespace ADD7003E
{
    partial class RNS001
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
            this.grdSopr = new DevExpress.XtraGrid.GridControl();
            this.grdSoprView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSOPR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDFEE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdDiagBf = new DevExpress.XtraGrid.GridControl();
            this.grdDiagBfView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDIAG_NM_BF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSICK_SYM_BF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDiagAf = new DevExpress.XtraGrid.GridControl();
            this.grdDiagAfView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDIAG_NM_AF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSICK_SYM_AF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdTmcat = new DevExpress.XtraGrid.GridControl();
            this.grdTmcatView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTMCAT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTMCAT_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNOM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdMds = new DevExpress.XtraGrid.GridControl();
            this.grdMdsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMDS_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTOT_INJC_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_UNIT_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINJC_MTH_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagBf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagBfView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagAf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagAfView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTmcat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTmcatView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdsView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSopr
            // 
            this.grdSopr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSopr.Location = new System.Drawing.Point(449, 41);
            this.grdSopr.MainView = this.grdSoprView;
            this.grdSopr.Name = "grdSopr";
            this.grdSopr.Size = new System.Drawing.Size(423, 149);
            this.grdSopr.TabIndex = 30;
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
            this.grdSoprView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcSOPR_NM
            // 
            this.gcSOPR_NM.Caption = "수술명";
            this.gcSOPR_NM.FieldName = "SOPR_NM";
            this.gcSOPR_NM.Name = "gcSOPR_NM";
            this.gcSOPR_NM.Visible = true;
            this.gcSOPR_NM.VisibleIndex = 0;
            this.gcSOPR_NM.Width = 600;
            // 
            // gcMDFEE_CD
            // 
            this.gcMDFEE_CD.Caption = "수가코드";
            this.gcMDFEE_CD.FieldName = "MDFEE_CD";
            this.gcMDFEE_CD.Name = "gcMDFEE_CD";
            this.gcMDFEE_CD.Visible = true;
            this.gcMDFEE_CD.VisibleIndex = 1;
            // 
            // grdInfo
            // 
            this.grdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(430, 149);
            this.grdInfo.TabIndex = 29;
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
            this.btnSubmit.TabIndex = 28;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 27;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdDiagBf
            // 
            this.grdDiagBf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDiagBf.Location = new System.Drawing.Point(12, 200);
            this.grdDiagBf.MainView = this.grdDiagBfView;
            this.grdDiagBf.Name = "grdDiagBf";
            this.grdDiagBf.Size = new System.Drawing.Size(430, 157);
            this.grdDiagBf.TabIndex = 31;
            this.grdDiagBf.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagBfView});
            // 
            // grdDiagBfView
            // 
            this.grdDiagBfView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDIAG_NM_BF,
            this.gcSICK_SYM_BF});
            this.grdDiagBfView.GridControl = this.grdDiagBf;
            this.grdDiagBfView.Name = "grdDiagBfView";
            this.grdDiagBfView.OptionsBehavior.ReadOnly = true;
            this.grdDiagBfView.OptionsCustomization.AllowSort = false;
            this.grdDiagBfView.OptionsView.ColumnAutoWidth = false;
            this.grdDiagBfView.OptionsView.ShowGroupPanel = false;
            this.grdDiagBfView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDIAG_NM_BF
            // 
            this.gcDIAG_NM_BF.Caption = "수술전 진단명";
            this.gcDIAG_NM_BF.FieldName = "DIAG_NM";
            this.gcDIAG_NM_BF.Name = "gcDIAG_NM_BF";
            this.gcDIAG_NM_BF.Visible = true;
            this.gcDIAG_NM_BF.VisibleIndex = 0;
            this.gcDIAG_NM_BF.Width = 300;
            // 
            // gcSICK_SYM_BF
            // 
            this.gcSICK_SYM_BF.Caption = "상병코드";
            this.gcSICK_SYM_BF.FieldName = "SICK_SYM";
            this.gcSICK_SYM_BF.Name = "gcSICK_SYM_BF";
            this.gcSICK_SYM_BF.Visible = true;
            this.gcSICK_SYM_BF.VisibleIndex = 1;
            // 
            // grdDiagAf
            // 
            this.grdDiagAf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDiagAf.Location = new System.Drawing.Point(12, 363);
            this.grdDiagAf.MainView = this.grdDiagAfView;
            this.grdDiagAf.Name = "grdDiagAf";
            this.grdDiagAf.Size = new System.Drawing.Size(430, 157);
            this.grdDiagAf.TabIndex = 32;
            this.grdDiagAf.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagAfView});
            // 
            // grdDiagAfView
            // 
            this.grdDiagAfView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDIAG_NM_AF,
            this.gcSICK_SYM_AF});
            this.grdDiagAfView.GridControl = this.grdDiagAf;
            this.grdDiagAfView.Name = "grdDiagAfView";
            this.grdDiagAfView.OptionsBehavior.ReadOnly = true;
            this.grdDiagAfView.OptionsCustomization.AllowSort = false;
            this.grdDiagAfView.OptionsView.ColumnAutoWidth = false;
            this.grdDiagAfView.OptionsView.ShowGroupPanel = false;
            this.grdDiagAfView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDIAG_NM_AF
            // 
            this.gcDIAG_NM_AF.Caption = "수술후 진단명";
            this.gcDIAG_NM_AF.FieldName = "DIAG_NM";
            this.gcDIAG_NM_AF.Name = "gcDIAG_NM_AF";
            this.gcDIAG_NM_AF.Visible = true;
            this.gcDIAG_NM_AF.VisibleIndex = 0;
            this.gcDIAG_NM_AF.Width = 300;
            // 
            // gcSICK_SYM_AF
            // 
            this.gcSICK_SYM_AF.Caption = "상병코드";
            this.gcSICK_SYM_AF.FieldName = "SICK_SYM";
            this.gcSICK_SYM_AF.Name = "gcSICK_SYM_AF";
            this.gcSICK_SYM_AF.Visible = true;
            this.gcSICK_SYM_AF.VisibleIndex = 1;
            // 
            // grdTmcat
            // 
            this.grdTmcat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTmcat.Location = new System.Drawing.Point(448, 200);
            this.grdTmcat.MainView = this.grdTmcatView;
            this.grdTmcat.Name = "grdTmcat";
            this.grdTmcat.Size = new System.Drawing.Size(423, 157);
            this.grdTmcat.TabIndex = 33;
            this.grdTmcat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdTmcatView});
            // 
            // grdTmcatView
            // 
            this.grdTmcatView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcTMCAT_CD,
            this.gcTMCAT_NM,
            this.gcNOM,
            this.gcQTY,
            this.gcUNIT});
            this.grdTmcatView.GridControl = this.grdTmcat;
            this.grdTmcatView.Name = "grdTmcatView";
            this.grdTmcatView.OptionsBehavior.ReadOnly = true;
            this.grdTmcatView.OptionsCustomization.AllowSort = false;
            this.grdTmcatView.OptionsView.ColumnAutoWidth = false;
            this.grdTmcatView.OptionsView.ShowGroupPanel = false;
            this.grdTmcatView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcTMCAT_CD
            // 
            this.gcTMCAT_CD.Caption = "재료코드";
            this.gcTMCAT_CD.FieldName = "TMCAT_CD";
            this.gcTMCAT_CD.Name = "gcTMCAT_CD";
            this.gcTMCAT_CD.Visible = true;
            this.gcTMCAT_CD.VisibleIndex = 0;
            // 
            // gcTMCAT_NM
            // 
            this.gcTMCAT_NM.Caption = "재료명";
            this.gcTMCAT_NM.FieldName = "TMCAT_NM";
            this.gcTMCAT_NM.Name = "gcTMCAT_NM";
            this.gcTMCAT_NM.Visible = true;
            this.gcTMCAT_NM.VisibleIndex = 1;
            this.gcTMCAT_NM.Width = 200;
            // 
            // gcNOM
            // 
            this.gcNOM.Caption = "규격";
            this.gcNOM.FieldName = "NOM";
            this.gcNOM.Name = "gcNOM";
            this.gcNOM.Visible = true;
            this.gcNOM.VisibleIndex = 2;
            // 
            // gcQTY
            // 
            this.gcQTY.Caption = "수량";
            this.gcQTY.FieldName = "QTY";
            this.gcQTY.Name = "gcQTY";
            this.gcQTY.Visible = true;
            this.gcQTY.VisibleIndex = 3;
            // 
            // gcUNIT
            // 
            this.gcUNIT.Caption = "단위";
            this.gcUNIT.FieldName = "UNIT";
            this.gcUNIT.Name = "gcUNIT";
            this.gcUNIT.Visible = true;
            this.gcUNIT.VisibleIndex = 4;
            // 
            // grdMds
            // 
            this.grdMds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMds.Location = new System.Drawing.Point(449, 363);
            this.grdMds.MainView = this.grdMdsView;
            this.grdMds.Name = "grdMds";
            this.grdMds.Size = new System.Drawing.Size(423, 157);
            this.grdMds.TabIndex = 34;
            this.grdMds.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMdsView});
            // 
            // grdMdsView
            // 
            this.grdMdsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMDS_CD,
            this.gcMDS_NM,
            this.gcTOT_INJC_QTY,
            this.gcMDS_UNIT_TXT,
            this.gcINJC_MTH_TXT});
            this.grdMdsView.GridControl = this.grdMds;
            this.grdMdsView.Name = "grdMdsView";
            this.grdMdsView.OptionsBehavior.ReadOnly = true;
            this.grdMdsView.OptionsCustomization.AllowSort = false;
            this.grdMdsView.OptionsView.ColumnAutoWidth = false;
            this.grdMdsView.OptionsView.ShowGroupPanel = false;
            this.grdMdsView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcMDS_CD
            // 
            this.gcMDS_CD.Caption = "약품코드";
            this.gcMDS_CD.FieldName = "MDS_CD";
            this.gcMDS_CD.Name = "gcMDS_CD";
            this.gcMDS_CD.Visible = true;
            this.gcMDS_CD.VisibleIndex = 0;
            // 
            // gcMDS_NM
            // 
            this.gcMDS_NM.Caption = "약품명";
            this.gcMDS_NM.FieldName = "MDS_NM";
            this.gcMDS_NM.Name = "gcMDS_NM";
            this.gcMDS_NM.Visible = true;
            this.gcMDS_NM.VisibleIndex = 1;
            this.gcMDS_NM.Width = 200;
            // 
            // gcTOT_INJC_QTY
            // 
            this.gcTOT_INJC_QTY.Caption = "투여용량";
            this.gcTOT_INJC_QTY.FieldName = "TOT_INJC_QTY";
            this.gcTOT_INJC_QTY.Name = "gcTOT_INJC_QTY";
            this.gcTOT_INJC_QTY.Visible = true;
            this.gcTOT_INJC_QTY.VisibleIndex = 2;
            // 
            // gcMDS_UNIT_TXT
            // 
            this.gcMDS_UNIT_TXT.Caption = "단위";
            this.gcMDS_UNIT_TXT.FieldName = "MDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.Name = "gcMDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.Visible = true;
            this.gcMDS_UNIT_TXT.VisibleIndex = 3;
            // 
            // gcINJC_MTH_TXT
            // 
            this.gcINJC_MTH_TXT.Caption = "투여경로";
            this.gcINJC_MTH_TXT.FieldName = "INJC_MTH_TXT";
            this.gcINJC_MTH_TXT.Name = "gcINJC_MTH_TXT";
            this.gcINJC_MTH_TXT.Visible = true;
            this.gcINJC_MTH_TXT.VisibleIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(717, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 12);
            this.label1.TabIndex = 45;
            this.label1.Text = "수술간호기록자료(RNS001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 46;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RNS001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdMds);
            this.Controls.Add(this.grdTmcat);
            this.Controls.Add(this.grdDiagAf);
            this.Controls.Add(this.grdDiagBf);
            this.Controls.Add(this.grdSopr);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RNS001";
            this.Text = "수술간호기록자료(RNS001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdSopr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSoprView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagBf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagBfView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagAf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagAfView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTmcat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTmcatView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMdsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSopr;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSoprView;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcSOPR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDFEE_CD;
        private DevExpress.XtraGrid.GridControl grdDiagBf;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagBfView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM_BF;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_SYM_BF;
        private DevExpress.XtraGrid.GridControl grdDiagAf;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagAfView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM_AF;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_SYM_AF;
        private DevExpress.XtraGrid.GridControl grdTmcat;
        private DevExpress.XtraGrid.Views.Grid.GridView grdTmcatView;
        private DevExpress.XtraGrid.Columns.GridColumn gcTMCAT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcTMCAT_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcNOM;
        private DevExpress.XtraGrid.Columns.GridColumn gcQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNIT;
        private DevExpress.XtraGrid.GridControl grdMds;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMdsView;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcTOT_INJC_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_UNIT_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcINJC_MTH_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
namespace ADD7003E
{
    partial class RAR001
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
            this.grdEmss = new DevExpress.XtraGrid.GridControl();
            this.grdEmssView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEXEC_DT_EMSS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcASM_RST_TXT_EMSS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdPain = new DevExpress.XtraGrid.GridControl();
            this.grdPainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEXEC_DT_PAIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPAIN_ASM_TL_CD_PAIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcASM_TL_ETC_TXT_PAIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcASM_RST_TXT_PAIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdVtsg = new DevExpress.XtraGrid.GridControl();
            this.grdVtsgView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMASR_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBPRSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPULS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBRT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTMPR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOXY_STRT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVTSG_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdRcov = new DevExpress.XtraGrid.GridControl();
            this.grdRcovView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMASR_DT_RCOV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcACTV_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBRT_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCRCL_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCNSCS_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSKN_COLR_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTOT_PNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmssView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsgView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcovView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdEmss
            // 
            this.grdEmss.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdEmss.Location = new System.Drawing.Point(348, 327);
            this.grdEmss.MainView = this.grdEmssView;
            this.grdEmss.Name = "grdEmss";
            this.grdEmss.Size = new System.Drawing.Size(218, 193);
            this.grdEmss.TabIndex = 34;
            this.grdEmss.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdEmssView});
            // 
            // grdEmssView
            // 
            this.grdEmssView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEXEC_DT_EMSS,
            this.gcASM_RST_TXT_EMSS});
            this.grdEmssView.GridControl = this.grdEmss;
            this.grdEmssView.Name = "grdEmssView";
            this.grdEmssView.OptionsBehavior.ReadOnly = true;
            this.grdEmssView.OptionsCustomization.AllowSort = false;
            this.grdEmssView.OptionsView.ColumnAutoWidth = false;
            this.grdEmssView.OptionsView.ShowGroupPanel = false;
            this.grdEmssView.OptionsView.ShowViewCaption = true;
            this.grdEmssView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdEmssView.ViewCaption = "오심구토평가";
            // 
            // gcEXEC_DT_EMSS
            // 
            this.gcEXEC_DT_EMSS.Caption = "실시일시";
            this.gcEXEC_DT_EMSS.FieldName = "EXEC_DT";
            this.gcEXEC_DT_EMSS.Name = "gcEXEC_DT_EMSS";
            this.gcEXEC_DT_EMSS.Visible = true;
            this.gcEXEC_DT_EMSS.VisibleIndex = 0;
            this.gcEXEC_DT_EMSS.Width = 95;
            // 
            // gcASM_RST_TXT_EMSS
            // 
            this.gcASM_RST_TXT_EMSS.Caption = "결과";
            this.gcASM_RST_TXT_EMSS.FieldName = "ASM_RST_TXT";
            this.gcASM_RST_TXT_EMSS.Name = "gcASM_RST_TXT_EMSS";
            this.gcASM_RST_TXT_EMSS.Visible = true;
            this.gcASM_RST_TXT_EMSS.VisibleIndex = 1;
            // 
            // grdPain
            // 
            this.grdPain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdPain.Location = new System.Drawing.Point(12, 327);
            this.grdPain.MainView = this.grdPainView;
            this.grdPain.Name = "grdPain";
            this.grdPain.Size = new System.Drawing.Size(330, 193);
            this.grdPain.TabIndex = 33;
            this.grdPain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPainView});
            // 
            // grdPainView
            // 
            this.grdPainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEXEC_DT_PAIN,
            this.gcPAIN_ASM_TL_CD_PAIN,
            this.gcASM_TL_ETC_TXT_PAIN,
            this.gcASM_RST_TXT_PAIN});
            this.grdPainView.GridControl = this.grdPain;
            this.grdPainView.Name = "grdPainView";
            this.grdPainView.OptionsBehavior.ReadOnly = true;
            this.grdPainView.OptionsCustomization.AllowSort = false;
            this.grdPainView.OptionsView.ColumnAutoWidth = false;
            this.grdPainView.OptionsView.ShowGroupPanel = false;
            this.grdPainView.OptionsView.ShowViewCaption = true;
            this.grdPainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdPainView.ViewCaption = "통증평가";
            // 
            // gcEXEC_DT_PAIN
            // 
            this.gcEXEC_DT_PAIN.Caption = "평가일시";
            this.gcEXEC_DT_PAIN.FieldName = "EXEC_DT";
            this.gcEXEC_DT_PAIN.Name = "gcEXEC_DT_PAIN";
            this.gcEXEC_DT_PAIN.Visible = true;
            this.gcEXEC_DT_PAIN.VisibleIndex = 0;
            this.gcEXEC_DT_PAIN.Width = 95;
            // 
            // gcPAIN_ASM_TL_CD_PAIN
            // 
            this.gcPAIN_ASM_TL_CD_PAIN.Caption = "도구";
            this.gcPAIN_ASM_TL_CD_PAIN.FieldName = "PAIN_ASM_TL_CD";
            this.gcPAIN_ASM_TL_CD_PAIN.Name = "gcPAIN_ASM_TL_CD_PAIN";
            this.gcPAIN_ASM_TL_CD_PAIN.Visible = true;
            this.gcPAIN_ASM_TL_CD_PAIN.VisibleIndex = 1;
            this.gcPAIN_ASM_TL_CD_PAIN.Width = 35;
            // 
            // gcASM_TL_ETC_TXT_PAIN
            // 
            this.gcASM_TL_ETC_TXT_PAIN.Caption = "도구상세";
            this.gcASM_TL_ETC_TXT_PAIN.FieldName = "ASM_TL_ETC_TXT";
            this.gcASM_TL_ETC_TXT_PAIN.Name = "gcASM_TL_ETC_TXT_PAIN";
            this.gcASM_TL_ETC_TXT_PAIN.Visible = true;
            this.gcASM_TL_ETC_TXT_PAIN.VisibleIndex = 2;
            // 
            // gcASM_RST_TXT_PAIN
            // 
            this.gcASM_RST_TXT_PAIN.Caption = "결과";
            this.gcASM_RST_TXT_PAIN.FieldName = "ASM_RST_TXT";
            this.gcASM_RST_TXT_PAIN.Name = "gcASM_RST_TXT_PAIN";
            this.gcASM_RST_TXT_PAIN.Visible = true;
            this.gcASM_RST_TXT_PAIN.VisibleIndex = 3;
            // 
            // grdVtsg
            // 
            this.grdVtsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVtsg.Location = new System.Drawing.Point(454, 41);
            this.grdVtsg.MainView = this.grdVtsgView;
            this.grdVtsg.Name = "grdVtsg";
            this.grdVtsg.Size = new System.Drawing.Size(418, 280);
            this.grdVtsg.TabIndex = 31;
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
            this.gcTMPR,
            this.gcOXY_STRT,
            this.gcVTSG_TXT});
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
            this.gcBPRSU.Width = 65;
            // 
            // gcPULS
            // 
            this.gcPULS.Caption = "맥박";
            this.gcPULS.FieldName = "PULS";
            this.gcPULS.Name = "gcPULS";
            this.gcPULS.Visible = true;
            this.gcPULS.VisibleIndex = 2;
            this.gcPULS.Width = 45;
            // 
            // gcBRT
            // 
            this.gcBRT.Caption = "호흡";
            this.gcBRT.FieldName = "BRT";
            this.gcBRT.Name = "gcBRT";
            this.gcBRT.Visible = true;
            this.gcBRT.VisibleIndex = 3;
            this.gcBRT.Width = 45;
            // 
            // gcTMPR
            // 
            this.gcTMPR.Caption = "체온";
            this.gcTMPR.FieldName = "TMPR";
            this.gcTMPR.Name = "gcTMPR";
            this.gcTMPR.Visible = true;
            this.gcTMPR.VisibleIndex = 4;
            this.gcTMPR.Width = 45;
            // 
            // gcOXY_STRT
            // 
            this.gcOXY_STRT.Caption = "산소포화도";
            this.gcOXY_STRT.FieldName = "OXY_STRT";
            this.gcOXY_STRT.Name = "gcOXY_STRT";
            this.gcOXY_STRT.Visible = true;
            this.gcOXY_STRT.VisibleIndex = 5;
            // 
            // gcVTSG_TXT
            // 
            this.gcVTSG_TXT.Caption = "특이사항";
            this.gcVTSG_TXT.FieldName = "VTSG_TXT";
            this.gcVTSG_TXT.Name = "gcVTSG_TXT";
            this.gcVTSG_TXT.Visible = true;
            this.gcVTSG_TXT.VisibleIndex = 6;
            this.gcVTSG_TXT.Width = 200;
            // 
            // grdInfo
            // 
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(436, 280);
            this.grdInfo.TabIndex = 30;
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
            this.btnSubmit.TabIndex = 29;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 28;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdRcov
            // 
            this.grdRcov.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRcov.Location = new System.Drawing.Point(572, 327);
            this.grdRcov.MainView = this.grdRcovView;
            this.grdRcov.Name = "grdRcov";
            this.grdRcov.Size = new System.Drawing.Size(300, 193);
            this.grdRcov.TabIndex = 35;
            this.grdRcov.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdRcovView});
            // 
            // grdRcovView
            // 
            this.grdRcovView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMASR_DT_RCOV,
            this.gcACTV_PNT,
            this.gcBRT_PNT,
            this.gcCRCL_PNT,
            this.gcCNSCS_PNT,
            this.gcSKN_COLR_PNT,
            this.gcTOT_PNT});
            this.grdRcovView.GridControl = this.grdRcov;
            this.grdRcovView.Name = "grdRcovView";
            this.grdRcovView.OptionsBehavior.ReadOnly = true;
            this.grdRcovView.OptionsCustomization.AllowSort = false;
            this.grdRcovView.OptionsView.ColumnAutoWidth = false;
            this.grdRcovView.OptionsView.ShowGroupPanel = false;
            this.grdRcovView.OptionsView.ShowViewCaption = true;
            this.grdRcovView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdRcovView.ViewCaption = "마취회복점수";
            // 
            // gcMASR_DT_RCOV
            // 
            this.gcMASR_DT_RCOV.Caption = "축정일시";
            this.gcMASR_DT_RCOV.FieldName = "MASR_DT";
            this.gcMASR_DT_RCOV.Name = "gcMASR_DT_RCOV";
            this.gcMASR_DT_RCOV.Visible = true;
            this.gcMASR_DT_RCOV.VisibleIndex = 0;
            // 
            // gcACTV_PNT
            // 
            this.gcACTV_PNT.Caption = "활동성";
            this.gcACTV_PNT.FieldName = "ACTV_PNT";
            this.gcACTV_PNT.Name = "gcACTV_PNT";
            this.gcACTV_PNT.Visible = true;
            this.gcACTV_PNT.VisibleIndex = 1;
            // 
            // gcBRT_PNT
            // 
            this.gcBRT_PNT.Caption = "호흡";
            this.gcBRT_PNT.FieldName = "BRT_PNT";
            this.gcBRT_PNT.Name = "gcBRT_PNT";
            this.gcBRT_PNT.Visible = true;
            this.gcBRT_PNT.VisibleIndex = 2;
            // 
            // gcCRCL_PNT
            // 
            this.gcCRCL_PNT.Caption = "순환";
            this.gcCRCL_PNT.FieldName = "CRCL_PNT";
            this.gcCRCL_PNT.Name = "gcCRCL_PNT";
            this.gcCRCL_PNT.Visible = true;
            this.gcCRCL_PNT.VisibleIndex = 3;
            // 
            // gcCNSCS_PNT
            // 
            this.gcCNSCS_PNT.Caption = "의식";
            this.gcCNSCS_PNT.FieldName = "CNSCS_PNT";
            this.gcCNSCS_PNT.Name = "gcCNSCS_PNT";
            this.gcCNSCS_PNT.Visible = true;
            this.gcCNSCS_PNT.VisibleIndex = 4;
            // 
            // gcSKN_COLR_PNT
            // 
            this.gcSKN_COLR_PNT.Caption = "피부색";
            this.gcSKN_COLR_PNT.FieldName = "SKN_COLR_PNT";
            this.gcSKN_COLR_PNT.Name = "gcSKN_COLR_PNT";
            this.gcSKN_COLR_PNT.Visible = true;
            this.gcSKN_COLR_PNT.VisibleIndex = 5;
            // 
            // gcTOT_PNT
            // 
            this.gcTOT_PNT.Caption = "합계";
            this.gcTOT_PNT.FieldName = "TOT_PNT";
            this.gcTOT_PNT.Name = "gcTOT_PNT";
            this.gcTOT_PNT.Visible = true;
            this.gcTOT_PNT.VisibleIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(739, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "회복기록자료(RAR001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 37;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RAR001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdRcov);
            this.Controls.Add(this.grdEmss);
            this.Controls.Add(this.grdPain);
            this.Controls.Add(this.grdVtsg);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RAR001";
            this.Text = "회복기록자료(RAR001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdEmss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmssView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVtsgView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcovView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdEmss;
        private DevExpress.XtraGrid.Views.Grid.GridView grdEmssView;
        private DevExpress.XtraGrid.GridControl grdPain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPainView;
        private DevExpress.XtraGrid.GridControl grdVtsg;
        private DevExpress.XtraGrid.Views.Grid.GridView grdVtsgView;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBPRSU;
        private DevExpress.XtraGrid.Columns.GridColumn gcPULS;
        private DevExpress.XtraGrid.Columns.GridColumn gcBRT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTMPR;
        private DevExpress.XtraGrid.Columns.GridColumn gcOXY_STRT;
        private DevExpress.XtraGrid.Columns.GridColumn gcVTSG_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXEC_DT_PAIN;
        private DevExpress.XtraGrid.Columns.GridColumn gcPAIN_ASM_TL_CD_PAIN;
        private DevExpress.XtraGrid.Columns.GridColumn gcASM_TL_ETC_TXT_PAIN;
        private DevExpress.XtraGrid.Columns.GridColumn gcASM_RST_TXT_PAIN;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXEC_DT_EMSS;
        private DevExpress.XtraGrid.Columns.GridColumn gcASM_RST_TXT_EMSS;
        private DevExpress.XtraGrid.GridControl grdRcov;
        private DevExpress.XtraGrid.Views.Grid.GridView grdRcovView;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_DT_RCOV;
        private DevExpress.XtraGrid.Columns.GridColumn gcACTV_PNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBRT_PNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCRCL_PNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCNSCS_PNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSKN_COLR_PNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcTOT_PNT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
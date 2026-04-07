namespace ADD7003E
{
    partial class RWI001
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
            this.grdDscg = new DevExpress.XtraGrid.GridControl();
            this.grdDscgView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCHRG_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRTP_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSPRM_IPAT_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSPRM_IPAT_PTH_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIPAT_PTH_ETC_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSPRM_IPAT_RS_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRE_IPAT_RS_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIPAT_RS_ETC_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSPRM_DSCG_RST_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDSCG_RST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEATH_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEATH_SICK_SYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEATH_DIAG_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSPRM_DSCG_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDscg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDscgView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(174, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 12;
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
            this.grdInfo.TabIndex = 14;
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
            this.grdDiag.TabIndex = 15;
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
            this.grdDiagView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
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
            // grdDscg
            // 
            this.grdDscg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDscg.Location = new System.Drawing.Point(12, 257);
            this.grdDscg.MainView = this.grdDscgView;
            this.grdDscg.Name = "grdDscg";
            this.grdDscg.Size = new System.Drawing.Size(860, 263);
            this.grdDscg.TabIndex = 16;
            this.grdDscg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDscgView});
            // 
            // grdDscgView
            // 
            this.grdDscgView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCHRG_DR_NM,
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcWRTP_NM,
            this.gcSPRM_IPAT_DT,
            this.gcSPRM_IPAT_PTH_CD,
            this.gcIPAT_PTH_ETC_TXT,
            this.gcSPRM_IPAT_RS_CD,
            this.gcRE_IPAT_RS_TXT,
            this.gcIPAT_RS_ETC_TXT,
            this.gcSPRM_DSCG_RST_CD,
            this.gcDSCG_RST_TXT,
            this.gcDEATH_DT,
            this.gcDEATH_SICK_SYM,
            this.gcDEATH_DIAG_NM,
            this.gcSPRM_DSCG_DT});
            this.grdDscgView.GridControl = this.grdDscg;
            this.grdDscgView.Name = "grdDscgView";
            this.grdDscgView.OptionsBehavior.ReadOnly = true;
            this.grdDscgView.OptionsCustomization.AllowSort = false;
            this.grdDscgView.OptionsView.ColumnAutoWidth = false;
            this.grdDscgView.OptionsView.ShowGroupPanel = false;
            this.grdDscgView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcCHRG_DR_NM
            // 
            this.gcCHRG_DR_NM.Caption = "담당의";
            this.gcCHRG_DR_NM.FieldName = "CHRG_DR_NM";
            this.gcCHRG_DR_NM.Name = "gcCHRG_DR_NM";
            this.gcCHRG_DR_NM.Visible = true;
            this.gcCHRG_DR_NM.VisibleIndex = 0;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "진료과";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 1;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 2;
            // 
            // gcWRTP_NM
            // 
            this.gcWRTP_NM.Caption = "작성자";
            this.gcWRTP_NM.FieldName = "WRTP_NM";
            this.gcWRTP_NM.Name = "gcWRTP_NM";
            this.gcWRTP_NM.Visible = true;
            this.gcWRTP_NM.VisibleIndex = 3;
            // 
            // gcSPRM_IPAT_DT
            // 
            this.gcSPRM_IPAT_DT.Caption = "입실일시";
            this.gcSPRM_IPAT_DT.FieldName = "SPRM_IPAT_DT";
            this.gcSPRM_IPAT_DT.Name = "gcSPRM_IPAT_DT";
            this.gcSPRM_IPAT_DT.Visible = true;
            this.gcSPRM_IPAT_DT.VisibleIndex = 4;
            // 
            // gcSPRM_IPAT_PTH_CD
            // 
            this.gcSPRM_IPAT_PTH_CD.Caption = "입실경로";
            this.gcSPRM_IPAT_PTH_CD.FieldName = "SPRM_IPAT_PTH_CD";
            this.gcSPRM_IPAT_PTH_CD.Name = "gcSPRM_IPAT_PTH_CD";
            this.gcSPRM_IPAT_PTH_CD.Visible = true;
            this.gcSPRM_IPAT_PTH_CD.VisibleIndex = 5;
            // 
            // gcIPAT_PTH_ETC_TXT
            // 
            this.gcIPAT_PTH_ETC_TXT.Caption = "입실상세";
            this.gcIPAT_PTH_ETC_TXT.FieldName = "IPAT_PTH_ETC_TXT";
            this.gcIPAT_PTH_ETC_TXT.Name = "gcIPAT_PTH_ETC_TXT";
            this.gcIPAT_PTH_ETC_TXT.Visible = true;
            this.gcIPAT_PTH_ETC_TXT.VisibleIndex = 6;
            // 
            // gcSPRM_IPAT_RS_CD
            // 
            this.gcSPRM_IPAT_RS_CD.Caption = "입실사유";
            this.gcSPRM_IPAT_RS_CD.FieldName = "SPRM_IPAT_RS_CD";
            this.gcSPRM_IPAT_RS_CD.Name = "gcSPRM_IPAT_RS_CD";
            this.gcSPRM_IPAT_RS_CD.Visible = true;
            this.gcSPRM_IPAT_RS_CD.VisibleIndex = 7;
            // 
            // gcRE_IPAT_RS_TXT
            // 
            this.gcRE_IPAT_RS_TXT.Caption = "재입실사유";
            this.gcRE_IPAT_RS_TXT.FieldName = "RE_IPAT_RS_TXT";
            this.gcRE_IPAT_RS_TXT.Name = "gcRE_IPAT_RS_TXT";
            this.gcRE_IPAT_RS_TXT.Visible = true;
            this.gcRE_IPAT_RS_TXT.VisibleIndex = 8;
            // 
            // gcIPAT_RS_ETC_TXT
            // 
            this.gcIPAT_RS_ETC_TXT.Caption = "입실사유상세";
            this.gcIPAT_RS_ETC_TXT.FieldName = "IPAT_RS_ETC_TXT";
            this.gcIPAT_RS_ETC_TXT.Name = "gcIPAT_RS_ETC_TXT";
            this.gcIPAT_RS_ETC_TXT.Visible = true;
            this.gcIPAT_RS_ETC_TXT.VisibleIndex = 9;
            // 
            // gcSPRM_DSCG_RST_CD
            // 
            this.gcSPRM_DSCG_RST_CD.Caption = "퇴실상태";
            this.gcSPRM_DSCG_RST_CD.FieldName = "SPRM_DSCG_RST_CD";
            this.gcSPRM_DSCG_RST_CD.Name = "gcSPRM_DSCG_RST_CD";
            this.gcSPRM_DSCG_RST_CD.Visible = true;
            this.gcSPRM_DSCG_RST_CD.VisibleIndex = 10;
            // 
            // gcDSCG_RST_TXT
            // 
            this.gcDSCG_RST_TXT.Caption = "퇴실상세";
            this.gcDSCG_RST_TXT.FieldName = "DSCG_RST_TXT";
            this.gcDSCG_RST_TXT.Name = "gcDSCG_RST_TXT";
            this.gcDSCG_RST_TXT.Visible = true;
            this.gcDSCG_RST_TXT.VisibleIndex = 11;
            // 
            // gcDEATH_DT
            // 
            this.gcDEATH_DT.Caption = "사망일시";
            this.gcDEATH_DT.FieldName = "DEATH_DT";
            this.gcDEATH_DT.Name = "gcDEATH_DT";
            this.gcDEATH_DT.Visible = true;
            this.gcDEATH_DT.VisibleIndex = 12;
            // 
            // gcDEATH_SICK_SYM
            // 
            this.gcDEATH_SICK_SYM.Caption = "원사인";
            this.gcDEATH_SICK_SYM.FieldName = "DEATH_SICK_SYM";
            this.gcDEATH_SICK_SYM.Name = "gcDEATH_SICK_SYM";
            this.gcDEATH_SICK_SYM.Visible = true;
            this.gcDEATH_SICK_SYM.VisibleIndex = 13;
            // 
            // gcDEATH_DIAG_NM
            // 
            this.gcDEATH_DIAG_NM.Caption = "사망진단";
            this.gcDEATH_DIAG_NM.FieldName = "DEATH_DIAG_NM";
            this.gcDEATH_DIAG_NM.Name = "gcDEATH_DIAG_NM";
            this.gcDEATH_DIAG_NM.Visible = true;
            this.gcDEATH_DIAG_NM.VisibleIndex = 14;
            // 
            // gcSPRM_DSCG_DT
            // 
            this.gcSPRM_DSCG_DT.Caption = "퇴실일시";
            this.gcSPRM_DSCG_DT.FieldName = "SPRM_DSCG_DT";
            this.gcSPRM_DSCG_DT.Name = "gcSPRM_DSCG_DT";
            this.gcSPRM_DSCG_DT.Visible = true;
            this.gcSPRM_DSCG_DT.VisibleIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(719, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "중환자실기록자료(RWI001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 47;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RWI001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdDscg);
            this.Controls.Add(this.grdDiag);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RWI001";
            this.Text = "중환자실기록자료(RWI001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDscg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDscgView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private DevExpress.XtraGrid.GridControl grdDiag;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_TP_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_SICK_SYM;
        private DevExpress.XtraGrid.GridControl grdDscg;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDscgView;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHRG_DR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRTP_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSPRM_IPAT_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSPRM_IPAT_PTH_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIPAT_PTH_ETC_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSPRM_IPAT_RS_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcRE_IPAT_RS_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSPRM_DSCG_RST_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDSCG_RST_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEATH_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEATH_SICK_SYM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEATH_DIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSPRM_DSCG_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcIPAT_RS_ETC_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
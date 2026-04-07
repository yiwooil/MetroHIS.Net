namespace ADD7003E
{
    partial class RNE001
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
            this.grdRcd = new DevExpress.XtraGrid.GridControl();
            this.grdRcdView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRCD_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRCD_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNURSE_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRCD_TXT_ORG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdStat = new DevExpress.XtraGrid.GridControl();
            this.grdStatView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMASR_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMASR_TL_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMASR_RST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStatView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRcd
            // 
            this.grdRcd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRcd.Location = new System.Drawing.Point(448, 194);
            this.grdRcd.MainView = this.grdRcdView;
            this.grdRcd.Name = "grdRcd";
            this.grdRcd.Size = new System.Drawing.Size(423, 326);
            this.grdRcd.TabIndex = 34;
            this.grdRcd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdRcdView});
            // 
            // grdRcdView
            // 
            this.grdRcdView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcRCD_DT,
            this.gcRCD_TXT,
            this.gcNURSE_NM,
            this.gcRCD_TXT_ORG});
            this.grdRcdView.GridControl = this.grdRcd;
            this.grdRcdView.Name = "grdRcdView";
            this.grdRcdView.OptionsBehavior.ReadOnly = true;
            this.grdRcdView.OptionsCustomization.AllowSort = false;
            this.grdRcdView.OptionsView.ColumnAutoWidth = false;
            this.grdRcdView.OptionsView.ShowGroupPanel = false;
            this.grdRcdView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcRCD_DT
            // 
            this.gcRCD_DT.Caption = "기록일시";
            this.gcRCD_DT.FieldName = "RCD_DT";
            this.gcRCD_DT.Name = "gcRCD_DT";
            this.gcRCD_DT.Visible = true;
            this.gcRCD_DT.VisibleIndex = 0;
            this.gcRCD_DT.Width = 95;
            // 
            // gcRCD_TXT
            // 
            this.gcRCD_TXT.Caption = "긴호기록";
            this.gcRCD_TXT.FieldName = "RCD_TXT";
            this.gcRCD_TXT.Name = "gcRCD_TXT";
            this.gcRCD_TXT.Visible = true;
            this.gcRCD_TXT.VisibleIndex = 1;
            this.gcRCD_TXT.Width = 400;
            // 
            // gcNURSE_NM
            // 
            this.gcNURSE_NM.Caption = "간호사";
            this.gcNURSE_NM.FieldName = "NURSE_NM";
            this.gcNURSE_NM.Name = "gcNURSE_NM";
            this.gcNURSE_NM.Visible = true;
            this.gcNURSE_NM.VisibleIndex = 2;
            // 
            // gcRCD_TXT_ORG
            // 
            this.gcRCD_TXT_ORG.Caption = "간호기록원문";
            this.gcRCD_TXT_ORG.FieldName = "RCD_TXT_ORG";
            this.gcRCD_TXT_ORG.Name = "gcRCD_TXT_ORG";
            // 
            // grdInfo
            // 
            this.grdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(430, 479);
            this.grdInfo.TabIndex = 33;
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
            this.btnSubmit.TabIndex = 32;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 31;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdStat
            // 
            this.grdStat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdStat.Location = new System.Drawing.Point(449, 41);
            this.grdStat.MainView = this.grdStatView;
            this.grdStat.Name = "grdStat";
            this.grdStat.Size = new System.Drawing.Size(423, 147);
            this.grdStat.TabIndex = 35;
            this.grdStat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdStatView});
            // 
            // grdStatView
            // 
            this.grdStatView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMASR_DT,
            this.gcMASR_TL_NM,
            this.gcMASR_RST_TXT});
            this.grdStatView.GridControl = this.grdStat;
            this.grdStatView.Name = "grdStatView";
            this.grdStatView.OptionsBehavior.ReadOnly = true;
            this.grdStatView.OptionsCustomization.AllowSort = false;
            this.grdStatView.OptionsView.ColumnAutoWidth = false;
            this.grdStatView.OptionsView.ShowGroupPanel = false;
            this.grdStatView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
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
            // gcMASR_TL_NM
            // 
            this.gcMASR_TL_NM.Caption = "도구";
            this.gcMASR_TL_NM.FieldName = "MASR_TL_NM";
            this.gcMASR_TL_NM.Name = "gcMASR_TL_NM";
            this.gcMASR_TL_NM.Visible = true;
            this.gcMASR_TL_NM.VisibleIndex = 1;
            // 
            // gcMASR_RST_TXT
            // 
            this.gcMASR_RST_TXT.Caption = "결과";
            this.gcMASR_RST_TXT.FieldName = "MASR_RST_TXT";
            this.gcMASR_RST_TXT.Name = "gcMASR_RST_TXT";
            this.gcMASR_RST_TXT.Visible = true;
            this.gcMASR_RST_TXT.VisibleIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(719, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "응급간호기록자료(RNE001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 45;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RNE001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdStat);
            this.Controls.Add(this.grdRcd);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RNE001";
            this.Text = "응급간호기록자료(RNE001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdRcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStatView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdRcd;
        private DevExpress.XtraGrid.Views.Grid.GridView grdRcdView;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcRCD_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcRCD_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcNURSE_NM;
        private DevExpress.XtraGrid.GridControl grdStat;
        private DevExpress.XtraGrid.Views.Grid.GridView grdStatView;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_TL_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcMASR_RST_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
        private DevExpress.XtraGrid.Columns.GridColumn gcRCD_TXT_ORG;
    }
}
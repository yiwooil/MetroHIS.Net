namespace ADD7003E
{
    partial class RDD001
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
            this.gcPRSC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRSC_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRMK_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRSC_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRcd
            // 
            this.grdRcd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRcd.Location = new System.Drawing.Point(12, 179);
            this.grdRcd.MainView = this.grdRcdView;
            this.grdRcd.Name = "grdRcd";
            this.grdRcd.Size = new System.Drawing.Size(860, 341);
            this.grdRcd.TabIndex = 21;
            this.grdRcd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdRcdView});
            // 
            // grdRcdView
            // 
            this.grdRcdView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPRSC_DT,
            this.gcPRSC_TXT,
            this.gcRMK_TXT,
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcPRSC_DR_NM});
            this.grdRcdView.GridControl = this.grdRcd;
            this.grdRcdView.Name = "grdRcdView";
            this.grdRcdView.OptionsBehavior.ReadOnly = true;
            this.grdRcdView.OptionsCustomization.AllowSort = false;
            this.grdRcdView.OptionsView.ColumnAutoWidth = false;
            this.grdRcdView.OptionsView.ShowGroupPanel = false;
            this.grdRcdView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcPRSC_DT
            // 
            this.gcPRSC_DT.Caption = "처방일시";
            this.gcPRSC_DT.FieldName = "PRSC_DT";
            this.gcPRSC_DT.Name = "gcPRSC_DT";
            this.gcPRSC_DT.Visible = true;
            this.gcPRSC_DT.VisibleIndex = 0;
            this.gcPRSC_DT.Width = 95;
            // 
            // gcPRSC_TXT
            // 
            this.gcPRSC_TXT.Caption = "처방내역";
            this.gcPRSC_TXT.FieldName = "PRSC_TXT";
            this.gcPRSC_TXT.Name = "gcPRSC_TXT";
            this.gcPRSC_TXT.Visible = true;
            this.gcPRSC_TXT.VisibleIndex = 1;
            this.gcPRSC_TXT.Width = 600;
            // 
            // gcRMK_TXT
            // 
            this.gcRMK_TXT.Caption = "비고";
            this.gcRMK_TXT.FieldName = "RMK_TXT";
            this.gcRMK_TXT.Name = "gcRMK_TXT";
            this.gcRMK_TXT.Visible = true;
            this.gcRMK_TXT.VisibleIndex = 2;
            this.gcRMK_TXT.Width = 200;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "진료과목";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 3;
            this.gcDGSBJT_CD.Width = 55;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 4;
            this.gcIFLD_DTL_SPC_SBJT_CD.Width = 55;
            // 
            // gcPRSC_DR_NM
            // 
            this.gcPRSC_DR_NM.Caption = "처방의";
            this.gcPRSC_DR_NM.FieldName = "PRSC_DR_NM";
            this.gcPRSC_DR_NM.Name = "gcPRSC_DR_NM";
            this.gcPRSC_DR_NM.Visible = true;
            this.gcPRSC_DR_NM.VisibleIndex = 5;
            this.gcPRSC_DR_NM.Width = 55;
            // 
            // grdInfo
            // 
            this.grdInfo.Location = new System.Drawing.Point(12, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(431, 132);
            this.grdInfo.TabIndex = 19;
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
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(717, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "의사지시기록자료(RDD001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 39;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RDD001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdRcd);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RDD001";
            this.Text = "의사지시기록(RDD001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdRcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRcdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcRMK_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DR_NM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
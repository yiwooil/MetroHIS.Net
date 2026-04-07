namespace ADD7003E
{
    partial class RMM001
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
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPRSC_DD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRSC_DIV_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINJC_PTH_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFQ1_MDCT_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMDS_UNIT_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDY1_INJC_FQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTOT_INJC_DDCNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINJC_EXEC_CD_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNEXEC_RS_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXEC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRMK_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINJC_EXEC_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 40);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(860, 413);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView,
            this.gridView2});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPRSC_DD,
            this.gcPRSC_DIV_CD,
            this.gcMDS_CD,
            this.gcMDS_NM,
            this.gcINJC_PTH_TXT,
            this.gcFQ1_MDCT_QTY,
            this.gcMDS_UNIT_TXT,
            this.gcDY1_INJC_FQ,
            this.gcTOT_INJC_DDCNT,
            this.gcINJC_EXEC_CD_NM,
            this.gcNEXEC_RS_TXT,
            this.gcEXEC_DT,
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcRMK_TXT,
            this.gcINJC_EXEC_CD});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcPRSC_DD
            // 
            this.gcPRSC_DD.Caption = "처방일자";
            this.gcPRSC_DD.FieldName = "PRSC_DD";
            this.gcPRSC_DD.Name = "gcPRSC_DD";
            this.gcPRSC_DD.OptionsColumn.AllowEdit = false;
            this.gcPRSC_DD.OptionsColumn.ReadOnly = true;
            this.gcPRSC_DD.Visible = true;
            this.gcPRSC_DD.VisibleIndex = 0;
            // 
            // gcPRSC_DIV_CD
            // 
            this.gcPRSC_DIV_CD.Caption = "처방분류";
            this.gcPRSC_DIV_CD.FieldName = "PRSC_DIV_CD";
            this.gcPRSC_DIV_CD.Name = "gcPRSC_DIV_CD";
            this.gcPRSC_DIV_CD.OptionsColumn.AllowEdit = false;
            this.gcPRSC_DIV_CD.OptionsColumn.ReadOnly = true;
            this.gcPRSC_DIV_CD.Visible = true;
            this.gcPRSC_DIV_CD.VisibleIndex = 1;
            this.gcPRSC_DIV_CD.Width = 55;
            // 
            // gcMDS_CD
            // 
            this.gcMDS_CD.Caption = "약품코드";
            this.gcMDS_CD.FieldName = "MDS_CD";
            this.gcMDS_CD.Name = "gcMDS_CD";
            this.gcMDS_CD.OptionsColumn.AllowEdit = false;
            this.gcMDS_CD.OptionsColumn.ReadOnly = true;
            this.gcMDS_CD.Visible = true;
            this.gcMDS_CD.VisibleIndex = 2;
            // 
            // gcMDS_NM
            // 
            this.gcMDS_NM.Caption = "약품명";
            this.gcMDS_NM.FieldName = "MDS_NM";
            this.gcMDS_NM.Name = "gcMDS_NM";
            this.gcMDS_NM.OptionsColumn.AllowEdit = false;
            this.gcMDS_NM.OptionsColumn.ReadOnly = true;
            this.gcMDS_NM.Visible = true;
            this.gcMDS_NM.VisibleIndex = 3;
            this.gcMDS_NM.Width = 150;
            // 
            // gcINJC_PTH_TXT
            // 
            this.gcINJC_PTH_TXT.Caption = "투여경로";
            this.gcINJC_PTH_TXT.FieldName = "INJC_PTH_TXT";
            this.gcINJC_PTH_TXT.Name = "gcINJC_PTH_TXT";
            this.gcINJC_PTH_TXT.OptionsColumn.AllowEdit = false;
            this.gcINJC_PTH_TXT.OptionsColumn.ReadOnly = true;
            this.gcINJC_PTH_TXT.Visible = true;
            this.gcINJC_PTH_TXT.VisibleIndex = 4;
            this.gcINJC_PTH_TXT.Width = 55;
            // 
            // gcFQ1_MDCT_QTY
            // 
            this.gcFQ1_MDCT_QTY.Caption = "1회투약량";
            this.gcFQ1_MDCT_QTY.FieldName = "FQ1_MDCT_QTY";
            this.gcFQ1_MDCT_QTY.Name = "gcFQ1_MDCT_QTY";
            this.gcFQ1_MDCT_QTY.OptionsColumn.AllowEdit = false;
            this.gcFQ1_MDCT_QTY.OptionsColumn.ReadOnly = true;
            this.gcFQ1_MDCT_QTY.Visible = true;
            this.gcFQ1_MDCT_QTY.VisibleIndex = 5;
            // 
            // gcMDS_UNIT_TXT
            // 
            this.gcMDS_UNIT_TXT.Caption = "단위";
            this.gcMDS_UNIT_TXT.FieldName = "MDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.Name = "gcMDS_UNIT_TXT";
            this.gcMDS_UNIT_TXT.OptionsColumn.AllowEdit = false;
            this.gcMDS_UNIT_TXT.OptionsColumn.ReadOnly = true;
            this.gcMDS_UNIT_TXT.Visible = true;
            this.gcMDS_UNIT_TXT.VisibleIndex = 6;
            this.gcMDS_UNIT_TXT.Width = 45;
            // 
            // gcDY1_INJC_FQ
            // 
            this.gcDY1_INJC_FQ.Caption = "1일투여횟수";
            this.gcDY1_INJC_FQ.FieldName = "DY1_INJC_FQ";
            this.gcDY1_INJC_FQ.Name = "gcDY1_INJC_FQ";
            this.gcDY1_INJC_FQ.OptionsColumn.AllowEdit = false;
            this.gcDY1_INJC_FQ.OptionsColumn.ReadOnly = true;
            this.gcDY1_INJC_FQ.Visible = true;
            this.gcDY1_INJC_FQ.VisibleIndex = 7;
            // 
            // gcTOT_INJC_DDCNT
            // 
            this.gcTOT_INJC_DDCNT.Caption = "총투약일수";
            this.gcTOT_INJC_DDCNT.FieldName = "TOT_INJC_DDCNT";
            this.gcTOT_INJC_DDCNT.Name = "gcTOT_INJC_DDCNT";
            this.gcTOT_INJC_DDCNT.OptionsColumn.AllowEdit = false;
            this.gcTOT_INJC_DDCNT.OptionsColumn.ReadOnly = true;
            this.gcTOT_INJC_DDCNT.Visible = true;
            this.gcTOT_INJC_DDCNT.VisibleIndex = 8;
            // 
            // gcINJC_EXEC_CD_NM
            // 
            this.gcINJC_EXEC_CD_NM.Caption = "투여여부";
            this.gcINJC_EXEC_CD_NM.FieldName = "INJC_EXEC_CD_NM";
            this.gcINJC_EXEC_CD_NM.Name = "gcINJC_EXEC_CD_NM";
            this.gcINJC_EXEC_CD_NM.OptionsColumn.AllowEdit = false;
            this.gcINJC_EXEC_CD_NM.OptionsColumn.ReadOnly = true;
            this.gcINJC_EXEC_CD_NM.Visible = true;
            this.gcINJC_EXEC_CD_NM.VisibleIndex = 9;
            this.gcINJC_EXEC_CD_NM.Width = 55;
            // 
            // gcNEXEC_RS_TXT
            // 
            this.gcNEXEC_RS_TXT.Caption = "미실시사유";
            this.gcNEXEC_RS_TXT.FieldName = "NEXEC_RS_TXT";
            this.gcNEXEC_RS_TXT.Name = "gcNEXEC_RS_TXT";
            this.gcNEXEC_RS_TXT.OptionsColumn.AllowEdit = false;
            this.gcNEXEC_RS_TXT.OptionsColumn.ReadOnly = true;
            this.gcNEXEC_RS_TXT.Visible = true;
            this.gcNEXEC_RS_TXT.VisibleIndex = 10;
            // 
            // gcEXEC_DT
            // 
            this.gcEXEC_DT.Caption = "실시일시";
            this.gcEXEC_DT.FieldName = "EXEC_DT";
            this.gcEXEC_DT.Name = "gcEXEC_DT";
            this.gcEXEC_DT.OptionsColumn.AllowEdit = false;
            this.gcEXEC_DT.OptionsColumn.ReadOnly = true;
            this.gcEXEC_DT.Visible = true;
            this.gcEXEC_DT.VisibleIndex = 11;
            this.gcEXEC_DT.Width = 100;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "진료과목";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.OptionsColumn.AllowEdit = false;
            this.gcDGSBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 12;
            this.gcDGSBJT_CD.Width = 55;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.OptionsColumn.AllowEdit = false;
            this.gcIFLD_DTL_SPC_SBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 13;
            this.gcIFLD_DTL_SPC_SBJT_CD.Width = 55;
            // 
            // gcRMK_TXT
            // 
            this.gcRMK_TXT.Caption = "비고";
            this.gcRMK_TXT.FieldName = "RMK_TXT";
            this.gcRMK_TXT.Name = "gcRMK_TXT";
            this.gcRMK_TXT.OptionsColumn.AllowEdit = false;
            this.gcRMK_TXT.OptionsColumn.ReadOnly = true;
            this.gcRMK_TXT.Visible = true;
            this.gcRMK_TXT.VisibleIndex = 14;
            // 
            // gcINJC_EXEC_CD
            // 
            this.gcINJC_EXEC_CD.Caption = "투여여부코드";
            this.gcINJC_EXEC_CD.FieldName = "INJC_EXEC_CD";
            this.gcINJC_EXEC_CD.Name = "gcINJC_EXEC_CD";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdMain;
            this.gridView2.Name = "gridView2";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(11, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(173, 11);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(735, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "투약기록자료(RMM001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(92, 11);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 45;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RMM001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.ControlBox = false;
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RMM001";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "투약기록자료(RMM001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DIV_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcINJC_PTH_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcFQ1_MDCT_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcMDS_UNIT_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDY1_INJC_FQ;
        private DevExpress.XtraGrid.Columns.GridColumn gcTOT_INJC_DDCNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcINJC_EXEC_CD_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcNEXEC_RS_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXEC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcRMK_TXT;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gcINJC_EXEC_CD;
        private System.Windows.Forms.Button btnChcek;
    }
}
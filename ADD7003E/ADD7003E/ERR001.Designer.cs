namespace ADD7003E
{
    partial class ERR001
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
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRSC_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_PRSC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_EXEC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_RST_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDCT_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDCT_DR_LCS_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_MDFEE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_RST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            this.gcEXM_RST_TXT_ORG = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(174, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 41);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(860, 409);
            this.grdMain.TabIndex = 6;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcPRSC_DR_NM,
            this.gcEXM_PRSC_DT,
            this.gcEXM_EXEC_DT,
            this.gcEXM_RST_DT,
            this.gcDCT_DR_NM,
            this.gcDCT_DR_LCS_NO,
            this.gcEXM_MDFEE_CD,
            this.gcEXM_CD,
            this.gcEXM_NM,
            this.gcEXM_RST_TXT,
            this.gcEXM_RST_TXT_ORG});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "진료과";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.OptionsColumn.AllowEdit = false;
            this.gcDGSBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 0;
            this.gcDGSBJT_CD.Width = 45;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.OptionsColumn.AllowEdit = false;
            this.gcIFLD_DTL_SPC_SBJT_CD.OptionsColumn.ReadOnly = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 1;
            this.gcIFLD_DTL_SPC_SBJT_CD.Width = 55;
            // 
            // gcPRSC_DR_NM
            // 
            this.gcPRSC_DR_NM.Caption = "처방의";
            this.gcPRSC_DR_NM.FieldName = "PRSC_DR_NM";
            this.gcPRSC_DR_NM.Name = "gcPRSC_DR_NM";
            this.gcPRSC_DR_NM.OptionsColumn.AllowEdit = false;
            this.gcPRSC_DR_NM.OptionsColumn.ReadOnly = true;
            this.gcPRSC_DR_NM.Visible = true;
            this.gcPRSC_DR_NM.VisibleIndex = 2;
            this.gcPRSC_DR_NM.Width = 55;
            // 
            // gcEXM_PRSC_DT
            // 
            this.gcEXM_PRSC_DT.Caption = "처방일시";
            this.gcEXM_PRSC_DT.FieldName = "EXM_PRSC_DT";
            this.gcEXM_PRSC_DT.Name = "gcEXM_PRSC_DT";
            this.gcEXM_PRSC_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_PRSC_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_PRSC_DT.Visible = true;
            this.gcEXM_PRSC_DT.VisibleIndex = 3;
            this.gcEXM_PRSC_DT.Width = 95;
            // 
            // gcEXM_EXEC_DT
            // 
            this.gcEXM_EXEC_DT.Caption = "검사일시";
            this.gcEXM_EXEC_DT.FieldName = "EXM_EXEC_DT";
            this.gcEXM_EXEC_DT.Name = "gcEXM_EXEC_DT";
            this.gcEXM_EXEC_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_EXEC_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_EXEC_DT.Visible = true;
            this.gcEXM_EXEC_DT.VisibleIndex = 4;
            this.gcEXM_EXEC_DT.Width = 95;
            // 
            // gcEXM_RST_DT
            // 
            this.gcEXM_RST_DT.Caption = "판독일시";
            this.gcEXM_RST_DT.FieldName = "EXM_RST_DT";
            this.gcEXM_RST_DT.Name = "gcEXM_RST_DT";
            this.gcEXM_RST_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_RST_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_RST_DT.Visible = true;
            this.gcEXM_RST_DT.VisibleIndex = 5;
            this.gcEXM_RST_DT.Width = 95;
            // 
            // gcDCT_DR_NM
            // 
            this.gcDCT_DR_NM.Caption = "판독의";
            this.gcDCT_DR_NM.FieldName = "DCT_DR_NM";
            this.gcDCT_DR_NM.Name = "gcDCT_DR_NM";
            this.gcDCT_DR_NM.OptionsColumn.AllowEdit = false;
            this.gcDCT_DR_NM.OptionsColumn.ReadOnly = true;
            this.gcDCT_DR_NM.Visible = true;
            this.gcDCT_DR_NM.VisibleIndex = 6;
            this.gcDCT_DR_NM.Width = 55;
            // 
            // gcDCT_DR_LCS_NO
            // 
            this.gcDCT_DR_LCS_NO.Caption = "면허번호";
            this.gcDCT_DR_LCS_NO.FieldName = "DCT_DR_LCS_NO";
            this.gcDCT_DR_LCS_NO.Name = "gcDCT_DR_LCS_NO";
            this.gcDCT_DR_LCS_NO.OptionsColumn.AllowEdit = false;
            this.gcDCT_DR_LCS_NO.OptionsColumn.ReadOnly = true;
            this.gcDCT_DR_LCS_NO.Visible = true;
            this.gcDCT_DR_LCS_NO.VisibleIndex = 7;
            this.gcDCT_DR_LCS_NO.Width = 65;
            // 
            // gcEXM_MDFEE_CD
            // 
            this.gcEXM_MDFEE_CD.Caption = "수가코드";
            this.gcEXM_MDFEE_CD.FieldName = "EXM_MDFEE_CD";
            this.gcEXM_MDFEE_CD.Name = "gcEXM_MDFEE_CD";
            this.gcEXM_MDFEE_CD.OptionsColumn.AllowEdit = false;
            this.gcEXM_MDFEE_CD.OptionsColumn.ReadOnly = true;
            this.gcEXM_MDFEE_CD.Visible = true;
            this.gcEXM_MDFEE_CD.VisibleIndex = 8;
            this.gcEXM_MDFEE_CD.Width = 55;
            // 
            // gcEXM_CD
            // 
            this.gcEXM_CD.Caption = "검사코드";
            this.gcEXM_CD.FieldName = "EXM_CD";
            this.gcEXM_CD.Name = "gcEXM_CD";
            this.gcEXM_CD.OptionsColumn.AllowEdit = false;
            this.gcEXM_CD.OptionsColumn.ReadOnly = true;
            this.gcEXM_CD.Visible = true;
            this.gcEXM_CD.VisibleIndex = 9;
            // 
            // gcEXM_NM
            // 
            this.gcEXM_NM.Caption = "검사명";
            this.gcEXM_NM.FieldName = "EXM_NM";
            this.gcEXM_NM.Name = "gcEXM_NM";
            this.gcEXM_NM.OptionsColumn.AllowEdit = false;
            this.gcEXM_NM.OptionsColumn.ReadOnly = true;
            this.gcEXM_NM.Visible = true;
            this.gcEXM_NM.VisibleIndex = 10;
            // 
            // gcEXM_RST_TXT
            // 
            this.gcEXM_RST_TXT.Caption = "판독결과";
            this.gcEXM_RST_TXT.FieldName = "EXM_RST_TXT";
            this.gcEXM_RST_TXT.Name = "gcEXM_RST_TXT";
            this.gcEXM_RST_TXT.OptionsColumn.AllowEdit = false;
            this.gcEXM_RST_TXT.OptionsColumn.ReadOnly = true;
            this.gcEXM_RST_TXT.Visible = true;
            this.gcEXM_RST_TXT.VisibleIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(750, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "영상결과지(ERR001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(93, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 9;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // gcEXM_RST_TXT_ORG
            // 
            this.gcEXM_RST_TXT_ORG.Caption = "판독결과원문";
            this.gcEXM_RST_TXT_ORG.FieldName = "EXM_RST_TXT_ORG";
            this.gcEXM_RST_TXT_ORG.Name = "gcEXM_RST_TXT_ORG";
            // 
            // ERR001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.ControlBox = false;
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "ERR001";
            this.Text = "영상결과지(ERR001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_PRSC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_EXEC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RST_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDCT_DR_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDCT_DR_LCS_NO;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_MDFEE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RST_TXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RST_TXT_ORG;
    }
}
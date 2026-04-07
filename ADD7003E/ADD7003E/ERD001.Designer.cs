namespace ADD7003E
{
    partial class ERD001
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
            this.gcEXM_PRSC_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_GAT_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_RCV_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_RST_DT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_SPCM_CD_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_SPCM_ETC_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_MDFEE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_RST_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_REF_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_UNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_ADD_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDGSBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIFLD_DTL_SPC_SBJT_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRSC_DR_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXM_SPCM_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(173, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(11, 12);
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
            this.gcEXM_PRSC_DT,
            this.gcEXM_GAT_DT,
            this.gcEXM_RCV_DT,
            this.gcEXM_RST_DT,
            this.gcEXM_SPCM_CD_NM,
            this.gcEXM_SPCM_ETC_TXT,
            this.gcEXM_MDFEE_CD,
            this.gcEXM_CD,
            this.gcEXM_NM,
            this.gcEXM_RST_TXT,
            this.gcEXM_REF_TXT,
            this.gcEXM_UNIT,
            this.gcEXM_ADD_TXT,
            this.gcDGSBJT_CD,
            this.gcIFLD_DTL_SPC_SBJT_CD,
            this.gcPRSC_DR_NM,
            this.gcEXM_SPCM_CD});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcEXM_PRSC_DT
            // 
            this.gcEXM_PRSC_DT.Caption = "처방일시";
            this.gcEXM_PRSC_DT.FieldName = "EXM_PRSC_DT";
            this.gcEXM_PRSC_DT.Name = "gcEXM_PRSC_DT";
            this.gcEXM_PRSC_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_PRSC_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_PRSC_DT.Visible = true;
            this.gcEXM_PRSC_DT.VisibleIndex = 0;
            this.gcEXM_PRSC_DT.Width = 95;
            // 
            // gcEXM_GAT_DT
            // 
            this.gcEXM_GAT_DT.Caption = "채취일시";
            this.gcEXM_GAT_DT.FieldName = "EXM_GAT_DT";
            this.gcEXM_GAT_DT.Name = "gcEXM_GAT_DT";
            this.gcEXM_GAT_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_GAT_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_GAT_DT.Visible = true;
            this.gcEXM_GAT_DT.VisibleIndex = 1;
            this.gcEXM_GAT_DT.Width = 95;
            // 
            // gcEXM_RCV_DT
            // 
            this.gcEXM_RCV_DT.Caption = "접수일시";
            this.gcEXM_RCV_DT.FieldName = "EXM_RCV_DT";
            this.gcEXM_RCV_DT.Name = "gcEXM_RCV_DT";
            this.gcEXM_RCV_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_RCV_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_RCV_DT.Visible = true;
            this.gcEXM_RCV_DT.VisibleIndex = 2;
            this.gcEXM_RCV_DT.Width = 95;
            // 
            // gcEXM_RST_DT
            // 
            this.gcEXM_RST_DT.Caption = "결과일시";
            this.gcEXM_RST_DT.FieldName = "EXM_RST_DT";
            this.gcEXM_RST_DT.Name = "gcEXM_RST_DT";
            this.gcEXM_RST_DT.OptionsColumn.AllowEdit = false;
            this.gcEXM_RST_DT.OptionsColumn.ReadOnly = true;
            this.gcEXM_RST_DT.Visible = true;
            this.gcEXM_RST_DT.VisibleIndex = 3;
            this.gcEXM_RST_DT.Width = 95;
            // 
            // gcEXM_SPCM_CD_NM
            // 
            this.gcEXM_SPCM_CD_NM.Caption = "검체종류";
            this.gcEXM_SPCM_CD_NM.FieldName = "EXM_SPCM_CD_NM";
            this.gcEXM_SPCM_CD_NM.Name = "gcEXM_SPCM_CD_NM";
            this.gcEXM_SPCM_CD_NM.OptionsColumn.AllowEdit = false;
            this.gcEXM_SPCM_CD_NM.OptionsColumn.ReadOnly = true;
            this.gcEXM_SPCM_CD_NM.Visible = true;
            this.gcEXM_SPCM_CD_NM.VisibleIndex = 4;
            this.gcEXM_SPCM_CD_NM.Width = 55;
            // 
            // gcEXM_SPCM_ETC_TXT
            // 
            this.gcEXM_SPCM_ETC_TXT.Caption = "검체상세";
            this.gcEXM_SPCM_ETC_TXT.FieldName = "EXM_SPCM_ETC_TXT";
            this.gcEXM_SPCM_ETC_TXT.Name = "gcEXM_SPCM_ETC_TXT";
            this.gcEXM_SPCM_ETC_TXT.OptionsColumn.AllowEdit = false;
            this.gcEXM_SPCM_ETC_TXT.OptionsColumn.ReadOnly = true;
            this.gcEXM_SPCM_ETC_TXT.Visible = true;
            this.gcEXM_SPCM_ETC_TXT.VisibleIndex = 5;
            this.gcEXM_SPCM_ETC_TXT.Width = 55;
            // 
            // gcEXM_MDFEE_CD
            // 
            this.gcEXM_MDFEE_CD.Caption = "수가코드";
            this.gcEXM_MDFEE_CD.FieldName = "EXM_MDFEE_CD";
            this.gcEXM_MDFEE_CD.Name = "gcEXM_MDFEE_CD";
            this.gcEXM_MDFEE_CD.OptionsColumn.AllowEdit = false;
            this.gcEXM_MDFEE_CD.OptionsColumn.ReadOnly = true;
            this.gcEXM_MDFEE_CD.Visible = true;
            this.gcEXM_MDFEE_CD.VisibleIndex = 6;
            // 
            // gcEXM_CD
            // 
            this.gcEXM_CD.Caption = "검사코드";
            this.gcEXM_CD.FieldName = "EXM_CD";
            this.gcEXM_CD.Name = "gcEXM_CD";
            this.gcEXM_CD.OptionsColumn.AllowEdit = false;
            this.gcEXM_CD.OptionsColumn.ReadOnly = true;
            this.gcEXM_CD.Visible = true;
            this.gcEXM_CD.VisibleIndex = 7;
            // 
            // gcEXM_NM
            // 
            this.gcEXM_NM.Caption = "검사명";
            this.gcEXM_NM.FieldName = "EXM_NM";
            this.gcEXM_NM.Name = "gcEXM_NM";
            this.gcEXM_NM.OptionsColumn.AllowEdit = false;
            this.gcEXM_NM.OptionsColumn.ReadOnly = true;
            this.gcEXM_NM.Visible = true;
            this.gcEXM_NM.VisibleIndex = 8;
            // 
            // gcEXM_RST_TXT
            // 
            this.gcEXM_RST_TXT.Caption = "검사결과";
            this.gcEXM_RST_TXT.FieldName = "EXM_RST_TXT";
            this.gcEXM_RST_TXT.Name = "gcEXM_RST_TXT";
            this.gcEXM_RST_TXT.OptionsColumn.AllowEdit = false;
            this.gcEXM_RST_TXT.OptionsColumn.ReadOnly = true;
            this.gcEXM_RST_TXT.Visible = true;
            this.gcEXM_RST_TXT.VisibleIndex = 9;
            // 
            // gcEXM_REF_TXT
            // 
            this.gcEXM_REF_TXT.Caption = "참고치";
            this.gcEXM_REF_TXT.FieldName = "EXM_REF_TXT";
            this.gcEXM_REF_TXT.Name = "gcEXM_REF_TXT";
            this.gcEXM_REF_TXT.OptionsColumn.AllowEdit = false;
            this.gcEXM_REF_TXT.OptionsColumn.ReadOnly = true;
            this.gcEXM_REF_TXT.Visible = true;
            this.gcEXM_REF_TXT.VisibleIndex = 10;
            // 
            // gcEXM_UNIT
            // 
            this.gcEXM_UNIT.Caption = "단위";
            this.gcEXM_UNIT.FieldName = "EXM_UNIT";
            this.gcEXM_UNIT.Name = "gcEXM_UNIT";
            this.gcEXM_UNIT.OptionsColumn.AllowEdit = false;
            this.gcEXM_UNIT.OptionsColumn.ReadOnly = true;
            this.gcEXM_UNIT.Visible = true;
            this.gcEXM_UNIT.VisibleIndex = 11;
            // 
            // gcEXM_ADD_TXT
            // 
            this.gcEXM_ADD_TXT.Caption = "추가정보";
            this.gcEXM_ADD_TXT.FieldName = "EXM_ADD_TXT";
            this.gcEXM_ADD_TXT.Name = "gcEXM_ADD_TXT";
            this.gcEXM_ADD_TXT.OptionsColumn.AllowEdit = false;
            this.gcEXM_ADD_TXT.OptionsColumn.ReadOnly = true;
            this.gcEXM_ADD_TXT.Visible = true;
            this.gcEXM_ADD_TXT.VisibleIndex = 12;
            // 
            // gcDGSBJT_CD
            // 
            this.gcDGSBJT_CD.Caption = "처방과";
            this.gcDGSBJT_CD.FieldName = "DGSBJT_CD";
            this.gcDGSBJT_CD.Name = "gcDGSBJT_CD";
            this.gcDGSBJT_CD.Visible = true;
            this.gcDGSBJT_CD.VisibleIndex = 13;
            // 
            // gcIFLD_DTL_SPC_SBJT_CD
            // 
            this.gcIFLD_DTL_SPC_SBJT_CD.Caption = "내과세부과";
            this.gcIFLD_DTL_SPC_SBJT_CD.FieldName = "IFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Name = "gcIFLD_DTL_SPC_SBJT_CD";
            this.gcIFLD_DTL_SPC_SBJT_CD.Visible = true;
            this.gcIFLD_DTL_SPC_SBJT_CD.VisibleIndex = 14;
            // 
            // gcPRSC_DR_NM
            // 
            this.gcPRSC_DR_NM.Caption = "처방의사";
            this.gcPRSC_DR_NM.FieldName = "PRSC_DR_NM";
            this.gcPRSC_DR_NM.Name = "gcPRSC_DR_NM";
            this.gcPRSC_DR_NM.Visible = true;
            this.gcPRSC_DR_NM.VisibleIndex = 15;
            // 
            // gcEXM_SPCM_CD
            // 
            this.gcEXM_SPCM_CD.FieldName = "EXM_SPCM_CD";
            this.gcEXM_SPCM_CD.Name = "gcEXM_SPCM_CD";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(731, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "진단검사결과지(ERD001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(92, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 10;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // ERD001
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
            this.Name = "ERD001";
            this.Text = "진단검사결과지(ERD001)";
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
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_PRSC_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_GAT_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RCV_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RST_DT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_SPCM_CD_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_SPCM_ETC_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_MDFEE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_RST_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_REF_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_UNIT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_ADD_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDGSBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcIFLD_DTL_SPC_SBJT_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRSC_DR_NM;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXM_SPCM_CD;
        private System.Windows.Forms.Button btnChcek;
    }
}
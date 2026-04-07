namespace ADD7003E
{
    partial class RCC001
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
            this.gcSICK_TP_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDIAG_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSICK_SYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdInfo = new DevExpress.XtraGrid.GridControl();
            this.grdInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcITEM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCONTENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChcek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDiag
            // 
            this.grdDiag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDiag.Location = new System.Drawing.Point(451, 41);
            this.grdDiag.MainView = this.grdDiagView;
            this.grdDiag.Name = "grdDiag";
            this.grdDiag.Size = new System.Drawing.Size(418, 479);
            this.grdDiag.TabIndex = 19;
            this.grdDiag.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDiagView});
            // 
            // grdDiagView
            // 
            this.grdDiagView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSICK_TP_CD,
            this.gcDIAG_NM,
            this.gcSICK_SYM});
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
            // gcSICK_SYM
            // 
            this.gcSICK_SYM.Caption = "상병코드";
            this.gcSICK_SYM.FieldName = "SICK_SYM";
            this.gcSICK_SYM.Name = "gcSICK_SYM";
            this.gcSICK_SYM.Visible = true;
            this.gcSICK_SYM.VisibleIndex = 2;
            // 
            // grdInfo
            // 
            this.grdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdInfo.Location = new System.Drawing.Point(9, 41);
            this.grdInfo.MainView = this.grdInfoView;
            this.grdInfo.Name = "grdInfo";
            this.grdInfo.Size = new System.Drawing.Size(436, 479);
            this.grdInfo.TabIndex = 18;
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
            this.btnSubmit.Location = new System.Drawing.Point(173, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Text = "제출";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(9, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 16;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(711, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "협의진료기록자료(RCC001)";
            // 
            // btnChcek
            // 
            this.btnChcek.Location = new System.Drawing.Point(92, 12);
            this.btnChcek.Name = "btnChcek";
            this.btnChcek.Size = new System.Drawing.Size(75, 23);
            this.btnChcek.TabIndex = 38;
            this.btnChcek.Text = "점검";
            this.btnChcek.UseVisualStyleBackColor = true;
            this.btnChcek.Click += new System.EventHandler(this.btnChcek_Click);
            // 
            // RCC001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.btnChcek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdDiag);
            this.Controls.Add(this.grdInfo);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnQuery);
            this.Name = "RCC001";
            this.Text = "협의진료기록자료(RCC001)";
            ((System.ComponentModel.ISupportInitialize)(this.grdDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfoView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdDiag;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDiagView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_TP_CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDIAG_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcSICK_SYM;
        private DevExpress.XtraGrid.GridControl grdInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCONTENT;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChcek;
    }
}
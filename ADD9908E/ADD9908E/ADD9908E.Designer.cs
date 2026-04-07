namespace ADD9908E
{
    partial class ADD9908E
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcUNICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNINM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJBUNICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJBUNINM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdJBUnicd = new DevExpress.XtraGrid.GridControl();
            this.grdJBUnicdView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSTDCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTDNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJBUnicd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJBUnicdView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdMain.Location = new System.Drawing.Point(7, 6);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(487, 510);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcUNICD,
            this.gcUNINM,
            this.gcJBUNICD,
            this.gcJBUNINM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcUNICD
            // 
            this.gcUNICD.Caption = "조합기호";
            this.gcUNICD.FieldName = "UNICD";
            this.gcUNICD.Name = "gcUNICD";
            this.gcUNICD.OptionsColumn.AllowEdit = false;
            this.gcUNICD.OptionsColumn.ReadOnly = true;
            this.gcUNICD.Visible = true;
            this.gcUNICD.VisibleIndex = 0;
            // 
            // gcUNINM
            // 
            this.gcUNINM.Caption = "조합명";
            this.gcUNINM.FieldName = "UNINM";
            this.gcUNINM.Name = "gcUNINM";
            this.gcUNINM.OptionsColumn.AllowEdit = false;
            this.gcUNINM.OptionsColumn.ReadOnly = true;
            this.gcUNINM.Visible = true;
            this.gcUNINM.VisibleIndex = 1;
            this.gcUNINM.Width = 150;
            // 
            // gcJBUNICD
            // 
            this.gcJBUNICD.Caption = "표준기호";
            this.gcJBUNICD.FieldName = "JBUNICD";
            this.gcJBUNICD.Name = "gcJBUNICD";
            this.gcJBUNICD.OptionsColumn.AllowEdit = false;
            this.gcJBUNICD.OptionsColumn.ReadOnly = true;
            this.gcJBUNICD.Visible = true;
            this.gcJBUNICD.VisibleIndex = 2;
            // 
            // gcJBUNINM
            // 
            this.gcJBUNINM.Caption = "손보사명칭";
            this.gcJBUNINM.FieldName = "JBUNINM";
            this.gcJBUNINM.Name = "gcJBUNINM";
            this.gcJBUNINM.OptionsColumn.AllowEdit = false;
            this.gcJBUNINM.OptionsColumn.ReadOnly = true;
            this.gcJBUNINM.Visible = true;
            this.gcJBUNINM.VisibleIndex = 3;
            this.gcJBUNINM.Width = 150;
            // 
            // grdJBUnicd
            // 
            this.grdJBUnicd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdJBUnicd.Location = new System.Drawing.Point(562, 7);
            this.grdJBUnicd.MainView = this.grdJBUnicdView;
            this.grdJBUnicd.Name = "grdJBUnicd";
            this.grdJBUnicd.Size = new System.Drawing.Size(262, 533);
            this.grdJBUnicd.TabIndex = 1;
            this.grdJBUnicd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdJBUnicdView});
            // 
            // grdJBUnicdView
            // 
            this.grdJBUnicdView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSTDCODE,
            this.gcSTDNAME});
            this.grdJBUnicdView.GridControl = this.grdJBUnicd;
            this.grdJBUnicdView.Name = "grdJBUnicdView";
            this.grdJBUnicdView.OptionsView.ColumnAutoWidth = false;
            this.grdJBUnicdView.OptionsView.ShowGroupPanel = false;
            this.grdJBUnicdView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdJBUnicdView.DoubleClick += new System.EventHandler(this.grdJBUnicdView_DoubleClick);
            // 
            // gcSTDCODE
            // 
            this.gcSTDCODE.Caption = "표준기호";
            this.gcSTDCODE.FieldName = "STDCODE";
            this.gcSTDCODE.Name = "gcSTDCODE";
            this.gcSTDCODE.OptionsColumn.AllowEdit = false;
            this.gcSTDCODE.OptionsColumn.ReadOnly = true;
            this.gcSTDCODE.Visible = true;
            this.gcSTDCODE.VisibleIndex = 0;
            // 
            // gcSTDNAME
            // 
            this.gcSTDNAME.Caption = "손보사명칭";
            this.gcSTDNAME.FieldName = "STDNAME";
            this.gcSTDNAME.Name = "gcSTDNAME";
            this.gcSTDNAME.OptionsColumn.AllowEdit = false;
            this.gcSTDNAME.OptionsColumn.ReadOnly = true;
            this.gcSTDNAME.Visible = true;
            this.gcSTDNAME.VisibleIndex = 1;
            this.gcSTDNAME.Width = 150;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 528);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 12);
            this.label1.TabIndex = 51;
            this.label1.Text = "표준손보사 선택시 바로 저장됩니다.";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(505, 122);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(49, 23);
            this.btnApply.TabIndex = 52;
            this.btnApply.Text = "<<";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(505, 151);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(49, 23);
            this.btnDel.TabIndex = 53;
            this.btnDel.Text = ">>";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // ADD9908E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 548);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdJBUnicd);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD9908E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "표준손보사설정(ADD9908E)";
            this.Load += new System.EventHandler(this.ADD9908E_Load);
            this.Activated += new System.EventHandler(this.ADD9908E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJBUnicd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJBUnicdView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNINM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJBUNICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcJBUNINM;
        private DevExpress.XtraGrid.GridControl grdJBUnicd;
        private DevExpress.XtraGrid.Views.Grid.GridView grdJBUnicdView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDCODE;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDNAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnDel;
    }
}


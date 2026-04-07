namespace ADD8002Q
{
    partial class ADD8002Q_P
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
            this.gcACCNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCNTNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREPDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPAYDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCNTTOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMTOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREALPAYAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMEMO = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(8, 8);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(829, 412);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcACCNO,
            this.gcCNTNO,
            this.gcREPDT,
            this.gcPAYDT,
            this.gcCNTTOT,
            this.gcDEMTOT,
            this.gcREALPAYAMT,
            this.gcMEMO});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcACCNO
            // 
            this.gcACCNO.Caption = "접수번호";
            this.gcACCNO.FieldName = "ACCNO";
            this.gcACCNO.Name = "gcACCNO";
            this.gcACCNO.OptionsColumn.AllowEdit = false;
            this.gcACCNO.Visible = true;
            this.gcACCNO.VisibleIndex = 0;
            this.gcACCNO.Width = 150;
            // 
            // gcCNTNO
            // 
            this.gcCNTNO.Caption = "차수";
            this.gcCNTNO.FieldName = "CNTNO";
            this.gcCNTNO.Name = "gcCNTNO";
            this.gcCNTNO.OptionsColumn.AllowEdit = false;
            this.gcCNTNO.Visible = true;
            this.gcCNTNO.VisibleIndex = 1;
            this.gcCNTNO.Width = 45;
            // 
            // gcREPDT
            // 
            this.gcREPDT.Caption = "통보일자";
            this.gcREPDT.FieldName = "REPDT";
            this.gcREPDT.Name = "gcREPDT";
            this.gcREPDT.OptionsColumn.AllowEdit = false;
            this.gcREPDT.Visible = true;
            this.gcREPDT.VisibleIndex = 2;
            // 
            // gcPAYDT
            // 
            this.gcPAYDT.Caption = "예정일자";
            this.gcPAYDT.FieldName = "PAYDT";
            this.gcPAYDT.Name = "gcPAYDT";
            this.gcPAYDT.OptionsColumn.AllowEdit = false;
            this.gcPAYDT.Visible = true;
            this.gcPAYDT.VisibleIndex = 3;
            // 
            // gcCNTTOT
            // 
            this.gcCNTTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcCNTTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcCNTTOT.Caption = "청구건수";
            this.gcCNTTOT.FieldName = "CNTTOT";
            this.gcCNTTOT.Name = "gcCNTTOT";
            this.gcCNTTOT.OptionsColumn.AllowEdit = false;
            this.gcCNTTOT.Visible = true;
            this.gcCNTTOT.VisibleIndex = 4;
            // 
            // gcDEMTOT
            // 
            this.gcDEMTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMTOT.Caption = "청구금액";
            this.gcDEMTOT.FieldName = "DEMTOT";
            this.gcDEMTOT.Name = "gcDEMTOT";
            this.gcDEMTOT.OptionsColumn.AllowEdit = false;
            this.gcDEMTOT.Visible = true;
            this.gcDEMTOT.VisibleIndex = 5;
            // 
            // gcREALPAYAMT
            // 
            this.gcREALPAYAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcREALPAYAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcREALPAYAMT.Caption = "실지급액";
            this.gcREALPAYAMT.FieldName = "REALPAYAMT";
            this.gcREALPAYAMT.Name = "gcREALPAYAMT";
            this.gcREALPAYAMT.OptionsColumn.AllowEdit = false;
            this.gcREALPAYAMT.Visible = true;
            this.gcREALPAYAMT.VisibleIndex = 6;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "비고";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.OptionsColumn.AllowEdit = false;
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 7;
            this.gcMEMO.Width = 220;
            // 
            // ADD8002Q_P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 428);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD8002Q_P";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "자료검색(ADD8002Q_P)";
            this.Load += new System.EventHandler(this.ADD8002Q_P_Load);
            this.Activated += new System.EventHandler(this.ADD8002Q_P_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcACCNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcCNTNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcREPDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcPAYDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcCNTTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcREALPAYAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
    }
}
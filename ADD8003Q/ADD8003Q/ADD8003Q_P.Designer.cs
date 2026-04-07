namespace ADD8003Q
{
    partial class ADD8003Q_P
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
            this.gcDEMCNTTOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMQYTOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXAMCNTTOT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXAMQYTOT = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.grdMain.Location = new System.Drawing.Point(12, 12);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(690, 415);
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
            this.gcDEMCNTTOT,
            this.gcDEMQYTOT,
            this.gcEXAMCNTTOT,
            this.gcEXAMQYTOT,
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
            // 
            // gcCNTNO
            // 
            this.gcCNTNO.Caption = "차수";
            this.gcCNTNO.FieldName = "CNTNO";
            this.gcCNTNO.Name = "gcCNTNO";
            this.gcCNTNO.OptionsColumn.AllowEdit = false;
            this.gcCNTNO.Visible = true;
            this.gcCNTNO.VisibleIndex = 1;
            this.gcCNTNO.Width = 50;
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
            // gcDEMCNTTOT
            // 
            this.gcDEMCNTTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMCNTTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMCNTTOT.Caption = "청구건수";
            this.gcDEMCNTTOT.FieldName = "DEMCNTTOT";
            this.gcDEMCNTTOT.Name = "gcDEMCNTTOT";
            this.gcDEMCNTTOT.OptionsColumn.AllowEdit = false;
            this.gcDEMCNTTOT.Visible = true;
            this.gcDEMCNTTOT.VisibleIndex = 3;
            this.gcDEMCNTTOT.Width = 60;
            // 
            // gcDEMQYTOT
            // 
            this.gcDEMQYTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMQYTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMQYTOT.Caption = "청구금액";
            this.gcDEMQYTOT.FieldName = "DEMQYTOT";
            this.gcDEMQYTOT.Name = "gcDEMQYTOT";
            this.gcDEMQYTOT.OptionsColumn.AllowEdit = false;
            this.gcDEMQYTOT.Visible = true;
            this.gcDEMQYTOT.VisibleIndex = 4;
            this.gcDEMQYTOT.Width = 90;
            // 
            // gcEXAMCNTTOT
            // 
            this.gcEXAMCNTTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcEXAMCNTTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcEXAMCNTTOT.Caption = "심사건수";
            this.gcEXAMCNTTOT.FieldName = "EXAMCNTTOT";
            this.gcEXAMCNTTOT.Name = "gcEXAMCNTTOT";
            this.gcEXAMCNTTOT.OptionsColumn.AllowEdit = false;
            this.gcEXAMCNTTOT.Visible = true;
            this.gcEXAMCNTTOT.VisibleIndex = 5;
            this.gcEXAMCNTTOT.Width = 60;
            // 
            // gcEXAMQYTOT
            // 
            this.gcEXAMQYTOT.AppearanceCell.Options.UseTextOptions = true;
            this.gcEXAMQYTOT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcEXAMQYTOT.Caption = "심사금액";
            this.gcEXAMQYTOT.FieldName = "EXAMQYTOT";
            this.gcEXAMQYTOT.Name = "gcEXAMQYTOT";
            this.gcEXAMQYTOT.OptionsColumn.AllowEdit = false;
            this.gcEXAMQYTOT.Visible = true;
            this.gcEXAMQYTOT.VisibleIndex = 6;
            this.gcEXAMQYTOT.Width = 90;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "비고";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.OptionsColumn.AllowEdit = false;
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 7;
            this.gcMEMO.Width = 150;
            // 
            // ADD8003Q_P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 439);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD8003Q_P";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "자료검색(ADD8003Q_P)";
            this.Load += new System.EventHandler(this.ADD8003Q_P_Load);
            this.Activated += new System.EventHandler(this.ADD8003Q_P_Activated);
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
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMCNTTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMQYTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXAMCNTTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXAMQYTOT;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
    }
}
namespace ADD0715E
{
    partial class ADD0715E_01
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
            this.gcDEMNO = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.grdMain.Location = new System.Drawing.Point(5, 5);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(315, 252);
            this.grdMain.TabIndex = 1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDEMNO,
            this.gcMEMO});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcDEMNO
            // 
            this.gcDEMNO.Caption = "통보번호";
            this.gcDEMNO.FieldName = "DEMNO";
            this.gcDEMNO.Name = "gcDEMNO";
            this.gcDEMNO.OptionsColumn.AllowEdit = false;
            this.gcDEMNO.Visible = true;
            this.gcDEMNO.VisibleIndex = 0;
            this.gcDEMNO.Width = 100;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "참조란";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.OptionsColumn.AllowEdit = false;
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 1;
            this.gcMEMO.Width = 175;
            // 
            // ADD0715E_01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 262);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD0715E_01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "리스트조회(ADD0715E_01)";
            this.Load += new System.EventHandler(this.ADD0715E_01_Load);
            this.Activated += new System.EventHandler(this.ADD0715E_01_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
    }
}
namespace ADD7000E
{
    partial class ADD7000E_1
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
            this.grdViewer = new DevExpress.XtraGrid.GridControl();
            this.grdViewerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcKEY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDESC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcVALUE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADD1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADD2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADD3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADD4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdViewer
            // 
            this.grdViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdViewer.Location = new System.Drawing.Point(12, 12);
            this.grdViewer.MainView = this.grdViewerView;
            this.grdViewer.Name = "grdViewer";
            this.grdViewer.Size = new System.Drawing.Size(810, 538);
            this.grdViewer.TabIndex = 0;
            this.grdViewer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewerView,
            this.gridView2});
            // 
            // grdViewerView
            // 
            this.grdViewerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcKEY,
            this.gcDESC,
            this.gcVALUE,
            this.gcADD1,
            this.gcADD2,
            this.gcADD3,
            this.gcADD4});
            this.grdViewerView.GridControl = this.grdViewer;
            this.grdViewerView.Name = "grdViewerView";
            // 
            // gcKEY
            // 
            this.gcKEY.Caption = "키";
            this.gcKEY.FieldName = "KEY_VALUE";
            this.gcKEY.Name = "gcKEY";
            this.gcKEY.OptionsColumn.AllowEdit = false;
            this.gcKEY.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcKEY.Visible = true;
            this.gcKEY.VisibleIndex = 0;
            // 
            // gcDESC
            // 
            this.gcDESC.Caption = "설명";
            this.gcDESC.FieldName = "DESC_VALUE";
            this.gcDESC.Name = "gcDESC";
            this.gcDESC.OptionsColumn.AllowEdit = false;
            this.gcDESC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDESC.Visible = true;
            this.gcDESC.VisibleIndex = 1;
            // 
            // gcVALUE
            // 
            this.gcVALUE.Caption = "값";
            this.gcVALUE.FieldName = "DATA_VALUE";
            this.gcVALUE.Name = "gcVALUE";
            this.gcVALUE.OptionsColumn.AllowEdit = false;
            this.gcVALUE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcVALUE.Visible = true;
            this.gcVALUE.VisibleIndex = 2;
            // 
            // gcADD1
            // 
            this.gcADD1.Caption = " ";
            this.gcADD1.FieldName = "ADD1_VALUE";
            this.gcADD1.Name = "gcADD1";
            this.gcADD1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcADD1.Visible = true;
            this.gcADD1.VisibleIndex = 3;
            // 
            // gcADD2
            // 
            this.gcADD2.Caption = " ";
            this.gcADD2.FieldName = "ADD2_VALUE";
            this.gcADD2.Name = "gcADD2";
            this.gcADD2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcADD2.Visible = true;
            this.gcADD2.VisibleIndex = 4;
            // 
            // gcADD3
            // 
            this.gcADD3.Caption = " ";
            this.gcADD3.FieldName = "ADD3_VALUE";
            this.gcADD3.Name = "gcADD3";
            this.gcADD3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcADD3.Visible = true;
            this.gcADD3.VisibleIndex = 5;
            // 
            // gcADD4
            // 
            this.gcADD4.Caption = " ";
            this.gcADD4.FieldName = "ADD4_VALUE";
            this.gcADD4.Name = "gcADD4";
            this.gcADD4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcADD4.Visible = true;
            this.gcADD4.VisibleIndex = 6;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdViewer;
            this.gridView2.Name = "gridView2";
            // 
            // ADD7000E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 562);
            this.Controls.Add(this.grdViewer);
            this.Name = "ADD7000E_1";
            this.Text = "자료조회(ADD7000E_1)";
            this.Load += new System.EventHandler(this.ADD7000E_1_Load);
            this.Activated += new System.EventHandler(this.ADD7000E_1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ADD7000E_1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdViewer;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewerView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gcKEY;
        private DevExpress.XtraGrid.Columns.GridColumn gcVALUE;
        private DevExpress.XtraGrid.Columns.GridColumn gcDESC;
        private DevExpress.XtraGrid.Columns.GridColumn gcADD1;
        private DevExpress.XtraGrid.Columns.GridColumn gcADD2;
        private DevExpress.XtraGrid.Columns.GridColumn gcADD3;
        private DevExpress.XtraGrid.Columns.GridColumn gcADD4;

    }
}
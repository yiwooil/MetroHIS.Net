namespace ADD0112E
{
    partial class ADD0112E_1
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
            this.components = new System.ComponentModel.Container();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRESID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLSTDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHTELNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOTELNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINSNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 39);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(660, 361);
            this.grdMain.TabIndex = 1;
            this.grdMain.ToolTipController = this.toolTipController1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPID,
            this.gcPNM,
            this.gcRESID,
            this.gcLSTDT,
            this.gcQFYNM,
            this.gcBEDEDT,
            this.gcHTELNO,
            this.gcOTELNO,
            this.gcADDR,
            this.gcINSNM,
            this.gcRMK});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsHint.ShowCellHints = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdMainView_KeyPress);
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.AllowEdit = false;
            this.gcPID.OptionsColumn.ReadOnly = true;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 0;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자성명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.OptionsColumn.ReadOnly = true;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcRESID
            // 
            this.gcRESID.Caption = "주민번호";
            this.gcRESID.FieldName = "RESID";
            this.gcRESID.Name = "gcRESID";
            this.gcRESID.OptionsColumn.AllowEdit = false;
            this.gcRESID.OptionsColumn.ReadOnly = true;
            this.gcRESID.Visible = true;
            this.gcRESID.VisibleIndex = 2;
            this.gcRESID.Width = 110;
            // 
            // gcLSTDT
            // 
            this.gcLSTDT.Caption = "최근내원일";
            this.gcLSTDT.FieldName = "LSTDT";
            this.gcLSTDT.Name = "gcLSTDT";
            this.gcLSTDT.OptionsColumn.AllowEdit = false;
            this.gcLSTDT.OptionsColumn.ReadOnly = true;
            this.gcLSTDT.Visible = true;
            this.gcLSTDT.VisibleIndex = 3;
            // 
            // gcQFYNM
            // 
            this.gcQFYNM.Caption = "자격";
            this.gcQFYNM.FieldName = "QFYNM";
            this.gcQFYNM.Name = "gcQFYNM";
            this.gcQFYNM.OptionsColumn.AllowEdit = false;
            this.gcQFYNM.OptionsColumn.ReadOnly = true;
            this.gcQFYNM.Visible = true;
            this.gcQFYNM.VisibleIndex = 4;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "최근입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.AllowEdit = false;
            this.gcBEDEDT.OptionsColumn.ReadOnly = true;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 5;
            // 
            // gcHTELNO
            // 
            this.gcHTELNO.Caption = "전화번호";
            this.gcHTELNO.FieldName = "HTELNO";
            this.gcHTELNO.Name = "gcHTELNO";
            this.gcHTELNO.OptionsColumn.AllowEdit = false;
            this.gcHTELNO.OptionsColumn.ReadOnly = true;
            this.gcHTELNO.Visible = true;
            this.gcHTELNO.VisibleIndex = 6;
            // 
            // gcOTELNO
            // 
            this.gcOTELNO.Caption = "회사/휴대폰";
            this.gcOTELNO.FieldName = "OTELNO";
            this.gcOTELNO.Name = "gcOTELNO";
            this.gcOTELNO.OptionsColumn.AllowEdit = false;
            this.gcOTELNO.OptionsColumn.ReadOnly = true;
            this.gcOTELNO.Visible = true;
            this.gcOTELNO.VisibleIndex = 7;
            // 
            // gcADDR
            // 
            this.gcADDR.Caption = "주소";
            this.gcADDR.FieldName = "ADDR";
            this.gcADDR.Name = "gcADDR";
            this.gcADDR.OptionsColumn.AllowEdit = false;
            this.gcADDR.OptionsColumn.ReadOnly = true;
            this.gcADDR.Visible = true;
            this.gcADDR.VisibleIndex = 8;
            this.gcADDR.Width = 150;
            // 
            // gcINSNM
            // 
            this.gcINSNM.Caption = "피보험자";
            this.gcINSNM.FieldName = "INSNM";
            this.gcINSNM.Name = "gcINSNM";
            this.gcINSNM.OptionsColumn.AllowEdit = false;
            this.gcINSNM.OptionsColumn.ReadOnly = true;
            this.gcINSNM.Visible = true;
            this.gcINSNM.VisibleIndex = 9;
            // 
            // gcRMK
            // 
            this.gcRMK.Caption = "메모";
            this.gcRMK.FieldName = "RMK";
            this.gcRMK.Name = "gcRMK";
            this.gcRMK.OptionsColumn.AllowEdit = false;
            this.gcRMK.OptionsColumn.ReadOnly = true;
            this.gcRMK.Visible = true;
            this.gcRMK.VisibleIndex = 10;
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(133, 12);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.Size = new System.Drawing.Size(120, 21);
            this.txtPnm.TabIndex = 0;
            this.txtPnm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPnm_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "환자성명/주민번호 :";
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // ADD0112E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD0112E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "환자조회(ADD0112E_1)";
            this.Load += new System.EventHandler(this.ADD0112E_1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRESID;
        private DevExpress.XtraGrid.Columns.GridColumn gcLSTDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcHTELNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcOTELNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcADDR;
        private DevExpress.XtraGrid.Columns.GridColumn gcINSNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRMK;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}
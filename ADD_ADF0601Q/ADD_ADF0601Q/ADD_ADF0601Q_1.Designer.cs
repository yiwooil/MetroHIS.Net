namespace ADD_ADF0601Q
{
    partial class ADD_ADF0601Q_1
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
            this.gcPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRESID_MASK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWARD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBEDEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQFYNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTELNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPtnt7 = new System.Windows.Forms.RadioButton();
            this.rbPtnt6 = new System.Windows.Forms.RadioButton();
            this.rbPtnt5 = new System.Windows.Forms.RadioButton();
            this.rbPtnt4 = new System.Windows.Forms.RadioButton();
            this.rbPtnt3 = new System.Windows.Forms.RadioButton();
            this.rbPtnt1 = new System.Windows.Forms.RadioButton();
            this.rbPtnt2 = new System.Windows.Forms.RadioButton();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDptcd = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboWard = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 68);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(679, 387);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcPID,
            this.gcPNM,
            this.gcRESID_MASK,
            this.gcSAGE,
            this.gcWARD,
            this.gcDPTNM,
            this.gcPDRNM,
            this.gcBEDEDT,
            this.gcQFYNM,
            this.gcTELNO});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdMainView_KeyPress);
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcPID
            // 
            this.gcPID.Caption = "환자ID";
            this.gcPID.FieldName = "PID";
            this.gcPID.Name = "gcPID";
            this.gcPID.OptionsColumn.AllowEdit = false;
            this.gcPID.Visible = true;
            this.gcPID.VisibleIndex = 0;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcRESID_MASK
            // 
            this.gcRESID_MASK.Caption = "주민번호";
            this.gcRESID_MASK.FieldName = "RESID_MASK";
            this.gcRESID_MASK.Name = "gcRESID_MASK";
            this.gcRESID_MASK.OptionsColumn.AllowEdit = false;
            this.gcRESID_MASK.Visible = true;
            this.gcRESID_MASK.VisibleIndex = 2;
            this.gcRESID_MASK.Width = 120;
            // 
            // gcSAGE
            // 
            this.gcSAGE.Caption = "성/나이";
            this.gcSAGE.FieldName = "SAGE";
            this.gcSAGE.Name = "gcSAGE";
            this.gcSAGE.OptionsColumn.AllowEdit = false;
            this.gcSAGE.Visible = true;
            this.gcSAGE.VisibleIndex = 3;
            this.gcSAGE.Width = 55;
            // 
            // gcWARD
            // 
            this.gcWARD.Caption = "병실";
            this.gcWARD.FieldName = "WARD";
            this.gcWARD.Name = "gcWARD";
            this.gcWARD.OptionsColumn.AllowEdit = false;
            this.gcWARD.Visible = true;
            this.gcWARD.VisibleIndex = 4;
            this.gcWARD.Width = 85;
            // 
            // gcDPTNM
            // 
            this.gcDPTNM.Caption = "진료과";
            this.gcDPTNM.FieldName = "DPTNM";
            this.gcDPTNM.Name = "gcDPTNM";
            this.gcDPTNM.OptionsColumn.AllowEdit = false;
            this.gcDPTNM.Visible = true;
            this.gcDPTNM.VisibleIndex = 5;
            this.gcDPTNM.Width = 80;
            // 
            // gcPDRNM
            // 
            this.gcPDRNM.Caption = "주치의";
            this.gcPDRNM.FieldName = "PDRNM";
            this.gcPDRNM.Name = "gcPDRNM";
            this.gcPDRNM.OptionsColumn.AllowEdit = false;
            this.gcPDRNM.Visible = true;
            this.gcPDRNM.VisibleIndex = 6;
            // 
            // gcBEDEDT
            // 
            this.gcBEDEDT.Caption = "입원일";
            this.gcBEDEDT.FieldName = "BEDEDT";
            this.gcBEDEDT.Name = "gcBEDEDT";
            this.gcBEDEDT.OptionsColumn.AllowEdit = false;
            this.gcBEDEDT.Visible = true;
            this.gcBEDEDT.VisibleIndex = 7;
            // 
            // gcQFYNM
            // 
            this.gcQFYNM.Caption = "자격";
            this.gcQFYNM.FieldName = "QFYNM";
            this.gcQFYNM.Name = "gcQFYNM";
            this.gcQFYNM.OptionsColumn.AllowEdit = false;
            this.gcQFYNM.Visible = true;
            this.gcQFYNM.VisibleIndex = 8;
            // 
            // gcTELNO
            // 
            this.gcTELNO.Caption = "전화번호";
            this.gcTELNO.FieldName = "TELNO";
            this.gcTELNO.Name = "gcTELNO";
            this.gcTELNO.OptionsColumn.AllowEdit = false;
            this.gcTELNO.Visible = true;
            this.gcTELNO.VisibleIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbPtnt7);
            this.panel1.Controls.Add(this.rbPtnt6);
            this.panel1.Controls.Add(this.rbPtnt5);
            this.panel1.Controls.Add(this.rbPtnt4);
            this.panel1.Controls.Add(this.rbPtnt3);
            this.panel1.Controls.Add(this.rbPtnt1);
            this.panel1.Controls.Add(this.rbPtnt2);
            this.panel1.Location = new System.Drawing.Point(14, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 28);
            this.panel1.TabIndex = 1;
            // 
            // rbPtnt7
            // 
            this.rbPtnt7.AutoSize = true;
            this.rbPtnt7.Location = new System.Drawing.Point(515, 6);
            this.rbPtnt7.Name = "rbPtnt7";
            this.rbPtnt7.Size = new System.Drawing.Size(71, 16);
            this.rbPtnt7.TabIndex = 6;
            this.rbPtnt7.Text = "전체환자";
            this.rbPtnt7.UseVisualStyleBackColor = true;
            this.rbPtnt7.CheckedChanged += new System.EventHandler(this.rbPtnt7_CheckedChanged);
            // 
            // rbPtnt6
            // 
            this.rbPtnt6.AutoSize = true;
            this.rbPtnt6.Location = new System.Drawing.Point(427, 7);
            this.rbPtnt6.Name = "rbPtnt6";
            this.rbPtnt6.Size = new System.Drawing.Size(83, 16);
            this.rbPtnt6.TabIndex = 5;
            this.rbPtnt6.Text = "당일퇴원자";
            this.rbPtnt6.UseVisualStyleBackColor = true;
            this.rbPtnt6.CheckedChanged += new System.EventHandler(this.rbPtnt6_CheckedChanged);
            // 
            // rbPtnt5
            // 
            this.rbPtnt5.AutoSize = true;
            this.rbPtnt5.Location = new System.Drawing.Point(338, 7);
            this.rbPtnt5.Name = "rbPtnt5";
            this.rbPtnt5.Size = new System.Drawing.Size(83, 16);
            this.rbPtnt5.TabIndex = 4;
            this.rbPtnt5.Text = "심사완료자";
            this.rbPtnt5.UseVisualStyleBackColor = true;
            this.rbPtnt5.CheckedChanged += new System.EventHandler(this.rbPtnt5_CheckedChanged);
            // 
            // rbPtnt4
            // 
            this.rbPtnt4.AutoSize = true;
            this.rbPtnt4.Location = new System.Drawing.Point(250, 7);
            this.rbPtnt4.Name = "rbPtnt4";
            this.rbPtnt4.Size = new System.Drawing.Size(83, 16);
            this.rbPtnt4.TabIndex = 3;
            this.rbPtnt4.Text = "처방마감자";
            this.rbPtnt4.UseVisualStyleBackColor = true;
            this.rbPtnt4.CheckedChanged += new System.EventHandler(this.rbPtnt4_CheckedChanged);
            // 
            // rbPtnt3
            // 
            this.rbPtnt3.AutoSize = true;
            this.rbPtnt3.Location = new System.Drawing.Point(164, 7);
            this.rbPtnt3.Name = "rbPtnt3";
            this.rbPtnt3.Size = new System.Drawing.Size(83, 16);
            this.rbPtnt3.TabIndex = 2;
            this.rbPtnt3.Text = "퇴원예정자";
            this.rbPtnt3.UseVisualStyleBackColor = true;
            this.rbPtnt3.CheckedChanged += new System.EventHandler(this.rbPtnt3_CheckedChanged);
            // 
            // rbPtnt1
            // 
            this.rbPtnt1.AutoSize = true;
            this.rbPtnt1.Location = new System.Drawing.Point(6, 7);
            this.rbPtnt1.Name = "rbPtnt1";
            this.rbPtnt1.Size = new System.Drawing.Size(83, 16);
            this.rbPtnt1.TabIndex = 1;
            this.rbPtnt1.Text = "입원예정자";
            this.rbPtnt1.UseVisualStyleBackColor = true;
            this.rbPtnt1.CheckedChanged += new System.EventHandler(this.rbPtnt1_CheckedChanged);
            // 
            // rbPtnt2
            // 
            this.rbPtnt2.AutoSize = true;
            this.rbPtnt2.Checked = true;
            this.rbPtnt2.Location = new System.Drawing.Point(90, 7);
            this.rbPtnt2.Name = "rbPtnt2";
            this.rbPtnt2.Size = new System.Drawing.Size(71, 16);
            this.rbPtnt2.TabIndex = 0;
            this.rbPtnt2.TabStop = true;
            this.rbPtnt2.Text = "재원환자";
            this.rbPtnt2.UseVisualStyleBackColor = true;
            this.rbPtnt2.CheckedChanged += new System.EventHandler(this.rbPtnt2_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(616, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 73;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(63, 41);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.Size = new System.Drawing.Size(86, 21);
            this.txtPnm.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 75;
            this.label1.Text = "환자명 :";
            // 
            // cboDptcd
            // 
            this.cboDptcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDptcd.FormattingEnabled = true;
            this.cboDptcd.Location = new System.Drawing.Point(208, 42);
            this.cboDptcd.Name = "cboDptcd";
            this.cboDptcd.Size = new System.Drawing.Size(139, 20);
            this.cboDptcd.TabIndex = 77;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(157, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 12);
            this.label12.TabIndex = 76;
            this.label12.Text = "진료과 :";
            // 
            // cboWard
            // 
            this.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWard.FormattingEnabled = true;
            this.cboWard.Location = new System.Drawing.Point(405, 42);
            this.cboWard.Name = "cboWard";
            this.cboWard.Size = new System.Drawing.Size(73, 20);
            this.cboWard.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 78;
            this.label2.Text = "병동 :";
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.Location = new System.Drawing.Point(534, 41);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(153, 20);
            this.cboQfycd.TabIndex = 80;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(494, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 12);
            this.label15.TabIndex = 81;
            this.label15.Text = "자격 :";
            // 
            // ADD_ADF0601Q_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 467);
            this.Controls.Add(this.cboQfycd);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboWard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDptcd);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD_ADF0601Q_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ADD_ADF0601Q_1";
            this.Load += new System.EventHandler(this.ADD_ADF0601Q_1_Load);
            this.Activated += new System.EventHandler(this.ADD_ADF0601Q_1_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPtnt2;
        private DevExpress.XtraGrid.Columns.GridColumn gcPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcRESID_MASK;
        private DevExpress.XtraGrid.Columns.GridColumn gcSAGE;
        private DevExpress.XtraGrid.Columns.GridColumn gcWARD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcBEDEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcQFYNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcTELNO;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.RadioButton rbPtnt1;
        private System.Windows.Forms.RadioButton rbPtnt4;
        private System.Windows.Forms.RadioButton rbPtnt3;
        private System.Windows.Forms.RadioButton rbPtnt7;
        private System.Windows.Forms.RadioButton rbPtnt6;
        private System.Windows.Forms.RadioButton rbPtnt5;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDptcd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboWard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.Label label15;
    }
}
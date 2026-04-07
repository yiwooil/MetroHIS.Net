namespace ADD9909E
{
    partial class ADD9909E_1
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
            this.txtDrnm = new System.Windows.Forms.TextBox();
            this.txtDrid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grdDoct = new DevExpress.XtraGrid.GridControl();
            this.grdDoctView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDRID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDept = new DevExpress.XtraGrid.GridControl();
            this.grdDeptView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDPTCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkDrExp = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeptView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDrnm
            // 
            this.txtDrnm.Location = new System.Drawing.Point(137, 31);
            this.txtDrnm.Name = "txtDrnm";
            this.txtDrnm.Size = new System.Drawing.Size(69, 21);
            this.txtDrnm.TabIndex = 22;
            // 
            // txtDrid
            // 
            this.txtDrid.Location = new System.Drawing.Point(69, 31);
            this.txtDrid.Name = "txtDrid";
            this.txtDrid.Size = new System.Drawing.Size(67, 21);
            this.txtDrid.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "의사ID :";
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(137, 55);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(69, 21);
            this.txtTodt.TabIndex = 25;
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(69, 55);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(67, 21);
            this.txtFrdt.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "기간 :";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(70, 80);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(136, 21);
            this.txtMemo.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "메모 :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(33, 124);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(116, 123);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 23);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "취소";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(224, 31);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(45, 23);
            this.btnApply.TabIndex = 51;
            this.btnApply.Text = "<-";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // grdDoct
            // 
            this.grdDoct.Location = new System.Drawing.Point(281, 31);
            this.grdDoct.MainView = this.grdDoctView;
            this.grdDoct.Name = "grdDoct";
            this.grdDoct.Size = new System.Drawing.Size(263, 330);
            this.grdDoct.TabIndex = 52;
            this.grdDoct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDoctView});
            this.grdDoct.DoubleClick += new System.EventHandler(this.grdDoct_DoubleClick);
            // 
            // grdDoctView
            // 
            this.grdDoctView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDRID,
            this.gcDRNM,
            this.gridColumn2});
            this.grdDoctView.GridControl = this.grdDoct;
            this.grdDoctView.Name = "grdDoctView";
            this.grdDoctView.OptionsView.ColumnAutoWidth = false;
            this.grdDoctView.OptionsView.ShowGroupPanel = false;
            this.grdDoctView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDRID
            // 
            this.gcDRID.Caption = "의사ID";
            this.gcDRID.FieldName = "DRID";
            this.gcDRID.Name = "gcDRID";
            this.gcDRID.OptionsColumn.AllowEdit = false;
            this.gcDRID.Visible = true;
            this.gcDRID.VisibleIndex = 0;
            // 
            // gcDRNM
            // 
            this.gcDRNM.Caption = "의사명";
            this.gcDRNM.FieldName = "DRNM";
            this.gcDRNM.Name = "gcDRNM";
            this.gcDRNM.OptionsColumn.AllowEdit = false;
            this.gcDRNM.Visible = true;
            this.gcDRNM.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "진료과";
            this.gridColumn2.FieldName = "DPTCD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // grdDept
            // 
            this.grdDept.Location = new System.Drawing.Point(550, 31);
            this.grdDept.MainView = this.grdDeptView;
            this.grdDept.Name = "grdDept";
            this.grdDept.Size = new System.Drawing.Size(189, 330);
            this.grdDept.TabIndex = 53;
            this.grdDept.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDeptView});
            this.grdDept.Click += new System.EventHandler(this.grdDept_Click);
            // 
            // grdDeptView
            // 
            this.grdDeptView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDPTCD,
            this.gridColumn1});
            this.grdDeptView.GridControl = this.grdDept;
            this.grdDeptView.Name = "grdDeptView";
            this.grdDeptView.OptionsView.ColumnAutoWidth = false;
            this.grdDeptView.OptionsView.ShowGroupPanel = false;
            this.grdDeptView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDPTCD
            // 
            this.gcDPTCD.Caption = "진료과";
            this.gcDPTCD.FieldName = "DPTCD";
            this.gcDPTCD.Name = "gcDPTCD";
            this.gcDPTCD.OptionsColumn.AllowEdit = false;
            this.gcDPTCD.Visible = true;
            this.gcDPTCD.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "진료과명";
            this.gridColumn1.FieldName = "DPTNM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // chkDrExp
            // 
            this.chkDrExp.AutoSize = true;
            this.chkDrExp.Location = new System.Drawing.Point(283, 11);
            this.chkDrExp.Name = "chkDrExp";
            this.chkDrExp.Size = new System.Drawing.Size(96, 16);
            this.chkDrExp.TabIndex = 54;
            this.chkDrExp.Text = "퇴사의사포함";
            this.chkDrExp.UseVisualStyleBackColor = true;
            this.chkDrExp.CheckedChanged += new System.EventHandler(this.chkDrExp_CheckedChanged);
            // 
            // ADD9909E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 377);
            this.Controls.Add(this.chkDrExp);
            this.Controls.Add(this.grdDept);
            this.Controls.Add(this.grdDoct);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTodt);
            this.Controls.Add(this.txtFrdt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDrnm);
            this.Controls.Add(this.txtDrid);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD9909E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "신규등록(ADD9909E_1)";
            this.Load += new System.EventHandler(this.ADD9909E_1_Load);
            this.Activated += new System.EventHandler(this.ADD9909E_1_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdDoct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeptView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDrnm;
        private System.Windows.Forms.TextBox txtDrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        private DevExpress.XtraGrid.GridControl grdDoct;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDoctView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRID;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRNM;
        private DevExpress.XtraGrid.GridControl grdDept;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDeptView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTCD;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.CheckBox chkDrExp;
    }
}
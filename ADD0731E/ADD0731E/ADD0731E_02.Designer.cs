namespace ADD0731E
{
    partial class ADD0731E_02
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
            this.gcCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCODENAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbHan = new System.Windows.Forms.RadioButton();
            this.rbYang = new System.Windows.Forms.RadioButton();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.grdMain.Location = new System.Drawing.Point(5, 36);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(540, 342);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCODE,
            this.gcCODENAME});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcCODE
            // 
            this.gcCODE.Caption = "코드";
            this.gcCODE.FieldName = "CODE";
            this.gcCODE.Name = "gcCODE";
            this.gcCODE.OptionsColumn.AllowEdit = false;
            this.gcCODE.Visible = true;
            this.gcCODE.VisibleIndex = 0;
            this.gcCODE.Width = 90;
            // 
            // gcCODENAME
            // 
            this.gcCODENAME.Caption = "명칭";
            this.gcCODENAME.FieldName = "CODENAME";
            this.gcCODENAME.Name = "gcCODENAME";
            this.gcCODENAME.OptionsColumn.AllowEdit = false;
            this.gcCODENAME.Visible = true;
            this.gcCODENAME.VisibleIndex = 1;
            this.gcCODENAME.Width = 400;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbHan);
            this.panel1.Controls.Add(this.rbYang);
            this.panel1.Location = new System.Drawing.Point(9, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 23);
            this.panel1.TabIndex = 1;
            // 
            // rbHan
            // 
            this.rbHan.AutoSize = true;
            this.rbHan.Location = new System.Drawing.Point(56, 4);
            this.rbHan.Name = "rbHan";
            this.rbHan.Size = new System.Drawing.Size(47, 16);
            this.rbHan.TabIndex = 1;
            this.rbHan.Text = "한방";
            this.rbHan.UseVisualStyleBackColor = true;
            // 
            // rbYang
            // 
            this.rbYang.AutoSize = true;
            this.rbYang.Checked = true;
            this.rbYang.Location = new System.Drawing.Point(6, 4);
            this.rbYang.Name = "rbYang";
            this.rbYang.Size = new System.Drawing.Size(47, 16);
            this.rbYang.TabIndex = 0;
            this.rbYang.TabStop = true;
            this.rbYang.Text = "양방";
            this.rbYang.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(181, 8);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(86, 21);
            this.txtCode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "코드";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(468, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(322, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(111, 21);
            this.txtName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "코드명";
            // 
            // ADD0731E_02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 384);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD0731E_02";
            this.Text = "코드조회(ADD0731E_02)";
            this.Load += new System.EventHandler(this.ADD0731E_02_Load);
            this.Activated += new System.EventHandler(this.ADD0731E_02_Activated);
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
        private DevExpress.XtraGrid.Columns.GridColumn gcCODE;
        private DevExpress.XtraGrid.Columns.GridColumn gcCODENAME;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbHan;
        private System.Windows.Forms.RadioButton rbYang;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
    }
}
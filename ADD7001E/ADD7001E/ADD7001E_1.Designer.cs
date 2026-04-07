namespace ADD7001E
{
    partial class ADD7001E_1
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
            this.gcSORT_SNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcYADM_TRMN_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcYADM_TRMN_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDTL_TXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLABEL_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(669, 243);
            this.grdMain.TabIndex = 1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSORT_SNO,
            this.gcYADM_TRMN_ID,
            this.gcYADM_TRMN_NM,
            this.gcDTL_TXT,
            this.gcLABEL_NM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowGroup = false;
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdMainView_FocusedRowChanged);
            // 
            // gcSORT_SNO
            // 
            this.gcSORT_SNO.Caption = "순번";
            this.gcSORT_SNO.FieldName = "SORT_SNO";
            this.gcSORT_SNO.Name = "gcSORT_SNO";
            this.gcSORT_SNO.OptionsColumn.AllowEdit = false;
            this.gcSORT_SNO.Visible = true;
            this.gcSORT_SNO.VisibleIndex = 0;
            // 
            // gcYADM_TRMN_ID
            // 
            this.gcYADM_TRMN_ID.Caption = "항목코드";
            this.gcYADM_TRMN_ID.FieldName = "YADM_TRMN_ID";
            this.gcYADM_TRMN_ID.Name = "gcYADM_TRMN_ID";
            this.gcYADM_TRMN_ID.OptionsColumn.AllowEdit = false;
            this.gcYADM_TRMN_ID.Visible = true;
            this.gcYADM_TRMN_ID.VisibleIndex = 1;
            // 
            // gcYADM_TRMN_NM
            // 
            this.gcYADM_TRMN_NM.Caption = "항목코드명";
            this.gcYADM_TRMN_NM.FieldName = "YADM_TRMN_NM";
            this.gcYADM_TRMN_NM.Name = "gcYADM_TRMN_NM";
            this.gcYADM_TRMN_NM.OptionsColumn.AllowEdit = false;
            this.gcYADM_TRMN_NM.Visible = true;
            this.gcYADM_TRMN_NM.VisibleIndex = 2;
            // 
            // gcDTL_TXT
            // 
            this.gcDTL_TXT.Caption = "항목내용";
            this.gcDTL_TXT.FieldName = "DTL_TXT";
            this.gcDTL_TXT.Name = "gcDTL_TXT";
            this.gcDTL_TXT.OptionsColumn.AllowEdit = false;
            this.gcDTL_TXT.Visible = true;
            this.gcDTL_TXT.VisibleIndex = 3;
            this.gcDTL_TXT.Width = 300;
            // 
            // gcLABEL_NM
            // 
            this.gcLABEL_NM.Caption = "라벨명";
            this.gcLABEL_NM.FieldName = "LABEL_NM";
            this.gcLABEL_NM.Name = "gcLABEL_NM";
            this.gcLABEL_NM.OptionsColumn.AllowEdit = false;
            this.gcLABEL_NM.Visible = true;
            this.gcLABEL_NM.VisibleIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 279);
            this.panel1.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.grdMain);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 36);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(669, 243);
            this.panel6.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnPrev);
            this.panel5.Controls.Add(this.btnNext);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(669, 36);
            this.panel5.TabIndex = 0;
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Location = new System.Drawing.Point(513, 5);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(72, 25);
            this.btnPrev.TabIndex = 5;
            this.btnPrev.Text = "이전";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(586, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(72, 25);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "다음";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 279);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(669, 5);
            this.splitterControl1.TabIndex = 3;
            this.splitterControl1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 284);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(669, 220);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtMsg);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(669, 184);
            this.panel4.TabIndex = 4;
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.SystemColors.Window;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(669, 184);
            this.txtMsg.TabIndex = 2;
            this.txtMsg.TextChanged += new System.EventHandler(this.txtMsg_TextChanged);
            this.txtMsg.Leave += new System.EventHandler(this.txtMsg_Leave);
            this.txtMsg.Enter += new System.EventHandler(this.txtMsg_Enter);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 184);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(669, 36);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "수정하면 자동으로 저장됩니다.";
            // 
            // ADD7001E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ADD7001E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "자료조회(ADD7001E_1)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gcSORT_SNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcYADM_TRMN_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gcYADM_TRMN_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDTL_TXT;
        private DevExpress.XtraGrid.Columns.GridColumn gcLABEL_NM;
        public DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
    }
}
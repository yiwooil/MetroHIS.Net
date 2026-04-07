namespace ADD7003E
{
    partial class ADD7003E_1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.txtEprtno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grdDoc = new DevExpress.XtraGrid.GridControl();
            this.grdDocView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDOC_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcDOC_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStedt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBededt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtResid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panForm = new System.Windows.Forms.Panel();
            this.txtEnddt = new System.Windows.Forms.TextBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "청구번호";
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(65, 9);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.ReadOnly = true;
            this.txtDemno.Size = new System.Drawing.Size(75, 21);
            this.txtDemno.TabIndex = 1;
            // 
            // txtEprtno
            // 
            this.txtEprtno.Location = new System.Drawing.Point(195, 9);
            this.txtEprtno.Name = "txtEprtno";
            this.txtEprtno.ReadOnly = true;
            this.txtEprtno.Size = new System.Drawing.Size(47, 21);
            this.txtEprtno.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "명일련";
            // 
            // grdDoc
            // 
            this.grdDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDoc.Location = new System.Drawing.Point(11, 41);
            this.grdDoc.MainView = this.grdDocView;
            this.grdDoc.Name = "grdDoc";
            this.grdDoc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEdit});
            this.grdDoc.Size = new System.Drawing.Size(235, 408);
            this.grdDoc.TabIndex = 4;
            this.grdDoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDocView,
            this.gridView2});
            this.grdDoc.DoubleClick += new System.EventHandler(this.grdDoc_DoubleClick);
            this.grdDoc.Click += new System.EventHandler(this.grdDoc_Click);
            // 
            // grdDocView
            // 
            this.grdDocView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDOC_NM,
            this.gridColumn1,
            this.gcDOC_CD});
            this.grdDocView.GridControl = this.grdDoc;
            this.grdDocView.Name = "grdDocView";
            this.grdDocView.OptionsCustomization.AllowSort = false;
            this.grdDocView.OptionsView.ColumnAutoWidth = false;
            this.grdDocView.OptionsView.ShowGroupPanel = false;
            this.grdDocView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcDOC_NM
            // 
            this.gcDOC_NM.Caption = "문서";
            this.gcDOC_NM.FieldName = "DOC_NM";
            this.gcDOC_NM.Name = "gcDOC_NM";
            this.gcDOC_NM.OptionsColumn.AllowEdit = false;
            this.gcDOC_NM.Visible = true;
            this.gcDOC_NM.VisibleIndex = 0;
            this.gcDOC_NM.Width = 165;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.btnEdit;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 33;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoHeight = false;
            this.btnEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEdit_ButtonClick);
            // 
            // gcDOC_CD
            // 
            this.gcDOC_CD.Caption = "문서코드";
            this.gcDOC_CD.FieldName = "DOC_CD";
            this.gcDOC_CD.Name = "gcDOC_CD";
            this.gcDOC_CD.OptionsColumn.AllowEdit = false;
            this.gcDOC_CD.Visible = true;
            this.gcDOC_CD.VisibleIndex = 2;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdDoc;
            this.gridView2.Name = "gridView2";
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(367, 9);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.ReadOnly = true;
            this.txtPnm.Size = new System.Drawing.Size(72, 21);
            this.txtPnm.TabIndex = 8;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(294, 9);
            this.txtPid.Name = "txtPid";
            this.txtPid.ReadOnly = true;
            this.txtPid.Size = new System.Drawing.Size(72, 21);
            this.txtPid.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "환자ID";
            // 
            // txtStedt
            // 
            this.txtStedt.Location = new System.Drawing.Point(629, 9);
            this.txtStedt.Name = "txtStedt";
            this.txtStedt.ReadOnly = true;
            this.txtStedt.Size = new System.Drawing.Size(70, 21);
            this.txtStedt.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(571, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "진료기간";
            // 
            // txtBededt
            // 
            this.txtBededt.Location = new System.Drawing.Point(492, 9);
            this.txtBededt.Name = "txtBededt";
            this.txtBededt.ReadOnly = true;
            this.txtBededt.Size = new System.Drawing.Size(70, 21);
            this.txtBededt.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(449, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "입원일";
            // 
            // txtResid
            // 
            this.txtResid.Location = new System.Drawing.Point(866, 9);
            this.txtResid.Name = "txtResid";
            this.txtResid.ReadOnly = true;
            this.txtResid.Size = new System.Drawing.Size(103, 21);
            this.txtResid.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(784, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "주민등록번호";
            // 
            // panForm
            // 
            this.panForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panForm.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panForm.Location = new System.Drawing.Point(252, 41);
            this.panForm.Name = "panForm";
            this.panForm.Padding = new System.Windows.Forms.Padding(3);
            this.panForm.Size = new System.Drawing.Size(820, 409);
            this.panForm.TabIndex = 15;
            // 
            // txtEnddt
            // 
            this.txtEnddt.Location = new System.Drawing.Point(703, 9);
            this.txtEnddt.Name = "txtEnddt";
            this.txtEnddt.ReadOnly = true;
            this.txtEnddt.Size = new System.Drawing.Size(70, 21);
            this.txtEnddt.TabIndex = 16;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(975, 7);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(49, 23);
            this.btnPrev.TabIndex = 17;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(1023, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(49, 23);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ADD7003E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 462);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.txtEnddt);
            this.Controls.Add(this.panForm);
            this.Controls.Add(this.txtResid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBededt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStedt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grdDoc);
            this.Controls.Add(this.txtEprtno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label1);
            this.Name = "ADD7003E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "자료등록(ADD7003E_1)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ADD7003E_1_Load);
            this.Activated += new System.EventHandler(this.ADD7003E_1_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.TextBox txtEprtno;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdDoc;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDocView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gcDOC_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDOC_CD;
        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStedt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBededt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panForm;
        private System.Windows.Forms.TextBox txtEnddt;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
    }
}
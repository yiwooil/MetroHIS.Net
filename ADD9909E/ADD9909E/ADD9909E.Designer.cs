namespace ADD9909E
{
    partial class ADD9909E
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDRID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFRDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTODATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMEMO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNHDID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtDrnm = new System.Windows.Forms.TextBox();
            this.txtDrid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 40);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(536, 374);
            this.grdMain.TabIndex = 2;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDRID,
            this.gcDRNM,
            this.gcFRDATE,
            this.gcTODATE,
            this.gcMEMO,
            this.gcNHDID});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grdMainView_ValidatingEditor);
            this.grdMainView.ShownEditor += new System.EventHandler(this.grdMainView_ShownEditor);
            this.grdMainView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.grdMainView_ShowingEditor);
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
            this.gcDRNM.OptionsColumn.ReadOnly = true;
            this.gcDRNM.Visible = true;
            this.gcDRNM.VisibleIndex = 1;
            // 
            // gcFRDATE
            // 
            this.gcFRDATE.Caption = "시작일";
            this.gcFRDATE.FieldName = "FRDATE";
            this.gcFRDATE.Name = "gcFRDATE";
            this.gcFRDATE.Visible = true;
            this.gcFRDATE.VisibleIndex = 2;
            // 
            // gcTODATE
            // 
            this.gcTODATE.Caption = "종료일";
            this.gcTODATE.FieldName = "TODATE";
            this.gcTODATE.Name = "gcTODATE";
            this.gcTODATE.Visible = true;
            this.gcTODATE.VisibleIndex = 3;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "메모";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 4;
            this.gcMEMO.Width = 200;
            // 
            // gcNHDID
            // 
            this.gcNHDID.Caption = "NHDID";
            this.gcNHDID.FieldName = "NHDID";
            this.gcNHDID.Name = "gcNHDID";
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(172, 11);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(79, 23);
            this.btnDel.TabIndex = 46;
            this.btnDel.Text = "행삭제";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 45;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "시작일, 종료일, 메모는 입력하면 바로 저장됩니다.";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(92, 11);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(79, 23);
            this.btnNew.TabIndex = 48;
            this.btnNew.Text = "신규";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtDrnm
            // 
            this.txtDrnm.Location = new System.Drawing.Point(430, 12);
            this.txtDrnm.Name = "txtDrnm";
            this.txtDrnm.Size = new System.Drawing.Size(69, 21);
            this.txtDrnm.TabIndex = 51;
            // 
            // txtDrid
            // 
            this.txtDrid.Location = new System.Drawing.Point(307, 12);
            this.txtDrid.Name = "txtDrid";
            this.txtDrid.Size = new System.Drawing.Size(67, 21);
            this.txtDrid.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "의사ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "의사명 :";
            // 
            // ADD9909E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 438);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDrnm);
            this.Controls.Add(this.txtDrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD9909E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "의사부재기간등록(ADD9909E)";
            this.Load += new System.EventHandler(this.ADD9909E_Load);
            this.Activated += new System.EventHandler(this.ADD9909E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRID;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcFRDATE;
        private DevExpress.XtraGrid.Columns.GridColumn gcTODATE;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcNHDID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtDrnm;
        private System.Windows.Forms.TextBox txtDrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


namespace ADD9907E
{
    partial class ADD9907E
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDACD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDANM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFRAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTOAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(93, 11);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(79, 23);
            this.btnDel.TabIndex = 44;
            this.btnDel.Text = "행삭제";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(11, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 43;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 40);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(532, 335);
            this.grdMain.TabIndex = 45;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDACD,
            this.gcDANM,
            this.gcFRAGE,
            this.gcTOAGE});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grdMainView_ValidatingEditor);
            this.grdMainView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.grdMainView_ShowingEditor);
            // 
            // gcDACD
            // 
            this.gcDACD.Caption = "상병";
            this.gcDACD.FieldName = "DACD";
            this.gcDACD.Name = "gcDACD";
            this.gcDACD.Visible = true;
            this.gcDACD.VisibleIndex = 0;
            // 
            // gcDANM
            // 
            this.gcDANM.Caption = "명칭";
            this.gcDANM.FieldName = "DANM";
            this.gcDANM.Name = "gcDANM";
            this.gcDANM.OptionsColumn.ReadOnly = true;
            this.gcDANM.Visible = true;
            this.gcDANM.VisibleIndex = 1;
            this.gcDANM.Width = 300;
            // 
            // gcFRAGE
            // 
            this.gcFRAGE.Caption = "시작나이";
            this.gcFRAGE.FieldName = "FRAGE";
            this.gcFRAGE.Name = "gcFRAGE";
            this.gcFRAGE.Visible = true;
            this.gcFRAGE.VisibleIndex = 2;
            this.gcFRAGE.Width = 60;
            // 
            // gcTOAGE
            // 
            this.gcTOAGE.Caption = "종료나이";
            this.gcTOAGE.FieldName = "TOAGE";
            this.gcTOAGE.Name = "gcTOAGE";
            this.gcTOAGE.Visible = true;
            this.gcTOAGE.VisibleIndex = 3;
            this.gcTOAGE.Width = 60;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(395, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "입력하면 바로 저장됩니다.";
            // 
            // ADD9907E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 387);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnQuery);
            this.Name = "ADD9907E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "상병별연령관리(ADD9907E)";
            this.Load += new System.EventHandler(this.ADD9907E_Load);
            this.Activated += new System.EventHandler(this.ADD9907E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcDACD;
        private DevExpress.XtraGrid.Columns.GridColumn gcDANM;
        private DevExpress.XtraGrid.Columns.GridColumn gcFRAGE;
        private DevExpress.XtraGrid.Columns.GridColumn gcTOAGE;
        private System.Windows.Forms.Label label1;
    }
}


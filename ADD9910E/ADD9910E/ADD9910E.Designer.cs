namespace ADD9910E
{
    partial class ADD9910E
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSEQNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcKORNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcENGNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtExdt = new System.Windows.Forms.TextBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(140, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 47;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Location = new System.Drawing.Point(49, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(86, 21);
            this.txtCode.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "코드 :";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(10, 41);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(436, 353);
            this.grdMain.TabIndex = 48;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSEQNO,
            this.gcCODE,
            this.gcKORNM,
            this.gcENGNM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcSEQNO
            // 
            this.gcSEQNO.Caption = "순번";
            this.gcSEQNO.FieldName = "SEQNO";
            this.gcSEQNO.Name = "gcSEQNO";
            this.gcSEQNO.OptionsColumn.ReadOnly = true;
            this.gcSEQNO.Visible = true;
            this.gcSEQNO.VisibleIndex = 0;
            // 
            // gcCODE
            // 
            this.gcCODE.Caption = "코드";
            this.gcCODE.FieldName = "CODE";
            this.gcCODE.Name = "gcCODE";
            this.gcCODE.OptionsColumn.ReadOnly = true;
            this.gcCODE.Visible = true;
            this.gcCODE.VisibleIndex = 1;
            // 
            // gcKORNM
            // 
            this.gcKORNM.Caption = "명칭";
            this.gcKORNM.FieldName = "KORNM";
            this.gcKORNM.Name = "gcKORNM";
            this.gcKORNM.OptionsColumn.ReadOnly = true;
            this.gcKORNM.Visible = true;
            this.gcKORNM.VisibleIndex = 2;
            this.gcKORNM.Width = 250;
            // 
            // gcENGNM
            // 
            this.gcENGNM.Caption = "영문명";
            this.gcENGNM.FieldName = "ENGNM";
            this.gcENGNM.Name = "gcENGNM";
            this.gcENGNM.OptionsColumn.ReadOnly = true;
            this.gcENGNM.Visible = true;
            this.gcENGNM.VisibleIndex = 3;
            this.gcENGNM.Width = 250;
            // 
            // txtExdt
            // 
            this.txtExdt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExdt.Location = new System.Drawing.Point(12, 400);
            this.txtExdt.Name = "txtExdt";
            this.txtExdt.Size = new System.Drawing.Size(86, 21);
            this.txtExdt.TabIndex = 49;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(280, 11);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(28, 23);
            this.btnUp.TabIndex = 50;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDn
            // 
            this.btnDn.Location = new System.Drawing.Point(308, 11);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(28, 23);
            this.btnDn.TabIndex = 51;
            this.btnDn.Text = "↓";
            this.btnDn.UseVisualStyleBackColor = true;
            this.btnDn.Click += new System.EventHandler(this.btnDn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "우선순위 조정시 바로 저장됩니다.";
            // 
            // ADD9910E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 428);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDn);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.txtExdt);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label3);
            this.Name = "ADD9910E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "청구상병우선순위조정(ADD9910E)";
            this.Load += new System.EventHandler(this.ADD9910E_Load);
            this.Activated += new System.EventHandler(this.ADD9910E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSEQNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcCODE;
        private DevExpress.XtraGrid.Columns.GridColumn gcKORNM;
        private System.Windows.Forms.TextBox txtExdt;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDn;
        private DevExpress.XtraGrid.Columns.GridColumn gcENGNM;
        private System.Windows.Forms.Label label1;
    }
}


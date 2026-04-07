namespace ADD0116Q
{
    partial class ADD0116Q
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
            this.gcKIND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXPCNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXPDTM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEXPRSN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEMPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 34);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(597, 243);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcKIND,
            this.gcEXPCNT,
            this.gcEXPDTM,
            this.gcEXPRSN,
            this.gcEMPNM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcKIND
            // 
            this.gcKIND.Caption = "종류";
            this.gcKIND.FieldName = "KIND";
            this.gcKIND.Name = "gcKIND";
            this.gcKIND.Visible = true;
            this.gcKIND.VisibleIndex = 0;
            this.gcKIND.Width = 160;
            // 
            // gcEXPCNT
            // 
            this.gcEXPCNT.AppearanceCell.Options.UseTextOptions = true;
            this.gcEXPCNT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcEXPCNT.Caption = "대상건수";
            this.gcEXPCNT.FieldName = "EXPCNT";
            this.gcEXPCNT.Name = "gcEXPCNT";
            this.gcEXPCNT.Visible = true;
            this.gcEXPCNT.VisibleIndex = 1;
            // 
            // gcEXPDTM
            // 
            this.gcEXPDTM.Caption = "파기일시";
            this.gcEXPDTM.FieldName = "EXPDTM";
            this.gcEXPDTM.Name = "gcEXPDTM";
            this.gcEXPDTM.Visible = true;
            this.gcEXPDTM.VisibleIndex = 2;
            this.gcEXPDTM.Width = 120;
            // 
            // gcEXPRSN
            // 
            this.gcEXPRSN.Caption = "파기사유";
            this.gcEXPRSN.FieldName = "EXPRSN";
            this.gcEXPRSN.Name = "gcEXPRSN";
            this.gcEXPRSN.Visible = true;
            this.gcEXPRSN.VisibleIndex = 3;
            this.gcEXPRSN.Width = 120;
            // 
            // gcEMPNM
            // 
            this.gcEMPNM.Caption = "담당자";
            this.gcEMPNM.FieldName = "EMPNM";
            this.gcEMPNM.Name = "gcEMPNM";
            this.gcEMPNM.Visible = true;
            this.gcEMPNM.VisibleIndex = 4;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(530, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 30;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(449, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 29;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ADD0116Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 286);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Name = "ADD0116Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "개인정보파기대장(ADD0116Q)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcKIND;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXPCNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXPDTM;
        private DevExpress.XtraGrid.Columns.GridColumn gcEXPRSN;
        private DevExpress.XtraGrid.Columns.GridColumn gcEMPNM;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
    }
}


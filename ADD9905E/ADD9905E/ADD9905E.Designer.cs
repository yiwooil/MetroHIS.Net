namespace ADD9905E
{
    partial class ADD9905E
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
            this.cboMyList = new System.Windows.Forms.ComboBox();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMST3CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCDNM = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMyList
            // 
            this.cboMyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMyList.FormattingEnabled = true;
            this.cboMyList.Location = new System.Drawing.Point(12, 12);
            this.cboMyList.Name = "cboMyList";
            this.cboMyList.Size = new System.Drawing.Size(260, 20);
            this.cboMyList.TabIndex = 0;
            this.cboMyList.SelectedIndexChanged += new System.EventHandler(this.cboMyList_SelectedIndexChanged);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 38);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(613, 306);
            this.grdMain.TabIndex = 1;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMST3CD,
            this.gcCDNM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcMST3CD
            // 
            this.gcMST3CD.Caption = "코드";
            this.gcMST3CD.FieldName = "MST3CD";
            this.gcMST3CD.Name = "gcMST3CD";
            this.gcMST3CD.OptionsColumn.ReadOnly = true;
            this.gcMST3CD.Visible = true;
            this.gcMST3CD.VisibleIndex = 0;
            // 
            // gcCDNM
            // 
            this.gcCDNM.Caption = "코드명";
            this.gcCDNM.FieldName = "CDNM";
            this.gcCDNM.Name = "gcCDNM";
            this.gcCDNM.OptionsColumn.ReadOnly = true;
            this.gcCDNM.Visible = true;
            this.gcCDNM.VisibleIndex = 1;
            this.gcCDNM.Width = 500;
            // 
            // ADD9905E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 356);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.cboMyList);
            this.Name = "ADD9905E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "기타코드조회(ADD9905E)";
            this.Load += new System.EventHandler(this.ADD9905E_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMyList;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcMST3CD;
        private DevExpress.XtraGrid.Columns.GridColumn gcCDNM;
    }
}


namespace ADD8004Q
{
    partial class ADD8004Q
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
            this.grdList = new DevExpress.XtraGrid.GridControl();
            this.grdListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcREPYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREPSEQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDPTNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcREQYM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREQSEQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREPDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcELINENO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMKDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINFODIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINFOSTMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREQSTMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUYQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREQDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDptnm = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.Location = new System.Drawing.Point(12, 12);
            this.grdList.MainView = this.grdListView;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(270, 136);
            this.grdList.TabIndex = 0;
            this.grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdListView});
            // 
            // grdListView
            // 
            this.grdListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcREPYM,
            this.gcREPSEQ,
            this.gcDPTNM});
            this.grdListView.GridControl = this.grdList;
            this.grdListView.Name = "grdListView";
            this.grdListView.OptionsView.ColumnAutoWidth = false;
            this.grdListView.OptionsView.ShowGroupPanel = false;
            this.grdListView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdListView.DoubleClick += new System.EventHandler(this.grdListView_DoubleClick);
            // 
            // gcREPYM
            // 
            this.gcREPYM.Caption = "통보일자";
            this.gcREPYM.FieldName = "REPYM";
            this.gcREPYM.Name = "gcREPYM";
            this.gcREPYM.OptionsColumn.AllowEdit = false;
            this.gcREPYM.Visible = true;
            this.gcREPYM.VisibleIndex = 0;
            // 
            // gcREPSEQ
            // 
            this.gcREPSEQ.Caption = "순번";
            this.gcREPSEQ.FieldName = "REPSEQ";
            this.gcREPSEQ.Name = "gcREPSEQ";
            this.gcREPSEQ.OptionsColumn.AllowEdit = false;
            this.gcREPSEQ.Visible = true;
            this.gcREPSEQ.VisibleIndex = 1;
            this.gcREPSEQ.Width = 50;
            // 
            // gcDPTNM
            // 
            this.gcDPTNM.Caption = "담당과";
            this.gcDPTNM.FieldName = "DPTNM";
            this.gcDPTNM.Name = "gcDPTNM";
            this.gcDPTNM.OptionsColumn.AllowEdit = false;
            this.gcDPTNM.Visible = true;
            this.gcDPTNM.VisibleIndex = 2;
            this.gcDPTNM.Width = 100;
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.BackColor = System.Drawing.SystemColors.Window;
            this.txtMemo.Location = new System.Drawing.Point(289, 39);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(618, 107);
            this.txtMemo.TabIndex = 1;
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 154);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(895, 311);
            this.grdMain.TabIndex = 2;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcREQYM,
            this.gcREQSEQ,
            this.gcREPDIV,
            this.gcELINENO,
            this.gcMKDIV,
            this.gcINFODIV,
            this.gcITEMCD,
            this.gcITEMNM,
            this.gcINFOSTMT,
            this.gcREQSTMT,
            this.gcBUYQTY,
            this.gcREQDIV});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcREQYM
            // 
            this.gcREQYM.Caption = "신청년월";
            this.gcREQYM.FieldName = "REQYM";
            this.gcREQYM.Name = "gcREQYM";
            this.gcREQYM.OptionsColumn.AllowEdit = false;
            this.gcREQYM.Visible = true;
            this.gcREQYM.VisibleIndex = 0;
            this.gcREQYM.Width = 60;
            // 
            // gcREQSEQ
            // 
            this.gcREQSEQ.Caption = "신청구분";
            this.gcREQSEQ.FieldName = "REQSEQ";
            this.gcREQSEQ.Name = "gcREQSEQ";
            this.gcREQSEQ.OptionsColumn.AllowEdit = false;
            this.gcREQSEQ.Visible = true;
            this.gcREQSEQ.VisibleIndex = 1;
            this.gcREQSEQ.Width = 60;
            // 
            // gcREPDIV
            // 
            this.gcREPDIV.Caption = "신고서구분";
            this.gcREPDIV.FieldName = "REPDIV";
            this.gcREPDIV.Name = "gcREPDIV";
            this.gcREPDIV.OptionsColumn.AllowEdit = false;
            this.gcREPDIV.Visible = true;
            this.gcREPDIV.VisibleIndex = 2;
            // 
            // gcELINENO
            // 
            this.gcELINENO.Caption = "줄번호";
            this.gcELINENO.FieldName = "ELINENO";
            this.gcELINENO.Name = "gcELINENO";
            this.gcELINENO.OptionsColumn.AllowEdit = false;
            this.gcELINENO.Visible = true;
            this.gcELINENO.VisibleIndex = 3;
            this.gcELINENO.Width = 60;
            // 
            // gcMKDIV
            // 
            this.gcMKDIV.Caption = "조제제제구분";
            this.gcMKDIV.FieldName = "MKDIV";
            this.gcMKDIV.Name = "gcMKDIV";
            this.gcMKDIV.OptionsColumn.AllowEdit = false;
            this.gcMKDIV.Visible = true;
            this.gcMKDIV.VisibleIndex = 4;
            this.gcMKDIV.Width = 80;
            // 
            // gcINFODIV
            // 
            this.gcINFODIV.Caption = "안내사항구분";
            this.gcINFODIV.FieldName = "INFODIV";
            this.gcINFODIV.Name = "gcINFODIV";
            this.gcINFODIV.OptionsColumn.AllowEdit = false;
            this.gcINFODIV.Visible = true;
            this.gcINFODIV.VisibleIndex = 5;
            this.gcINFODIV.Width = 80;
            // 
            // gcITEMCD
            // 
            this.gcITEMCD.Caption = "품목코드";
            this.gcITEMCD.FieldName = "ITEMCD";
            this.gcITEMCD.Name = "gcITEMCD";
            this.gcITEMCD.OptionsColumn.AllowEdit = false;
            this.gcITEMCD.Visible = true;
            this.gcITEMCD.VisibleIndex = 6;
            // 
            // gcITEMNM
            // 
            this.gcITEMNM.Caption = "품명";
            this.gcITEMNM.FieldName = "ITEMNM";
            this.gcITEMNM.Name = "gcITEMNM";
            this.gcITEMNM.OptionsColumn.AllowEdit = false;
            this.gcITEMNM.Visible = true;
            this.gcITEMNM.VisibleIndex = 7;
            // 
            // gcINFOSTMT
            // 
            this.gcINFOSTMT.Caption = "안내사항내역";
            this.gcINFOSTMT.FieldName = "INFOSTMT";
            this.gcINFOSTMT.Name = "gcINFOSTMT";
            this.gcINFOSTMT.OptionsColumn.AllowEdit = false;
            this.gcINFOSTMT.Visible = true;
            this.gcINFOSTMT.VisibleIndex = 8;
            this.gcINFOSTMT.Width = 80;
            // 
            // gcREQSTMT
            // 
            this.gcREQSTMT.Caption = "자료요청내역";
            this.gcREQSTMT.FieldName = "REQSTMT";
            this.gcREQSTMT.Name = "gcREQSTMT";
            this.gcREQSTMT.OptionsColumn.AllowEdit = false;
            this.gcREQSTMT.Visible = true;
            this.gcREQSTMT.VisibleIndex = 9;
            this.gcREQSTMT.Width = 80;
            // 
            // gcBUYQTY
            // 
            this.gcBUYQTY.Caption = "구입량";
            this.gcBUYQTY.FieldName = "BUYQTY";
            this.gcBUYQTY.Name = "gcBUYQTY";
            this.gcBUYQTY.OptionsColumn.AllowEdit = false;
            this.gcBUYQTY.Visible = true;
            this.gcBUYQTY.VisibleIndex = 10;
            this.gcBUYQTY.Width = 55;
            // 
            // gcREQDIV
            // 
            this.gcREQDIV.Caption = "자료요청구분";
            this.gcREQDIV.FieldName = "REQDIV";
            this.gcREQDIV.Name = "gcREQDIV";
            this.gcREQDIV.OptionsColumn.AllowEdit = false;
            this.gcREQDIV.Visible = true;
            this.gcREQDIV.VisibleIndex = 11;
            this.gcREQDIV.Width = 80;
            // 
            // txtDptnm
            // 
            this.txtDptnm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDptnm.BackColor = System.Drawing.SystemColors.Window;
            this.txtDptnm.Location = new System.Drawing.Point(289, 13);
            this.txtDptnm.Name = "txtDptnm";
            this.txtDptnm.ReadOnly = true;
            this.txtDptnm.Size = new System.Drawing.Size(617, 21);
            this.txtDptnm.TabIndex = 3;
            // 
            // ADD8004Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 477);
            this.Controls.Add(this.txtDptnm);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.grdList);
            this.Name = "ADD8004Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "산재보험구입신고관련사항안내(ADD8004Q)";
            this.Activated += new System.EventHandler(this.ADD8004Q_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdList;
        private DevExpress.XtraGrid.Views.Grid.GridView grdListView;
        private DevExpress.XtraGrid.Columns.GridColumn gcREPYM;
        private DevExpress.XtraGrid.Columns.GridColumn gcREPSEQ;
        private DevExpress.XtraGrid.Columns.GridColumn gcDPTNM;
        private System.Windows.Forms.TextBox txtMemo;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcREQYM;
        private DevExpress.XtraGrid.Columns.GridColumn gcREQSEQ;
        private DevExpress.XtraGrid.Columns.GridColumn gcREPDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcELINENO;
        private DevExpress.XtraGrid.Columns.GridColumn gcMKDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcINFODIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcINFOSTMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcREQSTMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUYQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcREQDIV;
        private System.Windows.Forms.TextBox txtDptnm;
    }
}


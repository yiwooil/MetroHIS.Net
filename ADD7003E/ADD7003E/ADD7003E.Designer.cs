namespace ADD7003E
{
    partial class ADD7003E
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTrmTpCd = new System.Windows.Forms.ComboBox();
            this.txtYmFrom = new System.Windows.Forms.TextBox();
            this.txtYmTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDrgReqDataTpCd = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRcvNo = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEditInGrdMain = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEM_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRCV_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSP_SNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREQ_DATA_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRCV_YR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBILL_SNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcINSUP_TP_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkTest = new System.Windows.Forms.CheckBox();
            this.txtDemno = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditInGrdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "조회기간";
            // 
            // cboTrmTpCd
            // 
            this.cboTrmTpCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrmTpCd.FormattingEnabled = true;
            this.cboTrmTpCd.Items.AddRange(new object[] {
            "접수년월",
            "진료년월",
            "요청년월"});
            this.cboTrmTpCd.Location = new System.Drawing.Point(65, 9);
            this.cboTrmTpCd.Name = "cboTrmTpCd";
            this.cboTrmTpCd.Size = new System.Drawing.Size(80, 20);
            this.cboTrmTpCd.TabIndex = 1;
            // 
            // txtYmFrom
            // 
            this.txtYmFrom.Location = new System.Drawing.Point(150, 9);
            this.txtYmFrom.Name = "txtYmFrom";
            this.txtYmFrom.Size = new System.Drawing.Size(83, 21);
            this.txtYmFrom.TabIndex = 2;
            // 
            // txtYmTo
            // 
            this.txtYmTo.Location = new System.Drawing.Point(235, 9);
            this.txtYmTo.Name = "txtYmTo";
            this.txtYmTo.Size = new System.Drawing.Size(83, 21);
            this.txtYmTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "자료구분";
            // 
            // cboDrgReqDataTpCd
            // 
            this.cboDrgReqDataTpCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrgReqDataTpCd.FormattingEnabled = true;
            this.cboDrgReqDataTpCd.Items.AddRange(new object[] {
            "전체",
            "모니터링",
            "분리청구",
            "행위별청구",
            "중복청구"});
            this.cboDrgReqDataTpCd.Location = new System.Drawing.Point(379, 9);
            this.cboDrgReqDataTpCd.Name = "cboDrgReqDataTpCd";
            this.cboDrgReqDataTpCd.Size = new System.Drawing.Size(119, 20);
            this.cboDrgReqDataTpCd.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "접수번호";
            // 
            // txtRcvNo
            // 
            this.txtRcvNo.Location = new System.Drawing.Point(576, 8);
            this.txtRcvNo.Name = "txtRcvNo";
            this.txtRcvNo.Size = new System.Drawing.Size(83, 21);
            this.txtRcvNo.TabIndex = 7;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(816, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 42);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnEditInGrdMain});
            this.grdMain.Size = new System.Drawing.Size(960, 411);
            this.grdMain.TabIndex = 9;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gcDEM_NO,
            this.gcRCV_NO,
            this.gcSP_SNO,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gcREQ_DATA_NO,
            this.gcRCV_YR,
            this.gcBILL_SNO,
            this.gcINSUP_TP_CD});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "자료등록";
            this.gridColumn13.ColumnEdit = this.btnEditInGrdMain;
            this.gridColumn13.FieldName = "EDIT_BUTTON";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 13;
            // 
            // btnEditInGrdMain
            // 
            this.btnEditInGrdMain.AutoHeight = false;
            this.btnEditInGrdMain.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right, "등록", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnEditInGrdMain.Name = "btnEditInGrdMain";
            this.btnEditInGrdMain.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnEditInGrdMain.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditInGrdMain_ButtonClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "순번";
            this.gridColumn1.FieldName = "RN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "구분";
            this.gridColumn2.FieldName = "DRG_REQ_DATA_TP_CD_NM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "지불제도";
            this.gridColumn3.FieldName = "PSYS_TP_CD_NM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "보험자";
            this.gridColumn4.FieldName = "INSUP_TP_CD_NM";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "접수일자";
            this.gridColumn5.FieldName = "RCV_DD";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gcDEM_NO
            // 
            this.gcDEM_NO.Caption = "청구번호";
            this.gcDEM_NO.FieldName = "DMD_NO";
            this.gcDEM_NO.Name = "gcDEM_NO";
            this.gcDEM_NO.OptionsColumn.ReadOnly = true;
            this.gcDEM_NO.Visible = true;
            this.gcDEM_NO.VisibleIndex = 5;
            // 
            // gcRCV_NO
            // 
            this.gcRCV_NO.Caption = "접수번호";
            this.gcRCV_NO.FieldName = "RCV_NO";
            this.gcRCV_NO.Name = "gcRCV_NO";
            this.gcRCV_NO.OptionsColumn.ReadOnly = true;
            this.gcRCV_NO.Visible = true;
            this.gcRCV_NO.VisibleIndex = 6;
            // 
            // gcSP_SNO
            // 
            this.gcSP_SNO.Caption = "명일련";
            this.gcSP_SNO.FieldName = "SP_SNO";
            this.gcSP_SNO.Name = "gcSP_SNO";
            this.gcSP_SNO.OptionsColumn.ReadOnly = true;
            this.gcSP_SNO.Visible = true;
            this.gcSP_SNO.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "수진자명";
            this.gridColumn8.FieldName = "PAT_NM";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "등록번호";
            this.gridColumn9.FieldName = "HOSP_RNO";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "진료과목";
            this.gridColumn10.FieldName = "DGSBJT_CD_NM";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "요양개시일";
            this.gridColumn11.FieldName = "RECU_FR_DD";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "처리상태";
            this.gridColumn12.FieldName = "SMIT_YN";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 12;
            // 
            // gcREQ_DATA_NO
            // 
            this.gcREQ_DATA_NO.Caption = "자료요청번호";
            this.gcREQ_DATA_NO.FieldName = "REQ_DATA_NO";
            this.gcREQ_DATA_NO.Name = "gcREQ_DATA_NO";
            // 
            // gcRCV_YR
            // 
            this.gcRCV_YR.Caption = "접수년도";
            this.gcRCV_YR.FieldName = "RCV_YR";
            this.gcRCV_YR.Name = "gcRCV_YR";
            // 
            // gcBILL_SNO
            // 
            this.gcBILL_SNO.Caption = "청일련";
            this.gcBILL_SNO.FieldName = "BILL_SNO";
            this.gcBILL_SNO.Name = "gcBILL_SNO";
            // 
            // gcINSUP_TP_CD
            // 
            this.gcINSUP_TP_CD.Caption = "보험자코드";
            this.gcINSUP_TP_CD.FieldName = "INSUP_TP_CD";
            this.gcINSUP_TP_CD.Name = "gcINSUP_TP_CD";
            // 
            // chkTest
            // 
            this.chkTest.AutoSize = true;
            this.chkTest.Location = new System.Drawing.Point(668, 13);
            this.chkTest.Name = "chkTest";
            this.chkTest.Size = new System.Drawing.Size(60, 16);
            this.chkTest.TabIndex = 10;
            this.chkTest.Text = "테스트";
            this.chkTest.UseVisualStyleBackColor = true;
            this.chkTest.CheckedChanged += new System.EventHandler(this.chkTest_CheckedChanged);
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(727, 10);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(83, 21);
            this.txtDemno.TabIndex = 11;
            // 
            // ADD7003E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.chkTest);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtRcvNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboDrgReqDataTpCd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYmTo);
            this.Controls.Add(this.txtYmFrom);
            this.Controls.Add(this.cboTrmTpCd);
            this.Controls.Add(this.label1);
            this.Name = "ADD7003E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "신포괄모니터링자료제출(ADD7003E)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ADD7003E_Load);
            this.Activated += new System.EventHandler(this.ADD7003E_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditInGrdMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTrmTpCd;
        private System.Windows.Forms.TextBox txtYmFrom;
        private System.Windows.Forms.TextBox txtYmTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDrgReqDataTpCd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRcvNo;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gcRCV_NO;
        private DevExpress.XtraGrid.Columns.GridColumn gcSP_SNO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEM_NO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEditInGrdMain;
        private DevExpress.XtraGrid.Columns.GridColumn gcREQ_DATA_NO;
        private DevExpress.XtraGrid.Columns.GridColumn gcRCV_YR;
        private DevExpress.XtraGrid.Columns.GridColumn gcBILL_SNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcINSUP_TP_CD;
        private System.Windows.Forms.CheckBox chkTest;
        private System.Windows.Forms.TextBox txtDemno;
    }
}


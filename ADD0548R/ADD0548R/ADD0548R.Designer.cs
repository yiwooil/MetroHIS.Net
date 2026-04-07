namespace ADD0548R
{
    partial class ADD0548R
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbIofg2 = new System.Windows.Forms.RadioButton();
            this.rbIofg1 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbQfy29 = new System.Windows.Forms.RadioButton();
            this.rbQfy38 = new System.Windows.Forms.RadioButton();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy3 = new System.Windows.Forms.RadioButton();
            this.rbQfy5 = new System.Windows.Forms.RadioButton();
            this.rbQfy2 = new System.Windows.Forms.RadioButton();
            this.txtYYMMto = new System.Windows.Forms.TextBox();
            this.txtYYMMfr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cboDemgb = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbIofg2);
            this.panel2.Controls.Add(this.rbIofg1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(118, 27);
            this.panel2.TabIndex = 11;
            // 
            // rbIofg2
            // 
            this.rbIofg2.AutoSize = true;
            this.rbIofg2.Location = new System.Drawing.Point(57, 4);
            this.rbIofg2.Name = "rbIofg2";
            this.rbIofg2.Size = new System.Drawing.Size(47, 16);
            this.rbIofg2.TabIndex = 1;
            this.rbIofg2.Text = "입원";
            this.rbIofg2.UseVisualStyleBackColor = true;
            this.rbIofg2.CheckedChanged += new System.EventHandler(this.rbIofg2_CheckedChanged);
            // 
            // rbIofg1
            // 
            this.rbIofg1.AutoSize = true;
            this.rbIofg1.Checked = true;
            this.rbIofg1.Location = new System.Drawing.Point(8, 4);
            this.rbIofg1.Name = "rbIofg1";
            this.rbIofg1.Size = new System.Drawing.Size(47, 16);
            this.rbIofg1.TabIndex = 0;
            this.rbIofg1.TabStop = true;
            this.rbIofg1.Text = "외래";
            this.rbIofg1.UseVisualStyleBackColor = true;
            this.rbIofg1.CheckedChanged += new System.EventHandler(this.rbIofg1_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbQfy29);
            this.panel3.Controls.Add(this.rbQfy38);
            this.panel3.Controls.Add(this.rbQfy6);
            this.panel3.Controls.Add(this.rbQfy3);
            this.panel3.Controls.Add(this.rbQfy5);
            this.panel3.Controls.Add(this.rbQfy2);
            this.panel3.Location = new System.Drawing.Point(132, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(427, 27);
            this.panel3.TabIndex = 12;
            // 
            // rbQfy29
            // 
            this.rbQfy29.AutoSize = true;
            this.rbQfy29.Location = new System.Drawing.Point(339, 4);
            this.rbQfy29.Name = "rbQfy29";
            this.rbQfy29.Size = new System.Drawing.Size(71, 16);
            this.rbQfy29.TabIndex = 5;
            this.rbQfy29.Text = "보훈일반";
            this.rbQfy29.UseVisualStyleBackColor = true;
            // 
            // rbQfy38
            // 
            this.rbQfy38.AutoSize = true;
            this.rbQfy38.Location = new System.Drawing.Point(250, 4);
            this.rbQfy38.Name = "rbQfy38";
            this.rbQfy38.Size = new System.Drawing.Size(83, 16);
            this.rbQfy38.TabIndex = 4;
            this.rbQfy38.Text = "보호정신과";
            this.rbQfy38.UseVisualStyleBackColor = true;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(194, 4);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 3;
            this.rbQfy6.Text = "자보";
            this.rbQfy6.UseVisualStyleBackColor = true;
            // 
            // rbQfy3
            // 
            this.rbQfy3.AutoSize = true;
            this.rbQfy3.Location = new System.Drawing.Point(91, 4);
            this.rbQfy3.Name = "rbQfy3";
            this.rbQfy3.Size = new System.Drawing.Size(47, 16);
            this.rbQfy3.TabIndex = 1;
            this.rbQfy3.Text = "보호";
            this.rbQfy3.UseVisualStyleBackColor = true;
            // 
            // rbQfy5
            // 
            this.rbQfy5.AutoSize = true;
            this.rbQfy5.Location = new System.Drawing.Point(142, 4);
            this.rbQfy5.Name = "rbQfy5";
            this.rbQfy5.Size = new System.Drawing.Size(47, 16);
            this.rbQfy5.TabIndex = 2;
            this.rbQfy5.Text = "산재";
            this.rbQfy5.UseVisualStyleBackColor = true;
            // 
            // rbQfy2
            // 
            this.rbQfy2.AutoSize = true;
            this.rbQfy2.Checked = true;
            this.rbQfy2.Location = new System.Drawing.Point(10, 4);
            this.rbQfy2.Name = "rbQfy2";
            this.rbQfy2.Size = new System.Drawing.Size(75, 16);
            this.rbQfy2.TabIndex = 0;
            this.rbQfy2.TabStop = true;
            this.rbQfy2.Text = "보험,공상";
            this.rbQfy2.UseVisualStyleBackColor = true;
            // 
            // txtYYMMto
            // 
            this.txtYYMMto.Location = new System.Drawing.Point(137, 45);
            this.txtYYMMto.Name = "txtYYMMto";
            this.txtYYMMto.Size = new System.Drawing.Size(69, 21);
            this.txtYYMMto.TabIndex = 19;
            // 
            // txtYYMMfr
            // 
            this.txtYYMMfr.Location = new System.Drawing.Point(68, 45);
            this.txtYYMMfr.Name = "txtYYMMfr";
            this.txtYYMMfr.Size = new System.Drawing.Size(67, 21);
            this.txtYYMMfr.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "청구년월";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 72);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(830, 349);
            this.grdMain.TabIndex = 20;
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
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "No";
            this.gridColumn1.FieldName = "NO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "명일련";
            this.gridColumn2.FieldName = "EPRTNO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 55;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "환자명";
            this.gridColumn3.FieldName = "PNM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "주민번호";
            this.gridColumn4.FieldName = "RESID_FMT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 115;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "기관명";
            this.gridColumn5.FieldName = "REFNM";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "기관기호";
            this.gridColumn6.FieldName = "REFCD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "청구코드";
            this.gridColumn7.FieldName = "BGIHO";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "코드명";
            this.gridColumn8.FieldName = "PRKNM";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 155;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "일투";
            this.gridColumn9.DisplayFormat.FormatString = "#,###.#####";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn9.FieldName = "DQTY";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 55;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "총투";
            this.gridColumn10.DisplayFormat.FormatString = "#,###";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "DDAY";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 55;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(769, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 169;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(695, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 168;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cboDemgb
            // 
            this.cboDemgb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDemgb.FormattingEnabled = true;
            this.cboDemgb.Items.AddRange(new object[] {
            "전체",
            "원청구",
            "보완청구",
            "추가청구",
            "약제상한추가"});
            this.cboDemgb.Location = new System.Drawing.Point(423, 45);
            this.cboDemgb.Name = "cboDemgb";
            this.cboDemgb.Size = new System.Drawing.Size(136, 20);
            this.cboDemgb.TabIndex = 170;
            // 
            // ADD0548R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 433);
            this.Controls.Add(this.cboDemgb);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtYYMMto);
            this.Controls.Add(this.txtYYMMfr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "ADD0548R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "수탁환자명단(ADD0548R)";
            this.Load += new System.EventHandler(this.ADD0548R_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbIofg2;
        private System.Windows.Forms.RadioButton rbIofg1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbQfy29;
        private System.Windows.Forms.RadioButton rbQfy38;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.RadioButton rbQfy3;
        private System.Windows.Forms.RadioButton rbQfy5;
        private System.Windows.Forms.RadioButton rbQfy2;
        private System.Windows.Forms.TextBox txtYYMMto;
        private System.Windows.Forms.TextBox txtYYMMfr;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cboDemgb;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}


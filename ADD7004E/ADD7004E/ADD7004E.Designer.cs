namespace ADD7004E
{
    partial class ADD7004E
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtReqmm = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbYoyang = new System.Windows.Forms.RadioButton();
            this.rbPacare = new System.Windows.Forms.RadioButton();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSEQ1_NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJA_GUMAK1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJA_GUMAK2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJA_GS_GUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJA_TOT_GUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJR_GUMAK1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJR_GUMAK2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJR_GS_GUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJR_TOT_GUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCHA_GUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExcel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "청구월";
            // 
            // txtReqmm
            // 
            this.txtReqmm.Location = new System.Drawing.Point(59, 9);
            this.txtReqmm.Name = "txtReqmm";
            this.txtReqmm.Size = new System.Drawing.Size(79, 21);
            this.txtReqmm.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbYoyang);
            this.panel1.Controls.Add(this.rbPacare);
            this.panel1.Location = new System.Drawing.Point(282, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 24);
            this.panel1.TabIndex = 2;
            // 
            // rbYoyang
            // 
            this.rbYoyang.AutoSize = true;
            this.rbYoyang.Location = new System.Drawing.Point(82, 4);
            this.rbYoyang.Name = "rbYoyang";
            this.rbYoyang.Size = new System.Drawing.Size(71, 16);
            this.rbYoyang.TabIndex = 3;
            this.rbYoyang.TabStop = true;
            this.rbYoyang.Text = "요양환자";
            this.rbYoyang.UseVisualStyleBackColor = true;
            // 
            // rbPacare
            // 
            this.rbPacare.AutoSize = true;
            this.rbPacare.Checked = true;
            this.rbPacare.Location = new System.Drawing.Point(5, 4);
            this.rbPacare.Name = "rbPacare";
            this.rbPacare.Size = new System.Drawing.Size(71, 16);
            this.rbPacare.TabIndex = 2;
            this.rbPacare.TabStop = true;
            this.rbPacare.Text = "완화의료";
            this.rbPacare.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(453, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(189, 9);
            this.txtPid.Name = "txtPid";
            this.txtPid.Size = new System.Drawing.Size(79, 21);
            this.txtPid.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "환자ID";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(609, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 36);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(836, 413);
            this.grdMain.TabIndex = 7;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdMainView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSEQ1_NM,
            this.gcJA_GUMAK1,
            this.gcJA_GUMAK2,
            this.gcJA_GS_GUM,
            this.gcJA_TOT_GUM,
            this.gcJR_GUMAK1,
            this.gcJR_GUMAK2,
            this.gcJR_GS_GUM,
            this.gcJR_TOT_GUM,
            this.gcCHA_GUM});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdMainView_RowCellStyle);
            // 
            // gcSEQ1_NM
            // 
            this.gcSEQ1_NM.Caption = "구분";
            this.gcSEQ1_NM.FieldName = "SEQ1_NM";
            this.gcSEQ1_NM.Name = "gcSEQ1_NM";
            this.gcSEQ1_NM.OptionsColumn.ReadOnly = true;
            this.gcSEQ1_NM.Visible = true;
            this.gcSEQ1_NM.VisibleIndex = 0;
            // 
            // gcJA_GUMAK1
            // 
            this.gcJA_GUMAK1.Caption = "정액 재료";
            this.gcJA_GUMAK1.DisplayFormat.FormatString = "#,###";
            this.gcJA_GUMAK1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJA_GUMAK1.FieldName = "JA_GUMAK1";
            this.gcJA_GUMAK1.Name = "gcJA_GUMAK1";
            this.gcJA_GUMAK1.OptionsColumn.ReadOnly = true;
            this.gcJA_GUMAK1.Visible = true;
            this.gcJA_GUMAK1.VisibleIndex = 1;
            // 
            // gcJA_GUMAK2
            // 
            this.gcJA_GUMAK2.Caption = "정액 행위";
            this.gcJA_GUMAK2.DisplayFormat.FormatString = "#,###";
            this.gcJA_GUMAK2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJA_GUMAK2.FieldName = "JA_GUMAK2";
            this.gcJA_GUMAK2.Name = "gcJA_GUMAK2";
            this.gcJA_GUMAK2.OptionsColumn.ReadOnly = true;
            this.gcJA_GUMAK2.Visible = true;
            this.gcJA_GUMAK2.VisibleIndex = 2;
            // 
            // gcJA_GS_GUM
            // 
            this.gcJA_GS_GUM.Caption = "정액 가산금";
            this.gcJA_GS_GUM.DisplayFormat.FormatString = "#,###";
            this.gcJA_GS_GUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJA_GS_GUM.FieldName = "JA_GS_GUM";
            this.gcJA_GS_GUM.Name = "gcJA_GS_GUM";
            this.gcJA_GS_GUM.OptionsColumn.ReadOnly = true;
            this.gcJA_GS_GUM.Visible = true;
            this.gcJA_GS_GUM.VisibleIndex = 3;
            // 
            // gcJA_TOT_GUM
            // 
            this.gcJA_TOT_GUM.Caption = "정액 합계";
            this.gcJA_TOT_GUM.DisplayFormat.FormatString = "#,###";
            this.gcJA_TOT_GUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJA_TOT_GUM.FieldName = "JA_TOT_GUM";
            this.gcJA_TOT_GUM.Name = "gcJA_TOT_GUM";
            this.gcJA_TOT_GUM.OptionsColumn.ReadOnly = true;
            this.gcJA_TOT_GUM.Visible = true;
            this.gcJA_TOT_GUM.VisibleIndex = 4;
            // 
            // gcJR_GUMAK1
            // 
            this.gcJR_GUMAK1.Caption = "행위별 재료";
            this.gcJR_GUMAK1.DisplayFormat.FormatString = "#,###";
            this.gcJR_GUMAK1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJR_GUMAK1.FieldName = "JR_GUMAK1";
            this.gcJR_GUMAK1.Name = "gcJR_GUMAK1";
            this.gcJR_GUMAK1.OptionsColumn.ReadOnly = true;
            this.gcJR_GUMAK1.Visible = true;
            this.gcJR_GUMAK1.VisibleIndex = 5;
            // 
            // gcJR_GUMAK2
            // 
            this.gcJR_GUMAK2.Caption = "행위별 행위";
            this.gcJR_GUMAK2.DisplayFormat.FormatString = "#,###";
            this.gcJR_GUMAK2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJR_GUMAK2.FieldName = "JR_GUMAK2";
            this.gcJR_GUMAK2.Name = "gcJR_GUMAK2";
            this.gcJR_GUMAK2.OptionsColumn.ReadOnly = true;
            this.gcJR_GUMAK2.Visible = true;
            this.gcJR_GUMAK2.VisibleIndex = 6;
            // 
            // gcJR_GS_GUM
            // 
            this.gcJR_GS_GUM.Caption = "행위별 가산금";
            this.gcJR_GS_GUM.DisplayFormat.FormatString = "#,###";
            this.gcJR_GS_GUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJR_GS_GUM.FieldName = "JR_GS_GUM";
            this.gcJR_GS_GUM.Name = "gcJR_GS_GUM";
            this.gcJR_GS_GUM.OptionsColumn.ReadOnly = true;
            this.gcJR_GS_GUM.Visible = true;
            this.gcJR_GS_GUM.VisibleIndex = 7;
            // 
            // gcJR_TOT_GUM
            // 
            this.gcJR_TOT_GUM.Caption = "행위별 합계";
            this.gcJR_TOT_GUM.DisplayFormat.FormatString = "#,###";
            this.gcJR_TOT_GUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcJR_TOT_GUM.FieldName = "JR_TOT_GUM";
            this.gcJR_TOT_GUM.Name = "gcJR_TOT_GUM";
            this.gcJR_TOT_GUM.OptionsColumn.ReadOnly = true;
            this.gcJR_TOT_GUM.Visible = true;
            this.gcJR_TOT_GUM.VisibleIndex = 8;
            // 
            // gcCHA_GUM
            // 
            this.gcCHA_GUM.Caption = "차액";
            this.gcCHA_GUM.DisplayFormat.FormatString = "#,###";
            this.gcCHA_GUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcCHA_GUM.FieldName = "CHA_GUM";
            this.gcCHA_GUM.Name = "gcCHA_GUM";
            this.gcCHA_GUM.OptionsColumn.ReadOnly = true;
            this.gcCHA_GUM.Visible = true;
            this.gcCHA_GUM.VisibleIndex = 9;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(529, 9);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(79, 23);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "엑셀";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // ADD7004E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 461);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtReqmm);
            this.Controls.Add(this.label1);
            this.Name = "ADD7004E";
            this.Text = "정액환자진료비분석(ADD7004E)";
            this.Load += new System.EventHandler(this.ADD7004E_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReqmm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPacare;
        private System.Windows.Forms.RadioButton rbYoyang;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcSEQ1_NM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJA_GUMAK1;
        private DevExpress.XtraGrid.Columns.GridColumn gcJA_GUMAK2;
        private DevExpress.XtraGrid.Columns.GridColumn gcJA_GS_GUM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJA_TOT_GUM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJR_GUMAK1;
        private DevExpress.XtraGrid.Columns.GridColumn gcJR_GUMAK2;
        private DevExpress.XtraGrid.Columns.GridColumn gcJR_GS_GUM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJR_TOT_GUM;
        private DevExpress.XtraGrid.Columns.GridColumn gcCHA_GUM;
        private System.Windows.Forms.Button btnExcel;
    }
}


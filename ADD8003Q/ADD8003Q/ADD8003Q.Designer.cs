namespace ADD8003Q
{
    partial class ADD8003Q
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD8003Q));
            this.txtAccno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCntno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEPRTNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSENDAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDELAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcELINENO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPRICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBGIHO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRECD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLINEAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLINEQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRepdt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDemcnttot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDemqytot = new System.Windows.Forms.TextBox();
            this.txtExamqytot = new System.Windows.Forms.TextBox();
            this.txtExamcnttot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPaynotqytot = new System.Windows.Forms.TextBox();
            this.txtPaynotcnttot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDelqytot = new System.Windows.Forms.TextBox();
            this.txtDelcnttot = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPayrsvqytot = new System.Windows.Forms.TextBox();
            this.txtPayrsvcnttot = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPayqytot = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAccno
            // 
            this.txtAccno.Location = new System.Drawing.Point(73, 12);
            this.txtAccno.Name = "txtAccno";
            this.txtAccno.Size = new System.Drawing.Size(70, 21);
            this.txtAccno.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "접수번호 :";
            // 
            // txtCntno
            // 
            this.txtCntno.Location = new System.Drawing.Point(197, 12);
            this.txtCntno.Name = "txtCntno";
            this.txtCntno.Size = new System.Drawing.Size(70, 21);
            this.txtCntno.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "차수 :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(934, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(853, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(271, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(8, 113);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(1002, 361);
            this.grdMain.TabIndex = 20;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEPRTNO,
            this.gcPNM,
            this.gcDEMAMT,
            this.gcSENDAMT,
            this.gcDELAMT,
            this.gcELINENO,
            this.gcPRICD,
            this.gcBGIHO,
            this.gcRECD,
            this.gcRMK,
            this.gcLINEAMT,
            this.gcLINEQTY});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gcEPRTNO
            // 
            this.gcEPRTNO.AppearanceCell.Options.UseTextOptions = true;
            this.gcEPRTNO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcEPRTNO.Caption = "명일련";
            this.gcEPRTNO.FieldName = "EPRTNO";
            this.gcEPRTNO.Name = "gcEPRTNO";
            this.gcEPRTNO.OptionsColumn.AllowEdit = false;
            this.gcEPRTNO.Visible = true;
            this.gcEPRTNO.VisibleIndex = 0;
            this.gcEPRTNO.Width = 50;
            // 
            // gcPNM
            // 
            this.gcPNM.Caption = "환자명";
            this.gcPNM.FieldName = "PNM";
            this.gcPNM.Name = "gcPNM";
            this.gcPNM.OptionsColumn.AllowEdit = false;
            this.gcPNM.Visible = true;
            this.gcPNM.VisibleIndex = 1;
            // 
            // gcDEMAMT
            // 
            this.gcDEMAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMAMT.Caption = "청구액";
            this.gcDEMAMT.FieldName = "DEMAMT";
            this.gcDEMAMT.Name = "gcDEMAMT";
            this.gcDEMAMT.OptionsColumn.AllowEdit = false;
            this.gcDEMAMT.Visible = true;
            this.gcDEMAMT.VisibleIndex = 2;
            // 
            // gcSENDAMT
            // 
            this.gcSENDAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcSENDAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcSENDAMT.Caption = "지급결정액";
            this.gcSENDAMT.FieldName = "SENDAMT";
            this.gcSENDAMT.Name = "gcSENDAMT";
            this.gcSENDAMT.OptionsColumn.AllowEdit = false;
            this.gcSENDAMT.Visible = true;
            this.gcSENDAMT.VisibleIndex = 3;
            // 
            // gcDELAMT
            // 
            this.gcDELAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDELAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDELAMT.Caption = "삭감금액";
            this.gcDELAMT.FieldName = "DELAMT";
            this.gcDELAMT.Name = "gcDELAMT";
            this.gcDELAMT.OptionsColumn.AllowEdit = false;
            this.gcDELAMT.Visible = true;
            this.gcDELAMT.VisibleIndex = 4;
            // 
            // gcELINENO
            // 
            this.gcELINENO.AppearanceCell.Options.UseTextOptions = true;
            this.gcELINENO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcELINENO.Caption = "줄번호";
            this.gcELINENO.FieldName = "ELINENO";
            this.gcELINENO.Name = "gcELINENO";
            this.gcELINENO.OptionsColumn.AllowEdit = false;
            this.gcELINENO.Visible = true;
            this.gcELINENO.VisibleIndex = 5;
            this.gcELINENO.Width = 45;
            // 
            // gcPRICD
            // 
            this.gcPRICD.Caption = "수가코드";
            this.gcPRICD.FieldName = "PRICD";
            this.gcPRICD.Name = "gcPRICD";
            this.gcPRICD.OptionsColumn.AllowEdit = false;
            this.gcPRICD.Visible = true;
            this.gcPRICD.VisibleIndex = 6;
            // 
            // gcBGIHO
            // 
            this.gcBGIHO.Caption = "EDI코드";
            this.gcBGIHO.FieldName = "BGIHO";
            this.gcBGIHO.Name = "gcBGIHO";
            this.gcBGIHO.OptionsColumn.AllowEdit = false;
            this.gcBGIHO.Visible = true;
            this.gcBGIHO.VisibleIndex = 7;
            // 
            // gcRECD
            // 
            this.gcRECD.Caption = "사유코드";
            this.gcRECD.FieldName = "RECD";
            this.gcRECD.Name = "gcRECD";
            this.gcRECD.OptionsColumn.AllowEdit = false;
            this.gcRECD.Visible = true;
            this.gcRECD.VisibleIndex = 8;
            this.gcRECD.Width = 55;
            // 
            // gcRMK
            // 
            this.gcRMK.Caption = "사유";
            this.gcRMK.FieldName = "RMK";
            this.gcRMK.Name = "gcRMK";
            this.gcRMK.OptionsColumn.AllowEdit = false;
            this.gcRMK.Visible = true;
            this.gcRMK.VisibleIndex = 9;
            this.gcRMK.Width = 200;
            // 
            // gcLINEAMT
            // 
            this.gcLINEAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcLINEAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcLINEAMT.Caption = "삭감금액";
            this.gcLINEAMT.FieldName = "LINEAMT";
            this.gcLINEAMT.Name = "gcLINEAMT";
            this.gcLINEAMT.OptionsColumn.AllowEdit = false;
            this.gcLINEAMT.Visible = true;
            this.gcLINEAMT.VisibleIndex = 10;
            // 
            // gcLINEQTY
            // 
            this.gcLINEQTY.AppearanceCell.Options.UseTextOptions = true;
            this.gcLINEQTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcLINEQTY.Caption = "삭감수량";
            this.gcLINEQTY.FieldName = "LINEQTY";
            this.gcLINEQTY.Name = "gcLINEQTY";
            this.gcLINEQTY.OptionsColumn.AllowEdit = false;
            this.gcLINEQTY.Visible = true;
            this.gcLINEQTY.VisibleIndex = 11;
            // 
            // txtRepdt
            // 
            this.txtRepdt.Location = new System.Drawing.Point(73, 40);
            this.txtRepdt.Name = "txtRepdt";
            this.txtRepdt.ReadOnly = true;
            this.txtRepdt.Size = new System.Drawing.Size(70, 21);
            this.txtRepdt.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "통보일자 :";
            // 
            // txtDemcnttot
            // 
            this.txtDemcnttot.Location = new System.Drawing.Point(197, 40);
            this.txtDemcnttot.Name = "txtDemcnttot";
            this.txtDemcnttot.ReadOnly = true;
            this.txtDemcnttot.Size = new System.Drawing.Size(70, 21);
            this.txtDemcnttot.TabIndex = 24;
            this.txtDemcnttot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "청구 :";
            // 
            // txtDemqytot
            // 
            this.txtDemqytot.Location = new System.Drawing.Point(268, 40);
            this.txtDemqytot.Name = "txtDemqytot";
            this.txtDemqytot.ReadOnly = true;
            this.txtDemqytot.Size = new System.Drawing.Size(112, 21);
            this.txtDemqytot.TabIndex = 25;
            this.txtDemqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtExamqytot
            // 
            this.txtExamqytot.Location = new System.Drawing.Point(508, 40);
            this.txtExamqytot.Name = "txtExamqytot";
            this.txtExamqytot.ReadOnly = true;
            this.txtExamqytot.Size = new System.Drawing.Size(112, 21);
            this.txtExamqytot.TabIndex = 28;
            this.txtExamqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtExamcnttot
            // 
            this.txtExamcnttot.Location = new System.Drawing.Point(436, 40);
            this.txtExamcnttot.Name = "txtExamcnttot";
            this.txtExamcnttot.ReadOnly = true;
            this.txtExamcnttot.Size = new System.Drawing.Size(70, 21);
            this.txtExamcnttot.TabIndex = 27;
            this.txtExamcnttot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "심사 :";
            // 
            // txtPaynotqytot
            // 
            this.txtPaynotqytot.Location = new System.Drawing.Point(748, 40);
            this.txtPaynotqytot.Name = "txtPaynotqytot";
            this.txtPaynotqytot.ReadOnly = true;
            this.txtPaynotqytot.Size = new System.Drawing.Size(112, 21);
            this.txtPaynotqytot.TabIndex = 31;
            this.txtPaynotqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPaynotcnttot
            // 
            this.txtPaynotcnttot.Location = new System.Drawing.Point(676, 40);
            this.txtPaynotcnttot.Name = "txtPaynotcnttot";
            this.txtPaynotcnttot.ReadOnly = true;
            this.txtPaynotcnttot.Size = new System.Drawing.Size(70, 21);
            this.txtPaynotcnttot.TabIndex = 30;
            this.txtPaynotcnttot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(638, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "불능 :";
            // 
            // txtDelqytot
            // 
            this.txtDelqytot.Location = new System.Drawing.Point(748, 63);
            this.txtDelqytot.Name = "txtDelqytot";
            this.txtDelqytot.ReadOnly = true;
            this.txtDelqytot.Size = new System.Drawing.Size(112, 21);
            this.txtDelqytot.TabIndex = 37;
            this.txtDelqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDelcnttot
            // 
            this.txtDelcnttot.Location = new System.Drawing.Point(676, 63);
            this.txtDelcnttot.Name = "txtDelcnttot";
            this.txtDelcnttot.ReadOnly = true;
            this.txtDelcnttot.Size = new System.Drawing.Size(70, 21);
            this.txtDelcnttot.TabIndex = 36;
            this.txtDelcnttot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(638, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "삭감 :";
            // 
            // txtPayrsvqytot
            // 
            this.txtPayrsvqytot.Location = new System.Drawing.Point(508, 63);
            this.txtPayrsvqytot.Name = "txtPayrsvqytot";
            this.txtPayrsvqytot.ReadOnly = true;
            this.txtPayrsvqytot.Size = new System.Drawing.Size(112, 21);
            this.txtPayrsvqytot.TabIndex = 34;
            this.txtPayrsvqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPayrsvcnttot
            // 
            this.txtPayrsvcnttot.Location = new System.Drawing.Point(436, 63);
            this.txtPayrsvcnttot.Name = "txtPayrsvcnttot";
            this.txtPayrsvcnttot.ReadOnly = true;
            this.txtPayrsvcnttot.Size = new System.Drawing.Size(70, 21);
            this.txtPayrsvcnttot.TabIndex = 33;
            this.txtPayrsvcnttot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(397, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "보류 :";
            // 
            // txtPayqytot
            // 
            this.txtPayqytot.Location = new System.Drawing.Point(268, 63);
            this.txtPayqytot.Name = "txtPayqytot";
            this.txtPayqytot.ReadOnly = true;
            this.txtPayqytot.Size = new System.Drawing.Size(112, 21);
            this.txtPayqytot.TabIndex = 39;
            this.txtPayqytot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(159, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "지급결정액 :";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(197, 87);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.Size = new System.Drawing.Size(813, 21);
            this.txtMemo.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(159, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 12);
            this.label10.TabIndex = 40;
            this.label10.Text = "비고 :";
            // 
            // ADD8003Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 480);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPayqytot);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDelqytot);
            this.Controls.Add(this.txtDelcnttot);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPayrsvqytot);
            this.Controls.Add(this.txtPayrsvcnttot);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPaynotqytot);
            this.Controls.Add(this.txtPaynotcnttot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtExamqytot);
            this.Controls.Add(this.txtExamcnttot);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDemqytot);
            this.Controls.Add(this.txtDemcnttot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRepdt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtCntno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAccno);
            this.Controls.Add(this.label1);
            this.Name = "ADD8003Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "산재보험진료비심사내역통보서(ADD8003Q)";
            this.Activated += new System.EventHandler(this.ADD8003Q_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAccno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCntno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcEPRTNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcPNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSENDAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDELAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcELINENO;
        private DevExpress.XtraGrid.Columns.GridColumn gcRECD;
        private DevExpress.XtraGrid.Columns.GridColumn gcRMK;
        private DevExpress.XtraGrid.Columns.GridColumn gcLINEAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcLINEQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gcPRICD;
        private DevExpress.XtraGrid.Columns.GridColumn gcBGIHO;
        private System.Windows.Forms.TextBox txtRepdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDemcnttot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDemqytot;
        private System.Windows.Forms.TextBox txtExamqytot;
        private System.Windows.Forms.TextBox txtExamcnttot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPaynotqytot;
        private System.Windows.Forms.TextBox txtPaynotcnttot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDelqytot;
        private System.Windows.Forms.TextBox txtDelcnttot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPayrsvqytot;
        private System.Windows.Forms.TextBox txtPayrsvcnttot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPayqytot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label10;
    }
}


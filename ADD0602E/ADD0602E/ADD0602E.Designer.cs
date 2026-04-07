namespace ADD0602E
{
    partial class ADD0602E
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
            this.panObj = new System.Windows.Forms.Panel();
            this.rbObjB = new System.Windows.Forms.RadioButton();
            this.rbObjA = new System.Windows.Forms.RadioButton();
            this.panQfy = new System.Windows.Forms.Panel();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy3 = new System.Windows.Forms.RadioButton();
            this.rbQfy2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCnecno = new System.Windows.Forms.TextBox();
            this.txtDcount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDemseq = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGrpno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReday = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHosnm = new System.Windows.Forms.TextBox();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.txtPhoneno = new System.Windows.Forms.TextBox();
            this.txtWorknm = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMake = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnFinishCancel = new System.Windows.Forms.Button();
            this.panObj.SuspendLayout();
            this.panQfy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "작업구분";
            // 
            // panObj
            // 
            this.panObj.Controls.Add(this.rbObjB);
            this.panObj.Controls.Add(this.rbObjA);
            this.panObj.Location = new System.Drawing.Point(67, 5);
            this.panObj.Name = "panObj";
            this.panObj.Size = new System.Drawing.Size(210, 24);
            this.panObj.TabIndex = 1;
            // 
            // rbObjB
            // 
            this.rbObjB.AutoSize = true;
            this.rbObjB.Checked = true;
            this.rbObjB.Location = new System.Drawing.Point(116, 4);
            this.rbObjB.Name = "rbObjB";
            this.rbObjB.Size = new System.Drawing.Size(71, 16);
            this.rbObjB.TabIndex = 1;
            this.rbObjB.TabStop = true;
            this.rbObjB.Text = "이의신청";
            this.rbObjB.UseVisualStyleBackColor = true;
            this.rbObjB.CheckedChanged += new System.EventHandler(this.rbObjB_CheckedChanged);
            // 
            // rbObjA
            // 
            this.rbObjA.AutoSize = true;
            this.rbObjA.Location = new System.Drawing.Point(3, 4);
            this.rbObjA.Name = "rbObjA";
            this.rbObjA.Size = new System.Drawing.Size(107, 16);
            this.rbObjA.TabIndex = 0;
            this.rbObjA.Text = "재심사조정청구";
            this.rbObjA.UseVisualStyleBackColor = true;
            this.rbObjA.CheckedChanged += new System.EventHandler(this.rbObjA_CheckedChanged);
            // 
            // panQfy
            // 
            this.panQfy.Controls.Add(this.rbQfy6);
            this.panQfy.Controls.Add(this.rbQfy3);
            this.panQfy.Controls.Add(this.rbQfy2);
            this.panQfy.Location = new System.Drawing.Point(349, 5);
            this.panQfy.Name = "panQfy";
            this.panQfy.Size = new System.Drawing.Size(210, 24);
            this.panQfy.TabIndex = 3;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(117, 4);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 2;
            this.rbQfy6.Text = "자보";
            this.rbQfy6.UseVisualStyleBackColor = true;
            // 
            // rbQfy3
            // 
            this.rbQfy3.AutoSize = true;
            this.rbQfy3.Location = new System.Drawing.Point(58, 4);
            this.rbQfy3.Name = "rbQfy3";
            this.rbQfy3.Size = new System.Drawing.Size(47, 16);
            this.rbQfy3.TabIndex = 1;
            this.rbQfy3.Text = "보호";
            this.rbQfy3.UseVisualStyleBackColor = true;
            // 
            // rbQfy2
            // 
            this.rbQfy2.AutoSize = true;
            this.rbQfy2.Checked = true;
            this.rbQfy2.Location = new System.Drawing.Point(3, 4);
            this.rbQfy2.Name = "rbQfy2";
            this.rbQfy2.Size = new System.Drawing.Size(47, 16);
            this.rbQfy2.TabIndex = 0;
            this.rbQfy2.TabStop = true;
            this.rbQfy2.Text = "보험";
            this.rbQfy2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "자격구분";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "접수번호";
            // 
            // txtCnecno
            // 
            this.txtCnecno.Location = new System.Drawing.Point(67, 32);
            this.txtCnecno.Name = "txtCnecno";
            this.txtCnecno.Size = new System.Drawing.Size(100, 21);
            this.txtCnecno.TabIndex = 5;
            // 
            // txtDcount
            // 
            this.txtDcount.Location = new System.Drawing.Point(406, 32);
            this.txtDcount.Name = "txtDcount";
            this.txtDcount.Size = new System.Drawing.Size(100, 21);
            this.txtDcount.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "청일련";
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(67, 57);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(100, 21);
            this.txtDemno.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "청구번호";
            // 
            // txtDemseq
            // 
            this.txtDemseq.Location = new System.Drawing.Point(236, 32);
            this.txtDemseq.Name = "txtDemseq";
            this.txtDemseq.Size = new System.Drawing.Size(100, 21);
            this.txtDemseq.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(177, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "심사차수";
            // 
            // txtGrpno
            // 
            this.txtGrpno.Location = new System.Drawing.Point(236, 57);
            this.txtGrpno.Name = "txtGrpno";
            this.txtGrpno.Size = new System.Drawing.Size(100, 21);
            this.txtGrpno.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "묶음번호";
            // 
            // txtReday
            // 
            this.txtReday.Location = new System.Drawing.Point(406, 57);
            this.txtReday.Name = "txtReday";
            this.txtReday.Size = new System.Drawing.Size(100, 21);
            this.txtReday.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "도달일자";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(823, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHosid.Location = new System.Drawing.Point(67, 512);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(100, 21);
            this.txtHosid.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 517);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "요양기관";
            // 
            // txtHosnm
            // 
            this.txtHosnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHosnm.Location = new System.Drawing.Point(168, 512);
            this.txtHosnm.Name = "txtHosnm";
            this.txtHosnm.Size = new System.Drawing.Size(100, 21);
            this.txtHosnm.TabIndex = 19;
            // 
            // txtAddr
            // 
            this.txtAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddr.Location = new System.Drawing.Point(269, 512);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(629, 21);
            this.txtAddr.TabIndex = 20;
            // 
            // txtPhoneno
            // 
            this.txtPhoneno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPhoneno.Location = new System.Drawing.Point(168, 534);
            this.txtPhoneno.Name = "txtPhoneno";
            this.txtPhoneno.Size = new System.Drawing.Size(100, 21);
            this.txtPhoneno.TabIndex = 23;
            // 
            // txtWorknm
            // 
            this.txtWorknm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWorknm.Location = new System.Drawing.Point(67, 534);
            this.txtWorknm.Name = "txtWorknm";
            this.txtWorknm.Size = new System.Drawing.Size(100, 21);
            this.txtWorknm.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 539);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "신청인";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(9, 84);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(889, 422);
            this.grdMain.TabIndex = 24;
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
            this.gridColumn10,
            this.gridColumn11});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "명일련";
            this.gridColumn1.FieldName = "EPRTNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 55;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "환자ID";
            this.gridColumn2.FieldName = "PID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "심사상태";
            this.gridColumn4.FieldName = "DONFG";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 55;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "삭감금액1";
            this.gridColumn5.FieldName = "SAKAMT1";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "삭감금액2";
            this.gridColumn6.FieldName = "SAKAMT2";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "이의신청금액1";
            this.gridColumn7.FieldName = "OBJAMT1";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 95;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "이의신청금액2";
            this.gridColumn8.FieldName = "OBJAMT2";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 95;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "문서번호";
            this.gridColumn9.FieldName = "DOCUNO";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 95;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "완료일자";
            this.gridColumn10.FieldName = "PRTDT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 65;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "입외";
            this.gridColumn11.FieldName = "IOFGNM";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 35;
            // 
            // btnMake
            // 
            this.btnMake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMake.Location = new System.Drawing.Point(557, 55);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(114, 23);
            this.btnMake.TabIndex = 25;
            this.btnMake.Text = "이의신청서 생성";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(671, 55);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(114, 23);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.Text = "이의신청서 출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(785, 55);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(114, 23);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "이의신청서 삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.Location = new System.Drawing.Point(557, 26);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(114, 23);
            this.btnFinish.TabIndex = 28;
            this.btnFinish.Text = "청구 완료";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnFinishCancel
            // 
            this.btnFinishCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinishCancel.Location = new System.Drawing.Point(671, 26);
            this.btnFinishCancel.Name = "btnFinishCancel";
            this.btnFinishCancel.Size = new System.Drawing.Size(114, 23);
            this.btnFinishCancel.TabIndex = 29;
            this.btnFinishCancel.Text = "완료 취소";
            this.btnFinishCancel.UseVisualStyleBackColor = true;
            this.btnFinishCancel.Click += new System.EventHandler(this.btnFinishCancel_Click);
            // 
            // ADD0602E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 563);
            this.Controls.Add(this.btnFinishCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtPhoneno);
            this.Controls.Add(this.txtWorknm);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.txtHosnm);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtReday);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGrpno);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDemseq);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDcount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCnecno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panQfy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panObj);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD0602E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "이의신청대상자조회(ADD0602E)";
            this.Load += new System.EventHandler(this.ADD0602E_Load);
            this.Activated += new System.EventHandler(this.ADD0602E_Activated);
            this.panObj.ResumeLayout(false);
            this.panObj.PerformLayout();
            this.panQfy.ResumeLayout(false);
            this.panQfy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panObj;
        private System.Windows.Forms.RadioButton rbObjB;
        private System.Windows.Forms.RadioButton rbObjA;
        private System.Windows.Forms.Panel panQfy;
        private System.Windows.Forms.RadioButton rbQfy3;
        private System.Windows.Forms.RadioButton rbQfy2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCnecno;
        private System.Windows.Forms.TextBox txtDcount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDemseq;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGrpno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReday;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHosnm;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.TextBox txtPhoneno;
        private System.Windows.Forms.TextBox txtWorknm;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.Button btnMake;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnFinishCancel;
    }
}


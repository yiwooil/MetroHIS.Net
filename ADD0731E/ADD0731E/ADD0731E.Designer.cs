namespace ADD0731E
{
    partial class ADD0731E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD0731E));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDemno = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDelRow = new System.Windows.Forms.Button();
            this.btnInsRow = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnEdi = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCDGBNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcJRGBNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPCODENM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPPLYDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcADDFILEFGNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREMARK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDdnm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotcnt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFmno = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "신청번호";
            // 
            // txtDemno
            // 
            this.txtDemno.Location = new System.Drawing.Point(66, 10);
            this.txtDemno.Name = "txtDemno";
            this.txtDemno.Size = new System.Drawing.Size(86, 21);
            this.txtDemno.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(178, 9);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "신규";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(342, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(711, 9);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 5;
            this.btnAddRow.Text = "행추가";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDelRow
            // 
            this.btnDelRow.Location = new System.Drawing.Point(859, 9);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(75, 23);
            this.btnDelRow.TabIndex = 7;
            this.btnDelRow.Text = "행삭제";
            this.btnDelRow.UseVisualStyleBackColor = true;
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // btnInsRow
            // 
            this.btnInsRow.Location = new System.Drawing.Point(785, 9);
            this.btnInsRow.Name = "btnInsRow";
            this.btnInsRow.Size = new System.Drawing.Size(75, 23);
            this.btnInsRow.TabIndex = 6;
            this.btnInsRow.Text = "행삽입";
            this.btnInsRow.UseVisualStyleBackColor = true;
            this.btnInsRow.Click += new System.EventHandler(this.btnInsRow_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(605, 9);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "삭제";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(416, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(490, 9);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(116, 23);
            this.btnSaveAs.TabIndex = 10;
            this.btnSaveAs.Text = "다른 번호로 저장";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnEdi
            // 
            this.btnEdi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdi.Location = new System.Drawing.Point(12, 450);
            this.btnEdi.Name = "btnEdi";
            this.btnEdi.Size = new System.Drawing.Size(96, 23);
            this.btnEdi.TabIndex = 11;
            this.btnEdi.Text = "EDI 파일 생성";
            this.btnEdi.UseVisualStyleBackColor = true;
            this.btnEdi.Click += new System.EventHandler(this.btnEdi_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(12, 38);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(921, 405);
            this.grdMain.TabIndex = 12;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCDGBNM,
            this.gcJRGBNM,
            this.gcPCODE,
            this.gcPCODENM,
            this.gcAMT,
            this.gcAPPLYDT,
            this.gcADDFILEFGNM,
            this.gcREMARK});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsCustomization.AllowSort = false;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grdMainView_ValidatingEditor);
            this.grdMainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMainView_KeyDown);
            this.grdMainView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.grdMainView_CustomRowCellEdit);
            this.grdMainView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdMainView_KeyPress);
            // 
            // gcCDGBNM
            // 
            this.gcCDGBNM.Caption = "코드구분";
            this.gcCDGBNM.FieldName = "CDGBNM";
            this.gcCDGBNM.Name = "gcCDGBNM";
            this.gcCDGBNM.Visible = true;
            this.gcCDGBNM.VisibleIndex = 0;
            this.gcCDGBNM.Width = 80;
            // 
            // gcJRGBNM
            // 
            this.gcJRGBNM.Caption = "진료구분";
            this.gcJRGBNM.FieldName = "JRGBNM";
            this.gcJRGBNM.Name = "gcJRGBNM";
            this.gcJRGBNM.Visible = true;
            this.gcJRGBNM.VisibleIndex = 1;
            this.gcJRGBNM.Width = 65;
            // 
            // gcPCODE
            // 
            this.gcPCODE.Caption = "코드";
            this.gcPCODE.FieldName = "PCODE";
            this.gcPCODE.Name = "gcPCODE";
            this.gcPCODE.Visible = true;
            this.gcPCODE.VisibleIndex = 2;
            // 
            // gcPCODENM
            // 
            this.gcPCODENM.Caption = "코드명";
            this.gcPCODENM.FieldName = "PCODENM";
            this.gcPCODENM.Name = "gcPCODENM";
            this.gcPCODENM.Visible = true;
            this.gcPCODENM.VisibleIndex = 3;
            this.gcPCODENM.Width = 250;
            // 
            // gcAMT
            // 
            this.gcAMT.Caption = "비용";
            this.gcAMT.DisplayFormat.FormatString = "#,###";
            this.gcAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gcAMT.FieldName = "AMT";
            this.gcAMT.Name = "gcAMT";
            this.gcAMT.Visible = true;
            this.gcAMT.VisibleIndex = 4;
            // 
            // gcAPPLYDT
            // 
            this.gcAPPLYDT.Caption = "적용일";
            this.gcAPPLYDT.FieldName = "APPLYDT";
            this.gcAPPLYDT.Name = "gcAPPLYDT";
            this.gcAPPLYDT.Visible = true;
            this.gcAPPLYDT.VisibleIndex = 5;
            // 
            // gcADDFILEFGNM
            // 
            this.gcADDFILEFGNM.Caption = "첨부파일";
            this.gcADDFILEFGNM.FieldName = "ADDFILEFGNM";
            this.gcADDFILEFGNM.Name = "gcADDFILEFGNM";
            this.gcADDFILEFGNM.Visible = true;
            this.gcADDFILEFGNM.VisibleIndex = 6;
            this.gcADDFILEFGNM.Width = 60;
            // 
            // gcREMARK
            // 
            this.gcREMARK.Caption = "참조란";
            this.gcREMARK.FieldName = "REMARK";
            this.gcREMARK.Name = "gcREMARK";
            this.gcREMARK.Visible = true;
            this.gcREMARK.VisibleIndex = 7;
            this.gcREMARK.Width = 200;
            // 
            // txtHosid
            // 
            this.txtHosid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHosid.Location = new System.Drawing.Point(484, 451);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(76, 21);
            this.txtHosid.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "요양기관기호";
            // 
            // txtDdnm
            // 
            this.txtDdnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDdnm.Location = new System.Drawing.Point(618, 451);
            this.txtDdnm.Name = "txtDdnm";
            this.txtDdnm.Size = new System.Drawing.Size(76, 21);
            this.txtDdnm.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "담당자";
            // 
            // txtTotcnt
            // 
            this.txtTotcnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotcnt.Location = new System.Drawing.Point(742, 450);
            this.txtTotcnt.Name = "txtTotcnt";
            this.txtTotcnt.Size = new System.Drawing.Size(76, 21);
            this.txtTotcnt.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(707, 454);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "건수";
            // 
            // txtFmno
            // 
            this.txtFmno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFmno.Location = new System.Drawing.Point(820, 450);
            this.txtFmno.Name = "txtFmno";
            this.txtFmno.Size = new System.Drawing.Size(64, 21);
            this.txtFmno.TabIndex = 19;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtVersion.Location = new System.Drawing.Point(886, 450);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(46, 21);
            this.txtVersion.TabIndex = 20;
            // 
            // btnList
            // 
            this.btnList.Image = ((System.Drawing.Image)(resources.GetObject("btnList.Image")));
            this.btnList.Location = new System.Drawing.Point(156, 9);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(23, 23);
            this.btnList.TabIndex = 78;
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // ADD0731E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 480);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtFmno);
            this.Controls.Add(this.txtTotcnt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDdnm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnEdi);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelRow);
            this.Controls.Add(this.btnInsRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtDemno);
            this.Controls.Add(this.label1);
            this.Name = "ADD0731E";
            this.Text = "비용산정통보서(ADD0731E)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDemno;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDelRow;
        private System.Windows.Forms.Button btnInsRow;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnEdi;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDdnm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotcnt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFmno;
        private System.Windows.Forms.TextBox txtVersion;
        private DevExpress.XtraGrid.Columns.GridColumn gcCDGBNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcJRGBNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPCODE;
        private DevExpress.XtraGrid.Columns.GridColumn gcPCODENM;
        private DevExpress.XtraGrid.Columns.GridColumn gcAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPPLYDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcADDFILEFGNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcREMARK;
        private System.Windows.Forms.Button btnList;
    }
}


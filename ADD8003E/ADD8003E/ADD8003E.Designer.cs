namespace ADD8003E
{
    partial class ADD8003E
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADD8003E));
            this.txtReqymAndSeq = new System.Windows.Forms.TextBox();
            this.btnReqymAndSeq = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFmno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDdnm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPhareqtot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcELINESEQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMKDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPHADIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRGEFFKND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcITEMNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWRITEDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSTDSIZE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUNIT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAPPLDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDRGEFF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDOESQY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lstReqymAndSeq = new System.Windows.Forms.ListBox();
            this.btnDelRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDelReqseq = new System.Windows.Forms.Button();
            this.btnNewReqseq = new System.Windows.Forms.Button();
            this.btnSaveReqseq = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReqymAndSeq
            // 
            this.txtReqymAndSeq.Location = new System.Drawing.Point(76, 12);
            this.txtReqymAndSeq.Name = "txtReqymAndSeq";
            this.txtReqymAndSeq.Size = new System.Drawing.Size(102, 21);
            this.txtReqymAndSeq.TabIndex = 27;
            this.txtReqymAndSeq.TextChanged += new System.EventHandler(this.txtReqymAndSeq_TextChanged);
            // 
            // btnReqymAndSeq
            // 
            this.btnReqymAndSeq.Image = ((System.Drawing.Image)(resources.GetObject("btnReqymAndSeq.Image")));
            this.btnReqymAndSeq.Location = new System.Drawing.Point(179, 12);
            this.btnReqymAndSeq.Name = "btnReqymAndSeq";
            this.btnReqymAndSeq.Size = new System.Drawing.Size(23, 23);
            this.btnReqymAndSeq.TabIndex = 28;
            this.btnReqymAndSeq.UseVisualStyleBackColor = true;
            this.btnReqymAndSeq.Click += new System.EventHandler(this.btnReqymAndSeq_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "신청번호 :";
            // 
            // txtFmno
            // 
            this.txtFmno.Location = new System.Drawing.Point(401, 13);
            this.txtFmno.Name = "txtFmno";
            this.txtFmno.Size = new System.Drawing.Size(70, 21);
            this.txtFmno.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "서식번호 :";
            // 
            // txtHosid
            // 
            this.txtHosid.Location = new System.Drawing.Point(579, 14);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(73, 21);
            this.txtHosid.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "요양기관기호 :";
            // 
            // txtDdnm
            // 
            this.txtDdnm.Location = new System.Drawing.Point(728, 15);
            this.txtDdnm.Name = "txtDdnm";
            this.txtDdnm.Size = new System.Drawing.Size(70, 21);
            this.txtDdnm.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(675, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "담당자 :";
            // 
            // txtPhareqtot
            // 
            this.txtPhareqtot.Location = new System.Drawing.Point(909, 16);
            this.txtPhareqtot.Name = "txtPhareqtot";
            this.txtPhareqtot.ReadOnly = true;
            this.txtPhareqtot.Size = new System.Drawing.Size(70, 21);
            this.txtPhareqtot.TabIndex = 38;
            this.txtPhareqtot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(827, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "신고 총건수 :";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(74, 39);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(908, 21);
            this.txtMemo.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "참조란 :";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(13, 90);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(969, 401);
            this.grdMain.TabIndex = 41;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcELINESEQ,
            this.gcMKDIVNM,
            this.gcPHADIVNM,
            this.gcDRGEFFKND,
            this.gcITEMCD,
            this.gcITEMNM,
            this.gcWRITEDT,
            this.gcDEMAMT,
            this.gcSTDSIZE,
            this.gcUNIT,
            this.gcAPPLDT,
            this.gcDRGEFF,
            this.gcDOESQY});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.DoubleClick += new System.EventHandler(this.grdMainView_DoubleClick);
            // 
            // gcELINESEQ
            // 
            this.gcELINESEQ.Caption = "줄번호";
            this.gcELINESEQ.FieldName = "ELINESEQ";
            this.gcELINESEQ.Name = "gcELINESEQ";
            this.gcELINESEQ.OptionsColumn.AllowEdit = false;
            this.gcELINESEQ.OptionsColumn.ReadOnly = true;
            this.gcELINESEQ.Visible = true;
            this.gcELINESEQ.VisibleIndex = 0;
            this.gcELINESEQ.Width = 50;
            // 
            // gcMKDIVNM
            // 
            this.gcMKDIVNM.Caption = "구분";
            this.gcMKDIVNM.FieldName = "MKDIVNM";
            this.gcMKDIVNM.Name = "gcMKDIVNM";
            this.gcMKDIVNM.OptionsColumn.AllowEdit = false;
            this.gcMKDIVNM.OptionsColumn.ReadOnly = true;
            this.gcMKDIVNM.Visible = true;
            this.gcMKDIVNM.VisibleIndex = 1;
            // 
            // gcPHADIVNM
            // 
            this.gcPHADIVNM.Caption = "투여형태";
            this.gcPHADIVNM.FieldName = "PHADIVNM";
            this.gcPHADIVNM.Name = "gcPHADIVNM";
            this.gcPHADIVNM.OptionsColumn.AllowEdit = false;
            this.gcPHADIVNM.OptionsColumn.ReadOnly = true;
            this.gcPHADIVNM.Visible = true;
            this.gcPHADIVNM.VisibleIndex = 2;
            // 
            // gcDRGEFFKND
            // 
            this.gcDRGEFFKND.Caption = "주요효능군";
            this.gcDRGEFFKND.FieldName = "DRGEFFKND";
            this.gcDRGEFFKND.Name = "gcDRGEFFKND";
            this.gcDRGEFFKND.OptionsColumn.AllowEdit = false;
            this.gcDRGEFFKND.OptionsColumn.ReadOnly = true;
            this.gcDRGEFFKND.Visible = true;
            this.gcDRGEFFKND.VisibleIndex = 3;
            // 
            // gcITEMCD
            // 
            this.gcITEMCD.Caption = "품목코드";
            this.gcITEMCD.FieldName = "ITEMCD";
            this.gcITEMCD.Name = "gcITEMCD";
            this.gcITEMCD.OptionsColumn.AllowEdit = false;
            this.gcITEMCD.OptionsColumn.ReadOnly = true;
            this.gcITEMCD.Visible = true;
            this.gcITEMCD.VisibleIndex = 4;
            // 
            // gcITEMNM
            // 
            this.gcITEMNM.Caption = "품명";
            this.gcITEMNM.FieldName = "ITEMNM";
            this.gcITEMNM.Name = "gcITEMNM";
            this.gcITEMNM.OptionsColumn.AllowEdit = false;
            this.gcITEMNM.OptionsColumn.ReadOnly = true;
            this.gcITEMNM.Visible = true;
            this.gcITEMNM.VisibleIndex = 5;
            // 
            // gcWRITEDT
            // 
            this.gcWRITEDT.Caption = "기재일";
            this.gcWRITEDT.FieldName = "WRITEDT";
            this.gcWRITEDT.Name = "gcWRITEDT";
            this.gcWRITEDT.OptionsColumn.AllowEdit = false;
            this.gcWRITEDT.OptionsColumn.ReadOnly = true;
            this.gcWRITEDT.Visible = true;
            this.gcWRITEDT.VisibleIndex = 6;
            // 
            // gcDEMAMT
            // 
            this.gcDEMAMT.Caption = "청구가";
            this.gcDEMAMT.FieldName = "DEMAMT";
            this.gcDEMAMT.Name = "gcDEMAMT";
            this.gcDEMAMT.OptionsColumn.AllowEdit = false;
            this.gcDEMAMT.OptionsColumn.ReadOnly = true;
            this.gcDEMAMT.Visible = true;
            this.gcDEMAMT.VisibleIndex = 7;
            // 
            // gcSTDSIZE
            // 
            this.gcSTDSIZE.Caption = "규격";
            this.gcSTDSIZE.FieldName = "STDSIZE";
            this.gcSTDSIZE.Name = "gcSTDSIZE";
            this.gcSTDSIZE.OptionsColumn.AllowEdit = false;
            this.gcSTDSIZE.OptionsColumn.ReadOnly = true;
            this.gcSTDSIZE.Visible = true;
            this.gcSTDSIZE.VisibleIndex = 8;
            this.gcSTDSIZE.Width = 65;
            // 
            // gcUNIT
            // 
            this.gcUNIT.Caption = "단위";
            this.gcUNIT.FieldName = "UNIT";
            this.gcUNIT.Name = "gcUNIT";
            this.gcUNIT.OptionsColumn.AllowEdit = false;
            this.gcUNIT.OptionsColumn.ReadOnly = true;
            this.gcUNIT.Visible = true;
            this.gcUNIT.VisibleIndex = 9;
            this.gcUNIT.Width = 55;
            // 
            // gcAPPLDT
            // 
            this.gcAPPLDT.Caption = "가격적용일";
            this.gcAPPLDT.FieldName = "APPLDT";
            this.gcAPPLDT.Name = "gcAPPLDT";
            this.gcAPPLDT.OptionsColumn.AllowEdit = false;
            this.gcAPPLDT.OptionsColumn.ReadOnly = true;
            this.gcAPPLDT.Visible = true;
            this.gcAPPLDT.VisibleIndex = 10;
            // 
            // gcDRGEFF
            // 
            this.gcDRGEFF.Caption = "효능,효과";
            this.gcDRGEFF.FieldName = "DRGEFF";
            this.gcDRGEFF.Name = "gcDRGEFF";
            this.gcDRGEFF.OptionsColumn.AllowEdit = false;
            this.gcDRGEFF.OptionsColumn.ReadOnly = true;
            this.gcDRGEFF.Visible = true;
            this.gcDRGEFF.VisibleIndex = 11;
            // 
            // gcDOESQY
            // 
            this.gcDOESQY.Caption = "용법,용량";
            this.gcDOESQY.FieldName = "DOESQY";
            this.gcDOESQY.Name = "gcDOESQY";
            this.gcDOESQY.OptionsColumn.AllowEdit = false;
            this.gcDOESQY.OptionsColumn.ReadOnly = true;
            this.gcDOESQY.Visible = true;
            this.gcDOESQY.VisibleIndex = 12;
            // 
            // lstReqymAndSeq
            // 
            this.lstReqymAndSeq.FormattingEnabled = true;
            this.lstReqymAndSeq.ItemHeight = 12;
            this.lstReqymAndSeq.Location = new System.Drawing.Point(74, 165);
            this.lstReqymAndSeq.Name = "lstReqymAndSeq";
            this.lstReqymAndSeq.Size = new System.Drawing.Size(109, 196);
            this.lstReqymAndSeq.TabIndex = 42;
            this.lstReqymAndSeq.Visible = false;
            this.lstReqymAndSeq.DoubleClick += new System.EventHandler(this.lstReqymAndSeq_DoubleClick);
            // 
            // btnDelRow
            // 
            this.btnDelRow.Location = new System.Drawing.Point(94, 62);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(79, 23);
            this.btnDelRow.TabIndex = 72;
            this.btnDelRow.Text = "행삭제";
            this.btnDelRow.UseVisualStyleBackColor = true;
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(13, 62);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(79, 23);
            this.btnAddRow.TabIndex = 71;
            this.btnAddRow.Text = "행추가";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDelReqseq
            // 
            this.btnDelReqseq.Location = new System.Drawing.Point(867, 63);
            this.btnDelReqseq.Name = "btnDelReqseq";
            this.btnDelReqseq.Size = new System.Drawing.Size(114, 23);
            this.btnDelReqseq.TabIndex = 74;
            this.btnDelReqseq.Text = "신청번호 삭제";
            this.btnDelReqseq.UseVisualStyleBackColor = true;
            this.btnDelReqseq.Click += new System.EventHandler(this.btnDelReqseq_Click);
            // 
            // btnNewReqseq
            // 
            this.btnNewReqseq.Location = new System.Drawing.Point(208, 12);
            this.btnNewReqseq.Name = "btnNewReqseq";
            this.btnNewReqseq.Size = new System.Drawing.Size(114, 23);
            this.btnNewReqseq.TabIndex = 73;
            this.btnNewReqseq.Text = "신청번호 만들기";
            this.btnNewReqseq.UseVisualStyleBackColor = true;
            this.btnNewReqseq.Click += new System.EventHandler(this.btnNewReqseq_Click);
            // 
            // btnSaveReqseq
            // 
            this.btnSaveReqseq.Location = new System.Drawing.Point(752, 63);
            this.btnSaveReqseq.Name = "btnSaveReqseq";
            this.btnSaveReqseq.Size = new System.Drawing.Size(114, 23);
            this.btnSaveReqseq.TabIndex = 75;
            this.btnSaveReqseq.Text = "신청번호 저장";
            this.btnSaveReqseq.UseVisualStyleBackColor = true;
            this.btnSaveReqseq.Click += new System.EventHandler(this.btnSaveReqseq_Click);
            // 
            // ADD8003E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 503);
            this.Controls.Add(this.btnSaveReqseq);
            this.Controls.Add(this.btnDelReqseq);
            this.Controls.Add(this.btnNewReqseq);
            this.Controls.Add(this.btnDelRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.lstReqymAndSeq);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPhareqtot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDdnm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFmno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReqymAndSeq);
            this.Controls.Add(this.txtReqymAndSeq);
            this.Name = "ADD8003E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "산재보험자체조제제제약신고서(ADD8003E)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReqymAndSeq;
        private System.Windows.Forms.Button btnReqymAndSeq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFmno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDdnm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPhareqtot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcELINESEQ;
        private DevExpress.XtraGrid.Columns.GridColumn gcMKDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcPHADIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRGEFFKND;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcITEMNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcWRITEDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcSTDSIZE;
        private DevExpress.XtraGrid.Columns.GridColumn gcUNIT;
        private DevExpress.XtraGrid.Columns.GridColumn gcAPPLDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDRGEFF;
        private DevExpress.XtraGrid.Columns.GridColumn gcDOESQY;
        private System.Windows.Forms.ListBox lstReqymAndSeq;
        private System.Windows.Forms.Button btnDelRow;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDelReqseq;
        private System.Windows.Forms.Button btnNewReqseq;
        private System.Windows.Forms.Button btnSaveReqseq;
    }
}


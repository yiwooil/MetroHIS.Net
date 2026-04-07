namespace ADD8001Q
{
    partial class ADD8001Q
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
            this.txtFrdt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTodt = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdMainView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcADDDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMGBNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcACCBACKDIVNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcREPDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcACCNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMCNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDEMAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcYYMM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBUSSNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcETCFG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRSNCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMEMO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcISI020 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcISI030 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFrdt
            // 
            this.txtFrdt.Location = new System.Drawing.Point(75, 12);
            this.txtFrdt.Name = "txtFrdt";
            this.txtFrdt.Size = new System.Drawing.Size(70, 21);
            this.txtFrdt.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "통지일자 :";
            // 
            // txtTodt
            // 
            this.txtTodt.Location = new System.Drawing.Point(151, 12);
            this.txtTodt.Name = "txtTodt";
            this.txtTodt.Size = new System.Drawing.Size(70, 21);
            this.txtTodt.TabIndex = 26;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(830, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(79, 23);
            this.btnPrint.TabIndex = 28;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(749, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 27;
            this.btnQuery.Text = "조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Location = new System.Drawing.Point(13, 39);
            this.grdMain.MainView = this.grdMainView;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2});
            this.grdMain.Size = new System.Drawing.Size(896, 437);
            this.grdMain.TabIndex = 29;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMainView});
            // 
            // grdMainView
            // 
            this.grdMainView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcADDDIV,
            this.gcDEMNO,
            this.gcDEMGBNM,
            this.gcACCBACKDIVNM,
            this.gcREPDT,
            this.gcACCNO,
            this.gcDEMCNT,
            this.gcDEMAMT,
            this.gcYYMM,
            this.gcBUSSNM,
            this.gcETCFG,
            this.gcRSNCD,
            this.gcMEMO,
            this.gcISI020,
            this.gcISI030});
            this.grdMainView.GridControl = this.grdMain;
            this.grdMainView.Name = "grdMainView";
            this.grdMainView.OptionsSelection.MultiSelect = true;
            this.grdMainView.OptionsView.ColumnAutoWidth = false;
            this.grdMainView.OptionsView.ShowGroupPanel = false;
            this.grdMainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grdMainView.Click += new System.EventHandler(this.grdMainView_Click);
            this.grdMainView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdMainView_RowCellClick);
            // 
            // gcADDDIV
            // 
            this.gcADDDIV.Caption = "접수구분";
            this.gcADDDIV.FieldName = "ACCDIV";
            this.gcADDDIV.Name = "gcADDDIV";
            this.gcADDDIV.OptionsColumn.ReadOnly = true;
            this.gcADDDIV.Visible = true;
            this.gcADDDIV.VisibleIndex = 0;
            this.gcADDDIV.Width = 55;
            // 
            // gcDEMNO
            // 
            this.gcDEMNO.Caption = "청구번호";
            this.gcDEMNO.FieldName = "DEMNO";
            this.gcDEMNO.Name = "gcDEMNO";
            this.gcDEMNO.OptionsColumn.ReadOnly = true;
            this.gcDEMNO.Visible = true;
            this.gcDEMNO.VisibleIndex = 1;
            // 
            // gcDEMGBNM
            // 
            this.gcDEMGBNM.Caption = "청구구분";
            this.gcDEMGBNM.FieldName = "DEMGBNM";
            this.gcDEMGBNM.Name = "gcDEMGBNM";
            this.gcDEMGBNM.OptionsColumn.ReadOnly = true;
            this.gcDEMGBNM.Visible = true;
            this.gcDEMGBNM.VisibleIndex = 2;
            this.gcDEMGBNM.Width = 55;
            // 
            // gcACCBACKDIVNM
            // 
            this.gcACCBACKDIVNM.Caption = "구분";
            this.gcACCBACKDIVNM.FieldName = "ACCBACKDIVNM";
            this.gcACCBACKDIVNM.Name = "gcACCBACKDIVNM";
            this.gcACCBACKDIVNM.OptionsColumn.ReadOnly = true;
            this.gcACCBACKDIVNM.Visible = true;
            this.gcACCBACKDIVNM.VisibleIndex = 3;
            this.gcACCBACKDIVNM.Width = 35;
            // 
            // gcREPDT
            // 
            this.gcREPDT.Caption = "통지일자";
            this.gcREPDT.FieldName = "REPDT";
            this.gcREPDT.Name = "gcREPDT";
            this.gcREPDT.OptionsColumn.ReadOnly = true;
            this.gcREPDT.Visible = true;
            this.gcREPDT.VisibleIndex = 4;
            // 
            // gcACCNO
            // 
            this.gcACCNO.Caption = "접수번호";
            this.gcACCNO.FieldName = "ACCNO";
            this.gcACCNO.Name = "gcACCNO";
            this.gcACCNO.OptionsColumn.ReadOnly = true;
            this.gcACCNO.Visible = true;
            this.gcACCNO.VisibleIndex = 5;
            // 
            // gcDEMCNT
            // 
            this.gcDEMCNT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMCNT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMCNT.Caption = "청구건수";
            this.gcDEMCNT.FieldName = "DEMCNT";
            this.gcDEMCNT.Name = "gcDEMCNT";
            this.gcDEMCNT.OptionsColumn.ReadOnly = true;
            this.gcDEMCNT.Visible = true;
            this.gcDEMCNT.VisibleIndex = 6;
            // 
            // gcDEMAMT
            // 
            this.gcDEMAMT.AppearanceCell.Options.UseTextOptions = true;
            this.gcDEMAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gcDEMAMT.Caption = "청구금액";
            this.gcDEMAMT.FieldName = "DEMAMT";
            this.gcDEMAMT.Name = "gcDEMAMT";
            this.gcDEMAMT.OptionsColumn.ReadOnly = true;
            this.gcDEMAMT.Visible = true;
            this.gcDEMAMT.VisibleIndex = 7;
            this.gcDEMAMT.Width = 90;
            // 
            // gcYYMM
            // 
            this.gcYYMM.Caption = "진료월";
            this.gcYYMM.FieldName = "YYMM";
            this.gcYYMM.MinWidth = 50;
            this.gcYYMM.Name = "gcYYMM";
            this.gcYYMM.OptionsColumn.ReadOnly = true;
            this.gcYYMM.Visible = true;
            this.gcYYMM.VisibleIndex = 8;
            this.gcYYMM.Width = 50;
            // 
            // gcBUSSNM
            // 
            this.gcBUSSNM.Caption = "입외";
            this.gcBUSSNM.FieldName = "BUSSNM";
            this.gcBUSSNM.Name = "gcBUSSNM";
            this.gcBUSSNM.OptionsColumn.ReadOnly = true;
            this.gcBUSSNM.Visible = true;
            this.gcBUSSNM.VisibleIndex = 9;
            this.gcBUSSNM.Width = 35;
            // 
            // gcETCFG
            // 
            this.gcETCFG.Caption = "후유";
            this.gcETCFG.FieldName = "ETCFG";
            this.gcETCFG.Name = "gcETCFG";
            this.gcETCFG.OptionsColumn.ReadOnly = true;
            this.gcETCFG.Visible = true;
            this.gcETCFG.VisibleIndex = 10;
            this.gcETCFG.Width = 35;
            // 
            // gcRSNCD
            // 
            this.gcRSNCD.Caption = "반송사유";
            this.gcRSNCD.FieldName = "RSNCD";
            this.gcRSNCD.Name = "gcRSNCD";
            this.gcRSNCD.OptionsColumn.ReadOnly = true;
            this.gcRSNCD.Visible = true;
            this.gcRSNCD.VisibleIndex = 11;
            this.gcRSNCD.Width = 55;
            // 
            // gcMEMO
            // 
            this.gcMEMO.Caption = "참조";
            this.gcMEMO.FieldName = "MEMO";
            this.gcMEMO.Name = "gcMEMO";
            this.gcMEMO.OptionsColumn.ReadOnly = true;
            this.gcMEMO.Visible = true;
            this.gcMEMO.VisibleIndex = 12;
            // 
            // gcISI020
            // 
            this.gcISI020.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.gcISI020.AppearanceCell.Options.UseFont = true;
            this.gcISI020.AppearanceCell.Options.UseTextOptions = true;
            this.gcISI020.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcISI020.Caption = "지불";
            this.gcISI020.FieldName = "ISI020";
            this.gcISI020.Name = "gcISI020";
            this.gcISI020.OptionsColumn.AllowEdit = false;
            this.gcISI020.OptionsColumn.ReadOnly = true;
            this.gcISI020.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gcISI020.Visible = true;
            this.gcISI020.VisibleIndex = 13;
            this.gcISI020.Width = 35;
            // 
            // gcISI030
            // 
            this.gcISI030.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.gcISI030.AppearanceCell.Options.UseFont = true;
            this.gcISI030.AppearanceCell.Options.UseTextOptions = true;
            this.gcISI030.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcISI030.Caption = "심사";
            this.gcISI030.FieldName = "ISI030";
            this.gcISI030.Name = "gcISI030";
            this.gcISI030.OptionsColumn.AllowEdit = false;
            this.gcISI030.OptionsColumn.ReadOnly = true;
            this.gcISI030.Visible = true;
            this.gcISI030.VisibleIndex = 14;
            this.gcISI030.Width = 35;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // ADD8001Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 488);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtTodt);
            this.Controls.Add(this.txtFrdt);
            this.Controls.Add(this.label2);
            this.Name = "ADD8001Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "산재보험접수반송증(ADD8001Q)";
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFrdt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTodt;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnQuery;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdMainView;
        private DevExpress.XtraGrid.Columns.GridColumn gcADDDIV;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMGBNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcACCBACKDIVNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcREPDT;
        private DevExpress.XtraGrid.Columns.GridColumn gcACCNO;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMCNT;
        private DevExpress.XtraGrid.Columns.GridColumn gcDEMAMT;
        private DevExpress.XtraGrid.Columns.GridColumn gcYYMM;
        private DevExpress.XtraGrid.Columns.GridColumn gcBUSSNM;
        private DevExpress.XtraGrid.Columns.GridColumn gcETCFG;
        private DevExpress.XtraGrid.Columns.GridColumn gcRSNCD;
        private DevExpress.XtraGrid.Columns.GridColumn gcMEMO;
        private DevExpress.XtraGrid.Columns.GridColumn gcISI020;
        private DevExpress.XtraGrid.Columns.GridColumn gcISI030;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
    }
}


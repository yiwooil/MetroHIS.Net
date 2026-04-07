namespace EMR_IMG_TS
{
    partial class EMR_IMG_TS
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
            this.txtPnm = new System.Windows.Forms.TextBox();
            this.txtPid = new System.Windows.Forms.TextBox();
            this.cboZoom = new System.Windows.Forms.ComboBox();
            this.panMain = new System.Windows.Forms.Panel();
            this.picMain1 = new System.Windows.Forms.PictureBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtLeftMargin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRightMargin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBottomMargin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTopMargin = new System.Windows.Forms.TextBox();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPnm
            // 
            this.txtPnm.Location = new System.Drawing.Point(197, 12);
            this.txtPnm.Name = "txtPnm";
            this.txtPnm.ReadOnly = true;
            this.txtPnm.Size = new System.Drawing.Size(100, 21);
            this.txtPnm.TabIndex = 8;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(94, 12);
            this.txtPid.Name = "txtPid";
            this.txtPid.ReadOnly = true;
            this.txtPid.Size = new System.Drawing.Size(100, 21);
            this.txtPid.TabIndex = 7;
            // 
            // cboZoom
            // 
            this.cboZoom.FormattingEnabled = true;
            this.cboZoom.Items.AddRange(new object[] {
            "100",
            "90",
            "80",
            "70",
            "60",
            "50",
            "40",
            "30",
            "20",
            "10"});
            this.cboZoom.Location = new System.Drawing.Point(15, 12);
            this.cboZoom.Name = "cboZoom";
            this.cboZoom.Size = new System.Drawing.Size(73, 20);
            this.cboZoom.TabIndex = 6;
            this.cboZoom.SelectedIndexChanged += new System.EventHandler(this.cboZoom_SelectedIndexChanged);
            this.cboZoom.Leave += new System.EventHandler(this.cboZoom_Leave);
            this.cboZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboZoom_KeyPress);
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panMain.AutoScroll = true;
            this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMain.Controls.Add(this.picMain1);
            this.panMain.Location = new System.Drawing.Point(12, 57);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(665, 537);
            this.panMain.TabIndex = 5;
            // 
            // picMain1
            // 
            this.picMain1.Location = new System.Drawing.Point(123, 93);
            this.picMain1.Name = "picMain1";
            this.picMain1.Size = new System.Drawing.Size(260, 238);
            this.picMain1.TabIndex = 0;
            this.picMain1.TabStop = false;
            this.picMain1.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(603, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "출력";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtLeftMargin
            // 
            this.txtLeftMargin.Location = new System.Drawing.Point(342, 12);
            this.txtLeftMargin.Name = "txtLeftMargin";
            this.txtLeftMargin.Size = new System.Drawing.Size(25, 21);
            this.txtLeftMargin.TabIndex = 10;
            this.txtLeftMargin.Text = "30";
            this.txtLeftMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLeftMargin.Leave += new System.EventHandler(this.txtLeftMargin_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "왼쪽";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "오른쪽";
            // 
            // txtRightMargin
            // 
            this.txtRightMargin.Location = new System.Drawing.Point(416, 12);
            this.txtRightMargin.Name = "txtRightMargin";
            this.txtRightMargin.Size = new System.Drawing.Size(25, 21);
            this.txtRightMargin.TabIndex = 12;
            this.txtRightMargin.Text = "30";
            this.txtRightMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRightMargin.Leave += new System.EventHandler(this.txtRightMargin_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(504, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "아래";
            // 
            // txtBottomMargin
            // 
            this.txtBottomMargin.Location = new System.Drawing.Point(531, 12);
            this.txtBottomMargin.Name = "txtBottomMargin";
            this.txtBottomMargin.Size = new System.Drawing.Size(25, 21);
            this.txtBottomMargin.TabIndex = 16;
            this.txtBottomMargin.Text = "20";
            this.txtBottomMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBottomMargin.Leave += new System.EventHandler(this.txtBottomMargin_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(452, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "위";
            // 
            // txtTopMargin
            // 
            this.txtTopMargin.Location = new System.Drawing.Point(471, 12);
            this.txtTopMargin.Name = "txtTopMargin";
            this.txtTopMargin.Size = new System.Drawing.Size(25, 21);
            this.txtTopMargin.TabIndex = 14;
            this.txtTopMargin.Text = "20";
            this.txtTopMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTopMargin.Leave += new System.EventHandler(this.txtTopMargin_Leave);
            // 
            // EMR_IMG_TS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 606);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBottomMargin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTopMargin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRightMargin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLeftMargin);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtPnm);
            this.Controls.Add(this.txtPid);
            this.Controls.Add(this.cboZoom);
            this.Controls.Add(this.panMain);
            this.Name = "EMR_IMG_TS";
            this.Text = "동의서출력(EMR_IMG_TS)";
            this.Load += new System.EventHandler(this.EMR_IMG_TS_Load);
            this.Activated += new System.EventHandler(this.EMR_IMG_TS_Activated);
            this.Resize += new System.EventHandler(this.EMR_IMG_TS_Resize);
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMain1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPnm;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.ComboBox cboZoom;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.PictureBox picMain1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtLeftMargin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRightMargin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBottomMargin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTopMargin;
    }
}


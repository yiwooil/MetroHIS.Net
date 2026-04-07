namespace ADD0110E
{
    partial class ADD0110E
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
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAfterBdodt = new System.Windows.Forms.TextBox();
            this.txtAfterQfycd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChangQF = new System.Windows.Forms.TextBox();
            this.cboQfycd = new System.Windows.Forms.ComboBox();
            this.txtAfterDptcd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAfterPid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkBoRyu = new System.Windows.Forms.CheckBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Items.AddRange(new object[] {
            "11.외래->외래",
            "12.외래->입원",
            "21.입원->외래",
            "22.입원->입원"});
            this.cboMode.Location = new System.Drawing.Point(23, 23);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(176, 20);
            this.cboMode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "청구월";
            // 
            // txtAfterBdodt
            // 
            this.txtAfterBdodt.Location = new System.Drawing.Point(80, 55);
            this.txtAfterBdodt.Name = "txtAfterBdodt";
            this.txtAfterBdodt.Size = new System.Drawing.Size(119, 21);
            this.txtAfterBdodt.TabIndex = 2;
            // 
            // txtAfterQfycd
            // 
            this.txtAfterQfycd.Location = new System.Drawing.Point(80, 82);
            this.txtAfterQfycd.Name = "txtAfterQfycd";
            this.txtAfterQfycd.Size = new System.Drawing.Size(48, 21);
            this.txtAfterQfycd.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "자격";
            // 
            // txtChangQF
            // 
            this.txtChangQF.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtChangQF.Location = new System.Drawing.Point(151, 82);
            this.txtChangQF.Name = "txtChangQF";
            this.txtChangQF.Size = new System.Drawing.Size(48, 21);
            this.txtChangQF.TabIndex = 5;
            // 
            // cboQfycd
            // 
            this.cboQfycd.DropDownHeight = 200;
            this.cboQfycd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQfycd.DropDownWidth = 200;
            this.cboQfycd.FormattingEnabled = true;
            this.cboQfycd.IntegralHeight = false;
            this.cboQfycd.ItemHeight = 12;
            this.cboQfycd.Location = new System.Drawing.Point(80, 109);
            this.cboQfycd.Name = "cboQfycd";
            this.cboQfycd.Size = new System.Drawing.Size(119, 20);
            this.cboQfycd.TabIndex = 6;
            this.cboQfycd.SelectedIndexChanged += new System.EventHandler(this.cboQfycd_SelectedIndexChanged);
            // 
            // txtAfterDptcd
            // 
            this.txtAfterDptcd.Location = new System.Drawing.Point(80, 135);
            this.txtAfterDptcd.Name = "txtAfterDptcd";
            this.txtAfterDptcd.Size = new System.Drawing.Size(119, 21);
            this.txtAfterDptcd.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "진료과";
            // 
            // txtAfterPid
            // 
            this.txtAfterPid.Location = new System.Drawing.Point(80, 162);
            this.txtAfterPid.Name = "txtAfterPid";
            this.txtAfterPid.Size = new System.Drawing.Size(119, 21);
            this.txtAfterPid.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "환자ID";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "실행";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkBoRyu
            // 
            this.chkBoRyu.AutoSize = true;
            this.chkBoRyu.Location = new System.Drawing.Point(23, 202);
            this.chkBoRyu.Name = "chkBoRyu";
            this.chkBoRyu.Size = new System.Drawing.Size(192, 16);
            this.chkBoRyu.TabIndex = 14;
            this.chkBoRyu.Text = "원본을 [보류]상태로 만듭니다.";
            this.chkBoRyu.UseVisualStyleBackColor = true;
            // 
            // lblWait
            // 
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWait.Location = new System.Drawing.Point(4, 3);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(86, 17);
            this.lblWait.TabIndex = 15;
            this.lblWait.Text = "처리중입니다.";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWait.Visible = false;
            // 
            // ADD0110E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 272);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.chkBoRyu);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAfterPid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAfterDptcd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboQfycd);
            this.Controls.Add(this.txtChangQF);
            this.Controls.Add(this.txtAfterQfycd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAfterBdodt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboMode);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD0110E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "명세서복사(ADD0110E)";
            this.Load += new System.EventHandler(this.ADD0110E_Load);
            this.Activated += new System.EventHandler(this.ADD0110E_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAfterBdodt;
        private System.Windows.Forms.TextBox txtAfterQfycd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChangQF;
        private System.Windows.Forms.ComboBox cboQfycd;
        private System.Windows.Forms.TextBox txtAfterDptcd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAfterPid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkBoRyu;
        private System.Windows.Forms.Label lblWait;
    }
}


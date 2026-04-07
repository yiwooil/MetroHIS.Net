namespace ADD_ROI_LOGIN
{
    partial class ADD_ROI_LOGIN
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
            this.txtRoipwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRoiuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRoiFolder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRoipwd
            // 
            this.txtRoipwd.Location = new System.Drawing.Point(92, 53);
            this.txtRoipwd.Name = "txtRoipwd";
            this.txtRoipwd.Size = new System.Drawing.Size(200, 21);
            this.txtRoipwd.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "비밀번호";
            // 
            // txtRoiuid
            // 
            this.txtRoiuid.Location = new System.Drawing.Point(92, 26);
            this.txtRoiuid.Name = "txtRoiuid";
            this.txtRoiuid.Size = new System.Drawing.Size(200, 21);
            this.txtRoiuid.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "사용자ID";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(104, 98);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 23);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(192, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRoiFolder
            // 
            this.lblRoiFolder.AutoSize = true;
            this.lblRoiFolder.Location = new System.Drawing.Point(4, 128);
            this.lblRoiFolder.Name = "lblRoiFolder";
            this.lblRoiFolder.Size = new System.Drawing.Size(54, 12);
            this.lblRoiFolder.TabIndex = 11;
            this.lblRoiFolder.Text = "roi folder";
            // 
            // ADD_ROI_LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 145);
            this.Controls.Add(this.lblRoiFolder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtRoipwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRoiuid);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD_ROI_LOGIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ROI로그인(ADD_ROI_LOGIN)";
            this.Load += new System.EventHandler(this.ADD_ROI_LOGIN_Load);
            this.Activated += new System.EventHandler(this.ADD_ROI_LOGIN_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRoipwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRoiuid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRoiFolder;
    }
}


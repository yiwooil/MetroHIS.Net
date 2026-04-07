namespace CODEDr.NET
{
    partial class CODEDr
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtHosid = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.txtDrid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(129, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "생성";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtHosid
            // 
            this.txtHosid.Location = new System.Drawing.Point(224, 19);
            this.txtHosid.Name = "txtHosid";
            this.txtHosid.Size = new System.Drawing.Size(100, 21);
            this.txtHosid.TabIndex = 6;
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(118, 19);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(100, 21);
            this.txtServerIp.TabIndex = 5;
            // 
            // txtDrid
            // 
            this.txtDrid.Location = new System.Drawing.Point(12, 19);
            this.txtDrid.Name = "txtDrid";
            this.txtDrid.Size = new System.Drawing.Size(100, 21);
            this.txtDrid.TabIndex = 4;
            // 
            // CODEDr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 144);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtHosid);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.txtDrid);
            this.Name = "CODEDr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.CODEDr_Load);
            this.Activated += new System.EventHandler(this.CODEDr_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtHosid;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.TextBox txtDrid;
    }
}


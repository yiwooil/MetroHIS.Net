namespace ADD8002E
{
    partial class ADD8002E_1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtReqno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMake = new System.Windows.Forms.Button();
            this.cboRcvid = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 78;
            this.label1.Text = "신청번호 :";
            // 
            // txtReqno
            // 
            this.txtReqno.Location = new System.Drawing.Point(81, 35);
            this.txtReqno.Name = "txtReqno";
            this.txtReqno.ReadOnly = true;
            this.txtReqno.Size = new System.Drawing.Size(102, 21);
            this.txtReqno.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 81;
            this.label2.Text = "폴더 :";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(288, 124);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(28, 23);
            this.btnFolder.TabIndex = 80;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.BackColor = System.Drawing.SystemColors.Window;
            this.txtFolder.Location = new System.Drawing.Point(58, 124);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(224, 21);
            this.txtFolder.TabIndex = 79;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 83;
            this.label3.Text = "수신인 :";
            // 
            // btnMake
            // 
            this.btnMake.Location = new System.Drawing.Point(106, 168);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(113, 23);
            this.btnMake.TabIndex = 84;
            this.btnMake.Text = "송신 파일 생성";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // cboRcvid
            // 
            this.cboRcvid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRcvid.FormattingEnabled = true;
            this.cboRcvid.Location = new System.Drawing.Point(81, 65);
            this.cboRcvid.Name = "cboRcvid";
            this.cboRcvid.Size = new System.Drawing.Size(200, 20);
            this.cboRcvid.TabIndex = 85;
            this.cboRcvid.SelectedIndexChanged += new System.EventHandler(this.cboRcvid_SelectedIndexChanged);
            // 
            // ADD8002E_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 221);
            this.Controls.Add(this.cboRcvid);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReqno);
            this.Name = "ADD8002E_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "송신파일생성(ADD8002E_1)";
            this.Load += new System.EventHandler(this.ADD8002E_1_Load);
            this.Activated += new System.EventHandler(this.ADD8002E_1_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReqno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMake;
        private System.Windows.Forms.ComboBox cboRcvid;
    }
}
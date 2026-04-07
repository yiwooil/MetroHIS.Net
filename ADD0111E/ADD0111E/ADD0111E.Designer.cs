namespace ADD0111E
{
    partial class ADD0111E
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbHan = new System.Windows.Forms.RadioButton();
            this.rbYang = new System.Windows.Forms.RadioButton();
            this.rbOutPtnt = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbInPtnt = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYmm = new System.Windows.Forms.TextBox();
            this.txtFdate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTdate = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbQfy6 = new System.Windows.Forms.RadioButton();
            this.rbQfy3Dent = new System.Windows.Forms.RadioButton();
            this.rbQfy3 = new System.Windows.Forms.RadioButton();
            this.rbQfy5 = new System.Windows.Forms.RadioButton();
            this.rbQfy2Dent = new System.Windows.Forms.RadioButton();
            this.rbQfy2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMake = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbHan);
            this.panel1.Controls.Add(this.rbYang);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(65, 50);
            this.panel1.TabIndex = 0;
            // 
            // rbHan
            // 
            this.rbHan.AutoSize = true;
            this.rbHan.Location = new System.Drawing.Point(8, 27);
            this.rbHan.Name = "rbHan";
            this.rbHan.Size = new System.Drawing.Size(47, 16);
            this.rbHan.TabIndex = 1;
            this.rbHan.Text = "한방";
            this.rbHan.UseVisualStyleBackColor = true;
            // 
            // rbYang
            // 
            this.rbYang.AutoSize = true;
            this.rbYang.Checked = true;
            this.rbYang.Location = new System.Drawing.Point(8, 6);
            this.rbYang.Name = "rbYang";
            this.rbYang.Size = new System.Drawing.Size(47, 16);
            this.rbYang.TabIndex = 0;
            this.rbYang.TabStop = true;
            this.rbYang.Text = "양방";
            this.rbYang.UseVisualStyleBackColor = true;
            // 
            // rbOutPtnt
            // 
            this.rbOutPtnt.AutoSize = true;
            this.rbOutPtnt.Checked = true;
            this.rbOutPtnt.Location = new System.Drawing.Point(8, 16);
            this.rbOutPtnt.Name = "rbOutPtnt";
            this.rbOutPtnt.Size = new System.Drawing.Size(47, 16);
            this.rbOutPtnt.TabIndex = 2;
            this.rbOutPtnt.TabStop = true;
            this.rbOutPtnt.Text = "외래";
            this.rbOutPtnt.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbInPtnt);
            this.panel2.Controls.Add(this.rbOutPtnt);
            this.panel2.Location = new System.Drawing.Point(73, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 50);
            this.panel2.TabIndex = 2;
            // 
            // rbInPtnt
            // 
            this.rbInPtnt.AutoSize = true;
            this.rbInPtnt.Location = new System.Drawing.Point(60, 16);
            this.rbInPtnt.Name = "rbInPtnt";
            this.rbInPtnt.Size = new System.Drawing.Size(47, 16);
            this.rbInPtnt.TabIndex = 3;
            this.rbInPtnt.Text = "입원";
            this.rbInPtnt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "작업년월 :";
            // 
            // txtYmm
            // 
            this.txtYmm.Location = new System.Drawing.Point(73, 59);
            this.txtYmm.Name = "txtYmm";
            this.txtYmm.Size = new System.Drawing.Size(82, 21);
            this.txtYmm.TabIndex = 4;
            this.txtYmm.TextChanged += new System.EventHandler(this.txtYmm_TextChanged);
            // 
            // txtFdate
            // 
            this.txtFdate.Location = new System.Drawing.Point(73, 86);
            this.txtFdate.Name = "txtFdate";
            this.txtFdate.Size = new System.Drawing.Size(82, 21);
            this.txtFdate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "기간 :";
            // 
            // txtTdate
            // 
            this.txtTdate.Location = new System.Drawing.Point(158, 87);
            this.txtTdate.Name = "txtTdate";
            this.txtTdate.Size = new System.Drawing.Size(82, 21);
            this.txtTdate.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbQfy6);
            this.panel3.Controls.Add(this.rbQfy3Dent);
            this.panel3.Controls.Add(this.rbQfy3);
            this.panel3.Controls.Add(this.rbQfy5);
            this.panel3.Controls.Add(this.rbQfy2Dent);
            this.panel3.Controls.Add(this.rbQfy2);
            this.panel3.Location = new System.Drawing.Point(72, 114);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 73);
            this.panel3.TabIndex = 8;
            // 
            // rbQfy6
            // 
            this.rbQfy6.AutoSize = true;
            this.rbQfy6.Location = new System.Drawing.Point(169, 38);
            this.rbQfy6.Name = "rbQfy6";
            this.rbQfy6.Size = new System.Drawing.Size(47, 16);
            this.rbQfy6.TabIndex = 12;
            this.rbQfy6.Text = "자보";
            this.rbQfy6.UseVisualStyleBackColor = true;
            // 
            // rbQfy3Dent
            // 
            this.rbQfy3Dent.AutoSize = true;
            this.rbQfy3Dent.Location = new System.Drawing.Point(91, 39);
            this.rbQfy3Dent.Name = "rbQfy3Dent";
            this.rbQfy3Dent.Size = new System.Drawing.Size(71, 16);
            this.rbQfy3Dent.TabIndex = 11;
            this.rbQfy3Dent.Text = "보호치과";
            this.rbQfy3Dent.UseVisualStyleBackColor = true;
            // 
            // rbQfy3
            // 
            this.rbQfy3.AutoSize = true;
            this.rbQfy3.Location = new System.Drawing.Point(10, 39);
            this.rbQfy3.Name = "rbQfy3";
            this.rbQfy3.Size = new System.Drawing.Size(47, 16);
            this.rbQfy3.TabIndex = 10;
            this.rbQfy3.Text = "보호";
            this.rbQfy3.UseVisualStyleBackColor = true;
            // 
            // rbQfy5
            // 
            this.rbQfy5.AutoSize = true;
            this.rbQfy5.Location = new System.Drawing.Point(169, 15);
            this.rbQfy5.Name = "rbQfy5";
            this.rbQfy5.Size = new System.Drawing.Size(47, 16);
            this.rbQfy5.TabIndex = 9;
            this.rbQfy5.Text = "산재";
            this.rbQfy5.UseVisualStyleBackColor = true;
            // 
            // rbQfy2Dent
            // 
            this.rbQfy2Dent.AutoSize = true;
            this.rbQfy2Dent.Location = new System.Drawing.Point(91, 16);
            this.rbQfy2Dent.Name = "rbQfy2Dent";
            this.rbQfy2Dent.Size = new System.Drawing.Size(71, 16);
            this.rbQfy2Dent.TabIndex = 8;
            this.rbQfy2Dent.Text = "보험치과";
            this.rbQfy2Dent.UseVisualStyleBackColor = true;
            // 
            // rbQfy2
            // 
            this.rbQfy2.AutoSize = true;
            this.rbQfy2.Checked = true;
            this.rbQfy2.Location = new System.Drawing.Point(10, 16);
            this.rbQfy2.Name = "rbQfy2";
            this.rbQfy2.Size = new System.Drawing.Size(75, 16);
            this.rbQfy2.TabIndex = 7;
            this.rbQfy2.TabStop = true;
            this.rbQfy2.Text = "보험,공상";
            this.rbQfy2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "심사구분 :";
            // 
            // btnMake
            // 
            this.btnMake.Location = new System.Drawing.Point(120, 203);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(75, 23);
            this.btnMake.TabIndex = 10;
            this.btnMake.Text = "재생성";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // ADD0111E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 238);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.txtTdate);
            this.Controls.Add(this.txtFdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYmm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD0111E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "심사번호재생성(ADD0111E)";
            this.Load += new System.EventHandler(this.ADD0111E_Load);
            this.Activated += new System.EventHandler(this.ADD0111E_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbOutPtnt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbInPtnt;
        private System.Windows.Forms.RadioButton rbHan;
        private System.Windows.Forms.RadioButton rbYang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYmm;
        private System.Windows.Forms.TextBox txtFdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbQfy5;
        private System.Windows.Forms.RadioButton rbQfy2Dent;
        private System.Windows.Forms.RadioButton rbQfy2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbQfy6;
        private System.Windows.Forms.RadioButton rbQfy3Dent;
        private System.Windows.Forms.RadioButton rbQfy3;
        private System.Windows.Forms.Button btnMake;
    }
}


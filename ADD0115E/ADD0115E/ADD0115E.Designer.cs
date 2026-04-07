namespace ADD0115E
{
    partial class ADD0115E
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtExdate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExprsn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Location = new System.Drawing.Point(51, 25);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(252, 16);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "19990101 이전 자료가 대상입니다.";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtExdate
            // 
            this.txtExdate.Location = new System.Drawing.Point(93, 64);
            this.txtExdate.Name = "txtExdate";
            this.txtExdate.Size = new System.Drawing.Size(82, 21);
            this.txtExdate.TabIndex = 6;
            this.txtExdate.TextChanged += new System.EventHandler(this.txtExdate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "청구월 :";
            // 
            // txtCnt
            // 
            this.txtCnt.Location = new System.Drawing.Point(281, 64);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(40, 21);
            this.txtCnt.TabIndex = 8;
            this.txtCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCnt.DoubleClick += new System.EventHandler(this.txtCnt_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "대상건수 :";
            // 
            // txtExprsn
            // 
            this.txtExprsn.Location = new System.Drawing.Point(93, 91);
            this.txtExprsn.Name = "txtExprsn";
            this.txtExprsn.Size = new System.Drawing.Size(228, 21);
            this.txtExprsn.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "파기사유 :";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(66, 163);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(107, 23);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "대상 건수 조회";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExp
            // 
            this.btnExp.Location = new System.Drawing.Point(179, 163);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(107, 23);
            this.btnExp.TabIndex = 12;
            this.btnExp.Text = "파기 처리";
            this.btnExp.UseVisualStyleBackColor = true;
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(36, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "파기된 자료는 복구할 수 없습니다.";
            // 
            // ADD0115E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 262);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExp);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtExprsn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCnt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMsg);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ADD0115E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "개인정보파기(ADD0115E)";
            this.Load += new System.EventHandler(this.ADD0115E_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtExdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExprsn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.Label label4;

    }
}


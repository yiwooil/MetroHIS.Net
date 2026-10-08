namespace ADD9902E
{
    partial class ADD9902E_C1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtMST4CD = new System.Windows.Forms.TextBox();
            this.lblFld1qty = new System.Windows.Forms.Label();
            this.txtFld1qty = new System.Windows.Forms.TextBox();
            this.lblFld2qty = new System.Windows.Forms.Label();
            this.txtFld2qty = new System.Windows.Forms.TextBox();
            this.lblFld3qty = new System.Windows.Forms.Label();
            this.txtFld3qty = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "적용일자 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMST4CD
            // 
            this.txtMST4CD.Location = new System.Drawing.Point(139, 28);
            this.txtMST4CD.Name = "txtMST4CD";
            this.txtMST4CD.Size = new System.Drawing.Size(117, 21);
            this.txtMST4CD.TabIndex = 0;
            // 
            // lblFld1qty
            // 
            this.lblFld1qty.Location = new System.Drawing.Point(24, 55);
            this.lblFld1qty.Name = "lblFld1qty";
            this.lblFld1qty.Size = new System.Drawing.Size(108, 21);
            this.lblFld1qty.TabIndex = 6;
            this.lblFld1qty.Text = "요양기관기호 :";
            this.lblFld1qty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFld1qty
            // 
            this.txtFld1qty.Location = new System.Drawing.Point(139, 55);
            this.txtFld1qty.Name = "txtFld1qty";
            this.txtFld1qty.Size = new System.Drawing.Size(117, 21);
            this.txtFld1qty.TabIndex = 1;
            // 
            // lblFld2qty
            // 
            this.lblFld2qty.Location = new System.Drawing.Point(24, 81);
            this.lblFld2qty.Name = "lblFld2qty";
            this.lblFld2qty.Size = new System.Drawing.Size(108, 21);
            this.lblFld2qty.TabIndex = 7;
            this.lblFld2qty.Text = "산재지정기호 :";
            this.lblFld2qty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFld2qty
            // 
            this.txtFld2qty.Location = new System.Drawing.Point(139, 81);
            this.txtFld2qty.Name = "txtFld2qty";
            this.txtFld2qty.Size = new System.Drawing.Size(117, 21);
            this.txtFld2qty.TabIndex = 2;
            // 
            // lblFld3qty
            // 
            this.lblFld3qty.Location = new System.Drawing.Point(24, 107);
            this.lblFld3qty.Name = "lblFld3qty";
            this.lblFld3qty.Size = new System.Drawing.Size(108, 21);
            this.lblFld3qty.TabIndex = 8;
            this.lblFld3qty.Text = "기타 :";
            this.lblFld3qty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFld3qty
            // 
            this.txtFld3qty.Location = new System.Drawing.Point(139, 107);
            this.txtFld3qty.Name = "txtFld3qty";
            this.txtFld3qty.Size = new System.Drawing.Size(117, 21);
            this.txtFld3qty.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 165);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ADD9902E_C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 225);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMST4CD);
            this.Controls.Add(this.lblFld1qty);
            this.Controls.Add(this.txtFld1qty);
            this.Controls.Add(this.lblFld2qty);
            this.Controls.Add(this.txtFld2qty);
            this.Controls.Add(this.lblFld3qty);
            this.Controls.Add(this.txtFld3qty);
            this.Controls.Add(this.btnSave);
            this.Name = "ADD9902E_C1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "요양기관등록(ADD9902E_C1)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMST4CD;
        private System.Windows.Forms.Label lblFld1qty;
        private System.Windows.Forms.TextBox txtFld1qty;
        private System.Windows.Forms.Label lblFld2qty;
        private System.Windows.Forms.TextBox txtFld2qty;
        private System.Windows.Forms.Label lblFld3qty;
        private System.Windows.Forms.TextBox txtFld3qty;
        private System.Windows.Forms.Button btnSave;
    }
}

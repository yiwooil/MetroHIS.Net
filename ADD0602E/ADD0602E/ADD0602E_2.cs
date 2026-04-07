using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0602E
{
    public partial class ADD0602E_2 : Form
    {
        public string m_docuno;

        public ADD0602E_2()
        {
            InitializeComponent();
        }

        public ADD0602E_2(String p_docuno)
            : this()
        {
            txtDocuno.Text = p_docuno;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_docuno = "";
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_docuno = txtDocuno.Text.ToString();
            this.Close();
        }
    }
}

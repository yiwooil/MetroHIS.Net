using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7000E
{
    public partial class ADD7000E_ERR : Form
    {
        public ADD7000E_ERR()
        {
            InitializeComponent();
        }

        public void SetMsg(String msg)
        {
            txtMsg.Text = msg;
        }

        private void ADD7000E_ERR_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}

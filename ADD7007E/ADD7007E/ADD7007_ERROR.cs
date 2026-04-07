using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7007E
{
    public partial class ADD7007_ERROR : Form
    {
        public ADD7007_ERROR()
        {
            InitializeComponent();
        }

        public void SetError(string err_code, string err_desc)
        {
            txtError.Text = err_code +Environment.NewLine + Environment.NewLine + err_desc;
        }
    }
}

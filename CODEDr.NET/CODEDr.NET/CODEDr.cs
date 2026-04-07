using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CODEDr.NET
{
    public partial class CODEDr : Form
    {
        private bool IsFirst;

        public CODEDr()
        {
            InitializeComponent();
        }

        public CODEDr(string drid, string serverIp, string hosid)
            : this()
        {
            txtDrid.Text = drid;
            txtServerIp.Text = serverIp;
            txtHosid.Text = hosid;

            if (drid != "") txtDrid.Enabled = false;
            if (serverIp != "") txtServerIp.Enabled = false;
            if (hosid != "") txtHosid.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int port = 0;
                int.TryParse("2" + txtHosid.Text.ToString(), out port);

                TcpClient client = new TcpClient(txtServerIp.Text.ToString(), port);

                NetworkStream stream = client.GetStream();

                string message = txtDrid.Text.ToString();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                stream.Close();
                client.Close();

                if (response == "success")
                {
                    // 성공함.
                }
                else
                {
                    MessageBox.Show(response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // 성공
                if (txtDrid.Enabled == false && txtServerIp.Enabled == false && txtHosid.Enabled == false)
                {
                    this.Close();
                }
            }
        }

        private void CODEDr_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void CODEDr_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            if (txtDrid.Enabled == false && txtServerIp.Enabled == false && txtHosid.Enabled == false)
            {
                btnSave.PerformClick();
            }
        }
    }
}

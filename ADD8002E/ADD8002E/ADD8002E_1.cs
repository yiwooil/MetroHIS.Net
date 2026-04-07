using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADD8002E
{
    public partial class ADD8002E_1 : Form
    {
        public String m_reqno;

        private Boolean IsFirst;
        private Boolean m_OnPgm;

        public ADD8002E_1()
        {
            InitializeComponent();

            this.SetConfig();
        }

        private void SetConfig()
        {
            try
            {
                m_OnPgm = true;
                //
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD");
                String folder = reg.GetValue("ADD8002E.FOLDER", "").ToString();
                if (folder == "") folder = "C:/hira/DDMD/sam/out";
                txtFolder.Text = folder;
                //
                cboRcvid.Items.Clear();
                String strRcvid = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    string sql = "SELECT FLD2QTY  FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='37'";
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            strRcvid = reader["FLD2QTY"].ToString();
                        }
                        reader.Close();
                    }
                    sql = "SELECT MST3CD, CDNM FROM TA88 WHERE MST1CD='A' AND MST2CD='EDIRCVID'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        OleDbDataReader reader = cmd.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                            String mst3cd = reader["MST3CD"].ToString();
                            String cdnm = reader["CDNM"].ToString();
                            cboRcvid.Items.Add(mst3cd+"-"+cdnm);
                            if (mst3cd == strRcvid) cboRcvid.SelectedIndex = i - 1;
                        }
                        reader.Close();
                    }
                }
                //
                m_OnPgm = false;
            }
            catch (Exception ex)
            {
                m_OnPgm = false;
                MessageBox.Show(ex.Message);
            }
        }


        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReqno.Text.ToString() == "")
                {
                    MessageBox.Show("신청번호가 없습니다.");
                    return;
                }

                if (cboRcvid.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("수신인을 선택하세요");
                    return;
                }

                if (txtFolder.Text.ToString() == "")
                {
                    MessageBox.Show("폴더를 선택하세요");
                    return;
                }

                if (Directory.Exists(txtFolder.Text.ToString()) == false)
                {
                    Directory.CreateDirectory(txtFolder.Text.ToString());
                }

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeSamFile();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("생성이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeSamFile()
        {
            DeleteEveryFile();

            String reqno = txtReqno.Text.ToString();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                this.MakeM0501(conn);
                this.MakeM0502(conn);
                this.MakeMedlogenc(conn);
            }
        }

        private void DeleteEveryFile()
        {
            if (Directory.Exists(txtFolder.Text.ToString()))
            {
                string[] files = Directory.GetFiles(txtFolder.Text.ToString());
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string deletefile = txtFolder.Text.ToString() + "/" + fileName;
                    File.Delete(deletefile);
                }
            }
        }

        private void MakeM0501(OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/M050.1");

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT REQNO,FMNO,HOSID,DDNM,BUYREQTOT,MEMO";
            sql += System.Environment.NewLine + "  FROM TIE_M0501";
            sql += System.Environment.NewLine + " WHERE REQNO=?  ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    sb.Length = 0;
                    sb.Append(Conv(reader["REQNO"].ToString(), "N", 10));
                    sb.Append(Conv(reader["FMNO"].ToString(), "C", 4));
                    sb.Append(Conv(reader["HOSID"].ToString(), "C", 8));
                    sb.Append(Conv(reader["DDNM"].ToString(), "C", 20));
                    sb.Append(Conv(reader["BUYREQTOT"].ToString(), "N", 4));
                    sb.Append(Conv(reader["MEMO"].ToString(), "C", 350));
                }
                reader.Close();
            }

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private void MakeM0502(OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/M050.2");

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT REQNO,REQDIV,ELINENO,ITEMCD,ITEMINFO,STDSIZE,UNIT,BUSINESSCD,TRADENM,PRESNDDIV,BUYDT,BUYQTY,BUYAMT,UNITCOST";
            sql += System.Environment.NewLine + "  FROM TIE_M0502";
            sql += System.Environment.NewLine + " WHERE REQNO=?  ";
            sql += System.Environment.NewLine + " ORDER BY ELINENO";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));

                int lineno = 0;
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lineno++;
                    sb.Length = 0;
                    sb.Append(Conv(reader["REQNO"].ToString(),      "N", 10));
                    sb.Append(Conv(reader["REQDIV"].ToString(),     "C", 1));
                    sb.Append(Conv(reader["ELINENO"].ToString(),    "N", 3));
                    sb.Append(Conv(reader["ITEMCD"].ToString(),     "C", 8));
                    sb.Append(Conv(reader["ITEMINFO"].ToString(),   "C", 140));
                    sb.Append(Conv(reader["STDSIZE"].ToString(),    "C", 140));
                    sb.Append(Conv(reader["UNIT"].ToString(),       "C", 70));
                    sb.Append(Conv(reader["BUSINESSCD"].ToString(), "C", 17));
                    sb.Append(Conv(reader["TRADENM"].ToString(),    "C", 35));
                    sb.Append(Conv(reader["PRESNDDIV"].ToString(),  "C", 1));
                    sb.Append(Conv(reader["BUYDT"].ToString(),      "N", 6));
                    sb.Append(Conv(reader["BUYQTY"].ToString(),     "N", 7));
                    sb.Append(Conv(reader["BUYAMT"].ToString(),     "N", 10));
                    sb.Append(Conv(reader["UNITCOST"].ToString(),   "N", 10));

                    String strLine = sb.ToString();
                    if (lineno > 1) sw.Write(Environment.NewLine);
                    sw.Write(strLine);
                }
                reader.Close();
            }
            sw.Close();
        }

        private void MakeMedlogenc(OleDbConnection p_conn)
        {
            String strRcvid = cboRcvid.SelectedItem.ToString();
            strRcvid = (strRcvid + "-").Split('-')[0];

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/MEDLOG.ENC");

            StringBuilder sb = new StringBuilder();

            sb.Length = 0;
            sb.Append(Conv(strRcvid, "C", 12));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("NULL", "C", 8));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("산재보험 구입고서", "C", 30));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("", "C", 12));

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private String Conv(String value, String Type, int Len)
        {
            if (Type == "N")
            {
                String ret = value.PadLeft(Len, '0');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
            else
            {
                String ret = value.PadRight(Len, ' ');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
        }

        private void ShowProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormCaption(caption);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        private void CloseProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
        }

        private void ADD8002E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD8002E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            txtReqno.Text = m_reqno;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtFolder.Text.ToString();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
            }
        }

        private void cboRcvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_OnPgm) return;

            try
            {
                String strRcvid = cboRcvid.SelectedItem.ToString();
                strRcvid = (strRcvid + "-").Split('-')[0];

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TA88";
                    sql += System.Environment.NewLine + "   SET FLD2QTY=?";
                    sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                    sql += System.Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                    sql += System.Environment.NewLine + "   AND MST3CD='37'";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", strRcvid));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

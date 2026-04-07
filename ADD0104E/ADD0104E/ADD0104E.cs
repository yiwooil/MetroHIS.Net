using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0104E
{
    public partial class ADD0104E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private bool IsFirst;
        private string m_pgm_step = ""; // 2022.01.12 WOOIL - 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD0104E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD0104E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD0104E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0104E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            SetInit();

        }

        private void SetInit()
        {
            try
            {
                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT MST3CD,CDNM";
                sql += Environment.NewLine + "  FROM TA88 (NOLOCK)";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='26'";
                sql += Environment.NewLine + " ORDER BY MST3CD";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string strHdate = MetroLib.Util.GetSysDate(conn);
                    txtYYMM.Text = strHdate.Substring(0, 6);


                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        cboQfycd.Items.Add(reader["MST3CD"].ToString() + "." + reader["CDNM"].ToString());
                        return true;
                    });

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            if (txtPid.Text.ToString() == "") return;
            ReadPnm();
        }

        private void txtPid_Leave(object sender, EventArgs e)
        {
            if (txtPid.Text.ToString() == "") return;
            ReadPnm();
        }

        private void ReadPnm()
        {
            if (txtPid.Text.ToString().Length < 9)
            {
                string pid = txtPid.Text.ToString();
                pid = pid.PadLeft(9, '0');
                txtPid.Text = pid;
            }

            ReadTA01(txtPid.Text.ToString());
        }

        private void ReadTA01(string p_pid)
        {
            string sql = "";
            sql = "";
            sql = "SELECT PNM FROM TA01 WHERE PID='" + p_pid + "'";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) txtPnm.Text = reader["PNM"].ToString();
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtYYMM.Text.ToString() == "")
            {
                MessageBox.Show("청구년월을 입력하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            string gbn = "1"; // 양방
            if (rbYHgbn2.Checked) gbn = "2"; // 한방
            string qfy = "2"; // 보험.공상
            if (rbQfy3.Checked) qfy = "3"; // 보호
            else if (rbQfy5.Checked) qfy = "5"; // 산재
            else if (rbQfy6.Checked) qfy = "6"; // 자보
            else if (rbQfy38.Checked) qfy = "38"; // 보호정신과
            else if (rbQfy29.Checked) qfy = "29"; // 보훈일반

            string yymm = txtYYMM.Text.ToString();
            string pid = txtPid.Text.ToString();


            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT A.SIMNO";
            sql = sql + Environment.NewLine + "     , A.EPRTNO";
            sql = sql + Environment.NewLine + "     , A.PID";
            sql = sql + Environment.NewLine + "     , A.PNM";
            sql = sql + Environment.NewLine + "     , A.QFYCD";
            sql = sql + Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
            sql = sql + Environment.NewLine + "     , A.DONFG";
            sql = sql + Environment.NewLine + "     , A.STEDT";
            sql = sql + Environment.NewLine + "     , A.DEMNO";
            sql = sql + Environment.NewLine + "     , A.BDODT";
            sql = sql + Environment.NewLine + "     , A.QFYCD";
            sql = sql + Environment.NewLine + "     , A.JRBY";
            sql = sql + Environment.NewLine + "     , A.PID";
            sql = sql + Environment.NewLine + "     , A.UNISQ";
            sql = sql + Environment.NewLine + "     , A.SIMCS";
            sql = sql + Environment.NewLine + "  FROM TI2A A";
            sql = sql + Environment.NewLine + " WHERE A.BDODT LIKE '" + yymm + "%'";
            sql = sql + Environment.NewLine + "   AND ISNULL(A.SIMFG,'')<>'' ";
            if (qfy == "2")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('21','22','23','40') ";
            }
            else if (qfy == "3")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('31','32') ";
            }
            else if (qfy == "5")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('50') ";
            }
            else if (qfy == "6")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('61') ";
            }
            else if (qfy == "38")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('38','39') ";
            }
            else if (qfy == "29")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD = '29' "; // 2006.05.23 WOOIL - 보훈일반 추가
            }
            if (pid != "")
            {
                sql = sql + Environment.NewLine + "   AND A.PID='" + pid + "'";
            }
            sql = sql + Environment.NewLine + "   AND A.SIMCS>0";
            sql = sql + Environment.NewLine + " ORDER BY A.BDODT,A.QFYCD,A.SIMNO ";

            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.BDODT = reader["BDODT"].ToString();
                    data.SIMNO = reader["SIMNO"].ToString();
                    data.EPRTNO = reader["EPRTNO"].ToString();
                    data.PID = reader["PID"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.QFYCD = reader["QFYCD"].ToString();
                    data.DPTCD = reader["DPTCD"].ToString();
                    data.DONFG = reader["DONFG"].ToString();
                    data.STEDT = reader["STEDT"].ToString();
                    data.JRBY = reader["JRBY"].ToString();
                    data.UNISQ = reader["UNISQ"].ToString();
                    data.SIMCS = reader["SIMCS"].ToString();

                    list.Add(data);

                    return true;
                });
                conn.Close();
            }

            this.RefreshGridMain();
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

        private void RefreshGridMain()
        {
            if (grdMain.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdMain.BeginInvoke(new Action(() => grdMainView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdMainView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string bdodt = grdMainView.GetRowCellValue(e.RowHandle, "BDODT").ToString();
                string qfycd = grdMainView.GetRowCellValue(e.RowHandle, "QFYCD").ToString();
                string dptcd = grdMainView.GetRowCellValue(e.RowHandle, "DPTCD").ToString();
                string pid = grdMainView.GetRowCellValue(e.RowHandle, "PID").ToString();

                txtAfterBdodt.Text = bdodt.Substring(0, 6);
                txtAfterQfycd.Text = qfycd;
                txtAfterDptcd.Text = dptcd;
                txtAfterPid.Text = pid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Save()
        {
            string strBfBdodt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "BDODT").ToString();
            string strBfQfycd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "QFYCD").ToString();
            string strBfJrby = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JRBY").ToString();
            string strBfPid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PID").ToString();
            string strBfUnisq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "UNISQ").ToString();
            string strBfSimcs = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "SIMCS").ToString();

            string strAfBdodt = txtAfterBdodt.Text.ToString();
            string strAfNewQfycd = "";
            if (txtChangQF.Text.ToString() != "" && txtChangQF.Text.ToString() != txtAfterQfycd.Text.ToString())
            {
                strAfNewQfycd = txtChangQF.Text.ToString();
            }
            string strBoRyu = (chkBoRyu.Checked == true ? "1" : "");

            string addpara = "";
            addpara += "2" + (char)21;
            addpara += strBfBdodt + (char)21;
            addpara += strBfQfycd + (char)21;
            addpara += strBfJrby + (char)21;
            addpara += strBfPid + (char)21;
            addpara += strBfUnisq + (char)21;
            addpara += strBfSimcs + (char)21;
            addpara += "Y" + (char)21;
            addpara += strAfBdodt + (char)21;
            addpara += strAfNewQfycd + (char)21;
            addpara += strBoRyu;

            string para = "user=" + m_User + ",pwd=" + m_Pwd + ",prjcd=ADD,addpara=" + addpara;

            string exefile = "C:/Metro/DLL/ADD0110E.exe";
            string exefolder = "C:/Metro/DLL/";
            this.ExecCmd(exefile, exefolder, para);

        }

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

        private void cboQfycd_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChangQF.Text = cboQfycd.SelectedItem.ToString().Substring(0, 2);
        }
        
    }
}

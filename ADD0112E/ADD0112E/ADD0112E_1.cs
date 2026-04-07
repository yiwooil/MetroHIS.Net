using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0112E
{
    public partial class ADD0112E_1 : Form
    {
        public String m_User;
        public String m_Prjcd;
        public string m_pid;

        public ADD0112E_1()
        {
            InitializeComponent();
        }

        private void ADD0112E_1_Load(object sender, EventArgs e)
        {
            m_pid = "";
        }

        private void txtPnm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPnm.Text.ToString() == "") return;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.ShowProgressForm("", "조회중입니다.");
                    this.FindByPnm(txtPnm.Text.ToString());
                    this.CloseProgressForm("", "조회중입니다.");
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.CloseProgressForm("", "조회중입니다.");
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FindByPnm(string p_pnm)
        {
            string resid = p_pnm;
            if (resid.Length > 6) resid = resid.Substring(0, 6);

            List<CPatient> list = new List<CPatient>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql="";
                sql = sql + Environment.NewLine + "SELECT A01.PID";
                sql = sql + Environment.NewLine + "     , A01.PNM";
                sql = sql + Environment.NewLine + "     , A01.RESID";
                sql = sql + Environment.NewLine + "     , ISNULL((select max(exdt)";
                sql = sql + Environment.NewLine + "                 from ts21 x (nolock) ";
                sql = sql + Environment.NewLine + "                where x.pid = a01.pid";
                sql = sql + Environment.NewLine + "                  and isnull(x.ccfg,'') in ('','0') ";
                sql = sql + Environment.NewLine + "                  and ((isnull(x.exdof,'') in ('1','2') and x.exdt < convert(varchar,getdate(),112)) ";
                sql = sql + Environment.NewLine + "                    or (isnull(x.exdof,'') in ('0','1','2') and x.exdt = convert(varchar,getdate(),112)))),' ') LSTDT";
                sql = sql + Environment.NewLine + "     , (SELECT CDNM FROM TA88 WHERE MST1CD = 'A' AND MST2CD = '26' AND MST3CD = A10.QLFYCD) QFYNM";
                sql = sql + Environment.NewLine + "     , A04.BEDEDT";
                sql = sql + Environment.NewLine + "     , A01.HTELNO";
                sql = sql + Environment.NewLine + "     , A01.OTELNO";
                sql = sql + Environment.NewLine + "     , ISNULL(A01.ADDR1,'')+' '+ISNULL(A01.ADDR2,'') ADDR";
                sql = sql + Environment.NewLine + "     , (SELECT TOP 1 INSNM FROM TA56 X (nolock) WHERE X.PID = A10.PID AND X.QLFYCD = A10.QLFYCD ORDER BY CREDT DESC) INSNM";
                sql = sql + Environment.NewLine + "     , ISNULL(A01.DRRMK,'')+CASE WHEN ISNULL(A01.WONRMK,'') <> '' THEN '['+ISNULL(A01.WONRMK,'')+']' ELSE ISNULL(A01.WONRMK,'') END RMK";
                sql = sql + Environment.NewLine + "  FROM TA01 A01 (nolock) INNER JOIN TA10 A10 (nolock) ON A10.PID = A01.PID";
                sql = sql + Environment.NewLine + "                         LEFT JOIN TA04 A04 (nolock) ON A04.PID = A01.PID AND A04.BEDEDT = (SELECT MAX(A.BEDEDT) FROM TA04 A (nolock) WHERE A.PID = A01.PID AND ISNULL(A.BEDIPTHCD,'') NOT IN ('0')) ";
                sql = sql + Environment.NewLine + " WHERE A01.PNM LIKE ? ";
                sql = sql + Environment.NewLine + "    OR A01.RESID1 = ? ";
                sql = sql + Environment.NewLine + " ORDER BY A01.RESID, A01.PNM, A01.PID ";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", p_pnm + "%"));
                    cmd.Parameters.Add(new OleDbParameter("@2", resid));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CPatient ptnt = new CPatient();
                        ptnt.PID = reader["PID"].ToString();
                        ptnt.PNM = reader["PNM"].ToString();
                        ptnt.RESID = reader["RESID"].ToString();
                        ptnt.LSTDT = reader["LSTDT"].ToString();
                        ptnt.QFYNM = reader["QFYNM"].ToString();
                        ptnt.BEDEDT = reader["BEDEDT"].ToString();
                        ptnt.HTELNO = reader["HTELNO"].ToString();
                        ptnt.OTELNO = reader["OTELNO"].ToString();
                        ptnt.ADDR = reader["ADDR"].ToString();
                        ptnt.INSNM = reader["INSNM"].ToString();
                        ptnt.RMK = reader["RMK"].ToString();
                        list.Add(ptnt);
                    }
                }
                if (list.Count > 0)
                {
                    MetroLib.Util.WritePIICLog(m_User, m_Prjcd, this.Name, "열람", p_pnm, list.Count.ToString(), "FindByPnm", conn);
                }

                conn.Close();
            }
            this.RefreshGridMain();
            if (list.Count > 0) grdMain.Focus();
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            SelectPatient();
        }

        private void grdMainView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SelectPatient();
            }
        }

        private void SelectPatient()
        {
            string pid = grdMainView.GetRowCellValue(grdMainView.GetSelectedRows()[0], gcPID).ToString();
            if (pid == "") return;
            m_pid = pid;
            this.Close();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != grdMain) return;
            var info = grdMainView.CalcHitInfo(e.ControlMousePosition);
            if (info.InRowCell && info.RowHandle != -1 && info.Column != null)
            {
                string toolTipText = "";
                if (info.Column == gcRESID)
                {
                    string pid = grdMainView.GetRowCellValue(info.RowHandle, gcPID).ToString();
                    List<CPatient> list = (List<CPatient>)grdMain.DataSource;
                    foreach (CPatient ptnt in list)
                    {
                        if (ptnt.PID == pid)
                        {
                            toolTipText = ptnt.RESID_NO_MASK;
                            break;
                        }
                    }
                }
                else
                {
                    toolTipText = grdMainView.GetRowCellValue(info.RowHandle, info.Column).ToString();
                }
                e.Info = new DevExpress.Utils.ToolTipControlInfo("", toolTipText);
            }
        }

        /*
        private void WritePIICLog(string usrid, string prjid, string frmnm, string job, string ippm, string oprv, string remark, OleDbConnection p_conn)
        {
            string seq = "";
            string hostip = "";
            string hostnm = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(hostnm);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    hostip = ip.ToString();
                    break;
                }
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ),0) + 1 NEXT_SEQ";
            sql = sql + Environment.NewLine + "  FROM TA94C_PIICL";
            sql = sql + Environment.NewLine + " WHERE USRIP=?";
            sql = sql + Environment.NewLine + "   AND USRID=?";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    seq = reader["NEXT_SEQ"].ToString();
                }
            }

            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TA94C_PIICL(USRIP, USRID, SEQ, PRJID, FRMNM, JOB, IPPM, OPRV, REMARK, HSTNM, ENTDT, ENTTM)";
            sql = sql + Environment.NewLine + "VALUES";
            sql = sql + Environment.NewLine + "(?,?,?,?,?,?,?,?,?,?";
            sql = sql + Environment.NewLine + ",CONVERT(VARCHAR,GETDATE(),112),LEFT(REPLACE(CONVERT(VARCHAR,GETDATE(),14),':',''),6))";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));
                cmd.Parameters.Add(new OleDbParameter("@3", seq));
                cmd.Parameters.Add(new OleDbParameter("@4", prjid));
                cmd.Parameters.Add(new OleDbParameter("@5", frmnm));
                cmd.Parameters.Add(new OleDbParameter("@6", job));
                cmd.Parameters.Add(new OleDbParameter("@7", ippm));
                cmd.Parameters.Add(new OleDbParameter("@8", oprv));
                cmd.Parameters.Add(new OleDbParameter("@9", remark));
                cmd.Parameters.Add(new OleDbParameter("@10", hostnm));

                cmd.ExecuteNonQuery();
            }
        }
        */
    }
}

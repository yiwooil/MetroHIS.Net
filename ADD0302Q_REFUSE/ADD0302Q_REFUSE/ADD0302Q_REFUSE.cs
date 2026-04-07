using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0302Q_REFUSE
{
    public partial class ADD0302Q_REFUSE : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;

        private bool IsFirst;

        private string m_PgmPos;

        public ADD0302Q_REFUSE()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";
        }

        public ADD0302Q_REFUSE(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_HospMulti = GetHospmulti();
        }

        private string GetHospmulti()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@2", m_Prjcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["MULTIFG"].ToString();
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void ADD0302Q_REFUSE_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0302Q_REFUSE_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                Init();
                txtExdate.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init()
        {
            string sysDate = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sysDate = MetroLib.Util.GetSysDate(conn);

                conn.Close();
            }

            if (sysDate.Length >= 6) txtExdate.Text = sysDate.Substring(0, 6);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string exdate = txtExdate.Text;
                if (exdate.Length != 6)
                {
                    MessageBox.Show("청구월을 확인하세요.");
                    return;
                }

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
            string yhgbn = rbYHgbn1.Checked ? "1" : "2";
            string iofg = rbIofg1.Checked ? "1" : "2";
            string exdate = txtExdate.Text;

            string simfg = "1";
            if (rbQfy2.Checked) simfg = "1";//보험
            else if (rbQfy2Dent.Checked) simfg = "2";// 보험치과
            else if (rbQfy3.Checked) simfg = "3";// 보호
            else if (rbQfy3Dent.Checked) simfg = "4";//보호치과
            else if (rbQfy5.Checked) simfg = "5";//산재
            else if (rbQfy6.Checked) simfg = "6";//자보
            else if (rbQfy2Han.Checked) simfg = "7";//보험한방
            else if (rbQfy3Han.Checked) simfg = "8";//보호한방
            else if (rbQfy4Han.Checked) simfg = "8";//공상한방
            else if (rbQfy4.Checked) simfg = "3";//공상
            else if (rbQfy29.Checked) simfg = "10";//보훈일반

            string jrby = "";
            if (rbJrby1.Checked) jrby = "1";//내과
            else if (rbJrby2.Checked) jrby = "2";//외과
            else if (rbJrby3.Checked) jrby = "3";//산소
            else if (rbJrby4.Checked) jrby = "4";//안이
            else if (rbJrby5.Checked) jrby = "5";//피비
            else if (rbJrby6.Checked) jrby = "6";//치과
            else if (rbJrby7.Checked) jrby = "7";//한방

            string donfg = "ALL";
            if (rbDonfg_.Checked) donfg = "";//기초
            else if (rbDonfgN.Checked) donfg = "N";//미완
            else if (rbDonfgP.Checked) donfg = "P";//보류
            else if (rbDonfgY.Checked) donfg = "Y";//완료
            else if (rbDonfgX.Checked) donfg = "X";//청구안함

            string pid = txtPid.Text.ToString();
            string multifg = m_HospMulti;

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            if (iofg == "1")
            {
                sql="";
                sql += Environment.NewLine + "SELECT A.EXDATE REQMM";
                sql += Environment.NewLine + "     , A.SIMNO";
                sql += Environment.NewLine + "     , A.EPRTNO";
                sql += Environment.NewLine + "     , A.PID";
                sql += Environment.NewLine + "     , A.PNM";
                sql += Environment.NewLine + "     , A.QFYCD";
                sql += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) DPTCD";
                sql += Environment.NewLine + "     , A.DONFG";
                sql += Environment.NewLine + "     , A.RESID";
                sql += Environment.NewLine + "     , A.DEMNO";
                sql += Environment.NewLine + "     , A.GONSGB";
                sql += Environment.NewLine + "     , A.DAETC";
                sql += Environment.NewLine + "     , A.TJKH";
                sql += Environment.NewLine + "     , A.STEDT";
                sql += Environment.NewLine + "     , R.RMPID";
                sql += Environment.NewLine + "     , R.EXDT RMPDT";
                sql += Environment.NewLine + "     , R.HMS RMPTM";
                sql += Environment.NewLine + "     , R.REASON AS RSN";
                sql += Environment.NewLine + "     , A.EXDATE AS K1";
                sql += Environment.NewLine + "     , A.QFYCD AS K2";
                sql += Environment.NewLine + "     , A.JRBY AS K3";
                sql += Environment.NewLine + "     , A.PID AS K4";
                sql += Environment.NewLine + "     , A.UNISQ AS K5";
                sql += Environment.NewLine + "     , A.SIMCS AS K6";
                sql += Environment.NewLine + "     , A.DELID";
                sql += Environment.NewLine + "     , A.SIMFG";
                sql += Environment.NewLine + "  FROM TI1_REFUSE R INNER JOIN TI1A_B A ON A.DELID   = R.DELID ";
                sql += Environment.NewLine + "                                       AND A.EXDATE  = R.EXDATE ";
                sql += Environment.NewLine + "                                       AND A.QFYCD   = R.QFYCD ";
                sql += Environment.NewLine + "                                       AND A.JRBY    = R.JRBY ";
                sql += Environment.NewLine + "                                       AND A.PID     = R.PID ";
                sql += Environment.NewLine + "                                       AND A.UNISQ   = R.UNISQ ";
                sql += Environment.NewLine + "                                       AND A.SIMCS   = R.SIMCS ";
                sql += Environment.NewLine + "                    INNER JOIN TA09 A09 ON A09.DPTCD= DBO.MFN_PIECE(A.JRKWA,'$',3)";
                sql += Environment.NewLine + " WHERE A.EXDATE LIKE ? ";
                sql += Environment.NewLine + "   AND A.SIMFG  = ? ";
                sql += Environment.NewLine + "   AND A.SIMCS  > 0 ";
                sql += Environment.NewLine + "   AND ISNULL(A09.ADDDPTCD,'')=? ";
                sql += Environment.NewLine + "   AND ISNULL(R.DELFG,'') NOT IN ('D','U') ";

                // 특정진료분야만을 원할때
                if (jrby != "")
                {
                    sql += Environment.NewLine + "   AND A.JRKWA LIKE ? ";
                }

                // 특정환자만을 원할때
                if (pid != "")
                {
                    sql += Environment.NewLine + "   AND A.PID = ? ";
                }

                // 특정심사상태를 원할때
                if (donfg != "ALL")
                {
                    sql += Environment.NewLine + "   AND ISNULL(A.DONFG,'') = ? ";
                }

                // 2010.04.30 WOOIL - 개발자가 아니면 개발자가 삭제한 내역은 보이지 않게 한다.
                if (m_User.ToUpper() != "MMS")
                {
                    sql += Environment.NewLine + "   AND R.RMPID NOT LIKE 'MMS%' ";
                }

                sql += Environment.NewLine + " ORDER BY R.EXDT DESC,A.EXDATE, A.SIMFG, A.SIMNO ";
            }
            else
            {
                sql = "";
                sql += Environment.NewLine + "SELECT LEFT(A.BDODT,6) AS REQMM";
                sql += Environment.NewLine + "     , A.SIMNO";
                sql += Environment.NewLine + "     , A.EPRTNO";
                sql += Environment.NewLine + "     , A.PID";
                sql += Environment.NewLine + "     , A.PNM";
                sql += Environment.NewLine + "     , A.QFYCD";
                sql += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) DPTCD";
                sql += Environment.NewLine + "     , A.DONFG";
                sql += Environment.NewLine + "     , A.RESID";
                sql += Environment.NewLine + "     , A.GONSGB";
                sql += Environment.NewLine + "     , A.DAETC";
                sql += Environment.NewLine + "     , A.TJKH";
                sql += Environment.NewLine + "     , A.STEDT";
                sql += Environment.NewLine + "     , R.RMPID";
                sql += Environment.NewLine + "     , R.EXDT RMPDT";
                sql += Environment.NewLine + "     , R.HMS RMPTM";
                sql += Environment.NewLine + "     , R.REASON AS RSN";
                sql += Environment.NewLine + "     , A.BDODT AS K1";
                sql += Environment.NewLine + "     , A.QFYCD AS K2";
                sql += Environment.NewLine + "     , A.JRBY AS K3";
                sql += Environment.NewLine + "     , A.PID AS K4";
                sql += Environment.NewLine + "     , A.UNISQ AS K5";
                sql += Environment.NewLine + "     , A.SIMCS AS K6";
                sql += Environment.NewLine + "     , A.DELID";
                sql += Environment.NewLine + "     , A.SIMFG";
                sql += Environment.NewLine + "  FROM TI2_REFUSE R INNER JOIN TI2A_B A ON A.DELID  = R.DELID ";
                sql += Environment.NewLine + "                                       AND A.BDODT  = R.BDODT ";
                sql += Environment.NewLine + "                                       AND A.QFYCD  = R.QFYCD ";
                sql += Environment.NewLine + "                                       AND A.JRBY   = R.JRBY ";
                sql += Environment.NewLine + "                                       AND A.PID    = R.PID ";
                sql += Environment.NewLine + "                                       AND A.UNISQ  = R.UNISQ ";
                sql += Environment.NewLine + "                                       AND A.SIMCS  = R.SIMCS ";
                sql += Environment.NewLine + "                    INNER JOIN TA09 A09 ON A09.DPTCD= DBO.MFN_PIECE(A.JRKWA,'$',3)";
                sql += Environment.NewLine + " WHERE A.BDODT LIKE ? ";
                sql += Environment.NewLine + "   AND A.SIMFG = ? ";
                sql += Environment.NewLine + "   AND A.SIMCS > 0 ";
                sql += Environment.NewLine + "   AND ISNULL(A09.ADDDPTCD,'')=? ";
                sql += Environment.NewLine + "   AND ISNULL(R.DELFG,'') NOT IN ('D','U') ";

                // 특정진료분야만을 원할때
                if (jrby != "")
                {
                    sql += Environment.NewLine + "   AND A.JRKWA LIKE ? ";
                }

                // 특정환자만을 원할때
                if (pid != "")
                {
                    sql += Environment.NewLine + "   AND A.PID = ? ";
                }

                // 특정심사상태를 원할때
                if (donfg != "ALL")
                {
                    sql += Environment.NewLine + "   AND ISNULL(A.DONFG,'') = ? ";
                }

                // 2010.04.30 WOOIL - 개발자가 아니면 개발자가 삭제한 내역은 보이지 않게 한다.
                if (m_User.ToUpper() != "MMS")
                {
                    sql += Environment.NewLine + "   AND R.RMPID NOT LIKE 'MMS%' ";
                }

                sql += Environment.NewLine + " ORDER BY R.BDODT DESC,A.BDODT, A.SIMFG, A.SIMNO ";
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@", exdate + "%"));
                    cmd.Parameters.Add(new OleDbParameter("@", simfg));
                    cmd.Parameters.Add(new OleDbParameter("@", multifg));
                    if (jrby != "") cmd.Parameters.Add(new OleDbParameter("@", jrby + "%"));
                    if (pid != "") cmd.Parameters.Add(new OleDbParameter("@", pid));
                    if (donfg != "ALL") cmd.Parameters.Add(new OleDbParameter("@", donfg));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.REQMM = reader["REQMM"].ToString();
                        data.PID = reader["PID"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.RESID = reader["RESID"].ToString();
                        data.DONFG = reader["DONFG"].ToString();
                        data.SIMNO = reader["SIMNO"].ToString();
                        data.EPRTNO = reader["EPRTNO"].ToString();
                        data.STEDT = reader["STEDT"].ToString();
                        data.QFYCD = reader["QFYCD"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.GONSGB = reader["GONSGB"].ToString();
                        data.DAETC = reader["DAETC"].ToString();
                        data.TJKH = reader["TJKH"].ToString();
                        data.RMPID = reader["RMPID"].ToString();
                        data.RMPDT = reader["RMPDT"].ToString();
                        data.RMPTM = reader["RMPTM"].ToString();
                        data.RSN = reader["RSN"].ToString();
                        data.K1 = reader["K1"].ToString();
                        data.K2 = reader["K2"].ToString();
                        data.K3 = reader["K3"].ToString();
                        data.K4 = reader["K4"].ToString();
                        data.K5 = reader["K5"].ToString();
                        data.K6 = reader["K6"].ToString();
                        data.DELID = reader["DELID"].ToString();
                        data.SIMFG = reader["SIMFG"].ToString();

                        list.Add(data);

                    }
                    reader.Close();
                }

                // 주민번호가 *이 경우 TI1AR, TI2AR에서 읽는다.
                if (iofg == "1")
                {
                    sql = "SELECT RESID FROM TI1AR_B WHERE DELID=? AND EXDATE=? AND QFYCD=? AND JRBY=? AND PID=? AND UNISQ=? AND SIMCS=?";
                }
                else
                {
                    sql = "SELECT RESID FROM TI2AR_B WHERE DELID=? AND BDODT=? AND QFYCD=? AND JRBY=? AND PID=? AND UNISQ=? AND SIMCS=?";
                }
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    foreach (CData data in list)
                    {
                        if (data.RESID == "*")
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new OleDbParameter("@1", data.DELID));
                            cmd.Parameters.Add(new OleDbParameter("@2", data.K1));
                            cmd.Parameters.Add(new OleDbParameter("@3", data.K2));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.K3));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.K4));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.K5));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.K6));

                            OleDbDataReader reader = cmd.ExecuteReader();
                            if (reader.Read()) data.RESID = reader["RESID"].ToString();
                            reader.Close();
                        }
                    }
                }

                if (list.Count > 0)
                {
                    MetroLib.Util.WritePIICLog(m_User, m_Prjcd, this.Name, "열람", txtExdate.Text.ToString() + "," + txtPid.Text.ToString(), list.Count.ToString(), "Query", conn);
                }

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

        private void rbIofg1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIofg1.Checked) grdMain.DataSource = null;
        }

        private void rbIofg2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIofg2.Checked) grdMain.DataSource = null;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_PgmPos = "";
                this.Undo();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
                btnQuery.PerformClick();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "(" + m_PgmPos + ")");
            }
        }

        private void Undo()
        {
            string iofg = rbIofg1.Checked ? "1" : "2";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    for (int row = 0; row < grdMainView.RowCount; row++)
                    {
                        bool op = (bool)grdMainView.GetRowCellValue(row, gcOP);
                        if (op)
                        {
                            string pnm = grdMainView.GetRowCellValue(row, gcPNM).ToString();
                            this.ShowProgressForm("", pnm + "환자 삭제 내역 복구 중입니다.");


                            string exdate = grdMainView.GetRowCellValue(row, gcK1).ToString();
                            string qfycd = grdMainView.GetRowCellValue(row, gcK2).ToString();
                            string jrby = grdMainView.GetRowCellValue(row, gcK3).ToString();
                            string pid = grdMainView.GetRowCellValue(row, gcK4).ToString();
                            string unisq = grdMainView.GetRowCellValue(row, gcK5).ToString();
                            string simcs = grdMainView.GetRowCellValue(row, gcK6).ToString();
                            string delid = grdMainView.GetRowCellValue(row, gcDELID).ToString();
                            string simfg = grdMainView.GetRowCellValue(row, gcSIMFG).ToString();

                            string new_unisq = "";
                            string new_simno = "";

                            if (iofg == "1")
                            {
                                m_PgmPos = "GetNewUnisq";
                                new_unisq = GetNewUnisq(iofg, exdate, qfycd, jrby, pid, unisq, conn, tran);
                                m_PgmPos = "GetSimno1";
                                new_simno = GetSimno1(exdate, simfg, conn, tran);

                                m_PgmPos = "UNDO_1A";
                                UNDO_1A(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, new_simno, conn, tran);
                                m_PgmPos = "UNDO_1B";
                                UNDO_1B(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_1E";
                                UNDO_1E(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_1F";
                                UNDO_1F(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_1H";
                                UNDO_1H(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_1J";
                                UNDO_1J(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2006.04.25 WOOIL 추가
                                m_PgmPos = "UNDO_13";
                                UNDO_13(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_14";
                                UNDO_14(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                m_PgmPos = "UNDO_12";
                                UNDO_12(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2006.04.25 WOOIL 추가
                                m_PgmPos = "UNDO_15";
                                UNDO_15(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2006.04.25 WOOIL 추가
                                m_PgmPos = "UNDO_13T";
                                UNDO_13T(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2007.10.31 WOOIL
                                m_PgmPos = "UNDO_1AA";
                                UNDO_1AA(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2007.07.24 WOOIL
                                m_PgmPos = "UNDO_1EA";
                                UNDO_1EA(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2007.07.24 WOOIL
                                m_PgmPos = "UNDO_1FA";
                                UNDO_1FA(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2007.07.24 WOOIL
                                m_PgmPos = "UNDO_1K";
                                UNDO_1K(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2012.06.21 WOOIL - DRG진료내역
                                m_PgmPos = "UNDO_12A";
                                UNDO_12A(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2012.07.10 WOOIL 추가
                                m_PgmPos = "UNDO_15A";
                                UNDO_15A(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2012.07.10 WOOIL 추가
                                m_PgmPos = "UNDO_1AR";
                                UNDO_1AR(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2019.05.17 WOOIL 추가

                                m_PgmPos = "UNDO_20";
                                UNDO_20(iofg, delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2009.09.22 WOOIL - 심사자메모
                                m_PgmPos = "UNDO_1REFUSE";
                                UNDO_1REFUSE(delid, exdate, qfycd, jrby, pid, unisq, simcs, conn, tran);  // 2008.03.07
                                m_PgmPos = "";
                            }
                            else
                            {
                                new_unisq = GetNewUnisq(iofg, exdate, qfycd, jrby, pid, unisq, conn, tran);
                                new_simno = GetSimno2(exdate, simfg, conn, tran);

                                UNDO_2A(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, new_simno, conn, tran);
                                UNDO_2B(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_2E(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_2F(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_2H(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_2J(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_23(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_24(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);
                                UNDO_23T(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2007.10.31 WOOIL
                                UNDO_22(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2009.04.21 WOOIL
                                UNDO_2K(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran);  // 2012.06.21 WOOIL - DRG진료내역
                                UNDO_2AR(delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2019.05.17 WOOIL 추가

                                UNDO_20(iofg, delid, exdate, qfycd, jrby, pid, unisq, simcs, new_unisq, conn, tran); // 2009.09.22 WOOIL - 심사자메모
                                UNDO_2REFUSE(delid, exdate, qfycd, jrby, pid, unisq, simcs, conn, tran);  // 2008.03.07
                            }
                        }
                    }

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private string GetNewUnisq(string iofg, string exdate, string qfycd, string jrby, string pid, string unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int new_unisq = 0;
            int.TryParse(unisq, out new_unisq);

            string sql = "";
            if (iofg == "1")
            {
                sql = "SELECT COUNT(*) AS CNT  FROM TI1A WHERE EXDATE=? AND QFYCD=? AND JRBY=? AND PID=? AND UNISQ=? ";
            }
            else
            {
                sql = "SELECT COUNT(*) AS CNT FROM TI2A WHERE BDODT=? AND QFYCD=? AND JRBY=? AND PID=? AND UNISQ=? ";
            }
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                while (true)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@4", pid));
                    cmd.Parameters.Add(new OleDbParameter("@5", new_unisq));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    int cnt = 0;
                    if (reader.Read()) int.TryParse(reader["CNT"].ToString(), out cnt);
                    reader.Close();

                    if (cnt <= 0) break;
                    new_unisq++;
                }
            }

            return new_unisq.ToString();
        }

        private string GetSimno1(string exdate, string simfg, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int simno = 0;
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0) AS MAXSIMNO ";
            sql += Environment.NewLine + "  FROM TI1A ";
            sql += Environment.NewLine + " WHERE EXDATE = ? ";
            sql += Environment.NewLine + "   AND SIMFG  = ? ";


            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                cmd.Parameters.Add(new OleDbParameter("@2", simfg));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) int.TryParse(reader["MAXSIMNO"].ToString(), out simno);
                reader.Close();
            }

            return (++simno).ToString();
        }

        private string GetSimno2(string bdodt, string simfg, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int simno = 0;
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0) AS MAXSIMNO ";
            sql += Environment.NewLine + "  FROM TI2A ";
            sql += Environment.NewLine + " WHERE BDODT LIKE ? ";
            sql += Environment.NewLine + "   AND SIMFG  = ? ";


            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", bdodt.Substring(0, 6) + "%")); // 심사번호를 월별로 구한다.
                cmd.Parameters.Add(new OleDbParameter("@2", simfg));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) int.TryParse(reader["MAXSIMNO"].ToString(), out simno);
                reader.Close();
            }

            return (++simno).ToString();
        }

        private void UNDO_1A(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, string new_simno, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1A", ref fields, ref fields_b, p_conn, p_tran);

            string sql="";
            sql="";
            sql += Environment.NewLine + "INSERT INTO TI1A ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1A_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", new_simno));
                cmd.Parameters.Add(new OleDbParameter("@3", delid));
                cmd.Parameters.Add(new OleDbParameter("@4", exdate));
                cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                cmd.Parameters.Add(new OleDbParameter("@7", pid));
                cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                cmd.Parameters.Add(new OleDbParameter("@9", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", new_simno));
                    cmd.Parameters.Add(new OleDbParameter("@3", delid));
                    cmd.Parameters.Add(new OleDbParameter("@4", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@7", pid));
                    cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@9", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1B(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1B", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1B ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1B_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1E(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1E", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1E ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1E_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1F(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1F", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1F ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1F_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1H(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1H", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1H ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1H_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1J(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1J", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1J ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1J_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_13(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI13", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI13 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI13_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_14(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI14", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI14 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI14_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_12(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI12", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI12 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI12_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_15(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI15", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI15 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI15_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_13T(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI13T", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI13T ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI13T_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1AA(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1AA", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1AA ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1AA_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1EA(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1EA", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1EA ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1EA_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1FA(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1FA", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1FA ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1FA_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1K(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1K", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1K ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1K_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_12A(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI12A", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI12A ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI12A_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_15A(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI15A", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI15A ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI15A_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1AR(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI1AR", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI1AR ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI1AR_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2A(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, string new_simno, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2A", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2A ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2A_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", new_simno));
                cmd.Parameters.Add(new OleDbParameter("@3", delid));
                cmd.Parameters.Add(new OleDbParameter("@4", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                cmd.Parameters.Add(new OleDbParameter("@7", pid));
                cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                cmd.Parameters.Add(new OleDbParameter("@9", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", new_simno));
                    cmd.Parameters.Add(new OleDbParameter("@3", delid));
                    cmd.Parameters.Add(new OleDbParameter("@4", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@7", pid));
                    cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@9", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2B(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2B", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2B ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2B_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2E(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2E", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2E ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2E_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2F(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2F", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2F ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2F_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2H(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2H", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2H ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2H_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2J(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2J", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2J ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2J_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_23(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI23", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI23 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI23_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_24(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI24", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI24 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI24_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_23T(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI23T", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI23T ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI23T_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_22(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI22", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI22 ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI22_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2K(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2K", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2K ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2K_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2AR(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string fields = "";
            string fields_b = "";

            GetTableFields("TI2AR", ref fields, ref fields_b, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI2AR ";
            sql += Environment.NewLine + "      (" + fields + ")";
            sql += Environment.NewLine + "SELECT " + fields_b + "";
            sql += Environment.NewLine + "  FROM TI2AR_B ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                cmd.Parameters.Add(new OleDbParameter("@6", pid));
                cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                cmd.Parameters.Add(new OleDbParameter("@8", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@5", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@6", pid));
                    cmd.Parameters.Add(new OleDbParameter("@7", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@8", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_20(string iofg, string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, string new_unisq, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI20 ";
            sql += Environment.NewLine + "      (IOFG,K1,K2,K3,K4,K5,K6,SIMTEXT)";
            sql += Environment.NewLine + "SELECT IOFG,K1,K2,K3,K4,?, K6,SIMTEXT ";
            sql += Environment.NewLine + "  FROM TI20_B ";
            sql += Environment.NewLine + " WHERE DELID = ? ";
            sql += Environment.NewLine + "   AND IOFG  = ? ";
            sql += Environment.NewLine + "   AND K1 = ? ";
            sql += Environment.NewLine + "   AND K2 = ? ";
            sql += Environment.NewLine + "   AND K3 = ? ";
            sql += Environment.NewLine + "   AND K4 = ? ";
            sql += Environment.NewLine + "   AND K5 = ? ";
            sql += Environment.NewLine + "   AND K6 = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                cmd.Parameters.Add(new OleDbParameter("@2", delid));
                cmd.Parameters.Add(new OleDbParameter("@3", iofg));
                cmd.Parameters.Add(new OleDbParameter("@4", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                cmd.Parameters.Add(new OleDbParameter("@7", pid));
                cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                cmd.Parameters.Add(new OleDbParameter("@9", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", new_unisq));
                    cmd.Parameters.Add(new OleDbParameter("@2", delid));
                    cmd.Parameters.Add(new OleDbParameter("@3", iofg));
                    cmd.Parameters.Add(new OleDbParameter("@4", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@5", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@6", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@7", pid));
                    cmd.Parameters.Add(new OleDbParameter("@8", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@9", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_1REFUSE(string delid, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI1_REFUSE ";
            sql += Environment.NewLine + "   SET DELFG='U' ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND EXDATE = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", delid));
                cmd.Parameters.Add(new OleDbParameter("@2", exdate));
                cmd.Parameters.Add(new OleDbParameter("@3", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@4", jrby));
                cmd.Parameters.Add(new OleDbParameter("@5", pid));
                cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                cmd.Parameters.Add(new OleDbParameter("@6", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", delid));
                    cmd.Parameters.Add(new OleDbParameter("@2", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@3", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@4", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@5", pid));
                    cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@6", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UNDO_2REFUSE(string delid, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI2_REFUSE ";
            sql += Environment.NewLine + "   SET DELFG='U' ";
            sql += Environment.NewLine + " WHERE DELID  = ? ";
            sql += Environment.NewLine + "   AND BDODT  = ? ";
            sql += Environment.NewLine + "   AND QFYCD  = ? ";
            sql += Environment.NewLine + "   AND JRBY   = ? ";
            sql += Environment.NewLine + "   AND PID    = ? ";
            sql += Environment.NewLine + "   AND UNISQ  = ? ";
            sql += Environment.NewLine + "   AND SIMCS  = ? ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", delid));
                cmd.Parameters.Add(new OleDbParameter("@2", bdodt));
                cmd.Parameters.Add(new OleDbParameter("@3", qfycd));
                cmd.Parameters.Add(new OleDbParameter("@4", jrby));
                cmd.Parameters.Add(new OleDbParameter("@5", pid));
                cmd.Parameters.Add(new OleDbParameter("@6", unisq));
                cmd.Parameters.Add(new OleDbParameter("@7", simcs));
                cmd.ExecuteNonQuery();

                if (simcs == "1")
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", delid));
                    cmd.Parameters.Add(new OleDbParameter("@2", bdodt));
                    cmd.Parameters.Add(new OleDbParameter("@3", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@4", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@5", pid));
                    cmd.Parameters.Add(new OleDbParameter("@6", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@7", "0"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void GetTableFields(string tTABLE, ref string fields, ref string fields_b, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            var field_list = new List<string>();
            /*
            string sql = "SELECT TOP 1 * FROM " + tTABLE + "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                int field_count = reader.FieldCount;
                for (int i = 0; i < field_count; i++)
                {
                    field_list.Add(reader.GetName(i).ToUpper());
                }
                return MetroLib.SqlHelper.BREAK;
            });
            */
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT ORDINAL_POSITION, COLUMN_NAME";
            sql += Environment.NewLine + "  FROM INFORMATION_SCHEMA.COLUMNS";
            sql += Environment.NewLine + " WHERE TABLE_NAME = '" + tTABLE + "'";
            sql += Environment.NewLine + " ORDER BY ORDINAL_POSITION";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                field_list.Add(reader["COLUMN_NAME"].ToString().ToUpper());
                return MetroLib.SqlHelper.CONTINUE;
            });

            fields = "";
            foreach (string col_name in field_list)
            {
                string col = col_name;
                if (col == "HDID")
                {
                    // 제외
                }
                else
                {
                    if (fields == "") fields = col;
                    else fields += "," + col;
                }
            }

            fields_b = "";
            foreach (string col_name in field_list)
            {
                string col = col_name;
                if (col == "HDID")
                {
                    // 제외
                }
                else
                {
                    if (col == "UNISQ" || col == "SIMNO") col = "?";

                    if (fields_b == "") fields_b = col;
                    else fields_b += "," + col;
                }
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            List<CData> list = (List<CData>)grdMainView.DataSource;
            foreach (CData data in list)
            {
                data.OP = chkAll.Checked;
            }
            this.RefreshGridMain();
        }
    }
}

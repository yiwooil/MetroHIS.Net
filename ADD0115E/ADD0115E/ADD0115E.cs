using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0115E
{
    public partial class ADD0115E : Form
    {

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private List<CData> m_KeyList = new List<CData>();
        private Dictionary<string, CCnt> m_Cnt = new Dictionary<string, CCnt>();

        public ADD0115E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD0115E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD0115E_Load(object sender, EventArgs e)
        {
            lblMsg.Text = DateTime.Now.AddYears(-5).ToString("yyyyMMdd") + " 이전 자료가 대상입니다.";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string exdate = txtExdate.Text.ToString();
            if (exdate == "")
            {
                MessageBox.Show("청구월을 입력하세요.");
                return;
            }
            if (exdate.Length != 6)
            {
                MessageBox.Show("청구월을 확인하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "자료검색 중입니다.");
                this.Query(exdate);
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query(string p_exdate)
        {
            m_KeyList.Clear();
            m_Cnt.Clear();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                //외래
                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT '1' AS IOFG,'N' AS DELFG,EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,'' DELID";
                sql += System.Environment.NewLine + "  FROM TI1A";
                sql += System.Environment.NewLine + " WHERE EXDATE=?";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_exdate));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        QuerySub(reader);
                    }
                    reader.Close();
                }

                //외래삭제내역
                sql = "";
                sql += System.Environment.NewLine + "SELECT '1' AS IOFG,'Y' AS DELFG,EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,DELID";
                sql += System.Environment.NewLine + "  FROM TI1A_B";
                sql += System.Environment.NewLine + " WHERE EXDATE=?";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_exdate));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        QuerySub(reader);
                    }
                    reader.Close();
                }

                //입원
                sql = "";
                sql += System.Environment.NewLine + "SELECT '2' AS IOFG,'N' AS DELFG,BDODT AS EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,'' DELID";
                sql += System.Environment.NewLine + "  FROM TI2A";
                sql += System.Environment.NewLine + " WHERE BDODT LIKE ?";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_exdate + "%"));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        QuerySub(reader);
                    }
                    reader.Close();
                }

                //입원삭제내역
                sql = "";
                sql += System.Environment.NewLine + "SELECT '2' AS IOFG,'Y' AS DELFG,BDODT AS EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,DELID";
                sql += System.Environment.NewLine + "  FROM TI2A_B";
                sql += System.Environment.NewLine + " WHERE BDODT LIKE ?";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_exdate + "%"));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        QuerySub(reader);
                    }
                    reader.Close();
                }

                // 대상 건수
                txtCnt.Text = m_KeyList.Count.ToString();

                //
                conn.Close();
            }
        }

        private void QuerySub(OleDbDataReader p_reader)
        {
            // 삭제할 키를 담아 놓는다.
            CData data = new CData();
            data.IOFG = p_reader["IOFG"].ToString();
            data.DELFG = p_reader["DELFG"].ToString();
            data.EXDATE = p_reader["EXDATE"].ToString();
            data.QFYCD = p_reader["QFYCD"].ToString();
            data.JRBY = p_reader["JRBY"].ToString();
            data.PID = p_reader["PID"].ToString();
            data.UNISQ = p_reader["UNISQ"].ToString();
            data.SIMCS = p_reader["SIMCS"].ToString();
            data.DELID = p_reader["DELID"].ToString();

            m_KeyList.Add(data);

            // 입원외래, 자격별 건수를 세어놓는다.
            string key = data.IOFG + "," + data.QFYCD;
            if (m_Cnt.ContainsKey(key) == true)
            {
                m_Cnt[key].CNT += (data.DELFG == "Y" ? 0 : 1);
                m_Cnt[key].CNT_B += (data.DELFG == "Y" ? 1 : 0);
            }
            else
            {
                CCnt cntData = new CCnt();
                cntData.IOFG = data.IOFG;
                cntData.QFYCD = data.QFYCD;
                cntData.CNT = (data.DELFG=="Y" ? 0 : 1);
                cntData.CNT_B = (data.DELFG == "Y" ? 1 : 0);
                m_Cnt.Add(key, cntData);
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

        private void btnExp_Click(object sender, EventArgs e)
        {
            if (m_KeyList.Count < 1)
            {
                MessageBox.Show("파기할 자료가 없습니다.");
                return;
            }
            string exdate = txtExdate.Text.ToString();
            string exprsn = txtExprsn.Text.ToString();
            if (exdate == "")
            {
                MessageBox.Show("청구월을 입력하세요.");
                return;
            }
            if (exdate.Length != 6)
            {
                MessageBox.Show("청구월을 확인하세요.");
                return;
            }
            if (exprsn == "")
            {
                MessageBox.Show("파기사유를 입력하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "파기처리 중입니다.");
                this.Exp(exdate, exprsn);
                this.CloseProgressForm("", "파기처리 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("파기처리가 성공적으로 완료되었습니다.");
                txtExdate.Text = "";
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "파기처리 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Exp(string p_exdate, string p_rsn)
        {
            string tblList1 = "TI1A,TI1B,TI1E,TI1F,TI1H,TI1J,TI13,TI14,TI13T,TI1AA,TI1EA,TI1FA,TI1K,TI1AR";
            string tblList2 = "TI2A,TI2B,TI2E,TI2F,TI2H,TI2J,TI23,TI24,TI23T,TI2K,TI2AR";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    string sql = "";

                    conn.Open();

                    string sysdate = MetroLib.Util.GetSysDate(conn);
                    string systime = MetroLib.Util.GetSysTime(conn);

                    tran = conn.BeginTransaction();

                    foreach (CData data in m_KeyList)
                    {
                        string[] tbls;
                        string fEXDATE = "";
                        string tTI1_REFUSE = "";
                        if (data.IOFG == "1")
                        {
                            tbls = tblList1.Split(',');
                            fEXDATE = "EXDATE";
                            tTI1_REFUSE = "TI1_REFUSE";
                        }
                        else
                        {
                            tbls = tblList2.Split(',');
                            fEXDATE = "BDODT";
                            tTI1_REFUSE = "TI2_REFUSE";
                        }

                        foreach (string tbl in tbls)
                        {
                            sql = "";
                            sql += System.Environment.NewLine + "DELETE FROM " + tbl + "" + (data.DELFG == "Y" ? "_B" : "") + "";
                            sql += System.Environment.NewLine + " WHERE " + fEXDATE + "=?";
                            sql += System.Environment.NewLine + "   AND QFYCD=?";
                            sql += System.Environment.NewLine + "   AND JRBY=?";
                            sql += System.Environment.NewLine + "   AND PID=?";
                            sql += System.Environment.NewLine + "   AND UNISQ=?";
                            sql += System.Environment.NewLine + "   AND SIMCS=?";
                            if (data.DELFG == "Y")
                            {
                                sql += System.Environment.NewLine + "   AND DELID=?";
                            }
                            // TSQL문장과 Connection 객체를 지정   
                            using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                            {
                                cmd.Parameters.Add(new OleDbParameter("@1", data.EXDATE));
                                cmd.Parameters.Add(new OleDbParameter("@2", data.QFYCD));
                                cmd.Parameters.Add(new OleDbParameter("@3", data.JRBY));
                                cmd.Parameters.Add(new OleDbParameter("@4", data.PID));
                                cmd.Parameters.Add(new OleDbParameter("@5", data.UNISQ));
                                cmd.Parameters.Add(new OleDbParameter("@6", data.SIMCS));
                                if (data.DELFG == "Y")
                                {
                                    cmd.Parameters.Add(new OleDbParameter("@7", data.DELID));
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (data.IOFG == "Y")
                        {
                            sql = "";
                            sql += System.Environment.NewLine + "DELETE FROM " + tTI1_REFUSE + "";
                            sql += System.Environment.NewLine + " WHERE " + fEXDATE + "=?";
                            sql += System.Environment.NewLine + "   AND QFYCD=?";
                            sql += System.Environment.NewLine + "   AND JRBY=?";
                            sql += System.Environment.NewLine + "   AND PID=?";
                            sql += System.Environment.NewLine + "   AND UNISQ=?";
                            sql += System.Environment.NewLine + "   AND SIMCS=?";
                            sql += System.Environment.NewLine + "   AND DELID=?";
                            // TSQL문장과 Connection 객체를 지정   
                            using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                            {
                                cmd.Parameters.Add(new OleDbParameter("@1", data.EXDATE));
                                cmd.Parameters.Add(new OleDbParameter("@2", data.QFYCD));
                                cmd.Parameters.Add(new OleDbParameter("@3", data.JRBY));
                                cmd.Parameters.Add(new OleDbParameter("@4", data.PID));
                                cmd.Parameters.Add(new OleDbParameter("@5", data.UNISQ));
                                cmd.Parameters.Add(new OleDbParameter("@6", data.SIMCS));
                                cmd.Parameters.Add(new OleDbParameter("@7", data.DELID));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        sql = "";
                        sql += System.Environment.NewLine + "DELETE FROM TI20" + (data.DELFG == "Y" ? "_B" : "") + "";
                        sql += System.Environment.NewLine + " WHERE IOFG=?";
                        sql += System.Environment.NewLine + "   AND K1=?";
                        sql += System.Environment.NewLine + "   AND K2=?";
                        sql += System.Environment.NewLine + "   AND K3=?";
                        sql += System.Environment.NewLine + "   AND K4=?";
                        sql += System.Environment.NewLine + "   AND K5=?";
                        sql += System.Environment.NewLine + "   AND K6=?";
                        if (data.DELFG == "Y")
                        {
                            sql += System.Environment.NewLine + "   AND DELID=?";
                        }
                        // TSQL문장과 Connection 객체를 지정   
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", data.IOFG));
                            cmd.Parameters.Add(new OleDbParameter("@2", data.EXDATE));
                            cmd.Parameters.Add(new OleDbParameter("@3", data.QFYCD));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.JRBY));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.PID));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.UNISQ));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.SIMCS));
                            if (data.DELFG == "Y")
                            {
                                cmd.Parameters.Add(new OleDbParameter("@8", data.DELID));
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // 파기 로그를 남지는 작업
                    string expseq = "1";
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT MAX(EXPSEQ) AS MAXSEQ";
                    sql += System.Environment.NewLine + "  FROM TI_EXP_LOG";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if(reader.Read()){
                            expseq = reader["MAXSEQ"].ToString();
                            int tmp = 0;
                            int.TryParse(expseq, out tmp);
                            expseq = (tmp + 1).ToString();
                        }else{
                            expseq="1";
                        }
                    }
                    //
                    sql = "";
                    sql += System.Environment.NewLine + "INSERT INTO TI_EXP_LOG(EXPSEQ,EXDATE,EXPDT,EXPTM,EMPID,EXPRSN)";
                    sql += System.Environment.NewLine + "VALUES(?,?,?,?,?,?)";
                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", expseq));
                        cmd.Parameters.Add(new OleDbParameter("@2", p_exdate));
                        cmd.Parameters.Add(new OleDbParameter("@3", sysdate));
                        cmd.Parameters.Add(new OleDbParameter("@4", systime));
                        cmd.Parameters.Add(new OleDbParameter("@5", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@6", p_rsn));

                        cmd.ExecuteNonQuery();
                    }
                    // 입원외래, 자격별 건수 로그
                    foreach (KeyValuePair<string, CCnt> dic in m_Cnt)
                    {
                        sql = "";
                        sql += System.Environment.NewLine + "INSERT INTO TI_EXP_LOG_A(EXPSEQ,IOFG,QFYCD,CNT,CNT_B)";
                        sql += System.Environment.NewLine + "VALUES(?,?,?,?,?)";
                        // TSQL문장과 Connection 객체를 지정   
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", expseq));
                            cmd.Parameters.Add(new OleDbParameter("@2", dic.Value.IOFG));
                            cmd.Parameters.Add(new OleDbParameter("@3", dic.Value.QFYCD));
                            cmd.Parameters.Add(new OleDbParameter("@4", dic.Value.CNT));
                            cmd.Parameters.Add(new OleDbParameter("@5", dic.Value.CNT_B));

                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        private void txtCnt_DoubleClick(object sender, EventArgs e)
        {
            string msg = "";
            foreach (KeyValuePair<string, CCnt> dic in m_Cnt)
            {
                msg += dic.Value.IOFG + ",자격=" + dic.Value.QFYCD + ",정산건=" + dic.Value.CNT.ToString() + ",삭제건=" + dic.Value.CNT_B.ToString() + Environment.NewLine;
            }
            MessageBox.Show(msg);

        }

        private void txtExdate_TextChanged(object sender, EventArgs e)
        {
            txtCnt.Text = "";
            txtExprsn.Text = "";
            m_KeyList.Clear();
            m_Cnt.Clear();
        }
    }
}

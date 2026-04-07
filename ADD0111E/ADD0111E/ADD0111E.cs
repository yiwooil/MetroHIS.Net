using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0111E
{
    public partial class ADD0111E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;

        private Boolean isFirst;

        public ADD0111E()
        {
            InitializeComponent();
        }

        public ADD0111E(String user, String pwd, String prjcd)
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

        private void txtYmm_TextChanged(object sender, EventArgs e)
        {
            if (txtYmm.Text.ToString().Length == 6)
            {
                string fdate = txtYmm.Text.ToString() + "01";
                txtFdate.Text = fdate;
                DateTime dt;
                DateTime.TryParse(fdate.Substring(0, 4) + "-" + fdate.Substring(4, 2) + "-" + fdate.Substring(6, 2), out dt);
                dt = dt.AddMonths(1);
                dt = dt.AddDays(-1);
                txtTdate.Text = dt.ToString("yyyyMMdd");
            }
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Make();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("작업이 성공적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Make()
        {
            string gbn = rbYang.Checked == true ? "1" : "2"; // 양.한방
            string iofg = rbOutPtnt.Checked == true ? "1" : "2"; // 입원.외래구분
            string simfg = "1"; // 기본 보험
            if (gbn == "1")
            {
                if (rbQfy2.Checked) simfg = "1";
                else if (rbQfy2Dent.Checked) simfg = "2";
                else if (rbQfy3.Checked) simfg = "3";
                else if (rbQfy3Dent.Checked) simfg = "4";
                else if (rbQfy5.Checked) simfg = "5";
                else if (rbQfy6.Checked) simfg = "6";
            }
            else
            {
                if (rbQfy2.Checked) simfg = "7";
                else if (rbQfy2Dent.Checked) simfg = "2";
                else if (rbQfy3.Checked) simfg = "8";
                else if (rbQfy3Dent.Checked) simfg = "4";
                else if (rbQfy5.Checked) simfg = "9";
                else if (rbQfy6.Checked) simfg = "6";
            }
            if (iofg == "1")
            {
                Make1(gbn, simfg, m_HospMulti);
            }
            else
            {
                Make2(gbn, simfg, m_HospMulti);
            }
        }

        private void Make1(string gbn, string simfg, string hospmulti)
        {
            int simno = 0;
            string exdate = txtYmm.Text.ToString();

            List<CData> list = new List<CData>();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();

                    string hospjong = GetHospjong(txtFdate.Text.ToString(), conn);

                    string sql = "";
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT A.EXDATE,A.QFYCD,A.JRBY,A.PID,A.UNISQ,A.SIMCS,A.SIMNO,A.JRKWA";
                    sql = sql + Environment.NewLine + "    FROM TI1A A INNER JOIN TA09 A09 ON A09.DPTCD = DBO.MFN_PIECE(A.JRKWA,'$',3)";
                    sql = sql + Environment.NewLine + "   WHERE A.EXDATE = ? ";
                    sql = sql + Environment.NewLine + "     AND A.SIMFG  = ? ";
                    sql = sql + Environment.NewLine + "     AND A.SIMCS <> 0 ";
                    sql = sql + Environment.NewLine + "     AND ISNULL(A.DELFG,'')='' ";
                    sql = sql + Environment.NewLine + "     AND ISNULL(A09.ADDDPTCD,'')=? ";
                    if (String.Compare(hospjong, "2") <= 0 && gbn == "1" && (simfg == "1" || simfg == "3"))
                    {
                        // 종합병원이상 & 양방 & 보험,보호의과 이면 내과세부진료과목순...
                        sql = sql + Environment.NewLine + " ORDER BY CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                        sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                        sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                        sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                        sql = sql + Environment.NewLine + "          END";
                        sql = sql + Environment.NewLine + "        , A09.PRIMDPTCD";
                        sql = sql + Environment.NewLine + "        , A09.INSDPTCD";
                        sql = sql + Environment.NewLine + "        , CASE WHEN A09.INSDPTCD<>'01' ";
                        sql = sql + Environment.NewLine + "               THEN '' ";
                        sql = sql + Environment.NewLine + "               ELSE CASE WHEN ISNULL(A09.INSDPTCD2,'')='' THEN '00' ELSE A09.INSDPTCD2 END";
                        sql = sql + Environment.NewLine + "          END ";
                        sql = sql + Environment.NewLine + "        , A09.DPTCD";
                        sql = sql + Environment.NewLine + "        , A.PID ";
                        sql = sql + Environment.NewLine + "        , A.STEDT ";
                        sql = sql + Environment.NewLine + "        , A.UNISQ ";
                    }
                    else
                    {
                        sql = sql + Environment.NewLine + " ORDER BY CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                        sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                        sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                        sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                        sql = sql + Environment.NewLine + "          END";
                        sql = sql + Environment.NewLine + "        , A.JRKWA";
                        sql = sql + Environment.NewLine + "        , A.PID ";
                        sql = sql + Environment.NewLine + "        , A.STEDT ";
                        sql = sql + Environment.NewLine + "        , A.UNISQ ";
                    }

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                        cmd.Parameters.Add(new OleDbParameter("@2", simfg));
                        cmd.Parameters.Add(new OleDbParameter("@3", hospmulti));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string qfycd = reader["QFYCD"].ToString();
                            if (qfycd == "21" || qfycd == "22" || qfycd == "23" || qfycd == "40")
                            {
                                if (simfg != "1" && simfg != "2" && simfg != "7") continue;
                            }
                            if (qfycd == "31" || qfycd == "32" || qfycd == "38" || qfycd == "39")
                            {
                                if (simfg != "3" && simfg != "4" && simfg != "8") continue;
                            }
                            string jrby = reader["JRKWA"].ToString().Substring(0, 1);
                            if (String.Compare(simfg,"7")<0 && String.Compare(jrby,"7")>0) continue; // 양방인 경우 한방진료과이면 END
                            if (simfg == "1" && jrby == "6") continue; //양방보험
                            if (simfg == "2" && jrby != "6") continue; //양방보험치과
                            if (simfg == "3" && jrby == "6") continue; //양방보호
                            if (simfg == "4" && jrby != "6") continue; //양방보호치과
                            if (simfg == "7" && jrby != "7") continue; //한방보험
                            if (simfg == "8" && jrby != "7") continue; //한방보호
                            if (simfg == "9" && jrby != "7") continue; //한방산재

                            // OleDbDataReader는 reader.Read()중 업데이트 문을 수행하면 오류가 발생한다.
                            //       수동 트렌잭션 또는 분산 트렌잭션 모드에 있기 대문에 연결을 새로 만들 수 없습니다.
                            // 그래서 update 문을 밖으로 빼야한다.

                            simno++;

                            CData data = new CData();
                            data.EXDATE = reader["EXDATE"].ToString();
                            data.QFYCD = reader["QFYCD"].ToString();
                            data.JRBY = reader["JRBY"].ToString();
                            data.PID = reader["PID"].ToString();
                            data.UNISQ = reader["UNISQ"].ToString();
                            data.SIMCS = reader["SIMCS"].ToString();
                            data.SIMFG = simfg;
                            data.SIMNO = simno.ToString();

                            list.Add(data);
                        }
                        reader.Close();
                    }
                    
                    tran = conn.BeginTransaction();
                    foreach (CData data in list)
                    {
                        string uSql = "";
                        uSql = "";
                        uSql = uSql + Environment.NewLine + "UPDATE TI1A ";
                        uSql = uSql + Environment.NewLine + "   SET SIMNO  = ? ";
                        uSql = uSql + Environment.NewLine + " WHERE EXDATE = ? ";
                        uSql = uSql + Environment.NewLine + "   AND QFYCD  = ? ";
                        uSql = uSql + Environment.NewLine + "   AND JRBY   = ? ";
                        uSql = uSql + Environment.NewLine + "   AND PID    = ? ";
                        uSql = uSql + Environment.NewLine + "   AND UNISQ  = ? ";
                        uSql = uSql + Environment.NewLine + "   AND SIMCS  = ?";
                        uSql = uSql + Environment.NewLine + "   AND SIMFG  = ? ";

                        using (OleDbCommand uCmd = new OleDbCommand(uSql, conn, tran))
                        {
                            uCmd.Parameters.Add(new OleDbParameter("@1", data.SIMNO));
                            uCmd.Parameters.Add(new OleDbParameter("@2", data.EXDATE));
                            uCmd.Parameters.Add(new OleDbParameter("@3", data.QFYCD));
                            uCmd.Parameters.Add(new OleDbParameter("@4", data.JRBY));
                            uCmd.Parameters.Add(new OleDbParameter("@5", data.PID));
                            uCmd.Parameters.Add(new OleDbParameter("@6", data.UNISQ));
                            uCmd.Parameters.Add(new OleDbParameter("@7", data.SIMCS));
                            uCmd.Parameters.Add(new OleDbParameter("@8", data.SIMFG));
                            uCmd.ExecuteNonQuery();
                        }
                        using (OleDbCommand uCmd = new OleDbCommand(uSql, conn, tran))
                        {
                            uCmd.Parameters.Add(new OleDbParameter("@1", data.SIMNO));
                            uCmd.Parameters.Add(new OleDbParameter("@2", data.EXDATE));
                            uCmd.Parameters.Add(new OleDbParameter("@3", data.QFYCD));
                            uCmd.Parameters.Add(new OleDbParameter("@4", data.JRBY));
                            uCmd.Parameters.Add(new OleDbParameter("@5", data.PID));
                            uCmd.Parameters.Add(new OleDbParameter("@6", data.UNISQ));
                            uCmd.Parameters.Add(new OleDbParameter("@7", "0"));
                            uCmd.Parameters.Add(new OleDbParameter("@8", data.SIMFG));
                            uCmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
        }

        private void Make2(string gbn, string simfg, string hospmulti)
        {
            int simno = 0;
            string fdate = txtFdate.Text.ToString();
            string tdate = txtTdate.Text.ToString();

            List<CData> list = new List<CData>();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();

                    string hospjong = GetHospjong(txtFdate.Text.ToString(), conn);
                    string simnomonthfg = GetSimnoMonthfg(conn);

                    string sql="";
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT A.BDODT,A.QFYCD,A.JRBY,A.PID,A.UNISQ,A.SIMCS,A.SIMNO,A.JRKWA";
                    sql = sql + Environment.NewLine + "    FROM TI2A A INNER JOIN TA09 A09 ON A09.DPTCD = DBO.MFN_PIECE(A.JRKWA,'$',3)";
                    sql = sql + Environment.NewLine + "   WHERE A.BDODT >= ? ";
                    sql = sql + Environment.NewLine + "     AND A.BDODT <= ? ";
                    sql = sql + Environment.NewLine + "     AND A.SIMFG  = ? ";
                    sql = sql + Environment.NewLine + "     AND A.SIMCS <> 0 ";
                    sql = sql + Environment.NewLine + "     AND ISNULL(A.DELFG,'')='' ";
                    sql = sql + Environment.NewLine + "     AND ISNULL(A09.ADDDPTCD,'')=? ";
                    if (String.Compare(hospjong, "2") <= 0 && gbn == "1" && (simfg == "1" || simfg == "3"))
                    {
                        // 종합병원이상 & 양방 & 보험,보호의과 이면 내과세부진료과목순...
                        if (simnomonthfg == "1")
                        {
                            // 월단위생성
                            sql = sql + Environment.NewLine + " ORDER BY CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                            sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                            sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                            sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                            sql = sql + Environment.NewLine + "          END";
                            sql = sql + Environment.NewLine + "        , A09.PRIMDPTCD";
                            sql = sql + Environment.NewLine + "        , A09.INSDPTCD";
                            sql = sql + Environment.NewLine + "        , CASE WHEN A09.INSDPTCD<>'01' ";
                            sql = sql + Environment.NewLine + "               THEN '' ";
                            sql = sql + Environment.NewLine + "               ELSE CASE WHEN ISNULL(A09.INSDPTCD2,'')='' THEN '00' ELSE A09.INSDPTCD2 END";
                            sql = sql + Environment.NewLine + "          END ";
                            sql = sql + Environment.NewLine + "        , A09.DPTCD";
                            sql = sql + Environment.NewLine + "        , A.BDODT";
                            sql = sql + Environment.NewLine + "        , A.PID ";
                            sql = sql + Environment.NewLine + "        , A.STEDT ";
                            sql = sql + Environment.NewLine + "        , A.UNISQ ";
                        }
                        else
                        {
                            // 일단위생성
                            sql = sql + Environment.NewLine + " ORDER BY A.BDODT";
                            sql = sql + Environment.NewLine + "        , CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                            sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                            sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                            sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                            sql = sql + Environment.NewLine + "          END";
                            sql = sql + Environment.NewLine + "        , A09.PRIMDPTCD";
                            sql = sql + Environment.NewLine + "        , A09.INSDPTCD";
                            sql = sql + Environment.NewLine + "        , CASE WHEN A09.INSDPTCD<>'01' ";
                            sql = sql + Environment.NewLine + "               THEN '' ";
                            sql = sql + Environment.NewLine + "               ELSE CASE WHEN ISNULL(A09.INSDPTCD2,'')='' THEN '00' ELSE A09.INSDPTCD2 END";
                            sql = sql + Environment.NewLine + "          END ";
                            sql = sql + Environment.NewLine + "        , A09.DPTCD";
                            sql = sql + Environment.NewLine + "        , A.PID ";
                            sql = sql + Environment.NewLine + "        , A.STEDT ";
                            sql = sql + Environment.NewLine + "        , A.UNISQ ";
                        }
                    }
                    else
                    {
                        if (simnomonthfg == "1")
                        {
                            // 월단위생성
                            sql = sql + Environment.NewLine + " ORDER BY CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                            sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                            sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                            sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                            sql = sql + Environment.NewLine + "          END";
                            sql = sql + Environment.NewLine + "        , A.JRKWA";
                            sql = sql + Environment.NewLine + "        , A.BDODT";
                            sql = sql + Environment.NewLine + "        , A.PID ";
                            sql = sql + Environment.NewLine + "        , A.STEDT ";
                            sql = sql + Environment.NewLine + "        , A.UNISQ ";
                        }
                        else
                        {
                            // 일단위생성
                            sql = sql + Environment.NewLine + " ORDER BY A.BDODT";
                            sql = sql + Environment.NewLine + "        , CASE WHEN A.DONFG='X' THEN 2 ELSE 1 END";
                            sql = sql + Environment.NewLine + "        , CASE WHEN ISNULL(A.ADDZ1,'')='2' THEN 3";//추가청구
                            sql = sql + Environment.NewLine + "               WHEN ISNULL(A.ADDZ1,'')='1' THEN 2";//보완청구
                            sql = sql + Environment.NewLine + "               ELSE 1"; // 원청구+분리청구
                            sql = sql + Environment.NewLine + "          END";
                            sql = sql + Environment.NewLine + "        , A.JRKWA";
                            sql = sql + Environment.NewLine + "        , A.PID ";
                            sql = sql + Environment.NewLine + "        , A.STEDT ";
                            sql = sql + Environment.NewLine + "        , A.UNISQ ";
                        }
                    }

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", fdate));
                        cmd.Parameters.Add(new OleDbParameter("@2", tdate));
                        cmd.Parameters.Add(new OleDbParameter("@3", simfg));
                        cmd.Parameters.Add(new OleDbParameter("@4", hospmulti));

                        string bf_bdodt = "";

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string qfycd = reader["QFYCD"].ToString();
                            if (qfycd == "21" || qfycd == "22" || qfycd == "23" || qfycd == "40")
                            {
                                if (simfg != "1" && simfg != "2" && simfg != "7") continue;
                            }
                            if (qfycd == "31" || qfycd == "32" || qfycd == "38" || qfycd == "39")
                            {
                                if (simfg != "3" && simfg != "4" && simfg != "8") continue;
                            }
                            string jrby = reader["JRKWA"].ToString().Substring(0, 1);
                            if (String.Compare(simfg, "7") < 0 && String.Compare(jrby, "7") > 0) continue; // 양방인 경우 한방진료과이면 END
                            if (simfg == "1" && jrby == "6") continue; //양방보험
                            if (simfg == "2" && jrby != "6") continue; //양방보험치과
                            if (simfg == "3" && jrby == "6") continue; //양방보호
                            if (simfg == "4" && jrby != "6") continue; //양방보호치과
                            if (simfg == "7" && jrby != "7") continue; //한방보험
                            if (simfg == "8" && jrby != "7") continue; //한방보호
                            if (simfg == "9" && jrby != "7") continue; //한방산재

                            if (simnomonthfg == "1")
                            {
                                // 월별 생성.
                            }
                            else
                            {
                                // 일자별 생성. 일자별 생성이므로 일자가 변경되면 심사번호를 초기화한다.
                                string bdodt = reader["BDODT"].ToString();
                                if (bf_bdodt == "")
                                {
                                    simno = 0;
                                    bf_bdodt = bdodt;
                                }
                                else if (bf_bdodt != bdodt)
                                {
                                    simno = 0;
                                    bf_bdodt = bdodt;
                                }
                            }
                            simno++;

                            CData data = new CData();
                            data.EXDATE = reader["BDODT"].ToString();
                            data.QFYCD = reader["QFYCD"].ToString();
                            data.JRBY = reader["JRBY"].ToString();
                            data.PID = reader["PID"].ToString();
                            data.UNISQ = reader["UNISQ"].ToString();
                            data.SIMCS = reader["SIMCS"].ToString();
                            data.SIMFG = simfg;
                            data.SIMNO = simno.ToString();

                            list.Add(data);
                        }
                    }
                    
                    tran = conn.BeginTransaction();
                    foreach (CData data in list)
                    {
                        string uSql = "";
                        uSql = "";
                        uSql = uSql + Environment.NewLine + "UPDATE TI2A ";
                        uSql = uSql + Environment.NewLine + "   SET SIMNO  = ? ";
                        uSql = uSql + Environment.NewLine + " WHERE BDODT  = ? ";
                        uSql = uSql + Environment.NewLine + "   AND QFYCD  = ? ";
                        uSql = uSql + Environment.NewLine + "   AND JRBY   = ? ";
                        uSql = uSql + Environment.NewLine + "   AND PID    = ? ";
                        uSql = uSql + Environment.NewLine + "   AND UNISQ  = ? ";
                        uSql = uSql + Environment.NewLine + "   AND SIMCS  = ?";
                        uSql = uSql + Environment.NewLine + "   AND SIMFG  = ? ";

                        using (OleDbCommand uCmd = new OleDbCommand(uSql, conn, tran))
                        {
                            uCmd.Parameters.Add(new OleDbParameter("@1", data.SIMNO));
                            uCmd.Parameters.Add(new OleDbParameter("@2", data.EXDATE));
                            uCmd.Parameters.Add(new OleDbParameter("@3", data.QFYCD));
                            uCmd.Parameters.Add(new OleDbParameter("@4", data.JRBY));
                            uCmd.Parameters.Add(new OleDbParameter("@5", data.PID));
                            uCmd.Parameters.Add(new OleDbParameter("@6", data.UNISQ));
                            uCmd.Parameters.Add(new OleDbParameter("@7", data.SIMCS));
                            uCmd.Parameters.Add(new OleDbParameter("@8", data.SIMFG));
                            uCmd.ExecuteNonQuery();
                        }
                        using (OleDbCommand uCmd = new OleDbCommand(uSql, conn, tran))
                        {
                            uCmd.Parameters.Add(new OleDbParameter("@1", data.SIMNO));
                            uCmd.Parameters.Add(new OleDbParameter("@2", data.EXDATE));
                            uCmd.Parameters.Add(new OleDbParameter("@3", data.QFYCD));
                            uCmd.Parameters.Add(new OleDbParameter("@4", data.JRBY));
                            uCmd.Parameters.Add(new OleDbParameter("@5", data.PID));
                            uCmd.Parameters.Add(new OleDbParameter("@6", data.UNISQ));
                            uCmd.Parameters.Add(new OleDbParameter("@7", "0"));
                            uCmd.Parameters.Add(new OleDbParameter("@8", data.SIMFG));
                            uCmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
        }

        private string GetHospjong(string p_exdt, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT dbo.MFS_ADD_GET_HOSPITALJONG('" + p_exdt + "') AS JONG";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = reader["JONG"].ToString();
                }
            }
            return ret;
        }

        private string GetSimnoMonthfg(OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='133'";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = reader["FLD2QTY"].ToString();
                }
            }
            return ret;
        }

        private void ADD0111E_Load(object sender, EventArgs e)
        {
            isFirst = true;
        }

        private void ADD0111E_Activated(object sender, EventArgs e)
        {
            if (isFirst == false) return;
            isFirst = false;
            txtYmm.Focus();
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
    }
}

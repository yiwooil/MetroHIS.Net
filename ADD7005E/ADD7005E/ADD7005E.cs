using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7005E
{
    public partial class ADD7005E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;
        private String m_Demno;

        private bool IsFirst;
        private string m_pgm_step = ""; // 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD7005E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";
            m_Demno = "";

            this.CreatePopupMenu();
        }

        public ADD7005E(String user, String pwd, String prjcd, String demno)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Demno = demno;
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
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["MULTIFG"].ToString();
                        return false;
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private string GetHospId()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='2'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["FLD1QTY"].ToString();
                        return false;
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void CreatePopupMenu()
        {
            //
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("전송 제외", new EventHandler(mnuRemoveData_Click));
            cm.MenuItems.Add("전송 제외 취소", new EventHandler(mnuCancelRemoveData_Click));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("검사 결과 다시 읽기", new EventHandler(mnuReQueryData_Click));
            grdMain.ContextMenu = cm;
        }

        private void ADD7005E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD7005E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtDemno.Text = m_Demno;
            txtHosid.Text = GetHospId();

            btnQuery.PerformClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

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
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            grdSub.DataSource = null;

            string demno = txtDemno.Text.ToString();

            int no = 0;

            string iofg = "";
            string cnectdd = "";
            string dcount = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                iofg = GetIofg(demno, conn);
                txtIofg.Text = iofg;
                txtCnecno.Text = GetCnecno(demno, conn, ref cnectdd, ref dcount); // 접수번호를 가져온다.
                txtCnectdd.Text = cnectdd; // 접수일자
                toolTip1.SetToolTip(txtCnectdd, "");

                if (txtCnecno.Text.ToString() == "")
                {
                    txtCnecno.Text = "0000000";
                    txtBillSno.Text = "0"; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
                    //txtCnectdd.Text = txtDemno.Text.ToString(); // 접수년도만 사용할 것이므로 청구번호로 만든다.
                }
                else
                {
                    if (dcount == "")
                    {
                        txtBillSno.Text = "1";
                    }
                    else
                    {
                        txtBillSno.Text = dcount;
                    }
                }
                // 2024.11.05 WOOIL - 접수번호를 심결에서 읽으면 접수일자가 없다.
                //                    접수일자 칸에 청구번호를 넣는다.
                if (txtCnectdd.Text.ToString() == "")
                {
                    txtCnectdd.Text = txtDemno.Text.ToString(); // 접수년도만 사용할 것이므로 청구번호로 만든다.
                    toolTip1.SetToolTip(txtCnectdd, "접수일자를 찾지 못해 청구번호를 사용합니다.");
                }

                string tTI1A = "TI1A";
                string tTI1F = "TI1F";
                string tTI1H = "TI1H";
                string fEXDATE = "EXDATE";
                if (iofg == "2")
                {
                    tTI1A = "TI2A";
                    tTI1F = "TI2F";
                    tTI1H = "TI2H";
                    fEXDATE = "BDODT";
                }

                // 2022.06,02 WOOIL - 신경학적 검사를 오늘 이전에 작성해놓고 사용해도 된다.
                //                    따라서 가장 최근에 입력한 결과지를 사용한다.
                //                    척추MRI검사(RHI109, HI110, HI111, HI112, HI113)가 있는 명세서만 작성한다.
                string sql = "";
                sql += Environment.NewLine + "SELECT A.DEMNO, A.EPRTNO, A.PID, A.PNM, DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD, A.STEDT, A.BDEDT, A.PSEX, A.RESID, A.EXAMC, A.QFYCD, A." + fEXDATE + " AS BEDODT";
                sql += Environment.NewLine + "     , A." + fEXDATE + "+','+A.QFYCD+','+A.JRBY+','+A.PID+','+CONVERT(VARCHAR,A.UNISQ)+','+CONVERT(VARCHAR,A.SIMCS) AS A_KEY";
                sql += Environment.NewLine + "  FROM " + tTI1A + " A";
                sql += Environment.NewLine + " WHERE A.DEMNO='" + demno + "'";
                sql += Environment.NewLine + "   AND (";
                sql += Environment.NewLine + "       EXISTS (SELECT *";
                sql += Environment.NewLine + "                 FROM " + tTI1F + " X";
                sql += Environment.NewLine + "                WHERE X." + fEXDATE + "=A." + fEXDATE + "";
                sql += Environment.NewLine + "                  AND X.QFYCD=A.QFYCD";
                sql += Environment.NewLine + "                  AND X.JRBY=A.JRBY";
                sql += Environment.NewLine + "                  AND X.PID=A.PID";
                sql += Environment.NewLine + "                  AND X.UNISQ=A.UNISQ";
                sql += Environment.NewLine + "                  AND X.SIMCS=A.SIMCS";
                sql += Environment.NewLine + "                  AND (X.BGIHO LIKE 'HI109%' OR"; // 척추 MRI코드가 있는 명세서만 조회함.
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI110%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI111%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI112%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI113%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI209%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI210%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI211%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI212%' OR"; // 2023.08.29 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI213%' OR"; // 2023.08.29 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI409%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI410%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI411%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI412%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI413%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HJ611%'";    // 2025.03.14 WOOIL - 추가
                sql += Environment.NewLine + "                      )";
                sql += Environment.NewLine + "              )";
                sql += Environment.NewLine + "       OR";
                sql += Environment.NewLine + "       EXISTS (SELECT *";
                sql += Environment.NewLine + "                 FROM " + tTI1H + " X";
                sql += Environment.NewLine + "                WHERE X." + fEXDATE + "=A." + fEXDATE + "";
                sql += Environment.NewLine + "                  AND X.QFYCD=A.QFYCD";
                sql += Environment.NewLine + "                  AND X.JRBY=A.JRBY";
                sql += Environment.NewLine + "                  AND X.PID=A.PID";
                sql += Environment.NewLine + "                  AND X.UNISQ=A.UNISQ";
                sql += Environment.NewLine + "                  AND X.SIMCS=A.SIMCS";
                sql += Environment.NewLine + "                  AND ISNULL(X.AFPFG,'')='1'"; // 2023.01.26 WOOIL 보훈 청구하는 경우 추가 ******
                sql += Environment.NewLine + "                  AND (X.BGIHO LIKE 'HI109%' OR"; // 척추 MRI코드가 있는 명세서만 조회함.
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI110%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI111%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI112%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI113%' OR";
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI209%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI210%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI211%' OR"; // 2022.07.06 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI212%' OR"; // 2023.08.29 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI213%' OR"; // 2025.02.10 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI409%' OR"; // 2025.02.10 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI410%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI411%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI412%' OR"; // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HI413%' OR";    // 2022.07.25 WOOIL - 추가
                sql += Environment.NewLine + "                       X.BGIHO LIKE 'HJ611%'";    // 2025.03.14 WOOIL - 추가
                sql += Environment.NewLine + "                      )";
                sql += Environment.NewLine + "              )";
                sql += Environment.NewLine + "       )";
                sql += Environment.NewLine + " ORDER BY A.DEMNO,A.EPRTNO";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    string stedt = row["STEDT"].ToString();
                    string enddt = row["BEDODT"].ToString();
                    int examc = 0;
                    int.TryParse(row["EXAMC"].ToString(), out examc);
                    if (examc > 0)
                    {
                        DateTime dtStedt = DateTime.ParseExact(stedt, "yyyyMMdd", null);
                        DateTime dtEnddt = dtStedt.AddDays(examc > 0 ? examc - 1 : 0);
                        enddt = dtEnddt.ToString("yyyyMMdd");
                    }
                    else
                    {
                        if (iofg == "1")
                        {
                            if (enddt.Length == 6) enddt += "31"; // 외래인 경우 대비
                        }
                    }

                    // 전송완료된 내역이 있으며 전송완료된 내역을 가져온다.
                    string sql2 = "";
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT A.*, B.RESID AS RESID_R";
                    sql2 += Environment.NewLine + "  FROM TI84 A LEFT JOIN TI84R B ON B.DEMNO=A.DEMNO AND B.EPRTNO=A.EPRTNO AND B.RDATE=A.RDATE AND B.SEQNO=A.SEQNO ";
                    sql2 += Environment.NewLine + " WHERE A.DEMNO='" + row["DEMNO"].ToString() + "'";
                    sql2 += Environment.NewLine + "   AND A.EPRTNO='" + row["EPRTNO"].ToString() + "'";
                    sql2 += Environment.NewLine + "   AND A.SEQNO=(SELECT MAX(X.SEQNO) FROM TI84 X WHERE X.DEMNO=A.DEMNO AND X.EPRTNO=A.EPRTNO AND X.RDATE=A.RDATE)";
                    sql2 += Environment.NewLine + "   AND A.STATUS='Y'";
                    sql2 += Environment.NewLine + "   AND A.TMP_SEND_YN<>'Y'";
                    sql2 += Environment.NewLine + " ORDER BY A.DEMNO,A.EPRTNO,A.SEQNO";

                    int cnt = 0;
                    MetroLib.SqlHelper.GetDataReader(sql2, conn, delegate(OleDbDataReader reader)
                    {
                        cnt++;

                        CData data = new CData();
                        data.Clear();

                        data.SEL = true;
                        data.NO = (++no);
                        data.DEMNO = row["DEMNO"].ToString(); // 앞에서 읽은 자료를 사용한다.
                        data.EPRTNO = row["EPRTNO"].ToString(); // 앞에서 읽은 자료를 사용한다.
                        data.PID = row["PID"].ToString();
                        data.PNM = row["PNM"].ToString();
                        data.DPTCD = row["DPTCD"].ToString();
                        data.STEDT = row["STEDT"].ToString(); // 앞에서 읽은 자료를 사용한다.
                        data.ENDDT = enddt;
                        data.BDEDT = row["BDEDT"].ToString(); // 앞에서 읽은 자료를 사용한다.
                        data.PSEX = row["PSEX"].ToString();
                        data.RESID = row["RESID"].ToString();
                        data.QFYCD = row["QFYCD"].ToString();

                        // EMR306
                        data.RDATE = reader["RDATE"].ToString(); // 
                        data.Q1 = reader["Q1"].ToString();
                        data.Q2 = reader["Q2"].ToString();
                        data.Q3 = reader["Q3"].ToString();
                        data.Q4 = reader["Q4"].ToString();
                        data.Q5 = reader["Q5"].ToString();
                        data.Q6 = reader["Q6"].ToString();
                        data.Q7 = reader["Q7"].ToString();
                        data.Q8 = reader["Q8"].ToString();
                        data.Q9 = reader["Q9"].ToString();
                        data.Q10 = reader["Q10"].ToString();
                        data.Q11 = reader["Q11"].ToString();
                        data.Q12 = reader["Q12"].ToString();
                        data.Q13 = reader["Q13"].ToString();
                        data.Q14 = reader["Q14"].ToString();
                        data.Q15 = reader["Q15"].ToString();
                        data.Q16 = reader["Q16"].ToString();
                        data.Q17 = reader["Q17"].ToString();
                        data.Q18 = reader["Q18"].ToString();
                        data.Q19 = reader["Q19"].ToString();
                        data.Q20 = reader["Q20"].ToString();
                        data.Q21 = reader["Q21"].ToString();
                        data.Q22 = reader["Q22"].ToString();
                        data.Q23 = reader["Q23"].ToString();
                        data.Q24 = reader["Q24"].ToString();
                        data.Q25 = reader["Q25"].ToString();
                        data.Q26 = reader["Q26"].ToString();
                        data.OTHERS = reader["OTHERS"].ToString();
                        data.GUBUN = reader["GUBUN"].ToString();
                        data.DIAG = reader["DIAG"].ToString();
                        data.QOTHER = reader["QOTHER"].ToString();
                        data.Q25N = reader["Q25N"].ToString();
                        data.Q26N = reader["Q26N"].ToString();

                        data.RCV_NO_0000000 = (reader["RCV_NO"].ToString() == "0000000" ? "Y" : "");

                        data.A_KEY = reader["A_KEY"].ToString();
                        data.B_KEY = reader["B_KEY"].ToString();

                        data.STATUS = reader["STATUS"].ToString();
                        data.ERR_CODE = reader["ERR_CODE"].ToString();
                        data.ERR_DESC = reader["ERR_DESC"].ToString();

                        list.Add(data);

                        return true;
                    });

                    // 전송한 내역이 없으면 검사결과를 읽는다.
                    if (cnt < 1)
                    {

                        sql2 = "";
                        sql2 += Environment.NewLine + "SELECT B.RDATE";
                        sql2 += Environment.NewLine + "     , B.Q1,  B.Q2,  B.Q3,  B.Q4,  B.Q5,  B.Q6,  B.Q7,  B.Q8,  B.Q9,  B.Q10";
                        sql2 += Environment.NewLine + "     , B.Q11, B.Q12, B.Q13, B.Q14, B.Q15, B.Q16, B.Q17, B.Q18, B.Q19, B.Q20";
                        sql2 += Environment.NewLine + "     , B.Q21, B.Q22, B.Q23, B.Q24, B.Q25, B.Q26, B.OTHERS, B.GUBUN, B.DIAG, B.QOTHER, B.Q25N, B.Q26N";
                        sql2 += Environment.NewLine + "     , B.PID+','+B.BEDEDT+','+B.WDATE+','+CONVERT(VARCHAR,B.SEQ) AS B_KEY";
                        sql2 += Environment.NewLine + "     , B.NO_SEND_FG";
                        sql2 += Environment.NewLine + "  FROM EMR306 B";
                        sql2 += Environment.NewLine + " WHERE B.PID='" + row["PID"].ToString() + "'";
                        sql2 += Environment.NewLine + "   AND B.RDATE<='" + (iofg == "2" ? enddt : stedt) + "'";
                        sql2 += Environment.NewLine + "   AND ISNULL(B.UPDDT,'')=''";
                        sql2 += Environment.NewLine + " ORDER BY B.RDATE DESC";

                        MetroLib.SqlHelper.GetDataReader(sql2, conn, delegate(OleDbDataReader reader)
                        {
                            cnt++;

                            string noSendFg = reader["NO_SEND_FG"].ToString();
                            if (noSendFg != "1" || chkNoSendFg.Checked == true)
                            {
                                CData data = new CData();
                                data.Clear();

                                data.SEL = true;

                                // TI2A
                                data.NO = (++no);
                                data.DEMNO = row["DEMNO"].ToString();
                                data.EPRTNO = row["EPRTNO"].ToString();
                                data.PID = row["PID"].ToString();
                                data.PNM = row["PNM"].ToString();
                                data.DPTCD = row["DPTCD"].ToString();
                                data.STEDT = row["STEDT"].ToString();
                                data.ENDDT = enddt;
                                data.BDEDT = row["BDEDT"].ToString();
                                data.PSEX = row["PSEX"].ToString();
                                data.RESID = row["RESID"].ToString();
                                data.QFYCD = row["QFYCD"].ToString();
                                data.A_KEY = row["A_KEY"].ToString();

                                // EMR306
                                data.RDATE = reader["RDATE"].ToString();
                                data.Q1 = reader["Q1"].ToString();
                                data.Q2 = reader["Q2"].ToString();
                                data.Q3 = reader["Q3"].ToString();
                                data.Q4 = reader["Q4"].ToString();
                                data.Q5 = reader["Q5"].ToString();
                                data.Q6 = reader["Q6"].ToString();
                                data.Q7 = reader["Q7"].ToString();
                                data.Q8 = reader["Q8"].ToString();
                                data.Q9 = reader["Q9"].ToString();
                                data.Q10 = reader["Q10"].ToString();
                                data.Q11 = reader["Q11"].ToString();
                                data.Q12 = reader["Q12"].ToString();
                                data.Q13 = reader["Q13"].ToString();
                                data.Q14 = reader["Q14"].ToString();
                                data.Q15 = reader["Q15"].ToString();
                                data.Q16 = reader["Q16"].ToString();
                                data.Q17 = reader["Q17"].ToString();
                                data.Q18 = reader["Q18"].ToString();
                                data.Q19 = reader["Q19"].ToString();
                                data.Q20 = reader["Q20"].ToString();
                                data.Q21 = reader["Q21"].ToString();
                                data.Q22 = reader["Q22"].ToString();
                                data.Q23 = reader["Q23"].ToString();
                                data.Q24 = reader["Q24"].ToString();
                                data.Q25 = reader["Q25"].ToString();
                                data.Q26 = reader["Q26"].ToString();
                                data.OTHERS = reader["OTHERS"].ToString();
                                data.GUBUN = reader["GUBUN"].ToString();
                                data.DIAG = reader["DIAG"].ToString();
                                data.QOTHER = reader["QOTHER"].ToString();
                                data.Q25N = reader["Q25N"].ToString();
                                data.Q26N = reader["Q26N"].ToString();

                                data.B_KEY = reader["B_KEY"].ToString();

                                data.STATUS = (noSendFg == "1" ? "X" : ""); // STATUS가 X이면 전송제외 결과지임.

                                list.Add(data);
                            }

                            return false;
                        });
                    }

                    // 결과지가 없어도 명단은 조회한다.
                    if (chkAll.Checked == true && cnt < 1)
                    {
                        CData data = new CData();
                        data.Clear();

                        data.SEL = false;

                        // TI2A
                        data.NO = (++no);
                        data.DEMNO = row["DEMNO"].ToString();
                        data.EPRTNO = row["EPRTNO"].ToString();
                        data.PID = row["PID"].ToString();
                        data.PNM = row["PNM"].ToString();
                        data.DPTCD = row["DPTCD"].ToString();
                        data.STEDT = row["STEDT"].ToString();
                        data.ENDDT = enddt;
                        data.BDEDT = row["BDEDT"].ToString();
                        data.PSEX = row["PSEX"].ToString();
                        data.RESID = row["RESID"].ToString();
                        data.QFYCD = row["QFYCD"].ToString();
                        data.A_KEY = row["A_KEY"].ToString();

                        list.Add(data);
                    }

                    return true;
                });

                RefreshGridMain();
                RefreshGridSub();
            }
        }

        private string GetIofg(string p_demno, OleDbConnection p_conn)
        {
            string iofg = "";
            string sql = "";
            // 청구번호가 입원인지 검사
            sql = "SELECT COUNT(*) AS CNT FROM TI2A WHERE DEMNO='" + p_demno + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int cnt = 0;
                int.TryParse(reader["CNT"].ToString(), out cnt);
                if (cnt > 0) iofg = "2";
                return false;
            });
            if (iofg != "") return iofg;

            // 청구번호가 외래인지 검사
            sql = "SELECT COUNT(*) AS CNT FROM TI1A WHERE DEMNO='" + p_demno + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int cnt = 0;
                int.TryParse(reader["CNT"].ToString(), out cnt);
                if (cnt > 0) iofg = "1";
                return false;
            });
            if (iofg != "") return iofg;

            // 입원도 아니고, 외래도 아님. 없는 청구번호임.
            return iofg;
        }

        private string GetCnecno(string p_demno, OleDbConnection p_conn, ref string p_cnectdd, ref string p_dcount)
        {
            //string iofg = "";
            string qfycd = "";
            string addz1 = "";
            string addz2 = "";
            {
                int cnt = 0;
                // 청구번호가 입원인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                string sql = "SELECT * FROM TI2A WHERE DEMNO='" + p_demno + "'";
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    //iofg = "2";
                    qfycd = reader["QFYCD"].ToString();
                    addz1 = reader["ADDZ1"].ToString();
                    addz2 = reader["ADDZ2"].ToString();
                    return false;
                });
                if (cnt < 1)
                {
                    // 청구번호가 외래인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                    sql = "SELECT * FROM TI1A WHERE DEMNO='" + p_demno + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                    {
                        cnt++;
                        //iofg = "1";
                        qfycd = reader["QFYCD"].ToString();
                        addz1 = reader["ADDZ1"].ToString();
                        addz2 = reader["ADDZ2"].ToString();
                        return false;
                    });
                }

            }

            string cnecno = "";
            string cnectdd = "";
            string dcount = "";

            // 접수증을 읽는다.
            {
                String sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0102 A";
                sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + p_demno + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(A.CNECTNO,'')<>''"; // 접수번호가 있는 자료만
                sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnecno = reader["CNECTNO"].ToString(); // 접수번호
                    cnectdd = reader["CNECTDD"].ToString(); // 접수일자
                    return MetroLib.SqlHelper.BREAK;
                });
                // 2024.11.05 WOOIL - 접수증에 없으면 심결에서
                //                    심결에는 접수일자가 없다.
                if (cnecno == "")
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT * ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + p_demno + "'";
                    sql += System.Environment.NewLine + "   AND ISNULL(A.CNECNO,'')<>''"; // 접수번호가 있는 자료만
                    MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                    {
                        cnecno = reader["CNECNO"].ToString(); // 접수번호
                        return MetroLib.SqlHelper.BREAK;
                    });
                }
            }
            p_cnectdd = cnectdd;

            // 보완청구이면
            if ("1".Equals(addz1))
            {
                String sql = "";
                if (qfycd.StartsWith("3"))
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    dcount = reader["DCOUNT"].ToString(); // 청구서 일련번호
                    return false;
                });
            }
            p_dcount = dcount.Replace(" ","");

            return cnecno;
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

        private void RefreshGridSub()
        {
            if (grdSub.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            grdMainViewRowCellClick();
        }

        private void grdMainViewRowCellClick()
        {
            if (grdMainView.FocusedRowHandle < 0) return;

            List<CDataSub> listSub = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = listSub;

            List<CData> list = (List<CData>)grdMainView.DataSource;
            CData data = list[grdMainView.FocusedRowHandle];

            listSub.Add(new CDataSub("EPRTNO", data.EPRTNO));
            listSub.Add(new CDataSub("PNM", data.PNM + data.PID));
            listSub.Add(new CDataSub("STEDT", data.STEDT));
            listSub.Add(new CDataSub("ENDDT", data.ENDDT));
            listSub.Add(new CDataSub("RDATE", data.RDATE));
            listSub.Add(new CDataSub("VAS score (NRS)", ""));
            listSub.Add(new CDataSub("Q1", data.Q1));
            listSub.Add(new CDataSub("Q2", data.Q2));
            listSub.Add(new CDataSub("Motor abnormality", ""));
            listSub.Add(new CDataSub("Q3", data.Q3));
            listSub.Add(new CDataSub("Q4", data.Q4));
            listSub.Add(new CDataSub("Q5", data.Q5));
            listSub.Add(new CDataSub("Q6", data.Q6));
            listSub.Add(new CDataSub("Sensory abnormality", ""));
            listSub.Add(new CDataSub("Q7", data.Q7));
            listSub.Add(new CDataSub("Q8", data.Q8));
            listSub.Add(new CDataSub("Deep tendon reflex abnormality", ""));
            listSub.Add(new CDataSub("Q9", data.Q9));
            listSub.Add(new CDataSub("Q10", data.Q10));
            listSub.Add(new CDataSub("Q11", data.Q11));
            listSub.Add(new CDataSub("Q12", data.Q12));
            listSub.Add(new CDataSub("Pathologic reflex", ""));
            listSub.Add(new CDataSub("Q13", data.Q13));
            listSub.Add(new CDataSub("Q14", data.Q14));
            listSub.Add(new CDataSub("Q15", data.Q15));
            listSub.Add(new CDataSub("OTHERSYN", (data.OTHERS == "" ? "N" : "Y")));
            listSub.Add(new CDataSub("OTHERS", data.OTHERS));
            listSub.Add(new CDataSub("Physical examination", ""));
            listSub.Add(new CDataSub("Cervical", ""));
            listSub.Add(new CDataSub("Q16", data.Q16));
            listSub.Add(new CDataSub("Q17", data.Q17));
            listSub.Add(new CDataSub("Q18", data.Q18));
            listSub.Add(new CDataSub("Lumbar", ""));
            listSub.Add(new CDataSub("Q19", data.Q19));
            listSub.Add(new CDataSub("Q20", data.Q20));
            listSub.Add(new CDataSub("Gait", ""));
            listSub.Add(new CDataSub("Q21", data.Q21));
            listSub.Add(new CDataSub("Q22", data.Q22));
            listSub.Add(new CDataSub("Q23", data.Q23));
            listSub.Add(new CDataSub("Q24", data.Q24));
            listSub.Add(new CDataSub("진행되는 신경학적 결손 해당 여부", ""));
            listSub.Add(new CDataSub("Q25", data.Q25));
            listSub.Add(new CDataSub("Q26", data.Q26));
            listSub.Add(new CDataSub("결과", ""));
            listSub.Add(new CDataSub("DIAG", data.DIAG));

            txtMsg.Text = data.ERRMSG;

            string pid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PID").ToString();
            string stedt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "STEDT").ToString();
            QueryRdateList(pid, stedt);

            RefreshGridSub();
        }

        private void QueryRdateList(string p_pid, string p_stedt)
        {
            try
            {
                cboRDate.Items.Clear();

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {

                    conn.Open();

                    string sql = "";
                    sql += Environment.NewLine + "SELECT B.RDATE, B.PID+','+B.BEDEDT+','+B.WDATE+','+CONVERT(VARCHAR,B.SEQ) AS B_KEY";
                    sql += Environment.NewLine + "  FROM EMR306 B";
                    sql += Environment.NewLine + " WHERE B.PID='" + p_pid + "'";
                    if (txtIofg.Text.ToString() == "2")
                    {
                        sql += Environment.NewLine + "   AND B.BEDEDT<='" + p_stedt + "'";
                    }
                    else
                    {
                        sql += Environment.NewLine + "   AND B.RDATE<='" + p_stedt + "'";
                    }
                    sql += Environment.NewLine + "   AND ISNULL(B.UPDDT,'')=''";
                    sql += Environment.NewLine + " ORDER BY B.RDATE DESC";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        cboRDate.Items.Add(reader["RDATE"].ToString() + "         $" + reader["B_KEY"].ToString());

                        return true;
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            if (MessageBox.Show("전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            // 2023.01.16 WOOIL - 접수번호가 없으면 전송할지 물어보자.
            if (txtCnecno.Text.ToString() == "0000000" || txtCnecno.Text.ToString() == "")
            {
                if (MessageBox.Show("접수번호가 없습니다. 전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }


            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Send(false);
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

        private void btnTmpSend_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            if (MessageBox.Show("임시전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Send(true);
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("(" + m_pgm_step + ")" + ex.Message);
            }
        }

        private void Send(bool isTmp)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                List<CData> list = (List<CData>)grdMainView.DataSource;
                for (int turn = 0; turn < 2; turn++)
                {
                    // 2025.08.07 WOOIL - 맨 첫 줄이 항상 오류가 발생하는 경우가 있음. 인증서 로그인 전에 전송됨.
                    //                    두 번 보내는 것으로...
                    for (int row = 0; row < grdMainView.DataRowCount; row++)
                    {
                        grdMainView.FocusedRowHandle = row;
                        CData data = list[row];
                        if ((data.STATUS == (isTmp ? "T" : "Y") && data.RCV_NO_0000000 != "Y") || data.STATUS == "X")
                        {
                            // 전송에 성공한 자료는 다시 보내지 않는다. 임시전송 성공은 T임. 
                            // 전송제외 결과지는 보내지 않는다.
                        }
                        else if (data.B_KEY == "")
                        {
                            // 결과가 등록되지 않은 명세서임.
                            // 전송할 것이 없다.
                        }
                        else
                        {
                            SendOne(data, isTmp, sysdt, systm, conn);
                        }
                    }
                }
            }
        }

        private string SendOne(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            m_pgm_step = "";
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중
            RefreshGridMain();

            m_pgm_step = "D01";
            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();
            m_pgm_step = "D02";

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "ERM001"; //서식코드: 척추 MRI 퇴행성 질환
            dynReq.Metadata["FOM_VER"].Value = "001"; // 서식버전
            dynReq.Metadata["YKIHO"].Value = txtHosid.Text.ToString(); // 요양기관기호
            dynReq.Metadata["DMD_NO"].Value = txtDemno.Text.ToString(); // 청구번호
            dynReq.Metadata["RCV_NO"].Value = txtCnecno.Text.ToString(); // 접수번호(없는 경우 0000000)
            dynReq.Metadata["RCV_YR"].Value = txtCnectdd.Text.ToString().Substring(0, 4); // 접수년도
            dynReq.Metadata["BILL_SNO"].Value = txtBillSno.Text.ToString(); // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            dynReq.Metadata["INSUP_TP_CD"].Value = insup_tp_cd; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = "01"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // A. 검사정보
            dynReq.Elements["EXM_DD"].Value = data.RDATE; // 검사일자
            // B.검사결과
            dynReq.Elements["AX_PAIN_PNT_CD"].Value = (data.Q1.Length==1 ? "0" : "") + data.Q1; // 01,02,03,... 이렇게 2자리로 넘겨야 함.
            dynReq.Elements["ROOT_PAIN_PNT_CD"].Value = (data.Q2.Length == 1 ? "0" : "") + data.Q2; // 01,02,03,... 이렇게 2자리로 넘겨야 함.
            dynReq.Elements["RGHT_UPLB_LCMN_PNT_CD"].Value = data.Q4;
            dynReq.Elements["LEFT_UPLB_LCMN_PNT_CD"].Value = data.Q3;
            dynReq.Elements["RGHT_LWLB_LCMN_PNT_CD"].Value = data.Q6;
            dynReq.Elements["LEFT_LWLB_LCMN_PNT_CD"].Value = data.Q5;
            dynReq.Elements["UPLB_SEN_PNT_CD"].Value = data.Q7;
            dynReq.Elements["LWLB_SEN_PNT_CD"].Value = data.Q8;
            dynReq.Elements["RGHT_UPLB_DTR_PNT_CD"].Value = data.Q10;
            dynReq.Elements["LEFT_UPLB_DTR_PNT_CD"].Value = data.Q9;
            dynReq.Elements["RGHT_LWLB_DTR_PNT_CD"].Value = data.Q12;
            dynReq.Elements["LEFT_LWLB_DTR_PNT_CD"].Value = data.Q11;
            dynReq.Elements["BBSG_YN"].Value = data.GetYn(data.Q13);
            dynReq.Elements["ANKL_SG_YN"].Value = data.GetYn(data.Q14);
            dynReq.Elements["HOFM_SG_YN"].Value = data.GetYn(data.Q15);
            dynReq.Elements["ETC_YN"].Value = (data.OTHERS != "" ? "1" : "2");
            dynReq.Elements["ETC_EXM"].Value = data.OTHERS;
            dynReq.Elements["CVCL_VRTB_SPL_SG_YN"].Value = data.GetYn(data.Q16); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["CVCL_VRTB_LHMT_SG_YN"].Value = data.GetYn(data.Q17); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["CVCL_VRTB_SHLD_ABDT_YN"].Value = data.GetYn(data.Q18); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["LUMB_VRTB_SLRT_YN"].Value = data.GetYn(data.Q19); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["LUMB_VRTB_FNST_YN"].Value = data.GetYn(data.Q20); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["TTOE_GAIT_YN"].Value = data.GetYn(data.Q21); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["CCNA_GAIT_YN"].Value = data.GetYn(data.Q22); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["LIMP_GAIT_YN"].Value = data.GetYn(data.Q23); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["INMT_LIMP_GAIT_YN"].Value = data.GetYn(data.Q24); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["AGG_RDT_PAIN_SEN_PRYS_YN"].Value = data.GetYn(data.Q25); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["ERYY_NON_PTLG_REFX_YN"].Value = data.GetYn(data.Q26); // yes=1, no=2로 넘겨야 함.
            dynReq.Elements["DIAG_RST_IMPT_ABNL_OPN"].Value = data.DIAG;

            m_pgm_step = "E00";
            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(txtHosid.Text.ToString(), isTmp ? "createTmpDoc":"createDoc");
            m_pgm_step = "E01";
            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = "E"; // 오류
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                data.SUPL_DATA_FOM_CD = ""; //서식코드
                data.RCV_NO = ""; //접수번호
                data.SP_SNO = ""; //명세서일련번호
                data.HOSP_RNO = ""; //환자등록번호
                data.PAT_NM = ""; //환자성명
                data.INSUP_TP_CD = ""; //참고업무구분

                RefreshGridMain();
            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

                RefreshGridMain();
            }

            // 전송내역을 저장한다.
            SaveToTI84(data, isTmp, p_sysdt, p_systm, p_conn);

            return "";
        }

        private void SaveToTI84(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            // 저장
            int seqno = 0;
            // 2022.09.07 WOOIL - TI84의 테이블에는 RDATE가 PK로 되어있으나 SEQNO는 DEMNO,EPRTNO만드로 구한다.
            string sql = "";
            sql += Environment.NewLine + "SELECT MAX(SEQNO) AS MAX_SEQNO";
            sql += Environment.NewLine + "  FROM TI84";
            sql += Environment.NewLine + " WHERE DEMNO='" + data.DEMNO + "'";
            sql += Environment.NewLine + "   AND EPRTNO='" + data.EPRTNO + "'";
            //sql += Environment.NewLine + "   AND RDATE='" + data.RDATE + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int.TryParse(reader["MAX_SEQNO"].ToString(), out seqno);
                return false;
            });
            seqno++;

            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84(DEMNO,EPRTNO,RDATE,SEQNO,PID,PNM,RESID,QFYCD,DPTCD";
            sql += Environment.NewLine + "                ,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,OTHERS,GUBUN,DIAG,QOTHER,Q25N,Q26N";
            sql += Environment.NewLine + "                ,STATUS,ERR_CODE,ERR_DESC,A_KEY,B_KEY,TMP_SEND_YN";
            sql += Environment.NewLine + "                ,DOC_NO,SUPL_DATA_FOM_CD,RCV_NO,SP_SNO,HOSP_RNO,PAT_NM,INSUP_TP_CD";
            sql += Environment.NewLine + "                ,EMPID,SYSDT,SYSTM)";
            sql += Environment.NewLine + "VALUES";
            sql += Environment.NewLine + "('" + data.DEMNO + "'";
            sql += Environment.NewLine + ",'" + data.EPRTNO + "'";
            sql += Environment.NewLine + ",'" + data.RDATE + "'";
            sql += Environment.NewLine + ",'" + seqno.ToString() + "'";
            sql += Environment.NewLine + ",'" + data.PID + "'";
            sql += Environment.NewLine + ",'" + data.PNM + "'";
            sql += Environment.NewLine + ",'" + data.RESID + "'";
            sql += Environment.NewLine + ",'" + data.QFYCD + "'";
            sql += Environment.NewLine + ",'" + data.DPTCD + "'";

            sql += Environment.NewLine + ",'" + data.Q1 + "'";
            sql += Environment.NewLine + ",'" + data.Q2 + "'";
            sql += Environment.NewLine + ",'" + data.Q3 + "'";
            sql += Environment.NewLine + ",'" + data.Q4 + "'";
            sql += Environment.NewLine + ",'" + data.Q5 + "'";
            sql += Environment.NewLine + ",'" + data.Q6 + "'";
            sql += Environment.NewLine + ",'" + data.Q7 + "'";
            sql += Environment.NewLine + ",'" + data.Q8 + "'";
            sql += Environment.NewLine + ",'" + data.Q9 + "'";
            sql += Environment.NewLine + ",'" + data.Q10 + "'";
            sql += Environment.NewLine + ",'" + data.Q11 + "'";
            sql += Environment.NewLine + ",'" + data.Q12 + "'";
            sql += Environment.NewLine + ",'" + data.Q13 + "'";
            sql += Environment.NewLine + ",'" + data.Q14 + "'";
            sql += Environment.NewLine + ",'" + data.Q15 + "'";
            sql += Environment.NewLine + ",'" + data.Q16 + "'";
            sql += Environment.NewLine + ",'" + data.Q17 + "'";
            sql += Environment.NewLine + ",'" + data.Q18 + "'";
            sql += Environment.NewLine + ",'" + data.Q19 + "'";
            sql += Environment.NewLine + ",'" + data.Q20 + "'";
            sql += Environment.NewLine + ",'" + data.Q21 + "'";
            sql += Environment.NewLine + ",'" + data.Q22 + "'";
            sql += Environment.NewLine + ",'" + data.Q23 + "'";
            sql += Environment.NewLine + ",'" + data.Q24 + "'";
            sql += Environment.NewLine + ",'" + data.Q25 + "'";
            sql += Environment.NewLine + ",'" + data.Q26 + "'";
            sql += Environment.NewLine + ",'" + data.OTHERS + "'";
            sql += Environment.NewLine + ",'" + data.GUBUN + "'";
            sql += Environment.NewLine + ",'" + data.DIAG + "'";
            sql += Environment.NewLine + ",'" + data.QOTHER + "'";
            sql += Environment.NewLine + ",'" + data.Q25N + "'";
            sql += Environment.NewLine + ",'" + data.Q26N + "'";

            sql += Environment.NewLine + ",'" + data.STATUS + "'";
            sql += Environment.NewLine + ",'" + data.ERR_CODE + "'";
            sql += Environment.NewLine + ",'" + data.ERR_DESC + "'";
            sql += Environment.NewLine + ",'" + data.A_KEY + "'";
            sql += Environment.NewLine + ",'" + data.B_KEY + "'";
            sql += Environment.NewLine + ",'" + (isTmp == true ? "Y" : "") + "'";

            sql += Environment.NewLine + ",'" + data.DOC_NO + "'";
            sql += Environment.NewLine + ",'" + data.SUPL_DATA_FOM_CD + "'";
            sql += Environment.NewLine + ",'" + data.RCV_NO + "'";
            sql += Environment.NewLine + ",'" + data.SP_SNO + "'";
            sql += Environment.NewLine + ",'" + data.HOSP_RNO + "'";
            sql += Environment.NewLine + ",'" + data.PAT_NM + "'";
            sql += Environment.NewLine + ",'" + data.INSUP_TP_CD + "'";

            sql += Environment.NewLine + ",'" + m_User + "'";
            sql += Environment.NewLine + ",'" + p_sysdt + "'";
            sql += Environment.NewLine + ",'" + p_systm + "'";
            sql += Environment.NewLine + ")";

            MetroLib.SqlHelper.ExecuteSql(sql, p_conn);

            // 주민번호 별도저장(암호화때문에)
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84R(DEMNO,EPRTNO,RDATE,SEQNO,RESID)";
            sql += Environment.NewLine + "VALUES";
            sql += Environment.NewLine + "('" + data.DEMNO + "'";
            sql += Environment.NewLine + ",'" + data.EPRTNO + "'";
            sql += Environment.NewLine + ",'" + data.RDATE + "'";
            sql += Environment.NewLine + ",'" + seqno.ToString() + "'";
            sql += Environment.NewLine + ",'" + data.RESID + "'";
            sql += Environment.NewLine + ")";

            MetroLib.SqlHelper.ExecuteSql(sql, p_conn);
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            //}
        }

        private void grdMainView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "EPRTNO")
            {
                string eprtno1 = grdMainView.GetRowCellValue(e.RowHandle1, "EPRTNO").ToString();
                string eprtno2 = grdMainView.GetRowCellValue(e.RowHandle2, "EPRTNO").ToString();
                e.Merge = (eprtno1 == eprtno2); // 명일련 번호가 같으면 셀을 합친다.
                e.Handled = true;
            }
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;
            int row = e.RowHandle;
            if (row < 0) return;

            if (e.Column.FieldName == "STATUS_NM")
            {
                String val = e.CellValue.ToString();
                if (val == "오류")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
                if (val == "전송제외")
                {
                    e.Appearance.BackColor = Color.LightBlue;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

        private void grdMainView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;
            int row = e.RowHandle;
            if (row < 0) return;

            if (e.Column.FieldName == "STATUS_NM")
            {
                List<CData> list = (List<CData>)grdMainView.DataSource;
                CData data = list[row];

                if (data.RCV_NO_0000000 == "Y" && data.STATUS == "Y")
                {
                    Font underlineFont = null;
                    string words = e.CellValue.ToString();
                    Rectangle wordBounds = e.Bounds;
                    string currentString;
                    int currentStringWidth = 0;
                    int tempWidth;
                    currentString = words;
                    tempWidth = wordBounds.Width - currentStringWidth;
                    if (tempWidth < 0) return;

                    wordBounds = new Rectangle(wordBounds.X + currentStringWidth, wordBounds.Y, tempWidth, wordBounds.Height);
                    currentStringWidth = e.Appearance.CalcTextSizeInt(e.Cache, currentString, tempWidth).Width;
                    Font drawFont = e.Appearance.Font;
                    if (underlineFont == null)
                        underlineFont = e.Cache.GetFont(e.Appearance.Font, FontStyle.Underline);
                    drawFont = underlineFont;
                    e.Appearance.DrawString(e.Cache, words, wordBounds, drawFont, new StringFormat());

                    e.Handled = true;
                }
            }
        }

        private void mnuReQueryData_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.FocusedRowHandle < 0) return;

                List<CData> list = (List<CData>)grdMainView.DataSource;
                string eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
                string pid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PID").ToString();
                string pnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PNM").ToString();
                string rdate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "RDATE").ToString();

                CData data = list[grdMainView.FocusedRowHandle];

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT B.RDATE";
                    sql += Environment.NewLine + "     , B.Q1,  B.Q2,  B.Q3,  B.Q4,  B.Q5,  B.Q6,  B.Q7,  B.Q8,  B.Q9,  B.Q10";
                    sql += Environment.NewLine + "     , B.Q11, B.Q12, B.Q13, B.Q14, B.Q15, B.Q16, B.Q17, B.Q18, B.Q19, B.Q20";
                    sql += Environment.NewLine + "     , B.Q21, B.Q22, B.Q23, B.Q24, B.Q25, B.Q26, B.OTHERS, B.GUBUN, B.DIAG, B.QOTHER, B.Q25N, B.Q26N";
                    sql += Environment.NewLine + "     , B.PID+','+B.BEDEDT+','+B.WDATE+','+CONVERT(VARCHAR,B.SEQ) AS B_KEY";
                    sql += Environment.NewLine + "     , B.NO_SEND_FG";
                    sql += Environment.NewLine + "  FROM EMR306 B";
                    sql += Environment.NewLine + " WHERE B.PID='" + data.PID + "'";
                    sql += Environment.NewLine + "   AND B.RDATE='" + data.RDATE + "'";
                    sql += Environment.NewLine + "   AND ISNULL(B.UPDDT,'')=''";
                    sql += Environment.NewLine + " ORDER BY B.RDATE DESC";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        // EMR306
                        data.RDATE = reader["RDATE"].ToString();
                        data.Q1 = reader["Q1"].ToString();
                        data.Q2 = reader["Q2"].ToString();
                        data.Q3 = reader["Q3"].ToString();
                        data.Q4 = reader["Q4"].ToString();
                        data.Q5 = reader["Q5"].ToString();
                        data.Q6 = reader["Q6"].ToString();
                        data.Q7 = reader["Q7"].ToString();
                        data.Q8 = reader["Q8"].ToString();
                        data.Q9 = reader["Q9"].ToString();
                        data.Q10 = reader["Q10"].ToString();
                        data.Q11 = reader["Q11"].ToString();
                        data.Q12 = reader["Q12"].ToString();
                        data.Q13 = reader["Q13"].ToString();
                        data.Q14 = reader["Q14"].ToString();
                        data.Q15 = reader["Q15"].ToString();
                        data.Q16 = reader["Q16"].ToString();
                        data.Q17 = reader["Q17"].ToString();
                        data.Q18 = reader["Q18"].ToString();
                        data.Q19 = reader["Q19"].ToString();
                        data.Q20 = reader["Q20"].ToString();
                        data.Q21 = reader["Q21"].ToString();
                        data.Q22 = reader["Q22"].ToString();
                        data.Q23 = reader["Q23"].ToString();
                        data.Q24 = reader["Q24"].ToString();
                        data.Q25 = reader["Q25"].ToString();
                        data.Q26 = reader["Q26"].ToString();
                        data.OTHERS = reader["OTHERS"].ToString();
                        data.GUBUN = reader["GUBUN"].ToString();
                        data.DIAG = reader["DIAG"].ToString();
                        data.QOTHER = reader["QOTHER"].ToString();
                        data.Q25N = reader["Q25N"].ToString();
                        data.Q26N = reader["Q26N"].ToString();

                        data.B_KEY = reader["B_KEY"].ToString();

                        data.STATUS = "";

                        return MetroLib.SqlHelper.BREAK;
                    });
                }
                RefreshGridMain();
                grdMainViewRowCellClick();
                MessageBox.Show("조회가 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void mnuRemoveData_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.RemoveDataOne(true) == true)
                {
                    this.Query(); // 성공하면 다시 조회한다.
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("처리가 성공적으로 완료되었습니다.");
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuCancelRemoveData_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.RemoveDataOne(false) == true)
                {
                    this.Query(); // 성공하면 다시 조회한다.
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("처리가 성공적으로 완료되었습니다.");
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private bool RemoveDataOne(bool isNoSend)
        {
            bool bRet = false;
            List<CData> list = (List<CData>)grdMainView.DataSource;
            string eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
            string pid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PID").ToString();
            string pnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "PNM").ToString();
            string rdate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "RDATE").ToString();

            CData data = list[grdMainView.FocusedRowHandle];
            if (data.STATUS == "Y")
            {
                // 전송에 성공한 자료는 제외하지 않는다.
                MessageBox.Show("전송이 완료된 자료는 제외할 수 없습니다.");
            }
            else if (data.B_KEY == "")
            {
                // 결과가 등록되지 않은 명세서임.
                MessageBox.Show("결과가 등록되지 않은 명세서입니다.");
            }
            else
            {
                string msg = "명일련 : " + eprtno + ", 환자ID : " + pid + ", 환자명 : " + pnm + ", 검사일자 : " + rdate + Environment.NewLine
                           + Environment.NewLine
                           + "자료를 " + (isNoSend == true ? "[전송 제외]" : "[전송 제외 취소]") + "하시겠습니까?";
                if (MessageBox.Show(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bRet = RemoveDataOne(data, isNoSend);
                }
            }
            return bRet;
        }

        private bool RemoveDataOne(CData data, bool isNoSend)
        {
            string[] arrKey = data.B_KEY.Split(',');
            string pid = arrKey[0];
            string bededt = arrKey[1];
            string wdate = arrKey[2];
            string seq = arrKey[3];

            string noSendFg = (isNoSend == true ? "1" : "");
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "UPDATE EMR306 SET NO_SEND_FG='" + noSendFg + "' WHERE PID='" + pid + "' AND BEDEDT='" + bededt + "' AND WDATE='" + wdate + "' AND SEQ=" + seq + "";
                    MetroLib.SqlHelper.ExecuteSql(sql, conn);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void grdMainView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            grdMainViewRowCellClick();
        }

        private void chkNoSendFg_CheckedChanged(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeRdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeRdate()
        {
            string b_key = cboRDate.SelectedItem.ToString().Split('$')[1];
            string[] key = b_key.Split(',');
            string pid = key[0];
            string bededt = key[1];
            string wdate = key[2];
            string seq = key[3];

            List<CDataSub> listSub = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = listSub;

            string eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
            string rdate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "RDATE").ToString();
            List<CData> list = (List<CData>)grdMainView.DataSource;
            CData data = list[grdMainView.FocusedRowHandle];
            // 검사결과지 변경
            if (data.STATUS == "Y")
            {
                MessageBox.Show("전송완료한 자료는 선택을 변경할 수 없습니다.");
                return;
            }
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT B.RDATE";
                sql += Environment.NewLine + "     , B.Q1,  B.Q2,  B.Q3,  B.Q4,  B.Q5,  B.Q6,  B.Q7,  B.Q8,  B.Q9,  B.Q10";
                sql += Environment.NewLine + "     , B.Q11, B.Q12, B.Q13, B.Q14, B.Q15, B.Q16, B.Q17, B.Q18, B.Q19, B.Q20";
                sql += Environment.NewLine + "     , B.Q21, B.Q22, B.Q23, B.Q24, B.Q25, B.Q26, B.OTHERS, B.GUBUN, B.DIAG, B.QOTHER, B.Q25N, B.Q26N";
                sql += Environment.NewLine + "     , B.PID+','+B.BEDEDT+','+B.WDATE+','+CONVERT(VARCHAR,B.SEQ) AS B_KEY";
                sql += Environment.NewLine + "     , B.NO_SEND_FG";
                sql += Environment.NewLine + "  FROM EMR306 B";
                sql += Environment.NewLine + " WHERE B.PID='" + pid + "'";
                sql += Environment.NewLine + "   AND B.BEDEDT='" + bededt + "'";
                sql += Environment.NewLine + "   AND B.WDATE='" + wdate + "'";
                sql += Environment.NewLine + "   AND B.SEQ='" + seq + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    // 추가하려는 검사일자가 있는지 점검
                    string r_date = row["RDATE"].ToString();
                    bool bOK = false;
                    foreach (CData data2 in list)
                    {
                        if (data2.DEMNO == data.DEMNO && data2.EPRTNO == data.EPRTNO)
                        {
                            bOK = true;
                            if (data.RDATE == r_date)
                            {
                                bOK = false;
                                break;
                            }
                        }
                    }
                    if (bOK == false)
                    {
                        MessageBox.Show("이미 있는 검사일로 변경할 수 없습니다.");
                    }
                    else
                    {
                        // EMR306
                        data.RDATE = row["RDATE"].ToString();
                        data.Q1 = row["Q1"].ToString();
                        data.Q2 = row["Q2"].ToString();
                        data.Q3 = row["Q3"].ToString();
                        data.Q4 = row["Q4"].ToString();
                        data.Q5 = row["Q5"].ToString();
                        data.Q6 = row["Q6"].ToString();
                        data.Q7 = row["Q7"].ToString();
                        data.Q8 = row["Q8"].ToString();
                        data.Q9 = row["Q9"].ToString();
                        data.Q10 = row["Q10"].ToString();
                        data.Q11 = row["Q11"].ToString();
                        data.Q12 = row["Q12"].ToString();
                        data.Q13 = row["Q13"].ToString();
                        data.Q14 = row["Q14"].ToString();
                        data.Q15 = row["Q15"].ToString();
                        data.Q16 = row["Q16"].ToString();
                        data.Q17 = row["Q17"].ToString();
                        data.Q18 = row["Q18"].ToString();
                        data.Q19 = row["Q19"].ToString();
                        data.Q20 = row["Q20"].ToString();
                        data.Q21 = row["Q21"].ToString();
                        data.Q22 = row["Q22"].ToString();
                        data.Q23 = row["Q23"].ToString();
                        data.Q24 = row["Q24"].ToString();
                        data.Q25 = row["Q25"].ToString();
                        data.Q26 = row["Q26"].ToString();
                        data.OTHERS = row["OTHERS"].ToString();
                        data.GUBUN = row["GUBUN"].ToString();
                        data.DIAG = row["DIAG"].ToString();
                        data.QOTHER = row["QOTHER"].ToString();
                        data.Q25N = row["Q25N"].ToString();
                        data.Q26N = row["Q26N"].ToString();

                        data.B_KEY = row["B_KEY"].ToString();

                        data.STATUS = (row["NO_SEND_FG"].ToString() == "1" ? "X" : ""); // STATUS가 X이면 전송제외 결과지임.
                    }
                    return false;
                });
            }
            grdMainViewRowCellClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddRdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddRdate()
        {
            string b_key = cboRDate.SelectedItem.ToString().Split('$')[1];
            string[] key = b_key.Split(',');
            string pid = key[0];
            string bededt = key[1];
            string wdate = key[2];
            string seq = key[3];

            List<CDataSub> listSub = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = listSub;

            string eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
            string rdate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "RDATE").ToString();
            List<CData> list = (List<CData>)grdMainView.DataSource;
            CData data = list[grdMainView.FocusedRowHandle];
            // 검사결과지 추가
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT B.RDATE";
                sql += Environment.NewLine + "     , B.Q1,  B.Q2,  B.Q3,  B.Q4,  B.Q5,  B.Q6,  B.Q7,  B.Q8,  B.Q9,  B.Q10";
                sql += Environment.NewLine + "     , B.Q11, B.Q12, B.Q13, B.Q14, B.Q15, B.Q16, B.Q17, B.Q18, B.Q19, B.Q20";
                sql += Environment.NewLine + "     , B.Q21, B.Q22, B.Q23, B.Q24, B.Q25, B.Q26, B.OTHERS, B.GUBUN, B.DIAG, B.QOTHER, B.Q25N, B.Q26N";
                sql += Environment.NewLine + "     , B.PID+','+B.BEDEDT+','+B.WDATE+','+CONVERT(VARCHAR,B.SEQ) AS B_KEY";
                sql += Environment.NewLine + "     , B.NO_SEND_FG";
                sql += Environment.NewLine + "  FROM EMR306 B";
                sql += Environment.NewLine + " WHERE B.PID='" + pid + "'";
                sql += Environment.NewLine + "   AND B.BEDEDT='" + bededt + "'";
                sql += Environment.NewLine + "   AND B.WDATE='" + wdate + "'";
                sql += Environment.NewLine + "   AND B.SEQ='" + seq + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    // 추가하려는 검사일자가 있는지 점검
                    string r_date = row["RDATE"].ToString();
                    bool bOK = false;
                    foreach (CData data2 in list)
                    {
                        if (data2.DEMNO == data.DEMNO && data2.EPRTNO == data.EPRTNO)
                        {
                            bOK = true;
                            if (data.RDATE == r_date)
                            {
                                bOK = false;
                                break;
                            }
                        }
                    }
                    if (bOK == false)
                    {
                        MessageBox.Show("이미 있는 검사일은 추가할 수 없습니다.");
                    }
                    else
                    {
                        CData addData = new CData();
                        addData.Clear();

                        addData.SEL = true;

                        addData.DEMNO = data.DEMNO;
                        addData.EPRTNO = data.EPRTNO;
                        addData.PID = data.PID;
                        addData.PNM = data.PNM;
                        addData.PSEX = data.PSEX;
                        addData.DPTCD = data.DPTCD;
                        addData.STEDT = data.STEDT;
                        addData.BDEDT = data.BDEDT;
                        addData.RESID = data.RESID;
                        addData.QFYCD = data.QFYCD;

                        // EMR306
                        addData.RDATE = row["RDATE"].ToString();
                        addData.Q1 = row["Q1"].ToString();
                        addData.Q2 = row["Q2"].ToString();
                        addData.Q3 = row["Q3"].ToString();
                        addData.Q4 = row["Q4"].ToString();
                        addData.Q5 = row["Q5"].ToString();
                        addData.Q6 = row["Q6"].ToString();
                        addData.Q7 = row["Q7"].ToString();
                        addData.Q8 = row["Q8"].ToString();
                        addData.Q9 = row["Q9"].ToString();
                        addData.Q10 = row["Q10"].ToString();
                        addData.Q11 = row["Q11"].ToString();
                        addData.Q12 = row["Q12"].ToString();
                        addData.Q13 = row["Q13"].ToString();
                        addData.Q14 = row["Q14"].ToString();
                        addData.Q15 = row["Q15"].ToString();
                        addData.Q16 = row["Q16"].ToString();
                        addData.Q17 = row["Q17"].ToString();
                        addData.Q18 = row["Q18"].ToString();
                        addData.Q19 = row["Q19"].ToString();
                        addData.Q20 = row["Q20"].ToString();
                        addData.Q21 = row["Q21"].ToString();
                        addData.Q22 = row["Q22"].ToString();
                        addData.Q23 = row["Q23"].ToString();
                        addData.Q24 = row["Q24"].ToString();
                        addData.Q25 = row["Q25"].ToString();
                        addData.Q26 = row["Q26"].ToString();
                        addData.OTHERS = row["OTHERS"].ToString();
                        addData.GUBUN = row["GUBUN"].ToString();
                        addData.DIAG = row["DIAG"].ToString();
                        addData.QOTHER = row["QOTHER"].ToString();
                        addData.Q25N = row["Q25N"].ToString();
                        addData.Q26N = row["Q26N"].ToString();

                        addData.B_KEY = row["B_KEY"].ToString();

                        addData.STATUS = (row["NO_SEND_FG"].ToString() == "1" ? "X" : ""); // STATUS가 X이면 전송제외 결과지임.

                        list.Insert(grdMainView.FocusedRowHandle+1, addData);

                        RefreshGridMain();
                    }
                    return false;
                });
            }
            grdMainViewRowCellClick();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
        }

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void txtHosid_DoubleClick(object sender, EventArgs e)
        {
            string hosId = txtHosid.Text.ToString();
            DialogResult dr = InputBox("입력", "요양기관기호", ref hosId);
            if (dr == DialogResult.OK)
            {
                txtHosid.Text = hosId;
            }
        }

        public static DialogResult InputBox(string title, string content, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.ClientSize = new Size(300, 100);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            form.Text = title;
            label.Text = content;
            textBox.Text = value;
            buttonOk.Text = "확인";
            buttonCancel.Text = "취소";

            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(45, 17, 100, 20);
            textBox.SetBounds(45, 40, 220, 20);
            buttonOk.SetBounds(115, 70, 70, 20);
            buttonCancel.SetBounds(195, 70, 70, 20);

            DialogResult dialogResult = form.ShowDialog();

            value = textBox.Text;
            return dialogResult;
        }

        public static DialogResult InputBox2(string title, string content, ref string value, string content2, ref string value2)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Label label2 = new Label();
            TextBox textBox2 = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.ClientSize = new Size(300, 150);
            form.Controls.AddRange(new Control[] { label, textBox, label2, textBox2, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            form.Text = title;
            label.Text = content;
            textBox.Text = value;
            label2.Text = content2;
            textBox2.Text = value2;
            buttonOk.Text = "확인";
            buttonCancel.Text = "취소";

            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(25, 30, 60, 20);
            textBox.SetBounds(90, 30, 170, 20);
            label2.SetBounds(25, 58, 60, 20);
            textBox2.SetBounds(90, 58, 170, 20);

            buttonOk.SetBounds(95, 100, 70, 20);
            buttonCancel.SetBounds(170, 100, 70, 20);

            DialogResult dialogResult = form.ShowDialog();

            value = textBox.Text;
            value2 = textBox2.Text;

            return dialogResult;
        }

        public static DialogResult InputBox3(string title, string content, ref string value, string content2, ref string value2, string content3, ref string value3)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Label label2 = new Label();
            TextBox textBox2 = new TextBox();
            Label label3 = new Label();
            TextBox textBox3 = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.ClientSize = new Size(300,  200);
            form.Controls.AddRange(new Control[] { label, textBox, label2, textBox2, label3, textBox3, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            form.Text = title;
            label.Text = content;
            textBox.Text = value;
            label2.Text = content2;
            textBox2.Text = value2;
            label3.Text = content3;
            textBox3.Text = value3;
            buttonOk.Text = "확인";
            buttonCancel.Text = "취소";

            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(25, 30, 60, 20);
            textBox.SetBounds(90, 30, 170, 20);
            label2.SetBounds(25, 58, 60, 20);
            textBox2.SetBounds(90, 58, 170, 20);
            label3.SetBounds(25, 86, 60, 20);
            textBox3.SetBounds(90, 85, 170, 20);

            buttonOk.SetBounds(95, 130, 70, 20);
            buttonCancel.SetBounds(170, 130, 70, 20);

            DialogResult dialogResult = form.ShowDialog();

            value = textBox.Text.ToString();
            value2 = textBox2.Text.ToString();
            value3 = textBox3.Text.ToString();

            return dialogResult;
        }

        private void chkSendAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<CData> list = (List<CData>)grdMainView.DataSource;
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    CData data = list[row];
                    data.SEL = chkSendAll.Checked;
                }
                RefreshGridMain();
                RefreshGridSub();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCnecno_DoubleClick(object sender, EventArgs e)
        {
            string cnecno = txtCnecno.Text.ToString();
            string cnectdd = txtCnectdd.Text.ToString();
            string billsno = txtBillSno.Text.ToString();
            if (cnecno == "0000000") cnecno = "";

            DialogResult dr = InputBox3("입력", "접수번호", ref cnecno, "접수일자", ref cnectdd,"청일련",ref billsno);
            if (dr == DialogResult.OK)
            {
                txtCnecno.Text = cnecno;
                txtCnectdd.Text = cnectdd;
                txtBillSno.Text = billsno;
            }
        }

        private void txtCnectdd_DoubleClick(object sender, EventArgs e)
        {
            string cnectdd = txtCnectdd.Text.ToString();
            DialogResult dr = InputBox("입력", "접수일자", ref cnectdd);
            if (dr == DialogResult.OK)
            {
                txtCnectdd.Text = cnectdd;
            }
        }

        private void txtBillSno_DoubleClick(object sender, EventArgs e)
        {
            string billsno = txtBillSno.Text.ToString();
            DialogResult dr = InputBox("입력", "청일련", ref billsno);
            if (dr == DialogResult.OK)
            {
                txtBillSno.Text = billsno;
            }
        }

    }
}

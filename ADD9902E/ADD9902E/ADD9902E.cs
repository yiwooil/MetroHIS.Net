using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9902E
{
    public partial class ADD9902E : Form
    {
        private bool IsFirst;
        private bool OnPgm;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;

        public ADD9902E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";
        }

        public ADD9902E(String user, String pwd, String prjcd)
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

        private void ADD9902E_Load(object sender, EventArgs e)
        {
            this.GetHosInfo();
        }

        private void ADD9902E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
        }

        private void GetHosInfo()
        {
            try
            {
                string multi = m_HospMulti;
                string sysdate = "";

                string strConn = MetroLib.DBHelper.GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // DB 서버 날짜
                    string sql = "";
                    sql += Environment.NewLine + "SELECT CONVERT(VARCHAR,GETDATE(),112) AS HDATE";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        sysdate = reader["HDATE"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 대행청구 기관 목록
                    cboREDEM.Items.Clear();

                    sql = "";
                    sql += Environment.NewLine + "SELECT CDNM";
                    sql += Environment.NewLine + "  FROM TI88";
                    sql += Environment.NewLine + " WHERE MST1CD='A'";
                    sql += Environment.NewLine + "   AND MST2CD='REDEMID'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        cboREDEM.Items.Add(reader["CDNM"].ToString());
                        return MetroLib.SqlHelper.CONTINUE;
                    });

                    // 서면 명세서 출력 버전 목록
                    cboPrtVer.Items.Clear();
                    cboPrtVer.Items.Add("0. 요양급여.의료급여비용명세서 출력시 2005년 01월 변경된 서식(다중바코드)으로 출력됩니다.");
                    cboPrtVer.Items.Add("1. 요양급여.의료급여비용명세서 출력시 2005년 10월 변경된 서식(보훈국비환자 진료분 심사수탁)으로 출력됩니다.");
                    cboPrtVer.Items.Add("2. 요양급여.의료급여비용명세서 출력시 2006년 06월 변경된 서식(식대,PET 항목 추가)으로 출력됩니다.");

                    // 병원 일반사항
                    txtHosNm.Text = ReadTA88_HOSPITAL("1", "FLD1QTY", multi, conn);
                    txtHosId.Text = ReadTA88_HOSPITAL("2", "FLD1QTY", multi, conn);
                    txtHosId2.Text = ReadTA88_HOSPITAL("2", "FLD2QTY", multi, conn);
                    txtHosAddr.Text = ReadTA88_HOSPITAL("3", "FLD1QTY", multi, conn);
                    txtHosJB.Text = ReadTA88_HOSPITAL("4", "FLD1QTY", multi, conn);
                    txtHosCEO.Text = ReadTA88_HOSPITAL("5", "FLD1QTY", multi, conn);
                    txtHOSCEORID.Text = ReadTA88_HOSPITAL("5", "FLD2QTY", multi, conn);
                    txtWorkNM.Text = ReadTA88_HOSPITAL("48", "FLD1QTY", multi, conn);
                    txtWorkRID.Text = ReadTA88_HOSPITAL("48", "FLD2QTY", multi, conn);
                    txtDemNmSanje.Text = ReadTA88_HOSPITAL("48", "FLD3QTY", multi, conn);

                    string redem = ReadTA88_HOSPITAL("101", "FLD2QTY", multi, conn);

                    // DropDownList에서도 목록에 없는 저장값 표시
                    if (redem != "" && cboREDEM.FindStringExact(redem) < 0)
                    {
                        cboREDEM.Items.Add(redem);
                    }

                    cboREDEM.Text = redem;

                    // 오늘 기준 병원종별
                    string hosJBcd = HospitalJong(sysdate, multi, conn);

                    switch (hosJBcd)
                    {
                        case "1":
                            txtHosJB.Text = "전문요양기관";
                            break;
                        case "2":
                            txtHosJB.Text = "종합병원";
                            break;
                        case "3":
                            txtHosJB.Text = "병원";
                            break;
                        case "4":
                            txtHosJB.Text = "의원";
                            break;
                        case "5":
                            txtHosJB.Text = "약국";
                            break;
                    }

                    // 원본의 최종 조회값: 병원종별에 따른 서식버전
                    txtEDIVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD2QTY", conn);
                    txtDENTVer.Text = txtEDIVer.Text;
                    txtDRGVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD3QTY", conn);
                    txtPHYVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD4QTY", conn);
                    txtHANVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD1CD", conn);
                    txtYOYANGVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD2CD", conn);
                    txtBloodVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD3CD", conn);
                    txtPaCareVer.Text = ReadTI88("MIGVER", hosJBcd, "FLD4CD", conn);

                    // 병원종별에 따른 소프트웨어 검수번호
                    txtOkDno.Text = ReadTI88("SWNO", hosJBcd, "FLD2QTY", conn);
                    txtOkDnoDENT.Text = ReadTI88("SWNO", hosJBcd, "FLD3QTY", conn);
                    txtOkDnoHAN.Text = ReadTI88("SWNO", hosJBcd, "FLD4QTY", conn);
                    txtOkDnoPHY.Text = ReadTI88("SWNO", hosJBcd, "FLD1CD", conn);
                    txtOkDnoDRG.Text = ReadTI88("SWNO", hosJBcd, "FLD2CD", conn);
                    txtOkDnoYOYANG.Text = ReadTI88("SWNO", hosJBcd, "FLD3CD", conn);
                    txtOkDnoBlood.Text = ReadTI88("SWNO", hosJBcd, "FLD4CD", conn);
                    txtOkDnoPaCare.Text = ReadTI88("SWNO", hosJBcd, "FLD5CD", conn);

                    // 원본처럼 기본 HOSPITAL의 출력 버전 조회
                    string prtVer = ReadTI88_HOSPITAL("7", "FLD2QTY", "", conn);

                    if (prtVer == "") prtVer = "0";

                    cboPrtVer.SelectedIndex = Convert.ToInt32(prtVer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ReadTA88_HOSPITAL(string p_strMst3cd, string p_strFldName, string p_strMulti, OleDbConnection p_conn)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = "HOSPITAL" + p_strMulti;
            string mst3cd = p_strMst3cd;
            string ret = "";

            string sql = "";
            sql += Environment.NewLine + "SELECT " + field + " AS FLDDATA";
            sql += Environment.NewLine + "  FROM TA88";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FLDDATA"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return ret;
        }

        private string ReadTI88_HOSPITAL(string p_strMst3cd, string p_strFldName, string p_strMulti, OleDbConnection p_conn)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = "HOSPITAL" + p_strMulti;
            string mst3cd = p_strMst3cd;
            string ret = "";

            string sql = "";
            sql += Environment.NewLine + "SELECT " + field + " AS FLDDATA";
            sql += Environment.NewLine + "  FROM TI88";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FLDDATA"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return ret;
        }

        private string ReadTI88(string p_strMst2cd, string p_strMst3cd, string p_strFldName, OleDbConnection p_conn)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = p_strMst2cd;
            string mst3cd = p_strMst3cd;
            string ret = "";

            string sql = "";
            sql += Environment.NewLine + "SELECT " + field + " AS FLDDATA";
            sql += Environment.NewLine + "  FROM TI88";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FLDDATA"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return ret;
        }

        private string HospitalJong(string p_strExdt, string p_strMulti, OleDbConnection p_conn)
        {
            string mst2cd = "HOSPITAL" + p_strMulti;
            string exdt = p_strExdt;
            string ret = "";

            bool exists = false;

            string sql = "";
            sql += Environment.NewLine + "SELECT FLD1CD";
            sql += Environment.NewLine + "  FROM TA88A";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD='4'";
            sql += Environment.NewLine + "   AND MST4CD=(SELECT MAX(X.MST4CD)";
            sql += Environment.NewLine + "                 FROM TA88A X";
            sql += Environment.NewLine + "                WHERE X.MST1CD='A'";
            sql += Environment.NewLine + "                  AND X.MST2CD=?";
            sql += Environment.NewLine + "                  AND X.MST3CD='4'";
            sql += Environment.NewLine + "                  AND X.MST4CD<=?";
            sql += Environment.NewLine + "              )";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst2cd);
            para.Add(exdt);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, delegate(OleDbDataReader reader)
            {
                exists = true;
                ret = reader["FLD1CD"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            // 원본처럼 이력 행이 없을 때만 TA88 조회
            if (exists == false)
            {
                ret = ReadTA88_HOSPITAL("4", "FLD1CD", p_strMulti, p_conn);
            }

            return ret;
        }

        private int SaveTA88_HOSPITAL(string p_strMst3cd, string p_strFldName, string p_strFldData, string p_strMulti, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = "HOSPITAL" + p_strMulti;
            string mst3cd = p_strMst3cd;
            string data = p_strFldData;

            bool exists = false;

            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA88";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                exists = true;
                return MetroLib.SqlHelper.BREAK;
            });

            para = new List<Object>();

            if (exists)
            {
                sql = "";
                sql += Environment.NewLine + "UPDATE TA88";
                sql += Environment.NewLine + "   SET " + field + "=?";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD=?";
                sql += Environment.NewLine + "   AND MST3CD=?";

                // OleDb의 ? 순서대로 추가
                para.Add(data);
                para.Add(mst2cd);
                para.Add(mst3cd);
            }
            else
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TA88(MST1CD, MST2CD, MST3CD, " + field + ")";
                sql += Environment.NewLine + "VALUES ('A', ?, ?, ?)";

                para.Add(mst2cd);
                para.Add(mst3cd);
                para.Add(data);
            }

            return MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        private int SaveTI88_HOSPITAL(string p_strMst3cd, string p_strFldName, string p_strFldData, string p_strMulti, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = "HOSPITAL" + p_strMulti;
            string mst3cd = p_strMst3cd;
            string data = p_strFldData;

            bool exists = false;

            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI88";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                exists = true;
                return MetroLib.SqlHelper.BREAK;
            });

            para = new List<Object>();

            if (exists)
            {
                sql = "";
                sql += Environment.NewLine + "UPDATE TI88";
                sql += Environment.NewLine + "   SET " + field + "=?";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD=?";
                sql += Environment.NewLine + "   AND MST3CD=?";

                // OleDb의 ? 순서대로 추가
                para.Add(data);
                para.Add(mst2cd);
                para.Add(mst3cd);
            }
            else
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI88(MST1CD, MST2CD, MST3CD, " + field + ")";
                sql += Environment.NewLine + "VALUES ('A', ?, ?, ?)";

                para.Add(mst2cd);
                para.Add(mst3cd);
                para.Add(data);
            }

            return MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        private int SaveTA88A_HOSPITAL(string p_strMst3cd, string p_strExdt, string p_strFldName, string p_strFldData, string p_strMulti, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string field = "[" + p_strFldName + "]";
            string mst2cd = "HOSPITAL" + p_strMulti;
            string mst3cd = p_strMst3cd;
            string exdt = p_strExdt;
            string data = p_strFldData;
            string mst4cd = "";

            bool exists = false;

            string sql = "";
            sql += Environment.NewLine + "SELECT MAX(MST4CD) AS MAX_MST4CD";
            sql += Environment.NewLine + "  FROM TA88A";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";
            sql += Environment.NewLine + "   AND MST4CD<=?";

            List<Object> para = new List<Object>();
            para.Add(mst2cd);
            para.Add(mst3cd);
            para.Add(exdt);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                exists = true;
                mst4cd = reader["MAX_MST4CD"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            para = new List<Object>();

            if (exists)
            {
                sql = "";
                sql += Environment.NewLine + "UPDATE TA88A";
                sql += Environment.NewLine + "   SET " + field + "=?";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD=?";
                sql += Environment.NewLine + "   AND MST3CD=?";
                sql += Environment.NewLine + "   AND MST4CD=?";

                // OleDb의 ? 순서대로 추가
                para.Add(data);
                para.Add(mst2cd);
                para.Add(mst3cd);
                para.Add(mst4cd);

                return MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            return 0;
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

                MessageBox.Show("저장되었습니다.");

                this.GetHosInfo();// 재조회
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
            string multi = m_HospMulti;

            string hosId = txtHosId.Text;
            string hosId2 = txtHosId2.Text;
            string hosNm = txtHosNm.Text;
            string hosAddr = txtHosAddr.Text;
            string hosCEO = txtHosCEO.Text;
            string hosCEORID = txtHOSCEORID.Text;
            string workNM = txtWorkNM.Text;
            string workRID = txtWorkRID.Text;
            string demNmSanje = txtDemNmSanje.Text;
            string redem = cboREDEM.Text;
            string prtVer = cboPrtVer.SelectedIndex.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                using (OleDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // DB 서버 날짜
                        string sysdate = "";

                        string sql = "";
                        sql += Environment.NewLine + "SELECT CONVERT(VARCHAR,GETDATE(),112) AS HDATE";

                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            sysdate = reader["HDATE"].ToString();
                            return MetroLib.SqlHelper.BREAK;
                        });

                        // 병원 일반사항 저장
                        SaveTA88_HOSPITAL("1", "FLD1QTY", hosNm, multi, conn, tran);          // 의료기관명칭
                        SaveTA88_HOSPITAL("2", "FLD1QTY", hosId, multi, conn, tran);          // 의료기관코드
                        SaveTA88_HOSPITAL("2", "FLD2QTY", hosId2, multi, conn, tran);         // 의료기관코드(산재)
                        SaveTA88_HOSPITAL("3", "FLD1QTY", hosAddr, multi, conn, tran);        // 의료기관소재지
                        SaveTA88_HOSPITAL("5", "FLD1QTY", hosCEO, multi, conn, tran);         // 대표자명
                        SaveTA88_HOSPITAL("5", "FLD2QTY", hosCEORID, multi, conn, tran);      // 대표자주민번호
                        SaveTA88_HOSPITAL("48", "FLD1QTY", workNM, multi, conn, tran);        // 작성자명
                        SaveTA88_HOSPITAL("48", "FLD2QTY", workRID, multi, conn, tran);       // 작성자주민번호
                        SaveTA88_HOSPITAL("48", "FLD3QTY", demNmSanje, multi, conn, tran);    // 산재 청구인
                        SaveTA88_HOSPITAL("101", "FLD2QTY", redem, multi, conn, tran);        // 대행청구기관

                        // 서면 명세서 출력 버전
                        SaveTI88_HOSPITAL("7", "FLD2QTY", prtVer, "", conn, tran);

                        // 병원 일반사항 이력 갱신
                        SaveTA88A_HOSPITAL("2", sysdate, "FLD1QTY", hosId, multi, conn, tran);       // 의료기관코드
                        SaveTA88A_HOSPITAL("2", sysdate, "FLD2QTY", hosId2, multi, conn, tran);      // 의료기관코드(산재)
                        SaveTA88A_HOSPITAL("5", sysdate, "FLD1QTY", hosCEO, multi, conn, tran);      // 대표자명
                        SaveTA88A_HOSPITAL("5", sysdate, "FLD2QTY", hosCEORID, multi, conn, tran);   // 대표자주민번호
                        SaveTA88A_HOSPITAL("48", sysdate, "FLD1QTY", workNM, multi, conn, tran);     // 작성자명
                        SaveTA88A_HOSPITAL("48", sysdate, "FLD2QTY", workRID, multi, conn, tran);    // 작성자주민번호
                        SaveTA88A_HOSPITAL("48", sysdate, "FLD3QTY", demNmSanje, multi, conn, tran); // 산재 청구인

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
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

        private void ADD9902E_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Keys keys = Keys.Control | Keys.Shift | Keys.Alt;

            if ((Control.ModifierKeys & keys) != keys) return;

            using (ADD9902E_B form = new ADD9902E_B())
            {
                form.ShowDialog(this);
            }
        }
    }
}

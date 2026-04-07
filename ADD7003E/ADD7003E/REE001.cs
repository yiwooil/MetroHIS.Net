using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7003E
{
    public partial class REE001 : Form
    {
        private string m_ykiho; // 요양기관기호
        private string m_dmd_no; // 청구번호
        private string m_rcv_no; // 접수번호
        private string m_rcv_yr; // 접수년도
        private string m_bill_sno; // 청구서일련번호
        private string m_sp_sno; // 명세서 일련번호
        private string m_insup_tp_cd; // 보험자구분코드
        private string m_req_data_no; // 요청번호

        private string m_pid;
        private string m_bededt;
        private string m_frdt;
        private string m_todt;
        private string m_pnm;
        private string m_resid;

        private REE001_Info m_Info = new REE001_Info();

        public REE001()
        {
            InitializeComponent();
        }

        public void DoQuery(bool p_IsTest, string p_ykiho, string p_dmd_no, string p_rcv_no, string p_rcv_yr, string p_bill_sno, string p_sp_sno, string p_insup_tp_cd, string p_req_data_no, string p_pid, string p_bededt, string p_frdt, string p_todt, string p_pnm, string p_resid)
        {
            m_ykiho = p_ykiho;
            m_dmd_no = p_dmd_no;
            m_rcv_no = p_rcv_no; // 접수번호
            m_rcv_yr = p_rcv_yr; // 접수년도
            m_bill_sno = p_bill_sno; // 청구서일련번호
            m_sp_sno = p_sp_sno; // 명세서 일련번호
            m_insup_tp_cd = p_insup_tp_cd; // 보험자구분코드
            m_req_data_no = p_req_data_no;

            m_pid = p_pid;
            m_bededt = p_bededt;
            m_frdt = p_frdt;
            m_todt = p_todt;
            m_pnm = p_pnm;
            m_resid = p_resid;

            InitInfo();

            if (p_IsTest) btnSubmit.Enabled = false;

            btnQuery.PerformClick();

        }

        private void InitInfo()
        {
            List<CInfoTable> list = new List<CInfoTable>();
            grdInfo.DataSource = list;

            list.Add(new CInfoTable("응급실 도착일시",""));
            list.Add(new CInfoTable("응급실 퇴실일시",""));
            list.Add(new CInfoTable("KTAS 분류일시",""));
            list.Add(new CInfoTable("KTAS 등급",""));
            list.Add(new CInfoTable("전원여부",""));
            list.Add(new CInfoTable("타병원 진료내용",""));
            list.Add(new CInfoTable("주호소",""));
            list.Add(new CInfoTable("현병력",""));
            list.Add(new CInfoTable("약물 이상반응 여부",""));
            list.Add(new CInfoTable("약물이상반응 내용",""));
            list.Add(new CInfoTable("과거력",""));
            list.Add(new CInfoTable("약물복용 여부",""));
            list.Add(new CInfoTable("약물종류",""));
            list.Add(new CInfoTable("약물종류상세",""));
            list.Add(new CInfoTable("음주여부",""));
            list.Add(new CInfoTable("음주내용",""));
            list.Add(new CInfoTable("흡연여부",""));
            list.Add(new CInfoTable("흡연내용",""));
            list.Add(new CInfoTable("가족력여부",""));
            list.Add(new CInfoTable("가족력내용",""));
            list.Add(new CInfoTable("계통문진",""));
            list.Add(new CInfoTable("신체검진",""));
            list.Add(new CInfoTable("퇴실형태",""));
            list.Add(new CInfoTable("퇴실형태상세", ""));

            RefreshGridInfo();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
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
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                SetInfo(conn);
                SetDiag(conn);
                SetSopr(conn);
                SetSdr(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.PTMIINDT, A.PTMIINTM, A.PTMIOTDT, A.PTMIOTTM, A.PTMIKTDT, A.PTMIKTTM, A.PTMIKTS1, A.PTMIINCD, A.PTMIEMRT ";
            sql += Environment.NewLine + "  FROM EDIS.DBO.EMIHPTMI A ";
            sql += Environment.NewLine + " WHERE A.PTMIIDNO = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.PTMIINDT >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.PTMIINDT <= '" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY A.PTMIINDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.PTMIINDT = reader["PTMIINDT"].ToString(); // 응급실 도착일자
                m_Info.PTMIINTM = reader["PTMIINTM"].ToString(); //        도착시간
                m_Info.PTMIOTDT = reader["PTMIOTDT"].ToString(); // 응급실 퇴실일자
                m_Info.PTMIOTTM = reader["PTMIOTTM"].ToString(); //        퇴실시간
                m_Info.PTMIKTDT = reader["PTMIKTDT"].ToString(); // 응급실 KTAS 분류일자
                m_Info.PTMIKTTM = reader["PTMIKTTM"].ToString(); //        KTAS 분류시간
                m_Info.PTMIKTS1 = reader["PTMIKTS1"].ToString(); //        KTAS 등급
                m_Info.PTMIINCD = reader["PTMIINCD"].ToString(); //        전원기관코드
                m_Info.PTMIEMRT = reader["PTMIEMRT"].ToString(); // 응급시 퇴실형태

                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT A.C_CASE, A.RMK1 ";
            sql += Environment.NewLine + "  FROM TE12C A ";
            sql += Environment.NewLine + " WHERE A.PID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.BEDEDT = '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND A.BDIV = '3'";
            sql += Environment.NewLine + " ORDER BY A.EXDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string c_case = reader["C_CASE"].ToString();
                string rmk1 = reader["RMK1"].ToString();
                if (c_case == "CC")
                {
                    m_Info.E12C_CC = rmk1;  // 주호소
                }
                else if (c_case == "PI")
                {
                    m_Info.E12C_PT = rmk1;  // 현병력
                }
                else if (c_case == "PHX")
                {
                    m_Info.E12C_PHX = rmk1; // 과거력
                }
                else if (c_case == "FHX")
                {
                    m_Info.E12C_FHX = rmk1; // 가족력
                }
                else if (c_case == "ROS")
                {
                    m_Info.E12C_ROS = rmk1; // 계통문진
                }
                else if (c_case == "PE")
                {
                    m_Info.E12C_PE = rmk1;  // 신체검사
                }

                return true;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.EMRRM_IPAT_DT);
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.EMRRM_DSCG_DT);
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.KTAS_GRD_DIV_DT);
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.KTAS_GRD_TXT);
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.DHI_YN_NM);
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.OIST_TRET_TXT);
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.CC_TXT);
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.CUR_HOC_TXT);
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.ALRG_YN_NM);
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.ALRG_TXT);
            grdInfoView.SetRowCellValue(10, gcCONTENT, m_Info.ANMN_TXT);
            grdInfoView.SetRowCellValue(11, gcCONTENT, m_Info.MDS_DOS_YN_NM);
            grdInfoView.SetRowCellValue(12, gcCONTENT, m_Info.MDS_KND_CD);
            grdInfoView.SetRowCellValue(13, gcCONTENT, m_Info.MDS_ETC_TXT);
            grdInfoView.SetRowCellValue(14, gcCONTENT, m_Info.DRNK_YN_NM);
            grdInfoView.SetRowCellValue(15, gcCONTENT, m_Info.DRNK_TXT);
            grdInfoView.SetRowCellValue(16, gcCONTENT, m_Info.SMKN_YN_NM);
            grdInfoView.SetRowCellValue(17, gcCONTENT, m_Info.SMKN_TXT);
            grdInfoView.SetRowCellValue(18, gcCONTENT, m_Info.FMHS_YN_NM);
            grdInfoView.SetRowCellValue(19, gcCONTENT, m_Info.FMHS_TXT);
            grdInfoView.SetRowCellValue(20, gcCONTENT, m_Info.ROS_TXT);
            grdInfoView.SetRowCellValue(21, gcCONTENT, m_Info.PHBD_MEDEXM_TXT);
            grdInfoView.SetRowCellValue(22, gcCONTENT, m_Info.EMY_DSCG_FRM_CD_NM);
            grdInfoView.SetRowCellValue(23, gcCONTENT, m_Info.DSCG_FRM_TXT);

            RefreshGridInfo();
        }

        public void SetDiag(OleDbConnection p_conn)
        {
            List<REE001_Diag> list = new List<REE001_Diag>();
            grdDiag.DataSource = null;
            grdDiag.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 진단
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT S06.ROFG, S06.DXD, S06.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TS06 S06 INNER JOIN TA16 A16 ON A16.DISECD=S06.DACD AND A16.DISEDIV=S06.DISEDIV";
            sql += Environment.NewLine + " WHERE S06.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND S06.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND S06.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND S06.DPTCD='ER'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                REE001_Diag data = new REE001_Diag();
                data.ROFG = reader["ROFG"].ToString();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGridDiag();
        }

        private void SetSopr(OleDbConnection p_conn)
        {
            List<REE001_Sopr> list = new List<REE001_Sopr>();
            grdSopr.DataSource = null;
            grdSopr.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V01.ODT,V01.OTM,V01A.OCD,A18.ONM,A18.PRICD";
            sql += Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
            sql += Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql += Environment.NewLine + " WHERE V01.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V01.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND V01.ODT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND V01.ODT<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND V01.BDIV='3'";
            sql += Environment.NewLine + "   AND V01A.OCD LIKE 'T%'";
            sql += Environment.NewLine + "   AND V01.DCFG = '0'";
            sql += Environment.NewLine + "   AND V01.ODIVCD NOT LIKE 'H%'";
            sql += Environment.NewLine + "   AND V01.DCFG IN ('0')";
            sql += Environment.NewLine + " ORDER BY V01.ODT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                REE001_Sopr data = new REE001_Sopr();
                data.ODT = reader["ODT"].ToString();
                data.OTM = reader["OTM"].ToString();
                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGridSopr();
        }

        private void SetSdr(OleDbConnection p_conn)
        {
            List<REE001_Sdr> list = new List<REE001_Sdr>();
            grdSdr.DataSource = null;
            grdSdr.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.EXDT, A.USERID, A.SYSDT, A.SYSTM, A.C_CASE, A.RMK1 ";
            sql += Environment.NewLine + "     , A07.DRNM USERNM, A07.GDRLCID, A07.DPTCD";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TE12C A INNER JOIN TA07 A07 ON A07.DRID=A.USERID";
            sql += Environment.NewLine + "               INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += Environment.NewLine + " WHERE A.PID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.BEDEDT = '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND A.BDIV = '3'";
            sql += Environment.NewLine + "   AND A.C_CASE IN ('A','P')";
            sql += Environment.NewLine + " ORDER BY A.EXDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string exdt = reader["EXDT"].ToString();
                string userid = reader["USERID"].ToString();
                string usernm = reader["USERNM"].ToString();
                string gdrlcid = reader["GDRLCID"].ToString();
                string sysdt = reader["SYSDT"].ToString();
                string systm = reader["SYSTM"].ToString();
                string dptcd = reader["DPTCD"].ToString();
                string insdptcd = reader["INSDPTCD"].ToString();
                string insdptcd2 = reader["INSDPTCD2"].ToString();
                string c_case = reader["C_CASE"].ToString();
                string rmk1 = reader["RMK1"].ToString();

                bool bFind = false;
                foreach (REE001_Sdr data in list)
                {
                    if (exdt == data.EXDT &&
                        userid == data.USERID &&
                        usernm == data.USERNM &&
                        gdrlcid == data.GDRLCID &&
                        sysdt == data.SYSDT &&
                        systm == data.SYSTM &&
                        dptcd == data.DPTCD &&
                        insdptcd == data.INSDPTCD &&
                        insdptcd2 == data.INSDPTCD2)
                    {
                        if (c_case == "A")
                        {
                            data.E12C_A = rmk1;
                        }
                        if (c_case == "P")
                        {
                            data.E12C_P = rmk1;
                        }
                        bFind = true;
                        break;
                    }
                }
                if (bFind == false)
                {

                    REE001_Sdr data = new REE001_Sdr();
                    data.EXDT = exdt;
                    data.USERID = userid;
                    data.USERNM = usernm;
                    data.GDRLCID = gdrlcid;
                    data.SYSDT = sysdt;
                    data.SYSTM = systm;
                    data.DPTCD = dptcd;
                    data.INSDPTCD = insdptcd;
                    data.INSDPTCD2 = insdptcd2;
                    if (c_case == "A")
                    {
                        data.E12C_A = rmk1;
                    }
                    if (c_case == "P")
                    {
                        data.E12C_P = rmk1;
                    }
                    list.Add(data);
                }
                return true;
            });

            RefreshGridSdr();
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

        private void RefreshGridInfo()
        {
            if (grdInfo.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdInfo.BeginInvoke(new Action(() => grdInfoView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdInfoView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridDiag()
        {
            if (grdDiag.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDiag.BeginInvoke(new Action(() => grdDiagView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDiagView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridSopr()
        {
            if (grdSopr.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSopr.BeginInvoke(new Action(() => grdSoprView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSoprView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridSdr()
        {
            if (grdSdr.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSdr.BeginInvoke(new Action(() => grdSdrView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSdrView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnChcek_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Submit(false);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Submit(true);
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

        private void Submit(bool p_isSending)
        {
            HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();

            // 메타정보
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "REE001");
            doc.Metadata.Add("FOM_VER", "002");
            doc.Metadata.Add("YKIHO", m_ykiho);
            doc.Metadata.Add("DMD_NO", m_dmd_no); // 청구번호

            doc.Metadata.Add("RCV_NO", m_rcv_no); // 접수번호
            doc.Metadata.Add("RCV_YR", m_rcv_yr); // 접수년도
            doc.Metadata.Add("BILL_SNO", m_bill_sno); // 청구서일련번호
            doc.Metadata.Add("SP_SNO", m_sp_sno.PadLeft(5, '0')); // 명세서 일련번호
            doc.Metadata.Add("INSUP_TP_CD", m_insup_tp_cd); // 보험자구분코드
            doc.Metadata.Add("FOM_REF_BIZ_TP_CD", "06"); // 업무구분코드
            doc.Metadata.Add("DTL_BIZ_CD", "NDM"); // 업무상세코드
            doc.Metadata.Add("REQ_DATA_NO", m_req_data_no); // 요청번호

            doc.Metadata.Add("HOSP_RNO", m_pid);
            doc.Metadata.Add("PAT_NM", m_pnm);
            doc.Metadata.Add("PAT_JNO", m_resid);

            // A.응급실 기본정보
            doc.Elements.Add("EMRRM_IPAT_DT", m_Info.EMRRM_IPAT_DT); // 응급실 도착일시
            doc.Elements.Add("EMRRM_DSCG_DT", m_Info.EMRRM_IPAT_DT); // 응급실 퇴실일시

            // KTAS
            doc.Tables.AddColumn("TBL_KTAS_GRD", "KTAS_GRD_DIV_DT"); // KTAS 분류일시
            doc.Tables.AddColumn("TBL_KTAS_GRD", "KTAS_GRD_TXT"); // KTAS 등급

            doc.Tables.AddRow("TBL_KTAS_GRD");
            doc.Tables["TBL_KTAS_GRD"].Rows[0]["KTAS_GRD_DIV_DT"].Value = m_Info.KTAS_GRD_DIV_DT;
            doc.Tables["TBL_KTAS_GRD"].Rows[0]["KTAS_GRD_TXT"].Value = m_Info.KTAS_GRD_TXT;

            doc.Elements.Add("DHI_YN", m_Info.DHI_YN); // 전원여부
            doc.Elements.Add("OIST_TRET_TXT", m_Info.OIST_TRET_TXT); // 타병원 진료내용


            // B.기초 사전 정보

            // C.응급실 경과

            // 주호소
            doc.Tables.AddColumn("TBL_CC", "CC_TXT"); // 주호소

            doc.Tables.AddRow("TBL_CC");
            doc.Tables["TBL_CC"].Rows[0]["CC_TXT"].Value = m_Info.CC_TXT;

            doc.Elements.Add("CUR_HOC_TXT", m_Info.CUR_HOC_TXT); // 현병력
            doc.Elements.Add("ALRG_YN", m_Info.ALRG_YN); // 약물 이상반응 여부
            doc.Elements.Add("ALRG_TXT", m_Info.ALRG_TXT); // 약물 이상반응 내용
            doc.Elements.Add("ANMN_TXT", m_Info.ANMN_TXT); // 과거력
            doc.Elements.Add("MDS_DOS_YN", m_Info.MDS_DOS_YN); // 약물 복용여부
            doc.Elements.Add("MDS_KND_CD", m_Info.MDS_KND_CD); // 약물 종류
            doc.Elements.Add("MDS_ETC_TXT", m_Info.MDS_ETC_TXT); // 약물 종류 상세
            doc.Elements.Add("DRNK_YN", m_Info.DRNK_YN); // 음주여부
            doc.Elements.Add("DRNK_TXT", m_Info.DRNK_TXT); // 음주내용
            doc.Elements.Add("SMKN_YN", m_Info.SMKN_YN); // 흡연여부
            doc.Elements.Add("SMKN_TXT", m_Info.SMKN_TXT); // 흡연내용
            doc.Elements.Add("FMHS_YN", m_Info.FMHS_YN); // 가족력
            doc.Elements.Add("FMHS_TXT", m_Info.FMHS_TXT); // 가족력 내용
            doc.Elements.Add("ROS_TXT", m_Info.ROS_TXT); // 계통문진
            doc.Elements.Add("PHBD_MEDEXM_TXT", m_Info.PHBD_MEDEXM_TXT); // 신계검진

            // 12. 진료내역
            doc.Tables.AddColumn("TBL_SDR_DIAG", "SDR_DIAG_DT"); // 진료일시
            doc.Tables.AddColumn("TBL_SDR_DIAG", "SDR_DGSBJT_CD"); // 진료과
            doc.Tables.AddColumn("TBL_SDR_DIAG", "IFLD_DTL_SPC_SBJT_CD"); // 내과세부
            doc.Tables.AddColumn("TBL_SDR_DIAG", "SDR_NM"); // 진료의명
            doc.Tables.AddColumn("TBL_SDR_DIAG", "SDR_LCS_NO"); // 진료의 면허번호
            doc.Tables.AddColumn("TBL_SDR_DIAG", "WRTP_NM"); // 작성자 성명
            doc.Tables.AddColumn("TBL_SDR_DIAG", "WRT_DT"); // 작성일시
            doc.Tables.AddColumn("TBL_SDR_DIAG", "PRBM_LIST_TXT"); // 문제목록 및 평가
            doc.Tables.AddColumn("TBL_SDR_DIAG", "TRET_PLAN_TXT"); // 치료계획

            for (int row = 0; row < grdSdrView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SDR_DIAG");
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["SDR_DIAG_DT"].Value = grdSdrView.GetRowCellValue(row, gcSDR_DIAG_DT).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["SDR_DGSBJT_CD"].Value = grdSdrView.GetRowCellValue(row, gcSDR_DGSBJT_CD).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdSdrView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["SDR_NM"].Value = grdSdrView.GetRowCellValue(row, gcSDR_NM).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["SDR_LCS_NO"].Value = grdSdrView.GetRowCellValue(row, gcSDR_LCS_NO).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["WRTP_NM"].Value = grdSdrView.GetRowCellValue(row, gcWRTP_NM).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["WRT_DT"].Value = grdSdrView.GetRowCellValue(row, gcWRT_DT).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["PRBM_LIST_TXT"].Value = grdSdrView.GetRowCellValue(row, gcPRBM_LIST_TXT).ToString();
                doc.Tables["TBL_SDR_DIAG"].Rows[row]["TRET_PLAN_TXT"].Value = grdSdrView.GetRowCellValue(row, gcTRET_PLAN_TXT).ToString();
            }

            // 13. 진단
            doc.Tables.AddColumn("TBL_DIAG", "FDEC_DIAG_YN"); // 확진여부
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_SICK_SYM"); // 상병분류기호

            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DIAG");
                doc.Tables["TBL_DIAG"].Rows[row]["FDEC_DIAG_YN"].Value = grdDiagView.GetRowCellValue(row, gcFDEC_DIAG_YN).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_SICK_SYM).ToString().Replace(".","");
            }

            // 14. 시술.처치 및 수술
            doc.Tables.AddColumn("TBL_MOPR_SOPR", "SOPR_ENFC_DT"); // 시행일시
            doc.Tables.AddColumn("TBL_MOPR_SOPR", "SOPR_NM"); // 처치명

            for (int row = 0; row < grdSoprView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_MOPR_SOPR");
                doc.Tables["TBL_MOPR_SOPR"].Rows[row]["SOPR_ENFC_DT"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_ENFC_DT).ToString();
                doc.Tables["TBL_MOPR_SOPR"].Rows[row]["SOPR_NM"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_NM).ToString();
            }



            doc.Elements.Add("EMY_DSCG_FRM_CD", m_Info.EMY_DSCG_FRM_CD); // 퇴실형태
            doc.Elements.Add("DSCG_FRM_TXT", m_Info.DSCG_FRM_TXT); // 퇴실형태 상세
            

            // D.기타정보

            // E.추가정보

            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("REE001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

        private void grdInfoView_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (view == null) return;

            //if (e.RowHandle > 0)
            //{
            //    string item = grdInfoView.GetRowCellValue(e.RowHandle, gcITEM).ToString();
            //    string content = grdInfoView.GetRowCellValue(e.RowHandle, gcCONTENT).ToString();
            //    if (item == "현병력")
            //    {
            //        e.RowHeight = 50;
            //    }
            //}
        }

    }
}

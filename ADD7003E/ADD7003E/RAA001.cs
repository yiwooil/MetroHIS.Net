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
    public partial class RAA001 : Form
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

        private RAA001_Info m_Info = new RAA001_Info();

        public RAA001()
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

            list.Add(new CInfoTable("마취 시작일시", ""));
            list.Add(new CInfoTable("마취 종료일시", ""));
            list.Add(new CInfoTable("수술 시작일시", ""));
            list.Add(new CInfoTable("수술 종료일시", ""));
            list.Add(new CInfoTable("마취의사", ""));
            list.Add(new CInfoTable("작성자 성명", ""));
            list.Add(new CInfoTable("작성일시", ""));
            list.Add(new CInfoTable("마취형태", ""));
            list.Add(new CInfoTable("ASA 점수", ""));
            list.Add(new CInfoTable("마취방법", ""));
            list.Add(new CInfoTable("마취방법 상세", ""));
            list.Add(new CInfoTable("마취중 감시여부", ""));
            list.Add(new CInfoTable("마취중 감시종류", ""));
            list.Add(new CInfoTable("마취중 감시상세", ""));
            list.Add(new CInfoTable("Intake 총량", ""));
            list.Add(new CInfoTable("Intake 수액", ""));
            list.Add(new CInfoTable("Intake 수혈", ""));
            list.Add(new CInfoTable("Output 총량", ""));
            list.Add(new CInfoTable("Output 배뇨", ""));
            list.Add(new CInfoTable("Output 실혈", ""));
            list.Add(new CInfoTable("Output 기타", ""));

            RefreshGrid(grdInfo, grdInfoView);
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
                SetSopr(conn);
                SetDiag(conn);
                SetVtsg(conn);
                SetMdct(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U03.ANSDT, U03.ANSHR, U03.ANSMN, U03.ANEDT, U03.ANEHR, U03.ANEMN, U03.ANEDR, U03.USRID, U03.ENTDT, U03.ENTMS, U03.ANETP";
            sql += Environment.NewLine + "     , A07.DRNM";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "     , A31.CDNM";
            sql += Environment.NewLine + "     , U01.OPSDT, U01.OPSHR, U01.OPSMN, U01.OPEDT, U01.OPEHR, U01.OPEMN";
            sql += Environment.NewLine + "  FROM TU03 U03 INNER JOIN TA07 A07 ON A07.DRID=U03.ANEDR";
            sql += Environment.NewLine + "                INNER JOIN VA13 A13 ON A13.EMPID=U03.USRID";
            sql += Environment.NewLine + "                INNER JOIN TA31 A31 ON A31.MST1CD='58' AND A31.MST2CD=U03.ANETP";
            sql += Environment.NewLine + "                INNER JOIN TU01 U01 ON U01.PID=U03.PID AND U01.OPDT=U03.OPDT AND U01.DPTCD=U03.DPTCD AND U01.OPSEQ=U03.OPSEQ";
            sql += Environment.NewLine + " WHERE U03.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U03.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += Environment.NewLine + " ORDER BY U03.ANSDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.ANSDT = reader["ANSDT"].ToString(); // 마취 시작일자
                m_Info.ANSHR = reader["ANSHR"].ToString(); // 마취 시작시간
                m_Info.ANSMN = reader["ANSMN"].ToString(); // 마취 시작분
                m_Info.ANEDT = reader["ANEDT"].ToString(); // 마취 종료일자
                m_Info.ANEHR = reader["ANEHR"].ToString(); // 마취 종료시간
                m_Info.ANEMN = reader["ANEMN"].ToString(); // 마취 종료분
                m_Info.ANEDR = reader["ANEDR"].ToString(); // 퇴실 결정 의사
                m_Info.ANENM = reader["DRNM"].ToString();
                m_Info.USRID = reader["USRID"].ToString(); // 등록자ID
                m_Info.USRNM = reader["EMPNM"].ToString();
                m_Info.ENTDT = reader["ENTDT"].ToString(); // 등록일자
                m_Info.ENTMS = reader["ENTMS"].ToString(); // 등록시분
                m_Info.ANETP = reader["ANETP"].ToString();
                m_Info.ANETPNM = reader["CDNM"].ToString();

                m_Info.OPSDT = reader["OPSDT"].ToString(); // 수술시작일자
                m_Info.OPSHR = reader["OPSHR"].ToString(); // 수술시작시간
                m_Info.OPSMN = reader["OPSMN"].ToString(); // 수술시작분
                m_Info.OPEDT = reader["OPEDT"].ToString(); // 수술종료일자
                m_Info.OPEHR = reader["OPEHR"].ToString(); // 수술종료시간
                m_Info.OPEMN = reader["OPEMN"].ToString(); // 수술종료분


                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT SUM(PATE_V) PATE_V, SUM(BLOOD_V) BLOOD_V, SUM(URINE) URINE, SUM(S_V_O_V) S_V_O_V";
            sql += Environment.NewLine + "  FROM TU57_2";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND CHKDT>='" + m_Info.ANSDT + "'"; // 마취시작일자
            sql += Environment.NewLine + "   AND CHKDT<='" + m_Info.ANEDT + "'"; // 마취종료일자

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.PATE_V = reader["PATE_V"].ToString(); // 수액
                m_Info.PATE_V = reader["BLOOD_V"].ToString(); // 혈액
                m_Info.URINE = reader["URINE"].ToString(); // 소변
                m_Info.S_V_O_V = reader["S_V_O_V"].ToString(); // 마취 시작분

                return false;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT BLOOD_C, SUM(BLOOD_V) BLOOD_V";
            sql += Environment.NewLine + "  FROM TU57_2";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND CHKDT>='" + m_Info.ANSDT + "'"; // 마취시작일자
            sql += Environment.NewLine + "   AND CHKDT<='" + m_Info.ANEDT + "'"; // 마취종료일자
            sql += Environment.NewLine + " GROUP BY BLOOD_C";

            m_Info.BLOOD_LIST = "";
            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                if (m_Info.BLOOD_LIST == "")
                {
                    m_Info.BLOOD_LIST = reader["BLOOD_C"].ToString() + "/" + reader["BLOOD_V"].ToString();
                }
                else
                {
                    m_Info.BLOOD_LIST += "/" + reader["BLOOD_C"].ToString() + "/" + reader["BLOOD_V"].ToString();
                }

                return true;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.NCT_STA_DT); // 마취 시작일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.NCT_END_DT); // 마취 종료일시
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.SOPR_STA_DT); // 수술시작일시
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.SOPR_END_DT); // 수술종료일시
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.NCT_SDR_NM); // 마취의사
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.WRTP_NM); // 작성자성명
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.WRT_DT); // 작성일시
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.NCT_FRM_CD_NM); // 마취형태
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.ASA_PNT_NM); // ASA점수
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.NCT_MTH_CD_NM); // 마취방법
            grdInfoView.SetRowCellValue(10, gcCONTENT, m_Info.NCT_MTH_ETC_TXT); // 마취방법상세
            grdInfoView.SetRowCellValue(11, gcCONTENT, m_Info.NCT_MIDD_MNTR_YN_NM); // 마취중감시여부
            grdInfoView.SetRowCellValue(12, gcCONTENT, m_Info.NCT_MNTR_KND_CD_NM); // 마취중 감시종류
            grdInfoView.SetRowCellValue(13, gcCONTENT, m_Info.MNTR_ETC_TXT); // 마취중 감사종류상세
            grdInfoView.SetRowCellValue(14, gcCONTENT, m_Info.IGSN_TOT_QTY); // Intake 총량
            grdInfoView.SetRowCellValue(15, gcCONTENT, m_Info.IGSN_IFSL_QTY); // Intake 수액
            grdInfoView.SetRowCellValue(16, gcCONTENT, m_Info.BLTS_NM); // intake 혈액
            grdInfoView.SetRowCellValue(17, gcCONTENT, m_Info.PROD_TOT_QTY); // Output 총액
            grdInfoView.SetRowCellValue(18, gcCONTENT, m_Info.PROD_URNN_QTY); // Output 배뇨
            grdInfoView.SetRowCellValue(19, gcCONTENT, m_Info.PROD_HMRHG_QTY); // Ooutput 실혈
            grdInfoView.SetRowCellValue(20, gcCONTENT, m_Info.PROD_ETC_QTY); // Output 기타

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetSopr(OleDbConnection p_conn)
        {
            List<RAA001_Sopr> list = new List<RAA001_Sopr>();
            grdSopr.DataSource = null;
            grdSopr.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U02.OCD, A18.ONM, A18.PRICD, A02.ISPCD";
            sql += Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + " WHERE U02.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U02.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY U02.OPDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAA001_Sopr data = new RAA001_Sopr();

                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                data.ISPCD = reader["ISPCD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdSopr, grdSoprView);
        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RAA001_Diag> list = new List<RAA001_Diag>();
            grdDiag.DataSource = null;
            grdDiag.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 진단
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T05.PTYSQ, T05.ROFG, T05.DXD, T05.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TT05 T05 INNER JOIN TA16 A16 ON A16.DISECD=T05.DACD AND A16.DISEDIV=T05.DISEDIV";
            sql += Environment.NewLine + " WHERE T05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T05.BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY T05.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAA001_Diag data = new RAA001_Diag();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGrid(grdDiag, grdDiagView);
        }

        private void SetVtsg(OleDbConnection p_conn)
        {
            List<RAA001_Vtsg> list = new List<RAA001_Vtsg>();
            grdVtsg.DataSource = null;
            grdVtsg.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U64.CHKDT, U64.CHKTM, U64.BP, U64.PR, U64.RR, U64.TMP, U64.RMK";
            sql += Environment.NewLine + "  FROM TU64 U64";
            sql += Environment.NewLine + " WHERE U64.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U64.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U64.TMP,'')<>''"; // 체온이 없으면 안된다.
            sql += Environment.NewLine + " ORDER BY U64.CHKDT, U64.CHKTM";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAA001_Vtsg data = new RAA001_Vtsg();

                data.CHKDT = reader["CHKDT"].ToString(); // 활력징후 측정일자
                data.CHKTM = reader["CHKTM"].ToString(); //          측정시분
                data.BP = reader["BP"].ToString(); // 혈압
                data.PR = reader["PR"].ToString(); // 맥박
                data.RR = reader["RR"].ToString(); // 호흡
                data.TMP = reader["TMP"].ToString(); // 체온
                data.RMK = reader["RMK"].ToString(); // 비고

                list.Add(data);

                return true;
            });

            RefreshGrid(grdVtsg, grdVtsgView);
        }

        private void SetMdct(OleDbConnection p_conn)
        {
            List<RAA001_Mdct> list = new List<RAA001_Mdct>();
            grdMdct.DataSource = null;
            grdMdct.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U04.ENTDT, U04.ENTMS, U04.OCD, U04.DQTY, U04.DUNIT";
            sql += Environment.NewLine + "     , A18.ONM, A18.PRICD";
            sql += Environment.NewLine + "  FROM TU04 U04 INNER JOIN TA18 A18 ON A18.OCD=U04.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U04.OCD AND X.CREDT<=U04.ENTDT)";
            sql += Environment.NewLine + " WHERE U04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U04.BDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + " ORDER BY U04.ENTDT, U04.ENTMS";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAA001_Mdct data = new RAA001_Mdct();

                data.ENTDT = reader["ENTDT"].ToString(); // 투약일자
                data.ENTMS = reader["ENTMS"].ToString(); // 투약시간
                data.ONM = reader["ONM"].ToString(); // 약품명
                data.DQTY = reader["DQTY"].ToString(); // 1회투약량
                data.DUNIT = reader["DUNIT"].ToString(); // 단위
                data.OCD = reader["OCD"].ToString(); // 처방코드
                data.PRICD = reader["PRICD"].ToString(); // 수가코드

                list.Add(data);

                return true;
            });

            RefreshGrid(grdMdct, grdMdctView);
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

        private void RefreshGrid(DevExpress.XtraGrid.GridControl grid, DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (grid.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grid.BeginInvoke(new Action(() => gridView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                gridView.RefreshData();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RAA001");
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

            // A.기본정보
            doc.Elements.Add("NCT_STA_DT", m_Info.NCT_STA_DT); // 마취 시작일시
            doc.Elements.Add("NCT_END_DT", m_Info.NCT_END_DT); // 마취 종료일시
            doc.Elements.Add("SOPR_STA_DT", m_Info.SOPR_STA_DT); // 수술 시작일시
            doc.Elements.Add("SOPR_END_DT", m_Info.SOPR_END_DT); // 수술 종료일시

            // 마취의
            doc.Tables.AddColumn("TBL_NCT_SDR", "NCT_SDR_NM"); // 마취의명
            doc.Tables.AddRow("TBL_NCT_SDR");
            doc.Tables["TBL_NCT_SDR"].Rows[0]["NCT_SDR_NM"].Value = m_Info.NCT_SDR_NM;

            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); // 작성자성면
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일시

            // B.마취전 정보

            // 1.수술
            doc.Tables.AddColumn("TBL_SOPR", "SOPR_NM"); // 수술명
            doc.Tables.AddColumn("TBL_SOPR", "MDFEE_CD"); // 수가코드(EDI코드, 없으면 '-'입력)
            for (int row = 0; row < grdSoprView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR");
                doc.Tables["TBL_SOPR"].Rows[row]["SOPR_NM"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_NM).ToString();
                doc.Tables["TBL_SOPR"].Rows[row]["MDFEE_CD"].Value = grdSoprView.GetRowCellValue(row, gcMDFEE_CD).ToString();
            }

            // 2.진단
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_DIAG", "SICK_SYM"); // 상병분류기호
            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DIAG");
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcSICK_SYM).ToString().Replace(".","");
            }

            doc.Elements.Add("NCT_FRM_CD", m_Info.NCT_FRM_CD); // 마취형태
            doc.Elements.Add("ASA_PNT", m_Info.ASA_PNT); // ASA점수

            // C.마취 중 정보

            doc.Elements.Add("NCT_MTH_CD", m_Info.NCT_MTH_CD); // 마취방법
            doc.Elements.Add("NCT_MTH_ETC_TXT", m_Info.NCT_MTH_ETC_TXT); // 마취방법상세
            doc.Elements.Add("NCT_MIDD_MNTR_YN", m_Info.NCT_MIDD_MNTR_YN); // 마취중감시여부
            doc.Elements.Add("NCT_MNTR_KND_CD", m_Info.NCT_MNTR_KND_CD); // 마취중감시종류
            doc.Elements.Add("MNTR_ETC_TXT", m_Info.MNTR_ETC_TXT); // 마취중감시싱세

            // 3.활력징후
            doc.Tables.AddColumn("TBL_VTSG", "MASR_DT"); // 측정일시
            doc.Tables.AddColumn("TBL_VTSG", "BPRSU"); // 혈압
            doc.Tables.AddColumn("TBL_VTSG", "PULS"); // 맥박
            doc.Tables.AddColumn("TBL_VTSG", "BRT"); // 호흡
            doc.Tables.AddColumn("TBL_VTSG", "TMPR"); // 체온
            for (int row = 0; row < grdVtsgView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_VTSG");
                doc.Tables["TBL_VTSG"].Rows[row]["MASR_DT"].Value = grdVtsgView.GetRowCellValue(row, gcMASR_DT).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["BPRSU"].Value = grdVtsgView.GetRowCellValue(row, gcBPRSU).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["PULS"].Value = grdVtsgView.GetRowCellValue(row, gcPULS).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["BRT"].Value = grdVtsgView.GetRowCellValue(row, gcBRT).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["TMPR"].Value = grdVtsgView.GetRowCellValue(row, gcTMPR).ToString();
            }

            // 4.마취중 감시측정
            doc.Tables.AddColumn("TBL_MIDD_MNTR", "MASR_DT"); // 측정일시
            doc.Tables.AddRow("TBL_MIDD_MNTR");
            doc.Tables["TBL_MIDD_MNTR"].Rows[0]["MASR_DT"].Value = "-";

            // 5.마취중 투약
            doc.Tables.AddColumn("TBL_SOPR_MIDD_MDCT", "NCT_KND_CD"); // 분류
            doc.Tables.AddColumn("TBL_SOPR_MIDD_MDCT", "MDCT_DT"); // 투약일시
            doc.Tables.AddColumn("TBL_SOPR_MIDD_MDCT", "MDS_NM"); // 약품명
            doc.Tables.AddColumn("TBL_SOPR_MIDD_MDCT", "FQ1_MDCT_QTY"); // 1회투약량
            doc.Tables.AddColumn("TBL_SOPR_MIDD_MDCT", "MDS_UNIT_TXT"); // 단위
            for (int row = 0; row < grdMdctView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_MIDD_MDCT");
                doc.Tables["TBL_SOPR_MIDD_MDCT"].Rows[row]["NCT_KND_CD"].Value = grdMdctView.GetRowCellValue(row, gcNCT_KND_CD).ToString();
                doc.Tables["TBL_SOPR_MIDD_MDCT"].Rows[row]["MDCT_DT"].Value = grdMdctView.GetRowCellValue(row, gcMDCT_DT).ToString();
                doc.Tables["TBL_SOPR_MIDD_MDCT"].Rows[row]["MDS_NM"].Value = grdMdctView.GetRowCellValue(row, gcMDS_NM).ToString();
                doc.Tables["TBL_SOPR_MIDD_MDCT"].Rows[row]["FQ1_MDCT_QTY"].Value = grdMdctView.GetRowCellValue(row, gcFQ1_MDCT_QTY).ToString();
                doc.Tables["TBL_SOPR_MIDD_MDCT"].Rows[row]["MDS_UNIT_TXT"].Value = grdMdctView.GetRowCellValue(row, gcMDS_UNIT_TXT).ToString();
            }

            // 6.intake, output
            doc.Elements.Add("IGSN_TOT_QTY", m_Info.IGSN_TOT_QTY); // INTAKE 총량
            doc.Elements.Add("IGSN_IFSL_QTY", m_Info.IGSN_IFSL_QTY); // INTAKE 수액

            // 수혈
            doc.Tables.AddColumn("TBL_BLTS", "BLTS_KND_TXT"); // 혈액종류
            doc.Tables.AddColumn("TBL_BLTS", "BLTS_QTY"); // 수혈량
            if (m_Info.BLTS_NM != "")
            {
                string[] blts = m_Info.BLTS_NM.Split('/');
                int row = 0;
                for (int idx = 0; idx < blts.Length; idx += 2)
                {
                    doc.Tables.AddRow("TBL_BLTS");
                    doc.Tables["TBL_BLTS"].Rows[row]["BLTS_KND_TXT"].Value = blts[idx];
                    doc.Tables["TBL_BLTS"].Rows[row]["BLTS_QTY"].Value = blts[idx + 1];
                    row++;
                }
            }
            doc.Elements.Add("PROD_TOT_QTY", m_Info.PROD_TOT_QTY); // OUPUT 총량
            doc.Elements.Add("PROD_URNN_QTY", m_Info.PROD_URNN_QTY); // OUPUT 배뇨
            doc.Elements.Add("PROD_HMRHG_QTY", m_Info.PROD_HMRHG_QTY); // OUPUT 실혈
            doc.Elements.Add("PROD_ETC_QTY", m_Info.PROD_ETC_QTY); // OUPUT 기타

            // 7.마취관련 기록
            doc.Tables.AddColumn("TBL_NCT_RCD", "OCUR_DT"); // 발생일시
            doc.Tables.AddColumn("TBL_NCT_RCD", "RCD_TXT"); // 내용
            doc.Tables.AddRow("TBL_NCT_RCD");
            doc.Tables["TBL_NCT_RCD"].Rows[0]["OCUR_DT"].Value = "-";
            doc.Tables["TBL_NCT_RCD"].Rows[0]["RCD_TXT"].Value = "-";

            // D.기타 정보

            // E.추가 정보


            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("RAA001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }
    }
}

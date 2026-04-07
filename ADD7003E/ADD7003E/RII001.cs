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
    public partial class RII001 : Form
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

        private RII001_Info m_Info = new RII001_Info();

        public RII001()
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

            list.Add(new CInfoTable("입원일시", ""));
            list.Add(new CInfoTable("진료과", ""));
            list.Add(new CInfoTable("내과세부", ""));
            list.Add(new CInfoTable("담당의사 성명", ""));
            list.Add(new CInfoTable("작성자 성명", ""));
            list.Add(new CInfoTable("작성일시", ""));
            list.Add(new CInfoTable("입원경로", ""));
            list.Add(new CInfoTable("입원경로상세", ""));
            list.Add(new CInfoTable("주호소", ""));
            list.Add(new CInfoTable("발병시기", ""));
            list.Add(new CInfoTable("현병력", ""));
            list.Add(new CInfoTable("약물이상반응 여부", ""));
            list.Add(new CInfoTable("약물이상반응 내용", ""));
            list.Add(new CInfoTable("과거력", ""));
            list.Add(new CInfoTable("약물복용 여부", ""));
            list.Add(new CInfoTable("약물종류", ""));
            list.Add(new CInfoTable("약물종류상세", ""));
            list.Add(new CInfoTable("음주여부", ""));
            list.Add(new CInfoTable("음주내용", ""));
            list.Add(new CInfoTable("흡연여부", ""));
            list.Add(new CInfoTable("흡연내용", ""));
            list.Add(new CInfoTable("가족력여부", ""));
            list.Add(new CInfoTable("가족력내용", ""));
            list.Add(new CInfoTable("계통문진", ""));
            list.Add(new CInfoTable("신체검진", ""));
            list.Add(new CInfoTable("문제목록및평가", ""));
            list.Add(new CInfoTable("치료계획", ""));

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

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.BEDEDT, A.DPTCD, A.DRID, A.EMPID, A.WDATE, A.WTIME, A.MJ_HOSO, A.ONSET, A.PI, A.PHX, A.ROS, A.PE, A.CUREPLAN ";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "     , A07.DRNM";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "     , A04.BEDEHM";
            sql += Environment.NewLine + "  FROM EMR290 A INNER JOIN TA09 A09 ON A09.DPTCD=A.DPTCD";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=A.DRID";
            sql += Environment.NewLine + "                INNER JOIN VA13 A13 ON A13.EMPID=A.EMPID";
            sql += Environment.NewLine + "                INNER JOIN TA04 A04 ON A04.PID=A.PID AND A04.BEDEDT=A.BEDEDT";
            sql += Environment.NewLine + " WHERE A.PID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.BEDEDT >= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.WDATE >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.WDATE <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY A.WDATE, A.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.BEDEDT = reader["BEDEDT"].ToString(); // 입원일자
                m_Info.BEDEHM = reader["BEDEHM"].ToString(); // 입원시간
                m_Info.DPTCD = reader["DPTCD"].ToString(); // 과코드
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                m_Info.DRID = reader["DRID"].ToString(); // 의사ID
                m_Info.DRNM = reader["DRNM"].ToString();
                m_Info.EMPID = reader["EMPID"].ToString(); // 작성자ID
                m_Info.EMPNM = reader["EMPNM"].ToString();
                m_Info.WDATE = reader["WDATE"].ToString(); // 작성일
                m_Info.WTIME = reader["WTIME"].ToString(); // 작성시간
                m_Info.MJ_HOSO = reader["MJ_HOSO"].ToString(); // 주호소
                m_Info.ONSET = reader["ONSET"].ToString(); // 발병시기
                m_Info.PI = reader["PI"].ToString(); // 현병력
                m_Info.PHX = reader["PHX"].ToString(); // 과거력
                m_Info.ROS = reader["ROS"].ToString(); // 계통문진
                m_Info.PE = reader["PE"].ToString(); // 신체검진
                m_Info.CUREPLAN = reader["CUREPLAN"].ToString(); // 치료계획

                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT CASE WHEN dbo.MFN_PIECE(InCondiQ1,CHAR(21),1)='1' THEN '0'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(InCondiQ1,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "			  WHEN dbo.MFN_PIECE(InCondiQ1,CHAR(21),3)='1' THEN '2'";
            sql += Environment.NewLine + "			  ELSE '0'";
            sql += Environment.NewLine + "	     END AS InCondiQ1_fg";
            sql += Environment.NewLine + "     , CASE WHEN InCondiQ7 IN ('1','2') THEN '1' ELSE '0' END AS InCondiQ7_fg";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(Society2_Q3,CHAR(21),3)='1' THEN '1' ELSE '0' END AS Society2_Q3_fg";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(Society2_Q3_ETC,CHAR(21),1) AS Society2_Q3_ETC_txt";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(InCondiQ6,CHAR(21),2)='1' THEN '1' ELSE '0' END AS InCondiQ6_fg";
            sql += Environment.NewLine + "	   , InCondiQ6_ETC AS InCondiQ6_ETC_txt";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(HABITQ3,CHAR(21),3)='1' THEN '1' ELSE '0' END AS HABITQ3_fg";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ3_ETC,CHAR(21),1) AS HABITQ3_ETC_qty";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ3_ETC,CHAR(21),2) AS HABITQ3_ETC_cnt";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ3_ETC,CHAR(21),3) AS HABITQ3_ETC_last";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(HABITQ4,CHAR(21),3)='1' THEN '1' ELSE '0' END AS HABITQ4_fg";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ4_ETC,CHAR(21),1) AS HABITQ4_ETC_qty";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ4_ETC,CHAR(21),2) AS HABITQ4_ETC_period";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(HABITQ4_ETC,CHAR(21),3) AS HABITQ4_ETC_stop";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),1)='1' THEN '0' ELSE '1' END AS FAQ1_fg";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),2)='1' THEN '고혈압'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),3)='1' THEN '당뇨'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),4)='1' THEN '결핵'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),5)='1' THEN '간질환'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),6)='1' THEN CASE WHEN FAQ1_ETC='' THEN '암' ELSE FAQ1_ETC END";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ1,CHAR(21),7)='1' THEN '신장질환'";
            sql += Environment.NewLine + "		      ELSE FAQ1_ETC";
            sql += Environment.NewLine + "	     END AS FAQ1_txt";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),1)='1' THEN '0' ELSE '1' END AS FAQ2_fg";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),2)='1' THEN '고혈압'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),3)='1' THEN '당뇨'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),4)='1' THEN '결핵'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),5)='1' THEN '간질환'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),6)='1' THEN CASE WHEN FAQ2_ETC='' THEN '암' ELSE FAQ2_ETC END";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ2,CHAR(21),7)='1' THEN '신장질환'";
            sql += Environment.NewLine + "		      ELSE FAQ2_ETC";
            sql += Environment.NewLine + "	     END AS FAQ2_txt";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),1)='1' THEN '0' ELSE '1' END AS FAQ3_fg";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),2)='1' THEN '고혈압'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),3)='1' THEN '당뇨'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),4)='1' THEN '결핵'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),5)='1' THEN '간질환'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),6)='1' THEN CASE WHEN FAQ3_ETC='' THEN '암' ELSE FAQ3_ETC END";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ3,CHAR(21),7)='1' THEN '신장질환'";
            sql += Environment.NewLine + "		      ELSE FAQ3_ETC";
            sql += Environment.NewLine + "	     END AS FAQ3_txt";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),1)='1' THEN '0' ELSE '1' END AS FAQ4_fg";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),2)='1' THEN '고혈압'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),3)='1' THEN '당뇨'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),4)='1' THEN '결핵'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),5)='1' THEN '간질환'";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),6)='1' THEN CASE WHEN FAQ4_ETC='' THEN '암' ELSE FAQ4_ETC END";
            sql += Environment.NewLine + "	          WHEN dbo.MFN_PIECE(FAQ4,CHAR(21),7)='1' THEN '신장질환'";
            sql += Environment.NewLine + "			  ELSE FAQ4_ETC";
            sql += Environment.NewLine + "	     END AS FAQ4_txt";
            sql += Environment.NewLine + "  FROM TV95_10 ";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "' ";
            sql += Environment.NewLine + "   AND BEDEDT >= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND WDATE >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND WDATE <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(CHNGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.InCondiQ1_fg = reader["InCondiQ1_fg"].ToString(); // 입원경로(0.외래 1.응급 2.기타)
                m_Info.InCondiQ7_fg = reader["InCondiQ7_fg"].ToString(); // 전원여부(1.Yes 0.No)
                m_Info.Society2_Q3_fg = reader["Society2_Q3_fg"].ToString(); // 약물이상 반영여부(1.Yes 0.No)
                m_Info.Society2_Q3_ETC_txt = reader["Society2_Q3_ETC_txt"].ToString(); // 약물이상 반응내용
                m_Info.InCondiQ6_fg = reader["InCondiQ6_fg"].ToString(); // 약물복용여부(1.Yes 0.No)
                m_Info.InCondiQ6_ETC_txt = reader["InCondiQ6_ETC_txt"].ToString(); // 약물종류상세
                m_Info.HABITQ3_fg = reader["HABITQ3_fg"].ToString(); // 응주여부(1.Yes 0.No)
                m_Info.HABITQ3_ETC_qty = reader["HABITQ3_ETC_qty"].ToString(); // 음주양
                m_Info.HABITQ3_ETC_cnt = reader["HABITQ3_ETC_cnt"].ToString(); // 음주횟수
                m_Info.HABITQ3_ETC_last = reader["HABITQ3_ETC_last"].ToString(); // 마지막음주
                m_Info.HABITQ4_fg = reader["HABITQ4_fg"].ToString(); // 흡연여부
                m_Info.HABITQ4_ETC_qty = reader["HABITQ4_ETC_qty"].ToString(); // 흡연양
                m_Info.HABITQ4_ETC_period = reader["HABITQ4_ETC_period"].ToString(); // 흡연기간
                m_Info.HABITQ4_ETC_stop = reader["HABITQ4_ETC_stop"].ToString(); // 금연시작일
                m_Info.FAQ1_fg = reader["FAQ1_fg"].ToString(); // 부 가족력여부
                m_Info.FAQ1_txt = reader["FAQ1_txt"].ToString(); // 부 가족력내용
                m_Info.FAQ2_fg = reader["FAQ2_fg"].ToString(); // 모 가족력여부
                m_Info.FAQ2_txt = reader["FAQ2_txt"].ToString(); // 모 가족력내용
                m_Info.FAQ3_fg = reader["FAQ3_fg"].ToString(); // 형제 가족력여부
                m_Info.FAQ3_txt = reader["FAQ3_txt"].ToString(); // 형제 가족력내용
                m_Info.FAQ4_fg = reader["FAQ4_fg"].ToString(); // 기타 가족력여부
                m_Info.FAQ4_txt = reader["FAQ4_txt"].ToString(); // 기타 가족력내용

                return false;
            });

            List<CInfoTable> list = new List<CInfoTable>();
            grdInfo.DataSource = null;
            grdInfo.DataSource = list;

            list.Add(new CInfoTable("입원일시", m_Info.IPAT_DT));
            list.Add(new CInfoTable("진료과", m_Info.IPAT_DGSBJT_CD));
            list.Add(new CInfoTable("내과세부", m_Info.IFLD_DTL_SPC_SBJT_CD));
            list.Add(new CInfoTable("담당의사 성명", m_Info.CHRG_DR_NM));
            list.Add(new CInfoTable("작성자 성명", m_Info.WRTP_NM));
            list.Add(new CInfoTable("작성일시", m_Info.WRT_DT));
            list.Add(new CInfoTable("입원경로", m_Info.VST_PTH_NM));
            list.Add(new CInfoTable("입원경로상세", m_Info.VST_PTH_ETC_TXT));
            string[] arr = m_Info.CC_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "주호소" : "", arr[idx]));
            }
            list.Add(new CInfoTable("발병시기", m_Info.OCUR_ERA_TXT));
            arr = m_Info.CUR_HOC_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "현병력" : "", arr[idx]));
            }
            list.Add(new CInfoTable("약물이상반응 여부", m_Info.ALRG_YN_NM));
            list.Add(new CInfoTable("약물이상반응 내용", m_Info.ALRG_TXT));
            arr = m_Info.ANMN_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "과거력" : "", arr[idx]));
            }
            list.Add(new CInfoTable("약물복용 여부", m_Info.MDS_DOS_YN_NM));
            list.Add(new CInfoTable("약물종류", m_Info.MDS_KND_CD));
            list.Add(new CInfoTable("약물종류상세", m_Info.MDS_ETC_TXT));
            list.Add(new CInfoTable("음주여부", m_Info.DRNK_YN_NM));
            list.Add(new CInfoTable("음주내용", m_Info.DRNK_TXT));
            list.Add(new CInfoTable("흡연여부", m_Info.SMKN_YN_NM));
            list.Add(new CInfoTable("흡연내용", m_Info.SMKN_TXT));
            list.Add(new CInfoTable("가족력여부", m_Info.FMHS_YN_NM));
            list.Add(new CInfoTable("가족력내용", m_Info.FMHS_TXT));
            arr = m_Info.ROS_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "계통문진" : "", arr[idx]));
            }
            arr = m_Info.PHBD_MEDEXM_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "신체검진" : "", arr[idx]));
            }
            arr = m_Info.PRBM_LIST_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "문제목록및평가" : "", arr[idx]));
            }
            arr = m_Info.TRET_PLAN_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "치료계획" : "", arr[idx]));
            }

            RefreshGridInfo();

        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RII001_Diag> list = new List<RII001_Diag>();
            grdDiag.DataSource = null;
            grdDiag.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 진단
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T05.ROFG, T05.DXD, T05.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TT05 T05 INNER JOIN TA16 A16 ON A16.DISECD=T05.DACD AND A16.DISEDIV=T05.DISEDIV";
            sql += Environment.NewLine + " WHERE T05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T05.BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY T05.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RII001_Diag data = new RII001_Diag();
                data.ROFG = reader["ROFG"].ToString();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();
                list.Add(data);

                return true;
            });

            if (list.Count < 1)
            {
                RII001_Diag data = new RII001_Diag();
                data.ROFG = "";
                data.DXD = "";
                data.DACD = "";
                data.ZCD10CD = "";
                list.Add(data);
            }

            RefreshGridDiag();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RII001");
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
            doc.Elements.Add("IPAT_DT", m_Info.IPAT_DT); // 입원일시
            doc.Elements.Add("IPAT_DGSBJT_CD", m_Info.IPAT_DGSBJT_CD); //진료과
            doc.Elements.Add("IFLD_DTL_SPC_SBJT_CD", m_Info.IFLD_DTL_SPC_SBJT_CD); //내과상세
            doc.Elements.Add("CHRG_DR_NM", m_Info.CHRG_DR_NM); //담당의사 성명
            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); //작성자 성명
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); //작성일시

            // B.입원정보
            doc.Elements.Add("VST_PTH_CD", m_Info.VST_PTH_CD); //입원경로
            doc.Elements.Add("VST_PTH_ETC_TXT", m_Info.VST_PTH_ETC_TXT); //입원경로상세

            // 주호소
            doc.Tables.AddColumn("TBL_CC", "CC_TXT"); // 주호소
            doc.Tables.AddColumn("TBL_CC", "OCUR_ERA_TXT"); // 발병시가

            doc.Tables.AddRow("TBL_CC");
            doc.Tables["TBL_CC"].Rows[0]["CC_TXT"].Value = m_Info.CC_TXT;
            doc.Tables["TBL_CC"].Rows[0]["OCUR_ERA_TXT"].Value = m_Info.OCUR_ERA_TXT;

            doc.Elements.Add("CUR_HOC_TXT", m_Info.CUR_HOC_TXT); // 현병력
            doc.Elements.Add("ALRG_YN", m_Info.ALRG_YN); // 약물이상반응 여부
            doc.Elements.Add("ALRG_TXT", m_Info.ALRG_TXT); // 약물이상반응 내용
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
            doc.Elements.Add("PRBM_LIST_TXT", m_Info.PRBM_LIST_TXT); // 문제목록및 평가

            // 13. 초기진단
            doc.Tables.AddColumn("TBL_EARLY_DIAG", "EARLY_FDEC_DIAG_YN"); // 확진여부
            doc.Tables.AddColumn("TBL_EARLY_DIAG", "EARLY_DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_EARLY_DIAG", "EARLY_DIAG_SICK_SYM"); // 상병분류기호

            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_EARLY_DIAG");
                doc.Tables["TBL_EARLY_DIAG"].Rows[row]["EARLY_FDEC_DIAG_YN"].Value = grdDiagView.GetRowCellValue(row, gcEARLY_FDEC_DIAG_YN).ToString();
                doc.Tables["TBL_EARLY_DIAG"].Rows[row]["EARLY_DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcEARLY_DIAG_NM).ToString();
                doc.Tables["TBL_EARLY_DIAG"].Rows[row]["EARLY_DIAG_SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcEARLY_DIAG_SICK_SYM).ToString().Replace(".", "");
            }

            doc.Elements.Add("TRET_PLAN_TXT", m_Info.TRET_PLAN_TXT); // 치료계획


            // C.기타정보

            // D.추가정보

            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("RII001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

        private void grdInfoView_MouseDown(object sender, MouseEventArgs e)
        {
            //var hitInfo = grdInfoView.CalcHitInfo(e.Location);
            //if (hitInfo.InRowCell)
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        int rowHandle = hitInfo.RowHandle;
            //        DevExpress.XtraGrid.Columns.GridColumn column = hitInfo.Column;
            //        string value = grdInfoView.GetRowCellValue(rowHandle, column).ToString();
            //        txtView.Text = value;
            //        //string[] value_arr = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //    }
            //}
        }

        private void grdDiagView_MouseDown(object sender, MouseEventArgs e)
        {
            //var hitInfo = grdDiagView.CalcHitInfo(e.Location);
            //if (hitInfo.InRowCell)
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        int rowHandle = hitInfo.RowHandle;
            //        DevExpress.XtraGrid.Columns.GridColumn column = hitInfo.Column;
            //        string value = grdDiagView.GetRowCellValue(rowHandle, column).ToString();
            //        txtView.Text = value;
            //        //string[] value_arr = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //    }
            //}
        }

    }
}

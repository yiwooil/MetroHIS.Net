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
    public partial class RNP001 : Form
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

        private RNP001_Info m_Info = new RNP001_Info();

        public RNP001()
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
            list.Add(new CInfoTable("작성자", ""));
            list.Add(new CInfoTable("작성일시", ""));
            list.Add(new CInfoTable("입원경로", ""));
            list.Add(new CInfoTable("입원경로상세", ""));
            list.Add(new CInfoTable("환자구분", ""));
            // 일반정보
            list.Add(new CInfoTable("입원동기", ""));
            list.Add(new CInfoTable("과거력", ""));
            list.Add(new CInfoTable("수술력", ""));
            list.Add(new CInfoTable("최근투약상태", ""));
            list.Add(new CInfoTable("알레르기 여부", ""));
            list.Add(new CInfoTable("알레르기 내용", ""));
            list.Add(new CInfoTable("가족력", ""));
            list.Add(new CInfoTable("음주여부", ""));
            list.Add(new CInfoTable("음주내용", ""));
            list.Add(new CInfoTable("흡연여부", ""));
            list.Add(new CInfoTable("흡연내용", ""));
            list.Add(new CInfoTable("신장", ""));
            list.Add(new CInfoTable("체중", ""));
            list.Add(new CInfoTable("신체검진", ""));
            // 신생아 정보
            list.Add(new CInfoTable("출생일시", ""));
            list.Add(new CInfoTable("재태기간", ""));
            list.Add(new CInfoTable("분만형태", ""));
            list.Add(new CInfoTable("Apgar Score", ""));
            list.Add(new CInfoTable("분만관련 특이사항", ""));
            list.Add(new CInfoTable("혈압", ""));
            list.Add(new CInfoTable("맥박", ""));
            list.Add(new CInfoTable("호흡", ""));
            list.Add(new CInfoTable("체온", ""));
            list.Add(new CInfoTable("산소포화도", ""));
            list.Add(new CInfoTable("출생시 체중", ""));
            list.Add(new CInfoTable("입실시 체중", ""));
            list.Add(new CInfoTable("신체검진", ""));

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

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A04.BEDEDT, A04.BEDEHM, A04.DPTCD";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TA04 A04 INNER JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += Environment.NewLine + " WHERE A04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND A04.BEDEDT='" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.BEDEDT = reader["BEDEDT"].ToString();
                m_Info.BEDEHM = reader["BEDEHM"].ToString();
                m_Info.DPTCD = reader["DPTCD"].ToString();
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString();

                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT V95_10.EMPID, V95_10.WDATE, V95_10.WTIME";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiQ1,CHAR(21),1) InCondiQ1_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiQ1,CHAR(21),2) InCondiQ1_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiQ1,CHAR(21),3) InCondiQ1_3";
            sql += Environment.NewLine + "     , V95_10.InCondiQ7";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.GUBUN,CHAR(21),3) GUBUN_3";
            sql += Environment.NewLine + "     , V95_10.Society1_ADOBJ, V95_10.Society2_INHIS, V95_10.InCondiQ6_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.Society2_Q3,CHAR(21),2) Society2_Q3_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.Society2_Q3_ETC,CHAR(21),1) Society2_Q3_ETC_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.Society2_Q3_ETC,CHAR(21),2) Society2_Q3_ETC_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),1) FAQ1_1"; // 부 가족력
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),2) FAQ1_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),3) FAQ1_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),4) FAQ1_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),5) FAQ1_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),6) FAQ1_6";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ1,CHAR(21),7) FAQ1_7";
            sql += Environment.NewLine + "     , V95_10.FAQ1_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),1) FAQ2_1"; // 모 가족력
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),2) FAQ2_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),3) FAQ2_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),4) FAQ2_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),5) FAQ2_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),6) FAQ2_6";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ2,CHAR(21),7) FAQ2_7";
            sql += Environment.NewLine + "     , V95_10.FAQ2_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),1) FAQ3_1"; // 형제 가족력
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),2) FAQ3_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),3) FAQ3_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),4) FAQ3_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),5) FAQ3_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),6) FAQ3_6";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ3,CHAR(21),7) FAQ3_7";
            sql += Environment.NewLine + "     , V95_10.FAQ3_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),1) FAQ4_1"; // 조무보/기타 가족력
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),2) FAQ4_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),3) FAQ4_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),4) FAQ4_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),5) FAQ4_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),6) FAQ4_6";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.FAQ4,CHAR(21),7) FAQ4_7";
            sql += Environment.NewLine + "     , V95_10.FAQ4_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ3,CHAR(21),3) HABITQ3_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ3_ETC,CHAR(21),1) HABITQ3_ETC_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ3_ETC,CHAR(21),2) HABITQ3_ETC_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ3_ETC,CHAR(21),3) HABITQ3_ETC_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ4,CHAR(21),3) HABITQ4_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ4_ETC,CHAR(21),1) HABITQ4_ETC_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ4_ETC,CHAR(21),2) HABITQ4_ETC_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.HABITQ4_ETC,CHAR(21),3) HABITQ4_ETC_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.NewBornQ13,CHAR(21),1) NewBornQ13_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.NewBornQ13,CHAR(21),2) NewBornQ13_2";
            sql += Environment.NewLine + "     , V95_10.NewBornQ13_ETC";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),1) InCondiTPR_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),2) InCondiTPR_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),3) InCondiTPR_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),4) InCondiTPR_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),5) InCondiTPR_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(V95_10.InCondiTPR,CHAR(21),8) InCondiTPR_8";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "  FROM TV95_10 V95_10 INNER JOIN VA13 A13 ON A13.EMPID=V95_10.EMPID";
            sql += Environment.NewLine + " WHERE V95_10.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V95_10.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(V95_10.CHNGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY V95_10.WDATE, V95_10.WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.EMPID = reader["EMPID"].ToString();
                m_Info.EMPNM = reader["EMPNM"].ToString();
                m_Info.WDATE = reader["WDATE"].ToString();
                m_Info.WTIME = reader["WTIME"].ToString();
                m_Info.InCondiQ1_1 = reader["InCondiQ1_1"].ToString();
                m_Info.InCondiQ1_2 = reader["InCondiQ1_2"].ToString();
                m_Info.InCondiQ1_3 = reader["InCondiQ1_3"].ToString();
                m_Info.InCondiQ7 = reader["InCondiQ7"].ToString();
                m_Info.GUBUN_3 = reader["GUBUN_3"].ToString();
                m_Info.Society1_ADOBJ = reader["Society1_ADOBJ"].ToString();
                m_Info.Society2_INHIS = reader["Society2_INHIS"].ToString();
                m_Info.InCondiQ6_ETC = reader["InCondiQ6_ETC"].ToString();
                m_Info.Society2_Q3_2 = reader["Society2_Q3_2"].ToString();
                m_Info.Society2_Q3_ETC_1 = reader["Society2_Q3_ETC_1"].ToString();
                m_Info.Society2_Q3_ETC_2 = reader["Society2_Q3_ETC_2"].ToString();
                m_Info.FAQ1_1 = reader["FAQ1_1"].ToString();
                m_Info.FAQ1_2 = reader["FAQ1_2"].ToString();
                m_Info.FAQ1_3 = reader["FAQ1_3"].ToString();
                m_Info.FAQ1_4 = reader["FAQ1_4"].ToString();
                m_Info.FAQ1_5 = reader["FAQ1_5"].ToString();
                m_Info.FAQ1_6 = reader["FAQ1_6"].ToString();
                m_Info.FAQ1_7 = reader["FAQ1_7"].ToString();
                m_Info.FAQ1_ETC = reader["FAQ1_ETC"].ToString();
                m_Info.FAQ2_1 = reader["FAQ2_1"].ToString();
                m_Info.FAQ2_2 = reader["FAQ2_2"].ToString();
                m_Info.FAQ2_3 = reader["FAQ2_3"].ToString();
                m_Info.FAQ2_4 = reader["FAQ2_4"].ToString();
                m_Info.FAQ2_5 = reader["FAQ2_5"].ToString();
                m_Info.FAQ2_6 = reader["FAQ2_6"].ToString();
                m_Info.FAQ2_7 = reader["FAQ2_7"].ToString();
                m_Info.FAQ2_ETC = reader["FAQ2_ETC"].ToString();
                m_Info.FAQ3_1 = reader["FAQ3_1"].ToString();
                m_Info.FAQ3_2 = reader["FAQ3_2"].ToString();
                m_Info.FAQ3_3 = reader["FAQ3_3"].ToString();
                m_Info.FAQ3_4 = reader["FAQ3_4"].ToString();
                m_Info.FAQ3_5 = reader["FAQ3_5"].ToString();
                m_Info.FAQ3_6 = reader["FAQ3_6"].ToString();
                m_Info.FAQ3_7 = reader["FAQ3_7"].ToString();
                m_Info.FAQ3_ETC = reader["FAQ3_ETC"].ToString();
                m_Info.FAQ4_1 = reader["FAQ4_1"].ToString();
                m_Info.FAQ4_2 = reader["FAQ4_2"].ToString();
                m_Info.FAQ4_3 = reader["FAQ4_3"].ToString();
                m_Info.FAQ4_4 = reader["FAQ4_4"].ToString();
                m_Info.FAQ4_5 = reader["FAQ4_5"].ToString();
                m_Info.FAQ4_6 = reader["FAQ4_6"].ToString();
                m_Info.FAQ4_7 = reader["FAQ4_7"].ToString();
                m_Info.FAQ4_ETC = reader["FAQ4_ETC"].ToString();
                m_Info.HABITQ3_3 = reader["HABITQ3_3"].ToString();
                m_Info.HABITQ3_ETC_1 = reader["HABITQ3_ETC_1"].ToString();
                m_Info.HABITQ3_ETC_2 = reader["HABITQ3_ETC_2"].ToString();
                m_Info.HABITQ3_ETC_3 = reader["HABITQ3_ETC_3"].ToString();
                m_Info.HABITQ4_3 = reader["HABITQ4_3"].ToString();
                m_Info.HABITQ4_ETC_1 = reader["HABITQ4_ETC_1"].ToString();
                m_Info.HABITQ4_ETC_2 = reader["HABITQ4_ETC_2"].ToString();
                m_Info.HABITQ4_ETC_3 = reader["HABITQ4_ETC_3"].ToString();
                m_Info.NewBornQ13_1 = reader["NewBornQ13_1"].ToString();
                m_Info.NewBornQ13_2 = reader["NewBornQ13_2"].ToString();
                m_Info.NewBornQ13_ETC = reader["NewBornQ13_ETC"].ToString();
                m_Info.InCondiTPR_1 = reader["InCondiTPR_1"].ToString();
                m_Info.InCondiTPR_2 = reader["InCondiTPR_2"].ToString();
                m_Info.InCondiTPR_3 = reader["InCondiTPR_3"].ToString();
                m_Info.InCondiTPR_4 = reader["InCondiTPR_4"].ToString();
                m_Info.InCondiTPR_5 = reader["InCondiTPR_5"].ToString();
                m_Info.InCondiTPR_8 = reader["InCondiTPR_8"].ToString();

                return false;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT PHX,PE";
            sql += Environment.NewLine + "  FROM EMR290";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.PHX = reader["PHX"].ToString();

                return false;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT TOP 1 HT";
            sql += Environment.NewLine + "  FROM TU64";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(HT,'')<>''";
            sql += Environment.NewLine + " ORDER BY CHKDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.HT = reader["HT"].ToString();

                return false;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT TOP 1 WT";
            sql += Environment.NewLine + "  FROM TU64";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(WT,'')<>''";
            sql += Environment.NewLine + " ORDER BY CHKDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.WT = reader["WT"].ToString();

                return false;
            });

            List<CInfoTable> list = new List<CInfoTable>();
            grdInfo.DataSource = null;
            grdInfo.DataSource = list;

            list.Add(new CInfoTable("입원일시", m_Info.IPAT_DT));
            list.Add(new CInfoTable("진료과", m_Info.IPAT_DGSBJT_CD));
            list.Add(new CInfoTable("내과세부", m_Info.IFLD_DTL_SPC_SBJT_CD));
            list.Add(new CInfoTable("작성자", m_Info.WRTP_NM));
            list.Add(new CInfoTable("작성일시", m_Info.WRT_DT));
            list.Add(new CInfoTable("입원경로", m_Info.VST_PTH_CD_NM));
            list.Add(new CInfoTable("입원경로상세", m_Info.VST_PTH_ETC_TXT));
            list.Add(new CInfoTable("환자구분", m_Info.PTNT_TP_CD_NM));
            // 일반정보
            string[] arr = m_Info.CC_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "입원동기" : "", arr[idx]));
            }
            arr = m_Info.ANMN_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "과거력" : "", arr[idx]));
            }
            arr = m_Info.SOPR_HIST_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "수술력" : "", arr[idx]));
            }
            arr = m_Info.MDCT_STAT_TXT.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int idx = 0; idx < arr.Length; idx++)
            {
                list.Add(new CInfoTable(idx == 0 ? "최근투약상태" : "", arr[idx]));
            }
            list.Add(new CInfoTable("알레르기 여부", m_Info.ALRG_YN_NM));
            list.Add(new CInfoTable("알레르기 내용", m_Info.ALRG_TXT));
            list.Add(new CInfoTable("가족력", m_Info.FMHS_TXT));
            list.Add(new CInfoTable("음주여부", m_Info.DRNK_YN_NM));
            list.Add(new CInfoTable("음주내용", m_Info.DRNK_TXT));
            list.Add(new CInfoTable("흡연여부", m_Info.SMKN_YN_NM));
            list.Add(new CInfoTable("흡연내용", m_Info.SMKN_TXT));
            list.Add(new CInfoTable("신장", m_Info.HEIG));
            list.Add(new CInfoTable("체중", m_Info.BWGT));
            list.Add(new CInfoTable("신체검진", m_Info.PHBD_MEDEXM_TXT));
            // 신생아 정보
            list.Add(new CInfoTable("출생일시", m_Info.BIRTH_DT));
            list.Add(new CInfoTable("재태기간", m_Info.FTUS_DEV_TRM));
            list.Add(new CInfoTable("분만형태", m_Info.PARTU_FRM_TXT));
            list.Add(new CInfoTable("Apgar Score", m_Info.APSC_PNT));
            list.Add(new CInfoTable("분만관련 특이사항", m_Info.PARTU_TXT));
            list.Add(new CInfoTable("혈압", m_Info.NBY_BPRSU));
            list.Add(new CInfoTable("맥박", m_Info.NBY_PULS));
            list.Add(new CInfoTable("호흡", m_Info.NBY_BRT));
            list.Add(new CInfoTable("체온", m_Info.NBY_TMPR));
            list.Add(new CInfoTable("산소포화도", m_Info.NBY_OXY_STRT));
            list.Add(new CInfoTable("출생시 체중", m_Info.NBY_BIRTH_BWGT));
            list.Add(new CInfoTable("입실시 체중", m_Info.NBY_IPAT_BWGT));
            list.Add(new CInfoTable("신체검진", m_Info.NBY_PHBD_MEDEXM_TXT));

            RefreshGrid(grdInfo, grdInfoView);
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RNP001");
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
            doc.Elements.Add("IPAT_DGSBJT_CD", m_Info.IPAT_DGSBJT_CD); // 진료과
            doc.Elements.Add("IFLD_DTL_SPC_SBJT_CD", m_Info.IFLD_DTL_SPC_SBJT_CD); // 내과세부
            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); // 작성자
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일시
            doc.Elements.Add("VST_PTH_CD", m_Info.VST_PTH_CD); // 입원경로
            doc.Elements.Add("VST_PTH_ETC_TXT", m_Info.VST_PTH_ETC_TXT); // 입원경로 상세
            doc.Elements.Add("PTNT_TP_CD", m_Info.PTNT_TP_CD); // 환자구분

            // B.일반정보
            if (m_Info.PTNT_TP_CD == "1")
            {
                doc.Elements.Add("CC_TXT", m_Info.CC_TXT); // 입원동기
                doc.Elements.Add("ANMN_TXT", m_Info.ANMN_TXT); // 과거력
                doc.Elements.Add("SOPR_HIST_TXT", m_Info.SOPR_HIST_TXT); // 수술력
                doc.Elements.Add("MDCT_STAT_TXT", m_Info.MDCT_STAT_TXT); // 최근 투약 상태
                doc.Elements.Add("ALRG_YN", m_Info.ALRG_YN); // 알레르기 여부
                doc.Elements.Add("ALRG_TXT", m_Info.ALRG_TXT); // 얄레르기 내용
                doc.Elements.Add("FMHS_TXT", m_Info.FMHS_TXT); // 가족력
                doc.Elements.Add("DRNK_YN", m_Info.DRNK_YN); // 음주여부
                doc.Elements.Add("DRNK_TXT", m_Info.DRNK_TXT); // 음주내용
                doc.Elements.Add("SMKN_YN", m_Info.SMKN_YN); // 흡연여부
                doc.Elements.Add("SMKN_TXT", m_Info.SMKN_TXT); // 흡연내용
                doc.Elements.Add("HEIG", m_Info.HEIG); // 신장
                doc.Elements.Add("BWGT", m_Info.BWGT); // 입원시체중
                doc.Elements.Add("PHBD_MEDEXM_TXT", m_Info.ALRG_TXT); // 신체검진
            }

            // C.신생아정보
            if (m_Info.PTNT_TP_CD == "2")
            {
                doc.Elements.Add("BIRTH_DT", m_Info.BIRTH_DT); // 출생일시
                doc.Elements.Add("FTUS_DEV_TRM", m_Info.FTUS_DEV_TRM); // 재태기간
                doc.Elements.Add("PARTU_FRM_TXT", m_Info.PARTU_FRM_TXT); // 분만형태
                doc.Elements.Add("APSC_PNT", m_Info.APSC_PNT); // Apgar Score
                doc.Elements.Add("PARTU_TXT", m_Info.PARTU_TXT); // 분만관련 특이사항
                doc.Elements.Add("NBY_BPRSU", m_Info.NBY_BPRSU); // 혈압
                doc.Elements.Add("NBY_PULS", m_Info.NBY_PULS); // 맥박
                doc.Elements.Add("NBY_BRT", m_Info.NBY_BRT); // 호흡
                doc.Elements.Add("NBY_TMPR", m_Info.NBY_TMPR); // 체온
                doc.Elements.Add("NBY_OXY_STRT", m_Info.NBY_OXY_STRT); // 산소포화도
                doc.Elements.Add("NBY_BIRTH_BWGT", m_Info.NBY_BIRTH_BWGT); // 출생시 체중
                doc.Elements.Add("NBY_IPAT_BWGT", m_Info.NBY_IPAT_BWGT); // 입실시 체중
                doc.Elements.Add("NBY_PHBD_MEDEXM_TXT", m_Info.NBY_PHBD_MEDEXM_TXT); // 신체검진
            }

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
                    CTI84A.SaveSend("RNP001", m_req_data_no, m_dmd_no, m_sp_sno, "");
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

    }
}

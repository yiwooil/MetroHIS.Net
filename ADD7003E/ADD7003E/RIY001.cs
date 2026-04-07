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
    public partial class RIY001 : Form
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

        private RIY001_Info m_Info = new RIY001_Info();

        public RIY001()
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
            list.Add(new CInfoTable("입원과", ""));
            list.Add(new CInfoTable("입원 내과세부", ""));
            list.Add(new CInfoTable("전과일", ""));
            list.Add(new CInfoTable("전입과", ""));
            list.Add(new CInfoTable("전입 내과세부", ""));
            list.Add(new CInfoTable("전출과", ""));
            list.Add(new CInfoTable("전출 내과세부", ""));
            list.Add(new CInfoTable("담당의사", ""));
            list.Add(new CInfoTable("작성자", ""));
            list.Add(new CInfoTable("작성일시", ""));

            list.Add(new CInfoTable("현병력", ""));
            list.Add(new CInfoTable("신체검진", ""));
            list.Add(new CInfoTable("문제목록", ""));

            list.Add(new CInfoTable("진단명", ""));
            list.Add(new CInfoTable("치료계획", ""));

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
            sql += Environment.NewLine + "SELECT GR.TDATE, GR.TDRID, GR.DRNM, GR.SYSDT, GR.SYSTM, GR.NOWT, GR.APLAN, GR.DXD";
            sql += Environment.NewLine + "     , A07.DRNM TDRNM, A07.DPTCD";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TK71GR GR INNER JOIN TA07 A07 ON A07.DRID=GR.TDRID";
            sql += Environment.NewLine + "                 INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += Environment.NewLine + " WHERE GR.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND GR.TDATE>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND GR.TDATE<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(GR.CANCEL,'')=''";
            sql += Environment.NewLine + " ORDER BY GR.TDATE";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.IN_DATE = reader["TDATE"].ToString(); // 전과일
                m_Info.IN_DRID = reader["TDRID"].ToString(); // 전입의사
                m_Info.IN_DRNM = reader["TDRNM"].ToString();
                m_Info.IN_DPTCD = reader["DPTCD"].ToString(); // 진료과
                m_Info.IN_INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.IN_INSDPTCD2 = reader["INSDPTCD2"].ToString();
                m_Info.DRNM = reader["DRNM"].ToString();
                m_Info.SYSDT = reader["SYSDT"].ToString();
                m_Info.SYSTM = reader["SYSTM"].ToString();
                m_Info.NOWT = reader["NOWT"].ToString();
                m_Info.APLAN = reader["APLAN"].ToString();
                m_Info.DXD = reader["DXD"].ToString();

                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT HM.DRID,A07.DRNM,A07.DPTCD,A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TK71HM HM INNER JOIN TA07 A07 ON A07.DRID=HM.DRID";
            sql += Environment.NewLine + "                 INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += Environment.NewLine + " WHERE HM.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND HM.TDATE>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND HM.TDATE<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(HM.CANCEL,'')=''";
            sql += Environment.NewLine + " ORDER BY HM.TDATE";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.OT_DRID = reader["DRID"].ToString();
                m_Info.OT_DRNM = reader["DRNM"].ToString();
                m_Info.OT_DPTCD = reader["DPTCD"].ToString();
                m_Info.OT_INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.OT_INSDPTCD2 = reader["INSDPTCD2"].ToString();

                return false;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT A04.BEDEDT, A04.BEDEHM, A04.DPTCD, A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TA04 A04 INNER JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += Environment.NewLine + " WHERE A04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND A04.BEDEDT='" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.BEDEDT = reader["BEDEDT"].ToString();
                m_Info.BEDEHM = reader["BEDEHM"].ToString();
                m_Info.DPTCD = reader["DPTCD"].ToString();
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString();

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.IPAT_DT); // 입원일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.IPAT_DGSBJT_CD); // 입원과
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.IPAT_IFLD_DTL_SPC_SBJT_CD); // 입원내과세부
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.TRFR_DD); // 전과일
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.MVIN_DGSBJT_CD); // 전입과
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.MVIN_IFLD_DTL_SPC_SBJT_CD); // 전입내과세부
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.MVOT_DGSBJT_CD); // 전출과
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.MVOT_IFLD_DTL_SPC_SBJT_CD); // 전출내과세부
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.CHRG_DR_NM); // 담당의
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.WRTP_NM); // 작성자
            grdInfoView.SetRowCellValue(10, gcCONTENT, m_Info.WRT_DT); // 작성일시

            grdInfoView.SetRowCellValue(11, gcCONTENT, m_Info.CUR_HOC_TXT); // 현병력
            grdInfoView.SetRowCellValue(12, gcCONTENT, m_Info.PHBD_MEDEXM_TXT); // 신체검진
            grdInfoView.SetRowCellValue(13, gcCONTENT, m_Info.PRBM_LIST_TXT); // 문제목록

            grdInfoView.SetRowCellValue(14, gcCONTENT, m_Info.DIAG_NM); // 진단명
            grdInfoView.SetRowCellValue(15, gcCONTENT, m_Info.TRET_PLAN_TXT); // 치료계획

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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RIY001");
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
            doc.Elements.Add("IPAT_DGSBJT_CD", m_Info.IPAT_DGSBJT_CD); // 입원과
            doc.Elements.Add("IPAT_IFLD_DTL_SPC_SBJT_CD", m_Info.IPAT_IFLD_DTL_SPC_SBJT_CD); // 입원과 내과상세
            doc.Elements.Add("TRFR_DD", m_Info.TRFR_DD); // 전과일
            doc.Elements.Add("MVIN_DGSBJT_CD", m_Info.MVIN_DGSBJT_CD); // 전입과
            doc.Elements.Add("MVIN_IFLD_DTL_SPC_SBJT_CD", m_Info.MVIN_IFLD_DTL_SPC_SBJT_CD); // 전입과 내과세부
            doc.Elements.Add("MVOT_DGSBJT_CD", m_Info.MVOT_DGSBJT_CD); // 전출과
            doc.Elements.Add("MVOT_IFLD_DTL_SPC_SBJT_CD", m_Info.MVOT_IFLD_DTL_SPC_SBJT_CD); // 전출과 내과세부
            doc.Elements.Add("CHRG_DR_NM", m_Info.CHRG_DR_NM); // 담당의사
            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); // 작성자
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일시

            // B.전입기록
            doc.Elements.Add("CUR_HOC_TXT", m_Info.CUR_HOC_TXT); // 현병력
            doc.Elements.Add("PHBD_MEDEXM_TXT", m_Info.PHBD_MEDEXM_TXT); // 신체검진
            doc.Elements.Add("PRBM_LIST_TXT", m_Info.PRBM_LIST_TXT); // 문제목록

            // 2.진단
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddRow("TBL_DIAG");
            doc.Tables["TBL_DIAG"].Rows[0]["DIAG_NM"].Value = m_Info.DXD;

            doc.Elements.Add("TRET_PLAN_TXT", m_Info.TRET_PLAN_TXT); // 치료계획



            // C.추가정보

            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("RIY001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }
    }
}

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
    public partial class RNE001 : Form
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

        private RNE001_Info m_Info = new RNE001_Info();

        public RNE001()
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

            list.Add(new CInfoTable("응급실 도착일시", ""));
            list.Add(new CInfoTable("응급실 퇴실일시", ""));
            list.Add(new CInfoTable("내원경로", ""));
            list.Add(new CInfoTable("내원경로 상세", ""));
            list.Add(new CInfoTable("내원동기 및 현상태", ""));

            list.Add(new CInfoTable("활력징후 측정일시", ""));
            list.Add(new CInfoTable("혈압", ""));
            list.Add(new CInfoTable("맥박", ""));
            list.Add(new CInfoTable("호흡", ""));
            list.Add(new CInfoTable("체온", ""));
            list.Add(new CInfoTable("산소 포화도", ""));

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
                SetStat(conn);
                SetRcd(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT PTMIINDT, PTMIINTM, PTMIOTDT, PTMIOTTM, PTMIINRT";
            sql += Environment.NewLine + "     , PTMIHIBP, PTMILOBP, PTMIPULS, PTMIBRTH, PTMIBDHT, PTMIVOXS";
            sql += Environment.NewLine + "  FROM EDIS.DBO.EMIHPTMI";
            sql += Environment.NewLine + " WHERE PTMIIDNO='" + m_pid + "'";
            sql += Environment.NewLine + "   AND PTMIINDT='" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.PTMIINDT = reader["PTMIINDT"].ToString();
                m_Info.PTMIINTM = reader["PTMIINTM"].ToString();
                m_Info.PTMIOTDT = reader["PTMIOTDT"].ToString();
                m_Info.PTMIOTTM = reader["PTMIOTTM"].ToString();
                m_Info.PTMIINRT = reader["PTMIINRT"].ToString();
                m_Info.PTMIHIBP = reader["PTMIHIBP"].ToString(); // 수축기 혈압
                m_Info.PTMILOBP = reader["PTMILOBP"].ToString(); // 이완기 혈압
                m_Info.PTMIPULS = reader["PTMIPULS"].ToString(); // 맥박
                m_Info.PTMIBRTH = reader["PTMIBRTH"].ToString(); // 호흡
                m_Info.PTMIBDHT = reader["PTMIBDHT"].ToString(); // 체온
                m_Info.PTMIVOXS = reader["PTMIVOXS"].ToString(); // 산소포화도

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.EMRRM_IPAT_DT); // 응급실 입실일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.EMRRM_DSCG_DT); // 응급실 퇴실일시
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.EMRRM_VST_PTH_CD_NM); // 내원경로
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.VST_PTH_ETC_TXT); // 내원경로 상세
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.PTNT_STAT_TXT); // 내원동기 및 현상태
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.MASR_DT); // 홀력징후 측정일시
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.BPRSU); // 혈압
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.PULS); // 맥박
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.BRT); // 호흡
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.TMPR); // 체온
            grdInfoView.SetRowCellValue(10, gcCONTENT, m_Info.OXY_STRT); // 산소포화도

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetRcd(OleDbConnection p_conn)
        {
            List<RNE001_Rcd> list = new List<RNE001_Rcd>();
            grdRcd.DataSource = null;
            grdRcd.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V92.WDATE,V92.WTIME,V92.RESULT,V92.PNURES";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "  FROM TV92 V92 INNER JOIN VA13 A13 ON A13.EMPID=V92.PNURES";
            sql += Environment.NewLine + " WHERE V92.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V92.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND V92.BDIV='3'";
            sql += Environment.NewLine + " ORDER BY V92.WDATE,V92.WTIME";


            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string result = reader["RESULT"].ToString();
                string[] arr = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int idx = 0; idx < arr.Length; idx++)
                {
                    RNE001_Rcd data = new RNE001_Rcd();
                    data.Clear();
                    if (idx == 0)
                    {
                        data.WDATE = reader["WDATE"].ToString();
                        data.WTIME = reader["WTIME"].ToString();
                        data.RESULT_ORG = result;
                        data.PNURES = reader["PNURES"].ToString();
                        data.EMPNM = reader["EMPNM"].ToString();
                    }
                    data.RESULT = arr[idx];

                    list.Add(data);
                }

                return true;
            });

            RefreshGrid(grdRcd, grdRcdView);
        }

        private void SetStat(OleDbConnection p_conn)
        {
            List<RNE001_Stat> list = new List<RNE001_Stat>();
            grdStat.DataSource = null;
            grdStat.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT EXDT";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(EO_SCORE,CHAR(21),1) EO_SCORE_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(EO_SCORE,CHAR(21),2) EO_SCORE_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(EO_SCORE,CHAR(21),3) EO_SCORE_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(EO_SCORE,CHAR(21),4) EO_SCORE_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(VR_SCORE,CHAR(21),1) VR_SCORE_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(VR_SCORE,CHAR(21),2) VR_SCORE_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(VR_SCORE,CHAR(21),3) VR_SCORE_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(VR_SCORE,CHAR(21),4) VR_SCORE_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(VR_SCORE,CHAR(21),5) VR_SCORE_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),1) EMR_SCORE_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),2) EMR_SCORE_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),3) EMR_SCORE_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),4) EMR_SCORE_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),5) EMR_SCORE_5";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BMR_SCORE,CHAR(21),6) EMR_SCORE_6";
            sql += Environment.NewLine + "  FROM TV17_4";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY EXDT";


            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNE001_Stat data = new RNE001_Stat();

                data.EXDT = reader["EXDT"].ToString();
                data.EO_SCORE_1 = reader["EO_SCORE_1"].ToString();
                data.EO_SCORE_2 = reader["EO_SCORE_2"].ToString();
                data.EO_SCORE_3 = reader["EO_SCORE_3"].ToString();
                data.EO_SCORE_4 = reader["EO_SCORE_4"].ToString();
                data.VR_SCORE_2 = reader["VR_SCORE_1"].ToString();
                data.VR_SCORE_3 = reader["VR_SCORE_2"].ToString();
                data.VR_SCORE_4 = reader["VR_SCORE_3"].ToString();
                data.VR_SCORE_5 = reader["VR_SCORE_4"].ToString();
                data.VR_SCORE_1 = reader["VR_SCORE_5"].ToString();
                data.BMR_SCORE_1 = reader["BMR_SCORE_1"].ToString();
                data.BMR_SCORE_2 = reader["BMR_SCORE_2"].ToString();
                data.BMR_SCORE_3 = reader["BMR_SCORE_3"].ToString();
                data.BMR_SCORE_4 = reader["BMR_SCORE_4"].ToString();
                data.BMR_SCORE_5 = reader["BMR_SCORE_5"].ToString();
                data.BMR_SCORE_6 = reader["BMR_SCORE_6"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdRcd, grdRcdView);
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RNE001");
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
            doc.Elements.Add("EMRRM_IPAT_DT", m_Info.EMRRM_IPAT_DT); // 응급실 입실일시
            doc.Elements.Add("EMRRM_DSCG_DT", m_Info.EMRRM_DSCG_DT); // 응급실 퇴실일시
            doc.Elements.Add("EMRRM_VST_PTH_CD", m_Info.EMRRM_VST_PTH_CD); // 내원경로
            doc.Elements.Add("VST_PTH_ETC_TXT", m_Info.VST_PTH_ETC_TXT); // 내원경로 상세
            doc.Elements.Add("PTNT_STAT_TXT", m_Info.PTNT_STAT_TXT); // 내원동기 및 현상태



            // B.응급 간호기록

            // 1.환자상태척도
            doc.Tables.AddColumn("TBL_PTNT_STAT_SCL", "MASR_DT"); // 척정일시
            doc.Tables.AddColumn("TBL_PTNT_STAT_SCL", "MASR_TL_NM"); // 도구
            doc.Tables.AddColumn("TBL_PTNT_STAT_SCL", "MASR_RST_TXT"); // 결과

            for (int row = 0; row < grdStatView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_PTNT_STAT_SCL");
                doc.Tables["TBL_PTNT_STAT_SCL"].Rows[row]["MASR_DT"].Value = grdRcdView.GetRowCellValue(row, gcMASR_DT).ToString();
                doc.Tables["TBL_PTNT_STAT_SCL"].Rows[row]["MASR_TL_NM"].Value = grdRcdView.GetRowCellValue(row, gcMASR_TL_NM).ToString();
                doc.Tables["TBL_PTNT_STAT_SCL"].Rows[row]["MASR_RST_TXT"].Value = grdRcdView.GetRowCellValue(row, gcMASR_RST_TXT).ToString();
            }

            //  2.활력징후
            doc.Tables.AddColumn("TBL_VTSG", "MASR_DT"); // 측정일시
            doc.Tables.AddColumn("TBL_VTSG", "BPRSU"); // 혈압
            doc.Tables.AddColumn("TBL_VTSG", "PULS"); //맥박
            doc.Tables.AddColumn("TBL_VTSG", "BRT"); // 호흡
            doc.Tables.AddColumn("TBL_VTSG", "TMPR"); // 체온
            doc.Tables.AddColumn("TBL_VTSG", "OXY_STRT"); // 산소포화도

            doc.Tables.AddRow("TBL_VTSG");
            doc.Tables["TBL_VTSG"].Rows[0]["MASR_DT"].Value = m_Info.MASR_DT;
            doc.Tables["TBL_VTSG"].Rows[0]["BPRSU"].Value = m_Info.BPRSU;
            doc.Tables["TBL_VTSG"].Rows[0]["PULS"].Value = m_Info.PULS;
            doc.Tables["TBL_VTSG"].Rows[0]["BRT"].Value = m_Info.BRT;
            doc.Tables["TBL_VTSG"].Rows[0]["TMPR"].Value = m_Info.TMPR;
            doc.Tables["TBL_VTSG"].Rows[0]["OXY_STRT"].Value = m_Info.OXY_STRT;

            
            // 3.처치및 간호내용
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_DT"); // 기록일시
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_TXT"); // 간호기록
            doc.Tables.AddColumn("TBL_CARE_RCD", "NURSE_NM"); // 간호사 성명
            int row_ins = 0;
            for (int row = 0; row < grdRcdView.RowCount; row++)
            {
                string rcd_txt_org = grdRcdView.GetRowCellValue(row, gcRCD_TXT_ORG).ToString();
                if (rcd_txt_org != "")
                {
                    doc.Tables.AddRow("TBL_CARE_RCD");
                    doc.Tables["TBL_CARE_RCD"].Rows[row_ins]["RCD_DT"].Value = grdRcdView.GetRowCellValue(row, gcRCD_DT).ToString();
                    doc.Tables["TBL_CARE_RCD"].Rows[row_ins]["RCD_TXT"].Value = rcd_txt_org;
                    doc.Tables["TBL_CARE_RCD"].Rows[row_ins]["NURSE_NM"].Value = grdRcdView.GetRowCellValue(row, gcNURSE_NM).ToString();
                    row_ins++;
                }
            }

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
                    CTI84A.SaveSend("RNE001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

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
    public partial class RAR001 : Form
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

        private RAR001_Info m_Info = new RAR001_Info();

        public RAR001()
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

            list.Add(new CInfoTable("회복실 도착일시", ""));
            list.Add(new CInfoTable("회복실 퇴실일시", ""));
            list.Add(new CInfoTable("퇴실결정 의사", ""));
            list.Add(new CInfoTable("작성자 성명", ""));
            list.Add(new CInfoTable("작성일시", ""));

            RefreshGrid(grdInfo, grdInfoView);
            //RefreshGridInfo();
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
                SetVtsg(conn);
                SetPain(conn);
                SetEmss(conn);
                SetRcov(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U03.RCVDT2, U03.RCVHR2, U03.RCVMN2, U03.RCVDT, U03.RCVHR, U03.RCVMN, U03.ANEDR, U03.USRID, U03.ENTDT, U03.ENTMS";
            sql += Environment.NewLine + "     , A07.DRNM";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "  FROM TU03 U03 INNER JOIN TA07 A07 ON A07.DRID=U03.ANEDR";
            sql += Environment.NewLine + "                INNER JOIN VA13 A13 ON A13.EMPID=U03.USRID";
            sql += Environment.NewLine + " WHERE U03.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U03.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += Environment.NewLine + " ORDER BY U03.RCVDT2";


            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.RCVDT2 = reader["RCVDT2"].ToString(); // 회복실 도착일자
                m_Info.RCVHR2 = reader["RCVHR2"].ToString(); // 회복실 도착시간
                m_Info.RCVMN2 = reader["RCVMN2"].ToString(); // 회복실 도착분
                m_Info.RCVDT = reader["RCVDT"].ToString(); // 회복실 퇴실일자
                m_Info.RCVHR = reader["RCVHR"].ToString(); // 회복실 퇴신시간
                m_Info.RCVMN = reader["RCVMN"].ToString(); // 회복실 퇴실분
                m_Info.ANEDR = reader["ANEDR"].ToString(); // 퇴실 결정 의사
                m_Info.ANENM = reader["DRNM"].ToString();
                m_Info.USRID = reader["USRID"].ToString(); // 등록자ID
                m_Info.USRNM = reader["EMPNM"].ToString();
                m_Info.ENTDT = reader["ENTDT"].ToString(); // 등록일자
                m_Info.ENTMS = reader["ENTMS"].ToString(); // 등록시분

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.RCRM_IPAT_DT); // 회복실 도착일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.RCRM_DSCG_DT); // 회복실 퇴실일시
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.DSCG_DEC_DR_NM); //퇴실 결정 의사 성명
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.WRTP_NM); // 작성자 성명
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.WRT_DT); // 작성일시

            RefreshGrid(grdInfo, grdInfoView);
            //RefreshGridInfo();
        }

        private void SetVtsg(OleDbConnection p_conn)
        {
            List<RAR001_Vtsg> list = new List<RAR001_Vtsg>();
            grdVtsg.DataSource = null;
            grdVtsg.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U64.CHKDT, U64.CHKTM, U64.BP, U64.PR, U64.RR, U64.TMP, U64.RMK";
            sql += Environment.NewLine + "  FROM TU64 U64";
            sql += Environment.NewLine + " WHERE U64.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U64.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + " ORDER BY U64.CHKDT, U64.CHKTM";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAR001_Vtsg data = new RAR001_Vtsg();

                data.CHKDT = reader["CHKDT"].ToString(); // 활력징후 측정일자
                data.CHKTM = reader["CHKTM"].ToString(); //          측정시분
                data.BP = reader["BP"].ToString(); // 혈압
                data.PR = reader["PR"].ToString(); // 맥박
                data.RR = reader["RR"].ToString(); // 호흡
                data.TMP = reader["TMP"].ToString(); // 체옴
                data.RMK = reader["RMK"].ToString(); // 비고

                list.Add(data);

                return true;
            });

            RefreshGrid(grdVtsg, grdVtsgView);
            //RefreshGridVtsg();
        }

        private void SetPain(OleDbConnection p_conn)
        {
            List<RAR001_Pain> list = new List<RAR001_Pain>();
            grdPain.DataSource = null;
            grdPain.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE, WTIME, TOOL, POW";
            sql += Environment.NewLine + "  FROM AUTH004";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAR001_Pain data = new RAR001_Pain();

                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = reader["WTIME"].ToString();
                data.TOOL = reader["TOOL"].ToString();
                data.POW = reader["POW"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdPain, grdPainView);
            //RefreshGridPain();
        }

        private void SetEmss(OleDbConnection p_conn)
        {
            List<RAR001_Emss> list = new List<RAR001_Emss>();
            grdEmss.DataSource = null;
            grdEmss.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE, WTIME, NRS";
            sql += Environment.NewLine + "  FROM AUTH031";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAR001_Emss data = new RAR001_Emss();

                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = reader["WTIME"].ToString();
                data.NRS = reader["NRS"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdEmss, grdEmssView);
        }

        private void SetRcov(OleDbConnection p_conn)
        {
            List<RAR001_Rcov> list = new List<RAR001_Rcov>();
            grdRcov.DataSource = null;
            grdRcov.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE, WTIME";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(Q1,CHAR(21),1)='1' THEN '2'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(Q1,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "            ELSE '0'";
            sql += Environment.NewLine + "       END Q1_POINT";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(Q2,CHAR(21),1)='1' THEN '2'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(Q2,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "            ELSE '0'";
            sql += Environment.NewLine + "       END Q2_POINT";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(Q3,CHAR(21),1)='1' THEN '2'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(Q3,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "            ELSE '0'";
            sql += Environment.NewLine + "       END Q3_POINT";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(Q4,CHAR(21),1)='1' THEN '2'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(Q4,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "            ELSE '0'";
            sql += Environment.NewLine + "       END Q4_POINT";
            sql += Environment.NewLine + "     , CASE WHEN dbo.MFN_PIECE(Q5,CHAR(21),1)='1' THEN '2'";
            sql += Environment.NewLine + "            WHEN dbo.MFN_PIECE(Q5,CHAR(21),2)='1' THEN '1'";
            sql += Environment.NewLine + "            ELSE '0'";
            sql += Environment.NewLine + "       END Q5_POINT";
            sql += Environment.NewLine + "     , TOTAL";
            sql += Environment.NewLine + "  FROM EMR244";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RAR001_Rcov data = new RAR001_Rcov();

                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = reader["WTIME"].ToString();
                data.Q1_POINT = reader["Q1_POINT"].ToString();
                data.Q2_POINT = reader["Q2_POINT"].ToString();
                data.Q3_POINT = reader["Q3_POINT"].ToString();
                data.Q4_POINT = reader["Q4_POINT"].ToString();
                data.Q5_POINT = reader["Q5_POINT"].ToString();
                data.TOTAL = reader["TOTAL"].ToString();

                list.Add(data);

                return true;
            });

            if (list.Count < 1)
            {
                RAR001_Rcov data = new RAR001_Rcov();

                data.WDATE = "";
                data.WTIME = "";
                data.Q1_POINT ="";
                data.Q2_POINT = "";
                data.Q3_POINT = "";
                data.Q4_POINT = "";
                data.Q5_POINT = "";
                data.TOTAL = "";

                list.Add(data);
            }

            RefreshGrid(grdRcov, grdRcovView);
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RAR001");
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
            doc.Elements.Add("RCRM_IPAT_DT", m_Info.RCRM_IPAT_DT); // 회복실 도착일시
            doc.Elements.Add("RCRM_DSCG_DT", m_Info.RCRM_DSCG_DT); // 회복실 퇴실일시
            doc.Elements.Add("DSCG_DEC_DR_NM", m_Info.DSCG_DEC_DR_NM); // 퇴실 결정 의사
            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); // 작성자 성명
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일시

            // B.세부정보

            // 1.활력징후
            doc.Tables.AddColumn("TBL_VTSG", "MASR_DT"); // 측정일시
            doc.Tables.AddColumn("TBL_VTSG", "BPRSU"); // 혈압
            doc.Tables.AddColumn("TBL_VTSG", "PULS"); // 맥박
            doc.Tables.AddColumn("TBL_VTSG", "BRT"); // 호흡
            doc.Tables.AddColumn("TBL_VTSG", "TMPR"); // 체온
            doc.Tables.AddColumn("TBL_VTSG", "OXY_STRT"); // 산소포화도
            doc.Tables.AddColumn("TBL_VTSG", "VTSG_TXT"); // 특이사항

            for (int row = 0; row < grdVtsgView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_VTSG");
                doc.Tables["TBL_VTSG"].Rows[row]["MASR_DT"].Value = grdVtsgView.GetRowCellValue(row, gcMASR_DT).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["BPRSU"].Value = grdVtsgView.GetRowCellValue(row, gcBPRSU).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["PULS"].Value = grdVtsgView.GetRowCellValue(row, gcPULS).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["BRT"].Value = grdVtsgView.GetRowCellValue(row, gcBRT).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["TMPR"].Value = grdVtsgView.GetRowCellValue(row, gcTMPR).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["OXY_STRT"].Value = grdVtsgView.GetRowCellValue(row, gcOXY_STRT).ToString();
                doc.Tables["TBL_VTSG"].Rows[row]["VTSG_TXT"].Value = grdVtsgView.GetRowCellValue(row, gcVTSG_TXT).ToString();
            }

            // 3.평가
            // 3-1.통증평가
            doc.Elements.Add("PAIN_ASM_YN", grdPainView.RowCount > 0 ? "1" : "2"); // 통증평가 실시여부
            if (grdPainView.RowCount > 0)
            {
                doc.Tables.AddColumn("TBL_PAIN_ASM", "EXEC_DT"); // 평가일시
                doc.Tables.AddColumn("TBL_PAIN_ASM", "PAIN_ASM_TL_CD"); // 평가도구
                doc.Tables.AddColumn("TBL_PAIN_ASM", "ASM_TL_ETC_TXT"); // 도구상세
                doc.Tables.AddColumn("TBL_PAIN_ASM", "ASM_RST_TXT"); // 결과
                for (int row = 0; row < grdPainView.RowCount; row++)
                {
                    doc.Tables.AddRow("TBL_PAIN_ASM");
                    doc.Tables["TBL_PAIN_ASM"].Rows[row]["EXEC_DT"].Value = grdPainView.GetRowCellValue(row, gcEXEC_DT_PAIN).ToString();
                    doc.Tables["TBL_PAIN_ASM"].Rows[row]["PAIN_ASM_TL_CD"].Value = grdPainView.GetRowCellValue(row, gcPAIN_ASM_TL_CD_PAIN).ToString();
                    doc.Tables["TBL_PAIN_ASM"].Rows[row]["ASM_TL_ETC_TXT"].Value = grdPainView.GetRowCellValue(row, gcASM_TL_ETC_TXT_PAIN).ToString();
                    doc.Tables["TBL_PAIN_ASM"].Rows[row]["ASM_RST_TXT"].Value = grdPainView.GetRowCellValue(row, gcASM_RST_TXT_PAIN).ToString();
                }
            }

            // 3-2.오심구토평가
            doc.Elements.Add("EMSS_ASM_YN", grdEmssView.RowCount > 0 ? "1" : "2"); // 오심구토평가 실시여부
            if (grdEmssView.RowCount > 0)
            {
                doc.Tables.AddColumn("TBL_EMSS_ASM", "EXEC_DT"); // 평가일시
                doc.Tables.AddColumn("TBL_EMSS_ASM", "ASM_RST_TXT"); // 결과
                for (int row = 0; row < grdEmssView.RowCount; row++)
                {
                    doc.Tables.AddRow("TBL_EMSS_ASM");
                    doc.Tables["TBL_EMSS_ASM"].Rows[row]["EXEC_DT"].Value = grdEmssView.GetRowCellValue(row, gcEXEC_DT_EMSS).ToString();
                    doc.Tables["TBL_EMSS_ASM"].Rows[row]["ASM_RST_TXT"].Value = grdEmssView.GetRowCellValue(row, gcASM_RST_TXT_EMSS).ToString();
                }
            }

            // C. Post-Anesthetic Recovery Score

            // 마취회복점수
            doc.Tables.AddColumn("TBL_NCT_RCOV", "MASR_DT"); // 측정일시
            doc.Tables.AddColumn("TBL_NCT_RCOV", "ACTV_PNT"); // 활동성
            doc.Tables.AddColumn("TBL_NCT_RCOV", "BRT_PNT"); // 호흡
            doc.Tables.AddColumn("TBL_NCT_RCOV", "CRCL_PNT"); // 순환
            doc.Tables.AddColumn("TBL_NCT_RCOV", "CNSCS_PNT"); // 의식
            doc.Tables.AddColumn("TBL_NCT_RCOV", "SKN_COLR_PNT"); // 피부색
            doc.Tables.AddColumn("TBL_NCT_RCOV", "TOT_PNT"); // 합계

            for (int row = 0; row < grdRcovView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_NCT_RCOV");
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["MASR_DT"].Value = grdRcovView.GetRowCellValue(row, gcMASR_DT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["ACTV_PNT"].Value = grdRcovView.GetRowCellValue(row, gcACTV_PNT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["BRT_PNT"].Value = grdRcovView.GetRowCellValue(row, gcBRT_PNT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["CRCL_PNT"].Value = grdRcovView.GetRowCellValue(row, gcCRCL_PNT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["CNSCS_PNT"].Value = grdRcovView.GetRowCellValue(row, gcCNSCS_PNT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["SKN_COLR_PNT"].Value = grdRcovView.GetRowCellValue(row, gcSKN_COLR_PNT).ToString();
                doc.Tables["TBL_NCT_RCOV"].Rows[row]["TOT_PNT"].Value = grdRcovView.GetRowCellValue(row, gcTOT_PNT).ToString();
            }

            // D.회북 중 틍이사항

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
                    CTI84A.SaveSend("RAR001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }
    }
}

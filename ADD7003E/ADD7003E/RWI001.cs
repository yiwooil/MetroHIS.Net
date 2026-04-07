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
    public partial class RWI001 : Form
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

        private RWI001_Info m_Info = new RWI001_Info();

        public RWI001()
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

            list.Add(new CInfoTable("최초입실일시", ""));
            list.Add(new CInfoTable("인공호흡기 여부", ""));
            list.Add(new CInfoTable("산소요법 여부", ""));
            list.Add(new CInfoTable("삽입관 여부", ""));
            list.Add(new CInfoTable("배액관 여부", ""));
            list.Add(new CInfoTable("특수처치 여부", ""));
            list.Add(new CInfoTable("모니터링 종류", ""));
            list.Add(new CInfoTable("중증도 여부", ""));

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
                SetDiag(conn);
                SetDscg(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            // ICU에 있었는지 검사
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TT04";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND WDID2 IN ('ICU')";
            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;
                return false;
            });

            if (m_Info.R_CNT < 1) return;


            // ICU에 처음 들어간 일시
            sql = "";
            sql += Environment.NewLine + "SELECT MIN(CRDHM) MIN_CRDHM";
            sql += Environment.NewLine + "  FROM TT04";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND WDID2 IN ('ICU')";


            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.MIN_CRDHM = reader["MIN_CRDHM"].ToString();
                return false;
            });


            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.FST_IPAT_DT); // 최초입실일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.ATFL_RPRT_ENFC_YN_NM); // 인공호흡기여부
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.OXY_CURE_YN_NM); // 산소흐흡기여부
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.CNNL_ENFC_YN_NM); // 삽입관여부
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.DRN_ENFC_YN_NM); // 배액관여부
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.SPCL_TRET_CD_NM); // 틋수처치여부
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.MNTR_KND_CD_NM); // 모니터링 종류
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.SGRD_PNT_YN_NM); // 중증도여부

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RWI001_Diag> list = new List<RWI001_Diag>();
            grdDiag.DataSource = null;
            grdDiag.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 진단
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.PTYSQ, A.ROFG, A.DXD, A.DACD";
            sql += Environment.NewLine + "  FROM TT05 A";
            sql += Environment.NewLine + " WHERE A.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY A.PTYSQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RWI001_Diag data = new RWI001_Diag();
                data.PTYSQ = reader["PTYSQ"].ToString();
                data.ROFG = reader["ROFG"].ToString();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGrid(grdDiag, grdDiagView);
        }

        private void SetDscg(OleDbConnection p_conn)
        {
            List<RWI001_Dscg> list = new List<RWI001_Dscg>();
            grdDscg.DataSource = null;
            grdDscg.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 입퇴실정보
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T04.CRDHM, T04.DPTC2, T04.PDR2";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "     , A07.DRNM";
            sql += Environment.NewLine + "  FROM TT04 T04 INNER JOIN TA09 A09 ON A09.DPTCD=T04.DPTC2";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=T04.PDR2";
            sql += Environment.NewLine + " WHERE T04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T04.BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND WDID2 IN ('ICU')";
            sql += Environment.NewLine + " ORDER BY T04.CRDHM";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RWI001_Dscg data = new RWI001_Dscg();
                data.CRDHM = reader["CRDHM"].ToString();
                data.DPTCD = reader["DPTC2"].ToString();
                data.PDRID = reader["PDR2"].ToString();
                data.INSDPTCD = reader["INSDPTCD"].ToString();
                data.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                data.PDRNM = reader["DRNM"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdDscg, grdDscgView);
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RWI001");
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
            doc.Elements.Add("IPAT_DT", m_Info.FST_IPAT_DT); // 최초입실일시

            // 2.진단
            doc.Tables.AddColumn("TBL_DIAG", "SICK_TP_CD"); // 상병분류구분
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_SICK_SYM"); // 상병분류기호

            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DIAG");
                doc.Tables["TBL_DIAG"].Rows[row]["SICK_TP_CD"].Value = grdDiagView.GetRowCellValue(row, gcSICK_TP_CD).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_SICK_SYM).ToString();
            }

            // B.중환지실 입.퇴실 정보
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "CHRG_DR_NM"); // 담당의사
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "DGSBJT_CD"); // 진료과
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "IFLD_DTL_SPC_SBJT_CD"); // 내과세부
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "WRTP_NM"); // 작성자
            // 입실
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "SPRM_IPAT_DT"); //입실일시
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "SPRM_IPAT_PTH_CD"); // 입실경로
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "IPAT_PTH_ETC_TXT"); // 입실경로 상세
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "SPRM_IPAT_RS_CD"); // 입실사유
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "RE_IPAT_RS_TXT"); // 재입실 사유
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "IPAT_RS_ETC_TXT"); // 입실사유 상세
            // 퇴실
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "SPRM_DSCG_RST_CD"); // 퇴실상태
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "DSCG_RST_TXT"); // 퇴실상태 상세
            // 사망
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "DEATH_DT"); // 사망일시
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "DEATH_SICK_SYM"); // 원사인
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "DEATH_DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_IPAT_DSCG", "SPRM_DSCG_DT"); // 퇴실일시

            for (int row = 0; row < grdDscgView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_IPAT_DSCG");
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["CHRG_DR_NM"].Value = grdDscgView.GetRowCellValue(row, gcCHRG_DR_NM).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["DGSBJT_CD"].Value = grdDscgView.GetRowCellValue(row, gcDGSBJT_CD).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdDscgView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["WRTP_NM"].Value = grdDscgView.GetRowCellValue(row, gcWRTP_NM).ToString();
                // 입실
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["SPRM_IPAT_DT"].Value = grdDscgView.GetRowCellValue(row, gcSPRM_IPAT_DT).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["SPRM_IPAT_PTH_CD"].Value = grdDscgView.GetRowCellValue(row, gcSPRM_IPAT_PTH_CD).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["IPAT_PTH_ETC_TXT"].Value = grdDscgView.GetRowCellValue(row, gcIPAT_PTH_ETC_TXT).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["SPRM_IPAT_RS_CD"].Value = grdDscgView.GetRowCellValue(row, gcSPRM_IPAT_RS_CD).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["RE_IPAT_RS_TXT"].Value = grdDscgView.GetRowCellValue(row, gcRE_IPAT_RS_TXT).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["IPAT_RS_ETC_TXT"].Value = grdDscgView.GetRowCellValue(row, gcIPAT_RS_ETC_TXT).ToString();
                // 퇴실
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["SPRM_DSCG_RST_CD"].Value = grdDscgView.GetRowCellValue(row, gcSPRM_DSCG_RST_CD).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["DSCG_RST_TXT"].Value = grdDscgView.GetRowCellValue(row, gcDSCG_RST_TXT).ToString();
                // 사망
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["DEATH_DT"].Value = grdDscgView.GetRowCellValue(row, gcDEATH_DT).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["DEATH_SICK_SYM"].Value = grdDscgView.GetRowCellValue(row, gcDEATH_SICK_SYM).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["DEATH_DIAG_NM"].Value = grdDscgView.GetRowCellValue(row, gcDEATH_DIAG_NM).ToString();
                doc.Tables["TBL_IPAT_DSCG"].Rows[row]["SPRM_DSCG_DT"].Value = grdDscgView.GetRowCellValue(row, gcSPRM_DSCG_DT).ToString();
            }


            // D.기타정보
            doc.Elements.Add("ATFL_RPRT_ENFC_YN", m_Info.ATFL_RPRT_ENFC_YN); // 인공호흡기 적용 여부
            doc.Elements.Add("OXY_CURE_YN", m_Info.OXY_CURE_YN); // 산소요법 적용여부
            doc.Elements.Add("OXY_CURE_YN", m_Info.CNNL_ENFC_YN); // 삽입관 적용여부
            doc.Elements.Add("DRN_ENFC_YN", m_Info.DRN_ENFC_YN); // 배액관 적용여부
            doc.Elements.Add("DRN_ENFC_YN", m_Info.SPCL_TRET_CD); // 특수처치
            doc.Elements.Add("MNTR_KND_CD", m_Info.MNTR_KND_CD); // 모니터링
            doc.Elements.Add("SGRD_PNT_YN", m_Info.SGRD_PNT_YN); // 중증도


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
                    CTI84A.SaveSend("RWI001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }


    }
}

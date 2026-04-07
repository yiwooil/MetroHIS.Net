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
    public partial class RNH001 : Form
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

        private RNH001_Info m_Info = new RNH001_Info();

        public RNH001()
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

            list.Add(new CInfoTable("진료과목", ""));
            list.Add(new CInfoTable("내과세부", ""));
            list.Add(new CInfoTable("담당의사", ""));
            list.Add(new CInfoTable("혈관협착 모니터링 여부", ""));
            list.Add(new CInfoTable("투석종류", ""));

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

                SetBldd(conn);
                SetVtsg(conn);
                SetRcd(conn);
                // 혈액투석한 경우에만 조회한다.
                SetInfo(conn);
                SetDiag(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            if (grdBlddView.RowCount < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A04.DPTCD, A04.PDRID, A09.INSDPTCD, A09.INSDPTCD2, A07.DRNM";
            sql += Environment.NewLine + "  FROM TA04 A04 INNER JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=A04.PDRID";
            sql += Environment.NewLine + " WHERE A04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND A04.BEDEDT='" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.DPTCD = reader["DPTCD"].ToString();
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                m_Info.PDRID = reader["PDRID"].ToString();
                m_Info.PDRNM = reader["DRNM"].ToString();

                return false;
            });


            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.DGSBJT_CD); // 진료과목
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.IFLD_DTL_SPC_SBJT_CD); // 내과세부
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.CHRG_DR_NM); // 담당의사
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.BLDVS_STNS_MNTR_YN_NM); // 혈관협착 모니터링 여부
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.DLYS_KND_CD_NM); // 투석종류

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RNH001_Diag> list = new List<RNH001_Diag>();
            grdDiag.DataSource = null;
            grdDiag.DataSource = list;

            if (grdBlddView.RowCount < 1) return;

            // 진단
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T05.DXD, T05.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TT05 T05 INNER JOIN TA16 A16 ON A16.DISECD=T05.DACD AND A16.DISEDIV=T05.DISEDIV";
            sql += Environment.NewLine + " WHERE T05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T05.BDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY T05.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNH001_Diag data = new RNH001_Diag();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGrid(grdDiag, grdDiagView);
        }

        private void SetBldd(OleDbConnection p_conn)
        {
            List<RNH001_Bldd> list = new List<RNH001_Bldd>();
            grdBldd.DataSource = null;
            grdBldd.DataSource = list;

            // 혈액투석
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U12.HMSDT, U12.HMFDT";
            sql += Environment.NewLine + "     , U67.LastWt, U67.HMBeCurWt, U67.HMAfCurWt, U67.HMveWay, U67.AntiBaseOqty, U67.MaintOqty, U67.HMMachine, U67.HMFluid, U67.UPDID";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "  FROM TU12 U12 INNER JOIN TU67 U67 ON U67.PID=U12.PID AND U67.CHKDT=U12.HMSDT";
            sql += Environment.NewLine + "                INNER JOIN VA13 A13 ON A13.EMPID=U67.UPDID";
            sql += Environment.NewLine + " WHERE U12.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U12.HMSDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND U12.HMSDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY U12.HMSDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNH001_Bldd data = new RNH001_Bldd();
                data.HMSDT = reader["HMSDT"].ToString(); // 투석시작일
                data.HMFDT = reader["HMFDT"].ToString(); // 투석종료일
                data.LastWt = reader["LastWt"].ToString(); // 건체중
                data.HMBeCurWt = reader["HMBeCurWt"].ToString(); // 투석적 체중
                data.HMAfCurWt = reader["HMAfCurWt"].ToString(); // 투석후 체중
                data.HMveWay = reader["HMveWay"].ToString(); // 혈관통로
                data.AntiBaseOqty = reader["AntiBaseOqty"].ToString(); // 항응고요법 초기 용량
                data.MaintOqty = reader["MaintOqty"].ToString(); // 항응고요법 유지 용량
                data.HMMachine = reader["HMMachine"].ToString(); // 투석기(기계명)
                data.HMFluid = reader["HMFluid"].ToString(); // 투석액
                data.UPDID = reader["UPDID"].ToString(); // 작성자
                data.UPDNM = reader["EMPNM"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGrid(grdBldd, grdBlddView);
        }

        private void SetVtsg(OleDbConnection p_conn)
        {
            List<RNH001_Vtsg> list = new List<RNH001_Vtsg>();
            grdVtsg.DataSource = null;
            grdVtsg.DataSource = list;

            // 혈액투석
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT CHKDT, CHKTM, Vtm, Vpressure, Vpulsation, Vvein, VSPEED";
            sql += Environment.NewLine + "  FROM TU67A";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND CHKDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND CHKDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY CHKDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNH001_Vtsg data = new RNH001_Vtsg();
                data.CHKDT = reader["CHKDT"].ToString(); // 측정일자
                data.CHKTM = reader["CHKTM"].ToString(); // 측정시간
                data.Vtm = reader["Vtm"].ToString(); // 혈압
                data.Vpressure = reader["Vpressure"].ToString(); // 맥박
                data.Vpulsation = reader["Vpulsation"].ToString(); // 혈류속도
                data.Vvein = reader["Vvein"].ToString(); // 동맥압
                data.VSPEED = reader["VSPEED"].ToString(); // 정맥압
                list.Add(data);

                return true;
            });

            RefreshGrid(grdVtsg, grdVtsgView);
        }

        private void SetRcd(OleDbConnection p_conn)
        {
            List<RNH001_Rcd> list = new List<RNH001_Rcd>();
            grdRcd.DataSource = null;
            grdRcd.DataSource = list;

            // 혈액투석
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U71.WDATE, U71.ILL, U71.EID, A13.EMPNM";
            sql += Environment.NewLine + "  FROM TU71 U71 INNER JOIN VA13 A13 ON A13.EMPID=U71.EID";
            sql += Environment.NewLine + " WHERE U71.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U71.WDATE>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND U71.WDATE<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY U71.WDATE";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNH001_Rcd data = new RNH001_Rcd();
                data.WDATE = reader["WDATE"].ToString();
                data.ILL = reader["ILL"].ToString();
                data.EID = reader["EID"].ToString();
                data.ENM = reader["EMPNM"].ToString();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RNH001");
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
            doc.Elements.Add("DGSBJT_CD", m_Info.DGSBJT_CD); //진료과목
            doc.Elements.Add("IFLD_DTL_SPC_SBJT_CD", m_Info.IFLD_DTL_SPC_SBJT_CD); //내과세부
            doc.Elements.Add("CHRG_DR_NN", m_Info.CHRG_DR_NM); //담당의사

            // B.진단정보
            doc.Tables.AddColumn("TBL_DIAG", "SICK_NM"); // 진단명
            doc.Tables.AddColumn("TBL_DIAG", "SICK_SYM"); // 상병코드
            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DIAG");
                doc.Tables["TBL_DIAG"].Rows[row]["SICK_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcSICK_SYM).ToString();
            }
            doc.Elements.Add("DLYS_KND_CD", m_Info.DLYS_KND_CD); // 투석종류

            // C.혈액투석

            // 1.혈액투석
            doc.Tables.AddColumn("TBL_BLDD", "BLDD_STA_DT"); // 시작일시
            doc.Tables.AddColumn("TBL_BLDD", "BLDD_END_DT"); // 종료일시
            doc.Tables.AddColumn("TBL_BLDD", "DLYS_BWGT"); // 건체중
            doc.Tables.AddColumn("TBL_BLDD", "BF_BWGT"); // 투석전 체중
            doc.Tables.AddColumn("TBL_BLDD", "AF_BWGT"); // 투석후체중
            doc.Tables.AddColumn("TBL_BLDD", "BLDVE_CH_CD"); // 혈관통로
            doc.Tables.AddColumn("TBL_BLDD", "CATH_TXT"); // 카테터내용
            doc.Tables.AddColumn("TBL_BLDD", "GL_WTR_DEL_QTY"); // 목표수분제거량
            doc.Tables.AddColumn("TBL_BLDD", "ATCG_ERYY_TXT"); // 항응고요법 초기
            doc.Tables.AddColumn("TBL_BLDD", "ATCG_MNTN_TXT"); // 항응고요법 유지
            doc.Tables.AddColumn("TBL_BLDD", "DLYS_DV_TXT"); // 투석기 
            doc.Tables.AddColumn("TBL_BLDD", "DLYS_LQD_TXT"); // 투석액
            doc.Tables.AddColumn("TBL_BLDD", "WRTP_NM"); // 적성자
            for (int row = 0; row < grdBlddView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_BLDD");
                doc.Tables["TBL_BLDD"].Rows[row]["BLDD_STA_DT"].Value = grdBlddView.GetRowCellValue(row, gcBLDD_STA_DT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["BLDD_END_DT"].Value = grdBlddView.GetRowCellValue(row, gcBLDD_END_DT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["DLYS_BWGT"].Value = grdBlddView.GetRowCellValue(row, gcDLYS_BWGT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["BF_BWGT"].Value = grdBlddView.GetRowCellValue(row, gcBF_BWGT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["AF_BWGT"].Value = grdBlddView.GetRowCellValue(row, gcAF_BWGT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["BLDVE_CH_CD"].Value = grdBlddView.GetRowCellValue(row, gcBLDVE_CH_CD).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["CATH_TXT"].Value = grdBlddView.GetRowCellValue(row, gcCATH_TXT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["GL_WTR_DEL_QTY"].Value = grdBlddView.GetRowCellValue(row, gcGL_WTR_DEL_QTY).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["ATCG_ERYY_TXT"].Value = grdBlddView.GetRowCellValue(row, gcATCG_ERYY_TXT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["ATCG_MNTN_TXT"].Value = grdBlddView.GetRowCellValue(row, gcATCG_MNTN_TXT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["DLYS_DV_TXT"].Value = grdBlddView.GetRowCellValue(row, gcDLYS_DV_TXT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["DLYS_LQD_TXT"].Value = grdBlddView.GetRowCellValue(row, gcDLYS_LQD_TXT).ToString();
                doc.Tables["TBL_BLDD"].Rows[row]["WRTP_NM"].Value = grdBlddView.GetRowCellValue(row, gcWRTP_NM).ToString();
            }

            // 1.혈액투석상세
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "MASR_DT"); // 측정일시
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "BPRSU"); // 혈압
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "PULS"); // 호흡
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "BRT"); // 맥박
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "TMPR"); // 체온
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "BLFL_RT"); // 혈류속도
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "ARTR_PRES"); // 동맥압
            doc.Tables.AddColumn("TBL_BLDD_VTSG", "VIN_PRES"); // 정맥압
            for (int row = 0; row < grdBlddView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_BLDD_VTSG");
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["MASR_DT"].Value = grdBlddView.GetRowCellValue(row, gcMASR_DT).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["BPRSU"].Value = grdBlddView.GetRowCellValue(row, gcBPRSU).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["PULS"].Value = grdBlddView.GetRowCellValue(row, gcPULS).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["BRT"].Value = grdBlddView.GetRowCellValue(row, gcBRT).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["TMPR"].Value = grdBlddView.GetRowCellValue(row, gcTMPR).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["BLFL_RT"].Value = grdBlddView.GetRowCellValue(row, gcBLFL_RT).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["ARTR_PRES"].Value = grdBlddView.GetRowCellValue(row, gcARTR_PRES).ToString();
                doc.Tables["TBL_BLDD_VTSG"].Rows[row]["VIN_PRES"].Value = grdBlddView.GetRowCellValue(row, gcVIN_PRES).ToString();
            }
            // D.지속적신대체요법

            // E.간호기록
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_DT"); // 기록일시
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_TXT"); // 간호기록
            doc.Tables.AddColumn("TBL_CARE_RCD", "NURSE_NM"); // 간호사
            for (int row = 0; row < grdBlddView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_CARE_RCD");
                doc.Tables["TBL_CARE_RCD"].Rows[row]["RCD_DT"].Value = grdRcdView.GetRowCellValue(row, gcRCD_DT).ToString();
                doc.Tables["TBL_CARE_RCD"].Rows[row]["RCD_TXT"].Value = grdRcdView.GetRowCellValue(row, gcRCD_TXT).ToString();
                doc.Tables["TBL_CARE_RCD"].Rows[row]["NURSE_NM"].Value = grdRcdView.GetRowCellValue(row, gcNURSE_NM).ToString();
            }

            // F.추가정보


            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("RNH001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

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
    public partial class RDD001 : Form
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

        private RDD001_Info m_Info = new RDD001_Info();

        public RDD001()
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

            list.Add(new CInfoTable("진료형태", ""));
            list.Add(new CInfoTable("입원일시", ""));

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
                SetRcd(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT BEDEDT, BEDEHM";
            sql += Environment.NewLine + "  FROM TA04";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT= '" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.BEDEDT = reader["BEDEDT"].ToString();
                m_Info.BEDEHM = reader["BEDEHM"].ToString();

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.IPAT_OPAT_TP_CD_NM); // 진료형태
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.IPAT_DT); // 입원일시

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetRcd(OleDbConnection p_conn)
        {
            List<RDD001_Rcd> list = new List<RDD001_Rcd>();
            grdRcd.DataSource = null;
            grdRcd.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V01.ODT, V01.OTM, V01.RMK, V01.DPTCD, V01.EXDRID, V01.ODIVCD";
            sql += Environment.NewLine + "     , V01A.OCD, V01A.OQTY, V01A.ORDCNT, V01A.FLDCD8, V01A.FLDCD4, V01A.OUNIT";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "     , A07.DRNM";
            sql += Environment.NewLine + "     , A18.ONM";
            sql += Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
            sql += Environment.NewLine + "                INNER JOIN TA09 A09 ON A09.DPTCD=V01.DPTCD";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=V01.EXDRID";
            sql += Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql += Environment.NewLine + " WHERE V01.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V01.BEDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND V01.ODIVCD NOT LIKE 'H%'";
            sql += Environment.NewLine + "   AND V01.DCFG='0'";
            sql += Environment.NewLine + " ORDER BY V01.ODT,V01.ONO,V01A.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RDD001_Rcd data = new RDD001_Rcd();

                data.ODT = reader["ODT"].ToString(); // 처방일
                data.OTM = reader["OTM"].ToString(); // 처방시간
                data.RMK = reader["RMK"].ToString(); // 비고
                data.DPTCD = reader["DPTCD"].ToString(); // 처방과
                data.INSDPTCD = reader["INSDPTCD"].ToString();
                data.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                data.EXDRID = reader["EXDRID"].ToString(); // 처방의
                data.EXDRNM = reader["DRNM"].ToString();
                data.ODIVCD = reader["ODIVCD"].ToString();

                data.OCD = reader["OCD"].ToString(); // 처방코드
                data.ONM = reader["ONM"].ToString();
                data.OQTY = reader["OQTY"].ToString(); // 용량
                data.ORDCNT = reader["ORDCNT"].ToString(); // 횟수
                data.FLDCD8 = reader["FLDCD8"].ToString(); // 일수
                data.FLDCD4 = reader["FLDCD4"].ToString(); // 용법
                data.OUNIT = reader["OUNIT"].ToString(); // 단위

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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RDD001");
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
            doc.Elements.Add("IPAT_OPAT_TP_CD", m_Info.IPAT_OPAT_TP_CD); // 진료형태
            doc.Elements.Add("IPAT_DT", m_Info.IPAT_DT); // 입원일시

            // B.의사지시기록
            // 2.진단
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "PRSC_DT"); // 처방일시
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "PRSC_TXT"); // 처방내역
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "RMK_TXT"); // 비고
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "DGSBJT_CD"); // 처방진료과
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "IFLD_DTL_SPC_SBJT_CD"); // 내과세부
            doc.Tables.AddColumn("TBL_DR_IDCT_RCD", "PRSC_DR_NM"); // 처방의

            for (int row = 0; row < grdRcdView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DR_IDCT_RCD");
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["PRSC_DT"].Value = grdRcdView.GetRowCellValue(row, gcPRSC_DT).ToString();
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["PRSC_TXT"].Value = grdRcdView.GetRowCellValue(row, gcPRSC_TXT).ToString();
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["RMK_TXT"].Value = grdRcdView.GetRowCellValue(row, gcRMK_TXT).ToString();
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["DGSBJT_CD"].Value = grdRcdView.GetRowCellValue(row, gcDGSBJT_CD).ToString();
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdRcdView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                doc.Tables["TBL_DR_IDCT_RCD"].Rows[row]["PRSC_DR_NM"].Value = grdRcdView.GetRowCellValue(row, gcPRSC_DR_NM).ToString();
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
                    CTI84A.SaveSend("RDD001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }
    }
}

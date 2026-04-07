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
    public partial class RSS001 : Form
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

        private RSS001_Info m_Info = new RSS001_Info();

        public RSS001()
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

            list.Add(new CInfoTable("수술 시작일시", ""));
            list.Add(new CInfoTable("수술 종료일시", ""));
            list.Add(new CInfoTable("수술의 구분", ""));
            list.Add(new CInfoTable("진료과", ""));
            list.Add(new CInfoTable("내과세부", ""));
            list.Add(new CInfoTable("수술의 성명", ""));
            list.Add(new CInfoTable("면허번호", ""));
            list.Add(new CInfoTable("작성자 성명", ""));
            list.Add(new CInfoTable("작성일시", ""));
            list.Add(new CInfoTable("응급여부", ""));
            list.Add(new CInfoTable("응급사유", ""));
            list.Add(new CInfoTable("마취종류", ""));
            list.Add(new CInfoTable("수술전 진단", ""));
            list.Add(new CInfoTable("수술후 진단", ""));
            list.Add(new CInfoTable("수술 체위", ""));
            list.Add(new CInfoTable("병변 위치", ""));
            list.Add(new CInfoTable("수술 소견", ""));
            list.Add(new CInfoTable("수술 절차", ""));
            list.Add(new CInfoTable("특이 사항", ""));

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
                SetMain(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            // 수술신청
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U01.PID, U01.OPDT, U01.DPTCD, U01.OPSEQ, U01.SEQ, U01.OPSDT, U01.OPSHR, U01.OPSMN, U01.OPEDT, U01.OPEHR, U01.OPEMN, U01.DRID, U01.STAFG ";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "     , A07.DRNM, A07.GDRLCID";
            sql += Environment.NewLine + "  FROM TU01 U01 INNER JOIN TA09 A09 ON A09.DPTCD=U01.DPTCD ";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=U01.DRID";
            sql += Environment.NewLine + " WHERE U01.PID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND U01.OPSDT >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND U01.OPSDT <= '" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY U01.OPSDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.PID = reader["PID"].ToString();
                m_Info.OPDT_KEY = reader["OPDT"].ToString();
                m_Info.DPTCD = reader["DPTCD"].ToString(); // 수술과
                m_Info.OPSEQ = reader["OPSEQ"].ToString();
                m_Info.SEQ = reader["SEQ"].ToString();

                m_Info.OPSDT = reader["OPSDT"].ToString(); // 수술시작일자
                m_Info.OPSHR = reader["OPSHR"].ToString(); // 수술시작시간
                m_Info.OPSMN = reader["OPSMN"].ToString(); // 수술시작분
                m_Info.OPEDT = reader["OPEDT"].ToString(); // 수술종료일자
                m_Info.OPEHR = reader["OPEHR"].ToString(); // 수술종료시간
                m_Info.OPEMN = reader["OPEMN"].ToString(); // 수술종료분
                m_Info.DRID = reader["DRID"].ToString();   // 수술의
                m_Info.STAFG = reader["STAFG"].ToString(); // 응급여부(0.정규 1.초응급 2.중응급 3.응급)
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString(); // 수술과(심평원과)
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString(); // 수술과(내과세부)
                m_Info.DRNM = reader["DRNM"].ToString();   // 수술의명
                m_Info.GDRLCID = reader["GDRLCID"].ToString();   // 수술의면허번호

                return false;
            });

            if(m_Info.R_CNT<1) return;

            // 마취내역
            sql = "";
            sql += Environment.NewLine + "SELECT U03.ANETP";
            sql += Environment.NewLine + "     , A31.CDNM ANETPNM";
            sql += Environment.NewLine + "  FROM TU03 U03 INNER JOIN TA31 A31 ON A31.MST1CD='58' AND A31.MST2CD=U03.ANETP";
            sql += Environment.NewLine + " WHERE U03.PID = '" + m_Info.PID + "'";
            sql += Environment.NewLine + "   AND U03.OPDT = '" + m_Info.OPDT_KEY + "'";
            sql += Environment.NewLine + "   AND U03.DPTCD = '" + m_Info.DPTCD + "'";
            sql += Environment.NewLine + "   AND U03.OPSEQ = '" + m_Info.OPSEQ + "'";
            sql += Environment.NewLine + "   AND U03.SEQ = '" + m_Info.SEQ + "'";
            sql += Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.ANETP = reader["ANETP"].ToString(); // 마취내역 마취종류
                m_Info.ANTTPNM = reader["ANETPNM"].ToString(); // 마취내역 마취종류 명칭

                return false;
            });

            // 수술기록지
            sql = "";
            sql += Environment.NewLine + "SELECT U90.EMPID";
            sql += Environment.NewLine + "     , U90.OPDT";
            sql += Environment.NewLine + "     , U90.WTIME";
            sql += Environment.NewLine + "     , U90.PREDX";
            sql += Environment.NewLine + "     , U90.POSDX";
            sql += Environment.NewLine + "     , U90.POS";
            sql += Environment.NewLine + "     , U90.INDIOFSURGERY";
            sql += Environment.NewLine + "     , U90.SURFNDNPRO";
            sql += Environment.NewLine + "     , A07.DRNM EMPNM";
            sql += Environment.NewLine + "  FROM TU90 U90 INNER JOIN TA07 A07 ON A07.DRID=U90.DRID";
            sql += Environment.NewLine + " WHERE U90.PID = '" + m_Info.PID + "'";
            sql += Environment.NewLine + "   AND U90.OPDT = '" + m_Info.OPDT_KEY + "'";
            sql += Environment.NewLine + "   AND U90.DPTCD = '" + m_Info.DPTCD + "'";
            sql += Environment.NewLine + "   AND U90.OPSEQ = '" + m_Info.OPSEQ + "'";
            sql += Environment.NewLine + "   AND U90.SEQ = '" + m_Info.SEQ + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {

                m_Info.EMPID = reader["EMPID"].ToString(); // 수술기록지 작성자
                m_Info.EMPNM = reader["EMPNM"].ToString(); // 수술기록지 작성자명
                m_Info.OPDT = reader["OPDT"].ToString(); // 수술기록지 작성일자
                m_Info.WTIME = reader["WTIME"].ToString(); // 수술기록지 작성시간
                m_Info.PREDX = reader["PREDX"].ToString(); // 수술기록지 수술전진단
                m_Info.POSDX = reader["POSDX"].ToString(); // 수술기록지 수술후진단
                m_Info.POS = reader["POS"].ToString(); // 수술기록지 수술체외
                m_Info.INDIOFSURGERY = reader["INDIOFSURGERY"].ToString(); // 수술기록지 수술소견
                m_Info.SURFNDNPRO = reader["SURFNDNPRO"].ToString(); // 수술기록지 수술절차

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.SOPR_STA_DT_NM); // 수술시작일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.SOPR_END_DT_NM); // 수술종료일시
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.SOPR_DR_CD_NM); // 수술의 구분
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.SOPR_DGSBJT_CD); // 진료과
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.IFLD_DTL_SPC_SBJT_CD); //내과세부
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.SOPR_DR_NM); // 수술의 성명
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.SOPR_DR_LCS_NO); // 면허번호
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.WRTP_NM); // 작성자성명
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.WRT_DT_NM); // 작성일시
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.EMY_YN_NM);// 응급여부
            grdInfoView.SetRowCellValue(10, gcCONTENT, m_Info.EMY_RS_TXT); //응급사유
            grdInfoView.SetRowCellValue(11, gcCONTENT, m_Info.NCT_KND_TXT); // 마취종류
            grdInfoView.SetRowCellValue(12, gcCONTENT, m_Info.SOPR_BF_DIAG_NM); // 수술전 진단
            grdInfoView.SetRowCellValue(13, gcCONTENT, m_Info.SOPR_AF_DIAG_NM); // 수술후 진단
            grdInfoView.SetRowCellValue(14, gcCONTENT, m_Info.SOPR_PHSQ_TXT); // 수술체위
            grdInfoView.SetRowCellValue(15, gcCONTENT, m_Info.LSN_LOCA_TXT); // 병병의 위치
            grdInfoView.SetRowCellValue(16, gcCONTENT, m_Info.SOPR_RST_TXT); // 수술 소견
            grdInfoView.SetRowCellValue(17, gcCONTENT, m_Info.SOPR_PROC_TXT); // 수술 절차
            grdInfoView.SetRowCellValue(18, gcCONTENT, m_Info.SOPR_SPCL_TXT); // 특이 사항

            RefreshGrid(grdInfo, grdInfoView);

        }

        private void SetMain(OleDbConnection p_conn)
        {
            List<RSS001_Data> list = new List<RSS001_Data>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 수술비
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U02.OCD, A18.ONM, A18.PRICD, A02.ISPCD";
            sql += Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + " WHERE U02.PID = '" + m_Info.PID + "'";
            sql += Environment.NewLine + "   AND U02.BEDEDT = '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U02.OPDT = '" + m_Info.OPDT_KEY + "'";
            sql += Environment.NewLine + "   AND U02.DPTCD = '" + m_Info.DPTCD + "'";
            sql += Environment.NewLine + "   AND U02.OPSEQ = '" + m_Info.OPSEQ + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RSS001_Data data = new RSS001_Data();

                data.OCD = reader["OCD"].ToString(); // 수술비 수술처방코드
                data.ONM = reader["ONM"].ToString(); // 수술비 수술처방명
                data.PRICD = reader["PRICD"].ToString(); // 수술비 수술수가코드
                data.ISPCD = reader["ISPCD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdMain, grdMainView);

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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RSS001");
            doc.Metadata.Add("FOM_VER", "002");
            doc.Metadata.Add("YKIHO", m_ykiho);
            doc.Metadata.Add("DMD_NO", m_dmd_no); // 청구번호

            doc.Metadata.Add("RCV_NO", m_rcv_no); // 접수번호
            doc.Metadata.Add("RCV_YR", m_rcv_yr); // 접수년도
            doc.Metadata.Add("BILL_SNO", m_bill_sno); // 청구서일련번호
            doc.Metadata.Add("SP_SNO", m_sp_sno.PadLeft(5,'0')); // 명세서 일련번호
            doc.Metadata.Add("INSUP_TP_CD", m_insup_tp_cd); // 보험자구분코드
            doc.Metadata.Add("FOM_REF_BIZ_TP_CD", "06"); // 업무구분코드
            doc.Metadata.Add("DTL_BIZ_CD", "NDM"); // 업무상세코드
            doc.Metadata.Add("REQ_DATA_NO", m_req_data_no); // 요청번호

            doc.Metadata.Add("HOSP_RNO", m_pid);
            doc.Metadata.Add("PAT_NM", m_pnm);
            doc.Metadata.Add("PAT_JNO", m_resid);

            // A.기본정보
            doc.Elements.Add("SOPR_STA_DT", m_Info.SOPR_STA_DT); //수술 시작일시
            doc.Elements.Add("SOPR_END_DT", m_Info.SOPR_END_DT); //수술 종료일시

            // 수술의사
            doc.Tables.AddColumn("TBL_SOPR_DR", "SOPR_DR_CD"); // 구분
            doc.Tables.AddColumn("TBL_SOPR_DR", "SOPR_DGSBJT_CD"); // 진료과
            doc.Tables.AddColumn("TBL_SOPR_DR", "IFLD_DTL_SPC_SBJT_CD"); // 내과세부전문과목
            doc.Tables.AddColumn("TBL_SOPR_DR", "SOPR_DR_NM"); // 성명
            doc.Tables.AddColumn("TBL_SOPR_DR", "SOPR_DR_LCS_NO"); // 면허번호

            doc.Tables.AddRow("TBL_SOPR_DR");
            doc.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DR_CD"].Value = m_Info.SOPR_DR_CD;
            doc.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DGSBJT_CD"].Value = m_Info.SOPR_DGSBJT_CD;
            doc.Tables["TBL_SOPR_DR"].Rows[0]["IFLD_DTL_SPC_SBJT_CD"].Value = m_Info.IFLD_DTL_SPC_SBJT_CD;
            doc.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DR_NM"].Value = m_Info.SOPR_DR_NM;
            doc.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DR_LCS_NO"].Value = m_Info.SOPR_DR_LCS_NO;

            doc.Elements.Add("WRTP_NM", m_Info.WRTP_NM); // 작성자 성명
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일자


            // B.수술정보
            doc.Elements.Add("EMY_YN", m_Info.EMY_YN); // 응급여부
            doc.Elements.Add("EMY_RS_TXT", m_Info.EMY_RS_TXT); // 응급사유
            doc.Elements.Add("NCT_KND_TXT", m_Info.NCT_KND_TXT); // 마취종류

            // 수술전진단
            doc.Tables.AddColumn("TBL_SOPR_BF_DIAG", "SOPR_BF_DIAG_NM"); // 진단명
            doc.Tables.AddRow("TBL_SOPR_BF_DIAG");
            doc.Tables["TBL_SOPR_BF_DIAG"].Rows[0]["SOPR_BF_DIAG_NM"].Value = m_Info.SOPR_BF_DIAG_NM;


            // 수술후진단
            doc.Tables.AddColumn("TBL_SOPR_AF_DIAG", "SOPR_AF_DIAG_NM"); // 진단명
            doc.Tables.AddRow("TBL_SOPR_AF_DIAG");
            doc.Tables["TBL_SOPR_AF_DIAG"].Rows[0]["SOPR_AF_DIAG_NM"].Value = m_Info.SOPR_AF_DIAG_NM;

            // 수술명
            doc.Tables.AddColumn("TBL_SOPR_NM", "SOPR_NM"); // 수술명
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_NM");
                doc.Tables["TBL_SOPR_NM"].Rows[row]["SOPR_NM"].Value = grdMainView.GetRowCellValue(row, gcSOPR_NM).ToString();
            }

            // 수술수가코드
            doc.Tables.AddColumn("TBL_MDFEE_CD", "MDFEE_CD"); // 수술코드
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_MDFEE_CD");
                doc.Tables["TBL_MDFEE_CD"].Rows[row]["MDFEE_CD"].Value = grdMainView.GetRowCellValue(row, gcMDFEE_CD).ToString();
            }

            doc.Elements.Add("SOPR_PHSQ_TXT", m_Info.SOPR_PHSQ_TXT); // 수술체위
            doc.Elements.Add("LSN_LOCA_TXT", m_Info.LSN_LOCA_TXT); // 병변의 위치
            doc.Elements.Add("SOPR_RST_TXT", m_Info.SOPR_RST_TXT); // 수술소견
            doc.Elements.Add("SOPR_PROC_TXT", m_Info.SOPR_PROC_TXT); // 수술절차
            doc.Elements.Add("SOPR_SPCL_TXT", m_Info.SOPR_SPCL_TXT); // 중요(특이)사항

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
                    CTI84A.SaveSend("RSS001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

        private void grdInfoView_MouseDown(object sender, MouseEventArgs e)
        {
            var hitInfo = grdInfoView.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int rowHandle = hitInfo.RowHandle;
                    DevExpress.XtraGrid.Columns.GridColumn column = hitInfo.Column;
                    string value = grdInfoView.GetRowCellValue(rowHandle, column).ToString();
                    txtView.Text = value;
                    //string[] value_arr = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                }
            }
        }

        private void grdMainView_MouseDown(object sender, MouseEventArgs e)
        {
            var hitInfo = grdMainView.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int rowHandle = hitInfo.RowHandle;
                    DevExpress.XtraGrid.Columns.GridColumn column = hitInfo.Column;
                    string value = grdMainView.GetRowCellValue(rowHandle, column).ToString();
                    txtView.Text = value;
                }
            }
        }

    }
}

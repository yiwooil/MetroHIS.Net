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
    public partial class RCC001 : Form
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

        private RCC001_Info m_Info = new RCC001_Info();

        public RCC001()
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

            list.Add(new CInfoTable("의뢰일시", ""));
            list.Add(new CInfoTable("의뢰내용", ""));
            list.Add(new CInfoTable("의뢰진료과", ""));
            list.Add(new CInfoTable("내과세부", ""));
            list.Add(new CInfoTable("의뢰의사", ""));
            list.Add(new CInfoTable("회신일시", ""));
            list.Add(new CInfoTable("회신내용", ""));
            list.Add(new CInfoTable("회신진료과", ""));
            list.Add(new CInfoTable("내과세부", ""));
            list.Add(new CInfoTable("회신의사", ""));

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
            sql += Environment.NewLine + "SELECT U07.ODT,U07.REQDPTCD,U07.REQDID,U07.REPLYDT,U07.REPLY,U07.CSTDPTCD,U07.CSTDRID";
            sql += Environment.NewLine + "     , A09_REQ.INSDPTCD REQINSDPTCD, A09_REQ.INSDPTCD2 REQINSDPTCD2";
            sql += Environment.NewLine + "     , A09_CST.INSDPTCD CSTINSDPTCD, A09_CST.INSDPTCD2 CSTINSDPTCD2";
            sql += Environment.NewLine + "     , A07_REQ.DRNM REQDNM";
            sql += Environment.NewLine + "     , A07_CST.DRNM CSTDRNM";
            sql += Environment.NewLine + "     , V01A.FLDCD9";
            sql += Environment.NewLine + "  FROM TU07 U07 INNER JOIN TA09 A09_REQ ON A09_REQ.DPTCD=U07.REQDPTCD";
            sql += Environment.NewLine + "                INNER JOIN TA09 A09_CST ON A09_CST.DPTCD=U07.CSTDPTCD";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07_REQ ON A07_REQ.DRID=U07.REQDID";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07_CST ON A07_CST.DRID=U07.CSTDRID";
            sql += Environment.NewLine + "                INNER JOIN TV01 V01 ON V01.PID=U07.PID AND V01.BEDEDT=U07.BEDEDT AND V01.BDIV=U07.BDIV AND V01.ODT=U07.ODT AND V01.ONO=U07.ONO";
            sql += Environment.NewLine + "                INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
            sql += Environment.NewLine + " WHERE U07.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U07.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U07.ODT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND U07.ODT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY U07.ODT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.ODT = reader["ODT"].ToString(); // 의뢰일자
                m_Info.FLDCD9 = reader["FLDCD9"].ToString(); // 의뢰내용
                m_Info.REQDPTCD = reader["REQDPTCD"].ToString(); // 의뢰과
                m_Info.REQINSDPTCD = reader["REQINSDPTCD"].ToString();
                m_Info.REQINSDPTCD2 = reader["REQINSDPTCD2"].ToString();
                m_Info.REQDID = reader["REQDID"].ToString(); // 의뢰의사
                m_Info.REQDNM = reader["REQDNM"].ToString();
                m_Info.REPLYDT = reader["REPLYDT"].ToString(); // 회신일자
                m_Info.REPLY = reader["REPLY"].ToString(); // 회신내용
                m_Info.CSTDPTCD = reader["CSTDPTCD"].ToString(); // 회신과
                m_Info.CSTINSDPTCD = reader["CSTINSDPTCD"].ToString();
                m_Info.CSTINSDPTCD2 = reader["CSTINSDPTCD2"].ToString();
                m_Info.CSTDRID = reader["CSTDRID"].ToString(); // 회신의사
                m_Info.CSTDRNM = reader["CSTDRNM"].ToString();

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.REQ_DT); // 의뢰일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.REQ_TXT); // 의뢰내용
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.REQ_DGSBJT_CD); // 의뢰진료과
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.REQ_IFLD_DTL_SPC_SBJT_CD); // 내과세부
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.REQ_DR_NM); // 의뢰의사
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.RPY_DT); // 회신일시
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.RPY_TXT); // 회신내용
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.RPY_DGSBJT_CD); // 회신진료과
            grdInfoView.SetRowCellValue(8, gcCONTENT, m_Info.RPY_IFLD_DTL_SPC_SBJT_CD); // 내과세부
            grdInfoView.SetRowCellValue(9, gcCONTENT, m_Info.RPY_DR_NM); // 회신의사

            RefreshGridInfo();
        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RCC001_Diag> list = new List<RCC001_Diag>();
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
                RCC001_Diag data = new RCC001_Diag();
                data.Clear();
                data.PTYSQ = reader["PTYSQ"].ToString();
                data.ROFG = reader["ROFG"].ToString();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();
                list.Add(data);

                return true;
            });

            if (list.Count < 1)
            {
                RCC001_Diag data = new RCC001_Diag();
                data.Clear();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RCC001");
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

            // A.의뢰
            doc.Elements.Add("REQ_DT", m_Info.REQ_DT); // 의뢰일시


            // 2.진단
            doc.Tables.AddColumn("TBL_REQ_DIAG", "SICK_TP_CD"); // 상병분류구분
            doc.Tables.AddColumn("TBL_REQ_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_REQ_DIAG", "SICK_SYM"); // 상병분류기호

            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_REQ_DIAG");
                doc.Tables["TBL_REQ_DIAG"].Rows[row]["SICK_TP_CD"].Value = grdDiagView.GetRowCellValue(row, gcSICK_TP_CD).ToString();
                doc.Tables["TBL_REQ_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_REQ_DIAG"].Rows[row]["SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcSICK_SYM).ToString().Replace(".","");
            }


            doc.Elements.Add("REQ_TXT", m_Info.REQ_TXT); // 의뢰내용
            doc.Elements.Add("REQ_DGSBJT_CD", m_Info.REQ_DGSBJT_CD); // 의뢰과
            doc.Elements.Add("REQ_IFLD_DTL_SPC_SBJT_CD", m_Info.REQ_IFLD_DTL_SPC_SBJT_CD); // 내과세부
            doc.Elements.Add("REQ_DR_NM", m_Info.REQ_DR_NM); // 의뢰의사

            // B.회신
            doc.Elements.Add("RPY_DT", m_Info.RPY_DT); // 회신일시
            doc.Elements.Add("RPY_TXT", m_Info.RPY_TXT); // 회신내용
            doc.Elements.Add("RPY_DGSBJT_CD", m_Info.RPY_DGSBJT_CD); // 회신과
            doc.Elements.Add("RPY_IFLD_DTL_SPC_SBJT_CD", m_Info.RPY_IFLD_DTL_SPC_SBJT_CD); // 내과세부
            doc.Elements.Add("RPY_DR_NM", m_Info.RPY_DR_NM); // 회신의사


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
                    CTI84A.SaveSend("RCC001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

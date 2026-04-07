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
    public partial class RNO001 : Form
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

        private RNO001_Info m_Info = new RNO001_Info();

        public RNO001()
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

            list.Add(new CInfoTable("진료과", ""));
            list.Add(new CInfoTable("내과세부", ""));

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
                SetAsm(conn);
                SetRcd(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A04.DPTCD, A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TA04 A04 INNER JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += Environment.NewLine + " WHERE A04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND A04.BEDEDT= '" + m_bededt + "'";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.DPTCD = reader["DPTCD"].ToString();
                m_Info.INSDPTCD = reader["INSDPTCD"].ToString();
                m_Info.INSDPTCD2 = reader["INSDPTCD2"].ToString();

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.DGSBJT_CD); // 진료과
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.IFLD_DTL_SPC_SBJT_CD); // 내과세부

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetAsm(OleDbConnection p_conn)
        {
            List<RNO001_Asm> list = new List<RNO001_Asm>();
            grdAsm.DataSource = null;
            grdAsm.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            // 낙상
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE, WTIME, JUM";
            sql += Environment.NewLine + "  FROM AUTH005";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNO001_Asm data = new RNO001_Asm();
                data.TYPE = 0;
                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = reader["WTIME"].ToString();
                data.TOOL = "";
                data.JUM = reader["JUM"].ToString();

                list.Add(data);

                return true;
            });

            // 통증
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE, WTIME, TOOL, PART, POW, CONDITION, TERM, FREQUENCY, REMARK, DRUG, NODRUG";
            sql += Environment.NewLine + "  FROM AUTH004";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE, WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNO001_Asm data = new RNO001_Asm();
                data.TYPE = 1;
                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = reader["WTIME"].ToString();
                data.TOOL = reader["TOOL"].ToString();
                data.PART = reader["PART"].ToString(); // 부위
                data.POW = reader["POW"].ToString(); // 강도
                data.CONDITION = reader["CONDITION"].ToString(); // 양상
                data.TERM = reader["TERM"].ToString(); // 기간
                data.FREQUENCY = reader["FREQUENCY"].ToString(); // 빈도
                data.REMARK = reader["REMARK"].ToString(); // 평가구분
                data.DRUG = reader["DRUG"].ToString(); // 약물중재
                data.NODRUG = reader["NODRUG"].ToString(); // 비약물중재

                list.Add(data);

                return true;
            });

            // 욕창
            sql = "";
            sql += Environment.NewLine + "SELECT WDATE";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BodySt,CHAR(21),1) BodySt_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BodySt,CHAR(21),2) BodySt_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BodySt,CHAR(21),3) BodySt_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(BodySt,CHAR(21),4) BodySt_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MindSt,CHAR(21),1) MindSt_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MindSt,CHAR(21),2) MindSt_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MindSt,CHAR(21),3) MindSt_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MindSt,CHAR(21),4) MindSt_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(ActiveSt,CHAR(21),1) ActiveSt_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(ActiveSt,CHAR(21),2) ActiveSt_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(ActiveSt,CHAR(21),4) ActiveSt_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(ActiveSt,CHAR(21),5) ActiveSt_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MoveSt,CHAR(21),1) MoveSt_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MoveSt,CHAR(21),2) MoveSt_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MoveSt,CHAR(21),3) MoveSt_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(MoveSt,CHAR(21),4) MoveSt_4";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(InCtncSt,CHAR(21),1) InCtncSt_1";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(InCtncSt,CHAR(21),2) InCtncSt_2";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(InCtncSt,CHAR(21),3) InCtncSt_3";
            sql += Environment.NewLine + "     , dbo.MFN_PIECE(InCtncSt,CHAR(21),4) InCtncSt_4";
            sql += Environment.NewLine + "  FROM TV40";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";
            sql += Environment.NewLine + " ORDER BY WDATE";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNO001_Asm data = new RNO001_Asm();
                data.TYPE = 1;
                data.WDATE = reader["WDATE"].ToString();
                data.WTIME = "0000";
                data.TOOL = "";
                data.BodySt_1 = reader["BodySt_1"].ToString(); // 4점
                data.BodySt_2 = reader["BodySt_2"].ToString(); // 3점
                data.BodySt_3 = reader["BodySt_3"].ToString(); // 2점
                data.BodySt_4 = reader["BodySt_4"].ToString(); // 1점
                data.MindSt_1 = reader["MindSt_1"].ToString(); // 4점
                data.MindSt_2 = reader["MindSt_2"].ToString(); // 3점
                data.MindSt_3 = reader["MindSt_3"].ToString(); // 2점
                data.MindSt_4 = reader["MindSt_4"].ToString(); // 1점
                data.ActiveSt_1 = reader["ActiveSt_1"].ToString(); // 4점
                data.ActiveSt_2 = reader["ActiveSt_1"].ToString(); // 3점
                data.ActiveSt_3 = reader["ActiveSt_3"].ToString(); // 2점
                data.ActiveSt_4 = reader["ActiveSt_4"].ToString(); // 1점
                data.MoveSt_1 = reader["MoveSt_1"].ToString(); // 4점
                data.MoveSt_2 = reader["MoveSt_2"].ToString(); // 3점
                data.MoveSt_3 = reader["MoveSt_3"].ToString(); // 2점
                data.MoveSt_4 = reader["MoveSt_4"].ToString(); // 1점
                data.InCtncSt_1 = reader["InCtncSt_1"].ToString(); // 4점
                data.InCtncSt_2 = reader["InCtncSt_2"].ToString(); // 3점
                data.InCtncSt_3 = reader["InCtncSt_3"].ToString(); // 2점
                data.InCtncSt_4 = reader["InCtncSt_4"].ToString(); // 1점

                list.Add(data);

                return true;
            });

            RefreshGrid(grdAsm, grdAsmView);
        }

        private void SetRcd(OleDbConnection p_conn)
        {
            List<RNO001_Rcd> list = new List<RNO001_Rcd>();
            grdRcd.DataSource = null;
            grdRcd.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V92.WDATE, V92.WTIME, V92.RESULT, V92.PNURES";
            sql += Environment.NewLine + "     , A13.EMPNM";
            sql += Environment.NewLine + "  FROM TV92 V92 INNER JOIN VA13 A13 ON A13.EMPID=V92.PNURES";
            sql += Environment.NewLine + " WHERE V92.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V92.BEDEDT= '" + m_bededt + "'";
            sql += Environment.NewLine + " ORDER BY V92.WDATE, V92.WTIME";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string result = reader["RESULT"].ToString();
                string[] arr = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int idx = 0; idx < arr.Length; idx++)
                {
                    RNO001_Rcd data = new RNO001_Rcd();
                    if (idx == 0)
                    {
                        data.WDATE = reader["WDATE"].ToString();
                        data.WTIME = reader["WTIME"].ToString();
                        data.RESULT = arr[idx]; // reader["RESULT"].ToString();
                        data.RESULT_ORG = reader["RESULT"].ToString();
                        data.PNURES = reader["PNURES"].ToString();
                        data.EMPNM = reader["EMPNM"].ToString();
                    }
                    else
                    {
                        data.WDATE = "";
                        data.WTIME = "";
                        data.RESULT = arr[idx]; // reader["RESULT"].ToString();
                        data.RESULT_ORG = "";
                        data.PNURES = "";
                        data.EMPNM = "";
                    }

                    list.Add(data);
                }

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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RNO001");
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
            doc.Elements.Add("DGSBJT_CD", m_Info.DGSBJT_CD); // 진료과
            doc.Elements.Add("IFLD_DTL_SPC_SBJT_CD", m_Info.IFLD_DTL_SPC_SBJT_CD); // 내과세부

            // B.간호사정 및 평가
            if (grdAsmView.RowCount > 0)
            {
                doc.Tables.AddColumn("TBL_CARE_ASM", "EXEC_DT"); // 기록일시
                doc.Tables.AddColumn("TBL_CARE_ASM", "ASM_TL_NM"); // 간호기록
                doc.Tables.AddColumn("TBL_CARE_ASM", "ASM_RST_TXT"); // 간호사 성명

                for (int row = 0; row < grdAsmView.RowCount; row++)
                {
                    doc.Tables.AddRow("TBL_CARE_ASM");
                    doc.Tables["TBL_CARE_ASM"].Rows[row]["EXEC_DT"].Value = grdAsmView.GetRowCellValue(row, gcEXEC_DT).ToString();
                    doc.Tables["TBL_CARE_ASM"].Rows[row]["ASM_TL_NM"].Value = grdAsmView.GetRowCellValue(row, gcASM_TL_NM).ToString();
                    doc.Tables["TBL_CARE_ASM"].Rows[row]["ASM_RST_TXT"].Value = grdAsmView.GetRowCellValue(row, gcASM_RST_TXT).ToString();
                }
            }

            // C.간호기록
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_DT"); // 기록일시
            doc.Tables.AddColumn("TBL_CARE_RCD", "RCD_TXT"); // 간호기록
            doc.Tables.AddColumn("TBL_CARE_RCD", "NURSE_NM"); // 간호사 성명
            int ins_row = 0;
            for (int row = 0; row < grdRcdView.RowCount; row++)
            {
                string result_org = grdRcdView.GetRowCellValue(row, gcRCD_TXT_ORG).ToString();
                if (result_org != "")
                {
                    doc.Tables.AddRow("TBL_CARE_RCD");
                    doc.Tables["TBL_CARE_RCD"].Rows[ins_row]["RCD_DT"].Value = grdRcdView.GetRowCellValue(row, gcRCD_DT).ToString();
                    doc.Tables["TBL_CARE_RCD"].Rows[ins_row]["RCD_TXT"].Value = grdRcdView.GetRowCellValue(row, gcRCD_TXT_ORG).ToString();
                    doc.Tables["TBL_CARE_RCD"].Rows[ins_row]["NURSE_NM"].Value = grdRcdView.GetRowCellValue(row, gcNURSE_NM).ToString();
                    ins_row++;
                }
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
                    CTI84A.SaveSend("RNO001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }


    }
}

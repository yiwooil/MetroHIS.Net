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
    public partial class ERR001 : Form
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

        public ERR001()
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

            if (p_IsTest) btnSubmit.Enabled = false;

            btnQuery.PerformClick();
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
            List<ERR001_Data> list = new List<ERR001_Data>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT X01.ODT,X01.ONO,X01.ACPTNO,X01.ACPTDT,X01.ACPTTM,x01.PHTDT,x01.PHTTM,X01.OCD,X01.OSTSCD ";
            sql += Environment.NewLine + "     , X01.DPTCD,X01.WARDID,X01.RMID,X01.BEDID,X01.RPTPTFG ";
            sql += Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2 ";
            sql += Environment.NewLine + "     , A18.ONM";
            sql += Environment.NewLine + "     , A07.DRNM EXDRNM";
            sql += Environment.NewLine + "     , X01.RPTDT,X01.RPTTM,X01.RPTNO";
            sql += Environment.NewLine + "     , X01.RDRID1";
            sql += Environment.NewLine + "     , A071.DRNM RDRNM1,A071.GDRLCID GDRLCID1";
            sql += Environment.NewLine + "	   , x02.RPTXT ";
            sql += Environment.NewLine + "	   , x02.RESULT ";
            sql += Environment.NewLine + "	   , x02.REC ";
            sql += Environment.NewLine + "  FROM TX01 X01 LEFT  JOIN TA07 A07  ON A07.DRID = X01.EXDRID ";
            sql += Environment.NewLine + "                LEFT  JOIN TA07 A071 ON A071.DRID = X01.RDRID1 ";
            sql += Environment.NewLine + "                LEFT  JOIN TA09 A09  ON A09.DPTCD = X01.DPTCD ";
            sql += Environment.NewLine + "                INNER JOIN TA18 A18  ON A18.OCD = X01.OCD";
            sql += Environment.NewLine + "                                    AND A18.CREDT = (SELECT MAX(Z.CREDT)";
            sql += Environment.NewLine + "                                                       FROM TA18 Z";
            sql += Environment.NewLine + "                                                      WHERE Z.OCD = X01.OCD";
            sql += Environment.NewLine + "                                                        AND Z.CREDT <= X01.ODT";
            sql += Environment.NewLine + "                                                    )";
            sql += Environment.NewLine + "		          LEFT  JOIN TX02 x02 ON x02.RPTDT = x01.RPTDT AND x02.RPTNO = x01.RPTNO ";
            sql += Environment.NewLine + " WHERE X01.PID = '" + m_pid + "' ";
            sql += Environment.NewLine + "   AND X01.ODT >= '" + m_frdt + "' ";
            sql += Environment.NewLine + "   AND X01.ODT <= '" + m_todt + "' ";
            sql += Environment.NewLine + "   AND ISNULL(X01.OSTSCD,'') <> '0' ";
            sql += Environment.NewLine + "   AND ISNULL(X01.OSTSCD,'') >= '5' ";
            sql += Environment.NewLine + " ORDER BY X01.ODT DESC ";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string rptxt = reader["RPTXT"].ToString();
                    string result = reader["RESULT"].ToString();
                    string rec = reader["REC"].ToString();
                    string rptxt_result_rec = rptxt;
                    if (result != "") rptxt_result_rec += "\r\n" + result;
                    if (rec != "") rptxt_result_rec += "\r\n" + rec;

                    string[] arr = rptxt_result_rec.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    for (int idx = 0; idx < arr.Length; idx++)
                    {
                        ERR001_Data data = new ERR001_Data();
                        data.Clear();

                        if (idx == 0)
                        {
                            data.ODT = reader["ODT"].ToString();
                            data.ONO = reader["ONO"].ToString();
                            data.ACPTNO = reader["ACPTNO"].ToString();
                            data.ACPTDT = reader["ACPTDT"].ToString();
                            data.ACPTTM = reader["ACPTTM"].ToString();
                            data.PHTDT = reader["PHTDT"].ToString();
                            data.PHTTM = reader["PHTTM"].ToString();
                            data.OCD = reader["OCD"].ToString();
                            data.OSTSCD = reader["OSTSCD"].ToString();
                            data.DPTCD = reader["DPTCD"].ToString();
                            data.INSDPTCD = reader["INSDPTCD"].ToString();
                            data.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                            data.WARDID = reader["WARDID"].ToString();
                            data.RMID = reader["RMID"].ToString();
                            data.BEDID = reader["BEDID"].ToString();
                            data.RPTPTFG = reader["RPTPTFG"].ToString();
                            data.ONM = reader["ONM"].ToString();
                            data.EXDRNM = reader["EXDRNM"].ToString();
                            data.RPTDT = reader["RPTDT"].ToString();
                            data.RPTTM = reader["RPTTM"].ToString();
                            data.RPTNO = reader["RPTNO"].ToString();
                            data.RDRID1 = reader["RDRID1"].ToString();
                            data.RDRNM1 = reader["RDRNM1"].ToString();
                            data.GDRLCID1 = reader["GDRLCID1"].ToString();
                            data.RPTXT_RESULT_REC_ORG = rptxt_result_rec;
                        }
                        data.RPTXT_RESULT_REC = arr[idx];
                        list.Add(data);
                    }
                    
                });
                conn.Close();
            }

            RefreshGridMain();
        }

        private void GetDataReader(string p_sql, OleDbConnection p_conn, Action<OleDbDataReader> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p_callback(reader);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
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

        private void RefreshGridMain()
        {
            if (grdMain.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdMain.BeginInvoke(new Action(() => grdMainView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdMainView.RefreshData();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "ERR001");
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

            // A.검사 정보 및 결과

            doc.Tables.AddColumn("TBL_EXM_RST", "DGSBJT_CD"); // 진료과
            doc.Tables.AddColumn("TBL_EXM_RST", "IFLD_DTL_SPC_SBJT_CD");
            doc.Tables.AddColumn("TBL_EXM_RST", "PRSC_DR_NM");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_PRSC_DT");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_EXEC_DT");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_RST_DT");
            doc.Tables.AddColumn("TBL_EXM_RST", "DCT_DR_NM");
            doc.Tables.AddColumn("TBL_EXM_RST", "DCT_DR_LCS_NO");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_MDFEE_CD");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_CD");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_NM");
            doc.Tables.AddColumn("TBL_EXM_RST", "EXM_RST_TXT");
            int ins_row = 0;
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                string exm_rst_txt_org = grdMainView.GetRowCellValue(row, gcEXM_RST_TXT_ORG).ToString();
                if (exm_rst_txt_org != "")
                {
                    doc.Tables.AddRow("TBL_EXM_RST");
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["DGSBJT_CD"].Value = grdMainView.GetRowCellValue(row, gcDGSBJT_CD).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdMainView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["PRSC_DR_NM"].Value = grdMainView.GetRowCellValue(row, gcPRSC_DR_NM).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_PRSC_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_PRSC_DT).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_EXEC_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_EXEC_DT).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_RST_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_RST_DT).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["DCT_DR_NM"].Value = grdMainView.GetRowCellValue(row, gcDCT_DR_NM).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["DCT_DR_LCS_NO"].Value = grdMainView.GetRowCellValue(row, gcDCT_DR_LCS_NO).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_MDFEE_CD"].Value = grdMainView.GetRowCellValue(row, gcEXM_MDFEE_CD).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_CD"].Value = grdMainView.GetRowCellValue(row, gcEXM_CD).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_NM"].Value = grdMainView.GetRowCellValue(row, gcEXM_NM).ToString();
                    doc.Tables["TBL_EXM_RST"].Rows[ins_row]["EXM_RST_TXT"].Value = exm_rst_txt_org;
                    ins_row++;
                }
            }

            // B.추가정보

            // 서식추가
            doc.addDoc();

            // 기재점검
            if (CSubmitDocument.CheckDocument(doc) == false) return;

            // 제출
            if (p_isSending)
            {
                if (CSubmitDocument.SubmitDocument(doc) == true)
                {
                    CTI84A.SaveSend("ERR001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

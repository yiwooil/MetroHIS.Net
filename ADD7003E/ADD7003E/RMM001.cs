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
    public partial class RMM001 : Form
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

        public RMM001()
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
            List<RMM001_Data> list = new List<RMM001_Data>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V20.ODT, V20.OCD, V20.DQTY, V20.DUNIT, V20.ORDCNT, 1 DDAY, V20.DSTSCD, '' RSN, V20.DODT, V20.DOHR, V20.DOMN, V20.RMK";
            sql += Environment.NewLine + "     , A18.PRICD, A18.ONM";
            sql += Environment.NewLine + "     , V01.DPTCD, V01A.FLDCD4";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TV20 V20 INNER JOIN TA18 A18 ON A18.OCD=V20.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V20.OCD AND X.CREDT<=V20.ODT)";
            sql += Environment.NewLine + "                INNER JOIN TV01 V01 ON V01.PID=V20.PID AND V01.BEDEDT=V20.BEDEDT AND V01.BDIV=V20.BDIV AND V01.ODT=V20.ODT AND V01.ONO=V20.ONO";
            sql += Environment.NewLine + "			      INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID AND V01A.OCD=V20.OCD";
            sql += Environment.NewLine + "			      INNER JOIN TA09 A09 ON A09.DPTCD=V01.DPTCD";
            sql += Environment.NewLine + " WHERE V20.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V20.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND V20.OCD LIKE 'M%'";
            sql += Environment.NewLine + "   AND V20.DQTY<>0";
            sql += Environment.NewLine + " ORDER BY V20.ODT,V20.NO";

            //Action<OleDbDataReader> act = delegate(OleDbDataReader reader) { };

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    if (reader["DSTSCD"].ToString() == "Y")
                    {
                        RMM001_Data data = new RMM001_Data();
                        data.ODT = reader["ODT"].ToString();
                        data.OCD = reader["OCD"].ToString();
                        data.PRICD = reader["PRICD"].ToString();
                        data.ISPCD = GetIspcd(data.PRICD, data.ODT, conn);
                        data.ONM = reader["ONM"].ToString(); ;
                        data.FLDCD4 = reader["FLDCD4"].ToString(); // 투여경로
                        data.DQTY = reader["DQTY"].ToString();
                        data.DUNIT = reader["DUNIT"].ToString();
                        data.ORDCNT = reader["ORDCNT"].ToString();
                        data.DDAY = reader["DDAY"].ToString();
                        data.DSTSCD = reader["DSTSCD"].ToString();
                        data.RSN = reader["RSN"].ToString(); // 미실시사유
                        data.DODT = reader["DODT"].ToString();
                        data.DOHR = reader["DOHR"].ToString();
                        data.DOMN = reader["DOMN"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.INSDPTCD = reader["INSDPTCD"].ToString();
                        data.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                        data.RMK = reader["RMK"].ToString(); // 비고
                        list.Add(data);
                    }
                });
                conn.Close();
            }

            RefreshGridMain();
        }

        private string GetIspcd(string p_pricd, string p_exdt, OleDbConnection p_conn)
        {
            string ispcd = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A02.ISPCD";
            sql += Environment.NewLine + "  FROM TA02 A02";
            sql += Environment.NewLine + " WHERE A02.PRICD='" + p_pricd + "'";
            sql += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT)";
            sql += Environment.NewLine + "                    FROM TA02 X";
            sql += Environment.NewLine + "                   WHERE X.PRICD=A02.PRICD";
            sql += Environment.NewLine + "                     AND X.CREDT<='" + p_exdt + "'";
            sql += Environment.NewLine + "                 )";
            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ispcd = reader["ISPCD"].ToString();
            });
            return ispcd;
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RMM001");
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
            doc.Elements.Add("IPAT_OPAT_TP_CD", "1"); // 진료형태(1.입원 2.외래)

            // B.투약정보
            doc.Tables.AddColumn("TBL_MDS_PRSC", "PRSC_DD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "PRSC_DIV_CD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "MDS_CD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "MDS_NM");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "INJC_PTH_TXT");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "FQ1_MDCT_QTY");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "MDS_UNIT_TXT");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "DY1_INJC_FQ");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "TOT_INJC_DDCNT");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "INJC_EXEC_CD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "NEXEC_RS_TXT");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "EXEC_DT");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "DGSBJT_CD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "IFLD_DTL_SPC_SBJT_CD");
            doc.Tables.AddColumn("TBL_MDS_PRSC", "RMK_TXT");

            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_MDS_PRSC");
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["PRSC_DD"].Value = grdMainView.GetRowCellValue(row, gcPRSC_DD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["PRSC_DIV_CD"].Value = grdMainView.GetRowCellValue(row, gcPRSC_DIV_CD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["MDS_CD"].Value = grdMainView.GetRowCellValue(row, gcMDS_CD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["MDS_NM"].Value = grdMainView.GetRowCellValue(row, gcMDS_NM).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["INJC_PTH_TXT"].Value = grdMainView.GetRowCellValue(row, gcINJC_PTH_TXT).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["FQ1_MDCT_QTY"].Value = grdMainView.GetRowCellValue(row, gcFQ1_MDCT_QTY).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["MDS_UNIT_TXT"].Value = grdMainView.GetRowCellValue(row, gcMDS_UNIT_TXT).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["DY1_INJC_FQ"].Value = grdMainView.GetRowCellValue(row, gcDY1_INJC_FQ).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["TOT_INJC_DDCNT"].Value = grdMainView.GetRowCellValue(row, gcTOT_INJC_DDCNT).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["INJC_EXEC_CD"].Value = grdMainView.GetRowCellValue(row, gcINJC_EXEC_CD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["NEXEC_RS_TXT"].Value = grdMainView.GetRowCellValue(row, gcNEXEC_RS_TXT).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["EXEC_DT"].Value = grdMainView.GetRowCellValue(row, gcEXEC_DT).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["DGSBJT_CD"].Value = grdMainView.GetRowCellValue(row, gcDGSBJT_CD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdMainView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                doc.Tables["TBL_MDS_PRSC"].Rows[row]["RMK_TXT"].Value = grdMainView.GetRowCellValue(row, gcRMK_TXT).ToString();
            }

            // C.항암화학요법
            doc.Elements.Add("ANCR_TRET_YN", "2"); // 항암치료여부

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
                    CTI84A.SaveSend("RMM001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
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


    }
}

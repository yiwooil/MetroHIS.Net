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
    public partial class ERD001 : Form
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

        public ERD001()
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
            List<ERD001_Data> list = new List<ERD001_Data>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT DISTINCT A.SPCNO, A.SEX, A.AGE, A.RCVDT, A.RCVTM, A.STSCD, A.SPCFOOTSEQ, A.PTHRPT, A.DIAGNOSIS, A.ORDDT, A.SPCNM, A.MAJNM, A.BLOODDT, A.BLOODTM, A.AGEDIV, A.DEPTCD, A.MAJDR ";
            sql += Environment.NewLine + "     , B.INSDPTCD, B.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TC201 A INNER JOIN TA09 B ON B.DPTCD=A.DEPTCD";
            sql += Environment.NewLine + " WHERE A.PTID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.ORDDT >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.ORDDT <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND (A.CANCELFG != '1' OR A.CANCELFG IS NULL) ";
            sql += Environment.NewLine + "   AND A.STSCD >= 1";
            sql += Environment.NewLine + "   AND A.ACCDIV = '0' ";
            sql += Environment.NewLine + "   AND LEFT(A.DEPTCD, 2) != 'ER' ";
            sql += Environment.NewLine + " ORDER BY A.ORDDT, A.SPCNO";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                CSQLHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    SpcNoResult(reader, conn, list);
                    return true;
                });
                conn.Close();
            }

            RefreshGridMain();
        }

        private void SpcNoResult(OleDbDataReader p_reader, OleDbConnection p_conn, List<ERD001_Data> p_list)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.SPCNO, A.TESTCD, A.SEQ, A.APPDT, A.APPTM, A.TESTDIV, A.TESTRSTTYPE, A.HEADTESTCD ";
            sql += Environment.NewLine + "     , A.SITECD, A.PTID, A.RSTFG, A.WSCD, A.SPCCD, A.RSTVAL, A.REFERCHK, A.PANICCHK ";
            sql += Environment.NewLine + "     , A.DELTACHK, A.PICKCD, A.MICRORSTTYPE, A.STAINSEQ, A.CULTURESEQ, A.FOOTSEQ, A.MODIFYFG ";
            sql += Environment.NewLine + "     , A.DLYFG, A.STATFG, A.MANSTATFG, A.EQUIPCD, A.VFYID, A.VFYDT, A.VFYTM, A.PRTFG, A.PRTDT ";
            sql += Environment.NewLine + "     , A.PRTTM, A.STSCD, A.CANCELFG, A.CANCELCD, A.REGDR, A.SPECDR, B.ABBRNM ";
            sql += Environment.NewLine + "     , B.DATATYPE, B.DATALEN, B.KEYPAD, B.RSTFG, B.NORSTFG, B.QUERYFG, B.NORSTQUERYFG, B.ONOFFFG, B.WEEKDAYDIV ";
            sql += Environment.NewLine + "     , '" + p_reader["SEX"].ToString() + "' AS SEX";
            sql += Environment.NewLine + "     , '" + p_reader["AGE"].ToString() + "' AS AGE";
            sql += Environment.NewLine + "     , '" + p_reader["RCVDT"].ToString() + "' AS RCVDT";
            sql += Environment.NewLine + "     , '" + p_reader["RCVTM"].ToString() + "' AS RCVTM";
            sql += Environment.NewLine + "     , '" + p_reader["STSCD"].ToString() + "' AS STSCD";
            sql += Environment.NewLine + "     , '" + p_reader["SPCFOOTSEQ"].ToString() + "' AS SPCFOOTSEQ";
            sql += Environment.NewLine + "     , '" + p_reader["PTHRPT"].ToString() + "' AS PTHRPT";
            sql += Environment.NewLine + "     , '" + p_reader["DIAGNOSIS"].ToString() + "' AS DIAGNOSIS";
            sql += Environment.NewLine + "     , '" + p_reader["ORDDT"].ToString() + "' AS ORDDT";
            sql += Environment.NewLine + "     , '" + p_reader["SPCNM"].ToString() + "' AS SPCNM";
            sql += Environment.NewLine + "     , '" + p_reader["MAJNM"].ToString() + "' AS MAJNM";
            sql += Environment.NewLine + "     , '" + p_reader["BLOODDT"].ToString() + "' AS BLOODDT";
            sql += Environment.NewLine + "     , '" + p_reader["BLOODTM"].ToString() + "' AS BLOODTM";
            sql += Environment.NewLine + "     , '" + p_reader["AGEDIV"].ToString() + "' AS AGEDIV";
            sql += Environment.NewLine + "     , '" + p_reader["DEPTCD"].ToString() + "' AS DEPTCD";
            sql += Environment.NewLine + "     , '" + p_reader["MAJDR"].ToString() + "' AS MAJDR";
            sql += Environment.NewLine + "     , '" + p_reader["INSDPTCD"].ToString() + "' AS INSDPTCD";
            sql += Environment.NewLine + "     , '" + p_reader["INSDPTCD2"].ToString() + "' AS INSDPTCD2";
            sql += Environment.NewLine + "  FROM TC301 A, TC001 B ";
            sql += Environment.NewLine + " WHERE A.SPCNO = '" + p_reader["SPCNO"].ToString() + "'";
            sql += Environment.NewLine + "   AND A.STSCD >= '7'";
            sql += Environment.NewLine + "   AND (A.CANCELFG != '1' OR A.CANCELFG IS NULL) ";
            sql += Environment.NewLine + "   AND B.TESTCD = A.TESTCD ";
            sql += Environment.NewLine + "   AND B.APPDT = A.APPDT ";
            sql += Environment.NewLine + "   AND B.APPTM = A.APPTM ";
            sql += Environment.NewLine + "   AND ((B.RSTFG = '1') OR ((RTRIM(A.RSTVAL) != '' OR A.FOOTSEQ > 0 OR A.STAINSEQ > 0 OR A.CULTURESEQ > 0) AND B.QUERYFG = '1') OR (B.NORSTQUERYFG = '1')) ";
            sql += Environment.NewLine + " ORDER BY A.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ERD001_Data data = new ERD001_Data();
                data.ODT = reader["ORDDT"].ToString();
                data.BLOODDT = reader["BLOODDT"].ToString();
                data.BLOODTM = reader["BLOODTM"].ToString();
                data.RCVDT = reader["RCVDT"].ToString();
                data.RCVTM = reader["RCVTM"].ToString();
                data.VFYDT = reader["VFYDT"].ToString();
                data.VFYTM = reader["VFYTM"].ToString();
                data.SPCNM = reader["SPCNM"].ToString();
                data.DEPTCD = reader["DEPTCD"].ToString();
                data.MAJDR = reader["MAJDR"].ToString();
                data.MAJNM = reader["MAJNM"].ToString();
                data.SPCNO = reader["SPCNO"].ToString();
                data.STSCD = reader["STSCD"].ToString();
                data.ABBRNM = reader["ABBRNM"].ToString();
                data.TESTCD = reader["TESTCD"].ToString();
                data.REFERCHK = reader["REFERCHK"].ToString();
                data.PANICCHK = reader["PANICCHK"].ToString();
                data.DELTACHK = reader["DELTACHK"].ToString();
                data.INSDPTCD = reader["INSDPTCD"].ToString();
                data.INSDPTCD2 = reader["INSDPTCD2"].ToString();
                // 결과
                if (reader["STSCD"].ToString().CompareTo("7") >= 0)
                {
                    data.RSTVAL = reader["RSTVAL"].ToString();
                }
                else
                {
                    data.RSTVAL = "";
                }
                // 단위
                data.UNIT = GetUnit(reader, p_conn);
                // 참고치
                data.REFERENCE = GetReference(reader, p_conn);
                // 검사결과가 미생물 stain 결과이고, 미생물 stain 결과가 있다면
                int stainSeq = 0;
                int.TryParse(reader["STAINSEQ"].ToString(), out stainSeq);
                if (reader["TESTRSTTYPE"].ToString() == "1" && stainSeq > 0 && reader["STSCD"].ToString().CompareTo("7") >= 0)
                {
                    data.RSTVAL += GetTC305Result(reader, p_conn);
                }
                // 검사결과가 미생물 culture 결과이고, 미생물 culture 결과가 있다면
                int cultureSeq = 0;
                int.TryParse(reader["CULTURESEQ"].ToString(), out cultureSeq);
                if (reader["TESTRSTTYPE"].ToString() == "1" && cultureSeq > 0 && reader["STSCD"].ToString().CompareTo("5") >= 0)
                {
                    data.RSTVAL += GetTC306Result(reader, p_conn);
                }
                // FOOT NOTE
                int footSeq = 0;
                int.TryParse(reader["FOOTSEQ"].ToString(), out footSeq);
                if (reader["STSCD"].ToString().CompareTo("7") >= 0 && footSeq > 0)
                {
                    data.RSTVAL += GetTC302Footnote(reader, p_conn);
                }


                p_list.Add(data);

                return true;
            });
        }

        private string GetUnit(OleDbDataReader p_reader, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT TESTCD, EQUIPCD, SPCCD, APPDT, APPTM, EQUIPSEQ, SPCSEQ, AUTOVERIFY, ";
            sql += Environment.NewLine + "       TUBECD, VOLCD, UNITCD, BARCNT, TURNDAY, TURNTIME, MORFG, PANICFG, PANICFR, ";
            sql += Environment.NewLine + "       PANICTO, DELTAFG, DELTATYPE, DELTAFR, DELTATO, DELDT ";
            sql += Environment.NewLine + "  FROM TC002 ";
            sql += Environment.NewLine + " WHERE TESTCD = '" + p_reader["TESTCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND APPDT = '" + p_reader["APPDT"].ToString() + "'";
            sql += Environment.NewLine + "   AND APPTM = '" + p_reader["APPTM"].ToString() + "'";
            if (p_reader["EQUIPCD"].ToString() != "")
            {
                sql += Environment.NewLine + "   AND EQUIPCD = '" + p_reader["EQUIPCD"].ToString() + "'";
            }
            if (p_reader["SPCCD"].ToString() != "")
            {
                sql += Environment.NewLine + "   AND SPCCD = '" + p_reader["SPCCD"].ToString() + "'";
            }
            sql += Environment.NewLine + " ORDER BY EQUIPSEQ, SPCSEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = GetBaseFieldNm(reader["UNITCD"].ToString(), p_conn);
                return true;
            });
            return ret;
        }

        private string GetBaseFieldNm(string p_unitcd, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT FIELD1 FROM TC032 WHERE CDDIV = 'C207' AND CDVAL1 = '" + p_unitcd + "'";
            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FIELD1"].ToString();
                return true;
            });
            return ret;
        }

        private string GetReference(OleDbDataReader p_reader, OleDbConnection p_conn)
        {
            string ret="";

            string age = p_reader["AGE"].ToString();
            string agediv = p_reader["AGEDIV"].ToString();
            string blooddt = p_reader["BLOODDT"].ToString();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT TESTCD, EQUIPCD, SPCCD, APPDT, APPTM, SEX, AGEFR, AGETO, AGEDIV, ";
            sql += Environment.NewLine + "       REFERFR, SIGNFR, SIGNTO, REFERTO, REFER, SEQ, DELDT ";
            sql += Environment.NewLine + "  FROM TC004 ";
            sql += Environment.NewLine + " WHERE TESTCD = '" + p_reader["TESTCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND EQUIPCD = '" + p_reader["EQUIPCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND SPCCD = '" + p_reader["SPCCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND APPDT = '" + p_reader["APPDT"].ToString() + "'";
            sql += Environment.NewLine + "   AND APPTM = '" + p_reader["APPTM"].ToString() + "'";
            sql += Environment.NewLine + "   AND SEX IN ('" + p_reader["SEX"].ToString() + "', 'A') ";
            sql += Environment.NewLine + "   AND ((CASE '" + agediv + "'";
            sql += Environment.NewLine + "              WHEN 'Y'";
            sql += Environment.NewLine + "              THEN CASE WHEN REPLACE(CONVERT(VARCHAR(5), CAST('" + blooddt + "' AS DATETIME) - CAST('" + age + "' AS INT), 10), '-', '') > SUBSTRING('" + blooddt + "', 5, 4)";
            sql += Environment.NewLine + "                        THEN (DATEDIFF(YYYY, CAST('" + blooddt + "' AS DATETIME) - CAST('" + age + "' AS INT), CAST('" + blooddt + "' AS DATETIME)) - 1) * 365";
            sql += Environment.NewLine + "                        ELSE DATEDIFF(YYYY, CAST('" + blooddt + "' AS DATETIME) - CAST('" + age + "' AS INT), CAST('" + blooddt + "' AS DATETIME)) * 365";
            sql += Environment.NewLine + "                   END ";
            sql += Environment.NewLine + "              WHEN 'M'";
            sql += Environment.NewLine + "              THEN DATEDIFF(MM, CAST('" + blooddt + "' AS DATETIME) - CAST('" + age + "' AS INT), CAST('" + blooddt + "' AS DATETIME)) * 30";
            sql += Environment.NewLine + "              WHEN 'D' ";
            sql += Environment.NewLine + "              THEN '" + age + "'";
            sql += Environment.NewLine + "         END BETWEEN AGEFR AND AGETO)";
            sql += Environment.NewLine + "        OR";
            sql += Environment.NewLine + "        (RTRIM(AGEFR) = '' AND RTRIM(AGETO) = '')";
            sql += Environment.NewLine + "       )";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string referfr = reader["REFERFR"].ToString();
                string referto = reader["REFERTO"].ToString();

                if (referfr != "" && referto != "")
                {
                    ret = referfr + " - " + referto;
                }
                else if (referfr != "" && referto == "")
                {
                    ret = referfr;
                }
                else if (referfr == "" && referto != "")
                {
                    ret = referto;
                }
                return true;
            });
            return ret;
        }

        private string GetTC305Result(OleDbDataReader p_reader, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql="";
            sql += Environment.NewLine + "SELECT SPCNO, TESTCD, SEQ, STAINSEQ, STAINVALSEQ, STAINVAL ";
            sql += Environment.NewLine + "  FROM TC305 ";
            sql += Environment.NewLine + " WHERE SPCNO = '" + p_reader["SPCNO"].ToString() + "'";
            sql += Environment.NewLine + "   AND TESTCD = '" + p_reader["TESTCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + p_reader["SEQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND STAINSEQ = '" + p_reader["STAINSEQ"].ToString() + "'";
            sql += Environment.NewLine + " ORDER BY STAINVALSEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                if (ret == "") ret = reader["STAINVAL"].ToString();
                else ret += " " + reader["STAINVAL"].ToString();
                return true;
            });
            return ret;
        }

        private string GetTC306Result(OleDbDataReader p_reader, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT SPCNO, TESTCD, SEQ, CULTURESEQ, WEEKDAYFG, WEEKDAY, CULTUREVAL, STSDIV ";
            sql += Environment.NewLine + "  FROM TC306 ";
            sql += Environment.NewLine + " WHERE SPCNO = '" + p_reader["SPCNO"].ToString() + "'";
            sql += Environment.NewLine + "   AND TESTCD = '" + p_reader["TESTCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + p_reader["SEQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND CULTURESEQ = '" + p_reader["CULTURESEQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND STSDIV != '0' ";
            sql += Environment.NewLine + " ORDER BY STSDIV DESC, WEEKDAY DESC";


            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                if (ret == "") ret = reader["CULTUREVAL"].ToString();
                else ret += " " + reader["CULTUREVAL"].ToString();
                return true;
            });
            return ret;
        }

        private string GetTC302Footnote(OleDbDataReader p_reader, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT SPCNO, TESTCD, SEQ, FOOTSEQ, FOOTNOTE ";
            sql += Environment.NewLine + "  FROM TC302 ";
            sql += Environment.NewLine + " WHERE SPCNO = '" + p_reader["SPCNO"].ToString() + "'";
            sql += Environment.NewLine + "   AND TESTCD = '" + p_reader["TESTCD"].ToString() + "'";
            if (p_reader["SEQ"].ToString() != "")
            {
                sql += Environment.NewLine + "   AND SEQ = '" + p_reader["SEQ"].ToString() + "'";
            }
            if (p_reader["FOOTSEQ"].ToString() != "")
            {
                sql += Environment.NewLine + "   AND FOOTSEQ = '" + p_reader["FOOTSEQ"].ToString() + "'";
            }
            sql += Environment.NewLine + "ORDER BY FOOTSEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                if (ret == "") ret = reader["FOOTNOTE"].ToString();
                else ret += " " + reader["FOOTNOTE"].ToString();
                return true;
            });
            return ret;
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "ERD001");
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
            if (grdMainView.RowCount > 0)
            {
                doc.Elements.Add("DGSBJT_CD", grdMainView.GetRowCellValue(0, gcDGSBJT_CD).ToString());
                doc.Elements.Add("IFLD_DTL_SPC_SBJT_CD", grdMainView.GetRowCellValue(0, gcIFLD_DTL_SPC_SBJT_CD).ToString());
                doc.Elements.Add("PRSC_DR_NM", grdMainView.GetRowCellValue(0, gcPRSC_DR_NM).ToString());
            }

            // B.Text형 검사 결과
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_PRSC_DT"); // 처방일시
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_GAT_DT"); // 채취일시
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_RCV_DT"); // 접수일시
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_RST_DT"); // 결과일시
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_SPCM_CD"); // 검체종류
            //doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_SPCM_ETC_TXT"); // 검체상세
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_MDFEE_CD"); // 수가코드(EDI코드)
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_CD"); // 검사코드(병원에서 부여한 코드)
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_NM"); // 검사명
            doc.Tables.AddColumn("TBL_TXT_EXM", "EXM_RST_TXT"); // 판독결과
            doc.Tables.AddColumn("TBL_TXT_EXM", "DCT_DR_NM"); // 판독의사
            //doc.Tables.AddColumn("TBL_TXT_EXM", "DCT_DR_LCS_NO"); // 판독의사 면허번호

            doc.Tables.AddRow("TBL_TXT_EXM");
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_PRSC_DT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_GAT_DT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RCV_DT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_DT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_SPCM_CD"].Value = "-";
            //doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_SPCM_ETC_TXT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_CD"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_NM"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_TXT"].Value = "-";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["DCT_DR_NM"].Value = "-";
            //doc.Tables["TBL_TXT_EXM"].Rows[0]["DCT_DR_LCS_NO"].Value = "-";

            // C.Grid형 검사 결과
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_PRSC_DT"); // 처방일시
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_GAT_DT"); // 채취일시
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_RCV_DT"); // 접수일시
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_RST_DT"); // 결과일시
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_SPCM_CD"); // 검체종류
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_SPCM_ETC_TXT"); // 검체상세
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_MDFEE_CD"); // 수가코드(EDI코드)
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_CD"); // 검사코드(병원에서 부여한 코드)
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_NM"); // 검사명
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_RST_TXT"); // 검사결과
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_REF_TXT"); // 참고치
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_UNIT"); // 단위
            doc.Tables.AddColumn("TBL_GRID_EXM", "EXM_ADD_TXT"); // 추가정보

            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_GRID_EXM");
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_PRSC_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_PRSC_DT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_GAT_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_GAT_DT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_RCV_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_RCV_DT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_RST_DT"].Value = grdMainView.GetRowCellValue(row, gcEXM_RST_DT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_SPCM_CD"].Value = grdMainView.GetRowCellValue(row, gcEXM_SPCM_CD).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_SPCM_ETC_TXT"].Value = grdMainView.GetRowCellValue(row, gcEXM_SPCM_ETC_TXT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_MDFEE_CD"].Value = grdMainView.GetRowCellValue(row, gcEXM_MDFEE_CD).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_CD"].Value = grdMainView.GetRowCellValue(row, gcEXM_CD).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_NM"].Value = grdMainView.GetRowCellValue(row, gcEXM_NM).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_RST_TXT"].Value = grdMainView.GetRowCellValue(row, gcEXM_RST_TXT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_REF_TXT"].Value = grdMainView.GetRowCellValue(row, gcEXM_REF_TXT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_UNIT"].Value = grdMainView.GetRowCellValue(row, gcEXM_UNIT).ToString();
                doc.Tables["TBL_GRID_EXM"].Rows[row]["EXM_ADD_TXT"].Value = grdMainView.GetRowCellValue(row, gcEXM_ADD_TXT).ToString();
            }

            // D.기타정보

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
                    CTI84A.SaveSend("ERD001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }

        }

    }
}

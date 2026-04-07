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
    public partial class RIP001 : Form
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

        private RIP001_Info m_Info = new RIP001_Info();

        public RIP001()
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

            list.Add(new CInfoTable("최초입원일시", ""));
            list.Add(new CInfoTable("약물이상반응 여부", ""));
            list.Add(new CInfoTable("약물이상반응 내용", ""));

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
                SetSopr(conn);
                SetElaps(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.BEDEDT";
            sql += Environment.NewLine + "	   , CASE WHEN dbo.MFN_PIECE(A.Society2_Q3,CHAR(21),3)='1' THEN '1' ELSE '0' END AS Society2_Q3_fg";
            sql += Environment.NewLine + "	   , dbo.MFN_PIECE(A.Society2_Q3_ETC,CHAR(21),1) AS Society2_Q3_ETC_txt";
            sql += Environment.NewLine + "     , A04.BEDEHM";
            sql += Environment.NewLine + "  FROM TV95_10 A INNER JOIN TA04 A04 ON A04.PID=A.PID AND A04.BEDEDT=A.BEDEDT ";
            sql += Environment.NewLine + " WHERE A.PID='" + m_pid + "' ";
            sql += Environment.NewLine + "   AND A.BEDEDT >= '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.WDATE >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.WDATE <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.CHNGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY A.WDATE, A.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.BEDEDT = reader["BEDEDT"].ToString();
                m_Info.BEDEHM = reader["BEDEHM"].ToString();
                m_Info.Society2_Q3_fg = reader["Society2_Q3_fg"].ToString(); // 약물이상 반영여부(1.Yes 0.No)
                m_Info.Society2_Q3_ETC_txt = reader["Society2_Q3_ETC_txt"].ToString(); // 약물이상 반응내용

                return false;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.IPAT_DT); // 입원일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.ALRG_YN_NM); // 약물이상반응 여부
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.ALRG_TXT); // 약물이상 반응 내용

            RefreshGridInfo();
        }

        private void SetDiag(OleDbConnection p_conn)
        {
            List<RIP001_Diag> list = new List<RIP001_Diag>();
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
                RIP001_Diag data = new RIP001_Diag();
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
                RIP001_Diag data = new RIP001_Diag();
                data.PTYSQ = "";
                data.ROFG = "";
                data.DXD = "";
                data.DACD = "";
                data.ZCD10CD = "";
                list.Add(data);
            }

            RefreshGridDiag();
        }

        private void SetSopr(OleDbConnection p_conn)
        {
            List<RIP001_Sopr> list = new List<RIP001_Sopr>();
            grdSopr.DataSource = null;
            grdSopr.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT V01.ODT,V01.OTM,V01A.OCD,A18.ONM,A18.PRICD";
            sql += Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
            sql += Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql += Environment.NewLine + " WHERE V01.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND V01.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND V01.ODT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND V01.ODT<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND V01A.OCD LIKE 'T%'";
            sql += Environment.NewLine + "   AND V01.DCFG = '0'";
            sql += Environment.NewLine + "   AND V01.ODIVCD NOT LIKE 'H%'";
            sql += Environment.NewLine + "   AND V01.DCFG IN ('0')";
            sql += Environment.NewLine + " ORDER BY V01.ODT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RIP001_Sopr data = new RIP001_Sopr();
                data.ODT = reader["ODT"].ToString();
                data.OTM = reader["OTM"].ToString();
                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                list.Add(data);

                return true;
            });

            RefreshGridSopr();
        }

        private void SetElaps(OleDbConnection p_conn)
        {
            List<RIP001_Elaps> list = new List<RIP001_Elaps>();
            grdElaps.DataSource = null;
            grdElaps.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.EXDT, A.USERID, A.SYSDT, A.SYSTM, A.C_CASE, A.RMK1 ";
            sql += Environment.NewLine + "     , A07.DRNM USERNM, A07.DPTCD";
            sql += Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
            sql += Environment.NewLine + "  FROM TE12C A INNER JOIN TA07 A07 ON A07.DRID=A.USERID";
            sql += Environment.NewLine + "               INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += Environment.NewLine + " WHERE A.PID = '" + m_pid + "'";
            sql += Environment.NewLine + "   AND A.BEDEDT = '" + m_bededt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  >= '" + m_frdt + "'";
            sql += Environment.NewLine + "   AND A.EXDT  <= '" + m_todt + "'";
            sql += Environment.NewLine + "   AND A.C_CASE IN ('S','O','A','P')";
            sql += Environment.NewLine + " ORDER BY A.EXDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string exdt = reader["EXDT"].ToString();
                string userid = reader["USERID"].ToString();
                string usernm = reader["USERNM"].ToString();
                string sysdt = reader["SYSDT"].ToString();
                string systm = reader["SYSTM"].ToString();
                string dptcd = reader["DPTCD"].ToString();
                string insdptcd = reader["INSDPTCD"].ToString();
                string insdptcd2 = reader["INSDPTCD2"].ToString();
                string c_case = reader["C_CASE"].ToString();
                string rmk1 = reader["RMK1"].ToString();

                bool bFind = false;
                foreach (RIP001_Elaps data in list)
                {
                    if (exdt == data.EXDT &&
                        userid == data.USERID &&
                        usernm == data.USERNM &&
                        sysdt == data.SYSDT &&
                        systm == data.SYSTM &&
                        dptcd == data.DPTCD &&
                        insdptcd == data.INSDPTCD &&
                        insdptcd2 == data.INSDPTCD2)
                    {
                        if (c_case == "S" || c_case == "O" || c_case == "A")
                        {
                            data.E12C_SOA = rmk1 + "\r\n";
                        }
                        if (c_case == "P")
                        {
                            data.E12C_P = rmk1;
                        }
                        bFind = true;
                        break;
                    }
                }
                if (bFind == false)
                {

                    RIP001_Elaps data = new RIP001_Elaps();
                    data.Clear();
                    data.EXDT = exdt;
                    data.USERID = userid;
                    data.USERNM = usernm;
                    data.SYSDT = sysdt;
                    data.SYSTM = systm;
                    data.DPTCD = dptcd;
                    data.INSDPTCD = insdptcd;
                    data.INSDPTCD2 = insdptcd2;
                    if (c_case == "S" || c_case == "O" ||c_case == "A")
                    {
                        data.E12C_SOA = rmk1 + "\r\n";
                    }
                    if (c_case == "P")
                    {
                        data.E12C_P = rmk1;
                    }
                    data.IS_SEND_DATA = "Y";
                    list.Add(data);
                }
                return true;
            });


            List<RIP001_Elaps> list_new = new List<RIP001_Elaps>();
            grdElaps.DataSource = null;
            grdElaps.DataSource = list_new;

            foreach (RIP001_Elaps data in list)
            {
                string desc = "";
                if (data.PRBM_LIST_TXT != "")
                {
                    desc += "[정보및평가]\r\n" + data.PRBM_LIST_TXT + "\r\n";
                }
                if (data.TRET_PLAN_TXT != "")
                {
                    desc += "[치료계획]\r\n" + data.TRET_PLAN_TXT;
                }
                string[] arr = desc.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int idx = 0; idx < arr.Length; idx++)
                {
                    RIP001_Elaps data_new = new RIP001_Elaps();
                    data_new.Clear();
                    if (idx == 0)
                    {
                        data_new.EXDT = data.EXDT;
                        data_new.DPTCD = data.DPTCD;
                        data_new.INSDPTCD = data.INSDPTCD;
                        data_new.INSDPTCD2 = data.INSDPTCD2;
                        data_new.USERID = data.USERID;
                        data_new.USERNM = data.USERNM;
                        data_new.SYSDT = data.SYSDT;
                        data_new.SYSTM = data.SYSTM;
                        data_new.E12C_SOA = data.E12C_SOA;
                        data_new.E12C_P = data.E12C_P;
                        data_new.IS_SEND_DATA = "Y";
                    }
                    data_new.E12C_SOAP = arr[idx];
                    list_new.Add(data_new);                    
                }
            }

            RefreshGridElaps();
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

        private void RefreshGridSopr()
        {
            if (grdSopr.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSopr.BeginInvoke(new Action(() => grdSoprView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSoprView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridElaps()
        {
            if (grdElaps.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdElaps.BeginInvoke(new Action(() => grdElapsView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdElapsView.RefreshData();
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RIP001");
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
            doc.Elements.Add("IPAT_DT", m_Info.IPAT_DT); // 최초입원일시

            // B.진단및 치치

            // 1.진단
            doc.Tables.AddColumn("TBL_DIAG", "SICK_TP_CD"); // 상병분류구분
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_DIAG", "DIAG_SICK_SYM"); // 상병분류기호

            for (int row = 0; row < grdDiagView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_DIAG");
                doc.Tables["TBL_DIAG"].Rows[row]["SICK_TP_CD"].Value = grdDiagView.GetRowCellValue(row, gcSICK_TP_CD).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_NM).ToString();
                doc.Tables["TBL_DIAG"].Rows[row]["DIAG_SICK_SYM"].Value = grdDiagView.GetRowCellValue(row, gcDIAG_SICK_SYM).ToString().Replace(".","");
            }

            // 2.시술.처치 및 수술
            doc.Tables.AddColumn("TBL_MOPR_SOPR", "SOPR_ENFC_DT"); // 시행일시
            doc.Tables.AddColumn("TBL_MOPR_SOPR", "SOPR_NM"); // 처치명

            for (int row = 0; row < grdSoprView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_MOPR_SOPR");
                doc.Tables["TBL_MOPR_SOPR"].Rows[row]["SOPR_ENFC_DT"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_ENFC_DT).ToString();
                doc.Tables["TBL_MOPR_SOPR"].Rows[row]["SOPR_NM"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_NM).ToString();
            }

            // 3.약물이상반응
            doc.Elements.Add("ALRG_YN", m_Info.ALRG_YN); // 약물이상반응 여부
            doc.Elements.Add("ALRG_TXT", m_Info.ALRG_TXT); // 약물이상반응 내용

            // C.입원경과
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "DIAG_DD"); // 진료일자
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "DGSBJT_CD"); // 진료과목
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "IFLD_DTL_SPC_SBJT_CD"); // 내과세부
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "CHRG_DR_NM"); // 담당의 성명
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "WRTP_NM"); // 작성자성명
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "WRT_DT"); // 작성일시
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "PRBM_LIST_TXT"); // 정보 및 평고
            doc.Tables.AddColumn("TBL_IPAT_ELAPS", "TRET_PLAN_TXT"); // 평가
            int ins_row = 0;
            for (int row = 0; row < grdElapsView.RowCount; row++)
            {
                string is_send_data = grdElapsView.GetRowCellValue(row, gcIS_SEND_DATA).ToString();
                if (is_send_data == "Y" )
                {
                    doc.Tables.AddRow("TBL_IPAT_ELAPS");
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["DIAG_DD"].Value = grdElapsView.GetRowCellValue(row, gcDIAG_DD).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["DGSBJT_CD"].Value = grdElapsView.GetRowCellValue(row, gcDGSBJT_CD).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["IFLD_DTL_SPC_SBJT_CD"].Value = grdElapsView.GetRowCellValue(row, gcIFLD_DTL_SPC_SBJT_CD).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["CHRG_DR_NM"].Value = grdElapsView.GetRowCellValue(row, gcCHRG_DR_NM).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["WRTP_NM"].Value = grdElapsView.GetRowCellValue(row, gcWRTP_NM).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["WRT_DT"].Value = grdElapsView.GetRowCellValue(row, gcWRT_DT).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["PRBM_LIST_TXT"].Value = grdElapsView.GetRowCellValue(row, gcPRBM_LIST_TXT).ToString();
                    doc.Tables["TBL_IPAT_ELAPS"].Rows[ins_row]["TRET_PLAN_TXT"].Value = grdElapsView.GetRowCellValue(row, gcTRET_PLAN_TXT).ToString();
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
                    CTI84A.SaveSend("RIP001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

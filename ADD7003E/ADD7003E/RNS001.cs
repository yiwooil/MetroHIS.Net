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
    public partial class RNS001 : Form
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

        private RNS001_Info m_Info = new RNS001_Info();

        public RNS001()
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

            list.Add(new CInfoTable("수술실 입실일시", ""));
            list.Add(new CInfoTable("수술실 퇴실일시", ""));
            list.Add(new CInfoTable("수술 시작일시", ""));
            list.Add(new CInfoTable("수술 종료일시", ""));
            list.Add(new CInfoTable("소독 간호사", ""));
            list.Add(new CInfoTable("순회 간호사", ""));
            list.Add(new CInfoTable("작성일시", ""));
            list.Add(new CInfoTable("Time Out 여부", ""));

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
                SetDiagBf(conn);
                SetDiagAf(conn);
                SetSopr(conn);
                SetTmcat(conn);
                SetMds(conn);

                conn.Close();
            }
        }

        private void SetInfo(OleDbConnection p_conn)
        {
            m_Info.Clear();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U01.OPDT, U01.PTAHR, U01.PTAMN, U01.OPODT, U01.OPOHR, U01.OPOMN, U01.OPSDT, U01.OPSHR, U01.OPSMN, U01.OPEDT, U01.OPEHR, U01.OPEMN";
            sql += Environment.NewLine + "     , U01.SCNR1, U01.CRNR1";
            sql += Environment.NewLine + "     , A13_A.EMPNM SCNRNM1, A13_B.EMPNM CRNRNM1";
            sql += Environment.NewLine + "  FROM TU01 U01 LEFT JOIN VA13 A13_A ON A13_A.EMPID=SCNR1";
            sql += Environment.NewLine + "                LEFT JOIN VA13 A13_B ON A13_B.EMPID=CRNR1";
            sql += Environment.NewLine + " WHERE U01.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U01.OPDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND U01.OPDT<='" + m_todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U01.CANCL,'')<>'1'";
            sql += Environment.NewLine + " ORDER BY U01.OPDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.R_CNT++;

                m_Info.OPDT = reader["OPDT"].ToString();
                m_Info.PTAHR = reader["PTAHR"].ToString();
                m_Info.PTAMN = reader["PTAMN"].ToString();
                m_Info.OPODT = reader["OPODT"].ToString();
                m_Info.OPOHR = reader["OPOHR"].ToString();
                m_Info.OPOMN = reader["OPOMN"].ToString();
                m_Info.OPSDT = reader["OPSDT"].ToString();
                m_Info.OPSHR = reader["OPSHR"].ToString();
                m_Info.OPSMN = reader["OPSMN"].ToString();
                m_Info.OPEDT = reader["OPEDT"].ToString();
                m_Info.OPEHR = reader["OPEHR"].ToString();
                m_Info.OPEMN = reader["OPEMN"].ToString();
                m_Info.SCNR1 = reader["SCNR1"].ToString();
                m_Info.SCNRNM1 = reader["SCNRNM1"].ToString();
                m_Info.CRNR1 = reader["CRNR1"].ToString();
                m_Info.CRNRNM1 = reader["CRNRNM1"].ToString();

                return false;
            });

            if (m_Info.R_CNT < 1) return;

            sql = "";
            sql += Environment.NewLine + "SELECT OPDT";
            sql += Environment.NewLine + "  FROM EMR109_3";
            sql += Environment.NewLine + " WHERE PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND OPDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND OPDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY OPDT";

            m_Info.EMR109_3_CNT = 0;
            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_Info.EMR109_3_CNT++;

                return true;
            });

            grdInfoView.SetRowCellValue(0, gcCONTENT, m_Info.OPRM_IPAT_DT); // 수술실 입실일시
            grdInfoView.SetRowCellValue(1, gcCONTENT, m_Info.OPRM_DSCG_DT); // 수술실 퇴실일시
            grdInfoView.SetRowCellValue(2, gcCONTENT, m_Info.SOPR_STA_DT); // 수술 시작일시
            grdInfoView.SetRowCellValue(3, gcCONTENT, m_Info.SOPR_END_DT); // 수술 종료일시
            grdInfoView.SetRowCellValue(4, gcCONTENT, m_Info.DSFN_NURSE_NM); // 소독 간호사
            grdInfoView.SetRowCellValue(5, gcCONTENT, m_Info.CRCL_NURSE_NM); // 순회 간호사
            grdInfoView.SetRowCellValue(6, gcCONTENT, m_Info.WRT_DT); // 작성일시
            grdInfoView.SetRowCellValue(7, gcCONTENT, m_Info.PTNT_POSI_CFR_YN_NM); // Time Out 여부

            RefreshGrid(grdInfo, grdInfoView);
        }

        private void SetDiagBf(OleDbConnection p_conn)
        {
            // 수술전
            List<RNS001_Diag> list = new List<RNS001_Diag>();
            grdDiagBf.DataSource = null;
            grdDiagBf.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T05.DXD, T05.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TT05 T05 INNER JOIN TA16 A16 ON A16.DISECD=T05.DACD AND A16.DISEDIV=T05.DISEDIV";
            sql += Environment.NewLine + " WHERE T05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T05.BDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY T05.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNS001_Diag data = new RNS001_Diag();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdDiagBf, grdDiagBfView);
            
        }

        private void SetDiagAf(OleDbConnection p_conn)
        {
            // 수술후(수술전과 동일)
            List<RNS001_Diag> list = new List<RNS001_Diag>();
            grdDiagAf.DataSource = null;
            grdDiagAf.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT T05.DXD, T05.DACD, A16.ZCD10CD";
            sql += Environment.NewLine + "  FROM TT05 T05 INNER JOIN TA16 A16 ON A16.DISECD=T05.DACD AND A16.DISEDIV=T05.DISEDIV";
            sql += Environment.NewLine + " WHERE T05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND T05.BDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT>='" + m_frdt + "'";
            sql += Environment.NewLine + "   AND T05.EXDT<='" + m_todt + "'";
            sql += Environment.NewLine + " ORDER BY T05.SEQ";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNS001_Diag data = new RNS001_Diag();
                data.DXD = reader["DXD"].ToString();
                data.DACD = reader["DACD"].ToString();
                data.ZCD10CD = reader["ZCD10CD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdDiagAf, grdDiagAfView);
        }

        private void SetSopr(OleDbConnection p_conn)
        {
            List<RNS001_Sopr> list = new List<RNS001_Sopr>();
            grdSopr.DataSource = null;
            grdSopr.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U02.OCD,A18.ONM,A18.PRICD,A02.ISPCD";
            sql += Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
            sql += Environment.NewLine + " WHERE U02.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U02.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY U02.OPDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNS001_Sopr data = new RNS001_Sopr();
                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                data.ISPCD = reader["ISPCD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdSopr, grdSoprView);
        }

        private void SetTmcat(OleDbConnection p_conn)
        {
            List<RNS001_Tmcat> list = new List<RNS001_Tmcat>();
            grdTmcat.DataSource = null;
            grdTmcat.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U04.OCD,A18.ONM,A18.PRICD,A02.ISPCD";
            sql += Environment.NewLine + "     , ISNULL(U04.DQTY,0) + ISNULL(U04.NOQT,0) DQTY";
            sql += Environment.NewLine + "     , U04.OPDT, U04.DUNIT";
            sql += Environment.NewLine + "  FROM TU04 U04 INNER JOIN TA18 A18 ON A18.OCD=U04.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U04.OCD AND X.CREDT<=U04.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U04.OPDT)";
            sql += Environment.NewLine + " WHERE U04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U04.BDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U04.OCD LIKE 'G%'";
            sql += Environment.NewLine + "   AND ISNULL(U04.CHGDT,'')=''";
            sql += Environment.NewLine + " UNION ALL";
            sql += Environment.NewLine + "SELECT U05.OCD,A18.ONM,A18.PRICD,A02.ISPCD";
            sql += Environment.NewLine + "     , ISNULL(U05.IPQT,0)+ISNULL(U05.BBQT,0)+ISNULL(U05.USQT,0)+ISNULL(U05.NOQT,0)+ISNULL(U05.MJQT,0)+ISNULL(U05.HHQT,0)+ISNULL(U05.DQTY,0) DQTY";
            sql += Environment.NewLine + "     , U05.OPDT, U05.DUNIT";
            sql += Environment.NewLine + "  FROM TU05 U05 INNER JOIN TA18 A18 ON A18.OCD=U05.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U05.OCD AND X.CREDT<=U05.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U05.OPDT)";
            sql += Environment.NewLine + " WHERE U05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U05.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U05.OCD LIKE 'G%'";
            sql += Environment.NewLine + "   AND ISNULL(U05.CHGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY OPDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNS001_Tmcat data = new RNS001_Tmcat();
                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.DQTY = reader["DQTY"].ToString();
                data.DUNIT = reader["DUNIT"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                data.ISPCD = reader["ISPCD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdTmcat, grdTmcatView);
        }

        private void SetMds(OleDbConnection p_conn)
        {
            List<RNS001_Mds> list = new List<RNS001_Mds>();
            grdMds.DataSource = null;
            grdMds.DataSource = list;

            if (m_Info.R_CNT < 1) return;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT U04.OCD,A18.ONM,A18.PRICD,A02.ISPCD";
            sql += Environment.NewLine + "     , ISNULL(U04.DQTY,0) + ISNULL(U04.NOQT,0) DQTY";
            sql += Environment.NewLine + "     , U04.OPDT, U04.DUNIT";
            sql += Environment.NewLine + "  FROM TU04 U04 INNER JOIN TA18 A18 ON A18.OCD=U04.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U04.OCD AND X.CREDT<=U04.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U04.OPDT)";
            sql += Environment.NewLine + " WHERE U04.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U04.BDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U04.OCD LIKE 'M%'";
            sql += Environment.NewLine + "   AND ISNULL(U04.CHGDT,'')=''";
            sql += Environment.NewLine + " UNION ALL";
            sql += Environment.NewLine + "SELECT U05.OCD,A18.ONM,A18.PRICD,A02.ISPCD";
            sql += Environment.NewLine + "     , ISNULL(U05.IPQT,0)+ISNULL(U05.BBQT,0)+ISNULL(U05.USQT,0)+ISNULL(U05.NOQT,0)+ISNULL(U05.MJQT,0)+ISNULL(U05.HHQT,0)+ISNULL(U05.DQTY,0) DQTY";
            sql += Environment.NewLine + "     , U05.OPDT, U05.DUNIT";
            sql += Environment.NewLine + "  FROM TU05 U05 INNER JOIN TA18 A18 ON A18.OCD=U05.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U05.OCD AND X.CREDT<=U05.OPDT)";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U05.OPDT)";
            sql += Environment.NewLine + " WHERE U05.PID='" + m_pid + "'";
            sql += Environment.NewLine + "   AND U05.BEDEDT='" + m_bededt + "'";
            sql += Environment.NewLine + "   AND U05.OCD LIKE 'M%'";
            sql += Environment.NewLine + "   AND ISNULL(U05.CHGDT,'')=''";
            sql += Environment.NewLine + " ORDER BY OPDT";

            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                RNS001_Mds data = new RNS001_Mds();
                data.OCD = reader["OCD"].ToString();
                data.ONM = reader["ONM"].ToString();
                data.DQTY = reader["DQTY"].ToString();
                data.DUNIT = reader["DUNIT"].ToString();
                data.PRICD = reader["PRICD"].ToString();
                data.ISPCD = reader["ISPCD"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGrid(grdMds, grdMdsView);
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
            doc.Metadata.Add("SUPL_DATA_FOM_CD", "RNS001");
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
            doc.Elements.Add("OPRM_IPAT_DT", m_Info.OPRM_IPAT_DT); // 수술실 입실일시
            doc.Elements.Add("OPRM_DSCG_DT", m_Info.OPRM_DSCG_DT); // 수술실 퇴실일시
            doc.Elements.Add("SOPR_STA_DT", m_Info.SOPR_STA_DT); // 수술 시작일시
            doc.Elements.Add("SOPR_END_DT", m_Info.SOPR_END_DT); // 수술 종료일시
            doc.Elements.Add("DSFN_NURSE_NM", m_Info.DSFN_NURSE_NM); // 소독 간호사
            doc.Elements.Add("CRCL_NURSE_NM", m_Info.CRCL_NURSE_NM); // 순회 간호사
            doc.Elements.Add("WRT_DT", m_Info.WRT_DT); // 작성일시

            // B.수술전확인
            doc.Elements.Add("PTNT_POSI_CFR_YN", m_Info.PTNT_POSI_CFR_YN); // Time Out 작성여부

            // C.수술정보

            // 2.수술전 진단
            doc.Tables.AddColumn("TBL_SOPR_BF_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_SOPR_BF_DIAG", "SICK_SYM"); // 상병코드

            for (int row = 0; row < grdDiagBfView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_BF_DIAG");
                doc.Tables["TBL_SOPR_BF_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagBfView.GetRowCellValue(row, gcDIAG_NM_BF).ToString();
                doc.Tables["TBL_SOPR_BF_DIAG"].Rows[row]["SICK_SYM"].Value = grdDiagBfView.GetRowCellValue(row, gcSICK_SYM_BF).ToString().Replace(".","");
            }

            // 3.수술후 진단
            doc.Tables.AddColumn("TBL_SOPR_AF_DIAG", "DIAG_NM"); // 진단명
            doc.Tables.AddColumn("TBL_SOPR_AF_DIAG", "SICK_SYM"); // 상병코드

            for (int row = 0; row < grdDiagAfView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_AF_DIAG");
                doc.Tables["TBL_SOPR_AF_DIAG"].Rows[row]["DIAG_NM"].Value = grdDiagAfView.GetRowCellValue(row, gcDIAG_NM_AF).ToString();
                doc.Tables["TBL_SOPR_AF_DIAG"].Rows[row]["SICK_SYM"].Value = grdDiagAfView.GetRowCellValue(row, gcSICK_SYM_AF).ToString().Replace(".", "");
            }

            // 4.수술명
            doc.Tables.AddColumn("TBL_SOPR_NM", "SOPR_NM"); // 수술명
            doc.Tables.AddColumn("TBL_SOPR_NM", "MDFEE_CD"); // 수술코드

            for (int row = 0; row < grdSoprView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_NM");
                doc.Tables["TBL_SOPR_NM"].Rows[row]["SOPR_NM"].Value = grdSoprView.GetRowCellValue(row, gcSOPR_NM).ToString();
                doc.Tables["TBL_SOPR_NM"].Rows[row]["MDFEE_CD"].Value = grdSoprView.GetRowCellValue(row, gcMDFEE_CD).ToString();
            }

            // D.수술 세부 정보

            // 2.치료재료
            doc.Tables.AddColumn("TBL_TMCAT", "TMCAT_CD"); // 재로코드
            doc.Tables.AddColumn("TBL_TMCAT", "TMCAT_NM"); // 재료명
            doc.Tables.AddColumn("TBL_TMCAT", "NOM"); // 규격
            doc.Tables.AddColumn("TBL_TMCAT", "QTY"); // 수량
            doc.Tables.AddColumn("TBL_TMCAT", "UNIT"); // 단위

            for (int row = 0; row < grdTmcatView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_TMCAT");
                doc.Tables["TBL_TMCAT"].Rows[row]["TMCAT_CD"].Value = grdTmcatView.GetRowCellValue(row, gcTMCAT_CD).ToString();
                doc.Tables["TBL_TMCAT"].Rows[row]["TMCAT_NM"].Value = grdTmcatView.GetRowCellValue(row, gcTMCAT_NM).ToString();
                doc.Tables["TBL_TMCAT"].Rows[row]["NOM"].Value = grdTmcatView.GetRowCellValue(row, gcNOM).ToString();
                doc.Tables["TBL_TMCAT"].Rows[row]["QTY"].Value = grdTmcatView.GetRowCellValue(row, gcQTY).ToString();
                doc.Tables["TBL_TMCAT"].Rows[row]["UNIT"].Value = grdTmcatView.GetRowCellValue(row, gcUNIT).ToString();
            }

            // 3.약물
            doc.Tables.AddColumn("TBL_SOPR_MDS", "MDS_CD"); // 약품코드
            doc.Tables.AddColumn("TBL_SOPR_MDS", "MDS_NM"); // 약품명
            doc.Tables.AddColumn("TBL_SOPR_MDS", "TOT_INJC_QTY"); // 투야용량
            doc.Tables.AddColumn("TBL_SOPR_MDS", "MDS_UNIT_TXT"); // 단위
            doc.Tables.AddColumn("TBL_SOPR_MDS", "INJC_MTH_TXT"); // 투여경로

            for (int row = 0; row < grdMdsView.RowCount; row++)
            {
                doc.Tables.AddRow("TBL_SOPR_MDS");
                doc.Tables["TBL_SOPR_MDS"].Rows[row]["MDS_CD"].Value = grdMdsView.GetRowCellValue(row, gcMDS_CD).ToString();
                doc.Tables["TBL_SOPR_MDS"].Rows[row]["MDS_NM"].Value = grdMdsView.GetRowCellValue(row, gcMDS_NM).ToString();
                doc.Tables["TBL_SOPR_MDS"].Rows[row]["TOT_INJC_QTY"].Value = grdMdsView.GetRowCellValue(row, gcTOT_INJC_QTY).ToString();
                doc.Tables["TBL_SOPR_MDS"].Rows[row]["MDS_UNIT_TXT"].Value = grdMdsView.GetRowCellValue(row, gcMDS_UNIT_TXT).ToString();
                doc.Tables["TBL_SOPR_MDS"].Rows[row]["INJC_MTH_TXT"].Value = grdMdsView.GetRowCellValue(row, gcINJC_MTH_TXT).ToString();
            }


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
                    CTI84A.SaveSend("RNS001", m_req_data_no, m_dmd_no, m_sp_sno, "");
                }
            }
        }

    }
}

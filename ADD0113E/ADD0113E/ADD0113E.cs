using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0113E
{
    public partial class ADD0113E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private bool IsFirst;
        private string m_pgm_step = ""; // 2022.01.12 WOOIL - 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD0113E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD0113E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD0113E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0113E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtYYMMfr.Text = "";
            txtYYMMto.Text = "";

            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string strHdate = MetroLib.Util.GetSysDate(conn);
                    conn.Close();
                    txtYYMMfr.Text = strHdate.Substring(0, 6);
                    txtYYMMto.Text = strHdate.Substring(0, 6);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtYYMMfr.Text.ToString() == "")
            {
                MessageBox.Show("청구년월을 입력하세요.");
                return;
            }

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
            string gbn = "1"; // 양방
            if (rbYHgbn2.Checked) gbn = "2"; // 한방
            string iofg = "1"; // 외래
            if (rbIofg2.Checked) iofg = "2"; // 입원
            string qfy = "2"; // 보험.공상
            if (rbQfy3.Checked) qfy = "3"; // 보호
            else if (rbQfy5.Checked) qfy = "5"; // 산재
            else if (rbQfy6.Checked) qfy = "6"; // 자보
            else if (rbQfy38.Checked) qfy = "38"; // 보호정신과
            else if (rbQfy29.Checked) qfy = "29"; // 보훈일반

            string frmm = txtYYMMfr.Text.ToString();
            string tomm = txtYYMMto.Text.ToString();
            string pid = txtPid.Text.ToString().Trim();
            string pnm = txtPnm.Text.ToString().Trim();

            if (tomm == "") tomm = frmm;

            if (iofg == "2")
            {
                frmm += "01";
                tomm += "31";
            }

            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (iofg == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT A.SIMNO";
            sql = sql + Environment.NewLine + "     , A.EPRTNO";
            sql = sql + Environment.NewLine + "     , A.PID";
            sql = sql + Environment.NewLine + "     , A.PNM";
            sql = sql + Environment.NewLine + "     , A.QFYCD";
            sql = sql + Environment.NewLine + "     , A.PDIV";
            sql = sql + Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
            sql = sql + Environment.NewLine + "     , A.DONFG";
            sql = sql + Environment.NewLine + "     , A.UNICD";
            sql = sql + Environment.NewLine + "     , A.STEDT";
            sql = sql + Environment.NewLine + "     , A.GONSGB";
            sql = sql + Environment.NewLine + "     , A.SIMFG";
            sql = sql + Environment.NewLine + "     , A." + fEXDATE + " AS K1";
            sql = sql + Environment.NewLine + "     , A.QFYCD AS K2";
            sql = sql + Environment.NewLine + "     , A.JRBY AS K3";
            sql = sql + Environment.NewLine + "     , A.PID AS K4";
            sql = sql + Environment.NewLine + "     , A.UNISQ AS K5";
            sql = sql + Environment.NewLine + "     , A.SIMCS AS K6";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " A";
            sql = sql + Environment.NewLine + " WHERE A." + fEXDATE + ">='" + frmm + "'";
            sql = sql + Environment.NewLine + "   AND A." + fEXDATE + "<='" + tomm + "'";
            sql = sql + Environment.NewLine + "   AND ISNULL(A.SIMFG,'')<>'' ";
            if (qfy == "2")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('21','22','23','24','25','26','27','28','40') ";
            }
            else if (qfy == "3")
            {
                sql = sql + Environment.NewLine + "   AND (A.QFYCD LIKE '3%' AND A.QFYCD NOT IN ('38','39'))";
            }
            else if (qfy == "5")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('50') ";
            }
            else if (qfy == "6")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('61','62','63','64','65','66','67','68','69') ";
            }
            else if (qfy == "38")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('38','39') ";
            }
            else if (qfy == "29")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD = '29' "; // 2006.05.23 WOOIL - 보훈일반 추가
            }
            if (pid != "")
            {
                sql = sql + Environment.NewLine + "   AND A.PID='" + pid + "'";
            }
            if (pnm != "")
            {
                sql = sql + Environment.NewLine + "   AND A.PNM LIKE '" + pnm + "%'";
            }
            sql = sql + Environment.NewLine + "   AND A.SIMCS>0";
            sql = sql + Environment.NewLine + "   AND ISNULL(A.ADDZ1,'') IN ('','0','3') /* 원청구+분리청구 */ ";
            sql = sql + Environment.NewLine + " ORDER BY A." + fEXDATE + ",A.QFYCD,A.SIMNO ";

            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            grdSub.DataSource = null;
            List<CData> slist = new List<CData>();
            grdSub.DataSource = slist;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CData data = new CData();
                            data.OP = false;
                            data.EPRTNO = reader["EPRTNO"].ToString();
                            data.SIMNO = reader["SIMNO"].ToString();
                            data.PID = reader["PID"].ToString();
                            data.PNM = reader["PNM"].ToString();
                            data.QFYCD = reader["QFYCD"].ToString();
                            data.DPTCD = reader["DPTCD"].ToString();
                            data.PDIV = reader["PDIV"].ToString();
                            data.GONSGB = reader["GONSGB"].ToString();
                            data.DONFG = reader["DONFG"].ToString();
                            data.UNICD = reader["UNICD"].ToString();
                            data.STEDT = reader["STEDT"].ToString();
                            data.SIMFG = reader["SIMFG"].ToString();
                            data.K1 = reader["K1"].ToString();
                            data.K2 = reader["K2"].ToString();
                            data.K3 = reader["K3"].ToString();
                            data.K4 = reader["K4"].ToString();
                            data.K5 = reader["K5"].ToString();
                            data.K6 = reader["K6"].ToString();
                            data.IOFG = iofg;

                            list.Add(data);

                            data = new CData();
                            data.OP = false;
                            data.EPRTNO = reader["EPRTNO"].ToString();
                            data.SIMNO = reader["SIMNO"].ToString();
                            data.PID = reader["PID"].ToString();
                            data.PNM = reader["PNM"].ToString();
                            data.QFYCD = reader["QFYCD"].ToString();
                            data.DPTCD = reader["DPTCD"].ToString();
                            data.PDIV = reader["PDIV"].ToString();
                            data.GONSGB = reader["GONSGB"].ToString();
                            data.DONFG = reader["DONFG"].ToString();
                            data.UNICD = reader["UNICD"].ToString();
                            data.STEDT = reader["STEDT"].ToString();
                            data.SIMFG = reader["SIMFG"].ToString();
                            data.K1 = reader["K1"].ToString();
                            data.K2 = reader["K2"].ToString();
                            data.K3 = reader["K3"].ToString();
                            data.K4 = reader["K4"].ToString();
                            data.K5 = reader["K5"].ToString();
                            data.K6 = reader["K6"].ToString();
                            data.IOFG = iofg;

                            slist.Add(data);
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }

            this.RefreshGridMain();
            this.RefreshGridSub();
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

        private void RefreshGridSub()
        {
            if (grdSub.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_pgm_step = "";
            bool bMain = false;
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                if ((bool)grdMainView.GetRowCellValue(row, gcOP) == true)
                {
                    bMain = true;
                    break;
                }
            }
            if (bMain == false)
            {
                MessageBox.Show("명세서A를 선택하세요.");
                return;
            }

            bool bSub = false;
            for (int row = 0; row < grdSubView.RowCount; row++)
            {
                if ((bool)grdSubView.GetRowCellValue(row, gcOP_SUB) == true)
                {
                    bSub = true;
                    break;
                }
            }
            if (bSub == false)
            {
                MessageBox.Show("명세서B를 선택하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Make();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "(" + m_pgm_step + ")");
            }
        }

        private void Make()
        {
            CKey keyA = new CKey();
            keyA.Clear();
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                if ((bool)grdMainView.GetRowCellValue(row, gcOP) == true)
                {
                    keyA.IOFG = grdMainView.GetRowCellValue(row, gcIOFG).ToString();
                    keyA.EXDATE = grdMainView.GetRowCellValue(row, gcK1).ToString();
                    keyA.QFYCD = grdMainView.GetRowCellValue(row, gcK2).ToString();
                    keyA.JRBY = grdMainView.GetRowCellValue(row, gcK3).ToString();
                    keyA.PID = grdMainView.GetRowCellValue(row, gcK4).ToString();
                    keyA.UNISQ = grdMainView.GetRowCellValue(row, gcK5).ToString();
                    keyA.SIMCS = grdMainView.GetRowCellValue(row, gcK6).ToString();
                    keyA.SIMFG = grdMainView.GetRowCellValue(row, gcSIMFG).ToString();
                    break;
                }
            }
            CKey keyB = new CKey();
            keyB.Clear();
            for (int row = 0; row < grdSubView.RowCount; row++)
            {
                if ((bool)grdSubView.GetRowCellValue(row, gcOP_SUB) == true)
                {
                    keyB.IOFG = grdSubView.GetRowCellValue(row, gcIOFG_SUB).ToString();
                    keyB.EXDATE = grdSubView.GetRowCellValue(row, gcK1_SUB).ToString();
                    keyB.QFYCD = grdSubView.GetRowCellValue(row, gcK2_SUB).ToString();
                    keyB.JRBY = grdSubView.GetRowCellValue(row, gcK3_SUB).ToString();
                    keyB.PID = grdSubView.GetRowCellValue(row, gcK4_SUB).ToString();
                    keyB.UNISQ = grdSubView.GetRowCellValue(row, gcK5_SUB).ToString();
                    keyB.SIMCS = grdSubView.GetRowCellValue(row, gcK6_SUB).ToString();
                    keyB.SIMFG = grdSubView.GetRowCellValue(row, gcSIMFG_SUB).ToString();
                    break;
                }
            }

            CKey keyN = new CKey();
            keyN.Clear();
            keyN.IOFG = keyA.IOFG;
            keyN.EXDATE = keyA.EXDATE;
            keyN.QFYCD = keyA.QFYCD;
            keyN.JRBY = keyA.JRBY;
            keyN.PID = keyA.PID;
            keyN.UNISQ = keyA.UNISQ;
            keyN.SIMCS = keyA.SIMCS;
            keyN.SIMFG = keyA.SIMFG;

            CTT55 t55 = new CTT55();
            t55.Clear();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    // 새로운 unisq를 구한다.
                    m_pgm_step = "SetNewUnisq";
                    SetNewUnisq(keyN, conn, tran);

                    // 새로운 심사번호를 구한다.
                    m_pgm_step = "SetNewSimno";
                    SetNewSimno(keyN, conn, tran);

                    // TI1A를 새로만든다.(명세서 A를 기준)
                    m_pgm_step = "Merge1A";
                    Merge1A(keyA, keyB, keyN, t55, conn, tran);

                    // TI1B를 새로만든다.(명세서 A를 기준)
                    m_pgm_step = "Merge1B";
                    Merge1B(keyA, keyB, keyN, conn, tran);

                    // TI1F를 합친다.
                    m_pgm_step = "Merge1F";
                    Merge1F(keyA, keyB, keyN, conn, tran);

                    // TI1H를 합친다.
                    m_pgm_step = "Merge1H";
                    Merge1H(keyA, keyB, keyN, conn, tran);

                    // TI13을 합친다.
                    m_pgm_step = "Merge13";
                    Merge13(keyA, keyB, keyN, conn, tran);

                    // TI13을 합친다.
                    m_pgm_step = "Merge13T";
                    Merge13T(keyA, keyB, keyN, conn, tran);

                    // TI14을 합친다.
                    m_pgm_step = "Merge14";
                    Merge14(keyA, keyB, keyN, conn, tran);

                    // TI1J를 다시 생성한다
                    m_pgm_step = "Merge1J";
                    Merge1J(keyA, keyB, keyN, conn, tran);

                    // TI20을 합친다.(2016.06.13 KJW)
                    m_pgm_step = "Merge20";
                    Merge20(keyA, keyB, keyN, conn, tran);

                    // 청구안함상태로 변경
                    m_pgm_step = "SetDonfgX";
                    if (chkX.Checked == true) SetDonfgX(keyA, keyB, keyN, conn, tran);

                    // 로그저장
                    m_pgm_step = "SaveTT55";
                    SaveTT55(t55, conn, tran);

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }

        }

        private void SetNewUnisq(CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(UNISQ),0)+1 AS NEWUNISQ ";
            sql = sql + Environment.NewLine + " FROM " + tTI1A + " ";
            sql = sql + Environment.NewLine + "WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "  AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "  AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "  AND PID   ='" + p_keyN.PID + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) p_keyN.UNISQ = reader["NEWUNISQ"].ToString();
                    reader.Close();
                }
            }
        }

        private void SetNewSimno(CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0)+1 AS NEWSIMNO ";
            sql = sql + Environment.NewLine + " FROM " + tTI1A + " ";
            sql = sql + Environment.NewLine + "WHERE " + fEXDATE + " LIKE '" + p_keyN.EXDATE.Substring(0, 6) + "%' ";
            sql = sql + Environment.NewLine + "  AND SIMFG ='" + p_keyN.SIMFG + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) p_keyN.SIMNO = reader["NEWSIMNO"].ToString();
                    reader.Close();
                }
            }
        }

        private void Merge1A(CKey p_keyA, CKey p_keyB, CKey p_keyN, CTT55 t55, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string entdt = MetroLib.Util.GetSysDate(p_conn, p_tran);

            // 명세서 A를 기본으로 한다.
            //string sql = "";
            //sql = "";
            //sql = sql + Environment.NewLine + "INSERT INTO " + tTI1A + "(";
            //sql = sql + Environment.NewLine + "       " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SIMNO,TRANSFG,";
            //sql = sql + Environment.NewLine + "       UNICD,UNINM,INSNM,PNM,INSID,RESID,PSEX,FMRCD,JBFG,NBPID,PDIV,APRDT,GENDT,MADDR,REMARK,SIMFG,YYMM,APPRNO,JAJR,BOHUN,JANGAEFG,RPID,OPRFG,DAETC,TJKH,RSLT,DISEAPOS,BDEDT,FSTDT,INSTRU,BEDODT,IPATH,PHYCDCHK,ARVPATH,HRFG,DAILYSUMFG,MAXAUTOFG,SBRDNTYPE,HOMENRFG,CFHCCFRNOFG,TT97KEY,YOFG,YOPDIV,JIWONAMT,MT020,HWTTAMT,YOGROUP,GONSGB,PDIVM,BOHUNDCFG,BOHUNDCCD,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,TUBERFG,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,ICUFRDT,ICUTODT,ICUDAYS,PACAREFG,SJSDFG,DRGCHUGAFG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT";
            //sql = sql + Environment.NewLine + "       ) ";
            //sql = sql + Environment.NewLine + "SELECT '" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "','" + p_keyN.UNISQ + "','" + p_keyN.SIMCS + "','" + p_keyN.SIMNO + "','*',";
            //sql = sql + Environment.NewLine + "       UNICD,UNINM,INSNM,PNM,INSID,RESID,PSEX,FMRCD,JBFG,NBPID,PDIV,APRDT,GENDT,MADDR,REMARK,SIMFG,YYMM,APPRNO,JAJR,BOHUN,JANGAEFG,RPID,OPRFG,DAETC,TJKH,RSLT,DISEAPOS,BDEDT,FSTDT,INSTRU,BEDODT,IPATH,PHYCDCHK,ARVPATH,HRFG,DAILYSUMFG,MAXAUTOFG,SBRDNTYPE,HOMENRFG,CFHCCFRNOFG,TT97KEY,YOFG,YOPDIV,JIWONAMT,MT020,HWTTAMT,YOGROUP,GONSGB,PDIVM,BOHUNDCFG,BOHUNDCCD,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,TUBERFG,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,ICUFRDT,ICUTODT,ICUDAYS,PACAREFG,SJSDFG,DRGCHUGAFG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            //
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    cmd.ExecuteNonQuery();
            //}

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SIMNO") para.Add(p_keyN.SIMNO);
                    else if (col.ColumnName == "TRANSFG") para.Add("*");
                    else if (col.ColumnName == "DONFG") para.Add("");
                    else if (col.ColumnName == "EMPID") para.Add(m_User);
                    else if (col.ColumnName == "EPRTNO") para.Add(null);
                    else if (col.ColumnName == "DODHM") para.Add(entdt);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1A + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.BREAK;
            });


            // 명세서 A의 정보를 읽는다.
            string a_bededt = "";
            string a_stedt = "";
            string a_jrkk = "";
            string a_examc = "";
            string a_xdays = "";
            string a_sbrdntype = "";
            string a_cfhccfrno = "";
            int a_mt020 = 0;
            int a_mt029 = 0;
            string a_dailyptamtfg = "";
            string a_sj070 = "";

            sql = "";
            sql = sql + Environment.NewLine + "SELECT BDEDT,STEDT,JRKK,EXAMC,XDAYS,SBRDNTYPE,CFHCCFRNO,MT020,MT029,DAILYPTAMTFG,SJ070 ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        a_bededt = reader["BDEDT"].ToString();// 로그를 위해 읽는다.
                        a_stedt = reader["STEDT"].ToString();
                        a_jrkk = reader["JRKK"].ToString();
                        a_examc = reader["EXAMC"].ToString();
                        a_xdays = reader["XDAYS"].ToString();
                        a_sbrdntype = reader["SBRDNTYPE"].ToString();
                        a_cfhccfrno = reader["CFHCCFRNO"].ToString();
                        int.TryParse(reader["MT020"].ToString(), out a_mt020);
                        int.TryParse(reader["MT029"].ToString(), out a_mt029);
                        a_dailyptamtfg = reader["DAILYPTAMTFG"].ToString();
                        a_sj070 = reader["SJ070"].ToString();
                    }
                    reader.Close();
                }
            }

            // 명세서 B의 정보를 읽는다.
            string b_stedt = "";
            string b_jrkk = "";
            string b_examc = "";
            string b_xdays = "";
            string b_sbrdntype = "";
            string b_cfhccfrno = "";
            int b_mt020 = 0;
            int b_mt029 = 0;
            string b_dailyptamtfg = "";
            string b_sj070 = "";

            sql = "";
            sql = sql + Environment.NewLine + "SELECT STEDT,JRKK,EXAMC,XDAYS,SBRDNTYPE,CFHCCFRNO,MT020,MT029,DAILYPTAMTFG,SJ070 ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        b_stedt = reader["STEDT"].ToString();
                        b_jrkk = reader["JRKK"].ToString();
                        b_examc = reader["EXAMC"].ToString();
                        b_xdays = reader["XDAYS"].ToString();
                        b_sbrdntype = reader["SBRDNTYPE"].ToString();
                        b_cfhccfrno = reader["CFHCCFRNO"].ToString();
                        int.TryParse(reader["MT020"].ToString(), out b_mt020);
                        int.TryParse(reader["MT029"].ToString(), out b_mt029);
                        b_dailyptamtfg = reader["DAILYPTAMTFG"].ToString();
                        b_sj070 = reader["SJ070"].ToString();
                    }
                    reader.Close();
                }
            }

            // 새로운 값을 만든다.
            string n_stedt = (string.Compare(b_stedt, a_stedt) < 0 ? b_stedt : a_stedt);
            string n_xdays = GetNewXdays(a_xdays, b_xdays);
            string n_sbrdntype = a_sbrdntype;
            string n_cfhccfrno = GetNewCFHCCFRNO(a_cfhccfrno, b_cfhccfrno);
            string n_examc = GetExamc(n_xdays);
            string n_jrkk = n_examc;
            int n_mt020 = a_mt020 + b_mt020;
            int n_mt029 = a_mt029 + b_mt029;
            string n_dailyptamtfg = "0";
            string n_dailysumfg = "0";
            string n_sj070 = a_sj070;
            if (a_dailyptamtfg == "1" && b_dailyptamtfg == "1") n_dailyptamtfg = "1";
            // 2012.01.06 WOOIL - 보험,보호는 무조건 방문일자별로 만든다.
            if (p_keyA.QFYCD == "50")
            {
                if (n_sj070 == "") n_sj070 = "070";
            }
            else if (p_keyA.QFYCD.StartsWith("2") || p_keyA.QFYCD.StartsWith("3") || p_keyA.QFYCD.StartsWith("4"))
            {
                n_dailysumfg = "1";
                n_dailyptamtfg = "0";
            }

            // 새로운 값으로 업데이트한다.
            sql = "";
            sql = sql + Environment.NewLine + "UPDATE " + tTI1A + "";
            sql = sql + Environment.NewLine + "   SET STEDT = '" + n_stedt + "' ";
            sql = sql + Environment.NewLine + "     , JRKK = '" + n_jrkk + "' ";
            sql = sql + Environment.NewLine + "     , EXAMC = '" + n_examc + "' ";
            sql = sql + Environment.NewLine + "     , XDAYS = '" + n_xdays + "' ";
            sql = sql + Environment.NewLine + "     , SBRDNTYPE = '" + n_sbrdntype + "' ";
            sql = sql + Environment.NewLine + "     , CFHCCFRNO = '" + n_cfhccfrno + "' ";
            sql = sql + Environment.NewLine + "     , MT020 = " + n_mt020 + " ";
            sql = sql + Environment.NewLine + "     , MT029 = " + n_mt029 + " ";
            if (p_keyN.IOFG == "1")
            {
                sql = sql + Environment.NewLine + "     , DAILYPTAMTFG = '" + n_dailyptamtfg + "' ";
                sql = sql + Environment.NewLine + "     , DAILYSUMFG = '" + n_dailysumfg + "' ";
            }
            sql = sql + Environment.NewLine + "     , SJ070 = '" + n_sj070 + "'";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            if (p_keyN.IOFG == "1" && n_dailyptamtfg == "1")
            {
                Merge1AA(p_keyA, p_keyB, p_keyN, p_conn, p_tran);
                Merge1FA(p_keyA, p_keyB, p_keyN, p_conn, p_tran);
                Merge1EA();
            }

            // 2021.08.27 WOOIL - 로그를 외래도 남기자
            t55.ENTDT = entdt;
            t55.PID = p_keyN.PID;
            t55.BEDEDT = (a_bededt == "" ? a_stedt : a_bededt);
            t55.ENTTM = ""; // 끝날때 셋팅한다.
            t55.DPTCD = "";
            t55.ENDTM = ""; // 끝날때 셋팅한다.
            t55.EMPID = m_User;
            t55.EMPNM = "";
            t55.PRGID = "";
            t55.PRGID2 = "ADD0113E";
            t55.WORKNM = "명세서합치기";
            t55.REMARK = "";
            t55.REMARK += p_keyA.EXDATE + ",";
            t55.REMARK += p_keyA.QFYCD + ",";
            t55.REMARK += p_keyA.JRBY + ",";
            t55.REMARK += p_keyA.PID + ",";
            t55.REMARK += p_keyA.UNISQ + ",";
            t55.REMARK += p_keyA.SIMCS + "+";
            t55.REMARK += p_keyB.EXDATE + ",";
            t55.REMARK += p_keyB.QFYCD + ",";
            t55.REMARK += p_keyB.JRBY + ",";
            t55.REMARK += p_keyB.PID + ",";
            t55.REMARK += p_keyB.UNISQ + ",";
            t55.REMARK += p_keyB.SIMCS + "=>";
            t55.REMARK += p_keyN.EXDATE + ",";
            t55.REMARK += p_keyN.QFYCD + ",";
            t55.REMARK += p_keyN.JRBY + ",";
            t55.REMARK += p_keyN.PID + ",";
            t55.REMARK += p_keyN.UNISQ + ",";
            t55.REMARK += p_keyN.SIMCS;

        }

        private string GetNewXdays(string a_xdays, string b_xdays)
        {
            string n_xdays = "";
            string[] n_xdays_arr = new string[31];
            for (int i = 0; i < 31; i++)
            {
                n_xdays_arr[i] = "";
            }
            string[] a_xdays_arr = (a_xdays + "$").Split('$');
            for (int i = 0; i < a_xdays_arr.Length; i++)
            {
                if (a_xdays_arr[i] != "")
                {
                    int day = 0;
                    int.TryParse(a_xdays_arr[i], out day);
                    if (day >= 1 && day <= 31)
                    {
                        n_xdays_arr[day - 1] = "*";
                    }
                }
            }
            string[] b_xdays_arr = (b_xdays + "$").Split('$');
            for (int i = 0; i < b_xdays_arr.Length; i++)
            {
                if (b_xdays_arr[i] != "")
                {
                    int day = 0;
                    int.TryParse(b_xdays_arr[i], out day);
                    if (day >= 1 && day <= 31)
                    {
                        n_xdays_arr[day - 1] = "*";
                    }
                }
            }
            for (int i = 1; i <= 31; i++)
            {
                if (n_xdays_arr[i - 1] == "*")
                {
                    if (n_xdays == "") n_xdays = i.ToString();
                    else n_xdays += "$" + i.ToString();
                }
            }
            return n_xdays;
        }

        private string GetNewCFHCCFRNO(string a_cfhccfrno, string b_cfhccfrno)
        {
            string n_cfhccfrno = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] a_arr = (a_cfhccfrno + ",").Split(',');
            for (int i = 0; i < a_arr.Length; i++)
            {
                if (a_arr[i] != "")
                {
                    if (dic.ContainsKey(a_arr[i]) == false) dic.Add(a_arr[i], "");
                }
            }
            string[] b_arr = (b_cfhccfrno + ",").Split(',');
            for (int i = 0; i < b_arr.Length; i++)
            {
                if (b_arr[i] != "")
                {
                    if (dic.ContainsKey(b_arr[i]) == false) dic.Add(b_arr[i], "");
                }
            }
            foreach (KeyValuePair<string,string> kv in dic)
            {
                if (n_cfhccfrno == "") n_cfhccfrno = kv.Key;
                else n_cfhccfrno += "," + kv.Key;
            }

            return n_cfhccfrno;
        }

        private string GetExamc(string n_xdays)
        {
            int examc = 0;
            string[] n_arr = (n_xdays + "$").Split('$');
            for (int i = 0; i < n_arr.Length; i++)
            {
                if (n_arr[i] != "") examc++;
            }
            return examc.ToString();
        }

        private void Merge1AA(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql="";
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TI1AA(EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,EXDT,MT020,MT029)";
            sql = sql + Environment.NewLine + "SELECT '" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "'," + p_keyN.UNISQ + "," + p_keyN.SIMCS + "";
            sql = sql + Environment.NewLine + "     , EXDT,SUM(MT020) AS MT020,SUM(MT029) AS MT029";
            sql = sql + Environment.NewLine + "  FROM (";
            sql = sql + Environment.NewLine + "       SELECT EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,EXDT,ISNULL(MT020,0) AS MT020,ISNULL(MT029,0) AS MT029";
            sql = sql + Environment.NewLine + "        FROM TI1AA ";
            sql = sql + Environment.NewLine + "       WHERE EXDATE='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "         AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "         AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "         AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "         AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "         AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + "       UNION ALL ";
            sql = sql + Environment.NewLine + "       SELECT EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,EXDT,ISNULL(MT020,0) AS MT020,ISNULL(MT029,0) AS MT029";
            sql = sql + Environment.NewLine + "        FROM TI1AA ";
            sql = sql + Environment.NewLine + "       WHERE EXDATE='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "         AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "         AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "         AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "         AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "         AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "       ) X ";
            sql = sql + Environment.NewLine + " GROUP BY X.EXDT";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void Merge1FA(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TI1FA(EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,SEQNO,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT)";
            sql = sql + Environment.NewLine + "SELECT '" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "'," + p_keyN.UNISQ + "," + p_keyN.SIMCS + "";
            sql = sql + Environment.NewLine + "     , ROW_NUMBER() OVER(ORDER BY X.SEQ1,X.POS2,X.MAFG DESC,X.PRICD,X.DQTY) AS SEQNO";
            sql = sql + Environment.NewLine + "     , SEQ1,'0' AS SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,'0' AS IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,0 ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT";
            sql = sql + Environment.NewLine + "  FROM (";
            sql = sql + Environment.NewLine + "      SELECT EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,SEQNO,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT ";
            sql = sql + Environment.NewLine + "        FROM TI1FA ";
            sql = sql + Environment.NewLine + "       WHERE EXDATE='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "         AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "         AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "         AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "         AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "         AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + "       UNION ALL ";
            sql = sql + Environment.NewLine + "      SELECT EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,SEQNO,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT ";
            sql = sql + Environment.NewLine + "        FROM TI1FA ";
            sql = sql + Environment.NewLine + "       WHERE EXDATE='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "         AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "         AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "         AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "         AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "         AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "       ) X";
            sql = sql + Environment.NewLine + " ORDER BY X.SEQ1,X.POS2,X.MAFG DESC,X.PRICD,X.DQTY ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void Merge1EA()
        {
            // 다시 계산하기 때문에 만들필요없다.
        }

        private void Merge1B(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1B = "TI1B";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1B = "TI2B";
                fEXDATE = "BDODT";
            }

            m_pgm_step = "Merge1B-1";
            // 명세서 A 의 내역을 옮긴다.
            //string sql="";
            //sql = "";
            //sql = sql + Environment.NewLine + "INSERT INTO " + tTI1B + "(";
            //sql = sql + Environment.NewLine + "       " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,";
            //sql = sql + Environment.NewLine + "       SEQ1,DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,JRKWA,IPWON,TGWON,PDRID,ROFG,DAEXDT,DRLCID,POA) ";
            //sql = sql + Environment.NewLine + "SELECT '" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "','" + p_keyN.UNISQ + "','" + p_keyN.SIMCS + "'";
            //sql = sql + Environment.NewLine + "     , SEQ1,DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,JRKWA,IPWON,TGWON,PDRID,ROFG,DAEXDT,DRLCID,POA ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            //sql = sql + Environment.NewLine + " ORDER BY SEQ1";
            //
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    cmd.ExecuteNonQuery();
            //}

            int seq1 = 0;

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " ORDER BY SEQ1";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                seq1++;
                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ1") para.Add(seq1);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1B + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;
            });

            m_pgm_step = "Merge1B-2";
            // MAX SEQ1을 구해놓는다(상병용).
            //string max_seq1 = "0";
            //sql = "";
            //sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ1),0) MAX_SEQ1 ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";
            //
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    using (OleDbDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.Read()) max_seq1 = reader["MAX_SEQ1"].ToString();
            //        reader.Close();
            //    }
            //}

            m_pgm_step = "Merge1B-3";
            // 명세서 B의 TI1B를 읽으면서 추가되는 것을 넣는다(상병용).
            //sql = "";
            //sql = sql + Environment.NewLine + "INSERT INTO " + tTI1B + "(";
            //sql = sql + Environment.NewLine + "       " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,";
            //sql = sql + Environment.NewLine + "       SEQ1,DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,JRKWA,IPWON,TGWON,PDRID,ROFG,DAEXDT,DRLCID,POA) ";
            //sql = sql + Environment.NewLine + "SELECT '" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "','" + p_keyN.UNISQ + "','" + p_keyN.SIMCS + "'";
            //sql = sql + Environment.NewLine + "     , ROW_NUMBER() OVER(ORDER BY SEQ1)+" + max_seq1 + " AS SEQ1";
            //sql = sql + Environment.NewLine + "     , DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,'' JRKWA,'' IPWON,'' TGWON,'' PDRID,ROFG,DAEXDT,DRLCID,POA ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            //sql = sql + Environment.NewLine + "   AND DACD NOT IN (";
            //sql = sql + Environment.NewLine + "                    SELECT DACD";
            //sql = sql + Environment.NewLine + "                      FROM " + tTI1B + " ";
            //sql = sql + Environment.NewLine + "                     WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "                       AND QFYCD ='" + p_keyN.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "                       AND JRBY  ='" + p_keyN.JRBY + "' ";
            //sql = sql + Environment.NewLine + "                       AND PID   ='" + p_keyN.PID + "' ";
            //sql = sql + Environment.NewLine + "                       AND UNISQ ='" + p_keyN.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "                       AND SIMCS ='" + p_keyN.SIMCS + "' ";
            //sql = sql + Environment.NewLine + "                   )";
            //sql = sql + Environment.NewLine + " ORDER BY SEQ1";
            //
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    cmd.ExecuteNonQuery();
            //}

            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND DACD NOT IN (";
            sql = sql + Environment.NewLine + "                    SELECT DACD";
            sql = sql + Environment.NewLine + "                      FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + "                     WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "                       AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "                       AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "                       AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "                       AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "                       AND SIMCS ='" + p_keyN.SIMCS + "' ";
            sql = sql + Environment.NewLine + "                   )";
            sql = sql + Environment.NewLine + " ORDER BY SEQ1";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                seq1++;
                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "JRKWA") para.Add("");
                    else if (col.ColumnName == "IPWON") para.Add("");
                    else if (col.ColumnName == "TGWON") para.Add("");
                    else if (col.ColumnName == "PDRID") para.Add("");
                    else if (col.ColumnName == "SEQ1") para.Add(seq1);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1B + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;
            });

            m_pgm_step = "Merge1B-4";
            // MAX SEQ1을 구해놓는다(진료과용).
            int max_dept_seq1 = 0;
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ1),0) MAX_SEQ1 ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND ISNULL(JRKWA,'') NOT IN ('','$$$','$$','$')";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) int.TryParse(reader["MAX_SEQ1"].ToString(), out max_dept_seq1);
                    reader.Close();
                }
            }

            m_pgm_step = "Merge1B-5";
            // MAX SEQ1을 구해놓는다(진료과용).
            int max_dept_seq1_all = 0;
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ1),0) MAX_SEQ1 ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) int.TryParse(reader["MAX_SEQ1"].ToString(), out max_dept_seq1_all);
                    reader.Close();
                }
            }

            m_pgm_step = "Merge1B-6";
            // 진료과가 있는지 찾아본다.
            sql = "";
            sql = sql + Environment.NewLine + "SELECT JRKWA,IPWON,TGWON,PDRID ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND ISNULL(JRKWA,'') NOT IN ('','$$$','$$','$')";
            sql = sql + Environment.NewLine + "   AND JRKWA NOT IN (";
            sql = sql + Environment.NewLine + "                    SELECT JRKWA";
            sql = sql + Environment.NewLine + "                      FROM " + tTI1B + " ";
            sql = sql + Environment.NewLine + "                     WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "                       AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "                       AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "                       AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "                       AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "                       AND SIMCS ='" + p_keyN.SIMCS + "' ";
            sql = sql + Environment.NewLine + "                   )";
            sql = sql + Environment.NewLine + " ORDER BY SEQ1";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using(OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    using (DataSet ds = new DataSet())
                    {
                        adapter.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            max_dept_seq1++;

                            string jrkwa = row["JRKWA"].ToString();
                            string ipwon = row["IPWON"].ToString();
                            string tgwon = row["TGWON"].ToString();
                            string pdrid = row["PDRID"].ToString();

                            if (max_dept_seq1 <= max_dept_seq1_all)
                            {
                                m_pgm_step = "Merge1B-7";
                                // 수정
                                string usql = "";
                                usql = "";
                                usql = usql + Environment.NewLine + "UPDATE " + tTI1B + " ";
                                usql = usql + Environment.NewLine + "   SET JRKWA='" + jrkwa + "'";
                                usql = usql + Environment.NewLine + "     , IPWON='" + ipwon + "'";
                                usql = usql + Environment.NewLine + "     , TGWON='" + tgwon + "'";
                                usql = usql + Environment.NewLine + "     , PDRID='" + pdrid + "'";
                                usql = usql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
                                usql = usql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
                                usql = usql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
                                usql = usql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
                                usql = usql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
                                usql = usql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";
                                usql = usql + Environment.NewLine + "   AND SEQ1  ='" + max_dept_seq1.ToString() + "' ";

                                using (OleDbCommand ucmd = new OleDbCommand(usql, p_conn, p_tran))
                                {
                                    ucmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                m_pgm_step = "Merge1B-8";
                                // 인서트
                                string isql = "";
                                isql = "";
                                isql = isql + Environment.NewLine + "INSERT INTO " + tTI1B + "(";
                                isql = isql + Environment.NewLine + "       " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,";
                                isql = isql + Environment.NewLine + "       SEQ1,JRKWA,IPWON,TGWON,PDRID) ";
                                isql = isql + Environment.NewLine + "VALUES('" + p_keyN.EXDATE + "','" + p_keyN.QFYCD + "','" + p_keyN.JRBY + "','" + p_keyN.PID + "','" + p_keyN.UNISQ + "','" + p_keyN.SIMCS + "'";
                                isql = isql + Environment.NewLine + "," + max_dept_seq1.ToString() + ",'" + jrkwa + "','" + ipwon + "','" + tgwon + "','" + pdrid + "')";

                                using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
                                {
                                    icmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            m_pgm_step = "Merge1B-9";
        }

        private void Merge1F(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1F = "TI1F";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1F = "TI2F";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1F + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1F + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + " ORDER BY SEQ1,POS2,MAFG DESC,PRICD,DQTY ";

            int eno = 0;
            int seq2 = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                eno++;
                seq2 = eno;

                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ2") para.Add(seq2);
                    else if (col.ColumnName == "ELINENO") para.Add(eno);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1F + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                // 줄단위특정내역
                Merge1J_LINE(row, eno, p_keyN, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;
            });


            //string sql="";
            //sql="";
            //sql = sql + Environment.NewLine + "SELECT " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,DBO.MFN_TJMULTIRMK_LINE('F','" + p_keyN.IOFG + "'," + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2) AS MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1F + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            //sql = sql + Environment.NewLine + " UNION ALL ";
            //sql = sql + Environment.NewLine + "SELECT " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,DBO.MFN_TJMULTIRMK_LINE('F','" + p_keyN.IOFG + "'," + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2) AS MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1F + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            //sql = sql + Environment.NewLine + " ORDER BY SEQ1,POS2,MAFG DESC,PRICD,DQTY ";
            //
            //int eno = 0;
            //int seq2 = 0;
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    using(OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
            //    {
            //        using (DataSet ds = new DataSet())
            //        {
            //            adapter.Fill(ds);
            //
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                // 2010.11.01 WOOIL - 약제상한차액이면 제외시킨다.
            //                if (row["BGIHO"].ToString() == "BBBBBB") continue;
            //
            //                eno++;
            //                seq2 = eno;
            //
            //                string isql = "";
            //                isql = "";
            //                isql = isql + Environment.NewLine + "INSERT INTO " + tTI1F + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO)";
            //                isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //
            //                using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
            //                {
            //                    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
            //                    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
            //                    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
            //                    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
            //                    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
            //                    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
            //                    icmd.Parameters.Add(new OleDbParameter("@7", row["SEQ1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@8", seq2));         // SEQ2
            //                    icmd.Parameters.Add(new OleDbParameter("@9", row["OP"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@10", row["PRICD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@11", row["BGIHO"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@12", row["PRKNM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@13", row["NTDIV"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@14", row["FCRFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@15", row["DANGA"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@16", row["DQTY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@17", row["DDAY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@18", row["GUMAK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@19", row["EXDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@20", row["POS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@21", row["MAFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@22", row["ACTFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@23", row["EVENT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@24", row["DRGCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@25", row["STTEX"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@26", row["IPOS1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@27", row["ALLEX"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@28", row["GRPCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@29", row["GRPACT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@30", row["GRPNM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@31", row["RSNCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@32", row["REMARK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@33", row["FRDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@34", row["TODT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@35", row["PRIDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@36", eno));         // 줄번호
            //                    icmd.Parameters.Add(new OleDbParameter("@37", row["OKCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@38", row["REFCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@39", row["LOWFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@40", row["CDENTDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@41", row["TPOS1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@42", row["TPOS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@43", row["TPOS3"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@44", row["TPOS4"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@45", row["CDGB"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@46", row["MULTIRMK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@47", row["EXHM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@48", row["CHRLT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@49", row["AFPFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@50", row["CDCHGDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@51", row["HBPRICD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@52", row["CNTQTY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@53", row["LOWRSNCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@54", row["LOWRSNRMK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@55", row["DANGACHK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@56", row["DRADDFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@57", row["UPLMTAMT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@58", row["UPLMTCHAAMT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@59", row["EDIENTDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@60", row["ADDFG1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@61", row["BHEXFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@62", row["SPFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@63", row["SPRT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@64", row["SPAMT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@65", row["SPPOS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@66", row["JBPTFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@67", row["DRIDLIST"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@68", row["INREFFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@69", row["INREFCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@70", row["OYAKFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@71", row["BOSANGRT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@72", row["GUMAK2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@73", row["DRG7_ADD_FG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@74", row["DRG7_ADD_RT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@75", row["DRG7_ADD_GUMAK2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@76", row["INREFFG2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@77", row["INREFCD2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@78", row["DRG7_SEQ1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@79", row["DRG7_POS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@80", row["DRG7_ELINENO"]));
            //
            //                    icmd.ExecuteNonQuery();
            //                }
            //
            //                // 줄단위특정내역
            //                char d_lev5 = (char)25;
            //                char d_lev4 = (char)24;
            //                char d_lev3 = (char)23;
            //                char d_lev2 = (char)22;
            //                char d_lev1 = (char)21;
            //                int tjcdseq = 0;
            //                string[] multirmk_arr = (row["MULTIRMK"].ToString() + d_lev5).Split(d_lev5);
            //                foreach (string rec in multirmk_arr)
            //                {
            //                    if (rec == "") continue;
            //                    string[] rec_arr = (rec + d_lev3).Split(d_lev3);
            //                    string tjcd = rec_arr[0];
            //                    string tjcdrmk = rec_arr[1];
            //                    if (tjcd == "") continue;
            //                    if (tjcdrmk == "") continue;
            //
            //                    tjcdseq++;
            //
            //                    isql = "";
            //                    isql = isql + Environment.NewLine + "INSERT INTO " + tTI1J + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,ELINENO,SEQ,TJCD,TJCDRMK)";
            //                    isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";
            //
            //                    using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
            //                    {
            //                        icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
            //                        icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
            //                        icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
            //                        icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
            //                        icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
            //                        icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
            //                        icmd.Parameters.Add(new OleDbParameter("@7", eno));         // 줄번호
            //                        icmd.Parameters.Add(new OleDbParameter("@8", tjcdseq));
            //                        icmd.Parameters.Add(new OleDbParameter("@9", tjcd));
            //                        icmd.Parameters.Add(new OleDbParameter("@10", tjcdrmk));
            //
            //                        icmd.ExecuteNonQuery();
            //                    }
            //
            //                }
            //
            //                // 2010.11.01 WOOIL - 산재이고 약제상한차액이 있으면...
            //                int uplmtchaamt = 0;
            //                int.TryParse(row["UPLMTCHAAMT"].ToString(), out uplmtchaamt);
            //                if (p_keyN.QFYCD.StartsWith("5") && uplmtchaamt > 0)
            //                {
            //                    eno++;
            //                    seq2 = eno;
            //
            //                    isql = "";
            //                    isql = isql + Environment.NewLine + "INSERT INTO " + tTI1F + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT)";
            //                    isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //
            //                    using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
            //                    {
            //                        icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
            //                        icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
            //                        icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
            //                        icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
            //                        icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
            //                        icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
            //                        icmd.Parameters.Add(new OleDbParameter("@7", row["SEQ1"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@8", seq2));         // SEQ2
            //                        icmd.Parameters.Add(new OleDbParameter("@9", row["OP"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@10", row["PRICD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@11", "BBBBBB"));
            //                        icmd.Parameters.Add(new OleDbParameter("@12", "상한차액(" + row["PRKNM"].ToString() + ")"));
            //                        icmd.Parameters.Add(new OleDbParameter("@13", row["NTDIV"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@14", row["FCRFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@15", uplmtchaamt));   // 상한차액을 단가로 만든다.
            //                        icmd.Parameters.Add(new OleDbParameter("@16", "1"));
            //                        icmd.Parameters.Add(new OleDbParameter("@17", "1"));
            //                        icmd.Parameters.Add(new OleDbParameter("@18", uplmtchaamt));   // 상한차액을 금액으로 만든다.
            //                        icmd.Parameters.Add(new OleDbParameter("@19", row["EXDT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@20", row["POS2"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@21", row["MAFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@22", row["ACTFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@23", row["EVENT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@24", row["DRGCD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@25", row["STTEX"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@26", row["IPOS1"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@27", row["ALLEX"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@28", row["GRPCD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@29", row["GRPACT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@30", row["GRPNM"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@31", ""));
            //                        icmd.Parameters.Add(new OleDbParameter("@32", ""));
            //                        icmd.Parameters.Add(new OleDbParameter("@33", row["FRDT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@34", row["TODT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@35", row["PRIDT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@36", eno));         // 줄번호 새로...
            //                        icmd.Parameters.Add(new OleDbParameter("@37", row["OKCD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@38", row["REFCD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@39", row["LOWFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@40", row["CDENTDT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@41", row["TPOS1"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@42", row["TPOS2"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@43", row["TPOS3"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@44", row["TPOS4"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@45", row["CDGB"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@46", ""));
            //                        icmd.Parameters.Add(new OleDbParameter("@47", row["EXHM"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@48", row["CHRLT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@49", row["AFPFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@50", row["CDCHGDT"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@51", row["HBPRICD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@52", row["CNTQTY"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@53", row["LOWRSNCD"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@54", row["LOWRSNRMK"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@55", row["DANGACHK"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@56", row["DRADDFG"]));
            //                        icmd.Parameters.Add(new OleDbParameter("@57", "0"));
            //                        icmd.Parameters.Add(new OleDbParameter("@58", "0"));
            //                        icmd.Parameters.Add(new OleDbParameter("@59", row["EDIENTDT"]));
            //
            //                        icmd.ExecuteNonQuery();
            //                    }
            //                }
            //            }
            //        }// end of using (DataSet ds = new DataSet())
            //    }// end of using(OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
            //}// end of using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
        }

        private void Merge1J_LINE(DataRow p_row, int p_new_eno, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1J = "TI1J";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1J = "TI2J";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM " + tTI1J + "";
            sql += Environment.NewLine + " WHERE " + fEXDATE + "='" + p_row[fEXDATE].ToString() + "' ";
            sql += Environment.NewLine + "   AND QFYCD ='" + p_row["QFYCD"].ToString() + "' ";
            sql += Environment.NewLine + "   AND JRBY  ='" + p_row["JRBY"].ToString() + "' ";
            sql += Environment.NewLine + "   AND PID   ='" + p_row["PID"].ToString() + "' ";
            sql += Environment.NewLine + "   AND UNISQ ='" + p_row["UNISQ"].ToString() + "' ";
            sql += Environment.NewLine + "   AND SIMCS ='" + p_row["SIMCS"].ToString() + "' ";
            sql += Environment.NewLine + "   AND ELINENO = '" + p_row["ELINENO"].ToString() + "'";
            sql += Environment.NewLine + " ORDER BY SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "ELINENO") para.Add(p_new_eno);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1J + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;
            });
        }

        private void Merge1H(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1F = "TI1F";
            string tTI1H = "TI1H";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1F = "TI2F";
                tTI1H = "TI2H";
                fEXDATE = "BDODT";
            }

            int eno = 0;
            int seq2 = 0;

            // 마지막줄번호를 구한다.
            string sql="";
            sql="";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(ELINENO),0) AS MAXENO ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1F + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) int.TryParse(reader["MAXENO"].ToString(), out eno);
                    reader.Close();
                }
            }

            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1H + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI1H + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + " ORDER BY SEQ1,POS2,MAFG DESC,PRICD,DQTY ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                // 줄번호
                string afpfg = row["AFPFG"].ToString();
                if (afpfg == "1") eno++;
                seq2++;

                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ2") para.Add(seq2);
                    else if (col.ColumnName == "ELINENO") para.Add((afpfg == "1" ? eno : 0));
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI1H + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                // 청구하는 자료가 아니면 특정내역을 만들지 않는다.
                if (afpfg != "1") return MetroLib.SqlHelper.CONTINUE;

                // 줄단위특정내역
                Merge1J_LINE(row, eno, p_keyN, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;
            });

            //sql="";
            //sql = sql + Environment.NewLine + "SELECT " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,DBO.MFN_TJMULTIRMK_LINE('H','" + p_keyA.IOFG + "'," + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2) AS MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1H + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            //sql = sql + Environment.NewLine + " UNION ALL ";
            //sql = sql + Environment.NewLine + "SELECT " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,DBO.MFN_TJMULTIRMK_LINE('H','" + p_keyB.IOFG + "'," + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2) AS MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT ";
            //sql = sql + Environment.NewLine + "  FROM " + tTI1H + " ";
            //sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            //sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            //sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            //sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            //sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            //sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            //sql = sql + Environment.NewLine + " ORDER BY SEQ1,POS2,MAFG DESC,PRICD,DQTY ";
            //
            //using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            //{
            //    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
            //    {
            //        using (DataSet ds = new DataSet())
            //        {
            //            adapter.Fill(ds);
            //
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //
            //                // 줄번호
            //                string afpfg = row["AFPFG"].ToString();
            //                if (afpfg == "1") eno++;
            //                seq2++;
            //
            //                string isql = "";
            //                isql = "";
            //                isql = isql + Environment.NewLine + "INSERT INTO " + tTI1H + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG)";
            //                isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //
            //                using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
            //                {
            //                    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
            //                    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
            //                    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
            //                    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
            //                    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
            //                    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
            //                    icmd.Parameters.Add(new OleDbParameter("@7", row["SEQ1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@8", seq2));         // SEQ2 새로
            //                    icmd.Parameters.Add(new OleDbParameter("@9", row["OP"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@10", row["PRICD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@11", row["BGIHO"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@12", row["PRKNM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@13", row["NTDIV"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@14", row["FCRFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@15", row["DANGA"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@16", row["DQTY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@17", row["DDAY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@18", row["GUMAK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@19", row["EXDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@20", row["POS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@21", row["MAFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@22", row["ACTFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@23", row["EVENT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@24", row["DRGCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@25", row["STTEX"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@26", row["IPOS1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@27", row["ALLEX"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@28", row["GRPCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@29", row["GRPACT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@30", row["GRPNM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@31", row["RSNCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@32", row["REMARK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@33", row["FRDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@34", row["TODT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@35", row["PRIDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@36", (afpfg == "1" ? eno : 0))); // 줄번호 새로...
            //                    icmd.Parameters.Add(new OleDbParameter("@37", row["OKCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@38", row["REFCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@39", row["LOWFG"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@40", row["CDENTDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@41", row["TPOS1"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@42", row["TPOS2"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@43", row["TPOS3"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@44", row["TPOS4"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@45", row["CDGB"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@46", row["MULTIRMK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@47", row["EXHM"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@48", row["CHRLT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@49", afpfg));
            //                    icmd.Parameters.Add(new OleDbParameter("@50", row["CDCHGDT"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@51", row["HBPRICD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@52", row["CNTQTY"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@53", row["LOWRSNCD"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@54", row["LOWRSNRMK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@55", row["DANGACHK"]));
            //                    icmd.Parameters.Add(new OleDbParameter("@56", row["DRADDFG"]));
            //
            //                    icmd.ExecuteNonQuery();
            //
            //                }
            //
            //                // 청구하는 자료가 아니면 특정내역을 만들지 않는다.
            //                if (afpfg != "1") continue;
            //
            //                // 줄단위특정내역
            //                char d_lev5 = (char)25;
            //                char d_lev4 = (char)24;
            //                char d_lev3 = (char)23;
            //                char d_lev2 = (char)22;
            //                char d_lev1 = (char)21;
            //                int tjcdseq = 0;
            //                string[] multirmk_arr = (row["MULTIRMK"].ToString() + d_lev5).Split(d_lev5);
            //                foreach (string rec in multirmk_arr)
            //                {
            //                    if (rec == "") continue;
            //                    string[] rec_arr = (rec + d_lev3).Split(d_lev3);
            //                    string tjcd = rec_arr[0];
            //                    string tjcdrmk = rec_arr[1];
            //                    if (tjcd == "") continue;
            //                    if (tjcdrmk == "") continue;
            //
            //                    tjcdseq++;
            //
            //                    isql = "";
            //                    isql = isql + Environment.NewLine + "INSERT INTO " + tTI1J + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,ELINENO,SEQ,TJCD,TJCDRMK)";
            //                    isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";
            //
            //                    using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
            //                    {
            //                        icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
            //                        icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
            //                        icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
            //                        icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
            //                        icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
            //                        icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
            //                        icmd.Parameters.Add(new OleDbParameter("@7", eno));         // 줄번호
            //                        icmd.Parameters.Add(new OleDbParameter("@8", tjcdseq));
            //                        icmd.Parameters.Add(new OleDbParameter("@9", tjcd));
            //                        icmd.Parameters.Add(new OleDbParameter("@10", tjcdrmk));
            //
            //                        icmd.ExecuteNonQuery();
            //                    }
            //
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void Merge13(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI13 = "TI13";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI13 = "TI23";
                fEXDATE = "BDODT";
            }

            string sql="";
            sql="";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI13 + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI13 + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + " ORDER BY OUTSEQ,PRICD ";

            string bkoutseq = "";
            int seq = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                // 교부번호당 줄번호
                if (bkoutseq != row["OUTSEQ"].ToString())
                {
                    seq = 0;
                    bkoutseq = row["OUTSEQ"].ToString();
                }
                seq++;


                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ") para.Add(seq);
                    else if (col.ColumnName == "ELINENO") para.Add(seq);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI13 + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                // TI13T 처리
                NewTI13T(row, seq, p_keyN, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;

                //string isql="";
                //isql = "";
                //isql = isql + Environment.NewLine + "INSERT INTO " + tTI13 + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,OUTSEQ,SEQ,PRICD,BGIHO,PRKNM,DANGA,DQTY,DDAY,GUMAK,ORDCNT,ELINENO,LOWFG,CDGB,ODAY,BAEKFG,LOWRSNCD,LOWRSNRMK)";
                //isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                //
                //using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
                //{
                //    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
                //    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
                //    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
                //    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
                //    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
                //    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
                //    icmd.Parameters.Add(new OleDbParameter("@7", row["OUTSEQ"]));
                //    icmd.Parameters.Add(new OleDbParameter("@8", seq));// 교부번호별 줄번호
                //    icmd.Parameters.Add(new OleDbParameter("@9", row["PRICD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@10", row["BGIHO"]));
                //    icmd.Parameters.Add(new OleDbParameter("@11", row["PRKNM"]));
                //    icmd.Parameters.Add(new OleDbParameter("@12", row["DANGA"]));
                //    icmd.Parameters.Add(new OleDbParameter("@13", row["DQTY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@14", row["DDAY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@15", row["GUMAK"]));
                //    icmd.Parameters.Add(new OleDbParameter("@16", row["ORDCNT"]));
                //    icmd.Parameters.Add(new OleDbParameter("@17", seq));
                //    icmd.Parameters.Add(new OleDbParameter("@18", row["LOWFG"]));
                //    icmd.Parameters.Add(new OleDbParameter("@19", row["CDGB"]));
                //    icmd.Parameters.Add(new OleDbParameter("@20", row["DDAY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@21", row["BAEKFG"]));
                //    icmd.Parameters.Add(new OleDbParameter("@22", row["LOWRSNCD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@23", row["LOWRSNRMK"]));
                //
                //    icmd.ExecuteNonQuery();
                //
                //    // TI13T 처리
                //    NewTI13T(row, seq, p_keyN, p_conn, p_tran);
                //}
                //
                //return true;
            });
        }

        private void NewTI13T(DataRow p_row, int p_seq, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI13T = "TI13T";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI13T = "TI23T";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI13T + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_row[fEXDATE].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_row["QFYCD"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_row["JRBY"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_row["PID"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_row["UNISQ"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_row["SIMCS"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND OUTSEQ='" + p_row["OUTSEQ"].ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND SEQ   ='" + p_row["SEQ"].ToString() + "' ";
            sql = sql + Environment.NewLine + " ORDER BY SEQNO";

            //int seqno = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ") para.Add(p_seq);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI13T + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;

                //string isql = "";
                //isql = "";
                //isql = isql + Environment.NewLine + "INSERT INTO " + tTI13T + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,OUTSEQ,SEQ,SEQNO,TJCD,TJCDRMK)";
                //isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
                //
                //using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
                //{
                //    seqno++;
                //    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
                //    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
                //    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
                //    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
                //    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
                //    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
                //    icmd.Parameters.Add(new OleDbParameter("@7", p_row["OUTSEQ"]));
                //    icmd.Parameters.Add(new OleDbParameter("@8", p_seq));// 교부번호별 줄번호
                //    icmd.Parameters.Add(new OleDbParameter("@9", seqno));
                //    icmd.Parameters.Add(new OleDbParameter("@10", row["TJCD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@11", row["TJCDRMK"]));
                //
                //    icmd.ExecuteNonQuery();
                //}
                //
                //return true;
            });
        }

        private void Merge13T(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI13T = "TI13T";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI13T = "TI23T";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI13T + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND SEQ=0";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI13T + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND SEQ=0";
            sql = sql + Environment.NewLine + " ORDER BY OUTSEQ,SEQ,SEQNO ";

            string bkoutseq = "";
            int seqno = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                // 교부번호당 줄번호
                if (bkoutseq != row["OUTSEQ"].ToString())
                {
                    seqno = 0;
                    bkoutseq = row["OUTSEQ"].ToString();
                }
                seqno++;

                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQNO") para.Add(seqno);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI13T + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;

                //string isql = "";
                //isql = "";
                //isql = isql + Environment.NewLine + "INSERT INTO " + tTI13T + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,OUTSEQ,SEQ,SEQNO,TJCD,TJCDRMK)";
                //isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
                //
                //using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
                //{
                //    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
                //    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
                //    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
                //    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
                //    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
                //    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
                //    icmd.Parameters.Add(new OleDbParameter("@7", row["OUTSEQ"]));
                //    icmd.Parameters.Add(new OleDbParameter("@8", row["SEQ"]));
                //    icmd.Parameters.Add(new OleDbParameter("@9", seqno));
                //    icmd.Parameters.Add(new OleDbParameter("@10", row["TJCD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@11", row["TJCDRMK"]));
                //
                //    icmd.ExecuteNonQuery();
                //}
                //
                //return true;
            });
        }

        private void Merge14(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI14 = "TI14";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI14 = "TI24";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI14 + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT *";
            sql = sql + Environment.NewLine + "  FROM " + tTI14 + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + " ORDER BY OUTSEQ,PRICD ";

            int seq = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                seq++;

                string fields = "";
                string ins_flds = "";
                List<object> para = new List<object>();
                para.Clear();
                foreach (DataColumn col in row.Table.Columns)
                {
                    if (fields == "") fields = col.ColumnName;
                    else fields += "," + col.ColumnName;

                    if (ins_flds == "") ins_flds = "?";
                    else ins_flds += "," + "?";

                    if (col.ColumnName == "BDODT" || col.ColumnName == "EXDATE") para.Add(p_keyN.EXDATE);
                    else if (col.ColumnName == "QFYCD") para.Add(p_keyN.QFYCD);
                    else if (col.ColumnName == "JRBY") para.Add(p_keyN.JRBY);
                    else if (col.ColumnName == "PID") para.Add(p_keyN.PID);
                    else if (col.ColumnName == "UNISQ") para.Add(p_keyN.UNISQ);
                    else if (col.ColumnName == "SIMCS") para.Add(p_keyN.SIMCS);
                    else if (col.ColumnName == "SEQ") para.Add(seq);
                    else para.Add(row[col]);
                }

                string isql = "";
                isql = "";
                isql += Environment.NewLine + "INSERT INTO " + tTI14 + "(" + fields + ")";
                isql += Environment.NewLine + "VALUES(" + ins_flds + ")";

                MetroLib.SqlHelper.ExecuteSql(isql, para, p_conn, p_tran);

                return MetroLib.SqlHelper.CONTINUE;

                //string isql = "";
                //isql = isql + Environment.NewLine + "INSERT INTO " + tTI14 + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ,FG,PRICD,PRKNM,DQTY,DDAY,ORDCNT,LOWFG,CDGB,OUTSEQ,BGIHO,DANGA,GUMAK,ELINENO,ODAY,LOWRSNCD,LOWRSNRMK)";
                //isql = isql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                //
                //using (OleDbCommand icmd = new OleDbCommand(isql, p_conn, p_tran))
                //{
                //    seq++;
                //    icmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
                //    icmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
                //    icmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
                //    icmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
                //    icmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
                //    icmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
                //    icmd.Parameters.Add(new OleDbParameter("@7", seq));
                //    icmd.Parameters.Add(new OleDbParameter("@8", row["FG"]));
                //    icmd.Parameters.Add(new OleDbParameter("@9", row["PRICD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@10", row["PRKNM"]));
                //    icmd.Parameters.Add(new OleDbParameter("@11", row["DQTY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@12", row["DDAY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@13", row["ORDCNT"]));
                //    icmd.Parameters.Add(new OleDbParameter("@14", row["LOWFG"]));
                //    icmd.Parameters.Add(new OleDbParameter("@15", row["CDGB"]));
                //    icmd.Parameters.Add(new OleDbParameter("@16", row["OUTSEQ"]));
                //    icmd.Parameters.Add(new OleDbParameter("@17", row["BGIHO"]));
                //    icmd.Parameters.Add(new OleDbParameter("@18", row["DANGA"]));
                //    icmd.Parameters.Add(new OleDbParameter("@19", row["GUMAK"]));
                //    icmd.Parameters.Add(new OleDbParameter("@20", row["ELINENO"]));
                //    icmd.Parameters.Add(new OleDbParameter("@21", row["ODAY"]));
                //    icmd.Parameters.Add(new OleDbParameter("@22", row["LOWRSNCD"]));
                //    icmd.Parameters.Add(new OleDbParameter("@23", row["LOWRSNRMK"]));
                //
                //    icmd.ExecuteNonQuery();
                //}

                //return true;
            });
        }

        private void Merge1J(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1A = "TI1A";
            string tTI1F = "TI1F";
            string tTI1J = "TI1J";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1A = "TI2A";
                tTI1F = "TI2F";
                tTI1J = "TI2J";
                fEXDATE = "BDODT";
            }

            m_pgm_step = "Merge1J(1)";
            int inscount=0;
            string mx999 = ReadTA88_hospital("113", p_conn, p_tran); // 2005.08.18 NSK - 보훈환자인 경우 특정내역(MX999 보훈환자) 자동발생 여부

            string daetc = "";
            string tjkh = "";
            string bohun = "";
            string sbrdntype = "";
            string cfhccfrno = "";
            int mt020 = 0;
            string pdiv = "";
            string stedt = "";
            int mt029 = 0;
            string gonsgb = "";

            // TI1A를 읽어서 특정내역을 생성시킨다.
            string sql="";
            sql="";
            sql = sql + Environment.NewLine + "SELECT DAETC,TJKH,BOHUN,SBRDNTYPE,CFHCCFRNO,MT020,PDIV,STEDT,MT029,GONSGB";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " A ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";

            m_pgm_step = "Merge1J(2)";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        daetc = reader["DAETC"].ToString();
                        tjkh = reader["TJKH"].ToString();
                        bohun = reader["BOHUN"].ToString();
                        sbrdntype = reader["SBRDNTYPE"].ToString();
                        cfhccfrno = reader["CFHCCFRNO"].ToString();
                        int.TryParse(reader["MT020"].ToString(), out mt020);
                        pdiv = reader["PDIV"].ToString();
                        stedt = reader["STEDT"].ToString();
                        int.TryParse(reader["MT029"].ToString(), out mt029);
                        gonsgb = reader["GONSGB"].ToString();
                    }
                    reader.Close();
                }
            }

            m_pgm_step = "Merge1J(3)";
            string hosjong = GetHospitalJong(stedt, p_conn, p_tran);

            // ------------------------------------
            // 2007.12.13 WOOIL - MS006과 특정기호
            // ------------------------------------
            if (p_keyN.IOFG == "2" && pdiv == "L")
            {
                sql="";
                sql = sql + Environment.NewLine + "SELECT A88.FLD1QTY, LEFT(F.EXDT,8) AS EXDT ";
                sql = sql + Environment.NewLine + "  FROM " + tTI1F + " F INNER JOIN TA88 A88 ON A88.MST1CD = 'A' AND A88.MST2CD = 'DISEOPCHK' AND A88.MST3CD = LEFT(F.BGIHO,5)";
                sql = sql + Environment.NewLine + " WHERE F." + fEXDATE + "='" + p_keyN.EXDATE + "' ";
                sql = sql + Environment.NewLine + "   AND F.QFYCD ='" + p_keyN.QFYCD + "' ";
                sql = sql + Environment.NewLine + "   AND F.JRBY  ='" + p_keyN.JRBY + "' ";
                sql = sql + Environment.NewLine + "   AND F.PID   ='" + p_keyN.PID + "' ";
                sql = sql + Environment.NewLine + "   AND F.UNISQ ='" + p_keyN.UNISQ + "' ";
                sql = sql + Environment.NewLine + "   AND F.SIMCS ='" + p_keyN.SIMCS + "' ";

                m_pgm_step = "Merge1J(4)";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tjkh_new = reader["FLD1QTY"].ToString();
                            string exdt = reader["EXDT"].ToString();

                            InsTI1J(p_keyN, ++inscount, "MS006", exdt, p_conn, p_tran);

                            if (tjkh == "" && tjkh_new != "")
                            {
                                tjkh = tjkh_new;
                                string usql = "";
                                usql = "";
                                usql = usql + Environment.NewLine + "UPDATE " + tTI1A + " ";
                                usql = usql + Environment.NewLine + "   SET TJKH='" + tjkh + "' ";
                                usql = usql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
                                usql = usql + Environment.NewLine + "   AND QFYCD ='" + p_keyN.QFYCD + "' ";
                                usql = usql + Environment.NewLine + "   AND JRBY  ='" + p_keyN.JRBY + "' ";
                                usql = usql + Environment.NewLine + "   AND PID   ='" + p_keyN.PID + "' ";
                                usql = usql + Environment.NewLine + "   AND UNISQ ='" + p_keyN.UNISQ + "' ";
                                usql = usql + Environment.NewLine + "   AND SIMCS ='" + p_keyN.SIMCS + "' ";

                                using (OleDbCommand ucmd = new OleDbCommand(usql, p_conn, p_tran))
                                {
                                    ucmd.ExecuteNonQuery();
                                }
                            }
                        }
                        reader.Close();
                    }
                }

            }
            // -----------------------------------------------------------------------------------------
            // MT001(생해외인)
            // -----------------------------------------------------------------------------------------
            if (daetc != "")
            {
                m_pgm_step = "Merge1J(5)";
                InsTI1J(p_keyN, ++inscount, "MT001", daetc, p_conn, p_tran);
            }
            // -----------------------------------------------------------------------------------------
            // MT002(특정기호)
            // -----------------------------------------------------------------------------------------
            if (tjkh != "")
            {
                // 2006.08.23 WOOIL - 특정내역이 콤마(,)로 분리되어 2개이상 있을 수 있다.
                string[] tjkh_arr = (tjkh + ",").Split(',');
                foreach (string tjkh_str in tjkh_arr)
                {
                    if (tjkh_str != "")
                    {
                        m_pgm_step = "Merge1J(6)";
                        InsTI1J(p_keyN, ++inscount, "MT002", tjkh_str, p_conn, p_tran);
                    }
                }
            }
            // -----------------------------------------------------------------------------------------
            // 2007.06.19 WOOIL - 본인부담구분코드(보호1종본인부담신설) MT018
            // -----------------------------------------------------------------------------------------
            if(sbrdntype!=""){
                m_pgm_step = "Merge1J(7)";
                InsTI1J(p_keyN, ++inscount, "MT018", sbrdntype, p_conn, p_tran);
            }
            // -----------------------------------------------------------------------------------------
            // 2007.06.12 WOOIL - 진료확인번호(보호1종본인부담신설) MT019
            // -----------------------------------------------------------------------------------------
            if(cfhccfrno!=""){
                // 2006.08.23 WOOIL - 특정내역이 콤마(,)로 분리되어 2개이상 있을 수 있다.
                string[] cfhccfrno_arr = (cfhccfrno + ",").Split(',');
                foreach (string cfhccfrno_str in cfhccfrno_arr)
                {
                    if (cfhccfrno_str != "")
                    {
                        m_pgm_step = "Merge1J(8)";
                        InsTI1J(p_keyN, ++inscount, "MT019", cfhccfrno_str, p_conn, p_tran);
                    }
                }
            }
            // -----------------------------------------------------------------------------------------
            // 2008.03.11 WOOIL - 원내직접조제투약횟수 MT020
            //                    기재대상 : 1종 -> 제1차, 2차, 3차 의료급여기관 외래진료분
            //                               2종 -> 제1차 의료급여기관 외래진료
            //                                      만성질환자의 제2차 의료급여기관 외래진료
            // 2008.12.24 WOOIL - 2009.1.1 진료분부터 촉탁의도 MT020작성
            // 2009.03.20 WOOIL - 2009.4.1 진료분부터 차상위 2종(공상구분 E,F) 추가
            //                    의원(치과의원,한의원,보건의료원 포함)외래진료
            //                    병원(치과병원,한방병원,요양병원 포함) 및 종합병원에서
            //                    의료급여법 시행령 별표 제2호 가목에 따른 만성질환자 외래진료
            // -----------------------------------------------------------------------------------------
            if(p_keyN.IOFG=="1" && mt020 > 0 && stedt.CompareTo("20080401")>=0){
                bool ok = false;
                if(p_keyN.QFYCD=="31" || p_keyN.QFYCD=="38"){
                    ok=true;
                }
                else if (p_keyN.QFYCD == "32" || p_keyN.QFYCD == "39")
                {
                    if (hosjong == "4")
                    {
                        ok = true;
                    }
                    else
                    {
                        if (pdiv == "L" || pdiv == "2" || pdiv == "I" || pdiv == "H") ok = true;
                    }
                    if (stedt.CompareTo("20090101") >= 0 && daetc == "J")
                    {
                        ok = true;
                    }
                }
                if (stedt.CompareTo("20090401") >= 0)
                {
                    if (IsCHASANG2(p_keyN.QFYCD, gonsgb) == true)
                    {
                        if (hosjong == "4")
                        {
                            ok = true;
                        }
                        else if (hosjong == "2" || hosjong == "3")
                        {
                            if (pdiv == "L" || IsMANSUNG(tjkh) == true) ok = true;
                        }
                    }
                }
                if (ok == true)
                {
                    m_pgm_step = "Merge1J(9)";
                    InsTI1J(p_keyN, ++inscount, "MT020", mt020.ToString(), p_conn, p_tran);
                }
            }
            // -----------------------------------------------------------------------------------------
            // 2009.03.20 WOOIL - 진찰횟수 MT029
            //                    2009.4.1 진료분부터
            //                    기재대상 : 1종 -> 제1차, 2차, 3차 의료급여기관 외래진료분
            //                               2종 -> 제1차 의료급여기관 외래진료
            //                                      만성질환자의 제2차 의료급여기관 외래진료
            //                               차상위 2종(공상구분 E,F)
            //                               의원(치과의원,한의원,보건의료원 포함)외래진료
            //                               병원(치과병원,한방병원,요양병원 포함) 및 종합병원에서
            //                               의료급여법 시행령 별표 제2호 가목에 따른 만성질환자 외래진료
            // -----------------------------------------------------------------------------------------
            if (p_keyN.IOFG == "1" && mt029 > 1 && stedt.CompareTo("20090401") >= 0)
            {
                bool ok = false;
                if (p_keyN.QFYCD == "31" || p_keyN.QFYCD == "38")
                {
                    ok = true;
                }
                else if (p_keyN.QFYCD == "32" || p_keyN.QFYCD == "39")
                {
                    if (hosjong == "4")
                    {
                        ok = true;
                    }
                    else
                    {
                        if (pdiv == "L" || pdiv == "2" || pdiv == "I" || pdiv == "H") ok = true;
                    }
                }
                else if (IsCHASANG2(p_keyN.QFYCD, gonsgb) == true)
                {
                    if (hosjong == "4")
                    {
                        ok = true;
                    }
                    else if (hosjong == "2" || hosjong == "3")
                    {
                        if (pdiv == "L" || IsMANSUNG(tjkh) == true) ok = true;
                    }
                }
                if (ok == true)
                {
                    m_pgm_step = "Merge1J(10)";
                    InsTI1J(p_keyN, ++inscount, "MT029", mt029.ToString(), p_conn, p_tran);
                }
            }
            // -----------------------------------------------------------------------------------------
            // 자동발생시키지 않는 특정내역을 읽어서 쓴다.
            // -----------------------------------------------------------------------------------------
            int ms001=0;
            int ms002=0;
            sql = "";
            sql = sql + Environment.NewLine + "SELECT 1 AS SORTNO,SEQ,TJCD,TJCDRMK ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1J + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND ELINENO=0 ";
            sql = sql + Environment.NewLine + " UNION ALL ";
            sql = sql + Environment.NewLine + "SELECT 2 AS SORTNO,SEQ,TJCD,TJCDRMK ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1J + " ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyN.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";
            sql = sql + Environment.NewLine + "   AND ELINENO=0 ";
            sql = sql + Environment.NewLine + " ORDER BY SORTNO,SEQ ";

            m_pgm_step = "Merge1J(11)";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                string tjcd = row["TJCD"].ToString();
                string tjcdrmk = row["TJCDRMK"].ToString();
                int tjcdrmk_value = 0;

                if (tjcd == "") return true;
                if (tjcd == "MT001") return true;
                if (tjcd == "MT002") return true;
                if (tjcd == "MT017") return true;
                if (tjcd == "MT018") return true;
                if (tjcd == "MT019") return true;
                if (tjcd == "MT020") return true;
                if (tjcd == "MT021") return true;
                if (tjcd == "MX999")
                {
                    if (tjcdrmk == "보훈환자") return true;
                    if (tjcdrmk == "다른 의료급여기관 정신질환자 진료의뢰건") return true;
                }
                if (tjcd == "MS001")
                {
                    int.TryParse(tjcdrmk, out tjcdrmk_value);
                    ms001 += tjcdrmk_value;
                    return true;
                }
                if (tjcd == "MS002")
                {
                    int.TryParse(tjcdrmk, out tjcdrmk_value);
                    ms002 += tjcdrmk_value;
                    return true;
                }
                m_pgm_step = "Merge1J(12)";
                InsTI1J(p_keyN, ++inscount, tjcd, tjcdrmk, p_conn, p_tran);

                return true;
            });

            if (ms001 > 0)
            {
                m_pgm_step = "Merge1J(13)";
                string ms001_str = ms001.ToString();
                if (ms001_str.Length == 1) ms001_str = "00" + ms001_str;
                else if (ms001_str.Length == 2) ms001_str = "0" + ms001_str;
                InsTI1J(p_keyN, ++inscount, "MS001", ms001_str, p_conn, p_tran);
            }
            if (ms002 > 0)
            {
                m_pgm_step = "Merge1J(14)";
                string ms002_str = ms002.ToString();
                if (ms002_str.Length == 1) ms002_str = "00" + ms002_str;
                else if (ms002_str.Length == 2) ms002_str = "0" + ms002_str;
                InsTI1J(p_keyN, ++inscount, "MS002", ms002_str, p_conn, p_tran);
            }
            // -----------------------------------------------------------------------------------------
            // 2005.08.18 NSK - 보훈환자인 경우 특정내역(MX999) 자동 발생하기 위함
            // 2009.11.10 WOOIL - 보훈환자여부를 공상구분으로 판단함.(BOHUN->GONSGB)
            // -----------------------------------------------------------------------------------------
            if (gonsgb == "4" || gonsgb == "7")
            {
                if (mx999 == "1")
                {
                    m_pgm_step = "Merge1J(15)";
                    InsTI1J(p_keyN, ++inscount, "MX999", "보훈환자", p_conn, p_tran);
                }
            }
            // -----------------------------------------------------------------------------------------
            // MX999 다른 의료급여기관 정신질환자 진료의뢰건
            // -----------------------------------------------------------------------------------------
            if (daetc == "E")
            {
                m_pgm_step = "Merge1J(16)";
                InsTI1J(p_keyN, ++inscount, "MX999", "다른 의료급여기관 정신질환자 진료의뢰건", p_conn, p_tran);
            }
        }

        private void Merge20(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string new_simtext="";
            string sql="";
            sql="";
            sql = sql + Environment.NewLine + "SELECT SIMTEXT ";
            sql = sql + Environment.NewLine + "  FROM TI20 ";
            sql = sql + Environment.NewLine + " WHERE IOFG='" + p_keyA.IOFG + "'";
            sql = sql + Environment.NewLine + "   AND K1 ='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND K2 ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND K3 ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND K4 ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND K5 ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND K6 ='" + p_keyA.SIMCS + "' ";
            sql = sql + Environment.NewLine + " UNION ALL";
            sql = sql + Environment.NewLine + "SELECT SIMTEXT ";
            sql = sql + Environment.NewLine + "  FROM TI20 ";
            sql = sql + Environment.NewLine + " WHERE IOFG='" + p_keyB.IOFG + "'";
            sql = sql + Environment.NewLine + "   AND K1 ='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND K2 ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND K3 ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND K4 ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND K5 ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND K6 ='" + p_keyB.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (new_simtext == "")
                        {
                            new_simtext = reader["SIMTEXT"].ToString();
                        }
                        else
                        {
                            new_simtext += " " + reader["SIMTEXT"].ToString();
                        }
                    }
                    reader.Close();
                }
            }

            if (new_simtext != "")
            {
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO TI20(IOFG,K1,K2,K3,K4,K5,K6,SIMTEXT)";
                sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?)";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_keyN.IOFG));
                    cmd.Parameters.Add(new OleDbParameter("@2", p_keyN.EXDATE));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_keyN.QFYCD));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_keyN.JRBY));
                    cmd.Parameters.Add(new OleDbParameter("@5", p_keyN.PID));
                    cmd.Parameters.Add(new OleDbParameter("@6", p_keyN.UNISQ));
                    cmd.Parameters.Add(new OleDbParameter("@7", p_keyN.SIMCS));
                    cmd.Parameters.Add(new OleDbParameter("@8", new_simtext));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SetDonfgX(CKey p_keyA, CKey p_keyB, CKey p_keyN, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "UPDATE " + tTI1A + " ";
            sql = sql + Environment.NewLine + "   SET DONFG='X' ";
            sql = sql + Environment.NewLine + "     , REMARK='##명세서합치기작업을 했습니다(명세서A).## ' + ISNULL(REMARK,'') ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyA.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyA.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyA.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyA.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyA.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyA.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            sql = "";
            sql = sql + Environment.NewLine + "UPDATE " + tTI1A + " ";
            sql = sql + Environment.NewLine + "   SET DONFG='X' ";
            sql = sql + Environment.NewLine + "     , REMARK='##명세서합치기작업을 했습니다(명세서B).## ' + ISNULL(REMARK,'') ";
            sql = sql + Environment.NewLine + " WHERE " + fEXDATE + "='" + p_keyB.EXDATE + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD ='" + p_keyB.QFYCD + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  ='" + p_keyB.JRBY + "' ";
            sql = sql + Environment.NewLine + "   AND PID   ='" + p_keyB.PID + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ ='" + p_keyB.UNISQ + "' ";
            sql = sql + Environment.NewLine + "   AND SIMCS ='" + p_keyB.SIMCS + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveTT55(CTT55 p_t55, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int cnt=0;
            p_t55.ENTTM = MetroLib.Util.GetSysTime(p_conn, p_tran);
            p_t55.ENDTM = p_t55.ENTTM;
            // 키중복방지용
            string sql="";
            while (true)
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT COUNT(*) AS CNT ";
                sql = sql + Environment.NewLine + "  FROM TT55 ";
                sql = sql + Environment.NewLine + " WHERE ENTDT='" + p_t55.ENTDT + "'";
                sql = sql + Environment.NewLine + "   AND PID='" + p_t55.PID + "'";
                sql = sql + Environment.NewLine + "   AND BEDEDT='" + p_t55.BEDEDT + "'";
                sql = sql + Environment.NewLine + "   AND ENTTM='" + p_t55.ENTTM + "'";

                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) int.TryParse(reader["CNT"].ToString(), out cnt);
                        reader.Close();
                    }
                }
                // 업으면 사용
                if (cnt <= 0) break;

                // 만약있으면 ... 시간을 증가사켜 다시 았는지 검사
                int tm = 0;
                int.TryParse(p_t55.ENTTM, out tm);
                tm += 1000001;
                p_t55.ENTTM = tm.ToString().Substring(1, 6);
            }

            // 저장
            sql="";
            sql = sql + Environment.NewLine + "INSERT INTO TT55(ENTDT,PID,BEDEDT,ENTTM,DPTCD,ENDTM,EMPID,EMPNM,PRGID,WORKNM,REMARK,PRGID2) ";
            sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", p_t55.ENTDT));
                cmd.Parameters.Add(new OleDbParameter("@2", p_t55.PID));
                cmd.Parameters.Add(new OleDbParameter("@3", p_t55.BEDEDT));
                cmd.Parameters.Add(new OleDbParameter("@4", p_t55.ENTTM));
                cmd.Parameters.Add(new OleDbParameter("@5", p_t55.DPTCD));
                cmd.Parameters.Add(new OleDbParameter("@6", p_t55.ENDTM));
                cmd.Parameters.Add(new OleDbParameter("@7", p_t55.EMPID));
                cmd.Parameters.Add(new OleDbParameter("@8", p_t55.EMPNM));
                cmd.Parameters.Add(new OleDbParameter("@9", p_t55.PRGID));
                cmd.Parameters.Add(new OleDbParameter("@10", p_t55.WORKNM));
                cmd.Parameters.Add(new OleDbParameter("@11", p_t55.REMARK));
                cmd.Parameters.Add(new OleDbParameter("@12", p_t55.PRGID2));

                cmd.ExecuteNonQuery();
            }
        }

        private void InsTI1J(CKey p_keyN, int p_seq, string p_tjcd, string p_tjcdrmk, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI1J = "TI1J";
            string fEXDATE = "EXDATE";
            if (p_keyN.IOFG == "2")
            {
                tTI1J = "TI2J";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO " + tTI1J + "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,ELINENO,SEQ,TJCD,TJCDRMK) ";
            sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", p_keyN.EXDATE));
                cmd.Parameters.Add(new OleDbParameter("@2", p_keyN.QFYCD));
                cmd.Parameters.Add(new OleDbParameter("@3", p_keyN.JRBY));
                cmd.Parameters.Add(new OleDbParameter("@4", p_keyN.PID));
                cmd.Parameters.Add(new OleDbParameter("@5", p_keyN.UNISQ));
                cmd.Parameters.Add(new OleDbParameter("@6", p_keyN.SIMCS));
                cmd.Parameters.Add(new OleDbParameter("@7", "0"));
                cmd.Parameters.Add(new OleDbParameter("@8", p_seq));
                cmd.Parameters.Add(new OleDbParameter("@9", p_tjcd));
                cmd.Parameters.Add(new OleDbParameter("@10", p_tjcdrmk));

                cmd.ExecuteNonQuery();
            }
        }

        private string GetHospitalJong(string p_exdt, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int cnt = 0;
            string ret = "";

            string sql="";
            sql="";
            sql = sql + Environment.NewLine + "SELECT COUNT(*) CNT ";
            sql = sql + Environment.NewLine + "  FROM TA88A ";
            sql = sql + Environment.NewLine + " WHERE MST1CD = 'A' ";
            sql = sql + Environment.NewLine + "   AND MST2CD = 'HOSPITAL' ";
            sql = sql + Environment.NewLine + "   AND MST3CD = '4' ";
            sql = sql + Environment.NewLine + "   AND MST4CD = ( SELECT MAX(X.MST4CD) ";
            sql = sql + Environment.NewLine + "                    FROM TA88A X ";
            sql = sql + Environment.NewLine + "                   WHERE X.MST1CD  = 'A'";
            sql = sql + Environment.NewLine + "                     AND X.MST2CD  = 'HOSPITAL' ";
            sql = sql + Environment.NewLine + "                     AND X.MST3CD  = '4' ";
            sql = sql + Environment.NewLine + "                     AND X.MST4CD <= '" + p_exdt + "' ";
            sql = sql + Environment.NewLine + "                )";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) int.TryParse(reader["CNT"].ToString(), out cnt);
                }
            }

            if (cnt > 0)
            {
                //READ TA88A
                sql = "";
                sql = sql + Environment.NewLine + "SELECT FLD1CD ";
                sql = sql + Environment.NewLine + "  FROM TA88A ";
                sql = sql + Environment.NewLine + " WHERE MST1CD = 'A' ";
                sql = sql + Environment.NewLine + "   AND MST2CD = 'HOSPITAL' ";
                sql = sql + Environment.NewLine + "   AND MST3CD = '4' ";
                sql = sql + Environment.NewLine + "   AND MST4CD = ( SELECT MAX(X.MST4CD) ";
                sql = sql + Environment.NewLine + "                    FROM TA88A X ";
                sql = sql + Environment.NewLine + "                   WHERE X.MST1CD  = 'A' ";
                sql = sql + Environment.NewLine + "                     AND X.MST2CD  = 'HOSPITAL' ";
                sql = sql + Environment.NewLine + "                     AND X.MST3CD  = '4' ";
                sql = sql + Environment.NewLine + "                     AND X.MST4CD <= '" + p_exdt + "' ";
                sql = sql + Environment.NewLine + "                )";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT FLD1CD ";
                sql = sql + Environment.NewLine + "  FROM TA88 ";
                sql = sql + Environment.NewLine + " WHERE MST1CD='A'";
                sql = sql + Environment.NewLine + "   AND MST2CD='HOSPITAL' ";
                sql = sql + Environment.NewLine + "   AND MST3CD='4' ";
            }

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) ret = reader["FLD1CD"].ToString();
                }
            }

            return ret;
        }

        private string ReadTA88_hospital(string p_key, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string ret = "";

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT FLD2QTY ";
            sql = sql + Environment.NewLine + "  FROM TA88 ";
            sql = sql + Environment.NewLine + " WHERE MST1CD='A'";
            sql = sql + Environment.NewLine + "   AND MST2CD='HOSPITAL' ";
            sql = sql + Environment.NewLine + "   AND MST3CD='" + p_key + "' ";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) ret = reader["FLD2QTY"].ToString();
                }
            }

            return ret;
        }

        private bool IsCHASANG2(string p_qfycd, string p_gonsgb)
        {
            if (p_qfycd.StartsWith("2"))
            {
                if (p_gonsgb == "E") return true;
                if (p_gonsgb == "F") return true;
            }
            return false;
        }

        private bool IsMANSUNG(string p_tjkh)
        {
            if (p_tjkh == "V001") return true; // 혈우병,인공신장투석 PDIV='2'
            if (p_tjkh == "V003") return true; // 복막투석 PDIV='I'
            if (p_tjkh == "V005") return true; //
            if (p_tjkh == "V009") return true;
            if (p_tjkh == "V012") return true;
            if (p_tjkh == "V013") return true;
            if (p_tjkh == "V014") return true;
            if (p_tjkh == "V015") return true;
            if (p_tjkh == "V117") return true;
            if (p_tjkh == "V027") return true;
            return false;
        }

        private string ToInt(string p_val)
        {
            int ret = 0;
            int.TryParse(p_val, out ret);
            return ret.ToString();
        }

        private void grdMainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "OP") return;
            if ((bool)e.Value == false) return;

            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                if (row != e.RowHandle)
                {
                    if ((bool)grdMainView.GetRowCellValue(row, gcOP) == true)
                    {
                        grdMainView.SetRowCellValue(row, gcOP, false);
                    }
                }
            }
        }

        private void grdSubView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "OP") return;
            if ((bool)e.Value == false) return;

            for (int row = 0; row < grdSubView.RowCount; row++)
            {
                if (row != e.RowHandle)
                {
                    if ((bool)grdSubView.GetRowCellValue(row, gcOP_SUB) == true)
                    {
                        grdSubView.SetRowCellValue(row, gcOP_SUB, false);
                    }
                }
            }
        }

        private void txtPid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            if (txtPid.Text.ToString() == "") return;
            ReadPnm();
        }

        private void txtPid_Leave(object sender, EventArgs e)
        {
            if (txtPid.Text.ToString() == "") return;
            ReadPnm();
        }

        private void ReadPnm()
        {
            if (txtPid.Text.ToString().Length < 9)
            {
                string pid = txtPid.Text.ToString();
                pid = pid.PadLeft(9, '0');
                txtPid.Text = pid;
            }

            ReadTA01(txtPid.Text.ToString());
        }

        private void ReadTA01(string p_pid)
        {
            string sql = "";
            sql = "";
            sql = "SELECT PNM FROM TA01 WHERE PID='" + p_pid + "'";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) txtPnm.Text = reader["PNM"].ToString();
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }

    }
}

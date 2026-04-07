using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0706E
{
    public partial class ADD0706E_1 : Form
    {
        public string m_User;
        public string m_demym;
        public string m_demdd;

        private Boolean IsFirst;

        public ADD0706E_1()
        {
            InitializeComponent();
        }

        private void ADD0706E_2_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0706E_2_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtDemym.Text = m_demym;
            txtDemdd.Text = m_demdd;

            string demyy = m_demdd.Substring(0,4);
            string bungi = m_demdd.Substring(4);

            string newCredt="";

            if (bungi == "14") newCredt = demyy + "0501";
            else if (bungi == "24") newCredt = demyy + "0801";
            else if (bungi == "34") newCredt = demyy + "1101";
            else if (bungi == "44")
            {
                int yyyy = 0;
                bool b = int.TryParse(demyy, out yyyy);
                newCredt = (yyyy + 1).ToString() + "0201";
            }
            txtNewCredt.Text = newCredt;

            Application.DoEvents();
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
            string demym = txtDemym.Text.ToString();
            string newCredt = txtNewCredt.Text.ToString();

            List<CDataSuga> list = new List<CDataSuga>();
            grdSuga.DataSource = null;
            grdSuga.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_H0802 ";
                sql += Environment.NewLine + " WHERE DEMYM='" + demym + "'";
                sql += Environment.NewLine + " ORDER BY CONVERT(NUMERIC,PRODNO) ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string itemcd = reader["ITEMCD"].ToString();
                    string iteminfo = reader["ITEMINFO"].ToString();
                    long addavr = MetroLib.StrHelper.ToLong(reader["ADDAVR"].ToString());

                    if (addavr == 0) return MetroLib.SqlHelper.CONTINUE;

                    this.ShowProgressForm("", "조회 중입니다. (" + itemcd + ")");

                    string sql2 = "";
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT * ";
                    sql2 += Environment.NewLine + "  FROM TA02 A02 WITH (NOLOCK) ";
                    sql2 += Environment.NewLine + " WHERE A02.ISPCD='" + itemcd + "'";
                    sql2 += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(Z.CREDT) FROM TA02 Z WHERE Z.PRICD=A02.PRICD AND Z.CREDT<='" + newCredt + "') ";
                    sql2 += Environment.NewLine + "   AND ISNULL(EXPDT,'') =''"; //2020.11.02 PHH - 폐기코드 제외
                    sql2 += Environment.NewLine + "   AND ISNULL(REFFG,'') NOT IN ('T')"; // 2020.11.02 PHH - 위탁진료 제외
                    sql2 += Environment.NewLine + " ORDER BY A02.PRICD ";

                    MetroLib.SqlHelper.GetDataReader(sql2, conn, delegate(OleDbDataReader reader2)
                    {
                        string ialwf = reader2["IALWF"].ToString();
                        if (ialwf == "1" || ialwf == "2") return true; // 비급여, 비보험 제외

                        long ipamt = MetroLib.StrHelper.ToLong(reader2["IPAMT"].ToString());
                        if (ipamt <= 0) return true; // 단가가 0원이면 제외

                        string pricd = reader2["PRICD"].ToString();
                        if (pricd.StartsWith("M"))
                        {
                            string a84_drgcd = "";
                            string a84_credt = "";
                            string a84_expdt = "";
                            int a84_cnt = ReadTA84(pricd, newCredt, conn, out a84_drgcd, out a84_credt, out a84_expdt);
                            if (a84_expdt != "" && String.Compare(a84_expdt, newCredt) <= 0) return true;
                        }
                        else
                        {
                            string a18_ocd = "";
                            string a18_credt = "";
                            string a18_expdt = "";
                            int a18_cnt = ReadTA18(pricd, newCredt, conn, out a18_ocd, out a18_credt, out a18_expdt);
                            if (a18_expdt != "" && String.Compare(a18_expdt, newCredt) <= 0) return true;
                        }

                        long i09_kumak = 0;
                        long i09_lkumak = 0;
                        int i09_cnt = ReadTI09(itemcd, newCredt, conn, out i09_kumak, out i09_lkumak);

                        if (i09_cnt < 1) return true;

                        string gpfix = reader2["GPFIX"].ToString();
                        string mchval = reader2["MCHVAL"].ToString();

                        // 가중평균가를 의보수가로 만든다.
                        long ipamt_new = addavr;
                        
                        // 가중평균가가 상한가보다 크면 상한가를 의보수가로 만든다.
                        if (ipamt_new > i09_kumak) ipamt_new = i09_kumak;
                        
                        //' 환산치가 있으면 적용
                        double dMchval = 0;
                        double.TryParse(mchval, out dMchval);
                        if (dMchval > 0)
                        {
                            ipamt_new = (long)Math.Round(ipamt_new * dMchval, MidpointRounding.AwayFromZero);
                        }
                            
                        // 일반수가고정이면 기존금액을 사용한다.
                        // 아니면, 의보수가에 배율을 곱하여 구한다.
                        long gpamt_new = 0;
                        if (gpfix == "1")
                        {
                            gpamt_new = MetroLib.StrHelper.ToLong(reader2["GPAMT"].ToString());
                        }
                        else
                        {
                            gpamt_new = (long)Math.Round(ipamt_new * GetTimes(ipamt_new, conn), MidpointRounding.AwayFromZero);
                        }

                        CDataSuga data = new CDataSuga();
                        data.Clear();

                        data.SEL = true;
                        data.EDICODE = itemcd;
                        data.EDINAME = iteminfo;
                        data.ADDAVR = addavr;
                        data.EDIAMT = i09_kumak;
                        data.LKUMAK = i09_lkumak;
                        data.PRICD = pricd;
                        data.PRKNM = reader2["PRKNM"].ToString();
                        data.IPAMT_NEW = ipamt_new;
                        data.GPAMT_NEW = gpamt_new;
                        data.CREDT_OLD = reader2["CREDT"].ToString();
                        data.IPAMT_OLD = MetroLib.StrHelper.ToLong(reader2["IPAMT"].ToString());
                        data.GPAMT_OLD = MetroLib.StrHelper.ToLong(reader2["GPAMT"].ToString());
                        data.GPFIX = gpfix;
                        data.MCHVAL = mchval;

                        list.Add(data);

                        return true;
                    });

                    return true;
                });
            }

            RefreshGridSuga();
        }

        private int ReadTA84(string p_drgcd, string p_newCredt, OleDbConnection p_conn, out string p_a84_drgcd, out string p_a84_credt, out string p_a84_expdt)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT * ";
            sql += Environment.NewLine + "  FROM TA84 A84 (NOLOCK) ";
            sql += Environment.NewLine + " WHERE A84.DRGCD='" + p_drgcd + "'";
            sql += Environment.NewLine + "   AND A84.CREDT=(SELECT MAX(Z.CREDT) FROM TA84 Z (NOLOCK) WHERE Z.DRGCD=A84.DRGCD AND Z.CREDT<='" + p_newCredt + "') ";

            int a84_cnt = 0;
            string a84_drgcd = "";
            string a84_credt = "";
            string a84_expdt = "";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                a84_cnt++;
                a84_drgcd = reader["DRGCD"].ToString();
                a84_credt = reader["CREDT"].ToString();
                a84_expdt = reader["EXPDT"].ToString();

                return true;
            });

            p_a84_drgcd = a84_drgcd;
            p_a84_credt = a84_credt;
            p_a84_expdt = a84_expdt;

            return a84_cnt;
        }

        private int ReadTA18(string p_ocd, string p_newCredt, OleDbConnection p_conn, out string p_a18_ocd, out string p_a18_credt, out string p_a18_expdt)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT * ";
            sql += Environment.NewLine + "  FROM TA18 A18 (NOLOCK) ";
            sql += Environment.NewLine + " WHERE A18.OCD='" + p_ocd + "'";
            sql += Environment.NewLine + "   AND A18.CREDT=(SELECT MAX(Z.CREDT) FROM TA18 Z (NOLOCK) WHERE Z.OCD=A18.OCD AND Z.CREDT<='" + p_newCredt + "') ";

            int a18_cnt = 0;
            string a18_ocd = "";
            string a18_credt = "";
            string a18_expdt = "";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                a18_cnt++;
                a18_ocd = reader["OCD"].ToString();
                a18_credt = reader["CREDT"].ToString();
                a18_expdt = reader["EXPDT"].ToString();

                return true;
            });

            p_a18_ocd = a18_ocd;
            p_a18_credt = a18_credt;
            p_a18_expdt = a18_expdt;

            return a18_cnt;
        }

        private int ReadTI09(string p_pcode, string p_newCredt, OleDbConnection p_conn, out long p_i09_kumak, out long p_i09_lkumak)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT * ";
            sql += Environment.NewLine + "  FROM TI09 I09 WITH (NOLOCK) ";
            sql += Environment.NewLine + " WHERE I09.PCODE='" + p_pcode + "'";
            sql += Environment.NewLine + "   AND I09.GUBUN='3' ";
            sql += Environment.NewLine + "   AND I09.ADTDT=(SELECT MAX(X.ADTDT) FROM TI09 X (NOLOCK) WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN AND X.ADTDT<='" + p_newCredt + "')";
            
            int i09_cnt = 0;
            long i09_kumak = 0;
            long i09_lkumak = 0;

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                i09_cnt++;
                i09_kumak = MetroLib.StrHelper.ToLong(reader["KUMAK"].ToString());
                i09_lkumak = MetroLib.StrHelper.ToLong(reader["LKUMAK"].ToString());

                return true;
            });

            p_i09_kumak = i09_kumak;
            p_i09_lkumak = i09_lkumak;

            return i09_cnt;
        }

        private double GetTimes(long p_amt, OleDbConnection p_conn)
        {
            double ret = 1.0;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT FLD1QTY ";
            sql += Environment.NewLine + "  FROM TA88 ";
            sql += Environment.NewLine + " WHERE MST1CD='A' ";
            sql += Environment.NewLine + "   AND MST2CD='MPRT' ";
            sql += Environment.NewLine + "   AND " + p_amt + " BETWEEN FLD1CD AND (CASE WHEN FLD2CD='*' THEN 999999999 ELSE FLD2CD END) ";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = MetroLib.StrHelper.ToDouble(reader["FLD1QTY"].ToString());
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

        private void RefreshGridSuga()
        {
            if (grdSuga.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSuga.BeginInvoke(new Action(() => grdSugaView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSugaView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("작업이 완료되엇습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Save()
        {
            string new_credt = txtNewCredt.Text.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try{
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string hdate = MetroLib.Util.GetSysDate(conn, tran);
                    string htime = MetroLib.Util.GetSysTime(conn, tran);

                    for (int i = 0; i < grdSugaView.RowCount; i++)
                    {
                        string pricd = grdSugaView.GetRowCellValue(i,"PRICD").ToString();
                        string credt_old = grdSugaView.GetRowCellValue(i,"CREDT_OLD").ToString();
                        string ipamt_new = grdSugaView.GetRowCellValue(i,"IPAMT_NEW").ToString();
                        string gpamt_new = grdSugaView.GetRowCellValue(i,"GPAMT_NEW").ToString();
                        string ediamt = grdSugaView.GetRowCellValue(i,"EDIAMT").ToString();
                        string lkumak = grdSugaView.GetRowCellValue(i,"LKUMAK").ToString();

                        this.ShowProgressForm("", "생성 중입니다. (" + pricd + ")");

                        if(pricd=="") continue;

                        if (credt_old.CompareTo(new_credt) < 0)
                        {
                            MkNew(pricd, credt_old, ipamt_new, gpamt_new, ediamt, lkumak, hdate, htime, conn, tran);
                        }
                        if (credt_old == new_credt)
                        {
                            MkUpd(pricd, credt_old, ipamt_new, gpamt_new, ediamt, lkumak, hdate, htime, conn, tran);
                        }
                    }
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

        private void MkNew(string p_pricd, string p_credt, string p_ipamt, string p_gpamt, string p_ediamt, string p_lkumak, string p_hdate, string p_htime, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TA02(";
            sql += Environment.NewLine + "       PRICD,ISFNM,PRKNM,EXPDT,ACTFG,ISPCD,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GSPAM,GSPRT,GSBRT,GSERT,IALWF,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NSPAM,NSPRT,NSBRT,NSERT,LABDV,SUGFG,REFCD,CODEGB,CHGDT,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG";
            sql += Environment.NewLine + "     , CREDT,ENTDT,IPAMT,CPAMT,DPAMT,NPAMT,GPAMT,EDIAMT,LKUMAK,EMPID,SYSDT,SYSTM) ";
            sql += Environment.NewLine + "SELECT PRICD,ISFNM,PRKNM,EXPDT,ACTFG,ISPCD,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GSPAM,GSPRT,GSBRT,GSERT,IALWF,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NSPAM,NSPRT,NSBRT,NSERT,LABDV,SUGFG,REFCD,CODEGB,CHGDT,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG";
            sql += Environment.NewLine + "     , ?,?,?,?,?,?,?,?,?,?,?,?";
            sql += Environment.NewLine + "  FROM TA02 ";
            sql += Environment.NewLine + " WHERE PRICD='" + p_pricd + "' ";
            sql += Environment.NewLine + "   AND CREDT='" + p_credt + "' ";

            List<Object> para = new List<object>();
            para.Add(txtNewCredt.Text.ToString());
            para.Add(p_hdate);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_gpamt);
            para.Add(p_ediamt);
            para.Add(p_lkumak);
            para.Add(m_User);
            para.Add(p_hdate);
            para.Add(p_htime);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

        }

        private void MkUpd(string p_pricd, string p_credt, string p_ipamt, string p_gpamt, string p_ediamt, string p_lkumak, string p_hdate, string p_htime, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            // ---------------------------------------
            // TA02 수정 백업
            // ---------------------------------------
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT TA02_HX(";
            sql += Environment.NewLine + "       HXID,HXDT,HXTM,HXRMK,";
            sql += Environment.NewLine + "       PRICD,CREDT,ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,";
            sql += Environment.NewLine + "       NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG) ";
            sql += Environment.NewLine + "SELECT ?,?,?,?,";
            sql += Environment.NewLine + "       PRICD,CREDT,ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,";
            sql += Environment.NewLine + "       NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG";
            sql += Environment.NewLine + "  FROM TA02 ";
            sql += Environment.NewLine + " WHERE PRICD='" + p_pricd + "'";
            sql += Environment.NewLine + "   AND CREDT='" + p_credt + "'";

            List<Object> para = new List<object>();
            para.Add(m_User);
            para.Add(p_hdate);
            para.Add(p_htime);
            para.Add("가중평균가적용");

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // ---------------------------------------
            // TA02 수정
            // ---------------------------------------
            sql = "";
            sql += Environment.NewLine + " UPDATE TA02 ";
            sql += Environment.NewLine + "   SET ENTDT=?";
            sql += Environment.NewLine + "     , IPAMT=?";
            sql += Environment.NewLine + "     , CPAMT=?";
            sql += Environment.NewLine + "     , DPAMT=?";
            sql += Environment.NewLine + "     , NPAMT=?";
            sql += Environment.NewLine + "     , GPAMT=?";
            sql += Environment.NewLine + "     , EDIAMT=?";
            sql += Environment.NewLine + "     , LKUMAK=?";
            sql += Environment.NewLine + "     , EMPID=?";
            sql += Environment.NewLine + "     , SYSDT=?";
            sql += Environment.NewLine + "     , SYSTM=?";
            sql += Environment.NewLine + " WHERE PRICD='" + p_pricd + "'";
            sql += Environment.NewLine + "   AND CREDT='" + p_credt + "'";

            para.Clear();
            para.Add(p_hdate);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_ipamt);
            para.Add(p_gpamt);
            para.Add(p_ediamt);
            para.Add(p_lkumak);
            para.Add(m_User);
            para.Add(p_hdate);
            para.Add(p_htime);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdSugaView.RowCount; i++)
            {
                grdSugaView.SetRowCellValue(i, "SEL", chkAll.Checked);
            }
        }
    }
}

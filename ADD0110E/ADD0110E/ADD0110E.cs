using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0110E
{
    public partial class ADD0110E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;

        private bool IsFirst;

        private string m_bf_bdodt = "";
        private string m_bf_qfycd = "";
        private string m_bf_jrby = "";
        private string m_bf_pid = "";
        private string m_bf_unisq = "";
        private string m_bf_simcs = "";

        private string m_auto_run = "";
        private string m_af_bdodt = "";
        private string m_af_qfycd = "";
        private string m_boryu = "";

        private string m_pgm_step = ""; // 2022.01.10 WOOIL - 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD0110E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
        }

        public ADD0110E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
        }

        private void ADD0110E_Load(object sender, EventArgs e)
        {
            IsFirst = true;

            SetQfycdCombo();
            if (m_Addpara != "") SetInitValues();
        }

        private void ADD0110E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            if (m_auto_run == "Y")
            {
                Application.DoEvents();

                txtAfterBdodt.Text = m_af_bdodt;
                if (m_af_qfycd != "")
                {
                    // 2022.04.26 WOOIL - 자격이 있는 경우만...
                    for (int i = 0; i < cboQfycd.Items.Count; i++)
                    {
                        if (cboQfycd.Items[i].ToString().StartsWith(m_af_qfycd))
                        {
                            cboQfycd.SelectedIndex = i;
                            break;
                        }
                    }
                }
                chkBoRyu.Checked = (m_boryu == "1");
                Application.DoEvents();

                btnSave.PerformClick();
                this.Close();
            }

        }

        private void SetQfycdCombo()
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT MST3CD,CDNM";
            sql += Environment.NewLine + "  FROM TA88 (NOLOCK)";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD='26'";
            sql += Environment.NewLine + " ORDER BY MST3CD";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cboQfycd.Items.Add(reader["MST3CD"].ToString() + "." + reader["CDNM"].ToString());
                    return true;
                });

                conn.Close();
            }
        }

        private void SetInitValues()
        {
            char d_lev1 = (char)21;
            string[] paras = m_Addpara.Split(d_lev1);

            int idx = 0; int.TryParse(paras[0], out idx);
            string exdate = paras[1];
            string qfycd = paras[2];
            string jrby = paras[3];
            string pid = paras[4];
            string unisq = (paras.Length >= 6 ? paras[5] : "");
            string simcs = (paras.Length >= 7 ? paras[6] : "");
            string auto_run = (paras.Length >= 8 ? paras[7] : ""); // 자동실행
            string af_bdodt = (paras.Length >= 9 ? paras[8] : ""); //복사후 청구월
            string af_qfycd = (paras.Length >= 10 ? paras[9] : ""); //복사후 자격
            string boryu = (paras.Length >= 11 ? paras[10] : ""); //복사후 보류시킬지여부

            //20210326 MADE BY PHH 진료과 조회
            string tTI1A = "";
            string fEXDATE = "";
            if (idx == 0 || idx == 1) //외래
            {
                tTI1A = "TI1A";
                fEXDATE = "EXDATE";
            }
            else if (idx == 2 || idx == 3) //입원
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }
            string dptcd = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT JRKWA";
            sql += Environment.NewLine + "  FROM " + tTI1A + "";
            sql += Environment.NewLine + " WHERE " + fEXDATE + " = '" + exdate + "'";
            sql += Environment.NewLine + "   AND QFYCD = '" + qfycd + "'";
            sql += Environment.NewLine + "   AND JRBY ='" + jrby + "'";
            sql += Environment.NewLine + "   AND PID ='" + pid + "'";
            sql += Environment.NewLine + "   AND UNISQ ='" + unisq + "'";
            sql += Environment.NewLine + "   AND SIMCS ='" + simcs + "'";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string jrkwa = reader["JRKWA"].ToString();
                    string[] val = jrkwa.Split('$');
                    dptcd = val[2];
                    return false;
                });

                conn.Close();
            }

            string exdate_af = "";
            if (idx == 0)
            {
                exdate_af = exdate; //외래->외래
            }
            else if (idx == 1)
            {
                exdate_af = DateDayLast(exdate); //외래->입원
            }
            else if (idx == 2)
            {
                exdate_af = exdate.Substring(0, 6); //입원->외래
            }
            else if (idx == 3)
            {
                exdate_af = exdate; //입원->입원
            }


            cboMode.SelectedIndex = idx;
            m_bf_bdodt = exdate;
            m_bf_qfycd = qfycd;
            m_bf_jrby = jrby;
            m_bf_pid = pid;
            m_bf_unisq = unisq;
            m_bf_simcs = simcs;

            txtAfterBdodt.Text = exdate_af;
            txtAfterQfycd.Text = qfycd;
            txtAfterDptcd.Text = dptcd;
            txtAfterPid.Text = pid;

            m_auto_run = auto_run;
            m_af_bdodt = af_bdodt;
            m_af_qfycd = af_qfycd;
            m_boryu = boryu;
        }

        private string DateDayLast(string p_date)
        {
            string yymm = p_date.Substring(0, 6);
            yymm += "01";
            DateTime dtDate = DateTime.ParseExact(yymm, "yyyyMMdd", null);
            dtDate = dtDate.AddMonths(1);
            dtDate = dtDate.AddDays(-1);
            string ret = dtDate.ToString("yyyyMMdd");
            return ret;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                m_pgm_step = "";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("작업이 완료되었습니다.");
                this.Close();
                m_pgm_step = "";
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "(" + m_pgm_step + ")");
                //this.Close();
                m_pgm_step = "";
            }
        }

        private void Save()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sysdt = MetroLib.Util.GetSysDate(conn, tran);
                    string systm = MetroLib.Util.GetSysTime(conn, tran);

                    string mode = cboMode.SelectedItem.ToString().Substring(0, 2);
                    string mode_bf = mode.Substring(0, 1); // 시작 테이블
                    string mode_af = mode.Substring(1, 1); // 도착 테이블
                    
                    string tTI2A_af = "";
                    string tTI2B_af = "";
                    string tTI2E_af = "";
                    string tTI2F_af = "";
                    string tTI2H_af = "";
                    string tTI2J_af = "";
                    string tTI23_af = "";
                    string tTI23T_af = "";
                    string tTI24_af = "";
                    string tTI2K_af = "";
                    string tTI2AR_af = "";
                    string fBDODT_af = "";
                    if (mode_af == "2")
                    {
                        tTI2A_af = "TI2A";
                        tTI2B_af = "TI2B";
                        tTI2E_af = "TI2E";
                        tTI2F_af = "TI2F";
                        tTI2H_af = "TI2H";
                        tTI2J_af = "TI2J";
                        tTI23_af = "TI23";
                        tTI23T_af = "TI23T";
                        tTI24_af = "TI24";
                        tTI2K_af = "TI2K";
                        tTI2AR_af = "TI2AR";
                        fBDODT_af = "BDODT";
                    }
                    else if (mode_af == "1")
                    {
                        tTI2A_af = "TI1A";
                        tTI2B_af = "TI1B";
                        tTI2E_af = "TI1E";
                        tTI2F_af = "TI1F";
                        tTI2H_af = "TI1H";
                        tTI2J_af = "TI1J";
                        tTI23_af = "TI13";
                        tTI23T_af = "TI13T";
                        tTI24_af = "TI14";
                        tTI2K_af = "TI1K";
                        tTI2AR_af = "TI1AR";
                        fBDODT_af = "EXDATE";
                    }
                    string tTI2A_bf = "";
                    string tTI2B_bf = "";
                    string tTI2E_bf = "";
                    string tTI2F_bf = "";
                    string tTI2H_bf = "";
                    string tTI2J_bf = "";
                    string tTI23_bf = "";
                    string tTI23T_bf = "";
                    string tTI24_bf = "";
                    string tTI2K_bf = "";
                    string tTI2AR_bf = "";
                    string fBDODT_bf = "";
                    if (mode_bf == "2")
                    {
                        tTI2A_bf = "TI2A";
                        tTI2B_bf = "TI2B";
                        tTI2E_bf = "TI2E";
                        tTI2F_bf = "TI2F";
                        tTI2H_bf = "TI2H";
                        tTI2J_bf = "TI2J";
                        tTI23_bf = "TI23";
                        tTI23T_bf = "TI23T";
                        tTI24_bf = "TI24";
                        tTI2K_bf = "TI2K";
                        tTI2AR_bf = "TI2AR";
                        fBDODT_bf = "BDODT";
                    }
                    else if (mode_bf == "1")
                    {
                        tTI2A_bf = "TI1A";
                        tTI2B_bf = "TI1B";
                        tTI2E_bf = "TI1E";
                        tTI2F_bf = "TI1F";
                        tTI2H_bf = "TI1H";
                        tTI2J_bf = "TI1J";
                        tTI23_bf = "TI13";
                        tTI23T_bf = "TI13T";
                        tTI24_bf = "TI14";
                        tTI2K_bf = "TI1K";
                        tTI2AR_bf = "TI1AR";
                        fBDODT_bf = "EXDATE";
                    }

                    string bf_bdodt = m_bf_bdodt;
                    string bf_qfycd = m_bf_qfycd;
                    string bf_jrby = m_bf_jrby;
                    string bf_pid = m_bf_pid;
                    string bf_unisq = m_bf_unisq;
                    string bf_simcs = m_bf_simcs;

                    string af_bdodt = txtAfterBdodt.Text.ToString();
                    string af_qfycd = txtAfterQfycd.Text.ToString();
                    string af_jrby = "";
                    string af_pid = txtAfterPid.Text.ToString();
                    string af_unisq = "";
                    string af_simcs = "1";

                    string af_dptcd = txtAfterDptcd.Text.ToString().ToUpper();
                    string boryu = chkBoRyu.Checked ? "1" : "0";

                    string bf_jrkwa = "";
                    string af_jrkwa = "";
                    // --------------------------------------------------------------------------
                    // 다른 자격으로 복사하는 경우
                    // --------------------------------------------------------------------------
                    string af_new_qfycd = "";
                    if (txtAfterQfycd.Text.ToString() != txtChangQF.Text.ToString() && txtChangQF.Text.ToString() != "")
                    {
                        af_new_qfycd = txtChangQF.Text.ToString();
                    }
                    if (af_new_qfycd != "") af_qfycd = af_new_qfycd;

                    // --------------------------------------------------------------------------
                    // JRBY를 만든다.
                    // --------------------------------------------------------------------------
                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT PRIMDPTCD,INSDPTCD,DPTCD,DPTNM";
                    sql += Environment.NewLine + "  FROM TA09";
                    sql += Environment.NewLine + " WHERE DPTCD='" + af_dptcd + "'";
                    sql += Environment.NewLine + "   AND PRIMDPTCD IN ('1','2','3','4','5','6','7')";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                    {
                        string primdptcd = reader["PRIMDPTCD"].ToString();
                        string insdptcd = reader["INSDPTCD"].ToString();
                        string dptcd = reader["DPTCD"].ToString();
                        string dptnm = reader["DPTNM"].ToString();

                        if (primdptcd != "" && insdptcd != "" && dptcd != "" && dptnm != "")
                        {
                            af_jrby = primdptcd + "$" + insdptcd;
                            af_jrkwa = primdptcd + "$" + insdptcd + "$" + dptcd + "$" + dptnm;
                        }
                        return false;
                    });
                    if (af_jrkwa == "")
                    {
                        throw new Exception("진료과 코드가 잘 못 되었습니다.");
                        //MessageBox.Show("진료과 코드가 잘 못 되었습니다.");
                        //return;
                    }

                    // --------------------------------------------------------------------------
                    // UNISQ를 만든다.
                    // --------------------------------------------------------------------------
                    long lUnisq = 9;
                    while (true)
                    {
                        af_unisq = lUnisq.ToString();

                        sql = "";
                        sql += Environment.NewLine + "SELECT COUNT(*) CNT";
                        sql += Environment.NewLine + "  FROM " + tTI2A_af + "";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY ='" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID  ='" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + lUnisq.ToString() + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                        int cnt = 0;
                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            int.TryParse(reader["CNT"].ToString(), out cnt);
                            return false;
                        });
                        if (cnt < 1) break;
                        lUnisq++;
                    }

                    // --------------------------------------------------------------------------
                    // 2008.02.27 WOOIL - 청구완료일자
                    // --------------------------------------------------------------------------
                    string compldt = "";
                    string demno = "";
                    string bededt = "";
                    string stedt = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT DEMNO,BDEDT,STEDT,JRKWA";
                    sql += Environment.NewLine + "  FROM " + tTI2A_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY ='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID  ='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                    {
                        demno = reader["DEMNO"].ToString();
                        bededt = reader["BDEDT"].ToString();
                        stedt = reader["STEDT"].ToString();
                        if (bededt == "") bededt = stedt;
                        bf_jrkwa = reader["JRKWA"].ToString();
                        return false;
                    });

                    if (demno != "")
                    {
                        sql = "";
                        sql += Environment.NewLine + "SELECT COMPLDT";
                        sql += Environment.NewLine + "  FROM TIE_H010";
                        sql += Environment.NewLine + " WHERE DEMNO = '" + demno + "'";
                        sql += Environment.NewLine + " ORDER BY COMPLDT DESC";

                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            compldt = reader["COMPLDT"].ToString();
                            return false;
                        });
                    }

                    // --------------------------------------------------------------------------
                    // TT55에 자료만들기
                    // --------------------------------------------------------------------------
                    string tt55_entdt = sysdt;
                    string tt55_pid = af_pid;
                    string tt55_bededt = bededt;
                    string tt55_enttm = systm;
                    string tt55_dptcd = af_dptcd;
                    string tt55_endtm = "";  // 끝날때 셋팅한다.
                    string tt55_empid = m_User;
                    string tt55_empnm = "";
                    string tt55_prgid = "ADD0110E";
                    string tt55_worknm = "명세서복사(" + mode + ")";
                    string tt55_remark = "";
                    tt55_remark += bf_bdodt + ",";
                    tt55_remark += bf_qfycd + ",";
                    tt55_remark += bf_jrby + ",";
                    tt55_remark += bf_pid + ",";
                    tt55_remark += bf_unisq + ",";
                    tt55_remark += bf_simcs + "->";
                    tt55_remark += af_bdodt + ",";
                    tt55_remark += af_qfycd + ",";
                    tt55_remark += af_jrby + ",";
                    tt55_remark += af_pid + ",";
                    tt55_remark += af_unisq + ",";
                    tt55_remark += af_simcs;

                    string fields_af = "";
                    string fields_bf = "";
                    // --------------------------------------------------------------------------
                    // CopyA
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyA";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "UNICD, UNINM, INSNM, PNM, INSID, RESID, PSEX, FMRCD, JBFG, DONFG, EMPID, DODHM, FINDHM, NBPID, PDIV, APRDT, GENDT,";
                    //fields_af += "TRANSFG, EPRTNO, ESAMNO, DELFG, MADDR, PRTFG, REMARK, SIMFG, YYMM, DEMNO, APPRNO,";
                    //fields_af += "JAJR, BOHUN, JANGAEFG, RPID, OPRFG, DAETC, TJKH, STEDT, JRKK, RSLT, DISEAPOS, BDEDT, FSTDT,";
                    //fields_af += "INSTRU, BEDODT, EXAMC, XDAYS, XDAYS2, CTCAK, MRICA, JSOGE, HSOGE, GSRT, GSGUM,";
                    //fields_af += "TTAMT, PTAMT, UNAMT, JAM, RELAM, CSCD, GSCD, XJDFG, XCRFG, IPATH, ADDZ1, ADDZ2, ADDZ3, ADDZ4, ADDZ5, ADDZ6, ADDZ7, ADDZ8, ADDZ9, PHYCDCHK,";
                    //fields_af += "PETAMT,BFEXDT,ARVPATH,EMDB,AMTCHK,RPTPTAMT,TT41KEY,HRFG,DAILYSUMFG,MAXAUTOFG,GBFRDT,GBTODT,SJSTEDT,SBRDNTYPE,CFHCCFRNO,YOFG,YOPDIV,JIWONAMT,MT020,GONSGB,MEDAMT,MAXPTAMT,HWTTAMT,YOGROUP,MT029,PDIVM,BOHUNDCFG,BOHUNDCCD,DAILYPTAMTFG,FOREIGNFG,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "UNICD, UNINM, INSNM, PNM, INSID, RESID, PSEX, FMRCD, JBFG, DONFG, '" + m_User + "', DODHM, FINDHM, NBPID, PDIV, APRDT, GENDT,";
                    //fields_bf += "'+', EPRTNO, ESAMNO, DELFG, MADDR, PRTFG, REMARK, SIMFG, YYMM, DEMNO, APPRNO,";
                    //fields_bf += "JAJR, BOHUN, JANGAEFG, RPID, OPRFG, DAETC, TJKH, STEDT, JRKK, RSLT, DISEAPOS, BDEDT, FSTDT,";
                    //fields_bf += "INSTRU, BEDODT, EXAMC, XDAYS, XDAYS2, CTCAK, MRICA, JSOGE, HSOGE, GSRT, GSGUM,";
                    //fields_bf += "TTAMT, PTAMT, UNAMT, JAM, RELAM, CSCD, GSCD, XJDFG, XCRFG, IPATH, ADDZ1, ADDZ2, ADDZ3, ADDZ4, ADDZ5, ADDZ6, ADDZ7, ADDZ8, ADDZ9, PHYCDCHK,";
                    //fields_bf += "PETAMT,BFEXDT,ARVPATH,EMDB,AMTCHK,RPTPTAMT,TT41KEY,HRFG,DAILYSUMFG,MAXAUTOFG,GBFRDT,GBTODT,SJSTEDT,SBRDNTYPE,CFHCCFRNO,YOFG,YOPDIV,JIWONAMT,MT020,GONSGB,MEDAMT,MAXPTAMT,HWTTAMT,YOGROUP,MT029,PDIVM,BOHUNDCFG,BOHUNDCCD,DAILYPTAMTFG,FOREIGNFG,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT";

                    GetTableFields(tTI2A_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2A_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2A_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    //System.IO.File.WriteAllText("C:/Metro/DLL/ADD0110E.log", sql, Encoding.Default);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // 2012.01.05 WOOIL - 산재는 무조건 070
                    if (af_qfycd == "50")
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                        sql += Environment.NewLine + "   SET SJ070='070'";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + " = '" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }
                    // 2012.01.05 WOOIL - 보험보호는 무조건 방문일자별
                    if (mode_af == "1")
                    {
                        if (af_qfycd.StartsWith("2") || af_qfycd.StartsWith("3") || af_qfycd.StartsWith("4"))
                        {
                            sql = "";
                            sql += Environment.NewLine + "UPDATE TI1A";
                            sql += Environment.NewLine + "   SET DAILYSUMFG='1',DAILYPTAMTFG=''";
                            sql += Environment.NewLine + " WHERE EXDATE = '" + af_bdodt + "'";
                            sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                            sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                            sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                            sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                            sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";

                            MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                        }
                    }
                    // 2022.01.11 WOOIL - 보험,보호가 아니면 DRGFG를 초기화한다.
                    if (af_qfycd.StartsWith("2") || af_qfycd.StartsWith("3") || af_qfycd.StartsWith("4"))
                    {
                    }
                    else
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                        sql += Environment.NewLine + "   SET DRGFG=''";
                        sql += Environment.NewLine + "     , DRGNO=''";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + " = '" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }
                    // 2022.01.11 WOOIL - 외래는 DRGFG를 초기화한다.
                    if (mode_af == "1")
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                        sql += Environment.NewLine + "   SET DRGFG=''";
                        sql += Environment.NewLine + "     , DRGNO=''";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + " = '" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }
                    // 2023.06.30 WOOIL - 과코드를 변경했을 수 있으므로 JRKWA를 업데이트한다.
                    if (bf_jrkwa != af_jrkwa)
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                        sql += Environment.NewLine + "   SET JRKWA='" + af_jrkwa + "'";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + " = '" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }

                    // 보류작업
                    if (boryu == "1" && compldt == "")
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_bf + "";
                        sql += Environment.NewLine + "   SET DONFG='P', REMARK='##대체작업을 했습니다.## ' + ISNULL(REMARK,'')";
                        sql += Environment.NewLine + " WHERE " + fBDODT_bf + " = '" + bf_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + bf_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + bf_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + bf_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + bf_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + bf_simcs + " ";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }

                    // --------------------------------------------------------------------------
                    // CopyB
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyB";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "SEQ1, DACD, DANM, TPOS1, TPOS2, TPOS3, TPOS4, JRKWA, IPWON, TGWON, PDRID,ROFG,DAEXDT,POA";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "SEQ1, DACD, DANM, TPOS1, TPOS2, TPOS3, TPOS4, JRKWA, IPWON, TGWON, PDRID,ROFG,DAEXDT,POA";

                    GetTableFields(tTI2B_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2B_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2B_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // 2023.06.30 WOOIL - 과코드를 변경했을 수 있으므로 JRKWA를 업데이트한다.
                    if (bf_jrkwa != af_jrkwa)
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2B_af + "";
                        sql += Environment.NewLine + "   SET JRKWA='" + af_jrkwa + "'";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + " = '" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD  = '" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY   = '" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID    = '" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ  = " + af_unisq + " ";
                        sql += Environment.NewLine + "   AND SIMCS  = " + af_simcs + " ";
                        sql += Environment.NewLine + "   AND SEQ1   = 1";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }

                    // --------------------------------------------------------------------------
                    // CopyE
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyE";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "SEQ1, SEQ2, CNT, JGMAK, HGMAK";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "SEQ1, SEQ2, CNT, JGMAK, HGMAK";

                    GetTableFields(tTI2E_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2E_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2E_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // CopyF
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyF";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "SEQ1, SEQ2, OP, PRICD, BGIHO, PRKNM, NTDIV, FCRFG, DANGA, DQTY, DDAY, GUMAK, EXDT, POS2, MAFG, ACTFG, EVENT, DRGCD,";
                    //fields_af += "STTEX, IPOS1, ALLEX, GRPCD, GRPACT, GRPNM, RSNCD, REMARK, FRDT, TODT, PRIDT, ELINENO,OKCD,REFCD,LOWFG,CDENTDT,";
                    //fields_af += "TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "SEQ1, SEQ2, OP, PRICD, BGIHO, PRKNM, NTDIV, FCRFG, DANGA, DQTY, DDAY, GUMAK, EXDT, POS2, MAFG, ACTFG, EVENT, DRGCD,";
                    //fields_bf += "STTEX, IPOS1, ALLEX, GRPCD, GRPACT, GRPNM, RSNCD, REMARK, FRDT, TODT, PRIDT, ELINENO,OKCD,REFCD,LOWFG,CDENTDT,";
                    //fields_bf += "TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO";

                    GetTableFields(tTI2F_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2F_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2F_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // 보험,보호,공상,자보에서 다른 자격(산재,거래처)로 복사할때는 
                    // 1회투약량(CNTQTY)가 없으므로 CNTQTY*DQTY를 하여 수량으로 만들어야 한다.
                    // 상한차액은 0으로 만든다.
                    if (bf_qfycd.StartsWith("2") || bf_qfycd.StartsWith("3") || bf_qfycd.StartsWith("4") || bf_qfycd.StartsWith("6"))
                    {
                        if (af_qfycd.StartsWith("2") || af_qfycd.StartsWith("3") || af_qfycd.StartsWith("4") || af_qfycd.StartsWith("6"))
                        {
                        }
                        else
                        {
                            sql = "";
                            sql += Environment.NewLine + "UPDATE " + tTI2F_af + "";
                            sql += Environment.NewLine + "   SET DQTY = (CASE WHEN ISNULL(CNTQTY,0)=0 THEN 1 ELSE CNTQTY END) * DQTY";
                            sql += Environment.NewLine + "     , UPLMTAMT = 0";
                            sql += Environment.NewLine + "     , UPLMTCHAAMT = 0";
                            sql += Environment.NewLine + "     , CNTQTY = 1";
                            sql += Environment.NewLine + "     , FRDT = LEFT(EXDT,8)";
                            sql += Environment.NewLine + "     , TODT = CONVERT(VARCHAR,DATEADD(DAY,DDAY-1,LEFT(EXDT,8)),112)";
                            sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                            sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                            sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                            sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                            sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                            sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                            MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                        }
                    }

                    // --------------------------------------------------------------------------
                    // CopyH
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyH";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "SEQ1, SEQ2, OP, PRICD, BGIHO, PRKNM, NTDIV, FCRFG, DANGA, DQTY, DDAY, GUMAK, EXDT, POS2, MAFG, ACTFG, EVENT, DRGCD,";
                    //fields_af += "STTEX, IPOS1, ALLEX, GRPCD, GRPACT, GRPNM, RSNCD, REMARK, FRDT, TODT, PRIDT, ELINENO,OKCD,REFCD,LOWFG,CDENTDT,";
                    //fields_af += "TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "SEQ1, SEQ2, OP, PRICD, BGIHO, PRKNM, NTDIV, FCRFG, DANGA, DQTY, DDAY, GUMAK, EXDT, POS2, MAFG, ACTFG, EVENT, DRGCD,";
                    //fields_bf += "STTEX, IPOS1, ALLEX, GRPCD, GRPACT, GRPNM, RSNCD, REMARK, FRDT, TODT, PRIDT, ELINENO,OKCD,REFCD,LOWFG,CDENTDT,";
                    //fields_bf += "TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO";

                    GetTableFields(tTI2H_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2H_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2H_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // CopyJ
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyJ";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "ELINENO, SEQ, TJCD, TJCDRMK,MAFG";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "ELINENO, SEQ, TJCD, TJCDRMK,MAFG";

                    GetTableFields(tTI2J_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2J_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2J_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // Copy23
                    // --------------------------------------------------------------------------
                    m_pgm_step = "Copy23";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "OUTSEQ, SEQ, PRICD, BGIHO, PRKNM, DANGA, DQTY, DDAY, GUMAK, ORDCNT, ELINENO,LOWFG,CDGB,ODAY,BAEKFG,LOWRSNCD,LOWRSNRMK";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "OUTSEQ, SEQ, PRICD, BGIHO, PRKNM, DANGA, DQTY, DDAY, GUMAK, ORDCNT, ELINENO,LOWFG,CDGB,ODAY,BAEKFG,LOWRSNCD,LOWRSNRMK";

                    GetTableFields(tTI23_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI23_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI23_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // Copy23T
                    // --------------------------------------------------------------------------
                    m_pgm_step = "Copy23T";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "OUTSEQ, SEQ, SEQNO,TJCD,TJCDRMK";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "OUTSEQ, SEQ, SEQNO,TJCD,TJCDRMK";

                    GetTableFields(tTI23T_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI23T_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI23T_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // Copy24
                    // --------------------------------------------------------------------------
                    m_pgm_step = "Copy24";
                    //fields_af = "";
                    //fields_af += fBDODT_af + " ,QFYCD,JRBY,PID,UNISQ,SIMCS,";
                    //fields_af += "SEQ,FG,PRICD,PRKNM,DQTY,DDAY,ORDCNT,LOWFG,CDGB,OUTSEQ,BGIHO,DANGA,GUMAK,ELINENO,ODAY,LOWRSNCD,LOWRSNRMK";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "SEQ,FG,PRICD,PRKNM,DQTY,DDAY,ORDCNT,LOWFG,CDGB,OUTSEQ,BGIHO,DANGA,GUMAK,ELINENO,ODAY,LOWRSNCD,LOWRSNRMK";

                    GetTableFields(tTI24_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI24_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI24_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // CopyDailyInfo
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyDailyInfo";
                    if (mode == "11" && bf_qfycd.StartsWith("2") && af_qfycd.StartsWith("2") && af_bdodt.CompareTo("200907") >= 0)
                    {
                        // TI1AA
                        fields_af = "";
                        fields_af += "EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,";
                        fields_af += "EXDT,MT020,MT029,CTCAK,MRICA,PETAMT,JSOGE,HSOGE,GSRT,GSGUM,TTAMT,PTAMT,UNAMT,JAM,MEDAMT,HWTTAMT,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TSJRAMT";

                        fields_bf = "";
                        fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                        fields_bf += "EXDT,MT020,MT029,CTCAK,MRICA,PETAMT,JSOGE,HSOGE,GSRT,GSGUM,TTAMT,PTAMT,UNAMT,JAM,MEDAMT,HWTTAMT,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TSJRAMT";

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TI1AA(" + fields_af + ")";
                        sql += Environment.NewLine + "SELECT " + fields_bf + "";
                        sql += Environment.NewLine + "  FROM TI1AA";
                        sql += Environment.NewLine + " WHERE EXDATE='" + bf_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                        // TI1EA
                        fields_af = "";
                        fields_af += "EXDATE, QFYCD, JRBY, PID, UNISQ, SIMCS,";
                        fields_af += "EXDT,SEQ1,SEQ2,CNT,JGMAK,HGMAK";

                        fields_bf = "";
                        fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                        fields_bf += "EXDT,SEQ1,SEQ2,CNT,JGMAK,HGMAK";

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TI1EA(" + fields_af + ")";
                        sql += Environment.NewLine + "SELECT " + fields_bf + "";
                        sql += Environment.NewLine + "  FROM TI1EA";
                        sql += Environment.NewLine + " WHERE EXDATE='" + bf_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                        // TI1FA
                        fields_af = "";
                        fields_af += "EXDATE,QFYCD,JRBY,PID,UNISQ,SIMCS,";
                        fields_af += "SEQNO,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1";

                        fields_bf = "";
                        fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                        fields_bf += "SEQNO,SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,JUBSUDT,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1";

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TI1FA(" + fields_af + ")";
                        sql += Environment.NewLine + "SELECT " + fields_bf + "";
                        sql += Environment.NewLine + "  FROM TI1FA";
                        sql += Environment.NewLine + " WHERE EXDATE='" + bf_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }
                    else
                    {
                        sql = "";
                        sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                        sql += Environment.NewLine + "   SET DAILYPTAMTFG = Null";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                        MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);
                    }

                    // --------------------------------------------------------------------------
                    // CopyK
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyK";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "OPRCD,OPRCD1,OPRCD2,OPRCD3,OPRCD4,OPRCD5,OPRCD6,OPRCD7,OPRCD8,OPRCD9,EXMCD1,EXMCD2,EXMCD3,EXMCD4,EXMCD5,RADCD1,RADCD2,RADCD3,RADCD4,RADCD5,INJCD1,INJCD2,INJCD3,INJCD4,INJCD5,ANECD1,ANECD2,ANECD3,ANECD4,ANECD5,DETDIV1,DETDIV2,DETDIV3,DETDIV4,DETDIV5,TOTAMT,PTAMT,INSAMT,JAM,CALCFG,ALCOL,WEIGHT,AHOUR,NTDATE,NTTIME,TOTAMT1";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "OPRCD,OPRCD1,OPRCD2,OPRCD3,OPRCD4,OPRCD5,OPRCD6,OPRCD7,OPRCD8,OPRCD9,EXMCD1,EXMCD2,EXMCD3,EXMCD4,EXMCD5,RADCD1,RADCD2,RADCD3,RADCD4,RADCD5,INJCD1,INJCD2,INJCD3,INJCD4,INJCD5,ANECD1,ANECD2,ANECD3,ANECD4,ANECD5,DETDIV1,DETDIV2,DETDIV3,DETDIV4,DETDIV5,TOTAMT,PTAMT,INSAMT,JAM,CALCFG,ALCOL,WEIGHT,AHOUR,NTDATE,NTTIME,TOTAMT1";

                    GetTableFields(tTI2K_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2K_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2K_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // CopyAR
                    // --------------------------------------------------------------------------
                    m_pgm_step = "CopyAR";
                    //fields_af = "";
                    //fields_af += fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS,";
                    //fields_af += "RESID";

                    //fields_bf = "";
                    //fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    //fields_bf += "RESID";

                    GetTableFields(tTI2AR_af, fBDODT_af, af_bdodt, af_qfycd, af_jrby, af_pid, af_unisq, af_simcs, ref fields_af, ref fields_bf, conn, tran);

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO " + tTI2AR_af + "(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM " + tTI2AR_bf + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_bf + "='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + bf_simcs + "";

                    //MessageBox.Show(sql);

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // Copy20
                    // --------------------------------------------------------------------------
                    m_pgm_step = "Copy20";
                    fields_af = "";
                    fields_af += "IOFG,";
                    fields_af += "K1,K2,K3,K4,K5,K6,";
                    fields_af += "SIMTEXT";

                    fields_bf = "";
                    fields_bf += "'" + mode_af + "',";
                    fields_bf += "'" + af_bdodt + "','" + af_qfycd + "','" + af_jrby + "','" + af_pid + "'," + af_unisq + ", " + af_simcs + ",";
                    fields_bf += "SIMTEXT";

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TI20(" + fields_af + ")";
                    sql += Environment.NewLine + "SELECT " + fields_bf + "";
                    sql += Environment.NewLine + "  FROM TI20";
                    sql += Environment.NewLine + " WHERE IOFG='" + mode_bf + "'";
                    sql += Environment.NewLine + "   AND K1='" + bf_bdodt + "'";
                    sql += Environment.NewLine + "   AND K2='" + bf_qfycd + "'";
                    sql += Environment.NewLine + "   AND K3='" + bf_jrby + "'";
                    sql += Environment.NewLine + "   AND K4='" + bf_pid + "'";
                    sql += Environment.NewLine + "   AND K5=" + bf_unisq + "";
                    sql += Environment.NewLine + "   AND K6=" + bf_simcs + "";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);


                    // --------------------------------------------------------------------------
                    // 자격이 변경되었으면 진료내역을 다시 계산한다.
                    // --------------------------------------------------------------------------
                    m_pgm_step = "재계산";
                    if (af_new_qfycd != "")
                    {
                        string pdiv = "";
                        string gonsgb = "";
                        string addz1 = "";
                        string drgfg = "";
                        string pacare_fg = "";
                        string ndrg_gbn = "";
                        string yofg = "";
                        string tjkh = "";
                        string er_serious = "";

                        sql = "";
                        sql += Environment.NewLine + "SELECT PDIV,GONSGB,ADDZ1,DRGFG,PACAREFG,NDRGGBN,YOFG,TJKH,ERSERIOUS";
                        sql += Environment.NewLine + "  FROM " + tTI2A_af + "";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY ='" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID  ='" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            pdiv = reader["PDIV"].ToString();
                            gonsgb = reader["GONSGB"].ToString();
                            addz1 = reader["ADDZ1"].ToString();
                            drgfg = reader["DRGFG"].ToString();
                            pacare_fg = reader["PACAREFG"].ToString();
                            ndrg_gbn = reader["NDRGGBN"].ToString();
                            yofg = reader["YOFG"].ToString();
                            tjkh = reader["TJKH"].ToString();
                            er_serious = reader["ERSERIOUS"].ToString();
                            return false;
                        });

                        string yh_gbn = af_jrby.StartsWith("7") ? "2" : "1";

                        string refg = "";
                        string change_field = "";
                        string pacare_chrlt = "";
                        string hbuf = "";
                        hbuf += af_pid + ";";
                        hbuf += mode_af + ";";
                        hbuf += af_qfycd + ";";
                        hbuf += af_dptcd + ";";
                        hbuf += pdiv + ";";
                        hbuf += bededt + ";";
                        hbuf += gonsgb + ";";
                        hbuf += ";";
                        hbuf += refg + ";";
                        hbuf += change_field + ";";
                        hbuf += addz1 + ";";
                        hbuf += stedt + ";";
                        hbuf += ";";
                        hbuf += ";";
                        hbuf += "0;";
                        hbuf += drgfg + ";";
                        hbuf += pacare_fg + ";";
                        hbuf += pacare_chrlt + ";";
                        hbuf += ndrg_gbn + ";";
                        hbuf += yofg + ";";
                        hbuf += tjkh + ";";
                        hbuf += ";";
                        hbuf += er_serious + ";";

                        sql = "";
                        sql += Environment.NewLine + "SELECT *";
                        sql += Environment.NewLine + "  FROM " + tTI2F_af + "";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                        m_pgm_step = "재계산 자료 추출";

                        List<CTIF> list = new List<CTIF>();
                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            CTIF f = new CTIF();
                            f.SetValues(mode_af, reader);
                            list.Add(f);

                            return true;
                        });

                        // 다시 계산
                        int turn_cnt = list.Count;
                        int turn_idx = 0;
                        m_pgm_step = "재계산";
                        foreach (CTIF f in list)
                        {
                            turn_idx++;

                            ShowMessage("처리중입니다.(1)(" + turn_idx + "/" + turn_cnt + ")");

                            string f_rec = f.GetFRec();

                            // 계산
                            m_pgm_step = "재계산 시도_" + turn_idx.ToString();
                            //string ret_value = MetroLib.ComHelper.MExe("$$RESUM2^mtrMADSimCommon.Common", yh_gbn, af_qfycd, af_jrby, mode_af, hbuf, f_rec, "", "", "", "");

                            // 수가계산할 VB DLL을 호출한다.
                            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("C:/Metro/DLL/");
                            if (di.Exists == false)
                            {
                                di.Create();
                            }
                            // 계산 결과 파일 초기화
                            if (System.IO.File.Exists("C:/Metro/DLL/ADD_RESUM.out"))
                            {
                                System.IO.File.WriteAllText("C:/Metro/DLL/ADD_RESUM.out", "", Encoding.Default);
                            }
                            // 계산할 자료 파일을 만든다.
                            string resum_file = "C:/Metro/DLL/ADD_RESUM.in";
                            System.IO.File.WriteAllText(resum_file, "", Encoding.Default);
                            System.IO.File.AppendAllText(resum_file, yh_gbn + System.Environment.NewLine, Encoding.Default);
                            System.IO.File.AppendAllText(resum_file, af_qfycd + System.Environment.NewLine, Encoding.Default);
                            System.IO.File.AppendAllText(resum_file, "" + System.Environment.NewLine, Encoding.Default);//jrby
                            System.IO.File.AppendAllText(resum_file, "" + System.Environment.NewLine, Encoding.Default);//bdiv
                            System.IO.File.AppendAllText(resum_file, hbuf + System.Environment.NewLine, Encoding.Default);
                            System.IO.File.AppendAllText(resum_file, f_rec + System.Environment.NewLine, Encoding.Default);
                            // 수가 계산 VB DLL 호출
                            string cmd_path = "C:/Metro/DLL/";
                            string cmd_str = "C:/Metro/DLL/ADD_RESUM.exe";
                            string args = "";
                            ExecCmd(cmd_str, cmd_path, args);

                            // 계산 결과를 읽는다.
                            string ret_value = "";
                            if (System.IO.File.Exists("C:/Metro/DLL/ADD_RESUM.out"))
                            {
                                string[] lines = System.IO.File.ReadAllLines("C:/Metro/DLL/ADD_RESUM.out", Encoding.Default);
                                ret_value = lines[0];
                            }

                            int idx = ret_value.IndexOf("^");
                            string cnt_value = ret_value.Substring(0, idx);
                            string ret = ret_value.Substring(idx + 1);
                            int cnt = 0;
                            int.TryParse(cnt_value, out cnt);
                            if (cnt < 0)
                            {
                                throw new Exception(ret);
                            }

                            // 계산 결과
                            m_pgm_step = "재계산 결과 추출_" + turn_idx.ToString();
                            f.ReplaceValues(ret);
                        }

                        // 저장
                        turn_cnt = list.Count;
                        turn_idx = 0;
                        foreach (CTIF f in list)
                        {
                            turn_idx++;

                            ShowMessage("처리중입니다.(2)(" + turn_idx + "/" + turn_cnt + ")");

                            m_pgm_step = "재계산 결과 저장_" + turn_idx.ToString() + "/";
                            m_pgm_step += "ELINENO=" + f.ELINENO + "/"
                                       + "PRICD=" + f.PRICD + "/"
                                       + "DANGA=" + f.DANGA + "/"
                                       + "CNTQTY=" + f.CNTQTY + "/"
                                       + "DQTY=" + f.DQTY + "/"
                                       + "DDAY=" + f.DDAY + "/"
                                       + "GUMAK=" + f.GUMAK + "/"
                                       + "IPOS1=" + f.IPOS1 + "/"
                                       + "POS2=" + f.POS2 + "/"
                                       + "SEQ1=" + f.SEQ1 + "/"
                                       + "SEQ2=" + f.SEQ2 + "/"
                                       + "BOSANGRT=" + f.BOSANGRT + "/"
                                       + "GUMAK2=" + f.GUMAK2 + "/"
                                       + "DRG7_ADD_RT=" + f.DRG7_ADD_RT + "/"
                                       + "DRG7_ADD_GUMAK2=" + f.DRG7_ADD_GUMAK2 + "/"
                                       + "DRG7_SEQ1=" + f.DRG7_SEQ1 + "/"
                                       + "DRG7_POS2=" + f.DRG7_POS2 + "/"
                                       + "DRG7_ELINENO=" + f.DRG7_ELINENO + "/"
                                       + "UPLMTAMT=" + f.UPLMTAMT + "/"
                                       + "UPLMTCHAAMT=" + f.UPLMTCHAAMT + "/"
                                       + "SPAMT=" + f.SPAMT + "/"
                                       + "SPPOS2=" + f.SPPOS2 + "/"
                                       + "";

                            f.SaveValues(mode_af, conn, tran);
                        }

                        // 줄번호를 지운다.
                        sql = "";
                        sql += Environment.NewLine + "SELECT *";
                        sql += Environment.NewLine + "  FROM " + tTI2F_af + "";
                        sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                        sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                        sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                        sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                        sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                        sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";
                        sql += Environment.NewLine + " ORDER BY " + fBDODT_af + ", QFYCD, JRBY, PID, UNISQ, SIMCS, SEQ1, POS2, ELINENO";

                        MetroLib.SqlHelper.GetDataRow(sql, conn, tran, delegate(DataRow row)
                        {
                            sql = "";
                            sql += Environment.NewLine + "UPDATE " + tTI2F_af + "";
                            sql += Environment.NewLine + "   SET ELINENO=Null";
                            sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                            sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                            sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                            sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                            sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                            sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";
                            sql += Environment.NewLine + "   AND SEQ1=" + row["SEQ1"].ToString() + "";
                            sql += Environment.NewLine + "   AND SEQ2=" + row["SEQ2"].ToString() + "";

                            MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                            return true;
                        });


                        ShowMessage("처리중입니다.");
                    }

                    // --------------------------------------------------------------------------
                    // SIMFG 를 다시 만든다.
                    // --------------------------------------------------------------------------
                    m_pgm_step = "SIMFG";
                    string qfy1 = af_qfycd.Substring(0, 1);
                    string jrby1 = af_jrby.Substring(0, 1);
                    string simfg = "";
                    if (qfy1 == "2" && jrby1.CompareTo("6") < 0) simfg = "1";    // 보험
                    if (qfy1 == "2" && jrby1.CompareTo("6") == 0) simfg = "2";   // 보험치과

                    if (qfy1 == "3" && jrby1.CompareTo("6") < 0) simfg = "3";    // 보호
                    if (qfy1 == "3" && jrby1.CompareTo("6") == 0) simfg = "4";   // 보호치과

                    if (qfy1 == "4" && jrby1.CompareTo("6") < 0) simfg = "1";    // 2005.07.28 NSK - 공상 -> 건강보험 SIMFG로 Setting
                    if (qfy1 == "4" && jrby1.CompareTo("6") == 0) simfg = "2";   // 2005.07.28 NSK - 공상 -> 건강보험 SIMFG로 Setting

                    if (qfy1 == "5" && jrby1.CompareTo("7") < 0) simfg = "5";     // 산재

                    if (qfy1 == "6" && jrby1.CompareTo("7") < 0) simfg = "6";     // 자보

                    if (qfy1 == "2" && jrby1.CompareTo("6") > 0) simfg = "7";     // 한방보험
                    if (qfy1 == "3" && jrby1.CompareTo("6") > 0) simfg = "8";     // 한방보호
                    if (qfy1 == "6" && jrby1.CompareTo("6") > 0) simfg = "13";    // 한방자보

                    if (qfy1 == "4" && jrby1.CompareTo("7") >= 0) simfg = "7";    // 2005.07.28 NSK - 한방공상-> 한방건강보험 SIMFG로 Setting
                    if (qfy1 == "5" && jrby1.CompareTo("7") >= 0) simfg = "9";    // 산재

                    if (af_qfycd == "29") simfg = "10";  // 2005.10.27 NSK 보훈일반
                    if (qfy1 == "8") simfg = "12";      // 2007.05.29 WOOIL - 계약처

                    // --------------------------------------------------------------------------
                    // SIMNO를 다시 만든다.
                    // --------------------------------------------------------------------------
                    m_pgm_step = "SIMNO";
                    string simno = "1";
                    sql = "";
                    sql += Environment.NewLine + "SELECT MAX(SIMNO) MAX_SIMNO";
                    sql += Environment.NewLine + "  FROM " + tTI2A_af + "";
                    sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                    sql += Environment.NewLine + "   AND SIMFG='" + simfg + "'";
                    sql += Environment.NewLine + "   AND SIMCS= 1";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                    {
                        int max_simno = 0;
                        int.TryParse(reader["MAX_SIMNO"].ToString(), out max_simno);
                        max_simno++;
                        simno = max_simno.ToString();
                        return false;
                    });

                    // --------------------------------------------------------------------------
                    // SIMFG,SIMNO 업데이트
                    // --------------------------------------------------------------------------
                    m_pgm_step = "SIMFG,SIMNO";
                    sql = "";
                    sql += Environment.NewLine + "UPDATE " + tTI2A_af + "";
                    sql += Environment.NewLine + "   SET SIMFG = '" + simfg + "'";
                    sql += Environment.NewLine + "     , SIMNO = " + simno + " ";
                    sql += Environment.NewLine + "     , DONFG = '' ";
                    sql += Environment.NewLine + "     , EPRTNO=NULL";
                    sql += Environment.NewLine + "     , DEMNO =NULL";
                    sql += Environment.NewLine + " WHERE " + fBDODT_af + "='" + af_bdodt + "'";
                    sql += Environment.NewLine + "   AND QFYCD='" + af_qfycd + "'";
                    sql += Environment.NewLine + "   AND JRBY='" + af_jrby + "'";
                    sql += Environment.NewLine + "   AND PID='" + af_pid + "'";
                    sql += Environment.NewLine + "   AND UNISQ=" + af_unisq + "";
                    sql += Environment.NewLine + "   AND SIMCS=" + af_simcs + "";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // --------------------------------------------------------------------------
                    // TT55에 로그남기기
                    // --------------------------------------------------------------------------
                    m_pgm_step = "로그";
                    tt55_endtm = MetroLib.Util.GetSysTime(conn, tran);
                    // 키중복방지용
                    while (true)
                    {
                        sql = "";
                        sql += Environment.NewLine + "SELECT COUNT(*) AS CNT";
                        sql += Environment.NewLine + "  FROM TT55";
                        sql += Environment.NewLine + " WHERE ENTDT ='" + tt55_entdt + "'";
                        sql += Environment.NewLine + "   AND PID   ='" + tt55_pid + "'";
                        sql += Environment.NewLine + "   AND BEDEDT='" + tt55_bededt + "'";
                        sql += Environment.NewLine + "   AND ENTTM ='" + tt55_enttm + "'";
                        int tt55_cnt = 0;
                        MetroLib.SqlHelper.GetDataReader(sql, conn, tran, delegate(OleDbDataReader reader)
                        {
                            int.TryParse(reader["CNT"].ToString(), out tt55_cnt);
                            return false;
                        });
                        if (tt55_cnt < 1) break; // 키가 중복되지 않으면 사용
                        // 값을 1증가시킨다.
                        int enttm = 0;
                        int.TryParse(tt55_enttm, out enttm);
                        enttm++;
                        tt55_enttm = enttm.ToString();
                    }
                    // 저장
                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TT55(ENTDT,PID,BEDEDT,ENTTM,DPTCD,ENDTM,EMPID,EMPNM,PRGID,WORKNM,REMARK)";
                    sql += Environment.NewLine + "VALUES";
                    sql += Environment.NewLine + "('" + tt55_entdt + "'";
                    sql += Environment.NewLine + ",'" + tt55_pid + "'";
                    sql += Environment.NewLine + ",'" + tt55_bededt + "'";
                    sql += Environment.NewLine + ",'" + tt55_enttm + "'";
                    sql += Environment.NewLine + ",'" + tt55_dptcd + "'";
                    sql += Environment.NewLine + ",'" + tt55_endtm + "'";
                    sql += Environment.NewLine + ",'" + tt55_empid + "'";
                    sql += Environment.NewLine + ",'" + tt55_empnm + "'";
                    sql += Environment.NewLine + ",'" + tt55_prgid + "'";
                    sql += Environment.NewLine + ",'" + tt55_worknm + "'";
                    sql += Environment.NewLine + ",'" + tt55_remark + "'";
                    sql += Environment.NewLine + ")";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);




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

        private void GetTableFields(string tTABLE_af, string fBDODT_af, string af_bdodt, string af_qfycd, string af_jrby, string af_pid, string af_unisq, string af_simcs, ref string field_af, ref string field_bf, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            var field_list = new List<string>();
            /*
            string sql = "SELECT TOP 1 * FROM " + tTABLE_af + "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                int field_count = reader.FieldCount;
                for (int i = 0; i < field_count; i++)
                {
                    field_list.Add(reader.GetName(i).ToUpper());
                }
                return MetroLib.SqlHelper.BREAK;
            });
            */

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT ORDINAL_POSITION, COLUMN_NAME";
            sql += Environment.NewLine + "  FROM INFORMATION_SCHEMA.COLUMNS";
            sql += Environment.NewLine + " WHERE TABLE_NAME = '" + tTABLE_af + "'";
            sql += Environment.NewLine + " ORDER BY ORDINAL_POSITION";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                field_list.Add(reader["COLUMN_NAME"].ToString().ToUpper());
                return MetroLib.SqlHelper.CONTINUE;
            });

            field_af = "";
            foreach (string col_name in field_list)
            {
                string col=col_name;
                if (col == "BDODT" || col == "EXDATE") col = fBDODT_af;

                if (field_af == "") field_af = col;
                else field_af += "," + col;
            }

            field_bf = "";
            foreach (string col_name in field_list)
            {
                string col = col_name;
                if (col == "BDODT" || col == "EXDATE") col = "'" + af_bdodt + "'";
                if (col == "QFYCD") col = "'" + af_qfycd + "'";
                if (col == "JRBY") col = "'" + af_jrby + "'";
                if (col == "PID") col = "'" + af_pid + "'";
                if (col == "UNISQ") col = "'" + af_unisq + "'";
                if (col == "SIMCS") col = "'" + af_simcs + "'";

                if (col == "TRANSFG") col = "'+'"; // TI1A와 TI2A에 복사자료라고 남긴다.

                if (field_bf == "") field_bf = col;
                else field_bf += "," + col;
            }
        }

        //private string GetFieldNameList(string p_table, OleDbConnection p_conn, OleDbTransaction p_tran)
        //{
        //    string ret = "";
        //    string sql = "";
        //    sql = "";
        //    sql += Environment.NewLine + "SELECT ORDINAL_POSITION, COLUMN_NAME";
        //    sql += Environment.NewLine + "  FROM INFORMATION_SCHEMA.COLUMNS";
        //    sql += Environment.NewLine + " WHERE TABLE_NAME = '" + p_table + "'";
        //    sql += Environment.NewLine + " ORDER BY ORDINAL_POSITION";
        //    MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
        //    {
        //        if (ret == "")
        //        {
        //            ret = reader["COLUMN_NAME"].ToString();
        //        }
        //        else
        //        {
        //            ret += "," + reader["COLUMN_NAME"].ToString();
        //        }
        //        return true;
        //    });
        //    return ret;
        //}

        private void ShowProgressForm(String caption, String description)
        {
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            //DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormCaption(caption);
            //DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormDescription(description);
            //
            lblWait.Top = 5;
            lblWait.Left = 5;
            lblWait.Width = 209;
            lblWait.Height = 260;
            lblWait.Visible = true;
            Application.DoEvents();
        }

        private void CloseProgressForm(String caption, String description)
        {
            //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            //
            lblWait.Visible = false;
            Application.DoEvents();
        }

        private void ShowMessage(string msg)
        {
            lblWait.Text = msg;
            Application.DoEvents();
        }

        private void cboQfycd_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChangQF.Text = cboQfycd.SelectedItem.ToString().Substring(0, 2);
        }

        private string FieldToValue(string field_info, string field_name, string field_value)
        {
            string ret = "";
            string[] field_arr = field_info.Split(',');
            for (int i = 0; i < field_arr.Length; i++)
            {
                if (field_arr[i] == field_name)
                {
                    ret += ret == "" ? field_value : "," + field_value;
                }
                else
                {
                    ret += ret == "" ? field_arr[i] : "," + field_arr[i];
                }
            }
            return ret;
        }

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

    }
}

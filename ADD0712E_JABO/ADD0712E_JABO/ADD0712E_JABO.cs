using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0712E_JABO
{
    public partial class ADD0712E_JABO : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0712E_JABO()
        {
            InitializeComponent();
            
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            this.CreatePopupMenu();
        }

        public ADD0712E_JABO(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();
        }

        private string GetHospmulti()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["MULTIFG"].ToString();
                        return false;
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void CreatePopupMenu()
        {
            //
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("보완청구내성생성", new EventHandler(mnuBOMake_Click));
            cm.MenuItems.Add("이의신청", new EventHandler(mnuObjMake_Click));
            grdMain.ContextMenu = cm;
        }

        private void ADD0712E_JABO_Load(object sender, EventArgs e)
        {
            cboQueryOption.SelectedIndex = 0;
            tabControl1.SelectedIndex = 0;
            m_PrevTabIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("");
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

        private void Query(string queryOption)
        {
            string multihosyn = "";
            string hsptid = "";

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            grdPtnt.DataSource = null;
            grdBul.DataSource = null;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 한 서버에 여러 병원이 운영되는지 여부
                string sql = "";
                sql += Environment.NewLine + "SELECT FLD2QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='0'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    multihosyn = reader["FLD2QTY"].ToString();
                    return true;
                });
                if (multihosyn == "1")
                {
                    // 로그인 한 사용자가 소속된 병원ID
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL" + m_HospMulti + "' AND MST3CD='2'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        hsptid = reader["FLD1QTY"].ToString();
                        return true;
                    });
                }

                SetFrdtTodt(queryOption, conn);

                sql = "";
                sql += Environment.NewLine + "SELECT N0201.VERSION";
                sql += Environment.NewLine + "     , N0201.JBUNICD";
                sql += Environment.NewLine + "     , N0201.DEMSEQ";
                sql += Environment.NewLine + "     , N0201.FMNO";
                sql += Environment.NewLine + "     , N0201.REDAY";
                sql += Environment.NewLine + "     , N0201.HOSID";
                sql += Environment.NewLine + "     , N0201.CNECNO";
                sql += Environment.NewLine + "     , N0201.GRPNO";
                sql += Environment.NewLine + "     , N0201.DCOUNT";
                sql += Environment.NewLine + "     , N0201.DEMNO";
                sql += Environment.NewLine + "     , N0201.FTDAYS";
                sql += Environment.NewLine + "     , N0201.JRFG";
                sql += Environment.NewLine + "     , N0201.REDPT1";
                sql += Environment.NewLine + "     , N0201.REDPT2";
                sql += Environment.NewLine + "     , N0201.REDPNM";
                sql += Environment.NewLine + "     , N0201.RETELE";
                sql += Environment.NewLine + "     , N0201.MEMO";
                sql += Environment.NewLine + "     , H010.IOFG";
                sql += Environment.NewLine + "     , H010.JRKWAFG";
                sql += Environment.NewLine + "     , N0204.CNT1";
                sql += Environment.NewLine + "     , N0204.CNT2";
                sql += Environment.NewLine + "     , H010.WEEKNUM";
                sql += Environment.NewLine + "     , (SELECT X.CDNM FROM TI88 X WHERE X.MST1CD='A' AND X.MST2CD='JBUNICD' AND X.MST3CD=N0201.JBUNICD) AS JBUNINM ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 INNER JOIN TIE_N0204 N0204 ON  N0201.DEMSEQ=N0204.DEMSEQ";
                sql += Environment.NewLine + "                                                  AND N0201.CNECNO=N0204.CNECNO";
                sql += Environment.NewLine + "                                                  AND N0201.GRPNO=N0204.GRPNO";
                sql += Environment.NewLine + "                                                  AND N0201.DCOUNT=N0204.DCOUNT";
                sql += Environment.NewLine + "                                                  AND N0201.DEMNO=N0204.DEMNO";
                sql += Environment.NewLine + "                       LEFT JOIN TIE_H010 H010 ON  N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0201.REDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0201.REDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0201.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0201.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if(txtInsmm.Text.ToString() !=""){
                    sql += Environment.NewLine + "   AND N0201.FTDAYS LIKE '" + txtInsmm.Text.ToString() + "%' ";
                }
                if (multihosyn == "1")
                {
                    sql += Environment.NewLine + "   AND N0201.HOSID = '" + hsptid + "' ";
                }
                sql += Environment.NewLine + " ORDER BY N0201.DEMSEQ DESC ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.IOFG = reader["IOFG"].ToString().TrimEnd();
                    data.JRKWAFG = reader["JRKWAFG"].ToString().TrimEnd();
                    data.FTDAYS = reader["FTDAYS"].ToString().TrimEnd();
                    data.CNT1 = Convert.ToInt64(reader["CNT1"].ToString().TrimEnd());
                    data.CNT2 = Convert.ToInt64(reader["CNT2"].ToString().TrimEnd());
                    data.REDAY = reader["REDAY"].ToString().TrimEnd();
                    data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd();
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd();
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd();
                    data.GRPNO = reader["GRPNO"].ToString().TrimEnd();
                    data.JBUNICD = reader["JBUNICD"].ToString().TrimEnd();
                    data.JBUNINM = reader["JBUNINM"].ToString().TrimEnd();
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();
                    data.WEEKNUM = reader["WEEKNUM"].ToString().TrimEnd();
                    data.VERSION = reader["VERSION"].ToString().TrimEnd();
                    data.REDPT1 = reader["REDPT1"].ToString().TrimEnd();
                    data.REDPT2 = reader["REDPT2"].ToString().TrimEnd();
                    data.REDPNM = reader["REDPNM"].ToString().TrimEnd();
                    data.RETELE = reader["RETELE"].ToString().TrimEnd();
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });

            }

            RefreshGridMain();
        }

        private void SetFrdtTodt(string queryOption, OleDbConnection conn)
        {
            if (queryOption == "") return;
            if (queryOption == "5")
            {
                // 제한없음
                txtFrdt.Text = "";
                txtTodt.Text = "";
                return;
            }


            DateTime dtFrdt = DateTime.Now;
            DateTime dtTodt = DateTime.Now;

            string sysdt = MetroLib.Util.GetSysDate(conn);
            DateTime.TryParse(DateTime.ParseExact(sysdt, "yyyyMMdd", null).ToString("yyyy-MM-dd"), out dtTodt);

            if (queryOption == "0")
            {
                // 최근 1년
                dtFrdt = dtTodt.AddYears(-1);
            }
            else if (queryOption == "1")
            {
                // 최근 6개월
                dtFrdt = dtTodt.AddMonths(-6);
            }
            else if (queryOption == "2")
            {
                // 최근 3개월
                dtFrdt = dtTodt.AddMonths(-3);
            }
            else if (queryOption == "3")
            {
                // 최근 1개월
                dtFrdt = dtTodt.AddMonths(-1);
            }
            else if (queryOption == "4")
            {
                // 최근 1주
                dtFrdt = dtTodt.AddDays(-7);
            }
            txtFrdt.Text = dtFrdt.ToString("yyyyMMdd");
            txtTodt.Text = dtTodt.ToString("yyyyMMdd");
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

        private void RefreshGridPtnt()
        {
            if (grdPtnt.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdPtnt.BeginInvoke(new Action(() => grdPtntView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdPtntView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridBul()
        {
            if (grdBul.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdBul.BeginInvoke(new Action(() => grdBulView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdBulView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridSak()
        {
            if (grdSak.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSak.BeginInvoke(new Action(() => grdSakView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSakView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridOutYak()
        {
            if (grdOutYak.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdOutYak.BeginInvoke(new Action(() => grdOutYakView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdOutYakView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridSutak()
        {
            if (grdSutak.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSutak.BeginInvoke(new Action(() => grdSutakView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSutakView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridTotal()
        {
            if (grdTotal.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdTotal.BeginInvoke(new Action(() => grdTotalView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdTotalView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridComb()
        {
            if (grdComb.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdComb.BeginInvoke(new Action(() => grdCombView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdCombView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;
                string memo = grdMainView.GetRowCellValue(e.FocusedRowHandle, "MEMO").ToString();
                txtMemo.Text = memo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                if (m_PrevTabIndex == 0)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        this.ShowProgressForm("", "조회 중입니다.");
                        this.QuerySub();
                        this.CloseProgressForm("", "조회 중입니다.");
                        Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        this.CloseProgressForm("", "조회 중입니다.");
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message + Environment.NewLine + m_ErrPos);
                    }

                }
            }
            m_PrevTabIndex = tabControl1.SelectedIndex;
        }

        private void QuerySub()
        {
            if (grdMainView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;

            string iofgnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "IOFGNM").ToString();
            string reday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "REDAY").ToString();
            string grpno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "GRPNO").ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECNO").ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DCOUNT").ToString();
            string demseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMSEQ").ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
            string weeknumnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "WEEKNUMNM").ToString();
            string version = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "VERSION").ToString();
            string ftdays = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "FTDAYS").ToString();
            string redpt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "REDPT").ToString();
            string memo = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "MEMO").ToString();
            string insmm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "INSMM").ToString();
            string iofg = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "IOFG").ToString();

            txtIofgnm.Text = iofgnm;
            txtReday.Text = reday;
            txtGrpno.Text = grpno;
            txtHeadCnecno.Text = cnecno;
            txtDcount.Text = dcount;
            txtDemseq.Text = demseq;
            txtHeadDemno.Text = demno;
            txtWeeknumnm.Text = weeknumnm;
            txtVersion.Text = version;
            txtFtdays.Text = ftdays;
            txtRedpt.Text = redpt;
            txtMemo.Text = memo;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_ErrPos = "QueryPtnt";
                QueryPtnt(demseq, cnecno, demno, insmm, iofg, conn);
                m_ErrPos = "QueryBulSak";
                QueryBulSak(demseq, cnecno, demno, insmm, iofg, conn);
                m_ErrPos = "QueryOutYak";
                QueryOutYak(demseq, cnecno, demno, insmm, conn);
                m_ErrPos = "QuerySutak";
                QuerySutak(demseq, cnecno, demno, insmm, conn);
                m_ErrPos = "QueryTotal";
                QueryTotal(demseq, cnecno, demno, insmm, conn);
                m_ErrPos = "QueryComb";
                QueryComb(demseq, cnecno, demno, insmm, iofg, conn);
            }
        }

        private void QueryPtnt(string p_demseq, string p_cnecno, string p_demno, string p_insmm, string p_iofg, OleDbConnection p_conn)
        {
            List<CDataPtnt> list = new List<CDataPtnt>();
            grdPtnt.DataSource = null;
            grdPtnt.DataSource = list;

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                string sql2="";
                sql2 += Environment.NewLine + "SELECT N0202.EPRTNO";
                sql2 += Environment.NewLine + "     , N0202.UNICD";
                sql2 += Environment.NewLine + "     , N0202.APPRNO";
                sql2 += Environment.NewLine + "     , N0202.PNM";
                sql2 += Environment.NewLine + "     , N0202.UNAMT";
                sql2 += Environment.NewLine + "     , N0202.JBPTAMT";
                sql2 += Environment.NewLine + "     , N0202.TTTAMT";
                sql2 += Environment.NewLine + "     , N0202.CHOGUM";
                sql2 += Environment.NewLine + "     , N0202.JAEGUM";
                sql2 += Environment.NewLine + "     , N0202.RQUPLMTCHATTAMT";
                sql2 += Environment.NewLine + "     , N0202.EXAMC";
                sql2 += Environment.NewLine + "     , N0202.ORDDAYS";
                sql2 += Environment.NewLine + "     , N0202.ORDCNT";
                sql2 += Environment.NewLine + "     , N0202.MEMO";
                sql2 += Environment.NewLine + "     , N0202.DEMNO";
                sql2 += Environment.NewLine + "     , H010.IOFG";
                sql2 += Environment.NewLine + "     , N0202.GBFG";
                sql2 += Environment.NewLine + "     , N0202.ASTAMT";
                sql2 += Environment.NewLine + "     , N0202.ACTGUM";
                sql2 += Environment.NewLine + "     , N0202.ATTTAMT";
                sql2 += Environment.NewLine + "     , N0202.AJBPTAMT";
                sql2 += Environment.NewLine + "     , N0202.SKUPLMTCHATTAMT";
                sql2 += Environment.NewLine + "     , N0202.CHOCNT";
                sql2 += Environment.NewLine + "     , N0202.CHONCNT";
                sql2 += Environment.NewLine + "     , N0202.JAECNT";
                sql2 += Environment.NewLine + "     , N0202.JAENCNT";
                sql2 += Environment.NewLine + "     , N0202.STEDT";
                sql2 += Environment.NewLine + "     , N0202.MDACD";
                sql2 += Environment.NewLine + "  FROM TIE_N0202 N0202 LEFT JOIN TIE_H010 H010 ON H010.DEMNO=N0202.DEMNO";
                sql2 += Environment.NewLine + " WHERE N0202.DEMSEQ ='" + row["DEMSEQ"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N0202.CNECNO ='" + row["CNECNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N0202.GRPNO ='" + row["GRPNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N0202.DCOUNT ='" + row["DCOUNT"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N0202.DEMNO ='" + row["DEMNO"].ToString() + "' ";
                sql2 += Environment.NewLine + " ORDER BY N0202.EPRTNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    CDataPtnt data = new CDataPtnt();
                    data.Clear();

                    data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                    data.PNM = row2["PNM"].ToString().TrimEnd();
                    data.STEDT = row2["STEDT"].ToString().TrimEnd();
                    data.MDACD = row2["MDACD"].ToString().TrimEnd();
                    data.UNAMT = Convert.ToInt64(row2["UNAMT"].ToString().TrimEnd());
                    data.TTTAMT = Convert.ToInt64(row2["TTTAMT"].ToString().TrimEnd());
                    data.JBPTAMT = Convert.ToInt64(row2["JBPTAMT"].ToString().TrimEnd());
                    data.RQUPLMTCHATTAMT = Convert.ToInt64(row2["RQUPLMTCHATTAMT"].ToString().TrimEnd());
                    data.ACTGUM = Convert.ToInt64(row2["ACTGUM"].ToString().TrimEnd());
                    data.ATTTAMT = Convert.ToInt64(row2["ATTTAMT"].ToString().TrimEnd());
                    data.ASTAMT = Convert.ToInt64(row2["ASTAMT"].ToString().TrimEnd());
                    data.AJBPTAMT = Convert.ToInt64(row2["AJBPTAMT"].ToString().TrimEnd());
                    data.SKUPLMTCHATTAMT = Convert.ToInt64(row2["SKUPLMTCHATTAMT"].ToString().TrimEnd());
                    data.CHOGUM = Convert.ToInt64(row2["CHOGUM"].ToString().TrimEnd());
                    data.CHOCNT = Convert.ToInt64(row2["CHOCNT"].ToString().TrimEnd());
                    data.CHONCNT = Convert.ToInt64(row2["CHONCNT"].ToString().TrimEnd());
                    data.JAEGUM = Convert.ToInt64(row2["JAEGUM"].ToString().TrimEnd());
                    data.JAECNT = Convert.ToInt64(row2["JAECNT"].ToString().TrimEnd());
                    data.JAENCNT = Convert.ToInt64(row2["JAENCNT"].ToString().TrimEnd());
                    data.EXAMC = Convert.ToInt64(row2["EXAMC"].ToString().TrimEnd());
                    data.ORDDAYS = Convert.ToInt64(row2["ORDDAYS"].ToString().TrimEnd());
                    data.ORDCNT = Convert.ToInt64(row2["ORDCNT"].ToString().TrimEnd());
                    data.APPRNO = row2["APPRNO"].ToString().TrimEnd();
                    data.DEMNO = row2["DEMNO"].ToString().TrimEnd();
                    data.MEMO = row2["MEMO"].ToString().TrimEnd();
                    data.IOFG = p_iofg;

                    list.Add(data);

                    return true;
                });

                return true;
            });

            RefreshGridPtnt();
        }

        private void QueryBulSak(string p_demseq, string p_cnecno, string p_demno, string p_insmm, string p_iofg, OleDbConnection p_conn)
        {
            string tTI1A = "TI1A";
            string tTI1F = "TI1F";
            string tTI13 = "TI13";
            string fEXDATE = "EXDATE";
            if (p_iofg != "1")
            {
                tTI1A = "TI2A";
                tTI1F = "TI2F";
                tTI13 = "TI23";
                fEXDATE = "BDODT";
            }

            List<CDataBul> listBul = new List<CDataBul>();
            grdBul.DataSource = null;
            grdBul.DataSource = listBul;

            List<CDataSak> listSak = new List<CDataSak>();
            grdSak.DataSource = null;
            grdSak.DataSource = listSak;

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT N3.EPRTNO";
                sql2 += Environment.NewLine + "     , '9' AS OUTCNT";
                sql2 += Environment.NewLine + "     , N3.LNO";
                sql2 += Environment.NewLine + "     , N3.JJRMK AS JJRMK1";
                sql2 += Environment.NewLine + "     , N3.JJRMK2";
                sql2 += Environment.NewLine + "     , N3.JJDETAIL";
                sql2 += Environment.NewLine + "     , N3.JJTEXT";
                sql2 += Environment.NewLine + "     , N3.BGIHO";
                sql2 += Environment.NewLine + "     , N3.DANGA";
                sql2 += Environment.NewLine + "     , N3.CNTQTY"; // 인정 1회투약량
                sql2 += Environment.NewLine + "     , N3.DQTY"; // 인정 일투
                sql2 += Environment.NewLine + "     , N3.DDAY";// 인정 총투
                sql2 += Environment.NewLine + "     , N3.JJGUMAK";
                sql2 += Environment.NewLine + "     , F.BGIHO AS F_BGIHO";
                sql2 += Environment.NewLine + "     , F.PRKNM";
                sql2 += Environment.NewLine + "     , F.DANGA AS F_DANGA";
                sql2 += Environment.NewLine + "     , F.CNTQTY AS F_CNTQTY";
                sql2 += Environment.NewLine + "     , F.DQTY AS F_DQTY";
                sql2 += Environment.NewLine + "     , F.DDAY AS F_DDAY";
                sql2 += Environment.NewLine + "     , F.GUMAK AS F_GUMAK";
                sql2 += Environment.NewLine + "     , F.MAFG";
                sql2 += Environment.NewLine + "     , A.GSRT";
                sql2 += Environment.NewLine + "     , A.PID";
                sql2 += Environment.NewLine + "     , A.PNM AS A_PNM";
                sql2 += Environment.NewLine + "     , N2.PNM AS N2_PNM";
                sql2 += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
                sql2 += Environment.NewLine + "     , ISNULL((SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A.PDRID),'') AS DRNM";
                sql2 += Environment.NewLine + "     , N2.MDACD";
                sql2 += Environment.NewLine + "     , N3.DEMNO";
                sql2 += Environment.NewLine + "     , N3.CNECNO";
                sql2 += Environment.NewLine + "     , N3.GRPNO";
                sql2 += Environment.NewLine + "     , N3.DCOUNT";
                sql2 += Environment.NewLine + "     , N1.DEMSEQ";
                sql2 += Environment.NewLine + "     , N1.REDAY";
                sql2 += Environment.NewLine + "     , N3.UPLMTAMT";
                sql2 += Environment.NewLine + "     , N3.JJUPLMTCHAAMT";
                sql2 += Environment.NewLine + "  FROM TIE_N0203 N3 INNER JOIN TIE_N0202 N2 ON N2.DEMSEQ=N3.DEMSEQ AND N2.CNECNO=N3.CNECNO AND N2.GRPNO=N3.GRPNO AND N2.DCOUNT=N3.DCOUNT AND N2.DEMNO=N3.DEMNO AND N2.EPRTNO=N3.EPRTNO";
                sql2 += Environment.NewLine + "                    INNER JOIN TIE_N0201 N1 ON N1.DEMSEQ=N3.DEMSEQ AND N1.CNECNO=N3.CNECNO AND N1.GRPNO=N3.GRPNO AND N1.DCOUNT=N3.DCOUNT AND N1.DEMNO=N3.DEMNO";
                sql2 += Environment.NewLine + "                    LEFT  JOIN " + tTI1A + " A ON A.DEMNO=RTRIM(N3.DEMNO) AND A.EPRTNO=RTRIM(N3.EPRTNO)";
                sql2 += Environment.NewLine + "                    LEFT  JOIN " + tTI1F + " F ON F." + fEXDATE + "=A." + fEXDATE + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS AND F.ELINENO=N3.LNO";
                sql2 += Environment.NewLine + " WHERE N3.DEMSEQ ='" + row["DEMSEQ"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.CNECNO ='" + row["CNECNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.GRPNO ='" + row["GRPNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.DCOUNT ='" + row["DCOUNT"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.DEMNO ='" + row["DEMNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.LNO<>0 ";
                sql2 += Environment.NewLine + " UNION ALL ";
                sql2 += Environment.NewLine + "SELECT N3.EPRTNO";
                sql2 += Environment.NewLine + "     , '9' AS OUTCNT";
                sql2 += Environment.NewLine + "     , N3.LNO";
                sql2 += Environment.NewLine + "     , N3.JJRMK AS JJRMK1";
                sql2 += Environment.NewLine + "     , N3.JJRMK2";
                sql2 += Environment.NewLine + "     , N3.JJDETAIL";
                sql2 += Environment.NewLine + "     , N3.JJTEXT";
                sql2 += Environment.NewLine + "     , N3.BGIHO";
                sql2 += Environment.NewLine + "     , N3.DANGA";
                sql2 += Environment.NewLine + "     , N3.CNTQTY"; // 인정 1회투약량
                sql2 += Environment.NewLine + "     , N3.DQTY"; // 인정 일투
                sql2 += Environment.NewLine + "     , N3.DDAY";// 인정 총투
                sql2 += Environment.NewLine + "     , N3.JJGUMAK";
                sql2 += Environment.NewLine + "     , NULL AS F_BGIHO";
                sql2 += Environment.NewLine + "     , NULL AS PRKNM";
                sql2 += Environment.NewLine + "     , NULL AS F_CDANGA"; // 2009.08.28 WOOIL - 청구단가
                sql2 += Environment.NewLine + "     , NULL AS F_CNTQTY";
                sql2 += Environment.NewLine + "     , NULL AS F_DQTY";
                sql2 += Environment.NewLine + "     , NULL AS F_DDAY";
                sql2 += Environment.NewLine + "     , NULL AS F_GUMAK";
                sql2 += Environment.NewLine + "     , '1' AS MAFG";
                sql2 += Environment.NewLine + "     , A.GSRT";
                sql2 += Environment.NewLine + "     , A.PID";
                sql2 += Environment.NewLine + "     , A.PNM AS A_PNM";
                sql2 += Environment.NewLine + "     , N2.PNM AS N2_PNM";
                sql2 += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
                sql2 += Environment.NewLine + "     , ISNULL((SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A.PDRID),'') AS DRNM";
                sql2 += Environment.NewLine + "     , N2.MDACD";
                sql2 += Environment.NewLine + "     , N3.DEMNO";
                sql2 += Environment.NewLine + "     , N3.CNECNO";
                sql2 += Environment.NewLine + "     , N3.GRPNO";
                sql2 += Environment.NewLine + "     , N3.DCOUNT";
                sql2 += Environment.NewLine + "     , N1.DEMSEQ";
                sql2 += Environment.NewLine + "     , N1.REDAY";
                sql2 += Environment.NewLine + "     , N3.UPLMTAMT";
                sql2 += Environment.NewLine + "     , N3.JJUPLMTCHAAMT";
                sql2 += Environment.NewLine + "  FROM TIE_N0203 N3 INNER JOIN TIE_N0202 N2 ON N2.DEMSEQ=N3.DEMSEQ AND N2.CNECNO=N3.CNECNO AND N2.GRPNO=N3.GRPNO AND N2.DCOUNT=N3.DCOUNT AND N2.DEMNO=N3.DEMNO AND N2.EPRTNO=N3.EPRTNO";
                sql2 += Environment.NewLine + "                    INNER JOIN TIE_N0201 N1 ON N1.DEMSEQ=N3.DEMSEQ AND N1.CNECNO=N3.CNECNO AND N1.GRPNO=N3.GRPNO AND N1.DCOUNT=N3.DCOUNT AND N1.DEMNO=N3.DEMNO";
                sql2 += Environment.NewLine + "                    LEFT  JOIN " + tTI1A + " A ON A.DEMNO=RTRIM(N3.DEMNO) AND A.EPRTNO=RTRIM(N3.EPRTNO)";
                sql2 += Environment.NewLine + " WHERE N3.DEMSEQ ='" + row["DEMSEQ"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.CNECNO ='" + row["CNECNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.GRPNO ='" + row["GRPNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.DCOUNT ='" + row["DCOUNT"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.DEMNO ='" + row["DEMNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N3.LNO=0 ";
                sql2 += Environment.NewLine + " UNION ALL ";
                sql2 += Environment.NewLine + "SELECT N6.EPRTNO";
                sql2 += Environment.NewLine + "     , N6.OUTCNT";
                sql2 += Environment.NewLine + "     , N6.LNO";
                sql2 += Environment.NewLine + "     , N6.REMARK AS JJRMK1";
                sql2 += Environment.NewLine + "     , NULL AS JJRMK2";
                sql2 += Environment.NewLine + "     , NULL AS JJDETAIL";
                sql2 += Environment.NewLine + "     , N6.MEMO AS JJTEXT";
                sql2 += Environment.NewLine + "     , NULL AS BGIHO";
                sql2 += Environment.NewLine + "     , NULL AS DANGA";
                sql2 += Environment.NewLine + "     , NULL AS CNTQTY";
                sql2 += Environment.NewLine + "     , NULL AS DQTY";
                sql2 += Environment.NewLine + "     , NULL AS DDAY";
                sql2 += Environment.NewLine + "     , NULL AS JJGUMAK";
                sql2 += Environment.NewLine + "     , I13.BGIHO AS F_BGIHO";
                sql2 += Environment.NewLine + "     , I13.PRKNM";
                sql2 += Environment.NewLine + "     , I13.DANGA AS F_DANGA";
                sql2 += Environment.NewLine + "     , CONVERT(NUMERIC(9,2),I13.DQTY/CASE WHEN ISNULL(I13.ORDCNT,0)=0 THEN 1 ELSE I13.ORDCNT END) AS F_CNTQTY";
                sql2 += Environment.NewLine + "     , CASE WHEN ISNULL(I13.ORDCNT,0)=0 THEN 1 ELSE I13.ORDCNT END AS F_DQTY";
                sql2 += Environment.NewLine + "     , I13.DDAY AS F_DDAY";
                sql2 += Environment.NewLine + "     , I13.GUMAK AS F_GUMAK";
                sql2 += Environment.NewLine + "     , '1' AS MAFG";
                sql2 += Environment.NewLine + "     , A.GSRT";
                sql2 += Environment.NewLine + "     , A.PID";
                sql2 += Environment.NewLine + "     , A.PNM AS A_PNM";
                sql2 += Environment.NewLine + "     , N2.PNM AS N2_PNM";
                sql2 += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
                sql2 += Environment.NewLine + "     , ISNULL((SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A.PDRID),'') AS DRNM";
                sql2 += Environment.NewLine + "     , N2.MDACD";
                sql2 += Environment.NewLine + "     , N6.DEMNO";
                sql2 += Environment.NewLine + "     , N6.CNECNO";
                sql2 += Environment.NewLine + "     , N6.GRPNO";
                sql2 += Environment.NewLine + "     , N6.DCOUNT";
                sql2 += Environment.NewLine + "     , N1.DEMSEQ";
                sql2 += Environment.NewLine + "     , N1.REDAY ";
                sql2 += Environment.NewLine + "     , NULL AS UPLMTAMT ";
                sql2 += Environment.NewLine + "     , NULL AS JJUPLMTCHAAMT ";
                sql2 += Environment.NewLine + "  FROM TIE_N0206 N6 INNER JOIN TIE_N0202 N2 ON N2.DEMSEQ=N6.DEMSEQ AND N2.CNECNO=N6.CNECNO AND N2.GRPNO=N6.GRPNO AND N2.DCOUNT=N6.DCOUNT AND N2.DEMNO=N6.DEMNO AND N2.EPRTNO=N6.EPRTNO";
                sql2 += Environment.NewLine + "                    INNER JOIN TIE_N0201 N1 ON N1.DEMSEQ=N6.DEMSEQ AND N1.CNECNO=N6.CNECNO AND N1.GRPNO=N6.GRPNO AND N1.DCOUNT=N6.DCOUNT AND N1.DEMNO=N6.DEMNO";
                sql2 += Environment.NewLine + "                    LEFT  JOIN " + tTI1A + " A ON A.DEMNO=RTRIM(N6.DEMNO) AND A.EPRTNO=RTRIM(N6.EPRTNO)";
                sql2 += Environment.NewLine + "                    LEFT  JOIN " + tTI13 + " I13 ON I13." + fEXDATE + "=A." + fEXDATE + " AND I13.QFYCD=A.QFYCD AND I13.JRBY=A.JRBY AND I13.PID=A.PID AND I13.UNISQ=A.UNISQ AND I13.SIMCS=A.SIMCS AND I13.OUTSEQ=N6.OUTCNT AND I13.SEQ=N6.LNO";
                sql2 += Environment.NewLine + " WHERE N6.DEMSEQ ='" + row["DEMSEQ"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N6.CNECNO ='" + row["CNECNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N6.GRPNO ='" + row["GRPNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N6.DCOUNT ='" + row["DCOUNT"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + "   AND N6.DEMNO ='" + row["DEMNO"].ToString().TrimEnd() + "' ";
                sql2 += Environment.NewLine + " ORDER BY 1,2,3"; // EPRTNO,OUTCNT,LNO

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    bool isBul = false;
                    long lno = Convert.ToInt64(row2["LNO"].ToString().TrimEnd());
                    if (lno == 0)
                    {
                        // 줄번호가 0인 경우만
                        string jjrmk1 = row2["JJRMK1"].ToString().TrimEnd();
                        if (jjrmk1 == "J1" || jjrmk1 == "J2" || jjrmk1 == "J3")
                        {
                            // 조정사유가 J1, J2, J3이면 불능
                            isBul = true;
                        }
                        else
                        {
                            long jjrmk1_no = 0;
                            if (long.TryParse(jjrmk1, out jjrmk1_no) == true)
                            {
                                // 조정사유가 숫자이면 불능
                                isBul = true;
                            }
                        }
                    }

                    if (isBul == true)
                    {
                        m_ErrPos = "QueryBulSak-불능";
                        // 불능.보류
                        CDataBul data = new CDataBul();
                        data.Clear();

                        data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                        data.PID = row2["PID"].ToString().TrimEnd();
                        data.A_PNM = row2["A_PNM"].ToString().TrimEnd();
                        data.N2_PNM = row2["N2_PNM"].ToString().TrimEnd();
                        data.DPTCD = row2["DPTCD"].ToString().TrimEnd();
                        data.DRNM = row2["DRNM"].ToString().TrimEnd();
                        data.MDACD = row2["MDACD"].ToString().TrimEnd();
                        data.JJRMK1 = row2["JJRMK1"].ToString().TrimEnd();
                        data.JJRMK2 = row2["JJRMK2"].ToString().TrimEnd();
                        data.JJDETAIL = row2["JJDETAIL"].ToString().TrimEnd();
                        data.JJTEXT = row2["JJTEXT"].ToString().TrimEnd();
                        data.JJGUMAK = Convert.ToInt64(row2["JJGUMAK"].ToString().TrimEnd());
                        data.DEMNO = row2["DEMNO"].ToString().TrimEnd();
                        data.REDAY = row2["REDAY"].ToString().TrimEnd();
                        data.JJRMKNM = GetBulJjrmkNm(data.JJRMK1, data.JJRMK2, data.JJDETAIL, p_conn);

                        listBul.Add(data);
                    }
                    else
                    {
                        m_ErrPos = "QueryBulSak-보류";
                        // 삭감
                        CDataSak data = new CDataSak();
                        data.Clear();

                        data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                        data.PID = row2["PID"].ToString().TrimEnd();
                        data.A_PNM = row2["A_PNM"].ToString().TrimEnd();
                        data.N2_PNM = row2["N2_PNM"].ToString().TrimEnd();
                        data.DPTCD = row2["DPTCD"].ToString().TrimEnd();
                        data.DRNM = row2["DRNM"].ToString().TrimEnd();
                        data.MDACD = row2["MDACD"].ToString().TrimEnd();
                        data.LNO = row2["LNO"].ToString().TrimEnd();
                        data.JJRMK1 = row2["JJRMK1"].ToString().TrimEnd();
                        data.JJRMK2 = row2["JJRMK2"].ToString().TrimEnd();
                        data.JJDETAIL = row2["JJDETAIL"].ToString().TrimEnd();
                        data.JJTEXT = row2["JJTEXT"].ToString().TrimEnd();
                        //
                        data.F_BGIHO = row2["F_BGIHO"].ToString().TrimEnd();
                        data.PRKNM = row2["PRKNM"].ToString().TrimEnd();
                        data.F_DANGA = ToLong(row2["F_DANGA"].ToString().TrimEnd());
                        data.F_CNTQTY = row2["F_CNTQTY"].ToString().TrimEnd();
                        data.F_DQTY = row2["F_DQTY"].ToString().TrimEnd();
                        data.F_DDAY = ToLong(row2["F_DDAY"].ToString().TrimEnd());
                        data.F_GUMAK = ToLong(row2["F_GUMAK"].ToString().TrimEnd());
                        data.MAFG = row2["MAFG"].ToString().TrimEnd();
                        data.GSRT = ToDouble(row2["GSRT"].ToString().TrimEnd());
                        //
                        data.BGIHO = row2["BGIHO"].ToString().TrimEnd();
                        //m_ErrPos = "DANGA : " + row2["DANGA"].ToString().TrimEnd();
                        data.DANGA = row2["DANGA"].ToString().TrimEnd();
                        data.CNTQTY = row2["CNTQTY"].ToString().TrimEnd();
                        data.DQTY = row2["DQTY"].ToString().TrimEnd();
                        data.DDAY = row2["DDAY"].ToString().TrimEnd();
                        data.JJGUMAK = row2["JJGUMAK"].ToString().TrimEnd();
                        //
                        data.DEMNO = row2["DEMNO"].ToString().TrimEnd();
                        data.REDAY = row2["REDAY"].ToString().TrimEnd();
                        data.JJRMKNM = GetSakJjrmkNm(data.JJRMK1, data.JJRMK2, data.JJDETAIL, p_conn);

                        listSak.Add(data);
                    }

                    return true;
                });

                return true;
            });

            RefreshGridBul();
            RefreshGridSak();
        }

        private string GetBulJjrmkNm(string p_jjrmk1, string p_jjrmk2, string p_jjdetail, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD' AND MST3CD='" + p_jjrmk1 + p_jjdetail + "'";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                ret = row["CDNM"].ToString().TrimEnd();
                return true;
            });
            return ret;
        }

        private string GetSakJjrmkNm(string p_jjrmk1, string p_jjrmk2, string p_jjdetail, OleDbConnection p_conn)
        {
            string ret="";
            string ret1 = "";
            string ret2 = "";
            string sql = "";
            sql = "SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD='JJCD' AND MST3CD='" + p_jjrmk1 + "'";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                ret1 = row["CDNM"].ToString().TrimEnd();
                return true;
            });
            if (p_jjrmk2 != "")
            {
                sql = "SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD='JJCD2' AND MST3CD='" + p_jjrmk2 + "'";
                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
                {
                    ret2 = row["CDNM"].ToString().TrimEnd();
                    return true;
                });
            }
            ret = ret1;
            if (ret2 != "") ret += "/" + ret2;
            return ret;
        }

        private void QueryOutYak(string p_demseq, string p_cnecno, string p_demno, string p_insmm, OleDbConnection p_conn)
        {
            List<CDataOutYak> list = new List<CDataOutYak>();
            grdOutYak.DataSource = null;
            grdOutYak.DataSource = list;

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                string sql2="";
                sql2 += Environment.NewLine + "SELECT CNECNO,GRPNO,DCOUNT,EPRTNO,OUTCNT,LNO,REMARK,JJCOUNT,MEMO,DEMSEQ ";
                sql2 += Environment.NewLine + "  FROM TIE_N0206 ";
                sql2 += Environment.NewLine + " WHERE DEMSEQ ='" + row["DEMSEQ"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND CNECNO ='" + row["CNECNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND GRPNO ='" + row["GRPNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DCOUNT ='" + row["DCOUNT"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DEMNO ='" + row["DEMNO"].ToString() + "' ";
                sql2 += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,EPRTNO,OUTCNT,LNO ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    CDataOutYak data = new CDataOutYak();
                    data.Clear();

                    data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                    data.OUTCNT = row2["OUTCNT"].ToString().TrimEnd();
                    data.LNO = row2["LNO"].ToString().TrimEnd();
                    data.REMARK = row2["REMARK"].ToString().TrimEnd();
                    data.JJCOUNT = row2["JJCOUNT"].ToString().TrimEnd();
                    data.MEMO = row2["MEMO"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });

                return true;
            });

            RefreshGridOutYak();
        }

        private void QuerySutak(string p_demseq, string p_cnecno, string p_demno, string p_insmm, OleDbConnection p_conn)
        {
            List<CDataSutak> list = new List<CDataSutak>();
            grdSutak.DataSource = null;
            grdSutak.DataSource = list;

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT CNECNO,GRPNO,DCOUNT,EPRTNO,LNO,STAKID,STAKAMT,MEMO,DEMSEQ";
                sql2 += Environment.NewLine + "  FROM TIE_N0205 ";
                sql2 += Environment.NewLine + " WHERE DEMSEQ ='" + row["DEMSEQ"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND CNECNO ='" + row["CNECNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND GRPNO ='" + row["GRPNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DCOUNT ='" + row["DCOUNT"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DEMNO ='" + row["DEMNO"].ToString() + "' ";
                sql2 += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,EPRTNO,LNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    CDataSutak data = new CDataSutak();
                    data.Clear();

                    data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                    data.LNO = row2["LNO"].ToString().TrimEnd();
                    data.STAKID = row2["STAKID"].ToString().TrimEnd();
                    data.STAKAMT = row2["STAKAMT"].ToString().TrimEnd();
                    data.MEMO = row2["MEMO"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });

                return true;
            });

            RefreshGridSutak();
        }

        private void QueryTotal(string p_demseq, string p_cnecno, string p_demno, string p_insmm, OleDbConnection p_conn)
        {
            List<CDataTotal> list = new List<CDataTotal>();
            grdTotal.DataSource = null;
            grdTotal.DataSource = list;

            CDataTotal data = new CDataTotal();
            data.Clear();

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            int ii = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                ii++;
                if (ii == 1)
                {
                    data.DEMNO = row["DEMNO"].ToString().TrimEnd();
                    data.FTDAYS = row["FTDAYS"].ToString().TrimEnd();
                    data.DEMSEQ = row["DEMSEQ"].ToString().TrimEnd();
                    data.REDAY = row["REDAY"].ToString().TrimEnd();
                    data.CNECNO = row["CNECNO"].ToString().TrimEnd();
                    data.GRPNO = row["GRPNO"].ToString().TrimEnd();
                    data.DCOUNT = row["DCOUNT"].ToString().TrimEnd();
                    data.HOSID = row["HOSID"].ToString().TrimEnd();
                    data.JBUNICD = row["JBUNICD"].ToString().TrimEnd();
                    data.MEMO = row["MEMO"].ToString().TrimEnd();
                    data.REDPT1 = row["REDPT1"].ToString().TrimEnd();
                    data.REDPT2 = row["REDPT2"].ToString().TrimEnd();
                    data.REDPNM = row["REDPNM"].ToString().TrimEnd();
                    data.RETELE = row["RETELE"].ToString().TrimEnd();
                }

                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT CNT1"; // 청구_건수
                sql2 += Environment.NewLine + "     , RQUPLMTCHATTAMT"; // 청구사항-약제상한차액 합계
                sql2 += Environment.NewLine + "     , UNAMT1"; // 청구_청구액
                sql2 += Environment.NewLine + "     , TTTAMT1"; // 청구_진료비총액
                sql2 += Environment.NewLine + "     , JBPTAMT1"; // 청구_환자납부총액
                sql2 += Environment.NewLine + "     , CNT2"; // 심결_건수
                sql2 += Environment.NewLine + "     , ASUTAMT2"; // 심결_위탁검사직접지급금
                sql2 += Environment.NewLine + "     , SKUPLMTCHATTAMT"; // 심결사항-약제상한차액 합계
                sql2 += Environment.NewLine + "     , ARSTAMT2"; // 심결_심사결정액
                sql2 += Environment.NewLine + "     , TTTAMT2"; // 심결_진료비총액
                sql2 += Environment.NewLine + "     , JBPTAMT2"; // 심결_환자납부총액
                sql2 += Environment.NewLine + "     , SKJJUPLMTCHATTAMT"; // 심결사항-약제상한차액조정금액 합계
                sql2 += Environment.NewLine + "     , RCNT1"; // 불능_건수
                sql2 += Environment.NewLine + "     , RTTAMT1"; // 불능_요양급여비용총액
                sql2 += Environment.NewLine + "     , RJBPTAMT1"; // 불능_환자납부총액
                sql2 += Environment.NewLine + "     , RCNT2"; // 감내역_건수
                sql2 += Environment.NewLine + "     , RCALCAMT2"; // 감내역_조정금액
                sql2 += Environment.NewLine + "     , RJBPTAMT2"; // 감내역_환자납부총액
                sql2 += Environment.NewLine + "  FROM TIE_N0204";
                sql2 += Environment.NewLine + " WHERE DEMSEQ ='" + row["DEMSEQ"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND CNECNO ='" + row["CNECNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND GRPNO ='" + row["GRPNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DCOUNT ='" + row["DCOUNT"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND DEMNO ='" + row["DEMNO"].ToString() + "' ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {

                    data.CNT1 += ToLong(row2["CNT1"].ToString().TrimEnd());
                    data.UNAMT1 += ToLong(row2["UNAMT1"].ToString().TrimEnd());
                    data.TTTAMT1 += ToLong(row2["TTTAMT1"].ToString().TrimEnd());
                    data.JBPTAMT1 += ToLong(row2["JBPTAMT1"].ToString().TrimEnd());
                    data.RQUPLMTCHATTAMT += ToLong(row2["RQUPLMTCHATTAMT"].ToString().TrimEnd());
                    data.CNT2 += ToLong(row2["CNT2"].ToString().TrimEnd());
                    data.ARSTAMT2 += ToLong(row2["ARSTAMT2"].ToString().TrimEnd());
                    data.TTTAMT2 += ToLong(row2["TTTAMT2"].ToString().TrimEnd());
                    data.JBPTAMT2 += ToLong(row2["JBPTAMT2"].ToString().TrimEnd());
                    data.ASUTAMT2 += ToLong(row2["ASUTAMT2"].ToString().TrimEnd());
                    data.SKUPLMTCHATTAMT += ToLong(row2["SKUPLMTCHATTAMT"].ToString().TrimEnd());
                    data.SKJJUPLMTCHATTAMT += ToLong(row2["SKJJUPLMTCHATTAMT"].ToString().TrimEnd());
                    data.RCNT1 += ToLong(row2["RCNT1"].ToString().TrimEnd());
                    data.RTTAMT1 += ToLong(row2["RTTAMT1"].ToString().TrimEnd());
                    data.RJBPTAMT1 += ToLong(row2["RJBPTAMT1"].ToString().TrimEnd());
                    data.RCNT2 += ToLong(row2["RCNT2"].ToString().TrimEnd());
                    data.RCALCAMT2 += ToLong(row2["RCALCAMT2"].ToString().TrimEnd());
                    data.RJBPTAMT2 += ToLong(row2["RJBPTAMT2"].ToString().TrimEnd());

                    return true;
                });

                return true;
            });

            list.Add(data);
            RefreshGridTotal();
        }

        public long ToLong(string p_value)
        {
            long ret = 0;
            long.TryParse(p_value, out ret);
            return ret;
        }

        public double ToDouble(string p_value)
        {
            double ret = 0;
            double.TryParse(p_value, out ret);
            return ret;
        }

        private void QueryComb(string p_demseq, string p_cnecno, string p_demno, string p_insmm, string p_iofg, OleDbConnection p_conn)
        {
            string tTI1A = "TI1A";
            string tTI1F = "TI1F";
            string tTI13 = "TI13";
            string fEXDATE = "EXDATE";
            if (p_iofg != "1")
            {
                tTI1A = "TI2A";
                tTI1F = "TI2F";
                tTI13 = "TI23";
                fEXDATE = "BDODT";
            }

            List<CDataComb> list = new List<CDataComb>();
            grdComb.DataSource = null;
            grdComb.DataSource = list;

            string sql = "";
            if (cboQueryOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else if (cboQueryOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + "SELECT N0201.* ";
                sql += Environment.NewLine + "  FROM TIE_N0201 N0201 LEFT JOIN TIE_H010 H010 ON N0201.DEMNO=H010.DEMNO";
                sql += Environment.NewLine + " WHERE N0201.FTDAYS LIKE '" + p_insmm.Substring(0, 4) + "%' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }
            else
            {
                // 기본
                sql += Environment.NewLine + "SELECT * ";
                sql += Environment.NewLine + "  FROM TIE_N0201 ";
                sql += Environment.NewLine + " WHERE DEMSEQ = '" + p_demseq + "' ";
                sql += Environment.NewLine + "   AND CNECNO = '" + p_cnecno + "' ";
                sql += Environment.NewLine + " ORDER BY DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                string sql2 = "";
                sql2 = "";
                sql2 += Environment.NewLine + "SELECT N2.DEMSEQ";
                sql2 += Environment.NewLine + "     , N2.CNECNO";
                sql2 += Environment.NewLine + "     , N2.GRPNO";
                sql2 += Environment.NewLine + "     , N2.DCOUNT";
                sql2 += Environment.NewLine + "     , N2.DEMNO";
                sql2 += Environment.NewLine + "     , N2.EPRTNO";
                sql2 += Environment.NewLine + "     , N2.PNM";
                sql2 += Environment.NewLine + "     , N2.JRHT";
                sql2 += Environment.NewLine + "     , N2.CHOGUM";
                sql2 += Environment.NewLine + "     , N2.JAEGUM";
                sql2 += Environment.NewLine + "     , N2.UNAMT";
                sql2 += Environment.NewLine + "     , N2.TTTAMT";
                sql2 += Environment.NewLine + "     , N2.JBPTAMT";
                sql2 += Environment.NewLine + "     , N2.GBFG";
                sql2 += Environment.NewLine + "     , N2.ASTAMT";
                sql2 += Environment.NewLine + "     , N2.ACTGUM";
                sql2 += Environment.NewLine + "     , N2.AJBPTAMT";
                sql2 += Environment.NewLine + "     , N2.CHOCNT";
                sql2 += Environment.NewLine + "     , N2.CHONCNT";
                sql2 += Environment.NewLine + "     , N2.JAECNT";
                sql2 += Environment.NewLine + "     , N2.JAENCNT";
                sql2 += Environment.NewLine + "     , N2.EXAMC";
                sql2 += Environment.NewLine + "     , N2.ORDDAYS";
                sql2 += Environment.NewLine + "     , N2.ORDCNT";
                sql2 += Environment.NewLine + "     , N2.STEDT";
                sql2 += Environment.NewLine + "     , N2.RETEAM";
                sql2 += Environment.NewLine + "     , N2.MDACD";
                sql2 += Environment.NewLine + "     , N2.MEMO";
                sql2 += Environment.NewLine + "     , N2.RQUPLMTCHATTAMT";
                sql2 += Environment.NewLine + "     , N2.SKUPLMTCHATTAMT";
                sql2 += Environment.NewLine + "     , A.PID";
                sql2 += Environment.NewLine + "     , A.RESID";
                sql2 += Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD";
                sql2 += Environment.NewLine + "     , A.PDRID";
                sql2 += Environment.NewLine + "     , (SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A.PDRID) AS DRNM";
                sql2 += Environment.NewLine + "  FROM TIE_N0202 N2 LEFT JOIN " + tTI1A + " A ON A.DEMNO=N2.DEMNO AND A.EPRTNO=N2.EPRTNO";
                sql2 += Environment.NewLine + " WHERE N2.DEMSEQ ='" + row["DEMSEQ"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N2.CNECNO ='" + row["CNECNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N2.GRPNO ='" + row["GRPNO"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N2.DCOUNT ='" + row["DCOUNT"].ToString() + "' ";
                sql2 += Environment.NewLine + "   AND N2.DEMNO ='" + row["DEMNO"].ToString() + "' ";
                sql2 += Environment.NewLine + " ORDER BY N2.DEMSEQ,N2.CNECNO,N2.GRPNO,N2.DCOUNT,N2.DEMNO,N2.EPRTNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    // 삭감액을 읽는다.
                    string sql3 = "";
                    sql3 = "";
                    sql3 += Environment.NewLine + "SELECT SUM(CASE WHEN F.MAFG='2' THEN N3.JJGUMAK+(N3.JJGUMAK*(A.GSRT/100)) ELSE N3.JJGUMAK END) AS SAKGAMAMT";
                    sql3 += Environment.NewLine + "  FROM TIE_N0203 N3 LEFT JOIN " + tTI1A + " A ON  A.DEMNO =N3.DEMNO AND A.EPRTNO=N3.EPRTNO";
                    sql3 += Environment.NewLine + "                    LEFT JOIN " + tTI1F + " F ON  F." + fEXDATE + "=A." + fEXDATE + "";
                    sql3 += Environment.NewLine + "                                         AND F.QFYCD=A.QFYCD";
                    sql3 += Environment.NewLine + "                                         AND F.JRBY=A.JRBY";
                    sql3 += Environment.NewLine + "                                         AND F.PID=A.PID";
                    sql3 += Environment.NewLine + "                                         AND F.UNISQ=A.UNISQ";
                    sql3 += Environment.NewLine + "                                         AND F.SIMCS=A.SIMCS";
                    sql3 += Environment.NewLine + "                                         AND F.ELINENO=N3.LNO";
                    sql3 += Environment.NewLine + " WHERE N3.DEMSEQ='" + row["DEMSEQ"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.CNECNO='" + row["CNECNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.GRPNO='" + row["GRPNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.DCOUNT='" + row["DCOUNT"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.DEMNO='" + row["DEMNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.EPRTNO='" + row2["EPRTNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.LNO<>0 ";

                    string sakgamamt = "";
                    string sakgamamt2 = "";
                    MetroLib.SqlHelper.GetDataRow(sql3, p_conn, delegate(System.Data.DataRow row3)
                    {
                        sakgamamt = row3["SAKGAMAMT"].ToString().TrimEnd(); // 삭감금액
                        double amt = 0;
                        double.TryParse(sakgamamt, out amt);
                        int iamt = Convert.ToInt32(amt + 9);
                        iamt /= 10;
                        iamt *= 10;
                        sakgamamt2 = iamt.ToString();  // 실삭감액

                        return true;
                    });

                    CDataComb data = new CDataComb();
                    data.Clear();

                    data.EPRTNO = row2["EPRTNO"].ToString().TrimEnd();
                    data.PID = row2["PID"].ToString().TrimEnd();
                    data.PNM = row2["PNM"].ToString().TrimEnd();
                    data.RESID = row2["RESID"].ToString().TrimEnd();
                    data.DPTCD = row2["DPTCD"].ToString().TrimEnd();
                    // 청구사항
                    data.TTTAMT = row2["TTTAMT"].ToString().TrimEnd(); // 진료비총액
                    data.UNAMT = row2["UNAMT"].ToString().TrimEnd(); // 청구액
                    data.JBPTAMT = row2["JBPTAMT"].ToString().TrimEnd(); //  환자납부액
                    data.RQUPLMTCHATTAMT = row2["RQUPLMTCHATTAMT"].ToString().TrimEnd(); // 약제상한차액
                    // 심결사항
                    data.ACTGUM = row2["ACTGUM"].ToString().TrimEnd(); // 심사결정액
                    data.AJBPTAMT = row2["AJBPTAMT"].ToString().TrimEnd(); // 환자납부액
                    data.SKUPLMTCHATTAMT = row2["SKUPLMTCHATTAMT"].ToString().TrimEnd(); // 약제상한차액
                    data.ASTAMT = row2["ASTAMT"].ToString().TrimEnd(); // 위탁검사직접지급금

                    data.SAKGAMAMT = sakgamamt; // 삭감금액
                    data.SAKGAMAMT2 = sakgamamt2;  // 실삭감액

                    data.MEMO = row2["MEMO"].ToString().TrimEnd();
                    data.RETEAM = row2["RETEAM"].ToString().TrimEnd(); // 심사담당조
                    data.DEMNO = row2["DEMNO"].ToString().TrimEnd(); // 청구번호

                    data.DATA_FG = "1";

                    list.Add(data);


                    // 줄단위자료를 읽는다.
                    sql3 = "";
                    sql3 += Environment.NewLine + "SELECT N3.DEMSEQ";
                    sql3 += Environment.NewLine + "     , N3.CNECNO";
                    sql3 += Environment.NewLine + "     , N3.GRPNO";
                    sql3 += Environment.NewLine + "     , N3.DCOUNT";
                    sql3 += Environment.NewLine + "     , N3.DEMNO";
                    sql3 += Environment.NewLine + "     , N3.EPRTNO";
                    sql3 += Environment.NewLine + "     , N3.LNO";
                    sql3 += Environment.NewLine + "     , N3.BGIHO";
                    sql3 += Environment.NewLine + "     , N3.DANGA";
                    sql3 += Environment.NewLine + "     , N3.DQTY";
                    sql3 += Environment.NewLine + "     , N3.DDAY";
                    sql3 += Environment.NewLine + "     , N3.JJRMK AS JJRMK1";
                    sql3 += Environment.NewLine + "     , N3.JJRMK2";
                    sql3 += Environment.NewLine + "     , N3.JJDETAIL";
                    sql3 += Environment.NewLine + "     , N3.JJGUMAK";
                    sql3 += Environment.NewLine + "     , N3.JJTEXT";
                    sql3 += Environment.NewLine + "     , CASE WHEN ISNUMERIC(N3.JJRMK)=1";
                    sql3 += Environment.NewLine + "            THEN (SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD = 'BULCD' AND MST3CD=N3.JJRMK + ISNULL(N3.JJDETAIL,''))";
                    sql3 += Environment.NewLine + "            ELSE (SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD = 'JJCD' AND MST3CD=N3.JJRMK)";
                    sql3 += Environment.NewLine + "                 + ISNULL((SELECT '/' + CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD = 'JJCD2' AND MST3CD=N3.JJRMK2),'')";
                    sql3 += Environment.NewLine + "       END AS JJRMKDOC";
                    sql3 += Environment.NewLine + "     , CASE WHEN F.MAFG='2' THEN N3.JJGUMAK+(N3.JJGUMAK*(A.GSRT/100)) ELSE N3.JJGUMAK END AS JJGAGUMAK";
                    sql3 += Environment.NewLine + "     , F.PRICD AS F_PRICD";
                    sql3 += Environment.NewLine + "     , F.BGIHO AS F_BGIHO";
                    sql3 += Environment.NewLine + "     , F.PRKNM AS F_PRKNM";
                    sql3 += Environment.NewLine + "     , F.DQTY  AS F_DQTY";
                    sql3 += Environment.NewLine + "     , F.DDAY  AS F_DDAY";
                    sql3 += Environment.NewLine + "     , F.GUMAK AS F_GUMAK";
                    sql3 += Environment.NewLine + "     , CASE WHEN F.MAFG='2' THEN F.GUMAK+(F.GUMAK*(A.GSRT/100)) ELSE F.GUMAK END AS F_GAGUMAK";
                    sql3 += Environment.NewLine + "     , N3.UPLMTAMT";
                    sql3 += Environment.NewLine + "     , N3.JJUPLMTCHAAMT";
                    sql3 += Environment.NewLine + "  FROM TIE_N0203 N3 LEFT JOIN " + tTI1A + " A ON A.DEMNO =N3.DEMNO AND A.EPRTNO=N3.EPRTNO";
                    sql3 += Environment.NewLine + "                    LEFT JOIN " + tTI1F + " F ON F." + fEXDATE + "=A." + fEXDATE + "";
                    sql3 += Environment.NewLine + "                                    AND F.QFYCD=A.QFYCD";
                    sql3 += Environment.NewLine + "                                    AND F.JRBY=A.JRBY";
                    sql3 += Environment.NewLine + "                                    AND F.PID=A.PID";
                    sql3 += Environment.NewLine + "                                    AND F.UNISQ=A.UNISQ";
                    sql3 += Environment.NewLine + "                                    AND F.SIMCS=A.SIMCS";
                    sql3 += Environment.NewLine + "                                    AND F.ELINENO=N3.LNO";
                    sql3 += Environment.NewLine + " WHERE N3.DEMSEQ='" + row["DEMSEQ"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.CNECNO='" + row["CNECNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.GRPNO='" + row["GRPNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.DCOUNT='" + row["DCOUNT"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.DEMNO='" + row["DEMNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N3.EPRTNO='" + row2["EPRTNO"].ToString() + "'";
                    sql3 += Environment.NewLine + " ORDER BY N3.DEMSEQ,N3.CNECNO,N3.GRPNO,N3.DCOUNT,N3.DEMNO,N3.EPRTNO,N3.LNO ";

                    MetroLib.SqlHelper.GetDataRow(sql3, p_conn, delegate(System.Data.DataRow row3)
                    {
                        data = new CDataComb();
                        data.Clear();

                        data.LNO = row3["LNO"].ToString().TrimEnd();
                        data.F_PRICD = row3["F_PRICD"].ToString().TrimEnd();
                        data.F_BGIHO = row3["F_BGIHO"].ToString().TrimEnd();
                        data.F_PRKNM = row3["F_PRKNM"].ToString().TrimEnd();
                        data.F_DQTY = row3["F_DQTY"].ToString().TrimEnd();
                        data.F_DDAY = row3["F_DDAY"].ToString().TrimEnd();
                        data.F_GUMAK = row3["F_GUMAK"].ToString().TrimEnd();
                        data.BGIHO = row3["BGIHO"].ToString().TrimEnd();
                        data.DQTY = row3["DQTY"].ToString().TrimEnd();
                        data.DDAY = row3["DDAY"].ToString().TrimEnd();
                        data.JJGUMAK = row3["JJGUMAK"].ToString().TrimEnd();
                        data.JJGAGUMAK = row3["JJGAGUMAK"].ToString().TrimEnd();
                        data.UPLMTAMT = row3["UPLMTAMT"].ToString().TrimEnd();
                        data.JJUPLMTCHAAMT = row3["JJUPLMTCHAAMT"].ToString().TrimEnd();
                        data.JJRMK1 = row3["JJRMK1"].ToString().TrimEnd();
                        data.JJRMK2 = row3["JJRMK2"].ToString().TrimEnd();
                        data.JJDETAIL = row3["JJDETAIL"].ToString().TrimEnd();
                        data.JJTEXT = row3["JJTEXT"].ToString().TrimEnd();
                        data.JJRMKDOC = row3["JJRMKDOC"].ToString().TrimEnd();
                        data.DATA_FG = "2";

                        list.Add(data);
                        return true;
                    });

                    // 줄단위자료를 읽는다.(원외)
                    sql3 = "";
                    sql3 += Environment.NewLine + "SELECT N6.DEMSEQ";
                    sql3 += Environment.NewLine + "     , N6.CNECNO";
                    sql3 += Environment.NewLine + "     , N6.GRPNO";
                    sql3 += Environment.NewLine + "     , N6.DCOUNT";
                    sql3 += Environment.NewLine + "     , N6.DEMNO";
                    sql3 += Environment.NewLine + "     , N6.EPRTNO";
                    sql3 += Environment.NewLine + "     , N6.LNO";
                    sql3 += Environment.NewLine + "     , N6.OUTCNT  AS BGIHO";
                    sql3 += Environment.NewLine + "     , N6.JJCOUNT AS DQTY";
                    sql3 += Environment.NewLine + "     , N6.REMARK  AS JJRMK1";
                    sql3 += Environment.NewLine + "     , N6.MEMO    AS JJTEXT";
                    sql3 += Environment.NewLine + "     , (SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD IN ('BULCD','JJCD') AND MST3CD=N6.REMARK) AS JJRMKDOC";
                    sql3 += Environment.NewLine + "     , F.PRICD AS F_PRICD";
                    sql3 += Environment.NewLine + "     , F.BGIHO AS F_BGIHO";
                    sql3 += Environment.NewLine + "     , F.PRKNM AS F_PRKNM";
                    sql3 += Environment.NewLine + "     , F.DQTY  AS F_DQTY";
                    sql3 += Environment.NewLine + "     , F.DDAY  AS F_DDAY";
                    sql3 += Environment.NewLine + "     , F.GUMAK AS F_GUMAK";
                    sql3 += Environment.NewLine + "     , F.GUMAK AS F_GAGUMAK";
                    sql3 += Environment.NewLine + "  FROM TIE_N0206 N6 LEFT JOIN " + tTI1A + " A ON A.DEMNO =N6.DEMNO AND A.EPRTNO=N6.EPRTNO";
                    sql3 += Environment.NewLine + "                    LEFT JOIN " + tTI13 + " F ON F." + fEXDATE + "=A." + fEXDATE + "";
                    sql3 += Environment.NewLine + "                                    AND F.QFYCD=A.QFYCD";
                    sql3 += Environment.NewLine + "                                    AND F.JRBY =A.JRBY";
                    sql3 += Environment.NewLine + "                                    AND F.PID  =A.PID";
                    sql3 += Environment.NewLine + "                                    AND F.UNISQ=A.UNISQ";
                    sql3 += Environment.NewLine + "                                    AND F.SIMCS=A.SIMCS";
                    sql3 += Environment.NewLine + "                                    AND F.OUTSEQ=N6.OUTCNT";
                    sql3 += Environment.NewLine + "                                    AND F.ELINENO=N6.LNO";
                    sql3 += Environment.NewLine + " WHERE N6.DEMSEQ='" + row["DEMSEQ"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N6.CNECNO='" + row["CNECNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N6.GRPNO='" + row["GRPNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N6.DCOUNT='" + row["DCOUNT"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N6.DEMNO='" + row["DEMNO"].ToString() + "'";
                    sql3 += Environment.NewLine + "   AND N6.EPRTNO='" + row2["EPRTNO"].ToString() + "'";
                    sql3 += Environment.NewLine + " ORDER BY N6.DEMSEQ,N6.CNECNO,N6.GRPNO,N6.DCOUNT,N6.DEMNO,N6.EPRTNO,N6.OUTCNT,N6.LNO";

                    MetroLib.SqlHelper.GetDataRow(sql3, p_conn, delegate(System.Data.DataRow row3)
                    {
                        data = new CDataComb();
                        data.Clear();

                        data.LNO = row3["LNO"].ToString().TrimEnd();
                        data.F_PRICD = row3["F_PRICD"].ToString().TrimEnd();
                        data.F_BGIHO = row3["F_BGIHO"].ToString().TrimEnd();
                        data.F_PRKNM = row3["F_PRKNM"].ToString().TrimEnd();
                        data.F_DQTY = row3["F_DQTY"].ToString().TrimEnd();
                        data.F_DDAY = row3["F_DDAY"].ToString().TrimEnd();
                        data.F_GUMAK = row3["F_GUMAK"].ToString().TrimEnd();
                        data.BGIHO = row3["BGIHO"].ToString().TrimEnd();
                        data.DQTY = row3["DQTY"].ToString().TrimEnd();
                        data.JJRMK1 = row3["JJRMK1"].ToString().TrimEnd();
                        data.JJTEXT = row3["JJTEXT"].ToString().TrimEnd();
                        data.JJRMKDOC = row3["JJRMKDOC"].ToString().TrimEnd();
                        data.DATA_FG = "2";

                        list.Add(data);
                        return true;
                    });

                    //
                    return true;
                });

                //
                return true;
            });

            RefreshGridComb();
        }

        private void grdPtntView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (grdPtntView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;

                string iofg = grdPtntView.GetRowCellValue(grdPtntView.FocusedRowHandle, "IOFG").ToString();
                string demno = grdPtntView.GetRowCellValue(grdPtntView.FocusedRowHandle, "DEMNO").ToString();
                string eprtno = grdPtntView.GetRowCellValue(grdPtntView.FocusedRowHandle, "EPRTNO").ToString();

                string tTI1A = "TI1A";
                string fEXDATE = "EXDATE";
                if (iofg == "2")
                {
                    tTI1A = "TI2A";
                    fEXDATE = "BDODT";
                }

                string exdate = "";
                string qfycd = "";
                string jrby = "";
                string pid = "";
                string unisq = "";
                string simcs = "";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql += Environment.NewLine + "SELECT " + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS";
                    sql += Environment.NewLine + "  FROM " + tTI1A + "";
                    sql += Environment.NewLine + " WHERE DEMNO='" + demno + "'";
                    sql += Environment.NewLine + "   AND EPRTNO='" + eprtno + "'";

                    MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(System.Data.DataRow row)
                    {
                        exdate = row[fEXDATE].ToString();
                        qfycd = row["QFYCD"].ToString();
                        jrby = row["JRBY"].ToString();
                        pid = row["PID"].ToString();
                        unisq = row["UNISQ"].ToString();
                        simcs = row["SIMCS"].ToString();

                        return true;
                    });
                }

                if (exdate == "")
                {
                    MessageBox.Show("명세서를 찾을 수 없습니다. (iofg=" + iofg + ", demno=" + demno + ", eprtno=" + eprtno);

                }
                else
                {
                    ADD0712E_JABO_S f = new ADD0712E_JABO_S(iofg, exdate, qfycd, jrby, pid, unisq, simcs);
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print();
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

        private void Print()
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            if (tabControl1.SelectedIndex == 0)
            {
                printableComponentLink.Component = grdMain;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                printableComponentLink.Component = grdPtnt;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                printableComponentLink.Component = grdBul;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                printableComponentLink.Component = grdSak;
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                printableComponentLink.Component = grdOutYak;
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                printableComponentLink.Component = grdSutak;
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                printableComponentLink.Component = grdTotal;
            }
            else if (tabControl1.SelectedIndex == 7)
            {
                printableComponentLink.Component = grdComb;
            }
            //printableComponentLink.ShowPreview(); // <-- 아래 세 줄로 변경
            printableComponentLink.CreateDocument();
            DevExpress.XtraPrinting.PrintTool printTool = new DevExpress.XtraPrinting.PrintTool(printableComponentLink.PrintingSystemBase);
            printTool.ShowRibbonPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "자동차보험심사결과통지서";
            strTitle += "(" + tabControl1.SelectedTab.Text.ToString() + ")";

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strCaption = "";
            //strCaption += "접수번호 : " + txtAccno.Text.ToString();
            //strCaption += ", 차수 : " + txtCntno.Text.ToString();
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string sysDate = "";
            string sysTime = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sysDate = MetroLib.Util.GetSysDate(conn);
                sysTime = MetroLib.Util.GetSysTime(conn);
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0712E_JABO", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void mnuBOMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                BOMakeOne();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void BOMakeOne()
        {
            int i = grdMainView.FocusedRowHandle;
            string iofg = grdMainView.GetRowCellValue(i, "IOFG").ToString();
            string cnecno = grdMainView.GetRowCellValue(i, "CNECNO").ToString();
            string dcount = grdMainView.GetRowCellValue(i, "DCOUNT").ToString();
            string demno = grdMainView.GetRowCellValue(i, "DEMNO").ToString();
            string demseq = grdMainView.GetRowCellValue(i, "DEMSEQ").ToString();
            string grpno = grdMainView.GetRowCellValue(i, "GRPNO").ToString();

            ADD0712E_JABO_BO f = new ADD0712E_JABO_BO(m_User, m_Pwd, m_Prjcd, iofg, cnecno, dcount, demno, demseq, grpno);
            f.ShowDialog(this);

        }

        private void mnuObjMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ObjMakeOne();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void ObjMakeOne()
        {
            int i = grdMainView.FocusedRowHandle;
            string para = "";
            para += "1" + (char)21; // 0.재심사 1.이의신청
            para += "2" + (char)21; // 0.보험 1.보호 2.자보
            para += grdMainView.GetRowCellValue(i, "CNECNO").ToString() +(char)21;
            para += grdMainView.GetRowCellValue(i, "DCOUNT").ToString() + (char)21;
            para += grdMainView.GetRowCellValue(i, "DEMNO").ToString() + (char)21;
            para += grdMainView.GetRowCellValue(i, "DEMSEQ").ToString() + (char)21;
            para += grdMainView.GetRowCellValue(i, "GRPNO").ToString() + (char)21;
            para += grdMainView.GetRowCellValue(i, "REDAY").ToString();

            string addPara = "user=" + m_User + ",pwd=" + m_Pwd + ",prjcd=" + m_Prjcd + ",addpara=" + para;
            // 호출
            string exefile = "C:/Metro/DLL/ADD0602E.exe";
            string exefolder = "C:/Metro/DLL/";
            this.ExecCmd(exefile, exefolder, addPara);
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

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdPtntView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdBulView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdSakView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdOutYakView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdSutakView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdTotalView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdCombView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void btnQueryOneYear_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("0");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuerySixMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("1");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryThreeMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("2");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryOneMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("3");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryOneWeek_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("4");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryNoLimit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("5");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

    }
}

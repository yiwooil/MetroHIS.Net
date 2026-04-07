using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0711E_JABO
{
    public partial class ADD0711E_JABO : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        public ADD0711E_JABO()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0711E_JABO(String user, String pwd, String prjcd, String addpara)
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
//            txtCnectmm.Text = "";
            string multihosyn = "";

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            grdSub.DataSource = null;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql="";
                sql += Environment.NewLine + "SELECT FLD2QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='0'";
                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    multihosyn = row["FLD2QTY"].ToString();
                    return true;
                });

                SetFrdtTodt(queryOption, conn);

                sql="";
                sql += Environment.NewLine + "SELECT N0102.CALLFG";
                sql += Environment.NewLine + "     , N0102.CNECTDD";
                sql += Environment.NewLine + "     , N0102.JSBS";
                sql += Environment.NewLine + "     , N0102.CNECTNO";
                sql += Environment.NewLine + "     , N0102.JBFG";
                sql += Environment.NewLine + "     , H010.JRDIV";
                sql += Environment.NewLine + "     , H010.IOFG";
                sql += Environment.NewLine + "     , H010.JRKWAFG";
                sql += Environment.NewLine + "     , N0102.DEMNO";
                sql += Environment.NewLine + "     , N0102.SPTID SPTID";
                sql += Environment.NewLine + "     , N0102.CNECTFG";
                sql += Environment.NewLine + "     , N0102.DEMCNT";
                sql += Environment.NewLine + "     , N0102.UNAMT";
                sql += Environment.NewLine + "     , N0102.YYMM";
                sql += Environment.NewLine + "     , N0102.ADDZ1";
                sql += Environment.NewLine + "     , CASE WHEN (SELECT COUNT(*) FROM TIE_N0103 N0103 WHERE N0103.CALLFG=N0102.CALLFG AND N0103.CNECTDD=N0102.CNECTDD AND N0103.DEMNO=N0102.DEMNO)>0 THEN '1' ELSE '0' END AS ASTAR";
                sql += Environment.NewLine + "     , H010.DEMCNT AS H010_DEMCNT";
                sql += Environment.NewLine + "     , H010.TTAMT AS H010_TTAMT";
                sql += Environment.NewLine + "     , H010.JBPTAMT AS H010_JBPTAMT";
                sql += Environment.NewLine + "     , H010.UNAMT AS H010_UNAMT";
                sql += Environment.NewLine + "     , H010.UNICD AS UNICD";
                sql += Environment.NewLine + "  FROM TIE_N0102 N0102 LEFT JOIN TIE_H010  H010  ON H010.DEMNO=N0102.DEMNO";
                sql += Environment.NewLine + " WHERE 1=1";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0102.CNECTDD>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0102.CNECTDD<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0102.CNECTNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0102.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if (txtInsmm.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N0102.YYMM='" + txtInsmm.Text.ToString() + "'";
                }
                sql += Environment.NewLine + "  ORDER  BY N0102.CNECTDD DESC";


                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    if (multihosyn == "1")
                    {
                        string multihsfg = GetMultihsfg(row["DEMNO"].ToString().TrimEnd(), conn);
                        if (multihsfg != m_HospMulti) return true;
                    }

                    CData data = new CData();
                    data.Clear();

                    data.CALLFG = row["CALLFG"].ToString().TrimEnd();
                    data.CNECTDD = row["CNECTDD"].ToString().TrimEnd();
                    data.JSBS = row["JSBS"].ToString().TrimEnd();
                    data.CNECTNO = row["CNECTNO"].ToString().TrimEnd();
                    data.JBFG = row["JBFG"].ToString().TrimEnd();
                    data.JRDIV = row["JRDIV"].ToString().TrimEnd();
                    data.IOFG = row["IOFG"].ToString();
                    data.JRKWAFG = row["JRKWAFG"].ToString();
                    data.DEMNO = row["DEMNO"].ToString().TrimEnd();
                    data.SPTID = row["SPTID"].ToString().TrimEnd();
                    data.CNECTFG = row["CNECTFG"].ToString().TrimEnd();
                    data.DEMCNT = ToLong(row["DEMCNT"].ToString().TrimEnd());
                    data.UNAMT = ToLong(row["UNAMT"].ToString().TrimEnd());
                    data.YYMM = row["YYMM"].ToString().TrimEnd();
                    data.ADDZ1 = row["ADDZ1"].ToString().TrimEnd();
                    data.ASTAR = row["ASTAR"].ToString();
                    data.H010_DEMCNT = ToLong(row["H010_DEMCNT"].ToString());
                    data.H010_TTAMT = ToLong(row["H010_TTAMT"].ToString());
                    data.H010_JBPTAMT = ToLong(row["H010_JBPTAMT"].ToString());
                    data.H010_UNAMT = ToLong(row["H010_UNAMT"].ToString());
                    data.JBUNICD = row["UNICD"].ToString();

                    string version = "";
                    string fmno = "";
                    string hosid = "";
                    string cnectmm = "";

                    ReadN0101(data.CALLFG, data.CNECTDD, ref version, ref fmno, ref hosid, ref cnectmm, conn);

                    data.VERSION = version;
                    data.FMNO = fmno;
                    data.HOSID = hosid;
                    data.CNECTMM = cnectmm;

                    list.Add(data);

                    return true;
                });

            }

            RefreshGridMain();
            RefreshGridSub();
        }

        private long ToLong(string value)
        {
            long ret = 0;
            long.TryParse(value, out ret);
            return ret;
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

        private string GetMultihsfg(string p_demno, OleDbConnection p_conn)
        {
            bool bFind = false;
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT MULTIHSFG ";
            sql += Environment.NewLine + "  FROM TIE_H010 ";
            sql += Environment.NewLine + " WHERE DEMNO='" + p_demno + "' ";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                bFind = true;
                ret = row["MULTIHSFG"].ToString();
                return true;
            });
            if (bFind == true) return ret;

            // 치료재료신고서에서 검색
            sql = "";
            sql += Environment.NewLine + "SELECT MULTIHSFG ";
            sql += Environment.NewLine + "  FROM TIE_H0601 ";
            sql += Environment.NewLine + " WHERE DEMNO='" + p_demno + "' ";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                bFind = true;
                ret = row["MULTIHSFG"].ToString();
                return true;
            });
            if (bFind == true) return ret;

            // 의약품구입내역에서 검색
            sql = "";
            sql += Environment.NewLine + "SELECT MULTIHSFG ";
            sql += Environment.NewLine + "  FROM TIE_H0801 ";
            sql += Environment.NewLine + " WHERE DEMYM='" + p_demno + "' ";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                bFind = true;
                ret = row["MULTIHSFG"].ToString();
                return true;
            });
            return ret;
        }

        private void ReadN0101(string p_callfg, string p_cnectdd, ref string p_version, ref string p_fmno, ref string p_hosid, ref string p_cnectmm, OleDbConnection p_conn)
        {
            string version = "";
            string fmno = "";
            string hosid = "";
            string cnectmm = "";

            string sql = "";
            sql += Environment.NewLine + "SELECT VERSION";
            sql += Environment.NewLine + "     , FMNO";
            sql += Environment.NewLine + "     , HOSID";
            sql += Environment.NewLine + "     , RTRIM(CNECTMM) AS CNECTMM";
            sql += Environment.NewLine + "  FROM TIE_N0101 ";
            sql += Environment.NewLine + " WHERE CALLFG = '" + p_callfg + "' ";
            sql += Environment.NewLine + "   AND CNECTDD = '" + p_cnectdd + "' ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                version = row["VERSION"].ToString();
                fmno = row["FMNO"].ToString();
                hosid = row["HOSID"].ToString();
                if (cnectmm == "")
                {
                    cnectmm = row["CNECTMM"].ToString();
                }
                else
                {
                    cnectmm += "," + row["CNECTMM"].ToString();
                }
                return true;
            });

            p_version = version;
            p_fmno = fmno;
            p_hosid = hosid;
            p_cnectmm = cnectmm;
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

        private void grdMainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;
                string cnectmm = grdMainView.GetRowCellValue(e.FocusedRowHandle, "CNECTMM").ToString();
                txtCnectmm.Text = cnectmm;
                string astar = grdMainView.GetRowCellValue(e.FocusedRowHandle, "ASTAR").ToString();
                if (astar == "1")
                {
                    string callfg = grdMainView.GetRowCellValue(e.FocusedRowHandle, "CALLFG").ToString();
                    string cnectdd = grdMainView.GetRowCellValue(e.FocusedRowHandle, "CNECTDD").ToString();
                    string demno = grdMainView.GetRowCellValue(e.FocusedRowHandle, "DEMNO").ToString();

                    ReadN0103(callfg, cnectdd, demno);

                }
                else
                {
                    grdSub.DataSource = null;
                    RefreshGridSub();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadN0103(string p_callfg, string p_cnectdd, string p_demno)
        {
            List<CDataSub> list = new List<CDataSub>();
            grdSub.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT EPRTNO";
                sql += Environment.NewLine + "     , RCODE";
                sql += Environment.NewLine + "     , SEQ";
                sql += Environment.NewLine + "     , RMEMO";
                sql += Environment.NewLine + "     , (SELECT CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD' AND MST3CD=REPLACE(A.RCODE,'-','')) AS CDNM";
                sql += Environment.NewLine + "  FROM TIE_N0103 A ";
                sql += Environment.NewLine + " WHERE A.CALLFG  = '" + p_callfg + "' ";
                sql += Environment.NewLine + "   AND A.CNECTDD = '" + p_cnectdd + "' ";
                sql += Environment.NewLine + "   AND A.DEMNO   = '" + p_demno + "' ";
                sql += Environment.NewLine + " ORDER BY A.CNECTDD,A.DEMNO ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSub data = new CDataSub();
                    data.Clear();

                    data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd();
                    data.RCODE = reader["RCODE"].ToString().TrimEnd();
                    data.SEQ = reader["SEQ"].ToString().TrimEnd();
                    data.RMEMO = reader["RMEMO"].ToString().TrimEnd();
                    data.CDNM = reader["CDNM"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridSub();
        }

        private void grdMainView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle == -1) return;

            string astar = grdMainView.GetRowCellValue(e.RowHandle,"ASTAR").ToString();
            if (astar == "1")
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "UNAMT")
            {
                long unamt = ToLong(grdMainView.GetRowCellValue(e.RowHandle, "UNAMT").ToString());
                long h010_unamt = ToLong(grdMainView.GetRowCellValue(e.RowHandle, "H010_UNAMT").ToString());
                if (unamt != h010_unamt)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
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
            printableComponentLink.Component = grdMain;
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("[자보]접수증", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string subTitle = "";
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(subTitle, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
            e.Graph.DrawString("ADD0711E_JABO", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

    }
}

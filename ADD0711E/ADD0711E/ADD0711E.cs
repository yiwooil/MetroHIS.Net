using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0711E
{
    public partial class ADD0711E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private string m_ErrPos = "";

        public ADD0711E()
        {
            InitializeComponent();
        }

        public ADD0711E(String user, String pwd, String prjcd, String addpara)
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
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("보완청구내성생성", new EventHandler(mnuBOMake_Click));
            //cm.MenuItems.Add("이의신청", new EventHandler(mnuObjMake_Click));
            //grdMain.ContextMenu = cm;
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
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                SetFrdtTodt(queryOption, conn);

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT F0102.CALLFG";
                sql += Environment.NewLine + "     , F0102.CNECTDD";
                sql += Environment.NewLine + "     , F0102.JSBS";
                sql += Environment.NewLine + "     , F0102.CNECTNO";
                sql += Environment.NewLine + "     , F0102.JBFG";
                sql += Environment.NewLine + "     , H010.JRDIV";
                sql += Environment.NewLine + "     , H010.IOFG";
                sql += Environment.NewLine + "     , H010.JRKWAFG";
                sql += Environment.NewLine + "     , F0102.DEMNO";
                sql += Environment.NewLine + "     , F0102.SPTID";
                sql += Environment.NewLine + "     , F0102.CNECTFG";
                sql += Environment.NewLine + "     , F0102.DEMCNT";
                sql += Environment.NewLine + "     , F0102.UNAMT";
                sql += Environment.NewLine + "     , F0102.YYMM";
                sql += Environment.NewLine + "     , F0102.ADDZ1";
                sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TIE_H0601 H0601 WHERE H0601.DEMNO=F0102.DEMNO) AS H0601_CNT";
                sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TIE_H0801 H0801 WHERE H0801.DEMYM=F0102.DEMNO) AS H0801_CNT";
                sql += Environment.NewLine + "     , H010.DEMCNT AS H010_DEMCNT"; // 2012.04.10 WOOIL
                sql += Environment.NewLine + "     , H010.TTAMT AS H010_TTAMT"; // 2008.11.13 WOOIL
                sql += Environment.NewLine + "     , H010.PTAMT AS H010_PTAMT"; // 2008.11.13 WOOIL
                sql += Environment.NewLine + "     , H010.UNAMT AS H010_UNAMT"; // 2008.11.13 WOOIL
                sql += Environment.NewLine + "     , H010.BAKDNTTAMT AS H010_BAKDNTTAMT"; // 2017.02.07 WOOIL
                sql += Environment.NewLine + "     , H010.BAKDNPTAMT AS H010_BAKDNPTAMT"; // 2017.02.07 WOOIL
                sql += Environment.NewLine + "     , H010.BAKDNUNAMT AS H010_BAKDNUNAMT"; // 2017.02.07 WOOIL
                sql += Environment.NewLine + "  FROM TIE_F0102 F0102 LEFT JOIN TIE_H010 H010 ON H010.DEMNO=F0102.DEMNO ";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0102.CNECTDD>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0102.CNECTDD<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0102.CNECTNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0102.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if (txtInsmm.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "    AND  F0102.YYMM='" + txtInsmm.Text.ToString() + "'";
                }
                sql += Environment.NewLine + "  ORDER  BY F0102.CNECTDD DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.H0601_CNT = MetroLib.StrHelper.ToLong(reader["H0601_CNT"].ToString().TrimEnd());
                    data.H0801_CNT = MetroLib.StrHelper.ToLong(reader["H0801_CNT"].ToString().TrimEnd());
                    data.CALLFG = reader["CALLFG"].ToString().TrimEnd();
                    data.CNECTDD = reader["CNECTDD"].ToString().TrimEnd();
                    data.JSBS = reader["JSBS"].ToString().TrimEnd();
                    data.CNECTNO = reader["CNECTNO"].ToString().TrimEnd();
                    data.JBFG = reader["JBFG"].ToString().TrimEnd();
                    data.JRDIV = reader["JRDIV"].ToString().TrimEnd();
                    data.IOFG = reader["IOFG"].ToString().TrimEnd();
                    data.JRKWAFG = reader["JRKWAFG"].ToString().TrimEnd();
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd();
                    data.SPTID = reader["SPTID"].ToString().TrimEnd();
                    data.CNECTFG = reader["CNECTFG"].ToString().TrimEnd();
                    data.DEMCNT = MetroLib.StrHelper.ToLong(reader["DEMCNT"].ToString().TrimEnd());
                    data.UNAMT = MetroLib.StrHelper.ToLong(reader["UNAMT"].ToString().TrimEnd());
                    data.YYMM = reader["YYMM"].ToString().TrimEnd();
                    data.ADDZ1 = reader["ADDZ1"].ToString().TrimEnd();
                    data.H010_DEMCNT = MetroLib.StrHelper.ToLong(reader["H010_DEMCNT"].ToString().TrimEnd());
                    data.H010_TTAMT = MetroLib.StrHelper.ToLong(reader["H010_TTAMT"].ToString().TrimEnd());
                    data.H010_PTAMT = MetroLib.StrHelper.ToLong(reader["H010_PTAMT"].ToString().TrimEnd());
                    data.H010_UNAMT = MetroLib.StrHelper.ToLong(reader["H010_UNAMT"].ToString().TrimEnd());
                    data.H010_BAKDNTTAMT = MetroLib.StrHelper.ToLong(reader["H010_BAKDNTTAMT"].ToString().TrimEnd());
                    data.H010_BAKDNPTAMT = MetroLib.StrHelper.ToLong(reader["H010_BAKDNPTAMT"].ToString().TrimEnd());
                    data.H010_BAKDNUNAMT = MetroLib.StrHelper.ToLong(reader["H010_BAKDNUNAMT"].ToString().TrimEnd());

                    string sql2 = "";
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT VERSION, FMNO, HOSID, JIWONCD, CNECTMM";
                    sql2 += Environment.NewLine + "  FROM TIE_F0101";
                    sql2 += Environment.NewLine + " WHERE CALLFG = '" + data.CALLFG + "'";
                    sql2 += Environment.NewLine + "   AND CNECTDD = '" + data.CNECTDD + "'";

                    MetroLib.SqlHelper.GetDataReader(sql2, conn, delegate(OleDbDataReader reader2)
                    {
                        data.VERSION = reader2["VERSION"].ToString().TrimEnd();
                        data.FMNO = reader2["FMNO"].ToString().TrimEnd();
                        data.HOSID = reader2["HOSID"].ToString().TrimEnd();
                        data.JIWONCD = reader2["JIWONCD"].ToString().TrimEnd();
                        if (data.CNECTMM == "")
                        {
                            data.CNECTMM = reader2["CNECTMM"].ToString().TrimEnd();
                        }
                        else
                        {
                            data.CNECTMM += "," + reader2["CNECTMM"].ToString().TrimEnd();
                        }

                        return true;
                    });



                    list.Add(data);

                    return true;
                });
            }

            string[] check_field =
            {
                "H010_BAKDNUNAMT_2",
                "H010_BAKDNUNAMT",
                "H010_BAKDNPTAMT", 
                "H010_BAKDNTTAMT",
            };
            DxLib.GridHelper.HideColumnIfZero(check_field, grdMainView);

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
            //
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

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.IsMultiSelect == false)
                {
                    // 한 셀만 선택가능
                    if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                    {
                        Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                    }
                    else
                    {
                        Clipboard.SetText("");
                        //MessageBox.Show("The value in the selected cell is null or empty!");
                    }
                }
                else
                {
                    // 여러셀을 선택하여 복사
                    StringBuilder sb = new StringBuilder();
                    for (int row_no = 0; row_no < view.RowCount; row_no++)
                    {
                        StringBuilder sbRow = new StringBuilder();
                        DevExpress.XtraGrid.Columns.GridColumn[] cols = view.GetSelectedCells(row_no);
                        if (cols == null) continue;
                        if (cols.Length>0)
                        {
                            for (int col_no = 0; col_no < cols.Length; col_no++)
                            {
                                if (col_no > 0) sbRow.Append("\t"); // 컬럼 구분자로 TAB문자
                                sbRow.Append(view.GetRowCellValue(row_no, cols[col_no].FieldName).ToString());
                            }
                            if (row_no > 0) sb.Append(Environment.NewLine); // 줄사이 구분자는 엔터문자
                            sb.Append(sbRow.ToString());
                        }
                    }
                    Clipboard.SetText(sb.ToString());
                }
                e.Handled = true;
            }
            */
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;
                string remark = grdMainView.GetRowCellValue(e.RowHandle, "CNECTMM").ToString();
                string callfg = grdMainView.GetRowCellValue(e.RowHandle, "CALLFG").ToString();
                string cnectdd = grdMainView.GetRowCellValue(e.RowHandle, "CNECTDD").ToString();
                string demno = grdMainView.GetRowCellValue(e.RowHandle, "DEMNO").ToString();
                txtMemo.Text = remark;
                QuerySub(callfg, cnectdd, demno);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QuerySub(string p_callfg, string p_cnectdd, string p_demno)
        {
            List<CDataSub> list = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT A.EPRTNO,A.RCODE,A.SEQ,A.RMEMO";
                sql += Environment.NewLine + "     , B.CDNM AS RMEMO_CDNM";
                sql += Environment.NewLine + "  FROM TIE_F0103 A LEFT JOIN TI88 B ON B.MST1CD='A' AND B.MST2CD='BULCD' AND B.MST3CD=REPLACE(A.RCODE,'-','')";
                sql += Environment.NewLine + " WHERE A.CALLFG  = '" + p_callfg + "'";
                sql += Environment.NewLine + "   AND A.CNECTDD = '" + p_cnectdd + "'";
                sql += Environment.NewLine + "   AND A.DEMNO   = '" + p_demno + "'";
                sql += Environment.NewLine + " ORDER BY A.CNECTDD,A.DEMNO";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSub data = new CDataSub();
                    data.Clear();

                    data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd();
                    data.RCODE = reader["RCODE"].ToString().TrimEnd();
                    data.SEQ = reader["SEQ"].ToString().TrimEnd();
                    data.RMEMO = reader["RMEMO"].ToString().TrimEnd();
                    data.RMEMO_CDNM = reader["RMEMO_CDNM"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(0);
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
        private int m_PrintIndex = 0;

        private void Print(int index)
        {
            m_PrintIndex = index;

            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            if (m_PrintIndex == 0)
            {
                grdMainView.Columns["CNECTMM"].Visible = false; // 출력할 때는 안나오게
                grdMainView.Columns["VERSION"].Visible = false; // 출력할 때는 안나오게

                printableComponentLink.Component = grdMain;
            }
            else if (m_PrintIndex == 1)
            {
                if (txtMemo.Text.ToString() != "") printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 150, 50);
                printableComponentLink.Component = grdSub;
            }

            printableComponentLink.ShowPreview();

            if (m_PrintIndex == 0)
            {
                grdMainView.Columns["CNECTMM"].Visible = true; // 출력 끝 다시 보이게
                grdMainView.Columns["VERSION"].Visible = true; // 출력 끝 다시 보이게
            }
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "청구명세서등접수반송증";
            if (m_PrintIndex == 0)
            {
                strTitle += "(리스트)";
            }
            else if (m_PrintIndex == 1)
            {
                strTitle += "(명단)";
            }

            // 가운데 정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);

            // 왼쪽정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Near);
            // 조회조건
            String strCaption = "";
            if (m_PrintIndex != 0)
            {
                string callfgnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CALLFGNM").ToString();
                string cnectdd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECTDD").ToString();
                string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
                string demcnt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMCNT").ToString();
                string unamt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "UNAMT").ToString();

                strCaption += "신청구분 : " + callfgnm;
                strCaption += ", 접수일자 : " + cnectdd;
                strCaption += ", 청구번호 : " + demno;
                strCaption += ", 청구건수 : " + demcnt;
                strCaption += ", 청구금액 : " + unamt;

                if (txtMemo.Text.ToString().TrimEnd() != "")
                {
                    strCaption += Environment.NewLine;
                    strCaption += "메모 : " + txtMemo.Text.ToString();
                }

                e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
                e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 100), DevExpress.XtraPrinting.BorderSide.None);
            }
            // 가운데 정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
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
            e.Graph.DrawString("ADD0711E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnPrintSub_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(1);
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

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            if (e.Column.FieldName == "DEMCNT")
            {
                long demcnt = MetroLib.StrHelper.ToLong(view.GetRowCellValue(e.RowHandle, "DEMCNT").ToString());
                long h010_demcnt = MetroLib.StrHelper.ToLong(view.GetRowCellValue(e.RowHandle, "H010_DEMCNT").ToString());

                if (demcnt != h010_demcnt)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == "UNAMT")
            {
                long unamt = MetroLib.StrHelper.ToLong(view.GetRowCellValue(e.RowHandle, "UNAMT").ToString());
                long h010_unamt = MetroLib.StrHelper.ToLong(view.GetRowCellValue(e.RowHandle, "H010_UNAMT").ToString());

                if (unamt != h010_unamt)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }

        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != grdMain) return;

            DevExpress.Utils.ToolTipControlInfo info = null;
            //Get the view at the current mouse position
            DevExpress.XtraGrid.Views.Grid.GridView view = grdMain.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            //Get the view's element information that resides at the current position
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //Display a hint for row indicator cells
            if (hi == null) return;
            if (hi.Column == null) return;
            if (hi.Column.FieldName == "CNECTFG")
            {
                if (hi.RowHandle >= 0)
                {
                    string cnectfg = view.GetRowCellValue(hi.RowHandle, "CNECTFG").ToString();
                    string cnectfgnm = "";
                    if (cnectfg == "1") cnectfgnm = "DRG";
                    else if (cnectfg == "2") cnectfgnm = "요양급여비용심사청구서";
                    else if (cnectfg == "3") cnectfgnm = "검체검사공급내역통보서";
                    else if (cnectfg == "4") cnectfgnm = "구입내역통보서";
                    else if (cnectfg == "5") cnectfgnm = "PACS";
                    else if (cnectfg == "6") cnectfgnm = "정신과정액";
                    else if (cnectfg == "7") cnectfgnm = "노숙자및외국인근로자";
                    else if (cnectfg == "8") cnectfgnm = "장기요양";

                    object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
                    info = new DevExpress.Utils.ToolTipControlInfo(o, cnectfgnm);
                }
            }
            //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
            if (info != null)
                e.Info = info;

        }

        private void btnQueryOneYear_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("0");
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

        private void btnQuerySixMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("1");
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

        private void btnQueryThreeMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("2");
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

        private void btnQueryOneMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("3");
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

        private void btnQueryOneWeek_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("4");
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

        private void btnQueryNoLimit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("5");
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
    }
}

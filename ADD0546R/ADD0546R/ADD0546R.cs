using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0546R
{
    public partial class ADD0546R : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool IsFirst;

        public ADD0546R()
        {
            InitializeComponent();
        }

        public ADD0546R(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            CreatePopupMenu();
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
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@2", m_Prjcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["MULTIFG"].ToString();
                        }
                    }
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
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("구입일자순 정렬", new EventHandler(mnuSelFst_Click));
            //grdMain.ContextMenu = cm;
        }

        private void rbIofg1_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel2();
        }

        private void rbIofg2_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel2();
        }

        private void SetLabel2()
        {
            label2.Text = rbIofg1.Checked ? "청구년월" : "청구일자";
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
            string tTI2A = "TI2A";
            string tTI2F = "TI2F";
            string tTI2H = "TI2H";
            string fBDODT = "BDODT";
            if (rbIofg1.Checked)
            {
                tTI2A = "TI1A";
                tTI2F = "TI1F";
                tTI2H = "TI1H";
                fBDODT = "EXDATE";
            }
            string fdate = txtFdate.Text.ToString();
            string tdate = txtTdate.Text.ToString();
            if (rbIofg1.Checked)
            {
                if (fdate.Length > 4) fdate = fdate.Substring(0, 4);
                if (tdate.Length > 4) tdate = tdate.Substring(0, 4);
            }

            string inQfy = "";
            if (rbQfy2.Checked)
            {
                inQfy = "'21','22','23','24','40'"; // 2017.03.06 WOOIL - 청주효성에서 24(국민공단(보훈80%)) 사용
            }
            else if (rbQfy3.Checked)
            {
                inQfy = "'31','32','33','34'";
            }
            else if (rbQfy5.Checked)
            {
                inQfy = "'50'";
            }
            else if (rbQfy51.Checked)
            {
                inQfy = "'51','52','53','54','55','56','57','58','59'";
            }
            else if (rbQfy6.Checked)
            {
                inQfy = "'61','62','63','64','65','66','67','68','69'";// 2009.03.02 WOOIL - 62~69추가
            }
            else if (rbQfy38.Checked)
            {
                inQfy = "'38','39'";
            }
            else if (rbQfy29.Checked)
            {
                inQfy = "'29'";
            }

            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            grdPtnt.DataSource = null;
            List<CDataPtnt> listPtnt = new List<CDataPtnt>();
            grdPtnt.DataSource = listPtnt;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT A.PID, A.PNM, F.BGIHO, F.PRICD, F.PRKNM, ISNULL(A.ADDZ1,'') AS ADDZ1, SUM(F.DDAY*F.DQTY) AS TQTY, SUM(F.GUMAK) AS GUMAK ";
                sql += Environment.NewLine + "  FROM " + tTI2A + " A INNER JOIN " + tTI2F + " F ON F." + fBDODT + "=A." + fBDODT + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS";
                sql += Environment.NewLine + "              INNER JOIN TA88 A88 ON A88.MST1CD='A' AND A88.MST2CD='2' AND A88.MST3CD=F.ACTFG";
                sql += Environment.NewLine + " WHERE A." + fBDODT + " >= '" + fdate + "' ";
                sql += Environment.NewLine + "   AND A." + fBDODT + " <= '" + tdate + "' ";
                sql += Environment.NewLine + "   AND A.QFYCD IN (" + inQfy + ") ";
                sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                sql += Environment.NewLine + "   AND ISNULL(A.DONFG,'Y')='Y' ";
                sql += Environment.NewLine + "   AND F.MAFG='1' ";
                sql += Environment.NewLine + "   AND SUBSTRING(A88.FLD3CD,1,2) IN ('08','10')";
                if (chkSingo.Checked)
                {
                    sql += Environment.NewLine + "   AND NOT EXISTS ( SELECT *";
                    sql += Environment.NewLine + "                      FROM TIE_H0602 H0602";
                    sql += Environment.NewLine + "                     WHERE H0602.ITEMCD = F.BGIHO";
                    sql += Environment.NewLine + "                       AND H0602.BUYDT  > CONVERT(VARCHAR,DATEADD(YY,-2,CONVERT(DATETIME,LEFT(F.EXDT,8))),112)";
                    sql += Environment.NewLine + "                  )";
                }
                sql += Environment.NewLine + " GROUP BY A.PID, A.PNM, F.BGIHO, F.PRICD,F.PRKNM, ISNULL(A.ADDZ1,'') ";
                sql += Environment.NewLine + " UNION ALL ";
                sql += Environment.NewLine + "SELECT A.PID, A.PNM, F.BGIHO, F.PRICD, F.PRKNM, ISNULL(A.ADDZ1,'') AS ADDZ1, SUM(F.DDAY*F.DQTY) AS TQTY, SUM(F.GUMAK) AS GUMAK ";
                sql += Environment.NewLine + "  FROM " + tTI2A + " A INNER JOIN " + tTI2H + " F ON F." + fBDODT + "=A." + fBDODT + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS";
                sql += Environment.NewLine + "              INNER JOIN TA88 A88 ON A88.MST1CD='A' AND A88.MST2CD='2' AND A88.MST3CD=F.ACTFG";
                sql += Environment.NewLine + " WHERE A." + fBDODT + " >= '" + fdate + "' ";
                sql += Environment.NewLine + "   AND A." + fBDODT + " <= '" + tdate + "' ";
                sql += Environment.NewLine + "   AND A.QFYCD IN (" + inQfy + ") ";
                sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                sql += Environment.NewLine + "   AND ISNULL(A.DONFG,'Y')='Y' ";
                sql += Environment.NewLine + "   AND F.AFPFG='1' ";
                sql += Environment.NewLine + "   AND F.MAFG='1' ";
                sql += Environment.NewLine + "   AND SUBSTRING(A88.FLD3CD,1,2) IN ('08','10')";
                if (chkSingo.Checked)
                {
                    sql += Environment.NewLine + "   AND NOT EXISTS ( SELECT *";
                    sql += Environment.NewLine + "                      FROM TIE_H0602 H0602";
                    sql += Environment.NewLine + "                     WHERE H0602.ITEMCD = F.BGIHO";
                    sql += Environment.NewLine + "                       AND H0602.BUYDT  > CONVERT(VARCHAR,DATEADD(YY,-2,CONVERT(DATETIME,LEFT(F.EXDT,8))),112)";
                    sql += Environment.NewLine + "                  )";
                }
                sql += Environment.NewLine + " GROUP BY A.PID, A.PNM, F.BGIHO, F.PRICD,F.PRKNM, ISNULL(A.ADDZ1,'') ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string addz1 = reader["ADDZ1"].ToString();

                    bool bOk = false;
                    if (chkDemfg0.Checked && (addz1 == "" || addz1 == "0" || addz1 == "3")) bOk = true; // 원청구(분리청구 포함)
                    if (chkDemfg1.Checked && addz1 == "1") bOk = true; // 보완청구
                    if (chkDemfg2.Checked && addz1 == "2") bOk = true; // 추가청구

                    if (bOk == false) return MetroLib.SqlHelper.CONTINUE;

                    string pid = reader["PID"].ToString();
                    string pnm = reader["PNM"].ToString();
                    string bgiho = reader["BGIHO"].ToString();
                    string pricd = reader["PRICD"].ToString();
                    string prknm = reader["PRKNM"].ToString();
                    double tqty = MetroLib.StrHelper.ToDouble(reader["TQTY"].ToString());
                    long gumak = MetroLib.StrHelper.ToLong(reader["GUMAK"].ToString());


                    // 환자별
                    bool bFind = false;
                    foreach (CDataPtnt ptnt in listPtnt)
                    {
                        if (ptnt.PID == pid && ptnt.PNM == pnm && ptnt.BGIHO == bgiho && ptnt.PRICD == pricd && ptnt.PRKNM == prknm)
                        {
                            ptnt.TQTY += tqty;
                            ptnt.GUMAK += gumak;
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == false)
                    {
                        CDataPtnt ptnt = new CDataPtnt();
                        ptnt.PID = pid;
                        ptnt.PNM = pnm;
                        ptnt.BGIHO = bgiho;
                        ptnt.PRICD = pricd;
                        ptnt.PRKNM = prknm;
                        ptnt.TQTY = tqty;
                        ptnt.GUMAK = gumak;
                        listPtnt.Add(ptnt);
                    }

                    // 코드별
                    bFind = false;
                    foreach (CData data in list)
                    {
                        if (data.BGIHO==bgiho && data.PRICD == pricd && data.PRKNM == prknm)
                        {
                            data.TQTY += tqty;
                            data.GUMAK += gumak;
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == false)
                    {
                        CData data = new CData();
                        data.BGIHO = bgiho;
                        data.PRICD = pricd;
                        data.PRKNM = prknm;
                        data.TQTY = tqty;
                        data.GUMAK = gumak;
                        list.Add(data);
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });

            }

            RefreshGridMain();
            RefreshGridPtnt();
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

        private void ADD0546R_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0546R_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            txtFdate.Focus();
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
            grdMainView.OptionsPrint.AutoWidth = false; // 이 값이 true이면 출력시 column의 폭을 자동으로 조절하여 한 페이지에 출력되게 한다.
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string fdate = txtFdate.Text.ToString();
            string tdate = txtTdate.Text.ToString();
            if (rbIofg1.Checked)
            {
                if (fdate.Length > 4) fdate = fdate.Substring(0, 4);
                if (tdate.Length > 4) tdate = tdate.Substring(0, 4);
            }

            string title = "재료대사용현황";

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string msg = rbIofg1.Checked ? "외래" : "입원";
            msg += " ";
            msg += fdate + " - " + tdate;
            // 조회조건 출력
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(msg, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string prtDate = "";
            string prtTime = "";
            GetDateTime(out prtDate, out prtTime);

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD00546R", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(prtDate + " " + prtTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void GetDateTime(out string out_date, out string out_time)
        {
            out_date = "";
            out_time = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    out_date = MetroLib.Util.GetSysDate(conn);
                    out_time = MetroLib.Util.GetSysTime(conn);

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                out_date = DateTime.Now.ToString("yyyyMMdd");
                out_time = DateTime.Now.ToString("HHmmss");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0545R
{
    public partial class ADD0545R : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool IsFirst;

        public ADD0545R()
        {
            InitializeComponent();
        }

        public ADD0545R(String user, String pwd, String prjcd, String addpara)
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
            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;
            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string objdiv = rbObjdivA.Checked ? "A" : "B";

                string sql = "";
                sql = "";
                sql = sql + Environment.NewLine + "SELECT DOCUNO";
                sql = sql + Environment.NewLine + "     , OBJCOUNT";
                sql = sql + Environment.NewLine + "     , OBJAMT1";
                sql = sql + Environment.NewLine + "     , OBJAMT2";
                sql = sql + Environment.NewLine + "     , PRTDT";
                sql = sql + Environment.NewLine + "     , DEMSEQ";
                sql = sql + Environment.NewLine + "     , CNECNO";
                sql = sql + Environment.NewLine + "     , DEMNO";
                sql = sql + Environment.NewLine + "  FROM TI32 ";
                sql = sql + Environment.NewLine + " WHERE OBJDIV='" + objdiv + "' ";
                sql = sql + Environment.NewLine + " ORDER BY PRTDT DESC,DOCUNO DESC ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.DOCUNO = reader["DOCUNO"].ToString();
                    data.OBJCOUNT = MetroLib.StrHelper.ToLong(reader["OBJCOUNT"].ToString());
                    data.OBJAMT1 = MetroLib.StrHelper.ToLong(reader["OBJAMT1"].ToString());
                    data.OBJAMT2 = MetroLib.StrHelper.ToLong(reader["OBJAMT2"].ToString());
                    data.PRTDT = reader["PRTDT"].ToString();
                    data.DEMSEQ = reader["DEMSEQ"].ToString();
                    data.CNECNO = reader["CNECNO"].ToString();
                    data.DEMNO = reader["DEMNO"].ToString();

                    list.Add(data);

                    return MetroLib.SqlHelper.CONTINUE;
                });

            }

            RefreshGridMain();
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

        private void rbObjdivA_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel();
        }

        private void rbObjdivB_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel();
        }

        private void SetLabel()
        {
            if (rbObjdivA.Checked)
            {
                grdMainView.Columns["OBJCOUNT"].Caption = "재심청구건수";
                grdMainView.Columns["OBJAMT1"].Caption = "재심청구금액1항";
                grdMainView.Columns["OBJAMT2"].Caption = "재심청구금액2항";
                grdMainView.Columns["OBJAMTTOT"].Caption = "재심청구총금액";
            }
            else
            {
                grdMainView.Columns["OBJCOUNT"].Caption = "이의신청건수";
                grdMainView.Columns["OBJAMT1"].Caption = "이의신청금액1항";
                grdMainView.Columns["OBJAMT2"].Caption = "이의신청금액2항";
                grdMainView.Columns["OBJAMTTOT"].Caption = "이의신청총금액";
            }
        }

        private void ADD0545R_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0545R_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            SetLabel();

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

            string title = rbObjdivA.Checked ? "재심청구건수및금액" : "이의신청건수및금액";

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string msg = "";
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
            e.Graph.DrawString("ADD00545R", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
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

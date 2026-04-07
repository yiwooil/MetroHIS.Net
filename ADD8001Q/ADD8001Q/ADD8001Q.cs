using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD8001Q
{
    public partial class ADD8001Q : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private String m_SysDate;
        private String m_SysTime;

        public ADD8001Q()
        {
            InitializeComponent();
        }

        public ADD8001Q(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "자료검색 중입니다.");
                this.Query();
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            String strFrdt = txtFrdt.Text.ToString();
            String strTodt = txtTodt.Text.ToString();
            if (strTodt == "" && strFrdt != "") strTodt = strFrdt;
            else if (strTodt != "" && strFrdt == "") strFrdt = strTodt;

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT I010.ACCDIV";
            sql += System.Environment.NewLine + "     , I010.DEMNO";
            sql += System.Environment.NewLine + "     , I010.ACCBACKDIV";
            sql += System.Environment.NewLine + "     , I010.FMNO";
            sql += System.Environment.NewLine + "     , I010.HOSID";
            sql += System.Environment.NewLine + "     , I010.REPDT";
            sql += System.Environment.NewLine + "     , I010.ACCNO";
            sql += System.Environment.NewLine + "     , I010.DEMCNT";
            sql += System.Environment.NewLine + "     , I010.DEMAMT";
            sql += System.Environment.NewLine + "     , M010.YYMM";
            sql += System.Environment.NewLine + "     , M010.BUSSDIV";
            sql += System.Environment.NewLine + "     , I010.RSNCD";
            sql += System.Environment.NewLine + "     , REPLACE(I010.MEMO,CHAR(13)+CHAR(10),'/') MEMO";
            sql += System.Environment.NewLine + "     , CASE WHEN Z.CNT>0 THEN 'Y' ELSE '' END ISI020";
            sql += System.Environment.NewLine + "     , CASE WHEN Y.CNT>0 THEN 'Y' ELSE '' END ISI030";
            sql += System.Environment.NewLine + "     , M010.DEMGB";
            sql += System.Environment.NewLine + "  FROM TIE_I010 I010 INNER JOIN TIE_M010 M010 ON M010.DEMNO = I010.DEMNO        ";
            sql += System.Environment.NewLine + "                     LEFT  JOIN (                                               ";
            sql += System.Environment.NewLine + "                                 SELECT I020.ACCNO,COUNT(*) CNT                 ";
            sql += System.Environment.NewLine + "                                   FROM TIE_I020 I020                           ";
            sql += System.Environment.NewLine + "                                  GROUP BY I020.ACCNO                           ";
            sql += System.Environment.NewLine + "                                ) Z ON Z.ACCNO = I010.ACCNO                     ";
            sql += System.Environment.NewLine + "                     LEFT  JOIN (                                               ";
            sql += System.Environment.NewLine + "                                 SELECT I0301.ACCNO,COUNT(*) CNT                ";
            sql += System.Environment.NewLine + "                                   FROM TIE_I0301 I0301                         ";
            sql += System.Environment.NewLine + "                                  GROUP BY I0301.ACCNO                          ";
            sql += System.Environment.NewLine + "                                ) Y ON Y.ACCNO = I010.ACCNO                     ";
            if (strFrdt != "" && strTodt != "")
            {
                sql += System.Environment.NewLine + " WHERE I010.REPDT BETWEEN '" + strFrdt + "' AND '" + strTodt + "' ";
            }
            sql += System.Environment.NewLine + " ORDER BY I010.REPDT DESC,I010.DEMNO DESC,I010.ACCNO                            ";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_SysDate = MetroLib.Util.GetSysDate(conn);
                m_SysTime = MetroLib.Util.GetSysTime(conn);

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.ACCDIV = reader["ACCDIV"].ToString().Trim();
                        data.DEMNO = reader["DEMNO"].ToString().Trim();
                        data.ACCBACKDIV = reader["ACCBACKDIV"].ToString().Trim();
                        data.FMNO = reader["FMNO"].ToString().Trim();
                        data.HOSID = reader["HOSID"].ToString().Trim();
                        data.REPDT = reader["REPDT"].ToString().Trim();
                        data.ACCNO = reader["ACCNO"].ToString().Trim();
                        data.DEMCNT = reader["DEMCNT"].ToString().Trim();
                        data.DEMAMT = reader["DEMAMT"].ToString().Trim();
                        data.YYMM = reader["YYMM"].ToString().Trim();
                        data.BUSSDIV = reader["BUSSDIV"].ToString().Trim();
                        data.RSNCD = reader["RSNCD"].ToString().Trim();
                        data.MEMO = reader["MEMO"].ToString().Trim();
                        data.ISI020 = reader["ISI020"].ToString().Trim();
                        data.ISI030 = reader["ISI030"].ToString().Trim();
                        data.DEMGB = reader["DEMGB"].ToString().Trim();

                        list.Add(data);

                    }
                    reader.Close();
                }

                conn.Close();
            }

            this.RefreshGridMain();
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

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "ISI020")
            {
                String value = e.CellValue.ToString();
                if (value == "Y")
                {
                    // 2021.05.21 WOOIL - 사용자가 컬럼 제목을 클릭하여 정렬한 경우 DataSource의 인덱스는 변경되지 않아 엉뚱한 값을 가져온다.
                    //                    그래서, GetRowCellValue 함수를 사용한다.
                    //CData data = ((List<CData>)grdMain.DataSource)[e.RowHandle];
                    //String para = "accno=" + data.ACCNO + ",cntno=1";
                    String accno = grdMainView.GetRowCellValue(e.RowHandle, gcACCNO).ToString();
                    String para = "accno=" + accno + ",cntno=1";
                    MetroLib.DLLHelper.LoadDLL("ADD8002Q", "산재보험진료비지불결정통지서", para, true);

                }
            }
            else if (e.Column.FieldName == "ISI030")
            {
                String value = e.CellValue.ToString();
                if (value == "Y")
                {
                    // 2021.05.21 WOOIL - 사용자가 컬럼 제목을 클릭하여 정렬한 경우 DataSource의 인덱스는 변경되지 않아 엉뚱한 값을 가져온다.
                    //                    그래서, GetRowCellValue 함수를 사용한다.
                    //CData data = ((List<CData>)grdMain.DataSource)[e.RowHandle];
                    //String para = "accno=" + data.ACCNO + ",cntno=1";
                    String accno = grdMainView.GetRowCellValue(e.RowHandle, gcACCNO).ToString();
                    String para = "accno=" + accno + ",cntno=1";
                    MetroLib.DLLHelper.LoadDLL("ADD8003Q", "산재보험진료비심사내역통보서", para, true);
                    
                }
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
            String strTitle = "산재보험접수반송증";
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strFrdt = txtFrdt.Text.ToString();
            String strTodt = txtTodt.Text.ToString();
            if (strTodt == "" && strFrdt != "") strTodt = strFrdt;
            else if (strTodt != "" && strFrdt == "") strFrdt = strTodt;
            String strCaption = "";
            strCaption += "통지일자 : " + strFrdt + " - " + strTodt;
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD8001Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdMainView_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = grdMainView.CalcHitInfo(args.Location);
            if (args.Button == MouseButtons.Left && hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton && hitInfo.Column == null)
            {
                grdMainView.SelectAll();
            }
        }
    }
}

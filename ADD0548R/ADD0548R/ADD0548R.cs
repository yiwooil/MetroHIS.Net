using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0548R
{
    public partial class ADD0548R : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private string m_ErrPos = "";

        public ADD0548R()
        {
            InitializeComponent();
        }
        public ADD0548R(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0548R_Load(object sender, EventArgs e)
        {
            cboDemgb.SelectedIndex = 1;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysDate = MetroLib.Util.GetSysDate(conn);
                DateTime dtSys = DateTime.ParseExact(sysDate, "yyyyMMdd", null);

                txtYYMMfr.Text = dtSys.AddDays(-1).ToString("yyyyMMdd").Substring(0, 6);
                txtYYMMto.Text = txtYYMMfr.Text.ToString();
            }
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
            string tTI2A = "";
            string tTI2F = "";
            string fBDODT = "";
            if (rbIofg2.Checked)
            {
                tTI2A = "TI2A";
                tTI2F = "TI2F";
                fBDODT = "BDODT";
            }
            else
            {
                tTI2A = "TI1A";
                tTI2F = "TI1F";
                fBDODT = "EXDATE";
            }

            string frdt = txtYYMMfr.Text.ToString();
            string todt = txtYYMMto.Text.ToString();
            if (rbIofg1.Checked)
            {
                frdt = frdt.Substring(0, 4);
                todt = todt.Substring(0, 4);
            }

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT I2A.EPRTNO, A01.PNM, A01.RESID, A88.CDNM AS REFNM, LEFT(I2F.REFCD,8) AS REFCD, I2F.BGIHO, I2F.PRKNM, I2F.DQTY, I2F.DDAY";
                sql += Environment.NewLine + "  FROM " + tTI2A + " I2A INNER JOIN " + tTI2F + " I2F ON I2F." + fBDODT + "=I2A." + fBDODT + " AND I2F.QFYCD=I2A.QFYCD AND I2F.JRBY=I2A.JRBY AND I2F.PID=I2A.PID AND I2F.UNISQ=I2A.UNISQ AND I2F.SIMCS=I2A.SIMCS";
                sql += Environment.NewLine + "                         INNER JOIN TA01 A01 ON A01.PID=I2A.PID";
                sql += Environment.NewLine + "                         INNER JOIN TA88 A88 ON A88.MST1CD='A' AND A88.MST2CD='SUTAK' AND A88.MST3CD=LEFT(I2F.REFCD,8)";
                sql += Environment.NewLine + " WHERE 1=1";
                sql += Environment.NewLine + "   AND I2A." + fBDODT + ">='" + frdt + "'";
                sql += Environment.NewLine + "   AND I2A." + fBDODT + "<='" + todt + "'";
                if (rbQfy2.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('21','22','23','24','25','40')";
                }
                else if (rbQfy3.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('31','32','33','34')";
                }
                else if (rbQfy5.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('50')";
                }
                else if (rbQfy6.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('61','62','63','64','65','66','67','68','69')";
                }
                else if (rbQfy38.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('38','39')";
                }
                if (rbQfy29.Checked)
                {
                    sql += Environment.NewLine + "   AND I2A.QFYCD IN ('29')";
                }

                // 2008.03.31 WOOIL - 청구구분별 조회
                if (cboDemgb.SelectedIndex == 1) {
                    // 원청구 = 원청구+분리청구
                    sql += Environment.NewLine + "   AND I2A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(I2A.ADDZ1,'') IN ('','0','3') ";
                }
                else if (cboDemgb.SelectedIndex == 2)
                {
                    // 보완청구
                    sql += Environment.NewLine + "   AND I2A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(I2A.ADDZ1,'') IN ('1') ";
                }
                else if (cboDemgb.SelectedIndex == 3)
                {
                    // 추가청구
                    sql += Environment.NewLine + "   AND I2A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(I2A.ADDZ1,'') IN ('2') ";
                }
                else if (cboDemgb.SelectedIndex == 4)
                {
                    // 약제상한추가청구
                    sql += Environment.NewLine + "   AND I2A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(I2A.ADDZ1,'') IN ('8') ";
                }
                else
                {
                    // 전체조회
                    sql += Environment.NewLine + "   AND I2A.SIMCS>0 ";
                }
                sql += Environment.NewLine + "   AND I2A.DONFG='Y' ";
                sql += Environment.NewLine + "   AND I2F.ACTFG='9B' ";
                sql += Environment.NewLine + " ORDER BY I2A.JRKWA,I2A.EPRTNO ";

                long no = 0;
                string bkResid = "";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.EPRTNO = reader["EPRTNO"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.RESID = reader["RESID"].ToString();
                    data.REFNM = reader["REFNM"].ToString();
                    data.REFCD = reader["REFCD"].ToString();
                    data.BGIHO = reader["BGIHO"].ToString();
                    data.PRKNM = reader["PRKNM"].ToString();
                    data.DQTY = MetroLib.StrHelper.ToDouble(reader["DQTY"].ToString());
                    data.DDAY = MetroLib.StrHelper.ToLong(reader["DDAY"].ToString());

                    if (bkResid == "")
                    {
                        data.NO = (++no).ToString();
                        bkResid = data.RESID;
                    }
                    else if (bkResid != data.RESID)
                    {
                        data.NO = (++no).ToString();
                        bkResid = data.RESID;
                    }

                    list.Add(data);

                    return true;
                });

            }
            RefreshGridMain();
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

        private void rbIofg1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtYYMMfr.Text.ToString().Length > 6)
            {
                txtYYMMfr.Text = txtYYMMfr.Text.ToString().Substring(0, 6);
            }
            if (txtYYMMto.Text.ToString().Length > 6)
            {
                txtYYMMto.Text = txtYYMMto.Text.ToString().Substring(0, 6);
            }
        }

        private void rbIofg2_CheckedChanged(object sender, EventArgs e)
        {
            if (txtYYMMfr.Text.ToString().Length == 6)
            {
                DateTime dtSys = DateTime.ParseExact(txtYYMMfr.Text.ToString() + "01", "yyyyMMdd", null);
                txtYYMMfr.Text = dtSys.ToString("yyyyMMdd").Substring(0, 6) + "01"; // 1일
            }
            if (txtYYMMto.Text.ToString().Length == 6)
            {
                DateTime dtSys = DateTime.ParseExact(txtYYMMto.Text.ToString() + "01", "yyyyMMdd", null);
                txtYYMMto.Text = dtSys.AddMonths(1).AddDays(-1).ToString("yyyyMMdd"); // 마지막 날
            }
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
            String strTitle = "수탁환자명단";

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
            String strCaption = GetCaption();
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 100), DevExpress.XtraPrinting.BorderSide.None);

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
            e.Graph.DrawString("ADD0548R", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private string GetCaption()
        {
            String strCaption = "";
            strCaption += "청구년월 : " + txtYYMMfr.Text.ToString() + " - " + txtYYMMto.Text.ToString();
            if (rbIofg1.Checked)
            {
                strCaption += ", 외래";
            }
            else
            {
                strCaption += ", 입원";
            }

            if (rbQfy2.Checked)
            {
                strCaption += ", 보험";
            }
            else if (rbQfy3.Checked)
            {
                strCaption += ", 보호";
            }
            else if (rbQfy5.Checked)
            {
                strCaption += ", 산재";
            }
            else if (rbQfy6.Checked)
            {
                strCaption += ", 자보";
            }
            else if (rbQfy38.Checked)
            {
                strCaption += ", 보호정신과";
            }
            if (rbQfy29.Checked)
            {
                strCaption += ", 보훈일반";
            }

            if (cboDemgb.SelectedIndex == 1)
            {
                strCaption += ", 원청구";
            }
            else if (cboDemgb.SelectedIndex == 2)
            {
                strCaption += ", 보완청구";
            }
            else if (cboDemgb.SelectedIndex == 3)
            {
                strCaption += ", 추가청구";
            }
            else if (cboDemgb.SelectedIndex == 4)
            {
                strCaption += ", 약제상한추가";
            }
            else
            {
                strCaption += ", 전체";
            }

            return strCaption;
            
        }
    }
}

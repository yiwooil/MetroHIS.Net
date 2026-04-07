using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7004E
{
    public partial class ADD7004E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;

        public ADD7004E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
        }

        public ADD7004E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
        }

        private void ADD7004E_Load(object sender, EventArgs e)
        {

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

        private void Query()
        {

            List<CAMT> list = new List<CAMT>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string reqmm = txtReqmm.Text.ToString();
            string pid = txtPid.Text.ToString();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.PDRID, A.GSRT, F.SEQ1, F.POS2, F.MAFG, F.GUMAK, A07.DRNM";
            sql += Environment.NewLine + "  FROM TI2A A INNER JOIN TI2F F ON F.BDODT=A.BDODT AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS";
            sql += Environment.NewLine + "              LEFT JOIN TA07 A07 ON A07.DRID=A.PDRID";
            sql += Environment.NewLine + " WHERE A.BDODT LIKE '" + reqmm + "%'";
            sql += Environment.NewLine + "   AND A.DONFG='Y'";
            if (pid != "")
            {
                sql += Environment.NewLine + "   AND A.PID='" + pid + "'";
            }
            if (rbPacare.Checked)
            {
                sql += Environment.NewLine + "   AND ISNULL(A.PACAREFG,'')='1'";
            }
            else if (rbYoyang.Checked)
            {
                sql += Environment.NewLine + "   AND ISNULL(A.YOFG,'')='1'";
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    float gsrt = 0;
                    int seq1 = 0;
                    int pos2 = 0;
                    int gumak = 0;

                    string mafg = reader["MAFG"].ToString();

                    float.TryParse(reader["GSRT"].ToString(), out gsrt);
                    int.TryParse(reader["SEQ1"].ToString(), out seq1);
                    int.TryParse(reader["POS2"].ToString(), out pos2);
                    int.TryParse(reader["GUMAK"].ToString(), out gumak);

                    string pdrid = reader["PDRID"].ToString();
                    string pdrnm = reader["DRNM"].ToString();

                    int gumak1 = 0;
                    int gumak2 = 0;
                    int gs_gum = 0;

                    if (mafg == "1")
                    {
                        gumak1 = gumak;
                    }
                    else
                    {
                        gumak2 = gumak;
                        gs_gum = (int)(gumak * (gsrt / 100));
                    }

                    // 정액진료비
                    if ( seq1!=22 || pos2 < 81)
                    {
                        SetJA(list, pdrid, pdrnm, 0, 0, 0, 0); // 의사명을 출력하기 위함.
                        SetJA(list, pdrid, pdrnm, seq1, gumak1, gumak2, gs_gum);
                        SetJA(list, pdrid, pdrnm, 99, gumak1, gumak2, gs_gum);
                        SetJA(list, "zzzzzz", "총계", 0, 0, 0, 0);
                        SetJA(list, "zzzzzz", "총계", seq1, gumak1, gumak2, gs_gum);
                        SetJA(list, "zzzzzz", "총계", 99, gumak1, gumak2, gs_gum);
                    }

                    // 행위별 진료비
                    if (seq1 != 22 || pos2 >= 81)
                    {
                        if (seq1 == 22)
                        {
                            if (pos2 == 81) seq1 = 11;
                            else if (pos2 == 82) seq1 = 12;
                            else if (pos2 == 83) seq1 = 13;
                            else if (pos2 == 84) seq1 = 14;
                            else if (pos2 == 85) seq1 = 15;
                            else if (pos2 == 86) seq1 = 16;
                            else if (pos2 == 87) seq1 = 17;
                            else if (pos2 == 88) seq1 = 18;
                            else if (pos2 == 89) seq1 = 19;
                            else if (pos2 == 90) seq1 = 20;
                            else if (pos2 == 91) seq1 = 20;
                            else if (pos2 == 92) seq1 = 31; // 100/100
                            else if (pos2 == 93) seq1 = 32; // 비급여
                            else if (pos2 == 94) seq1 = 25; // 백미만
                        }

                        SetJR(list, pdrid, pdrnm, 0, 0, 0, 0); // 의사명을 출력하기 위함.
                        SetJR(list, pdrid, pdrnm, seq1, gumak1, gumak2, gs_gum);
                        SetJR(list, pdrid, pdrnm, 99, gumak1, gumak2, gs_gum);
                        SetJR(list, "zzzzzz", "총계", 0, 0, 0, 0);
                        SetJR(list, "zzzzzz", "총계", seq1, gumak1, gumak2, gs_gum);
                        SetJR(list, "zzzzzz", "총계", 99, gumak1, gumak2, gs_gum);
                    }


                    return true;
                });
            }

            // 합계
            /*
            CAMT amt99 = new CAMT();
            amt99.SEQ1 = 99;
            foreach (CAMT a_amt in list)
            {
                amt99.JA_GUMAK1 += a_amt.JA_GUMAK1;
                amt99.JA_GUMAK2 += a_amt.JA_GUMAK2; 
                amt99.JA_GS_GUM += a_amt.JA_GS_GUM;
                amt99.JR_GUMAK1 += a_amt.JR_GUMAK1;
                amt99.JR_GUMAK2 += a_amt.JR_GUMAK2;
                amt99.JR_GS_GUM += a_amt.JR_GS_GUM;
            }
            list.Add(amt99);
            */

            // PDRID + SEQ1으로 정렬
            list.Sort(delegate(CAMT x, CAMT y)
            {
                string xKey = x.PDRID + x.SEQ1.ToString();
                string yKey = y.PDRID + y.SEQ1.ToString();

                return xKey.CompareTo(yKey);
            });

            RefreshGridMain();

        }

        private void SetJA(List<CAMT> p_list, string p_pdrid, string p_pdrnm, int p_seq1, int p_gumak1, int p_gumak2, int p_gs_gum)
        {
            bool find = false;
            foreach (CAMT a_amt in p_list)
            {
                if (a_amt.SEQ1 == p_seq1 && a_amt.PDRID == p_pdrid)
                {
                    a_amt.JA_GUMAK1 += p_gumak1;
                    a_amt.JA_GUMAK2 += p_gumak2;
                    a_amt.JA_GS_GUM += p_gs_gum;
                    find = true;
                    break;
                }
            }
            // 리스트에 없으면 새로 만든다.
            if (find == false)
            {
                CAMT amt = new CAMT();
                amt.Clear();
                amt.PDRID = p_pdrid;
                amt.PDRNM = p_pdrnm;
                amt.SEQ1 = p_seq1;
                amt.JA_GUMAK1 = p_gumak1;
                amt.JA_GUMAK2 = p_gumak2;
                amt.JA_GS_GUM = p_gs_gum;
                if (rbPacare.Checked) amt.JA_NM = "완화의료";
                else if (rbYoyang.Checked) amt.JA_NM = "요양환자";
                p_list.Add(amt);
            }
        }

        private void SetJR(List<CAMT> p_list, string p_pdrid, string p_pdrnm, int p_seq1, int p_gumak1, int p_gumak2, int p_gs_gum)
        {
            bool find = false;
            foreach (CAMT a_amt in p_list)
            {
                if (a_amt.SEQ1 == p_seq1 && a_amt.PDRID == p_pdrid)
                {
                    a_amt.JR_GUMAK1 += p_gumak1;
                    a_amt.JR_GUMAK2 += p_gumak2;
                    a_amt.JR_GS_GUM += p_gs_gum;
                    find = true;
                    break;
                }
            }
            // 리스트에 없으면 새로 만든다.
            if (find == false)
            {
                CAMT amt = new CAMT();
                amt.Clear();
                amt.PDRID = p_pdrid;
                amt.PDRNM = p_pdrnm;
                amt.SEQ1 = p_seq1;
                amt.JR_GUMAK1 = p_gumak1;
                amt.JR_GUMAK2 = p_gumak2;
                amt.JR_GS_GUM = p_gs_gum;
                if (rbPacare.Checked) amt.JA_NM = "완화의료";
                else if (rbYoyang.Checked) amt.JA_NM = "요양환자";
                p_list.Add(amt);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "정액환자 진료비분석";
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    grdMain.ExportToXlsx(filePath);

                    if (MessageBox.Show("파일을 열까요?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
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
            printableComponentLink.Component = grdMain;
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string ja_nm = "";
            if (rbPacare.Checked) ja_nm = "  (완화의료)";
            else if (rbYoyang.Checked) ja_nm = "  (요양환자)";

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("정액환자 진료비분석", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("퇴원월:" + txtReqmm.Text.ToString() + ja_nm, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
            e.Graph.DrawString("ADD7004E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
        }

    }
}

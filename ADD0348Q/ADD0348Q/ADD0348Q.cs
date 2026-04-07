using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0348Q
{
    public partial class ADD0348Q : Form
    {
        private String m_User;
        private String m_Pwd;

        private String m_SysDate;
        private String m_SysTime;

        public ADD0348Q()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
        }

        public ADD0348Q(String user, String pwd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;

            this.CreatePopupMenu();
        }

        private void CreatePopupMenu()
        {
            //
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("엑셀로 저장", new EventHandler(mnuSaveExcel_Click));
            //grdMain.ContextMenu = cm;
        }

        //private void mnuSaveExcel_Click(object sender, EventArgs e)
        //{
        //}

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
            String strFrdt = txtFrDt.Text.ToString();
            String strTodt = txtToDt.Text.ToString();
            String strPid = txtPid.Text.ToString();
            String strQfycd = cboQfycd.SelectedItem.ToString();
            if (strQfycd != "")
            {
                strQfycd = strQfycd.Split('.')[0];
            }
            Boolean bOpt1 = chkOpt1.Checked;
            Boolean bDcSkip = chkDcSkip.Checked;


            List<CData> list = new List<CData>();
            List<CData> list2 = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT a04.PID, a01.PNM, a04.BEDEDT, a04.QLFYCD, a04.BEDODT, s41.EXDT, s41.QFYCD AS S41QFYCD, s41.OUTSEQ, S41.DRID AS S41DRID, A07.DRNM AS S41DRNM,";
            sql += System.Environment.NewLine + "       s31.PRICD, a02.ISPCD, a02.PRKNM, s31.UTAMT, s31.CALQY, s31.DDAY, s31.TTAMT, '0' AS ORDCNT,";
            sql += System.Environment.NewLine + "       s31.KYSTR,s31.FRFG,s31.DPTCD,s31.BDIV ";
            sql += System.Environment.NewLine + "  FROM TA04 a04 INNER JOIN TA01 a01 ON a01.PID = a04.PID";
            sql += System.Environment.NewLine + "                INNER JOIN TS41 s41 ON s41.PID = a04.PID ";
            sql += System.Environment.NewLine + "                                   AND s41.EXDT >= a04.BEDEDT ";
            sql += System.Environment.NewLine + "                                   AND s41.EXDT <= a04.BEDODT ";
            sql += System.Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID = S41.DRID";
            sql += System.Environment.NewLine + "                INNER JOIN TS31 s31 ON s31.PID = s41.PID ";
            sql += System.Environment.NewLine + "                                   AND s31.ENTDT = s41.ENTDT ";
            sql += System.Environment.NewLine + "                                   AND s31.BDIV = s41.BDIV ";
            sql += System.Environment.NewLine + "                                   AND s31.GRPNO = s41.GRPNO ";
            sql += System.Environment.NewLine + "                INNER JOIN TA02 a02 ON a02.PRICD = s31.PRICD ";
            sql += System.Environment.NewLine + "                                   AND a02.CREDT = (SELECT MAX(CREDT) FROM TA02 X WHERE X.PRICD = s31.PRICD AND X.CREDT <= s31.EXDT)";
            sql += System.Environment.NewLine + " WHERE a04.PID NOT LIKE 'T%' ";
            sql += System.Environment.NewLine + "   AND a04.BEDODT >= '" + strFrdt + "' ";
            sql += System.Environment.NewLine + "   AND a04.BEDODT <= '" + strTodt + "' ";
            sql += System.Environment.NewLine + "   AND ISNULL(a04.BEDIPTHCD,'') NOT IN ('0','8') ";
            sql += System.Environment.NewLine + "   AND s41.SEQ = 0 ";
            sql += System.Environment.NewLine + "   AND ISNULL(s41.RECFG,'') NOT IN ('F','D') ";
            sql += System.Environment.NewLine + "   AND ISNULL(s41.OUTSEQ,'') <> '' ";
            sql += System.Environment.NewLine + "   AND s31.PRICD LIKE 'M%' ";
            sql += System.Environment.NewLine + "   AND s31.DIRCD = '1' ";
            if (strPid != "")
            {
                sql += System.Environment.NewLine + "   AND a04.PID = '" + strPid + "' ";
            }
            if (strQfycd != "")
            {
                sql += System.Environment.NewLine + "   AND s41.QFYCD = '" + strQfycd + "' ";
            }
            sql += System.Environment.NewLine + " ORDER BY a04.PID ";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_SysDate = MetroLib.Util.GetSysDate(conn);
                m_SysTime = MetroLib.Util.GetSysTime(conn);

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (bOpt1)
                    {
                        String strBEDEDT = reader["BEDEDT"].ToString();
                        String strEXDT = reader["EXDT"].ToString();
                        if (strEXDT == strBEDEDT) continue;
                    }
                    CData data = new CData();
                    data.Clear();
                    data.PID = reader["PID"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.BEDEDT = reader["BEDEDT"].ToString();
                    data.BEDODT = reader["BEDODT"].ToString();
                    data.QFYCD = reader["QLFYCD"].ToString();
                    data.EXDT = reader["EXDT"].ToString();
                    data.S41QFYCD = reader["S41QFYCD"].ToString();
                    data.S41DRID = reader["S41DRID"].ToString();
                    data.S41DRNM = reader["S41DRNM"].ToString();
                    data.OUTSEQ = reader["OUTSEQ"].ToString();
                    data.PRICD = reader["PRICD"].ToString();
                    data.EDICODE = reader["ISPCD"].ToString();
                    data.PRINM = reader["PRKNM"].ToString();
                    data.DANGA = reader["UTAMT"].ToString();
                    data.DQTY = reader["CALQY"].ToString();
                    data.ORDCNT = reader["ORDCNT"].ToString();
                    data.DDAY = reader["DDAY"].ToString();
                    data.GUMAK = reader["TTAMT"].ToString();
                    data.KYSTR = reader["KYSTR"].ToString();
                    data.IsDC = false;
                    data.FRFG = reader["FRFG"].ToString();
                    data.DPTCD = reader["DPTCD"].ToString();
                    data.BDIV = reader["BDIV"].ToString();

                    list.Add(data);

                }
                reader.Close();

                foreach (CData data in list)
                {
                    if (data.KYSTR != "" && data.FRFG == "A")
                    {
                        if (data.DPTCD == "ER" || data.BDIV == "3")
                        {
                            data.IsDC = IsDC_ER(data.KYSTR, conn);
                        }
                        else
                        {
                            data.IsDC = IsDC(data.KYSTR, conn);
                        }
                    }
                    if (bDcSkip == true && data.IsDC == true) continue;

                    list2.Add(data);
                }

                conn.Close();
            }

            grdMain.DataSource = null;
            grdMain.DataSource = list2;
            this.RefreshGridMain();
        }

        private Boolean IsDC(String kystr, OleDbConnection conn)
        {
            String[] arrKystr = kystr.Split(',');
            String strPid = arrKystr[0];
            String strOdt = arrKystr[1];
            String strOno = arrKystr[2];
            if (strPid == "" || strOdt == "" || strOno == "") return false;

            Boolean bRet = false;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DCFG ";
            sql += System.Environment.NewLine + "  FROM TE01 WITH (NOLOCK) ";
            sql += System.Environment.NewLine + " WHERE PID='" + strPid + "'";
            sql += System.Environment.NewLine + "   AND ODT='" + strOdt + "'";
            sql += System.Environment.NewLine + "   AND ONO='" + strOno + "'";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                String strDcfg = reader["DCFG"].ToString();
                bRet = (strDcfg == "1");
            }
            reader.Close();

            return bRet;
        }

        private Boolean IsDC_ER(String kystr, OleDbConnection conn)
        {
            String[] arrKystr = kystr.Split(',');
            String strPid = arrKystr[0];
            String strBededt = arrKystr[1];
            String strBdiv = arrKystr[2];
            String strOdt = arrKystr[3];
            String strOno = arrKystr[4];
            if (strPid == "" || strBededt == "" || strBdiv == "" || strOdt == "" || strOno == "") return false;


            Boolean bRet = false;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DCFG ";
            sql += System.Environment.NewLine + "  FROM TV01 WITH (NOLOCK) ";
            sql += System.Environment.NewLine + " WHERE PID='" + strPid + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT='" + strBededt + "'";
            sql += System.Environment.NewLine + "   AND BDIV='" + strBdiv + "'";
            sql += System.Environment.NewLine + "   AND ODT='" + strOdt + "'";
            sql += System.Environment.NewLine + "   AND ONO='" + strOno + "'";


            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                String strDcfg = reader["DCFG"].ToString();
                bRet = (strDcfg == "1" || strDcfg == "3");
            }
            reader.Close();

            return bRet;
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

        private void ADD0348Q_Load(object sender, EventArgs e)
        {
            this.SetQfycdCombo();
        }

        private void SetQfycdCombo()
        {
            try
            {
                cboQfycd.Items.Clear();
                cboQfycd.Items.Add("");

                string sql = "";
                sql += System.Environment.NewLine + "SELECT MST3CD, CDNM ";
                sql += System.Environment.NewLine + "  FROM TA88 (nolock) ";
                sql += System.Environment.NewLine + " WHERE MST1CD = 'A' ";
                sql += System.Environment.NewLine + "   AND MST2CD = '26' ";
                sql += System.Environment.NewLine + "   AND ISNULL(EXPDT,'') = '' ";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboQfycd.Items.Add(reader["MST3CD"].ToString() + "." + reader["CDNM"].ToString());
                    }
                    reader.Close();

                    conn.Close();
                }

                cboQfycd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
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
            e.Graph.DrawString("입원중원외처방발생환자리스트", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strCaption = "조회기간:" + txtFrDt.Text.ToString() + " - " + txtToDt.Text.ToString() + ",  자격:" + cboQfycd.SelectedItem.ToString();;
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption , Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0348Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "입원중원외처방발생환자리스트";
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
    }
}

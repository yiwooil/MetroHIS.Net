using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD_ADF0601Q
{
    public partial class ADD_ADF0601Q : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD_ADF0601Q()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD_ADF0601Q(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD_ADF0601Q_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD_ADF0601Q_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            for (int i = 1; i <= 100; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                grdMainView.Columns.Add(col);
                col.Caption = i.ToString();
                col.FieldName = "DAY" + i.ToString();
                col.Width = 45;
                col.OptionsColumn.AllowEdit = false;
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                col.DisplayFormat.FormatString = "###";

                col.Visible = true;
            }
            this.RefreshGridMain();
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
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;

            gcITEMNAME.GroupIndex = -1;

            if (cboBededt.SelectedItem == null) return;

            string pid = txtPid.Text.ToString();
            string bededt = cboBededt.SelectedItem.ToString();
            string frdt = txtFrdt.Text.ToString();
            string todt = txtTodt.Text.ToString();


            // 컬림을 일단 다 보이게 처리한다.
            for (int col = 1; col <= 100; col++)
            {
                string fieldName = "DAY" + col;
                grdMainView.Columns[fieldName].Visible = true;
                string exdt = frdt != "" ? frdt : bededt;
                DateTime dtDate;
                bool bSuccess = DateTime.TryParseExact(exdt, "yyyyMMdd", provider, System.Globalization.DateTimeStyles.None, out dtDate);
                dtDate = dtDate.AddDays(col - 1);
                grdMainView.Columns[fieldName].Caption = dtDate.ToString("yyyyMMdd").Substring(6);
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
                sql += Environment.NewLine + "SELECT T31.PID";
                sql += Environment.NewLine + "     , T31.BDEDT";
                sql += Environment.NewLine + "     , T31.ITEM";
                sql += Environment.NewLine + "     , T31.PRICD";
                sql += Environment.NewLine + "     , T31.UTAMT";
                sql += Environment.NewLine + "     , T31.EXDT";
                sql += Environment.NewLine + "     , T31.CALQY";
                sql += Environment.NewLine + "     , T31.DDAY";
                sql += Environment.NewLine + "     , T31.CHRLT";
                sql += Environment.NewLine + "     , T31.MSFG";
                sql += Environment.NewLine + "      ,T31.BUNDT";
                sql += Environment.NewLine + "     , T03.QFYCD";
                sql += Environment.NewLine + "     , T03.PDIV";
                sql += Environment.NewLine + "     , T03.UNICD";
                sql += Environment.NewLine + "     , T03.INSID";
                sql += Environment.NewLine + "     , T03.INSNM";
                sql += Environment.NewLine + "     , T03.RESID";
                sql += Environment.NewLine + "     , T03.FMRCD";
                sql += Environment.NewLine + "     , T03.QFYSB";
                sql += Environment.NewLine + "     , T03.PDIVS";
                sql += Environment.NewLine + "     , ISNULL(A02.IALWF,'0') IALWF";
                sql += Environment.NewLine + "     , T31.OKCD";
                sql += Environment.NewLine + "     , A02.PRKNM";
                sql += Environment.NewLine + "     , A88.CDNM ITEMNM";
                sql += Environment.NewLine + "     , A02.ISPCD";
                sql += Environment.NewLine + "     , A04.PDRID ";
                sql += Environment.NewLine + "     , (SELECT DRNM FROM TA07 WHERE DRID = A04.PDRID) PDRNM ";
                sql += Environment.NewLine + "     , T31.DRID ";
                sql += Environment.NewLine + "     , (SELECT DRNM FROM TA07 WHERE DRID = T31.DRID) DRNM ";
                sql += Environment.NewLine + "     , T31.TTAMT";
                sql += Environment.NewLine + "  FROM TT31 T31 (nolock) LEFT  JOIN TA88 A88 (nolock) ON A88.MST1CD='A' AND MST2CD='43' AND MST3CD = T31.ITEM ";
                sql += Environment.NewLine + "                         INNER JOIN TA04 A04 (nolock) ON A04.PID=T31.PID AND A04.BEDEDT=T31.BDEDT";
                sql += Environment.NewLine + "     , TT03 T03 (nolock)";
                sql += Environment.NewLine + "     , TA02 A02 (nolock)";
                sql += Environment.NewLine + " WHERE T31.PID   =? ";
                sql += Environment.NewLine + "   AND T31.BDEDT =? ";
                if (frdt != "")
                {
                    sql += Environment.NewLine + "   AND T31.EXDT>=? ";
                }
                if (todt != "")
                {
                    sql += Environment.NewLine + "   AND T31.EXDT<=? ";
                }
                sql += Environment.NewLine + "   AND T03.PID   = T31.PID ";
                sql += Environment.NewLine + "   AND T03.BDEDT = T31.BDEDT ";
                sql += Environment.NewLine + "   AND T03.CREDT = ( SELECT MAX(Z.CREDT) ";
                sql += Environment.NewLine + "                       FROM TT03 Z (nolock) ";
                sql += Environment.NewLine + "                      WHERE Z.PID   = T31.PID ";
                sql += Environment.NewLine + "                        AND Z.BDEDT = T31.BDEDT ";
                sql += Environment.NewLine + "                        AND Z.CREDT<= T31.EXDT ";
                sql += Environment.NewLine + "                   ) ";
                sql += Environment.NewLine + "   AND A02.PRICD = T31.PRICD ";
                sql += Environment.NewLine + "   AND A02.CREDT = ( SELECT MAX(Z.CREDT) ";
                sql += Environment.NewLine + "                       FROM ( SELECT ISNULL(MAX(Y.CREDT),'') CREDT ";
                sql += Environment.NewLine + "                                FROM TA02 Y (nolock) ";
                sql += Environment.NewLine + "                               WHERE Y.PRICD = T31.PRICD ";
                sql += Environment.NewLine + "                                 AND Y.CREDT<= T31.EXDT ";
                sql += Environment.NewLine + "                               UNION ALL ";
                sql += Environment.NewLine + "                              SELECT ISNULL(MIN(Y.CREDT),'') CREDT ";
                sql += Environment.NewLine + "                                FROM TA02 Y (nolock) ";
                sql += Environment.NewLine + "                               WHERE Y.PRICD = T31.PRICD ";
                sql += Environment.NewLine + "                            ) Z ";
                sql += Environment.NewLine + "                   ) ";


                List<object> para = new List<object>();
                para.Add(pid);
                para.Add(bededt);
                if (frdt != "") para.Add(frdt);
                if (todt != "") para.Add(todt);

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    string msfg = reader["MSFG"].ToString().ToUpper();

                    string qfycd = reader["QFYCD"].ToString();
                    string qfysb = reader["QFYSB"].ToString();
                    if (msfg == "Y" && qfysb != "") qfycd = qfysb; // 부자격임.

                    string pdiv = reader["PDIV"].ToString();
                    string pdivs = reader["PDIVS"].ToString();
                    if (msfg == "Y" && pdivs != "") pdiv = pdivs; // 부자격의 환자구분.

                    long utamt = MetroLib.StrHelper.ToLong(reader["UTAMT"].ToString());
                    double calqy = MetroLib.StrHelper.ToDouble(reader["CALQY"].ToString());
                    long dday = MetroLib.StrHelper.ToLong(reader["DDAY"].ToString());
                    long ttamt = MetroLib.StrHelper.ToLong(reader["TTAMT"].ToString());

                    string exdt = reader["EXDT"].ToString();


                    for (int i = 0; i < dday; i++)
                    {
                        DateTime dtExdt;
                        bool bSuccess = DateTime.TryParseExact(exdt, "yyyyMMdd", provider, System.Globalization.DateTimeStyles.None, out dtExdt);
                        dtExdt = dtExdt.AddDays(i);

                        string exdt_1 = dtExdt.ToString("yyyyMMdd");

                        bool bFind = false;
                        foreach (CData d in list)
                        {
                            if (d.PID != reader["PID"].ToString()) continue;
                            if (d.BDEDT != reader["BDEDT"].ToString()) continue;
                            if (d.ITEM != reader["ITEM"].ToString()) continue;
                            if (d.PDRID != reader["PDRID"].ToString()) continue;
                            if (d.EXDRID != reader["DRID"].ToString()) continue;
                            if (d.PRICD != reader["PRICD"].ToString()) continue;
                            if (d.CHRLT != reader["CHRLT"].ToString()) continue;
                            if (d.QFYCD != qfycd) continue;
                            if (d.OKCD != reader["OKCD"].ToString()) continue;
                            if (d.UTAMT != utamt) continue;

                            d.TTAMT += ttamt;
                            d.AddQty(exdt_1, calqy);

                            bFind = true;
                            break;
                        }

                        if (bFind == false)
                        {
                            CData data = new CData();
                            data.Clear();
                            data.PID = reader["PID"].ToString();
                            data.BDEDT = reader["BDEDT"].ToString();
                            data.ITEM = reader["ITEM"].ToString();
                            data.ITEMNM = reader["ITEMNM"].ToString();
                            data.PRICD = reader["PRICD"].ToString();
                            data.FRDT = frdt != "" ? frdt : bededt;
                            data.CHRLT = reader["CHRLT"].ToString();
                            data.QFYCD = qfycd;
                            data.PDIV = pdiv;
                            data.OKCD = reader["OKCD"].ToString();
                            data.PRKNM = reader["PRKNM"].ToString();
                            data.ISPCD = reader["ISPCD"].ToString();
                            data.PDRID = reader["PDRID"].ToString();
                            data.PDRNM = reader["PDRNM"].ToString();
                            data.EXDRID = reader["DRID"].ToString();
                            data.EXDRNM = reader["DRNM"].ToString();
                            data.UTAMT = utamt;
                            data.TTAMT = ttamt;
                            data.SetQty(exdt_1, calqy);

                            list.Add(data);
                        }
                    }

                    return true;
                });

                conn.Close();

                // 총 수량이 0인 자료 삭제
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].TQTY == 0)
                    {
                        list.RemoveAt(i);
                    }
                }

                // 정렬
                list.Sort(delegate(CData x, CData y)
                {
                    int ret = 0;
                    // 항목
                    ret = x.ITEMNAME.CompareTo(y.ITEMNAME);
                    if (ret != 0) return ret;
                    // 퇴원약여부
                    ret = x.OKCD.CompareTo(y.OKCD);
                    if (ret != 0) return ret;
                    // 수가코드
                    ret = x.PRICD.CompareTo(y.PRICD);
                    if (ret != 0) return ret;
                    // 단가
                    ret = (int)(x.UTAMT - y.UTAMT);
                    if (ret != 0) return ret;
                    // 월
                    ret = x.EXMM.CompareTo(y.EXMM);
                    if (ret != 0) return ret;
                    // 자격
                    ret = x.QFYCD.CompareTo(y.QFYCD);
                    if (ret != 0) return ret;
                    // 부담
                    ret = x.CHRLT.CompareTo(y.CHRLT);
                    if (ret != 0) return ret;

                    // 기본
                    return ret;
                });


                // 자료가 없는 일자는 숨긴다. 31일부터 앞으로
                bool visible = false;
                for (int col = 100; col > 1; col--)
                {
                    string fieldName = "DAY" + col;
                    for (int row = 0; row < grdMainView.RowCount; row++)
                    {
                        double val = MetroLib.StrHelper.ToDouble(grdMainView.GetRowCellValue(row, fieldName).ToString());
                        if (val != 0)
                        {
                            visible = true;
                            break;
                        }
                    }
                    if (visible == false)
                        grdMainView.Columns[fieldName].Visible = false;
                    else
                        break;
                }

            }
            gcITEMNAME.GroupIndex = 0;
            grdMainView.ExpandAllGroups();

            grdMainView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn());

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

        private void txtPid_TextChanged(object sender, EventArgs e)
        {
            if (txtPid.Text.ToString().Length != 9)
            {
                txtPnm.Text = "";
                cboBededt.Items.Clear();
                grdMain.DataSource = null;
            }
        }

        private void txtPid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                if (txtPid.Text.ToString().Trim() == "")
                {
                    ADD_ADF0601Q_1 f = new ADD_ADF0601Q_1();
                    f.m_User = m_User;
                    f.m_Prjcd = m_Prjcd;
                    f.ShowDialog(this);
                    if (f.m_pid != "") txtPid.Text = f.m_pid;
                    f = null;
                }

                AfterPidInput();
                cboBededt.Focus();
            }
        }

        private void txtPid_Leave(object sender, EventArgs e)
        {
            AfterPidInput();
        }

        private void AfterPidInput()
        {
            if (txtPid.Text.ToString() == "") return;
            if (txtPid.Text.ToString().Length < 9)
            {
                txtPid.Text = txtPid.Text.ToString().PadLeft(9, '0');
            }
            try
            {
                GetPatientInformation(txtPid.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetPatientInformation(string p_pid)
        {
            txtPnm.Text = "";
            cboBededt.Items.Clear();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT PNM";
                sql += Environment.NewLine + "  FROM TA01";
                sql += Environment.NewLine + " WHERE PID=?";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", p_pid));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtPnm.Text = reader["PNM"].ToString();
                    }
                    reader.Close();
                }

                sql = "";
                sql += Environment.NewLine + "SELECT BEDEDT";
                sql += Environment.NewLine + "  FROM TA04";
                sql += Environment.NewLine + " WHERE PID=?";
                sql += Environment.NewLine + " ORDER BY BEDEDT DESC";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", p_pid));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboBededt.Items.Add(reader["BEDEDT"].ToString());
                    }
                    reader.Close();
                }

                conn.Close();

                if (cboBededt.Items.Count > 0) cboBededt.SelectedIndex = 0;
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
            grdMainView.OptionsPrint.AutoWidth = false; // 이 값이 true이면 출력시 column의 폭을 자동으로 조절하여 한 페이지에 출력되게 한다.
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("수가조견표", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string msg = "환자 : " + txtPid.Text.ToString() + " " + txtPnm.Text.ToString();
            msg += ", 입원일 : " + cboBededt.SelectedItem.ToString();
            if (txtFrdt.Text.ToString() != "" || txtTodt.Text.ToString() != "") msg += ", 기간 : " + txtFrdt.Text.ToString() + " ~ " + txtTodt.Text.ToString();
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
            e.Graph.DrawString("ADD_ADF0601Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0250Q
{
    public partial class ADD0250Q : Form
    {
        //private Boolean m_OnPgm;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private String m_SysDate;
        private String m_SysTime;

        private Dictionary<string, string> m_dicQFYCD = new Dictionary<string, string>();
        private Dictionary<string, string> m_dicPDIV = new Dictionary<string, string>();
        private Dictionary<string, string> m_dicEMPID = new Dictionary<string, string>();

        public ADD0250Q()
        {
            InitializeComponent();
            //m_OnPgm = false;
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD0250Q(String user, String pwd, String prjcd)
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
            if (txtYYMM.Text.ToString() == "")
            {
                MessageBox.Show("퇴원월을 입력하세요.");
                return;
            }

            string strFrDt = txtYYMM.Text.ToString() + "01";
            string strToDt = txtYYMM.Text.ToString() + "31";
            string strQfyDiv = "";
            if (chkQfycd2.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'21','22','23','29'";
                }
                else
                {
                    strQfyDiv += ",'21','22','23','29'";
                }
            }
            if (chkQfycd3.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'31','32'";
                }
                else
                {
                    strQfyDiv += ",'31','32'";
                }
            }
            if (chkQfycd5.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'50'";
                }
                else
                {
                    strQfyDiv += ",'50'";
                }
            }
            if (chkQfycd6.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'61'";
                }
                else
                {
                    strQfyDiv += ",'61'";
                }
            }
            if (chkQfycd4.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'40'";
                }
                else
                {
                    strQfyDiv += ",'40'";
                }
            }
            if (chkQfycd38.Checked == true)
            {
                if (strQfyDiv == "")
                {
                    strQfyDiv = "'38','39'";
                }
                else
                {
                    strQfyDiv += ",'38','39'";
                }
            }
            if (strQfyDiv == "")
            {
                strQfyDiv = "'21','22','23','29','31','32','38','39','40','50','61'";
            }

            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string sql = "";
            sql += Environment.NewLine + "SELECT A04.PID";
            sql += Environment.NewLine + "     , A04.BEDEDT";
            sql += Environment.NewLine + "     , A01.PNM";
            sql += Environment.NewLine + "     , A01.PSEX";
            sql += Environment.NewLine + "     , A01.BTHDT";
            sql += Environment.NewLine + "     , A04.BEDODT";
            sql += Environment.NewLine + "     , A04.DRGNO"; // 2026.04.02 WOOIL - 추가
            sql += Environment.NewLine + "     , T03.CREDT";
            sql += Environment.NewLine + "     , T03.QFYCD";
            sql += Environment.NewLine + "     , T03.PDIV";
            sql += Environment.NewLine + "     , T03.QFYSB";
            sql += Environment.NewLine + "     , T03.PDIVS";
            sql += Environment.NewLine + "     , (SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A04.PDRID) DRNM";
            sql += Environment.NewLine + "     , A04.DPTCD";
            sql += Environment.NewLine + "     , A04.WARDID + '-' + A04.RMID + '-' + A04.BEDID WARD";
            sql += Environment.NewLine + "     , A04.BEDODIV + '.' + ISNULL((SELECT X.CDNM FROM TA88 X WHERE X.MST1CD='A' AND X.MST2CD='31' AND X.MST3CD=A04.BEDODIV),'') BEDODIVNM ";
            sql += Environment.NewLine + "     , T03A_A.QFYCD QFYSB2";
            sql += Environment.NewLine + "     , T03A_A.PDIV PDIVS2";
            sql += Environment.NewLine + "     , T03A_B.QFYCD QFYSB3";
            sql += Environment.NewLine + "     , T03A_B.PDIV PDIVS3";
            sql += Environment.NewLine + "  FROM TA04 A04 INNER JOIN TA01 A01 ON A01.PID=A04.PID";
            sql += Environment.NewLine + "                INNER JOIN TT03 T03 ON T03.PID=A04.PID AND T03.BDEDT=A04.BEDEDT";
            sql += Environment.NewLine + "                LEFT  JOIN TT03A T03A_A ON T03A_A.PID=A04.PID AND T03A_A.BDEDT=A04.BEDEDT AND T03A_A.QFYCH='A'";
            sql += Environment.NewLine + "                LEFT  JOIN TT03A T03A_B ON T03A_B.PID=A04.PID AND T03A_B.BDEDT=A04.BEDEDT AND T03A_B.QFYCH='B'";
            sql += Environment.NewLine + " WHERE A04.BEDODT >='" + strFrDt + "' ";
            sql += Environment.NewLine + "   AND A04.BEDODT <='" + strToDt + "' ";
            sql += Environment.NewLine + "   AND A04.WARDID <> 'ER1' ";
            sql += Environment.NewLine + "   AND A04.PID    NOT LIKE 'T%' ";
            sql += Environment.NewLine + "   AND (";
            sql += Environment.NewLine + "          T03.QFYCD  IN (" + strQfyDiv + ") ";
            sql += Environment.NewLine + "       OR ISNULL(T03.QFYSB,'')  IN (" + strQfyDiv + ") ";
            sql += Environment.NewLine + "       OR ISNULL(T03A_A.QFYCD,'')  IN (" + strQfyDiv + ") ";
            sql += Environment.NewLine + "       OR ISNULL(T03A_B.QFYCD,'')  IN (" + strQfyDiv + ") ";
            sql += Environment.NewLine + "       )";
            sql += Environment.NewLine + " ORDER BY A04.PID,A04.BEDEDT,T03.CREDT ";

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
                    CData data = new CData();
                    data.Clear();
                    data.PID = reader["PID"].ToString();
                    data.BEDEDT = reader["BEDEDT"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.PSEX = reader["PSEX"].ToString();
                    data.PAGE = "";
                    data.BTHDT = reader["BTHDT"].ToString();
                    data.BEDODT = reader["BEDODT"].ToString();
                    data.DRGNO = reader["DRGNO"].ToString();
                    data.CREDT = reader["CREDT"].ToString();
                    data.QFYCD = reader["QFYCD"].ToString();
                    data.QFYCDNM = "";
                    data.PDIV = reader["PDIV"].ToString();
                    data.PDIVNM = "";
                    data.RPOK = "";
                    data.QFYSB = reader["QFYSB"].ToString();
                    data.QFYSBNM = "";
                    data.PDIVS = reader["PDIVS"].ToString();
                    data.PDIVSNM = "";
                    data.RPOKS = "";
                    data.PDRNM = reader["DRNM"].ToString();
                    data.DPTCD = reader["DPTCD"].ToString();
                    data.WARD = reader["WARD"].ToString();
                    data.BEDODIVNM = reader["BEDODIVNM"].ToString();
                    data.SIMSANM = "";

                    list.Add(data);

                    if (reader["QFYSB2"].ToString() != "")
                    {
                        data = new CData();
                        data.Clear();
                        data.PID = reader["PID"].ToString();
                        data.BEDEDT = reader["BEDEDT"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.PSEX = reader["PSEX"].ToString();
                        data.PAGE = "";
                        data.BTHDT = reader["BTHDT"].ToString();
                        data.BEDODT = reader["BEDODT"].ToString();
                        data.CREDT = reader["CREDT"].ToString();
                        data.QFYCD = "";
                        data.QFYCDNM = "";
                        data.PDIV = "";
                        data.PDIVNM = "";
                        data.RPOK = "";
                        data.QFYSB = reader["QFYSB2"].ToString();
                        data.QFYSBNM = "";
                        data.PDIVS = reader["PDIVS2"].ToString();
                        data.PDIVSNM = "";
                        data.RPOKS = "";
                        data.PDRNM = reader["DRNM"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.WARD = reader["WARD"].ToString();
                        data.BEDODIVNM = reader["BEDODIVNM"].ToString();
                        data.SIMSANM = "";

                        list.Add(data);
                    }

                    if (reader["QFYSB3"].ToString() != "")
                    {
                        data = new CData();
                        data.Clear();
                        data.PID = reader["PID"].ToString();
                        data.BEDEDT = reader["BEDEDT"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.PSEX = reader["PSEX"].ToString();
                        data.PAGE = "";
                        data.BTHDT = reader["BTHDT"].ToString();
                        data.BEDODT = reader["BEDODT"].ToString();
                        data.CREDT = reader["CREDT"].ToString();
                        data.QFYCD = "";
                        data.QFYCDNM = "";
                        data.PDIV = "";
                        data.PDIVNM = "";
                        data.RPOK = "";
                        data.QFYSB = reader["QFYSB3"].ToString();
                        data.QFYSBNM = "";
                        data.PDIVS = reader["PDIVS3"].ToString();
                        data.PDIVSNM = "";
                        data.RPOKS = "";
                        data.PDRNM = reader["DRNM"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.WARD = reader["WARD"].ToString();
                        data.BEDODIVNM = reader["BEDODIVNM"].ToString();
                        data.SIMSANM = "";

                        list.Add(data);
                    }
                }
                reader.Close();

                // 이렇게 분리 하는 것이 속도가 빠르다.
                int no = 0;
                foreach (CData data in list)
                {
                    data.NO = (++no).ToString();
                    data.PAGE = MetroLib.Util.GetAge(data.BTHDT, data.BEDEDT, "year");
                    data.QFYCDNM = GetQfycdNm(data.QFYCD, conn);
                    data.PDIVNM = GetPdivNm(data.PDIV, conn);
                    data.RPOK = GetRpok(data.PID, data.BEDEDT, data.QFYCD, conn);
                    data.QFYSBNM = GetQfycdNm(data.QFYSB, conn);
                    data.PDIVSNM = GetPdivNm(data.PDIVS, conn);
                    data.RPOKS = GetRpok(data.PID, data.BEDEDT, data.QFYSB, conn);
                    data.SIMSANM = GetSimsaNm(data.PID, data.BEDEDT, conn);
                }

                conn.Close();
            }
            this.RefreshGridMain();
        }

        public String GetQfycdNm(String p_qfycd, OleDbConnection p_conn)
        {
            if (p_qfycd == "") return "";
            if (m_dicQFYCD.ContainsKey(p_qfycd) == true)
            {
                return m_dicQFYCD[p_qfycd].ToString(); // 보관된 자료를 사용.
            }

            String strRet = "";
            String sql = "SELECT CDNM FROM TA88 WHERE MST1CD='A' AND MST2CD='26' AND MST3CD='" + p_qfycd + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["CDNM"].ToString();
                }
                reader.Close();
            }
            m_dicQFYCD[p_qfycd] = strRet; // 나중에 사용하도록 보관.
            return strRet;
        }

        public String GetPdivNm(String p_pdiv, OleDbConnection p_conn)
        {
            if (p_pdiv == "") return "";
            if (m_dicPDIV.ContainsKey(p_pdiv) == true)
            {
                return m_dicPDIV[p_pdiv].ToString(); // 보관된 자료를 사용.
            }

            String strRet = "";
            String sql = "SELECT CDNM FROM TA88 WHERE MST1CD='A' AND MST2CD='23' AND MST3CD='" + p_pdiv + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["CDNM"].ToString();
                }
                reader.Close();
            }
            m_dicPDIV[p_pdiv] = strRet; // 나중에 사용하도록 보관.
            return strRet;
        }

        public String GetSimsaNm(String p_pid, String p_bededt, OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "";
            sql += Environment.NewLine + "SELECT SIMSA, SIMSAE";
            sql += Environment.NewLine + "  FROM TT56";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += Environment.NewLine + "   AND BDEDT='" + p_bededt + "'";

            String strSimsa = "";
            String strSimsae = "";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strSimsa = reader["SIMSA"].ToString();
                    strSimsae = reader["SIMSAE"].ToString();
                }
                reader.Close();
            }
            if (strSimsa == "" && strSimsae == "") return "";

            String strEmpid = "";
            if (strSimsae != "")
            {
                String[] arr = (strSimsae + "$$$").Split('$');
                strEmpid = arr[2];
            }
            else if (strSimsa != "")
            {
                String[] arr = (strSimsae + "$$$").Split('$');
                strEmpid = arr[2];
            }
            if (strEmpid == "") return "";

            if (m_dicEMPID.ContainsKey(strEmpid) == true)
            {
                return m_dicEMPID[strEmpid].ToString(); // 보관된 자료를 사용.
            }

            sql = "";
            sql += Environment.NewLine + "SELECT EMPNM";
            sql += Environment.NewLine + "  FROM TA13";
            sql += Environment.NewLine + " WHERE EMPID='" + strEmpid + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["EMPNM"].ToString();
                }
                reader.Close();
            }
            m_dicEMPID[strEmpid] = strRet; // 나중에 사용하도록 보관.
            return strRet;
        }

        public String GetRpok(String p_pid, String p_bededt, String p_qfycd, OleDbConnection p_conn)
        {
            if (p_qfycd == "") return "";

            String strRet = "";
            String sql = "";
            sql += Environment.NewLine + "SELECT COUNT(*) AS CNT";
            sql += Environment.NewLine + "  FROM TT41";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += Environment.NewLine + "   AND BDEDT='" + p_bededt + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + p_qfycd + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    long lCnt = int.Parse(reader["CNT"].ToString());
                    if (lCnt <= 0) strRet = "X";
                }
                reader.Close();
            }
            return strRet;
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "퇴원환자리스트";
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
                //this.ShowIndicator(grdMainView);
                this.Print();
                //this.HideIndicator(grdMainView);
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
            e.Graph.DrawString("퇴원환자리스트",Color.Black,new RectangleF(0, 10, 1080, 40),DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("퇴원월:" + txtYYMM.Text.ToString(), Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0250Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //grdMainView.IndicatorWidth = 50;
            //if (e.RowHandle >= 0)
            //{
            //    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            //}
        }

        private void grdMainView_EndSorting(object sender, EventArgs e)
        {
            // 특정 컬럼을 클릭했을 경우 줄번호를 다시 부여한다.
            for (int i = 0; i < grdMainView.RowCount; i++)
            {
                grdMainView.SetRowCellValue(i, gcNO, i + 1);
            }
        }

        //string indicatorFieldName = "No";

        //private void ShowIndicator(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        //{
        //    DevExpress.XtraGrid.Columns.GridColumn col = gridView.Columns.AddVisible(indicatorFieldName);
        //    col.VisibleIndex = 0;
        //    col.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
        //    gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData; 
        //}

        //private void HideIndicator(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        //{
        //    gridView.Columns.Remove(gridView.Columns[indicatorFieldName]);
        //    gridView.CustomUnboundColumnData -= gridView_CustomUnboundColumnData; 
        //}

        //void gridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        //{
        //    if (e.Column.FieldName == indicatorFieldName && e.IsGetData)
        //    {
        //        e.Value = e.ListSourceRowIndex;
        //    }
        //} 
    }
}

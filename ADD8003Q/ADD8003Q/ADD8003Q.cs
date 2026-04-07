using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ADD8003Q
{
    public partial class ADD8003Q : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private Boolean IsFirst;

        private String m_SysDate;
        private String m_SysTime;

        public ADD8003Q()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            IsFirst = true;
        }

        public ADD8003Q(String user, String pwd, String prjcd, String accno, String cntno)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;

            if (accno != "" && cntno!="")
            {
                txtAccno.Text = accno;
                txtCntno.Text = cntno;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == MetroLib.Win32API.WM_COPYDATA)
            {
                MetroLib.Win32API.COPYDATASTRUCT lParam1 = (MetroLib.Win32API.COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(MetroLib.Win32API.COPYDATASTRUCT));
                MetroLib.Win32API.COPYDATASTRUCT lParam2 = new MetroLib.Win32API.COPYDATASTRUCT();
                lParam2 = (MetroLib.Win32API.COPYDATASTRUCT)m.GetLParam(lParam2.GetType());

                //MessageBox.Show(lParam1.lpData);
                //MessageBox.Show(lParam2.lpData);

                // 사용자 메시지이면
                String accno = "";
                String cntno = "";
                ParseArg(lParam1.lpData, ref accno, ref cntno);
                if (accno != "" && cntno != "")
                {
                    txtAccno.Text = accno;
                    txtCntno.Text = cntno;
                    btnQuery.PerformClick();
                }
            }
        }

        private void ParseArg(String arg, ref String accno, ref String cntno)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("ACCNO".Equals(val[0].ToUpper())) accno = val[1];
                else if ("CNTNO".Equals(val[0].ToUpper())) cntno = val[1];
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ADD8003Q_P f = new ADD8003Q_P();
            f.ShowDialog(this);
            Boolean bSel = f.m_SEL;
            String accno = f.m_ACCNO;
            String cntno = f.m_CNTNO;
            f = null;
            if (bSel == true)
            {
                txtAccno.Text = accno;
                txtCntno.Text = cntno;

                this.btnQuery.PerformClick();
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
            String strAccno = txtAccno.Text.ToString();
            String strCntno = txtCntno.Text.ToString();

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;


            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_SysDate = MetroLib.Util.GetSysDate(conn);
                m_SysTime = MetroLib.Util.GetSysTime(conn);

                string sql = "";

                sql = "";
                sql += System.Environment.NewLine + "SELECT REPDT, DEMCNTTOT, EXAMCNTTOT, PAYNOTCNTTOT, PAYRSVCNTTOT, DELCNTTOT, DEMQYTOT, EXAMQYTOT, PAYNOTQYTOT, PAYRSVQYTOT, DELQYTOT, PAYQYTOT, MEMO ";
                sql += System.Environment.NewLine + "  FROM TIE_I0301 ";
                sql += System.Environment.NewLine + " WHERE ACCNO  = '" + strAccno + "' ";
                sql += System.Environment.NewLine + "   AND CNTNO  = '" + strCntno + "' ";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtRepdt.Text = reader["REPDT"].ToString();//통보일자
                        txtDemcnttot.Text = ToCurrency(reader["DEMCNTTOT"].ToString());//청구건수
                        txtDemqytot.Text = ToCurrency(reader["DEMQYTOT"].ToString());//청구금액
                        txtExamcnttot.Text = ToCurrency(reader["EXAMCNTTOT"].ToString());//심사건수
                        txtExamqytot.Text = ToCurrency(reader["EXAMQYTOT"].ToString());//심사금액
                        txtPaynotcnttot.Text = ToCurrency(reader["PAYNOTCNTTOT"].ToString());//불능건수
                        txtPaynotqytot.Text = ToCurrency(reader["PAYNOTQYTOT"].ToString());//블능금액
                        txtPayrsvcnttot.Text = ToCurrency(reader["PAYRSVCNTTOT"].ToString());//보류건수
                        txtPayrsvqytot.Text = ToCurrency(reader["PAYRSVQYTOT"].ToString());//보류금액
                        txtDelcnttot.Text = ToCurrency(reader["DELCNTTOT"].ToString());//삭감건수
                        txtDelqytot.Text = ToCurrency(reader["DELQYTOT"].ToString());//삭감금액
                        txtPayqytot.Text = ToCurrency(reader["PAYQYTOT"].ToString());//지급결정액
                        txtMemo.Text = reader["MEMO"].ToString().Trim();//메모
                    }
                    reader.Close();
                }

                sql = "";
                sql += System.Environment.NewLine + "SELECT I0302.EPRTNO,RTRIM(I0302.PNM) PNM,I0302.DEMAMT,I0302.SENDAMT,I0302.DELAMT";
                sql += System.Environment.NewLine + "     , I0303.ELINENO,I0303.RECD";
                sql += System.Environment.NewLine + "     , ISNULL((SELECT I88.CDNM+'/' FROM TI88 I88 WHERE I88.MST1CD='A' AND I88.MST2CD IN ('JJCD_SANJE','BULCD_SANJE')  AND I88.MST3CD=LTRIM(I0303.RECD)),'')+ISNULL(RTRIM(I0303.DCPS),'') AS RMK ";
                sql += System.Environment.NewLine + "     , I0303.LINEAMT,I0303.LINEQTY";
                sql += System.Environment.NewLine + "     , I0302.ACCNO,I0302.CNTNO ";
                sql += System.Environment.NewLine + "  FROM TIE_I0302 I0302 INNER JOIN TIE_I0303 I0303 ON I0303.ACCNO  = I0302.ACCNO";
                sql += System.Environment.NewLine + "                                                 AND I0303.CNTNO  = I0302.CNTNO ";
                sql += System.Environment.NewLine + "                                                 AND I0303.EPRTNO = I0302.EPRTNO ";
                sql += System.Environment.NewLine + " WHERE I0302.ACCNO  = '" + strAccno + "' ";
                sql += System.Environment.NewLine + "   AND I0302.CNTNO  = '" + strCntno + "' ";
                sql += System.Environment.NewLine + " ORDER BY I0302.ACCNO,I0302.CNTNO,I0302.EPRTNO,I0303.ELINENO ";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.EPRTNO = reader["EPRTNO"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.DEMAMT = reader["DEMAMT"].ToString();
                        data.SENDAMT = reader["SENDAMT"].ToString();
                        data.DELAMT = reader["DELAMT"].ToString();
                        data.ELINENO = reader["ELINENO"].ToString();
                        data.RECD = reader["RECD"].ToString();
                        data.RMK = reader["RMK"].ToString();
                        data.LINEAMT = reader["LINEAMT"].ToString();
                        data.LINEQTY = reader["LINEQTY"].ToString();
                        data.ACCNO = reader["ACCNO"].ToString();
                        data.CNTNO = reader["CNTNO"].ToString();
                        data.PRICD = ""; // 바로 아래에서
                        data.BGIHO = ""; // 바로 아래에서

                        list.Add(data);

                    }
                    reader.Close();
                }

                foreach (CData data in list)
                {
                    String pricd = "";
                    String bgiho = "";
                    GetFINFO(data.ACCNO, data.EPRTNO, data.ELINENO, conn, ref pricd, ref bgiho);
                    data.PRICD = pricd;
                    data.BGIHO = bgiho;
                }

                conn.Close();
            }

            this.RefreshGridMain();
        }

        private String ToCurrency(String value)
        {
            try
            {
                if (value == "") value = "0";
                int val = int.Parse(value);
                return String.Format("{0:#,##0}", val);
            }
            catch (Exception ex)
            {
                return value;
            }
        }

        private void GetFINFO(String p_accno, String p_eprtno, String p_elineno, OleDbConnection p_conn, ref String p_pricd, ref String p_bgiho)
        {
            // ---------------------------------------------------------------------
            String strDemno = "";

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMNO ";
            sql += System.Environment.NewLine + "  FROM TIE_I010 WITH (NOLOCK) ";
            sql += System.Environment.NewLine + " WHERE ACCNO='" + p_accno + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {


                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strDemno = reader["DEMNO"].ToString();
                }
                reader.Close();
            }
            if (strDemno == "") return;

            // ---------------------------------------------------------------------
            String strYymm = "";
            String strBussdiv = "";

            sql = "";
            sql += System.Environment.NewLine + "SELECT YYMM, BUSSDIV ";
            sql += System.Environment.NewLine + "  FROM TIE_M010 WITH (NOLOCK) ";
            sql += System.Environment.NewLine + " WHERE DEMNO='" + strDemno + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strYymm = reader["YYMM"].ToString();
                    strBussdiv = reader["BUSSDIV"].ToString();
                }
                reader.Close();
            }
            if (strYymm == "" || strBussdiv == "") return;

            // ---------------------------------------------------------------------
            if (strBussdiv == "1")
            {
                sql="";
                sql += System.Environment.NewLine + "SELECT I1F.PRICD,I1F.BGIHO ";
                sql += System.Environment.NewLine + "  FROM TI1A I1A INNER JOIN TI1F I1F ON I1F.EXDATE=I1A.EXDATE";
                sql += System.Environment.NewLine + "                                   AND I1F.QFYCD=I1A.QFYCD ";
                sql += System.Environment.NewLine + "                                   AND I1F.JRBY=I1A.JRBY ";
                sql += System.Environment.NewLine + "                                   AND I1F.PID=I1A.PID ";
                sql += System.Environment.NewLine + "                                   AND I1F.UNISQ=I1A.UNISQ ";
                sql += System.Environment.NewLine + "                                   AND I1F.SIMCS=I1A.SIMCS ";
                sql += System.Environment.NewLine + " WHERE I1A.YYMM  ='" + strYymm + "' ";
                sql += System.Environment.NewLine + "   AND I1A.DEMNO ='" + strDemno + "' ";
                sql += System.Environment.NewLine + "   AND I1A.EPRTNO= " + p_eprtno + "  ";
                sql += System.Environment.NewLine + "   AND I1A.QFYCD ='50'";
                sql += System.Environment.NewLine + "   AND I1F.ELINENO='" + p_elineno + "' ";
            }
            else
            {
                sql="";
                sql += System.Environment.NewLine + "SELECT I2F.PRICD,I2F.BGIHO ";
                sql += System.Environment.NewLine + "  FROM TI2A I2A INNER JOIN TI2F I2F ON I2F.BDODT=I2A.BDODT";
                sql += System.Environment.NewLine + "                                   AND I2F.QFYCD=I2A.QFYCD ";
                sql += System.Environment.NewLine + "                                   AND I2F.JRBY=I2A.JRBY ";
                sql += System.Environment.NewLine + "                                   AND I2F.PID=I2A.PID ";
                sql += System.Environment.NewLine + "                                   AND I2F.UNISQ=I2A.UNISQ ";
                sql += System.Environment.NewLine + "                                   AND I2F.SIMCS=I2A.SIMCS ";
                sql += System.Environment.NewLine + " WHERE I2A.YYMM  ='" + strYymm + "' ";
                sql += System.Environment.NewLine + "   AND I2A.DEMNO ='" + strDemno + "' ";
                sql += System.Environment.NewLine + "   AND I2A.EPRTNO= " + p_eprtno + "  ";
                sql += System.Environment.NewLine + "   AND I2A.QFYCD ='50'";
                sql += System.Environment.NewLine + "   AND I2F.ELINENO='" + p_elineno + "' ";
            }

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    p_pricd = reader["PRICD"].ToString();
                    p_bgiho = reader["BGIHO"].ToString();
                }
                reader.Close();
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
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 140, 50);
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
            e.Graph.DrawString("산재보험진료비심사내역통보서", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strCaption = "";
            strCaption += "접수번호 : " + txtAccno.Text.ToString();
            strCaption += ", 차수 : " + txtCntno.Text.ToString();
            strCaption += ", 통보일자 : " + txtRepdt.Text.ToString();
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);

            String strCaption2 = "";
            strCaption2 += "청구 : " + txtDemcnttot.Text.ToString() + "건 " + txtDemqytot.Text.ToString() + "원";
            strCaption2 += ", 심사 : " + txtExamcnttot.Text.ToString() + "건 " + txtExamqytot.Text.ToString() + "원";
            strCaption2 += ", 불능 : " + txtPaynotcnttot.Text.ToString() + "건 " + txtPaynotqytot.Text.ToString() + "원";
            strCaption2 += ", 보류 : " + txtPayrsvcnttot.Text.ToString() + "건 " + txtPayrsvqytot.Text.ToString() + "원";
            strCaption2 += ", 지급결정액 : " + txtPayqytot.Text.ToString() + "원";
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption2, Color.Black, new RectangleF(0, 65, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD8003Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void ADD8003Q_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            if (txtAccno.Text.ToString() != "" && txtCntno.Text.ToString() != "")
            {
                btnQuery.PerformClick();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0547R
{
    public partial class ADD0547R : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool IsFirst;

        public ADD0547R()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0547R(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0547R_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0547R_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            cboDemgb.SelectedIndex = 1;
            this.InitDate();
        }

        private void InitDate()
        {
            try
            {
                string sysDate = "";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    sysDate = MetroLib.Util.GetSysDate(conn);
                }
                string sysDate01 = sysDate.Substring(0, 6) + "01";
                // 이번 달
                DateTime dtSysDate = DateTime.ParseExact(sysDate, "yyyyMMdd", null);
                DateTime dtSysDate01 = DateTime.ParseExact(sysDate01, "yyyyMMdd", null);
                // 이전 달
                DateTime dtLastMonDate = dtSysDate.AddMonths(-1);
                // 외래 용
                txtFrdt.Text = dtLastMonDate.ToString("yyyyMM");
                txtTodt.Text = dtLastMonDate.ToString("yyyyMM");

                //// 외래용 청구월
                //txtYymm.Text = dtLastMonDate.ToString("yyyyMM");
                //txtYymmto.Text = dtLastMonDate.ToString("yyyyMM");
                //// 입원용 청구일자
                //txtFrdt.Text = dtLastMonDate.ToString("yyyyMM01");
                //txtTodt.Text = dtSysDate01.AddDays(-1).ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbIofg1_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel();
        }

        private void rbIofg2_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel();
        }

        private void chkComp_CheckedChanged(object sender, EventArgs e)
        {
            SetLabel();
        }

        private void SetLabel()
        {
            if (chkComp.Checked == true)
            {
                label2.Text = "완료일";
                if (txtFrdt.Text.ToString().Length != 8) txtFrdt.Text = txtFrdt.Text.ToString().Substring(0, 6) + "01";
                if (txtTodt.Text.ToString().Length != 8)
                {
                    DateTime dtDate = DateTime.ParseExact(txtTodt.Text.ToString().Substring(0, 6) + "01", "yyyyMMdd", null);
                    txtTodt.Text = dtDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
                }
            }
            else
            {
                if (rbIofg1.Checked == true)
                {
                    label2.Text = "청구월";
                    if (txtFrdt.Text.ToString().Length > 6) txtFrdt.Text = txtFrdt.Text.ToString().Substring(0, 6);
                    if (txtTodt.Text.ToString().Length > 6) txtTodt.Text = txtTodt.Text.ToString().Substring(0, 6);
                }
                else if (rbIofg2.Checked == true){
                    label2.Text = "청구일";
                    if (txtFrdt.Text.ToString().Length != 8) txtFrdt.Text = txtFrdt.Text.ToString().Substring(0, 6) + "01";
                    if (txtTodt.Text.ToString().Length != 8)
                    {
                        DateTime dtDate = DateTime.ParseExact(txtTodt.Text.ToString().Substring(0, 6) + "01", "yyyyMMdd", null);
                        txtTodt.Text = dtDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
                    }
                }
            }
            grdMain.DataSource = null;
            RefreshGridMain();
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

            List<CData> list_tmp = new List<CData>();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string qfy51="";
                string sql = "";
                sql += "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='207'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    qfy51 = reader["FLD2QTY"].ToString();

                    return MetroLib.SqlHelper.CONTINUE;
                });


                string tTI1A="TI1A";
                string fEXDATE="EXDATE";
                if (rbIofg2.Checked == true)
                {
                    tTI1A = "TI2A";
                    fEXDATE = "BDODT";
                }

                sql = "";
                sql += Environment.NewLine + "SELECT A.PID";
                sql += Environment.NewLine + "     , A.PNM";
                sql += Environment.NewLine + "     , A.GENDT";
                sql += Environment.NewLine + "     , A.IPWON";
                sql += Environment.NewLine + "     , A.TGWON";
                sql += Environment.NewLine + "     , A.MADDR";
                sql += Environment.NewLine + "     , A.APPRNO";
                sql += Environment.NewLine + "     , A.UNAMT";
                sql += Environment.NewLine + "     , A.EPRTNO";
                sql += Environment.NewLine + "     , A.RESID";
                sql += Environment.NewLine + "     , A.UNICD ";
                sql += Environment.NewLine + "     , A.UNINM ";
                sql += Environment.NewLine + "     , A.STEDT ";
                sql += Environment.NewLine + "     , CASE WHEN A.STEDT<>'99991231' AND ISDATE(A.STEDT)=1 THEN CONVERT(VARCHAR,DATEADD(D,A.EXAMC-1,A.STEDT),112) ELSE A.STEDT END ENDDT ";
                sql += Environment.NewLine + "  FROM " + tTI1A + " A (nolock)";
                if (chkComp.Checked == true)
                {
                    // 2018.02.20 WOOIL - 완료일자 추가
                    sql += Environment.NewLine + "       INNER JOIN TIE_H010 H010 WITH (NOLOCK) ON H010.DEMNO = A.DEMNO";
                    sql += Environment.NewLine + " WHERE H010.COMPLDT >='" + txtFrdt.Text.ToString() + "'";
                    sql += Environment.NewLine + "   AND H010.COMPLDT <='" + txtTodt.Text.ToString() + "'";
                }
                else
                {
                    sql += Environment.NewLine + " WHERE A." + fEXDATE + " >= '" + txtFrdt.Text.ToString() + "' ";
                    sql += Environment.NewLine + "   AND A." + fEXDATE + " <= '" + txtTodt.Text.ToString() + "' ";
                    //If strIofg = "1" And strStedtfg = "1" Then
                    //    sql += Environment.NewLine + " WHERE A." + fEXDATE + " = '" + strFrdt + "' ";
                    //    sql += Environment.NewLine + "   AND A.STEDT >='" + strFrdt + strFrStedt + "'";
                    //    sql += Environment.NewLine + "   AND A.STEDT <='" + strFrdt + strToStedt + "'";
                    //Else
                    //    sql += Environment.NewLine + " WHERE A." + fEXDATE + " >= '" + strFrdt + "' ";
                    //    sql += Environment.NewLine + "   AND A." + fEXDATE + " <= '" + strTODT + "' ";
                    //End If
                }
                if (rbQfy5.Checked == true)
                {
                    if (qfy51 != "")
                    {
                        sql += Environment.NewLine + "   AND (A.QFYCD='50' OR A.QFYCD='" + qfy51 + "')";
                        sql += Environment.NewLine + "   AND NOT (REPLACE(A.UNICD,' ','')='51' OR ISNULL(A.SJSDFG,'')='1' OR A.QFYCD='" + qfy51 + "')";
                    }
                    else
                    {
                        sql += Environment.NewLine + "   AND A.QFYCD='50'";
                        sql += Environment.NewLine + "   AND NOT (REPLACE(A.UNICD,' ','')='51' OR ISNULL(A.SJSDFG,'')='1')";
                    }
                }
                else if (rbQfy51.Checked == true)
                {
                    // 2006.01.12 NSK - 산재 판단기준 변경 UNICD=50 -> UNICD<>51
                    // 2018.02.27 WOOIL - SJSDFG가 '1'이면 후유임.
                    if (qfy51 != "")
                    {
                        sql += Environment.NewLine + "   AND (A.QFYCD='50' OR A.QFYCD='" + qfy51 + "')";
                        sql += Environment.NewLine + "   AND (REPLACE(A.UNICD,' ','')='51' OR ISNULL(A.SJSDFG,'')='1' OR A.QFYCD='" + qfy51 + "')";
                    }
                    else
                    {
                        sql += Environment.NewLine + "   AND A.QFYCD='50'";
                        sql += Environment.NewLine + "   AND (REPLACE(A.UNICD,' ','')='51' OR ISNULL(A.SJSDFG,'')='1')";
                    }
                }
                else if (rbQfy6.Checked == true)
                {
                    sql += Environment.NewLine + "   AND A.QFYCD in ('61','62','63','64','65','66','67','68','69')";
                }
                // 2008.03.31 WOOIL - 청구구분별 조회
                if (cboDemgb.SelectedIndex == 1)
                {
                    // 원청구 = 원청구+분리청구
                    sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(A.ADDZ1,'') IN ('','0','3') ";
                }
                else if (cboDemgb.SelectedIndex == 2)
                {
                    // 보완청구
                    sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(A.ADDZ1,'') IN ('1') ";
                }
                else if (cboDemgb.SelectedIndex == 3)
                {
                    // 추가청구
                    sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                    sql += Environment.NewLine + "   AND ISNULL(A.ADDZ1,'') IN ('2') ";
                }
                else
                {
                    // 전체조회
                    sql += Environment.NewLine + "   AND A.SIMCS>0 ";
                }
                sql += Environment.NewLine + "   AND ISNULL(A.DONFG,'')='Y' ";
                if (rbQfy5.Checked == true || rbQfy51.Checked == true)
                {
                    sql += Environment.NewLine + " ORDER BY A.EPRTNO,A.PID ";
                }
                else if (rbQfy6.Checked == true)
                {
                    if (rbIofg1.Checked == true)
                    {
                        sql += Environment.NewLine + " ORDER BY A.UNINM,A.PID ";
                    }
                    else if (rbIofg2.Checked == true)
                    {
                        sql += Environment.NewLine + " ORDER BY A.UNINM,A.EPRTNO,A.PID ";
                    }
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string eprtno = reader["EPRTNO"].ToString();
                    string pid = reader["PID"].ToString();
                    string unicd = reader["UNICD"].ToString();
                    string apprno = reader["APPRNO"].ToString();
                    string startdt = reader["STEDT"].ToString();
                    string enddt = reader["ENDDT"].ToString();
                    string ipwon = reader["IPWON"].ToString();
                    string tgwon = reader["TGWON"].ToString();
                    string resid = reader["RESID"].ToString();
                    long unamt = MetroLib.StrHelper.ToLong(reader["UNAMT"].ToString());

                    // 산재, 산재후유이면
                    if (rbQfy6.Checked == false)
                    {
                        if (rbIofg1.Checked == true)
                        {
                            // 외래
                            string[] aTgwon = (tgwon + "$").Split('$');
                            startdt = aTgwon[0];
                            enddt = aTgwon[1];
                        }
                        else
                        {
                            // 입원
                            string[] aIpwon = (ipwon + "$").Split('$');
                            startdt = aIpwon[0];
                            enddt = aIpwon[1];
                        }
                    }

                    // 자동차보험환자면 환자별로 모은다.
                    if (rbQfy6.Checked == true && rbIofg1.Checked == true)
                    {
                        eprtno = ""; // 줄번호를 없앤다.

                        bool find = false;
                        foreach (CData d in list_tmp)
                        {
                            if (d.PID == pid && d.UNICD == unicd && d.APPRNO == apprno)
                            {
                                if (startdt.CompareTo(d.STARTDT) < 0) d.STARTDT = startdt;
                                if (enddt.CompareTo(d.ENDDT) > 0) d.ENDDT = enddt;
                                d.UNAMT += unamt;
                                find = true;
                                break;
                            }
                        }
                        // 기존 내역에 합산했으면 다음 명세로로...
                        if (find) return MetroLib.SqlHelper.CONTINUE;
                    }

                    CData data = new CData();
                    data.Clear();
                    data.EPRTNO = eprtno;
                    data.PID = pid;
                    data.PNM = reader["PNM"].ToString();
                    data.STARTDT = startdt;
                    data.ENDDT = enddt;
                    data.UNAMT = unamt;
                    data.RESID = resid.Substring(0, 6) + "-" + resid.Substring(6);

                    if (rbQfy6.Checked == true)
                    {
                        data.UNICD = unicd;
                        data.APPRNO = apprno;
                        data.UNINM = reader["UNINM"].ToString();
                    }
                    else
                    {
                        data.GENDT = reader["GENDT"].ToString();
                        data.UNICD = apprno;
                        data.APPRNO = "";
                        data.UNINM = reader["MADDR"].ToString();
                    }

                    list_tmp.Add(data);

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }

            // 합계를 구한다.
            long tot_unamt = 0;
            long unicd_sum = 0; // 손보사별 소계(자보면)
            string bf_unicd = "";

            foreach (CData d in list_tmp)
            {
                // 손보사 소계
                if (rbQfy6.Checked == true)
                {
                    if (bf_unicd != "" && bf_unicd != d.UNICD)
                    {
                        CData unicd_tot = new CData();
                        unicd_tot.Clear();
                        unicd_tot.PNM = "손보사 소계";
                        unicd_tot.UNAMT = unicd_sum;
                        list.Add(unicd_tot);
                        unicd_sum = 0;
                    }
                    bf_unicd = d.UNICD;
                    unicd_sum += d.UNAMT;
                }

                // 합계
                tot_unamt += d.UNAMT;

                list.Add(d);
            }

            // 손보사 소계(자보인 경우)
            if (rbQfy6.Checked == true)
            {
                CData unicd_tot = new CData();
                unicd_tot.Clear();
                unicd_tot.PNM = "손보사 소계";
                unicd_tot.UNAMT = unicd_sum;
                list.Add(unicd_tot);
            }

            // 합계를 추가한다.
            CData tot = new CData();
            tot.Clear();
            tot.PNM = "합계";
            tot.UNAMT = tot_unamt;
            list.Add(tot);

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

        private void rbQfy5_CheckedChanged(object sender, EventArgs e)
        {
            QfyClick();
        }

        private void rbQfy51_CheckedChanged(object sender, EventArgs e)
        {
            QfyClick();
        }

        private void rbQfy6_CheckedChanged(object sender, EventArgs e)
        {
            QfyClick();
        }

        private void QfyClick()
        {
            if (rbQfy5.Checked == true || rbQfy51.Checked == true)
            {
                grdMainView.Columns["UNICD"].Caption = "사업장기호";
            }
            else if (rbQfy6.Checked == true)
            {
                grdMainView.Columns["UNICD"].Caption = "기관기호";
            }
            grdMain.DataSource = null;
            RefreshGridMain();
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
            string title = "";
            if (rbQfy5.Checked == true) title = "산재집계표";
            else if (rbQfy51.Checked == true) title = "산재후유집계표";
            else if (rbQfy6.Checked == true) title = "자보집계표"; ;

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string msg = rbIofg1.Checked ? "외래" : "입원";
            msg += " ";
            msg += txtFrdt.Text.ToString() + " - " + txtTodt.Text.ToString();
            msg += "(" + cboDemgb.Text + ")";
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
            e.Graph.DrawString("ADD00547R", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();

                if (rbQfy5.Checked == true) sfd.FileName = "산재집계표";
                else if (rbQfy51.Checked == true) sfd.FileName = "산재후유집계표";
                else if (rbQfy6.Checked == true) sfd.FileName = "자보집계표"; ;

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

        private void cboDemgb_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            RefreshGridMain();
        }
    }
}

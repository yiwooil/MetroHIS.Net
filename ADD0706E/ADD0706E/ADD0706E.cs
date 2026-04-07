using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADD0706E
{
    public partial class ADD0706E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool OnPgm = false;

        public ADD0706E()
        {
            InitializeComponent();
        }

        public ADD0706E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            ReadConfig();
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

        private void btnDemym_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDemym.Visible == true)
                {
                    lstDemym.Hide();
                }
                else
                {
                    this.SetDemymListBox();
                    lstDemym.Top = txtDemym.Top + txtDemym.Height;
                    lstDemym.Left = txtDemym.Left;
                    lstDemym.Show();
                    lstDemym.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDemymListBox()
        {
            lstDemym.Items.Clear();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT DEMYM";
                sql += System.Environment.NewLine + "  FROM TIE_H0801";
                sql += System.Environment.NewLine + " WHERE ISNULL(MULTIHSFG,'') = '" + m_HospMulti + "' ";
                sql += System.Environment.NewLine + " ORDER BY DEMYM DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    lstDemym.Items.Add(reader["DEMYM"].ToString());
                    return true;
                });

                conn.Close();
            }
        }

        private void lstDemym_DoubleClick(object sender, EventArgs e)
        {
            if (lstDemym.SelectedItem != null)
            {
                string demym = lstDemym.SelectedItem.ToString();
                lstDemym.Hide();
                Application.DoEvents();
                txtDemym.Text = demym;

            }
        }

        private void txtDemym_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ActionQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActionQuery()
        {
            try
            {
                String strDemym = txtDemym.Text.ToString();
                if (strDemym == "" || strDemym.Length < 10)
                {
                    txtHosid.Text = "";
                    txtDemnm.Text = "";
                    txtMemo.Text = "";
                    grdMain.DataSource = null;
                    grdSub.DataSource = null;
                    RefreshGridMain();
                    RefreshGridSub();
                    return;
                }
                Application.DoEvents();

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "조회 중입니다.");
                this.Query();
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "조회 중입니다.");
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

                string demym = txtDemym.Text.ToString();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT DEMSEQ,HOSID,DEMNM,ACPNM,DEMDD,TOTCNT,MEMO,SNDDT,YHGBN";
                sql += System.Environment.NewLine + "  FROM TIE_H0801";
                sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    txtHosid.Text = reader["HOSID"].ToString();
                    txtDemnm.Text = reader["DEMNM"].ToString();
                    txtAcpnm.Text = reader["ACPNM"].ToString();
                    txtDemdd.Text = reader["DEMDD"].ToString();
                    txtTotcnt.Text = reader["TOTCNT"].ToString();
                    txtMemo.Text = reader["MEMO"].ToString();
                    txtSenddt.Text = reader["SNDDT"].ToString();
                    if (reader["YHGBN"].ToString() == "2") rbYHgbn2.Checked = true;
                    else rbYHgbn1.Checked = true;

                    return true;
                });

                sql="";
                sql += System.Environment.NewLine + "SELECT PRODNO,PRODCM,ITEMCD,ITEMINFO,STDSIZE,UNIT,LINEFG,ADDAVR,FSTBUYFG";
                sql += System.Environment.NewLine + "  FROM TIE_H0802";
                sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
                sql += System.Environment.NewLine + " ORDER BY CONVERT(NUMERIC,PRODNO)";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.PRODNO = reader["PRODNO"].ToString();
                    data.PRODCM = reader["PRODCM"].ToString();
                    data.ITEMCD = reader["ITEMCD"].ToString();
                    data.ITEMINFO = reader["ITEMINFO"].ToString();
                    data.STDSIZE = reader["STDSIZE"].ToString();
                    data.UNIT = reader["UNIT"].ToString();
                    data.LINEFG = reader["LINEFG"].ToString();
                    data.ADDAVR = MetroLib.StrHelper.ToLong(reader["ADDAVR"].ToString());
                    data.FSTBUYFG = reader["FSTBUYFG"].ToString();

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

        private void RefreshGridSub()
        {
            if (grdSub.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                string demym = txtDemym.Text.ToString();
                string prodno =  grdMainView.GetFocusedRowCellValue("PRODNO").ToString();

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "조회 중입니다.");
                this.QuerySub(demym, prodno);
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void QuerySub(string demym, string prodno)
        {
            grdSub.DataSource = null;
            List<CDataSub> list = new List<CDataSub>();
            grdSub.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT BUSINESSCD, TRADENM, BUYDT, BUYQTY, BUYAMT, BUYTOTAMT, TRADEMEMO";
                sql += System.Environment.NewLine + "  FROM TIE_H0803";
                sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
                sql += System.Environment.NewLine + "   AND UNITFGNO = '" + prodno + "'";
                sql += System.Environment.NewLine + " ORDER BY ELINENO";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSub data = new CDataSub();
                    data.Clear();

                    data.BUSINESSCD = reader["BUSINESSCD"].ToString();
                    data.TRADENM = reader["TRADENM"].ToString();
                    data.BUYDT = reader["BUYDT"].ToString();
                    data.BUYQTY = MetroLib.StrHelper.ToDouble(reader["BUYQTY"].ToString());
                    data.BUYAMT = MetroLib.StrHelper.ToLong(reader["BUYAMT"].ToString());
                    data.BUYTOTAMT = MetroLib.StrHelper.ToLong(reader["BUYTOTAMT"].ToString());
                    data.TRADEMEMO = reader["TRADEMEMO"].ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridSub();
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDemym.Text.ToString() == "")
                {
                    MessageBox.Show("신청번호가 없습니다.");
                    return;
                }

                if (cboRcvid.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("수신인을 선택하세요");
                    return;
                }

                if (txtFolder.Text.ToString() == "")
                {
                    MessageBox.Show("폴더를 선택하세요");
                    return;
                }

                if (Directory.Exists(txtFolder.Text.ToString()) == false)
                {
                    Directory.CreateDirectory(txtFolder.Text.ToString());
                }

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeSamFile();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("생성이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeSamFile()
        {
            string demym = txtDemym.Text.ToString();
            string demdd = txtDemdd.Text.ToString();
            if (demdd.Length >= 6) demdd = demdd.Substring(0, 4) + "년 " + demdd.Substring(4) + "분기";

            DeleteAllFile();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                this.MakeH0801(demym, conn);
                this.MakeH0802(demym, conn);
                this.MakeH0803(demym, conn);
                this.MakeMedlogenc(demdd, conn);
            }
        }

        private void DeleteAllFile()
        {
            if (Directory.Exists(txtFolder.Text.ToString()))
            {
                string[] files = Directory.GetFiles(txtFolder.Text.ToString());
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string deletefile = txtFolder.Text.ToString() + "/" + fileName;
                    File.Delete(deletefile);
                }
            }
        }

        private void MakeH0801(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.1", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMYM,'H080' AS FMNO,HOSID,DEMNM,ACPNM,DEMDD,TOTCNT,MEMO";
            sql += System.Environment.NewLine + "  FROM TIE_H0801";
            sql += System.Environment.NewLine + " WHERE DEMYM='" + demym + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                sb.Length = 0;
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["FMNO"].ToString(), "C", 4));
                sb.Append(Conv(reader["HOSID"].ToString(), "C", 8));
                sb.Append(Conv(reader["DEMNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["ACPNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["DEMDD"].ToString(), "C", 6));
                sb.Append(Conv(reader["TOTCNT"].ToString(), "C", 5));
                sb.Append(Conv(reader["MEMO"].ToString(), "C", 1750));

                return true;
            });

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private void MakeH0802(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.2", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMYM,PRODNO,PRODCM,ITEMCD,ITEMINFO,STDSIZE,UNIT,LINEFG,ADDAVR,FSTBUYFG";
            sql += System.Environment.NewLine + "  FROM TIE_H0802 ";
            sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
            sql += System.Environment.NewLine + " ORDER BY CONVERT(NUMERIC,PRODNO)";

            int lineno = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                lineno++;
                sb.Length = 0;
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["PRODNO"].ToString(), "N", 5));
                sb.Append(Conv(reader["PRODCM"].ToString(), "C", 35));
                sb.Append(Conv(reader["ITEMCD"].ToString(), "C", 9));
                sb.Append(Conv(reader["ITEMINFO"].ToString(), "C", 140));
                sb.Append(Conv(reader["STDSIZE"].ToString(), "C", 140));
                sb.Append(Conv(reader["UNIT"].ToString(), "C", 140));
                sb.Append(Conv(reader["LINEFG"].ToString(), "C", 140));
                sb.Append(Conv(reader["ADDAVR"].ToString(), "N", 10));
                sb.Append(Conv(reader["FSTBUYFG"].ToString(), "C", 1));

                String strLine = sb.ToString();
                if (lineno > 1) sw.Write("\n");
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeH0803(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.3", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMYM,ELINENO,BUSINESSCD,TRADENM,BUYDT,BUYQTY * 100 AS BUYQTY100,BUYAMT,BUYTOTAMT,TRADEMEMO,UNITFGNO";
            sql += System.Environment.NewLine + "  FROM TIE_H0803 ";
            sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
            sql += System.Environment.NewLine + " ORDER BY CONVERT(NUMERIC,UNITFGNO), ELINENO";

            int lineno = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                long buyqty100 = MetroLib.StrHelper.ToLong(reader["BUYQTY100"].ToString());

                lineno++;
                sb.Length = 0;
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["ELINENO"].ToString(), "N", 5));
                sb.Append(Conv(reader["BUSINESSCD"].ToString(), "C", 17));
                sb.Append(Conv(reader["TRADENM"].ToString(), "C", 35));
                sb.Append(Conv(reader["BUYDT"].ToString(), "C", 8));
                sb.Append(Conv(buyqty100.ToString(), "N2", 10));
                sb.Append(Conv(reader["BUYAMT"].ToString(), "N", 10));
                sb.Append(Conv(reader["BUYTOTAMT"].ToString(), "N", 10));
                sb.Append(Conv(reader["TRADEMEMO"].ToString(), "C", 140));
                sb.Append(Conv(reader["UNITFGNO"].ToString(), "N", 5));

                String strLine = sb.ToString();
                if (lineno > 1) sw.Write("\n");
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeMedlogenc(string demdd, OleDbConnection p_conn)
        {
            String strRcvid = cboRcvid.SelectedItem.ToString();
            strRcvid = (strRcvid + "-").Split('-')[0];

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/MEDLOG.ENC", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            sb.Length = 0;
            sb.Append(Conv(strRcvid, "C", 12));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("NULL", "C", 8));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv(demdd + " 의약품구입내역 목록표", "C", 30));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("", "C", 12));

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private String Conv(String value, String Type, int Len)
        {
            if (Type == "N" || Type == "N2")
            {
                String ret = value.PadLeft(Len, '0');
                return MetroLib.StrHelper.LeftH(ret, Len);
            }
            else
            {
                String ret = value.PadRight(Len, ' ');
                return MetroLib.StrHelper.LeftH(ret, Len);
            }
        }

        private void btnMkPricd_Click(object sender, EventArgs e)
        {
            if (txtDemym.Text.ToString() == "") return;

            ADD0706E_1 f = new ADD0706E_1();
            f.m_demym = txtDemym.Text.ToString();
            f.m_demdd = txtDemdd.Text.ToString();
            f.m_User = m_User;
            f.ShowDialog(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDemym.Text.ToString() == "") return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
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

        private void Save()
        {
            string demym = txtDemym.Text.ToString();
            string senddt = txtSenddt.Text.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "UPDATE TIE_H0801";
                sql += System.Environment.NewLine + "   SET SNDDT = '" + senddt + "'";
                sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);
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
            String strTitle = "의약품구입내역목록표";

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
            String strCaption = "신청번호 : " + txtDemym.Text.ToString();
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
            e.Graph.DrawString("ADD0706E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void ADD0706E_Load(object sender, EventArgs e)
        {
            //ReadConfig();
        }

        private void ReadConfig()
        {
            OnPgm = true;

            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    txtHosid.Text = "";
                    txtAcpnm.Text = "";
                    txtDemnm.Text = "";

                    string sql = "";

                    // 요양기관기호
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A' ";
                    sql += Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + "   AND MST3CD = '2'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtHosid.Text = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 대표자
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A'";
                    sql += Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + "   AND MST3CD = '5'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtAcpnm.Text = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 작성자
                    sql = "";
                    sql += Environment.NewLine + "SELECT USRNM";
                    sql += Environment.NewLine + "  FROM TA94";
                    sql += Environment.NewLine + " WHERE USRID = '" + m_User + "'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtDemnm.Text = reader["USRNM"].ToString();
                        return true;
                    });

                    // 수신기관코드
                    cboRcvid.Items.Clear();
                    String strRcvid = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A'";
                    sql += Environment.NewLine + " AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + " AND MST3CD = '37'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        strRcvid = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 수신인 리스트
                    sql = "SELECT MST3CD, CDNM FROM TA88 WHERE MST1CD='A' AND MST2CD='EDIRCVID'";
                    int i = 0;
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        i++;
                        String mst3cd = reader["MST3CD"].ToString();
                        String cdnm = reader["CDNM"].ToString();
                        cboRcvid.Items.Add(mst3cd + "-" + cdnm);
                        if (mst3cd == strRcvid) cboRcvid.SelectedIndex = i - 1;
                        return MetroLib.SqlHelper.CONTINUE;
                    });

                    // 파일저장 폴더
                    RegistryKey reg;
                    reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.NET").CreateSubKey("ADD0706E");
                    String folder = reg.GetValue(txtFolder.Name, "").ToString();
                    if (folder == "") folder = "C:/hira/DDMD/sam/in";
                    txtFolder.Text = folder;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                OnPgm = false;
            }
        }

        //private void cboPosOption_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboPosOption.SelectedIndex == 1)
        //    {
        //        // 심평원
        //        txtFolder.Text = "C:/hira/DDMD/sam/in";
        //    }
        //    else if (cboPosOption.SelectedIndex == 2)
        //    {
        //        // KT
        //        txtFolder.Text = "C:/KTEDI/TR/Gen_in";
        //    }
        //    else
        //    {
        //        // 자동
        //        if (Directory.Exists("C:/KTEDI/TR/Gen_in") == true)
        //        {
        //            txtFolder.Text = "C:/KTEDI/TR/Gen_in";
        //        }
        //        else
        //        {
        //            txtFolder.Text = "C:/hira/DDMD/sam/in";
        //        }
        //    }
        //    Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0705E"); ;
        //    reg.SetValue(cboPosOption.Name, cboPosOption.SelectedIndex);
        //}

        //private void btnRcvid_Click(object sender, EventArgs e)
        //{
        //    if (panRcvid.Visible == true)
        //    {
        //        panRcvid.Visible = false;
        //    }
        //    else
        //    {
        //        panRcvid.Left = btnRcvid.Left - panRcvid.Width + btnRcvid.Width;
        //        panRcvid.Top = btnRcvid.Top - panRcvid.Height;
        //
        //        panRcvid.Visible = true;
        //    }
        //}

        //private void btnRcvidCancel_Click(object sender, EventArgs e)
        //{
        //    panRcvid.Visible = false;
        //}

        //private void btnRcvidSel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        String strRcvid = cboRcvid.SelectedItem.ToString();
        //        strRcvid = (strRcvid + "-").Split('-')[0];
        //
        //        string strConn = MetroLib.DBHelper.GetConnectionString();
        //        using (OleDbConnection conn = new OleDbConnection(strConn))
        //        {
        //            conn.Open();
        //
        //            string sql = "";
        //            sql += Environment.NewLine + "UPDATE TA88 SET FLD1QTY = '" + strRcvid + "' ";
        //            sql += Environment.NewLine + " WHERE MST1CD='A' ";
        //            sql += Environment.NewLine + "   AND MST2CD='HOSPITAL" + m_HospMulti + "' ";
        //            sql += Environment.NewLine + "   AND MST3CD='37' ";
        //
        //            MetroLib.SqlHelper.ExecuteSql(sql, conn);
        //        }
        //        txtRcvid.Text = strRcvid;
        //
        //        panRcvid.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtFolder.Text.ToString();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0706E");
                reg.SetValue(txtFolder.Name, txtFolder.Text.ToString());
            }
        }

        private void cboRcvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPgm) return;

            try
            {
                String strRcvid = cboRcvid.SelectedItem.ToString();
                strRcvid = (strRcvid + "-").Split('-')[0];

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TA88";
                    sql += System.Environment.NewLine + "   SET FLD1QTY=?";
                    sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                    sql += System.Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                    sql += System.Environment.NewLine + "   AND MST3CD='37'";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", strRcvid));

                        cmd.ExecuteNonQuery();
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

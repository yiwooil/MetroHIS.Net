using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0704E
{
    public partial class ADD0704E_2 : Form
    {
        private bool IsFirst = true;
        private bool IsFirstQuery = true;

        public string in_pcode = "";
        public string in_buydt = "";

        public string out_yn = "";
        public string out_pcode = "";
        public string out_iteminfo = "";
        public string out_prodcm = "";
        public string out_stdsize = "";
        public string out_unit = "";
        public string out_kumak = "";
        public string out_pricd = "";
        public string out_ipamt = "";
        

        public ADD0704E_2()
        {
            InitializeComponent();
        }

        private void ADD0704E_2_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            IsFirstQuery = true;
        }

        private void ADD0704E_2_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtEdicd.Text = in_pcode;
            txtBuydt.Text = in_buydt;

            btnQuery.PerformClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtEdicd.Text.ToString().Length < 2)
            {
                //MessageBox.Show("두 글자 이상 입력하세요.");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query();
                // 급여에 없으면 비급여를 자동으로 조회. 딱 한번만
                if (IsFirstQuery == true)
                {
                    if (grdTI09View.RowCount < 1)
                    {
                        chkByak.Checked = true;
                        this.Query();
                    }
                }
                IsFirstQuery = false;
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
            string itemcd = txtEdicd.Text.ToString();
            string buydt = txtBuydt.Text.ToString();

            grdTI09.DataSource = null;
            List<CDataTI09> list = new List<CDataTI09>();
            grdTI09.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                if (buydt == "") buydt = MetroLib.Util.GetSysDate(conn);

                string sql = "";

                if (chkByak.Checked)
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT I09.PCODE ";
                    sql += Environment.NewLine + "     , null as ADTDT ";
                    sql += Environment.NewLine + "     , I09.PCODENM ";
                    sql += Environment.NewLine + "     , I09.MKCNM";
                    sql += Environment.NewLine + "     , null AS MKCNMK";
                    sql += Environment.NewLine + "     , I09.PTYPE ";
                    sql += Environment.NewLine + "     , I09.PDUT ";
                    sql += Environment.NewLine + "     , null KUMAK ";
                    sql += Environment.NewLine + "     , null BUNCD ";
                    sql += Environment.NewLine + "     , ( SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "     , ( SELECT TOP 1 A02.IPAMT FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS IPAMT";
                    sql += Environment.NewLine + "  FROM TI09_BYAK I09 ";
                    sql += Environment.NewLine + " WHERE I09.PCODE LIKE '" + itemcd + "%'";
                    sql += Environment.NewLine + " ORDER BY I09.PCODE ";
                }
                else
                {
                    sql += Environment.NewLine + "SELECT I09.PCODE";
                    sql += Environment.NewLine + "     , I09.ADTDT";
                    sql += Environment.NewLine + "     , I09.PCODENM";
                    sql += Environment.NewLine + "     , I09.MKCNM";
                    sql += Environment.NewLine + "     , I09.MKCNMK";
                    sql += Environment.NewLine + "     , I09.PTYPE";
                    sql += Environment.NewLine + "     , I09.PDUT";
                    sql += Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN I09.KUMAK2 ELSE I09.KUMAK1 END AS KUMAK";
                    sql += Environment.NewLine + "     , ( SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "     , ( SELECT TOP 1 A02.IPAMT FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS IPAMT";
                    sql += Environment.NewLine + "  FROM TI09 I09";
                    sql += Environment.NewLine + " WHERE I09.PCODE LIKE '" + itemcd + "%'";
                    sql += Environment.NewLine + "   AND I09.ADTDT = (SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN AND X.ADTDT<='" + buydt + "')";
                    sql += Environment.NewLine + "   AND I09.GUBUN = '3'";
                    sql += Environment.NewLine + " ORDER BY I09.PCODE";
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataTI09 data = new CDataTI09();
                    data.Clear();

                    data.PCODE = reader["PCODE"].ToString();
                    data.ADTDT = reader["ADTDT"].ToString();
                    data.PCODENM = reader["PCODENM"].ToString();
                    data.MKCNM = reader["MKCNM"].ToString();
                    data.MKCNMK = reader["MKCNMK"].ToString();
                    data.PTYPE = reader["PTYPE"].ToString();
                    data.PDUT = reader["PDUT"].ToString();
                    data.KUMAK = reader["KUMAK"].ToString();
                    data.PRICD = reader["PRICD"].ToString();
                    data.IPAMT = reader["IPAMT"].ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridTI09();
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

        private void RefreshGridTI09()
        {
            if (grdTI09.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdTI09.BeginInvoke(new Action(() => grdTI09View.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdTI09View.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdTI09View_DoubleClick(object sender, EventArgs e)
        {
            List<CDataTI09> list = (List<CDataTI09>)grdTI09.DataSource;
            if (list == null) return;
            int row = grdTI09View.FocusedRowHandle;
            if (row < 0) return;

            out_yn = "Y";
            out_pcode = grdTI09View.GetRowCellValue(row, "PCODE").ToString();
            out_iteminfo = grdTI09View.GetRowCellValue(row, "PCODENM").ToString();
            out_prodcm = grdTI09View.GetRowCellValue(row, "MKCNM").ToString();
            out_stdsize = grdTI09View.GetRowCellValue(row, "PTYPE").ToString();
            out_unit = grdTI09View.GetRowCellValue(row, "PDUT").ToString();
            out_kumak = grdTI09View.GetRowCellValue(row, "KUMAK").ToString();
            out_pricd = grdTI09View.GetRowCellValue(row, "PRICD").ToString();
            out_ipamt = grdTI09View.GetRowCellValue(row, "IPAMT").ToString();

            this.Close();
        }
    }
}

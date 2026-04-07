using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0707E
{
    public partial class ADD0707E_1 : Form
    {
        public string m_in_pcode;
        public string m_in_buydt;
        public string m_in_demdivnm;

        public string m_out_pcode;
        public string m_out_pcodenm;
        public string m_out_mkcnm;
        public string m_out_mkcnmk;
        public string m_out_ptype;
        public string m_out_pdut;
        public string m_out_pricd;
        public string m_out_kumak;

        public bool m_out_sel;

        private bool IsFirst;

        public ADD0707E_1()
        {
            InitializeComponent();

            m_out_pcode = "";
            m_out_pcodenm = "";
            m_out_mkcnm = "";
            m_out_mkcnmk = "";
            m_out_ptype = "";
            m_out_pdut = "";
            m_out_pricd = "";
            m_out_kumak = "";

            m_out_sel = false;
        }

        private void ADD0707E_3_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0707E_3_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtEdicd.Text = m_in_pcode;
            btnQuery.PerformClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "조회 중입니다.");
                this.Query();
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;

                grdMain.Focus();
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
            List<CDataTI09> list = new List<CDataTI09>();
            grdMain.DataSource = list;

            string edicd = txtEdicd.Text.ToString();
            string buydt = m_in_buydt;

            if (edicd.Length < 2) return;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                if (buydt == "") buydt = MetroLib.Util.GetSysDate(conn);

                string sql = "";
                if (m_in_demdivnm == "비급여약제")
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT PCODE,PCODENM,MKCNM,'' AS MKCNMK,PTYPE,PDUT,'' AS ADTDT,0 AS KUMAK";
                    sql += Environment.NewLine + "     , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(EXPDT,'') ='' AND ISNULL(REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "  FROM TI09_BYAK I09";
                    sql += Environment.NewLine + " WHERE I09.PCODE LIKE '" + edicd + "%'";
                    sql += Environment.NewLine + " ORDER BY PCODE";
                }
                else
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT PCODE,PCODENM,MKCNM,MKCNMK,PTYPE,PDUT,ADTDT";
                    sql += Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN KUMAK2 ELSE KUMAK1 END AS KUMAK";
                    sql += Environment.NewLine + "     , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(EXPDT,'') ='' AND ISNULL(REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "  FROM TI09 I09";
                    sql += Environment.NewLine + " WHERE I09.PCODE LIKE '" + edicd + "%'";
                    sql += Environment.NewLine + "   AND I09.GUBUN = '2'";
                    sql += Environment.NewLine + "   AND I09.ADTDT = (SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN AND X.ADTDT<='" + buydt + "')";
                    sql += Environment.NewLine + " ORDER BY PCODE";
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataTI09 data = new CDataTI09();
                    data.Clear();

                    data.PCODE = reader["PCODE"].ToString();
                    data.ADTDT = reader["ADTDT"].ToString();
                    data.PCODENM = reader["PCODENM"].ToString().Replace("\r", "").Replace("\t", "");
                    data.MKCNM = reader["MKCNM"].ToString().Replace("\r", "").Replace("\t", "");
                    data.MKCNMK = reader["MKCNMK"].ToString().Replace("\r", "").Replace("\t", "");
                    data.PTYPE = reader["PTYPE"].ToString().Replace("\r", "").Replace("\t", "");
                    data.PDUT = reader["PDUT"].ToString().Replace("\r", "").Replace("\t", "");
                    data.KUMAK = MetroLib.StrHelper.ToLong(reader["KUMAK"].ToString());
                    data.PRICD = reader["PRICD"].ToString();// ReadTA02(data.PCODE, conn);

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }
        /*
        private string ReadTA02(string itemcd, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT TOP 1 A02.PRICD";
            sql += Environment.NewLine + "  FROM TA02 A02 (nolock)";
            sql += Environment.NewLine + " WHERE A02.ISPCD='" + itemcd + "'";
            sql += Environment.NewLine + "   AND A02.GUBUN='2'";
            sql += Environment.NewLine + " ORDER BY A02.CREDT DESC";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["PRICD"].ToString();
                return true;
            });
            return ret;
        }
        */
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            if (grdMainView.FocusedRowHandle < 0) return;
            SelectedRow(grdMainView.FocusedRowHandle);
        }

        private void SelectedRow(int rowHandle)
        {
            m_out_pcode = grdMainView.GetRowCellValue(rowHandle, "PCODE").ToString();
            m_out_pcodenm = grdMainView.GetRowCellValue(rowHandle, "PCODENM").ToString();
            m_out_mkcnm = grdMainView.GetRowCellValue(rowHandle, "MKCNM").ToString();
            m_out_mkcnmk = grdMainView.GetRowCellValue(rowHandle, "MKCNMK").ToString();
            m_out_ptype = grdMainView.GetRowCellValue(rowHandle, "PTYPE").ToString();
            m_out_pdut = grdMainView.GetRowCellValue(rowHandle, "PDUT").ToString();
            m_out_pricd = grdMainView.GetRowCellValue(rowHandle, "PRICD").ToString();
            m_out_kumak = grdMainView.GetRowCellValue(rowHandle, "KUMAK").ToString();

            m_out_sel = true;
            this.Close();
        }

        private void txtEdicd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnQuery.PerformClick();
            }
        }

        private void grdMainView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (grdMainView.FocusedRowHandle < 0) return;
                SelectedRow(grdMainView.FocusedRowHandle);
            }
        }
    }
}

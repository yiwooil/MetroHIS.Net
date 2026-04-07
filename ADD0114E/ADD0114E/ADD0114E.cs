using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0114E
{
    public partial class ADD0114E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool IsFirst;

        public ADD0114E()
        {
            InitializeComponent();
        }

        public ADD0114E(String user, String pwd, String prjcd, String addpara)
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
            if (m_Addpara == "") return;

            string[] para = m_Addpara.Split((char)21);

            string iofg = para[0];
            string qfydiv = para[1];
            string cnecno = para[2];
            string dcount = para[3];
            string demno = para[4];
            string demseq = para[5];
            string grpno = para[6];

            string F3tbl = "TIE_F0203_062";
            if (qfydiv == "3") F3tbl = "TIE_F0603_062";
            if (qfydiv == "6") F3tbl = "TIE_N0203"; //2013.07.04 KJW 자보추가

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                string sql = "";
                sql = "";
                if (iofg == "1")
                {
                    sql += Environment.NewLine + "SELECT I1A.SIMNO";
                    sql += Environment.NewLine + "     , I1A.PID";
                    sql += Environment.NewLine + "     , I1A.PNM";
                    sql += Environment.NewLine + "     , F3.CNECNO";
                    sql += Environment.NewLine + "     , F3.JJRMK";
                    sql += Environment.NewLine + "     , I1A.EXDATE AS K1";
                    sql += Environment.NewLine + "     , I1A.QFYCD AS K2";
                    sql += Environment.NewLine + "     , I1A.JRBY AS K3";
                    sql += Environment.NewLine + "     , I1A.PID AS K4";
                    sql += Environment.NewLine + "     , I1A.UNISQ AS K5";
                    sql += Environment.NewLine + "     , I1A.SIMCS AS K6";
                    sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TI1A X WHERE X.EXDATE=I1A.EXDATE AND X.QFYCD=I1A.QFYCD AND X.JRBY=I1A.JRBY AND X.PID=I1A.PID AND X.UNISQ=I1A.UNISQ AND X.SIMCS>=2) AS BO_CNT";
                    sql += Environment.NewLine + "  FROM " + F3tbl + " F3 inner join TI1A I1A ON I1A.DEMNO=F3.DEMNO AND I1A.EPRTNO=F3.EPRTNO";
                    sql += Environment.NewLine + " WHERE F3.DEMSEQ='" + demseq + "'";
                    sql += Environment.NewLine + "   AND F3.CNECNO='" + cnecno + "'";
                    sql += Environment.NewLine + "   AND F3.GRPNO='" + grpno + "'";
                    sql += Environment.NewLine + "   AND F3.DCOUNT='" + dcount + "'";
                    sql += Environment.NewLine + "   AND F3.DEMNO='" + demno + "'";
                    sql += Environment.NewLine + "   AND F3.LNO=0 ";
                    sql += Environment.NewLine + "   AND ISNUMERIC(F3.JJRMK)=1";    // 2008.07.24 WOOIL - 사유가 숫자이면 불능임.

                    if (qfydiv == "6")
                    {

                        sql += Environment.NewLine + " UNION ALL";
                        sql += Environment.NewLine + "SELECT I1A.SIMNO";
                        sql += Environment.NewLine + "     , I1A.PID";
                        sql += Environment.NewLine + "     , I1A.PNM";
                        sql += Environment.NewLine + "     , F3.CNECNO";
                        sql += Environment.NewLine + "     , F3.JJRMK";
                        sql += Environment.NewLine + "     , I1A.EXDATE AS K1";
                        sql += Environment.NewLine + "     , I1A.QFYCD AS K2";
                        sql += Environment.NewLine + "     , I1A.JRBY AS K3";
                        sql += Environment.NewLine + "     , I1A.PID AS K4";
                        sql += Environment.NewLine + "     , I1A.UNISQ AS K5";
                        sql += Environment.NewLine + "     , I1A.SIMCS AS K6";
                        sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TI1A X WHERE X.EXDATE=I1A.EXDATE AND X.QFYCD=I1A.QFYCD AND X.JRBY=I1A.JRBY AND X.PID=I1A.PID AND X.UNISQ=I1A.UNISQ AND X.SIMCS>=2) AS BO_CNT";
                        sql += Environment.NewLine + "  FROM " + F3tbl + " F3 inner join TI1A I1A ON I1A.DEMNO=F3.DEMNO AND I1A.EPRTNO=F3.EPRTNO";
                        sql += Environment.NewLine + " WHERE F3.DEMSEQ='" + demseq + "'";
                        sql += Environment.NewLine + "   AND F3.CNECNO='" + cnecno + "'";
                        sql += Environment.NewLine + "   AND F3.GRPNO='" + grpno + "'";
                        sql += Environment.NewLine + "   AND F3.DCOUNT='" + dcount + "'";
                        sql += Environment.NewLine + "   AND F3.DEMNO='" + demno + "'";
                        sql += Environment.NewLine + "   AND F3.LNO=0";
                        sql += Environment.NewLine + "   AND F3.JJRMK IN ('J1','J2','J3')";   // 사유가 J1,J2,J3이면 불능임.

                    }
                }
                else
                {
                    sql += Environment.NewLine + "SELECT I2A.SIMNO";
                    sql += Environment.NewLine + "     , I2A.PID";
                    sql += Environment.NewLine + "     , I2A.PNM";
                    sql += Environment.NewLine + "     , F3.CNECNO";
                    sql += Environment.NewLine + "     , F3.JJRMK";
                    sql += Environment.NewLine + "     , I2A.BDODT AS K1";
                    sql += Environment.NewLine + "     , I2A.QFYCD AS K2";
                    sql += Environment.NewLine + "     , I2A.JRBY AS K3";
                    sql += Environment.NewLine + "     , I2A.PID AS K4";
                    sql += Environment.NewLine + "     , I2A.UNISQ AS K5";
                    sql += Environment.NewLine + "     , I2A.SIMCS AS K6";
                    sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TI2A X WHERE X.BDODT=I2A.BDODT AND X.QFYCD=I2A.QFYCD AND X.JRBY=I2A.JRBY AND X.PID=I2A.PID AND X.UNISQ=I2A.UNISQ AND X.SIMCS>=2) AS BO_CNT";
                    sql += Environment.NewLine + "  FROM " + F3tbl + " F3 inner join TI2A I2A ON I2A.DEMNO=F3.DEMNO AND I2A.EPRTNO=F3.EPRTNO";
                    sql += Environment.NewLine + " WHERE F3.DEMSEQ='" + demseq + "'";
                    sql += Environment.NewLine + "   AND F3.CNECNO='" + cnecno + "'";
                    sql += Environment.NewLine + "   AND F3.GRPNO='" + grpno + "'";
                    sql += Environment.NewLine + "   AND F3.DCOUNT='" + dcount + "'";
                    sql += Environment.NewLine + "   AND F3.DEMNO='" + demno + "'";
                    sql += Environment.NewLine + "   AND F3.LNO=0";
                    sql += Environment.NewLine + "   AND ISNUMERIC(F3.JJRMK)=1";    // 2008.07.24 WOOIL - 사유가 숫자이면 불능임.

                    if (qfydiv == "6")
                    {

                        sql += Environment.NewLine + " UNION ALL";
                        sql += Environment.NewLine + "SELECT I2A.SIMNO";
                        sql += Environment.NewLine + "     , I2A.PID";
                        sql += Environment.NewLine + "     , I2A.PNM";
                        sql += Environment.NewLine + "     , F3.CNECNO";
                        sql += Environment.NewLine + "     , F3.JJRMK";
                        sql += Environment.NewLine + "     , I2A.BDODT AS K1";
                        sql += Environment.NewLine + "     , I2A.QFYCD AS K2";
                        sql += Environment.NewLine + "     , I2A.JRBY AS K3";
                        sql += Environment.NewLine + "     , I2A.PID AS K4";
                        sql += Environment.NewLine + "     , I2A.UNISQ AS K5";
                        sql += Environment.NewLine + "     , I2A.SIMCS AS K6";
                        sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM TI2A X WHERE X.BDODT=I2A.BDODT AND X.QFYCD=I2A.QFYCD AND X.JRBY=I2A.JRBY AND X.PID=I2A.PID AND X.UNISQ=I2A.UNISQ AND X.SIMCS>=2) AS BO_CNT";
                        sql += Environment.NewLine + "  FROM " + F3tbl + " F3 inner join TI2A I2A ON I2A.DEMNO=F3.DEMNO AND I2A.EPRTNO=F3.EPRTNO";
                        sql += Environment.NewLine + " WHERE F3.DEMSEQ='" + demseq + "'";
                        sql += Environment.NewLine + "   AND F3.CNECNO='" + cnecno + "'";
                        sql += Environment.NewLine + "   AND F3.GRPNO='" + grpno + "'";
                        sql += Environment.NewLine + "   AND F3.DCOUNT='" + dcount + "'";
                        sql += Environment.NewLine + "   AND F3.DEMNO='" + demno + "'";
                        sql += Environment.NewLine + "   AND F3.LNO=0";
                        sql += Environment.NewLine + "   AND F3.JJRMK IN ('J1','J2','J3')";    // 사유가 J1,J2,J3이면 불능임.

                    }
                }

                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.SIMNO = reader["SIMNO"].ToString().TrimEnd();
                    data.PID = reader["PID"].ToString().TrimEnd();
                    data.PNM = reader["PNM"].ToString().TrimEnd();
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd();
                    data.JJRMK = reader["JJRMK"].ToString().TrimEnd();
                    data.K1 = reader["K1"].ToString().TrimEnd();
                    data.K2 = reader["K2"].ToString().TrimEnd();
                    data.K3 = reader["K3"].ToString().TrimEnd();
                    data.K4 = reader["K4"].ToString().TrimEnd();
                    data.K5 = reader["K5"].ToString().TrimEnd();
                    data.K6 = reader["K6"].ToString().TrimEnd();
                    data.BO_CNT = MetroLib.StrHelper.ToLong(reader["BO_CNT"].ToString().TrimEnd());
                    data.IOFG = iofg;

                    data.SEL = data.BO_CNT <= 0;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                // 
                this.btnQuery.PerformClick();
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
            string bo_data = "";

            List<CData> list = (List<CData>)grdMain.DataSource;
            foreach (CData data in list)
            {
                if (data.SEL == false) continue;

                string rec = "";
                rec += data.IOFG + (char)21;
                rec += data.K1 + (char)21;
                rec += data.K2 + (char)21;
                rec += data.K3 + (char)21;
                rec += data.K4 + (char)21;
                rec += data.K5 + (char)21;
                rec += data.K6 + (char)21;
                rec += "" + (char)21;
                rec += data.JJRMK + (char)21;
                rec += data.CNECNO + (char)21;
                rec += data.PID + (char)21;
                rec += data.PNM + (char)21;
                rec += "1" + (char)21; // 보완청구 고정
                rec += data.SIMNO;

                if (bo_data == "") bo_data = rec;
                else bo_data += (char)22 + rec;
            }

            if(bo_data=="") return;

            string add_para = "user=" + m_User + ",pwd=" + m_Pwd + ",prjcd=" + m_Prjcd + ",addpara=" + bo_data;
            MetroLib.DLLHelper.LoadDLL("ADD0390E", "보완청구생성", add_para, true);
        }

        private void ADD0114E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0114E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            btnQuery.PerformClick();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdMainView.RowCount; i++)
            {
                grdMainView.SetRowCellValue(i, "SEL", chkAll.Checked);
            }
        }
    }
}

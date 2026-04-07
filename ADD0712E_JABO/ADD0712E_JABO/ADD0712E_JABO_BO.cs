using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0712E_JABO
{
    public partial class ADD0712E_JABO_BO : Form
    {
        public bool IsFirst;

        private string m_User;
        private string m_Pwd;
        private string m_Prjcd;

        private string m_IOFG;
        private string m_CNECNO;
        private string m_DCOUNT;
        private string m_DEMNO;
        private string m_DEMSEQ;
        private string m_GRPNO;

        public ADD0712E_JABO_BO()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            m_IOFG = "";
            m_CNECNO = "";
            m_DCOUNT = "";
            m_DEMNO = "";
            m_DEMSEQ = "";
            m_GRPNO = "";
        }

        public ADD0712E_JABO_BO(string p_user, string p_pwd, string p_prjcd, string p_iofg, string p_cnecno, string p_dcount, string p_demno, string p_demseq, string p_grpno)
            : this()
        {
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            m_IOFG = p_iofg;
            m_CNECNO = p_cnecno;
            m_DCOUNT = p_dcount;
            m_DEMNO = p_demno;
            m_DEMSEQ = p_demseq;
            m_GRPNO = p_grpno;
        }

        private void ADD0712E_JABO_BO_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0712E_JABO_BO_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            btnQuery.PerformClick();
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
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if(m_IOFG=="2"){
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            grdMain.DataSource = null;
            List<CDataBo> list = new List<CDataBo>();
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql="";
                sql = "";
                sql += Environment.NewLine + "SELECT A.PID";
                sql += Environment.NewLine + "     , A.PNM";
                sql += Environment.NewLine + "     , F3.CNECNO";
                sql += Environment.NewLine + "     , F3.JJRMK";
                sql += Environment.NewLine + "     , A." + fEXDATE + "";
                sql += Environment.NewLine + "     , A.QFYCD";
                sql += Environment.NewLine + "     , A.JRBY";
                sql += Environment.NewLine + "     , A.PID";
                sql += Environment.NewLine + "     , A.UNISQ";
                sql += Environment.NewLine + "     , A.SIMCS";
                sql += Environment.NewLine + "     , A.SIMNO";
                sql += Environment.NewLine + "     , (SELECT COUNT(*) FROM " + tTI1A + " X WHERE X." + fEXDATE + "=A." + fEXDATE + " AND X.QFYCD=A.QFYCD AND X.JRBY=A.JRBY AND X.PID=A.PID AND X.UNISQ=A.UNISQ AND X.SIMCS>=2) AS BO_CNT";
                sql += Environment.NewLine + "  FROM TIE_N0203 F3 INNER JOIN " + tTI1A + " A ON A.DEMNO=F3.DEMNO AND A.EPRTNO=F3.EPRTNO";
                sql += Environment.NewLine + " WHERE F3.DEMSEQ='" + m_DEMSEQ + "'";
                sql += Environment.NewLine + "   AND F3.CNECNO='" + m_CNECNO + "'";
                sql += Environment.NewLine + "   AND F3.GRPNO='" + m_GRPNO + "'";
                sql += Environment.NewLine + "   AND F3.DCOUNT='" + m_DCOUNT + "'";
                sql += Environment.NewLine + "   AND F3.DEMNO='" + m_DEMNO + "' ";
                sql += Environment.NewLine + "   AND F3.LNO=0";
                sql += Environment.NewLine + "   AND (ISNUMERIC(F3.JJRMK)=1";    // 2008.07.24 WOOIL - 사유가 숫자이면 불능임.
                sql += Environment.NewLine + "        OR F3.JJRMK IN ('J1','J2','J3'))"; // 사유가 J1,J2,J3이면 불능임.

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataBo data = new CDataBo();
                    data.Clear();

                    data.SEL = true;
                    data.PID = reader["PID"].ToString().TrimEnd();
                    data.PNM = reader["PNM"].ToString().TrimEnd();
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd();
                    data.JJRMK = reader["JJRMK"].ToString().TrimEnd();
                    data.BO_CNT = reader["BO_CNT"].ToString().TrimEnd();
                    data.EXDATE = reader[fEXDATE].ToString().TrimEnd();
                    data.QFYCD = reader["QFYCD"].ToString().TrimEnd();
                    data.JRBY = reader["JRBY"].ToString().TrimEnd();
                    data.UNISQ = reader["UNISQ"].ToString().TrimEnd();
                    data.SIMCS = reader["SIMCS"].ToString().TrimEnd();
                    data.SIMNO = reader["SIMNO"].ToString().TrimEnd();

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

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Make();
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

        private void Make()
        {
            string addz1 = "1";
            string para = "";
            for (int i = 0; i < grdMainView.RowCount; i++)
            {
                bool sel = (bool)grdMainView.GetRowCellValue(i, "SEL");
                if (sel == true)
                {
                    string rec = "";
                    rec += m_IOFG + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "EXDATE").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "QFYCD").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "JRBY").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "PID").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "UNISQ").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "SIMCS").ToString() + (char)21;
                    rec += "" + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "JJRMK").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "CNECNO").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "PID").ToString() + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "PNM").ToString() + (char)21;
                    rec += addz1 + (char)21;
                    rec += grdMainView.GetRowCellValue(i, "SIMNO").ToString();

                    if (para == "") para = rec;
                    else para += (char)22 + rec;
                }
            }

            if (para != "")
            {
                string addPara = "user=" + m_User + ",pwd=" + m_Pwd + ",prjcd=" + m_Prjcd + ",addpara=" + para;
                // 호출
                string exefile = "C:/Metro/DLL/ADD0390E.exe";
                string exefolder = "C:/Metro/DLL/";
                this.ExecCmd(exefile, exefolder, addPara);
            }
        }

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

    }
}

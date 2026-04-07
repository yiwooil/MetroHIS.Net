using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD8004Q
{
    public partial class ADD8004Q : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private Boolean IsFirst;

        public ADD8004Q()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            IsFirst = true;
        }

        public ADD8004Q(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD8004Q_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                this.QueryList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryList()
        {
            List<CListData> list = new List<CListData>();

            grdList.DataSource = null;
            grdList.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT REPYM, REPSEQ, FMNO, HOSID, DPTNM, MEMO";
            sql += System.Environment.NewLine + "  FROM TIE_I0601";
            sql += System.Environment.NewLine + " ORDER BY REPYM DESC,REPSEQ DESC";

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
                    CListData data = new CListData();
                    data.Clear();
                    data.REPYM = reader["REPYM"].ToString();
                    data.REPSEQ = reader["REPSEQ"].ToString();
                    data.FMNO = reader["FMNO"].ToString();
                    data.HOSID = reader["HOSID"].ToString();
                    data.DPTNM = reader["DPTNM"].ToString();
                    data.MEMO = reader["MEMO"].ToString();

                    list.Add(data);

                }
                reader.Close();

                conn.Close();
            }

            this.RefreshGridList();
        }

        private void RefreshGridList()
        {
            if (grdList.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdList.BeginInvoke(new Action(() => grdListView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdListView.RefreshData();
                Application.DoEvents();
            }
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

        private void grdListView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = grdListView.CalcHitInfo(ea.Location);
            if (info.RowHandle < 0) return;
            List<CListData> list = (List<CListData>)grdList.DataSource;
            CListData data = list[info.RowHandle];
            txtDptnm.Text = data.DPTNM;
            txtMemo.Text = data.MEMO;
            try
            {
                this.QueryData(data.REPYM, data.REPSEQ);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryData(String p_repym, String p_repseq)
        {
            List<CMainData> list = new List<CMainData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT I2.REPYM, I2.REPSEQ, I2.REQYM, I2.REQSEQ, I2.REPDIV, I2.ELINENO, I2.MKDIV, I2.INFODIV, I2.ITEMCD, I2.ITEMNM, I2.INFOSTMT, I2.REQSTMT, I2.BUYQTY, I3.REQDIV";
            sql += System.Environment.NewLine + "  FROM TIE_I0602 I2 INNER JOIN TIE_I0603 I3 ON I3.REPYM=I2.REPYM AND I3.REPSEQ=I2.REPSEQ AND I3.REQYM=I2.REQYM AND I3.REQSEQ=I2.REQSEQ AND I3.REPDIV=I2.REPDIV AND I3.ELINENO=I2.ELINENO ";
            sql += System.Environment.NewLine + " WHERE I2.REPYM='" + p_repym + "'";
            sql += System.Environment.NewLine + "   AND I2.REPSEQ=" + p_repseq + "";
            sql += System.Environment.NewLine + " ORDER BY I2.REPYM, I2.REPSEQ, I2.REQYM, I2.REQSEQ, I2.REPDIV, I2.ELINENO";

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
                    CMainData data = new CMainData();
                    data.Clear();
                    data.REPYM = reader["REPYM"].ToString();
                    data.REPSEQ = reader["REPSEQ"].ToString();
                    data.REQYM = reader["REQYM"].ToString();
                    data.REQSEQ = reader["REQSEQ"].ToString();
                    data.REPDIV = reader["REPDIV"].ToString();
                    data.ELINENO = reader["ELINENO"].ToString();
                    data.MKDIV = reader["MKDIV"].ToString();
                    data.INFODIV = reader["INFODIV"].ToString();
                    data.ITEMCD = reader["ITEMCD"].ToString();
                    data.ITEMNM = reader["ITEMNM"].ToString();
                    data.INFOSTMT = reader["INFOSTMT"].ToString();
                    data.REQSTMT = reader["REQSTMT"].ToString();
                    data.BUYQTY = reader["BUYQTY"].ToString();
                    data.REQDIV = reader["REQDIV"].ToString();

                    list.Add(data);

                }
                reader.Close();

                conn.Close();
            }

            this.RefreshGridMain();
        }
    }
}

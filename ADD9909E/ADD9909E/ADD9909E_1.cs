using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9909E
{
    public partial class ADD9909E_1 : Form
    {
        public bool m_IsSave;

        private bool IsFirst;

        public ADD9909E_1()
        {
            InitializeComponent();
        }

        private void ADD9909E_1_Load(object sender, EventArgs e)
        {
            m_IsSave = false;
            IsFirst = true;
        }

        private void ADD9909E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            DeptList();
            DoctList("");
        }

        private void DeptList()
        {
            List<CDept> list = new List<CDept>();

            grdDept.DataSource = null;
            grdDept.DataSource = list;

            try
            {
                CDept data = new CDept();
                data.DPTCD = "";
                data.DPTNM = "전체";
                list.Add(data);

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT DISTINCT A07.DPTCD,A09.DPTNM ";
                    sql += Environment.NewLine + "  FROM TA07 A07, TA09 A09 ";
                    sql += Environment.NewLine + " WHERE A07.DPTCD = A09.DPTCD ";
                    sql += Environment.NewLine + "   AND ISNULL(A07.EXPDT,'') = ''";
                    sql += Environment.NewLine + "   AND A07.DPTCD NOT IN ('DRG')";
                    sql += Environment.NewLine + " ORDER BY A07.DPTCD ";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            data = new CDept();
                            data.DPTCD = reader["DPTCD"].ToString();
                            data.DPTNM = reader["DPTNM"].ToString();
                            list.Add(data);
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                RefreshGridDept();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoctList(string p_dptcd)
        {
            List<CDoct> list = new List<CDoct>();

            grdDoct.DataSource = null;
            grdDoct.DataSource = list;

            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT A07.DRID, A07.DRNM, A07.DPTCD ";
                    sql += Environment.NewLine + "  FROM TA07 A07, TA09 A09 ";
                    sql += Environment.NewLine + " WHERE A07.DPTCD = A09.DPTCD ";
                    sql += Environment.NewLine + "   AND A07.DPTCD NOT IN ('DRG')";
                    if (chkDrExp.Checked == false)
                    {
                        sql += Environment.NewLine + "   AND ISNULL(A07.EXPDT,'') = ''";
                    }
                    if (p_dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND A07.DPTCD='" + p_dptcd + "'";
                    }
                    sql += Environment.NewLine + " ORDER BY A07.DRID ";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CDoct data = new CDoct();
                            data.DRID = reader["DRID"].ToString();
                            data.DRNM = reader["DRNM"].ToString();
                            data.DPTCD = reader["DPTCD"].ToString();

                            list.Add(data);
                        }
                        reader.Close();
                    }
                    conn.Close();
                }

                RefreshGridDoct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGridDept()
        {
            if (grdDept.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDept.BeginInvoke(new Action(() => grdDeptView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDeptView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridDoct()
        {
            if (grdDoct.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDoct.BeginInvoke(new Action(() => grdDoctView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDoctView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdDept_Click(object sender, EventArgs e)
        {
            string dptcd = grdDeptView.GetRowCellValue(grdDeptView.FocusedRowHandle, gcDPTCD).ToString();
            DoctList(dptcd);
        }

        private void chkDrExp_CheckedChanged(object sender, EventArgs e)
        {
            string dptcd = grdDeptView.GetRowCellValue(grdDeptView.FocusedRowHandle, gcDPTCD).ToString();
            DoctList(dptcd);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string drid = grdDoctView.GetRowCellValue(grdDoctView.FocusedRowHandle, gcDRID).ToString();
            string drnm = grdDoctView.GetRowCellValue(grdDoctView.FocusedRowHandle, gcDRNM).ToString();
            txtDrid.Text = drid;
            txtDrnm.Text = drnm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string drid = txtDrid.Text.ToString();
            string frdt = txtFrdt.Text.ToString();
            string todt = txtTodt.Text.ToString();
            string memo = txtMemo.Text.ToString();

            if (drid == "")
            {
                MessageBox.Show("의사를 선택하세요.");
                return;
            }
            if (frdt == "")
            {
                MessageBox.Show("시작일을 입력하세요.");
                return;
            }
            if (todt == "")
            {
                MessageBox.Show("종료일을 입력하세요.");
                return;
            }
            if (MetroLib.Util.ValDt(frdt) == false)
            {
                MessageBox.Show("시작일을 확인하세요.");
                return;
            }
            if (MetroLib.Util.ValDt(todt) == false)
            {
                MessageBox.Show("종료일을 확인하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                m_IsSave = true;
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
            string nhdid = "";
            string drid = txtDrid.Text.ToString();
            string frdt = txtFrdt.Text.ToString();
            string todt = txtTodt.Text.ToString();
            string memo = txtMemo.Text.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // HDID를 구한다.
                string sql = "";
                sql += System.Environment.NewLine + "SELECT NEWID() AS NHDID ";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) nhdid = reader["NHDID"].ToString();
                    reader.Close();
                }

                // 저장
                sql = "";
                sql += System.Environment.NewLine + "INSERT INTO TA07A(NHDID,DRID,FRDATE,TODATE,MEMO)";
                sql += System.Environment.NewLine + "VALUES(?,?,?,?,?)";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", nhdid));
                    cmd.Parameters.Add(new OleDbParameter("@2", drid));
                    cmd.Parameters.Add(new OleDbParameter("@3", frdt));
                    cmd.Parameters.Add(new OleDbParameter("@4", todt));
                    cmd.Parameters.Add(new OleDbParameter("@5", memo));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdDoct_DoubleClick(object sender, EventArgs e)
        {
            btnApply.PerformClick();
        }

    }
}

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
    public partial class ADD9909E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private bool IsFirst;

        private AutoCompleteStringCollection m_source = new AutoCompleteStringCollection();

        public ADD9909E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9909E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9909E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD9909E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            //Init();

            btnQuery.PerformClick();
        }

        private void Init()
        {
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT DRID, DRNM, DPTCD";
                    sql += Environment.NewLine + "  FROM TA07";
                    sql += Environment.NewLine + " ORDER BY DRID";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string drid = reader["DRID"].ToString();
                            string drnm = reader["DRNM"].ToString().Replace(" ", "_");
                            string dptcd = reader["DPTCD"].ToString();
                            m_source.Add(drid + " " + drnm + " " + dptcd);
                            m_source.Add(drnm + " " + dptcd + " " + drid);
                        }
                        reader.Close();
                    }
                    conn.Close();

                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                ;
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
            string drid = txtDrid.Text.ToString();
            string drnm = txtDrnm.Text.ToString();

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A.NHDID,A.DRID,A.FRDATE,A.TODATE,A.MEMO,B.DRNM ";
            sql += System.Environment.NewLine + "  FROM TA07A A INNER JOIN TA07 B ON B.DRID=A.DRID ";
            sql += System.Environment.NewLine + " WHERE A.DRID LIKE '" + drid + "%'";
            sql += System.Environment.NewLine + "   AND B.DRNM LIKE '" + drnm + "%'";
            sql += System.Environment.NewLine + " ORDER BY A.FRDATE DESC,A.DRID";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.DRID = reader["DRID"].ToString();
                        data.DRNM = reader["DRNM"].ToString();
                        data.FRDATE = reader["FRDATE"].ToString();
                        data.TODATE = reader["TODATE"].ToString();
                        data.MEMO = reader["MEMO"].ToString();
                        data.NHDID = reader["NHDID"].ToString();

                        list.Add(data);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            this.RefreshGridMain();
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

        private void grdMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (grdMainView.FocusedColumn == gcDRID)
            //{
            //    string nhdid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcNHDID).ToString();
            //    if (nhdid != "") e.Cancel = true;
            //}
        }

        private void grdMainView_ShownEditor(object sender, EventArgs e)
        {
        //    if (grdMainView.FocusedColumn == gcDRID)
        //    {
        //        // 해당 셀 위치에 TextBox를 띄운다.

        //        // 샐의 Rectangle을 찾는다.
        //        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)grdMainView.GetViewInfo();
        //        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo dridCell = viewInfo.GetGridCellInfo(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn);
        //        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo drnmCell = viewInfo.GetGridCellInfo(grdMainView.FocusedRowHandle, gcDRNM);
        //        Rectangle dridRec = dridCell.CellValueRect;
        //        Rectangle drnmRec = drnmCell.CellValueRect;

        //        // 텍스트박스의 위치과 크기를 설정한다.
        //        txtDrid.Top = dridRec.Top + grdMain.Top; ;
        //        txtDrid.Left = dridRec.Left + grdMain.Left;
        //        txtDrid.Width = dridRec.Width + drnmRec.Width;
        //        txtDrid.Height = dridRec.Height;

        //        // 화면에 보여준다.
        //        txtDrid.Show();
        //        txtDrid.BringToFront();
        //        txtDrid.Focus();
        //    }
        }


        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                string nhdid = grdMainView.GetRowCellValue(view.FocusedRowHandle, gcNHDID).ToString();
                string drid = grdMainView.GetRowCellValue(view.FocusedRowHandle, gcDRID).ToString();
                string frdate = grdMainView.GetRowCellValue(view.FocusedRowHandle, gcFRDATE).ToString();
                string todate = grdMainView.GetRowCellValue(view.FocusedRowHandle, gcTODATE).ToString();
                string memo = grdMainView.GetRowCellValue(view.FocusedRowHandle, gcMEMO).ToString();

                if (view.FocusedColumn == gcFRDATE)
                {
                    frdate = e.Value.ToString();
                }
                else if (view.FocusedColumn == gcTODATE)
                {
                    todate = e.Value.ToString();
                }
                else if (view.FocusedColumn == gcMEMO)
                {
                    memo = e.Value.ToString();
                }
                // 값을 저장한다.
                if (nhdid != "")
                {
                    if (CheckInputvalue(frdate, todate) == false) return;
                    Upd(nhdid, frdate, todate, memo);
                }
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckInputvalue(string p_frdate, string p_todate)
        {
            if (p_frdate == "") return false;
            if (p_todate == "") return false;
            if (MetroLib.Util.ValDt(p_frdate) == false) return false;
            if (MetroLib.Util.ValDt(p_todate) == false) return false;

            return true;
        }

        private void Upd(string p_nhdid, string p_frdate, string p_todate, string p_memo)
        {

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "UPDATE TA07A";
                sql += System.Environment.NewLine + "   SET FRDATE=?";
                sql += System.Environment.NewLine + "     , TODATE=?";
                sql += System.Environment.NewLine + "     , MEMO=?";
                sql += System.Environment.NewLine + " WHERE NHDID=?";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_frdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", p_todate));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_memo));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_nhdid));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.DelLine();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void DelLine()
        {
            string nhdid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcNHDID).ToString();
            string drid = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDRID).ToString();
            string drnm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDRNM).ToString();
            string frdate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcFRDATE).ToString();
            string todate = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcTODATE).ToString();

            string msg = "";
            msg += "의사 : " + drid + " " + drnm + Environment.NewLine;
            msg += "기간 : " + frdate + " - " + todate + Environment.NewLine;

            if (MessageBox.Show(msg + " 를 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                DelTA07A(nhdid);
                grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelTA07A(string p_nhdid)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "DELETE FROM TA07A";
                sql += System.Environment.NewLine + " WHERE NHDID=?";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_nhdid));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ADD9909E_1 f = new ADD9909E_1();
            f.ShowDialog(this);
            bool isSave = f.m_IsSave;
            f = null;
            if (isSave) btnQuery.PerformClick();
        }

    }
}

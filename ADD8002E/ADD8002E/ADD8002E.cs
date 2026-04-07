using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD8002E
{
    public partial class ADD8002E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD8002E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD8002E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void btnReqno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstReqno.Visible == true)
                {
                    lstReqno.Hide();
                }
                else
                {
                    this.SetReqnoListBox();
                    lstReqno.Top = txtReqno.Top + txtReqno.Height;
                    lstReqno.Left = txtReqno.Left;
                    lstReqno.Show();
                    lstReqno.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetReqnoListBox()
        {
            lstReqno.Items.Clear();

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT REQNO,EMPID,CODEGB,REQDIV,COMPLDT";
            sql += System.Environment.NewLine + "  FROM TIE_M0501";
            sql += System.Environment.NewLine + " ORDER BY REQNO DESC";

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
                        lstReqno.Items.Add(reader["REQNO"].ToString());
                    }
                    reader.Close();
                }

                conn.Close();
            }
        }

        private void lstReqno_DoubleClick(object sender, EventArgs e)
        {
            if (lstReqno.SelectedItem != null)
            {
                txtReqno.Text = lstReqno.SelectedItem.ToString();
                lstReqno.Hide();

            }
        }

        private void txtReqno_TextChanged(object sender, EventArgs e)
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
                String strReqno = txtReqno.Text.ToString();
                if (strReqno == "" || strReqno.Length < 10)
                {
                    txtFmno.Text = "";
                    txtHosid.Text = "";
                    txtDdnm.Text = "";
                    txtMemo.Text = "";
                    grdMain.DataSource = null;

                    return;
                }

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
            String reqno = txtReqno.Text.ToString();

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT FMNO,HOSID,DDNM,BUYREQTOT,MEMO,EMPID,SYSDT,SYSTM,COMPLDT,CODEGB,REQDIV";
                sql += System.Environment.NewLine + "  FROM TIE_M0501";
                sql += System.Environment.NewLine + " WHERE REQNO=?";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", reqno));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFmno.Text = reader["FMNO"].ToString();
                        txtHosid.Text = reader["HOSID"].ToString();
                        txtDdnm.Text = reader["DDNM"].ToString();
                        txtScnt.Text = reader["BUYREQTOT"].ToString();
                        txtMemo.Text = reader["MEMO"].ToString();
                        String strReqdiv = reader["REQDIV"].ToString();
                        if (strReqdiv == "A") cboReqdiv.SelectedIndex = 0;
                        else if (strReqdiv == "B") cboReqdiv.SelectedIndex = 1;
                        else cboReqdiv.SelectedIndex = -1;
                        String strCodegb = reader["CODEGB"].ToString();
                        if (strCodegb == "2") cboCodegb.SelectedIndex = 0;
                        else if (strCodegb == "3") cboCodegb.SelectedIndex = 1;
                        else if (strCodegb == "4") cboCodegb.SelectedIndex = 2;
                        else if (strCodegb == "5") cboCodegb.SelectedIndex = 3;
                        else cboCodegb.SelectedIndex = -1;
                    }
                    reader.Close();
                }

                sql = "";
                sql += System.Environment.NewLine + "SELECT REQDIV,ITEMCD,ITEMINFO,STDSIZE,UNIT,BUSINESSCD,TRADENM,PRESNDDIV,BUYDT,BUYQTY,BUYAMT,UNITCOST";
                sql += System.Environment.NewLine + "  FROM TIE_M0502";
                sql += System.Environment.NewLine + " WHERE REQNO=?";
                sql += System.Environment.NewLine + " ORDER BY ELINENO";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", reqno));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();

                        data.SEL = true;
                        data.REQDIV = reader["REQDIV"].ToString();
                        data.ITEMCD = reader["ITEMCD"].ToString();
                        data.ITEMINFO = reader["ITEMINFO"].ToString();
                        data.STDSIZE = reader["STDSIZE"].ToString();
                        data.UNIT = reader["UNIT"].ToString();
                        data.BUSINESSCD = reader["BUSINESSCD"].ToString();
                        data.TRADENM = reader["TRADENM"].ToString();
                        data.PRESNDDIV = reader["PRESNDDIV"].ToString();
                        data.BUYDT = reader["BUYDT"].ToString();
                        data.BUYQTY = reader["BUYQTY"].ToString();
                        data.BUYAMT = reader["BUYAMT"].ToString();
                        data.UNITCOST = reader["UNITCOST"].ToString();

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

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                List<CData> list = (List<CData>)grdMain.DataSource;
                CData data = new CData();
                data.Clear();
                list.Add(data);
                grdMainView.FocusedRowHandle = list.Count - 1;
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.FocusedRowHandle < 0) return;
                List<CData> list = (List<CData>)grdMain.DataSource;
                list.RemoveAt(grdMainView.FocusedRowHandle);
                RefreshGridMain();
                txtScnt.Text = list.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewReqno_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.NewReqno();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                this.btnAddRow.PerformClick();

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void NewReqno()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                txtFmno.Text = "M050";

                // HOSID 읽기
                string sql = "";
                sql += System.Environment.NewLine + "SELECT FLD2QTY ";
                sql += System.Environment.NewLine + "  FROM TA88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD ='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD = 'HOSPITAL' ";
                sql += System.Environment.NewLine + "   AND MST3CD = '2'";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtHosid.Text = reader["FLD2QTY"].ToString();
                    }
                    reader.Close();
                }

                // DDNM 읽기
                sql = "";
                sql += System.Environment.NewLine + "SELECT FLD3QTY ";
                sql += System.Environment.NewLine + "  FROM TA88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD ='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD = 'HOSPITAL' ";
                sql += System.Environment.NewLine + "   AND MST3CD = '48'";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtDdnm.Text = reader["FLD3QTY"].ToString();
                    }
                    reader.Close();
                }


                // REQNO 만들기
                String strReqno = "";
                String strSysdt = MetroLib.Util.GetSysDate(conn).Substring(2); // YYMMDD

                sql = "";
                sql += System.Environment.NewLine + "SELECT ISNULL(MAX(REQNO),'') MAX_REQNO";
                sql += System.Environment.NewLine + "  FROM TIE_M0501";
                sql += System.Environment.NewLine + " WHERE REQNO LIKE ?";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", strSysdt + "%"));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        strReqno = reader["MAX_REQNO"].ToString();
                    }
                    reader.Close();
                }

                if (strReqno == "") strReqno = strSysdt + "0000";
                int intReqno = 0;
                int.TryParse(strReqno, out intReqno);
                intReqno++;

                txtReqno.Text = intReqno.ToString();

                cboReqdiv.SelectedIndex = 0;
                cboCodegb.SelectedIndex = 0;
            }
        }

        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (view.FocusedColumn.FieldName == "ITEMCD")
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.ShowProgressForm("", "조회 중입니다.");
                    this.SetIteminfo(view.FocusedRowHandle, e.Value as String);
                    this.CloseProgressForm("", "조회 중입니다.");
                    Cursor.Current = Cursors.Default;

                    this.RefreshGridMain();
                }
                catch (Exception ex)
                {
                    this.CloseProgressForm("", "조회 중입니다.");
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SetIteminfo(int row, String itemcd)
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            int ta02_count = 0;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TA02 읽기
                string sql = "";
                sql += System.Environment.NewLine + "SELECT TOP 1 ISPCD ";
                sql += System.Environment.NewLine + "  FROM TA02 ";
                sql += System.Environment.NewLine + " WHERE ISPCD=?";
                sql += System.Environment.NewLine + "   AND GUBUN IN ('2','3')";
                sql += System.Environment.NewLine + " ORDER BY CREDT DESC";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", itemcd));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ta02_count++;
                    }
                    reader.Close();
                }

                if (ta02_count > 0)
                {
                    // TI09 읽기
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT TOP 1 PCODENM,MKCNM,PTYPE,PDUT,KUMAK ";
                    sql += System.Environment.NewLine + "  FROM TI09 ";
                    sql += System.Environment.NewLine + " WHERE PCODE =? ";
                    sql += System.Environment.NewLine + "   AND GUBUN IN ('2','3')";
                    sql += System.Environment.NewLine + " ORDER BY ADTDT DESC";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", itemcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            list[row].SEL = true;
                            list[row].ITEMINFO1 = reader["PCODENM"].ToString();
                            list[row].ITEMINFO2 = reader["MKCNM"].ToString();
                            list[row].STDSIZE = reader["PTYPE"].ToString();
                            list[row].UNIT = reader["PDUT"].ToString();
                            list[row].UNITCOST = reader["KUMAK"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
        }

        private void btnSaveReqno_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "저장 중입니다.");
                this.Save();
                this.CloseProgressForm("", "저장 중입니다.");
                Cursor.Current = Cursors.Default;

                this.ActionQuery();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "저장 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Save()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    if (IsM0501(conn, tran))
                    {
                        UpdM0501(conn, tran);
                    }
                    else
                    {
                        InsM0501(conn, tran);
                    }
                    DelM0502(conn, tran);
                    InsM0502(conn, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private Boolean IsM0501(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strCount = "0";

            string sql = "";
            sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
            sql += System.Environment.NewLine + "  FROM TIE_M0501";
            sql += System.Environment.NewLine + " WHERE REQNO=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strCount = reader["CNT"].ToString();
                }
                reader.Close();
            }
            int count = 0;
            int.TryParse(strCount, out count);

            return count > 0;
        }

        private void InsM0501(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strSysdt = MetroLib.Util.GetSysDate(p_conn, p_tran);
            String strSystm = MetroLib.Util.GetSysTime(p_conn, p_tran);

            String strCodegb = "";
            if (cboCodegb.SelectedIndex == 0) strCodegb = "2";
            else if (cboCodegb.SelectedIndex == 1) strCodegb = "3";
            else if (cboCodegb.SelectedIndex == 2) strCodegb = "4";
            else if (cboCodegb.SelectedIndex == 3) strCodegb = "5";
            else strCodegb = "";

            String strReqdiv = "";
            if (cboReqdiv.SelectedIndex == 0) strReqdiv = "A";
            else if (cboReqdiv.SelectedIndex == 1) strReqdiv = "B";
            else strReqdiv = "";

            string sql = "";
            sql += System.Environment.NewLine + "INSERT TIE_M0501(REQNO, FMNO, HOSID, DDNM, BUYREQTOT, MEMO, EMPID, SYSDT, SYSTM, CODEGB, REQDIV)";
            sql += System.Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtFmno.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtHosid.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P4", txtDdnm.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P5", "0"));
                cmd.Parameters.Add(new OleDbParameter("@P6", txtMemo.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P7", m_User));
                cmd.Parameters.Add(new OleDbParameter("@P8", strSysdt));
                cmd.Parameters.Add(new OleDbParameter("@P9", strSystm));
                cmd.Parameters.Add(new OleDbParameter("@P10", strCodegb));
                cmd.Parameters.Add(new OleDbParameter("@P11", strReqdiv));

                cmd.ExecuteNonQuery();
            }
        }

        private void UpdM0501(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strSysdt = MetroLib.Util.GetSysDate(p_conn, p_tran);
            String strSystm = MetroLib.Util.GetSysTime(p_conn, p_tran);

            String strCodegb = "";
            if (cboCodegb.SelectedIndex == 0) strCodegb = "2";
            else if (cboCodegb.SelectedIndex == 1) strCodegb = "3";
            else if (cboCodegb.SelectedIndex == 2) strCodegb = "4";
            else if (cboCodegb.SelectedIndex == 3) strCodegb = "5";
            else strCodegb = "";

            String strReqdiv = "";
            if (cboReqdiv.SelectedIndex == 0) strReqdiv = "A";
            else if (cboReqdiv.SelectedIndex == 1) strReqdiv = "B";
            else strReqdiv = "";

            string sql = "";
            sql += System.Environment.NewLine + "UPDATE TIE_M0501";
            sql += System.Environment.NewLine + "   SET FMNO=?";
            sql += System.Environment.NewLine + "     , HOSID=?";
            sql += System.Environment.NewLine + "     , DDNM=?";
            sql += System.Environment.NewLine + "     , BUYREQTOT=?";
            sql += System.Environment.NewLine + "     , MEMO=?";
            sql += System.Environment.NewLine + "     , EMPID=?";
            sql += System.Environment.NewLine + "     , SYSDT=?";
            sql += System.Environment.NewLine + "     , SYSTM=?";
            sql += System.Environment.NewLine + "     , CODEGB=?";
            sql += System.Environment.NewLine + "     , REQDIV=?";
            sql += System.Environment.NewLine + " WHERE REQNO=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtFmno.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtHosid.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtDdnm.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P4", txtScnt.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P5", txtMemo.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P6", m_User));
                cmd.Parameters.Add(new OleDbParameter("@P7", strSysdt));
                cmd.Parameters.Add(new OleDbParameter("@P8", strSystm));
                cmd.Parameters.Add(new OleDbParameter("@P9", strCodegb));
                cmd.Parameters.Add(new OleDbParameter("@P10", strReqdiv));
                cmd.Parameters.Add(new OleDbParameter("@P11", txtReqno.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void DelM0502(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql += System.Environment.NewLine + "DELETE";
            sql += System.Environment.NewLine + "  FROM TIE_M0502";
            sql += System.Environment.NewLine + " WHERE REQNO=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void InsM0502(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strReqdiv = "";
            if (cboReqdiv.SelectedIndex == 0) strReqdiv = "A";
            else if (cboReqdiv.SelectedIndex == 1) strReqdiv = "B";
            else strReqdiv = "";

            int seq = 0;
            List<CData> list = (List<CData>)grdMain.DataSource;

            // TSQL문장과 Connection 객체를 지정   
            string sql = "";
            sql += System.Environment.NewLine + "INSERT TIE_M0502(REQNO, ELINENO, REQDIV, ITEMCD, ITEMINFO, STDSIZE, UNIT, BUSINESSCD, TRADENM, PRESNDDIV, BUYDT, BUYQTY, BUYAMT, UNITCOST)";
            sql += System.Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                foreach (CData data in list)
                {
                    if (data.SEL)
                    {
                        cmd.Parameters.Clear();

                        seq++;
                        cmd.Parameters.Add(new OleDbParameter("@P1", txtReqno.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@P4", seq));
                        cmd.Parameters.Add(new OleDbParameter("@P5", strReqdiv));
                        cmd.Parameters.Add(new OleDbParameter("@P6", data.ITEMCD));
                        cmd.Parameters.Add(new OleDbParameter("@P7", data.ITEMINFO));
                        cmd.Parameters.Add(new OleDbParameter("@P8", data.STDSIZE));
                        cmd.Parameters.Add(new OleDbParameter("@P9", data.UNIT));
                        cmd.Parameters.Add(new OleDbParameter("@P10", data.BUSINESSCD));
                        cmd.Parameters.Add(new OleDbParameter("@P11", data.TRADENM));
                        cmd.Parameters.Add(new OleDbParameter("@P12", data.PRESNDDIV));
                        cmd.Parameters.Add(new OleDbParameter("@P13", data.BUYDT));
                        cmd.Parameters.Add(new OleDbParameter("@P14", data.BUYQTY_SAVE));
                        cmd.Parameters.Add(new OleDbParameter("@P15", data.BUYAMT_SAVE));
                        cmd.Parameters.Add(new OleDbParameter("@P16", data.UNITCOST_SAVE));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            // 선고건수 UPDATE
            sql = "";
            sql += System.Environment.NewLine + "UPDATE TIE_M0501";
            sql += System.Environment.NewLine + "   SET BUYREQTOT=?";
            sql += System.Environment.NewLine + " WHERE REQNO=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", seq));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqno.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void btnDelReqno_Click(object sender, EventArgs e)
        {
            try
            {
                String strReqno = txtReqno.Text.ToString();
                if (strReqno == "") return;
                if (MessageBox.Show(strReqno + " 를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Del();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                txtReqno.Text = "";
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Del()
        {
            String reqno = txtReqno.Text.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    // TIE_M0502 삭제
                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0502";
                    sql += System.Environment.NewLine + " WHERE REQNO=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqno));

                        cmd.ExecuteNonQuery();
                    }

                    // TIE_M0501 삭제
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0501";
                    sql += System.Environment.NewLine + " WHERE REQNO=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqno));

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            ADD8002E_1 f = new ADD8002E_1();
            f.m_reqno = txtReqno.Text.ToString();
            f.ShowDialog(this);
        }

    }
}

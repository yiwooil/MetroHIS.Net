using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD8003E
{
    public partial class ADD8003E_1 : Form
    {
        public String m_reqym;
        public String m_reqseq;
        public String m_elineseq;

        public Boolean m_saved;

        private Boolean IsFirst;

        public ADD8003E_1()
        {
            InitializeComponent();
        }

        private void ADD8003E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_saved = false;
        }

        private void ADD8003E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
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
            txtReqym.Text = m_reqym;
            txtReqseq.Text = m_reqseq;
            txtElineseq.Text = m_elineseq;

            List<CData_1> list = new List<CData_1>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            if (m_elineseq == "") return;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT MKDIV,PHADIV,DRGEFFKND,ITEMCD,ITEMNM,WRITEDT,DEMAMT,STDSIZE,UNIT,APPLDT,DRGEFF,DOESQY";
                sql += System.Environment.NewLine + "  FROM TIE_M0602";
                sql += System.Environment.NewLine + " WHERE REQYM='" + m_reqym + "'";
                sql += System.Environment.NewLine + "   AND REQSEQ=" + m_reqseq + "";
                sql += System.Environment.NewLine + "   AND ELINESEQ=" + m_elineseq + "";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtDrgeffknd.Text = reader["DRGEFFKND"].ToString();
                        txtItemcd.Text = reader["ITEMCD"].ToString();
                        txtItemnm.Text = reader["ITEMNM"].ToString();
                        txtWritedt.Text = reader["WRITEDT"].ToString();
                        txtDemamt.Text = reader["DEMAMT"].ToString();
                        txtStdsize.Text = reader["STDSIZE"].ToString();
                        txtUnit.Text = reader["UNIT"].ToString();
                        txtAppldt.Text = reader["APPLDT"].ToString();
                        txtDrgeff.Text = reader["DRGEFF"].ToString();
                        txtDoesqy.Text = reader["DOESQY"].ToString();

                        String strMkdiv = reader["MKDIV"].ToString();
                        if (strMkdiv == "1") cboMkdiv.SelectedIndex = 0;
                        else if (strMkdiv == "2") cboMkdiv.SelectedIndex = 1;
                        else cboMkdiv.SelectedIndex = -1;

                        String strPhadiv = reader["PHADIV"].ToString();
                        if (strPhadiv == "1") cboPhadiv.SelectedIndex = 0;
                        else if (strPhadiv == "2") cboPhadiv.SelectedIndex = 1;
                        else if (strPhadiv == "3") cboPhadiv.SelectedIndex = 2;
                        else cboMkdiv.SelectedIndex = -1;
                    }
                    reader.Close();
                }

                sql = "";
                sql += System.Environment.NewLine + "SELECT GURESID,GUNM,CODEDIV,DRGCD,DRGNM,STDSZCD,CTNUT,BUYDT,CTNUTAMT,DRGQTY,QTYCTNUT,QTYAMT";
                sql += System.Environment.NewLine + "  FROM TIE_M0603";
                sql += System.Environment.NewLine + " WHERE REQYM='" + m_reqym + "'";
                sql += System.Environment.NewLine + "   AND REQSEQ=" + m_reqseq + "";
                sql += System.Environment.NewLine + "   AND ELINESEQ=" + m_elineseq + "";
                sql += System.Environment.NewLine + " ORDER BY SEQ";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData_1 data = new CData_1();
                        data.Clear();

                        data.GURESID = reader["GURESID"].ToString();
                        data.GUNM = reader["GUNM"].ToString();
                        data.CODEDIV = reader["CODEDIV"].ToString();
                        data.DRGCD = reader["DRGCD"].ToString();
                        data.DRGNM = reader["DRGNM"].ToString();
                        data.STDSZCD = reader["STDSZCD"].ToString();
                        data.CTNUT = reader["CTNUT"].ToString();
                        data.BUYDT = reader["BUYDT"].ToString();
                        data.CTNUTAMT = reader["CTNUTAMT"].ToString();
                        data.DRGQTY = reader["DRGQTY"].ToString();
                        data.QTYCTNUT = reader["QTYCTNUT"].ToString();
                        data.QTYAMT = reader["QTYAMT"].ToString();

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
                List<CData_1> list = (List<CData_1>)grdMain.DataSource;
                CData_1 data = new CData_1();
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
            try{
                if (grdMainView.FocusedRowHandle < 0) return;
                List<CData_1> list = (List<CData_1>)grdMain.DataSource;
                list.RemoveAt(grdMainView.FocusedRowHandle);
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "저장 중입니다.");
                this.Save();
                this.CloseProgressForm("", "저장 중입니다.");
                Cursor.Current = Cursors.Default;

                m_saved = true;
                this.Close();
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

                    if (txtElineseq.Text.ToString() == "")
                    {
                        SetNewSeq(conn, tran);
                        if (txtElineseq.Text.ToString() == "")
                        {
                            tran.Rollback();
                            MessageBox.Show("줄번호를 만들 수 없습니다.");
                            return;
                        }
                    }

                    if (IsM0602(conn, tran))
                    {
                        UpdM0602(conn, tran);
                    }
                    else
                    {
                        InsM0602(conn, tran);
                    }
                    DelM0603(conn, tran);
                    InsM0603(conn, tran);
                    UpdM0601(conn, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void SetNewSeq(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql += System.Environment.NewLine + "SELECT ISNULL(MAX(ELINESEQ),0)+1 NEXT_ELINESEQ";
            sql += System.Environment.NewLine + "  FROM TIE_M0602";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtElineseq.Text = reader["NEXT_ELINESEQ"].ToString();
                }
                reader.Close();
            }
        }

        private Boolean IsM0602(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strCount = "0";

            string sql = "";
            sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
            sql += System.Environment.NewLine + "  FROM TIE_M0602";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";
            sql += System.Environment.NewLine + "   AND ELINESEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtElineseq.Text.ToString()));

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

        private void InsM0602(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strMkdiv = "";
            if (cboMkdiv.SelectedIndex == 0) strMkdiv = "1";
            else if (cboMkdiv.SelectedIndex == 1) strMkdiv = "2";
            else strMkdiv = "";

            String strPhadiv = "";
            if (cboPhadiv.SelectedIndex == 0) strPhadiv = "1";
            else if (cboPhadiv.SelectedIndex == 1) strPhadiv = "2";
            else if (cboPhadiv.SelectedIndex == 2) strPhadiv = "3";
            else strPhadiv = "";

            int intDemamt = 0;
            int.TryParse(txtDemamt.Text.ToString(), out intDemamt);

            string sql = "";
            sql += System.Environment.NewLine + "INSERT TIE_M0602(REQYM, REQSEQ, ELINESEQ, MKDIV, PHADIV, DRGEFFKND, ITEMCD, ITEMNM, WRITEDT, DEMAMT, STDSIZE, UNIT, APPLDT, DRGEFF, DOESQY, ELINENO)";
            sql += System.Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtElineseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P4", strMkdiv));
                cmd.Parameters.Add(new OleDbParameter("@P5", strPhadiv));
                cmd.Parameters.Add(new OleDbParameter("@P6", txtDrgeffknd.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P7", txtItemcd.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P8", txtItemnm.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P9", txtWritedt.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P10", intDemamt.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P11", txtStdsize.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P12", txtUnit.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P13", txtAppldt.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P14", txtDrgeff.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P15", txtDoesqy.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P16", txtElineseq.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void UpdM0602(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strMkdiv = "";
            if (cboMkdiv.SelectedIndex == 0) strMkdiv = "1";
            else if (cboMkdiv.SelectedIndex == 1) strMkdiv = "2";
            else strMkdiv = "";

            String strPhadiv = "";
            if (cboPhadiv.SelectedIndex == 0) strPhadiv = "1";
            else if (cboPhadiv.SelectedIndex == 1) strPhadiv = "2";
            else if (cboPhadiv.SelectedIndex == 2) strPhadiv = "3";
            else strPhadiv = "";

            int intDemamt = 0;
            int.TryParse(txtDemamt.Text.ToString(), out intDemamt);


            string sql = "";
            sql += System.Environment.NewLine + "UPDATE TIE_M0602";
            sql += System.Environment.NewLine + "   SET MKDIV=?";
            sql += System.Environment.NewLine + "     , PHADIV=?";
            sql += System.Environment.NewLine + "     , DRGEFFKND=?";
            sql += System.Environment.NewLine + "     , ITEMCD=?";
            sql += System.Environment.NewLine + "     , ITEMNM=?";
            sql += System.Environment.NewLine + "     , WRITEDT=?";
            sql += System.Environment.NewLine + "     , DEMAMT=?";
            sql += System.Environment.NewLine + "     , STDSIZE=?";
            sql += System.Environment.NewLine + "     , UNIT=?";
            sql += System.Environment.NewLine + "     , APPLDT=?";
            sql += System.Environment.NewLine + "     , DRGEFF=?";
            sql += System.Environment.NewLine + "     , DOESQY=?";
            sql += System.Environment.NewLine + "     , ELINENO=?";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";
            sql += System.Environment.NewLine + "   AND ELINESEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", strMkdiv));
                cmd.Parameters.Add(new OleDbParameter("@P2", strPhadiv));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtDrgeffknd.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P4", txtItemcd.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P5", txtItemnm.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P6", txtWritedt.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P7", intDemamt.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P8", txtStdsize.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P9", txtUnit.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P10", txtAppldt.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P11", txtDrgeff.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P12", txtDoesqy.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P13", txtElineseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P14", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P15", txtReqseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P16", txtElineseq.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void DelM0603(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql += System.Environment.NewLine + "DELETE";
            sql += System.Environment.NewLine + "  FROM TIE_M0603";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";
            sql += System.Environment.NewLine + "   AND ELINESEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtElineseq.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

        private void InsM0603(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            List<CData_1> list = (List<CData_1>)grdMain.DataSource;

            // TSQL문장과 Connection 객체를 지정   
            string sql = "";
            sql += System.Environment.NewLine + "INSERT TIE_M0603(REQYM, REQSEQ, ELINESEQ, SEQ, GURESID, GUNM, CODEDIV, DRGCD, DRGNM, STDSZCD, CTNUT, BUYDT, CTNUTAMT, DRGQTY, QTYCTNUT, QTYAMT)";
            sql += System.Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                int seq = 0;
                foreach (CData_1 data in list)
                {
                    cmd.Parameters.Clear();

                    seq++;
                    cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P3", txtElineseq.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P4", seq));
                    cmd.Parameters.Add(new OleDbParameter("@P5", data.GURESID));
                    cmd.Parameters.Add(new OleDbParameter("@P6", data.GUNM));
                    cmd.Parameters.Add(new OleDbParameter("@P7", data.CODEDIV));
                    cmd.Parameters.Add(new OleDbParameter("@P8", data.DRGCD));
                    cmd.Parameters.Add(new OleDbParameter("@P9", data.DRGNM));
                    cmd.Parameters.Add(new OleDbParameter("@P10", data.STDSZCD));
                    cmd.Parameters.Add(new OleDbParameter("@P11", data.CTNUT));
                    cmd.Parameters.Add(new OleDbParameter("@P12", data.BUYDT));
                    cmd.Parameters.Add(new OleDbParameter("@P13", data.CTNUTAMT_SAVE));
                    cmd.Parameters.Add(new OleDbParameter("@P14", data.DRGQTY_SAVE));
                    cmd.Parameters.Add(new OleDbParameter("@P15", data.QTYCTNUT));
                    cmd.Parameters.Add(new OleDbParameter("@P16", data.QTYAMT_SAVE));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdM0601(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strPhareqtot = "";

            string sql = "";
            sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
            sql += System.Environment.NewLine + "  FROM TIE_M0602";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqseq.Text.ToString()));

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strPhareqtot = reader["CNT"].ToString();
                }
                reader.Close();
            }
            int count = 0;
            int.TryParse(strPhareqtot, out count);

            // TIE_M0601에 TIE_M0602건수 업데이트
            sql = "";
            sql += System.Environment.NewLine + "UPDATE TIE_M0601";
            sql += System.Environment.NewLine + "   SET PHAREQTOT=?";
            sql += System.Environment.NewLine + " WHERE REQYM=?";
            sql += System.Environment.NewLine + "   AND REQSEQ=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@P1", count));
                cmd.Parameters.Add(new OleDbParameter("@P2", txtReqym.Text.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@P3", txtReqseq.Text.ToString()));

                cmd.ExecuteNonQuery();
            }
        }

    }
}

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
    public partial class ADD8003E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD8003E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD8003E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void btnReqymAndSeq_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstReqymAndSeq.Visible == true)
                {
                    lstReqymAndSeq.Hide();
                }
                else
                {
                    this.SetReqymAndSeqListBox();
                    lstReqymAndSeq.Top = txtReqymAndSeq.Top + txtReqymAndSeq.Height;
                    lstReqymAndSeq.Left = txtReqymAndSeq.Left;
                    lstReqymAndSeq.Show();
                    lstReqymAndSeq.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetReqymAndSeqListBox()
        {
            lstReqymAndSeq.Items.Clear();

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT DISTINCT REQYM,REQSEQ";
            sql += System.Environment.NewLine + "  FROM TIE_M0601";
            sql += System.Environment.NewLine + " ORDER BY REQYM DESC, REQSEQ DESC";

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
                        lstReqymAndSeq.Items.Add(reader["REQYM"].ToString() + "-" + reader["REQSEQ"].ToString());
                    }
                    reader.Close();
                }

                conn.Close();
            }
        }

        private void txtReqymAndSeq_TextChanged(object sender, EventArgs e)
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

        private void lstReqymAndSeq_DoubleClick(object sender, EventArgs e)
        {
            if (lstReqymAndSeq.SelectedItem != null)
            {
                txtReqymAndSeq.Text = lstReqymAndSeq.SelectedItem.ToString();
                lstReqymAndSeq.Hide();

            }
        }

        private void ActionQuery()
        {
            try
            {
                String strReqymAndSeq = txtReqymAndSeq.Text.ToString();
                strReqymAndSeq += "-"; // 에러 방지용 
                if (strReqymAndSeq.Split('-')[0] == "" || strReqymAndSeq.Split('-')[1]=="")
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
            String reqym = txtReqymAndSeq.Text.ToString().Split('-')[0];
            String reqseq = txtReqymAndSeq.Text.ToString().Split('-')[1];

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT FMNO,HOSID,DDNM,PHAREQTOT,MEMO";
                sql += System.Environment.NewLine + "  FROM TIE_M0601";
                sql += System.Environment.NewLine + " WHERE REQYM=?";
                sql += System.Environment.NewLine + "   AND REQSEQ=?";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                    cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFmno.Text = reader["FMNO"].ToString();
                        txtHosid.Text = reader["HOSID"].ToString();
                        txtDdnm.Text = reader["DDNM"].ToString();
                        txtPhareqtot.Text = reader["PHAREQTOT"].ToString();
                        txtMemo.Text = reader["MEMO"].ToString();
                    }
                    reader.Close();
                }

                sql="";
                sql += System.Environment.NewLine + "SELECT REQYM,REQSEQ,ELINESEQ,MKDIV,PHADIV,DRGEFFKND,ITEMCD,ITEMNM,WRITEDT,DEMAMT,STDSIZE,UNIT,APPLDT,DRGEFF,DOESQY";
                sql += System.Environment.NewLine + "  FROM TIE_M0602";
                sql += System.Environment.NewLine + " WHERE REQYM=?";
                sql += System.Environment.NewLine + "   AND REQSEQ=?";
                sql += System.Environment.NewLine + " ORDER BY ELINENO";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                    cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();

                        data.REQYM = reader["REQYM"].ToString();
                        data.REQSEQ = reader["REQSEQ"].ToString();
                        data.ELINESEQ = reader["ELINESEQ"].ToString();
                        data.MKDIV = reader["MKDIV"].ToString();
                        data.PHADIV = reader["PHADIV"].ToString();
                        data.DRGEFFKND = reader["DRGEFFKND"].ToString();
                        data.ITEMCD = reader["ITEMCD"].ToString();
                        data.ITEMNM = reader["ITEMNM"].ToString();
                        data.WRITEDT = reader["WRITEDT"].ToString();
                        data.DEMAMT = reader["DEMAMT"].ToString();
                        data.STDSIZE = reader["STDSIZE"].ToString();
                        data.UNIT = reader["UNIT"].ToString();
                        data.APPLDT = reader["APPLDT"].ToString();
                        data.DRGEFF = reader["DRGEFF"].ToString();
                        data.DOESQY = reader["DOESQY"].ToString();

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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = grdMainView.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                List<CData> list = (List<CData>)grdMain.DataSource;
                String reqym = list[info.RowHandle].REQYM;
                String reqseq = list[info.RowHandle].REQSEQ;
                String elineseq  = list[info.RowHandle].ELINESEQ;

                ADD8003E_1 f = new ADD8003E_1();
                f.m_reqym = reqym;
                f.m_reqseq = reqseq;
                f.m_elineseq = elineseq;
                f.ShowDialog(this);
                Boolean saved = f.m_saved;
                if (saved) this.ActionQuery();
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (txtReqymAndSeq.Text.ToString() == "") return;
            String reqym = txtReqymAndSeq.Text.ToString().Split('-')[0];
            String reqseq = txtReqymAndSeq.Text.ToString().Split('-')[1];
            String elineseq = "";

            ADD8003E_1 f = new ADD8003E_1();
            f.m_reqym = reqym;
            f.m_reqseq = reqseq;
            f.m_elineseq = elineseq;
            f.ShowDialog(this);
            Boolean saved = f.m_saved;
            if (saved) this.ActionQuery();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReqymAndSeq.Text.ToString() == "") return;
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.DelRow();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                this.ActionQuery();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void DelRow()
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            int idx = grdMainView.FocusedRowHandle;
            String reqym = list[idx].REQYM;
            String reqseq = list[idx].REQSEQ;
            String elineseq = list[idx].ELINESEQ;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    // TIE_M0603 삭제
                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0603";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";
                    sql += System.Environment.NewLine + "   AND ELINESEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));
                        cmd.Parameters.Add(new OleDbParameter("@P3", elineseq));

                        cmd.ExecuteNonQuery();
                    }

                    // TIE_M0602 삭제
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0602";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";
                    sql += System.Environment.NewLine + "   AND ELINESEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));
                        cmd.Parameters.Add(new OleDbParameter("@P3", elineseq));

                        cmd.ExecuteNonQuery();
                    }

                    // TIE_M0602와 TIE_M0603의 ELINESEQ 재조정
                    int seq = 0; // TIE_M0602의 개수로도 사용됨.
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT *";
                    sql += System.Environment.NewLine + "  FROM TIE_M0602";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";
                    sql += System.Environment.NewLine + " ORDER BY ELINESEQ";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            seq++;
                            int intElineseq = 0;
                            int.TryParse(reader["ELINESEQ"].ToString(),out intElineseq);
                            if (seq != intElineseq)
                            {
                                // TIE_M0602의 ELINESEQ 수정
                                sql = "";
                                sql += System.Environment.NewLine + "UPDATE TIE_M0602";
                                sql += System.Environment.NewLine + "   SET ELINESEQ=?";
                                sql += System.Environment.NewLine + "     , ELINENO=?";
                                sql += System.Environment.NewLine + " WHERE REQYM=?";
                                sql += System.Environment.NewLine + "   AND REQSEQ=?";
                                sql += System.Environment.NewLine + "   AND ELINESEQ=?";
                                using (OleDbCommand ucmd = new OleDbCommand(sql, conn, tran))
                                {
                                    ucmd.Parameters.Add(new OleDbParameter("@P1", seq));
                                    ucmd.Parameters.Add(new OleDbParameter("@P2", seq));
                                    ucmd.Parameters.Add(new OleDbParameter("@P3", reqym));
                                    ucmd.Parameters.Add(new OleDbParameter("@P4", reqseq));
                                    ucmd.Parameters.Add(new OleDbParameter("@P5", intElineseq));

                                    ucmd.ExecuteNonQuery();
                                }
                                // TIE_M0603의 ELINESEQ 수정
                                sql = "";
                                sql += System.Environment.NewLine + "UPDATE TIE_M0603";
                                sql += System.Environment.NewLine + "   SET ELINESEQ=?";
                                sql += System.Environment.NewLine + " WHERE REQYM=?";
                                sql += System.Environment.NewLine + "   AND REQSEQ=?";
                                sql += System.Environment.NewLine + "   AND ELINESEQ=?";
                                using (OleDbCommand ucmd = new OleDbCommand(sql, conn, tran))
                                {
                                    ucmd.Parameters.Add(new OleDbParameter("@P1", seq));
                                    ucmd.Parameters.Add(new OleDbParameter("@P2", reqym));
                                    ucmd.Parameters.Add(new OleDbParameter("@P3", reqseq));
                                    ucmd.Parameters.Add(new OleDbParameter("@P4", intElineseq));

                                    ucmd.ExecuteNonQuery();
                                }
                            }
                        }
                        reader.Close();
                    }

                    // TIE_M0601에 TIE_M0602건수 업데이트
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TIE_M0601";
                    sql += System.Environment.NewLine + "   SET PHAREQTOT=?";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", seq));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P3", reqseq));

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

        private void btnNewReqseq_Click(object sender, EventArgs e)
        {
            try
            {
                String strReqym = GetSysDate();
                if (InputBox("", "신청일자 : ", ref strReqym) == DialogResult.Cancel) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.NewReqseq(strReqym);
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

        private String GetSysDate()
        {
            String strRet = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                strRet = MetroLib.Util.GetSysDate(conn);
            }
            return strRet;
        }

        private void NewReqseq(String strReqym)
        {
            String strReqseq = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                txtFmno.Text = "M060";

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

                // REQSEQ 만들기
                sql="";
                sql += System.Environment.NewLine + "SELECT ISNULL(MAX(REQSEQ),0)+1 NEXT_REQSEQ";
                sql += System.Environment.NewLine + "  FROM TIE_M0601";
                sql += System.Environment.NewLine + " WHERE REQYM=?";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", strReqym));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        strReqseq = reader["NEXT_REQSEQ"].ToString();
                    }
                    reader.Close();
                }

                txtReqymAndSeq.Text = strReqym + "-" + strReqseq;

                sql = "";
                sql += System.Environment.NewLine + "INSERT TIE_M0601(REQYM,REQSEQ,FMNO,HOSID,DDNM)";
                sql += System.Environment.NewLine + "VALUES(?,?,?,?,?)";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", strReqym));
                    cmd.Parameters.Add(new OleDbParameter("@P2", strReqseq));
                    cmd.Parameters.Add(new OleDbParameter("@P3", txtFmno.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P4", txtHosid.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P5", txtDdnm.Text.ToString()));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void btnDelReqseq_Click(object sender, EventArgs e)
        {
            try
            {
                String strReqymAndSeq = txtReqymAndSeq.Text.ToString();
                if (strReqymAndSeq == "") return;
                if (MessageBox.Show(strReqymAndSeq + " 를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.DelReqseq();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                txtReqymAndSeq.Text = "";
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void DelReqseq()
        {
            String reqym = txtReqymAndSeq.Text.ToString().Split('-')[0];
            String reqseq = txtReqymAndSeq.Text.ToString().Split('-')[1];

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    // TIE_M0603 삭제
                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0603";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

                        cmd.ExecuteNonQuery();
                    }

                    // TIE_M0602 삭제
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0602";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

                        cmd.ExecuteNonQuery();
                    }

                    // TIE_M0601 삭제
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE";
                    sql += System.Environment.NewLine + "  FROM TIE_M0601";
                    sql += System.Environment.NewLine + " WHERE REQYM=?";
                    sql += System.Environment.NewLine + "   AND REQSEQ=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", reqym));
                        cmd.Parameters.Add(new OleDbParameter("@P2", reqseq));

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

        private void btnSaveReqseq_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReqymAndSeq.Text.ToString() == "") return;
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.SaveReqseq();
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

        private void SaveReqseq()
        {
            String reqym = txtReqymAndSeq.Text.ToString().Split('-')[0];
            String reqseq = txtReqymAndSeq.Text.ToString().Split('-')[1];

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "UPDATE TIE_M0601";
                sql += System.Environment.NewLine + "   SET FMNO=?";
                sql += System.Environment.NewLine + "     , HOSID=?";
                sql += System.Environment.NewLine + "     , DDNM=?";
                sql += System.Environment.NewLine + "     , MEMO=?";
                sql += System.Environment.NewLine + " WHERE REQYM=?";
                sql += System.Environment.NewLine + "   AND REQSEQ=?";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@P1", txtFmno.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P2", txtHosid.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P3", txtDdnm.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P4", txtMemo.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@P5", reqym));
                    cmd.Parameters.Add(new OleDbParameter("@P6", reqseq));

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

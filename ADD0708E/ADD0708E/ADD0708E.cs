using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0708E
{
    public partial class ADD0708E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox mkdivComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox phadivComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox codedivComboBox;

        public ADD0708E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            MakeComboInGrid();
        }

        public ADD0708E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            mkdivComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            phadivComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            codedivComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

            grdMainView.Columns["MKDIVNM"].ColumnEdit = mkdivComboBox;
            grdMainView.Columns["PHADIVNM"].ColumnEdit = phadivComboBox;
            grdMainView.Columns["CODEDIVNM"].ColumnEdit = codedivComboBox;

            mkdivComboBox.Items.Clear();
            mkdivComboBox.Items.Add("조제");
            mkdivComboBox.Items.Add("제제");

            phadivComboBox.Items.Clear();
            phadivComboBox.Items.Add("내복약");
            phadivComboBox.Items.Add("주사약");
            phadivComboBox.Items.Add("외용약");

            codedivComboBox.Items.Clear();
            codedivComboBox.Items.Add("보험등재약");
            codedivComboBox.Items.Add("원료약");
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

        private void btnList_Click(object sender, EventArgs e)
        {
            ADD0708E_01 f = new ADD0708E_01();
            f.ShowDialog(this);
            txtDemno.Text = f.m_demno;
            f = null;
            btnQuery.PerformClick();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtDemno.Text = GetNewDemno();
            Clear();
            btnAddRow.PerformClick();
        }

        private void Clear()
        {
            txtHosid.Text = GetHosid();
            txtDdnm.Text = GetUsernm();
            txtTotcnt.Text = "";
            txtFmno.Text = "H070";
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;
        }

        private string GetHosid()
        {
            try
            {
                string hosid = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL" + m_HospMulti + "' AND MST3CD='2'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        hosid = reader["FLD1QTY"].ToString();
                        return false;
                    });
                }
                return hosid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private string GetUsernm()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT USRNM FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["USRNM"].ToString();
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

        private string GetNewDemno()
        {
            try
            {
                string demno = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    demno = MetroLib.Util.GetSysDate(conn) + MetroLib.Util.GetSysTime(conn).Substring(0, 4);
                    while (true)
                    {
                        string sql = "SELECT COUNT(*) AS CNT FROM TIE_H0701 WHERE DEMNO='" + demno + "'";
                        int cnt = 0;
                        MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                        {
                            int.TryParse(reader["CNT"].ToString(), out cnt);
                            return false;
                        });
                        if (cnt <= 0) break;
                        long no = 0;
                        long.TryParse(demno, out no);
                        no++;
                        demno = no.ToString();
                    }
                }
                return demno;
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
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "SELECT * FROM TIE_H0701 WHERE DEMNO='" + txtDemno.Text.ToString() + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    txtHosid.Text = reader["HOSID"].ToString();
                    txtDdnm.Text = reader["DDNM"].ToString();
                    txtTotcnt.Text = reader["TOTCNT"].ToString();
                    txtFmno.Text = reader["FMNO"].ToString();

                    return false;
                });

                sql = "SELECT * FROM TIE_H0702 WHERE DEMNO='" + txtDemno.Text.ToString() + "' ORDER BY ELINENO";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();
                    int elineno = 0;
                    int.TryParse(reader["ELINENO"].ToString(), out elineno);
                    data.ELINENO = elineno;
                    data.MKDIVNM = GetMKDIVNM(reader["MKDIV"].ToString());
                    data.PHADIVNM = GetPHADIVNM(reader["PHADIV"].ToString());
                    data.DRGEFKND = reader["DRGEFKND"].ToString();
                    data.ITEMCD = reader["ITEMCD"].ToString();
                    data.ITEMNM = reader["ITEMNM"].ToString();
                    data.WRITEDT = reader["WRITEDT"].ToString();
                    int demamt = 0;
                    int.TryParse(reader["DEMAMT"].ToString(), out demamt);
                    data.DEMAMT = demamt;
                    data.PTYPE = reader["PTYPE"].ToString();
                    data.PDUT = reader["PDUT"].ToString();
                    data.ADTDT = reader["ADTDT"].ToString();
                    data.DRGEF = reader["DRGEF"].ToString();
                    data.DOESQY = reader["DOESQY"].ToString();

                    sql = "SELECT * FROM TIE_H0703 WHERE DEMNO='" + txtDemno.Text.ToString() + "' AND ELINENO=" + reader["ELINENO"].ToString();
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader2)
                    {
                        data.BUSINESSCD = reader2["BUSINESSCD"].ToString();
                        data.TRADENM = reader2["TRADENM"].ToString();
                        data.CODEDIVNM = GetCODEDIVNM(reader2["CODEDIV"].ToString());
                        data.DRGCD = reader2["DRGCD"].ToString();
                        data.DRGNM = reader2["DRGNM"].ToString();
                        data.PTYPE_3 = reader2["PTYPE"].ToString();
                        data.PDUT_3 = reader2["PDUT"].ToString();
                        data.BUYDT = reader2["BUYDT"].ToString();
                        float utamt = 0;
                        float.TryParse(reader2["UTAMT"].ToString(), out utamt);
                        data.UTAMT = utamt;
                        float drgqty = 0;
                        float.TryParse(reader2["DRGQTY"].ToString(), out drgqty);
                        data.DRGQTY = drgqty;
                        data.QTYUT = reader2["QTYUT"].ToString();
                        float qtyamt = 0;
                        float.TryParse(reader2["QTYAMT"].ToString(), out qtyamt);
                        data.QTYAMT = qtyamt;

                        return false;
                    });

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private string GetMKDIVNM(string p_mkdiv)
        {
            if (p_mkdiv == "1") return "조제";
            if (p_mkdiv == "2") return "제제";
            return "조제";
        }

        private string GetPHADIVNM(string p_phadiv)
        {
            if (p_phadiv == "1") return "내복약";
            if (p_phadiv == "2") return "주사약";
            if (p_phadiv == "3") return "외용약";
            return "내복약";
        }

        private string GetCODEDIVNM(string p_codediv)
        {
            if (p_codediv == "1") return "보험등재약";
            if (p_codediv == "2") return "원료약";
            return "보험등재약";
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
            if (txtDemno.Text.ToString() == "") return;

            AddRow();
            grdMainView.FocusedRowHandle = grdMainView.FocusedRowHandle + 1;
            grdMainView.FocusedColumn = gcMKDIVNM;
            grdMain.Focus();
        }

        private void AddRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Add(data);
            //data.CDGBNM = "수가";
            //data.JRGBNM = "의과";
            //data.APPLYDT = GetDefaultDate(list, list.Count - 1);
            //data.ADDFILEFGNM = "없음";

            RefreshGridMain();
        }

        private void btnInsRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            InsRow();
            grdMainView.FocusedColumn = gcMKDIVNM;
            grdMain.Focus();
        }

        private void InsRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Insert(grdMainView.FocusedRowHandle, data);
            //data.CDGBNM = "수가";
            //data.JRGBNM = "의과";
            //data.APPLYDT = GetDefaultDate(list, grdMainView.FocusedRowHandle);
            //data.ADDFILEFGNM = "없음";

            RefreshGridMain();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
            grdMainView.FocusedColumn = gcMKDIVNM;
            grdMain.Focus();
            RefreshGridMain();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                demno = GetNewDemno();
                if (MessageBox.Show("신청번호 [" + demno + "] 로 저장하시겠습니까?", (sender as Button).Text.ToString(), MessageBoxButtons.YesNo) == DialogResult.No) return;


                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(demno);
                txtDemno.Text = demno;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(demno);
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

        private void Save(string p_demno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sql = "";

                    sql = "DELETE FROM TIE_H0701 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0702 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0703 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    int lineno = 0;
                    List<CData> list = (List<CData>)grdMain.DataSource;
                    foreach (CData data in list)
                    {
                        lineno++;

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TIE_H0702(DEMNO,ELINENO,MKDIV,PHADIV,DRGEFKND,ITEMCD,ITEMNM,WRITEDT,DEMAMT,PTYPE,PDUT,ADTDT,DRGEF,DOESQY)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                            cmd.Parameters.Add(new OleDbParameter("@2", lineno));
                            cmd.Parameters.Add(new OleDbParameter("@3", data.MKDIV));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.PHADIV));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.DRGEFKND));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.ITEMCD));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.ITEMNM));
                            cmd.Parameters.Add(new OleDbParameter("@8", data.WRITEDT));
                            cmd.Parameters.Add(new OleDbParameter("@9", data.DEMAMT));
                            cmd.Parameters.Add(new OleDbParameter("@10", data.PTYPE));
                            cmd.Parameters.Add(new OleDbParameter("@11", data.PDUT));
                            cmd.Parameters.Add(new OleDbParameter("@12", data.ADTDT));
                            cmd.Parameters.Add(new OleDbParameter("@13", data.DRGEF));
                            cmd.Parameters.Add(new OleDbParameter("@14", data.DOESQY));
                            cmd.ExecuteNonQuery();
                        }

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TIE_H0703(DEMNO,ELINENO,BUSINESSCD,TRADENM,CODEDIV,DRGCD,DRGNM,PTYPE,PDUT,BUYDT,UTAMT,DRGQTY,QTYUT,QTYAMT)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                            cmd.Parameters.Add(new OleDbParameter("@2", lineno));
                            cmd.Parameters.Add(new OleDbParameter("@3", data.BUSINESSCD));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.TRADENM));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.CODEDIV));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.DRGCD));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.DRGNM));
                            cmd.Parameters.Add(new OleDbParameter("@8", data.PTYPE_3));
                            cmd.Parameters.Add(new OleDbParameter("@9", data.PDUT_3));
                            cmd.Parameters.Add(new OleDbParameter("@10", data.BUYDT));
                            cmd.Parameters.Add(new OleDbParameter("@11", data.UTAMT));
                            cmd.Parameters.Add(new OleDbParameter("@12", data.DRGQTY));
                            cmd.Parameters.Add(new OleDbParameter("@13", data.QTYUT));
                            cmd.Parameters.Add(new OleDbParameter("@14", data.QTYAMT));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TIE_H0701(DEMNO,FMNO,HOSID,DDNM,TOTCNT,MEMO)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                        cmd.Parameters.Add(new OleDbParameter("@2", txtFmno.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@3", txtHosid.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@4", txtDdnm.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@5", lineno));
                        cmd.Parameters.Add(new OleDbParameter("@6", txtMemo.Text.ToString()));
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                if (MessageBox.Show("신청번호 [" + demno + "] 를 삭제하시겠습니까?", (sender as Button).Text.ToString(), MessageBoxButtons.YesNo) == DialogResult.No) return;


                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Del(demno);
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                txtDemno.Text = "";
                Clear();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Del(string p_demno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sql = "";

                    sql = "DELETE FROM TIE_H0701 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0702 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0703 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
        }

        private void btnEdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDemno.Text.ToString() == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MkEdifile();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("성공적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void MkEdifile()
        {
            string demno = txtDemno.Text.ToString();

            string path = "C:/HIRA/DDMD/sam/in";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            if (di.Exists == false) di.Create();

            // 파일 내용 지우기
            System.IO.StreamWriter sw = null;
            sw = System.IO.File.CreateText(path + "/MEDLOG.enc");
            sw.Write("");
            sw.Close();
            sw = System.IO.File.CreateText(path + "/H070.1");
            sw.Write("");
            sw.Close();
            sw = System.IO.File.CreateText(path + "/H070.2");
            sw.Write("");
            sw.Close();
            sw = System.IO.File.CreateText(path + "/H070.3");
            sw.Write("");
            sw.Close();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";

                sw = System.IO.File.AppendText(path + "/H070.1");
                sql = "SELECT * FROM TIE_H0701 WHERE DEMNO='" + demno + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string rec = "";
                    rec += Conv(reader["DEMNO"].ToString(), "N", 12);
                    rec += Conv(reader["FMNO"].ToString(), "C", 4);
                    rec += Conv(reader["HOSID"].ToString(), "N", 8);
                    rec += Conv(reader["DDNM"].ToString(), "C", 20);
                    rec += Conv(reader["TOTCNT"].ToString(), "N", 4);
                    rec += Conv(reader["MEMO"].ToString(), "C", 1750);

                    sw.Write(rec);

                    return false;
                });
                sw.Close();

                int cnt = 0;
                sw = System.IO.File.AppendText(path + "/H070.2");
                sql = "SELECT * FROM TIE_H0702 WHERE DEMNO='" + demno + "' ORDER BY ELINENO";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    string rec = "";
                    rec += Conv(reader["DEMNO"].ToString(), "N", 12);
                    rec += Conv(reader["ELINENO"].ToString(), "N", 3);
                    rec += Conv(reader["MKDIV"].ToString(), "N", 1);
                    rec += Conv(reader["PHADIV"].ToString(), "N", 1);
                    rec += Conv(reader["DRGEFKND"].ToString(), "C", 50);
                    rec += Conv(reader["ITEMCD"].ToString(), "C", 8);
                    rec += Conv(reader["ITEMNM"].ToString(), "C", 140);
                    rec += Conv(reader["WRITEDT"].ToString(), "N", 8);
                    rec += Conv(reader["DEMAMT"].ToString(), "N", 10);
                    rec += Conv(reader["PTYPE"].ToString(), "C", 140);
                    rec += Conv(reader["PDUT"].ToString(), "C", 70);
                    rec += Conv(reader["ADTDT"].ToString(), "N", 8);
                    rec += Conv(reader["DRGEF"].ToString(), "C", 350);
                    rec += Conv(reader["DOESQY"].ToString(), "C", 350);

                    if (cnt > 1) sw.WriteLine();
                    sw.Write(rec);

                    return true;
                });
                sw.Close();

                cnt = 0;
                sw = System.IO.File.AppendText(path + "/H070.3");
                sql = "SELECT * FROM TIE_H0703 WHERE DEMNO='" + demno + "' ORDER BY ELINENO";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    string rec = "";
                    rec += Conv(reader["DEMNO"].ToString(), "N", 12);
                    rec += Conv(reader["ELINENO"].ToString(), "N", 3);
                    rec += Conv(reader["BUSINESSCD"].ToString(), "C", 17);
                    rec += Conv(reader["TRADENM"].ToString(), "C", 35);
                    rec += Conv(reader["CODEDIV"].ToString(), "C", 3);
                    rec += Conv(reader["DRGCD"].ToString(), "C", 9);
                    rec += Conv(reader["DRGNM"].ToString(), "C", 140);
                    rec += Conv(reader["PTYPE"].ToString(), "C", 140);
                    rec += Conv(reader["PDUT"].ToString(), "C", 70);
                    rec += Conv(reader["BUYDT"].ToString(), "N", 8);
                    rec += Conv(reader["UTAMT"].ToString(), "N2", 10);
                    rec += Conv(reader["DRGQTY"].ToString(), "N2", 9);
                    rec += Conv(reader["QTYUT"].ToString(), "N", 3);
                    rec += Conv(reader["QTYAMT"].ToString(), "N2", 9);

                    if (cnt > 1) sw.WriteLine();
                    sw.Write(rec);

                    return true;
                });
                sw.Close();

                sw = System.IO.File.AppendText(path + "/MEDLOG.enc");
                sw.Write(Conv("10100012", "C", 12));
                sw.WriteLine();
                sw.Write(Conv("NULL", "C", 8));
                sw.WriteLine();
                sw.Write(Conv(demno.Substring(0, 8) + " 자체조제제제약통보서", "C", 30));
                sw.WriteLine();
                sw.Write(Conv("", "C", 12));
                sw.Close();
            }
            
        }

        private string Conv(string p_value, string p_type, int p_len)
        {
            if (p_type == "N2") p_value = p_value.Replace(".", "");
            string space = "";
            for (int i = 0; i < p_len; i++)
            {
                space += (p_type == "N" || p_type == "N2" ? "0" : " ");
            }
            
            string ret = "";
            if (p_type == "N" || p_type == "N2")
            {
                ret = MetroLib.StrHelper.RSubstringH(space + p_value, 0, p_len);
            }
            else
            {
                ret = MetroLib.StrHelper.SubstringH(p_value + space, 0, p_len);
            }
            return ret;
        }
    }
}

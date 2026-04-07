using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0731E
{
    public partial class ADD0731E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cdgbComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox jrgbComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox addfilefgComboBox;

        public ADD0731E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            MakeComboInGrid();
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.
            cdgbComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            jrgbComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            addfilefgComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

            grdMainView.Columns["CDGBNM"].ColumnEdit = cdgbComboBox;
            grdMainView.Columns["JRGBNM"].ColumnEdit = jrgbComboBox;
            grdMainView.Columns["ADDFILEFGNM"].ColumnEdit = addfilefgComboBox;

            cdgbComboBox.Items.Clear();
            cdgbComboBox.Items.Add("수가");
            cdgbComboBox.Items.Add("신기술행위");
            cdgbComboBox.Items.Add("신기술재료");

            jrgbComboBox.Items.Clear();
            jrgbComboBox.Items.Add("의과");
            jrgbComboBox.Items.Add("치과");
            jrgbComboBox.Items.Add("한방");

            addfilefgComboBox.Items.Clear();
            addfilefgComboBox.Items.Add("있음");
            addfilefgComboBox.Items.Add("없음");
        }

        public ADD0731E(String user, String pwd, String prjcd, String addpara)
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
            txtFmno.Text = "C100";
            txtVersion.Text = "010";
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;
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
                        string sql = "SELECT COUNT(*) AS CNT FROM TIE_C1001 WHERE DEMNO='" + demno + "'";
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
                sql = "SELECT * FROM TIE_C1001 WHERE DEMNO='" + txtDemno.Text.ToString() + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    txtHosid.Text = reader["HOSID"].ToString();
                    txtDdnm.Text = reader["DDNM"].ToString();
                    txtTotcnt.Text = reader["TOTCNT"].ToString();
                    txtFmno.Text = reader["FMNO"].ToString();
                    txtVersion.Text = reader["VERSION"].ToString();

                    return false;
                });

                sql = "SELECT * FROM TIE_C1002 WHERE DEMNO='" + txtDemno.Text.ToString() + "' ORDER BY ELINENO";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.CDGBNM = GetCDGBNM(reader["CDGB"].ToString());
                    data.JRGBNM = GetJRGBNM(reader["JRGB"].ToString());
                    data.PCODE = reader["PCODE"].ToString();
                    data.PCODENM = reader["PCODENM"].ToString();
                    int amt = 0;
                    int.TryParse(reader["AMT"].ToString(), out amt);
                    data.AMT = amt;
                    data.APPLYDT = reader["APPLYDT"].ToString();
                    data.ADDFILEFGNM = GetADDFILEFGNM(reader["ADDFILEFG"].ToString());
                    data.REMARK = reader["REMARK"].ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private string GetCDGBNM(string p_cdgb)
        {
            if (p_cdgb == "A") return "수가";
            if (p_cdgb == "B") return "행위신기술";
            if (p_cdgb == "C") return "재료신기술";
            return "수가";
        }

        private string GetJRGBNM(string p_jrgb)
        {
            if (p_jrgb == "1") return "의과";
            if (p_jrgb == "2") return "치과";
            if (p_jrgb == "3") return "한방";
            return "의과";
        }

        private string GetADDFILEFGNM(string p_addfilefg)
        {
            if (p_addfilefg == "Y") return "있음";
            if (p_addfilefg == "N") return "없음";
            return "없음";
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
            grdMainView.FocusedColumn = gcPCODE;
            grdMain.Focus();
        }

        private void AddRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Add(data);
            data.CDGBNM = "수가";
            data.JRGBNM = "의과";
            data.APPLYDT = GetDefaultDate(list, list.Count - 1);
            data.ADDFILEFGNM = "없음";

            RefreshGridMain();
        }

        private string GetDefaultDate(List<CData> p_list, int p_idx){
            // 위로 올라가면서 찾는다.
            for (int idx = p_idx; idx >= 0; idx--)
            {
                if (p_list[idx].APPLYDT != "") return p_list[idx].APPLYDT;
            }
            // 아래로 내려가면서 찾는다.
            for (int idx = p_idx; idx <p_list.Count-1; idx++)
            {
                if (p_list[idx].APPLYDT != "") return p_list[idx].APPLYDT;
            }
            // 없으면 현재일자를 사용한다.
            string ret = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                ret = MetroLib.Util.GetSysDate(conn);
            }

            return ret;
        }

        private void btnInsRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            InsRow();
            grdMainView.FocusedColumn = gcPCODE;
            grdMain.Focus();
        }

        private void InsRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Insert(grdMainView.FocusedRowHandle, data);
            data.CDGBNM = "수가";
            data.JRGBNM = "의과";
            data.APPLYDT = GetDefaultDate(list, grdMainView.FocusedRowHandle);
            data.ADDFILEFGNM = "없음";

            RefreshGridMain();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
            grdMainView.FocusedColumn = gcPCODE;
            grdMain.Focus();
            RefreshGridMain();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ADD0731E_01 f = new ADD0731E_01();
            f.ShowDialog(this);
            txtDemno.Text = f.m_demno;
            f = null;
            btnQuery.PerformClick();
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

                    sql = "DELETE FROM TIE_C1001 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_C1002 WHERE DEMNO='" + p_demno + "'";
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
                        sql += Environment.NewLine + "INSERT INTO TIE_C1002(DEMNO,ELINENO,CDGB,JRGB,PCODE,PCODENM,AMT,APPLYDT,ADDFILEFG,REMARK)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                            cmd.Parameters.Add(new OleDbParameter("@2", lineno));
                            cmd.Parameters.Add(new OleDbParameter("@3", GetCDGB(data.CDGBNM)));
                            cmd.Parameters.Add(new OleDbParameter("@4", GetJRGB(data.JRGBNM)));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.PCODE));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.PCODENM));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.AMT));
                            cmd.Parameters.Add(new OleDbParameter("@8", data.APPLYDT));
                            cmd.Parameters.Add(new OleDbParameter("@9", GetADDFILEFG(data.ADDFILEFGNM)));
                            cmd.Parameters.Add(new OleDbParameter("@10", data.REMARK));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TIE_C1001(DEMNO,FMNO,HOSID,DDNM,TOTCNT,VERSION)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                        cmd.Parameters.Add(new OleDbParameter("@2", txtFmno.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@3", txtHosid.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@4", txtDdnm.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@5", lineno));
                        cmd.Parameters.Add(new OleDbParameter("@6", txtVersion.Text.ToString()));
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

        private string GetCDGB(string p_cdgbnm)
        {
            if (p_cdgbnm == "수가") return "A";
            if (p_cdgbnm == "행위신기술") return "B";
            if (p_cdgbnm == "재료신기술") return "C";
            return "A";
        }

        private string GetJRGB(string p_jrgbnm)
        {
            if (p_jrgbnm == "의과") return "1";
            if (p_jrgbnm == "치과") return "2";
            if (p_jrgbnm == "한방") return "3";
            return "1";
        }

        private string GetADDFILEFG(string p_addfilefgnm)
        {
            if (p_addfilefgnm == "있음") return "Y";
            if (p_addfilefgnm == "없음") return "N";
            return "N";
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
            //System.IO.StreamWriter sw = null;
            //sw = System.IO.File.CreateText(path + "/MEDLOG.enc");
            //sw.Write("");
            //sw.Close();
            //sw = System.IO.File.CreateText(path + "/C100.1");
            //sw.Write("");
            //sw.Close();
            //sw = System.IO.File.CreateText(path + "/C100.2");
            //sw.Write("");
            //sw.Close();

            System.IO.StreamWriter sw = null;
            sw = new System.IO.StreamWriter(path + "/MEDLOG.enc", false, Encoding.Default);
            sw.Write("");
            sw.Close();
            sw = new System.IO.StreamWriter(path + "/C100.1", false, Encoding.Default);
            sw.Write("");
            sw.Close();
            sw = new System.IO.StreamWriter(path + "/C100.2", false, Encoding.Default);
            sw.Write("");
            sw.Close();


            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";

                //sw = System.IO.File.AppendText(path + "/C100.1");
                sw = new System.IO.StreamWriter(path + "/C100.1", true, Encoding.Default);
                sql = "SELECT * FROM TIE_C1001 WHERE DEMNO='" + demno + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string rec = "";
                    rec += Conv(reader["VERSION"].ToString(), "C", 3);
                    rec += Conv(reader["DEMNO"].ToString(), "C", 12);
                    rec += Conv(reader["FMNO"].ToString(), "C", 4);
                    rec += Conv(reader["HOSID"].ToString(), "C", 8);
                    rec += Conv(reader["DDNM"].ToString(), "C", 20);
                    rec += Conv(reader["TOTCNT"].ToString(), "N", 4);

                    sw.Write(rec);

                    return false;
                });
                sw.Close();

                int cnt = 0;
                //sw = System.IO.File.AppendText(path + "/C100.2");
                sw = new System.IO.StreamWriter(path + "/C100.2", false, Encoding.Default);
                sql = "SELECT * FROM TIE_C1002 WHERE DEMNO='" + demno + "' ORDER BY ELINENO";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    string rec = "";
                    rec += Conv(reader["DEMNO"].ToString(), "C", 12);
                    rec += Conv(reader["ELINENO"].ToString(), "N", 3);
                    rec += Conv(reader["CDGB"].ToString(), "C", 1);
                    rec += Conv(reader["JRGB"].ToString(), "C", 1);
                    rec += Conv(reader["PCODE"].ToString(), "C", 8);
                    rec += Conv(reader["PCODENM"].ToString(), "C", 140);
                    rec += Conv(reader["AMT"].ToString(), "N", 10);
                    rec += Conv(reader["APPLYDT"].ToString(), "C", 8);
                    rec += Conv(reader["ADDFILEFG"].ToString(), "C", 1);
                    rec += Conv(reader["REMARK"].ToString(), "C", 1750);

                    if (cnt > 1) sw.WriteLine();
                    sw.Write(rec);

                    return true;
                });
                sw.Close();

                //sw = System.IO.File.AppendText(path + "/MEDLOG.enc");
                sw = new System.IO.StreamWriter(path + "/MEDLOG.enc", false, Encoding.Default);
                sw.Write(Conv("10100011", "C", 12));
                sw.WriteLine();
                sw.Write(Conv("NULL", "C", 8));
                sw.WriteLine();
                sw.Write(Conv(demno.Substring(0, 8) + " 자보 비용산정통보서", "C", 30));
                sw.WriteLine();
                sw.Write(Conv("", "C", 12));
                sw.Close();
            }

        }

        private string Conv(string p_value, string p_type, int p_len)
        {
            string space = "";
            for (int i = 0; i < p_len; i++)
            {
                space += (p_type == "N" ? "0" : " ");
            }
            string ret="";
            if (p_type == "N")
            {
                ret = MetroLib.StrHelper.RSubstringH(space + p_value, 0, p_len);
            }
            else
            {
                ret = MetroLib.StrHelper.SubstringH(p_value + space, 0, p_len);
            }
            return ret;
        }

        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private void grdMainView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
        }

        private string GetPCODENM(string p_code)
        {
            if (p_code == "") return "";

            string pcodenm = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "SELECT A.PCODENM FROM TI09_JABO A WHERE A.PCODE='" + p_code + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    pcodenm = reader["PCODENM"].ToString();

                    return false;
                });

                sql = "SELECT A.PCODENM FROM TI09 A WHERE A.GUBUN IN ('1','9') AND (A.KUMAK1 = 0 AND A.KUMAK2 = 0 AND A.KUMAK3 = 0 AND A.KUMAK4 = 0 AND A.KUMAK5 = 0 AND A.KUMAK6 = 0) AND A.PCODE='" + p_code + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    pcodenm = reader["PCODENM"].ToString();

                    return false;
                });
            }
            return pcodenm;
        }

        private void grdMainView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            int row = grdMainView.GetFocusedDataSourceRowIndex();
            if (grdMainView.FocusedColumn.FieldName == "PCODE")
            {
                string pcode =  grdMainView.EditingValue.ToString();
                if (pcode == "") return;
                string pcodenm = GetPCODENM(pcode);
                if (pcodenm != "")
                {
                    grdMainView.SetRowCellValue(row, gcPCODENM, pcodenm);
                    return; // *******************
                }

                ADD0731E_02 f = new ADD0731E_02();
                f.m_in_pcode = pcode;
                f.ShowDialog(this);
                if (f.m_sel == true)
                {
                    grdMainView.SetRowCellValue(row, gcPCODE, f.m_out_pcode);
                    grdMainView.SetRowCellValue(row, gcPCODENM, f.m_out_pcodenm);
                }
                f = null;
            }
            if (grdMainView.FocusedColumn.FieldName == "PCODENM")
            {
                string pcodenm = grdMainView.EditingValue.ToString();
                if (pcodenm == "") return;
                ADD0731E_02 f = new ADD0731E_02();
                f.m_in_pcodenm = pcodenm;
                f.ShowDialog(this);
                if (f.m_sel == true)
                {
                    grdMainView.SetRowCellValue(row, gcPCODE, f.m_out_pcode);
                    grdMainView.SetRowCellValue(row, gcPCODENM, f.m_out_pcodenm);
                }
                f = null;
            }
        }

    }
}

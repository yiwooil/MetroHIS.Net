using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0705E
{
    public partial class ADD0705E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private String m_ErrKeys;

        private bool IsFirst;
        private Boolean m_OnPgm;

        public ADD0705E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            m_ErrKeys = "";
        }

        public ADD0705E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            CreatePopupMenu();
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
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@2", m_Prjcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["MULTIFG"].ToString();
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void CreatePopupMenu()
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("최초구입분만 선택", new EventHandler(mnuSelFst_Click));
            cm.MenuItems.Add("최초구입구분 삭제", new EventHandler(mnuEraFst_Click));
            grdMain.ContextMenu = cm;
        }

        private void ADD0705E_Load(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0705E");;
            int idx = (int)reg.GetValue(cboPosOption.Name, 0);
            cboPosOption.SelectedIndex = idx >= 0 && idx <= 2 ? idx : 0;

            string hdate = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    hdate = MetroLib.Util.GetSysDate(conn);

                    txtDemdd.Text = hdate.Substring(0, 4);
                    cboDemdd.SelectedIndex = 0;
                    string mm = hdate.Substring(4, 2);
                    if (mm == "01" || mm == "02" || mm == "03") cboDemdd.SelectedIndex = 0;
                    else if (mm == "04" || mm == "05" || mm == "06") cboDemdd.SelectedIndex = 1;
                    else if (mm == "07" || mm == "08" || mm == "09") cboDemdd.SelectedIndex = 2;
                    else if (mm == "10" || mm == "11" || mm == "12") cboDemdd.SelectedIndex = 3;

                    txtHosid.Text = "";
                    txtAcpnm.Text = "";
                    txtRcvid.Text = "";
                    txtDemnm.Text = "";

                    string sql = "";

                    // 요양기관기호
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A' ";
                    sql += Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + "   AND MST3CD = '2'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtHosid.Text = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 대표자
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A'";
                    sql += Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + "   AND MST3CD = '5'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtAcpnm.Text = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 수신기관코드
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD = 'A'";
                    sql += Environment.NewLine + " AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
                    sql += Environment.NewLine + " AND MST3CD = '37'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtRcvid.Text = reader["FLD1QTY"].ToString();
                        return true;
                    });

                    // 작성자
                    sql = "";
                    sql += Environment.NewLine + "SELECT USRNM";
                    sql += Environment.NewLine + "  FROM TA94";
                    sql += Environment.NewLine + " WHERE USRID = '" + m_User + "'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtDemnm.Text = reader["USRNM"].ToString();
                        return true;
                    });

                    // 수신인 리스트
                    sql = "SELECT MST3CD, CDNM FROM TA88 WHERE MST1CD='A' AND MST2CD='EDIRCVID'";
                    int i = 0;
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        i++;
                        String mst3cd = reader["MST3CD"].ToString();
                        String cdnm = reader["CDNM"].ToString();
                        cboRcvid.Items.Add(mst3cd + "-" + cdnm);
                        if (mst3cd == txtRcvid.Text.ToString()) cboRcvid.SelectedIndex = i - 1;

                        return true;
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            string demdd = GetDemdd();
            string yhgbn = GetYHgbn();

            grdMain.DataSource=null;
            List<CData> list = new List<CData>();
            grdMain.DataSource=list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT BUYDT,ITEMCD,ITEMINFO,STDSIZE,UNIT,BUYQTY,BUYTOTAMT,BUYAMT,BUSSCD,BUSSNM,FSTBUYFG,MEMO,PRODCM,EMPID,ENTDT,ENTTM,UPDID,UPDDT,UPDTM,";
                sql += Environment.NewLine + "       (SELECT CASE WHEN SUM(X.BUYQTY)=0 THEN 0 ELSE ROUND(SUM(X.BUYTOTAMT)/SUM(X.BUYQTY),0) END FROM TIE_H0800 X WHERE X.DEMDD=A.DEMDD AND X.ITEMCD=A.ITEMCD AND X.MULTIHSFG=A.MULTIHSFG AND X.YHGBN=A.YHGBN) AS ADDAVR ";
                sql += Environment.NewLine + "  FROM TIE_H0800 A ";
                sql += Environment.NewLine + " WHERE A.DEMDD = '" + demdd + "'";
                sql += Environment.NewLine + "   AND A.MULTIHSFG = '" + m_HospMulti + "'";
                sql += Environment.NewLine + "   AND A.YHGBN = '" + yhgbn + "' ";
                sql += Environment.NewLine + " ORDER BY A.ITEMCD,A.BUYDT,A.SEQ ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.SEL = true;
                    data.BUYDT = reader["BUYDT"].ToString();
                    data.ITEMCD = reader["ITEMCD"].ToString();
                    data.ITEMINFO = reader["ITEMINFO"].ToString();
                    data.STDSIZE = reader["STDSIZE"].ToString();
                    data.UNIT = reader["UNIT"].ToString();
                    data.BUYQTY = MetroLib.StrHelper.ToDouble(reader["BUYQTY"].ToString());
                    data.BUYTOTAMT = MetroLib.StrHelper.ToLong(reader["BUYTOTAMT"].ToString());
                    data.BUYAMT = MetroLib.StrHelper.ToLong(reader["BUYAMT"].ToString());
                    data.BUSSCD = reader["BUSSCD"].ToString();
                    data.BUSSNM = reader["BUSSNM"].ToString();
                    data.FSTBUYFG = reader["FSTBUYFG"].ToString();
                    data.MEMO = reader["MEMO"].ToString();
                    data.PRODCM = reader["PRODCM"].ToString();
                    data.EMPID = reader["EMPID"].ToString();
                    data.ENTDT = reader["ENTDT"].ToString();
                    data.ENTTM = reader["ENTTM"].ToString();
                    data.UPDID = reader["UPDID"].ToString();
                    data.UPDDT = reader["UPDDT"].ToString();
                    data.UPDTM = reader["UPDTM"].ToString();
                    data.ADDAVR = MetroLib.StrHelper.ToDouble(reader["ADDAVR"].ToString());


                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private string GetDemdd()
        {
            string demdd = txtDemdd.Text.ToString();
            if (cboDemdd.SelectedIndex == 0) demdd += "14";
            else if (cboDemdd.SelectedIndex == 1) demdd += "24";
            else if (cboDemdd.SelectedIndex == 2) demdd += "34";
            else if (cboDemdd.SelectedIndex == 3) demdd += "44";
            return demdd;
        }

        private string GetYHgbn()
        {
            return rbYHgbn1.Checked == true ? "1" : "2";
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            foreach (CData data in list)
            {
                data.SEL = chkAll.Checked;
            }

            RefreshGridMain();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtHosid.Text.ToString() == "") {
                MessageBox.Show("요양기관기호가 없습니다.");
                return;
            }
            if (txtDemnm.Text.ToString() == "") {
                MessageBox.Show( "작성자가 없습니다.");
                return;
            }
            if (txtAcpnm.Text.ToString() == "") {
                MessageBox.Show( "확인자가 없습니다.");
                return;
            }
            if (txtRcvid.Text.ToString() == "") {
                MessageBox.Show("수신인이 없습니다.");
                return;
            }
            long cnt = 0;
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null)
            {
                MessageBox.Show("자료가 없습니다.");
                return;
            }
            foreach (CData data in list)
            {
                if (data.SEL == false) continue;
                cnt++;
            }
            if (cnt < 1)
            {
                MessageBox.Show("선택한 자료가 없습니다.");
                return;
            }

            try
            {
                m_ErrKeys = ""; 
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                string demym = this.Save();
                this.MakeEdiFile(demym);
                grdMain.DataSource = null;
                RefreshGridMain();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + Environment.NewLine + m_ErrKeys);
            }
        }

        private string Save()
        {
            string sql = "";
            string bk_itemcd = "";
            long elineno = 0;
            string prodno = "";
            string demym = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sysdate = MetroLib.Util.GetSysDate(conn, tran);
                    string systime = MetroLib.Util.GetSysTime(conn, tran);

                    demym = sysdate + systime.Substring(0, 4);
                    List<object> para = new List<object>();

                    List<CData> list = (List<CData>)grdMain.DataSource;
                    foreach (CData data in list)
                    {
                        if (data.SEL == false) continue;


                        elineno++;

                        // TIE_H0802
                        if (bk_itemcd == "" || bk_itemcd != data.ITEMCD)
                        {
                            prodno = elineno.ToString();
                            bk_itemcd = data.ITEMCD;

                            sql = "";
                            sql += Environment.NewLine + "INSERT INTO TIE_H0802(DEMYM,PRODNO,PRODCM,ITEMCD,ITEMINFO,STDSIZE,UNIT,LINEFG,ADDAVR,FSTBUYFG)";
                            sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";

                            para.Clear();
                            para.Add(demym);
                            para.Add(prodno);
                            para.Add(data.PRODCM);
                            para.Add(data.ITEMCD);
                            para.Add(data.ITEMINFO);
                            para.Add(data.STDSIZE);
                            para.Add(data.UNIT);
                            para.Add("");
                            para.Add(data.ADDAVR);
                            para.Add(data.FSTBUYFG);

                            MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);
                        }

                        // TIE_H0803
                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TIE_H0803(DEMYM,ELINENO,BUSINESSCD,TRADENM,BUYDT,BUYQTY,BUYAMT,BUYTOTAMT,TRADEMEMO,UNITFGNO)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?)";

                        para.Clear();
                        para.Add(demym);
                        para.Add(elineno);
                        para.Add(data.BUSSCD);
                        para.Add(data.BUSSNM);
                        para.Add(data.BUYDT);
                        para.Add(data.BUYQTY);
                        para.Add(data.BUYAMT);
                        para.Add(data.BUYTOTAMT);
                        para.Add(data.MEMO);
                        para.Add(prodno);

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                    }

                    // 가중평균값을 구한다.
                    sql = "";
                    sql += Environment.NewLine + "UPDATE TIE_H0802";
                    sql += Environment.NewLine + "   SET ADDAVR=Z.ADDAVR";
                    sql += Environment.NewLine + "  FROM TIE_H0802 H2";
                    sql += Environment.NewLine + "     , (";
                    sql += Environment.NewLine + "        SELECT H3.DEMYM,H3.UNITFGNO,CASE WHEN SUM(H3.BUYQTY)=0 THEN 0 ELSE ROUND(SUM(H3.BUYTOTAMT)/SUM(H3.BUYQTY),0) END AS ADDAVR";
                    sql += Environment.NewLine + "          FROM TIE_H0803 H3";
                    sql += Environment.NewLine + "         WHERE H3.DEMYM='" + demym + "'";
                    sql += Environment.NewLine + "         GROUP BY H3.DEMYM,H3.UNITFGNO";
                    sql += Environment.NewLine + "       ) Z";
                    sql += Environment.NewLine + " WHERE H2.DEMYM='" + demym + "'";
                    sql += Environment.NewLine + "   AND H2.DEMYM=Z.DEMYM";
                    sql += Environment.NewLine + "   AND H2.PRODNO=Z.UNITFGNO";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);

                    // TIE_H0801
                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TIE_H0801(DEMYM,DEMSEQ,HOSID,DEMNM,ACPNM,DEMDD,TOTCNT,MEMO,SNDDT,MULTIHSFG,YHGBN)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

                    para.Clear();
                    para.Add(demym);
                    para.Add("H080");
                    para.Add(txtHosid.Text.ToString());
                    para.Add(txtDemnm.Text.ToString());
                    para.Add(txtAcpnm.Text.ToString());
                    para.Add(GetDemdd());
                    para.Add(elineno);
                    para.Add("");
                    para.Add(txtSnddt.Text.ToString());
                    para.Add(m_HospMulti);
                    para.Add(GetYHgbn());

                    MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
            return demym;
        }

        private void MakeEdiFile(string demym)
        {
            if (Directory.Exists(txtFolder.Text.ToString()) == false)
            {
                Directory.CreateDirectory(txtFolder.Text.ToString());
            }

            if (Directory.Exists(txtFolder.Text.ToString()))
            {
                string[] files = Directory.GetFiles(txtFolder.Text.ToString());
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string deletefile = txtFolder.Text.ToString() + "/" + fileName;
                    File.Delete(deletefile);
                }
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                this.MakeH0801(demym, conn);
                this.MakeH0802(demym, conn);
                this.MakeH0803(demym, conn);
                this.MakeMedlogenc(demym, conn);
            }
        }

        private void MakeH0801(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.1", true, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMYM,DEMSEQ,HOSID,DEMNM,ACPNM,DEMDD,TOTCNT,MEMO";
            sql += System.Environment.NewLine + "  FROM TIE_H0801 ";
            sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                sb.Length = 0;
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["DEMSEQ"].ToString(), "C", 4));
                sb.Append(Conv(reader["HOSID"].ToString(), "C", 8));
                sb.Append(Conv(reader["DEMNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["ACPNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["DEMDD"].ToString(), "C", 6));
                sb.Append(Conv(reader["TOTCNT"].ToString(), "C", 5));
                sb.Append(Conv(reader["MEMO"].ToString(), "C", 1750));

                String strLine = sb.ToString();
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeH0802(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.2", true, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += Environment.NewLine + "SELECT DEMYM,PRODNO,PRODCM,ITEMCD,ITEMINFO,STDSIZE,UNIT,LINEFG,ADDAVR,FSTBUYFG";
            sql += Environment.NewLine + "  FROM TIE_H0802 ";
            sql += System.Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
            sql += Environment.NewLine + " ORDER BY CONVERT(NUMERIC,PRODNO)";

            int no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                sb.Length = 0;
                if (++no > 1) sb.Append('\n');
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["PRODNO"].ToString(), "N", 5));
                sb.Append(Conv(reader["PRODCM"].ToString(), "C", 35));
                sb.Append(Conv(reader["ITEMCD"].ToString(), "C", 9));
                sb.Append(Conv(reader["ITEMINFO"].ToString(), "C", 140));
                sb.Append(Conv(reader["STDSIZE"].ToString(), "C", 140));
                sb.Append(Conv(reader["UNIT"].ToString(), "C", 140));
                sb.Append(Conv(reader["LINEFG"].ToString(), "C", 140));
                sb.Append(Conv(reader["ADDAVR"].ToString(), "N", 10));
                sb.Append(Conv(reader["FSTBUYFG"].ToString(), "C", 1));

                String strLine = sb.ToString();
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeH0803(string demym, OleDbConnection p_conn)
        {
            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H080.3", true, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += Environment.NewLine + "SELECT DEMYM,ELINENO,BUSINESSCD,TRADENM,BUYDT,BUYQTY * 100 AS BUYQTY,BUYAMT,BUYTOTAMT,TRADEMEMO,UNITFGNO";
            sql += Environment.NewLine + "  FROM TIE_H0803 ";
            sql += Environment.NewLine + " WHERE DEMYM = '" + demym + "'";
            sql += Environment.NewLine + " ORDER BY CONVERT(NUMERIC,UNITFGNO), ELINENO";

            int no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                sb.Length = 0;
                if (++no > 1) sb.Append('\n');
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["ELINENO"].ToString(), "N", 5));
                sb.Append(Conv(reader["BUSINESSCD"].ToString(), "C", 17));
                sb.Append(Conv(reader["TRADENM"].ToString(), "C", 35));
                sb.Append(Conv(reader["BUYDT"].ToString(), "C", 8));
                sb.Append(Conv(MetroLib.StrHelper.ToLong(reader["BUYQTY"].ToString()), "N2", 10));
                sb.Append(Conv(reader["BUYAMT"].ToString(), "N", 10));
                sb.Append(Conv(reader["BUYTOTAMT"].ToString(), "N", 10));
                sb.Append(Conv(reader["TRADEMEMO"].ToString(), "C", 140));
                sb.Append(Conv(reader["UNITFGNO"].ToString(), "N", 5));

                String strLine = sb.ToString();
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeMedlogenc(string demym, OleDbConnection p_conn)
        {
            String strRcvid = txtRcvid.Text.ToString();

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/MEDLOG.ENC", true, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            sb.Length = 0;
            sb.Append(Conv(strRcvid, "C", 12));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("NULL", "C", 8));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("의약품구입내역 목록표", "C", 30));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("", "C", 12));

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private String Conv(String value, String Type, int Len)
        {
            if (Type == "N" || Type == "N2")
            {
                String ret = value.PadLeft(Len, '0');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
            else
            {
                String ret = value.PadRight(Len, ' ');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
        }

        private String Conv(long val, String Type, int Len)
        {
            String value = val.ToString();
            if (Type == "N" || Type == "N2")
            {
                String ret = value.PadLeft(Len, '0');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
            else
            {
                String ret = value.PadRight(Len, ' ');
                return MetroLib.StrHelper.SubstringH(ret, 0, Len);
            }
        }

        private void cboPosOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPosOption.SelectedIndex == 1)
            {
                // 심평원
                txtFolder.Text = "C:/hira/DDMD/sam/in";
            }
            else if (cboPosOption.SelectedIndex == 2)
            {
                // KT
                txtFolder.Text = "C:/KTEDI/TR/Gen_in";
            }
            else
            {
                // 자동
                if (Directory.Exists("C:/KTEDI/TR/Gen_in") == true)
                {
                    txtFolder.Text = "C:/KTEDI/TR/Gen_in";
                }
                else
                {
                    txtFolder.Text = "C:/hira/DDMD/sam/in";
                }
            }
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0705E"); ;
            reg.SetValue(cboPosOption.Name, cboPosOption.SelectedIndex);

        }

        private void mnuSelFst_Click(object sender, EventArgs e)
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            foreach (CData data in list)
            {
                data.SEL = data.FSTBUYFG == "A";
            }
            RefreshGridMain();
        }

        private void mnuEraFst_Click(object sender, EventArgs e)
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            foreach (CData data in list)
            {
                if (data.FSTBUYFG == "A") data.FSTBUYFG = "";
            }
            RefreshGridMain();
        }

        private void btnRcvid_Click(object sender, EventArgs e)
        {
            if (panRcvid.Visible == true)
            {
                panRcvid.Visible = false;
            }
            else
            {
                panRcvid.Left = btnRcvid.Left - panRcvid.Width + btnRcvid.Width;
                panRcvid.Top = btnRcvid.Top - panRcvid.Height;

                panRcvid.Visible = true;
            }
        }

        private void btnRcvidCancel_Click(object sender, EventArgs e)
        {
            panRcvid.Visible = false;
        }

        private void btnRcvidSel_Click(object sender, EventArgs e)
        {
            try
            {
                String strRcvid = cboRcvid.SelectedItem.ToString();
                strRcvid = (strRcvid + "-").Split('-')[0];

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql += Environment.NewLine + "UPDATE TA88 SET FLD1QTY = '" + strRcvid + "' ";
                    sql += Environment.NewLine + " WHERE MST1CD='A' ";
                    sql += Environment.NewLine + "   AND MST2CD='HOSPITAL" + m_HospMulti + "' ";
                    sql += Environment.NewLine + "   AND MST3CD='37' ";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn);
                }
                txtRcvid.Text = strRcvid;

                panRcvid.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

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

//using Microsoft.Win32;

namespace ADD0707E
{
    public partial class ADD0707E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox demdivComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox sndivComboBox;

        private string m_DupChkFg;
        private string m_DupChkDemno;

        private bool IsFirst;
        private bool OnPgm;

        public ADD0707E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            MakeComboInGrid();

            OnPgm = false;
        }

        public ADD0707E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            CreatePopupMenu();
            ReadConfig();
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            demdivComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            sndivComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

            grdMainView.Columns["DEMDIVNM"].ColumnEdit = demdivComboBox;
            grdMainView.Columns["SNDIVNM"].ColumnEdit = sndivComboBox;

            demdivComboBox.Items.Clear();
            demdivComboBox.Items.Add("치료재료");
            demdivComboBox.Items.Add("원료약");
            demdivComboBox.Items.Add("비급여약제");

            sndivComboBox.Items.Clear();
            sndivComboBox.Items.Add("");
            sndivComboBox.Items.Add("선납");
            sndivComboBox.Items.Add("2년경과");

        }

        private void CreatePopupMenu()
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("신고 History 조회", new EventHandler(mnuHistory_Click));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("행삽입", new EventHandler(mnuInsRow_Click));
            cm.MenuItems.Add("행삭제", new EventHandler(mnuDelRow_Click));
            grdMain.ContextMenu = cm;

            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("통보서 삭제",null, new EventHandler(mnuDel_Click));
            lblDemno.ContextMenuStrip = cms;
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

        private void ReadConfig()
        {
            OnPgm = true;

            m_DupChkFg = "";
            m_DupChkDemno = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT FLD2QTY,FLD3QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='231'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_DupChkFg = reader["FLD2QTY"].ToString();
                        m_DupChkDemno = reader["FLD3QTY"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 의료기관기호
                    sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD = 'A' AND MST2CD = 'HOSPITAL" + m_HospMulti + "' AND MST3CD = '2'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtHosid.Text = reader["FLD1QTY"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 로그인 사용자명
                    sql = "SELECT USRNM FROM TA94 WHERE USRID = '" + m_User + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        txtDdnm.Text = reader["USRNM"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 수신인(저장된)
                    cboRcvid.Items.Clear();
                    String strRcvid = "";
                    sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='37'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        strRcvid = reader["FLD1QTY"].ToString();
                        return MetroLib.SqlHelper.CONTINUE;
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
                        if (mst3cd == strRcvid) cboRcvid.SelectedIndex = i - 1;
                        return MetroLib.SqlHelper.CONTINUE;
                    });

                    // 파일저장 폴더
                    String folder = MetroLib.RegHelper.GetValue("ADD0707E", txtFolder.Name, "");
                    if (folder == "") folder = "C:/hira/DDMD/sam/in";
                    txtFolder.Text = folder;

                    //행추가시 선납품으로 설정
                    chkSnDiv.Checked = MetroLib.RegHelper.GetValue("ADD0707E", chkSnDiv.Name, "") == "true";

                    //구입가와 구입량으로 단가 계산
                    chkCalcFg.Checked = MetroLib.RegHelper.GetValue("ADD0707E", chkCalcFg.Name, "") == "true";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                OnPgm = false;
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDemno.Visible == true)
                {
                    lstDemno.Hide();
                }
                else
                {
                    this.SetDemnoListBox();
                    lstDemno.Top = txtDemno.Top + txtDemno.Height;
                    lstDemno.Left = txtDemno.Left;
                    lstDemno.Show();
                    lstDemno.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDemnoListBox()
        {
            lstDemno.Items.Clear();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT DEMNO";
                sql += System.Environment.NewLine + "  FROM TIE_H0601";
                sql += System.Environment.NewLine + " WHERE ISNULL(MULTIHSFG,'') = '" + m_HospMulti + "' ";
                sql += System.Environment.NewLine + " ORDER BY DEMNO DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    lstDemno.Items.Add(reader["DEMNO"].ToString());
                    return true;
                });

                conn.Close();
            }
        }

        private void lstDemno_DoubleClick(object sender, EventArgs e)
        {
            if (lstDemno.SelectedItem != null)
            {
                string demno = lstDemno.SelectedItem.ToString();
                lstDemno.Hide();
                Application.DoEvents();
                txtDemno.Text = demno;
            }
        }

        private void txtDemno_TextChanged(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
            if (grdMainView.RowCount > 0) grdMain.Focus();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
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
            // 일단 화면의 내용을 지운다.
            txtHosid.Text = "";
            txtFmno.Text = "";
            txtDdnm.Text = "";
            txtTotcnt.Text = "";
            txtMemo.Text = "";
            txtSenddt.Text = "";


            grdMain.DataSource = null;
            RefreshGridMain();
            grdTI09.DataSource = null;
            RefreshGridTI09();

            // 신청번호 10자리가 아니면 종료
            string strDemno = txtDemno.Text.ToString();
            if (strDemno == "" || strDemno.Length < 10) return;

            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                int demno_cnt = 0;
                string demno = txtDemno.Text.ToString();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT FMNO,HOSID,DDNM,TOTCNT,MEMO,SNDDT";
                sql += System.Environment.NewLine + "  FROM TIE_H0601";
                sql += System.Environment.NewLine + " WHERE DEMNO = '" + demno + "'";


                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    txtHosid.Text = reader["HOSID"].ToString();
                    txtFmno.Text = reader["FMNO"].ToString();
                    txtDdnm.Text = reader["DDNM"].ToString();
                    txtTotcnt.Text = reader["TOTCNT"].ToString();
                    txtMemo.Text = reader["MEMO"].ToString();
                    txtSenddt.Text = reader["SNDDT"].ToString();

                    demno_cnt++;
                    return true;
                });

                if (demno_cnt <= 0)
                {
                    // 조회되는 내용이 없으면 신규 번호임.

                    txtHosid.Text = GetHosid(conn); // 의료기관코드
                    txtFmno.Text = "H060";
                    txtDdnm.Text = GetEmpnm(conn); // 작성자 명
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.DEMDIV,A.ITEMCD,A.ITEMINFO,A.PTYPE,A.PDUT,A.BUSINESSCD,A.TRADENM,A.SNDIV,A.BUYDT,A.BUYQTY,A.BUYAMT,A.EADANGA";
                    sql += System.Environment.NewLine + "     , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=A.ITEMCD AND ISNULL(EXPDT,'') ='' AND ISNULL(REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += System.Environment.NewLine + "     , B.RCVRESID,B.RCVNM,B.USEDT,A.ELINENO";
                    sql += System.Environment.NewLine + "  FROM TIE_H0602 A INNER JOIN TIE_H0603 B ON B.DEMNO = A.DEMNO";
                    sql += System.Environment.NewLine + "                                         AND B.DEMDIV = A.DEMDIV";
                    sql += System.Environment.NewLine + "                                         AND B.ELINENO = A.ELINENO";
                    sql += System.Environment.NewLine + " WHERE A.DEMNO = '" + demno + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.ELINENO";


                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        CData data = new CData();
                        data.Clear();

                        data.DEMDIVNM = data.GetDEMDIVNM(reader["DEMDIV"].ToString());
                        data.ITEMCD = reader["ITEMCD"].ToString();
                        data.ITEMINFO = reader["ITEMINFO"].ToString();
                        data.PTYPE = reader["PTYPE"].ToString();
                        data.PDUT = reader["PDUT"].ToString();
                        data.BUSINESSCD = reader["BUSINESSCD"].ToString();
                        data.TRADENM = reader["TRADENM"].ToString();
                        data.SNDIVNM = data.GetSNDiVNM(reader["SNDIV"].ToString());
                        data.BUYDT = reader["BUYDT"].ToString();
                        data.BUYQTY = MetroLib.StrHelper.ToLong(reader["BUYQTY"].ToString());
                        data.BUYAMT = MetroLib.StrHelper.ToLong(reader["BUYAMT"].ToString());
                        data.EADANGA = MetroLib.StrHelper.ToLong(reader["EADANGA"].ToString());
                        data.PRICD = reader["PRICD"].ToString();
                        data.RCVRESID = reader["RCVRESID"].ToString();
                        data.RCVNM = reader["RCVNM"].ToString();
                        data.USEDT = reader["USEDT"].ToString();
                        data.ELINENO = reader["ELINENO"].ToString();

                        string expdt = "";
                        string ispcd = GetIspcdByPricd(data.PRICD, ref expdt);
                        data.EXPDT = expdt;


                        list.Add(data);

                        return true;
                    });
                }
            }

            RefreshGridMain();
        }

        private string GetHosid(OleDbConnection p_conn)
        {
            string ret = "";
            // 의료기관코드
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT FLD1QTY";
            sql += System.Environment.NewLine + "  FROM TA88";
            sql += System.Environment.NewLine + " WHERE MST1CD = 'A'";
            sql += System.Environment.NewLine + " AND MST2CD = 'HOSPITAL" + m_HospMulti + "'";
            sql += System.Environment.NewLine + " AND MST3CD = '2'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FLD1QTY"].ToString();
                return true;
            });

            return ret;
        }

        private string GetEmpnm(OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT EMPNM FROM TA13 WHERE EMPID='" + m_User + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["EMPNM"].ToString();
                return true;
            });

            return ret;
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtDemno.Text = this.GetNewDemno();
        }

        private string GetNewDemno()
        {
            // txtDemno의 TextChanged 이벤트에서 데이터베이스르 읽고 작업함.
            //            데이터 베이스를 중복으로 연결하지 않기 위해 데이터베이스 연결을 끊은뒤
            //            txtDemno에 값을 넣음.
            try
            {
                string demno = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    demno = MetroLib.Util.GetSysDate(conn) + MetroLib.Util.GetSysTime(conn).Substring(0, 4);

                    // 새로만들 번호가 있는지 점검해본다. 이미 있으면 번호를 바꾸어야 한다.
                    while (true)
                    {
                        int demno_cnt = 0;

                        string sql = "";
                        sql = "";
                        sql += System.Environment.NewLine + "SELECT FMNO,HOSID,DDNM,TOTCNT,MEMO,SNDDT";
                        sql += System.Environment.NewLine + "  FROM TIE_H0601";
                        sql += System.Environment.NewLine + " WHERE DEMNO = '" + demno + "'";

                        MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                        {
                            demno_cnt++;
                            return true;
                        });

                        if (demno_cnt <= 0) break; // 없음. 사용하자.

                        // 신청번호 변경
                        demno = (MetroLib.StrHelper.ToLong(demno) + 1).ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                if (IsErrorInput() == true) return;
                
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

        private bool IsErrorInput()
        {
            bool ret = true;

            if (txtDdnm.Text.ToString() == "")
            {
                MessageBox.Show("담당자는 필수 입력 항목입니다.");
                return ret;
            }

            if (grdMainView.RowCount < 1)
            {
                MessageBox.Show("신고 품목 코드는 하나 이상 들어가야 합니다.");
                return ret;
            }

            for (int lngRow = 0; lngRow < grdMainView.RowCount; lngRow++)
            {
                string demdivnm = grdMainView.GetRowCellValue(lngRow, "DEMDIVNM").ToString();
                if (demdivnm == "")
                {
                    MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "신고서구분을 선택하지 않았습니다.");
                    return ret;
                }
                string itemcd = grdMainView.GetRowCellValue(lngRow, "ITEMCD").ToString();
                if (itemcd == "")
                {
                    MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "품목코드를 입력 하지 않았습니다.");
                    return ret;
                }
                string iteminfo1 = grdMainView.GetRowCellValue(lngRow, "ITEMINFO1").ToString();
                if (iteminfo1 == "")
                {
                    MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "품명을 입력 하지 않았습니다.");
                    return ret;
                }
                string buydt = grdMainView.GetRowCellValue(lngRow, "BUYDT").ToString();
                if (buydt != "")
                {
                    if (buydt.Length != 8)
                    {
                        MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "구입일자가 입력되었으나 날짜형식이 아닙니다.");
                        return ret;
                    }
                    if (MetroLib.Util.ValDt(buydt) == false)
                    {
                        MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "구입일자가 입력되었으나 날짜형식이 아닙니다.");
                        return ret;
                    }
                }
                string usedt = grdMainView.GetRowCellValue(lngRow, "USEDT").ToString();
                if (usedt != "")
                {
                    if (usedt.Length != 8)
                    {
                        MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "사용일자가 입력되었으나 날짜형식이 아닙니다.");
                        return ret;
                    }
                    if (MetroLib.Util.ValDt(usedt) == false)
                    {
                        MessageBox.Show("줄번호 : " + (lngRow + 1) + Environment.NewLine + "사용일자가 입력되었으나 날짜형식이 아닙니다.");
                        return ret;
                    }
                }
            }

            ret = false; // 오류없음.
            return ret;
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

                    sql = "DELETE FROM TIE_H0601 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0602 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0603 WHERE DEMNO='" + p_demno + "'";
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
                        sql += Environment.NewLine + "INSERT INTO TIE_H0602(DEMNO,DEMDIV,ELINENO,ITEMCD,ITEMINFO,PTYPE,PDUT,BUSINESSCD,TRADENM,SNDIV,BUYDT,BUYQTY,BUYAMT,EADANGA)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                            cmd.Parameters.Add(new OleDbParameter("@2", data.DEMDIV));
                            cmd.Parameters.Add(new OleDbParameter("@3", lineno));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.ITEMCD));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.ITEMINFO));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.PTYPE));
                            cmd.Parameters.Add(new OleDbParameter("@7", data.PDUT));
                            cmd.Parameters.Add(new OleDbParameter("@8", data.BUSINESSCD));
                            cmd.Parameters.Add(new OleDbParameter("@9", data.TRADENM));
                            cmd.Parameters.Add(new OleDbParameter("@10", data.SNDIV));
                            cmd.Parameters.Add(new OleDbParameter("@11", data.BUYDT));
                            cmd.Parameters.Add(new OleDbParameter("@12", data.BUYQTY));
                            cmd.Parameters.Add(new OleDbParameter("@13", data.BUYAMT));
                            cmd.Parameters.Add(new OleDbParameter("@14", data.EADANGA));
                            cmd.ExecuteNonQuery();
                        }

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TIE_H0603(DEMNO,DEMDIV,ELINENO,RCVRESID,RCVNM,USEDT)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?)";
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                        {
                            cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                            cmd.Parameters.Add(new OleDbParameter("@2", data.DEMDIV));
                            cmd.Parameters.Add(new OleDbParameter("@3", lineno));
                            cmd.Parameters.Add(new OleDbParameter("@4", data.RCVRESID));
                            cmd.Parameters.Add(new OleDbParameter("@5", data.RCVNM));
                            cmd.Parameters.Add(new OleDbParameter("@6", data.USEDT));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TIE_H0601(DEMNO,FMNO,HOSID,DDNM,TOTCNT,MEMO,MULTIHSFG,SNDDT)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_demno));
                        cmd.Parameters.Add(new OleDbParameter("@2", txtFmno.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@3", txtHosid.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@4", txtDdnm.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@5", lineno));
                        cmd.Parameters.Add(new OleDbParameter("@6", txtMemo.Text.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@7", m_HospMulti));
                        cmd.Parameters.Add(new OleDbParameter("@8", txtSenddt.Text.ToString()));
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

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                if (IsErrorInput() == true) return;

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

        /* 2024.05.14 WOOIL - 바르다에서 실수로 내역을 삭제하여 버튼을 없애달라고 함
         *                    혹시나 해서 코딩은 남겨둠
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
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        */

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

                    sql = "DELETE FROM TIE_H0601 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0602 WHERE DEMNO='" + p_demno + "'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = "DELETE FROM TIE_H0603 WHERE DEMNO='" + p_demno + "'";
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

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            AddRow();
            grdMainView.FocusedRowHandle = grdMainView.RowCount - 1;
            grdMainView.FocusedColumn = grdMainView.Columns["ITEMCD"];// gcDEMDIVNM;
            grdMainView.ClearSelection();
            grdMainView.SelectCell(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn);
            grdMain.Focus();

            CopyValues();
            RefreshGridMain();

            grdTI09.DataSource = null;
            RefreshGridTI09();
        }

        private void AddRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Add(data);
        }

        private void btnInsRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            int rowHandle = grdMainView.FocusedRowHandle;

            InsRow();
            grdMainView.FocusedColumn = grdMainView.Columns["ITEMCD"];// gcDEMDIVNM;
            grdMain.Focus();

            CopyValues();
            grdMainView.FocusedRowHandle = rowHandle;
            grdMainView.ClearSelection();
            grdMainView.SelectCell(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn);
            RefreshGridMain();

            grdTI09.DataSource = null;
            RefreshGridTI09();
        }

        private void InsRow()
        {
            if (txtDemno.Text.ToString() == "") return;

            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = new CData();
            data.Clear();
            list.Insert(grdMainView.FocusedRowHandle, data);
        }

        private void CopyValues()
        {
            int rowHandle = grdMainView.FocusedRowHandle;
            if (rowHandle <= 0) return; // 첫 줄이면 종료

            OnPgm = true;

            string demdivnm = grdMainView.GetRowCellValue(rowHandle - 1, "DEMDIVNM").ToString();
            string businesscd = grdMainView.GetRowCellValue(rowHandle - 1, "BUSINESSCD").ToString();
            string tradenm = grdMainView.GetRowCellValue(rowHandle - 1, "TRADENM").ToString();
            string sndivnm = grdMainView.GetRowCellValue(rowHandle - 1, "SNDIVNM").ToString();
            string buydt = grdMainView.GetRowCellValue(rowHandle - 1, "BUYDT").ToString();

            grdMainView.SetRowCellValue(rowHandle, "DEMDIVNM", demdivnm);
            grdMainView.SetRowCellValue(rowHandle, "BUSINESSCD", businesscd);
            grdMainView.SetRowCellValue(rowHandle, "TRADENM", tradenm);
            grdMainView.SetRowCellValue(rowHandle, "SNDIVNM", sndivnm);
            grdMainView.SetRowCellValue(rowHandle, "BUYDT", buydt);

            if (chkSnDiv.Checked)
            {
                grdMainView.SetRowCellValue(rowHandle, "SNDIVNM", "선납");
            }

            OnPgm = false;
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
            grdMainView.FocusedColumn = grdMainView.Columns["ITEMCD"];// gcDEMDIVNM;
            grdMain.Focus();
            RefreshGridMain();
        }

        private void btnSaveSenddt_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "송신일자 저장 중입니다.");
                this.SaveSenddt(demno);
                this.CloseProgressForm("", "송신일자 저장 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "송신일자 저장 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveSenddt(string p_demno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "UPDATE TIE_H0601 SET SNDDT='" + txtSenddt.Text.ToString() + "' WHERE DEMNO='" + p_demno + "'";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);
            }
        }

        private void grdMainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.Column.FieldName == "ITEMCD" || e.Column.FieldName == "BUYDT")
            {
                if (OnPgm == true) return;
                //string itemcd = (string)e.Value;
                string itemcd = gridView.GetRowCellValue(e.RowHandle, "ITEMCD").ToString();
                string buydt = gridView.GetRowCellValue(e.RowHandle, "BUYDT").ToString();
                string demdivnm = gridView.GetRowCellValue(e.RowHandle, "DEMDIVNM").ToString();

                if (itemcd == "") return;

                string pcodenm = "";
                string mkcnm = "";
                string mkcnmk = "";
                string ptype = "";
                string pdut = "";
                string pricd = "";
                string kumak = "";

                int cnt = ReadTI09(itemcd, buydt, demdivnm, ref pcodenm, ref mkcnm, ref mkcnmk, ref ptype, ref pdut, ref pricd, ref kumak);
                if (cnt == 1)
                {
                    string expdt = "";
                    if (e.Column.FieldName == "ITEMCD")
                    {
                        // 구입일자 칸에서는 동작하지 않게 한다.
                        if (pricd == "")
                        {
                            MessageBox.Show("원내수가가 만들어지지 않은 코드입니다.");
                        }
                        else
                        {
                            string ispcd = GetIspcdByPricd(pricd, ref expdt);
                            if (ispcd != itemcd)
                            {
                                MessageBox.Show("더 이상 사용되지 않는 코드입니다.");
                            }
                        }
                    }

                    grdMainView.SetRowCellValue(e.RowHandle, "ITEMINFO1", pcodenm);
                    grdMainView.SetRowCellValue(e.RowHandle, "ITEMINFO2", mkcnm);
                    grdMainView.SetRowCellValue(e.RowHandle, "ITEMINFO3", mkcnmk);
                    grdMainView.SetRowCellValue(e.RowHandle, "PTYPE", ptype);
                    grdMainView.SetRowCellValue(e.RowHandle, "PDUT", pdut);
                    grdMainView.SetRowCellValue(e.RowHandle, "PRICD", pricd);
                    if (chkCalcFg.Checked == false)
                    {
                        // EDI단가를 사용
                        grdMainView.SetRowCellValue(e.RowHandle, "EADANGA", MetroLib.StrHelper.ToLong(kumak));
                    }
                    grdMainView.SetRowCellValue(e.RowHandle, "EXPDT", expdt);

                    if (e.Column.FieldName == "ITEMCD")
                    {
                        // 구입일자 칸에서는 동작하지 않게 한다.
                        ReadHx(e.RowHandle);
                        DupCheck(e.RowHandle);
                    }
                }
                else
                {
                    if (e.Column.FieldName == "ITEMCD")
                    {
                        // 구입일자에서는 동작하지 않도록 함.
                        if (PopupEdicode(e.RowHandle, itemcd, buydt, demdivnm) == true)
                        {
                            ReadHx(e.RowHandle);
                            DupCheck(e.RowHandle);
                        }

                    }
                }
            }
            else if (e.Column.FieldName == "BUSINESSCD")
            {
                if (OnPgm == true) return;
                string in_busscd = (string)e.Value;
                if (in_busscd == "")
                {
                    gridView.SetRowCellValue(e.RowHandle, "TRADENM", "");
                }
                else
                {
                    string businesscd = "";
                    string tradenm = "";
                    int cnt = GetTRADENM(in_busscd, ref businesscd, ref tradenm);
                    if (cnt == 1)
                    {
                        // 조회된 자료가 1건이면 사용한다.
                        OnPgm = true;
                        gridView.SetRowCellValue(e.RowHandle, "BUSINESSCD", businesscd.ToUpper()); // 이때 CellValueChanged 이벤트가 발생한다. (OnPgm 변수를 사용하는 이유)
                        OnPgm = false;
                        gridView.SetRowCellValue(e.RowHandle, "TRADENM", tradenm);
                    }
                    else
                    {
                        // 조회된 자료가 없거나 2건 이상이면 팝업창을 띄워서 선택하게 한다.
                        ADD0707E_2 f = new ADD0707E_2();
                        f.m_busscd = (cnt == 0 ? "" : in_busscd); // 조회되는 건이 없으면 팝업창에 모두 조회하게 하기 위해.
                        f.ShowDialog(this);
                        if (f.m_out_sel == true)
                        {
                            OnPgm = true;
                            gridView.SetRowCellValue(e.RowHandle, "BUSINESSCD", f.m_out_busscd); // 이때 CellValueChanged 이벤트가 발생한다. (OnPgm 변수를 사용하는 이유)
                            OnPgm = false;
                            gridView.SetRowCellValue(e.RowHandle, "TRADENM", f.m_out_tradenm);
                        }
                    }
                }
                DupCheck(e.RowHandle);
            }
            else if (e.Column.FieldName == "BUYDT")
            {
                DupCheck(e.RowHandle);
            }
            else if (e.Column.FieldName == "BUYQTY" || e.Column.FieldName == "BUYAMT" || e.Column.FieldName == "EADANGA")
            {
                if (chkCalcFg.Checked == true)
                {
                    // 구입가와 구입량으로 단가 계산
                    if (e.Column.FieldName == "BUYQTY" || e.Column.FieldName == "BUYAMT")
                    {
                        long buyamt = MetroLib.StrHelper.ToLong(gridView.GetRowCellValue(e.RowHandle, "BUYAMT").ToString());
                        long buyqty = MetroLib.StrHelper.ToLong(gridView.GetRowCellValue(e.RowHandle, "BUYQTY").ToString());
                        long eadange = 0;
                        if (buyqty != 0) eadange = (long)((double)buyamt / (double)buyqty + 0.5);
                        gridView.SetRowCellValue(e.RowHandle, "EADANGA", eadange);
                    }
                }
                else
                {
                    // 단가와 구입량으로 구입가 계산
                    if (e.Column.FieldName == "BUYQTY" || e.Column.FieldName == "EADANGA")
                    {
                        long buyqty = MetroLib.StrHelper.ToLong(gridView.GetRowCellValue(e.RowHandle, "BUYQTY").ToString());
                        long eadanga = MetroLib.StrHelper.ToLong(gridView.GetRowCellValue(e.RowHandle, "EADANGA").ToString());
                        long buyamt = buyqty * eadanga;
                        gridView.SetRowCellValue(e.RowHandle, "BUYAMT", buyamt);
                    }
                }
            }
        }

        private bool PopupEdicode(int rowHandle, string itemcd, string buydt, string demdivnm)
        {
            bool ret = false;
            ADD0707E_1 f = new ADD0707E_1();
            f.m_in_pcode = itemcd;
            f.m_in_buydt = buydt;
            f.m_in_demdivnm = demdivnm;
            f.ShowDialog(this);
            if (f.m_out_sel == true)
            {
                string expdt = "";
                if (f.m_out_pricd == "")
                {
                    MessageBox.Show("원내수가가 만들어지지 않은 코드입니다.");
                }
                else
                {
                    string ispcd = GetIspcdByPricd(f.m_out_pricd, ref expdt);
                    if (ispcd != f.m_out_pcode)
                    {
                        MessageBox.Show("더 이상 사용되지 않는 코드입니다.");
                    }
                }

                OnPgm = true;
                grdMainView.SetRowCellValue(rowHandle, "ITEMCD", f.m_out_pcode);
                OnPgm = false;
                grdMainView.SetRowCellValue(rowHandle, "ITEMINFO1", f.m_out_pcodenm);
                grdMainView.SetRowCellValue(rowHandle, "ITEMINFO2", f.m_out_mkcnm);
                grdMainView.SetRowCellValue(rowHandle, "ITEMINFO3", f.m_out_mkcnmk);
                grdMainView.SetRowCellValue(rowHandle, "PTYPE", f.m_out_ptype);
                grdMainView.SetRowCellValue(rowHandle, "PDUT", f.m_out_pdut);
                grdMainView.SetRowCellValue(rowHandle, "PRICD", f.m_out_pricd);
                if (chkCalcFg.Checked == false)
                {
                    // EDI단가를 사용
                    grdMainView.SetRowCellValue(rowHandle, "EADANGA", MetroLib.StrHelper.ToLong(f.m_out_kumak));
                }
                grdMainView.SetRowCellValue(rowHandle, "EXPDT", expdt);
                ret = true;
            }
            return ret;
        }

        private string GetIspcdByPricd(string pricd, ref string o_expdt)
        {
            try
            {
                string ispcd = "";
                string expdt = "";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "SELECT TOP 1 ISPCD,EXPDT FROM TA02 WITH (NOLOCK) WHERE PRICD='" + pricd + "' ORDER BY CREDT DESC";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ispcd = reader["ISPCD"].ToString();
                        expdt = reader["EXPDT"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    ispcd = ispcd.Replace("\t", "");
                    ispcd = ispcd.Replace("\r", "");
                    ispcd = ispcd.Replace("\n", "");
                    ispcd = ispcd.Trim();

                    o_expdt = expdt;
                    return ispcd;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void DupCheck(int rowHandle)
        {
            // 중복 입력 여부 점검
            // 품목코드, 구입일자, 구입기관이 동한 내역이 있으면 경고창
            string chk_itemcd = grdMainView.GetRowCellValue(rowHandle, "ITEMCD").ToString();
            string chk_buydt = grdMainView.GetRowCellValue(rowHandle, "BUYDT").ToString();
            string chk_busscd = grdMainView.GetRowCellValue(rowHandle, "BUSINESSCD").ToString();

            if (chk_itemcd == "" || chk_buydt == "" || chk_busscd == "") return;

            bool find = false;
            int find_row = 0;

            for (int i = 0; i < grdMainView.RowCount; i++)
            {
                if (i == rowHandle) continue;

                string itemcd = grdMainView.GetRowCellValue(i, "ITEMCD").ToString();
                string buydt = grdMainView.GetRowCellValue(i, "BUYDT").ToString();
                string busscd = grdMainView.GetRowCellValue(i, "BUSINESSCD").ToString();

                if (itemcd == "" || buydt == "" || busscd == "") continue;

                if (chk_itemcd == itemcd && chk_buydt == buydt && chk_busscd == busscd)
                {
                    find = true;
                    find_row = i;
                    break;
                }
            }
            if (find == true)
            {
                MessageBox.Show(find_row + " 줄에 동일한 내용이 있습니다.");
                return;
            }

            // 다른 신청번호에 있는지 찾아본다.
            string demno_list = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT DEMNO";
                sql += Environment.NewLine + "  FROM TIE_H0602";
                sql += Environment.NewLine + " WHERE ITEMCD='" + chk_itemcd + "'";
                sql += Environment.NewLine + "   AND BUYDT='" + chk_buydt + "'";
                sql += Environment.NewLine + "   AND BUSINESSCD='" + chk_busscd + "'";
                sql += Environment.NewLine + "   AND DEMNO<>'" + txtDemno.Text.ToString() + "'";
                if (m_DupChkFg == "1")
                {
                    sql += Environment.NewLine + "   AND DEMNO>='" + m_DupChkDemno + "'";
                }


                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    string demno = reader["DEMNO"].ToString();
                    if (demno_list == "") demno_list = demno;
                    else demno_list = "," + demno;

                    return true;
                });
            }

            if (demno_list != "")
            {
                MessageBox.Show("다른 신청번호에 중복자료가 있습니다( 신청번호 : " + demno_list + " ).");
            }

        }

        private int ReadTI09(string itemcd, string buydt, string demdivnm, ref string o_pcodenm, ref string o_mkcnm, ref string o_mkcnmk, ref string o_ptype, ref string o_pdut, ref string o_pricd, ref string o_kumak)
        {
            int ti09_cnt = 0;

            string pcodenm = "";
            string mkcnm = "";
            string mkcnmk = "";
            string ptype = "";
            string pdut = "";
            string pricd = "";
            string kumak = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                if(buydt=="") buydt = MetroLib.Util.GetSysDate(conn);

                string sql = "";
                if (demdivnm == "비급여약제")
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT PCODENM,MKCNM,'' AS MKCNMK,PTYPE,PDUT,'' AS ADTDT,0 AS KUMAK";
                    sql += Environment.NewLine + "     , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(EXPDT,'') ='' AND ISNULL(REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "  FROM TI09_BYAK I09";
                    sql += Environment.NewLine + " WHERE I09.PCODE = '" + itemcd + "'";
                }
                else
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT PCODENM,MKCNM,MKCNMK,PTYPE,PDUT,ADTDT";
                    sql += Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN KUMAK2 ELSE KUMAK1 END AS KUMAK";
                    sql += Environment.NewLine + "     , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += Environment.NewLine + "  FROM TI09 I09";
                    sql += Environment.NewLine + " WHERE I09.PCODE = '" + itemcd + "'";
                    sql += Environment.NewLine + "   AND I09.GUBUN = '2'";
                    sql += Environment.NewLine + "   AND I09.ADTDT = (SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN AND X.ADTDT<='" + buydt + "')";
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    pcodenm = reader["PCODENM"].ToString().Replace("\r","").Replace("\t","");
                    mkcnm = reader["MKCNM"].ToString().Replace("\r", "").Replace("\t", "");
                    mkcnmk = reader["MKCNMK"].ToString().Replace("\r", "").Replace("\t", "");
                    ptype = reader["PTYPE"].ToString().Replace("\r", "").Replace("\t", "");
                    pdut = reader["PDUT"].ToString().Replace("\r", "").Replace("\t", "");
                    pricd = reader["PRICD"].ToString();
                    kumak = reader["KUMAK"].ToString();

                    ti09_cnt++;
                    return true;
                });
            }

            o_pcodenm = pcodenm;
            o_mkcnm = mkcnm;
            o_mkcnmk = mkcnmk;
            o_ptype = ptype;
            o_pdut = pdut;
            o_pricd = pricd;
            o_kumak = kumak;

            return ti09_cnt;
        }

        private int GetTRADENM(string in_busscd, ref string businesscd, ref string tradenm)
        {
            string cdnm = "";
            string fld1qty = "";
            int ret = 0;
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT COUNT(*) CNT, CDNM, FLD1QTY ";
                sql += Environment.NewLine + "  FROM TI88 ";
                sql += Environment.NewLine + " WHERE MST1CD='A' ";
                sql += Environment.NewLine + "   AND MST2CD='BUSINESSCD' ";
                sql += Environment.NewLine + "   AND REPLACE(CDNM,'-','') LIKE '" + in_busscd.Replace("-", "").ToString() + "%'";
                sql += Environment.NewLine + " GROUP BY CDNM, FLD1QTY ";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    ret++;
                    cdnm = reader["CDNM"].ToString();
                    fld1qty = reader["FLD1QTY"].ToString();
                    return true;
                });
            }
            businesscd = cdnm;
            tradenm = fld1qty;
            return ret;
        }

        private bool sortOrder = true;
        private void grdMainView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (view == null) return;
                if (view.RowCount < 1) return;

                // 2024.05.10 WOOIL - 컬럼을 클릭하면 정렬
                if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column)
                {
                    List<CData> newList = null;
                    List<CData> list = (List<CData>)grdMain.DataSource;

                    if (hi.Column.FieldName == "ITEMCD") newList = sortOrder ? list.OrderBy(p => p.ITEMCD).ToList() : list.OrderByDescending(p => p.ITEMCD).ToList();

                    if (newList != null)
                    {
                        sortOrder = !sortOrder;
                        grdMain.DataSource = newList;
                    }
                    RefreshGridMain();
                }

                // 그리드 셀이 편집 가능한 상태에서는 RowClick이나 RowCellClick 이벤트가 밝생하지 않는다.
                if (hi.InRowCell && e.Button == MouseButtons.Left)
                {
                    //FocusedRowChanged에 있음.
                    //string itemcd = view.GetRowCellValue(hi.RowHandle, "ITEMCD").ToString();
                    //string buydt = view.GetRowCellValue(hi.RowHandle, "BUYDT").ToString();
                    //string demdivnm = view.GetRowCellValue(hi.RowHandle, "DEMDIVNM").ToString();
                    //ReadHx(itemcd, demdivnm, buydt);
                }

                // click으로는 편집모드로 들어가지 못하게 막는다.
                if (hi.InRowCell && e.Button == MouseButtons.Left)
                {
                    if (e.Clicks == 1)
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ClearSelection();
                        view.SelectCell(view.FocusedRowHandle, view.FocusedColumn);

                        if (hi.Column.FieldName == "DEMDIVNM" || hi.Column.FieldName == "SNDIVNM")
                        {
                            // 클릭으로도 편집모드로 들어가게 허용
                            view.ShowEditor();
                        }
                    }
                    else if (e.Clicks == 2)
                    {
                        view.ShowEditor();
                    }

                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadHx(int rowHandle)
        {
            if (grdMainView == null) return;
            string itemcd = grdMainView.GetRowCellValue(rowHandle, "ITEMCD").ToString();
            string buydt = grdMainView.GetRowCellValue(rowHandle, "BUYDT").ToString();
            string demdivnm = grdMainView.GetRowCellValue(rowHandle, "DEMDIVNM").ToString();

            ReadHx(itemcd, demdivnm, buydt);
        }

        private void ReadHx(string itemcd, string demdivnm, string buydt)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                if (buydt == "") buydt = MetroLib.Util.GetSysDate(conn);

                ReadTI09Hx(itemcd, demdivnm, buydt, conn);
            }
        }

        private void ReadTI09Hx(string itemcd, string demdivnm, string buydt, OleDbConnection p_conn)
        {
            grdTI09.DataSource = null;
            List<CDataTI09> list = new List<CDataTI09>();
            grdTI09.DataSource = list;

            string sql = "";
            if (demdivnm == "비급여약제")
            {
                sql = "";
                sql += Environment.NewLine + "SELECT PCODENM,MKCNM,'' AS MKCNMK,PTYPE,PDUT,'' AS ADTDT,0 AS KUMAK";
                sql += Environment.NewLine + "  FROM TI09_BYAK I09";
                sql += Environment.NewLine + " WHERE I09.PCODE = '" + itemcd + "'";
            }
            else
            {
                sql = "";
                sql += Environment.NewLine + "SELECT PCODENM,MKCNM,MKCNMK,PTYPE,PDUT,ADTDT";
                sql += Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN KUMAK2 ELSE KUMAK1 END AS KUMAK";
                sql += Environment.NewLine + "  FROM TI09 I09";
                sql += Environment.NewLine + " WHERE I09.PCODE = '" + itemcd + "'";
                sql += Environment.NewLine + "   AND I09.GUBUN = '2'";
                sql += Environment.NewLine + " ORDER BY ADTDT DESC";
            }

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataTI09 data = new CDataTI09();
                data.Clear();

                data.PCODE = itemcd;
                data.ADTDT = reader["ADTDT"].ToString();
                data.PCODENM = reader["PCODENM"].ToString();
                data.MKCNM = reader["MKCNM"].ToString();
                data.MKCNMK = reader["MKCNMK"].ToString();
                data.PTYPE = reader["PTYPE"].ToString();
                data.PDUT = reader["PDUT"].ToString();
                data.KUMAK = MetroLib.StrHelper.ToLong(reader["KUMAK"].ToString());

                list.Add(data);

                return true;
            });

            RefreshGridTI09();
        }

        private void RefreshGridTI09()
        {
            if (grdTI09.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdTI09.BeginInvoke(new Action(() => grdTI09View.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdTI09View.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_HiddenEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (view.FocusedRowHandle < 0) return;

            //GoToNextColumn(view);
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "PRICD")
            {
                string expdt = grdMainView.GetRowCellValue(e.RowHandle, "EXPDT").ToString();
                if (expdt != "")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            /*
            if (e.RowHandle == grdMainView.FocusedRowHandle && e.Column.FieldName == grdMainView.FocusedColumn.FieldName)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
                foreach (DevExpress.XtraGrid.Views.Base.GridCell cell in grdMainView.GetSelectedCells())
                {
                    if (e.RowHandle == cell.RowHandle && e.Column.FieldName == cell.Column.FieldName)
                    {
                        e.Appearance.BackColor = Color.Lavender;
                    }
                }
            }
            */
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtFolder.Text.ToString();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
                MetroLib.RegHelper.SaveValue("ADD0707E", txtFolder.Name, txtFolder.Text.ToString());
            }
        }

        private void cboRcvid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPgm) return;

            try
            {
                String strRcvid = cboRcvid.SelectedItem.ToString();
                strRcvid = (strRcvid + "-").Split('-')[0];

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TA88";
                    sql += System.Environment.NewLine + "   SET FLD1QTY=?";
                    sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                    sql += System.Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                    sql += System.Environment.NewLine + "   AND MST3CD='37'";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@P1", strRcvid));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDemno.Text.ToString() == "")
                {
                    MessageBox.Show("신청번호가 없습니다.");
                    return;
                }

                if (cboRcvid.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("수신인을 선택하세요");
                    return;
                }

                if (txtFolder.Text.ToString() == "")
                {
                    MessageBox.Show("폴더를 선택하세요");
                    return;
                }

                if (Directory.Exists(txtFolder.Text.ToString()) == false)
                {
                    Directory.CreateDirectory(txtFolder.Text.ToString());
                }

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeSamFile();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("생성이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

        }

        private void MakeSamFile()
        {
            string demno = txtDemno.Text.ToString();

            DeleteAllFile();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                this.MakeH0601(demno, conn);
                this.MakeH0602(demno, conn);
                this.MakeH0603(demno, conn);
                this.MakeMedlogenc(demno, conn);
            }
        }

        private void DeleteAllFile()
        {
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
        }

        private void MakeH0601(string demno, OleDbConnection p_conn)
        {
            string h0601 = "/H060.1";
            if (chkJabo.Checked == true) h0601 = "/C060.1";

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + h0601, false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMNO,FMNO,HOSID,DDNM,TOTCNT,MEMO";
            sql += System.Environment.NewLine + "  FROM TIE_H0601";
            sql += System.Environment.NewLine + " WHERE DEMNO= '" + demno + "'";



            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string fmno = reader["FMNO"].ToString();
                if (chkJabo.Checked == true) fmno = "C060";

                sb.Length = 0;
                sb.Append(Conv(reader["DEMNO"].ToString(), "C", 12));
                sb.Append(Conv(fmno, "C", 4));
                sb.Append(Conv(reader["HOSID"].ToString(), "C", 8));
                sb.Append(Conv(reader["DDNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["TOTCNT"].ToString(), "N", 4));
                sb.Append(Conv(reader["MEMO"].ToString(), "C", 1750));

                return true;
            });

            String strLine = sb.ToString();
            sw.Write(strLine);
            sw.Close();
        }

        private void MakeH0602(string demno, OleDbConnection p_conn)
        {
            string h0602 = "/H060.2";
            if (chkJabo.Checked == true) h0602 = "/C060.2";

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + h0602, false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMNO,DEMDIV,ELINENO,ITEMCD,ITEMINFO,PTYPE,PDUT,BUSINESSCD,TRADENM,SNDIV,BUYDT,BUYQTY,BUYAMT,EADANGA";
            sql += System.Environment.NewLine + "  FROM TIE_H0602";
            sql += System.Environment.NewLine + " WHERE DEMNO= '" + demno + "'";
            sql += System.Environment.NewLine + " ORDER BY DEMNO,ELINENO";


            int lineno = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                lineno++;
                sb.Length = 0;
                sb.Append(Conv(reader["DEMNO"].ToString(), "C", 12));
                sb.Append(Conv(reader["DEMDIV"].ToString(), "C", 1));
                sb.Append(Conv(reader["ELINENO"].ToString(), "N", 3));
                sb.Append(Conv(reader["ITEMCD"].ToString(), "C", (chkJabo.Checked == true ? 9 : 8))); // 자보는 품목코드가 9자리이다.
                sb.Append(Conv(reader["ITEMINFO"].ToString(), "C", 140));
                sb.Append(Conv(reader["PTYPE"].ToString(), "C", 140));
                sb.Append(Conv(reader["PDUT"].ToString(), "C", 70));
                sb.Append(Conv(reader["BUSINESSCD"].ToString(), "C", 17));
                sb.Append(Conv(reader["TRADENM"].ToString(), "C", 35));
                sb.Append(Conv(reader["SNDIV"].ToString(), "C", 1));
                sb.Append(Conv(reader["BUYDT"].ToString(), "C", 8));
                sb.Append(Conv(reader["BUYQTY"].ToString(), "N", 7));
                sb.Append(Conv(reader["BUYAMT"].ToString(), "N", 10));
                sb.Append(Conv(reader["EADANGA"].ToString(), "N", 10));

                String strLine = sb.ToString();
                if (lineno > 1) sw.Write(Environment.NewLine);
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeH0603(string demno, OleDbConnection p_conn)
        {
            if (chkJabo.Checked == true) return;

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/H060.3", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            string sql = "";
            sql += System.Environment.NewLine + "SELECT DEMNO,DEMDIV,ELINENO,RCVRESID,RCVNM,USEDT";
            sql += System.Environment.NewLine + "  FROM TIE_H0603";
            sql += System.Environment.NewLine + " WHERE DEMNO= '" + demno + "' ";
            sql += System.Environment.NewLine + "   AND ISNULL(RCVRESID,'')<>''";
            sql += System.Environment.NewLine + "   AND ISNULL(RCVNM,'')<>''";
            sql += System.Environment.NewLine + "   AND ISNULL(USEDT,'')<>''";


            int lineno = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                lineno++;
                sb.Length = 0;
                sb.Append(Conv(reader["DEMYM"].ToString(), "C", 12));
                sb.Append(Conv(reader["DEMDIV"].ToString(), "C", 1));
                sb.Append(Conv(reader["ELINENO"].ToString(), "N", 3));
                sb.Append(Conv(reader["RCVRESID"].ToString(), "C", 13));
                sb.Append(Conv(reader["RCVNM"].ToString(), "C", 20));
                sb.Append(Conv(reader["USEDT"].ToString(), "C", 8));

                String strLine = sb.ToString();
                if (lineno > 1) sw.Write(Environment.NewLine);
                sw.Write(strLine);

                return true;
            });

            sw.Close();
        }

        private void MakeMedlogenc(string demno, OleDbConnection p_conn)
        {
            String strRcvid = cboRcvid.SelectedItem.ToString();
            strRcvid = (strRcvid + "-").Split('-')[0];
            if (chkJabo.Checked == true) strRcvid = "10100011"; // 자보는 모두 본원(10100011)으로 보낸다.

            StreamWriter sw = new StreamWriter(txtFolder.Text.ToString() + "/MEDLOG.ENC", false, Encoding.Default);

            StringBuilder sb = new StringBuilder();

            sb.Length = 0;
            sb.Append(Conv(strRcvid, "C", 12));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv("NULL", "C", 8));
            sb.Append(System.Environment.NewLine);
            sb.Append(Conv(demno.Substring(0, 8) + (chkJabo.Checked == true ? " 자보" : "") + " 치료재료 및 약제구입내역 통보서", "C", 30));
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
                return MetroLib.StrHelper.LeftH(ret, Len);
            }
            else
            {
                String ret = value.PadRight(Len, ' ');
                return MetroLib.StrHelper.LeftH(ret, Len);
            }
        }

        private void ADD0707E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0707E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            string demno = GetLatestDemno();
            if (demno != "") txtDemno.Text = demno;
        }

        private string GetLatestDemno()
        {
            string demno = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT DEMNO";
                sql += System.Environment.NewLine + "  FROM TIE_H0601";
                sql += System.Environment.NewLine + " WHERE ISNULL(MULTIHSFG,'') = '" + m_HospMulti + "' ";
                sql += System.Environment.NewLine + " ORDER BY DEMNO DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    demno = reader["DEMNO"].ToString();
                    return MetroLib.SqlHelper.BREAK ;
                });

                conn.Close();
            }
            return demno;
        }

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (view == null) return;
                    if (view.RowCount < 1) return;
                    if (view.FocusedRowHandle < 0) return;

                    if (view.FocusedColumn.FieldName != "ITEMCD" && view.FocusedColumn.FieldName != "ITEMINFO1") return;

                    string itemcd = view.GetRowCellValue(view.FocusedRowHandle, "ITEMCD").ToString();
                    string buydt = view.GetRowCellValue(view.FocusedRowHandle, "BUYDT").ToString();
                    string demdivnm = view.GetRowCellValue(view.FocusedRowHandle, "DEMDIVNM").ToString();

                    if (PopupEdicode(view.FocusedRowHandle, itemcd, buydt, demdivnm) == true)
                    {
                        ReadHx(view.FocusedRowHandle);
                        DupCheck(view.FocusedRowHandle);
                    }

                    e.Handled = true;
                }

                if (e.KeyCode == Keys.Enter)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (view == null) return;
                    if (view.RowCount < 1) return;
                    if (view.FocusedRowHandle < 0) return;

                    if (view.FocusedColumn.FieldName != "EADANGA") return;


                    // 다음줄 품목코드로 이동
                    // 현재줄이 마지막이면 줄을 추가하고 품목코드로 이동
                    if (view.FocusedRowHandle < view.RowCount - 1)
                    {
                        view.FocusedRowHandle += 1;
                        view.FocusedColumn = view.Columns["ITEMCD"];
                        view.ClearSelection();
                        view.SelectCell(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn);
                    }
                    else if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        // 마지막 줄이면
                        btnAddRow.PerformClick();
                        grdMainView.UpdateCurrentRow();
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOtherId_Click(object sender, EventArgs e)
        {
            ADD0707E_3 f = new ADD0707E_3();
            f.ShowDialog(this);
        }

        private void mnuHistory_Click(object sender, EventArgs e)
        {
            string code = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "ITEMCD").ToString();
            string name = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "ITEMINFO1").ToString();
            ADD0707E_4 f = new ADD0707E_4();
            f.in_code = code;
            f.in_name = name;
            f.ShowDialog(this);

        }

        private void mnuInsRow_Click(object sender, EventArgs e)
        {
            btnInsRow.PerformClick();
        }

        private void mnuDelRow_Click(object sender, EventArgs e)
        {
            btnDelRow.PerformClick();
        }

        private void mnuDel_Click(object sender, EventArgs e)
        {
            try
            {
                string demno = txtDemno.Text.ToString();
                if (demno == "") return;

                if (MessageBox.Show("신청번호 [" + demno + "] 를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;


                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "삭제 중입니다.");
                this.Del(demno);
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                txtDemno.Text = "";
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        // 포거스가 가면 바로 편집모드가 되는 현상 방지
        //DevExpress.XtraGrid.Columns.GridColumn prevColumn = null;
        //int prevRow = -1;
        private void grdMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
        //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
        //    if (prevColumn != view.FocusedColumn || prevRow != view.FocusedRowHandle)
        //    {
        //        if (view.FocusedColumn.FieldName == "DEMDIVNM" || view.FocusedColumn.FieldName == "SNDIVNM")
        //        {
        //            // 특정 필드는 동작하도록
        //        }
        //        else
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //    prevColumn = view.FocusedColumn;
        //    prevRow = view.FocusedRowHandle;
        }

        private void grdMainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;
            ReadHx(e.FocusedRowHandle);
        }

        private void chkSnDiv_CheckedChanged(object sender, EventArgs e)
        {
            if (OnPgm) return;
            MetroLib.RegHelper.SaveValue("ADD0707E", chkSnDiv.Name, chkSnDiv.Checked == true ? "true" : "");
        }

        private void chkCalcFg_CheckedChanged(object sender, EventArgs e)
        {
            if (OnPgm) return;
            MetroLib.RegHelper.SaveValue("ADD0707E", chkCalcFg.Name, chkCalcFg.Checked == true ? "true" : "");
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //grdMainView.IndicatorWidth = 30;
            if (e.RowHandle > 0)
            {
                e.Info.DisplayText = e.RowHandle.ToString();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = "치료재료및약제구입내역통보서";

                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    grdMain.ExportToXlsx(filePath);

                    if (MessageBox.Show("파일을 열까요?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
        private void GoToNextColumn(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            if (view.FocusedColumn.FieldName == "DEMDIVNM")
            {
                view.FocusedColumn = view.Columns["ITEMCD"];
            }
            else if (view.FocusedColumn.FieldName == "ITEMCD")
            {
                view.FocusedColumn = view.Columns["ITEMINFO1"];
            }
            else if (view.FocusedColumn.FieldName == "ITEMINFO1")
            {
                view.FocusedColumn = view.Columns["ITEMINFO2"];
            }
            else if (view.FocusedColumn.FieldName == "ITEMINFO2")
            {
                view.FocusedColumn = view.Columns["ITEMINFO3"];
            }
            else if (view.FocusedColumn.FieldName == "ITEMINFO3")
            {
                view.FocusedColumn = view.Columns["PTYPE"];
            }
            else if (view.FocusedColumn.FieldName == "PTYPE")
            {
                view.FocusedColumn = view.Columns["PDUT"];
            }
            else if (view.FocusedColumn.FieldName == "PDUT")
            {
                view.FocusedColumn = view.Columns["BUSINESSCD"];
            }
            else if (view.FocusedColumn.FieldName == "BUSINESSCD")
            {
                view.FocusedColumn = view.Columns["TRADENM"];
            }
            else if (view.FocusedColumn.FieldName == "TRADENM")
            {
                view.FocusedColumn = view.Columns["SNDIVNM"];
            }
            else if (view.FocusedColumn.FieldName == "SNDIVNM")
            {
                view.FocusedColumn = view.Columns["BUYDT"];
            }
            else if (view.FocusedColumn.FieldName == "BUYDT")
            {
                view.FocusedColumn = view.Columns["BUYQTY"];
            }
            else if (view.FocusedColumn.FieldName == "BUYQTY")
            {
                view.FocusedColumn = view.Columns["BUYAMT"];
            }
            else if (view.FocusedColumn.FieldName == "BUYAMT")
            {
                view.FocusedColumn = view.Columns["EADANGA"];
            }
            else if (view.FocusedColumn.FieldName == "EADANGA")
            {
                // 다음줄 품목코드로 이동
                // 현재줄이 마지막이면 줄을 추가하고 품목코드로 이동
                if (view.FocusedRowHandle < view.RowCount - 1)
                {
                    view.FocusedRowHandle += 1;
                    view.FocusedColumn = view.Columns["ITEMCD"];
                }
                else if (view.FocusedRowHandle == view.RowCount - 1)
                {
                    // 마지막 줄이면
                    btnAddRow.PerformClick();
                    grdMainView.UpdateCurrentRow();
                }
            }
        }
        */
    }
}

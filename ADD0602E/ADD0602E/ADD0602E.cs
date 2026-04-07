using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0602E
{
    public partial class ADD0602E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private bool IsFirst;

        public ADD0602E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0602E(String user, String pwd, String prjcd, String addpara)
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
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    List<object> para = new List<object>();
                    para.Add(m_User);
                    para.Add(m_Prjcd);

                    MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["MULTIFG"].ToString();
                        return true;
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

        private void ADD0602E_Load(object sender, EventArgs e)
        {
            this.IsFirst = true;
        }

        private void ADD0602E_Activated(object sender, EventArgs e)
        {
            if (this.IsFirst == false) return;
            this.IsFirst = false;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string exdt = MetroLib.Util.GetSysDate(conn);
                SetHosInfo(conn, exdt);
            }

            if (m_Addpara != "")
            {
                char d_lev1 = (char)21;
                string[] para = m_Addpara.Split(d_lev1);
                // 0.재심사조정청구 1.이의신청 
                if (para[0] == "0") rbObjA.Checked = true; 
                else rbObjB.Checked = true;
                // 0.보험 1.보호 2.자보
                if (para[1] == "0") rbQfy2.Checked = true; 
                else if (para[1] == "1") rbQfy3.Checked = true;
                else rbQfy6.Checked = true;
                //
                txtCnecno.Text = para[2];
                txtDcount.Text = para[3];
                txtDemno.Text = para[4];
                txtDemseq.Text = para[5];
                txtGrpno.Text = para[6];
                txtReday.Text = para[7];
                //
                panObj.Enabled = false;
                panQfy.Enabled = false;
                
                txtCnecno.ReadOnly = true; txtCnecno.BackColor = Color.White;
                txtDcount.ReadOnly = true; txtDcount.BackColor = Color.White;
                txtDemno.ReadOnly = true; txtDemno.BackColor = Color.White;
                txtDemseq.ReadOnly = true; txtDemseq.BackColor = Color.White;
                txtGrpno.ReadOnly = true; txtGrpno.BackColor = Color.White;
                txtReday.ReadOnly = true; txtReday.BackColor = Color.White;

                btnQuery.PerformClick();
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

        private void SetHosInfo(OleDbConnection p_conn, string p_exdt)
        {
            // 요양기관기호
            txtHosid.Text = GetTA88A(p_conn, "2", p_exdt);
            // 요양기관명
            txtHosnm.Text = GetTA88A(p_conn, "1", p_exdt);
            // 주소
            txtAddr.Text = GetTA88A(p_conn, "3", p_exdt);

            string sql = "";
            // 작업자명
            sql = "SELECT USRNM FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                txtWorknm.Text = reader["USRNM"].ToString();
                return true;
            });
        }

        private string GetTA88A(OleDbConnection p_conn, string p_mst3cd, string p_exdt)
        {
            string ret = "";
            float cnt = 0;
            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT COUNT(*) CNT ";
            sql = sql + Environment.NewLine + "  FROM TA88A ";
            sql = sql + Environment.NewLine + " WHERE MST1CD = 'A' ";
            sql = sql + Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "' ";
            sql = sql + Environment.NewLine + "   AND MST3CD = '" + p_mst3cd + "' ";
            sql = sql + Environment.NewLine + "   AND MST4CD = ( SELECT MAX(X.MST4CD) ";
            sql = sql + Environment.NewLine + "                    FROM TA88A X ";
            sql = sql + Environment.NewLine + "                   WHERE X.MST1CD  = 'A'";
            sql = sql + Environment.NewLine + "                     AND X.MST2CD  = 'HOSPITAL" + m_HospMulti + "' ";
            sql = sql + Environment.NewLine + "                     AND X.MST3CD  = '" + p_mst3cd + "' ";
            sql = sql + Environment.NewLine + "                     AND X.MST4CD <= '" + p_exdt + "' ";
            sql = sql + Environment.NewLine + "                )";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                cnt = ToFloat(reader["CNT"].ToString());
                return false;
            });
            if (cnt > 0)
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT FLD1QTY ";
                sql = sql + Environment.NewLine + "  FROM TA88A ";
                sql = sql + Environment.NewLine + " WHERE MST1CD = 'A' ";
                sql = sql + Environment.NewLine + "   AND MST2CD = 'HOSPITAL" + m_HospMulti + "' ";
                sql = sql + Environment.NewLine + "   AND MST3CD = '" + p_mst3cd + "' ";
                sql = sql + Environment.NewLine + "   AND MST4CD = ( SELECT MAX(X.MST4CD) ";
                sql = sql + Environment.NewLine + "                    FROM TA88A X ";
                sql = sql + Environment.NewLine + "                   WHERE X.MST1CD  = 'A' ";
                sql = sql + Environment.NewLine + "                     AND X.MST2CD  = 'HOSPITAL" + m_HospMulti + "' ";
                sql = sql + Environment.NewLine + "                     AND X.MST3CD  = '" + p_mst3cd + "' ";
                sql = sql + Environment.NewLine + "                     AND X.MST4CD <= '" + p_exdt + "' ";
                sql = sql + Environment.NewLine + "                )";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT FLD1QTY ";
                sql = sql + Environment.NewLine + "  FROM TA88 ";
                sql = sql + Environment.NewLine + " WHERE MST1CD='A'";
                sql = sql + Environment.NewLine + "   AND MST2CD='HOSPITAL" + m_HospMulti + "' ";
                sql = sql + Environment.NewLine + "   AND MST3CD='" + p_mst3cd + "' ";
            }
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["FLD1QTY"].ToString();
                return false;
            });

            return ret;
        }

        private string GetObjdiv()
        {
            return (rbObjA.Checked == true ? "A" : "B"); // A.재심 B.이의신청
        }

        private string GetQfydiv()
        {
            string qfydiv = "2"; //보험
            if (rbQfy6.Checked == true) qfydiv = "6"; // 자보
            else if (rbQfy3.Checked == true) qfydiv = "3"; // 보호
            return qfydiv;
        }

        private void Query()
        {
            grdMain.DataSource=null;
            List<CData> list = new List<CData>();
            grdMain.DataSource=list;
            RefreshGridMain();

            if (txtDemno.Text.ToString() == "") return;
            if (txtDemno.Text.ToString().Length < 6) return;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string exdt = txtDemno.Text.ToString().Substring(0, 6) + "01";
                SetHosInfo(conn, exdt);

                string objdiv = GetObjdiv();
                string tF3 = "";
                if (rbQfy6.Checked==true)
                {
                    tF3 = "TIE_N0203"; // 자보
                }
                else if (rbQfy3.Checked == true)
                {
                    tF3 = "TIE_F0603_062"; // 보호
                }
                else
                {
                    tF3 = "TIE_F0203_062"; // 보험
                }
                string iofg = "";
                string sql = "";
                sql = "SELECT IOFG FROM TIE_H010 WHERE DEMNO='" + txtDemno.Text.ToString() + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    iofg = reader["IOFG"].ToString();
                    return false;
                });
                string tTI1A = "TI1A";
                string tTI1F = "TI1F";
                string fEXDATE = "EXDATE";
                if (iofg == "1")
                {
                    tTI1A = "TI1A";
                    tTI1F = "TI1F";
                    fEXDATE = "EXDATE";
                }
                else
                {
                    tTI1A = "TI2A";
                    tTI1F = "TI2F";
                    fEXDATE = "BDODT";
                }

                if (rbQfy6.Checked == true)
                {
                    // 자보
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT N3.EPRTNO AS EPRTNO";
                    sql = sql + Environment.NewLine + "     , A.PID     AS PID";
                    sql = sql + Environment.NewLine + "     , A.PNM     AS PNM";
                    sql = sql + Environment.NewLine + "     , I31.DONFG AS DONFG";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT1 AS OBJAMT1";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT2 AS OBJAMT2";
                    sql = sql + Environment.NewLine + "     , I31.DOCUNO  AS DOCUNO";
                    sql = sql + Environment.NewLine + "     , SUM(CASE WHEN F.MAFG<>'2' THEN N3.JJGUMAK ELSE 0 END) AS SAKAMT1 ";
                    sql = sql + Environment.NewLine + "     , SUM(ROUND(CASE WHEN F.MAFG ='2' THEN N3.JJGUMAK*(1+(A.GSRT/100)) ELSE 0 END,0)) AS SAKAMT2 ";
                    sql = sql + Environment.NewLine + "     , I32.PRTDT AS PRTDT";
                    sql = sql + Environment.NewLine + " FROM (";
                    sql = sql + Environment.NewLine + "       SELECT N3.DEMSEQ,N3.CNECNO,N3.GRPNO,N3.DCOUNT,N3.DEMNO,N3.EPRTNO,N3.JJGUMAK,N3.LNO";
                    sql = sql + Environment.NewLine + "         FROM TIE_N0203 N3 ";
                    sql = sql + Environment.NewLine + "        WHERE N3.DEMSEQ='" + txtDemseq.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "          AND N3.CNECNO='" + txtCnecno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "          AND N3.GRPNO ='" + txtGrpno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "          AND N3.DCOUNT='" + txtDcount.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "          AND N3.DEMNO ='" + txtDemno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "          AND ISNUMERIC(N3.JJRMK)=0 ";        // 2008.07.24 WOOIL - 사유가 영문자이면 삭감임.
                    sql = sql + Environment.NewLine + "          AND N3.JJRMK NOT IN ('J1','J2','J3') ";        // 사유가 J1,J2,J3이면 불능임.
                    sql = sql + Environment.NewLine + "      ) N3 ";
                    sql = sql + Environment.NewLine + "           INNER JOIN " + tTI1A + " A ON A.DEMNO=N3.DEMNO AND A.EPRTNO = CONVERT(NUMERIC,N3.EPRTNO)";
                    sql = sql + Environment.NewLine + "           INNER JOIN " + tTI1F + " F ON F." + fEXDATE + "=A." + fEXDATE + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS AND F.ELINENO=N3.LNO";
                    sql = sql + Environment.NewLine + "           LEFT JOIN TI31 I31 ON I31.OBJDIV='" + objdiv + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.DEMSEQ='" + txtDemseq.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.CNECNO='" + txtCnecno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.GRPNO='" + txtGrpno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.DCOUNT='" + txtDcount.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.DEMNO='" + txtDemno.Text.ToString() + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.EPRTNO=N3.EPRTNO ";
                    sql = sql + Environment.NewLine + "           LEFT JOIN TI32 I32 ON I32.OBJDIV='" + objdiv + "' ";
                    sql = sql + Environment.NewLine + "                             AND I32.DOCUNO=I31.DOCUNO ";
                    sql = sql + Environment.NewLine + " GROUP BY N3.EPRTNO,A.PID,A.PNM,I31.DONFG,I31.OBJAMT1,I31.OBJAMT2,I31.DOCUNO,I32.PRTDT";
                }
                else
                {
                    // 보험,보호
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT F3.EPRTNO AS EPRTNO";
                    sql = sql + Environment.NewLine + "     , A.PID     AS PID";
                    sql = sql + Environment.NewLine + "     , A.PNM     AS PNM";
                    sql = sql + Environment.NewLine + "     , I31.DONFG AS DONFG";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT1 AS OBJAMT1";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT2 AS OBJAMT2";
                    sql = sql + Environment.NewLine + "     , I31.DOCUNO  AS DOCUNO";
                    sql = sql + Environment.NewLine + "     , SUM(CASE WHEN F3.GUBUN<>'2' THEN F3.JJGUMAK ELSE 0 END) AS SAKAMT1";
                    sql = sql + Environment.NewLine + "     , SUM(ROUND(CASE WHEN F3.GUBUN ='2' THEN F3.JJGUMAK*(1+(A.GSRT/100)) ELSE 0 END,0)) AS SAKAMT2";
                    sql = sql + Environment.NewLine + "     , I32.PRTDT AS PRTDT";
                    sql = sql + Environment.NewLine + " FROM (";
                    sql = sql + Environment.NewLine + "       SELECT F3.DEMSEQ,F3.CNECNO,F3.GRPNO,F3.DCOUNT,F3.DEMNO,F3.EPRTNO,F3.GUBUN,F3.JJGUMAK";
                    sql = sql + Environment.NewLine + "         FROM " + tF3 + " F3";
                    sql = sql + Environment.NewLine + "        WHERE F3.DEMSEQ='" + txtDemseq.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F3.CNECNO='" + txtCnecno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F3.GRPNO ='" + txtGrpno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F3.DCOUNT='" + txtDcount.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F3.DEMNO ='" + txtDemno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND ISNUMERIC(F3.JJRMK)=0";        // 2008.07.24 WOOIL - 사유가 영문자이면 삭감임.
                    sql = sql + Environment.NewLine + "       UNION ALL";
                    sql = sql + Environment.NewLine + "       SELECT F4.DEMSEQ,F4.CNECNO,F1.GRPNO,F4.DCOUNT,F1.DEMNO,F4.EPRTNO,'1' AS GUBUN,F4.JJAMT AS JJGUMAK";
                    sql = sql + Environment.NewLine + "         FROM TIE_F0904 F4 INNER JOIN TIE_F0901 F1 ON F1.DEMSEQ=F4.DEMSEQ AND F1.REDAY=F4.REDAY AND F1.CNECNO=F4.CNECNO AND F1.DCOUNT=F4.DCOUNT";
                    sql = sql + Environment.NewLine + "        WHERE F4.DEMSEQ='" + txtDemseq.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F4.CNECNO='" + txtCnecno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F1.GRPNO ='" + txtGrpno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F4.DCOUNT='" + txtDcount.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "          AND F1.DEMNO ='" + txtDemno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "      ) F3 ";
                    sql = sql + Environment.NewLine + "           INNER JOIN " + tTI1A + " A ON A.DEMNO=F3.DEMNO AND A.EPRTNO = CONVERT(NUMERIC,F3.EPRTNO)";
                    sql = sql + Environment.NewLine + "           LEFT JOIN TI31 I31 ON I31.OBJDIV='" + objdiv + "' ";
                    sql = sql + Environment.NewLine + "                             AND I31.DEMSEQ='" + txtDemseq.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "                             AND I31.CNECNO='" + txtCnecno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "                             AND I31.GRPNO='" + txtGrpno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "                             AND I31.DCOUNT='" + txtDcount.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "                             AND I31.DEMNO='" + txtDemno.Text.ToString() + "'";
                    sql = sql + Environment.NewLine + "                             AND I31.EPRTNO=F3.EPRTNO";
                    sql = sql + Environment.NewLine + "           LEFT JOIN TI32 I32 ON I32.OBJDIV='" + objdiv + "'";
                    sql = sql + Environment.NewLine + "                             AND I32.DOCUNO=I31.DOCUNO";
                    sql = sql + Environment.NewLine + " GROUP BY F3.EPRTNO,A.PID,A.PNM,I31.DONFG,I31.OBJAMT1,I31.OBJAMT2,I31.DOCUNO,I32.PRTDT";
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.EPRTNO = reader["EPRTNO"].ToString();
                    data.PID = reader["PID"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.DONFG = reader["DONFG"].ToString();
                    data.SAKAMT1 = ToFloat(reader["SAKAMT1"].ToString());
                    data.SAKAMT2 = ToFloat(reader["SAKAMT2"].ToString());
                    data.OBJAMT1 = ToFloat(reader["OBJAMT1"].ToString());
                    data.OBJAMT2 = ToFloat(reader["OBJAMT2"].ToString());
                    data.DOCUNO = reader["DOCUNO"].ToString();
                    data.PRTDT = reader["PRTDT"].ToString();
                    data.IOFG = iofg;

                    list.Add(data);
                    return true;
                });

            }

            RefreshGridMain();

        }

        public float ToFloat(string s)
        {
            float ret = 0;
            float.TryParse(s, out ret);
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            string objdiv = GetObjdiv();
            string qfydiv = GetQfydiv();

            if (grdMainView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;

            string eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();

            ADD0602E_1 f = new ADD0602E_1(m_User, objdiv, qfydiv, txtDemseq.Text.ToString(), txtCnecno.Text.ToString(), txtGrpno.Text.ToString(), txtDcount.Text.ToString(), txtDemno.Text.ToString(), eprtno);
            f.PrevButton_Click += new EventHandler(delegate(object sender1, EventArgs e1)
            {
                if (grdMainView.FocusedRowHandle > 0)
                {
                    grdMainView.FocusedRowHandle--;
                    eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
                    MyEventArgs e2 = e1 as MyEventArgs;
                    e2.m_requery = true;
                    e2.m_eprtno = eprtno;
                }
                else
                {
                    MessageBox.Show("처음 자료입니다.");
                }
            });
            f.NextButton_Click += new EventHandler(delegate(object sender1, EventArgs e1)
            {
                if (grdMainView.FocusedRowHandle < grdMainView.RowCount - 1)
                {
                    grdMainView.FocusedRowHandle++;
                    eprtno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "EPRTNO").ToString();
                    MyEventArgs e2 = e1 as MyEventArgs;
                    e2.m_requery = true;
                    e2.m_eprtno = eprtno;
                }
                else
                {
                    MessageBox.Show("마지막 자료입니다.");
                }
            });
            f.ShowDialog(this);

        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                ADD0602E_2 f = new ADD0602E_2();
                f.ShowDialog(this);
                string docuno = f.m_docuno;
                if (docuno == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Make(docuno);
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

        private void Make(string p_docuno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    Make_in(p_docuno, conn, tran);

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

        private void Make_in(string p_docuno, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql="";
            sql = sql + Environment.NewLine + "SELECT COUNT(*) AS CNT";
            sql = sql + Environment.NewLine + "  FROM TI32";
            sql = sql + Environment.NewLine + " WHERE DOCUNO = '" + p_docuno + "'";

            int t32_count=0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                int.TryParse(reader["CNT"].ToString(),out t32_count);
                return true;
            });

            if(t32_count>0){
                MessageBox.Show("이미 있는 문서번호입니다.");
                return;
            }

            string sysdate = MetroLib.Util.GetSysDate(p_conn, p_tran);
            string systime = MetroLib.Util.GetSysTime(p_conn, p_tran);

            sql = "";
            sql = sql + Environment.NewLine + "UPDATE TI31 ";
            sql = sql + Environment.NewLine + "   SET DOCUNO='" + p_docuno + "' ";
            sql = sql + Environment.NewLine + "     , REDAY ='" + txtReday.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DEMSEQ='" + txtDemseq.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND CNECNO='" + txtCnecno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND GRPNO='" + txtGrpno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DCOUNT='" + txtDcount.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DEMNO='" + txtDemno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DONFG='Y' ";
            sql = sql + Environment.NewLine + "   AND ISNULL(DOCUNO,'')='' ";
            sql = sql + Environment.NewLine + "   AND (ISNULL(OBJAMT1,0)<>0 OR ISNULL(OBJAMT2,0)<>0)";

            MetroLib.SqlHelper.ExecuteSql(sql,p_conn,p_tran);

            sql = "";
            sql = sql + Environment.NewLine + "INSERT TI32(";
            sql = sql + Environment.NewLine + "       OBJDIV,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,REDAY,DOCUNO,OBJCOUNT,OBJAMT1,OBJAMT2,SAKAMT1,SAKAMT2,EMPID,ENTDT,ENTTM ";
            sql = sql + Environment.NewLine + "       )";
            sql = sql + Environment.NewLine + "SELECT OBJDIV,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,REDAY,DOCUNO,COUNT(*),SUM(OBJAMT1),SUM(OBJAMT2),SUM(SAKAMT1),SUM(SAKAMT2) ";
            sql = sql + Environment.NewLine + "     , '" + m_User + "','" + sysdate + "','" + systime + "' ";
            sql = sql + Environment.NewLine + "  FROM TI31 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";
            sql = sql + Environment.NewLine + " GROUP BY OBJDIV,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,REDAY,DOCUNO";

            MetroLib.SqlHelper.ExecuteSql(sql,p_conn,p_tran);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string docuno = Convert.ToString(grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DOCUNO"));
                ADD0602E_2 f = new ADD0602E_2(docuno);
                f.ShowDialog(this);
                docuno = f.m_docuno;
                if (docuno == "") return;

                if (MessageBox.Show("문서번호 : " + docuno + " 를 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Delete(docuno);
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

        private void Delete(string p_docuno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    Delete_in(p_docuno, conn, tran);

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

        private void Delete_in(string p_docuno, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT PRTDT ";
            sql = sql + Environment.NewLine + "  FROM TI32 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";

            string prtdt = "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                prtdt = reader["PRTDT"].ToString();
                return true;
            });

            if (prtdt != "")
            {
                MessageBox.Show("완료된 자료는 삭제할 수 없습니다.");
                return;
            }

            sql = "";
            sql = sql + Environment.NewLine + "UPDATE TI31 ";
            sql = sql + Environment.NewLine + "   SET DOCUNO='' ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DEMSEQ='" + txtDemseq.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND CNECNO='" + txtCnecno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND GRPNO='" + txtGrpno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DCOUNT='" + txtDcount.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DEMNO='" + txtDemno.Text.ToString() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";

            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "";
            sql = sql + Environment.NewLine + "DELETE ";
            sql = sql + Environment.NewLine + "  FROM TI32 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";

            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string docuno = Convert.ToString(grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DOCUNO"));
                ADD0602E_2 f = new ADD0602E_2(docuno);
                f.ShowDialog(this);
                docuno = f.m_docuno;
                if (docuno == "") return;

                if (MessageBox.Show("문서번호 : " + docuno + " 를 출력하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(docuno);
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

        private string m_Hdate;

        private float m_Left;
        private float m_Top;
        private float m_Right;
        private float m_Bottom;

        private float[] m_yp = new float[52]; // 0 ~ 51
        private float[] m_xp = new float[19]; // 0 ~ 18

        private int m_pageNo;
        private int m_text_ygap;

        private float m_text_height;
        private float m_text_width;


        private CTI32 m_TI32 = new CTI32();
        private List<CTI31> m_TI31s = new List<CTI31>();
        private List<CTI31A> m_TI31As = new List<CTI31A>();

        private void Print(string p_docuno)
        {
            string objdiv = GetObjdiv();
            string qfydiv = GetQfydiv();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_Hdate = MetroLib.Util.GetSysDate(conn);

                m_TI32.Clear();
                m_TI31s.Clear();
                m_TI31As.Clear();


                SetTI32(p_docuno, conn);
                SetTI31(p_docuno, conn);
                SetTI31A(p_docuno, conn);

                //SetTI32_MOK(p_docuno, conn);
                //SetTI31_MOK(p_docuno, conn);
                //SetTI31A_MOK(p_docuno, conn);

                conn.Close();

                Print_Xtra();

                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

            }
        }

        private void SetTI32(string p_docuno, OleDbConnection p_conn)
        {
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT OBJDIV,DOCUNO,OBJCOUNT,SAKAMT1,SAKAMT2,OBJAMT1,OBJAMT2,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,REDAY,ENTDT,ENTTM,EMPID,PRTDT,PRTTM,PRTID ";
            sql = sql + Environment.NewLine + "  FROM TI32 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                m_TI32.CNT++;
                m_TI32.OBJDIV = reader["OBJDIV"].ToString();
                m_TI32.DOCUNO = reader["DOCUNO"].ToString();
                m_TI32.OBJCOUNT = reader["OBJCOUNT"].ToString();
                m_TI32.SAKAMT1 = reader["SAKAMT1"].ToString();
                m_TI32.SAKAMT2 = reader["SAKAMT2"].ToString();
                m_TI32.OBJAMT1 = reader["OBJAMT1"].ToString();
                m_TI32.OBJAMT2 = reader["OBJAMT2"].ToString();
                m_TI32.DEMSEQ = reader["DEMSEQ"].ToString();
                m_TI32.CNECNO = reader["CNECNO"].ToString();
                m_TI32.GRPNO = reader["GRPNO"].ToString();
                m_TI32.DCOUNT = reader["DCOUNT"].ToString();
                m_TI32.DEMNO = reader["DEMNO"].ToString();
                m_TI32.REDAY = reader["REDAY"].ToString();
                m_TI32.ENTDT = reader["ENTDT"].ToString();
                m_TI32.ENTTM = reader["ENTTM"].ToString();
                m_TI32.EMPID = reader["EMPID"].ToString();
                m_TI32.PRTDT = reader["PRTDT"].ToString();
                m_TI32.PRTTM = reader["PRTTM"].ToString();
                m_TI32.PRTID = reader["PRTID"].ToString();
                return true;
            });
        }

        private void SetTI31(string p_docuno, OleDbConnection p_conn)
        {
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT EPRTNO,PNM,BDIV,OBJAMT1,OBJAMT2,OBJTEXT,OBJADD,OBJADDTEXT ";
            sql = sql + Environment.NewLine + "  FROM TI31 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";
            sql = sql + Environment.NewLine + " ORDER BY CONVERT(NUMERIC,EPRTNO) ";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = reader["EPRTNO"].ToString();
                ti31.PNM = reader["PNM"].ToString();
                ti31.BDIV = reader["BDIV"].ToString();
                ti31.OBJAMT1 = reader["OBJAMT1"].ToString();
                ti31.OBJAMT2 = reader["OBJAMT2"].ToString();
                ti31.OBJTEXT = reader["OBJTEXT"].ToString();
                ti31.OBJADD = reader["OBJADD"].ToString();
                ti31.OBJADDTEXT = reader["OBJADDTEXT"].ToString();

                m_TI31s.Add(ti31);

                return true;
            });
        }

        private void SetTI31A(string p_docuno, OleDbConnection p_conn)
        {
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT B.EPRTNO,A.LNO,ISNULL(A.HANGNO,0) AS HANGNO, A.LNO, A.PRKNM, A.JJRMK, CONVERT(VARCHAR,A.OBJAMT) +'('+ A.GUBUN+')' AS OBJAMT ";
            sql = sql + Environment.NewLine + "  FROM TI31A A INNER JOIN TI31 B ON A.OBJDIV = B.OBJDIV ";
            sql = sql + Environment.NewLine + "                                AND A.DEMSEQ = B.DEMSEQ ";
            sql = sql + Environment.NewLine + "                                AND A.CNECNO = B.CNECNO ";
            sql = sql + Environment.NewLine + "                                AND A.GRPNO  = B.GRPNO  ";
            sql = sql + Environment.NewLine + "                                AND A.DCOUNT = B.DCOUNT ";
            sql = sql + Environment.NewLine + "                                AND A.DEMNO  = B.DEMNO  ";
            sql = sql + Environment.NewLine + "                                AND A.EPRTNO = B.EPRTNO ";
            sql = sql + Environment.NewLine + " WHERE B.OBJDIV='" + GetObjdiv() + "' ";
            sql = sql + Environment.NewLine + "   AND B.DOCUNO='" + p_docuno + "' ";
            sql = sql + Environment.NewLine + "   AND A.OBJAMT <> 0 ";
            sql = sql + Environment.NewLine + " ORDER BY CONVERT(NUMERIC,B.EPRTNO),A.LNO,A.HANGNO ";


            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = reader["EPRTNO"].ToString();
                ti31a.LNO = reader["LNO"].ToString();
                ti31a.HANGNO = reader["HANGNO"].ToString();
                ti31a.PRKNM = reader["PRKNM"].ToString();
                ti31a.JJRMK = reader["JJRMK"].ToString();
                ti31a.OBJAMT = reader["OBJAMT"].ToString();

                m_TI31As.Add(ti31a);

                return true;
            });

        }

        private void SetTI32_MOK(string p_docuno, OleDbConnection p_conn)
        {
            m_TI32.CNT++;
            m_TI32.OBJDIV = "A";
            m_TI32.DOCUNO = "재심-2019-01";
            m_TI32.OBJCOUNT = "6";
            m_TI32.SAKAMT1 = "10000";
            m_TI32.SAKAMT2 = "12000";
            m_TI32.OBJAMT1 = "10000";
            m_TI32.OBJAMT2 = "12000";
            m_TI32.DEMSEQ = "1";
            m_TI32.CNECNO = "4026278";
            m_TI32.GRPNO = "1";
            m_TI32.DCOUNT = "20190222";
            m_TI32.DEMNO = "";
            m_TI32.REDAY = "";
            m_TI32.ENTDT = "";
            m_TI32.ENTTM = "";
            m_TI32.EMPID = "";
            m_TI32.PRTDT = "";
            m_TI32.PRTTM = "";
            m_TI32.PRTID = "";
        }

        private void SetTI31_MOK(string p_docuno, OleDbConnection p_conn)
        {
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00039";
                ti31.PNM = "김창용";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "1000";
                ti31.OBJAMT2 = "1200";
                ti31.OBJTEXT = "QBCDEDFG11111122222222222222333333333333333334444444444444444446666666666666666666666";
                ti31.OBJADD = "1,2,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00050";
                ti31.PNM = "박상심";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "1300";
                ti31.OBJAMT2 = "1400";
                ti31.OBJTEXT = "QBCDEDFG" + Environment.NewLine
                             + "dkdkajkdjfadjf" + Environment.NewLine
                             + "dkdkajkd38uq89ru" + Environment.NewLine
                             + "가가가가가가가" + Environment.NewLine
                             + "dkdkajkdjfadjf" + Environment.NewLine
                             + "dkdkajkd38uq89ru" + Environment.NewLine
                             + "가가가가가가가";
                ti31.OBJADD = "1,2,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00051";
                ti31.PNM = "홍길동";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "1500";
                ti31.OBJAMT2 = "1500";
                ti31.OBJTEXT = "QBCDEDFG" + Environment.NewLine
                             + "가가가가가가가";
                ti31.OBJADD = "1,2,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00052";
                ti31.PNM = "이순신";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "1700";
                ti31.OBJAMT2 = "1800";
                ti31.OBJTEXT = "QBCDEDFG" + Environment.NewLine
                             + "나나나난가가가가가가가" + Environment.NewLine
                             + "3847947";
                ti31.OBJADD = "1,2,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00053";
                ti31.PNM = "강감찬";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "1900";
                ti31.OBJAMT2 = "2000";
                ti31.OBJTEXT = "QBCDEDFG" + Environment.NewLine
                             + "나나나난가가가가가가가" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947";
                ti31.OBJADD = "1,2,3,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
            {
                CTI31 ti31 = new CTI31();
                ti31.Clear();

                ti31.EPRTNO = "00054";
                ti31.PNM = "마루치";
                ti31.BDIV = "2";
                ti31.OBJAMT1 = "2100";
                ti31.OBJAMT2 = "2200";
                ti31.OBJTEXT = "QBCDEDFG" + Environment.NewLine
                             + "나나나난가가가가가가가" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "나나나난가가가가가가가" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "나나나난가가가가가가가" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947" + Environment.NewLine
                             + "3847947";
                ti31.OBJADD = "1,2,3,5";
                ti31.OBJADDTEXT = "기타";

                m_TI31s.Add(ti31);
            }
        }

        private void SetTI31A_MOK(string p_docuno, OleDbConnection p_conn)
        {
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00039";
                ti31a.LNO = "10";
                ti31a.HANGNO = "19";
                ti31a.PRKNM = "RKSKEK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "2000";

                m_TI31As.Add(ti31a);
            }
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00050";
                ti31a.LNO = "10";
                ti31a.HANGNO = "19";
                ti31a.PRKNM = "RKSKEK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "2000";

                m_TI31As.Add(ti31a);
            }
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00051";
                ti31a.LNO = "10";
                ti31a.HANGNO = "19";
                ti31a.PRKNM = "RKSKEK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "2000";

                m_TI31As.Add(ti31a);
            }
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00052";
                ti31a.LNO = "10";
                ti31a.HANGNO = "19";
                ti31a.PRKNM = "RKSKEK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "2000";

                m_TI31As.Add(ti31a);
            }
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00053";
                ti31a.LNO = "11";
                ti31a.HANGNO = "18";
                ti31a.PRKNM = "RKSKEdkdkdkdK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "3000";

                m_TI31As.Add(ti31a);
            }
            {
                CTI31A ti31a = new CTI31A();
                ti31a.Clear();

                ti31a.EPRTNO = "00054";
                ti31a.LNO = "11";
                ti31a.HANGNO = "18";
                ti31a.PRKNM = "RKSKEdkdkdkdK";
                ti31a.JJRMK = "C";
                ti31a.OBJAMT = "3000";

                m_TI31As.Add(ti31a);
            }
        }

        private string MakeAutoWrap2(int len, string vData)
        {
            string ret="";
            string line = "";
            string s = "";

            for (int i = 0; i < vData.Length; i++)
            {
                s = vData.Substring(i, 1);
                if (s == "\r")// Environment.NewLine)
                {
                    //ENTER문자를 만나면 길이에 관계없이 자른다.
                    ret += line + Environment.NewLine;
                    line = "";
                }
                else if (s == "\n")
                {
                    // 아무것도 안함.
                }
                else
                {
                    if (MetroLib.StrHelper.LengthH(line + s) > len)
                    {
                        ret += line + Environment.NewLine;
                        line = "";
                    }
                    line += s;
                }
            }
            ret += line;
            return ret;
        }

        private void SetXYPos()
        {
            m_text_ygap = 10;
            m_text_height = 18;
            m_text_width = 10;

            m_Left = 20;
            m_Top = 50 + (m_pageNo-1) * 1017;
            m_Right = 760;
            m_Bottom = m_Top + 890;

            int loopCount = 40;
            float xgap = (m_Right - m_Left) / 51; // 굴림체로 1글자 들어가는 크기
            float ygap = (m_Bottom - m_Top) / loopCount;

            for (int i = 0; i < loopCount; i++)
            {
                m_yp[i] = m_Top + (i * ygap);
            }

            m_xp[0] = m_Left;
            m_xp[1] = m_xp[0] + xgap * 3;     // 순번
            m_xp[2] = m_xp[1] + xgap * 4;     // 명세서일련번호
            m_xp[3] = m_xp[2] + xgap * (float)3.5;   // 수진자
            m_xp[4] = m_xp[3] + xgap * 5;     // 진료구분
            m_xp[5] = m_xp[4] + xgap * 5;
            m_xp[6] = m_xp[5] + xgap * 1;
            m_xp[7] = m_xp[6] + xgap * 4;
            m_xp[8] = m_xp[7] + xgap * 1;
            m_xp[9] = m_xp[8] + xgap * (float)11.5;
            m_xp[10] = m_xp[9] + xgap * 2;
            m_xp[11] = m_xp[10] + xgap * 5;
            m_xp[12] = m_xp[11] + xgap * 4;
            m_xp[13] = m_Right;

            m_xp[14] = m_xp[7] + xgap * 2;
            m_xp[15] = m_xp[14] + xgap * 3;
            m_xp[16] = m_xp[15] + xgap * (float)12.5;
            m_xp[17] = m_xp[16] + xgap * 2;
            m_xp[18] = m_xp[17] + xgap * 4;

        }

        private void Print_Xtra()
        {
            // Create a new Printing System.
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();

            // Create a link and add it to the printing system's collection of links.
            DevExpress.XtraPrinting.Link link = new DevExpress.XtraPrinting.Link();
            printingSystem.Links.Add(link);

            link.Margins.Left = 0;
            link.Margins.Top = 0;
            link.Margins.Right = 0;
            link.Margins.Bottom = 0;

            // Subscribe to the events to customize the detail and marginal page header sections of a document.
            link.CreateDetailArea += Link_CreateDetailArea;
            link.CreateMarginalHeaderArea += Link_CreateMarginalHeaderArea;

            // Create a document and show it in the document preview.
            //link.ShowPreview();
            link.CreateDocument();
            DevExpress.XtraPrinting.PrintTool printTool = new DevExpress.XtraPrinting.PrintTool(link.PrintingSystemBase);
            printTool.ShowRibbonPreview();
        }

        private void Link_CreateDetailArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            // Specify required settings for the brick graphics.
            DevExpress.XtraPrinting.BrickGraphics brickGraphics = e.Graph;
            DevExpress.XtraPrinting.BrickStringFormat format = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near, StringAlignment.Center);
            brickGraphics.StringFormat = format;
            //brickGraphics.BorderColor = SystemColors.ControlDark;

            // Start creation of a non-separable group of bricks.
            brickGraphics.BeginUnionRect();

            PrintPtnt(brickGraphics);

            // Finish the creation of a non-separable group of bricks.
            brickGraphics.EndUnionRect();
        }

        private void PrintForm(DevExpress.XtraPrinting.BrickGraphics brickGraphics)
        {
            if (m_pageNo > 1)
            {
                PrintForm2(brickGraphics);
                return;
            }

            string title = "이 의 신 청 서";
            string duration = "60일";
            string subTitle = "이의신청";
            string objdiv = GetObjdiv();

            if (objdiv == "A")
            {
                title = "재심사조정 청구서";
                duration = "30일";
                subTitle = "재심사조정청구";
            }

            float halfgap = (m_yp[1] - m_yp[0]) / 2;

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_Top, m_Right, m_Bottom);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_Top, m_Right, m_yp[3]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_Top, m_Right, m_yp[3], title, "굴림체", 14, true, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[11], m_Top, m_Right, m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[11], m_Top, m_Right, m_yp[1], "처리기간", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[11], m_yp[1], m_Right, m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[11], m_yp[1], m_Right, m_yp[2], duration, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[3], m_xp[2], m_yp[5]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[3], m_xp[2], m_yp[5], "문서번호", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[3], m_xp[4], m_yp[5]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[3], m_xp[4], m_yp[5], m_TI32.DOCUNO, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[3], m_xp[8], m_yp[5]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[3], m_xp[8], m_yp[5], "진료분야", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[8], m_yp[3], m_Right, m_yp[5]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[8], m_yp[3], m_Right, m_yp[5], "", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[5], m_xp[1], m_yp[9]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[6], m_xp[1], m_yp[7], "요양", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[7], m_xp[1], m_yp[8], "기관", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[5], m_xp[2], m_yp[7]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[5], m_xp[2], m_yp[7], "명칭", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[5], m_xp[4], m_yp[7]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[5], m_xp[4], m_yp[7], txtHosnm.Text.ToString(), "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[7], m_xp[2], m_yp[9]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[7], m_xp[2], m_yp[9], "기호", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[7], m_xp[4], m_yp[9]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[7], m_xp[4], m_yp[9], txtHosid.Text.ToString(), "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[9], m_xp[2], m_yp[11]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[9], m_xp[2], m_yp[10], subTitle, "굴림체", 9, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[10], m_xp[2], m_yp[11], "건수총계", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[9], m_xp[4], m_yp[11]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[9], m_xp[4], m_yp[11], String.Format(m_TI32.OBJCOUNT, "#,###"), "굴림체", 10, false, 1);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[11], m_xp[2], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[11], m_xp[2], m_yp[12], subTitle, "굴림체", 9, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[12], m_xp[2], m_yp[13], "비용총액", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[11], m_xp[4], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[11], m_xp[4], m_yp[13], String.Format(m_TI32.GetOBJAMT(), "#,###"), "굴림체", 10, false, 1);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[5], m_xp[6], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[7], m_xp[6], m_yp[8], "요양급여", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[8], m_xp[6], m_yp[9], "비용심사", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[9], m_xp[6], m_yp[10], "결과통보서", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[6], m_yp[5], m_xp[8], m_yp[7]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[6], m_yp[5], m_xp[8], m_yp[7], "접수번호", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[8], m_yp[5], m_xp[9], m_yp[7]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[8], m_yp[5], m_xp[9], m_yp[7], m_TI32.CNECNO, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[6], m_yp[7], m_xp[8], m_yp[9]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[6], m_yp[7], m_xp[8], m_yp[9], "묶음번호", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[8], m_yp[7], m_xp[9], m_yp[9]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[8], m_yp[7], m_xp[9], m_yp[9], m_TI32.GRPNO, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[6], m_yp[9], m_xp[8], m_yp[11]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[6], m_yp[9], m_xp[8], m_yp[11], "심사차수", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[8], m_yp[9], m_xp[9], m_yp[11]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[8], m_yp[9], m_xp[9], m_yp[11], m_TI32.DEMSEQ, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[6], m_yp[11], m_xp[8], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[6], m_yp[11], m_xp[8], m_yp[12], "통 보 서", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[6], m_yp[12], m_xp[8], m_yp[13], "도달일자", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[8], m_yp[11], m_xp[9], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[8], m_yp[11], m_xp[9], m_yp[13], m_TI32.REDAY, "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[9], m_yp[5], m_xp[10], m_yp[7]);
            if (objdiv == "B")
            {
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[5], m_xp[10], m_yp[6], "분", "굴림체", 10, false, 2);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[6], m_xp[10], m_yp[7], "류", "굴림체", 10, false, 2);
            }

            CXPrt.PrintBox_A4(brickGraphics, m_xp[10], m_yp[5], m_Right, m_yp[7]);
            if (objdiv == "B")
            {
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[5], m_Right, m_yp[6], "1.단순심사", "굴림체", 10, false, 0);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[6], m_Right, m_yp[7], "2.의학적심사", "굴림체", 10, false, 0);
            }

            CXPrt.PrintBox_A4(brickGraphics, m_xp[9], m_yp[7], m_xp[10], m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[8], m_xp[10], m_yp[9], "첨", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[9], m_xp[10], m_yp[10], "부", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[10], m_xp[10], m_yp[11], "서", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[9], m_yp[11], m_xp[10], m_yp[12], "류", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[10], m_yp[7], m_Right, m_yp[13]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[7] + halfgap, m_Right, m_yp[8] + halfgap, "1.심사결과통보서", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[8] + halfgap, m_Right, m_yp[9] + halfgap, "2.진료기록부", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[9] + halfgap, m_Right, m_yp[10] + halfgap, "3.X-ray film", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[10] + halfgap, m_Right, m_yp[11] + halfgap, "4.검사결과지", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[10], m_yp[11] + halfgap, m_Right, m_yp[12] + halfgap, "5.기타", "굴림체", 10, false, 0);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[13], m_xp[1], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[13], m_xp[1], m_yp[14], "순", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[14], m_xp[1], m_yp[15], "번", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[13], m_xp[2], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[13], m_xp[2], m_yp[14], "명세서", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[14], m_xp[2], m_yp[15], "일련번호", "굴림체", 9, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[13], m_xp[3], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[13], m_xp[3], m_yp[15], "수진자", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[3], m_yp[13], m_xp[4], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[3], m_yp[13], m_xp[4], m_yp[14], "진료구분", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[3], m_yp[14], m_xp[4], m_yp[15], "(입원.외래)", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[13], m_xp[7], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[13], m_xp[7], m_yp[14], subTitle + "금액", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[14], m_xp[5], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[14], m_xp[5], m_yp[15], "I항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[5], m_yp[14], m_xp[7], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[14], m_xp[7], m_yp[15], "II항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[7], m_yp[13], m_xp[14], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], m_yp[13], m_xp[14], m_yp[14], "항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[14], m_yp[13], m_xp[15], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[14], m_yp[13], m_xp[15], m_yp[14], "줄번호", "굴림체", 9, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[15], m_yp[13], m_xp[16], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[15], m_yp[13], m_xp[16], m_yp[14], "코드명", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[16], m_yp[13], m_xp[17], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[16], m_yp[13], m_xp[17], m_yp[14], "사유", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[17], m_yp[13], m_xp[18], m_yp[14]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[17], m_yp[13], m_xp[18], m_yp[14], "금액", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[7], m_yp[14], m_xp[12], m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], m_yp[14], m_xp[12], m_yp[15], subTitle + " 사유 및 내역", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[12], m_yp[13], m_Right, m_yp[15]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[12], m_yp[13], m_Right, m_yp[14], "첨부", "굴림체", 8, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[12], m_yp[14], m_Right, m_yp[15], "서류", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[15], m_xp[1], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[15], m_xp[2], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[15], m_xp[3], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[3], m_yp[15], m_xp[4], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[15], m_xp[4], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[5], m_yp[15], m_xp[7], m_yp[32]);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[7], m_yp[15], m_xp[12], m_yp[32]);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[32], m_Right, m_Bottom);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[32], m_Right, m_yp[33], "위와 같이 심사평가원의 처분에 대하여 " + subTitle + "합니다.", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[34], m_Right, m_yp[35], String.Format(m_Hdate, "####-##-##"), "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[35], m_Right, m_yp[36], "신청인  : " + txtWorknm.Text.ToString() + "  (서명 또는 인)", "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[36], m_Right, m_yp[37], "주  소  : " + txtAddr.Text.ToString(), "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[37], m_Right, m_yp[38], "전화번호: " + txtPhoneno.Text.ToString(), "굴림체", 10, false, 0);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[38], m_Right, m_Bottom, "건강보험심사평가원장 귀하", "궁서체", 12, false, 0);

            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_Bottom, m_Right, m_Bottom + m_text_ygap * 2, "Page : " + m_pageNo, "굴림체", 9, false, 1);


        }

        private void PrintForm2(DevExpress.XtraPrinting.BrickGraphics brickGraphics)
        {
            string subTitle = "이의신청";
            string objdiv = GetObjdiv();

            if (objdiv == "A")
            {
                subTitle = "재심사조정청구";
            }

            float halfgap = (m_yp[1] - m_yp[0]) / 2;

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_Top, m_Right, m_Bottom);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[0], m_xp[1], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[0], m_xp[1], m_yp[1], "순", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_yp[1], m_xp[1], m_yp[2], "번", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[0], m_xp[2], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[0], m_xp[2], m_yp[1], "명세서", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], m_yp[1], m_xp[2], m_yp[2], "일련번호", "굴림체", 9, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[0], m_xp[3], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], m_yp[0], m_xp[3], m_yp[2], "수진자", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[3], m_yp[0], m_xp[4], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[3], m_yp[0], m_xp[4], m_yp[1], "진료구분", "굴림체", 10, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[3], m_yp[1], m_xp[4], m_yp[2], "(입원.외래)", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[0], m_xp[7], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[0], m_xp[7], m_yp[1], subTitle + "금액", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[1], m_xp[5], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], m_yp[1], m_xp[5], m_yp[2], "I항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[5], m_yp[1], m_xp[7], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], m_yp[1], m_xp[7], m_yp[2], "II항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[7], m_yp[1], m_xp[12], m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], m_yp[1], m_xp[12], m_yp[2], subTitle + " 사유 및 내역", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[7], m_yp[0], m_xp[14], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], m_yp[0], m_xp[14], m_yp[1], "항", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[14], m_yp[0], m_xp[15], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[14], m_yp[0], m_xp[15], m_yp[1], "줄번호", "굴림체", 9, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[15], m_yp[0], m_xp[16], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[15], m_yp[0], m_xp[16], m_yp[1], "코드명", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[16], m_yp[0], m_xp[17], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[16], m_yp[0], m_xp[17], m_yp[1], "사유", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[17], m_yp[0], m_xp[18], m_yp[1]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[17], m_yp[0], m_xp[18], m_yp[1], "금액", "굴림체", 10, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[12], m_yp[0], m_Right, m_yp[2]);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[12], m_yp[0], m_Right, m_yp[1], "첨부", "굴림체", 8, false, 2);
            CXPrt.PrintTxt_A4(brickGraphics, m_xp[12], m_yp[1], m_Right, m_yp[2], "서류", "굴림체", 8, false, 2);

            CXPrt.PrintBox_A4(brickGraphics, m_Left, m_yp[2], m_xp[1], m_Bottom);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[1], m_yp[2], m_xp[2], m_Bottom);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[2], m_yp[2], m_xp[3], m_Bottom);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[3], m_yp[2], m_xp[4], m_Bottom);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[4], m_yp[2], m_xp[5], m_Bottom);
            CXPrt.PrintBox_A4(brickGraphics, m_xp[5], m_yp[2], m_xp[7], m_Bottom);

            CXPrt.PrintBox_A4(brickGraphics, m_xp[12], m_yp[2], m_xp[12], m_Bottom);

            CXPrt.PrintTxt_A4(brickGraphics, m_Left, m_Bottom, m_Right, m_Bottom + m_text_ygap * 2, "Page : " + m_pageNo, "굴림체", 9, false, 1);

        }

        private void PrintPtnt(DevExpress.XtraPrinting.BrickGraphics brickGraphics)
        {
            m_pageNo = 1;

            SetXYPos();

            PrintForm(brickGraphics);

            float yposf = m_yp[15];
            int m_max_lineno = 20;
            int lineNo = 0;
            int seqNo = 0;

            foreach (CTI31 ti31 in m_TI31s)
            {
                ++seqNo;

                float yp1 = yposf + (lineNo * m_text_height); // + m_text_ygap
                float yp2 = yposf + ((lineNo + 1) * m_text_height); // + m_text_ygap

                string objadd = "";
                if (ti31.OBJADD != "")
                {
                    string[] arr = (ti31.OBJADD + ",").Split(',');
                    foreach (string s in arr)
                    {
                        if (s == "1") objadd += (objadd != "" ? "," : "") + "1.심사결과통보서";
                        if (s == "2") objadd += (objadd != "" ? "," : "") + "2.진료기록부";
                        if (s == "3") objadd += (objadd != "" ? "," : "") + "3.X-ray film";
                        if (s == "4") objadd += (objadd != "" ? "," : "") + "4.검사결과지";
                        if (s == "5") objadd += (objadd != "" ? "," : "") + ti31.OBJADDTEXT;
                    }

                    ti31.OBJTEXT += Environment.NewLine + "*첨부서류 : " + objadd;
                }

                string objtext = MakeAutoWrap2(52, ti31.OBJTEXT);
                string[] arrobj = objtext.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                CXPrt.PrintTxt_A4(brickGraphics, m_Left, yp1, m_xp[1], yp2, seqNo.ToString(), "굴림체", 9, false, 1);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[1], yp1, m_xp[2], yp2, ti31.EPRTNO, "굴림체", 9, false, 1);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[2], yp1, m_xp[3], yp2, ti31.PNM, "굴림체", 9, false, 0);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[3], yp1, m_xp[4], yp2, (ti31.BDIV == "1" ? "외래" : "입원"), "굴림체", 9, false, 2);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[4], yp1, m_xp[5], yp2, String.Format(ti31.OBJAMT1, "#,###"), "굴림체", 9, false, 1);
                CXPrt.PrintTxt_A4(brickGraphics, m_xp[5], yp1, m_xp[7], yp2, String.Format(ti31.OBJAMT2, "#,###"), "굴림체", 9, false, 1);

                int ti31aCount = 0;
                foreach (CTI31A ti31a in m_TI31As)
                {
                    if (ti31a.EPRTNO != ti31.EPRTNO) continue;

                    ti31aCount++;

                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], yp1, m_xp[14], yp2, ti31a.HANGNO, "굴림체", 9, false, 1);
                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[14], yp1, m_xp[15], yp2, ti31a.LNO, "굴림체", 9, false, 1);
                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[15], yp1, m_xp[16], yp2, MetroLib.StrHelper.SubstringH(ti31a.PRKNM, 0, 26), "굴림체", 9, false, 0);
                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[16], yp1, m_xp[17], yp2, ti31a.JJRMK, "굴림체", 9, false, 2);
                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[17], yp1, m_xp[18], yp2, ti31a.OBJAMT, "굴림체", 9, false, 1);

                    CXPrt.PrintBox_A4(brickGraphics,  m_xp[7], yp1, m_xp[14], yp2);
                    CXPrt.PrintBox_A4(brickGraphics,  m_xp[14], yp1, m_xp[15], yp2);
                    CXPrt.PrintBox_A4(brickGraphics,  m_xp[15], yp1, m_xp[16], yp2);
                    CXPrt.PrintBox_A4(brickGraphics,  m_xp[16], yp1, m_xp[17], yp2);
                    CXPrt.PrintBox_A4(brickGraphics,  m_xp[17], yp1, m_xp[18], yp2);

                    lineNo++;
                    
                    if (lineNo > m_max_lineno)
                    {
                        m_pageNo++;
                        SetXYPos();

                        PrintForm(brickGraphics);
                        yposf = m_yp[2];
                        m_max_lineno = 46;
                        lineNo = 0;
                    }
                    
                    yp1 = yposf + (lineNo * m_text_height); // + m_text_ygap
                    yp2 = yposf + ((lineNo + 1) * m_text_height); // + m_text_ygap

                }



                foreach (string ss in arrobj)
                {
                    CXPrt.PrintTxt_A4(brickGraphics, m_xp[7], yp1, m_xp[12], yp2, ss, "굴림체", 9, false, 0);

                    lineNo++;
                    
                    if (lineNo > m_max_lineno)
                    {
                        m_pageNo++;
                        SetXYPos();

                        PrintForm(brickGraphics);
                        yposf = m_yp[2];
                        m_max_lineno = 46;
                        lineNo = 0;
                    }
                    
                    yp1 = yposf + (lineNo * m_text_height); // + m_text_ygap
                    yp2 = yposf + ((lineNo + 1) * m_text_height); // + m_text_ygap
                }

                if (ti31aCount == 0) lineNo++;

                if (lineNo > m_max_lineno)
                {
                    m_pageNo++;
                    SetXYPos();

                    PrintForm(brickGraphics);
                    yposf = m_yp[2];
                    m_max_lineno = 46;
                    lineNo = 0;
                }
                else
                {
                    // 환자사이에 줄을 긎는다.
                    yp1 = yposf + (lineNo * m_text_height); // + m_text_ygap
                    yp2 = yposf + ((lineNo + 1) * m_text_height); // + m_text_ygap
                    CXPrt.PrintBox_A4(brickGraphics,  m_Left, yp1, m_Right, yp1);
                }
            }

        }

        private void Link_CreateMarginalHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            //// Specify required settings for the brick graphics.
            //DevExpress.XtraPrinting.BrickGraphics brickGraphics = e.Graph;
            //brickGraphics.BackColor = Color.White;
            //brickGraphics.Font = new Font("Arial", 8);

            //// Declare bricks.
            //DevExpress.XtraPrinting.PageInfoBrick pageInfoBrick;
            //DevExpress.XtraPrinting.PageImageBrick pageImageBrick;

            //// Declare text strings.
            //string devexpress = "XtraPrintingSystem by Developer Express Inc.";

            //// Define the image to display.
            //Image pageImage = Image.FromFile(@"..\..\logo.png");

            //// Display the DevExpress text string.
            //SizeF size = brickGraphics.MeasureString(devexpress);
            //pageInfoBrick = brickGraphics.DrawPageInfo(DevExpress.XtraPrinting.PageInfo.None, devexpress, Color.Black, new RectangleF(new PointF(343 - (size.Width - pageImage.Width) / 2, pageImage.Height + 3), size), DevExpress.XtraPrinting.BorderSide.None);
            //pageInfoBrick.Alignment = DevExpress.XtraPrinting.BrickAlignment.Center;

            //// Display the PageImageBrick containing the DevExpress logo.
            //pageImageBrick = brickGraphics.DrawPageImage(pageImage, new RectangleF(343, 0, pageImage.Width, pageImage.Height), DevExpress.XtraPrinting.BorderSide.None, Color.Transparent);
            //pageImageBrick.Alignment = DevExpress.XtraPrinting.BrickAlignment.Center;

            //// Set the rectangle for a page info brick. 
            //RectangleF r = RectangleF.Empty;
            //r.Height = 20;

            //// Display the PageInfoBrick containing date-time information. Date-time information is displayed
            //// in the left part of the MarginalHeader section using the FullDateTimePattern.
            //pageInfoBrick = brickGraphics.DrawPageInfo(DevExpress.XtraPrinting.PageInfo.DateTime, "{0:F}", Color.Black, r, DevExpress.XtraPrinting.BorderSide.None);
            //pageInfoBrick.Alignment = DevExpress.XtraPrinting.BrickAlignment.Near;

            //// Display the PageInfoBrick containing the page number among total pages. The page number
            //// is displayed in the right part of the MarginalHeader section.
            //pageInfoBrick = brickGraphics.DrawPageInfo(DevExpress.XtraPrinting.PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Black, r, DevExpress.XtraPrinting.BorderSide.None);
            //pageInfoBrick.Alignment = DevExpress.XtraPrinting.BrickAlignment.Far;
        }

        private void rbObjA_CheckedChanged(object sender, EventArgs e)
        {
            ObjAB_Changed();
        }

        private void rbObjB_CheckedChanged(object sender, EventArgs e)
        {
            ObjAB_Changed();
        }

        private void ObjAB_Changed()
        {
            if (rbObjA.Checked == true)
            {
                // 재심사조정청구
                btnMake.Text = "재심청구서 생성";
                btnPrint.Text = "재심청구서 출력";
                btnDelete.Text = "재심청구서 삭제";
                grdMainView.Columns["OBJAMT1"].Caption = "재심청구금액1";
                grdMainView.Columns["OBJAMT2"].Caption = "재심청구금액2";
            }
            else
            {
                // 이의신청
                btnMake.Text = "이의선청서 생성";
                btnPrint.Text = "이의신청서 출력";
                btnDelete.Text = "이의신청서 출력";
                grdMainView.Columns["OBJAMT1"].Caption = "이의신청금액1";
                grdMainView.Columns["OBJAMT2"].Caption = "이의신청금액2";
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                string docuno = Convert.ToString(grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DOCUNO"));
                ADD0602E_2 f = new ADD0602E_2(docuno);
                f.ShowDialog(this);
                docuno = f.m_docuno;
                if (docuno == "") return;

                if (MessageBox.Show("문서번호 : " + docuno + " 를 청구완료하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Finish(docuno, true);
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

            btnQuery.PerformClick();
        }

        private void btnFinishCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string docuno = Convert.ToString(grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DOCUNO"));
                ADD0602E_2 f = new ADD0602E_2(docuno);
                f.ShowDialog(this);
                docuno = f.m_docuno;
                if (docuno == "") return;

                if (MessageBox.Show("문서번호 : " + docuno + " 를 완료취소하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Finish(docuno, false);
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

            btnQuery.PerformClick();
        }

        private void Finish(string p_docuno, bool b_finish)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string hdate = "";
                string htime = "";

                if (b_finish == true)
                {
                    hdate = MetroLib.Util.GetSysDate(conn);
                    htime = MetroLib.Util.GetSysTime(conn);
                }

                string sql = "";
                sql = sql + Environment.NewLine + "UPDATE TI32 ";
                sql = sql + Environment.NewLine + "   SET PRTDT='" + hdate + "' ";
                sql = sql + Environment.NewLine + "     , PRTTM='" + htime + "' ";
                sql = sql + Environment.NewLine + "     , PRTID='" + m_User + "' ";
                sql = sql + Environment.NewLine + " WHERE OBJDIV='" + GetObjdiv() + "' ";
                sql = sql + Environment.NewLine + "   AND DOCUNO='" + p_docuno + "' ";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);

                conn.Close();
            }
        }

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

namespace ADD0390E
{
    public partial class ADD0390E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private String m_ErrKeys;

        private bool IsFirst;

        public ADD0390E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            m_ErrKeys = "";
        }

        public ADD0390E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();
        }

        private void ADD0390E_Load(object sender, EventArgs e)
        {
            this.IsFirst = true;
        }

        private void ADD0390E_Activated(object sender, EventArgs e)
        {
            if (this.IsFirst == false) return;
            this.IsFirst = false;

            if (m_Addpara != "")
            {
                this.Width = 300;
                this.Height = 80;
                this.panel1.Visible = false;
                this.panel2.Visible = false;
                this.panel3.Visible = false;
                this.panel4.Visible = false;
                Application.DoEvents();

                RunAuto(m_Addpara);
                this.Close();
            }

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

        private void RunAuto(string p_addpara)
        {
            try
            {
                m_ErrKeys = "";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "처리 중입니다.");

                char d_lev2 = (char)22;
                char d_lev1 = (char)21;
                string[] lines = (p_addpara + d_lev2).Split(d_lev2);
                foreach (string line in lines)
                {
                    if (line == "") continue;
                    string[] para = line.Split(d_lev1);
                    List<CMakeData> list = new List<CMakeData>();
                    CMakeData data = new CMakeData();
                    data.IOFG = para[0];
                    data.K1 = para[1];
                    data.K2 = para[2];
                    data.K3 = para[3];
                    data.K4 = para[4];
                    data.K5 = para[5];
                    data.K6 = para[6];
                    data.ADDZ1 = (para.Length < 13 ? "" : para[12]);
                    data.SIMNO = (para.Length < 14 ? "" : para[13]);
                    data.CHUGAFG = (para.Length < 16 ? "" : para[15]);
                    list.Add(data);

                    MakeBoOrChu(list);
                }


                this.CloseProgressForm("", "처리 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "처리 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "\r\n" + "\r\n" + m_ErrKeys);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtYYMM.Text.ToString() == "")
            {
                MessageBox.Show("작업년월을 입력하세요.");
                return;
            }

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
            string gbn = "1"; // 양방
            if (rbYHgbn2.Checked) gbn = "2"; // 한방
            string iofg = "1"; // 외래
            if (rbIofg2.Checked) iofg = "2"; // 입원
            string qfy = "2"; // 보험.공상
            if (rbQfy3.Checked) qfy = "3"; // 보호
            else if (rbQfy5.Checked) qfy = "5"; // 산재
            else if (rbQfy6.Checked) qfy = "6"; // 자보
            else if (rbQfy38.Checked) qfy = "38"; // 보호정신과
            else if (rbQfy29.Checked) qfy = "29"; // 보훈일반
            string jrby = ""; // 전체
            if (rbJrby1.Checked) jrby = "1"; // 내과
            else if (rbJrby2.Checked) jrby = "2"; // 외과
            else if (rbJrby3.Checked) jrby = "3"; // 산소
            else if (rbJrby4.Checked) jrby = "4"; // 안이
            else if (rbJrby5.Checked) jrby = "5"; // 피비
            else if (rbJrby6.Checked) jrby = "6"; // 치과
            else if (rbJrby7.Checked) jrby = "7"; // 한방
            string banfg = ""; // 반송된 자료만
            string yymm = txtYYMM.Text.ToString();
            string jbfg = "2"; // 보험
            if (qfy == "3") jbfg = "5";//보호
            if (qfy == "29") jbfg = "7";//보훈일반

            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (iofg == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT A.DEMNO";
            sql = sql + Environment.NewLine + "     , ( ";
            sql = sql + Environment.NewLine + "         SELECT DISTINCT H.COMPLDT"; // 2006.08.18 WOOIL - DISTINCT 추가(오류발생:하위 쿼리에서 값을 둘 이상 반환했습니다.)
            sql = sql + Environment.NewLine + "           FROM TIE_H010 H";
            sql = sql + Environment.NewLine + "          WHERE H.GBN   = '" + gbn + "' ";
            sql = sql + Environment.NewLine + "            AND H.IOFG  = '" + iofg + "' ";
            sql = sql + Environment.NewLine + "            AND H.DEMNO = A.DEMNO";
            sql = sql + Environment.NewLine + "       ) COMDT";
            sql = sql + Environment.NewLine + "     , ( ";                         // 2006.10.11 WOOIL - DISTINCT 추가(오류발생:하위 쿼리에서 값을 둘 이상 반환했습니다.)
            sql = sql + Environment.NewLine + "         SELECT TOP 1 CNECTNO ";    //                  - 같은 청구번호(DEMNO)로 외래와 입원이 발생하였음.
            sql = sql + Environment.NewLine + "           FROM TIE_F0102 F0102 ";
            sql = sql + Environment.NewLine + "          WHERE F0102.DEMNO = A.DEMNO ";
            sql = sql + Environment.NewLine + "            AND ISNULL(F0102.CNECTNO,'') <> ''";
            sql = sql + Environment.NewLine + "            AND F0102.JBFG  = '" + jbfg + "' ";
            sql = sql + Environment.NewLine + "          ORDER BY CNECTNO DESC ";
            sql = sql + Environment.NewLine + "       ) CNECTNO ";
            sql = sql + Environment.NewLine + "     , A.EPRTNO";
            sql = sql + Environment.NewLine + "     , A.SIMNO";
            sql = sql + Environment.NewLine + "     , A.PID";
            sql = sql + Environment.NewLine + "     , A.PNM";
            sql = sql + Environment.NewLine + "     , A.QFYCD";
            sql = sql + Environment.NewLine + "     , A.SIMCS";
            sql = sql + Environment.NewLine + "     , DBO.MFN_PIECE(A.JRKWA,'$',3) DPTCD";
            sql = sql + Environment.NewLine + "     , A." + fEXDATE + " K1 ";
            sql = sql + Environment.NewLine + "     , A.QFYCD  K2 ";
            sql = sql + Environment.NewLine + "     , A.JRBY   K3 ";
            sql = sql + Environment.NewLine + "     , A.PID    K4 ";
            sql = sql + Environment.NewLine + "     , A.UNISQ  K5 ";
            sql = sql + Environment.NewLine + "     , A.SIMCS  K6 ";
            sql = sql + Environment.NewLine + "     , ( ";
            sql = sql + Environment.NewLine + "        SELECT MIN(JJRMK) ";
            sql = sql + Environment.NewLine + "          FROM ( ";
            sql = sql + Environment.NewLine + "               SELECT ISNULL(JJRMK,'') AS JJRMK FROM TIE_F0203_062 X WHERE X.DEMNO=A.DEMNO AND X.EPRTNO=A.EPRTNO AND JJRMK IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD')   ";
            sql = sql + Environment.NewLine + "               UNION ALL  ";
            sql = sql + Environment.NewLine + "               SELECT ISNULL(JJRMK,'') AS JJRMK FROM TIE_F0603_062 X WHERE X.DEMNO=A.DEMNO AND X.EPRTNO=A.EPRTNO AND JJRMK IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD')   ";
            sql = sql + Environment.NewLine + "               ) X ";
            sql = sql + Environment.NewLine + "       ) JJRMK ";
            sql = sql + Environment.NewLine + "     , (SELECT COUNT(*) FROM " + tTI1A + " X WHERE X." + fEXDATE + "=A." + fEXDATE + " AND X.QFYCD=A.QFYCD AND X.JRBY=A.JRBY AND X.PID=A.PID AND X.UNISQ=A.UNISQ AND X.SIMCS>A.SIMCS) AS BO_CNT ";
            // 2014.05.21 KJW - 보완청구번호,추가청구번호 추가함.
            sql = sql + Environment.NewLine + "     , (SELECT TOP 1 X.DEMNO FROM " + tTI1A + " X WHERE X." + fEXDATE + "=A." + fEXDATE + " AND X.QFYCD=A.QFYCD AND X.JRBY=A.JRBY AND X.PID=A.PID AND X.UNISQ=A.UNISQ AND X.SIMCS>A.SIMCS AND X.ADDZ1 = '1' ORDER BY X.DEMNO DESC) AS BO_DEMNO ";
            sql = sql + Environment.NewLine + "     , (SELECT TOP 1 X.DEMNO FROM " + tTI1A + " X WHERE X." + fEXDATE + "=A." + fEXDATE + " AND X.QFYCD=A.QFYCD AND X.JRBY=A.JRBY AND X.PID=A.PID AND X.UNISQ>A.UNISQ AND X.SIMCS=A.SIMCS AND X.ADDZ1 = '2' ORDER BY X.DEMNO DESC) AS CHU_DEMNO ";
            sql = sql + Environment.NewLine + "  FROM " + tTI1A + " A INNER JOIN TA09 A09 ON A09.DPTCD= DBO.MFN_PIECE(A.JRKWA,'$',3)";
            sql = sql + Environment.NewLine + " WHERE A." + fEXDATE + " LIKE '" + yymm + "%' ";
            sql = sql + Environment.NewLine + "   AND A.SIMCS  > 0 ";
            sql = sql + Environment.NewLine + "   AND ISNULL(A.ADDZ1,'') IN ('','0','3') ";

            if (qfy == "2")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('21','22','23','40') ";
            }
            else if (qfy == "3")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('31','32') ";
            }
            else if (qfy == "5")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('50') ";
            }
            else if (qfy == "6")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('61') ";
            }
            else if (qfy == "38")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD IN ('38','39') ";
            }
            else if (qfy == "29")
            {
                sql = sql + Environment.NewLine + "   AND A.QFYCD = '29' "; // 2006.05.23 WOOIL - 보훈일반 추가
            }

            if (jrby != "")
            {
                sql = sql + Environment.NewLine + "   AND A.JRKWA LIKE '" + jrby + "%' ";
            }

            sql = sql + Environment.NewLine + "   AND ISNULL(A.DONFG,'')='Y'";
            sql = sql + Environment.NewLine + "   AND ISNULL(A.DELFG,'')=''";
            sql = sql + Environment.NewLine + "   AND ISNULL(A09.ADDDPTCD,'')='" + m_HospMulti + "' ";

            // 2006.06.12 WOOIL - 반송된 자료만 조회
            if (banfg == "1")
            {
                sql = sql + Environment.NewLine + "   AND ( ";
                sql = sql + Environment.NewLine + "           EXISTS ( SELECT * FROM TIE_F0203_062 X WHERE X.DEMNO=A.DEMNO AND X.EPRTNO=A.EPRTNO AND JJRMK IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD') ) ";
                sql = sql + Environment.NewLine + "        OR EXISTS ( SELECT * FROM TIE_F0603_062 X WHERE X.DEMNO=A.DEMNO AND X.EPRTNO=A.EPRTNO AND JJRMK IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BULCD') ) ";
                sql = sql + Environment.NewLine + "       ) ";
            }

            sql = sql + Environment.NewLine + " ORDER BY CASE WHEN ISNULL(A.DEMNO,'')='' THEN '9999999999' ELSE A.DEMNO END,A.EPRTNO,A.JRKWA,A.PID,A.STEDT ";

            grdMain.DataSource=null;
            List<CData> list = new List<CData>();
            grdMain.DataSource=list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CData data = new CData();
                            data.OP = false;
                            data.DEMNO = reader["DEMNO"].ToString();
                            data.COMDT = reader["COMDT"].ToString();
                            data.CNECTNO = reader["CNECTNO"].ToString();
                            data.EPRTNO = reader["EPRTNO"].ToString();
                            data.SIMNO = reader["SIMNO"].ToString();
                            data.PID = reader["PID"].ToString();
                            data.PNM = reader["PNM"].ToString();
                            data.QFYCD = reader["QFYCD"].ToString();
                            data.DPTCD = reader["DPTCD"].ToString();
                            data.JJRMK = reader["JJRMK"].ToString();
                            data.BO_CNT = reader["BO_CNT"].ToString();
                            data.BO_DEMNO = reader["BO_DEMNO"].ToString();
                            data.CHU_DEMNO = reader["CHU_DEMNO"].ToString();
                            data.K1 = reader["K1"].ToString();
                            data.K2 = reader["K2"].ToString();
                            data.K3 = reader["K3"].ToString();
                            data.K4 = reader["K4"].ToString();
                            data.K5 = reader["K5"].ToString();
                            data.K6 = reader["K6"].ToString();
                            data.IOFG = iofg;

                            list.Add(data);
                        }
                        reader.Close();
                    }
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

        private void btnMakeBo_Click(object sender, EventArgs e)
        {
            try
            {
                m_ErrKeys = "";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeBo();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "\r\n" + "\r\n" + m_ErrKeys);
            }
        }

        private void MakeBo()
        {
            List<CMakeData> list = new List<CMakeData>();

            // 보완청구내역생성
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                bool op = (bool)grdMainView.GetRowCellValue(row, gcOP);
                if (op)
                {
                    CMakeData data = new CMakeData();
                    data.K1 = grdMainView.GetRowCellValue(row, gcK1).ToString(); // EXDATE, BDODT
                    data.K2 = grdMainView.GetRowCellValue(row, gcK2).ToString(); // QFYCD
                    data.K3 = grdMainView.GetRowCellValue(row, gcK3).ToString(); // JRBY
                    data.K4 = grdMainView.GetRowCellValue(row, gcK4).ToString(); // PID
                    data.K5 = grdMainView.GetRowCellValue(row, gcK5).ToString(); // UNISQ
                    data.K6 = grdMainView.GetRowCellValue(row, gcK6).ToString(); // SIMCS
                    data.SIMNO = grdMainView.GetRowCellValue(row, gcSIMNO).ToString();
                    data.IOFG = grdMainView.GetRowCellValue(row, gcIOFG).ToString();
                    data.ADDZ1 = "1";
                    data.CHUGAFG = "";

                    list.Add(data);
                }
            }

            if (list.Count > 0) MakeBoOrChu(list);
        }

        private void btnMakeChu_Click(object sender, EventArgs e)
        {
            try
            {
                m_ErrKeys = "";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeChu();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "\r\n" + "\r\n" + m_ErrKeys);
            }
        }

        private void MakeChu()
        {
            List<CMakeData> list = new List<CMakeData>();

            // 추가청구내역생성
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                bool op = (bool)grdMainView.GetRowCellValue(row, gcOP);
                if (op)
                {
                    CMakeData data = new CMakeData();
                    data.K1 = grdMainView.GetRowCellValue(row, gcK1).ToString(); // EXDATE, BDODT
                    data.K2 = grdMainView.GetRowCellValue(row, gcK2).ToString(); // QFYCD
                    data.K3 = grdMainView.GetRowCellValue(row, gcK3).ToString(); // JRBY
                    data.K4 = grdMainView.GetRowCellValue(row, gcK4).ToString(); // PID
                    data.K5 = grdMainView.GetRowCellValue(row, gcK5).ToString(); // UNISQ
                    data.K6 = grdMainView.GetRowCellValue(row, gcK6).ToString(); // SIMCS
                    data.SIMNO = grdMainView.GetRowCellValue(row, gcSIMNO).ToString();
                    data.IOFG = grdMainView.GetRowCellValue(row, gcIOFG).ToString();
                    data.ADDZ1 = "2";
                    data.CHUGAFG = "";

                    list.Add(data);
                }
            }

            if (list.Count > 0) MakeBoOrChu(list);
        }


        private void btnMakeChu2_Click(object sender, EventArgs e)
        {
            try
            {
                m_ErrKeys = "";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeChu2();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + "\r\n" + "\r\n" + m_ErrKeys);
            }
        }

        private void MakeChu2()
        {
            List<CMakeData> list = new List<CMakeData>();

            // 추가청구내역생성.진료내역포함
            for (int row = 0; row < grdMainView.RowCount; row++)
            {
                bool op = (bool)grdMainView.GetRowCellValue(row, gcOP);
                if (op)
                {
                    CMakeData data = new CMakeData();
                    data.K1 = grdMainView.GetRowCellValue(row, gcK1).ToString(); // EXDATE, BDODT
                    data.K2 = grdMainView.GetRowCellValue(row, gcK2).ToString(); // QFYCD
                    data.K3 = grdMainView.GetRowCellValue(row, gcK3).ToString(); // JRBY
                    data.K4 = grdMainView.GetRowCellValue(row, gcK4).ToString(); // PID
                    data.K5 = grdMainView.GetRowCellValue(row, gcK5).ToString(); // UNISQ
                    data.K6 = grdMainView.GetRowCellValue(row, gcK6).ToString(); // SIMCS
                    data.SIMNO = grdMainView.GetRowCellValue(row, gcSIMNO).ToString();
                    data.IOFG = grdMainView.GetRowCellValue(row, gcIOFG).ToString();
                    data.ADDZ1 = "2";
                    data.CHUGAFG = "1";

                    list.Add(data);
                }
            }

            if (list.Count > 0) MakeBoOrChu(list);
        }

        private void MakeBoOrChu(List<CMakeData> p_list)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    foreach (CMakeData data in p_list)
                    {
                        m_ErrKeys = data.IOFG + "," + data.K1 + "," + data.K2 + "," + data.K3 + "," + data.K4 + "," + data.K5 + "," + data.K6;
                        SaveBoOrChu(data.IOFG, data.K1, data.K2, data.K3, data.K4, data.K5, data.K6, data.SIMNO, data.ADDZ1, data.CHUGAFG, conn, tran);
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

        private void SaveBoOrChu(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, string p_k5, string p_k6, string p_simno, string p_addz1, string p_chugafg, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string new_unisq = "";
            string new_simno = "";
            string new_simcs = "";
            if (p_addz1 == "1")
            {
                // 보완청구
                new_unisq = p_k5;
                new_simno = p_simno;
                new_simcs = GetNextSimcs(p_iofg, p_k1, p_k2, p_k3, p_k4, p_k5, p_conn, p_tran);
            }
            else if (p_addz1 == "2")
            {
                // 추가청구
                new_unisq = GetNextUnisq(p_iofg, p_k1, p_k2, p_k3, p_k4, p_conn, p_tran);
                new_simno = GetNextSimno(p_iofg, p_k1, p_k2, p_k3, p_k4, p_k5, p_k6, p_conn, p_tran);
                new_simcs = p_k6;
            }
            else
            {
                // 보완청구 추가청구 이외에는 종료
                return;
            }
            string new_cnecno = ""; // 접수번호
            string new_sau = ""; // 반송사유
            GetCnecnoAndSau(p_iofg, p_k1, p_k2, p_k3, p_k4, p_k5, p_k6, p_addz1, p_conn, p_tran, ref new_cnecno, ref new_sau);
            string drgfg = GetDrgfg(p_iofg, p_k1, p_k2, p_k3, p_k4, p_k5, p_k6, p_conn, p_tran);

            string tTI2A = "TI2A";
            string tTI2AR = "TI2AR";
            string tTI2B = "TI2B";
            string tTI2E = "TI2E";
            string tTI2F = "TI2F";
            string tTI2H = "TI2H";
            string tTI23 = "TI23";
            string tTI2J = "TI2J";
            string tTI23T = "TI23T";
            string tTI24 = "TI24";

            string fBDODT = "BDODT";
            if (p_iofg == "1")
            {
                tTI2A = "TI1A";
                tTI2AR = "TI1AR";
                tTI2B = "TI1B";
                tTI2E = "TI1E";
                tTI2F = "TI1F";
                tTI2H = "TI1H";
                tTI23 = "TI13";
                tTI2J = "TI1J";
                tTI23T = "TI13T";
                tTI24 = "TI14";
                fBDODT = "EXDATE";
            }

            // 생성
            string sql="";
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO " + tTI2A + "";
            sql = sql + Environment.NewLine + "      ("+ fBDODT + ",QFYCD,JRBY,PID,UNISQ,SIMCS";
            sql = sql + Environment.NewLine + "      ,UNICD,UNINM,INSNM,PNM,INSID,RESID,PSEX,FMRCD,JBFG,EMPID,NBPID,PDIV,APRDT,GENDT,    SIMNO,        MADDR,PRTFG,REMARK,SIMFG,YYMM,DEMNO,APPRNO,JAJR,BOHUN,JANGAEFG,RPID,OPRFG,DAETC,TJKH,STEDT,JRKK,RSLT,DISEAPOS,BDEDT,FSTDT,INSTRU,BEDODT,EXAMC,XDAYS,XDAYS2,CTCAK,MRICA,JSOGE,HSOGE,GSRT,GSGUM,TTAMT,PTAMT,UNAMT,JAM,RELAM,MAXPTAMT,GAMACK,CSCD,GSCD,XJDFG,XCRFG,IPATH,ADDZ1,                 ADDZ2,              ADDZ3,      ADDZ4";
            sql = sql + Environment.NewLine + "      ,ARVPATH,EMDB,AMTCHK,RPTPTAMT,TT41KEY,HRFG,DAILYSUMFG,MAXAUTOFG,GBFRDT,GBTODT,SJSTEDT,SBRDNTYPE,CFHCCFRNO,YOFG,YOPDIV,JIWONAMT,MT020,GONSGB,HWTTAMT,YOGROUP,MT029,PDIVM,BOHUNDCFG,BOHUNDCCD,DAILYPTAMTFG,FOREIGNFG,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,TUBERFG,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,PACAREFG,SJSDFG,DRGCHUGAFG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT";
            sql = sql + Environment.NewLine + "      ,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT,NPO_TTAMT,NPO_PTAMT,NPO_UNAMT,NPO_JAM,CHILD_REH)";
            sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + "";
            sql = sql + Environment.NewLine + "      ,UNICD,UNINM,INSNM,PNM,INSID,RESID,PSEX,FMRCD,JBFG,EMPID,NBPID,PDIV,APRDT,GENDT," + new_simno + ",MADDR,PRTFG,REMARK,SIMFG,YYMM,'',   APPRNO,JAJR,BOHUN,JANGAEFG,RPID,OPRFG,DAETC,TJKH,STEDT,JRKK,RSLT,DISEAPOS,BDEDT,FSTDT,INSTRU,BEDODT,EXAMC,XDAYS,XDAYS2,CTCAK,MRICA,JSOGE,HSOGE,GSRT,GSGUM,TTAMT,PTAMT,UNAMT,JAM,RELAM,MAXPTAMT,GAMACK,CSCD,GSCD,XJDFG,XCRFG,IPATH,'" + p_addz1 + "','" + new_cnecno + "','" + new_sau + "',EPRTNO";
            sql = sql + Environment.NewLine + "      ,ARVPATH,EMDB,AMTCHK,RPTPTAMT,TT41KEY,HRFG,DAILYSUMFG,MAXAUTOFG,GBFRDT,GBTODT,SJSTEDT,SBRDNTYPE,CFHCCFRNO,YOFG,YOPDIV,JIWONAMT,MT020,GONSGB,HWTTAMT,YOGROUP,MT029,PDIVM,BOHUNDCFG,BOHUNDCCD,DAILYPTAMTFG,FOREIGNFG,BOHUNDCAMT,UPLMTCHATTAMT,PTTTAMT,TJKHFIX,DACD,DANM,JRKWA,IPWON,TGWON,PDRID,JLYL,TSJRAMT,SJ070,SJPDIV,BAKAMT,GANTYPE,DRGFG,DRGNO,BHPTAMTFG,BHPTAMT,TUBERFG,JBPTAMT,WARRANTY,F008FG,BAKDNTTAMT,BAKDNPTAMT,BAKDNUNAMT,BAKDNBHUNAMT,SEWOLFG,QLFRESTRICTCD,F009FG,F010FG,PACAREFG,SJSDFG,DRGCHUGAFG,ERSERIOUS,REQ,C111FG,F012FG,NRSVCFG,C049AMT,NDRGGBN,NDRGSUGA,NDRGTTAMT";
            sql = sql + Environment.NewLine + "      ,PTCLSCD,HOME_HOSPICE,DRGSUGA,DRGTTAMT,INITDT,NPO_TTAMT,NPO_PTAMT,NPO_UNAMT,NPO_JAM,CHILD_REH";
            sql = sql + Environment.NewLine + "  FROM " + tTI2A + " ";
            sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
            sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
            sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            // 2020.02.11 WOOIL - 추가청구이면 내원일수,요양일수를 0으로 만든다
            // 2020.07.29 WOOIL - DRG이면 EXAMC,JRKK를 0으로 만들지 않는다.
            if (p_addz1 == "2" && drgfg != "1")
            {
                sql = "";
                sql = sql + Environment.NewLine + "UPDATE " + tTI2A + "";
                sql = sql + Environment.NewLine + "   SET EXAMC=0,JRKK=0";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + new_unisq + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + new_simcs + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            //  2019.05.17 WOOIL - 추가
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO " + tTI2AR + "";
            sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,UNISQ,SIMCS";
            sql = sql + Environment.NewLine + "      ,RESID)";
            sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + "";
            sql = sql + Environment.NewLine + "      ,RESID";
            sql = sql + Environment.NewLine + "  FROM " + tTI2AR + " ";
            sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
            sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
            sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            // 2016.05.19 KJW - 빈 심사자메모 생성.(INNER JOIN을 위해.)
            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TI20";
            sql = sql + Environment.NewLine + "      (IOFG,K1,K2,K3,K4,    K5,               K6,           SIMTEXT)";
            sql = sql + Environment.NewLine + "SELECT IOFG,K1,K2,K3,K4," + new_unisq + "," + new_simcs + ",ISNULL(SIMTEXT,'') ";
            sql = sql + Environment.NewLine + "  FROM TI20 ";
            sql = sql + Environment.NewLine + " WHERE IOFG = '" + p_iofg + "'";
            sql = sql + Environment.NewLine + "   AND K1  = '" + p_k1 + "' ";
            sql = sql + Environment.NewLine + "   AND K2  = '" + p_k2 + "' ";
            sql = sql + Environment.NewLine + "   AND K3  = '" + p_k3 + "' ";
            sql = sql + Environment.NewLine + "   AND K4  = '" + p_k4 + "' ";
            sql = sql + Environment.NewLine + "   AND K5  =  " + p_k5 + "  ";
            sql = sql + Environment.NewLine + "   AND K6  =  " + p_k6 + "  ";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            // 2012.01.05 WOOIL - 산재는 무조건 070
            if (p_k2 == "50")
            {
                sql = "";
                sql = sql + Environment.NewLine + "UPDATE " + tTI2A + "";
                sql = sql + Environment.NewLine + "   SET SJ070='070'";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + new_unisq + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + new_simcs + "  ";
                sql = sql + Environment.NewLine + "   AND ISNULL(SJ070,'')=''";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            else if (p_k2.Substring(0, 1) == "2" || p_k2.Substring(0, 1) == "3" || p_k2.Substring(0, 1) == "4")
            {
                if (p_iofg == "1")
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "UPDATE TI1A";
                    sql = sql + Environment.NewLine + "   SET DAILYSUMFG='1',DAILYPTAMTFG=''";
                    sql = sql + Environment.NewLine + " WHERE EXDATE = '" + p_k1 + "' ";
                    sql = sql + Environment.NewLine + "   AND QFYCD  = '" + p_k2 + "' ";
                    sql = sql + Environment.NewLine + "   AND JRBY   = '" + p_k3 + "' ";
                    sql = sql + Environment.NewLine + "   AND PID    = '" + p_k4 + "' ";
                    sql = sql + Environment.NewLine + "   AND UNISQ  =  " + new_unisq + "  ";
                    sql = sql + Environment.NewLine + "   AND SIMCS  =  " + new_simcs + "  ";
                    using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO " + tTI2B + "";
            sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        SEQ1,DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,JRKWA,IPWON,TGWON,PDRID,ROFG,DAEXDT,POA)";
            sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",SEQ1,DACD,DANM,TPOS1,TPOS2,TPOS3,TPOS4,JRKWA,IPWON,TGWON,PDRID,ROFG,DAEXDT,POA ";
            sql = sql + Environment.NewLine + "  FROM " + tTI2B + " ";
            sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
            sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
            sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
            sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
            sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
            sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.ExecuteNonQuery();
            }

            if (drgfg == "1")
            {
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO TI2K";
                sql = sql + Environment.NewLine + "      (BDODT,QFYCD,JRBY,PID,    UNISQ,            SIMCS,        OPRCD,OPRCD1,OPRCD2,OPRCD3,OPRCD4,OPRCD5,OPRCD6,OPRCD7,OPRCD8,OPRCD9,EXMCD1,EXMCD2,EXMCD3,EXMCD4,EXMCD5,RADCD1,RADCD2,RADCD3,RADCD4,RADCD5,INJCD1,INJCD2,INJCD3,INJCD4,INJCD5,ANECD1,ANECD2,ANECD3,ANECD4,ANECD5,DETDIV1,DETDIV2,DETDIV3,DETDIV4,DETDIV5,TOTAMT,PTAMT,INSAMT,JAM,CALCFG,ALCOL,WEIGHT,AHOUR,NTDATE,NTTIME,TOTAMT1)";
                sql = sql + Environment.NewLine + "SELECT BDODT,QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",OPRCD,OPRCD1,OPRCD2,OPRCD3,OPRCD4,OPRCD5,OPRCD6,OPRCD7,OPRCD8,OPRCD9,EXMCD1,EXMCD2,EXMCD3,EXMCD4,EXMCD5,RADCD1,RADCD2,RADCD3,RADCD4,RADCD5,INJCD1,INJCD2,INJCD3,INJCD4,INJCD5,ANECD1,ANECD2,ANECD3,ANECD4,ANECD5,DETDIV1,DETDIV2,DETDIV3,DETDIV4,DETDIV5,TOTAMT,PTAMT,INSAMT,JAM,CALCFG,ALCOL,WEIGHT,AHOUR,NTDATE,NTTIME,TOTAMT1 ";
                sql = sql + Environment.NewLine + "  FROM TI2K ";
                sql = sql + Environment.NewLine + " WHERE BDODT = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            if (p_addz1 == "1" || p_chugafg == "1")
            {
                // 보완청구이거나 추가청구인데 진료내역을 모두 포함시키는 경우
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI2E + "";
                sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        SEQ1,SEQ2,CNT,JGMAK,HGMAK) ";
                sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",SEQ1,SEQ2,CNT,JGMAK,HGMAK  ";
                sql = sql + Environment.NewLine + "  FROM " + tTI2E + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
        
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI2F + "";
                sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO) ";
                sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO  ";
                sql = sql + Environment.NewLine + "  FROM " + tTI2F + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
        
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI2H + "";
                sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO) ";
                sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",SEQ1,SEQ2,OP,PRICD,BGIHO,PRKNM,NTDIV,FCRFG,DANGA,DQTY,DDAY,GUMAK,EXDT,POS2,MAFG,ACTFG,EVENT,DRGCD,STTEX,IPOS1,ALLEX,GRPCD,GRPACT,GRPNM,RSNCD,REMARK,FRDT,TODT,PRIDT,ELINENO,OKCD,REFCD,LOWFG,CDENTDT,TPOS1,TPOS2,TPOS3,TPOS4,CDGB,MULTIRMK,EXHM,CHRLT,AFPFG,CDCHGDT,HBPRICD,CNTQTY,LOWRSNCD,LOWRSNRMK,DANGACHK,DRADDFG,UPLMTAMT,UPLMTCHAAMT,EDIENTDT,ADDFG1,BHEXFG,SPFG,SPRT,SPAMT,SPPOS2,JBPTFG,DRIDLIST,INREFFG,INREFCD,OYAKFG,BOSANGRT,GUMAK2,DRG7_ADD_FG,DRG7_ADD_RT,DRG7_ADD_GUMAK2,INREFFG2,INREFCD2,DRG7_SEQ1,DRG7_POS2,DRG7_ELINENO  ";
                sql = sql + Environment.NewLine + "  FROM " + tTI2H + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
        
                sql = "";
                sql = sql + Environment.NewLine + " INSERT INTO " + tTI23 + "";
                sql = sql + Environment.NewLine + "       (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        OUTSEQ,SEQ,PRICD,BGIHO,PRKNM,DANGA,DQTY,DDAY,GUMAK,ORDCNT,ELINENO,LOWFG,CDGB,ODAY,BAEKFG,LOWRSNCD,LOWRSNRMK)";
                sql = sql + Environment.NewLine + " SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",OUTSEQ,SEQ,PRICD,BGIHO,PRKNM,DANGA,DQTY,DDAY,GUMAK,ORDCNT,ELINENO,LOWFG,CDGB,ODAY,BAEKFG,LOWRSNCD,LOWRSNRMK ";
                sql = sql + Environment.NewLine + "  FROM " + tTI23 + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "";
                sql = sql + Environment.NewLine + " INSERT INTO " + tTI2J + "";
                sql = sql + Environment.NewLine + "       (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        ELINENO,SEQ,TJCD,TJCDRMK,MAFG) ";
                sql = sql + Environment.NewLine + " SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",ELINENO,SEQ,TJCD,TJCDRMK,MAFG  ";
                sql = sql + Environment.NewLine + "   FROM " + tTI2J + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
        
                sql = "";
                sql = sql + Environment.NewLine + " INSERT INTO " + tTI23T + "";
                sql = sql + Environment.NewLine + "       (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        OUTSEQ,SEQ,SEQNO,TJCD,TJCDRMK)";
                sql = sql + Environment.NewLine + " SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",OUTSEQ,SEQ,SEQNO,TJCD,TJCDRMK ";
                sql = sql + Environment.NewLine + "  FROM " + tTI23T + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
        
                sql = "";
                sql = sql + Environment.NewLine + " INSERT INTO " + tTI24 + "";
                sql = sql + Environment.NewLine + "       (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        SEQ,FG,PRICD,PRKNM,DQTY,DDAY,ORDCNT,LOWFG,CDGB,OUTSEQ,BGIHO,DANGA,GUMAK,ELINENO,ODAY,LOWRSNCD,LOWRSNRMK)";
                sql = sql + Environment.NewLine + " SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",SEQ,FG,PRICD,PRKNM,DQTY,DDAY,ORDCNT,LOWFG,CDGB,OUTSEQ,BGIHO,DANGA,GUMAK,ELINENO,ODAY,LOWRSNCD,LOWRSNRMK ";
                sql = sql + Environment.NewLine + "   FROM " + tTI24+ " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            else if (p_addz1 == "2")
            {
                // 추가청구
                // 2015.04.09 KJW - 추가청구일때 MT014가 있으면 생성되게 수정함.(메트로병원요청)
                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI2J + "";
                sql = sql + Environment.NewLine + "      (" + fBDODT + ",QFYCD,JRBY,PID,    UNISQ,            SIMCS,        ELINENO,SEQ,TJCD,TJCDRMK,MAFG)";
                sql = sql + Environment.NewLine + "SELECT " + fBDODT + ",QFYCD,JRBY,PID," + new_unisq + "," + new_simcs + ",ELINENO,SEQ,TJCD,TJCDRMK,MAFG ";
                sql = sql + Environment.NewLine + "  FROM " + tTI2J + " ";
                sql = sql + Environment.NewLine + " WHERE " + fBDODT + " = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ =  " + p_k5 + "  ";
                sql = sql + Environment.NewLine + "   AND SIMCS =  " + p_k6 + "  ";
                sql = sql + Environment.NewLine + "   AND TJCD = 'MT014' ";
            }

        }

        private string GetNextSimcs(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, string p_k5, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int next_simcs = 0;
            string sql="";
            if (p_iofg == "2")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMCS),0)+1 AS NEXTSIMCS ";
                sql = sql + Environment.NewLine + "  FROM TI2A ";
                sql = sql + Environment.NewLine + " WHERE BDODT = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ = '" + p_k5 + "' ";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMCS),0)+1 AS NEXTSIMCS ";
                sql = sql + Environment.NewLine + "  FROM TI1A ";
                sql = sql + Environment.NewLine + " WHERE EXDATE = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD  = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY   = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID    = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ  = '" + p_k5 + "' ";
            }

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int.TryParse(reader["NEXTSIMCS"].ToString(), out next_simcs);
                    }
                    reader.Close();
                }
            }
            if (next_simcs < 1) next_simcs = 1;
            return next_simcs.ToString();
        }

        private string GetNextUnisq(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int next_unisq = 0;
            string sql = "";
            if (p_iofg == "2")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(UNISQ),0)+1 AS NEXTUNISQ ";
                sql = sql + Environment.NewLine + "  FROM TI2A ";
                sql = sql + Environment.NewLine + " WHERE BDODT = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(UNISQ),0)+1 AS NEXTUNISQ ";
                sql = sql + Environment.NewLine + "  FROM TI1A ";
                sql = sql + Environment.NewLine + " WHERE EXDATE = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD  = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY   = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID    = '" + p_k4 + "' ";
            }

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int.TryParse(reader["NEXTUNISQ"].ToString(), out next_unisq);
                    }
                    reader.Close();
                }
            }
            if (next_unisq < 1) next_unisq = 1;
            return next_unisq.ToString();
        }

        private string GetNextSimno(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, string p_k5, string p_k6, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int next_simno = 0;
            string sql = "";
            if (p_iofg == "2")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0)+1 AS NEXTSIMNO ";
                sql = sql + Environment.NewLine + "  FROM TI2A ";
                sql = sql + Environment.NewLine + " WHERE BDODT LIKE '" + p_k1.Substring(0, 6) + "%' ";
                sql = sql + Environment.NewLine + "   AND SIMFG=(SELECT X.SIMFG";
                sql = sql + Environment.NewLine + "                FROM TI2A X";
                sql = sql + Environment.NewLine + "               WHERE X.BDODT = '" + p_k1 + "'";
                sql = sql + Environment.NewLine + "                 AND X.QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.UNISQ = '" + p_k5 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.SIMCS = '" + p_k6 + "' ";
                sql = sql + Environment.NewLine + "             )";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0)+1 AS NEXTSIMNO ";
                sql = sql + Environment.NewLine + "  FROM TI1A ";
                sql = sql + Environment.NewLine + " WHERE EXDATE = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND SIMFG=(SELECT X.SIMFG";
                sql = sql + Environment.NewLine + "                FROM TI1A X";
                sql = sql + Environment.NewLine + "               WHERE X.EXDATE = '" + p_k1 + "'";
                sql = sql + Environment.NewLine + "                 AND X.QFYCD  = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.JRBY   = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.PID    = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.UNISQ  = '" + p_k5 + "' ";
                sql = sql + Environment.NewLine + "                 AND X.SIMCS  = '" + p_k6 + "' ";
                sql = sql + Environment.NewLine + "             )";
            }

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int.TryParse(reader["NEXTSIMNO"].ToString(), out next_simno);
                    }
                    reader.Close();
                }
            }
            return next_simno.ToString();
        }

        private string GetDrgfg(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, string p_k5, string p_k6, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string drgfg = "";
            if (p_iofg == "2")
            {
                string sql = "";
                sql = "";
                sql = sql + Environment.NewLine + "SELECT DRGFG ";
                sql = sql + Environment.NewLine + "  FROM TI2A ";
                sql = sql + Environment.NewLine + " WHERE BDODT = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ = '" + p_k5 + "' ";
                sql = sql + Environment.NewLine + "   AND SIMCS = '" + p_k6 + "' ";

                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
                {

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            drgfg = reader["DRGFG"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            return drgfg;
        }

        private void GetCnecnoAndSau(string p_iofg, string p_k1, string p_k2, string p_k3, string p_k4, string p_k5, string p_k6, string p_addz1, OleDbConnection p_conn, OleDbTransaction p_tran, ref string p_cnecno, ref string p_sau)
        {
            p_cnecno = "";
            p_sau = "";

            // 청구번호와 명일련 번호를 구한다.
            string sql = "";
            if (p_iofg == "2")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT DEMNO, EPRTNO ";
                sql = sql + Environment.NewLine + "  FROM TI2A ";
                sql = sql + Environment.NewLine + " WHERE BDODT = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY  = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID   = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ = '" + p_k5 + "' ";
                sql = sql + Environment.NewLine + "   AND SIMCS = '" + p_k6 + "' ";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT DEMNO, EPRTNO ";
                sql = sql + Environment.NewLine + "  FROM TI1A ";
                sql = sql + Environment.NewLine + " WHERE EXDATE = '" + p_k1 + "' ";
                sql = sql + Environment.NewLine + "   AND QFYCD  = '" + p_k2 + "' ";
                sql = sql + Environment.NewLine + "   AND JRBY   = '" + p_k3 + "' ";
                sql = sql + Environment.NewLine + "   AND PID    = '" + p_k4 + "' ";
                sql = sql + Environment.NewLine + "   AND UNISQ  = '" + p_k5 + "' ";
                sql = sql + Environment.NewLine + "   AND SIMCS  = '" + p_k6 + "' ";
            }

            string demno = "";
            string eprtno = "";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        demno = reader["DEMNO"].ToString();
                        eprtno = reader["EPRTNO"].ToString();
                    }
                    reader.Close();
                }
            }
            if (demno == "") return;

            // 접수번호를 구한다.
            if (p_k2.Substring(0, 1) == "6")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT TOP 1 X.CNECTNO ";
                sql = sql + Environment.NewLine + "  FROM TIE_N0102 X ";
                sql = sql + Environment.NewLine + " WHERE X.DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + " ORDER BY X.CNECTDD DESC ";
            }
            else if (p_k2.Substring(0, 1) == "5")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT TOP 1 X.ACCNO AS CNECTNO ";
                sql = sql + Environment.NewLine + "  FROM TIE_I010 X ";
                sql = sql + Environment.NewLine + " WHERE X.DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + " ORDER BY X.REPDT DESC ";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT TOP 1 X.CNECTNO ";
                sql = sql + Environment.NewLine + "  FROM TIE_F0102 X ";
                sql = sql + Environment.NewLine + " WHERE X.DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + " ORDER BY X.CNECTDD DESC ";
            }
            string cnecno = "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader){
                cnecno = reader["CNECTNO"].ToString();
                return false;
            });
            p_cnecno = cnecno;
            int int_eprtno = 0;
            int.TryParse(eprtno, out int_eprtno);
            // 접수증에서 없으면 심결에서 찾아본다.
            if (p_cnecno == "")
            {
                // 반송사유를 구한다.
                if (p_k2.Substring(0, 1) == "6")
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT CNECNO ";
                    sql = sql + Environment.NewLine + "  FROM TIE_N0203 ";
                    sql = sql + Environment.NewLine + " WHERE DEMNO='" + demno + "' ";
                    sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                    sql = sql + Environment.NewLine + "   AND LNO=0 ";
                }
                else if (p_k2.Substring(0, 1) == "2")
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT CNECNO ";
                    sql = sql + Environment.NewLine + "  FROM TIE_F0203_062 ";
                    sql = sql + Environment.NewLine + " WHERE DEMNO='" + demno + "' ";
                    sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                    sql = sql + Environment.NewLine + "   AND LNO=0 ";
                }
                else
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT CNECNO ";
                    sql = sql + Environment.NewLine + "  FROM TIE_F0603_062 ";
                    sql = sql + Environment.NewLine + " WHERE DEMNO='" + demno + "' ";
                    sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                    sql = sql + Environment.NewLine + "   AND LNO=0 ";
                }
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
                {
                    cnecno = reader["CNECNO"].ToString();
                    return false;
                });
                p_cnecno = cnecno;
            }
            if (p_cnecno == "") return;
            if (p_addz1 != "1") return; // 보완청구가 아니면 사유를 구하지 않는다.
            if (p_k2.Substring(0, 1) == "5") return; // 산재는 사유를 구하지 않는다.
            if (eprtno == "") return; // 명일련번호가 없으면 중지.

            // 반송사유를 구한다.
            if (p_k2.Substring(0, 1) == "6"){
                sql = "";
                sql = sql + Environment.NewLine + "SELECT JJRMK ";
                sql = sql + Environment.NewLine + "  FROM TIE_N0203 ";
                sql = sql + Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "' ";
                sql = sql + Environment.NewLine + "   AND DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                sql = sql + Environment.NewLine + "   AND LNO=0 ";
            }
            else if (p_k2.Substring(0, 1) == "2")
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT JJRMK ";
                sql = sql + Environment.NewLine + "  FROM TIE_F0203_062 ";
                sql = sql + Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "' ";
                sql = sql + Environment.NewLine + "   AND DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                sql = sql + Environment.NewLine + "   AND LNO=0 ";
            }
            else
            {
                sql = "";
                sql = sql + Environment.NewLine + "SELECT JJRMK ";
                sql = sql + Environment.NewLine + "  FROM TIE_F0603_062 ";
                sql = sql + Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "' ";
                sql = sql + Environment.NewLine + "   AND DEMNO='" + demno + "' ";
                sql = sql + Environment.NewLine + "   AND CONVERT(NUMERIC,EPRTNO)=" + int_eprtno.ToString() + " ";
                sql = sql + Environment.NewLine + "   AND LNO=0 ";
            }
            string sau = "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                sau = reader["JJRMK"].ToString();
                return false;
            });
            p_sau = sau;
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 선택된 열의 배경색을 변경한다.
            // 셀이 병합되면 여러 셀을 선택하는 기능이 안됨???
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.IsCellSelected(e.RowHandle, e.Column))
            {
                e.Appearance.BackColor = view.PaintAppearance.FocusedRow.BackColor;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == MetroLib.Win32API.WM_COPYDATA)
            {
                MetroLib.Win32API.COPYDATASTRUCT lParam1 = (MetroLib.Win32API.COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(MetroLib.Win32API.COPYDATASTRUCT));
                MetroLib.Win32API.COPYDATASTRUCT lParam2 = new MetroLib.Win32API.COPYDATASTRUCT();
                lParam2 = (MetroLib.Win32API.COPYDATASTRUCT)m.GetLParam(lParam2.GetType());

                //MessageBox.Show(lParam1.lpData);
                //MessageBox.Show(lParam2.lpData);

                // 사용자 메시지이면
                String addpara = "";
                ParseArg(lParam1.lpData, ref addpara);
                if (addpara != "")
                {
                    RunAuto(addpara);
                }
            }
        }

        private static void ParseArg(String arg, ref String addpara)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("ADDPARA".Equals(val[0].ToUpper())) addpara = val[1];
            }
        }

    }

}

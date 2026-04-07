using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7006E
{
    public partial class ADD7006E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;
        private String m_Demno;

        private bool IsFirst;
        private string m_pgm_step = ""; // 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD7006E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";
            m_Demno = "";

            this.CreatePopupMenu();
        }

        public ADD7006E(String user, String pwd, String prjcd, String demno)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Demno = demno;
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

        private string GetHospId()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='2'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["FLD1QTY"].ToString();
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

        private void CreatePopupMenu()
        {
            //
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("전송 제외", new EventHandler(mnuRemoveData_Click));
            //cm.MenuItems.Add("전송 제외 취소", new EventHandler(mnuCancelRemoveData_Click));
            //grdMain.ContextMenu = cm;
        }

        private void ADD7006E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD7006E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            ReadConfig();

            try
            {
                grdMainView.Columns["ERP001_STATUS_NM"].Visible = false; // 병리검사기록자료 안보이게
                grdMainView.Columns["RRC001_STATUS_NM"].Visible = false; // 의원급진료기록자료 안보이게
                grdMainView.Columns["RPP001_STATUS_NM"].Visible = false; // 방사선치료기록자료 안보이게
            }
            catch (Exception ex)
            {
            }

            txtDemno.Text = m_Demno;
            txtHosid.Text = GetHospId();

            btnQuery.PerformClick();
        }

        private void ReadConfig()
        {
            //try
            //{
            //    string strConn = MetroLib.DBHelper.GetConnectionString();
            //    using (OleDbConnection conn = new OleDbConnection(strConn))
            //    {
            //        conn.Open();
            //        string sql = "";
            //        MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
            //        {
            //            return MetroLib.SqlHelper.BREAK;
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_pgm_step = "";
                this.Query();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(m_pgm_step + " : " + ex.Message);
            }
        }

        private void Query()
        {
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string demno = txtDemno.Text.ToString();

            string iofg = "";
            string cnectdd = "";
            string dcount = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                iofg = GetIofg(demno, conn);
                txtCnecno.Text = GetCnecno(demno, conn, ref cnectdd, ref dcount); // 접수번호를 가져온다.
                txtCnectdd.Text = cnectdd; // 접수일자
                if (txtCnecno.Text.ToString() == "")
                {
                    txtCnecno.Text = "0000000";
                    txtBillSno.Text = "0"; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
                    txtCnectdd.Text = txtDemno.Text.ToString(); // 접수년도만 사용할 것이므로 청구번호로 만든다.
                }
                else
                {
                    if (dcount == "")
                    {
                        txtBillSno.Text = "1";
                    }
                    else
                    {
                        txtBillSno.Text = dcount;
                    }
                }

                string tTI1A = "TI1A";
                string fEXDATE = "EXDATE";
                if (iofg == "2")
                {
                    tTI1A = "TI2A";
                    fEXDATE = "BDODT";
                }

                string sql = "";
                sql += Environment.NewLine + "SELECT A.DEMNO,A.EPRTNO,A.PID,A.PNM,DBO.MFN_PIECE(A.JRKWA,'$',3) AS DPTCD,A.STEDT,A.BDEDT,A.PSEX,A.RESID,A.QFYCD,A.EXAMC";
                sql += Environment.NewLine + "     , A." + fEXDATE + "+','+A.QFYCD+','+A.JRBY+','+A.PID+','+CONVERT(VARCHAR,A.UNISQ)+','+CONVERT(VARCHAR,A.SIMCS) AS A_KEY";
                sql += Environment.NewLine + "  FROM " + tTI1A + " A";
                sql += Environment.NewLine + " WHERE A.DEMNO='" + demno + "'";
                sql += Environment.NewLine + " ORDER BY A.DEMNO,A.EPRTNO";


                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    CData data = new CData();
                    data.Clear();

                    // TI2A
                    data.DEMNO = row["DEMNO"].ToString();
                    data.EPRTNO = row["EPRTNO"].ToString();
                    data.PID = row["PID"].ToString();
                    data.PNM = row["PNM"].ToString();
                    data.DPTCD = row["DPTCD"].ToString();
                    data.STEDT = row["STEDT"].ToString();
                    data.BDEDT = row["BDEDT"].ToString();
                    data.PSEX = row["PSEX"].ToString();
                    data.RESID = row["RESID"].ToString();
                    data.QFYCD = row["QFYCD"].ToString();
                    data.A_KEY = row["A_KEY"].ToString();
                    data.EXAMC = row["EXAMC"].ToString();
                    data.IOFG = iofg;

                    list.Add(data);

                    return true;
                });


                RefreshGridMain();
            }
        }

        private string GetIofg(string p_demno, OleDbConnection p_conn)
        {
            string iofg = "";
            string sql = "";
            // 청구번호가 입원인지 검사
            sql = "SELECT COUNT(*) AS CNT FROM TI2A WHERE DEMNO='" + p_demno + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int cnt = 0;
                int.TryParse(reader["CNT"].ToString(), out cnt);
                if (cnt > 0) iofg = "2";
                return false;
            });
            if (iofg != "") return iofg;

            // 청구번호가 외래인지 검사
            sql = "SELECT COUNT(*) AS CNT FROM TI1A WHERE DEMNO='" + p_demno + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int cnt = 0;
                int.TryParse(reader["CNT"].ToString(), out cnt);
                if (cnt > 0) iofg = "1";
                return false;
            });
            if (iofg != "") return iofg;

            // 입원도 아니고, 외래도 아님. 없는 청구번호임.
            return iofg;
        }

        private string GetCnecno(string p_demno, OleDbConnection p_conn, ref string p_cnectdd, ref string p_dcount)
        {
            //string iofg = "";
            string qfycd = "";
            string addz1 = "";
            string addz2 = "";
            {
                int cnt = 0;
                // 청구번호가 입원인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                string sql = "SELECT * FROM TI2A WHERE DEMNO='" + p_demno + "'";
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    //iofg = "2";
                    qfycd = reader["QFYCD"].ToString();
                    addz1 = reader["ADDZ1"].ToString();
                    addz2 = reader["ADDZ2"].ToString();
                    return false;
                });
                if (cnt < 1)
                {
                    // 청구번호가 외래인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                    sql = "SELECT * FROM TI1A WHERE DEMNO='" + p_demno + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                    {
                        cnt++;
                        //iofg = "1";
                        qfycd = reader["QFYCD"].ToString();
                        addz1 = reader["ADDZ1"].ToString();
                        addz2 = reader["ADDZ2"].ToString();
                        return false;
                    });
                }

            }

            string cnecno = "";
            string cnectdd = "";
            string dcount = "";

            // 접수증을 읽는다.
            {
                String sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0102 A";
                sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + p_demno + "'";
                sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnecno = reader["CNECTNO"].ToString(); // 접수번호
                    cnectdd = reader["CNECTDD"].ToString(); // 접수일자
                    return false;
                });
            }
            p_cnectdd = cnectdd;

            // 보완청구이면
            if ("1".Equals(addz1))
            {
                String sql = "";
                if (qfycd.StartsWith("3"))
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    dcount = reader["DCOUNT"].ToString(); // 청구서 일련번호
                    return false;
                });
            }
            p_dcount = dcount.Replace(" ", "");

            return cnecno;
        }

        private void ShowProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormCaption(caption);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        private void ShowProgressForm(String description)
        {
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

        private void btnMake_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_pgm_step = "";
                this.Make();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(m_pgm_step + " : " + ex.Message);
            }
        }

        private void Make()
        {
            int rowHandle = 0;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                List<CData> list = (List<CData>)grdMain.DataSource;
                foreach (CData data in list)
                {
                    this.ShowProgressForm("전송자료 조회 중입니다.(" + (rowHandle + 1 + "/" + list.Count) + ")");

                    grdMainView.FocusedRowHandle = rowHandle;
                    RefreshGridMain();

                    // 퇴원요약지(RID001)
                    m_pgm_step = "퇴원요약지(RID001)";
                    CMakeRID001 makeRID001 = new CMakeRID001();
                    data.RID001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRID001.Make(data, conn);
                    if (data.RID001_LIST.Count > 0) data.RID001_RESULT.STATUS = "B";
                    else data.RID001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 진단검사결과지(ERD001)
                    m_pgm_step = "진단검사결과지(ERD001)";
                    CMakeERD001 makeERD001 = new CMakeERD001();
                    data.ERD001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeERD001.Make(data, conn);
                    if (data.ERD001_LIST.Count > 0) data.ERD001_RESULT.STATUS = "B";
                    else data.ERD001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 영상검사결과지(ERR001)
                    m_pgm_step = "영상검사결과지(ERR001)";
                    CMakeERR001 makeERR001 = new CMakeERR001();
                    data.ERR001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeERR001.Make(data, conn);
                    if (data.ERR001_LIST.Count > 0) data.ERR001_RESULT.STATUS = "B";
                    else data.ERR001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 병리검사결과지(ERP001)(작성안함)
                    m_pgm_step = "병리검사결과지(ERP001)";
                    RefreshGridMain();

                    // 수술기록자료(RSS001)
                    m_pgm_step = "수술기록자료(RSS001)";
                    CMakeRSS001 makeRSS001 = new CMakeRSS001();
                    data.RSS001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRSS001.Make(data, conn);
                    if (data.RSS001_LIST.Count > 0) data.RSS001_RESULT.STATUS = "B";
                    else data.RSS001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 응급기록자료(REE001)
                    m_pgm_step = "응급기록자료(REE001)";
                    CMakeREE001 makeREE001 = new CMakeREE001();
                    data.REE001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeREE001.Make(data, conn);
                    if (data.REE001_LIST.Count > 0) data.REE001_RESULT.STATUS = "B";
                    else data.REE001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 입원초진기록자료(RII001)
                    m_pgm_step = "입원초진기록자료(RII001)";
                    CMakeRII001 makeRII001 = new CMakeRII001();
                    data.RII001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRII001.Make(data, conn);
                    if (data.RII001_LIST.Count > 0) data.RII001_RESULT.STATUS = "B";
                    else data.RII001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 입원경과기록자료(RIP001)
                    m_pgm_step = "입원경과기록자료(RIP001)";
                    CMakeRIP001 makeRIP001 = new CMakeRIP001();
                    data.RIP001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRIP001.Make(data, conn);
                    if (data.RIP001_LIST.Count > 0) data.RIP001_RESULT.STATUS = "B";
                    else data.RIP001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 외래초진기록자료(ROO001)
                    m_pgm_step = "외래초진기록자료(ROO001)";
                    CMakeROO001 makeROO001 = new CMakeROO001();
                    data.ROO001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeROO001.Make(data, conn);
                    if (data.ROO001_LIST.Count > 0) data.ROO001_RESULT.STATUS = "B";
                    else data.ROO001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 외래경과기록자료(ROP001)
                    m_pgm_step = "외래경과기록자료(ROP001)";
                    CMakeROP001 makeROP001 = new CMakeROP001();
                    data.ROP001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeROP001.Make(data, conn);
                    if (data.ROP001_LIST.Count > 0) data.ROP001_RESULT.STATUS = "B";
                    else data.ROP001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 중환자실기록자료(RWI001)
                    m_pgm_step = "중환자실기록자료(RWI001)";
                    CMakeRWI001 makeRWI001 = new CMakeRWI001();
                    data.RWI001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRWI001.Make(data, conn);
                    if (data.RWI001_LIST.Count > 0) data.RWI001_RESULT.STATUS = "B";
                    else data.RWI001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 협의진료기록자료(RCC001)
                    m_pgm_step = "협의진료기록자료(RCC001)";
                    CMakeRCC001 makeRCC001 = new CMakeRCC001();
                    data.RCC001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRCC001.Make(data, conn);
                    if (data.RCC001_LIST.Count > 0) data.RCC001_RESULT.STATUS = "B";
                    else data.RCC001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 마취기록자료(RAA001)
                    m_pgm_step = "마취기록자료(RAA001)";
                    CMakeRAA001 makeRAA001 = new CMakeRAA001();
                    data.RAA001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRAA001.Make(data, conn);
                    if (data.RAA001_LIST.Count > 0) data.RAA001_RESULT.STATUS = "B";
                    else data.RAA001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 회복기록자료(RAR001)
                    m_pgm_step = "회복기록자료(RAR001)";
                    CMakeRAR001 makeRAR001 = new CMakeRAR001();
                    data.RAR001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRAR001.Make(data, conn);
                    if (data.RAR001_LIST.Count > 0) data.RAR001_RESULT.STATUS = "B";
                    else data.RAR001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 간호정보조사자료(RNP001)
                    m_pgm_step = "간호정보조사자료(RNP001)";
                    CMake001 makeRNP001;
                    makeRNP001 = new CMakeRNP001();
                    data.RNP001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRNP001.Make(data, conn);
                    if (data.RNP001_LIST.Count > 0) data.RNP001_RESULT.STATUS = "B";
                    else data.RNP001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 기타간호기록자료(RNO001)
                    m_pgm_step = "기타간호기록자료(RNO001)";
                    CMakeRNO001 makeRNO001 = new CMakeRNO001();
                    data.RNO001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRNO001.Make(data, conn);
                    if (data.RNO001_LIST.Count > 0) data.RNO001_RESULT.STATUS = "B";
                    else data.RNO001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 수술간호기록자료(RNS001)
                    m_pgm_step = "수술간호기록자료(RNS001)";
                    CMakeRNS001 makeRNS001 = new CMakeRNS001();
                    data.RNS001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRNS001.Make(data, conn);
                    if (data.RNS001_LIST.Count > 0) data.RNS001_RESULT.STATUS = "B";
                    else data.RNS001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 응급간호기록자료(RNE001)
                    m_pgm_step = "응급간호기록자료(RNE001)";
                    CMakeRNE001 makeRNE001 = new CMakeRNE001();
                    data.RNE001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRNE001.Make(data, conn);
                    if (data.RNE001_LIST.Count > 0) data.RNE001_RESULT.STATUS = "B";
                    else data.RNE001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 의사지시기록자료(RDD001)
                    m_pgm_step = "의사지시기록자료(RDD001)";
                    CMakeRDD001 makeRDD001 = new CMakeRDD001();
                    data.RDD001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRDD001.Make(data, conn);
                    if (data.RDD001_LIST.Count > 0) data.RDD001_RESULT.STATUS = "B";
                    else data.RDD001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 임상관찰기록자료(RWW001)
                    m_pgm_step = "임상관찰기록자료(RWW001)";
                    CMakeRWW001 makeRWW001 = new CMakeRWW001();
                    data.RWW001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRWW001.Make(data, conn);
                    if (data.RWW001_LIST.Count > 0) data.RWW001_RESULT.STATUS = "B";
                    else data.RWW001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 투석기록자료(RNH001)
                    m_pgm_step = "투석기록자료(RNH001)";
                    CMakeRNH001 makeRNH001 = new CMakeRNH001();
                    data.RNH001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRNH001.Make(data, conn);
                    if (data.RNH001_LIST.Count > 0) data.RNH001_RESULT.STATUS = "B";
                    else data.RNH001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 전입기록자료(RIY001)
                    m_pgm_step = "전입기록자료(RIY001)";
                    CMakeRIY001 makeRIY001 = new CMakeRIY001();
                    data.RIY001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRIY001.Make(data, conn);
                    if (data.RIY001_LIST.Count > 0) data.RIY001_RESULT.STATUS = "B";
                    else data.RIY001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 전출기록자료(RIZ001)
                    m_pgm_step = "전출기록자료(RIZ001)";
                    CMakeRIZ001 makeRIZ001 = new CMakeRIZ001();
                    data.RIZ001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRIZ001.Make(data, conn);
                    if (data.RIZ001_LIST.Count > 0) data.RIZ001_RESULT.STATUS = "B";
                    else data.RIZ001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 시술기록자료(RTT001)
                    m_pgm_step = "시술기록자료(RTT001)";
                    CMakeRTT001 makeRTT001 = new CMakeRTT001();
                    data.RTT001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRTT001.Make(data, conn);
                    if (data.RTT001_LIST.Count > 0) data.RTT001_RESULT.STATUS = "B";
                    else data.RTT001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 투약기록자료(RMM001)
                    m_pgm_step = "투약기록자료(RMM001)";
                    CMakeRMM001 makeRMM001 = new CMakeRMM001();
                    data.RMM001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRMM001.Make(data, conn);
                    if (data.RMM001_LIST.Count > 0) data.RMM001_RESULT.STATUS = "B";
                    else data.RMM001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 신생아중환자실기록자료(RWN001)
                    m_pgm_step = "신생아중환자실기록자료(RWN001)";
                    CMakeRWN001 makeRWN001 = new CMakeRWN001();
                    data.RWN001_RESULT.STATUS = "A";
                    RefreshGridMain();
                    makeRWN001.Make(data, conn);
                    if (data.RWN001_LIST.Count > 0) data.RWN001_RESULT.STATUS = "B";
                    else data.RWN001_RESULT.STATUS = "";
                    RefreshGridMain();

                    // 의원급진료기록자료(RCC001)(작성안함)
                    m_pgm_step = "의원급진료기록자료(RCC001)";
                    RefreshGridMain();

                    // 방사선치료기록자료(RCC001)(작성안함)
                    m_pgm_step = "방사선치료기록자료(RCC001)";
                    RefreshGridMain();

                    rowHandle++;
                }
            }

            grdMainView.FocusedRowHandle = 0;
            RefreshGridMain();

        }

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                if (info.Column.FieldName == "RID001_STATUS_NM") CallShowForm("RID001"); // 퇴원요약자료
                if (info.Column.FieldName == "ERD001_STATUS_NM") CallShowForm("ERD001"); // 진단검사결과지
                if (info.Column.FieldName == "ERR001_STATUS_NM") CallShowForm("ERR001"); // 영상검사결과지
                                                                                         // 병리검사결과지
                if (info.Column.FieldName == "RSS001_STATUS_NM") CallShowForm("RSS001"); // 수술기록자료
                if (info.Column.FieldName == "REE001_STATUS_NM") CallShowForm("REE001"); // 응급기록자료
                if (info.Column.FieldName == "RII001_STATUS_NM") CallShowForm("RII001"); // 입원초진기록자료
                if (info.Column.FieldName == "RIP001_STATUS_NM") CallShowForm("RIP001"); // 입원경과기록자료
                if (info.Column.FieldName == "ROO001_STATUS_NM") CallShowForm("ROO001"); // 외래초진기록자료
                if (info.Column.FieldName == "ROP001_STATUS_NM") CallShowForm("ROP001"); // 외래경과기록자료
                if (info.Column.FieldName == "RWI001_STATUS_NM") CallShowForm("RWI001"); // 중환자실기록자료
                if (info.Column.FieldName == "RCC001_STATUS_NM") CallShowForm("RCC001"); // 협의진료기록자료
                if (info.Column.FieldName == "RAA001_STATUS_NM") CallShowForm("RAA001"); // 마취기록자료
                if (info.Column.FieldName == "RAR001_STATUS_NM") CallShowForm("RAR001"); // 회복기록자료
                if (info.Column.FieldName == "RNP001_STATUS_NM") CallShowForm("RNP001"); // 간호정보조사자료
                if (info.Column.FieldName == "RNO001_STATUS_NM") CallShowForm("RNO001"); // 기타간호기록자료
                if (info.Column.FieldName == "RNS001_STATUS_NM") CallShowForm("RNS001"); // 수술간호기록자료
                if (info.Column.FieldName == "RNS001_STATUS_NM") CallShowForm("RNE001"); // 응급간호기록자료
                if (info.Column.FieldName == "RDD001_STATUS_NM") CallShowForm("RDD001"); // 의사지시기록자료
                if (info.Column.FieldName == "RWW001_STATUS_NM") CallShowForm("RWW001"); // 임상관찰기록자료
                if (info.Column.FieldName == "RNH001_STATUS_NM") CallShowForm("RNH001"); // 투석기록자료
                if (info.Column.FieldName == "RIY001_STATUS_NM") CallShowForm("RIY001"); // 전입기록자료
                if (info.Column.FieldName == "RIZ001_STATUS_NM") CallShowForm("RIZ001"); // 전출기록자료
                if (info.Column.FieldName == "RTT001_STATUS_NM") CallShowForm("RTT001"); // 시술기록자료
                if (info.Column.FieldName == "RMM001_STATUS_NM") CallShowForm("RMM001"); // 투약기록자료
                if (info.Column.FieldName == "RWN001_STATUS_NM") CallShowForm("RWN001"); // 신생아중환자실기록자료
                                                                                         // 의원급진료기록자료
                                                                                         // 방사선치료기록자료
            }        
        }

        private void CallShowForm(string kind)
        {
            ADD7006E_DISP f = new ADD7006E_DISP(kind);
            f.RequestData += new EventHandler(CurrData);
            f.RequestPrevPtnt += new EventHandler(PrevPtnt);
            f.RequestNextPtnt += new EventHandler(NextPtnt);
            f.RequestPrevForm += new EventHandler(PrevForm);
            f.RequestNextForm += new EventHandler(NextForm);
            f.ShowDialog(this);
        }

        private void CurrData(object sender1, EventArgs e1)
        {
            MyEventArgs e2 = e1 as MyEventArgs;
            e2.dataOk = true;
            e2.data = GetData();
        }

        private void PrevPtnt(object sender1, EventArgs e1)
        {
            if (grdMainView.FocusedRowHandle > 0)
            {
                grdMainView.FocusedRowHandle--;
                MyEventArgs e2 = e1 as MyEventArgs;
                e2.dataOk = true;
                e2.data = GetData();
                e2.kind = grdMainView.FocusedColumn.FieldName.Substring(0, 6);
            }
            else
            {
                MessageBox.Show("처음 환자입니다.");
            }
        }

        private void NextPtnt(object sender1, EventArgs e1)
        {
            if (grdMainView.FocusedRowHandle < grdMainView.RowCount - 1)
            {
                grdMainView.FocusedRowHandle++;
                MyEventArgs e2 = e1 as MyEventArgs;
                e2.dataOk = true;
                e2.data = GetData();
                e2.kind = grdMainView.FocusedColumn.FieldName.Substring(0, 6);
            }
            else
            {
                MessageBox.Show("마지막 환자입니다.");
            }
        }

        private void PrevForm(object sender1, EventArgs e1)
        {
            if (grdMainView.FocusedColumn.FieldName == "RWN001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RMM001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RMM001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RTT001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RTT001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIZ001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIZ001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIY001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIY001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNH001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNH001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RWW001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RWW001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RDD001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RDD001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNE001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNE001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNS001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNS001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNO001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNO001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RAR001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RAR001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RAA001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RAA001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RCC001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RCC001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RWI001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RWI001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ROP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ROP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ROO001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ROO001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RII001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RII001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["REE001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "REE001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RSS001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RSS001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ERR001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ERR001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ERD001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ERD001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RID001_STATUS_NM"];
            else
            {
                MessageBox.Show("처음 양식입니다.");
                return;
            }
            

            MyEventArgs e2 = e1 as MyEventArgs;
            e2.dataOk = true;
            e2.data = GetData();
            e2.kind = grdMainView.FocusedColumn.FieldName.Substring(0, 6);
        }

        private void NextForm(object sender1, EventArgs e1)
        {
            if (grdMainView.FocusedColumn.FieldName == "RID001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ERD001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ERD001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ERR001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ERR001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RSS001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RSS001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["REE001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "REE001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RII001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RII001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ROO001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ROO001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["ROP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "ROP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RWI001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RWI001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RCC001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RCC001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RAA001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RAA001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RAR001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RAR001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNP001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNP001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNO001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNO001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNS001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNS001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNE001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNE001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RDD001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RDD001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RWW001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RWW001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RNH001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RNH001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIY001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIY001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RIZ001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RIZ001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RTT001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RTT001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RMM001_STATUS_NM"];
            else if (grdMainView.FocusedColumn.FieldName == "RMM001_STATUS_NM") grdMainView.FocusedColumn = grdMainView.Columns["RWN001_STATUS_NM"];
            else
            {
                MessageBox.Show("마지막 양식입니다.");
                return;
            }


            MyEventArgs e2 = e1 as MyEventArgs;
            e2.dataOk = true;
            e2.data = GetData();
            e2.kind = grdMainView.FocusedColumn.FieldName.Substring(0, 6);
        }

        private CData GetData()
        {
            List<CData> list = (List<CData>)grdMain.DataSource;
            CData data = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == grdMainView.FocusedRowHandle)
                {
                    data = list[i];
                    break;
                }
            }
            return data;

        }

        private void btnTmpSend_Click(object sender, EventArgs e)
        {
            if (txtDemno.Text.ToString() == "") return;
            if (MessageBox.Show("임시전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_pgm_step = "";
                this.Send(true);
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(m_pgm_step + " : " + ex.Message);
            }
        }

        private void Send(bool isTmp)
        {
            string hosid = txtHosid.Text.ToString();
            string demno = txtDemno.Text.ToString();
            string cnecno = txtCnecno.Text.ToString();
            string cnectdd = txtCnectdd.Text.ToString();
            string billsno = txtBillSno.Text.ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                int rowHandle = 0;
                List<CData> list = (List<CData>)grdMainView.DataSource;
                foreach (CData data in list)
                {
                    grdMainView.FocusedRowHandle = rowHandle;

                    // 퇴원요약자료
                    m_pgm_step = "퇴원요약자료";
                    CHiraRID001 hiraRID001 = new CHiraRID001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RID001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRID001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 잔단검사결과지
                    m_pgm_step = "잔단검사결과지";
                    CHiraERD001 hiraERD001 = new CHiraERD001(hosid, demno, cnecno, cnectdd, billsno);
                    data.ERD001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraERD001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 영상검사결과지
                    m_pgm_step = "영상검사결과지";
                    CHiraERR001 hiraERR001 = new CHiraERR001(hosid, demno, cnecno, cnectdd, billsno);
                    data.ERR001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraERR001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 병리검사결과지(작성안함)
                    m_pgm_step = "병리검사결과지";

                    // 수술기록자료
                    m_pgm_step = "수술기록자료";
                    CHiraRSS001 hiraRSS001 = new CHiraRSS001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RSS001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRSS001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 응급기록자료
                    m_pgm_step = "응급기록자료";
                    CHiraREE001 hiraREE001 = new CHiraREE001(hosid, demno, cnecno, cnectdd, billsno);
                    data.REE001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraREE001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 입원초진기록자료
                    m_pgm_step = "입원초진기록자료";
                    CHiraRII001 hiraRII001 = new CHiraRII001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RII001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRII001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 입원경과기록자료
                    m_pgm_step = "입원경과기록자료";
                    CHiraRIP001 hiraRIP001 = new CHiraRIP001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RIP001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRIP001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 외래초진기록자료
                    m_pgm_step = "외래초진기록자료";
                    CHiraROO001 hiraROO001 = new CHiraROO001(hosid, demno, cnecno, cnectdd, billsno);
                    data.ROO001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraROO001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 외래경과기록자료
                    m_pgm_step = "외래경과기록자료";
                    CHiraROP001 hiraROP001 = new CHiraROP001(hosid, demno, cnecno, cnectdd, billsno);
                    data.ROP001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraROP001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 중환자실기록자료
                    m_pgm_step = "중환자실기록자료";
                    CHiraRWI001 hiraRWI001 = new CHiraRWI001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RWI001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRWI001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 협의진료기록자료
                    m_pgm_step = "협의진료기록자료";
                    CHiraRCC001 hiraRCC001 = new CHiraRCC001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RCC001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRCC001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 마취기록자료
                    m_pgm_step = "마취기록자료";
                    CHiraRAA001 hiraRAA001 = new CHiraRAA001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RAA001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRAA001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 회복기록자료
                    m_pgm_step = "회복기록자료";

                    // 간호정보조사자료
                    m_pgm_step = "간호정보조사자료";
                    CHiraRNP001 hiraRNP001 = new CHiraRNP001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RNP001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRNP001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 기타간호기록자료
                    m_pgm_step = "기타간호기록자료";
                    CHiraRNO001 hiraRNO001 = new CHiraRNO001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RNO001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRNO001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 수술간호기록자료
                    m_pgm_step = "수술간호기록자료";
                    CHiraRNS001 hiraRNS001 = new CHiraRNS001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RNS001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRNS001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 응급간호기록자료
                    m_pgm_step = "응급간호기록자료";

                    // 의사지시기록자료
                    m_pgm_step = "의사지시기록자료";
                    CHiraRDD001 hiraRDD001 = new CHiraRDD001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RDD001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRDD001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 임상관찰기록자료
                    m_pgm_step = "임상관찰기록자료";
                    CHiraRWW001 hiraRWW001 = new CHiraRWW001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RWW001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRWW001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 투석기록자료
                    m_pgm_step = "투석기록자료";
                    CHiraRNH001 hiraRNH001 = new CHiraRNH001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RNH001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRNH001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 전입기록자료
                    m_pgm_step = "전입기록자료";
                    CHiraRIY001 hiraRIY001 = new CHiraRIY001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RIY001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRIY001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 전출기록자료
                    m_pgm_step = "전출기록자료";
                    CHiraRIZ001 hiraRIZ001 = new CHiraRIZ001(hosid, demno, cnecno, cnectdd, billsno);
                    data.RIZ001_RESULT.STATUS = "N"; // 전송중
                    RefreshGridMain();
                    hiraRIZ001.Send(data, isTmp, sysdt, systm, conn);
                    RefreshGridMain();

                    // 시술기록자료
                    m_pgm_step = "시술기록자료";

                    // 투약기록자료
                    m_pgm_step = "투약기록자료";

                    // 신생아중환자실기록자료
                    m_pgm_step = "신생아중환자실기록자료";

                    // 의원급진료기록자료

                    // 방사선치료기록자료

                }
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

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void grdMain_Click(object sender, EventArgs e)
        {

        }

    }
}

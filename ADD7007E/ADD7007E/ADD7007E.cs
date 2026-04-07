using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7007E
{
    public partial class ADD7007E : Form
    {
        class SUPL_TYPE
        {
            public string TYPE_NM { get; set; }
            public string TYPE_CD { get; set; }
        }

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;

        private bool IsFirst;
        private string m_pgm_step = ""; // 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD7007E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";

            this.CreatePopupMenu();
        }

        public ADD7007E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
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
                        return MetroLib.SqlHelper.BREAK;
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
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 몇 가지 설정을 읽어놓자.
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD2QTY,FLD3QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='242'";
                    MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                    {
                        CConfig.boho1 = row["FLD2QTY"].ToString();
                        CConfig.boho2 = row["FLD3QTY"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });
                    if (CConfig.boho1 == "") CConfig.boho1 = "33"; // 보호1종 추가
                    if (CConfig.boho2 == "") CConfig.boho2 = "34"; // 보호2종 추가

                    // 처방 HEADER와 BODY를 JOIN하는 방법
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='216'";
                    MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                    {
                        CConfig.BodyNewFg = row["FLD2QTY"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    //// 항생제 서식
                    //sql = "";
                    //sql += Environment.NewLine + "SELECT FLD3QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='EFormASM' AND MST3CD='ASM010'";
                    //MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                    //{
                    //    CConfig.asm010_fld3qty = row["FLD3QTY"].ToString();
                    //    return MetroLib.SqlHelper.BREAK;
                    //});

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

        private void ADD7007E_Load(object sender, EventArgs e)
        {
            IsFirst = true;

            grdASM010.Dock = DockStyle.Fill; // 예방학적 항생제
            grdASM035.Dock = DockStyle.Fill; // 마취
        }

        private void ADD7007E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtHosid.Text = GetHospId();

            MakeListGrid();

        }

        private void MakeListGrid()
        {
            List<SUPL_TYPE> list = new List<SUPL_TYPE>();
            grdList.DataSource = null;
            grdList.DataSource = list;
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TI88";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='EFormASM'";
                sql += Environment.NewLine + "   AND ISNULL(FLD2QTY,'')='1'";
                sql += Environment.NewLine + " ORDER BY CDNM";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    list.Add(new SUPL_TYPE { TYPE_NM = row["CDNM"].ToString().Trim(), TYPE_CD = row["MST3CD"].ToString().Trim() });

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }
            RefreshGrid();

            /*
            list.Add(new SUPL_TYPE { TYPE_NM = "관상동맥우회술", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "급성기뇌졸증", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "마취", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "수술의예방학적항생제사용", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "수혈", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "신생아중환자실", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "영상검사", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "의료급여정신과", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "정신건강입원영역", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "중환자실", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "폐렴", TYPE_CD = "" });
            list.Add(new SUPL_TYPE { TYPE_NM = "혈액투석", TYPE_CD = "" });
            */
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string typeName = txtTypeName.Text.ToString().Trim();
            string frdt = txtFrdt.Text.ToString().Trim();
            string todt = txtTodt.Text.ToString().Trim();

            if (typeName == "")
            {
                MessageBox.Show("서식명을 선택하세요.");
                return;
            }
            if (frdt == "")
            {
                MessageBox.Show("시작일을 입력하세요.");
                return;
            }
            if (todt == "")
            {
                MessageBox.Show("종료일을 입력하세요.");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                // 조회된 자료가 있는지 알아보자.
                if (typeName == "수술의예방적항생제사용")
                {
                    if (grdASM010View.DataRowCount < 1) MessageBox.Show("대상자가 없습니다. [대상자 올리기]를 먼저 수행하세요.");
                }
                else if (typeName == "마취")
                {
                    if (grdASM035View.DataRowCount < 1) MessageBox.Show("대상자가 없습니다. [대상자 올리기]를 먼저 수행하세요.");
                }
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + Environment.NewLine + m_pgm_step);
            }
        }

        private string GetTypename()
        {
            return txtTypeName.Text.ToString();
        }

        private void Query()
        {
            string typeName = GetTypename();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();

                    string sysdt = MetroLib.Util.GetSysDate(conn);
                    string systm = MetroLib.Util.GetSysTime(conn);

                    switch (typeName)
                    {
                        case "관상동맥우회술":
                            Query_ASM002(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM002(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;
                        case "급성기뇌졸중":
                            Query_ASM003(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM003(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "마취":
                            Query_ASM035(conn);
                            //tran = conn.BeginTransaction();
                            //Make_ASM035(conn, tran, sysdt, systm);
                            //tran.Commit();
                            break;

                        case "수술의예방적항생제사용":
                            Query_ASM010(conn);
                            //tran = conn.BeginTransaction();
                            //Make_ASM010(conn, tran, sysdt, systm);
                            //tran.Commit();
                            break;

                        case "수혈":
                            Query_ASM037(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM037(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "신생아중환자실":
                            Query_ASM033(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM033(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "영상검사":
                            Query_ASM049(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM049(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "의료급여정신과":
                            Query_ASM014(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM014(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "정신건강입원영역":
                            Query_ASM036(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM036(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "중환자실":
                            Query_ASM024(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM024(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "폐렴":
                            Query_ASM023(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM023(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        case "혈액투석":
                            Query_ASM008(conn);
                            tran = conn.BeginTransaction();
                            Make_ASM008(conn, tran, sysdt, systm);
                            tran.Commit();
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    //throw ex;
                    MessageBox.Show(ex.Message);
                }
            }
            RefreshGrid();
        }

        //***********************************************************************************************************

        private void Query_ASM002(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM002";

            CQuery_ASM002 query = new CQuery_ASM002();
            List<CDataASM002_002> list = query.Query_ASM002(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM003(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM003";

            CQuery_ASM003 query = new CQuery_ASM003();
            List<CDataASM003_002> list = query.Query_ASM003(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM008(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM008";

            CQuery_ASM008 query = new CQuery_ASM008();
            List<CDataASM008_002> list = query.Query_ASM008(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM010(OleDbConnection conn)
        {
            ShowProgressForm("", "대상자를 조회하고 있습니다.");

            m_pgm_step = "Query_ASM010";
            
            List<CDataASM010_002> list = new List<CDataASM010_002>();
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;

            System.Windows.Forms.Application.DoEvents();

            string form = "ASM010";
            string keystr = txtFrdt.Text.ToString().Trim() + "," + txtTodt.Text.ToString().Trim();

            // 
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + keystr + "'";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                ShowProgressForm("", "대상자를 조회하고 있습니다." + row["PNM"].ToString());

                System.Windows.Forms.Application.DoEvents();

                CDataASM010_002 data = new CDataASM010_002();
                data.Clear();
                list.Add(data);

                data.SetValuesFromDataRow(row);

                data.PNM_TI2A = GetPNM_TI2A(conn, data.IOFG, data.BDODT, data.QFYCD,data.JRBY, data.PID, data.UNISQ, data.SIMCS);

                data.ReadDataFromSaved(conn, null);

                return MetroLib.SqlHelper.CONTINUE;
            });


            RefreshGrid();

            CloseProgressForm("", "");

            /*
            CQuery_ASM010 query = new CQuery_ASM010();
            query.QueryEvent += new EventHandler<QueryEventArgs>(query_QueryEvent);
            List<CDataASM010_002> list = query.Query_ASM010(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdMain.DataSource = null;
            grdMain.DataSource = list;
            RefreshGrid();
            */
            
        }

        private void Query_ASM014(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM014";

            CQuery_ASM014 query = new CQuery_ASM014();
            List<CDataASM014_001> list = query.Query_ASM014(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM023(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM023";

            CQuery_ASM023 query = new CQuery_ASM023();
            List<CDataASM023_002> list = query.Query_ASM023(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM024(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM024";

            CQuery_ASM024 query = new CQuery_ASM024();
            List<CDataASM024_002> list = query.Query_ASM024(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM033(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM033";

            CQuery_ASM033 query = new CQuery_ASM033();
            List<CDataASM033_003> list = query.Query_ASM033(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM035(OleDbConnection conn)
        {
            // 마취
            m_pgm_step = "Query_ASM035";

            ShowProgressForm("", "대상자를 조회하고 있습니다.");

            List<CDataASM035_003> list = new List<CDataASM035_003>();
            grdASM035.DataSource = null;
            grdASM035.DataSource = list;

            System.Windows.Forms.Application.DoEvents();

            string form = "ASM035";
            string keystr = txtFrdt.Text.ToString().Trim() + "," + txtTodt.Text.ToString().Trim();

            // 
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + keystr + "'";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                ShowProgressForm("", "대상자를 조회하고 있습니다." + row["PNM"].ToString());

                System.Windows.Forms.Application.DoEvents();

                CDataASM035_003 data = new CDataASM035_003();
                data.Clear();
                list.Add(data);

                data.SetValuesFromDataRow(row);

                data.PNM_TI2A = GetPNM_TI2A(conn, data.IOFG, data.BDODT, data.QFYCD, data.JRBY, data.PID, data.UNISQ, data.SIMCS);

                data.ReadDataFromSaved(conn, null);

                return MetroLib.SqlHelper.CONTINUE;
            });


            RefreshGrid();

            CloseProgressForm("", "");

            //CQuery_ASM035 query = new CQuery_ASM035();
            //List<CDataASM035_003> list = query.Query_ASM035(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            //grdASM010.DataSource = null;
            //grdASM010.DataSource = list;
            //RefreshGrid();
        }

        private void Query_ASM036(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM036";

            CQuery_ASM036 query = new CQuery_ASM036();
            List<CDataASM036_002> list = query.Query_ASM036(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM037(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM037";

            CQuery_ASM037 query = new CQuery_ASM037();
            List<CDataASM037_003> list = query.Query_ASM037(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private void Query_ASM049(OleDbConnection conn)
        {
            m_pgm_step = "Query_ASM049";

            CQuery_ASM049 query = new CQuery_ASM049();
            List<CDataASM049_001> list = query.Query_ASM049(conn, txtFrdt.Text.ToString(), txtTodt.Text.ToString());
            grdASM010.DataSource = null;
            grdASM010.DataSource = list;
            RefreshGrid();
        }

        private string GetPNM_TI2A(OleDbConnection conn, string iofg, string bdodt, string qfycd, string jrby, string pid, string unisq, string simcs)
        {
            if (iofg != "1" && iofg != "2") return "";
            if (bdodt == "" || qfycd == "" || jrby == "" || pid == "" || unisq == "" || simcs == "") return "";

            string tTI2A = "TI2A";
            string fBDODT = "BDODT";
            if (iofg == "1")
            {
                tTI2A = "TI1A";
                fBDODT = "EXDATE";
            }
            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT PNM";
            sql += Environment.NewLine + "  FROM " + tTI2A + "";
            sql += Environment.NewLine + " WHERE " + fBDODT + "='" + bdodt + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + qfycd + "'";
            sql += Environment.NewLine + "   AND JRBY='" + jrby + "'";
            sql += Environment.NewLine + "   AND PID='" + pid + "'";
            sql += Environment.NewLine + "   AND UNISQ='" + unisq + "'";
            sql += Environment.NewLine + "   AND SIMCS='" + simcs + "'";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                ret = row["PNM"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return ret;
        }

        void query_QueryEvent(object sender, QueryEventArgs e)
        {
            this.ShowProgressForm("", "자료 조회 중입니다(" + e.Message + ").");
            RefreshGrid();
        }


        //***********************************************************************************************************

        private void Make_ASM002(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM002";

            CMakeASM002 make = new CMakeASM002();
            List<CDataASM002_002> list = (List<CDataASM002_002>)grdASM010.DataSource;
            foreach (CDataASM002_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM002(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM003(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM003";

            CMakeASM003 make = new CMakeASM003();
            List<CDataASM003_002> list = (List<CDataASM003_002>)grdASM010.DataSource;
            foreach (CDataASM003_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM003(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM008(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM008";

            CMakeASM008 make = new CMakeASM008();
            List<CDataASM008_002> list = (List<CDataASM008_002>)grdASM010.DataSource;
            foreach (CDataASM008_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM008(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        //private void Make_ASM010(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        //{
        //    m_pgm_step = "Make_ASM010";
        //
        //    CMakeASM010 make = new CMakeASM010();
        //    List<CDataASM010_002> list = (List<CDataASM010_002>)grdMain.DataSource;
        //    foreach (CDataASM010_002 data in list)
        //    {
        //        this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
        //        m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
        //        make.MakeASM010(data, sysdt, systm, m_User, conn, tran, false);
        //    }
        //}

        private void Make_ASM014(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM014";

            CMakeASM014 make = new CMakeASM014();
            List<CDataASM014_001> list = (List<CDataASM014_001>)grdASM010.DataSource;
            foreach (CDataASM014_001 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM014(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM023(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM023";

            CMakeASM023 make = new CMakeASM023();
            List<CDataASM023_002> list = (List<CDataASM023_002>)grdASM010.DataSource;
            foreach (CDataASM023_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM023(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM024(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM024";

            CMakeASM024 make = new CMakeASM024();
            List<CDataASM024_002> list = (List<CDataASM024_002>)grdASM010.DataSource;
            foreach (CDataASM024_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM024(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM033(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM033";

            CMakeASM033 make = new CMakeASM033();
            List<CDataASM033_003> list = (List<CDataASM033_003>)grdASM010.DataSource;
            foreach (CDataASM033_003 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM033(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        //private void Make_ASM035(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        //{
        //    m_pgm_step = "Make_ASM035";
        //
        //    CMakeASM035 make = new CMakeASM035();
        //    List<CDataASM035_003> list = (List<CDataASM035_003>)grdASM010.DataSource;
        //    foreach (CDataASM035_003 data in list)
        //    {
        //        this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
        //        m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
        //        make.MakeASM035(data, sysdt, systm, m_User, conn, tran, false);
        //    }
        //}

        private void Make_ASM036(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM036";

            CMakeASM036 make = new CMakeASM036();
            List<CDataASM036_002> list = (List<CDataASM036_002>)grdASM010.DataSource;
            foreach (CDataASM036_002 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM036(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM037(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM037";

            CMakeASM037 make = new CMakeASM037();
            List<CDataASM037_003> list = (List<CDataASM037_003>)grdASM010.DataSource;
            foreach (CDataASM037_003 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM037(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        private void Make_ASM049(OleDbConnection conn, OleDbTransaction tran, string sysdt, string systm)
        {
            m_pgm_step = "Make_ASM049";

            CMakeASM049 make = new CMakeASM049();
            List<CDataASM049_001> list = (List<CDataASM049_001>)grdASM010.DataSource;
            foreach (CDataASM049_001 data in list)
            {
                this.ShowProgressForm("", data.PNM + "(" + data.PID + ") 환자 자료 조회 중입니다.");
                m_pgm_step = data.PNM + "(" + data.PID + ") " + data.STEDT;
                make.MakeASM049(data, sysdt, systm, m_User, conn, tran, false);
            }
        }

        //***********************************************************************************************************

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendTF(false);
        }

        private void btnSendTmp_Click(object sender, EventArgs e)
        {
            SendTF(true);
        }

        private void SendTF(bool isTmp)
        {
            if (txtTypeName.Text.ToString() == "") return;
            if (txtFrdt.Text.ToString() == "") return;
            if (txtTodt.Text.ToString() == "") return;

            if (MessageBox.Show((isTmp ? "임시 " : "") + "전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (isTmp ? "임시 " : "") + "전송 중입니다.");
                this.Send(isTmp);
                this.CloseProgressForm("", (isTmp ? "임시 " : "") + "전송 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (isTmp ? "임시 " : "") + "전송 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Send(bool isTmp)
        {
            string typeName = GetTypename();
            DevExpress.XtraGrid.Views.Grid.GridView view = null;
            if (typeName == "수술의예방적항생제사용")
            {
                view = grdASM010View;
            }
            else if (typeName == "마취")
            {
                view = grdASM035View;
            }
            else
            {
                MessageBox.Show("준비 중입니다.");
                return;
            }

            CHiraEForm hira = new CHiraEForm();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                Send_ASM_G(hira, conn, sysdt, systm, isTmp, view);

            }
        }

        private void Send_ASM_G(CHiraEForm hira, OleDbConnection conn, string sysdt, string systm, bool isTmp, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            // 대상 리스트를 일반화
            for (int row = 0; row < view.DataRowCount; row++)
            {
                string count_msg = (row + 1) + "/" + (view.DataRowCount);
                this.ShowProgressForm("", (isTmp ? "임시 " : "") + "전송 중입니다." + count_msg);
                view.FocusedRowHandle = row;
                CData data = (CData)view.GetRow(view.GetDataSourceRowIndex(row));
                RefreshGrid();
                hira.Send(data, isTmp, sysdt, systm, m_User, txtHosid.Text.ToString(), conn);
                RefreshGrid();
            }

        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;
            int row = e.RowHandle;
            if (row < 0) return;

            if (e.Column.FieldName == "STATUS_NM")
            {
                String val = e.CellValue.ToString();
                if (val == "오류" || val == "삭제오류")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            else if (e.Column.FieldName == "PNM")
            {
                // 현재 Row의 데이터 객체를 가져온다.
                object rowObj = view.GetRow(e.RowHandle);

                CData data = rowObj as CData;
                if (data != null)
                {
                    string pnm = (data.PNM == null) ? "" : data.PNM.Trim();
                    string pnmHira = (data.PNM_TI2A == null) ? "" : data.PNM_TI2A.Trim();

                    if (pnm != pnmHira)
                    {
                        e.Appearance.ForeColor = Color.Red;     // 빨간색 계열
                        // 좀 더 강조하고 싶으면(선택):
                        // e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;

            if (e.Column.FieldName == "SEL")
            {
                CData data = view.GetRow(e.RowHandle) as CData;
                if (data != null)
                {
                    data.SEL = !data.SEL;
                    RefreshGrid();
                }
            }
            else if (e.Column.FieldName == "STATUS_NM")
            {
                txtMsg.Text = "";
                CData data = view.GetRow(e.RowHandle) as CData;
                if (data != null)
                {
                    txtMsg.Text = data.ERR_MSG;
                }
            }
        }

        private void chkSendAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string typeName = GetTypename();
                DevExpress.XtraGrid.Views.Grid.GridView view = null;
                if (typeName == "수술의예방적항생제사용")
                {
                    view = grdASM010View;
                }
                else if (typeName == "마취")
                {
                    view = grdASM035View;
                }
                else
                {
                    MessageBox.Show("준비 중입니다.");
                    return;
                }

                for (int row = 0; row < view.DataRowCount; row++)
                {
                    view.SetRowCellValue(row, "SEL", chkSendAll.Checked);
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            if (txtTypeName.Text.ToString() == "") return;

            ShowSubForm();
            RefreshGrid();
        }

        private void ShowSubForm()
        {
            Form f = null;
            string typeName = GetTypename();
            if (typeName == "관상동맥우회술")
            {
                f = new ADD7007_ASM002_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "급성기뇌졸증")
            {
                f = new ADD7007_ASM003_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "마취")
            {
                var frm = new ADD7007_ASM035_003(txtHosid.Text.ToString(), m_User, grdASM035View);
                frm.RemakeRequested += new EventHandler<RemakeRequestedEventArgs<CDataASM035_003>>(ASM_RemakeRequested);
                f = frm;
            }
            else if (typeName == "수술의예방적항생제사용")
            {
                var frm = new ADD7007_ASM010_002(txtHosid.Text.ToString(), m_User, grdASM010View);
                frm.RemakeRequested += new EventHandler<RemakeRequestedEventArgs<CDataASM010_002>>(ASM_RemakeRequested);
                f = frm;
            }
            else if (typeName == "수혈")
            {
                f = new ADD7007_ASM037_003(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "신생아중환자실")
            {
                f = new ADD7007_ASM033_003(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "영상검사")
            {
                f = new ADD7007_ASM049_001(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "의료급여정신과")
            {
                f = new ADD7007_ASM014_001(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "정신건강입원영역")
            {
                f = new ADD7007_ASM036_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "중환자실")
            {
                f = new ADD7007_ASM024_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "폐렴")
            {
                f = new ADD7007_ASM023_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else if (typeName == "혈액투석")
            {
                f = new ADD7007_ASM008_002(txtHosid.Text.ToString(), m_User, grdASM010View);
            }
            else
            {
                MessageBox.Show("알 수 없는 서식입니다.");
                return;
            }

            f.ShowDialog(this);
            RefreshGrid();
        }

        private void ASM_RemakeRequested<T>(object sender, RemakeRequestedEventArgs<T> e)
            where T : class, IDataRemake
        {
            OleDbTransaction tran = null;
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sysdt = MetroLib.Util.GetSysDate(conn);
                    string systm = MetroLib.Util.GetSysTime(conn);

                    e.data.ReadDataFromEMR(conn, null);

                    tran = conn.BeginTransaction();

                    e.data.UpdData(sysdt, systm, m_User, conn, tran);

                    tran.Commit();

                    e.Success = true;
                }

                // 메인 그리드도 필요 시 갱신
                RefreshGrid();
            }
            catch (Exception ex)
            {
                e.Success = false;
                e.FailureMessage = ex.Message;

                if (tran != null) tran.Rollback();
            }
        }

        private void grdListView_DoubleClick(object sender, EventArgs e)
        {
            txtTypeName.Text = grdListView.GetRowCellValue(grdListView.GetDataSourceRowIndex(grdListView.FocusedRowHandle), "TYPE_NM").ToString();
            if (txtTypeName.Text.ToString().Trim() == "수술의예방적항생제사용")
            {
                grdASM010.BringToFront();
                txtFrdt.Text = "20250401";
                txtTodt.Text = "20250630";
                grdASM010.DataSource = null;
            }
            else if (txtTypeName.Text.ToString().Trim() == "마취")
            {
                grdASM035.BringToFront();
                txtFrdt.Text = "20250701";
                txtTodt.Text = "20250930";
                grdASM035.DataSource = null;
            }
            RefreshGrid();
        }

        private void btnExcept_Click(object sender, EventArgs e)
        {
            MessageBox.Show("준비 중입니다.");
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
            //    this.Except();
            //    this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
            //    Cursor.Current = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
            //    Cursor.Current = Cursors.Default;
            //    MessageBox.Show(ex.Message);
            //}
        }

        //private void Except()
        //{
        //    string strConn = MetroLib.DBHelper.GetConnectionString();
        //    using (OleDbConnection conn = new OleDbConnection(strConn))
        //    {
        //        conn.Open();
        //
        //        string sysdt = MetroLib.Util.GetSysDate(conn);
        //        string systm = MetroLib.Util.GetSysTime(conn);
        //
        //        int topRowIndex = grdASM010View.TopRowIndex;
        //        int focusedRowHandle = grdASM010View.FocusedRowHandle;
        //
        //        for (int row = 0; row < grdASM010View.DataRowCount; row++)
        //        {
        //            grdASM010View.FocusedRowHandle = row;
        //            CData data = (CData)grdASM010View.GetRow(grdASM010View.GetDataSourceRowIndex(row));
        //            ExceptOne(data, sysdt, systm, conn);
        //        }
        //
        //        grdASM010View.FocusedRowHandle = focusedRowHandle;
        //        grdASM010View.TopRowIndex = topRowIndex;
        //        RefreshGrid();
        //    }
        //}

        //private void ExceptOne(CData data, string p_sysdt, string p_systm, OleDbConnection p_conn)
        //{
        //    if (data.SEL == false) return;
        //    if (data.STATUS == "Y") return; // 전송에 성공한 자료는 삭제할 수 없음.
        //
        //    string status = data.STATUS;
        //
        //    data.STATUS = "N"; // 진행중
        //    RefreshGrid();
        //
        //    data.Except_ASM000(p_sysdt, p_systm, m_User, p_conn);
        //
        //    data.STATUS = status;
        //    RefreshGrid();
        //}

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string typeName = GetTypename();
                DevExpress.XtraGrid.Views.Grid.GridView view = null;
                DevExpress.XtraGrid.GridControl grid = null;
                if (typeName == "수술의예방적항생제사용")
                {
                    view = grdASM010View;
                    grid = grdASM010;
                }
                else if (typeName == "마취")
                {
                    view = grdASM035View;
                    grid = grdASM035;
                }
                else
                {
                    MessageBox.Show("준비 중입니다.");
                    return;
                }

                if (view.RowCount < 1) return; // 자료가 없으면 종료.

                String filePath = "";
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = "적정성자료_" + txtTypeName.Text.ToString() + "_" + txtFrdt.Text.ToString() + "_" + txtTodt.Text.ToString();

                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    view.Columns["SEL"].Visible = false;
                    grid.ExportToXlsx(filePath);
                    view.Columns["SEL"].Visible = true;

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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string typeName = txtTypeName.Text.ToString().Trim();
            string frdt = txtFrdt.Text.ToString().Trim();
            string todt = txtTodt.Text.ToString().Trim();

            if (typeName == "")
            {
                MessageBox.Show("서식명을 선택하세요.");
                return;
            }
            if (frdt == "")
            {
                MessageBox.Show("시작일을 입력하세요.");
                return;
            }
            if (todt == "")
            {
                MessageBox.Show("종료일을 입력하세요.");
                return;
            }

            ADD7007E_EXCEL f = new ADD7007E_EXCEL(typeName, frdt, todt);
            f.PtntChanged += new EventHandler<PtntChangedEventArgs>(f_PtntChanged);
            f.ShowDialog(this);
        }

        private void f_PtntChanged(object sender, PtntChangedEventArgs e)
        {
            string typeName = GetTypename();
            if (typeName == "수술의예방학적항생제사용")
            {
                f_PtntChanged_ASM010(sender, e);
            }
            else if (typeName == "마취")
            {
                f_PtntChanged_ASM035(sender, e);
            }

            RefreshGrid();
        }

        private void f_PtntChanged_ASM010(object sender, PtntChangedEventArgs e)
        {
            List<CDataASM010_002> list = null;
            if (e.no == 1)
            {
                list = new List<CDataASM010_002>();
                grdASM010.DataSource = null;
                grdASM010.DataSource = list;
            }
            else
            {
                list = grdASM010.DataSource as List<CDataASM010_002>;
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                // 접수번호, 명일련번호, 환자명이 있으므로 청구내역을 찾는다.
                CDataASM010_002 data = GetASM010_002(conn, e.frdt, e.todt, e.no, e.cnecno, e.cnecdd, e.eprtno, e.pnm, e.dacd1, e.opcode1);
                if (data.DEMNO != "")
                {
                    data.ReadDataFromEMR(conn, null);
                }
                list.Add(data);

                OleDbTransaction tran = null;
                try
                {
                    tran = conn.BeginTransaction();

                    if (e.no == 1)
                    {
                        // 모든 TI84_ASM000 자료를 삭제함.
                        data.DelAll_ASM000(conn, tran);

                        // 모든 자료 삭제
                        data.DelAllData(conn, tran);
                    }

                    // TI84_ASM000 저장
                    data.Into_ASM000(sysdt, systm, m_User, conn, tran, false);

                    // 자료저장
                    data.InsData(sysdt, systm, m_User, conn, tran, false);

                    tran.Commit();

                    e.Success = true;
                }
                catch (Exception ex)
                {
                    e.Success = false;
                    e.FailureMessage = ex.Message;

                    if (tran != null) tran.Rollback();
                }
            }
        }

        private CDataASM010_002 GetASM010_002(OleDbConnection conn, string frdt, string todt, int no, string cnecno, string cnecdd, string eprtno, string pnm, string dacd1, string opcode1)
        {
            // eprtno가 뒤에 "00"을 없애버려야 함.
            if (eprtno.EndsWith("00"))
                eprtno = eprtno.Substring(0, eprtno.Length - 2);

            // 청구번호를 구한다.
            string cnectdd = "";
            string billsno = "";
            string req_demno = "";
            string demno = GetDemno(cnecno, conn, ref cnectdd, ref billsno, ref req_demno);

            CDataASM010_002 data = new CDataASM010_002();
            data.Clear();

            data.KEYSTR = frdt + "," + todt;
            data.SEQ = no;
            data.NO = no;
            data.CNECNO = cnecno;
            data.CNECTDD = cnecdd; // 접수일자
            data.DEMNO = demno;
            data.EPRTNO = eprtno;
            data.PNM = pnm; // 심평원에서 준 이름
            data.BILLSNO = billsno; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

            data.DACD = dacd1;
            data.MDFEE_CD = opcode1; // 수술코드

            // 2026.02.20 WOOIL - 청구번호가 있는 경우에만 이하 작업을 한다.
            if (demno != "")
            {

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TI2A";
                sql += Environment.NewLine + " WHERE DEMNO='" + req_demno + "'";
                sql += Environment.NewLine + "   AND EPRTNO='" + eprtno + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    data.SEL = true;
                    data.IOFG = "2";
                    data.PID = row["PID"].ToString();
                    data.PNM_TI2A = row["PNM"].ToString();
                    data.RESID = row["RESID"].ToString();
                    data.BDEDT = row["BDEDT"].ToString();
                    data.QFYCD = row["QFYCD"].ToString();
                    data.GONSGB = row["GONSGB"].ToString();

                    data.BDODT = row["BDODT"].ToString();
                    data.JRBY = row["JRBY"].ToString();
                    data.UNISQ = row["UNISQ"].ToString();
                    data.SIMCS = row["SIMCS"].ToString();

                    data.STEDT = row["STEDT"].ToString();

                    string a04_bedehm = "";
                    string a04_bedodt = "";
                    string a04_bedohm = "";
                    string a04_bedodiv = "";

                    GetA04(data.PID, data.BDEDT, conn, ref a04_bedehm, ref a04_bedodt, ref a04_bedohm, ref a04_bedodiv);

                    data.A04_BEDEHM = a04_bedehm;
                    data.A04_BEDODT = a04_bedodt;
                    data.A04_BEDOHM = a04_bedohm;
                    data.A04_BEDODIV = a04_bedodiv;


                    data.FR_DATE = frdt; // 자료 시작일
                    data.TO_DATE = todt; // 자료 종료일

                    return MetroLib.SqlHelper.BREAK;
                });

            }

            return data;
        }

        private void f_PtntChanged_ASM035(object sender, PtntChangedEventArgs e)
        {
            List<CDataASM035_003> list = null;
            if (e.no == 1)
            {
                list = new List<CDataASM035_003>();
                grdASM035.DataSource = null;
                grdASM035.DataSource = list;
            }
            else
            {
                list = grdASM035.DataSource as List<CDataASM035_003>;
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                // 접수번호, 명일련번호, 환자명이 있으므로 청구내역을 찾는다.
                CDataASM035_003 data = GetASM035_003(conn, e.frdt, e.todt, e.no, e.cnecno, e.cnecdd, e.eprtno, e.pnm, e.dacd1, e.opcode1);
                if (data.DEMNO != "")
                {
                    data.ReadDataFromEMR(conn, null);
                }
                list.Add(data);

                OleDbTransaction tran = null;
                try
                {
                    tran = conn.BeginTransaction();

                    if (e.no == 1)
                    {
                        // 모든 TI84_ASM000 자료를 삭제함.
                        data.DelAll_ASM000(conn, tran);

                        // 모든 자료 삭제
                        data.DelAllData(conn, tran);
                    }

                    // TI84_ASM000 저장
                    data.Into_ASM000(sysdt, systm, m_User, conn, tran, false);

                    // 자료저장
                    data.InsData(sysdt, systm, m_User, conn, tran, false);

                    tran.Commit();

                    e.Success = true;
                }
                catch (Exception ex)
                {
                    e.Success = false;
                    e.FailureMessage = ex.Message;

                    if (tran != null) tran.Rollback();
                }
            }
        }

        private CDataASM035_003 GetASM035_003(OleDbConnection conn, string frdt, string todt, int no, string cnecno, string cnecdd, string eprtno, string pnm, string dacd1, string opcode1)
        {
            // eprtno가 뒤에 "00"을 없애버려야 함.
            if (eprtno.EndsWith("00"))
                eprtno = eprtno.Substring(0, eprtno.Length - 2);

            // 청구번호를 구한다.
            string cnectdd = "";
            string billsno = "";
            string req_demno = "";
            string demno = GetDemno(cnecno, conn, ref cnectdd, ref billsno, ref req_demno);

            CDataASM035_003 data = new CDataASM035_003();
            data.Clear();

            data.KEYSTR = frdt + "," + todt;
            data.SEQ = no;
            data.NO = no;
            data.CNECNO = cnecno;
            data.CNECTDD = cnecdd; // 접수일자
            data.DEMNO = demno;
            data.EPRTNO = eprtno;
            data.PNM = pnm; // 심평원에서 준 이름
            data.BILLSNO = billsno; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

            data.DACD = dacd1;
            data.MDFEE_CD = opcode1; // 수술코드

            // 2026.02.20 WOOIL - 청구번호가 있는 경우에만 이하 작업을 한다.
            if (demno != "")
            {

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TI2A";
                sql += Environment.NewLine + " WHERE DEMNO='" + req_demno + "'";
                sql += Environment.NewLine + "   AND EPRTNO='" + eprtno + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    data.SEL = true;
                    data.IOFG = "2";
                    data.PID = row["PID"].ToString();
                    data.PNM_TI2A = row["PNM"].ToString();
                    data.RESID = row["RESID"].ToString();
                    data.BDEDT = row["BDEDT"].ToString();
                    data.QFYCD = row["QFYCD"].ToString();
                    data.GONSGB = row["GONSGB"].ToString();

                    data.BDODT = row["BDODT"].ToString();
                    data.JRBY = row["JRBY"].ToString();
                    data.UNISQ = row["UNISQ"].ToString();
                    data.SIMCS = row["SIMCS"].ToString();

                    data.STEDT = row["STEDT"].ToString();

                    string a04_bedehm = "";
                    string a04_bedodt = "";
                    string a04_bedohm = "";
                    string a04_bedodiv = "";

                    GetA04(data.PID, data.BDEDT, conn, ref a04_bedehm, ref a04_bedodt, ref a04_bedohm, ref a04_bedodiv);

                    data.A04_BEDEHM = a04_bedehm;
                    data.A04_BEDODT = a04_bedodt;
                    data.A04_BEDOHM = a04_bedohm;
                    data.A04_BEDODIV = a04_bedodiv;


                    data.FR_DATE = frdt; // 자료 시작일
                    data.TO_DATE = todt; // 자료 종료일

                    return MetroLib.SqlHelper.BREAK;
                });

            }

            return data;
        }

        private string GetDemno(string p_cnecno, OleDbConnection conn, ref string p_cnectdd, ref string p_billsno, ref string p_req_demno)
        {
            string demno = "";
            string cnectdd = "";
            string billsno = "1"; // 일단 1로 고정
            //string req_demno = "";

            // 접수증을 읽는다.
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TIE_F0102";
            sql += System.Environment.NewLine + " WHERE CNECTNO='" + p_cnecno + "'";
            sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                demno = row["DEMNO"].ToString();
                cnectdd = row["CNECTDD"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            p_cnectdd = cnectdd;
            p_billsno = billsno;

            if (demno == "")
            {

                // 보험심결을 읽는다.
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0201_062";
                sql += System.Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    demno = row["DEMNO"].ToString();
                    return MetroLib.SqlHelper.BREAK;
                });
            }

            if (demno == "")
            {

                // 보호심결을 읽는다.
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0601_062";
                sql += System.Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    demno = row["DEMNO"].ToString();
                    return MetroLib.SqlHelper.BREAK;
                });
            }

            // 2026.02.25 WOOIL - 송신했을 때 청구번호와 현재 병원에 남아있는 청구번호가 다른 경우를 처리하기 위한 변수. 저장하지는 않는다.
            p_req_demno = demno;

            // 2026.02.20 WOOIL - 김포가자연세병원에 접수번호와 청구번호 연결이 잘 되지 않아 강제로 변경함.
            if (txtHosid.Text.ToString().Trim() == "31210686")
            {
                if (p_cnecno.Trim() == "4235830")
                {
                    if (p_req_demno.Trim() == "2025042502")
                    {
                        p_req_demno = "2025042501";
                    }
                }
                if (p_cnecno.Trim() == "4299852")
                {
                    if (p_req_demno.Trim() == "")
                    {
                        p_req_demno = "2025052501";
                    }
                }
            }

            return demno;
        }

        private void GetA04(string p_pid, string p_bededt, OleDbConnection p_conn, ref string p_bedehm, ref string p_bedodt, ref string p_bedohm, ref string p_bedodiv)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA04";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + p_bededt + "'";

            string bedehm = "";
            string bedodt = "";
            string bedohm = "";
            string bedodiv = "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                bedehm = reader["BEDEHM"].ToString();
                bedodt = reader["BEDODT"].ToString();
                bedohm = reader["BEDOHM"].ToString();
                bedodiv = reader["BEDODIV"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            p_bedehm = bedehm;
            p_bedodt = bedodt;
            p_bedohm = bedohm;
            p_bedodiv = bedodiv;
        }

        //***********************************************************************************************************

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

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdList, grdListView);
            CUtil.RefreshGrid(grdASM010, grdASM010View);
            CUtil.RefreshGrid(grdASM035, grdASM035View);
        }

    }
}

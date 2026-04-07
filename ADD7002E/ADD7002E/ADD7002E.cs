using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7002E
{
    public partial class ADD7002E : Form
    {
        private String m_User;
        private String m_Pwd;

        public ADD7002E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
        }

        public ADD7002E(String user,String pwd):this()
        {
            m_User = user;
            m_Pwd = pwd;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ShowProgressForm("", "자료조회중입니다.");
                this.Query();
                CloseProgressForm("", "자료조회중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                CloseProgressForm("", "자료조회중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Query()
        {
            grdMain.DataSource = null;
            List<CTT26_KDRG11> list = new List<CTT26_KDRG11>();
            list.Clear();
            grdMain.DataSource = list;

            int count = 0;
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string strRdrg = txtRDRG.Text.ToString();
                string strAppldt = txtAPPLDT.Text.ToString();
                if ("".Equals(strAppldt)) strAppldt = CUtil.GetSysDate(conn);

                sql = "";
                sql += System.Environment.NewLine + "SELECT DISTINCT APPLDT";
                sql += System.Environment.NewLine + "  FROM TT26_KDRG11";
                sql += System.Environment.NewLine + " WHERE APPLDT<='" + strAppldt + "'";
                sql += System.Environment.NewLine + " ORDER BY APPLDT DESC";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand qcmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader qreader = qcmd.ExecuteReader();

                    if (qreader.Read())
                    {
                        strAppldt = qreader["APPLDT"].ToString();
                    }
                    qreader.Close();
                }

                sql = "";
                sql += System.Environment.NewLine + "SELECT DISTINCT RDRG, NULL ILSU, APPLDT, AADRG, NAME, IMOSGB, IB, BTMILSU, TOPILSU, STDILSU, DANGA, STDSCR, DAYSCR, INCENTIVE, BHMADJFACTOR, BHMSTDSCR, BHMDAYSCR, BHMSTDSUGA, BHMDAYSUGA, NULL BHMINCLSUGA, NULL BHMSUGA2, BHOADJFACTOR, BHOSTDSCR, BHODAYSCR, BHOSTDSUGA, BHODAYSUGA, NULL BHOINCLSUGA, NULL BHOSUGA2, DRG7GROUPYM";
                sql += System.Environment.NewLine + "  FROM TT26_KDRG11";
                sql += System.Environment.NewLine + " WHERE APPLDT='" + strAppldt + "'";
                if ("".Equals(strRdrg) == false)
                {
                    sql += System.Environment.NewLine + "    AND RDRG LIKE '" + strRdrg + "%'";
                }
                sql += System.Environment.NewLine + " ORDER BY RDRG, APPLDT, ILSU";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                        CTT26_KDRG11 t26 = new CTT26_KDRG11();
                        t26.RDRG = reader["RDRG"].ToString();
                        t26.ILSU = reader["ILSU"].ToString();
                        t26.APPLDT = reader["APPLDT"].ToString();
                        t26.AADRG = reader["AADRG"].ToString();
                        t26.NAME = reader["NAME"].ToString();
                        t26.IMOSGB = reader["IMOSGB"].ToString();
                        t26.IB = reader["IB"].ToString();
                        t26.BTMILSU = reader["BTMILSU"].ToString();
                        t26.TOPILSU = reader["TOPILSU"].ToString();
                        t26.STDILSU = reader["STDILSU"].ToString();
                        t26.DANGA = reader["DANGA"].ToString();
                        t26.STDSCR = reader["STDSCR"].ToString();
                        t26.DAYSCR = reader["DAYSCR"].ToString();
                        t26.INCENTIVE = reader["INCENTIVE"].ToString();
                        t26.BHMADJFACTOR = reader["BHMADJFACTOR"].ToString();
                        t26.BHMSTDSCR = reader["BHMSTDSCR"].ToString();
                        t26.BHMDAYSCR = reader["BHMDAYSCR"].ToString();
                        t26.BHMSTDSUGA = reader["BHMSTDSUGA"].ToString();
                        t26.BHMDAYSUGA = reader["BHMDAYSUGA"].ToString();
                        t26.BHMINCLSUGA = reader["BHMINCLSUGA"].ToString();
                        t26.BHMSUGA2 = reader["BHMSUGA2"].ToString();
                        t26.BHOADJFACTOR = reader["BHOADJFACTOR"].ToString();
                        t26.BHOSTDSCR = reader["BHOSTDSCR"].ToString();
                        t26.BHODAYSCR = reader["BHODAYSCR"].ToString();
                        t26.BHOSTDSUGA = reader["BHOSTDSUGA"].ToString();
                        t26.BHODAYSUGA = reader["BHODAYSUGA"].ToString();
                        t26.BHOINCLSUGA = reader["BHOINCLSUGA"].ToString();
                        t26.BHOSUGA2 = reader["BHOSUGA2"].ToString();
                        t26.DRG7GROUPYM = reader["DRG7GROUPYM"].ToString();

                        list.Add(t26);

                        grdMainView.FocusedRowHandle = count - 1;
                        this.RefreshGridMain();
                    }
                    reader.Close();
                }
                if (count > 0) grdMainView.FocusedRowHandle = 0;
            }
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            List<CTT26_KDRG11> list = (List<CTT26_KDRG11>)grdMain.DataSource;
            if (list == null) return;
            CTT26_KDRG11 t26 = list[grdMainView.FocusedRowHandle];
            if (t26 == null) return;
            if ("".EndsWith(t26.ILSU))
            {
                t26.ILSU = "+";
                RefreshGridMain();
                this.Query(t26.APPLDT, t26.RDRG, grdMainView.FocusedRowHandle, list);
            }
        }

        private void Query(string p_strAppldt, string p_strRdrg, int p_row, List<CTT26_KDRG11> p_list)
        {
            int count = 0;
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sql = "";
                sql += System.Environment.NewLine + "SELECT RDRG, ILSU, APPLDT, AADRG, NAME, IMOSGB, IB, BTMILSU, TOPILSU, STDILSU, DANGA, STDSCR, DAYSCR, INCENTIVE, BHMADJFACTOR, BHMSTDSCR, BHMDAYSCR, BHMSTDSUGA, BHMDAYSUGA, BHMINCLSUGA, BHMSUGA2, BHOADJFACTOR, BHOSTDSCR, BHODAYSCR, BHOSTDSUGA, BHODAYSUGA, BHOINCLSUGA, BHOSUGA2, DRG7GROUPYM";
                sql += System.Environment.NewLine + "  FROM TT26_KDRG11";
                sql += System.Environment.NewLine + " WHERE RDRG='" + p_strRdrg + "'";
                sql += System.Environment.NewLine + "   AND APPLDT='" + p_strAppldt + "'";
                sql += System.Environment.NewLine + " ORDER BY RDRG, APPLDT, ILSU";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                        CTT26_KDRG11 t26 = new CTT26_KDRG11();
                        t26.RDRG = reader["RDRG"].ToString();
                        t26.ILSU = reader["ILSU"].ToString();
                        t26.APPLDT = reader["APPLDT"].ToString();
                        t26.AADRG = reader["AADRG"].ToString();
                        t26.NAME = reader["NAME"].ToString();
                        t26.IMOSGB = reader["IMOSGB"].ToString();
                        t26.IB = reader["IB"].ToString();
                        t26.BTMILSU = reader["BTMILSU"].ToString();
                        t26.TOPILSU = reader["TOPILSU"].ToString();
                        t26.STDILSU = reader["STDILSU"].ToString();
                        t26.DANGA = reader["DANGA"].ToString();
                        t26.STDSCR = reader["STDSCR"].ToString();
                        t26.DAYSCR = reader["DAYSCR"].ToString();
                        t26.INCENTIVE = reader["INCENTIVE"].ToString();
                        t26.BHMADJFACTOR = reader["BHMADJFACTOR"].ToString();
                        t26.BHMSTDSCR = reader["BHMSTDSCR"].ToString();
                        t26.BHMDAYSCR = reader["BHMDAYSCR"].ToString();
                        t26.BHMSTDSUGA = reader["BHMSTDSUGA"].ToString();
                        t26.BHMDAYSUGA = reader["BHMDAYSUGA"].ToString();
                        t26.BHMINCLSUGA = reader["BHMINCLSUGA"].ToString();
                        t26.BHMSUGA2 = reader["BHMSUGA2"].ToString();
                        t26.BHOADJFACTOR = reader["BHOADJFACTOR"].ToString();
                        t26.BHOSTDSCR = reader["BHOSTDSCR"].ToString();
                        t26.BHODAYSCR = reader["BHODAYSCR"].ToString();
                        t26.BHOSTDSUGA = reader["BHOSTDSUGA"].ToString();
                        t26.BHODAYSUGA = reader["BHODAYSUGA"].ToString();
                        t26.BHOINCLSUGA = reader["BHOINCLSUGA"].ToString();
                        t26.BHOSUGA2 = reader["BHOSUGA2"].ToString();
                        t26.DRG7GROUPYM = reader["DRG7GROUPYM"].ToString();

                        p_list.Insert(p_row + count,t26);

                        this.RefreshGridMain();
                    }
                    reader.Close();
                }
            }
        }

    }
}

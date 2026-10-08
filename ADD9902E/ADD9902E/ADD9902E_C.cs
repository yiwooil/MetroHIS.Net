using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9902E
{
    public partial class ADD9902E_C : Form
    {
        public ADD9902E_C()
        {
            InitializeComponent();
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
            List<CDataHx> list = new List<CDataHx>();
            Dictionary<string, CDataHx> rows = new Dictionary<string, CDataHx>();

            grdMain.DataSource = null;

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 적용일자별 행 생성
                string sql = "";
                sql += Environment.NewLine + "SELECT DISTINCT MST4CD";
                sql += Environment.NewLine + "  FROM TA88A";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                sql += Environment.NewLine + "   AND MST3CD IN ('2','4','5','48')";
                sql += Environment.NewLine + " ORDER BY MST4CD";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataHx data = new CDataHx();
                    data.MST4CD = reader["MST4CD"].ToString();

                    list.Add(data);
                    rows[data.MST4CD] = data;

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 종별, 기관기호, 대표자, 작성자 순으로 조회
                string[] codes = new string[] { "4", "2", "5", "48" };

                foreach (string mst3cd in codes)
                {
                    sql = "";
                    sql += Environment.NewLine + "SELECT MST4CD,FLD1CD,FLD1QTY,FLD2QTY,FLD3QTY";
                    sql += Environment.NewLine + "  FROM TA88A";
                    sql += Environment.NewLine + " WHERE MST1CD='A'";
                    sql += Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                    sql += Environment.NewLine + "   AND MST3CD=?";
                    sql += Environment.NewLine + " ORDER BY MST4CD";

                    List<Object> para = new List<Object>();
                    para.Add(mst3cd);

                    MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                    {
                        string mst4cd = reader["MST4CD"].ToString();
                        CDataHx data;

                        // 해당 적용일자의 행이 없으면 추가
                        if (rows.TryGetValue(mst4cd, out data) == false)
                        {
                            data = new CDataHx();
                            data.MST4CD = mst4cd;

                            list.Add(data);
                            rows[mst4cd] = data;
                        }

                        switch (mst3cd)
                        {
                            case "4":
                                data.JONG = reader["FLD1CD"].ToString();
                                data.JONGNM = GetJongNm(data.JONG);
                                break;

                            case "2":
                                data.HOSID = reader["FLD1QTY"].ToString();
                                data.HOSID2 = reader["FLD2QTY"].ToString();
                                break;

                            case "5":
                                data.HOSCEO = reader["FLD1QTY"].ToString();
                                data.HOSCEORID = reader["FLD2QTY"].ToString();
                                break;

                            case "48":
                                data.WORKNM = reader["FLD1QTY"].ToString();
                                data.WORKRID = reader["FLD2QTY"].ToString();
                                data.DEMNMSANJE = reader["FLD3QTY"].ToString();
                                break;
                        }

                        return MetroLib.SqlHelper.CONTINUE;
                    });
                }
            }

            grdMain.DataSource = list;
            grdMainView.RefreshData();
        }

        private string GetJongNm(string jongcd)
        {
            switch (jongcd)
            {
                case "1":
                    return "전문요양기관";
                case "2":
                    return "종합병원";
                case "3":
                    return "병원";
                case "4":
                    return "의원";
                case "5":
                    return "약국";
                default:
                    return jongcd;
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

        private void ADD9902E_C_Shown(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
        }

        private void btnAddHx0_Click(object sender, EventArgs e)
        {
            AddHx(0);
        }

        private void btnAddHx1_Click(object sender, EventArgs e)
        {
            AddHx(1);
        }

        private void btnAddHx2_Click(object sender, EventArgs e)
        {
            AddHx(2);
        }

        private void AddHx(int mode)
        {
            using (ADD9902E_C1 form = new ADD9902E_C1(mode))
            {
                form.ShowDialog(this);
            }

            // VB6처럼 폼이 닫히면 재조회
            btnQuery.PerformClick();
        }

    }
}

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
    public partial class ADD9902E_B : Form
    {
        public ADD9902E_B()
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
            string table = "TI88";
            string hospKey = "HOSPITAL"; // 병원 키 설정은 추후 추가

            if (txtTA88DCD.Checked)
            {
                table = "TA88";
                hospKey = "DCD";
            }
            else if (chkTA88.Checked)
            {
                table = "TA88";
            }

            List<CData> list = new List<CData>();

            // 기존 조회 결과 초기화
            grdMain.DataSource = null;

            if (hospKey == "")
            {
                grdMain.DataSource = list;
                return;
            }

            List<Object> para = new List<Object>();
            para.Add(hospKey);
            para.Add("%" + txtCondition.Text + "%");

            // 각 조회 행을 CData에 담아 list에 추가
            Func<OleDbDataReader, bool> setValue = delegate(OleDbDataReader reader)
            {
                CData data = new CData();

                data.MST3CD = reader["MST3CD"].ToString();
                data.CDNM = reader["CDNM"].ToString();
                data.FLD1QTY = reader["FLD1QTY"].ToString();
                data.FLD2QTY = reader["FLD2QTY"].ToString();
                data.FLD3QTY = reader["FLD3QTY"].ToString();
                data.FLD4QTY = reader["FLD4QTY"].ToString();
                data.FLD1CD = reader["FLD1CD"].ToString();
                data.FLD2CD = reader["FLD2CD"].ToString();
                data.FLD3CD = reader["FLD3CD"].ToString();
                data.FLD4CD = reader["FLD4CD"].ToString();
                data.FLD5CD = reader["FLD5CD"].ToString();

                list.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            };

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 숫자 코드: 숫자 기준 내림차순
                string sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM " + table;
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD=?";
                sql += Environment.NewLine + "   AND ISNUMERIC(MST3CD)=1";
                sql += Environment.NewLine + "   AND ISNULL(CDNM,'') LIKE ?";
                sql += Environment.NewLine + " ORDER BY CONVERT(NUMERIC,MST3CD) DESC";

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, setValue);

                // 비숫자 코드: 문자 기준 내림차순
                // 같은 list에 이어서 추가
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM " + table;
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD=?";
                sql += Environment.NewLine + "   AND ISNUMERIC(MST3CD)=0";
                sql += Environment.NewLine + "   AND ISNULL(CDNM,'') LIKE ?";
                sql += Environment.NewLine + " ORDER BY MST3CD DESC";

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, setValue);
            }

            // 조회가 끝난 list를 그리드에 바인딩
            grdMain.DataSource = list;
            grdMainView.RefreshData();
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

        private void chkTA88_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTA88.Checked)
            {
                txtTA88DCD.Checked = false;
            }
            grdMain.DataSource = new List<CData>();
            grdMainView.RefreshData();
        }

        private void txtTA88DCD_CheckedChanged(object sender, EventArgs e)
        {
            if (txtTA88DCD.Checked)
            {
                chkTA88.Checked = false;
            }
            grdMain.DataSource = new List<CData>();
            grdMainView.RefreshData();
        }

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = grdMainView.CalcHitInfo(grdMain.PointToClient(Control.MousePosition));

            if (hitInfo.InRow == false && hitInfo.InRowCell == false)
            {
                return;
            }

            if (grdMainView.IsDataRow(hitInfo.RowHandle) == false)
            {
                return;
            }

            CData data = grdMainView.GetRow(hitInfo.RowHandle) as CData;

            if (data == null)
            {
                return;
            }

            string table = "TI88";
            string mst2cd = "HOSPITAL";

            if (txtTA88DCD.Checked == true)
            {
                table = "TA88";
                mst2cd = "DCD";
            }
            else if (chkTA88.Checked == true)
            {
                table = "TA88";
            }

            using (ADD9902E_B1 form = new ADD9902E_B1(table, "A", mst2cd, data.MST3CD))
            {
                form.ShowDialog(this);
            }
        }

        private void btnShowHx_Click(object sender, EventArgs e)
        {
            using (ADD9902E_C form = new ADD9902E_C())
            {
                form.ShowDialog(this);
            }
        }

    }
}

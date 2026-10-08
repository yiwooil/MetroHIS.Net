using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9902E
{
    public partial class ADD9902E_C1 : Form
    {
        private int m_mode = 0;
        
        public ADD9902E_C1()
        {
            InitializeComponent();
        }

        public ADD9902E_C1(int mode)
            : this()
        {
            m_mode = mode;

            lblFld3qty.Visible = (mode == 2);
            txtFld3qty.Visible = (mode == 2);

            switch (mode)
            {
                case 0:
                    lblFld1qty.Text = "요양기관기호 :";
                    lblFld2qty.Text = "산재지정기호 :";
                    break;

                case 1:
                    lblFld1qty.Text = "대표자 :";
                    lblFld2qty.Text = "대표자주민번호 :";
                    break;

                case 2:
                    lblFld1qty.Text = "작성자 :";
                    lblFld2qty.Text = "작성자주민번호 :";
                    lblFld3qty.Text = "산재청구인 :";
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
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

        private bool Save()
        {
            string exdt = txtMST4CD.Text;
            string fld1qty = txtFld1qty.Text;
            string fld2qty = txtFld2qty.Text;
            string fld3qty = txtFld3qty.Text;

            if (exdt == "")
            {
                MessageBox.Show("적용일이 없습니다.");
                return false;
            }

            DateTime applyDate;

            if (exdt.Length != 8 || DateTime.TryParseExact(exdt, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out applyDate) == false)
            {
                MessageBox.Show("적용일을 확인하세요.");
                return false;
            }

            string mst3cd = "";

            switch (m_mode)
            {
                case 0:
                    mst3cd = "2";
                    break;

                case 1:
                    mst3cd = "5";
                    break;

                case 2:
                    mst3cd = "48";
                    break;

                default:
                    MessageBox.Show("저장 모드를 확인하세요.");
                    return false;
            }

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT MST3CD";
                sql += Environment.NewLine + "  FROM TA88A";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                sql += Environment.NewLine + "   AND MST3CD=?";
                sql += Environment.NewLine + "   AND MST4CD=?";

                List<Object> para = new List<Object>();
                para.Add(mst3cd);
                para.Add(exdt);

                bool exists = false;

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    exists = true;

                    return MetroLib.SqlHelper.BREAK;
                });

                sql = "";
                para.Clear();

                if (exists == true)
                {
                    DialogResult result = MessageBox.Show(this, exdt + " 일자에 자료가 있습니다. 수정하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        sql += Environment.NewLine + "UPDATE TA88A";
                        sql += Environment.NewLine + "   SET FLD1QTY=?";
                        sql += Environment.NewLine + "     , FLD2QTY=?";
                        sql += Environment.NewLine + "     , FLD3QTY=?";
                        sql += Environment.NewLine + " WHERE MST1CD='A'";
                        sql += Environment.NewLine + "   AND MST2CD='HOSPITAL'";
                        sql += Environment.NewLine + "   AND MST3CD=?";
                        sql += Environment.NewLine + "   AND MST4CD=?";

                        para.Add(fld1qty);
                        para.Add(fld2qty);
                        para.Add(fld3qty);
                        para.Add(mst3cd);
                        para.Add(exdt);
                    }
                }
                else
                {
                    sql += Environment.NewLine + "INSERT INTO TA88A(MST1CD,MST2CD,MST3CD,MST4CD,FLD1QTY,FLD2QTY,FLD3QTY)";
                    sql += Environment.NewLine + "VALUES('A','HOSPITAL',?,?,?,?,?)";

                    para.Add(mst3cd);
                    para.Add(exdt);
                    para.Add(fld1qty);
                    para.Add(fld2qty);
                    para.Add(fld3qty);
                }

                MetroLib.SqlHelper.ExecuteSql(sql, para, conn, null);
            }

            return true;
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

    }
}

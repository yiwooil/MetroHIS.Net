using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9905E
{
    public partial class ADD9905E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private List<CList> m_MyList = new List<CList>();

        public ADD9905E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9905E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9905E_Load(object sender, EventArgs e)
        {
            InitMyList();
            SetCombo();
        }

        private void InitMyList()
        {
            CList list = new CList();
            list.KEYNO = "";
            list.CDNM = "";
            m_MyList.Add(list);

            list = new CList();
            list.KEYNO = "JJCD";
            list.CDNM = "조정사유";
            m_MyList.Add(list);

            list = new CList();
            list.KEYNO = "BULCD";
            list.CDNM = "불능사유";
            m_MyList.Add(list);
            
            list = new CList();
            list.KEYNO = "RSNCD";
            list.CDNM = "의약분업예외사유";
            m_MyList.Add(list);

            list = new CList();
            list.KEYNO = "TJCD1";
            list.CDNM = "특정내역(명단위)";
            m_MyList.Add(list);
            
            list = new CList();
            list.KEYNO = "TJCD2";
            list.CDNM = "특정내역(줄단위)";
            m_MyList.Add(list);
            
            list = new CList();
            list.KEYNO = "DAETC";
            list.CDNM = "상해외인";
            m_MyList.Add(list);
            
            list = new CList();
            list.KEYNO = "GONSGB";
            list.CDNM = "공상등구분";
            m_MyList.Add(list);
        }

        private void SetCombo()
        {
            cboMyList.Items.Clear();
            foreach (CList list in m_MyList)
            {
                cboMyList.Items.Add(list.CDNM);
            }
        }

        private void cboMyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //this.ShowProgressForm("", "자료검색 중입니다.");
                this.Query();
                //this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                //this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            grdMain.DataSource = null;

            int idx = cboMyList.SelectedIndex;
            if (m_MyList[idx].KEYNO == "") return;

            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string sql = "";
            sql="";
            sql += System.Environment.NewLine + "SELECT MST3CD, CDNM";
            sql += System.Environment.NewLine + "  FROM TI88 ";
            sql += System.Environment.NewLine + " WHERE MST1CD='A'";
            sql += System.Environment.NewLine + "   AND MST2CD=?";
            sql += System.Environment.NewLine + " ORDER BY MST3CD";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", m_MyList[idx].KEYNO));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.MST3CD = reader["MST3CD"].ToString();
                        data.CDNM = reader["CDNM"].ToString();

                        list.Add(data);

                    }
                    reader.Close();
                }

                conn.Close();
            }

            this.RefreshGridMain();

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

    }
}

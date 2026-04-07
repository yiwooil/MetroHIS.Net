using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0714E
{
    public partial class ADD0714E_01 : Form
    {
        public bool m_sel;
        public string m_demseq;

        private bool IsFirst;

        public ADD0714E_01()
        {
            InitializeComponent();
        }

        private void ADD0714E_01_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_sel = false;
        }

        private void ADD0714E_01_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            Query();
        }

        private void Query()
        {
            List<CDataDemseq> list = new List<CDataDemseq>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "SELECT * FROM TIE_F0611 WHERE VERSION='M' ORDER BY DEMSEQ DESC";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataDemseq data = new CDataDemseq();
                    data.Clear();
                    data.DEMSEQ = reader["DEMSEQ"].ToString();
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
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
            if (grdMainView.FocusedRowHandle < 0) return;
            if (grdMainView.FocusedRowHandle >= grdMainView.RowCount) return;
            m_sel = true;
            m_demseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMSEQ).ToString();
            this.Close();
        }
    }
}

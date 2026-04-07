using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7003E
{
    public partial class ADD7003E : Form
    {
        private bool IsFirst;

        private string m_User;
        private string m_Pwd;

        private string m_hosid;

        public ADD7003E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
        }

        public ADD7003E(String user,String pwd):this()
        {
            m_User = user;
            m_Pwd = pwd;
        }

        private void ADD7003E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            cboTrmTpCd.SelectedIndex = 1;
            cboDrgReqDataTpCd.SelectedIndex = 0;
            m_hosid = GetHosid();
        }

        private void ADD7003E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
        }

        private string GetHosid()
        {
            string ret = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='2'";
                CSQLHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    ret = reader["FLD1QTY"].ToString();

                    return false;
                });

                conn.Close();
            }
            return ret;
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
            string ykiho = m_hosid;
            string trmTpCd = (cboTrmTpCd.SelectedIndex + 1).ToString();
            string ymFrom = txtYmFrom.Text.ToString();
            string ymTo = txtYmTo.Text.ToString();
            string drgReqDataTpCd = "";
            if (cboDrgReqDataTpCd.SelectedIndex == 1) drgReqDataTpCd = "01";
            if (cboDrgReqDataTpCd.SelectedIndex == 2) drgReqDataTpCd = "05";
            if (cboDrgReqDataTpCd.SelectedIndex == 3) drgReqDataTpCd = "06";
            if (cboDrgReqDataTpCd.SelectedIndex == 4) drgReqDataTpCd = "07";
            string rcvNo = txtRcvNo.Text.ToString();

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;
            RefreshGridMain();

            if (chkTest.Checked == false)
            {
                HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();
                HIRA.EformEntry.ResponseModel.MasterListResponse mlresult = doc.selectNdrgMntrReqList(ykiho, trmTpCd, ymFrom, ymTo, drgReqDataTpCd, rcvNo, "", "1", "300");
                if (mlresult.Result)
                {
                    if (mlresult.Datas.Rows.Count == 0)
                    {
                        MessageBox.Show("해당 조건의 데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < mlresult.Datas.Rows.Count; i++)
                        {
                            CData data = new CData();
                            data.SetValues(mlresult.Datas.Rows[i]);
                            list.Add(data);
                        }

                        RefreshGridMain();
                    }

                    // 자료저장
                    string strConn = MetroLib.DBHelper.GetConnectionString();
                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {
                        conn.Open();

                        string sysdt = CUtil.GetSysDate(conn);
                        string systm = CUtil.GetSysTime(conn);

                        foreach (CData data in list)
                        {
                            data.SaveData(conn, sysdt, sysdt, m_User);
                        }

                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("조회오류\n" + mlresult.ErrorCode + "\n" + mlresult.ErrorMessage);
                }
            }
            else
            {
                if (txtDemno.Text.ToString() != "")
                {
                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT *";
                    sql += Environment.NewLine + "  FROM TI2A A";
                    sql += Environment.NewLine + " WHERE A.DEMNO='" + txtDemno.Text.ToString() + "'";
                    sql += Environment.NewLine + " ORDER BY A.EPRTNO";

                    string strConn = MetroLib.DBHelper.GetConnectionString();
                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {
                        conn.Open();

                        CSQLHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                        {
                            CData data = new CData();
                            data.SetValues(reader);
                            list.Add(data);

                            return true;
                        });
                        RefreshGridMain();

                        // 자료저장
                        string sysdt = CUtil.GetSysDate(conn);
                        string systm = CUtil.GetSysTime(conn);

                        foreach (CData data in list)
                        {
                            data.SaveData(conn, sysdt, sysdt, m_User);
                        }

                        conn.Close();
                    }
                }
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

        private void btnEditInGrdMain_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ADD7003E_1 f = new ADD7003E_1();
            f.ShowData(this, chkTest.Checked);
            f.ShowDialog(this);
        }

        public bool GetPtnt(int dir, ref string p_hosid, ref string p_dmd_no, ref string p_sp_sno, ref string p_rcv_no, ref string p_rcv_yr, ref string p_bill_sno, ref string p_insup_tp_cd, ref string p_req_data_no)
        {
            // dir = 0 : 현재
            //      -1 : 이전
            //      +1 : 다음
            if (dir < 0)
            {
                int rowHandle = grdMainView.FocusedRowHandle;
                if (rowHandle <= 0) return false;

                grdMainView.FocusedRowHandle--;
            }
            else if (dir > 0)
            {
                int rowHandle = grdMainView.FocusedRowHandle;
                if (rowHandle >= grdMainView.RowCount - 1) return false;

                grdMainView.FocusedRowHandle++;
            }

            p_hosid = m_hosid;
            p_dmd_no = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEM_NO).ToString(); // 청구번호
            p_sp_sno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcSP_SNO).ToString(); // 명일련
            p_req_data_no = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcREQ_DATA_NO).ToString(); // 자료요청번호
            p_rcv_no = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcRCV_NO).ToString(); // 접수번호
            p_rcv_yr = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcRCV_YR).ToString(); // 접수년도
            p_bill_sno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcBILL_SNO).ToString(); // 청일련
            p_insup_tp_cd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcINSUP_TP_CD).ToString(); // 보험자구분

            return true;
        }

        private void chkTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTest.Checked)
            {
                if (txtDemno.Text.ToString() == "") txtDemno.Text = "2021051201";
            }
        }

    }
}

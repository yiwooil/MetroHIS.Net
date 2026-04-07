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
    public partial class ADD7007_ASM024_002 : Form
    {
        class IPAT
        {
            public string SPRM_IPAT_DT_DATE { get; set; } // 입실일자
            public string SPRM_IPAT_DT_TIME { get; set; } // 입실시간
            public string SPRM_DSCG_DT_DATE { get; set; } // 퇴실일자
            public string SPRM_DSCG_DT_TIME { get; set; } // 퇴실시간
            public string ASM_SPRM_DSCG_RST_CD { get; set; } // 퇴실현황
            public string SPRM_RE_IPAT_PLAN_YN { get; set; } // 재입실 계획여부
            public string SPRM_IPAT_RS_CD { get; set; } // 입실사유
            public string IPAT_TEXT { get; set; } // 입실사유상세
            public string RE_IPAT_RS_TXT { get { return SPRM_IPAT_RS_CD == "4" ? IPAT_TEXT : ""; } } // 입실사유 재입실 상세
            public string IPAT_RS_ETC_TXT { get { return SPRM_IPAT_RS_CD == "9" ? IPAT_TEXT : ""; } } // 입실사유 기타 상세

            public IPAT()
            {
                SPRM_IPAT_DT_DATE = ""; // 입실일자
                SPRM_IPAT_DT_TIME = ""; // 입실시간
                SPRM_DSCG_DT_DATE = ""; // 퇴실일자
                SPRM_DSCG_DT_TIME = ""; // 퇴실시간
                ASM_SPRM_DSCG_RST_CD = ""; // 퇴실현황
                SPRM_RE_IPAT_PLAN_YN = ""; // 재입실 계획여부
                SPRM_IPAT_RS_CD = ""; // 입실사유
                IPAT_TEXT = ""; // 입실사유 재입실 상세 or 입실사유기타상세
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM024_002 m_data;

        public ADD7007_ASM024_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboInGrid();
        }

        public ADD7007_ASM024_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM024_002> list = (List<CDataASM024_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            // 퇴원현황
            CUtil.SetGridCombo(grdIPATView.Columns["ASM_SPRM_DSCG_RST_CD"],
                "",
                "전실(일반병동)",
                "타병원 전원",
                "사망",
                "뇌사판정(이식)",
                "계속 입실",
                "퇴원(요양원 등 포함)"
                );

            // 재입실계획여부
            CUtil.SetGridCombo(grdIPATView.Columns["SPRM_RE_IPAT_PLAN_YN"],
                "",
                "Yes",
                "No"
                );

            // 입실사유
            CUtil.SetGridCombo(grdIPATView.Columns["SPRM_IPAT_RS_CD"],
                "",
                "상태 악화되어 집중관찰",
                "특수한 치료 또는 관리가 필요한 경우",
                "수술이나 시술 후 집중관찰",
                "의료진의 계획에 따라 예정된 재입실",
                "기타"
                );
        }

        private string GetASM_SPRM_DSCG_RST_CD_NM(string p_value)
        {
            // 퇴실현황
            if (p_value == "1") return "전실(일반병동)";
            if (p_value == "2") return "타병원 전원";
            if (p_value == "3") return "사망";
            if (p_value == "4") return "뇌사판정(이식)";
            if (p_value == "5") return "계속 입실";
            if (p_value == "6") return "퇴원(요양원 등 포함)";
            return "";
        }

        private string GetASM_SPRM_DSCG_RST_CD(string p_value)
        {
            // 퇴실현황
            if (p_value == "전실(일반병동)") return "1";
            if (p_value == "타병원 전원") return "2";
            if (p_value == "사망") return "3";
            if (p_value == "뇌사판정(이식)") return "4";
            if (p_value == "계속 입실") return "5";
            if (p_value == "퇴원(요양원 등 포함)") return "6";
            return "";
        }

        private string GetSPRM_RE_IPAT_PLAN_YN_NM(string p_value)
        {
            // 재입실 계획여부
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetSPRM_RE_IPAT_PLAN_YN(string p_value)
        {
            // 재입실 계획여부
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private string GetSPRM_IPAT_RS_CD_NM(string p_value)
        {
            // 입실사유
            if (p_value == "1") return "상태 악화되어 집중관찰";
            if (p_value == "2") return "특수한 치료 또는 관리가 필요한 경우";
            if (p_value == "3") return "수술이나 시술 후 집중관찰";
            if (p_value == "4") return "의료진의 계획에 따라 예정된 재입실";
            if (p_value == "9") return "기타";
            return "";
        }

        private string GetSPRM_IPAT_RS_CD(string p_value)
        {
            // 입실사유
            if (p_value == "상태 악화되어 집중관찰") return "1";
            if (p_value == "특수한 치료 또는 관리가 필요한 경우") return "2";
            if (p_value == "수술이나 시술 후 집중관찰") return "3";
            if (p_value == "의료진의 계획에 따라 예정된 재입실") return "4";
            if (p_value == "기타") return "9";
            return "";
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // A.기본 정보
            txtIPAT_DD.Text = m_data.BDEDT;

            // B.중환자실 입퇴실정보
            List<IPAT> list = new List<IPAT>();
            grdIPAT.DataSource = null;
            grdIPAT.DataSource = list;
            for (int i = 0; i < m_data.SPRM_IPAT_DT.Count; i++)
            {
                IPAT data = new IPAT();
                data.SPRM_IPAT_DT_DATE = CUtil.GetDate(m_data.SPRM_IPAT_DT[i]); // 입실일자
                data.SPRM_IPAT_DT_TIME = CUtil.GetTime(m_data.SPRM_IPAT_DT[i]); // 입실시간
                data.SPRM_DSCG_DT_DATE = CUtil.GetDate(m_data.SPRM_DSCG_DT[i]); // 퇴실일자
                data.SPRM_DSCG_DT_TIME = CUtil.GetTime(m_data.SPRM_DSCG_DT[i]); // 퇴실시간
                data.ASM_SPRM_DSCG_RST_CD = GetASM_SPRM_DSCG_RST_CD_NM(m_data.ASM_SPRM_DSCG_RST_CD[i]); // 퇴실현황
                data.SPRM_RE_IPAT_PLAN_YN = GetSPRM_RE_IPAT_PLAN_YN_NM(m_data.SPRM_RE_IPAT_PLAN_YN[i]); // 재입실 계획여부
                data.SPRM_IPAT_RS_CD = GetSPRM_IPAT_RS_CD_NM(m_data.SPRM_IPAT_RS_CD[i]); // 입실사유

                // 입실사유상세
                if (m_data.SPRM_IPAT_RS_CD[i] == "4")
                {
                    data.IPAT_TEXT = m_data.RE_IPAT_RS_TXT[i]; // 입실사유 재입실 상세
                }
                else if (m_data.SPRM_IPAT_RS_CD[i] == "9")
                {
                    data.IPAT_TEXT = m_data.IPAT_RS_ETC_TXT[i]; // 입실사유 기타 상세
                }
                else
                {
                    data.IPAT_TEXT = ""; // 
                }
                

                list.Add(data);
            }

            // C.사망 현황
            rbASM_DEATH_YN_1.Checked = m_data.ASM_DEATH_YN == "1";
            rbASM_DEATH_YN_2.Checked = m_data.ASM_DEATH_YN == "2";
            txtDEATH_DT_DATE.Text = CUtil.GetDate(m_data.DEATH_DT);
            txtDEATH_DT_TIME.Text = CUtil.GetTime(m_data.DEATH_DT);
            rbWLST_RCD_YN_1.Checked = m_data.WLST_RCD_YN == "1";
            rbWLST_RCD_YN_2.Checked = m_data.WLST_RCD_YN == "2";
            txtWLST_RCD_DT.Text = m_data.WLST_RCD_DT;

            string[] record_cd = m_data.WLST_RCD_CD.Split('/');
            chkWLST_RCD_CD_1.Checked = record_cd.Contains("1");
            chkWLST_RCD_CD_2.Checked = record_cd.Contains("2");
            chkWLST_RCD_CD_3.Checked = record_cd.Contains("3");
            chkWLST_RCD_CD_4.Checked = record_cd.Contains("4");
            chkWLST_RCD_CD_5.Checked = record_cd.Contains("5");
            chkWLST_RCD_CD_6.Checked = record_cd.Contains("6");
            chkWLST_RCD_CD_7.Checked = record_cd.Contains("7");
            chkWLST_RCD_CD_9.Checked = record_cd.Contains("9");

            txtWLST_RCD_ETC_TXT.Text = m_data.WLST_RCD_ETC_TXT;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdIPAT, grdIPATView);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // A.기본 정보
            m_data.IPAT_DD = txtIPAT_DD.Text.ToString();

            // B.중환자실 입퇴실정보
            m_data.SPRM_IPAT_DT.Clear(); // 입실일시
            m_data.SPRM_IPAT_RS_CD.Clear(); // 입실사유
            m_data.RE_IPAT_RS_TXT.Clear(); // 재입실상세
            m_data.IPAT_RS_ETC_TXT.Clear(); // 입실사유기타
            m_data.ASM_SPRM_DSCG_RST_CD.Clear(); // 퇴실현황
            m_data.SPRM_DSCG_DT.Clear(); // 퇴실일시
            m_data.SPRM_RE_IPAT_PLAN_YN.Clear(); // 재입실 계획여부

            List<IPAT> list = (List<IPAT>)grdIPAT.DataSource;
            foreach (IPAT data in list)
            {
                m_data.SPRM_IPAT_DT.Add(CUtil.GetDateTime(data.SPRM_IPAT_DT_DATE, data.SPRM_IPAT_DT_TIME)); // 입실일시
                m_data.SPRM_IPAT_RS_CD.Add(GetSPRM_IPAT_RS_CD(data.SPRM_IPAT_RS_CD)); // 입실사유
                m_data.RE_IPAT_RS_TXT.Add(data.RE_IPAT_RS_TXT); // 재입실상세
                m_data.IPAT_RS_ETC_TXT.Add(data.IPAT_RS_ETC_TXT); // 입실사유기타
                m_data.ASM_SPRM_DSCG_RST_CD.Add(GetASM_SPRM_DSCG_RST_CD(data.ASM_SPRM_DSCG_RST_CD)); // 퇴실현황
                m_data.SPRM_DSCG_DT.Add(data.SPRM_DSCG_DT_DATE + data.SPRM_DSCG_DT_TIME); // 퇴실일시
                m_data.SPRM_RE_IPAT_PLAN_YN.Add(GetSPRM_RE_IPAT_PLAN_YN(data.SPRM_RE_IPAT_PLAN_YN)); // 재입실 계획여부
            }

            // C.사망 현황
            m_data.ASM_DEATH_YN = CUtil.GetRBString(rbASM_DEATH_YN_1, rbASM_DEATH_YN_2);
            m_data.DEATH_DT = CUtil.GetDateTime(txtDEATH_DT_DATE.Text.ToString(), txtDEATH_DT_TIME.Text.ToString());
            m_data.WLST_RCD_YN = CUtil.GetRBString(rbWLST_RCD_YN_1, rbWLST_RCD_YN_2);
            m_data.WLST_RCD_DT = txtWLST_RCD_DT.Text;

            m_data.WLST_RCD_CD = "";
            if (chkWLST_RCD_CD_1.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "1";
            if (chkWLST_RCD_CD_2.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "2";
            if (chkWLST_RCD_CD_3.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "3";
            if (chkWLST_RCD_CD_4.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "4";
            if (chkWLST_RCD_CD_5.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "5";
            if (chkWLST_RCD_CD_6.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "6";
            if (chkWLST_RCD_CD_7.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "7";
            if (chkWLST_RCD_CD_9.Checked == true) m_data.WLST_RCD_CD += (m_data.WLST_RCD_CD != "" ? "/" : "") + "9";

            m_data.WLST_RCD_ETC_TXT = txtWLST_RCD_ETC_TXT.Text;

            // 데이터 베이스에 저장한다.
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sysdt = MetroLib.Util.GetSysDate(conn, tran);
                    string systm = MetroLib.Util.GetSysTime(conn, tran);

                    m_data.UpdData(sysdt, systm, m_User, conn, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show(ex.Message);
                    //throw ex;
                }
            }
        }

        private void btnInsRow_Click(object sender, EventArgs e)
        {
            List<IPAT> list = (List<IPAT>)grdIPAT.DataSource;
            list.Add(new IPAT());
            RefreshGrid();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdIPATView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<IPAT> list = (List<IPAT>)grdIPAT.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_view.FocusedRowHandle == 0)
            {
                // 첫 줄은 아무런 동작도 하지 앟는다.
                MessageBox.Show("처음 자료입니다.");
            }
            else
            {
                m_view.FocusedRowHandle--;
                List<CDataASM024_002> list = (List<CDataASM024_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_view.FocusedRowHandle >= m_view.RowCount - 1)
            {
                // 마지막 줄은 아무런 동작도 하지 앟는다.
                MessageBox.Show("마지막 자료입니다.");
            }
            else
            {
                m_view.FocusedRowHandle++;
                List<CDataASM024_002> list = (List<CDataASM024_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

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
            if (MessageBox.Show((isTmp ? "임시 " : "") + "전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Send(isTmp);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Send(bool isTmp)
        {
            CHiraEForm hira = new CHiraEForm();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                hira.Send(m_data, isTmp, sysdt, systm, m_User, m_Hosid, conn);

            }
            RefreshGrid();
        }

        private void btnReQuery_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("자료 다시 생성을 하면 저장한 자료등이 모두 초기화됩니다." + Environment.NewLine + Environment.NewLine + "계속하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ReQuery();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void ReQuery()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;

                try
                {
                    conn.Open();

                    string sysdt = MetroLib.Util.GetSysDate(conn);
                    string systm = MetroLib.Util.GetSysTime(conn);

                    CMakeASM024 make = new CMakeASM024();
                    tran = conn.BeginTransaction();
                    make.MakeASM024(m_data, sysdt, systm, m_User, conn, tran, true);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show(ex.Message);
                }

                ShowData();
                RefreshGrid();
            }
        }

        private void btnExcept_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("전송제외하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Except();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Except()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                m_data.Except_ASM000(sysdt, systm, m_User, conn);
            }
            RefreshGrid();
        }

    }
}

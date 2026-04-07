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
    public partial class ADD7007_ASM014_001 : Form
    {
        public class TRFR
        {
            public string TRFR_DD { get; set; }
            public string MVOT_DGSBJT_CD { get; set; }
            public string MVIN_DGSBJT_CD { get; set; }
            public TRFR()
            {
                TRFR_DD = "";
                MVOT_DGSBJT_CD = "";
                MVIN_DGSBJT_CD = "";
            }
        }

        public class QLF
        {
            public string QLF_CHG_DD { get; set; }
            public QLF()
            {
                QLF_CHG_DD = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM014_001 m_data;

        public ADD7007_ASM014_001()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM014_001(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM014_001> list = (List<CDataASM014_001>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 1. 퇴원상태 콤보박스 (cboDSCG_STAT_CD)
            cboDSCG_STAT_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "호전 퇴원" },
                new { Key = "2", Value = "치료거부 퇴원" },
                new { Key = "3", Value = "가망없는 퇴원" },
                new { Key = "4", Value = "타병원 전원" },
                new { Key = "5", Value = "사망" }
            };
            cboDSCG_STAT_CD.DisplayMember = "Value";
            cboDSCG_STAT_CD.ValueMember = "Key";

            // 2. 지역사회서비스 미의뢰사유 콤보박스 (cboPLC_SCTY_SVC_CONN_NREQ_RS_CD)
            cboPLC_SCTY_SVC_CONN_NREQ_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "연계 의뢰 거부" },
                new { Key = "2", Value = "환자요인으로 인한 각종 미귀원" },
                new { Key = "3", Value = "기록없음" },
                new { Key = "9", Value = "기타" }
            };
            cboPLC_SCTY_SVC_CONN_NREQ_RS_CD.DisplayMember = "Value";
            cboPLC_SCTY_SVC_CONN_NREQ_RS_CD.ValueMember = "Key";

            // 3. 퇴원경험도 미시행 사유 콤보박스 (cboDSCG_EXPR_NOPER_RS_CD)
            cboDSCG_EXPR_NOPER_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "의학적 권고에 반하는 퇴원" },
                new { Key = "2", Value = "설문지 거부 또는 무응답" },
                new { Key = "3", Value = "환자요인으로 인한 미귀원" },
                new { Key = "4", Value = "기록없음" },
                new { Key = "9", Value = "기타" }
            };
            cboDSCG_EXPR_NOPER_RS_CD.DisplayMember = "Value";
            cboDSCG_EXPR_NOPER_RS_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            // GRID에 콤보 컬럼을 만든다.
        }

        //private string GetDSCG_STAT_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "호전 퇴원";
        //    if (p_value == "2") return "치료거부 퇴원";
        //    if (p_value == "3") return "가망없는 퇴원";
        //    if (p_value == "4") return "타병원 전원";
        //    if (p_value == "5") return "사망";
        //    return "";
        //}

        //private string GetDSCG_STAT_CD(string p_value)
        //{
        //    if (p_value == "호전 퇴원") return "1";
        //    if (p_value == "치료거부 퇴원") return "2";
        //    if (p_value == "가망없는 퇴원") return "3";
        //    if (p_value == "타병원 전원") return "4";
        //    if (p_value == "사망") return "5";
        //    return "";
        //}

        //private string GetPLC_SCTY_SVC_CONN_NREQ_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "연계 의뢰 거부";
        //    if (p_value == "2") return "환자요인으로 인한 각종 미귀원";
        //    if (p_value == "3") return "기록없음";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetPLC_SCTY_SVC_CONN_NREQ_RS_CD(string p_value)
        //{
        //    if (p_value == "연계 의뢰 거부") return "1";
        //    if (p_value == "환자요인으로 인한 각종 미귀원") return "2";
        //    if (p_value == "기록없음") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetDSCG_EXPR_NOPER_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "의학적 권고에 반하는 퇴원";
        //    if (p_value == "2") return "설문지 거부 또는 무응답";
        //    if (p_value == "3") return "환자요인으로 인한 미귀원";
        //    if (p_value == "4") return "기록없음";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetDSCG_EXPR_NOPER_RS_CD(string p_value)
        //{
        //    if (p_value == "의학적 권고에 반하는 퇴원") return "1";
        //    if (p_value == "설문지 거부 또는 무응답") return "2";
        //    if (p_value == "환자요인으로 인한 미귀원") return "3";
        //    if (p_value == "기록없음") return "4";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // A. 기본 정보
            rbTRFR_YN_1.Checked = m_data.TRFR_YN == "1";  // 전과 여부: Yes
            rbTRFR_YN_2.Checked = m_data.TRFR_YN == "2";  // 전과 여부: No

            // 전과일자 리스트 → Grid 바인딩
            var trfrRows = new List<TRFR>();
            for (int i = 0; i < m_data.TRFR_DD.Count; i++)
            {
                var row = new TRFR();
                row.TRFR_DD = m_data.TRFR_DD[i];
                row.TRFR_DD = m_data.MVOT_DGSBJT_CD[i];
                row.TRFR_DD = m_data.MVIN_DGSBJT_CD[i];
                trfrRows.Add(row); // 서브 클래스 TRFR 생성
            }
            grdTRFR.DataSource = trfrRows;

            rbINSUP_QLF_CHG_YN_1.Checked = m_data.INSUP_QLF_CHG_YN == "1"; // 자격변동 여부: Yes
            rbINSUP_QLF_CHG_YN_2.Checked = m_data.INSUP_QLF_CHG_YN == "2"; // 자격변동 여부: No

            // 자격변동일자 리스트 → Grid 바인딩
            var qlfRows = new List<QLF>();
            foreach (var item in m_data.QLF_CHG_DD)
            {
                qlfRows.Add(new QLF { QLF_CHG_DD = item }); // 서브 클래스 QLF 생성
            }
            grdQLF.DataSource = qlfRows;

            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1";  // 퇴원 여부: Yes
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2";  // 퇴원 여부: No

            txtDSCG_DD.Text = m_data.DSCG_DD;            // 퇴원일자
            CUtil.SetComboboxSelectedValue(cboDSCG_STAT_CD, m_data.DSCG_STAT_CD); // 퇴원 상태

            rbSPNT_IPAT_YN_1.Checked = m_data.SPNT_IPAT_YN == "1"; // 자발적 입원 여부: Yes
            rbSPNT_IPAT_YN_2.Checked = m_data.SPNT_IPAT_YN == "2"; // 자발적 입원 여부: No

            // B. 지역사회서비스 연계
            rbDSCG_POTM_PSYCHI_DS_DIAG_YN_1.Checked = m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN == "1"; // 조현병 여부: Yes
            rbDSCG_POTM_PSYCHI_DS_DIAG_YN_2.Checked = m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN == "2"; // 조현병 여부: No

            rbPLC_SCTY_SVC_CONN_REQ_YN_1.Checked = m_data.PLC_SCTY_SVC_CONN_REQ_YN == "1"; // 의뢰 여부 Yes
            rbPLC_SCTY_SVC_CONN_REQ_YN_2.Checked = m_data.PLC_SCTY_SVC_CONN_REQ_YN == "2"; // 의뢰 여부 No

            CUtil.SetComboboxSelectedValue(cboPLC_SCTY_SVC_CONN_NREQ_RS_CD, m_data.PLC_SCTY_SVC_CONN_NREQ_RS_CD); // 미의뢰 사유
            txtPLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT.Text = m_data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT; // 기타 상세

            // C. 퇴원 시 환자경험도 조사
            rbDSCG_EXPR_INVT_ENFC_YN_1.Checked = m_data.DSCG_EXPR_INVT_ENFC_YN == "1"; // 경험도조사 수행 여부
            rbDSCG_EXPR_INVT_ENFC_YN_2.Checked = m_data.DSCG_EXPR_INVT_ENFC_YN == "2";

            CUtil.SetComboboxSelectedValue(cboDSCG_EXPR_NOPER_RS_CD, m_data.DSCG_EXPR_NOPER_RS_CD); // 미시행 사유
            txtNOPER_RS_ETC_TXT.Text = m_data.NOPER_RS_ETC_TXT; // 경험도조사 기타 상세입력

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdTRFR, grdTRFRView);
            CUtil.RefreshGrid(grdQLF, grdQLFView);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // A. 기본 정보 입력값 수집
            m_data.TRFR_YN = CUtil.GetRBString(rbTRFR_YN_1, rbTRFR_YN_2);             // 전과 여부

            m_data.TRFR_DD.Clear();
            m_data.MVOT_DGSBJT_CD.Clear();
            m_data.MVIN_DGSBJT_CD.Clear();

            foreach (TRFR row in (List<TRFR>)grdTRFR.DataSource)
            {
                m_data.TRFR_DD.Add(row.TRFR_DD);
                m_data.MVOT_DGSBJT_CD.Add(row.MVOT_DGSBJT_CD);
                m_data.MVIN_DGSBJT_CD.Add(row.MVIN_DGSBJT_CD);
            }

            m_data.INSUP_QLF_CHG_YN = CUtil.GetRBString(rbINSUP_QLF_CHG_YN_1, rbINSUP_QLF_CHG_YN_2); // 자격변동 여부

            m_data.QLF_CHG_DD.Clear();
            foreach (QLF row in (List<QLF>)grdQLF.DataSource)
            {
                m_data.QLF_CHG_DD.Add(row.QLF_CHG_DD);
            }

            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2);             // 퇴원 여부
            m_data.DSCG_DD = txtDSCG_DD.Text.Trim();                                  // 퇴원일자
            m_data.DSCG_STAT_CD = CUtil.GetComboboxSelectedValue(cboDSCG_STAT_CD); // 퇴원상태 코드
            m_data.SPNT_IPAT_YN = CUtil.GetRBString(rbSPNT_IPAT_YN_1, rbSPNT_IPAT_YN_2); // 자발적 입원 여부

            // B. 지역사회 연계 정보
            m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN = CUtil.GetRBString(rbDSCG_POTM_PSYCHI_DS_DIAG_YN_1, rbDSCG_POTM_PSYCHI_DS_DIAG_YN_2);
            m_data.PLC_SCTY_SVC_CONN_REQ_YN = CUtil.GetRBString(rbPLC_SCTY_SVC_CONN_REQ_YN_1, rbPLC_SCTY_SVC_CONN_REQ_YN_2);
            m_data.PLC_SCTY_SVC_CONN_NREQ_RS_CD = CUtil.GetComboboxSelectedValue(cboPLC_SCTY_SVC_CONN_NREQ_RS_CD);
            m_data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = txtPLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT.Text;

            // C. 환자경험도조사
            m_data.DSCG_EXPR_INVT_ENFC_YN = CUtil.GetRBString(rbDSCG_EXPR_INVT_ENFC_YN_1, rbDSCG_EXPR_INVT_ENFC_YN_2);
            m_data.DSCG_EXPR_NOPER_RS_CD = CUtil.GetComboboxSelectedValue(cboDSCG_EXPR_NOPER_RS_CD);
            m_data.NOPER_RS_ETC_TXT = txtNOPER_RS_ETC_TXT.Text;

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
                List<CDataASM014_001> list = (List<CDataASM014_001>)m_view.DataSource;
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
                List<CDataASM014_001> list = (List<CDataASM014_001>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowTRFR_Click(object sender, EventArgs e)
        {
            List<TRFR> list = (List<TRFR>)grdTRFR.DataSource;
            list.Add(new TRFR());
            RefreshGrid();
        }

        private void btnDelRowTRFR_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdTRFRView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<TRFR> list = (List<TRFR>)grdTRFR.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowQLF_Click(object sender, EventArgs e)
        {
            List<QLF> list = (List<QLF>)grdQLF.DataSource;
            list.Add(new QLF());
            RefreshGrid();
        }

        private void btnDelRowQLF_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdQLFView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<QLF> list = (List<QLF>)grdQLF.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
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

                    CMakeASM014 make = new CMakeASM014();
                    tran = conn.BeginTransaction();
                    make.MakeASM014(m_data, sysdt, systm, m_User, conn, tran, true);
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

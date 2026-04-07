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
    public partial class ADD7007_ASM036_002 : Form
    {
        public class TRFR
        {
            public string TRFR_DD { get; set; }
            public TRFR()
            {
                TRFR_DD = "";
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

        public class FCLT
        {
            public string FCLT_ASM_TL_ENFC_DD { get; set; }
            public string PSYCHI_HTH_FCLT_ASM_TL_CD { get; set; }
            public string WHO12_FCLT_ASM_PNT { get; set; }
            public string WHO36_FCLT_ASM_PNT { get; set; }
            public string HONOS_FCLT_ASM_PNT { get; set; }
            public string GAF_FCLT_ASM_PNT { get; set; }
            public string CGI_FCLT_ASM_PNT { get; set; }
            public FCLT()
            {
                FCLT_ASM_TL_ENFC_DD = "";
                PSYCHI_HTH_FCLT_ASM_TL_CD = "";
                WHO12_FCLT_ASM_PNT = "";
                WHO36_FCLT_ASM_PNT = "";
                HONOS_FCLT_ASM_PNT = "";
                GAF_FCLT_ASM_PNT = "";
                CGI_FCLT_ASM_PNT = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM036_002 m_data;

        public ADD7007_ASM036_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM036_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM036_002> list = (List<CDataASM036_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 퇴원상태
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

            // 미의뢰사유
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

            // 미시행사유
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
            txtFST_IPAT_DD.Text = m_data.FST_IPAT_DD;     // 최초 입원일자
            txtIPAT_DGSBJT_CD.Text = m_data.IPAT_DGSBJT_CD; // 입원 당시 진료과목 코드

            rbTRFR_YN_1.Checked = m_data.TRFR_YN == "1";  // 전과 여부: Yes
            rbTRFR_YN_2.Checked = m_data.TRFR_YN == "2";  // 전과 여부: No

            // 전과일자 리스트 → Grid 바인딩
            var trfrRows = new List<TRFR>();
            foreach (var item in m_data.TRFR_DD)
            {
                trfrRows.Add(new TRFR { TRFR_DD = item }); // 서브 클래스 TRFR 생성
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

            txtDEATH_DD.Text = m_data.DEATH_DD;          // 사망일 (필요 시)
            rbSPNT_IPAT_YN_1.Checked = m_data.SPNT_IPAT_YN == "1"; // 자발적 입원 여부: Yes
            rbSPNT_IPAT_YN_2.Checked = m_data.SPNT_IPAT_YN == "2"; // 자발적 입원 여부: No

            // B. 기능평가
            rbFCLT_ASM_YN_1.Checked = m_data.FCLT_ASM_YN == "1"; // 기능평가 수행 여부: Yes
            rbFCLT_ASM_YN_2.Checked = m_data.FCLT_ASM_YN == "2"; // 기능평가 수행 여부: No

            // 기능평가 상세 Grid 바인딩
            var fcltRows = new List<FCLT>();
            for (int i = 0; i < m_data.FCLT_ASM_TL_ENFC_DD.Count; i++)
            {
                var row = new FCLT
                {
                    FCLT_ASM_TL_ENFC_DD = m_data.FCLT_ASM_TL_ENFC_DD[i],                      // 시행일자
                    PSYCHI_HTH_FCLT_ASM_TL_CD = m_data.PSYCHI_HTH_FCLT_ASM_TL_CD[i],          // 도구코드
                    WHO12_FCLT_ASM_PNT = m_data.WHO12_FCLT_ASM_PNT[i], // WHODAS 2.0 12개 문항 점수
                    WHO36_FCLT_ASM_PNT = m_data.WHO36_FCLT_ASM_PNT[i], // WHODAS 2.0 36개 문항 점수
                    HONOS_FCLT_ASM_PNT = m_data.HONOS_FCLT_ASM_PNT[i], // HoNOS 점수
                    GAF_FCLT_ASM_PNT = m_data.GAF_FCLT_ASM_PNT[i], // GAF 점수
                    CGI_FCLT_ASM_PNT = m_data.CGI_FCLT_ASM_PNT[i]  // CGI 점수
                };
                fcltRows.Add(row);
            }
            grdFCLT.DataSource = fcltRows;

            // C. 지역사회서비스 연계
            rbDSCG_POTM_PSYCHI_DS_DIAG_YN_1.Checked = m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN == "1"; // 조현병 여부: Yes
            rbDSCG_POTM_PSYCHI_DS_DIAG_YN_2.Checked = m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN == "2"; // 조현병 여부: No

            rbPLC_SCTY_SVC_CONN_REQ_YN_1.Checked = m_data.PLC_SCTY_SVC_CONN_REQ_YN == "1"; // 의뢰 여부 Yes
            rbPLC_SCTY_SVC_CONN_REQ_YN_2.Checked = m_data.PLC_SCTY_SVC_CONN_REQ_YN == "2"; // 의뢰 여부 No

            CUtil.SetComboboxSelectedValue(cboPLC_SCTY_SVC_CONN_NREQ_RS_CD, m_data.PLC_SCTY_SVC_CONN_NREQ_RS_CD); // 미의뢰 사유
            txtPLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT.Text = m_data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT; // 기타 상세

            // D. 퇴원 시 환자경험도 조사
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
            CUtil.RefreshGrid(grdFCLT, grdFCLTView);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // A. 기본 정보 입력값 수집
            m_data.FST_IPAT_DD = txtFST_IPAT_DD.Text.Trim();                           // 최초 입원일자
            m_data.IPAT_DGSBJT_CD = txtIPAT_DGSBJT_CD.Text.Trim();                    // 진료과
            m_data.TRFR_YN = CUtil.GetRBString(rbTRFR_YN_1, rbTRFR_YN_2);             // 전과 여부

            m_data.TRFR_DD.Clear();
            foreach (TRFR row in (List<TRFR>)grdTRFR.DataSource)
            {
                m_data.TRFR_DD.Add(row.TRFR_DD);
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
            m_data.DEATH_DD = txtDEATH_DD.Text.Trim();                                // 사망일자
            m_data.SPNT_IPAT_YN = CUtil.GetRBString(rbSPNT_IPAT_YN_1, rbSPNT_IPAT_YN_2); // 자발적 입원 여부

            // B. 기능평가 정보 수집
            m_data.FCLT_ASM_YN = CUtil.GetRBString(rbFCLT_ASM_YN_1, rbFCLT_ASM_YN_2);  // 기능평가 여부

            m_data.FCLT_ASM_TL_ENFC_DD.Clear();
            m_data.PSYCHI_HTH_FCLT_ASM_TL_CD.Clear();
            m_data.WHO12_FCLT_ASM_PNT.Clear();
            m_data.WHO36_FCLT_ASM_PNT.Clear();
            m_data.HONOS_FCLT_ASM_PNT.Clear();
            m_data.GAF_FCLT_ASM_PNT.Clear();
            m_data.CGI_FCLT_ASM_PNT.Clear();

            foreach (FCLT row in (List<FCLT>)grdFCLT.DataSource)
            {
                m_data.FCLT_ASM_TL_ENFC_DD.Add(row.FCLT_ASM_TL_ENFC_DD);
                m_data.PSYCHI_HTH_FCLT_ASM_TL_CD.Add(row.PSYCHI_HTH_FCLT_ASM_TL_CD);
                m_data.WHO12_FCLT_ASM_PNT.Add(row.WHO12_FCLT_ASM_PNT);
                m_data.WHO36_FCLT_ASM_PNT.Add(row.WHO36_FCLT_ASM_PNT);
                m_data.HONOS_FCLT_ASM_PNT.Add(row.HONOS_FCLT_ASM_PNT);
                m_data.GAF_FCLT_ASM_PNT.Add(row.GAF_FCLT_ASM_PNT);
                m_data.CGI_FCLT_ASM_PNT.Add(row.CGI_FCLT_ASM_PNT);
            }

            // C. 지역사회 연계 정보
            m_data.DSCG_POTM_PSYCHI_DS_DIAG_YN = CUtil.GetRBString(rbDSCG_POTM_PSYCHI_DS_DIAG_YN_1, rbDSCG_POTM_PSYCHI_DS_DIAG_YN_2);
            m_data.PLC_SCTY_SVC_CONN_REQ_YN = CUtil.GetRBString(rbPLC_SCTY_SVC_CONN_REQ_YN_1, rbPLC_SCTY_SVC_CONN_REQ_YN_2);
            m_data.PLC_SCTY_SVC_CONN_NREQ_RS_CD = CUtil.GetComboboxSelectedValue(cboPLC_SCTY_SVC_CONN_NREQ_RS_CD);
            m_data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = txtPLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT.Text;

            // D. 환자경험도조사
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
                List<CDataASM036_002> list = (List<CDataASM036_002>)m_view.DataSource;
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
                List<CDataASM036_002> list = (List<CDataASM036_002>)m_view.DataSource;
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

        private void btnInsRowFCLT_Click(object sender, EventArgs e)
        {
            List<FCLT> list = (List<FCLT>)grdFCLT.DataSource;
            list.Add(new FCLT());
            RefreshGrid();
        }

        private void btnDelRowFCLT_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdFCLTView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<FCLT> list = (List<FCLT>)grdFCLT.DataSource;
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

                    CMakeASM036 make = new CMakeASM036();
                    tran = conn.BeginTransaction();
                    make.MakeASM036(m_data, sysdt, systm, m_User, conn, tran, true);
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

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
    public partial class ADD7007_ASM035_003 : Form
    {
        public event EventHandler<RemakeRequestedEventArgs<CDataASM035_003>> RemakeRequested;
        private ADD7007_ERROR f_err = new ADD7007_ERROR();
        
        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM035_003 m_data;


        public ADD7007_ASM035_003()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM035_003(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM035_003> list = (List<CDataASM035_003>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
            txtIPAT_DD.Focus();
        }

        private void InitCombobox()
        {
            // 마취사유
            cboNCT_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "01", Value = "처치 및 수술" },
                new { Key = "02", Value = "영상진단 및 방사선치료" },
                new { Key = "03", Value = "무통분만" },
                new { Key = "04", Value = "통증조절" },
                new { Key = "99", Value = "기타 검사" }
            };
            cboNCT_RS_CD.DisplayMember = "Value";
            cboNCT_RS_CD.ValueMember = "Key";

            // 회복실 미입실 사유
            cboRCRM_DSU_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "중환자실 입실" },
                new { Key = "2", Value = "격리가 필요한 감염" },
                new { Key = "3", Value = "사망" },
                new { Key = "4", Value = "단순 미입실(병실 이동, 퇴원 등)" },
                new { Key = "5", Value = "회복실 미운영" }
            };
            cboRCRM_DSU_RS_CD.DisplayMember = "Value";
            cboRCRM_DSU_RS_CD.ValueMember = "Key";

            // 오심 구토 평가
            cboEMSS_ASM_EXEC_FQ_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "미실시" },
                new { Key = "1", Value = "1회" },
                new { Key = "2", Value = "2회 이상" }
            };
            cboEMSS_ASM_EXEC_FQ_CD.DisplayMember = "Value";
            cboEMSS_ASM_EXEC_FQ_CD.ValueMember = "Key";

            // 통증평가
            cboPAIN_ASM_EXEC_FQ_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "미실시" },
                new { Key = "1", Value = "1회" },
                new { Key = "2", Value = "2회 이상" }
            };
            cboPAIN_ASM_EXEC_FQ_CD.DisplayMember = "Value";
            cboPAIN_ASM_EXEC_FQ_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            //
        }

        private void ShowData()
        {
            if (m_data.STATUS == "E" || m_data.STATUS == "F")
            {
                f_err.SetError(m_data.ERR_CODE, m_data.ERR_DESC);
                f_err.Show(this.Parent);
            }
            else
            {
                f_err.Hide();
            }

            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // --- A. 기본 정보 ---
            txtIPAT_DD.Text = m_data.IPAT_DD; // 입원일자(YYYYMMDD)
            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1"; // 퇴원여부(1.Yes 2.No)
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2";
            txtDSCG_DD.Text = m_data.DSCG_DD; // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

            // --- B. 마취 정보 ---
            txtNCT_STA_DT_DATE.Text = CUtil.GetDate(m_data.NCT_STA_DT); // 마취 시작일시(YYYYMMDDHHMM)
            txtNCT_STA_DT_TIME.Text = CUtil.GetTime(m_data.NCT_STA_DT);
            txtNCT_END_DT_DATE.Text = CUtil.GetDate(m_data.NCT_END_DT); // 마취 종료일시(YYYYMMDDHHMM)
            txtNCT_END_DT_TIME.Text = CUtil.GetTime(m_data.NCT_END_DT);
            rbNCT_FRM_CD_1.Checked = m_data.NCT_FRM_CD == "1"; // 마취형태 구분코드(1.정규 2.응급)
            rbNCT_FRM_CD_2.Checked = m_data.NCT_FRM_CD == "2";

            // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            var asmCodes = m_data.ASM_NCT_MTH_CD.Split('/');
            chkASM_NCT_MTH_CD_1.Checked = asmCodes.Contains("1");
            chkASM_NCT_MTH_CD_2.Checked = asmCodes.Contains("2");
            chkASM_NCT_MTH_CD_3.Checked = asmCodes.Contains("3");
            chkASM_NCT_MTH_CD_4.Checked = asmCodes.Contains("4");
            chkASM_NCT_MTH_CD_5.Checked = asmCodes.Contains("5");
            chkASM_NCT_MTH_CD_6.Checked = asmCodes.Contains("6");
            chkASM_NCT_MTH_CD_7.Checked = asmCodes.Contains("7");
            chkASM_NCT_MTH_CD_8.Checked = asmCodes.Contains("8");
            chkASM_NCT_MTH_CD_9.Checked = asmCodes.Contains("9");

            CUtil.SetComboboxSelectedValue(cboNCT_RS_CD, m_data.NCT_RS_CD); // 마취사유 구분코드(01~99)
            txtMDFEE_CD.Text = m_data.MDFEE_CD; // 수가코드(마취사유가 02/03/04일 때 필수)
            txtMDFEE_CD_NM.Text = m_data.MDFEE_CD_NM; // 수가코드명(마취사유가 02/03/04일 때 필수)

            // --- C. 마취 전 ---
            rbPTNT_ASM_YN_1.Checked = m_data.PTNT_ASM_YN == "1"; // 마취 전 환자평가 시행여부(1.Yes)
            rbPTNT_ASM_YN_2.Checked = m_data.PTNT_ASM_YN == "2"; // 마취 전 환자평가 시행여부(2.No)

            // --- D. 마취 중 ---
            rbLBT_TRET_YN_1.Checked = m_data.LBT_TRET_YN == "1"; // 의도적 저체온증(1.Yes)
            rbLBT_TRET_YN_2.Checked = m_data.LBT_TRET_YN == "2"; // 의도적 저체온증(2.No)

            // 체온 평가(저체온증 No일 때)
            rbCNTR_TMPR_MASR_YN_1.Checked = m_data.CNTR_TMPR_MASR_YN == "1"; // 연속적 체온 측정 및 기록 여부(1.Yes)
            rbCNTR_TMPR_MASR_YN_2.Checked = m_data.CNTR_TMPR_MASR_YN == "2"; // 연속적 체온 측정 및 기록 여부(2.No)

            // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
            var tmprCodes = m_data.TMPR_RGN_CD.Split('/');
            chkTMPR_RGN_CD_01.Checked = tmprCodes.Contains("01");
            chkTMPR_RGN_CD_02.Checked = tmprCodes.Contains("02");
            chkTMPR_RGN_CD_03.Checked = tmprCodes.Contains("03");
            chkTMPR_RGN_CD_04.Checked = tmprCodes.Contains("04");
            chkTMPR_RGN_CD_05.Checked = tmprCodes.Contains("05");
            chkTMPR_RGN_CD_06.Checked = tmprCodes.Contains("06");
            chkTMPR_RGN_CD_07.Checked = tmprCodes.Contains("07");
            chkTMPR_RGN_CD_08.Checked = tmprCodes.Contains("08");
            chkTMPR_RGN_CD_99.Checked = tmprCodes.Contains("99");

            txtTMPR_RGN_ETC_TXT.Text = m_data.TMPR_RGN_ETC_TXT; // 체온 측정방법 기타 상세
            txtLWET_TMPR.Text = m_data.LWET_TMPR; // 최저체온

            // 신경근 감시
            rbNRRT_BLCK_USE_YN_1.Checked = m_data.NRRT_BLCK_USE_YN == "1"; // 신경근 차단제 사용 여부(1.Yes)
            rbNRRT_BLCK_USE_YN_2.Checked = m_data.NRRT_BLCK_USE_YN == "2"; // 신경근 차단제 사용 여부(2.No)
            rbNRRT_MNTR_YN_1.Checked = m_data.NRRT_MNTR_YN == "1"; // 신경근 감시 여부(1.Yes)
            rbNRRT_MNTR_YN_2.Checked = m_data.NRRT_MNTR_YN == "2"; // 신경근 감시 여부(2.No)

            // --- E. 마취 후(회복실) ---
            rbRCRM_IPAT_YN_1.Checked = m_data.RCRM_IPAT_YN == "1"; // 회복실 입실 여부(1.Yes)
            rbRCRM_IPAT_YN_2.Checked = m_data.RCRM_IPAT_YN == "2"; // 회복실 입실 여부(2.No)
            CUtil.SetComboboxSelectedValue(cboRCRM_DSU_RS_CD, m_data.RCRM_DSU_RS_CD); // 회복실 미입실 사유(1~5, 입실 No일 때)
            CUtil.SetComboboxSelectedValue(cboEMSS_ASM_EXEC_FQ_CD, m_data.EMSS_ASM_EXEC_FQ_CD); // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            txtEMSS_ASM_RS_TXT.Text = m_data.EMSS_ASM_RS_TXT; // 오심 및 구토평가 미실시/1회 사유
            CUtil.SetComboboxSelectedValue(cboPAIN_ASM_EXEC_FQ_CD, m_data.PAIN_ASM_EXEC_FQ_CD); // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            txtPAIN_ASM_RS_TXT.Text = m_data.PAIN_ASM_RS_TXT; // 통증평가 미실시/1회 사유
        }

        private void RefreshGrid()
        {
        }

        //private string GetNCT_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "01") return "처치 및 수술";
        //    if (p_value == "02") return "영상진단 및 방사선치료";
        //    if (p_value == "03") return "무통분만";
        //    if (p_value == "04") return "통증조절";
        //    if (p_value == "99") return "기타 검사";
        //    return "";
        //}

        //private string GetNCT_RS_CD(string p_value)
        //{
        //    if (p_value == "처치 및 수술") return "01";
        //    if (p_value == "영상진단 및 방사선치료") return "02";
        //    if (p_value == "무통분만") return "03";
        //    if (p_value == "통증조절") return "04";
        //    if (p_value == "기타 검사") return "99";
        //    return "";
        //}

        //private string GetRCRM_DSU_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "중환자실 입실";
        //    if (p_value == "2") return "격리가 필요한 감염";
        //    if (p_value == "3") return "사망";
        //    if (p_value == "4") return "단순 미입실(병실 이동, 퇴원 등)";
        //    if (p_value == "5") return "회복실 미운영";
        //    return "";
        //}

        //private string GetRCRM_DSU_RS_CD(string p_value)
        //{
        //    if (p_value == "중환자실 입실") return "1";
        //    if (p_value == "격리가 필요한 감염") return "2";
        //    if (p_value == "사망") return "3";
        //    if (p_value == "단순 미입실(병실 이동, 퇴원 등)") return "4";
        //    if (p_value == "회복실 미운영") return "5";
        //    return "";
        //}

        //private string GetEMSS_ASM_EXEC_FQ_CD_NM(string p_value)
        //{
        //    if (p_value == "0") return "미실시";
        //    if (p_value == "1") return "1회";
        //    if (p_value == "2") return "2회 이상";
        //    return "";
        //}

        //private string GetEMSS_ASM_EXEC_FQ_CD(string p_value)
        //{
        //    if (p_value == "미실시") return "0";
        //    if (p_value == "1회") return "1";
        //    if (p_value == "2회 이상") return "2";
        //    return "";
        //}

        //private string GetPAIN_ASM_EXEC_FQ_CD_NM(string p_value)
        //{
        //    if (p_value == "0") return "미실시";
        //    if (p_value == "1") return "1회";
        //    if (p_value == "2") return "2회 이상";
        //    return "";
        //}

        //private string GetPAIN_ASM_EXEC_FQ_CD(string p_value)
        //{
        //    if (p_value == "미실시") return "0";
        //    if (p_value == "1회") return "1";
        //    if (p_value == "2회 이상") return "2";
        //    return "";
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- A. 기본 정보 ---
            m_data.IPAT_DD = txtIPAT_DD.Text.Trim(); // 입원일자(YYYYMMDD)
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2); // 퇴원여부(1.Yes 2.No)
            m_data.DSCG_DD = txtDSCG_DD.Text.Trim(); // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

            // --- B. 마취 정보 ---
            m_data.NCT_STA_DT = CUtil.GetDateTime(txtNCT_STA_DT_DATE.Text, txtNCT_STA_DT_TIME.Text); // 마취 시작일시(YYYYMMDDHHMM)
            m_data.NCT_END_DT = CUtil.GetDateTime(txtNCT_END_DT_DATE.Text, txtNCT_END_DT_TIME.Text); // 마취 종료일시(YYYYMMDDHHMM)
            m_data.NCT_FRM_CD = rbNCT_FRM_CD_1.Checked ? "1" : (rbNCT_FRM_CD_2.Checked ? "2" : ""); // 마취형태 구분코드(1.정규 2.응급)

            // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            List<string> nctMthCodes = new List<string>();
            if (chkASM_NCT_MTH_CD_1.Checked) nctMthCodes.Add("1");
            if (chkASM_NCT_MTH_CD_2.Checked) nctMthCodes.Add("2");
            if (chkASM_NCT_MTH_CD_3.Checked) nctMthCodes.Add("3");
            if (chkASM_NCT_MTH_CD_4.Checked) nctMthCodes.Add("4");
            if (chkASM_NCT_MTH_CD_5.Checked) nctMthCodes.Add("5");
            if (chkASM_NCT_MTH_CD_6.Checked) nctMthCodes.Add("6");
            if (chkASM_NCT_MTH_CD_7.Checked) nctMthCodes.Add("7");
            if (chkASM_NCT_MTH_CD_8.Checked) nctMthCodes.Add("8");
            if (chkASM_NCT_MTH_CD_9.Checked) nctMthCodes.Add("9");
            m_data.ASM_NCT_MTH_CD = string.Join("/", nctMthCodes.ToArray()); // 마취방법 구분코드

            // 마취사유 구분코드(01~99)
            m_data.NCT_RS_CD = CUtil.GetComboboxSelectedValue(cboNCT_RS_CD);
            m_data.MDFEE_CD = txtMDFEE_CD.Text.Trim(); // 수가코드(마취사유가 02/03/04일 때 필수)
            m_data.MDFEE_CD_NM = txtMDFEE_CD_NM.Text.Trim(); // 수가코드명(마취사유가 02/03/04일 때 필수)

            // --- C. 마취 전 ---
            m_data.PTNT_ASM_YN = CUtil.GetRBString(rbPTNT_ASM_YN_1, rbPTNT_ASM_YN_2); // 마취 전 환자평가 시행여부(1.Yes 2.No)

            // --- D. 마취 중 ---
            m_data.LBT_TRET_YN = CUtil.GetRBString(rbLBT_TRET_YN_1, rbLBT_TRET_YN_2); // 의도적 저체온증 적용 여부(1.Yes 2.No)
            m_data.CNTR_TMPR_MASR_YN = CUtil.GetRBString(rbCNTR_TMPR_MASR_YN_1, rbCNTR_TMPR_MASR_YN_2); // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)

            // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
            List<string> tmprRgnCodes = new List<string>();
            if (chkTMPR_RGN_CD_01.Checked) tmprRgnCodes.Add("01");
            if (chkTMPR_RGN_CD_02.Checked) tmprRgnCodes.Add("02");
            if (chkTMPR_RGN_CD_03.Checked) tmprRgnCodes.Add("03");
            if (chkTMPR_RGN_CD_04.Checked) tmprRgnCodes.Add("04");
            if (chkTMPR_RGN_CD_05.Checked) tmprRgnCodes.Add("05");
            if (chkTMPR_RGN_CD_06.Checked) tmprRgnCodes.Add("06");
            if (chkTMPR_RGN_CD_07.Checked) tmprRgnCodes.Add("07");
            if (chkTMPR_RGN_CD_08.Checked) tmprRgnCodes.Add("08");
            if (chkTMPR_RGN_CD_99.Checked) tmprRgnCodes.Add("99");
            m_data.TMPR_RGN_CD = string.Join("/", tmprRgnCodes.ToArray()); // 체온 측정방법 구분코드

            m_data.TMPR_RGN_ETC_TXT = txtTMPR_RGN_ETC_TXT.Text.Trim(); // 체온 측정방법 기타 상세(99일 때)
            m_data.LWET_TMPR = txtLWET_TMPR.Text.Trim(); // 최저체온(℃, 소수점 첫째자리)

            m_data.NRRT_BLCK_USE_YN = CUtil.GetRBString(rbNRRT_BLCK_USE_YN_1, rbNRRT_BLCK_USE_YN_2); // 신경근 차단제 사용 여부(1.Yes 2.No)
            m_data.NRRT_MNTR_YN = CUtil.GetRBString(rbNRRT_MNTR_YN_1, rbNRRT_MNTR_YN_2); // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)

            // --- E. 마취 후(회복실) ---
            m_data.RCRM_IPAT_YN = CUtil.GetRBString(rbRCRM_IPAT_YN_1, rbRCRM_IPAT_YN_2); // 회복실 입실 여부(1.Yes 2.No)
            m_data.RCRM_DSU_RS_CD = CUtil.GetComboboxSelectedValue(cboRCRM_DSU_RS_CD); // 회복실 미입실 사유(1~5, 입실 No일 때)
            m_data.EMSS_ASM_EXEC_FQ_CD = CUtil.GetComboboxSelectedValue(cboEMSS_ASM_EXEC_FQ_CD); // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            m_data.EMSS_ASM_RS_TXT = txtEMSS_ASM_RS_TXT.Text.Trim(); // 오심 및 구토평가 미실시/1회 사유
            m_data.PAIN_ASM_EXEC_FQ_CD = CUtil.GetComboboxSelectedValue(cboPAIN_ASM_EXEC_FQ_CD); // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            m_data.PAIN_ASM_RS_TXT = txtPAIN_ASM_RS_TXT.Text.Trim(); // 통증평가 미실시/1회 사유

            // --- 데이터베이스 저장 ---
            string strConn = MetroLib.DBHelper.GetConnectionString(); // DB 연결 문자열
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null; // 트랜잭션 객체
                try
                {
                    conn.Open(); // DB 연결
                    tran = conn.BeginTransaction(); // 트랜잭션 시작
                    string sysdt = MetroLib.Util.GetSysDate(conn, tran); // 시스템 일자
                    string systm = MetroLib.Util.GetSysTime(conn, tran); // 시스템 시간
                    m_data.UpdData(sysdt, systm, m_User, conn, tran); // 데이터 저장
                    tran.Commit(); // 트랜잭션 커밋
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback(); // 롤백
                    MessageBox.Show(ex.Message); // 에러 메시지 표시
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
                List<CDataASM035_003> list = (List<CDataASM035_003>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
                txtIPAT_DD.Focus();
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
                List<CDataASM035_003> list = (List<CDataASM035_003>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
                txtIPAT_DD.Focus();
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
            var args = new RemakeRequestedEventArgs<CDataASM035_003>(m_data);

            // ADD7007E가 처리하도록 이벤트만 발생
            if (RemakeRequested != null)
            {
                RemakeRequested(this, args);

                if (args.Success)
                {
                    ShowData();
                    txtIPAT_DD.Focus();
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show(args.FailureMessage);
                }
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

        private void SetEnabled()
        {
            // 퇴원 여부가 ‘1. Yes’인 경우, 퇴원한 날짜 기재
            if (rbDSCG_YN_1.Checked == false)
            {
                txtDSCG_DD.Enabled = false;
            }
            else
            {
                txtDSCG_DD.Enabled = true;
            }
            rbLBT_TRET_YN_1.Enabled = true;
            rbLBT_TRET_YN_2.Enabled = true;

            // 1. 의도적 저체온증이 '(2)No'인 경우, "마취 중 연속적 체온 측정 및 기록여부", "마취 중 주요 체온 측정 방법의 구분코드"를 기재
            if (rbLBT_TRET_YN_2.Checked == false)
            {
                // 연속적 체온 측정 및 기록여부
                rbCNTR_TMPR_MASR_YN_1.Enabled = false;
                rbCNTR_TMPR_MASR_YN_2.Enabled = false;
                // 체온측정방법
                chkTMPR_RGN_CD_01.Enabled = false;
                chkTMPR_RGN_CD_02.Enabled = false;
                chkTMPR_RGN_CD_03.Enabled = false;
                chkTMPR_RGN_CD_04.Enabled = false;
                chkTMPR_RGN_CD_05.Enabled = false;
                chkTMPR_RGN_CD_06.Enabled = false;
                chkTMPR_RGN_CD_07.Enabled = false;
                chkTMPR_RGN_CD_08.Enabled = false;
                chkTMPR_RGN_CD_99.Enabled = false;
                txtTMPR_RGN_ETC_TXT.Enabled = false;
                txtLWET_TMPR.Enabled = false;
            }
            else
            {
                // 연속적 체온 측정 및 기록여부
                rbCNTR_TMPR_MASR_YN_1.Enabled = true;
                rbCNTR_TMPR_MASR_YN_2.Enabled = true;
                // 체온측정방법
                chkTMPR_RGN_CD_01.Enabled = true;
                chkTMPR_RGN_CD_02.Enabled = true;
                chkTMPR_RGN_CD_03.Enabled = true;
                chkTMPR_RGN_CD_04.Enabled = true;
                chkTMPR_RGN_CD_05.Enabled = true;
                chkTMPR_RGN_CD_06.Enabled = true;
                chkTMPR_RGN_CD_07.Enabled = true;
                chkTMPR_RGN_CD_08.Enabled = true;
                chkTMPR_RGN_CD_99.Enabled = true;
                // 측정방법이 '(99) 기타'인 경우 체온 측정 부위를 평문으로 기재
                if (chkTMPR_RGN_CD_99.Checked == false)
                {
                    txtTMPR_RGN_ETC_TXT.Enabled = false;
                }
                else
                {
                    txtTMPR_RGN_ETC_TXT.Enabled = true;
                }
                txtLWET_TMPR.Enabled = true;
            }

            // '마취사유가 '(02)영상진단 및 방사선치료' 또는 '(03)무통분만' 또는 '(04)통증조절'인 경우 수가코드와 수가명 필수 작성
            /*
             * 이런 경우에만 작성이 아니고 이런 겨우에는 필수 작성이라고 되어있음. 즉 넣어도 되는 상황이라서 enable 하는 부분을 막음.
            if (cboNCT_RS_CD.SelectedIndex == 2 || cboNCT_RS_CD.SelectedIndex == 3 || cboNCT_RS_CD.SelectedIndex == 4)
            {
                txtMDFEE_CD.Enabled = true;
                txtMDFEE_CD_NM.Enabled = true;
            }
            else
            {
                txtMDFEE_CD.Enabled = false;
                txtMDFEE_CD_NM.Enabled = false;
            }
            */
            // 신경근 차단제 사용 여부
            if (rbNRRT_BLCK_USE_YN_1.Checked == false)
            {
                rbNRRT_MNTR_YN_1.Enabled = false;
                rbNRRT_MNTR_YN_2.Enabled = false;
            }
            else
            {
                rbNRRT_MNTR_YN_1.Enabled = true;
                rbNRRT_MNTR_YN_2.Enabled = true;
            }
            // 회복실 입실 여부
            // 회복실 미입실 사유
            if (rbRCRM_IPAT_YN_2.Checked == false)
            {
                cboRCRM_DSU_RS_CD.Enabled = false;
            }
            else
            {
                cboRCRM_DSU_RS_CD.Enabled = true;
            }
            // 오심 및 구토 평가
            if (rbRCRM_IPAT_YN_1.Checked == false)
            {
                cboEMSS_ASM_EXEC_FQ_CD.Enabled = false;
                txtEMSS_ASM_RS_TXT.Enabled = false;
                cboPAIN_ASM_EXEC_FQ_CD.Enabled = false;
                txtPAIN_ASM_RS_TXT.Enabled = false;
            }
            else
            {
                // 오심 및 구토평가
                cboEMSS_ASM_EXEC_FQ_CD.Enabled = true;
                if (cboEMSS_ASM_EXEC_FQ_CD.SelectedIndex == 1 || cboEMSS_ASM_EXEC_FQ_CD.SelectedIndex == 2)
                {
                    txtEMSS_ASM_RS_TXT.Enabled = true;
                }
                else
                {
                    txtEMSS_ASM_RS_TXT.Enabled = false;
                }
                // 통증평가
                cboPAIN_ASM_EXEC_FQ_CD.Enabled = true;
                if (cboPAIN_ASM_EXEC_FQ_CD.SelectedIndex == 1 || cboPAIN_ASM_EXEC_FQ_CD.SelectedIndex == 2)
                {
                    txtPAIN_ASM_RS_TXT.Enabled = true;
                }
                else
                {
                    txtPAIN_ASM_RS_TXT.Enabled = false;
                }
            }
        }

        private void rbDSCG_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbDSCG_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void cboNCT_RS_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbLBT_TRET_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbLBT_TRET_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void chkTMPR_RGN_CD_99_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbNRRT_BLCK_USE_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbNRRT_BLCK_USE_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbRCRM_IPAT_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbRCRM_IPAT_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void cboEMSS_ASM_EXEC_FQ_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void cboPAIN_ASM_EXEC_FQ_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void chkASM_NCT_MTH_CD_4_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();

        }

        private void chkASM_NCT_MTH_CD_5_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }
    }
}

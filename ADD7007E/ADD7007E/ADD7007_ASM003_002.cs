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
    public partial class ADD7007_ASM003_002 : Form
    {
        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM003_002 m_data;

        public ADD7007_ASM003_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM003_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM003_002> list = (List<CDataASM003_002>)m_view.DataSource;
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
                new { Key = "5", Value = "사망" },
            };
            cboDSCG_STAT_CD.DisplayMember = "Value";
            cboDSCG_STAT_CD.ValueMember = "Key";
            // 전원사유
            cboACPH_DHI_RS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "급성기 치료 위해 전원" },
                new { Key = "2", Value = "급성기 치료 후 전원" },
            };
            cboACPH_DHI_RS_CD.DisplayMember = "Value";
            cboACPH_DHI_RS_CD.ValueMember = "Key";
            // 급성기 뇌졸증과의 관련성
            cboACPH_CRST_REL_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "해당없음" },
                new { Key = "1", Value = "타질환으로 입원 중 발생한 뇌졸중" },
                new { Key = "2", Value = "과거 뇌졸중의 후유증으로 내원" },
                new { Key = "3", Value = "외상으로 인해 발생한 뇌졸중" },
            };
            cboACPH_CRST_REL_CD.DisplayMember = "Value";
            cboACPH_CRST_REL_CD.ValueMember = "Key";
            // 뇌졸증 증상발생
            cboCRST_SYMT_OCUR_CFR_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "증상발생 시각이 명확한 경우" },
                new { Key = "2", Value = "증상발생 시각이 명확하지 않은 경우" },
                new { Key = "3", Value = "unknown" },
                new { Key = "4", Value = "기록없음" },
            };
            cboCRST_SYMT_OCUR_CFR_CD.DisplayMember = "Value";
            cboCRST_SYMT_OCUR_CFR_CD.ValueMember = "Key";
            // 내원장소
            cboASM_FST_IPAT_PTH_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "외래" },
                new { Key = "2", Value = "응급실" },
                new { Key = "3", Value = "기록없음" },
            };
            cboASM_FST_IPAT_PTH_CD.DisplayMember = "Value";
            cboASM_FST_IPAT_PTH_CD.ValueMember = "Key";
            // 내원경로
            cboASM_VST_PTH_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "직접내원" },
                new { Key = "2", Value = "타병원 전원" },
                new { Key = "3", Value = "기록없음" },
            };
            cboASM_VST_PTH_CD.DisplayMember = "Value";
            cboASM_VST_PTH_CD.ValueMember = "Key";
            // 내원방법
            cboVST_MTH_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "구급차" },
                new { Key = "2", Value = "다른 교통수단" },
                new { Key = "3", Value = "기록없음" },
            };
            cboVST_MTH_CD.DisplayMember = "Value";
            cboVST_MTH_CD.ValueMember = "Key";
            // Stroke Scale 종류
            cboCRST_SCL_KND_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "NIHSS" },
                new { Key = "2", Value = "GCS" },
                new { Key = "9", Value = "기타" },
            };
            cboCRST_SCL_KND_CD.DisplayMember = "Value";
            cboCRST_SCL_KND_CD.ValueMember = "Key";
            // 기능평가종류
            cboDSCG_FCLT_ASM_TL_KND_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "K-MBI" },
                new { Key = "2", Value = "MBI" },
                new { Key = "3", Value = "BI" },
                new { Key = "4", Value = "FIM" },
                new { Key = "5", Value = "mRS" },
                new { Key = "6", Value = "GOS" },
                new { Key = "9", Value = "기타" },
            };
            cboDSCG_FCLT_ASM_TL_KND_CD.DisplayMember = "Value";
            cboDSCG_FCLT_ASM_TL_KND_CD.ValueMember = "Key";
            // 재활협진의뢰여부
            cboRHBLTN_DDIAG_REQ_YN.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "Yes" },
                new { Key = "2", Value = "No" },
                new { Key = "3", Value = "재활의학과 없음" },
            };
            cboRHBLTN_DDIAG_REQ_YN.DisplayMember = "Value";
            cboRHBLTN_DDIAG_REQ_YN.ValueMember = "Key";
            // 재활협진 또는 재활치료 미실시 사유(5일 이내)
            cboCLI_ISTBY_RS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "00", Value = "해당없음" },
                new { Key = "01", Value = "수축기혈압<120mmHg 또는 >220mmHg" },
                new { Key = "02", Value = "산소포화도<92%" },
                new { Key = "03", Value = "심박동수<40회/분 또는 >100회/분" },
                new { Key = "04", Value = "체온 >38.5도" },
                new { Key = "05", Value = "급성관상동맥질환" },
                new { Key = "06", Value = "심한 심부전" },
                new { Key = "07", Value = "입원 3일이내 신경학적으로 악화" },
                new { Key = "08", Value = "입원 5일이내 수술을 받거나 수술 예정" },
                new { Key = "09", Value = "기록없음" },
                new { Key = "99", Value = "기타" },
            };
            cboCLI_ISTBY_RS_CD.DisplayMember = "Value";
            cboCLI_ISTBY_RS_CD.ValueMember = "Key";
            // 기능장해 평가 도구
            cboFCLT_HDP_ASM_TL_KND_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "mRS" },
                new { Key = "2", Value = "NIHSS" },
                new { Key = "9", Value = "기타" },
            };
            cboFCLT_HDP_ASM_TL_KND_CD.DisplayMember = "Value";
            cboFCLT_HDP_ASM_TL_KND_CD.ValueMember = "Key";
            // 폐렴 종류
            cboPNEM_KND_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "흡인성 폐렴" },
                new { Key = "2", Value = "인공호흡기 관련 폐렴" },
                new { Key = "9", Value = "기타" },
            };
            cboPNEM_KND_CD.DisplayMember = "Value";
            cboPNEM_KND_CD.ValueMember = "Key";
            // 미투여 사유
            cboMDS_INJC_NEXEC_RS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "01", Value = "증상발생(최종 정상확인) 시각으로부터 병원도착까지 4.5시간이 초과된 경우" },
                new { Key = "02", Value = "증상발생(최종 정상확인) 시각을 모르는 경우" },
                new { Key = "03", Value = "출혈의 위험성(두개강 내, 두 개강 외)" },
                new { Key = "04", Value = "경미한 증상" },
                new { Key = "05", Value = "증상의 급속한 호전" },
                new { Key = "06", Value = "다른 질환으로 인한 것일 가능성이 있는 경우" },
                new { Key = "07", Value = "피검사에서 출혈 성향이 있는 것 (혈소판수치 10만이하, 비정상 aPTT, PT의 INR 1.5이상)" },
                new { Key = "08", Value = "조절되지 않은 고혈압" },
                new { Key = "09", Value = "환자 또는 보호자의 거부" },
                new { Key = "10", Value = "타병원에서 정맥내 혈전용해제(t-PA) 투여후 전원" },
                new { Key = "11", Value = "기록없음" },
                new { Key = "99", Value = "기타" },
            };
            cboMDS_INJC_NEXEC_RS_CD.DisplayMember = "Value";
            cboMDS_INJC_NEXEC_RS_CD.ValueMember = "Key";
            // 병원도착60분초과사유
            cboMN60_ECS_INJC_RS_CD2.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "해당없음" },
                new { Key = "1", Value = "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우" },
                new { Key = "2", Value = "NIHSS 2점 이상 증가하여 증상이 악화되는 경우" },
                new { Key = "3", Value = "혈압이 표준진료지침에서 권고하는 수준 보다 높아 혈압 강하 치료가 우선 시행되었던 경우" },
                new { Key = "4", Value = "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우" },
                new { Key = "5", Value = "기록없음" },
            };
            cboMN60_ECS_INJC_RS_CD2.DisplayMember = "Value";
            cboMN60_ECS_INJC_RS_CD2.ValueMember = "Key";
            // 병원도착 120분 초과 실시 사유
            cboMN120_ECS_EXEC_RS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "해당없음" },
                new { Key = "1", Value = "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우" },
                new { Key = "2", Value = "NIHSS 2점 이상 증가하여 증상이 악화되는 경우" },
                new { Key = "3", Value = "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우" },
                new { Key = "4", Value = "기록없음" },
                new { Key = "9", Value = "기타" },
            };
            cboMN120_ECS_EXEC_RS_CD.DisplayMember = "Value";
            cboMN120_ECS_EXEC_RS_CD.ValueMember = "Key";
            // 병원도착 24시간 초과 최종치료 실시 사유
            cboHR24_ECS_LAST_TRET_EXEC_RS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "초기 검사에서 파열된 뇌동맥류 증거가 없었던 경우" },
                new { Key = "2", Value = "기록없음" },
                new { Key = "9", Value = "기타" },
            };
            cboHR24_ECS_LAST_TRET_EXEC_RS_CD.DisplayMember = "Value";
            cboHR24_ECS_LAST_TRET_EXEC_RS_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            // GRID에 콤보 컬럼을 만든다.
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // --- A. 입∙퇴원 정보 ---
            txtASM_HOSP_ARIV_DT_DATE.Text = CUtil.GetDate(m_data.ASM_HOSP_ARIV_DT); // 병원도착일자 - YYYYMMDD
            txtASM_HOSP_ARIV_DT_TIME.Text = CUtil.GetTime(m_data.ASM_HOSP_ARIV_DT); // 병원도착시간 - HHMM

            // 퇴원 여부 라디오 버튼 설정
            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1"; // Yes
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2"; // No

            txtASM_DSCG_DT_DATE.Text = CUtil.GetDate(m_data.ASM_DSCG_DT); // 퇴원일자
            txtASM_DSCG_DT_TIME.Text = CUtil.GetTime(m_data.ASM_DSCG_DT); // 퇴원시간

            rbDSCG_STAT_RCD_YN_1.Checked = m_data.DSCG_STAT_RCD_YN == "1"; // Yes
            rbDSCG_STAT_RCD_YN_2.Checked = m_data.DSCG_STAT_RCD_YN == "2"; // No

            CUtil.SetComboboxSelectedValue(cboDSCG_STAT_CD, m_data.DSCG_STAT_CD); // 퇴원상태 코드
            CUtil.SetComboboxSelectedValue(cboACPH_DHI_RS_CD, m_data.ACPH_DHI_RS_CD); // 전원사유 코드

            // 최종 진단명 체크박스 설정
            var acphCodes = m_data.ACPH_CRST_DIAG.Split('/');
            chkACPH_CRST_DIAG_1.Checked = acphCodes.Contains("1");
            chkACPH_CRST_DIAG_2.Checked = acphCodes.Contains("2");
            chkACPH_CRST_DIAG_3.Checked = acphCodes.Contains("3");
            chkACPH_CRST_DIAG_4.Checked = acphCodes.Contains("4");
            chkACPH_CRST_DIAG_5.Checked = acphCodes.Contains("5");
            chkACPH_CRST_DIAG_6.Checked = acphCodes.Contains("6");
            chkACPH_CRST_DIAG_7.Checked = acphCodes.Contains("7");
            chkACPH_CRST_DIAG_8.Checked = acphCodes.Contains("8");
            chkACPH_CRST_DIAG_9.Checked = acphCodes.Contains("9");

            txtACPH_CRST_DIAG_ETC_TXT.Text = m_data.ACPH_CRST_DIAG_ETC_TXT; // 최종진단 기타 상세

            CUtil.SetComboboxSelectedValue(cboACPH_CRST_REL_CD, m_data.ACPH_CRST_REL_CD); // 급성기 뇌졸중 관련성
            CUtil.SetComboboxSelectedValue(cboCRST_SYMT_OCUR_CFR_CD, m_data.CRST_SYMT_OCUR_CFR_CD); // 증상발생 시각 확인

            txtSYMT_OCUR_DT_DATE.Text = CUtil.GetDate(m_data.SYMT_OCUR_DT); // 증상발생일자
            txtSYMT_OCUR_DT_TIME.Text = CUtil.GetTime(m_data.SYMT_OCUR_DT); // 증상발생시간

            txtSTAT_CFR_DT_DATE.Text = CUtil.GetDate(m_data.STAT_CFR_DT); // 발견일자
            txtSTAT_CFR_DT_TIME.Text = CUtil.GetTime(m_data.STAT_CFR_DT); // 발견시간

            txtLAST_NRM_CFR_DT_DATE.Text = CUtil.GetDate(m_data.LAST_NRM_CFR_DT); // 최종 정상일자
            txtLAST_NRM_CFR_DT_TIME.Text = CUtil.GetTime(m_data.LAST_NRM_CFR_DT); // 최종 정상시간

            CUtil.SetComboboxSelectedValue(cboASM_FST_IPAT_PTH_CD, m_data.ASM_FST_IPAT_PTH_CD); // 내원장소
            CUtil.SetComboboxSelectedValue(cboASM_VST_PTH_CD, m_data.ASM_VST_PTH_CD); // 내원경로
            CUtil.SetComboboxSelectedValue(cboVST_MTH_CD, m_data.VST_MTH_CD); // 내원방법

            // 재활의학과 전과 여부
            rbRHBLTN_DGSBJT_TRFR_YN_1.Checked = m_data.RHBLTN_DGSBJT_TRFR_YN == "1";
            rbRHBLTN_DGSBJT_TRFR_YN_2.Checked = m_data.RHBLTN_DGSBJT_TRFR_YN == "2";
            txtTRFR_DD.Text = m_data.TRFR_DD; // 전과일자

            // 연명의료중단 결정 여부
            rbWLST_RCD_YN_1.Checked = m_data.WLST_RCD_YN == "1";
            rbWLST_RCD_YN_2.Checked = m_data.WLST_RCD_YN == "2";
            txtDEATH_PCS_PTNT_RCD_DT.Text = m_data.DEATH_PCS_PTNT_RCD_DT; // 판단서 작성일자

            // 뇌사판정 여부
            rbBRN_DEATH_YN_1.Checked = m_data.BRN_DEATH_YN == "1";
            rbBRN_DEATH_YN_2.Checked = m_data.BRN_DEATH_YN == "2";
            txtBRN_DEATH_DD.Text = m_data.BRN_DEATH_DD; // 뇌사판정일자

            // --- B. 진료 정보 ---
            // Stroke Scale
            rbCRST_SCL_ENFC_YN_1.Checked = m_data.CRST_SCL_ENFC_YN == "1";         // Stroke Scale 시행 여부(1:Yes, 2:No, 내원장소=2)
            rbCRST_SCL_ENFC_YN_2.Checked = m_data.CRST_SCL_ENFC_YN == "2";
            txtCRST_SCL_FST_EXEC_DD.Text = m_data.CRST_SCL_FST_EXEC_DD;     // 최초 시행일(YYYYMMDD, Stroke Scale Yes)
            CUtil.SetComboboxSelectedValue(cboCRST_SCL_KND_CD, m_data.CRST_SCL_KND_CD);          // Stroke Scale 종류(1:NIHSS, 2:GCS, 9:기타, Stroke Scale Yes)
            txtETC_CRST_SCL_NM.Text = m_data.ETC_CRST_SCL_NM;          // 기타 Scale 명칭(Stroke Scale 종류=9)
            txtETC_CRST_SCL_HGH_PNT.Text = m_data.ETC_CRST_SCL_HGH_PNT;     // 기타 Scale 최고점수(Stroke Scale 종류=9)
            txtNIHSS_PNT.Text = m_data.NIHSS_PNT;                // NIHSS 평가점수(Stroke Scale 종류=1)
            txtGCS_PNT.Text = m_data.GCS_PNT;                  // GCS 평가점수(Stroke Scale 종류=2)
            txtETC_CRST_SCL_ASM_PNT.Text = m_data.ETC_CRST_SCL_ASM_PNT;     // 기타 Scale 평가점수(Stroke Scale 종류=9)

            // 기능평가 실시
            rbFCLT_ASM_TL_ENFC_YN_1.Checked = m_data.FCLT_ASM_TL_ENFC_YN == "1";      // 기능평가 시행 여부(1:Yes, 2:No, 내원장소=2)
            rbFCLT_ASM_TL_ENFC_YN_2.Checked = m_data.FCLT_ASM_TL_ENFC_YN == "2";
            txtLAST_FCLT_ASM_TL_ENFC_DD.Text = m_data.LAST_FCLT_ASM_TL_ENFC_DD; // 시행일(YYYYMMDD, 기능평가 Yes)
            CUtil.SetComboboxSelectedValue(cboDSCG_FCLT_ASM_TL_KND_CD, m_data.DSCG_FCLT_ASM_TL_KND_CD);  // 기능평가 종류(1:K-MBI, 2:MBI, 3:BI, 4:FIM, 5:mRS, 6:GOS, 9:기타, 기능평가 Yes)
            txtETC_FCLT_ASM_TL_TXT.Text = m_data.ETC_FCLT_ASM_TL_TXT;      // 기타 Scale 명칭(기능평가 종류=9)
            txtETC_FCLT_ASM_TL_HGH_PNT.Text = m_data.ETC_FCLT_ASM_TL_HGH_PNT;  // 기타 Scale 최고점수(기능평가 종류=9)
            txtKMBI_PNT.Text = m_data.KMBI_PNT;                 // K-MBI 평가점수(기능평가 종류=1)
            txtMBI_PNT.Text = m_data.MBI_PNT;                  // MBI 평가점수(기능평가 종류=2)
            txtBI_PNT.Text = m_data.BI_PNT;                   // BI 평가점수(기능평가 종류=3)
            txtFIM_PNT.Text = m_data.FIM_PNT;                  // FIM 평가점수(기능평가 종류=4)
            txtMRS_GRD.Text = m_data.MRS_GRD;                  // mRS 평가등급(기능평가 종류=5)
            txtGOS_GRD.Text = m_data.GOS_GRD;                  // GOS 평가등급(기능평가 종류=6)
            txtETC_FCLT_ASM_PNT.Text = m_data.ETC_FCLT_ASM_PNT;         // 기타 Scale 평가점수(기능평가 종류=9)

            // 조기재활
            CUtil.SetComboboxSelectedValue(cboRHBLTN_DDIAG_REQ_YN, m_data.RHBLTN_DDIAG_REQ_YN);      // 재활협진 의뢰 여부(1:Yes, 2:No, 3:재활의학과 없음, 내원장소=2)
            txtFST_REQ_DD.Text = m_data.FST_REQ_DD;               // 의뢰일(YYYYMMDD, 재활협진의뢰 Yes)
            rbRHBLTN_DDIAG_FST_RPY_YN_1.Checked = m_data.RHBLTN_DDIAG_FST_RPY_YN == "1";  // 재활협진 회신 여부(1:Yes, 2:No, 재활협진의뢰 Yes)
            rbRHBLTN_DDIAG_FST_RPY_YN_1.Checked = m_data.RHBLTN_DDIAG_FST_RPY_YN == "2";
            txtRPY_DD.Text = m_data.RPY_DD;                   // 회신일(YYYYMMDD, 회신 Yes)
            rbRHBLTN_TRET_YN_1.Checked = m_data.RHBLTN_TRET_YN == "1"; // 재활치료 여부 “예” 선택
            rbRHBLTN_TRET_YN_2.Checked = m_data.RHBLTN_TRET_YN == "2"; // 재활치료 여부 “아니오” 선택
            txtFST_TRET_DD.Text = m_data.FST_TRET_DD; // 재활치료 최초 시행일
            rbFCLT_HDP_YN_1.Checked = m_data.FCLT_HDP_YN == "1"; // 기능장해 여부 “예”
            rbFCLT_HDP_YN_2.Checked = m_data.FCLT_HDP_YN == "2"; // 기능장해 여부 “아니오”
            CUtil.SetComboboxSelectedValue(cboCLI_ISTBY_RS_CD, m_data.CLI_ISTBY_RS_CD); // 재활 협진·치료 미실시 사유 코드 표시
            txtCLI_ISTBY_RS_ETC_TXT.Text = m_data.CLI_ISTBY_RS_ETC_TXT; // 미실시 사유 기타 상세 내용
            txtCLI_ISTBY_RS_RCD_DD.Text = m_data.CLI_ISTBY_RS_RCD_DD; // 미실시 사유 기록 일자
            rbFCLT_HDP_ASM_YN_1.Checked = m_data.FCLT_HDP_ASM_YN == "1"; // 기능장해 평가 여부 “예”
            rbFCLT_HDP_ASM_YN_2.Checked = m_data.FCLT_HDP_ASM_YN == "2"; // 기능장해 평가 여부 “아니오”
            CUtil.SetComboboxSelectedValue(cboFCLT_HDP_ASM_TL_KND_CD, m_data.FCLT_HDP_ASM_TL_KND_CD); // 기능장해 평가도구 종류 콤보 표시
            txtFCLT_HDP_ASM_ETC_TL_TXT.Text = m_data.FCLT_HDP_ASM_ETC_TL_TXT; // 기타 평가도구명 상세 입력
            txtHDP_MRS_GRD.Text = m_data.HDP_MRS_GRD; // mRS 평가 등급 값 출력
            txtHDP_NIHSS_PNT.Text = m_data.HDP_NIHSS_PNT; // NIHSS 점수 입력 값 표시
            txtFCLT_HDP_ETC_ASM_PNT.Text = m_data.FCLT_HDP_ETC_ASM_PNT; // 기타 도구 점수
            txtFCLT_HDP_ASM_TL_EXEC_DD.Text = m_data.FCLT_HDP_ASM_TL_EXEC_DD; // 기능장해 평가 도구 실시 날짜

            // 폐렴
            rbHR48_AF_PNEM_SICK_YN_1.Checked = m_data.HR48_AF_PNEM_SICK_YN == "1"; // 입원 48시간 이후 폐렴 발생 여부 - Yes 선택
            rbHR48_AF_PNEM_SICK_YN_2.Checked = m_data.HR48_AF_PNEM_SICK_YN == "2"; // 입원 48시간 이후 폐렴 발생 여부 - No 선택
            CUtil.SetComboboxSelectedValue(cboPNEM_KND_CD, m_data.PNEM_KND_CD); // 폐렴 종류 선택 (흡인성, 인공호흡기, 기타)
            txtPNEM_KND_ETC_TXT.Text = m_data.PNEM_KND_ETC_TXT; // 폐렴 종류가 기타일 경우 상세내용 입력
            txtDIAG_SICK_SYM.Text = m_data.DIAG_SICK_SYM; // 폐렴의 상병분류기호 (질병코드 등)
            txtDIAG_NM.Text = m_data.DIAG_NM; // 폐렴 진단명 (예: Aspiration pneumonia)
            rbATFL_RPRT_YN_1.Checked = m_data.ATFL_RPRT_YN == "1"; // 인공호흡기 사용 여부 - Yes 선택
            rbATFL_RPRT_YN_2.Checked = m_data.ATFL_RPRT_YN == "2"; // 인공호흡기 사용 여부 - No 선택
            txtATFL_RPRT_FST_STA_DD.Text = m_data.ATFL_RPRT_FST_STA_DD; // 인공호흡기 최초 적용 시작일 (YYYYMMDD)
            txtATFL_RPRT_FST_END_DD.Text = m_data.ATFL_RPRT_FST_END_DD; // 인공호흡기 최초 적용 종료일 (YYYYMMDD)

            // --- C. 허혈성 뇌졸중 ---
            // 정맥내 t-PA 투여(액티라제)
            rbDGM_INJC_YN_1.Checked = m_data.DGM_INJC_YN == "1"; // 정맥내 혈전용해제(t-PA) 투여 여부 - Yes
            rbDGM_INJC_YN_2.Checked = m_data.DGM_INJC_YN == "2"; // 정맥내 혈전용해제(t-PA) 투여 여부 - No
            txtMDS_INJC_DT_DATE.Text = CUtil.GetDate(m_data.MDS_INJC_DT); // t-PA 투여일자 (예: 20240101)
            txtMDS_INJC_DT_TIME.Text = CUtil.GetTime(m_data.MDS_INJC_DT); // t-PA 투여시간 (예: 0930)
            CUtil.SetComboboxSelectedValue(cboMDS_INJC_NEXEC_RS_CD, m_data.MDS_INJC_NEXEC_RS_CD); // t-PA 미투여 사유 코드 → 한글명 매핑
            txtMDS_INJC_NEXEC_RS_ETC_TXT.Text = m_data.MDS_INJC_NEXEC_RS_ETC_TXT; // 미투여 사유가 '99.기타'일 경우 상세 내용 입력
            CUtil.SetComboboxSelectedValue(cboMN60_ECS_INJC_RS_CD2, m_data.MN60_ECS_INJC_RS_CD2); // 병원도착 60분 초과 t-PA 투여 사유 선택

            // 동맥내 혈전제거술(기계적 혈전제거술)
            rbINARTR_THBE_EXEC_YN_1.Checked = m_data.INARTR_THBE_EXEC_YN == "1"; // 동맥내 혈전제거술 실시 여부 - Yes 선택
            rbINARTR_THBE_EXEC_YN_2.Checked = m_data.INARTR_THBE_EXEC_YN == "2"; // 동맥내 혈전제거술 실시 여부 - No 선택
            txtTHBE_EXEC_DT_DATE.Text = CUtil.GetDate(m_data.THBE_EXEC_DT); // 동맥내 혈전제거술 실시일자 (형식: YYYYMMDD)
            txtTHBE_EXEC_DT_TIME.Text = CUtil.GetTime(m_data.THBE_EXEC_DT); // 동맥내 혈전제거술 실시시각 (형식: HHMM)
            CUtil.SetComboboxSelectedValue(cboMN120_ECS_EXEC_RS_CD, m_data.MN120_ECS_EXEC_RS_CD); // 120분 초과 사유 코드 → 명칭 표시
            txtMN120_ECS_EXEC_RS_ETC_TXT.Text = m_data.MN120_ECS_EXEC_RS_ETC_TXT; // 120분 초과 사유가 '기타(9)'일 경우 상세 내용 입력

            // --- D. 출혈성 뇌졸중 ---
            // 지주막하출혈 최종치료
            rbSBRC_HMRHG_LAST_TRET_EXEC_YN_1.Checked = m_data.SBRC_HMRHG_LAST_TRET_EXEC_YN == "1"; // 지주막하출혈 최종치료 여부 - Yes 선택
            rbSBRC_HMRHG_LAST_TRET_EXEC_YN_2.Checked = m_data.SBRC_HMRHG_LAST_TRET_EXEC_YN == "2"; // 지주막하출혈 최종치료 여부 - No 선택

            txtLAST_TRET_EXEC_DT_DATE.Text = CUtil.GetDate(m_data.LAST_TRET_EXEC_DT); // 최종치료 실시일자 (예: 20240101)
            txtLAST_TRET_EXEC_DT_TIME.Text = CUtil.GetTime(m_data.LAST_TRET_EXEC_DT); // 최종치료 실시시각 (예: 0930)
            CUtil.SetComboboxSelectedValue(cboHR24_ECS_LAST_TRET_EXEC_RS_CD, m_data.HR24_ECS_LAST_TRET_EXEC_RS_CD); // 병원도착 24시간 초과 실시 사유 (코드 → 명칭 매핑)
            txtHR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT.Text = m_data.HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT; // 초과 사유가 '9:기타'인 경우 상세 내용 입력
        }

        private void RefreshGrid()
        {
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

        //private string GetACPH_DHI_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "급성기 치료 위해 전원";
        //    if (p_value == "2") return "급성기 치료 후 전원";
        //    return "";
        //}

        //private string GetACPH_DHI_RS_CD(string p_value)
        //{
        //    if (p_value == "급성기 치료 위해 전원") return "1";
        //    if (p_value == "급성기 치료 후 전원") return "2";
        //    return "";
        //}

        //private string GetACPH_CRST_REL_CD_NM(string p_value)
        //{
        //    if (p_value == "0") return "해당없음";
        //    if (p_value == "1") return "타질환으로 입원 중 발생한 뇌졸중";
        //    if (p_value == "2") return "과거 뇌졸중의 후유증으로 내원";
        //    if (p_value == "3") return "외상으로 인해 발생한 뇌졸중";
        //    return "";
        //}

        //private string GetACPH_CRST_REL_CD(string p_value)
        //{
        //    if (p_value == "해당없음") return "0";
        //    if (p_value == "타질환으로 입원 중 발생한 뇌졸중") return "1";
        //    if (p_value == "과거 뇌졸중의 후유증으로 내원") return "2";
        //    if (p_value == "외상으로 인해 발생한 뇌졸중") return "3";
        //    return "";
        //}

        //private string GetCRST_SYMT_OCUR_CFR_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "증상발생 시각이 명확한 경우";
        //    if (p_value == "2") return "증상발생 시각이 명확하지 않은 경우";
        //    if (p_value == "3") return "unknown";
        //    if (p_value == "4") return "기록없음";
        //    return "";
        //}

        //private string GetCRST_SYMT_OCUR_CFR_CD(string p_value)
        //{
        //    if (p_value == "증상발생 시각이 명확한 경우") return "1";
        //    if (p_value == "증상발생 시각이 명확하지 않은 경우") return "2";
        //    if (p_value == "unknown") return "3";
        //    if (p_value == "기록없음") return "4";
        //    return "";
        //}

        //private string GetASM_FST_IPAT_PTH_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "외래";
        //    if (p_value == "2") return "응급실";
        //    if (p_value == "3") return "기록없음";
        //    return "";
        //}

        //private string GetASM_FST_IPAT_PTH_CD(string p_value)
        //{
        //    if (p_value == "외래") return "1";
        //    if (p_value == "응급실") return "2";
        //    if (p_value == "기록없음") return "3";
        //    return "";
        //}

        //private string GetASM_VST_PTH_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "직접내원";
        //    if (p_value == "2") return "타병원 전원";
        //    if (p_value == "3") return "기록없음";
        //    return "";
        //}

        //private string GetASM_VST_PTH_CD(string p_value)
        //{
        //    if (p_value == "직접내원") return "1";
        //    if (p_value == "타병원 전원") return "2";
        //    if (p_value == "기록없음") return "3";
        //    return "";
        //}

        //private string GetVST_MTH_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "구급차";
        //    if (p_value == "2") return "다른 교통수단";
        //    if (p_value == "3") return "기록없음";
        //    return "";
        //}

        //private string GetVST_MTH_CD(string p_value)
        //{
        //    if (p_value == "구급차") return "1";
        //    if (p_value == "다른 교통수단") return "2";
        //    if (p_value == "기록없음") return "3";
        //    return "";
        //}

        //private string GetCRST_SCL_KND_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "NIHSS";
        //    if (p_value == "2") return "GCS";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetCRST_SCL_KND_CD(string p_value)
        //{
        //    if (p_value == "NIHSS") return "1";
        //    if (p_value == "GCS") return "2";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetDSCG_FCLT_ASM_TL_KND_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "K-MBI";
        //    if (p_value == "2") return "MBI";
        //    if (p_value == "3") return "BI";
        //    if (p_value == "4") return "FIM";
        //    if (p_value == "5") return "mRS";
        //    if (p_value == "6") return "GOS";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetDSCG_FCLT_ASM_TL_KND_CD(string p_value)
        //{
        //    if (p_value == "K-MBI") return "1";
        //    if (p_value == "MBI") return "2";
        //    if (p_value == "BI") return "3";
        //    if (p_value == "FIM") return "4";
        //    if (p_value == "mRS") return "5";
        //    if (p_value == "GOS") return "6";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetRHBLTN_DDIAG_REQ_YN_NM(string p_value)
        //{
        //    if (p_value == "1") return "Yes";
        //    if (p_value == "2") return "No";
        //    if (p_value == "3") return "재활의학과 없음";
        //    return "";
        //}

        //private string GetRHBLTN_DDIAG_REQ_YN(string p_value)
        //{
        //    if (p_value == "Yes") return "1";
        //    if (p_value == "No") return "2";
        //    if (p_value == "재활의학과 없음") return "3";
        //    return "";
        //}

        //private string GetCLI_ISTBY_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "00") return "해당없음";
        //    if (p_value == "01") return "수축기혈압<120mmHg 또는 >220mmHg";
        //    if (p_value == "02") return "산소포화도<92%";
        //    if (p_value == "03") return "심박동수<40회/분 또는 >100회/분";
        //    if (p_value == "04") return "체온 >38.5도";
        //    if (p_value == "05") return "급성관상동맥질환";
        //    if (p_value == "06") return "심한 심부전";
        //    if (p_value == "07") return "입원 3일이내 신경학적으로 악화";
        //    if (p_value == "08") return "입원 5일이내 수술을 받거나 수술 예정";
        //    if (p_value == "09") return "기록없음";
        //    if (p_value == "99") return "기타";
        //    return "";
        //}

        //private string GetCLI_ISTBY_RS_CD(string p_value)
        //{
        //    if (p_value == "해당없음") return "00";
        //    if (p_value == "수축기혈압<120mmHg 또는 >220mmHg") return "01";
        //    if (p_value == "산소포화도<92%") return "02";
        //    if (p_value == "심박동수<40회/분 또는 >100회/분") return "03";
        //    if (p_value == "체온 >38.5도") return "04";
        //    if (p_value == "급성관상동맥질환") return "05";
        //    if (p_value == "심한 심부전") return "06";
        //    if (p_value == "입원 3일이내 신경학적으로 악화") return "07";
        //    if (p_value == "입원 5일이내 수술을 받거나 수술 예정") return "08";
        //    if (p_value == "기록없음") return "09";
        //    if (p_value == "기타") return "99";
        //    return "";
        //}

        //private string GetFCLT_HDP_ASM_TL_KND_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "mRS";
        //    if (p_value == "2") return "NIHSS";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetFCLT_HDP_ASM_TL_KND_CD(string p_value)
        //{
        //    if (p_value == "mRS") return "1";
        //    if (p_value == "NIHSS") return "2";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetPNEM_KND_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "흡인성 폐렴";
        //    if (p_value == "2") return "인공호흡기 관련 폐렴";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetPNEM_KND_CD(string p_value)
        //{
        //    if (p_value == "흡인성 폐렴") return "1";
        //    if (p_value == "인공호흡기 관련 폐렴") return "2";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetMDS_INJC_NEXEC_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "01") return "증상발생(최종 정상확인) 시각으로부터 병원도착까지 4.5시간이 초과된 경우";
        //    if (p_value == "02") return "증상발생(최종 정상확인) 시각을 모르는 경우";
        //    if (p_value == "03") return "출혈의 위험성(두개강 내, 두 개강 외)";
        //    if (p_value == "04") return "경미한 증상";
        //    if (p_value == "05") return "증상의 급속한 호전";
        //    if (p_value == "06") return "다른 질환으로 인한 것일 가능성이 있는 경우";
        //    if (p_value == "07") return "피검사에서 출혈 성향이 있는 것 (혈소판수치 10만이하, 비정상 aPTT, PT의 INR 1.5이상)";
        //    if (p_value == "08") return "조절되지 않은 고혈압";
        //    if (p_value == "09") return "환자 또는 보호자의 거부";
        //    if (p_value == "10") return "타병원에서 정맥내 혈전용해제(t-PA) 투여후 전원";
        //    if (p_value == "11") return "기록없음";
        //    if (p_value == "99") return "기타";
        //    return "";
        //}

        //private string GetMDS_INJC_NEXEC_RS_CD(string p_value)
        //{
        //    if (p_value == "증상발생(최종 정상확인) 시각으로부터 병원도착까지 4.5시간이 초과된 경우") return "01";
        //    if (p_value == "증상발생(최종 정상확인) 시각을 모르는 경우") return "02";
        //    if (p_value == "출혈의 위험성(두개강 내, 두 개강 외)") return "03";
        //    if (p_value == "경미한 증상") return "04";
        //    if (p_value == "증상의 급속한 호전") return "05";
        //    if (p_value == "다른 질환으로 인한 것일 가능성이 있는 경우") return "06";
        //    if (p_value == "피검사에서 출혈 성향이 있는 것 (혈소판수치 10만이하, 비정상 aPTT, PT의 INR 1.5이상)") return "07";
        //    if (p_value == "조절되지 않은 고혈압") return "08";
        //    if (p_value == "환자 또는 보호자의 거부") return "09";
        //    if (p_value == "타병원에서 정맥내 혈전용해제(t-PA) 투여후 전원") return "10";
        //    if (p_value == "기록없음") return "11";
        //    if (p_value == "기타") return "99";
        //    return "";
        //}

        //private string GetMN60_ECS_INJC_RS_CD2_NM(string p_value)
        //{
        //    if (p_value == "0") return "해당없음";
        //    if (p_value == "1") return "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우";
        //    if (p_value == "2") return "NIHSS 2점 이상 증가하여 증상이 악화되는 경우";
        //    if (p_value == "3") return "혈압이 표준진료지침에서 권고하는 수준 보다 높아 혈압 강하 치료가 우선 시행되었던 경우";
        //    if (p_value == "4") return "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우";
        //    if (p_value == "5") return "기록없음";
        //    return "";
        //}

        //private string GetMN60_ECS_INJC_RS_CD2(string p_value)
        //{
        //    if (p_value == "해당없음") return "0";
        //    if (p_value == "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우") return "1";
        //    if (p_value == "NIHSS 2점 이상 증가하여 증상이 악화되는 경우") return "2";
        //    if (p_value == "혈압이 표준진료지침에서 권고하는 수준 보다 높아 혈압 강하 치료가 우선 시행되었던 경우") return "3";
        //    if (p_value == "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우") return "4";
        //    if (p_value == "기록없음") return "5";
        //    return "";
        //}

        //private string GetMN120_ECS_EXEC_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "0") return "해당없음";
        //    if (p_value == "1") return "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우";
        //    if (p_value == "2") return "NIHSS 2점 이상 증가하여 증상이 악화되는 경우";
        //    if (p_value == "3") return "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우";
        //    if (p_value == "4") return "기록없음";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetMN120_ECS_EXEC_RS_CD(string p_value)
        //{
        //    if (p_value == "해당없음") return "0";
        //    if (p_value == "증상이 NIHSS 4점 이하로 경미하다가 5점 이상으로 악화되는 경우") return "1";
        //    if (p_value == "NIHSS 2점 이상 증가하여 증상이 악화되는 경우") return "2";
        //    if (p_value == "호흡곤란이나 활력징후가 불안정하여 기도삽관이 우선 시행되었던 경우") return "3";
        //    if (p_value == "기록없음") return "4";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetHR24_ECS_LAST_TRET_EXEC_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "초기 검사에서 파열된 뇌동맥류 증거가 없었던 경우";
        //    if (p_value == "2") return "기록없음";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetHR24_ECS_LAST_TRET_EXEC_RS_CD(string p_value)
        //{
        //    if (p_value == "초기 검사에서 파열된 뇌동맥류 증거가 없었던 경우") return "1";
        //    if (p_value == "기록없음") return "2";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {

            // --- A. 입·퇴원 정보 ---
            m_data.ASM_HOSP_ARIV_DT = CUtil.GetDateTime(txtASM_HOSP_ARIV_DT_DATE.Text, txtASM_HOSP_ARIV_DT_TIME.Text); // 병원도착일시
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2); // 퇴원 여부
            m_data.ASM_DSCG_DT = CUtil.GetDateTime(txtASM_DSCG_DT_DATE.Text, txtASM_DSCG_DT_TIME.Text); // 퇴원일시
            m_data.DSCG_STAT_RCD_YN = CUtil.GetRBString(rbDSCG_STAT_RCD_YN_1, rbDSCG_STAT_RCD_YN_2); // 퇴원상태 기록 여부
            m_data.DSCG_STAT_CD = CUtil.GetComboboxSelectedValue(cboDSCG_STAT_CD); // 퇴원상태 코드
            m_data.ACPH_DHI_RS_CD = CUtil.GetComboboxSelectedValue(cboACPH_DHI_RS_CD); // 전원사유 코드

            // 진단명
            List<string> diagCodes = new List<string>();
            if (chkACPH_CRST_DIAG_1.Checked) diagCodes.Add("1");
            if (chkACPH_CRST_DIAG_2.Checked) diagCodes.Add("2");
            if (chkACPH_CRST_DIAG_3.Checked) diagCodes.Add("3");
            if (chkACPH_CRST_DIAG_4.Checked) diagCodes.Add("4");
            if (chkACPH_CRST_DIAG_5.Checked) diagCodes.Add("5");
            if (chkACPH_CRST_DIAG_6.Checked) diagCodes.Add("6");
            if (chkACPH_CRST_DIAG_7.Checked) diagCodes.Add("7");
            if (chkACPH_CRST_DIAG_8.Checked) diagCodes.Add("8");
            if (chkACPH_CRST_DIAG_9.Checked) diagCodes.Add("9");
            m_data.ACPH_CRST_DIAG = string.Join("/", diagCodes.ToArray()); // 진단 코드 리스트

            m_data.ACPH_CRST_DIAG_ETC_TXT = txtACPH_CRST_DIAG_ETC_TXT.Text; // 기타 상세

            // 증상 및 연관 정보
            m_data.ACPH_CRST_REL_CD = CUtil.GetComboboxSelectedValue(cboACPH_CRST_REL_CD);
            m_data.CRST_SYMT_OCUR_CFR_CD = CUtil.GetComboboxSelectedValue(cboCRST_SYMT_OCUR_CFR_CD);
            m_data.SYMT_OCUR_DT = CUtil.GetDateTime(txtSYMT_OCUR_DT_DATE.Text, txtSYMT_OCUR_DT_TIME.Text);
            m_data.STAT_CFR_DT = CUtil.GetDateTime(txtSTAT_CFR_DT_DATE.Text, txtSTAT_CFR_DT_TIME.Text);
            m_data.LAST_NRM_CFR_DT = CUtil.GetDateTime(txtLAST_NRM_CFR_DT_DATE.Text, txtLAST_NRM_CFR_DT_TIME.Text);

            // 내원 관련
            m_data.ASM_FST_IPAT_PTH_CD = CUtil.GetComboboxSelectedValue(cboASM_FST_IPAT_PTH_CD);
            m_data.ASM_VST_PTH_CD = CUtil.GetComboboxSelectedValue(cboASM_VST_PTH_CD);
            m_data.VST_MTH_CD = CUtil.GetComboboxSelectedValue(cboVST_MTH_CD);

            // 전과
            m_data.RHBLTN_DGSBJT_TRFR_YN = CUtil.GetRBString(rbRHBLTN_DGSBJT_TRFR_YN_1, rbRHBLTN_DGSBJT_TRFR_YN_2);
            m_data.TRFR_DD = txtTRFR_DD.Text;

            // 연명의료 / 뇌사판정
            m_data.WLST_RCD_YN = CUtil.GetRBString(rbWLST_RCD_YN_1, rbWLST_RCD_YN_2);
            m_data.DEATH_PCS_PTNT_RCD_DT = txtDEATH_PCS_PTNT_RCD_DT.Text;
            m_data.BRN_DEATH_YN = CUtil.GetRBString(rbBRN_DEATH_YN_1, rbBRN_DEATH_YN_2);
            m_data.BRN_DEATH_DD = txtBRN_DEATH_DD.Text;

            // --- B. 진료 정보 ---
            // Stroke Scale
            m_data.CRST_SCL_ENFC_YN = CUtil.GetRBString(rbCRST_SCL_ENFC_YN_1, rbCRST_SCL_ENFC_YN_2);
            m_data.CRST_SCL_FST_EXEC_DD = txtCRST_SCL_FST_EXEC_DD.Text;
            m_data.CRST_SCL_KND_CD = CUtil.GetComboboxSelectedValue(cboCRST_SCL_KND_CD);
            m_data.ETC_CRST_SCL_NM = txtETC_CRST_SCL_NM.Text;
            m_data.ETC_CRST_SCL_HGH_PNT = txtETC_CRST_SCL_HGH_PNT.Text;
            m_data.NIHSS_PNT = txtNIHSS_PNT.Text;
            m_data.GCS_PNT = txtGCS_PNT.Text;
            m_data.ETC_CRST_SCL_ASM_PNT = txtETC_CRST_SCL_ASM_PNT.Text;

            // 기능평가
            m_data.FCLT_ASM_TL_ENFC_YN = CUtil.GetRBString(rbFCLT_ASM_TL_ENFC_YN_1, rbFCLT_ASM_TL_ENFC_YN_2);
            m_data.LAST_FCLT_ASM_TL_ENFC_DD = txtLAST_FCLT_ASM_TL_ENFC_DD.Text;
            m_data.DSCG_FCLT_ASM_TL_KND_CD = CUtil.GetComboboxSelectedValue(cboDSCG_FCLT_ASM_TL_KND_CD);
            m_data.ETC_FCLT_ASM_TL_TXT = txtETC_FCLT_ASM_TL_TXT.Text;
            m_data.ETC_FCLT_ASM_TL_HGH_PNT = txtETC_FCLT_ASM_TL_HGH_PNT.Text;
            m_data.KMBI_PNT = txtKMBI_PNT.Text;
            m_data.MBI_PNT = txtMBI_PNT.Text;
            m_data.BI_PNT = txtBI_PNT.Text;
            m_data.FIM_PNT = txtFIM_PNT.Text;
            m_data.MRS_GRD = txtMRS_GRD.Text;
            m_data.GOS_GRD = txtGOS_GRD.Text;
            m_data.ETC_FCLT_ASM_PNT = txtETC_FCLT_ASM_PNT.Text;

            // 조기재활
            m_data.RHBLTN_DDIAG_REQ_YN = CUtil.GetComboboxSelectedValue(cboRHBLTN_DDIAG_REQ_YN);
            m_data.FST_REQ_DD = txtFST_REQ_DD.Text;
            m_data.RHBLTN_DDIAG_FST_RPY_YN = CUtil.GetRBString(rbRHBLTN_DDIAG_FST_RPY_YN_1, rbRHBLTN_DDIAG_FST_RPY_YN_2);
            m_data.RPY_DD = txtRPY_DD.Text;
            m_data.RHBLTN_TRET_YN = CUtil.GetRBString(rbRHBLTN_TRET_YN_1, rbRHBLTN_TRET_YN_2);
            m_data.FST_TRET_DD = txtFST_TRET_DD.Text;
            m_data.FCLT_HDP_YN = CUtil.GetRBString(rbFCLT_HDP_YN_1, rbFCLT_HDP_YN_2);
            m_data.CLI_ISTBY_RS_CD = CUtil.GetComboboxSelectedValue(cboCLI_ISTBY_RS_CD);
            m_data.CLI_ISTBY_RS_ETC_TXT = txtCLI_ISTBY_RS_ETC_TXT.Text;
            m_data.CLI_ISTBY_RS_RCD_DD = txtCLI_ISTBY_RS_RCD_DD.Text;
            m_data.FCLT_HDP_ASM_YN = CUtil.GetRBString(rbFCLT_HDP_ASM_YN_1, rbFCLT_HDP_ASM_YN_2);
            m_data.FCLT_HDP_ASM_TL_KND_CD = CUtil.GetComboboxSelectedValue(cboFCLT_HDP_ASM_TL_KND_CD);
            m_data.FCLT_HDP_ASM_ETC_TL_TXT = txtFCLT_HDP_ASM_ETC_TL_TXT.Text;
            m_data.HDP_MRS_GRD = txtHDP_MRS_GRD.Text;
            m_data.HDP_NIHSS_PNT = txtHDP_NIHSS_PNT.Text;
            m_data.FCLT_HDP_ETC_ASM_PNT = txtFCLT_HDP_ETC_ASM_PNT.Text;
            m_data.FCLT_HDP_ASM_TL_EXEC_DD = txtFCLT_HDP_ASM_TL_EXEC_DD.Text;

            // 폐렴
            m_data.HR48_AF_PNEM_SICK_YN = CUtil.GetRBString(rbHR48_AF_PNEM_SICK_YN_1, rbHR48_AF_PNEM_SICK_YN_2);
            m_data.PNEM_KND_CD = CUtil.GetComboboxSelectedValue(cboPNEM_KND_CD);
            m_data.PNEM_KND_ETC_TXT = txtPNEM_KND_ETC_TXT.Text;
            m_data.DIAG_SICK_SYM = txtDIAG_SICK_SYM.Text;
            m_data.DIAG_NM = txtDIAG_NM.Text;
            m_data.ATFL_RPRT_YN = CUtil.GetRBString(rbATFL_RPRT_YN_1, rbATFL_RPRT_YN_2);
            m_data.ATFL_RPRT_FST_STA_DD = txtATFL_RPRT_FST_STA_DD.Text;
            m_data.ATFL_RPRT_FST_END_DD = txtATFL_RPRT_FST_END_DD.Text;

            // 허혈성 뇌졸중 - tPA
            m_data.DGM_INJC_YN = CUtil.GetRBString(rbDGM_INJC_YN_1, rbDGM_INJC_YN_2);
            m_data.MDS_INJC_DT = CUtil.GetDateTime(txtMDS_INJC_DT_DATE.Text, txtMDS_INJC_DT_TIME.Text);
            m_data.MDS_INJC_NEXEC_RS_CD = CUtil.GetComboboxSelectedValue(cboMDS_INJC_NEXEC_RS_CD);
            m_data.MDS_INJC_NEXEC_RS_ETC_TXT = txtMDS_INJC_NEXEC_RS_ETC_TXT.Text;
            m_data.MN60_ECS_INJC_RS_CD2 = CUtil.GetComboboxSelectedValue(cboMN60_ECS_INJC_RS_CD2);

            // 동맥내 혈전제거술
            m_data.INARTR_THBE_EXEC_YN = CUtil.GetRBString(rbINARTR_THBE_EXEC_YN_1, rbINARTR_THBE_EXEC_YN_2);
            m_data.THBE_EXEC_DT = CUtil.GetDateTime(txtTHBE_EXEC_DT_DATE.Text, txtTHBE_EXEC_DT_TIME.Text);
            m_data.MN120_ECS_EXEC_RS_CD = CUtil.GetComboboxSelectedValue(cboMN120_ECS_EXEC_RS_CD);
            m_data.MN120_ECS_EXEC_RS_ETC_TXT = txtMN120_ECS_EXEC_RS_ETC_TXT.Text;

            // 출혈성 뇌졸중 - 최종치료
            m_data.SBRC_HMRHG_LAST_TRET_EXEC_YN = CUtil.GetRBString(rbSBRC_HMRHG_LAST_TRET_EXEC_YN_1, rbSBRC_HMRHG_LAST_TRET_EXEC_YN_2);
            m_data.LAST_TRET_EXEC_DT = CUtil.GetDateTime(txtLAST_TRET_EXEC_DT_DATE.Text, txtLAST_TRET_EXEC_DT_TIME.Text);
            m_data.HR24_ECS_LAST_TRET_EXEC_RS_CD = CUtil.GetComboboxSelectedValue(cboHR24_ECS_LAST_TRET_EXEC_RS_CD);
            m_data.HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT = txtHR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT.Text;


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
                List<CDataASM003_002> list = (List<CDataASM003_002>)m_view.DataSource;
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
                List<CDataASM003_002> list = (List<CDataASM003_002>)m_view.DataSource;
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

                    CMakeASM003 make = new CMakeASM003();
                    tran = conn.BeginTransaction();
                    make.MakeASM003(m_data, sysdt, systm, m_User, conn, tran, true);
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

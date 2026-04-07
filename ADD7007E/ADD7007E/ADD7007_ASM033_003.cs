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
    public partial class ADD7007_ASM033_003 : Form
    {
        class IPAT
        {
            public string SPRM_IPAT_DT_DATE { get; set; }
            public string SPRM_IPAT_DT_TIME { get; set; }
            public string CLTR_ENFC_YN { get; set; }
            public string CLTR_RGN_CD { get; set; }
            public string CLTR_RGN_ETC_TXT { get; set; }
            public string CLTR_ISLTN_CD { get; set; }
            public string INFC_CFR_YN { get; set; }
            public string CLTR_AF_ISLTN_CD { get; set; }
            public string CLTR_NEXEC_RS_CD { get; set; }
            public string NBY_IPAT_RS_CD { get; set; }
            //public string RE_IPAT_RS_TXT { get; set; }
            //public string IPAT_RS_ETC_TXT { get; set; }
            public string ETC_TXT { get; set; }
            public string DSCG_DT_DATE { get; set; }
            public string DSCG_DT_TIME { get; set; }
            public string NBY_DSCG_PTH_CD { get; set; }
            public IPAT()
            {
                SPRM_IPAT_DT_DATE = "";
                SPRM_IPAT_DT_TIME = "";
                CLTR_ENFC_YN = "";
                CLTR_RGN_CD = "";
                CLTR_RGN_ETC_TXT = "";
                CLTR_ISLTN_CD = "";
                INFC_CFR_YN = "";
                CLTR_AF_ISLTN_CD = "";
                CLTR_NEXEC_RS_CD = "";
                NBY_IPAT_RS_CD = "";
                ETC_TXT = "";
                DSCG_DT_DATE = "";
                DSCG_DT_TIME = "";
                NBY_DSCG_PTH_CD = "";
            }
        }
        class SGRD
        {
            public string SGRD_ASM_MASR_DT_DATE { get; set; }
            public string SGRD_ASM_MASR_DT_TIME { get; set; }
            public string SGRD_ASM_KND_CD { get; set; }
            public string SGRD_ASM_KND_ETC_TXT { get; set; }
            public SGRD()
            {
                SGRD_ASM_MASR_DT_DATE = "";
                SGRD_ASM_MASR_DT_TIME = "";
                SGRD_ASM_KND_CD = "";
                SGRD_ASM_KND_ETC_TXT = "";
            }
        }
        class TPN
        {
            public string INJC_STA_DD { get; set; }
            public string INJC_END_DD { get; set; }
            public string TPN_DDIAG_YN { get; set; }
            public string DDIAG_NEXEC_RS_TXT { get; set; }
            public TPN()
            {
                INJC_STA_DD = "";
                INJC_END_DD = "";
                TPN_DDIAG_YN = "";
                DDIAG_NEXEC_RS_TXT = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM033_003 m_data;

        public ADD7007_ASM033_003()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM033_003(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM033_003> list = (List<CDataASM033_003>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 최소입실경로
            cboBIRTH_PTH_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "본원출생" },
                new { Key = "2", Value = "본원출생 - 퇴원 후 재입원" },				
                new { Key = "3", Value = "타기관 출생 후 전원" },
                new { Key = "4", Value = "타기관 출생 후 외래, 응급실 통해 입실" },
            };
            cboBIRTH_PTH_CD.DisplayMember = "Value";
            cboBIRTH_PTH_CD.ValueMember = "Key";
            // 출생장소
            cboBIRTH_PLC_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "본원" },
                new { Key = "2", Value = "타기관" },
                new { Key = "9", Value = "기타" },
            };
            cboBIRTH_PLC_CD.DisplayMember = "Value";
            cboBIRTH_PLC_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            // 2입실 4시간이내 감시배양 시행여부
            CUtil.SetGridCombo(grdIPATView.Columns["CLTR_ENFC_YN"],
                "",
                "Yes",
                "No"
                );

            // 감시배양부위
            CUtil.SetGridCombo(grdIPATView.Columns["CLTR_ENFC_YN"],
                "",
                "비강",
                "겨드랑이",
                "항문",
                "기타 "
                );

            // 감시 배양 시행시 격리여부
            CUtil.SetGridCombo(grdIPATView.Columns["CLTR_ISLTN_CD"],
                "",
                "격리실 1인 격리",
                "코호트 격리",
                "격리 안 함"
                );

            // 감염여부
            CUtil.SetGridCombo(grdIPATView.Columns["INFC_CFR_YN"],
                "",
                "Yes",
                "No"
                );

            // 감시배양 시행 후 격리여부
            CUtil.SetGridCombo(grdIPATView.Columns["CLTR_AF_ISLTN_CD"],
                "",
                "격리실 1인 격리",
                "코호트 격리",
                "격리 안 함"
                );

            // 감시배양 미시행 사유
            CUtil.SetGridCombo(grdIPATView.Columns["CLTR_NEXEC_RS_CD"],
                "",
                "해당없음",
                "선천성 감염",
                "선천성 기형"
                );

            // 입실 사유
            CUtil.SetGridCombo(grdIPATView.Columns["NBY_IPAT_RS_CD"],
                "",
                "미숙아 집중관찰",
                "저체중 출생아 집중관찰",							
                "재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우",							
                "특별한 처치 또는 관리가 필요한 경우",							
                "의료진의 치료 계획에 따라 예정된 재입실",
                "기타"
            );							

            // 퇴실경로
            CUtil.SetGridCombo(grdIPATView.Columns["NBY_DSCG_PTH_CD"],
                "",
                "퇴원",		
                "전실(신생아실, 병동)",		
                "전실(ICU)",		
                "전원",			
                "사망",
                "계속 입원"
                );			

            // 중증도 평가도구 종류
            CUtil.SetGridCombo(grdSGRDView.Columns["SGRD_ASM_KND_CD"],
                "",
                "SNAP",		
                "SNAP-PE",		
                "SNAP-Ⅱ",		
                "SNAPPE-Ⅱ", 		
                "NTISS", 		
                "CRIB-Ⅱ",		
                "CRIB-Ⅰ",
                "기타"
                );		

            // 집중영양칠팀 협진 여부
            CUtil.SetGridCombo(grdTPNView.Columns["TPN_DDIAG_YN"],
                "",
                "Yes",
                "No"
                );
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // --- A. 기본 정보 ---
            txtIPAT_DD.Text = m_data.IPAT_DD; // 입원일(YYYYMMDD)

            // 최초 입실 경로
            CUtil.SetComboboxSelectedValue(cboBIRTH_PTH_CD, m_data.BIRTH_PTH_CD);

            // 출생일 확인 여부
            rbBIRTH_YN_1.Checked = m_data.BIRTH_YN == "1";
            rbBIRTH_YN_2.Checked = m_data.BIRTH_YN == "2";

            // 출생일시
            txtBIRTH_DT_DATE.Text = CUtil.GetDate(m_data.BIRTH_DT); // YYYYMMDD
            txtBIRTH_DT_TIME.Text = CUtil.GetTime(m_data.BIRTH_DT); // HHMM

            // 출생장소
            CUtil.SetComboboxSelectedValue(cboBIRTH_PLC_CD, m_data.BIRTH_PLC_CD);

            txtBIRTH_PLC_ETC_TXT.Text = m_data.BIRTH_PLC_ETC_TXT; // 출생장소 기타상세

            // 분만형태
            rbASM_PARTU_FRM_CD_1.Checked = m_data.ASM_PARTU_FRM_CD == "1";
            rbASM_PARTU_FRM_CD_2.Checked = m_data.ASM_PARTU_FRM_CD == "2";

            // 재태기간
            var arr_1 = (m_data.FTUS_DEV_TRM + "/").Split('/');
            txtFTUS_DEV_TRM_WEEK.Text = arr_1[0];
            txtFTUS_DEV_TRM_DAY.Text = arr_1[1];

            // 다태아여부
            rbMEMB_YN_1.Checked = m_data.MEMB_YN == "1";
            rbMEMB_YN_2.Checked = m_data.MEMB_YN == "2";

            // 다태아 내용
            var arr_2 = (m_data.MEMB_TXT + "/").Split('/');
            txtMEMB_TXT_CNT.Text = arr_2[0];
            txtMEMB_TXT_ORD.Text = arr_2[1];

            txtNBY_BIRTH_BWGT.Text = m_data.NBY_BIRTH_BWGT; // 출생 시 체중(g)

            // --- B. 입실 및 퇴실 관련 항목 (그리드) ---
            var ipatList = new List<IPAT>();
            for (int i = 0; i < m_data.SPRM_IPAT_DT.Count; i++)
            {
                var data = new IPAT();
                data.SPRM_IPAT_DT_DATE = CUtil.GetDate(m_data.SPRM_IPAT_DT[i]); // 입원일시
                data.SPRM_IPAT_DT_TIME = CUtil.GetTime(m_data.SPRM_IPAT_DT[i]);
                data.CLTR_ENFC_YN = GetCLTR_ENFC_YN_NM(m_data.CLTR_ENFC_YN[i]); // 입실 24시간 이내 감시배양 시행 여부
                data.CLTR_RGN_CD = GetCLTR_RGN_CD_NM(m_data.CLTR_RGN_CD[i]); // 감시배양 부위
                data.CLTR_RGN_ETC_TXT = m_data.CLTR_RGN_ETC_TXT[i]; // 감시배양 부위 기타 상세
                data.CLTR_ISLTN_CD = GetCLTR_ISLTN_CD_NM(m_data.CLTR_ISLTN_CD[i]); // 감시배양 시행 시 격리여부
                data.INFC_CFR_YN = GetINFC_CFR_YN_NM(m_data.INFC_CFR_YN[i]); // 감염여부
                data.CLTR_AF_ISLTN_CD = GetCLTR_AF_ISLTN_CD_NM(m_data.CLTR_AF_ISLTN_CD[i]); // 감시배양 시행 후 격리여부
                data.CLTR_NEXEC_RS_CD = GetCLTR_NEXEC_RS_CD_NM(m_data.CLTR_NEXEC_RS_CD[i]); // 감시배양 미시행 사유
                data.NBY_IPAT_RS_CD = GetNBY_IPAT_RS_CD_NM(m_data.NBY_IPAT_RS_CD[i]); // 입실 사유
                data.ETC_TXT = m_data.NBY_IPAT_RS_CD[i] == "5" ? m_data.RE_IPAT_RS_TXT[i] : (m_data.NBY_IPAT_RS_CD[i] == "9" ? m_data.IPAT_RS_ETC_TXT[i] : ""); // 입실사유 기타 상세
                data.DSCG_DT_DATE = CUtil.GetDate(m_data.DSCG_DT[i]); // 퇴실일시
                data.DSCG_DT_TIME = CUtil.GetTime(m_data.DSCG_DT[i]);
                data.NBY_DSCG_PTH_CD = GetNBY_DSCG_PTH_CD_NM(m_data.NBY_DSCG_PTH_CD[i]); // 퇴실 경로
                ipatList.Add(data);
            }
            grdIPAT.DataSource = ipatList;

            // --- C. 진료 관련 항목 ---
            rbSGRD_ASM_ENFC_YN_1.Checked = m_data.SGRD_ASM_ENFC_YN == "1";
            rbSGRD_ASM_ENFC_YN_2.Checked = m_data.SGRD_ASM_ENFC_YN == "2";

            // --- C-1. 중증도 평가 시행 내용 (그리드) ---
            var sgrdList = new List<SGRD>();
            for (int i = 0; i < m_data.SGRD_ASM_MASR_DT.Count; i++)
            {
                var data = new SGRD();
                data.SGRD_ASM_MASR_DT_DATE = CUtil.GetDate(m_data.SGRD_ASM_MASR_DT[i]); // 측정일시
                data.SGRD_ASM_MASR_DT_TIME = CUtil.GetTime(m_data.SGRD_ASM_MASR_DT[i]);
                data.SGRD_ASM_KND_CD = GetSGRD_ASM_KND_CD_NM(m_data.SGRD_ASM_KND_CD[i]); // 중증도 평가도구
                data.SGRD_ASM_KND_ETC_TXT = m_data.SGRD_ASM_KND_ETC_TXT[i]; // 중증도 평가도구 상세
                sgrdList.Add(data);
            }
            grdSGRD.DataSource = sgrdList;
            grdSGRDView.RefreshData();

            // --- D. 집중영양치료 관련 항목 ---
            rbTPN_ENFC_YN_1.Checked = m_data.TPN_ENFC_YN == "1";
            rbTPN_ENFC_YN_2.Checked = m_data.TPN_ENFC_YN == "2";

            // --- D-1. TPN 투여일자 및 협진여부 (그리드) ---
            var tpnList = new List<TPN>();
            for (int i = 0; i < m_data.INJC_STA_DD.Count; i++)
            {
                var data = new TPN();
                data.INJC_STA_DD = m_data.INJC_STA_DD[i]; // 투여시작일
                data.INJC_END_DD = m_data.INJC_END_DD[i]; // 투여종료일
                data.TPN_DDIAG_YN = GetTPN_DDIAG_YN_NM(m_data.TPN_DDIAG_YN[i]); // 집중영양치료팀 협진 여부
                data.DDIAG_NEXEC_RS_TXT = m_data.DDIAG_NEXEC_RS_TXT[i]; // 협진 미실시 여부
                tpnList.Add(data);
            }
            grdTPN.DataSource = tpnList;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdIPAT, grdIPATView);
            CUtil.RefreshGrid(grdSGRD, grdSGRDView);
            CUtil.RefreshGrid(grdTPN, grdTPNView);
        }

        //private string GetBIRTH_PTH_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "본원출생";
        //    if (p_value == "2") return "본원출생 - 퇴원 후 재입원";				
        //    if (p_value == "3") return "타기관 출생 후 전원";
        //    if (p_value == "4") return "타기관 출생 후 외래, 응급실 통해 입실";
        //    return "";
        //}

        //private string GetBIRTH_PTH_CD(string p_value)
        //{
        //    if (p_value == "본원출생") return "1";
        //    if (p_value == "본원출생 - 퇴원 후 재입원") return "2";
        //    if (p_value == "타기관 출생 후 전원") return "3";
        //    if (p_value == "타기관 출생 후 외래, 응급실 통해 입실") return "4";
        //    return "";
        //}

        //private string GetBIRTH_PLC_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "본원";
        //    if (p_value == "2") return "타기관";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetBIRTH_PLC_CD(string p_value)
        //{
        //    if (p_value == "본원") return "1";
        //    if (p_value == "타기관") return "2";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        private string GetCLTR_ENFC_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetCLTR_ENFC_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private string GetCLTR_RGN_CD_NM(string p_value)
        {
            if (p_value == "1") return "비강";
            if (p_value == "2") return "겨드랑이";
            if (p_value == "3") return "항문";
            if (p_value == "9") return "기타 ";
            return "";
        }

        private string GetCLTR_RGN_CD(string p_value)
        {
            if (p_value == "비강") return "1";
            if (p_value == "겨드랑이") return "2";
            if (p_value == "항문") return "3";
            if (p_value == "기타 ") return "9";
            return "";
        }

        private string GetCLTR_ISLTN_CD_NM(string p_value)
        {
            if (p_value == "1") return "격리실 1인 격리";
            if (p_value == "2") return "코호트 격리";
            if (p_value == "3") return "격리 안 함";
            return "";
        }

        private string GetCLTR_ISLTN_CD(string p_value)
        {
            if (p_value == "격리실 1인 격리") return "1";
            if (p_value == "코호트 격리") return "2";
            if (p_value == "격리 안 함") return "3";
            return "";
        }

        private string GetINFC_CFR_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetINFC_CFR_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private string GetCLTR_AF_ISLTN_CD_NM(string p_value)
        {
            if (p_value == "1") return "격리실 1인 격리";
            if (p_value == "2") return "코호트 격리";
            if (p_value == "3") return "격리 안 함";
            return "";
        }

        private string GetCLTR_AF_ISLTN_CD(string p_value)
        {
            if (p_value == "격리실 1인 격리") return "1";
            if (p_value == "코호트 격리") return "2";
            if (p_value == "격리 안 함") return "3";
            return "";
        }

        private string GetCLTR_NEXEC_RS_CD_NM(string p_value)
        {
            if (p_value == "0") return "해당없음";
            if (p_value == "1") return "선천성 감염";
            if (p_value == "2") return "선천성 기형";
            return "";
        }

        private string GetCLTR_NEXEC_RS_CD(string p_value)
        {
            if (p_value == "해당없음") return "0";
            if (p_value == "선천성 감염") return "1";
            if (p_value == "선천성 기형") return "2";
            return "";
        }

        private string GetNBY_IPAT_RS_CD_NM(string p_value)
        {
            if (p_value == "1") return "미숙아 집중관찰";							
            if (p_value == "2") return "저체중 출생아 집중관찰";							
            if (p_value == "3") return "재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우";							
            if (p_value == "4") return "특별한 처치 또는 관리가 필요한 경우";							
            if (p_value == "5") return "의료진의 치료 계획에 따라 예정된 재입실";
            if (p_value == "9") return "기타";
            return "";				
        }

        private string GetNBY_IPAT_RS_CD(string p_value)
        {
            if (p_value == "미숙아 집중관찰") return "1";
            if (p_value == "저체중 출생아 집중관찰") return "2";
            if (p_value == "재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우") return "3";
            if (p_value == "특별한 처치 또는 관리가 필요한 경우") return "4";
            if (p_value == "의료진의 치료 계획에 따라 예정된 재입실") return "5";
            if (p_value == "기타") return "9";
            return "";
        }

        private string GetNBY_DSCG_PTH_CD_NM(string p_value)
        {
            if (p_value == "1") return "퇴원";		
            if (p_value == "2") return "전실(신생아실, 병동)";		
            if (p_value == "3") return "전실(ICU)";		
            if (p_value == "4") return "전원";			
            if (p_value == "5") return "사망";
            if (p_value == "6") return "계속 입원";
            return "";
        }

        private string GetNBY_DSCG_PTH_CD(string p_value)
        {
            if (p_value == "퇴원") return "1";
            if (p_value == "전실(신생아실, 병동)") return "2";
            if (p_value == "전실(ICU)") return "3";
            if (p_value == "전원") return "4";
            if (p_value == "사망") return "5";
            if (p_value == "계속 입원") return "6";
            return "";
        }

        private string GetSGRD_ASM_KND_CD_NM(string p_value)
        {
            if (p_value == "1") return "SNAP";		
            if (p_value == "2") return "SNAP-PE";		
            if (p_value == "3") return "SNAP-Ⅱ";		
            if (p_value == "4") return "SNAPPE-Ⅱ"; 		
            if (p_value == "5") return "NTISS"; 		
            if (p_value == "6") return "CRIB-Ⅱ";		
            if (p_value == "7") return "CRIB-Ⅰ";
            if (p_value == "9") return "기타";
            return "";
        }

        private string GetSGRD_ASM_KND_CD(string p_value)
        {
            if (p_value == "SNAP") return "1";
            if (p_value == "SNAP-PE") return "2";
            if (p_value == "SNAP-Ⅱ") return "3";
            if (p_value == "SNAPPE-Ⅱ") return "4";
            if (p_value == "NTISS") return "5";
            if (p_value == "CRIB-Ⅱ") return "6";
            if (p_value == "CRIB-Ⅰ") return "7";
            if (p_value == "기타") return "9";
            return "";
        }

        private string GetTPN_DDIAG_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetTPN_DDIAG_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- A. 기본 정보 ---
            m_data.IPAT_DD = txtIPAT_DD.Text.Trim(); // 입원일(YYYYMMDD)
            m_data.BIRTH_PTH_CD = CUtil.GetComboboxSelectedValue(cboBIRTH_PTH_CD); // 최초 입실 경로
            m_data.BIRTH_YN = CUtil.GetRBString(rbBIRTH_YN_1, rbBIRTH_YN_2); // 출생일 확인 여부
            m_data.BIRTH_DT = CUtil.GetDateTime(txtBIRTH_DT_DATE.Text.Trim(), txtBIRTH_DT_TIME.Text.Trim()); // 출생일시(YYYYMMDDHHMM)
            m_data.BIRTH_PLC_CD = CUtil.GetComboboxSelectedValue(cboBIRTH_PLC_CD); // 출생장소
            m_data.BIRTH_PLC_ETC_TXT = txtBIRTH_PLC_ETC_TXT.Text.Trim(); // 출생장소 기타상세
            m_data.ASM_PARTU_FRM_CD = CUtil.GetRBString(rbASM_PARTU_FRM_CD_1, rbASM_PARTU_FRM_CD_2); // 분만형태
            m_data.FTUS_DEV_TRM = txtFTUS_DEV_TRM_WEEK.Text.Trim() + "/" + txtFTUS_DEV_TRM_DAY.Text.Trim(); // 재태기간(주/일)
            m_data.MEMB_YN = CUtil.GetRBString(rbMEMB_YN_1, rbMEMB_YN_2); // 다태아여부
            m_data.MEMB_TXT = txtMEMB_TXT_CNT.Text.Trim() + "/" + txtMEMB_TXT_ORD.Text.Trim(); // 다태아 내용
            m_data.NBY_BIRTH_BWGT = txtNBY_BIRTH_BWGT.Text.Trim(); // 출생 시 체중(g)

            // --- B. 입실 및 퇴실 관련 항목 (그리드) ---
            m_data.SPRM_IPAT_DT.Clear();
            m_data.CLTR_ENFC_YN.Clear();
            m_data.CLTR_RGN_CD.Clear();
            m_data.CLTR_RGN_ETC_TXT.Clear();
            m_data.CLTR_ISLTN_CD.Clear();
            m_data.INFC_CFR_YN.Clear();
            m_data.CLTR_AF_ISLTN_CD.Clear();
            m_data.CLTR_NEXEC_RS_CD.Clear();
            m_data.NBY_IPAT_RS_CD.Clear();
            m_data.RE_IPAT_RS_TXT.Clear();
            m_data.IPAT_RS_ETC_TXT.Clear();
            m_data.DSCG_DT.Clear();
            m_data.NBY_DSCG_PTH_CD.Clear();

            var ipatList = grdIPAT.DataSource as List<IPAT>;
            foreach (var data in ipatList)
            {
                m_data.SPRM_IPAT_DT.Add(CUtil.GetDateTime(data.SPRM_IPAT_DT_DATE, data.SPRM_IPAT_DT_TIME)); // 입실일시(YYYYMMDDHHMM)
                m_data.CLTR_ENFC_YN.Add(GetCLTR_ENFC_YN(data.CLTR_ENFC_YN)); // 입실 24시간 이내 감시배양 시행 여부
                m_data.CLTR_RGN_CD.Add(GetCLTR_RGN_CD(data.CLTR_RGN_CD)); // 감시배양 부위
                m_data.CLTR_RGN_ETC_TXT.Add(data.CLTR_RGN_ETC_TXT); // 감시배양 부위 기타상세
                m_data.CLTR_ISLTN_CD.Add(GetCLTR_ISLTN_CD(data.CLTR_ISLTN_CD)); // 감시배양 시행 시 격리여부
                m_data.INFC_CFR_YN.Add(GetINFC_CFR_YN(data.INFC_CFR_YN)); // 감염여부
                m_data.CLTR_AF_ISLTN_CD.Add(GetCLTR_AF_ISLTN_CD(data.CLTR_AF_ISLTN_CD)); // 감시배양 시행 후 격리여부
                m_data.CLTR_NEXEC_RS_CD.Add(GetCLTR_NEXEC_RS_CD(data.CLTR_NEXEC_RS_CD)); // 감시배양 미시행 사유
                m_data.NBY_IPAT_RS_CD.Add(GetNBY_IPAT_RS_CD(data.NBY_IPAT_RS_CD)); // 입실 사유
                m_data.RE_IPAT_RS_TXT.Add(GetNBY_IPAT_RS_CD(data.NBY_IPAT_RS_CD) == "5" ? data.ETC_TXT : ""); // 입실사유 재입실 상세
                m_data.IPAT_RS_ETC_TXT.Add(GetNBY_IPAT_RS_CD(data.NBY_IPAT_RS_CD) == "9" ? data.ETC_TXT : ""); // 입실사유 기타 상세
                m_data.DSCG_DT.Add(CUtil.GetDateTime(data.DSCG_DT_DATE, data.DSCG_DT_TIME)); // 퇴실일시(YYYYMMDDHHMM)
                m_data.NBY_DSCG_PTH_CD.Add(GetNBY_DSCG_PTH_CD(data.NBY_DSCG_PTH_CD)); // 퇴실 경로
            }

            // --- C. 진료 관련 항목 ---
            m_data.SGRD_ASM_ENFC_YN = CUtil.GetRBString(rbSGRD_ASM_ENFC_YN_1, rbSGRD_ASM_ENFC_YN_2); // 중증도 평가 시행 여부

            // --- C-1. 중증도 평가 시행 내용 (그리드) ---
            m_data.SGRD_ASM_MASR_DT.Clear();
            m_data.SGRD_ASM_KND_CD.Clear();
            m_data.SGRD_ASM_KND_ETC_TXT.Clear();

            var sgrdList = grdSGRD.DataSource as List<SGRD>;
            foreach (var data in sgrdList)
            {
                m_data.SGRD_ASM_MASR_DT.Add(CUtil.GetDateTime(data.SGRD_ASM_MASR_DT_DATE, data.SGRD_ASM_MASR_DT_TIME)); // 측정일시(YYYYMMDDHHMM)
                m_data.SGRD_ASM_KND_CD.Add(GetSGRD_ASM_KND_CD(data.SGRD_ASM_KND_CD)); // 중증도 평가도구
                m_data.SGRD_ASM_KND_ETC_TXT.Add(data.SGRD_ASM_KND_ETC_TXT); // 중증도 평가도구 상세
            }

            // --- D. 집중영양치료 관련 항목 ---
            m_data.TPN_ENFC_YN = CUtil.GetRBString(rbTPN_ENFC_YN_1, rbTPN_ENFC_YN_2); // TPN 시행 여부

            // --- D-1. TPN 투여일자 및 협진여부 (그리드) ---
            m_data.INJC_STA_DD.Clear();
            m_data.INJC_END_DD.Clear();
            m_data.TPN_DDIAG_YN.Clear();
            m_data.DDIAG_NEXEC_RS_TXT.Clear();

            var tpnList = grdTPN.DataSource as List<TPN>;
            foreach (var data in tpnList)
            {
                m_data.INJC_STA_DD.Add(data.INJC_STA_DD); // 투여 시작일
                m_data.INJC_END_DD.Add(data.INJC_END_DD); // 투여 종료일
                m_data.TPN_DDIAG_YN.Add(GetTPN_DDIAG_YN(data.TPN_DDIAG_YN)); // 집중영양치료팀 협진 여부
                m_data.DDIAG_NEXEC_RS_TXT.Add(data.DDIAG_NEXEC_RS_TXT); // 협진 미실시 사유
            }

            // --- E. 기타 사항 ---
            //m_data.APND_DATA_NO = "";

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
                List<CDataASM033_003> list = (List<CDataASM033_003>)m_view.DataSource;
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
                List<CDataASM033_003> list = (List<CDataASM033_003>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowIPAT_Click(object sender, EventArgs e)
        {
            List<IPAT> list = (List<IPAT>)grdIPAT.DataSource;
            list.Add(new IPAT());
            RefreshGrid();
        }

        private void btnDelRowIPAT_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdIPATView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<IPAT> list = (List<IPAT>)grdIPAT.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowSGRD_Click(object sender, EventArgs e)
        {
            List<SGRD> list = (List<SGRD>)grdSGRD.DataSource;
            list.Add(new SGRD());
            RefreshGrid();
        }

        private void btnDelRowSGRD_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdSGRDView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<SGRD> list = (List<SGRD>)grdSGRD.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowTPN_Click(object sender, EventArgs e)
        {
            List<TPN> list = (List<TPN>)grdTPN.DataSource;
            list.Add(new TPN());
            RefreshGrid();
        }

        private void btnDelRowTPN_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdTPNView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<TPN> list = (List<TPN>)grdTPN.DataSource;
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

                    CMakeASM033 make = new CMakeASM033();
                    tran = conn.BeginTransaction();
                    make.MakeASM033(m_data, sysdt, systm, m_User, conn, tran, true);
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

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
    public partial class ADD7007_ASM008_002 : Form
    {
        public class OIST
        {
            public string EXM_NM { get; set; }         // 검사명
            public string MDFEE_CD { get; set; }       // 수가코드
            public string ENFC_DD { get; set; }        // 시행일자
            public string EXM_RST_TXT { get; set; }    // 결과 텍스트
            public OIST()
            {
                EXM_NM = "";         // 검사명
                MDFEE_CD = "";       // 수가코드
                ENFC_DD = "";        // 시행일자
                EXM_RST_TXT = "";    // 결과 텍스트
            }
        }

        public class CHS
        {
            public string EXM_NM { get; set; }
            public string MDFEE_CD { get; set; }
            public string ENFC_DD { get; set; }
            public string OIST_ENFC_YN { get; set; }
            public CHS()
            {
                EXM_NM = "";
                MDFEE_CD = "";
                ENFC_DD = "";
                OIST_ENFC_YN = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM008_002 m_data;

        public ADD7007_ASM008_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM008_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM008_002> list = (List<CDataASM008_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 퇴원형태
            cboDSCG_FRM_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "01", Value = "정상퇴원" },
                new { Key = "02", Value = "자의퇴원" },
                new { Key = "03", Value = "탈원" },
                new { Key = "04", Value = "가망없는 퇴원" },
                new { Key = "05", Value = "전원" },
                new { Key = "06", Value = "사망" },
                new { Key = "99", Value = "기타" },
            };
            cboDSCG_FRM_CD.DisplayMember = "Value";
            cboDSCG_FRM_CD.ValueMember = "Key";
            // 만성신부전원인상병
            cboCHRON_RNFL_CUZ_SICK_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "당뇨병" },
                new { Key = "2", Value = "고혈압" },
                new { Key = "3", Value = "사구체신염" },
                new { Key = "4", Value = "모름" },
                new { Key = "9", Value = "기타" },
            };
            cboCHRON_RNFL_CUZ_SICK_CD.DisplayMember = "Value";
            cboCHRON_RNFL_CUZ_SICK_CD.ValueMember = "Key";
            // 주요 혈관통로(해당 월)
            cboMAIN_BLDVE_CH_DECS_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "자가혈관 동정맥루(AVF)" },
                new { Key = "2", Value = "인조혈관 동정맥루(AVG)" },
                new { Key = "3", Value = "카테터" },
            };
            cboMAIN_BLDVE_CH_DECS_CD.DisplayMember = "Value";
            cboMAIN_BLDVE_CH_DECS_CD.ValueMember = "Key";
            // 사용한 혈관통로(투석일)
            cboUSE_BLDVE_CH_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "자가혈관 동정맥루(AVF)" },
                new { Key = "2", Value = "인조혈관 동정맥루(AVG)" },
                new { Key = "3", Value = "카테터" },
            };
            cboUSE_BLDVE_CH_CD.DisplayMember = "Value";
            cboUSE_BLDVE_CH_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            CUtil.SetGridCombo(grdCHSView.Columns["OIST_ENFC_YN"],
                "",
                "Yes",
                "No"
                );
        }

        private void ShowData()
        {
            // 주민번호 등 공통 정보 표시
            txtPid.Text = m_data.PID;                       // 환자 ID 표시
            txtPnm.Text = m_data.PNM;                       // 환자명 표시
            txtResid_disp.Text = m_data.RESID_DISP;         // 주민번호 표시

            // [A] 기본정보

            rbRECU_EQP_ADM_YN_1.Checked = m_data.RECU_EQP_ADM_YN == "1"; // 요양병원 여부: 예
            rbRECU_EQP_ADM_YN_2.Checked = m_data.RECU_EQP_ADM_YN == "2"; // 요양병원 여부: 아니오

            rbIPAT_OPAT_TP_CD_1.Checked = m_data.IPAT_OPAT_TP_CD == "1"; // 진료형태: 입원
            rbIPAT_OPAT_TP_CD_2.Checked = m_data.IPAT_OPAT_TP_CD == "2"; // 진료형태: 외래

            txtIPAT_DT_DATE.Text = CUtil.GetDate(m_data.IPAT_DT);   // 입원일자 분리
            txtIPAT_DT_TIME.Text = CUtil.GetTime(m_data.IPAT_DT);   // 입원시간 분리

            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1"; // 퇴원 여부: 예
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2"; // 퇴원 여부: 아니오

            CUtil.SetComboboxSelectedValue(cboDSCG_FRM_CD, m_data.DSCG_FRM_CD); // 퇴원형태 설정 (코드 → 명칭)

            // 외래 내원상태 체크박스 설정
            var vstStatCodes = m_data.VST_STAT_CD.Split('/');
            chkVST_STAT_CD_01.Checked = vstStatCodes.Contains("01");
            chkVST_STAT_CD_02.Checked = vstStatCodes.Contains("02");
            chkVST_STAT_CD_03.Checked = vstStatCodes.Contains("03");
            chkVST_STAT_CD_04.Checked = vstStatCodes.Contains("04");
            chkVST_STAT_CD_05.Checked = vstStatCodes.Contains("05");
            chkVST_STAT_CD_99.Checked = vstStatCodes.Contains("99");

            txtBLDD_FST_STA_DD.Text = m_data.BLDD_FST_STA_DD;           // 최초 혈액투석 시작일
            txtBLDD_HOFC_FST_STA_DD.Text = m_data.BLDD_HOFC_FST_STA_DD; // 본원 최초 시작일

            // [B] 환자 정보 

            // 만성신부전 원인상병
            CUtil.SetComboboxSelectedValue(cboCHRON_RNFL_CUZ_SICK_CD, m_data.CHRON_RNFL_CUZ_SICK_CD);

            // 심장질환
            rbHRT_CCM_SICK_YN_1.Checked = m_data.HRT_CCM_SICK_YN == "1"; // 심장질환 여부: 예
            rbHRT_CCM_SICK_YN_2.Checked = m_data.HRT_CCM_SICK_YN == "2"; // 심장질환 여부: 아니오
            txtHRT_SICK_SYM.Text = m_data.HRT_SICK_SYM;                  // 상병기호

            // 심장질환 - 심부전
            rbCDFL_YN_1.Checked = m_data.CDFL_YN == "1"; // 심부전 여부: 예
            rbCDFL_YN_2.Checked = m_data.CDFL_YN == "2"; // 심부전 여부: 아니오

            var cdflCodes = m_data.CDFL_CD.Split('/');
            chkCDFL_CD_00.Checked = cdflCodes.Contains("00");
            chkCDFL_CD_01.Checked = cdflCodes.Contains("01");
            chkCDFL_CD_02.Checked = cdflCodes.Contains("02");

            txtLVEF_CNT.Text = m_data.LVEF_CNT;   // 좌심실 박출률
            txtCT_RT_CNT.Text = m_data.CT_RT_CNT; // 심흉곽비

            // 심장질환 - 심방조동 및 세동
            rbATFB_YN_1.Checked = m_data.ATFB_YN == "1"; // 세동 여부: 예
            rbATFB_YN_2.Checked = m_data.ATFB_YN == "2"; // 세동 여부: 아니오

            var atfbCodes = m_data.ATFB_CD.Split('/');
            chkATFB_CD_00.Checked = atfbCodes.Contains("00");
            chkATFB_CD_01.Checked = atfbCodes.Contains("01");
            chkATFB_CD_02.Checked = atfbCodes.Contains("02");

            // 심장질환 - 허혈성 심장병
            rbISMA_HRT_DS_YN_1.Checked = m_data.ISMA_HRT_DS_YN == "1"; // 허혈성 심장병 여부: 예
            rbISMA_HRT_DS_YN_2.Checked = m_data.ISMA_HRT_DS_YN == "2"; // 허혈성 심장병 여부: 아니오

            var ismaCodes = m_data.ISMA_HRT_DS_CD.Split('/');
            chkISMA_HRT_DS_CD_00.Checked = ismaCodes.Contains("00");
            chkISMA_HRT_DS_CD_01.Checked = ismaCodes.Contains("01");
            chkISMA_HRT_DS_CD_02.Checked = ismaCodes.Contains("02");
            chkISMA_HRT_DS_CD_03.Checked = ismaCodes.Contains("03");

            // 심장질환 - 심장판막 치환 등 개심수술 여부
            rbOHS_YN_1.Checked = m_data.OHS_YN == "1";
            rbOHS_YN_2.Checked = m_data.OHS_YN == "2";

            // 뇌혈괄질환
            rbCRBL_CCM_SICK_YN_1.Checked = m_data.CRBL_CCM_SICK_YN == "1";
            rbCRBL_CCM_SICK_YN_2.Checked = m_data.CRBL_CCM_SICK_YN == "2";

            txtCRBL_SICK_SYM.Text = m_data.CRBL_SICK_SYM;

            rbCRBL_HDP_YN_1.Checked = m_data.CRBL_HDP_YN == "1";
            rbCRBL_HDP_YN_2.Checked = m_data.CRBL_HDP_YN == "2";

            rbASTC_REQR_YN_1.Checked = m_data.ASTC_REQR_YN == "1";
            rbASTC_REQR_YN_2.Checked = m_data.ASTC_REQR_YN == "2";

            // 간경변증
            rbLVCR_CCM_SICK_YN_1.Checked = m_data.LVCR_CCM_SICK_YN == "1";
            rbLVCR_CCM_SICK_YN_2.Checked = m_data.LVCR_CCM_SICK_YN == "2";

            txtLVCR_SICK_SYM.Text = m_data.LUNG_SICK_SYM;

            var lvcrCodes = m_data.LVCR_SYMT_CD.Split('/');
            chkLVCR_SYMT_CD_00.Checked = lvcrCodes.Contains("00");
            chkLVCR_SYMT_CD_01.Checked = lvcrCodes.Contains("01");
            chkLVCR_SYMT_CD_02.Checked = lvcrCodes.Contains("02");
            chkLVCR_SYMT_CD_03.Checked = lvcrCodes.Contains("03");

            txtREMN_LVR_FCLT_EXM_PNT.Text = m_data.REMN_LVR_FCLT_EXM_PNT;

            // 출혈성위장관질환
            rbHMRHG_CCM_SICK_YN_1.Checked = m_data.HMRHG_CCM_SICK_YN == "1";
            rbHMRHG_CCM_SICK_YN_2.Checked = m_data.HMRHG_CCM_SICK_YN == "2";

            txtHMRHG_SICK_SYM.Text = m_data.HMRHG_SICK_SYM;

            var hmrhgCodes = m_data.HMRHG_GIT_DS_CD.Split('/');
            chkHMRHG_GIT_DS_CD_00.Checked = hmrhgCodes.Contains("00");
            chkHMRHG_GIT_DS_CD_01.Checked = hmrhgCodes.Contains("01");
            chkHMRHG_GIT_DS_CD_02.Checked = hmrhgCodes.Contains("02");

            // 만성폐질환
            rbLUNG_CCM_SICK_YN_1.Checked = m_data.LUNG_CCM_SICK_YN == "1";
            rbLUNG_CCM_SICK_YN_2.Checked = m_data.LUNG_CCM_SICK_YN == "2";

            txtLUNG_SICK_SYM.Text = m_data.LUNG_SICK_SYM;
            txtARTR_BLDVE_OXY_PART_PRES.Text = m_data.ARTR_BLDVE_OXY_PART_PRES;

            // 악성종양
            rbTMR_CCM_SICK_YN_1.Checked = m_data.TMR_CCM_SICK_YN == "1";
            rbTMR_CCM_SICK_YN_2.Checked = m_data.TMR_CCM_SICK_YN == "2";

            txtTMR_SICK_SYM.Text = m_data.TMR_SICK_SYM;

            var tmrCodes = m_data.MNPLS_TMR_TRET_CD.Split('/');
            chkMNPLS_TMR_TRET_CD_00.Checked = tmrCodes.Contains("00");
            chkMNPLS_TMR_TRET_CD_01.Checked = tmrCodes.Contains("01");
            chkMNPLS_TMR_TRET_CD_02.Checked = tmrCodes.Contains("02");

            // 당뇨병
            rbDBML_CCM_SICK_YN_1.Checked = m_data.DBML_CCM_SICK_YN == "1";
            rbDBML_CCM_SICK_YN_2.Checked = m_data.DBML_CCM_SICK_YN == "2";

            txtDBML_SICK_SYM.Text = m_data.DBML_SICK_SYM;

            rbINSL_IJCT_INJC_YN_1.Checked = m_data.INSL_IJCT_INJC_YN == "1";
            rbINSL_IJCT_INJC_YN_2.Checked = m_data.INSL_IJCT_INJC_YN == "2";

            // 3급 이상 장애인 여부
            rbHDP_YN_1.Checked = m_data.HDP_YN == "1";
            rbHDP_YN_2.Checked = m_data.HDP_YN == "2";

            var hdpCodes = m_data.HDP_TY_CD.Split('/');
            chkHDP_TY_CD_01.Checked = hdpCodes.Contains("01");
            chkHDP_TY_CD_02.Checked = hdpCodes.Contains("02");
            chkHDP_TY_CD_03.Checked = hdpCodes.Contains("03");
            chkHDP_TY_CD_04.Checked = hdpCodes.Contains("04");
            chkHDP_TY_CD_05.Checked = hdpCodes.Contains("05");
            chkHDP_TY_CD_06.Checked = hdpCodes.Contains("06");
            chkHDP_TY_CD_07.Checked = hdpCodes.Contains("07");
            chkHDP_TY_CD_08.Checked = hdpCodes.Contains("08");
            chkHDP_TY_CD_09.Checked = hdpCodes.Contains("09");
            chkHDP_TY_CD_10.Checked = hdpCodes.Contains("10");
            

            // [C] 투석 정보

            txtBLDD_STA_DT_DATE.Text = CUtil.GetDate(m_data.BLDD_STA_DT); // 투석 일자
            txtBLDD_STA_DT_TIME.Text = CUtil.GetTime(m_data.BLDD_STA_DT); // 투석 시간

            txtHEIG.Text = m_data.HEIG;         // 키

            rbDLYS_BWGT_YN_1.Checked = m_data.DLYS_BWGT_YN == "1"; // 건체중 측정 여부
            rbDLYS_BWGT_YN_2.Checked = m_data.DLYS_BWGT_YN == "2";

            txtASM_DLYS_BWGT.Text = m_data.ASM_DLYS_BWGT; // 건체중

            rbBF_BWGT_YN_1.Checked = m_data.BF_BWGT_YN == "1"; // 투석 전 체중 측정 여부
            rbBF_BWGT_YN_2.Checked = m_data.BF_BWGT_YN == "2";

            txtASM_BF_BWGT.Text = m_data.ASM_BF_BWGT; // 투석 전 체중

            rbAF_BWGT_YN_1.Checked = m_data.AF_BWGT_YN == "1"; // 투석 후 체중 측정 여부
            rbAF_BWGT_YN_2.Checked = m_data.AF_BWGT_YN == "2";

            txtASM_AF_BWGT.Text = m_data.ASM_AF_BWGT; // 투석 후 체중

            // 혈액투석 적절도
            rbBLDD_PPRT_ASM_YN_1.Checked = m_data.BLDD_PPRT_ASM_YN == "1"; // 혈액투석 적절도 평가 여부
            rbBLDD_PPRT_ASM_YN_2.Checked = m_data.BLDD_PPRT_ASM_YN == "2";

            var blddCodes = m_data.BLDD_PPRT_ASM_CD.Split('/'); // 혈액투석 적절도 평가 상세
            chkBLDD_PPRT_ASM_CD_01.Checked = blddCodes.Contains("01");
            chkBLDD_PPRT_ASM_CD_02.Checked = blddCodes.Contains("02");

            txtPPRT_NUV.Text = m_data.PPRT_NUV;
            txtBLUR_DCR_RT.Text = m_data.BLUR_DCR_RT;

            // 혈관통로
            CUtil.SetComboboxSelectedValue(cboMAIN_BLDVE_CH_DECS_CD, m_data.MAIN_BLDVE_CH_DECS_CD);
            CUtil.SetComboboxSelectedValue(cboUSE_BLDVE_CH_CD, m_data.USE_BLDVE_CH_CD);

            // 빈혈관리
            rbHMTP_INJC_YN_1.Checked = m_data.HMTP_INJC_YN == "1";
            rbHMTP_INJC_YN_2.Checked = m_data.HMTP_INJC_YN == "2";

            txtHMTP_INJC_DT_DATE.Text = CUtil.GetDate(m_data.HMTP_INJC_DT);
            txtHMTP_INJC_DT_TIME.Text = CUtil.GetTime(m_data.HMTP_INJC_DT);

            // 기능검사
            rbECG_ENFC_YN_1.Checked = m_data.ECG_ENFC_YN == "1";
            rbECG_ENFC_YN_2.Checked = m_data.ECG_ENFC_YN == "2";

            txtECG_ENFC_DT_DATE.Text = CUtil.GetDate(m_data.ECG_ENFC_DT);
            txtECG_ENFC_DT_TIME.Text = CUtil.GetTime(m_data.ECG_ENFC_DT);

            // 타기관 검사
            rbOIST_EXM_ENFC_YN_1.Checked = m_data.OIST_EXM_ENFC_YN == "1";
            rbOIST_EXM_ENFC_YN_2.Checked = m_data.OIST_EXM_ENFC_YN == "2";

            // 티가관 검사 검사정보
            var oistList = new List<OIST>();
            for (int i = 0; i < m_data.OIST_EXM_NM.Count; i++)
            {
                oistList.Add(new OIST
                {
                    EXM_NM = m_data.OIST_EXM_NM[i],                          // 검사명
                    MDFEE_CD = m_data.OIST_MDFEE_CD[i],                      // 수가코드
                    ENFC_DD = m_data.OIST_ENFC_DD[i],                        // 검사일자
                    EXM_RST_TXT = m_data.OIST_EXM_RST_TXT[i]                 // 결과 텍스트
                });
            }
            grdOIST.DataSource = oistList;

            // 추적관리
            rbCHS_ENFC_YN_1.Checked = m_data.CHS_ENFC_YN == "1";
            rbCHS_ENFC_YN_2.Checked = m_data.CHS_ENFC_YN == "2";

            // 추적관리 검사정보
            var chsList = new List<CHS>();
            for (int i = 0; i < m_data.CHS_EXM_NM.Count; i++)
            {
                chsList.Add(new CHS
                {
                    EXM_NM = m_data.CHS_EXM_NM[i],
                    MDFEE_CD = m_data.CHS_MDFEE_CD[i],
                    ENFC_DD = m_data.CHS_ENFC_DD[i],
                    OIST_ENFC_YN = GetOIST_ENFC_YN_NM(m_data.CHS_OIST_ENFC_YN[i])
                });
            }
            grdCHS.DataSource = chsList;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdOIST, grdOISTView);
            CUtil.RefreshGrid(grdCHS, grdCHSView);
        }

        //private string GetDSCG_FRM_CD_NM(string p_value)
        //{
        //    if (p_value == "01") return "정상퇴원";
        //    if (p_value == "02") return "자의퇴원";
        //    if (p_value == "03") return "탈원";
        //    if (p_value == "04") return "가망없는 퇴원";
        //    if (p_value == "05") return "전원";
        //    if (p_value == "06") return "사망";
        //    if (p_value == "99") return "기타";
        //    return "";
        //}

        //private string GetDSCG_FRM_CD(string p_value)
        //{
        //    if (p_value == "정상퇴원") return "01";
        //    if (p_value == "자의퇴원") return "02";
        //    if (p_value == "탈원") return "03";
        //    if (p_value == "가망없는 퇴원") return "04";
        //    if (p_value == "전원") return "05";
        //    if (p_value == "사망") return "06";
        //    if (p_value == "기타") return "99";
        //    return "";
        //}

        //private string GetCHRON_RNFL_CUZ_SICK_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "당뇨병";
        //    if (p_value == "2") return "고혈압";
        //    if (p_value == "3") return "사구체신염";
        //    if (p_value == "4") return "모름";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetCHRON_RNFL_CUZ_SICK_CD(string p_value)
        //{
        //    if (p_value == "당뇨병") return "1";
        //    if (p_value == "고혈압") return "2";
        //    if (p_value == "사구체신염") return "3";
        //    if (p_value == "모름") return "4";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetTTL_PTNT_INFM_NM(string p_value)
        //{
        //    if (p_value == "1") return "당뇨병";
        //    if (p_value == "2") return "고혈압";
        //    if (p_value == "3") return "사구체신염";
        //    if (p_value == "4") return "모름";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetTTL_PTNT_INFM(string p_value)
        //{
        //    if (p_value == "당뇨병") return "1";
        //    if (p_value == "고혈압") return "2";
        //    if (p_value == "사구체신염") return "3";
        //    if (p_value == "모름") return "4";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //public string GetMAIN_BLDVE_CH_DECS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "자가혈관 동정맥루(AVF)";
        //    if (p_value == "2") return "인조혈관 동정맥루(AVG)";
        //    if (p_value == "3") return "카테터";
        //    return "";
        //}

        //public string GetMAIN_BLDVE_CH_DECS_CD(string p_value)
        //{
        //    if (p_value == "자가혈관 동정맥루(AVF)") return "1";
        //    if (p_value == "인조혈관 동정맥루(AVG)") return "2";
        //    if (p_value == "카테터") return "3";
        //    return "";
        //}

        //public string GetUSE_BLDVE_CH_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "자가혈관 동정맥루(AVF)";
        //    if (p_value == "2") return "인조혈관 동정맥루(AVG)";
        //    if (p_value == "3") return "카테터";
        //    return "";
        //}

        //public string GetUSE_BLDVE_CH_CD(string p_value)
        //{
        //    if (p_value == "자가혈관 동정맥루(AVF)") return "1";
        //    if (p_value == "인조혈관 동정맥루(AVG)") return "2";
        //    if (p_value == "카테터") return "3";
        //    return "";
        //}

        private string GetOIST_ENFC_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetOIST_ENFC_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // A. 기본 정보
            m_data.RECU_EQP_ADM_YN = CUtil.GetRBString(rbRECU_EQP_ADM_YN_1, rbRECU_EQP_ADM_YN_2); // 요양병원 입원 여부
            m_data.IPAT_OPAT_TP_CD = CUtil.GetRBString(rbIPAT_OPAT_TP_CD_1, rbIPAT_OPAT_TP_CD_2); // 진료형태
            m_data.IPAT_DT = CUtil.GetDateTime(txtIPAT_DT_DATE.Text, txtIPAT_DT_TIME.Text); // 입원일시
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2); // 퇴원여부
            m_data.DSCG_FRM_CD = CUtil.GetComboboxSelectedValue(cboDSCG_FRM_CD); // 퇴원형태코드

            List<string> vstStatCodes = new List<string>();
            if (chkVST_STAT_CD_01.Checked) vstStatCodes.Add("01");
            if (chkVST_STAT_CD_02.Checked) vstStatCodes.Add("02");
            if (chkVST_STAT_CD_03.Checked) vstStatCodes.Add("03");
            if (chkVST_STAT_CD_04.Checked) vstStatCodes.Add("04");
            if (chkVST_STAT_CD_05.Checked) vstStatCodes.Add("05");
            if (chkVST_STAT_CD_99.Checked) vstStatCodes.Add("99");
            m_data.VST_STAT_CD = string.Join("/", vstStatCodes.ToArray()); // 내원상태코드

            m_data.BLDD_FST_STA_DD = txtBLDD_FST_STA_DD.Text; // 최초 혈액투석일
            m_data.BLDD_HOFC_FST_STA_DD = txtBLDD_HOFC_FST_STA_DD.Text; // 본원 시작일

            // B. 환자 정보
            m_data.CHRON_RNFL_CUZ_SICK_CD = CUtil.GetComboboxSelectedValue(cboCHRON_RNFL_CUZ_SICK_CD);

            // 심장질환
            m_data.HRT_CCM_SICK_YN = CUtil.GetRBString(rbHRT_CCM_SICK_YN_1, rbHRT_CCM_SICK_YN_2);
            m_data.HRT_SICK_SYM = txtHRT_SICK_SYM.Text;

            // 심장질환 - 심부전
            m_data.CDFL_YN = CUtil.GetRBString(rbCDFL_YN_1, rbCDFL_YN_2);
            List<string> cdflCodes = new List<string>();
            if (chkCDFL_CD_00.Checked) cdflCodes.Add("00");
            if (chkCDFL_CD_01.Checked) cdflCodes.Add("01");
            if (chkCDFL_CD_02.Checked) cdflCodes.Add("02");
            m_data.CDFL_CD = string.Join("/", cdflCodes.ToArray());

            m_data.LVEF_CNT = txtLVEF_CNT.Text;
            m_data.CT_RT_CNT = txtCT_RT_CNT.Text;

            // 심장질환 - 심방조동 및 세동
            m_data.ATFB_YN = CUtil.GetRBString(rbATFB_YN_1, rbATFB_YN_2);
            List<string> atfbCodes = new List<string>();
            if (chkATFB_CD_00.Checked) atfbCodes.Add("00");
            if (chkATFB_CD_01.Checked) atfbCodes.Add("01");
            if (chkATFB_CD_02.Checked) atfbCodes.Add("02");
            m_data.ATFB_CD = string.Join("/", atfbCodes.ToArray());

            // 심장질환 - 허혈성 심장병
            m_data.ISMA_HRT_DS_YN = CUtil.GetRBString(rbISMA_HRT_DS_YN_1, rbISMA_HRT_DS_YN_2);
            List<string> ismaCodes = new List<string>();
            if (chkISMA_HRT_DS_CD_00.Checked) ismaCodes.Add("00");
            if (chkISMA_HRT_DS_CD_01.Checked) ismaCodes.Add("01");
            if (chkISMA_HRT_DS_CD_02.Checked) ismaCodes.Add("02");
            if (chkISMA_HRT_DS_CD_03.Checked) ismaCodes.Add("03");
            m_data.ISMA_HRT_DS_CD = string.Join("/", ismaCodes.ToArray());

            m_data.OHS_YN = CUtil.GetRBString(rbOHS_YN_1, rbOHS_YN_2);

            // 뇌혈관 질환
            m_data.CRBL_CCM_SICK_YN = CUtil.GetRBString(rbCRBL_CCM_SICK_YN_1, rbCRBL_CCM_SICK_YN_2);
            m_data.CRBL_SICK_SYM = txtCRBL_SICK_SYM.Text;
            m_data.CRBL_HDP_YN = CUtil.GetRBString(rbCRBL_HDP_YN_1, rbCRBL_HDP_YN_2);
            m_data.ASTC_REQR_YN = CUtil.GetRBString(rbASTC_REQR_YN_1, rbASTC_REQR_YN_2);

            // 간경병증
            m_data.LVCR_CCM_SICK_YN = CUtil.GetRBString(rbLVCR_CCM_SICK_YN_1, rbLVCR_CCM_SICK_YN_2);
            m_data.LVCR_SICK_SYM = txtLVCR_SICK_SYM.Text;
            List<string> lvcrCodes = new List<string>();
            if (chkLVCR_SYMT_CD_00.Checked) lvcrCodes.Add("00");
            if (chkLVCR_SYMT_CD_01.Checked) lvcrCodes.Add("01");
            if (chkLVCR_SYMT_CD_02.Checked) lvcrCodes.Add("02");
            if (chkLVCR_SYMT_CD_03.Checked) lvcrCodes.Add("03");
            m_data.LVCR_SYMT_CD = string.Join("/", lvcrCodes.ToArray());
            m_data.REMN_LVR_FCLT_EXM_PNT = txtREMN_LVR_FCLT_EXM_PNT.Text;

            // 출혈성위장관질환
            m_data.HMRHG_CCM_SICK_YN = CUtil.GetRBString(rbHMRHG_CCM_SICK_YN_1, rbHMRHG_CCM_SICK_YN_2);
            m_data.HMRHG_SICK_SYM = txtHMRHG_SICK_SYM.Text;
            List<string> hmrhgCodes = new List<string>();
            if (chkHMRHG_GIT_DS_CD_00.Checked) hmrhgCodes.Add("00");
            if (chkHMRHG_GIT_DS_CD_01.Checked) hmrhgCodes.Add("01");
            if (chkHMRHG_GIT_DS_CD_02.Checked) hmrhgCodes.Add("02");
            m_data.HMRHG_GIT_DS_CD = string.Join("/", hmrhgCodes.ToArray());

            // 만성폐질환
            m_data.LUNG_CCM_SICK_YN = CUtil.GetRBString(rbLUNG_CCM_SICK_YN_1, rbLUNG_CCM_SICK_YN_2);
            m_data.LUNG_SICK_SYM = txtLUNG_SICK_SYM.Text;
            m_data.ARTR_BLDVE_OXY_PART_PRES = txtARTR_BLDVE_OXY_PART_PRES.Text;

            // 악성종양
            m_data.TMR_CCM_SICK_YN = CUtil.GetRBString(rbTMR_CCM_SICK_YN_1, rbTMR_CCM_SICK_YN_2);
            m_data.TMR_SICK_SYM = txtTMR_SICK_SYM.Text;
            List<string> tmrCodes = new List<string>();
            if (chkMNPLS_TMR_TRET_CD_00.Checked) tmrCodes.Add("00");
            if (chkMNPLS_TMR_TRET_CD_01.Checked) tmrCodes.Add("01");
            if (chkMNPLS_TMR_TRET_CD_02.Checked) tmrCodes.Add("02");
            m_data.MNPLS_TMR_TRET_CD = string.Join("/", tmrCodes.ToArray());

            // 당뇨병
            m_data.DBML_CCM_SICK_YN = CUtil.GetRBString(rbDBML_CCM_SICK_YN_1, rbDBML_CCM_SICK_YN_2);
            m_data.DBML_SICK_SYM = txtDBML_SICK_SYM.Text;
            m_data.INSL_IJCT_INJC_YN = CUtil.GetRBString(rbINSL_IJCT_INJC_YN_1, rbINSL_IJCT_INJC_YN_2);

            // 3급 이상의 장애인
            m_data.HDP_YN = CUtil.GetRBString(rbHDP_YN_1, rbHDP_YN_2);
            List<string> hdpCodes = new List<string>();
            if (chkHDP_TY_CD_01.Checked) hdpCodes.Add("01");
            if (chkHDP_TY_CD_02.Checked) hdpCodes.Add("02");
            if (chkHDP_TY_CD_03.Checked) hdpCodes.Add("03");
            if (chkHDP_TY_CD_04.Checked) hdpCodes.Add("04");
            if (chkHDP_TY_CD_05.Checked) hdpCodes.Add("05");
            if (chkHDP_TY_CD_06.Checked) hdpCodes.Add("06");
            if (chkHDP_TY_CD_07.Checked) hdpCodes.Add("07");
            if (chkHDP_TY_CD_08.Checked) hdpCodes.Add("08");
            if (chkHDP_TY_CD_09.Checked) hdpCodes.Add("09");
            if (chkHDP_TY_CD_10.Checked) hdpCodes.Add("10");
            m_data.HDP_TY_CD = string.Join("/", hdpCodes.ToArray());

            // C. 투석 정보
            m_data.BLDD_STA_DT = CUtil.GetDateTime(txtBLDD_STA_DT_DATE.Text, txtBLDD_STA_DT_TIME.Text);
            m_data.HEIG = txtHEIG.Text;
            m_data.DLYS_BWGT_YN = CUtil.GetRBString(rbDLYS_BWGT_YN_1, rbDLYS_BWGT_YN_2);
            m_data.ASM_DLYS_BWGT = txtASM_DLYS_BWGT.Text;
            m_data.BF_BWGT_YN = CUtil.GetRBString(rbBF_BWGT_YN_1, rbBF_BWGT_YN_2);
            m_data.ASM_BF_BWGT = txtASM_BF_BWGT.Text;
            m_data.AF_BWGT_YN = CUtil.GetRBString(rbAF_BWGT_YN_1, rbAF_BWGT_YN_2);
            m_data.ASM_AF_BWGT = txtASM_AF_BWGT.Text;

            // 혈액투석 적절도
            m_data.BLDD_PPRT_ASM_YN = CUtil.GetRBString(rbBLDD_PPRT_ASM_YN_1, rbBLDD_PPRT_ASM_YN_2);

            List<string> blddCodes = new List<string>();
            if (chkBLDD_PPRT_ASM_CD_01.Checked) blddCodes.Add("01");
            if (chkBLDD_PPRT_ASM_CD_01.Checked) blddCodes.Add("02");
            m_data.BLDD_PPRT_ASM_CD = string.Join("/", blddCodes.ToArray());

            m_data.PPRT_NUV = txtPPRT_NUV.Text;
            m_data.BLUR_DCR_RT = txtBLUR_DCR_RT.Text;

            // 혈관통로
            m_data.MAIN_BLDVE_CH_DECS_CD = CUtil.GetComboboxSelectedValue(cboMAIN_BLDVE_CH_DECS_CD);
            m_data.USE_BLDVE_CH_CD = CUtil.GetComboboxSelectedValue(cboUSE_BLDVE_CH_CD);

            // 빈혈관리
            m_data.HMTP_INJC_YN = CUtil.GetRBString(rbHMTP_INJC_YN_1, rbHMTP_INJC_YN_2);
            m_data.HMTP_INJC_DT = CUtil.GetDateTime(txtHMTP_INJC_DT_DATE.Text, txtHMTP_INJC_DT_TIME.Text);

            // 기능검사
            m_data.ECG_ENFC_YN = CUtil.GetRBString(rbECG_ENFC_YN_1, rbECG_ENFC_YN_2);
            m_data.ECG_ENFC_DT = CUtil.GetDateTime(txtECG_ENFC_DT_DATE.Text, txtECG_ENFC_DT_TIME.Text);


            // 타기관 검사
            m_data.OIST_EXM_NM.Clear(); // 검사명
            m_data.OIST_MDFEE_CD.Clear(); // 수가코드
            m_data.OIST_ENFC_DD.Clear(); // 검사일자
            m_data.OIST_EXM_RST_TXT.Clear(); // 결과 텍스트

            // 타기관 검사
            m_data.OIST_EXM_ENFC_YN = CUtil.GetRBString(rbOIST_EXM_ENFC_YN_1, rbOIST_EXM_ENFC_YN_2);

            List<OIST> listOIST = (List<OIST>)grdOIST.DataSource;
            foreach (OIST data in listOIST)
            {
                m_data.OIST_EXM_NM.Add(data.EXM_NM); // 검사명
                m_data.OIST_MDFEE_CD.Add(data.MDFEE_CD); // 수가코드
                m_data.OIST_ENFC_DD.Add(data.ENFC_DD); // 검사일자
                m_data.OIST_EXM_RST_TXT.Add(data.EXM_RST_TXT); // 결과 텍스트
            }

            m_data.CHS_EXM_NM.Clear();
            m_data.CHS_MDFEE_CD.Clear();
            m_data.CHS_ENFC_DD.Clear();
            m_data.CHS_OIST_ENFC_YN.Clear();

            // 추적관리
            m_data.CHS_ENFC_YN = CUtil.GetRBString(rbCHS_ENFC_YN_1, rbCHS_ENFC_YN_2);

            List<CHS> listCHS = (List<CHS>)grdCHS.DataSource;
            foreach (CHS data in listCHS)
            {
                m_data.CHS_EXM_NM.Add(data.EXM_NM);
                m_data.CHS_MDFEE_CD.Add(data.MDFEE_CD);
                m_data.CHS_ENFC_DD.Add(data.ENFC_DD);
                m_data.CHS_OIST_ENFC_YN.Add(GetOIST_ENFC_YN(data.OIST_ENFC_YN));
            }


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
                List<CDataASM008_002> list = (List<CDataASM008_002>)m_view.DataSource;
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
                List<CDataASM008_002> list = (List<CDataASM008_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowOIST_Click(object sender, EventArgs e)
        {
            List<OIST> list = (List<OIST>)grdOIST.DataSource;
            list.Add(new OIST());
            RefreshGrid();
        }

        private void btnDelRowOIST_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdOISTView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<OIST> list = (List<OIST>)grdOIST.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowCHS_Click(object sender, EventArgs e)
        {
            List<CHS> list = (List<CHS>)grdCHS.DataSource;
            list.Add(new CHS());
            RefreshGrid();
        }

        private void btnDelRowCHS_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdCHSView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<CHS> list = (List<CHS>)grdCHS.DataSource;
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

                    CMakeASM008 make = new CMakeASM008();
                    tran = conn.BeginTransaction();
                    make.MakeASM008(m_data, sysdt, systm, m_User, conn, tran, true);
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

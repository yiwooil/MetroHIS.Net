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
    public partial class ADD7007_ASM002_002 : Form
    {
        // 관상동맥우회술(CABG)
        class CABG
        {
            public string SOPR_STA_DT_DATE { get; set; }
            public string SOPR_STA_DT_TIME { get; set; }
            public string SOPR_END_DT_DATE { get; set; }
            public string SOPR_END_DT_TIME { get; set; }
            public string EMY_YN { get; set; }
            public string ASM_EMY_RS_CD { get; set; }
            public string ASM_VNTR_SUP_DV_CD { get; set; }
            public string ASM_CATH_DT_DATE { get; set; }
            public string ASM_CATH_DT_TIME { get; set; }
            public string EMY_RS_ETC_TXT { get; set; }
            public string USE_BLDVE_CD { get; set; }
            public string ASM_ITAY_USE_RGN_CD { get; set; }
            public string ASM_ITAY_UNUS_RS_CD { get; set; }
            public string ITAY_UNUS_RS_ETC_TXT { get; set; }
            public string ASM_HRT_BLDVE_SAME_SOPR_CD { get; set; }
            public string ETC_SAME_SOPR_CD { get; set; }
            public string ETC_SAME_ST1_MDFEE_CD { get; set; }
            public string ETC_SAME_ST1_SOPR_NM { get; set; }
            public string ETC_SAME_ND2_MDFEE_CD { get; set; }
            public string ETC_SAME_ND2_SOPR_NM { get; set; }
            public string ASM_EXTB_DT_DATE { get; set; }
            public string ASM_EXTB_DT_TIME { get; set; }
            public CABG()
            {
                SOPR_STA_DT_DATE = "";
                SOPR_STA_DT_TIME = "";
                SOPR_END_DT_DATE = "";
                SOPR_END_DT_TIME = "";
                EMY_YN = "";
                ASM_EMY_RS_CD = "";
                ASM_VNTR_SUP_DV_CD = "";
                ASM_CATH_DT_DATE = "";
                ASM_CATH_DT_TIME = "";
                EMY_RS_ETC_TXT = "";
                USE_BLDVE_CD = "";
                ASM_ITAY_USE_RGN_CD = "";
                ASM_ITAY_UNUS_RS_CD = "";
                ITAY_UNUS_RS_ETC_TXT = "";
                ASM_HRT_BLDVE_SAME_SOPR_CD = "";
                ETC_SAME_SOPR_CD = "";
                ETC_SAME_ST1_MDFEE_CD = "";
                ETC_SAME_ST1_SOPR_NM = "";
                ETC_SAME_ND2_MDFEE_CD = "";
                ETC_SAME_ND2_SOPR_NM = "";
                ASM_EXTB_DT_DATE = "";
                ASM_EXTB_DT_TIME = "";
            }
        }

        // 기타 개흉술
        class THRC
        {
            public string THRC_STA_DT_DATE { get; set; }
            public string THRC_STA_DT_TIME { get; set; }
            public string THRC_SOPR_CD { get; set; }
            public string THRC_ST1_MDFEE_CD { get; set; }
            public string THRC_ST1_SOPR_NM { get; set; }
            public string THRC_ND2_MDFEE_CD { get; set; }
            public string THRC_ND2_SOPR_NM { get; set; }
            public string ASM_THRC_RS_CD { get; set; }
            public string THRC_RS_ETC_TXT { get; set; }
            public THRC()
            {
                THRC_STA_DT_DATE = "";
                THRC_STA_DT_TIME = "";
                THRC_SOPR_CD = "";
                THRC_ST1_MDFEE_CD = "";
                THRC_ST1_SOPR_NM = "";
                THRC_ND2_MDFEE_CD = "";
                THRC_ND2_SOPR_NM = "";
                ASM_THRC_RS_CD = "";
                THRC_RS_ETC_TXT = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM002_002 m_data;

        public ADD7007_ASM002_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM002_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM002_002> list = (List<CDataASM002_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 내원경로
            cboASM_VST_PTH_CD.DataSource = new[] 
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "직접내원" },
                new { Key = "2", Value = "타병원 전원" },
                new { Key = "3", Value = "기록없음" }
            };
            cboASM_VST_PTH_CD.DisplayMember = "Value";
            cboASM_VST_PTH_CD.ValueMember = "Key";
            // 퇴원상태
            cboDSCG_STAT_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "호전퇴원" },
                new { Key = "2", Value = "치료거부 퇴원" },
                new { Key = "3", Value = "가망없는 퇴원" },
                new { Key = "4", Value = "타병원 전원" },
                new { Key = "5", Value = "사망" }
            };
            cboDSCG_STAT_CD.DisplayMember = "Value";
            cboDSCG_STAT_CD.ValueMember = "Key";
            // 신장
            cboHEIG_MASR_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "측정" },
                new { Key = "2", Value = "측정불가" },
                new { Key = "3", Value = "기록없음" }
            };
            cboHEIG_MASR_CD.DisplayMember = "Value";
            cboHEIG_MASR_CD.ValueMember = "Key";
            // 체중
            cboBWGT_MASR_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "측정" },
                new { Key = "2", Value = "측정불가" },
                new { Key = "3", Value = "기록없음" }
            };
            cboBWGT_MASR_CD.DisplayMember = "Value";
            cboBWGT_MASR_CD.ValueMember = "Key";
            // 혈압측정여부
            cboBPRSU_MASR_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "측정" },
                new { Key = "2", Value = "측정불가" },
                new { Key = "3", Value = "측정되지 않음" }
            };
            cboBPRSU_MASR_CD.DisplayMember = "Value";
            cboBPRSU_MASR_CD.ValueMember = "Key";
            // 맥박측정여부
            cboPULS_MASR_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "측정" },
                new { Key = "2", Value = "측정불가" },
                new { Key = "3", Value = "측정되지 않음" }
            };
            cboPULS_MASR_CD.DisplayMember = "Value";
            cboPULS_MASR_CD.ValueMember = "Key";
            // 침습혈관 수
            cboASM_INVS_BLDVE_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "1 vessel disease" },
                new { Key = "2", Value = "2 vessel disease" },
                new { Key = "3", Value = "3 vessel disease" },
                new { Key = "4", Value = "No significant or Normal" },
                new { Key = "5", Value = "검사 미실시 또는 기록없음" }
            };
            cboASM_INVS_BLDVE_CD.DisplayMember = "Value";
            cboASM_INVS_BLDVE_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            // 관상동맥우회술(CABG)
            CUtil.SetGridCombo(grdCABGView.Columns["EMY_YN"], // 응급여부
                    "",
                    "정규",
                    "응급"
                );

            CUtil.SetGridCheckedCombo(grdCABGView.Columns["ASM_EMY_RS_CD"], // 응급수술사유
                    "PCI 실패",			
                    "Intubated",			
                    "IABP",			
                    "심실보조장치",			
                    "심인성 쇽",			
                    "치료에도 불구하고 지속되는 흉통",			
                    "cath 후 24시간이내 수술",
                    "기타"
                );


            CUtil.SetGridCheckedCombo(grdCABGView.Columns["ASM_VNTR_SUP_DV_CD"], // 심실보조장치 상세
                    "VAD",
                    "ECMO",
                    "기타"
                );


            CUtil.SetGridCombo(grdCABGView.Columns["USE_BLDVE_CD"], // 이용혈관
                    "", 
                    "Artery",			
                    "Vein",			
                    "Both(Artery&Vein)"			
                );


            CUtil.SetGridCombo(grdCABGView.Columns["ASM_ITAY_USE_RGN_CD"], // 내흉동맥 사용부위
                    "", 
                    "미사용",			
                    "Right",			
                    "Left",			
                    "Both(Right&Left)"			
                );

            CUtil.SetGridCheckedCombo(grdCABGView.Columns["ASM_ITAY_UNUS_RS_CD"], // 내흉동맥 미사용 사유
                    "기록없음",			
                    "정상 LAD",			
                    "Subclavian artery 문제",			
                    "IMA 박리 중 발생된 문제",			
                    "IMA 자체 문제(small, thin 등)",			
                    "환자 응급 상태",			
                    "기타"			
                );

            CUtil.SetGridCheckedCombo(grdCABGView.Columns["ASM_HRT_BLDVE_SAME_SOPR_CD"], // 심혈관관련 동시수술
                    "해당없음",			
                    "aorta",			
                    "valve",			
                    "LV aneurysm",			
                    "carotid OP",			
                    "VSD",			
                    "기타"			
                );

            CUtil.SetGridCombo(grdCABGView.Columns["ETC_SAME_SOPR_CD"], // 기타 동시수술
                    "", 
                    "1수술", 
                    "2수술"
                );

            // 기타 개흉술
            CUtil.SetGridCombo(grdTHRCView.Columns["THRC_SOPR_CD"], // 개흉술
                    "", 
                    "1수술", 
                    "2수술"
                );

            CUtil.SetGridCheckedCombo(grdTHRCView.Columns["ASM_THRC_RS_CD"], // 수술사유
                    "출혈 또는 혈종",
                    "수술부위 감염",
                    "종격동염",
                    "VAD제거술",
                    "기타"
                );
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // 환자정보
            txtASM_IPAT_DT_DATE.Text = CUtil.GetDate(m_data.ASM_IPAT_DT);
            txtASM_IPAT_DT_TIME.Text = CUtil.GetTime(m_data.ASM_IPAT_DT);

            CUtil.SetComboboxSelectedValue(cboASM_VST_PTH_CD, m_data.ASM_VST_PTH_CD);

            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1";
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2";

            txtASM_DSCG_DT_DATE.Text = CUtil.GetDate(m_data.ASM_DSCG_DT);
            txtASM_DSCG_DT_TIME.Text = CUtil.GetTime(m_data.ASM_DSCG_DT);

            CUtil.SetComboboxSelectedValue(cboDSCG_STAT_CD, m_data.DSCG_STAT_CD);

            txtDEATH_DT_DATE.Text = CUtil.GetDate(m_data.DEATH_DT);
            txtDEATH_DT_TIME.Text = CUtil.GetTime(m_data.DEATH_DT);

            CUtil.SetComboboxSelectedValue(cboHEIG_MASR_CD, m_data.HEIG_MASR_CD);
            txtHEIG.Text = m_data.HEIG;

            CUtil.SetComboboxSelectedValue(cboBWGT_MASR_CD, m_data.BWGT_MASR_CD);
            txtBWGT.Text = m_data.BWGT;

            // B. 과거력 및 시술경험
            rbSMKN_YN_1.Checked = m_data.SMKN_YN == "1";
            rbSMKN_YN_2.Checked = m_data.SMKN_YN == "2";
            rbSMKN_YN_3.Checked = m_data.SMKN_YN == "3";

            rbNTSM_CD_1.Checked = m_data.NTSM_CD == "1";
            rbNTSM_CD_2.Checked = m_data.NTSM_CD == "2";

            rbHYTEN_YN_1.Checked = m_data.HYTEN_YN == "1";
            rbHYTEN_YN_2.Checked = m_data.HYTEN_YN == "2";
            rbHYTEN_YN_3.Checked = m_data.HYTEN_YN == "3";

            rbDBML_YN_1.Checked = m_data.DBML_YN == "1";
            rbDBML_YN_2.Checked = m_data.DBML_YN == "2";
            rbDBML_YN_3.Checked = m_data.DBML_YN == "3";

            rbACTE_MCDI_OCCUR_YN_1.Checked = m_data.ACTE_MCDI_OCCUR_YN == "1";
            rbACTE_MCDI_OCCUR_YN_2.Checked = m_data.ACTE_MCDI_OCCUR_YN == "2";
            rbACTE_MCDI_OCCUR_YN_3.Checked = m_data.ACTE_MCDI_OCCUR_YN == "3";

            rbACTE_MCDI_OCCUR_RCD_CD_1.Checked = m_data.ACTE_MCDI_OCCUR_RCD_CD == "1";
            rbACTE_MCDI_OCCUR_RCD_CD_2.Checked = m_data.ACTE_MCDI_OCCUR_RCD_CD == "2";
            rbACTE_MCDI_OCCUR_RCD_CD_3.Checked = m_data.ACTE_MCDI_OCCUR_RCD_CD == "3";
            txtACTE_MCDI_OCCUR_DD.Text = m_data.ACTE_MCDI_OCCUR_DD;

            rbISTBY_AP_OCCUR_YN_1.Checked = m_data.ISTBY_AP_OCCUR_YN == "1";
            rbISTBY_AP_OCCUR_YN_2.Checked = m_data.ISTBY_AP_OCCUR_YN == "2";
            rbISTBY_AP_OCCUR_YN_3.Checked = m_data.ISTBY_AP_OCCUR_YN == "3";

            // panel12: 기타 과거질환 checkboxes
            string[] pastArr = (m_data.ASM_ETC_PAST_DS_CD ?? "").Split('/');
            chkASM_ETC_PAST_DS_CD_0.Checked = pastArr.Contains("0");
            chkASM_ETC_PAST_DS_CD_1.Checked = pastArr.Contains("1");
            chkASM_ETC_PAST_DS_CD_2.Checked = pastArr.Contains("2");
            chkASM_ETC_PAST_DS_CD_3.Checked = pastArr.Contains("3");
            chkASM_ETC_PAST_DS_CD_4.Checked = pastArr.Contains("4");
            chkASM_ETC_PAST_DS_CD_5.Checked = pastArr.Contains("5");
            chkASM_ETC_PAST_DS_CD_6.Checked = pastArr.Contains("6");

            rbPCI_MOPR_YN_1.Checked = m_data.PCI_MOPR_YN == "1";
            rbPCI_MOPR_YN_2.Checked = m_data.PCI_MOPR_YN == "2";
            rbPCI_MOPR_YN_3.Checked = m_data.PCI_MOPR_YN == "3";

            rbPCI_MOPR_ADM_CD_1.Checked = m_data.PCI_MOPR_ADM_CD == "1";
            rbPCI_MOPR_ADM_CD_2.Checked = m_data.PCI_MOPR_ADM_CD == "2";
            rbPCI_MOPR_ADM_CD_3.Checked = m_data.PCI_MOPR_ADM_CD == "3";

            txtASM_PCI_MOPR_DT_DATE.Text = CUtil.GetDate(m_data.ASM_PCI_MOPR_DT);
            txtASM_PCI_MOPR_DT_TIME.Text = CUtil.GetTime(m_data.ASM_PCI_MOPR_DT);

            rbLEFT_MAIN_ENFC_YN_1.Checked = m_data.LEFT_MAIN_ENFC_YN == "1";
            rbLEFT_MAIN_ENFC_YN_2.Checked = m_data.LEFT_MAIN_ENFC_YN == "2";
            rbLEFT_MAIN_ENFC_YN_3.Checked = m_data.LEFT_MAIN_ENFC_YN == "3";

            rbTHREE_VSS_ENFC_YN_1.Checked = m_data.THREE_VSS_ENFC_YN == "1";
            rbTHREE_VSS_ENFC_YN_2.Checked = m_data.THREE_VSS_ENFC_YN == "2";
            rbTHREE_VSS_ENFC_YN_3.Checked = m_data.THREE_VSS_ENFC_YN == "3";

            txtSTENT_INS_TOT_CNT.Text = m_data.STENT_INS_TOT_CNT;

            rbCABG_EXPR_YN_1.Checked = m_data.CABG_EXPR_YN == "1";
            rbCABG_EXPR_YN_2.Checked = m_data.CABG_EXPR_YN == "2";
            rbCABG_EXPR_YN_3.Checked = m_data.CABG_EXPR_YN == "3";

            rbETC_HRT_SOPR_EXPR_YN_1.Checked = m_data.ETC_HRT_SOPR_EXPR_YN == "1";
            rbETC_HRT_SOPR_EXPR_YN_2.Checked = m_data.ETC_HRT_SOPR_EXPR_YN == "2";
            rbETC_HRT_SOPR_EXPR_YN_3.Checked = m_data.ETC_HRT_SOPR_EXPR_YN == "3";

            // C. 수술 전 진료정보
            CUtil.SetComboboxSelectedValue(cboBPRSU_MASR_CD, m_data.BPRSU_MASR_CD);
            var bp = (m_data.BPRSU + "/").Split('/');
            txtBPRSU_SYSTOLIC.Text = bp[0];
            txtBPRSU_DIASTOLIC.Text = bp[1];

            CUtil.SetComboboxSelectedValue(cboPULS_MASR_CD, m_data.PULS_MASR_CD);
            txtPULS.Text = m_data.PULS;
            txtCHOL.Text = m_data.CHOL;
            txtNTFT.Text = m_data.NTFT;
            txtHDL.Text = m_data.HDL;
            txtLDL.Text = m_data.LDL;
            txtSRU_CRAT.Text = m_data.SRU_CRAT;
            txtCRHM.Text = m_data.CRHM;
            txtHCT.Text = m_data.HCT;
            txtLVEF.Text = m_data.LVEF;

            // panel23: 심전도 소견 checkboxes
            string[] ecgArr = (m_data.ASM_ECG_OPN_CD ?? "").Split('/');
            chkASM_ECG_OPN_CD_0.Checked = ecgArr.Contains("0");
            chkASM_ECG_OPN_CD_1.Checked = ecgArr.Contains("1");
            chkASM_ECG_OPN_CD_2.Checked = ecgArr.Contains("2");
            chkASM_ECG_OPN_CD_3.Checked = ecgArr.Contains("3");

            CUtil.SetComboboxSelectedValue(cboASM_INVS_BLDVE_CD, m_data.ASM_INVS_BLDVE_CD);

            rbLEFT_MAIN_ILNS_YN_1.Checked = m_data.LEFT_MAIN_ILNS_YN == "1";
            rbLEFT_MAIN_ILNS_YN_2.Checked = m_data.LEFT_MAIN_ILNS_YN == "2";
            rbLEFT_MAIN_ILNS_YN_3.Checked = m_data.LEFT_MAIN_ILNS_YN == "3";

            // panel25: 수술 전 주요 임상상태 checkboxes
            string[] cliArr = (m_data.SOPR_BF_IMPT_CLI_STAT_CD ?? "").Split('/');
            chkSOPR_BF_IMPT_CLI_STAT_CD_0.Checked = cliArr.Contains("0");
            chkSOPR_BF_IMPT_CLI_STAT_CD_1.Checked = cliArr.Contains("1");
            chkSOPR_BF_IMPT_CLI_STAT_CD_2.Checked = cliArr.Contains("2");
            chkSOPR_BF_IMPT_CLI_STAT_CD_3.Checked = cliArr.Contains("3");
            chkSOPR_BF_IMPT_CLI_STAT_CD_4.Checked = cliArr.Contains("4");
            chkSOPR_BF_IMPT_CLI_STAT_CD_5.Checked = cliArr.Contains("5");
            chkSOPR_BF_IMPT_CLI_STAT_CD_6.Checked = cliArr.Contains("6");

            // panel26: 심실보조 장치 코드 checkboxes
            string[] vntArr = (m_data.VNTR_SUP_DV_CD ?? "").Split('/');
            chkVNTR_SUP_DV_CD_1.Checked = vntArr.Contains("1");
            chkVNTR_SUP_DV_CD_2.Checked = vntArr.Contains("2");
            chkVNTR_SUP_DV_CD_3.Checked = vntArr.Contains("3");

            // D. CABG 서브 데이터리스트 → grdCABG
            grdCABG.DataSource = null;
            var cabgList = new List<CABG>();
            for (int i = 0; i < m_data.SOPR_STA_DT.Count; i++)
            {
                var data = new CABG();
                data.SOPR_STA_DT_DATE = CUtil.GetDate(m_data.SOPR_STA_DT[i]);
                data.SOPR_STA_DT_TIME = CUtil.GetTime(m_data.SOPR_STA_DT[i]);
                data.SOPR_END_DT_DATE = CUtil.GetDate(m_data.SOPR_END_DT[i]);
                data.SOPR_END_DT_TIME = CUtil.GetTime(m_data.SOPR_END_DT[i]);
                data.EMY_YN = GetEMY_YN_NM(m_data.EMY_YN[i]);
                data.ASM_EMY_RS_CD = GetASM_EMY_RS_CD_NM(m_data.ASM_EMY_RS_CD[i]); // 콤보/체크 처리 필요시 가공
                data.ASM_VNTR_SUP_DV_CD = GetASM_VNTR_SUP_DV_CD_NM(m_data.ASM_VNTR_SUP_DV_CD[i]);
                data.ASM_CATH_DT_DATE = CUtil.GetDate(m_data.ASM_CATH_DT[i]);
                data.ASM_CATH_DT_TIME = CUtil.GetTime(m_data.ASM_CATH_DT[i]);
                data.EMY_RS_ETC_TXT = m_data.EMY_RS_ETC_TXT[i];
                data.USE_BLDVE_CD = GetUSE_BLDVE_CD_NM(m_data.USE_BLDVE_CD[i]);
                data.ASM_ITAY_USE_RGN_CD = GetASM_ITAY_USE_RGN_CD_NM(m_data.ASM_ITAY_USE_RGN_CD[i]);
                data.ASM_ITAY_UNUS_RS_CD = GetASM_ITAY_UNUS_RS_CD_NM(m_data.ASM_ITAY_UNUS_RS_CD[i]);
                data.ITAY_UNUS_RS_ETC_TXT = m_data.ITAY_UNUS_RS_ETC_TXT[i];
                data.ASM_HRT_BLDVE_SAME_SOPR_CD = GetASM_HRT_BLDVE_SAME_SOPR_CD_NM(m_data.ASM_HRT_BLDVE_SAME_SOPR_CD[i]);
                data.ETC_SAME_SOPR_CD = GetETC_SAME_SOPR_CD_NM(m_data.ETC_SAME_SOPR_CD[i]);
                data.ETC_SAME_ST1_MDFEE_CD = m_data.ETC_SAME_ST1_MDFEE_CD[i];
                data.ETC_SAME_ST1_SOPR_NM = m_data.ETC_SAME_ST1_SOPR_NM[i];
                data.ETC_SAME_ND2_MDFEE_CD = m_data.ETC_SAME_ND2_MDFEE_CD[i];
                data.ETC_SAME_ND2_SOPR_NM = m_data.ETC_SAME_ND2_SOPR_NM[i];
                data.ASM_EXTB_DT_DATE = CUtil.GetDate(m_data.ASM_EXTB_DT[i]);
                data.ASM_EXTB_DT_TIME = CUtil.GetTime(m_data.ASM_EXTB_DT[i]);
                cabgList.Add(data);
            }
            grdCABG.DataSource = cabgList;

            // E. 개흉술 서브
            rbTHRC_YN_1.Checked = m_data.THRC_YN == "1";
            rbTHRC_YN_2.Checked = m_data.THRC_YN == "2";

            // E. 개흉술 서브 데이터리스트 → grdTHRC
            grdTHRC.DataSource = null;
            var thrcList = new List<THRC>();
            for (int i = 0; i < m_data.THRC_STA_DT.Count; i++)
            {
                var data = new THRC();
                data.THRC_STA_DT_DATE = CUtil.GetDate(m_data.THRC_STA_DT[i]);
                data.THRC_STA_DT_TIME = CUtil.GetTime(m_data.THRC_STA_DT[i]);
                data.THRC_SOPR_CD = GetTHRC_SOPR_CD_NM(m_data.THRC_SOPR_CD[i]);
                data.THRC_ST1_MDFEE_CD = m_data.THRC_ST1_MDFEE_CD[i];
                data.THRC_ST1_SOPR_NM = m_data.THRC_ST1_SOPR_NM[i];
                data.THRC_ND2_MDFEE_CD = m_data.THRC_ND2_MDFEE_CD[i];
                data.THRC_ND2_SOPR_NM = m_data.THRC_ND2_SOPR_NM[i];
                data.ASM_THRC_RS_CD = GetASM_THRC_RS_CD_NM(m_data.ASM_THRC_RS_CD[i]);
                data.THRC_RS_ETC_TXT = m_data.THRC_RS_ETC_TXT[i];
                thrcList.Add(data);
            }
            grdTHRC.DataSource = thrcList;

            // F. 퇴원 시 항혈소판제 처방
            rbANDR_PRSC_YN_1.Checked = m_data.ANDR_PRSC_YN == "1";
            rbANDR_PRSC_YN_2.Checked = m_data.ANDR_PRSC_YN == "2";

            txtMDS_CD.Text = m_data.MDS_CD;
            txtMDS_NM.Text = m_data.MDS_NM;

            // 미처방 사유(다중선택 체크박스)
            string[] noprsCodes = (m_data.ASM_ANDR_NOPRS_RS_CD ?? "").Split('/');
            chkASM_ANDR_NOPRS_RS_CD_0.Checked = noprsCodes.Contains("0");
            chkASM_ANDR_NOPRS_RS_CD_1.Checked = noprsCodes.Contains("1");
            chkASM_ANDR_NOPRS_RS_CD_2.Checked = noprsCodes.Contains("2");
            chkASM_ANDR_NOPRS_RS_CD_9.Checked = noprsCodes.Contains("9");

            // 미처방 사유 기타 상세
            txtANDR_NOPRS_RS_ETC_TXT.Text = m_data.ANDR_NOPRS_RS_ETC_TXT;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdCABG, grdCABGView);
            CUtil.RefreshGrid(grdTHRC, grdTHRCView);
        }

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

        //private string GetDSCG_STAT_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "호전퇴원";
        //    if (p_value == "2") return "치료거부 퇴원";
        //    if (p_value == "3") return "가망없는 퇴원";
        //    if (p_value == "4") return "타병원 전원";
        //    if (p_value == "5") return "사망";
        //    return "";
        //}

        //private string GetDSCG_STAT_CD(string p_value)
        //{
        //    if (p_value == "호전퇴원") return "1";
        //    if (p_value == "치료거부 퇴원") return "2";
        //    if (p_value == "가망없는 퇴원") return "3";
        //    if (p_value == "타병원 전원") return "4";
        //    if (p_value == "사망") return "5";
        //    return "";
        //}

        //private string GetHEIG_MASR_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "측정";
        //    if (p_value == "2") return "측정불가";
        //    if (p_value == "3") return "기록없음";
        //    return "";
        //}

        //private string GetHEIG_MASR_CD(string p_value)
        //{
        //    if (p_value == "측정") return "1";
        //    if (p_value == "측정불가") return "2";
        //    if (p_value == "기록없음") return "3";
        //    return "";
        //}

        //private string GetBWGT_MASR_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "측정";
        //    if (p_value == "2") return "측정불가";
        //    if (p_value == "3") return "기록없음";
        //    return "";
        //}

        //private string GetBWGT_MASR_CD(string p_value)
        //{
        //    if (p_value == "측정") return "1";
        //    if (p_value == "측정불가") return "2";
        //    if (p_value == "기록없음") return "3";
        //    return "";
        //}

        //private string GetBPRSU_MASR_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "정함";
        //    if (p_value == "2") return "측정불가";
        //    if (p_value == "3") return "측정되지 않음";
        //    return "";
        //}

        //private string GetBPRSU_MASR_CD(string p_value)
        //{
        //    if (p_value == "측정") return "1";
        //    if (p_value == "측정불가") return "2";
        //    if (p_value == "측정되지 않음") return "3";
        //    return "";
        //}

        //private string GetPULS_MASR_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "정함";
        //    if (p_value == "2") return "측정불가";
        //    if (p_value == "3") return "측정되지 않음";
        //    return "";
        //}

        //private string GetPULS_MASR_CD(string p_value)
        //{
        //    if (p_value == "측정") return "1";
        //    if (p_value == "측정불가") return "2";
        //    if (p_value == "측정되지 않음") return "3";
        //    return "";
        //}
        
        //private string GetASM_INVS_BLDVE_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "1 vessel disease";
        //    if (p_value == "2") return "2 vessel disease";
        //    if (p_value == "3") return "3 vessel disease";
        //    if (p_value == "4") return "No significant or Normal";
        //    if (p_value == "5") return "검사 미실시 또는 기록없음";
        //    return "";
        //}

        //private string GetASM_INVS_BLDVE_CD(string p_value)
        //{
        //    if (p_value == "1 vessel disease") return "1";
        //    if (p_value == "2 vessel disease") return "2";
        //    if (p_value == "3 vessel disease") return "3";
        //    if (p_value == "No significant or Normal") return "4";
        //    if (p_value == "검사 미실시 또는 기록없음") return "5";
        //    return "";
        //}

        private string GetEMY_YN_NM(string p_value)
        {
            if (p_value == "1") return "정규";
            if (p_value == "2") return "응급";
            return "";
        }

        private string GetEMY_YN(string p_value)
        {
            if (p_value == "정규") return "1";
            if (p_value == "응급") return "2";
            return "";
        }
        
        private string GetASM_EMY_RS_CD_NM(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "01") lst.Add("PCI 실패");
                if (ary[i].Trim() == "02") lst.Add("Intubated");
                if (ary[i].Trim() == "03") lst.Add("IABP");
                if (ary[i].Trim() == "04") lst.Add("심실보조장치");
                if (ary[i].Trim() == "05") lst.Add("심인성 쇽");
                if (ary[i].Trim() == "06") lst.Add("치료에도 불구하고 지속되는 흉통");
                if (ary[i].Trim() == "07") lst.Add("cath 후 24시간이내 수술");
                if (ary[i].Trim() == "99") lst.Add("기타");
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        private string GetASM_EMY_RS_CD(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "PCI 실패") lst.Add("01");
                if (ary[i].Trim() == "Intubated") lst.Add("02");
                if (ary[i].Trim() == "IABP") lst.Add("03");
                if (ary[i].Trim() == "심실보조장치") lst.Add("04");
                if (ary[i].Trim() == "심인성 쇽") lst.Add("05");
                if (ary[i].Trim() == "치료에도 불구하고 지속되는 흉통") lst.Add("06");
                if (ary[i].Trim() == "cath 후 24시간이내 수술") lst.Add("07");
                if (ary[i].Trim() == "기타") lst.Add("99");
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }
        
        private string GetASM_VNTR_SUP_DV_CD_NM(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "1") lst.Add("VAD");
                if (ary[i].Trim() == "2") lst.Add("ECMO");
                if (ary[i].Trim() == "9") lst.Add("기타");
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        private string GetASM_VNTR_SUP_DV_CD(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "VAD") lst.Add("1");
                if (ary[i].Trim() == "ECMO") lst.Add("2");
                if (ary[i].Trim() == "기타") lst.Add("9");
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }
        
        private string GetUSE_BLDVE_CD_NM(string p_value)
        {
            if (p_value == "1") return "Artery";
            if (p_value == "2") return "Vein";
            if (p_value == "3") return "Both(Artery&Vein)";
            return "";
        }

        private string GetUSE_BLDVE_CD(string p_value)
        {
            if (p_value == "Artery") return "1";
            if (p_value == "Vein") return "2";
            if (p_value == "Both(Artery&Vein)") return "3";
            return "";
        }
        
        private string GetASM_ITAY_USE_RGN_CD_NM(string p_value)
        {
            if (p_value == "0") return "미사용";
            if (p_value == "1") return "Right";
            if (p_value == "2") return "Left";
            if (p_value == "3") return "Both(Right&Left)";
            return "";
        }

        private string GetASM_ITAY_USE_RGN_CD(string p_value)
        {
            if (p_value == "미사용") return "0";
            if (p_value == "Right") return "1";
            if (p_value == "Left") return "2";
            if (p_value == "Both(Right&Left)") return "3";
            return "";
        }

        private string GetASM_ITAY_UNUS_RS_CD_NM(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "0") lst.Add("기록없음");
                if (ary[i].Trim() == "1") lst.Add("정상 LAD");
                if (ary[i].Trim() == "2") lst.Add("Subclavian artery 문제");
                if (ary[i].Trim() == "3") lst.Add("IMA 박리 중 발생된 문제");
                if (ary[i].Trim() == "4") lst.Add("IMA 자체 문제(small, thin 등)");
                if (ary[i].Trim() == "5") lst.Add("환자 응급 상태");
                if (ary[i].Trim() == "9") lst.Add("기타");
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        private string GetASM_ITAY_UNUS_RS_CD(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "기록없음") lst.Add("0");
                if (ary[i].Trim() == "정상 LAD") lst.Add("1");
                if (ary[i].Trim() == "Subclavian artery 문제") lst.Add("2");
                if (ary[i].Trim() == "IMA 박리 중 발생된 문제") lst.Add("3");
                if (ary[i].Trim() == "IMA 자체 문제(small, thin 등)") lst.Add("4");
                if (ary[i].Trim() == "환자 응급 상태") lst.Add("5");
                if (ary[i].Trim() == "기타") lst.Add("9");
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }

        public string GetASM_HRT_BLDVE_SAME_SOPR_CD_NM(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "0") lst.Add("해당없음");
                if (ary[i].Trim() == "1") lst.Add("aorta");
                if (ary[i].Trim() == "2") lst.Add("valve");
                if (ary[i].Trim() == "3") lst.Add("LV aneurysm");
                if (ary[i].Trim() == "4") lst.Add("carotid OP");
                if (ary[i].Trim() == "5") lst.Add("VSD");
                if (ary[i].Trim() == "9") lst.Add("기타");
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        public string GetASM_HRT_BLDVE_SAME_SOPR_CD(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "해당없음") lst.Add("0");
                if (ary[i].Trim() == "aorta") lst.Add("1");
                if (ary[i].Trim() == "valve") lst.Add("2");
                if (ary[i].Trim() == "LV aneurysm") lst.Add("3");
                if (ary[i].Trim() == "carotid OP") lst.Add("4");
                if (ary[i].Trim() == "VSD") lst.Add("5");
                if (ary[i].Trim() == "기타") lst.Add("9");
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }

        private string GetETC_SAME_SOPR_CD_NM(string p_value)
        {
            if (p_value == "1") return "1수술";
            if (p_value == "2") return "2수술";
            return "";
        }

        private string GetETC_SAME_SOPR_CD(string p_value)
        {
            if (p_value == "1수술") return "1";
            if (p_value == "2수술") return "2";
            return "";
        }
        
        private string GetTHRC_SOPR_CD_NM(string p_value)
        {
            if (p_value == "1") return "1수술";
            if (p_value == "2") return "2수술";
            return "";
        }

        private string GetTHRC_SOPR_CD(string p_value)
        {
            if (p_value == "1수술") return "1";
            if (p_value == "2수술") return "2";
            return "";
        }

        private string GetASM_THRC_RS_CD_NM(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "1") lst.Add("출혈 또는 혈종");
                if (ary[i].Trim() == "2") lst.Add("수술부위 감염");
                if (ary[i].Trim() == "3") lst.Add("종격동염");
                if (ary[i].Trim() == "4") lst.Add("VAD제거술");
                if (ary[i].Trim() == "9") lst.Add("기타");
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        private string GetASM_THRC_RS_CD(string p_value)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split(',');
            for (int i = 0; i < ary.Length; i++)
            {
                if (ary[i].Trim() == "출혈 또는 혈종") lst.Add("1");
                if (ary[i].Trim() == "수술부위 감염") lst.Add("2");
                if (ary[i].Trim() == "종격동염") lst.Add("3");
                if (ary[i].Trim() == "VAD제거술") lst.Add("4");
                if (ary[i].Trim() == "기타") lst.Add("9");
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkInputValue() == false) return;

            // A. 환자정보
            m_data.ASM_IPAT_DT = CUtil.GetDateTime(txtASM_IPAT_DT_DATE.Text.ToString(), txtASM_IPAT_DT_TIME.Text.ToString());
            m_data.ASM_VST_PTH_CD = CUtil.GetComboboxSelectedValue(cboASM_VST_PTH_CD);
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2);
            m_data.ASM_DSCG_DT = CUtil.GetDateTime(txtASM_DSCG_DT_DATE.Text, txtASM_DSCG_DT_TIME.Text);
            m_data.DSCG_STAT_CD = CUtil.GetComboboxSelectedValue(cboDSCG_STAT_CD);
            m_data.DEATH_DT = CUtil.GetDateTime(txtDEATH_DT_DATE.Text, txtDEATH_DT_TIME.Text);

            m_data.HEIG_MASR_CD = CUtil.GetComboboxSelectedValue(cboHEIG_MASR_CD);
            m_data.HEIG = txtHEIG.Text;
            m_data.BWGT_MASR_CD = CUtil.GetComboboxSelectedValue(cboBWGT_MASR_CD);
            m_data.BWGT = txtBWGT.Text;

            // B. 과거력 및 시술경험
            m_data.SMKN_YN = CUtil.GetRBString(rbSMKN_YN_1, rbSMKN_YN_2, rbSMKN_YN_3);
            m_data.NTSM_CD = CUtil.GetRBString(rbNTSM_CD_1, rbNTSM_CD_2);
            m_data.HYTEN_YN = CUtil.GetRBString(rbHYTEN_YN_1, rbHYTEN_YN_2, rbHYTEN_YN_3);
            m_data.DBML_YN = CUtil.GetRBString(rbDBML_YN_1, rbDBML_YN_2, rbDBML_YN_3);
            m_data.ACTE_MCDI_OCCUR_YN = CUtil.GetRBString(rbACTE_MCDI_OCCUR_YN_1, rbACTE_MCDI_OCCUR_YN_2, rbACTE_MCDI_OCCUR_YN_3);
            m_data.ACTE_MCDI_OCCUR_RCD_CD = CUtil.GetRBString(rbACTE_MCDI_OCCUR_RCD_CD_1, rbACTE_MCDI_OCCUR_RCD_CD_2, rbACTE_MCDI_OCCUR_RCD_CD_3);
            m_data.ACTE_MCDI_OCCUR_DD = txtACTE_MCDI_OCCUR_DD.Text;
            m_data.ISTBY_AP_OCCUR_YN = CUtil.GetRBString(rbISTBY_AP_OCCUR_YN_1, rbISTBY_AP_OCCUR_YN_2, rbISTBY_AP_OCCUR_YN_3);

            // 기타 과거질환
            List<string> pastCodes = new List<string>();
            if (chkASM_ETC_PAST_DS_CD_0.Checked) pastCodes.Add("0");
            if (chkASM_ETC_PAST_DS_CD_1.Checked) pastCodes.Add("1");
            if (chkASM_ETC_PAST_DS_CD_2.Checked) pastCodes.Add("2");
            if (chkASM_ETC_PAST_DS_CD_3.Checked) pastCodes.Add("3");
            if (chkASM_ETC_PAST_DS_CD_4.Checked) pastCodes.Add("4");
            if (chkASM_ETC_PAST_DS_CD_5.Checked) pastCodes.Add("5");
            if (chkASM_ETC_PAST_DS_CD_6.Checked) pastCodes.Add("6");
            m_data.ASM_ETC_PAST_DS_CD = string.Join("/", pastCodes.ToArray());

            m_data.PCI_MOPR_YN = CUtil.GetRBString(rbPCI_MOPR_YN_1, rbPCI_MOPR_YN_2, rbPCI_MOPR_YN_3);
            m_data.PCI_MOPR_ADM_CD = CUtil.GetRBString(rbPCI_MOPR_ADM_CD_1, rbPCI_MOPR_ADM_CD_2, rbPCI_MOPR_ADM_CD_3);
            m_data.ASM_PCI_MOPR_DT = CUtil.GetDateTime(txtASM_PCI_MOPR_DT_DATE.Text, txtASM_PCI_MOPR_DT_TIME.Text);
            m_data.LEFT_MAIN_ENFC_YN = CUtil.GetRBString(rbLEFT_MAIN_ENFC_YN_1, rbLEFT_MAIN_ENFC_YN_2, rbLEFT_MAIN_ENFC_YN_3);
            m_data.THREE_VSS_ENFC_YN = CUtil.GetRBString(rbTHREE_VSS_ENFC_YN_1, rbTHREE_VSS_ENFC_YN_2, rbTHREE_VSS_ENFC_YN_3);
            m_data.STENT_INS_TOT_CNT = txtSTENT_INS_TOT_CNT.Text;
            m_data.CABG_EXPR_YN = CUtil.GetRBString(rbCABG_EXPR_YN_1, rbCABG_EXPR_YN_2, rbCABG_EXPR_YN_3);
            m_data.ETC_HRT_SOPR_EXPR_YN = CUtil.GetRBString(rbETC_HRT_SOPR_EXPR_YN_1, rbETC_HRT_SOPR_EXPR_YN_2, rbETC_HRT_SOPR_EXPR_YN_3);


            // C. 수술 전 진료정보
            m_data.BPRSU_MASR_CD = CUtil.GetComboboxSelectedValue(cboBPRSU_MASR_CD);
            m_data.BPRSU = txtBPRSU_SYSTOLIC.Text.ToString() + "/" + txtBPRSU_DIASTOLIC.Text.ToString();
            m_data.PULS_MASR_CD = CUtil.GetComboboxSelectedValue(cboPULS_MASR_CD);
            m_data.PULS = txtPULS.Text;
            m_data.CHOL = txtCHOL.Text;
            m_data.NTFT = txtNTFT.Text;
            m_data.HDL = txtHDL.Text;
            m_data.LDL = txtLDL.Text;
            m_data.SRU_CRAT = txtSRU_CRAT.Text;
            m_data.CRHM = txtCRHM.Text;
            m_data.HCT = txtHCT.Text;
            m_data.LVEF = txtLVEF.Text;

            // 심전도소견
            List<string> ecgCodes = new List<string>();
            if (chkASM_ECG_OPN_CD_0.Checked) ecgCodes.Add("0");
            if (chkASM_ECG_OPN_CD_1.Checked) ecgCodes.Add("1");
            if (chkASM_ECG_OPN_CD_2.Checked) ecgCodes.Add("2");
            if (chkASM_ECG_OPN_CD_3.Checked) ecgCodes.Add("3");
            m_data.ASM_ECG_OPN_CD = string.Join("/", ecgCodes.ToArray());

            m_data.ASM_INVS_BLDVE_CD = CUtil.GetComboboxSelectedValue(cboASM_INVS_BLDVE_CD);
            m_data.LEFT_MAIN_ILNS_YN = CUtil.GetRBString(rbLEFT_MAIN_ILNS_YN_1, rbLEFT_MAIN_ILNS_YN_2, rbLEFT_MAIN_ILNS_YN_3);

            // 수술전 주요 임상상태
            List<string> cliCodes = new List<string>();
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_0.Checked) cliCodes.Add("0");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_1.Checked) cliCodes.Add("1");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_2.Checked) cliCodes.Add("2");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_3.Checked) cliCodes.Add("3");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_4.Checked) cliCodes.Add("4");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_5.Checked) cliCodes.Add("5");
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_6.Checked) cliCodes.Add("6");
            m_data.SOPR_BF_IMPT_CLI_STAT_CD = string.Join("/", cliCodes.ToArray());

            // 심살 보조장치 상세
            List<string> vntrCodes = new List<string>();
            if (chkVNTR_SUP_DV_CD_1.Checked) vntrCodes.Add("1");
            if (chkVNTR_SUP_DV_CD_2.Checked) vntrCodes.Add("2");
            if (chkVNTR_SUP_DV_CD_3.Checked) vntrCodes.Add("3");
            m_data.VNTR_SUP_DV_CD = string.Join("/", vntrCodes.ToArray());

            // D. CABG 수술 내역 (그리드)
            m_data.SOPR_STA_DT.Clear();
            m_data.SOPR_END_DT.Clear();
            m_data.EMY_YN.Clear();
            m_data.ASM_EMY_RS_CD.Clear();
            m_data.ASM_VNTR_SUP_DV_CD.Clear();
            m_data.ASM_CATH_DT.Clear();
            m_data.EMY_RS_ETC_TXT.Clear();
            m_data.USE_BLDVE_CD.Clear();
            m_data.ASM_ITAY_USE_RGN_CD.Clear();
            m_data.ASM_ITAY_UNUS_RS_CD.Clear();
            m_data.ITAY_UNUS_RS_ETC_TXT.Clear();
            m_data.ASM_HRT_BLDVE_SAME_SOPR_CD.Clear();
            m_data.ETC_SAME_SOPR_CD.Clear();
            m_data.ETC_SAME_ST1_MDFEE_CD.Clear();
            m_data.ETC_SAME_ST1_SOPR_NM.Clear();
            m_data.ETC_SAME_ND2_MDFEE_CD.Clear();
            m_data.ETC_SAME_ND2_SOPR_NM.Clear();
            m_data.ASM_EXTB_DT.Clear();

            foreach (CABG data in (List<CABG>)grdCABG.DataSource)
            {
                m_data.SOPR_STA_DT.Add(CUtil.GetDateTime(data.SOPR_STA_DT_DATE, data.SOPR_STA_DT_TIME));
                m_data.SOPR_END_DT.Add(CUtil.GetDateTime(data.SOPR_END_DT_DATE, data.SOPR_END_DT_TIME));
                m_data.EMY_YN.Add(GetEMY_YN(data.EMY_YN));
                m_data.ASM_EMY_RS_CD.Add(GetASM_EMY_RS_CD(data.ASM_EMY_RS_CD));
                m_data.ASM_VNTR_SUP_DV_CD.Add(GetASM_VNTR_SUP_DV_CD(data.ASM_VNTR_SUP_DV_CD));
                m_data.ASM_CATH_DT.Add(CUtil.GetDateTime(data.ASM_CATH_DT_DATE, data.ASM_CATH_DT_TIME));
                m_data.EMY_RS_ETC_TXT.Add(data.EMY_RS_ETC_TXT);
                m_data.USE_BLDVE_CD.Add(GetUSE_BLDVE_CD(data.USE_BLDVE_CD));
                m_data.ASM_ITAY_USE_RGN_CD.Add(GetASM_ITAY_USE_RGN_CD(data.ASM_ITAY_USE_RGN_CD));
                m_data.ASM_ITAY_UNUS_RS_CD.Add(GetASM_ITAY_UNUS_RS_CD(data.ASM_ITAY_UNUS_RS_CD));
                m_data.ITAY_UNUS_RS_ETC_TXT.Add(data.ITAY_UNUS_RS_ETC_TXT);
                m_data.ASM_HRT_BLDVE_SAME_SOPR_CD.Add(GetASM_HRT_BLDVE_SAME_SOPR_CD(data.ASM_HRT_BLDVE_SAME_SOPR_CD));
                m_data.ETC_SAME_SOPR_CD.Add(GetETC_SAME_SOPR_CD(data.ETC_SAME_SOPR_CD));
                m_data.ETC_SAME_ST1_MDFEE_CD.Add(data.ETC_SAME_ST1_MDFEE_CD);
                m_data.ETC_SAME_ST1_SOPR_NM.Add(data.ETC_SAME_ST1_SOPR_NM);
                m_data.ETC_SAME_ND2_MDFEE_CD.Add(data.ETC_SAME_ND2_MDFEE_CD);
                m_data.ETC_SAME_ND2_SOPR_NM.Add(data.ETC_SAME_ND2_SOPR_NM);
                m_data.ASM_EXTB_DT.Add(CUtil.GetDateTime(data.ASM_EXTB_DT_DATE, data.ASM_EXTB_DT_TIME));
            }

            // E. 개흉술 수술 내역 (단일값)
            m_data.THRC_YN = CUtil.GetRBString(rbTHRC_YN_1, rbTHRC_YN_2);

            // E. 개흉술 수술 내역 (그리드)
            m_data.THRC_STA_DT.Clear();
            m_data.THRC_SOPR_CD.Clear();
            m_data.THRC_ST1_MDFEE_CD.Clear();
            m_data.THRC_ST1_SOPR_NM.Clear();
            m_data.THRC_ND2_MDFEE_CD.Clear();
            m_data.THRC_ND2_SOPR_NM.Clear();
            m_data.ASM_THRC_RS_CD.Clear();
            m_data.THRC_RS_ETC_TXT.Clear();

            foreach (THRC data in (List<THRC>)grdTHRC.DataSource)
            {
                m_data.THRC_STA_DT.Add(CUtil.GetDateTime(data.THRC_STA_DT_DATE, data.THRC_STA_DT_TIME));
                m_data.THRC_SOPR_CD.Add(GetTHRC_SOPR_CD(data.THRC_SOPR_CD));
                m_data.THRC_ST1_MDFEE_CD.Add(data.THRC_ST1_MDFEE_CD);
                m_data.THRC_ST1_SOPR_NM.Add(data.THRC_ST1_SOPR_NM);
                m_data.THRC_ND2_MDFEE_CD.Add(data.THRC_ND2_MDFEE_CD);
                m_data.THRC_ND2_SOPR_NM.Add(data.THRC_ND2_SOPR_NM);
                m_data.ASM_THRC_RS_CD.Add(GetASM_THRC_RS_CD(data.ASM_THRC_RS_CD));
                m_data.THRC_RS_ETC_TXT.Add(data.THRC_RS_ETC_TXT);
            }

            // F. 퇴원 시 약제처방
            m_data.ANDR_PRSC_YN = CUtil.GetRBString(rbANDR_PRSC_YN_1, rbANDR_PRSC_YN_2);
            m_data.MDS_CD = txtMDS_CD.Text;
            m_data.MDS_NM = txtMDS_NM.Text;
            // 미처방 사유
            List<string> noprsCodes = new List<string>();
            if (chkASM_ANDR_NOPRS_RS_CD_0.Checked) noprsCodes.Add("0");
            if (chkASM_ANDR_NOPRS_RS_CD_1.Checked) noprsCodes.Add("1");
            if (chkASM_ANDR_NOPRS_RS_CD_2.Checked) noprsCodes.Add("2");
            if (chkASM_ANDR_NOPRS_RS_CD_9.Checked) noprsCodes.Add("9");
            m_data.ASM_ANDR_NOPRS_RS_CD = string.Join("/", noprsCodes.ToArray());
            // 기타상세
            m_data.ANDR_NOPRS_RS_ETC_TXT = txtANDR_NOPRS_RS_ETC_TXT.Text;


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

        private bool checkInputValue()
        {
            if (chkASM_ETC_PAST_DS_CD_0.Checked)
            {
                if (chkASM_ETC_PAST_DS_CD_1.Checked ||
                    chkASM_ETC_PAST_DS_CD_2.Checked ||
                    chkASM_ETC_PAST_DS_CD_3.Checked ||
                    chkASM_ETC_PAST_DS_CD_4.Checked ||
                    chkASM_ETC_PAST_DS_CD_5.Checked ||
                    chkASM_ETC_PAST_DS_CD_6.Checked)
                {
                    MessageBox.Show("과거기타질환에서 '해당없음'과 다른 것을 동시에 선택할 수 없습니다.");
                    return false;
                }
            }
            if (chkASM_ECG_OPN_CD_0.Checked)
            {
                if (chkASM_ECG_OPN_CD_1.Checked ||
                    chkASM_ECG_OPN_CD_2.Checked ||
                    chkASM_ECG_OPN_CD_3.Checked)
                {
                    MessageBox.Show("심전도 소견에서 '해당없음'과 다른 것을 동시에 선택할 수 없습니다.");
                    return false;
                }
            }
            if (chkSOPR_BF_IMPT_CLI_STAT_CD_0.Checked)
            {
                if (chkSOPR_BF_IMPT_CLI_STAT_CD_1.Checked ||
                    chkSOPR_BF_IMPT_CLI_STAT_CD_2.Checked ||
                    chkSOPR_BF_IMPT_CLI_STAT_CD_3.Checked ||
                    chkSOPR_BF_IMPT_CLI_STAT_CD_4.Checked ||
                    chkSOPR_BF_IMPT_CLI_STAT_CD_5.Checked ||
                    chkSOPR_BF_IMPT_CLI_STAT_CD_6.Checked)
                {
                    MessageBox.Show("수술 전 주요 임상상태에서 '해당없음'과 다른 것을 동시에 선택할 수 없습니다.");
                    return false;
                }
            }
            if (chkASM_ANDR_NOPRS_RS_CD_0.Checked)
            {
                if (chkASM_ANDR_NOPRS_RS_CD_1.Checked ||
                    chkASM_ANDR_NOPRS_RS_CD_2.Checked ||
                    chkASM_ANDR_NOPRS_RS_CD_9.Checked)
                {
                    MessageBox.Show("미처방 사유에서 '기록없음'과 다른 것을 동시에 선택할 수 없습니다.");
                    return false;
                }
            }

            return true;
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
                List<CDataASM002_002> list = (List<CDataASM002_002>)m_view.DataSource;
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
                List<CDataASM002_002> list = (List<CDataASM002_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowCABG_Click(object sender, EventArgs e)
        {
            List<CABG> list = (List<CABG>)grdCABG.DataSource;
            list.Add(new CABG());
            RefreshGrid();
        }

        private void btnDelRowCABG_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdCABGView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<CABG> list = (List<CABG>)grdCABG.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowTHRC_Click(object sender, EventArgs e)
        {
            List<THRC> list = (List<THRC>)grdTHRC.DataSource;
            list.Add(new THRC());
            RefreshGrid();
        }

        private void btnDelRowTHRC_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdTHRCView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<THRC> list = (List<THRC>)grdTHRC.DataSource;
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

                    CMakeASM002 make = new CMakeASM002();
                    tran = conn.BeginTransaction();
                    make.MakeASM002(m_data, sysdt, systm, m_User, conn, tran, true);
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

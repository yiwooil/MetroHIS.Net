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
    public partial class ADD7007_ASM023_002 : Form
    {
        // 산소포화도
        public class OXY
        {
            public string ASM_EXM_DT_DATE { get; set; }
            public string ASM_EXM_DT_TIME { get; set; }
            public string ASM_OXY_STRT { get; set; }
            public OXY()
            {
                ASM_EXM_DT_DATE = "";
                ASM_EXM_DT_TIME = "";
                ASM_OXY_STRT = "";
            }
        }

        // 객담배양
        public class SPTUM
        {
            public string ASM_PRSC_DT_DATE { get; set; }
            public string ASM_PRSC_DT_TIME { get; set; }
            public SPTUM()
            {
                ASM_PRSC_DT_DATE = "";
                ASM_PRSC_DT_TIME = "";
            }
        }

        // 혈액배양
        public class BLD
        {
            public string ASM_GAT_DT_DATE { get; set; }
            public string ASM_GAT_DT_TIME { get; set; }
            public BLD()
            {
                ASM_GAT_DT_DATE = "";
                ASM_GAT_DT_TIME = "";
            }
        }

        // 판정도구
        public class SGRD
        {
            public string ASM_USE_DT_DATE { get; set; }
            public string ASM_USE_DT_TIME { get; set; }
            public string ASM_SGRD_JDGM_TL_KND_CD { get; set; }
            public string ASM_SGRD_JDGM_TL_TOT_PNT { get; set; }
            public SGRD()
            {
                ASM_USE_DT_DATE = "";
                ASM_USE_DT_TIME = "";
                ASM_SGRD_JDGM_TL_KND_CD = "";
                ASM_SGRD_JDGM_TL_TOT_PNT = "";
            }
        }

        // 항생제
        public class DRUG
        {
            public string MDS_INJC_DT_DATE { get; set; }
            public string MDS_INJC_DT_TIME { get; set; }
            public string MDS_CD { get; set; }
            public string MDS_NM { get; set; }
            public DRUG()
            {
                MDS_INJC_DT_DATE = "";
                MDS_INJC_DT_TIME = "";
                MDS_CD = "";
                MDS_NM = "";
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM023_002 m_data;

        public ADD7007_ASM023_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboInGrid();
        }

        public ADD7007_ASM023_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM023_002> list = (List<CDataASM023_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // 퇴원상태 콤보박스
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

            // 내원경로 콤보박스
            cboASM_VST_PTH_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "직접내원" },
                new { Key = "2", Value = "타병원 전원" },
                new { Key = "3", Value = "기록없음" }
            };
            cboASM_VST_PTH_CD.DisplayMember = "Value";
            cboASM_VST_PTH_CD.ValueMember = "Key";

            // 폐렴 관련성 코드 콤보박스
            cboASM_PLC_SCTY_OBTN_PNEM_CD2.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "00", Value = "해당 없음" },
                new { Key = "01", Value = "입원 후 72시간 이내 정맥내 항생제 투여 받지 않은 경우" },
                new { Key = "02", Value = "인공호흡기 관련 폐렴인 경우" },
                new { Key = "03", Value = "타상병으로 입원 중 48시간 이후 발생한 병원 내 폐렴인 경우" },
                new { Key = "04", Value = "악성종양으로 3개월 이내 진단 또는 항암·방사선 치료를 받은 경우" },
                new { Key = "05", Value = "면역억제제 복용중이거나, 면역질환이 동반된 경우" },
                new { Key = "06", Value = "고용량 스테로이드 치료를 받은 경우" },
                new { Key = "07", Value = "HIV·AIDS 환자인 경우" },
                new { Key = "08", Value = "90일 이내 2일 이상 입원경력이 있는 경우" },
                new { Key = "09", Value = "타병원 또는 가정간호로 정맥내 항생제 투여 후 내원한 경우" },
                new { Key = "10", Value = "투석중 환자인 경우" },
                new { Key = "11", Value = "호스피스·완화의료 입원의 경우" },
                new { Key = "12", Value = "수술 후 폐렴" },
                new { Key = "13", Value = "응급수술 등으로 폐렴치료가 지연된 경우" },
                new { Key = "14", Value = "정맥내 항생제 3일 미만 투여한 경우" }
            };
            cboASM_PLC_SCTY_OBTN_PNEM_CD2.DisplayMember = "Value";
            cboASM_PLC_SCTY_OBTN_PNEM_CD2.ValueMember = "Key";

            // 혈액배양 선행 증상 변화 콤보박스
            cboANBO_CHG_BF_GAT_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "0", Value = "해당 없음" },
                new { Key = "1", Value = "증상: 숨가쁨 증상, 가래 증상" },
                new { Key = "2", Value = "체온 38℃ 이상 지속, 호흡수 증가, 혈압 저하" },
                new { Key = "3", Value = "흉부사진: 증상 악화 또는 신규 발생" },
                new { Key = "4", Value = "혈액검사: WBC 증가, PLT 감소, CRP 증가" }
            };
            cboANBO_CHG_BF_GAT_RS_CD.DisplayMember = "Value";
            cboANBO_CHG_BF_GAT_RS_CD.ValueMember = "Key";
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            // 중증도 판정도구 종류
            CUtil.SetGridCombo(grdSGRDView.Columns["ASM_SGRD_JDGM_TL_KND_CD"],
                "",
                "CURB-65",						
                "CRB-65",						
                "CURB",						
                "CRB",						
                "PSI"						
                );
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

        //private string GetDSCG_STAT_NM(string p_value)
        //{
        //    if (p_value == "호전 퇴원") return "1";
        //    if (p_value == "치료거부 퇴원") return "2";
        //    if (p_value == "가망없는 퇴원") return "3";
        //    if (p_value == "타병원 전원") return "4";
        //    if (p_value == "사망") return "5";
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

        //private string GetASM_PLC_SCTY_OBTN_PNEM_CD2_NM(string p_value)
        //{
        //    if (p_value == "00") return "해당 없음";
        //    if (p_value == "01") return "입원 후 72시간 이내 정맥내 항생제 투여 받지 않은 경우";
        //    if (p_value == "02") return "인공호흡기 관련 폐렴인 경우";
        //    if (p_value == "03") return "타상병으로 입원 중 48시간 이후 발생한 병원 내 폐렴인 경우";
        //    if (p_value == "04") return "악성종양으로 3개월 이내 진단 또는 항암∙방사선 치료를 받은 경우";
        //    if (p_value == "05") return "면역억제제 복용중이거나, 면역질환이 동반된 경우";
        //    if (p_value == "06") return "고용량 스테로이드 치료를 받은 경우";
        //    if (p_value == "07") return "인체면역결핍바이러스, 후천성면역결핍증후군 환자인 경우";
        //    if (p_value == "08") return "90일 이내 2일 이상 입원경력이 있는 경우";
        //    if (p_value == "09") return "타병원 또는 가정간호로 정맥내 항생제 투여 후 내원한 경우";
        //    if (p_value == "10") return "투석중인 환자의 경우";
        //    if (p_value == "11") return "호스피스·완화의료 입원의 경우";
        //    if (p_value == "12") return "수술 후 폐렴";
        //    if (p_value == "13") return "응급수술 등으로 폐렴치료가 지연된 경우";
        //    if (p_value == "14") return "정맥내 항생제 3일 미만 투여한 경우";
        //    return "";
        //}

        //private string GetASM_PLC_SCTY_OBTN_PNEM_CD2(string p_value)
        //{
        //    if (p_value == "해당 없음") return "00";
        //    if (p_value == "입원 후 72시간 이내 정맥내 항생제 투여 받지 않은 경우") return "01";
        //    if (p_value == "인공호흡기 관련 폐렴인 경우") return "02";
        //    if (p_value == "타상병으로 입원 중 48시간 이후 발생한 병원 내 폐렴인 경우") return "03";
        //    if (p_value == "악성종양으로 3개월 이내 진단 또는 항암∙방사선 치료를 받은 경우") return "04";
        //    if (p_value == "면역억제제 복용중이거나, 면역질환이 동반된 경우") return "05";
        //    if (p_value == "고용량 스테로이드 치료를 받은 경우") return "06";
        //    if (p_value == "인체면역결핍바이러스, 후천성면역결핍증후군 환자인 경우") return "07";
        //    if (p_value == "90일 이내 2일 이상 입원경력이 있는 경우") return "08";
        //    if (p_value == "타병원 또는 가정간호로 정맥내 항생제 투여 후 내원한 경우") return "09";
        //    if (p_value == "투석중인 환자의 경우") return "10";
        //    if (p_value == "호스피스·완화의료 입원의 경우") return "11";
        //    if (p_value == "수술 후 폐렴") return "12";
        //    if (p_value == "응급수술 등으로 폐렴치료가 지연된 경우") return "13";
        //    if (p_value == "정맥내 항생제 3일 미만 투여한 경우") return "14";
        //    return "";
        //}
        
        //private string GetANBO_CHG_BF_GAT_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "0") return "해당 없음";
        //    if (p_value == "1") return "증상: 숨가쁨 증상, 가래 증상";
        //    if (p_value == "2") return "체온이 계속 38℃ 이상, 호흡수 증가, 혈압이 저하된 경우";
        //    if (p_value == "3") return "흉부사진: 초기 증상 악화, 없었던 증상 발생";
        //    if (p_value == "4") return "혈액검사: WBC 증가, PLT 감소, CRP 증가";
        //    return "";
        //}

        //private string GetANBO_CHG_BF_GAT_RS_CD(string p_value)
        //{
        //    if (p_value == "해당 없음") return "0";
        //    if (p_value == "증상: 숨가쁨 증상, 가래 증상") return "1";
        //    if (p_value == "체온이 계속 38℃ 이상, 호흡수 증가, 혈압이 저하된 경우") return "2";
        //    if (p_value == "흉부사진: 초기 증상 악화, 없었던 증상 발생") return "3";
        //    if (p_value == "혈액검사: WBC 증가, PLT 감소, CRP 증가") return "4";
        //    return "";
        //}

        private string GetASM_SGRD_JDGM_TL_KND_CD_NM(string p_value)
        {
            if (p_value == "1") return "CURB-65";
            if (p_value == "2") return "CRB-65";
            if (p_value == "3") return "CURB";
            if (p_value == "4") return "CRB";
            if (p_value == "5") return "PSI";
            return "";
        }

        private string GetASM_SGRD_JDGM_TL_KND_CD(string p_value)
        {
            if (p_value == "CURB-65") return "1";
            if (p_value == "CRB-65") return "2";
            if (p_value == "CURB") return "3";
            if (p_value == "CRB") return "4";
            if (p_value == "PSI") return "5";
            return "";
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;                      // 환자등록번호
            txtPnm.Text = m_data.PNM;                      // 환자명
            txtResid_disp.Text = m_data.RESID_DISP;        // 주민번호 표시용

            // A. 기본 정보 파트
            txtASM_HOSP_ARIV_DT_DATE.Text = CUtil.GetDate(m_data.ASM_HOSP_ARIV_DT); // 병원 도착일자
            txtASM_HOSP_ARIV_DT_TIME.Text = CUtil.GetTime(m_data.ASM_HOSP_ARIV_DT); // 병원 도착시간

            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1";    // 퇴원여부 Yes
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2";    // 퇴원여부 No

            txtASM_DSCG_DT_DATE.Text = CUtil.GetDate(m_data.ASM_DSCG_DT); // 퇴원일시 - 날짜
            txtASM_DSCG_DT_TIME.Text = CUtil.GetTime(m_data.ASM_DSCG_DT); // 퇴원일시 - 시간

            rbIPAT_TRM_PNEM_SICK_YN_1.Checked = m_data.IPAT_TRM_PNEM_SICK_YN == "1"; // 입원 중 폐렴 유무 Yes
            rbIPAT_TRM_PNEM_SICK_YN_2.Checked = m_data.IPAT_TRM_PNEM_SICK_YN == "2"; // 입원 중 폐렴 유무 No

            rbDSCG_STAT_RCD_YN_1.Checked = m_data.DSCG_STAT_RCD_YN == "1"; // 퇴원상태 기록 여부 Yes
            rbDSCG_STAT_RCD_YN_2.Checked = m_data.DSCG_STAT_RCD_YN == "2"; // 퇴원상태 기록 여부 No

            CUtil.SetComboboxSelectedValue(cboDSCG_STAT_CD, m_data.DSCG_STAT_CD); // 퇴원상태 표시

            rbRECU_HOSP_VST_YN_1.Checked = m_data.RECU_HOSP_VST_YN == "1"; // 요양원 등에서 내원 여부 Yes
            rbRECU_HOSP_VST_YN_2.Checked = m_data.RECU_HOSP_VST_YN == "2"; // 요양원 등에서 내원 여부 No

            txtRECU_HOSP_NM.Text = m_data.RECU_HOSP_NM;                    // 시설 명칭

            CUtil.SetComboboxSelectedValue(cboASM_VST_PTH_CD, m_data.ASM_VST_PTH_CD); // 내원경로

            CUtil.SetComboboxSelectedValue(cboASM_PLC_SCTY_OBTN_PNEM_CD2, m_data.ASM_PLC_SCTY_OBTN_PNEM_CD2); // 폐렴 관련성 코드
            txtPLC_SCTY_OBTN_PNEM_TXT.Text = m_data.PLC_SCTY_OBTN_PNEM_TXT; // 폐렴 관련성 사유 상세

            // B-1. 산소포화도 검사 여부
            rbASM_EXM_YN_1.Checked = m_data.ASM_EXM_YN == "1";
            rbASM_EXM_YN_2.Checked = m_data.ASM_EXM_YN == "2";

            // OXY Grid: 서브 클래스 리스트로 바인딩
            var oxyList = new List<OXY>();
            for (int i = 0; i < m_data.ASM_EXM_DT.Count; i++)
            {
                oxyList.Add(new OXY
                {
                    ASM_EXM_DT_DATE = CUtil.GetDate(m_data.ASM_EXM_DT[i]),
                    ASM_EXM_DT_TIME = CUtil.GetTime(m_data.ASM_EXM_DT[i]),
                    ASM_OXY_STRT = m_data.ASM_OXY_STRT[i]
                });
            }
            grdOXY.DataSource = oxyList;

            // B-2. 객담배양 검사 여부
            rbASM_PRSC_YN_1.Checked = m_data.ASM_PRSC_YN == "1";
            rbASM_PRSC_YN_2.Checked = m_data.ASM_PRSC_YN == "2";

            var sptumList = new List<SPTUM>();
            for (int i = 0; i < m_data.ASM_PRSC_DT.Count; i++)
            {
                sptumList.Add(new SPTUM
                {
                    ASM_PRSC_DT_DATE = CUtil.GetDate(m_data.ASM_PRSC_DT[i]),
                    ASM_PRSC_DT_TIME = CUtil.GetTime(m_data.ASM_PRSC_DT[i])
                });
            }
            grdSPTUM.DataSource = sptumList;

            // B-3. 혈액배양 검사 여부
            rbASM_GAT_YN_1.Checked = m_data.ASM_GAT_YN == "1";
            rbASM_GAT_YN_2.Checked = m_data.ASM_GAT_YN == "2";

            var bldList = new List<BLD>();
            for (int i = 0; i < m_data.ASM_GAT_DT.Count; i++)
            {
                bldList.Add(new BLD
                {
                    ASM_GAT_DT_DATE = CUtil.GetDate(m_data.ASM_GAT_DT[i]),
                    ASM_GAT_DT_TIME = CUtil.GetTime(m_data.ASM_GAT_DT[i])
                });
            }
            grdBLD.DataSource = bldList;

            // 혈액배양 관련 추가 항목
            rbST1_ANBO_INJC_BF_GAT_YN_1.Checked = m_data.ST1_ANBO_INJC_BF_GAT_YN == "1";
            rbST1_ANBO_INJC_BF_GAT_YN_2.Checked = m_data.ST1_ANBO_INJC_BF_GAT_YN == "2";

            rbANBO_CHG_BF_GAT_YN_1.Checked = m_data.ANBO_CHG_BF_GAT_YN == "1";
            rbANBO_CHG_BF_GAT_YN_2.Checked = m_data.ANBO_CHG_BF_GAT_YN == "2";
            CUtil.SetComboboxSelectedValue(cboANBO_CHG_BF_GAT_RS_CD, m_data.ANBO_CHG_BF_GAT_RS_CD);

            // C. 중증도 판정도구
            rbASM_USE_YN_1.Checked = m_data.ASM_USE_YN == "1";
            rbASM_USE_YN_2.Checked = m_data.ASM_USE_YN == "2";

            var sgrdList = new List<SGRD>();
            for (int i = 0; i < m_data.ASM_USE_DT.Count; i++)
            {
                sgrdList.Add(new SGRD
                {
                    ASM_USE_DT_DATE = CUtil.GetDate(m_data.ASM_USE_DT[i]),
                    ASM_USE_DT_TIME = CUtil.GetTime(m_data.ASM_USE_DT[i]),
                    ASM_SGRD_JDGM_TL_KND_CD = GetASM_SGRD_JDGM_TL_KND_CD_NM(m_data.ASM_SGRD_JDGM_TL_KND_CD[i]),
                    ASM_SGRD_JDGM_TL_TOT_PNT = m_data.ASM_SGRD_JDGM_TL_TOT_PNT[i]
                });
            }
            grdSGRD.DataSource = sgrdList;

            rbCNFS_YN_1.Checked = m_data.CNFS_YN == "1";
            rbCNFS_YN_2.Checked = m_data.CNFS_YN == "2";

            rbBLUR_UNIT_1.Checked = m_data.BLUR_UNIT == "1";
            rbBLUR_UNIT_2.Checked = m_data.BLUR_UNIT == "2";

            txtBLUR_MG_CNT.Text = m_data.BLUR_MG_CNT;
            txtBLUR_MMOL_CNT.Text = m_data.BLUR_MMOL_CNT;
            txtBRT.Text = m_data.BRT;
            txtBPRSU.Text = m_data.BPRSU;

            // D. 항생제 투여
            rbANBO_USE_YN_1.Checked = m_data.ANBO_USE_YN == "1";
            rbANBO_USE_YN_2.Checked = m_data.ANBO_USE_YN == "2";

            var drugList = new List<DRUG>();
            for (int i = 0; i < m_data.MDS_INJC_DT.Count; i++)
            {
                drugList.Add(new DRUG
                {
                    MDS_INJC_DT_DATE = CUtil.GetDate(m_data.MDS_INJC_DT[i]),
                    MDS_INJC_DT_TIME = CUtil.GetTime(m_data.MDS_INJC_DT[i]),
                    MDS_CD = m_data.MDS_CD[i],
                    MDS_NM = m_data.MDS_NM[i]
                });
            }
            grdDRUG.DataSource = drugList;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdOXY, grdOXYView);
            CUtil.RefreshGrid(grdSPTUM, grdSPTUMView);
            CUtil.RefreshGrid(grdBLD, grdBLDView);
            CUtil.RefreshGrid(grdSGRD, grdSGRDView);
            CUtil.RefreshGrid(grdDRUG, grdDRUGView);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // A.기본 정보

            // 1.환자정보
            m_data.ASM_HOSP_ARIV_DT = CUtil.GetDateTime(txtASM_HOSP_ARIV_DT_DATE.Text.ToString(), txtASM_HOSP_ARIV_DT_TIME.Text.ToString()); // 병원도착일사
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2); // 퇴원여부
            m_data.ASM_DSCG_DT = CUtil.GetDateTime(txtASM_DSCG_DT_DATE.Text.ToString(), txtASM_DSCG_DT_TIME.Text.ToString()); // 퇴원일시
            m_data.IPAT_TRM_PNEM_SICK_YN = CUtil.GetRBString(rbIPAT_TRM_PNEM_SICK_YN_1, rbIPAT_TRM_PNEM_SICK_YN_2);
            m_data.DSCG_STAT_RCD_YN = CUtil.GetRBString(rbDSCG_STAT_RCD_YN_1, rbDSCG_STAT_RCD_YN_2);
            m_data.DSCG_STAT_CD = CUtil.GetComboboxSelectedValue(cboDSCG_STAT_CD);

            m_data.RECU_HOSP_VST_YN = CUtil.GetRBString(rbRECU_HOSP_VST_YN_1, rbRECU_HOSP_VST_YN_2);
            m_data.RECU_HOSP_NM = txtRECU_HOSP_NM.Text;
            m_data.ASM_VST_PTH_CD = CUtil.GetComboboxSelectedValue(cboASM_VST_PTH_CD);
            m_data.ASM_PLC_SCTY_OBTN_PNEM_CD2 = CUtil.GetComboboxSelectedValue(cboASM_PLC_SCTY_OBTN_PNEM_CD2);
            m_data.PLC_SCTY_OBTN_PNEM_TXT = txtPLC_SCTY_OBTN_PNEM_TXT.Text;

            // 산소포화도 검사
            m_data.ASM_EXM_YN = CUtil.GetRBString(rbASM_EXM_YN_1, rbASM_EXM_YN_2);
            m_data.ASM_EXM_DT.Clear();
            m_data.ASM_OXY_STRT.Clear();
            foreach (OXY row in (List<OXY>)grdOXY.DataSource)
            {
                m_data.ASM_EXM_DT.Add(CUtil.GetDateTime(row.ASM_EXM_DT_DATE, row.ASM_EXM_DT_TIME));
                m_data.ASM_OXY_STRT.Add(row.ASM_OXY_STRT);
            }

            // 객담배양
            m_data.ASM_PRSC_YN = CUtil.GetRBString(rbASM_PRSC_YN_1, rbASM_PRSC_YN_2);
            m_data.ASM_PRSC_DT.Clear();
            foreach (SPTUM row in (List<SPTUM>)grdSPTUM.DataSource)
            {
                m_data.ASM_PRSC_DT.Add(CUtil.GetDateTime(row.ASM_PRSC_DT_DATE, row.ASM_PRSC_DT_TIME));
            }

            // 혈액배양
            m_data.ASM_GAT_YN = CUtil.GetRBString(rbASM_GAT_YN_1, rbASM_GAT_YN_2);
            m_data.ASM_GAT_DT.Clear();
            foreach (BLD row in (List<BLD>)grdBLD.DataSource)
            {
                m_data.ASM_GAT_DT.Add(CUtil.GetDateTime(row.ASM_GAT_DT_DATE, row.ASM_GAT_DT_TIME));
            }

            m_data.ST1_ANBO_INJC_BF_GAT_YN = CUtil.GetRBString(rbST1_ANBO_INJC_BF_GAT_YN_1, rbST1_ANBO_INJC_BF_GAT_YN_2);
            m_data.ANBO_CHG_BF_GAT_YN = CUtil.GetRBString(rbANBO_CHG_BF_GAT_YN_1, rbANBO_CHG_BF_GAT_YN_2);
            m_data.ANBO_CHG_BF_GAT_RS_CD = CUtil.GetComboboxSelectedValue(cboANBO_CHG_BF_GAT_RS_CD);

            // 중증도 판정도구
            m_data.ASM_USE_YN = CUtil.GetRBString(rbASM_USE_YN_1, rbASM_USE_YN_2);
            m_data.ASM_USE_DT.Clear();
            m_data.ASM_SGRD_JDGM_TL_KND_CD.Clear();
            m_data.ASM_SGRD_JDGM_TL_TOT_PNT.Clear();
            foreach (SGRD row in (List<SGRD>)grdSGRD.DataSource)
            {
                m_data.ASM_USE_DT.Add(CUtil.GetDateTime(row.ASM_USE_DT_DATE, row.ASM_USE_DT_TIME));
                m_data.ASM_SGRD_JDGM_TL_KND_CD.Add(GetASM_SGRD_JDGM_TL_KND_CD(row.ASM_SGRD_JDGM_TL_KND_CD));
                m_data.ASM_SGRD_JDGM_TL_TOT_PNT.Add(row.ASM_SGRD_JDGM_TL_TOT_PNT);
            }

            m_data.CNFS_YN = CUtil.GetRBString(rbCNFS_YN_1, rbCNFS_YN_2);
            m_data.BLUR_UNIT = CUtil.GetRBString(rbBLUR_UNIT_1, rbBLUR_UNIT_2);
            m_data.BLUR_MG_CNT = txtBLUR_MG_CNT.Text;
            m_data.BLUR_MMOL_CNT = txtBLUR_MMOL_CNT.Text;
            m_data.BRT = txtBRT.Text;
            m_data.BPRSU = txtBPRSU.Text;

            // 항생제 투여 정보
            m_data.ANBO_USE_YN = CUtil.GetRBString(rbANBO_USE_YN_1, rbANBO_USE_YN_2);
            m_data.MDS_INJC_DT.Clear();
            m_data.MDS_CD.Clear();
            m_data.MDS_NM.Clear();
            foreach (DRUG row in (List<DRUG>)grdDRUG.DataSource)
            {
                m_data.MDS_INJC_DT.Add(CUtil.GetDateTime(row.MDS_INJC_DT_DATE, row.MDS_INJC_DT_TIME));
                m_data.MDS_CD.Add(row.MDS_CD);
                m_data.MDS_NM.Add(row.MDS_NM);
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
                List<CDataASM023_002> list = (List<CDataASM023_002>)m_view.DataSource;
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
                List<CDataASM023_002> list = (List<CDataASM023_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowOXY_Click(object sender, EventArgs e)
        {
            List<OXY> list = (List<OXY>)grdOXY.DataSource;
            list.Add(new OXY());
            RefreshGrid();
        }

        private void btnDelRowOXY_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdOXYView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<OXY> list = (List<OXY>)grdOXY.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowSPTUM_Click(object sender, EventArgs e)
        {
            List<SPTUM> list = (List<SPTUM>)grdSPTUM.DataSource;
            list.Add(new SPTUM());
            RefreshGrid();
        }

        private void btnDelRowSPTUM_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdSPTUMView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<SPTUM> list = (List<SPTUM>)grdSPTUM.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowBLD_Click(object sender, EventArgs e)
        {
            List<BLD> list = (List<BLD>)grdBLD.DataSource;
            list.Add(new BLD());
            RefreshGrid();
        }

        private void btnDelRowBLD_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdOXYView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<BLD> list = (List<BLD>)grdBLD.DataSource;
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

        private void btnInsRowDRUG_Click(object sender, EventArgs e)
        {
            List<DRUG> list = (List<DRUG>)grdDRUG.DataSource;
            list.Add(new DRUG());
            RefreshGrid();
        }

        private void btnDelRowDRUG_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdDRUGView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<DRUG> list = (List<DRUG>)grdDRUG.DataSource;
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

                    CMakeASM023 make = new CMakeASM023();
                    tran = conn.BeginTransaction();
                    make.MakeASM023(m_data, sysdt, systm, m_User, conn, tran, true);
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

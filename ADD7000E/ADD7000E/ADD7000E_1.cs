using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json.Linq;
using HIRA.EformEntry.Service.Library;

namespace ADD7000E
{
    public partial class ADD7000E_1 : Form
    {
        private HIRA.EformEntry.Model.Document m_doc;
        private bool m_First;

        private Dictionary<string, string> m_dicDesc = new Dictionary<string, string>();

        public ADD7000E_1()
        {
            InitializeComponent();
        }

        private void ADD7000E_1_Load(object sender, EventArgs e)
        {
            m_First = true;
        }

        private void ADD7000E_1_Activated(object sender, EventArgs e)
        {
            if (m_First == false) return;
            m_First = false;

            this.SetDicDesc();
        }

        private void ADD7000E_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        public void DataLoad(HIRA.EformEntry.Model.Document p_doc)
        {
            m_doc = null;
            m_doc = p_doc;
            this.ParseJsonData();
        }

        private void ParseJsonData()
        {
            grdViewer.DataSource = null;
            RefreshGridViewer();


            if (m_doc == null)
            {
                MessageBox.Show("자료가 없습니다.");
                return;
            }

            List<CViewer> list = new List<CViewer>();
            list.Clear();
            grdViewer.DataSource = list;

            foreach (HIRA.EformEntry.Model.Data data in m_doc.Metadata)
            {
                CViewer vi = new CViewer();

                vi.KEY_VALUE = data.Key.ToString();
                vi.DATA_VALUE = data.Value.ToString();

                list.Add(vi);
            }


            foreach (HIRA.EformEntry.Model.Data data in m_doc.Elements)
            {
                CViewer vi = new CViewer();

                vi.KEY_VALUE = data.Key.ToString();
                vi.DATA_VALUE = data.Value.ToString();

                list.Add(vi);
            }

            foreach (HIRA.EformEntry.Model.Table table in m_doc.Tables)
            {
                foreach (HIRA.EformEntry.Model.Row row in table.Rows)
                {
                    CViewer vi = new CViewer();

                    vi.KEY_VALUE = row.Key.ToString();
                    vi.DATA_VALUE = row.Value.ToString();

                    list.Add(vi);
                }
            }

            /*
            JObject obj = JObject.Parse(m_JsonData);
            JObject metadata = (JObject)obj["metadata"];
            JObject elements = (JObject)obj["elements"];
            JObject tables = (JObject)obj["tables"];
            JArray tbl_rcpt = (JArray)tables["TBL_RCPT"];

            CViewer vi;
            vi = new CViewer();
            vi.KEY_VALUE = "공통정보";
            vi.DESC_VALUE = "";
            vi.DATA_VALUE = "";
            vi.ADD1_VALUE = "";
            vi.ADD2_VALUE = "";
            vi.ADD3_VALUE = "";
            vi.ADD4_VALUE = "";
            list.Add(vi);

            foreach (var meta in metadata)
            {
                vi = new CViewer();
                vi.KEY_VALUE = meta.Key.ToString();
                vi.DESC_VALUE = this.GetDescString(vi.KEY_VALUE, "");
                vi.DATA_VALUE = meta.Value.ToString();
                vi.ADD1_VALUE = "";
                vi.ADD2_VALUE = "";
                vi.ADD3_VALUE = "";
                vi.ADD4_VALUE = "";
                list.Add(vi);
            }

            vi = new CViewer();
            vi.KEY_VALUE = "기본정보";
            vi.DESC_VALUE = "";
            vi.DATA_VALUE = "";
            vi.ADD1_VALUE = "";
            vi.ADD2_VALUE = "";
            vi.ADD3_VALUE = "";
            vi.ADD4_VALUE = "";
            list.Add(vi);

            foreach (var elem in elements)
            {
                vi = new CViewer();
                vi.KEY_VALUE = elem.Key.ToString();
                vi.DESC_VALUE = this.GetDescString(vi.KEY_VALUE, "");
                vi.DATA_VALUE = elem.Value.ToString();
                vi.ADD1_VALUE = "";
                vi.ADD2_VALUE = "";
                vi.ADD3_VALUE = "";
                vi.ADD4_VALUE = "";

                list.Add(vi);
            }

            vi = new CViewer();
            vi.KEY_VALUE = "금액정보";
            vi.DESC_VALUE = "";
            vi.DATA_VALUE = "";
            vi.ADD1_VALUE = "";
            vi.ADD2_VALUE = "";
            vi.ADD3_VALUE = "";
            vi.ADD4_VALUE = "";
            list.Add(vi);
            vi = new CViewer();
            vi.KEY_VALUE = "구분(키)";
            vi.DESC_VALUE = "설명";
            vi.DATA_VALUE = "급여 본인";
            vi.ADD1_VALUE = "급여 공단";
            vi.ADD2_VALUE = "급여 전액본인";
            vi.ADD3_VALUE = "비급여 선택";
            vi.ADD4_VALUE = "비급여 선택이외";
            list.Add(vi);

            foreach (JObject tableObj in tbl_rcpt)
            {
                vi = new CViewer();
                vi.KEY_VALUE = tableObj["RCPT_CZITM_CD"].ToString();
                vi.DESC_VALUE = GetDescString("RCPT_CZITM_CD", tableObj["RCPT_CZITM_CD"].ToString());
                vi.DATA_VALUE = tableObj["SLF_BRDN_AMT"].ToString();
                vi.ADD1_VALUE = tableObj["HINSU_BRDN_AMT"].ToString();
                vi.ADD2_VALUE = tableObj["ALAM_SLF_BRDN_AMT"].ToString();
                vi.ADD3_VALUE = tableObj["CHIC_DIAG_AMT"].ToString();
                vi.ADD4_VALUE = tableObj["CHIC_DIAG_EXCP_AMT"].ToString();

                list.Add(vi);
            }
            */

            RefreshGridViewer();
        }

        private void RefreshGridViewer()
        {
            if (grdViewer.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdViewer.BeginInvoke(new Action(() => grdViewerView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdViewerView.RefreshData();
                Application.DoEvents();
            }
        }

        private String GetDescString(String key, String value)
        {
            String strKey = key;
            if ("".Equals(value) == false) strKey += "_" + value;
            String strRet = "";
            if (m_dicDesc.ContainsKey(strKey) == true)
            {
                strRet = m_dicDesc[strKey];
            }
            return strRet;
        }

        private void SetDicDesc()
        {
            // 진료비영수증

            m_dicDesc["SUPL_DATA_FOM_CD"] = "서식코드";
            m_dicDesc["FOM_VER"] = "서식버전";
            m_dicDesc["YKIHO"] = "요양기관기호";
            m_dicDesc["DMD_NO"] = "청구번호";
            m_dicDesc["RCV_NO"] = "접수번호";
            m_dicDesc["RCV_YR"] = "접수년도";
            m_dicDesc["BILL_SNO"] = "청구서일련번호";
            m_dicDesc["SP_SNO"] = "명세서일련번호";
            m_dicDesc["INSUP_TP_CD"] = "보험자구분코드(4.건강보험 5.의료급여 7.보훈 8.자동차보험)";
            m_dicDesc["FOM_REF_BIZ_TP_CD"] = "참고업부구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 99.기타)";
            m_dicDesc["HOSP_RNO"] = "환자 등록번호";
            m_dicDesc["PAT_NM"] = "환자 성명";
            m_dicDesc["PAT_JNO"] = "환자 주민등록번호";

            m_dicDesc["DGSBJT_CD"] = "진료과";
            m_dicDesc["RECE_TY_CD"] = "수납유형(1.퇴원 2.중간)";
            m_dicDesc["DIAG_STA_DD"] = "진료시작일";
            m_dicDesc["DIAG_END_DD"] = "진료종료일";
            m_dicDesc["NGT_DIAG_CD"] = "야간(공휴일)진료(1.야간 2.공휴일 3.야간+공휴일)";
            m_dicDesc["DRG_NO"] = "질병군(DRG)번호";
            m_dicDesc["SRM_NM"] = "병실";
            m_dicDesc["PTNT_TY_CD"] = "환자구분(1.건강보험 2.의료급여1종 3.의료급여2종)";
            m_dicDesc["RCPT_NO"] = "영수증번호";

            m_dicDesc["RCPT_CZITM_CD_ENAMT"] = "진찰료";
            m_dicDesc["RCPT_CZITM_CD_INAMT_BDM1"] = "입원료(1인실)";
            m_dicDesc["RCPT_CZITM_CD_INAMT_BDM2"] = "입원료(2.3인실)";
            m_dicDesc["RCPT_CZITM_CD_INAMT_BDM4"] = "입원료(4인실이상)";
            m_dicDesc["RCPT_CZITM_CD_FOEP"] = "식대";
            m_dicDesc["RCPT_CZITM_CD_CPMD_ACTAMT"] = "투약및조제료(행위료)";
            m_dicDesc["RCPT_CZITM_CD_CPMD_MDAMT"] = "투약및조제료(약품비)";
            m_dicDesc["RCPT_CZITM_CD_IJCT_ACTAMT"] = "주사료(행위료)";
            m_dicDesc["RCPT_CZITM_CD_IJCT_MDAMT"] = "주사료(약품비)";
            m_dicDesc["RCPT_CZITM_CD_NCAMT"] = "마취료";
            m_dicDesc["RCPT_CZITM_CD_DPAMT"] = "처치및수술료";
            m_dicDesc["RCPT_CZITM_CD_EXAMT"] = "검사료";
            m_dicDesc["RCPT_CZITM_CD_IMG_DIAMT"] = "영상진단료";
            m_dicDesc["RCPT_CZITM_CD_RADT_TRRT"] = "방사선치료료";
            m_dicDesc["RCPT_CZITM_CD_TMCAT"] = "치료재료대";
            m_dicDesc["RCPT_CZITM_CD_PTR"] = "재활및물리치료료";
            m_dicDesc["RCPT_CZITM_CD_PYAMT"] = "정신요법료";
            m_dicDesc["RCPT_CZITM_CD_BCF"] = "전혈및혈액성분제제료";
            m_dicDesc["RCPT_CZITM_CD_CT_DIAMT"] = "CT진단료";
            m_dicDesc["RCPT_CZITM_CD_MRI_DIAMT"] = "MRI진단료";
            m_dicDesc["RCPT_CZITM_CD_PET_DIAMT"] = "PET진단료";
            m_dicDesc["RCPT_CZITM_CD_ULTRS_DIAMT"] = "초음파진단료";
            m_dicDesc["RCPT_CZITM_CD_CRFE"] = "보철.교정료";
            m_dicDesc["RCPT_CZITM_CD_ETC_AMT"] = "기타";
            m_dicDesc["RCPT_CZITM_CD_YPAY_XPNS"] = "<시행령 별표2 제4호의 요양급여>";
            m_dicDesc["RCPT_CZITM_CD_A65G"] = "65세 이상 등 정액";
            m_dicDesc["RCPT_CZITM_CD_FAMT_MDFEE"] = "정액수가(요양(의료)병원)";
            m_dicDesc["RCPT_CZITM_CD_ICSN_MDFEE"] = "포괄수가진료비";

            m_dicDesc["GSUM_SLF_BRDN_AMT"] = "① 일부본인부담-본인부담금";
            m_dicDesc["GSUM_HINSU_BRDN_AMT"]= "② 일부본인부담-공단부담금";
            m_dicDesc["GSUM_ALAM_SLF_BRDN_AMT"]= "③ 전액본인부담";
            m_dicDesc["GSUM_CHIC_DIAG_AMT"] = "④ 선택진료료";
            m_dicDesc["GSUM_CHIC_DIAG_EXCP_AMT"] = "⑤ 선택진료료 이외";
            m_dicDesc["MX_ECS_AMT"] = "⑥ 상한액 초과금";
            m_dicDesc["DAMT_TOT_AMT"] = "⑦ 진료비 총액(①+②+③+④+⑤)";
            m_dicDesc["PTNT_BRDN_TOT_AMT"] = "⑧ 환자부담 총액(①-⑥)+③+④+⑤)";
            m_dicDesc["PPMT_AMT"] = "⑨ 이미 납부한 금액";
            m_dicDesc["PYMN_EPT_AMT"] = "⑩ 납부할 금액(⑧-⑨)";
            m_dicDesc["PPMT_CARD_AMT"] = "⑪-1. 납부한 금액.카드";
            m_dicDesc["PPMT_CASH_RCPT_AMT"] = "⑪-2. 납부한 금액.현금영수증";
            m_dicDesc["PPMT_CASH_AMT"] = "⑪-3. 납부한 금액.현금";
            m_dicDesc["PPMT_GSUM_AMT"] = "⑪-4. 납부한 금액.합계";
            m_dicDesc["PYMN_BLCE_AMT"] = "⑫ 납부하지 않은 금액(⑩-⑪)";

            m_dicDesc["POSI_CFR_NO"] = "현금영수증.신분확인번호";
            m_dicDesc["CASH_ADMT_NO"] = "현금영수증.현금승인번호";
            m_dicDesc["PTNT_PYMN_TOT_AMT"] = "환자 실지불금액";
            m_dicDesc["PTNT_RCVB_AMT"] = "환자 미수금액";
            m_dicDesc["EMGY_MED_SPAMT"] = "긴급의료 지원금액";
            m_dicDesc["INFN_CARE_SPAMT"] = "무한돌봄 지원금액";
            m_dicDesc["HNDP_SPAMT"] = "장애인 지원금액";
            m_dicDesc["PTNT_GRP_SPAMT"] = "환우회 지원금액";
            m_dicDesc["HOSP_OSLF_SPAMT"] = "병원자체 지원금액";
            m_dicDesc["ETC_SPAMT"] = "기타 지원금액";
            m_dicDesc["RDXN_AMT"] = "감면액";
            m_dicDesc["YADM_OPT_PUSE_DTL_TXT"] = "요양(의료)기관 임의활용공간";
            m_dicDesc["CHIC_DIAG_APL_CD"] = "선택진료 신청";
            m_dicDesc["RECU_CL_CD"] = "요양(의료)기관 종류";
            m_dicDesc["BIZRNO"] = "사업자등록번호";
            m_dicDesc["CPNM_NM"] = "상호";
            m_dicDesc["TELNO"] = "전화번호";
            m_dicDesc["OFC_LOC_TXT"] = "사업장 소재지";
            m_dicDesc["RPPR_NM"] = "대표자";
            m_dicDesc["ISUE_DD"] = "발급일";

            // 퇴원요약지
            m_dicDesc["IPAT_DT"] = "입원일시";
            m_dicDesc["IPAT_DGSBJT_CD"] = "입원과";

        }

    }
}

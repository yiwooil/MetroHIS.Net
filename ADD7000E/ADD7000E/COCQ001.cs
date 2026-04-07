using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HIRA.EformEntry;
using HIRA.EformEntry.Model;
using HIRA.EformEntry.ResponseModel;

using System.Windows.Forms;

namespace ADD7000E
{
    class COCQ001
    {
        public HIRA.EformEntry.Model.Document GetDocument(CDataTI2A i2a, CHosInfo p_HosInfo)
        {
            CDataTK71T k71t = new CDataTK71T();
            k71t.Clear();
            k71t.SetData(i2a);
            int etcDacdCount = k71t.GetEtcDacdCount();

            // 의료의질향상을위한점검표
            HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();

            // Metadata
            doc.Metadata["SUPL_DATA_FOM_CD"].Value = "OCQ001"; // 서식코드
            doc.Metadata["FOM_VER"].Value = "001"; // 서식버전
            doc.Metadata["YKIHO"].Value = p_HosInfo.GetHosId(); // 요양기관기호

            doc.Metadata["DMD_NO"].Value = i2a.DEMNO; // 청구번호
            doc.Metadata["RCV_NO"].Value = i2a.CNECNO; // 접수번호. 접수번호가 없는 경우 0000000
            doc.Metadata["RCV_YR"].Value = i2a.RCV_YR; // 접수년도 CCYY
            doc.Metadata["BILL_SNO"].Value = i2a.DCOUNT; // 청구서일련번호(접수전이면 0, 원청구는 1, 보완청구는 심결통보서에 있는 번호)
            doc.Metadata["SP_SNO"].Value = i2a.EPRTNO; // 명세서 일련번호
            doc.Metadata["INSUP_TP_CD"].Value = i2a.INSUP_TP_CD; // 보험자구분코드 (4:건강보험 5:의료급여 7:보훈 8:지동차보험)
            doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "06"; // 참고업무구분코드 (01:1차심사 02:심사보완 03:이의신청 04:평가 05:진료비민원 06:신포괄 99:기타)
            doc.Metadata["DTL_BIZ_CD"].Value = "NDR"; // 업무상세코드 참고업무의 상세 업무구분이 있는 경우(참고업부구분코드가 '04:평가'인 경우)필수기재(상세코드는 업무별로 별도 안내 받은 코드를 기재)
            doc.Metadata["HOSP_RNO"].Value = i2a.PID; // 의료기관에서 부여한 환자등록번호
            doc.Metadata["PAT_NM"].Value = i2a.PNM; // 수진자 성명
            doc.Metadata["PAT_JNO"].Value = i2a.RESID; // 수진자 주민등록번호 ("-" 생략)

            // A. 일반사항
            doc.Elements["WRT_DD"].Value = k71t.RPTDT; // 작성일 CCYYMMDD
            doc.Elements["DR_NM"].Value = ""; // 의사 성명
            doc.Elements["NURSE_NM"].Value = ""; // 간호사 성명
            doc.Elements["WRTP_NM"].Value = k71t.DRNM; // 작성자성명
            //doc.Elements["APND_DATA_NO"].Value = ""; // 작성자서명. 공인인증서명 완료된 작성자명(서명 이미지)
            doc.Elements["IPAT_DD"].Value = k71t.BEDEDT; // 입원일 CCYYMMDD
            doc.Elements["DSCG_DD"].Value = k71t.BEDODT; // 퇴원일 CCYYMMDD
            doc.Elements["MSICK_CD"].Value = k71t.GetMajDacd(); // 주진단코드
            doc.Elements["MDIAG_NM"].Value = k71t.GetMajDanm(); // 주진단명

            if (etcDacdCount > 0)
            {
                // 기타진단(1~99)
                doc.Tables["TBL_ETC_DIAG"].Columns.Add("ETC_SICK_CD"); // 기타진단코드
                doc.Tables["TBL_ETC_DIAG"].Columns.Add("ETC_DIAG_NM"); // 기타진단명

                for (int i = 0; i < etcDacdCount; i++)
                {
                    // 기타진단
                    doc.Tables["TBL_ETC_DIAG"].AddRow();
                    doc.Tables["TBL_ETC_DIAG"].Rows[i]["ETC_SICK_CD"].Value = k71t.GetEtcDacd(i); // 기타진단코드
                    doc.Tables["TBL_ETC_DIAG"].Rows[i]["ETC_DIAG_NM"].Value = k71t.GetEtcDanm(i); // 기타진단명
                }
            }

            // B. 의료의 질 향상을 위한 점검표

            doc.Elements["SOPR_BF_EXM_ENFC_YN"].Value = k71t.GetItemValue("1.1.1"); // 수술전 필요검사시행 1.시행 2.미시행
            doc.Elements["SOPR_WRCN_WRT_YN"].Value = k71t.GetItemValue("1.1.2"); // 수술 동의서 작성 1.작성 2.미작성
            doc.Elements["SOPR_EDU_ENFC_YN"].Value = k71t.GetItemValue("1.1.3"); // 수술 전후 환자 교육 시행 1.시행 2.미시행
            doc.Elements["SOPR_NRM_ENFC_YN"].Value = k71t.GetItemValue("1.1.4"); // 당일 예정 수술의 정상적 시행 1.시행 2.미시행

            doc.Elements["PYC_ACDT_YN"].Value = k71t.GetItemValue("2.1.1"); // 불의의 병원내 물리적 사고 1.없음 2.있음
            doc.Elements["BLTS_ACDT_YN"].Value = k71t.GetItemValue("2.1.2"); // 수혈사고 혹은 수혈부작용 1.없음 2.있음
            doc.Elements["MDCT_ACDT_YN"].Value = k71t.GetItemValue("2.1.3"); // 투약실수 혹인 약물부작용 1.없음 2.있음
            doc.Elements["MDCT_ACDT_TXT"].Value = k71t.GetItemValueEx("2.1.3"); // 세부내용 1.사무착오 2.과다투여 3.약물알레르기 4.약물열 5.기타부작용
            doc.Elements["NCT_ACDT_YN"].Value = k71t.GetItemValue("2.1.4"); // 마취사고 혹은 마취부작용 1.없음 2.있음
            doc.Elements["NCT_ACDT_TXT"].Value = k71t.GetItemValueEx("2.1.4"); // 세부내용 Ex.A1 A.전신마취 B.척추마취 C.기타국소마취 1.중추신경계 2.순환계 3.호흡계 4.과민반응 5.국소합병증 6.기타부작용
            doc.Elements["SPRM_USE_YN"].Value = k71t.GetItemValue("2.2"); // 중환자실 이용 1.없음 2.있음
            doc.Elements["INFC_SYMT_YN"].Value = k71t.GetItemValue("2.3"); // 감염증 1.없음 2.있음.
            doc.Elements["SOPR_AF_CPCT_YN"].Value = k71t.GetItemValue("2.4"); // 수술후 합병증 1.없음 2.있음.
            doc.Elements["SOPR_AF_CPCT_TXT"].Value = k71t.GetItemValueEx("2.4"); // 수술후 합병증이 있는 경우 세부내용
            doc.Elements["RE_SOPR_YN"].Value = k71t.GetItemValue("2.5"); // 재수술 1.없음 2.있음
            doc.Elements["DEATH_YN"].Value = k71t.GetItemValue("2.6"); // 사망 1.없음 2.있음
            doc.Elements["DEATH_TY_CD"].Value = k71t.GetItemValueEx("2.6"); // 사망의 유형 1.수술중 혹은 수술직후 사망 2.기타 예상하지 못한 사망

            doc.Elements["DSCG_SMDC_WRT_YN"].Value = k71t.GetItemValue("3.1"); // 퇴원기록지 작성 1.작성 2.미작성
            doc.Elements["DSCG_PLAN_PPRT_YN"].Value = k71t.GetItemValue("3.2"); // 퇴원계획의 적절성 여부 1.예 2.아니오
            doc.Elements["NRM_DSCG_YN"].Value = k71t.GetItemValue("3.3"); // 정상퇴원여부 1.정상 2.이상
            doc.Elements["DSCG_TY_CD"].Value = k71t.GetItemValueEx("3.3"); // 퇴원의 유형 1.의학적 권고에 의한 퇴원 2.타의료기관으로 전원 3.사망

            doc.Elements["BPRSU_PRBM_YN"].Value = k71t.GetItemValue("3.4.1"); // 혈압 1.없음 2.있음
            doc.Elements["TMPR_PRBM_YN"].Value = k71t.GetItemValue("3.4.2"); // 체온 1.없음 2.있음
            doc.Elements["PULS_PRBM_YN"].Value = k71t.GetItemValue("3.4.3"); // 맥박 1.없음 2.있음
            doc.Elements["EXM_PRBM_YN"].Value = k71t.GetItemValue("3.4.4"); // 해결되지 않았고 설명이 없는 검사결과 이상 1.없음 2.있음
            doc.Elements["SOPR_RGN_PRBM_YN"].Value = k71t.GetItemValue("3.4.5"); // 통증,압통,발적감,수술부위의 문제 1.없음 2.있음

            doc.addDoc();
            return doc;
        }
    }
}

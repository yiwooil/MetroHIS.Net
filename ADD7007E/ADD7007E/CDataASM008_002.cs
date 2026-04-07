using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM008_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM008"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "KHD"; // 업무상세코드

        // A. 기본정보
        public string RECU_EQP_ADM_YN;         // 요양병원 여부 (1=Yes, 2=No)
        public string IPAT_OPAT_TP_CD;         // 진료형태 코드 (1=입원, 2=외래)
        
        public string IPAT_DT;                 // 입원일시 (CCYYMMDDHHMM)
        public string DSCG_YN;                 // 퇴원 여부 (1=Yes, 2=No)
        public string DSCG_FRM_CD;             // 퇴원 형태코드 (01~06, 99)
        public string VST_STAT_CD;             // 외래 내원상태 코드 (01~99, 다중입력)

        public string BLDD_FST_STA_DD;         // 혈액투석 최초 시작일
        public string BLDD_HOFC_FST_STA_DD;    // 본원 최초 시작일

        // B. 환자 정보
        public string CHRON_RNFL_CUZ_SICK_CD;  // 만성신부전 원인상병 코드 (1~9)

        // B-1) 심장질환
        public string HRT_CCM_SICK_YN;         // 심장질환 동반상병 여부
        public string HRT_SICK_SYM;            // 상병분류기호
        public string CDFL_YN;                 // 심부전 여부
        public string CDFL_CD;                 // 심부전 상세 코드 (00~02, 다중)
        public string LVEF_CNT;                // 좌심실 구혈율
        public string CT_RT_CNT;               // 심흉곽비
        public string ATFB_YN;                 // 심방조동 및 세동 여부
        public string ATFB_CD;                 // 세동 상세 코드
        public string ISMA_HRT_DS_YN;          // 허혈성 심장병 여부
        public string ISMA_HRT_DS_CD;          // 허혈성 상세 코드
        public string OHS_YN;                  // 개심수술 여부

        // B-2) 뇌혈관질환
        public string CRBL_CCM_SICK_YN;        // 뇌혈관 동반상병 여부
        public string CRBL_SICK_SYM;           // 뇌혈관 상병분류기호
        public string CRBL_HDP_YN;             // 장애 발생 여부
        public string ASTC_REQR_YN;            // 타인의 도움 필요 여부

        // B-3) 간경변증
        public string LVCR_CCM_SICK_YN;        // 간경변증 여부
        public string LVCR_SICK_SYM;           // 간경변증 상병기호
        public string LVCR_SYMT_CD;            // 간경변증 상세 코드
        public string REMN_LVR_FCLT_EXM_PNT;   // Child-Pugh score

        // B-4) 출혈성 위장관 질환
        public string HMRHG_CCM_SICK_YN;            // 출혈성 위장관 여부
        public string HMRHG_SICK_SYM;           // 상병분류기호
        public string HMRHG_GIT_DS_CD;         // 위장관 질환 상세 코드

        // B-5) 만성폐질환
        public string LUNG_CCM_SICK_YN;        // 만성폐질환 여부
        public string LUNG_SICK_SYM;           // 상병기호
        public string ARTR_BLDVE_OXY_PART_PRES;// 산소분압(mmHg)

        // B-6) 악성종양
        public string TMR_CCM_SICK_YN;                   // 악성종양 유무
        public string TMR_SICK_SYM;             // 상병기호
        public string MNPLS_TMR_TRET_CD;              // 종양 상세 코드

        // B-7) 당뇨병
        public string DBML_CCM_SICK_YN;                 // 당뇨병 동반 여부
        public string DBML_SICK_SYM;           // 상병기호
        public string INSL_IJCT_INJC_YN;       // 지속 인슐린 주사 여부

        // B-8) 장애
        public string HDP_YN;                 // 장애 여부
        public string HDP_TY_CD;               // 장애유형 구분 코드 (01~10 다중)

        // C. 투석 정보
        public string BLDD_STA_DT;             // 혈액투석일시
        public string HEIG;                    // 신장(cm 소수1자리)
        public string DLYS_BWGT_YN;            // 건체중 측정 여부
        public string ASM_DLYS_BWGT;           // 건체중 값

        public string BF_BWGT_YN;              // 투석 전 체중 측정 여부
        public string ASM_BF_BWGT;             // 투석 전 체중

        public string AF_BWGT_YN;              // 투석 후 체중 측정 여부
        public string ASM_AF_BWGT;             // 투석 후 체중

        public string BLDD_PPRT_ASM_YN;        // 혈액투석 적절도 평가 여부
        public string BLDD_PPRT_ASM_CD;        // 적절도 평가 항목 코드 (01: spKt/V, 02: URR)
        public string PPRT_NUV;                // spKt/V 수치
        public string BLUR_DCR_RT;             // URR 수치(%)

        public string MAIN_BLDVE_CH_DECS_CD;   // 주요 혈관통로(월단위 표시)
        public string USE_BLDVE_CH_CD;         // 투석일자 사용 혈관통로

        public string HMTP_INJC_YN;            // 조혈제 투여 여부
        public string HMTP_INJC_DT;            // 조혈제 투여일시

        public string ECG_ENFC_YN;             // EKG 시행 여부
        public string ECG_ENFC_DT;             // EKG 시행일시

        // D-1. 타기관 검사
        public string OIST_EXM_ENFC_YN;        // 타기관 검사 시행 여부
        public List<string> OIST_EXM_NM = new List<string>();         // 검사명 리스트
        public List<string> OIST_MDFEE_CD = new List<string>();   // 수가코드
        public List<string> OIST_ENFC_DD = new List<string>();    // 시행일자
        public List<string> OIST_EXM_RST_TXT = new List<string>();    // 검사결과

        // D-2. 추적관리
        public string CHS_ENFC_YN;             // 추적관리 검사 시행 여부
        public List<string> CHS_EXM_NM = new List<string>();          // 검사명
        public List<string> CHS_MDFEE_CD = new List<string>();    // 수가코드
        public List<string> CHS_ENFC_DD = new List<string>();     // 시행일자
        public List<string> CHS_OIST_ENFC_YN = new List<string>();    // 타기관 시행 여부

        // E. 첨부
        //public string APND_DATA_NO;            // 첨부자료 식별값

        // ---------------------------------------------------------------------------------------------------------------------------------
        // 진단검사결과지(ERD001)
        // ---------------------------------------------------------------------------------------------------------------------------------

        // A.기본정보
        public List<string> ERD_DGSBJT_CD = new List<string>(); // 진료과.해당 검사를 처방한 의사의 진료과목 *** 첫번째 자료만 사용 ***
        public List<string> ERD_IFLD_DTL_SPC_SBJT_CD = new List<string>(); // 내과 세부진료과 *** 첫번째 자료만 사용 ***
        public List<string> ERD_PRSC_DR_NM = new List<string>(); // 처방의사 성명.해당검사를 처방한 의사의 성명 *** 첫번째 자료만 사용 ***

        // B.Text형 검사 결과
        public List<string> ERD_EXM_PRSC_DT = new List<string>(); // 처방일시(ccyymmddhhmm)
        public List<string> ERD_EXM_GAT_DT = new List<string>(); // 채취일시(ccyymmddhhmm)
        public List<string> ERD_EXM_RCV_DT = new List<string>(); // 접수일시(ccyymmddhhmm)
        public List<string> ERD_EXM_RST_DT = new List<string>(); // 결과일시(ccyymmddhhmm)
        public List<string> ERD_EXM_SPCM_CD = new List<string>(); // 검체종류(01.혈액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
        public List<string> ERD_EXM_SPCM_ETC_TXT = new List<string>(); // 검체종류상세(검체종류가 99인 경우 평문기재)
        public List<string> ERD_EXM_MDFEE_CD = new List<string>(); // 수가코드(없으면 '00')
        public List<string> ERD_EXM_CD = new List<string>(); // 검사코드.병원에서 부여한 코드
        public List<string> ERD_EXM_NM = new List<string>(); //검사명.병원에서 부여한 명칭
        public List<string> ERD_EXM_RST_TXT = new List<string>(); // 검사결과
        public List<string> ERD_DCT_DR_NM = new List<string>(); // 판독의사 성명
        public List<string> ERD_DCT_DR_LCS_NO = new List<string>(); // 판독의사 면허번호

        // C.Grid형 검사 결과(처방일시부터 검사결과까지는 Text형 검사결과와 동일하게 사용한다.)
        public List<string> ERD_EXM_REF_TXT = new List<string>();// 참고치
        public List<string> ERD_EXM_UNIT = new List<string>(); // 검사의 단위
        public List<string> ERD_EXM_ADD_TXT = new List<string>(); // 추가정보(H.높음 L.낮음 정상, 이상값 등)

        // D.기타정보
        public List<string> ERD_RMK_TXT = new List<string>(); // 비고  *** 첫번째 자료만 사용 ***

        public List<string> ERD_RST_TYPE = new List<string>(); // 결과가 Text인지 Grid인지 구분 (T.Text G.Grid)


        // ---------------------------------------------------------------------------------------------------------------------------------
        // 영상검사결과지(ERR001)
        // ---------------------------------------------------------------------------------------------------------------------------------

        // A.검사 정보 및 결과

        // A.1.검사 정보 및 결과
        public List<string> ERR_DGSBJT_CD = new List<string>(); // 진료과(청구진료과목)
        public List<string> ERR_IFLD_DTL_SPC_SBJT_CD = new List<string>(); // 내부 세부전문과목
        public List<string> ERR_PRSC_DR_NM = new List<string>(); // 처방의사 성명.해당 검사를 처방한 의사의 성명
        public List<string> ERR_EXM_PRSC_DT = new List<string>(); // 처방일시. 해당 영상검사를 처방한 날짜와 시간(ccyymmddhhmm)
        public List<string> ERR_EXM_EXEC_DT = new List<string>(); // 검사일시. 해당 검사를 시행한 날짜외 사간(ccyymmddhhmm)
        public List<string> ERR_EXM_RST_DT = new List<string>(); // 판독일시. 해당 영상검사에 대패 판독의가 판독결과지를 작성한 날찌와 시간(ccyymmddhhmm)
        public List<string> ERR_DCT_DR_NM = new List<string>(); // 판독의사 성명
        public List<string> ERR_DCT_DR_LCS_NO = new List<string>(); // 판독의사 면허번호
        public List<string> ERR_EXM_MDFEE_CD = new List<string>(); // 수가코드(없으면 '00')
        public List<string> ERR_EXM_CD = new List<string>(); // 검사코드.병원에서 부여한 코드
        public List<string> ERR_EXM_NM = new List<string>(); // 검사명.병원에서 부여한 명칭
        public List<string> ERR_EXM_RST_TXT = new List<string>(); // 판독결과.평문기재

        // A.2.비고
        public List<string> ERR_RMK_TXT = new List<string>(); // 비고. 평문 *** 첫번째 자료만 사용 ***

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본정보
            RECU_EQP_ADM_YN = "";           // 요양병원 여부
            IPAT_OPAT_TP_CD = "";           // 진료형태
            IPAT_DT = "";                   // 입원일시
            DSCG_YN = "";                   // 퇴원여부
            DSCG_FRM_CD = "";               // 퇴원형태 코드
            VST_STAT_CD = "";               // 내원상태 코드
            BLDD_FST_STA_DD = "";           // 최초 혈액투석 시작일
            BLDD_HOFC_FST_STA_DD = "";      // 본원 최초 시작일

            // B. 환자 정보
            CHRON_RNFL_CUZ_SICK_CD = "";    // 만성신부전 원인 상병코드

            HRT_CCM_SICK_YN = "";           // 심장질환 여부
            HRT_SICK_SYM = "";              // 상병분류기호
            CDFL_YN = "";                   // 심부전 여부
            CDFL_CD = "";                   // 심부전 상세 코드
            LVEF_CNT = "";                  // 좌심실 구혈율
            CT_RT_CNT = "";                 // 심흉곽비
            ATFB_YN = "";                   // 심방조동 여부
            ATFB_CD = "";                   // 심방 세동 상세
            ISMA_HRT_DS_YN = "";            // 허혈성 심장병 여부
            ISMA_HRT_DS_CD = "";            // 허혈성 심장병 상세
            OHS_YN = "";                    // 개심수술 여부

            CRBL_CCM_SICK_YN = "";          // 뇌혈관질환 여부
            CRBL_SICK_SYM = "";             // 뇌혈관 상병코드
            CRBL_HDP_YN = "";               // 장애발생 여부
            ASTC_REQR_YN = "";              // 타인의 도움 필요 여부

            LVCR_CCM_SICK_YN = "";          // 간경변증 여부
            LVCR_SICK_SYM = "";             // 간경변증 상병
            LVCR_SYMT_CD = "";              // 간경변 상세 코드
            REMN_LVR_FCLT_EXM_PNT = "";     // Child-Pugh score

            HMRHG_CCM_SICK_YN = "";              // 출혈성 위장관 질환 여부
            HMRHG_SICK_SYM = "";             // 위장관 질환 상병코드
            HMRHG_GIT_DS_CD = "";           // 위장관 질환 상세 코드

            LUNG_CCM_SICK_YN = "";          // 만성폐질환
            LUNG_SICK_SYM = "";             // 폐질환 상병
            ARTR_BLDVE_OXY_PART_PRES = "";  // 산소분압

            TMR_CCM_SICK_YN = "";                     // 악성종양
            TMR_SICK_SYM = "";               // 종양 상병코드
            MNPLS_TMR_TRET_CD = "";                // 종양 상세

            DBML_CCM_SICK_YN = "";                   // 당뇨병 여부
            DBML_SICK_SYM = "";             // 당뇨 상병
            INSL_IJCT_INJC_YN = "";         // 인슐린 주사 여부

            HDP_YN = "";                   // 장애인 여부(3급 이상)
            HDP_TY_CD = "";                 // 장애유형 코드 (다중 선택)

            // C. 투석 정보
            BLDD_STA_DT = "";               // 투석 일시
            HEIG = "";                      // 신장
            DLYS_BWGT_YN = "";              // 건체중 측정 여부
            ASM_DLYS_BWGT = "";             // 건체중 값
            BF_BWGT_YN = "";                // 투석 전 체중 측정 여부
            ASM_BF_BWGT = "";               // 투석 전 체중
            AF_BWGT_YN = "";                // 투석 후 체중 측정 여부
            ASM_AF_BWGT = "";               // 투석 후 체중

            BLDD_PPRT_ASM_YN = "";          // 혈액투석 적절도 평가 여부
            BLDD_PPRT_ASM_CD = "";          // 적절도 평가 항목 코드
            PPRT_NUV = "";                  // spKt/V 수치
            BLUR_DCR_RT = "";               // URR 수치(%)

            MAIN_BLDVE_CH_DECS_CD = "";     // 월단위 혈관통로
            USE_BLDVE_CH_CD = "";           // 해당일 혈관통로

            HMTP_INJC_YN = "";              // 조혈제 투여 여부
            HMTP_INJC_DT = "";              // 조혈제 투여 일시

            ECG_ENFC_YN = "";               // EKG 시행 여부
            ECG_ENFC_DT = "";              // EKG 시행일

            // D-1. 타기관검사
            OIST_EXM_ENFC_YN = "";          // 타기관검사 여부
            OIST_EXM_NM.Clear();            // 검사명 리스트 초기화
            OIST_MDFEE_CD.Clear();      // 수가코드 리스트 초기화
            OIST_ENFC_DD.Clear();       // 시행일자 리스트 초기화
            OIST_EXM_RST_TXT.Clear();       // 결과값 리스트 초기화

            // D-2. 추적관리
            CHS_ENFC_YN = "";               // 추적관리 시행 여부
            CHS_EXM_NM.Clear();             // 검사명 리스트 초기화
            CHS_MDFEE_CD.Clear();       // 수가코드 리스트 초기화
            CHS_ENFC_DD.Clear();        // 시행일자 리스트 초기화
            CHS_OIST_ENFC_YN.Clear();       // 타기관 시행 여부 리스트 초기화

            // E. 첨부자료 (비필수)
            // APND_DATA_NO = "";           // 관련 이미지나 파일이 있다면 사용 (선택 사항)

            // ---------------------------------------------------------------------------------------------------------------------------------
            // 진단검사결과지(ERD001)
            // ---------------------------------------------------------------------------------------------------------------------------------

            // A.기본정보
            ERD_DGSBJT_CD.Clear(); // 진료과.해당 검사를 처방한 의사의 진료과목 *** 첫번째 자료만 사용 ***
            ERD_IFLD_DTL_SPC_SBJT_CD.Clear(); // 내과 세부진료과 *** 첫번째 자료만 사용 ***
            ERD_PRSC_DR_NM.Clear(); // 처방의사 성명.해당검사를 처방한 의사의 성명 *** 첫번째 자료만 사용 ***

            // B.Text형 검사 결과
            ERD_EXM_PRSC_DT.Clear(); // 처방일시(ccyymmddhhmm)
            ERD_EXM_GAT_DT.Clear(); // 채취일시(ccyymmddhhmm)
            ERD_EXM_RCV_DT.Clear(); // 접수일시(ccyymmddhhmm)
            ERD_EXM_RST_DT.Clear(); // 결과일시(ccyymmddhhmm)
            ERD_EXM_SPCM_CD.Clear(); // 검체종류(01.혈액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
            ERD_EXM_SPCM_ETC_TXT.Clear(); // 검체종류상세(검체종류가 99인 경우 평문기재)
            ERD_EXM_MDFEE_CD.Clear(); // 수가코드(없으면 '00')
            ERD_EXM_CD.Clear(); // 검사코드.병원에서 부여한 코드
            ERD_EXM_NM.Clear(); //검사명.병원에서 부여한 명칭
            ERD_EXM_RST_TXT.Clear(); // 검사결과
            ERD_DCT_DR_NM.Clear(); // 판독의사 성명
            ERD_DCT_DR_LCS_NO.Clear(); // 판독의사 면허번호

            // C.Grid형 검사 결과(처방일시부터 검사결과까지는 Text형 검사결과와 동일하게 사용한다.)
            ERD_EXM_REF_TXT.Clear();// 참고치
            ERD_EXM_UNIT.Clear(); // 검사의 단위
            ERD_EXM_ADD_TXT.Clear(); // 추가정보(H.높음 L.낮음 정상, 이상값 등)

            // D.기타정보
            ERD_RMK_TXT.Clear(); // 비고  *** 첫번째 자료만 사용 ***

            ERD_RST_TYPE.Clear(); // 결과가 Text인지 Grid인지 구분 (T.Text G.Grid)


            // ---------------------------------------------------------------------------------------------------------------------------------
            // 영상검사결과지(ERR001)
            // ---------------------------------------------------------------------------------------------------------------------------------

            // A.검사 정보 및 결과

            // A.1.검사 정보 및 결과
            ERR_DGSBJT_CD.Clear(); // 진료과(청구진료과목)
            ERR_IFLD_DTL_SPC_SBJT_CD.Clear(); // 내부 세부전문과목
            ERR_PRSC_DR_NM.Clear(); // 처방의사 성명.해당 검사를 처방한 의사의 성명
            ERR_EXM_PRSC_DT.Clear(); // 처방일시. 해당 영상검사를 처방한 날짜와 시간(ccyymmddhhmm)
            ERR_EXM_EXEC_DT.Clear(); // 검사일시. 해당 검사를 시행한 날짜외 사간(ccyymmddhhmm)
            ERR_EXM_RST_DT.Clear(); // 판독일시. 해당 영상검사에 대패 판독의가 판독결과지를 작성한 날찌와 시간(ccyymmddhhmm)
            ERR_DCT_DR_NM.Clear(); // 판독의사 성명
            ERR_DCT_DR_LCS_NO.Clear(); // 판독의사 면허번호
            ERR_EXM_MDFEE_CD.Clear(); // 수가코드(없으면 '00')
            ERR_EXM_CD.Clear(); // 검사코드.병원에서 부여한 코드
            ERR_EXM_NM.Clear(); // 검사명.병원에서 부여한 명칭
            ERR_EXM_RST_TXT.Clear(); // 판독결과.평문기재

            // A.2.비고
            ERR_RMK_TXT.Clear(); // 비고. 평문 *** 첫번째 자료만 사용 ***

        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";                  // SQL 문자열 초기화
            List<object> para;               // 파라미터 리스트

            // 1. 메인 테이블: TI84_ASM008
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM008 ";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                // A. 기본정보
                RECU_EQP_ADM_YN = reader["RECU_EQP_ADM_YN"].ToString();      // 요양병원 여부
                IPAT_OPAT_TP_CD = reader["IPAT_OPAT_TP_CD"].ToString();      // 진료형태
                IPAT_DT = reader["IPAT_DT"].ToString();                      // 입원일시
                DSCG_YN = reader["DSCG_YN"].ToString();                      // 퇴원 여부
                DSCG_FRM_CD = reader["DSCG_FRM_CD"].ToString();              // 퇴원형태
                VST_STAT_CD = reader["VST_STAT_CD"].ToString();              // 외래 내원상태
                BLDD_FST_STA_DD = reader["BLDD_FST_STA_DD"].ToString();      // 최초 투석일
                BLDD_HOFC_FST_STA_DD = reader["BLDD_HOFC_FST_STA_DD"].ToString(); // 본원 최초 투석일

                // B. 환자정보
                CHRON_RNFL_CUZ_SICK_CD = reader["CHRON_RNFL_CUZ_SICK_CD"].ToString(); // 만성신부전 원인

                // 심장질환
                HRT_CCM_SICK_YN = reader["HRT_CCM_SICK_YN"].ToString();
                HRT_SICK_SYM = reader["HRT_SICK_SYM"].ToString();
                CDFL_YN = reader["CDFL_YN"].ToString();
                CDFL_CD = reader["CDFL_CD"].ToString();
                LVEF_CNT = reader["LVEF_CNT"].ToString();
                CT_RT_CNT = reader["CT_RT_CNT"].ToString();
                ATFB_YN = reader["ATFB_YN"].ToString();
                ATFB_CD = reader["ATFB_CD"].ToString();
                ISMA_HRT_DS_YN = reader["ISMA_HRT_DS_YN"].ToString();
                ISMA_HRT_DS_CD = reader["ISMA_HRT_DS_CD"].ToString();
                OHS_YN = reader["OHS_YN"].ToString();

                // 뇌혈관질환
                CRBL_CCM_SICK_YN = reader["CRBL_CCM_SICK_YN"].ToString();
                CRBL_SICK_SYM = reader["CRBL_SICK_SYM"].ToString();
                CRBL_HDP_YN = reader["CRBL_HDP_YN"].ToString();
                ASTC_REQR_YN = reader["ASTC_REQR_YN"].ToString();

                // 간경변증
                LVCR_CCM_SICK_YN = reader["LVCR_CCM_SICK_YN"].ToString();
                LVCR_SICK_SYM = reader["LVCR_SICK_SYM"].ToString();
                LVCR_SYMT_CD = reader["LVCR_SYMT_CD"].ToString();
                REMN_LVR_FCLT_EXM_PNT = reader["REMN_LVR_FCLT_EXM_PNT"].ToString();

                // 위장관 질환
                HMRHG_CCM_SICK_YN = reader["HMRHG_CCM_SICK_YN"].ToString();
                HMRHG_SICK_SYM = reader["HMRHG_SICK_SYM"].ToString();
                HMRHG_GIT_DS_CD = reader["HMRHG_GIT_DS_CD"].ToString();

                // 폐질환
                LUNG_CCM_SICK_YN = reader["LUNG_CCM_SICK_YN"].ToString();
                LUNG_SICK_SYM = reader["LUNG_SICK_SYM"].ToString();
                ARTR_BLDVE_OXY_PART_PRES = reader["ARTR_BLDVE_OXY_PART_PRES"].ToString();

                // 악성종양
                TMR_CCM_SICK_YN = reader["TMR_CCM_SICK_YN"].ToString();
                TMR_SICK_SYM = reader["TMR_SICK_SYM"].ToString();
                MNPLS_TMR_TRET_CD = reader["MNPLS_TMR_TRET_CD"].ToString();

                // 당뇨병
                DBML_CCM_SICK_YN = reader["DBML_CCM_SICK_YN"].ToString();
                DBML_SICK_SYM = reader["DBML_SICK_SYM"].ToString();
                INSL_IJCT_INJC_YN = reader["INSL_IJCT_INJC_YN"].ToString();

                // 장애
                HDP_YN = reader["HDP_YN"].ToString();
                HDP_TY_CD = reader["HDP_TY_CD"].ToString();

                // C. 투석정보
                BLDD_STA_DT = reader["BLDD_STA_DT"].ToString();
                HEIG = reader["HEIG"].ToString();
                DLYS_BWGT_YN = reader["DLYS_BWGT_YN"].ToString();
                ASM_DLYS_BWGT = reader["ASM_DLYS_BWGT"].ToString();
                BF_BWGT_YN = reader["BF_BWGT_YN"].ToString();
                ASM_BF_BWGT = reader["ASM_BF_BWGT"].ToString();
                AF_BWGT_YN = reader["AF_BWGT_YN"].ToString();
                ASM_AF_BWGT = reader["ASM_AF_BWGT"].ToString();
                BLDD_PPRT_ASM_YN = reader["BLDD_PPRT_ASM_YN"].ToString();
                BLDD_PPRT_ASM_CD = reader["BLDD_PPRT_ASM_CD"].ToString();
                PPRT_NUV = reader["PPRT_NUV"].ToString();
                BLUR_DCR_RT = reader["BLUR_DCR_RT"].ToString();
                MAIN_BLDVE_CH_DECS_CD = reader["MAIN_BLDVE_CH_DECS_CD"].ToString();
                USE_BLDVE_CH_CD = reader["USE_BLDVE_CH_CD"].ToString();
                HMTP_INJC_YN = reader["HMTP_INJC_YN"].ToString();
                HMTP_INJC_DT = reader["HMTP_INJC_DT"].ToString();
                ECG_ENFC_YN = reader["ECG_ENFC_YN"].ToString();
                ECG_ENFC_DT = reader["ECG_ENFC_DT"].ToString();

                // D-1. 타기관검사
                OIST_EXM_ENFC_YN = reader["OIST_EXM_ENFC_YN"].ToString();

                // D-2. 추적관리
                CHS_ENFC_YN = reader["CHS_ENFC_YN"].ToString();

                return MetroLib.SqlHelper.BREAK; // 단일행 조회이므로 BREAK
            });

            // 2. 타기관검사: TI84_ASM008_OIST
            OIST_EXM_NM.Clear();
            OIST_MDFEE_CD.Clear();
            OIST_ENFC_DD.Clear();
            OIST_EXM_RST_TXT.Clear();

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM008_OIST ";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql,  p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                OIST_EXM_NM.Add(reader["EXM_NM"].ToString());         // 검사명
                OIST_MDFEE_CD.Add(reader["MDFEE_CD"].ToString()); // 수가코드
                OIST_ENFC_DD.Add(reader["ENFC_DD"].ToString());   // 시행일자
                OIST_EXM_RST_TXT.Add(reader["EXM_RST_TXT"].ToString());   // 검사결과

                return MetroLib.SqlHelper.CONTINUE;
            });

            // 3. 추적관리: TI84_ASM008_CHS
            CHS_EXM_NM.Clear();
            CHS_MDFEE_CD.Clear();
            CHS_ENFC_DD.Clear();
            CHS_OIST_ENFC_YN.Clear();

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM008_CHS ";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            para = new List<object> { form, KEYSTR, SEQ };
            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                CHS_EXM_NM.Add(reader["EXM_NM"].ToString());               // 검사명
                CHS_MDFEE_CD.Add(reader["MDFEE_CD"].ToString());   // 수가코드
                CHS_ENFC_DD.Add(reader["ENFC_DD"].ToString());     // 시행일자
                CHS_OIST_ENFC_YN.Add(reader["OIST_ENFC_YN"].ToString());   // 타기관 시행여부

                return MetroLib.SqlHelper.CONTINUE;
            });

            // ---------------------------------------------------------------------------------------------------------------------------------
            // 진단검사결과지(ERD001)
            // ---------------------------------------------------------------------------------------------------------------------------------

            // A.기본정보
            ERD_DGSBJT_CD.Clear(); // 진료과.해당 검사를 처방한 의사의 진료과목 *** 첫번째 자료만 사용 ***
            ERD_IFLD_DTL_SPC_SBJT_CD.Clear(); // 내과 세부진료과 *** 첫번째 자료만 사용 ***
            ERD_PRSC_DR_NM.Clear(); // 처방의사 성명.해당검사를 처방한 의사의 성명 *** 첫번째 자료만 사용 ***

            // B.Text형 검사 결과
            ERD_EXM_PRSC_DT.Clear(); // 처방일시(ccyymmddhhmm)
            ERD_EXM_GAT_DT.Clear(); // 채취일시(ccyymmddhhmm)
            ERD_EXM_RCV_DT.Clear(); // 접수일시(ccyymmddhhmm)
            ERD_EXM_RST_DT.Clear(); // 결과일시(ccyymmddhhmm)
            ERD_EXM_SPCM_CD.Clear(); // 검체종류(01.혈액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
            ERD_EXM_SPCM_ETC_TXT.Clear(); // 검체종류상세(검체종류가 99인 경우 평문기재)
            ERD_EXM_MDFEE_CD.Clear(); // 수가코드(없으면 '00')
            ERD_EXM_CD.Clear(); // 검사코드.병원에서 부여한 코드
            ERD_EXM_NM.Clear(); //검사명.병원에서 부여한 명칭
            ERD_EXM_RST_TXT.Clear(); // 검사결과
            ERD_DCT_DR_NM.Clear(); // 판독의사 성명
            ERD_DCT_DR_LCS_NO.Clear(); // 판독의사 면허번호

            // C.Grid형 검사 결과(처방일시부터 검사결과까지는 Text형 검사결과와 동일하게 사용한다.)
            ERD_EXM_REF_TXT.Clear();// 참고치
            ERD_EXM_UNIT.Clear(); // 검사의 단위
            ERD_EXM_ADD_TXT.Clear(); // 추가정보(H.높음 L.낮음 정상, 이상값 등)

            // D.기타정보
            ERD_RMK_TXT.Clear(); // 비고  *** 첫번째 자료만 사용 ***

            ERD_RST_TYPE.Clear(); // 결과가 Text인지 Grid인지 구분 (T.Text G.Grid)

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM008_ERD ";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            para = new List<object> { form, KEYSTR, SEQ };
            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                // A.기본정보
                ERD_DGSBJT_CD.Add(reader["DGSBJT_CD"].ToString()); // 진료과.해당 검사를 처방한 의사의 진료과목 *** 첫번째 자료만 사용 ***
                ERD_IFLD_DTL_SPC_SBJT_CD.Add(reader["IFLD_DTL_SPC_SBJT_CD"].ToString()); // 내과 세부진료과 *** 첫번째 자료만 사용 ***
                ERD_PRSC_DR_NM.Add(reader["PRSC_DR_NM"].ToString()); // 처방의사 성명.해당검사를 처방한 의사의 성명 *** 첫번째 자료만 사용 ***

                // B.Text형 검사 결과
                ERD_EXM_PRSC_DT.Add(reader["EXM_PRSC_DT"].ToString()); // 처방일시(ccyymmddhhmm)
                ERD_EXM_GAT_DT.Add(reader["EXM_GAT_DT"].ToString()); // 채취일시(ccyymmddhhmm)
                ERD_EXM_RCV_DT.Add(reader["EXM_RCV_DT"].ToString()); // 접수일시(ccyymmddhhmm)
                ERD_EXM_RST_DT.Add(reader["EXM_RST_DT"].ToString()); // 결과일시(ccyymmddhhmm)
                ERD_EXM_SPCM_CD.Add(reader["EXM_SPCM_CD"].ToString()); // 검체종류(01.혈액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
                ERD_EXM_SPCM_ETC_TXT.Add(reader["EXM_SPCM_ETC_TXT"].ToString()); // 검체종류상세(검체종류가 99인 경우 평문기재)
                ERD_EXM_MDFEE_CD.Add(reader["EXM_MDFEE_CD"].ToString()); // 수가코드(없으면 '00')
                ERD_EXM_CD.Add(reader["EXM_CD"].ToString()); // 검사코드.병원에서 부여한 코드
                ERD_EXM_NM.Add(reader["EXM_NM"].ToString()); // 검사명.병원에서 부여한 명칭
                ERD_EXM_RST_TXT.Add(reader["EXM_RST_TXT"].ToString()); // 검사결과
                ERD_DCT_DR_NM.Add(reader["DCT_DR_NM"].ToString()); // 판독의사 성명
                ERD_DCT_DR_LCS_NO.Add(reader["DCT_DR_LCS_NO"].ToString()); // 판독의사 면허번호

                // C.Grid형 검사 결과
                ERD_EXM_REF_TXT.Add(reader["EXM_REF_TXT"].ToString()); // 참고치
                ERD_EXM_UNIT.Add(reader["EXM_UNIT"].ToString()); // 검사의 단위
                ERD_EXM_ADD_TXT.Add(reader["EXM_ADD_TXT"].ToString()); // 추가정보(H.높음 L.낮음 정상, 이상값 등)

                // D.기타정보
                ERD_RMK_TXT.Add(reader["RMK_TXT"].ToString()); // 비고 *** 첫번째 자료만 사용 ***
                ERD_RST_TYPE.Add(reader["RST_TYPE"].ToString()); // 결과가 Text인지 Grid인지 구분 (T.Text G.Grid)
                return MetroLib.SqlHelper.CONTINUE;
            });

            // ---------------------------------------------------------------------------------------------------------------------------------
            // 영상검사결과지(ERR001)
            // ---------------------------------------------------------------------------------------------------------------------------------

            // A.검사 정보 및 결과

            // A.1.검사 정보 및 결과
            ERR_DGSBJT_CD.Clear(); // 진료과(청구진료과목)
            ERR_IFLD_DTL_SPC_SBJT_CD.Clear(); // 내부 세부전문과목
            ERR_PRSC_DR_NM.Clear(); // 처방의사 성명.해당 검사를 처방한 의사의 성명
            ERR_EXM_PRSC_DT.Clear(); // 처방일시. 해당 영상검사를 처방한 날짜와 시간(ccyymmddhhmm)
            ERR_EXM_EXEC_DT.Clear(); // 검사일시. 해당 검사를 시행한 날짜외 사간(ccyymmddhhmm)
            ERR_EXM_RST_DT.Clear(); // 판독일시. 해당 영상검사에 대패 판독의가 판독결과지를 작성한 날찌와 시간(ccyymmddhhmm)
            ERR_DCT_DR_NM.Clear(); // 판독의사 성명
            ERR_DCT_DR_LCS_NO.Clear(); // 판독의사 면허번호
            ERR_EXM_MDFEE_CD.Clear(); // 수가코드(없으면 '00')
            ERR_EXM_CD.Clear(); // 검사코드.병원에서 부여한 코드
            ERR_EXM_NM.Clear(); // 검사명.병원에서 부여한 명칭
            ERR_EXM_RST_TXT.Clear(); // 판독결과.평문기재

            // A.2.비고
            ERR_RMK_TXT.Clear(); // 비고. 평문 *** 첫번째 자료만 사용 ***

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM008_ERR ";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            para = new List<object> { form, KEYSTR, SEQ };
            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                ERR_DGSBJT_CD.Add(reader["DGSBJT_CD"].ToString()); // 진료과(청구진료과목)
                ERR_IFLD_DTL_SPC_SBJT_CD.Add(reader["IFLD_DTL_SPC_SBJT_CD"].ToString()); // 내부 세부전문과목
                ERR_PRSC_DR_NM.Add(reader["PRSC_DR_NM"].ToString()); // 처방의사 성명
                ERR_EXM_PRSC_DT.Add(reader["EXM_PRSC_DT"].ToString()); // 처방일시
                ERR_EXM_EXEC_DT.Add(reader["EXM_EXEC_DT"].ToString()); // 검사일시
                ERR_EXM_RST_DT.Add(reader["EXM_RST_DT"].ToString()); // 판독일시
                ERR_DCT_DR_NM.Add(reader["DCT_DR_NM"].ToString()); // 판독의사 성명
                ERR_DCT_DR_LCS_NO.Add(reader["DCT_DR_LCS_NO"].ToString()); // 판독의사 면허번호
                ERR_EXM_MDFEE_CD.Add(reader["EXM_MDFEE_CD"].ToString()); // 수가코드
                ERR_EXM_CD.Add(reader["EXM_CD"].ToString()); // 검사코드
                ERR_EXM_NM.Add(reader["EXM_NM"].ToString()); // 검사명
                ERR_EXM_RST_TXT.Add(reader["EXM_RST_TXT"].ToString()); // 판독결과
                ERR_RMK_TXT.Add(reader["RMK_TXT"].ToString()); // 비고

                return MetroLib.SqlHelper.CONTINUE;
            });
        }

        public void ReadDataFromEMR(OleDbConnection conn, OleDbTransaction p_tran)
        {
        }

        public void InsData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg)
        {
            string sql = "";

            if (del_fg == true)
            {
                sql = "";
                sql += "DELETE FROM TI84_ASM008 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM008_OIST WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM008_CHS WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM008_ERD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM008_ERR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>(); // 파라미터 초기화

            // 1. 메인 테이블 INSERT : TI84_ASM008
            sql = "";
            sql += "INSERT INTO TI84_ASM008 (";
            sql += "FORM, KEYSTR, SEQ, VER, "; // 4
            sql += "RECU_EQP_ADM_YN, IPAT_OPAT_TP_CD, IPAT_DT, DSCG_YN, DSCG_FRM_CD, VST_STAT_CD, "; // 6
            sql += "BLDD_FST_STA_DD, BLDD_HOFC_FST_STA_DD, CHRON_RNFL_CUZ_SICK_CD, "; // 3
            sql += "HRT_CCM_SICK_YN, HRT_SICK_SYM, CDFL_YN, CDFL_CD, LVEF_CNT, CT_RT_CNT, ATFB_YN, ATFB_CD, ISMA_HRT_DS_YN, ISMA_HRT_DS_CD, OHS_YN, "; // 11
            sql += "CRBL_CCM_SICK_YN, CRBL_SICK_SYM, CRBL_HDP_YN, ASTC_REQR_YN, "; // 4
            sql += "LVCR_CCM_SICK_YN, LVCR_SICK_SYM, LVCR_SYMT_CD, REMN_LVR_FCLT_EXM_PNT, "; // 4
            sql += "HMRHG_CCM_SICK_YN, HMRHG_SICK_SYM, HMRHG_GIT_DS_CD, "; // 3
            sql += "LUNG_CCM_SICK_YN, LUNG_SICK_SYM, ARTR_BLDVE_OXY_PART_PRES, "; // 3
            sql += "TMR_CCM_SICK_YN, TMR_SICK_SYM, MNPLS_TMR_TRET_CD, "; // 3
            sql += "DBML_CCM_SICK_YN, DBML_SICK_SYM, INSL_IJCT_INJC_YN, "; // 3
            sql += "HDP_YN, HDP_TY_CD, "; // 2
            sql += "BLDD_STA_DT, HEIG, DLYS_BWGT_YN, ASM_DLYS_BWGT, "; // 4
            sql += "BF_BWGT_YN, ASM_BF_BWGT, AF_BWGT_YN, ASM_AF_BWGT, "; // 4
            sql += "BLDD_PPRT_ASM_YN, BLDD_PPRT_ASM_CD, PPRT_NUV, BLUR_DCR_RT, "; // 4
            sql += "MAIN_BLDVE_CH_DECS_CD, USE_BLDVE_CH_CD, "; // 2
            sql += "HMTP_INJC_YN, HMTP_INJC_DT, ECG_ENFC_YN, ECG_ENFC_DT, "; // 4
            sql += "OIST_EXM_ENFC_YN, CHS_ENFC_YN"; // 2
            sql += ") VALUES (";
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ?, ?, ?, ?, ?, "; // 6
            sql += "?, ?, ?, "; // 3
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, "; // 11
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ?, ? ,?, "; // 4
            sql += "?, ?, ?, "; // 3
            sql += "?, ?, ?, "; // 3
            sql += "?, ?, ?, "; // 3
            sql += "?, ?, ?, "; // 3
            sql += "?, ?, "; // 2
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ?, "; // 2
            sql += "?, ?, ?, ?, "; // 4
            sql += "?, ? "; // 2
            sql += ")";

            para.Clear();
            para.Add(form);                  // FORM
            para.Add(KEYSTR);               // KEYSTR
            para.Add(SEQ);                  // SEQ
            para.Add(ver);                  // VER

            para.Add(RECU_EQP_ADM_YN); 
            para.Add(IPAT_OPAT_TP_CD); 
            para.Add(IPAT_DT);
            para.Add(DSCG_YN); 
            para.Add(DSCG_FRM_CD); 
            para.Add(VST_STAT_CD);

            para.Add(BLDD_FST_STA_DD); 
            para.Add(BLDD_HOFC_FST_STA_DD); 
            para.Add(CHRON_RNFL_CUZ_SICK_CD);

            para.Add(HRT_CCM_SICK_YN);
            para.Add(HRT_SICK_SYM);
            para.Add(CDFL_YN); 
            para.Add(CDFL_CD); 
            para.Add(LVEF_CNT); 
            para.Add(CT_RT_CNT);
            para.Add(ATFB_YN); 
            para.Add(ATFB_CD); 
            para.Add(ISMA_HRT_DS_YN); 
            para.Add(ISMA_HRT_DS_CD); 
            para.Add(OHS_YN);

            para.Add(CRBL_CCM_SICK_YN); 
            para.Add(CRBL_SICK_SYM);
            para.Add(CRBL_HDP_YN); 
            para.Add(ASTC_REQR_YN);

            para.Add(LVCR_CCM_SICK_YN); 
            para.Add(LVCR_SICK_SYM); 
            para.Add(LVCR_SYMT_CD); 
            para.Add(REMN_LVR_FCLT_EXM_PNT);

            para.Add(HMRHG_CCM_SICK_YN); 
            para.Add(HMRHG_SICK_SYM); 
            para.Add(HMRHG_GIT_DS_CD);

            para.Add(LUNG_CCM_SICK_YN); 
            para.Add(LUNG_SICK_SYM); 
            para.Add(ARTR_BLDVE_OXY_PART_PRES);

            para.Add(TMR_CCM_SICK_YN); 
            para.Add(TMR_SICK_SYM); 
            para.Add(MNPLS_TMR_TRET_CD);

            para.Add(DBML_CCM_SICK_YN); 
            para.Add(DBML_SICK_SYM); 
            para.Add(INSL_IJCT_INJC_YN);

            para.Add(HDP_YN); 
            para.Add(HDP_TY_CD);

            para.Add(BLDD_STA_DT); 
            para.Add(HEIG); 
            para.Add(DLYS_BWGT_YN); 
            para.Add(ASM_DLYS_BWGT);

            para.Add(BF_BWGT_YN); 
            para.Add(ASM_BF_BWGT); 
            para.Add(AF_BWGT_YN); 
            para.Add(ASM_AF_BWGT);

            para.Add(BLDD_PPRT_ASM_YN); 
            para.Add(BLDD_PPRT_ASM_CD); 
            para.Add(PPRT_NUV); 
            para.Add(BLUR_DCR_RT);

            para.Add(MAIN_BLDVE_CH_DECS_CD); 
            para.Add(USE_BLDVE_CH_CD);

            para.Add(HMTP_INJC_YN); 
            para.Add(HMTP_INJC_DT); 
            para.Add(ECG_ENFC_YN); 
            para.Add(ECG_ENFC_DT);

            para.Add(OIST_EXM_ENFC_YN); 
            para.Add(CHS_ENFC_YN);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 메인 테이블 입력 실행

            // 2. 타기관 검사 입력: TI84_ASM008_OIST
            for (int i = 0; i < OIST_EXM_NM.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_OIST (FORM, KEYSTR, SEQ, SEQNO, EXM_NM, MDFEE_CD, ENFC_DD, EXM_RST_TXT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);               // 기본키 + 일련번호
                para.Add(OIST_EXM_NM[i]); 
                para.Add(OIST_MDFEE_CD[i]);                      // 검사명, 수가코드
                para.Add(OIST_ENFC_DD[i]); 
                para.Add(OIST_EXM_RST_TXT[i]);                  // 시행일자, 결과

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);                      // 실행
            }

            // 3. 추적관리 입력: TI84_ASM008_CHS
            for (int i = 0; i < CHS_EXM_NM.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_CHS (FORM, KEYSTR, SEQ, SEQNO, EXM_NM, MDFEE_CD, ENFC_DD, OIST_ENFC_YN) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);               // 기본키 + 일련번호
                para.Add(CHS_EXM_NM[i]); 
                para.Add(CHS_MDFEE_CD[i]);                        // 검사명, 수가코드
                para.Add(CHS_ENFC_DD[i]); 
                para.Add(CHS_OIST_ENFC_YN[i]);                   // 시행일자, 타기관 여부

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);                      // 실행
            }

            // 4. ERD(Text/Grid 결과) 입력: TI84_ASM008_ERD
            for (int i = 0; i < ERD_EXM_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_ERD (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, ";
                sql += "DGSBJT_CD, IFLD_DTL_SPC_SBJT_CD, PRSC_DR_NM, ";
                sql += "EXM_PRSC_DT, EXM_GAT_DT, EXM_RCV_DT, EXM_RST_DT, ";
                sql += "EXM_SPCM_CD, EXM_SPCM_ETC_TXT, ";
                sql += "EXM_MDFEE_CD, EXM_CD, EXM_NM, EXM_RST_TXT, ";
                sql += "DCT_DR_NM, DCT_DR_LCS_NO, ";
                sql += "EXM_REF_TXT, EXM_UNIT, EXM_ADD_TXT, ";
                sql += "RMK_TXT, RST_TYPE) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);

                para.Add(ERD_DGSBJT_CD[i]);
                para.Add(ERD_IFLD_DTL_SPC_SBJT_CD[i]);
                para.Add(ERD_PRSC_DR_NM[i]);

                para.Add(ERD_EXM_PRSC_DT[i]);
                para.Add(ERD_EXM_GAT_DT[i]);
                para.Add(ERD_EXM_RCV_DT[i]);
                para.Add(ERD_EXM_RST_DT[i]);

                para.Add(ERD_EXM_SPCM_CD[i]);
                para.Add(ERD_EXM_SPCM_ETC_TXT[i]);

                para.Add(ERD_EXM_MDFEE_CD[i]);
                para.Add(ERD_EXM_CD[i]);
                para.Add(ERD_EXM_NM[i]);
                para.Add(ERD_EXM_RST_TXT[i]);

                para.Add(ERD_DCT_DR_NM[i]);
                para.Add(ERD_DCT_DR_LCS_NO[i]);

                para.Add(ERD_EXM_REF_TXT[i]);
                para.Add(ERD_EXM_UNIT[i]);
                para.Add(ERD_EXM_ADD_TXT[i]);

                para.Add(ERD_RMK_TXT[i]);
                para.Add(ERD_RST_TYPE[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 5. ERR(영상검사 결과) 입력: TI84_ASM008_ERR
            for (int i = 0; i < ERR_EXM_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_ERR (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, ";
                sql += "DGSBJT_CD, IFLD_DTL_SPC_SBJT_CD, PRSC_DR_NM, ";
                sql += "EXM_PRSC_DT, EXM_EXEC_DT, EXM_RST_DT, ";
                sql += "DCT_DR_NM, DCT_DR_LCS_NO, ";
                sql += "EXM_MDFEE_CD, EXM_CD, EXM_NM, EXM_RST_TXT, ";
                sql += "RMK_TXT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);

                para.Add(ERR_DGSBJT_CD[i]);
                para.Add(ERR_IFLD_DTL_SPC_SBJT_CD[i]);
                para.Add(ERR_PRSC_DR_NM[i]);

                para.Add(ERR_EXM_PRSC_DT[i]);
                para.Add(ERR_EXM_EXEC_DT[i]);
                para.Add(ERR_EXM_RST_DT[i]);

                para.Add(ERR_DCT_DR_NM[i]);
                para.Add(ERR_DCT_DR_LCS_NO[i]);

                para.Add(ERR_EXM_MDFEE_CD[i]);
                para.Add(ERR_EXM_CD[i]);
                para.Add(ERR_EXM_NM[i]);
                para.Add(ERR_EXM_RST_TXT[i]);

                para.Add(ERR_RMK_TXT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }


        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>(); // 파라미터 초기화
            string sql = "";                        // SQL 텍스트 초기화

            // 1. 메인 테이블 UPDATE : TI84_ASM008
            sql = "";
            sql += "UPDATE TI84_ASM008 SET ";
            sql += "RECU_EQP_ADM_YN = ?, IPAT_OPAT_TP_CD = ?, IPAT_DT = ?, DSCG_YN = ?, DSCG_FRM_CD = ?, VST_STAT_CD = ?, ";
            sql += "BLDD_FST_STA_DD = ?, BLDD_HOFC_FST_STA_DD = ?, CHRON_RNFL_CUZ_SICK_CD = ?, ";
            sql += "HRT_CCM_SICK_YN = ?, HRT_SICK_SYM = ?, CDFL_YN = ?, CDFL_CD = ?, LVEF_CNT = ?, CT_RT_CNT = ?, ATFB_YN = ?, ATFB_CD = ?, ISMA_HRT_DS_YN = ?, ISMA_HRT_DS_CD = ?, OHS_YN = ?, ";
            sql += "CRBL_CCM_SICK_YN = ?, CRBL_SICK_SYM = ?, CRBL_HDP_YN = ?, ASTC_REQR_YN = ?, ";
            sql += "LVCR_CCM_SICK_YN = ?, LVCR_SICK_SYM = ?, LVCR_SYMT_CD = ?, REMN_LVR_FCLT_EXM_PNT = ?, ";
            sql += "HMRHG_CCM_SICK_YN = ?, HMRHG_SICK_SYM = ?, HMRHG_GIT_DS_CD = ?, ";
            sql += "LUNG_CCM_SICK_YN = ?, LUNG_SICK_SYM = ?, ARTR_BLDVE_OXY_PART_PRES = ?, ";
            sql += "TMR_CCM_SICK_YN = ?, TMR_SICK_SYM = ?, MNPLS_TMR_TRET_CD = ?, ";
            sql += "DBML_CCM_SICK_YN = ?, DBML_SICK_SYM = ?, INSL_IJCT_INJC_YN = ?, ";
            sql += "HDP_YN = ?, HDP_TY_CD = ?, ";
            sql += "BLDD_STA_DT = ?, HEIG = ?, DLYS_BWGT_YN = ?, ASM_DLYS_BWGT = ?, ";
            sql += "BF_BWGT_YN = ?, ASM_BF_BWGT = ?, AF_BWGT_YN = ?, ASM_AF_BWGT = ?, ";
            sql += "BLDD_PPRT_ASM_YN = ?, BLDD_PPRT_ASM_CD = ?, PPRT_NUV = ?, BLUR_DCR_RT = ?, ";
            sql += "MAIN_BLDVE_CH_DECS_CD = ?, USE_BLDVE_CH_CD = ?, ";
            sql += "HMTP_INJC_YN = ?, HMTP_INJC_DT = ?, ECG_ENFC_YN = ?, ECG_ENFC_DT = ?, ";
            sql += "OIST_EXM_ENFC_YN = ?, CHS_ENFC_YN = ? ";
            sql += "WHERE FORM = ? AND KEYSTR = ? AND SEQ = ?";

            para.Clear();

            para.Add(RECU_EQP_ADM_YN);
            para.Add(IPAT_OPAT_TP_CD);
            para.Add(IPAT_DT);
            para.Add(DSCG_YN);
            para.Add(DSCG_FRM_CD);
            para.Add(VST_STAT_CD);
            para.Add(BLDD_FST_STA_DD);
            para.Add(BLDD_HOFC_FST_STA_DD);
            para.Add(CHRON_RNFL_CUZ_SICK_CD);

            para.Add(HRT_CCM_SICK_YN);
            para.Add(HRT_SICK_SYM);
            para.Add(CDFL_YN);
            para.Add(CDFL_CD);
            para.Add(LVEF_CNT);
            para.Add(CT_RT_CNT);
            para.Add(ATFB_YN);
            para.Add(ATFB_CD);
            para.Add(ISMA_HRT_DS_YN);
            para.Add(ISMA_HRT_DS_CD);
            para.Add(OHS_YN);

            para.Add(CRBL_CCM_SICK_YN);
            para.Add(CRBL_SICK_SYM);
            para.Add(CRBL_HDP_YN);
            para.Add(ASTC_REQR_YN);

            para.Add(LVCR_CCM_SICK_YN);
            para.Add(LVCR_SICK_SYM);
            para.Add(LVCR_SYMT_CD);
            para.Add(REMN_LVR_FCLT_EXM_PNT);

            para.Add(HMRHG_CCM_SICK_YN);
            para.Add(HMRHG_SICK_SYM);
            para.Add(HMRHG_GIT_DS_CD);

            para.Add(LUNG_CCM_SICK_YN);
            para.Add(LUNG_SICK_SYM);
            para.Add(ARTR_BLDVE_OXY_PART_PRES);

            para.Add(TMR_CCM_SICK_YN);
            para.Add(TMR_SICK_SYM);
            para.Add(MNPLS_TMR_TRET_CD);

            para.Add(DBML_CCM_SICK_YN);
            para.Add(DBML_SICK_SYM);
            para.Add(INSL_IJCT_INJC_YN);

            para.Add(HDP_YN);
            para.Add(HDP_TY_CD);

            para.Add(BLDD_STA_DT);
            para.Add(HEIG);
            para.Add(DLYS_BWGT_YN);
            para.Add(ASM_DLYS_BWGT);
            para.Add(BF_BWGT_YN);
            para.Add(ASM_BF_BWGT);
            para.Add(AF_BWGT_YN);
            para.Add(ASM_AF_BWGT);

            para.Add(BLDD_PPRT_ASM_YN);
            para.Add(BLDD_PPRT_ASM_CD);
            para.Add(PPRT_NUV);
            para.Add(BLUR_DCR_RT);

            para.Add(MAIN_BLDVE_CH_DECS_CD);
            para.Add(USE_BLDVE_CH_CD);

            para.Add(HMTP_INJC_YN);
            para.Add(HMTP_INJC_DT);
            para.Add(ECG_ENFC_YN);
            para.Add(ECG_ENFC_DT);

            para.Add(OIST_EXM_ENFC_YN);
            para.Add(CHS_ENFC_YN);

            para.Add(form);     // WHERE 조건: FORM
            para.Add(KEYSTR);   // WHERE 조건: KEYSTR
            para.Add(SEQ);      // WHERE 조건: SEQ

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 메인 UPDATE 실행

            // 2. 반복 데이터 삭제 - 기존 타기관검사 데이터 지우기
            sql = "DELETE FROM TI84_ASM008_OIST WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran); // 실행

            // 4. 타기관 검사 입력 재삽입
            for (int i = 0; i < OIST_EXM_NM.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_OIST (FORM, KEYSTR, SEQ, SEQNO, EXM_NM, MDFEE_CD, ENFC_DD, EXM_RST_TXT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1); // 기본 키 및 순번
                para.Add(OIST_EXM_NM[i]); // 검사명
                para.Add(OIST_MDFEE_CD[i]); // 수가코드
                para.Add(OIST_ENFC_DD[i]); // 시행일자
                para.Add(OIST_EXM_RST_TXT[i]); // 결과

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 반복 INSERT
            }

            // 3. 반복 데이터 삭제 - 기존 추적관리 데이터 지우기
            sql = "DELETE FROM TI84_ASM008_CHS WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran); // 실행

            // 5. 추적관리 검사 입력 재삽입
            for (int i = 0; i < CHS_EXM_NM.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_CHS (FORM, KEYSTR, SEQ, SEQNO, EXM_NM, MDFEE_CD, ENFC_DD, OIST_ENFC_YN) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1); // 기본 키 및 순번
                para.Add(CHS_EXM_NM[i]); // 검사명
                para.Add(CHS_MDFEE_CD[i]); // 수가코드
                para.Add(CHS_ENFC_DD[i]); // 시행일자
                para.Add(CHS_OIST_ENFC_YN[i]); // 타기관 여부

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 반복 INSERT
            }

            // 6. 반복 데이터 삭제 - 기존 ERD 데이터 지우기
            sql = "DELETE FROM TI84_ASM008_ERD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            // 7. ERD 입력 재삽입
            for (int i = 0; i < ERD_EXM_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_ERD (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, ";
                sql += "DGSBJT_CD, IFLD_DTL_SPC_SBJT_CD, PRSC_DR_NM, ";
                sql += "EXM_PRSC_DT, EXM_GAT_DT, EXM_RCV_DT, EXM_RST_DT, ";
                sql += "EXM_SPCM_CD, EXM_SPCM_ETC_TXT, ";
                sql += "EXM_MDFEE_CD, EXM_CD, EXM_NM, EXM_RST_TXT, ";
                sql += "DCT_DR_NM, DCT_DR_LCS_NO, ";
                sql += "EXM_REF_TXT, EXM_UNIT, EXM_ADD_TXT, ";
                sql += "RMK_TXT, RST_TYPE) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); para.Add(KEYSTR); para.Add(SEQ); para.Add(i + 1);

                para.Add(ERD_DGSBJT_CD[i]);
                para.Add(ERD_IFLD_DTL_SPC_SBJT_CD[i]);
                para.Add(ERD_PRSC_DR_NM[i]);

                para.Add(ERD_EXM_PRSC_DT[i]);
                para.Add(ERD_EXM_GAT_DT[i]);
                para.Add(ERD_EXM_RCV_DT[i]);
                para.Add(ERD_EXM_RST_DT[i]);

                para.Add(ERD_EXM_SPCM_CD[i]);
                para.Add(ERD_EXM_SPCM_ETC_TXT[i]);

                para.Add(ERD_EXM_MDFEE_CD[i]);
                para.Add(ERD_EXM_CD[i]);
                para.Add(ERD_EXM_NM[i]);
                para.Add(ERD_EXM_RST_TXT[i]);

                para.Add(ERD_DCT_DR_NM[i]);
                para.Add(ERD_DCT_DR_LCS_NO[i]);

                para.Add(ERD_EXM_REF_TXT[i]);
                para.Add(ERD_EXM_UNIT[i]);
                para.Add(ERD_EXM_ADD_TXT[i]);

                para.Add(ERD_RMK_TXT[i]);
                para.Add(ERD_RST_TYPE[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 8. 반복 데이터 삭제 - 기존 ERR 데이터 지우기
            sql = "DELETE FROM TI84_ASM008_ERR WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            // 9. ERR 입력 재삽입
            for (int i = 0; i < ERR_EXM_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM008_ERR (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, ";
                sql += "DGSBJT_CD, IFLD_DTL_SPC_SBJT_CD, PRSC_DR_NM, ";
                sql += "EXM_PRSC_DT, EXM_EXEC_DT, EXM_RST_DT, ";
                sql += "DCT_DR_NM, DCT_DR_LCS_NO, ";
                sql += "EXM_MDFEE_CD, EXM_CD, EXM_NM, EXM_RST_TXT, ";
                sql += "RMK_TXT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); para.Add(KEYSTR); para.Add(SEQ); para.Add(i + 1);

                para.Add(ERR_DGSBJT_CD[i]);
                para.Add(ERR_IFLD_DTL_SPC_SBJT_CD[i]);
                para.Add(ERR_PRSC_DR_NM[i]);

                para.Add(ERR_EXM_PRSC_DT[i]);
                para.Add(ERR_EXM_EXEC_DT[i]);
                para.Add(ERR_EXM_RST_DT[i]);

                para.Add(ERR_DCT_DR_NM[i]);
                para.Add(ERR_DCT_DR_LCS_NO[i]);

                para.Add(ERR_EXM_MDFEE_CD[i]);
                para.Add(ERR_EXM_CD[i]);
                para.Add(ERR_EXM_NM[i]);
                para.Add(ERR_EXM_RST_TXT[i]);

                para.Add(ERR_RMK_TXT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM008 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM008_OIST WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM008_CHS WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM008_ERD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM008_ERR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }

    }
}

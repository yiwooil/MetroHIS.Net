using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CMakeASM008
    {
        public void MakeASM008(CDataASM008_002 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
        {
            int count = data.Read_ASM000(p_conn, p_tran, re_query);

            if (count > 0 && (data.UPDDT != "" || data.STATUS != ""))
            {
                // 자료를 사용자가 저장했거나, 전송한 이력이 있으면 자료를 다시 읽지 않고 만들어진 자료를 조회한다.
                data.ReadDataFromSaved(p_conn, p_tran);
            }
            else
            {
                // EMR 에서 자료를 읽는다.
                SetData(data, p_sysdt, p_systm, p_user, p_conn, p_tran);


                // TI84_ASM000 저장
                data.Into_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

                // 자료저장
                data.InsData(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

            }
        }

        private void SetData(CDataASM008_002 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            data.ClearMe();

            // A. 기본정보
            data.RECU_EQP_ADM_YN = "2";           // 요양병원 여부(1.Yes 2.No)
            data.IPAT_OPAT_TP_CD = data.IOFG == "2" ? "1" : "2";           // 진료형태(1.입원 2.외래)
            data.IPAT_DT = data.A04_BEDEDT + data.A04_BEDEHM;                   // 입원일시(CCYYMMDDHHMM)
            if (data.IOFG == "2") data.DSCG_YN = data.A04_BEDODT != "" ? "1" : "2";                   // 퇴원여부(1.Yes 2.No)
            data.DSCG_FRM_CD = "";               // 퇴원형태 코드(01.정상퇴원 02.자의퇴원 03.탈원 04.가망없는 퇴원 05.전원 06.사망 99.기타)
            if (data.IOFG == "2")
            {
                if (data.A04_BEDODIV == "1") data.DSCG_FRM_CD = "01";
                else if (data.A04_BEDODIV == "2") data.DSCG_FRM_CD = "01";
                else if (data.A04_BEDODIV == "3") data.DSCG_FRM_CD = "01";
                else if (data.A04_BEDODIV == "4") data.DSCG_FRM_CD = "03";
            }
            data.VST_STAT_CD = "";               // 내원상태 코드(01.계속 외래 내원 02.귀 원에 입원 03.타 원에 입원 04.타 기관으로 전원 05.사망 99.기타)

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT MIN(HEMDT) FSTDT";
            sql += Environment.NewLine + "  FROM TU12A (NOLOCK)";
            sql += Environment.NewLine + " WHERE HEMDT>='" + data.FR_DATE + "'";
            sql += Environment.NewLine + "   AND HEMDT<='" + data.TO_DATE + "'";
            sql += Environment.NewLine + "   AND GUBUN='OPD'";
            sql += Environment.NewLine + "   AND DONFG='Y'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                data.BLDD_FST_STA_DD = row["FSTDT"].ToString(); // 최초 혈액투석 시작일
                return MetroLib.SqlHelper.BREAK;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT MIN(HEMDT) FSTDT";
            sql += Environment.NewLine + "  FROM TU12A (NOLOCK)";
            sql += Environment.NewLine + " WHERE GUBUN='OPD'";
            sql += Environment.NewLine + "   AND DONFG='Y'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                data.BLDD_HOFC_FST_STA_DD = row["FSTDT"].ToString(); // 본원 최초 시작일
                return MetroLib.SqlHelper.BREAK;
            });

            // B. 환자 정보
            data.CHRON_RNFL_CUZ_SICK_CD = "";    // 만성신부전 원인 상병코드(1.당뇨병 2.고혈압 3.사구체신염 4.모름 9.기타)

            data.HRT_CCM_SICK_YN = "";           // 심장질환 여부(1.Yes 2.No)
            data.HRT_SICK_SYM = "";              // 상병분류기호
            data.CDFL_YN = "";                   // 심부전 여부(1.Yes 2.No)
            data.CDFL_CD = "";                   // 심부전 상세 코드(01.해당없음 02.좌심실 구혈율 03.심흉곽비)
            data.LVEF_CNT = "";                  // 좌심실 구혈율
            data.CT_RT_CNT = "";                 // 심흉곽비
            data.ATFB_YN = "";                   // 심방조동 여부(1.Yes 2.No)
            data.ATFB_CD = "";                   // 심방 세동 상세
            data.ISMA_HRT_DS_YN = "";            // 허혈성 심장병 여부(1.Yes 2.No)
            data.ISMA_HRT_DS_CD = "";            // 허혈성 심장병 상세
            data.OHS_YN = "";                    // 개심수술 여부

            data.CRBL_CCM_SICK_YN = "";          // 뇌혈관질환 여부
            data.CRBL_SICK_SYM = "";             // 뇌혈관 상병코드
            data.CRBL_HDP_YN = "";               // 장애발생 여부
            data.ASTC_REQR_YN = "";              // 타인의 도움 필요 여부

            data.LVCR_CCM_SICK_YN = "";          // 간경변증 여부
            data.LVCR_SICK_SYM = "";             // 간경변증 상병
            data.LVCR_SYMT_CD = "";              // 간경변 상세 코드
            data.REMN_LVR_FCLT_EXM_PNT = "";     // Child-Pugh score

            data.HMRHG_CCM_SICK_YN = "";         // 출혈성 위장관 질환 여부
            data.HMRHG_SICK_SYM = "";            // 위장관 질환 상병코드
            data.HMRHG_GIT_DS_CD = "";           // 위장관 질환 상세 코드

            data.LUNG_CCM_SICK_YN = "";          // 만성폐질환
            data.LUNG_SICK_SYM = "";             // 폐질환 상병
            data.ARTR_BLDVE_OXY_PART_PRES = "";  // 산소분압

            data.TMR_CCM_SICK_YN = "";           // 악성종양
            data.TMR_SICK_SYM = "";              // 종양 상병코드
            data.MNPLS_TMR_TRET_CD = "";         // 종양 상세

            data.DBML_CCM_SICK_YN = "";          // 당뇨병 여부
            data.DBML_SICK_SYM = "";             // 당뇨 상병
            data.INSL_IJCT_INJC_YN = "";         // 인슐린 주사 여부

            data.HDP_YN = "";                    // 장애인 여부(3급 이상)
            data.HDP_TY_CD = "";                 // 장애유형 코드 (다중 선택)


            // C. 투석 정보
            data.BLDD_STA_DT = data.STEDT;       // 투석 일시

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TU67 (NOLOCK)";
            sql += Environment.NewLine + " WHERE PID='" + data.PID + "'";
            sql += Environment.NewLine + "   AND CHKDT='" + data.STEDT + "'";
            sql += Environment.NewLine + "   AND ISNULL(CHGDT,'')=''";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                data.HEIG = "";                      // 신장
                data.ASM_DLYS_BWGT = MetroLib.StrHelper.Round(row["LASTWT"].ToString(), 1).ToString();            // 건체중 값
                data.DLYS_BWGT_YN = data.ASM_DLYS_BWGT != "" ? "1" : "2"; // 건체중 측정 여부(1.Yew 2.No)
                data.ASM_BF_BWGT = MetroLib.StrHelper.Round(row["HMBECURWT"].ToString(), 1).ToString();           // 투석 전 체중
                data.BF_BWGT_YN = data.ASM_BF_BWGT != "" ? "1" : "2";     // 투석 전 체중 측정 여부(1.Yew 2.No)
                data.ASM_AF_BWGT = MetroLib.StrHelper.Round(row["HMAFCURWT"].ToString(), 1).ToString();           // 투석 후 체중
                data.AF_BWGT_YN = data.ASM_AF_BWGT != "" ? "1" : "2";     // 투석 후 체중 측정 여부(1.Yew 2.No)

                return MetroLib.SqlHelper.BREAK;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TU68 (NOLOCK)";
            sql += Environment.NewLine + " WHERE PID='" + data.PID + "'";
            sql += Environment.NewLine + "   AND WDATE='" + data.STEDT + "'";
            sql += Environment.NewLine + "   AND ISNULL(CHGDT,'')=''";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                data.BLDD_PPRT_ASM_YN = "";          // 혈액투석 적절도 평가 여부
                data.BLDD_PPRT_ASM_CD = "";          // 적절도 평가 항목 코드
                data.PPRT_NUV = MetroLib.StrHelper.Round(row["SPKT"].ToString(), 3).ToString();                 // spKt/V 수치
                data.BLUR_DCR_RT = MetroLib.StrHelper.Round(row["URR"].ToString(), 1).ToString();               // URR 수치(%)

                return MetroLib.SqlHelper.BREAK;
            });

            data.MAIN_BLDVE_CH_DECS_CD = "";     // 월단위 혈관통로
            data.USE_BLDVE_CH_CD = "";           // 해당일 혈관통로

            data.HMTP_INJC_YN = "";              // 조혈제 투여 여부
            data.HMTP_INJC_DT = "";              // 조혈제 투여 일시

            data.ECG_ENFC_YN = "";               // EKG 시행 여부
            data.ECG_ENFC_DT = "";               // EKG 시행일

            // D-1. 타기관검사
            data.OIST_EXM_ENFC_YN = "";          // 타기관검사 여부
            data.OIST_EXM_NM.Clear();            // 검사명 리스트 초기화
            data.OIST_MDFEE_CD.Clear();          // 수가코드 리스트 초기화
            data.OIST_ENFC_DD.Clear();           // 시행일자 리스트 초기화
            data.OIST_EXM_RST_TXT.Clear();       // 결과값 리스트 초기화

            // D-2. 추적관리
            data.CHS_ENFC_YN = "";               // 추적관리 시행 여부
            data.CHS_EXM_NM.Clear();             // 검사명 리스트 초기화
            data.CHS_MDFEE_CD.Clear();           // 수가코드 리스트 초기화
            data.CHS_ENFC_DD.Clear();            // 시행일자 리스트 초기화
            data.CHS_OIST_ENFC_YN.Clear();       // 타기관 시행 여부 리스트 초기화

        }
    }
}

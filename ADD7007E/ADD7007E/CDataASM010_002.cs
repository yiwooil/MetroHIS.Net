using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM010_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM010"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "SIP"; // 업무상세코드

        // A. 기본정보
        public string ASM_IPAT_DT { get; set; } // 입원일시(YYYYMMDDHHMM)
        public string DSCG_YN; // 퇴원여부(1.Yes 2.No)
        public string ASM_DSCG_DT { get; set; } // 퇴원일시(YYYYMMDDHHMM)


        // B.수술 및 감염 정보
        // 1. 수술 관련 환자 상태
        public string MDFEE_CD { get; set; } // 수가코드
        public string ASM_SOPR_STA_DT { get; set; } // 수술 시작일시(YYYYMMDDHHMM)
        public string ASM_SOPR_END_DT { get; set; } // 수술 종료일시(YYYYMMDDHHMM)
        public string EMY_CD; // 응급여부(1.정규 2.응급)
        public string KNJN_RPMT; // 슬관절치환술(1.Yes 2.No)
        public string HMRHG_CTRL_YN; // 토니켓 적용 여부(1.Yes 2.No)
        public string HMRHG_CTRL_DT; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
        public string CAESR_YN; // 제왕절개술 시행 여부(1.Yes 2.No)
        public string NBY_PARTU_DT; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
        public string CRVD_YN; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
        public string BSE_NCT_YN; // 기본마취 여부(1.Yes 2.No)
        public string ASA_PNT; // ASA 점수
        // 2.수술 전 항생제 투여
        public string SOPR_BF_ANBO_INJC_YN; // 수술 전 항생제 투여 여부(1.Yes 2.No)
        public string SOPR_BF_INFC_SICK_YN; // 감염상병 확진 여부(1.Yes 2.No)
        public string SOPR_BF_INFC_SICK_CD; // 감염상병 확진명
        public string SOPR_BF_DDIAG_YN; // 감염내과 협진여부(1.Yes 2.No)
        public string SOPR_BF_ASM_REQ_DT; // 의뢰일시(YYYYMMDDHHMM)
        public string SOPR_BF_RPY_YN; // 회신 여부(1.Yes 2.No)
        public string SOPR_BF_ASM_RPY_DT; // 회신일시(YYYYMMDDHHMM)
        public string SOPR_BF_ANBO_DR_RCD_YN; // 항생제 필요 의사기록 여부(1.Yes 2.No)
        public string SOPR_BF_ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
        public string SOPR_BF_ANBO_DR_RCDC_CD; // 기록지 종류(YYYYMMDDHHMM)
        public string SOPR_BF_REQR_RS_CD; // 필요사유
        public string SOPR_BF_DR_RCD_TXT; // 기록 상세 내용(평문)
        // 3.평가 제외 수술
        public string ASM_TGT_SOPR_SAME_ENFC_YN; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
        public string FQ2_GT_SOPR_ENFC_YN; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
        // 4.수술 후 항셍제 투여
        public string SOPR_RGN_INFC_ANBO_INJC_YN; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
        public string SOPR_RGN_INFC_CD; // 수술부위 감영 유형
        public string ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
        public string SOPR_RGN_INFC_DR_RCDC_CD; // 기록지 종류
        public string SOPR_RGN_INFC_DR_RCD_TXT; // 수술 부위 감염 사유 상세(평문)
        public string INFC_ANBO_INJC_YN; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
        public string CLTR_STRN_YN; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
        public string ASM_GAT_DT; // 채취일시(YYYYMMDDHHMM)
        public string INFC_SICK_DIAG; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
        public string SOPR_AF_INFC_SICK_CD; // 감염 상병 화진명
        public string SOPR_AF_DDIAG_YN; // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
        public string SOPR_AF_ASM_REQ_DT; // 의뢰일시(YYYYMMDDHHMM)
        public string SOPR_AF_RPY_YN; // 회신 여부(1.Yes 2.No)
        public string SOPR_AF_ASM_RPY_DT; // 회신일시(YYYYMMDDHHMM)
        public string SOPR_AF_ANBO_DR_RCD_YN; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
        public string SOPR_AF_ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
        public string SOPR_AF_ANBO_DR_RCDC_CD; // 기록지 종류
        public string SOPR_AF_REQR_RS_CD; // 필요사유
        public string SOPR_AF_DR_RCD_TXT; // 기록 상세 내용(평문)
        public string ANBO_ALRG_YN; // 항생제 알러지 여부(1.Yes 2.No)
        public string WHBL_RBC_BLTS_YN; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)

        // 수혈
        public List<string> BLTS_STA_DT = new List<string>(); // 수혈시작일시
        public List<string> BLTS_END_DT = new List<string>(); // 수혈종료일시
        public List<string> BLTS_MDFEE_CD = new List<string>(); // 수가코드
        public List<string> BLTS_DGM_NM = new List<string>(); // 수혈제제명

        // C.항생제 투여 여부
        public string ANBO_USE_YN; // 항생제 투여 여부(1.Yes 2.No)

        // 항생제 투여
        public List<string> INJC_STA_DT = new List<string>(); // 투여시작일시
        public List<string> INJC_END_DT = new List<string>(); // 투여종료일시
        public List<string> INJC_MDS_CD = new List<string>(); // 약품코드
        public List<string> INJC_MDS_NM = new List<string>(); // 약품명
        public List<string> ANBO_INJC_PTH_CD = new List<string>(); // 투여경로

        // 퇴원 시 항생제 처방
        public string DSCG_ANBO_PRSC_YN; // 퇴원시 항생제 처방 여부(1.Yes 2.No)

        public List<string> PRSC_MDS_CD = new List<string>(); // 약품코드
        public List<string> PRSC_MDS_NM = new List<string>(); // 약품명
        public List<string> PRSC_TOT_INJC_DDCNT = new List<string>(); // 총 투약일수


        // 이하 화면 조회용 ----------------------------------------------------
        public string INJC_STA_DT_DTM // 항생제 투여시작일자
        {
            get
            {
                string ret = "";
                for (int i = 0 ; i<INJC_STA_DT.Count ; i++)
                {
                    if (ret == "")
                    {
                        ret = INJC_STA_DT[i];
                    }
                    else if (INJC_STA_DT[i].CompareTo(ret) < 0)
                    {
                        ret = INJC_STA_DT[i];
                    }
                }
                return ret;
            }
        }
        public string INJC_END_DT_DTM // 항생제 투여종료일자
        {
            get
            {
                string ret = "";
                for (int i = 0; i < INJC_END_DT.Count; i++)
                {
                    if (ret == "")
                    {
                        ret = INJC_END_DT[i];
                    }
                    else if (INJC_END_DT[i].CompareTo(ret) > 0)
                    {
                        ret = INJC_END_DT[i];
                    }
                }
                return ret;

            }
        }

        // D. 기타 사항
        //public string APND_DATA_NO; // 첨부

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            MDFEE_CD = ""; // 수가코드
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본정보
            ASM_IPAT_DT = ""; // 입원일시(YYYYMMDDHHMM)
            DSCG_YN = "2"; // 퇴원여부(1.Yes 2.No)
            ASM_DSCG_DT = ""; // 퇴원일시(YYYYMMDDHHMM)


            // B.수술 및 감염 정보
            // 1. 수술 관련 환자 상태
            //MDFEE_CD = ""; // 수가코드 Clear함수에서 지운다.***
            ASM_SOPR_STA_DT = ""; // 수술 시작일시(YYYYMMDDHHMM)
            ASM_SOPR_END_DT = ""; // 수술 종료일시(YYYYMMDDHHMM)
            EMY_CD = "1"; // 응급여부(1.정규 2.응급)
            KNJN_RPMT = "2"; // 슬관절치환술(1.Yes 2.No)
            HMRHG_CTRL_YN = "2"; // 토니켓 적용 여부(1.Yes 2.No)
            HMRHG_CTRL_DT = ""; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            CAESR_YN = "2"; // 제왕절개술 시행 여부(1.Yes 2.No)
            NBY_PARTU_DT = ""; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
            CRVD_YN = "2"; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            BSE_NCT_YN = "2"; // 기본마취 여부(1.Yes 2.No)
            ASA_PNT = ""; // ASA 점수
            // 2.수술 전 항생제 투여
            SOPR_BF_ANBO_INJC_YN = "2"; // 수술 전 항생제 투여 여부(1.Yes 2.No)
            SOPR_BF_INFC_SICK_YN = "2"; // 감염상병 확진 여부(1.Yes 2.No)
            SOPR_BF_INFC_SICK_CD = ""; // 감염상병 확진명
            SOPR_BF_DDIAG_YN = "2"; // 감염내과 협진여부(1.Yes 2.No)
            SOPR_BF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
            SOPR_BF_RPY_YN = "2"; // 회신 여부(1.Yes 2.No)
            SOPR_BF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
            SOPR_BF_ANBO_DR_RCD_YN = "2"; // 항생제 필요 의사기록 여부(1.Yes 2.No)
            SOPR_BF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            SOPR_BF_ANBO_DR_RCDC_CD = ""; // 기록지 종류(YYYYMMDDHHMM)
            SOPR_BF_REQR_RS_CD = ""; // 필요사유
            SOPR_BF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
            // 3.평가 제외 수술
            ASM_TGT_SOPR_SAME_ENFC_YN = "2"; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
            FQ2_GT_SOPR_ENFC_YN = "2"; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
            // 4.수술 후 항셍제 투여
            SOPR_RGN_INFC_ANBO_INJC_YN = "2"; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
            SOPR_RGN_INFC_CD = ""; // 수술부위 감영 유형
            ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            SOPR_RGN_INFC_DR_RCDC_CD = ""; // 기록지 종류
            SOPR_RGN_INFC_DR_RCD_TXT = ""; // 수술 부위 감염 사유 상세(평문)
            INFC_ANBO_INJC_YN = "2"; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
            CLTR_STRN_YN = "2"; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
            ASM_GAT_DT = ""; // 채취일시(YYYYMMDDHHMM)
            INFC_SICK_DIAG = "2"; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
            SOPR_AF_INFC_SICK_CD = ""; // 감염 상병 화진명
            SOPR_AF_DDIAG_YN = "2"; // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
            SOPR_AF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
            SOPR_AF_RPY_YN = "2"; // 회신 여부(1.Yes 2.No)
            SOPR_AF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
            SOPR_AF_ANBO_DR_RCD_YN = "2"; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
            SOPR_AF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            SOPR_AF_ANBO_DR_RCDC_CD = ""; // 기록지 종류
            SOPR_AF_REQR_RS_CD = ""; // 필요사유
            SOPR_AF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
            ANBO_ALRG_YN = "2"; // 항생제 알러지 여부(1.Yes 2.No)
            WHBL_RBC_BLTS_YN = "2"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)

            // 수혈
            BLTS_STA_DT.Clear(); // 수혈시작일시
            BLTS_END_DT.Clear(); // 수혈종료일시
            BLTS_MDFEE_CD.Clear(); // 수가코드
            BLTS_DGM_NM.Clear(); // 수혈제제명

            // C.항생제 투여 여부
            ANBO_USE_YN = "2"; // 항생제 투여 여부(1.Yes 2.No)

            // 항생제 투여
            INJC_STA_DT.Clear(); // 투여시작일시
            INJC_END_DT.Clear(); // 투여종료일시
            INJC_MDS_CD.Clear(); // 약품코드
            INJC_MDS_NM.Clear(); // 약품명
            ANBO_INJC_PTH_CD.Clear(); // 투여경로

            // 퇴원 시 항생제 처방
            DSCG_ANBO_PRSC_YN = "2"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)

            PRSC_MDS_CD.Clear(); // 약품코드
            PRSC_MDS_NM.Clear(); // 약품명
            PRSC_TOT_INJC_DDCNT.Clear(); // 총 투약일수

            //APND_DATA_NO = ""; // 첨부
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";

            // A. 기본정보, B.수술 및 감염 정보, C.항생제 투여 여부 등 단일 Row 데이터
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM010";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_IPAT_DT = reader["ASM_IPAT_DT"].ToString(); // 입원일시(YYYYMMDDHHMM)
                DSCG_YN = reader["DSCG_YN"].ToString(); // 퇴원여부(1.Yes 2.No)
                ASM_DSCG_DT = reader["ASM_DSCG_DT"].ToString(); // 퇴원일시(YYYYMMDDHHMM)

                // 1. 수술 관련 환자 상태
                MDFEE_CD = reader["MDFEE_CD"].ToString(); // 수가코드
                ASM_SOPR_STA_DT = reader["ASM_SOPR_STA_DT"].ToString(); // 수술 시작일시(YYYYMMDDHHMM)
                ASM_SOPR_END_DT = reader["ASM_SOPR_END_DT"].ToString(); // 수술 종료일시(YYYYMMDDHHMM)
                EMY_CD = reader["EMY_CD"].ToString(); // 응급여부(1.정규 2.응급)
                KNJN_RPMT = reader["KNJN_RPMT"].ToString(); // 슬관절치환술(1.Yes 2.No)
                HMRHG_CTRL_YN = reader["HMRHG_CTRL_YN"].ToString(); // 토니켓 적용 여부(1.Yes 2.No)
                HMRHG_CTRL_DT = reader["HMRHG_CTRL_DT"].ToString(); // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
                CAESR_YN = reader["CAESR_YN"].ToString(); // 제왕절개술 시행 여부(1.Yes 2.No)
                NBY_PARTU_DT = reader["NBY_PARTU_DT"].ToString(); // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
                CRVD_YN = reader["CRVD_YN"].ToString(); // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
                BSE_NCT_YN = reader["BSE_NCT_YN"].ToString(); // 기본마취 여부(1.Yes 2.No)
                ASA_PNT = reader["ASA_PNT"].ToString(); // ASA 점수

                // 2. 수술 전 항생제 투여
                SOPR_BF_ANBO_INJC_YN = reader["SOPR_BF_ANBO_INJC_YN"].ToString(); // 수술 전 항생제 투여 여부(1.Yes 2.No)
                SOPR_BF_INFC_SICK_YN = reader["SOPR_BF_INFC_SICK_YN"].ToString(); // 감염상병 확진 여부(1.Yes 2.No)
                SOPR_BF_INFC_SICK_CD = reader["SOPR_BF_INFC_SICK_CD"].ToString(); // 감염상병 확진명
                SOPR_BF_DDIAG_YN = reader["SOPR_BF_DDIAG_YN"].ToString(); // 감염내과 협진여부(1.Yes 2.No)
                SOPR_BF_ASM_REQ_DT = reader["SOPR_BF_ASM_REQ_DT"].ToString(); // 의뢰일시(YYYYMMDDHHMM)
                SOPR_BF_RPY_YN = reader["SOPR_BF_RPY_YN"].ToString(); // 회신 여부(1.Yes 2.No)
                SOPR_BF_ASM_RPY_DT = reader["SOPR_BF_ASM_RPY_DT"].ToString(); // 회신일시(YYYYMMDDHHMM)
                SOPR_BF_ANBO_DR_RCD_YN = reader["SOPR_BF_ANBO_DR_RCD_YN"].ToString(); // 항생제 필요 의사기록 여부(1.Yes 2.No)
                SOPR_BF_ASM_RCD_DT = reader["SOPR_BF_ASM_RCD_DT"].ToString(); // 기록일시(YYYYMMDDHHMM)
                SOPR_BF_ANBO_DR_RCDC_CD = reader["SOPR_BF_ANBO_DR_RCDC_CD"].ToString(); // 기록지 종류(YYYYMMDDHHMM)
                SOPR_BF_REQR_RS_CD = reader["SOPR_BF_REQR_RS_CD"].ToString(); // 필요사유
                SOPR_BF_DR_RCD_TXT = reader["SOPR_BF_DR_RCD_TXT"].ToString(); // 기록 상세 내용(평문)

                // 3. 평가 제외 수술
                ASM_TGT_SOPR_SAME_ENFC_YN = reader["ASM_TGT_SOPR_SAME_ENFC_YN"].ToString(); // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
                FQ2_GT_SOPR_ENFC_YN = reader["FQ2_GT_SOPR_ENFC_YN"].ToString(); // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)

                // 4. 수술 후 항셍제 투여
                SOPR_RGN_INFC_ANBO_INJC_YN = reader["SOPR_RGN_INFC_ANBO_INJC_YN"].ToString(); // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
                SOPR_RGN_INFC_CD = reader["SOPR_RGN_INFC_CD"].ToString(); // 수술부위 감영 유형
                ASM_RCD_DT = reader["ASM_RCD_DT"].ToString(); // 기록일시(YYYYMMDDHHMM)
                SOPR_RGN_INFC_DR_RCDC_CD = reader["SOPR_RGN_INFC_DR_RCDC_CD"].ToString(); // 기록지 종류
                SOPR_RGN_INFC_DR_RCD_TXT = reader["SOPR_RGN_INFC_DR_RCD_TXT"].ToString(); // 수술 부위 감염 사유 상세(평문)
                INFC_ANBO_INJC_YN = reader["INFC_ANBO_INJC_YN"].ToString(); // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
                CLTR_STRN_YN = reader["CLTR_STRN_YN"].ToString(); // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
                ASM_GAT_DT = reader["ASM_GAT_DT"].ToString(); // 채취일시(YYYYMMDDHHMM)
                INFC_SICK_DIAG = reader["INFC_SICK_DIAG"].ToString(); // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_INFC_SICK_CD = reader["SOPR_AF_INFC_SICK_CD"].ToString(); // 감염 상병 화진명
                SOPR_AF_DDIAG_YN = reader["SOPR_AF_DDIAG_YN"].ToString(); // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_ASM_REQ_DT = reader["SOPR_AF_ASM_REQ_DT"].ToString(); // 의뢰일시(YYYYMMDDHHMM)
                SOPR_AF_RPY_YN = reader["SOPR_AF_RPY_YN"].ToString(); // 회신 여부(1.Yes 2.No)
                SOPR_AF_ASM_RPY_DT = reader["SOPR_AF_ASM_RPY_DT"].ToString(); // 회신일시(YYYYMMDDHHMM)
                SOPR_AF_ANBO_DR_RCD_YN = reader["SOPR_AF_ANBO_DR_RCD_YN"].ToString(); // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_ASM_RCD_DT = reader["SOPR_AF_ASM_RCD_DT"].ToString(); // 기록일시(YYYYMMDDHHMM)
                SOPR_AF_ANBO_DR_RCDC_CD = reader["SOPR_AF_ANBO_DR_RCDC_CD"].ToString(); // 기록지 종류
                SOPR_AF_REQR_RS_CD = reader["SOPR_AF_REQR_RS_CD"].ToString(); // 필요사유
                SOPR_AF_DR_RCD_TXT = reader["SOPR_AF_DR_RCD_TXT"].ToString(); // 기록 상세 내용(평문)
                ANBO_ALRG_YN = reader["ANBO_ALRG_YN"].ToString(); // 항생제 알러지 여부(1.Yes 2.No)
                WHBL_RBC_BLTS_YN = reader["WHBL_RBC_BLTS_YN"].ToString(); // 수술시작 후 전혈 및 적혈구제제 수혈 여부

                // C.항생제 투여 여부
                ANBO_USE_YN = reader["ANBO_USE_YN"].ToString(); // 항생제 투여 여부(1.Yes 2.No)

                // 퇴원 시 항생제 처방
                DSCG_ANBO_PRSC_YN = reader["DSCG_ANBO_PRSC_YN"].ToString(); // 퇴원시 항생제 처방 여부(1.Yes 2.No)

                return MetroLib.SqlHelper.BREAK;
            });

            // B. 수혈 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM010_BLTS";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                BLTS_STA_DT.Add(reader["BLTS_STA_DT"].ToString());
                BLTS_END_DT.Add(reader["BLTS_END_DT"].ToString());
                BLTS_MDFEE_CD.Add(reader["BLTS_MDFEE_CD"].ToString());
                BLTS_DGM_NM.Add(reader["BLTS_DGM_NM"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // C. 항생제 투여 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM010_ANBO";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                INJC_STA_DT.Add(reader["INJC_STA_DT"].ToString());
                INJC_END_DT.Add(reader["INJC_END_DT"].ToString());
                INJC_MDS_CD.Add(reader["INJC_MDS_CD"].ToString());
                INJC_MDS_NM.Add(reader["INJC_MDS_NM"].ToString());
                ANBO_INJC_PTH_CD.Add(reader["ANBO_INJC_PTH_CD"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 퇴원 시 항생제 처방 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM010_PRSC";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                PRSC_MDS_CD.Add(reader["PRSC_MDS_CD"].ToString());
                PRSC_MDS_NM.Add(reader["PRSC_MDS_NM"].ToString());
                PRSC_TOT_INJC_DDCNT.Add(reader["PRSC_TOT_INJC_DDCNT"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });
        }

        public void ReadDataFromEMR(OleDbConnection conn, OleDbTransaction p_tran)
        {
            ClearMe();

            // A. 기본정보
            ASM_IPAT_DT = A04_BEDEDT + A04_BEDEHM; // 입원일시(YYYYMMDDHHMM)
            DSCG_YN = A04_BEDODT != "" ? "1" : "2"; // 퇴원여부(1.Yes 2.No)
            ASM_DSCG_DT = A04_BEDODT + A04_BEDOHM; // 퇴원일시(YYYYMMDDHHMM)

            // TU02 키...TU01과 TU03을 읽을 때 사용함.
            string u02_bededt = "";
            string u02_opdt = "";
            string u02_dptcd = "";
            string u02_opseq = "";
            string u02_ocd = "";
            string u02_seq = "";

            string statfg = "";
            // 수술비내역(TU02)을 읽으면서 심평원에서 준 수술코드와 같은 자료를 찾는다.
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U02.*, A02.ISPCD";
            sql += System.Environment.NewLine + "  FROM TU02 U02 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
            sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
            sql += System.Environment.NewLine + " WHERE U02.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND U02.BEDEDT='" + BDEDT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY U02.OPDT,U02.DPTCD,U02.OPSEQ,U02.OCD,U02.SEQ";

            bool find = false;
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string ispcd = row["ISPCD"].ToString();

                // 수술코드는 청구한 자료를 사용한다.
                string ispcd5 = ispcd.Length > 5 ? ispcd.Substring(0, 5) : ispcd;
                if (MDFEE_CD != ispcd5) return MetroLib.SqlHelper.CONTINUE;

                find = true;

                u02_bededt = row["BEDEDT"].ToString();
                u02_opdt = row["OPDT"].ToString();
                u02_dptcd = row["DPTCD"].ToString();
                u02_opseq = row["OPSEQ"].ToString();
                u02_ocd = row["OCD"].ToString();
                u02_seq = row["SEQ"].ToString();

                statfg = row["STAFG"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // 2026.02.06 WOOIL - 수술비코드를 수술재료에 같이 등록하는 병원이 있음.(서울바른척도)
            if (find == false)
            {
                sql = "";
                sql += System.Environment.NewLine + "SELECT U05.*, A02.ISPCD";
                sql += System.Environment.NewLine + "  FROM TU05 U05 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U05.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U05.OCD AND X.CREDT<=U05.OPDT)";
                sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U05.OPDT)";
                sql += System.Environment.NewLine + " WHERE U05.PID='" + PID + "'";
                sql += System.Environment.NewLine + "   AND U05.BEDEDT='" + BDEDT + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(U05.CHGDT,'')=''";
                sql += System.Environment.NewLine + "   AND ISNULL(A02.GUBUN,'')='1'"; // 수가만(재료 제외)
                sql += System.Environment.NewLine + " ORDER BY U05.OPDT,U05.DPTCD,U05.OPSEQ,U05.OCD,U05.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row["ISPCD"].ToString();

                    // 수술코드는 청구한 자료를 사용한다.
                    string ispcd5 = ispcd.Length > 5 ? ispcd.Substring(0, 5) : ispcd;
                    if (MDFEE_CD != ispcd5) return MetroLib.SqlHelper.CONTINUE;

                    find = true;

                    u02_bededt = row["BEDEDT"].ToString();
                    u02_opdt = row["OPDT"].ToString();
                    u02_dptcd = row["DPTCD"].ToString();
                    u02_opseq = row["OPSEQ"].ToString();
                    u02_ocd = row["OCD"].ToString();
                    u02_seq = row["SEQ"].ToString();

                    statfg = "0"; // TU05에는 STAFG가 없음. 0.정규로 기본 설정함.

                    return MetroLib.SqlHelper.BREAK;
                });
            }

            // 2026.02.09 WOOIL - 수술내역을 못 찾더라도항생제투여 정보는 읽어오자
            if (find)
            {
                // 수술예정자(TU01) 자료
                string opsdt = "";
                string opshr = "";
                string opsmn = "";
                string opedt = "";
                string opehr = "";
                string opemn = "";

                sql = "";
                sql += System.Environment.NewLine + "SELECT *";
                sql += System.Environment.NewLine + "  FROM TU01 U01 (NOLOCK)";
                sql += System.Environment.NewLine + " WHERE U01.PID='" + PID + "'";
                sql += System.Environment.NewLine + "   AND U01.OPDT='" + u02_opdt + "'";
                sql += System.Environment.NewLine + "   AND U01.DPTCD='" + u02_dptcd + "'";
                sql += System.Environment.NewLine + "   AND U01.OPSEQ='" + u02_opseq + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
                sql += System.Environment.NewLine + " ORDER BY U01.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    opsdt = row["OPSDT"].ToString();
                    opshr = row["OPSHR"].ToString();
                    opsmn = row["OPSMN"].ToString();
                    opedt = row["OPEDT"].ToString();
                    opehr = row["OPEHR"].ToString();
                    opemn = row["OPEMN"].ToString();

                    opshr = opshr.PadLeft(2, '0');
                    opsmn = opsmn.PadLeft(2, '0');
                    opehr = opehr.PadLeft(2, '0');
                    opemn = opemn.PadLeft(2, '0');

                    return MetroLib.SqlHelper.BREAK;
                });

                // 마취내역을 읽는다.
                string aneno = ""; // TU91을 읽기위한 용도
                string anetp = "";
                string anedt = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT *";
                sql += System.Environment.NewLine + "  FROM TU03 U03 (NOLOCK)";
                sql += System.Environment.NewLine + " WHERE U03.PID='" + PID + "'";
                sql += System.Environment.NewLine + "   AND U03.OPDT='" + u02_opdt + "'";
                sql += System.Environment.NewLine + "   AND U03.DPTCD='" + u02_dptcd + "'";
                sql += System.Environment.NewLine + "   AND U03.OPSEQ='" + u02_opseq + "'";
                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    aneno = row["ANENO"].ToString();
                    anetp = row["ANETP"].ToString();
                    anedt = row["ANEDT"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                // 마취 방법에 대한 수가코드(EDI코드)를 찾는다.
                sql = "";
                sql += System.Environment.NewLine + "SELECT A18.PRICD, A02.ISPCD";
                sql += System.Environment.NewLine + "  FROM TA18 A18 (NOLOCK) INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<='" + anedt + "')";
                sql += System.Environment.NewLine + " WHERE A18.OCD = '*A" + anetp + "'";
                sql += System.Environment.NewLine + "   AND A18.CREDT = (SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=A18.OCD AND X.CREDT<='" + anedt + "')";

                string base_ane = "2";
                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string anetp_ispcd = row["ISPCD"].ToString();

                    if (anetp_ispcd.StartsWith("L0101")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L0102")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L0103")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1211")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1212")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1213")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1214")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1215")) base_ane = "1";
                    else if (anetp_ispcd.StartsWith("L1216")) base_ane = "1";

                    return MetroLib.SqlHelper.BREAK;
                });

                // ASA점수(TU91) ... 이 테이블이 우선임.
                bool find_asa = false;
                string asa_score = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT ASA";
                sql += System.Environment.NewLine + "  FROM TU91";
                sql += System.Environment.NewLine + " WHERE ANENO='" + aneno + "'";

                MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string asa = row["ASA"].ToString();

                    find_asa = true;
                    if (!string.IsNullOrEmpty(asa))
                    {
                        int idx = asa.IndexOf((char)25);
                        string value = (idx >= 0) ? asa.Substring(0, idx) : asa;
                        if (value == "0") asa_score = "1";
                        else if (value == "1") asa_score = "2";
                        else if (value == "2") asa_score = "3";
                        else if (value == "3") asa_score = "4";
                        else if (value == "4") asa_score = "5";
                        else if (value == "5") asa_score = "6";
                        else if (value == "6") asa_score = "7";
                        else if (value == "7") asa_score = "8";
                    }

                    return MetroLib.SqlHelper.BREAK;
                });

                // ASA점수(EMR082_2) ... TU91이 없으면 이 테이블에서...
                if (find_asa == false)
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT ASA, ASA6, ASA7";
                    sql += System.Environment.NewLine + "  FROM EMR082_2";
                    sql += System.Environment.NewLine + " WHERE PID='" + PID + "'";
                    sql += System.Environment.NewLine + "   AND OPDT='" + u02_opdt + "'";
                    sql += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                    MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                    {
                        System.Windows.Forms.Application.DoEvents();

                        string asa = row["ASA"].ToString();
                        string asa6 = row["ASA6"].ToString();
                        string asa7 = row["ASA7"].ToString();

                        asa += "      "; // 오류방지용
                        if (asa.Substring(0, 1) == "1") asa_score = "1";
                        else if (asa.Substring(1, 1) == "1") asa_score = "2";
                        else if (asa.Substring(2, 1) == "1") asa_score = "3";
                        else if (asa.Substring(3, 1) == "1") asa_score = "4";
                        else if (asa.Substring(4, 1) == "1") asa_score = "5";
                        else if (asa6 == "1") asa_score = "6";
                        else if (asa7 == "1") asa_score = "7";
                        else if (asa.Substring(5, 1) == "1") asa_score = "8";

                        return MetroLib.SqlHelper.BREAK;
                    });
                }

                // 값을 할당한다. ------------------------------

                ASM_SOPR_STA_DT = opsdt + opshr + opsmn; // 수술 시작일시(YYYYMMDDHHMM)
                ASM_SOPR_END_DT = opedt + opehr + opemn; // 수술 종료일시(YYYYMMDDHHMM)
                EMY_CD = statfg == "0" ? "1" : "2"; // 응급여부(1.정규 2.응급)
                if (CConfig.asm010_fld3qty != "") EMY_CD = CConfig.asm010_fld3qty; // 옵션적용
                KNJN_RPMT = MDFEE_CD.StartsWith("N2072") || MDFEE_CD.StartsWith("N2077") ? "1" : "2"; // 슬관절치환술(1.Yes 2.No)
                HMRHG_CTRL_YN = "2"; // 토니켓 적용 여부(1.Yes 2.No)
                HMRHG_CTRL_DT = ""; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
                // 슬괄전치환술이면 토니켓 사용함.
                if (KNJN_RPMT == "1")
                {
                    HMRHG_CTRL_YN = "1"; // 토니켓 적용 여부(1.Yes 2.No)
                    HMRHG_CTRL_DT = ASM_SOPR_STA_DT; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
                }
                CAESR_YN = MDFEE_CD.StartsWith("R4517") || MDFEE_CD.StartsWith("R4518") || MDFEE_CD.StartsWith("R4514") ? "1" : "2"; // 제왕절개술 시행 여부(1.Yes 2.No)
                NBY_PARTU_DT = ""; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
                CRVD_YN = ""; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
                BSE_NCT_YN = base_ane; // 기본마취 여부(1.Yes 2.No)
                ASA_PNT = asa_score; // ASA 점수
                // 2.수술 전 항생제 투여
                SOPR_BF_ANBO_INJC_YN = ""; // 수술 전 항생제 투여 여부(1.Yes 2.No)
                SOPR_BF_INFC_SICK_YN = ""; // 감염상병 확진 여부(1.Yes 2.No)
                SOPR_BF_INFC_SICK_CD = ""; // 감염상병 확진명
                SOPR_BF_DDIAG_YN = ""; // 감염내과 협진여부(1.Yes 2.No)
                SOPR_BF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
                SOPR_BF_RPY_YN = ""; // 회신 여부(1.Yes 2.No)
                SOPR_BF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
                SOPR_BF_ANBO_DR_RCD_YN = ""; // 항생제 필요 의사기록 여부(1.Yes 2.No)
                SOPR_BF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
                SOPR_BF_ANBO_DR_RCDC_CD = ""; // 기록지 종류(YYYYMMDDHHMM)
                SOPR_BF_REQR_RS_CD = ""; // 필요사유
                SOPR_BF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
                // 3.평가 제외 수술
                ASM_TGT_SOPR_SAME_ENFC_YN = ""; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
                FQ2_GT_SOPR_ENFC_YN = ""; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
                // 4.수술 후 항셍제 투여
                SOPR_RGN_INFC_ANBO_INJC_YN = ""; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
                SOPR_RGN_INFC_CD = ""; // 수술부위 감영 유형
                ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
                SOPR_RGN_INFC_DR_RCDC_CD = ""; // 기록지 종류
                SOPR_RGN_INFC_DR_RCD_TXT = ""; // 수술 부위 감염 사유 상세(평문)
                INFC_ANBO_INJC_YN = ""; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
                CLTR_STRN_YN = ""; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
                ASM_GAT_DT = ""; // 채취일시(YYYYMMDDHHMM)
                INFC_SICK_DIAG = ""; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_INFC_SICK_CD = ""; // 감염 상병 화진명
                SOPR_AF_DDIAG_YN = ""; // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
                SOPR_AF_RPY_YN = ""; // 회신 여부(1.Yes 2.No)
                SOPR_AF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
                SOPR_AF_ANBO_DR_RCD_YN = ""; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
                SOPR_AF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
                SOPR_AF_ANBO_DR_RCDC_CD = ""; // 기록지 종류
                SOPR_AF_REQR_RS_CD = ""; // 필요사유
                SOPR_AF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
                ANBO_ALRG_YN = ""; // 항생제 알러지 여부(1.Yes 2.No)

                WHBL_RBC_BLTS_YN = ""; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
                BLTS_STA_DT.Clear(); // 수혈시작일시
                BLTS_END_DT.Clear(); // 수혈종료일시
                BLTS_MDFEE_CD.Clear(); // 수가코드
                BLTS_DGM_NM.Clear(); // 수혈제제명
            }

            // 수혈 (TB08)

            // 혈액별로 "리스트에 들어간 인덱스"를 기억 (같은 bldcd면 기존 인덱스 갱신)
            Dictionary<string, int> bldIndexMap = new Dictionary<string, int>();

            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TB08 B08 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=B08.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=B08.OCD AND X.CREDT<=B08.BLDODT)";
            sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=B08.BLDODT)";
            sql += System.Environment.NewLine + " WHERE B08.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND B08.BEDEDT='" + A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND B08.BLDODT>='" + (u02_opdt == "" ? A04_BEDEDT : u02_opdt) + "'"; // u02_opdt를 못찾았을 경우 bededt부터
            sql += System.Environment.NewLine + "   AND ISNULL(B08.BLDRTNDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY B08.BLDODT";
            
            WHBL_RBC_BLTS_YN = "2"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            MetroLib.SqlHelper.GetDataRow(sql, conn, p_tran, delegate(DataRow row)
            {
                string pricd = row["PRICD"].ToString();
                string bldodt = row["BLDODT"].ToString();

                // 그룹수가로 되어있는 경우가 있음.
                if (pricd.StartsWith("X"))
                {
                    string sql2 = "";
                    sql2 += Environment.NewLine + "SELECT *";
                    sql2 += Environment.NewLine + "  FROM TA02A A02A INNER JOIN TA02 A02 ON A02.PRICD=A02A.SPCD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A02.PRICD AND X.CREDT<='" + bldodt + "')";
                    sql2 += Environment.NewLine + " WHERE A02A.PRICD='" + pricd + "'";
                    sql2 += Environment.NewLine + "   AND A02A.CREDT=(SELECT MAX(X.CREDT) FROM TA02A X WHERE X.PRICD=A02A.PRICD AND X.CREDT<='" + bldodt + "')";

                    MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                    {
                        string bldcd = row2["ISPCD"].ToString();
                        string prknm = row2["PRKNM"].ToString();
                        if (CUtil_ASM010.IsBLDCode(bldcd))
                        {
                            WHBL_RBC_BLTS_YN = "1"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)

                            int idx;
                            if (!bldIndexMap.TryGetValue(bldcd, out idx))
                            {
                                // 처음 나오는 약품코드 => 새 레코드 생성
                                idx = BLTS_MDFEE_CD.Count; // parallel-list 인덱스
                                bldIndexMap[bldcd] = idx;

                                BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                                BLTS_END_DT.Add(bldodt); // 수혈종료일시
                                BLTS_MDFEE_CD.Add(bldcd); // 수가코드
                                BLTS_DGM_NM.Add(prknm); // 수혈제제명
                            }
                            else
                            {
                                BLTS_END_DT[idx] = bldodt; // 수혈종료일시
                            }
                        }
                        return MetroLib.SqlHelper.CONTINUE;
                    });
                }
                else
                {
                    string bldcd = row["ISPCD"].ToString();
                    string prknm = row["PRKNM"].ToString();
                    if (CUtil_ASM010.IsBLDCode(bldcd))
                    {
                        WHBL_RBC_BLTS_YN = "1"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)

                        int idx;
                        if (!bldIndexMap.TryGetValue(bldcd, out idx))
                        {
                            // 처음 나오는 약품코드 => 새 레코드 생성
                            idx = BLTS_MDFEE_CD.Count; // parallel-list 인덱스
                            bldIndexMap[bldcd] = idx;

                            BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                            BLTS_END_DT.Add(bldodt); // 수혈종료일시
                            BLTS_MDFEE_CD.Add(bldcd); // 수가코드
                            BLTS_DGM_NM.Add(prknm); // 수혈제제명
                        }
                        else
                        {
                            BLTS_END_DT[idx] = bldodt; // 수혈종료일시
                        }
                    }
                }
                return MetroLib.SqlHelper.CONTINUE;
            });

            // C.항생제 투여 여부
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TV20 V20 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=V20.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V20.OCD AND X.CREDT<=V20.DODT)";
            sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V20.DODT)";
            if (CConfig.BodyNewFg == "1")
            {
                sql += System.Environment.NewLine + "                         INNER JOIN TV01A V01A (NOLOCK) ON V01A.BPID=V20.PID AND V01A.BBEDEDT=V20.BEDEDT AND V01A.BBDIV=V20.BDIV AND V01A.BODT=V20.ODT AND V01A.BONO=V20.ONO AND V01A.OCD=V20.OCD";
            }
            else
            {
                sql += System.Environment.NewLine + "                         INNER JOIN TV01 V01 (NOLOCK) ON V01.PID=V20.PID AND V01.BEDEDT=V20.BEDEDT AND V01.BDIV=V20.BDIV AND V01.ODT=V20.ODT AND V01.ONO=V20.ONO";
                sql += System.Environment.NewLine + "                         INNER JOIN TV01A V01A (NOLOCK) ON V01A.HDID=V01.HDID AND V01A.OCD=V20.OCD";
            }
            sql += System.Environment.NewLine + " WHERE V20.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND V20.BEDEDT='" + A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND V20.ODIVCD LIKE 'M%'";
            sql += System.Environment.NewLine + "   AND V20.DSTSCD = 'Y'";
            sql += System.Environment.NewLine + "   AND ISNULL(V20.CHNGDT,'') = ''";
            sql += System.Environment.NewLine + "   AND V20.DQTY <> 0";
            sql += System.Environment.NewLine + " ORDER BY V20.DODT, V20.DOHR, V20.DOMN";

            // 항생제 투여 여부
            //    같은 EDI코드는 한줄로 만드러야 함.  시작일~종료일로....

            // 약품코드별로 "리스트에 들어간 인덱스"를 기억 (같은 anbocd면 기존 인덱스 갱신)
            Dictionary<string, int> anboIndexMap = new Dictionary<string, int>();

            ANBO_USE_YN = "2"; // 항생제 투여 여부(1.Yes 2.No)
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string anbocd = row["ISPCD"].ToString();
                if (CUtil_ASM010.IsANBOCode(anbocd))
                {
                    string sta = row["DODT"].ToString() + row["DOHR"].ToString() + row["DOMN"].ToString();
                    string end = row["DODT"].ToString() + row["DOHR"].ToString() + row["DOMN"].ToString();

                    string fldcd4 = row["FLDCD4"].ToString();
                    string injPath = "";
                    if (fldcd4.StartsWith("IV")) injPath = "1";
                    if (fldcd4.StartsWith("IM")) injPath = "2";
                    if (fldcd4.StartsWith("PO")) injPath = "3";
                    if (fldcd4.StartsWith("IW")) injPath = "1"; // 2026.02.09 WOOIL - 서울바른척도와 협의

                    ANBO_USE_YN = "1"; // 항생제 투여 여부(1.Yes 2.No)

                    int idx;
                    if (!anboIndexMap.TryGetValue(anbocd, out idx))
                    {
                        // 처음 나오는 약품코드 => 새 레코드 생성
                        idx = INJC_MDS_CD.Count; // parallel-list 인덱스
                        anboIndexMap[anbocd] = idx;

                        // 항생제 투여
                        INJC_STA_DT.Add(sta); // 투여시작일시
                        INJC_END_DT.Add(end); // 투여종료일시
                        INJC_MDS_CD.Add(anbocd); // 약품코드
                        INJC_MDS_NM.Add(row["PRKNM"].ToString()); // 약품명
                        ANBO_INJC_PTH_CD.Add(injPath); // 투여경로
                    }
                    else
                    {
                        // sql 문에서 조회할 때 시간순으로 조회하므로 종료일시만 갱신하면 된다.
                        INJC_END_DT[idx] = end;
                    }

                }

                return MetroLib.SqlHelper.CONTINUE;

            });

            // 퇴원 시 항생제 처방
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TV01 V01 (NOLOCK) INNER JOIN TV01A V01A (NOLOCK)";
            if (CConfig.BodyNewFg == "1")
            {
                sql += System.Environment.NewLine + "                                 ON V01A.BPID=V01.PID AND V01A.BBEDEDT=V01.BEDEDT AND V01A.BBDIV=V01.BDIV AND V01A.BODT=V01.ODT AND V01A.BONO=V01.ONO";
            }
            else
            {
                sql += System.Environment.NewLine + "                                 ON V01A.HDID=V01.HDID";
            }
            sql += System.Environment.NewLine + "                         INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V01.ODT)";
            sql += System.Environment.NewLine + " WHERE V01.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND V01.BEDEDT='" + A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND V01.OKCD = '3'";
            sql += System.Environment.NewLine + "   AND V01.ODIVCD LIKE 'M%'";
            sql += System.Environment.NewLine + "   AND ISNULL(V01.DCFG,'') IN ('','0')";

            DSCG_ANBO_PRSC_YN = "2"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string anbocd = row["ISPCD"].ToString();
                if (CUtil_ASM010.IsANBOCode(anbocd))
                {
                    DSCG_ANBO_PRSC_YN = "1"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)

                    PRSC_MDS_CD.Add(anbocd); // 약품코드
                    PRSC_MDS_NM.Add(row["PRKNM"].ToString()); // 약품명
                    PRSC_TOT_INJC_DDCNT.Add(row["ODAYCNT"].ToString()); // 총 투약일수
                }

                return MetroLib.SqlHelper.CONTINUE;
            });
        }


        public void InsData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg)
        {
            string sql = "";

            if (del_fg == true)
            {
                sql = "";
                sql += "DELETE FROM TI84_ASM010 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM010_ANBO WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM010_PRSC WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // 단일 Row 저장
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM010(";
            sql += Environment.NewLine + " FORM, KEYSTR, SEQ, VER"; // 4
            sql += Environment.NewLine + " , ASM_IPAT_DT, DSCG_YN, ASM_DSCG_DT";
            sql += Environment.NewLine + " , MDFEE_CD, ASM_SOPR_STA_DT, ASM_SOPR_END_DT, EMY_CD, KNJN_RPMT, HMRHG_CTRL_YN, HMRHG_CTRL_DT, CAESR_YN, NBY_PARTU_DT, CRVD_YN, BSE_NCT_YN, ASA_PNT";
            sql += Environment.NewLine + " , SOPR_BF_ANBO_INJC_YN, SOPR_BF_INFC_SICK_YN, SOPR_BF_INFC_SICK_CD, SOPR_BF_DDIAG_YN, SOPR_BF_ASM_REQ_DT, SOPR_BF_RPY_YN, SOPR_BF_ASM_RPY_DT, SOPR_BF_ANBO_DR_RCD_YN, SOPR_BF_ASM_RCD_DT, SOPR_BF_ANBO_DR_RCDC_CD, SOPR_BF_REQR_RS_CD, SOPR_BF_DR_RCD_TXT";
            sql += Environment.NewLine + " , ASM_TGT_SOPR_SAME_ENFC_YN, FQ2_GT_SOPR_ENFC_YN";
            sql += Environment.NewLine + " , SOPR_RGN_INFC_ANBO_INJC_YN, SOPR_RGN_INFC_CD, ASM_RCD_DT, SOPR_RGN_INFC_DR_RCDC_CD, SOPR_RGN_INFC_DR_RCD_TXT, INFC_ANBO_INJC_YN, CLTR_STRN_YN, ASM_GAT_DT, INFC_SICK_DIAG, SOPR_AF_INFC_SICK_CD, SOPR_AF_DDIAG_YN, SOPR_AF_ASM_REQ_DT, SOPR_AF_RPY_YN, SOPR_AF_ASM_RPY_DT, SOPR_AF_ANBO_DR_RCD_YN, SOPR_AF_ASM_RCD_DT, SOPR_AF_ANBO_DR_RCDC_CD, SOPR_AF_REQR_RS_CD, SOPR_AF_DR_RCD_TXT, ANBO_ALRG_YN, WHBL_RBC_BLTS_YN";
            sql += Environment.NewLine + " , ANBO_USE_YN";
            sql += Environment.NewLine + " , DSCG_ANBO_PRSC_YN";
            sql += Environment.NewLine + ")";
            sql += Environment.NewLine + "VALUES(";
            sql += Environment.NewLine + " ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?";
            sql += Environment.NewLine + " , ?";
            sql += Environment.NewLine + ")";

            para.Clear();
            para.Add(form);                // FORM
            para.Add(KEYSTR);              // KEYSTR
            para.Add(SEQ);                 // SEQ
            para.Add(ver);                 // VER

            // A. 기본정보
            para.Add(ASM_IPAT_DT);         // 입원일시(YYYYMMDDHHMM)
            para.Add(DSCG_YN);             // 퇴원여부(1.Yes 2.No)
            para.Add(ASM_DSCG_DT);         // 퇴원일시(YYYYMMDDHHMM)

            // B.수술 및 감염 정보 - 1. 수술 관련 환자 상태
            para.Add(MDFEE_CD);            // 수가코드
            para.Add(ASM_SOPR_STA_DT);     // 수술 시작일시
            para.Add(ASM_SOPR_END_DT);     // 수술 종료일시
            para.Add(EMY_CD);              // 응급여부
            para.Add(KNJN_RPMT);           // 슬관절치환술
            para.Add(HMRHG_CTRL_YN);       // 토니켓 적용 여부
            para.Add(HMRHG_CTRL_DT);       // 토니켓 팽창 시작일시
            para.Add(CAESR_YN);            // 제왕절개술 시행 여부
            para.Add(NBY_PARTU_DT);        // 제대결찰(태아만출)일시
            para.Add(CRVD_YN);             // 자궁경부 4cm이상 기대 여부
            para.Add(BSE_NCT_YN);          // 기본마취 여부
            para.Add(ASA_PNT);             // ASA 점수

            // B.수술 및 감염 정보 - 2.수술 전 항생제 투여
            para.Add(SOPR_BF_ANBO_INJC_YN);    // 수술 전 항생제 투여 여부
            para.Add(SOPR_BF_INFC_SICK_YN);    // 감염상병 확진 여부
            para.Add(SOPR_BF_INFC_SICK_CD);    // 감염상병 확진명
            para.Add(SOPR_BF_DDIAG_YN);        // 감염내과 협진여부
            para.Add(SOPR_BF_ASM_REQ_DT);      // 의뢰일시
            para.Add(SOPR_BF_RPY_YN);          // 회신 여부
            para.Add(SOPR_BF_ASM_RPY_DT);      // 회신일시
            para.Add(SOPR_BF_ANBO_DR_RCD_YN);  // 항생제 필요 의사기록 여부
            para.Add(SOPR_BF_ASM_RCD_DT);      // 기록일시
            para.Add(SOPR_BF_ANBO_DR_RCDC_CD); // 기록지 종류
            para.Add(SOPR_BF_REQR_RS_CD);      // 필요사유
            para.Add(SOPR_BF_DR_RCD_TXT);      // 기록 상세 내용(평문)

            // B.수술 및 감염 정보 - 3.평가 제외 수술
            para.Add(ASM_TGT_SOPR_SAME_ENFC_YN);   // 대상 수술과 동시에 다른 수술 시행 여부
            para.Add(FQ2_GT_SOPR_ENFC_YN);         // 동일 입원기간 내에 2회 이상 수술 시행 여부

            // B.수술 및 감염 정보 - 4.수술 후 항셍제 투여
            para.Add(SOPR_RGN_INFC_ANBO_INJC_YN);      // 수술 후 수술부위 감염으로 항생제 투여 여부
            para.Add(SOPR_RGN_INFC_CD);                // 수술부위 감영 유형
            para.Add(ASM_RCD_DT);                      // 기록일시
            para.Add(SOPR_RGN_INFC_DR_RCDC_CD);        // 기록지 종류
            para.Add(SOPR_RGN_INFC_DR_RCD_TXT);        // 수술 부위 감염 사유 상세(평문)
            para.Add(INFC_ANBO_INJC_YN);               // 수술 후 수술부위 외 감염으로 항생제 투여 여부
            para.Add(CLTR_STRN_YN);                    // 혈액,뇌척수액 배양에서 균 분리 여부
            para.Add(ASM_GAT_DT);                      // 채취일시
            para.Add(INFC_SICK_DIAG);                  // 감염 상병 화진후 항생제 투여 여부
            para.Add(SOPR_AF_INFC_SICK_CD);            // 감염 상병 화진명
            para.Add(SOPR_AF_DDIAG_YN);                // 감염내과 협진후 항생제 투여 여부
            para.Add(SOPR_AF_ASM_REQ_DT);              // 의뢰일시
            para.Add(SOPR_AF_RPY_YN);                  // 회신 여부
            para.Add(SOPR_AF_ASM_RPY_DT);              // 회신일시
            para.Add(SOPR_AF_ANBO_DR_RCD_YN);          // 항생제 필요 의사기록이 있고 항생제 투여 여부
            para.Add(SOPR_AF_ASM_RCD_DT);              // 기록일시
            para.Add(SOPR_AF_ANBO_DR_RCDC_CD);         // 기록지 종류
            para.Add(SOPR_AF_REQR_RS_CD);              // 필요사유
            para.Add(SOPR_AF_DR_RCD_TXT);              // 기록 상세 내용(평문)
            para.Add(ANBO_ALRG_YN);                    // 항생제 알러지 여부
            para.Add(WHBL_RBC_BLTS_YN);                // 수술시작 후 전혈 및 적혈구제제 수혈 여부

            // C.항생제 투여 여부
            para.Add(ANBO_USE_YN);                 // 항생제 투여 여부

            // D.퇴원 시 항생제 처방
            para.Add(DSCG_ANBO_PRSC_YN);           // 퇴원시 항생제 처방 여부

            // 실행
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);


            // 항생제 투여 정보 저장
            for (int i = 0; i < INJC_STA_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM010_ANBO(FORM, KEYSTR, SEQ, SEQNO, INJC_STA_DT, INJC_END_DT, INJC_MDS_CD, INJC_MDS_NM, ANBO_INJC_PTH_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(INJC_STA_DT[i]);
                para.Add(INJC_END_DT[i]);
                para.Add(INJC_MDS_CD[i]);
                para.Add(INJC_MDS_NM[i]);
                para.Add(ANBO_INJC_PTH_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 퇴원 시 항생제 처방 저장
            for (int i = 0; i < PRSC_MDS_CD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM010_PRSC(FORM, KEYSTR, SEQ, SEQNO, PRSC_MDS_CD, PRSC_MDS_NM, PRSC_TOT_INJC_DDCNT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(PRSC_MDS_CD[i]);
                para.Add(PRSC_MDS_NM[i]);
                para.Add(PRSC_TOT_INJC_DDCNT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // 단일 Row UPDATE
            string sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM010";
            sql += Environment.NewLine + " SET ASM_IPAT_DT=?, DSCG_YN=?, ASM_DSCG_DT=?";
            sql += Environment.NewLine + " , MDFEE_CD=?, ASM_SOPR_STA_DT=?, ASM_SOPR_END_DT=?, EMY_CD=?, KNJN_RPMT=?, HMRHG_CTRL_YN=?, HMRHG_CTRL_DT=?, CAESR_YN=?, NBY_PARTU_DT=?, CRVD_YN=?, BSE_NCT_YN=?, ASA_PNT=?"; // 12
            sql += Environment.NewLine + " , SOPR_BF_ANBO_INJC_YN=?, SOPR_BF_INFC_SICK_YN=?, SOPR_BF_INFC_SICK_CD=?, SOPR_BF_DDIAG_YN=?, SOPR_BF_ASM_REQ_DT=?, SOPR_BF_RPY_YN=?, SOPR_BF_ASM_RPY_DT=?, SOPR_BF_ANBO_DR_RCD_YN=?, SOPR_BF_ASM_RCD_DT=?, SOPR_BF_ANBO_DR_RCDC_CD=?, SOPR_BF_REQR_RS_CD=?, SOPR_BF_DR_RCD_TXT=?";
            sql += Environment.NewLine + " , ASM_TGT_SOPR_SAME_ENFC_YN=?, FQ2_GT_SOPR_ENFC_YN=?";
            sql += Environment.NewLine + " , SOPR_RGN_INFC_ANBO_INJC_YN=?, SOPR_RGN_INFC_CD=?, ASM_RCD_DT=?, SOPR_RGN_INFC_DR_RCDC_CD=?, SOPR_RGN_INFC_DR_RCD_TXT=?, INFC_ANBO_INJC_YN=?, CLTR_STRN_YN=?, ASM_GAT_DT=?, INFC_SICK_DIAG=?, SOPR_AF_INFC_SICK_CD=?, SOPR_AF_DDIAG_YN=?, SOPR_AF_ASM_REQ_DT=?, SOPR_AF_RPY_YN=?, SOPR_AF_ASM_RPY_DT=?, SOPR_AF_ANBO_DR_RCD_YN=?, SOPR_AF_ASM_RCD_DT=?, SOPR_AF_ANBO_DR_RCDC_CD=?, SOPR_AF_REQR_RS_CD=?, SOPR_AF_DR_RCD_TXT=?, ANBO_ALRG_YN=?, WHBL_RBC_BLTS_YN=?";
            sql += Environment.NewLine + " , ANBO_USE_YN=?, DSCG_ANBO_PRSC_YN=?";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            para.Clear();
            para.Add(ASM_IPAT_DT);
            para.Add(DSCG_YN);
            para.Add(ASM_DSCG_DT);

            // B.수술 및 감염 정보 - 1. 수술 관련 환자 상태
            para.Add(MDFEE_CD);            // 수가코드
            para.Add(ASM_SOPR_STA_DT);     // 수술 시작일시
            para.Add(ASM_SOPR_END_DT);     // 수술 종료일시
            para.Add(EMY_CD);              // 응급여부
            para.Add(KNJN_RPMT);           // 슬관절치환술
            para.Add(HMRHG_CTRL_YN);       // 토니켓 적용 여부
            para.Add(HMRHG_CTRL_DT);       // 토니켓 팽창 시작일시
            para.Add(CAESR_YN);            // 제왕절개술 시행 여부
            para.Add(NBY_PARTU_DT);        // 제대결찰(태아만출)일시
            para.Add(CRVD_YN);             // 자궁경부 4cm이상 기대 여부
            para.Add(BSE_NCT_YN);          // 기본마취 여부
            para.Add(ASA_PNT);             // ASA 점수

            // B.수술 및 감염 정보 - 2.수술 전 항생제 투여
            para.Add(SOPR_BF_ANBO_INJC_YN);    // 수술 전 항생제 투여 여부
            para.Add(SOPR_BF_INFC_SICK_YN);    // 감염상병 확진 여부
            para.Add(SOPR_BF_INFC_SICK_CD);    // 감염상병 확진명
            para.Add(SOPR_BF_DDIAG_YN);        // 감염내과 협진여부
            para.Add(SOPR_BF_ASM_REQ_DT);      // 의뢰일시
            para.Add(SOPR_BF_RPY_YN);          // 회신 여부
            para.Add(SOPR_BF_ASM_RPY_DT);      // 회신일시
            para.Add(SOPR_BF_ANBO_DR_RCD_YN);  // 항생제 필요 의사기록 여부
            para.Add(SOPR_BF_ASM_RCD_DT);      // 기록일시
            para.Add(SOPR_BF_ANBO_DR_RCDC_CD); // 기록지 종류
            para.Add(SOPR_BF_REQR_RS_CD);      // 필요사유
            para.Add(SOPR_BF_DR_RCD_TXT);      // 기록 상세 내용(평문)

            // B.수술 및 감염 정보 - 3.평가 제외 수술
            para.Add(ASM_TGT_SOPR_SAME_ENFC_YN);   // 대상 수술과 동시에 다른 수술 시행 여부
            para.Add(FQ2_GT_SOPR_ENFC_YN);         // 동일 입원기간 내에 2회 이상 수술 시행 여부

            // B.수술 및 감염 정보 - 4.수술 후 항셍제 투여
            para.Add(SOPR_RGN_INFC_ANBO_INJC_YN);      // 수술 후 수술부위 감염으로 항생제 투여 여부
            para.Add(SOPR_RGN_INFC_CD);                // 수술부위 감영 유형
            para.Add(ASM_RCD_DT);                      // 기록일시
            para.Add(SOPR_RGN_INFC_DR_RCDC_CD);        // 기록지 종류
            para.Add(SOPR_RGN_INFC_DR_RCD_TXT);        // 수술 부위 감염 사유 상세(평문)
            para.Add(INFC_ANBO_INJC_YN);               // 수술 후 수술부위 외 감염으로 항생제 투여 여부
            para.Add(CLTR_STRN_YN);                    // 혈액,뇌척수액 배양에서 균 분리 여부
            para.Add(ASM_GAT_DT);                      // 채취일시
            para.Add(INFC_SICK_DIAG);                  // 감염 상병 화진후 항생제 투여 여부
            para.Add(SOPR_AF_INFC_SICK_CD);            // 감염 상병 화진명
            para.Add(SOPR_AF_DDIAG_YN);                // 감염내과 협진후 항생제 투여 여부
            para.Add(SOPR_AF_ASM_REQ_DT);              // 의뢰일시
            para.Add(SOPR_AF_RPY_YN);                  // 회신 여부
            para.Add(SOPR_AF_ASM_RPY_DT);              // 회신일시
            para.Add(SOPR_AF_ANBO_DR_RCD_YN);          // 항생제 필요 의사기록이 있고 항생제 투여 여부
            para.Add(SOPR_AF_ASM_RCD_DT);              // 기록일시
            para.Add(SOPR_AF_ANBO_DR_RCDC_CD);         // 기록지 종류
            para.Add(SOPR_AF_REQR_RS_CD);              // 필요사유
            para.Add(SOPR_AF_DR_RCD_TXT);              // 기록 상세 내용(평문)
            para.Add(ANBO_ALRG_YN);                    // 항생제 알러지 여부
            para.Add(WHBL_RBC_BLTS_YN);                // 수술시작 후 전혈 및 적혈구제제 수혈 여부
            para.Add(ANBO_USE_YN);
            para.Add(DSCG_ANBO_PRSC_YN);
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 기존 수혈/항생제/처방 데이터 삭제
            sql = "";
            sql += Environment.NewLine + "DELETE";
            sql += Environment.NewLine + "  FROM TI84_ASM010_BLTS";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "";
            sql += Environment.NewLine + "DELETE";
            sql += Environment.NewLine + "  FROM TI84_ASM010_ANBO";
            sql += Environment.NewLine + "WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "  AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "  AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "";
            sql += Environment.NewLine + "DELETE";
            sql += Environment.NewLine + "  FROM TI84_ASM010_PRSC";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            // 재삽입
            for (int i = 0; i < BLTS_STA_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM010_BLTS(FORM, KEYSTR, SEQ, SEQNO, BLTS_STA_DT, BLTS_END_DT, BLTS_MDFEE_CD, BLTS_DGM_NM)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(BLTS_STA_DT[i]);
                para.Add(BLTS_END_DT[i]);
                para.Add(BLTS_MDFEE_CD[i]);
                para.Add(BLTS_DGM_NM[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            for (int i = 0; i < INJC_STA_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM010_ANBO(FORM, KEYSTR, SEQ, SEQNO, INJC_STA_DT, INJC_END_DT, INJC_MDS_CD, INJC_MDS_NM, ANBO_INJC_PTH_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(INJC_STA_DT[i]);
                para.Add(INJC_END_DT[i]);
                para.Add(INJC_MDS_CD[i]);
                para.Add(INJC_MDS_NM[i]);
                para.Add(ANBO_INJC_PTH_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            for (int i = 0; i < PRSC_MDS_CD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM010_PRSC(FORM, KEYSTR, SEQ, SEQNO, PRSC_MDS_CD, PRSC_MDS_NM, PRSC_TOT_INJC_DDCNT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(PRSC_MDS_CD[i]);
                para.Add(PRSC_MDS_NM[i]);
                para.Add(MetroLib.StrHelper.ToLong(PRSC_TOT_INJC_DDCNT[i]));
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // TI84_ASM000 저장 (공통)
            Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM010 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "";
            sql += "DELETE FROM TI84_ASM010_ANBO WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "";
            sql += "DELETE FROM TI84_ASM010_PRSC WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }

    }
}

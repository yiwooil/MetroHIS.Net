using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM049_001 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM049"; // 서식코드
        public readonly string ver_id = "001"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "IMG"; // 업무상세코드

        // --- A. 환자 정보 확인 ---
        public string IPAT_OPAT_TP_CD;   // 청구유형(1:입원, 2:외래)
        public string IPAT_DD;           // 입원일(입원인 경우, YYYYMMDD)
        public string EMRRM_PASS_YN;     // 응급실을 통한 입원 여부(1:Yes, 2:No)
        public string EMRRM_VST_DD;      // 응급실 내원일(YYYYMMDD, 응급실입원 Yes일 때)
        public string DSCG_DD;           // 퇴원일(입원인 경우, YYYYMMDD)
        public string DIAG_DD;           // 내원일(외래인 경우, YYYYMMDD)

        // --- B. 검사정보 ---
        public string IMG_EXM_KND_CD;    // 검사종류(1:CT, 2:MRI, 3:PET, 다중선택시 "/"로 구분)
        public string PET_KND_CD;        // PET 종류(1:PET-CT, 2:PET-MRI, 3:PET, PET 선택시)

        // --- C. CT 검사 ---
        public string CT_DIAG_TY_CD;     // CT 진료유형(1:응급실, 2:입원, 3:외래)
        public string CT_PGP_INTNT_TP_CD;// CT 촬영목적(1:방사선 치료범위 결정, 2:이 외 진단/치료)
        // CT 검사정보(반복)
        public List<string> CT_MDFEE_CD = new List<string>();       // 수가코드
        public List<string> CT_MDFEE_CD_NM = new List<string>();    // 검사명
        public List<string> CT_EXM_EXEC_DT = new List<string>();    // 검사일시(YYYYMMDDHHMM)
        public List<string> CT_RD_SDR_DCT_YN = new List<string>();  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
        public List<string> CT_DCT_RST_DD = new List<string>();     // 판독 완료일(YYYYMMDD)

        // CT 조영제
        public string CT_CTRST_USE_YN;          // 조영제 사용(1:Yes, 2:No)
        public string CT_CTRST_MDCT_PTH_CD;     // 조영제 투약 경로(1:혈관 내 주사, 2:그 외)
        public string CT_CTRST_PTNT_ASM_RCD_YN; // 환자 평가 기록 유무(1:Yes, 2:No)
        public string CT_CTRST_PTNT_ASM_RCD_DD; // 환자 평가 기록 일자(YYYYMMDD)
        public string CT_KDNY_FCLT_EXM_YN;      // 신장기능검사 유무(1:Yes, 2:No)

        // --- D. MRI 검사 ---
        public string MRI_DIAG_TY_CD;           // MRI 진료유형(1:응급실, 2:입원, 3:외래)
        // MRI 검사정보(반복)
        public List<string> MRI_MDFEE_CD = new List<string>();      // 수가코드
        public List<string> MRI_MDFEE_CD_NM = new List<string>();   // 검사명
        public List<string> MRI_EXM_EXEC_DT = new List<string>();   // 검사일시(YYYYMMDDHHMM)
        public List<string> MRI_RD_SDR_DCT_YN = new List<string>(); // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
        public List<string> MRI_DCT_RST_DD = new List<string>();    // 판독 완료일(YYYYMMDD)

        // MRI 조영제
        public string MRI_CTRST_USE_YN;             // 조영제 사용(1:Yes, 2:No)
        public string MRI_CTRST_PTNT_ASM_RCD_YN;    // 환자 평가 기록 유무(1:Yes, 2:No)
        public string MRI_CTRST_PTNT_ASM_RCD_DD;    // 환자 평가 기록 일자(YYYYMMDD)
        public string MRI_KDNY_FCLT_EXM_YN;         // 신장기능검사 유무(1:Yes, 2:No)

        // MRI 전 환자평가
        public string BF_MRI_PTNT_ASM_RCD_YN;       // MRI전 환자 평가 기록 유무(1:Yes, 2:No)
        public string BF_MRI_PTNT_ASM_RCD_DD;       // MRI전 환자 평가 기록 일자(YYYYMMDD)

        // --- E. PET 검사 ---
        // PET 검사정보(반복)
        public List<string> PET_MDFEE_CD = new List<string>();      // 수가코드
        public List<string> PET_MDFEE_CD_NM = new List<string>();   // 검사명
        public List<string> PET_EXM_EXEC_DT = new List<string>();   // 검사일시(YYYYMMDDHHMM)

        // 방사성 동위원소
        public string FDG_INJC_QTY_RCD_YN;      // F-18 FDG 투여량 기록 유무(1:Yes, 2:No)
        public string FDG_TOT_INJC_QTY;         // F-18 FDG 투여량(소수점 첫째자리)
        public string FDG_UNIT;                 // 단위(1:MBq, 2:mCi)
        public string HEIG;                     // 키(cm, 소수점 첫째자리)
        public string BWGT;                     // 몸무게(kg, 소수점 첫째자리)

        // --- F. 기타 사항 ---
        //public string APND_DATA_NO;             // 첨부

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // --- A. 환자 정보 확인 ---
            IPAT_OPAT_TP_CD = ""; // 청구유형(1:입원, 2:외래)
            IPAT_DD = "";         // 입원일(입원인 경우, YYYYMMDD)
            EMRRM_PASS_YN = "";   // 응급실을 통한 입원 여부(1:Yes, 2:No)
            EMRRM_VST_DD = "";    // 응급실 내원일(YYYYMMDD, 응급실입원 Yes일 때)
            DSCG_DD = "";         // 퇴원일(입원인 경우, YYYYMMDD)
            DIAG_DD = "";         // 내원일(외래인 경우, YYYYMMDD)

            // --- B. 검사정보 ---
            IMG_EXM_KND_CD = ""; // 검사종류(1:CT, 2:MRI, 3:PET, 다중선택시 "/"로 구분)
            PET_KND_CD = "";     // PET 종류(1:PET-CT, 2:PET-MRI, 3:PET, PET 선택시)

            // --- C. CT 검사 ---
            CT_DIAG_TY_CD = "";        // CT 진료유형(1:응급실, 2:입원, 3:외래)
            CT_PGP_INTNT_TP_CD = "";   // CT 촬영목적(1:방사선 치료범위 결정, 2:이 외 진단/치료)
            CT_MDFEE_CD.Clear();       // 수가코드
            CT_MDFEE_CD_NM.Clear();    // 검사명
            CT_EXM_EXEC_DT.Clear();    // 검사일시(YYYYMMDDHHMM)
            CT_RD_SDR_DCT_YN.Clear();  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
            CT_DCT_RST_DD.Clear();     // 판독 완료일(YYYYMMDD)
            CT_CTRST_USE_YN = "";              // 조영제 사용(1:Yes, 2:No)
            CT_CTRST_MDCT_PTH_CD = "";         // 조영제 투약 경로(1:혈관 내 주사, 2:그 외)
            CT_CTRST_PTNT_ASM_RCD_YN = "";     // 환자 평가 기록 유무(1:Yes, 2:No)
            CT_CTRST_PTNT_ASM_RCD_DD = "";     // 환자 평가 기록 일자(YYYYMMDD)
            CT_KDNY_FCLT_EXM_YN = "";          // 신장기능검사 유무(1:Yes, 2:No)

            // --- D. MRI 검사 ---
            MRI_DIAG_TY_CD = "";        // MRI 진료유형(1:응급실, 2:입원, 3:외래)
            MRI_MDFEE_CD.Clear();       // 수가코드
            MRI_MDFEE_CD_NM.Clear();    // 검사명
            MRI_EXM_EXEC_DT.Clear();    // 검사일시(YYYYMMDDHHMM)
            MRI_RD_SDR_DCT_YN.Clear();  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
            MRI_DCT_RST_DD.Clear();     // 판독 완료일(YYYYMMDD)
            MRI_CTRST_USE_YN = "";              // 조영제 사용(1:Yes, 2:No)
            MRI_CTRST_PTNT_ASM_RCD_YN = "";     // 환자 평가 기록 유무(1:Yes, 2:No)
            MRI_CTRST_PTNT_ASM_RCD_DD = "";     // 환자 평가 기록 일자(YYYYMMDD)
            MRI_KDNY_FCLT_EXM_YN = "";          // 신장기능검사 유무(1:Yes, 2:No)
            BF_MRI_PTNT_ASM_RCD_YN = "";        // MRI전 환자 평가 기록 유무(1:Yes, 2:No)
            BF_MRI_PTNT_ASM_RCD_DD = "";        // MRI전 환자 평가 기록 일자(YYYYMMDD)

            // --- E. PET 검사 ---
            PET_MDFEE_CD.Clear();       // 수가코드
            PET_MDFEE_CD_NM.Clear();    // 검사명
            PET_EXM_EXEC_DT.Clear();    // 검사일시(YYYYMMDDHHMM)
            FDG_INJC_QTY_RCD_YN = "";   // F-18 FDG 투여량 기록 유무(1:Yes, 2:No)
            FDG_TOT_INJC_QTY = "";      // F-18 FDG 투여량(소수점 첫째자리)
            FDG_UNIT = "";              // 단위(1:MBq, 2:mCi)
            HEIG = "";                  // 키(cm, 소수점 첫째자리)
            BWGT = "";                  // 몸무게(kg, 소수점 첫째자리)

            // --- F. 기타 사항 ---
            //APND_DATA_NO = "";          // 첨부
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // --- A. 환자 정보 확인 및 B~F 단일값 ---
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM049";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                IPAT_OPAT_TP_CD = reader["IPAT_OPAT_TP_CD"].ToString(); // 청구유형(1:입원, 2:외래)
                IPAT_DD = reader["IPAT_DD"].ToString(); // 입원일
                EMRRM_PASS_YN = reader["EMRRM_PASS_YN"].ToString(); // 응급실을 통한 입원 여부
                EMRRM_VST_DD = reader["EMRRM_VST_DD"].ToString(); // 응급실 내원일
                DSCG_DD = reader["DSCG_DD"].ToString(); // 퇴원일
                DIAG_DD = reader["DIAG_DD"].ToString(); // 내원일

                IMG_EXM_KND_CD = reader["IMG_EXM_KND_CD"].ToString(); // 검사종류
                PET_KND_CD = reader["PET_KND_CD"].ToString(); // PET 종류

                CT_DIAG_TY_CD = reader["CT_DIAG_TY_CD"].ToString(); // CT 진료유형
                CT_PGP_INTNT_TP_CD = reader["CT_PGP_INTNT_TP_CD"].ToString(); // CT 촬영목적

                CT_CTRST_USE_YN = reader["CT_CTRST_USE_YN"].ToString(); // CT 조영제 사용
                CT_CTRST_MDCT_PTH_CD = reader["CT_CTRST_MDCT_PTH_CD"].ToString(); // CT 조영제 투약 경로
                CT_CTRST_PTNT_ASM_RCD_YN = reader["CT_CTRST_PTNT_ASM_RCD_YN"].ToString(); // CT 환자 평가 기록 유무
                CT_CTRST_PTNT_ASM_RCD_DD = reader["CT_CTRST_PTNT_ASM_RCD_DD"].ToString(); // CT 환자 평가 기록 일자
                CT_KDNY_FCLT_EXM_YN = reader["CT_KDNY_FCLT_EXM_YN"].ToString(); // CT 신장기능검사 유무

                MRI_DIAG_TY_CD = reader["MRI_DIAG_TY_CD"].ToString(); // MRI 진료유형
                MRI_CTRST_USE_YN = reader["MRI_CTRST_USE_YN"].ToString(); // MRI 조영제 사용
                MRI_CTRST_PTNT_ASM_RCD_YN = reader["MRI_CTRST_PTNT_ASM_RCD_YN"].ToString(); // MRI 환자 평가 기록 유무
                MRI_CTRST_PTNT_ASM_RCD_DD = reader["MRI_CTRST_PTNT_ASM_RCD_DD"].ToString(); // MRI 환자 평가 기록 일자
                MRI_KDNY_FCLT_EXM_YN = reader["MRI_KDNY_FCLT_EXM_YN"].ToString(); // MRI 신장기능검사 유무
                BF_MRI_PTNT_ASM_RCD_YN = reader["BF_MRI_PTNT_ASM_RCD_YN"].ToString(); // MRI전 환자 평가 기록 유무
                BF_MRI_PTNT_ASM_RCD_DD = reader["BF_MRI_PTNT_ASM_RCD_DD"].ToString(); // MRI전 환자 평가 기록 일자

                FDG_INJC_QTY_RCD_YN = reader["FDG_INJC_QTY_RCD_YN"].ToString(); // F-18 FDG 투여량 기록 유무
                FDG_TOT_INJC_QTY = reader["FDG_TOT_INJC_QTY"].ToString(); // F-18 FDG 투여량
                FDG_UNIT = reader["FDG_UNIT"].ToString(); // 단위
                HEIG = reader["HEIG"].ToString(); // 키
                BWGT = reader["BWGT"].ToString(); // 몸무게

                return MetroLib.SqlHelper.BREAK;
            });

            // --- C. CT 검사정보(반복) ---
            CT_MDFEE_CD.Clear();
            CT_MDFEE_CD_NM.Clear();
            CT_EXM_EXEC_DT.Clear();
            CT_RD_SDR_DCT_YN.Clear();
            CT_DCT_RST_DD.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM049_CT";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ= '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                CT_MDFEE_CD.Add(reader["MDFEE_CD"].ToString()); // 수가코드
                CT_MDFEE_CD_NM.Add(reader["MDFEE_CD_NM"].ToString()); // 검사명
                CT_EXM_EXEC_DT.Add(reader["EXM_EXEC_DT"].ToString()); // 검사일시(YYYYMMDDHHMM)
                CT_RD_SDR_DCT_YN.Add(reader["RD_SDR_DCT_YN"].ToString()); // 영상의학과 전문의 판독 여부
                CT_DCT_RST_DD.Add(reader["DCT_RST_DD"].ToString()); // 판독 완료일
                return MetroLib.SqlHelper.CONTINUE;
            });

            // --- D. MRI 검사정보(반복) ---
            MRI_MDFEE_CD.Clear();
            MRI_MDFEE_CD_NM.Clear();
            MRI_EXM_EXEC_DT.Clear();
            MRI_RD_SDR_DCT_YN.Clear();
            MRI_DCT_RST_DD.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM049_MRI";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                MRI_MDFEE_CD.Add(reader["MDFEE_CD"].ToString()); // 수가코드
                MRI_MDFEE_CD_NM.Add(reader["MDFEE_CD_NM"].ToString()); // 검사명
                MRI_EXM_EXEC_DT.Add(reader["EXM_EXEC_DT"].ToString()); // 검사일시(YYYYMMDDHHMM)
                MRI_RD_SDR_DCT_YN.Add(reader["RD_SDR_DCT_YN"].ToString()); // 영상의학과 전문의 판독 여부
                MRI_DCT_RST_DD.Add(reader["DCT_RST_DD"].ToString()); // 판독 완료일
                return MetroLib.SqlHelper.CONTINUE;
            });

            // --- E. PET 검사정보(반복) ---
            PET_MDFEE_CD.Clear();
            PET_MDFEE_CD_NM.Clear();
            PET_EXM_EXEC_DT.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM049_PET";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                PET_MDFEE_CD.Add(reader["MDFEE_CD"].ToString()); // 수가코드
                PET_MDFEE_CD_NM.Add(reader["MDFEE_CD_NM"].ToString()); // 검사명
                PET_EXM_EXEC_DT.Add(reader["EXM_EXEC_DT"].ToString()); // 검사일시(YYYYMMDDHHMM)
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
                sql += "DELETE FROM TI84_ASM049 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM049_CT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM049_MRI WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM049_PET WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // --- A~F 단일값 메인 테이블 저장 ---
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM049 (";
            sql += Environment.NewLine + "FORM, KEYSTR, SEQ, VER, IPAT_OPAT_TP_CD, IPAT_DD, EMRRM_PASS_YN, EMRRM_VST_DD, DSCG_DD, DIAG_DD, ";
            sql += Environment.NewLine + "IMG_EXM_KND_CD, PET_KND_CD, ";
            sql += Environment.NewLine + "CT_DIAG_TY_CD, CT_PGP_INTNT_TP_CD, CT_CTRST_USE_YN, CT_CTRST_MDCT_PTH_CD, CT_CTRST_PTNT_ASM_RCD_YN, CT_CTRST_PTNT_ASM_RCD_DD, CT_KDNY_FCLT_EXM_YN, ";
            sql += Environment.NewLine + "MRI_DIAG_TY_CD, MRI_CTRST_USE_YN, MRI_CTRST_PTNT_ASM_RCD_YN, MRI_CTRST_PTNT_ASM_RCD_DD, MRI_KDNY_FCLT_EXM_YN, BF_MRI_PTNT_ASM_RCD_YN, BF_MRI_PTNT_ASM_RCD_DD, ";
            sql += Environment.NewLine + "FDG_INJC_QTY_RCD_YN, FDG_TOT_INJC_QTY, FDG_UNIT, HEIG, BWGT";
            sql += Environment.NewLine + ") VALUES (";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);                       // FORM
            para.Add(KEYSTR);                     // KEYSTR
            para.Add(SEQ);                        // SEQ
            para.Add(ver);                        // VER
            para.Add(IPAT_OPAT_TP_CD);            // 청구유형
            para.Add(IPAT_DD);                    // 입원일
            para.Add(EMRRM_PASS_YN);              // 응급실을 통한 입원 여부
            para.Add(EMRRM_VST_DD);               // 응급실 내원일
            para.Add(DSCG_DD);                    // 퇴원일
            para.Add(DIAG_DD);                    // 내원일
            para.Add(IMG_EXM_KND_CD);             // 검사종류
            para.Add(PET_KND_CD);                 // PET 종류
            para.Add(CT_DIAG_TY_CD);              // CT 진료유형
            para.Add(CT_PGP_INTNT_TP_CD);         // CT 촬영목적
            para.Add(CT_CTRST_USE_YN);            // CT 조영제 사용
            para.Add(CT_CTRST_MDCT_PTH_CD);       // CT 조영제 투약 경로
            para.Add(CT_CTRST_PTNT_ASM_RCD_YN);   // CT 환자 평가 기록 유무
            para.Add(CT_CTRST_PTNT_ASM_RCD_DD);   // CT 환자 평가 기록 일자
            para.Add(CT_KDNY_FCLT_EXM_YN);        // CT 신장기능검사 유무
            para.Add(MRI_DIAG_TY_CD);             // MRI 진료유형
            para.Add(MRI_CTRST_USE_YN);           // MRI 조영제 사용
            para.Add(MRI_CTRST_PTNT_ASM_RCD_YN);  // MRI 환자 평가 기록 유무
            para.Add(MRI_CTRST_PTNT_ASM_RCD_DD);  // MRI 환자 평가 기록 일자
            para.Add(MRI_KDNY_FCLT_EXM_YN);       // MRI 신장기능검사 유무
            para.Add(BF_MRI_PTNT_ASM_RCD_YN);     // MRI전 환자 평가 기록 유무
            para.Add(BF_MRI_PTNT_ASM_RCD_DD);     // MRI전 환자 평가 기록 일자
            para.Add(FDG_INJC_QTY_RCD_YN);        // F-18 FDG 투여량 기록 유무
            para.Add(FDG_TOT_INJC_QTY);           // F-18 FDG 투여량
            para.Add(FDG_UNIT);                   // 단위
            para.Add(HEIG);                       // 키
            para.Add(BWGT);                       // 몸무게

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // --- C. CT 검사정보(반복) ---
            for (int i = 0; i < CT_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM049_CT (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT, RD_SDR_DCT_YN, DCT_RST_DD";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(CT_MDFEE_CD[i]); // 수가코드
                para.Add(CT_MDFEE_CD_NM[i]);
                para.Add(CT_EXM_EXEC_DT[i]);
                para.Add(CT_RD_SDR_DCT_YN[i]);
                para.Add(CT_DCT_RST_DD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- D. MRI 검사정보(반복) ---
            for (int i = 0; i < MRI_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM049_MRI (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT, RD_SDR_DCT_YN, DCT_RST_DD";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(MRI_MDFEE_CD[i]); // 수가코드
                para.Add(MRI_MDFEE_CD_NM[i]);
                para.Add(MRI_EXM_EXEC_DT[i]);
                para.Add(MRI_RD_SDR_DCT_YN[i]);
                para.Add(MRI_DCT_RST_DD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- E. PET 검사정보(반복) ---
            for (int i = 0; i < PET_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM049_PET (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(PET_MDFEE_CD[i]); // 수가코드
                para.Add(PET_MDFEE_CD_NM[i]);
                para.Add(PET_EXM_EXEC_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // --- A~F 단일값 메인 테이블 UPDATE ---
            string sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM049 SET ";
            sql += Environment.NewLine + "IPAT_OPAT_TP_CD=?, IPAT_DD=?, EMRRM_PASS_YN=?, EMRRM_VST_DD=?, DSCG_DD=?, DIAG_DD=?, ";
            sql += Environment.NewLine + "IMG_EXM_KND_CD=?, PET_KND_CD=?, ";
            sql += Environment.NewLine + "CT_DIAG_TY_CD=?, CT_PGP_INTNT_TP_CD=?, CT_CTRST_USE_YN=?, CT_CTRST_MDCT_PTH_CD=?, CT_CTRST_PTNT_ASM_RCD_YN=?, CT_CTRST_PTNT_ASM_RCD_DD=?, CT_KDNY_FCLT_EXM_YN=?, ";
            sql += Environment.NewLine + "MRI_DIAG_TY_CD=?, MRI_CTRST_USE_YN=?, MRI_CTRST_PTNT_ASM_RCD_YN=?, MRI_CTRST_PTNT_ASM_RCD_DD=?, MRI_KDNY_FCLT_EXM_YN=?, BF_MRI_PTNT_ASM_RCD_YN=?, BF_MRI_PTNT_ASM_RCD_DD=?, ";
            sql += Environment.NewLine + "FDG_INJC_QTY_RCD_YN=?, FDG_TOT_INJC_QTY=?, FDG_UNIT=?, HEIG=?, BWGT=? ";
            sql += Environment.NewLine + "WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "  AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "  AND SEQ = '" + SEQ + "'";

            para.Clear();
            para.Add(IPAT_OPAT_TP_CD);           // 청구유형
            para.Add(IPAT_DD);                   // 입원일
            para.Add(EMRRM_PASS_YN);             // 응급실을 통한 입원 여부
            para.Add(EMRRM_VST_DD);              // 응급실 내원일
            para.Add(DSCG_DD);                   // 퇴원일
            para.Add(DIAG_DD);                   // 내원일
            para.Add(IMG_EXM_KND_CD);            // 검사종류
            para.Add(PET_KND_CD);                // PET 종류
            para.Add(CT_DIAG_TY_CD);             // CT 진료유형
            para.Add(CT_PGP_INTNT_TP_CD);        // CT 촬영목적
            para.Add(CT_CTRST_USE_YN);           // CT 조영제 사용
            para.Add(CT_CTRST_MDCT_PTH_CD);      // CT 조영제 투약 경로
            para.Add(CT_CTRST_PTNT_ASM_RCD_YN);  // CT 환자 평가 기록 유무
            para.Add(CT_CTRST_PTNT_ASM_RCD_DD);  // CT 환자 평가 기록 일자
            para.Add(CT_KDNY_FCLT_EXM_YN);       // CT 신장기능검사 유무
            para.Add(MRI_DIAG_TY_CD);            // MRI 진료유형
            para.Add(MRI_CTRST_USE_YN);          // MRI 조영제 사용
            para.Add(MRI_CTRST_PTNT_ASM_RCD_YN); // MRI 환자 평가 기록 유무
            para.Add(MRI_CTRST_PTNT_ASM_RCD_DD); // MRI 환자 평가 기록 일자
            para.Add(MRI_KDNY_FCLT_EXM_YN);      // MRI 신장기능검사 유무
            para.Add(BF_MRI_PTNT_ASM_RCD_YN);    // MRI전 환자 평가 기록 유무
            para.Add(BF_MRI_PTNT_ASM_RCD_DD);    // MRI전 환자 평가 기록 일자
            para.Add(FDG_INJC_QTY_RCD_YN);       // F-18 FDG 투여량 기록 유무
            para.Add(FDG_TOT_INJC_QTY);          // F-18 FDG 투여량
            para.Add(FDG_UNIT);                  // 단위
            para.Add(HEIG);                      // 키
            para.Add(BWGT);                      // 몸무게

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // --- C. CT 검사정보(반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM049_CT WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < CT_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM049_CT (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT, RD_SDR_DCT_YN, DCT_RST_DD";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(CT_MDFEE_CD[i]); // 수가코드
                para.Add(CT_MDFEE_CD_NM[i]);
                para.Add(CT_EXM_EXEC_DT[i]);
                para.Add(CT_RD_SDR_DCT_YN[i]);
                para.Add(CT_DCT_RST_DD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- D. MRI 검사정보(반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM049_MRI WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < MRI_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM049_MRI (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT, RD_SDR_DCT_YN, DCT_RST_DD";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(MRI_MDFEE_CD[i]); // 수가코드
                para.Add(MRI_MDFEE_CD_NM[i]);
                para.Add(MRI_EXM_EXEC_DT[i]);
                para.Add(MRI_RD_SDR_DCT_YN[i]);
                para.Add(MRI_DCT_RST_DD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- E. PET 검사정보(반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM049_PET WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < PET_MDFEE_CD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM049_PET (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, MDFEE_CD, MDFEE_CD_NM, EXM_EXEC_DT";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); // FORM
                para.Add(KEYSTR); // KEYSTR
                para.Add(SEQ); // SEQ
                para.Add(i + 1); // SEQNO
                para.Add(PET_MDFEE_CD[i]); // 수가코드
                para.Add(PET_MDFEE_CD_NM[i]);
                para.Add(PET_EXM_EXEC_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM049 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM049_CT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM049_MRI WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM049_PET WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

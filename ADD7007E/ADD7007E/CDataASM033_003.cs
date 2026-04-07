using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM033_003 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM033"; // 서식코드
        public readonly string ver_id = "003"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "NIC"; // 업무상세코드

        // --- A. 기본 정보 ---
        public string IPAT_DD;              // 입원일(YYYYMMDD)
        public string BIRTH_PTH_CD;         // 최초 입실 경로(1:본원출생, 2:본원출생-퇴원후재입원, 3:타기관출생후전원, 4:타기관출생후외래/응급실입실)
        public string BIRTH_YN;             // 출생일 확인 여부(1:Yes, 2:No)
        public string BIRTH_DT;             // 출생일시(YYYYMMDDHHMM)
        public string BIRTH_PLC_CD;         // 출생장소(1:본원, 2:타기관, 9:기타)
        public string BIRTH_PLC_ETC_TXT;    // 출생장소 기타상세(기타 선택시)
        public string ASM_PARTU_FRM_CD;     // 분만형태(1:자연분만, 2:제왕절개)
        public string FTUS_DEV_TRM;         // 재태기간(주/일, 예: 32/5)
        public string MEMB_YN;              // 다태아여부(1:Yes, 2:No)
        public string MEMB_TXT;             // 다태아 내용(명/번째, 예: 3/1)
        public string NBY_BIRTH_BWGT;       // 출생 시 체중(g)

        // --- B. 입실 및 퇴실 관련 항목 (반복) ---
        // 입실 및 퇴실 관련은 테이블 구조이나, 단일 Row만 입력되는 경우 단일값으로 선언
        public List<string> SPRM_IPAT_DT = new List<string>();         // 입실일시(YYYYMMDDHHMM)
        public List<string> CLTR_ENFC_YN = new List<string>();         // 입실 24시간 이내 감시배양 시행 여부(1:Yes, 2:No)
        public List<string> CLTR_RGN_CD = new List<string>();          // 감시배양 부위(1:비강, 2:겨드랑이, 3:항문, 9:기타)
        public List<string> CLTR_RGN_ETC_TXT = new List<string>();     // 감시배양 부위 기타상세(기타 선택시)
        public List<string> CLTR_ISLTN_CD = new List<string>();        // 감시배양 시행 시 격리여부(1:격리실 1인, 2:코호트, 3:격리 안 함)
        public List<string> INFC_CFR_YN = new List<string>();          // 감염여부(1:Yes, 2:No)
        public List<string> CLTR_AF_ISLTN_CD = new List<string>();     // 감시배양 시행 후 격리여부(1:격리실 1인, 2:코호트, 3:격리 안 함)
        public List<string> CLTR_NEXEC_RS_CD = new List<string>();     // 감시배양 미시행사유(0:해당없음, 1:선천성 감염, 2:선천성 기형)
        public List<string> NBY_IPAT_RS_CD = new List<string>();       // 입실사유(1~9, 다중선택시 "/"로 구분)
        public List<string> RE_IPAT_RS_TXT = new List<string>();       // 입실사유 재입실상세(입실사유 5번 선택시)
        public List<string> IPAT_RS_ETC_TXT = new List<string>();      // 입실사유 기타상세(입실사유 9번 선택시)
        public List<string> DSCG_DT = new List<string>();              // 퇴실일시(YYYYMMDDHHMM)
        public List<string> NBY_DSCG_PTH_CD = new List<string>();      // 퇴실경로(1:퇴원, 2:전실(신생아실/병동), 3:전실(ICU), 4:전원, 5:사망, 6:계속입원)

        // --- C. 진료 관련 항목 ---
        public string SGRD_ASM_ENFC_YN;     // 중증도 평가 시행 여부(1:Yes, 2:No)

        // --- C-1. 중증도 평가 시행 내용 (반복) ---
        public List<string> SGRD_ASM_MASR_DT = new List<string>();      // 측정일시(YYYYMMDDHHMM)
        public List<string> SGRD_ASM_KND_CD = new List<string>();       // 중증도 평가도구 종류(1~7, 9)
        public List<string> SGRD_ASM_KND_ETC_TXT = new List<string>();  // 중증도 평가도구 기타상세(9번 선택시)

        // --- D. 집중영양치료 관련 항목 ---
        public string TPN_ENFC_YN;          // TPN 시행 여부(1:Yes, 2:No)

        // --- D-1. TPN 투여일자 및 협진여부 (반복) ---
        public List<string> INJC_STA_DD = new List<string>();           // 투여 시작일(YYYYMMDD)
        public List<string> INJC_END_DD = new List<string>();           // 투여 종료일(YYYYMMDD)
        public List<string> TPN_DDIAG_YN = new List<string>();          // 집중영양치료팀 협진 여부(1:Yes, 2:No)
        public List<string> DDIAG_NEXEC_RS_TXT = new List<string>();    // 협진 미실시 사유(협진 No 선택시)

        // --- E. 기타 사항 ---
        //public string APND_DATA_NO;         // 첨부

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // --- A. 기본 정보 ---
            IPAT_DD = "";                // 입원일(YYYYMMDD)
            BIRTH_PTH_CD = "";           // 최초 입실 경로(1:본원출생, 2:본원출생-퇴원후재입원, 3:타기관출생후전원, 4:타기관출생후외래/응급실입실)
            BIRTH_YN = "";               // 출생일 확인 여부(1:Yes, 2:No)
            BIRTH_DT = "";               // 출생일시(YYYYMMDDHHMM)
            BIRTH_PLC_CD = "";           // 출생장소(1:본원, 2:타기관, 9:기타)
            BIRTH_PLC_ETC_TXT = "";      // 출생장소 기타상세(기타 선택시)
            ASM_PARTU_FRM_CD = "";       // 분만형태(1:자연분만, 2:제왕절개)
            FTUS_DEV_TRM = "";           // 재태기간(주/일, 예: 32/5)
            MEMB_YN = "";                // 다태아여부(1:Yes, 2:No)
            MEMB_TXT = "";               // 다태아 내용(명/번째, 예: 3/1)
            NBY_BIRTH_BWGT = "";         // 출생 시 체중(g)

            // --- B. 입실 및 퇴실 관련 항목 (반복) ---
            SPRM_IPAT_DT.Clear();        // 입실일시(YYYYMMDDHHMM)
            CLTR_ENFC_YN.Clear();        // 입실 24시간 이내 감시배양 시행 여부(1:Yes, 2:No)
            CLTR_RGN_CD.Clear();         // 감시배양 부위(1:비강, 2:겨드랑이, 3:항문, 9:기타)
            CLTR_RGN_ETC_TXT.Clear();    // 감시배양 부위 기타상세(기타 선택시)
            CLTR_ISLTN_CD.Clear();       // 감시배양 시행 시 격리여부(1:격리실 1인, 2:코호트, 3:격리 안 함)
            INFC_CFR_YN.Clear();         // 감염여부(1:Yes, 2:No)
            CLTR_AF_ISLTN_CD.Clear();    // 감시배양 시행 후 격리여부(1:격리실 1인, 2:코호트, 3:격리 안 함)
            CLTR_NEXEC_RS_CD.Clear();    // 감시배양 미시행사유(0:해당없음, 1:선천성 감염, 2:선천성 기형)
            NBY_IPAT_RS_CD.Clear();      // 입실사유(1~9, 다중선택시 "/"로 구분)
            RE_IPAT_RS_TXT.Clear();      // 입실사유 재입실상세(입실사유 5번 선택시)
            IPAT_RS_ETC_TXT.Clear();     // 입실사유 기타상세(입실사유 9번 선택시)
            DSCG_DT.Clear();             // 퇴실일시(YYYYMMDDHHMM)
            NBY_DSCG_PTH_CD.Clear();     // 퇴실경로(1:퇴원, 2:전실(신생아실/병동), 3:전실(ICU), 4:전원, 5:사망, 6:계속입원)

            // --- C. 진료 관련 항목 ---
            SGRD_ASM_ENFC_YN = "";       // 중증도 평가 시행 여부(1:Yes, 2:No)

            // --- C-1. 중증도 평가 시행 내용 (반복) ---
            SGRD_ASM_MASR_DT.Clear();    // 측정일시(YYYYMMDDHHMM)
            SGRD_ASM_KND_CD.Clear();     // 중증도 평가도구 종류(1~7, 9)
            SGRD_ASM_KND_ETC_TXT.Clear();// 중증도 평가도구 기타상세(9번 선택시)

            // --- D. 집중영양치료 관련 항목 ---
            TPN_ENFC_YN = "";            // TPN 시행 여부(1:Yes, 2:No)

            // --- D-1. TPN 투여일자 및 협진여부 (반복) ---
            INJC_STA_DD.Clear();         // 투여 시작일(YYYYMMDD)
            INJC_END_DD.Clear();         // 투여 종료일(YYYYMMDD)
            TPN_DDIAG_YN.Clear();        // 집중영양치료팀 협진 여부(1:Yes, 2:No)
            DDIAG_NEXEC_RS_TXT.Clear();  // 협진 미실시 사유(협진 No 선택시)

            // --- E. 기타 사항 ---
            //APND_DATA_NO = "";           // 첨부
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // --- A. 기본 정보 ---
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM033";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                IPAT_DD = reader["IPAT_DD"].ToString();                 // 입원일(YYYYMMDD)
                BIRTH_PTH_CD = reader["BIRTH_PTH_CD"].ToString();       // 최초 입실 경로
                BIRTH_YN = reader["BIRTH_YN"].ToString();               // 출생일 확인 여부(1:Yes, 2:No)
                BIRTH_DT = reader["BIRTH_DT"].ToString();               // 출생일시(YYYYMMDDHHMM)
                BIRTH_PLC_CD = reader["BIRTH_PLC_CD"].ToString();       // 출생장소
                BIRTH_PLC_ETC_TXT = reader["BIRTH_PLC_ETC_TXT"].ToString(); // 출생장소 기타상세
                ASM_PARTU_FRM_CD = reader["ASM_PARTU_FRM_CD"].ToString();   // 분만형태
                FTUS_DEV_TRM = reader["FTUS_DEV_TRM"].ToString();       // 재태기간(주/일)
                MEMB_YN = reader["MEMB_YN"].ToString();                 // 다태아여부
                MEMB_TXT = reader["MEMB_TXT"].ToString();               // 다태아 내용
                NBY_BIRTH_BWGT = reader["NBY_BIRTH_BWGT"].ToString();   // 출생 시 체중(g)
                SGRD_ASM_ENFC_YN = reader["SGRD_ASM_ENFC_YN"].ToString(); // 중증도 평가 시행 여부
                TPN_ENFC_YN = reader["TPN_ENFC_YN"].ToString();         // TPN 시행 여부
                return MetroLib.SqlHelper.BREAK;
            });

            // --- B. 입실 및 퇴실 관련 항목 (반복) ---
            SPRM_IPAT_DT.Clear();
            CLTR_ENFC_YN.Clear();
            CLTR_RGN_CD.Clear();
            CLTR_RGN_ETC_TXT.Clear();
            CLTR_ISLTN_CD.Clear();
            INFC_CFR_YN.Clear();
            CLTR_AF_ISLTN_CD.Clear();
            CLTR_NEXEC_RS_CD.Clear();
            NBY_IPAT_RS_CD.Clear();
            RE_IPAT_RS_TXT.Clear();
            IPAT_RS_ETC_TXT.Clear();
            DSCG_DT.Clear();
            NBY_DSCG_PTH_CD.Clear();

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM033_IPAT";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                SPRM_IPAT_DT.Add(reader["SPRM_IPAT_DT"].ToString());           // 입실일시
                CLTR_ENFC_YN.Add(reader["CLTR_ENFC_YN"].ToString());           // 입실 24시간 이내 감시배양 시행 여부
                CLTR_RGN_CD.Add(reader["CLTR_RGN_CD"].ToString());             // 감시배양 부위
                CLTR_RGN_ETC_TXT.Add(reader["CLTR_RGN_ETC_TXT"].ToString());   // 감시배양 부위 기타상세
                CLTR_ISLTN_CD.Add(reader["CLTR_ISLTN_CD"].ToString());         // 감시배양 시행 시 격리여부
                INFC_CFR_YN.Add(reader["INFC_CFR_YN"].ToString());             // 감염여부
                CLTR_AF_ISLTN_CD.Add(reader["CLTR_AF_ISLTN_CD"].ToString());   // 감시배양 시행 후 격리여부
                CLTR_NEXEC_RS_CD.Add(reader["CLTR_NEXEC_RS_CD"].ToString());   // 감시배양 미시행사유
                NBY_IPAT_RS_CD.Add(reader["NBY_IPAT_RS_CD"].ToString());       // 입실사유
                RE_IPAT_RS_TXT.Add(reader["RE_IPAT_RS_TXT"].ToString());       // 입실사유 재입실상세
                IPAT_RS_ETC_TXT.Add(reader["IPAT_RS_ETC_TXT"].ToString());     // 입실사유 기타상세
                DSCG_DT.Add(reader["DSCG_DT"].ToString());                     // 퇴실일시
                NBY_DSCG_PTH_CD.Add(reader["NBY_DSCG_PTH_CD"].ToString());     // 퇴실경로
                return MetroLib.SqlHelper.CONTINUE;
            });

            // --- C-1. 중증도 평가 시행 내용 (반복) ---
            SGRD_ASM_MASR_DT.Clear();
            SGRD_ASM_KND_CD.Clear();
            SGRD_ASM_KND_ETC_TXT.Clear();

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM033_SGRD";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                SGRD_ASM_MASR_DT.Add(reader["SGRD_ASM_MASR_DT"].ToString());         // 측정일시
                SGRD_ASM_KND_CD.Add(reader["SGRD_ASM_KND_CD"].ToString());           // 중증도 평가도구 종류
                SGRD_ASM_KND_ETC_TXT.Add(reader["SGRD_ASM_KND_ETC_TXT"].ToString()); // 중증도 평가도구 기타상세
                return MetroLib.SqlHelper.CONTINUE;
            });

            // --- D-1. TPN 투여일자 및 협진여부 (반복) ---
            INJC_STA_DD.Clear();
            INJC_END_DD.Clear();
            TPN_DDIAG_YN.Clear();
            DDIAG_NEXEC_RS_TXT.Clear();

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM033_TPN";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                INJC_STA_DD.Add(reader["INJC_STA_DD"].ToString());           // 투여 시작일
                INJC_END_DD.Add(reader["INJC_END_DD"].ToString());           // 투여 종료일
                TPN_DDIAG_YN.Add(reader["TPN_DDIAG_YN"].ToString());         // 집중영양치료팀 협진 여부
                DDIAG_NEXEC_RS_TXT.Add(reader["DDIAG_NEXEC_RS_TXT"].ToString()); // 협진 미실시 사유
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
                sql += "DELETE FROM TI84_ASM033 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM033_IPAT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM033_SGRD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM033_TPN WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // --- A. 기본 정보 및 C, D, E 단일값 메인 테이블 저장 ---
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM033 (";
            sql += Environment.NewLine + "FORM, KEYSTR, SEQ, VER, IPAT_DD, BIRTH_PTH_CD, BIRTH_YN, BIRTH_DT, BIRTH_PLC_CD, BIRTH_PLC_ETC_TXT, ASM_PARTU_FRM_CD, ";
            sql += Environment.NewLine + "FTUS_DEV_TRM, MEMB_YN, MEMB_TXT, NBY_BIRTH_BWGT, SGRD_ASM_ENFC_YN, TPN_ENFC_YN";
            sql += Environment.NewLine + ") VALUES (";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);                   // FORM
            para.Add(KEYSTR);                 // KEYSTR
            para.Add(SEQ);                    // SEQ
            para.Add(ver);                    // VER
            para.Add(IPAT_DD);                // 입원일(YYYYMMDD)
            para.Add(BIRTH_PTH_CD);           // 최초 입실 경로
            para.Add(BIRTH_YN);               // 출생일 확인 여부
            para.Add(BIRTH_DT);               // 출생일시(YYYYMMDDHHMM)
            para.Add(BIRTH_PLC_CD);           // 출생장소
            para.Add(BIRTH_PLC_ETC_TXT);      // 출생장소 기타상세
            para.Add(ASM_PARTU_FRM_CD);       // 분만형태
            para.Add(FTUS_DEV_TRM);           // 재태기간(주/일)
            para.Add(MEMB_YN);                // 다태아여부
            para.Add(MEMB_TXT);               // 다태아 내용
            para.Add(NBY_BIRTH_BWGT);         // 출생 시 체중(g)
            para.Add(SGRD_ASM_ENFC_YN);       // 중증도 평가 시행 여부
            para.Add(TPN_ENFC_YN);            // TPN 시행 여부

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // --- B. 입실 및 퇴실 관련 항목 (반복) 상세 테이블 저장 ---
            for (int i = 0; i < SPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM033_IPAT (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, SPRM_IPAT_DT, CLTR_ENFC_YN, CLTR_RGN_CD, CLTR_RGN_ETC_TXT, CLTR_ISLTN_CD, INFC_CFR_YN, ";
                sql += Environment.NewLine + "CLTR_AF_ISLTN_CD, CLTR_NEXEC_RS_CD, NBY_IPAT_RS_CD, RE_IPAT_RS_TXT, IPAT_RS_ETC_TXT, DSCG_DT, NBY_DSCG_PTH_CD";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);                                         // FORM
                para.Add(KEYSTR);                                       // KEYSTR
                para.Add(SEQ);                                          // SEQ
                para.Add(i + 1);                                        // SEQNO
                para.Add(SPRM_IPAT_DT[i]);                              // 입실일시
                para.Add(CLTR_ENFC_YN[i]);                // 감시배양 시행 여부
                para.Add(CLTR_RGN_CD[i]);                  // 감시배양 부위
                para.Add(CLTR_RGN_ETC_TXT[i]);        // 감시배양 부위 기타상세
                para.Add(CLTR_ISLTN_CD[i]);              // 감시배양 시행 시 격리여부
                para.Add(INFC_CFR_YN[i]);                  // 감염여부
                para.Add(CLTR_AF_ISLTN_CD[i]);        // 감시배양 시행 후 격리여부
                para.Add(CLTR_NEXEC_RS_CD[i]);        // 감시배양 미시행사유
                para.Add(NBY_IPAT_RS_CD[i]);            // 입실사유
                para.Add(RE_IPAT_RS_TXT[i]);            // 입실사유 재입실상세
                para.Add(IPAT_RS_ETC_TXT[i]);          // 입실사유 기타상세
                para.Add(DSCG_DT[i]);                          // 퇴실일시
                para.Add(NBY_DSCG_PTH_CD[i]);          // 퇴실경로
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- C-1. 중증도 평가 시행 내용 (반복) 상세 테이블 저장 ---
            for (int i = 0; i < SGRD_ASM_MASR_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM033_SGRD (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, SGRD_ASM_MASR_DT, SGRD_ASM_KND_CD, SGRD_ASM_KND_ETC_TXT";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);                                         // FORM
                para.Add(KEYSTR);                                       // KEYSTR
                para.Add(SEQ);                                          // SEQ
                para.Add(i + 1);                                        // SEQNO
                para.Add(SGRD_ASM_MASR_DT[i]);                          // 측정일시
                para.Add(SGRD_ASM_KND_CD[i]);               // 평가도구 종류
                para.Add(SGRD_ASM_KND_ETC_TXT[i]);     // 평가도구 기타상세
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- D-1. TPN 투여일자 및 협진여부 (반복) 상세 테이블 저장 ---
            for (int i = 0; i < INJC_STA_DD.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM033_TPN (";
                sql += Environment.NewLine + "FORM, KEYSTR, SEQ, SEQNO, INJC_STA_DD, INJC_END_DD, TPN_DDIAG_YN, DDIAG_NEXEC_RS_TXT";
                sql += Environment.NewLine + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);                                         // FORM
                para.Add(KEYSTR);                                       // KEYSTR
                para.Add(SEQ);                                          // SEQ
                para.Add(i + 1);                                        // SEQNO
                para.Add(INJC_STA_DD[i]);                               // 투여 시작일
                para.Add(INJC_END_DD[i]);                  // 투여 종료일
                para.Add(TPN_DDIAG_YN[i]);                // 집중영양치료팀 협진 여부
                para.Add(DDIAG_NEXEC_RS_TXT[i]);    // 협진 미실시 사유
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // --- A. 기본 정보 및 C, D, E 단일값 메인 테이블 UPDATE ---
            string sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM033 SET ";
            sql += Environment.NewLine + "IPAT_DD=?, BIRTH_PTH_CD=?, BIRTH_YN=?, BIRTH_DT=?, BIRTH_PLC_CD=?, BIRTH_PLC_ETC_TXT=?, ASM_PARTU_FRM_CD=?, ";
            sql += Environment.NewLine + "FTUS_DEV_TRM=?, MEMB_YN=?, MEMB_TXT=?, NBY_BIRTH_BWGT=?, SGRD_ASM_ENFC_YN=?, TPN_ENFC_YN=? ";
            sql += Environment.NewLine + "WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "  AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "  AND SEQ = '" + SEQ + "'";

            para.Clear();
            para.Add(IPAT_DD);                // 입원일(YYYYMMDD)
            para.Add(BIRTH_PTH_CD);           // 최초 입실 경로
            para.Add(BIRTH_YN);               // 출생일 확인 여부
            para.Add(BIRTH_DT);               // 출생일시(YYYYMMDDHHMM)
            para.Add(BIRTH_PLC_CD);           // 출생장소
            para.Add(BIRTH_PLC_ETC_TXT);      // 출생장소 기타상세
            para.Add(ASM_PARTU_FRM_CD);       // 분만형태
            para.Add(FTUS_DEV_TRM);           // 재태기간(주/일)
            para.Add(MEMB_YN);                // 다태아여부
            para.Add(MEMB_TXT);               // 다태아 내용
            para.Add(NBY_BIRTH_BWGT);         // 출생 시 체중(g)
            para.Add(SGRD_ASM_ENFC_YN);       // 중증도 평가 시행 여부
            para.Add(TPN_ENFC_YN);            // TPN 시행 여부

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // --- B. 입실 및 퇴실 관련 항목 (반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM033_IPAT WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            for (int i = 0; i < SPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM033_IPAT (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, SPRM_IPAT_DT, CLTR_ENFC_YN, CLTR_RGN_CD, CLTR_RGN_ETC_TXT, CLTR_ISLTN_CD, INFC_CFR_YN, ";
                sql += "CLTR_AF_ISLTN_CD, CLTR_NEXEC_RS_CD, NBY_IPAT_RS_CD, RE_IPAT_RS_TXT, IPAT_RS_ETC_TXT, DSCG_DT, NBY_DSCG_PTH_CD";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SPRM_IPAT_DT[i]);
                para.Add(CLTR_ENFC_YN[i]);
                para.Add(CLTR_RGN_CD[i]);
                para.Add(CLTR_RGN_ETC_TXT[i]);
                para.Add(CLTR_ISLTN_CD[i]);
                para.Add(INFC_CFR_YN[i]);
                para.Add(CLTR_AF_ISLTN_CD[i]);
                para.Add(CLTR_NEXEC_RS_CD[i]);
                para.Add(NBY_IPAT_RS_CD[i]);
                para.Add(RE_IPAT_RS_TXT[i]);
                para.Add(IPAT_RS_ETC_TXT[i]);
                para.Add(DSCG_DT[i]);
                para.Add(NBY_DSCG_PTH_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- C-1. 중증도 평가 시행 내용 (반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM033_SGRD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            for (int i = 0; i < SGRD_ASM_MASR_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM033_SGRD (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, SGRD_ASM_MASR_DT, SGRD_ASM_KND_CD, SGRD_ASM_KND_ETC_TXT";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SGRD_ASM_MASR_DT[i]);
                para.Add(SGRD_ASM_KND_CD[i]);
                para.Add(SGRD_ASM_KND_ETC_TXT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // --- D-1. TPN 투여일자 및 협진여부 (반복) 상세 테이블 삭제 후 재삽입 ---
            sql = "DELETE FROM TI84_ASM033_TPN WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            for (int i = 0; i < INJC_STA_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM033_TPN (";
                sql += "FORM, KEYSTR, SEQ, SEQNO, INJC_STA_DD, INJC_END_DD, TPN_DDIAG_YN, DDIAG_NEXEC_RS_TXT";
                sql += ") VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(INJC_STA_DD[i]);
                para.Add(INJC_END_DD[i]);
                para.Add(TPN_DDIAG_YN[i]);
                para.Add(DDIAG_NEXEC_RS_TXT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM033 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM033_IPAT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM033_SGRD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM033_TPN WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

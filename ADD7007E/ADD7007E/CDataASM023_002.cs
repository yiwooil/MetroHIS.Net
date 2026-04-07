using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM023_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM023"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "CAP"; // 업무상세코드

        // A. 기본 정보
        public string ASM_HOSP_ARIV_DT;    // 병원도착일시 (CCYYMMDDHHMM)
        public string DSCG_YN;             // 퇴원 여부 (1:Yes, 2:No)
        public string ASM_DSCG_DT;         // 퇴원일시 (퇴원 Yes일 경우)
        public string IPAT_TRM_PNEM_SICK_YN; // 입원기간 내 폐렴 상병 유무
        public string DSCG_STAT_RCD_YN;    // 퇴원상태 기록 여부
        public string DSCG_STAT_CD;        // 퇴원상태 코드 (1~5)

        // 입원현황
        public string RECU_HOSP_VST_YN;    // 요양원 등 시설 내원 여부
        public string RECU_HOSP_NM;        // 시설 명칭
        public string ASM_VST_PTH_CD;      // 내원경로 (1:직접, 2:타병원, 3:기록없음)

        // 지역사회획득 폐렴 관련성
        public string ASM_PLC_SCTY_OBTN_PNEM_CD2; // 관련성 코드 (00~14)
        public string PLC_SCTY_OBTN_PNEM_TXT;     // 관련성 기타 상세사유

        // B. 검사 정보
        public string ASM_EXM_YN;          // 산소포화도 검사 시행 여부
        public List<string> ASM_EXM_DT = new List<string>();  // 산소포화도 검사일시(복수입력 가능)
        public List<string> ASM_OXY_STRT = new List<string>(); // 산소포화도 결과(복수입력)

        public string ASM_PRSC_YN;         // 객담배양 검사 처방 여부
        public List<string> ASM_PRSC_DT = new List<string>(); // 객담배양 검사 처방일시 (복수입력)

        public string ASM_GAT_YN;          // 혈액배양검사 채취 여부
        public List<string> ASM_GAT_DT = new List<string>();  // 혈액배양검사 채취일시 (복수입력)
        public string ST1_ANBO_INJC_BF_GAT_YN; // 첫 항생제 투여 전 검사 채취 여부
        public string ANBO_CHG_BF_GAT_YN;  // 항생제 변경 전 검사 채취 여부
        public string ANBO_CHG_BF_GAT_RS_CD; // 항생제 변경 전 검사 채취 사유

        // C. 중증도 판정도구
        public string ASM_USE_YN;          // 중증도 판정도구 사용 여부
        public List<string> ASM_USE_DT = new List<string>();   // 사용일시 (복수입력)
        public List<string> ASM_SGRD_JDGM_TL_KND_CD = new List<string>(); // 판정도구 종류 코드
        public List<string> ASM_SGRD_JDGM_TL_TOT_PNT = new List<string>(); // 판정 점수
        public string CNFS_YN;             // 혼돈 유무 (1:Yes, 2:No)
        public string BLUR_UNIT;           // BUN 단위 (1:mg/dL, 2:mmol/L)
        public string BLUR_MG_CNT;         // BUN 수치 (mg/dL 단위)
        public string BLUR_MMOL_CNT;       // BUN 수치 (mmol/L 단위)
        public string BRT;                 // 호흡수 (회/분)
        public string BPRSU;               // 혈압 (예: 120/80)

        // D. 투약 정보

        public string ANBO_USE_YN;         // 정맥내 항생제 투여 여부
        public List<string> MDS_INJC_DT = new List<string>(); // 투여일시(복수입력)
        public List<string> MDS_CD = new List<string>();      // 약품코드(복수입력)
        public List<string> MDS_NM = new List<string>();      // 약품명(복수입력)

        // E. 첨부
        //public string APND_DATA_NO;        // 첨부파일 데이터 번호 (선택)

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본 정보
            ASM_HOSP_ARIV_DT = "";             // 병원도착일시
            DSCG_YN = "";                       // 퇴원 여부
            ASM_DSCG_DT = "";                  // 퇴원일시
            IPAT_TRM_PNEM_SICK_YN = "";        // 입원기간 내 폐렴 상병 유무
            DSCG_STAT_RCD_YN = "";             // 퇴원상태 기록 여부
            DSCG_STAT_CD = "";                 // 퇴원상태 코드

            // 입원현황
            RECU_HOSP_VST_YN = "";             // 요양원 등 시설 내원 여부
            RECU_HOSP_NM = "";                 // 시설 명칭
            ASM_VST_PTH_CD = "";               // 내원경로

            // 지역사회획득 폐렴 관련성
            ASM_PLC_SCTY_OBTN_PNEM_CD2 = "";   // 관련성 코드
            PLC_SCTY_OBTN_PNEM_TXT = "";       // 기타 상세사유

            // B. 검사 정보
            ASM_EXM_YN = "";                   // 산소포화도 검사 여부
            ASM_EXM_DT.Clear();               // 산소포화도 검사일시
            ASM_OXY_STRT.Clear();             // 산소포화도 결과

            ASM_PRSC_YN = "";
            ASM_PRSC_DT.Clear();              // 객담배양 검사 처방일시

            ASM_GAT_YN = "";                  // 혈액배양 채취 여부
            ASM_GAT_DT.Clear();              // 혈액배양 채취일시
            ST1_ANBO_INJC_BF_GAT_YN = "";    // 첫 항생제 투여 전 혈액배양 채취 여부
            ANBO_CHG_BF_GAT_YN = "";         // 항생제 변경 전 검사 채취 여부
            ANBO_CHG_BF_GAT_RS_CD = "";      // 검사 채취 사유

            // C. 중증도 판정도구
            ASM_USE_YN = "";                  // 판정도구 사용 여부
            ASM_USE_DT.Clear();              // 사용일시
            ASM_SGRD_JDGM_TL_KND_CD.Clear(); // 판정도구 종류
            ASM_SGRD_JDGM_TL_TOT_PNT.Clear(); // 총점

            CNFS_YN = "";                     // 혼돈 유무
            BLUR_UNIT = "";                  // BUN 단위
            BLUR_MG_CNT = "";                // BUN 수치 (mg/dL)
            BLUR_MMOL_CNT = "";              // BUN 수치 (mmol/L)
            BRT = "";                        // 호흡수
            BPRSU = "";                      // 혈압

            // D. 투약 정보
            ANBO_USE_YN = "";
            MDS_INJC_DT.Clear();             // 항생제 투여일시
            MDS_CD.Clear();                  // 약품코드
            MDS_NM.Clear();                  // 약품명

            // E. 첨부
            //APND_DATA_NO = "";              // 첨부파일 (선택사항, 주석 처리 상태 유지)
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // 단일 항목: TI84_ASM023
            string sql = "";
            sql += "SELECT * FROM TI84_ASM023 ";
            sql += "WHERE FORM = '" + form + "' ";
            sql += "AND KEYSTR = '" + KEYSTR + "' ";
            sql += "AND SEQ = '" + SEQ + "' ";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_HOSP_ARIV_DT = reader["ASM_HOSP_ARIV_DT"].ToString();
                DSCG_YN = reader["DSCG_YN"].ToString();
                ASM_DSCG_DT = reader["ASM_DSCG_DT"].ToString();
                IPAT_TRM_PNEM_SICK_YN = reader["IPAT_TRM_PNEM_SICK_YN"].ToString();
                DSCG_STAT_RCD_YN = reader["DSCG_STAT_RCD_YN"].ToString();
                DSCG_STAT_CD = reader["DSCG_STAT_CD"].ToString();
                RECU_HOSP_VST_YN = reader["RECU_HOSP_VST_YN"].ToString();
                RECU_HOSP_NM = reader["RECU_HOSP_NM"].ToString();
                ASM_VST_PTH_CD = reader["ASM_VST_PTH_CD"].ToString();
                ASM_PLC_SCTY_OBTN_PNEM_CD2 = reader["ASM_PLC_SCTY_OBTN_PNEM_CD2"].ToString();
                PLC_SCTY_OBTN_PNEM_TXT = reader["PLC_SCTY_OBTN_PNEM_TXT"].ToString();

                ASM_EXM_YN = reader["ASM_EXM_YN"].ToString();
                ASM_PRSC_YN = reader["ASM_PRSC_YN"].ToString();
                ASM_GAT_YN = reader["ASM_GAT_YN"].ToString();
                ST1_ANBO_INJC_BF_GAT_YN = reader["ST1_ANBO_INJC_BF_GAT_YN"].ToString();
                ANBO_CHG_BF_GAT_YN = reader["ANBO_CHG_BF_GAT_YN"].ToString();
                ANBO_CHG_BF_GAT_RS_CD = reader["ANBO_CHG_BF_GAT_RS_CD"].ToString();

                ASM_USE_YN = reader["ASM_USE_YN"].ToString();
                CNFS_YN = reader["CNFS_YN"].ToString();

                BLUR_UNIT = reader["BLUR_UNIT"].ToString();
                BLUR_MG_CNT = reader["BLUR_MG_CNT"].ToString();
                BLUR_MMOL_CNT = reader["BLUR_MMOL_CNT"].ToString();
                BRT = reader["BRT"].ToString();
                BPRSU = reader["BPRSU"].ToString();

                ANBO_USE_YN = reader["ANBO_USE_YN"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // 산소포화도(복수)
            sql = "SELECT * FROM TI84_ASM023_OXY ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "' ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_EXM_DT.Add(reader["ASM_EXM_DT"].ToString());
                ASM_OXY_STRT.Add(reader["ASM_OXY_STRT"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 객담배양검사(복수)
            sql = "SELECT * FROM TI84_ASM023_SPTUM ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "' ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_PRSC_DT.Add(reader["ASM_PRSC_DT"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 혈액배양검사(복수)
            sql = "SELECT * FROM TI84_ASM023_BLD ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "' ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_GAT_DT.Add(reader["ASM_GAT_DT"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 중증도 판정도구(복수)
            sql = "SELECT * FROM TI84_ASM023_SGRD ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "' ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_USE_DT.Add(reader["ASM_USE_DT"].ToString());
                ASM_SGRD_JDGM_TL_KND_CD.Add(reader["ASM_SGRD_JDGM_TL_KND_CD"].ToString());
                ASM_SGRD_JDGM_TL_TOT_PNT.Add(reader["ASM_SGRD_JDGM_TL_TOT_PNT"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 항생제 투여 정보(복수)
            sql = "SELECT * FROM TI84_ASM023_DRUG ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "' ORDER BY SEQNO";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                MDS_INJC_DT.Add(reader["MDS_INJC_DT"].ToString());
                MDS_CD.Add(reader["MDS_CD"].ToString());
                MDS_NM.Add(reader["MDS_NM"].ToString());
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
                sql += "DELETE FROM TI84_ASM023 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM023_OXY WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM023_SPTUM WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM023_BLD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM023_SGRD WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM023_DRUG WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // TI84_ASM023 메인 테이블 삽입
            sql = "";
            sql += "INSERT INTO TI84_ASM023(";
            sql += "FORM, KEYSTR, SEQ, VER, ";
            sql += "ASM_HOSP_ARIV_DT, DSCG_YN, ASM_DSCG_DT, IPAT_TRM_PNEM_SICK_YN, ";
            sql += "DSCG_STAT_RCD_YN, DSCG_STAT_CD, ";
            sql += "RECU_HOSP_VST_YN, RECU_HOSP_NM, ASM_VST_PTH_CD, ";
            sql += "ASM_PLC_SCTY_OBTN_PNEM_CD2, PLC_SCTY_OBTN_PNEM_TXT, ";
            sql += "ASM_EXM_YN, ASM_PRSC_YN, ASM_GAT_YN, ST1_ANBO_INJC_BF_GAT_YN, ANBO_CHG_BF_GAT_YN, ANBO_CHG_BF_GAT_RS_CD, ";
            sql += "ASM_USE_YN, CNFS_YN, BLUR_UNIT, BLUR_MG_CNT, BLUR_MMOL_CNT, BRT, BPRSU, ";
            sql += "ANBO_USE_YN";
            sql += ") VALUES (";
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);
            para.Add(KEYSTR);
            para.Add(SEQ);
            para.Add(ver);
            para.Add(ASM_HOSP_ARIV_DT);
            para.Add(DSCG_YN);
            para.Add(ASM_DSCG_DT);
            para.Add(IPAT_TRM_PNEM_SICK_YN);
            para.Add(DSCG_STAT_RCD_YN);
            para.Add(DSCG_STAT_CD);
            para.Add(RECU_HOSP_VST_YN);
            para.Add(RECU_HOSP_NM);
            para.Add(ASM_VST_PTH_CD);
            para.Add(ASM_PLC_SCTY_OBTN_PNEM_CD2);
            para.Add(PLC_SCTY_OBTN_PNEM_TXT);

            para.Add(ASM_EXM_YN);
            para.Add(ASM_PRSC_YN);
            para.Add(ASM_GAT_YN);
            para.Add(ST1_ANBO_INJC_BF_GAT_YN);
            para.Add(ANBO_CHG_BF_GAT_YN);
            para.Add(ANBO_CHG_BF_GAT_RS_CD);

            para.Add(ASM_USE_YN);
            para.Add(CNFS_YN);
            para.Add(BLUR_UNIT);
            para.Add(BLUR_MG_CNT);
            para.Add(BLUR_MMOL_CNT);
            para.Add(BRT);
            para.Add(BPRSU);

            para.Add(ANBO_USE_YN);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 반복 삽입: 산소포화도 검사
            for (int i = 0; i < ASM_EXM_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM023_OXY(FORM, KEYSTR, SEQ, SEQNO, ASM_EXM_DT, ASM_OXY_STRT)";
                sql += " VALUES (?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_EXM_DT[i]);
                para.Add(ASM_OXY_STRT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 반복 삽입: 객담배양 검사
            for (int i = 0; i < ASM_PRSC_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM023_SPTUM(FORM, KEYSTR, SEQ, SEQNO, ASM_PRSC_DT)";
                sql += " VALUES (?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_PRSC_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 반복 삽입: 혈액배양 검사
            for (int i = 0; i < ASM_GAT_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM023_BLD(FORM, KEYSTR, SEQ, SEQNO, ASM_GAT_DT)";
                sql += " VALUES (?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_GAT_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 반복 삽입: 중증도 판정도구
            for (int i = 0; i < ASM_USE_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM023_SGRD(FORM, KEYSTR, SEQ, SEQNO, ASM_USE_DT, ASM_SGRD_JDGM_TL_KND_CD, ASM_SGRD_JDGM_TL_TOT_PNT)";
                sql += " VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_USE_DT[i]);
                para.Add(ASM_SGRD_JDGM_TL_KND_CD[i]);
                para.Add(ASM_SGRD_JDGM_TL_TOT_PNT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 반복 삽입: 항생제 투여정보
            for (int i = 0; i < MDS_INJC_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM023_DRUG(FORM, KEYSTR, SEQ, SEQNO, MDS_INJC_DT, MDS_CD, MDS_NM)";
                sql += " VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(MDS_INJC_DT[i]);
                para.Add(MDS_CD[i]);
                para.Add(MDS_NM[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // 1. 메인 테이블 UPDATE
            string sql = "";
            sql += "UPDATE TI84_ASM023 SET ";
            sql += "ASM_HOSP_ARIV_DT = ?, DSCG_YN = ?, ASM_DSCG_DT = ?, IPAT_TRM_PNEM_SICK_YN = ?, ";
            sql += "DSCG_STAT_RCD_YN = ?, DSCG_STAT_CD = ?, ";
            sql += "RECU_HOSP_VST_YN = ?, RECU_HOSP_NM = ?, ASM_VST_PTH_CD = ?, ";
            sql += "ASM_PLC_SCTY_OBTN_PNEM_CD2 = ?, PLC_SCTY_OBTN_PNEM_TXT = ?, ";
            sql += "ASM_EXM_YN = ?, ASM_PRSC_YN = ?, ASM_GAT_YN = ?, ST1_ANBO_INJC_BF_GAT_YN = ?, ";
            sql += "ANBO_CHG_BF_GAT_YN = ?, ANBO_CHG_BF_GAT_RS_CD = ?, ";
            sql += "ASM_USE_YN = ?, CNFS_YN = ?, BLUR_UNIT = ?, BLUR_MG_CNT = ?, BLUR_MMOL_CNT = ?, ";
            sql += "BRT = ?, BPRSU = ?, ANBO_USE_YN = ? ";
            sql += "WHERE FORM = ? AND KEYSTR = ? AND SEQ = ?";

            para.Clear();
            para.Add(ASM_HOSP_ARIV_DT);
            para.Add(DSCG_YN);
            para.Add(ASM_DSCG_DT);
            para.Add(IPAT_TRM_PNEM_SICK_YN);
            para.Add(DSCG_STAT_RCD_YN);
            para.Add(DSCG_STAT_CD);
            para.Add(RECU_HOSP_VST_YN);
            para.Add(RECU_HOSP_NM);
            para.Add(ASM_VST_PTH_CD);
            para.Add(ASM_PLC_SCTY_OBTN_PNEM_CD2);
            para.Add(PLC_SCTY_OBTN_PNEM_TXT);
            para.Add(ASM_EXM_YN);
            para.Add(ASM_PRSC_YN);
            para.Add(ASM_GAT_YN);
            para.Add(ST1_ANBO_INJC_BF_GAT_YN);
            para.Add(ANBO_CHG_BF_GAT_YN);
            para.Add(ANBO_CHG_BF_GAT_RS_CD);
            para.Add(ASM_USE_YN);
            para.Add(CNFS_YN);
            para.Add(BLUR_UNIT);
            para.Add(BLUR_MG_CNT);
            para.Add(BLUR_MMOL_CNT);
            para.Add(BRT);
            para.Add(BPRSU);
            para.Add(ANBO_USE_YN);

            para.Add(form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);


            // 산소포화도
            sql = "DELETE FROM TI84_ASM023_OXY WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            for (int i = 0; i < ASM_EXM_DT.Count; i++)
            {
                sql = "INSERT INTO TI84_ASM023_OXY(FORM, KEYSTR, SEQ, SEQNO, ASM_EXM_DT, ASM_OXY_STRT) VALUES (?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(ASM_EXM_DT[i]);
                para.Add(ASM_OXY_STRT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 객담배양 검사
            sql = "DELETE FROM TI84_ASM023_SPTUM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            for (int i = 0; i < ASM_PRSC_DT.Count; i++)
            {
                sql = "INSERT INTO TI84_ASM023_SPTUM(FORM, KEYSTR, SEQ, SEQNO, ASM_PRSC_DT) VALUES (?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(ASM_PRSC_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 혈액배양 검사
            sql = "DELETE FROM TI84_ASM023_BLD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            for (int i = 0; i < ASM_GAT_DT.Count; i++)
            {
                sql = "INSERT INTO TI84_ASM023_BLD(FORM, KEYSTR, SEQ, SEQNO, ASM_GAT_DT) VALUES (?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(ASM_GAT_DT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 중증도 판정도구
            sql = "DELETE FROM TI84_ASM023_SGRD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            for (int i = 0; i < ASM_USE_DT.Count; i++)
            {
                sql = "INSERT INTO TI84_ASM023_SGRD(FORM, KEYSTR, SEQ, SEQNO, ASM_USE_DT, ASM_SGRD_JDGM_TL_KND_CD, ASM_SGRD_JDGM_TL_TOT_PNT) VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(ASM_USE_DT[i]);
                para.Add(ASM_SGRD_JDGM_TL_KND_CD[i]);
                para.Add(ASM_SGRD_JDGM_TL_TOT_PNT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 투약정보
            sql = "DELETE FROM TI84_ASM023_DRUG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            for (int i = 0; i < MDS_INJC_DT.Count; i++)
            {
                sql = "INSERT INTO TI84_ASM023_DRUG(FORM, KEYSTR, SEQ, SEQNO, MDS_INJC_DT, MDS_CD, MDS_NM) VALUES (?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(MDS_INJC_DT[i]);
                para.Add(MDS_CD[i]);
                para.Add(MDS_NM[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // ASM000에 저장 기록
            Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM023 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM023_OXY WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM023_SPTUM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM023_BLD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM023_SGRD WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM023_DRUG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

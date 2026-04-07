using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM014_001 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM014"; // 서식코드
        public readonly string ver_id = "001"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "PSY"; // 업무상세코드

        // A. 기본 정보
        public string TRFR_YN;                        // 전과 여부 (1=Yes, 2=No)
        public List<string> TRFR_DD = new List<string>();          // 전과일자 복수 입력 (최대 3개)
        public List<string> MVOT_DGSBJT_CD = new List<string>();   // 전출과 진료과목 코드
        public List<string> MVIN_DGSBJT_CD = new List<string>();   // 전입과 진료과목 코드

        public string INSUP_QLF_CHG_YN;               // 자격변동 여부 (1=Yes, 2=No)
        public List<string> QLF_CHG_DD = new List<string>();       // 자격변동일 복수 입력 (최대 3개)

        public string DSCG_YN;                        // 퇴원 여부 (1=Yes, 2=No)
        public string DSCG_DD;                        // 퇴원일자
        public string DSCG_STAT_CD;                   // 퇴원상태 코드 (1~5)
        public string SPNT_IPAT_YN;                   // 자발적 입원 여부 (1=Yes, 2=No)

        // B. 지역사회서비스 연계
        public string DSCG_POTM_PSYCHI_DS_DIAG_YN;    // 조현병 진단 여부 (1=Yes, 2=No)
        public string PLC_SCTY_SVC_CONN_REQ_YN;       // 지역사회서비스 연계 의뢰 여부 (1=Yes, 2=No)
        public string PLC_SCTY_SVC_CONN_NREQ_RS_CD;   // 미의뢰 사유 코드 (1~3, 9)
        public string PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT; // 미의뢰 사유 기타 상세설명

        // C. 퇴원 시 환자경험도 조사
        public string DSCG_EXPR_INVT_ENFC_YN;         // 퇴원 시 경험도조사 시행 여부 (1=Yes, 2=No)
        public string DSCG_EXPR_NOPER_RS_CD;          // 조사 미시행 사유 코드 (1~4, 9)
        public string NOPER_RS_ETC_TXT;               // 미시행 사유 기타 상세 설명

        // D. 기타 사항
        //public string APND_DATA_NO;                   // 첨부자료 식별번호 (예: 이미지)

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본 정보
            TRFR_YN = "";                        // 전과 여부
            TRFR_DD.Clear();                    // 전과일자 리스트 초기화
            MVOT_DGSBJT_CD.Clear();             // 전출과 진료과목 코드 초기화
            MVIN_DGSBJT_CD.Clear();             // 전입과 진료과목 코드 초기화

            INSUP_QLF_CHG_YN = "";              // 자격변동 여부
            QLF_CHG_DD.Clear();                 // 자격변동일자 리스트 초기화

            DSCG_YN = "";                       // 퇴원 여부
            DSCG_DD = "";                       // 퇴원일자
            DSCG_STAT_CD = "";                 // 퇴원상태 코드
            SPNT_IPAT_YN = "";                 // 자발적 입원 여부

            // B. 지역사회서비스 연계
            DSCG_POTM_PSYCHI_DS_DIAG_YN = "";          // 조현병 진단 여부
            PLC_SCTY_SVC_CONN_REQ_YN = "";            // 지역사회서비스 연계 의뢰 여부
            PLC_SCTY_SVC_CONN_NREQ_RS_CD = "";        // 미의뢰 사유 코드
            PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = "";   // 미의뢰 기타 사유 상세

            // C. 퇴원 시 환자경험도 조사
            DSCG_EXPR_INVT_ENFC_YN = "";               // 경험도 조사 시행 여부
            DSCG_EXPR_NOPER_RS_CD = "";                // 미시행 사유 코드
            NOPER_RS_ETC_TXT = "";                     // 기타 미시행 사유

            // D. 기타 사항
            // APND_DATA_NO = "";                      // 첨부자료 식별번호 (선택 항목, 비사용 시 주석)
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";

            // 1. 메인 테이블 조회: TI84_ASM014
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM014";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";


            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                // A. 기본 정보
                TRFR_YN = reader["TRFR_YN"].ToString();                                     // 전과 여부
                INSUP_QLF_CHG_YN = reader["INSUP_QLF_CHG_YN"].ToString();                  // 자격변동 여부
                DSCG_YN = reader["DSCG_YN"].ToString();                                    // 퇴원 여부
                DSCG_DD = reader["DSCG_DD"].ToString();                                    // 퇴원일자
                DSCG_STAT_CD = reader["DSCG_STAT_CD"].ToString();                          // 퇴원상태 코드
                SPNT_IPAT_YN = reader["SPNT_IPAT_YN"].ToString();                          // 자발적 입원 여부

                // B. 지역사회 연계
                DSCG_POTM_PSYCHI_DS_DIAG_YN = reader["DSCG_POTM_PSYCHI_DS_DIAG_YN"].ToString(); // 조현병 진단 여부
                PLC_SCTY_SVC_CONN_REQ_YN = reader["PLC_SCTY_SVC_CONN_REQ_YN"].ToString();       // 지역사회서비스 연계 의뢰 여부
                PLC_SCTY_SVC_CONN_NREQ_RS_CD = reader["PLC_SCTY_SVC_CONN_NREQ_RS_CD"].ToString(); // 미의뢰 사유
                PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = reader["PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT"].ToString(); // 기타 사유

                // C. 환자경험도조사
                DSCG_EXPR_INVT_ENFC_YN = reader["DSCG_EXPR_INVT_ENFC_YN"].ToString();      // 경험도조사 시행 여부
                DSCG_EXPR_NOPER_RS_CD = reader["DSCG_EXPR_NOPER_RS_CD"].ToString();        // 미시행 사유 코드
                NOPER_RS_ETC_TXT = reader["NOPER_RS_ETC_TXT"].ToString();                  // 기타 상세 사유

                return MetroLib.SqlHelper.BREAK; // 단일 행만 조회하므로 BREAK
            });

            // 2. 전과 정보 반복 테이블 조회 (TI84_ASM014_TRFR)
            TRFR_DD.Clear();                          // 전과일자 초기화
            MVOT_DGSBJT_CD.Clear();                  // 전출과 진료과목 코드 초기화
            MVIN_DGSBJT_CD.Clear();                  // 전입과 진료과목 코드 초기화

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM014_TRFR";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + "ORDER BY SEQNO";
    
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                TRFR_DD.Add(reader["TRFR_DD"].ToString());                  // 전과일자
                MVOT_DGSBJT_CD.Add(reader["MVOT_DGSBJT_CD"].ToString());    // 전출과
                MVIN_DGSBJT_CD.Add(reader["MVIN_DGSBJT_CD"].ToString());    // 전입과
                return MetroLib.SqlHelper.CONTINUE; // 반복 행 읽기 계속
            });

            // 3. 자격변동일자 반복 테이블 조회 (TI84_ASM014_QLF)
            QLF_CHG_DD.Clear(); // 초기화

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM014_QLF";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + "ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                QLF_CHG_DD.Add(reader["QLF_CHG_DD"].ToString());           // 자격변동일자 추가
                return MetroLib.SqlHelper.CONTINUE; // 계속 반복
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
                sql += "DELETE FROM TI84_ASM014 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM014_TRFR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM014_QLF WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>(); // 파라미터 초기화
            

            // 1. 메인 테이블 INSERT: TI84_ASM014
            sql = "";
            sql += "INSERT INTO TI84_ASM014 (";
            sql += "FORM, KEYSTR, SEQ, VER, ";
            sql += "TRFR_YN, INSUP_QLF_CHG_YN, DSCG_YN, DSCG_DD, DSCG_STAT_CD, SPNT_IPAT_YN, ";
            sql += "DSCG_POTM_PSYCHI_DS_DIAG_YN, ";
            sql += "PLC_SCTY_SVC_CONN_REQ_YN, PLC_SCTY_SVC_CONN_NREQ_RS_CD, PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT, ";
            sql += "DSCG_EXPR_INVT_ENFC_YN, DSCG_EXPR_NOPER_RS_CD, NOPER_RS_ETC_TXT";
            sql += ") VALUES (";
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);                                 // FORM
            para.Add(KEYSTR);                               // KEYSTR
            para.Add(SEQ);                                  // SEQ
            para.Add(ver);                                  // VER
            para.Add(TRFR_YN);                              // 전과 여부
            para.Add(INSUP_QLF_CHG_YN);                     // 자격변동 여부
            para.Add(DSCG_YN);                              // 퇴원 여부
            para.Add(DSCG_DD);                              // 퇴원일자
            para.Add(DSCG_STAT_CD);                         // 퇴원상태
            para.Add(SPNT_IPAT_YN);                         // 자발적 입원 여부
            para.Add(DSCG_POTM_PSYCHI_DS_DIAG_YN);          // 조현병 진단 여부
            para.Add(PLC_SCTY_SVC_CONN_REQ_YN);             // 지역연계 의뢰 여부
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_CD);         // 미의뢰 사유코드
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT);    // 기타 사유텍스트
            para.Add(DSCG_EXPR_INVT_ENFC_YN);               // 환자경험도 조사 여부
            para.Add(DSCG_EXPR_NOPER_RS_CD);                // 미실시 사유 코드
            para.Add(NOPER_RS_ETC_TXT);                     // 기타 미실시 사유

            // 메인 테이블 실행
            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 2. 전과 내역 삽입: TI84_ASM014_TRFR
            for (int i = 0; i < TRFR_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM014_TRFR (FORM, KEYSTR, SEQ, SEQNO, TRFR_DD, MVOT_DGSBJT_CD, MVIN_DGSBJT_CD) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);                           // FORM
                para.Add(KEYSTR);                         // KEYSTR
                para.Add(SEQ);                            // SEQ
                para.Add(i + 1);                          // SEQNO (1부터)
                para.Add(TRFR_DD[i]);                     // 전과일자
                para.Add(MVOT_DGSBJT_CD[i]);              // 전출과 코드
                para.Add(MVIN_DGSBJT_CD[i]);              // 전입과 코드

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 3. 자격변동일자 삽입: TI84_ASM014_QLF
            for (int i = 0; i < QLF_CHG_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM014_QLF (FORM, KEYSTR, SEQ, SEQNO, QLF_CHG_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);                           // FORM
                para.Add(KEYSTR);                         // KEYSTR
                para.Add(SEQ);                            // SEQ
                para.Add(i + 1);                          // SEQNO (1부터)
                para.Add(QLF_CHG_DD[i]);                  // 자격변동일자

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>(); // SQL 파라미터용 리스트 초기화
            string sql = "";

            // 1. 메인 테이블 UPDATE : TI84_ASM014
            sql = "";
            sql += "UPDATE TI84_ASM014 SET ";
            sql += "TRFR_YN = ?, ";                                  // 전과 여부
            sql += "INSUP_QLF_CHG_YN = ?, ";                          // 자격변동 여부
            sql += "DSCG_YN = ?, ";                                   // 퇴원 여부
            sql += "DSCG_DD = ?, ";                                   // 퇴원일자
            sql += "DSCG_STAT_CD = ?, ";                              // 퇴원 상태
            sql += "SPNT_IPAT_YN = ?, ";                              // 자발적 입원 여부
            sql += "DSCG_POTM_PSYCHI_DS_DIAG_YN = ?, ";               // 조현병 진단 여부
            sql += "PLC_SCTY_SVC_CONN_REQ_YN = ?, ";                  // 지역사회 연계 여부
            sql += "PLC_SCTY_SVC_CONN_NREQ_RS_CD = ?, ";              // 미의뢰 사유코드
            sql += "PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = ?, ";         // 기타 사유
            sql += "DSCG_EXPR_INVT_ENFC_YN = ?, ";                    // 환자경험도조사 수행 여부
            sql += "DSCG_EXPR_NOPER_RS_CD = ?, ";                     // 미시행 사유 코드
            sql += "NOPER_RS_ETC_TXT = ? ";                           // 기타 설명
            sql += "WHERE FORM = ? AND KEYSTR = ? AND SEQ = ?";       // 조건절

            para.Clear();
            para.Add(TRFR_YN);                                        // 전과 여부
            para.Add(INSUP_QLF_CHG_YN);                               // 자격변동 여부
            para.Add(DSCG_YN);                                        // 퇴원 여부
            para.Add(DSCG_DD);                                        // 퇴원일자
            para.Add(DSCG_STAT_CD);                                   // 퇴원상태
            para.Add(SPNT_IPAT_YN);                                   // 자발적 입원 여부
            para.Add(DSCG_POTM_PSYCHI_DS_DIAG_YN);                    // 조현병 진단 여부
            para.Add(PLC_SCTY_SVC_CONN_REQ_YN);                       // 연계 의뢰 여부
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_CD);                   // 미의뢰 사유
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT);              // 기타 미의뢰 사유
            para.Add(DSCG_EXPR_INVT_ENFC_YN);                         // 경험도 조사 여부
            para.Add(DSCG_EXPR_NOPER_RS_CD);                          // 미시행 사유
            para.Add(NOPER_RS_ETC_TXT);                               // 미시행 기타 설명
            para.Add(form);                                           // FORM (PK)
            para.Add(KEYSTR);                                         // KEYSTR (PK)
            para.Add(SEQ);                                            // SEQ (PK)

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // SQL 실행

            // 2. 기존 반복 테이블 삭제 - 전과 테이블 삭제
            sql = "";
            sql += "DELETE FROM TI84_ASM014_TRFR ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);       // 전과일자 삭제

            // 3. 반복입력 다시 삽입: 전과 정보 → TI84_ASM014_TRFR
            for (int i = 0; i < TRFR_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM014_TRFR (FORM, KEYSTR, SEQ, SEQNO, TRFR_DD, MVOT_DGSBJT_CD, MVIN_DGSBJT_CD) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);                       // FORM
                para.Add(KEYSTR);                     // KEYSTR
                para.Add(SEQ);                        // SEQ
                para.Add(i + 1);                      // SEQNO (1부터 시작)
                para.Add(TRFR_DD[i]);                 // 전과일자
                para.Add(MVOT_DGSBJT_CD[i]);          // 전출과 코드
                para.Add(MVIN_DGSBJT_CD[i]);          // 전입과 코드

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 실행
            }

            // 4. 기존 반복 테이블 삭제 - 자격변동일 삭제
            sql = "";
            sql += "DELETE FROM TI84_ASM014_QLF ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);       // 자격변동일 삭제

            // 5. 반복입력 다시 삽입: 자격변동 → TI84_ASM014_QLF
            for (int i = 0; i < QLF_CHG_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM014_QLF (FORM, KEYSTR, SEQ, SEQNO, QLF_CHG_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);                       // FORM
                para.Add(KEYSTR);                     // KEYSTR
                para.Add(SEQ);                        // SEQ
                para.Add(i + 1);                      // SEQNO
                para.Add(QLF_CHG_DD[i]);              // 자격변동일자

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran); // 실행
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM014 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM014_TRFR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM014_QLF WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

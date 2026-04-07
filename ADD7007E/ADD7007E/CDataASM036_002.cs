using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM036_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM036"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "MHH"; // 업무상세코드

        // A. 기본 정보
        public string FST_IPAT_DD;             // 최초 입원일자
        public string IPAT_DGSBJT_CD;          // 최초 진료과목 코드

        public string TRFR_YN;                 // 전과 여부
        public List<string> TRFR_DD = new List<string>();   // 전과일자 리스트(최대 3개)

        public string INSUP_QLF_CHG_YN;        // 보험자 자격 변동 여부
        public List<string> QLF_CHG_DD = new List<string>(); // 자격 변동일 목록

        public string DSCG_YN;                 // 퇴원 여부
        public string DSCG_DD;                 // 퇴원일자
        public string DSCG_STAT_CD;            // 퇴원상태 코드
        public string DEATH_DD;                // 사망일 (퇴원상태==5일 경우)
        public string SPNT_IPAT_YN;            // 자발적 입원 여부

        // B. 기능평가
        public string FCLT_ASM_YN;             // 기능평가 시행 여부

        public List<string> FCLT_ASM_TL_ENFC_DD = new List<string>();         // 시행일자 리스트
        public List<string> PSYCHI_HTH_FCLT_ASM_TL_CD = new List<string>();   // 도구코드 (복수 선택: '4/5' 형태 또는 개별 가능)
        public List<string> WHO12_FCLT_ASM_PNT = new List<string>();      // WHODAS 12점수
        public List<string> WHO36_FCLT_ASM_PNT = new List<string>();      // WHODAS 36점수
        public List<string> HONOS_FCLT_ASM_PNT = new List<string>();      // HoNOS 점수
        public List<string> GAF_FCLT_ASM_PNT = new List<string>();        // GAF 점수
        public List<string> CGI_FCLT_ASM_PNT = new List<string>();        // CGI 점수

        // C. 지역사회서비스 연계
        public string DSCG_POTM_PSYCHI_DS_DIAG_YN;     // 조현병 진단 여부

        public string PLC_SCTY_SVC_CONN_REQ_YN;        // 지역사회서비스 의뢰 여부
        public string PLC_SCTY_SVC_CONN_NREQ_RS_CD;    // 미의뢰 사유 코드
        public string PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT; // 미의뢰 사유 기타 상세

        // D. 퇴원 시 환자 경험도 조사
        public string DSCG_EXPR_INVT_ENFC_YN;          // 환자 경험도 조사 시행 여부
        public string DSCG_EXPR_NOPER_RS_CD;           // 미시행 사유 코드
        public string NOPER_RS_ETC_TXT;                // 기타 미시행 사유 상세

        // E. 첨부파일 (선택 사항)
        //public string APND_DATA_NO;                    // 첨부문서 식별 ID

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id); // 상위 클래스(CData)의 공통 초기화 수행
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본 정보 초기화
            FST_IPAT_DD = "";                     // 최초 입원일자
            IPAT_DGSBJT_CD = "";                 // 최초 진료과목 코드
            TRFR_YN = "";                        // 전과 여부
            TRFR_DD.Clear();                    // 전과일자 목록 초기화
            INSUP_QLF_CHG_YN = "";              // 보험자 자격변동 여부
            QLF_CHG_DD.Clear();                 // 자격변동일 목록 초기화
            DSCG_YN = "";                       // 퇴원 여부
            DSCG_DD = "";                       // 퇴원일자
            DSCG_STAT_CD = "";                 // 퇴원상태 코드
            DEATH_DD = "";                     // 사망일
            SPNT_IPAT_YN = "";                 // 자발적 입원 여부

            // B. 기능평가 정보 초기화
            FCLT_ASM_YN = "";                   // 기능평가 시행 여부
            FCLT_ASM_TL_ENFC_DD.Clear();       // 기능평가 시행일자 목록
            PSYCHI_HTH_FCLT_ASM_TL_CD.Clear(); // 사용된 기능평가 도구 코드
            WHO12_FCLT_ASM_PNT.Clear();           // WHODAS 12점
            WHO36_FCLT_ASM_PNT.Clear();           // WHODAS 36점
            HONOS_FCLT_ASM_PNT.Clear();           // HoNOS 점수
            GAF_FCLT_ASM_PNT.Clear();             // GAF 점수
            CGI_FCLT_ASM_PNT.Clear();             // CGI 점수

            // C. 지역사회서비스 연계 정보 초기화
            DSCG_POTM_PSYCHI_DS_DIAG_YN = "";        // 조현병 진단 여부
            PLC_SCTY_SVC_CONN_REQ_YN = "";           // 지역사회 서비스 의뢰 여부
            PLC_SCTY_SVC_CONN_NREQ_RS_CD = "";       // 미의뢰 사유 코드
            PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = "";  // 미의뢰 상세 사유

            // D. 퇴원 시 환자 경험도 조사 초기화
            DSCG_EXPR_INVT_ENFC_YN = "";     // 환자경험도조사 시행 여부
            DSCG_EXPR_NOPER_RS_CD = "";      // 미시행 사유
            NOPER_RS_ETC_TXT = "";           // 기타 미시행 상세

            // E. 첨부자료 (선택)
            // APND_DATA_NO = "";            // (주석처리된 항목으로 현재 제외)
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";

            // 1. 메인 테이블 조회: TI84_ASM036
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM036";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                FST_IPAT_DD = reader["FST_IPAT_DD"].ToString();
                IPAT_DGSBJT_CD = reader["IPAT_DGSBJT_CD"].ToString();
                TRFR_YN = reader["TRFR_YN"].ToString();
                INSUP_QLF_CHG_YN = reader["INSUP_QLF_CHG_YN"].ToString();
                DSCG_YN = reader["DSCG_YN"].ToString();
                DSCG_DD = reader["DSCG_DD"].ToString();
                DSCG_STAT_CD = reader["DSCG_STAT_CD"].ToString();
                DEATH_DD = reader["DEATH_DD"].ToString();
                SPNT_IPAT_YN = reader["SPNT_IPAT_YN"].ToString();
                FCLT_ASM_YN = reader["FCLT_ASM_YN"].ToString();
                DSCG_POTM_PSYCHI_DS_DIAG_YN = reader["DSCG_POTM_PSYCHI_DS_DIAG_YN"].ToString();
                PLC_SCTY_SVC_CONN_REQ_YN = reader["PLC_SCTY_SVC_CONN_REQ_YN"].ToString();
                PLC_SCTY_SVC_CONN_NREQ_RS_CD = reader["PLC_SCTY_SVC_CONN_NREQ_RS_CD"].ToString();
                PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = reader["PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT"].ToString();
                DSCG_EXPR_INVT_ENFC_YN = reader["DSCG_EXPR_INVT_ENFC_YN"].ToString();
                DSCG_EXPR_NOPER_RS_CD = reader["DSCG_EXPR_NOPER_RS_CD"].ToString();
                NOPER_RS_ETC_TXT = reader["NOPER_RS_ETC_TXT"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // 전과일자 리스트: TI84_ASM036_TRFR
            TRFR_DD.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM036_TRFR";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + "ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                TRFR_DD.Add(reader["TRFR_DD"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 자격변동일자 리스트: TI84_ASM036_QLF
            QLF_CHG_DD.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM036_QLF";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                QLF_CHG_DD.Add(reader["QLF_CHG_DD"].ToString());
                return MetroLib.SqlHelper.CONTINUE;
            });

            // 기능평가 시행일자 및 도구: TI84_ASM036_FCLT
            FCLT_ASM_TL_ENFC_DD.Clear();
            PSYCHI_HTH_FCLT_ASM_TL_CD.Clear();
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM036_FCLT";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + "ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                FCLT_ASM_TL_ENFC_DD.Add(reader["FCLT_ASM_TL_ENFC_DD"].ToString());
                PSYCHI_HTH_FCLT_ASM_TL_CD.Add(reader["PSYCHI_HTH_FCLT_ASM_TL_CD"].ToString());
                WHO12_FCLT_ASM_PNT.Add(reader["WHO12_FCLT_ASM_PNT"].ToString());
                WHO36_FCLT_ASM_PNT.Add(reader["WHO36_FCLT_ASM_PNT"].ToString());
                HONOS_FCLT_ASM_PNT.Add(reader["HONOS_FCLT_ASM_PNT"].ToString());
                GAF_FCLT_ASM_PNT.Add(reader["GAF_FCLT_ASM_PNT"].ToString());
                CGI_FCLT_ASM_PNT.Add(reader["CGI_FCLT_ASM_PNT"].ToString());
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
                sql += "DELETE FROM TI84_ASM036 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM036_TRFR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM036_QLF WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM036_FCLT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // 1. 메인 테이블 INSERT: TI84_ASM036
            sql = "";
            sql += "INSERT INTO TI84_ASM036 (";
            sql += "FORM, KEYSTR, SEQ, VER, "; // 4
            sql += "FST_IPAT_DD, IPAT_DGSBJT_CD, TRFR_YN, INSUP_QLF_CHG_YN, "; // 4
            sql += "DSCG_YN, DSCG_DD, DSCG_STAT_CD, DEATH_DD, SPNT_IPAT_YN, "; // 5
            sql += "FCLT_ASM_YN, "; // 1
            sql += "DSCG_POTM_PSYCHI_DS_DIAG_YN, PLC_SCTY_SVC_CONN_REQ_YN, PLC_SCTY_SVC_CONN_NREQ_RS_CD, PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT, "; // 4
            sql += "DSCG_EXPR_INVT_ENFC_YN, DSCG_EXPR_NOPER_RS_CD, NOPER_RS_ETC_TXT";
            sql += ") VALUES (";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?)";

            para.Clear();
            para.Add(form);                                      // FORM
            para.Add(KEYSTR);                                    // KEYSTR
            para.Add(SEQ);                                       // SEQ
            para.Add(ver);                                       // VER

            para.Add(FST_IPAT_DD);                               // 최초 입원일자
            para.Add(IPAT_DGSBJT_CD);                            // 입원 진료과
            para.Add(TRFR_YN);                                   // 전과 여부
            para.Add(INSUP_QLF_CHG_YN);                          // 자격변동 여부

            para.Add(DSCG_YN);                                   // 퇴원 여부
            para.Add(DSCG_DD);                                   // 퇴원일자
            para.Add(DSCG_STAT_CD);                              // 퇴원상태
            para.Add(DEATH_DD);                                  // 사망일자
            para.Add(SPNT_IPAT_YN);                              // 자발적 입원 여부

            para.Add(FCLT_ASM_YN);                               // 기능평가 시행 여부

            para.Add(DSCG_POTM_PSYCHI_DS_DIAG_YN);               // 조현병 진단 여부
            para.Add(PLC_SCTY_SVC_CONN_REQ_YN);                  // 지역사회 서비스 연결 여부
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_CD);              // 미연결 사유 코드
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT);         // 기타 사유 상세

            para.Add(DSCG_EXPR_INVT_ENFC_YN);                    // 환자 경험도 조사 여부
            para.Add(DSCG_EXPR_NOPER_RS_CD);                     // 미실시 사유
            para.Add(NOPER_RS_ETC_TXT);                          // 기타 미실시 상세

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 2. 반복항목: 전과일자 리스트 → TI84_ASM036_TRFR
            for (int i = 0; i < TRFR_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_TRFR (FORM, KEYSTR, SEQ, SEQNO, TRFR_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO
                para.Add(TRFR_DD[i]); // 전과일자

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 3. 반복항목: 자격변동일자 리스트 → TI84_ASM036_QLF
            for (int i = 0; i < QLF_CHG_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_QLF (FORM, KEYSTR, SEQ, SEQNO, QLF_CHG_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO
                para.Add(QLF_CHG_DD[i]); // 변동일자

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 4. 반복항목: 기능평가 시행일자 및 도구 → TI84_ASM036_FCLT
            for (int i = 0; i < FCLT_ASM_TL_ENFC_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_FCLT (FORM, KEYSTR, SEQ, SEQNO, FCLT_ASM_TL_ENFC_DD, PSYCHI_HTH_FCLT_ASM_TL_CD, ";
                sql += "WHO12_FCLT_ASM_PNT, WHO36_FCLT_ASM_PNT, HONOS_FCLT_ASM_PNT, GAF_FCLT_ASM_PNT, CGI_FCLT_ASM_PNT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO

                para.Add(FCLT_ASM_TL_ENFC_DD[i]);           // 기능평가 시행일자
                para.Add(PSYCHI_HTH_FCLT_ASM_TL_CD[i]);     // 시행 도구 코드(다중선택 가능)

                para.Add(WHO12_FCLT_ASM_PNT[i]);                        // WHODAS 12점수
                para.Add(WHO36_FCLT_ASM_PNT[i]);                        // WHODAS 36점수
                para.Add(HONOS_FCLT_ASM_PNT[i]);                        // HoNOS 점수
                para.Add(GAF_FCLT_ASM_PNT[i]);                          // GAF 점수
                para.Add(CGI_FCLT_ASM_PNT[i]);                          // CGI 점수

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();
            string sql = "";

            // 1. 메인 테이블 UPDATE 구문
            sql = "";
            sql += "UPDATE TI84_ASM036 SET ";
            sql += "FST_IPAT_DD = ?, ";
            sql += "IPAT_DGSBJT_CD = ?, ";
            sql += "TRFR_YN = ?, ";
            sql += "INSUP_QLF_CHG_YN = ?, ";
            sql += "DSCG_YN = ?, ";
            sql += "DSCG_DD = ?, ";
            sql += "DSCG_STAT_CD = ?, ";
            sql += "DEATH_DD = ?, ";
            sql += "SPNT_IPAT_YN = ?, ";
            sql += "FCLT_ASM_YN = ?, ";
            sql += "DSCG_POTM_PSYCHI_DS_DIAG_YN = ?, ";
            sql += "PLC_SCTY_SVC_CONN_REQ_YN = ?, ";
            sql += "PLC_SCTY_SVC_CONN_NREQ_RS_CD = ?, ";
            sql += "PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT = ?, ";
            sql += "DSCG_EXPR_INVT_ENFC_YN = ?, ";
            sql += "DSCG_EXPR_NOPER_RS_CD = ?, ";
            sql += "NOPER_RS_ETC_TXT = ? ";
            sql += "WHERE FORM = ? AND KEYSTR = ? AND SEQ = ?";

            para.Clear();
            para.Add(FST_IPAT_DD);
            para.Add(IPAT_DGSBJT_CD);
            para.Add(TRFR_YN);
            para.Add(INSUP_QLF_CHG_YN);
            para.Add(DSCG_YN);
            para.Add(DSCG_DD);
            para.Add(DSCG_STAT_CD);
            para.Add(DEATH_DD);
            para.Add(SPNT_IPAT_YN);
            para.Add(FCLT_ASM_YN);
            para.Add(DSCG_POTM_PSYCHI_DS_DIAG_YN);
            para.Add(PLC_SCTY_SVC_CONN_REQ_YN);
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_CD);
            para.Add(PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT);
            para.Add(DSCG_EXPR_INVT_ENFC_YN);
            para.Add(DSCG_EXPR_NOPER_RS_CD);
            para.Add(NOPER_RS_ETC_TXT);
            para.Add(form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 2. 전과일자 재삽입 → TI84_ASM036_TRFR
            sql = "DELETE FROM TI84_ASM036_TRFR WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < TRFR_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_TRFR (FORM, KEYSTR, SEQ, SEQNO, TRFR_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO
                para.Add(TRFR_DD[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 3. 자격변동일자 재삽입 → TI84_ASM036_QLF
            sql = "DELETE FROM TI84_ASM036_QLF WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < QLF_CHG_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_QLF (FORM, KEYSTR, SEQ, SEQNO, QLF_CHG_DD) ";
                sql += "VALUES (?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO
                para.Add(QLF_CHG_DD[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 4. 기능평가 시행일자 및 도구재삽입 → TI84_ASM036_FCLT
            sql = "DELETE FROM TI84_ASM036_FCLT WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < FCLT_ASM_TL_ENFC_DD.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM036_FCLT (FORM, KEYSTR, SEQ, SEQNO, FCLT_ASM_TL_ENFC_DD, PSYCHI_HTH_FCLT_ASM_TL_CD, ";
                sql += "WHO12_FCLT_ASM_PNT, WHO36_FCLT_ASM_PNT, HONOS_FCLT_ASM_PNT, GAF_FCLT_ASM_PNT, CGI_FCLT_ASM_PNT) ";
                sql += "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1); // SEQNO

                para.Add(FCLT_ASM_TL_ENFC_DD[i]);           // 기능평가 시행일자
                para.Add(PSYCHI_HTH_FCLT_ASM_TL_CD[i]);     // 시행 도구 코드(다중선택 가능)

                para.Add(WHO12_FCLT_ASM_PNT[i]);                        // WHODAS 12점수
                para.Add(WHO36_FCLT_ASM_PNT[i]);                        // WHODAS 36점수
                para.Add(HONOS_FCLT_ASM_PNT[i]);                        // HoNOS 점수
                para.Add(GAF_FCLT_ASM_PNT[i]);                          // GAF 점수
                para.Add(CGI_FCLT_ASM_PNT[i]);                          // CGI 점수

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM036 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM036_TRFR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM036_QLF WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM036_FCLT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

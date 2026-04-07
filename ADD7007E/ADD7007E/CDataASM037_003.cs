using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM037_003 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM037"; // 서식코드
        public readonly string ver_id = "003"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "BLT"; // 업무상세코드

        // A. 기본정보
        public string IPAT_DD; // 입원일자(YYYYMMDD)
        public string DSCG_DD; // 퇴원일자(YYYYMMDD)

        // B 수술정보
        public string SOPR_YN; // 수술 여부(1.Yes 2.No)
        public List<string> ASM_OPRM_IPAT_DT = new List<string>(); // 수술실 입실일시(YYYYMMDDHHMM)
        public List<string> ASM_OPRM_DSCG_DT = new List<string>(); // 수술실 퇴실일시(YYYYMMDDHHMM)
        public List<string> ASM_RCRM_DSCG_DT = new List<string>(); // 회복실 퇴실일시(YYYYMMDDHHMM)
        public List<string> SOPR_NM = new List<string>(); // 수술명
        public List<string> SOPR_MDFEE_CD = new List<string>(); // 수가코드(MDFEE_CD)

        public string LFB_FS_YN; // 척추후방고정술 실시여부(1.Yes 2.No)
        public string LFB_FS_LVL; // 척추후방고정술 Level(1,2,3)
        public string KNJN_RPMT_YN; // 슬관절치환술 실시여부(1.Yes 2.No)
        public string KNJN_RPMT_RGN_CD; // 슬관절치환술 부위(1.단측 2.양측)

        // C.수혈 체크리스트 사용 현황
        public string ASM_PRSC_YN; // 처방여부(1.Yes 2.No)
        public List<string> ASM_PRSC_DT = new List<string>(); // 처방일시(YYYYMMDDHHMM)
        public List<string> ASM_PRSC_UNIT_CNT = new List<string>(); // 처방량(unit)
        public List<string> ASM_BLTS_CHKLST_USE_YN = new List<string>(); // 수혈 체크리스트 사용여부(1.Yes 2.No)
        public List<string> ASM_BLTS_STA_DT = new List<string>(); // 수혈시작일시(YYYYMMDDHHMM)
        public List<string> ASM_PRSC_BLTS_DGM_NM = new List<string>(); // 수혈제제명(BLTS_DGM_NM)
        public List<string> ASM_PRSC_MDFEE_CD = new List<string>(); // 수가코드(MDFEE_CD)
        public List<string> ASM_BLTS_UNIT_CNT = new List<string>(); // 수혈량(unit)(BLTS_UNIT_CNT)

        // D. 투약정보
        public string ANM_DIAG_YN; // 빈혈 진단(1.Yes 2.No)
        public List<string> SICK_SYM = new List<string>(); // 상병분류기호
        public List<string> DIAG_NM = new List<string>(); // 진단명
        public string ANM_REFM_YN; // 빈혈교정 유무(1.Yes 2.No)
        public List<string> MDS_NM = new List<string>(); // 빈혈교정 처방약품명
        public List<string> MDS_CD = new List<string>(); // 빈혈교정 처방약품코드

        // E.검사정보
        public string HG_EXM_ENFC_YN; // Hb검사 시행여부(1.Yes 2.No)
        public List<string> ASM_EXM_RST_DT = new List<string>(); // 검사결과일시(YYYYMMDDHHMM)
        public List<string> EXM_MDFEE_CD = new List<string>(); // 수가코드(MDFEE_CD)
        public List<string> EXM_NM = new List<string>(); // 검사명
        public List<string> HG_NUV = new List<string>(); // 검사결과(g/dL)

        // F.수혈정보
        public string BLTS_YN; // 수혈 시행여부(1.Yes 2.No)
        public List<string> BLTS_STA_DT = new List<string>(); // 수혈시작일시(YYYYMMDDHHMM)(ASM_BLTS_STA_DT)
        public List<string> BLTS_END_DT = new List<string>(); // 수혈종료일시(YYYYMMDDHHMM)(ASM_BLTS_END_DT)
        public List<string> BLTS_DGM_NM = new List<string>(); // 수혈제제명
        public List<string> BLTS_MDFEE_CD = new List<string>(); // 수가코드(MDFEE_CD)
        public List<string> BLTS_UNIT_CNT = new List<string>(); // 수혈량(unit)
        public List<string> HG_DCR_YN = new List<string>(); // Hb저하 발생 여부(1.Yes 2.No)
        public List<string> OPRM_HMRHG_OCUR_YN_CD = new List<string>(); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
        public List<string> OPRM_MIDD_HMRHG_QTY = new List<string>(); // 수술 중 실혈량(ml)
        public List<string> OPRM_AF_DRN_QTY = new List<string>(); // 수술 후 배액량(ml)
        public List<string> BLTS_RS_ETC_YN = new List<string>(); // 그 외 수혈사유 여부(1.Yes 2.No)
        public List<string> BLTS_RS_ETC_TXT = new List<string>(); // 수혈사유 기타 상세

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 기본정보
            IPAT_DD = ""; // 입원일자(YYYYMMDD)
            DSCG_DD = ""; // 퇴원일자(YYYYMMDD)

            // B. 수술정보
            SOPR_YN = ""; // 수술 여부(1.Yes 2.No)
            ASM_OPRM_IPAT_DT.Clear(); // 수술실 입실일시(YYYYMMDDHHMM)
            ASM_OPRM_DSCG_DT.Clear(); // 수술실 퇴실일시(YYYYMMDDHHMM)
            ASM_RCRM_DSCG_DT.Clear(); // 회복실 퇴실일시(YYYYMMDDHHMM)
            SOPR_NM.Clear(); // 수술명
            SOPR_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            LFB_FS_YN = ""; // 척추후방고정술 실시여부(1.Yes 2.No)
            LFB_FS_LVL = ""; // 척추후방고정술 Level(1,2,3)
            KNJN_RPMT_YN = ""; // 슬관절치환술 실시여부(1.Yes 2.No)
            KNJN_RPMT_RGN_CD = ""; // 슬관절치환술 부위(1.단측 2.양측)

            // C. 수혈 체크리스트 사용 현황
            ASM_PRSC_YN = ""; // 처방여부(1.Yes 2.No)
            ASM_PRSC_DT.Clear(); // 처방일시(YYYYMMDDHHMM)
            ASM_PRSC_UNIT_CNT.Clear(); // 처방량(unit)
            ASM_BLTS_CHKLST_USE_YN.Clear(); // 수혈 체크리스트 사용여부(1.Yes 2.No)
            ASM_BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)
            ASM_PRSC_BLTS_DGM_NM.Clear(); // 수혈제제명(BLTS_DGM_NM)
            ASM_PRSC_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            ASM_BLTS_UNIT_CNT.Clear(); // 수혈량(unit)(BLTS_UNIT_CNT)

            // D. 투약정보
            ANM_DIAG_YN = ""; // 빈혈 진단(1.Yes 2.No)
            SICK_SYM.Clear(); // 상병분류기호
            DIAG_NM.Clear(); // 진단명
            ANM_REFM_YN = ""; // 빈혈교정 유무(1.Yes 2.No)
            MDS_NM.Clear(); // 빈혈교정 처방약품명
            MDS_CD.Clear(); // 빈혈교정 처방약품코드

            // E. 검사정보
            HG_EXM_ENFC_YN = ""; // Hb검사 시행여부(1.Yes 2.No)
            ASM_EXM_RST_DT.Clear(); // 검사결과일시(YYYYMMDDHHMM)
            EXM_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            EXM_NM.Clear(); // 검사명
            HG_NUV.Clear(); // 검사결과(g/dL)

            // F. 수혈정보
            BLTS_YN = ""; // 수혈 시행여부(1.Yes 2.No)
            BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)(ASM_BLTS_STA_DT)
            BLTS_END_DT.Clear(); // 수혈종료일시(YYYYMMDDHHMM)(ASM_BLTS_END_DT)
            BLTS_DGM_NM.Clear(); // 수혈제제명
            BLTS_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            BLTS_UNIT_CNT.Clear(); // 수혈량(unit)
            HG_DCR_YN.Clear(); // Hb저하 발생 여부(1.Yes 2.No)
            OPRM_HMRHG_OCUR_YN_CD.Clear(); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
            OPRM_MIDD_HMRHG_QTY.Clear(); // 수술 중 실혈량(ml)
            OPRM_AF_DRN_QTY.Clear(); // 수술 후 배액량(ml)
            BLTS_RS_ETC_YN.Clear(); // 그 외 수혈사유 여부(1.Yes 2.No)
            BLTS_RS_ETC_TXT.Clear(); // 수혈사유 기타 상세        
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // 단일 Row 데이터 조회 (A~E 단일값)
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                // A. 기본 정보
                IPAT_DD = reader["IPAT_DD"].ToString(); // 입원일자(YYYYMMDD)
                DSCG_DD = reader["DSCG_DD"].ToString(); // 퇴원일자(YYYYMMDD)

                // B. 수술 정보 (단일값)
                SOPR_YN = reader["LFB_FS_YN"].ToString(); // 수술 여부(1.Yes 2.No)
                LFB_FS_YN = reader["LFB_FS_YN"].ToString(); // 척추후방고정술 실시여부(1.Yes 2.No)
                LFB_FS_LVL = reader["LFB_FS_LVL"].ToString(); // 척추후방고정술 Level(1,2,3)
                KNJN_RPMT_YN = reader["KNJN_RPMT_YN"].ToString(); // 슬관절치환술 실시여부(1.Yes 2.No)
                KNJN_RPMT_RGN_CD = reader["KNJN_RPMT_RGN_CD"].ToString(); // 슬관절치환술 부위(1.단측 2.양측)

                // C. 수혈 체크리스트 사용 현황 (단일값)
                ASM_PRSC_YN = reader["ASM_PRSC_YN"].ToString(); // 처방여부(1.Yes 2.No)

                // D. 투약 정보 (단일값)
                ANM_DIAG_YN = reader["ANM_DIAG_YN"].ToString(); // 빈혈 진단(1.Yes 2.No)
                ANM_REFM_YN = reader["ANM_REFM_YN"].ToString(); // 빈혈교정 유무(1.Yes 2.No)

                // E. 검사 정보 (단일값)
                HG_EXM_ENFC_YN = reader["HG_EXM_ENFC_YN"].ToString(); // Hb검사 시행여부(1.Yes 2.No)

                // F. 수혈 정보 (단일값)
                BLTS_YN = reader["BLTS_YN"].ToString(); // 수혈 시행여부(1.Yes 2.No)

                return MetroLib.SqlHelper.BREAK;
            });

            // B. 수술 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_SOPR";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_OPRM_IPAT_DT.Add(reader["ASM_OPRM_IPAT_DT"].ToString()); // 수술실 입실일시(YYYYMMDDHHMM)
                ASM_OPRM_DSCG_DT.Add(reader["ASM_OPRM_DSCG_DT"].ToString()); // 수술실 퇴실일시(YYYYMMDDHHMM)
                ASM_RCRM_DSCG_DT.Add(reader["ASM_RCRM_DSCG_DT"].ToString()); // 회복실 퇴실일시(YYYYMMDDHHMM)
                SOPR_NM.Add(reader["SOPR_NM"].ToString()); // 수술명
                SOPR_MDFEE_CD.Add(reader["SOPR_MDFEE_CD"].ToString()); // 수가코드(MDFEE_CD)

                return MetroLib.SqlHelper.CONTINUE;
            });

            // C. 수혈 체크리스트 사용 현황 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_PRSC";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_PRSC_DT.Add(reader["ASM_PRSC_DT"].ToString()); // 처방일시(YYYYMMDDHHMM)
                ASM_PRSC_UNIT_CNT.Add(reader["ASM_PRSC_UNIT_CNT"].ToString()); // 처방량(unit)
                ASM_BLTS_CHKLST_USE_YN.Add(reader["ASM_BLTS_CHKLST_USE_YN"].ToString()); // 수혈 체크리스트 사용여부(1.Yes 2.No)
                ASM_BLTS_STA_DT.Add(reader["ASM_BLTS_STA_DT"].ToString()); // 수혈시작일시(YYYYMMDDHHMM)
                ASM_PRSC_BLTS_DGM_NM.Add(reader["ASM_PRSC_BLTS_DGM_NM"].ToString()); // 수혈제제명(BLTS_DGM_NM)
                ASM_PRSC_MDFEE_CD.Add(reader["ASM_PRSC_MDFEE_CD"].ToString()); // 수가코드(MDFEE_CD)
                ASM_BLTS_UNIT_CNT.Add(reader["ASM_BLTS_UNIT_CNT"].ToString()); // 수혈량(unit)(BLTS_UNIT_CNT)

                return MetroLib.SqlHelper.CONTINUE;
            });

            // D. 투약 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_ANM_DIAG";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                SICK_SYM.Add(reader["SICK_SYM"].ToString()); // 상병분류기호
                DIAG_NM.Add(reader["DIAG_NM"].ToString()); // 진단명

                return MetroLib.SqlHelper.CONTINUE;
            });
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_ANM_REFM";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                MDS_NM.Add(reader["MDS_NM"].ToString()); // 빈혈교정 처방약품명
                MDS_CD.Add(reader["MDS_CD"].ToString()); // 빈혈교정 처방약품코드

                return MetroLib.SqlHelper.CONTINUE;
            });

            // E. 검사 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_EXM";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_EXM_RST_DT.Add(reader["ASM_EXM_RST_DT"].ToString()); // 검사결과일시(YYYYMMDDHHMM)
                EXM_MDFEE_CD.Add(reader["EXM_MDFEE_CD"].ToString()); // 수가코드(MDFEE_CD)
                EXM_NM.Add(reader["EXM_NM"].ToString()); // 검사명
                HG_NUV.Add(reader["HG_NUV"].ToString()); // 검사결과(g/dL)

                return MetroLib.SqlHelper.CONTINUE;
            });

            // F. 수혈 정보 (여러 Row)
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + " FROM TI84_ASM037_BLTS";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                BLTS_STA_DT.Add(reader["BLTS_STA_DT"].ToString()); // 수혈시작일시(YYYYMMDDHHMM)
                BLTS_END_DT.Add(reader["BLTS_END_DT"].ToString()); // 수혈종료일시(YYYYMMDDHHMM)
                BLTS_DGM_NM.Add(reader["BLTS_DGM_NM"].ToString()); // 수혈제제명
                BLTS_MDFEE_CD.Add(reader["BLTS_MDFEE_CD"].ToString()); // 수가코드(MDFEE_CD)
                BLTS_UNIT_CNT.Add(reader["BLTS_UNIT_CNT"].ToString()); // 수혈량(unit)
                HG_DCR_YN.Add(reader["HG_DCR_YN"].ToString()); // Hb저하 발생 여부(1.Yes 2.No)
                OPRM_HMRHG_OCUR_YN_CD.Add(reader["OPRM_HMRHG_OCUR_YN_CD"].ToString()); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
                OPRM_MIDD_HMRHG_QTY.Add(reader["OPRM_MIDD_HMRHG_QTY"].ToString()); // 수술 중 실혈량(ml)
                OPRM_AF_DRN_QTY.Add(reader["OPRM_AF_DRN_QTY"].ToString()); // 수술 후 배액량(ml)
                BLTS_RS_ETC_YN.Add(reader["BLTS_RS_ETC_YN"].ToString()); // 그 외 수혈사유 여부(1.Yes 2.No)
                BLTS_RS_ETC_TXT.Add(reader["BLTS_RS_ETC_TX"].ToString()); // 수혈사유 기타 상세

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
                sql += "DELETE FROM TI84_ASM037 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_SOPR WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_PRSC WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_ANM_DIAG WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_ANM_REFM WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_EXM WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM037_BLTS WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // 단일 Row 저장 (A~F의 단일값)
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM037(";
            sql += Environment.NewLine + " FORM, KEYSTR, SEQ, VER";
            sql += Environment.NewLine + " , IPAT_DD, DSCG_DD"; // A. 기본정보
            sql += Environment.NewLine + " , SOPR_YN, LFB_FS_YN, LFB_FS_LVL, KNJN_RPMT_YN, KNJN_RPMT_RGN_CD"; // B. 수술정보(단일)
            sql += Environment.NewLine + " , ASM_PRSC_YN"; // C. 수혈 체크리스트(단일)
            sql += Environment.NewLine + " , ANM_DIAG_YN, ANM_REFM_YN"; // D. 투약정보(단일)
            sql += Environment.NewLine + " , HG_EXM_ENFC_YN"; // E. 검사정보(단일)
            sql += Environment.NewLine + " , BLTS_YN"; // F. 수혈정보(단일)
            sql += Environment.NewLine + ")";
            sql += Environment.NewLine + "VALUES(";
            sql += Environment.NewLine + " ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?";
            sql += Environment.NewLine + " , ?, ?, ?, ?";
            sql += Environment.NewLine + " , ?";
            sql += Environment.NewLine + " , ?, ?";
            sql += Environment.NewLine + " , ?";
            sql += Environment.NewLine + " , ?";
            sql += Environment.NewLine + ")";

            para.Clear();
            para.Add(form); // FORM
            para.Add(KEYSTR); // KEYSTR
            para.Add(SEQ); // SEQ
            para.Add(ver); // VER
            para.Add(IPAT_DD); // 입원일자(YYYYMMDD)
            para.Add(DSCG_DD); // 퇴원일자(YYYYMMDD)
            para.Add(SOPR_YN); // 수술 여부(1.Yes 2.No)
            para.Add(LFB_FS_YN); // 척추후방고정술 실시여부
            para.Add(LFB_FS_LVL); // 척추후방고정술 Level
            para.Add(KNJN_RPMT_YN); // 슬관절치환술 실시여부
            para.Add(KNJN_RPMT_RGN_CD); // 슬관절치환술 부위
            para.Add(ASM_PRSC_YN); // 처방여부
            para.Add(ANM_DIAG_YN); // 빈혈 진단
            para.Add(ANM_REFM_YN); // 빈혈교정 유무
            para.Add(HG_EXM_ENFC_YN); // Hb검사 시행여부
            para.Add(BLTS_YN); // 수혈 시행여부

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // B. 수술 정보 (여러 Row)
            for (int i = 0; i < ASM_OPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_SOPR(FORM, KEYSTR, SEQ, SEQNO, ASM_OPRM_IPAT_DT, ASM_OPRM_DSCG_DT, ASM_RCRM_DSCG_DT, SOPR_NM, SOPR_MDFEE_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_OPRM_IPAT_DT[i]);
                para.Add(ASM_OPRM_DSCG_DT[i]);
                para.Add(ASM_RCRM_DSCG_DT[i]);
                para.Add(SOPR_NM[i]);
                para.Add(SOPR_MDFEE_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // C. 수혈 체크리스트 사용 현황 (여러 Row)
            for (int i = 0; i < ASM_PRSC_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_PRSC(FORM, KEYSTR, SEQ, SEQNO, ASM_PRSC_DT, ASM_PRSC_UNIT_CNT, ASM_BLTS_CHKLST_USE_YN, ASM_BLTS_STA_DT, ASM_PRSC_BLTS_DGM_NM, ASM_PRSC_MDFEE_CD, ASM_BLTS_UNIT_CNT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_PRSC_DT[i]);
                para.Add(ASM_PRSC_UNIT_CNT[i]);
                para.Add(ASM_BLTS_CHKLST_USE_YN[i]);
                para.Add(ASM_BLTS_STA_DT[i]);
                para.Add(ASM_PRSC_BLTS_DGM_NM[i]);
                para.Add(ASM_PRSC_MDFEE_CD[i]);
                para.Add(ASM_BLTS_UNIT_CNT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // D. 투약 정보 (여러 Row)
            for (int i = 0; i < SICK_SYM.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_ANM_DIAG(FORM, KEYSTR, SEQ, SEQNO, SICK_SYM, DIAG_NM)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SICK_SYM[i]);
                para.Add(DIAG_NM[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
            for (int i = 0; i < MDS_NM.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_ANM_REFM(FORM, KEYSTR, SEQ, SEQNO, MDS_NM, MDS_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(MDS_NM[i]);
                para.Add(MDS_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // E. 검사 정보 (여러 Row)
            for (int i = 0; i < ASM_EXM_RST_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_EXM(FORM, KEYSTR, SEQ, SEQNO, ASM_EXM_RST_DT, EXM_MDFEE_CD, EXM_NM, HG_NUV)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_EXM_RST_DT[i]);
                para.Add(EXM_MDFEE_CD[i]);
                para.Add(EXM_NM[i]);
                para.Add(HG_NUV[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // F. 수혈 정보 (여러 Row)
            for (int i = 0; i < BLTS_STA_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_BLTS(FORM, KEYSTR, SEQ, SEQNO, BLTS_STA_DT, BLTS_END_DT, BLTS_DGM_NM, BLTS_MDFEE_CD, BLTS_UNIT_CNT, HG_DCR_YN, OPRM_HMRHG_OCUR_YN_CD, OPRM_MIDD_HMRHG_QTY, OPRM_AF_DRN_QTY, BLTS_RS_ETC_YN, BLTS_RS_ETC_TXT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(BLTS_STA_DT[i]);
                para.Add(BLTS_END_DT[i]);
                para.Add(BLTS_DGM_NM[i]);
                para.Add(BLTS_MDFEE_CD[i]);
                para.Add(BLTS_UNIT_CNT[i]);
                para.Add(HG_DCR_YN[i]);
                para.Add(OPRM_HMRHG_OCUR_YN_CD[i]);
                para.Add(OPRM_MIDD_HMRHG_QTY[i]);
                para.Add(OPRM_AF_DRN_QTY[i]);
                para.Add(BLTS_RS_ETC_YN[i]);
                para.Add(BLTS_RS_ETC_TXT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // 1. 단일 Row UPDATE (A~F 단일값)
            string sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM037";
            sql += Environment.NewLine + " SET IPAT_DD=?, DSCG_DD=?"; // A. 기본정보
            sql += Environment.NewLine + " , SOPR_YN=?, LFB_FS_YN=?, LFB_FS_LVL=?, KNJN_RPMT_YN=?, KNJN_RPMT_RGN_CD=?"; // B. 수술정보(단일)
            sql += Environment.NewLine + " , ASM_PRSC_YN=?"; // C. 수혈 체크리스트(단일)
            sql += Environment.NewLine + " , ANM_DIAG_YN=?, ANM_REFM_YN=?"; // D. 투약정보(단일)
            sql += Environment.NewLine + " , HG_EXM_ENFC_YN=?"; // E. 검사정보(단일)
            sql += Environment.NewLine + " , BLTS_YN=?"; // F. 수혈정보(단일)
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            para.Clear();
            para.Add(IPAT_DD); // 입원일자(YYYYMMDD)
            para.Add(DSCG_DD); // 퇴원일자(YYYYMMDD)
            para.Add(SOPR_YN); // 수술 여부(1.Yes 2.No)
            para.Add(LFB_FS_YN); // 척추후방고정술 실시여부
            para.Add(LFB_FS_LVL); // 척추후방고정술 Level
            para.Add(KNJN_RPMT_YN); // 슬관절치환술 실시여부
            para.Add(KNJN_RPMT_RGN_CD); // 슬관절치환술 부위
            para.Add(ASM_PRSC_YN); // 처방여부
            para.Add(ANM_DIAG_YN); // 빈혈 진단
            para.Add(ANM_REFM_YN); // 빈혈교정 유무
            para.Add(HG_EXM_ENFC_YN); // Hb검사 시행여부
            para.Add(BLTS_YN); // 수혈 시행여부

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 2. 기존 상세 테이블 데이터 삭제
            sql = "DELETE FROM TI84_ASM037_SOPR WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "DELETE FROM TI84_ASM037_PRSC WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "DELETE FROM TI84_ASM037_ANM_DIAG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "DELETE FROM TI84_ASM037_ANM_REFM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "DELETE FROM TI84_ASM037_EXM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            sql = "DELETE FROM TI84_ASM037_BLTS WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            // 3. 상세 테이블 재삽입

            // B. 수술 정보 (여러 Row)
            for (int i = 0; i < ASM_OPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_SOPR(FORM, KEYSTR, SEQ, SEQNO, ASM_OPRM_IPAT_DT, ASM_OPRM_DSCG_DT, ASM_RCRM_DSCG_DT, SOPR_NM, SOPR_MDFEE_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_OPRM_IPAT_DT[i]);
                para.Add(ASM_OPRM_DSCG_DT[i]);
                para.Add(ASM_RCRM_DSCG_DT[i]);
                para.Add(SOPR_NM[i]);
                para.Add(SOPR_MDFEE_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // C. 수혈 체크리스트 사용 현황 (여러 Row)
            for (int i = 0; i < ASM_PRSC_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_PRSC(FORM, KEYSTR, SEQ, SEQNO, ASM_PRSC_DT, ASM_PRSC_UNIT_CNT, ASM_BLTS_CHKLST_USE_YN, ASM_BLTS_STA_DT, ASM_PRSC_BLTS_DGM_NM, ASM_PRSC_MDFEE_CD, ASM_BLTS_UNIT_CNT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_PRSC_DT[i]);
                para.Add(ASM_PRSC_UNIT_CNT[i]);
                para.Add(ASM_BLTS_CHKLST_USE_YN[i]);
                para.Add(ASM_BLTS_STA_DT[i]);
                para.Add(ASM_PRSC_BLTS_DGM_NM[i]);
                para.Add(ASM_PRSC_MDFEE_CD[i]);
                para.Add(ASM_BLTS_UNIT_CNT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // D. 투약 정보 (여러 Row)
            for (int i = 0; i < SICK_SYM.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_ANM_DIAG(FORM, KEYSTR, SEQ, SEQNO, SICK_SYM, DIAG_NM)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SICK_SYM[i]);
                para.Add(DIAG_NM[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
            for (int i = 0; i < MDS_NM.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_ANM_REFM(FORM, KEYSTR, SEQ, SEQNO, MDS_NM, MDS_CD)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(MDS_NM[i]);
                para.Add(MDS_CD[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // E. 검사 정보 (여러 Row)
            for (int i = 0; i < ASM_EXM_RST_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_EXM(FORM, KEYSTR, SEQ, SEQNO, ASM_EXM_RST_DT, EXM_MDFEE_CD, EXM_NM, HG_NUV)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(ASM_EXM_RST_DT[i]);
                para.Add(EXM_MDFEE_CD[i]);
                para.Add(EXM_NM[i]);
                para.Add(HG_NUV[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // F. 수혈 정보 (여러 Row)
            for (int i = 0; i < BLTS_STA_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM037_BLTS(FORM, KEYSTR, SEQ, SEQNO, BLTS_STA_DT, BLTS_END_DT, BLTS_DGM_NM, BLTS_MDFEE_CD, BLTS_UNIT_CNT, HG_DCR_YN, OPRM_HMRHG_OCUR_YN_CD, OPRM_MIDD_HMRHG_QTY, OPRM_AF_DRN_QTY, BLTS_RS_ETC_YN, BLTS_RS_ETC_TXT)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(BLTS_STA_DT[i]);
                para.Add(BLTS_END_DT[i]);
                para.Add(BLTS_DGM_NM[i]);
                para.Add(BLTS_MDFEE_CD[i]);
                para.Add(BLTS_UNIT_CNT[i]);
                para.Add(HG_DCR_YN[i]);
                para.Add(OPRM_HMRHG_OCUR_YN_CD[i]);
                para.Add(OPRM_MIDD_HMRHG_QTY[i]);
                para.Add(OPRM_AF_DRN_QTY[i]);
                para.Add(BLTS_RS_ETC_YN[i]);
                para.Add(BLTS_RS_ETC_TXT[i]);
                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 필요시 공통 테이블 저장 등 추가
            Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM037 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_SOPR WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_PRSC WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_ANM_DIAG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_ANM_REFM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_EXM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_BLTS WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

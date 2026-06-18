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
                SOPR_YN = reader["SOPR_YN"].ToString(); // 수술 여부(1.Yes 2.No)
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
                BLTS_RS_ETC_TXT.Add(reader["BLTS_RS_ETC_TXT"].ToString()); // 수혈사유 기타 상세

                return MetroLib.SqlHelper.CONTINUE;
            });
        }

        public void ReadDataFromEMR(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            ClearMe();

            // A. 기본정보
            IPAT_DD = BDEDT; // 입원일자(YYYYMMDD)
            DSCG_DD = BDODT; // 퇴원일자(YYYYMMDD)


            // B. 수술정보
            SOPR_YN = "2"; // 수술 여부(1.Yes 2.No)
            LFB_FS_YN = "2"; // 척추후방고정술 실시여부(1.Yes 2.No)
            LFB_FS_LVL = ""; // 척추후방고정술 Level(1,2,3)
            KNJN_RPMT_YN = "2"; // 슬관절치환술 실시여부(1.Yes 2.No)
            KNJN_RPMT_RGN_CD = ""; // 슬관절치환술 부위(1.단측 2.양측)

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TU01 U01 (NOLOCK)";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + A04_BEDODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string opdt = row["OPDT"].ToString();
                string opsdt = row["OPSDT"].ToString();
                string orinhr = row["ORINHR"].ToString();
                string orinmn = row["ORINMN"].ToString();
                string opodt = row["OPODT"].ToString();
                string opohr = row["OPOHR"].ToString();
                string opomn = row["OPOMN"].ToString();

                if (opsdt == "") opsdt = opdt;
                if (orinhr.Length == 1) orinhr = "0" + orinhr;
                if (orinmn.Length == 1) orinmn = "0" + orinmn;
                if (opohr.Length == 1) opohr = "0" + opohr;
                if (opomn.Length == 1) opomn = "0" + opomn;
                if (opodt == "")
                {
                    opohr = "";
                    opomn = "";
                }

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A02.ISPCD, A02.PRKNM";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + PID + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + opdt + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row2["ISPCD"].ToString();

                    ASM_OPRM_IPAT_DT.Add(opsdt + orinhr + orinmn); // 수술실 입실일시(YYYYMMDDHHMM)
                    ASM_OPRM_DSCG_DT.Add(opodt + opohr + opomn); // 수술실 퇴실일시(YYYYMMDDHHMM)
                    ASM_RCRM_DSCG_DT.Add(opodt + opohr + opomn); // 회복실 퇴실일시(YYYYMMDDHHMM)
                    SOPR_NM.Add(row2["PRKNM"].ToString()); // 수술명
                    SOPR_MDFEE_CD.Add(ispcd); // 수가코드(MDFEE_CD)

                    if (ispcd.StartsWith("N0469") || ispcd.StartsWith("N2470"))
                    {
                        SOPR_YN = "1"; // 수술 여부(1.Yes 2.No)
                        LFB_FS_YN = "1"; // 척추후방고정술 실시여부(1.Yes 2.No)
                    }
                    if (ispcd.StartsWith("N2072"))
                    {
                        SOPR_YN = "1"; // 수술 여부(1.Yes 2.No)
                        KNJN_RPMT_YN = "1"; // 슬관절치환술 실시여부(1.Yes 2.No)
                        if (KNJN_RPMT_RGN_CD == "")
                        {
                            KNJN_RPMT_RGN_CD = "1"; // 한 개면 단측
                        }
                        else if (KNJN_RPMT_RGN_CD == "1")
                        {
                            KNJN_RPMT_RGN_CD = "2"; // 두 개 이상이면 양측
                        }
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });

                return MetroLib.SqlHelper.CONTINUE;
            });

            // C. 수혈 체크리스트 사용 현황
            ASM_PRSC_YN = "2"; // 처방여부(1.Yes 2.No)

            // F. 수혈정보
            BLTS_YN = "2"; // 수혈 시행여부(1.Yes 2.No)


            Dictionary<string, int> bldIndexMap = new Dictionary<string, int>();

            if (1 == 1)
            {
                // TT31 사용
                sql = "";
                sql += System.Environment.NewLine + "SELECT T31.EXDT, T31.PRICD, T31.CALQY, A02.ISPCD, A02.PRKNM";
                sql += System.Environment.NewLine + "  FROM TT31 T31 (NOLOCK) INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=T31.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=T31.PRICD AND X.CREDT<=T31.EXDT)";
                sql += System.Environment.NewLine + " WHERE T31.PID='" + PID + "'";
                sql += System.Environment.NewLine + "   AND T31.BDEDT='" + A04_BEDEDT + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(T31.CHRLT,'') <> '3'";
                sql += System.Environment.NewLine + "   AND ISNULL(T31.CALQY,0) <> 0";
                sql += System.Environment.NewLine + "   AND ISNULL(T31.TTAMT,0) <> 0";
                sql += System.Environment.NewLine + " ORDER BY T31.EXDT";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
                {
                    string bldodt = row["EXDT"].ToString();
                    string bldcd = row["ISPCD"].ToString();
                    string prknm = row["PRKNM"].ToString();

                    decimal calqy = 0;
                    decimal.TryParse(row["CALQY"].ToString(), out calqy);
                    string calqyText = calqy.ToString("0.#############################");

                    if (CUtil_ASM010.IsBLDCode(bldcd))
                    {
                        int idx;
                        if (!bldIndexMap.TryGetValue(bldcd, out idx))
                        {
                            idx = ASM_PRSC_MDFEE_CD.Count;
                            bldIndexMap[bldcd] = idx;

                            // C. 수혈 체크리스트 사용 현황
                            ASM_PRSC_DT.Add(bldodt); // 처방일시
                            ASM_PRSC_UNIT_CNT.Add(calqyText); // 처방량
                            ASM_BLTS_CHKLST_USE_YN.Add(""); // 수혈 체크리스트 사용여부
                            ASM_BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                            ASM_PRSC_BLTS_DGM_NM.Add(prknm); // 수혈제제명
                            ASM_PRSC_MDFEE_CD.Add(bldcd); // 수가코드
                            ASM_BLTS_UNIT_CNT.Add(calqyText); // 수혈량

                            // F. 수혈정보
                            BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                            BLTS_END_DT.Add(bldodt); // 수혈종료일시
                            BLTS_DGM_NM.Add(prknm); // 수혈제제명
                            BLTS_MDFEE_CD.Add(bldcd); // 수가코드
                            BLTS_UNIT_CNT.Add(calqyText); // 수혈량
                            HG_DCR_YN.Add(""); // Hb저하 발생 여부
                            OPRM_HMRHG_OCUR_YN_CD.Add(""); // 수술 관련 실혈 발생 여부
                            OPRM_MIDD_HMRHG_QTY.Add(""); // 수술 중 실혈량
                            OPRM_AF_DRN_QTY.Add(""); // 수술 후 배액량
                            BLTS_RS_ETC_YN.Add(""); // 그 외 수혈사유 여부
                            BLTS_RS_ETC_TXT.Add(""); // 수혈사유 기타 상세
                        }
                        else
                        {
                            decimal unitCnt = 0;
                            decimal.TryParse(ASM_PRSC_UNIT_CNT[idx], out unitCnt);
                            ASM_PRSC_UNIT_CNT[idx] = (unitCnt + calqy).ToString("0.#############################");

                            unitCnt = 0;
                            decimal.TryParse(ASM_BLTS_UNIT_CNT[idx], out unitCnt);
                            ASM_BLTS_UNIT_CNT[idx] = (unitCnt + calqy).ToString("0.#############################");

                            unitCnt = 0;
                            decimal.TryParse(BLTS_UNIT_CNT[idx], out unitCnt);
                            BLTS_UNIT_CNT[idx] = (unitCnt + calqy).ToString("0.#############################");

                            BLTS_END_DT[idx] = bldodt; // 수혈종료일시
                        }
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });

                for (int i = ASM_PRSC_UNIT_CNT.Count - 1; i >= 0; i--)
                {
                    decimal unitCnt = 0;
                    decimal.TryParse(ASM_PRSC_UNIT_CNT[i], out unitCnt);

                    if (unitCnt == 0)
                    {
                        ASM_PRSC_DT.RemoveAt(i);
                        ASM_PRSC_UNIT_CNT.RemoveAt(i);
                        ASM_BLTS_CHKLST_USE_YN.RemoveAt(i);
                        ASM_BLTS_STA_DT.RemoveAt(i);
                        ASM_PRSC_BLTS_DGM_NM.RemoveAt(i);
                        ASM_PRSC_MDFEE_CD.RemoveAt(i);
                        ASM_BLTS_UNIT_CNT.RemoveAt(i);

                        BLTS_STA_DT.RemoveAt(i);
                        BLTS_END_DT.RemoveAt(i);
                        BLTS_DGM_NM.RemoveAt(i);
                        BLTS_MDFEE_CD.RemoveAt(i);
                        BLTS_UNIT_CNT.RemoveAt(i);
                        HG_DCR_YN.RemoveAt(i);
                        OPRM_HMRHG_OCUR_YN_CD.RemoveAt(i);
                        OPRM_MIDD_HMRHG_QTY.RemoveAt(i);
                        OPRM_AF_DRN_QTY.RemoveAt(i);
                        BLTS_RS_ETC_YN.RemoveAt(i);
                        BLTS_RS_ETC_TXT.RemoveAt(i);
                    }
                }

                if (ASM_PRSC_UNIT_CNT.Count > 0)
                {
                    ASM_PRSC_YN = "1";
                    BLTS_YN = "1";
                }
            }
            else
            {
                // TB08 사용
                // 사용하지 않으면 삭제하자.
                sql = "";
                sql += System.Environment.NewLine + "SELECT B08.BLDODT, A02.PRICD, A02.ISPCD, A02.PRKNM";
                sql += System.Environment.NewLine + "  FROM TB08 B08 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=B08.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=B08.OCD AND X.CREDT<=B08.BLDODT)";
                sql += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=B08.BLDODT)";
                sql += System.Environment.NewLine + " WHERE B08.PID='" + PID + "'";
                sql += System.Environment.NewLine + "   AND B08.BEDEDT='" + A04_BEDEDT + "'";
                sql += System.Environment.NewLine + "   AND B08.BLDODT>='" + A04_BEDEDT + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(B08.BLDRTNDT,'')=''";
                sql += System.Environment.NewLine + " ORDER BY B08.BLDODT";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
                {
                    string pricd = row["PRICD"].ToString();
                    string bldodt = row["BLDODT"].ToString();

                    if (pricd.StartsWith("X"))
                    {
                        string sql2 = "";
                        sql2 += Environment.NewLine + "SELECT A02.ISPCD, A02.PRKNM";
                        sql2 += Environment.NewLine + "  FROM TA02A A02A INNER JOIN TA02 A02 ON A02.PRICD=A02A.SPCD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A02.PRICD AND X.CREDT<='" + bldodt + "')";
                        sql2 += Environment.NewLine + " WHERE A02A.PRICD='" + pricd + "'";
                        sql2 += Environment.NewLine + "   AND A02A.CREDT=(SELECT MAX(X.CREDT) FROM TA02A X WHERE X.PRICD=A02A.PRICD AND X.CREDT<='" + bldodt + "')";

                        MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                        {
                            string bldcd = row2["ISPCD"].ToString();
                            string prknm = row2["PRKNM"].ToString();

                            if (CUtil_ASM010.IsBLDCode(bldcd))
                            {
                                ASM_PRSC_YN = "1";
                                BLTS_YN = "1";

                                int idx;
                                if (!bldIndexMap.TryGetValue(bldcd, out idx))
                                {
                                    idx = ASM_PRSC_MDFEE_CD.Count;
                                    bldIndexMap[bldcd] = idx;

                                    // C. 수혈 체크리스트 사용 현황
                                    ASM_PRSC_DT.Add(bldodt); // 처방일시
                                    ASM_PRSC_UNIT_CNT.Add("1"); // 처방량
                                    ASM_BLTS_CHKLST_USE_YN.Add(""); // 수혈 체크리스트 사용여부
                                    ASM_BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                                    ASM_PRSC_BLTS_DGM_NM.Add(prknm); // 수혈제제명
                                    ASM_PRSC_MDFEE_CD.Add(bldcd); // 수가코드
                                    ASM_BLTS_UNIT_CNT.Add("1"); // 수혈량

                                    // F. 수혈정보
                                    BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                                    BLTS_END_DT.Add(bldodt); // 수혈종료일시
                                    BLTS_DGM_NM.Add(prknm); // 수혈제제명
                                    BLTS_MDFEE_CD.Add(bldcd); // 수가코드
                                    BLTS_UNIT_CNT.Add("1"); // 수혈량
                                    HG_DCR_YN.Add(""); // Hb저하 발생 여부
                                    OPRM_HMRHG_OCUR_YN_CD.Add(""); // 수술 관련 실혈 발생 여부
                                    OPRM_MIDD_HMRHG_QTY.Add(""); // 수술 중 실혈량
                                    OPRM_AF_DRN_QTY.Add(""); // 수술 후 배액량
                                    BLTS_RS_ETC_YN.Add(""); // 그 외 수혈사유 여부
                                    BLTS_RS_ETC_TXT.Add(""); // 수혈사유 기타 상세
                                }
                                else
                                {
                                    int unitCnt = 0;
                                    int.TryParse(ASM_PRSC_UNIT_CNT[idx], out unitCnt);
                                    ASM_PRSC_UNIT_CNT[idx] = (unitCnt + 1).ToString();

                                    unitCnt = 0;
                                    int.TryParse(ASM_BLTS_UNIT_CNT[idx], out unitCnt);
                                    ASM_BLTS_UNIT_CNT[idx] = (unitCnt + 1).ToString();

                                    unitCnt = 0;
                                    int.TryParse(BLTS_UNIT_CNT[idx], out unitCnt);
                                    BLTS_UNIT_CNT[idx] = (unitCnt + 1).ToString();
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
                            ASM_PRSC_YN = "1";
                            BLTS_YN = "1";

                            int idx;
                            if (!bldIndexMap.TryGetValue(bldcd, out idx))
                            {
                                idx = ASM_PRSC_MDFEE_CD.Count;
                                bldIndexMap[bldcd] = idx;

                                // C. 수혈 체크리스트 사용 현황
                                ASM_PRSC_DT.Add(bldodt); // 처방일시
                                ASM_PRSC_UNIT_CNT.Add("1"); // 처방량
                                ASM_BLTS_CHKLST_USE_YN.Add(""); // 수혈 체크리스트 사용여부
                                ASM_BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                                ASM_PRSC_BLTS_DGM_NM.Add(prknm); // 수혈제제명
                                ASM_PRSC_MDFEE_CD.Add(bldcd); // 수가코드
                                ASM_BLTS_UNIT_CNT.Add("1"); // 수혈량

                                // F. 수혈정보
                                BLTS_STA_DT.Add(bldodt); // 수혈시작일시
                                BLTS_END_DT.Add(bldodt); // 수혈종료일시
                                BLTS_DGM_NM.Add(prknm); // 수혈제제명
                                BLTS_MDFEE_CD.Add(bldcd); // 수가코드
                                BLTS_UNIT_CNT.Add("1"); // 수혈량
                                HG_DCR_YN.Add(""); // Hb저하 발생 여부
                                OPRM_HMRHG_OCUR_YN_CD.Add(""); // 수술 관련 실혈 발생 여부
                                OPRM_MIDD_HMRHG_QTY.Add(""); // 수술 중 실혈량
                                OPRM_AF_DRN_QTY.Add(""); // 수술 후 배액량
                                BLTS_RS_ETC_YN.Add(""); // 그 외 수혈사유 여부
                                BLTS_RS_ETC_TXT.Add(""); // 수혈사유 기타 상세
                            }
                            else
                            {
                                int unitCnt = 0;
                                int.TryParse(ASM_PRSC_UNIT_CNT[idx], out unitCnt);
                                ASM_PRSC_UNIT_CNT[idx] = (unitCnt + 1).ToString();

                                unitCnt = 0;
                                int.TryParse(ASM_BLTS_UNIT_CNT[idx], out unitCnt);
                                ASM_BLTS_UNIT_CNT[idx] = (unitCnt + 1).ToString();

                                unitCnt = 0;
                                int.TryParse(BLTS_UNIT_CNT[idx], out unitCnt);
                                BLTS_UNIT_CNT[idx] = (unitCnt + 1).ToString();
                                BLTS_END_DT[idx] = bldodt; // 수혈종료일시
                            }
                        }
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }

            // 빈혈 진단여부를 설정하기 위한 사전작업
            string minAsmPrscDt = "";
            for (int i = 0; i < ASM_PRSC_DT.Count; i++)
            {
                string asmPrscDt = ASM_PRSC_DT[i];
                if (asmPrscDt.Length >= 8)
                {
                    asmPrscDt = asmPrscDt.Substring(0, 8);

                    if (minAsmPrscDt == "" || minAsmPrscDt.CompareTo(asmPrscDt) > 0)
                    {
                        minAsmPrscDt = asmPrscDt;
                    }
                }
            }

            // D. 투약정보
            ANM_DIAG_YN = "2"; // 빈혈 진단(1.Yes 2.No)
            ANM_REFM_YN = "2"; // 빈혈교정 유무(1.Yes 2.No)

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

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string ispcd = row["ISPCD"].ToString();
                if (CUtil_ASM037.IsANMREFMCode(ispcd) && MDS_CD.Contains(ispcd) == false)
                {
                    ANM_REFM_YN = "1"; // 빈혈교정 유무(1.Yes 2.No)
                    MDS_NM.Add(row["PRKNM"].ToString()); // 빈혈교정 처방약품명
                    MDS_CD.Add(ispcd); // 빈혈교정 처방약품코드
                }

                return MetroLib.SqlHelper.CONTINUE;
            });
            
            // E. 검사정보
            HG_EXM_ENFC_YN = "2"; // Hb검사 시행여부(1.Yes 2.No)
            ASM_EXM_RST_DT.Clear(); // 검사결과일시(YYYYMMDDHHMM)
            EXM_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            EXM_NM.Clear(); // 검사명
            HG_NUV.Clear(); // 검사결과(g/dL)

            if (MetroLib.Util.ValDt(A04_BEDEDT) == true)
            {
                string exm_frdt = MetroLib.Util.AddMonth(A04_BEDEDT, -3);
                sql = "";
                sql += Environment.NewLine + "SELECT DISTINCT SPCNO, SEX, AGE, RCVDT, RCVTM, STSCD, SPCFOOTSEQ, PTHRPT, DIAGNOSIS, ORDDT, SPCNM, MAJNM";
                sql += Environment.NewLine + "  FROM TC201 (NOLOCK)";
                sql += Environment.NewLine + " WHERE PTID='" + PID + "'";
                sql += Environment.NewLine + "   AND ORDDT>='" + exm_frdt + "'";
                sql += Environment.NewLine + "   AND ORDDT<='" + A04_BEDODT + "'";
                sql += Environment.NewLine + "   AND (CANCELFG != '1' OR CANCELFG IS NULL)";
                sql += Environment.NewLine + "   AND STSCD>='1'";
                sql += Environment.NewLine + " ORDER BY ORDDT, SPCNO";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string spcno = row["SPCNO"].ToString();
                    string orddt = row["ORDDT"].ToString();

                    string sql2 = "";
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT A.SPCNO, A.TESTCD, A.SEQ, A.APPDT, A.APPTM, A.TESTDIV, A.TESTRSTTYPE, A.HEADTESTCD";
                    sql2 += Environment.NewLine + "     , A.SITECD, A.PTID, A.RSTFG, A.WSCD, A.SPCCD, A.RSTVAL, A.REFERCHK, A.PANICCHK";
                    sql2 += Environment.NewLine + "     , A.DELTACHK, A.PICKCD, A.MICRORSTTYPE, A.STAINSEQ, A.CULTURESEQ, A.FOOTSEQ, A.MODIFYFG";
                    sql2 += Environment.NewLine + "     , A.DLYFG, A.STATFG, A.MANSTATFG, A.EQUIPCD, A.VFYID, A.VFYDT, A.VFYTM, A.PRTFG, A.PRTDT";
                    sql2 += Environment.NewLine + "     , A.PRTTM, A.STSCD, A.CANCELFG, A.CANCELCD, A.REGDR, A.SPECDR, B.ABBRNM";
                    sql2 += Environment.NewLine + "     , B.DATATYPE, B.DATALEN, B.KEYPAD, B.RSTFG, B.NORSTFG, B.QUERYFG, B.NORSTQUERYFG, B.ONOFFFG, B.WEEKDAYDIV";
                    sql2 += Environment.NewLine + "     , A02.ISPCD, A02.PRKNM";
                    sql2 += Environment.NewLine + "  FROM TC301 A (NOLOCK) INNER JOIN TC001 B (NOLOCK) ON B.TESTCD=A.TESTCD AND B.APPDT=A.APPDT AND B.APPTM=A.APPTM";
                    sql2 += Environment.NewLine + "                        INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=A.TESTCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=A.TESTCD AND X.CREDT<='" + orddt + "')";
                    sql2 += Environment.NewLine + "                        INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<='" + orddt + "')";
                    sql2 += Environment.NewLine + " WHERE A.SPCNO='" + spcno + "'";
                    sql2 += Environment.NewLine + "   AND (A.CANCELFG != '1' OR A.CANCELFG IS NULL)";
                    sql2 += Environment.NewLine + "   AND A.STSCD>='7'";
                    sql2 += Environment.NewLine + " ORDER BY A.SEQ";

                    MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();

                        string ispcd = row2["ISPCD"].ToString();
                        if (ispcd.StartsWith("D000205") || ispcd.StartsWith("D0003") || ispcd.StartsWith("D0005"))
                        {
                            string rstval = row2["RSTVAL"].ToString();
                            string vfyDt = row2["VFYDT"].ToString();

                            HG_EXM_ENFC_YN = "1"; // Hb검사 시행여부(1.Yes 2.No)
                            ASM_EXM_RST_DT.Add(vfyDt + row2["VFYTM"].ToString()); // 검사결과일시(YYYYMMDDHHMM)
                            EXM_MDFEE_CD.Add(ispcd); // 수가코드(MDFEE_CD)
                            EXM_NM.Add(row2["PRKNM"].ToString()); // 검사명
                            HG_NUV.Add(rstval); // 검사결과(g/dL)

                            double hgNuv = 0;
                            if (double.TryParse(rstval, out hgNuv) && hgNuv <= 10 && minAsmPrscDt != "" && vfyDt.CompareTo(minAsmPrscDt) < 0)
                            {
                                ANM_DIAG_YN = "1"; // 빈혈 진단(1.Yes 2.No)
                            }
                        }
                        return MetroLib.StrHelper.CONTINUE;
                    });

                    return MetroLib.StrHelper.CONTINUE;
                });
            }

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
            sql += "DELETE FROM TI84_ASM037_SOPR WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_PRSC WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_ANM_DIAG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_ANM_REFM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_EXM WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM037_BLTS WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

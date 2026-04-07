using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM024_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM024"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "ICU"; // 업무상세코드

        // A.기본정보
        public string IPAT_DD; // 입원일자(YYYYMMDD)

        // B.중환자실 입퇴실 정보
        public List<string> SPRM_IPAT_DT = new List<string>(); // 입실일시
        public List<string> SPRM_IPAT_RS_CD = new List<string>(); // 입실사유
        public List<string> RE_IPAT_RS_TXT = new List<string>(); // 재입실상세
        public List<string> IPAT_RS_ETC_TXT = new List<string>(); // 입실사유기타
        public List<string> ASM_SPRM_DSCG_RST_CD = new List<string>(); // 퇴실현황
        public List<string> SPRM_DSCG_DT = new List<string>(); // 퇴실일시
        public List<string> SPRM_RE_IPAT_PLAN_YN = new List<string>(); // 재입실 계획여부

        // C.사망현황
        public string ASM_DEATH_YN; // 사망여무
        public string DEATH_DT; // 사망일시
        public string WLST_RCD_YN; // 연명의료중단등결정이행서작성여부
        public string WLST_RCD_DT; // 작성일자
        public string WLST_RCD_CD; // 이행내용
        public string WLST_RCD_ETC_TXT; // 그 밖의 연명의료 상세

        // D. 기타 사항
        //public string APND_DATA_NO; // 첨부



        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            IPAT_DD = ""; // 입원일자(YYYYMMDD)

            // B.중환자실 입퇴실 정보
            SPRM_IPAT_DT.Clear(); // 입실일시
            SPRM_IPAT_RS_CD.Clear(); // 입실사유
            RE_IPAT_RS_TXT.Clear(); // 재입실상세
            IPAT_RS_ETC_TXT.Clear(); // 입실사유기타
            ASM_SPRM_DSCG_RST_CD.Clear(); // 퇴실현황
            SPRM_DSCG_DT.Clear(); // 퇴실일시
            SPRM_RE_IPAT_PLAN_YN.Clear(); // 재입실 계획여부

            // C.사망현황
            ASM_DEATH_YN = ""; // 사망여무
            DEATH_DT = ""; // 사망일시
            WLST_RCD_YN = ""; // 연명의료중단등결정이행서작성여부
            WLST_RCD_DT = ""; // 작성일자
            WLST_RCD_CD = ""; // 이행내용
            WLST_RCD_ETC_TXT = ""; // 그 밖의 연명의료 상세

            // D. 기타 사항
            //APND_DATA_NO = ""; // 첨부
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM024";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                IPAT_DD = reader["IPAT_DD"].ToString(); // 입원일자(YYYYMMDD)

                // C.사망현황
                ASM_DEATH_YN = reader["ASM_DEATH_YN"].ToString(); // 사망여무
                DEATH_DT = reader["DEATH_DT"].ToString(); // 사망일시
                WLST_RCD_YN = reader["WLST_RCD_YN"].ToString(); // 연명의료중단등결정이행서작성여부
                WLST_RCD_DT = reader["WLST_RCD_DT"].ToString(); // 작성일자
                WLST_RCD_CD = reader["WLST_RCD_CD"].ToString(); // 이행내용
                WLST_RCD_ETC_TXT = reader["WLST_RCD_ETC_TXT"].ToString(); // 그 밖의 연명의료 상세

                return MetroLib.SqlHelper.BREAK;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM024_IPAT";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                // B.중환자실 입퇴실 정보
                SPRM_IPAT_DT.Add(reader["SPRM_IPAT_DT"].ToString()); // 입실일시
                SPRM_IPAT_RS_CD.Add(reader["SPRM_IPAT_RS_CD"].ToString()); // 입실사유
                RE_IPAT_RS_TXT.Add(reader["RE_IPAT_RS_TXT"].ToString()); // 재입실상세
                IPAT_RS_ETC_TXT.Add(reader["IPAT_RS_ETC_TXT"].ToString()); // 입실사유기타
                ASM_SPRM_DSCG_RST_CD.Add(reader["ASM_SPRM_DSCG_RST_CD"].ToString()); // 퇴실현황
                SPRM_DSCG_DT.Add(reader["SPRM_DSCG_DT"].ToString()); // 퇴실일시
                SPRM_RE_IPAT_PLAN_YN.Add(reader["SPRM_RE_IPAT_PLAN_YN"].ToString()); // 재입실 계획여부

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
                sql += "DELETE FROM TI84_ASM024 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM024_IPAT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<Object> para = new List<object>();

            // 읽은 자료를 저장한다..
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM024(FORM, KEYSTR, SEQ, VER, IPAT_DD, ASM_DEATH_YN, DEATH_DT, WLST_RCD_YN, WLST_RCD_DT, WLST_RCD_CD, WLST_RCD_ETC_TXT)";
            sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);
            para.Add(KEYSTR);
            para.Add(SEQ);
            para.Add(ver);
            para.Add(IPAT_DD);
            para.Add(ASM_DEATH_YN);
            para.Add(DEATH_DT);
            para.Add(WLST_RCD_YN);
            para.Add(WLST_RCD_DT);
            para.Add(WLST_RCD_CD);
            para.Add(WLST_RCD_ETC_TXT);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            for (int i = 0; i < SPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM024_IPAT(FORM, KEYSTR, SEQ, SEQNO, SPRM_IPAT_DT, SPRM_IPAT_RS_CD, RE_IPAT_RS_TXT, IPAT_RS_ETC_TXT, ASM_SPRM_DSCG_RST_CD, SPRM_DSCG_DT, SPRM_RE_IPAT_PLAN_YN)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SPRM_IPAT_DT[i]); // 입실일시
                para.Add(SPRM_IPAT_RS_CD[i]); // 입실사유
                para.Add(RE_IPAT_RS_TXT[i]); // 재입실상세
                para.Add(IPAT_RS_ETC_TXT[i]); // 입실사유기타
                para.Add(ASM_SPRM_DSCG_RST_CD[i]); // 퇴실현황
                para.Add(SPRM_DSCG_DT[i]); // 퇴실일시
                para.Add(SPRM_RE_IPAT_PLAN_YN[i]); // 재입실 계획여부

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM024";
            sql += Environment.NewLine + "   SET IPAT_DD=?";
            sql += Environment.NewLine + "     , ASM_DEATH_YN=?";
            sql += Environment.NewLine + "     , DEATH_DT=?";
            sql += Environment.NewLine + "     , WLST_RCD_YN=?";
            sql += Environment.NewLine + "     , WLST_RCD_DT=?";
            sql += Environment.NewLine + "     , WLST_RCD_CD=?";
            sql += Environment.NewLine + "     , WLST_RCD_ETC_TXT=?";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(IPAT_DD);
            para.Add(ASM_DEATH_YN);
            para.Add(DEATH_DT);
            para.Add(WLST_RCD_YN);
            para.Add(WLST_RCD_DT);
            para.Add(WLST_RCD_CD);
            para.Add(WLST_RCD_ETC_TXT);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 일단 지우고
            sql = "";
            sql += Environment.NewLine + "DELETE TI84_ASM024_IPAT";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            // 새로 삽입
            for (int i = 0; i < SPRM_IPAT_DT.Count; i++)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84_ASM024_IPAT(FORM, KEYSTR, SEQ, SEQNO, SPRM_IPAT_DT, SPRM_IPAT_RS_CD, RE_IPAT_RS_TXT, IPAT_RS_ETC_TXT, ASM_SPRM_DSCG_RST_CD, SPRM_DSCG_DT, SPRM_RE_IPAT_PLAN_YN)";
                sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SPRM_IPAT_DT[i]);
                para.Add(SPRM_IPAT_RS_CD[i]);
                para.Add(RE_IPAT_RS_TXT[i]);
                para.Add(IPAT_RS_ETC_TXT[i]);
                para.Add(ASM_SPRM_DSCG_RST_CD[i]);
                para.Add(SPRM_DSCG_DT[i]);
                para.Add(SPRM_RE_IPAT_PLAN_YN[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // TI84_ASM000 저장
            Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM024 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM024_IPAT WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}

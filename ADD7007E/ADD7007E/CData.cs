using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CData
    {
        public string form; // 서식명
        public string ver = "001"; // 서석버전
        public string buss_cd = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public string buss_detail = "IMG"; // 업무상세코드

        public string KEYSTR; // BDODT(EXDATE) + "," + QFYCD + "," + JRBY + "," + PID + "," + UNISQ + "," + SIMCS
        public long SEQ; // KEY임

        public bool SEL { get; set; }
        public long NO { get; set; }
        public string IOFG;
        public string PID { get; set; }
        public string PNM { get; set; }
        public string PNM_TI2A; // HIRA에서 준 환자명
        public string RESID;
        public string BDEDT { get; set; }
        public string QFYCD { get; set; }
        public string GONSGB { get; set; }
        public string DACD { get; set; }
        public string DEMNO { get; set; }
        public string EPRTNO { get; set; }
        public string CNECNO { get; set; } // 접수번호
        public string CNECTDD { get; set; } // 접수년도
        public string BILLSNO { get; set; } // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

        public string BDODT;
        public string JRBY;
        public string UNISQ;
        public string SIMCS;

        public string STATUS;
        public string ERR_CODE;
        public string ERR_DESC;
        public string DOC_NO;

        public string STATUS_ERD001; // 진단검사결과 전송상태
        public string ERR_CODE_ERD001; // 진단검사결과 전송상태
        public string ERR_DESC_ERD001; // 진단검사결과 전송상태
        public string DOC_NO_ERD001; // 진단검사결과 전송상태

        public string STATUS_ERR001; // 영상검사결과 전송상태
        public string ERR_CODE_ERR001; // 영상검사결과 전송상태
        public string ERR_DESC_ERR001; // 영상검사결과 전송상태
        public string DOC_NO_ERR001; // 영상검사결과 전송상태

        public string UPDDT; // 저장일자
        public string DELYN; // 삭제여부

        public string STEDT;
        public string A04_BEDODT;
        public string A04_BEDOHM;
        public string A04_BEDODIV;
        public string A04_BEDEDT { get { return BDEDT; } }
        public string A04_BEDEHM;

        public string FR_DATE; // 자료 시작일
        public string TO_DATE; // 자료 종료일

        public string IOFGNM
        {
            get
            {
                if (IOFG == "1") return "외래";
                if (IOFG == "2") return "입원";
                return IOFG;
            }
        }
        public string RESID_DISP { get { return (RESID.Length > 6) ? RESID.Substring(0, 6) + "-" + RESID.Substring(6, 1) + "******" : RESID; } }
        public string INSUP_TP_CD // 보험자구분
        {
            get
            {
                if (QFYCD.StartsWith("6")) return "8";  // 자보
                //if (GONSGB == "4" || GONSGB == "7") return "7"; // 보훈 ---> 경산세명에서 공상구분"4"인 환자를 보훈(7)으로 보냈더니 문제가 되었음.
                if (GONSGB == "7") return "7"; // 보훈
                if (QFYCD.StartsWith("3")) return "5"; //의료급여
                if (QFYCD.StartsWith("2")) return "4"; // 건강보험
                return "";
            }
        }

        public string STATUS_NM
        {
            get
            {
                string ret = "";
                if (form == "ASM008")
                {
                    // 혈액투석은 진단검사결과와 영상검사결과를 고려해야한다.
                    if (DELYN == "Y") ret = "제외";
                    else if (STATUS == "Y" && STATUS_ERD001 == "Y" && STATUS_ERR001 == "Y") ret = "성공";
                    else if (STATUS == "E" || STATUS_ERD001 == "E" || STATUS_ERR001 == "E") ret = "오류";
                    else if (STATUS == "N" || STATUS_ERD001 == "N" || STATUS_ERR001 == "N") ret = "진행중";
                    else if (UPDDT != "") ret = "저장";

                }
                else
                {
                    if (DELYN == "Y") ret = "제외";
                    else if (STATUS == "Y") ret = "성공";
                    else if (STATUS == "E") ret = "오류";
                    else if (STATUS == "N") ret = "진행중";
                    else if (UPDDT != "") ret = "저장";
                }

                return ret;
            }
        }

        public string ERR_MSG
        {
            get
            {
                string ret = "";
                ret = ERR_CODE + Environment.NewLine + ERR_DESC;
                return ret;
            }
        }

        public void Clear(string form, string ver, string buss_cd, string buss_detail)
        {
            this.form = form;
            this.ver = ver;
            this.buss_cd = buss_cd;
            this.buss_detail = buss_detail;

            KEYSTR = ""; // BDODT(EXDATE) + "," + QFYCD + "," + JRBY + "," + PID + "," + UNISQ + "," + SIMCS
            SEQ = 0; // KEY임

            SEL = false;
            NO = 0;
            IOFG = "";
            PID = "";
            PNM = "";
            PNM_TI2A = "";
            RESID = "";
            BDEDT = "";
            QFYCD = "";
            GONSGB = "";
            DACD = "";
            DEMNO = "";
            EPRTNO = "";
            CNECNO = ""; // 접수번호
            CNECTDD = ""; // 접수년도
            BILLSNO = ""; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

            BDODT = "";
            JRBY = "";
            UNISQ = "";
            SIMCS = "";

            STATUS = "";
            ERR_CODE = "";
            ERR_DESC = "";
            DOC_NO = "";

            STATUS_ERD001 = ""; ; // 진단검사결과 전송상태
            ERR_CODE_ERD001 = ""; ; // 진단검사결과 전송상태
            ERR_DESC_ERD001 = ""; ; // 진단검사결과 전송상태
            DOC_NO_ERD001 = ""; ; // 진단검사결과 전송상태

            STATUS_ERR001 = ""; ; // 영상검사결과 전송상태
            ERR_CODE_ERR001 = ""; ; // 영상검사결과 전송상태
            ERR_DESC_ERR001 = ""; ; // 영상검사결과 전송상태
            DOC_NO_ERR001 = ""; ; // 영상검사결과 전송상태

            UPDDT = "";
            DELYN = ""; // 삭제여부

            STEDT = "";
            A04_BEDEHM = "";
            A04_BEDODT = "";
            A04_BEDOHM = "";
            A04_BEDODIV = "";

            FR_DATE = ""; // 자료 시작일
            TO_DATE = ""; // 자료 종료일

        }

        public void SetValuesFromDataRow(DataRow row)
        {
            SEL = true;
            KEYSTR = row["KEYSTR"].ToString();
            SEQ = MetroLib.StrHelper.ToLong(row["SEQ"].ToString());
            NO = SEQ;

            IOFG = row["IOFG"].ToString();
            PID = row["PID"].ToString();
            PNM = row["PNM"].ToString();
            RESID = row["RESID"].ToString();
            BDEDT = row["BDEDT"].ToString();
            QFYCD = row["QFYCD"].ToString();
            GONSGB = row["GONSGB"].ToString();
            DACD = row["DACD"].ToString();
            DEMNO = row["DEMNO"].ToString();
            EPRTNO = row["EPRTNO"].ToString();
            CNECNO = row["CNECNO"].ToString(); // 접수번호
            CNECTDD = row["CNECTDD"].ToString(); // 접수년도
            BILLSNO = row["BILLSNO"].ToString(); // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

            BDODT = row["BDODT"].ToString();
            JRBY = row["JRBY"].ToString();
            UNISQ = row["UNISQ"].ToString();
            SIMCS = row["SIMCS"].ToString();

            STATUS = row["STATUS"].ToString();
            ERR_CODE = row["ERR_CODE"].ToString();
            ERR_DESC = row["ERR_DESC"].ToString();
            DOC_NO = row["DOC_NO"].ToString();

            STATUS_ERD001 = row["STATUS_ERD001"].ToString();    // 진단검사결과 전송상태
            ERR_CODE_ERD001 = row["ERR_CODE_ERD001"].ToString();  // 진단검사결과 전송상태
            ERR_DESC_ERD001 = row["ERR_DESC_ERD001"].ToString();  // 진단검사결과 전송상태
            DOC_NO_ERD001 = row["DOC_NO_ERD001"].ToString();    // 진단검사결과 전송상태

            STATUS_ERR001 = row["STATUS_ERR001"].ToString();    // 영상검사결과 전송상태
            ERR_CODE_ERR001 = row["ERR_CODE_ERR001"].ToString();  // 영상검사결과 전송상태
            ERR_DESC_ERR001 = row["ERR_DESC_ERR001"].ToString();  // 영상검사결과 전송상태
            DOC_NO_ERR001 = row["DOC_NO_ERR001"].ToString();    // 영상검사결과 전송상태

            UPDDT = row["UPDDT"].ToString();
            DELYN = row["DELYN"].ToString();

            STEDT = row["STEDT"].ToString();
            A04_BEDEHM = row["A04_BEDEHM"].ToString();
            A04_BEDODT = row["A04_BEDODT"].ToString();
            A04_BEDOHM = row["A04_BEDOHM"].ToString();
            A04_BEDODIV = row["A04_BEDODIV"].ToString();

            FR_DATE = row["FR_DATE"].ToString(); // 자료 시작일
            TO_DATE = row["TO_DATE"].ToString(); // 자료 종료일
        }

        public int Read_ASM000(OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
        {
            int count = 0;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = '" + this.form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                count = 1;
                string upddt = reader["UPDDT"].ToString();
                string status = reader["STATUS"].ToString();

                // re_query가 true이면 TI84_ASM000을 사용하지 않고 앞에서 읽은 TI2A(TI1A) 자료를 사용한다.
                if (re_query == true || (upddt == "" && status == ""))
                {
                    // 강제로 다시 조회하거나, 아직 저장하지 않은 자료는 다시 읽는다.
                }
                else
                {
                    IOFG = reader["IOFG"].ToString();
                    PID = reader["PID"].ToString();
                    PNM = reader["PNM"].ToString();
                    RESID = reader["RESID"].ToString();
                    BDEDT = reader["BDEDT"].ToString();
                    QFYCD = reader["QFYCD"].ToString();
                    GONSGB = reader["GONSGB"].ToString();
                    DACD = reader["DACD"].ToString();
                    DEMNO = reader["DEMNO"].ToString();
                    EPRTNO = reader["EPRTNO"].ToString();
                    CNECNO = reader["CNECNO"].ToString(); // 접수번호
                    CNECTDD = reader["CNECTDD"].ToString(); // 접수년도
                    BILLSNO = reader["BILLSNO"].ToString(); // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)

                    STATUS = reader["STATUS"].ToString();
                    ERR_CODE = reader["ERR_CODE"].ToString();
                    ERR_DESC = reader["ERR_DESC"].ToString();
                    DOC_NO = reader["DOC_NO"].ToString();

                    UPDDT = reader["UPDDT"].ToString();
                    DELYN = reader["DELYN"].ToString();

                    STEDT = reader["STEDT"].ToString();
                    A04_BEDEHM = reader["A04_BEDEHM"].ToString();
                    A04_BEDODT = reader["A04_BEDODT"].ToString();
                    A04_BEDOHM = reader["A04_BEDOHM"].ToString();
                    A04_BEDODIV = reader["A04_BEDODIV"].ToString();

                    FR_DATE = reader["FR_DATE"].ToString(); // 자료 시작일
                    TO_DATE = reader["TO_DATE"].ToString(); // 자료 종료일
                }
                //if (re_query)
                //{
                //    STATUS = "";
                //    ERR_CODE = "";
                //    ERR_DESC = "";
                //    DOC_NO = "";
                //
                //    UPDDT = "";
                //    DELYN = "";
                //}

                return MetroLib.SqlHelper.BREAK;
            });

            return count;
        }

        public void Into_ASM000(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg)
        {
            string sql = "";

            List<Object> para = new List<object>();

            if (del_fg == true)
            {
                sql = "";
                sql += Environment.NewLine + "DELETE";
                sql += Environment.NewLine + "  FROM TI84_ASM000";
                sql += Environment.NewLine + " WHERE FORM = ?";
                sql += Environment.NewLine + "   AND KEYSTR = ?";
                sql += Environment.NewLine + "   AND SEQ = ?";

                para.Clear();
                para.Add(this.form);
                para.Add(KEYSTR);
                para.Add(SEQ);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }


            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM000(FORM, KEYSTR, SEQ, IOFG, PID, PNM, RESID, BDEDT, QFYCD, GONSGB, DACD, DEMNO, EPRTNO, CNECNO, CNECTDD, BILLSNO, BDODT, JRBY, UNISQ, SIMCS, CREDT, CRETM, CREID, STEDT, A04_BEDODT,A04_BEDOHM, A04_BEDODIV, A04_BEDEHM, FR_DATE, TO_DATE)";
            sql += Environment.NewLine + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);
            para.Add(IOFG);
            para.Add(PID);
            para.Add(PNM);
            para.Add(RESID);
            para.Add(BDEDT);
            para.Add(QFYCD);
            para.Add(GONSGB);
            para.Add(DACD);
            para.Add(DEMNO);
            para.Add(EPRTNO);
            para.Add(CNECNO);
            para.Add(CNECTDD);
            para.Add(BILLSNO);

            para.Add(BDODT);
            para.Add(JRBY);
            para.Add(UNISQ);
            para.Add(SIMCS);

            para.Add(p_sysdt);
            para.Add(p_systm);
            para.Add(p_user);

            para.Add(STEDT);
            para.Add(A04_BEDODT);
            para.Add(A04_BEDOHM);
            para.Add(A04_BEDODIV);
            para.Add(A04_BEDEHM);

            para.Add(FR_DATE);
            para.Add(TO_DATE);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        public void Upd_ASM000(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM000";
            sql += Environment.NewLine + "   SET UPDDT = ?";
            sql += Environment.NewLine + "     , UPDTM = ?";
            sql += Environment.NewLine + "     , UPDID = ?";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(p_sysdt);
            para.Add(p_systm);
            para.Add(p_user);
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 자료을 다시 읽는다.
            sql = "";
            sql += Environment.NewLine + "SELECT UPDDT";
            sql += Environment.NewLine + "  FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            para.Clear();
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                UPDDT = reader["UPDDT"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });
        }

        public void Upd_STATUS(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn)
        {

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM000";
            sql += Environment.NewLine + "   SET STATUS = ?";
            sql += Environment.NewLine + "     , ERR_CODE = ?";
            sql += Environment.NewLine + "     , ERR_DESC = ?";
            sql += Environment.NewLine + "     , DOC_NO = ?";
            sql += Environment.NewLine + "     , SENDDT = ?";
            sql += Environment.NewLine + "     , SENDTM = ?";
            sql += Environment.NewLine + "     , SENDID = ?";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(STATUS);
            para.Add(ERR_CODE);
            para.Add(ERR_DESC);
            para.Add(DOC_NO);
            para.Add(p_sysdt);
            para.Add(p_systm);
            para.Add(p_user);
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, null);
        }

        public void Upd_STATUS_ERD001(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn)
        {

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM000";
            sql += Environment.NewLine + "   SET STATUS_ERD001 = ?";
            sql += Environment.NewLine + "     , ERR_CODE_ERD001 = ?";
            sql += Environment.NewLine + "     , ERR_DESC_ERD001 = ?";
            sql += Environment.NewLine + "     , DOC_NO_ERD001 = ?";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(STATUS_ERD001);
            para.Add(ERR_CODE_ERD001);
            para.Add(ERR_DESC_ERD001);
            para.Add(DOC_NO);
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, null);
        }

        public void Upd_STATUS_ERR001(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn)
        {

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM000";
            sql += Environment.NewLine + "   SET STATUS_ERR001 = ?";
            sql += Environment.NewLine + "     , ERR_CODE_ERR001 = ?";
            sql += Environment.NewLine + "     , ERR_DESC_ERR001 = ?";
            sql += Environment.NewLine + "     , DOC_NO_ERR001 = ?";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(STATUS_ERR001);
            para.Add(ERR_CODE_ERR001);
            para.Add(ERR_DESC_ERR001);
            para.Add(DOC_NO);
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, null);
        }


        public void Except_ASM000(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM000";
            sql += Environment.NewLine + "   SET DELYN = 'Y'";
            sql += Environment.NewLine + "     , DELDT = ?";
            sql += Environment.NewLine + "     , DELTM = ?";
            sql += Environment.NewLine + "     , DELID = ?";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(p_sysdt);
            para.Add(p_systm);
            para.Add(p_user);
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, null);

            // 자료을 다시 읽는다.
            sql = "";
            sql += Environment.NewLine + "SELECT DELYN";
            sql += Environment.NewLine + "  FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "   AND KEYSTR = ?";
            sql += Environment.NewLine + "   AND SEQ = ?";

            para.Clear();
            para.Add(this.form);
            para.Add(KEYSTR);
            para.Add(SEQ);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, null, delegate(OleDbDataReader reader)
            {
                DELYN = reader["DELYN"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });
        }

        public void DelAll_ASM000(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "DELETE";
            sql += Environment.NewLine + "  FROM TI84_ASM000";
            sql += Environment.NewLine + " WHERE FORM = ?";
            sql += Environment.NewLine + "  AND KEYSTR = ?";

            List<Object> para = new List<object>();
            para.Clear();
            para.Add(this.form);
            para.Add(KEYSTR);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }
    }
}

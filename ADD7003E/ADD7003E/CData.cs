using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class CData
    {
        public string RN { get; set; } // 행번호
        public string REQ_DATA_NO { get; set; } // 요청자료번호
        public string REQ_STA_DD { get; set; } // 요청시작일자
        public string REQ_END_DD { get; set; } // 요청종료일자
        public string DRG_REQ_DATA_TP_CD { get; set; } // 포괄요청자료구분코드
        public string DRG_REQ_DATA_TP_CD_NM { get; set; } // 포괄요청자료구분코드명
        public string PSYS_TP_CD_NM { get; set; } // 지불제도구분코드명
        public string INSUP_TP_CD { get; set; } // 보험자구분코드
        public string INSUP_TP_CD_NM { get; set; } // 보험자구분코드명
        public string RCV_YR { get; set; } // 접수년도
        public string RCV_DD { get; set; } // 접수일자
        public string DMD_NO { get; set; } // 청구번호
        public string RCV_NO { get; set; } // 접수번호
        public string BILL_SNO { get; set; } // 청일련
        public string SP_SNO { get; set; } // 명일련
        public string PAT_NM { get; set; } // 환자명
        public string HOSP_RNO { get; set; } // 환자등록번호 
        public string DGSBJT_CD_NM { get; set; } // 진료과목명
        public string DIAG_YM { get; set; } // 진료년월
        public string RECU_FR_DD { get; set; } // 요양개시일자
        public string PRSCP_GRANT_NO { get; set; } // 원외처방교부번호
        public string SMIT_YN { get; set; } // 제출여부
        public string TOT_CNT { get; set; } // 조회건수

        public void SetValues(HIRA.EformEntry.Model.Row row)
        {
            RN = row["RN"].Value; // 행번호
            REQ_DATA_NO = row["REQ_DATA_NO"].Value; // 요청자료번호
            REQ_STA_DD = row["REQ_STA_DD"].Value; // 요청시작일자
            REQ_END_DD = row["REQ_END_DD"].Value; // 요청종료일자
            DRG_REQ_DATA_TP_CD = row["DRG_REQ_DATA_TP_CD"].Value; // 포괄요청자료구분코드
            DRG_REQ_DATA_TP_CD_NM = row["DRG_REQ_DATA_TP_CD_NM"].Value; // 포괄요청자료구분코드명
            PSYS_TP_CD_NM = row["PSYS_TP_CD_NM"].Value; // 지불제도구분코드명
            INSUP_TP_CD = row["INSUP_TP_CD"].Value; // 보험자구분코드
            INSUP_TP_CD_NM = row["INSUP_TP_CD_NM"].Value; // 보험자구분코드명
            RCV_YR = row["RCV_YR"].Value; // 접수년도
            RCV_DD = row["RCV_DD"].Value; // 접수일자
            DMD_NO = row["DMD_NO"].Value; // 청구번호
            RCV_NO = row["RCV_NO"].Value; // 접수번호
            BILL_SNO = row["BILL_SNO"].Value; // 청일련
            SP_SNO = row["SP_SNO"].Value; // 명일련
            PAT_NM = row["PAT_NM"].Value; // 환자명
            HOSP_RNO = row["HOSP_RNO"].Value; // 환자등록번호 
            DGSBJT_CD_NM = row["DGSBJT_CD_NM"].Value; // 진료과목명
            DIAG_YM = row["DIAG_YM"].Value; // 진료년월
            RECU_FR_DD = row["RECU_FR_DD"].Value; // 요양개시일자
            PRSCP_GRANT_NO = row["PRSCP_GRANT_NO"].Value; // 원외처방교부번호
            SMIT_YN = row["SMIT_YN"].Value; // 제출여부
            TOT_CNT = row["TOT_CNT"].Value; // 조회건수
        }

        public void SetValues(OleDbDataReader reader)
        {
            string insup_tp_cd = "";
            string insup_tp_cd_nm = "";
            if (reader["QFYCD"].ToString() == "29")
            {
                insup_tp_cd = "7";
                insup_tp_cd_nm = "보훈";
            }
            else if (reader["QFYCD"].ToString().StartsWith("3"))
            {
                insup_tp_cd = "5";
                insup_tp_cd_nm = "보호";
            }
            else
            {
                insup_tp_cd = "4";
                insup_tp_cd_nm = "보험";
            }

            RN = reader["EPRTNO"].ToString(); // 행번호
            REQ_DATA_NO = "0000" + reader["DEMNO"].ToString() + "0" + reader["EPRTNO"].ToString(); // 요청자료번호
            REQ_STA_DD = ""; // 요청시작일자
            REQ_END_DD = ""; // 요청종료일자
            DRG_REQ_DATA_TP_CD = ""; // 포괄요청자료구분코드
            DRG_REQ_DATA_TP_CD_NM = ""; // 포괄요청자료구분코드명
            PSYS_TP_CD_NM = ""; // 지불제도구분코드명
            INSUP_TP_CD = insup_tp_cd; // 보험자구분코드(임의의값)
            INSUP_TP_CD_NM = insup_tp_cd_nm; // 보험자구분코드명(임의의값)
            RCV_YR = reader["DEMNO"].ToString().Substring(0, 4); // 접수년도
            RCV_DD = reader["DEMNO"].ToString().Substring(0, 4) + "28"; // 접수일자
            DMD_NO = reader["DEMNO"].ToString(); // 청구번호
            RCV_NO = "1234567"; // 접수번호(임의의값)
            BILL_SNO = "1"; // 청일련(임의의값)
            SP_SNO = reader["EPRTNO"].ToString(); // 명일련
            PAT_NM = reader["PNM"].ToString(); // 환자명
            HOSP_RNO = reader["PID"].ToString(); // 환자등록번호 
            DGSBJT_CD_NM = ""; // 진료과목명
            DIAG_YM = ""; // 진료년월
            RECU_FR_DD = reader["STEDT"].ToString(); // 요양개시일자
            PRSCP_GRANT_NO = ""; // 원외처방교부번호
            SMIT_YN = ""; // 제출여부
            TOT_CNT = ""; // 조회건수
        }

        public void SaveData(OleDbConnection p_conn, string p_sysdt, string p_systm, string p_user)
        {
            // MMS를 남기지 않기 위한 작업
            string user = p_user;
            if ("MMS".Equals(user, StringComparison.CurrentCultureIgnoreCase)) user = "";

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT COUNT(*) CNT";
            sql += Environment.NewLine + "  FROM TI84";
            sql += Environment.NewLine + " WHERE REQ_DATA_NO='" + REQ_DATA_NO + "'";
            int cnt = 0;
            CSQLHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int.TryParse(reader["CNT"].ToString(), out cnt);
                return false;
            });

            if (cnt < 1)
            {
                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI84(REQ_DATA_NO,REQ_STA_DD,REQ_END_DD,DRG_REQ_DATA_TP_CD,DRG_REQ_DATA_TP_CD_NM,PSYS_TP_CD_NM,INSUP_TP_CD,INSUP_TP_CD_NM,RCV_YR,RCV_DD,DMD_NO,RCV_NO,BILL_SNO,SP_SNO,PAT_NM,HOSP_RNO,DGSBJT_CD_NM,DIAG_YM,RECU_FR_DD,PRSCP_GRANT_NO,SMIT_YN,SYSDT,SYSTM,EMPID)";
                sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, p_conn);

                cmd.Parameters.Add(new OleDbParameter("@1", REQ_DATA_NO));
                cmd.Parameters.Add(new OleDbParameter("@2", REQ_STA_DD));
                cmd.Parameters.Add(new OleDbParameter("@3", REQ_END_DD));
                cmd.Parameters.Add(new OleDbParameter("@4", DRG_REQ_DATA_TP_CD));
                cmd.Parameters.Add(new OleDbParameter("@5", DRG_REQ_DATA_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@6", PSYS_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@7", INSUP_TP_CD));
                cmd.Parameters.Add(new OleDbParameter("@8", INSUP_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@9", RCV_YR));
                cmd.Parameters.Add(new OleDbParameter("@10", RCV_DD));
                cmd.Parameters.Add(new OleDbParameter("@11", DMD_NO));
                cmd.Parameters.Add(new OleDbParameter("@12", RCV_NO));
                cmd.Parameters.Add(new OleDbParameter("@13", BILL_SNO));
                cmd.Parameters.Add(new OleDbParameter("@14", SP_SNO));
                cmd.Parameters.Add(new OleDbParameter("@15", PAT_NM));
                cmd.Parameters.Add(new OleDbParameter("@16", HOSP_RNO));
                cmd.Parameters.Add(new OleDbParameter("@17", DGSBJT_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@18", DIAG_YM));
                cmd.Parameters.Add(new OleDbParameter("@19", RECU_FR_DD));
                cmd.Parameters.Add(new OleDbParameter("@20", PRSCP_GRANT_NO));
                cmd.Parameters.Add(new OleDbParameter("@21", SMIT_YN));
                cmd.Parameters.Add(new OleDbParameter("@22", p_sysdt));
                cmd.Parameters.Add(new OleDbParameter("@23", p_systm));
                cmd.Parameters.Add(new OleDbParameter("@24", user));
                
                cmd.ExecuteNonQuery();
            }
            else
            {
                sql = "";
                sql += Environment.NewLine + "UPDATE TI84";
                sql += Environment.NewLine + "   SET REQ_STA_DD=?";
                sql += Environment.NewLine + "     , REQ_END_DD=?";
                sql += Environment.NewLine + "     , DRG_REQ_DATA_TP_CD=?";
                sql += Environment.NewLine + "     , DRG_REQ_DATA_TP_CD_NM=?";
                sql += Environment.NewLine + "     , PSYS_TP_CD_NM =?";
                sql += Environment.NewLine + "     , INSUP_TP_CD=?";
                sql += Environment.NewLine + "     , INSUP_TP_CD_NM=?";
                sql += Environment.NewLine + "     , RCV_YR=?";
                sql += Environment.NewLine + "     , RCV_DD=?";
                sql += Environment.NewLine + "     , DMD_NO=?";
                sql += Environment.NewLine + "     , RCV_NO=?";
                sql += Environment.NewLine + "     , BILL_SNO=?";
                sql += Environment.NewLine + "     , SP_SNO=?";
                sql += Environment.NewLine + "     , PAT_NM=?";
                sql += Environment.NewLine + "     , HOSP_RNO=?";
                sql += Environment.NewLine + "     , DGSBJT_CD_NM=?";
                sql += Environment.NewLine + "     , DIAG_YM=?";
                sql += Environment.NewLine + "     , RECU_FR_DD=?";
                sql += Environment.NewLine + "     , PRSCP_GRANT_NO=?";
                sql += Environment.NewLine + "     , SMIT_YN=?";
                sql += Environment.NewLine + "     , SYSDT=?";
                sql += Environment.NewLine + "     , SYSTM=?";
                if (user != "")
                {
                    sql += Environment.NewLine + "     , EMPID=?";
                }
                sql += Environment.NewLine + " WHERE REQ_DATA_NO=?";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, p_conn);

                cmd.Parameters.Add(new OleDbParameter("@1", REQ_STA_DD));
                cmd.Parameters.Add(new OleDbParameter("@2", REQ_END_DD));
                cmd.Parameters.Add(new OleDbParameter("@3", DRG_REQ_DATA_TP_CD));
                cmd.Parameters.Add(new OleDbParameter("@4", DRG_REQ_DATA_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@5", PSYS_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@6", INSUP_TP_CD));
                cmd.Parameters.Add(new OleDbParameter("@7", INSUP_TP_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@8", RCV_YR));
                cmd.Parameters.Add(new OleDbParameter("@9", RCV_DD));
                cmd.Parameters.Add(new OleDbParameter("@10", DMD_NO));
                cmd.Parameters.Add(new OleDbParameter("@11", RCV_NO));
                cmd.Parameters.Add(new OleDbParameter("@12", BILL_SNO));
                cmd.Parameters.Add(new OleDbParameter("@13", SP_SNO));
                cmd.Parameters.Add(new OleDbParameter("@14", PAT_NM));
                cmd.Parameters.Add(new OleDbParameter("@15", HOSP_RNO));
                cmd.Parameters.Add(new OleDbParameter("@16", DGSBJT_CD_NM));
                cmd.Parameters.Add(new OleDbParameter("@17", DIAG_YM));
                cmd.Parameters.Add(new OleDbParameter("@18", RECU_FR_DD));
                cmd.Parameters.Add(new OleDbParameter("@10", PRSCP_GRANT_NO));
                cmd.Parameters.Add(new OleDbParameter("@20", SMIT_YN));
                cmd.Parameters.Add(new OleDbParameter("@21", p_sysdt));
                cmd.Parameters.Add(new OleDbParameter("@22", p_systm));
                if (user != "")
                {
                    cmd.Parameters.Add(new OleDbParameter("@23", user));
                }
                cmd.Parameters.Add(new OleDbParameter("@24", REQ_DATA_NO));

                cmd.ExecuteNonQuery();

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CEMR290
    {
        public int READ_CNT;

        public string CC; // 주호소
        public string PI; // 현병력
        public string CC_DATE; // 주호소의 발생시기 <--- 자료없음.
        public string PHX; // 과거력
        public string FHX; // 가족력
        public string ROS; // 계통문진
        public string PE; // 신체검진
        public string IMP; // 추정진단
        public string TXPLAN; // 치료계획
        public string MODIFY_HX; // 수정이력

        public string EMPID; // 등록자ID
        public string EMPNM; // 등록자
        public string SYSDT; // 등록일자
        public string SYSTM; // 등록시간

        public void Clear()
        {
            READ_CNT = 0;

            CC = ""; // 주호소
            PI = ""; // 현병력
            CC_DATE = ""; // 주호소의 발생시기 <--- 자료없음.
            PHX = ""; // 과거력
            FHX = ""; // 가족력
            ROS = ""; // 계통문진
            PE = ""; // 신체검진
            IMP = ""; // 추정진단
            TXPLAN = ""; // 치료계획
            MODIFY_HX = ""; // 수정이력

            EMPID = ""; // 등록자ID
            EMPNM = ""; // 등록자
            SYSDT = ""; // 등록일자
            SYSTM = ""; // 등록시간
        }

        public void SetData(string p_pid, string p_bdedt)
        {
            try
            {

                string sql = "";
                sql += System.Environment.NewLine + "SELECT	PID,BEDEDT,WDATE,SEQ,MJ_HOSO,ONSET,PI,PHX,FHX,ROS,PE,IMP,CUREPLAN,SYSDT,SYSTM,EMPID";
                sql += System.Environment.NewLine + "  FROM EMR290";
                sql += System.Environment.NewLine + " WHERE PID =	'" + p_pid + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_bdedt + "'";
                sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,WDATE DESC,SEQ DESC";
                string strConn = DBHelper.GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        READ_CNT = 1;
                        CC = reader["MJ_HOSO"].ToString();
                        CC_DATE = reader["ONSET"].ToString();
                        PI = reader["PI"].ToString();
                        PHX = reader["PHX"].ToString();
                        FHX = reader["FHX"].ToString();
                        ROS = reader["ROS"].ToString();
                        PE = reader["PE"].ToString();
                        IMP = reader["IMP"].ToString();
                        TXPLAN = reader["CUREPLAN"].ToString();
                        SYSDT = reader["SYSDT"].ToString();
                        SYSTM = reader["SYSTM"].ToString();
                        EMPID = reader["EMPID"].ToString();
                        EMPNM = GetEmpnm(conn, EMPID);
                        MODIFY_HX = GetModifyHx(conn, p_pid, p_bdedt, reader["WDATE"].ToString(), reader["SEQ"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            return CEmployee.GetEmpnm(p_conn, p_empid);
            /*
            if ("".Equals(p_empid)) return "";

            String strRet = "";
            String sql = "";
            if (p_empid.StartsWith("AA"))
            {
                sql += System.Environment.NewLine + "SELECT A07.DRNM EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA07 A07";
                sql += System.Environment.NewLine + " WHERE A07.DRID='" + p_empid + "' ";
            }
            else
            {
                sql += System.Environment.NewLine + "SELECT A13.EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA13 A13";
                sql += System.Environment.NewLine + " WHERE A13.EMPID='" + p_empid + "' ";
            }

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["EMPNM"].ToString();
            }
            reader.Close();
            return strRet;
            */
        }

        private String GetModifyHx(OleDbConnection p_conn, String p_pid, String p_bededt, String p_wdate, String p_seq)
        {
            String strRet = "";
            String sql = "";
            sql += System.Environment.NewLine + "SELECT	SYSDT,SYSTM,STAT";
            sql += System.Environment.NewLine + "  FROM EMR290_HISTORY";
            sql += System.Environment.NewLine + " WHERE PID =	'" + p_pid + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_bededt + "'";
            sql += System.Environment.NewLine + "   AND WDATE = '" + p_wdate + "'";
            sql += System.Environment.NewLine + "   AND SEQ = " + p_seq + "";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["SYSDT"].ToString() + " " + reader["SYSTM"].ToString().Substring(0, 4) + " " + reader["STAT"].ToString() + System.Environment.NewLine;
            }
            reader.Close();

            return strRet;
        }

    }
}

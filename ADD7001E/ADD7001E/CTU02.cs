using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CTU02
    {
        public int READ_COUNT;

        public string OPDT; // 수술일
        public string DPTCD; // 진료과
        public string DPTNM; // 진료과명
        public string OCD; // 수술코드
        public string ONM; // 수술명
        public string OPDR; // 수술의
        public string OPDRNM; // 수술의명
        public string SYSDT;
        public string SYSTM;
        public string USRID;
        public string USRNM;

        public void Clear()
        {
            READ_COUNT = 0;

            OPDT = ""; // 수술일
            DPTCD = ""; // 진료과
            DPTNM = ""; // 진료과명
            OCD = ""; // 수술코드
            ONM = ""; // 수술명
            OPDR = ""; // 수술의
            OPDRNM = ""; // 수술의명
            SYSDT = "";
            SYSTM = "";
            USRID = "";
            USRNM = "";
        }

        public void SetData(string p_pid, string p_bdedt)
        {
            try
            {
                Clear();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT	U02.OPDT,U02.DPTCD,A09.DPTNM,U02.OCD,A18.ONM,U02.OPDR,A07.DRNM OPDRNM,U02.SYSDT,U02.SYSTM,U02.USRID";
                sql += System.Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA07 A07 ON A07.DRID=U02.OPDR";
                sql += System.Environment.NewLine + "                INNER JOIN TA09 A09 ON A09.DPTCD=U02.DPTCD";
                sql += System.Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD=U02.OCD";
                sql += System.Environment.NewLine + "                                    AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql += System.Environment.NewLine + " WHERE U02.PID =	'" + p_pid + "'";
                sql += System.Environment.NewLine + "   AND U02.BEDEDT = '" + p_bdedt + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
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
                        READ_COUNT = 1;
                        OPDT = reader["OPDT"].ToString(); // 수술일
                        DPTCD = reader["DPTCD"].ToString(); // 진료과
                        DPTNM = reader["DPTNM"].ToString(); // 진료과명
                        OCD = reader["OCD"].ToString(); // 수술코드
                        ONM = reader["ONM"].ToString(); // 수술명
                        OPDR = reader["OPDR"].ToString(); // 수술의
                        OPDRNM = reader["OPDRNM"].ToString(); // 수술의명
                        SYSDT = reader["SYSDT"].ToString();
                        SYSTM = reader["SYSTM"].ToString();
                        USRID = reader["USRID"].ToString();
                        USRNM = CEmployee.GetEmpnm(conn, USRID);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

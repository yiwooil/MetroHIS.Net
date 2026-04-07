using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CTU03
    {
        public int READ_COUNT;

        public string ANETP;
        public string ANETPNM;
        public string ANEDR;
        public string ANEDRNM;

        public void Clear()
        {
            READ_COUNT = 0;
            ANETP = "";
            ANETPNM = "";
            ANEDR = "";
            ANEDRNM = "";
        }

        public void SetData(string p_pid, string p_bdedt)
        {
            try
            {
                Clear();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT	U03.ANETP,U03.ANEDR,A31.CDNM ANETPNM,A07.DRNM ANEDRNM";
                sql += System.Environment.NewLine + "  FROM TU03 U03 INNER JOIN TA31 A31 ON A31.MST1CD='58' AND A31.MST2CD=U03.ANETP";
                sql += System.Environment.NewLine + "                INNER JOIN TA07 A07 ON A07.DRID=U03.ANEDR";
                sql += System.Environment.NewLine + " WHERE U03.PID =	'" + p_pid + "'";
                sql += System.Environment.NewLine + "   AND U03.BEDEDT = '" + p_bdedt + "'";
                sql += System.Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
                sql += System.Environment.NewLine + "   AND ISNULL(U03.CANCEL,'')=''";
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
                        ANETP = reader["ANETP"].ToString(); // 마취방법
                        ANETPNM = reader["ANETPNM"].ToString(); // 마취방법명
                        ANEDR = reader["ANEDR"].ToString(); // 마취의사
                        ANEDRNM = reader["ANEDRNM"].ToString(); // 마취의사명
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

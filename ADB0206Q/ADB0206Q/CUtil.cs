using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADB0206Q
{
    class CUtil
    {
        static public String GetSysDate(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT CONVERT(VARCHAR,GETDATE(),112) AS SYSDATE ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSDATE"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }
        static public String GetSysTime(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','') AS SYSTIME ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSTIME"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }

        static public String GetAge(String Fday, String Tday, String option)
        {
            try
            {
                if (ValDt(Fday) == false || ValDt(Tday) == false) return "";
                if (Fday.CompareTo(Tday) > 0) return "";

                long OYY;
                long OMM;
                long ODD;

                String Dcnt;

                long FYY;
                long FMM;
                long FDD;
                long TYY;
                long TMM;
                long TDD;

                Dcnt = "31,28,31,30,31,30,31,31,30,31,30,31";

                FYY = BtoL(Fday.Substring(0, 4));
                FMM = BtoL(Fday.Substring(4, 2));
                FDD = BtoL(Fday.Substring(6, 2));

                TYY = BtoL(Tday.Substring(0, 4));
                TMM = BtoL(Tday.Substring(4, 2));
                TDD = BtoL(Tday.Substring(6, 2));

                if (((FYY % 4) == 0) && (((FYY % 100) != 0) || ((FYY % 400) == 0)))
                {
                    Dcnt = "31,29,31,30,31,30,31,31,30,31,30,31";
                }
                String[] arrDcnt = Dcnt.Split(',');

                OYY = TYY - FYY - 1;
                OMM = (12 - FMM) + (TMM - 1);
                ODD = BtoL(arrDcnt[FMM - 1]) - FDD + TDD;
                if (ODD >= BtoL(arrDcnt[FMM - 1]))
                {
                    ODD = ODD - BtoL(arrDcnt[FMM - 1]);
                    OMM = OMM + 1;
                }
                if (ODD >= BtoL(arrDcnt[TMM - 1]))
                {
                    ODD = ODD - BtoL(arrDcnt[TMM - 1]);
                    OMM = OMM + 1;
                }
                while (OMM >= 12)
                {
                    OMM = OMM - 12;
                    OYY = OYY + 1;
                }

                String rtn;
                if (option == "year")
                {
                    rtn = OYY.ToString();
                }
                else
                {
                    rtn = ((10000 + OYY).ToString()).Substring(2, 4);
                    rtn += ((100 + OMM).ToString()).Substring(2, 2);
                    rtn += ((100 + ODD).ToString()).Substring(2, 2);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        static public Boolean ValDt(String date)
        {
            if (date.Length != 8) return false;
            try
            {
                DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public long BtoL(string sValue)
        {
            long ret = 0;
            long.TryParse(sValue, out ret);
            return ret;
        }
    }
}

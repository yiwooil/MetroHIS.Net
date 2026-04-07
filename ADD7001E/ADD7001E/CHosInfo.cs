using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CHosInfo
    {
        private String m_HosId; // 요양기관기호
        //private String m_CeoNm; // 대표자
        //private String m_Juso;  // 사업장 소재지
        //private String m_Telno; // 전화번호
        //private String m_BussNm; // 상호
        //private String m_BussNo; // 사업자 등록번호
        //private String m_HosJong; // 종별

        public String GetHosId()
        {
            return m_HosId;
        }

        //public String GetCeoNm()
        //{
        //    return m_CeoNm;
        //}

        //public String GetJuso()
        //{
        //    return m_Juso;
        //}

        //public String GetTelno()
        //{
        //    return m_Telno;
        //}

        //public String GetBussNm()
        //{
        //    return m_BussNm;
        //}

        //public String GetBussNo()
        //{
        //    return m_BussNo.Replace("-", "");
        //}

        public void SetInfo()
        {
            try
            {
                string sql = "";

                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // 요양기관기호
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='2'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_HosId = reader["FLD1QTY"].ToString();
                        reader.Close();
                    }

                    //// 요양기관명
                    //sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='1'";
                    //using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    //{
                    //    // 데이타는 서버에서 가져오도록 실행
                    //    OleDbDataReader reader = cmd.ExecuteReader();
                    //    if (reader.Read()) m_BussNm = reader["FLD1QTY"].ToString();
                    //    reader.Close();
                    //}

                    //// 주소
                    //sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='3'";
                    //using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    //{
                    //    // 데이타는 서버에서 가져오도록 실행
                    //    OleDbDataReader reader = cmd.ExecuteReader();
                    //    if (reader.Read()) m_Juso = reader["FLD1QTY"].ToString();
                    //    reader.Close();
                    //}

                    //// 종별
                    //sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='4'";
                    //using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    //{
                    //    // 데이타는 서버에서 가져오도록 실행
                    //    OleDbDataReader reader = cmd.ExecuteReader();
                    //    if (reader.Read()) m_HosJong = reader["FLD1CD"].ToString();
                    //    reader.Close();
                    //}

                    //// 대표자
                    //sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='5'";
                    //using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    //{
                    //    // 데이타는 서버에서 가져오도록 실행
                    //    OleDbDataReader reader = cmd.ExecuteReader();
                    //    if (reader.Read()) m_CeoNm = reader["FLD1QTY"].ToString();
                    //    reader.Close();
                    //}

                    //// 사업자 등록번호
                    //sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='16'";
                    //using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    //{
                    //    // 데이타는 서버에서 가져오도록 실행
                    //    OleDbDataReader reader = cmd.ExecuteReader();
                    //    if (reader.Read()) m_BussNo = reader["FLD1QTY"].ToString();
                    //    reader.Close();
                    //}

                    //// 전화번호
                    //m_Telno = "1577-7277";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

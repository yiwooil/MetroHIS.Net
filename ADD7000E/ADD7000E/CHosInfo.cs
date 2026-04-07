using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7000E
{
    class CHosInfo
    {
        private String m_HosId; // 요양기관기호
        private String m_CeoNm; // 대표자
        private String m_Juso;  // 사업장 소재지
        private String m_Telno; // 전화번호
        private String m_BussNm; // 상호
        private String m_BussNo; // 사업자 등록번호
        private String m_HosJong; // 종별

        public String GetHosId()
        {
            return m_HosId;
        }

        public String GetCeoNm()
        {
            return m_CeoNm;
        }

        public String GetJuso()
        {
            return m_Juso;
        }

        public String GetTelno()
        {
            return m_Telno;
        }

        public String GetBussNm()
        {
            return m_BussNm;
        }

        public String GetBussNo()
        {
            return m_BussNo.Replace("-","");
        }

        public String GetHosType()
        {
            // 요양기관 종류 01.상급종합병원급 11.종합병원 21.병원 31.의원.보건기관
            if ("1".Equals(m_HosJong)) return "01";
            if ("2".Equals(m_HosJong)) return "11";
            if ("3".Equals(m_HosJong)) return "21";
            if ("4".Equals(m_HosJong)) return "31"; // 2023년1월16일 부터 적용
            return "11";
        }

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

                    // 요양기관명
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='1'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_BussNm = reader["FLD1QTY"].ToString();
                        reader.Close();
                    }

                    // 주소
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='3'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_Juso = reader["FLD1QTY"].ToString();
                        reader.Close();
                    }

                    // 종별
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='4'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_HosJong = reader["FLD1CD"].ToString();
                        reader.Close();
                    }

                    // 대표자
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='5'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_CeoNm = reader["FLD1QTY"].ToString();
                        reader.Close();
                    }

                    // 사업자 등록번호
                    sql = "SELECT * FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='16'";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) m_BussNo = reader["FLD1QTY"].ToString();
                        reader.Close();
                    }

                    // 전화번호
                    m_Telno = "1577-7277";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}

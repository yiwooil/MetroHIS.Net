using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CTE12C_ADM
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

        public string USERID; // 등록자ID
        public string USERNM; // 등록자
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

            USERID = ""; // 등록자ID
            USERNM = ""; // 등록자
            SYSDT = ""; // 등록일자
            SYSTM = ""; // 등록시간
        }

        public void SetData(string p_pid, string p_bdedt)
        {
            try
            {
                Clear();

                string sql = "";
                sql += System.Environment.NewLine + "SELECT	PID,BEDEDT,BDIV,EXDT,USERID,SEQ,SORT_SEQ,C_CASE,SYSDT,SYSTM,RMK1";
                sql += System.Environment.NewLine + "  FROM TE12C";
                sql += System.Environment.NewLine + " WHERE PID =	'" + p_pid + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_bdedt + "'";
                sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,EXDT,BDIV,USERID,SEQ,SORT_SEQ";
                string strConn = DBHelper.GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string strPID = reader["PID"].ToString();
                        string strBEDEDT = reader["BEDEDT"].ToString();
                        string strBDIV = reader["BDIV"].ToString();
                        string strEXDT = reader["EXDT"].ToString();
                        string strUSERID = reader["USERID"].ToString();
                        string strSEQ = reader["SEQ"].ToString();
                        string strSORT_SEQ = reader["SORT_SEQ"].ToString();
                        string strSYSDT = reader["SYSDT"].ToString();
                        string strSYSTM = reader["SYSTM"].ToString();
                        string strRMK1 = reader["RMK1"].ToString();
                        string strC_CASE = reader["C_CASE"].ToString();

                        strRMK1 = strRMK1.Trim();

                        if ("PN".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase) && strRMK1.Replace(" ","").StartsWith("<ADMISSIONNOTE>", StringComparison.CurrentCultureIgnoreCase))
                        {
                            READ_CNT=1;
                            DustributeValues(strRMK1);
                            USERID = strUSERID;
                            USERNM = GetEmpnm(conn, USERID);
                            SYSDT = strSYSDT;
                            SYSTM = strSYSTM;
                            MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            break;
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DustributeValues(string p_rmk1)
        {
            int step = 0;
            string[] separator = { System.Environment.NewLine};
            string[] rmk = p_rmk1.Split(separator, StringSplitOptions.None);
            int len = rmk.Length;
            for (int i = 0; i < len; i++)
            {
                // 앞에 있는 글자 없앰.
                if (rmk[i].StartsWith("1)") || rmk[i].StartsWith("2)") || rmk[i].StartsWith("3)") || rmk[i].StartsWith("4)") || rmk[i].StartsWith("5)") || rmk[i].StartsWith("6)") || rmk[i].StartsWith("7)") || rmk[i].StartsWith("8)") || rmk[i].StartsWith("9)"))
                {
                    rmk[i] = rmk[i].Substring(2);
                }
                else if (rmk[i].StartsWith("10)") || rmk[i].StartsWith("11)") || rmk[i].StartsWith("12)") || rmk[i].StartsWith("13)") || rmk[i].StartsWith("14)") || rmk[i].StartsWith("15)") || rmk[i].StartsWith("16)") || rmk[i].StartsWith("17)") || rmk[i].StartsWith("18)") || rmk[i].StartsWith("19)"))
                {
                    rmk[i] = rmk[i].Substring(3);
                }
                // -------------
                if (rmk[i].StartsWith("C.C", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 주호소
                    if ("".Equals(CC)) CC = rmk[i];
                    step = 1;
                }
                else if (rmk[i].StartsWith("P.I", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 현병력
                    if ("".Equals(PI)) PI = rmk[i];
                    step = 2;
                }
                else if (rmk[i].StartsWith("PHX", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 과거력
                    if ("".Equals(PHX)) PHX = rmk[i];
                    step = 3;
                }
                else if (rmk[i].StartsWith("FHX", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 가족력
                    if ("".Equals(FHX)) FHX = rmk[i];
                    step = 4;
                }
                else if (rmk[i].StartsWith("ROS", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 계통문진
                    if ("".Equals(ROS)) ROS = rmk[i];
                    step = 5;
                }
                else if (rmk[i].StartsWith("PE", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 신체검진
                    if ("".Equals(PE)) PE = rmk[i];
                    step = 6;
                }
                else if (rmk[i].StartsWith("IMP", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 추정진단
                    if ("".Equals(IMP)) IMP = rmk[i];
                    step = 7;
                }
                else if (rmk[i].StartsWith("TX PLAN", StringComparison.CurrentCultureIgnoreCase))
                {
                    // 치료계획
                    if ("".Equals(TXPLAN)) TXPLAN = rmk[i];
                    step = 8;
                }
                else
                {
                    if (step == 1)
                    {
                        CC += Environment.NewLine + rmk[i];
                    }
                    else if (step == 2)
                    {
                        PI += Environment.NewLine + rmk[i];
                    }
                    else if (step == 3)
                    {
                        PHX += Environment.NewLine + rmk[i];
                    }
                    else if (step == 4)
                    {
                        FHX += Environment.NewLine + rmk[i];
                    }
                    else if (step == 5)
                    {
                        ROS += Environment.NewLine + rmk[i];
                    }
                    else if (step == 6)
                    {
                        PE += Environment.NewLine + rmk[i];
                    }
                    else if (step == 7)
                    {
                        IMP += Environment.NewLine + rmk[i];
                    }
                    else if (step == 8)
                    {
                        TXPLAN += Environment.NewLine + rmk[i];
                    }
                }
            }
        }

        private String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            return CEmployee.GetEmpnm(p_conn, p_empid);
        }

        private String GetModifyHx(OleDbConnection p_conn, String p_pid, String p_bededt, String p_bdiv, String p_exdt, String p_userid, String p_seq, String p_sort_seq)
        {
            String strRet = "";
            String sql = "";
            sql += System.Environment.NewLine + "SELECT HISSYSDT,HISSYSTM,STAT";
            sql += System.Environment.NewLine + "  FROM TE12C_HISTORY";
            sql += System.Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT='" + p_bededt + "'";
            sql += System.Environment.NewLine + "   AND BDIV='" + p_bdiv + "'";
            sql += System.Environment.NewLine + "   AND EXDT='" + p_exdt + "'";
            sql += System.Environment.NewLine + "   AND USERID='" + p_userid + "'";
            sql += System.Environment.NewLine + "   AND SEQ=" + p_seq + "";
            sql += System.Environment.NewLine + "   AND SORT_SEQ=" + p_sort_seq + "";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["HISSYSDT"].ToString() + " " + reader["HISSYSTM"].ToString().Substring(0, 4) + " " + reader["STAT"].ToString() + System.Environment.NewLine;
            }
            reader.Close();

            return strRet;
        }
    }
}

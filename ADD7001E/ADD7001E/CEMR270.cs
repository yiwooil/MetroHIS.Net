using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CEMR270
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
                sql += System.Environment.NewLine + "SELECT	PID,BEDEDT,WDATE,SEQ,MJ_HOSO,ONSET,DISEASE,HX,HX_ETC1,HX_ETC2,HX_ETC3,FX,FX_ETC,MUNJIN,MUNJIN_ETC,MENTALITY,BP1,BP2,PR,RESP,BT,GUMJIN,GUESS_JINDAN,CURE_PLAN,SYSDT,SYSTM,EMPID";
                sql += System.Environment.NewLine + "  FROM EMR270";
                sql += System.Environment.NewLine + " WHERE PID =	'" + p_pid + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_bdedt + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')=''"; // 2021.02.23 WOOIL - 삭제내역을 제외시킴.
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
                        PI = reader["DISEASE"].ToString();
                        string strHX = reader["HX"].ToString();
                        string strHX_ETC1 = reader["HX_ETC1"].ToString();
                        string strHX_ETC2 = reader["HX_ETC2"].ToString();
                        string strHX_ETC3 = reader["HX_ETC3"].ToString();
                        string strFX = reader["FX"].ToString();
                        string strFX_ETC = reader["FX_ETC"].ToString();
                        string strMUNJIN = reader["MUNJIN"].ToString();
                        string strMUNJIN_ETC = reader["MUNJIN_ETC"].ToString();
                        string strMENTALITY = reader["MENTALITY"].ToString();
                        string strBP1 = reader["BP1"].ToString();
                        string strBP2 = reader["BP2"].ToString();
                        string strPR = reader["PR"].ToString();
                        string strRESP = reader["RESP"].ToString();
                        string strBT = reader["BT"].ToString();
                        string strGUMJIN = reader["GUMJIN"].ToString();
                        IMP = reader["GUESS_JINDAN"].ToString();
                        TXPLAN = reader["CURE_PLAN"].ToString();
                        SYSDT = reader["SYSDT"].ToString();
                        SYSTM = reader["SYSTM"].ToString();
                        EMPID = reader["EMPID"].ToString();
                        EMPNM = GetEmpnm(conn, EMPID);
                        MODIFY_HX = GetModifyHx(conn, p_pid, p_bdedt, reader["WDATE"].ToString(), reader["SEQ"].ToString());
                        //
                        PHX = "";
                        string[] aHX = strHX.Split(Convert.ToChar(21));
                        if ("1".Equals(aHX[0])) PHX = "HTN";
                        if ("1".Equals(aHX[1])) PHX = "DM";
                        if ("1".Equals(aHX[2])) PHX = "Hepatitis";
                        if ("1".Equals(aHX[3])) PHX = "Tbc";
                        if ("1".Equals(aHX[4])) PHX = "Dyslipidemia";
                        if ("1".Equals(aHX[5])) PHX = "OP Hx(" + strHX_ETC1 + ")";
                        if ("1".Equals(aHX[6])) PHX = "Drug allergy(" + strHX_ETC2 + ")";
                        if ("1".Equals(aHX[7])) PHX = "Others(" + strHX_ETC3 + ")";
                        if ("1".Equals(aHX[8])) PHX = "None";
                        //
                        FHX = "";
                        string[] aFX = strFX.Split(Convert.ToChar(21));
                        if ("1".Equals(aFX[0])) FHX = "HTN";
                        if ("1".Equals(aFX[1])) FHX = "DM";
                        if ("1".Equals(aFX[2])) FHX = "Hepatitis";
                        if ("1".Equals(aFX[3])) FHX = "Others(" + strFX_ETC + ")";
                        if ("1".Equals(aFX[4])) FHX = "None";
                        //
                        ROS = "";
                        string[] aMUNJIN = strMUNJIN.Split(Convert.ToChar(21));
                        if ("1".Equals(aMUNJIN[0])) ROS = "Weakness";
                        if ("1".Equals(aMUNJIN[1])) ROS = "Chill";
                        if ("1".Equals(aMUNJIN[2])) ROS = "fatigue";
                        if ("1".Equals(aMUNJIN[3])) ROS = "wt.loss";
                        if ("1".Equals(aMUNJIN[4])) ROS = "arthralgia";
                        if ("1".Equals(aMUNJIN[5])) ROS = "myalgia";
                        if ("1".Equals(aMUNJIN[6])) ROS = "Others(" + strMUNJIN_ETC + ")";
                        //
                        PE = "";
                        string[] aMENTALITY = strMENTALITY.Split(Convert.ToChar(21));
                        if ("1".Equals(aMENTALITY[0])) PE = "Alert";
                        if ("1".Equals(aMENTALITY[1])) PE = "Drowsy";
                        if ("1".Equals(aMENTALITY[2])) PE = "Stupor";
                        if ("1".Equals(aMENTALITY[3])) PE = "Semicoma";
                        if ("1".Equals(aMENTALITY[4])) PE = "Cona";
                        PE += ",BP(" + strBP1 + "/" + strBP2 + ")";
                        PE += ",PR(" + strPR + ")";
                        PE += ",Resp(" + strRESP + ")";
                        PE += ",BT(" + strBT + ")";
                        PE += "," + strGUMJIN;
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
        }

        private String GetModifyHx(OleDbConnection p_conn, String p_pid, String p_bededt, String p_wdate, String p_seq)
        {
            String strRet = "";
            String sql = "";
            sql += System.Environment.NewLine + "SELECT	SYSDT,SYSTM";
            sql += System.Environment.NewLine + "  FROM EMR270";
            sql += System.Environment.NewLine + " WHERE PID =	'" + p_pid + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_bededt + "'";
            sql += System.Environment.NewLine + "   AND WDATE = '" + p_wdate + "'";
            sql += System.Environment.NewLine + "   AND SEQ < " + p_seq + "";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["SYSDT"].ToString() + " " + reader["SYSTM"].ToString().Substring(0, 4) + System.Environment.NewLine;
            }
            reader.Close();

            return strRet;
        }

    }
}

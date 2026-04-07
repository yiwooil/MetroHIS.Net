using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CTE12C
    {
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

        public string OP_AFTER_RMK; // 수술후 환자평가
        public string OP_AFTER_USERID; // 등록자ID
        public string OP_AFTER_USERNM; // 등록자
        public string OP_AFTER_SYSDT; // 등록일자
        public string OP_AFTER_SYSTM; // 등록시간

        public List<CTX_ING> TX_ING_LIST = new List<CTX_ING>(); // 입원기간동안의 환자상태변화

        public void Clear()
        {
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

            // 수술후 환자평가 ----------------------
            OP_AFTER_RMK = ""; // 수술후 환자평가내용
            OP_AFTER_USERID = ""; // 등록자ID
            OP_AFTER_USERNM = ""; // 등록자
            OP_AFTER_SYSDT = ""; // 등록일자
            OP_AFTER_SYSTM = ""; // 등록시간

            // 입원기간 동안의 환자상태변화 ---------
            TX_ING_LIST.Clear();
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

                        if ("CC".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 주호소
                            if ("".Equals(CC))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                CC = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("PI".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 현병력
                            if ("".Equals(PI))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                PI = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("PHX".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 과거력
                            if ("".Equals(PHX))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                PHX = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("FHX".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 가족력
                            if ("".Equals(FHX))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                FHX = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("PE".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 신체검사
                            if ("".Equals(PE))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                PE = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("ROS".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 계통문진
                            if ("".Equals(ROS))
                            {
                                USERID = strUSERID;
                                USERNM = GetEmpnm(conn, USERID);
                                SYSDT = strSYSDT;
                                SYSTM = strSYSTM;
                                ROS = strRMK1;
                                MODIFY_HX = GetModifyHx(conn, strPID, strBEDEDT, strBDIV, strEXDT, strUSERID, strSEQ, strSORT_SEQ);
                            }
                            // PN에도 넣어달라고 함.(유비스)
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                        }
                        else if ("PN".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase) && strRMK1.StartsWith("<ADMISSION NOTE>", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // CTE12C_ADM에서 처리
                        }
                        else if ("PN".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase) && strRMK1.StartsWith("<수술 후 환자상태 평가>", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // 수술후 환자평가
                            if ("".Equals(OP_AFTER_RMK))
                            {
                                OP_AFTER_RMK = strRMK1;
                                OP_AFTER_USERID = strUSERID;
                                OP_AFTER_USERNM = GetEmpnm(conn, OP_AFTER_USERID);
                                OP_AFTER_SYSDT = strSYSDT;
                                OP_AFTER_SYSTM = strSYSTM;
                            }
                        }
                        else if ("초기평가".Equals(strC_CASE, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // EMR290에 있는 내용임.
                            // EMR290을 사용하자
                        }
                        else
                        {
                            // 입원기간중 환자상태변화
                            this.AddPN(conn, strRMK1, strEXDT, strUSERID, strSYSDT, strSYSTM);
                            /*
                            bool bFind = false;
                            for (int i = 0; i < TX_ING_LIST.Count; i++)
                            {
                                if (strEXDT.Equals(TX_ING_LIST[i].EXDT) && strUSERID.Equals(TX_ING_LIST[i].USERID))
                                {
                                    // 같은 날자의 자료를 하나로 보내기 위함.
                                    // 의사별로 작성 *** 심평원 ***
                                    TX_ING_LIST[i].PN += "\r\n" + strRMK1;
                                    TX_ING_LIST[i].USERID = strUSERID; // 등록자
                                    TX_ING_LIST[i].USERNM = GetEmpnm(conn, strUSERID); // 등록자명
                                    TX_ING_LIST[i].SYSDT = strSYSDT; // 등록일자
                                    TX_ING_LIST[i].SYSTM = strSYSTM; // 등록시간
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind == false)
                            {
                                CTX_ING tx_ing = new CTX_ING();
                                tx_ing.PN = strRMK1; // 내용
                                tx_ing.EXDT = strEXDT; // 진료일자
                                tx_ing.USERID = strUSERID; // 등록자
                                tx_ing.USERNM = GetEmpnm(conn, strUSERID); // 등록자명
                                tx_ing.SYSDT = strSYSDT; // 등록일자
                                tx_ing.SYSTM = strSYSTM; // 등록시간
                                TX_ING_LIST.Add(tx_ing);
                            }
                            */
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

        private void AddPN(OleDbConnection p_conn, string p_strRMK1, string p_strEXDT, string p_strUSERID, string p_strSYSDT, string p_strSYSTM)
        {
            bool bFind = false;
            for (int i = 0; i < TX_ING_LIST.Count; i++)
            {
                if (p_strEXDT.Equals(TX_ING_LIST[i].EXDT) && p_strUSERID.Equals(TX_ING_LIST[i].USERID))
                {
                    // 같은 날자의 자료를 하나로 보내기 위함.
                    // 의사별로 작성 *** 심평원 ***
                    TX_ING_LIST[i].PN += "\r\n" + p_strRMK1;
                    TX_ING_LIST[i].USERID = p_strUSERID; // 등록자
                    TX_ING_LIST[i].USERNM = GetEmpnm(p_conn, p_strUSERID); // 등록자명
                    TX_ING_LIST[i].SYSDT = p_strSYSDT; // 등록일자
                    TX_ING_LIST[i].SYSTM = p_strSYSTM; // 등록시간
                    bFind = true;
                    break;
                }
            }
            if (bFind == false)
            {
                CTX_ING tx_ing = new CTX_ING();
                tx_ing.PN = p_strRMK1; // 내용
                tx_ing.EXDT = p_strEXDT; // 진료일자
                tx_ing.USERID = p_strUSERID; // 등록자
                tx_ing.USERNM = GetEmpnm(p_conn, p_strUSERID); // 등록자명
                tx_ing.SYSDT = p_strSYSDT; // 등록일자
                tx_ing.SYSTM = p_strSYSTM; // 등록시간
                TX_ING_LIST.Add(tx_ing);
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CCUS001
    {
        protected List<CTBL_FOM_CZITM> m_List = new List<CTBL_FOM_CZITM>();
        protected List<CLOCAL_FILE_PTH> m_FileList = new List<CLOCAL_FILE_PTH>();
        protected string m_User;

        public CCUS001()
        {
            m_List.Clear();
            m_FileList.Clear();
        }

        public bool ReadValues(string p_REQ_DATA_NO)
        {
            m_List.Clear();
            int count = 0;
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sql = "";
                sql += System.Environment.NewLine + "SELECT *";
                sql += System.Environment.NewLine + "  FROM TI83A";
                sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                sql += System.Environment.NewLine + " ORDER BY REQ_DATA_NO, SORT_SNO";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    string SORT_SNO = reader["SORT_SNO"].ToString();
                    string YADM_TRMN_ID = reader["YADM_TRMN_ID"].ToString();
                    string YADM_TRMN_NM = reader["YADM_TRMN_NM"].ToString();
                    string DTL_TXT = reader["DTL_TXT"].ToString();

                    AddList(SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT);
                }
                reader.Close();

            }

            return (count > 0);
        }

        public bool ReadFiles(string p_REQ_DATA_NO)
        {
            m_FileList.Clear();
            int count = 0;
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sql = "";
                sql += System.Environment.NewLine + "SELECT *";
                sql += System.Environment.NewLine + "  FROM TI83B";
                sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                sql += System.Environment.NewLine + " ORDER BY REQ_DATA_NO, SEQ_NO";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    string SEQ_NO = reader["SEQ_NO"].ToString();
                    string LOCAL_FILE_PTH = reader["LOCAL_FILE_PTH"].ToString();

                    AddFileList(SEQ_NO, LOCAL_FILE_PTH);
                }
                reader.Close();

            }

            return (count > 0);
        }

        protected void SaveValues(string p_REQ_DATA_NO)
        {
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string strSysDate = CUtil.GetSysDate(conn);
                string strSysTime = CUtil.GetSysTime(conn);

                // 트렌잭션 시작
                OleDbTransaction tran = conn.BeginTransaction();

                try
                {
                    // MMS를 남기지 않기위함.
                    string user = m_User;
                    if ("MMS".Equals(user, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //
                        sql = "SELECT EMPID FROM TI83 WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                        // TSQL문장과 Connection 객체를 지정   
                        OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            user = reader["EMPID"].ToString();
                        }
                        reader.Close();
                    }
                    // 그래도 MMS이면 지운다.
                    if ("MMS".Equals(user, StringComparison.CurrentCultureIgnoreCase)) user = "";

                    // 삭제하고
                    sql = "";
                    sql += System.Environment.NewLine + "DELETE TI83A";
                    sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                    OleDbCommand dcmd = new OleDbCommand(sql, conn, tran);
                    dcmd.ExecuteNonQuery();

                    // 입력
                    int count = m_List.Count;
                    for (int i = 0; i < count; i++)
                    {
                        /*
                        sql = "";
                        sql += "INSERT INTO TI83A(REQ_DATA_NO, SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT, LABEL_NM, FOM_CZITM_CD, SYSDT, SYSTM, EMPID)";
                        sql += "VALUES('" + p_REQ_DATA_NO + "'," + m_List[i].SORT_SNO + ",'" + m_List[i].YADM_TRMN_ID + "','" + m_List[i].YADM_TRMN_NM + "','" + m_List[i].DTL_TXT.Replace("'","''") + "','" + m_List[i].LABEL_NM + "', '', CONVERT(VARCHAR,GETDATE(),112), REPLACE(CONVERT(VARCHAR,GETDATE(),8),':',''), '" + m_User + "')";

                        // TSQL문장과 Connection 객체를 지정   
                        OleDbCommand cmd = new OleDbCommand(sql, conn);

                        cmd.ExecuteNonQuery();
                        */
                        sql = "";
                        sql += "INSERT INTO TI83A(REQ_DATA_NO, SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT, LABEL_NM, FOM_CZITM_CD, SYSDT, SYSTM, EMPID)";
                        sql += "VALUES(?,?,?,?,?,?,?,?,?,?)";

                        // TSQL문장과 Connection 객체를 지정   
                        OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                        cmd.Parameters.Add(new OleDbParameter("@p1", p_REQ_DATA_NO));
                        cmd.Parameters.Add(new OleDbParameter("@p2", m_List[i].SORT_SNO));
                        cmd.Parameters.Add(new OleDbParameter("@p3", m_List[i].YADM_TRMN_ID));
                        cmd.Parameters.Add(new OleDbParameter("@p4", m_List[i].YADM_TRMN_NM));
                        cmd.Parameters.Add(new OleDbParameter("@p5", m_List[i].DTL_TXT));
                        cmd.Parameters.Add(new OleDbParameter("@p6", m_List[i].LABEL_NM));
                        cmd.Parameters.Add(new OleDbParameter("@p7", ""));
                        cmd.Parameters.Add(new OleDbParameter("@p8", strSysDate));
                        cmd.Parameters.Add(new OleDbParameter("@p9", strSysTime));
                        cmd.Parameters.Add(new OleDbParameter("@p10", user));

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public List<CTBL_FOM_CZITM> GetList()
        {
            return m_List;
        }

        public List<CLOCAL_FILE_PTH> GetFileList()
        {
            return m_FileList;
        }

        protected void AddList(string p_YADM_TRMN_ID, string p_YADM_TRMN_NM, string p_DTL_TXT)
        {
            if ("".Equals(p_DTL_TXT) == false)
            {
                int count = m_List.Count;
                int idx = count + 1;
                AddList(idx.ToString(), p_YADM_TRMN_ID, p_YADM_TRMN_NM, p_DTL_TXT);
            }
        }

        protected void AddList(string p_SORT_SNO, string p_YADM_TRMN_ID, string p_YADM_TRMN_NM, string p_DTL_TXT)
        {
            CTBL_FOM_CZITM data = new CTBL_FOM_CZITM(p_SORT_SNO, p_YADM_TRMN_ID, p_YADM_TRMN_NM, p_DTL_TXT);
            m_List.Add(data);
        }

        protected void AddFileList(string p_SEQ_NO, string p_LOCAL_FILE_PTH)
        {
            CLOCAL_FILE_PTH data = new CLOCAL_FILE_PTH();
            data.SEQ_NO = p_SEQ_NO;
            data.LOCAL_FILE_PTH = p_LOCAL_FILE_PTH;
            m_FileList.Add(data);
        }

    }
}

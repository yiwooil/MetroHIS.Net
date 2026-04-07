using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class CTI84A
    {
        static public void SaveMake(string p_form_cd, string p_req_data_no, string p_dmd_no, string p_sp_sno, string p_empid)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TI84A";
                sql += Environment.NewLine + " WHERE REQ_DATA_NO='" + p_req_data_no + "'";
                sql += Environment.NewLine + "   AND DMD_NO='" + p_dmd_no + "'";
                sql += Environment.NewLine + "   AND SP_SNO='" + p_sp_sno + "'";
                sql += Environment.NewLine + "   AND SUPL_DATA_FOM_CD='" + p_form_cd + "'";

                int cnt = 0;
                CSQLHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    return true;
                });

                if (cnt < 1)
                {
                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TI84A(REQ_DATA_NO, DMD_NO, SP_SNO, SUPL_DATA_FOM_CD, MAKE_DT, MAKE_TM, MAKE_ID)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?)";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.Parameters.Add(new OleDbParameter("@1", p_req_data_no));
                    cmd.Parameters.Add(new OleDbParameter("@2", p_dmd_no));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_sp_sno));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_form_cd));
                    cmd.Parameters.Add(new OleDbParameter("@5", sysdt));
                    cmd.Parameters.Add(new OleDbParameter("@6", systm));
                    cmd.Parameters.Add(new OleDbParameter("@7", p_empid));

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "";
                    sql += Environment.NewLine + "UPDATE TI84A";
                    sql += Environment.NewLine + "   SET MAKE_DT=?";
                    sql += Environment.NewLine + "     , MAKE_TM=?";
                    sql += Environment.NewLine + "     , MAKE_ID=?";
                    sql += Environment.NewLine + " WHERE REQ_DATA_NO=?";
                    sql += Environment.NewLine + "   AND DMD_NO=?";
                    sql += Environment.NewLine + "   AND SP_SNO=?";
                    sql += Environment.NewLine + "   AND SUPL_DATA_FOM_CD=?"; 


                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.Parameters.Add(new OleDbParameter("@1", sysdt));
                    cmd.Parameters.Add(new OleDbParameter("@2", systm));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_empid));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_req_data_no));
                    cmd.Parameters.Add(new OleDbParameter("@5", p_dmd_no));
                    cmd.Parameters.Add(new OleDbParameter("@6", p_sp_sno));
                    cmd.Parameters.Add(new OleDbParameter("@7", p_form_cd));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        static public void SaveSend(string p_form_cd, string p_req_data_no, string p_dmd_no, string p_sp_sno, string p_empid)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TI84A";
                sql += Environment.NewLine + " WHERE REQ_DATA_NO='" + p_req_data_no + "'";
                sql += Environment.NewLine + "   AND DMD_NO='" + p_dmd_no + "'";
                sql += Environment.NewLine + "   AND SP_SNO='" + p_sp_sno + "'";
                sql += Environment.NewLine + "   AND SUPL_DATA_FOM_CD='" + p_form_cd + "'";

                int cnt = 0;
                CSQLHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    return true;
                });

                if (cnt < 1)
                {
                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TI84A(REQ_DATA_NO, DMD_NO, SP_SNO, SUPL_DATA_FOM_CD, SEND_DT, SEND_TM, SEND_ID)";
                    sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?)";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.Parameters.Add(new OleDbParameter("@1", p_req_data_no));
                    cmd.Parameters.Add(new OleDbParameter("@2", p_dmd_no));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_sp_sno));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_form_cd));
                    cmd.Parameters.Add(new OleDbParameter("@5", sysdt));
                    cmd.Parameters.Add(new OleDbParameter("@6", systm));
                    cmd.Parameters.Add(new OleDbParameter("@7", p_empid));

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "";
                    sql += Environment.NewLine + "UPDATE TI84A";
                    sql += Environment.NewLine + "   SET SEND_DT=?";
                    sql += Environment.NewLine + "     , SEND_TM=?";
                    sql += Environment.NewLine + "     , SEND_ID=?";
                    sql += Environment.NewLine + " WHERE REQ_DATA_NO=?";
                    sql += Environment.NewLine + "   AND DMD_NO=?";
                    sql += Environment.NewLine + "   AND SP_SNO=?";
                    sql += Environment.NewLine + "   AND SUPL_DATA_FOM_CD=?";


                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.Parameters.Add(new OleDbParameter("@1", sysdt));
                    cmd.Parameters.Add(new OleDbParameter("@2", systm));
                    cmd.Parameters.Add(new OleDbParameter("@3", p_empid));
                    cmd.Parameters.Add(new OleDbParameter("@4", p_req_data_no));
                    cmd.Parameters.Add(new OleDbParameter("@5", p_dmd_no));
                    cmd.Parameters.Add(new OleDbParameter("@6", p_sp_sno));
                    cmd.Parameters.Add(new OleDbParameter("@7", p_form_cd));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}

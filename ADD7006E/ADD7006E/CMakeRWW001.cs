using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRWW001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RWW001_LIST.Clear();

            if (p_dsdata.IOFG == "2")
            {
                // 입원
                string a04_bededt = "";
                string a04_bedehm = "";
                string a04_dptcd = "";
                string a04_insdptcd = "";
                string a04_insdptcd2 = "";

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT A04.BEDEDT,A04.BEDEHM,A04.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
                sql += System.Environment.NewLine + "  FROM TA04 A04 LEFT JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
                sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    a04_bededt = row["BEDEDT"].ToString();
                    a04_bedehm = row["BEDEHM"].ToString();
                    a04_dptcd = row["DPTCD"].ToString();
                    a04_insdptcd = row["INSDPTCD"].ToString();
                    a04_insdptcd2 = row["INSDPTCD2"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                CDataRWW001 data = null;
                // 
                sql = "";
                sql += System.Environment.NewLine + "SELECT CHKDT,CHKTM,BP,PR,RR,TMP";
                sql += System.Environment.NewLine + "  FROM TU64";
                sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND NOT (BP='/' AND PR='' AND RR='' AND TMP='')";
                sql += System.Environment.NewLine + " ORDER BY CHKDT,CHKTM";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {

                    if (data ==null)
                    {
                        // 처음에 한번만
                        data = new CDataRWW001();
                        data.Clear();
                        p_dsdata.RWW001_LIST.Add(data);

                        data.BEDEDT = a04_bededt;
                        data.BEDEHM = a04_bedehm;
                        data.DPTCD = a04_dptcd;
                        data.INSDPTCD = a04_insdptcd;
                        data.INSDPTCD2 = a04_insdptcd2;
                    }


                    data.CHKDT.Add(row["CHKDT"].ToString()); // 측정일자
                    data.CHKTM.Add(row["CHKTM"].ToString()); // 측정시간
                    data.BP.Add(row["BP"].ToString()); // 혈압
                    data.PR.Add(row["PR"].ToString()); // 맥박
                    data.RR.Add(row["RR"].ToString()); // 호흡
                    data.TMP.Add(row["TMP"].ToString()); // 체온

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 섭취량 & 배설량
                sql = "";
                sql += System.Environment.NewLine + "SELECT CHKDT,CHKTM,ORAL_C,ORAL_V,PATE_C,PATE_V,BLOOD_C,BLOOD_V,URINE,DR_SU,S_V_O_C,S_V_O_V,RMK,EID,UPDID,STOOL,VOMIT,OTHERS";
                sql += System.Environment.NewLine + "  FROM TU57_2";
                sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + " ORDER BY CHKDT,CHKTM";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {

                    if (data == null)
                    {
                        // 처음에 한번만
                        data = new CDataRWW001();
                        data.Clear();
                        p_dsdata.RWW001_LIST.Add(data);

                        data.BEDEDT = a04_bededt;
                        data.BEDEHM = a04_bedehm;
                        data.DPTCD = a04_dptcd;
                        data.INSDPTCD = a04_insdptcd;
                        data.INSDPTCD2 = a04_insdptcd2;
                    }

                    data.IO_CHKDT.Add(row["CHKDT"].ToString());
                    data.IO_CHKTM.Add(row["CHKTM"].ToString());
                    data.ORAL_C.Add(row["ORAL_C"].ToString());
                    data.ORAL_V.Add(row["ORAL_V"].ToString());
                    data.PATE_C.Add(row["PATE_C"].ToString());
                    data.PATE_V.Add(row["PATE_V"].ToString());
                    data.BLOOD_C.Add(row["BLOOD_C"].ToString());
                    data.BLOOD_V.Add(row["BLOOD_V"].ToString());
                    data.URINE.Add(row["URINE"].ToString());
                    data.DR_SU.Add(row["DR_SU"].ToString());
                    data.S_V_O_C.Add(row["S_V_O_C"].ToString());
                    data.S_V_O_V.Add(row["S_V_O_V"].ToString());
                    data.RMK.Add(row["RMK"].ToString());
                    data.EID.Add(row["EID"].ToString());
                    data.UPDID.Add(row["UPDID"].ToString());
                    data.STOOL.Add(row["STOOL"].ToString());
                    data.VOMIT.Add(row["VOMIT"].ToString());
                    data.OTHERS.Add(row["OTHERS"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }
            else
            {
                // 외래
            }
        }
    }
}

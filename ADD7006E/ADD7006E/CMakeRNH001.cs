using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRNH001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RNH001_LIST.Clear();

            CDataRNH001 data = new CDataRNH001();

            // 혈액투석
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT CHKDT, TRETMENT_STTIME, TRETMENT_EDTIME, LastWt, HMBeCurWt, HMAfCurWt, HMveWay, UFTOTAL, AntiBaseOqty, MaintOqty, HMMachine, HMFluid, EID ";
            sql += System.Environment.NewLine + "  FROM TU67 ";
            sql += System.Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND CHKDT >= '" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND CHKDT <= '" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY CHKDT,SEQ";

            int cnt = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                cnt++;

                data.CHKDT.Add(row["CHKDT"].ToString());
                data.TRETMENT_STTIME.Add(row["TRETMENT_STTIME"].ToString()); // 시작시간
                data.TRETMENT_EDTIME.Add(row["TRETMENT_EDTIME"].ToString()); // 종료시간
                data.LastWt.Add(row["LastWt"].ToString()); // 건체중
                data.HMBeCurWt.Add(row["HMBeCurWt"].ToString()); // 투석 전 체중
                data.HMAfCurWt.Add(row["HMAfCurWt"].ToString()); // 투석 후 체중
                data.HMveWay.Add(row["HMveWay"].ToString()); // 혈관통로
                data.UFTOTAL.Add(row["UFTOTAL"].ToString()); // 목표수분제거량
                data.AntiBaseOqty.Add(row["AntiBaseOqty"].ToString()); // 항응고요법초기
                data.MaintOqty.Add(row["MaintOqty"].ToString()); // 항응고요법유지
                data.HMMachine.Add(row["HMMachine"].ToString()); // 투석기
                data.HMFluid.Add(row["HMFluid"].ToString()); // 투석액
                data.EID.Add(row["EID"].ToString()); // 작성자
                data.ENM.Add(CUtil.GetEmpnm(row["EID"].ToString(), p_conn)); // 작성자 성명


                return MetroLib.SqlHelper.CONTINUE;
            });

            if (cnt < 1) return; // 혈액투석내용이 없으면 종료

            // 혈액투석상세(TU67A)
            sql = "";
            sql += System.Environment.NewLine + "SELECT CHKDT, CHKTM, VTM, Vpressure, Vpulsation, Vvein, VSPEED";
            sql += System.Environment.NewLine + "  FROM TU67A ";
            sql += System.Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND CHKDT >= '" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND CHKDT <= '" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY CHKDT,SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                data.VCHKDT.Add(row["CHKDT"].ToString()); // 측정일자
                data.VCHKTM.Add(row["CHKTM"].ToString()); // 측정시간
                data.VTM.Add(row["VTM"].ToString()); // 혈압
                data.Vpressure.Add(row["Vpressure"].ToString()); // 맥박
                data.Vpulsation.Add(row["Vpulsation"].ToString()); // 혈류속도
                data.Vvein.Add(row["Vvein"].ToString()); // 동맥압
                data.VSPEED.Add(row["VSPEED"].ToString()); // 정맥압

                return MetroLib.SqlHelper.CONTINUE;
            });

            // 간호기록(TU67B)
            sql = "";
            sql += System.Environment.NewLine + "SELECT CHKDT, CHKTM, Nursing, EID";
            sql += System.Environment.NewLine + "  FROM TU67B ";
            sql += System.Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND CHKDT >= '" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND CHKDT <= '" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY CHKDT,SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                data.N_CHKDT.Add(row["CHKDT"].ToString()); // 기록일자
                data.N_CHKTM.Add(row["CHKTM"].ToString()); // 기록시간
                data.N_Nursing.Add(row["Nursing"].ToString()); // 간호기록
                data.N_EID.Add(row["EID"].ToString()); // 간호사
                data.N_ENM.Add(CUtil.GetEmpnm(row["EID"].ToString(), p_conn)); // 간호사 성명

                return MetroLib.SqlHelper.CONTINUE;
            });


            // 입원내역 or 접수내역
            if (p_dsdata.IOFG == "2")
            {
                sql = "";
                sql += System.Environment.NewLine + "SELECT A04.DPTCD,A04.PDRID";
                sql += System.Environment.NewLine + "     , A07.DRNM";
                sql += System.Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
                sql += System.Environment.NewLine + "  FROM TA04 A04 LEFT JOIN TA07 A07 ON A07.DRID=A04.PDRID";
                sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
                sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + " ORDER BY A04.BEDEDT,A04.BEDEHM";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    data.DPTCD = row["DPTCD"].ToString();
                    data.INSDPTCD = row["INSDPTCD"].ToString();
                    data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                    data.DRID = row["PDRID"].ToString();
                    data.DRNM = row["DRNM"].ToString();
                    return true;
                });
            }
            else
            {
                sql = "";
                sql += System.Environment.NewLine + "SELECT S21.DPTCD,S21.DRID";
                sql += System.Environment.NewLine + "     , A07.DRNM";
                sql += System.Environment.NewLine + "     , A09.INSDPTCD, A09.INSDPTCD2";
                sql += System.Environment.NewLine + "  FROM TS21 S21 LEFT JOIN TA07 A07 ON A07.DRID=S21.DRID";
                sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=S21.DPTCD";
                sql += System.Environment.NewLine + " WHERE S21.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND S21.EXDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(S21.CCFG,'') IN ('','0')";
                sql += System.Environment.NewLine + " ORDER BY S21.HMS DESC";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    data.DPTCD = row["DPTCD"].ToString();
                    data.INSDPTCD = row["INSDPTCD"].ToString();
                    data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                    data.DRID = row["DRID"].ToString();
                    data.DRNM = row["DRNM"].ToString();
                    return true;
                });
            }

            // 진단
            if (p_dsdata.IOFG == "2")
            {
                // TT05
                sql = "";
                sql += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql += System.Environment.NewLine + "  FROM TT05";
                sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND BDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    data.DXD.Add(row["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }
            else
            {
                // TS06
                sql = "";
                sql += System.Environment.NewLine + "SELECT ROFG,DXD,DACD";
                sql += System.Environment.NewLine + "  FROM TS06";
                sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND EXDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND DPTCD='ER'";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    data.DXD.Add(row["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }

            p_dsdata.RNH001_LIST.Add(data);
        }
    }
}

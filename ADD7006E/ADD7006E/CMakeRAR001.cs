using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRAR001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RAR001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U93.PT_INDT, U93.PT_INTM, U93.PT_OUTDT, U93.PT_OUTTM";
            sql += System.Environment.NewLine + "     , U93.ANDRID, A07.DRNM AS ANDRNM";
            sql += System.Environment.NewLine + "     , U93.EMPID, U93.WDATE, U93.WTIME";
            sql += System.Environment.NewLine + "     , U93.VOM_NRS_1, U93.VOM_NRS_2, U93.PAINCASE";
            sql += System.Environment.NewLine + "     , U93.PCA_1, U93.PCA_2, U93.PCA_3, U93.PCA_TXT";
            sql += System.Environment.NewLine + "     , U93.PARSCR1_1, U93.PARSCR1_2, U93.PARSCR1_3, U93.PARSCR1_4, U93.PARSCR1_5";
            sql += System.Environment.NewLine + "     , U93.PARSCR2_1, U93.PARSCR2_2, U93.PARSCR2_3, U93.PARSCR2_4, U93.PARSCR2_5";
            sql += System.Environment.NewLine + "     , U93.PAINDT1, U93.PAINDT2, U93.EMSSDT1, U93.EMSSDT2, U93.ASM_RST_TXT1, U93.ASM_RST_TXT2";
            sql += System.Environment.NewLine + "     , U93.OPDT, U93.DPTCD, U93.OPSEQ, U93.SEQ";
            sql += System.Environment.NewLine + "  FROM TU93 U93 LEFT JOIN TA07 A07 ON A07.DRID = U93.ANDRID";
            sql += System.Environment.NewLine + " WHERE U93.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND U93.OPDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND U93.OPDT<='" + p_dsdata.TODT + "'";


            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRAR001 data = new CDataRAR001();
                data.Clear();

                data.PT_INDT = row["PT_INDT"].ToString();  // 도착일자
                data.PT_INTM = row["PT_INTM"].ToString();  // 도착시간
                data.PT_OUTDT = row["PT_OUTDT"].ToString(); // 퇴실일자
                data.PT_OUTTM = row["PT_OUTTM"].ToString(); // 퇴실시간
                data.ANDRID = row["ANDRID"].ToString();  // 퇴실결정의사
                data.ANDRNM = row["ANDRNM"].ToString();  // 퇴실결정의사성명(TA07)
                data.EMPID = row["EMPID"].ToString();    // 작성자
                data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn);    // 작성자성명(TA07, TA13)
                data.WDATE = row["WDATE"].ToString();     // 작성일자
                data.WTIME = row["WTIME"].ToString();    // 작성시간

                data.VOM_NRS_1 = row["VOM_NRS_1"].ToString(); // 1차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
                data.VOM_NRS_2 = row["VOM_NRS_2"].ToString(); // 2차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
                data.PAINCASE = row["PAINCASE"].ToString();  // 1차(통증평가도구) + char(21) + 2차(통증평가도구)

                data.PCA_1 = row["PCA_1"].ToString(); // None
                data.PCA_2 = row["PCA_2"].ToString(); // Epidural
                data.PCA_3 = row["PCA_3"].ToString(); // IV
                data.PCA_TXT = row["PCA_TXT"].ToString(); // 평문

                data.PARSCR1_1 = row["PARSCR1_1"].ToString(); // 활동성 점수(도착시)(0~2)
                data.PARSCR1_2 = row["PARSCR1_2"].ToString(); // 호흡 점수(도착시)(0~2)
                data.PARSCR1_3 = row["PARSCR1_3"].ToString(); // 순환 점수(도착시)(0~2)
                data.PARSCR1_4 = row["PARSCR1_4"].ToString(); // 의식 점수(도착시)(0~2)
                data.PARSCR1_5 = row["PARSCR1_5"].ToString(); // 피부색 점수(도착시)(0~2)

                data.PARSCR2_1 = row["PARSCR2_1"].ToString(); // 활동성 점수(퇴실시)(0~2)
                data.PARSCR2_2 = row["PARSCR2_2"].ToString(); // 호흡 점수(퇴실시)(0~2)
                data.PARSCR2_3 = row["PARSCR2_3"].ToString(); // 순환 점수(퇴실시)(0~2)
                data.PARSCR2_4 = row["PARSCR2_4"].ToString(); // 의식 점수(퇴실시)(0~2)
                data.PARSCR2_5 = row["PARSCR2_4"].ToString(); // 피부색 점수(퇴실시)(0~2)

                data.PAINDT1 = row["PAINDT1"].ToString(); // 통증 평가일시1
                data.PAINDT2 = row["PAINDT2"].ToString(); // 통증 평가일시2
                data.EMSSDT1 = row["EMSSDT1"].ToString(); // 오심구토 평가일시1
                data.EMSSDT2 = row["EMSSDT2"].ToString(); // 오심구토 평가일시2
                data.ASM_RST_TXT1 = row["ASM_RST_TXT1"].ToString(); // 오심구토 결과평가상세1
                data.ASM_RST_TXT1 = row["ASM_RST_TXT2"].ToString(); // 오심구토 결과평가상세2

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT WTIME, SBP, DBP, HR, RR, BT, SPO2, RMK";
                sql2 += System.Environment.NewLine + "  FROM TU93A";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.VTSG_DT.Add(row["PT_INDT"].ToString());// *** TU93의 도착일자 임 ***
                    data.VTSG_TM.Add(row2["WTIME"].ToString());
                    data.SBP.Add(row2["SBP"].ToString());
                    data.DBP.Add(row2["DBP"].ToString());
                    data.HR.Add(row2["HR"].ToString());
                    data.RR.Add(row2["RR"].ToString());
                    data.BT.Add(row2["BT"].ToString());
                    data.SPO2.Add(row2["SPO2"].ToString());
                    data.RMK.Add(row2["RMK"].ToString());


                    return true;
                });

                p_dsdata.RAR001_LIST.Add(data);

                return true;
            });
        }
    }
}

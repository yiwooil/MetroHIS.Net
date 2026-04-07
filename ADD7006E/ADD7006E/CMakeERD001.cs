using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeERD001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.ERD001_LIST.Clear();

            // 진단검사결과지
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT DISTINCT SPCNO, SEX, AGE, RCVDT, RCVTM, STSCD, SPCFOOTSEQ, PTHRPT, DIAGNOSIS, ORDDT, ORDTM, SPCNM, MAJNM, AGEDIV, BLOODDT, BLOODTM, DEPTCD, MAJDR, ORDDR ";
            sql += System.Environment.NewLine + "  FROM TC201 ";
            sql += System.Environment.NewLine + " WHERE PTID = '" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND ORDDT >= '" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND ORDDT <= '" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND (CANCELFG != '1' OR CANCELFG IS NULL) ";
            sql += System.Environment.NewLine + "   AND STSCD >= 1";
            sql += System.Environment.NewLine + " ORDER BY ORDDT DESC, SPCNO DESC";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A.SPCNO, A.TESTCD, A.SEQ, A.APPDT, A.APPTM, A.TESTDIV, A.TESTRSTTYPE, A.HEADTESTCD ";
                sql2 += System.Environment.NewLine + "     , A.SITECD, A.PTID, A.RSTFG, A.WSCD, A.SPCCD, A.RSTVAL, A.REFERCHK, A.PANICCHK ";
                sql2 += System.Environment.NewLine + "     , A.DELTACHK, A.PICKCD, A.MICRORSTTYPE, A.STAINSEQ, A.CULTURESEQ, A.FOOTSEQ, A.MODIFYFG ";
                sql2 += System.Environment.NewLine + "     , A.DLYFG, A.STATFG, A.MANSTATFG, A.EQUIPCD, A.VFYID, A.VFYDT, A.VFYTM, A.PRTFG, A.PRTDT ";
                sql2 += System.Environment.NewLine + "     , A.PRTTM, A.STSCD, A.CANCELFG, A.CANCELCD, A.REGDR, A.SPECDR, B.ABBRNM ";
                sql2 += System.Environment.NewLine + "     , B.DATATYPE, B.DATALEN, B.KEYPAD, B.RSTFG, B.NORSTFG, B.QUERYFG, B.NORSTQUERYFG, B.ONOFFFG, B.WEEKDAYDIV ";
                sql2 += System.Environment.NewLine + "  FROM TC301 A INNER JOIN TC001 B ON B.TESTCD = A.TESTCD AND B.APPDT = A.APPDT AND B.APPTM = A.APPTM";
                sql2 += System.Environment.NewLine + " WHERE A.SPCNO = '" + row["SPCNO"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND A.STSCD >= '7'";
                sql2 += System.Environment.NewLine + "   AND (A.CANCELFG != '1' OR A.CANCELFG IS NULL) ";
                sql2 += System.Environment.NewLine + "   AND ((B.RSTFG = '1') OR ((RTRIM(A.RSTVAL) != '' OR A.FOOTSEQ > 0 OR A.STAINSEQ > 0 OR A.CULTURESEQ > 0) AND B.QUERYFG = '1') OR (B.NORSTQUERYFG = '1')) ";
                sql2 += System.Environment.NewLine + " ORDER BY A.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    string testdiv = row2["TESTDIV"].ToString();
                    if (testdiv == "1")
                    {

                        CDataERD001 data = new CDataERD001();
                        data.Clear();

                        data.DPTCD = row["DEPTCD"].ToString(); // 처방과
                        data.INSDPTCD = GetInsdptcd(data.DPTCD, out data.INSDPTCD2, p_conn); // 진료과목
                        data.DRID = (row["ORDDR"].ToString() == "" ? row["MAJDR"].ToString() : row["ORDDR"].ToString()); // 처방의
                        data.DRNM = GetDrnm(data.DRID, p_conn); // 의사명
                        data.ORDDT = row["ORDDT"].ToString(); // 처방일자
                        data.ORDTM = row["ORDTM"].ToString(); // 처방시간
                        data.SPCNM = row["SPCNM"].ToString(); // 검체명
                        data.BLOODDT = row["BLOODDT"].ToString(); // 채혈일자
                        data.BLOODTM = row["BLOODTM"].ToString(); // 채혈시간
                        data.RCVDT = row["RCVDT"].ToString(); // 접수일자
                        data.RCVTM = row["RCVTM"].ToString(); // 접수시간

                        data.VFYDT = row2["VFYDT"].ToString(); // 검사일자
                        data.VFYTM = row2["VFYTM"].ToString(); // 검사시간
                        data.TESTCD = row2["TESTCD"].ToString(); // 검사코드
                        data.TESTNM = row2["ABBRNM"].ToString(); // 검사명
                        data.RSTVAL = row2["RSTVAL"].ToString(); // 검사결과
                        data.REFERCHK = row2["REFERCHK"].ToString();
                        data.PANICCHK = row2["PANICCHK"].ToString();
                        data.DELTACHK = row2["DELTACHK"].ToString();

                        // 결과단위
                        string sql3 = "";
                        sql3 = "";
                        sql3 += System.Environment.NewLine + "SELECT A.TESTCD, A.EQUIPCD, A.SPCCD, A.APPDT, A.APPTM, A.EQUIPSEQ, A.SPCSEQ, A.AUTOVERIFY";
                        sql3 += System.Environment.NewLine + "     , A.TUBECD, A.VOLCD, A.UNITCD, A.BARCNT, A.TURNDAY, A.TURNTIME, A.MORFG, A.PANICFG, A.PANICFR";
                        sql3 += System.Environment.NewLine + "     , A.PANICTO, A.DELTAFG, A.DELTATYPE, A.DELTAFR, A.DELTATO, A.DELDT";
                        sql3 += System.Environment.NewLine + "     , B.FIELD1";
                        sql3 += System.Environment.NewLine + "  FROM TC002 A LEFT JOIN TC032 B ON B.CDDIV='C207' AND B.CDVAL1=A.UNITCD";
                        sql3 += System.Environment.NewLine + " WHERE A.TESTCD = '" + row2["TESTCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND A.APPDT = '" + row2["APPDT"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND A.APPTM = '" + row2["APPTM"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND A.EQUIPCD = '" + row2["EQUIPCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND A.SPCCD = '" + row2["SPCCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + " ORDER BY A.EQUIPSEQ, A.SPCSEQ";

                        MetroLib.SqlHelper.GetDataRow(sql3, p_conn, delegate(DataRow row3)
                        {
                            data.UNIT = row3["FIELD1"].ToString();

                            return false; // 한번만
                        });

                        // 참고치
                        sql3 = "";
                        sql3 += System.Environment.NewLine + "SELECT TESTCD, EQUIPCD, SPCCD, APPDT, APPTM, SEX, AGEFR, AGETO, AGEDIV";
                        sql3 += System.Environment.NewLine + "     , REFERFR, SIGNFR, SIGNTO, REFERTO, REFER, SEQ, DELDT";
                        sql3 += System.Environment.NewLine + "  FROM TC004 ";
                        sql3 += System.Environment.NewLine + " WHERE TESTCD = '" + row2["TESTCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND EQUIPCD = '" + row2["EQUIPCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND SPCCD = '" + row2["SPCCD"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND APPDT = '" + row2["APPDT"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND APPTM = '" + row2["APPTM"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "   AND SEX IN ('" + row["SEX"].ToString() + "', 'A') ";
                        sql3 += System.Environment.NewLine + "   AND (( CASE '" + row["AGEDIV"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "          WHEN 'Y'";
                        sql3 += System.Environment.NewLine + "          THEN ";
                        sql3 += System.Environment.NewLine + "              CASE ";
                        sql3 += System.Environment.NewLine + "                  WHEN REPLACE(CONVERT(VARCHAR(5), CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME) - CAST('" + row["AGE"].ToString() + "' AS INT), 10), '-', '') > SUBSTRING('" + row["BLOODDT"].ToString() + "', 5, 4)";
                        sql3 += System.Environment.NewLine + "                  THEN (DATEDIFF(YYYY, CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME) - CAST('" + row["AGE"].ToString() + "' AS INT), CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME)) - 1) * 365";
                        sql3 += System.Environment.NewLine + "                  ELSE DATEDIFF(YYYY, CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME) - CAST('" + row["AGE"].ToString() + "' AS INT), CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME)) * 365";
                        sql3 += System.Environment.NewLine + "              END ";
                        sql3 += System.Environment.NewLine + "          WHEN 'M'";
                        sql3 += System.Environment.NewLine + "          THEN DATEDIFF(MM, CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME) - CAST('" + row["AGE"].ToString() + "' AS INT), CAST('" + row["BLOODDT"].ToString() + "' AS DATETIME)) * 30";
                        sql3 += System.Environment.NewLine + "          WHEN 'D'";
                        sql3 += System.Environment.NewLine + "          THEN '" + row["AGE"].ToString() + "'";
                        sql3 += System.Environment.NewLine + "          END BETWEEN AGEFR AND AGETO)";
                        sql3 += System.Environment.NewLine + "        OR";
                        sql3 += System.Environment.NewLine + "        (RTRIM(AGEFR) = '' AND RTRIM(AGETO) = '')";
                        sql3 += System.Environment.NewLine + "       )";

                        MetroLib.SqlHelper.GetDataRow(sql3, p_conn, delegate(DataRow row3)
                        {
                            data.REFERFR = row3["REFERFR"].ToString();
                            data.REFERTO = row3["REFERTO"].ToString();
                            data.SIGNFR = row3["SIGNFR"].ToString();
                            data.SIGNTO = row3["SIGNTO"].ToString();

                            return false;
                        });

                        p_dsdata.ERD001_LIST.Add(data);

                    }

                    return true;
                });

                return true;
            });
        }

        private string GetDrnm(string p_drid, OleDbConnection p_conn)
        {
            if (p_drid == "") return "";
            string ret = "";
            string sql = "SELECT DRNM FROM TA07 WHERE DRID='" + p_drid + "'";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                ret = row["DRNM"].ToString();
                return false;
            });
            return ret;
        }

        private string GetInsdptcd(string p_dptcd, out string p_insdptcd2, OleDbConnection p_conn)
        {
            p_insdptcd2 = "";
            if (p_dptcd == "") return "";
            string insdptcd2 = "";
            string ret = "";
            string sql = "SELECT INSDPTCD, INSDPTCD2 FROM TA09 WHERE DPTCD='" + p_dptcd + "'";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                ret = row["INSDPTCD"].ToString();
                insdptcd2 = row["INSDPTCD2"].ToString();
                return false;
            });
            if (ret == "01") p_insdptcd2 = insdptcd2;
            return ret;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRAA001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RAA001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 의뢰내역
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U01.PID,U01.OPDT,U01.DPTCD,U01.OPSEQ,U01.SEQ";
            sql += System.Environment.NewLine + "     , U01.OPSDT,U01.OPSHR,U01.OPSMN,U01.OPEDT,U01.OPEHR,U01.OPEMN";
            sql += System.Environment.NewLine + "     , U03.ANSDT,U03.ANSHR,U03.ANSMN,U03.ANEDT,U03.ANEHR,U03.ANEMN,U03.ANEDR,U03.USRID,U03.ENTDT,U03.ENTMS,U03.ANENO";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "  FROM TU01 U01 INNER JOIN TU03 U03 ON U03.PID=U01.PID AND U03.OPDT=U01.OPDT AND U03.DPTCD=U01.DPTCD AND U03.OPSEQ=U01.OPSEQ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID = U03.DRID";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + p_dsdata.TODT + "'";
            //sql += System.Environment.NewLine + "   AND ISNULL(U01.CANCL,'')<>'1'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRAA001 data = new CDataRAA001();
                data.Clear();

                data.OPSDT = row["OPSDT"].ToString(); // 수술시작일
                data.OPSHR = row["OPSHR"].ToString();
                data.OPSMN = row["OPSMN"].ToString();
                data.OPEDT = row["OPEDT"].ToString(); // 수술종료일
                data.OPEHR = row["OPEHR"].ToString();
                data.OPEMN = row["OPEMN"].ToString();

                data.ANSDT = row["ANSDT"].ToString(); // 마취시작일
                data.ANSHR = row["ANSHR"].ToString(); // 마취시작시
                data.ANSMN = row["ANSMN"].ToString(); // 마취시작분
                data.ANEDT = row["ANEDT"].ToString(); // 마취종료일
                data.ANEHR = row["ANEHR"].ToString(); // 마취종료시
                data.ANEMN = row["ANEMN"].ToString(); // 마취종료분
                data.ANEDR = row["ANEDR"].ToString(); // 마취통증의학과 전문의
                data.ANEDRNM = row["DRNM"].ToString();
                data.USRID = row["USRID"].ToString(); // 작성자
                data.USRNM = CUtil.GetEmpnm(data.USRID, p_conn);
                data.ENTDT = row["ENTDT"].ToString(); // 작성일자
                data.ENTMS = row["ENTMS"].ToString(); // 작성시분초

                // 수술명(TU02)
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U02.OCD, U02.OPDT, A18.ONM, A02.ISPCD";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.OPNM.Add(row2["ONM"].ToString());
                    data.ISPCD.Add(row2["ISPCD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TT05
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql2 += System.Environment.NewLine + "  FROM TT05";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BDEDT='" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.DXD.Add(row2["DXD"].ToString());
                    data.DACD.Add(row2["DACD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR082_2
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT NCT_FRM_CD, ASA, ASA6, ASA7";
                sql2 += System.Environment.NewLine + "  FROM EMR082_2";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.NCT_FRM_CD = row2["NCT_FRM_CD"].ToString();
                    data.ASA = row2["ASA"].ToString();
                    data.ASA6 = row2["ASA6"].ToString();
                    data.ASA7 = row2["ASA7"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                // EMR320
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT MTH_CD, MTH_ETC, MIDD_MNTR_YN, MNTR_KND_CD, MNTR_ETC";
                sql2 += System.Environment.NewLine + "  FROM EMR320";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.MTH_CD = row2["MTH_CD"].ToString();
                    data.MTH_ETC = row2["MTH_ETC"].ToString();
                    data.MIDD_MNTR_YN = row2["MIDD_MNTR_YN"].ToString();
                    data.MNTR_KND_CD = row2["MNTR_KND_CD"].ToString();
                    data.MNTR_ETC = row2["MNTR_ETC"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                // EMR320_VS
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT MASR_DT, BPRSU, PULS, BRT, TMPR";
                sql2 += System.Environment.NewLine + "  FROM EMR320_VS";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.VTSG_MASR_DT.Add(row2["MASR_DT"].ToString());
                    data.BPRSU.Add(row2["BPRSU"].ToString());
                    data.PULS.Add(row2["PULS"].ToString());
                    data.BRT.Add(row2["BRT"].ToString());
                    data.TMPR.Add(row2["TMPR"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR320_MNTR
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT MASR_DT, OXY_STRT, CRBR_OXY_STRT, NRRT_CNDC_CD, NRRT_CNDC_RT, NRRT_CNDC_CNT, BIS_CNT, CROT_CNT, CVP_CNT, RMK_TXT";
                sql2 += System.Environment.NewLine + "  FROM EMR320_MNTR";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.MNTR_MASR_DT.Add(row2["MASR_DT"].ToString());
                    data.OXY_STRT.Add(row2["OXY_STRT"].ToString());
                    data.CRBR_OXY_STRT.Add(row2["CRBR_OXY_STRT"].ToString());
                    data.NRRT_CNDC_CD.Add(row2["NRRT_CNDC_CD"].ToString());
                    data.NRRT_CNDC_RT.Add(row2["NRRT_CNDC_RT"].ToString());
                    data.NRRT_CNDC_CNT.Add(row2["NRRT_CNDC_CNT"].ToString());
                    data.BIS_CNT.Add(row2["BIS_CNT"].ToString());
                    data.CROT_CNT.Add(row2["CROT_CNT"].ToString());
                    data.CVP_CNT.Add(row2["CVP_CNT"].ToString());
                    data.RMK_TXT.Add(row2["RMK_TXT"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR320_MDCT
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT KND_CD, MDCT_STDT, MDCT_STTM, MDCT_EDDT, MDCT_EDTM, MDS_NM, OQTY, UNIT";
                sql2 += System.Environment.NewLine + "  FROM EMR320_MDCT";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.KND_CD.Add(row2["KND_CD"].ToString());
                    data.MDCT_STDT.Add(row2["MDCT_STDT"].ToString());
                    data.MDCT_STTM.Add(row2["MDCT_STTM"].ToString());
                    data.MDCT_EDDT.Add(row2["MDCT_EDDT"].ToString());
                    data.MDCT_EDTM.Add(row2["MDCT_EDTM"].ToString());
                    data.MDS_NM.Add(row2["MDS_NM"].ToString());
                    data.OQTY.Add(row2["OQTY"].ToString());
                    data.UNIT.Add(row2["UNIT"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR320_IN
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT QTY, GUBUN, ONM";
                sql2 += System.Environment.NewLine + "  FROM EMR320_IN";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    long qty = MetroLib.StrHelper.ToLong( row2["QTY"].ToString());
                    string gubun = row2["GUBUN"].ToString(); // 1.수액 2.혈액
                    string onm = row2["ONM"].ToString();

                    data.IN_TOT_QTY += qty; // 섭취량 총량
                    if (gubun == "1") data.IN_IFSL_QTY += qty; // 섭취량 수액
                    if (gubun == "2")
                    {
                        data.BLTS_KND.Add(onm); // 수혈 종류
                        data.BLTS_QTY.Add(qty); // 수혈량
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR320_OUT
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT QTY, GUBUN";
                sql2 += System.Environment.NewLine + "  FROM EMR320_OUT";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    long qty = MetroLib.StrHelper.ToLong(row2["QTY"].ToString());
                    string gubun = row2["GUBUN"].ToString(); // 1.배뇨 2.출혈 3.기타

                    data.OUT_TOT_QTY += qty; // 섭취량 총량
                    if (gubun == "1") data.OUT_URNN_QTY += qty; // 배설량 배뇨
                    if (gubun == "2") data.OUT_BLD_QTY += qty; // 배설량 출혈
                    if (gubun == "3") data.OUT_ETC_QTY += qty; // 배설량 기타

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // EMR320_RCD
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT OCUR_DT, OCUR_TM, RCD_TXT";
                sql2 += System.Environment.NewLine + "  FROM EMR320_RCD";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.OCUR_DT.Add(row2["OCUR_DT"].ToString());
                    data.OCUR_TM.Add(row2["OCUR_TM"].ToString());
                    data.RCD_TXT.Add(row2["RCD_TXT"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });
                
                
                p_dsdata.RAA001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });
        }
    }
}

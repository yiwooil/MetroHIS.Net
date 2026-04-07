using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRID001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RID001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 입원처방
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT	V100.BEDEDT";	                /* 입원일자 */
            sql += System.Environment.NewLine + "     , V100.BEDEHM";                   /* 입원시간 */
            sql += System.Environment.NewLine + "     , V100.INDRID DRID_IN";           /* 입원의사ID */
            sql += System.Environment.NewLine + "     , A09IN.DPTCD DPTCD_IN";          /* 입원과 */
            sql += System.Environment.NewLine + "     , A09IN.PRIMDPTCD PDPTCD_IN";     /* 입원과(진료분야) */
            sql += System.Environment.NewLine + "     , A09IN.INSDPTCD INSDPTCD_IN";    /* 입원과(보험분류과) */
            sql += System.Environment.NewLine + "     , A07IN.GDRLCID GDRLCID_IN";      /* 입원의사면허번호 */
            sql += System.Environment.NewLine + "     , A07IN.DRNM DRNM_IN";            /* 입원의사성명 */
            sql += System.Environment.NewLine + "     , V100.BEDODT";                   /* 퇴원일자 */
            sql += System.Environment.NewLine + "     , V100.BEDOHM";                   /* 퇴원시간 */
            sql += System.Environment.NewLine + "     , V100.OUTDRID DRID_OUT";         /* 퇴원의사ID */
            sql += System.Environment.NewLine + "     , A09OUT.DPTCD DPTCD_OUT";        /* 퇴원과 */
            sql += System.Environment.NewLine + "     , A09OUT.PRIMDPTCD PDPTCD_OUT";   /* 퇴원과(진료분야) */
            sql += System.Environment.NewLine + "     , A09OUT.INSDPTCD INSDPTCD_OUT";  /* 퇴원과(보험분류과) */
            sql += System.Environment.NewLine + "     , A07OUT.GDRLCID GDRLCID_OUT";    /* 퇴원의사면허번호 */
            sql += System.Environment.NewLine + "     , A07OUT.DRNM DRNM_OUT";          /* 퇴원의사성명 */
            sql += System.Environment.NewLine + "     , V100.EMPID";                    /* 작성자ID */
            sql += System.Environment.NewLine + "     , V100.WDATE";                    /* 작성일자 */
            sql += System.Environment.NewLine + "     , V100.WTIME";                    /* 작성시간 */
            sql += System.Environment.NewLine + "     , V100.REBED";                    /* 30일이내 재입원 여부(1.유 0.무) */
            sql += System.Environment.NewLine + "     , V100.REBEDPLAN";                /* 재입원 계획 여부(1.유 0.무) */
            sql += System.Environment.NewLine + "     , V100.PREOUT";                   /* 직전퇴원일 아는지 여부(1.유 0.무) */
            sql += System.Environment.NewLine + "     , V100.COMPLAINT";                /* 주호소 */
            sql += System.Environment.NewLine + "     , V100.HOPI";                     /* 입원사유 및 현병력 */
            sql += System.Environment.NewLine + "     , V100.COT";                      /* 입원경과 및 치료과정 요약 */
            sql += System.Environment.NewLine + "     , V100.TRDPT";                    /* 전과여부 */
            sql += System.Environment.NewLine + "     , V100.ALLERGY";                  /* 약 알러지여부 */
            sql += System.Environment.NewLine + "     , V100.DRUGNAME";                 /* 의약품명칭 */
            sql += System.Environment.NewLine + "     , V100.HEPA";                     /* 감염 여부 */
            sql += System.Environment.NewLine + "     , V100.COMPLICATION";             /* 합병증 여부 */
            sql += System.Environment.NewLine + "     , V100.OUTREASON";                /* 퇴원 형태 */
            sql += System.Environment.NewLine + "     , V100.OUTSTATUS";                /* 퇴원시 환자상태(완쾌:0, 호전:1, 호전안됨:2, 치료없이 진단만:3, 기타:4 + CHAR(22) + 기타내용 */
            sql += System.Environment.NewLine + "     , V100.DEATHDATE";                /* 사망 일자 */
            sql += System.Environment.NewLine + "     , V100.DEATHTIME";                /* 사망 시간 */
            sql += System.Environment.NewLine + "     , V100.TRHOS";                    /* 전원 요구 */
            sql += System.Environment.NewLine + "     , V100.TRHOSREASON";              /* 전원 사유 */
            sql += System.Environment.NewLine + "     , V100.TRHOSREASON_TXT";              /* 전원 사유. 기타 */
            sql += System.Environment.NewLine + "     , V100.CONTICARE";                /* 연속적 치료 */
            sql += System.Environment.NewLine + "     , V100.TRHOSNUM";                 /* 전원보낸 기관기호 */
            sql += System.Environment.NewLine + "     , V100.TRHOSNAME";                /* 전원보낸 기관명 */
            sql += System.Environment.NewLine + "     , V100.TRHOSDPT";                 /* 전원보낸 진료과 */
            sql += System.Environment.NewLine + "     , V100.OUTCARE";                  /* 퇴원후 진료계획 */
            sql += System.Environment.NewLine + "     , A01.OTELNO";                    /* 회사 전화번호 */
            sql += System.Environment.NewLine + "     , A01.HTELNO";                    /* 집 전화번호 */
            sql += System.Environment.NewLine + "     , V100.NEWDACD";                  /* 조직적 진단코드 */
            sql += System.Environment.NewLine + "     , V100.NEWDXD";                   /* 조직적 진단명 */
            sql += System.Environment.NewLine + "     , V100.OUTFOS";                   /* 퇴원시 Functional outcom scale */
            sql += System.Environment.NewLine + "     , V100.SCORE";                    /* 퇴원시 Functional outcom scale.Scale */
            sql += System.Environment.NewLine + "     , V100.ASSPERSON";                /* 퇴원시 Functional outcom scale.평가자 */
                                                                                        /* 이하 002버전 추가 */
            sql += System.Environment.NewLine + "     , V100.VER";                      /* 버전 */
            sql += System.Environment.NewLine + "     , A09IN.INSDPTCD2 INSDPTCD2_IN";  /* 입원과(내과세부진료과목) */
            sql += System.Environment.NewLine + "     , A09OUT.INSDPTCD2 INSDPTCD2_OUT";/* 퇴원과(내과세부진료과목) */
            sql += System.Environment.NewLine + "     , V100.REBED_REASON";             /* 10일 이내 재입원사유 */
            sql += System.Environment.NewLine + "     , V100.PTNT_YN";                  /* 환자 안전관리 발생여부 */
            sql += System.Environment.NewLine + "     , V100.PTNT_TXT";                 /* 환자 안전관리 상세내용 */
            sql += System.Environment.NewLine + "  FROM TV100 V100 INNER JOIN TA07 A07IN ON A07IN.DRID=V100.INDRID";
            sql += System.Environment.NewLine + "                  INNER JOIN TA09 A09IN ON A09IN.DPTCD=A07IN.DPTCD";
            sql += System.Environment.NewLine + "                  INNER JOIN TA07 A07OUT ON A07OUT.DRID=V100.OUTDRID";
            sql += System.Environment.NewLine + "                  INNER JOIN TA09 A09OUT ON A09OUT.DPTCD=A07OUT.DPTCD";
            sql += System.Environment.NewLine + "                  INNER JOIN TA01 A01 ON A01.PID=V100.PID";
            sql += System.Environment.NewLine + " WHERE V100.PID =	'" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND V100.BEDEDT = '" + p_dsdata.FRDT + "'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRID001 data = new CDataRID001();
                data.Clear();

                data.BEDEDT = row["BEDEDT"].ToString();
                data.BEDEHM = row["BEDEHM"].ToString();

                data.DPTCD_IN = row["DPTCD_IN"].ToString();   // 입원진료과코드
                data.INSDPTCD_IN = row["INSDPTCD_IN"].ToString(); // 입원진료과코드(보험코드)
                data.INSDPTCD2_IN = row["INSDPTCD2_IN"].ToString(); // 입원내과세부진료과목(보험코드)
                data.DRID_IN = row["DRID_IN"].ToString();    // 입원의사ID
                data.DRNM_IN = row["DRNM_IN"].ToString();    // 입원의사명

                data.BEDODT = row["BEDODT"].ToString();
                data.BEDOHM = row["BEDOHM"].ToString();

                data.DPTCD_OUT = row["DPTCD_OUT"].ToString();   // 퇴원진료과코드
                data.INSDPTCD_OUT = row["INSDPTCD_OUT"].ToString(); // 퇴원진료과코드(보험코드)
                data.INSDPTCD2_OUT = row["INSDPTCD2_OUT"].ToString(); // 퇴원내과세부진료과목(보험코드)
                data.DRID_OUT = row["DRID_OUT"].ToString();    // 퇴원의사ID
                data.DRNM_OUT = row["DRNM_OUT"].ToString();    // 퇴원의사명

                data.EMPID = row["EMPID"].ToString();
                data.EMPNM = GetEmpnm(data.EMPID, p_conn);
                data.WDATE = row["WDATE"].ToString();
                data.WTIME = row["WTIME"].ToString();

                data.REBED = row["REBED"].ToString(); // 30일이내 재입원 여부(1.Yes 2.No 3.확인불가) 
                data.REBED_REASON = row["REBED_REASON"].ToString(); // 30일 이재 재입원사유
                data.REBEDPLAN = row["REBEDPLAN"].ToString(); // 재입원 계획 여부(1.계획에 있는 재입원 2.계획에 없는 재입원) 
                data.PREOUT = row["PREOUT"].ToString(); // 직전퇴원일

                data.HOPI = row["HOPI"].ToString(); // 입원사유 및 현병력
                data.COT = row["COT"].ToString(); // 입원경과 및 치료과정

                data.ALLERGY = row["ALLERGY"].ToString(); // 약물이상반응

                data.HEPA = row["HEPA"].ToString(); // 감염
                data.COMPLICATION = row["COMPLICATION"].ToString(); // 합병증
                data.PTNT_YN = row["PTNT_YN"].ToString(); // 환자 안전관리 발생여부
                data.PTNT_TXT = row["PTNT_TXT"].ToString(); // 환자 안전관리 상세내용

                data.OUTREASON = row["OUTREASON"].ToString(); // 퇴원 형태
                data.OUTSTATUS = row["OUTSTATUS"].ToString(); // 퇴원시 환자상태
                data.DEATHDATE = row["DEATHDATE"].ToString(); // 사망일자
                data.DEATHTIME = row["DEATHTIME"].ToString(); // 사망시간
                data.TRHOSREASON = row["TRHOSREASON"].ToString(); // 전원사유
                data.TRHOSREASON_TXT = row["TRHOSREASON_TXT"].ToString(); // 전원사유.기타
                data.OUTCARE = row["OUTCARE"].ToString(); // 퇴원후진료계획


                // 주호소
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT COMPLAINT";	/*주호소*/
                sql2 += System.Environment.NewLine + "     , ERA"; /*발병시기*/
                sql2 += System.Environment.NewLine + "  FROM TV100_CC";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.COMPLAINT.Add(row2["COMPLAINT"].ToString());
                    data.ERA.Add(row2["ERA"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 처치및수술
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT OPDT";		/*수술일시*/
                sql2 += System.Environment.NewLine + "     , OPNAME";	/*처치 및 수술명*/
                sql2 += System.Environment.NewLine + "     , ICD9CM";	/*ICD9CM VOL.3*/
                sql2 += System.Environment.NewLine + "     , PRICD";	    /*수가코드*/
                sql2 += System.Environment.NewLine + "  FROM TV100_OP";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.OPDT.Add(row2["OPDT"].ToString()); // 수술일시
                    data.OPNAME.Add(row2["OPNAME"].ToString()); // 처치 및 수술명
                    data.ICD9CM.Add(row2["ICD9CM"].ToString()); // ICD9CM VOL.3
                    data.PRICD.Add(row2["PRICD"].ToString()); // 수가코드

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 검사소견
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT ODT";	/*처방일*/
                sql2 += System.Environment.NewLine + "     , GUMDT"; /*검사일시*/
                sql2 += System.Environment.NewLine + "     , RSDT";  /*결과일시*/
                sql2 += System.Environment.NewLine + "     , ONM";   /*검사명*/
                sql2 += System.Environment.NewLine + "     , GUMRESULT"; /*검사결과*/
                sql2 += System.Environment.NewLine + "  FROM TV100_GUM";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.GUMDT.Add(row2["GUMDT"].ToString()); // 검사일시
                    data.RSDT.Add(row2["RSDT"].ToString()); // 결과일시
                    data.GUMNM.Add(row2["ONM"].ToString()); // 검사명
                    data.GUMRESULT.Add(row2["GUMRESULT"].ToString()); // 검사결과

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 최종 진단
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT DX.ROFG";		/*확진여부*/
                sql2 += System.Environment.NewLine + "     , DX.DISECD";	/*상병분류기호*/
                sql2 += System.Environment.NewLine + "     , DX.DXD";	    /*진단명*/
                sql2 += System.Environment.NewLine + "     , DX.DPTCD";	    /*진료과*/
                sql2 += System.Environment.NewLine + "     , DX.POA";	    /*입원시 상병여부*/
                sql2 += System.Environment.NewLine + "     , A09.INSDPTCD";
                sql2 += System.Environment.NewLine + "     , A09.INSDPTCD2"; /* 내과 세부진료과목 */
                sql2 += System.Environment.NewLine + "  FROM TV100_DX DX (NOLOCK) INNER JOIN TA09 A09 (NOLOCK) ON A09.DPTCD=DX.DPTCD";
                sql2 += System.Environment.NewLine + " WHERE DX.PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND DX.BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ROFG.Add(row2["ROFG"].ToString());	// 확진여부
                    data.DISECD.Add(row2["DISECD"].ToString()); // 상병분류기호
                    data.DXD.Add(row2["DXD"].ToString()); // 진단명
                    data.DPTCD.Add(row2["DPTCD"].ToString()); // 진료과
                    data.POA.Add(row2["POA"].ToString()); // 입원시 상병여부
                    data.INSDPTCD.Add(row2["INSDPTCD"].ToString());
                    data.INSDPTCD2.Add(row2["INSDPTCD2"].ToString()); // 내과 세부진료과목

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 전과
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT TR.TRDATE";	  /*전과일시*/
                sql2 += System.Environment.NewLine + "     , TR.TROUTDPT";	  /*의뢰과*/
                sql2 += System.Environment.NewLine + "     , TR.TROUTDRNM";	  /*의뢰의사 성명*/
                sql2 += System.Environment.NewLine + "     , TR.TROUTDR";	  /*의뢰의사 면허종류*/
                sql2 += System.Environment.NewLine + "     , TR.TROUTDRLCID"; /*의뢰의사 면허번호*/
                sql2 += System.Environment.NewLine + "     , TR.TROUTREASON"; /*전과사유*/
                sql2 += System.Environment.NewLine + "     , TR.TRINDPT";	  /*회신과*/
                sql2 += System.Environment.NewLine + "     , TR.TRINDRNM";	  /*회신의사 성명*/
                sql2 += System.Environment.NewLine + "     , TR.TRINDR";	  /*회신의사 면허종류*/
                sql2 += System.Environment.NewLine + "     , TR.TRINDRLCID";  /*회신의사 면허번호*/
                sql2 += System.Environment.NewLine + "     , A09_OUT.INSDPTCD AS TROUTINSDPTCD";
                sql2 += System.Environment.NewLine + "     , A09_OUT.INSDPTCD2 AS TROUTINSDPTCD2";
                sql2 += System.Environment.NewLine + "     , A09_IN.INSDPTCD AS TRININSDPTCD";
                sql2 += System.Environment.NewLine + "     , A09_IN.INSDPTCD2 AS TRININSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TV100_TR TR LEFT JOIN TA09 A09_OUT ON A09_OUT.DPTCD=TR.TROUTDPT";
                sql2 += System.Environment.NewLine + "                   LEFT JOIN TA09 A09_IN ON A09_IN.DPTCD=TR.TRINDPT";
                sql2 += System.Environment.NewLine + " WHERE TR.PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND TR.BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY TR.PID,TR.BEDEDT,TR.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.TR_DATE.Add(row2["TRDATE"].ToString()); // 전과일시
                    data.TR_OUT_DPT.Add(row2["TROUTDPT"].ToString());	// 의뢰과
                    data.TR_OUT_INSDPTCD.Add(row2["TROUTINSDPTCD"].ToString());
                    data.TR_OUT_INSDPTCD2.Add(row2["TROUTINSDPTCD2"].ToString());
                    data.TR_OUT_DRNM.Add(row2["TROUTDRNM"].ToString()); // 의뢰의사 성명
                    data.TR_OUT_DR.Add(row2["TROUTDRNM"].ToString()); // 의뢰의사 면허종류
                    data.TR_OUT_DRLCID.Add(row2["TROUTDRLCID"].ToString()); // 의뢰의사 면허번호
                    data.TR_OUT_REASON.Add(row2["TROUTREASON"].ToString()); // 전과사유
                    data.TR_IN_DPT.Add(row2["TRINDPT"].ToString()); // 회신과
                    data.TR_IN_INSDPTCD.Add(row2["TRININSDPTCD"].ToString());
                    data.TR_IN_INSDPTCD2.Add(row2["TRININSDPTCD2"].ToString());
                    data.TR_IN_DRNM.Add(row2["TRINDRNM"].ToString()); // 회신의사 성명
                    data.TR_IN_DR.Add(row2["TRINDR"].ToString()); // 회신의사 면허종류
                    data.TR_IN_DRLCID.Add(row2["TRINDRLCID"].ToString()); // 회신의사 면허번호

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 환자 상태 척도
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT ERA_CD";/*측정시기*/
                sql2 += System.Environment.NewLine + "     , ERA_ETC_TXT"; /*측정시기 기타 상세*/
                sql2 += System.Environment.NewLine + "     , TL_NM"; /*도구*/
                sql2 += System.Environment.NewLine + "     , RST_TXT"; /*결과*/
                sql2 += System.Environment.NewLine + "     , RMK_TXT"; /*참고사항*/
                sql2 += System.Environment.NewLine + "  FROM TV100_MASR";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ERA_CD.Add(row2["ERA_CD"].ToString()); // 측정시기
                    data.ERA_ETC_TXT.Add(row2["ERA_ETC_TXT"].ToString()); // 측정시기 기타 상세
                    data.TL_NM.Add(row2["TL_NM"].ToString()); // 도구
                    data.RST_TXT.Add(row2["RST_TXT"].ToString()); // 결과
                    data.RMK_TXT.Add(row2["RMK_TXT"].ToString()); // 참고사항

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // 퇴원처방
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT ONM";	/*약품명*/
                sql2 += System.Environment.NewLine + "     , OUNIT"; /*용법*/
                sql2 += System.Environment.NewLine + "     , OQTY";  /*1회투약량*/
                sql2 += System.Environment.NewLine + "     , ORDCNT"; /*1일투여횟수*/
                sql2 += System.Environment.NewLine + "     , ODAYCNT"; /*총 투약일수*/
                sql2 += System.Environment.NewLine + "     , ORDER_TYPE"; /*처방구분*/
                sql2 += System.Environment.NewLine + "  FROM TV100_DCOR";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(ONM,'')<>''"; // 처방명이 있는 경우만
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ORDER_TYPE.Add(row2["ORDER_TYPE"].ToString()); // 처방구분
                    data.ONM.Add(row2["ONM"].ToString()); // 약품명
                    data.OUNIT.Add(row2["OUNIT"].ToString()); // 용법
                    data.OQTY.Add(row2["OQTY"].ToString()); // 1회 투약량
                    data.ORDCNT.Add(row2["ORDCNT"].ToString()); // 1일 투여횟수
                    data.ODAYCNT.Add(row2["ODAYCNT"].ToString()); // 총 투약일수

                    return MetroLib.SqlHelper.CONTINUE;
                });


                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT DEATHDISECD";	/*상병분류기호*/
                sql2 += System.Environment.NewLine + "     , DEATHDXD";	    /*진단명*/
                sql2 += System.Environment.NewLine + "  FROM TV100_DEDX";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.DEDX_DISECD = row2["DEATHDISECD"].ToString();
                    data.DEDX_DXD = row2["DEATHDXD"].ToString();

                    return false;

                });


                p_dsdata.RID001_LIST.Add(data);

                return true;
            });


        }

        protected string GetEmpnm(string p_empid, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            if (p_empid.StartsWith("AA"))
            {
                sql = "SELECT DRNM EMPNM FROM TA07 WHERE DRID='" + p_empid + "'";
            }
            else
            {
                sql = "SELECT EMPNM FROM TA13 WHERE EMPID='" + p_empid + "'";
            }
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                ret = row["EMPNM"].ToString();
                return false;
            });
            return ret;
        }
    }
}

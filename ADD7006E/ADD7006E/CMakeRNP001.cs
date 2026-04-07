using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7006E
{
    class CMakeRNP001 : CMake001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RNP001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 입원마스터
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A04.PID,A04.BEDEDT,A04.BEDEHM,A04.DPTCD";
            sql += System.Environment.NewLine + "     , A09.DPTNM,A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TA04 A04 LEFT JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + " ORDER BY A04.BEDEDT,A04.BEDEHM";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRNP001 data = new CDataRNP001();
                data.Clear();

                // TA04
                data.BEDEDT = row["BEDEDT"].ToString();
                data.BEDEHM = row["BEDEHM"].ToString();
                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();

                // TV95_10
                string sql2 = "";
                sql2 = "";
                sql2 += Environment.NewLine + "SELECT EMPID,WDATE,WTIME,GUBUN,InCondiQ1,Society1_ADOBJ,Society2_Q1,Society2_INHIS,Society2_Q3,Society2_Q3_ETC,Society2_Q3_TXT";
                sql2 += Environment.NewLine + "     , FAQ1,FAQ2,FAQ3,FAQ4,FAQ1_ETC,FAQ2_ETC,FAQ3_ETC,FAQ4_ETC,HabitQ3,HabitQ3_ETC,HabitQ4,HabitQ4_ETC,InCondiTPR,NewBornQ13,NewBornQ13_ETC";
                sql2 += Environment.NewLine + "     , AD_PATH_TXT,MDCT_STAT_TXT,MEDEXM_TXT,BIRTH_DT,FTUS_DEV_WEEK,FTUS_DEV_DAY,APSC_PNT1,APSC_PNT2";
                sql2 += Environment.NewLine + "  FROM TV95_10";
                sql2 += Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                sql2 += Environment.NewLine + "   AND BEDEDT='" + row["BEDEDT"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.TV95_VER = "TV95_10";

                    data.V95_10_EMPID = row2["EMPID"].ToString();
                    data.V95_10_EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn);
                    data.V95_10_WDATE = row2["WDATE"].ToString();
                    data.V95_10_WTIME = row2["WTIME"].ToString();
                    data.V95_10_InCondiQ1 = row2["InCondiQ1"].ToString(); // 입원경로
                    data.V95_10_AD_PATH_TXT = row2["AD_PATH_TXT"].ToString(); // 입원경로가 기타인 경우 상세내용
                    data.V95_10_GUBUN = row2["GUBUN"].ToString(); // 환자구분(1.일반 2.신생아(28일이내))
                                                                  // 정보제공자성명
                    data.V95_10_Society1_ADOBJ = row2["Society1_ADOBJ"].ToString(); // 입원동기
                    data.V95_10_Society2_Q1 = row2["Society2_Q1"].ToString(); // 과거력
                    data.V95_10_Society2_INHIS = row2["Society2_INHIS"].ToString(); // 수술력
                    data.V95_10_MDCT_STAT_TXT = row2["MDCT_STAT_TXT"].ToString(); // 최근 투약 상태
                    data.V95_10_Society2_Q3 = row2["Society2_Q3"].ToString(); // 알레르기 여부
                    data.V95_10_Society2_Q3_ETC = row2["Society2_Q3_ETC"].ToString(); // 알레르기 내용
                    data.V95_10_Society2_Q3_TXT = row2["Society2_Q3_TXT"].ToString(); // 알레르기 기타 내용
                    data.V95_10_FAQ1 = row2["FAQ1"].ToString(); // 가족력(부)
                    data.V95_10_FAQ2 = row2["FAQ2"].ToString(); // 가족력(모)
                    data.V95_10_FAQ3 = row2["FAQ3"].ToString(); // 가족력(형제자매)
                    data.V95_10_FAQ4 = row2["FAQ4"].ToString(); // 가족력(조부모기타
                    data.V95_10_FAQ1_ETC = row2["FAQ1_ETC"].ToString(); // 가족력기타내용(부)
                    data.V95_10_FAQ2_ETC = row2["FAQ2_ETC"].ToString(); // 가족력기타내용(모)
                    data.V95_10_FAQ3_ETC = row2["FAQ3_ETC"].ToString(); // 가족력기타내용(형제자매)
                    data.V95_10_FAQ4_ETC = row2["FAQ4_ETC"].ToString(); // 가족력가타내용(조부모기타)
                    data.V95_10_HabitQ3 = row2["HabitQ3"].ToString(); // 음주여부
                    data.V95_10_HabitQ3_ETC = row2["HabitQ3_ETC"].ToString(); // 음주내용
                    data.V95_10_HabitQ4 = row2["HabitQ4"].ToString(); // 흡연여부
                    data.V95_10_HabitQ4_ETC = row2["HabitQ4_ETC"].ToString(); // 흡연내용
                    data.V95_10_InCondiTPR = row2["InCondiTPR"].ToString(); // 입원시 체증,신장 등
                    data.V95_10_MEDEXM_TXT = row2["MEDEXM_TXT"].ToString(); // 신체검진
                    data.V95_10_BIRTH_DT = row2["BIRTH_DT"].ToString(); // 신생아 출생일시
                    data.V95_10_FTUS_DEV_WEEK = row2["FTUS_DEV_WEEK"].ToString(); // 신생아 재태기간 주
                    data.V95_10_FTUS_DEV_DAY = row2["FTUS_DEV_DAY"].ToString(); // 신생아 재태기간 일
                    data.V95_10_NewBornQ13 = row2["NewBornQ13"].ToString(); // 분만형태
                    data.V95_10_NewBornQ13_ETC = row2["NewBornQ13_ETC"].ToString(); // 분만형태기타
                    data.V95_10_APSC_PNT1 = row2["APSC_PNT1"].ToString(); // 신생아 Apgar Score 1분 점수
                    data.V95_10_APSC_PNT2 = row2["APSC_PNT2"].ToString(); // 신생아 Apgar Score 5분 점수

                    p_dsdata.RNP001_LIST.Add(data); // ** TV95가 있는 경우에만 ...

                    return MetroLib.SqlHelper.BREAK;
                });


                // TV95_10에 자료가 있으면 TV95를 읽지 않는다.
                if (data.TV95_VER == "")
                {
                    // TV95
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT EMPID,WDATE,WTIME,AD_PATH,SVC_NM,AD_OBJ,OLD_ILL,AD_OP_HIS,MED_ST,C_FOOD,C_ANTI,ANTI,FHX,FH_DREAM_CHK,FH_DREAM_ETC,ALGOL,SMOKING,HEIGHT,AD_AD_TPR,SO_DESC";
                    sql2 += System.Environment.NewLine + "     , AD_PATH_TXT,PTNT_TP_CD,BIRTH_DT,FTUS_DEV_WEEK,FTUS_DEV_DAY,PARTU_FRM_TXT,APSC_PNT1,APSC_PNT2";
                    sql2 += System.Environment.NewLine + "  FROM TV95";
                    sql2 += System.Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND BEDEDT='" + row["BEDEDT"].ToString() + "'";

                    MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                    {
                        data.EMPID = row2["EMPID"].ToString();
                        data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn);
                        data.WDATE = row2["WDATE"].ToString();
                        data.WTIME = row2["WTIME"].ToString();
                        data.AD_PATH = row2["AD_PATH"].ToString(); // 입원경로
                        data.AD_PATH_TXT = row2["AD_PATH_TXT"].ToString(); // 입원경로가 기타인 경우 상세내용
                        data.PTNT_TP_CD = row2["PTNT_TP_CD"].ToString(); // 환자구분(1.일반 2.신생아(28일이내))
                        data.SVC_NM = row2["SVC_NM"].ToString(); // 정보제공자성명
                        data.AD_OBJ = row2["AD_OBJ"].ToString(); // 입원동기
                        data.OLD_ILL = row2["OLD_ILL"].ToString(); // 과거력
                        data.AD_OP_HIS = row2["AD_OP_HIS"].ToString(); // 수술력
                        data.MED_ST = row2["MED_ST"].ToString(); // 약품 알레르기 여부
                        data.C_FOOD = row2["C_FOOD"].ToString(); // 식품 알레르기 여부
                        data.C_ANTI = row2["C_ANTI"].ToString(); // 항생제 알레르기 여부
                        data.ANTI = row2["ANTI"].ToString(); // 항생제 알레르기 내용
                        data.FHX = row2["FHX"].ToString(); // 가족력
                        data.FH_DREAM_CHK = row2["FH_DREAM_CHK"].ToString(); // 가족력기타
                        data.FH_DREAM_ETC = row2["FH_DREAM_ETC"].ToString(); // 가족력기타내용
                        data.ALGOL = row2["ALGOL"].ToString(); // 음주
                        data.SMOKING = row2["SMOKING"].ToString(); // 흡연
                        data.HEIGHT = row2["HEIGHT"].ToString(); // 키
                        data.AD_AD_TPR = row2["AD_AD_TPR"].ToString(); // 입원시 체중등
                        data.SO_DESC = row2["SO_DESC"].ToString(); // 신체검진
                        data.BIRTH_DT = row2["BIRTH_DT"].ToString(); // 신생아 출생일시
                        data.FTUS_DEV_WEEK = row2["FTUS_DEV_WEEK"].ToString(); // 신생아 재태기간 주
                        data.FTUS_DEV_DAY = row2["FTUS_DEV_DAY"].ToString(); // 신생아 재태기간 일
                        data.PARTU_FRM_TXT = row2["PARTU_FRM_TXT"].ToString(); // 신생아 분만형태(평문)
                        data.APSC_PNT1 = row2["APSC_PNT1"].ToString(); // 신생아 Apgar Score 1분 점수
                        data.APSC_PNT2 = row2["APSC_PNT2"].ToString(); // 신생아 Apgar Score 5분 점수

                        p_dsdata.RNP001_LIST.Add(data); // ** TV95가 있는 경우에만 ...

                        return MetroLib.SqlHelper.BREAK;
                    });
                }

                return MetroLib.SqlHelper.BREAK;
            });
        }

    }
}

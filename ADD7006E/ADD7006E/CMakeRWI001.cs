using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRWI001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RWI001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;


            string fst_ipat_dt = "";

            // EMR328
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A.CHRG_DR_NM,A.DPTCD,A.WRTP_NM,A.SPRM_IPAT_DT,A.SPRM_IPAT_PTH_CD,A.IPAT_PTH_ETC_TXT,A.SPRM_IPAT_RS_CD,RE_IPAT_RS_TXT,IPAT_RS_ETC_TXT";
            sql += System.Environment.NewLine + "     , A.SPRM_DSCG_RST_CD,A.DSCG_RST_TXT,A.DEATH_DT,A.DEATH_SICK_SYM,A.DEATH_DIAG_NM,A.SPRM_DSCG_DT";
            sql += System.Environment.NewLine + "     , A.ATFL_RPRT_ENFC_YN,A.OXY_CURE_YN,A.CNNL_ENFC_YN,A.DRN_ENFC_YN,A.SPCL_TRET_CD,A.SPCL_TRET_ETC_TXT,A.SGRD_PNT_YN,A.SGRD_RVSN_TL_CD,A.SGRD_RVSN_TL_TXT";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM EMR328 A INNER JOIN TA09 A09 ON A09.DPTCD=A.DPTCD";
            sql += System.Environment.NewLine + " WHERE A.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + " ORDER BY A.SPRM_IPAT_DT";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRWI001 data = new CDataRWI001();
                data.Clear();

                if (fst_ipat_dt == "") fst_ipat_dt = row["SPRM_IPAT_DT"].ToString();

                // EMR328

                // A.기본 정보
                data.FST_IPAT_DT = fst_ipat_dt; // 최초 입실일시

                // TT05
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql2 += System.Environment.NewLine + "  FROM TT05";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BDEDT='" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.DXD.Add(row2["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });


                // B.중환자실 입.퇴실 정보
                data.CHRG_DR_NM = row["CHRG_DR_NM"].ToString(); // 담당의사 성명
                data.DPTCD = row["DPTCD"].ToString(); // 진료과코드
                data.INSDPTCD = row["INSDPTCD"].ToString(); // 진료과목
                data.INSDPTCD2 = row["INSDPTCD2"].ToString(); // 내과 세부전문과목
                data.WRTP_NM = row["WRTP_NM"].ToString(); // 작성자 성명
                data.SPRM_IPAT_DT = row["SPRM_IPAT_DT"].ToString(); // 입실일시
                data.SPRM_IPAT_PTH_CD = row["SPRM_IPAT_PTH_CD"].ToString(); // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실 9.기타)
                data.IPAT_PTH_ETC_TXT = row["IPAT_PTH_ETC_TXT"].ToString(); // 입실경로 기타 상세
                data.SPRM_IPAT_RS_CD = row["SPRM_IPAT_RS_CD"].ToString(); // 입실사유(1.상태 악화되어 집중관찰 2.특수한 치료 또는 관리가 필요한 이유 3.수술이나 시술 후 집중관찰 5.의료진의 치료 계획에 따라 예정되 재입실 9.기타)
                data.RE_IPAT_RS_TXT = row["RE_IPAT_RS_TXT"].ToString(); // 입실사유 재입실사유
                data.IPAT_RS_ETC_TXT = row["IPAT_RS_ETC_TXT"].ToString(); // 입실사유 기타상세
                data.SPRM_DSCG_RST_CD = row["SPRM_DSCG_RST_CD"].ToString(); // 퇴실상태(01.퇴원 02.전실(병동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.퇴사판정(이식) 08.계속 입원 99.기타)
                data.DSCG_RST_TXT = row["DSCG_RST_TXT"].ToString(); // 퇴실현환 기타 상세
                data.DEATH_DT = row["DEATH_DT"].ToString(); // 사망 일시
                data.DEATH_SICK_SYM = row["DEATH_SICK_SYM"].ToString(); // 원사인 상병분류기호
                data.DEATH_DIAG_NM = row["DEATH_DIAG_NM"].ToString(); // 사망 진단명
                data.SPRM_DSCG_DT = row["SPRM_DSCG_DT"].ToString(); // 퇴실일시

                // C.중환자실 관찰 기록

                // D.기타정보
                data.ATFL_RPRT_ENFC_YN = row["ATFL_RPRT_ENFC_YN"].ToString(); // 인공호흡기 적용 여부
                data.OXY_CURE_YN = row["OXY_CURE_YN"].ToString(); // 산소요법 적용 여부
                data.CNNL_ENFC_YN = row["CNNL_ENFC_YN"].ToString(); // 삽입관 적용 여부
                data.DRN_ENFC_YN = row["DRN_ENFC_YN"].ToString(); // 배액관 적용 여부
                data.SPCL_TRET_CD = row["SPCL_TRET_CD"].ToString(); // 특수 처치(00.해당 없음 01.ECMO 02.신대체요법 99.기타)
                data.SPCL_TRET_ETC_TXT = row["SPCL_TRET_ETC_TXT"].ToString(); // 특수 처치 기타 상세
                data.MNTR_KND_CD = row["MNTR_KND_CD"].ToString(); // 모니터링 종류(00.해당없음 01.ABP Monitor 02.EKG Monitor 03.O2 Saturation Monitor 04.Continuous EEG 99.기타)
                data.SGRD_PNT_YN = row["SGRD_PNT_YN"].ToString(); // 중증도 점수 여부(1.Yes 2.No)
                data.SGRD_RVSN_TL_CD = row["SGRD_RVSN_TL_CD"].ToString(); // 중증도 종류(1.APACH II 2.PAPCHEIII 3.SAPS 2 3.SAPS 3 9.기타)
                data.SGRD_RVSN_TL_TXT = row["SGRD_RVSN_TL_TXT"].ToString(); // 증증도 보정도구 종류상세

                p_dsdata.RWI001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });



        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRWN001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RWN001_LIST.Clear();

            // 신생아중환자실기록자료
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT PID, BEDEDT, WDATE, SEQ, BIRTH_YN, BIRTH_DT, BIRTH_PLC_CD, BIRTH_PLC_ETC_TXT, PARTU_FRM_CD, PARTU_FRM_ETC_TXT, FTUS_DEV_TRM, MEMB_YN, MEMB_TXT";
            sql += System.Environment.NewLine + "     , APSC_YN, APSC_PNT, NBY_BIRTH_BWGT, FST_IPAT_DT, CHRG_DR_NM, WRTP_NM, SPRM_IPAT_DT, SPRM_IPAT_PTH_CD, IPAT_PTH_ETC_TXT";
            sql += System.Environment.NewLine + "     , NBY_IPAT_RS_CD, RE_IPAT_RS_TXT, IPAT_RS_ETC_TXT, IPAT_NBY_BWGT, SPRM_DSCG_RST_CD, DSCG_RST_ETC_TXT, DEATH_DT, DEATH_SICK_SYM, DEATH_DIAG_NM";
            sql += System.Environment.NewLine + "     , SPRM_DSCG_DT, ATFL_RPRT_YN, OXY_CURE_YN, CNNL_YN, OSTM_YN, OSTM_ETC_TXT, ETC_DSPL_CD";
            sql += System.Environment.NewLine + "  FROM EMR330 ";
            sql += System.Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + " ORDER BY WDATE, SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRWN001 data = new CDataRWN001();
                data.Clear();

                data.BIRTH_YN = row["BIRTH_YN"].ToString(); // 출생일 확인 여부(1.Yes 2.No)
                data.BIRTH_DT = row["BIRTH_DT"].ToString(); // 출생일시(ccyymmddhhmm)
                data.BIRTH_PLC_CD = row["BIRTH_PLC_CD"].ToString(); // 출생장소(1.본원 2.타기관 9.기타)
                data.BIRTH_PLC_ETC_TXT = row["BIRTH_PLC_ETC_TXT"].ToString(); //출생장소 기타상세(출생장소가 9 기타일 경우)
                data.PARTU_FRM_CD = row["PARTU_FRM_CD"].ToString(); // 분만형태(1.자연분문 2.제왕절개 9.기타)
                data.PARTU_FRM_ETC_TXT = row["DEPTCD"].ToString(); // 분만형태 기타상세(분만형태가 9 기타일 경우)
                data.FTUS_DEV_TRM = row["FTUS_DEV_TRM"].ToString(); // 재태기간(  주  일)형태로
                data.MEMB_YN = row["MEMB_YN"].ToString(); // 다태아여부(1.Yes 2.No)
                data.MEMB_TXT = row["MEMB_TXT"].ToString(); // 다태아내용(다태아여부 1 Yes 일경우)(예시 세 쌍둥이 중 첫째 아기인 경우 3/1)
                data.APSC_YN = row["APSC_YN"].ToString(); // Apgar Score(1.Yes 2.No)
                data.APSC_PNT = row["APSC_PNT"].ToString(); // Apgar Score 내용 (Apgar Score 1 Yes 일경우)(예시 1분 2점, 5분 8점인 경우:2/8)
                data.NBY_BIRTH_BWGT = row["NBY_BIRTH_BWGT"].ToString(); // 출생시체중
                data.FST_IPAT_DT = row["FST_IPAT_DT"].ToString(); // 최초입실일시(ccyymmddhhmm)
                data.CHRG_DR_NM = row["CHRG_DR_NM"].ToString(); // 담당의 성명
                data.WRTP_NM = row["WRTP_NM"].ToString(); // 작성자성명
                data.SPRM_IPAT_DT = row["SPRM_IPAT_DT"].ToString(); // 입실일시(ccyymmddhhmm)
                data.SPRM_IPAT_PTH_CD = row["SPRM_IPAT_PTH_CD"].ToString(); // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타)
                data.IPAT_PTH_ETC_TXT = row["IPAT_PTH_ETC_TXT"].ToString(); // 입실경로 기타상세(입실경로 9 기타일경우 입실경로 평문기재)
                data.NBY_IPAT_RS_CD = row["NBY_IPAT_RS_CD"].ToString(); // 입실사유(1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타)
                data.RE_IPAT_RS_TXT = row["RE_IPAT_RS_TXT"].ToString(); // 입실사유 재입실상세(입실사유 5 일경우 평문)
                data.IPAT_RS_ETC_TXT = row["IPAT_RS_ETC_TXT"].ToString(); // 입실사유 기타상세(입실사유 9 기타일경우 평문)
                data.IPAT_NBY_BWGT = row["IPAT_NBY_BWGT"].ToString(); // 입실시 체중
                data.SPRM_DSCG_RST_CD = row["SPRM_DSCG_RST_CD"].ToString(); // 퇴실상태(01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타)
                data.DSCG_RST_ETC_TXT = row["DSCG_RST_ETC_TXT"].ToString(); //퇴실상태 기타상세(퇴실상태 99 기타일경우 평문)
                data.DEATH_DT = row["DEATH_DT"].ToString(); // 사망일시(ccyymmddhhmm)(퇴실상태가 06.사망인 경우)
                data.DEATH_SICK_SYM = row["DEPTCD"].ToString(); // 원사인 상병분류기호(퇴실상태가 06.사망인 경우)
                data.DEATH_DIAG_NM = row["DEPTCD"].ToString(); // 사망진단명(퇴실상태가 06.사망인 경우)
                data.SPRM_DSCG_DT = row["DEPTCD"].ToString(); //퇴실일시(ccyymmddhhmm)
                data.ATFL_RPRT_YN = row["DEPTCD"].ToString(); // 인공호흡기 적용 여부(1.Yes 2.No)
                data.OXY_CURE_YN = row["DEPTCD"].ToString(); // 산소요법 적용 여부(1.Yes 2.No)
                data.CNNL_YN = row["DEPTCD"].ToString(); // 삽입관 및 배액관 적용여부(1.Yes 2.No)

                data.OSTM_YN = row["DEPTCD"].ToString(); // 장루유무(1.Yes 2.No 9.기타)
                data.OSTM_ETC_TXT = row["DEPTCD"].ToString(); // 장루유무-기타상세(장루유무가 9기타인경우 평문)
                data.ETC_DSPL_CD = row["DEPTCD"].ToString(); // 기타처치 시행여부(00.해당없음 01.광선요법 02.저체온요법 03.하기도 증기흡입요법 04.교환수혈 05.심폐소생술 99.기타)


                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT CNNL_KND_CD, CNNL_KND_ETC_TXT, CNNL_INS_DT, CNNL_DEL_DT";
                sql2 += System.Environment.NewLine + "  FROM EMR330_CNNL";
                sql2 += System.Environment.NewLine + " WHERE PID = '" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND WDATE = '" + row["WDATE"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ = '" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY A.SEQNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.CNNL_KND_CD.Add(row2["CNNL_KND_CD"].ToString()); // 삽입관 및 배액관 종류(01.Umbilical venous catheter 02.Umbilical arterial catheter 03.Peripherally inserted central catheter 04.Arterial catheter 05.Central venous catheter 06.Tracheostomy 07.Endotracheal tube 99.기타)
                    data.CNNL_KND_ETC_TXT.Add(row2["CNNL_KND_ETC_TXT"].ToString()); // 삽입관 및 배액관유형-기타상세(99기타인경우 평문)
                    data.CNNL_INS_DT.Add(row2["CNNL_INS_DT"].ToString()); // 삽입일시(ccyymmddhhmm)
                    data.CNNL_DEL_DT.Add(row2["CNNL_DEL_DT"].ToString()); // 제거일시(ccyymmddhhmm)


                    return MetroLib.SqlHelper.CONTINUE;
                });

                return MetroLib.SqlHelper.CONTINUE;
            });
        }
    }
}

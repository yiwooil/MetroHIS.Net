using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------
    // 2026.01.22 WOOIL - 이 클래스 사용 안 함. *******************************************************************************************************************
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------
    class CMakeASM035
    {
        // 마취
        public void MakeASM035(CDataASM035_003 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
        {
            int count = data.Read_ASM000(p_conn, p_tran, re_query);

            if (count > 0 && (data.UPDDT != "" || data.STATUS!=""))
            {
                // 자료를 사용자가 저장했거나, 전송한 이력이 있으면 자료를 다시 읽지 않고 만들어진 자료를 조회한다.
                data.ReadDataFromSaved(p_conn, p_tran);
            }
            else
            {
                // EMR 에서 자료를 읽는다.
                SetData(data, p_sysdt, p_systm, p_user, p_conn, p_tran);

                // TI84_ASM000 저장
                data.Into_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

                // 자료저장
                data.InsData(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

            }
        }

        private void SetData(CDataASM035_003 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            data.ClearMe();

            // --- A. 기본 정보 ---
            data.IPAT_DD = data.BDEDT;  // 입원일자(YYYYMMDD)
            data.DSCG_YN = "1";         // 퇴원여부(1.Yes 2.No)
            data.DSCG_DD = data.BDODT;  // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U01.PID,U01.OPDT,U01.DPTCD,U01.OPSEQ,U01.SEQ";
            sql += System.Environment.NewLine + "     , U01.OPSDT,U01.OPSHR,U01.OPSMN,U01.OPEDT,U01.OPEHR,U01.OPEMN";
            sql += System.Environment.NewLine + "     , U03.ANSDT,U03.ANSHR,U03.ANSMN,U03.ANEDT,U03.ANEHR,U03.ANEMN,U03.ANEDR,U03.USRID,U03.ENTDT,U03.ENTMS,U03.ANENO";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "  FROM TU01 U01 INNER JOIN TU03 U03 ON U03.PID=U01.PID AND U03.OPDT=U01.OPDT AND U03.DPTCD=U01.DPTCD AND U03.OPSEQ=U01.OPSEQ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID = U03.DRID";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + data.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + data.BDEDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + data.BDODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string sql2 = "";

                // --- B. 마취 정보 ---
                data.NCT_STA_DT = CUtil.GetDateTime(row["ANSDT"].ToString(), row["ANSHR"].ToString(), row["ANSMN"].ToString()); // 마취 시작일시(YYYYMMDDHHMM)
                data.NCT_END_DT = CUtil.GetDateTime(row["ANEDT"].ToString(), row["ANEHR"].ToString(), row["ANEMN"].ToString()); // 마취 종료일시(YYYYMMDDHHMM)

                // EMR082_2
                data.NCT_FRM_CD = ""; // 마취형태 구분코드(1.정규 2.응급)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT NCT_FRM_CD, ASA, ASA6, ASA7";
                sql2 += System.Environment.NewLine + "  FROM EMR082_2";
                sql2 += System.Environment.NewLine + " WHERE PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    data.NCT_FRM_CD = row2["NCT_FRM_CD"].ToString(); // 마취형태 구분코드(1.정규 2.응급)

                    return MetroLib.SqlHelper.BREAK;
                });

                // EMR320
                string mntr_knd_cd = "";
                string lbt_tret_yn = "";
                string cntr_tmpr_masr_yn = "";
                string tmpr_rgn_cd = "";
                string tmpr_rgn_etc_txt = "";
                string lwet_tmpr = "";
                string rcrm_ipat_yn = "";
                string rcrm_dsu_rs_cd = "";
                data.ASM_NCT_MTH_CD = ""; // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT MTH_CD, MTH_ETC, MIDD_MNTR_YN, MNTR_KND_CD, MNTR_ETC";
                sql2 += System.Environment.NewLine + "     , LBT_TRET_YN, CNTR_TMPR_MASR_YN, TMPR_RGN_CD, TMPR_RGN_ETC_TXT, LWET_TMPR, RCRM_IPAT_YN, RCRM_DSU_RS_CD";
                sql2 += System.Environment.NewLine + "  FROM EMR320";
                sql2 += System.Environment.NewLine + " WHERE PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                List<string> mth_cd_list = new List<string>();
                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string mth_cd = row2["MTH_CD"].ToString();
                    string[] mth_cd_ary = mth_cd.Split((char)21);
                    for (int i = 0; i < mth_cd_ary.Length; i++)
                    {
                        if (mth_cd_ary[i] == "1") mth_cd_list.Add((i+1).ToString());
                    }
                    data.ASM_NCT_MTH_CD = string.Join("/", mth_cd_list.ToArray()); // 마취방법 구분코드(다중선택, 예: 1/3/7/8)

                    mntr_knd_cd = row2["MNTR_KND_CD"].ToString(); // 실제 사용은 아랫쪽에서..
                    lbt_tret_yn = row2["LBT_TRET_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                    cntr_tmpr_masr_yn = row2["CNTR_TMPR_MASR_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                    tmpr_rgn_cd = row2["TMPR_RGN_CD"].ToString(); // 실제 사용은 아랫쪽에서..
                    tmpr_rgn_etc_txt = row2["TMPR_RGN_ETC_TXT"].ToString(); // 실제 사용은 아랫쪽에서..
                    lwet_tmpr = row2["LWET_TMPR"].ToString(); // 실제 사용은 아랫쪽에서..
                    rcrm_ipat_yn = row2["RCRM_IPAT_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                    rcrm_dsu_rs_cd = row2["RCRM_DSU_RS_CD"].ToString(); // 실제 사용은 아랫쪽에서..

                    return MetroLib.SqlHelper.BREAK;
                });


                // 수술명(TU02)
                data.NCT_RS_CD = ""; // 마취사유 구분코드(01~99)
                data.MDFEE_CD = ""; // 수가코드(마취사유가 02/03/04일 때 필수)
                data.MDFEE_CD_NM = ""; // 수가코드명(마취사유가 02/03/04일 때 필수)
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

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row2["ISPCD"].ToString();
                    string ispcd15 = ispcd.Substring(0, 5);

                    // 일단 이렇게...
                    data.NCT_RS_CD = "";       // 마취사유 구분코드(01~99)
                    if (IsLA210(ispcd15)) data.NCT_RS_CD = "04"; // 통증조절
                    else if (IsR3131(ispcd15)) data.NCT_RS_CD = "03"; // 무통분만
                    else data.NCT_RS_CD = "01"; // 처치 및 수술

                    data.MDFEE_CD = ispcd;  // 수가코드(마취사유가 02/03/04일 때 필수)
                    data.MDFEE_CD_NM = row2["ONM"].ToString(); // 수가코드명(마취사유가 02/03/04일 때 필수)

                    return MetroLib.SqlHelper.BREAK;
                });

                // --- C. 마취 전 ---
                data.PTNT_ASM_YN = "2";     // 마취 전 환자평가 시행여부(1.Yes 2.No)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT *";
                sql2 += System.Environment.NewLine + "  FROM EMR082_2";
                sql2 += System.Environment.NewLine + " WHERE PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT='" + data.BDEDT + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    data.PTNT_ASM_YN = "1";

                    return MetroLib.SqlHelper.BREAK;
                });

                // --- D. 마취 중 ---
                data.LBT_TRET_YN = "";         // 의도적 저체온증 적용 여부(1.Yes 2.No)
                if (lbt_tret_yn != "")
                {
                    string[] lbt_tret_yn_ary = lbt_tret_yn.Split((char)21);
                    if (lbt_tret_yn_ary[0] == "1") data.LBT_TRET_YN = "1";
                    if (lbt_tret_yn_ary[1] == "1") data.LBT_TRET_YN = "2";
                }

                data.CNTR_TMPR_MASR_YN = "";   // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                if (cntr_tmpr_masr_yn != "")
                {
                    string[] cntr_tmpr_masr_yn_ary = cntr_tmpr_masr_yn.Split((char)21);
                    if (cntr_tmpr_masr_yn_ary[0] == "1") data.CNTR_TMPR_MASR_YN = "1";
                    if (cntr_tmpr_masr_yn_ary[1] == "1") data.CNTR_TMPR_MASR_YN = "2";
                }

                data.TMPR_RGN_CD = "";         // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
                if (tmpr_rgn_cd != "")
                {
                    string[] tmpr_rgn_cd_ary = tmpr_rgn_cd.Split((char)21);
                    List<string> tmpr_rgn_cd_list = new List<string>();
                    for (int i = 0; i < tmpr_rgn_cd_ary.Length; i++)
                    {
                        if (tmpr_rgn_cd_ary[i] == "1")
                        {
                            if (i == 8)
                            {
                                tmpr_rgn_cd_list.Add("99"); // 기타
                            }
                            else
                            {
                                tmpr_rgn_cd_list.Add((i + 1).ToString("D2")); // 숫자를 2자리 문자로 만든다.(ex. 1 -> "01")
                            }
                        }
                    }
                    data.TMPR_RGN_CD = string.Join("/", tmpr_rgn_cd_list.ToArray());
                }
                data.TMPR_RGN_ETC_TXT = tmpr_rgn_etc_txt;    // 체온 측정방법 기타 상세(99일 때)
                data.LWET_TMPR = lwet_tmpr;           // 최저체온(℃, 소수점 첫째자리)

                // EMR320_MDCT
                data.NRRT_BLCK_USE_YN = "2";    // 신경근 차단제 사용 여부(1.Yes 2.No)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT KND_CD, MDCT_STDT, MDCT_STTM, MDCT_EDDT, MDCT_EDTM, MDS_NM, OQTY, UNIT";
                sql2 += System.Environment.NewLine + "  FROM EMR320_MDCT";
                sql2 += System.Environment.NewLine + " WHERE PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string knd_cd = row2["KND_CD"].ToString();
                    if (knd_cd == "3")
                    {
                        data.NRRT_BLCK_USE_YN = "1"; // 신경근 차단제 사용 여부(1.Yes 2.No)
                        return MetroLib.SqlHelper.BREAK;
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });

                data.NRRT_MNTR_YN = ""; // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)
                if (mntr_knd_cd != "")
                {
                    string[] mntr_knd_cd_ary = mntr_knd_cd.Split((char)21);
                    if (mntr_knd_cd_ary.Length >= 5 && mntr_knd_cd_ary[6] == "1")
                    {
                        data.NRRT_MNTR_YN = "1";
                    }
                }

                // --- E. 마취 후(회복실) ---
                data.RCRM_IPAT_YN = "";       // 회복실 입실 여부(1.Yes 2.No)
                if (rcrm_ipat_yn != "")
                {
                    string[] rcrm_ipat_yn_ary = rcrm_ipat_yn.Split((char)21);
                    if (rcrm_ipat_yn_ary[0] == "1") data.RCRM_IPAT_YN = "1";
                    if (rcrm_ipat_yn_ary[0] == "2") data.RCRM_IPAT_YN = "2";
                }

                data.RCRM_DSU_RS_CD = "";      // 회복실 미입실 사유(1~5, 입실 No일 때)
                if (rcrm_dsu_rs_cd != "")
                {
                    string[] rcrm_dsu_rs_cd_ary = rcrm_dsu_rs_cd.Split((char)21);
                    for (int i = 0; i < rcrm_dsu_rs_cd_ary.Length; i++)
                    {
                        if (rcrm_dsu_rs_cd_ary[i] == "1") data.RCRM_DSU_RS_CD = (i + 1).ToString();
                    }
                }
                data.EMSS_ASM_EXEC_FQ_CD = ""; // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                data.EMSS_ASM_RS_TXT = "";     // 오심 및 구토평가 미실시/1회 사유
                data.PAIN_ASM_EXEC_FQ_CD = ""; // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                data.PAIN_ASM_RS_TXT = "";     // 통증평가 미실시/1회 사유

                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U93.PT_INDT, U93.PT_INTM, U93.PT_OUTDT, U93.PT_OUTTM";
                sql2 += System.Environment.NewLine + "     , U93.ANDRID, A07.DRNM AS ANDRNM";
                sql2 += System.Environment.NewLine + "     , U93.EMPID, U93.WDATE, U93.WTIME";
                sql2 += System.Environment.NewLine + "     , U93.VOM_NRS_1, U93.VOM_NRS_2, U93.PAINCASE";
                sql2 += System.Environment.NewLine + "     , U93.PCA_1, U93.PCA_2, U93.PCA_3, U93.PCA_TXT";
                sql2 += System.Environment.NewLine + "     , U93.PARSCR1_1, U93.PARSCR1_2, U93.PARSCR1_3, U93.PARSCR1_4, U93.PARSCR1_5";
                sql2 += System.Environment.NewLine + "     , U93.PARSCR2_1, U93.PARSCR2_2, U93.PARSCR2_3, U93.PARSCR2_4, U93.PARSCR2_5";
                sql2 += System.Environment.NewLine + "     , U93.PAINDT1, U93.PAINDT2, U93.EMSSDT1, U93.EMSSDT2, U93.ASM_RST_TXT1, U93.ASM_RST_TXT2";
                sql2 += System.Environment.NewLine + "     , U93.OPDT, U93.DPTCD, U93.OPSEQ, U93.SEQ";
                sql2 += System.Environment.NewLine + "     , U93.EMSS_ASM_RS_TXT, U93.PAIN_ASM_RS_TXT";
                sql2 += System.Environment.NewLine + "  FROM TU93 U93 LEFT JOIN TA07 A07 ON A07.DRID = U93.ANDRID";
                sql2 += System.Environment.NewLine + " WHERE U93.PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND U93.OPDT='" + row["OPDT"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string emssdt1 = row2["EMSSDT1"].ToString(); // 오심구토 평가일시1
                    string emssdt2 = row2["EMSSDT2"].ToString(); // 오심구토 평가일시2
                    int emss_asm_exec_fq_cd = 0;
                    if (emssdt1 != "") emss_asm_exec_fq_cd++;
                    if (emssdt2 != "") emss_asm_exec_fq_cd++;

                    string paindt1 = row2["PAINDT1"].ToString(); // 통증 평가일시1
                    string paindt2 = row2["PAINDT2"].ToString(); // 통증 평가일시2
                    int pain_asm_exec_fq_cd = 0;
                    if (paindt1 != "") pain_asm_exec_fq_cd++;
                    if (paindt2 != "") pain_asm_exec_fq_cd++;

                    data.EMSS_ASM_EXEC_FQ_CD = emss_asm_exec_fq_cd.ToString(); // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                    data.PAIN_ASM_EXEC_FQ_CD = pain_asm_exec_fq_cd.ToString(); // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)

                    data.EMSS_ASM_RS_TXT = row2["EMSS_ASM_RS_TXT"].ToString();     // 오심 및 구토평가 미실시/1회 사유
                    data.PAIN_ASM_RS_TXT = row2["PAIN_ASM_RS_TXT"].ToString();     // 통증평가 미실시/1회 사유

                    return MetroLib.SqlHelper.BREAK;
                });


                return MetroLib.SqlHelper.BREAK;
            });



        }

        private bool IsLA210(string p_value)
        {
            if (p_value == "LA210") return true;
            if (p_value == "LA221") return true;
            if (p_value == "LA222") return true;
            if (p_value == "LA223") return true;
            if (p_value == "LA224") return true;
            if (p_value == "LA225") return true;
            if (p_value == "LA226") return true;
            if (p_value == "LA227") return true;
            if (p_value == "LA228") return true;
            if (p_value == "LA230") return true;
            if (p_value == "LA231") return true;
            if (p_value == "LA232") return true;
            if (p_value == "LA233") return true;
            if (p_value == "LA234") return true;
            if (p_value == "LA235") return true;
            if (p_value == "LA240") return true;
            if (p_value == "LA241") return true;
            if (p_value == "LA242") return true;
            if (p_value == "LA243") return true;
            if (p_value == "LA244") return true;
            if (p_value == "LA245") return true;
            if (p_value == "LA246") return true;
            if (p_value == "LA247") return true;
            if (p_value == "LA248") return true;
            if (p_value == "LA249") return true;
            if (p_value == "LA250") return true;
            if (p_value == "LA251") return true;
            if (p_value == "LA252") return true;
            if (p_value == "LA253") return true;
            if (p_value == "LA254") return true;
            if (p_value == "LA261") return true;
            if (p_value == "LA262") return true;
            if (p_value == "LA263") return true;
            if (p_value == "LA264") return true;
            if (p_value == "LA265") return true;
            if (p_value == "LA266") return true;
            if (p_value == "LA270") return true;
            if (p_value == "LA271") return true;
            if (p_value == "LA272") return true;
            if (p_value == "LA273") return true;
            if (p_value == "LA274") return true;
            if (p_value == "LA275") return true;
            if (p_value == "LA276") return true;
            return false;
        }

        private bool IsR3131(string p_value)
        {
            if (p_value == "R3131") return true;
            if (p_value == "R3133") return true;
            if (p_value == "R3136") return true;
            if (p_value == "R3138") return true;
            if (p_value == "R3141") return true;
            if (p_value == "R3143") return true;
            if (p_value == "R3146") return true;
            if (p_value == "R3148") return true;
            if (p_value == "R4351") return true;
            if (p_value == "R4353") return true;
            if (p_value == "R4356") return true;
            if (p_value == "R4358") return true;
            if (p_value == "RA311") return true;
            if (p_value == "RA312") return true;
            if (p_value == "RA313") return true;
            if (p_value == "RA314") return true;
            if (p_value == "RA315") return true;
            if (p_value == "RA316") return true;
            if (p_value == "RA317") return true;
            if (p_value == "RA318") return true;
            if (p_value == "RA431") return true;
            if (p_value == "RA432") return true;
            if (p_value == "RA433") return true;
            if (p_value == "RA434") return true;
            if (p_value == "R4360") return true;
            if (p_value == "R4361") return true;
            if (p_value == "R4362") return true;
            if (p_value == "RA361") return true;
            if (p_value == "RA362") return true;
            if (p_value == "R4380") return true;
            if (p_value == "RA380") return true;            
            return false;
        }
    }
}

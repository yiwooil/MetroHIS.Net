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
    class CMakeASM010
    {
        public void MakeASM010(CDataASM010_002 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
        {
            int count = data.Read_ASM000(p_conn, p_tran, re_query);

            if (count > 0 && (data.UPDDT != "" || data.STATUS != ""))
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

        private void SetData(CDataASM010_002 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            data.ClearMe();

            // 청구한 수술 코드를 구한다.(CQuery_ASM010과 동일)
            string f_bgiho = "";
            string f_exdt = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2F (NOLOCK)";
            sql += Environment.NewLine + " WHERE BDODT='" + data.BDODT + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + data.QFYCD + "'";
            sql += Environment.NewLine + "   AND JRBY='" + data.JRBY + "'";
            sql += Environment.NewLine + "   AND PID='" + data.PID + "'";
            sql += Environment.NewLine + "   AND UNISQ='" + data.UNISQ + "'";
            sql += Environment.NewLine + "   AND SIMCS='" + data.SIMCS + "'";
            sql += Environment.NewLine + "   AND ISNULL(MAFG,'')='2'";
            sql += Environment.NewLine + "   AND ISNULL(OKCD,'')=''"; // 위탁진료 제외
            sql += Environment.NewLine + " ORDER BY ELINENO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                f_bgiho = row["BGIHO"].ToString();
                f_exdt = row["EXDT"].ToString();
                if (CUtil_ASM010.IsOPCode(f_bgiho) == true)
                {
                    return MetroLib.SqlHelper.BREAK;
                }
                return MetroLib.SqlHelper.CONTINUE;
            });

            // A. 기본정보
            data.ASM_IPAT_DT = data.A04_BEDEDT + data.A04_BEDEHM; // 입원일시(YYYYMMDDHHMM)
            data.DSCG_YN = data.A04_BEDODT != "" ? "1" : "2"; // 퇴원여부(1.Yes 2.No)
            data.ASM_DSCG_DT = data.A04_BEDODT + data.A04_BEDOHM; // 퇴원일시(YYYYMMDDHHMM)


            // B.수술 및 감염 정보
            // 1. 수술 관련 환자 상태
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TU01 U01 (NOLOCK)";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + data.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + data.A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + data.A04_BEDODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            string sql2 = "";
            bool find = false;
            string opdt = "";
            string dptcd = "";
            string opseq = "";
            string opsdt = "";
            string opshr = "";
            string opsmn = "";
            string opedt = "";
            string opehr = "";
            string opemn = "";
            string ispcd = "";
            string statfg = "";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                opdt = row["OPDT"].ToString();
                dptcd = row["DPTCD"].ToString();
                opseq = row["OPSEQ"].ToString();
                opsdt = row["OPSDT"].ToString();
                opshr = row["OPSHR"].ToString();
                opsmn = row["OPSMN"].ToString();
                opedt = row["OPEDT"].ToString();
                opehr = row["OPEHR"].ToString();
                opemn = row["OPEMN"].ToString();

                if (opshr.Length == 1) opshr = "0" + opshr;
                if (opsmn.Length == 1) opsmn = "0" + opsmn;
                if (opehr.Length == 1) opehr = "0" + opehr;
                if (opemn.Length == 1) opemn = "0" + opemn;

                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U02.STAFG, A02.ISPCD";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + opdt + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + dptcd + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + opseq + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    ispcd = row2["ISPCD"].ToString();
                    statfg = row2["STAFG"].ToString();

                    // 수술코드는 청구한 자료를 사용한다.
                    string ispcd5 = ispcd.Length > 5 ? ispcd.Substring(0, 5) : ispcd;
                    if (f_bgiho.StartsWith(ispcd5))
                    {
                        find = true;
                        return MetroLib.SqlHelper.BREAK;
                    }

                    //if (CUtil_ASM010.IsOPCode(ispcd) == true)
                    //{
                    //    find = true;
                    //    return MetroLib.SqlHelper.BREAK;
                    //}

                    return MetroLib.SqlHelper.CONTINUE;
                });

                if (find == true) return MetroLib.SqlHelper.BREAK;

                return MetroLib.SqlHelper.CONTINUE;
            });

            if (find == false)
            {
                data.MDFEE_CD = f_bgiho; // 수가코드
                data.ASM_SOPR_STA_DT = f_exdt.Length > 8 ? f_exdt.Substring(0, 8) : f_exdt; // 수술 시작일시(YYYYMMDDHHMM)
                return;
            }

            // 마취내역을 읽는다.
            string anetp = "";
            string anedt = "";
            sql2 = "";
            sql2 += System.Environment.NewLine + "SELECT *";
            sql2 += System.Environment.NewLine + "  FROM TU03 U03 (NOLOCK)";
            sql2 += System.Environment.NewLine + " WHERE U03.PID='" + data.PID + "'";
            sql2 += System.Environment.NewLine + "   AND U03.OPDT='" + opdt + "'";
            sql2 += System.Environment.NewLine + "   AND U03.DPTCD='" + dptcd + "'";
            sql2 += System.Environment.NewLine + "   AND U03.OPSEQ='" + opseq + "'";
            MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            {
                System.Windows.Forms.Application.DoEvents();

                anetp = row2["ANETP"].ToString();
                anedt = row2["ANEDT"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // 마취 방법에 대한 수가코드(EDI코드)를 찾는다.
            sql2 = "";
            sql2 += System.Environment.NewLine + "SELECT A18.PRICD, A02.ISPCD";
            sql2 += System.Environment.NewLine + "  FROM TA18 A18 (NOLOCK) INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<='" + anedt + "')";
            sql2 += System.Environment.NewLine + " WHERE A18.OCD = '*A" + anetp + "'";
            sql2 += System.Environment.NewLine + "   AND A18.CREDT = (SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=A18.OCD AND X.CREDT<='" + anedt + "')";

            string base_ane = "2";
            MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            {
                System.Windows.Forms.Application.DoEvents();

                string anetp_ispcd = row2["ISPCD"].ToString();

                if (anetp_ispcd.StartsWith("L0101")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L0102")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L0103")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1211")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1212")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1213")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1214")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1215")) base_ane = "1";
                else if (anetp_ispcd.StartsWith("L1216")) base_ane = "1";

                return MetroLib.SqlHelper.BREAK;
            });

            // ASA점수(EMR082_2)
            sql2 = "";
            sql2 += System.Environment.NewLine + "SELECT ASA, ASA6, ASA7";
            sql2 += System.Environment.NewLine + "  FROM EMR082_2";
            sql2 += System.Environment.NewLine + " WHERE PID='" + data.PID + "'";
            sql2 += System.Environment.NewLine + "   AND OPDT='" + opdt + "'";
            sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

            string asa = "";
            string asa6 = "";
            string asa7 = "";
            MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            {
                System.Windows.Forms.Application.DoEvents();

                asa = row2["ASA"].ToString(); 
                asa6 = row2["ASA6"].ToString();
                asa7 = row2["ASA7"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // 값을 할당한다.

            data.MDFEE_CD = ispcd; // 수가코드
            data.ASM_SOPR_STA_DT = opsdt + opshr + opsmn; // 수술 시작일시(YYYYMMDDHHMM)
            data.ASM_SOPR_END_DT = opedt + opehr + opemn; // 수술 종료일시(YYYYMMDDHHMM)
            data.EMY_CD = statfg == "0" ? "1" : "2"; // 응급여부(1.정규 2.응급)
            if (CConfig.asm010_fld3qty != "") data.EMY_CD = CConfig.asm010_fld3qty; // 옵션적용
            data.KNJN_RPMT = ispcd.StartsWith("N2072") || ispcd.StartsWith("N2077") ? "1" : "2"; // 슬관절치환술(1.Yes 2.No)
            data.HMRHG_CTRL_YN = "2"; // 토니켓 적용 여부(1.Yes 2.No)
            data.HMRHG_CTRL_DT = ""; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            // 슬괄전치환술이면 토니켓 사용함.
            if (data.KNJN_RPMT == "1")
            {
                data.HMRHG_CTRL_YN = "1"; // 토니켓 적용 여부(1.Yes 2.No)
                data.HMRHG_CTRL_DT = data.ASM_SOPR_STA_DT; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            }
            data.CAESR_YN = ispcd.StartsWith("R4517") || ispcd.StartsWith("R4518") || ispcd.StartsWith("R4514") ? "1" : "2"; // 제왕절개술 시행 여부(1.Yes 2.No)
            data.NBY_PARTU_DT = ""; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
            data.CRVD_YN = ""; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            data.BSE_NCT_YN = base_ane; // 기본마취 여부(1.Yes 2.No)
            asa += "      "; // 오류방지용
            data.ASA_PNT = ""; // ASA 점수
            if (asa.Substring(0, 1) == "1") data.ASA_PNT = "1";
            else if (asa.Substring(1, 1) == "1") data.ASA_PNT = "2";
            else if (asa.Substring(2, 1) == "1") data.ASA_PNT = "3";
            else if (asa.Substring(3, 1) == "1") data.ASA_PNT = "4";
            else if (asa.Substring(4, 1) == "1") data.ASA_PNT = "5";
            else if (asa6 == "1") data.ASA_PNT = "6";
            else if (asa7 == "1") data.ASA_PNT = "7";
            else if (asa.Substring(5, 1) == "1") data.ASA_PNT = "8";
            // 2.수술 전 항생제 투여
            data.SOPR_BF_ANBO_INJC_YN = ""; // 수술 전 항생제 투여 여부(1.Yes 2.No)
            data.SOPR_BF_INFC_SICK_YN = ""; // 감염상병 확진 여부(1.Yes 2.No)
            data.SOPR_BF_INFC_SICK_CD = ""; // 감염상병 확진명
            data.SOPR_BF_DDIAG_YN = ""; // 감염내과 협진여부(1.Yes 2.No)
            data.SOPR_BF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
            data.SOPR_BF_RPY_YN = ""; // 회신 여부(1.Yes 2.No)
            data.SOPR_BF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
            data.SOPR_BF_ANBO_DR_RCD_YN = ""; // 항생제 필요 의사기록 여부(1.Yes 2.No)
            data.SOPR_BF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            data.SOPR_BF_ANBO_DR_RCDC_CD = ""; // 기록지 종류(YYYYMMDDHHMM)
            data.SOPR_BF_REQR_RS_CD = ""; // 필요사유
            data.SOPR_BF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
            // 3.평가 제외 수술
            data.ASM_TGT_SOPR_SAME_ENFC_YN = ""; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
            data.FQ2_GT_SOPR_ENFC_YN = ""; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
            // 4.수술 후 항셍제 투여
            data.SOPR_RGN_INFC_ANBO_INJC_YN = ""; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
            data.SOPR_RGN_INFC_CD = ""; // 수술부위 감영 유형
            data.ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            data.SOPR_RGN_INFC_DR_RCDC_CD = ""; // 기록지 종류
            data.SOPR_RGN_INFC_DR_RCD_TXT = ""; // 수술 부위 감염 사유 상세(평문)
            data.INFC_ANBO_INJC_YN = ""; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
            data.CLTR_STRN_YN = ""; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
            data.ASM_GAT_DT = ""; // 채취일시(YYYYMMDDHHMM)
            data.INFC_SICK_DIAG = ""; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
            data.SOPR_AF_INFC_SICK_CD = ""; // 감염 상병 화진명
            data.SOPR_AF_DDIAG_YN = ""; // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
            data.SOPR_AF_ASM_REQ_DT = ""; // 의뢰일시(YYYYMMDDHHMM)
            data.SOPR_AF_RPY_YN = ""; // 회신 여부(1.Yes 2.No)
            data.SOPR_AF_ASM_RPY_DT = ""; // 회신일시(YYYYMMDDHHMM)
            data.SOPR_AF_ANBO_DR_RCD_YN = ""; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
            data.SOPR_AF_ASM_RCD_DT = ""; // 기록일시(YYYYMMDDHHMM)
            data.SOPR_AF_ANBO_DR_RCDC_CD = ""; // 기록지 종류
            data.SOPR_AF_REQR_RS_CD = ""; // 필요사유
            data.SOPR_AF_DR_RCD_TXT = ""; // 기록 상세 내용(평문)
            data.ANBO_ALRG_YN = ""; // 항생제 알러지 여부(1.Yes 2.No)

            data.WHBL_RBC_BLTS_YN = ""; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            data.BLTS_STA_DT.Clear(); // 수혈시작일시
            data.BLTS_END_DT.Clear(); // 수혈종료일시
            data.BLTS_MDFEE_CD.Clear(); // 수가코드
            data.BLTS_DGM_NM.Clear(); // 수혈제제명

            //// 수혈 (TB08)
            //sql2 = "";
            //sql2 += System.Environment.NewLine + "SELECT *";
            //sql2 += System.Environment.NewLine + "  FROM TB08 B08 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=B08.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=B08.OCD AND X.CREDT<=B08.BLDODT)";
            //sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=B08.BLDODT)";
            //sql2 += System.Environment.NewLine + " WHERE B08.PID='" + data.PID + "'";
            //sql2 += System.Environment.NewLine + "   AND B08.BEDEDT='" + data.A04_BEDEDT + "'";
            //sql2 += System.Environment.NewLine + "   AND B08.BLDODT>='" + opdt + "'";
            //sql2 += System.Environment.NewLine + "   AND ISNULL(B08.BLDRTNDT,'')=''";
            //sql2 += System.Environment.NewLine + " ORDER BY B08.BLDODT";
            //
            //data.WHBL_RBC_BLTS_YN = "2"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            //MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            //{
            //    string bldcd = row2["ISPCD"].ToString();
            //    if (CUtil_ASM010.IsBLDCode(ispcd))
            //    {
            //        data.WHBL_RBC_BLTS_YN = "1"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            //        data.BLTS_STA_DT.Add(row2["BLTODT"].ToString()); // 수혈시작일시
            //        data.BLTS_END_DT.Add(""); // 수혈종료일시
            //        data.BLTS_MDFEE_CD.Add(bldcd); // 수가코드
            //        data.BLTS_DGM_NM.Add(row2["PRKNM"].ToString()); // 수혈제제명
            //    }
            //    return MetroLib.SqlHelper.CONTINUE;
            //});

            // C.항생제 투여 여부
            sql2 = "";
            sql2 += System.Environment.NewLine + "SELECT *";
            sql2 += System.Environment.NewLine + "  FROM TV20 V20 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=V20.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V20.OCD AND X.CREDT<=V20.DODT)";
            sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V20.DODT)";
            if (CConfig.BodyNewFg == "1")
            {
                sql2 += System.Environment.NewLine + "                         INNER JOIN TV01A V01A (NOLOCK) ON V01A.BPID=V20.PID AND V01A.BBEDEDT=V20.BEDEDT AND V01A.BBDIV=V20.BDIV AND V01A.BODT=V20.ODT AND V01A.BONO=V20.ONO AND V01A.OCD=V20.OCD";
            }
            else
            {
                sql2 += System.Environment.NewLine + "                         INNER JOIN TV01 V01 (NOLOCK) ON V01.PID=V20.PID AND V01.BEDEDT=V20.BEDEDT AND V01.BDIV=V20.BDIV AND V01.ODT=V20.ODT AND V01.ONO=V20.ONO";
                sql2 += System.Environment.NewLine + "                         INNER JOIN TV01A V01A (NOLOCK) ON V01A.HDID=V01.HDID AND V01A.OCD=V20.OCD";
            }
            sql2 += System.Environment.NewLine + " WHERE V20.PID='" + data.PID + "'";
            sql2 += System.Environment.NewLine + "   AND V20.BEDEDT='" + data.A04_BEDEDT + "'";
            sql2 += System.Environment.NewLine + "   AND V20.ODIVCD LIKE 'M%'";
            sql2 += System.Environment.NewLine + "   AND V20.DSTSCD = 'Y'";
            sql2 += System.Environment.NewLine + " ORDER BY V20.DODT, V20.DOHR, V20.DOMN";

            data.ANBO_USE_YN = "2"; // 항생제 투여 여부(1.Yes 2.No)
            MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            {
                System.Windows.Forms.Application.DoEvents();

                string anbocd = row2["ISPCD"].ToString();
                if (CUtil_ASM010.IsANBOCode(anbocd))
                {
                    string enddt = row2["ENDDT"].ToString();
                    string endhr = row2["ENDHR"].ToString();
                    string endmn = row2["ENDMN"].ToString();
                    if (enddt == "")
                    {
                        enddt = row2["DODT"].ToString();
                        endhr = row2["DOHR"].ToString();
                        endmn = row2["DOMN"].ToString();
                    }
                    string fldcd4 = row2["FLDCD4"].ToString();
                    string injPath = "";
                    if (fldcd4.StartsWith("IV")) injPath = "1";
                    if (fldcd4.StartsWith("IM")) injPath = "2";
                    if (fldcd4.StartsWith("PO")) injPath = "3";

                    data.ANBO_USE_YN = "1"; // 항생제 투여 여부(1.Yes 2.No)

                    // 항생제 투여
                    data.INJC_STA_DT.Add(row2["DODT"].ToString() + row2["DOHR"].ToString() + row2["DOMN"].ToString()); // 투여시작일시
                    data.INJC_END_DT.Add(enddt + endhr + endmn); // 투여종료일시
                    data.INJC_MDS_CD.Add(anbocd); // 약품코드
                    data.INJC_MDS_NM.Add(row2["PRKNM"].ToString()); // 약품명
                    data.ANBO_INJC_PTH_CD.Add(injPath); // 투여경로
                }

                return MetroLib.SqlHelper.CONTINUE;

            });

            // 퇴원 시 항생제 처방
            sql2 = "";
            sql2 += System.Environment.NewLine + "SELECT *";
            sql2 += System.Environment.NewLine + "  FROM TV01 V01 (NOLOCK) INNER JOIN TV01A V01A (NOLOCK)";
            if (CConfig.BodyNewFg == "1")
            {
                sql2 += System.Environment.NewLine + "                                 ON V01A.BPID=V01.PID AND V01A.BBEDEDT=V01.BEDEDT AND V01A.BBDIV=V01.BDIV AND V01A.BODT=V01.ODT AND V01A.BONO=V01.ONO";
            }
            else
            {
                sql2 += System.Environment.NewLine + "                                 ON V01A.HDID=V01.HDID";
            }
            sql2 += System.Environment.NewLine + "                         INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V01.ODT)";
            sql2 += System.Environment.NewLine + " WHERE V01.PID='" + data.PID + "'";
            sql2 += System.Environment.NewLine + "   AND V01.BEDEDT='" + data.A04_BEDEDT + "'";
            sql2 += System.Environment.NewLine + "   AND V01.OKCD = '3'";
            sql2 += System.Environment.NewLine + "   AND V01.ODIVCD LIKE 'M%'";
            sql2 += System.Environment.NewLine + "   AND ISNULL(V01.DCFG,'') IN ('','0')";

            data.DSCG_ANBO_PRSC_YN = "2"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)
            MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
            {
                System.Windows.Forms.Application.DoEvents();

                string anbocd = row2["ISPCD"].ToString();
                if (CUtil_ASM010.IsANBOCode(anbocd))
                {
                    data.DSCG_ANBO_PRSC_YN = "1"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)

                    data.PRSC_MDS_CD.Add(anbocd); // 약품코드
                    data.PRSC_MDS_NM.Add(row2["PRKNM"].ToString()); // 약품명
                    data.PRSC_TOT_INJC_DDCNT.Add(row2["ODAYCNT"].ToString()); // 총 투약일수
                }

                return MetroLib.SqlHelper.CONTINUE;
            });
            
        }
    }
}

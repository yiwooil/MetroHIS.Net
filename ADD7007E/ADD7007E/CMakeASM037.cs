using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CMakeASM037
    {
        public void MakeASM037(CDataASM037_003 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
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

        private void SetData(CDataASM037_003 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            data.ClearMe();

            // A. 기본정보
            data.IPAT_DD = data.BDEDT; // 입원일자(YYYYMMDD)
            data.DSCG_DD = data.BDODT; // 퇴원일자(YYYYMMDD)


            // B. 수술정보
            data.SOPR_YN = "2"; // 수술 여부(1.Yes 2.No)
            data.LFB_FS_YN = "2"; // 척추후방고정술 실시여부(1.Yes 2.No)
            data.LFB_FS_LVL = ""; // 척추후방고정술 Level(1,2,3)
            data.KNJN_RPMT_YN = "2"; // 슬관절치환술 실시여부(1.Yes 2.No)
            data.KNJN_RPMT_RGN_CD = ""; // 슬관절치환술 부위(1.단측 2.양측)

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TU01 U01 (NOLOCK)";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + data.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + data.A04_BEDEDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + data.A04_BEDODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string opdt = row["OPDT"].ToString();
                string opsdt = row["OPSDT"].ToString();
                string orinhr = row["ORINHR"].ToString();
                string orinmn = row["ORINMN"].ToString();
                string opodt = row["OPODT"].ToString();
                string opohr = row["OPOHR"].ToString();
                string opomn = row["OPOMN"].ToString();

                if (opsdt == "") opsdt = opdt;
                if (orinhr.Length == 1) orinhr = "0" + orinhr;
                if (orinmn.Length == 1) orinmn = "0" + orinmn;
                if (opohr.Length == 1) opohr = "0" + opohr;
                if (opomn.Length == 1) opomn = "0" + opomn;
                if (opodt == "")
                {
                    opohr = "";
                    opomn = "";
                }

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A02.ISPCD, A02.PRKNM";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + data.PID + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + opdt + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row2["ISPCD"].ToString();

                    data.SOPR_YN = "1"; // 수술 여부(1.Yes 2.No)
                    data.ASM_OPRM_IPAT_DT.Add(opsdt + orinhr + orinmn); // 수술실 입실일시(YYYYMMDDHHMM)
                    data.ASM_OPRM_DSCG_DT.Add(opodt + opohr + opomn); // 수술실 퇴실일시(YYYYMMDDHHMM)
                    data.ASM_RCRM_DSCG_DT.Add(opodt + opohr + opomn); // 회복실 퇴실일시(YYYYMMDDHHMM)
                    data.SOPR_NM.Add(row2["PRKNM"].ToString()); // 수술명
                    data.SOPR_MDFEE_CD.Add(ispcd); // 수가코드(MDFEE_CD)

                    if (ispcd.StartsWith("N0469") || ispcd.StartsWith("N2470")) data.LFB_FS_YN = "1"; // 척추후방고정술 실시여부(1.Yes 2.No)
                    if (ispcd.StartsWith("N2072")) data.KNJN_RPMT_YN = "1"; // 슬관절치환술 실시여부(1.Yes 2.No)

                    return MetroLib.SqlHelper.CONTINUE;
                });

                return MetroLib.SqlHelper.CONTINUE;
            });

            // C. 수혈 체크리스트 사용 현황
            data.ASM_PRSC_YN = ""; // 처방여부(1.Yes 2.No)
            data.ASM_PRSC_DT.Clear(); // 처방일시(YYYYMMDDHHMM)
            data.ASM_PRSC_UNIT_CNT.Clear(); // 처방량(unit)
            data.ASM_BLTS_CHKLST_USE_YN.Clear(); // 수혈 체크리스트 사용여부(1.Yes 2.No)
            data.ASM_BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)
            data.ASM_PRSC_BLTS_DGM_NM.Clear(); // 수혈제제명(BLTS_DGM_NM)
            data.ASM_PRSC_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            data.ASM_BLTS_UNIT_CNT.Clear(); // 수혈량(unit)(BLTS_UNIT_CNT)

            // D. 투약정보
            data.ANM_DIAG_YN = ""; // 빈혈 진단(1.Yes 2.No)
            data.SICK_SYM.Clear(); // 상병분류기호
            data.DIAG_NM.Clear(); // 진단명
            data.ANM_REFM_YN = ""; // 빈혈교정 유무(1.Yes 2.No)
            data.MDS_NM.Clear(); // 빈혈교정 처방약품명
            data.MDS_CD.Clear(); // 빈혈교정 처방약품코드
            
            // E. 검사정보
            data.HG_EXM_ENFC_YN = "2"; // Hb검사 시행여부(1.Yes 2.No)
            data.ASM_EXM_RST_DT.Clear(); // 검사결과일시(YYYYMMDDHHMM)
            data.EXM_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            data.EXM_NM.Clear(); // 검사명
            data.HG_NUV.Clear(); // 검사결과(g/dL)

            string exm_frdt = MetroLib.Util.AddMonth(data.A04_BEDEDT, -3);
            sql = "";
            sql += Environment.NewLine + "SELECT DISTINCT SPCNO, SEX, AGE, RCVDT, RCVTM, STSCD, SPCFOOTSEQ, PTHRPT, DIAGNOSIS, ORDDT, SPCNM, MAJNM";
            sql += Environment.NewLine + "  FROM TC201 (NOLOCK)";
            sql += Environment.NewLine + " WHERE PTID='" + data.PID + "'";
            sql += Environment.NewLine + "   AND ORDDT>='" + exm_frdt + "'";
            sql += Environment.NewLine + "   AND ORDDT<='" + data.A04_BEDODT + "'";
            sql += Environment.NewLine + "   AND (CANCELFG != '1' OR CANCELFG IS NULL)";
            sql += Environment.NewLine + "   AND STSCD>='1'";
            sql += Environment.NewLine + " ORDER BY ORDDT, SPCNO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string spcno = row["SPCNO"].ToString();
                string orddt = row["ORDDT"].ToString();

                string sql2 = "";
                sql2 = "";
                sql2 += Environment.NewLine + "SELECT A.SPCNO, A.TESTCD, A.SEQ, A.APPDT, A.APPTM, A.TESTDIV, A.TESTRSTTYPE, A.HEADTESTCD";
                sql2 += Environment.NewLine + "     , A.SITECD, A.PTID, A.RSTFG, A.WSCD, A.SPCCD, A.RSTVAL, A.REFERCHK, A.PANICCHK";
                sql2 += Environment.NewLine + "     , A.DELTACHK, A.PICKCD, A.MICRORSTTYPE, A.STAINSEQ, A.CULTURESEQ, A.FOOTSEQ, A.MODIFYFG";
                sql2 += Environment.NewLine + "     , A.DLYFG, A.STATFG, A.MANSTATFG, A.EQUIPCD, A.VFYID, A.VFYDT, A.VFYTM, A.PRTFG, A.PRTDT";
                sql2 += Environment.NewLine + "     , A.PRTTM, A.STSCD, A.CANCELFG, A.CANCELCD, A.REGDR, A.SPECDR, B.ABBRNM";
                sql2 += Environment.NewLine + "     , B.DATATYPE, B.DATALEN, B.KEYPAD, B.RSTFG, B.NORSTFG, B.QUERYFG, B.NORSTQUERYFG, B.ONOFFFG, B.WEEKDAYDIV";
                sql2 += Environment.NewLine + "     , A02.ISPCD, A02.PRKNM";
                sql2 += Environment.NewLine + "  FROM TC301 A (NOLOCK) INNER JOIN TC001 B (NOLOCK) ON B.TESTCD=A.TESTCD AND B.APPDT=A.APPDT AND B.APPTM=A.APPTM";
                sql2 += Environment.NewLine + "                        INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=A.TESTCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=A.TESTCD AND X.CREDT<='" + orddt + "')";
                sql2 += Environment.NewLine + "                        INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<='" + orddt + "')";
                sql2 += Environment.NewLine + " WHERE A.SPCNO='" + spcno + "'";
                sql2 += Environment.NewLine + "   AND (A.CANCELFG != '1' OR A.CANCELFG IS NULL)";
                sql2 += Environment.NewLine + "   AND A.STSCD>='7'";
                sql2 += Environment.NewLine + " ORDER BY A.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row2["ISPCD"].ToString();
                    if (ispcd.StartsWith("D000205") || ispcd.StartsWith("D0003") || ispcd.StartsWith("D0005"))
                    {
                        data.HG_EXM_ENFC_YN = "1"; // Hb검사 시행여부(1.Yes 2.No)
                        data.ASM_EXM_RST_DT.Add(row2["VFYDT"].ToString() + row2["VFYTM"].ToString()); // 검사결과일시(YYYYMMDDHHMM)
                        data.EXM_MDFEE_CD.Add(ispcd); // 수가코드(MDFEE_CD)
                        data.EXM_NM.Add(row2["PRKNM"].ToString()); // 검사명
                        data.HG_NUV.Add(row2["RSTVAL"].ToString()); // 검사결과(g/dL)
                    }
                    return MetroLib.StrHelper.CONTINUE;
                });
                
                return MetroLib.StrHelper.CONTINUE;
            });

            

            // F. 수혈정보
            data.BLTS_YN = ""; // 수혈 시행여부(1.Yes 2.No)
            data.BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)(ASM_BLTS_STA_DT)
            data.BLTS_END_DT.Clear(); // 수혈종료일시(YYYYMMDDHHMM)(ASM_BLTS_END_DT)
            data.BLTS_DGM_NM.Clear(); // 수혈제제명
            data.BLTS_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            data.BLTS_UNIT_CNT.Clear(); // 수혈량(unit)
            data.HG_DCR_YN.Clear(); // Hb저하 발생 여부(1.Yes 2.No)
            data.OPRM_HMRHG_OCUR_YN_CD.Clear(); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
            data.OPRM_MIDD_HMRHG_QTY.Clear(); // 수술 중 실혈량(ml)
            data.OPRM_AF_DRN_QTY.Clear(); // 수술 후 배액량(ml)
            data.BLTS_RS_ETC_YN.Clear(); // 그 외 수혈사유 여부(1.Yes 2.No)
            data.BLTS_RS_ETC_TXT.Clear(); // 수혈사유 기타 상세        
        }
    }
}

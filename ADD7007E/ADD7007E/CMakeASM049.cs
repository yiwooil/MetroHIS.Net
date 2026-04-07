using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ADD7007E
{
    class CMakeASM049
    {
        public void MakeASM049(CDataASM049_001 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool re_query)
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
                SetData_TX01(data, p_sysdt, p_systm, p_user, p_conn, p_tran);


                // TI84_ASM000 저장
                data.Into_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

                // 자료저장
                data.InsData(p_sysdt, p_systm, p_user, p_conn, p_tran, count > 0);

            }
        }

        private void SetData_TX01(CDataASM049_001 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            data.ClearMe();

            if (data.IOFG == "1")
            {
                data.IPAT_OPAT_TP_CD = "2";
                data.DIAG_DD = data.STEDT;
            }
            else
            {
                data.IPAT_OPAT_TP_CD = "1";
                data.IPAT_DD = data.BDEDT != "" ? data.BDEDT : data.STEDT; // 기본애 BDEDT, 값이 없으면 STEDT
                data.EMRRM_PASS_YN = "2";
                if (data.EMRRM_PASS_YN == "1")
                {
                    data.EMRRM_VST_DD = data.IPAT_DD; // 응급실 내원일
                }
                data.DSCG_DD = data.A04_BEDODT;
            }

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TX01 X01 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=X01.OCD";
            sql += Environment.NewLine + "                                                     AND A18.CREDT=(SELECT MAX(X.CREDT)";
            sql += Environment.NewLine + "                                                                      FROM TA18 X (NOLOCK)";
            sql += Environment.NewLine + "                                                                     WHERE X.OCD=X01.OCD";
            sql += Environment.NewLine + "                                                                       AND X.CREDT<=X01.ODT";
            sql += Environment.NewLine + "                                                                   ) ";
            sql += Environment.NewLine + " WHERE X01.PID='" + data.PID + "'";
            if (data.IOFG == "1")
            {
                sql += Environment.NewLine + "   AND X01.ODT='" + data.STEDT + "'";
            }
            else
            {
                // 외래나 응급실에서 처방 받고 입원한 경우가 있을 수 있어서 이렇게 QUERY함.
                sql += Environment.NewLine + "   AND X01.ODT>='" + data.IPAT_DD + "'"; // 입원일
                sql += Environment.NewLine + "   AND X01.ODT<='" + data.A04_BEDODT + "'"; // 퇴원일
            }
            sql += Environment.NewLine + " ORDER BY X01.ODT";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string pricd = row["PRICD"].ToString();
                string odt = row["ODT"].ToString();

                // 수납내역을 읽어서 비급여로 수납했으면 제외시킴.
                string chrlt = GetChrlt(row, p_conn, p_tran);

                if (chrlt == "1" || chrlt == "4" || chrlt == "5")
                {
                    // 비급여, 비보험이면 제외.
                    return MetroLib.SqlHelper.CONTINUE;
                }

                if (pricd.StartsWith("X") == false)
                {
                    SetData_TX01_1(data, "", pricd, row, p_conn, p_tran);
                }
                else
                {
                    string sql2 = "";
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT *";
                    sql2 += Environment.NewLine + "  FROM TA02A A02A (NOLOCK)";
                    sql2 += Environment.NewLine + " WHERE A02A.PRICD='" + pricd + "'";
                    sql2 += Environment.NewLine + "   AND A02A.CREDT=(SELECT MAX(X.CREDT)";
                    sql2 += Environment.NewLine + "                     FROM TA02A X (NOLOCK)";
                    sql2 += Environment.NewLine + "                    WHERE X.PRICD=A02A.PRICD";
                    sql2 += Environment.NewLine + "                      AND X.CREDT<='" + odt + "'";
                    sql2 += Environment.NewLine + "                  )";
                    sql2 += Environment.NewLine + " ORDER BY A02A.SEQ";

                    MetroLib.SqlHelper.GetDataRow(sql2, p_conn, p_tran, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();

                        string spcd = row2["SPCD"].ToString();

                        SetData_TX01_1(data, pricd, spcd, row, p_conn, p_tran);

                        return MetroLib.SqlHelper.CONTINUE;
                    });
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            // 검사종류
            List<string> exmKnd = new List<string>();
            if (data.CT_MDFEE_CD.Count>0) exmKnd.Add("1");
            if (data.MRI_MDFEE_CD.Count > 0) exmKnd.Add("2");
            if (data.PET_MDFEE_CD.Count > 0) exmKnd.Add("3");
            data.IMG_EXM_KND_CD = string.Join("/", exmKnd.ToArray());
        }

        private void SetData_TX01_1(CDataASM049_001 data, string p_grpcd, string p_pricd, DataRow p_row, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string odt = p_row["ODT"].ToString();
            string acpttm = p_row["ACPTTM"].ToString().Substring(0, 4);
            string rptdt = p_row["RPTDT"].ToString();
            string bdiv = p_row["BDIV"].ToString();
            string grpact = "";

            string sql = "";

            if (p_grpcd != "")
            {
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TA02 A02 (NOLOCK)";
                sql += Environment.NewLine + " WHERE A02.PRICD='" + p_grpcd + "'";
                sql += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT)";
                sql += Environment.NewLine + "                    FROM TA02 X (NOLOCK)";
                sql += Environment.NewLine + "                   WHERE X.PRICD=A02.PRICD";
                sql += Environment.NewLine + "                     AND X.CREDT<='" + odt + "'";
                sql += Environment.NewLine + "                 )";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
                {
                    System.Windows.Forms.Application.DoEvents();

                    grpact = row["ACTFG"].ToString();
                    return MetroLib.SqlHelper.BREAK;
                });
            }

            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA02 A02 (NOLOCK)";
            sql += Environment.NewLine + " WHERE A02.PRICD='" + p_pricd + "'";
            sql += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT)";
            sql += Environment.NewLine + "                    FROM TA02 X (NOLOCK)";
            sql += Environment.NewLine + "                   WHERE X.PRICD=A02.PRICD";
            sql += Environment.NewLine + "                     AND X.CREDT<='" + odt + "'";
            sql += Environment.NewLine + "                 )";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string ispcd = row["ISPCD"].ToString();
                string prknm = row["PRKNM"].ToString();
                string actfg = row["ACTFG"].ToString();

                if (ispcd.StartsWith("GB") || ispcd.StartsWith("HB"))
                {
                    // Full PACS 이용료임. 제외.
                }
                else
                {
                    if (actfg == "05" || grpact == "05")
                    {
                        // 진료유형
                        if (data.CT_DIAG_TY_CD != "1" && data.CT_DIAG_TY_CD != "2" && data.CT_DIAG_TY_CD != "3")
                        {
                            if (bdiv == "1") data.CT_DIAG_TY_CD = "3";
                            else if (bdiv == "2") data.CT_DIAG_TY_CD = "2";
                            else if (bdiv == "3") data.CT_DIAG_TY_CD = "1";

                        }
                        // 조영제 사용여부. 기본이 No
                        if (data.CT_CTRST_USE_YN != "1" && data.CT_CTRST_USE_YN != "2") data.CT_CTRST_USE_YN = "2";
                        if (Regex.IsMatch(ispcd, @"^\d{9}$"))
                        {
                            // edi코드가 모두 숫자이면 약제임.
                            // 조영제로 간주한다.
                            data.CT_CTRST_USE_YN = "1";
                        }
                        if (actfg == "05")
                        {
                            string pandok = GetPandok(ispcd, odt, actfg, p_conn, p_tran);
                            data.CT_MDFEE_CD.Add(ispcd);       // 수가코드
                            data.CT_MDFEE_CD_NM.Add(prknm);    // 검사명
                            data.CT_EXM_EXEC_DT.Add(odt + acpttm);    // 검사일시(YYYYMMDDHHMM)
                            data.CT_RD_SDR_DCT_YN.Add(pandok);  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                            data.CT_DCT_RST_DD.Add(rptdt);  // 판독 완료일(YYYYMMDD)
                        }
                    }
                    else if (actfg == "06" || grpact == "06")
                    {
                        // 진료유형
                        if (data.MRI_DIAG_TY_CD != "1" && data.MRI_DIAG_TY_CD != "2" && data.MRI_DIAG_TY_CD != "3")
                        {
                            if (bdiv == "1") data.MRI_DIAG_TY_CD = "3";
                            else if (bdiv == "2") data.MRI_DIAG_TY_CD = "2";
                            else if (bdiv == "3") data.MRI_DIAG_TY_CD = "1";

                        }
                        // 조영제 사용여부. 기본이 No
                        if (data.MRI_CTRST_USE_YN != "1" && data.MRI_CTRST_USE_YN != "2") data.MRI_CTRST_USE_YN = "2";
                        if (Regex.IsMatch(ispcd, @"^\d{9}$"))
                        {
                            // edi코드가 모두 숫자이면 약제임.
                            // 조영제로 간주한다.
                            data.MRI_CTRST_USE_YN = "1";
                        }
                        if (actfg == "06")
                        {
                            string pandok = GetPandok(ispcd, odt, actfg, p_conn, p_tran);
                            data.MRI_MDFEE_CD.Add(ispcd);       // 수가코드
                            data.MRI_MDFEE_CD_NM.Add(prknm);    // 검사명
                            data.MRI_EXM_EXEC_DT.Add(odt + acpttm);    // 검사일시(YYYYMMDDHHMM)
                            data.MRI_RD_SDR_DCT_YN.Add(pandok);  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                            data.MRI_DCT_RST_DD.Add(rptdt);     // 판독 완료일(YYYYMMDD)
                        }
                    }
                    else if (actfg == "07" || grpact == "07")
                    {
                        if (actfg == "07")
                        {
                            data.PET_MDFEE_CD.Add(ispcd);       // 수가코드
                            data.PET_MDFEE_CD_NM.Add(prknm);    // 검사명
                            data.PET_EXM_EXEC_DT.Add(odt + acpttm);    // 검사일시(YYYYMMDDHHMM)
                        }
                    }
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

        }

        private string GetChrlt(DataRow p_row, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string chrlt = "";
            string pid = p_row["PID"].ToString();
            string bdiv = p_row["BDIV"].ToString();
            string kystr = "";
            if (bdiv == "1")
            {
                kystr = p_row["PID"].ToString() + "," + p_row["ODT"].ToString() + "," + p_row["ONO"].ToString();
            }
            else
            {
                kystr = p_row["PID"].ToString() + "," + p_row["BEDEDT"].ToString() + "," + p_row["BDIV"].ToString() + "," + p_row["ODT"].ToString() + "," + p_row["ONO"].ToString();
            }

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT CHRLT, SUM(DQTY*DDAY) AS TQTY";
            sql += Environment.NewLine + "  FROM TS31 (NOLOCK)";
            sql += Environment.NewLine + " WHERE PID='" + pid + "'";
            sql += Environment.NewLine + "   AND KYSTR='" + kystr + "'";
            sql += Environment.NewLine + "   AND ISNULL(RECFG,'') NOT IN ('F','D','O')";
            sql += Environment.NewLine + " GROUP BY CHRLT";
            sql += Environment.NewLine + "HAVING SUM(DQTY*DDAY)<>0";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                chrlt = row["CHRLT"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            sql = "";
            sql += Environment.NewLine + "SELECT CHRLT, SUM(DQTY*DDAY) AS TQTY";
            sql += Environment.NewLine + "  FROM TT31 (NOLOCK)";
            sql += Environment.NewLine + " WHERE PID='" + pid + "'";
            sql += Environment.NewLine + "   AND KYSTR='" + kystr + "'";
            sql += Environment.NewLine + " GROUP BY CHRLT";
            sql += Environment.NewLine + "HAVING SUM(DQTY*DDAY)<>0";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                chrlt = row["CHRLT"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return chrlt;
        }

        private string GetPandok(string ispcd, string exdt, string actfg, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            if (ispcd.StartsWith("HI")) return "2"; // 촤영료 전용 코드 판독가산없음.
            if (ispcd.StartsWith("HJ"))
            {
                if (ispcd.Length == 8 && ispcd.EndsWith("4")) return "1"; // 판독가산코드임.
                else return "2";
            }

            // 판독가산하는 행위구분인지 검사한다.
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA88A A (NOLOCK)";
            sql += Environment.NewLine + " WHERE A.MST1CD = 'A' ";
            sql += Environment.NewLine + "   AND A.MST2CD = 'HOSPITAL' ";
            sql += Environment.NewLine + "   AND A.MST3CD = '11'";
            sql += Environment.NewLine + "   AND A.MST4CD = ( SELECT MAX(X.MST4CD) ";
            sql += Environment.NewLine + "                     FROM TA88A X (NOLOCK)";
            sql += Environment.NewLine + "                    WHERE X.MST1CD  = 'A' ";
            sql += Environment.NewLine + "                      AND X.MST2CD  = 'HOSPITAL' ";
            sql += Environment.NewLine + "                      AND X.MST3CD  = '11' ";
            sql += Environment.NewLine + "                      AND X.MST4CD <= '" + exdt + "' ";
            sql += Environment.NewLine + "                   ) ";

            int cnt = 0;
            string fld2qty = "";
            string fld3qty = "";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                cnt++;
                fld2qty = row["FLD2QTY"].ToString();
                fld3qty = row["FLD3QTY"].ToString();
                
                return MetroLib.SqlHelper.BREAK;
            });

            if (cnt > 0)
            {
                if (fld2qty == "1")
                {
                    var items = fld3qty.Split(',').Select(s => s.Trim());
                    return items.Contains(actfg) == true ? "1" : "2";
                }
                else
                {
                    return "2";
                }
            }

            // TA88에서 자료를 찾아본다.
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA88 A (NOLOCK)";
            sql += Environment.NewLine + " WHERE A.MST1CD = 'A' ";
            sql += Environment.NewLine + "   AND A.MST2CD = 'HOSPITAL' ";
            sql += Environment.NewLine + "   AND A.MST3CD = '11'";

            cnt = 0;
            fld2qty = "";
            fld3qty = "";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                cnt++;
                fld2qty = row["FLD2QTY"].ToString();
                fld3qty = row["FLD3QTY"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            if (cnt > 0)
            {
                if (fld2qty == "1")
                {
                    var items = fld3qty.Split(',').Select(s => s.Trim());
                    return items.Contains(actfg) == true ? "1" : "2";
                }
                else
                {
                    return "2";
                }
            }

            return "2";
        }

        private void SetData_TInF(CDataASM049_001 data, string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            data.ClearMe();

            if (data.IOFG == "1")
            {
                data.IPAT_OPAT_TP_CD = "2";
                data.DIAG_DD = data.STEDT;
            }
            else
            {
                data.IPAT_OPAT_TP_CD = "1";
                data.IPAT_DD = data.BDEDT != "" ? data.BDEDT : data.STEDT; // 기본애 BDEDT, 값이 없으면 STEDT
                data.EMRRM_PASS_YN = "2";
                if (data.EMRRM_PASS_YN == "1")
                {
                    data.EMRRM_VST_DD = data.IPAT_DD; // 응급실 내원일
                }
                data.DSCG_DD = data.A04_BEDODT;
            }

            string tTI2F = "TI2F";
            string fBDODT = "BDODT";
            if (data.IOFG == "1")
            {
                tTI2F = "TI1F";
                fBDODT = "EXDATE";
            }

            int ct_count = 0;
            int mri_count = 0;
            int pet_count = 0;
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM " + tTI2F + " (NOLOCK)";
            sql += Environment.NewLine + " WHERE " + fBDODT + "='" + data.BDODT + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + data.QFYCD + "'";
            sql += Environment.NewLine + "   AND JRBY='" + data.JRBY + "'";
            sql += Environment.NewLine + "   AND PID='" + data.PID + "'";
            sql += Environment.NewLine + "   AND UNISQ='" + data.UNISQ + "'";
            sql += Environment.NewLine + "   AND SIMCS='" + data.SIMCS + "'";
            sql += Environment.NewLine + "   AND (ACTFG IN ('05','06','07') OR GRPACT IN ('05','06','07'))";
            sql += Environment.NewLine + "   AND ISNULL(OKCD,'')=''"; // 위탁진료 제외
            sql += Environment.NewLine + " ORDER BY ELINENO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                long seq1 = MetroLib.StrHelper.ToLong(row["SEQ1"].ToString());
                long pos2 = MetroLib.StrHelper.ToLong(row["POS2"].ToString());

                bool isCT = seq1 == 20 && pos2 == 3 || seq1 == 21;
                bool isMRI = seq1 == 20 && pos2 == 4 || seq1 == 22;
                bool isPET = seq1 == 20 && pos2 == 5 || seq1 == 23;

                string bgiho = row["BGIHO"].ToString();
                if (bgiho.StartsWith("HB")) return MetroLib.SqlHelper.CONTINUE; // HB로 시작하면 FULL PACS 이용료임.

                string mafg = row["MAFG"].ToString();
                string prknm = row["PRKNM"].ToString();
                string exdt = row["EXDT"].ToString().Substring(0, 8);

                if (isCT)
                {
                    ct_count++;
                    if (ct_count == 1)
                    {
                        // 최초 한 번
                        data.CT_CTRST_USE_YN = "2";
                    }

                    if (mafg == "1")
                    {
                        data.CT_CTRST_USE_YN = "1"; // 조영제 사용 여부
                        return MetroLib.SqlHelper.CONTINUE;
                    }

                    string pandokYn = "2";
                    if (bgiho.Length == 8 && bgiho.EndsWith("006")) pandokYn = "1";

                    data.CT_MDFEE_CD.Add(bgiho);       // 수가코드
                    data.CT_MDFEE_CD_NM.Add(prknm);    // 검사명
                    data.CT_EXM_EXEC_DT.Add(exdt);    // 검사일시(YYYYMMDDHHMM)
                    data.CT_RD_SDR_DCT_YN.Add(pandokYn);  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                    data.CT_DCT_RST_DD.Add("");     // 판독 완료일(YYYYMMDD)
                }
                else if (isMRI)
                {
                    mri_count++;
                    if (mri_count == 1)
                    {
                        // 최초 한 번
                        data.MRI_CTRST_USE_YN = "2";
                    }

                    if (mafg == "1")
                    {
                        data.MRI_CTRST_USE_YN = "1"; // 조영제 사용 여부
                        return MetroLib.SqlHelper.CONTINUE;
                    }

                    string pandokYn = "2";
                    if (bgiho.Length == 8 && bgiho.EndsWith("006")) pandokYn = "1";

                    data.MRI_MDFEE_CD.Add(bgiho);       // 수가코드
                    data.MRI_MDFEE_CD_NM.Add(prknm);    // 검사명
                    data.MRI_EXM_EXEC_DT.Add(exdt);    // 검사일시(YYYYMMDDHHMM)
                    data.MRI_RD_SDR_DCT_YN.Add(pandokYn);  // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                    data.MRI_DCT_RST_DD.Add("");     // 판독 완료일(YYYYMMDD)
                }
                else if (isPET)
                {
                    pet_count++;
                    if (pet_count == 1)
                    {
                        // 최초 한 번
                    }

                    data.PET_MDFEE_CD.Add(bgiho);       // 수가코드
                    data.PET_MDFEE_CD_NM.Add(prknm);    // 검사명
                    data.PET_EXM_EXEC_DT.Add(exdt);    // 검사일시(YYYYMMDDHHMM)
                }

                return MetroLib.SqlHelper.CONTINUE;
            });


        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CODE902E.NET
{
    class CA02Up
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string m_OutFile;
        private Dictionary<string, string> m_jong_dic = new Dictionary<string, string>();
        private Dictionary<string, string> m_refcd_jong_dic = new Dictionary<string, string>();

        private string m_hanfg = "";
        private string m_dentfg = "";
        private string m_times100fg = "";
        private string m_9bfg = "";
        private string m_9btimes = "";
        private string m_timesfg = "";
        private Dictionary<string, double> m_actfg_times = new Dictionary<string,double>();
        private List<string> m_prt = new List<string>();

        private string m_ver = "0.9";
        private string m_pgm_pos = "";

        public void main()
        {
            try
            {
                string strConn = GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    // 한방여부
                    sql = "SELECT FLD2QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='146'";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_hanfg = reader["FLD2QTY"].ToString();
                        return true;
                    });
                    // 치과여부
                    sql = "SELECT FLD2QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='164'";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_dentfg = reader["FLD2QTY"].ToString();
                        return true;
                    });
                    // 100/100 코드 일반수가 배율 적용 방법
                    sql = "SELECT FLD2QTY FROM TA972 WHERE PRJCD='DCD' AND FRMNM='CODE902E' AND SEQ=3";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_times100fg = reader["FLD2QTY"].ToString();
                        return true;
                    });
                    // 수탁검사(행위구분=9B) 일반수가 배율 적용방법
                    sql = "SELECT FLD2QTY, FLD3QTY FROM TA972 WHERE PRJCD='DCD' AND FRMNM='CODE902E' AND SEQ=1";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_9bfg = reader["FLD2QTY"].ToString();
                        m_9btimes = reader["FLD3QTY"].ToString();
                        return true;
                    });
                    // 행위구분이 가산율을 적요하지 않는 수가의 일반수가 배율 적용 방법
                    sql = "SELECT FLD2QTY FROM TA972 WHERE PRJCD='DCD' AND FRMNM='CODE902E' AND SEQ=2";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_timesfg = reader["FLD2QTY"].ToString();
                        return true;
                    });
                    // 행위구분별 일반수가 배율 
                    sql = "SELECT MST3CD,FLD2QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='GPAMTRT'";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_actfg_times.Add(reader["MST3CD"].ToString().ToUpper(), ToDouble(reader["FLD2QTY"].ToString()));
                        return true;
                    });
                    // 일반수가 배율 규칙
                    sql = "";
                    sql += Environment.NewLine + "SELECT CASE WHEN MST2CD='APRT' THEN '행위'";
                    sql += Environment.NewLine + "       ELSE CASE WHEN MST2CD='MPRT' THEN '약'";
                    sql += Environment.NewLine + "            ELSE '재료'";
                    sql += Environment.NewLine + "            END";
                    sql += Environment.NewLine + "       END MST2CD,FLD1CD,FLD2CD,FLD1QTY";
                    sql += Environment.NewLine + "  FROM TA88";
                    sql += Environment.NewLine + " WHERE MST1CD='A' AND MST2CD IN ('MPRT','GPRT','APRT')";
                    sql += Environment.NewLine + " ORDER BY MST2CD, MST3CD";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_prt.Add(reader["MST2CD"].ToString() + "," + reader["FLD1CD"].ToString() + "," + reader["FLD2CD"].ToString() + "," + reader["FLD1QTY"].ToString());
                        return true;
                    });

                    string currentPath = System.Reflection.Assembly.GetExecutingAssembly().Location; // 이 실행파일이 구동되는 FULL PATH
                    string currentPathName = System.IO.Path.GetDirectoryName(currentPath); // 이 파일이 구동되는 폴더

                    m_OutFile = currentPathName + Path.DirectorySeparatorChar + "edi.out";
                    WriteLog("작업시작", false);
                    WriteLog("버전 = " + m_ver);

                    // 작업할 파일을 읽는다. 파일명음 edi.in 임.
                    string[] lines = File.ReadAllLines(currentPathName + Path.DirectorySeparatorChar + "edi.in");
                    foreach (string line in lines)
                    {
                        if (line != "")
                        {
                            m_pgm_pos = "1";

                            string[] fields = line.Split(',');
                            string ti09_table = fields[0];
                            string pcode = fields[1];
                            string adtdt = fields[2];
                            string gubun = fields[3];

                            WriteLog(ti09_table + "," + pcode + "," + adtdt + "," + gubun);

                            // ti09를 읽는다.
                            if (ti09_table.Split('=')[1] == "TI09")
                            {
                                m_pgm_pos = "2";
                                read_ti09(ti09_table.Split('=')[1], pcode.Split('=')[1], adtdt.Split('=')[1], gubun.Split('=')[1], conn);
                            }
                            else if (ti09_table.Split('=')[1] == "TI09_JABO")
                            {
                                read_ti09_jabo(ti09_table.Split('=')[1], pcode.Split('=')[1], adtdt.Split('=')[1], gubun.Split('=')[1], conn);
                            }
                        }
                        m_pgm_pos = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string currentPath = System.Reflection.Assembly.GetExecutingAssembly().Location; // 이 실행파일이 구동되는 FULL PATH
                string currentPathName = System.IO.Path.GetDirectoryName(currentPath); // 이 파일이 구동되는 폴더

                string outFile = currentPathName + Path.DirectorySeparatorChar + "edi.out";
                File.AppendAllText(outFile, DateTime.Now.ToString() + " ***** 오류발생(" + m_pgm_pos + ") *****" + Environment.NewLine);
                File.AppendAllText(outFile, DateTime.Now.ToString() + " " + ex.Message + Environment.NewLine);
                if (ex.InnerException != null)
                {
                    File.AppendAllText(outFile, DateTime.Now.ToString() + "   " + ex.InnerException.Message + Environment.NewLine);
                }
            }
        }

        private void read_ti09(string p_ti09_table, string p_pcode, string p_adtdt, string p_gubun, OleDbConnection p_conn)
        {
            m_pgm_pos = "3";
            List<CTI09> list = new List<CTI09>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT PCODE,ADTDT,GUBUN,KUMAK1,KUMAK2,KUMAK3,KUMAK6,BUNCD,EDISCORE,MAFG,PCODENM,SPEC";
            sql += Environment.NewLine + "  FROM TI09";
            sql += Environment.NewLine + " WHERE PCODE='" + p_pcode + "'";
            sql += Environment.NewLine + "   AND ADTDT='" + p_adtdt + "'";
            sql += Environment.NewLine + "   AND GUBUN='" + p_gubun + "'";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTI09 ti09 = new CTI09();
                ti09.Clear();

                ti09.PCODE = reader["PCODE"].ToString();
                ti09.ADTDT = reader["ADTDT"].ToString();
                ti09.GUBUN = reader["GUBUN"].ToString();
                ti09.KUMAK1 = ToLong(reader["KUMAK1"].ToString());
                ti09.KUMAK2 = ToLong(reader["KUMAK2"].ToString());
                ti09.KUMAK3 = ToLong(reader["KUMAK3"].ToString());
                ti09.KUMAK6 = ToLong(reader["KUMAK6"].ToString());
                ti09.BUNCD = reader["BUNCD"].ToString();
                ti09.EDISCORE = reader["EDISCORE"].ToString();
                ti09.MAFG = reader["MAFG"].ToString();
                ti09.PCODENM = reader["PCODENM"].ToString();
                ti09.SPEC = reader["SPEC"].ToString();

                list.Add(ti09);

                return true;
            });

            m_pgm_pos = "4";
            if (list.Count < 1)
            {
                WriteLog("  TI09 없음");
            }

            m_pgm_pos = "5";
            foreach (CTI09 ti09 in list)
            {
                read_ta02(ti09, p_conn);
            }
        }

        private void read_ta02(CTI09 ti09, OleDbConnection p_conn)
        {
            m_pgm_pos = "6";
            if (ti09.GUBUN != "1" && ti09.GUBUN != "9")
            {
                WriteLog("  수가가 아님. 작업제외");
                return;
            }
            if (ti09.BUNCD == "삭제" || ti09.BUNCD == "급여정지")
            {
                WriteLog("  " + ti09.BUNCD + " 되었음. 작업제외");
                return;
            }

            m_pgm_pos = "7";
            List<CTA02> list = new List<CTA02>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A02.PRICD,A02.CREDT,A02.ISPCD,A02.ISPCD_SANJE,A02.ISPCD_JABO,A02.JABOEDIFG";
            sql += Environment.NewLine + "     , A02.IPAMT,A02.IPAMT_DENT,A02.NPAMT,A02.NPAMT_DENT,A02.DPAMT,A02.DPAMT_DENT,A02.CPAMT,A02.CPAMT_DENT,A02.GPAMT,A02.GPAMT_DENT";
            sql += Environment.NewLine + "     , A02.GUBUN,A02.REFCD,A02.EXPDT,A02.IALWF,A02.NALWF,A02.DALWF,A02.CALWF";
            sql += Environment.NewLine + "     , A02.GPFIX,A02.ACTFG";
            sql += Environment.NewLine + "     , A02.EDIAMT,A02.EDIAMT_DENT,A02.EDIAMT_SANJE,A02.EDIAMT_DENT_SANJE,A02.EDIAMT_JABO,A02.EDIAMT_DENT_JABO";
            sql += Environment.NewLine + "     , A02.JUMSU,A02.JUMSU_SANJE,A02.JUMSU_JABO";
            sql += Environment.NewLine + "     , A02.MCHVAL,A02.MCHVAL_SANJE,A02.MCHVAL_JABO";
            sql += Environment.NewLine + "  FROM TA02 A02";
            sql += Environment.NewLine + " WHERE (A02.ISPCD='" + ti09.PCODE + "'";
            sql += Environment.NewLine + "        OR A02.ISPCD_SANJE='" + ti09.PCODE + "'";
            sql += Environment.NewLine + "        OR A02.ISPCD_JABO='" + ti09.PCODE + "' AND A02.JABOEDIFG='Y'";
            sql += Environment.NewLine + "       )";
            sql += Environment.NewLine + "   AND A02.GUBUN='" + ti09.GUBUN + "'";
            sql += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A02.PRICD AND X.CREDT<='" + ti09.ADTDT + "')";
            sql += Environment.NewLine + " ORDER BY A02.PRICD,A02.CREDT";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTA02 ta02 = new CTA02();

                ta02.PRICD = reader["PRICD"].ToString();
                ta02.CREDT = reader["CREDT"].ToString();
                ta02.ISPCD = reader["ISPCD"].ToString();
                ta02.ISPCD_SANJE = reader["ISPCD_SANJE"].ToString();
                ta02.ISPCD_JABO = reader["ISPCD_JABO"].ToString();
                ta02.JABOEDIFG = reader["JABOEDIFG"].ToString().ToUpper();
                ta02.IPAMT = ToLong(reader["IPAMT"].ToString());
                ta02.IPAMT_DENT = ToLong(reader["IPAMT_DENT"].ToString());
                ta02.NPAMT = ToLong(reader["NPAMT"].ToString());
                ta02.NPAMT_DENT = ToLong(reader["NPAMT_DENT"].ToString());
                ta02.DPAMT = ToLong(reader["DPAMT"].ToString());
                ta02.DPAMT_DENT = ToLong(reader["DPAMT_DENT"].ToString());
                ta02.CPAMT = ToLong(reader["CPAMT"].ToString());
                ta02.CPAMT_DENT = ToLong(reader["CPAMT_DENT"].ToString());
                ta02.GPAMT = ToLong(reader["GPAMT"].ToString());
                ta02.GPAMT_DENT = ToLong(reader["GPAMT_DENT"].ToString());
                ta02.GUBUN = reader["GUBUN"].ToString();
                ta02.REFCD = reader["REFCD"].ToString();
                ta02.EXPDT = reader["EXPDT"].ToString();
                ta02.IALWF = reader["IALWF"].ToString();
                ta02.NALWF = reader["NALWF"].ToString();
                ta02.DALWF = reader["DALWF"].ToString();
                ta02.CALWF = reader["CALWF"].ToString();
                ta02.GPFIX = reader["GPFIX"].ToString();
                ta02.ACTFG = reader["ACTFG"].ToString();

                ta02.EDIAMT = ToLong(reader["EDIAMT"].ToString());
                ta02.EDIAMT_DENT = ToLong(reader["EDIAMT_DENT"].ToString());
                ta02.EDIAMT_SANJE = ToLong(reader["EDIAMT_SANJE"].ToString());
                ta02.EDIAMT_DENT_SANJE = ToLong(reader["EDIAMT_DENT_SANJE"].ToString());
                ta02.EDIAMT_JABO = ToLong(reader["EDIAMT_JABO"].ToString());
                ta02.EDIAMT_DENT_JABO = ToLong(reader["EDIAMT_DENT_JABO"].ToString());
                ta02.JUMSU = reader["JUMSU"].ToString();
                ta02.JUMSU_SANJE = reader["JUMSU_SANJE"].ToString();
                ta02.JUMSU_JABO = reader["JUMSU_JABO"].ToString();
                ta02.MCHVAL = reader["MCHVAL"].ToString();
                ta02.MCHVAL_SANJE = reader["MCHVAL_SANJE"].ToString();
                ta02.MCHVAL_JABO = reader["MCHVAL_JABO"].ToString();

                if (ta02.JABOEDIFG == "Y" && ta02.ISPCD_JABO == "") ta02.JABOEDIFG = ""; // 자보EDI 코드가 없으면 JABOEDIFG를 지운다.

                list.Add(ta02);

                return true;
            });

            m_pgm_pos = "8";
            if (list.Count < 1)
            {
                WriteLog("  원내코드 없음");
                WriteLog("  = 생성 시도 =");
                make_new_pricd(ti09, p_conn);
            }

            m_pgm_pos = "9";
            foreach (CTA02 ta02 in list)
            {
                make_new_amt(ti09, ta02, p_conn);
            }

        }

        private void make_new_amt(CTI09 ti09, CTA02 ta02, OleDbConnection p_conn)
        {
            m_pgm_pos = "10";
            // 금액이 0원이면 자동 업데이트에서 제외시킨다.
            if (m_dentfg == "1")
            {
                if (ta02.IPAMT == 0 && ta02.IPAMT_DENT == 0 && ta02.NPAMT == 0 && ta02.NPAMT_DENT == 0 && ta02.DPAMT == 0 && ta02.DPAMT_DENT == 0 && ta02.CPAMT == 0 && ta02.CPAMT_DENT == 0)
                {
                    WriteLog("  수가코드 = " + ta02.PRICD + ", 수가가 0원임. 작업제외");
                    return;
                }
            }
            else
            {
                if (ta02.IPAMT == 0 && ta02.NPAMT == 0 && ta02.DPAMT == 0 && ta02.CPAMT == 0)
                {
                    WriteLog("  수가코드 = " + ta02.PRICD + ", 수가가 0원임. 작업제외");
                    return;
                }
            }
            // 폐기된 코드 제외
            if (ta02.EXPDT != "")
            {
                WriteLog("  수가코드 = " + ta02.PRICD + ", 폐기(" + ta02.EXPDT + ")된 코드임. 작업제외");
                return;
            }
            // 비급여,비보험이면 제외
            if ((ta02.IALWF == "1" || ta02.IALWF == "2") && (ta02.NALWF == "1" || ta02.NALWF == "2") && (ta02.DALWF == "1" || ta02.DALWF == "2") && (ta02.CALWF == "1" || ta02.CALWF == "2"))
            {
                WriteLog("  수가코드 = " + ta02.PRICD + ", 비급여(보험=" + ta02.IALWF + ",보호=" + ta02.NALWF + ",산재=" + ta02.DALWF + ",자보=" + ta02.CALWF + ") 코드임. 작업제외");
                return;
            }

            m_pgm_pos = "11";
            // 신규적용일
            string new_credt = ti09.ADTDT;

            m_pgm_pos = "12";
            // 병원 종별
            string hosp_jong = GetJong(ti09.ADTDT);
            string hosp_jong_ref = ta02.REFCD == "" ? "" : GetRefJong(ta02.REFCD); // 위탁기관종별
            if (hosp_jong_ref == "") hosp_jong_ref = hosp_jong;

            // EDI 단가
            long new_ediamt = GetNewEdiamt(ti09, ta02.REFCD, hosp_jong, hosp_jong_ref);
            long new_ediamt_dent = GetNewEdiamtDent(ti09, ta02.REFCD, hosp_jong_ref);

            if (m_dentfg == "1")
            {
                if (new_ediamt == 0 && new_ediamt_dent == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }
            else
            {
                if (new_ediamt == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }

            // 자격별 EDI단가
            long new_ediamt_bohum = 0;
            long new_ediamt_dent_bohum = 0;
            long new_ediamt_sanje = 0;
            long new_ediamt_dent_sanje = 0;
            long new_ediamt_jabo = 0;
            long new_ediamt_dent_jabo = 0;

            // 점수
            string new_jumsu = "";
            string new_jumsu_sanje = "";
            string new_jumsu_jabo = "";

            // EDI 금액(GUBUN == 9 : 한방수가)
            if (ta02.ISPCD == ti09.PCODE)
            {
                new_ediamt_bohum = new_ediamt;
                new_ediamt_dent_bohum = new_ediamt_dent;
                new_jumsu = ti09.EDISCORE;
            }
            else
            {
                new_ediamt_bohum = ta02.EDIAMT;
                new_ediamt_dent_bohum = ta02.EDIAMT_DENT;
                new_jumsu = ta02.JUMSU;
            }
            // 산재용 EDI 금액
            if (ta02.ISPCD_SANJE == ti09.PCODE)
            {
                new_ediamt_sanje = new_ediamt;
                new_ediamt_dent_sanje = new_ediamt_dent;
                new_jumsu_sanje = ti09.EDISCORE;
            }
            else
            {
                new_ediamt_sanje = ta02.EDIAMT_SANJE;
                new_ediamt_dent_sanje = ta02.EDIAMT_DENT_SANJE;
                new_jumsu_sanje = ta02.JUMSU_SANJE;
            }
            // 자보용 EDI 금액
            if (ta02.ISPCD_JABO == ti09.PCODE && ta02.JABOEDIFG == "Y")
            {
                new_ediamt_jabo = new_ediamt;
                new_ediamt_dent_jabo = new_ediamt_dent;
                new_jumsu_jabo = ti09.EDISCORE;
            }
            else
            {
                new_ediamt_jabo = ta02.EDIAMT_JABO;
                new_ediamt_dent_jabo = ta02.EDIAMT_DENT_JABO;
                new_jumsu_jabo = ta02.JUMSU_JABO;
            }

            // 환산치
            double mchval = 0;
            double mchval_sanje = 0;
            double mchval_jabo = 0;

            double.TryParse(ta02.MCHVAL, out mchval);
            double.TryParse(ta02.MCHVAL_SANJE, out mchval_sanje);
            double.TryParse(ta02.MCHVAL_JABO, out mchval_jabo);

            if (mchval == 0) mchval = 1;
            if (mchval_sanje == 0) mchval_sanje = 1;
            if (mchval_jabo == 0) mchval_jabo = 1;

            // 자격별 환산치 결정
            if (ta02.ISPCD_SANJE == "") mchval_sanje = 1; // EDI코드(산재)가 없으면 환산치도 없앤다.
            if (ta02.ISPCD_JABO == "") mchval_jabo = 1; // EDI코드(자보)가 없으면 환산치도 없앤다.

            // 자격별 단가
            long new_ipamt = 0;
            long new_ipamt_dent = 0;
            long new_npamt = 0;
            long new_npamt_dent = 0;
            long new_dpamt = 0;
            long new_dpamt_dent = 0;
            long new_cpamt = 0;
            long new_cpamt_dent = 0;
            long new_gpamt = 0;
            long new_gpamt_dent = 0;

            // 보험금액
            if (ta02.IALWF == "1" || ta02.IALWF == "2" || ta02.ISPCD != ti09.PCODE)
            {
                // ① 코드가 비급여(비보험)이거나 
                // ② 현재 작업하고 있는 EDI코드가 아니면
                new_ipamt = ta02.IPAMT;
                new_ipamt_dent = ta02.IPAMT_DENT;
            }
            else
            {
                // ① 단가가 0원으로 되어있으면 유지
                new_ipamt = ta02.IPAMT == 0 ? 0 : MyRound((double)new_ediamt_bohum * mchval);
                new_ipamt_dent = ta02.IPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_bohum * mchval);
            }
            
            // 보호금액
            if (ta02.NALWF == "1" || ta02.NALWF == "2" || ta02.ISPCD != ti09.PCODE)
            {
                // ① 코드가 비급여(비보험)이거나 
                // ② 현재 작업하고 있는 EDI코드가 아니면
                new_npamt = ta02.NPAMT;
                new_npamt_dent = ta02.NPAMT_DENT;
            }
            else
            {
                // ① 단가가 0원으로 되어있으면 유지
                new_npamt = ta02.NPAMT == 0 ? 0 : MyRound((double)new_ediamt_bohum * mchval);
                new_npamt_dent = ta02.NPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_bohum * mchval);
            }

            // 산재금액
            if (ta02.DALWF == "1" || ta02.DALWF == "2" || (ta02.ISPCD_SANJE != "" && ta02.ISPCD_SANJE != ti09.PCODE))
            {
                // ① 코드가 비급여(비보험)이거나 
                // ② 산재 EDI 코드를 등록했는데 현재 작업하고 있는 EDI코드가 아니면
                new_dpamt = ta02.DPAMT;
                new_dpamt_dent = ta02.DPAMT_DENT;
            }
            else if (ta02.ISPCD_SANJE != "" && ta02.ISPCD_SANJE == ti09.PCODE)
            {
                // ① 단가가 0원으로 되어있으면 유지
                // ② 산재 EDI 코드를 등록했는데 현재 작업하고 있는 EDI코드면
                new_dpamt = ta02.DPAMT == 0 ? 0 : MyRound((double)new_ediamt_sanje * mchval_sanje);
                new_dpamt_dent = ta02.DPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_sanje * mchval_sanje);
            }
            else
            {
                // ① 단가가 0원으로 되어있으면 유지
                // ② 산재 EDI 코드를 등록하지 않았으면 보험 EDI 금액으로 작업
                new_dpamt = ta02.DPAMT == 0 ? 0 : MyRound((double)new_ediamt_bohum * mchval);
                new_dpamt_dent = ta02.DPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_bohum * mchval);
            }

            // 자보금액
            if (ta02.CALWF == "1" || ta02.CALWF == "2" || (ta02.ISPCD_JABO != "" && (ta02.ISPCD_JABO != ti09.PCODE || ta02.JABOEDIFG != "Y")))
            {
                // ① 코드가 비급여(비보험)이거나 
                // ② 자보 EDI 코드를 보험과 다르게 사용하는데 현재 작업하고 있는 EDI코드가 아니거나
                // ③ 금액을 TI09_JABO에서 구해야하는 경우
                new_cpamt = ta02.CPAMT;
                new_cpamt_dent = ta02.CPAMT_DENT;
            }
            else if (ta02.ISPCD_JABO != "" && ta02.ISPCD_JABO == ti09.PCODE && ta02.JABOEDIFG == "Y")
            {
                // ① 단가가 0원으로 되어있으면 유지
                // ② 자보 EDI 코드를 등록했는데 현재 작업하고 있는 EDI코드면
                new_cpamt = ta02.CPAMT == 0 ? 0 : MyRound((double)new_ediamt_jabo * mchval_jabo);
                new_cpamt_dent = ta02.CPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_jabo * mchval_jabo);
            }
            else
            {
                // ① 단가가 0원으로 되어있으면 유지
                // ② 자보 EDI 코드를 등록하지 않았으면 보험 EDI 금액으로 작업
                new_cpamt = ta02.CPAMT == 0 ? 0 : MyRound((double)new_ediamt_bohum * mchval);
                new_cpamt_dent = ta02.CPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_bohum * mchval);
            }

            // 일반수가
            double times = 1;
            double times_dent = 1;
            if (ta02.GPFIX == "1" || ta02.IALWF == "1" || ta02.IALWF == "2")
            {
                // 일반수가 고정이면 이전 금액을 그대로 사용한다.
                // 보험이 비급여,비보험이면 기존의 일반수가를 그대로 사용한다.
                new_gpamt = ta02.GPAMT;
                new_gpamt_dent = ta02.GPAMT_DENT;
            }
            else
            {
                // 환산치를 적용한 후 일반수가 배율을 적용한다.
                long new_amt = MyRound((double)new_ediamt_bohum * mchval);
                long new_amt_dent = MyRound((double)new_ediamt_dent_bohum * mchval);

                times = GetTimes(ta02.PRICD, ta02.ACTFG, new_amt, ta02.GUBUN, ta02.IALWF);
                times_dent = GetTimes(ta02.PRICD, ta02.ACTFG, new_amt_dent, ta02.GUBUN, ta02.IALWF);

                new_gpamt = ta02.GPAMT == 0 ? ta02.GPAMT : MyRound((double)new_amt * times);
                new_gpamt_dent = ta02.GPAMT_DENT == 0 ? ta02.GPAMT_DENT : MyRound((double)new_amt_dent * times_dent);
            }


            WriteLog("  수가코드 = " + ta02.PRICD + ", 행위구분 = " + ta02.ACTFG + ", EDI = " + ta02.ISPCD + ", EDI산재 = " + ta02.ISPCD_SANJE + ", EDI자보 = " + ta02.ISPCD_JABO);
            WriteLog("    적용일자 = " + ta02.CREDT + "->" + new_credt);

            WriteLog("    EDI금액 = " + ta02.EDIAMT + "->" + new_ediamt_bohum + ", EDI금액(치과) = " + ta02.EDIAMT_DENT + "->" + new_ediamt_dent_bohum);
            WriteLog("    EDI금액(산재) = " + ta02.EDIAMT_SANJE + "->" + new_ediamt_sanje + ", EDI금액(산재)(치과) = " + ta02.EDIAMT_DENT_SANJE + "->" + new_ediamt_dent_sanje);
            WriteLog("    EDI금액(자보) = " + ta02.EDIAMT_JABO + "->" + new_ediamt_jabo + ", EDI금액(자보)(치과) = " + ta02.EDIAMT_DENT_JABO + "->" + new_ediamt_dent_jabo);

            WriteLog("    건보금액 = " + ta02.IPAMT + "->" + new_ipamt + ", 건보금액(치과) = " + ta02.IPAMT_DENT + "->" + new_ipamt_dent + ", 급여구분 = " + ta02.IALWF);
            WriteLog("    보호금액 = " + ta02.NPAMT + "->" + new_npamt + ", 보호금액(치과) = " + ta02.NPAMT_DENT + "->" + new_npamt_dent + ", 급여구분 = " + ta02.NALWF);
            WriteLog("    산재금액 = " + ta02.DPAMT + "->" + new_dpamt + ", 산재금액(치과) = " + ta02.DPAMT_DENT + "->" + new_dpamt_dent + ", 급여구분 = " + ta02.DALWF);
            WriteLog("    자보금액 = " + ta02.CPAMT + "->" + new_cpamt + ", 자보금액(치과) = " + ta02.CPAMT_DENT + "->" + new_cpamt_dent + ", 급여구분 = " + ta02.CALWF);
            WriteLog("    일반금액 = " + ta02.GPAMT + "->" + new_gpamt + ", 일반금액(치과) = " + ta02.GPAMT_DENT + "->" + new_gpamt_dent + (ta02.GPFIX == "1" ? ", 일반수가고정" : ", 일반배율 = " + times + ", 일반배율(치과) = " + times_dent));

            WriteLog("    EDI점수 = " + ta02.JUMSU + "->" + new_jumsu + ", EDI점수(산재) = " + ta02.JUMSU_SANJE + "->" + new_jumsu_sanje + ", EDI점수(자보) = " + ta02.JUMSU_JABO + "->" + new_jumsu_jabo);
            WriteLog("    환산치 = " + mchval + ", 환산치(산재) = " + mchval_sanje + ", 환산치(자보) = " + mchval_jabo);


            // PRICD, NEW_CREDT 레크드가 없으면 이전 적용일자로로 먼저 동일하게 만든다.
            //                           있으면 TA02_HX를 만든다.
            long ta02_cnt = GetTA02Cnt(ta02.PRICD, new_credt, p_conn);
            if (ta02_cnt > 0)
            {
                TA02Hx(ta02.PRICD, new_credt, p_conn);
            }
            else
            {
                CopyTA02(ta02.PRICD, ta02.CREDT, new_credt, p_conn);
            }

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "update ta02";
            sql += Environment.NewLine + "   set gpamt=" + new_gpamt + "";
            sql += Environment.NewLine + "     , ipamt=" + new_ipamt + "";
            sql += Environment.NewLine + "     , npamt=" + new_npamt + "";
            sql += Environment.NewLine + "     , cpamt=" + new_cpamt + "";
            sql += Environment.NewLine + "     , dpamt=" + new_dpamt + "";
            sql += Environment.NewLine + "     , gpamt_dent=" + new_gpamt_dent + "";
            sql += Environment.NewLine + "     , ipamt_dent=" + new_ipamt_dent + "";
            sql += Environment.NewLine + "     , npamt_dent=" + new_npamt_dent + "";
            sql += Environment.NewLine + "     , cpamt_dent=" + new_cpamt_dent + "";
            sql += Environment.NewLine + "     , dpamt_dent=" + new_dpamt_dent + "";

            sql += Environment.NewLine + "     , ediamt=" + new_ediamt_bohum + "";
            sql += Environment.NewLine + "     , ediamt_dent=" + new_ediamt_dent_bohum + "";
            sql += Environment.NewLine + "     , jumsu='" + new_jumsu + "'";
            sql += Environment.NewLine + "     , ediamt_sanje=" + new_ediamt_sanje + "";
            sql += Environment.NewLine + "     , ediamt_dent_sanje=" + new_ediamt_dent_sanje + "";
            sql += Environment.NewLine + "     , jumsu_sanje='" + new_jumsu_sanje + "'";
            sql += Environment.NewLine + "     , ediamt_jabo=" + new_ediamt_jabo + "";
            sql += Environment.NewLine + "     , ediamt_dent_jabo=" + new_ediamt_dent_jabo + "";
            sql += Environment.NewLine + "     , jumsu_jabo='" + new_jumsu_jabo + "'";

            sql += Environment.NewLine + "     , entdt=CONVERT(VARCHAR,GETDATE(),112)";
            sql += Environment.NewLine + "     , empid='AUTO'";
            sql += Environment.NewLine + "     , sysdt=CONVERT(VARCHAR,GETDATE(),112)";
            sql += Environment.NewLine + "     , systm=REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','')";
            sql += Environment.NewLine + " where pricd='" + ta02.PRICD + "'";
            sql += Environment.NewLine + "   and credt='" + new_credt + "'";

            ExecuteSql(sql, p_conn);

        }

        // ------------------------------------------------------
        // HOSJONG 1.전문종합병원 2.종합병원 3.병원 4.의원 5.약국
        // ------------------------------------------------------
        // GUBUN   1.수가 2.재료 3.약가 8.한방약가 9.한방수가
        // ------------------------------------------------------
        // KUMAK1 : 병원급이상단가
        // KUKAK2 : 의원단가,
        // KUMAK3 : 치과병·의원단가
        // KUMAK4 : 보건기관단가
        // KUMAK5 : 조산원단가
        // KUMAK6 : 한방병·의원단가
        // ------------------------------------------------------

        private long GetNewEdiamt(CTI09 ti09, string refcd, string hosp_jong, string hosp_jong_ref)
        {
            long kumak = 0;
            if (ti09.GUBUN != "9")
            {
                if (refcd != "") hosp_jong = hosp_jong_ref; //2021.01.22 PHH
                kumak = hosp_jong == "1" || hosp_jong == "2" || hosp_jong == "3" ? ti09.KUMAK1 : ti09.KUMAK2;
            }
            else
            {
                kumak = m_hanfg == "1" ? ti09.KUMAK6 : ti09.KUMAK1;
            }
            return kumak;
        }

        private long GetNewEdiamtDent(CTI09 ti09, string refcd, string hosp_jong_ref)
        {
            long kumak = 0;
            if (refcd != "")
            {
                kumak = hosp_jong_ref == "1" || hosp_jong_ref == "2" || hosp_jong_ref == "3" ? ti09.KUMAK1 : ti09.KUMAK2;
            }
            else
            {
                kumak = ti09.GUBUN == "2" || ti09.GUBUN == "3" || ti09.GUBUN == "8" ? ti09.KUMAK1 : ti09.KUMAK3;
            }
            return kumak;
        }


        // ------------------------------------------------------------------------------------------------------------

        private void read_ti09_jabo(string p_ti09_table, string p_pcode, string p_adtdt, string p_gubun, OleDbConnection p_conn)
        {
            List<CTI09> list = new List<CTI09>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT PCODE,ADTDT,GUBUN,KUMAK1,KUMAK2,KUMAK3,KUMAK6,BUNCD,EDISCORE";
            sql += Environment.NewLine + "  FROM TI09_JABO";
            sql += Environment.NewLine + " WHERE PCODE='" + p_pcode + "'";
            sql += Environment.NewLine + "   AND ADTDT='" + p_adtdt + "'";
            sql += Environment.NewLine + "   AND GUBUN='" + p_gubun + "'";
            sql += Environment.NewLine + "   AND JABOFG<>'2'";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTI09 ti09_jabo = new CTI09();
                ti09_jabo.Clear();

                ti09_jabo.PCODE = reader["PCODE"].ToString();
                ti09_jabo.ADTDT = reader["ADTDT"].ToString();
                ti09_jabo.GUBUN = reader["GUBUN"].ToString();
                ti09_jabo.KUMAK1 = ToLong(reader["KUMAK1"].ToString());
                ti09_jabo.KUMAK2 = ToLong(reader["KUMAK2"].ToString());
                ti09_jabo.KUMAK3 = ToLong(reader["KUMAK3"].ToString());
                ti09_jabo.KUMAK6 = ToLong(reader["KUMAK6"].ToString());
                ti09_jabo.BUNCD = reader["BUNCD"].ToString();
                ti09_jabo.EDISCORE = reader["EDISCORE"].ToString();

                list.Add(ti09_jabo);

                return true;
            });

            if (list.Count < 1)
            {
                WriteLog("  TI09_JABO 없음");
            }

            foreach (CTI09 ti09_jabo in list)
            {
                read_ta02_jabo(ti09_jabo, p_conn);
            }
        }

        private void read_ta02_jabo(CTI09 ti09_jabo, OleDbConnection p_conn)
        {
            if (ti09_jabo.GUBUN != "1" && ti09_jabo.GUBUN != "9")
            {
                WriteLog("  수가가 아님. 작업제외");
                return;
            }
            if (ti09_jabo.BUNCD == "삭제" || ti09_jabo.BUNCD == "급여정지")
            {
                WriteLog("  " + ti09_jabo.BUNCD + " 되었음. 작업제외");
                return;
            }

            List<CTA02> list = new List<CTA02>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A02.PRICD,A02.CREDT,A02.ISPCD,A02.ISPCD_SANJE,A02.ISPCD_JABO,A02.JABOEDIFG";
            sql += Environment.NewLine + "     , A02.IPAMT,A02.IPAMT_DENT,A02.NPAMT,A02.NPAMT_DENT,A02.DPAMT,A02.DPAMT_DENT,A02.CPAMT,A02.CPAMT_DENT,A02.GPAMT,A02.GPAMT_DENT";
            sql += Environment.NewLine + "     , A02.GUBUN,A02.REFCD,A02.EXPDT,A02.IALWF,A02.NALWF,A02.DALWF,A02.CALWF";
            sql += Environment.NewLine + "     , A02.GPFIX,A02.ACTFG";
            sql += Environment.NewLine + "     , A02.EDIAMT,A02.EDIAMT_DENT,A02.EDIAMT_SANJE,A02.EDIAMT_DENT_SANJE,A02.EDIAMT_JABO,A02.EDIAMT_DENT_JABO";
            sql += Environment.NewLine + "     , A02.JUMSU,A02.JUMSU_SANJE,A02.JUMSU_JABO";
            sql += Environment.NewLine + "     , A02.MCHVAL,A02.MCHVAL_SANJE,A02.MCHVAL_JABO";
            sql += Environment.NewLine + "  FROM TA02 A02";
            sql += Environment.NewLine + " WHERE (A02.ISPCD='" + ti09_jabo.PCODE + "'";
            sql += Environment.NewLine + "        OR A02.ISPCD_JABO='" + ti09_jabo.PCODE + "' AND ISNULL(A02.JABOEDIFG,'')<>'Y'";
            sql += Environment.NewLine + "       )";
            sql += Environment.NewLine + "   AND A02.GUBUN='" + ti09_jabo.GUBUN + "'";
            sql += Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A02.PRICD AND X.CREDT<='" + ti09_jabo.ADTDT + "')";
            sql += Environment.NewLine + " ORDER BY A02.PRICD,A02.CREDT";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CTA02 ta02_jabo = new CTA02();

                ta02_jabo.PRICD = reader["PRICD"].ToString();
                ta02_jabo.CREDT = reader["CREDT"].ToString();
                ta02_jabo.ISPCD = reader["ISPCD"].ToString();
                ta02_jabo.ISPCD_SANJE = reader["ISPCD_SANJE"].ToString();
                ta02_jabo.ISPCD_JABO = reader["ISPCD_JABO"].ToString();
                ta02_jabo.JABOEDIFG = reader["JABOEDIFG"].ToString().ToUpper();
                ta02_jabo.IPAMT = ToLong(reader["IPAMT"].ToString());
                ta02_jabo.IPAMT_DENT = ToLong(reader["IPAMT_DENT"].ToString());
                ta02_jabo.NPAMT = ToLong(reader["NPAMT"].ToString());
                ta02_jabo.NPAMT_DENT = ToLong(reader["NPAMT_DENT"].ToString());
                ta02_jabo.DPAMT = ToLong(reader["DPAMT"].ToString());
                ta02_jabo.DPAMT_DENT = ToLong(reader["DPAMT_DENT"].ToString());
                ta02_jabo.CPAMT = ToLong(reader["CPAMT"].ToString());
                ta02_jabo.CPAMT_DENT = ToLong(reader["CPAMT_DENT"].ToString());
                ta02_jabo.GPAMT = ToLong(reader["GPAMT"].ToString());
                ta02_jabo.GPAMT_DENT = ToLong(reader["GPAMT_DENT"].ToString());
                ta02_jabo.GUBUN = reader["GUBUN"].ToString();
                ta02_jabo.REFCD = reader["REFCD"].ToString();
                ta02_jabo.EXPDT = reader["EXPDT"].ToString();
                ta02_jabo.IALWF = reader["IALWF"].ToString();
                ta02_jabo.NALWF = reader["NALWF"].ToString();
                ta02_jabo.DALWF = reader["DALWF"].ToString();
                ta02_jabo.CALWF = reader["CALWF"].ToString();
                ta02_jabo.GPFIX = reader["GPFIX"].ToString();
                ta02_jabo.ACTFG = reader["ACTFG"].ToString();

                ta02_jabo.EDIAMT = ToLong(reader["EDIAMT"].ToString());
                ta02_jabo.EDIAMT_DENT = ToLong(reader["EDIAMT_DENT"].ToString());
                ta02_jabo.EDIAMT_SANJE = ToLong(reader["EDIAMT_SANJE"].ToString());
                ta02_jabo.EDIAMT_DENT_SANJE = ToLong(reader["EDIAMT_DENT_SANJE"].ToString());
                ta02_jabo.EDIAMT_JABO = ToLong(reader["EDIAMT_JABO"].ToString());
                ta02_jabo.EDIAMT_DENT_JABO = ToLong(reader["EDIAMT_DENT_JABO"].ToString());
                ta02_jabo.JUMSU = reader["JUMSU"].ToString();
                ta02_jabo.JUMSU_SANJE = reader["JUMSU_SANJE"].ToString();
                ta02_jabo.JUMSU_JABO = reader["JUMSU_JABO"].ToString();
                ta02_jabo.MCHVAL = reader["MCHVAL"].ToString();
                ta02_jabo.MCHVAL_SANJE = reader["MCHVAL_SANJE"].ToString();
                ta02_jabo.MCHVAL_JABO = reader["MCHVAL_JABO"].ToString();

                if (ta02_jabo.JABOEDIFG == "Y" && ta02_jabo.ISPCD_JABO == "") ta02_jabo.JABOEDIFG = ""; // 자보EDI 코드가 없으면 JABOEDIFG를 지운다.

                list.Add(ta02_jabo);

                return true;
            });
            

            if (list.Count < 1)
            {
                WriteLog("  원내코드 없음");
            }

            foreach (CTA02 ta02_jabo in list)
            {
                make_new_amt_jabo(ti09_jabo, ta02_jabo, p_conn);
            }
        }

        private void make_new_amt_jabo(CTI09 ti09_jabo, CTA02 ta02_jabo, OleDbConnection p_conn)
        {
            // 금액이 0원이면 자동 업데이트에서 제외시킨다.
            if (m_dentfg == "1")
            {
                if (ta02_jabo.CPAMT == 0 && ta02_jabo.CPAMT_DENT == 0)
                {
                    WriteLog("  수가코드 = " + ta02_jabo.PRICD + ", 수가가 0원임. 작업제외");
                    return;
                }
            }
            else
            {
                if (ta02_jabo.CPAMT == 0)
                {
                    WriteLog("  수가코드 = " + ta02_jabo.PRICD + ", 수가가 0원임. 작업제외");
                    return;
                }
            }
            // 폐기된 코드 제외
            if (ta02_jabo.EXPDT != "")
            {
                WriteLog("  수가코드 = " + ta02_jabo.PRICD + ", 폐기(" + ta02_jabo.EXPDT + ")된 코드임. 작업제외");
                return;
            }
            // 비급여,비보험이면 제외
            if (ta02_jabo.CALWF == "1" || ta02_jabo.CALWF == "2")
            {
                WriteLog("  수가코드 = " + ta02_jabo.PRICD + ", 비급여(자보=" + ta02_jabo.CALWF + ") 코드임. 작업제외");
                return;
            }

            // 신규적용일
            string new_credt = ti09_jabo.ADTDT;

            // 병원 종별
            string hosp_jong = GetJong(ti09_jabo.ADTDT);
            string hosp_jong_ref = ta02_jabo.REFCD == "" ? "" : GetRefJong(ta02_jabo.REFCD); // 위탁기관종별
            if (hosp_jong_ref == "") hosp_jong_ref = hosp_jong;

            // EDI 단가
            long new_ediamt = GetNewEdiamt(ti09_jabo, ta02_jabo.REFCD, hosp_jong, hosp_jong_ref);
            long new_ediamt_dent = GetNewEdiamtDent(ti09_jabo, ta02_jabo.REFCD, hosp_jong_ref);

            if (m_dentfg == "1")
            {
                if (new_ediamt == 0 && new_ediamt_dent == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }
            else
            {
                if (new_ediamt == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }

            long new_ediamt_jabo = 0;
            long new_ediamt_dent_jabo = 0;
            string new_jumsu_jabo = "";

            // ISPCD_JABO에 값이 있고 JABOEDIFG!='Y'이면 TI09_JABO를 사용하고
            // ISPCD_JABO에 값이 없지만 ISPCD에 있는 값이 TI09_JABO에 값이 있으면 TI09_JABO를 사용한다.
            if (ta02_jabo.ISPCD_JABO != "" && ta02_jabo.ISPCD_JABO == ti09_jabo.PCODE && ta02_jabo.JABOEDIFG != "Y" || ta02_jabo.ISPCD == ti09_jabo.PCODE)
            {
                new_ediamt_jabo = new_ediamt;
                new_ediamt_dent_jabo = new_ediamt_dent;
                new_jumsu_jabo = ti09_jabo.EDISCORE;
            }
            else
            {
                new_ediamt_jabo = ta02_jabo.EDIAMT_JABO;
                new_ediamt_dent_jabo = ta02_jabo.EDIAMT_DENT_JABO;
                new_jumsu_jabo = ta02_jabo.JUMSU_JABO;
            }

            // 환산치
            double mchval_jabo = 0;
            double.TryParse(ta02_jabo.MCHVAL_JABO, out mchval_jabo);
            if (mchval_jabo == 0) mchval_jabo = 1;
            if (ta02_jabo.ISPCD_JABO == "") mchval_jabo = 1; // EDI코드(자보)가 없으면 환산치도 없앤다.


            long new_cpamt = 0;
            long new_cpamt_dent = 0;

            // 자보금액
            if (ta02_jabo.CALWF == "1" || ta02_jabo.CALWF == "2")
            {
                // ① 코드가 비급여(비보험)이거나 
                // ② 자보 EDI 코드를 보험과 다르게 사용하는데 현재 작업하고 있는 EDI코드가 아니면
                // ③ 금액을 TI09_JABO에서 구해야하는 경우
                new_cpamt = ta02_jabo.CPAMT;
                new_cpamt_dent = ta02_jabo.CPAMT_DENT;
            }
            else
            {
                // ① 단가가 0원으로 되어있으면 유지
                // ② 자보 EDI 코드를 등록했는데 현재 작업하고 있는 EDI코드면
                new_cpamt = ta02_jabo.CPAMT == 0 ? 0 : MyRound((double)new_ediamt_jabo * mchval_jabo);
                new_cpamt_dent = ta02_jabo.CPAMT_DENT == 0 ? 0 : MyRound((double)new_ediamt_dent_jabo * mchval_jabo);
            }

            WriteLog("  수가코드 = " + ta02_jabo.PRICD + ", 행위구분 = " + ta02_jabo.ACTFG + ", EDI = " + ta02_jabo.ISPCD + ", EDI자보 = " + ta02_jabo.ISPCD_JABO);
            WriteLog("    적용일자 = " + ta02_jabo.CREDT + "->" + new_credt);

            WriteLog("    EDI금액(자보) = " + ta02_jabo.EDIAMT_JABO + "->" + new_ediamt_jabo + ", EDI금액(자보)(치과) = " + ta02_jabo.EDIAMT_DENT_JABO + "->" + new_ediamt_dent_jabo);

            WriteLog("    자보금액 = " + ta02_jabo.CPAMT + "->" + new_cpamt + ", 자보금액(치과) = " + ta02_jabo.CPAMT_DENT + "->" + new_cpamt_dent + ", 급여구분 = " + ta02_jabo.CALWF);

            WriteLog("    EDI점수(자보) = " + ta02_jabo.JUMSU_JABO + "->" + new_jumsu_jabo);
            WriteLog("    환산치(자보) = " + mchval_jabo);


            // PRICD, NEW_CREDT 레크드가 없으면 이전 적용일자로로 먼저 동일하게 만든다.
            //                           있으면 TA02_HX를 만든다.
            long ta02_cnt = GetTA02Cnt(ta02_jabo.PRICD, new_credt, p_conn);
            if (ta02_cnt > 0)
            {
                TA02Hx(ta02_jabo.PRICD, new_credt, p_conn);
            }
            else
            {
                CopyTA02(ta02_jabo.PRICD, ta02_jabo.CREDT, new_credt, p_conn);
            }

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "update ta02";
            sql += Environment.NewLine + "   set cpamt=" + new_cpamt + "";
            sql += Environment.NewLine + "     , cpamt_dent=" + new_cpamt_dent + "";

            sql += Environment.NewLine + "     , ediamt_jabo=" + new_ediamt_jabo + "";
            sql += Environment.NewLine + "     , ediamt_dent_jabo=" + new_ediamt_dent_jabo + "";
            sql += Environment.NewLine + "     , jumsu_jabo='" + new_jumsu_jabo + "'";

            sql += Environment.NewLine + "     , entdt=CONVERT(VARCHAR,GETDATE(),112)";
            sql += Environment.NewLine + "     , empid='AUTO'";
            sql += Environment.NewLine + "     , sysdt=CONVERT(VARCHAR,GETDATE(),112)";
            sql += Environment.NewLine + "     , systm=REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','')";
            sql += Environment.NewLine + " where pricd='" + ta02_jabo.PRICD + "'";
            sql += Environment.NewLine + "   and credt='" + new_credt + "'";

            ExecuteSql(sql, p_conn);
            
        }

        private long GetTA02Cnt(string pricd, string credt, OleDbConnection p_conn)
        {
            long ret = 0;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA02";
            sql += Environment.NewLine + " WHERE PRICD='" + pricd + "'";
            sql += Environment.NewLine + "   AND CREDT='" + credt + "'";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret++;
                return true;
            });
            return ret;
        }

        private void TA02Hx(string pricd, string credt, OleDbConnection p_conn)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT TA02_HX(HXID,HXDT,HXTM,HXRMK,";
            sql += Environment.NewLine + "PRICD,CREDT,ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG,NO_MED_MNG_FG,NT_ADD_FG) ";
            sql += Environment.NewLine + "SELECT 'AUTO',CONVERT(VARCHAR,GETDATE(),112),REPLACE(CONVERT(VARCHAR,GETDATE(),8),':',''),'변경수가확인',";
            sql += Environment.NewLine + "PRICD,CREDT,ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG,NO_MED_MNG_FG,NT_ADD_FG ";
            sql += Environment.NewLine + "  FROM TA02 ";
            sql += Environment.NewLine + " WHERE PRICD='" + pricd + "' ";
            sql += Environment.NewLine + "   AND CREDT='" + credt + "' ";

            ExecuteSql(sql, p_conn);
        }

        private void CopyTA02(string pricd, string old_credt, string new_credt, OleDbConnection p_conn)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "INSERT TA02(PRICD,CREDT";
            sql += Environment.NewLine + "     , ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG,NO_MED_MNG_FG,NT_ADD_FG) ";
            sql += Environment.NewLine + "SELECT PRICD,'" + new_credt + "' AS CREDT";
            sql += Environment.NewLine + "     , ISFNM,PRKNM,ENTDT,EXPDT,ACTFG,ISPCD,EDIAMT,MCHVAL,CALFC,INXCD,JJCD,VNDCD,PRUNT,OLDNM,EVENT,OPRFG,CNTST,OPSTC,IPSTC,ADDIV,SPEXF,GALWF,GPAMT,GSPAM,GSPRT,GSBRT,GSERT,IALWF,IPAMT,ISPAM,ISPRT,ISBRT,ISERT,CALWF,CPAMT,CSPAM,CSPRT,CSBRT,CSERT,DALWF,DPAMT,DSPAM,DSPRT,DSBRT,DSERT,NALWF,NPAMT,NSPAM,NSPRT,NSBRT,NSERT,LABDV,EMPID,SYSDT,SYSTM,SUGFG,REFCD,CODEGB,CHGDT,LKUMAK,REFFG,ISRRT,NSRRT,GPFIX,JUMSU,GUBUN,SCODE,EFCD,GSADD,CSADD,ADDBONFG,PCKFG,AMTFG1,AMTFG2,EDIAMT_DENT,GPAMT_DENT,IPAMT_DENT,CPAMT_DENT,DPAMT_DENT,NPAMT_DENT,ISPCD_JABO,EDIAMT_JABO,EDIAMT_DENT_JABO,JUMSU_JABO,MCHVAL_JABO,CSRRT,JABOEDIFG,BURNFG,BON80FG,NOJOJE,LABGB,ISPCD_SANJE,EDIAMT_SANJE,EDIAMT_DENT_SANJE,JUMSU_SANJE,MCHVAL_SANJE,NOADDALL,NDRGFG,BOSANGRT,CHGDTFG,NDRG_BAKDNYN,NDRG_BAKDN30,NDRG_BAKDN50,NDRG_BAKDN80,NDRG_BAKDN90,NSADD,NDRG_RULE2,NO_MAYAK_MNG_FG,DRG7_ADD_FG,DRG7_ADD_RT,NO_NT_ADD,NO_KIOSK_FG,DIALYSATE_FG,SELF_INJ,COVID19_PO,STD_CODE,STD_CODE_FG,NO_MED_MNG_FG,NT_ADD_FG ";
            sql += Environment.NewLine + "  FROM TA02 ";
            sql += Environment.NewLine + " WHERE PRICD='" + pricd + "' ";
            sql += Environment.NewLine + "   AND CREDT='" + old_credt + "' ";

            ExecuteSql(sql, p_conn);
        }

        // -----------------------------------------------------------------------------------------------------------

        private void make_new_pricd(CTI09 ti09, OleDbConnection p_conn)
        {
            long ti09_cnt = GetTI09Cnt(ti09, p_conn);
            if (ti09_cnt > 0)
            {
                WriteLog("  새로 만들어진 EDI 코드가 아님(" + ti09_cnt + "). 제외");
                return;
            }

            string actfg = "";
            string pricd1 = "";
            string pricd_new = "";

            if (ti09.PCODE == "B0001")
            {
                // 임상병리검사 종합검증료
                actfg = "92";
                pricd1 = "L";
            }
            else if (ti09.PCODE.StartsWith("C") || ti09.PCODE.StartsWith("D"))
            {
                if (ti09.PCODE.Length == 9 && ti09.PCODE.EndsWith("Z"))
                {
                    WriteLog("  위탁검사 코드임. 제외");
                    return;
                }

                SetLABKIND(ti09, p_conn);

                string labgb1 = ""; // 진단검사 등급
                string labgb2 = ""; // 병리검사 등급
                string labgb3 = ""; // 행의학검사 등급
                ReadLabGb(ti09.ADTDT, ref labgb1, ref labgb2, ref labgb3, p_conn);

                // 2023.12.15 WOOIL - 오동작 방지용
                if (labgb1 != "1" && labgb1 != "2" && labgb1 != "3" && labgb1 != "4" && labgb1 != "5" && labgb1 != "6") labgb1 = "0";
                if (labgb2 != "1") labgb2 = "0";
                if (labgb3 != "1") labgb3 = "0";
                WriteLog("  진단검사등급=" + labgb1 + ",병리검사등급=" + labgb2 + ",핵의학검사등급=" + labgb3);

                string labkind1 = ""; // J
                string labkind2 = ""; // B
                string labkind3 = ""; // H
                ReadLabKind(ti09.PCODE, ti09.ADTDT, ref labkind1, ref labkind2, ref labkind3, p_conn);

                if (ti09.PCODE.Length == 8)
                {
                    // 수가코드를 만들때 끝자리에 0,6,9만 사용한다. 왜? 기본코드이므로.
                    if (ti09.PCODE.EndsWith("L"))
                    {
                        if (labgb1 == "6") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 6등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("K"))
                    {
                        if (labgb1 == "5") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 5등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("J"))
                    {
                        if (labgb1 == "6") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 6등급
                    }
                    else if (ti09.PCODE.EndsWith("H"))
                    {
                        if (labgb1 == "5") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 5등급
                    }
                    else if (ti09.PCODE.EndsWith("G"))
                    {
                        if (labgb2 == "1") pricd_new = ti09.PCODE.Substring(0, 7) + "9"; // 병리검사 가산 외부슬라이드 판독
                    }
                    else if (ti09.PCODE.EndsWith("F"))
                    {
                        if (labgb2 == "1" && labkind1 != "J") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 병리검사 가산 전문의 판독(진단검사코드가 없는 경우만 생성)
                    }
                    else if (ti09.PCODE.EndsWith("E"))
                    {
                        if (labgb3 == "1" && labkind1 != "J" && labkind2 != "B") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; //핵의학검사 가산 전문의 판독(진단검사코드와 병리검사코드가 없는 경우만 생성)
                    }
                    else if (ti09.PCODE.EndsWith("D"))
                    {
                        if (labgb1 == "4") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 4등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("C"))
                    {
                        if (labgb1 == "3") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 3등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("B"))
                    {
                        if (labgb1 == "2") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 2등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("A"))
                    {
                        if (labgb1 == "1") pricd_new = ti09.PCODE.Substring(0, 7) + "6"; // 진단검사 1등급 전문의 판독
                    }
                    else if (ti09.PCODE.EndsWith("9"))
                    {
                        if (labgb2 != "1") pricd_new = ti09.PCODE; // 병리검사 가산X 외부슬라이드 판독 *** EDI코드 그대로 사용 ***
                    }
                    else if (ti09.PCODE.EndsWith("8"))
                    {
                        if (labgb2 == "1") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 병리검사 가산
                    }
                    else if (ti09.PCODE.EndsWith("7"))
                    {
                        if (labgb3 == "1" && labkind1 != "J" && labkind2 != "B") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 핵의학 가산(진단검사코드와 병리검사코드가 없는 경우만 생성)
                    }
                    else if (ti09.PCODE.EndsWith("6"))
                    {
                        if (labgb1 == "0" && labgb2 == "0" && labgb3 == "0") pricd_new = ti09.PCODE; // 가산없음. 전문의판독 *** EDI코드 그대로 사용 ***
                    }
                    else if (ti09.PCODE.EndsWith("4"))
                    {
                        if (labgb1 == "4") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 4등급
                    }
                    else if (ti09.PCODE.EndsWith("3"))
                    {
                        if (labgb1 == "3") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 3등급
                    }
                    else if (ti09.PCODE.EndsWith("2"))
                    {
                        if (labgb1 == "2") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 2등급
                    }
                    else if (ti09.PCODE.EndsWith("1"))
                    {
                        if (labgb1 == "1") pricd_new = ti09.PCODE.Substring(0, 7) + "0"; // 진단검사 1등급
                    }
                    else if (ti09.PCODE.EndsWith("0"))
                    {
                        if (labgb1 == "0" && labgb2 == "0" && labgb3 == "0") pricd_new = ti09.PCODE; // 가산없음. *** EDI코드 그대로 사용 ***
                    }

                    if (pricd_new == "")
                    {
                        WriteLog("  검체검사 질가산 등급이 안맞음. 제외");
                        return;
                    }


                    /*
                    if (ti09.PCODE.EndsWith("0") || ti09.PCODE.EndsWith("6") || ti09.PCODE.EndsWith("009"))
                    {
                        // 만들자.
                        // 009. 슬라이드외부판독
                        // nn0. 질가산없음.
                        // nn6. 질가산없음(전문의판독).
                    }
                    else
                    {
                        WriteLog("  검체검사 질가산 코드임. 제외");
                        SetLABKIND(ti09, p_conn);
                        return;
                    }
                    */
                }

                // 임상병리검사
                if (ti09.PCODE.StartsWith("D"))
                {
                    actfg = "92"; // 검체검사료
                }
                else
                {
                    actfg = "93"; // 조직검사료
                }
                pricd1 = "L";
            }
            else if (ti09.PCODE.StartsWith("EB") && ti09.PCODE.Length == 5)
            {
                // 초음파
                actfg = "96";//actfg = ti09.MAFG == "2" ? "96" : "9A5";
                pricd1 = "T";
            }
            else if ((ti09.PCODE.StartsWith("E") || ti09.PCODE.StartsWith("F")) && ti09.PCODE.Length == 5)
            {
                // 기능검사
                actfg = ti09.MAFG == "2" ? "95" : "9A5";
                pricd1 = "T";
            }
            else if (IsG0_9(ti09.PCODE)==true)
            {
                // 방사선(G0 ~ G9)
                if (ti09.PCODE.Length == 5)
                {
                    actfg = "011";//actfg = ti09.MAFG == "2" ? "011" : "0A1";
                    pricd1 = "R";
                }
                else
                {
                    WriteLog("  대상 코드 아님. 제외");
                    return;
                }
            }
            else if (ti09.PCODE.StartsWith("HA"))
            {
                // 방사선
                if (ti09.PCODE.Length == 5)
                {
                    actfg = "05";//actfg = ti09.MAFG == "2" ? "05" : "0A5";
                    pricd1 = "R";
                }
                else if(ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("001"))
                {
                    // 방사선.외부병원필름판독
                    actfg = "05";
                    pricd1 = "R";
                }
                else if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("046"))
                {
                    // 방사선.응급판독
                    actfg = "05";
                    pricd1 = "R";
                }
                else if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("646"))
                {
                    // 방사선.응급판독+6세미만소아
                    actfg = "05";
                    pricd1 = "R";
                }
                else
                {
                    WriteLog("  대상 코드 아님. 제외");
                    return;
                }
            }
            else if (ti09.PCODE.StartsWith("HE") || ti09.PCODE.StartsWith("HF"))
            {
                // MRI
                if (ti09.PCODE.Length == 5)
                {
                    actfg = "06";//actfg = ti09.MAFG == "2" ? "06" : "0A6";
                    pricd1 = "R";
                }
                else if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("001"))
                {
                    // MRI.외부병원필름판독
                    actfg = "06";
                    pricd1 = "R";
                }
                else if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("046"))
                {
                    // MRI.응급판독
                    actfg = "06";
                    pricd1 = "R";
                }
                else if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("646"))
                {
                    // MRI.응급판독+6세미만소아
                    actfg = "06";
                    pricd1 = "R";
                }
                else
                {
                    WriteLog("  대상 코드 아님. 제외");
                    return;
                }
            }
            else if (ti09.PCODE.StartsWith("HI") || ti09.PCODE.StartsWith("HJ"))
            {
                if (ti09.PCODE.Length == 8 && ti09.PCODE.EndsWith("600"))
                {
                    WriteLog("  MRI 소아 가산 코드임. 제외");
                    return;
                }
                else
                {
                    // MRI
                    actfg = "06";//actfg = ti09.MAFG == "2" ? "06" : "0A6";
                    pricd1 = "R";
                }
            }
            else if (ti09.PCODE.StartsWith("HK") && ti09.PCODE.Length == 8)
            {
                // PET.
                if (ti09.PCODE.Substring(5, 6) == "6" || ti09.PCODE.Substring(7, 8) == "6")
                {
                    // 6nn : 6세미만 소아
                    // nn6 : 판독가산
                    WriteLog("  대상 코드 아님. 제외");
                    return;
                }
                else
                {
                    actfg = "07";//actfg = ti09.MAFG == "2" ? "07" : "0A7";
                    pricd1 = "R";
                }
            }
            else if ((ti09.PCODE.StartsWith("LA") || ti09.PCODE.StartsWith("LB")) && ti09.PCODE.Length == 5)
            {
                // 마취
                actfg = ti09.MAFG == "2" ? "52" : "5A";
                pricd1 = "T";
            }
            else if ((ti09.PCODE.StartsWith("MM") || ti09.PCODE.StartsWith("IM") || ti09.PCODE.StartsWith("IN")) && ti09.PCODE.Length == 5)
            {
                // 물리치료
                actfg = ti09.MAFG == "2" ? "61" : "6A1";
                pricd1 = "O";
            }
            else if (IsMNOPQRS(ti09.PCODE) == true  && ti09.PCODE.Length == 5)
            {
                if (ti09.PCODE.StartsWith("O999"))
                {
                    WriteLog("  의료급여-혈액투석 정액수가. 제외");
                    return;
                }
                else
                {
                    // 처치
                    actfg = ti09.MAFG == "2" ? "82" : "8A1";
                    pricd1 = "T";
                }
            }
            else
            {
                WriteLog("  신규 생성 대상 코드가 아님. 제외");
                return;
            }

            string pricd = pricd1 + (pricd_new != "" ? pricd_new : ti09.PCODE);
            if (GetTA02Cnt(pricd, ti09.ADTDT, p_conn) > 0)
            {
                WriteLog("  수가코드 " + pricd + "가 있음. 제외");
                return;
            }

            // 병원 종별
            string hosp_jong = GetJong(ti09.ADTDT);

            // EDI 단가
            long new_ediamt = GetNewEdiamt(ti09, "", hosp_jong, "");
            long new_ediamt_dent = GetNewEdiamtDent(ti09, "", "");

            if (m_dentfg == "1")
            {
                if (new_ediamt == 0 && new_ediamt_dent == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }
            else
            {
                if (new_ediamt == 0)
                {
                    WriteLog("  EDI 단가가 0원임. 작업제외");
                    return;
                }
            }

            // 자격별 단가
            long new_ipamt = 0;
            long new_ipamt_dent = 0;
            long new_npamt = 0;
            long new_npamt_dent = 0;
            long new_dpamt = 0;
            long new_dpamt_dent = 0;
            long new_cpamt = 0;
            long new_cpamt_dent = 0;
            long new_gpamt = 0;
            long new_gpamt_dent = 0;

            // 보험금액
            new_ipamt = new_ediamt;
            new_ipamt_dent = new_ediamt_dent;

            // 보호금액
            new_npamt = new_ediamt;
            new_npamt_dent = new_ediamt_dent;


            // 산재금액
            new_dpamt = new_ediamt;
            new_dpamt_dent = new_ediamt_dent;

            // 자보금액
            new_cpamt = new_ediamt;
            new_cpamt_dent = new_ediamt_dent;


            // 일반수가
            double times = 1;
            double times_dent = 1;

            times = GetTimes(pricd, actfg, new_ediamt, ti09.GUBUN, "0");
            times_dent = GetTimes(pricd, actfg, new_ediamt_dent, ti09.GUBUN, "0");

            new_gpamt = MyRound((double)new_ediamt * times);
            new_gpamt_dent = MyRound((double)new_ediamt_dent * times_dent);

            WriteLog("  수가코드 = " + pricd + ", 행위구분 = " + actfg);
            WriteLog("    적용일자 = " + ti09.ADTDT);

            WriteLog("    EDI금액 = " + new_ediamt + ", EDI금액(치과) = " + new_ediamt_dent);

            WriteLog("    건보금액 = " + new_ipamt + ", 건보금액(치과) = " + + new_ipamt_dent);
            WriteLog("    보호금액 = " + new_npamt + ", 보호금액(치과) = " + + new_npamt_dent);
            WriteLog("    산재금액 = " + new_dpamt + ", 산재금액(치과) = " + + new_dpamt_dent);
            WriteLog("    자보금액 = " + new_cpamt + ", 자보금액(치과) = " + + new_cpamt_dent);
            WriteLog("    일반금액 = " + new_gpamt + ", 일반금액(치과) = " + + new_gpamt_dent + ", 일반배율 = " + times + ", 일반배율(치과) = " + times_dent);

            WriteLog("    EDI점수 = " + ti09.EDISCORE);

            string sql = "";
            sql += Environment.NewLine + "INSERT INTO TA02(PRICD,CREDT,ISFNM,PRKNM,ACTFG,ISPCD,EDIAMT,EDIAMT_DENT,EMPID,JUMSU,GUBUN";
            sql += Environment.NewLine + "                ,GPAMT,GPAMT_DENT,IALWF,IPAMT,IPAMT_DENT,CALWF,CPAMT,CPAMT_DENT,DALWF,DPAMT,DPAMT_DENT,NALWF,NPAMT,NPAMT_DENT";
            sql += Environment.NewLine + "                ,ENTDT,SYSDT,SYSTM)";
            sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?";
            sql += Environment.NewLine + "      ,?,?,?,?,?,?,?,?,?,?,?,?,?,?";
            sql += Environment.NewLine + "      ,CONVERT(VARCHAR,GETDATE(),112),CONVERT(VARCHAR,GETDATE(),112),REPLACE(CONVERT(VARCHAR,GETDATE(),8),':',''))";

            string prknm = ti09.PCODENM;
            if (ti09.SPEC != "")
            {
                string spec = ti09.SPEC;
                spec = spec.Replace("「응급의료에 관한 법률」에 의한 응급의료기관의 응급실에서 중증응급환자, 중증응급의심환자에게 영상의학과 전문의판독", "영상의학과전문의응급판독");
                prknm += "/" + spec;
            }

            List<object> para = new List<object>();
            para.Add(pricd);
            para.Add(ti09.ADTDT);
            para.Add(ti09.BUNCD);
            para.Add(prknm);
            para.Add(actfg);
            para.Add(ti09.PCODE);
            para.Add(new_ediamt);
            para.Add(new_ediamt_dent);
            para.Add("AUTO");
            para.Add(ti09.EDISCORE);
            para.Add(ti09.GUBUN);
            para.Add(new_gpamt);
            para.Add(new_gpamt_dent);
            para.Add("0");
            para.Add(new_ipamt);
            para.Add(new_ipamt_dent);
            para.Add("0");
            para.Add(new_cpamt);
            para.Add(new_cpamt_dent);
            para.Add("0");
            para.Add(new_dpamt);
            para.Add(new_dpamt_dent);
            para.Add("0");
            para.Add(new_npamt);
            para.Add(new_npamt_dent);

            ExecuteSql(sql, para, p_conn);
        }

        private long GetTI09Cnt(CTI09 ti09, OleDbConnection p_conn)
        {
            long ret = 0;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT COUNT(*) AS CNT";
            sql += Environment.NewLine + "  FROM TI09";
            sql += Environment.NewLine + " WHERE PCODE='" + ti09.PCODE + "'";
            sql += Environment.NewLine + "   AND GUBUN='" + ti09.GUBUN + "'";
            sql += Environment.NewLine + "   AND ADTDT<'" + ti09.ADTDT + "'";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string cnt = reader["CNT"].ToString();
                long.TryParse(cnt, out ret);
                return true;
            });
            return ret;
        }

        private void SetLABKIND(CTI09 ti09, OleDbConnection p_conn)
        {
            string pcode15 = ti09.PCODE.Substring(0, 5);
            string mst2cd = "";
            string spec = ti09.SPEC.Replace(" ", "");
            if (spec.StartsWith("진단검사질가산")) 
            {
                mst2cd = "LABKIND1";

            }
            else if (spec.StartsWith("병리검사질가산"))
            {
                mst2cd = "LABKIND4";
            }
            else if (spec.StartsWith("핵의학검사질가산"))
            {
                mst2cd = "LABKIND8";
            }

            if (mst2cd != "")
            {
                WriteLog("    " + mst2cd + " 대상");

                long cnt = 0;
                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT *";
                sql += Environment.NewLine + "  FROM TA88";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='" + mst2cd + "'";
                sql += Environment.NewLine + "   AND MST3CD='" + pcode15 + "'";

                GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    return true;
                });

                if (cnt < 1)
                {
                    WriteLog("    " + mst2cd + " 등록");

                    sql = "";
                    sql += Environment.NewLine + "INSERT INTO TA88(MST1CD,MST2CD,MST3CD) VALUES('A','" + mst2cd + "','" + pcode15 + "')";
                    ExecuteSql(sql, p_conn);
                }
            }
        }

        // ============================================================================================================

        private void WriteLog(string msg)
        {
            WriteLog(msg, true);
        }

        private void WriteLog(string msg, bool append)
        {
            if (append)
            {
                StreamWriter writer = File.AppendText(m_OutFile);
                writer.Write(DateTime.Now.ToString() + " " + msg + Environment.NewLine);
                writer.Close();

                //File.AppendAllText(m_OutFile, DateTime.Now.ToString() + " " + msg + Environment.NewLine);
            }
            else
            {
                File.WriteAllText(m_OutFile, DateTime.Now.ToString() + " " + msg + Environment.NewLine);
            }
        }

        private string GetJong(string exdt)
        {
            if (m_jong_dic.ContainsKey(exdt) == false)
            {
                string strConn = GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT FLD1CD";
                    sql += Environment.NewLine + "  FROM TA88A A";
                    sql += Environment.NewLine + " WHERE A.MST1CD='A'";
                    sql += Environment.NewLine + "   AND A.MST2CD='HOSPITAL'";
                    sql += Environment.NewLine + "   AND A.MST3CD='4'";
                    sql += Environment.NewLine + "   AND A.MST4CD=(SELECT MAX(X.MST4CD) ";
                    sql += Environment.NewLine + "                   FROM TA88A X";
                    sql += Environment.NewLine + "                  WHERE X.MST1CD=A.MST1CD ";
                    sql += Environment.NewLine + "                    AND X.MST2CD=A.MST2CD ";
                    sql += Environment.NewLine + "                    AND X.MST3CD=A.MST3CD ";
                    sql += Environment.NewLine + "                    AND X.MST4CD<='" + exdt + "'";
                    sql += Environment.NewLine + "                )";

                    int cnt = 0;
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        cnt++;
                        m_jong_dic[exdt] = reader["FLD1CD"].ToString();
                        return false;
                    });

                    if (cnt < 1)
                    {
                        sql = "SELECT FLD1CD FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='4'";
                        GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                        {
                            m_jong_dic[exdt] = reader["FLD1CD"].ToString();
                            return false;
                        });
                    }
                }
            }

            return m_jong_dic[exdt].ToString();
        }

        private string GetRefJong(string refcd)
        {
            if (m_refcd_jong_dic.ContainsKey(refcd) == false)
            {
                string strConn = GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string sql = "";
                    sql = "";
                    sql = "SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='SUTAK' AND MST3CD='" + refcd + "'";
                    GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_refcd_jong_dic[refcd] = reader["FLD1QTY"].ToString();
                        return false;
                    });
                }
            }
            return m_refcd_jong_dic.ContainsKey(refcd) == true ? m_refcd_jong_dic[refcd].ToString() : "";
        }

        private double GetTimes(string pricd, string actfg, long amt, string gubun, string ialwf)
        {
            double times = 0.0;

            if ( ialwf == "1") return 1.0; // 비급여
            if ( ialwf == "2") return 1.0; // 비보험
            if ( ialwf == "3" && m_times100fg!="1") return 1.0; // 100/100

            
            // 일반수가배율(행위구분으로처리)
            if (m_actfg_times.ContainsKey(actfg)==true){
                times = m_actfg_times[actfg];
                if(times==0) times=1.0;
                return times;
            }

            string actfg22 = actfg.Substring(1,1);
            long result = 0;
            bool is_numeric = long.TryParse(actfg22, out result);

            string gub = "";
            if (gubun == "1" || gubun == "9")
            {

                //1.수가 9.한방수가
                if (m_9bfg == "1" && actfg == "9B")
                {
                    gub = "행위";
                }
                else if (m_9bfg == "2" && actfg == "9B")
                {
                    times = ToDouble(m_9btimes);
                    if (times == 0) times = 1.0;
                    return times;

                }
                else if (is_numeric == true)
                {
                    gub = "행위";
                }
                else if (m_timesfg == "1")
                {
                    gub = "재료";
                }
                else if (m_timesfg == "2")
                {
                    gub = "행위";
                }

            }
            else if (gubun == "2")
            {

                //재료
                gub = "재료";

            }
            else if (gubun == "3" || gubun == "8")
            {

                //약가, 한방약가
                gub = "약";

            }
            else
            {

                return 1.0;

            }

            foreach (string data in m_prt)
            {
                string[] datas = data.Split(',');
                if (datas[0] == gub)
                {
                    long framt = ToLong(datas[1]);
                    long toamt = ToLong(datas[2]);
                    if (toamt == 0) toamt = 2147483647;
                    if (amt >= framt && amt <= toamt)
                    {
                        times = ToDouble(datas[3]);
                        break;
                    }
                }
            }

            if (times == 0) times = 1;
            return times;
        }

        private long MyRound(double value)
        {
            return (long)(value + (double)0.5);
        }

        private void ReadLabGb(string p_credt, ref string p_labgb1, ref string p_labgb2, ref string p_labgb3, OleDbConnection p_conn)
        {
            string labgb1 = ""; // 진단검사 등급
            string labgb2 = ""; // 병리검사 등급
            string labgb3 = ""; // 행의학검사 등급

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT A.MST3CD,A.MST4CD,A.CDNM,A.FLD2QTY,A.FLD3QTY,A.FLD4QTY";
            sql += Environment.NewLine + "  FROM TA88A A";
            sql += Environment.NewLine + " WHERE A.MST1CD='A'";
            sql += Environment.NewLine + "   AND A.MST2CD='JGASAN'";
            sql += Environment.NewLine + "   AND A.MST3CD='00000000'";
            sql += Environment.NewLine + "   AND A.MST4CD=(SELECT MAX(X.MST4CD)";
            sql += Environment.NewLine + "                   FROM TA88A X";
            sql += Environment.NewLine + "                  WHERE X.MST1CD='A'";
            sql += Environment.NewLine + "                    AND X.MST2CD='JGASAN'";
            sql += Environment.NewLine + "                    AND X.MST3CD=A.MST3CD";
            sql += Environment.NewLine + "                    AND X.MST4CD<='" + p_credt + "'";
            sql += Environment.NewLine + "                )";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                labgb1 = reader["FLD2QTY"].ToString();
                labgb2 = reader["FLD3QTY"].ToString();
                labgb3 = reader["FLD4QTY"].ToString();
                return false;
            });
            if (labgb1 != "1" && labgb1 != "2" && labgb1 != "3" && labgb1 != "4" && labgb1 != "5" && labgb1 != "6") labgb1 = "0";
            if (labgb2 != "1") labgb2 = "0";
            if (labgb3 != "1") labgb3 = "0";

            p_labgb1 = labgb1; // 진단검사 등급
            p_labgb2 = labgb2; // 병리검사 등급
            p_labgb3 = labgb3; // 행의학검사 등급
        }

        private void ReadLabKind(string p_code, string p_credt, ref string p_labkind1, ref string p_labkind2, ref string p_labkind3, OleDbConnection p_conn)
        {
            string labkind1 = "";
            string labkind2 = "";
            string labkind3 = "";

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT I09.PCODE,I09.SPEC";
            sql += Environment.NewLine + "  FROM TI09 I09 (nolock)";
            sql += Environment.NewLine + " WHERE I09.PCODE LIKE '" + p_code.Substring(0, 5) + "%'";
            sql += Environment.NewLine + "   AND I09.GUBUN='1'";
            sql += Environment.NewLine + "   AND I09.ADTDT = (SELECT MAX(X.ADTDT) FROM TI09 X (nolock) WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN AND X.ADTDT<='" + p_credt + "')";
            sql += Environment.NewLine + "   AND I09.SPEC LIKE '%질가산%'";
            sql += Environment.NewLine + " ORDER BY I09.PCODE";

            GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                string spec = reader["SPEC"].ToString().Trim();

                int idx = spec.IndexOf("진단검사");
                if (idx > 0) labkind1 = "J";
                idx = spec.IndexOf("병리검사");
                if (idx > 0) labkind2 = "B";
                idx = spec.IndexOf("핵의학검사");
                if (idx > 0) labkind3 = "H";

                return true;
            });

            p_labkind1 = labkind1;
            p_labkind2 = labkind2;
            p_labkind3 = labkind3;
        }

        private bool IsG0_9(string pcode)
        {
            if (pcode.StartsWith("G0")) return true;
            if (pcode.StartsWith("G1")) return true;
            if (pcode.StartsWith("G2")) return true;
            if (pcode.StartsWith("G3")) return true;
            if (pcode.StartsWith("G4")) return true;
            if (pcode.StartsWith("G5")) return true;
            if (pcode.StartsWith("G6")) return true;
            if (pcode.StartsWith("G7")) return true;
            if (pcode.StartsWith("G8")) return true;
            if (pcode.StartsWith("G9")) return true;
            return false;
        }

        private bool IsMNOPQRS(string pcode)
        {
            if (pcode.StartsWith("M")) return true;
            if (pcode.StartsWith("N")) return true;
            if (pcode.StartsWith("O")) return true;
            if (pcode.StartsWith("P")) return true;
            if (pcode.StartsWith("Q")) return true;
            if (pcode.StartsWith("R")) return true;
            if (pcode.StartsWith("S")) return true;
            return false;
        }

        // ============================================================================================================

        private void GetDataReader(string p_sql, OleDbConnection p_conn, Func<OleDbDataReader, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool bContinue = p_callback(reader);
                            if (bContinue == false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ExecuteSql(string p_sql, OleDbConnection p_conn)
        {
            int cnt = 0;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    cnt = cmd.ExecuteNonQuery();
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public int ExecuteSql(string p_sql, List<Object> p_para, OleDbConnection p_conn)
        {
            int cnt = 0;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    cnt = cmd.ExecuteNonQuery();
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GetConnectionString()
        {
            string systemFolder = Environment.SystemDirectory; // SYSTEM FOLDER
            string iniFilePath = systemFolder + Path.DirectorySeparatorChar + "Metro.ini"; // METRO.INI 파일
            string strConn = ReadIniFile(iniFilePath, "CONNECTION STRING", "METRO"); // 데이터베이스 CONNECTION 문자열
            if ("".Equals(strConn))
            {
                // 64비트 서버이면 시스템 폴더가 SysWOW64이다.
                iniFilePath = "C:/Windows/SysWOW64/Metro.ini";
                strConn = ReadIniFile(iniFilePath, "CONNECTION STRING", "METRO"); // 데이터베이스 CONNECTION 문자열
            }
            return strConn;
        }

        private string ReadIniFile(string path, string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, path);
            return sb.ToString();
        }

        private double ToDouble(String Value)
        {
            double result = 0;
            double.TryParse(Value, out result);
            return result;
        }

        private long ToLong(String Value)
        {
            double result = 0;
            double.TryParse(Value, out result);
            long ret = 0;
            long.TryParse(result.ToString(), out ret);
            return ret;
        }
    }
}

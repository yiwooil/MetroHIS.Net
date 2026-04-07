using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7000E
{
    class CDataTT41A
    {
        public const int ENAMT = 0; // 진찰료
        public const int INAMT_BDM1 = 1; // 1인실 입원료 *** 2019.01.21 추가 ***
        public const int INAMT_BDM2 = 2; // 2,3인실 입원료 *** 2019.01.21 추가 ***
        public const int INAMT_BDM4 = 3; // 4인실이상 입원료
        public const int FOEP = 4; // 식대
        public const int CPMD_ACTAMT = 5; // 투약 및 조제료.행위료
        public const int CPMD_MDAMT = 6; // 투약 및 조제로.약품비
        public const int IJCT_ACTAMT = 7; // 주사료.행위료
        public const int IJCT_MDAMT = 8; // 주사료.약품비
        public const int NCAMT = 9; // 마취료
        public const int DPAMT = 10;// 처치 및 수술료
        public const int EXAMT = 11;// 검사료
        public const int IMG_DIAMT = 12;// 영상진단
        public const int RADT_TRRT = 13;// 방사선치료료
        public const int TMCAT = 14;// 치료재료대
        public const int PTR = 15;// 재활 및 물리치료료
        public const int PYAMT = 16;// 정신요법료
        public const int BCF = 17;// 전혈 및 혈액성분제제료
        public const int CT_DIAMT = 18;// CT진단료
        public const int MRI_DIAMT = 19;// MRI진단료
        public const int PET_DIAMT = -1;// PET진단료 *** 유비스 병원에 없음 ***
        public const int ULTRS_DIAMT = 20;// 초음파진단료
        public const int CRFE = 21;// 보철.교정료
        public const int ETC_AMT = -1;// 기타 *** 유비스 병원에 없음 ***
        public const int YPAY_XPNS = 22; // <시행령 별표2 제4호의 요양급여>. 선별급여
                                         // 23은 가짜
        public const int A65G = 24;// 65세이상등 정액
        public const int FAMT_MDFEE = -1;// 요양병원정액수가 *** 유비스 병원에 없음 ***
        public const int HOSPICE = -1;   // 완화의료정액수가.*** 신포괄 영수증에 없음 ***
        public const int ICSN_MDFEE = 25;// 포괄수가진료비

        public const int PTAMT = 0;  // 급여 본인
        public const int UNAMT = 1;  // 급여 조합
        public const int PPAMT = 2; // 급여 전액본인
        public const int SXAMT = 3;  // 비급여 선택
        public const int BBAMT = 4;  // 비급여 선택이외

        public long[,] gumak = new long[27, 5]; // 급여 본인


        // TT41A의 IDX값임. 이 클래스에 있는 ARRAY 변수의 INDEX는 1을 뺀 값을 적용해야한다. 즉, 0부터 시작.
        // idx =  1 : 급여조합
        // idx =  2 : 급여본인
        // idx =  3 : 비급여+비보험(13)+100.100(8)
        // idx =  4 : 특진
        // idx =  5 : 종별가산
        // idx =  6 : 비급여(코드)
        // idx =  7 : 비보험(코드)
        // idx =  8 : 100/100
        // idx =  9 : 선별급여조합
        // idx = 10 : 선별급여조합
        // idx = 11 : 선별급여특진
        // idx = 12 : 선별급여종별가산
        // idx = 13 : 의보전액본인

        public long[,] amt = new long[51,13];

        public long[] amt2gumak = new long[51];

        public String DPTCD;
        public String QFYCD;
        public String STTDT;
        public String ENDDT;
        public String RPID;

        public String INSDPTCD;

        public String DRGNO;
        public String WARDID;

        private String m_BOAMT;
        private String m_PSAMT;
        private String m_CONFAMT;
        private String m_CSHAM;

        private String m_TT96KEY;

        private String m_ACCAMT; // TT96에 있음.
        private String m_IDNO; // TT96에 있음.
        private String m_ACCNO; // TT96에 있음.


        public void Clear()
        {
            for (int i = 0; i < 51; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    amt[i, j] = 0;
                }
            }

            for (int i = 0; i < 27; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gumak[i, j] = 0;
                }
            }

            for (int i = 0; i < 51; i++)
            {
                amt2gumak[i] = 0;
            }

            DPTCD = "";
            QFYCD = "";
            STTDT = "";
            ENDDT = "";
            RPID = "";
            INSDPTCD = "";
            DRGNO = "";
            WARDID = "";

            m_BOAMT = "";
            m_PSAMT = "";
            m_CONFAMT = "";
            m_CSHAM = "";

            m_TT96KEY = "";

            m_ACCAMT = "";
            m_IDNO = "";
            m_ACCNO = "";
        }

        public void SetData(CDataTI2A i2a)
        {
            try
            {
                if ("".Equals(i2a.TT41KEY)) return;
                String[] keys = i2a.TT41KEY.Split(',');

                bool first = true;

                string sql = "";
                sql += System.Environment.NewLine + "SELECT T41A.*";
                sql += System.Environment.NewLine + "     , T41.*";
                sql += System.Environment.NewLine + "     , A09.INSDPTCD";
                sql += System.Environment.NewLine + "     , A04.DRGNO, A04.WARDID";
                sql += System.Environment.NewLine + "  FROM TT41 T41 (NOLOCK) INNER JOIN TT41A T41A (NOLOCK) ON T41A.HDID=T41.HDID";
                sql += System.Environment.NewLine + "                         INNER JOIN TA09 A09 (NOLOCK) ON A09.DPTCD=T41.DPTCD";
                sql += System.Environment.NewLine + "                         INNER JOIN TA04 A04 (NOLOCK) ON A04.PID=T41.PID AND A04.BEDEDT=T41.BDEDT";
                sql += System.Environment.NewLine + " WHERE T41.PID = '" + keys[0] + "'";
                sql += System.Environment.NewLine + "   AND T41.BDEDT = '" + keys[1] + "'";
                sql += System.Environment.NewLine + "   AND T41.QFYCD = '" + keys[2] + "'";
                sql += System.Environment.NewLine + "   AND T41.STTDT = '" + keys[3] + "'";
                sql += System.Environment.NewLine + "   AND T41.DRID = '" + keys[4] + "'";
                sql += System.Environment.NewLine + "   AND T41.SEQ = '" + keys[5] + "'";

                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (first == true)
                        {
                            DPTCD = reader["DPTCD"].ToString();
                            QFYCD = reader["QFYCD"].ToString();
                            STTDT = reader["STTDT"].ToString();
                            ENDDT = reader["ENDDT"].ToString();
                            RPID = reader["RPID"].ToString();

                            INSDPTCD = reader["INSDPTCD"].ToString();

                            DRGNO = reader["DRGNO"].ToString();
                            WARDID = reader["WARDID"].ToString();


                            m_BOAMT = reader["BOAMT"].ToString();
                            m_PSAMT = reader["PSAMT"].ToString();
                            m_CONFAMT = reader["CONFAMT"].ToString();
                            m_CSHAM = reader["CSHAM"].ToString();

                            m_TT96KEY = reader["TT96KEY"].ToString();

                            first = false;
                        }
                        String strIDX = reader["IDX"].ToString();
                        int idx = int.Parse(strIDX);
                        if (idx < 1 || idx > 13) continue;

                        for (long k = 1; k <= 51; k++)
                        {
                            String fldName = "AMT" + k.ToString();
                            String value = reader[fldName].ToString();
                            if ("".Equals(value)) value = "0";
                            amt[k - 1, idx - 1] = long.Parse(value);
                        }
                    }
                    reader.Close();

                    SetAmtToGumak(conn);

                    ReadTT96(conn);
                }
                
                SetGumak();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetAmtToGumak(OleDbConnection p_conn)
        {
            String sql ="";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TA88 ";
            sql += System.Environment.NewLine + " WHERE MST1CD='A' ";
            sql += System.Environment.NewLine + "   AND MST2CD='PRT_ODR_Y'";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                String strIdx51 = reader["MST3CD"].ToString(); // 48개항목
                String strIdx = reader["FLD2QTY"].ToString();  // 영수증 세로위치

                int idx51 = int.Parse(strIdx51);
                int idx = int.Parse(strIdx);

                amt2gumak[idx51 - 1] = idx - 1;
            }
            reader.Close();

            // 포괄수가는 별도로.
            amt2gumak[50] = CDataTT41A.ICSN_MDFEE; // AMT51(배열의 인덱스는 50임)은 포괄수가임.
        }

        private void SetGumak()
        {
            for (int idx51 = 0; idx51 < 51; idx51++)
            {
                long idx = amt2gumak[idx51];
                gumak[idx, CDataTT41A.UNAMT] += amt[idx51, 0] - amt[idx51, 8]; // 선별급여를 빼주어야함.
                gumak[idx, CDataTT41A.PTAMT] += amt[idx51, 1] - amt[idx51, 9]; // 선별급여를 빼주어야함.
                gumak[idx, CDataTT41A.PPAMT] += amt[idx51, 7];
                gumak[idx, CDataTT41A.SXAMT] += amt[idx51, 3];
                gumak[idx, CDataTT41A.BBAMT] += amt[idx51, 2] - amt[idx51, 7]; // 선택진료이외비급여

                // 선별급여
                gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.UNAMT] +=  amt[idx51, 8]; // 조합
                gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.PTAMT] +=  amt[idx51, 9]; // 본인
                gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.PPAMT] += 0;
                gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.SXAMT] += amt[idx51, 10]; // 특진
                gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.BBAMT] += 0;
            }
        }

        private void ReadTT96(OleDbConnection p_conn)
        {
            if ("".Equals(m_TT96KEY.Trim())) return;

            String[] aryTT96key = (m_TT96KEY + ";;;").Split(';');

            if ("".Equals(aryTT96key[0])) return;
            if ("".Equals(aryTT96key[1])) return;
            if ("".Equals(aryTT96key[2])) return;
            if ("".Equals(aryTT96key[3])) return;

            String sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TT96 ";
            sql += System.Environment.NewLine + " WHERE ACCDT='" + aryTT96key[0] + "' ";
            sql += System.Environment.NewLine + "   AND PID='" + aryTT96key[1] + "'";
            sql += System.Environment.NewLine + "   AND EXDIV='" + aryTT96key[2] + "'";
            sql += System.Environment.NewLine + "   AND SEQ='" + aryTT96key[3] + "'";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                m_ACCAMT = reader["ACCAMT"].ToString();
                m_IDNO = reader["IDNO"].ToString();
                m_ACCNO = reader["ACCNO"].ToString();
            }
            reader.Close();

        }

        public long GetTotPTAMT()
        {
            long totamt = 0;
            for (int i = 0; i < 27; i++)
            {
                totamt += gumak[i, CDataTT41A.PTAMT];
            }
            return totamt;
        }

        public long GetTotUNAMT()
        {
            long totamt = 0;
            for (int i = 0; i < 27; i++)
            {
                totamt += gumak[i, CDataTT41A.UNAMT];
            }
            return totamt;
        }

        public long GetTotPPAMT()
        {
            long totamt = 0;
            for (int i = 0; i < 27; i++)
            {
                totamt += gumak[i, CDataTT41A.PPAMT];
            }
            return totamt;
        }

        public long GetTotSXAMT()
        {
            long totamt = 0;
            for (int i = 0; i < 27; i++)
            {
                totamt += gumak[i, CDataTT41A.SXAMT];
            }
            return totamt;
        }

        public long GetTotBBAMT()
        {
            long totamt = 0;
            for (int i = 0; i < 27; i++)
            {
                totamt += gumak[i, CDataTT41A.BBAMT];
            }
            return totamt;
        }

        public long GetMaxPtAMT()
        {
            // 상한액초과금
            return 0;
        }

        public String GetPtntTyCd()
        {
            if ("31".Equals(QFYCD) == true) return "2";
            if ("32".Equals(QFYCD) == true) return "3";
            return "1";
        }

        public String GetSunapTyCd()
        {
            if ("".Equals(RPID)) return "";
            if (RPID.Length < 7) return "";
            return ("G".Equals(RPID.Substring(6, 1)) ? "2" : "1"); // 수납유형 1.퇴원 2.중간
        }

        public long GetPrePayAmt()
        {
            // 이미 납부한 금액
            long lBoamt = 0;
            long lPsamt = 0;

            try
            {
                lBoamt = int.Parse(m_BOAMT);
            }
            catch (Exception e1)
            {
                lBoamt = 0;
            }
            try
            {
                lPsamt = int.Parse(m_PSAMT);
            }
            catch (Exception e2)
            {
                lPsamt = 0;
            }

            return lBoamt + lPsamt;
        }

        public long GetConfAmt()
        {
            // 카드로 납부한 금액
            long lConfamt = 0;
            try
            {
                lConfamt = int.Parse(m_CONFAMT);
            }
            catch (Exception e2)
            {
                lConfamt = 0;
            }
            return lConfamt;
        }

        public long GetAccAmt()
        {
            // 현급 영수증으로 납부한 금액
            long lAccamt = 0;
            try
            {
                lAccamt = int.Parse(m_ACCAMT);
            }
            catch (Exception e2)
            {
                lAccamt = 0;
            }
            return lAccamt;
        }

        public long GetCashAmt()
        {
            // 현금으로 납부한 금액
            // 우리 자료에는 현금영수증금액과 현금금액 양쪽에 다 자료가 있음.
            // 현금금액 - 현금영수증금액 으로 하여야 심평원에서 원하는 자료가 됨.
            long lAccamt = 0;
            long lCsham = 0;
            try
            {
                lAccamt = int.Parse(m_ACCAMT);
                lCsham = int.Parse(m_CSHAM);
            }
            catch (Exception e2)
            {
                lAccamt = 0;
                lCsham = 0;
            }
            return lCsham - lAccamt;
        }

        public String GetIdno()
        {
            return m_IDNO;
        }

        public String GetAccno()
        {
            return m_ACCNO;
        }

    }
}

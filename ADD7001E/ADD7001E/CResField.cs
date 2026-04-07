using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CResField
    {
        private string m_User;

        public string RN { get; set; }
        public string TOT_CNT { get; set; }
        public string REQ_DATA_NO { get; set; }
        public string REQ_STA_DD { get; set; }
        public string REQ_END_DD { get; set; }
        public string REQ_CLOS_YN { get; set; }
        public string SUPL_DATA_FOM_CD { get; set; }
        public string SUPL_DATA_FOM_CD_NM { get; set; }
        public string SC_DTL_FOM_CD { get; set; }
        public string SC_DTL_FOM_CD_NM { get; set; }
        public string ASM_YM { get; set; }
        public string DMD_NO { get; set; }
        public string RCV_NO { get; set; }
        public string RCV_YR { get; set; }
        public string BILL_SNO { get; set; }
        public string SP_SNO { get; set; }
        public string INSUP_TP_CD { get; set; }
        public string HOSP_RNO { get; set; }
        public string PAT_NM { get; set; }
        public string PAT_BTH { get; set; }
        public string PAT_SEX { get; set; }
        public string DIAG_YM { get; set; }
        public string RCV_DD { get; set; }
        public string RECU_FR_DD { get; set; }
        public string RECU_END_DD { get; set; }
        public string DGSBJT_CD_NM { get; set; }
        public string MSICK_SICK_SYM { get; set; }
        public string KOR_SICK_NM { get; set; }
        public string SMIT_YN { get; set; }
        public string SMIT_FDEC_DD { get; set; }
        public string currentPage { get; set; }
        public string recordCountPerPage { get; set; }
        //
        //
        public string LOCAL_FILE_PTH_COUNT { 
            get{
                string strRet = "";
                List<CLOCAL_FILE_PTH> files = GetFileList();
                if (files != null)
                {
                    if (files.Count > 0)
                    {
                        strRet = files.Count.ToString();
                    }
                }
                return strRet;
            }
        }
        public string PROCESS { get; set; }
        public string ERR_MSG;
        // 
        public string PAT_SEX_NM { 
            get {
                if ("1".Equals(PAT_SEX)) return "남";
                else return "여";
            } 
        }
        //
        private string I2A_PID;
        public string I2A_BDEDT { get; set; } // *** 화면에 보이게
        private string I2A_RESID;
        private CTV100 m_TV100;
        private CTE12C m_E12C;
        private CEMR290 m_EMR290;
        private CEMR270 m_EMR270;
        private CTE12C_ADM m_E12C_ADM;
        //private CTU02 m_U02;
        //private CTU03 m_U03;
        //
        private CRID001 m_RID001;
        private CRII001 m_RII001;
        private CRIP001 m_RIP001;
        private CODD001 m_ODD001; // 기타자료
        //private CRSS001 m_RSS001;

        public void SetValues(HIRA.EformEntry.Model.Row row, string p_User)
        {
            m_User = p_User;

            RN = row["RN"].Value;
            TOT_CNT = row["TOT_CNT"].Value;
            REQ_DATA_NO = row["REQ_DATA_NO"].Value;
            REQ_STA_DD = row["REQ_STA_DD"].Value;
            REQ_END_DD = row["REQ_END_DD"].Value;
            REQ_CLOS_YN = row["REQ_CLOS_YN"].Value;
            SUPL_DATA_FOM_CD = row["SUPL_DATA_FOM_CD"].Value;
            SUPL_DATA_FOM_CD_NM = row["SUPL_DATA_FOM_CD_NM"].Value;
            SC_DTL_FOM_CD = row["SC_DTL_FOM_CD"].Value;
            SC_DTL_FOM_CD_NM = row["SC_DTL_FOM_CD_NM"].Value;
            ASM_YM = row["ASM_YM"].Value;
            DMD_NO = row["DMD_NO"].Value; // 청구번호
            RCV_NO = row["RCV_NO"].Value;
            RCV_YR = row["RCV_YR"].Value;
            BILL_SNO = row["BILL_SNO"].Value;
            SP_SNO = row["SP_SNO"].Value; // 명일련
            INSUP_TP_CD = row["INSUP_TP_CD"].Value;
            HOSP_RNO = row["HOSP_RNO"].Value; // 환자ID
            PAT_NM = row["PAT_NM"].Value;
            PAT_BTH = row["PAT_BTH"].Value;
            PAT_SEX = row["PAT_SEX"].Value;
            DIAG_YM = row["DIAG_YM"].Value;
            RCV_DD = row["RCV_DD"].Value;
            RECU_FR_DD = row["RECU_FR_DD"].Value;
            RECU_END_DD = row["RECU_END_DD"].Value;
            DGSBJT_CD_NM = row["DGSBJT_CD_NM"].Value;
            MSICK_SICK_SYM = row["MSICK_SICK_SYM"].Value;
            KOR_SICK_NM = row["KOR_SICK_NM"].Value;
            SMIT_YN = row["SMIT_YN"].Value;
            SMIT_FDEC_DD = row["SMIT_FDEC_DD"].Value;
            currentPage = row["currentPage"].Value;
            recordCountPerPage = row["recordCountPerPage"].Value;

            // 입원등록일을 찾기 위함임.
            this.ReadTI2A();
            //
            m_TV100 = new CTV100();
            m_TV100.Clear();
            m_TV100.SetData(I2A_PID, I2A_BDEDT);
            //
            m_E12C = new CTE12C();
            m_E12C.Clear();
            m_E12C.SetData(I2A_PID, I2A_BDEDT);
            //
            m_EMR290 = new CEMR290();
            m_EMR290.Clear();
            m_EMR290.SetData(I2A_PID, I2A_BDEDT);
            //
            m_EMR270 = new CEMR270();
            m_EMR270.Clear();
            m_EMR270.SetData(I2A_PID, I2A_BDEDT);
            //
            m_E12C_ADM = new CTE12C_ADM();
            m_E12C_ADM.Clear();
            m_E12C_ADM.SetData(I2A_PID, I2A_BDEDT);
            //
            //m_U02 = new CTU02();
            //m_U02.Clear();
            //m_U02.SetData(I2A_PID, I2A_BDEDT);
            //
            //m_U03 = new CTU03();
            //m_U03.Clear();
            //m_U03.SetData(I2A_PID, I2A_BDEDT);
            //
            PROCESS = "대기";
            ERR_MSG = "";
            //
            //if(m_U02.READ_COUNT>0)
            //{
            //    if ("ODD001".Equals(SC_DTL_FOM_CD))
            //    {
            //        SC_DTL_FOM_CD_NM = "RSS001 수술기록지";
            //    }
            //}
            //
            SaveData();
        }

        public void SetValues(CTI83 ti83, string p_User)
        {
            m_User = p_User;

            RN = "";
            TOT_CNT = "";
            REQ_DATA_NO = ti83.REQ_DATA_NO;
            REQ_STA_DD = ti83.REQ_STA_DD;
            REQ_END_DD = ti83.REQ_END_DD;
            REQ_CLOS_YN = ti83.REQ_CLOS_YN;
            SUPL_DATA_FOM_CD = ti83.SUPL_DATA_FOM_CD;
            SUPL_DATA_FOM_CD_NM = ti83.SUPL_DATA_FOM_CD_NM;
            SC_DTL_FOM_CD = ti83.SC_DTL_FOM_CD;
            SC_DTL_FOM_CD_NM = ti83.SC_DTL_FOM_CD_NM;
            ASM_YM = ti83.ASM_YM;
            DMD_NO = ti83.DMD_NO; // 청구번호
            RCV_NO = ti83.RCV_NO;
            RCV_YR = ti83.RCV_YR;
            BILL_SNO = ti83.BILL_SNO;
            SP_SNO = ti83.SP_SNO; // 명일련
            INSUP_TP_CD = ti83.INSUP_TP_CD;
            HOSP_RNO = ti83.HOSP_RNO; // 환자ID
            PAT_NM = ti83.PAT_NM;
            PAT_BTH = ti83.PAT_BTH;
            PAT_SEX = ti83.PAT_SEX;
            DIAG_YM = ti83.DIAG_YM;
            RCV_DD = ti83.RCV_DD;
            RECU_FR_DD = ti83.RECU_FR_DD;
            RECU_END_DD = ti83.RECU_END_DD;
            DGSBJT_CD_NM = ti83.DGSBJT_CD_NM;
            MSICK_SICK_SYM = ti83.MSICK_SICK_SYM;
            KOR_SICK_NM = ti83.KOR_SICK_NM;
            SMIT_YN = ti83.SMIT_YN;
            SMIT_FDEC_DD = ti83.SMIT_FDEC_DD;
            currentPage = "";
            recordCountPerPage = "";

            // 입원등록일을 찾기 위함임.
            this.ReadTI2A();
            ////
            //m_TV100 = new CTV100();
            //m_TV100.Clear();
            //m_TV100.SetData(I2A_PID, I2A_BDEDT);
            ////
            //m_E12C = new CTE12C();
            //m_E12C.Clear();
            //m_E12C.SetData(I2A_PID, I2A_BDEDT);
            ////
            //m_EMR290 = new CEMR290();
            //m_EMR290.Clear();
            //m_EMR290.SetData(I2A_PID, I2A_BDEDT);
            ////
            //m_EMR270 = new CEMR270();
            //m_EMR270.Clear();
            //m_EMR270.SetData(I2A_PID, I2A_BDEDT);
            ////
            //m_E12C_ADM = new CTE12C_ADM();
            //m_E12C_ADM.Clear();
            //m_E12C_ADM.SetData(I2A_PID, I2A_BDEDT);
            ////
            PROCESS = "대기";
            ERR_MSG = "";
        }

        public CResField()
        {
            RN = "";
            TOT_CNT = "";
            REQ_DATA_NO = "";
            REQ_STA_DD = "";
            REQ_END_DD = "";
            REQ_CLOS_YN = "";
            SUPL_DATA_FOM_CD = "";
            SUPL_DATA_FOM_CD_NM = "";
            SC_DTL_FOM_CD = "";
            SC_DTL_FOM_CD_NM = "";
            ASM_YM = "";
            DMD_NO = "";
            RCV_NO = "";
            RCV_YR = "";
            BILL_SNO = "";
            SP_SNO = "";
            INSUP_TP_CD = "";
            HOSP_RNO = "";
            PAT_NM = "";
            PAT_BTH = "";
            PAT_SEX = "";
            DIAG_YM = "";
            RCV_DD = "";
            RECU_FR_DD = "";
            RECU_END_DD = "";
            DGSBJT_CD_NM = "";
            MSICK_SICK_SYM = "";
            KOR_SICK_NM = "";
            SMIT_YN = "";
            SMIT_FDEC_DD = "";
            currentPage = "";
            recordCountPerPage = "";
            //
            PROCESS = "";
            ERR_MSG = "";
            //
            I2A_PID = "";
            I2A_BDEDT = "";
            I2A_RESID = "";
            m_TV100 = null;

        }

        private void ReadTI2A()
        {
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TI2A A";
            sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + DMD_NO + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(A.EPRTNO,0)=" + SP_SNO + "";
                
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    I2A_PID = reader["PID"].ToString();
                    I2A_BDEDT = reader["BDEDT"].ToString();
                    I2A_RESID = reader["RESID"].ToString();
                }
                reader.Close();
            }
        }

        public void SetList(bool bRemake, bool bReadOnly)
        {
            if ("RID001".Equals(SC_DTL_FOM_CD))
            {
                // 퇴원요약
                m_RID001 = new CRID001(m_User);
                if (bReadOnly)
                {
                    m_RID001.ReadValues(REQ_DATA_NO);
                }
                else
                {
                    if (bRemake)
                    {
                        m_RID001.SetValues(m_TV100, REQ_DATA_NO);
                    }
                    else
                    {
                        if (m_RID001.ReadValues(REQ_DATA_NO) == false)
                        {
                            m_RID001.SetValues(m_TV100, REQ_DATA_NO);
                        }
                    }
                }
                m_RID001.ReadFiles(REQ_DATA_NO);
            }
            else if ("RII001".Equals(SC_DTL_FOM_CD))
            {
                // 입원기록
                m_RII001 = new CRII001(m_User);
                if (bReadOnly)
                {
                    m_RII001.ReadValues(REQ_DATA_NO);
                }
                else
                {
                    if (bRemake)
                    {
                        m_RII001.SetValues(m_E12C, m_EMR290, m_E12C_ADM, m_EMR270, REQ_DATA_NO);
                    }
                    else
                    {
                        if (m_RII001.ReadValues(REQ_DATA_NO) == false)
                        {
                            m_RII001.SetValues(m_E12C, m_EMR290, m_E12C_ADM, m_EMR270, REQ_DATA_NO);
                        }
                    }
                }
                m_RII001.ReadFiles(REQ_DATA_NO);
            }
            else if ("RIP001".Equals(SC_DTL_FOM_CD))
            {
                // 경과기록
                m_RIP001 = new CRIP001(m_User);
                if (bReadOnly)
                {
                    m_RIP001.ReadValues(REQ_DATA_NO);
                }
                else
                {
                    if (bRemake)
                    {
                        m_RIP001.SetValues(m_TV100, m_E12C, REQ_DATA_NO);
                    }
                    else
                    {
                        if (m_RIP001.ReadValues(REQ_DATA_NO) == false)
                        {
                            m_RIP001.SetValues(m_TV100, m_E12C, REQ_DATA_NO);
                        }
                    }
                }
                m_RIP001.ReadFiles(REQ_DATA_NO);
            }
            else if ("ODD001".Equals(SC_DTL_FOM_CD))
            {
                // 기타자료
                // 파일 첨부로 보내는 것이라고 함. 033-739-0815

                m_ODD001 = new CODD001(m_User);
                if (bReadOnly)
                {
                    m_ODD001.ReadValues(REQ_DATA_NO);
                }
                else
                {
                    if (bRemake)
                    {
                        m_ODD001.SetValues(REQ_DATA_NO);
                    }
                    else
                    {
                        if (m_ODD001.ReadValues(REQ_DATA_NO) == false)
                        {
                            m_ODD001.SetValues(REQ_DATA_NO);
                        }
                    }
                }
                m_ODD001.ReadFiles(REQ_DATA_NO);

                
                // 수술기록
                //if (SC_DTL_FOM_CD_NM.StartsWith("RSS001"))
                //{
                //    m_RSS001 = new CRSS001(m_User);
                //    if (bRemake)
                //    {
                //        m_RSS001.SetValues(m_U02, m_U03, REQ_DATA_NO);
                //    }
                //    else
                //    {
                //        if (m_RSS001.ReadValues(REQ_DATA_NO) == false)
                //        {
                //            m_RSS001.SetValues(m_U02, m_U03, REQ_DATA_NO);
                //        }
                //    }
                //}
            }
        }

        public List<CTBL_FOM_CZITM> GetList()
        {
            if ("RID001".Equals(SC_DTL_FOM_CD))
            {
                // 퇴원요약
                if (m_RID001 == null) return null;
                else return m_RID001.GetList(); 
            }
            else if ("RII001".Equals(SC_DTL_FOM_CD))
            {
                // 입원기록
                if (m_RII001 == null) return null;
                else return m_RII001.GetList();
            }
            else if ("RIP001".Equals(SC_DTL_FOM_CD))
            {
                // 경과기록
                if (m_RIP001 == null) return null;
                else return m_RIP001.GetList();
            }
            else if ("ODD001".Equals(SC_DTL_FOM_CD))
            {
                // 기타자료
                if (m_ODD001 == null) return null;
                else return m_ODD001.GetList();

                // 수술기록
                //if (SC_DTL_FOM_CD_NM.StartsWith("RSS001"))
                //{
                //    return m_RSS001.GetList();
                //}
                //else
                //{
                //    return null;
                //}
            }
            else
            {
                return null;
            }
        }

        public List<CLOCAL_FILE_PTH> GetFileList()
        {
            if ("RID001".Equals(SC_DTL_FOM_CD))
            {
                // 퇴원요약
                if (m_RID001 == null) return null;
                else return m_RID001.GetFileList();
            }
            else if ("RII001".Equals(SC_DTL_FOM_CD))
            {
                // 입원기록
                if (m_RII001 == null) return null;
                else return m_RII001.GetFileList();
            }
            else if ("RIP001".Equals(SC_DTL_FOM_CD))
            {
                // 경과기록
                if (m_RIP001 == null) return null;
                else return m_RIP001.GetFileList();
            }
            else if ("ODD001".Equals(SC_DTL_FOM_CD))
            {
                // 기타자료
                if (m_ODD001 == null) return null;
                else return m_ODD001.GetFileList();
            }
            else
            {
                return null;
            }
        }

        public HIRA.EformEntry.Model.Document GetDocument(string ykiho)
        {
            List<CTBL_FOM_CZITM> list = GetList();
            if (list == null) return null;
            if (list.Count < 1) return null;

            HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();

            // Metadata 입력
            doc.Metadata["SUPL_DATA_FOM_CD"].Value = "CUS001";
            doc.Metadata["SC_DTL_FOM_CD"].Value = SC_DTL_FOM_CD;
            doc.Metadata["FOM_VER"].Value = "001";
            doc.Metadata["YKIHO"].Value = ykiho;
            doc.Metadata["DMD_NO"].Value = DMD_NO;
            doc.Metadata["RCV_NO"].Value = RCV_NO;
            doc.Metadata["RCV_YR"].Value = RCV_YR;
            doc.Metadata["BILL_SNO"].Value = BILL_SNO;
            doc.Metadata["SP_SNO"].Value = SP_SNO;
            doc.Metadata["INSUP_TP_CD"].Value = INSUP_TP_CD;
            //doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "10"; // <-- 10으로 해야한다고 함.
            doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "06"; // 2022.03.23 WOOIL - 10에서 06으로 변경
            doc.Metadata["DTL_BIZ_CD"].Value = "NDA"; // 2022.03.23 WOOIL - 신설
            doc.Metadata["HOSP_RNO"].Value = HOSP_RNO;
            doc.Metadata["PAT_NM"].Value = PAT_NM;
            doc.Metadata["PAT_JNO"].Value = I2A_RESID;
            doc.Metadata["REQ_DATA_NO"].Value = REQ_DATA_NO;

            // Table(TBL_FOM_CZITM) Column 선언
            doc.Tables["TBL_FOM_CZITM"].Columns.Add("SORT_SNO");
            doc.Tables["TBL_FOM_CZITM"].Columns.Add("YADM_TRMN_ID");
            doc.Tables["TBL_FOM_CZITM"].Columns.Add("YADM_TRMN_NM");
            doc.Tables["TBL_FOM_CZITM"].Columns.Add("DTL_TXT");
            doc.Tables["TBL_FOM_CZITM"].Columns.Add("LABEL_NM");

            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                doc.Tables["TBL_FOM_CZITM"].Rows.AddRow();
                doc.Tables["TBL_FOM_CZITM"].Rows[i]["SORT_SNO"].Value = list[i].SORT_SNO;
                doc.Tables["TBL_FOM_CZITM"].Rows[i]["YADM_TRMN_ID"].Value = list[i].YADM_TRMN_ID;
                doc.Tables["TBL_FOM_CZITM"].Rows[i]["YADM_TRMN_NM"].Value = list[i].YADM_TRMN_NM;
                doc.Tables["TBL_FOM_CZITM"].Rows[i]["DTL_TXT"].Value = list[i].DTL_TXT;
                doc.Tables["TBL_FOM_CZITM"].Rows[i]["LABEL_NM"].Value = list[i].LABEL_NM;
            }

            // 서식에 첨부할 파일이 있는 경우 첨부
            List<CLOCAL_FILE_PTH> files = GetFileList();
            int fileCount = files.Count;
            for (int i = 0; i < fileCount; i++)
            {
                doc.ApndDatas.Rows.AddRow();
                doc.ApndDatas.Rows[i]["LOCAL_FILE_PTH"].Value = files[i].LOCAL_FILE_PTH;
                doc.ApndDatas.Rows[i]["ROW_STAT"].Value = "C";
            }

            // 첨부파일
            doc.addDoc();

            return doc;
        }

        private void SaveData()
        {
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string strSysDate = CUtil.GetSysDate(conn);
                string strSysTime = CUtil.GetSysTime(conn);

                int count = 0;

                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
                sql += System.Environment.NewLine + "  FROM TI83";
                sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + REQ_DATA_NO + "'";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand qcmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = qcmd.ExecuteReader();
                if (reader.Read())
                {
                    count = Convert.ToInt32(reader["CNT"].ToString());
                }
                reader.Close();

                // MMS를 남기지 않기 위한 작업
                string user = m_User;
                if("MMS".Equals(user,StringComparison.CurrentCultureIgnoreCase)) user = "";

                if (count < 1)
                {
                    sql = "";
                    sql += System.Environment.NewLine + "INSERT INTO TI83(REQ_DATA_NO, REQ_STA_DD, REQ_END_DD, REQ_CLOS_YN, SUPL_DATA_FOM_CD, SUPL_DATA_FOM_CD_NM, SC_DTL_FOM_CD, SC_DTL_FOM_CD_NM, ASM_YM, DMD_NO, RCV_NO, RCV_YR, BILL_SNO, SP_SNO, INSUP_TP_CD, HOSP_RNO, PAT_NM, PAT_BTH, PAT_SEX, DIAG_YM, RCV_DD, RECU_FR_DD, RECU_END_DD, DGSBJT_CD_NM, MSICK_SICK_SYM, KOR_SICK_NM, SMIT_YN, SMIT_FDEC_DD, SYSDT, SYSTM, EMPID)";
                    sql += System.Environment.NewLine + "VALUES('" + REQ_DATA_NO + "','" + REQ_STA_DD + "','" + REQ_END_DD + "','" + REQ_CLOS_YN + "','" + SUPL_DATA_FOM_CD + "','" + SUPL_DATA_FOM_CD_NM + "','" + SC_DTL_FOM_CD + "','" + SC_DTL_FOM_CD_NM + "','" + ASM_YM + "','" + DMD_NO + "','" + RCV_NO + "','" + RCV_YR + "','" + BILL_SNO + "','" + SP_SNO + "','" + INSUP_TP_CD + "','" + HOSP_RNO + "','" + PAT_NM + "','" + PAT_BTH + "','" + PAT_SEX + "','" + DIAG_YM + "','" + RCV_DD + "','" + RECU_FR_DD + "','" + RECU_END_DD + "','" + DGSBJT_CD_NM + "','" + MSICK_SICK_SYM + "','" + KOR_SICK_NM + "','" + SMIT_YN + "','" + SMIT_FDEC_DD + "','" + strSysDate + "','" + strSysTime + "','" + user + "')";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TI83";
                    sql += System.Environment.NewLine + "   SET REQ_STA_DD='" + REQ_STA_DD +"'";
                    sql += System.Environment.NewLine + "     , REQ_END_DD='" + REQ_END_DD +"'";
                    sql += System.Environment.NewLine + "     , REQ_CLOS_YN='" + REQ_CLOS_YN +"'";
                    sql += System.Environment.NewLine + "     , SUPL_DATA_FOM_CD='" + SUPL_DATA_FOM_CD +"'";
                    sql += System.Environment.NewLine + "     , SUPL_DATA_FOM_CD_NM='" + SUPL_DATA_FOM_CD_NM +"'";
                    sql += System.Environment.NewLine + "     , SC_DTL_FOM_CD='" + SC_DTL_FOM_CD +"'";
                    sql += System.Environment.NewLine + "     , SC_DTL_FOM_CD_NM='" + SC_DTL_FOM_CD_NM +"'";
                    sql += System.Environment.NewLine + "     , ASM_YM='" + ASM_YM +"'";
                    sql += System.Environment.NewLine + "     , DMD_NO='" + DMD_NO +"'";
                    sql += System.Environment.NewLine + "     , RCV_NO='" + RCV_NO +"'";
                    sql += System.Environment.NewLine + "     , RCV_YR='" + RCV_YR +"'";
                    sql += System.Environment.NewLine + "     , BILL_SNO='" + BILL_SNO +"'";
                    sql += System.Environment.NewLine + "     , SP_SNO='" + SP_SNO +"'";
                    sql += System.Environment.NewLine + "     , INSUP_TP_CD='" + INSUP_TP_CD +"'";
                    sql += System.Environment.NewLine + "     , HOSP_RNO='" + HOSP_RNO +"'";
                    sql += System.Environment.NewLine + "     , PAT_NM='" + PAT_NM +"'";
                    sql += System.Environment.NewLine + "     , PAT_BTH='" + PAT_BTH +"'";
                    sql += System.Environment.NewLine + "     , PAT_SEX='" + PAT_SEX +"'";
                    sql += System.Environment.NewLine + "     , DIAG_YM='" + DIAG_YM +"'";
                    sql += System.Environment.NewLine + "     , RCV_DD='" + RCV_DD +"'";
                    sql += System.Environment.NewLine + "     , RECU_FR_DD='" + RECU_FR_DD +"'";
                    sql += System.Environment.NewLine + "     , RECU_END_DD='" + RECU_END_DD +"'";
                    sql += System.Environment.NewLine + "     , DGSBJT_CD_NM='" + DGSBJT_CD_NM +"'";
                    sql += System.Environment.NewLine + "     , MSICK_SICK_SYM='" + MSICK_SICK_SYM +"'";
                    sql += System.Environment.NewLine + "     , KOR_SICK_NM='" + KOR_SICK_NM +"'";
                    sql += System.Environment.NewLine + "     , SMIT_YN='" + SMIT_YN +"'";
                    sql += System.Environment.NewLine + "     , SMIT_FDEC_DD='" + SMIT_FDEC_DD +"'";
                    sql += System.Environment.NewLine + "     , SYSDT='" + strSysDate + "'";
                    sql += System.Environment.NewLine + "     , SYSTM='" + strSysTime + "'";
                    if ("".Equals(user)==false)
                    {
                        sql += System.Environment.NewLine + "     , EMPID='" + user + "'";
                    }
                    sql += System.Environment.NewLine + "WHERE REQ_DATA_NO='" + REQ_DATA_NO + "'";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        /*
        public void SaveFilePth()
        {
            string sql = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sql = "";
                sql += System.Environment.NewLine + "UPDATE TI83";
                sql += System.Environment.NewLine + "   SET LOCAL_FILE_PTH='" + LOCAL_FILE_PTH + "'";
                sql += System.Environment.NewLine + "WHERE REQ_DATA_NO='" + REQ_DATA_NO + "'";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD0110E
{
    class CTIF
    {
        public string BDODT; 
        public string QFYCD; 
        public string JRBY;
        public string PID;
        public string UNISQ; 
        public string SIMCS; 
        public string SEQ1;
        public string SEQ2; 
        public string OP;
        public string PRICD;
        public string BGIHO; 
        public string PRKNM; 
        public string NTDIV; 
        public string FCRFG; 
        public string DANGA; 
        public string DQTY;
        public string DDAY; 
        public string GUMAK; 
        public string EXDT;
        public string POS2; 
        public string MAFG; 
        public string ACTFG;
        public string EVENT; 
        public string DRGCD; 
        public string STTEX; 
        public string IPOS1; 
        public string ALLEX; 
        public string GRPCD; 
        public string GRPACT; 
        public string GRPNM;
        public string RSNCD; 
        public string REMARK; 
        public string FRDT;
        public string TODT; 
        public string PRIDT; 
        public string ELINENO; 
        public string OKCD;
        public string REFCD; 
        public string LOWFG; 
        public string CDENTDT;
        public string TPOS1;
        public string TPOS2; 
        public string TPOS3; 
        public string TPOS4; 
        public string CDGB;
        public string MULTIRMK;
        public string EXHM;
        public string CHRLT; 
        public string AFPFG; 
        public string CDCHGDT;
        public string HBPRICD; 
        public string CNTQTY;
        public string LOWRSNCD;
        public string LOWRSNRMK; 
        public string DANGACHK;
        public string DRADDFG;
        public string UPLMTAMT; 
        public string UPLMTCHAAMT;
        public string EDIENTDT;
        public string ADDFG1;
        public string BHEXFG; 
        public string SPFG;
        public string SPRT; 
        public string SPAMT; 
        public string SPPOS2; 
        public string JBPTFG; 
        public string DRIDLIST;
        public string INREFFG;
        public string INREFCD; 
        public string OYAKFG;
        public string BOSANGRT; 
        public string GUMAK2;
        public string DRG7_ADD_FG;
        public string DRG7_ADD_RT; 
        public string DRG7_ADD_GUMAK2;
        public string INREFFG2;
        public string INREFCD2; 
        public string DRG7_SEQ1; 
        public string DRG7_POS2;
        public string DRG7_ELINENO;

        public void SetValues(string p_iofg, OleDbDataReader reader)
        {
            BDODT = p_iofg == "2" ? reader["BDODT"].ToString() : reader["EXDATE"].ToString();
            QFYCD = reader["QFYCD"].ToString();
            JRBY = reader["JRBY"].ToString();
            PID = reader["PID"].ToString();
            UNISQ = reader["UNISQ"].ToString();
            SIMCS = reader["SIMCS"].ToString();
            SEQ1 = reader["SEQ1"].ToString();
            SEQ2 = reader["SEQ2"].ToString();
            OP = reader["OP"].ToString();
            PRICD = reader["PRICD"].ToString();
            BGIHO = reader["BGIHO"].ToString();
            PRKNM = reader["PRKNM"].ToString();
            NTDIV = reader["NTDIV"].ToString();
            FCRFG = reader["FCRFG"].ToString();
            DANGA = reader["DANGA"].ToString();
            DQTY = reader["DQTY"].ToString();
            DDAY = reader["DDAY"].ToString();
            GUMAK = reader["GUMAK"].ToString();
            EXDT = reader["EXDT"].ToString();
            POS2 = reader["POS2"].ToString();
            MAFG = reader["MAFG"].ToString();
            ACTFG = reader["ACTFG"].ToString();
            EVENT = reader["EVENT"].ToString();
            DRGCD = reader["DRGCD"].ToString();
            STTEX = reader["STTEX"].ToString();
            IPOS1 = reader["IPOS1"].ToString();
            ALLEX = reader["ALLEX"].ToString();
            GRPCD = reader["GRPCD"].ToString();
            GRPACT = reader["GRPACT"].ToString();
            GRPNM = reader["GRPNM"].ToString();
            RSNCD = reader["RSNCD"].ToString();
            REMARK = reader["REMARK"].ToString();
            FRDT = reader["FRDT"].ToString();
            TODT = reader["TODT"].ToString();
            PRIDT = reader["PRIDT"].ToString();
            ELINENO = reader["ELINENO"].ToString();
            OKCD = reader["OKCD"].ToString();
            REFCD = reader["REFCD"].ToString();
            LOWFG = reader["LOWFG"].ToString();
            CDENTDT = reader["CDENTDT"].ToString();
            TPOS1 = reader["TPOS1"].ToString();
            TPOS2 = reader["TPOS2"].ToString();
            TPOS3 = reader["TPOS3"].ToString();
            TPOS4 = reader["TPOS4"].ToString();
            CDGB = reader["CDGB"].ToString();
            MULTIRMK = reader["MULTIRMK"].ToString();
            EXHM = reader["EXHM"].ToString();
            CHRLT = reader["CHRLT"].ToString();
            AFPFG = reader["AFPFG"].ToString();
            CDCHGDT = reader["CDCHGDT"].ToString();
            HBPRICD = reader["HBPRICD"].ToString();
            CNTQTY = reader["CNTQTY"].ToString();
            LOWRSNCD = reader["LOWRSNCD"].ToString();
            LOWRSNRMK = reader["LOWRSNRMK"].ToString();
            DANGACHK = reader["DANGACHK"].ToString();
            DRADDFG = reader["DRADDFG"].ToString();
            UPLMTAMT = reader["UPLMTAMT"].ToString();
            UPLMTCHAAMT = reader["UPLMTCHAAMT"].ToString();
            EDIENTDT = reader["EDIENTDT"].ToString();
            ADDFG1 = reader["ADDFG1"].ToString();
            BHEXFG = reader["BHEXFG"].ToString();
            SPFG = reader["SPFG"].ToString();
            SPRT = reader["SPRT"].ToString();
            SPAMT = reader["SPAMT"].ToString();
            SPPOS2 = reader["SPPOS2"].ToString();
            JBPTFG = reader["JBPTFG"].ToString();
            DRIDLIST = reader["DRIDLIST"].ToString();
            INREFFG = reader["INREFFG"].ToString();
            INREFCD = reader["INREFCD"].ToString();
            OYAKFG = reader["OYAKFG"].ToString();
            BOSANGRT = reader["BOSANGRT"].ToString();
            GUMAK2 = reader["GUMAK2"].ToString();
            DRG7_ADD_FG = reader["DRG7_ADD_FG"].ToString();
            DRG7_ADD_RT = reader["DRG7_ADD_RT"].ToString();
            DRG7_ADD_GUMAK2 = reader["DRG7_ADD_GUMAK2"].ToString();
            INREFFG2 = reader["INREFFG2"].ToString();
            INREFCD2 = reader["INREFCD2"].ToString();
            DRG7_SEQ1 = reader["DRG7_SEQ1"].ToString();
            DRG7_POS2 = reader["DRG7_POS2"].ToString();
            DRG7_ELINENO = reader["DRG7_ELINENO"].ToString();
        }

        public string GetFRec()
        {
            string f_rec = "";
            f_rec += OP + (char)21; // 1
            f_rec += PRICD + (char)21; // 2
            f_rec += BGIHO + (char)21; // 3
            f_rec += "" + (char)21; // 4 PRINM
            f_rec += "" + (char)21; // 5 A100
            f_rec += CHRLT + (char)21; // 6
            f_rec += AFPFG + (char)21; // 7
            f_rec += NTDIV + (char)21; // 8
            f_rec += FCRFG + (char)21; // 9
            f_rec += DRADDFG + (char)21; // 10
            f_rec += UPLMTAMT + (char)21; // 11
            f_rec += DANGA + (char)21; // 12
            f_rec += CNTQTY + (char)21; // 13
            f_rec += DQTY + (char)21; // 14
            f_rec += DDAY + (char)21; // 15
            f_rec += GUMAK + (char)21; // 16
            f_rec += UPLMTCHAAMT + (char)21; // 17
            f_rec += LOWRSNCD + (char)21; // 18
            f_rec += EXDT + (char)21; // 19
            f_rec += POS2 + (char)21; // 20
            f_rec += MAFG + (char)21; // 21
            f_rec += ACTFG + (char)21; // 22
            f_rec += EVENT + (char)21; // 23
            f_rec += DRGCD + (char)21; // 24
            f_rec += STTEX + (char)21; // 25
            f_rec += IPOS1 + (char)21; // 26
            f_rec += ALLEX + (char)21; // 27
            f_rec += SEQ1 + (char)21; // 28
            f_rec += SEQ2 + (char)21; // 29
            f_rec += GRPCD + (char)21; // 30
            f_rec += GRPACT + (char)21; // 31
            f_rec += GRPNM + (char)21; // 32
            f_rec += RSNCD + (char)21; // 33
            f_rec += PRKNM + (char)21; // 34
            f_rec += REMARK + (char)21; // 35
            f_rec += FRDT + (char)21; // 36
            f_rec += TODT + (char)21; // 37
            f_rec += REFCD + (char)21; // 38
            f_rec += OKCD + (char)21; // 39
            f_rec += "" + (char)21; // 40 MCHVAL
            f_rec += LOWFG + (char)21; // 41
            f_rec += "" + (char)21; // 42 LKUMAK
            f_rec += CDENTDT + (char)21; // 43
            f_rec += EDIENTDT + (char)21; // 44
            f_rec += MULTIRMK + (char)21; //'45
            f_rec += TPOS1 + (char)21; // 46
            f_rec += TPOS2 + (char)21; // 47
            f_rec += TPOS3 + (char)21; // 48
            f_rec += TPOS4 + (char)21; // 49
            f_rec += EXHM + (char)21; // 50
            f_rec += CDCHGDT + (char)21; // 51
            f_rec += HBPRICD + (char)21; // 52
            f_rec += LOWRSNRMK + (char)21; // 53
            f_rec += "" + (char)21; // 54 LOWCTCNT
            f_rec += DANGACHK + (char)21; // 55
            f_rec += CDGB + (char)21; // 56
            f_rec += "" + (char)21; // 57 HNO
            f_rec += "" + (char)21; // 58 MNO
            f_rec += "" + (char)21; // 59 JUBSUDT
            f_rec += ADDFG1 + (char)21; // 60
            f_rec += BHEXFG + (char)21; // 61
            f_rec += "" + (char)21; // 62
            f_rec += "" + (char)21; // 63
            f_rec += "" + (char)21; // 64
            f_rec += "" + (char)21; // 65
            f_rec += "" + (char)21; // 66
            f_rec += INREFFG + (char)21; // 67
            f_rec += INREFCD + (char)21; // 68
            f_rec += OYAKFG + (char)21; // 69
            f_rec += INREFFG2 + (char)21; // 70
            f_rec += INREFCD2 + (char)21; // 71
            return f_rec;
        }

        public void ReplaceValues(string p_val)
        {
            string[] val = p_val.Split((char)21);
            OP = ""; // 1
            PRICD = val[1]; // 2
            BGIHO = val[2]; // 3
            //PRINM = val[3]; // 4
            //A100 = val[4]; // 5
            CHRLT = val[5]; // 6
            AFPFG = val[6]; // 7
            NTDIV = val[7]; // 8
            FCRFG = val[8]; // 9
            DRADDFG = val[9]; // 10
            UPLMTAMT = val[10]; // 11
            DANGA = val[11]; // 12
            CNTQTY = val[12]; // 13
            DQTY = val[13]; // 14
            DDAY = val[14]; // 15
            GUMAK = val[15]; // 16
            UPLMTCHAAMT = val[16]; // 17
            LOWRSNCD = val[17]; // 18
            EXDT = val[18]; // 19
            POS2 = val[19]; // 20
            MAFG = val[20]; // 21
            ACTFG = val[21]; // 22
            EVENT = val[22]; // 23
            DRGCD = val[23]; // 24
            STTEX = val[24]; // 25
            IPOS1 = val[25]; // 26
            ALLEX = val[26]; // 27
            //SEQ1 = val[27]; // 28
            //SEQ2 = val[28]; // 29
            GRPCD = val[29]; // 30
            GRPACT = val[30]; // 31
            GRPNM = val[31]; // 32
            RSNCD = val[32]; // 33
            PRKNM = val[33]; // 34
            REMARK = val[34]; // 35
            //FRDT = val[35]; // 36
            //TODT = val[36]; // 37
            REFCD = val[37]; // 38
            OKCD = val[38]; // 39
            //MCHVAL = val[39]; // 40
            LOWFG = val[40]; // 41
            //LKUMAK = val[41]; // 42
            CDENTDT = val[42]; // 43
            EDIENTDT = val[43]; // 44
            MULTIRMK = val[44]; // 45
            TPOS1 = val[45]; // 46
            TPOS2 = val[46]; // 47
            TPOS3 = val[47]; // 48
            TPOS4 = val[48]; // 49
            EXHM = val[49]; // 50
            CDCHGDT = val[50]; // 51
            HBPRICD = val[51]; // 52
            LOWRSNRMK = val[52]; // 53
            //LOWCTCNT = val[53]; // 54
            DANGACHK = val[54]; // 55
            CDGB = val[55]; // 56
            //HNO = val[56]; // 57
            //MNO = val[57]; // 58
            //JUBSUDT = val[58]; // 59
            ADDFG1 = val[59]; // 60
            BHEXFG = val[60]; // 61
            // 62
            // ...
            // 67
            INREFFG = val[67]; // 68
            INREFCD = val[68]; // 69
            OYAKFG = val[69]; // 70
            BOSANGRT = val[70]; // 71
            GUMAK2 = val[71]; // 72
            // 73
            // ...
            // 94
            DRG7_ADD_FG = val[94]; // 95
            DRG7_ADD_RT = val[95]; // 96
            DRG7_ADD_GUMAK2 = val[96]; // 97
            // 98
            INREFFG2 = val[98]; // 99
            INREFCD2 = val[99]; // 100
            DRG7_SEQ1 = val[100]; // 101
            DRG7_POS2 = val[101]; // 102
            DRG7_ELINENO = val[102]; // 103

            int int_pos1 = 0;
            int.TryParse(IPOS1, out int_pos1);
            IPOS1 = (int_pos1 - 10).ToString();
        }

        public void SaveValues(string p_iofg, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string tTI2F = "";
            string fBDODT="";
            if(p_iofg=="2"){
                tTI2F="TI2F";
                fBDODT="BDODT";
            }else{
                tTI2F="TI1F";
                fBDODT="EXDATE";
            }

            string sql = "";

            // SEQ1은 11부터 시작한다.
            int ipos1 = 0;
            int.TryParse(IPOS1, out ipos1);
            ipos1 += 10;

            //SEQ2의 중복을 방지하기 위하여
            string new_seq2 = SEQ2;
            if (SEQ1 != ipos1.ToString())
            {
                int max_seq2 = 0;
                sql = "";
                sql += Environment.NewLine + "SELECT ISNULL(MAX(SEQ2),0) AS MAX_SEQ2";
                sql += Environment.NewLine + "  FROM " + tTI2F + "";
                sql += Environment.NewLine + " WHERE " + fBDODT + "='" + BDODT + "'";
                sql += Environment.NewLine + "   AND QFYCD='" + QFYCD + "'";
                sql += Environment.NewLine + "   AND JRBY='" + JRBY + "'";
                sql += Environment.NewLine + "   AND PID='" + PID + "'";
                sql += Environment.NewLine + "   AND UNISQ=" + UNISQ + "";
                sql += Environment.NewLine + "   AND SIMCS=" + SIMCS + "";
                sql += Environment.NewLine + "   AND SEQ1=" + ipos1.ToString() + "";
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
                {
                    int.TryParse(reader["MAX_SEQ2"].ToString(), out max_seq2);
                    return false;
                });
                new_seq2 = (max_seq2 + 1).ToString();
            }
            

            // 자료 갱신
            sql = "";
            sql += Environment.NewLine + "UPDATE " + tTI2F + "";
            sql += Environment.NewLine + "   SET OP=?";
            sql += Environment.NewLine + "     , PRICD=?";
            sql += Environment.NewLine + "     , BGIHO=?"; 
            sql += Environment.NewLine + "     , PRKNM=?"; 
            sql += Environment.NewLine + "     , NTDIV=?";
            sql += Environment.NewLine + "     , FCRFG=?";
            sql += Environment.NewLine + "     , DANGA=?";
            sql += Environment.NewLine + "     , DQTY=?";
            sql += Environment.NewLine + "     , DDAY=?"; 
            sql += Environment.NewLine + "     , GUMAK=?"; 
            sql += Environment.NewLine + "     , EXDT=?";
            sql += Environment.NewLine + "     , POS2=?"; 
            sql += Environment.NewLine + "     , MAFG=?"; 
            sql += Environment.NewLine + "     , ACTFG=?";
            sql += Environment.NewLine + "     , EVENT=?"; 
            sql += Environment.NewLine + "     , DRGCD=?"; 
            sql += Environment.NewLine + "     , STTEX=?"; 
            sql += Environment.NewLine + "     , IPOS1=?"; 
            sql += Environment.NewLine + "     , ALLEX=?"; 
            sql += Environment.NewLine + "     , GRPCD=?"; 
            sql += Environment.NewLine + "     , GRPACT=?"; 
            sql += Environment.NewLine + "     , GRPNM=?";
            sql += Environment.NewLine + "     , RSNCD=?"; 
            sql += Environment.NewLine + "     , REMARK=?"; 
            sql += Environment.NewLine + "     , FRDT=?";
            sql += Environment.NewLine + "     , TODT=?"; 
            sql += Environment.NewLine + "     , PRIDT=?"; 
            sql += Environment.NewLine + "     , ELINENO=?"; 
            sql += Environment.NewLine + "     , OKCD=?";
            sql += Environment.NewLine + "     , REFCD=?"; 
            sql += Environment.NewLine + "     , LOWFG=?"; 
            sql += Environment.NewLine + "     , CDENTDT=?";
            sql += Environment.NewLine + "     , TPOS1=?";
            sql += Environment.NewLine + "     , TPOS2=?"; 
            sql += Environment.NewLine + "     , TPOS3=?"; 
            sql += Environment.NewLine + "     , TPOS4=?"; 
            sql += Environment.NewLine + "     , CDGB=?";
            sql += Environment.NewLine + "     , MULTIRMK=?";
            sql += Environment.NewLine + "     , EXHM=?";
            sql += Environment.NewLine + "     , CHRLT=?"; 
            sql += Environment.NewLine + "     , AFPFG=?"; 
            sql += Environment.NewLine + "     , CDCHGDT=?";
            sql += Environment.NewLine + "     , HBPRICD=?"; 
            sql += Environment.NewLine + "     , CNTQTY=?";
            sql += Environment.NewLine + "     , LOWRSNCD=?";
            sql += Environment.NewLine + "     , LOWRSNRMK=?"; 
            sql += Environment.NewLine + "     , DANGACHK=?";
            sql += Environment.NewLine + "     , DRADDFG=?";
            sql += Environment.NewLine + "     , UPLMTAMT=?"; 
            sql += Environment.NewLine + "     , UPLMTCHAAMT=?";
            sql += Environment.NewLine + "     , EDIENTDT=?";
            sql += Environment.NewLine + "     , ADDFG1=?";
            sql += Environment.NewLine + "     , BHEXFG=?"; 
            sql += Environment.NewLine + "     , SPFG=?";
            sql += Environment.NewLine + "     , SPRT=?"; 
            sql += Environment.NewLine + "     , SPAMT=?"; 
            sql += Environment.NewLine + "     , SPPOS2=?"; 
            sql += Environment.NewLine + "     , JBPTFG=?"; 
            sql += Environment.NewLine + "     , DRIDLIST=?";
            sql += Environment.NewLine + "     , INREFFG=?";
            sql += Environment.NewLine + "     , INREFCD=?"; 
            sql += Environment.NewLine + "     , OYAKFG=?";
            sql += Environment.NewLine + "     , BOSANGRT=?"; 
            sql += Environment.NewLine + "     , GUMAK2=?";
            sql += Environment.NewLine + "     , DRG7_ADD_FG=?";
            sql += Environment.NewLine + "     , DRG7_ADD_RT=?"; 
            sql += Environment.NewLine + "     , DRG7_ADD_GUMAK2=?";
            sql += Environment.NewLine + "     , INREFFG2=?";
            sql += Environment.NewLine + "     , INREFCD2=?"; 
            sql += Environment.NewLine + "     , DRG7_SEQ1=?"; 
            sql += Environment.NewLine + "     , DRG7_POS2=?";
            sql += Environment.NewLine + "     , DRG7_ELINENO=?";
            sql += Environment.NewLine + "     , SEQ1=?";
            sql += Environment.NewLine + "     , SEQ2=?";
            sql += Environment.NewLine + " WHERE " + fBDODT + "=?";
            sql += Environment.NewLine + "   AND QFYCD=?";
            sql += Environment.NewLine + "   AND JRBY=?";
            sql += Environment.NewLine + "   AND PID=?";
            sql += Environment.NewLine + "   AND UNISQ=?";
            sql += Environment.NewLine + "   AND SIMCS=?";
            sql += Environment.NewLine + "   AND SEQ1=?";
            sql += Environment.NewLine + "   AND SEQ2=?";

            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn, p_tran))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", OP));
                cmd.Parameters.Add(new OleDbParameter("@2", PRICD));
                cmd.Parameters.Add(new OleDbParameter("@3", BGIHO)); 
                cmd.Parameters.Add(new OleDbParameter("@4", PRKNM)); 
                cmd.Parameters.Add(new OleDbParameter("@5", NTDIV)); 
                cmd.Parameters.Add(new OleDbParameter("@6", FCRFG));
                cmd.Parameters.Add(new OleDbParameter("@7", DANGA == "" ? "0" : DANGA));
                cmd.Parameters.Add(new OleDbParameter("@8", DQTY == "" ? "0" : DQTY));
                cmd.Parameters.Add(new OleDbParameter("@9", DDAY == "" ? "0" : DDAY)); 
                cmd.Parameters.Add(new OleDbParameter("@10", GUMAK == "" ? "0" : GUMAK)); 
                cmd.Parameters.Add(new OleDbParameter("@11", EXDT));
                cmd.Parameters.Add(new OleDbParameter("@12", POS2 == "" ? "0" : POS2)); 
                cmd.Parameters.Add(new OleDbParameter("@13", MAFG)); 
                cmd.Parameters.Add(new OleDbParameter("@14", ACTFG));
                cmd.Parameters.Add(new OleDbParameter("@15", EVENT)); 
                cmd.Parameters.Add(new OleDbParameter("@16", DRGCD)); 
                cmd.Parameters.Add(new OleDbParameter("@17", STTEX));
                cmd.Parameters.Add(new OleDbParameter("@18", IPOS1 == "" ? "0" : IPOS1)); 
                cmd.Parameters.Add(new OleDbParameter("@19", ALLEX)); 
                cmd.Parameters.Add(new OleDbParameter("@20", GRPCD)); 
                cmd.Parameters.Add(new OleDbParameter("@21", GRPACT)); 
                cmd.Parameters.Add(new OleDbParameter("@22", GRPNM));
                cmd.Parameters.Add(new OleDbParameter("@23", RSNCD)); 
                cmd.Parameters.Add(new OleDbParameter("@24", REMARK)); 
                cmd.Parameters.Add(new OleDbParameter("@25", FRDT));
                cmd.Parameters.Add(new OleDbParameter("@26", TODT)); 
                cmd.Parameters.Add(new OleDbParameter("@27", PRIDT));
                cmd.Parameters.Add(new OleDbParameter("@28", ELINENO == "" ? "0" : ELINENO)); 
                cmd.Parameters.Add(new OleDbParameter("@29", OKCD));
                cmd.Parameters.Add(new OleDbParameter("@30", REFCD)); 
                cmd.Parameters.Add(new OleDbParameter("@31", LOWFG)); 
                cmd.Parameters.Add(new OleDbParameter("@32", CDENTDT));
                cmd.Parameters.Add(new OleDbParameter("@33", TPOS1));
                cmd.Parameters.Add(new OleDbParameter("@34", TPOS2)); 
                cmd.Parameters.Add(new OleDbParameter("@35", TPOS3)); 
                cmd.Parameters.Add(new OleDbParameter("@36", TPOS4)); 
                cmd.Parameters.Add(new OleDbParameter("@37", CDGB));
                cmd.Parameters.Add(new OleDbParameter("@38", MULTIRMK));
                cmd.Parameters.Add(new OleDbParameter("@39", EXHM));
                cmd.Parameters.Add(new OleDbParameter("@40", CHRLT)); 
                cmd.Parameters.Add(new OleDbParameter("@41", AFPFG)); 
                cmd.Parameters.Add(new OleDbParameter("@42", CDCHGDT));
                cmd.Parameters.Add(new OleDbParameter("@43", HBPRICD));
                cmd.Parameters.Add(new OleDbParameter("@44", CNTQTY == "" ? "1" : CNTQTY));
                cmd.Parameters.Add(new OleDbParameter("@45", LOWRSNCD));
                cmd.Parameters.Add(new OleDbParameter("@46", LOWRSNRMK)); 
                cmd.Parameters.Add(new OleDbParameter("@47", DANGACHK));
                cmd.Parameters.Add(new OleDbParameter("@48", DRADDFG));
                cmd.Parameters.Add(new OleDbParameter("@49", UPLMTAMT == "" ? "0" : UPLMTAMT));
                cmd.Parameters.Add(new OleDbParameter("@50", UPLMTCHAAMT == "" ? "0" : UPLMTCHAAMT));
                cmd.Parameters.Add(new OleDbParameter("@51", EDIENTDT));
                cmd.Parameters.Add(new OleDbParameter("@52", ADDFG1));
                cmd.Parameters.Add(new OleDbParameter("@53", BHEXFG)); 
                cmd.Parameters.Add(new OleDbParameter("@54", SPFG));
                cmd.Parameters.Add(new OleDbParameter("@55", SPRT));
                cmd.Parameters.Add(new OleDbParameter("@56", SPAMT == "" ? "0" : SPAMT)); 
                cmd.Parameters.Add(new OleDbParameter("@57", SPPOS2 == "" ? "0" : SPPOS2)); 
                cmd.Parameters.Add(new OleDbParameter("@58", JBPTFG)); 
                cmd.Parameters.Add(new OleDbParameter("@59", DRIDLIST));
                cmd.Parameters.Add(new OleDbParameter("@60", INREFFG));
                cmd.Parameters.Add(new OleDbParameter("@61", INREFCD)); 
                cmd.Parameters.Add(new OleDbParameter("@62", OYAKFG));
                cmd.Parameters.Add(new OleDbParameter("@63", BOSANGRT == "" ? "0" : BOSANGRT));
                cmd.Parameters.Add(new OleDbParameter("@64", GUMAK2 == "" ? "0" : GUMAK2));
                cmd.Parameters.Add(new OleDbParameter("@65", DRG7_ADD_FG));
                cmd.Parameters.Add(new OleDbParameter("@66", DRG7_ADD_RT == "" ? "0" : DRG7_ADD_RT));
                cmd.Parameters.Add(new OleDbParameter("@67", DRG7_ADD_GUMAK2 == "" ? "0" : DRG7_ADD_GUMAK2));
                cmd.Parameters.Add(new OleDbParameter("@68", INREFFG2));
                cmd.Parameters.Add(new OleDbParameter("@69", INREFCD2));
                cmd.Parameters.Add(new OleDbParameter("@70", DRG7_SEQ1 == "" ? "0" : DRG7_SEQ1));
                cmd.Parameters.Add(new OleDbParameter("@71", DRG7_POS2 == "" ? "0" : DRG7_POS2));
                cmd.Parameters.Add(new OleDbParameter("@72", DRG7_ELINENO == "" ? "0" : DRG7_ELINENO));
                cmd.Parameters.Add(new OleDbParameter("@73", ipos1.ToString()));
                cmd.Parameters.Add(new OleDbParameter("@74", new_seq2));

                cmd.Parameters.Add(new OleDbParameter("@75", BDODT));
                cmd.Parameters.Add(new OleDbParameter("@76", QFYCD)); 
                cmd.Parameters.Add(new OleDbParameter("@77", JRBY));
                cmd.Parameters.Add(new OleDbParameter("@78", PID));
                cmd.Parameters.Add(new OleDbParameter("@79", UNISQ)); 
                cmd.Parameters.Add(new OleDbParameter("@80", SIMCS)); 
                cmd.Parameters.Add(new OleDbParameter("@81", SEQ1));
                cmd.Parameters.Add(new OleDbParameter("@82", SEQ2));

                int cnt = cmd.ExecuteNonQuery();
            }
                
        }

    }
}

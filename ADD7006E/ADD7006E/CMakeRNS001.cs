using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRNS001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RNS001_LIST.Clear();

            // 수술기록자료
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT PID,OPDT,DPTCD,OPSEQ";
            sql += System.Environment.NewLine + "     , OR_INDT,OR_INTM,PT_OUTDT,PT_OUTTM,OP_STDT,OP_STTM,SRNURS1,CIRNURS1,SYSDT,SYSTM,TIMEOUTCHK_1,TIMEOUTCHK_2,PREDXNM,POSTDXNM";
            sql += System.Environment.NewLine + "     , IRR_1,IRR_2,SPR_1,SPR_2,INJ_1,INJ_2,ETC1_1,ETC1_2,ETC2_1,ETC2_2";
            sql += System.Environment.NewLine + "  FROM TU94";
            sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND OPDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND OPDT<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID, OPDT, DPTCD, OPSEQ, SEQ";


            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRNS001 data = new CDataRNS001();
                data.Clear();

                // TU94
                data.OR_INDT = row["OR_INDT"].ToString(); // 수술실 입실일자(YYYYMMDD)
                data.OR_INTM = row["OR_INTM"].ToString(); // 수술실 입실시간(HH:MM)
                data.PT_OUTDT = row["PT_OUTDT"].ToString(); // 수술실 퇴실일자(YYYYMMDD)
                data.PT_OUTTM = row["PT_OUTTM"].ToString(); // 수술실 퇴실시간(HH:MM)
                data.OP_STDT = row["OP_STDT"].ToString(); // 수술 시작일자(YYYYMMDD)
                data.OP_STTM = row["OP_STTM"].ToString(); // 수술 시작시간(HH:MM)
                data.OP_ENDDT = row["OP_ENDDT"].ToString(); // 수술 종료일자(YYYYMMDD)
                data.OP_ENDTM = row["OP_ENDTM"].ToString(); // 수술 종료시간(HH:MM)
                data.SRNURS1 = row["SRNURS1"].ToString(); // 소독 간호사 성명
                data.CIRNURS1 = row["CIRNURS1"].ToString(); // 순회 간호사 성명
                data.SYSDT = row["SYSDT"].ToString(); // 작성일자
                data.SYSTM = row["SYSTM"].ToString(); // 작성시간
                data.TIMEOUTCHK_1 = row["TIMEOUTCHK_1"].ToString(); // Time Out 시행여부(1이면 시행)
                data.TIMEOUTCHK_2 = row["TIMEOUTCHK_2"].ToString(); // Time Out 시행여부(1이면 미시행)
                data.PREDXNM = row["PREDXNM"].ToString(); // 수술 전 진단명
                data.POSTDXNM = row["POSTDXNM"].ToString(); // 수술 후 진단명
                data.POSTOPNM = row["POSTOPNM"].ToString(); // 수술 후 수술명

                // TU94
                string irr_1 = row["IRR_1"].ToString();
                string spr_1 = row["SPR_1"].ToString();
                string inj_1 = row["INJ_1"].ToString();
                string etc1_1 = row["ETC1_1"].ToString();
                string etc2_1 = row["ETC2_1"].ToString();

                if (irr_1 != "")
                {
                    data.ONM.Add(irr_1); // 약품명
                    data.QTY.Add(row["IRR_2"].ToString()); // 투여량
                }
                if (spr_1 != "")
                {
                    data.ONM.Add(spr_1); // 약품명
                    data.QTY.Add(row["SPR_2"].ToString()); // 투여량
                }
                if (inj_1 != "")
                {
                    data.ONM.Add(inj_1); // 약품명
                    data.QTY.Add(row["INJ_2"].ToString()); // 투여량
                }
                if (etc1_1 != "")
                {
                    data.ONM.Add(etc1_1); // 약품명
                    data.QTY.Add(row["ETC1_2"].ToString()); // 투여량
                }
                if (etc2_1 != "")
                {
                    data.ONM.Add(etc2_1); // 약품명
                    data.QTY.Add(row["ETC2_2"].ToString()); // 투여량
                }





                // TU94A
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT TUBE_1,TUBE_2,TUBE_3,TUBE_4";
                sql2 += System.Environment.NewLine + "  FROM TU94A";
                sql2 += System.Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.TUBE_1.Add(row["TUBE_1"].ToString()); // 삽입관 종류
                    data.TUBE_2.Add(row["TUBE_2"].ToString()); // 삽입관 크기
                    data.TUBE_3.Add(row["TUBE_3"].ToString()); // 삽입관 부위
                    data.TUBE_4.Add(row["TUBE_4"].ToString()); // 삽입관 수량

                    return MetroLib.SqlHelper.CONTINUE;
                });


                // TU94C
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT GUM_1,GUM_2,GUM_3";
                sql2 += System.Environment.NewLine + "  FROM TU94C";
                sql2 += System.Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.GUM_1.Add(row["GUM_1"].ToString()); // 검체종류
                    data.GUM_2.Add(row["GUM_2"].ToString()); // 검체부위
                    data.GUM_3.Add(row["GUM_2"].ToString()); // 검체개수

                    return MetroLib.SqlHelper.CONTINUE;
                });

                p_dsdata.RNS001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

        }
    }
}

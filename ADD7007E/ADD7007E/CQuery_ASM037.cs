using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery_ASM037 : CQuery
    {
        public List<CDataASM037_003> Query_ASM037(OleDbConnection conn, string frdt, string todt)
        {
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM037_003> list = new List<CDataASM037_003>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2A A";
            sql += Environment.NewLine + " WHERE A.BDODT>='" + frdt + "'";
            sql += Environment.NewLine + "   AND A.BDODT<='" + todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PID, A.STEDT";

            int no = 0;
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 사용가능한 자료인지 점검
                if (Check_ASM037(row, conn) == true)
                {
                    CDataASM037_003 data = new CDataASM037_003();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM037(DataRow p_row, OleDbConnection p_conn)
        {
            System.Windows.Forms.Application.DoEvents();

            // 수혈
            // 적혈구 제제 수혈이나 평가대상 수술을 청구한 상급종합병원, 종합병원, 병원, 의원
            // 슬관절치환술[단측], 척추후방고정술[1 Level]
            // 슬관절치환술[양측], 척추후방고정술[2 Level]은 수혈률(지표4), 수혈량(지표8) 모니터링
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2F I2F INNER JOIN TA02 A02 ON A02.PRICD=I2F.PRICD";
            sql += Environment.NewLine + "                                   AND A02.CREDT<=(SELECT MAX(X.CREDT)";
            sql += Environment.NewLine + "                                                     FROM TA02 X";
            sql += Environment.NewLine + "                                                    WHERE X.PRICD=I2F.PRICD";
            sql += Environment.NewLine + "                                                      AND X.CREDT<=LEFT(I2F.EXDT, 8)";
            sql += Environment.NewLine + "                                                )";
            sql += Environment.NewLine + " WHERE I2F.BDODT = '" + p_row["BDODT"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.QFYCD = '" + p_row["QFYCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.JRBY = '" + p_row["JRBY"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.PID = '" + p_row["PID"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.UNISQ = '" + p_row["UNISQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.SIMCS = '" + p_row["SIMCS"].ToString() + "'";
            sql += Environment.NewLine + "   AND A02.GUBUN = '1'"; // 수가만
            sql += Environment.NewLine + " ORDER BY I2F.ELINENO";

            int no = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 
                string bgiho = row["BGIHO"].ToString();
                if (IsCode(bgiho))
                {
                    no++;
                    return MetroLib.SqlHelper.BREAK;
                }
                return MetroLib.SqlHelper.CONTINUE;
            });

            return (no > 0);
        }

        private bool IsCode(string bgiho)
        {
            // 적혈구제제
            if (bgiho.StartsWith("X2021")) return true; //농축적혈구, 전혈 320ml 기준
            if (bgiho.StartsWith("X2022")) return true; //농축적혈구, 전혈 400ml 기준
            if (bgiho.StartsWith("X2031")) return true; //세척적혈구, 전혈 320ml 기준
            if (bgiho.StartsWith("X2032")) return true; //세척적혈구, 전혈 400ml 기준
            if (bgiho.StartsWith("X2131")) return true; //동결해동적혈구, 전혈 320ml 기준
            if (bgiho.StartsWith("X2132")) return true; //동결해동적혈구, 전혈 400ml 기준
            if (bgiho.StartsWith("X2091")) return true; //백혈구제거적혈구, 전혈 320ml 기준
            if (bgiho.StartsWith("X2092")) return true; //백혈구제거적혈구, 전혈 400ml 기준
            if (bgiho.StartsWith("X2111")) return true; //백혈구여과제거적혈구, 전혈 320ml 기준
            if (bgiho.StartsWith("X2112")) return true; //백혈구여과제거적혈구, 전혈 400ml 기준
            if (bgiho.StartsWith("X2512")) return true; //복합성분채집 적혈구(190ml)
            if (bgiho.StartsWith("X2515")) return true; //성분채집 적혈구(190ml)

            // 평가대상 수술코드
            if (bgiho.StartsWith("N2072")) return true; //인공관절치환술-전치환[슬관절]
            if (bgiho.StartsWith("N0469")) return true; //척추고정술[기기,기구사용고정포함]-후방고정-요추
            if (bgiho.StartsWith("N2470")) return true; //척추고정술[기기,기구사용고정포함]-후방고정-요추-Cage를이용한추체간유합술            
            
            return false;
        }

    }
}

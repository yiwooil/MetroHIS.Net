using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CRID001 : CCUS001
    {

        // 퇴원요약

        public CRID001(string p_User)
        {
            m_User = p_User;
        }

        public void SetValues(CTV100 v100, string p_REQ_DATA_NO)
        {
            try
            {
                m_List.Clear();
                string tmp = "";
                if (v100.DX_COUNT > 0)
                {
                    tmp = ""; 
                    for (int i = 0; i < v100.DX_COUNT; i++)
                    {
                        if (i == 0)
                        {
                            // 주진단
                            AddList("DX_DISECD0", "주진단", v100.DX_DISECD[i] + " " + v100.DX_DXD[i]);
                        }
                        else
                        {
                            // 기타진단
                            tmp += v100.DX_DISECD[i] + " " + v100.DX_DXD[i];
                            tmp += Environment.NewLine;
                        }
                    }
                    AddList("DX_DISECD9", "기타진단", tmp);
                }
                // 주호소
                AddList("COMPLAINT", "주호소", v100.COMPLAINT);
                // 입원경과 및 치료과정 요약
                AddList("COT", "입원경과 및 치료과정 요약", v100.COT);
                // 주요 처치.시술 및 수술내용
                if (v100.OPCOUNT > 0)
                {
                    tmp = "";
                    for (int i = 0; i < v100.OPCOUNT; i++)
                    {
                        tmp += v100.OPDT[i].Substring(0, 8) + " "; // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                        tmp += v100.OPNAME[i] + " "; // 시술.처치 및 수술명
                        //tmp += v100.ICD9CM[i] + " "; // ICD-9-CM volumn3
                        //tmp += v100.PRICD[i] + " "; // 수가코드. 반드시 대문자로
                        tmp += Environment.NewLine;
                    }
                    AddList("OP_INFO", "주요 처치.시술 및 수술내용", tmp);
                }
                else
                {
                    AddList("OP_INFO", "주요 처치.시술 및 수술내용", " "); // 필수기재(없으면 내용을 공란으로 표기) *** 심평원 ***
                }
                // 검사소견(검사날짜, 검사명, 검사결과기재)
                if (v100.GUM_COUNT > 0)
                {
                    tmp = "";
                    for (int i = 0; i < v100.GUM_COUNT; i++)
                    {
                        tmp += v100.GUM_GUMDT[i].Substring(0, 8) + " "; // 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                        tmp += v100.GUM_RSDT[i].Substring(0, 8) + " "; // 검사결과일시 CCYYMMDDHHMM
                        tmp += v100.GUM_ONM[i] + " "; // 검사명
                        tmp += ""; // 수가코드 *** 병원에서 제외시킴 ***
                        tmp += v100.GUM_GUMRESULT[i] + " "; //검사결과
                        tmp += Environment.NewLine;
                    }
                    AddList("LAB_INFO", "검사소견", tmp);
                }
                // 퇴원처방내역
                if (v100.DCORCOUNT > 0)
                {
                    tmp = "";
                    for (int i = 0; i < v100.DCORCOUNT; i++)
                    {
                        tmp += v100.ONM[i] + " "; // 약품명
                        tmp += ""; // 약품코드
                        tmp += v100.OUNIT[i] + " "; // 용법
                        tmp += v100.OQTY[i] + " "; // 1회투약량
                        tmp += v100.ORDCNT[i] + " "; // 1일투여횟수
                        tmp += v100.ODAYCNT[i] + " "; // 총 투약일수
                        tmp += Environment.NewLine;
                    }
                    AddList("DISCHARGE_PRESCRIBE", "퇴원처방내역", tmp);
                }
                // 퇴원 시 환자상태
                tmp = "";
                if ("1".Equals(v100.OUTSTATUS_CD))
                {
                    tmp = "완쾌";
                }
                else if ("2".Equals(v100.OUTSTATUS_CD))
                {
                    tmp = "호전";
                }
                else if ("3".Equals(v100.OUTSTATUS_CD))
                {
                    tmp = "호전안됨";
                }
                else if ("4".Equals(v100.OUTSTATUS_CD))
                {
                    tmp = "치료없이진단만";
                }
                else
                {
                    tmp = v100.OUTSTATUS_DESC;
                }
                AddList("OUT_STATUS", "퇴원 시 환자상태", tmp);
                // 퇴원 후 진료계획
                tmp = "";
                if ("0".Equals(v100.OUTCARE_CD))
                {
                    tmp = "없음";
                }
                else if ("1".Equals(v100.OUTCARE_CD))
                {
                    tmp = "외래진료예정 " + v100.OUTCARE_DT + " " + v100.OUTCARE_DPTCD;
                }
                else if ("2".Equals(v100.OUTCARE_CD))
                {
                    tmp = "재입원예정";
                }
                else
                {
                    tmp = "기타";
                }
                AddList("OUT_CARE", "퇴원 후 진료계획", tmp);
                // 작성일자시간
                AddList("WRT_DT_TM", "작성일자시간", v100.WDTTM.Substring(0, 8) + v100.WDTTM.Substring(8, 4));
                // 작성자
                AddList("WRT_NM", "작성자", v100.EMPNM);
                //
                SaveValues(p_REQ_DATA_NO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

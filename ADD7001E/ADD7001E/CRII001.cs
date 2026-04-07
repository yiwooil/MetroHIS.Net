using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CRII001 : CCUS001
    {
        // 입원기록(입원초기평가)

        public CRII001(string p_User)
        {
            m_User = p_User;
        }

        public void SetValues(CTE12C e12c, CEMR290 emr290, CTE12C_ADM e12c_adm, CEMR270 emr270, string p_REQ_DATA_NO)
        {
            try
            {
                m_List.Clear();
                if (emr290.READ_CNT > 0)
                {
                    // 주호소
                    AddList("CC", "주호소", emr290.CC);
                    // 현병력
                    AddList("PI", "현병력", emr290.PI);
                    // 주호소의 발생시기
                    AddList("CC_DATE", "주호소발생시기", emr290.CC_DATE);
                    // 과거력
                    AddList("PHX", "과거력", emr290.PHX);
                    // 가족력(입원기록에 있는 경우만 인정)
                    AddList("FHX", "가족력", emr290.FHX);
                    // 계통문진
                    AddList("ROS", "계통문진", emr290.ROS);
                    // 신체검진
                    AddList("PE", "신체검진", emr290.PE);
                    // 추정진단
                    AddList("IMP", "추정진단", emr290.IMP);
                    // 치료계획
                    AddList("TXPLAN", "치료계획", emr290.TXPLAN);
                    // 수정이력
                    AddList("MODIFY_HX", "수정이력", emr290.MODIFY_HX);
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", emr290.SYSDT + emr290.SYSTM.Substring(0, 4));
                    // 작성자
                    AddList("WRT_NM", "작성자", emr290.EMPNM);
                }
                else if (emr270.READ_CNT > 0)
                {
                    // 주호소
                    AddList("CC", "주호소", emr270.CC);
                    // 현병력
                    AddList("PI", "현병력", emr270.PI);
                    // 주호소의 발생시기
                    AddList("CC_DATE", "주호소발생시기", emr270.CC_DATE);
                    // 과거력
                    AddList("PHX", "과거력", emr270.PHX);
                    // 가족력(입원기록에 있는 경우만 인정)
                    AddList("FHX", "가족력", emr270.FHX);
                    // 계통문진
                    AddList("ROS", "계통문진", emr270.ROS);
                    // 신체검진
                    AddList("PE", "신체검진", emr270.PE);
                    // 추정진단
                    AddList("IMP", "추정진단", emr270.IMP);
                    // 치료계획
                    AddList("TXPLAN", "치료계획", emr270.TXPLAN);
                    // 수정이력
                    AddList("MODIFY_HX", "수정이력", emr270.MODIFY_HX);
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", emr270.SYSDT + emr270.SYSTM.Substring(0, 4));
                    // 작성자
                    AddList("WRT_NM", "작성자", emr270.EMPNM);
                }
                else if (e12c_adm.READ_CNT > 0)
                {
                    // 주호소
                    AddList("CC", "주호소", e12c_adm.CC);
                    // 현병력
                    AddList("PI", "현병력", e12c_adm.PI);
                    // 주호소의 발생시기
                    AddList("CC_DATE", "주호소발생시기", "");
                    // 과거력
                    AddList("PHX", "과거력", e12c_adm.PHX);
                    // 가족력(입원기록에 있는 경우만 인정)
                    AddList("FHX", "가족력", e12c_adm.FHX);
                    // 계통문진
                    AddList("ROS", "계통문진", e12c_adm.ROS);
                    // 신체검진
                    AddList("PE", "신처검진", e12c_adm.PE);
                    // 추정진단
                    AddList("IMP", "추정진단", e12c_adm.IMP);
                    // 치료계획
                    AddList("TXPLAN", "치료계획", e12c_adm.TXPLAN);
                    // 수정이력
                    AddList("MODIFY_HX", "수정이력", e12c_adm.MODIFY_HX);
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", e12c_adm.SYSDT + (e12c_adm.SYSTM.Length >= 4 ? e12c_adm.SYSTM.Substring(0, 4) : e12c_adm.SYSTM));
                    // 작성자
                    AddList("WRT_NM", "작성자", e12c_adm.USERNM);
                }
                else
                {
                    // 주호소
                    AddList("CC", "주호소", e12c.CC);
                    // 현병력
                    AddList("PI", "현병력", e12c.PI);
                    // 주호소의 발생시기
                    AddList("CC_DATE", "주호소발생시기", "");
                    // 과거력
                    AddList("PHX", "과거력", e12c.PHX);
                    // 가족력(입원기록에 있는 경우만 인정)
                    AddList("FHX", "가족력", e12c.FHX);
                    // 계통문진
                    AddList("ROS", "계통문진", e12c.ROS);
                    // 신체검진
                    AddList("PE", "신처검진", e12c.PE);
                    // 추정진단
                    AddList("IMP", "추정진단", e12c.IMP);
                    // 치료계획
                    AddList("TXPLAN", "치료계획", e12c.TXPLAN);
                    // 수정이력
                    AddList("MODIFY_HX", "수정이력", e12c.MODIFY_HX);
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", e12c.SYSDT + (e12c.SYSTM.Length >= 4 ? e12c.SYSTM.Substring(0, 4) : e12c.SYSTM));
                    // 작성자
                    AddList("WRT_NM", "작성자", e12c.USERNM);
                }
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

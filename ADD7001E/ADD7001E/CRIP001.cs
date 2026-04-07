using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7001E
{
    class CRIP001 : CCUS001
    {
        // 경과기록

        public CRIP001(string p_User)
        {
            m_User = p_User;
        }

        public void SetValues(CTV100 v100, CTE12C e12c, string p_REQ_DATA_NO)
        {
            try
            {
                m_List.Clear();
                string tmp = "";

                // 입원기간 동안의환자상태변화(치료과정 또는 결과 포함)
                foreach (CTX_ING tx_ing in e12c.TX_ING_LIST)
                {
                    if ("".Equals(tx_ing.PN) == false)
                    {
                        // 작성일자
                        AddList("WRT_DT_TM", "작성일자시간", tx_ing.SYSDT + (tx_ing.SYSTM.Length >= 4 ? tx_ing.SYSTM.Substring(0, 4) : tx_ing.SYSTM));
                        // 작성자
                        AddList("WRT_NM", "작성자", tx_ing.USERNM);
                        // 내용
                        AddList("PN", "입원기간 동안의 환자상태변화", tx_ing.EXDT + Environment.NewLine + Environment.NewLine + tx_ing.PN);
                    }
                }

                // 수술 후 기록
                if ("".Equals(e12c.OP_AFTER_RMK) == false)
                {
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", e12c.OP_AFTER_SYSDT + (e12c.OP_AFTER_SYSTM.Length >= 4 ? e12c.OP_AFTER_SYSTM.Substring(0, 4) : e12c.OP_AFTER_SYSTM));
                    // 작성자
                    AddList("WRT_NM", "작성자", e12c.OP_AFTER_USERNM);
                    // 내용
                    AddList("OP", "수술 후 기록", e12c.OP_AFTER_RMK);
                }

                // 사망기록(사망일자와 사망시간 포함)
                if ("".Equals(v100.DEATHDTTM)==false)
                {
                    tmp = v100.DEATHDTTM;
                    if ("".Equals(v100.DEDX_DISECD[0]) == false)
                    {
                        tmp += " " + v100.DEDX_DISECD[0];
                        if ("".Equals(v100.DEDX_DXD[0]) == false)
                        {
                            tmp += " " + v100.DEDX_DXD[0];
                        }
                    }
                }
                if ("".Equals(tmp) == false)
                {
                    // 작성일자
                    AddList("WRT_DT_TM", "작성일자시간", v100.WDTTM.Substring(0, 8) + (v100.WDTTM.Length >= 4 ? v100.WDTTM.Substring(8, 4) : v100.WDTTM));
                    // 작성자
                    AddList("WRT_NM", "작성자", v100.EMPNM);
                    // 내용
                    AddList("DEAD", "사망기록", tmp);
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

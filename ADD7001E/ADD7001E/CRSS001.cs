using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CRSS001 : CCUS001
    {
        // 수술기록지

        public CRSS001(string p_User)
        {
            m_User = p_User;
        }

        public void SetValues(CTU02 u02, CTU03 u03, string p_REQ_DATA_NO)
        {
            try
            {
                m_List.Clear();
                //string tmp = "";

                // 수술일
                AddList("OP_DT", "수술일자", u02.OPDT);
                // 수술과
                AddList("OP_DEPT", "수술과", u02.DPTNM);
                // 수술집도의
                AddList("OP_DOCT", "수술집도의", u02.OPDRNM);
                // 수술참여의사명
                AddList("OP_DOCT9", "수술참여의", "");
                // 마취방법
                AddList("ANES_METHOD", "마취방법", u03.ANETPNM);
                // 수술전진단
                AddList("OP_DX_BF", "수술전진단", "");
                // 수술후진단
                AddList("OP_DX_AF", "수술후진단", "");
                // 수술명
                AddList("OP_NAME", "수술명", u02.ONM);
                // 수술소견 또는 수술중 특이사항
                AddList("OP_REMARK", "수술소견", "");
                // 수술방법 및 절차
                AddList("OP_PROGRESS", "수술방법 및 절차", "");
                // 조직검사
                AddList("OP_LAB", "조직검사", "");
                // 배농/배액
                AddList("PUS", "배농/배액", "");
                // 패드확인 유무
                AddList("PAD_YN", "패드확인유무", "");
                // 작성일자
                AddList("WRT_DT", "작성일자", u02.SYSDT);
                // 작성시간
                AddList("WRT_TM", "작성시간", u02.SYSTM.Substring(0, 4));
                // 작성자
                AddList("WRT_NM", "작성자", u02.USRNM);
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

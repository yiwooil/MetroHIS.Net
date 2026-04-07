using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CODD001 : CCUS001
    {
        // 기타자료
        public CODD001(string p_User)
        {
            m_User = p_User;
        }

        public void SetValues(string p_REQ_DATA_NO)
        {
            m_List.Clear();

            AddList("-", "-", "-");
            SaveValues(p_REQ_DATA_NO);
        }
    }
}

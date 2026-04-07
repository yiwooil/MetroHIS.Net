using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataDept
    {
        public string JRKWA;
        public string DPTCD
        {
            get
            {
                if (JRKWA == "")
                {
                    return "";
                }
                else
                {
                    string[] jrkwa;
                    jrkwa = JRKWA.Split('$');
                    return jrkwa[2];
                }
            }
        }
        public string DPTNM
        {
            get
            {
                if (JRKWA == "")
                {
                    return "";
                }
                else
                {
                    string[] jrkwa;
                    jrkwa = JRKWA.Split('$');
                    return jrkwa[3];
                }
            }
        }

        public void Clear()
        {
            JRKWA = "";
        }
    }
}

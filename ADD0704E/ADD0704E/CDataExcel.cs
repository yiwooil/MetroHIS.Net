using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0704E
{
    class CDataExcel
    {
        public object[] COL_VLAUE = new object[26];

        public object COL_A { get { return COL_VLAUE[0]; } }
        public object COL_B { get { return COL_VLAUE[1]; } }
        public object COL_C { get { return COL_VLAUE[2]; } }
        public object COL_D { get { return COL_VLAUE[3]; } }
        public object COL_E { get { return COL_VLAUE[4]; } }
        public object COL_F { get { return COL_VLAUE[5]; } }
        public object COL_G { get { return COL_VLAUE[6]; } }
        public object COL_H { get { return COL_VLAUE[7]; } }
        public object COL_I { get { return COL_VLAUE[8]; } }
        public object COL_J { get { return COL_VLAUE[9]; } }
        public object COL_K { get { return COL_VLAUE[10]; } }
        public object COL_L { get { return COL_VLAUE[11]; } }
        public object COL_M { get { return COL_VLAUE[12]; } }
        public object COL_N { get { return COL_VLAUE[13]; } }
        public object COL_O { get { return COL_VLAUE[14]; } }
        public object COL_P { get { return COL_VLAUE[15]; } }
        public object COL_Q { get { return COL_VLAUE[16]; } }
        public object COL_R { get { return COL_VLAUE[17]; } }
        public object COL_S { get { return COL_VLAUE[18]; } }
        public object COL_T { get { return COL_VLAUE[19]; } }
        public object COL_U { get { return COL_VLAUE[20]; } }
        public object COL_V { get { return COL_VLAUE[21]; } }
        public object COL_W { get { return COL_VLAUE[22]; } }
        public object COL_X { get { return COL_VLAUE[23]; } }
        public object COL_Y { get { return COL_VLAUE[24]; } }
        public object COL_Z { get { return COL_VLAUE[25]; } }

        public string ERR_CD { get; set; }
        public string ERR_MSG { get; set; }

        public void Clear()
        {
            for (int i = 0; i < 26; i++)
            {
                COL_VLAUE[i] = null;
            }
            ERR_CD = "";
            ERR_MSG = "";
        }
    }
}

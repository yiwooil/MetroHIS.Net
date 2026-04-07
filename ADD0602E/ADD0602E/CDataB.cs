using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0602E
{
    class CDataB
    {
        public bool ROFG { get; set; }
        public string DACD { get; set; }
        public string DANM { get; set; }

        public void Clear()
        {
            ROFG = false;
            DACD = "";
            DANM = "";
        }
    }
}

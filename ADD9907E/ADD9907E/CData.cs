using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD9907E
{
    class CData
    {
        public string DACD { get; set; }
        public string DANM { get; set; }
        public string FRAGE { get; set; }
        public string TOAGE { get; set; }
        public bool READONLY;

        public void Clear()
        {
            DACD = "";
            DANM = "";
            FRAGE = "";
            TOAGE = "";
            READONLY = false;
        }
    }
}

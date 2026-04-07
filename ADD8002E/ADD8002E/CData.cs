using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002E
{
    class CData
    {
        public Boolean SEL { get; set; }
        public String REQDIV { get; set; }
        public String ITEMCD { get; set; }
        public String ITEMINFO;
        public String ITEMINFO1
        {
            get
            {
                return (ITEMINFO + "\\\\").Split('\\')[0];
            }
            set
            {
                ITEMINFO = value + "\\" + ITEMINFO2 + "\\" + ITEMINFO3;
            }
        }
        public String ITEMINFO2
        {
            get
            {
                return (ITEMINFO + "\\\\").Split('\\')[1];
            }
            set
            {
                ITEMINFO = ITEMINFO1 + "\\" + value + "\\" + ITEMINFO3;
            }
        }
        public String ITEMINFO3
        {
            get
            {
                return (ITEMINFO + "\\\\").Split('\\')[2];
            }
            set
            {
                ITEMINFO = ITEMINFO1 + "\\" + ITEMINFO2 + "\\" + value;
            }
        }
        public String STDSIZE { get; set; }
        public String UNIT { get; set; }
        public String BUSINESSCD { get; set; }
        public String TRADENM { get; set; }
        public String PRESNDDIV { get; set; }
        public String BUYDT { get; set; }
        public String BUYQTY { get; set; }
        public String BUYQTY_SAVE
        {
            get
            {
                int ret = 0;
                int.TryParse(BUYQTY, out ret);
                return ret.ToString();
            }
        }
        public String BUYAMT { get; set; }
        public String BUYAMT_SAVE
        {
            get
            {
                int ret = 0;
                int.TryParse(BUYAMT, out ret);
                return ret.ToString();
            }
        }
        public String UNITCOST { get; set; }
        public String UNITCOST_SAVE
        {
            get
            {
                int ret = 0;
                int.TryParse(UNITCOST, out ret);
                return ret.ToString();
            }
        }

        public void Clear()
        {
            SEL = false;
            REQDIV = "";
            ITEMCD = "";
            ITEMINFO = "";
            STDSIZE = "";
            UNIT = "";
            BUSINESSCD = "";
            TRADENM = "";
            PRESNDDIV = "";
            BUYDT = "";
            BUYQTY = "";
            BUYAMT = "";
            UNITCOST = "";
        }
    }
}

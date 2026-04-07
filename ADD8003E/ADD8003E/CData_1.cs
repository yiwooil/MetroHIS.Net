using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8003E
{
    class CData_1
    {
        public String GURESID { get; set; }
        public String GUNM { get; set; }
        public String CODEDIV { get; set; }
        public String CODEDIVNM
        {
            get
            {
                if (CODEDIV == "7I3") return "국산보험등재약";
                else if (CODEDIV == "7I6") return "수입약,원료약,요양기관자체조제,제제약";
                else return "";
            }
        }
        public String DRGCD { get; set; }
        public String DRGNM { get; set; }
        public String STDSZCD { get; set; }
        public String CTNUT { get; set; }
        public String BUYDT { get; set; }
        public String CTNUTAMT { get; set; }
        public String DRGQTY { get; set; }
        public String QTYCTNUT { get; set; }
        public String QTYAMT { get; set; }

        public String CTNUTAMT_SAVE
        {
            get
            {
                int ret = 0;
                int.TryParse(CTNUTAMT, out ret);
                return ret.ToString();
            }
        }
        public String DRGQTY_SAVE
        {
            get
            {
                Double ret = 0;
                Double.TryParse(DRGQTY, out ret);
                return ret.ToString();
            }
        }
        public String QTYAMT_SAVE
        {
            get
            {
                Double ret = 0;
                Double.TryParse(QTYAMT, out ret);
                return ret.ToString();
            } 
        }

        public void Clear()
        {
            GURESID= "";
            GUNM= "";
            CODEDIV= "";
            DRGCD= "";
            DRGNM= "";
            STDSZCD= "";
            CTNUT= "";
            BUYDT= "";
            CTNUTAMT= "";
            DRGQTY= "";
            QTYCTNUT= "";
            QTYAMT= "";
        }
    }
}

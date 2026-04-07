using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0704E
{
    class CData
    {
        public string BUYDT { get; set; }
        public string ITEMCD { get; set; }
        public string ITEMINFO { get; set; }
        public string STDSIZE { get; set; }
        public string UNIT { get; set; }
        public long BUYQTY { get; set; }
        public long BUYTOTAMT { get; set; }
        public long BUYAMT { get; set; }
        public string BUSSCD { get; set; }
        public string BUSSNM { get; set; }
        public string FSTBUYFG { get; set; }
        public string MEMO { get; set; }
        public string PRODCM { get; set; }
        public string EMPID { get; set; }
        public string ENTDT { get; set; }
        public string ENTTM { get; set; }
        public string UPDID { get; set; }
        public string UPDDT { get; set; }
        public string UPDTM { get; set; }
        public string KUMAK { get; set; }
        public string IPAMT { get; set; }
        public string PRICD { get; set; }

        // 수정되었는지 파악하기 위한 용도
        public string BF_BUYDT;
        public string BF_ITEMCD;
        public string BF_ITEMINFO;
        public string BF_STDSIZE;
        public string BF_UNIT;
        public long BF_BUYQTY;
        public long BF_BUYTOTAMT;
        public long BF_BUYAMT;
        public string BF_BUSSCD;
        public string BF_BUSSNM;
        public string BF_FSTBUYFG;
        public string BF_MEMO;
        public string BF_PRODCM;
        public string BF_KUMAK;

        public bool isNew
        {
            get
            {
                if (BF_ITEMCD == "") return true;
                else return false;
            }
        }

        public bool IsChange
        {
            get
            {
                if (BF_BUYDT != BUYDT) return true;
                if (BF_ITEMCD != ITEMCD) return true;
                if (BF_ITEMINFO != ITEMINFO) return true;
                if (BF_STDSIZE != STDSIZE) return true;
                if (BF_UNIT != UNIT) return true;
                if (BF_BUYQTY != BUYQTY) return true;
                if (BF_BUYTOTAMT != BUYTOTAMT) return true;
                if (BF_BUYAMT != BUYAMT) return true;
                if (BF_BUSSCD != BUSSCD) return true;
                if (BF_BUSSNM != BUSSNM) return true;
                if (BF_FSTBUYFG != FSTBUYFG) return true;
                if (BF_MEMO != MEMO) return true;
                if (BF_PRODCM != PRODCM) return true;
                if (BF_KUMAK != KUMAK) return true;

                return false;
            }
        }

        public void Clear()
        {
            BUYDT = "";
            ITEMCD = "";
            ITEMINFO = "";
            STDSIZE = "";
            UNIT = "";
            BUYQTY = 0;
            BUYTOTAMT = 0;
            BUYAMT = 0;
            BUSSCD = "";
            BUSSNM = "";
            FSTBUYFG = "";
            MEMO = "";
            PRODCM = "";
            EMPID = "";
            ENTDT = "";
            ENTTM = "";
            UPDID = "";
            UPDDT = "";
            UPDTM = "";
            KUMAK = "";
            IPAMT = "";
            PRICD = "";

            // 수정되었는지 파악하기 위한 용도
            BF_BUYDT = "";
            BF_ITEMCD = "";
            BF_ITEMINFO = "";
            BF_STDSIZE = "";
            BF_UNIT = "";
            BF_BUYQTY = 0;
            BF_BUYTOTAMT = 0;
            BF_BUYAMT = 0;
            BF_BUSSCD = "";
            BF_BUSSNM = "";
            BF_FSTBUYFG = "";
            BF_MEMO = "";
            BF_PRODCM = "";
            BF_KUMAK = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_ADF0601Q
{
    class CData
    {
        private System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
        private Dictionary<string, double> m_QTY = new Dictionary<string, double>();

        public string PID;
        public string BDEDT;
        public string ITEM;
        public string ITEMNM;
        public string ITEMNAME
        {
            get
            {
                return ITEM.PadLeft(2, '0') + "." + ITEMNM;
            }
        }
        public string PRICD { get; set; }
        public string EXMM { get { return FRDT.Substring(0, 6); } }
        public string CHRLT { get; set; }
        public string QFYCD { get; set; }
        public string PDIV;
        public string OKCD;
        public string PRKNM;
        public string ISPCD { get; set; }
        public string PDRID { get; set; }
        public string PDRNM { get; set; }
        public string EXDRID { get; set; }
        public string EXDRNM { get; set; }
        public string PRINM
        {
            get
            {
                string ret = PRKNM;
                if (OKCD == "Y") ret = "(퇴)" + ret;
                return ret;
            }
        }
        public long UTAMT { get; set; }
        public double TQTY
        {
            get
            {
                double ret = 0;
                foreach (KeyValuePair<string, double> items in m_QTY)
                {
                    ret += items.Value;
                }
                return ret;
            }

        }
        public long TTAMT { get; set; }
        private double GetQty(int index)
        {
            DateTime dtFrdt;
            bool bSuccess = DateTime.TryParseExact(FRDT, "yyyyMMdd", provider, System.Globalization.DateTimeStyles.None, out dtFrdt);
            dtFrdt = dtFrdt.AddDays(index);
            string exdt = dtFrdt.ToString("yyyyMMdd");
            return m_QTY.ContainsKey(exdt) ? m_QTY[exdt] : 0;
        }
        public double DAY1 { get { return GetQty(0); } }
        public double DAY2 { get { return GetQty(1); } }
        public double DAY3 { get { return GetQty(2); } }
        public double DAY4 { get { return GetQty(3); } }
        public double DAY5 { get { return GetQty(4); } }
        public double DAY6 { get { return GetQty(5); } }
        public double DAY7 { get { return GetQty(6); } }
        public double DAY8 { get { return GetQty(7); } }
        public double DAY9 { get { return GetQty(8); } }
        public double DAY10 { get { return GetQty(9); } }
        public double DAY11 { get { return GetQty(10); } }
        public double DAY12 { get { return GetQty(11); } }
        public double DAY13 { get { return GetQty(12); } }
        public double DAY14 { get { return GetQty(13); } }
        public double DAY15 { get { return GetQty(14); } }
        public double DAY16 { get { return GetQty(15); } }
        public double DAY17 { get { return GetQty(16); } }
        public double DAY18 { get { return GetQty(17); } }
        public double DAY19 { get { return GetQty(18); } }
        public double DAY20 { get { return GetQty(19); } }
        public double DAY21 { get { return GetQty(20); } }
        public double DAY22 { get { return GetQty(21); } }
        public double DAY23 { get { return GetQty(22); } }
        public double DAY24 { get { return GetQty(23); } }
        public double DAY25 { get { return GetQty(24); } }
        public double DAY26 { get { return GetQty(25); } }
        public double DAY27 { get { return GetQty(26); } }
        public double DAY28 { get { return GetQty(27); } }
        public double DAY29 { get { return GetQty(28); } }
        public double DAY30 { get { return GetQty(29); } }
        public double DAY31 { get { return GetQty(30); } }
        public double DAY32 { get { return GetQty(31); } }
        public double DAY33 { get { return GetQty(32); } }
        public double DAY34 { get { return GetQty(33); } }
        public double DAY35 { get { return GetQty(34); } }
        public double DAY36 { get { return GetQty(35); } }
        public double DAY37 { get { return GetQty(36); } }
        public double DAY38 { get { return GetQty(37); } }
        public double DAY39 { get { return GetQty(38); } }
        public double DAY40 { get { return GetQty(39); } }
        public double DAY41 { get { return GetQty(40); } }
        public double DAY42 { get { return GetQty(41); } }
        public double DAY43 { get { return GetQty(42); } }
        public double DAY44 { get { return GetQty(43); } }
        public double DAY45 { get { return GetQty(44); } }
        public double DAY46 { get { return GetQty(45); } }
        public double DAY47 { get { return GetQty(46); } }
        public double DAY48 { get { return GetQty(47); } }
        public double DAY49 { get { return GetQty(48); } }
        public double DAY50 { get { return GetQty(49); } }
        public double DAY51 { get { return GetQty(50); } }
        public double DAY52 { get { return GetQty(51); } }
        public double DAY53 { get { return GetQty(52); } }
        public double DAY54 { get { return GetQty(53); } }
        public double DAY55 { get { return GetQty(54); } }
        public double DAY56 { get { return GetQty(55); } }
        public double DAY57 { get { return GetQty(56); } }
        public double DAY58 { get { return GetQty(57); } }
        public double DAY59 { get { return GetQty(58); } }
        public double DAY60 { get { return GetQty(59); } }
        public double DAY61 { get { return GetQty(60); } }
        public double DAY62 { get { return GetQty(61); } }
        public double DAY63 { get { return GetQty(62); } }
        public double DAY64 { get { return GetQty(63); } }
        public double DAY65 { get { return GetQty(64); } }
        public double DAY66 { get { return GetQty(65); } }
        public double DAY67 { get { return GetQty(66); } }
        public double DAY68 { get { return GetQty(67); } }
        public double DAY69 { get { return GetQty(68); } }
        public double DAY70 { get { return GetQty(69); } }
        public double DAY71 { get { return GetQty(70); } }
        public double DAY72 { get { return GetQty(71); } }
        public double DAY73 { get { return GetQty(72); } }
        public double DAY74 { get { return GetQty(73); } }
        public double DAY75 { get { return GetQty(74); } }
        public double DAY76 { get { return GetQty(75); } }
        public double DAY77 { get { return GetQty(76); } }
        public double DAY78 { get { return GetQty(77); } }
        public double DAY79 { get { return GetQty(78); } }
        public double DAY80 { get { return GetQty(79); } }
        public double DAY81 { get { return GetQty(80); } }
        public double DAY82 { get { return GetQty(81); } }
        public double DAY83 { get { return GetQty(82); } }
        public double DAY84 { get { return GetQty(83); } }
        public double DAY85 { get { return GetQty(84); } }
        public double DAY86 { get { return GetQty(85); } }
        public double DAY87 { get { return GetQty(86); } }
        public double DAY88 { get { return GetQty(87); } }
        public double DAY89 { get { return GetQty(88); } }
        public double DAY90 { get { return GetQty(89); } }
        public double DAY91 { get { return GetQty(90); } }
        public double DAY92 { get { return GetQty(91); } }
        public double DAY93 { get { return GetQty(92); } }
        public double DAY94 { get { return GetQty(93); } }
        public double DAY95 { get { return GetQty(94); } }
        public double DAY96 { get { return GetQty(95); } }
        public double DAY97 { get { return GetQty(96); } }
        public double DAY98 { get { return GetQty(97); } }
        public double DAY99 { get { return GetQty(98); } }
        public double DAY100 { get { return GetQty(99); } }

        public void SetQty(string p_exdt, double p_qty)
        {
            if (m_QTY.ContainsKey(p_exdt) == false) m_QTY.Add(p_exdt,0);
            m_QTY[p_exdt] += p_qty;
        }

        public void AddQty(string p_exdt, double p_qty)
        {
            if (m_QTY.ContainsKey(p_exdt) == false) m_QTY.Add(p_exdt, 0);
            m_QTY[p_exdt] += p_qty;
        }

        public string FRDT;


        public void Clear()
        {
            m_QTY.Clear();
            PID = "";
            BDEDT = "";
            ITEM = "";
            PRICD = "";
            CHRLT = "";
            QFYCD = "";
            PDIV = "";
            OKCD = "";
            PRKNM = "";
            ITEMNM = "";
            ISPCD = "";
            PDRID = "";
            PDRNM = "";
            EXDRID = "";
            EXDRNM = "";
            UTAMT = 0;
            TTAMT = 0;
            FRDT = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRNH001
    {
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string DRID;
        public string DRNM;

        // TS06, TT05
        public List<string> DXD = new List<string>();

        //public string DLYS_KND_CD { get { return "1"; } } // 1.혈액투석 2.지속적대체요법

        // TU67
        public List<string> CHKDT = new List<string>(); // 시작일자(종료일자)
        public List<string> TRETMENT_STTIME = new List<string>(); // 시작시간
        public List<string> TRETMENT_EDTIME = new List<string>(); // 종료시간
        public string SDTM(int idx) { return CHKDT[idx] + TRETMENT_STTIME[idx]; }
        public string EDTM(int idx) { return CHKDT[idx] + TRETMENT_EDTIME[idx]; }
        public List<string> LastWt = new List<string>(); // 건체중
        public List<string> HMBeCurWt = new List<string>(); // 투석 전 체중
        public List<string> HMAfCurWt = new List<string>(); // 투석 후 체중
        public List<string> HMveWay = new List<string>(); // 혈관통로
        public string BLDVE_CH_CD(int idx)
        {
            string ret = "";
            if (HMveWay[idx].StartsWith("AVF") == true) ret = "1";
            else if (HMveWay[idx].StartsWith("동정맥류") == true) ret = "1";
            else if (HMveWay[idx].StartsWith("AVG") == true) ret = "2";
            else if (HMveWay[idx].StartsWith("Graft") == true) ret = "2";
            else if (HMveWay[idx].StartsWith("카테터") == true) ret = "3";
            else if (HMveWay[idx].StartsWith("도관") == true) ret = "3";
            return ret;
        }
        public List<string> UFTOTAL = new List<string>(); // 목표수분제거량
        public List<string> AntiBaseOqty = new List<string>(); // 항응고요법초기
        public List<string> MaintOqty = new List<string>(); // 항응고요법유지
        public List<string> HMMachine = new List<string>(); // 투석기
        public List<string> HMFluid = new List<string>(); // 투석액
        public List<string> EID = new List<string>(); // 작성자
        public List<string> ENM = new List<string>(); // 작성자 성명

        // TU67A
        public List<string> VCHKDT = new List<string>(); // 측정일자
        public List<string> VCHKTM = new List<string>(); // 측정시간
        public string VCHKDTM(int idx) { return VCHKDT[idx] + VCHKTM[idx]; }
        public List<string> VTM = new List<string>(); // 혈압
        public List<string> Vpressure = new List<string>(); // 맥박
        public List<string> Vpulsation = new List<string>(); // 혈류속도
        public List<string> Vvein = new List<string>(); // 동맥압
        public List<string> VSPEED = new List<string>(); // 정맥압

        //public string BLDVE_STNS_MNTR_YN { get { return "2"; } } // 혈과협착모니터링실시여부(1.Yes 2.No)

        // TU67B
        public List<string> N_CHKDT = new List<string>(); // 기록일자
        public List<string> N_CHKTM = new List<string>(); // 기록시간
        public string N_CHKDTM(int idx) { return N_CHKDT[idx] + N_CHKTM[idx]; }
        public List<string> N_Nursing = new List<string>(); // 간호기록
        public List<string> N_EID = new List<string>(); // 간호사
        public List<string> N_ENM = new List<string>(); // 간호사 성명

        public void Clear()
        {
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";

            // TS06, TT05
            DXD.Clear();

            // TU67
            CHKDT.Clear(); // 시작일자(종료일자)
            TRETMENT_STTIME.Clear(); // 시작시간
            TRETMENT_EDTIME.Clear(); // 종료시간
            LastWt.Clear(); // 건체중
            HMBeCurWt.Clear(); // 투석 전 체중
            HMAfCurWt.Clear(); // 투석 후 체중
            HMveWay.Clear(); // 혈관통로
            UFTOTAL.Clear(); // 목표수분제거량
            AntiBaseOqty.Clear(); // 항응고요법초기
            MaintOqty.Clear(); // 항응고요법유지
            HMMachine.Clear(); // 투석기
            HMFluid.Clear(); // 투석액
            EID.Clear(); // 작성자
            ENM.Clear(); // 작성자 성명

            // TU67A
            VCHKDT.Clear(); // 측정일자
            VCHKTM.Clear(); // 측정시간
            VTM.Clear(); // 혈압
            Vpressure.Clear(); // 맥박
            Vpulsation.Clear(); // 혈류속도
            Vvein.Clear(); // 동맥압
            VSPEED.Clear(); // 정맥압

            // TU67B
            N_CHKDT.Clear(); // 기록일자
            N_CHKTM.Clear(); // 기록시간
            N_Nursing.Clear(); // 간호기록
            N_EID.Clear(); // 간호사
            N_ENM.Clear(); // 간호사 성명
        }

    }
}

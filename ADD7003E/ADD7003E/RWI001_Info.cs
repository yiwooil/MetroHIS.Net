using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RWI001_Info
    {
        public int R_CNT;

        public string MIN_CRDHM; //

        // --------------------------------------

        public string FST_IPAT_DT // 최초입실일시
        {
            get { return MIN_CRDHM; }
        }
        public string ATFL_RPRT_ENFC_YN // 인공호흡기여부
        {
            get { return "2"; }
        }
        public string ATFL_RPRT_ENFC_YN_NM
        {
           get{return "No";}
        }
        public string OXY_CURE_YN // 산소흐흡기여부
        {
           get{return "2";}
        }
        public string OXY_CURE_YN_NM
        {
            get { return "No"; }
        }
        public string CNNL_ENFC_YN // 삽입관여부
        {
           get{return "2";}
        }
        public string CNNL_ENFC_YN_NM
        {
            get { return "No"; }
        }
        public string DRN_ENFC_YN // 배액관여부
        {
           get{return "2";}
        }
        public string DRN_ENFC_YN_NM
        {
            get { return "No"; }
        }
        public string SPCL_TRET_CD // 틋수처치
        {
            get { return "00"; }
        }
        public string SPCL_TRET_CD_NM
        {
           get{return "해당없음";}
        }
        public string MNTR_KND_CD // 모니터링 종류
        {
           get{return "00";}
        }
        public string MNTR_KND_CD_NM
        {
            get { return "해당없음"; }
        }
        public string SGRD_PNT_YN // 중증도여부
        {
           get { return "2"; }
        }
        public string SGRD_PNT_YN_NM
        {
            get { return "No"; }
        }

        // ---

        public void Clear()
        {
            R_CNT = 0;

            MIN_CRDHM = "";
        }
    }
}

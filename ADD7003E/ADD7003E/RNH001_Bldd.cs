using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNH001_Bldd
    {
        public string HMSDT; // 투석시작일
        public string HMFDT; // 투석종료일
        public string LastWt; // 건체중
        public string HMBeCurWt; // 투석적 체중
        public string HMAfCurWt; // 투석후 체중
        public string HMveWay; // 혈관통로
        public string AntiBaseOqty; // 항응고요법 초기 용량
        public string MaintOqty; // 항응고요법 유지 용량
        public string HMMachine; // 투석기(기계명)
        public string HMFluid; // 투석액
        public string UPDID; // 작성자
        public string UPDNM;

        // ---

        public string BLDD_STA_DT // 시작일시
        {
            get { return HMSDT + "0000"; }
        }
        public string BLDD_END_DT // 종료일시
        {
            get { return HMFDT + "0000"; }
        }
        public string DLYS_BWGT // 건체중
        {
            get { return LastWt; }
        }
        public string BF_BGWT // 투석전 체중
        {
            get { return HMBeCurWt; }
        }
        public string AF_BGWT // 투석후 체중
        {
            get { return HMAfCurWt; }
        }
        public string BLDVE_CH_CD // 혈관통로
        {
            get { return HMveWay; }
        }
        public string CATH_TXT // 카테터 내용
        {
            get { return ""; }
        }
        public string GL_WTL_DEL_QTY // 목표 수분 제거량
        {
            get { return ""; }
        }
        public string ATCG_ERYY_TXT // 항응고요법 초기
        {
            get { return AntiBaseOqty; }
        }
        public string ATCG_MNTN_TXT // 항응고요법 유지
        {
            get { return MaintOqty; }
        }
        public string DLYS_DV_TXT // 투석기
        {
            get { return HMMachine; }
        }
        public string DLYS_LQD_TXT // 투석액
        {
            get { return HMFluid; }
        }
        public string WRTP_NM // 작성자
        {
            get { return UPDNM; }
        }

    }
}

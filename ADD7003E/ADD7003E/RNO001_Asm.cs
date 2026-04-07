using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNO001_Asm
    {
        public int TYPE; // 0.낙상 1.통증 2.욕창
        public string WDATE; // 평가일자
        public string WTIME; // 평가시간
        public string TOOL; // 도구
        // 낙상(AUTH005)
        public string JUM; // 점수
        // 통증(AUTH004)
        public string PART; // 부위
        public string POW; // 강도
        public string CONDITION; // 양상
        public string TERM; // 기간
        public string FREQUENCY; // 빈도
        public string REMARK; // 평가구분
        public string DRUG; // 약물중재
        public string NODRUG; // 비약물중재
        // 욕청(TV40)
        public string BodySt_1; // 4점
        public string BodySt_2; // 3점
        public string BodySt_3; // 2점
        public string BodySt_4; // 1점
        public string MindSt_1; // 4점
        public string MindSt_2; // 3점
        public string MindSt_3; // 2점
        public string MindSt_4; // 1점
        public string ActiveSt_1; // 4점
        public string ActiveSt_2; // 3점
        public string ActiveSt_3; // 2점
        public string ActiveSt_4; // 1점
        public string MoveSt_1; // 4점
        public string MoveSt_2; // 3점
        public string MoveSt_3; // 2점
        public string MoveSt_4; // 1점
        public string InCtncSt_1; // 4점
        public string InCtncSt_2; // 3점
        public string InCtncSt_3; // 2점
        public string InCtncSt_4; // 1점

        // ---

        public string EXEC_DT // 평가일시
        {
            get { return WDATE + WTIME; }
        }
        public string ASM_TL_NM // 도구
        {
            get { return TOOL; }
        }
        public string ASM_RST_TXT // 결과
        {
            get
            {
                if (TYPE == 0)
                {
                    return JUM;
                }
                if (TYPE == 1)
                {
                    return "부위:" + PART + ", 강도:" + POW + ", 양상:" + CONDITION + ", 기간:" + TERM + ", 빈도:" + FREQUENCY + ", 평구구분:" + REMARK + ", 약물중재:" + DRUG + ", 비약물중재:" + NODRUG; ;
                }
                if (TYPE == 2)
                {
                    int body_st = 0;
                    int mind_st = 0;
                    int active_st = 0;
                    int move_st = 0;
                    int in_st = 0;

                    if (BodySt_1 == "1") body_st = 4;
                    else if (BodySt_2 == "1") body_st = 3;
                    else if (BodySt_3 == "1") body_st = 2;
                    else if (BodySt_4 == "1") body_st = 1;

                    if (MindSt_1 == "1") mind_st = 4;
                    else if (MindSt_2 == "1") mind_st = 3;
                    else if (MindSt_3 == "1") mind_st = 2;
                    else if (MindSt_4 == "1") mind_st = 1;

                    if (ActiveSt_1 == "1") active_st = 4;
                    else if (ActiveSt_2 == "1") active_st = 3;
                    else if (ActiveSt_3 == "1") active_st = 2;
                    else if (ActiveSt_4 == "1") active_st = 1;

                    if (MoveSt_1 == "1") move_st = 4;
                    else if (MoveSt_2 == "1") move_st = 3;
                    else if (MoveSt_3 == "1") move_st = 2;
                    else if (MoveSt_4 == "1") move_st = 1;

                    if (InCtncSt_1 == "1") in_st = 4;
                    else if (InCtncSt_2 == "1") in_st = 3;
                    else if (InCtncSt_3 == "1") in_st = 2;
                    else if (InCtncSt_4 == "1") in_st = 1;

                    return (body_st + mind_st + active_st + move_st + in_st).ToString();
                }
                return "";
            }
        }
    }
}

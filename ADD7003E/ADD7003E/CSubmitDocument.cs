using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7003E
{
    class CSubmitDocument
    {
        public static bool CheckDocument(HIRA.EformEntry.Model.Document p_doc)
        {
            bool ret = false;

            HIRA.EformEntry.MultiJsonConvertResponse multiJsonConvertResponse = p_doc.ToJson(); // 기재점검및 json생성

            if (multiJsonConvertResponse.Result) // 전체점검결과
            {
                for (int i = 0; i < multiJsonConvertResponse.Count; i++)
                {
                    //MessageBox.Show(multiJsonConvertResponse[i].Json); //서식지별 Json 결과 값 리턴
                }
                ret = true;
            }
            else
            {
                if (multiJsonConvertResponse.Count < 1)
                {
                    //점검할 데이터가 없을 경우 ( addDoc() 함수 호출 안했을 경우)
                    MessageBox.Show(multiJsonConvertResponse.ErrorMessage);
                }
                else //전체 점검 후, 적어도 한 개라도 점검 실패 시
                {
                    for (int i = 0; i < multiJsonConvertResponse.Count; i++)
                    {
                        MessageBox.Show(multiJsonConvertResponse[i].ErrorMessage); //서식지별 오류메세지리턴
                    }
                }
                ret = false; // 오류 발생 중단.
            }
            return ret;
        }

        public static bool SubmitDocument(HIRA.EformEntry.Model.Document p_doc)
        {
            bool ret = false;

            HIRA.EformEntry.ResponseModel.MultiMasterResponse masters = p_doc.createDoc(); // 제출서비스 실행

            if (masters.Result)
            {
                //성공결과
                MessageBox.Show("서식제출 성공");
                ret = true;
            }
            else
            {
                if (masters.Count < 1)
                {
                    //점검할 데이터가 없을 경우 ( addDoc() 함수 호출 안했을 경우)
                    MessageBox.Show(masters.ErrorMessage);
                }
                else
                {
                    //전체 점검 후, 적어도 한 개라도 점검 실패 시
                    for (int i = 0; i < masters.Count; i++)
                    {
                        MessageBox.Show("서식제출 실패 " + masters[i].ErrorMessage); //서식지별 오류메세지리턴
                    }
                }
                ret = false;
            }

            return ret;
        }
    }
}

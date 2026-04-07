using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace ADD_EDI_SANJE
{
    public partial class ADD_EDI_SANJE_03 : Form
    {
        public ADD_EDI_SANJE_03()
        {
            InitializeComponent();
        }

        private void btnQueryPayment_Click(object sender, EventArgs e)
        {
            try
            {
                grdTongjiseo2.DataSource = null;
                grdTongjiseo3.DataSource = null;

                var data = new
                {
                    jeopsuNo = txtQryJeopsuNo.Text.ToString()
                };

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/payment/tongiView";

                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                txtJeopsuNo.Text = obj["jeopsuNo"].ToString();

                txtVersion.Text = obj["version"].ToString(); // 버전

                txtJeopsuNo.Text = obj["jeopsuNo"].ToString(); // 접수번호
                txtJeopsuNoSaengseongFg.Text = obj["jeopsuNoSaengseongFg"].ToString(); // 접수번호생성구분
                txtSeosikNo.Text = obj["seosikNo"].ToString(); // 서식번호
                txtHospitalNo.Text = obj["hospitalNo"].ToString(); // 청구기관코드
                txtDaepyojaNm.Text = obj["daepyojaNm"].ToString(); // 대표자성명
                txtSaeopFg.Text = obj["saeopFg"].ToString(); // 사업구분
                txtBalsongNo.Text = obj["balsongNo"].ToString(); // 발송번호
                txtCheongguNo.Text = obj["cheongguNo"].ToString(); // 청구번호
                txtJeopsuDt.Text = obj["jeopsuDt"].ToString(); // 접수일자
                txtTongjiDt.Text = obj["tongjiDt"].ToString(); // 통지일자
                txtGaesanJigeupDt.Text = obj["gaesanJigeupDt"].ToString(); // 개산지급일자

                txtSodeukse.Text = obj["sodeukse"].ToString(); // 소득세
                txtJuminse.Text = obj["juminse"].ToString(); // 주민세
                txtGongjePrcSum.Text = obj["gongjePrcSum"].ToString(); // 공제총액

                txtAccntNo.Text = obj["accntNo"].ToString(); // 계좌번호
                txtGeoraeBankCd.Text = obj["georaeBankCd"].ToString(); // 거래은행코드

                txtCheongguCnt.Text = obj["cheongguCnt"].ToString(); // 청구건수
                txtCheongguPrc.Text = obj["cheongguPrc"].ToString(); // 청구금액

                txtJigeupCnt.Text = obj["jigeupCnt"].ToString(); // 지급건수
                txtJigeupPrc.Text = obj["jigeupPrc"].ToString(); // 지급금액

                txtSakgamCnt.Text = obj["sakgamCnt"].ToString(); // 조정건수
                txtSakgamPrc.Text = obj["sakgamPrc"].ToString(); // 조정금액

                txtBulneungCnt.Text = obj["bulneungCnt"].ToString(); // 불능건수
                txtBulneungPrc.Text = obj["bulneungPrc"].ToString(); // 불능금액

                txtBoryuCnt.Text = obj["boryuCnt"].ToString(); // 보류건수
                txtBoryuPrc.Text = obj["boryuPrc"].ToString(); // 보류금액

                txtChangeCnt.Text = obj["changeCnt"].ToString(); // 급여종류변경건수
                txtChangePrc.Text = obj["changePrc"].ToString(); // 급여종류변경금액

                txtJigeupGjPrc.Text = obj["jigeupGjPrc"].ToString(); // 지급결정액
                txtSilJigeupPrc.Text = obj["silJigeupPrc"].ToString(); // 실지급액
                txtGaesangeupJigeupPrc.Text = obj["gaesangeupJigeupPrc"].ToString(); // 개산지급액
                txtJungsanChaPrc.Text = obj["jungsanChaPrc"].ToString(); // 정산차액

                txtBoryuJeopsuNo.Text = obj["boryuJeopsuNo"].ToString(); // 보류 가청구 접수번호
                txtChangeJeopsuNo.Text = obj["changeJeopsuNo"].ToString(); // 급여종류변경 가청구 접수번호

                txtBunhalNaeyeok.Text = obj["bunhalNaeyeok"].ToString(); // 분할내역
                txtTongjiSahang.Text = obj["tongjiSahang"].ToString(); // 통지사항

                txtReportMessage.Text = obj["reportMessage"].ToString(); // 리포트메시지
                txtResponseCd.Text = obj["responseCd"].ToString(); // 응답코드
                txtResponseMsg.Text = obj["responseMsg"].ToString(); // 응답메시지

                var tongjiseo2_list = obj["tongjiseo2_list"];
                List<Ctongjiseo2> tongjiseo2List = new List<Ctongjiseo2>();
                foreach (var item in tongjiseo2_list)
                {
                    Ctongjiseo2 tongjiseo2 = new Ctongjiseo2();
                    tongjiseo2.jeopsuNo = item["jeopsuNo"].ToString(); // 접수번호
                    tongjiseo2.jeopsuNoSaengseongFg = item["jeopsuNoSaengseongFg"].ToString(); // 접수번호생성구분
                    tongjiseo2.mssSer = item["mssSer"].ToString(); // 명일련
                    tongjiseo2.jaehaejaNm = item["jaehaejaNm"].ToString(); // 환자명
                    tongjiseo2.jinryoFg = item["jinryoFg"].ToString(); // 진료구분
                    tongjiseo2.jaehaeDt = item["jaehaeDt"].ToString(); // 재해발생일자
                    tongjiseo2.jinryoGigan = item["jinryoGigan"].ToString(); // 진료기간
                    tongjiseo2.cheongguPrc = item["cheongguPrc"].ToString(); // 청구액
                    tongjiseo2.jojeongPrc = item["jojeongPrc"].ToString(); // 조정액
                    tongjiseo2.jigeupPrc = item["jigeupPrc"].ToString(); // 지급액
                    tongjiseo2.bulneungPrc = item["bulneungPrc"].ToString(); // 불능액
                    tongjiseo2.boryuPrc = item["boryuPrc"].ToString(); // 보류액
                    tongjiseo2.changePrc = item["changePrc"].ToString(); // 급여종류변경액
                    tongjiseo2.silJinryoIlsu = item["silJinryoIlsu"].ToString(); // 신진료일수
                    tongjiseo2.bulneungSayu = item["bulneungSayu"].ToString(); // 불능사유
                    tongjiseo2.tongjiSahang = item["tongjiSahang"].ToString(); // 통지사항
                    tongjiseo2List.Add(tongjiseo2);
                }

                var tongjiseo3_list = obj["tongjiseo3_list"];
                List<Ctongjiseo3> tongjiseo3List = new List<Ctongjiseo3>();
                foreach (var item in tongjiseo3_list)
                {
                    Ctongjiseo3 tongjiseo3 = new Ctongjiseo3();
                    tongjiseo3.jeopsuNo = item["jeopsuNo"].ToString(); // 접수번호
                    tongjiseo3.jeopsuNoSaengseongFg = item["jeopsuNoSaengseongFg"].ToString(); // 접수번호생성구분
                    tongjiseo3.mssSer = item["mssSer"].ToString(); // 명일련
                    tongjiseo3.julNo = item["julNo"].ToString(); // 줄번호
                    tongjiseo3.hangmokNo = item["hangmokNo"].ToString(); // 항목번호
                    tongjiseo3.cdFg = item["cdFg"].ToString(); // 코드구분
                    tongjiseo3.cd = item["cd"].ToString(); // 코드
                    tongjiseo3.cdNm = item["cdNm"].ToString(); // 코드명
                    tongjiseo3.jeongjeongCd = item["jeongjeongCd"].ToString(); // 정정코드
                    tongjiseo3.jeongjeongCdNm = item["jeongjeongCdNm"].ToString(); // 정정코드명
                    tongjiseo3.danga = item["danga"].ToString(); // 단가
                    tongjiseo3.jeongjeongDanga = item["jeongjeongDanga"].ToString(); // 정정단가
                    tongjiseo3.qty = item["qty"].ToString(); // 수량
                    tongjiseo3.injeongQty = item["injeongQty"].ToString(); // 인정수량
                    tongjiseo3.ilsu = item["ilsu"].ToString(); // 일수
                    tongjiseo3.injeongIlsu = item["injeongIlsu"].ToString(); // 인정일수
                    tongjiseo3.totInjeongQty = item["totInjeongQty"].ToString(); // 총인정수량
                    tongjiseo3.prc = item["prc"].ToString(); // 금액
                    tongjiseo3.jojeongPrc = item["jojeongPrc"].ToString(); // 조정금액
                    tongjiseo3.jojeongBulneungCd = item["jojeongBulneungCd"].ToString(); // 조정불능코드
                    tongjiseo3.jojeongSayu = item["jojeongSayu"].ToString(); // 조정사유                    tongjiseo3List.Add(tongjiseo3);
                }

                grdTongjiseo2.DataSource = tongjiseo2List;
                grdTongjiseo3.DataSource = tongjiseo3List;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

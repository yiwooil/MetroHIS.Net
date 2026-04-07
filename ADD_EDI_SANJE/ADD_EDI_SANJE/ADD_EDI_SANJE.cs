using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace ADD_EDI_SANJE
{
    public partial class ADD_EDI_SANJE : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_HospMulti;

        private bool IsFirst;
        private string m_pgm_step = ""; // 어느 단계에서 오류가 발생하는지 확인하기 위한 용도

        public ADD_EDI_SANJE()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_HospMulti = "";

            this.CreatePopupMenu();
        }

        public ADD_EDI_SANJE(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_HospMulti = GetHospmulti();
        }

        private string GetHospmulti()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["MULTIFG"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void CreatePopupMenu()
        {
            //
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("전송 제외", new EventHandler(mnuRemoveData_Click));
            //cm.MenuItems.Add("전송 제외 취소", new EventHandler(mnuCancelRemoveData_Click));
            //grdMain.ContextMenu = cm;
        }

        private void btnSelFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string fileName = ofd.SafeFileName;
                string filePath = Path.GetDirectoryName(ofd.FileName);

                txtM0101.Text = filePath + Path.DirectorySeparatorChar + "M010.1";
                txtM0102.Text = filePath + Path.DirectorySeparatorChar + "M010.2";
                txtM0201.Text = filePath + Path.DirectorySeparatorChar + (rbYang.Checked ? "M020.1" : "M021.1");
                txtM0202.Text = filePath + Path.DirectorySeparatorChar + (rbYang.Checked ? "M020.2" : "M021.2");
                txtM0203.Text = filePath + Path.DirectorySeparatorChar + (rbYang.Checked ? "M020.3" : "M021.3");
                txtM0204.Text = filePath + Path.DirectorySeparatorChar + (rbYang.Checked ? "M020.4" : "M021.4");
                txtM0205.Text = (rbYang.Checked ? filePath + Path.DirectorySeparatorChar + "M020.5" : "");
            }
        }

        private void btnYoyangCheckJinryobi_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new CYoyangCheckJinryobi();
                data.jinryoYymm = txtYYMM.Text.ToString();
                data.yanghanFg = (rbYang.Checked ? "1" : "2");
                data.m0101FileNm = "M010.1";
                data.m0101File = CUtil.ReadFileToBytes(txtM0101.Text.ToString());
                data.m0102FileNm = "M010.2";
                data.m0102File = CUtil.ReadFileToBytes(txtM0102.Text.ToString());
                data.m0201FileNm = (rbYang.Checked ? "M020.1" : "M021.1");
                data.m0201File = CUtil.ReadFileToBytes(txtM0201.Text.ToString());
                data.m0202FileNm = (rbYang.Checked ? "M020.2" : "M021.2");
                data.m0202File = CUtil.ReadFileToBytes(txtM0202.Text.ToString());
                data.m0203FileNm = (rbYang.Checked ? "M020.3" : "M021.3");
                data.m0203File = CUtil.ReadFileToBytes(txtM0203.Text.ToString());
                data.m0204FileNm = (rbYang.Checked ? "M020.4" : "M021.4");
                data.m0204File = CUtil.ReadFileToBytes(txtM0204.Text.ToString());
                if (rbYang.Checked)
                {
                    // 양방만
                    data.m0205FileNm = "M020.5";
                    data.m0205File = CUtil.ReadFileToBytes(txtM0205.Text.ToString());
                }

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/check/jinryobi";

                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                string responseCd = obj["responseCd"].ToString();
                string responseMsg = obj["responseMsg"].ToString();
                string jeonsongNo = obj["jeonsongNo"].ToString();

                txtResponseCd.Text = responseCd;
                txtResponseMsg.Text = responseMsg;
                txtJeonsongNo.Text = jeonsongNo;
        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnQueryResult_Click(object sender, EventArgs e)
        {
            try
            {
                grdResult.DataSource = null;

                var data = new
                {
                    cheongguDtStr = txtCheongguDtStr.Text.ToString(),
                    cheongguDtEnd = txtCheongguDtEnd.Text.ToString(),
                    jeonsongNo = ""
                };

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/check/result";
                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                string responseCd = obj["responseCd"].ToString();
                string responseMsg = obj["responseMsg"].ToString();
                
                txtResponseCd.Text = responseCd;
                txtResponseMsg.Text = responseMsg;

                List<Cresult> resultList = new List<Cresult>();

                var list = obj["result_list"];
                foreach (var item in list)
                {
                    Cresult rslt = new Cresult();
                    rslt.minwonFg = item["minwonFg"].ToString(); // 민원구분(1.진료비 2.본인부담치료비 3.비급여치료비)
                    rslt.jeonsongNo = item["jeonsongNo"].ToString(); // 전송번호
                    rslt.cheongguCnt = item["cheongguCnt"].ToString(); // 청구건수
                    rslt.normalCnt = item["normalCnt"].ToString(); // 정상명세서건수
                    rslt.bulneungMss = item["bulneungMss"].ToString(); // 확인대상명세서건수
                    rslt.stateFgNm = item["stateFgNm"].ToString(); // 상태명
                    resultList.Add(rslt);
                }

                grdResult.DataSource = resultList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                string strJeonsongNo = "";
                string strMinwonFg = "";

                var data = new
                {
                    jeonsongNo = strJeonsongNo,
                    minwonFg = strMinwonFg
                };

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/receipt/regist";
                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                string responseCd = obj["responseCd"].ToString();
                string responseMsg = obj["responseMsg"].ToString();
                string jeopsuNo = obj["jeopsuNo"].ToString();

                txtResponseCd.Text = responseCd;
                txtResponseMsg.Text = responseMsg;
                txtJeopsuNo.Text = jeopsuNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryTongjiseo_Click(object sender, EventArgs e)
        {
            try
            {
                grdTongjiseo.DataSource = null;

                var data = new
                {
                    jeopsuFromDt = txtJeopsuFromDt.Text.ToString(),
                    jeopsuToDt = txtJeopsuToDt.Text.ToString(),
                    tongjiseoFg = ""
                };

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/search/tongjiList";
                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                string responseCd = obj["responseCd"].ToString();
                string responseMsg = obj["responseMsg"].ToString();

                txtResponseCd.Text = responseCd;
                txtResponseMsg.Text = responseMsg;

                List<Ctongjiseo> tongjiseoList = new List<Ctongjiseo>();

                var list = obj["tongjiseo_list"];
                foreach (var item in list)
                {
                    Ctongjiseo tongjiseo = new Ctongjiseo();
                    tongjiseo.jeopsuNo = item["jeopsuNo"].ToString(); // 접수번호
                    tongjiseo.jinryoYymm = item["jinryoYymm"].ToString(); // 진료년월
                    tongjiseo.saeopFg = item["saeopFg"].ToString(); // 사업구분
                    tongjiseo.cheongguFg = item["cheongguFg"].ToString(); // 청구구분(0.원청구 1.보완청구 2.추가청구)
                    tongjiseo.ndbhCheoriStatus = item["ndbhCheoriStatus"].ToString(); // 처리상태
                    tongjiseo.tongboseoFg = item["tongboseoFg"].ToString(); // 통보서명(A.접수증 B.반송증 C.지급결정통지서 D.개산지급결정통지서)
                    tongjiseo.tongboseoStatus = item["tongboseoStatus"].ToString(); // 통지서상태
                    tongjiseo.tongboDt = item["tongboDt"].ToString(); // 통보일자

                    tongjiseoList.Add(tongjiseo);
                }

                grdTongjiseo.DataSource = tongjiseoList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

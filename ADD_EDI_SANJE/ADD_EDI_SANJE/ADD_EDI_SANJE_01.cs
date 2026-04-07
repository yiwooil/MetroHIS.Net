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
    public partial class ADD_EDI_SANJE_01 : Form
    {
        private ADD_EDI_SANJE_01()
        {
            InitializeComponent();
        }

        public ADD_EDI_SANJE_01(string jeopsuNo)
            : this()
        {
            txtQryJeopsuNo.Text = jeopsuNo;
        }

        private void btnQueryResult_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new 
                {
                    jeopsuNo = txtQryJeopsuNo.Text.ToString()
                };

                string strJson = JsonConvert.SerializeObject(data);

                string path = "/yoyang/search/jeopsujeung";

                string result = CUtil.CallAPI(path, strJson);

                var obj = Newtonsoft.Json.Linq.JObject.Parse(result);

                txtJeopsuNo.Text = obj["jeopsuNo"].ToString();
                txtCheongguFg.Text = obj["cheongguFg"].ToString();
                txtHospitalNo.Text = obj["hospitalNo"].ToString();
                txtHospitalNm.Text = obj["hospitalNm"].ToString();
                txtJeopsuDt.Text = obj["jeopsuDt"].ToString();
                txtCheongguCnt.Text = obj["cheongguCnt"].ToString();
                txtCheongguPrc.Text = obj["cheongguPrc"].ToString();
                txtMinwonDocCd.Text = obj["minwonDocCd"].ToString();
                txtMinwonDocNm.Text = obj["minwonDocNm"].ToString();
                txtGyeolaeStatusCd.Text = obj["gyeolaeStatusCd"].ToString();
                txtJeopsuStatusCd.Text = obj["jeopsuStatusCd"].ToString();
                txtReportMessage.Text = obj["reportMessage"].ToString();
                txtResponseCd.Text = obj["responseCd"].ToString();
                txtResponseMsg.Text = obj["responseMsg"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

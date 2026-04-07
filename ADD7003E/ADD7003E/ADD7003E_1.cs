using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7003E
{
    public partial class ADD7003E_1 : Form
    {
        private ADD7003E m_ADD7003E;

        private bool m_IsTest; // 테스트중인지?

        private string m_hosid;
        private string m_dmd_no; // 청구번호
        private string m_sp_sno; // 명일련
        private string m_rcv_no; // 접수번호
        private string m_rcv_yr; // 접수년도
        private string m_bill_sno; // 청일련
        private string m_insup_tp_cd; // 보험자구분
        private string m_req_data_no; // 요청번호

        private ERD001 m_ERD001; // 진단검사결과지
        private ERR001 m_ERR001; // 영상검사결과지
        private RSS001 m_RSS001; // 수술기록자료
        private REE001 m_REE001; // 응급기록자료
        private RII001 m_RII001; // 입원초진기록자료
        private RIP001 m_RIP001; // 입원경과기록자료
        private RWI001 m_RWI001; // 중환자실기록자료
        private RCC001 m_RCC001; // 협의진료기록자료
        private RAA001 m_RAA001; // 협의진료기록자료
        private RAR001 m_RAR001; // 협의진료기록자료
        private RNP001 m_RNP001; // 간호정보조사자료
        private RNO001 m_RNO001; // 기타간호기록자료
        private RNS001 m_RNS001; // 수술간호기록자료
        private RNE001 m_RNE001; // 응급간호기록자료
        private RDD001 m_RDD001; // 의사지시기록자료
        private RNH001 m_RNH001; // 투석기록자료
        private RIY001 m_RIY001; // 전입기록자료
        private RIZ001 m_RIZ001; // 전출기록자료
        private RMM001 m_RMM001; // 투약기록지

        private bool IsFirst;

        public ADD7003E_1()
        {
            InitializeComponent();
        }

        private void ADD7003E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD7003E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
        }

        public void ShowData(ADD7003E p_ADD7003E,  bool p_IsTest)
        {
            m_ADD7003E = p_ADD7003E;
            m_IsTest = p_IsTest;

            SetDocList();
            SetFormList();
            LoadPtnt(0);
            SetPatientInfo();
        }

        private bool LoadPtnt(int p_dir)
        {
            string p_hosid = "";
            string p_dmd_no = ""; // 청구번호
            string p_sp_sno = ""; // 명일련
            string p_rcv_no = ""; // 접수번호
            string p_rcv_yr = ""; // 접수년도
            string p_bill_sno = ""; // 청일련
            string p_insup_tp_cd = ""; // 보험자구분
            string p_req_data_no = ""; // 요청번호

            if (m_ADD7003E.GetPtnt(p_dir, ref p_hosid, ref p_dmd_no, ref p_sp_sno, ref p_rcv_no, ref p_rcv_yr, ref p_bill_sno, ref p_insup_tp_cd, ref p_req_data_no) == false)
            {
                return false;
            }
            else
            {
                m_hosid = p_hosid;
                m_dmd_no = p_dmd_no; // 청구번호
                m_sp_sno = p_sp_sno; // 명일련
                m_rcv_no = p_rcv_no; // 접수번호
                m_rcv_yr = p_rcv_yr; // 접수년도
                m_bill_sno = p_bill_sno; // 청일련
                m_insup_tp_cd = p_insup_tp_cd; // 보험자구분
                m_req_data_no = p_req_data_no; // 요청번호
                return true;
            }
        }

        private void SetPatientInfo()
        {
            try
            {
                txtDemno.Text = m_dmd_no;
                txtEprtno.Text = m_sp_sno;

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT I2A.PID,I2A.PNM,I2A.BDEDT,I2A.STEDT,I2A.EXAMC,A01.RESID";
                sql += Environment.NewLine + "  FROM TI2A I2A INNER JOIN TA01 A01 ON A01.PID=I2A.PID";
                sql += Environment.NewLine + " WHERE I2A.DEMNO='" + m_dmd_no + "'";
                sql += Environment.NewLine + "   AND I2A.EPRTNO=" + m_sp_sno + "";

                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPid.Text = reader["PID"].ToString();
                                txtPnm.Text = reader["PNM"].ToString();
                                txtStedt.Text = reader["STEDT"].ToString();
                                txtBededt.Text = reader["BDEDT"].ToString();
                                txtResid.Text = reader["RESID"].ToString();
                                int examc = 0;
                                int.TryParse(reader["EXAMC"].ToString(), out examc);
                                DateTime dt = DateTime.ParseExact(txtStedt.Text.ToString(), "yyyyMMdd",null);
                                examc--;
                                if (examc < 0) examc = 0;
                                txtEnddt.Text = (dt.AddDays(examc)).ToString("yyyyMMdd");
                            }
                            reader.Close();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDocList()
        {
            string[] dic_arr = { "RID001","퇴원요약지x"
                               , "ERD001","진단검사결과지"
                               , "ERR001","영상검사결과지"
                               , "ERP001","병리검사결과지x"
                               , "RSS001","수술기록자료"
                               , "REE001","응급기록자료"
                               , "RII001","입원초진기록자료"
                               , "RIP001","입원경과기록자료"
                               , "ROO001","외래초진기록자료x"
                               , "ROP001","외래경과기록자료x"
                               , "RWI001","중환자실기록자료"
                               , "RCC001","협의진료기록자료"
                               , "RAA001","마취기록자료"
                               , "RAR001","회복기록자료"
                               , "RNP001","간호정보조사자료"
                               , "RNO001","기타간호기록자료"
                               , "RNS001","수술간호기록자료"
                               , "RNE001","응급간호기록자료"
                               , "RDD001","의사지시기록자료"
                               , "RWW001","임상관찰기록자료x"
                               , "RNH001","투석기록자료"
                               , "RIY001","전입기록자료"
                               , "RIZ001","전축기록자료"
                               , "RTT001","시술기록자료x"
                               , "RMM001","투약기록자료"
                               , "RWN001","신생아중환자실기록자료x"
                               , "RCC001","의원급진료기록자료x"
                               , "RPR001","방사선치료기록자료x"
                               , "ORM001","입원진료비계산서.영수증x"
                               , "OCQ001","의료의진향상을위한점검표x"
                               , "RTI001","난임시술기록지x"
                               , "RPE001","교육상담일지x"
                               , "RPH001","이학요법실시기록지x"
                               , "RPS001","정신요법실시기록지x"
                               , "ONP001","처방소견서x"
                               , "ODF001","진료비세부산정내역x"
                               };

            List<CDoc> list = new List<CDoc>();
            grdDoc.DataSource = null;
            grdDoc.DataSource = list;

            for (int i = 0; i < dic_arr.Length; i += 2)
            {
                CDoc data = new CDoc();
                data.DOC_CD = dic_arr[i];
                data.DOC_NM = dic_arr[i + 1];
                list.Add(data);
            }

            RefreshGridDoc();
        }

        private void SetFormList()
        {
            // 진단검사결과지
            m_ERD001 = new ERD001();
            m_ERD001.FormBorderStyle = FormBorderStyle.None;
            m_ERD001.TopLevel = false;
            m_ERD001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_ERD001);
            // 영상검사결과지
            m_ERR001 = new ERR001();
            m_ERR001.FormBorderStyle = FormBorderStyle.None;
            m_ERR001.TopLevel = false;
            m_ERR001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_ERR001);
            // 수술기록자료
            m_RSS001 = new RSS001();
            m_RSS001.FormBorderStyle = FormBorderStyle.None;
            m_RSS001.TopLevel = false;
            m_RSS001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RSS001);
            // 응급기록자료
            m_REE001 = new REE001();
            m_REE001.FormBorderStyle = FormBorderStyle.None;
            m_REE001.TopLevel = false;
            m_REE001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_REE001);
            // 입원초진기록자료
            m_RII001 = new RII001();
            m_RII001.FormBorderStyle = FormBorderStyle.None;
            m_RII001.TopLevel = false;
            m_RII001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RII001);
            // 입원경과기록자료
            m_RIP001 = new RIP001();
            m_RIP001.FormBorderStyle = FormBorderStyle.None;
            m_RIP001.TopLevel = false;
            m_RIP001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RIP001);
            // 중환자실기록자료
            m_RWI001 = new RWI001();
            m_RWI001.FormBorderStyle = FormBorderStyle.None;
            m_RWI001.TopLevel = false;
            m_RWI001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RWI001);
            // 협의진료기록자료
            m_RCC001 = new RCC001();
            m_RCC001.FormBorderStyle = FormBorderStyle.None;
            m_RCC001.TopLevel = false;
            m_RCC001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RCC001);
            // 마취기록자료
            m_RAA001 = new RAA001();
            m_RAA001.FormBorderStyle = FormBorderStyle.None;
            m_RAA001.TopLevel = false;
            m_RAA001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RAA001);
            // 회복기록자료
            m_RAR001 = new RAR001();
            m_RAR001.FormBorderStyle = FormBorderStyle.None;
            m_RAR001.TopLevel = false;
            m_RAR001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RAR001);
            // 간호정보조사자료
            m_RNP001 = new RNP001();
            m_RNP001.FormBorderStyle = FormBorderStyle.None;
            m_RNP001.TopLevel = false;
            m_RNP001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RNP001);
            // 기타간호기록자료
            m_RNO001 = new RNO001();
            m_RNO001.FormBorderStyle = FormBorderStyle.None;
            m_RNO001.TopLevel = false;
            m_RNO001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RNO001);
            // 수술간호기록자료
            m_RNS001 = new RNS001();
            m_RNS001.FormBorderStyle = FormBorderStyle.None;
            m_RNS001.TopLevel = false;
            m_RNS001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RNS001);
            // 응급간호기록자료
            m_RNE001 = new RNE001();
            m_RNE001.FormBorderStyle = FormBorderStyle.None;
            m_RNE001.TopLevel = false;
            m_RNE001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RNE001);
            // 의사지시기록자료
            m_RDD001 = new RDD001();
            m_RDD001.FormBorderStyle = FormBorderStyle.None;
            m_RDD001.TopLevel = false;
            m_RDD001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RDD001);
            // 투약기록자료
            m_RNH001 = new RNH001();
            m_RNH001.FormBorderStyle = FormBorderStyle.None;
            m_RNH001.TopLevel = false;
            m_RNH001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RNH001);
            // 전입기록자료
            m_RIY001 = new RIY001();
            m_RIY001.FormBorderStyle = FormBorderStyle.None;
            m_RIY001.TopLevel = false;
            m_RIY001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RIY001);
            // 전출기록자료
            m_RIZ001 = new RIZ001();
            m_RIZ001.FormBorderStyle = FormBorderStyle.None;
            m_RIZ001.TopLevel = false;
            m_RIZ001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RIZ001);
            // 투약기록지
            m_RMM001 = new RMM001();
            m_RMM001.FormBorderStyle = FormBorderStyle.None;
            m_RMM001.TopLevel = false;
            m_RMM001.Dock = DockStyle.Fill;
            panForm.Controls.Add(m_RMM001);
        }

        private void ClearForm()
        {
            // 진단검사결과지
            m_ERD001.Hide();
            // 영상검사결과지
            m_ERR001.Hide();
            // 수술기록자료
            m_RSS001.Hide();
            // 응급기록자료
            m_REE001.Hide();
            // 입원초진기록자료
            m_RII001.Hide();
            // 입원경과기록자료
            m_RIP001.Hide();
            // 중환자실기록자료
            m_RWI001.Hide();
            // 협의진료기록자료
            m_RCC001.Hide();
            // 마취기록자료
            m_RAA001.Hide();
            // 회복기록자료
            m_RAR001.Hide();
            // 간호정보조사자료
            m_RNP001.Hide();
            // 기타간호기록자료
            m_RNO001.Hide();
            // 수술간호기록자료
            m_RNS001.Hide();
            // 응급간호기록자료
            m_RNE001.Hide();
            // 의사지시기록자료
            m_RDD001.Hide();
            // 투약기록자료
            m_RNH001.Hide();
            // 전입기록자료
            m_RIY001.Hide();
            // 전출기록자료
            m_RIZ001.Hide();
            // 투약기록지
            m_RMM001.Hide();
        }

        private void RefreshGridDoc()
        {
            if (grdDoc.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDoc.BeginInvoke(new Action(() => grdDocView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDocView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdDoc_DoubleClick(object sender, EventArgs e)
        {
            callForm();
        }

        private void grdDoc_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            callForm();
        }

        public void callForm()
        {
            string pid = txtPid.Text.ToString();
            string bededt = txtBededt.Text.ToString();
            string stedt = txtStedt.Text.ToString();
            string enddt = txtEnddt.Text.ToString();
            string pnm = txtPnm.Text.ToString();
            string resid = txtResid.Text.ToString();
            string docCode = grdDocView.GetRowCellValue(grdDocView.FocusedRowHandle, gcDOC_CD).ToString();
            if (docCode == "ERD001")
            {
                // 진단검사결과지
                m_ERD001.Show();
                m_ERD001.BringToFront();
                m_ERD001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "ERR001")
            {
                // 영상검사결과지
                m_ERR001.Show();
                m_ERR001.BringToFront();
                m_ERR001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RSS001")
            {
                // 수술기록자료
                m_RSS001.Show();
                m_RSS001.BringToFront();
                m_RSS001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "REE001")
            {
                // 응급기록자료
                m_REE001.Show();
                m_REE001.BringToFront();
                m_REE001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RII001")
            {
                // 입원초진기록자료
                m_RII001.Show();
                m_RII001.BringToFront();
                m_RII001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RIP001")
            {
                // 입원경과기록자료
                m_RIP001.Show();
                m_RIP001.BringToFront();
                m_RIP001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RWI001")
            {
                // 중환자실기록자료
                m_RWI001.Show();
                m_RWI001.BringToFront();
                m_RWI001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RCC001")
            {
                // 협의진료기록자료
                m_RCC001.Show();
                m_RCC001.BringToFront();
                m_RCC001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RAA001")
            {
                // 마취기록자료
                m_RAA001.Show();
                m_RAA001.BringToFront();
                m_RAA001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RAR001")
            {
                // 회복기록자료
                m_RAR001.Show();
                m_RAR001.BringToFront();
                m_RAR001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RNP001")
            {
                // 간호정보조사자료
                m_RNP001.Show();
                m_RNP001.BringToFront();
                m_RNP001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RNO001")
            {
                // 기타간호기록자료
                m_RNO001.Show();
                m_RNO001.BringToFront();
                m_RNO001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RNS001")
            {
                // 수술간호기록자료
                m_RNS001.Show();
                m_RNS001.BringToFront();
                m_RNS001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RNE001")
            {
                // 응급간호기록자료
                m_RNE001.Show();
                m_RNE001.BringToFront();
                m_RNE001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RDD001")
            {
                // 의사지시기록자료
                m_RDD001.Show();
                m_RDD001.BringToFront();
                m_RDD001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RNH001")
            {
                // 투석기록자료
                m_RNH001.Show();
                m_RNH001.BringToFront();
                m_RNH001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RIY001")
            {
                // 전입기록자료
                m_RIY001.Show();
                m_RIY001.BringToFront();
                m_RIY001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RIZ001")
            {
                // 전출기록자료
                m_RIZ001.Show();
                m_RIZ001.BringToFront();
                m_RIZ001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
            else if (docCode == "RMM001")
            {
                // 투약기록지
                m_RMM001.Show();
                m_RMM001.BringToFront();
                m_RMM001.DoQuery(m_IsTest, m_hosid, m_dmd_no, m_rcv_no, m_rcv_yr, m_bill_sno, m_sp_sno, m_insup_tp_cd, m_req_data_no, pid, bededt, stedt, enddt, pnm, resid);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (LoadPtnt(-1) == false)
            {
                MessageBox.Show("이전 자료가 없습니다.");
            }
            else
            {
                ClearForm();
                SetPatientInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (LoadPtnt(1) == false)
            {
                MessageBox.Show("다음 자료가 없습니다.");
            }
            else
            {
                ClearForm();
                SetPatientInfo();
            }
        }

    }
}

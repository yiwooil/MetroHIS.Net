using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using HIRA.EformEntry;
using HIRA.EformEntry.Model;
using HIRA.EformEntry.ResponseModel;

namespace ADD7000E
{
    public partial class ADD7000E : Form
    {

        private bool m_First;
        private CHosInfo m_HosInfo;
        private String m_Demno;
        private String m_User;
        private String m_Pwd;


        private ADD7000E_1 m_ADD7000E_1 = new ADD7000E_1();
        //private ADD7000E_ERR m_ADD7000E_ERR = new ADD7000E_ERR();

        public ADD7000E()
        {
            InitializeComponent();
            m_Demno = "";
            m_User = "";
            m_Pwd = "";

            this.CreatePopupMenu();
        }

        private void CreatePopupMenu()
        {
            MenuItem[] mi = new MenuItem[2];
            mi[0] = new MenuItem("임시전송", MenuItem_Click);
            mi[1] = new MenuItem("전송", MenuItem_Click);
            ContextMenu cm = new ContextMenu(mi);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
        }

        public ADD7000E(String demno,String user,String pwd):this()
        {
            m_Demno = demno;
            m_User = user;
            m_Pwd = pwd;
        }

        private void ADD7000E_Load(object sender, EventArgs e)
        {
            m_First = true;
        }

        private void ADD7000E_Activated(object sender, EventArgs e)
        {
            try
            {
                if (m_First == false) return;
                m_First = false;
                m_HosInfo = new CHosInfo();
                m_HosInfo.SetInfo();
                txtDEMNO.Text = m_Demno;
                if ("".Equals(m_Demno) == false)
                {
                    this.Query();
                    this.MakeAndCheck();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //if (m_ADD7000E_ERR == null) m_ADD7000E_ERR = new ADD7000E_ERR();
            //if (m_ADD7000E_ERR.Visible == true) m_ADD7000E_ERR.Visible = false;
            this.Query();
        }

        private void Query()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                List<CDataTI2A> list = new List<CDataTI2A>();
                list.Clear();

                bool bReadCnecno = false;

                String strCnecno = "";
                String strDcount = "";
                String strCnectdd = "";

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT *, CONVERT(VARCHAR,GETDATE(),112) AS HDATE ";
                sql += System.Environment.NewLine + "  FROM TI2A A";
                sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + txtDEMNO.Text.ToString() + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(A.DONFG,'')='Y'";
                sql += System.Environment.NewLine + " ORDER BY A.EPRTNO";
                    
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        if (bReadCnecno == false)
                        {
                            bReadCnecno = true;
                            strCnecno = ReadCnecno(reader["DEMNO"].ToString(), reader["QFYCD"].ToString(), reader["ADDZ1"].ToString(), reader["ADDZ2"].ToString(), reader["ADDZ4"].ToString(), conn, ref strDcount, ref strCnectdd);
                        }

                        
                        CDataTI2A i2a = new CDataTI2A();

                        // 화면에 보이게
                        i2a.DEMNO = reader["DEMNO"].ToString();
                        i2a.EPRTNO = reader["EPRTNO"].ToString();
                        i2a.PID = reader["PID"].ToString();
                        i2a.PNM = reader["PNM"].ToString();
                        i2a.JRKWA = reader["JRKWA"].ToString();
                        i2a.QFYCD = reader["QFYCD"].ToString();
                        i2a.GONSGB = reader["GONSGB"].ToString();
                        i2a.ADDZ1 = reader["ADDZ1"].ToString();
                        i2a.CNECNO = strCnecno; // 접수번호
                        i2a.RCV_YR = "".Equals(strCnectdd) ? "": strCnectdd.Substring(0, 4);// 접수년도
                        i2a.DCOUNT = strDcount; // 청구서일련번호
                        i2a.CHECK_ORM001 = ""; // 영수정
                        i2a.CHECK_RID001 = ""; // 퇴원요약지
                        i2a.CHECK_OCQ001 = ""; // 의료의질향상을위한점검표
                        // 내부처리용
                        i2a.BDODT = reader["BDODT"].ToString();
                        i2a.JRBY = reader["JRBY"].ToString();
                        i2a.UNISQ = reader["UNISQ"].ToString();
                        i2a.SIMCS = reader["SIMCS"].ToString();
                        i2a.BDEDT = reader["BDEDT"].ToString();
                        i2a.RESID = reader["RESID"].ToString();
                        i2a.TT41KEY = reader["TT41KEY"].ToString();
                        i2a.HDATE = reader["HDATE"].ToString(); // 2023.01.06 WOOIL - 2023년1월16일부터 서식이 변경됨. 날짜로 구분하기 위한 용도.

                        i2a.json_OCQ001 = "";
                        i2a.json_ORM001 = "";
                        i2a.json_RID001 = "";

                        i2a.error_OCQ001 = "";
                        i2a.error_ORM001 = "";
                        i2a.error_RID001 = "";

                        list.Add(i2a);

                    }
                    reader.Close();
                }
                grdMain.DataSource = list;

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "오류");
            }
        }

        private String ReadCnecno(String demno, String qfycd, String addz1, String addz2, String addz4, OleDbConnection conn, ref String dcount, ref String cnectdd)
        {
            // addz1 : 1 = 보완청구
            // addz2 : 원청구 접수번호
            // addz4 : 원청구 명일련
            String strCnecno = "";
            dcount = "";
            cnectdd = "";


            // 2019.01.06 WOOIL - 접수일자를 읽어야 하므로 접수증을 읽어야 한다.
            //                    심결에는 접수일자가 없음.

            //// 심결을 읽는다.
            //{
            //    String sql = "";
            //    if (qfycd.StartsWith("3"))
            //    {
            //        sql = "";
            //        sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
            //        sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
            //        sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + demno + "'";
            //        sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
            //    }
            //    else
            //    {
            //        sql = "";
            //        sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
            //        sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
            //        sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + demno + "'";
            //        sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
            //    }
            //    // TSQL문장과 Connection 객체를 지정   
            //    OleDbCommand cmd = new OleDbCommand(sql, conn);
            //
            //    // 데이타는 서버에서 가져오도록 실행
            //    OleDbDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        if ("".Equals(strCnecno)) strCnecno = reader["CNECNO"].ToString();
            //    }
            //    reader.Close();
            //}
            //
            //// 접수번호를 못 구했으면 접수증을 읽어본다.
            //if ("".Equals(strCnecno))
            //{
            //    string sql = "";
            //    sql = "";
            //    sql += System.Environment.NewLine + "SELECT * ";
            //    sql += System.Environment.NewLine + "  FROM TIE_F0102 A";
            //    sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + demno + "'";
            //    sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";
            //
            //    // TSQL문장과 Connection 객체를 지정   
            //    OleDbCommand cmd = new OleDbCommand(sql, conn);
            //
            //    // 데이타는 서버에서 가져오도록 실행
            //    OleDbDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        strCnecno = reader["CNECTNO"].ToString();
            //        cnectdd = reader["CNECTDD"].ToString();
            //        if ("".Equals(strCnecno) == false) break;
            //    }
            //    reader.Close();
            //}

            // 접수증을 읽는다.
            {
                String sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0102 A";
                sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + demno + "'";
                sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    strCnecno = reader["CNECTNO"].ToString();
                    cnectdd = reader["CNECTDD"].ToString();
                    if ("".Equals(strCnecno) == false) break;
                }
                reader.Close();
            }

            // 보완청구이면
            if ("1".Equals(addz1))
            {
                String sql = "";
                if (qfycd.StartsWith("3"))
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ("".Equals(dcount)) dcount = reader["DCOUNT"].ToString();
                }
                reader.Close();

                // 2022.10.14 WOOIL - 원청구는 건강보험인데 보완청구는 의료급여로 하는 경우가 있다고 함.(유비스)
                if ("".Equals(dcount))
                {
                    if (qfycd.StartsWith("3"))
                    {
                        sql = "";
                        sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                        sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                        sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                        sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                    }
                    else
                    {
                        sql = "";
                        sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                        sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
                        sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                        sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                    }
                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd_2 = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader_2 = cmd_2.ExecuteReader();
                    while (reader_2.Read())
                    {
                        if ("".Equals(dcount)) dcount = reader_2["DCOUNT"].ToString();
                    }
                    reader_2.Close();
                }
            }


            return strCnecno;
        }

        private void btnMakeAndCheck_Click(object sender, EventArgs e)
        {
            //if (m_ADD7000E_ERR == null) m_ADD7000E_ERR = new ADD7000E_ERR();
            //if (m_ADD7000E_ERR.Visible == true) m_ADD7000E_ERR.Visible = false;
            this.MakeAndCheck();
        }

        private void MakeAndCheck()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CORM001 oORM001 = new CORM001();
                CRID001 oRID001 = new CRID001();
                COCQ001 oOCQ001 = new COCQ001();

                List<CDataTI2A> list = (List<CDataTI2A>)grdMain.DataSource;
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    CDataTI2A i2a = list[row];
                    MakeORM001(i2a, oORM001); // 영수증
                    MakeRID001(i2a, oRID001); // 퇴원요약지
                    MakeOCQ001(i2a, oOCQ001); // 의료의 질 ...
                }
                Cursor.Current = Cursors.Default;

                // 오류가 있으면 표시
                String errMsg = "";
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    CDataTI2A i2a = list[row];
                    if ("점검완료".Equals(i2a.CHECK_ORM001) == false)
                    {
                        errMsg += GetFormattedErrorString(i2a.error_ORM001) + System.Environment.NewLine;
                    }
                    if ("점검완료".Equals(i2a.CHECK_RID001) == false)
                    {
                        errMsg += GetFormattedErrorString(i2a.error_RID001) + System.Environment.NewLine;
                    }
                    if ("점검완료".Equals(i2a.CHECK_OCQ001) == false)
                    {
                        errMsg += GetFormattedErrorString(i2a.error_OCQ001) + System.Environment.NewLine;
                    }
                }
                if ("".Equals(errMsg) == false)
                {
                    MessageBox.Show("점검오류가 있습니다.");
                    //if (m_ADD7000E_ERR == null) m_ADD7000E_ERR = new ADD7000E_ERR();
                    //m_ADD7000E_ERR.SetMsg(errMsg);
                    //m_ADD7000E_ERR.Left = this.Left;
                    //m_ADD7000E_ERR.Top = this.Top;
                    //m_ADD7000E_ERR.Width = this.Width;
                    //m_ADD7000E_ERR.Height = this.Height;
                    //m_ADD7000E_ERR.Show(this);
                }

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message,"오류");
            }
        }

        private void RefreshGridMain()
        {
            if (grdMain.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdMain.BeginInvoke(new Action(() => grdMainView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdMainView.RefreshData();
                Application.DoEvents();
            }
        }

        private void MakeRID001(CDataTI2A i2a, CRID001 p_RID001)
        {
            // RID001 : 퇴원요약지
            i2a.CHECK_RID001 = "생성중";
            RefreshGridMain();

            // 문서생성
            HIRA.EformEntry.Model.Document doc = p_RID001.GetDocument(i2a, m_HosInfo);
            i2a.doc_RID001 = doc;

            // Validation
            i2a.CHECK_RID001 = "점검중";
            RefreshGridMain();

            MultiJsonConvertResponse jsonResponse = doc.ToJson();
            if (!jsonResponse.Result)
            {
                i2a.error_RID001 = jsonResponse[0].ErrorMessage;
                i2a.json_RID001 = "";
                i2a.CHECK_RID001 = "점검오류";
                RefreshGridMain();
            }
            else
            {
                i2a.error_RID001 = "";
                i2a.CHECK_RID001 = "점검완료";
                i2a.json_RID001 = jsonResponse[0].Json;
                RefreshGridMain();
            }
        }

        private void MakeORM001(CDataTI2A i2a, CORM001 p_ORM001)
        {
            // ORM001 : 영수증
            i2a.CHECK_ORM001 = "생성중";
            RefreshGridMain();

            // 문서생성
            HIRA.EformEntry.Model.Document doc = p_ORM001.GetDocument(i2a, m_HosInfo);
            //HIRA.EformEntry.Model.Document doc = p_ORM001.GetDocumentSample();
            i2a.doc_ORM001 = doc;

            // Validation
            i2a.CHECK_ORM001 = "점검중";
            RefreshGridMain();

            MultiJsonConvertResponse jsonResponse = doc.ToJson();
            if (!jsonResponse.Result)
            {
                i2a.error_ORM001 = jsonResponse[0].ErrorMessage;
                i2a.json_ORM001 = "";
                i2a.CHECK_ORM001 = "점검오류";
                RefreshGridMain();
            }
            else
            {
                i2a.error_ORM001 = "";
                i2a.json_ORM001 = jsonResponse[0].Json;
                i2a.CHECK_ORM001 = "점검완료";
                RefreshGridMain();
            }
        }

        private void MakeOCQ001(CDataTI2A i2a, COCQ001 p_OCQ001)
        {
            // OCQ001 : 의료의질향상을위한점검표
            i2a.CHECK_OCQ001 = "생성중";
            RefreshGridMain();

            // 문서생성
            HIRA.EformEntry.Model.Document doc = p_OCQ001.GetDocument(i2a, m_HosInfo);
            i2a.doc_OCQ001 = doc;

            // Validation
            i2a.CHECK_OCQ001 = "점검중";
            RefreshGridMain();

            MultiJsonConvertResponse jsonResponse = doc.ToJson();
            if (!jsonResponse.Result)
            {
                i2a.error_OCQ001 = jsonResponse[0].ErrorMessage;
                i2a.json_OCQ001 = "";
                i2a.CHECK_OCQ001 = "점검오류";
                RefreshGridMain();
            }
            else
            {
                i2a.error_OCQ001 = "";
                i2a.CHECK_OCQ001 = "점검완료";
                i2a.json_OCQ001 = jsonResponse[0].Json;
                RefreshGridMain();
            }
        }

        private String GetFormattedErrorString(String errmsg)
        {
            if ("".Equals(errmsg)) return "";
            StringBuilder sb = new StringBuilder();
            String[] aryErrmsg = errmsg.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            int maxLen = 0;
            foreach (String errOne in aryErrmsg)
            {
                String[] errs = (errOne + "::").Split(':');
                int len = Encoding.Default.GetByteCount(errs[1]);
                if (len > maxLen) maxLen = len;
            }
            foreach (String errOne in aryErrmsg)
            {
                String[] errs = (errOne + "::").Split(':');
                int padLen = maxLen - Encoding.Default.GetBytes(errs[1]).Length;
                sb.AppendFormat("{0} : {1} : {2}" + System.Environment.NewLine, errs[0], errs[1] + "".PadLeft(padLen), errs[2]);
            }
            String strRet = "";
            strRet = sb.ToString();
            return strRet;
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            txtMsg.Text = "";

            List<CDataTI2A> list = (List<CDataTI2A>)grdMain.DataSource;
            CDataTI2A i2a = list[e.RowHandle];
            
            if(e.Column==gcCHECK_OCQ001)
            {
                txtMsg.Text = GetFormattedErrorString(i2a.error_OCQ001);
            }
            else if(e.Column==gcCHECK_ORM001)
            {
                txtMsg.Text = GetFormattedErrorString(i2a.error_ORM001);
            }
            else if (e.Column == gcCHECK_RID001)
            {
                txtMsg.Text = GetFormattedErrorString(i2a.error_RID001);
            }
        }

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            txtMsg.Text = "";

            List<CDataTI2A> list = (List<CDataTI2A>)grdMain.DataSource;
            CDataTI2A i2a = list[grdMainView.FocusedRowHandle];

            if (grdMainView.FocusedColumn == gcCHECK_OCQ001)
            {
                // 의료의질향상을위한점검표
                m_ADD7000E_1.Show();
                m_ADD7000E_1.DataLoad(i2a.doc_OCQ001);
            }
            else if (grdMainView.FocusedColumn == gcCHECK_ORM001)
            {
                // 영수증
                m_ADD7000E_1.Show();
                m_ADD7000E_1.DataLoad(i2a.doc_ORM001);
            }
            else if (grdMainView.FocusedColumn == gcCHECK_RID001)
            {
                // 퇴원요약지
                m_ADD7000E_1.Show();
                m_ADD7000E_1.DataLoad(i2a.doc_RID001);
            }
        }

        private void btnSendTmp_Click(object sender, EventArgs e)
        {
            this.Send(true);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            this.Send(false);
        }

        private void Send(bool bTmp)
        {
            try
            {
                bool bCheckError = false;
                Cursor.Current = Cursors.WaitCursor;

                List<CDataTI2A> list = (List<CDataTI2A>)grdMain.DataSource;

                // 점검완료여부 점검
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    CDataTI2A i2a = list[row];
                    if ("점검완료".Equals(i2a.CHECK_ORM001) == false) bCheckError = true;
                    if ("점검완료".Equals(i2a.CHECK_RID001) == false) bCheckError = true;
                    if ("점검완료".Equals(i2a.CHECK_OCQ001) == false) bCheckError = true;
                }

                if (bCheckError == true)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("점검완료되지 않은 내역이 있습니다. 전송을 취소합니다.","오류");
                    return;
                }

                // 전송작업
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    CDataTI2A i2a = list[row];

                    SendORM001(i2a, bTmp);
                    SendRID001(i2a, bTmp); // 퇴원요약지
                    SendOCQ001(i2a, bTmp);
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "오류");
            }

        }

        private void SendORM001(CDataTI2A i2a, bool bTmp)
        {
            // 전송
            if ("점검완료".Equals(i2a.CHECK_ORM001) == false) return;
            i2a.CHECK_ORM001 = "전송중";
            RefreshGridMain();

            HIRA.EformEntry.Model.Document doc = i2a.doc_ORM001;
            MultiMasterResponse masters = bTmp ? doc.createTmpDoc() : doc.createDoc();

            if(masters.Result)
            {
                i2a.CHECK_ORM001 = "전송성공";
                i2a.error_ORM001 = "";
                RefreshGridMain();
            }
            else
            {
                i2a.CHECK_ORM001 = "전송실패";
                i2a.error_ORM001 = masters[0].ErrorMessage;
                RefreshGridMain();
            }
        }

        private void SendRID001(CDataTI2A i2a, bool bTmp)
        {
            // 전송
            if ("점검완료".Equals(i2a.CHECK_RID001) == false) return;
            i2a.CHECK_RID001 = "전송중";
            RefreshGridMain();

            HIRA.EformEntry.Model.Document doc = i2a.doc_RID001;
            MultiMasterResponse masters = bTmp ? doc.createTmpDoc() : doc.createDoc();

            if (masters.Result)
            {
                i2a.CHECK_RID001 = "전송성공";
                i2a.error_RID001 = "";
                RefreshGridMain();
            }
            else
            {
                i2a.CHECK_RID001 = "전송실패";
                i2a.error_RID001 = masters[0].ErrorMessage;
                RefreshGridMain();
            }
        }

        private void SendOCQ001(CDataTI2A i2a, bool bTmp)
        {
            // 전송
            if ("점검완료".Equals(i2a.CHECK_OCQ001) == false) return;
            i2a.CHECK_OCQ001 = "전송중";
            RefreshGridMain();

            HIRA.EformEntry.Model.Document doc = i2a.doc_OCQ001;
            MultiMasterResponse masters = bTmp ? doc.createTmpDoc() : doc.createDoc();

            if (masters.Result)
            {
                i2a.CHECK_OCQ001 = "전송성공";
                i2a.error_OCQ001 = "";
                RefreshGridMain();
            }
            else
            {
                i2a.CHECK_OCQ001 = "전송실패";
                i2a.error_OCQ001 = masters[0].ErrorMessage;
                RefreshGridMain();
            }
        }

        private void btnMakeAndCheckAndSend_Click(object sender, EventArgs e)
        {
            //if (m_ADD7000E_ERR == null) m_ADD7000E_ERR = new ADD7000E_ERR();
            //if (m_ADD7000E_ERR.Visible == true) m_ADD7000E_ERR.Visible = false;
            this.MakeAndCheck();
            this.Send(true);
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            string strYkiho = "11111111";
            string strSuplDataFomCd = "ERD001";
            string strFomVer = "000";
            
            //Document 클래스 선언
            Document doc = new Document();

            //Metadata 입력
            doc.Metadata["YKIHO"].Value = strYkiho;
            doc.Metadata["SUPL_DATA_FOM_CD"].Value = strSuplDataFomCd;
            doc.Metadata["FOM_VER"].Value = strFomVer;
            doc.Metadata["DMD_NO"].Value = "2018100102";
            doc.Metadata["RCV_NO"].Value = "4000002";
            doc.Metadata["RCV_YR"].Value = "2018";
            doc.Metadata["SP_SNO"].Value = "00002";
            doc.Metadata["BILL_SNO"].Value = "1";
            doc.Metadata["HOSP_RNO"].Value = "10722812";
            doc.Metadata["PAT_NM"].Value = "신진단";
            doc.Metadata["PAT_JNO"].Value = "5509241000000";
            doc.Metadata["INSUP_TP_CD"].Value = "7";
            doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "01";

            //Elements 입력
            doc.Elements["DGSBJT_CD"].Value = "15";
            doc.Elements["PRSC_DR_NM"].Value = "오처방";
            doc.Elements["PRSC_DR_LCS_KND_CD"].Value = "1";
            doc.Elements["PRSC_DR_LCS_NO"].Value = "19651";
            doc.Elements["EXM_SPCM_NO"].Value = "18091600182";
            doc.Elements["EXM_PRSC_DT"].Value = "201809161001";
            doc.Elements["EXM_GAT_DT"].Value = "201809161001";
            doc.Elements["EXM_RCV_DT"].Value = "201809161008";
            doc.Elements["EXM_RST_DT"].Value = "201809161012";
            doc.Elements["DCT_DR_NM"].Value = "고판독";
            doc.Elements["DCT_DR_LCS_NO"].Value = "631";

            //Table 입력
            //Table Column 입력
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_MDFEE_CD");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_CD");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_NM");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_RST_TXT");

            //행추가 및 데이터 입력
            doc.Tables["TBL_TXT_EXM"].Rows.AddRow();
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "D2510";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_CD"].Value = "L80150";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_NM"].Value = "LDH(EM)";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_TXT"].Value = "negative";

            //Table Column 입력
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_MDFEE_CD");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_CD");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_NM");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_TXT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_REF_TXT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_UNIT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_ADD_TXT");

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_CD"].Value = "L80010";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_NM"].Value = "Hb (EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_RST_TXT"].Value = "12.8";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_REF_TXT"].Value = "13.0 ~ 17.3";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_UNIT"].Value = "g/dL";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_ADD_TXT"].Value = "L";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_CD"].Value = "L80014";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_NM"].Value = "Hct(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_RST_TXT"].Value = "39.1";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_REF_TXT"].Value = "40.0 ~ 52.0";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_ADD_TXT"].Value = "L";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_CD"].Value = "L80058";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_NM"].Value = "MCV(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_RST_TXT"].Value = "96.7";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_REF_TXT"].Value = "80.0 ~ 99.0";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_UNIT"].Value = "fL";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_CD"].Value = "L80062";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_NM"].Value = "MCH(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_RST_TXT"].Value = "34.6";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_REF_TXT"].Value = "26.0 ~ 34.0";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_UNIT"].Value = "pg";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_ADD_TXT"].Value = "H";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_CD"].Value = "L80066";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_NM"].Value = "MCHC(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_RST_TXT"].Value = "37.1";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_REF_TXT"].Value = "31.8 ~ 37.0";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_UNIT"].Value = "g/dL";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_ADD_TXT"].Value = "H";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_CD"].Value = "L80018";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_NM"].Value = "RDW(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_RST_TXT"].Value = "13.1";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_REF_TXT"].Value = "11.5 ~ 15.5";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_CD"].Value = "L80026";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_NM"].Value = "PLT(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_RST_TXT"].Value = "182";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_REF_TXT"].Value = "130 ~ 400";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_UNIT"].Value = "K/uL";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_CD"].Value = "L80022";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_NM"].Value = "PDW(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_RST_TXT"].Value = "17.4";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_REF_TXT"].Value = "0.00 ~ 99.0";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_ADD_TXT"].Value = "";

            //Elements 입력
            doc.Elements["RMK_TXT"].Value = "";

            //FileInfo fileInfo = new FileInfo("1Mb.txt");

            ////ApndDatas (첨부파일) 입력
            //doc.ApndDatas.Rows.AddRow();
            //doc.ApndDatas.Rows[0]["FORM_TYPE"].Value = "IEF";
            //doc.ApndDatas.Rows[0]["LOCAL_FILE_PTH"].Value = fileInfo.DirectoryName + "\\" + fileInfo.Name;
            //doc.ApndDatas.Rows[0]["ROW_STAT"].Value = "C";

            //서식 추가
            doc.addDoc();

            //서식 기재형식 점검 및 Json 파싱
            MultiJsonConvertResponse jsonResponse = doc.ToJson();

            //Panel panel = sender.Equals(btnCreateDoc) ? pnlCreateDoc : pnlCreateTmpDoc;
            StringBuilder sbRequest = new StringBuilder();
            StringBuilder sbResponse = new StringBuilder();
            StringBuilder sbResponseJson = new StringBuilder();

            if (jsonResponse.Result)        //모든 서식지가 기재형식 점검에 성공했을 경우
            {
                for (int i = 0; i < jsonResponse.Count; i++)
                {
                    sbRequest.AppendLine(jsonResponse[i].Json);
                }
                txtMsg.Text = sbRequest.ToString();
            }
            else                            //서식지가 없거나 또는 단 하나라도 기재형식 점검에 실패했을 경우
            {
                if (jsonResponse.Count < 1) //기재형식 점검할 서식지가 없을 경우 (addDoc 함수를 호출하지 않았을 경우)
                {
                    txtMsg.Text = jsonResponse.ErrorMessage;
                }
                else                        //여러 서식지 중, 단 하나라도 실패했을 경우
                {
                    for (int i = 0; i < jsonResponse.Count; i++)
                    {
                        sbResponse.AppendLine(jsonResponse[i].ErrorMessage);
                        //만약 실패한 부분만 메시지를 확인하려면 
                        //jsonResponse[i].Result가 false것만 거르면 된다.
                    }
                    txtMsg.Text = sbResponse.ToString();
                }

                return;
            }

            return;

            /*
            //createDoc : 문서 제출, createTmpDoc : 임시 문서 제출
            MultiMasterResponse response = doc.createTmpDoc();

            if (response.Result)            //모든 서식지가 최종제출 및 임시제출에 성공했을 경우
            {
                for (int i = 0; i < response.Count; i++)
                {
                    sbResponse.AppendLine(string.Format("DOC_NO : {0}\r\nSUPL_DATA_FOM_CD : {1}\r\nRCV_NO : {2}\r\nSP_SNO : {3}\r\nHOSP_RNO : {4}\r\nPAT_NM : {5}\r\nINSUP_TC_CD : {6}\r\n",
                        response[i].Datas["DOC_NO"].Value, response[i].Datas["SUPL_DATA_FOM_CD"].Value, response[i].Datas["RCV_NO"].Value, response[i].Datas["SP_SNO"].Value,
                        response[i].Datas["HOSP_RNO"].Value, response[i].Datas["PAT_NM"].Value, response[i].Datas["INSUP_TP_CD"].Value));

                    sbResponseJson.AppendLine(response[i].ToJson());
                }
            }
            else                            //서식지가 없거나 또는 단 하나라도 최종제출 및 임시제출에 실패했을 경우
            {
                if (response.Count < 1)     //제출할 서식지가 없을 경우 (addDoc 함수를 호출하지 않았을 경우)
                {
                    //panel.SetValue("Response", response.ErrorMessage);
                }
                else                        //여러 서식지 중, 단 하나라도 실패했을 경우
                {
                    for (int i = 0; i < response.Count; i++)
                    {
                        sbResponse.AppendLine(response[i].ErrorMessage);
                        sbResponseJson.AppendLine(response[i].ToJson());
                        //만약 실패한 부분만 메시지를 확인하려면 
                        //response[i].Result가 false것만 거르면 된다.
                    }
                }
            }
            */
        }

        private void btnSendOne_Click(object sender, EventArgs e)
        {
            this.SendOne(false);
        }

        private void SendOne(bool bTmp)
        {
            try
            {
                bool bCheckError = false;
                Cursor.Current = Cursors.WaitCursor;

                List<CDataTI2A> list = (List<CDataTI2A>)grdMain.DataSource;

                // 점검완료여부 점검
                int row = grdMainView.FocusedRowHandle;

                CDataTI2A i2a = list[row];

                if ("점검완료".Equals(i2a.CHECK_ORM001) == false) bCheckError = true;
                if ("점검완료".Equals(i2a.CHECK_RID001) == false) bCheckError = true;
                if ("점검완료".Equals(i2a.CHECK_OCQ001) == false) bCheckError = true;

                if (bCheckError == true)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("점검완료되지 않은 내역이 있습니다. 전송을 취소합니다.", "오류");
                    return;
                }

                // 전송작업
                SendORM001(i2a, bTmp);
                SendRID001(i2a, bTmp);
                SendOCQ001(i2a, bTmp);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "오류");
            }

        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;
            int row = e.RowHandle;
            if (row < 0) return;

            if ("gcCHECK_ORM001".Equals(e.Column.Name)||"gcCHECK_RID001".Equals(e.Column.Name)||"gcCHECK_OCQ001".Equals(e.Column.Name))
            {
                if (e.CellValue == null) return;
                String val = e.CellValue.ToString();
                if ("점검오류".Equals(val) || "전송실패".Equals(val))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ADD8002Q
{
    public partial class ADD8002Q : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private Boolean IsFirst;

        public ADD8002Q()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            IsFirst = true;
        }

        public ADD8002Q(String user, String pwd, String prjcd, String accno, String cntno)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;

            if (accno != "" && cntno!="")
            {
                txtAccno.Text = accno;
                txtCntno.Text = cntno;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == MetroLib.Win32API.WM_COPYDATA)
            {
                MetroLib.Win32API.COPYDATASTRUCT lParam1 = (MetroLib.Win32API.COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(MetroLib.Win32API.COPYDATASTRUCT));
                MetroLib.Win32API.COPYDATASTRUCT lParam2 = new MetroLib.Win32API.COPYDATASTRUCT();
                lParam2 = (MetroLib.Win32API.COPYDATASTRUCT)m.GetLParam(lParam2.GetType());

                //MessageBox.Show(lParam1.lpData);
                //MessageBox.Show(lParam2.lpData);

                // 사용자 메시지이면
                String accno = "";
                String cntno = "";
                ParseArg(lParam1.lpData, ref accno, ref cntno);
                if (accno != "" && cntno != "")
                {
                    txtAccno.Text = accno;
                    txtCntno.Text = cntno;
                    btnQuery.PerformClick();
                }
            }
        }

        private void ParseArg(String arg, ref String accno, ref String cntno)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("ACCNO".Equals(val[0].ToUpper())) accno = val[1];
                else if ("CNTNO".Equals(val[0].ToUpper())) cntno = val[1];
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            String strAccno = txtAccno.Text.ToString();
            String strCntno = txtCntno.Text.ToString();

            if (strAccno != "" && strCntno == "")
            {
                txtCntno.Text = "1";
                strCntno = "1";
            }

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                long I0203Count = GetI0203Count(strAccno, strCntno, conn);

                string sql = "";

                sql = "";
                sql += System.Environment.NewLine +  "SELECT *";
                sql += System.Environment.NewLine +  "     , (SELECT B.CDNM FROM TI88 B WHERE B.MST1CD='A' AND B.MST2CD='BANKNM' AND B.MST3CD=A.BANKCD) AS BANKNM";
                sql += System.Environment.NewLine +  "     , (SELECT FLD1QTY FROM TA88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='1') AS HOSNM";
                sql += System.Environment.NewLine + "  FROM TIE_I020 A";
                sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + strAccno + "' ";
                sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + strCntno + "' ";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        String strBussdiv = reader["BUSSDIV"].ToString().Trim();
                        String strBussdivnm = "";
                        if (strBussdiv == "1") strBussdivnm = "산재보험 요양급여비용";
                        else if (strBussdiv == "2") strBussdivnm = "후유증상관리비용";
                        else if (strBussdiv == "3") strBussdivnm = "진폐건강진단비용";
                        else strBussdivnm = strBussdiv;

                        CData data = new CData();
                        data.Clear();
                        data.BUSSDIVNM = strBussdivnm;
                        data.DEMNO = reader["DEMNO"].ToString().Trim();
                        data.SENDNO = reader["SENDNO"].ToString().Trim();
                        data.ACCNO = reader["ACCNO"].ToString().Trim();
                        data.ACCDT = reader["ACCDT"].ToString().Trim();
                        data.HOSID = reader["HOSID"].ToString().Trim();
                        data.HOSNM = reader["HOSNM"].ToString().Trim();
                        data.CEONM = reader["CEONM"].ToString().Trim();
                        data.CNTTOT = reader["CNTTOT"].ToString().Trim();
                        data.DEMTOT = reader["DEMTOT"].ToString().Trim();
                        data.PAYTOT = reader["PAYTOT"].ToString().Trim();
                        data.PAYQYTOT = reader["PAYQYTOT"].ToString().Trim();
                        data.BULCNT = reader["BULCNT"].ToString().Trim();
                        data.BULAMT = reader["BULAMT"].ToString().Trim();
                        data.INCOMETAX = reader["INCOMETAX"].ToString().Trim();
                        data.INHABITAX = reader["INHABITAX"].ToString().Trim();
                        data.TAXTOT = reader["TAXTOT"].ToString().Trim();
                        data.PAYRSVCNTTOT = reader["PAYRSVCNTTOT"].ToString().Trim();
                        data.PAYRSVQYTOT = reader["PAYRSVQYTOT"].ToString().Trim();
                        data.PREPAYAMT = reader["PREPAYAMT"].ToString().Trim();
                        data.PAYAMTTOT = reader["PAYAMTTOT"].ToString().Trim();
                        data.PREPAYDT = reader["PREPAYDT"].ToString().Trim();
                        data.REALPAYAMT = reader["REALPAYAMT"].ToString().Trim();
                        data.BUNMEMO = reader["BUNMEMO"].ToString().Trim().Trim();
                        data.MEMO = reader["MEMO"].ToString().Trim().Trim();
                        data.BANKCD = reader["BANKCD"].ToString().Trim();
                        data.BANKNM = reader["BANKNM"].ToString().Trim().Trim();
                        data.ACCOUNT = reader["ACCOUNT"].ToString().Trim().Trim();
                        data.REPDT = reader["REPDT"].ToString().Trim().Trim();
                        data.I0203_COUNT = I0203Count;


                        list.Add(data);
                    }
                    reader.Close();
                }

                this.QuerySub(strAccno, strCntno, conn);
                this.QueryDetail(strAccno, strCntno, conn);
                //this.QuerySubAndDetail(strAccno, strCntno, conn);
                this.QueryComb(strAccno, strCntno, conn);

                conn.Close();
            }

            this.RefreshGridMain();
        }

        private long GetI0203Count(String p_accno, String p_cntno, OleDbConnection p_conn)
        {
            long retvalue = 0;

            string sql="";
            sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
            sql += System.Environment.NewLine + "  FROM TIE_I0203 A INNER JOIN TIE_I0202 B ON B.ACCNO=A.ACCNO AND B.CNTNO=A.CNTNO AND B.EPRTNO=A.EPRTNO";
            sql += System.Environment.NewLine + " WHERE A.ACCNO='" + p_accno + "'";
            sql += System.Environment.NewLine + "   AND A.CNTNO='" + p_cntno + "'";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    long.TryParse( reader["CNT"].ToString(), out retvalue);
                }
                reader.Close();
            }
            return retvalue;
        }

        private void QuerySub(String p_accno, String p_cntno, OleDbConnection p_conn)
        {
            List<CDataSub> list = new List<CDataSub>();

            grdSub.DataSource = null;
            grdSub.DataSource = list;

            string sql = "";

            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TIE_I0202 A";
            sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
            sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
            sql += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataSub data = new CDataSub();
                data.Clear();
                data.PNM = reader["PNM"].ToString().Trim();
                data.EPRTNO = reader["EPRTNO"].ToString().Trim();
                data.JRGB = reader["JRGB"].ToString().Trim();
                data.GENDT = reader["GENDT"].ToString().Trim();
                data.FTDAYS = reader["FTDAYS"].ToString().Trim();
                data.UNAMT = Convert.ToInt64(reader["UNAMT"].ToString().Trim());
                data.JJAMT = Convert.ToInt64(reader["JJAMT"].ToString().Trim());
                data.PAYAMT = Convert.ToInt64(reader["PAYAMT"].ToString().Trim());
                data.BULAMT = Convert.ToInt64(reader["BULAMT"].ToString().Trim());
                data.BOAMT = Convert.ToInt64(reader["BOAMT"].ToString().Trim());
                data.ORDDAYS = Convert.ToInt64(reader["ORDDAYS"].ToString().Trim());
                data.BULRMK = reader["BULRMK"].ToString().Trim();
                data.MEMO = reader["MEMO"].ToString().Trim();

                list.Add(data);

                return true;
            });


            // 출력하면 합치기하지 앟은 상태로 출력됨. 그래서 막음.
            //// 셀 가로로 합치기
            //HorizontalMerging.MyGridViewHandler viewHandler = new HorizontalMerging.MyGridViewHandler(grdSubView);
            //int i = 0;
            //foreach (CDataSub data in list)
            //{
            //    if (data.GENDT=="불능사유"||data.GENDT=="비고")
            //    {
            //        viewHandler.MergeCells(data.FTDAYS, i, new DevExpress.XtraGrid.Columns.GridColumn[] { grdSubView.Columns[4], 
            //                                                                                              grdSubView.Columns[5], 
            //                                                                                              grdSubView.Columns[6], 
            //                                                                                              grdSubView.Columns[7], 
            //                                                                                              grdSubView.Columns[8], 
            //                                                                                              grdSubView.Columns[9], 
            //                                                                                              grdSubView.Columns[10] });
            //    }
            //    i++;
            //}

            this.RefreshGridSub();
        }

        private void QueryDetail(String p_accno, String p_cntno, OleDbConnection p_conn)
        {
            List<CDataDetail> list = new List<CDataDetail>();

            grdDetail.DataSource = null;
            grdDetail.DataSource = list;

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A.*,B.PNM";
            sql += System.Environment.NewLine + "  FROM TIE_I0203 A INNER JOIN TIE_I0202 B ON B.ACCNO=A.ACCNO AND B.CNTNO=A.CNTNO AND B.EPRTNO=A.EPRTNO";
            sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
            sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
            sql += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO,A.LNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                // 청구내역
                CDataDetail data = new CDataDetail();
                data.Clear();
                data.PNM = reader["PNM"].ToString().Trim();
                data.EPRTNO = reader["EPRTNO"].ToString().Trim();
                data.LNO = reader["LNO"].ToString().Trim();
                data.HANGMOKNO = reader["HANGMOKNO"].ToString().Trim();
                data.CDGB = reader["CDGB"].ToString().Trim();

                data.BGIHO = reader["BGIHO"].ToString().Trim();
                data.BGIHONM = reader["BGIHONM"].ToString().Trim();
                data.DANGA = Convert.ToInt64(Convert.ToDouble(reader["DANGA"].ToString().Trim()));
                data.DQTY = Convert.ToDouble(reader["DQTY"].ToString().Trim());
                data.DDAY = Convert.ToInt64(reader["DDAY"].ToString().Trim());
                data.TQTY = 0;
                data.GUMAK = Convert.ToInt64(reader["GUMAK"].ToString().Trim());

                // 조정내역
                data.JJBGIHO = reader["JJBGIHO"].ToString().Trim();
                data.JJBGIHONM = reader["JJBGIHONM"].ToString().Trim();
                data.JJDANGA = Convert.ToInt64(Convert.ToDouble(reader["JJDANGA"].ToString().Trim()));
                data.IJDQTY = Convert.ToDouble(reader["IJDQTY"].ToString().Trim());
                data.IJDAY = Convert.ToInt64(reader["IJDAY"].ToString().Trim());
                data.TIJQTY = Convert.ToDouble(reader["TIJQTY"].ToString().Trim());
                data.JJGUMAK = Convert.ToInt64(reader["JJGUMAK"].ToString().Trim());

                // 조정사유
                data.JJCD = reader["JJCD"].ToString().Trim();
                data.JJRMK = reader["JJRMK"].ToString().Trim();

                list.Add(data);

                return true;
            });

            // 출력하면 합치기하지 앟은 상태로 출력됨. 그래서 막음.
            //// 셀 가로로 합치기
            //HorizontalMerging.MyGridViewHandler viewHandler = new HorizontalMerging.MyGridViewHandler(grdDetailView);
            //int i = 0;
            //foreach (CDataDetail data in list)
            //{
            //    if (data.IsRemarkLine)
            //    {
            //        viewHandler.MergeCells(data.BGIHONM, i, new DevExpress.XtraGrid.Columns.GridColumn[] { grdDetailView.Columns[6], 
            //                                                                                               grdDetailView.Columns[7], 
            //                                                                                               grdDetailView.Columns[8], 
            //                                                                                               grdDetailView.Columns[9], 
            //                                                                                               grdDetailView.Columns[10], 
            //                                                                                               grdDetailView.Columns[11] });
            //    }
            //    i++;
            //}

            this.RefreshGridDetail();
        }

        //private void QuerySubAndDetail(String p_accno, String p_cntno, OleDbConnection p_conn)
        //{
        //    dataSet1.Clear();

        //    grdSubAndDetail.DataSource = null;

        //    string sql = "";

        //    sql = "";
        //    sql += System.Environment.NewLine + "SELECT *";
        //    sql += System.Environment.NewLine + "  FROM TIE_I0202 A";
        //    sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
        //    sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
        //    sql += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO";

        //    // TSQL문장과 Connection 객체를 지정   
        //    using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
        //    {

        //        // 데이타는 서버에서 가져오도록 실행
        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            DataRow dataRow = dataSet1.Tables["SUB"].NewRow();
        //            dataRow["PNM"] =  reader["PNM"].ToString().Trim();
        //            dataRow["EPRTNO"] = reader["EPRTNO"].ToString().Trim();
        //            dataRow["JRGB"] = reader["JRGB"].ToString().Trim();
        //            dataRow["GENDT"] = reader["GENDT"].ToString().Trim();
        //            dataRow["FTDAYS"] = reader["FTDAYS"].ToString().Trim();
        //            dataRow["UNAMT"] = reader["UNAMT"].ToString().Trim();
        //            dataRow["JJAMT"] = reader["JJAMT"].ToString().Trim();
        //            dataRow["PAYAMT"] = reader["PAYAMT"].ToString().Trim();
        //            dataRow["BULAMT"] = reader["BULAMT"].ToString().Trim();
        //            dataRow["BOAMT"] = reader["BOAMT"].ToString().Trim();
        //            dataRow["ORDDAYS"] = reader["ORDDAYS"].ToString().Trim();
        //            dataRow["BULRMK"] = reader["BULRMK"].ToString().Trim();
        //            dataRow["MEMO"] = reader["MEMO"].ToString().Trim();
        //            dataSet1.Tables["SUB"].Rows.Add(dataRow);
        //            //
        //            dataRow = dataSet1.Tables["SUB_BULRMK"].NewRow();
        //            dataRow["PNM"] = reader["PNM"].ToString().Trim();
        //            dataRow["EPRTNO"] = reader["EPRTNO"].ToString().Trim();
        //            dataRow["BULRMK"] = reader["BULRMK"].ToString().Trim();
        //            dataRow["MEMO"] = reader["MEMO"].ToString().Trim();
        //            dataSet1.Tables["SUB_BULRMK"].Rows.Add(dataRow);
        //        }
        //        reader.Close();
        //    }

        //    // ------------
        //    sql = "";
        //    sql += System.Environment.NewLine + "SELECT A.*,B.PNM";
        //    sql += System.Environment.NewLine + "  FROM TIE_I0203 A INNER JOIN TIE_I0202 B ON B.ACCNO=A.ACCNO AND B.CNTNO=A.CNTNO AND B.EPRTNO=A.EPRTNO";
        //    sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
        //    sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
        //    sql += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO,A.LNO";

        //    // TSQL문장과 Connection 객체를 지정   
        //    using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
        //    {

        //        // 데이타는 서버에서 가져오도록 실행
        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            DataRow dataRow = dataSet1.Tables["DETAIL"].NewRow();
        //            dataRow["PNM"] = reader["PNM"].ToString().Trim();
        //            dataRow["EPRTNO"] = reader["EPRTNO"].ToString().Trim();
        //            dataRow["LNO"] = reader["LNO"].ToString().Trim();
        //            dataRow["HANGMOKNO"] = reader["HANGMOKNO"].ToString().Trim();
        //            dataRow["CDGB"] = reader["CDGB"].ToString().Trim();
        //            dataRow["BGIHO"] = reader["BGIHO"].ToString().Trim();
        //            dataRow["BGIHONM"] = reader["BGIHONM"].ToString().Trim();
        //            dataRow["DANGA"] = reader["DANGA"].ToString().Trim();
        //            dataRow["DQTY"] = reader["DQTY"].ToString().Trim();
        //            dataRow["DDAY"] = reader["DDAY"].ToString().Trim();
        //            dataRow["GUMAK"] = reader["GUMAK"].ToString().Trim();
        //            dataRow["JJBGIHO"] = reader["JJBGIHO"].ToString().Trim();
        //            dataRow["JJBGIHONM"] = reader["JJBGIHONM"].ToString().Trim();
        //            dataRow["JJDANGA"] = reader["JJDANGA"].ToString().Trim();
        //            dataRow["IJDQTY"] = reader["IJDQTY"].ToString().Trim();
        //            dataRow["IJDAY"] = reader["IJDAY"].ToString().Trim();
        //            dataRow["TIJQTY"] = reader["TIJQTY"].ToString().Trim();
        //            dataRow["JJGUMAK"] = reader["JJGUMAK"].ToString().Trim();
        //            dataRow["JJCD"] = reader["JJCD"].ToString().Trim();
        //            dataRow["JJRMK"] = reader["JJRMK"].ToString().Trim();
        //            dataSet1.Tables["DETAIL"].Rows.Add(dataRow);

        //            dataRow = dataSet1.Tables["DETAIL_REMARK"].NewRow();
        //            dataRow["PNM"] = reader["PNM"].ToString().Trim();
        //            dataRow["EPRTNO"] = reader["EPRTNO"].ToString().Trim();
        //            dataRow["LNO"] = reader["LNO"].ToString().Trim();
        //            dataRow["JJRMK"] = reader["JJRMK"].ToString().Trim();
        //            dataSet1.Tables["DETAIL_REMARK"].Rows.Add(dataRow);
        //        }
        //        reader.Close();
        //    }

        //    grdSubAndDetail.DataSource = dataSet1.Tables["SUB"];

        //    // view의 속성을 수정한다.
        //    //for (int i = 0; i < grdSubAndDetailView.Columns.Count; i++)
        //    //{
        //    //    if (grdSubAndDetailView.Columns[i].FieldName == "UNAMT" ||
        //    //        grdSubAndDetailView.Columns[i].FieldName == "JJAMT" ||
        //    //        grdSubAndDetailView.Columns[i].FieldName == "PAYAMT" ||
        //    //        grdSubAndDetailView.Columns[i].FieldName == "BULAMT" ||
        //    //        grdSubAndDetailView.Columns[i].FieldName == "BOAMT" ||
        //    //        grdSubAndDetailView.Columns[i].FieldName == "ORDDAYS")
        //    //    {
        //    //        grdSubAndDetailView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //    //    }
        //    //}

        //    // detail view 를 펼쳐보인다.
        //    for (int i = 0; i < grdSubAndDetailView.DataRowCount; i++)
        //    {
        //        // 환자명단을 펼친다.(불능사유와 참고사항이 보이게 된다.)
        //        grdSubAndDetailView.SetMasterRowExpanded(i, true);
        //        DevExpress.XtraGrid.Views.Grid.GridView gridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdSubAndDetailView.GetDetailView(i, grdSubAndDetailView.GetVisibleDetailRelationIndex(i));
        //        if (gridView == null) continue;
                
        //        for (int j = 0; j < gridView.DataRowCount; j++)
        //        {
        //            // 불능사유와 참고사항을 펼친다.(코드가 보이게 된다.)
        //            gridView.SetMasterRowExpanded(j, true);
        //            DevExpress.XtraGrid.Views.Grid.GridView gridView2 = (DevExpress.XtraGrid.Views.Grid.GridView)gridView.GetDetailView(j, gridView.GetVisibleDetailRelationIndex(j));
        //            if (gridView2 == null) continue;
                    
        //            for (int k = 0; k < gridView2.DataRowCount; k++)
        //            {
        //                // 코드를 펼친다.(코드별 사유가 보이게 된다.)
        //                gridView2.SetMasterRowExpanded(k, true);
        //                DevExpress.XtraGrid.Views.Grid.GridView gridView3 = (DevExpress.XtraGrid.Views.Grid.GridView)gridView2.GetDetailView(k, gridView2.GetVisibleDetailRelationIndex(k));
        //                if (gridView3 == null) continue;
                        
        //            }
        //        }
        //    }

        //    this.RefreshGridSubAndDetail();
        //}

        private void QueryComb(String p_accno, String p_cntno, OleDbConnection p_conn)
        {
            grdComb.DataSource = null;
            List<CDataComb> list = new List<CDataComb>();
            grdComb.DataSource = list;

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT *";
            sql += System.Environment.NewLine + "  FROM TIE_I0202 A";
            sql += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
            sql += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
            sql += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataComb data = new CDataComb();
                data.Clear();

                data.PNM = row["PNM"].ToString().TrimEnd();
                data.LNO = row["EPRTNO"].ToString().TrimEnd();
                data.HANGMOKNO = row["JRGB"].ToString().Trim();
                data.BGIHO = row["GENDT"].ToString().Trim();
                data.BGIHONM = row["FTDAYS"].ToString().Trim();
                data.DANGA = row["UNAMT"].ToString().Trim();
                data.DQTY = row["JJAMT"].ToString().Trim();
                data.DDAY = row["PAYAMT"].ToString().Trim();
                data.GUMAK = row["BULAMT"].ToString().Trim();
                data.JJBGIHO = row["BOAMT"].ToString().Trim();
                data.JJBGIHONM = row["ORDDAYS"].ToString().Trim();
                data.JJCD = row["BULRMK"].ToString().Trim();
                data.JJRMK = row["MEMO"].ToString().Trim();
                data.DATA_FG = "1";

                list.Add(data);

                // 코드내역
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A.*";
                sql2 += System.Environment.NewLine + "  FROM TIE_I0203 A";
                sql2 += System.Environment.NewLine + " WHERE A.ACCNO  = '" + p_accno + "' ";
                sql2 += System.Environment.NewLine + "   AND A.CNTNO  = '" + p_cntno + "' ";
                sql2 += System.Environment.NewLine + "   AND A.EPRTNO  = '" + row["EPRTNO"].ToString() + "' ";
                sql2 += System.Environment.NewLine + " ORDER BY A.ACCNO,A.CNTNO,A.EPRTNO,A.LNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data = new CDataComb();
                    data.Clear();

                    data.LNO = row2["LNO"].ToString().TrimEnd();
                    data.HANGMOKNO = row2["HANGMOKNO"].ToString().TrimEnd();
                    data.CDGB = row2["CDGB"].ToString().TrimEnd();
                    data.BGIHO = row2["BGIHO"].ToString().TrimEnd();
                    data.BGIHONM = row2["BGIHONM"].ToString().TrimEnd();
                    data.DANGA = row2["DANGA"].ToString().TrimEnd();
                    data.DQTY = row2["DQTY"].ToString().TrimEnd();
                    data.DDAY = row2["DDAY"].ToString().TrimEnd();
                    data.GUMAK = row2["GUMAK"].ToString().TrimEnd();
                    data.JJBGIHO = row2["JJBGIHO"].ToString().TrimEnd();
                    data.JJBGIHONM = row2["JJBGIHONM"].ToString().TrimEnd();
                    data.JJDANGA = row2["JJDANGA"].ToString().TrimEnd();
                    data.IJDQTY = row2["IJDQTY"].ToString().TrimEnd();
                    data.IJDAY = row2["IJDAY"].ToString().TrimEnd();
                    data.TIJQTY = row2["TIJQTY"].ToString().TrimEnd();
                    data.JJGUMAK = row2["JJGUMAK"].ToString().TrimEnd();
                    data.JJCD = row2["JJCD"].ToString().TrimEnd();
                    data.JJRMK = row2["JJRMK"].ToString().TrimEnd();
                    data.DATA_FG = "2";

                    list.Add(data);

                    return true;
                });

                return true;
            });

            this.RefreshGridComb();
        }

        private void ShowProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormCaption(caption);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        private void CloseProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
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

        private void RefreshGridSub()
        {
            if (grdSub.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridDetail()
        {
            if (grdDetail.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDetail.BeginInvoke(new Action(() => grdDetailView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDetailView.RefreshData();
                Application.DoEvents();
            }
        }

        //private void RefreshGridSubAndDetail()
        //{
        //    if (grdSubAndDetail.InvokeRequired)
        //    {
        //        // 폼 이외의 스레드에서 호출한 경우
        //        grdSubAndDetail.BeginInvoke(new Action(() => grdSubAndDetailView.RefreshData()));
        //    }
        //    else
        //    {
        //        // 폼에서 호출한 경우
        //        grdSubAndDetailView.RefreshData();
        //        Application.DoEvents();
        //    }
        //}

        private void RefreshGridComb()
        {
            if (grdComb.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdComb.BeginInvoke(new Action(() => grdCombView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdCombView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Print()
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            if (tabControl1.SelectedIndex == 0)
            {
                printableComponentLink.Component = grdMain;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                printableComponentLink.Component = grdSub;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                printableComponentLink.Component = grdDetail;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                printableComponentLink.Component = grdComb;
            }
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "산재보험진료비지불결정통지서";
            if (tabControl1.SelectedIndex == 0)
            {
                strTitle = "산재보험진료비지불결정통지서";
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                strTitle = "산재보험진료비지불결정통지서(명단)";
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                strTitle = "산재보험진료비지불결정통지서(코드)";
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                strTitle = "산재보험진료비지불결정통지서(명단+코드)";
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strCaption = "";
            strCaption += "접수번호 : " + txtAccno.Text.ToString();
            strCaption += ", 차수 : " + txtCntno.Text.ToString();
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string sysDate = "";
            string sysTime = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sysDate = MetroLib.Util.GetSysDate(conn);
                sysTime = MetroLib.Util.GetSysTime(conn);
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD8002Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ADD8002Q_P f = new ADD8002Q_P();
            f.ShowDialog(this);
            Boolean bSel = f.m_SEL;
            String accno = f.m_ACCNO;
            String cntno = f.m_CNTNO;
            f = null;
            if (bSel == true)
            {
                txtAccno.Text = accno;
                txtCntno.Text = cntno;

                this.btnQuery.PerformClick();
            }
        }

        private void grdDetailView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            String pnm1 = view.GetRowCellValue(e.RowHandle1, "PNM").ToString();
            String pnm2 = view.GetRowCellValue(e.RowHandle2, "PNM").ToString();
            String eprtno1 = view.GetRowCellValue(e.RowHandle1, "EPRTNO").ToString();
            String eprtno2 = view.GetRowCellValue(e.RowHandle2, "EPRTNO").ToString();
            String lno1 = view.GetRowCellValue(e.RowHandle1, "LNO").ToString();
            String lno2 = view.GetRowCellValue(e.RowHandle2, "LNO").ToString();

            if (pnm1 == pnm2 && eprtno1 == eprtno2 && lno1 == lno2)
            {
                // 같은 경우만 셀 병합
            }
            else
            {
                // 환자가 달라지면 병합하지 않는다.
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void grdSubView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            String pnm1 = view.GetRowCellValue(e.RowHandle1, "PNM").ToString();
            String pnm2 = view.GetRowCellValue(e.RowHandle2, "PNM").ToString();
            String eprtno1 = view.GetRowCellValue(e.RowHandle1, "EPRTNO").ToString();
            String eprtno2 = view.GetRowCellValue(e.RowHandle2, "EPRTNO").ToString();

            if (pnm1 == pnm2 && eprtno1 == eprtno2)
            {
                // 같은 경우만 셀 병합
            }
            else
            {
                // 환자가 달라지면 병합하지 않는다.
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void grdDetailView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 셀 가로 합치기 후 정렬을 맞추가 위한 작업
            //List<CDataDetail> list = (List<CDataDetail>)grdDetail.DataSource;
            //if(list==null) return;
            //int row = e.RowHandle;
            //if (row < 1) return;
            //CDataDetail data = list[row];
            //if (data.IsRemarkLine)
            //{
            //    // 셀 병합되었으면 왼쪽정렬
            //    if (e.Column.ColumnHandle == 7 || e.Column.ColumnHandle == 8 || e.Column.ColumnHandle == 9 || e.Column.ColumnHandle == 10 || e.Column.ColumnHandle == 11)
            //    {
            //        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //    }
            //}
        }

        private void grdSubView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 셀 가로 합치기 후 정렬을 맞추가 위한 작업
            //List<CDataSub> list = (List<CDataSub>)grdSub.DataSource;
            //if (list == null) return;
            //int row = e.RowHandle;
            //if (row < 1) return;
            //CDataSub data = list[row];
            //if (data.GENDT=="불능사유" || data.GENDT=="비고")
            //{
            //    // 셀 병합되었으면 왼쪽정렬
            //    if (e.Column.ColumnHandle == 5 || e.Column.ColumnHandle == 6 || e.Column.ColumnHandle == 7 || e.Column.ColumnHandle == 8 || e.Column.ColumnHandle == 9 || e.Column.ColumnHandle == 10)
            //    {
            //        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //    }
            //}
        }

        private void ADD8002Q_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            if (txtAccno.Text.ToString() != "" && txtCntno.Text.ToString() != "")
            {
                btnQuery.PerformClick();
            }
        }

        private void ADD8002Q_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void grdSubAndDetailView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            if (e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Column.FieldName == "UNAMT" ||
                    e.Column.FieldName == "JJAMT" ||
                    e.Column.FieldName == "PAYAMT" ||
                    e.Column.FieldName == "BULAMT" ||
                    e.Column.FieldName == "BOAMT" ||
                    e.Column.FieldName == "ORDDAYS")
                {
                    long value = 0;
                    long.TryParse(e.Value.ToString(), out value);
                    e.DisplayText = string.Format("{0:#,0}", value);
                    e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
                else if (e.Column.FieldName == "FTDAYS")
                {
                    string value = e.Value.ToString();
                    e.DisplayText = value.Length <= 8 ? value : value.Substring(0, 8) + "-" + value.Substring(8);
                }
                else if (e.Column.FieldName == "DANGA" ||
                         e.Column.FieldName == "DDAY"||
                         e.Column.FieldName == "GUMAK" ||
                         e.Column.FieldName == "JJDANGA" ||
                         e.Column.FieldName == "IJDAY" ||
                         e.Column.FieldName == "JJGUMAK")
                {
                    double value = 0;
                    double.TryParse(e.Value.ToString(), out value);
                    e.DisplayText = string.Format("{0:#,0}", value);
                    e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
                else if (e.Column.FieldName == "DQTY" ||
                         e.Column.FieldName == "IJDQTY" ||
                         e.Column.FieldName == "TIJQTY")
                {
                    e.Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
            }
        }

        private void grdSubAndDetailView_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView master = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (master == null) return;
            //DevExpress.XtraGrid.Views.Grid.GridView detail = master.GetDetailView(e.RowHandle, e.RelationIndex) as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (detail == null) return;
            //for (int i = 0; i < detail.Columns.Count; i++)
            //{
            //    if (detail.Columns[i].FieldName == "DANGA")
            //    {
            //        detail.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //    }
            //}
            //DevExpress.XtraGrid.Views.Grid.GridView detail2 = detail.GetDetailView(e.RowHandle, e.RelationIndex) as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (detail2 == null) return;
        }

        private void grdSubAndDetail_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = e.View as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            view.CustomColumnDisplayText += grdSubAndDetailView_CustomColumnDisplayText;
        }
    }
}

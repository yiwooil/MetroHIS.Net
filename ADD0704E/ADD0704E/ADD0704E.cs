using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0704E
{
    public partial class ADD0704E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private String m_ErrKeys;

        private bool IsFirst;
        private Boolean m_OnPgm;

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox fstBuyFgComboBox;

        public ADD0704E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";

            m_ErrKeys = "";

            MakeComboInGrid();

            //grdMainView.Appearance.FocusedCell.BackColor = Color.LightYellow;
        }

        public ADD0704E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            CreatePopupMenu();
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
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@2", m_Prjcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["MULTIFG"].ToString();
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            fstBuyFgComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

            grdMainView.Columns["FSTBUYFG"].ColumnEdit = fstBuyFgComboBox;

            fstBuyFgComboBox.Items.Clear();
            fstBuyFgComboBox.Items.Add("");
            fstBuyFgComboBox.Items.Add("A");
            fstBuyFgComboBox.Items.Add("B");
            fstBuyFgComboBox.Items.Add("D");
        }

        private void CreatePopupMenu()
        {
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("수량 조절", new EventHandler(mnuAdjustQty_Click)); // 구입단가를 EDI단가와 동일하게 되도록 수량 변경
            //grdMain.ContextMenu = cm;
        }

        private void ADD0704E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0704E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            string hdate = "";

            ReadHDate(ref hdate);

            txtDemdd.Text = hdate.Substring(0, 4);
            string mm = hdate.Substring(4, 2);
            if (mm == "01" || mm == "02" || mm == "03") cboDemdd.SelectedIndex = 0;
            if (mm == "04" || mm == "05" || mm == "06") cboDemdd.SelectedIndex = 1;
            if (mm == "07" || mm == "08" || mm == "09") cboDemdd.SelectedIndex = 2;
            if (mm == "10" || mm == "11" || mm == "12") cboDemdd.SelectedIndex = 3;

            InitButton();
        }

        private void ReadHDate(ref string hdate)
        {
            hdate = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    hdate = MetroLib.Util.GetSysDate(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitButton()
        {
            txtDemdd.Enabled = true;
            cboDemdd.Enabled = true;
            btnQuery.Enabled = true;
            btnExcel.Enabled = false;
            btnPrint.Enabled = false;
            btnInsRow.Enabled = false;
            btnAddRow.Enabled = false;
            btnDelRow.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            rbYHgbn1.Enabled = true;
            rbYHgbn2.Enabled = true;

            grdMain.DataSource = null;
            RefreshGridMain();

        }

        private void mnuAdjustQty_Click(object sender, EventArgs e)
        {
            //List<CData> list = (List<CData>)grdMain.DataSource;
            //if (list == null) return;
            //foreach (CData data in list)
            //{
            //    data.SEL = data.FSTBUYFG == "A";
            //}
            //RefreshGridMain();
            for (int rowHandle = 0; rowHandle < grdMainView.RowCount; rowHandle++)
            {
                long buyamt = MetroLib.StrHelper.ToLong(grdMainView.GetRowCellValue(rowHandle, "BUYAMT").ToString()); // 구입단가
                long kumak = MetroLib.StrHelper.ToLong(grdMainView.GetRowCellValue(rowHandle, "KUMAK").ToString()); // EDI단가
                long buytotamt = MetroLib.StrHelper.ToLong(grdMainView.GetRowCellValue(rowHandle, "BUYTOTAMT").ToString()); // 구입액
                if (buyamt != kumak)
                {
                    // 구입단가를 EDI단가와 같아지도록 조정함.
                    long buyqty = buytotamt / kumak;
                    grdMainView.SetRowCellValue(rowHandle, "BUYQTY", buyqty);
                    if (buyqty != 0)
                    {
                        buyamt = (long)((double)buytotamt / (double)buyqty + 0.5);
                        grdMainView.SetRowCellValue(rowHandle, "BUYAMT", buyamt);
                    }
                }
            }
            RefreshGridMain();
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

                txtDemdd.Enabled = false;
                cboDemdd.Enabled = false;
                btnQuery.Enabled = false;
                btnExcel.Enabled = true;
                btnPrint.Enabled = true;
                btnInsRow.Enabled = true;
                btnAddRow.Enabled = true;
                btnDelRow.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                rbYHgbn1.Enabled = false;
                rbYHgbn2.Enabled = false;
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
            string demdd = GetDemdd();
            string yhgbn = GetYHgbn();

            grdMain.DataSource=null;
            List<CData> list = new List<CData>();
            grdMain.DataSource=list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT Z.BUYDT,Z.ITEMCD,Z.ITEMINFO,Z.STDSIZE,Z.UNIT,Z.BUYQTY,Z.BUYTOTAMT,Z.BUYAMT,Z.BUSSCD,Z.BUSSNM,Z.FSTBUYFG,Z.MEMO,Z.PRODCM,Z.EMPID,Z.ENTDT,Z.ENTTM,Z.UPDID,Z.UPDDT,Z.UPDTM ,Z.KUMAK,Z.PRICD ";
                sql += Environment.NewLine + "  FROM ";
                sql += Environment.NewLine + "  (SELECT A.BUYDT,A.ITEMCD,A.ITEMINFO,A.STDSIZE,A.UNIT,A.BUYQTY,A.BUYTOTAMT,A.BUYAMT,A.BUSSCD,A.BUSSNM,A.FSTBUYFG,A.MEMO,A.PRODCM,A.EMPID,A.ENTDT,A.ENTTM,A.UPDID,A.UPDDT,A.UPDTM ";
                sql += Environment.NewLine + "        , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG(A.BUYDT)=4 THEN KUMAK2 ELSE KUMAK1 END AS KUMAK";
                sql += Environment.NewLine + "        , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=A.ITEMCD ORDER BY A02.CREDT DESC) AS PRICD ";
                sql += Environment.NewLine + "     FROM TIE_H0800 A, TI09 I09 ";
                sql += Environment.NewLine + "    WHERE A.DEMDD = '" + demdd + "'";
                sql += Environment.NewLine + "      AND A.MULTIHSFG = '" + m_HospMulti + "'";
                sql += Environment.NewLine + "      AND A.YHGBN = '" + yhgbn + "'";
                sql += Environment.NewLine + "      AND I09.PCODE=A.ITEMCD ";
                sql += Environment.NewLine + "      AND I09.GUBUN='3' ";
                sql += Environment.NewLine + "      AND I09.ADTDT=(SELECT MAX(X.ADTDT) ";
                sql += Environment.NewLine + "                       FROM TI09 X ";
                sql += Environment.NewLine + "                      WHERE X.PCODE=A.ITEMCD ";
                sql += Environment.NewLine + "                        AND X.GUBUN='3' ";
                sql += Environment.NewLine + "                        AND X.ADTDT<=A.BUYDT ";
                sql += Environment.NewLine + "                    ) ";
                sql += Environment.NewLine + "    UNION ALL ";
                sql += Environment.NewLine + "   SELECT A.BUYDT,A.ITEMCD,A.ITEMINFO,A.STDSIZE,A.UNIT,A.BUYQTY,A.BUYTOTAMT,A.BUYAMT,A.BUSSCD,A.BUSSNM,A.FSTBUYFG,A.MEMO,A.PRODCM,A.EMPID,A.ENTDT,A.ENTTM,A.UPDID,A.UPDDT,A.UPDTM ";
                sql += Environment.NewLine + "        , 0 AS KUMAK";
                sql += Environment.NewLine + "        , (SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=A.ITEMCD ORDER BY A02.CREDT DESC) AS PRICD ";
                sql += Environment.NewLine + "     FROM TIE_H0800 A, TI09_BYAK I09 ";
                sql += Environment.NewLine + "    WHERE A.DEMDD = '" + demdd + "'";
                sql += Environment.NewLine + "      AND A.MULTIHSFG = '" + m_HospMulti + "'";
                sql += Environment.NewLine + "      AND A.YHGBN = '" + yhgbn + "'";
                sql += Environment.NewLine + "      AND I09.PCODE=A.ITEMCD ";
                sql += Environment.NewLine + "       AND NOT EXISTS (SELECT * FROM TI09 X WHERE X.PCODE = I09.PCODE) ";
                sql += Environment.NewLine + "   ) Z  ";
                sql += Environment.NewLine + " ORDER BY Z.BUYDT,Z.ITEMCD ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.BUYDT = reader["BUYDT"].ToString();
                    data.ITEMCD = reader["ITEMCD"].ToString();
                    data.ITEMINFO = reader["ITEMINFO"].ToString();
                    data.STDSIZE = reader["STDSIZE"].ToString();
                    data.UNIT = reader["UNIT"].ToString();
                    data.BUYQTY = MetroLib.StrHelper.ToLong(reader["BUYQTY"].ToString());
                    data.BUYTOTAMT = MetroLib.StrHelper.ToLong(reader["BUYTOTAMT"].ToString());
                    data.BUYAMT = MetroLib.StrHelper.ToLong(reader["BUYAMT"].ToString());
                    data.BUSSCD = reader["BUSSCD"].ToString();
                    data.BUSSNM = reader["BUSSNM"].ToString();
                    data.FSTBUYFG = reader["FSTBUYFG"].ToString();
                    data.MEMO = reader["MEMO"].ToString();
                    data.PRODCM = reader["PRODCM"].ToString();
                    data.EMPID = reader["EMPID"].ToString();
                    data.ENTDT = reader["ENTDT"].ToString();
                    data.ENTTM = reader["ENTTM"].ToString();
                    data.UPDID = reader["UPDID"].ToString();
                    data.UPDDT = reader["UPDDT"].ToString();
                    data.UPDTM = reader["UPDTM"].ToString();
                    data.KUMAK = reader["KUMAK"].ToString();
                    data.PRICD = reader["PRICD"].ToString();

                    // 수정되었는지 파악하기 위한 용도
                    data.BF_BUYDT = data.BUYDT;
                    data.BF_ITEMCD = data.ITEMCD;
                    data.BF_ITEMINFO = data.ITEMINFO;
                    data.BF_STDSIZE = data.STDSIZE;
                    data.BF_UNIT = data.UNIT;
                    data.BF_BUYQTY = data.BUYQTY;
                    data.BF_BUYTOTAMT = data.BUYTOTAMT;
                    data.BF_BUYAMT = data.BUYAMT;
                    data.BF_BUSSCD = data.BUSSCD;
                    data.BF_BUSSNM = data.BUSSNM;
                    data.BF_FSTBUYFG = data.FSTBUYFG;
                    data.BF_MEMO = data.MEMO;
                    data.BF_PRODCM = data.PRODCM;
                    data.BF_KUMAK = data.KUMAK;

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private string GetDemdd()
        {
            string demdd = txtDemdd.Text.ToString();
            if (cboDemdd.SelectedIndex == 0) demdd += "14";
            else if (cboDemdd.SelectedIndex == 1) demdd += "24";
            else if (cboDemdd.SelectedIndex == 2) demdd += "34";
            else if (cboDemdd.SelectedIndex == 3) demdd += "44";
            return demdd;
        }

        private string GetYHgbn()
        {
            return rbYHgbn1.Checked == true ? "1" : "2";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grdMainView.RowCount > 0)
            {
                if (MessageBox.Show("지금까지 한 작업이 모두 취소됩니다. 계속 하시겠습니까?", btnCancel.Text.ToString(), MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            InitButton();
        }

        private void btnInsRow_Click(object sender, EventArgs e)
        {
            int rowHandle = grdMainView.FocusedRowHandle;
            if (rowHandle < 0) return;
            if (rowHandle >= grdMainView.RowCount) return;


            string buydt = "";
            string busscd = "";
            string bussnm = "";

            if (rowHandle > 0)
            {
                buydt = grdMainView.GetRowCellValue(rowHandle - 1, "BUYDT").ToString();
                busscd = grdMainView.GetRowCellValue(rowHandle - 1, "BUSSCD").ToString();
                bussnm = grdMainView.GetRowCellValue(rowHandle - 1, "BUSSNM").ToString();
            }

            List<CData> list = (List<CData>)grdMainView.DataSource;
            CData data = new CData();
            data.Clear();
            data.BUYDT = buydt;
            data.BUSSCD = busscd;
            data.BUSSNM = bussnm;

            list.Insert(rowHandle, data);

            RefreshGridMain();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            string buydt = "";
            string busscd = "";
            string bussnm = "";

            if (grdMainView.RowCount > 0)
            {
                buydt = grdMainView.GetRowCellValue(grdMainView.RowCount - 1, "BUYDT").ToString();
                busscd = grdMainView.GetRowCellValue(grdMainView.RowCount - 1, "BUSSCD").ToString();
                bussnm = grdMainView.GetRowCellValue(grdMainView.RowCount - 1, "BUSSNM").ToString();
            }

            List<CData> list = (List<CData>)grdMainView.DataSource;
            CData data = new CData();
            data.Clear();
            data.BUYDT = buydt;
            data.BUSSCD = busscd;
            data.BUSSNM = bussnm;

            list.Add(data);

            if (buydt != "")
            {
                // 포커스를 약품코드로 이동시킨다.
                grdMainView.FocusedRowHandle = grdMainView.RowCount - 1;
                grdMainView.FocusedColumn = grdMainView.Columns["ITEMCD"];
            }
            else
            {
                // 포커스를 구입일자로 이동시킨다.
                grdMainView.FocusedRowHandle = grdMainView.RowCount - 1;
                grdMainView.FocusedColumn = grdMainView.Columns["BUYDT"];
            }
            
            grdMainView.Focus();

            RefreshGridMain();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            int rowHandle = grdMainView.FocusedRowHandle;
            if (rowHandle < 0) return;
            if (rowHandle >= grdMainView.RowCount) return;

            List<CData> list = (List<CData>)grdMainView.DataSource;

            list.RemoveAt(rowHandle);

            RefreshGridMain();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInputValue() == false) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                this.InitButton();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + Environment.NewLine + m_ErrKeys);
            }
        }

        private bool CheckInputValue()
        {
            string minDate = "";
            string maxDate = "";
            if (cboDemdd.SelectedIndex == 0)
            {
                minDate = txtDemdd.Text.ToString() + "0101";
                maxDate = txtDemdd.Text.ToString() + "0331";
            }
            else if (cboDemdd.SelectedIndex == 1)
            {
                minDate = txtDemdd.Text.ToString() + "0401";
                maxDate = txtDemdd.Text.ToString() + "0630";
            }
            else if (cboDemdd.SelectedIndex == 2)
            {
                minDate = txtDemdd.Text.ToString() + "0701";
                maxDate = txtDemdd.Text.ToString() + "0930";
            }
            else if (cboDemdd.SelectedIndex == 3)
            {
                minDate = txtDemdd.Text.ToString() + "1001";
                maxDate = txtDemdd.Text.ToString() + "1231";
            }

            // 입력값 점검
            int lineNo = 0;
            List<CData> list = (List<CData>)grdMainView.DataSource;
            foreach (CData data in list)
            {
                lineNo++;
                if (data.BUYDT.CompareTo(minDate) < 0 || data.BUYDT.CompareTo(maxDate) > 0)
                {
                    MessageBox.Show(lineNo + "줄 : 구입일자를 확인하세요.");
                    return false;
                }
                if (Math.Abs(data.BUYTOTAMT) < Math.Abs(data.BUYQTY))
                {
                    MessageBox.Show(lineNo + "줄 : 금액보다 수량이 클 수 없습니다.");
                    return false;
                }
                if (data.FSTBUYFG == "D")
                {
                    if (data.MEMO.Length != 8 || MetroLib.Util.ValDt(data.MEMO) == false)
                    {
                        MessageBox.Show(lineNo + "줄 : D코드 청구건입니다. 참고사항에 실제 적용한 일자를 기재하십시오.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void Save()
        {
            string yhgbn = GetYHgbn();
            string demdd = txtDemdd.Text.ToString();

            if (cboDemdd.SelectedIndex == 0) demdd = demdd + "14";
            else if (cboDemdd.SelectedIndex == 1) demdd = demdd + "24";
            else if (cboDemdd.SelectedIndex == 2) demdd = demdd + "34";
            else if (cboDemdd.SelectedIndex == 3) demdd = demdd + "44";

            List<CData> list = (List<CData>)grdMainView.DataSource;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try{
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string hdate = MetroLib.Util.GetSysDate(conn, tran);
                    string htime = MetroLib.Util.GetSysTime(conn, tran);

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "DELETE";
                    sql += Environment.NewLine + "  FROM TIE_H0800";
                    sql += Environment.NewLine + " WHERE DEMDD = '" + demdd + "'";

                    MetroLib.SqlHelper.ExecuteSql(sql,conn,tran);

                    m_ErrKeys = "";

                    int seq=0;
                    foreach (CData data in list){
                        if (data.isNew == true)
                        {
                            data.EMPID = m_User;
                            data.ENTDT = hdate;
                            data.ENTTM = htime;
                        }
                        else if (data.IsChange == true)
                        {
                            data.UPDID = m_User;
                            data.UPDDT = hdate;
                            data.UPDTM = htime;
                        }

                        m_ErrKeys = "구입일자=" + data.BUYDT + ", 코드=" + data.ITEMCD;

                        List<Object> para=new List<object>();
                        para.Clear();
                        para.Add(demdd);
                        para.Add(++seq);
                        para.Add(data.BUYDT);
                        para.Add(data.ITEMCD);
                        para.Add(data.ITEMINFO);
                        para.Add(data.STDSIZE);
                        para.Add(data.UNIT);
                        para.Add(data.BUYQTY);
                        para.Add(data.BUYTOTAMT);
                        para.Add(data.BUYAMT);
                        para.Add(data.BUSSCD);
                        para.Add(data.BUSSNM);
                        para.Add(data.FSTBUYFG);
                        para.Add(data.PRODCM);
                        para.Add(data.MEMO);
                        para.Add(data.EMPID);
                        para.Add(data.ENTDT);
                        para.Add(data.ENTTM);
                        para.Add(data.UPDID);
                        para.Add(data.UPDDT);
                        para.Add(data.UPDTM);
                        para.Add(m_HospMulti);
                        para.Add(yhgbn);

                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO TIE_H0800(DEMDD,SEQ,BUYDT,ITEMCD,ITEMINFO,STDSIZE,UNIT,BUYQTY,BUYTOTAMT,BUYAMT,BUSSCD,BUSSNM,FSTBUYFG,PRODCM,MEMO,EMPID,ENTDT,ENTTM,UPDID,UPDDT,UPDTM,MULTIHSFG,YHGBN)";
                        sql += Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
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
            grdMainView.Columns["EMPID"].Visible = false; // 출력할 때는 안나오게
            grdMainView.Columns["ENTDT"].Visible = false; // 출력할 때는 안나오게
            grdMainView.Columns["ENTTM"].Visible = false; // 출력할 때는 안나오게
            grdMainView.Columns["UPDID"].Visible = false; // 출력할 때는 안나오게
            grdMainView.Columns["UPDDT"].Visible = false; // 출력할 때는 안나오게
            grdMainView.Columns["UPDTM"].Visible = false; // 출력할 때는 안나오게
            
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            printableComponentLink.Component = grdMain;
            grdMainView.OptionsPrint.AutoWidth = false; // 이 값이 true이면 출력시 column의 폭을 자동으로 조절하여 한 페이지에 출력되게 한다.
            printableComponentLink.ShowPreview();

            grdMainView.Columns["UPDTM"].Visible = true; // 출력할 때는 안나오게
            grdMainView.Columns["UPDDT"].Visible = true; // 출력할 때는 안나오게
            grdMainView.Columns["UPDID"].Visible = true; // 출력할 때는 안나오게
            grdMainView.Columns["ENTTM"].Visible = true; // 출력할 때는 안나오게
            grdMainView.Columns["ENTDT"].Visible = true; // 출력할 때는 안나오게
            grdMainView.Columns["EMPID"].Visible = true; // 출력할 때는 안나오게
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("의약품구입내역", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string demdd = txtDemdd.Text.ToString();
            if (cboDemdd.SelectedIndex == 0) demdd = demdd + " 년도 14 분기";
            else if (cboDemdd.SelectedIndex == 1) demdd = demdd + " 년도 24 분기";
            else if (cboDemdd.SelectedIndex == 2) demdd = demdd +  "년도 3 4분기";
            else if (cboDemdd.SelectedIndex == 3) demdd = demdd + " 년도 4 4분기";
            string msg = demdd;
            // 조회조건 출력
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(msg, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string prtDate = "";
            string prtTime = "";
            GetDateTime(out prtDate, out prtTime);

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0704E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(prtDate + " " + prtTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void GetDateTime(out string out_date, out string out_time)
        {
            out_date = "";
            out_time = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    out_date = MetroLib.Util.GetSysDate(conn);
                    out_time = MetroLib.Util.GetSysTime(conn);

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                out_date = DateTime.Now.ToString("yyyyMMdd");
                out_time = DateTime.Now.ToString("HHmmss");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ADD0704E_3 f = new ADD0704E_3();
            f.ApplyButtonClick += new EventHandler(f_ApplyButtonClick);
            f.ShowDialog();
        }

        void f_ApplyButtonClick(object sender, EventArgs e)
        {

            string demdd = GetDemdd();
            string minDate = "";
            string maxDate = "";
            if (cboDemdd.SelectedIndex == 0)
            {
                minDate = txtDemdd.Text.ToString() + "0101";
                maxDate = txtDemdd.Text.ToString() + "0331";
            }
            else if (cboDemdd.SelectedIndex == 1)
            {
                minDate = txtDemdd.Text.ToString() + "0401";
                maxDate = txtDemdd.Text.ToString() + "0630";
            }
            else if (cboDemdd.SelectedIndex == 2)
            {
                minDate = txtDemdd.Text.ToString() + "0701";
                maxDate = txtDemdd.Text.ToString() + "0930";
            }
            else if (cboDemdd.SelectedIndex == 3)
            {
                minDate = txtDemdd.Text.ToString() + "1001";
                maxDate = txtDemdd.Text.ToString() + "1231";
            }

            ((ADD0704E_3.MyEventArgs)e).ERR_CD = "";
            ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "";

            string itemcd = ((ADD0704E_3.MyEventArgs)e).ITEMCD;
            string buydt = ((ADD0704E_3.MyEventArgs)e).BUYDT;
            string busscd = ((ADD0704E_3.MyEventArgs)e).BUSSCD;
            long buytotamt = ((ADD0704E_3.MyEventArgs)e).BUYTOTAMT;
            long buyqty = ((ADD0704E_3.MyEventArgs)e).BUYQTY;
            long buyamt = 0;
            if (buyqty != 0) buyamt = (long)((double)buytotamt / (double)buyqty + 0.5);

            if (buydt.Length != 8)
            {
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "1";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "구입일자를 확인하세요.";
                return;
            }
            if (MetroLib.Util.ValDt(buydt) == false)
            {
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "1";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "구입일자를 확인하세요.";
                return;
            }
            if (buydt.CompareTo(minDate) < 0 || buydt.CompareTo(maxDate) > 0)
            {
                //MessageBox.Show("구입일자를 확인하세요. 해당분기에서 벗어났습니다.(" + buydt + ")");
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "1";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "구입일자를 확인하세요. 해당분기에서 벗어났습니다.";
                return;
            }
            if (Math.Abs(buytotamt) < Math.Abs(buyqty))
            {
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "2";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "금액보다 수량이 클 수 없습니다.";
                return;
            }
            // edi코드확인
            string iteminfo = "";
            string prodcm = "";
            string stdsize = "";
            string unit = "";
            string kumak = "";
            string pricd = "";
            string ipamt = "";
            if (GetIteminfo(itemcd, buydt, out iteminfo, out prodcm, out stdsize, out unit, out kumak, out pricd, out ipamt) < 1)
            {
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "3";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "약품코드를 찾을 수 없습니다.";
                return;
            }
            // 등록된 거래처인지
            string bussnm = GetBussnm(busscd);
            if (bussnm == "")
            {
                ((ADD0704E_3.MyEventArgs)e).ERR_CD = "4";
                ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "구입기관이 등록되지 않았습니다.";
                return;
            }
            List<CData> list = (List<CData>)grdMainView.DataSource;
            // 2024.02.08 WOOIL - VB에 중복점검기능이 없음. 동일하게 만든다.
            //// 중복여부점검
            //foreach (CData d in list)
            //{
            //    if (buydt == d.BUYDT && itemcd == d.ITEMCD && busscd == d.BUSSCD && buytotamt == d.BUYTOTAMT && buyqty == d.BUYQTY)
            //    {
            //        ((ADD0704E_3.MyEventArgs)e).ERR_CD = "5";
            //        ((ADD0704E_3.MyEventArgs)e).ERR_MSG = "중복자료입니다.";
            //        return;
            //    }
            //}

            // 자료추가

            CData data = new CData();
            data.Clear();
            data.BUYDT = buydt;
            data.ITEMCD = itemcd;
            data.ITEMINFO = iteminfo;
            data.STDSIZE = stdsize;
            data.UNIT = unit;
            data.BUYQTY = buyqty;
            data.BUYTOTAMT = buytotamt;
            data.BUYAMT = buyamt;
            data.BUSSCD = busscd;
            data.BUSSNM = bussnm;
            data.FSTBUYFG = "";
            data.MEMO = "";
            data.PRODCM = prodcm;
            data.EMPID = m_User;
            data.KUMAK = kumak;
            data.PRICD = pricd;

            if (((ADD0704E_3.MyEventArgs)e).ADJUST_QTY == true)
            {
                long long_kumak = MetroLib.StrHelper.ToLong(kumak);
                if (long_kumak != 0)
                {
                    if (buyamt != long_kumak)
                    {
                        // 구입단가를 EDI단가와 같아지도록 조정함.
                        buyqty = buytotamt / long_kumak;
                        if (buyqty != 0)
                        {
                            data.BUYQTY = buyqty;
                            data.BUYAMT = (long)((double)buytotamt / (double)buyqty + 0.5);
                        }
                    }
                }
            }

            list.Add(data);

            RefreshGridMain();
        }

        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;  
            if (view.FocusedColumn.FieldName == "ITEMCD")
            {
                // 약품코드입력
                try
                {
                    List<CData> list = (List<CData>)grdMain.DataSource;
                    if (list == null) return;

                    int row = view.FocusedRowHandle;
                    string buydt = view.GetRowCellValue(row, "BUYDT").ToString();

                    string iteminfo = "";
                    string prodcm = "";
                    string stdsize = "";
                    string unit = "";
                    string kumak = "";
                    string pricd = "";
                    string ipamt = "";

                    if (GetIteminfo(e.Value as string, buydt, out iteminfo,out prodcm,out stdsize,out unit,out kumak,out pricd,out ipamt) > 0)
                    {
                        list[row].ITEMINFO = iteminfo;
                        list[row].PRODCM = prodcm;
                        list[row].STDSIZE = stdsize;
                        list[row].UNIT = unit;
                        list[row].KUMAK = kumak;
                        list[row].PRICD = pricd;
                        list[row].IPAMT = ipamt;
                        //grdMainView.FocusedColumn = grdMainView.Columns["BUYQTY"];
                    }
                    else
                    {
                        list[row].ITEMINFO = "";
                        // 코드가 없다. 검색하자.
                        ADD0704E_2 f = new ADD0704E_2();
                        f.in_pcode = e.Value as string;
                        f.in_buydt = buydt;
                        f.ShowDialog(this);
                        if (f.out_yn == "Y")
                        {
                            // EDI코드만 반환하고 추가 정보는 다시 검색한다.
                            e.Value = f.out_pcode;
                            list[row].ITEMINFO = f.out_iteminfo;
                            list[row].PRODCM = f.out_prodcm;
                            list[row].STDSIZE = f.out_stdsize;
                            list[row].UNIT = f.out_unit;
                            list[row].KUMAK = f.out_kumak;
                            list[row].PRICD = f.out_pricd;
                            list[row].IPAMT = f.out_ipamt;
                            grdMainView.FocusedColumn = grdMainView.Columns["BUYQTY"];
                        }
                        f = null;
                    }

                    this.RefreshGridMain();
                }
                catch (Exception ex)
                {
                    this.CloseProgressForm("", "조회 중입니다.");
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
            else if (view.FocusedColumn.FieldName == "BUYQTY")
            {
                // 구입량
                List<CData> list = (List<CData>)grdMain.DataSource;
                if (list == null) return;
                long buyqty = MetroLib.StrHelper.ToLong(e.Value as string);
                long buytotamt = MetroLib.StrHelper.ToLong(view.GetRowCellValue(view.FocusedRowHandle, "BUYTOTAMT").ToString());
                // 구입단가 계산
                long buyamt = 0;
                if (buyqty != 0) buyamt = (long)((double)buytotamt / (double)buyqty + 0.5);
                list[view.FocusedRowHandle].BUYAMT = buyamt;
                this.RefreshGridMain();
            }
            else if (view.FocusedColumn.FieldName == "BUYTOTAMT")
            {
                //  구입액
                List<CData> list = (List<CData>)grdMain.DataSource;
                if (list == null) return;
                long buyqty = MetroLib.StrHelper.ToLong(view.GetRowCellValue(view.FocusedRowHandle, "BUYQTY").ToString());
                long buytotamt = MetroLib.StrHelper.ToLong(e.Value as string);
                // 구임단가 계산
                long buyamt = 0;
                if (buyqty != 0) buyamt = (long)((double)buytotamt / (double)buyqty + 0.5);
                list[view.FocusedRowHandle].BUYAMT = buyamt;
                this.RefreshGridMain();
            }
            else if (view.FocusedColumn.FieldName == "BUSSCD")
            {
                // 구입기관번호
                List<CData> list = (List<CData>)grdMain.DataSource;
                if (list == null) return;
                try
                {
                    //string bussnm = GetBussnm(e.Value as string); // VB에서 코딩 실수로 막는 것이 맞음.
                    //if (bussnm != "")
                    //{
                    //    list[view.FocusedRowHandle].BUSSNM = bussnm;
                    //}
                    //else
                    //{
                        string out_busscd = "";
                        string out_bussnm = "";
                        int busscd_cnt = ReadBussnm(e.Value as string, out out_busscd, out out_bussnm);
                        if (busscd_cnt == 1)
                        {
                            e.Value = out_busscd;
                            list[view.FocusedRowHandle].BUSSNM = out_bussnm;
                        }
                        else if (busscd_cnt > 1)
                        {
                            // 못찾았음. 검색하자.
                            ADD0704E_1 f = new ADD0704E_1();
                            f.in_busscd = e.Value as string;
                            f.ShowDialog(this);
                            if (f.out_yn == "Y")
                            {
                                e.Value = f.out_busscd;
                                list[view.FocusedRowHandle].BUSSNM = f.out_bussnm;
                            }
                            f = null;
                        }
                        else
                        {
                            MessageBox.Show("등록된 사업장 코드가 없습니다.");
                            e.Value = "";
                            list[view.FocusedRowHandle].BUSSNM = "";
                        }
                    //}
                    this.RefreshGridMain();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int GetIteminfo(string itemcd, string buydt,out string p_iteminfo, out string p_prodcm, out string p_stdsize, out string p_unit, out string p_kumak, out string p_pricd, out string p_ipamt)
        {
            int ti09_count = 0;

            string iteminfo = "";
            string prodcm = "";
            string stdsize = "";
            string unit = "";
            string kumak = "";
            string pricd = "";
            string ipamt = "";


            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT TOP 1 I09.PCODENM";
                sql += System.Environment.NewLine + "     , I09.MKCNM";
                sql += System.Environment.NewLine + "     , I09.PTYPE";
                sql += System.Environment.NewLine + "     , I09.PDUT";
                sql += System.Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN I09.KUMAK2 ELSE I09.KUMAK1 END AS KUMAK";
                sql += System.Environment.NewLine + "     , I09.ADTDT";
                sql += System.Environment.NewLine + "     , ( SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                sql += System.Environment.NewLine + "     , ( SELECT TOP 1 A02.IPAMT FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS IPAMT";
                sql += System.Environment.NewLine + "  FROM TI09 I09";
                sql += System.Environment.NewLine + " WHERE I09.PCODE  = '" + itemcd + "'";
                sql += System.Environment.NewLine + "   AND I09.ADTDT <= '" + buydt + "'";
                sql += System.Environment.NewLine + "   AND I09.GUBUN = '3'";
                sql += System.Environment.NewLine + " ORDER BY I09.ADTDT DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    iteminfo = reader["PCODENM"].ToString();
                    prodcm = reader["MKCNM"].ToString();
                    stdsize = reader["PTYPE"].ToString();
                    unit = reader["PDUT"].ToString();
                    kumak = reader["KUMAK"].ToString();
                    pricd = reader["PRICD"].ToString();
                    ipamt = reader["IPAMT"].ToString();

                    ti09_count++;
                    return true;
                });
                if (ti09_count < 1)
                {

                    // TI09 다시 읽기
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT TOP 1 I09.PCODENM";
                    sql += System.Environment.NewLine + "     , I09.MKCNM";
                    sql += System.Environment.NewLine + "     , I09.PTYPE";
                    sql += System.Environment.NewLine + "     , I09.PDUT";
                    sql += System.Environment.NewLine + "     , CASE WHEN DBO.MFS_ADD_GET_HOSPITALJONG('" + buydt + "')=4 THEN I09.KUMAK2 ELSE I09.KUMAK1 END AS KUMAK";
                    sql += System.Environment.NewLine + "     , I09.ADTDT";
                    sql += System.Environment.NewLine + "     , ( SELECT TOP 1 A02.PRICD FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS PRICD";
                    sql += System.Environment.NewLine + "     , ( SELECT TOP 1 A02.IPAMT FROM TA02 A02 WHERE A02.ISPCD=I09.PCODE AND ISNULL(A02.EXPDT,'') ='' AND ISNULL(A02.REFFG,'') NOT IN ('T','K','P') ORDER BY A02.CREDT DESC) AS IPAMT";
                    sql += System.Environment.NewLine + "  FROM TI09 I09";
                    sql += System.Environment.NewLine + " WHERE I09.PCODE  = '" + itemcd + "'";
                    sql += System.Environment.NewLine + "   AND I09.ADTDT  > '" + buydt + "'";
                    sql += System.Environment.NewLine + "   AND I09.GUBUN = '3'";
                    sql += System.Environment.NewLine + " ORDER BY I09.ADTDT";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        iteminfo = reader["PCODENM"].ToString();
                        prodcm = reader["MKCNM"].ToString();
                        stdsize = reader["PTYPE"].ToString();
                        unit = reader["PDUT"].ToString();
                        kumak = reader["KUMAK"].ToString();
                        pricd = reader["PRICD"].ToString();
                        ipamt = reader["IPAMT"].ToString();

                        ti09_count++;
                        return true;
                    });
                }
            }

            p_iteminfo = iteminfo;
            p_prodcm = prodcm;
            p_stdsize = stdsize;
            p_unit = unit;
            p_kumak = kumak;
            p_pricd = pricd;
            p_ipamt = ipamt;

            return ti09_count;
        }

        private string GetBussnm(string busscd)
        {
            string bussnm = "";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT, CDNM, FLD1QTY ";
                sql += System.Environment.NewLine + "  FROM TI88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD='BUSINESSCD_PHA' ";
                sql += System.Environment.NewLine + "   AND CDNM='" + busscd + "' ";
                sql += System.Environment.NewLine + " GROUP BY CDNM, FLD1QTY ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    bussnm = reader["FLD1QTY"].ToString();
                    return MetroLib.SqlHelper.CONTINUE;
                });
            }
            return bussnm;
        }

        private int ReadBussnm(string busscd, out string p_busscd, out string p_bussnm)
        {
            int cnt = 0;
            string out_busscd = "";
            string out_bussnm = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT, CDNM, FLD1QTY ";
                sql += System.Environment.NewLine + "  FROM TI88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD='BUSINESSCD_PHA' ";
                sql += System.Environment.NewLine + "   AND CDNM LIKE '" + busscd + "%' ";
                sql += System.Environment.NewLine + " GROUP BY CDNM, FLD1QTY ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    out_busscd = reader["CDNM"].ToString();
                    out_bussnm = reader["FLD1QTY"].ToString();
                    return MetroLib.SqlHelper.CONTINUE;
                });
            }
            p_busscd = out_busscd;
            p_bussnm = out_bussnm;
            return cnt;
        }

        private void btnOtherId_Click(object sender, EventArgs e)
        {
            ADD0704E_1 f = new ADD0704E_1();
            f.ShowDialog(this);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int currentRow = 0;
            string find_itemcd = txtItemcd.Text.ToString();
            
            if(find_itemcd=="") return;
            
            currentRow = grdMainView.FocusedRowHandle;
            if(currentRow<0) return;

            for (int i = currentRow + 1; i < grdMainView.DataRowCount; i++)
            {
                string itemcd = grdMainView.GetRowCellValue(i, "ITEMCD").ToString();
                if (find_itemcd == itemcd.Substring(0, find_itemcd.Length))
                {
                    grdMainView.FocusedRowHandle = i;
                    return;
                }
            }
            for (int i = 0; i < currentRow ; i++)
            {
                string itemcd = grdMainView.GetRowCellValue(i, "ITEMCD").ToString();
                if (find_itemcd == itemcd.Substring(0, find_itemcd.Length))
                {
                    grdMainView.FocusedRowHandle = i;
                    return;
                }
            }
            MessageBox.Show("찾는 자료가 없습니다.");
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("구입일자순 정렬", new EventHandler(mnuSortBuydt_Click));
            //cm.MenuItems.Add("코드순 정렬", new EventHandler(mnuSortItemcd_Click));
            //cm.Show(btnSort, Cursor.Position);
        }

        private void btnSort_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("구입일자순 정렬", new EventHandler(mnuSortBuydt_Click));
                cm.MenuItems.Add("코드순 정렬", new EventHandler(mnuSortItemcd_Click));
                cm.MenuItems.Add("품명순 정렬", new EventHandler(mnuSortIteminfo_Click));
                cm.MenuItems.Add("최초구입순 정렬", new EventHandler(mnuSortFstbuyfg_Click));
                cm.MenuItems.Add("제약회사순 정렬", new EventHandler(mnuSortProdcm_Click));
                cm.MenuItems.Add("사업자번호순 정렬", new EventHandler(mnuSortBusscd_Click));
                cm.MenuItems.Add("입력일자순 정렬", new EventHandler(mnuSortEntdt_Click));
                cm.MenuItems.Add("수정일자순 정렬", new EventHandler(mnuSortUpddt_Click));
                cm.Show(btnSort, e.Location);
            }
        }

        private void mnuSortBuydt_Click(object sender, EventArgs e)
        {
            // 구입일자순 : 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortItemcd_Click(object sender, EventArgs e)
        {
            // 코드순 : 코드 + 구입일자
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortIteminfo_Click(object sender, EventArgs e)
        {
            // 품명순 : 품명 + 코드 + 구입일자
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.ITEMINFO.CompareTo(y.ITEMINFO);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortFstbuyfg_Click(object sender, EventArgs e)
        {
            // 최초구입순 : 최초구입 + 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                string xFstbuyfg = x.FSTBUYFG;
                string yFstbuyfg = y.FSTBUYFG;
                if (xFstbuyfg == "") xFstbuyfg = "Z"; // 공백을 뒤로 보내기 위해
                if (yFstbuyfg == "") yFstbuyfg = "Z"; // 공백을 뒤로 보내기 위해

                int ret = 0;
                ret = xFstbuyfg.CompareTo(yFstbuyfg);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortProdcm_Click(object sender, EventArgs e)
        {
            // 제약회사순 : 제약회사 + 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.PRODCM.CompareTo(y.PRODCM);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortBusscd_Click(object sender, EventArgs e)
        {
            // 사업자번호순 : 사업자번호 + 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.BUSSCD.CompareTo(y.BUSSCD);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortEntdt_Click(object sender, EventArgs e)
        {
            // 입력일자순 : 입력일자 + 입력시간 + 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.ENTDT.CompareTo(y.ENTDT);
                if (ret != 0) return ret;
                ret = x.ENTTM.CompareTo(y.ENTTM);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void mnuSortUpddt_Click(object sender, EventArgs e)
        {
            // 수정일자순 : 수정일자 + 수정시간 + 구입일자 + 코드
            List<CData> list = (List<CData>)grdMain.DataSource;
            if (list == null) return;
            list.Sort((x, y) =>
            {
                int ret = 0;
                ret = x.UPDDT.CompareTo(y.UPDDT);
                if (ret != 0) return ret;
                ret = x.UPDTM.CompareTo(y.UPDTM);
                if (ret != 0) return ret;
                ret = x.BUYDT.CompareTo(y.BUYDT);
                if (ret != 0) return ret;
                ret = x.ITEMCD.CompareTo(y.ITEMCD);
                return ret;
            });
            RefreshGridMain();
        }

        private void grdMainView_HiddenEditor(object sender, EventArgs e)
        {
            isEditing = false;

            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (view.FocusedRowHandle < 0) return;
            if (view.FocusedColumn.FieldName == "BUYDT")
            {
                string buydt = view.GetRowCellValue(view.FocusedRowHandle, "BUYDT").ToString();
                if (MetroLib.Util.ValDt(buydt) == true) view.FocusedColumn = view.Columns["ITEMCD"];
            }
            else if (view.FocusedColumn.FieldName == "ITEMCD")
            {
                string itemcd = view.GetRowCellValue(view.FocusedRowHandle, "ITEMCD").ToString();
                string iteminfo = view.GetRowCellValue(view.FocusedRowHandle, "ITEMINFO").ToString();
                if (itemcd != "" && iteminfo != "") view.FocusedColumn = view.Columns["BUYQTY"];
            }
            else if (view.FocusedColumn.FieldName == "BUYQTY")
            {
                string buyqty = view.GetRowCellValue(view.FocusedRowHandle, "BUYQTY").ToString();
                if (buyqty != "" && buyqty != "0") view.FocusedColumn = view.Columns["BUYTOTAMT"];
            }
            else if (view.FocusedColumn.FieldName == "BUYTOTAMT")
            {
                string buytotamt = view.GetRowCellValue(view.FocusedRowHandle, "BUYTOTAMT").ToString();
                if (buytotamt != "" && buytotamt != "0") view.FocusedColumn = view.Columns["BUSSCD"];
            }
            else if (view.FocusedColumn.FieldName == "BUSSCD")
            {
                // 구입기관상호 컬럼 에서는 최초 컬럼으로 이동
                string busscd = view.GetRowCellValue(view.FocusedRowHandle, "BUSSCD").ToString();
                string bussnm = view.GetRowCellValue(view.FocusedRowHandle, "BUSSNM").ToString();
                if (busscd != "" && bussnm != "") view.FocusedColumn = view.Columns["FSTBUYFG"];
            }
            else if (view.FocusedColumn.FieldName == "FSTBUYFG")
            {
                // 최초 컬럼에서는
                if (view.FocusedRowHandle == view.RowCount - 1)
                {
                    // 마지막 줄이면
                    btnAddRow.PerformClick();
                    grdMainView.UpdateCurrentRow();
                }
            }
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 커서가 있는 셀의 배경색 변경
            // 이렇게 색을 주면 선택된 셀의 색이 사라짐.
            /*
            if (e.RowHandle == grdMainView.FocusedRowHandle && e.Column.FieldName == grdMainView.FocusedColumn.FieldName)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
            */

            // 단가와 EDI단가가 다르면 글자색을 빨간색으로 변경
            if (e.Column.FieldName == "BUYAMT")
            {
                long buyamt = MetroLib.StrHelper.ToLong(grdMainView.GetRowCellValue(e.RowHandle, "BUYAMT").ToString());
                long kumak = MetroLib.StrHelper.ToLong(grdMainView.GetRowCellValue(e.RowHandle, "KUMAK").ToString());
                if (buyamt != kumak)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private DevExpress.XtraGrid.Columns.GridColumn GetColumn(int visibleIndex)
        {
            for (int i = 0; i < grdMainView.Columns.Count; i++)
            {
                if (grdMainView.Columns[i].VisibleIndex == visibleIndex)
                {
                    return grdMainView.Columns[i];
                }
            }
            return null;
        }

        private bool isEditing = false;
        private bool ctrlV = false;
        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.V && e.Control == true)
                {
                    if (isEditing) return;

                    ctrlV = true;

                    string paste = Clipboard.GetText();
                    if (paste.Contains("\r\n") == false && paste.Contains("\t") == false)
                    {
                        int rowHandle = grdMainView.FocusedRowHandle;
                        string colName = grdMainView.FocusedColumn.FieldName;
                        grdMainView.SetRowCellValue(rowHandle, colName, paste);
                    }
                    else
                    {
                        string[] seperator = { "\r\n" };
                        string[] arr_row = paste.Split(seperator, StringSplitOptions.None);

                        int rowHandle = grdMainView.FocusedRowHandle;
                        int colHandle = grdMainView.FocusedColumn.VisibleIndex;
                        for (int i = 0; i < arr_row.Length - 1; i++)
                        {
                            if (arr_row[i].Contains("\t") == false)
                            {
                                DevExpress.XtraGrid.Columns.GridColumn col = GetColumn(colHandle);
                                grdMainView.SetRowCellValue(rowHandle + i, col, arr_row[i]);
                            }
                            else
                            {
                                string[] col_seperator = { "\t" };
                                string[] arr_col = arr_row[i].Split(col_seperator, StringSplitOptions.None);
                                for (int j = 0; j < arr_col.Length; j++)
                                {
                                    DevExpress.XtraGrid.Columns.GridColumn col = GetColumn(colHandle + j);
                                    grdMainView.SetRowCellValue(rowHandle + i, col, arr_col[j]);
                                }
                            }
                        }
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdMainView_MouseDown(object sender, MouseEventArgs e)
        {
            // 그리드의 맨 왼쪽 상단을 클릭하여 모든 셀을 선택하는 기능
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton)
            {
                view.SelectAll();
            }
        }

        private void grdMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (ctrlV)
            {
                ctrlV = false;
                e.Cancel = true;
                return;
            }
        }

        private void grdMainView_ShownEditor(object sender, EventArgs e)
        {
            isEditing = true;
        }
    }
}

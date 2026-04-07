using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9004E
{
    public partial class ADD9004E : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD9004E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9004E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9004E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD9004E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            Application.DoEvents();
            btnQuery.PerformClick();
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
            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT MST3CD";
                sql += System.Environment.NewLine + "     , FLD1QTY";
                sql += System.Environment.NewLine + "     , CDNM";
                sql += System.Environment.NewLine + "     , CASE ISNULL(FLD3QTY,'') WHEN '1' THEN '외래' WHEN '2' THEN '입원' ELSE '공통' END AS IOFG";
                sql += System.Environment.NewLine + "     , FLD2QTY ";
                sql += System.Environment.NewLine + "  FROM TA88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                sql += System.Environment.NewLine + " ORDER BY FLD2QTY,MST3CD ";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.CODE = reader["MST3CD"].ToString();
                        data.DESC = reader["CDNM"].ToString();
                        data.EMPID = reader["FLD1QTY"].ToString();
                        data.IOFG = reader["IOFG"].ToString();
                        data.SORTNO = reader["FLD2QTY"].ToString();
                        data.READONLY = true;

                        list.Add(data);
                    }
                    reader.Close();
                }
                conn.Close();

                //AddNewRow();
            }
            this.RefreshGridMain();
        }

        //private void AddNewRow()
        //{
        //    List<CData> list = (List<CData>)grdMainView.DataSource;

        //    int maxsortno = 0;
        //    foreach (CData data in list)
        //    {
        //        int sortno = 0;
        //        int.TryParse(data.SORTNO, out sortno);
        //        if (sortno > maxsortno) maxsortno = sortno;
        //    }
        //    maxsortno++;
        //    if (maxsortno <= 1000000) maxsortno += maxsortno;

        //    CData ndata = new CData();
        //    ndata.Clear();
        //    ndata.SORTNO = maxsortno.ToString();
        //    list.Add(ndata);
        //}

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

        private void grdMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            //if (grdMainView.FocusedColumn == gcCODE)
            //{
            //    bool read_only = (bool)grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcREADONLY);
            //    if (read_only == true) e.Cancel = true;
            //}
        }

        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //bool read_only = (bool)grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcREADONLY);
            //string code = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCODE).ToString();
            //string desc = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDESC).ToString();
            //string iofg = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcIOFG).ToString();
            //string sortno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcSORTNO).ToString(); ;

            //if (grdMainView.FocusedColumn == gcCODE)
            //{
            //    code = e.Value.ToString();
            //}
            //else if (grdMainView.FocusedColumn == gcDESC)
            //{
            //    desc = e.Value.ToString();
            //}
            //else if (grdMainView.FocusedColumn == gcIOFG)
            //{
            //    iofg = e.Value.ToString();
            //}

            //// 입력내용이 없으면 종료
            //if (code == "" || desc == "") return;

            //// 코드가 중복되면 종료
            //for (int i = 0; i < grdMainView.RowCount; i++)
            //{
            //    if (code == grdMainView.GetRowCellValue(i, gcCODE).ToString() && i != grdMainView.FocusedRowHandle)
            //    {
            //        MessageBox.Show("동일한 코드가 존재합니다.");
            //        return;
            //    }
            //}

            //try
            //{
            //    // 저장
            //    SaveTEXT(code, desc, iofg, sortno);

            //    // 마지막 줄이면 한 줄 추가
            //    if (grdMainView.FocusedRowHandle == grdMainView.RowCount - 1)
            //    {
            //        AddNewRow();
            //    }
            //    RefreshGridMain();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void SaveTEXT(string p_code, string p_desc, string p_iofg, string p_sortno)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
                sql += System.Environment.NewLine + "   FROM TA88";
                sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                sql += System.Environment.NewLine + "   AND MST3CD=?";

                int cnt = 0;

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", p_code));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int.TryParse(reader["CNT"].ToString(), out cnt);
                    }
                    reader.Close();
                }

                string fld3qty = "0";
                if (p_iofg == "외래")
                {
                    fld3qty = "1";
                }
                else if (p_iofg == "입원")
                {
                    fld3qty = "2";
                }
                     
                if (cnt < 1)
                {
                    // INSERT
                    sql = "";
                    sql += System.Environment.NewLine + "INSERT TA88(MST1CD,MST2CD,MST3CD,CDNM,FLD1QTY,FLD2QTY,FLD3QTY)";
                    sql += System.Environment.NewLine + "VALUES('A','TEXT',?,?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", p_code)); // 코드
                        cmd.Parameters.Add(new OleDbParameter("@2", p_desc)); // 내용
                        cmd.Parameters.Add(new OleDbParameter("@3", m_User)); // 등록자
                        cmd.Parameters.Add(new OleDbParameter("@4", p_sortno)); // 정렬순서(사용안함)
                        cmd.Parameters.Add(new OleDbParameter("@5", fld3qty)); // 입원외래

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // UPDATE
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TA88";
                    sql += System.Environment.NewLine + "   SET CDNM=?";
                    sql += System.Environment.NewLine + "     , FLD1QTY=?";
                    sql += System.Environment.NewLine + "     , FLD2QTY=?";
                    sql += System.Environment.NewLine + "     , FLD3QTY=?";
                    sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                    sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                    sql += System.Environment.NewLine + "   AND MST3CD=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", p_desc)); // 내용
                        cmd.Parameters.Add(new OleDbParameter("@2", m_User)); // 등록자
                        cmd.Parameters.Add(new OleDbParameter("@3", p_sortno)); // 정렬순서(사용안함)
                        cmd.Parameters.Add(new OleDbParameter("@4", fld3qty)); // 입원외래
                        cmd.Parameters.Add(new OleDbParameter("@5", p_code)); // 코드

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.DelLine();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void DelLine()
        {
            string code = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCODE).ToString();
            string desc = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDESC).ToString();
            string msg = "";
            msg += "코드 : " + code + Environment.NewLine;
            msg += "내용 : " + desc + Environment.NewLine + Environment.NewLine;
            if (MessageBox.Show(msg + " 를 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo) == DialogResult.No) return;

            DelTEXT(code);
            grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
        }

        private void DelTEXT(string p_code)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "DELETE FROM TA88";
                sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                sql += System.Environment.NewLine + "   AND MST3CD=?";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_code));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.RowCount < 2) return; // 조회된 건이 없으면 종료
                if (grdMainView.FocusedRowHandle == 0) return; // 맨 윗줄이면 종료
                if (grdMainView.FocusedRowHandle == grdMainView.RowCount - 1) return; // 맨 아래줄이면 종료

                SwapRow(grdMainView.FocusedRowHandle - 1, grdMainView.FocusedRowHandle);
                grdMainView.FocusedRowHandle--;
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDn_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.RowCount < 2) return; // 조회된 건이 없으면 종료
                if (grdMainView.FocusedRowHandle == grdMainView.RowCount - 1) return; // 맨 아래줄이면 종료
                if (grdMainView.FocusedRowHandle == grdMainView.RowCount - 2) return; // 맨 아래줄 바로 윗줄이면 종료

                SwapRow(grdMainView.FocusedRowHandle, grdMainView.FocusedRowHandle + 1);
                grdMainView.FocusedRowHandle++;
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SwapRow(int row_u, int row_d)
        {
            string code_u = grdMainView.GetRowCellValue(row_u, gcCODE).ToString();
            string desc_u = grdMainView.GetRowCellValue(row_u, gcDESC).ToString();
            string empid_u = grdMainView.GetRowCellValue(row_u, gcEMPID).ToString();
            string iofg_u = grdMainView.GetRowCellValue(row_u, gcIOFG).ToString();

            string code_d = grdMainView.GetRowCellValue(row_d, gcCODE).ToString();
            string desc_d = grdMainView.GetRowCellValue(row_d, gcDESC).ToString();
            string empid_d = grdMainView.GetRowCellValue(row_d, gcEMPID).ToString();
            string iofg_d = grdMainView.GetRowCellValue(row_d, gcIOFG).ToString();

            grdMainView.SetRowCellValue(row_u, gcCODE, code_d);
            grdMainView.SetRowCellValue(row_u, gcDESC, desc_d);
            grdMainView.SetRowCellValue(row_u, gcEMPID, empid_d);
            grdMainView.SetRowCellValue(row_u, gcIOFG, iofg_d);

            grdMainView.SetRowCellValue(row_d, gcCODE, code_u);
            grdMainView.SetRowCellValue(row_d, gcDESC, desc_u);
            grdMainView.SetRowCellValue(row_d, gcEMPID, empid_u);
            grdMainView.SetRowCellValue(row_d, gcIOFG, iofg_u);

            // 정렬순서를 다시 부여한다.
            int sortno = 1000000;
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "UPDATE TA88";
                sql += System.Environment.NewLine + "   SET FLD2QTY=?";
                sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                sql += System.Environment.NewLine + "   AND MST3CD=?";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    for (int i = 0; i < grdMainView.RowCount - 1; i++)
                    {
                        string code = grdMainView.GetRowCellValue(i, gcCODE).ToString();

                        sortno++;

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", sortno)); // 정렬순서
                        cmd.Parameters.Add(new OleDbParameter("@2", code)); // 코드

                        cmd.ExecuteNonQuery();

                        grdMainView.SetRowCellValue(i, gcSORTNO, sortno);
                    }
                }

                conn.Close();
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
            printableComponentLink.Component = grdMain;
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("심사TEXT", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            //e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            //e.Graph.DrawString("퇴원월:" + txtYYMM.Text.ToString(), Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string prtDate = "";
            string prtTime = "";
            try
            {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                prtDate = MetroLib.Util.GetSysDate(conn);
                prtTime = MetroLib.Util.GetSysTime(conn);

                conn.Close();
            }
            }
            catch (Exception ex)
            {
                prtDate = DateTime.Now.ToString("yyyyMMdd");
                prtDate = DateTime.Now.ToString("HHmmss");
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD9004E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(prtDate + " " + prtTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

            // 저장했으면 다시 조회
            btnQuery.PerformClick();
        }

        private void Save()
        {
            string code = txtCode.Text.ToString();
            string desc = txtDesc.Text.ToString();
            string fld3qty = "0";
            string sortno = "";
            if (rbOut.Checked) fld3qty = "1";
            else if (rbIn.Checked) fld3qty = "2";

            if (code == "") return;
            if (desc == "") return;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
                sql += System.Environment.NewLine + "   FROM TA88";
                sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                sql += System.Environment.NewLine + "   AND MST3CD=?";

                int cnt = 0;

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", code));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int.TryParse(reader["CNT"].ToString(), out cnt);
                    }
                    reader.Close();
                }


                if (cnt < 1)
                {
                    // INSERT
                    sql = "";
                    sql += System.Environment.NewLine + "INSERT TA88(MST1CD,MST2CD,MST3CD,CDNM,FLD1QTY,FLD2QTY,FLD3QTY)";
                    sql += System.Environment.NewLine + "VALUES('A','TEXT',?,?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", code)); // 코드
                        cmd.Parameters.Add(new OleDbParameter("@2", desc)); // 내용
                        cmd.Parameters.Add(new OleDbParameter("@3", m_User)); // 등록자
                        cmd.Parameters.Add(new OleDbParameter("@4", sortno)); // 정렬순서(사용안함)
                        cmd.Parameters.Add(new OleDbParameter("@5", fld3qty)); // 입원외래

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // UPDATE
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TA88";
                    sql += System.Environment.NewLine + "   SET CDNM=?";
                    sql += System.Environment.NewLine + "     , FLD1QTY=?";
                    sql += System.Environment.NewLine + "     , FLD2QTY=?";
                    sql += System.Environment.NewLine + "     , FLD3QTY=?";
                    sql += System.Environment.NewLine + " WHERE MST1CD='A'";
                    sql += System.Environment.NewLine + "   AND MST2CD='TEXT'";
                    sql += System.Environment.NewLine + "   AND MST3CD=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", desc)); // 내용
                        cmd.Parameters.Add(new OleDbParameter("@2", m_User)); // 등록자
                        cmd.Parameters.Add(new OleDbParameter("@3", sortno)); // 정렬순서(사용안함)
                        cmd.Parameters.Add(new OleDbParameter("@4", fld3qty)); // 입원외래
                        cmd.Parameters.Add(new OleDbParameter("@5", code)); // 코드

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }

        private void grdMainView_Click(object sender, EventArgs e)
        {
            string code = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCODE).ToString();
            string desc = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDESC).ToString();
            string iofg = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcIOFG).ToString();

            txtCode.Text = code;
            txtDesc.Text = desc;
            if (iofg == "공통") rbAll.Checked = true;
            else if (iofg == "외래") rbOut.Checked = true;
            else if (iofg == "입원") rbIn.Checked = true;
        }
    }
}

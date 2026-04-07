using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9907E
{
    public partial class ADD9907E : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD9907E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9907E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9907E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD9907E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

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

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT DACD,DANM,FRAGE,TOAGE ";
            sql += System.Environment.NewLine + "  FROM TI04";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                CData data = null;

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        data = new CData();
                        data.Clear();
                        data.DACD = reader["DACD"].ToString();
                        data.DANM = reader["DANM"].ToString();
                        data.FRAGE = reader["FRAGE"].ToString();
                        data.TOAGE = reader["TOAGE"].ToString();
                        data.READONLY = true;

                        list.Add(data);
                    }
                    reader.Close();
                }
                conn.Close();

                // 조회된 자료는 상병컬럼 수정불가
                // ShowingEditor 이벤트에서 처리

                // 빈줄 추가
                data = new CData();
                data.Clear();
                list.Add(data);
            }
            this.RefreshGridMain();

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

        private void grdMainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (view.FocusedColumn == gcDACD)
            {
                string dacd = e.Value.ToString().ToUpper();
                if (dacd == "") return;

                e.Value = dacd;

                if (DupCheck(dacd) == true)
                {
                    MessageBox.Show("이미 등록된 상병입니다.");
                    return;
                }

                // 상병코드 입력 후
                string danm = GetDanm(dacd);
                if (danm != "")
                {
                    // 상병이 있으면
                    view.SetRowCellValue(view.FocusedRowHandle, gcDANM, danm);
                    // 저장
                    string frage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE).ToString();
                    string toage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcTOAGE).ToString();
                    if (frage == "")
                    {
                        frage = "0";
                        grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE, frage);
                    }
                    if (toage == "")
                    {
                        toage = "999";
                        grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcTOAGE, toage);
                    }
                    SaveTI04(dacd, danm, frage, toage);
                    // 마지막 줄이면 한 줄 추가
                    if (view.FocusedRowHandle == view.RowCount-1)
                    {
                        CData data = new CData();
                        data.Clear();
                        ((List<CData>)grdMainView.DataSource).Add(data);
                    }
                }
                else
                {
                    // 상병이 없으면 검색창을 띄운다.
                    ADD9907E_1 f = new ADD9907E_1();
                    f.m_InDacd = dacd;
                    f.ShowDialog(this);
                    dacd = f.m_OutDacd;
                    f = null;
                    // 팝업창에서 상병코드를 읽어옴.
                    if (dacd == "") return;

                    if (DupCheck(dacd) == true)
                    {
                        MessageBox.Show("이미 등록된 상병입니다.");
                        return;
                    }

                    danm = GetDanm(dacd);
                    view.SetRowCellValue(view.FocusedRowHandle, gcDANM, danm);
                    // 저장
                    string frage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE).ToString();
                    string toage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcTOAGE).ToString();
                    if (frage == "")
                    {
                        frage = "0";
                        grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE, frage);
                    }
                    if (toage == "")
                    {
                        toage = "999";
                        grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcTOAGE, toage);
                    }
                    SaveTI04(dacd, danm, frage, toage);
                    // 마지막 줄이면 한 줄 추가
                    if (view.FocusedRowHandle == view.RowCount - 1)
                    {
                        CData data = new CData();
                        data.Clear();
                        ((List<CData>)grdMainView.DataSource).Add(data);
                    }
                }
                RefreshGridMain();
            }
            else if (view.FocusedColumn == gcFRAGE)
            {
                // 시작나이 입력후
                // 저장
                string dacd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDACD).ToString();
                string danm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDANM).ToString();
                string frage = e.Value.ToString();
                string toage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcTOAGE).ToString();
                if (toage == "")
                {
                    toage = "999";
                    grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE, toage);
                }
                SaveTI04(dacd, danm, frage, toage);
                // 마지막 줄이면 한 줄 추가
                if (view.FocusedRowHandle == view.RowCount - 1)
                {
                    CData data = new CData();
                    data.Clear();
                    ((List<CData>)grdMainView.DataSource).Add(data);
                }
            }
            else if (view.FocusedColumn == gcTOAGE)
            {
                // 종료나이 입력후
                // 저장
                string dacd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDACD).ToString();
                string danm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDANM).ToString();
                string frage = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE).ToString();
                string toage = e.Value.ToString();
                if (frage == "")
                {
                    frage = "0";
                    grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcFRAGE, frage);
                }
                SaveTI04(dacd, danm, frage, toage);
                // 마지막 줄이면 한 줄 추가
                if (view.FocusedRowHandle == view.RowCount - 1)
                {
                    CData data = new CData();
                    data.Clear();
                    ((List<CData>)grdMainView.DataSource).Add(data);
                }
            }
        }

        private bool DupCheck(string p_dacd)
        {
            List<CData> list = (List<CData>)grdMainView.DataSource;
            foreach (CData data in list)
            {
                if (data.DACD == p_dacd)
                {
                    return true;
                }
            }
            return false;
        }

        private string GetDanm(string p_dacd)
        {
            string ret = "";
            string disediv="2";
            string exdt = "20121001";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string z16fg = "";

                string sql = "";
                sql = "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='24'";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        z16fg = reader["FLD2QTY"].ToString();
                    }
                    reader.Close();
                }

                if (z16fg == "1")
                {
                    // TZ16 사용
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT KORNM ";
                    sql += System.Environment.NewLine + "  FROM TZ16A Z16A (NOLOCK) ";
                    sql += System.Environment.NewLine + " WHERE Z16A.DACD = ? ";
                    sql += System.Environment.NewLine + "   AND ISNULL(Z16A.USEFG,'') <> 'N'";  // 불완전코드제외
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(Z16A.CREDT,'')='' THEN '19990101' ELSE Z16A.CREDT END <= ?";
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(Z16A.EXPDT,'')='' THEN '99991231' ELSE Z16A.EXPDT END >= ?";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_dacd));
                        cmd.Parameters.Add(new OleDbParameter("@2", exdt));
                        cmd.Parameters.Add(new OleDbParameter("@3", exdt));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["KORNM"].ToString();
                        }
                        reader.Close();
                    }
                }
                else
                {
                    // TA16 사용
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT DISENEWKOR AS KORNM";
                    sql += System.Environment.NewLine + "  FROM TA16 (NOLOCK) ";
                    sql += System.Environment.NewLine + " WHERE ZCD10CD = ? ";
                    sql += System.Environment.NewLine + "   AND DISECD  = ? ";
                    sql += System.Environment.NewLine + "   AND DISEDIV = ? ";
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(EXPDT,'')='' THEN '99991231' ELSE EXPDT END >= ?";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_dacd));
                        cmd.Parameters.Add(new OleDbParameter("@2", p_dacd));
                        cmd.Parameters.Add(new OleDbParameter("@3", disediv));
                        cmd.Parameters.Add(new OleDbParameter("@4", exdt));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["KORNM"].ToString();
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            return ret;
        }

        private void SaveTI04(string p_dacd, string p_danm, string p_frage, string p_toage)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT COUNT(*) CNT";
                sql += System.Environment.NewLine + "  FROM TI04";
                sql += System.Environment.NewLine + " WHERE DACD=?";

                int cnt = 0;

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_dacd));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int.TryParse(reader["CNT"].ToString(), out cnt);
                    }
                    reader.Close();
                }

                int frage = 0;
                int toage = 0;
                int.TryParse(p_frage, out frage);
                int.TryParse(p_toage, out toage);
                if (toage == 0) toage = 999;

                if (cnt < 1)
                {
                    // INSERT
                    sql = "";
                    sql += System.Environment.NewLine + "INSERT TI04(DACD,DANM,FRAGE,TOAGE)";
                    sql += System.Environment.NewLine + "VALUES(?,?,?,?)";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_dacd));
                        cmd.Parameters.Add(new OleDbParameter("@2", p_danm));
                        cmd.Parameters.Add(new OleDbParameter("@3", frage));
                        cmd.Parameters.Add(new OleDbParameter("@4", toage));

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // UPDATE
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TI04";
                    sql += System.Environment.NewLine + "   SET DANM=?";
                    sql += System.Environment.NewLine + "     , FRAGE=?";
                    sql += System.Environment.NewLine + "     , TOAGE=?";
                    sql += System.Environment.NewLine + " WHERE DACD=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", p_danm));
                        cmd.Parameters.Add(new OleDbParameter("@2", frage));
                        cmd.Parameters.Add(new OleDbParameter("@3", toage));
                        cmd.Parameters.Add(new OleDbParameter("@4", p_dacd));

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
            string dacd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDACD).ToString();
            if(MessageBox.Show( dacd + " 를 삭제하시겠습니까?","삭제확인",MessageBoxButtons.YesNo)==DialogResult.No) return;

            DelTI04(dacd);
            grdMainView.DeleteRow(grdMainView.FocusedRowHandle);
        }

        private void DelTI04(string p_dacd)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "DELETE FROM  TI04";
                sql += System.Environment.NewLine + " WHERE DACD=?";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", p_dacd));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void grdMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdMainView.FocusedColumn == gcDACD)
            {
                // 마지막 줄에서만 상병코드를 입력할 수 있게 막는다.
                if (grdMainView.FocusedRowHandle < grdMainView.RowCount - 1) e.Cancel = true;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0602E
{
    public partial class ADD0602E_1 : Form
    {
        public event EventHandler PrevButton_Click;
        public event EventHandler NextButton_Click;

        private string m_user;
        private string m_objdiv;
        private string m_qfydiv;
        private string m_demseq;
        private string m_cnecno;
        private string m_grpno;
        private string m_dcount;
        private string m_demno;
        private string m_eprtno;

        private string m_iofg;
        private string m_reday;
        private string m_bdodt;
        private string m_qfycd;
        private string m_jrby;
        private string m_unisq;
        private string m_simcs;

        private bool IsFirst;
        private String m_PgmPos;


        public ADD0602E_1()
        {
            InitializeComponent();

            m_user = "";
            m_objdiv = "";
            m_qfydiv = "";
            m_demseq = "";
            m_cnecno = "";
            m_grpno = "";
            m_dcount = "";
            m_demno = "";
            m_eprtno = "";
        }

        public ADD0602E_1(string p_user, string p_objdiv, string p_qfydiv, string p_demseq, string p_cnecno, string p_grpno, string p_dcount, string p_demno, string p_eprtno)
        {
            InitializeComponent();

            m_user = p_user;
            m_objdiv = p_objdiv;
            m_qfydiv = p_qfydiv;
            m_demseq = p_demseq;
            m_cnecno = p_cnecno;
            m_grpno = p_grpno;
            m_dcount = p_dcount;
            m_demno = p_demno;
            m_eprtno = p_eprtno;

            if (m_objdiv == "A")
            {
                // 재심청구
                this.Text = "재심청구 상세내역 등록(ADD0602E_1)";
                label8.Text = "재심금액1";
                label2.Text = "재심금액2";
                grdFView.Columns["OBJAMT"].Caption = "재심금액";
            }
            else
            {
                // 이의신청
                this.Text = "이의신청 상세내역 등록(ADD0602E_1)";
                label8.Text = "이신금액1";
                label2.Text = "이신금액2";
                grdFView.Columns["OBJAMT"].Caption = "이신금액";
            }

        }

        private void ADD0602E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_PgmPos = "";
        }

        private void ADD0602E_1_Activated(object sender, EventArgs e)
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
                MessageBox.Show(ex.Message + Environment.NewLine + "(" + m_PgmPos + ")");
            }
        }

        private void Query()
        {
            grdF.DataSource = null;
            List<CDataF> listF = new List<CDataF>();
            grdF.DataSource = listF;
            RefreshGridF();

            grdB.DataSource = null;
            List<CDataB> listB = new List<CDataB>();
            grdB.DataSource = listB;
            RefreshGridB();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";

                m_reday = "";
                m_iofg = GetIOfgByDemno(m_demno, conn);

                string tTI1A = "";
                string tTI1B = "";
                string tTI1F = "";
                string tTI13 = "";
                string fEXDATE = "";

                if (m_iofg == "1")
                {
                    tTI1A = "TI1A";
                    tTI1B = "TI1B";
                    tTI1F = "TI1F";
                    tTI13 = "TI13";
                    fEXDATE = "EXDATE";
                }
                else
                {
                    tTI1A = "TI2A";
                    tTI1B = "TI2B";
                    tTI1F = "TI2F";
                    tTI13 = "TI23";
                    fEXDATE = "BDODT";
                }

                int t31_count = GetTI31Count(conn);

                if (t31_count > 0)
                {
                    // 등록된 내역이 있음.
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT I31.CNECNO";
                    sql = sql + Environment.NewLine + "     , I31.GRPNO";
                    sql = sql + Environment.NewLine + "     , I31.DEMSEQ";
                    sql = sql + Environment.NewLine + "     , I31.DCOUNT";
                    sql = sql + Environment.NewLine + "     , I31.DEMNO";
                    sql = sql + Environment.NewLine + "     , I31.EPRTNO";
                    sql = sql + Environment.NewLine + "     , I31.REDAY";
                    sql = sql + Environment.NewLine + "     , I31.PID";
                    sql = sql + Environment.NewLine + "     , I31.PNM";
                    sql = sql + Environment.NewLine + "     , I31.BDODT";
                    sql = sql + Environment.NewLine + "     , I31.QFYCD";
                    sql = sql + Environment.NewLine + "     , I31.JRBY";
                    sql = sql + Environment.NewLine + "     , I31.UNISQ";
                    sql = sql + Environment.NewLine + "     , I31.SIMCS";
                    sql = sql + Environment.NewLine + "     , I31.SAKAMT1";
                    sql = sql + Environment.NewLine + "     , I31.SAKAMT2";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT1";
                    sql = sql + Environment.NewLine + "     , I31.OBJAMT2";
                    sql = sql + Environment.NewLine + "     , I31.DONFG";
                    sql = sql + Environment.NewLine + "     , I31.OBJTEXT";
                    sql = sql + Environment.NewLine + "     , I31.OBJADD";
                    sql = sql + Environment.NewLine + "     , I31.OBJADDTEXT";
                    sql = sql + Environment.NewLine + "     , I31.REMARK";
                    sql = sql + Environment.NewLine + "     , I31.DOCUNO";
                    sql = sql + Environment.NewLine + "     , I31.MEMO";
                    sql = sql + Environment.NewLine + "     , I32.PRTDT";
                    sql = sql + Environment.NewLine + "  FROM TI31 I31 LEFT JOIN TI32 I32 ON I32.OBJDIV=I31.OBJDIV AND I32.DOCUNO=I31.DOCUNO";
                    sql = sql + Environment.NewLine + " WHERE I31.OBJDIV='" + m_objdiv + "'";
                    sql = sql + Environment.NewLine + "   AND I31.DEMSEQ='" + m_demseq + "'";
                    sql = sql + Environment.NewLine + "   AND I31.CNECNO='" + m_cnecno + "'";
                    sql = sql + Environment.NewLine + "   AND I31.GRPNO ='" + m_grpno + "'";
                    sql = sql + Environment.NewLine + "   AND I31.DCOUNT='" + m_dcount + "'";
                    sql = sql + Environment.NewLine + "   AND I31.DEMNO ='" + m_demno + "'";
                    sql = sql + Environment.NewLine + "   AND I31.EPRTNO='" + m_eprtno + "'";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_reday = reader["REDAY"].ToString();
                        txtPid.Text = reader["PID"].ToString();
                        txtPnm.Text = reader["PNM"].ToString();
                        m_bdodt = reader["BDODT"].ToString();
                        m_qfycd = reader["QFYCD"].ToString();
                        m_jrby = reader["JRBY"].ToString();
                        m_unisq = reader["UNISQ"].ToString();
                        m_simcs = reader["SIMCS"].ToString();
                        txtSakamt1.Text = reader["SAKAMT1"].ToString();
                        txtSakamt2.Text = reader["SAKAMT2"].ToString();
                        txtObjamt1.Text = reader["OBJAMT1"].ToString();
                        txtObjamt2.Text = reader["OBJAMT2"].ToString();
                        SetDonfgnm(reader["DONFG"].ToString());
                        txtObjtext.Text = reader["OBJTEXT"].ToString();
                        SetObjadd(reader["OBJADD"].ToString());
                        txtObjAddtext.Text = reader["OBJADDTEXT"].ToString();
                        txtRemark.Text = reader["REMARK"].ToString();
                        txtMemo.Text = reader["MEMO"].ToString();
                        txtPrtdt.Text = reader["PRTDT"].ToString();
                        txtDocuno.Text = reader["DOCUNO"].ToString();
                        txtEprtno.Text = reader["EPRTNO"].ToString();

                        return true;
                    });

                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT A.LNO";
                    sql = sql + Environment.NewLine + "     , A.HANGNO";
                    sql = sql + Environment.NewLine + "     , A.MOKNO";
                    sql = sql + Environment.NewLine + "     , A.PRICD";
                    sql = sql + Environment.NewLine + "     , A.BGIHO";
                    sql = sql + Environment.NewLine + "     , A.PRKNM";
                    sql = sql + Environment.NewLine + "     , A.DANGA";
                    sql = sql + Environment.NewLine + "     , A.CNTQTY";
                    sql = sql + Environment.NewLine + "     , A.DQTY";
                    sql = sql + Environment.NewLine + "     , A.DDAY";
                    sql = sql + Environment.NewLine + "     , A.GASAN";
                    sql = sql + Environment.NewLine + "     , A.GUMAK";
                    sql = sql + Environment.NewLine + "     , A.SAKAMT";
                    sql = sql + Environment.NewLine + "     , A.OBJAMT";
                    sql = sql + Environment.NewLine + "     , A.JJRMK";
                    sql = sql + Environment.NewLine + "     , A.JJBGIHO";
                    sql = sql + Environment.NewLine + "     , A.OUTSEQ";
                    sql = sql + Environment.NewLine + "     , A.JJDETAIL";
                    sql = sql + Environment.NewLine + "     , A.GUBUN";
                    sql = sql + Environment.NewLine + "     , A.JJTEXT";
                    sql = sql + Environment.NewLine + "     , CASE WHEN RIGHT(A.JJRMK,1) BETWEEN '1' AND '9' ";
                    sql = sql + Environment.NewLine + "            THEN (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=LEFT(A.JJRMK,LEN(A.JJRMK)-1)) ";
                    sql = sql + Environment.NewLine + "            ELSE (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=A.JJRMK) ";
                    sql = sql + Environment.NewLine + "       END AS JJRMKNM";
                    sql = sql + Environment.NewLine + "  FROM TI31A A ";
                    sql = sql + Environment.NewLine + " WHERE A.OBJDIV='" + m_objdiv + "'";
                    sql = sql + Environment.NewLine + "   AND A.DEMSEQ='" + m_demseq + "' ";
                    sql = sql + Environment.NewLine + "   AND A.CNECNO='" + m_cnecno + "' ";
                    sql = sql + Environment.NewLine + "   AND A.GRPNO ='" + m_grpno + "' ";
                    sql = sql + Environment.NewLine + "   AND A.DCOUNT='" + m_dcount + "' ";
                    sql = sql + Environment.NewLine + "   AND A.DEMNO ='" + m_demno + "' ";
                    sql = sql + Environment.NewLine + "   AND A.EPRTNO='" + m_eprtno + "' ";
                    sql = sql + Environment.NewLine + " ORDER BY A.EPRTNO,A.LNO";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        CDataF data = new CDataF();
                        data.Clear();

                        data.LNO = reader["LNO"].ToString();
                        data.HANGNO = reader["HANGNO"].ToString();
                        data.MOKNO = reader["MOKNO"].ToString();
                        data.PRICD = reader["PRICD"].ToString();
                        data.BGIHO = reader["BGIHO"].ToString();
                        data.PRKNM = reader["PRKNM"].ToString();
                        data.DANGA = reader["DANGA"].ToString();
                        data.CNTQTY = reader["CNTQTY"].ToString();
                        data.DQTY = reader["DQTY"].ToString();
                        data.DDAY = reader["DDAY"].ToString();
                        data.GASAN = reader["GASAN"].ToString();
                        data.GUMAK = reader["GUMAK"].ToString();
                        data.SAKAMT = reader["SAKAMT"].ToString();
                        data.OBJAMT = reader["OBJAMT"].ToString();
                        data.JJRMK = reader["JJRMK"].ToString();
                        data.JJBGIHO = reader["JJBGIHO"].ToString();
                        data.OUTSEQ = reader["OUTSEQ"].ToString();
                        data.JJDETAIL = reader["JJDETAIL"].ToString();
                        data.GUBUN = reader["GUBUN"].ToString();
                        data.JJTEXT = reader["JJTEXT"].ToString();
                        data.JJRMKNM = reader["JJRMKNM"].ToString();

                        listF.Add(data);

                        return true;
                    });
                }
                else
                {
                    // 등록된 내역이 없음. 심결에서 읽는다.
                    string tF1 = "TIE_F0201_062";
                    string tF2 = "TIE_F0202_062";
                    string tF3 = "TIE_F0203_062";
                    //string tF8 = "TIE_F0208_062";
                    
                    if(m_qfydiv == "3"){
                        tF1 = "TIE_F0601_062";
                        tF2 = "TIE_F0602_062";
                        tF3 = "TIE_F0603_062";
                        //tF8 = "TIE_F0608_062";
                    }else if(m_qfydiv == "6"){
                        tF1 = "TIE_N0201";
                        tF2 = "TIE_N0202";
                        tF3 = "TIE_N0203";
                        //tF8 = "";
                    }
                    
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT F2.CNECNO";
                    sql = sql + Environment.NewLine + "     , F2.GRPNO";
                    sql = sql + Environment.NewLine + "     , F2.DEMSEQ";
                    sql = sql + Environment.NewLine + "     , F2.DCOUNT";
                    sql = sql + Environment.NewLine + "     , F2.DEMNO";
                    sql = sql + Environment.NewLine + "     , F2.EPRTNO";
                    sql = sql + Environment.NewLine + "     , F1.REDAY";
                    sql = sql + Environment.NewLine + "     , A.PID";
                    sql = sql + Environment.NewLine + "     , F2.PNM";
                    sql = sql + Environment.NewLine + "     , A.QFYCD";
                    sql = sql + Environment.NewLine + "     , A.JRBY";
                    sql = sql + Environment.NewLine + "     , A.UNISQ";
                    sql = sql + Environment.NewLine + "     , A.SIMCS";
                    sql = sql + Environment.NewLine + "     , RTRIM(F2.MEMO) AS MEMO";
                    sql = sql + Environment.NewLine + "     , A." + fEXDATE + " AS EXDATE";
                    sql = sql + Environment.NewLine + "  FROM " + tF2 + " F2 INNER JOIN " + tF1 + " F1 ON F1.DEMSEQ=F2.DEMSEQ";
                    sql = sql + Environment.NewLine + "                                               AND F1.CNECNO=F2.CNECNO";
                    sql = sql + Environment.NewLine + "                                               AND F1.GRPNO=F2.GRPNO";
                    sql = sql + Environment.NewLine + "                                               AND F1.DCOUNT=F2.DCOUNT";
                    sql = sql + Environment.NewLine + "                                               AND F1.DEMNO=F2.DEMNO";
                    sql = sql + Environment.NewLine + "                      INNER JOIN " + tTI1A + " A ON A.DEMNO=F2.DEMNO";
                    sql = sql + Environment.NewLine + "                                                AND A.EPRTNO=F2.EPRTNO";
                    sql = sql + Environment.NewLine + " WHERE F2.DEMSEQ='" + m_demseq + "' ";
                    sql = sql + Environment.NewLine + "   AND F2.CNECNO='" + m_cnecno + "' ";
                    sql = sql + Environment.NewLine + "   AND F2.GRPNO ='" + m_grpno + "' ";
                    sql = sql + Environment.NewLine + "   AND F2.DCOUNT='" + m_dcount + "' ";
                    sql = sql + Environment.NewLine + "   AND F2.DEMNO ='" + m_demno + "' ";
                    sql = sql + Environment.NewLine + "   AND F2.EPRTNO='" + m_eprtno + "' ";

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        m_reday = reader["REDAY"].ToString();
                        txtPid.Text = reader["PID"].ToString();
                        txtPnm.Text = reader["PNM"].ToString();
                        m_bdodt = reader["EXDATE"].ToString();
                        m_qfycd = reader["QFYCD"].ToString();
                        m_jrby = reader["JRBY"].ToString();
                        m_unisq = reader["UNISQ"].ToString();
                        m_simcs = reader["SIMCS"].ToString();
                        txtSakamt1.Text = "0"; // 아래에서 다시
                        txtSakamt2.Text = "0"; // 아래에서 다시
                        txtObjamt1.Text = "0"; // 아래에서 다시
                        txtObjamt2.Text = "0"; // 아래에서 다시
                        SetDonfgnm("");
                        txtObjtext.Text = "";
                        SetObjadd("");
                        txtObjAddtext.Text = "";
                        txtRemark.Text = "";
                        txtMemo.Text = reader["MEMO"].ToString();
                        txtPrtdt.Text = "";
                        txtDocuno.Text = "";
                        txtEprtno.Text = reader["EPRTNO"].ToString();
                        return true;
                    });

                    if (m_objdiv == "B")
                    {
                        sql = "";
                        sql = sql + Environment.NewLine + "SELECT OBJTEXT ";
                        sql = sql + Environment.NewLine + "  FROM TI31 ";
                        sql = sql + Environment.NewLine + " WHERE OBJDIV='A'";
                        sql = sql + Environment.NewLine + "   AND DEMSEQ='" + m_demseq + "'";
                        sql = sql + Environment.NewLine + "   AND CNECNO='" + m_cnecno + "'";
                        sql = sql + Environment.NewLine + "   AND GRPNO ='" + m_grpno + "'";
                        sql = sql + Environment.NewLine + "   AND DCOUNT='" + m_dcount + "'";
                        sql = sql + Environment.NewLine + "   AND DEMNO ='" + m_demno + "'";
                        sql = sql + Environment.NewLine + "   AND EPRTNO='" + m_eprtno + "'";

                        MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                        {
                            txtObjtext.Text = reader["OBJTEXT"].ToString();
                            return true;
                        });
                    }

                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT '9999999999999' AS OUTSEQ";
                    sql = sql + Environment.NewLine + "     , F3.LNO";
                    sql = sql + Environment.NewLine + "     , F.SEQ1 AS HANGNO";
                    sql = sql + Environment.NewLine + "     , F.POS2 AS MOKNO";
                    sql = sql + Environment.NewLine + "     , F.PRICD";
                    sql = sql + Environment.NewLine + "     , F.BGIHO";
                    sql = sql + Environment.NewLine + "     , F.PRKNM";
                    sql = sql + Environment.NewLine + "     , F.DANGA";
                    sql = sql + Environment.NewLine + "     , F.CNTQTY";
                    sql = sql + Environment.NewLine + "     , F.DQTY";
                    sql = sql + Environment.NewLine + "     , F.DDAY";
                    sql = sql + Environment.NewLine + "     , (CASE F.MAFG WHEN '2' THEN 1+(A.GSRT/100) ELSE 1 END) AS GASAN";
                    sql = sql + Environment.NewLine + "     , ROUND((CASE F.MAFG WHEN '2' THEN 1+(A.GSRT/100) ELSE 1 END)* F.GUMAK,0) AS GUMAK";
                    sql = sql + Environment.NewLine + "     , ROUND((CASE F.MAFG WHEN '2' THEN 1+(A.GSRT/100) ELSE 1 END)*F3.JJGUMAK,0) AS SAKAMT";
                    sql = sql + Environment.NewLine + "     , ROUND((CASE F.MAFG WHEN '2' THEN 1+(A.GSRT/100) ELSE 1 END)*F3.JJGUMAK,0) AS OBJAMT";
                    sql = sql + Environment.NewLine + "     , RTRIM(F3.JJRMK) AS JJRMK";
                    sql = sql + Environment.NewLine + "     , RTRIM(F3.BGIHO) AS JJBGIHO";
                    sql = sql + Environment.NewLine + "     , RTRIM(F3.JJDETAIL) AS JJDETAIL";
                    sql = sql + Environment.NewLine + "     , RTRIM(F.MAFG) AS GUBUN";
                    sql = sql + Environment.NewLine + "     , RTRIM(F3.JJTEXT) AS JJTEXT";
                    sql = sql + Environment.NewLine + "     , CASE WHEN RIGHT(F3.JJRMK,1) BETWEEN '1' AND '9' ";
                    sql = sql + Environment.NewLine + "            THEN (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=LEFT(F3.JJRMK,LEN(F3.JJRMK)-1)) ";
                    sql = sql + Environment.NewLine + "            ELSE (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=F3.JJRMK) ";
                    sql = sql + Environment.NewLine + "       END AS JJRMKNM";
                    sql = sql + Environment.NewLine + "  FROM " + tF3 + " F3 INNER JOIN " + tTI1A + " A ON A.DEMNO=F3.DEMNO AND A.EPRTNO=F3.EPRTNO";
                    sql = sql + Environment.NewLine + "                      LEFT  JOIN " + tTI1F + " F ON F." + fEXDATE + "=A." + fEXDATE + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS AND F.ELINENO=F3.LNO";
                    sql = sql + Environment.NewLine + " WHERE F3.DEMSEQ='" + m_demseq + "' ";
                    sql = sql + Environment.NewLine + "   AND F3.CNECNO='" + m_cnecno + "' ";
                    sql = sql + Environment.NewLine + "   AND F3.GRPNO ='" + m_grpno + "' ";
                    sql = sql + Environment.NewLine + "   AND F3.DCOUNT='" + m_dcount + "' ";
                    sql = sql + Environment.NewLine + "   AND F3.DEMNO ='" + m_demno + "' ";
                    sql = sql + Environment.NewLine + "   AND F3.EPRTNO='" + m_eprtno + "' ";

                    if(m_qfydiv != "6"){
                        sql = sql + Environment.NewLine + "UNION ALL ";
                        sql = sql + Environment.NewLine + "SELECT F4.OUTCNT AS OUTSEQ";
                        sql = sql + Environment.NewLine + "     , F4.LNO";
                        sql = sql + Environment.NewLine + "     , 0 AS HANGNO";
                        sql = sql + Environment.NewLine + "     , 0 AS MOKNO";
                        sql = sql + Environment.NewLine + "     , F.PRICD";
                        sql = sql + Environment.NewLine + "     , F.BGIHO";
                        sql = sql + Environment.NewLine + "     , F.PRKNM";
                        sql = sql + Environment.NewLine + "     , F.DANGA";
                        sql = sql + Environment.NewLine + "     , ROUND(F.DQTY/F.ORDCNT,2) AS CNTQTY";
                        sql = sql + Environment.NewLine + "     , F.ORDCNT AS DQTY";
                        sql = sql + Environment.NewLine + "     , F.DDAY";
                        sql = sql + Environment.NewLine + "     , 1 AS GASAN";
                        sql = sql + Environment.NewLine + "     , F.GUMAK AS GUMAK";
                        sql = sql + Environment.NewLine + "     , F4.JJAMT AS SAKAMT";
                        sql = sql + Environment.NewLine + "     , F4.JJAMT AS OBJAMT";
                        sql = sql + Environment.NewLine + "     , RTRIM(F4.JJRMK) AS JJRMK";
                        sql = sql + Environment.NewLine + "     , RTRIM(F4.BGIHO) AS JJBGIHO";
                        sql = sql + Environment.NewLine + "     , '' AS JJDETAIL";
                        sql = sql + Environment.NewLine + "     , '1' AS GUBUN";
                        sql = sql + Environment.NewLine + "     , RTRIM(F4.MEMO) AS JJTEXT";
                        sql = sql + Environment.NewLine + "     , CASE WHEN RIGHT(F4.JJRMK,1) BETWEEN '1' AND '9' ";
                        sql = sql + Environment.NewLine + "            THEN (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=LEFT(F4.JJRMK,LEN(F4.JJRMK)-1)) ";
                        sql = sql + Environment.NewLine + "            ELSE (SELECT I88.CDNM FROM TI88 I88 WHERE I88.MST1CD='A' AND MST2CD='JJCD' AND MST3CD=F4.JJRMK) ";
                        sql = sql + Environment.NewLine + "       END AS JJRMKNM";
                        sql = sql + Environment.NewLine + "  FROM TIE_F0904 F4  INNER JOIN TIE_F0901 F1 ON F1.DEMSEQ=F4.DEMSEQ AND F1.REDAY=F4.REDAY AND F1.CNECNO=F4.CNECNO AND F1.DCOUNT=F4.DCOUNT";
                        sql = sql + Environment.NewLine + "                     INNER JOIN " + tTI1A + " A ON A.DEMNO=F1.DEMNO AND A.EPRTNO=F4.EPRTNO";
                        sql = sql + Environment.NewLine + "                     LEFT  JOIN " + tTI13 + " F ON F." + fEXDATE + "=A." + fEXDATE + " AND F.QFYCD=A.QFYCD AND F.JRBY=A.JRBY AND F.PID=A.PID AND F.UNISQ=A.UNISQ AND F.SIMCS=A.SIMCS AND F.OUTSEQ=F4.OUTCNT AND F.SEQ=F4.LNO";
                        sql = sql + Environment.NewLine + " WHERE F4.DEMSEQ='" + m_demseq + "' ";
                        sql = sql + Environment.NewLine + "   AND F4.CNECNO='" + m_cnecno + "' ";
                        sql = sql + Environment.NewLine + "   AND F1.GRPNO ='" + m_grpno + "' ";
                        sql = sql + Environment.NewLine + "   AND F4.DCOUNT='" + m_dcount + "' ";
                        sql = sql + Environment.NewLine + "   AND F1.DEMNO ='" + m_demno + "' ";
                        sql = sql + Environment.NewLine + "   AND F4.EPRTNO='" + m_eprtno + "' ";
                        sql = sql + Environment.NewLine + "   AND F4.REDAY =(SELECT MIN(X.REDAY)";
                        sql = sql + Environment.NewLine + "                    FROM TIE_F0904 X";
                        sql = sql + Environment.NewLine + "                   WHERE X.DEMSEQ=F4.DEMSEQ";
                        sql = sql + Environment.NewLine + "                     AND X.CNECNO=F4.CNECNO";
                        sql = sql + Environment.NewLine + "                     AND X.DCOUNT=F4.DCOUNT";
                        sql = sql + Environment.NewLine + "                     AND X.EPRTNO=F4.EPRTNO";
                        sql = sql + Environment.NewLine + "                     AND X.OUTCNT=F4.OUTCNT";
                        sql = sql + Environment.NewLine + "                     AND X.LNO=F4.LNO";
                        sql = sql + Environment.NewLine + "                     AND X.JJRMK=F4.JJRMK";
                        sql = sql + Environment.NewLine + "                     AND X.REDAY>='" + m_reday + "'";
                        sql = sql + Environment.NewLine + "                 )";
                    }
                    sql = sql + Environment.NewLine + " ORDER BY 1,2 ";

                    int sakamt1 = 0;
                    int sakamt2 = 0;
                    int objamt1 = 0;
                    int objamt2 = 0;

                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        CDataF data = new CDataF();
                        data.Clear();

                        data.LNO = reader["LNO"].ToString();
                        data.HANGNO = reader["HANGNO"].ToString();
                        data.MOKNO = reader["MOKNO"].ToString();
                        data.PRICD = reader["PRICD"].ToString();
                        data.BGIHO = reader["BGIHO"].ToString();
                        data.PRKNM = reader["PRKNM"].ToString();
                        data.DANGA = reader["DANGA"].ToString();
                        data.CNTQTY = reader["CNTQTY"].ToString();
                        data.DQTY = reader["DQTY"].ToString();
                        data.DDAY = reader["DDAY"].ToString();
                        data.GASAN = reader["GASAN"].ToString();
                        data.GUMAK = reader["GUMAK"].ToString();
                        data.SAKAMT = reader["SAKAMT"].ToString();
                        data.OBJAMT = reader["OBJAMT"].ToString();
                        data.JJRMK = reader["JJRMK"].ToString();
                        data.JJBGIHO = reader["JJBGIHO"].ToString();
                        data.OUTSEQ = reader["OUTSEQ"].ToString();
                        data.JJDETAIL = reader["JJDETAIL"].ToString();
                        data.GUBUN = reader["GUBUN"].ToString();
                        data.JJTEXT = reader["JJTEXT"].ToString();
                        data.JJRMKNM = reader["JJRMKNM"].ToString();

                        if (data.OUTSEQ == "9999999999999") data.OUTSEQ = "";

                        int sakamt = 0;
                        int.TryParse(data.SAKAMT, out sakamt);
                        int objamt = 0;
                        int.TryParse(data.OBJAMT, out objamt);

                        if (data.GUBUN == "2")
                        {
                            sakamt2 += sakamt;
                            objamt2 += objamt;
                        }
                        else
                        {
                            sakamt1 += sakamt;
                            objamt1 += objamt;
                        }

                        listF.Add(data);

                        return true;
                    });

                    txtSakamt1.Text = sakamt1.ToString();
                    txtSakamt2.Text = sakamt2.ToString();
                    txtObjamt1.Text = objamt1.ToString();
                    txtObjamt2.Text = objamt2.ToString();

                }

                // 진단명을 구한다.
                sql = "";
                sql = sql + Environment.NewLine + "SELECT B.ROFG, B.DACD, B.DANM";
                sql = sql + Environment.NewLine + "  FROM " + tTI1B + " B ";
                sql = sql + Environment.NewLine + " WHERE B." + fEXDATE + "='" + m_bdodt + "' ";
                sql = sql + Environment.NewLine + "   AND B.QFYCD ='" + m_qfycd + "' ";
                sql = sql + Environment.NewLine + "   AND B.JRBY  ='" + m_jrby + "' ";
                sql = sql + Environment.NewLine + "   AND B.PID   ='" + txtPid.Text.ToString() + "' ";
                sql = sql + Environment.NewLine + "   AND B.UNISQ = " + m_unisq + "  ";
                sql = sql + Environment.NewLine + "   AND B.SIMCS = " + m_simcs + "  ";
                sql = sql + Environment.NewLine + "   AND ISNULL(B.DACD,'') <> '' ";
                sql = sql + Environment.NewLine + " ORDER BY B." + fEXDATE + ",B.QFYCD,B.JRBY,B.PID,B.UNISQ,B.SIMCS,B.SEQ1";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataB data = new CDataB();
                    data.Clear();

                    data.ROFG = (reader["ROFG"].ToString() == "1");
                    data.DACD = reader["DACD"].ToString();
                    data.DANM = reader["DANM"].ToString();

                    listB.Add(data);

                    return true;
                });

            }

            RefreshGridF();
            RefreshGridB();

            if (txtPrtdt.Text.ToString() == "")
            {
                txtPrtdt.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            else
            {
                txtPrtdt.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            }
        }

        private string GetIOfgByDemno(string p_demno, OleDbConnection p_conn)
        {
            string ret="";
            string sql="";
            sql = sql + Environment.NewLine + "SELECT IOFG ";
            sql = sql + Environment.NewLine + "  FROM TIE_H010 ";
            sql = sql + Environment.NewLine + " WHERE DEMNO = '" + p_demno + "' ";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["IOFG"].ToString();
                return true;
            });
            return ret;
        }

        private int GetTI31Count(OleDbConnection p_conn)
        {
            int t31_count = 0;
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT COUNT(*) AS CNT ";
            sql = sql + Environment.NewLine + "  FROM TI31 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + m_objdiv + "'";
            sql = sql + Environment.NewLine + "   AND DEMSEQ='" + m_demseq + "'";
            sql = sql + Environment.NewLine + "   AND CNECNO='" + m_cnecno + "'";
            sql = sql + Environment.NewLine + "   AND GRPNO ='" + m_grpno + "'";
            sql = sql + Environment.NewLine + "   AND DCOUNT='" + m_dcount + "'";
            sql = sql + Environment.NewLine + "   AND DEMNO ='" + m_demno + "'";
            sql = sql + Environment.NewLine + "   AND EPRTNO='" + m_eprtno + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                int.TryParse(reader["CNT"].ToString(), out t31_count);
                return false;
            });
            return t31_count;
        }

        private int GetTI31Count(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int t31_count = 0;
            string sql = "";
            sql = sql + Environment.NewLine + "SELECT COUNT(*) AS CNT ";
            sql = sql + Environment.NewLine + "  FROM TI31 ";
            sql = sql + Environment.NewLine + " WHERE OBJDIV='" + m_objdiv + "'";
            sql = sql + Environment.NewLine + "   AND DEMSEQ='" + m_demseq + "'";
            sql = sql + Environment.NewLine + "   AND CNECNO='" + m_cnecno + "'";
            sql = sql + Environment.NewLine + "   AND GRPNO ='" + m_grpno + "'";
            sql = sql + Environment.NewLine + "   AND DCOUNT='" + m_dcount + "'";
            sql = sql + Environment.NewLine + "   AND DEMNO ='" + m_demno + "'";
            sql = sql + Environment.NewLine + "   AND EPRTNO='" + m_eprtno + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                int.TryParse(reader["CNT"].ToString(), out t31_count);
                return false;
            });
            return t31_count;
        }

        private void SetObjadd(string p_objadd)
        {
            string[] arr_objadd = (p_objadd + ",,,,,").Split(',');
            for (int i = 0; i < arr_objadd.Length; i++)
            {
                if (arr_objadd[i] == "1") chkObjAdd0.Checked = true;
                else if (arr_objadd[i] == "2") chkObjAdd1.Checked = true;
                else if (arr_objadd[i] == "3") chkObjAdd2.Checked = true;
                else if (arr_objadd[i] == "4") chkObjAdd3.Checked = true;
                else if (arr_objadd[i] == "5") chkObjAdd4.Checked = true;
            }
        }

        private string GetObjadd()
        {
            string ret = "";
            if (chkObjAdd0.Checked == true) ret += "1,";
            if (chkObjAdd1.Checked == true) ret += "2,";
            if (chkObjAdd2.Checked == true) ret += "3,";
            if (chkObjAdd3.Checked == true) ret += "4,";
            if (chkObjAdd4.Checked == true) ret += "5,";
            return ret;
        }

        private void SetDonfgnm(string p_donfg)
        {
            if (p_donfg == "Y") txtDonfgnm.Text = "완료";
            else if (p_donfg == "N") txtDonfgnm.Text = "미완";
            else if (p_donfg == "P") txtDonfgnm.Text = "보류";
            else if (p_donfg == "") txtDonfgnm.Text = "기초";
            else txtDonfgnm.Text = "미완";
        }

        private string GetDonfg()
        {
            string ret = "";
            if (txtDonfgnm.Text.ToString() == "완료") ret = "Y";
            else if (txtDonfgnm.Text.ToString() == "미완") ret = "N";
            else if (txtDonfgnm.Text.ToString() == "보류") ret = "P";
            else if (txtDonfgnm.Text.ToString() == "기초") ret = "";
            else  ret = "N";
            return ret;
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

        private void RefreshGridF()
        {
            if (grdF.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdF.BeginInvoke(new Action(() => grdFView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdFView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridB()
        {
            if (grdB.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdB.BeginInvoke(new Action(() => grdBView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdBView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdFView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtJJtext.Text = "";

            DevExpress.XtraGrid.Views.Grid.GridView v = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (v == null) return;

            string jjrmk = Convert.ToString(v.GetRowCellValue(e.FocusedRowHandle, "JJRMK"));
            string jjrmknm = Convert.ToString(v.GetRowCellValue(e.FocusedRowHandle, "JJRMKNM"));
            string jjtext = Convert.ToString(v.GetRowCellValue(e.FocusedRowHandle, "JJTEXT"));

            if (jjrmknm != "")
            {
                txtJJtext.Text = jjrmk + " : " + jjrmknm + Environment.NewLine + jjtext;
            }
            else
            {
                txtJJtext.Text = jjtext;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string donfg = GetDonfg();
                if (donfg == "") donfg = "N";
                //
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(donfg);
                SetDonfgnm(donfg);
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

        private void Save(string p_donfg)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sysdt = MetroLib.Util.GetSysDate(conn, tran);
                    string systm = MetroLib.Util.GetSysTime(conn, tran);

                    string sql = "";

                    string jrkwafg = "";
                    string pid = txtPid.Text.ToString();
                    string pnm = txtPnm.Text.ToString();
                    string sakamt1 = txtSakamt1.Text.ToString();
                    string sakamt2 = txtSakamt2.Text.ToString();
                    string objamt1 = txtObjamt1.Text.ToString();
                    string objamt2 = txtObjamt2.Text.ToString();
                    string objtext = txtObjtext.Text.ToString();
                    string objadd = GetObjadd();
                    string objaddtext = txtObjAddtext.Text.ToString();
                    string donfg = p_donfg;
                    string memo = txtMemo.Text.ToString();
                    string remark = txtRemark.Text.ToString();
                    string docuno = txtDocuno.Text.ToString();

                    int t31_count = GetTI31Count(conn, tran);
                    if (t31_count < 1)
                    {
                        sql = "";
                        sql = sql + Environment.NewLine + "INSERT INTO TI31(OBJDIV,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,EPRTNO,BDIV,QFYDIV,JRKWAFG,BDODT,QFYCD,JRBY,PID,UNISQ,SIMCS,PNM,REDAY,SAKAMT1,SAKAMT2,OBJAMT1,OBJAMT2,OBJTEXT,OBJADD,OBJADDTEXT,DONFG,MEMO,REMARK,EMPID,ENTDT,ENTTM)";
                        sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                        List<Object> para = new List<Object>();
                        para.Add(m_objdiv);
                        para.Add(m_demseq);
                        para.Add(m_cnecno);
                        para.Add(m_grpno);
                        para.Add(m_dcount);
                        para.Add(m_demno);
                        para.Add(m_eprtno);
                        para.Add(m_iofg);
                        para.Add(m_qfydiv);
                        para.Add(jrkwafg);
                        para.Add(m_bdodt);
                        para.Add(m_qfycd);
                        para.Add(m_jrby);
                        para.Add(pid);
                        para.Add(MetroLib.StrHelper.ToLong(m_unisq));
                        para.Add(MetroLib.StrHelper.ToLong(m_simcs));
                        para.Add(pnm);
                        para.Add(m_reday);
                        para.Add(MetroLib.StrHelper.ToLong(sakamt1));
                        para.Add(MetroLib.StrHelper.ToLong(sakamt2));
                        para.Add(MetroLib.StrHelper.ToLong(objamt1));
                        para.Add(MetroLib.StrHelper.ToLong(objamt2));
                        para.Add(objtext);
                        para.Add(objadd);
                        para.Add(objaddtext);
                        para.Add(donfg);
                        para.Add(memo);
                        para.Add(remark);
                        para.Add(m_user);
                        para.Add(sysdt);
                        para.Add(systm);

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                    }
                    else
                    {
                        sql = "";
                        sql = sql + Environment.NewLine + "UPDATE TI31 ";
                        sql = sql + Environment.NewLine + "   SET BDIV=?";
                        sql = sql + Environment.NewLine + "     , QFYDIV=?";
                        sql = sql + Environment.NewLine + "     , JRKWAFG=?";
                        sql = sql + Environment.NewLine + "     , BDODT=?";
                        sql = sql + Environment.NewLine + "     , QFYCD=?";
                        sql = sql + Environment.NewLine + "     , JRBY=?";
                        sql = sql + Environment.NewLine + "     , PID=?";
                        sql = sql + Environment.NewLine + "     , UNISQ=?";
                        sql = sql + Environment.NewLine + "     , SIMCS=?";
                        sql = sql + Environment.NewLine + "     , PNM=?";
                        sql = sql + Environment.NewLine + "     , REDAY=?";
                        sql = sql + Environment.NewLine + "     , SAKAMT1=?";
                        sql = sql + Environment.NewLine + "     , SAKAMT2=?";
                        sql = sql + Environment.NewLine + "     , OBJAMT1=?";
                        sql = sql + Environment.NewLine + "     , OBJAMT2=?";
                        sql = sql + Environment.NewLine + "     , OBJTEXT=?";
                        sql = sql + Environment.NewLine + "     , OBJADD=?";
                        sql = sql + Environment.NewLine + "     , OBJADDTEXT=?";
                        sql = sql + Environment.NewLine + "     , DONFG=?";
                        sql = sql + Environment.NewLine + "     , MEMO=?";
                        sql = sql + Environment.NewLine + "     , REMARK=?";
                        sql = sql + Environment.NewLine + "     , UPDID=?";
                        sql = sql + Environment.NewLine + "     , UPDDT=?";
                        sql = sql + Environment.NewLine + "     , UPDTM=?";
                        sql = sql + Environment.NewLine + " WHERE OBJDIV=?";
                        sql = sql + Environment.NewLine + "   AND DEMSEQ=?";
                        sql = sql + Environment.NewLine + "   AND CNECNO=?";
                        sql = sql + Environment.NewLine + "   AND GRPNO=?";
                        sql = sql + Environment.NewLine + "   AND DCOUNT=?";
                        sql = sql + Environment.NewLine + "   AND DEMNO=?";
                        sql = sql + Environment.NewLine + "   AND EPRTNO=?";

                        List<Object> para = new List<Object>();
                        para.Add(m_iofg);
                        para.Add(m_qfydiv);
                        para.Add(jrkwafg);
                        para.Add(m_bdodt);
                        para.Add(m_qfycd);
                        para.Add(m_jrby);
                        para.Add(pid);
                        para.Add(MetroLib.StrHelper.ToLong(m_unisq));
                        para.Add(MetroLib.StrHelper.ToLong(m_simcs));
                        para.Add(pnm);
                        para.Add(m_reday);
                        para.Add(MetroLib.StrHelper.ToLong(sakamt1));
                        para.Add(MetroLib.StrHelper.ToLong(sakamt2));
                        para.Add(MetroLib.StrHelper.ToLong(objamt1));
                        para.Add(MetroLib.StrHelper.ToLong(objamt2));
                        para.Add(objtext);
                        para.Add(objadd);
                        para.Add(objaddtext);
                        para.Add(donfg);
                        para.Add(memo);
                        para.Add(remark);
                        para.Add(m_user);
                        para.Add(sysdt);
                        para.Add(systm);
                        para.Add(m_objdiv);
                        para.Add(m_demseq);
                        para.Add(m_cnecno);
                        para.Add(m_grpno);
                        para.Add(m_dcount);
                        para.Add(m_demno);
                        para.Add(m_eprtno);

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                    }

                    sql = "";
                    sql = sql + Environment.NewLine + "DELETE ";
                    sql = sql + Environment.NewLine + "  FROM TI31A ";
                    sql = sql + Environment.NewLine + " WHERE OBJDIV='" + m_objdiv + "'";
                    sql = sql + Environment.NewLine + "   AND DEMSEQ='" + m_demseq + "'";
                    sql = sql + Environment.NewLine + "   AND CNECNO='" + m_cnecno + "'";
                    sql = sql + Environment.NewLine + "   AND GRPNO ='" + m_grpno + "'";
                    sql = sql + Environment.NewLine + "   AND DCOUNT='" + m_dcount + "'";
                    sql = sql + Environment.NewLine + "   AND DEMNO ='" + m_demno + "'";
                    sql = sql + Environment.NewLine + "   AND EPRTNO='" + m_eprtno + "'";

                    MetroLib.SqlHelper.ExecuteSql(sql, conn, tran);


                    for (int i = 0; i < grdFView.RowCount; i++)
                    {
                        string hangno = grdFView.GetRowCellValue(i, "HANGNO").ToString();
                        string mokno = grdFView.GetRowCellValue(i, "MOKNO").ToString();
                        string lno = grdFView.GetRowCellValue(i, "LNO").ToString();
                        string pricd = grdFView.GetRowCellValue(i, "PRICD").ToString();
                        string bgiho = grdFView.GetRowCellValue(i, "BGIHO").ToString();
                        string prknm = grdFView.GetRowCellValue(i, "PRKNM").ToString();
                        string danga = grdFView.GetRowCellValue(i, "DANGA").ToString();
                        string cntqty = grdFView.GetRowCellValue(i, "CNTQTY").ToString();
                        string dqty = grdFView.GetRowCellValue(i, "DQTY").ToString();
                        string dday = grdFView.GetRowCellValue(i, "DDAY").ToString();
                        string gasan = grdFView.GetRowCellValue(i, "GASAN").ToString();
                        string gumak = grdFView.GetRowCellValue(i, "GUMAK").ToString();
                        string sakamt = grdFView.GetRowCellValue(i, "SAKAMT").ToString();
                        string objamt = grdFView.GetRowCellValue(i, "OBJAMT").ToString();
                        string jjrmk = grdFView.GetRowCellValue(i, "JJRMK").ToString();
                        string jjbgiho = grdFView.GetRowCellValue(i, "JJBGIHO").ToString();
                        string outseq = grdFView.GetRowCellValue(i, "OUTSEQ").ToString();
                        string jjdetail = grdFView.GetRowCellValue(i, "JJDETAIL").ToString();
                        string gubun = grdFView.GetRowCellValue(i, "GUBUN").ToString();
                        string jjrmknm = grdFView.GetRowCellValue(i, "JJRMKNM").ToString();
                        string jjtext = grdFView.GetRowCellValue(i, "JJTEXT").ToString();

                        if (cntqty == "") cntqty = "1";

                        sql = "";
                        sql = sql + Environment.NewLine + "INSERT INTO TI31A(OBJDIV,DEMSEQ,CNECNO,GRPNO,DCOUNT,DEMNO,EPRTNO,OUTSEQ,LNO,JJRMK,JJDETAIL,HANGNO,MOKNO,PRICD,BGIHO,PRKNM,DANGA,CNTQTY,DQTY,DDAY,GASAN,GUMAK,SAKAMT,OBJAMT,GUBUN,JJBGIHO,JJTEXT)";
                        sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                        List<Object> para = new List<Object>();
                        para.Add(m_objdiv);
                        para.Add(m_demseq);
                        para.Add(m_cnecno);
                        para.Add(m_grpno);
                        para.Add(m_dcount);
                        para.Add(m_demno);
                        para.Add(m_eprtno);
                        para.Add(outseq);
                        para.Add(MetroLib.StrHelper.ToLong(lno));
                        para.Add(jjrmk);
                        para.Add(jjdetail);
                        para.Add(MetroLib.StrHelper.ToLong(hangno));
                        para.Add(MetroLib.StrHelper.ToLong(mokno));
                        para.Add(pricd);
                        para.Add(bgiho);
                        para.Add(prknm);
                        para.Add(MetroLib.StrHelper.ToLong(danga));
                        para.Add(MetroLib.StrHelper.ToDouble(cntqty));
                        para.Add(MetroLib.StrHelper.ToDouble(dqty));
                        para.Add(MetroLib.StrHelper.ToLong(dday));
                        para.Add(MetroLib.StrHelper.ToDouble(gasan));
                        para.Add(MetroLib.StrHelper.ToLong(gumak));
                        para.Add(MetroLib.StrHelper.ToLong(sakamt));
                        para.Add(MetroLib.StrHelper.ToLong(objamt));
                        para.Add(gubun);
                        para.Add(jjbgiho);
                        para.Add(jjtext);

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                    }
                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }

            }
        }

        private void grdFView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "OBJAMT")
            {
                int objamt1 = 0;
                int objamt2 = 0;
                for (int i = 0; i < grdFView.RowCount; i++)
                {
                    string gubun = grdFView.GetRowCellValue(i, "GUBUN").ToString();
                    string objamt = grdFView.GetRowCellValue(i, "OBJAMT").ToString();
                    int amt = 0;
                    int.TryParse(objamt, out amt);
                    if (gubun == "2")
                    {
                        objamt2 += amt;
                    }
                    else
                    {
                        objamt1 += amt;
                    }
                }
                txtObjamt1.Text = objamt1.ToString();
                txtObjamt2.Text = objamt2.ToString();
            }
        }

        private void btnSaveY_Click(object sender, EventArgs e)
        {
            try
            {
                string donfg = "Y";
                //
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(donfg);
                SetDonfgnm(donfg);
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

        private void btnSaveP_Click(object sender, EventArgs e)
        {
            try
            {
                string donfg = "P";
                //
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(donfg);
                SetDonfgnm(donfg);
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

        private void btnSaveN_Click(object sender, EventArgs e)
        {
            try
            {
                string donfg = "N";
                //
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save(donfg);
                SetDonfgnm(donfg);
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.PrevButton_Click != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.m_requery = false;
                PrevButton_Click(this, args);
                if (args.m_requery == true)
                {
                    m_eprtno = args.m_eprtno;
                    btnQuery.PerformClick();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.NextButton_Click != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.m_requery = false;
                NextButton_Click(this, args);
                if (args.m_requery == true)
                {
                    m_eprtno = args.m_eprtno;
                    btnQuery.PerformClick();
                }
            }
        }

        private void grdFView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }
    }
}

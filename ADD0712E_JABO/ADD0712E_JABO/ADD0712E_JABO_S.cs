using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0712E_JABO
{
    public partial class ADD0712E_JABO_S : Form
    {
        private bool IsFirst;

        private string m_IOFG;
        private string m_EXDATE;
        private string m_QFYCD;
        private string m_JRBY;
        private string m_PID;
        private string m_UNISQ;
        private string m_SIMCS;

        public ADD0712E_JABO_S()
        {
            InitializeComponent();

            m_IOFG = "";
            m_EXDATE = "";
            m_QFYCD = "";
            m_JRBY = "";
            m_PID = "";
            m_UNISQ = "";
            m_SIMCS = "";
        }

        public ADD0712E_JABO_S(string iofg, string exdate, string qfycd, string jrby, string pid, string unisq, string simcs)
            : this()
        {
            m_IOFG = iofg;
            m_EXDATE = exdate;
            m_QFYCD = qfycd;
            m_JRBY = jrby;
            m_PID = pid;
            m_UNISQ = unisq;
            m_SIMCS = simcs;
        }

        private void ADD0712E_JABO_S_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0712E_JABO_S_Activated(object sender, EventArgs e)
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
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                QueryA(conn);
                QueryB(conn);
                QueryJ(conn);
                QueryF(conn);
            }
        }

        private void QueryA(OleDbConnection p_conn)
        {
            string tTI1A = "TI1A";
            string fEXDATE = "EXDATE";
            if (m_IOFG == "2")
            {
                tTI1A = "TI2A";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql += Environment.NewLine + "SELECT A." + fEXDATE + ",A.SIMNO,A.EPRTNO,A.PID,A.PNM,A.RESID,A.UNICD,A.UNINM,A.STEDT,A.EXAMC,A.JRKK,A.APPRNO,A.WARRANTY";
            sql += Environment.NewLine + "     , A.DEMNO,A.RSLT,A.BDEDT,A.ADDZ1,A.ADDZ2,A.ADDZ3,A.ADDZ4,A.ARVPATH,A.IPATH,A.PDRID,A.ERSERIOUS,A.YOFG,A.YOPDIV,A.YOGROUP";
            sql += Environment.NewLine + "     , A.GSRT,A.TTAMT,A.JBPTAMT,A.UNAMT";
            sql += Environment.NewLine + "     , DBO.MFI_GET_AGE_Y(A01.BTHDT,A.STEDT) AS PAGE";
            sql += Environment.NewLine + "     , A52.JBUNICD";
            sql += Environment.NewLine + "     , A04.BEDEHM,A04.BEDODT,A04.BEDOHM";
            sql += Environment.NewLine + "     , A07.DRNM AS PDRNM";
            sql += Environment.NewLine + "  FROM " + tTI1A + " A INNER JOIN TA01 A01 ON A01.PID=A.PID";
            sql += Environment.NewLine + "                       LEFT JOIN TA52 A52 ON A52.UNICD=A.UNICD";
            sql += Environment.NewLine + "                       LEFT JOIN TA04 A04 ON A04.PID=A.PID AND A04.BEDEDT=A.BDEDT";
            sql += Environment.NewLine + "                       LEFT JOIN TA07 A07 ON A07.DRID=A.PDRID";
            sql += Environment.NewLine + " WHERE A." + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND A.QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND A.JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND A.PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND A.UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND A.SIMCS=" + m_SIMCS + "";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                txtExdate.Text = row[fEXDATE].ToString();
                txtSimno.Text = row["SIMNO"].ToString();
                txtEprtno.Text = row["EPRTNO"].ToString();
                txtPid.Text = row["PID"].ToString();
                txtPnm.Text = row["PNM"].ToString();
                string resid = row["RESID"].ToString();
                if (resid == "*") resid = GetResidAR(p_conn);// 주민번호 암호화
                txtResid.Text = (resid.Length <= 6 ? resid : resid.Substring(0, 6) + "-" + resid.Substring(6));
                txtPAge.Text = row["PAGE"].ToString();
                txtUnicd.Text = row["UNICD"].ToString();
                txtUninm.Text = row["UNINM"].ToString();
                txtJBUnicd.Text = row["JBUNICD"].ToString();
                txtStedt.Text = row["STEDT"].ToString();
                txtExamc.Text = row["EXAMC"].ToString();
                txtJrkk.Text = row["JRKK"].ToString();
                txtApprno.Text = row["APPRNO"].ToString();
                txtWarranty.Text = row["WARRANTY"].ToString();
                txtDemno.Text = row["DEMNO"].ToString();
                txtCnecno.Text = GetCnecno(row["DEMNO"].ToString(), p_conn);
                SetRslt(row["RSLT"].ToString());
                txtBdedt.Text = row["BDEDT"].ToString();
                txtA04Bedehm.Text = row["BEDEHM"].ToString();
                txtA04Bedodt.Text = row["BEDODT"].ToString();
                txtA04Bedohm.Text = row["BEDOHM"].ToString();
                txtDemfg.Text = row["ADDZ1"].ToString();
                txtDemfgnm.Text = GetDemfgnm(row["ADDZ1"].ToString());
                txtAddz3.Text = row["ADDZ3"].ToString();
                txtAddz2.Text = row["ADDZ2"].ToString();
                txtAddz4.Text = row["ADDZ4"].ToString();
                txtArvPath.Text = row["ARVPATH"].ToString();
                txtArvPathnm.Text = GetArvPathnm(row["ARVPATH"].ToString());
                txtIPath.Text = row["IPATH"].ToString();
                txtIPathnm.Text = GetIPathnm(row["IPATH"].ToString());
                txtPdrid.Text = row["PDRID"].ToString();
                txtPdrnm.Text = row["PDRNM"].ToString();
                txtErserious.Text = row["ERSERIOUS"].ToString();
                txtErseriousnm.Text = GetErseriousnm(row["ERSERIOUS"].ToString());
                chkYofg.Checked = row["YOFG"].ToString() == "1";
                txtYopdiv.Text = row["YOPDIV"].ToString();
                txtYogroup.Text = row["YOGROUP"].ToString();
                string gsrt = row["GSRT"].ToString();
                double doub_gsrt = 0;
                double.TryParse(gsrt, out doub_gsrt);
                txtGsrt.Text = string.Format("{0:#.#}", doub_gsrt) + " %";
                txtTtamt.Text = MetroLib.StrHelper.ToNumberWithComma(row["TTAMT"].ToString());
                txtJBPtamt.Text = MetroLib.StrHelper.ToNumberWithComma(row["JBPTAMT"].ToString());
                txtUnamt.Text = MetroLib.StrHelper.ToNumberWithComma(row["UNAMT"].ToString());

                return true;
            });

            // 심사결정액
            sql = "";
            sql += Environment.NewLine + "SELECT N2.ACTGUM";
            sql += Environment.NewLine + "  FROM TIE_N0201 N1 INNER JOIN TIE_N0202 N2 ON N2.DEMSEQ=N1.DEMSEQ AND N2.CNECNO=N1.CNECNO AND N2.GRPNO=N1.GRPNO AND N2.DCOUNT=N1.DCOUNT AND N2.DEMNO=N1.DEMNO";
            sql += Environment.NewLine + " WHERE N1.DEMNO='" + txtDemno.Text.ToString() + "'";
            sql += Environment.NewLine + "   AND N1.CNECNO='" + txtCnecno.Text.ToString() + "'";
            sql += Environment.NewLine + "   AND N2.EPRTNO=" + txtEprtno.Text.ToString() + "";
            sql += Environment.NewLine + " ORDER BY N1.DEMSEQ DESC,N1.CNECNO,N1.GRPNO DESC,N1.DCOUNT DESC,N1.DEMNO,N2.EPRTNO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                txtActgum.Text = MetroLib.StrHelper.ToNumberWithComma(row["ACTGUM"].ToString());
                txtActgum.BackColor = Color.FromArgb(199, 199, 250);

                return false;
            });

        }

        private void QueryB(OleDbConnection p_conn)
        {
            grdDise.DataSource = null;
            List<CDataDise> listDise = new List<CDataDise>();
            grdDise.DataSource = listDise;

            grdDept.DataSource = null;
            List<CDataDept> listDept = new List<CDataDept>();
            grdDept.DataSource = listDept;

            string tTI1B = "TI1B";
            string fEXDATE = "EXDATE";
            if (m_IOFG == "2")
            {
                tTI1B = "TI2B";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql += Environment.NewLine + "SELECT B.ROFG,B.DACD,B.DANM,B.JRKWA";
            sql += Environment.NewLine + "  FROM " + tTI1B + " B";
            sql += Environment.NewLine + " WHERE B." + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND B.QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND B.JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND B.PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND B.UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND B.SIMCS=" + m_SIMCS + "";
            sql += Environment.NewLine + " ORDER BY B.SEQ1";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                CDataDise dataDise = new CDataDise();
                dataDise.Clear();

                dataDise.ROFG = row["ROFG"].ToString();
                dataDise.DACD = row["DACD"].ToString();
                dataDise.DANM = row["DANM"].ToString();

                listDise.Add(dataDise);

                if (row["JRKWA"].ToString() != "" && row["JRKWA"].ToString() != "$$$")
                {
                    CDataDept dataDept = new CDataDept();
                    dataDept.Clear();

                    dataDept.JRKWA = row["JRKWA"].ToString();

                    listDept.Add(dataDept);
                }

                return true;
            });

            RefreshGridDise();
            RefreshGridDept();
        }

        private void QueryJ(OleDbConnection p_conn)
        {
            grdTjcd.DataSource = null;
            List<CDataTjcd> list = new List<CDataTjcd>();
            grdTjcd.DataSource = list;

            string tTI1J = "TI1J";
            string fEXDATE = "EXDATE";
            if (m_IOFG == "2")
            {
                tTI1J = "TI2J";
                fEXDATE = "BDODT";
            }

            string sql = "";
            sql += Environment.NewLine + "SELECT J.TJCD,J.TJCDRMK";
            sql += Environment.NewLine + "  FROM " + tTI1J + " J";
            sql += Environment.NewLine + " WHERE J." + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND J.QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND J.JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND J.PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND J.UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND J.SIMCS=" + m_SIMCS + "";
            sql += Environment.NewLine + "   AND J.ELINENO=0";
            sql += Environment.NewLine + " ORDER BY J.ELINENO,J.SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                CDataTjcd data = new CDataTjcd();
                data.Clear();

                data.TJCD = row["TJCD"].ToString();
                data.TJCDRMK = row["TJCDRMK"].ToString();

                list.Add(data);

                return true;
            });

            RefreshGridTjcd();
        }

        private void QueryF(OleDbConnection p_conn)
        {
            grdF.DataSource = null;
            List<CDataF> list = new List<CDataF>();
            grdF.DataSource = list;

            string tTI13 = "TI13";
            string tTI13T = "TI13T";
            string tTI1F = "TI1F";
            string tTI1J = "TI1J";
            string fEXDATE = "EXDATE";
            if (m_IOFG == "2")
            {
                tTI13 = "TI23";
                tTI13T = "TI23T";
                tTI1F = "TI2F";
                tTI1J = "TI2J";
                fEXDATE = "BDODT";
            }

            CDataF data = null;
            string bkSeq1 = "";
            string bkOutseq = "";

            // 원외
            string sql = "";
            sql += Environment.NewLine + "SELECT PRICD,BGIHO,PRKNM,DANGA,DQTY/ORDCNT AS CNTQTY,DQTY,DDAY,GUMAK,OUTSEQ,ELINENO,SEQ";
            sql += Environment.NewLine + "  FROM " + tTI13 + "";
            sql += Environment.NewLine + " WHERE " + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND SIMCS=" + m_SIMCS + "";
            sql += Environment.NewLine + " ORDER BY OUTSEQ,ELINENO,SEQ";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                if (bkOutseq != row["OUTSEQ"].ToString())
                {
                    data = new CDataF();
                    data.Clear();
                    data.PRICD = "*";
                    data.PRKNM = "원외(" + row["OUTSEQ"].ToString() + ")";
                    data.DATA_FG = "1";
                    list.Add(data);

                    bkOutseq = row["OUTSEQ"].ToString();
                }

                data = new CDataF();
                data.Clear();

                data.PRICD = row["PRICD"].ToString();
                data.BGIHO = row["BGIHO"].ToString();
                data.PRKNM = row["PRKNM"].ToString();
                data.DANGA = row["DANGA"].ToString();
                data.CNTQTY = row["CNTQTY"].ToString();
                data.DQTY = row["DQTY"].ToString();
                data.DDAY = row["DDAY"].ToString();
                data.GUMAK = row["GUMAK"].ToString();
                data.EXDT = row["OUTSEQ"].ToString().Substring(0, 8);
                data.DATA_FG = "2";

                list.Add(data);

                // 원외 특정내역
                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT TJCD,TJCDRMK";
                sql2 += Environment.NewLine + "  FROM " + tTI13T + "";
                sql2 += Environment.NewLine + " WHERE " + fEXDATE + "='" + m_EXDATE + "'";
                sql2 += Environment.NewLine + "   AND QFYCD='" + m_QFYCD + "'";
                sql2 += Environment.NewLine + "   AND JRBY='" + m_JRBY + "'";
                sql2 += Environment.NewLine + "   AND PID='" + m_PID + "'";
                sql2 += Environment.NewLine + "   AND UNISQ=" + m_UNISQ + "";
                sql2 += Environment.NewLine + "   AND SIMCS=" + m_SIMCS + "";
                sql2 += Environment.NewLine + "   AND OUTSEQ=" + row["OUTSEQ"].ToString() + "";
                sql2 += Environment.NewLine + "   AND SEQ=" + row["SEQ"].ToString() + "";
                sql2 += Environment.NewLine + " ORDER BY SEQNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    data = new CDataF();
                    data.Clear();
                    data.BGIHO = row2["TJCD"].ToString();
                    data.PRKNM = row2["TJCDRMK"].ToString();
                    data.DATA_FG = "3";
                    list.Add(data);

                    return true;
                });

                return true;
            });

            // 원내
            sql = "";
            sql += Environment.NewLine + "SELECT PRICD,F.BGIHO,F.PRKNM,F.DANGA,F.CNTQTY,F.DQTY,F.DDAY,F.GUMAK,F.EXDT,F.SEQ1,F.ELINENO";
            sql += Environment.NewLine + "  FROM " + tTI1F + " F";
            sql += Environment.NewLine + " WHERE F." + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND F.QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND F.JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND F.PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND F.UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND F.SIMCS=" + m_SIMCS + "";
            sql += Environment.NewLine + " ORDER BY F.ELINENO,F.SEQ1,F.SEQ2";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                if (bkSeq1 != row["SEQ1"].ToString())
                {
                    data = new CDataF();
                    data.Clear();
                    data.PRICD = "*";
                    data.PRKNM = GetHangNm(row["SEQ1"].ToString());
                    data.DATA_FG = "1";
                    list.Add(data);

                    bkSeq1 = row["SEQ1"].ToString();
                }

                data = new CDataF();
                data.Clear();

                data.PRICD = row["PRICD"].ToString();
                data.BGIHO = row["BGIHO"].ToString();
                data.PRKNM = row["PRKNM"].ToString();
                data.DANGA = row["DANGA"].ToString();
                data.CNTQTY = row["CNTQTY"].ToString();
                data.DQTY = row["DQTY"].ToString();
                data.DDAY = row["DDAY"].ToString();
                data.GUMAK = row["GUMAK"].ToString();
                data.EXDT = row["EXDT"].ToString().Substring(0, 8);
                data.DATA_FG = "2";

                list.Add(data);

                // 줄단위 삭감내역
                string grpno_processing = "";
                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT N3.*";
                sql2 += Environment.NewLine + "  FROM TIE_N0201 N1 INNER JOIN TIE_N0202 N2 ON N2.DEMSEQ=N1.DEMSEQ AND N2.CNECNO=N1.CNECNO AND N2.GRPNO=N1.GRPNO AND N2.DCOUNT=N1.DCOUNT AND N2.DEMNO=N1.DEMNO";
                sql2 += Environment.NewLine + "                    INNER JOIN TIE_N0203 N3 ON N3.DEMSEQ=N1.DEMSEQ AND N3.CNECNO=N1.CNECNO AND N3.GRPNO=N1.GRPNO AND N3.DCOUNT=N1.DCOUNT AND N3.DEMNO=N1.DEMNO AND N3.EPRTNO=N2.EPRTNO";
                sql2 += Environment.NewLine + " WHERE N1.DEMNO='" + txtDemno.Text.ToString() + "'";
                sql2 += Environment.NewLine + "   AND N1.CNECNO='" + txtCnecno.Text.ToString() + "'";
                sql2 += Environment.NewLine + "   AND N2.EPRTNO=" + txtEprtno.Text.ToString() + "";
                sql2 += Environment.NewLine + "   AND N3.LNO=" + row["ELINENO"].ToString() + "";
                sql2 += Environment.NewLine + " ORDER BY N1.DEMSEQ DESC, N1.CNECNO, N1.GRPNO DESC, N1.DCOUNT DESC, N1.DEMNO, N2.EPRTNO,N3.LNO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    // 마지막에 온 것만 보여주기 위한 작업
                    if (grpno_processing == "") grpno_processing = row2["GRPNO"].ToString().TrimEnd();

                    if (grpno_processing == row2["GRPNO"].ToString().TrimEnd())
                    {
                        data = new CDataF();
                        data.Clear();

                        data.PRICD = row2["JJRMK"].ToString().TrimEnd();
                        data.BGIHO = row2["BGIHO"].ToString().TrimEnd();
                        data.PRKNM = row2["JJTEXT"].ToString().TrimEnd();
                        data.DANGA = row2["DANGA"].ToString().TrimEnd();
                        data.CNTQTY = row2["CNTQTY"].ToString().TrimEnd();
                        data.DQTY = row2["DQTY"].ToString().TrimEnd();
                        data.DDAY = row2["DDAY"].ToString().TrimEnd();
                        data.GUMAK = row2["JJGUMAK"].ToString().TrimEnd();
                        data.DATA_FG = "4";

                        list.Add(data);
                    }

                    return true;
                });

                // 줄단위 특정내역
                sql2 = "";
                sql2 += Environment.NewLine + "SELECT J.TJCD,J.TJCDRMK";
                sql2 += Environment.NewLine + "  FROM " + tTI1J + " J";
                sql2 += Environment.NewLine + " WHERE J." + fEXDATE + "='" + m_EXDATE + "'";
                sql2 += Environment.NewLine + "   AND J.QFYCD='" + m_QFYCD + "'";
                sql2 += Environment.NewLine + "   AND J.JRBY='" + m_JRBY + "'";
                sql2 += Environment.NewLine + "   AND J.PID='" + m_PID + "'";
                sql2 += Environment.NewLine + "   AND J.UNISQ=" + m_UNISQ + "";
                sql2 += Environment.NewLine + "   AND J.SIMCS=" + m_SIMCS + "";
                sql2 += Environment.NewLine + "   AND J.ELINENO=" + row["ELINENO"].ToString() + "";
                sql2 += Environment.NewLine + " ORDER BY J.ELINENO,J.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(System.Data.DataRow row2)
                {
                    data = new CDataF();
                    data.Clear();
                    data.BGIHO = row2["TJCD"].ToString();
                    data.PRKNM = row2["TJCDRMK"].ToString();
                    data.DATA_FG = "3";
                    list.Add(data);

                    return true;
                });

                return true;
            });

            // 가로로 셀합치기
            MyGridViewHandler viewHandler = new MyGridViewHandler(grdFView);
            for (int rowHandle = 0; rowHandle < grdFView.RowCount; rowHandle++)
            {
                string dataFg = grdFView.GetRowCellValue(rowHandle, "DATA_FG").ToString();
                if (/*dataFg == "1" || */dataFg == "3")
                {
                    string prknm = grdFView.GetRowCellValue(rowHandle, "PRKNM").ToString();
                    viewHandler.MergeCells(prknm, rowHandle,
                        new DevExpress.XtraGrid.Columns.GridColumn[] { grdFView.Columns[2], 
                                                                       grdFView.Columns[3], 
                                                                       grdFView.Columns[4], 
                                                                       grdFView.Columns[5], 
                                                                       grdFView.Columns[6], 
                                                                       grdFView.Columns[7], 
                                                                       grdFView.Columns[8] });
                }

            }

            RefreshGridF();
        }

        private string GetResidAR(OleDbConnection p_conn)
        {
            string tTI1AR = "TI1AR";
            string fEXDATE = "EXDATE";
            if (m_IOFG == "2")
            {
                tTI1AR = "TI2AR";
                fEXDATE = "BDODT";
            }

            string ret = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT RESID";
            sql += Environment.NewLine + "  FROM " + tTI1AR + "";
            sql += Environment.NewLine + " WHERE " + fEXDATE + "='" + m_EXDATE + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + m_QFYCD + "'";
            sql += Environment.NewLine + "   AND JRBY='" + m_JRBY + "'";
            sql += Environment.NewLine + "   AND PID='" + m_PID + "'";
            sql += Environment.NewLine + "   AND UNISQ=" + m_UNISQ + "";
            sql += Environment.NewLine + "   AND SIMCS=" + m_SIMCS + "";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                ret = row["RESID"].ToString();

                return false;
            });

            return ret;
        }

        private string GetCnecno(string p_demno, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT TOP 1 X.CNECTNO FROM TIE_N0102 X WHERE X.DEMNO='" + p_demno + "' ORDER BY X.CNECTDD DESC";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(System.Data.DataRow row)
            {
                ret = row["CNECTNO"].ToString().TrimEnd();
                return true;
            });
            return ret;
        }

        private void SetRslt(string p_rslt)
        {
            if (p_rslt == "1") rbRslt1.Checked = true;
            else if (p_rslt == "2") rbRslt2.Checked = true;
            else if (p_rslt == "3") rbRslt3.Checked = true;
            else if (p_rslt == "4") rbRslt4.Checked = true;
            else if (p_rslt == "5") rbRslt5.Checked = true;
        }

        private string GetDemfgnm(string p_demfg)
        {
            string ret = "";
            if (p_demfg == "1") ret = "보완청구";
            else if (p_demfg == "2") ret = "추가청구";
            else if (p_demfg == "3") ret = "분리청구";
            else if (p_demfg == "8") ret = "약제추가청구";
            return ret;
        }

        private string GetArvPathnm(string p_arvpath)
        {
            string ret = "";
            if (p_arvpath == "1") ret = "타요양기관경유";
            else if (p_arvpath == "2") ret = "응급구조대후송";
            else if (p_arvpath == "3") ret = "기타";
            return ret;
        }

        private string GetIPathnm(string p_ipath)
        {
            string ret = "";
            if (p_ipath == "1") ret = "응급실 경유";
            else if (p_ipath == "2") ret = "외래 경유";
            return ret;
        }

        private string GetErseriousnm(string p_erserious)
        {
            string ret = "";
            if (p_erserious == "1") ret = "응급중증도1";
            else if (p_erserious == "2") ret = "응급중증도2";
            else if (p_erserious == "3") ret = "응급중증도3";
            else if (p_erserious == "4") ret = "응급중증도4";
            else if (p_erserious == "5") ret = "응급중증도5";
            else if (p_erserious == "6") ret = "다중응급중증도";
            return ret;
        }

        private string GetHangNm(string p_seq1)
        {
            string ret = "";
            if (p_seq1 == "11") ret = "01.진찰료";
            else if (p_seq1 == "12") ret = "02.입원료";
            else if (p_seq1 == "13") ret = "03.투약료";
            else if (p_seq1 == "14") ret = "04.주사료";
            else if (p_seq1 == "15") ret = "05.마취료";
            else if (p_seq1 == "16") ret = "06.이학료";
            else if (p_seq1 == "17") ret = "07.정신료";
            else if (p_seq1 == "18") ret = "08.처치료";
            else if (p_seq1 == "19") ret = "09.검사료";
            else if (p_seq1 == "20") ret = "10.방사선료";
            else if (p_seq1 == "23") ret = "S.특수장비";
            else if (p_seq1 == "22") ret = "L.요양병원";
            else if (p_seq1 == "33") ret = "11.환자납부";
            else if (p_seq1 == "91") ret = "V.100분의100(청구안함)";
            else if (p_seq1 == "92") ret = "W.비급여(청구안함)";
            return ret;
        }

        private void RefreshGridDise()
        {
            if (grdDise.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDise.BeginInvoke(new Action(() => grdDiseView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDiseView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridDept()
        {
            if (grdDept.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDept.BeginInvoke(new Action(() => grdDeptView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDeptView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridTjcd()
        {
            if (grdTjcd.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdTjcd.BeginInvoke(new Action(() => grdTjcdView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdTjcdView.RefreshData();
                Application.DoEvents();
            }
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

        private void grdFView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            string dataFg = View.GetRowCellValue(e.RowHandle, "DATA_FG").ToString();
            if (dataFg == "1")
            {
                // 항번호 명칭 출력중
                if (e.Column.FieldName == "PRICD" || e.Column.FieldName == "PRKNM")
                {
                    e.Appearance.ForeColor = Color.FromArgb(150, Color.Blue);
                }
            }
            if (dataFg == "2" || dataFg == "4")
            {
                // 진료내역(2), 삭감내역(4) 출력중
                if (e.Column.FieldName == "DANGA" || e.Column.FieldName == "DDAY" || e.Column.FieldName == "GUMAK" || e.Column.FieldName == "CNTQTY" || e.Column.FieldName == "DQTY")
                {
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                }
            }
            if (dataFg == "4")
            {
                // 삭감내역 출력중
                e.Appearance.BackColor = Color.FromArgb(150, 250, 199, 199);
            }
        }

        private void grdFView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            string dataFg = grdFView.GetRowCellValue(e.RowHandle, "DATA_FG").ToString();
            if (dataFg == "2" || dataFg == "4")
            {
                // 진료내역(2), 삭감내역(4) 출력중
                if (e.Column.FieldName == "DANGA" || e.Column.FieldName == "GUMAK")
                {
                    long long_result = 0;
                    double doub_result = 0;
                    if (long.TryParse(e.Value.ToString(), out long_result))
                    {
                        e.DisplayText = string.Format("{0:#,##0}", long_result);
                    }
                    else if (double.TryParse(e.Value.ToString(), out doub_result))
                    {
                        e.DisplayText = string.Format("{0:#,##0.#}", doub_result);
                    }
                }
                else if (e.Column.FieldName == "CNTQTY" || e.Column.FieldName == "DQTY")
                {
                    double result = 0;
                    if (double.TryParse(e.Value.ToString(), out result))
                    {
                        e.DisplayText = string.Format("{0:0.#}", result);
                    }

                }
            }
        }
    }
}

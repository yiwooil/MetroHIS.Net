using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraPrinting; 

namespace ADD7008E
{
    public partial class ADD7008E : Form
    {
        public ADD7008E()
        {
            InitializeComponent();
        }

        private void ADD7008E_Load(object sender, EventArgs e)
        {
            SetColHeader();
        }

        private void SetColHeader()
        {
            List<CData> list = new List<CData>();
            CData data = GetColHeader();
            list.Add(data);
            grdMain.DataSource = null;
            grdMain.DataSource = list;
        }

        private CData GetColHeader()
        {
            CData data = new CData();
            data.Clear();
            data.COL_A = "청구번호";
            data.COL_B = "환자ID";
            data.COL_C = "성명";
            data.COL_D = "생년원일";
            data.COL_E = "접수년도";
            data.COL_F = "본부코드";
            data.COL_G = "접수번호";
            data.COL_H = "명일련";
            data.COL_I = "본인부담률";
            data.COL_J = "제품코드(EDI코드)";
            data.COL_K = "품목명(제품명)";
            data.COL_L = "진료의";
            data.COL_M = "면허번호";
            data.COL_N = "주진단코드";
            data.COL_O = "총사용량";
            data.COL_P = "총금액";
            data.COL_Q = "M6670";
            data.COL_R = "M6700";
            data.COL_S = "M6741";
            data.COL_T = "M6750";

            return data;
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

        private void RefreshGrid()
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

        private void btnSaveExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(grdMainView);
        }

        private void ExportGridToExcel(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
            dlg.Title = "엑셀로 저장";
            dlg.FileName = "허가밤위초과치료재료.xlsx";

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            string filePath = dlg.FileName;

            try
            {
                string ext = Path.GetExtension(filePath).ToLower();

                // 인쇄/그룹/필터 적용 상태를 그대로 내보내고 싶으면 옵션 사용
                // (DevExpress 버전에 따라 옵션 클래스가 없을 수 있으니 없으면 제거하세요.)
                if (ext == ".xlsx")
                {
                    // 옵션이 있는 버전
                    //XlsxExportOptions opt = new XlsxExportOptions();
                    //opt.TextExportMode = TextExportMode.Text; // 숫자도 텍스트로 내보내고 싶을 때
                    //view.ExportToXlsx(filePath, opt);

                    // 옵션이 없으면 아래 한 줄로도 대부분 동작
                    view.ExportToXlsx(filePath);
                }
                else
                {
                    // 구버전 Excel
                    view.ExportToXls(filePath);
                }

                MessageBox.Show("저장 완료:\r\n" + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("엑셀 저장 실패:\r\n" + ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ADD7008E_EXCEL f = new ADD7008E_EXCEL();
            f.PtntChanged += new EventHandler<PtntChangedEventArgs>(f_PtntChanged);
            f.ShowDialog(this);
        }

        private void f_PtntChanged(object sender, PtntChangedEventArgs e)
        {
            List<CData> list = null;
            if (e.no == 1)
            {
                list = new List<CData>();
                grdMain.DataSource = null;
                grdMain.DataSource = list;
                CData dataHeader = GetColHeader();
                list.Add(dataHeader);
            }
            else
            {
                list = grdMain.DataSource as List<CData>;
            }

            CData data = new CData();
            data.COL_A = ""; // 청구번호
            data.COL_B = ""; // 환자ID
            data.COL_C = e.pnm; // 수진자 성명
            data.COL_D = ""; // 생년월일
            data.COL_E = e.cnecyy; // 접수년도
            data.COL_F = e.hq_code; // 본부코드
            data.COL_G = e.cnecno; // 접수번호
            data.COL_H = e.eprtno; // 명일련
            data.COL_I = ""; // 본인부담율
            data.COL_J = e.edicode; // 제품코드(EDI코드)
            data.COL_K = e.ediname; // 품목명(제품명)
            data.COL_L = ""; // 진료의
            data.COL_M = ""; // 면허번호
            data.COL_N = ""; // 주진단코드
            data.COL_O = e.tqty; // 총 사용량
            data.COL_P = ""; // 총 금액
            data.COL_Q = ""; // M6670
            data.COL_R = ""; // M6700"
            data.COL_S = ""; // M6741"
            data.COL_T = ""; // M6750"

            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string bdodt = "";
                    string qfycd = "";
                    string jrby = "";
                    string pid = "";
                    string unisq = "";
                    string simcs = "";
                    string iofg = "";

                    // 청구번호를 구한다.
                    string cnectdd = "";
                    string billsno = "";
                    string demno = GetDemno(e.cnecno, conn, ref cnectdd, ref billsno);
                    demno = demno.TrimEnd();

                    data.COL_A = demno; // 청구번호

                    string sql = "";
                    sql = "";
                    sql += Environment.NewLine + "SELECT A.*,A01.BTHDT,A07.DRNM,A07.GDRLCID";
                    sql += Environment.NewLine + "  FROM TI2A A INNER JOIN TA01 A01 ON A01.PID=A.PID";
                    sql += Environment.NewLine + "              LEFT  JOIN TA07 A07 ON A07.DRID=A.PDRID";
                    sql += Environment.NewLine + " WHERE A.DEMNO='" + demno + "'";
                    sql += Environment.NewLine + "   AND A.EPRTNO='" + e.eprtno + "'";

                    MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                    {
                        bdodt = row["BDODT"].ToString();
                        qfycd = row["QFYCD"].ToString();
                        jrby = row["JRBY"].ToString();
                        pid = row["PID"].ToString();
                        unisq = row["UNISQ"].ToString();
                        simcs = row["SIMCS"].ToString();
                        iofg = "2";

                        data.COL_B = row["PID"].ToString(); // 환자ID
                        data.COL_D = row["BTHDT"].ToString(); // 생년월일
                        data.COL_L = row["DRNM"].ToString(); // 진료의
                        data.COL_M = row["GDRLCID"].ToString(); // 면허번호
                        data.COL_N = row["DACD"].ToString().Replace(".",""); // 주진단코드

                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 입원에 없으면 외래에서...
                    if (bdodt == "")
                    {
                        sql = "";
                        sql += Environment.NewLine + "SELECT A.*,A01.BTHDT,A07.DRNM,A07.GDRLCID";
                        sql += Environment.NewLine + "  FROM TI1A A INNER JOIN TA01 A01 ON A01.PID=A.PID";
                        sql += Environment.NewLine + "              LEFT  JOIN TA07 A07 ON A07.DRID=A.PDRID";
                        sql += Environment.NewLine + " WHERE A.DEMNO='" + demno + "'";
                        sql += Environment.NewLine + "   AND A.EPRTNO='" + e.eprtno + "'";

                        MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                        {
                            bdodt = row["EXDATE"].ToString();
                            qfycd = row["QFYCD"].ToString();
                            jrby = row["JRBY"].ToString();
                            pid = row["PID"].ToString();
                            unisq = row["UNISQ"].ToString();
                            simcs = row["SIMCS"].ToString();
                            iofg = "1";

                            data.COL_B = row["PID"].ToString(); // 환자ID
                            data.COL_D = row["BTHDT"].ToString(); // 생년월일
                            data.COL_L = row["DRNM"].ToString(); // 진료의
                            data.COL_M = row["GDRLCID"].ToString(); // 면허번호
                            data.COL_N = row["DACD"].ToString().Replace(".", ""); // 주진단코드

                            return MetroLib.SqlHelper.BREAK;
                        });
                    }

                    // 환자를 찾았다. 진료내역을 읽는다.
                    if (bdodt != "")
                    {
                        string tTI2F = "TI2F";
                        string fBDODT = "BDODT";
                        if (iofg == "1")
                        {
                            tTI2F = "TI1F";
                            fBDODT = "EXDATE";
                        }

                        // 재료의 총금액을 찾는다.
                        sql = "";
                        sql += Environment.NewLine + "SELECT SUM(F.GUMAK) AS TGUMAK";
                        sql += Environment.NewLine + "  FROM " + tTI2F + " F INNER JOIN TA02 A02 ON A02.PRICD=F.PRICD";
                        sql += Environment.NewLine + "                                          AND A02.CREDT=(SELECT MAX(X.CREDT)";
                        sql += Environment.NewLine + "                                                           FROM TA02 X";
                        sql += Environment.NewLine + "                                                          WHERE X.PRICD=F.PRICD";
                        sql += Environment.NewLine + "                                                            AND X.CREDT<=LEFT(F.EXDT,8)";
                        sql += Environment.NewLine + "                                                        )";
                        sql += Environment.NewLine + " WHERE F." + fBDODT + "='" + bdodt + "'";
                        sql += Environment.NewLine + "   AND F.QFYCD='" + qfycd + "'";
                        sql += Environment.NewLine + "   AND F.JRBY='" + jrby + "'";
                        sql += Environment.NewLine + "   AND F.PID='" + pid + "'";
                        sql += Environment.NewLine + "   AND F.UNISQ='" + unisq + "'";
                        sql += Environment.NewLine + "   AND F.SIMCS='" + simcs + "'";
                        sql += Environment.NewLine + "   AND F.BGIHO='" + e.edicode + "'";
                        sql += Environment.NewLine + "   AND A02.GUBUN = '2'"; // 재료코드

                        MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                        {
                            string tgumak = row["TGUMAK"].ToString();

                            int n;
                            string formatted = int.TryParse(tgumak, out n)
                                ? n.ToString("#,##0")
                                : "";

                            data.COL_P = formatted; // 총 금액

                            return MetroLib.SqlHelper.BREAK;
                        });


                        // 행위를 찾는다.
                        sql = "";
                        sql += Environment.NewLine + "SELECT LEFT(F.BGIHO,5) AS BGIHO5, SUM(ISNULL(F.CNTQTY,1) * F.DQTY * F.DDAY) AS TQTY_ACT";
                        sql += Environment.NewLine + "  FROM " + tTI2F + " F INNER JOIN TA02 A02 ON A02.PRICD=F.PRICD";
                        sql += Environment.NewLine + "                                          AND A02.CREDT=(SELECT MAX(X.CREDT)";
                        sql += Environment.NewLine + "                                                           FROM TA02 X";
                        sql += Environment.NewLine + "                                                          WHERE X.PRICD=F.PRICD";
                        sql += Environment.NewLine + "                                                            AND X.CREDT<=LEFT(F.EXDT,8)";
                        sql += Environment.NewLine + "                                                        )";
                        sql += Environment.NewLine + " WHERE F." + fBDODT + "='" + bdodt + "'";
                        sql += Environment.NewLine + "   AND F.QFYCD='" + qfycd + "'";
                        sql += Environment.NewLine + "   AND F.JRBY='" + jrby + "'";
                        sql += Environment.NewLine + "   AND F.PID='" + pid + "'";
                        sql += Environment.NewLine + "   AND F.UNISQ='" + unisq + "'";
                        sql += Environment.NewLine + "   AND F.SIMCS='" + simcs + "'";
                        sql += Environment.NewLine + "   AND LEFT(F.BGIHO,5) IN ('M6670','M6700','M6741','M6750') ";
                        sql += Environment.NewLine + "   AND A02.GUBUN = '1'"; // 행위코드
                        sql += Environment.NewLine + " GROUP BY LEFT(F.BGIHO,5)";

                        MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
                        {
                            string bgiho = row["BGIHO5"].ToString();
                            string tqtyAct = row["TQTY_ACT"].ToString();
                            decimal d;
                            string result = decimal.TryParse(Convert.ToString(tqtyAct), out d)
                                            ? d.ToString("0.########")
                                            : "";


                            if (bgiho == "M6670")
                            {
                                data.COL_Q = result;
                            }
                            else if (bgiho == "M6700")
                            {
                                data.COL_R = result;
                            }
                            else if (bgiho == "M6741")
                            {
                                data.COL_S = result;
                            }
                            else if (bgiho == "M6750")
                            {
                                data.COL_T = result;
                            }

                            return MetroLib.SqlHelper.CONTINUE;
                        });

                    }
                }

                e.Success = true;

            }
            catch (Exception ex)
            {
                e.Success = false;
                e.FailureMessage = ex.Message;
            }
            

            list.Add(data);

            e.Success = true;

            RefreshGrid();
        }

        private string GetDemno(string p_cnecno, OleDbConnection conn, ref string p_cnectdd, ref string p_billsno)
        {
            string demno = "";
            string cnectdd = "";
            string billsno = "1"; // 일단 1로 고정

            // 접수증을 읽는다.
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TIE_F0102";
            sql += System.Environment.NewLine + " WHERE CNECTNO='" + p_cnecno + "'";
            sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                demno = row["DEMNO"].ToString();
                cnectdd = row["CNECTDD"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            p_cnectdd = cnectdd;
            p_billsno = billsno;

            if (demno != "") return demno;

            // 심결을 읽는다.
            sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TIE_F0201_062";
            sql += System.Environment.NewLine + " WHERE CNECNO='" + p_cnecno + "'";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                demno = row["DEMNO"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            p_cnectdd = cnectdd;
            p_billsno = billsno;

            return demno;
        }
    }
}

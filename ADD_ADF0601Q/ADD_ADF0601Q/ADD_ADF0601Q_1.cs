using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD_ADF0601Q
{
    public partial class ADD_ADF0601Q_1 : Form
    {
        private bool IsFirst;

        public String m_User;
        public String m_Prjcd;
        public string m_pid;

        public ADD_ADF0601Q_1()
        {
            InitializeComponent();
        }

        private void ADD_ADF0601Q_1_Load(object sender, EventArgs e)
        {
            m_pid = "";
            IsFirst = true;
        }

        private void ADD_ADF0601Q_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            Init();
            btnQuery.PerformClick();
        }

        private void Init()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";

                // 환자자격
                cboQfycd.Items.Clear();
                cboQfycd.Items.Add("");
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='26' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboQfycd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }
                cboQfycd.SelectedIndex = 0;

                // 진료과
                cboDptcd.Items.Clear();
                cboDptcd.Items.Add("");
                sql = "SELECT DPTCD, DPTNM FROM TA09 (nolock) WHERE DPTDIV='1' ORDER BY DPTCD";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboDptcd.Items.Add(reader["DPTCD"].ToString() + " " + reader["DPTNM"].ToString());
                    }
                    reader.Close();
                }
                cboDptcd.SelectedIndex = 0;

                // 병동
                cboWard.Items.Clear();
                cboWard.Items.Add("");
                sql = "SELECT DPTCD, DPTNM FROM TA09 (nolock) WHERE DPTDIV='3' ORDER BY DPTCD";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboWard.Items.Add(reader["DPTCD"].ToString() + " ");
                    }
                    reader.Close();
                }
                cboWard.SelectedIndex = 0;

                conn.Close();
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
            string pnm = txtPnm.Text.ToString();
            string dptcd = (cboDptcd.SelectedIndex == 0 ? "" : cboDptcd.SelectedItem.ToString().Split(' ')[0]);
            string ward = (cboWard.SelectedIndex == 0 ? "" : cboWard.SelectedItem.ToString().Split(' ')[0]);
            string qfycd = (cboQfycd.SelectedIndex == 0 ? "" : cboQfycd.SelectedItem.ToString().Split(' ')[0]);
            string mypin = ""; // 일단 없는 것으로 하자
            string orderByFg = "";
            string simsafg = "";

            List<CPtnt> list = new List<CPtnt>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 정렬순서 옵션
                string sql = "";
                sql = "SELECT FLD2QTY FROM TA972 (nolock) WHERE PRJCD = 'ADA' AND FRMNM = 'ADA0101Q_PINFO' AND SEQ = 1 ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) orderByFg = reader["FLD2QTY"].ToString();
                    reader.Close();
                }

                // 2018.05.15 WOOIL - 전화번호칸에 쵯종 심사일시 표시(올바른서울요청사항)
                sql = "SELECT FLD2QTY FROM TA972 (nolock) WHERE PRJCD = 'ADB' AND FRMNM = 'ADA0101Q_PINFO' AND SEQ = 2 ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) simsafg = reader["FLD2QTY"].ToString();
                    reader.Close();
                }

                if (rbPtnt1.Checked)
                {
                    ward = ""; // 입원예정자는 병동이 없다.

                    // 입원예정자
                    sql = "";
                    sql += Environment.NewLine + "SELECT t02.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , t02.BDGRD AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , t02.BDQDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , '' ILSU"; // 재원일수
                    sql += Environment.NewLine + "     , '' SIMSADTM"; // 심사일지
                    sql += Environment.NewLine + "  FROM TT02 t02 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = t02.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = t02.DPTCD ";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock) LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a10.QLFYCD ";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock) ";
                    sql += Environment.NewLine + " WHERE LEFT(t02.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(t02.BDEDT,'') = ''";
                    sql += Environment.NewLine + "   AND ISNULL(t02.BDCCF, '') = '' ";
                    sql += Environment.NewLine + "   AND DATEDIFF(d, ISNULL(t02.BDQDT,''), CONVERT(VARCHAR,GETDATE(),112)) < 7 ";
                    sql += Environment.NewLine + "   AND t02.ODT = (SELECT MAX(ODT) FROM TT02 X (nolock) WHERE X.PID = t02.PID) ";
                    sql += Environment.NewLine + "   AND a01.PID = t02.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = t02.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND t02.DPTCD = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a10.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt2.Checked)
                {
                    // 재원환자
                    sql = "";
                    sql += Environment.NewLine + "SELECT a04.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , a04.WARDID+'/'+a04.RMID+'/'+a04.BEDID AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , a04.BEDEDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , DATEDIFF(d,a04.BEDEDT, CASE WHEN ISNULL(a04.BEDODT,'') = '' THEN Getdate() ELSE a04.BEDODT END)+1 ILSU ";
                    sql += Environment.NewLine + "     , (select isnull(replace(left(case when t56.simsa>t56.simsae then t56.simsa  else t56.simsae end,13),'$',' '),'')";
                    sql += Environment.NewLine + "          from tt56 t56";
                    sql += Environment.NewLine + "         Where t56.PID = a04.PID";
                    sql += Environment.NewLine + "           and t56.bdedt=a04.bededt";
                    sql += Environment.NewLine + "           and t56.sttdt=(select max(x.sttdt) from tt56 x where x.pid=t56.pid and x.bdedt=t56.bdedt)";
                    sql += Environment.NewLine + "       ) as SIMSADTM ";
                    sql += Environment.NewLine + "  FROM TA04 a04 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = a04.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = a04.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a04.QLFYCD ";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock)";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock) ";
                    sql += Environment.NewLine + " WHERE ISNULL(a04.BEDODT,'') = ''";
                    sql += Environment.NewLine + "   AND LEFT(a04.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.WARDID,'') <> 'ER1' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.BEDODIV, '') IN ('', '0') ";
                    sql += Environment.NewLine + "   AND a01.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = a04.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.DPTCD = ? ";
                    }
                    if (ward != "")
                    {
                        sql += Environment.NewLine + "   AND a04.WARDID = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt3.Checked)
                {
                    // 퇴원예정자
                    sql = "";
                    sql += Environment.NewLine + "SELECT a04.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , a04.WARDID+'/'+a04.RMID+'/'+a04.BEDID AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , a04.BEDEDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , DATEDIFF(d,a04.BEDEDT, CASE WHEN ISNULL(a04.BEDODT,'') = '' THEN Getdate() ELSE a04.BEDODT END)+1 ILSU ";
                    sql += Environment.NewLine + "     , (select isnull(replace(left(case when t56.simsa>t56.simsae then t56.simsa  else t56.simsae end,13),'$',' '),'')";
                    sql += Environment.NewLine + "          from tt56 t56";
                    sql += Environment.NewLine + "         Where t56.PID = a04.PID";
                    sql += Environment.NewLine + "           and t56.bdedt=a04.bededt";
                    sql += Environment.NewLine + "           and t56.sttdt=(select max(x.sttdt) from tt56 x where x.pid=t56.pid and x.bdedt=t56.bdedt)";
                    sql += Environment.NewLine + "       ) as SIMSADTM";
                    sql += Environment.NewLine + "  FROM TA04 a04 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = a04.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = a04.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a04.QLFYCD ";
                    sql += Environment.NewLine + "     , TT14 t14 (nolock)";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock)";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock) ";
                    sql += Environment.NewLine + " WHERE ISNULL(a04.BEDODT,'') = ''";
                    sql += Environment.NewLine + "   AND LEFT(a04.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.WARDID,'') <> 'ER1' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.BEDODIV, '') IN ('', '0') ";
                    sql += Environment.NewLine + "   AND t14.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND t14.BEDEDT = a04.BEDEDT ";
                    sql += Environment.NewLine + "   AND a01.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = a04.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.DPTCD = ? ";
                    }
                    if (ward != "")
                    {
                        sql += Environment.NewLine + "   AND a04.WARDID = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt4.Checked)
                {
                    // 처방마감자
                    sql = "";
                    sql += Environment.NewLine + "SELECT a04.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , a04.WARDID+'/'+a04.RMID+'/'+a04.BEDID AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , a04.BEDEDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , DATEDIFF(d,a04.BEDEDT, CASE WHEN ISNULL(a04.BEDODT,'') = '' THEN Getdate() ELSE a04.BEDODT END)+1 ILSU ";
                    sql += Environment.NewLine + "     , (select isnull(replace(left(case when t56.simsa>t56.simsae then t56.simsa  else t56.simsae end,13),'$',' '),'')";
                    sql += Environment.NewLine + "          from tt56 t56";
                    sql += Environment.NewLine + "         Where t56.PID = a04.PID";
                    sql += Environment.NewLine + "           and t56.bdedt=a04.bededt";
                    sql += Environment.NewLine + "           and t56.sttdt=(select max(x.sttdt) from tt56 x where x.pid=t56.pid and x.bdedt=t56.bdedt)";
                    sql += Environment.NewLine + "       ) as SIMSADTM ";
                    sql += Environment.NewLine + "  FROM TA04 a04 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = a04.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = a04.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a04.QLFYCD ";
                    sql += Environment.NewLine + "     , TT09 t09 (nolock)";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock)";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock) ";
                    sql += Environment.NewLine + " WHERE ISNULL(a04.BEDODT,'') = ''";
                    sql += Environment.NewLine + "   AND LEFT(a04.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.WARDID,'') <> 'ER1' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.BEDODIV, '') IN ('', '0') ";
                    sql += Environment.NewLine + "   AND t09.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND t09.BDEDT = a04.BEDEDT ";
                    sql += Environment.NewLine + "   AND a01.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = a04.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.DPTCD = ? ";
                    }
                    if (ward != "")
                    {
                        sql += Environment.NewLine + "   AND a04.WARDID = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt5.Checked)
                {
                    // 삼사완료자
                    sql = "";
                    sql += Environment.NewLine + "SELECT a04.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , a04.WARDID+'/'+a04.RMID+'/'+a04.BEDID AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , a04.BEDEDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , DATEDIFF(d,a04.BEDEDT, CASE WHEN ISNULL(a04.BEDODT,'') = '' THEN Getdate() ELSE a04.BEDODT END)+1 ILSU ";
                    sql += Environment.NewLine + "     , (select isnull(replace(left(case when t56.simsa>t56.simsae then t56.simsa  else t56.simsae end,13),'$',' '),'')";
                    sql += Environment.NewLine + "          from tt56 t56";
                    sql += Environment.NewLine + "         Where t56.PID = a04.PID";
                    sql += Environment.NewLine + "           and t56.bdedt=a04.bededt";
                    sql += Environment.NewLine + "           and t56.sttdt=(select max(x.sttdt) from tt56 x where x.pid=t56.pid and x.bdedt=t56.bdedt)";
                    sql += Environment.NewLine + "       ) as SIMSADTM ";
                    sql += Environment.NewLine + "  FROM TA04 a04 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = a04.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = a04.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a04.QLFYCD ";
                    sql += Environment.NewLine + "     , TT09 t09 (nolock)";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock)";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock)";
                    sql += Environment.NewLine + "     , TT14 t14 (nolock)";
                    sql += Environment.NewLine + "     , TT56 t56 (nolock)";
                    sql += Environment.NewLine + "     , TA04A a04a (nolock) ";
                    sql += Environment.NewLine + " WHERE ISNULL(a04.BEDODT,'') = ''";
                    sql += Environment.NewLine + "   AND LEFT(a04.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.WARDID,'') <> 'ER1' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.BEDODIV, '') IN ('', '0') ";
                    sql += Environment.NewLine + "   AND t09.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND t09.BDEDT = a04.BEDEDT ";
                    sql += Environment.NewLine + "   AND a01.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a04a.pid = a04.pid ";
                    sql += Environment.NewLine + "   AND a04a.bededt = a04.bededt ";
                    sql += Environment.NewLine + "   AND a04a.startdt = (select max(startdt) from ta04a a where a.pid = a04a.pid and a.bededt = a04a.bededt) ";
                    sql += Environment.NewLine + "   AND t56.pid = a04.pid ";
                    sql += Environment.NewLine + "   AND t56.bdedt = a04.bededt ";
                    sql += Environment.NewLine + "   AND t56.sttdt = a04a.startdt ";
                    sql += Environment.NewLine + "   AND ISNULL(t56.SIMSAE,'') <> '' ";
                    sql += Environment.NewLine + "   AND t14.pid = a04.pid ";
                    sql += Environment.NewLine + "   AND t14.bededt = a04.bededt ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.DPTCD = ? ";
                    }
                    if (ward != "")
                    {
                        sql += Environment.NewLine + "   AND a04.WARDID = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt6.Checked)
                {
                    // 당일퇴원자
                    sql = "";
                    sql += Environment.NewLine + "SELECT a04.PID";
                    sql += Environment.NewLine + "     , a01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , a04.WARDID+'/'+a04.RMID+'/'+a04.BEDID AS WARD";
                    sql += Environment.NewLine + "     , a09.DPTNM";
                    sql += Environment.NewLine + "     , a07.DRNM";
                    sql += Environment.NewLine + "     , a04.BEDEDT";
                    sql += Environment.NewLine + "     , a88.CDNM QFYNM";
                    sql += Environment.NewLine + "     , a01.HTELNO";
                    sql += Environment.NewLine + "     , DATEDIFF(d,a04.BEDEDT, CASE WHEN ISNULL(a04.BEDODT,'') = '' THEN Getdate() ELSE a04.BEDODT END)+1 ILSU ";
                    sql += Environment.NewLine + "     , (select isnull(replace(left(case when t56.simsa>t56.simsae then t56.simsa  else t56.simsae end,13),'$',' '),'')";
                    sql += Environment.NewLine + "          from tt56 t56";
                    sql += Environment.NewLine + "         Where t56.PID = a04.PID";
                    sql += Environment.NewLine + "           and t56.bdedt=a04.bededt";
                    sql += Environment.NewLine + "           and t56.sttdt=(select max(x.sttdt) from tt56 x where x.pid=t56.pid and x.bdedt=t56.bdedt)";
                    sql += Environment.NewLine + "       ) as SIMSADTM ";
                    sql += Environment.NewLine + "  FROM TA04 a04 (nolock) LEFT OUTER JOIN TA07 a07 (nolock) ON a07.DRID = a04.PDRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 a09 (nolock) ON a09.DPTCD = a04.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 a88 (nolock) ON a88.MST1CD='A' AND a88.MST2CD='26' AND a88.MST3CD=a04.QLFYCD ";
                    sql += Environment.NewLine + "     , TA01 a01 (nolock)";
                    sql += Environment.NewLine + "     , TA10 a10 (nolock) ";
                    sql += Environment.NewLine + " WHERE ISNULL(a04.BEDODT,'') = CONVERT(VARCHAR,GETDATE(),112) ";
                    sql += Environment.NewLine + "   AND LEFT(a04.PID, 1) <> 'T' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.WARDID,'') <> 'ER1' ";
                    sql += Environment.NewLine + "   AND ISNULL(a04.BEDODIV, '') NOT IN ('', '0') ";
                    sql += Environment.NewLine + "   AND a01.PID = a04.PID ";
                    sql += Environment.NewLine + "   AND a10.PID = a04.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.DPTCD = ? ";
                    }
                    if (ward != "")
                    {
                        sql += Environment.NewLine + "   AND a04.WARDID = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a04.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }
                else if (rbPtnt7.Checked)
                {
                    if (pnm.Length < 2)
                    {
                        MessageBox.Show("환자명을 2자 이상 입력하세요.");
                        return;
                    }

                    ward=""; // 병동 없음.

                    // 전체환자
                    sql = "";
                    sql += Environment.NewLine + "SELECT TOP 100 A01.PID";
                    sql += Environment.NewLine + "     , A01.PNM";
                    sql += Environment.NewLine + "     , a01.RESID";
                    sql += Environment.NewLine + "     , a01.MYPIN";
                    sql += Environment.NewLine + "     , ISNULL(a01.PSEX,'')+'/'+(CASE WHEN ISDATE(a01.BTHDT)=1 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,a01.BTHDT,GETDATE())-DATEDIFF(YEAR,a01.BTHDT,GETDATE())/4)/365) ELSE '' END) AS SAGE";
                    sql += Environment.NewLine + "     , '' WARD";
                    sql += Environment.NewLine + "     , A09.DPTNM";
                    sql += Environment.NewLine + "     , A07.DRNM";
                    sql += Environment.NewLine + "     , (SELECT MAX(BEDEDT) FROM TA04 A (nolock) WHERE A.PID = A01.PID AND ISNULL(A.BEDIPTHCD,'') NOT IN ('0','7','8','9')) BEDEDT";
                    sql += Environment.NewLine + "     , A88.CDNM AS QFYNM";
                    sql += Environment.NewLine + "     , A01.HTELNO";
                    sql += Environment.NewLine + "     , '' ILSU";
                    sql += Environment.NewLine + "     , '' SIMSADTM";
                    sql += Environment.NewLine + "  FROM TA01 A01 (nolock) ";
                    sql += Environment.NewLine + "     , TA10 A10 (nolock) LEFT OUTER JOIN TA07 A07 (nolock) ON A07.DRID = A10.DRID ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA09 A09 (nolock) ON A09.DPTCD = A10.DPTCD ";
                    sql += Environment.NewLine + "                         LEFT OUTER JOIN TA88 A88 (nolock) ON A88.MST1CD='A' AND A88.MST2CD='26' AND A88.MST3CD=a10.QLFYCD ";
                    sql += Environment.NewLine + " WHERE A10.PID = A01.PID ";
                    if (pnm != "")
                    {
                        sql += Environment.NewLine + "   AND a01.PNM LIKE ? ";
                    }
                    if (dptcd != "")
                    {
                        sql += Environment.NewLine + "   AND a10.DPTCD = ? ";
                    }
                    if (qfycd != "")
                    {
                        sql += Environment.NewLine + "   AND a10.QLFYCD = ? ";
                    }
                    if (mypin != "")
                    {
                        sql += Environment.NewLine + "   AND a01.MYPIN LIKE ? ";
                    }
                }

                if (orderByFg == "4")
                {
                    sql += Environment.NewLine + " ORDER BY a10.LSTDT DESC, a01.PNM, a01.RESID ";
                }
                else
                {
                    sql += Environment.NewLine + " ORDER BY a01.PNM ";
                }

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    if (pnm != "") cmd.Parameters.Add(new OleDbParameter("@", pnm + "%"));
                    if (dptcd != "") cmd.Parameters.Add(new OleDbParameter("@", dptcd));
                    if (ward != "") cmd.Parameters.Add(new OleDbParameter("@", ward));
                    if (qfycd != "") cmd.Parameters.Add(new OleDbParameter("@", qfycd));
                    if (mypin != "") cmd.Parameters.Add(new OleDbParameter("@", mypin + "%"));

                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CPtnt data = new CPtnt();
                        data.PID = reader["PID"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.RESID = reader["RESID"].ToString();
                        data.MYPIN = reader["MYPIN"].ToString();
                        data.SAGE = reader["SAGE"].ToString();
                        data.WARD = reader["WARD"].ToString();
                        data.DPTNM = reader["DPTNM"].ToString();
                        data.PDRNM = reader["DRNM"].ToString();
                        data.BEDEDT = reader["BEDEDT"].ToString();
                        data.QFYNM = reader["QFYNM"].ToString();
                        if (simsafg == "1" && m_Prjcd.ToUpper() == "ADF")
                        {
                            data.HTELNO = reader["SIMSADTM"].ToString();
                        }
                        else
                        {
                            data.HTELNO = reader["HTELNO"].ToString();
                        }

                        list.Add(data);

                    }
                    reader.Close();
                }

                WritePIICLog(m_User, m_Prjcd, this.Name, "열람", pnm + "," + dptcd + "," + ward + "," + qfycd + "," + mypin, list.Count.ToString(), "Query", conn);

                conn.Close();
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            SelectPatient();
        }

        private void grdMainView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SelectPatient();
            }
        }

        private void SelectPatient()
        {
            string pid = grdMainView.GetRowCellValue(grdMainView.GetSelectedRows()[0], gcPID).ToString();
            if (pid == "") return;
            m_pid = pid;
            this.Close();
        }

        private void rbPtnt1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt1.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt2.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt3.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt4.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt5.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt6_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPtnt6.Checked) btnQuery.PerformClick();
        }

        private void rbPtnt7_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbPtnt7.Checked) btnQuery.PerformClick();
        }

        private void WritePIICLog(string usrid, string prjid, string frmnm, string job, string ippm, string oprv, string remark, OleDbConnection p_conn)
        {
            string seq = "";
            string hostip = "";
            string hostnm = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(hostnm);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    hostip = ip.ToString();
                    break;
                }
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ),0) + 1 NEXT_SEQ";
            sql = sql + Environment.NewLine + "  FROM TA94C_PIICL";
            sql = sql + Environment.NewLine + " WHERE USRIP=?";
            sql = sql + Environment.NewLine + "   AND USRID=?";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) seq = reader["NEXT_SEQ"].ToString();
                reader.Close();
            }

            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TA94C_PIICL(USRIP, USRID, SEQ, PRJID, FRMNM, JOB, IPPM, OPRV, REMARK, HSTNM, ENTDT, ENTTM)";
            sql = sql + Environment.NewLine + "VALUES";
            sql = sql + Environment.NewLine + "(?,?,?,?,?,?,?,?,?,?";
            sql = sql + Environment.NewLine + ",CONVERT(VARCHAR,GETDATE(),112),LEFT(REPLACE(CONVERT(VARCHAR,GETDATE(),14),':',''),6))";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));
                cmd.Parameters.Add(new OleDbParameter("@3", seq));
                cmd.Parameters.Add(new OleDbParameter("@4", prjid));
                cmd.Parameters.Add(new OleDbParameter("@5", frmnm));
                cmd.Parameters.Add(new OleDbParameter("@6", job));
                cmd.Parameters.Add(new OleDbParameter("@7", ippm));
                cmd.Parameters.Add(new OleDbParameter("@8", oprv));
                cmd.Parameters.Add(new OleDbParameter("@9", remark));
                cmd.Parameters.Add(new OleDbParameter("@10", hostnm));

                cmd.ExecuteNonQuery();
            }
        }

    }
}

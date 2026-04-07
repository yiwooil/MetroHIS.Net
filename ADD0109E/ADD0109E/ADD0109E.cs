using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0109E
{
    public partial class ADD0109E : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD0109E()
        {
            InitializeComponent();
        }

        public ADD0109E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD0109E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0109E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                this.Init();
                this.ShowColumn("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string hdate = MetroLib.Util.GetSysDate(conn);
                txtFdate.Text = hdate.Substring(0, 6) + "01";
                txtTdate.Text = hdate;

                string sql = "";

                // 환자자격
                cboQfycd.Items.Clear();
                cboQfycd.Items.Add("");
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='26' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboQfycd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                        }
                        reader.Close();
                    }
                }
                cboQfycd.SelectedIndex = 0;

                // 수가코드리스트
                sql = "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='175'";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) txtPricd.Text = reader["FLD2QTY"].ToString();
                        reader.Close();
                    }
                }
                
                conn.Close();
            }

            // 진료분야
            cboPrimdptcd.Items.Clear();
            cboPrimdptcd.Items.Add("");
            cboPrimdptcd.Items.Add("1 내과");
            cboPrimdptcd.Items.Add("2 외과");
            cboPrimdptcd.Items.Add("3 산소");
            cboPrimdptcd.Items.Add("4 안이");
            cboPrimdptcd.Items.Add("5 피비");
            cboPrimdptcd.Items.Add("6 치과");
            cboPrimdptcd.SelectedIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (tabJobdiv.SelectedIndex == 0)
            {
                // 외래
                string frdt = txtFdate.Text.ToString();
                string todt = txtTdate.Text.ToString();
                if (frdt == "")
                {
                    MessageBox.Show("진료기간의 시작일을 입력하세요.");
                    return;
                }
                if (todt == "")
                {
                    MessageBox.Show("진료기간의 종료일을 입력하세요.");
                    return;
                }
                if (MetroLib.Util.ValDt(frdt) == false)
                {
                    MessageBox.Show("진료기간의 시작일을 확인하세요.");
                    return;
                }
                if (MetroLib.Util.ValDt(todt) == false)
                {
                    MessageBox.Show("진료기간의 종료일을 확인하세요.");
                    return;
                }
            }
            else if (tabJobdiv.SelectedIndex == 1)
            {
                // 재원환자전체

            }
            else if (tabJobdiv.SelectedIndex == 2)
            {
                // 특정환자
                string pid = txtPid.Text.ToString();
                string bededt = txtBededt.Text.ToString();
                if (pid == "")
                {
                    MessageBox.Show("환자ID를 입력하세요.");
                    return;
                }
                if (bededt == "")
                {
                    MessageBox.Show("입원일을 입력하세요.");
                    return;
                }
            }
            else if (tabJobdiv.SelectedIndex == 3)
            {
                // 퇴원환자전체(기간)
                string frdt = txtFbdodt.Text.ToString();
                string todt = txtTbdodt.Text.ToString();
                if (frdt == "")
                {
                    MessageBox.Show("퇴원기간의 시작일을 입력하세요.");
                    return;
                }
                if (todt == "")
                {
                    MessageBox.Show("퇴원기간의 종료일을 입력하세요.");
                    return;
                }
                if (MetroLib.Util.ValDt(frdt) == false)
                {
                    MessageBox.Show("퇴원기간의 시작일을 확인하세요.");
                    return;
                }
                if (MetroLib.Util.ValDt(todt) == false)
                {
                    MessageBox.Show("퇴원기간의 종료일을 확인하세요.");
                    return;
                }

            }

            //////////////////////////////////////////////////////////////////////////////////////
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

                if (tabJobdiv.SelectedIndex == 0)
                {
                    // 외래
                    QueryOut(conn);
                }
                else
                {
                    QueryIn(conn, tabJobdiv.SelectedIndex.ToString());
                }

                conn.Close();

            }

            //// 순번
            //for (int row = 0; row < list.Count; row++)
            //{
            //    list[row].NO = row + 1;
            //}

            this.RefreshGridMain();

        }

        private void QueryOut(OleDbConnection p_conn)
        {
            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            // 외래
            string frdt = txtFdate.Text.ToString();
            string todt = txtTdate.Text.ToString();
            string qfycd = (cboQfycd.SelectedIndex == 0 ? "" : cboQfycd.SelectedItem.ToString().Split(' ')[0]);
            string primdptcd = (cboPrimdptcd.SelectedIndex == 0 ? "" : cboPrimdptcd.SelectedItem.ToString().Split(' ')[0]);
            string pricd = txtPricd.Text.ToString();
            Dictionary<string, string> dicPricd = new Dictionary<string, string>();
            if (pricd != "")
            {
                string[] pricd_arr = (pricd + ",").Split(',');
                foreach (string str in pricd_arr)
                {
                    if (str != "")
                    {
                        dicPricd.Add(str, "");
                    }
                }
            }

            bool ocheckfg = chkOCheckfg.Checked;
            bool qcheckfg = chkQCheckfg.Checked;
            bool rcheckfg = chkRCheckfg.Checked;
            bool lcheckfg = chkLCheckfg.Checked;

            bool noexecfg =chkNoExecfg.Checked;

            string sql="";
            sql="";
            sql += Environment.NewLine + "SELECT S31.PID";
            sql += Environment.NewLine + "     , A01.PNM";
            sql += Environment.NewLine + "     , S31.EXDT";
            sql += Environment.NewLine + "     , S31.DPTCD";
            sql += Environment.NewLine + "     , S31.QFYCD";
            sql += Environment.NewLine + "     , S31.PRICD";
            sql += Environment.NewLine + "     , S31.CHRLT";
            sql += Environment.NewLine + "     , S31.CALQY";
            sql += Environment.NewLine + "     , S31.DDAY";
            sql += Environment.NewLine + "     , S31.GRPCD";
            sql += Environment.NewLine + "     , S31.FRFG";
            sql += Environment.NewLine + "     , S31.KYSTR";
            sql += Environment.NewLine + "     , S31.BDIV";
            sql += Environment.NewLine + "     , A01.PNM";
            sql += Environment.NewLine + "     , S41.PDIV";
            sql += Environment.NewLine + "     , S41.GONSGB";
            sql += Environment.NewLine + "     , S41.DAETC";
            sql += Environment.NewLine + "     , S41.FINDT";
            sql += Environment.NewLine + "     , A88.CDNM AS PDIVNM";
            sql += Environment.NewLine + "     , A02.PRKNM ";
            sql += Environment.NewLine + "     , LEFT(S31.ODINC,1) AS ODINC1 ";
            sql += Environment.NewLine + "     , A07.DRNM ";
            sql += Environment.NewLine + "  FROM TS31 S31 WITH (NOLOCK) ";
            sql += Environment.NewLine + "                INNER JOIN TA01 A01 WITH (NOLOCK) ON A01.PID=S31.PID ";
            sql += Environment.NewLine + "                INNER JOIN TS41 S41 WITH (NOLOCK) ON S41.PID=S31.PID AND S41.ENTDT=S31.ENTDT AND S41.BDIV=S31.BDIV AND S41.GRPNO=S31.GRPNO AND S41.SEQ=0 ";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 WITH (NOLOCK) ON A02.PRICD=S31.PRICD AND A02.CREDT=(SELECT MAX(Z.CREDT) FROM TA02 Z WITH (NOLOCK) WHERE Z.PRICD=S31.PRICD AND Z.CREDT<=S31.EXDT) ";
            sql += Environment.NewLine + "                INNER JOIN TA09 A09 WITH (NOLOCK) ON A09.DPTCD=S31.DPTCD ";
            sql += Environment.NewLine + "                LEFT  JOIN TA88 A88 WITH (NOLOCK) ON A88.MST1CD='A' AND A88.MST2CD='23' AND A88.MST3CD=S41.PDIV ";
            sql += Environment.NewLine + "                INNER JOIN TA07 A07 WITH (NOLOCK) ON A07.DRID=S31.DRID";
            sql += Environment.NewLine + " WHERE S31.EXDT>='" + frdt + "' ";
            sql += Environment.NewLine + "   AND S31.EXDT<='" + todt + "' ";
            sql += Environment.NewLine + "   AND S31.KYSTR<>'' ";
            sql += Environment.NewLine + "   AND S31.RCVFG='+' ";
            sql += Environment.NewLine + "   AND LEFT(S31.ODINC,1) IN ('Q','R','L','P','O') ";
            sql += Environment.NewLine + "   AND S31.PRICD NOT IN ('AAAAC') ";
            sql += Environment.NewLine + "   AND LEFT(S31.PRICD,1) NOT IN ('M','G') "; // 약,재료 제외
            sql += Environment.NewLine + "   AND LEFT(S31.ACTFG,1) NOT IN ('3','4') "; // 약,재료 제외
            sql += Environment.NewLine + "   AND S41.RECFG NOT IN ('F','D','O') "; // 환불 제외
            sql += Environment.NewLine + "   AND NOT (S41.BDIV='3' AND S41.PDIV='3') "; //응급6시간이상자 제외
            sql += Environment.NewLine + "   AND NOT (S41.BDIV='1' AND S41.PDIV='6') "; //정신과낮병동 제외
            sql += Environment.NewLine + "   AND NOT (S41.PDIV='D')                  "; //통원수술 제외
            if (noexecfg)
            {
                sql += Environment.NewLine + "   AND S31.PRICD NOT IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='NOEXEC') "; // 실시하지 않는 코드 제외
                sql += Environment.NewLine + "   AND (   ISNULL(S31.GRPCD,'')='' ";
                sql += Environment.NewLine + "        OR ISNULL(S31.GRPCD,'') NOT IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='NOEXEC') "; // 실시하지 않는 코드 제외
                sql += Environment.NewLine + "       )";
                sql += Environment.NewLine + "";
            }
            if (qfycd != "")
            {
                sql += Environment.NewLine + "   AND S41.QFYCD = '" + qfycd + "' ";
            }
            else
            {
                sql += Environment.NewLine + "   AND LEFT(S41.QFYCD,1) IN ('2','3','4','5','6','7','8','9') ";
            }
            if (primdptcd != "")
            {
                sql += Environment.NewLine + "   AND A09.PRIMDPTCD = '" + primdptcd + "' ";
            }
            sql += Environment.NewLine + " ORDER BY S31.PID,S31.EXDT";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                // cmd.Parameters를 사용하니 너무 느림.
                // 위 문장처럼 sql문을 만드는 것이 훨씨 더 빠르다.

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string odinc1 = reader["ODINC1"].ToString();

                        if (odinc1 == "Q" && qcheckfg == false) continue; // 기능검사
                        if (odinc1 == "R" && rcheckfg == false) continue; // 방사선
                        if (odinc1 == "L" && lcheckfg == false) continue; // 임상병리검사
                        if (odinc1 == "P" && lcheckfg == false) continue; // 임상병리검사
                        if (odinc1 == "O" && ocheckfg == false) continue; // 물리치료

                        if (dicPricd.ContainsKey(reader["PRICD"].ToString())) continue; // 제외코드


                        CData data = new CData();
                        data.Clear();
                        data.PID = reader["PID"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.EXDT = reader["EXDT"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.DRNM = reader["DRNM"].ToString();
                        data.BDIV = reader["BDIV"].ToString();
                        data.QFYCD = reader["QFYCD"].ToString();
                        data.PDIV = reader["PDIV"].ToString();
                        data.PDIVNM = reader["PDIVNM"].ToString();
                        data.GONSGB = reader["GONSGB"].ToString();
                        data.DAETC = reader["DAETC"].ToString();
                        data.PRICD = reader["PRICD"].ToString();
                        data.PRKNM = reader["PRKNM"].ToString();
                        data.CHRLT = reader["CHRLT"].ToString();
                        float calqy = 0; float.TryParse(reader["CALQY"].ToString(), out calqy);
                        data.CALQY = calqy;
                        int dday = 0; int.TryParse(reader["DDAY"].ToString(), out dday);
                        data.DDAY = dday;
                        data.FINDT = reader["FINDT"].ToString();
                        data.ODT = ""; // TS31에 없음.
                        data.ONO = "";
                        data.GRPCD = reader["GRPCD"].ToString();

                        data.FRFG = reader["FRFG"].ToString();
                        data.KYSTR = reader["KYSTR"].ToString();
                        data.ODINC1 = reader["ODINC1"].ToString();
                        data.DCFG = "";

                        list.Add(data);
                    }

                    reader.Close();
                }
            }
            this.RefreshGridMain();

            // 처방상태를 읽는다.
            for (int idx = list.Count - 1; idx >= 0; idx--)
            {
                CData data = list[idx];

                string ostscd = "";
                string odt = "";
                string ono = "";
                string bededt = "";
                string dcfg = "";

                if (data.BDIV == "1")
                {
                    if (data.ODINC1 == "O")
                    {
                        //물리치료인경우 물리치료실시내역(TU10)을 읽어야한다.
                        ostscd = GetTU10Ostscd(data.KYSTR, out odt, out ono, p_conn);
                    }
                    else if (data.FRFG == "C" || data.FRFG == "M" || data.FRFG == "N" || data.FRFG == "O")
                    {
                        //2016.12.05 WOOIL - 실시내역에서 행위도출된 내역임
                        ostscd = GetTV20Ostscd(data.KYSTR, data.FRFG, out odt, out ono, p_conn);
                        // 입원등록일
                        string[] keystr_arr = data.KYSTR.Split(',');
                        if (keystr_arr.Length == 5) bededt = keystr_arr[1];
                    }
                    else
                    {
                        // 2016.06.07 KJW - Keystring이 5개면 입원내역에서 읽는다.
                        string[] keystr_arr = data.KYSTR.Split(',');
                        if (keystr_arr.Length == 5)
                        {
                            ostscd = GetTV61Ostscd(data.KYSTR, out odt, out ono, p_conn);
                            // 입원등록일
                            bededt = keystr_arr[1];
                        }
                        else
                        {
                            ostscd = GetTE62Ostscd(data.KYSTR, out odt, out ono, p_conn);
                        }
                    }
                }
                else
                {
                    if (data.ODINC1 == "O")
                    {
                        if (data.BDIV == "3")
                        {
                            //물리치료인경우 물리치료실시내역(TU10)을 읽어야한다.
                            ostscd = GetTU10Ostscd(data.KYSTR, out odt, out ono, p_conn);
                        }
                        else
                        {
                            //병동환자는 실시한것으로간주함.
                            ostscd = "C";
                        }
                    }
                    else
                    {
                        ostscd = GetTV61Ostscd(data.KYSTR, out odt, out ono, p_conn);
                        // 입원등록일
                        string[] keystr_arr = data.KYSTR.Split(',');
                        if (keystr_arr.Length == 5) bededt = keystr_arr[1];
                    }
                }

                // 처방상태가 A, B만 남긴다.
                if (ostscd != "A" && ostscd != "B")
                {
                    // 실시된 내역임. 삭제.
                    list.RemoveAt(idx);
                }
                else
                {
                    // 추가로 필요한 정보를 ...
                    data.ODT = odt;
                    data.ONO = ono;
                    if (bededt != "")
                    {
                        dcfg = GetDCfg_TV01(data.PID, bededt, odt, ono, p_conn);
                    }
                    else
                    {
                        dcfg = GetDCfg(data.PID, odt, ono, p_conn);
                    }
                    data.DCFG = dcfg;
                    if (data.FINDT == data.EXDT) data.FINDT = "";
                    if (data.ODT == data.EXDT) data.ODT = "";
                }

            }

            this.RefreshGridMain();
        }

        private string GetTU10Ostscd(string p_kystr, out string p_odt, out string p_ono, OleDbConnection p_conn)
        {
            p_odt = "";
            p_ono = "";

            // KYSTR이 없으면
            if (p_kystr == "") return "";
            // TU10 읽기시작
            string[] kystr_arr=p_kystr.Split(',');
            string pid = kystr_arr[0];
            string rsvdt = kystr_arr[1];
            string rsvhr = kystr_arr[2];
            string rsvmn = kystr_arr[3];
            string ocd = kystr_arr[4];
            string dosts = "";
            string chgdt = "";
            int cnt = 0;
            string sql="";
            sql = "";
            sql += Environment.NewLine + "SELECT DOSTS,CHGDT,ODT,ONO ";
            sql += Environment.NewLine + "  FROM TU10 ";
            sql += Environment.NewLine + " WHERE PID='" + pid + "'";
            sql += Environment.NewLine + "   AND RSVDT='" + rsvdt + "'";
            sql += Environment.NewLine + "   AND RSVHR='" + rsvhr + "'";
            sql += Environment.NewLine + "   AND RSVMN='" + rsvmn + "'";
            sql += Environment.NewLine + "   AND OCD='" + ocd + "'";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cnt = 1;
                        dosts = reader["DOSTS"].ToString();
                        chgdt = reader["CHGDT"].ToString();
                        p_odt = reader["ODT"].ToString();
                        p_ono = reader["ONO"].ToString();
                    }
                    else
                    {
                        cnt = 0;
                    }
                    reader.Close();
                }
            }
            if (cnt <= 0) return "";
            if (dosts == "Y") return "C";
            if (dosts == "") return "";
            if (dosts == "D/C" && chgdt == "D/C") return "";
            // 이하 예약변경임.혹시나해서 100번으로 제한한다.
            for (int i = 0; i < 100; i++)
            {
                string[] chgdt_arr = chgdt.Split('$');
                pid = chgdt_arr[0];
                rsvdt = chgdt_arr[1];
                rsvhr = chgdt_arr[2];
                rsvmn = chgdt_arr[3];
                ocd = chgdt_arr[4];
                //
                sql = "";
                sql += Environment.NewLine + "SELECT DOSTS,CHGDT,ODT,ONO ";
                sql += Environment.NewLine + "  FROM TU10 ";
                sql += Environment.NewLine + " WHERE PID='" + pid + "'";
                sql += Environment.NewLine + "   AND RSVDT='" + rsvdt + "'";
                sql += Environment.NewLine + "   AND RSVHR='" + rsvhr + "'";
                sql += Environment.NewLine + "   AND RSVMN='" + rsvmn + "'";
                sql += Environment.NewLine + "   AND OCD='" + ocd + "'";
                //
                using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cnt = 1;
                            dosts = reader["DOSTS"].ToString();
                            chgdt = reader["CHGDT"].ToString();
                            p_odt = reader["ODT"].ToString();
                            p_ono = reader["ONO"].ToString();
                        }
                        else
                        {
                            cnt = 0;
                        }
                        reader.Close();
                    }
                    if (cnt <= 0) return "";
                    if (dosts == "Y") return "C";
                    if (dosts == "") return "";
                    if (dosts == "D/C" && chgdt == "D/C") return "";
                }
            }

            return "";
        }

        private string GetTV20Ostscd(string p_kystr,string p_frfg, out string p_odt, out string p_ono, OleDbConnection p_conn)
        {
            p_odt = "";
            p_ono = "";

            // KYSTR이 없으면
            if (p_kystr == "") return "";
            //
            string tTV20 = "TV20";
            if (p_frfg == "M") tTV20 = "TV20R";
            else if (p_frfg == "N") tTV20 = "TV20Q";
            else if (p_frfg == "O") tTV20 = "TV20L";

            string[] kystr_arr = p_kystr.Split(',');
            string pid = kystr_arr[0];
            string bededt = kystr_arr[1];
            string bdiv = kystr_arr[2];
            string no = kystr_arr[3];
            string dstscd = "";

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT DSTSCD,ODT,ONO ";
            sql += Environment.NewLine + "  FROM " + tTV20 + " ";
            sql += Environment.NewLine + " WHERE PID='" + pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + bededt + "'";
            sql += Environment.NewLine + "   AND BDIV='" + bdiv + "'";
            sql += Environment.NewLine + "   AND NO='" + no + "'";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dstscd = reader["DSTSCD"].ToString();
                        p_odt = reader["ODT"].ToString();
                        p_ono = reader["ONO"].ToString();
                    }
                    reader.Close();
                }
            }

            return (dstscd == "Y" ? "C" : "");
        }

        private string GetTV61Ostscd(string p_kystr, out string p_odt, out string p_ono, OleDbConnection p_conn)
        {
            p_odt = "";
            p_ono = "";

            // KYSTR이 없으면
            if (p_kystr == "") return "";
            //
            string[] kystr_arr = p_kystr.Split(',');
            string pid = kystr_arr[0];
            string bededt = kystr_arr[1];
            string bdiv = kystr_arr[2];
            string odt = kystr_arr[3];
            string ono = kystr_arr[4];
            string ostscd="";
            //
            p_odt = odt;
            p_ono = ono;
            //
            string sql="";
            sql="";
            sql += Environment.NewLine + "SELECT V61.OSTSCD ";
            sql += Environment.NewLine + "  FROM TV01 V01 WITH (NOLOCK) INNER JOIN TV61 V61 WITH (NOLOCK) ON V61.HDID=V01.HDID ";
            sql += Environment.NewLine + " WHERE V01.PID='" + pid + "' ";
            sql += Environment.NewLine + "   AND V01.BEDEDT='" + bededt + "' ";
            sql += Environment.NewLine + "   AND V01.BDIV='" + bdiv + "' ";
            sql += Environment.NewLine + "   AND V01.ODT='" + odt + "' ";
            sql += Environment.NewLine + "   AND V01.ONO='" + ono + "' ";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ostscd = reader["OSTSCD"].ToString();
                    }
                    reader.Close();
                }
            }

            return ostscd;
        }

        private string GetTE62Ostscd(string p_kystr, out string p_odt, out string p_ono, OleDbConnection p_conn)
        {
            p_odt = "";
            p_ono = "";

            // KYSTR이 없으면
            if (p_kystr == "") return "";
            //
            string[] kystr_arr = p_kystr.Split(',');
            string pid = kystr_arr[0];
            string odt = kystr_arr[1];
            string ono = kystr_arr[2];
            string ostscd = "";
            //
            p_odt = odt;
            p_ono = ono;
            //
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT E62.OSTSCD ";
            sql += Environment.NewLine + "  FROM TE01 E01 WITH (NOLOCK) INNER JOIN TE62 E62 WITH (NOLOCK) ON E62.HDID=E01.HDID ";
            sql += Environment.NewLine + " WHERE E01.PID='" + pid + "' ";
            sql += Environment.NewLine + "   AND E01.ODT='" + odt + "' ";
            sql += Environment.NewLine + "   AND E01.ONO='" + ono + "' ";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ostscd = reader["OSTSCD"].ToString();
                    }
                    reader.Close();
                }
            }

            return ostscd;
        }

        private string GetDCfg(string p_pid, string p_odt, string p_ono, OleDbConnection p_conn)
        {
            string ret = "";
            if (p_pid == "" || p_odt == "" || p_ono == "") return ret;
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT DCFG ";
            sql += Environment.NewLine + "  FROM TE01 ";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "' ";
            sql += Environment.NewLine + "   AND ODT='" + p_odt + "' ";
            sql += Environment.NewLine + "   AND ONO='" + p_ono + "' ";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ret = reader["DCFG"].ToString();
                    }
                    reader.Close();
                }
            }

            return ret;
        }

        private string GetDCfg_TV01(string p_pid, string p_bededt, string p_odt, string p_ono, OleDbConnection p_conn)
        {
            string ret = "";
            if (p_pid == "" || p_bededt == "" || p_odt == "" || p_ono == "") return ret;
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT DCFG ";
            sql += Environment.NewLine + "  FROM TV01 ";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + p_bededt + "'";
            sql += Environment.NewLine + "   AND ODT='" + p_odt + "'";
            sql += Environment.NewLine + "   AND ONO='" + p_ono + "'";
            //
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ret = reader["DCFG"].ToString();
                    }
                    reader.Close();
                }
            }

            return ret;
        }

        private void QueryIn(OleDbConnection p_conn, string p_jobdiv)
        {
            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            // p_jobdiv = 1 : 재원환자전체
            //            2 : 특정환자
            //            3 : 퇴원환자전체

            string pricd = txtPricd.Text.ToString();
            Dictionary<string, string> dicPricd = new Dictionary<string, string>();
            if (pricd != "")
            {
                string[] pricd_arr = (pricd + ",").Split(',');
                foreach (string str in pricd_arr)
                {
                    if (str != "")
                    {
                        dicPricd.Add(str, "");
                    }
                }
            }

            bool ocheckfg = chkOCheckfg.Checked;
            bool qcheckfg = chkQCheckfg.Checked;
            bool rcheckfg = chkRCheckfg.Checked;
            bool lcheckfg = chkLCheckfg.Checked;

            bool noexecfg =chkNoExecfg.Checked;

            string sql="";
            sql = "";
            sql += Environment.NewLine + "SELECT T31.PID";
            sql += Environment.NewLine + "     , A01.PNM";
            sql += Environment.NewLine + "     , T31.BDEDT";
            sql += Environment.NewLine + "     , T31.EXDT";
            sql += Environment.NewLine + "     , T31.PRICD";
            sql += Environment.NewLine + "     , T31.CHRLT";
            sql += Environment.NewLine + "     , T31.CALQY";
            sql += Environment.NewLine + "     , T31.DDAY";
            sql += Environment.NewLine + "     , T31.GRPCD";
            sql += Environment.NewLine + "     , T31.FRFG";
            sql += Environment.NewLine + "     , T31.KYSTR";
            sql += Environment.NewLine + "     , A02.PRKNM";
            sql += Environment.NewLine + "     , LEFT(T31.ODINC,1) AS ODINC1";
            sql += Environment.NewLine + "  FROM TA04 A04 WITH (NOLOCK)";
            sql += Environment.NewLine + "                INNER JOIN TT31 T31 WITH (NOLOCK) ON T31.PID=A04.PID AND T31.BDEDT=A04.BEDEDT";
            sql += Environment.NewLine + "                INNER JOIN TA01 A01 WITH (NOLOCK) ON A01.PID=T31.PID";
            sql += Environment.NewLine + "                INNER JOIN TA02 A02 WITH (NOLOCK) ON A02.PRICD=T31.PRICD AND A02.CREDT=(SELECT MAX(Z.CREDT) FROM TA02 Z WITH (NOLOCK) WHERE Z.PRICD=T31.PRICD AND Z.CREDT<=T31.EXDT) ";

            if (p_jobdiv == "1")
            {

                sql += Environment.NewLine + " WHERE ISNULL(A04.BEDODIV,'') IN ('','0','2')";
                sql += Environment.NewLine + "   AND A04.PID NOT LIKE 'T%'";

            }
            else if (p_jobdiv == "2")
            {
                string pid = txtPid.Text.ToString();
                string bededt = txtBededt.Text.ToString();
                sql += Environment.NewLine + " WHERE A04.PID='" + pid + "'";
                sql += Environment.NewLine + "   AND A04.BEDEDT='" + bededt + "'";

            }
            else if (p_jobdiv == "3")
            {
                string frdt = txtFbdodt.Text.ToString();
                string todt = txtTbdodt.Text.ToString();
                // 2014.11.13 KJW - 퇴원일자로 조회할 수 있도록 추가함.
                sql += Environment.NewLine + " WHERE A04.BEDODT BETWEEN '" + frdt + "' AND '" + todt + "' ";
                sql += Environment.NewLine + "   AND A04.PID NOT LIKE 'T%'";
            }
            
            sql += Environment.NewLine + "   AND A04.WARDID<>'ER1'";
            sql += Environment.NewLine + "   AND LEFT(T31.ODINC,1) IN ('Q','R','L','P','O')";
            sql += Environment.NewLine + "   AND LEFT(T31.PRICD,1) NOT IN ('M','G')";
            sql += Environment.NewLine + "   AND LEFT(T31.ACTFG,1) NOT IN ('3','4')";
            sql += Environment.NewLine + "   AND ISNULL(T31.KYSTR,'')<>''";
            sql += Environment.NewLine + "   AND T31.CALQY<>0";

            if (noexecfg)
            {
                sql += Environment.NewLine + "   AND T31.PRICD NOT IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='NOEXEC')";
                sql += Environment.NewLine + "   AND (   ISNULL(T31.GRPCD,'')=''";
                sql += Environment.NewLine + "        OR ISNULL(T31.GRPCD,'') NOT IN (SELECT MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='NOEXEC')";
                sql += Environment.NewLine + "       )";
            }
            
            sql += Environment.NewLine + " ORDER BY A04.PID,A04.BEDEDT,T31.EXDT";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string odinc1 = reader["ODINC1"].ToString();

                        if (odinc1 == "Q" && qcheckfg == false) continue; // 기능검사
                        if (odinc1 == "R" && rcheckfg == false) continue; // 방사선
                        if (odinc1 == "L" && lcheckfg == false) continue; // 임상병리검사
                        if (odinc1 == "P" && lcheckfg == false) continue; // 임상병리검사
                        if (odinc1 == "O") continue; // 물리치료. 물리치료는 실시해야 수납되므로 검사할 필요없다.

                        if (dicPricd.ContainsKey(reader["PRICD"].ToString())) continue; // 제외코드

                        CData data = new CData();
                        data.Clear();
                        data.PID = reader["PID"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.EXDT = reader["EXDT"].ToString();
                        data.BEDEDT = reader["BDEDT"].ToString();
                        data.PRICD = reader["PRICD"].ToString();
                        data.PRKNM = reader["PRKNM"].ToString();
                        data.CHRLT = reader["CHRLT"].ToString();
                        float calqy = 0; float.TryParse(reader["CALQY"].ToString(), out calqy);
                        data.CALQY = calqy;
                        int dday = 0; int.TryParse(reader["DDAY"].ToString(), out dday);
                        data.DDAY = dday;

                        data.ODT = ""; // TS31에 없음.
                        data.ONO = "";
                        data.GRPCD = reader["GRPCD"].ToString();

                        data.FRFG = reader["FRFG"].ToString();
                        data.KYSTR = reader["KYSTR"].ToString();
                        data.ODINC1 = reader["ODINC1"].ToString();
                        data.DCFG = "";

                        list.Add(data);

                    }
                    reader.Close();
                }
            }
            this.RefreshGridMain();

            // 처방상태를 읽는다.
            for (int idx = list.Count - 1; idx >= 0; idx--)
            {
                CData data = list[idx];

                string ostscd = "";
                string odt = "";
                string ono = "";
                string bededt = "";
                string dcfg = "";

                if (data.FRFG == "C" || data.FRFG == "M" || data.FRFG == "N" || data.FRFG == "O")
                {
                    ostscd = GetTV20Ostscd(data.KYSTR, data.ODINC1, out odt, out ono, p_conn);
                }
                else
                {
                    string[] keystr_arr = data.KYSTR.Split(',');
                    if (keystr_arr.Length == 5)
                    {
                        ostscd = GetTV61Ostscd(data.KYSTR, out odt, out ono, p_conn);
                        // 입원등록일
                        bededt = keystr_arr[1];
                    }
                    else
                    {
                        ostscd = GetTE62Ostscd(data.KYSTR, out odt, out ono, p_conn);
                    }


                }

                // 처방상태가 A, B만 남긴다.
                if (ostscd != "A" && ostscd != "B")
                {
                    // 실시된 내역임. 삭제.
                    list.RemoveAt(idx);
                }
                else
                {
                    // 추가로 필요한 정보를 ...
                    data.ODT = odt;
                    data.ONO = ono;
                    if (bededt != "")
                    {
                        dcfg = GetDCfg_TV01(data.PID, bededt, odt, ono, p_conn);
                    }
                    else
                    {
                        dcfg = GetDCfg(data.PID, odt, ono, p_conn);
                    }
                    data.DCFG = dcfg;
                }
            }

            this.RefreshGridMain();
        }

        private void ShowColumn(string p_iofg)
        {
            if (p_iofg == "1")
            {
                //외래
                gcBEDEDT.Visible = false;
                gcDPTCD.Visible = true;
                gcDRNM.Visible = true;
                gcERFG.Visible = true;
                gcQFYCD.Visible = true;
                gcPDIV.Visible = true;
                gcPDIVNM.Visible = true;
                gcGONSGB.Visible = true;
                gcDAETC.Visible = true;
                gcFINDT.Visible = true;
            }
            else
            {
                //입원
                gcBEDEDT.Visible = true;
                gcDPTCD.Visible = false;
                gcDRNM.Visible = false;
                gcERFG.Visible = false;
                gcQFYCD.Visible = false;
                gcPDIV.Visible = false;
                gcPDIVNM.Visible = false;
                gcGONSGB.Visible = false;
                gcDAETC.Visible = false;
                gcFINDT.Visible = false;
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

        private void tabJobdiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabJobdiv.SelectedIndex == 0)
            {
                this.ShowColumn("1");
            }
            else
            {
                this.ShowColumn("2");
            }
            this.grdMain.DataSource = null;
            this.RefreshGridMain();
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
            e.Graph.DrawString("미실시처방조회", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            string msg = "";
            if (tabJobdiv.SelectedIndex == 0) msg += "외래 : " + txtFdate.Text.ToString() + " ~ " + txtTdate.Text.ToString();
            if (tabJobdiv.SelectedIndex == 1) msg += "재원환자전체";
            if (tabJobdiv.SelectedIndex == 2) msg += "환자ID : " + txtPid.Text.ToString() + " 입원일자 : " + txtBededt.Text.ToString();
            if (tabJobdiv.SelectedIndex == 3) msg += "퇴원기간전체 : " + txtFbdodt.Text.ToString() + " ~ " + txtTbdodt.Text.ToString();
            //
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(msg, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
                prtTime = DateTime.Now.ToString("HHmmss");
            }
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0109E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(prtDate + " " + prtTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnPricd_Click(object sender, EventArgs e)
        {
            ADD0109E_1 f = new ADD0109E_1();
            f.ShowDialog(this);
            string pricd = f.m_pricd;
            f = null;

            if (pricd != "")
            {
                if(txtPricd.Text.ToString()=="") txtPricd.Text = pricd;
                else txtPricd.Text += "," + pricd;
            }
        }

        private void btnSavePricd_Click(object sender, EventArgs e)
        {
            try
            {
                SavePricd();
                MessageBox.Show("저장되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavePricd()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                int cnt = 0;
                sql = "SELECT COUNT(*) AS CNT FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='175'";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) int.TryParse(reader["CNT"].ToString(), out cnt);
                        reader.Close();
                    }
                }

                if (cnt < 1)
                {
                    sql = "INSERT INTO TI88(MST1CD,MST2CD,MST3CD,CDNM,FLD1QTY,FLD2QTY) VALUES('A','HOSPITAL','175','미실시처방조회시에외수가코드','콤마리스트임','" + txtPricd.Text.ToString() + "')";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    sql = "";
                    sql += Environment.NewLine + "UPDATE TI88 SET FLD2QTY='" + txtPricd.Text.ToString() + "'";
                    sql += Environment.NewLine + " WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='175'";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 선택된 셀을 반전시킨다.
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.IsCellSelected(e.RowHandle, e.Column))
            {
                e.Appearance.BackColor = view.PaintAppearance.FocusedRow.BackColor;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        //private void grdMainView_EndSorting(object sender, EventArgs e)
        //{
        //    // 특정 컬럼을 클릭했을 경우 줄번호를 다시 부여한다.
        //    for (int i = 0; i < grdMainView.RowCount; i++)
        //    {
        //        grdMainView.SetRowCellValue(i, gcNO, i + 1);
        //    }
        //}
    }
}

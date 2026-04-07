using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0112E
{
    public partial class ADD0112E : Form
    {
        private bool IsFirst;
        private bool OnPgm;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD0112E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD0112E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }
        private void ADD0112E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            OnPgm = false;
        }

        private void ADD0112E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                OnPgm = true;
                this.Init();
                OnPgm = false;
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

                string sql = "";

                // 직원구분
                cboEmpdiv.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='21' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboEmpdiv.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 환자정보
                cboPdesc.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='49' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboPdesc.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 환자자격
                cboQfycd.Items.Clear();
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

                // 환자구분
                cboPdiv.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='23' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboPdiv.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 가족관계
                cboFamrelcd.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='27' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboFamrelcd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 직업
                cboJobcd.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='JOBCD' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboJobcd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 종교
                cboRelicd.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='RELI' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboRelicd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 공상
                cboGonsgb.Items.Clear();
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='92' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboGonsgb.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                    reader.Close();
                }

                // 진료과
                cboDptcd.Items.Clear();
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

                // 의사
                cboDrid.Items.Clear();

                conn.Close();
            }
        }

        private void rbOPtnt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOPtnt.Checked)
            {
                lblBdodt.Text = "청구월 :";
                lblBdodtHelp.Text = "6자리로 입력하세요. 예) 200701";
                lblExdt.Text = "진료일자 :";
            }
        }

        private void rbIPtnt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIPtnt.Checked)
            {
                lblBdodtHelp.Text = "8자리로 입력하세요. 예) 20070101";
                lblBdodt.Text = "청구일 :";
                lblExdt.Text = "입원일자 :";
            }
        }

        private void txtPid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPid.Text.ToString().Trim() == "")
                {
                    ADD0112E_1 f = new ADD0112E_1();
                    f.m_User = m_User;
                    f.m_Prjcd = m_Prjcd;
                    f.ShowDialog(this);
                    if (f.m_pid != "") txtPid.Text = f.m_pid;
                    f = null;
                }
                txtExdt.Focus();
            }
        }

        private void txtPid_Leave(object sender, EventArgs e)
        {
            if (txtPid.Text.ToString() == "") return;
            if (txtPid.Text.ToString().Length < 9)
            {
                txtPid.Text = txtPid.Text.ToString().PadLeft(9, '0');

            }
            try
            {
                GetPatientInformation(txtPid.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPid_TextChanged(object sender, EventArgs e)
        {
            if (txtPid.Text.ToString().Length != 9)
            {
                ClearPinfo();
                ClearExInfo();
                ClearQfyInfo();
            }
        }

        private void GetPatientInformation(string p_pid)
        {

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                //GetA04(p_pid, conn);
                GetPInfo(p_pid, conn);
                GetExInfo(p_pid, conn);
                GetQfyInfo(p_pid, conn);
            }
        }

        //private void GetA04(string p_pid, OleDbConnection p_conn)
        //{
        //    string sql = "";
        //    sql = "";
        //    sql = sql + Environment.NewLine + "SELECT BEDEDT, WARDID, RMID, BEDID, BEDODT, BEDODIV, BEDIPTHCD ";
        //    sql = sql + Environment.NewLine + "  FROM TA04 A (nolock) ";
        //    sql = sql + Environment.NewLine + " WHERE PID = ? ";
        //    sql = sql + Environment.NewLine + "   AND BEDEDT = (SELECT MAX(B.BEDEDT) ";
        //    sql = sql + Environment.NewLine + "                   FROM TA04 B (nolock) ";
        //    sql = sql + Environment.NewLine + "                  WHERE B.PID = ? ) ";
        //    sql = sql + Environment.NewLine + "   AND ISNULL(BEDIPTHCD, '') NOT IN ('0','7', '8', '9') ";

        //    // TSQL문장과 Connection 객체를 지정   
        //    using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
        //    {
        //        cmd.Parameters.Add(new OleDbParameter("@1", p_pid));
        //        cmd.Parameters.Add(new OleDbParameter("@2", p_pid));

        //        // 데이타는 서버에서 가져오도록 실행
        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            string bedodiv = reader["BEDODIV"].ToString();
        //            string bedodt = reader["BEDODT"].ToString();
        //            string wardid = reader["WARDID"].ToString();
        //            string rmid = reader["RMID"].ToString();
        //            string bedid = reader["BEDID"].ToString();

        //            if (bedodiv == "1")
        //            {
        //                txtPidinfo.Text = "정상퇴원환자입니다.(" + bedodt + ")";
        //            }
        //            else if (bedodiv == "2")
        //            {
        //                txtPidinfo.Text = "정산전 가퇴원환자입니다.(" + bedodt + ")";
        //            }
        //            else if (bedodiv == "3")
        //            {
        //                txtPidinfo.Text = "정산후 가퇴원환자입니다.(" + bedodt + ")";
        //            }
        //            else if (bedodiv == "" || bedodiv == "0")
        //            {
        //                txtPidinfo.Text = "재원환자입니다.(" + wardid + "-" + rmid + "-" + bedid + ")";
        //            }
        //            else
        //            {
        //                txtPidinfo.Text = "입퇴원내역을 확인하세요.";
        //            }
        //        }
        //        else
        //        {
        //            txtPidinfo.Text = "입원력이 없습니다.";
        //        }
        //    }
        //}

        private void ClearPinfo()
        {
            txtPnm.Text = "";
            txtResid.Text = "";
            txtZipcd.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            cboEmpdiv.SelectedIndex = -1;
            txtTelno.Text = "";
            txtPidinfo.Text = "";
            txtEmail.Text = "";
            cboPdesc.SelectedIndex = -1;
            txtOtelno.Text = "";
            cboJobcd.SelectedIndex = -1;
            cboRelicd.SelectedIndex = -1;
            txtPsexage.Text = "";
            chkJafg.Checked = false;
        }

        private void GetPInfo(string p_pid, OleDbConnection p_conn)
        {
            ClearPinfo();

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT A01.PNM";
            sql = sql + Environment.NewLine + "     , LEFT(A01.RESID, 6) + '-' + SUBSTRING(A01.RESID, 7, 7) RESID";
            sql = sql + Environment.NewLine + "     , A01.ZIPCD";
            sql = sql + Environment.NewLine + "     , A01.ADDR1";
            sql = sql + Environment.NewLine + "     , A01.ADDR2";
            sql = sql + Environment.NewLine + "     , A10.EMPDIV";
            sql = sql + Environment.NewLine + "     , A01.HTELNO";
            sql = sql + Environment.NewLine + "     , A10.TTDAY";
            sql = sql + Environment.NewLine + "     , CASE WHEN T91.MJAM = 0 THEN 0 ELSE T91.MJAM END JAM";
            sql = sql + Environment.NewLine + "     , ISNULL(A01.DRRMK,'') + CHAR(25) + ISNULL(A01.WONRMK,'') DRRMK";
            sql = sql + Environment.NewLine + "     , A10.FAMNM";
            sql = sql + Environment.NewLine + "     , A01.EMAIL";
            sql = sql + Environment.NewLine + "     , A88.CDNM EMPDIVNM";
            sql = sql + Environment.NewLine + "     , A10.PDESC";
            sql = sql + Environment.NewLine + "     , A01.OTELNO";
            sql = sql + Environment.NewLine + "     , A01.JOBCD";
            sql = sql + Environment.NewLine + "     , A01.RELICD";
            sql = sql + Environment.NewLine + "     , ISNULL(A01.PSEX,'')+'/'+(CASE WHEN ISDATE(A01.BTHDT)=1 AND (DATEDIFF(DAY,A01.BTHDT,GETDATE())-DATEDIFF(YEAR,A01.BTHDT,GETDATE())/4)/365 < 6  THEN CONVERT(VARCHAR,(DATEDIFF(DAY,A01.BTHDT,GETDATE())-DATEDIFF(YEAR,A01.BTHDT,GETDATE())/4)/365) + '세'+ CONVERT(VARCHAR,(DATEDIFF(MONTH,DATEADD(DAY,-((DATEDIFF(DAY,A01.BTHDT,GETDATE())-DATEDIFF(YEAR,A01.BTHDT,GETDATE())/4)%365),GETDATE()),GETDATE()))) + '개월' ";
            sql = sql + Environment.NewLine + "								        WHEN ISDATE(A01.BTHDT)=1 AND (DATEDIFF(DAY,A01.BTHDT,GETDATE())-DATEDIFF(YEAR,A01.BTHDT,GETDATE())/4)/365 >= 6 THEN CONVERT(VARCHAR,(DATEDIFF(DAY,A01.BTHDT,GETDATE())-DATEDIFF(YEAR,A01.BTHDT,GETDATE())/4)/365) ";
            sql = sql + Environment.NewLine + "								        ELSE ''";
            sql = sql + Environment.NewLine + "                                END) PSEXAGE";
            sql = sql + Environment.NewLine + "     , A10.JAFG";
            sql = sql + Environment.NewLine + "     , A01.NATFG";
            sql = sql + Environment.NewLine + "     , A01.CONFFG";
            sql = sql + Environment.NewLine + "     , A01.CONFFG2";
            sql = sql + Environment.NewLine + "     , A01.CONFGB";
            sql = sql + Environment.NewLine + "     , A01.DOROCD";
            sql = sql + Environment.NewLine + "     , A01.DONGSEQNO";
            sql = sql + Environment.NewLine + "     , A01.JIHAFG";
            sql = sql + Environment.NewLine + "     , A01.BUILDINGMAIN";
            sql = sql + Environment.NewLine + "     , A01.BUILDINGSUB";
            sql = sql + Environment.NewLine + "     , (SELECT COUNT(*) FROM TA57 A (nolock) WHERE A.PID = A01.PID) A57CNT";
            sql = sql + Environment.NewLine + "     , (SELECT COUNT(*) FROM ( SELECT A.PID FROM TA58 A (nolock) WHERE A.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                               UNION ALL ";
            sql = sql + Environment.NewLine + "                               SELECT B.PID FROM TA581 B (nolock) WHERE B.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                               UNION ALL ";
            sql = sql + Environment.NewLine + "                               SELECT C.PID FROM TA582 C (nolock) WHERE C.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                               UNION ALL ";
            sql = sql + Environment.NewLine + "                               SELECT D.PID FROM TA585 D (nolock) WHERE D.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                               UNION ALL ";
            sql = sql + Environment.NewLine + "                               SELECT E.PID FROM TA586 E (nolock) WHERE E.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                               UNION ALL ";
            sql = sql + Environment.NewLine + "                               SELECT F.PID FROM TA587 F (nolock) WHERE F.PID = A01.PID ";
            sql = sql + Environment.NewLine + "                             ) X";
            sql = sql + Environment.NewLine + "       ) A58CNT";
            sql = sql + Environment.NewLine + "     , A01.ZIPCD_NEW";
            sql = sql + Environment.NewLine + "     , A10.OCCURDT";
            sql = sql + Environment.NewLine + "     , A10.CONFDT";
            sql = sql + Environment.NewLine + "     , A01.BDMNGNO";
            sql = sql + Environment.NewLine + "     , A01.FRIENDRMK";
            sql = sql + Environment.NewLine + "     , '' ";
            sql = sql + Environment.NewLine + "  FROM TA01 A01 (nolock) LEFT OUTER JOIN TT91 T91 (nolock) ON A01.PID = T91.PID ";
            sql = sql + Environment.NewLine + "     , TA10 A10 (nolock) LEFT OUTER JOIN TA88 A88 (nolock) ON A88.MST1CD = 'A' AND A88.MST2CD = '21' AND A88.MST3CD = A10.EMPDIV ";
            sql = sql + Environment.NewLine + " WHERE A01.PID = ? ";
            sql = sql + Environment.NewLine + " AND A01.PID = A10.PID ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", p_pid));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtPnm.Text = reader["PNM"].ToString();
                    txtResid.Text = reader["RESID"].ToString();
                    txtZipcd.Text = reader["ZIPCD_NEW"].ToString();
                    txtAddr1.Text = reader["ADDR1"].ToString();
                    txtAddr2.Text = reader["ADDR2"].ToString();

                    cboEmpdiv.SelectedIndex = -1;
                    string empdiv = reader["EMPDIV"].ToString();
                    if (empdiv != "")
                    {
                        for (int i = 0; i < cboEmpdiv.Items.Count; i++)
                        {
                            if (cboEmpdiv.Items[i].ToString().StartsWith(empdiv))
                            {
                                cboEmpdiv.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    txtTelno.Text = reader["HTELNO"].ToString();
                    txtPidinfo.Text = reader["DRRMK"].ToString();
                    txtEmail.Text = reader["EMAIL"].ToString();

                    cboPdesc.SelectedIndex = -1;
                    string pdesc = reader["PDESC"].ToString();
                    if (pdesc != "")
                    {
                        for (int i = 0; i < cboPdesc.Items.Count; i++)
                        {
                            if (cboPdesc.Items[i].ToString().StartsWith(pdesc))
                            {
                                cboPdesc.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    txtOtelno.Text = reader["OTELNO"].ToString();

                    cboJobcd.SelectedIndex = -1;
                    string jobcd = reader["JOBCD"].ToString();
                    if (jobcd != "")
                    {
                        for (int i = 0; i < cboJobcd.Items.Count; i++)
                        {
                            if (cboJobcd.Items[i].ToString().StartsWith(jobcd))
                            {
                                cboJobcd.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    cboRelicd.SelectedIndex = -1;
                    string relicd = reader["RELICD"].ToString();
                    if (relicd != "")
                    {
                        for (int i = 0; i < cboRelicd.Items.Count; i++)
                        {
                            if (cboRelicd.Items[i].ToString().StartsWith(relicd))
                            {
                                cboRelicd.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    txtPsexage.Text = reader["PSEXAGE"].ToString();
                    chkJafg.Checked = (reader["JAFG"].ToString() == "1");

                    WritePIICLog(m_User, m_Prjcd, this.Name, "열람", p_pid, "1", "GetPInfo", p_conn);
                }
            }
        }

        private void ClearExInfo()
        {
            OnPgm = true;

            cboDptcd.SelectedIndex = -1;
            cboDrid.SelectedIndex = -1;
            rbAcxsf2.Checked = true; // 특진제도가 없어졌음. 무조건 비특진으로 처리
            cboQfycd.SelectedIndex = -1;
            cboPdiv.SelectedIndex = -1;
            cboGonsgb.SelectedIndex = -1;

            OnPgm = false;
        }

        private void GetExInfo(string p_pid, OleDbConnection p_conn)
        {
            ClearExInfo();

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT TOP 1 S21.DPTCD";
            sql = sql + Environment.NewLine + "     , S21.ACTP";
            sql = sql + Environment.NewLine + "     , S21.DRID";
            sql = sql + Environment.NewLine + "     , S21.PDIV";
            sql = sql + Environment.NewLine + "     , S21.QFYCD";
            sql = sql + Environment.NewLine + "     , A10.QLFYCD A10QFYCD";
            sql = sql + Environment.NewLine + "     , A10.PDIV A10PDIV";
            sql = sql + Environment.NewLine + "     , S21.RCDIV";
            sql = sql + Environment.NewLine + "     , S21.DSCD";
            sql = sql + Environment.NewLine + "     , S21.NTDIV";
            sql = sql + Environment.NewLine + "     , S21.ACRID";
            sql = sql + Environment.NewLine + "     , S21.RPRID";
            sql = sql + Environment.NewLine + "     , S21.ACXAM";
            sql = sql + Environment.NewLine + "     , S21.ACSAM";
            sql = sql + Environment.NewLine + "     , S21.ACNOF";
            sql = sql + Environment.NewLine + "     , S21.KYSTR";
            sql = sql + Environment.NewLine + "     , A07.DRDIV ACSXF";
            sql = sql + Environment.NewLine + "     , S21.GONSGB";
            sql = sql + Environment.NewLine + "     , '' EXEMRSN";
            sql = sql + Environment.NewLine + "     , S21.CONFG";
            sql = sql + Environment.NewLine + "     , '' MEDONFG";
            sql = sql + Environment.NewLine + "     , '' DAETC";
            sql = sql + Environment.NewLine + "     , '' INTM";
            sql = sql + Environment.NewLine + "     , S21.ERSTAT";
            sql = sql + Environment.NewLine + "     , S21.CONSULTFG";
            sql = sql + Environment.NewLine + "     , S21.CONSULTDPTCD";
            sql = sql + Environment.NewLine + "     , S21.CONSULTDRID";
            sql = sql + Environment.NewLine + "     , S21.CONSULTRMK";
            sql = sql + Environment.NewLine + "     , S21.ERSERIOUS ";
            sql = sql + Environment.NewLine + "     , A07.DRNM ";
            sql = sql + Environment.NewLine + "  FROM TS21 S21 (nolock) LEFT  JOIN TA07 A07 (nolock) ON A07.DRID = S21.DRID ";
            sql = sql + Environment.NewLine + "                         INNER JOIN TA10 A10 (nolock) ON A10.PID = S21.PID";
            sql = sql + Environment.NewLine + " WHERE S21.PID = ? ";
            sql = sql + Environment.NewLine + "   AND ISNULL(s21.CCFG,'') IN ('','0') ";
            sql = sql + Environment.NewLine + " ORDER BY S21.EXDT DESC, S21.DPTCD DESC, S21.HMS DESC ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", p_pid));

                string dptcd = "";
                string drid = "";
                string acxsf = "";
                string qfycd = "";
                string pdiv = "";
                string gonsgb = "";

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dptcd = reader["DPTCD"].ToString();
                    drid = reader["DRID"].ToString();
                    acxsf = reader["ACSXF"].ToString();
                    qfycd = reader["QFYCD"].ToString();
                    pdiv = reader["PDIV"].ToString();
                    gonsgb = reader["GONSGB"].ToString();
                }
                reader.Close();

                // 값을 화면에 출력
                OnPgm = true;

                SelecteIndexCombo(cboDptcd, dptcd);
                SelecteIndexCombo(cboDrid, drid);
                SelecteIndexCombo(cboQfycd, qfycd);
                SelecteIndexCombo(cboPdiv, pdiv);
                SelecteIndexCombo(cboGonsgb, gonsgb);

                SetDridCombo(dptcd, p_conn);

                rbAcxsf2.Checked = true; // 특진제도가 없어졌음. 무조건 비특진으로 처리

                OnPgm = false;
            }
        }

        private void SelecteIndexCombo(System.Windows.Forms.ComboBox combo, string value)
        {
            combo.SelectedIndex = -1;
            if (value != "")
            {
                for (int i = 0; i < combo.Items.Count; i++)
                {
                    if (combo.Items[i].ToString().StartsWith(value))
                    {
                        combo.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void ClearQfyInfo()
        {
            txtUnicd.Text = "";
            txtUninm.Text = "";
            txtInsid.Text = "";
            txtInsnm.Text = "";
            txtInsresid.Text = "";
            cboFamrelcd.SelectedIndex = -1;
            txtApprno.Text = "";
            txtMaddr.Text = "";
            txtApprdt.Text = "";
            txtEntdt.Text = "";
            txtValdt.Text = "";
            txtRqpno.Text = "";
        }

        private void GetQfyInfo(string p_pid, OleDbConnection p_conn)
        {
            ClearQfyInfo();

            if (cboQfycd.SelectedItem == null) return;
            string qfycd = cboQfycd.SelectedItem.ToString().Split(' ')[0];
            if (qfycd.Trim() == "") return;

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT A56.ENTDT";
            sql = sql + Environment.NewLine + "     , A56.APPRDT";
            sql = sql + Environment.NewLine + "     , A56.OAVLDT";
            sql = sql + Environment.NewLine + "     , A56.UNICD";
            sql = sql + Environment.NewLine + "     , A56.INSID";
            sql = sql + Environment.NewLine + "     , LEFT(A56.RESID, 6) + '-' + SUBSTRING(A56.RESID, 7, 7) RESID";
            sql = sql + Environment.NewLine + "     , A56.INSNM";
            sql = sql + Environment.NewLine + "     , A52.UNINM";
            sql = sql + Environment.NewLine + "     , A56.FAMRELCD";
            sql = sql + Environment.NewLine + "     , A88.CDNM FAMRELNM";
            sql = sql + Environment.NewLine + "     , A56.APPRNO";
            sql = sql + Environment.NewLine + "     , A56.MADDR";
            sql = sql + Environment.NewLine + "     , A56.CREDT";
            sql = sql + Environment.NewLine + "     , A56.GDPTCD";
            sql = sql + Environment.NewLine + "     , A56.cfhcRem";
            sql = sql + Environment.NewLine + "     , A56.GENDT";
            sql = sql + Environment.NewLine + "     , A56.INS1";
            sql = sql + Environment.NewLine + "     , A56.INS2";
            sql = sql + Environment.NewLine + "     , A56.IAVLDT";
            sql = sql + Environment.NewLine + "     , A56.OAVLFG";
            sql = sql + Environment.NewLine + "     , A56.IAVLFG";
            sql = sql + Environment.NewLine + "     , A56.SBRDNTYPE";
            sql = sql + Environment.NewLine + "     , A88_1.CDNM";
            sql = sql + Environment.NewLine + "     , A56.disRegPrson1";
            sql = sql + Environment.NewLine + "     , A56.disRegPrson2";
            sql = sql + Environment.NewLine + "     , A56.disRegPrson3";
            sql = sql + Environment.NewLine + "     , A56.disRegPrson4";
            sql = sql + Environment.NewLine + "     , A56.disRegPrson5";
            sql = sql + Environment.NewLine + "     , A56.pregRemAmt";
            sql = sql + Environment.NewLine + "     , A10.JAFG";
            sql = sql + Environment.NewLine + "  FROM TA56 A56 (nolock) LEFT OUTER JOIN TA52 A52 (nolock) ON A52.UNICD = A56.UNICD ";
            sql = sql + Environment.NewLine + "                         LEFT OUTER JOIN TA88 A88 (nolock) ON A88.MST1CD = 'A' AND A88.MST2CD = '27' AND A88.MST3CD = A56.FAMRELCD ";
            sql = sql + Environment.NewLine + "                         LEFT OUTER JOIN TA88 A88_1 (nolock) ON A88_1.MST1CD = 'A' AND A88_1.MST2CD = 'SBRDNTYPE' AND A88_1.MST3CD = A56.SBRDNTYPE ";
            sql = sql + Environment.NewLine + "     , TA10 A10 ";
            sql = sql + Environment.NewLine + " WHERE A56.PID = ? ";
            sql = sql + Environment.NewLine + "   AND A56.QLFYCD = ? ";
            sql = sql + Environment.NewLine + "   AND A56.CREDT = (SELECT MAX(CREDT) ";
            sql = sql + Environment.NewLine + "                      FROM TA56 B (nolock) ";
            sql = sql + Environment.NewLine + "                     WHERE B.PID = A56.PID ";
            sql = sql + Environment.NewLine + "                       AND B.QLFYCD = A56.QLFYCD ";
            sql = sql + Environment.NewLine + "                   ) ";
            sql = sql + Environment.NewLine + "   AND A10.PID = A56.PID ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", p_pid));
                cmd.Parameters.Add(new OleDbParameter("@2", qfycd));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUnicd.Text = reader["UNICD"].ToString();
                    txtUninm.Text = reader["UNINM"].ToString();
                    txtInsid.Text = reader["INSID"].ToString();
                    txtInsnm.Text = reader["INSNM"].ToString();
                    txtInsresid.Text = reader["RESID"].ToString();

                    cboFamrelcd.SelectedIndex = -1;
                    string famrelcd = reader["FAMRELCD"].ToString();
                    if (famrelcd != "")
                    {
                        for (int i = 0; i < cboFamrelcd.Items.Count; i++)
                        {
                            if (cboFamrelcd.Items[i].ToString().StartsWith(famrelcd))
                            {
                                cboFamrelcd.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    txtApprno.Text = reader["APPRNO"].ToString();
                    txtMaddr.Text = reader["MADDR"].ToString();
                    txtApprdt.Text = reader["APPRDT"].ToString();
                }
            }

            sql = "";
            sql = sql + Environment.NewLine + "SELECT RQDIV, RQPDT, RQPNO, AVLDT ";
            sql = sql + Environment.NewLine + "  FROM TS04 A (nolock) ";
            sql = sql + Environment.NewLine + " WHERE PID = ? ";
            sql = sql + Environment.NewLine + "   AND QFYCD = ? ";
            sql = sql + Environment.NewLine + "   AND CREDT = (SELECT MAX(CREDT) ";
            sql = sql + Environment.NewLine + "                  FROM TS04 B (nolock) ";
            sql = sql + Environment.NewLine + "                 WHERE B.PID = A.PID ";
            sql = sql + Environment.NewLine + "                   AND B.QFYCD = A.QFYCD";
            sql = sql + Environment.NewLine + "               )";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", p_pid));
                cmd.Parameters.Add(new OleDbParameter("@2", qfycd));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtEntdt.Text = reader["ENTDT"].ToString();
                    txtValdt.Text = reader["VALDT"].ToString();
                    txtRqpno.Text = reader["RQPNO"].ToString();
                }
            }
        }

        private void SetDridCombo(string p_dptcd, OleDbConnection p_conn)
        {
            cboDrid.Items.Clear();
            string sql = "";
            sql = "SELECT DRID, DRNM FROM TA07 (nolock) WHERE DPTCD=? ORDER BY DRID";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", p_dptcd));

                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cboDrid.Items.Add(reader["DRID"].ToString() + " " + reader["DRNM"].ToString());
                }
                reader.Close();
            }
        }

        private void cboQfycd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            string pid = txtPid.Text.ToString();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                GetQfyInfo(pid, conn);
            }
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
                if (reader.Read())
                {
                    seq = reader["NEXT_SEQ"].ToString();
                }
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

        private void cboDptcd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            cboDrid.Items.Clear();
            if (cboDptcd.SelectedIndex == -1) return;
            string dptcd = cboDptcd.SelectedItem.ToString().Split(' ')[0];
            if (dptcd == "") return;
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                SetDridCombo(dptcd, conn);
            }
            if (cboDrid.Items.Count > 0) cboDrid.SelectedIndex = 0;
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                string iofg = (rbIPtnt.Checked ? "2" : "1");

                string exdate = txtBdodt.Text.ToString();
                if (iofg == "1")
                {
                    if (exdate == "")
                    {
                        MessageBox.Show("청구일을 입력하세요.");
                        return;
                    }
                    else if (MetroLib.Util.ValDt(exdate + "01") == false)
                    {
                        MessageBox.Show("청구일을 확인하세요(YYYYMM).");
                        return;
                    }
                }
                else
                {
                    if (exdate == "")
                    {
                        MessageBox.Show("청구월을 입력하세요.");
                        return;
                    }
                    else if (MetroLib.Util.ValDt(exdate) == false)
                    {
                        MessageBox.Show("청구월을 확인하세요(YYYYMMDD).");
                        return;
                    }
                }
                string exdt = txtExdt.Text.ToString();
                if (iofg == "1")
                {
                    if (exdt == "")
                    {
                        MessageBox.Show("진료일자를 입력하세요.");
                        return;
                    }
                    else if (exdt.Length != 8)
                    {
                        MessageBox.Show("진료일자를 확인하세요.");
                        return;
                    }
                    else if (MetroLib.Util.ValDt(exdt) == false)
                    {
                        MessageBox.Show("진료일자를 확인하세요.");
                        return;
                    }
                }
                else
                {
                    if (exdt == "")
                    {
                        MessageBox.Show("입원일자를 입력하세요.");
                        return;
                    }
                    else if (exdt.Length != 8)
                    {
                        MessageBox.Show("입원일자를 확인하세요.");
                        return;
                    }
                    else if (MetroLib.Util.ValDt(exdt) == false)
                    {
                        MessageBox.Show("입원일자를 확인하세요.");
                        return;
                    }
                }
                string dptcd = (cboDptcd.SelectedItem == null ? "" : cboDptcd.SelectedItem.ToString().Split(' ')[0]);
                if (dptcd == "")
                {
                    MessageBox.Show("진료과를 선택하세요.");
                    return;
                }
                string qfycd = (cboQfycd.SelectedItem == null ? "" : cboQfycd.SelectedItem.ToString().Split(' ')[0]);
                if (qfycd == "")
                {
                    MessageBox.Show("자격을 선택하세요.");
                    return;
                }
                string pdiv = (cboPdiv.SelectedItem == null ? "" : cboPdiv.SelectedItem.ToString().Split(' ')[0]);
                if (pdiv == "")
                {
                    MessageBox.Show("환자구분을 선택하세요.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Make();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show("작업이 성공적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
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

        private void Make()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string iofg = (rbIPtnt.Checked ? "2" : "1");

                string tTI1A = "TI1A";
                string tTI1AR = "TI1AR";
                string tTI1B = "TI1B";
                string fEXDATE = "EXDATE";
                if (iofg == "2")
                {
                    tTI1A = "TI2A";
                    tTI1AR = "TI2AR";
                    tTI1B = "TI2B";
                    fEXDATE = "BDODT";
                }

                string exdate = txtBdodt.Text.ToString();
                string dptcd = cboDptcd.SelectedItem.ToString().Split(' ')[0];
                string qfycd = cboQfycd.SelectedItem.ToString().Split(' ')[0];
                string pid = txtPid.Text.ToString();
                string jrby = GetJrby(dptcd, conn);
                string simcs = "1";
                string exdt = txtExdt.Text.ToString();
                // ----------------------------------------------
                // UNISQ 결정
                // ----------------------------------------------
                string unisq = "";
                string sql = "";
                sql = "";
                sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(UNISQ),0)+1 AS MAXUNISQ ";
                sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
                sql = sql + Environment.NewLine + " WHERE " + fEXDATE + " = ?";
                sql = sql + Environment.NewLine + "   AND QFYCD  = ?";
                sql = sql + Environment.NewLine + "   AND JRBY   = ?";
                sql = sql + Environment.NewLine + "   AND PID    = ?";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@4", pid));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        unisq = reader["MAXUNISQ"].ToString();
                    }
                }
                // ----------------------------------------------
                // SIMFG 결정
                // ----------------------------------------------
                string simfg = GetSimfg(qfycd, jrby);
                // ----------------------------------------------
                // 2012.01.05 WOOIL - 산재는 무조건 070
                //                    보험,보호는 무조건 방문일자별
                // ----------------------------------------------
                string sj070 = "";
                string dailySumfg = "";
                string dailyPtamtfg = "1";
                string qfy1 = qfycd.Substring(0, 1);

                if (qfycd == "50")
                {
                    sj070 = "070";
                }
                else if (qfy1 == "2" || qfy1 == "3" || qfy1 == "4")
                {
                    dailySumfg = "1";
                    dailyPtamtfg = "";
                }
                // ----------------------------------------------
                // SIMNO 결정
                // ----------------------------------------------
                string simnomonthfg = "";
                if (iofg == "2") simnomonthfg = GetSimnoMonthfg(conn);
                //
                string simno = "";
                if (iofg == "2" && simnomonthfg == "1")
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0)+1 AS MAXSIMNO ";
                    sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
                    sql = sql + Environment.NewLine + " WHERE " + fEXDATE + " LIKE ?";
                    sql = sql + Environment.NewLine + "   AND SIMFG  = ?";
                    sql = sql + Environment.NewLine + "   AND SIMCS  = ?";
                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", exdate.Substring(0, 6) + "%"));
                        cmd.Parameters.Add(new OleDbParameter("@2", simfg));
                        cmd.Parameters.Add(new OleDbParameter("@3", simcs));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            simno = reader["MAXSIMNO"].ToString();
                        }
                    }
                }
                else
                {
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SIMNO),0)+1 AS MAXSIMNO ";
                    sql = sql + Environment.NewLine + "  FROM " + tTI1A + " ";
                    sql = sql + Environment.NewLine + " WHERE " + fEXDATE + " = ?";
                    sql = sql + Environment.NewLine + "   AND SIMFG  = ?";
                    sql = sql + Environment.NewLine + "   AND SIMCS  = ?";
                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                        cmd.Parameters.Add(new OleDbParameter("@2", simfg));
                        cmd.Parameters.Add(new OleDbParameter("@3", simcs));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            simno = reader["MAXSIMNO"].ToString();
                        }
                    }
                }
                // ----------------------------------------------
                // DAILYPTAMTFG 조정
                // ----------------------------------------------
                if (String.Compare(exdate, "200907") < 0) dailyPtamtfg = "0";
                if (qfy1 != "2") dailyPtamtfg = "0";
                // ----------------------------------------------
                // 입원환자인경우 처리
                // ----------------------------------------------
                if (iofg == "2")
                {
                    dailySumfg = "";
                    dailyPtamtfg = "";
                }
                // ----------------------------------------------
                // 자격내용을 읽는다.
                // ----------------------------------------------
                string a56pid = "";
                string a56qfycd = "";
                string a56credt = "";
                string a56apprdt = "";
                string a56unicd = "";
                string a56uninm = "";
                string a56insid = "";
                string a56insnm = "";
                string a56famrelcd = "";
                string a56maddr = "";
                string a56gendt = "";
                string a56diseapos = "";
                string a56apprno = "";
                string a56ins1 = "";
                string a56disenm = "";

                sql = "";
                sql = sql + Environment.NewLine + "SELECT TOP 1 ";
                sql = sql + Environment.NewLine + "       A56.PID                   AS PID      ";
                sql = sql + Environment.NewLine + "     , A56.QLFYCD                AS QFYCD    ";
                sql = sql + Environment.NewLine + "     , A56.CREDT                 AS CREDT    ";
                sql = sql + Environment.NewLine + "     , APPRDT                    AS APPRDT   ";
                sql = sql + Environment.NewLine + "     , REPLACE(A56.UNICD,' ','') AS UNICD    ";
                sql = sql + Environment.NewLine + "     , REPLACE(A56.INSID,' ','') AS INSID    ";
                sql = sql + Environment.NewLine + "     , REPLACE(A56.INSNM,' ','') AS INSNM    ";
                sql = sql + Environment.NewLine + "     , A56.RESID                 AS RESID    ";
                sql = sql + Environment.NewLine + "     , A56.FAMRELCD              AS FAMRELCD ";
                sql = sql + Environment.NewLine + "     , A56.MADDR                 AS MADDR    ";
                sql = sql + Environment.NewLine + "     , A56.GENDT                 AS GENDT    ";
                sql = sql + Environment.NewLine + "     , A56.DISEAPOS              AS DISEAPOS ";
                sql = sql + Environment.NewLine + "     , A56.APPRNO                AS APPRNO   ";
                sql = sql + Environment.NewLine + "     , (SELECT A52.UNINM FROM TA52 A52 WITH (NOLOCK) WHERE A52.UNICD = A56.UNICD) AS UNINM ";
                sql = sql + Environment.NewLine + "     , A56.INS1                  AS INS1     ";
                sql = sql + Environment.NewLine + "     , A56.DISENM                AS DISENM   ";
                sql = sql + Environment.NewLine + "  FROM TA56 A56  WITH (NOLOCK)   ";
                sql = sql + Environment.NewLine + " WHERE A56.PID    = ?";
                sql = sql + Environment.NewLine + "   AND A56.QLFYCD = ?";
                sql = sql + Environment.NewLine + "   AND A56.CREDT <= ?";
                sql = sql + Environment.NewLine + " ORDER BY CREDT DESC ";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", pid));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", exdt));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        a56pid = reader["PID"].ToString();
                        a56qfycd = reader["QFYCD"].ToString();
                        a56credt = reader["CREDT"].ToString();
                        a56apprdt = reader["APPRDT"].ToString();
                        a56unicd = reader["UNICD"].ToString();
                        a56uninm = reader["UNINM"].ToString();
                        a56insid = reader["INSID"].ToString();
                        a56insnm = reader["INSNM"].ToString();
                        a56famrelcd = reader["FAMRELCD"].ToString();
                        a56maddr = reader["MADDR"].ToString();
                        a56gendt = reader["GENDT"].ToString();
                        a56diseapos = reader["DISEAPOS"].ToString();
                        a56apprno = reader["APPRNO"].ToString();
                        a56ins1 = reader["INS1"].ToString();
                        a56disenm = reader["DISENM"].ToString();
                    }
                }

                string resid = txtResid.Text.ToString().Replace("-", "");
                string pdiv = cboPdiv.SelectedItem.ToString().Split(' ')[0];
                string bdedt = (iofg=="2" ? exdt : "");
                string xdays = exdt.Substring(6, 2);
                string jrkwa = GetJrkwa(dptcd, conn);
                string pdrid = (cboDrid.SelectedItem == null ? "" : cboDrid.SelectedItem.ToString().Split(' ')[0]);
                string gonsgb = (cboGonsgb.SelectedItem == null ? "" : cboGonsgb.SelectedItem.ToString().Split(' ')[0]);
                if (gonsgb == "0") gonsgb = "";
                string rslt = (iofg == "2" ? "5" : "1"); // 진료결과 : 외래는 1.계속으로, 입원은 5.퇴원으로

                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI1A+ "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,PNM,RESID,PDIV,UNICD,UNINM,INSID,INSNM,SIMFG,SIMNO,BDEDT,STEDT,XDAYS,EXAMC,JRKK,DAILYPTAMTFG,JRKWA,PDRID,APPRNO,MADDR,APRDT,GENDT,SJ070,DAILYSUMFG,GONSGB,RSLT) ";
                sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@4", pid));
                    cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@6", simcs));
                    cmd.Parameters.Add(new OleDbParameter("@7", txtPnm.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@8", resid));
                    cmd.Parameters.Add(new OleDbParameter("@9", pdiv));
                    cmd.Parameters.Add(new OleDbParameter("@10", txtUnicd.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@11", txtUninm.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@12", txtInsid.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@13", txtInsnm.Text.ToString()));
                    cmd.Parameters.Add(new OleDbParameter("@14", simfg));
                    cmd.Parameters.Add(new OleDbParameter("@15", simno));
                    cmd.Parameters.Add(new OleDbParameter("@16", bdedt));
                    cmd.Parameters.Add(new OleDbParameter("@17", exdt));
                    cmd.Parameters.Add(new OleDbParameter("@18", xdays));
                    cmd.Parameters.Add(new OleDbParameter("@19", "1"));
                    cmd.Parameters.Add(new OleDbParameter("@20", "1"));
                    cmd.Parameters.Add(new OleDbParameter("@21", dailyPtamtfg));
                    cmd.Parameters.Add(new OleDbParameter("@22", jrkwa));
                    cmd.Parameters.Add(new OleDbParameter("@23", pdrid));
                    cmd.Parameters.Add(new OleDbParameter("@24", a56apprno)); // 성립기호(산재)
                    cmd.Parameters.Add(new OleDbParameter("@25", a56maddr)); // 사업자명(산재)
                    cmd.Parameters.Add(new OleDbParameter("@26", a56apprdt)); // 요양승인일(산재)
                    cmd.Parameters.Add(new OleDbParameter("@27", a56gendt)); // 재해발생일(산재)
                    cmd.Parameters.Add(new OleDbParameter("@28", sj070));
                    cmd.Parameters.Add(new OleDbParameter("@29", dailySumfg));
                    cmd.Parameters.Add(new OleDbParameter("@30", gonsgb));
                    cmd.Parameters.Add(new OleDbParameter("@31", rslt)); 

                    cmd.ExecuteNonQuery();
                }

                if(iofg=="2"){
                    sql="";
                    sql = sql + Environment.NewLine + "UPDATE TI2A ";
                    sql = sql + Environment.NewLine + "   SET EXAMC=DATEDIFF(DAY,STEDT,BDODT)+1 ";
                    sql = sql + Environment.NewLine + "     , JRKK=DATEDIFF(DAY,STEDT,BDODT)+1 ";
                    sql = sql + Environment.NewLine + " WHERE BDODT=?";
                    sql = sql + Environment.NewLine + "   AND QFYCD=?";
                    sql = sql + Environment.NewLine + "   AND JRBY=?";
                    sql = sql + Environment.NewLine + "   AND PID=?";
                    sql = sql + Environment.NewLine + "   AND UNISQ=?";
                    sql = sql + Environment.NewLine + "   AND SIMCS=?";
                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                        cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                        cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                        cmd.Parameters.Add(new OleDbParameter("@4", pid));
                        cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                        cmd.Parameters.Add(new OleDbParameter("@6", simcs));

                        cmd.ExecuteNonQuery();
                    }
                }

                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI1AR+ "(" + fEXDATE + ",QFYCD,JRBY,PID,UNISQ,SIMCS,RESID) ";
                sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?)";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@4", pid));
                    cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@6", simcs));
                    cmd.Parameters.Add(new OleDbParameter("@7", resid));

                    cmd.ExecuteNonQuery();
                }

                string tgwon = (iofg == "2" ? "" : exdt + "$" + exdt);
                string ipwon = (iofg == "2" ? exdt + "$" + exdate : "");

                sql = "";
                sql = sql + Environment.NewLine + "INSERT INTO " + tTI1B + "(" + fEXDATE+ ",QFYCD,JRBY,PID,UNISQ,SIMCS,SEQ1,JRKWA,PDRID,TGWON,IPWON) ";
                sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", exdate));
                    cmd.Parameters.Add(new OleDbParameter("@2", qfycd));
                    cmd.Parameters.Add(new OleDbParameter("@3", jrby));
                    cmd.Parameters.Add(new OleDbParameter("@4", pid));
                    cmd.Parameters.Add(new OleDbParameter("@5", unisq));
                    cmd.Parameters.Add(new OleDbParameter("@6", simcs));
                    cmd.Parameters.Add(new OleDbParameter("@7", "1"));
                    cmd.Parameters.Add(new OleDbParameter("@8", jrkwa));
                    cmd.Parameters.Add(new OleDbParameter("@9", pdrid));
                    cmd.Parameters.Add(new OleDbParameter("@10", tgwon));
                    cmd.Parameters.Add(new OleDbParameter("@11", ipwon));

                    cmd.ExecuteNonQuery();
                }

                if (iofg == "2")
                {
                    string t55entdt = MetroLib.Util.GetSysDate(conn);
                    string t55pid = pid;
                    string t55bededt = bdedt;
                    string t55enttm = MetroLib.Util.GetSysTime(conn);
                    string t55endtm = t55enttm;
                    string t55remark = exdate + "," + qfycd + "," + jrby + "," + pid + "," + unisq + "," + simcs;

                    int cnt = 0;

                    // 키중복방지용
                    sql = "";
                    sql = sql + Environment.NewLine + "SELECT COUNT(*) AS CNT ";
                    sql = sql + Environment.NewLine + "  FROM TT55 ";
                    sql = sql + Environment.NewLine + " WHERE ENTDT=?";
                    sql = sql + Environment.NewLine + "   AND PID=?";
                    sql = sql + Environment.NewLine + "   AND BEDEDT=?";
                    sql = sql + Environment.NewLine + "   AND ENTTM=?";
                    // TSQL문장과 Connection 객체를 지정
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", t55entdt));
                        cmd.Parameters.Add(new OleDbParameter("@2", t55pid));
                        cmd.Parameters.Add(new OleDbParameter("@3", t55bededt));
                        cmd.Parameters.Add(new OleDbParameter("@3", t55enttm));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int.TryParse(reader["CNT"].ToString(), out cnt);
                        }
                    }
                    // 있으면
                    if (cnt > 0)
                    {
                        sql = "";
                        sql = sql + Environment.NewLine + "SELECT MAX(ENTTM) AS ENTTM ";
                        sql = sql + Environment.NewLine + "  FROM TT55 ";
                        sql = sql + Environment.NewLine + " WHERE ENTDT=?";
                        sql = sql + Environment.NewLine + "   AND PID=?";
                        sql = sql + Environment.NewLine + "   AND BEDEDT=?";
                        sql = sql + Environment.NewLine + "   AND ENTTM<=?";
                        // TSQL문장과 Connection 객체를 지정
                        using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new OleDbParameter("@1", t55entdt));
                            cmd.Parameters.Add(new OleDbParameter("@2", t55pid));
                            cmd.Parameters.Add(new OleDbParameter("@3", t55bededt));
                            cmd.Parameters.Add(new OleDbParameter("@3", t55enttm.Substring(0,4) + "99"));

                            // 데이타는 서버에서 가져오도록 실행
                            OleDbDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                t55enttm = reader["ENTTM"].ToString();
                                int tmp = 0;
                                int.TryParse(t55enttm, out tmp);
                                tmp += 1000001;
                                t55enttm = tmp.ToString().Substring(1);
                            }
                        }
                    }
                    // 저장
                    sql = "";
                    sql = sql + Environment.NewLine + "INSERT INTO TT55(ENTDT,PID,BEDEDT,ENTTM,DPTCD,ENDTM,EMPID,EMPNM,PRGID,WORKNM,REMARK) ";
                    sql = sql + Environment.NewLine + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", t55entdt));
                        cmd.Parameters.Add(new OleDbParameter("@2", t55pid));
                        cmd.Parameters.Add(new OleDbParameter("@3", t55bededt));
                        cmd.Parameters.Add(new OleDbParameter("@4", t55enttm));
                        cmd.Parameters.Add(new OleDbParameter("@5", ""));
                        cmd.Parameters.Add(new OleDbParameter("@6", t55endtm));
                        cmd.Parameters.Add(new OleDbParameter("@7", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@8", ""));
                        cmd.Parameters.Add(new OleDbParameter("@9", "ADD0112E"));
                        cmd.Parameters.Add(new OleDbParameter("@10", "빈명세서생성"));
                        cmd.Parameters.Add(new OleDbParameter("@11",  t55remark));

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }

        private string GetJrby(string p_dptcd, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT PRIMDPTCD,INSDPTCD FROM TA09 WHERE DPTCD=?";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", p_dptcd));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = reader["PRIMDPTCD"].ToString() + "$" + reader["INSDPTCD"].ToString();
                }
            }
            return ret;
        }

        private string GetJrkwa(string p_dptcd, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT PRIMDPTCD,INSDPTCD, DPTCD, DPTNM FROM TA09 WHERE DPTCD=?";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Add(new OleDbParameter("@1", p_dptcd));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = reader["PRIMDPTCD"].ToString() + "$" + reader["INSDPTCD"].ToString() + "$" + reader["DPTCD"].ToString() + "$" + reader["DPTNM"].ToString();
                }
            }
            return ret;
        }

        private string GetSimfg(string p_qfycd, string p_jrby)
        {
            string ret = "";
            string qfycd1 = p_qfycd.Substring(0, 1);
            string jrby1 = p_jrby.Substring(0, 1);

            if (qfycd1 == "2" && String.Compare(jrby1, "6") < 0) ret = "1";  // 보험
            if (qfycd1 == "2" && jrby1 == "6") ret = "2";  // 보험치과

            if (qfycd1 == "3" && String.Compare(jrby1, "6") < 0) ret = "3";  // 보호
            if (qfycd1 == "3" && jrby1 == "6") ret = "4";  // 보호치과

            if (qfycd1 == "4" && String.Compare(jrby1, "6") < 0) ret = "1";  // 2005.07.28 NSK - 공상 -> 건강보험 SIMFG로 Setting
            if (qfycd1 == "4" && jrby1 == "6") ret = "2";  // 2005.07.28 NSK - 공상 -> 건강보험 SIMFG로 Setting

            if (qfycd1 == "5" && String.Compare(jrby1, "7") < 0) ret = "5";  // 산재

            if (qfycd1 == "6" && String.Compare(jrby1, "7") < 0) ret = "6";  // 자보

            if (qfycd1 == "2" && String.Compare(jrby1, "6") > 0) ret = "7";  // 한방보험
            if (qfycd1 == "3" && String.Compare(jrby1, "6") > 0) ret = "8";  // 한방보호
            if (qfycd1 == "6" && String.Compare(jrby1, "6") > 0) ret = "13"; // 한방자보

            if (qfycd1 == "4" && String.Compare(jrby1, "7") >= 0) ret = "7"; // 2005.07.28 NSK - 한방공상-> 한방건강보험 SIMFG로 Setting
            if (qfycd1 == "5" && String.Compare(jrby1, "7") >= 0) ret = "9"; // 산재

            if (p_qfycd == "29") ret = "10"; // 2005.10.27 NSK 보훈일반
            if (qfycd1 == "8") ret = "12";   // 2007.05.29 WOOIL - 계약처

            return ret;
        }

        private string GetSimnoMonthfg(OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            sql = "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='133'";
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = reader["FLD2QTY"].ToString();
                }
            }
            return ret;
        }

    }
}

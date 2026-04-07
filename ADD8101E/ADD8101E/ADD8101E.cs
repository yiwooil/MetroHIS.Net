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

using Microsoft.Win32;

namespace ADD8101E
{
    public partial class ADD8101E : Form
    {
        private Boolean m_OnPgm;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD8101E()
        {
            InitializeComponent();
            m_OnPgm = false;
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

        }

        public ADD8101E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;

            this.SetConfig();
            this.SetISOK_ALL();
        }

        private void SetConfig()
        {
            try
            {
                m_OnPgm = true;
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD");
                String folder = reg.GetValue("ADD8101E.FOLDER","").ToString();
                if (folder == "") folder = "C:/hira/DDMD/sam/out";
                txtFolder.Text = folder;
                m_OnPgm = false;
            }
            catch (Exception ex)
            {
                m_OnPgm = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_OnPgm) return;
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD");
                reg.SetValue("ADD8101E.FOLDER", txtFolder.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetISOK_ALL()
        {
            if (ISOK_I010() == true)
            {
                txtI010_3.Text = "준비됨";
                txtI010_3.ForeColor = Color.Blue;
            }
            else
            {
                txtI010_3.Text = "파일없음";
                txtI010_3.ForeColor = Color.Red;
            }
            if (ISOK_I020() == true)
            {
                txtI020_3.Text = "준비됨";
                txtI020_3.ForeColor = Color.Blue;
            }
            else
            {
                txtI020_3.Text = "파일없음";
                txtI020_3.ForeColor = Color.Red;
            }
            if (ISOK_I030() == true)
            {
                txtI030_3.Text = "준비됨";
                txtI030_3.ForeColor = Color.Blue;
            }
            else
            {
                txtI030_3.Text = "파일없음";
                txtI030_3.ForeColor = Color.Red;
            }
            if (ISOK_I060() == true)
            {
                txtI060_3.Text = "준비됨";
                txtI060_3.ForeColor = Color.Blue;
            }
            else
            {
                txtI060_3.Text = "파일없음";
                txtI060_3.ForeColor = Color.Red;
            }
        }

        private Boolean ISOK_I010()
        {
            if (FileExists("*I010") == false)
            {
                if (FileExists("*I010.txt") == true) return true;
                return false;
            }
            return true;
        }
        private Boolean ISOK_I020()
        {
            if (FileExists("*I020.1") == false) return false;
            if (FileExists("*I020.2") == false) return false;
            if (FileExists("*I020.3") == false) return false;
            return true;
        }
        private Boolean ISOK_I030()
        {
            if (FileExists("*I030.1") == false) return false;
            if (FileExists("*I030.2") == false) return false;
            if (FileExists("*I030.3") == false) return false;
            return true;
        }
        private Boolean ISOK_I060()
        {
            if (FileExists("*I060.1") == false) return false;
            if (FileExists("*I060.2") == false) return false;
            if (FileExists("*I060.3") == false) return false;
            return true;
        }

        private Boolean FileExists(String file)
        {
            if (Directory.Exists(txtFolder.Text) == false) return false;
            String[] files = Directory.GetFiles(txtFolder.Text, file);
            return files.Length > 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "처리중입니다.");

                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    this.SaveI010(conn);
                    this.SaveI020(conn);
                    this.SaveI030(conn);
                    this.SaveI060(conn);
                }

                this.CloseProgressForm("", "처리중입니다.");
                Cursor.Current = Cursors.Default;

                this.SetISOK_ALL();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "처리중입니다.");
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

        private void SaveI010(OleDbConnection p_conn)
        {
            if (Directory.Exists(txtFolder.Text) == false) return;
            String[] files = Directory.GetFiles(txtFolder.Text, "*I010");
            foreach (String file in files)
            {
                this.PutData("I010", file, p_conn);
            }
            // 파일 삭제
            foreach (String file in files)
            {
                File.Delete(file);
            }
            // I010.txt로 만들어지는 경우가 있다.
            files = Directory.GetFiles(txtFolder.Text, "*I010.txt");
            foreach (String file in files)
            {
                this.PutData("I010", file, p_conn);
            }
            // 파일 삭제
            foreach (String file in files)
            {
                File.Delete(file);
            }
        }

        private void SaveI020(OleDbConnection p_conn)
        {
            if (Directory.Exists(txtFolder.Text) == false) return;
            String[] files_1 = Directory.GetFiles(txtFolder.Text, "*I020.1");
            foreach (String file in files_1)
            {
                this.PutData("I020.1", file, p_conn);
            }
            String[] files_2 = Directory.GetFiles(txtFolder.Text, "*I020.2");
            foreach (String file in files_2)
            {
                this.PutData("I020.2", file, p_conn);
            }
            String[] files_3 = Directory.GetFiles(txtFolder.Text, "*I020.3");
            foreach (String file in files_3)
            {
                this.PutData("I020.3", file, p_conn);
            }
            // 파일 삭제
            foreach (String file in files_1)
            {
                File.Delete(file);
            }
            foreach (String file in files_2)
            {
                File.Delete(file);
            }
            foreach (String file in files_3)
            {
                File.Delete(file);
            }
        }

        private void SaveI030(OleDbConnection p_conn)
        {
            if (Directory.Exists(txtFolder.Text) == false) return;
            String[] files_1 = Directory.GetFiles(txtFolder.Text, "*I030.1");
            foreach (String file in files_1)
            {
                this.PutData("I030.1", file, p_conn);
            }
            String[] files_2 = Directory.GetFiles(txtFolder.Text, "*I030.2");
            foreach (String file in files_2)
            {
                this.PutData("I030.2", file, p_conn);
            }
            String[] files_3 = Directory.GetFiles(txtFolder.Text, "*I030.3");
            foreach (String file in files_3)
            {
                this.PutData("I030.3", file, p_conn);
            }
            // 파일 삭제
            foreach (String file in files_1)
            {
                File.Delete(file);
            }
            foreach (String file in files_2)
            {
                File.Delete(file);
            }
            foreach (String file in files_3)
            {
                File.Delete(file);
            }
        }

        private void SaveI060(OleDbConnection p_conn)
        {
            if (Directory.Exists(txtFolder.Text) == false) return;
            String[] files_1 = Directory.GetFiles(txtFolder.Text, "*I060.1");
            foreach (String file in files_1)
            {
                this.PutData("I060.1", file, p_conn);
            }
            String[] files_2 = Directory.GetFiles(txtFolder.Text, "*I060.2");
            foreach (String file in files_2)
            {
                this.PutData("I060.2", file, p_conn);
            }
            String[] files_3 = Directory.GetFiles(txtFolder.Text, "*I060.3");
            foreach (String file in files_3)
            {
                this.PutData("I060.3", file, p_conn);
            }
            // 파일 삭제
            foreach (String file in files_1)
            {
                File.Delete(file);
            }
            foreach (String file in files_2)
            {
                File.Delete(file);
            }
            foreach (String file in files_3)
            {
                File.Delete(file);
            }
        }

        private void PutData(String samid, String path, OleDbConnection p_conn)
        {
            // 파일의 인코딩을 확인한다.
            StreamReader sr = new StreamReader(path, true);
            Encoding enc = sr.CurrentEncoding;
            sr.Close();

            // 2021.04.12 WOOIL - KT-EDI에서 받은 파일이면
            if (Path.GetFileNameWithoutExtension(path).Length == 4) enc = Encoding.Default;

            String[] lines = File.ReadAllLines(path, enc);
            foreach (String line in lines)
            {
                this.PutSamFile(samid, line, "0", p_conn);
            }
        }

        private void PutSamFile(String samid, String data, String count, OleDbConnection p_conn)
        {
            if (data == "") return;

            long fieldCount = 0;
            String fieldType = "";
            String fieldLength = "";
            String insertSql = "";
            long keyCount = 0;
            String deleteSql = "";
            String keyField = "";

            if (samid == "I010")
            {
                String ver = data.Substring(0, 3);
                if (ver.CompareTo("070") >= 0)
                {
                    fieldCount = 15;
                    fieldType = "C,C,C,C,C,C,C,C,C,C,C,N,N,C,C";
                    fieldLength = "3,1,10,1,4,7,8,15,8,6,1,6,10,4,1750";
                    insertSql = "INSERT INTO TIE_I010(VERSION,ACCDIV,DEMNO,ACCBACKDIV,FMNO,HOSID,REPDT,ACCNO,REDAY,YYMM,ADDZ1,DEMCNT,DEMAMT,RSNCD,MEMO)";
                    keyCount = 8;
                    deleteSql = "DELETE FROM TIE_I010 WHERE ACCDIV=? AND DEMNO=? AND ACCBACKDIV=? AND FMNO=? AND HOSID=? AND REPDT=? AND ACCNO=?";
                    keyField = "1,K,K,K,K,K,K,K";
                }
            }
            else if (samid == "I020.1")
            {
                fieldCount = 38;
                fieldType = "C,C,C,C,C,C,C,C,C,C,C,C,N,N,C,C,C,C,N,N,C,N,N,C,N,N,C,N,N,C,N,N,N,N,N,N,C,C";
                fieldLength = "3,15,1,4,7,20,1,20,10,8,8,8,12,12,12,17,11,1,6,12,1,6,12,1,6,12,1,6,12,1,6,12,12,12,12,12,350,500";
                insertSql = "INSERT INTO TIE_I020(VERSION,ACCNO,CNTNO,FMNO,HOSID,CEONM,BUSSDIV,SENDNO,DEMNO,ACCDT,REPDT,PREPAYDT,INCOMETAX,INHABITAX,TAXTOT,ACCOUNT,BANKCD,DIV1,CNTTOT,DEMTOT,DIV2,PAYTOT,PAYQYTOT,DIV3,DELCNTTOT,DELQYTOT,DIV4,BULCNT,BULAMT,DIV5,PAYRSVCNTTOT,PAYRSVQYTOT,PAYAMTTOT,REALPAYAMT,PREPAYAMT,CHAAMT,BUNMEMO,MEMO)";
                keyCount = 3;
                deleteSql = "DELETE FROM TIE_I020 WHERE ACCNO=? AND CNTNO=?";
                keyField = "1,K,K";
            }
            else if (samid == "I020.2")
            {
                fieldCount = 15;
                fieldType = "C,C,C,C,C,C,C,N,N,N,N,N,N,C,C";
                fieldLength = "15,1,5,20,2,8,16,12,12,12,12,12,3,250,500";
                insertSql = "INSERT INTO TIE_I0202(ACCNO,CNTNO,EPRTNO,PNM,JRGB,GENDT,FTDAYS,UNAMT,JJAMT,PAYAMT,BULAMT,BOAMT,ORDDAYS,BULRMK,MEMO)";
                keyCount = 3;
                deleteSql = "DELETE FROM TIE_I0202 WHERE ACCNO=? AND CNTNO=? AND EPRTNO=?";
                keyField = "K,K,K";
            }
            else if (samid == "I020.3")
            {
                fieldCount = 21;
                fieldType = "C,C,C,N,C,C,C,C,N,N,N2,N2,N2,N2,N,N,N2,N,N,C,C";
                fieldLength = "15,1,5,4,4,2,9,70,9,70,8.2,8.2,5.2,5.2,3,3,8.2,12,12,4,350";
                insertSql = "INSERT INTO TIE_I0203(ACCNO,CNTNO,EPRTNO,LNO,HANGMOKNO,CDGB,BGIHO,BGIHONM,JJBGIHO,JJBGIHONM,DANGA,JJDANGA,DQTY,IJDQTY,DDAY,IJDAY,TIJQTY,GUMAK,JJGUMAK,JJCD,JJRMK)";
                keyCount = 4;
                deleteSql = "DELETE FROM TIE_I0203 WHERE ACCNO=? AND CNTNO=? AND EPRTNO=? AND LNO=?";
                keyField = "K,K,K,K";
            }
            else if (samid == "I030.1")
            {
                fieldCount = 18;
                fieldType = "C,C,C,C,C,D,N,N,N,N,N,N,N,N,N,N,N,C";
                fieldLength = "8,1,4,7,1,6,6,6,6,6,6,10,10,10,10,10,10,350";
                insertSql = "INSERT INTO TIE_I0301(ACCNO,CNTNO,FMNO,HOSID,ACCDIV,REPDT,DEMCNTTOT,EXAMCNTTOT,PAYNOTCNTTOT,PAYRSVCNTTOT,DELCNTTOT,DEMQYTOT,EXAMQYTOT,PAYNOTQYTOT,PAYRSVQYTOT,DELQYTOT,PAYQYTOT,MEMO)";
                keyCount = 2;
                deleteSql = "DELETE FROM TIE_I0301 WHERE ACCNO=? AND CNTNO=?";
                keyField = "K,K";
            }
            else if (samid == "I030.2")
            {
                fieldCount = 7;
                fieldType = "C,C,N,C,N,N,N";
                fieldLength = "8,1,5,20,10,10,10";
                insertSql = "INSERT INTO TIE_I0302(ACCNO,CNTNO,EPRTNO,PNM,DEMAMT,SENDAMT,DELAMT)";
                keyCount = 3;
                deleteSql = "DELETE FROM TIE_I0302 WHERE ACCNO=? AND CNTNO=? AND EPRTNO=?";
                keyField = "K,K,K";
            }
            else if (samid == "I030.3")
            {
                fieldCount = 8;
                fieldType = "C,C,N,N,C,C,N,N2";
                fieldLength = "8,1,5,3,2,350,10,4.3";
                insertSql = "INSERT INTO TIE_I0303(ACCNO,CNTNO,EPRTNO,ELINENO,RECD,DCPS,LINEAMT,LINEQTY)";
                keyCount = 4;
                deleteSql = "DELETE FROM TIE_I0303 WHERE ACCNO=? AND CNTNO=? AND EPRTNO=? AND ELINENO=?";
                keyField = "K,K,K,K";
            }
            else if (samid == "I060.1")
            {
                fieldCount = 6;
                fieldType = "D,N,C,C,C,C";
                fieldLength = "6,4,4,8,20,350";
                insertSql = "INSERT INTO TIE_I0601(REPYM,REPSEQ,FMNO,HOSID,DPTNM,MEMO)";
                keyCount = 2;
                deleteSql = "DELETE FROM TIE_I0601 WHERE REPYM=? AND REPSEQ=?";
                keyField = "K,K";
            }
            else if (samid == "I060.2")
            {
                fieldCount = 13;
                fieldType = "D,N,D,N,C,N,C,C,C,C,C,C,N";
                fieldLength = "6,4,6,4,1,3,1,1,8,140,350,350,7";
                insertSql = "INSERT INTO TIE_I0602(REPYM,REPSEQ,REQYM,REQSEQ,REPDIV,ELINENO,MKDIV,INFODIV,ITEMCD,ITEMNM,INFOSTMT,REQSTMT,BUYQTY)";
                keyCount = 6;
                deleteSql = "DELETE FROM TIE_I0602 WHERE REPYM=? AND REPSEQ=? AND REQYM=? AND REQSEQ=? AND REPDIV=? AND ELINENO=?";
                keyField = "K,K,K,K,K,K";
            }
            else if (samid == "I060.3")
            {
                fieldCount = 7;
                fieldType = "D,N,D,N,C,N,C";
                fieldLength = "6,4,6,4,1,3,3";
                insertSql = "INSERT INTO TIE_I0603(REPYM,REPSEQ,REQYM,REQSEQ,REPDIV,ELINENO,REQDIV)";
                keyCount = 7;
                deleteSql = "DELETE FROM TIE_I0603 WHERE REPYM=? AND REPSEQ=? AND REQYM=? AND REQSEQ=? AND REPDIV=? AND ELINENO=? AND REQDIV=?";
                keyField = "K,K,K,K,K,K,K";
            }
            else
            {
                return;
            }

            // INSERT 문장을 완성한다.
            insertSql += "VALUES";
            for (int i = 0; i < fieldCount; i++)
            {
                if (i == 0)
                {
                    insertSql += "(?";
                }
                else
                {
                    insertSql += ",?";
                }
            }
            insertSql += ")";

            // 값을 잘라 놓는다.
            Dictionary<int, String> val = new Dictionary<int,string>();
            val.Clear();

            String[] fieldTypes = fieldType.Split(',');
            String[] fieldLengths = fieldLength.Split(',');

            int start = 0;
            int end = 0;

            for (int i = 0; i < fieldCount; i++)
            {
                String type = fieldTypes[i];
                String length = fieldLengths[i];

                if (type == "N2")
                {
                    String[] lens = length.Split('.');
                    int len1 = int.Parse(lens[0]);
                    int len2 = int.Parse(lens[1]);
                    int len = len1 + len2;

                    start += end;
                    end = len;

                    Double value = 0;
                    Double.TryParse(StrHelper.SubstringH(data, start, end), out value);
                    Double tmp = 1;
                    for(int j=1;j<=len2;j++){
                        tmp *= 10;
                    }
                    value /= tmp;
                    val[i]=value.ToString();
                }
                else if (type == "N")
                {
                    int len = int.Parse(length);
                    start += end;
                    end = len;

                    Double value = 0;
                    Double.TryParse(StrHelper.SubstringH(data, start, end), out value);
                    val[i] = value.ToString();
                }
                else if (type == "NQ")
                {
                    int len = int.Parse(length);
                    start += end;
                    end = len;

                    String value = count;
                    val[i] = value;
                }
                else if (type == "D")
                {
                    int len = int.Parse(length);
                    start += end;
                    end = len;

                    String value = StrHelper.SubstringH(data, start, end);
                    val[i] = "20" + value;
                }
                else
                {
                    int len = int.Parse(length);
                    start += end;
                    end = len;

                    String value = StrHelper.SubstringH(data, start, end);
                    val[i] = value;
                }
            }

            // 먼저 삭제
            if (keyCount > 0)
            {
                OleDbCommand delCmd = new OleDbCommand(deleteSql, p_conn);
                long idx = 0;
                String[] keyFields = keyField.Split(',');
                for (int i = 0; i < keyCount; i++)
                {
                    if (keyFields[i] == "K")
                    {
                        idx++;
                        String name = "@p" + idx;
                        String value = val[i];
                        delCmd.Parameters.Add(new OleDbParameter(name, value));
                    }
                }
                delCmd.ExecuteNonQuery();
            }

            // INSERT
            OleDbCommand insCmd = new OleDbCommand(insertSql, p_conn);
            for (int i = 0; i < fieldCount; i++)
            {
                String name = "@p" + i;
                String value = val[i];
                insCmd.Parameters.Add(new OleDbParameter(name, value));
            }
            insCmd.ExecuteNonQuery();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.SetISOK_ALL();
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtFolder.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
                this.SetISOK_ALL();
            }
        }

    }
}

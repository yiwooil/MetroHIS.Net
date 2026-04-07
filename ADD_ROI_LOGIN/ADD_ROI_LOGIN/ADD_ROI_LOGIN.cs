using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADD_ROI_LOGIN
{
    public partial class ADD_ROI_LOGIN : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        string m_roi_path = "";

        private const string SERVICE_NAME = "ROIService";

        private bool IsFirst;

        public ADD_ROI_LOGIN()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD_ROI_LOGIN(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }
        private void ADD_ROI_LOGIN_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD_ROI_LOGIN_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            // ROI 설치폴더
            m_roi_path = GetRoiPath();
            // 설치되어 있지 않으면 종료
            if (m_roi_path == "")
            {
                this.Close();
            }
            // ROI 설치폴더를 확인
            lblRoiFolder.Text = m_roi_path;

            GetRoiUidPwd();
        }

        private void GetRoiUidPwd()
        {
            try
            {
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD").CreateSubKey("ROI");
                txtRoiuid.Text = reg.GetValue("UID", "").ToString();
                txtRoipwd.Text = reg.GetValue("PWD", "").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetRoiUidPwd()
        {
            try
            {
                
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD").CreateSubKey("ROI");
                reg.SetValue("UID", txtRoiuid.Text.ToString());
                reg.SetValue("PWD", txtRoipwd.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetRoiPath()
        {
            try
            {
                string path = "C:/ROI_Lite/";
                //RegistryKey reg;
                //reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("ROI");
                //path = reg.GetValue("Path", "").ToString();
                //if (path == "")
                //{
                //    reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("ROI_Lite");
                //    path = reg.GetValue("Path", "").ToString();
                //}
                return path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string roi_uid = txtRoiuid.Text.ToString();
                string roi_pwd = txtRoipwd.Text.ToString();

                if (roi_uid == "" || roi_pwd == "")
                {
                    MessageBox.Show("사용자ID와 비밀번호를 입력하세요.");
                    return;
                }

                // 사용자ID와 비밀번호를 저장한다.
                SetRoiUidPwd();

                // 일단 ROI서비스를 중지시킨다.
                WriteLog("-------------------------------");
                WriteLog("ROI 로그인을 위한 ROI 서비스 종료 시도");
                StopROIService();
                WriteLog("ROI 로그인을 위한 ROI 서비스 종료 성공");

                string cmd_str = "";
                string args = "";

                //// ROI 업그레이드 호출
                //cmd_str = "C:/ROI_Lite/ROI.Expert.Update.exe";
                //args = "ROIService " + roi_uid + " " + roi_pwd;
                //ExecCmd(cmd_str, "C:/ROI_Lite/", args);

                // ROI 로그인
                //     로그인이 다 끝나지 않았는데도 ROI.exe가 종료된다.
                //     로그인을 하면 업그레이드도 같이 한다.
                WriteLog("ROI 로그인 시도");
                cmd_str = m_roi_path + "ROI.exe";
                args = "/args=Login " + roi_uid + " " + roi_pwd;
                ExecCmd(cmd_str, m_roi_path, args);
                WriteLog("ROI 로그인 성공");

                SetLoginYesNo("Y");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetLoginYesNo("N");
            }
            finally
            {
                this.Close();
            }
        }

        private void StopROIService()
        {
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(SERVICE_NAME);
            while (sc.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
            {
                if (sc.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                    sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped); // 중지상태가 될때까지 대기
                }
            }
        }

        //private void StartROIService()
        //{
        //    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(SERVICE_NAME);
        //    while (sc.Status != System.ServiceProcess.ServiceControllerStatus.Running)
        //    {
        //        sc.Start();
        //        sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running); // 실행상태가 될때까지 대기
        //    }
        //}

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.SetLoginYesNo("N");
            this.Close();
        }

        private void SetLoginYesNo(string yesNo)
        {
            try
            {
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD").CreateSubKey("ROI");
                reg.SetValue("LOGIN_YN", yesNo);
                // VB에서 읽을수 있도록 파일에 작성한다.
                string log_file = "C:/Metro/DLL/ROI_LOGIN.txt";
                System.IO.File.WriteAllText(log_file, yesNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteLog(string msg)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("C:/Metro/DLL/Log/");
            if (di.Exists == false)
            {
                di.Create();
            }
            string log_file = "C:/Metro/DLL/Log/ROI_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            System.IO.File.AppendAllText(log_file, now + " " + msg + System.Environment.NewLine);
        }

    }
}

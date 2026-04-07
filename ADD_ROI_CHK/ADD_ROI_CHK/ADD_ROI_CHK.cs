using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADD_ROI_CHK
{
    public partial class ADD_ROI_CHK : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Roibase;
        private String m_ex_msg_code;
        private String m_un_ex_msg_code;

        private const string SERVICE_NAME = "ROIService";


        private bool IsFirst;

        public ADD_ROI_CHK()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Roibase = "";
            m_ex_msg_code = "";
            m_un_ex_msg_code = "";
        }

        public ADD_ROI_CHK(String user, String pwd, String prjcd, String roibase, String ex_msg_code, String un_ex_msg_code)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Roibase = roibase;
            m_ex_msg_code = ex_msg_code;
            m_un_ex_msg_code = un_ex_msg_code;
        }

        private void ADD_ROI_CHK_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD_ROI_CHK_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                string roi_path = GetRoiPath();
                WriteLog("-------------------------------");
                WriteLog("ROI 경로 = " + roi_path);
                if (roi_path != "")
                {
                    string loginYesNo = GetLoginYesNo();
                    WriteLog("ROI 로그인 여부 = " + loginYesNo);
                    if (loginYesNo == "Y")
                    {

                        // 이전에 만들어졌던 점검 결과 파일 삭제
                        //OnMsg("초기화중...");
                        WriteLog("ROI 점검 파일 삭제 시도");
                        DeleteRoiCheckFile(roi_path);
                        WriteLog("ROI 점검 파일 삭제 성공");


                        // ROI 서비스 시작
                        WriteLog("ROI 서비스 시작 시도");
                        OnMsg("ROI서비스 시작중...");
                        bool running = StartROIService();
                        if (running == true)
                        {
                            WriteLog("ROI 서비스 시작 성공");
                        }
                        else
                        {
                            WriteLog("ROI 서비스 시작 실패. 종료.");
                            return;
                        }

                        string cmd_str = "";
                        string args = "";

                        if (m_ex_msg_code != "")
                        {
                            // 점검예외 설정
                            WriteLog("ROI 점검 예외 설정");
                            OnMsg("점검 예외 설정...");
                            cmd_str = roi_path + "ROI.exe";
                            args = "/args=SetExcept 0 " + m_ex_msg_code + "";
                            ExecCmd(cmd_str, roi_path, args);
                            WriteLog("ROI 점검 예외 설정");
                        }
                        else if (m_un_ex_msg_code != "")
                        {
                            // 점검예외 해제
                            WriteLog("ROI 점검 예외 해제");
                            OnMsg("점검 예외 해제...");
                            cmd_str = roi_path + "ROI.exe";
                            args = "/args=SetExcept 1 " + m_un_ex_msg_code + "";
                            ExecCmd(cmd_str, roi_path, args);
                            WriteLog("ROI 점검 예외 해제");
                        }
                        else if (m_Roibase == "")
                        {
                            // 청구파일점검
                            WriteLog("ROI 점검 시도");
                            OnMsg("청구파일 점검중...");
                            cmd_str = roi_path + "ROI.exe";
                            args = "/args=ROICheck \"C:/Metro/DLL/ROIInput.txt\" 1 0 0 0";
                            ExecCmd(cmd_str, roi_path, args);
                            WriteLog("ROI 점검 성공");

                            // VB에서 읽을 수 있도록 파일을 복사한다.
                            if (System.IO.File.Exists(roi_path + "ROIService/ROICheck.txt"))
                            {
                                WriteLog("ROI 점검 결과 파일 복사 시도");
                                // VB에서 JSON 파싱할 때 내용에 \" 이 있으면 오류가 발생함. 이 문자를 없앤다.
                                string roiCheck = System.IO.File.ReadAllText(roi_path + "ROIService/ROICheck.txt", Encoding.Default);
                                System.IO.File.WriteAllText("C:/Metro/DLL/ROICheck.txt", roiCheck.Replace("\\\"", " "), Encoding.Default);
                                WriteLog("ROI 점검 결과 파일 복사 성공");
                            }
                            else
                            {
                                WriteLog("ROI 점검 결과 파일 없음");
                            }
                        }
                        else
                        {
                            // 관련근거
                            WriteLog("ROI 관련 근거 검색 시도");
                            OnMsg("관련근거 검색중...");
                            cmd_str = roi_path + "ROI.exe";
                            args = "/args=ROIBase " + m_Roibase;
                            ExecCmd(cmd_str, roi_path, args);
                            WriteLog("ROI 관련 근거 검색 성공");

                            // VB에서 읽을 수 있도록 파일을 복사한다.
                            if (System.IO.File.Exists(roi_path + "ROIService/ROIBase.txt"))
                            {
                                WriteLog("ROI 관련 근거 검색 파일 복사 시도");
                                //System.IO.File.Copy(roi_path + "ROIService/ROIBase.txt", "C:/Metro/DLL/ROIBase.txt", true);
                                // VB에서 JSON 파싱할 때 내용에 \" 이 있으면 오류가 발생함. 이 문자를 없앤다.
                                string roiBase = System.IO.File.ReadAllText(roi_path + "ROIService/ROIBase.txt", Encoding.Default);
                                System.IO.File.WriteAllText("C:/Metro/DLL/ROIBase.txt", roiBase.Replace("\\\"", " "), Encoding.Default);

                                WriteLog("ROI 관련 근거 검색 파일 복사 성공");
                            }
                            else
                            {
                                WriteLog("ROI 관련 근거 검색 파일 없음");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
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
                //   reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("ROI_Lite");
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

        private string GetLoginYesNo()
        {
            try
            {
                string yesNo = "";
                RegistryKey reg;
                reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADD").CreateSubKey("ROI");
                yesNo = reg.GetValue("LOGIN_YN", "").ToString();
                return yesNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void DeleteRoiCheckFile(string roi_path)
        {
            WriteLog("삭제 대상 파일 = " + roi_path + "ROIService/ROICheck.txt");
            if (System.IO.File.Exists(roi_path + "ROIService/ROICheck.txt"))
            {
                WriteLog("삭제 대상 파일 있음. 삭제시작");
                try
                {
                    System.IO.File.Delete(roi_path + "ROIService/ROICheck.txt");
                    WriteLog("삭제 성공");
                }
                catch (System.IO.IOException ex)
                {
                    // 무시하고 진행
                    WriteLog("삭제 오류" + Environment.NewLine + ex.Message);
                }
            }
            WriteLog("삭제 대상 파일 = " + roi_path + "ROIService/ROIBase.txt");
            if (System.IO.File.Exists(roi_path + "ROIService/ROIBase.txt"))
            {
                try
                {
                    System.IO.File.Delete(roi_path + "ROIService/ROIBase.txt");
                    WriteLog("삭제 성공");
                }
                catch (System.IO.IOException ex)
                {
                    // 무시하고 진행
                    WriteLog("삭제 오류" + Environment.NewLine + ex.Message);
                }
            }
            WriteLog("삭제 대상 파일 = C:/Metro/DLL/ROICheck.txt");
            if (System.IO.File.Exists("C:/Metro/DLL/ROICheck.txt"))
            {
                try
                {
                    System.IO.File.Delete("C:/Metro/DLL/ROICheck.txt");
                    WriteLog("삭제 성공");
                }
                catch (System.IO.IOException ex)
                {
                    // 무시하고 진행
                    WriteLog("삭제 오류" + Environment.NewLine + ex.Message);
                }
            }
            WriteLog("삭제 대상 파일 = C:/Metro/DLL/ROIBase.txt");
            if (System.IO.File.Exists("C:/Metro/DLL/ROIBase.txt"))
            {
                try
                {
                    System.IO.File.Delete("C:/Metro/DLL/ROIBase.txt");
                    WriteLog("삭제 성공");
                }
                catch (System.IO.IOException ex)
                {
                    // 무시하고 진행
                    WriteLog("삭제 오류" + Environment.NewLine + ex.Message);
                }
            }

        }

        //private void StopROIService()
        //{
        //    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(SERVICE_NAME);
        //    while (sc.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
        //    {
        //        if (sc.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
        //        {
        //            sc.Stop();
        //            sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped); // 중지상태가 될때까지 대기
        //        }
        //    }
        //}

        private bool StartROIService()
        {
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(SERVICE_NAME);
            WriteStatusLog(sc, 0);
            if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Running)
            {
                WriteLog("이미 실행 중임.");
                return true;
            }
            else
            {
                bool ret = false;
                int i = 0;
                WriteLog("실행 시키는 중.");
                if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }

                while (true)
                {
                    System.ServiceProcess.ServiceController sc2 = new System.ServiceProcess.ServiceController(SERVICE_NAME);
                    if (sc2.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                    {
                        ret = true;
                        break;
                    }
                    if (++i > 100) break;

                    WriteStatusLog(sc2, i);

                    WriteLog("실행 시키는 중(" + i + ")");
                    OnMsg("실행 시키는 중(" + i + ")");

                    System.Threading.Thread.Sleep(100);

                }

                return ret;
            }
        }

        private void WriteStatusLog(System.ServiceProcess.ServiceController sc, int idx)
        {
            switch (sc.Status)
            {
                case System.ServiceProcess.ServiceControllerStatus.ContinuePending:
                    WriteLog(" > 서비스 다시 시작 중." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.Paused:
                    WriteLog(" > 시비스 일시 정지 상태." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.PausePending:
                    WriteLog(" > 서비스 일시 정지 중." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.Running:
                    WriteLog(" > 서비스 실행 상태." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.StartPending:
                    WriteLog(" > 서비스 시작 중." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.Stopped:
                    WriteLog(" > 서비스 정지 상태." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                case System.ServiceProcess.ServiceControllerStatus.StopPending:
                    WriteLog(" > 서비스 정지 중." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
                default:
                    WriteLog(" > 서비스 상태를 알 수 없습니다." + (idx != 0 ? "(" + idx + ")" : ""));
                    break;
            }
        }

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            WriteLog("    fileName = " + fileName);
            WriteLog("    execfolder = " + execfolder);
            WriteLog("    args = " + args);

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

        private void OnMsg(string msg)
        {
            if (lblMsg.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                lblMsg.BeginInvoke(new Action(() =>
                {
                    lblMsg.Text = msg;
                    lblMsg.Refresh();
                    Application.DoEvents();
                }));
            }
            else
            {
                // 폼에서 호출한 경우
                lblMsg.Text = msg;
                lblMsg.Refresh();
                Application.DoEvents();
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

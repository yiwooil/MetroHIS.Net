using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HIRA.EformEntry.ResponseModel;

namespace ADD7001E
{
    public partial class ADD7001E : Form
    {
        private CHosInfo m_HosInfo;
        private String m_User;
        private String m_Pwd;

        private ContextMenuStrip m_MnuMain;
        private ContextMenuStrip m_MnuRip001Up;
        private ContextMenuStrip m_MnuRip001Dn;
        private ContextMenuStrip m_MnuRii001Up;
        private ContextMenuStrip m_MnuRii001Dn;
        private ContextMenuStrip m_MnuRid001Up;
        private ContextMenuStrip m_MnuRid001Dn;

        public ADD7001E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";

            this.CreatePopupMenu();
        }

        public ADD7001E(String user,String pwd):this()
        {
            m_User = user;
            m_Pwd = pwd;
        }

        private void CreatePopupMenu()
        {
            //
            m_MnuMain = new ContextMenuStrip();
            m_MnuMain.Items.Add(new ToolStripMenuItem("다시 준비,점검", null, new EventHandler(mnuMakeAndCheckDataOne_Click)));
            m_MnuMain.Items.Add(new ToolStripMenuItem("자료 전송", null, new EventHandler(mnuSendDataOne_Click)));
            m_MnuMain.Items.Add(new ToolStripMenuItem("다시 준비", null, new EventHandler(mnuMakeDataOne_Click)));
            m_MnuMain.Items.Add(new ToolStripMenuItem("점검", null, new EventHandler(mnuCheckDataOne_Click)));
            //
            m_MnuRip001Up = new ContextMenuStrip();
            m_MnuRip001Up.Items.Add(new ToolStripMenuItem("PN:입원기간 동안의 환자상태변화", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRip001Up.Items.Add(new ToolStripMenuItem("OP:수술 후 기록", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRip001Up.Items.Add(new ToolStripMenuItem("DEAD:사망기록", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRip001Up.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRip001Up.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsUpItem_Click)));
            //
            m_MnuRip001Dn = new ContextMenuStrip();
            m_MnuRip001Dn.Items.Add(new ToolStripMenuItem("PN:입원기간 동안의 환자상태변화", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRip001Dn.Items.Add(new ToolStripMenuItem("OP:수술 후 기록", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRip001Dn.Items.Add(new ToolStripMenuItem("DEAD:사망기록", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRip001Dn.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRip001Dn.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsDnItem_Click)));
            //
            m_MnuRii001Up = new ContextMenuStrip();
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("CC:주호소", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("PI:현병력", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("CC_DATE:주호소발생시기", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("PHX:과거력", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("FHX:가족력", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("ROS:계통문진", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("PE:신체검진", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("IMP:추정진단", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("TXPLAN:치료계획", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("MODIFY_HX:수정이력", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRii001Up.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsUpItem_Click)));
            //
            m_MnuRii001Dn = new ContextMenuStrip();
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("CC:주호소", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("PI:현병력", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("CC_DATE:주호소발생시기", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("PHX:과거력", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("FHX:가족력", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("ROS:계통문진", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("PE:신체검진", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("IMP:추정진단", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("TXPLAN:치료계획", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("MODIFY_HX:수정이력", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRii001Dn.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsDnItem_Click)));
            //
            m_MnuRid001Up = new ContextMenuStrip();
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("DX_DISECD0:주진단", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("DX_DISECD9:기타진단", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("COMPLAINT:주호소", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("COT:입원경과 및 치료과정 요약", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("OP_INFO:주요 처치.시술 및 수술내용", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("LAB_INFO:검사소견", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("DISCHARGE_PRESCRIBE:퇴원처방내역", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("OUT_STATUS:퇴원 시 환자상태", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("OUT_CARE:퇴원 후 진료계획", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsUpItem_Click)));
            m_MnuRid001Up.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsUpItem_Click)));
            //
            m_MnuRid001Dn = new ContextMenuStrip();
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("DX_DISECD0:주진단", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("DX_DISECD9:기타진단", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("COMPLAINT:주호소", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("COT:입원경과 및 치료과정 요약", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("OP_INFO:주요 처치.시술 및 수술내용", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("LAB_INFO:검사소견", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("DISCHARGE_PRESCRIBE:퇴원처방내역", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("OUT_STATUS:퇴원 시 환자상태", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("OUT_CARE:퇴원 후 진료계획", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("WRT_DT_TM:작성일자시간", null, new EventHandler(mnuInsDnItem_Click)));
            m_MnuRid001Dn.Items.Add(new ToolStripMenuItem("WRT_NM:작성자", null, new EventHandler(mnuInsDnItem_Click)));

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.QueryDaesang();
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

        private void QueryDaesang()
        {
            try
            {
                grdMain.DataSource = null;

                HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();

                string ykiho = m_HosInfo.GetHosId(); // 요양기관기호
                string asmYm = txtAsmYm.Text; // 평가년월

                if (chkLocal.Checked == true)
                {
                    List<CTI83> ti83s = GetTI83List(asmYm);

                    if (ti83s.Count == 0)
                    {
                        MessageBox.Show("해당조건의 데이터가 존재하지 않습니다.");
                        return;
                    }

                    List<CResField> list = new List<CResField>();
                    list.Clear();
                    grdMain.DataSource = list;

                    // 신포괄 정보관리 평가대상 내역 확인
                    foreach (CTI83 ti83 in ti83s)
                    {
                        CResField cFld = new CResField();
                        cFld.SetValues(ti83, m_User);

                        list.Add(cFld);
                        this.RefreshGridMain();
                    }
                }
                else
                {
                    MasterListResponse res = doc.selectNdrgAsmReqList(ykiho, asmYm, "", "", "", "", "", "", "", "1", "300");
                    if (res.Result == false)
                    {
                        string strErrCode = res.ErrorCode;
                        string strErrMesg = res.ErrorMessage;

                        MessageBox.Show("조회오류\n" + strErrCode + ":" + strErrMesg);
                    }
                    else
                    {
                        if (res.Datas.Rows.Count == 0)
                        {
                            MessageBox.Show("해당조건의 데이터가 존재하지 않습니다.");
                            return;
                        }

                        List<CResField> list = new List<CResField>();
                        list.Clear();
                        grdMain.DataSource = list;

                        // 신포괄 정보관리 평가대상 내역 확인
                        for (int i = 0; i < res.Datas.Rows.Count; i++)
                        {
                            CResField cFld = new CResField();
                            cFld.SetValues(res.Datas.Rows[i], m_User);

                            list.Add(cFld);
                            this.RefreshGridMain();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ADD7001E_Load(object sender, EventArgs e)
        {
            try
            {
                m_HosInfo = new CHosInfo();
                m_HosInfo.SetInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.MakeData();
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

        private void MakeData()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    list[row].PROCESS = "준비중";
                    this.RefreshGridMain();
                    list[row].SetList(false, chkLocal.Checked);
                    list[row].PROCESS = "준비완료";
                    this.RefreshGridMain();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MakeDataOne()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                int row = grdMainView.FocusedRowHandle;

                //
                list[row].PROCESS = "준비중";
                this.RefreshGridMain();
                list[row].SetList(true, chkLocal.Checked);
                list[row].PROCESS = "준비완료";
                this.RefreshGridMain();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckDataOne()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                int row = grdMainView.FocusedRowHandle;

                this.CheckDataRow(list, row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckData()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;
                    this.CheckDataRow(list, row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckDataRow(List<CResField> p_list, int p_row)
        {
            p_list[p_row].PROCESS = "점검중";
            this.RefreshGridMain();

            if ("ODD001".Equals(p_list[p_row].SC_DTL_FOM_CD))
            {
                // 첨부파일이 있어야함.
                if ("".Equals(p_list[p_row].LOCAL_FILE_PTH_COUNT))
                {
                    p_list[p_row].PROCESS = "파일없음";
                    this.RefreshGridMain();
                    return;
                }
            }

            HIRA.EformEntry.Model.Document doc = p_list[p_row].GetDocument(m_HosInfo.GetHosId());
            if (doc == null)
            {
                p_list[p_row].PROCESS = "자료없음";
            }
            else
            {
                HIRA.EformEntry.MultiJsonConvertResponse res = doc.ToJson();
                if (res.Result)
                {
                    // 성공
                    p_list[p_row].PROCESS = "점검완료";
                }
                else
                {
                    p_list[p_row].PROCESS = "점검오류";
                    string errMsg = "";
                    for (int i = 0; i < res.Count; i++)
                    {
                        errMsg += res[i].ErrorMessage + "\n";
                    }
                    p_list[p_row].ERR_MSG = errMsg;
                }
            }
            this.RefreshGridMain();
        }

        private void grdMainView_Click(object sender, EventArgs e)
        {
            // 상세내역 clear
            grdSub.DataSource = null;
            RefreshGridSub();
            txtMsg.Text = "";
            // 파일내역 clear
            grdFiles.DataSource = null;
            RefreshGridFiles();
            // 메지지 내역 clear
            txtErrMsg.Text = "";
            //
            List<CResField> lst = (List<CResField>)grdMain.DataSource;
            if (lst == null) return;
            CResField fld = lst[grdMainView.FocusedRowHandle];
            if (fld == null) return;
            // 오류내용
            txtErrMsg.Text = fld.ERR_MSG;
            // 첨부파일 리스트
            grdFiles.DataSource = fld.GetFileList();
            RefreshGridFiles();
            // 상세내용
            List<CTBL_FOM_CZITM> subList = fld.GetList();
            grdSub.DataSource = subList;
            RefreshGridSub();
            txtMsg.Text = "";
            // 상세내용에 사용하는 팝업메뉴
            //CreateSubPopupMenu(fld.SC_DTL_FOM_CD);           
        }
        /*
        private void CreateSubPopupMenu(string p_SC_DTL_FOM_CD)
        {
            ContextMenu cm = new ContextMenu();

            if ("RIP001".Equals(p_SC_DTL_FOM_CD))
            {
                // RIP001 경과기록
                MenuItem[] itemInsUp = new MenuItem[]
                {
                    new MenuItem("PN:입원기간 동안의 환자상태변화", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("OP:수술 후 기록", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("DEAD:사망기록", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsUpItem_Click))
                };
                MenuItem[] itemInsDn = new MenuItem[]
                {
                    new MenuItem("PN:입원기간 동안의 환자상태변화", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("OP:수술 후 기록", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("DEAD:사망기록", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsDnItem_Click))
                };
                cm.MenuItems.Add("위에 추가", itemInsUp);
                cm.MenuItems.Add("아래에 추가", itemInsDn);
            }
            else if ("RII001".Equals(p_SC_DTL_FOM_CD))
            {
                // RII001 입원기록
                MenuItem[] itemInsUp = new MenuItem[]
                {
                    new MenuItem("CC:주호소", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("PI:현병력", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("CC_DATE:주호소발생시기", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("PHX:과거력", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("FHX:가족력", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("ROS:계통문진", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("PE:신체검진", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("IMP:추정진단", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("TXPLAN:치료계획", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("MODIFY_HX:수정이력", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsUpItem_Click))
                };
                MenuItem[] itemInsDn = new MenuItem[]
                {
                    new MenuItem("CC:주호소", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("PI:현병력", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("CC_DATE:주호소발생시기", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("PHX:과거력", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("FHX:가족력", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("ROS:계통문진", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("PE:신체검진", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("IMP:추정진단", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("TXPLAN:치료계획", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("MODIFY_HX:수정이력", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsDnItem_Click))
                };
                cm.MenuItems.Add("위에 추가", itemInsUp);
                cm.MenuItems.Add("아래에 추가", itemInsDn);
            }
            else if ("RID001".Equals(p_SC_DTL_FOM_CD))
            {
                // RID001 퇴원요약
                MenuItem[] itemInsUp = new MenuItem[]
                {
                    new MenuItem("DX_DISECD0:주진단", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("DX_DISECD9:기타진단", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("COMPLAINT:주호소", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("COT:입원경과 및 치료과정 요약", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("OP_INFO:주요 처치.시술 및 수술내용", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("LAB_INFO:검사소견", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("DISCHARGE_PRESCRIBE:퇴원처방내역", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("OUT_STATUS:퇴원 시 환자상태", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("OUT_CARE:퇴원 후 진료계획", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsUpItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsUpItem_Click))
                };
                MenuItem[] itemInsDn = new MenuItem[]
                {
                    new MenuItem("DX_DISECD0:주진단", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("DX_DISECD9:기타진단", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("COMPLAINT:주호소", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("COT:입원경과 및 치료과정 요약", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("OP_INFO:주요 처치.시술 및 수술내용", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("LAB_INFO:검사소견", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("DISCHARGE_PRESCRIBE:퇴원처방내역", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("OUT_STATUS:퇴원 시 환자상태", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("OUT_CARE:퇴원 후 진료계획", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_DT_TM:작성일자시간", new EventHandler(mnuInsDnItem_Click)),
                    new MenuItem("WRT_NM:작성자", new EventHandler(mnuInsDnItem_Click))
                };
                cm.MenuItems.Add("위에 추가", itemInsUp);
                cm.MenuItems.Add("아래에 추가", itemInsDn);
            }
            cm.MenuItems.Add("삭제", new EventHandler(mnuDelItem_Click));
            grdSub.ContextMenu = cm;
        }
        */
        private void mnuInsUpItem_Click(object sender, EventArgs e)
        {
            // 위에 추가
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            int row = grdSubView.FocusedRowHandle;
            InsSubRow(row, itm);
        }

        private void mnuInsDnItem_Click(object sender, EventArgs e)
        {
            // 아래에 추가
            ToolStripMenuItem itm = sender as ToolStripMenuItem;
            int row = grdSubView.FocusedRowHandle;
            InsSubRow(row + 1, itm);
        }

        private void mnuDelItem_Click(object sender, EventArgs e)
        {
            List<CResField> mainList = (List<CResField>)grdMain.DataSource;
            if (mainList == null) return;
            MenuItem itm = sender as MenuItem;
            string[] itmText = itm.Text.ToString().Split(':');
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
            int row = grdSubView.FocusedRowHandle;
            lst.RemoveAt(row);
            this.InsertSubData(mainList[grdMainView.FocusedRowHandle].REQ_DATA_NO);
            this.RefreshGridSub();
            //
            txtMsg.Text = "";
            CTBL_FOM_CZITM tbl = lst[grdMainView.FocusedRowHandle];
            txtMsg.Text = tbl.DTL_TXT;
        }


        private void InsSubRow(int p_row, ToolStripMenuItem p_Item)
        {
            List<CResField> mainList = (List<CResField>)grdMain.DataSource;
            if (mainList == null) return;
            string[] itmText = p_Item.Text.ToString().Split(':');
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
            CTBL_FOM_CZITM tbl = new CTBL_FOM_CZITM("", itmText[0], itmText[1], "");
            lst.Insert(p_row, tbl);
            this.InsertSubData(mainList[grdMainView.FocusedRowHandle].REQ_DATA_NO);
            grdSubView.FocusedRowHandle = p_row;
            this.RefreshGridSub();
            //
            txtMsg.Text = tbl.DTL_TXT;
        }

        private void InsertSubData(string p_REQ_DATA_NO)
        {
            try
            {
                List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
                int cnt = lst.Count;
                //
                string sql = "";
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string strSysDate = CUtil.GetSysDate(conn);
                    string strSysTime = CUtil.GetSysTime(conn);

                    OleDbTransaction tran = conn.BeginTransaction();

                    try
                    {

                        // 삭제하고
                        sql = "";
                        sql += System.Environment.NewLine + "DELETE TI83A";
                        sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                        OleDbCommand dcmd = new OleDbCommand(sql, conn, tran);
                        dcmd.ExecuteNonQuery();

                        // 입력
                        for (int i = 0; i < cnt; i++)
                        {
                            CTBL_FOM_CZITM tbl = lst[i];
                            tbl.SORT_SNO = (i + 1).ToString();

                            sql = "";
                            sql += "INSERT INTO TI83A(REQ_DATA_NO, SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT, LABEL_NM, FOM_CZITM_CD, SYSDT, SYSTM, EMPID)";
                            sql += "VALUES(?,?,?,?,?,?,?,?,?,?)";

                            // TSQL문장과 Connection 객체를 지정   
                            OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                            cmd.Parameters.Add(new OleDbParameter("@p1", p_REQ_DATA_NO));
                            cmd.Parameters.Add(new OleDbParameter("@p2", tbl.SORT_SNO));
                            cmd.Parameters.Add(new OleDbParameter("@p3", tbl.YADM_TRMN_ID));
                            cmd.Parameters.Add(new OleDbParameter("@p4", tbl.YADM_TRMN_NM));
                            cmd.Parameters.Add(new OleDbParameter("@p5", tbl.DTL_TXT));
                            cmd.Parameters.Add(new OleDbParameter("@p6", tbl.LABEL_NM));
                            cmd.Parameters.Add(new OleDbParameter("@p7", ""));
                            cmd.Parameters.Add(new OleDbParameter("@p8", strSysDate));
                            cmd.Parameters.Add(new OleDbParameter("@p9", strSysTime));
                            cmd.Parameters.Add(new OleDbParameter("@p10", m_User));

                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e == null) return;
            int row = e.RowHandle;
            if (row < 0) return;

            if ("gcPROCESS".Equals(e.Column.Name))
            {
                if (e.CellValue == null) return;
                String val = e.CellValue.ToString();
                if ("자료없음".Equals(val) || "자료없음(2)".Equals(val) || "파일없음".Equals(val) || "점검오류".Equals(val) || "전송오류".Equals(val))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
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

        private void RefreshGridSub()
        {
            if (grdSub.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridFiles()
        {
            if (grdFiles.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdFiles.BeginInvoke(new Action(() => grdFilesView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdFilesView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnQueryAndMakeAndCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() +  " 중입니다.");
                this.QueryDaesang();
                this.MakeData();
                this.CheckData();
                CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.CheckData();
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.SendData();
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

        private void SendData()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                for (int row = 0; row < grdMainView.DataRowCount; row++)
                {
                    grdMainView.FocusedRowHandle = row;

                    this.SendData(list, row);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendData(List<CResField> list, int row)
        {
            try
            {
                list[row].PROCESS = "전송중";
                this.RefreshGridMain();

                if ("ODD001".Equals(list[row].SC_DTL_FOM_CD))
                {
                    // 첨부파일이 있어야함.
                    if ("".Equals(list[row].LOCAL_FILE_PTH_COUNT))
                    {
                        list[row].PROCESS = "파일없음";
                        this.RefreshGridMain();
                        return;
                    }
                }

                HIRA.EformEntry.Model.Document doc = list[row].GetDocument(m_HosInfo.GetHosId());
                if (doc == null)
                {
                    list[row].PROCESS = "자료없음";
                }
                else
                {

                    HIRA.EformEntry.MultiJsonConvertResponse res = doc.ToJson();
                    if (res.Result)
                    {
                        // 성공
                        list[row].PROCESS = "점검완료";

                        HIRA.EformEntry.ResponseModel.MultiMasterResponse master = doc.createDoc();
                        if (master.Result)
                        {
                            // 성공
                            list[row].PROCESS = "전송완료";
                        }
                        else
                        {
                            if (master.Count < 1)
                            {
                                list[row].PROCESS = "자료없음(2)";
                            }
                            else
                            {
                                list[row].PROCESS = "전송오류";
                                string errMsg = "";
                                for (int i = 0; i < master.Count; i++)
                                {
                                    errMsg += master[i].ErrorMessage + "\n";
                                }
                                list[row].ERR_MSG = errMsg;
                            }
                        }

                    }
                    else
                    {
                        list[row].PROCESS = "점검오류";
                        string errMsg = "";
                        for (int i = 0; i < res.Count; i++)
                        {
                            errMsg += res[i].ErrorMessage + "\n";
                        }
                        list[row].ERR_MSG = errMsg;
                    }

                }
                this.RefreshGridMain();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendDataOne()
        {
            try
            {
                List<CResField> list = (List<CResField>)grdMain.DataSource;
                int row = grdMainView.FocusedRowHandle;

                this.SendData(list, row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void mnuMakeAndCheckDataOne_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.MakeDataOne();
                this.CheckDataOne();
                grdMainView_Click(grdMainView, new EventArgs());
                Cursor.Current = Cursors.Default;
                MessageBox.Show("선택된 환자 자료생성,점검이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuSendDataOne_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.SendDataOne();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("선택된 환자 자료전송이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuMakeDataOne_Click(object sender, EventArgs e)
        {
            // 다시준비
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.MakeDataOne();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("선택된 환자 자료생성이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuCheckDataOne_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.CheckDataOne();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("선택된 환자 자료 점검이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

        }

        private void grdSubView_Click(object sender, EventArgs e)
        {
            txtMsg.Text = "";
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
            if (lst == null) return;
            CTBL_FOM_CZITM tbl = lst[grdSubView.FocusedRowHandle];
            if (tbl == null) return;
            txtMsg.Text = tbl.DTL_TXT;
        }

        private bool m_TextChanged;

        private void txtMsg_Enter(object sender, EventArgs e)
        {
            m_TextChanged = false;
        }

        private void txtMsg_TextChanged(object sender, EventArgs e)
        {
            m_TextChanged = true; 
        }

        private void txtMsg_Leave(object sender, EventArgs e)
        {
            if (chkLocal.Checked)
            {
                //MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                return;
            }

            if (m_TextChanged == true)
            {
                SaveSubData();
            }
        }

        private void SaveSubData()
        {
            try
            {
                List<CResField> mainList = (List<CResField>)grdMain.DataSource;
                if (mainList == null) return;
                CResField fld = mainList[grdMainView.FocusedRowHandle];
                if (fld == null) return;
                //
                if ("".Equals(fld.REQ_DATA_NO)) return;
                //
                List<CTBL_FOM_CZITM> subList = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
                if (subList == null) return;
                CTBL_FOM_CZITM tbl = subList[grdSubView.FocusedRowHandle];
                if (tbl == null) return;
                //
                if ("".Equals(tbl.SORT_SNO)) return;
                //
                string sql = "";
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    sql = "";
                    sql += "UPDATE TI83A";
                    sql += "   SET DTL_TXT='" + txtMsg.Text.ToString() + "'";
                    sql += " WHERE REQ_DATA_NO='" + fld.REQ_DATA_NO + "'";
                    sql += "   AND SORT_SNO=" + tbl.SORT_SNO + "";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.ExecuteNonQuery();
                }
                //
                for (int i = 0; i < subList.Count; i++)
                {
                    if (subList[i].SORT_SNO.Equals(tbl.SORT_SNO))
                    {
                        subList[i].DTL_TXT = txtMsg.Text.ToString();
                        RefreshGridSub();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdMainView_MouseDown(object sender, MouseEventArgs e)
        {
            
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Button == System.Windows.Forms.MouseButtons.Right && !view.GridControl.IsFocused)
            {
                view.GridControl.Select();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // focus 줄 변경
                view.FocusedRowHandle = view.CalcHitInfo(new Point(e.X, e.Y)).RowHandle;
                // 우측마추스 메뉴 띄우기
                m_MnuMain.Show(this, new Point(e.X, panel2.Top + e.Y));
                // 클릭이벤트 발생
                this.grdMainView_Click(sender, new EventArgs());
            }
            
        }

        private void btnFileAdd_Click(object sender, EventArgs e)
        {
            // 첨부파일을 선택해야함.
            try
            {
                if (chkLocal.Checked)
                {
                    MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                    return;
                }

                List<CResField> mainList = (List<CResField>)grdMain.DataSource;
                if (mainList == null) return;
                int row = grdMainView.FocusedRowHandle;
                string strREQ_DATA_NO = mainList[row].REQ_DATA_NO;
                //
                OpenFileDialog diag = new OpenFileDialog();
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    string strLOCAL_FILE_PTH = diag.FileName;
                    //
                    List<CLOCAL_FILE_PTH> lst = (List<CLOCAL_FILE_PTH>)grdFiles.DataSource;
                    CLOCAL_FILE_PTH file = new CLOCAL_FILE_PTH();
                    file.SEQ_NO = (lst.Count + 1).ToString();
                    file.LOCAL_FILE_PTH = strLOCAL_FILE_PTH;
                    lst.Add(file);
                    this.InsertFileList(strREQ_DATA_NO);
                    this.RefreshGridFiles();
                    this.CheckDataRow(mainList, row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFileDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkLocal.Checked)
                {
                    MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                    return;
                }

                List<CResField> mainList = (List<CResField>)grdMain.DataSource;
                if (mainList == null) return;
                int row = grdMainView.FocusedRowHandle;
                string strREQ_DATA_NO = mainList[row].REQ_DATA_NO;
                //
                List<CLOCAL_FILE_PTH> lst = (List<CLOCAL_FILE_PTH>)grdFiles.DataSource;
                if (lst == null) return;
                int fileRow = grdFilesView.FocusedRowHandle;
                lst.RemoveAt(fileRow);
                this.InsertFileList(strREQ_DATA_NO);
                this.RefreshGridFiles();
                this.CheckDataRow(mainList, row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertFileList(string p_REQ_DATA_NO)
        {
            try
            {
                List<CLOCAL_FILE_PTH> lst = (List<CLOCAL_FILE_PTH>)grdFiles.DataSource;
                int cnt = lst.Count;
                //
                string sql = "";
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string strSysDate = CUtil.GetSysDate(conn);
                    string strSysTime = CUtil.GetSysTime(conn);


                    OleDbTransaction tran = conn.BeginTransaction();

                    try
                    {
                        // 삭제하고
                        sql = "";
                        sql += System.Environment.NewLine + "DELETE TI83B";
                        sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + p_REQ_DATA_NO + "'";
                        OleDbCommand dcmd = new OleDbCommand(sql, conn, tran);
                        dcmd.ExecuteNonQuery();

                        // 입력
                        for (int i = 0; i < cnt; i++)
                        {
                            CLOCAL_FILE_PTH tbl = lst[i];
                            tbl.SEQ_NO = (i + 1).ToString();

                            sql = "";
                            sql += "INSERT INTO TI83B(REQ_DATA_NO, SEQ_NO, LOCAL_FILE_PTH, SYSDT, SYSTM, EMPID)";
                            sql += "VALUES(?,?,?,?,?,?)";

                            // TSQL문장과 Connection 객체를 지정   
                            OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                            cmd.Parameters.Add(new OleDbParameter("@p1", p_REQ_DATA_NO));
                            cmd.Parameters.Add(new OleDbParameter("@p2", tbl.SEQ_NO));
                            cmd.Parameters.Add(new OleDbParameter("@p3", tbl.LOCAL_FILE_PTH));
                            cmd.Parameters.Add(new OleDbParameter("@p4", strSysDate));
                            cmd.Parameters.Add(new OleDbParameter("@p5", strSysTime));
                            cmd.Parameters.Add(new OleDbParameter("@p6", m_User));

                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnItemDel_Click(object sender, EventArgs e)
        {
            if (chkLocal.Checked)
            {
                MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                return;
            }

            List<CResField> mainList = (List<CResField>)grdMain.DataSource;
            if (mainList == null) return;
            string strREQ_DATA_NO = mainList[grdMainView.FocusedRowHandle].REQ_DATA_NO;
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdSub.DataSource;
            int row = grdSubView.FocusedRowHandle;
            lst.RemoveAt(row);
            this.InsertSubData(strREQ_DATA_NO);
            this.RefreshGridSub();
            //
            txtMsg.Text = "";
            //if (row >= lst.Count) row = lst.Count - 1;
            //grdSubView.FocusedRowHandle = row;
            //this.RefreshGridSub();
            CTBL_FOM_CZITM tbl = lst[grdSubView.FocusedRowHandle];
            txtMsg.Text = tbl.DTL_TXT;
        }

        private int m_MouseX;
        private int m_MouseY;

        private void btnItemAddUp_Click(object sender, EventArgs e)
        {
            if (chkLocal.Checked)
            {
                MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                return;
            }

            List<CResField> lst = (List<CResField>)grdMain.DataSource;
            if (lst == null) return;
            CResField fld = lst[grdMainView.FocusedRowHandle];
            if (fld == null) return;
            // 상세내용에 사용하는 팝업메뉴
            if ("RIP001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRip001Up.Show(btnItemAddUp, new Point(m_MouseX, m_MouseY));
            }
            else if ("RII001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRii001Up.Show(btnItemAddUp, new Point(m_MouseX, m_MouseY));
            }
            else if ("RID001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRid001Up.Show(btnItemAddUp, new Point(m_MouseX, m_MouseY));
            }
        }

        private void btnItemAddDn_Click(object sender, EventArgs e)
        {
            if (chkLocal.Checked)
            {
                MessageBox.Show("조회만 가능합니다. 사용할 수 없습니다.");
                return;
            }

            List<CResField> lst = (List<CResField>)grdMain.DataSource;
            if (lst == null) return;
            CResField fld = lst[grdMainView.FocusedRowHandle];
            if (fld == null) return;
            // 상세내용에 사용하는 팝업메뉴
            if ("RIP001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRip001Dn.Show(btnItemAddDn, new Point(m_MouseX, m_MouseY));
            }
            else if ("RII001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRii001Dn.Show(btnItemAddDn, new Point(m_MouseX, m_MouseY));
            }
            else if ("RID001".Equals(fld.SC_DTL_FOM_CD))
            {
                m_MnuRid001Dn.Show(btnItemAddDn, new Point(m_MouseX, m_MouseY));
            }
        }

        private void btnItemAddUp_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseX = e.X;
            m_MouseY = e.Y;
        }

        private void btnItemAddDn_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseX = e.X;
            m_MouseY = e.Y;
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

        private List<CTI83> GetTI83List(string asmYm)
        {
            List<CTI83> list = new List<CTI83>();

            string sql = "";

            sql = "";
            sql += System.Environment.NewLine + "SELECT * ";
            sql += System.Environment.NewLine + "  FROM TI83 A";
            sql += System.Environment.NewLine + " WHERE A.ASM_YN='" + asmYm + "'";

            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTI83 i83 = new CTI83();
                    i83.REQ_DATA_NO = reader["REQ_DATA_NO"].ToString();
                    i83.REQ_STA_DD = reader["REQ_STA_DD"].ToString();
                    i83.REQ_END_DD = reader["REQ_END_DD"].ToString();
                    i83.REQ_CLOS_YN = reader["REQ_CLOS_YN"].ToString();
                    i83.SUPL_DATA_FOM_CD = reader["SUPL_DATA_FOM_CD"].ToString();
                    i83.SUPL_DATA_FOM_CD_NM = reader["SUPL_DATA_FOM_CD_NM"].ToString();
                    i83.SC_DTL_FOM_CD = reader["SC_DTL_FOM_CD"].ToString();
                    i83.SC_DTL_FOM_CD_NM = reader["SC_DTL_FOM_CD_NM"].ToString();
                    i83.ASM_YM = reader["ASM_YM"].ToString();
                    i83.DMD_NO = reader["DMD_NO"].ToString();
                    i83.RCV_NO = reader["RCV_NO"].ToString();
                    i83.RCV_YR = reader["RCV_YR"].ToString();
                    i83.BILL_SNO = reader["BILL_SNO"].ToString();
                    i83.SP_SNO = reader["SP_SNO"].ToString();
                    i83.INSUP_TP_CD = reader["INSUP_TP_CD"].ToString();
                    i83.HOSP_RNO = reader["HOSP_RNO"].ToString();
                    i83.PAT_NM = reader["PAT_NM"].ToString();
                    i83.PAT_BTH = reader["PAT_BTH"].ToString();
                    i83.PAT_SEX = reader["PAT_SEX"].ToString();
                    i83.DIAG_YM = reader["DIAG_YM"].ToString();
                    i83.RCV_DD = reader["RCV_DD"].ToString();
                    i83.RECU_FR_DD = reader["RECU_FR_DD"].ToString();
                    i83.RECU_END_DD = reader["RECU_END_DD"].ToString();
                    i83.DGSBJT_CD_NM = reader["DGSBJT_CD_NM"].ToString();
                    i83.MSICK_SICK_SYM = reader["MSICK_SICK_SYM"].ToString();
                    i83.KOR_SICK_NM = reader["KOR_SICK_NM"].ToString();
                    i83.SMIT_YN = reader["SMIT_YN"].ToString();
                    i83.SMIT_FDEC_DD = reader["SMIT_FDEC_DD"].ToString();
                    i83.SYSDT = reader["SYSDT"].ToString();
                    i83.SYSTM = reader["SYSTM"].ToString();
                    i83.EMPID = reader["EMPID"].ToString();

                    list.Add(i83);
                }
                reader.Close();
            }
            return list;
        }
    }
}

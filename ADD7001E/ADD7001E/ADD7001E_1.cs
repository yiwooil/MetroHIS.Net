using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7001E
{
    public partial class ADD7001E_1 : Form
    {
        public event EventHandler ChangedValue = new EventHandler((e, a) => { });
        public event EventHandler NextClick = new EventHandler((e, a) => { });
        public event EventHandler PrevClick = new EventHandler((e, a) => { });

        public string m_User;
        public string m_REQ_DATA_NO;
        public string m_SC_DTL_FOM_CD
        {
            set
            {
                this.CreatePopupMenu(value);
            }
        }

        private string m_SORT_SNO;
        private bool m_TextChanged;


        public ADD7001E_1()
        {
            InitializeComponent();
            m_TextChanged = false;
        }

        public void InitWork()
        {
            grdMainView.FocusedRowHandle = 0;
            //
            txtMsg.Text = "";
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
            CTBL_FOM_CZITM tbl = lst[grdMainView.FocusedRowHandle];

            m_SORT_SNO = tbl.SORT_SNO;
            txtMsg.Text = tbl.DTL_TXT;
        }

        private void grdMainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtMsg.Text = "";
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
            if (lst == null) return;
            CTBL_FOM_CZITM tbl = lst[grdMainView.FocusedRowHandle];
            if (tbl == null) return;

            m_SORT_SNO = tbl.SORT_SNO;
            txtMsg.Text = tbl.DTL_TXT;
        }

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
            if (m_TextChanged == true)
            {
                SaveData();
            }
        }

        private void SaveData()
        {
            try
            {
                if ("".Equals(m_REQ_DATA_NO)) return;
                if ("".Equals(m_SORT_SNO)) return;

                string sql = "";
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    sql = "";
                    sql += "UPDATE TI83A";
                    sql += "   SET DTL_TXT='" + txtMsg.Text.ToString() + "'";
                    sql += " WHERE REQ_DATA_NO='" + m_REQ_DATA_NO + "'";
                    sql += "   AND SORT_SNO=" + m_SORT_SNO + "";

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    cmd.ExecuteNonQuery();
                }
                //
                List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
                int cnt = lst.Count;
                for (int i = 0; i < cnt; i++)
                {
                    CTBL_FOM_CZITM tbl = lst[i];
                    if (tbl.SORT_SNO.Equals(m_SORT_SNO))
                    {
                        tbl.DTL_TXT = txtMsg.Text.ToString();
                        RefreshGridMain();
                        break;
                    }
                }
                // 안해도 되네
                //ChangedValueEventArgs args = new ChangedValueEventArgs();
                //args.REQ_DATA_NO = m_REQ_DATA_NO;
                //ChangedValue(this, args);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InsertData()
        {
            try
            {
                List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
                int cnt = lst.Count;
                //
                string sql = "";
                string strConn = DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    OleDbTransaction tran = conn.BeginTransaction();

                    try
                    {
                        string strSysDate = CUtil.GetSysDate(conn);
                        string strSysTime = CUtil.GetSysTime(conn);

                        // 삭제하고
                        sql = "";
                        sql += System.Environment.NewLine + "DELETE TI83A";
                        sql += System.Environment.NewLine + " WHERE REQ_DATA_NO='" + m_REQ_DATA_NO + "'";
                        OleDbCommand dcmd = new OleDbCommand(sql, conn, tran);
                        dcmd.ExecuteNonQuery();

                        // 입력
                        for (int i = 0; i < cnt; i++)
                        {
                            CTBL_FOM_CZITM tbl = lst[i];
                            tbl.SORT_SNO = (i + 1).ToString();

                            /*
                            sql = "";
                            sql += "INSERT INTO TI83A(REQ_DATA_NO, SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT, LABEL_NM, FOM_CZITM_CD, SYSDT, SYSTM, EMPID)";
                            sql += "VALUES('" + m_REQ_DATA_NO + "'," + tbl.SORT_SNO + ",'" + tbl.YADM_TRMN_ID + "','" + tbl.YADM_TRMN_NM + "','" + tbl.DTL_TXT + "','" + tbl.LABEL_NM + "', '', CONVERT(VARCHAR,GETDATE(),112), REPLACE(CONVERT(VARCHAR,GETDATE(),8),':',''), '" + m_User + "')";

                            // TSQL문장과 Connection 객체를 지정   
                            OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                            cmd.ExecuteNonQuery();
                            */
                            sql = "";
                            sql += "INSERT INTO TI83A(REQ_DATA_NO, SORT_SNO, YADM_TRMN_ID, YADM_TRMN_NM, DTL_TXT, LABEL_NM, FOM_CZITM_CD, SYSDT, SYSTM, EMPID)";
                            sql += "VALUES(?,?,?,?,?,?,?,?,?,?)";

                            // TSQL문장과 Connection 객체를 지정   
                            OleDbCommand cmd = new OleDbCommand(sql, conn, tran);

                            cmd.Parameters.Add(new OleDbParameter("@p1", m_REQ_DATA_NO));
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

        private void CreatePopupMenu(string p_SC_DTL_FOM_CD)
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
                cm.MenuItems.Add("아래에 추가",itemInsDn);
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
            grdMain.ContextMenu = cm;
        }

        private void mnuInsUpItem_Click(object sender, EventArgs e)
        {
            // 위에 추가
            MenuItem itm = sender as MenuItem;
            int row = grdMainView.FocusedRowHandle;
            InsRow(row, itm);
        }

        private void mnuInsDnItem_Click(object sender, EventArgs e)
        {
            // 아래에 추가
            MenuItem itm = sender as MenuItem;
            int row = grdMainView.FocusedRowHandle;
            InsRow(row + 1, itm);
        }

        private void InsRow(int p_row, MenuItem p_Item)
        {
            string[] itmText = p_Item.Text.ToString().Split(':');
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
            CTBL_FOM_CZITM tbl = new CTBL_FOM_CZITM("", itmText[0], itmText[1], "");
            lst.Insert(p_row, tbl);
            this.InsertData();
            grdMainView.FocusedRowHandle = p_row;
            this.RefreshGridMain();
            //
            m_SORT_SNO = tbl.SORT_SNO;
            txtMsg.Text = tbl.DTL_TXT;
        }

        private void mnuDelItem_Click(object sender, EventArgs e)
        {
            MenuItem itm = sender as MenuItem;
            string[] itmText = itm.Text.ToString().Split(':');
            List<CTBL_FOM_CZITM> lst = (List<CTBL_FOM_CZITM>)grdMain.DataSource;
            int row = grdMainView.FocusedRowHandle;
            lst.RemoveAt(row);
            this.InsertData();
            this.RefreshGridMain();
            //
            txtMsg.Text = "";
            CTBL_FOM_CZITM tbl = lst[grdMainView.FocusedRowHandle];
            m_SORT_SNO = tbl.SORT_SNO;
            txtMsg.Text = tbl.DTL_TXT;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            PrevClick(this, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextClick(this, e);
        }

    }
}

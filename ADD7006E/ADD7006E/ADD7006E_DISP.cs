using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7006E
{
    public partial class ADD7006E_DISP : Form
    {
        public string KIND = "";
        public event EventHandler RequestData;
        public event EventHandler RequestPrevPtnt;
        public event EventHandler RequestNextPtnt;
        public event EventHandler RequestPrevForm;
        public event EventHandler RequestNextForm;

        private int m_VisibleColumnCount = 14;

        private ADD7006E_DISP()
        {
            InitializeComponent();
        }

        public ADD7006E_DISP(string p_kind)
            : base()
        {
            InitializeComponent();
            KIND = p_kind;
            
        }

        private void ADD7006E_Load(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.RequestData != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.dataOk = false;
                RequestData(this, args);
                if (args.dataOk == true) ShowData(args.data);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.RequestPrevPtnt != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.dataOk = false;
                RequestPrevPtnt(this, args);
                if (args.dataOk == true) ShowData(args.data);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.RequestNextPtnt != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.dataOk = false;
                RequestNextPtnt(this, args);
                if (args.dataOk == true) ShowData(args.data);
            }
        }

        private void btnPrevForm_Click(object sender, EventArgs e)
        {
            if (this.RequestPrevForm != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.dataOk = false;
                RequestPrevForm(this, args);
                if (args.dataOk == true) ShowData(args.data, args.kind);
            }
        }

        private void btnNextForm_Click(object sender, EventArgs e)
        {
            if (this.RequestNextForm != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.dataOk = false;
                RequestNextForm(this, args);
                if (args.dataOk == true) ShowData(args.data, args.kind);
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

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
            */
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                txtMsg.Text = e.CellValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdMainView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn == null) return;
            if (grdMainView.FocusedRowHandle < 0) return;
            try
            {
                txtMsg.Text = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdMainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdMainView.FocusedColumn == null) return;
            if (e.FocusedRowHandle < 0) return;
            try
            {
                txtMsg.Text = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, grdMainView.FocusedColumn).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowData(CData p_data, string p_kind)
        {
            KIND = p_kind;
            ShowData(p_data);
        }

        private void ShowData(CData p_data)
        {
            txtEprtno.Text = p_data.EPRTNO;
            txtPid.Text = p_data.PID;
            txtPnm.Text = p_data.PNM;
            txtStedt.Text = p_data.STEDT;
            txtBdedt.Text = p_data.BDEDT;
            txtMsg.Text = "";

            if (KIND == "RID001") ShowDataRID001(p_data); // 퇴원요약자료
            if (KIND == "ERD001") ShowDataERD001(p_data); // 진단검사결과지
            if (KIND == "ERR001") ShowDataERR001(p_data); // 영상검사결과지
                                                          // 병리검사결과지
            if (KIND == "RSS001") ShowDataRSS001(p_data); // 수술기록자료
            if (KIND == "REE001") ShowDataREE001(p_data); // 응급기록자료
            if (KIND == "RII001") ShowDataRII001(p_data); // 입원초진기록자료
            if (KIND == "RIP001") ShowDataRIP001(p_data); // 입원경과기록자료
            if (KIND == "ROO001") ShowDataROO001(p_data); // 외래초진기록자료
            if (KIND == "ROP001") ShowDataROP001(p_data); // 외래경과기록자료
            if (KIND == "RWI001") ShowDataRWI001(p_data); // 중환자실기록자료
            if (KIND == "RCC001") ShowDataRCC001(p_data); // 협의진료기록자료
            if (KIND == "RAA001") ShowDataRAA001(p_data); // 마취기록자료
            if (KIND == "RAR001") ShowDataRAR001(p_data); // 회복기록자료
            if (KIND == "RNP001") ShowDataRNP001(p_data); // 간호정보조사자료
            if (KIND == "RNO001") ShowDataRNO001(p_data); // 기타간호기록자료
            if (KIND == "RNS001") ShowDataRNS001(p_data); // 수술간호기록자료
            if (KIND == "RNE001") ShowDataRNE001(p_data); // 응급간호기록자료
            if (KIND == "RDD001") ShowDataRDD001(p_data); // 의사지시기록자료
            if (KIND == "RWW001") ShowDataRWW001(p_data); // 임상관찰기록자료
            if (KIND == "RNH001") ShowDataRNH001(p_data); // 투석기록자료
            if (KIND == "RIY001") ShowDataRIY001(p_data); // 전입기록자료
            if (KIND == "RIZ001") ShowDataRIZ001(p_data); // 전출기록자료
            if (KIND == "RTT001") ShowDataRTT001(p_data); // 시술기록자료
            if (KIND == "RMM001") ShowDataRMM001(p_data); // 투약기록자료
            if (KIND == "RWN001") ShowDataRWN001(p_data); // 신생아중환자실기록자료
                                                          // 의원급진료기록자료
                                                          // 방사선치료기록자료

            // 화면에 보이는 컬럼 개수에 따른 처리
            int maxColumnCount = 0;
            List<CDataDisp> list = (List<CDataDisp>)grdMain.DataSource;
            foreach (CDataDisp d in list)
            {
                if (d.ColumnCount > maxColumnCount) maxColumnCount = d.ColumnCount;
            }
            // 변화가 없으면 종료
            if (maxColumnCount == m_VisibleColumnCount) return;
            m_VisibleColumnCount = maxColumnCount;
            // 일단 안보이게 해놓고
            grdMainView.Columns["VALUE13"].Visible = false;
            grdMainView.Columns["VALUE12"].Visible = false;
            grdMainView.Columns["VALUE11"].Visible = false;
            grdMainView.Columns["VALUE10"].Visible = false;
            grdMainView.Columns["VALUE9"].Visible = false;
            grdMainView.Columns["VALUE8"].Visible = false;
            grdMainView.Columns["VALUE7"].Visible = false;
            grdMainView.Columns["VALUE6"].Visible = false;
            grdMainView.Columns["VALUE5"].Visible = false;
            grdMainView.Columns["VALUE4"].Visible = false;
            grdMainView.Columns["VALUE3"].Visible = false;
            // 보이게 처리
            if (m_VisibleColumnCount > 3) grdMainView.Columns["VALUE3"].Visible = true;
            if (m_VisibleColumnCount > 4) grdMainView.Columns["VALUE4"].Visible = true;
            if (m_VisibleColumnCount > 5) grdMainView.Columns["VALUE5"].Visible = true;
            if (m_VisibleColumnCount > 6) grdMainView.Columns["VALUE6"].Visible = true;
            if (m_VisibleColumnCount > 7) grdMainView.Columns["VALUE7"].Visible = true;
            if (m_VisibleColumnCount > 8) grdMainView.Columns["VALUE8"].Visible = true;
            if (m_VisibleColumnCount > 9) grdMainView.Columns["VALUE9"].Visible = true;
            if (m_VisibleColumnCount > 10) grdMainView.Columns["VALUE10"].Visible = true;
            if (m_VisibleColumnCount > 11) grdMainView.Columns["VALUE11"].Visible = true;
            if (m_VisibleColumnCount > 12) grdMainView.Columns["VALUE12"].Visible = true;
            if (m_VisibleColumnCount > 13) grdMainView.Columns["VALUE13"].Visible = true;

        }

        private void ShowDataRAR001(CData p_data)
        {
            this.Text = "회복기록자료(RAR001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RAR001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("회복실 도착일시", p_data.RAR001_LIST[idx].PT_INDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("회복실 퇴실일시", p_data.RAR001_LIST[idx].PT_OUTDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실 결정의사 성명", p_data.RAR001_LIST[idx].ANDRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RAR001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RAR001_LIST[idx].WDTM, "").SetIsCaption("1"));

                // B. 세부 정보

                // 1. 활력징후
                list.Add(new CDataDisp("활력징후 측정일시", "혈압", "맥박", "호흡", "체온", "산소포화도", "특이사항").SetIsCaption("2"));

                if (p_data.RAR001_LIST[idx].VTSG_DT.Count < 1)
                {
                    list.Add(new CDataDisp("-", "-", "-", "-", "-", "-", "-"));
                }
                else
                {
                    for (int i = 0; i < p_data.RAR001_LIST[idx].VTSG_DT.Count; i++)
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].MASR_DT(i)
                                              , p_data.RAR001_LIST[idx].BP(i)
                                              , p_data.RAR001_LIST[idx].HR[i]
                                              , p_data.RAR001_LIST[idx].RR[i]
                                              , p_data.RAR001_LIST[idx].BT[i]
                                              , p_data.RAR001_LIST[idx].SPO2[i]
                                              , p_data.RAR001_LIST[idx].RMK[i]
                                              ));
                    }
                }

                // 2. 약제투여

                // 3. 통증

                // 1) 통증평가

                list.Add(new CDataDisp("통증평가 실시여부", p_data.RAR001_LIST[idx].PAINCASE_YN, "1.Yes 2.No").SetIsCaption("1"));

                if (p_data.RAR001_LIST[idx].PAINDT1 != "" || p_data.RAR001_LIST[idx].PAINDT2 != "")
                {
                    list.Add(new CDataDisp("통증평가 일시", "도구(1.NRS 2.VAS 3.FPRS 4.FLACC 9.기타)", "도구 상세", "결과").SetIsCaption("2"));

                    if (p_data.RAR001_LIST[idx].PAINDT1 != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].PAINDT1, p_data.RAR001_LIST[idx].PAINCASE_TOOL1, p_data.RAR001_LIST[idx].PAINCASE_TOOL_DETAIL1, p_data.RAR001_LIST[idx].PAINCASE_RESULT1));
                    }
                    if (p_data.RAR001_LIST[idx].PAINDT2 != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].PAINDT2, p_data.RAR001_LIST[idx].PAINCASE_TOOL2, p_data.RAR001_LIST[idx].PAINCASE_TOOL_DETAIL2, p_data.RAR001_LIST[idx].PAINCASE_RESULT2));
                    }
                }

                // 2) 오심구토평가
                list.Add(new CDataDisp("오심구토 평가 실시여부", p_data.RAR001_LIST[idx].VOM_YN, "1.Yes 2.No").SetIsCaption("1"));

                if (p_data.RAR001_LIST[idx].EMSSDT1 != "" || p_data.RAR001_LIST[idx].EMSSDT2 != "")
                {
                    list.Add(new CDataDisp("오심구토 평가 일시", "오심구토 평가 결과").SetIsCaption("2"));

                    if (p_data.RAR001_LIST[idx].EMSSDT1 != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].EMSSDT1, p_data.RAR001_LIST[idx].ASM_RST_TXT1));
                    }
                    if (p_data.RAR001_LIST[idx].EMSSDT2 != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].EMSSDT2, p_data.RAR001_LIST[idx].ASM_RST_TXT2));
                    }
                }

                list.Add(new CDataDisp("PCA", p_data.RAR001_LIST[idx].PCA_TXT, "").SetIsCaption("1"));

                // C. Post-Anesthetic Recovery Score

                if (p_data.RAR001_LIST[idx].PT_INDTM != "" || p_data.RAR001_LIST[idx].PT_OUTDTM != "")
                {
                    list.Add(new CDataDisp("마취회복점수 측정일시", "활동성(0~2)", "호흡(0~2)", "순환(0~2)", "의식(0~2)", "피부색(0~2)", "합계").SetIsCaption("2"));

                    if (p_data.RAR001_LIST[idx].PT_INDTM != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].PT_INDTM // 마취회복점수 측정일시
                                              , p_data.RAR001_LIST[idx].PARSCR1_1 // 마취회복점수 활동성
                                              , p_data.RAR001_LIST[idx].PARSCR1_2 // 마취회복점수 호흡
                                              , p_data.RAR001_LIST[idx].PARSCR1_3 // 마취회복점수 순환
                                              , p_data.RAR001_LIST[idx].PARSCR1_4 // 마취회복점수 의식
                                              , p_data.RAR001_LIST[idx].PARSCR1_5 // 마취회복점수 피부색
                                              , p_data.RAR001_LIST[idx].PARSCR1_SUM // 마취회복점수 합계
                                              ));
                    }
                    if (p_data.RAR001_LIST[idx].PT_OUTDTM != "")
                    {
                        list.Add(new CDataDisp(p_data.RAR001_LIST[idx].PT_OUTDTM
                                              , p_data.RAR001_LIST[idx].PARSCR2_1
                                              , p_data.RAR001_LIST[idx].PARSCR2_2
                                              , p_data.RAR001_LIST[idx].PARSCR2_3
                                              , p_data.RAR001_LIST[idx].PARSCR2_4
                                              , p_data.RAR001_LIST[idx].PARSCR2_5
                                              , p_data.RAR001_LIST[idx].PARSCR2_SUM
                                              ));
                    }
                }
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRII001(CData p_data)
        {
            this.Text = "입원초진기록자료(RII001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RII001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("입원일시", p_data.RII001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.RII001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.RII001_LIST[idx].DRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RII001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RII001_LIST[idx].SYSDTM, "").SetIsCaption("1"));

                list.Add(new CDataDisp("입원경로", p_data.RII001_LIST[idx].PATHCD, "1.외래 2.응급실 3.전원 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입원경로상세", p_data.RII001_LIST[idx].PATH_DETAIL, "입원경로가 [9.기타]인 경우 평문으로 기재").SetIsCaption("1"));

                list.Add(new CDataDisp("주호소", p_data.RII001_LIST[idx].CC, "").SetIsCaption("1"));
                list.Add(new CDataDisp("발병시기", p_data.RII001_LIST[idx].CC_DATE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("현병력", p_data.RII001_LIST[idx].PI, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물이상반응여부", p_data.RII001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물이상반응내용", p_data.RII001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("과거력", p_data.RII001_LIST[idx].PHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물복용여부", p_data.RII001_LIST[idx].MDS_DOS_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물종류", p_data.RII001_LIST[idx].MDS_KND, "1.고혈압 2.당뇨 3.항결핵 4. 고지혈증 5.항혈전 6.면역억제 7.항암 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("약물종류상세", p_data.RII001_LIST[idx].MDS_KND_ETC, "약물종류가 [9.기타]인 경우").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력여부", p_data.RII001_LIST[idx].FHX_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력내용", p_data.RII001_LIST[idx].FHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("계통문진", p_data.RII001_LIST[idx].ROS, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신체검진", p_data.RII001_LIST[idx].PE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("문제목록및평가", p_data.RII001_LIST[idx].PRBM_LIST, "").SetIsCaption("1"));

                list.Add(new CDataDisp("확진여부(1.확진 2.의증)", "초기진단 진단명").SetIsCaption("2"));
                for (int i = 0; i < p_data.RII001_LIST[idx].DXD.Count; i++)
                {
                    list.Add(new CDataDisp(p_data.RII001_LIST[idx].ROFG_12(i), p_data.RII001_LIST[idx].DXD[i]));
                }

                list.Add(new CDataDisp("치료계획", p_data.RII001_LIST[idx].TXPLAN, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRIY001(CData p_data)
        {
            this.Text = "전입기록자료(RIY001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RIY001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("입원일시", p_data.RIY001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원과", p_data.RIY001_LIST[idx].INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("내과 세부전문과목", p_data.RIY001_LIST[idx].INSDPTCD2, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전과일", p_data.RIY001_LIST[idx].TDATE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전입과", p_data.RIY001_LIST[idx].GR_INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전출과", p_data.RIY001_LIST[idx].HM_INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전출과 내과 세부진료과목", p_data.RIY001_LIST[idx].HM_INSDPTCD2, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.RIY001_LIST[idx].TDRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RIY001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RIY001_LIST[idx].SYSDTM, "").SetIsCaption("1"));

                list.Add(new CDataDisp("현병력", p_data.RIY001_LIST[idx].NOWT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신체검진", p_data.RIY001_LIST[idx].PE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("문제목록", p_data.RIY001_LIST[idx].PROBLEMS, "").SetIsCaption("1"));

                list.Add(new CDataDisp("진단명", p_data.RIY001_LIST[idx].DX_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("치료계획", p_data.RIY001_LIST[idx].APLAN, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRIZ001(CData p_data)
        {
            this.Text = "전출기록자료(RIZ001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RIZ001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("입원일시", p_data.RIZ001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원과", p_data.RIZ001_LIST[idx].INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("내과 세부전문과목", p_data.RIZ001_LIST[idx].INSDPTCD2, "").SetIsCaption("1"));

                list.Add(new CDataDisp("전과일", p_data.RIZ001_LIST[idx].TDATE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전출과", p_data.RIZ001_LIST[idx].HM_INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전출과 내과 세부진료과목", p_data.RIZ001_LIST[idx].HM_INSDPTCD2, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전입과", p_data.RIZ001_LIST[idx].GR_INSDPTCD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전입과 내과 세부진료과목", p_data.RIZ001_LIST[idx].GR_INSDPTCD2, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.RIZ001_LIST[idx].TDRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RIZ001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RIZ001_LIST[idx].SYSDTM, "").SetIsCaption("1"));

                list.Add(new CDataDisp("전과사유", p_data.RIZ001_LIST[idx].CHKTREA, "").SetIsCaption("1"));
                list.Add(new CDataDisp("치료경과 및 환자상태", p_data.RIZ001_LIST[idx].STATUS, "").SetIsCaption("1"));
                list.Add(new CDataDisp("주요검사결과", p_data.RIZ001_LIST[idx].TXTRESULT, "").SetIsCaption("1"));

                list.Add(new CDataDisp("처치및수술", p_data.RIZ001_LIST[idx].OP_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진단명", p_data.RIZ001_LIST[idx].DX_INFO, "").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRNP001(CData p_data)
        {
            this.Text = "간호정보조사자료(RNP001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RNP001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("입원일시", p_data.RNP001_LIST[idx].IPAT_DT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.RNP001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RNP001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RNP001_LIST[idx].WRT_DT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원경로", p_data.RNP001_LIST[idx].VST_PTH_CD, "1.외래 2.응급실 3.전원 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입원경로 기타 상세", p_data.RNP001_LIST[idx].VST_PTH_ETC_TXT, "입원경로가 [9.기타]인 경우 상세내용").SetIsCaption("1"));
                list.Add(new CDataDisp("환자구분", p_data.RNP001_LIST[idx].PTNT_TP_CD_12, "1.일반 2.신생아(28일이내)").SetIsCaption("1"));

                list.Add(new CDataDisp("정보제공자 성명", p_data.RNP001_LIST[idx].INFM_OFFRR_NM, "").SetIsCaption("1"));

                list.Add(new CDataDisp("입원동기", p_data.RNP001_LIST[idx].CC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("과거력", p_data.RNP001_LIST[idx].ANMN_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술력", p_data.RNP001_LIST[idx].SOPR_HIST_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("최근투약상태", p_data.RNP001_LIST[idx].MDCT_STAT_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("알레르기 여부", p_data.RNP001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("알레르기 내용", p_data.RNP001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력 내용", p_data.RNP001_LIST[idx].FMHS_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("음주 여부", p_data.RNP001_LIST[idx].DRNK_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("음주 내용", p_data.RNP001_LIST[idx].DRNK_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("흡연 여부", p_data.RNP001_LIST[idx].SMKN_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("흡연 내용", p_data.RNP001_LIST[idx].SMKN_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신장(cm)", p_data.RNP001_LIST[idx].HEIG, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원시 체중(kg)", p_data.RNP001_LIST[idx].BWGT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신체검전", p_data.RNP001_LIST[idx].PHBD_MEDEXM_TXT, "").SetIsCaption("1"));

                list.Add(new CDataDisp("신생아 출생일시", p_data.RNP001_LIST[idx].BIRTH_DTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신생아 재태기간", p_data.RNP001_LIST[idx].FTUS_DEV_TRM, "주/일").SetIsCaption("1"));
                list.Add(new CDataDisp("신생아 분만형태", p_data.RNP001_LIST[idx].PARTU_FRM_TXT_HIRA, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신생아 Apgar Score", p_data.RNP001_LIST[idx].APSC_PNT, "1분점수/5분점수").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataROO001(CData p_data)
        {
            this.Text = "외래초진기록자료(ROO001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.ROO001_LIST.Count; idx++)
            {
                list.Add(new CDataDisp("진료일시", p_data.ROO001_LIST[idx].EXDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.ROO001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.ROO001_LIST[idx].DRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.ROO001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.ROO001_LIST[idx].SYSDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("주호소", p_data.ROO001_LIST[idx].CC, "").SetIsCaption("1"));

                list.Add(new CDataDisp("현병력", p_data.ROO001_LIST[idx].PI, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 이상반응 여부", p_data.ROO001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 이상반응 내용", p_data.ROO001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("과거력", p_data.ROO001_LIST[idx].PHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물복용 여부", p_data.ROO001_LIST[idx].MDS_DOS_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물종류", p_data.ROO001_LIST[idx].MDS_KND, "1.고혈압 2.당뇨 3.항결핵 4.고지혈증 5.항혈전 6.면역억제 7.항암 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("약물종류 기타상세", "", "약물 종류가 [9.기타]인 경우").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력 여부", p_data.ROO001_LIST[idx].FHX_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력 내용", p_data.ROO001_LIST[idx].FHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("계통문진", p_data.ROO001_LIST[idx].ROS, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신체검진", p_data.ROO001_LIST[idx].PE, "").SetIsCaption("1"));
                list.Add(new CDataDisp("문제목록 및 평가", p_data.ROO001_LIST[idx].PRBM_LIST, "").SetIsCaption("1"));

                for (int i = 0; i < p_data.ROO001_LIST[idx].PTYSQ.Count; i++)
                {
                    list.Add(new CDataDisp("초기진단 진단명" + (i + 1), p_data.ROO001_LIST[idx].ROFG_12(i) + "." + p_data.ROO001_LIST[idx].DXD[i], "1.확진 2.의증").SetIsCaption("1"));
                }

                list.Add(new CDataDisp("치료계획", p_data.ROO001_LIST[idx].TXPLAN, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataROP001(CData p_data)
        {
            this.Text = "외래경과기록자료(ROP001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.ROP001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("진료일시", p_data.ROP001_LIST[idx].EXDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.ROP001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.ROP001_LIST[idx].DRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.ROP001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.ROP001_LIST[idx].SYSDTM, "").SetIsCaption("1"));

                // B. 외래경과
                list.Add(new CDataDisp("주관적정보&객관적정보&평가", p_data.ROP001_LIST[idx].PN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("치료계획", p_data.ROP001_LIST[idx].TXPLAN, "").SetIsCaption("1"));

                // 3. 진단
                //for (int i = 0; i < p_data.ROP001_LIST[idx].DACD_LIST.Count; i++)
                //{
                //    list.Add(new CDataDisp("(상병분류구분)진단명" + (i + 1), "(" + p_data.ROP001_LIST[idx].DACD_LIST[i].TYPE_123 + ")" + p_data.ROP001_LIST[idx].DACD_LIST[i].DXD, "1.주상병 2.부상벙 3.배재상병"));
                //}

                // 2. 치치및 수술
                //for (int i = 0; i < p_data.ROP001_LIST[idx].TREAT_LIST.Count; i++)
                //{
                //    list.Add(new CDataDisp("시행일시 처치및수술명" + (i + 1), p_data.ROP001_LIST[idx].TREAT_LIST[i].ODTM + p_data.ROP001_LIST[idx].TREAT_LIST[i].ONM, ""));
                //}

                list.Add(new CDataDisp("약물 이상반응 여부", p_data.ROP001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 이상반응 내용", p_data.ROP001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRSS001(CData p_data)
        {
            this.Text = "수술기록자료(RSS001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RSS001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("수술시작일시", p_data.RSS001_LIST[idx].OPSDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술종료일시", p_data.RSS001_LIST[idx].OPEDTM, "").SetIsCaption("1"));

                // 수술의
                list.Add(new CDataDisp("수술의사 구분(1.집도의 2.보조의)", "진료과", "성명").SetIsCaption("2"));
                list.Add(new CDataDisp(p_data.RSS001_LIST[idx].DR_GUBUN // 수술의사 구분
                                      , p_data.RSS001_LIST[idx].DEPT_INFO // 진료과
                                      , p_data.RSS001_LIST[idx].DRNM // 성명
                                      ));
                // 보조의1
                if (p_data.RSS001_LIST[idx].DR_GUBUN_SUB1 != "")
                {
                    list.Add(new CDataDisp(p_data.RSS001_LIST[idx].DR_GUBUN_SUB1 // 수술의사 구분
                                          , p_data.RSS001_LIST[idx].DEPT_INFO_SUB1 // 진료과
                                          , p_data.RSS001_LIST[idx].DRNM_SUB1 // 성명
                                          ));
                }
                // 보조의2
                if (p_data.RSS001_LIST[idx].DR_GUBUN_SUB2 != "")
                {
                    list.Add(new CDataDisp(p_data.RSS001_LIST[idx].DR_GUBUN_SUB2 // 수술의사 구분
                                          , p_data.RSS001_LIST[idx].DEPT_INFO_SUB2 // 진료과
                                          , p_data.RSS001_LIST[idx].DRNM_SUB2 // 성명
                                          ));
                }
                // 보조의3
                if (p_data.RSS001_LIST[idx].DR_GUBUN_SUB3 != "")
                {
                    list.Add(new CDataDisp(p_data.RSS001_LIST[idx].DR_GUBUN_SUB3 // 수술의사 구분
                                          , p_data.RSS001_LIST[idx].DEPT_INFO_SUB3 // 진료과
                                          , p_data.RSS001_LIST[idx].DRNM_SUB3 // 성명
                                          ));
                }

                list.Add(new CDataDisp("작성자 성명", p_data.RSS001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RSS001_LIST[idx].WRTDTM, "").SetIsCaption("1"));

                // B.수술정보
                list.Add(new CDataDisp("응급여부", p_data.RSS001_LIST[idx].STAFG_YN, "1.정규 2.응급").SetIsCaption("1"));
                list.Add(new CDataDisp("마취종류", p_data.RSS001_LIST[idx].ANETPNM, "").SetIsCaption("1"));

                // 수술전진단
                list.Add(new CDataDisp("수술전 진단", p_data.RSS001_LIST[idx].PREDX, "").SetIsCaption("1"));

                // 수술후진단
                list.Add(new CDataDisp("수술후 진단", p_data.RSS001_LIST[idx].POSDX, "").SetIsCaption("1"));

                // 수술명(수가코드+수술명)
                for (int i = 0; i < p_data.RSS001_LIST[idx].ONM.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("수술명", "수가코드").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RSS001_LIST[idx].ONM[i], p_data.RSS001_LIST[idx].ISPCD[i]));
                }

                list.Add(new CDataDisp("수술체위", p_data.RSS001_LIST[idx].POS, "").SetIsCaption("1"));
                list.Add(new CDataDisp("병변의 위치", p_data.RSS001_LIST[idx].LESION, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술소견", p_data.RSS001_LIST[idx].INDIOFSURGERY, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술절차", p_data.RSS001_LIST[idx].SURFNDNPRO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("중요(특이)사항", p_data.RSS001_LIST[idx].REMARK, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataREE001(CData p_data)
        {
            this.Text = "응급기록자료(REE001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.REE001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("응급실 도착일시", p_data.REE001_LIST[idx].PTMIINDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("응급실 퇴실일시", p_data.REE001_LIST[idx].PTMIOTDTM, "").SetIsCaption("1"));

                // KTAS 중증도
                list.Add(new CDataDisp("KTAS 중증도 분류일시", p_data.REE001_LIST[idx].PTMIKTDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("KTAS 중증도 등급", p_data.REE001_LIST[idx].PTMIKTS1, "1,2,3,4,5,8중 하나").SetIsCaption("1"));

                list.Add(new CDataDisp("전원 여부", p_data.REE001_LIST[idx].DHI_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("타병원 진료 내용", "-", "내용이 없는 경우 '-' 입력").SetIsCaption("1"));

                // B.기초 사정 정보

                // C.응급실 경과

                // 주호소
                list.Add(new CDataDisp("주호소", p_data.REE001_LIST[idx].MJ_HOSO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("발병시기", p_data.REE001_LIST[idx].ONSET, "").SetIsCaption("1"));

                list.Add(new CDataDisp("현병력", p_data.REE001_LIST[idx].PI, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 이상반응 여부", p_data.REE001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 이상반응 내용", p_data.REE001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("과거력", p_data.REE001_LIST[idx].PHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 복용여부", p_data.REE001_LIST[idx].MDS_DOS_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 종류", p_data.REE001_LIST[idx].MDS_KND, "1.고혈압 2.당뇨 3.항결핵 4.고지혈증 5.항혈전 6.면역억제 7.항암 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("약물 종류 상세", p_data.REE001_LIST[idx].MDS_KND_ETC, "약물 종류가 [9.기타]인 경우").SetIsCaption("1"));
                list.Add(new CDataDisp("음주 여부", p_data.REE001_LIST[idx].DRNK_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("음주 내용", p_data.REE001_LIST[idx].DRNK_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("흡연 여부", p_data.REE001_LIST[idx].SMKN_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("흡연 내용", p_data.REE001_LIST[idx].SMKN_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력 여부", p_data.REE001_LIST[idx].FHX_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("가족력 내용", p_data.REE001_LIST[idx].FHX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("계통 문진", p_data.REE001_LIST[idx].ROS, "").SetIsCaption("1"));
                list.Add(new CDataDisp("신체 검진", p_data.REE001_LIST[idx].PE, "").SetIsCaption("1"));

                // 진료내역
                list.Add(new CDataDisp("진료일시", "진료과", "진료의 성명", "작성자 성명", "작성일시", "문제목록 & 평가", "치료계획").SetIsCaption("2"));

                if (p_data.REE001_LIST[idx].EXDT.Count < 1)
                {
                    list.Add(new CDataDisp("-", "-","-", "-", "-", "-", "-"));
                }
                else
                {
                    for (int i = 0; i < p_data.REE001_LIST[idx].EXDT.Count; i++)
                    {
                        list.Add(new CDataDisp(p_data.REE001_LIST[idx].EXDTM(i)
                                              , p_data.REE001_LIST[idx].DEPT_INFO(i)
                                              , p_data.REE001_LIST[idx].USERNM[i]
                                              , p_data.REE001_LIST[idx].USERNM[i]
                                              , p_data.REE001_LIST[idx].WRTDTM(i)
                                              , p_data.REE001_LIST[idx].PRBM_TXT(i)
                                              , p_data.REE001_LIST[idx].PLAN_TXT(i)
                                              ));
                    }
                }

                // 진단
                for (int i = 0; i < p_data.REE001_LIST[idx].DXD.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("확진여부(1.확진 2.의증)", "진단명").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.REE001_LIST[idx].ROFG_12(i), p_data.REE001_LIST[idx].DXD[i]));
                }

                // 시술처치 및 수술
                for (int i = 0; i < p_data.REE001_LIST[idx].ONM.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("시행일시", "처치및수술명").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.REE001_LIST[idx].ODTM(i), p_data.REE001_LIST[idx].ONM[i]));
                }

                list.Add(new CDataDisp("퇴실형태", p_data.REE001_LIST[idx].PTMIEMRT_NM, "01.정상퇴원 02.자의퇴원 03.입원(본원) 04.타병원전원 05.외래추적관리 06.사망 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실형태상세", "", "퇴실형태가 [03]인 경우 진료과목 평문, [04]인 경우 병원명 진료과목 평문, [99]인 경우 평문").SetIsCaption("1"));
                list.Add(new CDataDisp("사망일시", p_data.REE001_LIST[idx].DEATHDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("원사인 상병분류기호", p_data.REE001_LIST[idx].DEATH_SICK_SYM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망 진단명", p_data.REE001_LIST[idx].DEATH_DIAG_NM, "").SetIsCaption("1"));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRNS001(CData p_data)
        {
            this.Text = "수술간호기록자료(RNS001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RNS001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("수술실 입실일시", p_data.RNS001_LIST[idx].OR_INDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술실 퇴실일시", p_data.RNS001_LIST[idx].PT_OUTDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술 시작일시", p_data.RNS001_LIST[idx].OP_STDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술 종료일시", p_data.RNS001_LIST[idx].OP_ENDDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("소독 간호사 성명", p_data.RNS001_LIST[idx].SRNURS1, "").SetIsCaption("1"));
                list.Add(new CDataDisp("순회 간호사 성명", p_data.RNS001_LIST[idx].CIRNURS1, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RNS001_LIST[idx].SYSDTM, "").SetIsCaption("1"));


                // B.수술전확인
                list.Add(new CDataDisp("Time Out", p_data.RNS001_LIST[idx].TIMEOUTCHK, "1.Yes 2.No").SetIsCaption("1"));

                // C.부위 정보
                list.Add(new CDataDisp("수술 전 진단", p_data.RNS001_LIST[idx].PREDXNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술 후 진단", p_data.RNS001_LIST[idx].POSTDXNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술명", p_data.RNS001_LIST[idx].POSTOPNM, "").SetIsCaption("1"));

                // D.수술 정보

                // 1.삽입관
                for (int i = 0; i < p_data.RNS001_LIST[idx].TUBE_1.Count; i++)
                {
                    list.Add(new CDataDisp("삽입관 종류" + (i + 1), p_data.RNS001_LIST[idx].TUBE_1[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("참고사항" + (i + 1), p_data.RNS001_LIST[idx].TUBE_RMK(i), "").SetIsCaption("1"));
                }

                // 2.치료재료 및 고가 소모품

                // 3.수술 중 사용 약품
                for (int i = 0; i < p_data.RNS001_LIST[idx].ONM.Count; i++)
                {
                    list.Add(new CDataDisp("약품명" + (i + 1), p_data.RNS001_LIST[idx].ONM[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("투여용량" + (i + 1), p_data.RNS001_LIST[idx].QTY[i], "").SetIsCaption("1"));
                }

                // 4.검체
                for (int i = 0; i < p_data.RNS001_LIST[idx].GUM_1.Count; i++)
                {
                    list.Add(new CDataDisp("검체종류" + (i + 1), p_data.RNS001_LIST[idx].GUM_1[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("부위" + (i + 1), p_data.RNS001_LIST[idx].GUM_2[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("개수" + (i + 1), p_data.RNS001_LIST[idx].GUM_3[i], "").SetIsCaption("1"));
                }
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRWW001(CData p_data)
        {
            this.Text = "임상관찰기록자료(RWW001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RWW001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("진료일시", p_data.RWW001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.RWW001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));


                // B.임상관찰기록

                // 1.활력징후
                for (int i = 0; i < p_data.RWW001_LIST[idx].CHKDT.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("측정일시", "혈압", "맥박", "호흠", "체온").SetIsCaption("2"));
                    }

                    list.Add(new CDataDisp(p_data.RWW001_LIST[idx].CHKDTM(i), p_data.RWW001_LIST[idx].BP[i], p_data.RWW001_LIST[idx].PR[i], p_data.RWW001_LIST[idx].RR[i], p_data.RWW001_LIST[idx].TMP[i]));
                }

                // 3.섭취량 & 배설량
                for (int i = 0; i < p_data.RWW001_LIST[idx].IO_CHKDT.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("측정시작일시", "섭취량 총량", "섭취량 기타", "배설량 총량", "배설량 배뇨", "배설량 기타").SetIsCaption("2"));
                    }

                    list.Add(new CDataDisp(p_data.RWW001_LIST[idx].IO_CHKDTM(i), p_data.RWW001_LIST[idx].INTAKE_TOT(i).ToString(), p_data.RWW001_LIST[idx].INTAKE_ETC(i).ToString(), p_data.RWW001_LIST[idx].OUTPUT_TOT(i).ToString(), p_data.RWW001_LIST[idx].OUTPUT_URN(i).ToString(), p_data.RWW001_LIST[idx].OUTPUT_ETC(i).ToString()));
                }

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRWI001(CData p_data)
        {
            this.Text = "중환자실기록자료(RWI001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RWI001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("최초입실일시", p_data.RWI001_LIST[idx].FST_IPAT_DT, "").SetIsCaption("1"));
                for (int i = 0; i < p_data.RWI001_LIST[idx].DXD.Count; i++)
                {
                    list.Add(new CDataDisp("진단명" + (i + 1), p_data.RWI001_LIST[idx].DXD[i], "").SetIsCaption("1"));
                }


                // B.중환자실 입퇴실정보
                list.Add(new CDataDisp("담당의사 성명", p_data.RWI001_LIST[idx].CHRG_DR_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진료과", p_data.RWI001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RWI001_LIST[idx].WRTP_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실일시", p_data.RWI001_LIST[idx].SPRM_IPAT_DT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실경로", p_data.RWI001_LIST[idx].SPRM_IPAT_PTH_CD, "1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입실경로 기타 상세", p_data.RWI001_LIST[idx].IPAT_PTH_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유", p_data.RWI001_LIST[idx].SPRM_IPAT_RS_CD, "1.상태 악화되어 집중관찰 2.특수한 치료 또는 관리가 필요한 이유 3.수술이나 시술 후 집중관찰 5.의료진의 치료 계획에 따라 예정되 재입실 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유 재입실사유", p_data.RWI001_LIST[idx].RE_IPAT_RS_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유 기타상세", p_data.RWI001_LIST[idx].IPAT_RS_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실상태", p_data.RWI001_LIST[idx].SPRM_DSCG_RST_CD, "01.퇴원 02.전실(병동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.퇴사판정(이식) 08.계속 입원 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실현황 기타 상세", p_data.RWI001_LIST[idx].DSCG_RST_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망 일시", p_data.RWI001_LIST[idx].DEATH_DT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("원사인 상병분류기호", p_data.RWI001_LIST[idx].DEATH_SICK_SYM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망 진단명", p_data.RWI001_LIST[idx].DEATH_DIAG_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실일시", p_data.RWI001_LIST[idx].SPRM_DSCG_DT, "").SetIsCaption("1"));

                // D.기타정보
                list.Add(new CDataDisp("인공호흡기 적용 여부", p_data.RWI001_LIST[idx].ATFL_RPRT_ENFC_YN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("산소요법 적용 여부", p_data.RWI001_LIST[idx].OXY_CURE_YN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("삽입관 적용 여부", p_data.RWI001_LIST[idx].CNNL_ENFC_YN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("배액관 적용 여부", p_data.RWI001_LIST[idx].DRN_ENFC_YN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("특수 처치", p_data.RWI001_LIST[idx].SPCL_TRET_CD, "00.해당 없음 01.ECMO 02.신대체요법 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("특수 처치 기타 상세", p_data.RWI001_LIST[idx].SPCL_TRET_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("모니터링 종류", p_data.RWI001_LIST[idx].MNTR_KND_CD, "00.해당없음 01.ABP Monitor 02.EKG Monitor 03.O2 Saturation Monitor 04.Continuous EEG 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("중증도 점수 여부", p_data.RWI001_LIST[idx].SGRD_PNT_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("중증도 종류", p_data.RWI001_LIST[idx].SGRD_RVSN_TL_CD, "1.APACH II 2.PAPCHEIII 3.SAPS 2 3.SAPS 3 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("증증도 보정도구 종류상세", p_data.RWI001_LIST[idx].SGRD_RVSN_TL_TXT, "").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRNO001(CData p_data)
        {
            this.Text = "기타간호기록자료(RNO001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RNO001_LIST.Count; idx++)
            {
                // A. 기본정보
                if (idx == 0)
                {
                    list.Add(new CDataDisp("진료과", p_data.RNO001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                    list.Add(new CDataDisp("기록일시", "간호사 성명", "간호기록").SetIsCaption("2"));
                }

                // B.간호기록
                list.Add(new CDataDisp(p_data.RNO001_LIST[idx].WDTM, p_data.RNO001_LIST[idx].PNURSE_NM, p_data.RNO001_LIST[idx].RESULT));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRIP001(CData p_data)
        {
            this.Text = "입원경과기록자료(RIP001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RIP001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("최초 입원일시", p_data.RIP001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));


                // B.진단 및 처치등

                // 1.진단
                for (int i = 0; i < p_data.RIP001_LIST[idx].DXD.Count; i++)
                {
                    if (i == 0)
                        list.Add(new CDataDisp("상병분류구분", "진단명").SetIsCaption("2"));

                    list.Add(new CDataDisp(p_data.RIP001_LIST[idx].TYPE_123(i), p_data.RIP001_LIST[idx].DXD[i], "1.주상병 2.부상병 3.배제 상병"));
                }

                // 2.처치 및 수술
                for (int i = 0; i < p_data.RIP001_LIST[idx].ODT.Count; i++)
                {
                    if (i == 0)
                        list.Add(new CDataDisp("시행일시", "치치 및 수술명").SetIsCaption("2"));

                    list.Add(new CDataDisp(p_data.RIP001_LIST[idx].ODTM(i), p_data.RIP001_LIST[idx].ONM[i]));
                }

                // 3.약물이상반응
                list.Add(new CDataDisp("약물이상반응여부", p_data.RIP001_LIST[idx].ALRG_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물이상반응내용", p_data.RIP001_LIST[idx].ALRG_TXT, "").SetIsCaption("1"));

                // C.입원 경과
                for (int i = 0; i < p_data.RIP001_LIST[idx].EXDT.Count; i++)
                {
                    if (i == 0)
                        list.Add(new CDataDisp("진료일시", "진료과", "담당의사성명", "작성자성명", "작성일시", "주관적정보 & 객관적정보 & 평가", "치료계획").SetIsCaption("2"));

                    list.Add(new CDataDisp(p_data.RIP001_LIST[idx].EXDT[i] // 진료일시
                                          , p_data.RIP001_LIST[idx].DEPT_INFO(i) // 진료과
                                          , p_data.RIP001_LIST[idx].DRNM // 담당의사성명
                                          , p_data.RIP001_LIST[idx].USERNM[i] // 작성자성명
                                          , p_data.RIP001_LIST[idx].WRTDTM(i) // 작성일시
                                          , p_data.RIP001_LIST[idx].PRBM_TXT(i) // 주관적정보 & 객관적정보 & 평가
                                          , p_data.RIP001_LIST[idx].PLAN_TXT(i) // 치료계획
                                          ));
                }

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRCC001(CData p_data)
        {
            this.Text = "협의진료기록자료(RCC001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RCC001_LIST.Count; idx++)
            {
                if (idx > 0)
                {
                    // 두 번째 자료와 첫 번째 자료사이에 구분선을 만들기 위해...
                    list.Add(new CDataDisp("").SetIsCaption("2"));
                }

                // A. 기본정보
                list.Add(new CDataDisp("의뢰일시", p_data.RCC001_LIST[idx].ODTM, "").SetIsCaption("1"));


                // 2.진단
                for (int i = 0; i < p_data.RCC001_LIST[idx].DXD.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("상병분류구분(1.주상병 2.부상병 3.배제 상병)", "진단명", "상병분류기호").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RCC001_LIST[idx].TYPE_123(i), p_data.RCC001_LIST[idx].DXD[i], p_data.RCC001_LIST[idx].DACD[i]));
                }

                // 3.의뢰내용
                list.Add(new CDataDisp("의뢰내용", p_data.RCC001_LIST[idx].FLDCD9, "").SetIsCaption("1"));

                // 4.의뢰과
                list.Add(new CDataDisp("의뢰과", p_data.RCC001_LIST[idx].EX_DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("의뢰의사 성명", p_data.RCC001_LIST[idx].EXDRNM, "").SetIsCaption("1"));

                // B.회신
                list.Add(new CDataDisp("회신일시", p_data.RCC001_LIST[idx].REPLYDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("회신내용", p_data.RCC001_LIST[idx].REPLY, "").SetIsCaption("1"));
                list.Add(new CDataDisp("회신과", p_data.RCC001_LIST[idx].CST_DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("회신의사성명", p_data.RCC001_LIST[idx].CSTDRNM, "").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataERR001(CData p_data)
        {
            this.Text = "영상검사결과지(ERR001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.ERR001_LIST.Count; idx++)
            {
                // A. 기본정보
                if (idx == 0)
                    list.Add(new CDataDisp("진료과", "처방의사 성명", "처방일시", "검사일시", "판독일시", "판독의사 성명", "수가코드(EDI코드)", "검사코드", "검사명", "판독결과").SetIsCaption("2"));

                list.Add(new CDataDisp(p_data.ERR001_LIST[idx].DEPT_INFO // 진료과 
                                      , p_data.ERR001_LIST[idx].DRNM // 처방의사 성명
                                      , p_data.ERR001_LIST[idx].ODTM //처방일시, 
                                      , p_data.ERR001_LIST[idx].PHTDTM // 검사일시 
                                      , p_data.ERR001_LIST[idx].RPTDTM // 판독일시 
                                      , p_data.ERR001_LIST[idx].RDRNM // 판독의사 성명
                                      , p_data.ERR001_LIST[idx].EDICD // 수가코드(EDI코드) 
                                      , p_data.ERR001_LIST[idx].OCD // 검사코드 
                                      , p_data.ERR001_LIST[idx].ONM // 검사명 
                                      , p_data.ERR001_LIST[idx].RPTXT // 판독결과 
                                      ));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRDD001(CData p_data)
        {
            this.Text = "의사지시기록자료(RDD001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RDD001_LIST.Count; idx++)
            {
                // A. 기본정보
                if (idx == 0)
                {
                    if (p_data.IOFG == "2")
                    {
                        list.Add(new CDataDisp("진료형태", "1", "1.입원 2.외래").SetIsCaption("1"));
                        list.Add(new CDataDisp("입원일시", p_data.RDD001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                    }
                    else
                    {
                        list.Add(new CDataDisp("진료형태", "2", "1.입원 2.외래").SetIsCaption("1"));
                        list.Add(new CDataDisp("진료일시", p_data.RDD001_LIST[idx].EXDTM, "").SetIsCaption("1"));
                    }
                }

                // B.의사지시기록
                if (idx == 0)
                {
                    list.Add(new CDataDisp("처방일시", "처방내역", "비고", "진료과", "처방의사 성명").SetIsCaption("2"));
                }
                list.Add(new CDataDisp(p_data.RDD001_LIST[idx].ODTM
                                      ,p_data.RDD001_LIST[idx].ORDER_INFO
                                      ,p_data.RDD001_LIST[idx].RMK_INFO
                                      ,p_data.RDD001_LIST[idx].DEPT_INFO
                                      ,p_data.RDD001_LIST[idx].EXDRNM
                ));
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRNE001(CData p_data)
        {
            this.Text = "응급간호기록자료(RNE001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RNE001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("응급실 도착일시", p_data.RNE001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("응급실 퇴실일시", p_data.RNE001_LIST[idx].BEDODTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("내원경로", p_data.RNE001_LIST[idx].VST_PTH_CD, "1.직접 내원 2.전원 3.외래에서 의뢰 9.기타").SetIsCaption("1"));


                // B.응급 간호기록

                // 2.활력징후
                for (int i = 0; i < p_data.RNE001_LIST[idx].CHKDT.Count; i++)
                {
                    list.Add(new CDataDisp("활력징후 측정일시" + (i + 1), p_data.RNE001_LIST[idx].CHKDTM(i), "").SetIsCaption("1"));
                    list.Add(new CDataDisp("혈압" + (i + 1), p_data.RNE001_LIST[idx].BP[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("맥박" + (i + 1), p_data.RNE001_LIST[idx].PR[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("호흡" + (i + 1), p_data.RNE001_LIST[idx].RR[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("체온" + (i + 1), p_data.RNE001_LIST[idx].TMP[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("산소포화도" + (i + 1), p_data.RNE001_LIST[idx].SPO2[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("특이사항" + (i + 1), p_data.RNE001_LIST[idx].RMK[i], "").SetIsCaption("1"));
                }

                // 3.처치 및 간호내용
                for (int i = 0; i < p_data.RNE001_LIST[idx].WDATE.Count; i++)
                {
                    list.Add(new CDataDisp("기록일시" + (i + 1), p_data.RNE001_LIST[idx].WDTM(i), "").SetIsCaption("1"));
                    list.Add(new CDataDisp("간호기록" + (i + 1), p_data.RNE001_LIST[idx].RESULT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("간호사 성명" + (i + 1), p_data.RNE001_LIST[idx].PNURES_NM[i], "").SetIsCaption("1"));
                }

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRAA001(CData p_data)
        {
            this.Text = "마취기록자료(RAA001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RAA001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("마취 시작일시", p_data.RAA001_LIST[idx].ANSDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("마취 종료일시", p_data.RAA001_LIST[idx].ANEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술 시작일시", p_data.RAA001_LIST[idx].OPSDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("수술 종료일시", p_data.RAA001_LIST[idx].OPEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("마취의 성명", p_data.RAA001_LIST[idx].ANEDRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RAA001_LIST[idx].USRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일자", p_data.RAA001_LIST[idx].ENTDTM, "").SetIsCaption("1"));


                // B.마취전정보

                // 1.수술
                for (int i = 0; i < p_data.RAA001_LIST[idx].OPNM.Count; i++)
                {
                    list.Add(new CDataDisp("마취 전 수술명" + (i + 1), p_data.RAA001_LIST[idx].ISPCD[i] + " " + p_data.RAA001_LIST[idx].OPNM[i], "").SetIsCaption("1"));
                }

                // 2.진단
                for (int i = 0; i < p_data.RAA001_LIST[idx].DXD.Count; i++)
                {
                    list.Add(new CDataDisp("마취 전 진단명" + (i + 1), p_data.RAA001_LIST[idx].DXD[i], "").SetIsCaption("1"));
                }

                list.Add(new CDataDisp("마취형태", p_data.RAA001_LIST[idx].NCT_FRM_CD, "").SetIsCaption("1"));
                list.Add(new CDataDisp("ASA 점수", p_data.RAA001_LIST[idx].ASA_PNT, "").SetIsCaption("1"));

                // C.마취 중 정보
                list.Add(new CDataDisp("마취방법", p_data.RAA001_LIST[idx].NCT_MTH_CD, "01.정맥내 전신마취 02.정맥마취 국소마취 03.정맥마취 감시하전신마취 04.기관내 삽관에 의한 폐쇄순환식 전신마취 05.마스크에 의한 폐쇄식 전신마취 06.척수마취 07.경막외마취 08.상박신경총마취 09.척추경막욈취 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("마취방법상세", p_data.RAA001_LIST[idx].NCT_MTH_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("마취 중 감시여부", p_data.RAA001_LIST[idx].NCT_MIDD_MNTR_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("마취 중 감시여부 종류", p_data.RAA001_LIST[idx].NCT_MNTR_KND_CD, "01.중심정맥압감시 02.짐습적동맥압감시 03.말초산소포화도감시 04.파형면이지수감시 05.순환기능감시 06.신경생리추적감시 07.신경근감시 08.대뇌산소포화도감시 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("마취 중 감시여부 종류상세", p_data.RAA001_LIST[idx].NCT_MTH_ETC_TXT, "").SetIsCaption("1"));

                // 3.확력징후
                for (int i = 0; i < p_data.RAA001_LIST[idx].VTSG_MASR_DT.Count; i++)
                {
                    list.Add(new CDataDisp("활력징후 측정일시" + (i + 1), p_data.RAA001_LIST[idx].VTSG_MASR_DT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("혈압" + (i + 1), p_data.RAA001_LIST[idx].BPRSU[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("맥박" + (i + 1), p_data.RAA001_LIST[idx].PULS[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("호흡" + (i + 1), p_data.RAA001_LIST[idx].BRT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("체온" + (i + 1), p_data.RAA001_LIST[idx].TMPR[i], "").SetIsCaption("1"));
                }

                // 4.마취 중 감시측정
                for (int i = 0; i < p_data.RAA001_LIST[idx].MNTR_MASR_DT.Count; i++)
                {
                    list.Add(new CDataDisp("마취 중 감시측정 측정일시" + (i + 1), p_data.RAA001_LIST[idx].MNTR_MASR_DT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("산소포화도" + (i + 1), p_data.RAA001_LIST[idx].OXY_STRT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("대뇌산소포화도" + (i + 1), p_data.RAA001_LIST[idx].CRBR_OXY_STRT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("TOF" + (i + 1), p_data.RAA001_LIST[idx].NRRT_CNDC_CD[i], "1.ratio 2.count").SetIsCaption("1"));
                    list.Add(new CDataDisp("ratio 상세" + (i + 1), p_data.RAA001_LIST[idx].NRRT_CNDC_RT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("count 상세" + (i + 1), p_data.RAA001_LIST[idx].NRRT_CNDC_CNT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("BIS" + (i + 1), p_data.RAA001_LIST[idx].BIS_CNT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("CO" + (i + 1), p_data.RAA001_LIST[idx].CROT_CNT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("CVP" + (i + 1), p_data.RAA001_LIST[idx].CVP_CNT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("특이사항" + (i + 1), p_data.RAA001_LIST[idx].RMK_TXT[i], "").SetIsCaption("1"));
                }

                // 5.마취 중 투약
                for (int i = 0; i < p_data.RAA001_LIST[idx].KND_CD.Count; i++)
                {
                    list.Add(new CDataDisp("마취 중 투약 분류" + (i + 1), p_data.RAA001_LIST[idx].KND_CD[i], "1.흡입마취제 2.정맥마취제 3.신경근차단제 4.국소마취제 5.근이완역전제 9.기타").SetIsCaption("1"));
                    list.Add(new CDataDisp("투약일시" + (i + 1), p_data.RAA001_LIST[idx].MDCT_DT(i), "").SetIsCaption("1"));
                    list.Add(new CDataDisp("종료일시" + (i + 1), p_data.RAA001_LIST[idx].CNTN_MDCT_END_DT(i), "").SetIsCaption("1"));
                    list.Add(new CDataDisp("약품명" + (i + 1), p_data.RAA001_LIST[idx].MDS_NM[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("1회투약량" + (i + 1), p_data.RAA001_LIST[idx].OQTY[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("투약 단위" + (i + 1), p_data.RAA001_LIST[idx].UNIT[i], "").SetIsCaption("1"));
                }

                // 6.섭취량 & 배설량
                list.Add(new CDataDisp("섭취량 총량", p_data.RAA001_LIST[idx].IN_TOT_QTY.ToString(), "").SetIsCaption("1"));
                list.Add(new CDataDisp("섭취량 수액", p_data.RAA001_LIST[idx].IN_IFSL_QTY.ToString(), "").SetIsCaption("1"));
                for (int i = 0; i < p_data.RAA001_LIST[idx].BLTS_KND.Count; i++)
                {
                    list.Add(new CDataDisp("혈액" + (i + 1), p_data.RAA001_LIST[idx].BLTS_KND[i] + " " + p_data.RAA001_LIST[idx].BLTS_QTY[i].ToString(), "").SetIsCaption("1"));
                }
                list.Add(new CDataDisp("배설량 총량", p_data.RAA001_LIST[idx].OUT_TOT_QTY.ToString(), "").SetIsCaption("1"));
                list.Add(new CDataDisp("배설량 배뇨", p_data.RAA001_LIST[idx].OUT_URNN_QTY.ToString(), "").SetIsCaption("1"));
                list.Add(new CDataDisp("배설량 실혈", p_data.RAA001_LIST[idx].OUT_BLD_QTY.ToString(), "").SetIsCaption("1"));
                list.Add(new CDataDisp("배설량 기타", p_data.RAA001_LIST[idx].OUT_ETC_QTY.ToString(), "").SetIsCaption("1"));

                // 7.마취관련 기록
                for (int i = 0; i < p_data.RAA001_LIST[idx].OCUR_DT.Count; i++)
                {
                    list.Add(new CDataDisp("마취기록 발생일시" + (i + 1), p_data.RAA001_LIST[idx].OCUR_DTM(i), "").SetIsCaption("1"));
                    list.Add(new CDataDisp("내용" + (i + 1), p_data.RAA001_LIST[idx].RCD_TXT[i], "").SetIsCaption("1"));
                }
            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRMM001(CData p_data)
        {
            this.Text = "투약기록자료(RMM001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RMM001_LIST.Count; idx++)
            {
                // A.기본정보

                // B.투약 정보
                if (idx == 0)
                {
                    list.Add(new CDataDisp("처방일자", "처방분류(1.원내 2.지참약 3.D/C(투여중단) 4.퇴원약 5.원외)", "약품명", "투여경로", "1회", "단위", "일투", "총투", "투여여부(0.해당사항 없음 1.정상투여 2.미시행)", "실시일시", "진료과").SetIsCaption("2"));
                }

                list.Add(new CDataDisp(p_data.RMM001_LIST[idx].ODT // 처방일자
                                      ,p_data.RMM001_LIST[idx].DIV_FG // 처방분류(1.원내 2.지참약 3.D/C(투여중단) 4.퇴원약 5.원외)
                                      ,p_data.RMM001_LIST[idx].ONM // 약품명
                                      ,p_data.RMM001_LIST[idx].FLDCD4 // 투여경로
                                      ,p_data.RMM001_LIST[idx].DQTY // 1회투약량
                                      ,p_data.RMM001_LIST[idx].DUNIT // 단위
                                      ,p_data.RMM001_LIST[idx].ORDCNT // 1일투여횟수
                                      ,p_data.RMM001_LIST[idx].ODAYCNT // 총투약일수(처방분류가 4.퇴원약 5.원외인 경우 필수로 기재)
                                      ,p_data.RMM001_LIST[idx].EXEC_FG // 투여여부(0.해당없음 1.정상투여 2.미시행)
                                      ,p_data.RMM001_LIST[idx].DODT // 실시일시(투여여부가 1.정상투여인 경우)
                                      ,p_data.RMM001_LIST[idx].DEPT_INFO // 진료과
                ));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataERD001(CData p_data)
        {
            this.Text = "진단검사결과지(ERD001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.ERD001_LIST.Count; idx++)
            {
                // A.기본정보

                // B.투약 정보
                if (idx == 0)
                {
                    list.Add(new CDataDisp("처방일시", "채취일시", "접수일시", "결과일시", "검체종류(01.혈액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)", "검체상세", "수가코드", "검사코드", "검사명", "검사결과", "참고치", "단위", "진료과","처방의").SetIsCaption("2"));
                }

                list.Add(new CDataDisp(p_data.ERD001_LIST[idx].ORDDTM // 처방일시
                                      , p_data.ERD001_LIST[idx].BLOODDTM // 채취일시
                                      , p_data.ERD001_LIST[idx].RCVDTM // 접수일시
                                      , p_data.ERD001_LIST[idx].VFYDTM // 결과일시
                                      , p_data.ERD001_LIST[idx].SPCCD // 검체종류(01.형액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
                                      , p_data.ERD001_LIST[idx].SPCNM // 검체종류상사(검제종류가 99.기타인 경우)
                                      , p_data.ERD001_LIST[idx].EDICD // 수가코드(EDI코드, 매핑되지 않으믄 00)
                                      , p_data.ERD001_LIST[idx].TESTCD // 검사코드(병원에서부여한코드)
                                      , p_data.ERD001_LIST[idx].TESTNM // 검사명(병원에서부여한명)
                                      , p_data.ERD001_LIST[idx].RSTVAL // 검사결과
                                      , p_data.ERD001_LIST[idx].REFERENCE // 참고치(검사의 정상범위를 기재)
                                      , p_data.ERD001_LIST[idx].UNIT // 검사단위
                                      , p_data.ERD001_LIST[0].DEPT_INFO // 처방의사명
                                      , p_data.ERD001_LIST[0].DRNM // 처방의사명
                                      ));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRTT001(CData p_data)
        {
            this.Text = "시술기록자료(RTT001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RTT001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("시술 시작일시", p_data.RTT001_LIST[idx].OPSDT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("시술 종료일시", p_data.RTT001_LIST[idx].OPEDT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("시술의 구분", p_data.RTT001_LIST[idx].DR_GUBUN, "1.집도의 2.보조의").SetIsCaption("1"));
                list.Add(new CDataDisp("시설의 진료과", p_data.RTT001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("시술의 성명", p_data.RTT001_LIST[idx].DRNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자 성명", p_data.RTT001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RTT001_LIST[idx].WDTM, "").SetIsCaption("1"));


                // B.시술정보
                for (int i = 0; i < p_data.RTT001_LIST[idx].ONM.Count; i++)
                {
                    list.Add(new CDataDisp("시술명" + (i + 1), p_data.RTT001_LIST[idx].ONM[i], p_data.RTT001_LIST[idx].ISPCD[i]).SetIsCaption("1"));
                }

                list.Add(new CDataDisp("시술 전 진단", p_data.RTT001_LIST[idx].PREDX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("시술 후 진단", p_data.RTT001_LIST[idx].POSDX, "").SetIsCaption("1"));
                list.Add(new CDataDisp("시술절차", p_data.RTT001_LIST[idx].SURFNDNPRO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("특이사항", p_data.RTT001_LIST[idx].RMKONAPP, "").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRNH001(CData p_data)
        {
            this.Text = "투석기록자료(RNH001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RNH001_LIST.Count; idx++)
            {
                // A. 기본정보
                list.Add(new CDataDisp("진료과목", p_data.RNH001_LIST[idx].DEPT_INFO, "").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의사 성명", p_data.RNH001_LIST[idx].DRNM, "").SetIsCaption("1"));


                // B.환자정보
                for (int i = 0; i < p_data.RNH001_LIST[idx].DXD.Count; i++)
                {
                    list.Add(new CDataDisp("진단명" + (i + 1), p_data.RNH001_LIST[idx].DXD[i], "").SetIsCaption("1"));
                }

                list.Add(new CDataDisp("투석종류", "1", "1.혈액투석 2.지속적대체요법").SetIsCaption("1"));

                // C.혈액투석
                for (int i = 0; i < p_data.RNH001_LIST[idx].CHKDT.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("시작일시", "종료일시", "건체중", "투석 전 체중", "투석 후 체중", "혈관통로(1.AVF(자가혈관 동정맥류) 2.AVG(인조혈관 동정맥류) 3.카테터)", "목표수분제거량", "항응고요법초기", "항응고요법유지", "투석기", "투석액", "작성자 성명").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RNH001_LIST[idx].SDTM(i) // 시작일시
                                          , p_data.RNH001_LIST[idx].EDTM(i) // 종료일시
                                          , p_data.RNH001_LIST[idx].LastWt[i] // 건체중
                                          , p_data.RNH001_LIST[idx].HMBeCurWt[i] // 투석 전 체중
                                          , p_data.RNH001_LIST[idx].HMAfCurWt[i] // 투석 후 제충
                                          , p_data.RNH001_LIST[idx].BLDVE_CH_CD(i) // 혈관통로(1.AVF(자가혈관 동정맥류) 2.AVG(인조혈관 동정맥류) 3.카테터) //
                                          , p_data.RNH001_LIST[idx].UFTOTAL[i] // 목표수분제거량 
                                          , p_data.RNH001_LIST[idx].AntiBaseOqty[i] // 항응고요법초기 
                                          , p_data.RNH001_LIST[idx].MaintOqty[i] // 항응고요법유지 
                                          , p_data.RNH001_LIST[idx].HMMachine[i] // 투석기 
                                          , p_data.RNH001_LIST[idx].HMFluid[i] // 투석액 
                                          , p_data.RNH001_LIST[idx].ENM[i] // 작성자 성명
                                          ));
                }

                // 혈액투석 상세
                for (int i = 0; i < p_data.RNH001_LIST[idx].VCHKDT.Count; i++)
                {
                    if (i == 0)
                        list.Add(new CDataDisp("측정일시", "혈압", "맥박", "혈류속도", "동맥압", "정맥압").SetIsCaption("2"));

                    list.Add(new CDataDisp(p_data.RNH001_LIST[idx].VCHKDTM(i) // 측정일시 
                                          , p_data.RNH001_LIST[idx].VTM[i] // 혈압 
                                          , p_data.RNH001_LIST[idx].Vpressure[i] // 맥박 
                                          , p_data.RNH001_LIST[idx].Vpulsation[i] // 혈류속도 
                                          , p_data.RNH001_LIST[idx].Vvein[i] // 동맥압 
                                          , p_data.RNH001_LIST[idx].VSPEED[i] // 정맥압
                                          ));
                }

                list.Add(new CDataDisp("협착모니터링 실시여부", "2", "1.Yes 2.No").SetIsCaption("1"));

                // E.간호기록
                for (int i = 0; i < p_data.RNH001_LIST[idx].N_CHKDT.Count; i++)
                {
                    if (i == 0)
                        list.Add(new CDataDisp("기록일시", "간호사 성명", "간호기록").SetIsCaption("2"));

                    list.Add(new CDataDisp(p_data.RNH001_LIST[idx].N_CHKDTM(i) // 기록일시
                                          , p_data.RNH001_LIST[idx].N_ENM[i] // 간호사 성명
                                          , p_data.RNH001_LIST[idx].N_Nursing[i] // 간호기록
                                          ));
                }

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRWN001(CData p_data)
        {
            this.Text = "신생아중환자실기록자료(RWN001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RWN001_LIST.Count; idx++)
            {
                // A. 신생아실중환자실 정보
                list.Add(new CDataDisp("출생일 확인 여부", p_data.RWN001_LIST[idx].BIRTH_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("출생일시", p_data.RWN001_LIST[idx].BIRTH_DT, "ccyymmddhhmm").SetIsCaption("1"));
                list.Add(new CDataDisp("출생장소", p_data.RWN001_LIST[idx].BIRTH_PLC_CD, "1.본원 2.타기관 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("출생장소 기타상세", p_data.RWN001_LIST[idx].BIRTH_PLC_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("분만형태", p_data.RWN001_LIST[idx].PARTU_FRM_CD, "1.자연분문 2.제왕절개 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("분만형태 기타상세", p_data.RWN001_LIST[idx].PARTU_FRM_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("재태기간", p_data.RWN001_LIST[idx].FTUS_DEV_TRM, "(  주  일)형태로").SetIsCaption("1"));
                list.Add(new CDataDisp("다태아여부", p_data.RWN001_LIST[idx].MEMB_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("다태아내용", p_data.RWN001_LIST[idx].MEMB_TXT, "예시) 세 쌍둥이 중 첫째 아기인 경우 3/1").SetIsCaption("1"));
                list.Add(new CDataDisp("Apgar Score", p_data.RWN001_LIST[idx].APSC_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("Apgar Score 내용", p_data.RWN001_LIST[idx].APSC_PNT, "예시) 1분 2점, 5분 8점인 경우:2/8").SetIsCaption("1"));
                list.Add(new CDataDisp("출생시체중", p_data.RWN001_LIST[idx].NBY_BIRTH_BWGT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("최초입실일시", p_data.RWN001_LIST[idx].FST_IPAT_DT, "ccyymmddhhmm").SetIsCaption("1"));
                list.Add(new CDataDisp("담당의 성명", p_data.RWN001_LIST[idx].CHRG_DR_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성자성명", p_data.RWN001_LIST[idx].WRTP_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실일시", p_data.RWN001_LIST[idx].SPRM_IPAT_DT, "ccyymmddhhmm").SetIsCaption("1"));
                list.Add(new CDataDisp("입실경로", p_data.RWN001_LIST[idx].SPRM_IPAT_PTH_CD, "1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입실경로 기타상세", p_data.RWN001_LIST[idx].IPAT_PTH_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유", p_data.RWN001_LIST[idx].NBY_IPAT_RS_CD, "1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유 재입실상세", p_data.RWN001_LIST[idx].RE_IPAT_RS_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실사유 기타상세", p_data.RWN001_LIST[idx].IPAT_RS_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입실시 체중", p_data.RWN001_LIST[idx].IPAT_NBY_BWGT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실상태", p_data.RWN001_LIST[idx].SPRM_DSCG_RST_CD, "01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실상태 기타상세", p_data.RWN001_LIST[idx].DSCG_RST_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망일시", p_data.RWN001_LIST[idx].DEATH_DT, "ccyymmddhhmm").SetIsCaption("1"));
                list.Add(new CDataDisp("원사인 상병분류기호", p_data.RWN001_LIST[idx].DEATH_SICK_SYM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망진단명", p_data.RWN001_LIST[idx].DEATH_DIAG_NM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴실일시", p_data.RWN001_LIST[idx].SPRM_DSCG_DT, "ccyymmddhhmm").SetIsCaption("1"));
                list.Add(new CDataDisp("인공호흡기 적용 여부", p_data.RWN001_LIST[idx].ATFL_RPRT_YN, "1.Yes 2.No").SetIsCaption("1"));
                list.Add(new CDataDisp("산소요법 적용 여부", p_data.RWN001_LIST[idx].OXY_CURE_YN, "1.Yes 2.No").SetIsCaption("1"));

                list.Add(new CDataDisp("삽입관 및 배액관 적용여부", p_data.RWN001_LIST[idx].CNNL_YN, "1.Yes 2.No").SetIsCaption("1"));
                for (int i = 0; i < p_data.RWN001_LIST[idx].CNNL_KND_CD.Count; i++)
                {
                    list.Add(new CDataDisp("삽입관 및 배액관 종류" + (i + 1), p_data.RWN001_LIST[idx].CNNL_KND_CD[i], "01.Umbilical venous catheter 02.Umbilical arterial catheter 03.Peripherally inserted central catheter 04.Arterial catheter 05.Central venous catheter 06.Tracheostomy 07.Endotracheal tube 99.기타").SetIsCaption("1"));
                    list.Add(new CDataDisp("삽입관 및 배액관유형-기타상세" + (i + 1), p_data.RWN001_LIST[idx].CNNL_KND_ETC_TXT[i], "").SetIsCaption("1"));
                    list.Add(new CDataDisp("삽입일시" + (i + 1), p_data.RWN001_LIST[idx].CNNL_INS_DT[i], "ccyymmddhhmm").SetIsCaption("1"));
                    list.Add(new CDataDisp("제거일시" + (i + 1), p_data.RWN001_LIST[idx].CNNL_DEL_DT[i], "ccyymmddhhmm").SetIsCaption("1"));
                }

                list.Add(new CDataDisp("장루유무", p_data.RWN001_LIST[idx].OSTM_YN, "1.Yes 2.No 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("장루유무-기타상세", p_data.RWN001_LIST[idx].OSTM_ETC_TXT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("기타처치 시행여부", p_data.RWN001_LIST[idx].ETC_DSPL_CD, "00.해당없음 01.광선요법 02.저체온요법 03.하기도 증기흡입요법 04.교환수혈 05.심폐소생술 99.기타").SetIsCaption("1"));

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void ShowDataRID001(CData p_data)
        {
            this.Text = "퇴원요약자료(RID001)";

            grdMain.DataSource = null;
            List<CDataDisp> list = new List<CDataDisp>();

            for (int idx = 0; idx < p_data.RID001_LIST.Count; idx++)
            {
                // A. 기본 정보
                list.Add(new CDataDisp("입원일시", p_data.RID001_LIST[idx].BEDEDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원과", p_data.RID001_LIST[idx].DEPTINFO_IN, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원의사명", p_data.RID001_LIST[idx].DRNM_IN, "").SetIsCaption("1"));

                list.Add(new CDataDisp("퇴원일시", p_data.RID001_LIST[idx].BEDODTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원과", p_data.RID001_LIST[idx].DEPTINFO_OUT, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원의사명", p_data.RID001_LIST[idx].DRNM_OUT, "").SetIsCaption("1"));

                list.Add(new CDataDisp("작성자 성명", p_data.RID001_LIST[idx].EMPNM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("작성일시", p_data.RID001_LIST[idx].WDTM, "").SetIsCaption("1"));

                // B.이전 입원 이력
                list.Add(new CDataDisp("30일내 재입원 여부", p_data.RID001_LIST[idx].REBED, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("30일내 재입원 사유", p_data.RID001_LIST[idx].REBED_REASON, "").SetIsCaption("1"));
                list.Add(new CDataDisp("재입원 계획 여부", p_data.RID001_LIST[idx].REBEDPLAN, "1.계획이 있는 재입원 2.계획이 없는 재입원").SetIsCaption("1"));
                list.Add(new CDataDisp("직전 퇴원일을 알고 있는지 여부", p_data.RID001_LIST[idx].PREOUT_CD, "1.알고 있음 2.모름").SetIsCaption("1"));
                list.Add(new CDataDisp("직전 퇴원일", p_data.RID001_LIST[idx].PREOUT_DT, "").SetIsCaption("1"));

                // C.입원경과

                // 주호소
                for (int i = 0; i < p_data.RID001_LIST[idx].COMPLAINT.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("주호소", "발병시기(평문)").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RID001_LIST[idx].COMPLAINT[i], p_data.RID001_LIST[idx].ERA[i]));
                }

                list.Add(new CDataDisp("입원사유 및 현병력", p_data.RID001_LIST[idx].HOPI, "").SetIsCaption("1"));
                list.Add(new CDataDisp("입원경과 및 치료과정", p_data.RID001_LIST[idx].COT, "").SetIsCaption("1"));

                // 처치 및 수술
                list.Add(new CDataDisp("시행일시", "처치 및 수술명").SetIsCaption("2"));
                if (p_data.RID001_LIST[idx].OPDT.Count < 1)
                {
                    list.Add(new CDataDisp("-", "-"));
                }
                else
                {
                    for (int i = 0; i < p_data.RID001_LIST[idx].OPDT.Count; i++)
                    {
                        list.Add(new CDataDisp(p_data.RID001_LIST[idx].OPDT[i] // 시행일시
                                              , p_data.RID001_LIST[idx].OPNAME[i] // 처치 및 수술명
                                              ));
                    }
                }

                // 검사소견
                list.Add(new CDataDisp("검사일시", "검사명", "검사결과일시","검사결과").SetIsCaption("2"));
                if (p_data.RID001_LIST[idx].GUMDT.Count < 1)
                {
                    list.Add(new CDataDisp("-", "-", "-","-"));
                }
                else
                {
                    for (int i = 0; i < p_data.RID001_LIST[idx].GUMDT.Count; i++)
                    {
                        list.Add(new CDataDisp(p_data.RID001_LIST[idx].GUMDT[i] // 검사일시
                                              , p_data.RID001_LIST[idx].GUMNM[i] // 검사명
                                              , p_data.RID001_LIST[idx].RSDT[i] // 검사결과일시
                                              , p_data.RID001_LIST[idx].GUMRESULT[i] // 검사결과
                                              ));
                    }
                }

                // 최종진단
                for (int i = 0; i < p_data.RID001_LIST[idx].DXD.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("상병분류구분(1.주상병 2.부상병 3.배제상병)", "상병분류기호", "진단명", "진료과","입원시 상병여부(1.Yes 2.No 3.Unknown 4.Undetermined 5.기타예외상병)").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RID001_LIST[idx].ROFG[i] // 상병분류구분(1.주상병 2.부상병 3.배제상병)
                                          , p_data.RID001_LIST[idx].DISECD[i] // 상병분류기호
                                          , p_data.RID001_LIST[idx].DXD[i] // 진단명
                                          , p_data.RID001_LIST[idx].DEPT_INFO(i) // 진료과
                                          , p_data.RID001_LIST[idx].POA_CD(i) // 입원시 상병여부(1.Yes 2.No 3.Unknown 4.Undetermined 5.기타예외상병)
                                          ));
                }

                // 전과
                list.Add(new CDataDisp("전과일시", "의뢰과", "의뢰의사 성명", "회신과", "회신의사 성명", "전과사유").SetIsCaption("2"));
                if (p_data.RID001_LIST[idx].TR_DATE.Count < 1)
                {
                    list.Add(new CDataDisp("-", "-", "-", "-", "-", "-"));
                }
                else
                {
                    for (int i = 0; i < p_data.RID001_LIST[idx].TR_DATE.Count; i++)
                    {
                        list.Add(new CDataDisp(p_data.RID001_LIST[idx].TR_DATE[i] // 전과일시
                                              , p_data.RID001_LIST[idx].TR_OUT_DEPT_INFO(i) // 의뢰과
                                              , p_data.RID001_LIST[idx].TR_OUT_DRNM[i] // 의뢰의사 성명
                                              , p_data.RID001_LIST[idx].TR_IN_DEPT_INFO(i) // 회신과
                                              , p_data.RID001_LIST[idx].TR_IN_DRNM[i] // 회신의사 성명
                                              , p_data.RID001_LIST[idx].TR_OUT_REASON[i] // 전과사유
                                              ));
                    }
                }

                list.Add(new CDataDisp("약물이상반응여부", p_data.RID001_LIST[idx].ALLERGY_CD, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("약물이상반응내용", p_data.RID001_LIST[idx].ALLERGY_DESC, "").SetIsCaption("1"));

                // 환자 상태 척도
                for (int i = 0; i < p_data.RID001_LIST[idx].ERA_CD.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("환자상태 측정시기(1.입원 당시 2.입원 중 3.퇴원시 9.기타)", "측정시기상세", "도구", "결과", "참고사항").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RID001_LIST[idx].ERA_CD[i] // 환자상태 측정시기(1.입원 당시 2.입원 중 3.퇴원시 9.기타)
                                          , p_data.RID001_LIST[idx].ERA_ETC_TXT[i] // "측정시기상세
                                          , p_data.RID001_LIST[idx].TL_NM[i] // 도구
                                          , p_data.RID001_LIST[idx].RST_TXT[i] // 결과
                                          , p_data.RID001_LIST[idx].RMK_TXT[i] // 참고사항
                                          )); 
                }

                list.Add(new CDataDisp("감염 발생여부", p_data.RID001_LIST[idx].HEPA_CD, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("감염 상세내용", p_data.RID001_LIST[idx].HEPA_DESC, "").SetIsCaption("1"));
                list.Add(new CDataDisp("합병증 발생여부", p_data.RID001_LIST[idx].COMPLICATION_CD, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("합병증 상세내용", p_data.RID001_LIST[idx].COMPLICATION_DESC, "").SetIsCaption("1"));
                list.Add(new CDataDisp("환자안전관리 특이사항 발생여부", p_data.RID001_LIST[idx].PTNT_YN, "1.Yes 2.No 3.확인불가").SetIsCaption("1"));
                list.Add(new CDataDisp("환자안전관리 특이사항 상세내용", p_data.RID001_LIST[idx].PTNT_TXT, "").SetIsCaption("1"));

                // D.퇴원현황

                list.Add(new CDataDisp("퇴원형태", p_data.RID001_LIST[idx].OUTREASON_CD, "01.정상퇴원 02.자의퇴원 03.탈원 04.가망없는 퇴원 05.전원 06.사망 99.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원형태 기타상세", p_data.RID001_LIST[idx].OUTREASON_DESC, "").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원시 환자상태", p_data.RID001_LIST[idx].OUTSTATUS_CD, "1.완쾌 2.호전 3.호전 안 됨 4.치료 없이 진단만 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원시 환자상태 기타상세", p_data.RID001_LIST[idx].OUTSTATUS_DESC, "").SetIsCaption("1"));
                list.Add(new CDataDisp("사망일시", p_data.RID001_LIST[idx].DEATHDTM, "").SetIsCaption("1"));
                list.Add(new CDataDisp("원사인", p_data.RID001_LIST[idx].DEATH_SICK, "").SetIsCaption("1"));
                list.Add(new CDataDisp("진단명", p_data.RID001_LIST[idx].DEATH_DIAG, "").SetIsCaption("1"));
                list.Add(new CDataDisp("전원사유", p_data.RID001_LIST[idx].DHI_RS_TXT, "평문").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원후 진료계획", p_data.RID001_LIST[idx].OUTCARE_CD, "0.해당사항 없음 1.외래예약 2.재입원 예정 9.기타").SetIsCaption("1"));
                list.Add(new CDataDisp("퇴원후 진료계획 상세", p_data.RID001_LIST[idx].OUTCARE_DESC, "").SetIsCaption("1"));

                // E.퇴원처방
                for (int i = 0; i < p_data.RID001_LIST[idx].ORDER_TYPE.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CDataDisp("처방구분(0.해당없음 1.원내 2.원외)", "약품명", "용법", "1회 투약량", "1일 투여횟수", "총 투약일수").SetIsCaption("2"));
                    }
                    list.Add(new CDataDisp(p_data.RID001_LIST[idx].ORDER_TYPE[i] // 처방구분(0.해당없음 1.원내 2.원외)
                                          , p_data.RID001_LIST[idx].ONM[i] // 약품명
                                          , p_data.RID001_LIST[idx].OUNIT[i] // 용법
                                          , p_data.RID001_LIST[idx].OQTY[i] // 1회 투약량
                                          , p_data.RID001_LIST[idx].ORDCNT[i] // 1일 투여횟수
                                          , p_data.RID001_LIST[idx].ODAYCNT[i] // 총 투약일수
                                          ));
                }

            }

            grdMain.DataSource = list;
            RefreshGridMain();
        }

        private void grdMainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            for (int i = 0; i < m_VisibleColumnCount; i++)
            {
                string value_field_name = "VALUE" + i;
                string cap_field_name = "IsCAPTION" + i;

                if (e.Column.FieldName == value_field_name)
                {
                    string is_caption = view.GetRowCellValue(e.RowHandle, cap_field_name).ToString();
                    if (is_caption == "1")
                    {
                        e.Appearance.BackColor = Color.DarkGray;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.White;
                    }
                }


            }
        }

    }
}

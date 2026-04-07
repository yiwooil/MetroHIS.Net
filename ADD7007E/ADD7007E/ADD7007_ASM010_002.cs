using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7007E
{
    public partial class ADD7007_ASM010_002 : Form
    {
        public event EventHandler<RemakeRequestedEventArgs<CDataASM010_002>> RemakeRequested;
        private ADD7007_ERROR f_err = new ADD7007_ERROR();

        // 수술
        class BLTS
        {
            public string BLTS_STA_DT_DATE { get; set; } // 수혈시작일시
            public string BLTS_STA_DT_TIME { get; set; }
            public string BLTS_END_DT_DATE { get; set; } // 수혈종료일시
            public string BLTS_END_DT_TIME { get; set; }
            public string BLTS_MDFEE_CD { get; set; } // 수가코드
            public string BLTS_DGM_NM { get; set; } // 수혈제제명

            public BLTS()
            {
                BLTS_STA_DT_DATE = ""; // 수혈시작일시
                BLTS_STA_DT_TIME = "";
                BLTS_END_DT_DATE = ""; // 수혈종료일시
                BLTS_END_DT_TIME = "";
                BLTS_MDFEE_CD = ""; // 수가코드
                BLTS_DGM_NM = ""; // 수혈제제명
            }
        }

        // 항생제 투여
        class INJC
        {
            public string INJC_STA_DT_DATE { get; set; } // 투여시작일시
            public string INJC_STA_DT_TIME { get; set; }
            public string INJC_END_DT_DATE { get; set; } // 투여종료일시
            public string INJC_END_DT_TIME { get; set; }
            public string INJC_MDS_CD { get; set; } // 약품코드
            public string INJC_MDS_NM { get; set; } // 약품명
            public string ANBO_INJC_PTH_CD { get; set; } // 투여경로

            public INJC()
            {
                INJC_STA_DT_DATE = ""; // 투여시작일시
                INJC_STA_DT_TIME = "";
                INJC_END_DT_DATE = ""; // 투여종료일시
                INJC_END_DT_TIME = "";
                INJC_MDS_CD = ""; // 약품코드
                INJC_MDS_NM = ""; // 약품명
                ANBO_INJC_PTH_CD = ""; // 투여경로
            }
        }

        // 퇴원시 항생제 처방
        class PRSC
        {
            public string PRSC_MDS_CD { get; set; } // 약품코드
            public string PRSC_MDS_NM { get; set; } // 약품명
            public string PRSC_TOT_INJC_DDCNT { get; set; } // 총 투약일수

            public PRSC()
            {
                PRSC_MDS_CD = ""; // 약품코드
                PRSC_MDS_NM = ""; // 약품명
                PRSC_TOT_INJC_DDCNT = ""; // 총 투약일수
            }
        }


        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM010_002 m_data;

        public ADD7007_ASM010_002()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboGrid();
        }

        public ADD7007_ASM010_002(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM010_002> list = (List<CDataASM010_002>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
            // ASA점수
            cboASA_PNT.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "정상(Class 1)" },
                new { Key = "2", Value = "경증질환/정상활동 가능(Class 2)" },
                new { Key = "3", Value = "중증질환/활동제한(Class 3)" },
                new { Key = "4", Value = "생명위협 중증질환(Class 4)" },
                new { Key = "5", Value = "수술하지 않으면 사망예상(Class 5)" },
                new { Key = "6", Value = "이식수술(Class 6)" },
                new { Key = "7", Value = "기록없음" },
                new { Key = "8", Value = "응급" },
            };
            cboASA_PNT.DisplayMember = "Value";
            cboASA_PNT.ValueMember = "Key";
            // 감염상병확진명
            cboSOPR_BF_INFC_SICK_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "01", Value = "골막염" },
                new { Key = "02", Value = "골수염" },
                new { Key = "03", Value = "개방성 골절" },
                new { Key = "04", Value = "개방성 상처" },
                new { Key = "05", Value = "급성 담낭염" },
                new { Key = "06", Value = "농양" },
                new { Key = "07", Value = "농흉" },
                new { Key = "08", Value = "뇌염" },
                new { Key = "09", Value = "동맥염" },
                new { Key = "10", Value = "림프관염" },
                new { Key = "11", Value = "방광염" },
                new { Key = "12", Value = "복막염" },
                new { Key = "13", Value = "수막염" },
                new { Key = "14", Value = "심근염" },
                new { Key = "15", Value = "연골염" },
                new { Key = "16", Value = "요도염" },
                new { Key = "17", Value = "요로감염" },
                new { Key = "18", Value = "조기양막파수" },
                new { Key = "19", Value = "좁쌀결핵" },
                new { Key = "20", Value = "천공" },
                new { Key = "21", Value = "치밀골염" },
                new { Key = "22", Value = "패혈증" },
                new { Key = "23", Value = "폐렴" },
                new { Key = "24", Value = "혈관염" },
                new { Key = "25", Value = "흉막염" }
            };
            cboSOPR_BF_INFC_SICK_CD.DisplayMember = "Value";
            cboSOPR_BF_INFC_SICK_CD.ValueMember = "Key";
            // 기록지 종류
            cboSOPR_BF_ANBO_DR_RCDC_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "경과기록지" },
                new { Key = "2", Value = "입퇴원기록지" },
                new { Key = "3", Value = "협진기록지" },
                new { Key = "9", Value = "기타" }
            };
            cboSOPR_BF_ANBO_DR_RCDC_CD.DisplayMember = "Value";
            cboSOPR_BF_ANBO_DR_RCDC_CD.ValueMember = "Key";
            // 필요사유
            cboSOPR_BF_REQR_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "감염상병 외 상병" },
                new { Key = "2", Value = "38도 이상의 고열" },
                new { Key = "3", Value = "감염내과 외 협진" },
                new { Key = "9", Value = "기타" }
            };
            cboSOPR_BF_REQR_RS_CD.DisplayMember = "Value";
            cboSOPR_BF_REQR_RS_CD.ValueMember = "Key";
            // 기록지 종류
            cboSOPR_RGN_INFC_DR_RCDC_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "경과기록지" },
                new { Key = "2", Value = "입퇴원기록지" },
                new { Key = "3", Value = "협진기록지" },
                new { Key = "9", Value = "기타" },
            };
            cboSOPR_RGN_INFC_DR_RCDC_CD.DisplayMember = "Value";
            cboSOPR_RGN_INFC_DR_RCDC_CD.ValueMember = "Key";
            // 감염상병 확진명
            cboSOPR_AF_INFC_SICK_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "01", Value = "골막염" },
                new { Key = "02", Value = "골수염" },
                new { Key = "03", Value = "개방성 상처" },
                new { Key = "04", Value = "급성 담낭염" },
                new { Key = "05", Value = "농양" },		
                new { Key = "06", Value = "농흉" },		
                new { Key = "07", Value = "뇌염" },		
                new { Key = "08", Value = "동맥염" },		
                new { Key = "09", Value = "림프관염" },		
                new { Key = "10", Value = "방광염" },		
                new { Key = "11", Value = "복막염" },		
                new { Key = "12", Value = "수막염" },		
                new { Key = "13", Value = "심근염" },		
                new { Key = "14", Value = "연골염" },		
                new { Key = "15", Value = "요도염" },		
                new { Key = "16", Value = "요로감염" },		
                new { Key = "17", Value = "좁쌀결핵" },		
                new { Key = "18", Value = "천공" },		
                new { Key = "19", Value = "치밀골염" },		
                new { Key = "20", Value = "패혈증" },		
                new { Key = "21", Value = "폐렴" },		
                new { Key = "22", Value = "혈관염" },		
                new { Key = "23", Value = "흉막염" },
                new { Key = "24", Value = "비의도적 절단" },
            };
            cboSOPR_AF_INFC_SICK_CD.DisplayMember = "Value";
            cboSOPR_AF_INFC_SICK_CD.ValueMember = "Key";
            // 수술부위 감염 유형
            cboSOPR_RGN_INFC_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "절개부위 또는 심부에 위치한 드레인의 농성배액" },
                new { Key = "2", Value = "절개부위, 심부 또는 기관에서 무균적으로 채취한 검체의 배양에서 균 분리" },		
                new { Key = "3", Value = "38℃ 이상의 발열, 국소동통, 압통, 발적 등 감염증상 중 하나 이상의 증상이 있고, 수술 창상의 심부가 저절로 파열되거나 외과의사가 개방한 경우" },
                new { Key = "4", Value = "조직병리검사, 방사선검사 등에서 심부 절개부위, 기관 또는 강의 농양이나 감염 증거 관찰" },
                new { Key = "5", Value = "수술의, 주치의 또는 감염내과 의사에 의한 수술 부위 감염 진단" },
            };
            cboSOPR_RGN_INFC_CD.DisplayMember = "Value";
            cboSOPR_RGN_INFC_CD.ValueMember = "Key";
            // 기록지 종류
            cboSOPR_AF_ANBO_DR_RCDC_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "경과기록지" },
                new { Key = "2", Value = "입퇴원기록지" },		
                new { Key = "3", Value = "협진기록지" },
                new { Key = "9", Value = "기타" },
            };
            cboSOPR_AF_ANBO_DR_RCDC_CD.DisplayMember = "Value";
            cboSOPR_AF_ANBO_DR_RCDC_CD.ValueMember = "Key";
            // 필요사유
            cboSOPR_AF_REQR_RS_CD.DataSource = new[]
            {
                new { Key = "", Value = "" },
                new { Key = "1", Value = "감염상병 외 상병" },
                new { Key = "2", Value = "38도 이상의 고열" },		
                new { Key = "3", Value = "감염내과 외 협진" },
                new { Key = "9", Value = "기타" },
            };
            cboSOPR_AF_REQR_RS_CD.DisplayMember = "Value";
            cboSOPR_AF_REQR_RS_CD.ValueMember = "Key";
        }

        private void MakeComboGrid()
        {
            CUtil.SetGridCombo(grdINJCView.Columns["ANBO_INJC_PTH_CD"],// 퇴실현황
                "",
                "IV",
                "IM",
                "PO"
                );
        }

        private void ShowData()
        {
            if (m_data.STATUS == "E" || m_data.STATUS == "F")
            {
                f_err.SetError(m_data.ERR_CODE, m_data.ERR_DESC);
                f_err.Show(this.Parent);
            }
            else
            {
                f_err.Hide();
            }

            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // A. 기본정보
            txtASM_IPAT_DT_DATE.Text = CUtil.GetDate(m_data.ASM_IPAT_DT); // 입원일시(YYYYMMDDHHMM)
            txtASM_IPAT_DT_TIME.Text = CUtil.GetTime(m_data.ASM_IPAT_DT);
            rbDSCG_YN_1.Checked = m_data.DSCG_YN == "1"; // 퇴원여부(1.Yes 2.No)
            rbDSCG_YN_2.Checked = m_data.DSCG_YN == "2";
            txtASM_DSCG_DT_DATE.Text = CUtil.GetDate(m_data.ASM_DSCG_DT); // 퇴원일시(YYYYMMDDHHMM)
            txtASM_DSCG_DT_TIME.Text = CUtil.GetTime(m_data.ASM_DSCG_DT);


            // B.수술 및 감염 정보
            // 1. 수술 관련 환자 상태
            txtMDFEE_CD.Text = m_data.MDFEE_CD; // 수가코드
            txtASM_SOPR_STA_DT_DATE.Text = CUtil.GetDate(m_data.ASM_SOPR_STA_DT); // 수술 시작일시(YYYYMMDDHHMM)
            txtASM_SOPR_STA_DT_TIME.Text = CUtil.GetTime(m_data.ASM_SOPR_STA_DT);
            txtASM_SOPR_END_DT_DATE.Text = CUtil.GetDate(m_data.ASM_SOPR_END_DT); // 수술 종료일시(YYYYMMDDHHMM)
            txtASM_SOPR_END_DT_TIME.Text = CUtil.GetTime(m_data.ASM_SOPR_END_DT);
            rbEMY_CD_1.Checked = m_data.EMY_CD == "1"; // 응급여부(1.정규 2.응급)
            rbEMY_CD_2.Checked = m_data.EMY_CD == "2";
            rbKNJN_RPMT_1.Checked = m_data.KNJN_RPMT == "1"; // 슬관절치환술(1.Yes 2.No)
            rbKNJN_RPMT_2.Checked = m_data.KNJN_RPMT == "2";
            rbHMRHG_CTRL_YN_1.Checked = m_data.HMRHG_CTRL_YN == "1"; // 토니켓 적용 여부(1.Yes 2.No)
            rbHMRHG_CTRL_YN_2.Checked = m_data.HMRHG_CTRL_YN == "2";
            txtHMRHG_CTRL_DT_DATE.Text = CUtil.GetDate(m_data.HMRHG_CTRL_DT); // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            txtHMRHG_CTRL_DT_TIME.Text = CUtil.GetTime(m_data.HMRHG_CTRL_DT);
            rbCAESR_YN_1.Checked = m_data.CAESR_YN == "1"; // 제왕절개술 시행 여부(1.Yes 2.No)
            rbCAESR_YN_2.Checked = m_data.CAESR_YN == "2";
            txtNBY_PARTU_DT_DATE.Text = CUtil.GetDate(m_data.NBY_PARTU_DT); // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
            txtNBY_PARTU_DT_TIME.Text = CUtil.GetTime(m_data.NBY_PARTU_DT);
            rbCRVD_YN_1.Checked = m_data.CRVD_YN == "1"; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            rbCRVD_YN_2.Checked = m_data.CRVD_YN == "2";
            rbBSE_NCT_YN_1.Checked = m_data.BSE_NCT_YN == "1"; // 기본마취 여부(1.Yes 2.No)
            rbBSE_NCT_YN_2.Checked = m_data.BSE_NCT_YN == "2";
            CUtil.SetComboboxSelectedValue(cboASA_PNT, m_data.ASA_PNT); // ASA 점수
            // 2.수술 전 항생제 투여
            rbSOPR_BF_ANBO_INJC_YN_1.Checked = m_data.SOPR_BF_ANBO_INJC_YN == "1"; // 수술 전 항생제 투여 여부(1.Yes 2.No)
            rbSOPR_BF_ANBO_INJC_YN_2.Checked = m_data.SOPR_BF_ANBO_INJC_YN == "2";
            rbSOPR_BF_INFC_SICK_YN_1.Checked = m_data.SOPR_BF_INFC_SICK_YN == "1"; // 감염상병 확진 여부(1.Yes 2.No)
            rbSOPR_BF_INFC_SICK_YN_2.Checked = m_data.SOPR_BF_INFC_SICK_YN == "2";
            CUtil.SetComboboxSelectedValue(cboSOPR_BF_INFC_SICK_CD, m_data.SOPR_BF_INFC_SICK_CD); // 감염상병 확진명
            rbSOPR_BF_DDIAG_YN_1.Checked = m_data.SOPR_BF_DDIAG_YN == "1"; // 감염내과 협진여부(1.Yes 2.No)
            rbSOPR_BF_DDIAG_YN_2.Checked = m_data.SOPR_BF_DDIAG_YN == "2";
            txtSOPR_BF_ASM_REQ_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_BF_ASM_REQ_DT); // 의뢰일시(YYYYMMDDHHMM)
            txtSOPR_BF_ASM_REQ_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_BF_ASM_REQ_DT);
            rbSOPR_BF_RPY_YN_1.Checked = m_data.SOPR_BF_RPY_YN == "1"; // 회신 여부(1.Yes 2.No)
            rbSOPR_BF_RPY_YN_2.Checked = m_data.SOPR_BF_RPY_YN == "2";
            txtSOPR_BF_ASM_RPY_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_BF_ASM_RPY_DT); // 회신일시(YYYYMMDDHHMM)
            txtSOPR_BF_ASM_RPY_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_BF_ASM_RPY_DT);
            rbSOPR_BF_ANBO_DR_RCD_YN_1.Checked = m_data.SOPR_BF_ANBO_DR_RCD_YN == "1"; // 항생제 필요 의사기록 여부(1.Yes 2.No)
            rbSOPR_BF_ANBO_DR_RCD_YN_2.Checked = m_data.SOPR_BF_ANBO_DR_RCD_YN == "2";
            txtSOPR_BF_ASM_RCD_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_BF_ASM_RCD_DT); // 기록일시(YYYYMMDDHHMM)
            txtSOPR_BF_ASM_RCD_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_BF_ASM_RCD_DT);
            CUtil.SetComboboxSelectedValue(cboSOPR_BF_ANBO_DR_RCDC_CD, m_data.SOPR_BF_ANBO_DR_RCDC_CD); // 기록지 종류
            CUtil.SetComboboxSelectedValue(cboSOPR_BF_REQR_RS_CD, m_data.SOPR_BF_REQR_RS_CD); // 필요사유
            txtSOPR_BF_DR_RCD_TXT.Text = m_data.SOPR_BF_DR_RCD_TXT; // 기록 상세 내용(평문)
            // 3.평가 제외 수술
            rbASM_TGT_SOPR_SAME_ENFC_YN_1.Checked = m_data.ASM_TGT_SOPR_SAME_ENFC_YN == "1"; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
            rbASM_TGT_SOPR_SAME_ENFC_YN_2.Checked = m_data.ASM_TGT_SOPR_SAME_ENFC_YN == "2";
            rbFQ2_GT_SOPR_ENFC_YN_1.Checked = m_data.FQ2_GT_SOPR_ENFC_YN == "1"; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
            rbFQ2_GT_SOPR_ENFC_YN_2.Checked = m_data.FQ2_GT_SOPR_ENFC_YN == "2";
            // 4.수술 후 항셍제 투여
            rbSOPR_RGN_INFC_ANBO_INJC_YN_1.Checked = m_data.SOPR_RGN_INFC_ANBO_INJC_YN == "1"; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
            rbSOPR_RGN_INFC_ANBO_INJC_YN_2.Checked = m_data.SOPR_RGN_INFC_ANBO_INJC_YN == "2";
            CUtil.SetComboboxSelectedValue(cboSOPR_RGN_INFC_CD, m_data.SOPR_RGN_INFC_CD); // 수술부위 감영 유형
            txtASM_RCD_DT_DATE.Text = CUtil.GetDate(m_data.ASM_RCD_DT); // 기록일시(YYYYMMDDHHMM)
            txtASM_RCD_DT_TIME.Text = CUtil.GetTime(m_data.ASM_RCD_DT);
            CUtil.SetComboboxSelectedValue(cboSOPR_RGN_INFC_DR_RCDC_CD, m_data.SOPR_RGN_INFC_DR_RCDC_CD); // 기록지 종류
            txtSOPR_RGN_INFC_DR_RCD_TXT.Text = m_data.SOPR_RGN_INFC_DR_RCD_TXT; // 수술 부위 감염 사유 상세(평문)
            rbINFC_ANBO_INJC_YN_1.Checked = m_data.INFC_ANBO_INJC_YN == "1"; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
            rbINFC_ANBO_INJC_YN_2.Checked = m_data.INFC_ANBO_INJC_YN == "2";
            rbCLTR_STRN_YN_1.Checked = m_data.CLTR_STRN_YN == "1"; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
            rbCLTR_STRN_YN_2.Checked = m_data.CLTR_STRN_YN == "2";
            txtASM_GAT_DT_DATE.Text = CUtil.GetDate(m_data.ASM_GAT_DT); // 채취일시(YYYYMMDDHHMM)
            txtASM_GAT_DT_TIME.Text = CUtil.GetTime(m_data.ASM_GAT_DT);
            rbINFC_SICK_DIAG_1.Checked = m_data.INFC_SICK_DIAG == "1"; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
            rbINFC_SICK_DIAG_2.Checked = m_data.INFC_SICK_DIAG == "2";
            CUtil.SetComboboxSelectedValue(cboSOPR_AF_INFC_SICK_CD, m_data.SOPR_AF_INFC_SICK_CD); // 감염 상병 화진명
            rbSOPR_AF_DDIAG_YN_1.Checked = m_data.SOPR_AF_DDIAG_YN == "1"; // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
            rbSOPR_AF_DDIAG_YN_2.Checked = m_data.SOPR_AF_DDIAG_YN == "2";
            txtSOPR_AF_ASM_REQ_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_AF_ASM_REQ_DT); // 의뢰일시(YYYYMMDDHHMM)
            txtSOPR_AF_ASM_REQ_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_AF_ASM_REQ_DT);
            rbSOPR_AF_RPY_YN_1.Checked = m_data.SOPR_AF_RPY_YN == "1"; // 회신 여부(1.Yes 2.No)
            rbSOPR_AF_RPY_YN_2.Checked = m_data.SOPR_AF_RPY_YN == "2";
            txtSOPR_AF_ASM_RPY_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_AF_ASM_RPY_DT); // 회신일시(YYYYMMDDHHMM)
            txtSOPR_AF_ASM_RPY_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_AF_ASM_RPY_DT);
            rbSOPR_AF_ANBO_DR_RCD_YN_1.Checked = m_data.SOPR_AF_ANBO_DR_RCD_YN == "1"; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
            rbSOPR_AF_ANBO_DR_RCD_YN_2.Checked = m_data.SOPR_AF_ANBO_DR_RCD_YN == "2";
            txtSOPR_AF_ASM_RCD_DT_DATE.Text = CUtil.GetDate(m_data.SOPR_AF_ASM_RCD_DT); // 기록일시(YYYYMMDDHHMM)
            txtSOPR_AF_ASM_RCD_DT_TIME.Text = CUtil.GetTime(m_data.SOPR_AF_ASM_RCD_DT);
            CUtil.SetComboboxSelectedValue(cboSOPR_AF_ANBO_DR_RCDC_CD, m_data.SOPR_AF_ANBO_DR_RCDC_CD); // 기록지 종류
            CUtil.SetComboboxSelectedValue(cboSOPR_AF_REQR_RS_CD, m_data.SOPR_AF_REQR_RS_CD); // 필요사유
            txtSOPR_AF_DR_RCD_TXT.Text = m_data.SOPR_AF_DR_RCD_TXT; // 기록 상세 내용(평문)
            rbANBO_ALRG_YN_1.Checked = m_data.ANBO_ALRG_YN == "1"; // 항생제 알러지 여부(1.Yes 2.No)
            rbANBO_ALRG_YN_2.Checked = m_data.ANBO_ALRG_YN == "2";
            rbWHBL_RBC_BLTS_YN_1.Checked = m_data.WHBL_RBC_BLTS_YN == "1"; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            rbWHBL_RBC_BLTS_YN_2.Checked = m_data.WHBL_RBC_BLTS_YN == "2";

            // 수혈
            grdBLTS.DataSource = null;
            List<BLTS> listBLTS = new List<BLTS>();
            grdBLTS.DataSource = listBLTS;
            for (int i = 0; i < m_data.BLTS_STA_DT.Count; i++)
            {
                BLTS data = new BLTS();
                data.BLTS_STA_DT_DATE = CUtil.GetDate(m_data.BLTS_STA_DT[i]); // 수혈시작일시
                data.BLTS_STA_DT_TIME = CUtil.GetTime(m_data.BLTS_STA_DT[i]);
                data.BLTS_END_DT_DATE = CUtil.GetDate(m_data.BLTS_END_DT[i]); // 수혈종료일시
                data.BLTS_END_DT_TIME = CUtil.GetTime(m_data.BLTS_END_DT[i]);
                data.BLTS_MDFEE_CD = m_data.BLTS_MDFEE_CD[i]; // 수가코드
                data.BLTS_DGM_NM = m_data.BLTS_DGM_NM[i]; // 수혈제제명

                listBLTS.Add(data);
            }

            // C.항생제 투여 여부
            rbANBO_USE_YN_1.Checked = m_data.ANBO_USE_YN == "1"; // 항생제 투여 여부(1.Yes 2.No)
            rbANBO_USE_YN_2.Checked = m_data.ANBO_USE_YN == "2";

            // 항생제 투여
            grdINJC.DataSource = null;
            List<INJC> listINJC = new List<INJC>();
            grdINJC.DataSource = listINJC;
            for (int i = 0; i < m_data.INJC_STA_DT.Count; i++)
            {
                INJC data = new INJC();
                data.INJC_STA_DT_DATE = CUtil.GetDate(m_data.INJC_STA_DT[i]); // 투여시작일시
                data.INJC_STA_DT_TIME = CUtil.GetTime(m_data.INJC_STA_DT[i]);
                data.INJC_END_DT_DATE = CUtil.GetDate(m_data.INJC_END_DT[i]); // 투여종료일시
                data.INJC_END_DT_TIME = CUtil.GetTime(m_data.INJC_END_DT[i]);
                data.INJC_MDS_CD = m_data.INJC_MDS_CD[i]; // 약품코드
                data.INJC_MDS_NM = m_data.INJC_MDS_NM[i]; // 약품명
                data.ANBO_INJC_PTH_CD = GetANBO_INJC_PTH_CD_NM(m_data.ANBO_INJC_PTH_CD[i]); // 투여경로

                listINJC.Add(data);
            }

            // 퇴원 시 항생제 처방 여부
            rbDSCG_ANBO_PRSC_YN_1.Checked = m_data.DSCG_ANBO_PRSC_YN == "1"; // 퇴원시 항생제 처방 여부(1.Yes 2.No)
            rbDSCG_ANBO_PRSC_YN_2.Checked = m_data.DSCG_ANBO_PRSC_YN == "2";

            // 퇴원 시 항생제 처방
            grdPRSC.DataSource = null;
            List<PRSC> listPRSC = new List<PRSC>();
            grdPRSC.DataSource = listPRSC;
            for (int i = 0; i < m_data.PRSC_MDS_CD.Count; i++)
            {
                PRSC data = new PRSC();
                data.PRSC_MDS_CD = m_data.PRSC_MDS_CD[i]; // 약품코드
                data.PRSC_MDS_NM = m_data.PRSC_MDS_NM[i]; // 약품명
                data.PRSC_TOT_INJC_DDCNT = m_data.PRSC_TOT_INJC_DDCNT[i]; // 총 투약일수

                listPRSC.Add(data);
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdBLTS, grdBLTSView);
            CUtil.RefreshGrid(grdINJC, grdINJCView);
            CUtil.RefreshGrid(grdPRSC, grdPRSCView);
        }

        //private string GetASA_PNT_NM(string p_value)
        //{
        //    if (p_value == "1") return "정상(Class 1)";
        //    if (p_value == "2") return "경증질환/정상활동 가능(Class 2)";
        //    if (p_value == "3") return "중증질환/활동제한(Class 3)";
        //    if (p_value == "4") return "생명위협 중증질환(Class 4)";
        //    if (p_value == "5") return "수술하지 않으면 사망예상(Class 5)";
        //    if (p_value == "6") return "이식수술(Class 6)";
        //    if (p_value == "7") return "기록없음";
        //    if (p_value == "8") return "응급";
        //    return "";
        //}

        //private string GetASA_PNT(string p_value)
        //{
        //    if (p_value == "정상(Class 1)") return "1";
        //    if (p_value == "경증질환/정상활동 가능(Class 2)") return "2";
        //    if (p_value == "중증질환/활동제한(Class 3)") return "3";
        //    if (p_value == "생명위협 중증질환(Class 4)") return "4";
        //    if (p_value == "수술하지 않으면 사망예상(Class 5)") return "5";
        //    if (p_value == "이식수술(Class 6)") return "6";
        //    if (p_value == "기록없음") return "7";
        //    if (p_value == "응급") return "8";
        //    return "";
        //}

        //private string GetSOPR_BF_INFC_SICK_CD_NM(string p_value)
        //{
        //    if (p_value == "01") return "골막염";		
        //    if (p_value == "02") return "골수염";		
        //    if (p_value == "03") return "개방성 골절";		
        //    if (p_value == "04") return "개방성 상처";		
        //    if (p_value == "05") return "급성 담낭염";		
        //    if (p_value == "06") return "농양";		
        //    if (p_value == "07") return "농흉";		
        //    if (p_value == "08") return "뇌염";		
        //    if (p_value == "09") return "동맥염";		
        //    if (p_value == "10") return "림프관염";		
        //    if (p_value == "11") return "방광염";		
        //    if (p_value == "12") return "복막염";		
        //    if (p_value == "13") return "수막염";		
        //    if (p_value == "14") return "심근염";		
        //    if (p_value == "15") return "연골염";		
        //    if (p_value == "16") return "요도염";		
        //    if (p_value == "17") return "요로감염";		
        //    if (p_value == "18") return "조기양막파수";		
        //    if (p_value == "19") return "좁쌀결핵";		
        //    if (p_value == "20") return "천공";		
        //    if (p_value == "21") return "치밀골염";		
        //    if (p_value == "22") return "패혈증";		
        //    if (p_value == "23") return "폐렴";		
        //    if (p_value == "24") return "혈관염";
        //    if (p_value == "25") return "흉막염";
        //    return "";
        //}

        //private string GetSOPR_BF_INFC_SICK_CD(string p_value)
        //{
        //    if (p_value == "골막염") return "01";
        //    if (p_value == "골수염") return "02";
        //    if (p_value == "개방성 골절") return "03";
        //    if (p_value == "개방성 상처") return "04";
        //    if (p_value == "급성 담낭염") return "05";
        //    if (p_value == "농양") return "06";
        //    if (p_value == "농흉") return "07";
        //    if (p_value == "뇌염") return "08";
        //    if (p_value == "동맥염") return "09";
        //    if (p_value == "림프관염") return "10";
        //    if (p_value == "방광염") return "11";
        //    if (p_value == "복막염") return "12";
        //    if (p_value == "수막염") return "13";
        //    if (p_value == "심근염") return "14";
        //    if (p_value == "연골염") return "15";
        //    if (p_value == "요도염") return "16";
        //    if (p_value == "요로감염") return "17";
        //    if (p_value == "조기양막파수") return "18";
        //    if (p_value == "좁쌀결핵") return "19";
        //    if (p_value == "천공") return "20";
        //    if (p_value == "치밀골염") return "21";
        //    if (p_value == "패혈증") return "22";
        //    if (p_value == "폐렴") return "23";
        //    if (p_value == "혈관염") return "24";
        //    if (p_value == "흉막염") return "25";
        //    return "";
        //}

        //private string GetSOPR_BF_ANBO_DR_RCDC_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "경과기록지";		
        //    if (p_value == "2") return "입퇴원기록지";		
        //    if (p_value == "3") return "협진기록지";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetSOPR_BF_ANBO_DR_RCDC_CD(string p_value)
        //{
        //    if (p_value == "경과기록지") return "1";
        //    if (p_value == "입퇴원기록지") return "2";
        //    if (p_value == "협진기록지") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetSOPR_BF_REQR_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "감염상병 외 상병";		
        //    if (p_value == "2") return "38도 이상의 고열";		
        //    if (p_value == "3") return "감염내과 외 협진";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetSOPR_BF_REQR_RS_CD(string p_value)
        //{
        //    if (p_value == "감염상병 외 상병") return "1";
        //    if (p_value == "38도 이상의 고열") return "2";
        //    if (p_value == "감염내과 외 협진") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        private string GetSOPR_RGN_INFC_CD_NM(string p_value)
        {
            if (p_value == "1") return "절개부위 또는 심부에 위치한 드레인의 농성배액";						
            if (p_value == "2") return "절개부위, 심부 또는 기관에서 무균적으로 채취한 검체의 배양에서 균 분리";						
            if (p_value == "3") return "38℃ 이상의 발열, 국소동통, 압통, 발적 등 감염증상 중 하나 이상의 증상이 있고, 수술 창상의 심부가 저절로 파열되거나 외과의사가 개방한 경우";						
            if (p_value == "4") return "조직병리검사, 방사선검사 등에서 심부 절개부위, 기관 또는 강의 농양이나 감염 증거 관찰";
            if (p_value == "5") return "수술의, 주치의 또는 감염내과 의사에 의한 수술 부위 감염 진단";
            return "";			
        }

        private string GetSOPR_RGN_INFC_CD(string p_value)
        {
            if (p_value == "절개부위 또는 심부에 위치한 드레인의 농성배액") return "1";
            if (p_value == "절개부위, 심부 또는 기관에서 무균적으로 채취한 검체의 배양에서 균 분리") return "2";
            if (p_value == "38℃ 이상의 발열, 국소동통, 압통, 발적 등 감염증상 중 하나 이상의 증상이 있고, 수술 창상의 심부가 저절로 파열되거나 외과의사가 개방한 경우") return "3";
            if (p_value == "조직병리검사, 방사선검사 등에서 심부 절개부위, 기관 또는 강의 농양이나 감염 증거 관찰") return "4";
            if (p_value == "수술의, 주치의 또는 감염내과 의사에 의한 수술 부위 감염 진단") return "5";
            return "";
        }

        //private string GetSOPR_RGN_INFC_DR_RCDC_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "경과기록지";			
        //    if (p_value == "2") return "입퇴원기록지";			
        //    if (p_value == "3") return "협진기록지";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetSOPR_RGN_INFC_DR_RCDC_CD(string p_value)
        //{
        //    if (p_value == "경과기록지") return "1";
        //    if (p_value == "입퇴원기록지") return "2";
        //    if (p_value == "협진기록지") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetSOPR_AF_INFC_SICK_CD_NM(string p_value)
        //{
        //    if (p_value == "01") return "골막염";		
        //    if (p_value == "02") return "골수염";		
        //    if (p_value == "03") return "개방성 상처";		
        //    if (p_value == "04") return "급성 담낭염";		
        //    if (p_value == "05") return "농양";		
        //    if (p_value == "06") return "농흉";		
        //    if (p_value == "07") return "뇌염";		
        //    if (p_value == "08") return "동맥염";		
        //    if (p_value == "09") return "림프관염";		
        //    if (p_value == "10") return "방광염";		
        //    if (p_value == "11") return "복막염";		
        //    if (p_value == "12") return "수막염";		
        //    if (p_value == "13") return "심근염";		
        //    if (p_value == "14") return "연골염";		
        //    if (p_value == "15") return "요도염";		
        //    if (p_value == "16") return "요로감염";		
        //    if (p_value == "17") return "좁쌀결핵";		
        //    if (p_value == "18") return "천공";		
        //    if (p_value == "19") return "치밀골염";		
        //    if (p_value == "20") return "패혈증";		
        //    if (p_value == "21") return "폐렴";		
        //    if (p_value == "22") return "혈관염";		
        //    if (p_value == "23") return "흉막염";
        //    if (p_value == "24") return "비의도적 절단";
        //    return "";
        //}

        //private string GetSOPR_AF_INFC_SICK_CD(string p_value)
        //{
        //    if (p_value == "골막염") return "01";
        //    if (p_value == "골수염") return "02";
        //    if (p_value == "개방성 상처") return "03";
        //    if (p_value == "급성 담낭염") return "04";
        //    if (p_value == "농양") return "05";
        //    if (p_value == "농흉") return "06";
        //    if (p_value == "뇌염") return "07";
        //    if (p_value == "동맥염") return "08";
        //    if (p_value == "림프관염") return "09";
        //    if (p_value == "방광염") return "10";
        //    if (p_value == "복막염") return "11";
        //    if (p_value == "수막염") return "12";
        //    if (p_value == "심근염") return "13";
        //    if (p_value == "연골염") return "14";
        //    if (p_value == "요도염") return "15";
        //    if (p_value == "요로감염") return "16";
        //    if (p_value == "좁쌀결핵") return "17";
        //    if (p_value == "천공") return "18";
        //    if (p_value == "치밀골염") return "19";
        //    if (p_value == "패혈증") return "20";
        //    if (p_value == "폐렴") return "21";
        //    if (p_value == "혈관염") return "22";
        //    if (p_value == "흉막염") return "23";
        //    if (p_value == "비의도적 절단") return "24";
        //    return "";
        //}

        //private string GetSOPR_AF_ANBO_DR_RCDC_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "경과기록지";		
        //    if (p_value == "2") return "입퇴원기록지";		
        //    if (p_value == "3") return "협진기록지";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetSOPR_AF_ANBO_DR_RCDC_CD(string p_value)
        //{
        //    if (p_value == "경과기록지") return "1";
        //    if (p_value == "입퇴원기록지") return "2";
        //    if (p_value == "협진기록지") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        //private string GetSOPR_AF_REQR_RS_CD_NM(string p_value)
        //{
        //    if (p_value == "1") return "감염상병 외 상병";		
        //    if (p_value == "2") return "38도 이상의 고열";		
        //    if (p_value == "3") return "감염내과 외 협진";
        //    if (p_value == "9") return "기타";
        //    return "";
        //}

        //private string GetSOPR_AF_REQR_RS_CD(string p_value)
        //{
        //    if (p_value == "감염상병 외 상병") return "1";
        //    if (p_value == "38도 이상의 고열") return "2";
        //    if (p_value == "감염내과 외 협진") return "3";
        //    if (p_value == "기타") return "9";
        //    return "";
        //}

        private string GetANBO_INJC_PTH_CD_NM(string p_value)
        {
            if (p_value == "1") return "IV";
            if (p_value == "2") return "IM";
            if (p_value == "3") return "PO";
            return "";
        }

        private string GetANBO_INJC_PTH_CD(string p_value)
        {
            if (p_value == "IV") return "1";
            if (p_value == "IM") return "2";
            if (p_value == "PO") return "3";
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Save();
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

        private void Save()
        {
            // A. 기본정보
            m_data.ASM_IPAT_DT = CUtil.GetDateTime(txtASM_IPAT_DT_DATE.Text.ToString(), txtASM_IPAT_DT_TIME.Text.ToString()); // 입원일시(YYYYMMDDHHMM)
            m_data.DSCG_YN = CUtil.GetRBString(rbDSCG_YN_1, rbDSCG_YN_2); // 퇴원여부(1.Yes 2.No)
            m_data.ASM_DSCG_DT = CUtil.GetDateTime(txtASM_DSCG_DT_DATE.Text.ToString(), txtASM_DSCG_DT_TIME.Text.ToString()); // 퇴원일시(YYYYMMDDHHMM)


            // B.수술 및 감염 정보
            // 1. 수술 관련 환자 상태
            m_data.MDFEE_CD = txtMDFEE_CD.Text.ToString(); // 수가코드
            m_data.ASM_SOPR_STA_DT = CUtil.GetDateTime(txtASM_SOPR_STA_DT_DATE.Text.ToString(), txtASM_SOPR_STA_DT_TIME.Text.ToString()); // 수술 시작일시(YYYYMMDDHHMM)
            m_data.ASM_SOPR_END_DT = CUtil.GetDateTime(txtASM_SOPR_END_DT_DATE.Text.ToString(), txtASM_SOPR_END_DT_TIME.Text.ToString()); // 수술 종료일시(YYYYMMDDHHMM)
            m_data.EMY_CD = CUtil.GetRBString(rbEMY_CD_1, rbEMY_CD_2); // 응급여부(1.정규 2.응급)
            m_data.KNJN_RPMT = CUtil.GetRBString(rbKNJN_RPMT_1, rbKNJN_RPMT_2); // 슬관절치환술(1.Yes 2.No)
            m_data.HMRHG_CTRL_YN = CUtil.GetRBString(rbHMRHG_CTRL_YN_1, rbHMRHG_CTRL_YN_2); // 토니켓 적용 여부(1.Yes 2.No)
            m_data.HMRHG_CTRL_DT = CUtil.GetDateTime(txtHMRHG_CTRL_DT_DATE.Text.ToString(), txtHMRHG_CTRL_DT_TIME.Text.ToString()); // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            m_data.CAESR_YN = CUtil.GetRBString(rbCAESR_YN_1, rbCAESR_YN_2); // 제왕절개술 시행 여부(1.Yes 2.No)
            m_data.NBY_PARTU_DT = CUtil.GetDateTime(txtNBY_PARTU_DT_DATE.Text.ToString(), txtNBY_PARTU_DT_TIME.Text.ToString()); // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
            m_data.CRVD_YN = CUtil.GetRBString(rbCRVD_YN_1, rbCRVD_YN_2); // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            m_data.BSE_NCT_YN = CUtil.GetRBString(rbBSE_NCT_YN_1, rbBSE_NCT_YN_2); // 기본마취 여부(1.Yes 2.No)
            m_data.ASA_PNT = CUtil.GetComboboxSelectedValue(cboASA_PNT); // ASA 점수
            // 2.수술 전 항생제 투여
            m_data.SOPR_BF_ANBO_INJC_YN = CUtil.GetRBString(rbSOPR_BF_ANBO_INJC_YN_1, rbSOPR_BF_ANBO_INJC_YN_2); // 수술 전 항생제 투여 여부(1.Yes 2.No)
            m_data.SOPR_BF_INFC_SICK_YN = CUtil.GetRBString(rbSOPR_BF_INFC_SICK_YN_1, rbSOPR_BF_INFC_SICK_YN_2); // 감염상병 확진 여부(1.Yes 2.No)
            m_data.SOPR_BF_INFC_SICK_CD = CUtil.GetComboboxSelectedValue(cboSOPR_BF_INFC_SICK_CD); // 감염상병 확진명
            m_data.SOPR_BF_DDIAG_YN = CUtil.GetRBString(rbSOPR_BF_DDIAG_YN_1, rbSOPR_BF_DDIAG_YN_2); // 감염내과 협진여부(1.Yes 2.No)
            m_data.SOPR_BF_ASM_REQ_DT = CUtil.GetDateTime(txtSOPR_BF_ASM_REQ_DT_DATE.Text.ToString(), txtSOPR_BF_ASM_REQ_DT_TIME.Text.ToString()); // 의뢰일시(YYYYMMDDHHMM)
            m_data.SOPR_BF_RPY_YN = CUtil.GetRBString(rbSOPR_BF_RPY_YN_1, rbSOPR_BF_RPY_YN_2); // 회신 여부(1.Yes 2.No)
            m_data.SOPR_BF_ASM_RPY_DT = CUtil.GetDateTime(txtSOPR_BF_ASM_RPY_DT_DATE.Text.ToString(), txtSOPR_BF_ASM_RPY_DT_TIME.Text.ToString()); // 회신일시(YYYYMMDDHHMM)
            m_data.SOPR_BF_ANBO_DR_RCD_YN = CUtil.GetRBString(rbSOPR_BF_ANBO_DR_RCD_YN_1, rbSOPR_BF_ANBO_DR_RCD_YN_2); // 항생제 필요 의사기록 여부(1.Yes 2.No)
            m_data.SOPR_BF_ASM_RCD_DT = CUtil.GetDateTime(txtSOPR_BF_ASM_RCD_DT_DATE.Text.ToString(), txtSOPR_BF_ASM_RCD_DT_TIME.Text.ToString()); // 기록일시(YYYYMMDDHHMM)
            m_data.SOPR_BF_ANBO_DR_RCDC_CD = CUtil.GetComboboxSelectedValue(cboSOPR_BF_ANBO_DR_RCDC_CD); // 기록지 종류
            m_data.SOPR_BF_REQR_RS_CD = CUtil.GetComboboxSelectedValue(cboSOPR_BF_REQR_RS_CD); // 필요사유
            m_data.SOPR_BF_DR_RCD_TXT = txtSOPR_BF_DR_RCD_TXT.Text.ToString(); // 기록 상세 내용(평문)
            // 3.평가 제외 수술
            m_data.ASM_TGT_SOPR_SAME_ENFC_YN = CUtil.GetRBString(rbASM_TGT_SOPR_SAME_ENFC_YN_1, rbASM_TGT_SOPR_SAME_ENFC_YN_2); // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
            m_data.FQ2_GT_SOPR_ENFC_YN = CUtil.GetRBString(rbFQ2_GT_SOPR_ENFC_YN_1, rbFQ2_GT_SOPR_ENFC_YN_2); // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)
            // 4.수술 후 항셍제 투여
            m_data.SOPR_RGN_INFC_ANBO_INJC_YN = CUtil.GetRBString(rbSOPR_RGN_INFC_ANBO_INJC_YN_1, rbSOPR_RGN_INFC_ANBO_INJC_YN_2); // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
            m_data.SOPR_RGN_INFC_CD = CUtil.GetComboboxSelectedValue(cboSOPR_RGN_INFC_CD); // 수술부위 감영 유형
            m_data.ASM_RCD_DT = CUtil.GetDateTime(txtASM_RCD_DT_DATE.Text.ToString(), txtASM_RCD_DT_TIME.Text.ToString()); // 기록일시(YYYYMMDDHHMM)
            m_data.SOPR_RGN_INFC_DR_RCDC_CD = CUtil.GetComboboxSelectedValue(cboSOPR_RGN_INFC_DR_RCDC_CD); // 기록지 종류
            m_data.SOPR_RGN_INFC_DR_RCD_TXT = txtSOPR_RGN_INFC_DR_RCD_TXT.Text.ToString(); // 수술 부위 감염 사유 상세(평문)
            m_data.INFC_ANBO_INJC_YN = CUtil.GetRBString(rbINFC_ANBO_INJC_YN_1, rbINFC_ANBO_INJC_YN_2); // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
            m_data.CLTR_STRN_YN = CUtil.GetRBString(rbCLTR_STRN_YN_1, rbCLTR_STRN_YN_2); // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
            m_data.ASM_GAT_DT = CUtil.GetDateTime(txtASM_GAT_DT_DATE.Text.ToString(), txtASM_GAT_DT_TIME.Text.ToString()); // 채취일시(YYYYMMDDHHMM)
            m_data.INFC_SICK_DIAG = CUtil.GetRBString(rbINFC_SICK_DIAG_1, rbINFC_SICK_DIAG_2); // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
            m_data.SOPR_AF_INFC_SICK_CD = CUtil.GetComboboxSelectedValue(cboSOPR_AF_INFC_SICK_CD); // 감염 상병 화진명
            m_data.SOPR_AF_DDIAG_YN = CUtil.GetRBString(rbSOPR_AF_DDIAG_YN_1, rbSOPR_AF_DDIAG_YN_2); // 감염내과 협진후 항생제 투여 여부(1.Yes 2.No)
            m_data.SOPR_AF_ASM_REQ_DT = CUtil.GetDateTime(txtSOPR_AF_ASM_REQ_DT_DATE.Text.ToString(), txtSOPR_AF_ASM_REQ_DT_TIME.Text.ToString()); // 의뢰일시(YYYYMMDDHHMM)
            m_data.SOPR_AF_RPY_YN = CUtil.GetRBString(rbSOPR_AF_RPY_YN_1, rbSOPR_AF_RPY_YN_2); // 회신 여부(1.Yes 2.No)
            m_data.SOPR_AF_ASM_RPY_DT = CUtil.GetDateTime(txtSOPR_AF_ASM_RPY_DT_DATE.Text.ToString(), txtSOPR_AF_ASM_RPY_DT_TIME.Text.ToString()); // 회신일시(YYYYMMDDHHMM)
            m_data.SOPR_AF_ANBO_DR_RCD_YN = CUtil.GetRBString(rbSOPR_AF_ANBO_DR_RCD_YN_1, rbSOPR_AF_ANBO_DR_RCD_YN_2); // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
            m_data.SOPR_AF_ASM_RCD_DT = CUtil.GetDateTime(txtSOPR_AF_ASM_RCD_DT_DATE.Text.ToString(), txtSOPR_AF_ASM_RCD_DT_TIME.Text.ToString()); // 기록일시(YYYYMMDDHHMM)
            m_data.SOPR_AF_ANBO_DR_RCDC_CD = CUtil.GetComboboxSelectedValue(cboSOPR_AF_ANBO_DR_RCDC_CD); // 기록지 종류
            m_data.SOPR_AF_REQR_RS_CD = CUtil.GetComboboxSelectedValue(cboSOPR_AF_REQR_RS_CD); // 필요사유
            m_data.SOPR_AF_DR_RCD_TXT = txtSOPR_AF_DR_RCD_TXT.Text.ToString(); // 기록 상세 내용(평문)
            m_data.ANBO_ALRG_YN = CUtil.GetRBString(rbANBO_ALRG_YN_1, rbANBO_ALRG_YN_2); // 항생제 알러지 여부(1.Yes 2.No)
            m_data.WHBL_RBC_BLTS_YN = CUtil.GetRBString(rbWHBL_RBC_BLTS_YN_1, rbWHBL_RBC_BLTS_YN_2); // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)

            // 수혈
            m_data.BLTS_STA_DT.Clear(); // 수혈시작일시
            m_data.BLTS_END_DT.Clear(); // 수혈종료일시
            m_data.BLTS_MDFEE_CD.Clear(); // 수가코드
            m_data.BLTS_DGM_NM.Clear(); // 수혈제제명

            List<BLTS> listBLTS = (List<BLTS>)grdBLTS.DataSource;
            foreach(BLTS data in listBLTS)
            {
                m_data.BLTS_STA_DT.Add(CUtil.GetDateTime(data.BLTS_STA_DT_DATE, data.BLTS_STA_DT_TIME)); // 수혈시작일시
                m_data.BLTS_END_DT.Add(CUtil.GetDateTime(data.BLTS_END_DT_DATE, data.BLTS_END_DT_TIME)); // 수혈종료일시
                m_data.BLTS_MDFEE_CD.Add(data.BLTS_MDFEE_CD); // 수가코드
                m_data.BLTS_DGM_NM.Add(data.BLTS_DGM_NM); // 수혈제제명
            }


            // C.항생제 투여 여부
            m_data.ANBO_USE_YN = CUtil.GetRBString(rbANBO_USE_YN_1, rbANBO_USE_YN_2); // 항생제 투여 여부(1.Yes 2.No)

            // 항생제 투여
            m_data.INJC_STA_DT.Clear(); // 투여시작일시
            m_data.INJC_END_DT.Clear(); // 투여종료일시
            m_data.INJC_MDS_CD.Clear(); // 약품코드
            m_data.INJC_MDS_NM.Clear(); // 약품명
            m_data.ANBO_INJC_PTH_CD.Clear(); // 투여경로

            List<INJC> listINJC = (List<INJC>)grdINJC.DataSource;
            foreach (INJC data in listINJC)
            {
                m_data.INJC_STA_DT.Add(CUtil.GetDateTime(data.INJC_STA_DT_DATE, data.INJC_STA_DT_TIME)); // 투여시작일시
                m_data.INJC_END_DT.Add(CUtil.GetDateTime(data.INJC_END_DT_DATE, data.INJC_END_DT_TIME)); // 투여종료일시
                m_data.INJC_MDS_CD.Add(data.INJC_MDS_CD); // 약품코드
                m_data.INJC_MDS_NM.Add(data.INJC_MDS_NM); // 약품명
                m_data.ANBO_INJC_PTH_CD.Add(GetANBO_INJC_PTH_CD(data.ANBO_INJC_PTH_CD)); // 투여경로
            }

            // 퇴원 시 항생제 처방
            m_data.DSCG_ANBO_PRSC_YN = CUtil.GetRBString(rbDSCG_ANBO_PRSC_YN_1, rbDSCG_ANBO_PRSC_YN_2); // 퇴원시 항생제 처방 여부(1.Yes 2.No)

            m_data.PRSC_MDS_CD.Clear(); // 약품코드
            m_data.PRSC_MDS_NM.Clear(); // 약품명
            m_data.PRSC_TOT_INJC_DDCNT.Clear(); // 총 투약일수

            List<PRSC> listPRSC = (List<PRSC>)grdPRSC.DataSource;
            foreach (PRSC data in listPRSC)
            {
                m_data.PRSC_MDS_CD.Add(data.PRSC_MDS_CD); // 약품코드
                m_data.PRSC_MDS_NM.Add(data.PRSC_MDS_NM); // 약품명
                m_data.PRSC_TOT_INJC_DDCNT.Add(data.PRSC_TOT_INJC_DDCNT); // 총 투약일수
            }

            // D. 기타 사항
            //m_data.APND_DATA_NO = ""; // 첨부

            // 데이터 베이스에 저장한다.
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    string sysdt = MetroLib.Util.GetSysDate(conn, tran);
                    string systm = MetroLib.Util.GetSysTime(conn, tran);

                    m_data.UpdData(sysdt, systm, m_User, conn, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show(ex.Message);
                    //throw ex;
                }
            }

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_view.FocusedRowHandle == 0)
            {
                // 첫 줄은 아무런 동작도 하지 앟는다.
                MessageBox.Show("처음 자료입니다.");
            }
            else
            {
                m_view.FocusedRowHandle--;
                List<CDataASM010_002> list = (List<CDataASM010_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_view.FocusedRowHandle >= m_view.RowCount - 1)
            {
                // 마지막 줄은 아무런 동작도 하지 앟는다.
                MessageBox.Show("마지막 자료입니다.");
            }
            else
            {
                m_view.FocusedRowHandle++;
                List<CDataASM010_002> list = (List<CDataASM010_002>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowBLTS_Click(object sender, EventArgs e)
        {
            List<BLTS> list = (List<BLTS>)grdBLTS.DataSource;
            list.Add(new BLTS());
            RefreshGrid();
        }

        private void btnDelRowBLTS_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdBLTSView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<BLTS> list = (List<BLTS>)grdBLTS.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowINJC_Click(object sender, EventArgs e)
        {
            List<INJC> list = (List<INJC>)grdINJC.DataSource;
            list.Add(new INJC());
            RefreshGrid();
        }

        private void btnDelRowINJC_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdINJCView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<INJC> list = (List<INJC>)grdINJC.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowPRSC_Click(object sender, EventArgs e)
        {
            List<PRSC> list = (List<PRSC>)grdPRSC.DataSource;
            list.Add(new PRSC());
            RefreshGrid();
        }

        private void btnDelRowPRSC_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdPRSCView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<PRSC> list = (List<PRSC>)grdPRSC.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                SendTF(false);
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

        private void btnSendTmp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("임시 전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                SendTF(true);
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


        private void SendTF(bool isTmp)
        {

            this.Send(isTmp);
        }

        private void Send(bool isTmp)
        {
            CHiraEForm hira = new CHiraEForm();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                hira.Send(m_data, isTmp, sysdt, systm, m_User, m_Hosid, conn);

            }
            RefreshGrid();
        }

        private void btnReQuery_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("자료 다시 생성을 하면 저장한 자료등이 모두 초기화됩니다." + Environment.NewLine + Environment.NewLine + "계속하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.ReQuery();
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

        private void ReQuery()
        {
            var args = new RemakeRequestedEventArgs<CDataASM010_002>(m_data);

            // ADD7007E가 처리하도록 이벤트만 발생
            if (RemakeRequested != null)
            {
                RemakeRequested(this, args);

                if (args.Success)
                {
                    ShowData();
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show(args.FailureMessage);
                }
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

        private void btnExcept_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("전송제외하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Except();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Except()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sysdt = MetroLib.Util.GetSysDate(conn);
                string systm = MetroLib.Util.GetSysTime(conn);

                m_data.Except_ASM000(sysdt, systm, m_User, conn);
            }
            RefreshGrid();
        }

        private void rbDSCG_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbDSCG_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbKNJN_RPMT_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbKNJN_RPMT_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCAESR_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCAESR_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbBSE_NCT_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbBSE_NCT_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_ANBO_INJC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_ANBO_INJC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_INFC_SICK_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_INFC_SICK_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbHMRHG_CTRL_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbHMRHG_CTRL_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCRVD_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCRVD_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_DDIAG_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_DDIAG_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_RPY_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_RPY_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_ANBO_DR_RCD_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_BF_ANBO_DR_RCD_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_RGN_INFC_ANBO_INJC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_RGN_INFC_ANBO_INJC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbINFC_ANBO_INJC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbINFC_ANBO_INJC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCLTR_STRN_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbCLTR_STRN_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbINFC_SICK_DIAG_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbINFC_SICK_DIAG_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_DDIAG_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_DDIAG_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_RPY_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_RPY_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_ANBO_DR_RCD_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbSOPR_AF_ANBO_DR_RCD_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbWHBL_RBC_BLTS_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbWHBL_RBC_BLTS_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbANBO_USE_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbANBO_USE_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbDSCG_ANBO_PRSC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbDSCG_ANBO_PRSC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void SetEnabled()
        {
            if (rbDSCG_YN_1.Checked)
            {
                txtASM_DSCG_DT_DATE.Enabled = true;
                txtASM_DSCG_DT_TIME.Enabled = true;
            }
            else
            {
                txtASM_DSCG_DT_DATE.Enabled = false;
                txtASM_DSCG_DT_TIME.Enabled = false;
            }

            // 스관절 치환술
            if (rbKNJN_RPMT_1.Checked == false)
            {
                rbHMRHG_CTRL_YN_1.Enabled = false;
                rbHMRHG_CTRL_YN_2.Enabled = false;
                txtHMRHG_CTRL_DT_DATE.Enabled = false;
                txtHMRHG_CTRL_DT_TIME.Enabled = false;
            }
            else
            {
                rbHMRHG_CTRL_YN_1.Enabled = true;
                rbHMRHG_CTRL_YN_2.Enabled = true;

                if (rbHMRHG_CTRL_YN_1.Checked == false)
                {
                    txtHMRHG_CTRL_DT_DATE.Enabled = false;
                    txtHMRHG_CTRL_DT_TIME.Enabled = false;
                }
                else
                {
                    txtHMRHG_CTRL_DT_DATE.Enabled = true;
                    txtHMRHG_CTRL_DT_TIME.Enabled = true;
                }
            }

            if (rbCAESR_YN_1.Checked)
            {
                txtNBY_PARTU_DT_DATE.Enabled = true;
                txtNBY_PARTU_DT_TIME.Enabled = true;
                rbCRVD_YN_1.Enabled = true;
                rbCRVD_YN_2.Enabled = true;
            }
            else
            {
                txtNBY_PARTU_DT_DATE.Enabled = false;
                txtNBY_PARTU_DT_TIME.Enabled = false;
                rbCRVD_YN_1.Enabled = false;
                rbCRVD_YN_2.Enabled = false;
            }

            if (rbBSE_NCT_YN_1.Checked)
            {
                cboASA_PNT.Enabled = true;
            }
            else
            {
                cboASA_PNT.Enabled = false;
            }

            // 수술 전 항생제 투여
            if (rbSOPR_BF_ANBO_INJC_YN_1.Checked == false)
            {
                rbSOPR_BF_INFC_SICK_YN_1.Enabled = false;
                rbSOPR_BF_INFC_SICK_YN_2.Enabled = false;
                cboSOPR_BF_INFC_SICK_CD.Enabled = false;
                rbSOPR_BF_DDIAG_YN_1.Enabled = false;
                rbSOPR_BF_DDIAG_YN_2.Enabled = false;
                txtSOPR_BF_ASM_REQ_DT_DATE.Enabled = false;
                txtSOPR_BF_ASM_REQ_DT_TIME.Enabled = false;
                rbSOPR_BF_RPY_YN_1.Enabled = false;
                rbSOPR_BF_RPY_YN_2.Enabled = false;
                txtSOPR_BF_ASM_RPY_DT_DATE.Enabled = false;
                txtSOPR_BF_ASM_RPY_DT_TIME.Enabled = false;
                rbSOPR_BF_ANBO_DR_RCD_YN_1.Enabled = false;
                rbSOPR_BF_ANBO_DR_RCD_YN_2.Enabled = false;
                txtSOPR_BF_ASM_RCD_DT_DATE.Enabled = false;
                txtSOPR_BF_ASM_RCD_DT_TIME.Enabled = false;
                cboSOPR_BF_ANBO_DR_RCDC_CD.Enabled = false;
                cboSOPR_BF_REQR_RS_CD.Enabled = false;
                txtSOPR_BF_DR_RCD_TXT.Enabled = false;
            }
            else
            {
                rbSOPR_BF_INFC_SICK_YN_1.Enabled = true;
                rbSOPR_BF_INFC_SICK_YN_2.Enabled = true;
                rbSOPR_BF_DDIAG_YN_1.Enabled = true;
                rbSOPR_BF_DDIAG_YN_2.Enabled = true;
                rbSOPR_BF_ANBO_DR_RCD_YN_1.Enabled = true;
                rbSOPR_BF_ANBO_DR_RCD_YN_2.Enabled = true;

                if (rbSOPR_BF_INFC_SICK_YN_1.Checked == false)
                {
                    cboSOPR_BF_INFC_SICK_CD.Enabled = false;
                }
                else
                {
                    cboSOPR_BF_INFC_SICK_CD.Enabled = true;
                }

                if (rbSOPR_BF_DDIAG_YN_1.Checked == false)
                {
                    txtSOPR_BF_ASM_REQ_DT_DATE.Enabled = false;
                    txtSOPR_BF_ASM_REQ_DT_TIME.Enabled = false;
                    rbSOPR_BF_RPY_YN_1.Enabled = false;
                    rbSOPR_BF_RPY_YN_2.Enabled = false;
                    txtSOPR_BF_ASM_RPY_DT_DATE.Enabled = false;
                    txtSOPR_BF_ASM_RPY_DT_TIME.Enabled = false;
                }
                else
                {
                    txtSOPR_BF_ASM_REQ_DT_DATE.Enabled = true;
                    txtSOPR_BF_ASM_REQ_DT_TIME.Enabled = true;
                    rbSOPR_BF_RPY_YN_1.Enabled = true;
                    rbSOPR_BF_RPY_YN_2.Enabled = true;

                    if (rbSOPR_BF_RPY_YN_1.Checked == false)
                    {
                        txtSOPR_BF_ASM_RPY_DT_DATE.Enabled = false;
                        txtSOPR_BF_ASM_RPY_DT_TIME.Enabled = false;
                    }
                    else
                    {
                        txtSOPR_BF_ASM_RPY_DT_DATE.Enabled = true;
                        txtSOPR_BF_ASM_RPY_DT_TIME.Enabled = true;
                    }
                }

                if (rbSOPR_BF_ANBO_DR_RCD_YN_1.Checked == false)
                {
                    txtSOPR_BF_ASM_RCD_DT_DATE.Enabled = false;
                    txtSOPR_BF_ASM_RCD_DT_TIME.Enabled = false;
                    cboSOPR_BF_ANBO_DR_RCDC_CD.Enabled = false;
                    cboSOPR_BF_REQR_RS_CD.Enabled = false;
                    txtSOPR_BF_DR_RCD_TXT.Enabled = false;
                }
                else
                {
                    txtSOPR_BF_ASM_RCD_DT_DATE.Enabled = true;
                    txtSOPR_BF_ASM_RCD_DT_TIME.Enabled = true;
                    cboSOPR_BF_ANBO_DR_RCDC_CD.Enabled = true;
                    cboSOPR_BF_REQR_RS_CD.Enabled = true;
                    txtSOPR_BF_DR_RCD_TXT.Enabled = true;
                }
            }


            // 수술부위 감염으로 항생제 투여
            if (rbSOPR_RGN_INFC_ANBO_INJC_YN_1.Checked == false)
            {
                cboSOPR_RGN_INFC_CD.Enabled = false;
                txtASM_RCD_DT_DATE.Enabled = false;
                txtASM_RCD_DT_TIME.Enabled = false;
                cboSOPR_RGN_INFC_DR_RCDC_CD.Enabled = false;
                txtSOPR_RGN_INFC_DR_RCD_TXT.Enabled = false;
            }
            else
            {
                cboSOPR_RGN_INFC_CD.Enabled = true;
                if (CUtil.GetComboboxSelectedValue(cboSOPR_RGN_INFC_CD) == "5")
                {
                    txtASM_RCD_DT_DATE.Enabled = true;
                    txtASM_RCD_DT_TIME.Enabled = true;
                    cboSOPR_RGN_INFC_DR_RCDC_CD.Enabled = true;
                    txtSOPR_RGN_INFC_DR_RCD_TXT.Enabled = true;
                }
                else
                {
                    txtASM_RCD_DT_DATE.Enabled = false;
                    txtASM_RCD_DT_TIME.Enabled = false;
                    cboSOPR_RGN_INFC_DR_RCDC_CD.Enabled = false;
                    txtSOPR_RGN_INFC_DR_RCD_TXT.Enabled = false;
                }
            }

            rbINFC_ANBO_INJC_YN_1.Enabled = true;
            rbINFC_ANBO_INJC_YN_2.Enabled = true;

            // 수술부위 외 감염 등으로 항생제 투여
            if (rbINFC_ANBO_INJC_YN_1.Checked == false)
            {
                rbCLTR_STRN_YN_1.Enabled = false;
                rbCLTR_STRN_YN_2.Enabled = false;
                txtASM_GAT_DT_DATE.Enabled = false;
                txtASM_GAT_DT_TIME.Enabled = false;
                rbINFC_SICK_DIAG_1.Enabled = false;
                rbINFC_SICK_DIAG_2.Enabled = false;
                cboSOPR_AF_INFC_SICK_CD.Enabled = false;
                rbSOPR_AF_DDIAG_YN_1.Enabled = false;
                rbSOPR_AF_DDIAG_YN_2.Enabled = false;
                txtSOPR_AF_ASM_REQ_DT_DATE.Enabled = false;
                txtSOPR_AF_ASM_REQ_DT_TIME.Enabled = false;
                rbSOPR_AF_RPY_YN_1.Enabled = false;
                rbSOPR_AF_RPY_YN_2.Enabled = false;
                txtSOPR_AF_ASM_RPY_DT_DATE.Enabled = false;
                txtSOPR_AF_ASM_RPY_DT_TIME.Enabled = false;
                rbSOPR_AF_ANBO_DR_RCD_YN_1.Enabled = false;
                rbSOPR_AF_ANBO_DR_RCD_YN_2.Enabled = false;
                txtSOPR_AF_ASM_RCD_DT_DATE.Enabled = false;
                txtSOPR_AF_ASM_RCD_DT_TIME.Enabled = false;
                cboSOPR_AF_ANBO_DR_RCDC_CD.Enabled = false;
                cboSOPR_AF_REQR_RS_CD.Enabled = false;
                txtSOPR_AF_DR_RCD_TXT.Enabled = false;
            }
            else
            {
                rbCLTR_STRN_YN_1.Enabled = true;
                rbCLTR_STRN_YN_2.Enabled = true;
                rbINFC_SICK_DIAG_1.Enabled = true;
                rbINFC_SICK_DIAG_2.Enabled = true;
                rbSOPR_AF_DDIAG_YN_1.Enabled = true;
                rbSOPR_AF_DDIAG_YN_2.Enabled = true;
                rbSOPR_AF_ANBO_DR_RCD_YN_1.Enabled = true;
                rbSOPR_AF_ANBO_DR_RCD_YN_2.Enabled = true;

                // 혈액, 뇌척수액 배양에서 균 분리 여부
                if (rbCLTR_STRN_YN_1.Checked == false)
                {
                    txtASM_GAT_DT_DATE.Enabled = false;
                    txtASM_GAT_DT_TIME.Enabled = false;
                }
                else
                {
                    txtASM_GAT_DT_DATE.Enabled = true;
                    txtASM_GAT_DT_TIME.Enabled = true;
                }

                // 감염상병 확진
                if (rbINFC_SICK_DIAG_1.Checked == false)
                {
                    cboSOPR_AF_INFC_SICK_CD.Enabled = false;
                }
                else
                {
                    cboSOPR_AF_INFC_SICK_CD.Enabled = true;
                }

                // 감염내과 협진
                if (rbSOPR_AF_DDIAG_YN_1.Checked == false)
                {
                    txtSOPR_AF_ASM_REQ_DT_DATE.Enabled = false;
                    txtSOPR_AF_ASM_REQ_DT_TIME.Enabled = false;
                    rbSOPR_AF_RPY_YN_1.Enabled = false;
                    rbSOPR_AF_RPY_YN_2.Enabled = false;
                    txtSOPR_AF_ASM_RPY_DT_DATE.Enabled = false;
                    txtSOPR_AF_ASM_RPY_DT_TIME.Enabled = false;
                }
                else
                {
                    txtSOPR_AF_ASM_REQ_DT_DATE.Enabled = true;
                    txtSOPR_AF_ASM_REQ_DT_TIME.Enabled = true;
                    rbSOPR_AF_RPY_YN_1.Enabled = true;
                    rbSOPR_AF_RPY_YN_2.Enabled = true;

                    if (rbSOPR_AF_RPY_YN_1.Checked == false)
                    {
                        txtSOPR_AF_ASM_RPY_DT_DATE.Enabled = false;
                        txtSOPR_AF_ASM_RPY_DT_TIME.Enabled = false;
                    }
                    else
                    {
                        txtSOPR_AF_ASM_RPY_DT_DATE.Enabled = true;
                        txtSOPR_AF_ASM_RPY_DT_TIME.Enabled = true;
                    }
                }

                // 항생제 필요 의사기록 관련
                if (rbSOPR_AF_ANBO_DR_RCD_YN_1.Checked == false)
                {
                    txtSOPR_AF_ASM_RCD_DT_DATE.Enabled = false;
                    txtSOPR_AF_ASM_RCD_DT_TIME.Enabled = false;
                    cboSOPR_AF_ANBO_DR_RCDC_CD.Enabled = false;
                    cboSOPR_AF_REQR_RS_CD.Enabled = false;
                    txtSOPR_AF_DR_RCD_TXT.Enabled = false;
                }
                else
                {
                    txtSOPR_AF_ASM_RCD_DT_DATE.Enabled = true;
                    txtSOPR_AF_ASM_RCD_DT_TIME.Enabled = true;
                    cboSOPR_AF_ANBO_DR_RCDC_CD.Enabled = true;
                    cboSOPR_AF_REQR_RS_CD.Enabled = true;
                    txtSOPR_AF_DR_RCD_TXT.Enabled = true;
                }
            }

            rbANBO_ALRG_YN_1.Enabled = true;
            rbANBO_ALRG_YN_2.Enabled = true;
            //
            rbWHBL_RBC_BLTS_YN_1.Enabled = true;
            rbWHBL_RBC_BLTS_YN_2.Enabled = true;

            if (rbWHBL_RBC_BLTS_YN_1.Checked == false)
            {
                btnInsRowBLTS.Enabled = false;
                btnDelRowBLTS.Enabled = false;
                grdBLTS.Enabled = false;
            }
            else
            {
                btnInsRowBLTS.Enabled = true;
                btnDelRowBLTS.Enabled = true;
                grdBLTS.Enabled = true;
            }

            rbANBO_USE_YN_1.Enabled = true;
            rbANBO_USE_YN_2.Enabled = true;

            if (rbANBO_USE_YN_1.Checked == false)
            {
                btnInsRowINJC.Enabled = false;
                btnDelRowINJC.Enabled = false;
                grdINJC.Enabled = false;
            }
            else
            {
                btnInsRowINJC.Enabled = true;
                btnDelRowINJC.Enabled = true;
                grdINJC.Enabled = true;
            }

            rbDSCG_ANBO_PRSC_YN_1.Enabled = true;
            rbDSCG_ANBO_PRSC_YN_2.Enabled = true;

            if (rbDSCG_ANBO_PRSC_YN_1.Checked == false)
            {
                btnInsRowPRSC.Enabled = false;
                btnDelRowPRSC.Enabled = false;
                grdPRSC.Enabled = false;
            }
            else
            {
                btnInsRowPRSC.Enabled = true;
                btnDelRowPRSC.Enabled = true;
                grdPRSC.Enabled = true;
            }

        }

        private void cboSOPR_RGN_INFC_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbFQ2_GT_SOPR_ENFC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbFQ2_GT_SOPR_ENFC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbASM_TGT_SOPR_SAME_ENFC_YN_1_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbASM_TGT_SOPR_SAME_ENFC_YN_2_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private string GetPcodenm(string p_code, string p_exdt)
        {
            if (p_code == "") return "";

            string name = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                List<object> para = new List<object>();

                string sql = "";
                sql += Environment.NewLine + "SELECT I09.PCODENM";
                sql += Environment.NewLine + "  FROM TI09 I09";
                sql += Environment.NewLine + " WHERE I09.PCODE = ? ";
                sql += Environment.NewLine + "   AND I09.GUBUN = '3'";
                sql += Environment.NewLine + "   AND I09.ADTDT = (SELECT MAX(X.ADTDT)";
                sql += Environment.NewLine + "                      FROM TI09 X";
                sql += Environment.NewLine + "                     WHERE X.PCODE = I09.PCODE";
                sql += Environment.NewLine + "                       AND X.GUBUN = I09.GUBUN";
                sql += Environment.NewLine + "                       AND X.ADTDT <= ?";
                sql += Environment.NewLine + "                   )";

                para.Clear();
                para.Add(p_code);
                para.Add(p_exdt);

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    System.Windows.Forms.Application.DoEvents();

                    name = reader["PCODENM"].ToString();
                    return MetroLib.SqlHelper.BREAK;
                });
            }

            return name;
        }

        private void grdINJCView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                // 약품코드를 입력하면 약품명을 가져온다.
                if (e.Column.FieldName != "INJC_MDS_CD") return;

                DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                string code = Convert.ToString(e.Value);
                code = (code ?? "").Trim();

                if (code == "")
                {
                    view.SetRowCellValue(e.RowHandle, "INJC_MDS_NM", "");
                }
                else
                {
                    // 투여시작일
                    string exdt = Convert.ToString(view.GetRowCellValue(e.RowHandle, "INJC_STA_DT_DATE"));
                    exdt = (exdt ?? "").Trim();
                    // 없으면 투여종료일
                    if (exdt == "")
                    {
                        exdt = Convert.ToString(view.GetRowCellValue(e.RowHandle, "INJC_END_DT_DATE"));
                        exdt = (exdt ?? "").Trim();
                    }
                    // 없으면 퇴원일
                    if (exdt == "")
                    {
                        exdt = txtASM_DSCG_DT_DATE.Text.ToString();
                        exdt = (exdt ?? "").Trim();
                    }
                    // 없으면 입원일
                    if (exdt == "")
                    {
                        exdt = txtASM_IPAT_DT_DATE.Text.ToString();
                        exdt = (exdt ?? "").Trim();
                    }
                    // 없으면 명칭을 공백으로
                    if (exdt == "")
                    {
                        view.SetRowCellValue(e.RowHandle, "INJC_MDS_NM", "");
                    }
                    else
                    {
                        string name = GetPcodenm(code, exdt);
                        view.SetRowCellValue(e.RowHandle, "INJC_MDS_NM", name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdPRSCView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                // 약품코드를 입력하면 약품명을 가져온다.
                if (e.Column.FieldName != "PRSC_MDS_CD") return;

                DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                string code = Convert.ToString(e.Value);
                code = (code ?? "").Trim();

                if (code == "")
                {
                    view.SetRowCellValue(e.RowHandle, "PRSC_MDS_NM", "");
                }
                else
                {
                    // 퇴원일
                    string exdt = txtASM_DSCG_DT_DATE.Text.ToString();
                    exdt = (exdt ?? "").Trim();
                    // 없으면 입원일
                    if (exdt == "")
                    {
                        exdt = txtASM_IPAT_DT_DATE.Text.ToString();
                        exdt = (exdt ?? "").Trim();
                    }
                    // 없으면 명칭을 공백으로
                    if (exdt == "")
                    {
                        view.SetRowCellValue(e.RowHandle, "PRSC_MDS_NM", "");
                    }
                    else
                    {
                        string name = GetPcodenm(code, exdt);
                        view.SetRowCellValue(e.RowHandle, "PRSC_MDS_NM", name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

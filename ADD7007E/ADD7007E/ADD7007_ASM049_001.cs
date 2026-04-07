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
    public partial class ADD7007_ASM049_001 : Form
    {
        class CT
        {
            public string MDFEE_CD { get; set; } // 수가코드
            public string MDFEE_CD_NM { get; set; } // 수가명
            public string EXM_EXEC_DT_DATE { get; set; } // 검사일자
            public string EXM_EXEC_DT_TIME { get; set; } // 검사시간
            public string RD_SDR_DCT_YN { get; set; } // 영상의학과 전문의 판독여부
            public string DCT_RST_DD { get; set; } // 판독일

            public CT()
            {
                MDFEE_CD = ""; // 수가코드
                MDFEE_CD_NM = ""; // 수가명
                EXM_EXEC_DT_DATE = ""; // 검사일자
                EXM_EXEC_DT_TIME = ""; // 검사시간
                RD_SDR_DCT_YN = ""; // 영상의학과 전문의 판독여부
                DCT_RST_DD = ""; // 판독일
            }
        }

        class MRI
        {
            public string MDFEE_CD { get; set; } // 수가코드
            public string MDFEE_CD_NM { get; set; } // 수가명
            public string EXM_EXEC_DT_DATE { get; set; } // 검사일자
            public string EXM_EXEC_DT_TIME { get; set; } // 검사시간
            public string RD_SDR_DCT_YN { get; set; } // 영상의학과 전문의 판독여부
            public string DCT_RST_DD { get; set; } // 판독일

            public MRI()
            {
                MDFEE_CD = ""; // 수가코드
                MDFEE_CD_NM = ""; // 수가명
                EXM_EXEC_DT_DATE = ""; // 검사일자
                EXM_EXEC_DT_TIME = ""; // 검사시간
                RD_SDR_DCT_YN = ""; // 영상의학과 전문의 판독여부
                DCT_RST_DD = ""; // 판독일
            }
        }

        class PET
        {
            public string MDFEE_CD { get; set; } // 수가코드
            public string MDFEE_CD_NM { get; set; } // 수가명
            public string EXM_EXEC_DT_DATE { get; set; } // 검사일자
            public string EXM_EXEC_DT_TIME { get; set; } // 검사시간

            public PET()
            {
                MDFEE_CD = ""; // 수가코드
                MDFEE_CD_NM = ""; // 수가명
                EXM_EXEC_DT_DATE = ""; // 검사일자
                EXM_EXEC_DT_TIME = ""; // 검사시간
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM049_001 m_data;

        public ADD7007_ASM049_001()
        {
            InitializeComponent();
            MakeComboGrid();
        }

        public ADD7007_ASM049_001(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM049_001> list = (List<CDataASM049_001>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void MakeComboGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            // 영상의학과 전문의 판독 여부
            CUtil.SetGridCombo(grdCTView.Columns["RD_SDR_DCT_YN"],
                "",
                "Yes",
                "No"
                );

            // 영상의학과 전문의 판독 여부
            CUtil.SetGridCombo(grdMRIView.Columns["RD_SDR_DCT_YN"],
                "",
                "Yes",
                "No"
                );
        }

        private void ShowData()
        {
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // --- A. 환자 정보 확인 ---
            // 청구유형
            rbIPAT_OPAT_TP_CD_1.Checked = m_data.IPAT_OPAT_TP_CD == "1"; // 입원
            rbIPAT_OPAT_TP_CD_2.Checked = m_data.IPAT_OPAT_TP_CD == "2"; // 외래
            txtIPAT_DD.Text = m_data.IPAT_DD; // 입원일(YYYYMMDD)
            rbEMRRM_PASS_YN_1.Checked = m_data.EMRRM_PASS_YN == "1"; // 응급실을 통한 입원 Yes
            rbEMRRM_PASS_YN_2.Checked = m_data.EMRRM_PASS_YN == "2"; // 응급실을 통한 입원 No
            txtEMRRM_VST_DD.Text = m_data.EMRRM_VST_DD; // 응급실 내원일(YYYYMMDD)
            txtDSCG_DD.Text = m_data.DSCG_DD; // 퇴원일(YYYYMMDD)
            txtDIAG_DD.Text = m_data.DIAG_DD; // 내원일(YYYYMMDD)

            // --- B. 검사정보 ---
            // 검사종류(체크박스)
            var imgCodes = m_data.IMG_EXM_KND_CD.Split('/');
            chkIMG_EXM_KND_CD_1.Checked = imgCodes.Contains("1"); // CT
            chkIMG_EXM_KND_CD_2.Checked = imgCodes.Contains("2"); // MRI
            chkIMG_EXM_KND_CD_3.Checked = imgCodes.Contains("3"); // PET

            // PET 종류(라디오버튼)
            rbPET_KND_CD_1.Checked = m_data.PET_KND_CD == "1";
            rbPET_KND_CD_2.Checked = m_data.PET_KND_CD == "2";
            rbPET_KND_CD_3.Checked = m_data.PET_KND_CD == "3";

            // --- C. CT 검사 ---
            // 진료유형(라디오버튼)
            rbCT_DIAG_TY_CD_1.Checked = m_data.CT_DIAG_TY_CD == "1";
            rbCT_DIAG_TY_CD_2.Checked = m_data.CT_DIAG_TY_CD == "2";
            rbCT_DIAG_TY_CD_3.Checked = m_data.CT_DIAG_TY_CD == "3";
            // 촬영목적(콤보박스)
            rbCT_PGP_INTNT_TP_CD_1.Checked = m_data.CT_PGP_INTNT_TP_CD == "1";
            rbCT_PGP_INTNT_TP_CD_2.Checked = m_data.CT_PGP_INTNT_TP_CD == "2";

            // 검사정보(그리드)
            grdCT.DataSource = null;
            var listCT = new List<CT>();
            grdCT.DataSource = listCT;
            for (int i = 0; i < m_data.CT_MDFEE_CD.Count; i++)
            {
                var data = new CT();
                data.MDFEE_CD = m_data.CT_MDFEE_CD[i];
                data.MDFEE_CD_NM = m_data.CT_MDFEE_CD_NM[i];
                data.EXM_EXEC_DT_DATE = CUtil.GetDate(m_data.CT_EXM_EXEC_DT[i]);
                data.EXM_EXEC_DT_TIME = CUtil.GetTime(m_data.CT_EXM_EXEC_DT[i]);
                data.RD_SDR_DCT_YN = GetRD_SDR_DCT_YN_NM(m_data.CT_RD_SDR_DCT_YN[i]);
                data.DCT_RST_DD = m_data.CT_DCT_RST_DD[i];
                listCT.Add(data);
            }

            // 조영제 사용(라디오버튼)
            rbCT_CTRST_USE_YN_1.Checked = m_data.CT_CTRST_USE_YN == "1";
            rbCT_CTRST_USE_YN_2.Checked = m_data.CT_CTRST_USE_YN == "2";
            // 조영제 투약 경로(라디오버튼)
            rbCT_CTRST_MDCT_PTH_CD_1.Checked = m_data.CT_CTRST_MDCT_PTH_CD == "1";
            rbCT_CTRST_MDCT_PTH_CD_2.Checked = m_data.CT_CTRST_MDCT_PTH_CD == "2";
            // 환자 평가 기록 유무(라디오버튼)
            rbCT_CTRST_PTNT_ASM_RCD_YN_1.Checked = m_data.CT_CTRST_PTNT_ASM_RCD_YN == "1";
            rbCT_CTRST_PTNT_ASM_RCD_YN_2.Checked = m_data.CT_CTRST_PTNT_ASM_RCD_YN == "2";
            txtCT_CTRST_PTNT_ASM_RCD_DD.Text = m_data.CT_CTRST_PTNT_ASM_RCD_DD;
            // 신장기능검사 유무(라디오버튼)
            rbCT_KDNY_FCLT_EXM_YN_1.Checked = m_data.CT_KDNY_FCLT_EXM_YN == "1";
            rbCT_KDNY_FCLT_EXM_YN_2.Checked = m_data.CT_KDNY_FCLT_EXM_YN == "2";

            // --- D. MRI 검사 ---
            // 진료유형(라디오버튼)
            rbMRI_DIAG_TY_CD_1.Checked = m_data.MRI_DIAG_TY_CD == "1";
            rbMRI_DIAG_TY_CD_2.Checked = m_data.MRI_DIAG_TY_CD == "2";
            rbMRI_DIAG_TY_CD_3.Checked = m_data.MRI_DIAG_TY_CD == "3";
            // 검사정보(그리드)
            grdMRI.DataSource = null;
            var listMRI = new List<MRI>();
            grdMRI.DataSource = listMRI;
            for (int i = 0; i < m_data.MRI_MDFEE_CD.Count; i++)
            {
                var data = new MRI();
                data.MDFEE_CD = m_data.MRI_MDFEE_CD[i];
                data.MDFEE_CD_NM = m_data.MRI_MDFEE_CD_NM[i];
                data.EXM_EXEC_DT_DATE = CUtil.GetDate(m_data.MRI_EXM_EXEC_DT[i]);
                data.EXM_EXEC_DT_TIME = CUtil.GetTime(m_data.MRI_EXM_EXEC_DT[i]);
                data.RD_SDR_DCT_YN = GetRD_SDR_DCT_YN_NM(m_data.MRI_RD_SDR_DCT_YN[i]);
                data.DCT_RST_DD = m_data.MRI_DCT_RST_DD[i];
                listMRI.Add(data);
            }

            // 조영제 사용(라디오버튼)
            rbMRI_CTRST_USE_YN_1.Checked = m_data.MRI_CTRST_USE_YN == "1";
            rbMRI_CTRST_USE_YN_2.Checked = m_data.MRI_CTRST_USE_YN == "2";
            // 환자 평가 기록 유무(라디오버튼)
            rbMRI_CTRST_PTNT_ASM_RCD_YN_1.Checked = m_data.MRI_CTRST_PTNT_ASM_RCD_YN == "1";
            rbMRI_CTRST_PTNT_ASM_RCD_YN_2.Checked = m_data.MRI_CTRST_PTNT_ASM_RCD_YN == "2";
            txtMRI_CTRST_PTNT_ASM_RCD_DD.Text = m_data.MRI_CTRST_PTNT_ASM_RCD_DD;
            // 신장기능검사 유무(라디오버튼)
            rbMRI_KDNY_FCLT_EXM_YN_1.Checked = m_data.MRI_KDNY_FCLT_EXM_YN == "1";
            rbMRI_KDNY_FCLT_EXM_YN_2.Checked = m_data.MRI_KDNY_FCLT_EXM_YN == "2";
            // MRI 전 환자 평가 기록 유무(라디오버튼)
            rbBF_MRI_PTNT_ASM_RCD_YN_1.Checked = m_data.BF_MRI_PTNT_ASM_RCD_YN == "1";
            rbBF_MRI_PTNT_ASM_RCD_YN_2.Checked = m_data.BF_MRI_PTNT_ASM_RCD_YN == "2";
            txtBF_MRI_PTNT_ASM_RCD_DD.Text = m_data.BF_MRI_PTNT_ASM_RCD_DD;

            // --- E. PET 검사 ---
            // 검사정보(그리드)
            grdPET.DataSource = null;
            var listPET = new List<PET>();
            grdPET.DataSource = listPET;
            for (int i = 0; i < m_data.PET_MDFEE_CD.Count; i++)
            {
                var data = new PET();
                data.MDFEE_CD = m_data.PET_MDFEE_CD[i];
                data.MDFEE_CD_NM = m_data.PET_MDFEE_CD_NM[i];
                data.EXM_EXEC_DT_DATE = CUtil.GetDate(m_data.PET_EXM_EXEC_DT[i]);
                data.EXM_EXEC_DT_TIME = CUtil.GetTime(m_data.PET_EXM_EXEC_DT[i]);
                listPET.Add(data);
            }

            // F-18 FDG 투여량 기록 유무(라디오버튼)
            rbFDG_INJC_QTY_RCD_YN_1.Checked = m_data.FDG_INJC_QTY_RCD_YN == "1";
            rbFDG_INJC_QTY_RCD_YN_2.Checked = m_data.FDG_INJC_QTY_RCD_YN == "2";
            txtFDG_TOT_INJC_QTY.Text = m_data.FDG_TOT_INJC_QTY;
            rbFDG_UNIT_1.Checked = m_data.FDG_UNIT == "1";
            rbFDG_UNIT_2.Checked = m_data.FDG_UNIT == "2";
            txtHEIG.Text = m_data.HEIG;
            txtBWGT.Text = m_data.BWGT;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdCT, grdCTView);
            CUtil.RefreshGrid(grdMRI, grdMRIView);
            CUtil.RefreshGrid(grdPET, grdPETView);
        }

        private string GetRD_SDR_DCT_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetRD_SDR_DCT_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- A. 환자 정보 확인 ---
            m_data.IPAT_OPAT_TP_CD = CUtil.GetRBString(rbIPAT_OPAT_TP_CD_1, rbIPAT_OPAT_TP_CD_2); // 청구유형(1:입원, 2:외래)
            m_data.IPAT_DD = txtIPAT_DD.Text.Trim(); // 입원일(YYYYMMDD)
            m_data.EMRRM_PASS_YN = CUtil.GetRBString(rbEMRRM_PASS_YN_1, rbEMRRM_PASS_YN_2); // 응급실을 통한 입원 여부(1:Yes, 2:No)
            m_data.EMRRM_VST_DD = txtEMRRM_VST_DD.Text.Trim(); // 응급실 내원일(YYYYMMDD)
            m_data.DSCG_DD = txtDSCG_DD.Text.Trim(); // 퇴원일(YYYYMMDD)
            m_data.DIAG_DD = txtDIAG_DD.Text.Trim(); // 내원일(YYYYMMDD)

            // --- B. 검사정보 ---
            List<string> exmKindList = new List<string>();
            if (chkIMG_EXM_KND_CD_1.Checked) exmKindList.Add("1");
            if (chkIMG_EXM_KND_CD_2.Checked) exmKindList.Add("2");
            if (chkIMG_EXM_KND_CD_3.Checked) exmKindList.Add("3");
            m_data.IMG_EXM_KND_CD = string.Join("/", exmKindList.ToArray()); // 검사종류(1:CT, 2:MRI, 3:PET, 다중선택시 "/"로 구분)
            m_data.PET_KND_CD = CUtil.GetRBString(rbPET_KND_CD_1, rbPET_KND_CD_2, rbPET_KND_CD_3);

            // --- C. CT ---
            m_data.CT_DIAG_TY_CD = CUtil.GetRBString(rbCT_DIAG_TY_CD_1, rbCT_DIAG_TY_CD_2, rbCT_DIAG_TY_CD_3);
            m_data.CT_PGP_INTNT_TP_CD = CUtil.GetRBString(rbCT_PGP_INTNT_TP_CD_1, rbCT_PGP_INTNT_TP_CD_2); // CT 촬영목적

            // CT 검사정보(그리드)
            m_data.CT_MDFEE_CD.Clear();
            m_data.CT_MDFEE_CD_NM.Clear();
            m_data.CT_EXM_EXEC_DT.Clear();
            m_data.CT_RD_SDR_DCT_YN.Clear();
            m_data.CT_DCT_RST_DD.Clear();
            var listCT = grdCT.DataSource as List<CT>;
            foreach (var data in listCT)
            {
                m_data.CT_MDFEE_CD.Add(data.MDFEE_CD); // 수가코드
                m_data.CT_MDFEE_CD_NM.Add(data.MDFEE_CD_NM); // 검사명
                m_data.CT_EXM_EXEC_DT.Add(CUtil.GetDateTime(data.EXM_EXEC_DT_DATE, data.EXM_EXEC_DT_TIME)); // 검사일시(YYYYMMDDHHMM)
                m_data.CT_RD_SDR_DCT_YN.Add(GetRD_SDR_DCT_YN(data.RD_SDR_DCT_YN)); // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                m_data.CT_DCT_RST_DD.Add(data.DCT_RST_DD); // 판독 완료일(YYYYMMDD)
            }

            m_data.CT_CTRST_USE_YN = CUtil.GetRBString(rbCT_CTRST_USE_YN_1, rbCT_CTRST_USE_YN_2); // 조영제 사용
            m_data.CT_CTRST_MDCT_PTH_CD = CUtil.GetRBString(rbCT_CTRST_MDCT_PTH_CD_1, rbCT_CTRST_MDCT_PTH_CD_2); // 조영제 투약 경로
            m_data.CT_CTRST_PTNT_ASM_RCD_YN = CUtil.GetRBString(rbCT_CTRST_PTNT_ASM_RCD_YN_1, rbCT_CTRST_PTNT_ASM_RCD_YN_2); // 환자 평가 기록 유무
            m_data.CT_CTRST_PTNT_ASM_RCD_DD = txtCT_CTRST_PTNT_ASM_RCD_DD.Text.Trim(); // 환자 평가 기록 일자
            m_data.CT_KDNY_FCLT_EXM_YN = CUtil.GetRBString(rbCT_KDNY_FCLT_EXM_YN_1, rbCT_KDNY_FCLT_EXM_YN_2); // 신장기능검사 유무

            // --- D. MRI ---
            m_data.MRI_DIAG_TY_CD = CUtil.GetRBString(rbMRI_DIAG_TY_CD_1, rbMRI_DIAG_TY_CD_2, rbMRI_DIAG_TY_CD_3);
            // MRI 검사정보(그리드)
            m_data.MRI_MDFEE_CD.Clear();
            m_data.MRI_MDFEE_CD_NM.Clear();
            m_data.MRI_EXM_EXEC_DT.Clear();
            m_data.MRI_RD_SDR_DCT_YN.Clear();
            m_data.MRI_DCT_RST_DD.Clear();
            var listMRI = grdMRI.DataSource as List<MRI>;
            foreach (var data in listMRI)
            {
                m_data.MRI_MDFEE_CD.Add(data.MDFEE_CD); // 수가코드
                m_data.MRI_MDFEE_CD_NM.Add(data.MDFEE_CD_NM); // 검사명
                m_data.MRI_EXM_EXEC_DT.Add(CUtil.GetDateTime(data.EXM_EXEC_DT_DATE, data.EXM_EXEC_DT_TIME)); // 검사일시(YYYYMMDDHHMM)
                m_data.MRI_RD_SDR_DCT_YN.Add(GetRD_SDR_DCT_YN(data.RD_SDR_DCT_YN)); // 영상의학과 전문의 판독 여부(1:Yes, 2:No)
                m_data.MRI_DCT_RST_DD.Add(data.DCT_RST_DD); // 판독 완료일(YYYYMMDD)
            }
            m_data.MRI_CTRST_USE_YN = CUtil.GetRBString(rbMRI_CTRST_USE_YN_1, rbMRI_CTRST_USE_YN_2); // 조영제 사용
            m_data.MRI_CTRST_PTNT_ASM_RCD_YN = CUtil.GetRBString(rbMRI_CTRST_PTNT_ASM_RCD_YN_1, rbMRI_CTRST_PTNT_ASM_RCD_YN_2); // 환자 평가 기록 유무
            m_data.MRI_CTRST_PTNT_ASM_RCD_DD = txtMRI_CTRST_PTNT_ASM_RCD_DD.Text.Trim(); // 환자 평가 기록 일자
            m_data.MRI_KDNY_FCLT_EXM_YN = CUtil.GetRBString(rbMRI_KDNY_FCLT_EXM_YN_1, rbMRI_KDNY_FCLT_EXM_YN_2); // 신장기능검사 유무
            m_data.BF_MRI_PTNT_ASM_RCD_YN = CUtil.GetRBString(rbBF_MRI_PTNT_ASM_RCD_YN_1, rbBF_MRI_PTNT_ASM_RCD_YN_2); // MRI전 환자 평가 기록 유무
            m_data.BF_MRI_PTNT_ASM_RCD_DD = txtBF_MRI_PTNT_ASM_RCD_DD.Text.Trim(); // MRI전 환자 평가 기록 일자

            // --- E. PET ---
            // PET 검사정보(그리드)
            m_data.PET_MDFEE_CD.Clear();
            m_data.PET_MDFEE_CD_NM.Clear();
            m_data.PET_EXM_EXEC_DT.Clear();
            var listPET = grdPET.DataSource as List<PET>;
            foreach (var data in listPET)
            {
                m_data.PET_MDFEE_CD.Add(data.MDFEE_CD); // 수가코드
                m_data.PET_MDFEE_CD_NM.Add(data.MDFEE_CD_NM); // 검사명
                m_data.PET_EXM_EXEC_DT.Add(CUtil.GetDateTime(data.EXM_EXEC_DT_DATE, data.EXM_EXEC_DT_TIME)); // 검사일시(YYYYMMDDHHMM)
            }
            m_data.FDG_INJC_QTY_RCD_YN = CUtil.GetRBString(rbFDG_INJC_QTY_RCD_YN_1, rbFDG_INJC_QTY_RCD_YN_2); // F-18 FDG 투여량 기록 유무
            m_data.FDG_TOT_INJC_QTY = txtFDG_TOT_INJC_QTY.Text.Trim(); // F-18 FDG 투여량
            m_data.FDG_UNIT = CUtil.GetRBString(rbFDG_UNIT_1, rbFDG_UNIT_2); // 단위
            m_data.HEIG = txtHEIG.Text.Trim(); // 키(cm)
            m_data.BWGT = txtBWGT.Text.Trim(); // 몸무게(kg)

            // --- F. 기타 사항 ---
            // 첨부
            //m_data.APND_DATA_NO = "";

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
                List<CDataASM049_001> list = (List<CDataASM049_001>)m_view.DataSource;
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
                List<CDataASM049_001> list = (List<CDataASM049_001>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowCT_Click(object sender, EventArgs e)
        {
            List<CT> list = (List<CT>)grdCT.DataSource;
            list.Add(new CT());
            RefreshGrid();
        }

        private void btnDelRowCT_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdCTView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<CT> list = (List<CT>)grdCT.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowMRI_Click(object sender, EventArgs e)
        {
            List<MRI> list = (List<MRI>)grdMRI.DataSource;
            list.Add(new MRI());
            RefreshGrid();
        }

        private void btnDelRowMRI_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdMRIView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<MRI> list = (List<MRI>)grdMRI.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowPET_Click(object sender, EventArgs e)
        {
            List<PET> list = (List<PET>)grdPET.DataSource;
            list.Add(new PET());
            RefreshGrid();
        }

        private void btnDelRowPET_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdPETView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<PET> list = (List<PET>)grdPET.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendTF(false);
        }

        private void btnSendTmp_Click(object sender, EventArgs e)
        {
            SendTF(true);
        }

        private void SendTF(bool isTmp)
        {
            if (MessageBox.Show((isTmp ? "임시 " : "") + "전송하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Send(isTmp);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
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
                this.ReQuery();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void ReQuery()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbTransaction tran = null;

                try
                {
                    conn.Open();

                    string sysdt = MetroLib.Util.GetSysDate(conn);
                    string systm = MetroLib.Util.GetSysTime(conn);

                    CMakeASM049 make = new CMakeASM049();
                    tran = conn.BeginTransaction();
                    make.MakeASM049(m_data, sysdt, systm, m_User, conn, tran, true);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show(ex.Message);
                }

                ShowData();
                RefreshGrid();
            }
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
    }
}

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
    public partial class ADD7007_ASM037_003 : Form
    {
        // B. 수술정보
        class SOPR
        {
            public string ASM_OPRM_IPAT_DT_DATE { get; set; } // 수술실 입실일시(YYYYMMDDHHMM)
            public string ASM_OPRM_IPAT_DT_TIME { get; set; }
            public string ASM_OPRM_DSCG_DT_DATE { get; set; } // 수술실 퇴실일시(YYYYMMDDHHMM)
            public string ASM_OPRM_DSCG_DT_TIME { get; set; }
            public string ASM_RCRM_DSCG_DT_DATE { get; set; } // 회복실 퇴실일시(YYYYMMDDHHMM)
            public string ASM_RCRM_DSCG_DT_TIME { get; set; }
            public string SOPR_NM { get; set; } // 수술명
            public string SOPR_MDFEE_CD { get; set; } // 수가코드(MDFEE_CD)

            public SOPR()
            {
                ASM_OPRM_IPAT_DT_DATE = ""; // 수술실 입실일시(YYYYMMDDHHMM)
                ASM_OPRM_IPAT_DT_TIME = "";
                ASM_OPRM_DSCG_DT_DATE = ""; // 수술실 퇴실일시(YYYYMMDDHHMM)
                ASM_OPRM_DSCG_DT_TIME = "";
                ASM_RCRM_DSCG_DT_DATE = ""; // 회복실 퇴실일시(YYYYMMDDHHMM)
                ASM_RCRM_DSCG_DT_TIME = "";
                SOPR_NM = ""; // 수술명
                SOPR_MDFEE_CD = ""; // 수가코드(MDFEE_CD)
            }
        }

        // C.수혈 체크리스트 사용 현황
        class PRSC
        {
            public string ASM_PRSC_DT_DATE { get; set; } // 처방일시(YYYYMMDDHHMM)
            public string ASM_PRSC_DT_TIME { get; set; }
            public string ASM_PRSC_UNIT_CNT { get; set; } // 처방량(unit)
            public string ASM_BLTS_CHKLST_USE_YN { get; set; } // 수혈 체크리스트 사용여부(1.Yes 2.No)
            public string ASM_BLTS_STA_DT_DATE { get; set; } // 수혈시작일시(YYYYMMDDHHMM)
            public string ASM_BLTS_STA_DT_TIME { get; set; }
            public string ASM_PRSC_BLTS_DGM_NM { get; set; } // 수혈제제명(BLTS_DGM_NM)
            public string ASM_PRSC_MDFEE_CD { get; set; } // 수가코드(MDFEE_CD)
            public string ASM_BLTS_UNIT_CNT { get; set; } // 수혈량(unit)(BLTS_UNIT_CNT)

            public PRSC()
            {
                ASM_PRSC_DT_DATE = ""; // 처방일시(YYYYMMDDHHMM)
                ASM_PRSC_DT_TIME = "";
                ASM_PRSC_UNIT_CNT = ""; // 처방량(unit)
                ASM_BLTS_CHKLST_USE_YN = ""; // 수혈 체크리스트 사용여부(1.Yes 2.No)
                ASM_BLTS_STA_DT_DATE = ""; // 수혈시작일시(YYYYMMDDHHMM)
                ASM_BLTS_STA_DT_TIME = "";
                ASM_PRSC_BLTS_DGM_NM = ""; // 수혈제제명(BLTS_DGM_NM)
                ASM_PRSC_MDFEE_CD = ""; // 수가코드(MDFEE_CD)
                ASM_BLTS_UNIT_CNT = ""; // 수혈량(unit)(BLTS_UNIT_CNT)
            }
        }

        // D. 투약정보
        class ANM_DIAG
        {
            public string SICK_SYM { get; set; } // 상병분류기호
            public string DIAG_NM { get; set; } // 진단명

            public ANM_DIAG()
            {
                SICK_SYM = ""; // 상병분류기호
                DIAG_NM = ""; // 진단명
            }
        
        }
        class ANM_REFM
        {
            public string MDS_NM { get; set; } // 빈혈교정 처방약품명
            public string MDS_CD { get; set; } // 빈혈교정 처방약품코드

            public ANM_REFM()
            {
                MDS_NM = "";
                MDS_CD = "";
            }
        }

        // E.검사정보
        class EXM
        {
            public string ASM_EXM_RST_DT_DATE { get; set; } // 검사결과일시(YYYYMMDDHHMM)
            public string ASM_EXM_RST_DT_TIME { get; set; }
            public string EXM_MDFEE_CD { get; set; } // 수가코드(MDFEE_CD)
            public string EXM_NM { get; set; } // 검사명
            public string HG_NUV { get; set; } // 검사결과(g/dL)

            public EXM()
            {
                ASM_EXM_RST_DT_DATE = ""; // 검사결과일시(YYYYMMDDHHMM)
                ASM_EXM_RST_DT_TIME = "";
                EXM_MDFEE_CD = ""; // 수가코드(MDFEE_CD)
                EXM_NM = ""; // 검사명
                HG_NUV = ""; // 검사결과(g/dL)
            }
        }

        // F.수혈정보
        class BLTS
        {
            public string BLTS_STA_DT_DATE { get; set; } // 수혈시작일시(YYYYMMDDHHMM)(ASM_BLTS_STA_DT)
            public string BLTS_STA_DT_TIME { get; set; }
            public string BLTS_END_DT_DATE { get; set; } // 수혈종료일시(YYYYMMDDHHMM)(ASM_BLTS_END_DT)
            public string BLTS_END_DT_TIME { get; set; }
            public string BLTS_DGM_NM { get; set; } // 수혈제제명
            public string BLTS_MDFEE_CD { get; set; } // 수가코드(MDFEE_CD)
            public string BLTS_UNIT_CNT { get; set; } // 수혈량(unit)
            public string HG_DCR_YN { get; set; } // Hb저하 발생 여부(1.Yes 2.No)
            public string OPRM_HMRHG_OCUR_YN_CD { get; set; } // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
            public string OPRM_MIDD_HMRHG_QTY { get; set; } // 수술 중 실혈량(ml)
            public string OPRM_AF_DRN_QTY { get; set; } // 수술 후 배액량(ml)
            public string BLTS_RS_ETC_YN { get; set; } // 그 외 수혈사유 여부(1.Yes 2.No)
            public string BLTS_RS_ETC_TXT { get; set; } // 수혈사유 기타 상세

            public BLTS()
            {
                BLTS_STA_DT_DATE = ""; // 수혈시작일시(YYYYMMDDHHMM)(ASM_BLTS_STA_DT)
                BLTS_STA_DT_TIME = "";
                BLTS_END_DT_DATE = ""; // 수혈종료일시(YYYYMMDDHHMM)(ASM_BLTS_END_DT)
                BLTS_END_DT_TIME = "";
                BLTS_DGM_NM = ""; // 수혈제제명
                BLTS_MDFEE_CD = ""; // 수가코드(MDFEE_CD)
                BLTS_UNIT_CNT = ""; // 수혈량(unit)
                HG_DCR_YN = ""; // Hb저하 발생 여부(1.Yes 2.No)
                OPRM_HMRHG_OCUR_YN_CD = ""; // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
                OPRM_MIDD_HMRHG_QTY = ""; // 수술 중 실혈량(ml)
                OPRM_AF_DRN_QTY = ""; // 수술 후 배액량(ml)
                BLTS_RS_ETC_YN = ""; // 그 외 수혈사유 여부(1.Yes 2.No)
                BLTS_RS_ETC_TXT = ""; // 수혈사유 기타 상세
            }
        }

        private string m_Hosid;
        private string m_User;
        private DevExpress.XtraGrid.Views.Grid.GridView m_view;
        private CDataASM037_003 m_data;

        public ADD7007_ASM037_003()
        {
            InitializeComponent();
            InitCombobox();
            MakeComboInGrid();
        }

        public ADD7007_ASM037_003(string hosid, string user, DevExpress.XtraGrid.Views.Grid.GridView view)
            : this()
        {
            m_Hosid = hosid;
            m_User = user;
            m_view = view;
            List<CDataASM037_003> list = (List<CDataASM037_003>)m_view.DataSource;
            m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
            ShowData();
        }

        private void InitCombobox()
        {
        }

        private void MakeComboInGrid()
        {
            // GRID에 콤보 컬럼을 만든다.

            // 1-2-3. 수혈 체크리스트 사용 여부
            CUtil.SetGridCombo(grdPRSCView.Columns["ASM_BLTS_CHKLST_USE_YN"],
                "",
                "Yes",
                "No"
                );

            // 1-2-6. Hb저하 발생 여부
            CUtil.SetGridCombo(grdBLTSView.Columns["HG_DCR_YN"],
                "",
                "Yes",
                "No"
                );

            // 1-2-7. 수술 관련 실혈 발생 여부
            CUtil.SetGridCombo(grdBLTSView.Columns["OPRM_HMRHG_OCUR_YN_CD"],
                "",
                "해당없음",
                "수술 중",
                "수술 후"
                );

            // 1-2-8. 그 외 수혈사유 여부
            CUtil.SetGridCombo(grdBLTSView.Columns["BLTS_RS_ETC_YN"],
                "",
                "Yes",
                "No"
                );
        }

        private void ShowData()
        {
            // 공통 상단 정보
            txtPid.Text = m_data.PID;
            txtPnm.Text = m_data.PNM;
            txtResid_disp.Text = m_data.RESID_DISP;

            // A. 기본 정보
            txtIPAT_DD.Text = m_data.IPAT_DD; // 입원일자(YYYYMMDD)
            txtDSCG_DD.Text = m_data.DSCG_DD; // 퇴원일자(YYYYMMDD)

            // B. 수술 정보 (그리드)
            rbSOPR_YN_1.Checked = m_data.SOPR_YN == "1"; // 수술 여부
            rbSOPR_YN_2.Checked = m_data.SOPR_YN == "2";
            grdSOPR.DataSource = null;
            List<SOPR> listSOPR = new List<SOPR>();
            grdSOPR.DataSource = listSOPR;
            for (int i = 0; i < m_data.ASM_OPRM_IPAT_DT.Count; i++)
            {
                SOPR data = new SOPR();
                data.ASM_OPRM_IPAT_DT_DATE = CUtil.GetDate(m_data.ASM_OPRM_IPAT_DT[i]); // 수술실 입실일시
                data.ASM_OPRM_IPAT_DT_TIME = CUtil.GetTime(m_data.ASM_OPRM_IPAT_DT[i]);
                data.ASM_OPRM_DSCG_DT_DATE = CUtil.GetDate(m_data.ASM_OPRM_DSCG_DT[i]); // 수숡실 퇴실일시
                data.ASM_OPRM_DSCG_DT_TIME = CUtil.GetTime(m_data.ASM_OPRM_DSCG_DT[i]);
                data.ASM_RCRM_DSCG_DT_DATE = CUtil.GetDate(m_data.ASM_RCRM_DSCG_DT[i]); // 회복실 퇴실일시
                data.ASM_RCRM_DSCG_DT_TIME = CUtil.GetTime(m_data.ASM_RCRM_DSCG_DT[i]);
                data.SOPR_NM = m_data.SOPR_NM[i]; // 수술명
                data.SOPR_MDFEE_CD = m_data.SOPR_MDFEE_CD[i]; // 수가코드

                listSOPR.Add(data);
            }

            rbLFB_FS_YN_1.Checked = m_data.LFB_FS_YN == "1"; // 척추후방고정술 실시여부
            rbLFB_FS_YN_2.Checked = m_data.LFB_FS_YN == "2";
            rbLFB_FS_LVL_1.Checked = m_data.LFB_FS_LVL == "1"; // 척추후방고정술 Level
            rbLFB_FS_LVL_2.Checked = m_data.LFB_FS_LVL == "2";
            rbLFB_FS_LVL_3.Checked = m_data.LFB_FS_LVL == "3";
            rbKNJN_RPMT_YN_1.Checked = m_data.KNJN_RPMT_YN == "1"; // 슬관절치환술 실시여부
            rbKNJN_RPMT_YN_2.Checked = m_data.KNJN_RPMT_YN == "2";
            rbKNJN_RPMT_RGN_CD_1.Checked = m_data.KNJN_RPMT_RGN_CD == "1"; // 슬관절치환술 부위
            rbKNJN_RPMT_RGN_CD_2.Checked = m_data.KNJN_RPMT_RGN_CD == "2";

            // C. 수혈 체크리스트 사용 현황
            rbASM_PRSC_YN_1.Checked = m_data.ASM_PRSC_YN == "1"; // 수혈처방 처방여부
            rbASM_PRSC_YN_2.Checked = m_data.ASM_PRSC_YN == "2";

            grdPRSC.DataSource = null;
            List<PRSC> listPRSC = new List<PRSC>();
            grdPRSC.DataSource = listPRSC;
            for (int i = 0; i < m_data.ASM_PRSC_DT.Count; i++)
            {
                PRSC data = new PRSC();
                data.ASM_PRSC_DT_DATE = CUtil.GetDate(m_data.ASM_PRSC_DT[i]);
                data.ASM_PRSC_DT_TIME = CUtil.GetTime(m_data.ASM_PRSC_DT[i]);
                data.ASM_PRSC_UNIT_CNT = m_data.ASM_PRSC_UNIT_CNT[i];
                data.ASM_BLTS_CHKLST_USE_YN = GetASM_BLTS_CHKLST_USE_YN_NM(m_data.ASM_BLTS_CHKLST_USE_YN[i]);
                data.ASM_BLTS_STA_DT_DATE = CUtil.GetDate(m_data.ASM_BLTS_STA_DT[i]);
                data.ASM_BLTS_STA_DT_TIME = CUtil.GetTime(m_data.ASM_BLTS_STA_DT[i]);
                data.ASM_PRSC_BLTS_DGM_NM = m_data.ASM_PRSC_BLTS_DGM_NM[i];
                data.ASM_PRSC_MDFEE_CD = m_data.ASM_PRSC_MDFEE_CD[i];
                data.ASM_BLTS_UNIT_CNT = m_data.ASM_BLTS_UNIT_CNT[i];

                listPRSC.Add(data);
            }

            // D. 투약 정보
            rbANM_DIAG_YN_1.Checked = m_data.ANM_DIAG_YN == "1"; // 빈혈 진단
            rbANM_DIAG_YN_2.Checked = m_data.ANM_DIAG_YN == "2";

            grdANM_DIAG.DataSource = null;
            List<ANM_DIAG> listANM_DIAG = new List<ANM_DIAG>();
            grdANM_DIAG.DataSource = listANM_DIAG;
            for (int i = 0; i < m_data.SICK_SYM.Count; i++)
            {
                ANM_DIAG data = new ANM_DIAG();
                data.SICK_SYM = m_data.SICK_SYM[i];
                data.DIAG_NM = m_data.DIAG_NM[i];

                listANM_DIAG.Add(data);
            }

            rbANM_REFM_YN_1.Checked = m_data.ANM_REFM_YN == "1"; // 빈혈교정 유무
            rbANM_REFM_YN_2.Checked = m_data.ANM_REFM_YN == "2";

            grdANM_REFM.DataSource = null;
            List<ANM_REFM> listANM_REFM = new List<ANM_REFM>();
            grdANM_REFM.DataSource = listANM_REFM;
            for (int i = 0; i < m_data.MDS_NM.Count; i++)
            {
                ANM_REFM data = new ANM_REFM();
                data.MDS_NM = m_data.MDS_NM[i];
                data.MDS_CD = m_data.MDS_CD[i];

                listANM_REFM.Add(data);
            }

            // E. 검사 정보
            rbHG_EXM_ENFC_YN_1.Checked = m_data.HG_EXM_ENFC_YN == "1"; // Hemoglobin(Hb) 시행 여부
            rbHG_EXM_ENFC_YN_2.Checked = m_data.HG_EXM_ENFC_YN == "2";

            grdEXM.DataSource = null;
            List<EXM> listEXM = new List<EXM>();
            grdEXM.DataSource = listEXM;
            for (int i = 0; i < m_data.ASM_EXM_RST_DT.Count; i++)
            {
                EXM data = new EXM();
                data.ASM_EXM_RST_DT_DATE = CUtil.GetDate(m_data.ASM_EXM_RST_DT[i]);
                data.ASM_EXM_RST_DT_TIME = CUtil.GetTime(m_data.ASM_EXM_RST_DT[i]);
                data.EXM_MDFEE_CD = m_data.EXM_MDFEE_CD[i];
                data.EXM_NM = m_data.EXM_NM[i];
                data.HG_NUV = m_data.HG_NUV[i];

                listEXM.Add(data);
            }

            // F. 수혈 정보
            rbBLTS_YN_1.Checked = m_data.BLTS_YN == "1"; // 수혈 시행 여부
            rbBLTS_YN_2.Checked = m_data.BLTS_YN == "2";

            grdBLTS.DataSource = null;
            List<BLTS> listBLTS = new List<BLTS>();
            grdBLTS.DataSource = listBLTS;
            for (int i = 0; i < m_data.BLTS_STA_DT.Count; i++)
            {
                BLTS data = new BLTS();
                data.BLTS_STA_DT_DATE = CUtil.GetDate(m_data.BLTS_STA_DT[i]);
                data.BLTS_STA_DT_TIME = CUtil.GetTime(m_data.BLTS_STA_DT[i]);
                data.BLTS_END_DT_DATE = CUtil.GetDate(m_data.BLTS_END_DT[i]);
                data.BLTS_END_DT_TIME = CUtil.GetTime(m_data.BLTS_END_DT[i]);
                data.BLTS_DGM_NM = m_data.BLTS_DGM_NM[i];
                data.BLTS_MDFEE_CD = m_data.BLTS_MDFEE_CD[i];
                data.BLTS_UNIT_CNT = m_data.BLTS_UNIT_CNT[i];
                data.HG_DCR_YN = GetHG_DCR_YN_NM(m_data.HG_DCR_YN[i]);
                data.OPRM_HMRHG_OCUR_YN_CD = GetOPRM_HMRHG_OCUR_YN_CD_NM(m_data.OPRM_HMRHG_OCUR_YN_CD[i]);
                data.OPRM_MIDD_HMRHG_QTY = m_data.OPRM_MIDD_HMRHG_QTY[i];
                data.OPRM_AF_DRN_QTY = m_data.OPRM_AF_DRN_QTY[i];
                data.BLTS_RS_ETC_YN = GetBLTS_RS_ETC_YN_NM(m_data.BLTS_RS_ETC_YN[i]);
                data.BLTS_RS_ETC_TXT = m_data.BLTS_RS_ETC_TXT[i];

                listBLTS.Add(data);
            }

            RefreshGrid();

        }

        private void RefreshGrid()
        {
            CUtil.RefreshGrid(grdSOPR, grdSOPRView);
            CUtil.RefreshGrid(grdPRSC, grdPRSCView);
            CUtil.RefreshGrid(grdANM_DIAG, grdANM_DIAGView);
            CUtil.RefreshGrid(grdANM_REFM, grdANM_REFMView);
            CUtil.RefreshGrid(grdEXM, grdEXMView);
            CUtil.RefreshGrid(grdBLTS, grdBLTSView);
        }

        private string GetASM_BLTS_CHKLST_USE_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetASM_BLTS_CHKLST_USE_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private string GetHG_DCR_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetHG_DCR_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private string GetOPRM_HMRHG_OCUR_YN_CD_NM(string p_value)
        {
            if (p_value == "0") return "해당없음";	
            if (p_value == "1") return "수술 중";
            if (p_value == "2") return "수술 후";
            return "";
        }

        private string GetOPRM_HMRHG_OCUR_YN_CD(string p_value)
        {
            if (p_value == "해당없음") return "0";
            if (p_value == "수술 중") return "1";
            if (p_value == "수술 후") return "2";
            return "";
        }

        private string GetBLTS_RS_ETC_YN_NM(string p_value)
        {
            if (p_value == "1") return "Yes";
            if (p_value == "2") return "No";
            return "";
        }

        private string GetBLTS_RS_ETC_YN(string p_value)
        {
            if (p_value == "Yes") return "1";
            if (p_value == "No") return "2";
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // A. 기본 정보
            m_data.IPAT_DD = txtIPAT_DD.Text.ToString(); // 입원일자(YYYYMMDD)
            m_data.DSCG_DD = txtDSCG_DD.Text.ToString(); // 퇴원일자(YYYYMMDD)

            // B. 수술 정보 (그리드)
            m_data.SOPR_YN = CUtil.GetRBString(rbSOPR_YN_1, rbSOPR_YN_2);
            m_data.ASM_OPRM_IPAT_DT.Clear(); // 수술실 입실일시(YYYYMMDDHHMM)
            m_data.ASM_OPRM_DSCG_DT.Clear(); // 수술실 퇴실일시(YYYYMMDDHHMM)
            m_data.ASM_RCRM_DSCG_DT.Clear(); // 회복실 퇴실일시(YYYYMMDDHHMM)
            m_data.SOPR_NM.Clear(); // 수술명
            m_data.SOPR_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)

            List<SOPR> listSOPR = (List<SOPR>)grdSOPR.DataSource;
            foreach (SOPR data in listSOPR)
            {
                m_data.ASM_OPRM_IPAT_DT.Add(CUtil.GetDateTime(data.ASM_OPRM_IPAT_DT_DATE, data.ASM_OPRM_IPAT_DT_TIME)); // 수술실 입실일시(YYYYMMDDHHMM)
                m_data.ASM_OPRM_DSCG_DT.Add(CUtil.GetDateTime(data.ASM_OPRM_DSCG_DT_DATE, data.ASM_OPRM_DSCG_DT_TIME)); // 수술실 퇴실일시(YYYYMMDDHHMM)
                m_data.ASM_RCRM_DSCG_DT.Add(CUtil.GetDateTime(data.ASM_RCRM_DSCG_DT_DATE, data.ASM_RCRM_DSCG_DT_TIME)); // 회복실 퇴실일시(YYYYMMDDHHMM)
                m_data.SOPR_NM.Add(data.SOPR_NM); // 수술명
                m_data.SOPR_MDFEE_CD.Add(data.SOPR_MDFEE_CD); // 수가코드(MDFEE_CD)
            }

            m_data.LFB_FS_YN = CUtil.GetRBString(rbLFB_FS_YN_1, rbLFB_FS_YN_2); // 척추후방고정술 실시여부(1.Yes, 2.No)
            m_data.LFB_FS_LVL = CUtil.GetRBString(rbLFB_FS_LVL_1, rbLFB_FS_LVL_2, rbLFB_FS_LVL_3); // 척추후방고정술 Level(1,2,3)
            m_data.KNJN_RPMT_YN = CUtil.GetRBString(rbKNJN_RPMT_YN_1, rbKNJN_RPMT_YN_1); // 슬관절치환술 실시여부(1.Yes 2.No)
            m_data.KNJN_RPMT_RGN_CD = CUtil.GetRBString(rbKNJN_RPMT_RGN_CD_1, rbKNJN_RPMT_RGN_CD_2); // 슬관절치환술 부위(1.단측 2.양측)

            // C. 수혈 체크리스트 사용 현황 (단일값)
            m_data.ASM_PRSC_YN = CUtil.GetRBString(rbASM_PRSC_YN_1, rbASM_PRSC_YN_2); // 처방여부(1.Yes 2.No)

            m_data.ASM_PRSC_DT.Clear(); // 처방일시(YYYYMMDDHHMM)
            m_data.ASM_PRSC_UNIT_CNT.Clear(); // 처방량(unit)
            m_data.ASM_BLTS_CHKLST_USE_YN.Clear(); // 수혈 체크리스트 사용여부(1.Yes 2.No)
            m_data.ASM_BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)
            m_data.ASM_PRSC_BLTS_DGM_NM.Clear(); // 수혈제제명(BLTS_DGM_NM)
            m_data.ASM_PRSC_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            m_data.ASM_BLTS_UNIT_CNT.Clear(); // 수혈량(unit)(BLTS_UNIT_CNT)

            List<PRSC> listPRSC = (List<PRSC>)grdPRSC.DataSource;
            foreach (PRSC data in listPRSC)
            {
                m_data.ASM_PRSC_DT.Add(CUtil.GetDateTime(data.ASM_PRSC_DT_DATE, data.ASM_PRSC_DT_TIME)); // 처방일시(YYYYMMDDHHMM)
                m_data.ASM_PRSC_UNIT_CNT.Add(data.ASM_PRSC_UNIT_CNT); // 처방량(unit)
                m_data.ASM_BLTS_CHKLST_USE_YN.Add(GetASM_BLTS_CHKLST_USE_YN(data.ASM_BLTS_CHKLST_USE_YN)); // 수혈 체크리스트 사용여부(1.Yes 2.No)
                m_data.ASM_BLTS_STA_DT.Add(CUtil.GetDateTime(data.ASM_BLTS_STA_DT_DATE, data.ASM_BLTS_STA_DT_TIME)); // 수혈시작일시(YYYYMMDDHHMM)
                m_data.ASM_PRSC_BLTS_DGM_NM.Add(data.ASM_PRSC_BLTS_DGM_NM); // 수혈제제명(BLTS_DGM_NM)
                m_data.ASM_PRSC_MDFEE_CD.Add(data.ASM_PRSC_MDFEE_CD); // 수가코드(MDFEE_CD)
                m_data.ASM_BLTS_UNIT_CNT.Add(data.ASM_BLTS_UNIT_CNT); // 수혈량(unit)(BLTS_UNIT_CNT)
            }

            // D. 투약 정보 (단일값)
            m_data.ANM_DIAG_YN = CUtil.GetRBString(rbANM_DIAG_YN_1, rbANM_DIAG_YN_2); // 빈혈 진단(1.Yes 2.No)

            m_data.SICK_SYM.Clear(); // 상병분류기호
            m_data.DIAG_NM.Clear(); // 진단명

            List<ANM_DIAG> listANM_DIAG = (List<ANM_DIAG>)grdANM_DIAG.DataSource;
            foreach (ANM_DIAG data in listANM_DIAG)
            {
                m_data.SICK_SYM.Add(data.SICK_SYM); // 상병분류기호
                m_data.DIAG_NM.Add(data.DIAG_NM); // 진단명
            }

            m_data.ANM_REFM_YN = CUtil.GetRBString(rbANM_REFM_YN_1, rbANM_REFM_YN_2); // 빈혈교정 유무(1.Yes 2.No)

            m_data.MDS_NM.Clear(); // 빈혈교정 처방약품명
            m_data.MDS_CD.Clear(); // 빈혈교정 처방약품코드
            List<ANM_REFM> listANM_REFM = (List<ANM_REFM>)grdANM_REFM.DataSource;
            foreach (ANM_REFM data in listANM_REFM)
            {
                m_data.MDS_NM.Add(data.MDS_NM); // 빈혈교정 처방약품명
                m_data.MDS_CD.Add(data.MDS_CD); // 빈혈교정 처방약품코드
            }

            // E. 검사 정보
            m_data.HG_EXM_ENFC_YN = CUtil.GetRBString(rbHG_EXM_ENFC_YN_1, rbHG_EXM_ENFC_YN_2); // Hb검사 시행여부(1.Yes 2.No)

            m_data.ASM_EXM_RST_DT.Clear(); // 검사결과일시(YYYYMMDDHHMM)
            m_data.EXM_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            m_data.EXM_NM.Clear(); // 검사명
            m_data.HG_NUV.Clear(); // 검사결과(g/dL)

            List<EXM> listEXM = (List<EXM>)grdEXM.DataSource;
            foreach (EXM data in listEXM)
            {
                m_data.ASM_EXM_RST_DT.Add(CUtil.GetDateTime(data.ASM_EXM_RST_DT_DATE, data.ASM_EXM_RST_DT_TIME)); // 검사결과일시(YYYYMMDDHHMM)
                m_data.EXM_MDFEE_CD.Add(data.EXM_MDFEE_CD); // 수가코드(MDFEE_CD)
                m_data.EXM_NM.Add(data.EXM_NM); // 검사명
                m_data.HG_NUV.Add(data.HG_NUV); // 검사결과(g/dL)
            }

            // F. 수혈 정보
            m_data.BLTS_YN = CUtil.GetRBString(rbBLTS_YN_1, rbBLTS_YN_2); // 수혈 시행여부(1.Yes 2.No)

            m_data.BLTS_STA_DT.Clear(); // 수혈시작일시(YYYYMMDDHHMM)
            m_data.BLTS_END_DT.Clear(); // 수혈종료일시(YYYYMMDDHHMM)
            m_data.BLTS_DGM_NM.Clear(); // 수혈제제명
            m_data.BLTS_MDFEE_CD.Clear(); // 수가코드(MDFEE_CD)
            m_data.BLTS_UNIT_CNT.Clear(); // 수혈량(unit)
            m_data.HG_DCR_YN.Clear(); // Hb저하 발생 여부(1.Yes 2.No)
            m_data.OPRM_HMRHG_OCUR_YN_CD.Clear(); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
            m_data.OPRM_MIDD_HMRHG_QTY.Clear(); // 수술 중 실혈량(ml)
            m_data.OPRM_AF_DRN_QTY.Clear(); // 수술 후 배액량(ml)
            m_data.BLTS_RS_ETC_YN.Clear(); // 그 외 수혈사유 여부(1.Yes 2.No)
            m_data.BLTS_RS_ETC_TXT.Clear(); // 수혈사유 기타 상세

            List<BLTS> listBLTS = (List<BLTS>)grdBLTS.DataSource;
            foreach (BLTS data in listBLTS)
            {
                m_data.BLTS_STA_DT.Add(CUtil.GetDateTime(data.BLTS_STA_DT_DATE, data.BLTS_STA_DT_TIME)); // 수혈시작일시(YYYYMMDDHHMM)
                m_data.BLTS_END_DT.Add(CUtil.GetDateTime(data.BLTS_END_DT_DATE, data.BLTS_END_DT_TIME)); // 수혈종료일시(YYYYMMDDHHMM)
                m_data.BLTS_DGM_NM.Add(data.BLTS_DGM_NM); // 수혈제제명
                m_data.BLTS_MDFEE_CD.Add(data.BLTS_MDFEE_CD); // 수가코드(MDFEE_CD)
                m_data.BLTS_UNIT_CNT.Add(data.BLTS_UNIT_CNT); // 수혈량(unit)
                m_data.HG_DCR_YN.Add(GetHG_DCR_YN(data.HG_DCR_YN)); // Hb저하 발생 여부(1.Yes 2.No)
                m_data.OPRM_HMRHG_OCUR_YN_CD.Add(GetOPRM_HMRHG_OCUR_YN_CD(data.OPRM_HMRHG_OCUR_YN_CD)); // 수술 관련 실혈 발생 여부(0.해당없음 1.수술중 2.수술후)
                m_data.OPRM_MIDD_HMRHG_QTY.Add(data.OPRM_MIDD_HMRHG_QTY); // 수술 중 실혈량(ml)
                m_data.OPRM_AF_DRN_QTY.Add(data.OPRM_AF_DRN_QTY); // 수술 후 배액량(ml)
                m_data.BLTS_RS_ETC_YN.Add(GetBLTS_RS_ETC_YN(data.BLTS_RS_ETC_YN)); // 그 외 수혈사유 여부(1.Yes 2.No)
                m_data.BLTS_RS_ETC_TXT.Add(data.BLTS_RS_ETC_TXT); // 수혈사유 기타 상세
            }
            

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
                List<CDataASM037_003> list = (List<CDataASM037_003>)m_view.DataSource;
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
                List<CDataASM037_003> list = (List<CDataASM037_003>)m_view.DataSource;
                m_data = list[m_view.GetDataSourceRowIndex(m_view.FocusedRowHandle)];
                ShowData();
            }
        }

        private void btnInsRowSOPR_Click(object sender, EventArgs e)
        {
            List<SOPR> list = (List<SOPR>)grdSOPR.DataSource;
            list.Add(new SOPR());
            RefreshGrid();
        }

        private void btnDelRowSOPR_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdSOPRView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<SOPR> list = (List<SOPR>)grdSOPR.DataSource;
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

        private void btnInsRowANM_DIAG_Click(object sender, EventArgs e)
        {
            List<ANM_DIAG> list = (List<ANM_DIAG>)grdANM_DIAG.DataSource;
            list.Add(new ANM_DIAG());
            RefreshGrid();
        }

        private void btnDelRowANM_DIAG_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdANM_DIAGView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<ANM_DIAG> list = (List<ANM_DIAG>)grdANM_DIAG.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowANN_REFM_Click(object sender, EventArgs e)
        {
            List<ANM_REFM> list = (List<ANM_REFM>)grdANM_REFM.DataSource;
            list.Add(new ANM_REFM());
            RefreshGrid();
        }

        private void btnDelRowANN_REFM_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdANM_REFMView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<ANM_REFM> list = (List<ANM_REFM>)grdANM_REFM.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowEXM_Click(object sender, EventArgs e)
        {
            List<EXM> list = (List<EXM>)grdEXM.DataSource;
            list.Add(new EXM());
            RefreshGrid();
        }

        private void btnDelRowEXM_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grdEXMView.FocusedRowHandle;
            if (focusedRowHandle < 0) return;

            List<EXM> list = (List<EXM>)grdEXM.DataSource;
            list.RemoveAt(focusedRowHandle);
            RefreshGrid();
        }

        private void btnInsRowBLTS_Click(object sender, EventArgs e)
        {
            List<BLTS> list = (List<BLTS>)grdBLTS.DataSource;
            list.Add(new BLTS());
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

                    CMakeASM037 make = new CMakeASM037();
                    tran = conn.BeginTransaction();
                    make.MakeASM037(m_data, sysdt, systm, m_User, conn, tran, true);
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

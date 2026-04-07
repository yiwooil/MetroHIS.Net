using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0609P
{
    public partial class ADD0609P : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        enum LRC
        {
            Left, Right, Center
        }

        enum FontKind
        {
            굴림체, 궁서체
        }


        private int m_Left;
        private int m_Top;
        private int m_Right;
        private int m_Bottom;

        private float[] m_yp;
        private float[] m_xp;

        public ADD0609P()
        {
            InitializeComponent();
        }

        public ADD0609P(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD0609P_Load(object sender, EventArgs e)
        {
            m_yp = new float[52];
            m_xp = new float[14];

            SetXYPos();

            int i = 0;
            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
            lstPrinter.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                lstPrinter.Items.Add(printer);
                if (printer == printDoc.PrinterSettings.PrinterName)
                {
                    lstPrinter.SelectedIndex = i;
                }
                i++;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (rbA.Checked == true) Print_A(e);
            if (rbB.Checked == true) Print_B(e);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Document.PrinterSettings.PrinterName = lstPrinter.SelectedItem.ToString();
            printPreviewDialog1.ShowDialog();
        }

        private void Print_A(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float halfgap = halfgap = (m_yp[1] - m_yp[0]) / 2;

            PrintBox_A4(g, m_Left, m_Top, m_Right, m_Bottom, 2);
            
            PrintBox_A4(g, m_Left, m_Top, m_Right, m_yp[3]);
            PrintTxt_A4(g, m_Left, m_Top, m_Right, m_yp[3], "재심사조정 청구서", 14, true);
            
            PrintBox_A4(g, m_xp[11], m_Top, m_Right, m_yp[1]);
            PrintTxt_A4(g, m_xp[11], m_Top, m_Right, m_yp[1], "처리기간");
            
            PrintBox_A4(g, m_xp[11], m_yp[1], m_Right, m_yp[2]);
            PrintTxt_A4(g, m_xp[11], m_yp[1], m_Right, m_yp[2], "30일");
            
            PrintBox_A4(g, m_Left, m_yp[3], m_xp[2], m_yp[5]);
            PrintTxt_A4(g, m_Left, m_yp[3], m_xp[2], m_yp[5], "문서번호");
            
            PrintBox_A4(g, m_xp[2], m_yp[3], m_xp[4], m_yp[5]);
            PrintTxt_A4(g, m_xp[2], m_yp[3], m_xp[4], m_yp[5], "");
            
            PrintBox_A4(g, m_xp[4], m_yp[3], m_xp[8], m_yp[5]);
            PrintTxt_A4(g, m_xp[4], m_yp[3], m_xp[8], m_yp[5], "진료분야");
            
            PrintBox_A4(g, m_xp[8], m_yp[3], m_Right, m_yp[5]);
            PrintTxt_A4(g, m_xp[8], m_yp[3], m_Right, m_yp[5], "");
            
            PrintBox_A4(g, m_Left, m_yp[5], m_xp[1], m_yp[9]);
            PrintTxt_A4(g, m_Left, m_yp[6], m_xp[1], m_yp[7], "요양");
            PrintTxt_A4(g, m_Left, m_yp[7], m_xp[1], m_yp[8], "기관");
            
            PrintBox_A4(g, m_xp[1], m_yp[5], m_xp[2], m_yp[7]);
            PrintTxt_A4(g, m_xp[1], m_yp[5], m_xp[2], m_yp[7], "명칭");
            
            PrintBox_A4(g, m_xp[2], m_yp[5], m_xp[4], m_yp[7]);
            PrintTxt_A4(g, m_xp[2], m_yp[5], m_xp[4], m_yp[7], "");
            
            PrintBox_A4(g, m_xp[1], m_yp[7], m_xp[2], m_yp[9]);
            PrintTxt_A4(g, m_xp[1], m_yp[7], m_xp[2], m_yp[9], "기호");
            
            PrintBox_A4(g, m_xp[2], m_yp[7], m_xp[4], m_yp[9]);
            PrintTxt_A4(g, m_xp[2], m_yp[7], m_xp[4], m_yp[9], "");
            
            PrintBox_A4(g, m_Left, m_yp[9], m_xp[2], m_yp[11]);
            PrintTxt_A4(g, m_Left, m_yp[9], m_xp[2], m_yp[10], "재심사조정청구");
            PrintTxt_A4(g, m_Left, m_yp[10], m_xp[2], m_yp[11], "건수총계");
            
            PrintBox_A4(g, m_xp[2], m_yp[9], m_xp[4], m_yp[11]);
            PrintTxt_A4(g, m_xp[2], m_yp[9], m_xp[4], m_yp[11], "");
            
            PrintBox_A4(g, m_Left, m_yp[11], m_xp[2], m_yp[13]);
            PrintTxt_A4(g, m_Left, m_yp[11], m_xp[2], m_yp[12], "재심사조정청구");
            PrintTxt_A4(g, m_Left, m_yp[12], m_xp[2], m_yp[13], "비용총액");
            
            PrintBox_A4(g, m_xp[2], m_yp[11], m_xp[4], m_yp[13]);
            PrintTxt_A4(g, m_xp[2], m_yp[11], m_xp[4], m_yp[13], "");
            
            PrintBox_A4(g, m_xp[4], m_yp[5], m_xp[6], m_yp[13]);
            PrintTxt_A4(g, m_xp[4], m_yp[7], m_xp[6], m_yp[8], "요양급여");
            PrintTxt_A4(g, m_xp[4], m_yp[8], m_xp[6], m_yp[9], "비용심사");
            PrintTxt_A4(g, m_xp[4], m_yp[9], m_xp[6], m_yp[10], "결과통보서");
            
            PrintBox_A4(g, m_xp[6], m_yp[5], m_xp[8], m_yp[7]);
            PrintTxt_A4(g, m_xp[6], m_yp[5], m_xp[8], m_yp[7], "접수번호");
            
            PrintBox_A4(g, m_xp[8], m_yp[5], m_xp[9], m_yp[7]);
            PrintTxt_A4(g, m_xp[8], m_yp[5], m_xp[9], m_yp[7], "");
            
            PrintBox_A4(g, m_xp[6], m_yp[7], m_xp[8], m_yp[9]);
            PrintTxt_A4(g, m_xp[6], m_yp[7], m_xp[8], m_yp[9], "묶음번호");
            
            PrintBox_A4(g, m_xp[8], m_yp[7], m_xp[9], m_yp[9]);
            PrintTxt_A4(g, m_xp[8], m_yp[7], m_xp[9], m_yp[9], "");
            
            PrintBox_A4(g, m_xp[6], m_yp[9], m_xp[8], m_yp[11]);
            PrintTxt_A4(g, m_xp[6], m_yp[9], m_xp[8], m_yp[11], "심사차수");
            
            PrintBox_A4(g, m_xp[8], m_yp[9], m_xp[9], m_yp[11]);
            PrintTxt_A4(g, m_xp[8], m_yp[9], m_xp[9], m_yp[11], "");

            PrintBox_A4(g, m_xp[6], m_yp[11], m_xp[8], m_yp[13]);
            PrintTxt_A4(g, m_xp[6], m_yp[11], m_xp[8], m_yp[12], "통 보 서");
            PrintTxt_A4(g, m_xp[6], m_yp[12], m_xp[8], m_yp[13], "도달일자");
            
            PrintBox_A4(g, m_xp[8], m_yp[11], m_xp[9], m_yp[13]);
            PrintTxt_A4(g, m_xp[8], m_yp[11], m_xp[9], m_yp[13], "");
            
            PrintBox_A4(g, m_xp[9], m_yp[5], m_xp[10], m_yp[7]);
            PrintTxt_A4(g, m_xp[9], m_yp[5], m_xp[10], m_yp[6], "");
            PrintTxt_A4(g, m_xp[9], m_yp[6], m_xp[10], m_yp[7], "");
            
            PrintBox_A4(g, m_xp[10], m_yp[5], m_Right, m_yp[7]);
            PrintTxt_A4(g, m_xp[10], m_yp[5], m_Right, m_yp[6], "");
            PrintTxt_A4(g, m_xp[10], m_yp[6], m_Right, m_yp[7], "");
            
            PrintBox_A4(g, m_xp[9], m_yp[7], m_xp[10], m_yp[13]);
            PrintTxt_A4(g, m_xp[9], m_yp[8], m_xp[10], m_yp[9], "첨");
            PrintTxt_A4(g, m_xp[9], m_yp[9], m_xp[10], m_yp[10], "부");
            PrintTxt_A4(g, m_xp[9], m_yp[10], m_xp[10], m_yp[11], "서");
            PrintTxt_A4(g, m_xp[9], m_yp[11], m_xp[10], m_yp[12], "류");

            PrintBox_A4(g, m_xp[10], m_yp[7], m_Right, m_yp[13]);
            PrintTxt_A4(g, m_xp[10], m_yp[7] + halfgap, m_Right, m_yp[8] + halfgap, "1.심사결과통보서", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[8] + halfgap, m_Right, m_yp[9] + halfgap, "2.진료기록부", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[9] + halfgap, m_Right, m_yp[10] + halfgap, "3.X-ray film", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[10] + halfgap, m_Right, m_yp[11] + halfgap, "4.검사결과지", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[11] + halfgap, m_Right, m_yp[12] + halfgap, "5.기타", LRC.Left);
            
            PrintBox_A4(g, m_Left, m_yp[13], m_xp[1], m_yp[15]);
            PrintTxt_A4(g, m_Left, m_yp[13], m_xp[1], m_yp[14], "순");
            PrintTxt_A4(g, m_Left, m_yp[14], m_xp[1], m_yp[15], "번");

            PrintBox_A4(g, m_xp[1], m_yp[13], m_xp[2], m_yp[15]);
            PrintTxt_A4(g, m_xp[1], m_yp[13], m_xp[2], m_yp[14], "명세서");
            PrintTxt_A4(g, m_xp[1], m_yp[14], m_xp[2], m_yp[15], "일련번호");

            PrintBox_A4(g, m_xp[2], m_yp[13], m_xp[3], m_yp[15]);
            PrintTxt_A4(g, m_xp[2], m_yp[13], m_xp[3], m_yp[15], "수진자");

            PrintBox_A4(g, m_xp[3], m_yp[13], m_xp[4], m_yp[15]);
            PrintTxt_A4(g, m_xp[3], m_yp[13], m_xp[4], m_yp[14], "진료구분");
            PrintTxt_A4(g, m_xp[3], m_yp[14], m_xp[4], m_yp[15], "(입원.외래)", 9);
            
            PrintBox_A4(g, m_xp[4], m_yp[13], m_xp[7], m_yp[14]);
            PrintTxt_A4(g, m_xp[4], m_yp[13], m_xp[7], m_yp[14], "재심사조정청구금액");
            
            PrintBox_A4(g, m_xp[4], m_yp[14], m_xp[5], m_yp[15]);
            PrintTxt_A4(g, m_xp[4], m_yp[14], m_xp[5], m_yp[15], "I항");
            
            PrintBox_A4(g, m_xp[5], m_yp[14], m_xp[7], m_yp[15]);
            PrintTxt_A4(g, m_xp[5], m_yp[14], m_xp[7], m_yp[15], "II항");
            
            PrintBox_A4(g, m_xp[7], m_yp[13], m_xp[12], m_yp[15]);
            PrintTxt_A4(g, m_xp[7], m_yp[13], m_xp[12], m_yp[15], "재심사조정청구 사유");
            
            PrintBox_A4(g, m_xp[12], m_yp[13], m_Right, m_yp[15]);
            PrintTxt_A4(g, m_xp[12], m_yp[13], m_Right, m_yp[14], "첨부");
            PrintTxt_A4(g, m_xp[12], m_yp[14], m_Right, m_yp[15], "서류");
            
            PrintBox_A4(g, m_Left, m_yp[15], m_xp[1], m_yp[32]);
            PrintBox_A4(g, m_xp[1], m_yp[15], m_xp[2], m_yp[32]);
            PrintBox_A4(g, m_xp[2], m_yp[15], m_xp[3], m_yp[32]);
            PrintBox_A4(g, m_xp[3], m_yp[15], m_xp[4], m_yp[32]);
            PrintBox_A4(g, m_xp[4], m_yp[15], m_xp[4], m_yp[32]);
            PrintBox_A4(g, m_xp[5], m_yp[15], m_xp[7], m_yp[32]);
            PrintBox_A4(g, m_xp[7], m_yp[15], m_xp[12], m_yp[32]);
            
            
            PrintBox_A4(g, m_Left, m_yp[32], m_Right, m_Bottom);
            PrintTxt_A4(g, m_Left, m_yp[32], m_Right, m_yp[33], "위와 같이 심사평가원의 처분에 대하여 재심사조정을 청구합니다.", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[34], m_Right, m_yp[35], "", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[35], m_Right, m_yp[36], "신청인  :                   (서명 또는 인)", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[36], m_Right, m_yp[37], "주  소  : ", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[37], m_Right, m_yp[38], "전화번호: ", LRC.Left);
            PrintTxt_A4(g, m_xp[1], m_yp[38], m_Right, m_Bottom, "건강보험심사평가원장 귀하", 12, false, FontKind.궁서체, LRC.Left);
        }

        private void Print_B(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float halfgap = (m_yp[1] - m_yp[0]) / 2;

            PrintBox_A4(g, m_Left, m_Top, m_Right, m_Bottom, 2);
            
            PrintBox_A4(g, m_Left, m_Top, m_Right, m_yp[3]);
            PrintTxt_A4(g, m_Left, m_Top, m_Right, m_yp[3], "이 의 신 청 서", 14,  true);
            
            PrintBox_A4(g, m_xp[11], m_Top, m_Right, m_yp[1]);
            PrintTxt_A4(g, m_xp[11], m_Top, m_Right, m_yp[1], "처리기간");
            
            PrintBox_A4(g, m_xp[11], m_yp[1], m_Right, m_yp[2]);
            PrintTxt_A4(g, m_xp[11], m_yp[1], m_Right, m_yp[2], "60일");
            
            PrintBox_A4(g, m_Left, m_yp[3], m_xp[2], m_yp[5]);
            PrintTxt_A4(g, m_Left, m_yp[3], m_xp[2], m_yp[5], "문서번호");
            
            PrintBox_A4(g, m_xp[2], m_yp[3], m_xp[4], m_yp[5]);
            PrintTxt_A4(g, m_xp[2], m_yp[3], m_xp[4], m_yp[5], "");
            
            PrintBox_A4(g, m_xp[4], m_yp[3], m_xp[8], m_yp[5]);
            PrintTxt_A4(g, m_xp[4], m_yp[3], m_xp[8], m_yp[5], "진료분야");
            
            PrintBox_A4(g, m_xp[8], m_yp[3], m_Right, m_yp[5]);
            PrintTxt_A4(g, m_xp[8], m_yp[3], m_Right, m_yp[5], "");
            
            PrintBox_A4(g, m_Left, m_yp[5], m_xp[1], m_yp[9]);
            PrintTxt_A4(g, m_Left, m_yp[6], m_xp[1], m_yp[7], "요양");
            PrintTxt_A4(g, m_Left, m_yp[7], m_xp[1], m_yp[8], "기관");
            
            PrintBox_A4(g, m_xp[1], m_yp[5], m_xp[2], m_yp[7]);
            PrintTxt_A4(g, m_xp[1], m_yp[5], m_xp[2], m_yp[7], "명칭");
            
            PrintBox_A4(g, m_xp[2], m_yp[5], m_xp[4], m_yp[7]);
            PrintTxt_A4(g, m_xp[2], m_yp[5], m_xp[4], m_yp[7], "");
            
            PrintBox_A4(g, m_xp[1], m_yp[7], m_xp[2], m_yp[9]);
            PrintTxt_A4(g, m_xp[1], m_yp[7], m_xp[2], m_yp[9], "기호");
            
            PrintBox_A4(g, m_xp[2], m_yp[7], m_xp[4], m_yp[9]);
            PrintTxt_A4(g, m_xp[2], m_yp[7], m_xp[4], m_yp[9], "");
            
            PrintBox_A4(g, m_Left, m_yp[9], m_xp[2], m_yp[11]);
            PrintTxt_A4(g, m_Left, m_yp[9], m_xp[2], m_yp[10], "이의신청");
            PrintTxt_A4(g, m_Left, m_yp[10], m_xp[2], m_yp[11], "건수총계");
            
            PrintBox_A4(g, m_xp[2], m_yp[9], m_xp[4], m_yp[11]);
            PrintTxt_A4(g, m_xp[2], m_yp[9], m_xp[4], m_yp[11], "");
            
            PrintBox_A4(g, m_Left, m_yp[11], m_xp[2], m_yp[13]);
            PrintTxt_A4(g, m_Left, m_yp[11], m_xp[2], m_yp[12], "이의신청");
            PrintTxt_A4(g, m_Left, m_yp[12], m_xp[2], m_yp[13], "비용총액");
            
            PrintBox_A4(g, m_xp[2], m_yp[11], m_xp[4], m_yp[13]);
            PrintTxt_A4(g, m_xp[2], m_yp[11], m_xp[4], m_yp[13], "");
            
            PrintBox_A4(g, m_xp[4], m_yp[5], m_xp[6], m_yp[13]);
            PrintTxt_A4(g, m_xp[4], m_yp[7], m_xp[6], m_yp[8], "요양급여");
            PrintTxt_A4(g, m_xp[4], m_yp[8], m_xp[6], m_yp[9], "비용심사");
            PrintTxt_A4(g, m_xp[4], m_yp[9], m_xp[6], m_yp[10], "결과통보서");
            
            PrintBox_A4(g, m_xp[6], m_yp[5], m_xp[8], m_yp[7]);
            PrintTxt_A4(g, m_xp[6], m_yp[5], m_xp[8], m_yp[7], "접수번호");
            
            PrintBox_A4(g, m_xp[8], m_yp[5], m_xp[9], m_yp[7]);
            PrintTxt_A4(g, m_xp[8], m_yp[5], m_xp[9], m_yp[7], "");
            
            PrintBox_A4(g, m_xp[6], m_yp[7], m_xp[8], m_yp[9]);
            PrintTxt_A4(g, m_xp[6], m_yp[7], m_xp[8], m_yp[9], "묶음번호");
            
            PrintBox_A4(g, m_xp[8], m_yp[7], m_xp[9], m_yp[9]);
            PrintTxt_A4(g, m_xp[8], m_yp[7], m_xp[9], m_yp[9], "");
            
            PrintBox_A4(g, m_xp[6], m_yp[9], m_xp[8], m_yp[11]);
            PrintTxt_A4(g, m_xp[6], m_yp[9], m_xp[8], m_yp[11], "심사차수");
            
            PrintBox_A4(g, m_xp[8], m_yp[9], m_xp[9], m_yp[11]);
            PrintTxt_A4(g, m_xp[8], m_yp[9], m_xp[9], m_yp[11], "");

            PrintBox_A4(g, m_xp[6], m_yp[11], m_xp[8], m_yp[13]);
            PrintTxt_A4(g, m_xp[6], m_yp[11], m_xp[8], m_yp[12], "통 보 서");
            PrintTxt_A4(g, m_xp[6], m_yp[12], m_xp[8], m_yp[13], "도달일자");
            
            PrintBox_A4(g, m_xp[8], m_yp[11], m_xp[9], m_yp[13]);
            PrintTxt_A4(g, m_xp[8], m_yp[11], m_xp[9], m_yp[13], "");
            
            PrintBox_A4(g, m_xp[9], m_yp[5], m_xp[10], m_yp[7]);
            PrintTxt_A4(g, m_xp[9], m_yp[5], m_xp[10], m_yp[6], "분");
            PrintTxt_A4(g, m_xp[9], m_yp[6], m_xp[10], m_yp[7], "류");
            
            PrintBox_A4(g, m_xp[10], m_yp[5], m_Right, m_yp[7]);
            PrintTxt_A4(g, m_xp[10], m_yp[5], m_Right, m_yp[6], "1.단순심사", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[6], m_Right, m_yp[7], "2.의학적심사", LRC.Left);
            
            PrintBox_A4(g, m_xp[9], m_yp[7], m_xp[10], m_yp[13]);
            PrintTxt_A4(g, m_xp[9], m_yp[8], m_xp[10], m_yp[9], "첨");
            PrintTxt_A4(g, m_xp[9], m_yp[9], m_xp[10], m_yp[10], "부");
            PrintTxt_A4(g, m_xp[9], m_yp[10], m_xp[10], m_yp[11], "서");
            PrintTxt_A4(g, m_xp[9], m_yp[11], m_xp[10], m_yp[12], "류");

            PrintBox_A4(g, m_xp[10], m_yp[7], m_Right, m_yp[13]);
            PrintTxt_A4(g, m_xp[10], m_yp[7] + halfgap, m_Right, m_yp[8] + halfgap, "1.심사결과통보서", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[8] + halfgap, m_Right, m_yp[9] + halfgap, "2.진료기록부", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[9] + halfgap, m_Right, m_yp[10] + halfgap, "3.X-ray film", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[10] + halfgap, m_Right, m_yp[11] + halfgap, "4.검사결과지", LRC.Left);
            PrintTxt_A4(g, m_xp[10], m_yp[11] + halfgap, m_Right, m_yp[12] + halfgap, "5.기타", LRC.Left);
            
            PrintBox_A4(g, m_Left, m_yp[13], m_xp[1], m_yp[15]);
            PrintTxt_A4(g, m_Left, m_yp[13], m_xp[1], m_yp[14], "순");
            PrintTxt_A4(g, m_Left, m_yp[14], m_xp[1], m_yp[15], "번");

            PrintBox_A4(g, m_xp[1], m_yp[13], m_xp[2], m_yp[15]);
            PrintTxt_A4(g, m_xp[1], m_yp[13], m_xp[2], m_yp[14], "명세서");
            PrintTxt_A4(g, m_xp[1], m_yp[14], m_xp[2], m_yp[15], "일련번호");

            PrintBox_A4(g, m_xp[2], m_yp[13], m_xp[3], m_yp[15]);
            PrintTxt_A4(g, m_xp[2], m_yp[13], m_xp[3], m_yp[15], "수진자");

            PrintBox_A4(g, m_xp[3], m_yp[13], m_xp[4], m_yp[15]);
            PrintTxt_A4(g, m_xp[3], m_yp[13], m_xp[4], m_yp[14], "진료구분");
            PrintTxt_A4(g, m_xp[3], m_yp[14], m_xp[4], m_yp[15], "(입원.외래)", 9);
            
            PrintBox_A4(g, m_xp[4], m_yp[13], m_xp[7], m_yp[14]);
            PrintTxt_A4(g, m_xp[4], m_yp[13], m_xp[7], m_yp[14], "이의신청금액");
            
            PrintBox_A4(g, m_xp[4], m_yp[14], m_xp[5], m_yp[15]);
            PrintTxt_A4(g, m_xp[4], m_yp[14], m_xp[5], m_yp[15], "I항");
            
            PrintBox_A4(g, m_xp[5], m_yp[14], m_xp[7], m_yp[15]);
            PrintTxt_A4(g, m_xp[5], m_yp[14], m_xp[7], m_yp[15], "II항");
            
            PrintBox_A4(g, m_xp[7], m_yp[13], m_xp[12], m_yp[15]);
            PrintTxt_A4(g, m_xp[7], m_yp[13], m_xp[12], m_yp[14], "이의신청 사유 및 내역");
            PrintTxt_A4(g, m_xp[7], m_yp[14], m_xp[12], m_yp[15], "(상세히 기술)");
            
            PrintBox_A4(g, m_xp[12], m_yp[13], m_Right, m_yp[15]);
            PrintTxt_A4(g, m_xp[12], m_yp[13], m_Right, m_yp[14], "첨부");
            PrintTxt_A4(g, m_xp[12], m_yp[14], m_Right, m_yp[15], "서류");
            
            PrintBox_A4(g, m_Left, m_yp[15], m_xp[1], m_yp[32]);
            PrintBox_A4(g, m_xp[1], m_yp[15], m_xp[2], m_yp[32]);
            PrintBox_A4(g, m_xp[2], m_yp[15], m_xp[3], m_yp[32]);
            PrintBox_A4(g, m_xp[3], m_yp[15], m_xp[4], m_yp[32]);
            PrintBox_A4(g, m_xp[4], m_yp[15], m_xp[4], m_yp[32]);
            PrintBox_A4(g, m_xp[5], m_yp[15], m_xp[7], m_yp[32]);
            PrintBox_A4(g, m_xp[7], m_yp[15], m_xp[12], m_yp[32]);
            
            
            PrintBox_A4(g, m_Left, m_yp[32], m_Right, m_Bottom);
            PrintTxt_A4(g, m_Left, m_yp[32], m_Right, m_yp[33], "국민건강보호법 제76조 제2항 및 동법 시행규칙 제43조의 규정에 의하여 위와 같이 심사평가원의", LRC.Left);
            PrintTxt_A4(g, m_Left, m_yp[33], m_Right, m_yp[34], "처분에 대하여 이의신청합니다.", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[34], m_Right, m_yp[35], "", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[35], m_Right, m_yp[36], "신청인  :                   (서명 또는 인)", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[36], m_Right, m_yp[37], "주  소  : ", LRC.Left);
            PrintTxt_A4(g, m_xp[5], m_yp[37], m_Right, m_yp[38], "전화번호: ", LRC.Left);
            PrintTxt_A4(g, m_xp[1], m_yp[38], m_Right, m_Bottom, "건강보험심사평가원장 귀하", 12, false, FontKind.궁서체, 0);
        }

        private void SetXYPos()
        {
            float xgap;
            float ygap;
            int loopCount;

            loopCount = 40;
            /*    
            m_Left = 500;
            m_Top = 1300;
            m_Right = 10800;
            m_Bottom = 15000;
            */
            // width 826, height 1169
            m_Left = 30;
            m_Top = 100;
            m_Right = 756;
            m_Bottom = 1069;

            xgap = (m_Right - m_Left) / 51; //'굴림체로 1글자 들어가는 크기
            ygap = (m_Bottom - m_Top) / loopCount;

            for (int i = 0; i <= loopCount; i++)
            {
                m_yp[i] = m_Top + (i * ygap);
            }

            m_xp[0] = m_Left;
            m_xp[1] = m_xp[0] + xgap * 3;           // 순번
            m_xp[2] = m_xp[1] + xgap * 4;           // 명세서일련번호
            m_xp[3] = m_xp[2] + xgap * (float)3.5;  // 수진자
            m_xp[4] = m_xp[3] + xgap * 5;           // 진료구분
            m_xp[5] = m_xp[4] + xgap * 5;
            m_xp[6] = m_xp[5] + xgap * 1;
            m_xp[7] = m_xp[6] + xgap * 4;
            m_xp[8] = m_xp[7] + xgap * 1;
            m_xp[9] = m_xp[8] + xgap * (float)11.5;
            m_xp[10] = m_xp[9] + xgap * 2;
            m_xp[11] = m_xp[10] + xgap * 5;
            m_xp[12] = m_xp[11] + xgap * 4;
            m_xp[13] = m_Right;

        }

        private void PrintBox_A4(Graphics g, float X1, float Y1, float X2, float Y2)
        {
            PrintBox_A4(g, X1, Y1, X2, Y2, 1);
        }

        private void PrintBox_A4(Graphics g, float X1, float Y1, float X2, float Y2, int vWidth){
            Pen pen = new Pen(Color.Black, vWidth);
            PointF[] points = new PointF[5];
            points[0] = new PointF(X1, Y1);
            points[1] = new PointF(X2, Y1);
            points[2] = new PointF(X2, Y2);
            points[3] = new PointF(X1, Y2);
            points[4] = new PointF(X1, Y1);
            g.DrawLines(pen, points);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData)
        {
            PrintTxt_A4(g, X1, Y1, X2, Y2, vData, 10);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData, LRC vLRC)
        {
            PrintTxt_A4(g, X1, Y1, X2, Y2, vData, 10, false, FontKind.굴림체, vLRC);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData, float vFSize)
        {
            PrintTxt_A4(g, X1, Y1, X2, Y2, vData, vFSize, false);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData, float vFSize, bool vFBold)
        {
            PrintTxt_A4(g, X1, Y1, X2, Y2, vData, vFSize, vFBold, FontKind.굴림체);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData, float vFSize, bool vFBold, FontKind vFKind)
        {
            PrintTxt_A4(g, X1, Y1, X2, Y2, vData, vFSize, vFBold, vFKind, LRC.Center);
        }

        private void PrintTxt_A4(Graphics g, float X1, float Y1, float X2, float Y2, string vData, float vFSize, bool vFBold, FontKind vFKind, LRC vLRC){
            float th;
            float tw;
            float X;
            float Y;

            string vFName = "굴림체";
            if (vFKind == FontKind.궁서체)
            {
                vFName = "궁서체";
            }

            Font font;
            if (vFBold == true)
            {
                font = new Font(vFName, vFSize, FontStyle.Bold);
            }
            else
            {
                font = new Font(vFName, vFSize);
            }
            SizeF s = g.MeasureString(vData,font);

            if (vLRC == LRC.Center) {
                // center
                th = s.Height;
                Y = ((Y2 - Y1) - th) / 2;;
                
                tw = s.Width;
                X = ((X2 - X1) - tw) / 2;
            }
            else if (vLRC == LRC.Right)
            {
                // right
                th = s.Height;
                Y = ((Y2 - Y1) - th) / 2;

                tw = s.Width;
                X = ((X2 - X1) - tw) - 30;
            }
            else
            {
                // left
                th = s.Height;
                Y = ((Y2 - Y1) - th) / 2;

                X = 30;
            }
            
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(vData, font, drawBrush, X1 + X, Y1 + Y);
        }

    }
}

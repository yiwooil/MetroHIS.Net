using System;
using System.Windows.Forms;
using System.Drawing;

namespace FJIN0930E
{
    partial class FJIN0930E
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            this.Text = "당뇨병환자 소모성재료 처방전";
            this.ClientSize = new System.Drawing.Size(1255, 982);
            this.KeyPreview = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // ssDX
            this.ssDX = new System.Windows.Forms.Panel();
            this.ssDX.Name = "ssDX";
            this.ssDX.Location = new System.Drawing.Point(884, 422);
            this.ssDX.Size = new System.Drawing.Size(639, 221);
            this.ssDX.TabIndex = 63;
            this.ssDX.Visible = false;

            // pic_TOP
            this.pic_TOP = new System.Windows.Forms.Panel();
            this.pic_TOP.Name = "pic_TOP";
            this.pic_TOP.Location = new System.Drawing.Point(3, 2);
            this.pic_TOP.Size = new System.Drawing.Size(582, 63);
            this.pic_TOP.TabIndex = 0;

            // txtHP
            this.txtHP = new System.Windows.Forms.TextBox();
            this.txtHP.Name = "txtHP";
            this.txtHP.Location = new System.Drawing.Point(413, 26);
            this.txtHP.Size = new System.Drawing.Size(97, 18);
            this.txtHP.TabIndex = 61;

            // cmdIns
            this.cmdIns = new System.Windows.Forms.Button();
            this.cmdIns.Name = "cmdIns";
            this.cmdIns.Location = new System.Drawing.Point(350, 2);
            this.cmdIns.Size = new System.Drawing.Size(68, 20);
            this.cmdIns.TabIndex = 33;
            this.cmdIns.Text = "등록/수정(F5)";

            // cmdQry
            this.cmdQry = new System.Windows.Forms.Button();
            this.cmdQry.Name = "cmdQry";
            this.cmdQry.Location = new System.Drawing.Point(281, 2);
            this.cmdQry.Size = new System.Drawing.Size(68, 20);
            this.cmdQry.TabIndex = 32;
            this.cmdQry.Text = "조회(F7)";

            // cmdExit
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Location = new System.Drawing.Point(486, 2);
            this.cmdExit.Size = new System.Drawing.Size(68, 20);
            this.cmdExit.TabIndex = 31;
            this.cmdExit.Text = "종료(F3)";

            // cmdPrt
            this.cmdPrt = new System.Windows.Forms.Button();
            this.cmdPrt.Name = "cmdPrt";
            this.cmdPrt.Location = new System.Drawing.Point(417, 2);
            this.cmdPrt.Size = new System.Drawing.Size(68, 20);
            this.cmdPrt.TabIndex = 30;
            this.cmdPrt.Text = "출력(F8)";

            // cmdDel
            this.cmdDel = new System.Windows.Forms.Button();
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Location = new System.Drawing.Point(212, 2);
            this.cmdDel.Size = new System.Drawing.Size(68, 20);
            this.cmdDel.TabIndex = 29;
            this.cmdDel.Text = "삭제(F4)";

            // lblResid
            this.lblResid = new System.Windows.Forms.TextBox();
            this.lblResid.Name = "lblResid";
            this.lblResid.Location = new System.Drawing.Point(58, 43);
            this.lblResid.Size = new System.Drawing.Size(102, 18);
            this.lblResid.TabIndex = 28;

            // txtPid
            this.txtPid = new System.Windows.Forms.TextBox();
            this.txtPid.Name = "txtPid";
            this.txtPid.Location = new System.Drawing.Point(58, 27);
            this.txtPid.Size = new System.Drawing.Size(102, 18);
            this.txtPid.TabIndex = 27;

            // txtAddr
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Location = new System.Drawing.Point(219, 43);
            this.txtAddr.Size = new System.Drawing.Size(189, 18);
            this.txtAddr.TabIndex = 26;

            // txtBthdt
            this.txtBthdt = new System.Windows.Forms.TextBox();
            this.txtBthdt.Name = "txtBthdt";
            this.txtBthdt.Location = new System.Drawing.Point(219, 27);
            this.txtBthdt.Size = new System.Drawing.Size(50, 18);
            this.txtBthdt.TabIndex = 25;

            // txtAge
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtAge.Name = "txtAge";
            this.txtAge.Location = new System.Drawing.Point(301, 27);
            this.txtAge.Size = new System.Drawing.Size(31, 18);
            this.txtAge.TabIndex = 24;

            // lblPnm
            this.lblPnm = new System.Windows.Forms.TextBox();
            this.lblPnm.Name = "lblPnm";
            this.lblPnm.Location = new System.Drawing.Point(58, 11);
            this.lblPnm.Size = new System.Drawing.Size(102, 18);
            this.lblPnm.TabIndex = 23;

            // txt_TEL
            this.txt_TEL = new System.Windows.Forms.TextBox();
            this.txt_TEL.Name = "txt_TEL";
            this.txt_TEL.Location = new System.Drawing.Point(413, 43);
            this.txt_TEL.Size = new System.Drawing.Size(97, 18);
            this.txt_TEL.TabIndex = 22;

            // lblRptseq
            this.lblRptseq = new System.Windows.Forms.Label();
            this.lblRptseq.Name = "lblRptseq";
            this.lblRptseq.Location = new System.Drawing.Point(519, 27);
            this.lblRptseq.Size = new System.Drawing.Size(53, 18);
            this.lblRptseq.TabIndex = 62;
            this.lblRptseq.Text = "";

            // lbl_CAPTION
            this.lbl_CAPTION = new System.Windows.Forms.Label();
            this.lbl_CAPTION.Name = "lbl_CAPTION";
            this.lbl_CAPTION.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION.TabIndex = 0;

            // lblPsex
            this.lblPsex = new System.Windows.Forms.Label();
            this.lblPsex.Name = "lblPsex";
            this.lblPsex.Location = new System.Drawing.Point(329, 17);
            this.lblPsex.Size = new System.Drawing.Size(77, 19);
            this.lblPsex.TabIndex = 42;

            // lbl_CAPTION_10
            this.lbl_CAPTION_10 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_10.Name = "lbl_CAPTION_10";
            this.lbl_CAPTION_10.Location = new System.Drawing.Point(558, 27);
            this.lbl_CAPTION_10.Size = new System.Drawing.Size(12, 12);
            this.lbl_CAPTION_10.TabIndex = 0;
            this.lbl_CAPTION_10.Text = "";

            // lbl_CAPTION_9
            this.lbl_CAPTION_9 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_9.Name = "lbl_CAPTION_9";
            this.lbl_CAPTION_9.Location = new System.Drawing.Point(558, 11);
            this.lbl_CAPTION_9.Size = new System.Drawing.Size(12, 12);
            this.lbl_CAPTION_9.TabIndex = 0;
            this.lbl_CAPTION_9.Text = "성명";

            // lbl_CAPTION_6
            this.lbl_CAPTION_6 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_6.Name = "lbl_CAPTION_6";
            this.lbl_CAPTION_6.Location = new System.Drawing.Point(173, 43);
            this.lbl_CAPTION_6.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_6.TabIndex = 0;
            this.lbl_CAPTION_6.Text = "주민번호";

            // lbl_CAPTION_5
            this.lbl_CAPTION_5 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_5.Name = "lbl_CAPTION_5";
            this.lbl_CAPTION_5.Location = new System.Drawing.Point(173, 27);
            this.lbl_CAPTION_5.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_5.TabIndex = 0;
            this.lbl_CAPTION_5.Text = "등록번호";

            // lbl_CAPTION_2
            this.lbl_CAPTION_2 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_2.Name = "lbl_CAPTION_2";
            this.lbl_CAPTION_2.Location = new System.Drawing.Point(8, 43);
            this.lbl_CAPTION_2.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_2.TabIndex = 0;
            this.lbl_CAPTION_2.Text = "주소";

            // lbl_CAPTION_1
            this.lbl_CAPTION_1 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_1.Name = "lbl_CAPTION_1";
            this.lbl_CAPTION_1.Location = new System.Drawing.Point(8, 27);
            this.lbl_CAPTION_1.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_1.TabIndex = 0;
            this.lbl_CAPTION_1.Text = "환자번호";

            // lbl_CAPTION_0
            this.lbl_CAPTION_0 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_0.Name = "lbl_CAPTION_0";
            this.lbl_CAPTION_0.Location = new System.Drawing.Point(8, 11);
            this.lbl_CAPTION_0.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_0.TabIndex = 0;
            this.lbl_CAPTION_0.Text = "성명";

            // Pic_DRLST
            this.Pic_DRLST = new System.Windows.Forms.Panel();
            this.Pic_DRLST.Name = "Pic_DRLST";
            this.Pic_DRLST.Location = new System.Drawing.Point(589, 2);
            this.Pic_DRLST.Size = new System.Drawing.Size(333, 147);
            this.Pic_DRLST.TabIndex = 1;
            this.Pic_DRLST.Visible = false;

            // ssDRLST
            this.ssDRLST = new System.Windows.Forms.Panel();
            this.ssDRLST.Name = "ssDRLST";
            this.ssDRLST.Location = new System.Drawing.Point(0, 0);
            this.ssDRLST.Size = new System.Drawing.Size(333, 147);
            this.ssDRLST.TabIndex = 0;

            // lblDRGET_0
            this.lblDRGET_0 = new System.Windows.Forms.Label();
            this.lblDRGET_0.Name = "lblDRGET_0";
            this.lblDRGET_0.Location = new System.Drawing.Point(0, 0);
            this.lblDRGET_0.Size = new System.Drawing.Size(0, 0);
            this.lblDRGET_0.TabIndex = 0;

            // Picture1
            this.Picture1 = new System.Windows.Forms.Panel();
            this.Picture1.Name = "Picture1";
            this.Picture1.Location = new System.Drawing.Point(3, 67);
            this.Picture1.Size = new System.Drawing.Size(582, 63);
            this.Picture1.TabIndex = 2;

            // lblDrid
            this.lblDrid = new System.Windows.Forms.TextBox();
            this.lblDrid.Name = "lblDrid";
            this.lblDrid.Location = new System.Drawing.Point(58, 11);
            this.lblDrid.Size = new System.Drawing.Size(79, 18);
            this.lblDrid.TabIndex = 0;

            // lblDrnm
            this.lblDrnm = new System.Windows.Forms.TextBox();
            this.lblDrnm.Name = "lblDrnm";
            this.lblDrnm.Location = new System.Drawing.Point(141, 11);
            this.lblDrnm.Size = new System.Drawing.Size(79, 18);
            this.lblDrnm.TabIndex = 0;

            // txtDptHidden
            this.txtDptHidden = new System.Windows.Forms.TextBox();
            this.txtDptHidden.Name = "txtDptHidden";
            this.txtDptHidden.Location = new System.Drawing.Point(58, 27);
            this.txtDptHidden.Size = new System.Drawing.Size(79, 18);
            this.txtDptHidden.TabIndex = 0;

            // txtLicense
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Location = new System.Drawing.Point(141, 27);
            this.txtLicense.Size = new System.Drawing.Size(79, 18);
            this.txtLicense.TabIndex = 0;

            // lbl_CAPTION_4
            this.lbl_CAPTION_4 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_4.Name = "lbl_CAPTION_4";
            this.lbl_CAPTION_4.Location = new System.Drawing.Point(8, 11);
            this.lbl_CAPTION_4.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_4.TabIndex = 0;
            this.lbl_CAPTION_4.Text = "의사코드";

            // lbl_CAPTION_7
            this.lbl_CAPTION_7 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_7.Name = "lbl_CAPTION_7";
            this.lbl_CAPTION_7.Location = new System.Drawing.Point(8, 27);
            this.lbl_CAPTION_7.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_7.TabIndex = 0;
            this.lbl_CAPTION_7.Text = "면허번호";

            // lbl_CAPTION_8
            this.lbl_CAPTION_8 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_8.Name = "lbl_CAPTION_8";
            this.lbl_CAPTION_8.Location = new System.Drawing.Point(222, 11);
            this.lbl_CAPTION_8.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_8.TabIndex = 0;
            this.lbl_CAPTION_8.Text = "과명";

            // lbl_CAPTION_11
            this.lbl_CAPTION_11 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_11.Name = "lbl_CAPTION_11";
            this.lbl_CAPTION_11.Location = new System.Drawing.Point(222, 27);
            this.lbl_CAPTION_11.Size = new System.Drawing.Size(45, 12);
            this.lbl_CAPTION_11.TabIndex = 0;
            this.lbl_CAPTION_11.Text = "면허종별";

            // lbl_CAPTION_16
            this.lbl_CAPTION_16 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_16.Name = "lbl_CAPTION_16";
            this.lbl_CAPTION_16.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_16.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_16.TabIndex = 0;

            // lbl_CAPTION_17
            this.lbl_CAPTION_17 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_17.Name = "lbl_CAPTION_17";
            this.lbl_CAPTION_17.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_17.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_17.TabIndex = 0;

            // lblGubun
            this.lblGubun = new System.Windows.Forms.Label();
            this.lblGubun.Name = "lblGubun";
            this.lblGubun.Location = new System.Drawing.Point(0, 0);
            this.lblGubun.Size = new System.Drawing.Size(0, 0);
            this.lblGubun.TabIndex = 0;

            // lbl_CAPTION_12
            this.lbl_CAPTION_12 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_12.Name = "lbl_CAPTION_12";
            this.lbl_CAPTION_12.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_12.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_12.TabIndex = 0;

            // lbl_CAPTION_13
            this.lbl_CAPTION_13 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_13.Name = "lbl_CAPTION_13";
            this.lbl_CAPTION_13.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_13.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_13.TabIndex = 0;

            // lbl_CAPTION_14
            this.lbl_CAPTION_14 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_14.Name = "lbl_CAPTION_14";
            this.lbl_CAPTION_14.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_14.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_14.TabIndex = 0;

            // lbl_CAPTION_15
            this.lbl_CAPTION_15 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_15.Name = "lbl_CAPTION_15";
            this.lbl_CAPTION_15.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_15.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_15.TabIndex = 0;

            // ssAmInfo
            this.ssAmInfo = new System.Windows.Forms.Panel();
            this.ssAmInfo.Name = "ssAmInfo";
            this.ssAmInfo.Location = new System.Drawing.Point(3, 131);
            this.ssAmInfo.Size = new System.Drawing.Size(582, 147);
            this.ssAmInfo.TabIndex = 3;

            // ssOP_DESC
            this.ssOP_DESC = new System.Windows.Forms.Panel();
            this.ssOP_DESC.Name = "ssOP_DESC";
            this.ssOP_DESC.Location = new System.Drawing.Point(3, 279);
            this.ssOP_DESC.Size = new System.Drawing.Size(582, 147);
            this.ssOP_DESC.TabIndex = 4;

            // ssRPT
            this.ssRPT = new System.Windows.Forms.Panel();
            this.ssRPT.Name = "ssRPT";
            this.ssRPT.Location = new System.Drawing.Point(3, 427);
            this.ssRPT.Size = new System.Drawing.Size(582, 147);
            this.ssRPT.TabIndex = 5;

            // pic_DETAIL
            this.pic_DETAIL = new System.Windows.Forms.Panel();
            this.pic_DETAIL.Name = "pic_DETAIL";
            this.pic_DETAIL.Location = new System.Drawing.Point(589, 151);
            this.pic_DETAIL.Size = new System.Drawing.Size(333, 423);
            this.pic_DETAIL.TabIndex = 6;

            // SSTab1
            this.SSTab1 = new System.Windows.Forms.TabControl();
            this.SSTab1.Name = "SSTab1";
            this.SSTab1.Location = new System.Drawing.Point(0, 0);
            this.SSTab1.Size = new System.Drawing.Size(333, 423);
            this.SSTab1.TabIndex = 0;

            // txtGuBun1
            this.txtGuBun1 = new System.Windows.Forms.TextBox();
            this.txtGuBun1.Name = "txtGuBun1";
            this.txtGuBun1.Location = new System.Drawing.Point(0, 0);
            this.txtGuBun1.Size = new System.Drawing.Size(0, 0);
            this.txtGuBun1.TabIndex = 0;

            // txtTongWonday
            this.txtTongWonday = new System.Windows.Forms.TextBox();
            this.txtTongWonday.Name = "txtTongWonday";
            this.txtTongWonday.Location = new System.Drawing.Point(0, 0);
            this.txtTongWonday.Size = new System.Drawing.Size(0, 0);
            this.txtTongWonday.TabIndex = 0;

            // txtDayCnt
            this.txtDayCnt = new System.Windows.Forms.TextBox();
            this.txtDayCnt.Name = "txtDayCnt";
            this.txtDayCnt.Location = new System.Drawing.Point(0, 0);
            this.txtDayCnt.Size = new System.Drawing.Size(0, 0);
            this.txtDayCnt.TabIndex = 0;

            // txtAvg_1
            this.txtAvg_1 = new System.Windows.Forms.TextBox();
            this.txtAvg_1.Name = "txtAvg_1";
            this.txtAvg_1.Location = new System.Drawing.Point(-4796, 158);
            this.txtAvg_1.Size = new System.Drawing.Size(31, 18);
            this.txtAvg_1.TabIndex = 102;

            // txtAvg_0
            this.txtAvg_0 = new System.Windows.Forms.TextBox();
            this.txtAvg_0.Name = "txtAvg_0";
            this.txtAvg_0.Location = new System.Drawing.Point(-4796, 124);
            this.txtAvg_0.Size = new System.Drawing.Size(31, 18);
            this.txtAvg_0.TabIndex = 101;

            // dtpPreFrom
            this.dtpPreFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpPreFrom.Name = "dtpPreFrom";
            this.dtpPreFrom.Location = new System.Drawing.Point(-4816, 90);
            this.dtpPreFrom.Size = new System.Drawing.Size(161, 21);
            this.dtpPreFrom.TabIndex = 94;

            // chkItem_0
            this.chkItem_0 = new System.Windows.Forms.CheckBox();
            this.chkItem_0.Name = "chkItem_0";
            this.chkItem_0.Location = new System.Drawing.Point(0, 0);
            this.chkItem_0.Size = new System.Drawing.Size(0, 0);
            this.chkItem_0.TabIndex = 0;

            // chkItem_1
            this.chkItem_1 = new System.Windows.Forms.CheckBox();
            this.chkItem_1.Name = "chkItem_1";
            this.chkItem_1.Location = new System.Drawing.Point(0, 0);
            this.chkItem_1.Size = new System.Drawing.Size(0, 0);
            this.chkItem_1.TabIndex = 0;

            // chkItem_2
            this.chkItem_2 = new System.Windows.Forms.CheckBox();
            this.chkItem_2.Name = "chkItem_2";
            this.chkItem_2.Location = new System.Drawing.Point(0, 0);
            this.chkItem_2.Size = new System.Drawing.Size(0, 0);
            this.chkItem_2.TabIndex = 0;

            // chkItem_3
            this.chkItem_3 = new System.Windows.Forms.CheckBox();
            this.chkItem_3.Name = "chkItem_3";
            this.chkItem_3.Location = new System.Drawing.Point(0, 0);
            this.chkItem_3.Size = new System.Drawing.Size(0, 0);
            this.chkItem_3.TabIndex = 0;

            // dtpImsin
            this.dtpImsin = new System.Windows.Forms.DateTimePicker();
            this.dtpImsin.Name = "dtpImsin";
            this.dtpImsin.Location = new System.Drawing.Point(0, 0);
            this.dtpImsin.Size = new System.Drawing.Size(0, 0);
            this.dtpImsin.TabIndex = 0;

            // chkImSin_2
            this.chkImSin_2 = new System.Windows.Forms.CheckBox();
            this.chkImSin_2.Name = "chkImSin_2";
            this.chkImSin_2.Location = new System.Drawing.Point(0, 0);
            this.chkImSin_2.Size = new System.Drawing.Size(0, 0);
            this.chkImSin_2.TabIndex = 0;

            // chkImSin_1
            this.chkImSin_1 = new System.Windows.Forms.CheckBox();
            this.chkImSin_1.Name = "chkImSin_1";
            this.chkImSin_1.Location = new System.Drawing.Point(0, 0);
            this.chkImSin_1.Size = new System.Drawing.Size(0, 0);
            this.chkImSin_1.TabIndex = 0;

            // chkGuBun2_0
            this.chkGuBun2_0 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_0.Name = "chkGuBun2_0";
            this.chkGuBun2_0.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_0.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_0.TabIndex = 0;

            // chkGuBun2_1
            this.chkGuBun2_1 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_1.Name = "chkGuBun2_1";
            this.chkGuBun2_1.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_1.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_1.TabIndex = 0;

            // chkGuBun2_2
            this.chkGuBun2_2 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_2.Name = "chkGuBun2_2";
            this.chkGuBun2_2.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_2.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_2.TabIndex = 0;

            // chkGuBun2_3
            this.chkGuBun2_3 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_3.Name = "chkGuBun2_3";
            this.chkGuBun2_3.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_3.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_3.TabIndex = 0;

            // chkGuBun2_4
            this.chkGuBun2_4 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_4.Name = "chkGuBun2_4";
            this.chkGuBun2_4.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_4.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_4.TabIndex = 0;

            // chkGuBun2_5
            this.chkGuBun2_5 = new System.Windows.Forms.CheckBox();
            this.chkGuBun2_5.Name = "chkGuBun2_5";
            this.chkGuBun2_5.Location = new System.Drawing.Point(0, 0);
            this.chkGuBun2_5.Size = new System.Drawing.Size(0, 0);
            this.chkGuBun2_5.TabIndex = 0;

            // lbl_CAPTION_18
            this.lbl_CAPTION_18 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_18.Name = "lbl_CAPTION_18";
            this.lbl_CAPTION_18.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_18.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_18.TabIndex = 0;

            // lbl_CAPTION_19
            this.lbl_CAPTION_19 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_19.Name = "lbl_CAPTION_19";
            this.lbl_CAPTION_19.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_19.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_19.TabIndex = 0;

            // lbl_CAPTION_20
            this.lbl_CAPTION_20 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_20.Name = "lbl_CAPTION_20";
            this.lbl_CAPTION_20.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_20.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_20.TabIndex = 0;

            // lbl_CAPTION_21
            this.lbl_CAPTION_21 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_21.Name = "lbl_CAPTION_21";
            this.lbl_CAPTION_21.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_21.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_21.TabIndex = 0;

            // lbl_CAPTION_22
            this.lbl_CAPTION_22 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_22.Name = "lbl_CAPTION_22";
            this.lbl_CAPTION_22.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_22.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_22.TabIndex = 0;

            // lbl_CAPTION_23
            this.lbl_CAPTION_23 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_23.Name = "lbl_CAPTION_23";
            this.lbl_CAPTION_23.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_23.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_23.TabIndex = 0;

            // lbl_CAPTION_24
            this.lbl_CAPTION_24 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_24.Name = "lbl_CAPTION_24";
            this.lbl_CAPTION_24.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_24.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_24.TabIndex = 0;

            // lbl_CAPTION_25
            this.lbl_CAPTION_25 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_25.Name = "lbl_CAPTION_25";
            this.lbl_CAPTION_25.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_25.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_25.TabIndex = 0;

            // lbl_CAPTION_26
            this.lbl_CAPTION_26 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_26.Name = "lbl_CAPTION_26";
            this.lbl_CAPTION_26.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_26.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_26.TabIndex = 0;

            // lbl_CAPTION_27
            this.lbl_CAPTION_27 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_27.Name = "lbl_CAPTION_27";
            this.lbl_CAPTION_27.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_27.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_27.TabIndex = 0;

            // Label32
            this.Label32 = new System.Windows.Forms.Label();
            this.Label32.Name = "Label32";
            this.Label32.Location = new System.Drawing.Point(0, 0);
            this.Label32.Size = new System.Drawing.Size(0, 0);
            this.Label32.TabIndex = 0;

            // Label30
            this.Label30 = new System.Windows.Forms.Label();
            this.Label30.Name = "Label30";
            this.Label30.Location = new System.Drawing.Point(0, 0);
            this.Label30.Size = new System.Drawing.Size(0, 0);
            this.Label30.TabIndex = 0;

            // Label29
            this.Label29 = new System.Windows.Forms.Label();
            this.Label29.Name = "Label29";
            this.Label29.Location = new System.Drawing.Point(0, 0);
            this.Label29.Size = new System.Drawing.Size(0, 0);
            this.Label29.TabIndex = 0;

            // Label18
            this.Label18 = new System.Windows.Forms.Label();
            this.Label18.Name = "Label18";
            this.Label18.Location = new System.Drawing.Point(0, 0);
            this.Label18.Size = new System.Drawing.Size(0, 0);
            this.Label18.TabIndex = 0;

            // Label1_3
            this.Label1_3 = new System.Windows.Forms.Label();
            this.Label1_3.Name = "Label1_3";
            this.Label1_3.Location = new System.Drawing.Point(0, 0);
            this.Label1_3.Size = new System.Drawing.Size(0, 0);
            this.Label1_3.TabIndex = 0;

            // Label1_2
            this.Label1_2 = new System.Windows.Forms.Label();
            this.Label1_2.Name = "Label1_2";
            this.Label1_2.Location = new System.Drawing.Point(0, 0);
            this.Label1_2.Size = new System.Drawing.Size(0, 0);
            this.Label1_2.TabIndex = 0;

            // Label1_1
            this.Label1_1 = new System.Windows.Forms.Label();
            this.Label1_1.Name = "Label1_1";
            this.Label1_1.Location = new System.Drawing.Point(0, 0);
            this.Label1_1.Size = new System.Drawing.Size(0, 0);
            this.Label1_1.TabIndex = 0;

            // Label1_0
            this.Label1_0 = new System.Windows.Forms.Label();
            this.Label1_0.Name = "Label1_0";
            this.Label1_0.Location = new System.Drawing.Point(0, 0);
            this.Label1_0.Size = new System.Drawing.Size(0, 0);
            this.Label1_0.TabIndex = 0;

            // cbo_DPTCD
            this.cbo_DPTCD = new System.Windows.Forms.ComboBox();
            this.cbo_DPTCD.Name = "cbo_DPTCD";
            this.cbo_DPTCD.Location = new System.Drawing.Point(0, 0);
            this.cbo_DPTCD.Size = new System.Drawing.Size(0, 0);
            this.cbo_DPTCD.TabIndex = 0;

            // dtpRptdt
            this.dtpRptdt = new System.Windows.Forms.DateTimePicker();
            this.dtpRptdt.Name = "dtpRptdt";
            this.dtpRptdt.Location = new System.Drawing.Point(0, 0);
            this.dtpRptdt.Size = new System.Drawing.Size(0, 0);
            this.dtpRptdt.TabIndex = 0;

            // dtpPreTo
            this.dtpPreTo = new System.Windows.Forms.DateTimePicker();
            this.dtpPreTo.Name = "dtpPreTo";
            this.dtpPreTo.Location = new System.Drawing.Point(0, 0);
            this.dtpPreTo.Size = new System.Drawing.Size(0, 0);
            this.dtpPreTo.TabIndex = 0;

            // Line10
            this.Line10 = new System.Windows.Forms.Label();
            this.Line10.Name = "Line10";
            this.Line10.Location = new System.Drawing.Point(0, 0);
            this.Line10.Size = new System.Drawing.Size(0, 0);
            this.Line10.TabIndex = 0;

            // Line9_1
            this.Line9_1 = new System.Windows.Forms.Label();
            this.Line9_1.Name = "Line9_1";
            this.Line9_1.Location = new System.Drawing.Point(0, 0);
            this.Line9_1.Size = new System.Drawing.Size(0, 0);
            this.Line9_1.TabIndex = 0;

            // Line9_0
            this.Line9_0 = new System.Windows.Forms.Label();
            this.Line9_0.Name = "Line9_0";
            this.Line9_0.Location = new System.Drawing.Point(0, 0);
            this.Line9_0.Size = new System.Drawing.Size(0, 0);
            this.Line9_0.TabIndex = 0;

            // Line8
            this.Line8 = new System.Windows.Forms.Label();
            this.Line8.Name = "Line8";
            this.Line8.Location = new System.Drawing.Point(0, 0);
            this.Line8.Size = new System.Drawing.Size(0, 0);
            this.Line8.TabIndex = 0;

            // Line7
            this.Line7 = new System.Windows.Forms.Label();
            this.Line7.Name = "Line7";
            this.Line7.Location = new System.Drawing.Point(0, 0);
            this.Line7.Size = new System.Drawing.Size(0, 0);
            this.Line7.TabIndex = 0;

            // Line6_10
            this.Line6_10 = new System.Windows.Forms.Label();
            this.Line6_10.Name = "Line6_10";
            this.Line6_10.Location = new System.Drawing.Point(0, 0);
            this.Line6_10.Size = new System.Drawing.Size(0, 0);
            this.Line6_10.TabIndex = 0;

            // Line6_9
            this.Line6_9 = new System.Windows.Forms.Label();
            this.Line6_9.Name = "Line6_9";
            this.Line6_9.Location = new System.Drawing.Point(0, 0);
            this.Line6_9.Size = new System.Drawing.Size(0, 0);
            this.Line6_9.TabIndex = 0;

            // Line6_8
            this.Line6_8 = new System.Windows.Forms.Label();
            this.Line6_8.Name = "Line6_8";
            this.Line6_8.Location = new System.Drawing.Point(0, 0);
            this.Line6_8.Size = new System.Drawing.Size(0, 0);
            this.Line6_8.TabIndex = 0;

            // Line6_7
            this.Line6_7 = new System.Windows.Forms.Label();
            this.Line6_7.Name = "Line6_7";
            this.Line6_7.Location = new System.Drawing.Point(0, 0);
            this.Line6_7.Size = new System.Drawing.Size(0, 0);
            this.Line6_7.TabIndex = 0;

            // Line6_6
            this.Line6_6 = new System.Windows.Forms.Label();
            this.Line6_6.Name = "Line6_6";
            this.Line6_6.Location = new System.Drawing.Point(0, 0);
            this.Line6_6.Size = new System.Drawing.Size(0, 0);
            this.Line6_6.TabIndex = 0;

            // Line6_5
            this.Line6_5 = new System.Windows.Forms.Label();
            this.Line6_5.Name = "Line6_5";
            this.Line6_5.Location = new System.Drawing.Point(0, 0);
            this.Line6_5.Size = new System.Drawing.Size(0, 0);
            this.Line6_5.TabIndex = 0;

            // Line6_4
            this.Line6_4 = new System.Windows.Forms.Label();
            this.Line6_4.Name = "Line6_4";
            this.Line6_4.Location = new System.Drawing.Point(0, 0);
            this.Line6_4.Size = new System.Drawing.Size(0, 0);
            this.Line6_4.TabIndex = 0;

            // Line6_3
            this.Line6_3 = new System.Windows.Forms.Label();
            this.Line6_3.Name = "Line6_3";
            this.Line6_3.Location = new System.Drawing.Point(0, 0);
            this.Line6_3.Size = new System.Drawing.Size(0, 0);
            this.Line6_3.TabIndex = 0;

            // Line6_2
            this.Line6_2 = new System.Windows.Forms.Label();
            this.Line6_2.Name = "Line6_2";
            this.Line6_2.Location = new System.Drawing.Point(0, 0);
            this.Line6_2.Size = new System.Drawing.Size(0, 0);
            this.Line6_2.TabIndex = 0;

            // Line6_1
            this.Line6_1 = new System.Windows.Forms.Label();
            this.Line6_1.Name = "Line6_1";
            this.Line6_1.Location = new System.Drawing.Point(0, 0);
            this.Line6_1.Size = new System.Drawing.Size(0, 0);
            this.Line6_1.TabIndex = 0;

            // Line6_0
            this.Line6_0 = new System.Windows.Forms.Label();
            this.Line6_0.Name = "Line6_0";
            this.Line6_0.Location = new System.Drawing.Point(0, 0);
            this.Line6_0.Size = new System.Drawing.Size(0, 0);
            this.Line6_0.TabIndex = 0;

            // Line5
            this.Line5 = new System.Windows.Forms.Label();
            this.Line5.Name = "Line5";
            this.Line5.Location = new System.Drawing.Point(0, 0);
            this.Line5.Size = new System.Drawing.Size(0, 0);
            this.Line5.TabIndex = 0;

            // Line4_1
            this.Line4_1 = new System.Windows.Forms.Label();
            this.Line4_1.Name = "Line4_1";
            this.Line4_1.Location = new System.Drawing.Point(0, 0);
            this.Line4_1.Size = new System.Drawing.Size(0, 0);
            this.Line4_1.TabIndex = 0;

            // Line4_0
            this.Line4_0 = new System.Windows.Forms.Label();
            this.Line4_0.Name = "Line4_0";
            this.Line4_0.Location = new System.Drawing.Point(0, 0);
            this.Line4_0.Size = new System.Drawing.Size(0, 0);
            this.Line4_0.TabIndex = 0;

            // Line3_1
            this.Line3_1 = new System.Windows.Forms.Label();
            this.Line3_1.Name = "Line3_1";
            this.Line3_1.Location = new System.Drawing.Point(0, 0);
            this.Line3_1.Size = new System.Drawing.Size(0, 0);
            this.Line3_1.TabIndex = 0;

            // Line3_0
            this.Line3_0 = new System.Windows.Forms.Label();
            this.Line3_0.Name = "Line3_0";
            this.Line3_0.Location = new System.Drawing.Point(0, 0);
            this.Line3_0.Size = new System.Drawing.Size(0, 0);
            this.Line3_0.TabIndex = 0;

            // Line2_1
            this.Line2_1 = new System.Windows.Forms.Label();
            this.Line2_1.Name = "Line2_1";
            this.Line2_1.Location = new System.Drawing.Point(0, 0);
            this.Line2_1.Size = new System.Drawing.Size(0, 0);
            this.Line2_1.TabIndex = 0;

            // Line2_0
            this.Line2_0 = new System.Windows.Forms.Label();
            this.Line2_0.Name = "Line2_0";
            this.Line2_0.Location = new System.Drawing.Point(0, 0);
            this.Line2_0.Size = new System.Drawing.Size(0, 0);
            this.Line2_0.TabIndex = 0;

            // Line1
            this.Line1 = new System.Windows.Forms.Label();
            this.Line1.Name = "Line1";
            this.Line1.Location = new System.Drawing.Point(0, 0);
            this.Line1.Size = new System.Drawing.Size(0, 0);
            this.Line1.TabIndex = 0;

            // Frame2
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.Frame2.Name = "Frame2";
            this.Frame2.Location = new System.Drawing.Point(0, 0);
            this.Frame2.Size = new System.Drawing.Size(0, 0);
            this.Frame2.TabIndex = 0;

            // Frame1
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.Frame1.Name = "Frame1";
            this.Frame1.Location = new System.Drawing.Point(0, 0);
            this.Frame1.Size = new System.Drawing.Size(0, 0);
            this.Frame1.TabIndex = 0;

            // lbl1_0
            this.lbl1_0 = new System.Windows.Forms.Label();
            this.lbl1_0.Name = "lbl1_0";
            this.lbl1_0.Location = new System.Drawing.Point(0, 0);
            this.lbl1_0.Size = new System.Drawing.Size(0, 0);
            this.lbl1_0.TabIndex = 0;

            // lbl1_1
            this.lbl1_1 = new System.Windows.Forms.Label();
            this.lbl1_1.Name = "lbl1_1";
            this.lbl1_1.Location = new System.Drawing.Point(0, 0);
            this.lbl1_1.Size = new System.Drawing.Size(0, 0);
            this.lbl1_1.TabIndex = 0;

            // lbl1_2
            this.lbl1_2 = new System.Windows.Forms.Label();
            this.lbl1_2.Name = "lbl1_2";
            this.lbl1_2.Location = new System.Drawing.Point(0, 0);
            this.lbl1_2.Size = new System.Drawing.Size(0, 0);
            this.lbl1_2.TabIndex = 0;

            // lbl1_3
            this.lbl1_3 = new System.Windows.Forms.Label();
            this.lbl1_3.Name = "lbl1_3";
            this.lbl1_3.Location = new System.Drawing.Point(0, 0);
            this.lbl1_3.Size = new System.Drawing.Size(0, 0);
            this.lbl1_3.TabIndex = 0;

            // lbl_CAPTION_3
            this.lbl_CAPTION_3 = new System.Windows.Forms.Label();
            this.lbl_CAPTION_3.Name = "lbl_CAPTION_3";
            this.lbl_CAPTION_3.Location = new System.Drawing.Point(0, 0);
            this.lbl_CAPTION_3.Size = new System.Drawing.Size(0, 0);
            this.lbl_CAPTION_3.TabIndex = 0;

            // chkDxdFg
            this.chkDxdFg = new System.Windows.Forms.CheckBox();
            this.chkDxdFg.Name = "chkDxdFg";
            this.chkDxdFg.Location = new System.Drawing.Point(0, 0);
            this.chkDxdFg.Size = new System.Drawing.Size(0, 0);
            this.chkDxdFg.TabIndex = 0;

            // chkREUSE
            this.chkREUSE = new System.Windows.Forms.CheckBox();
            this.chkREUSE.Name = "chkREUSE";
            this.chkREUSE.Location = new System.Drawing.Point(0, 0);
            this.chkREUSE.Size = new System.Drawing.Size(0, 0);
            this.chkREUSE.TabIndex = 0;

            // txtQ_4
            this.txtQ_4 = new System.Windows.Forms.TextBox();
            this.txtQ_4.Name = "txtQ_4";
            this.txtQ_4.Location = new System.Drawing.Point(0, 0);
            this.txtQ_4.Size = new System.Drawing.Size(0, 0);
            this.txtQ_4.TabIndex = 0;

            // txtQ_5
            this.txtQ_5 = new System.Windows.Forms.TextBox();
            this.txtQ_5.Name = "txtQ_5";
            this.txtQ_5.Location = new System.Drawing.Point(0, 0);
            this.txtQ_5.Size = new System.Drawing.Size(0, 0);
            this.txtQ_5.TabIndex = 0;

            // txtQ_3
            this.txtQ_3 = new System.Windows.Forms.TextBox();
            this.txtQ_3.Name = "txtQ_3";
            this.txtQ_3.Location = new System.Drawing.Point(0, 0);
            this.txtQ_3.Size = new System.Drawing.Size(0, 0);
            this.txtQ_3.TabIndex = 0;

            // txtQ_2
            this.txtQ_2 = new System.Windows.Forms.TextBox();
            this.txtQ_2.Name = "txtQ_2";
            this.txtQ_2.Location = new System.Drawing.Point(0, 0);
            this.txtQ_2.Size = new System.Drawing.Size(0, 0);
            this.txtQ_2.TabIndex = 0;

            // txtQ_1
            this.txtQ_1 = new System.Windows.Forms.TextBox();
            this.txtQ_1.Name = "txtQ_1";
            this.txtQ_1.Location = new System.Drawing.Point(0, 0);
            this.txtQ_1.Size = new System.Drawing.Size(0, 0);
            this.txtQ_1.TabIndex = 0;

            // txtQ_0
            this.txtQ_0 = new System.Windows.Forms.TextBox();
            this.txtQ_0.Name = "txtQ_0";
            this.txtQ_0.Location = new System.Drawing.Point(0, 0);
            this.txtQ_0.Size = new System.Drawing.Size(0, 0);
            this.txtQ_0.TabIndex = 0;

            // Controls hierarchy
            this.pic_TOP.Controls.Add(this.txtHP);
            this.pic_TOP.Controls.Add(this.cmdIns);
            this.pic_TOP.Controls.Add(this.cmdQry);
            this.pic_TOP.Controls.Add(this.cmdExit);
            this.pic_TOP.Controls.Add(this.cmdPrt);
            this.pic_TOP.Controls.Add(this.cmdDel);
            this.pic_TOP.Controls.Add(this.lblResid);
            this.pic_TOP.Controls.Add(this.txtPid);
            this.pic_TOP.Controls.Add(this.txtAddr);
            this.pic_TOP.Controls.Add(this.txtBthdt);
            this.pic_TOP.Controls.Add(this.txtAge);
            this.pic_TOP.Controls.Add(this.lblPnm);
            this.pic_TOP.Controls.Add(this.txt_TEL);
            this.pic_TOP.Controls.Add(this.lblRptseq);
            this.pic_TOP.Controls.Add(this.lblPsex);

            this.Pic_DRLST.Controls.Add(this.ssDRLST);
            this.Picture1.Controls.Add(this.lblDrid);
            this.Picture1.Controls.Add(this.lblDrnm);
            this.Picture1.Controls.Add(this.txtDptHidden);
            this.Picture1.Controls.Add(this.txtLicense);

            this.Controls.Add(this.ssDX);
            this.Controls.Add(this.pic_TOP);
            this.Controls.Add(this.Pic_DRLST);
            this.Controls.Add(this.Picture1);
            this.Controls.Add(this.ssAmInfo);
            this.Controls.Add(this.ssOP_DESC);
            this.Controls.Add(this.ssRPT);
            this.Controls.Add(this.pic_DETAIL);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel ssDX;
        private System.Windows.Forms.Panel pic_TOP;
        private System.Windows.Forms.TextBox txtHP;
        private System.Windows.Forms.Button cmdIns;
        private System.Windows.Forms.Button cmdQry;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdPrt;
        private System.Windows.Forms.Button cmdDel;
        private System.Windows.Forms.TextBox lblResid;
        private System.Windows.Forms.TextBox txtPid;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.TextBox txtBthdt;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox lblPnm;
        private System.Windows.Forms.TextBox txt_TEL;
        private System.Windows.Forms.Label lblRptseq;
        private System.Windows.Forms.Label lbl_CAPTION;
        private System.Windows.Forms.Label lblPsex;
        private System.Windows.Forms.Label lbl_CAPTION_10;
        private System.Windows.Forms.Label lbl_CAPTION_9;
        private System.Windows.Forms.Label lbl_CAPTION_6;
        private System.Windows.Forms.Label lbl_CAPTION_5;
        private System.Windows.Forms.Label lbl_CAPTION_2;
        private System.Windows.Forms.Label lbl_CAPTION_1;
        private System.Windows.Forms.Label lbl_CAPTION_0;
        private System.Windows.Forms.Panel Pic_DRLST;
        private System.Windows.Forms.Panel ssDRLST;
        private System.Windows.Forms.Label lblDRGET_0;
        private System.Windows.Forms.Panel Picture1;
        private System.Windows.Forms.TextBox lblDrid;
        private System.Windows.Forms.TextBox lblDrnm;
        private System.Windows.Forms.TextBox txtDptHidden;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label lbl_CAPTION_4;
        private System.Windows.Forms.Label lbl_CAPTION_7;
        private System.Windows.Forms.Label lbl_CAPTION_8;
        private System.Windows.Forms.Label lbl_CAPTION_11;
        private System.Windows.Forms.Label lbl_CAPTION_16;
        private System.Windows.Forms.Label lbl_CAPTION_17;
        private System.Windows.Forms.Label lblGubun;
        private System.Windows.Forms.Label lbl_CAPTION_12;
        private System.Windows.Forms.Label lbl_CAPTION_13;
        private System.Windows.Forms.Label lbl_CAPTION_14;
        private System.Windows.Forms.Label lbl_CAPTION_15;
        private System.Windows.Forms.Panel ssAmInfo;
        private System.Windows.Forms.Panel ssOP_DESC;
        private System.Windows.Forms.Panel ssRPT;
        private System.Windows.Forms.Panel pic_DETAIL;
        private System.Windows.Forms.TabControl SSTab1;
        private System.Windows.Forms.TextBox txtGuBun1;
        private System.Windows.Forms.TextBox txtTongWonday;
        private System.Windows.Forms.TextBox txtDayCnt;
        private System.Windows.Forms.TextBox txtAvg_1;
        private System.Windows.Forms.TextBox txtAvg_0;
        private System.Windows.Forms.DateTimePicker dtpPreFrom;
        private System.Windows.Forms.CheckBox chkItem_0;
        private System.Windows.Forms.CheckBox chkItem_1;
        private System.Windows.Forms.CheckBox chkItem_2;
        private System.Windows.Forms.CheckBox chkItem_3;
        private System.Windows.Forms.DateTimePicker dtpImsin;
        private System.Windows.Forms.CheckBox chkImSin_2;
        private System.Windows.Forms.CheckBox chkImSin_1;
        private System.Windows.Forms.CheckBox chkGuBun2_0;
        private System.Windows.Forms.CheckBox chkGuBun2_1;
        private System.Windows.Forms.CheckBox chkGuBun2_2;
        private System.Windows.Forms.CheckBox chkGuBun2_3;
        private System.Windows.Forms.CheckBox chkGuBun2_4;
        private System.Windows.Forms.CheckBox chkGuBun2_5;
        private System.Windows.Forms.Label lbl_CAPTION_18;
        private System.Windows.Forms.Label lbl_CAPTION_19;
        private System.Windows.Forms.Label lbl_CAPTION_20;
        private System.Windows.Forms.Label lbl_CAPTION_21;
        private System.Windows.Forms.Label lbl_CAPTION_22;
        private System.Windows.Forms.Label lbl_CAPTION_23;
        private System.Windows.Forms.Label lbl_CAPTION_24;
        private System.Windows.Forms.Label lbl_CAPTION_25;
        private System.Windows.Forms.Label lbl_CAPTION_26;
        private System.Windows.Forms.Label lbl_CAPTION_27;
        private System.Windows.Forms.Label Label32;
        private System.Windows.Forms.Label Label30;
        private System.Windows.Forms.Label Label29;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label Label1_3;
        private System.Windows.Forms.Label Label1_2;
        private System.Windows.Forms.Label Label1_1;
        private System.Windows.Forms.Label Label1_0;
        private System.Windows.Forms.ComboBox cbo_DPTCD;
        private System.Windows.Forms.DateTimePicker dtpRptdt;
        private System.Windows.Forms.DateTimePicker dtpPreTo;
        private System.Windows.Forms.Label Line10;
        private System.Windows.Forms.Label Line9_1;
        private System.Windows.Forms.Label Line9_0;
        private System.Windows.Forms.Label Line8;
        private System.Windows.Forms.Label Line7;
        private System.Windows.Forms.Label Line6_10;
        private System.Windows.Forms.Label Line6_9;
        private System.Windows.Forms.Label Line6_8;
        private System.Windows.Forms.Label Line6_7;
        private System.Windows.Forms.Label Line6_6;
        private System.Windows.Forms.Label Line6_5;
        private System.Windows.Forms.Label Line6_4;
        private System.Windows.Forms.Label Line6_3;
        private System.Windows.Forms.Label Line6_2;
        private System.Windows.Forms.Label Line6_1;
        private System.Windows.Forms.Label Line6_0;
        private System.Windows.Forms.Label Line5;
        private System.Windows.Forms.Label Line4_1;
        private System.Windows.Forms.Label Line4_0;
        private System.Windows.Forms.Label Line3_1;
        private System.Windows.Forms.Label Line3_0;
        private System.Windows.Forms.Label Line2_1;
        private System.Windows.Forms.Label Line2_0;
        private System.Windows.Forms.Label Line1;
        private System.Windows.Forms.GroupBox Frame2;
        private System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.Label lbl1_0;
        private System.Windows.Forms.Label lbl1_1;
        private System.Windows.Forms.Label lbl1_2;
        private System.Windows.Forms.Label lbl1_3;
        private System.Windows.Forms.Label lbl_CAPTION_3;
        private System.Windows.Forms.CheckBox chkDxdFg;
        private System.Windows.Forms.CheckBox chkREUSE;
        private System.Windows.Forms.TextBox txtQ_4;
        private System.Windows.Forms.TextBox txtQ_5;
        private System.Windows.Forms.TextBox txtQ_3;
        private System.Windows.Forms.TextBox txtQ_2;
        private System.Windows.Forms.TextBox txtQ_1;
        private System.Windows.Forms.TextBox txtQ_0;
    }
}

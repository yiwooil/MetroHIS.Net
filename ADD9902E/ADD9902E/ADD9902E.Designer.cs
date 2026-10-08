namespace ADD9902E
{
    partial class ADD9902E
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.lblHospitalCode = new System.Windows.Forms.Label();
            this.txtHosId = new System.Windows.Forms.TextBox();
            this.txtHosId2 = new System.Windows.Forms.TextBox();
            this.lblHospitalName = new System.Windows.Forms.Label();
            this.txtHosNm = new System.Windows.Forms.TextBox();
            this.lblHospitalType = new System.Windows.Forms.Label();
            this.txtHosJB = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtHosAddr = new System.Windows.Forms.TextBox();
            this.lblRepresentative = new System.Windows.Forms.Label();
            this.txtHosCEO = new System.Windows.Forms.TextBox();
            this.txtHOSCEORID = new System.Windows.Forms.TextBox();
            this.lblWriter = new System.Windows.Forms.Label();
            this.txtWorkNM = new System.Windows.Forms.TextBox();
            this.txtWorkRID = new System.Windows.Forms.TextBox();
            this.lblIndustrialClaimant = new System.Windows.Forms.Label();
            this.txtDemNmSanje = new System.Windows.Forms.TextBox();
            this.lblInspection = new System.Windows.Forms.Label();
            this.lblInternalMedicine = new System.Windows.Forms.Label();
            this.txtOkDno = new System.Windows.Forms.TextBox();
            this.txtEDIVer = new System.Windows.Forms.TextBox();
            this.lblPsychiatry = new System.Windows.Forms.Label();
            this.txtOkDnoPHY = new System.Windows.Forms.TextBox();
            this.txtPHYVer = new System.Windows.Forms.TextBox();
            this.lblDrg = new System.Windows.Forms.Label();
            this.txtOkDnoDRG = new System.Windows.Forms.TextBox();
            this.txtDRGVer = new System.Windows.Forms.TextBox();
            this.lblHemodialysis = new System.Windows.Forms.Label();
            this.txtOkDnoBlood = new System.Windows.Forms.TextBox();
            this.txtBloodVer = new System.Windows.Forms.TextBox();
            this.lblDental = new System.Windows.Forms.Label();
            this.txtOkDnoDENT = new System.Windows.Forms.TextBox();
            this.txtDENTVer = new System.Windows.Forms.TextBox();
            this.lblOrientalMedicine = new System.Windows.Forms.Label();
            this.txtOkDnoHAN = new System.Windows.Forms.TextBox();
            this.txtHANVer = new System.Windows.Forms.TextBox();
            this.lblNursing = new System.Windows.Forms.Label();
            this.txtOkDnoYOYANG = new System.Windows.Forms.TextBox();
            this.txtYOYANGVer = new System.Windows.Forms.TextBox();
            this.lblRehabilitation = new System.Windows.Forms.Label();
            this.txtOkDnoPaCare = new System.Windows.Forms.TextBox();
            this.txtPaCareVer = new System.Windows.Forms.TextBox();
            this.lblClaimAgency = new System.Windows.Forms.Label();
            this.lblPrintVersion = new System.Windows.Forms.Label();
            this.cboPrtVer = new System.Windows.Forms.ComboBox();
            this.txtREDEM = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(791, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblHospitalCode
            // 
            this.lblHospitalCode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblHospitalCode.Location = new System.Drawing.Point(25, 59);
            this.lblHospitalCode.Name = "lblHospitalCode";
            this.lblHospitalCode.Size = new System.Drawing.Size(105, 23);
            this.lblHospitalCode.TabIndex = 4;
            this.lblHospitalCode.Text = "Ⅰ.의료기관코드";
            this.lblHospitalCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHosId
            // 
            this.txtHosId.Location = new System.Drawing.Point(132, 62);
            this.txtHosId.MaxLength = 8;
            this.txtHosId.Name = "txtHosId";
            this.txtHosId.Size = new System.Drawing.Size(80, 21);
            this.txtHosId.TabIndex = 5;
            // 
            // txtHosId2
            // 
            this.txtHosId2.Location = new System.Drawing.Point(215, 62);
            this.txtHosId2.MaxLength = 8;
            this.txtHosId2.Name = "txtHosId2";
            this.txtHosId2.Size = new System.Drawing.Size(81, 21);
            this.txtHosId2.TabIndex = 6;
            // 
            // lblHospitalName
            // 
            this.lblHospitalName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblHospitalName.Location = new System.Drawing.Point(25, 88);
            this.lblHospitalName.Name = "lblHospitalName";
            this.lblHospitalName.Size = new System.Drawing.Size(105, 23);
            this.lblHospitalName.TabIndex = 7;
            this.lblHospitalName.Text = "Ⅱ.의료기관명칭";
            this.lblHospitalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHosNm
            // 
            this.txtHosNm.Location = new System.Drawing.Point(132, 90);
            this.txtHosNm.Name = "txtHosNm";
            this.txtHosNm.Size = new System.Drawing.Size(164, 21);
            this.txtHosNm.TabIndex = 8;
            // 
            // lblHospitalType
            // 
            this.lblHospitalType.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblHospitalType.Location = new System.Drawing.Point(25, 116);
            this.lblHospitalType.Name = "lblHospitalType";
            this.lblHospitalType.Size = new System.Drawing.Size(105, 23);
            this.lblHospitalType.TabIndex = 9;
            this.lblHospitalType.Text = "Ⅲ.의료기관종별";
            this.lblHospitalType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHosJB
            // 
            this.txtHosJB.Location = new System.Drawing.Point(132, 117);
            this.txtHosJB.Name = "txtHosJB";
            this.txtHosJB.ReadOnly = true;
            this.txtHosJB.Size = new System.Drawing.Size(164, 21);
            this.txtHosJB.TabIndex = 10;
            this.txtHosJB.TabStop = false;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAddress.Location = new System.Drawing.Point(311, 63);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(103, 23);
            this.lblAddress.TabIndex = 11;
            this.lblAddress.Text = "Ⅳ.의료기관주소";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHosAddr
            // 
            this.txtHosAddr.Location = new System.Drawing.Point(414, 63);
            this.txtHosAddr.Name = "txtHosAddr";
            this.txtHosAddr.Size = new System.Drawing.Size(452, 21);
            this.txtHosAddr.TabIndex = 12;
            // 
            // lblRepresentative
            // 
            this.lblRepresentative.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRepresentative.Location = new System.Drawing.Point(311, 88);
            this.lblRepresentative.Name = "lblRepresentative";
            this.lblRepresentative.Size = new System.Drawing.Size(103, 23);
            this.lblRepresentative.TabIndex = 13;
            this.lblRepresentative.Text = "Ⅴ. 대  표  자";
            this.lblRepresentative.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHosCEO
            // 
            this.txtHosCEO.Location = new System.Drawing.Point(414, 90);
            this.txtHosCEO.Name = "txtHosCEO";
            this.txtHosCEO.Size = new System.Drawing.Size(76, 21);
            this.txtHosCEO.TabIndex = 14;
            // 
            // txtHOSCEORID
            // 
            this.txtHOSCEORID.Location = new System.Drawing.Point(493, 90);
            this.txtHOSCEORID.Name = "txtHOSCEORID";
            this.txtHOSCEORID.Size = new System.Drawing.Size(168, 21);
            this.txtHOSCEORID.TabIndex = 15;
            // 
            // lblWriter
            // 
            this.lblWriter.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWriter.Location = new System.Drawing.Point(311, 116);
            this.lblWriter.Name = "lblWriter";
            this.lblWriter.Size = new System.Drawing.Size(103, 23);
            this.lblWriter.TabIndex = 16;
            this.lblWriter.Text = "Ⅵ. 작  성  자";
            this.lblWriter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWorkNM
            // 
            this.txtWorkNM.Location = new System.Drawing.Point(414, 117);
            this.txtWorkNM.Name = "txtWorkNM";
            this.txtWorkNM.Size = new System.Drawing.Size(76, 21);
            this.txtWorkNM.TabIndex = 17;
            // 
            // txtWorkRID
            // 
            this.txtWorkRID.Location = new System.Drawing.Point(493, 117);
            this.txtWorkRID.Name = "txtWorkRID";
            this.txtWorkRID.Size = new System.Drawing.Size(168, 21);
            this.txtWorkRID.TabIndex = 18;
            // 
            // lblIndustrialClaimant
            // 
            this.lblIndustrialClaimant.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblIndustrialClaimant.Location = new System.Drawing.Point(678, 116);
            this.lblIndustrialClaimant.Name = "lblIndustrialClaimant";
            this.lblIndustrialClaimant.Size = new System.Drawing.Size(75, 23);
            this.lblIndustrialClaimant.TabIndex = 19;
            this.lblIndustrialClaimant.Text = "산재 청구인";
            this.lblIndustrialClaimant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDemNmSanje
            // 
            this.txtDemNmSanje.Location = new System.Drawing.Point(752, 117);
            this.txtDemNmSanje.Name = "txtDemNmSanje";
            this.txtDemNmSanje.Size = new System.Drawing.Size(114, 21);
            this.txtDemNmSanje.TabIndex = 20;
            // 
            // lblInspection
            // 
            this.lblInspection.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInspection.Location = new System.Drawing.Point(24, 154);
            this.lblInspection.Name = "lblInspection";
            this.lblInspection.Size = new System.Drawing.Size(106, 23);
            this.lblInspection.TabIndex = 21;
            this.lblInspection.Text = "Ⅵ.Soft 검수번호";
            this.lblInspection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInternalMedicine
            // 
            this.lblInternalMedicine.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInternalMedicine.Location = new System.Drawing.Point(64, 182);
            this.lblInternalMedicine.Name = "lblInternalMedicine";
            this.lblInternalMedicine.Size = new System.Drawing.Size(70, 23);
            this.lblInternalMedicine.TabIndex = 22;
            this.lblInternalMedicine.Text = "의과";
            this.lblInternalMedicine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDno
            // 
            this.txtOkDno.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDno.Location = new System.Drawing.Point(136, 184);
            this.txtOkDno.Name = "txtOkDno";
            this.txtOkDno.ReadOnly = true;
            this.txtOkDno.Size = new System.Drawing.Size(261, 21);
            this.txtOkDno.TabIndex = 23;
            this.txtOkDno.TabStop = false;
            this.txtOkDno.Text = "999999999999999999999999999999999999";
            this.txtOkDno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEDIVer
            // 
            this.txtEDIVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtEDIVer.Location = new System.Drawing.Point(398, 184);
            this.txtEDIVer.Name = "txtEDIVer";
            this.txtEDIVer.ReadOnly = true;
            this.txtEDIVer.Size = new System.Drawing.Size(55, 21);
            this.txtEDIVer.TabIndex = 24;
            this.txtEDIVer.TabStop = false;
            this.txtEDIVer.Text = "060";
            this.txtEDIVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPsychiatry
            // 
            this.lblPsychiatry.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPsychiatry.Location = new System.Drawing.Point(64, 204);
            this.lblPsychiatry.Name = "lblPsychiatry";
            this.lblPsychiatry.Size = new System.Drawing.Size(70, 23);
            this.lblPsychiatry.TabIndex = 25;
            this.lblPsychiatry.Text = "정신과";
            this.lblPsychiatry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoPHY
            // 
            this.txtOkDnoPHY.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoPHY.Location = new System.Drawing.Point(136, 206);
            this.txtOkDnoPHY.Name = "txtOkDnoPHY";
            this.txtOkDnoPHY.ReadOnly = true;
            this.txtOkDnoPHY.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoPHY.TabIndex = 26;
            this.txtOkDnoPHY.TabStop = false;
            this.txtOkDnoPHY.Text = "999999999999999999999999999999999999";
            this.txtOkDnoPHY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPHYVer
            // 
            this.txtPHYVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPHYVer.Location = new System.Drawing.Point(398, 206);
            this.txtPHYVer.Name = "txtPHYVer";
            this.txtPHYVer.ReadOnly = true;
            this.txtPHYVer.Size = new System.Drawing.Size(55, 21);
            this.txtPHYVer.TabIndex = 27;
            this.txtPHYVer.TabStop = false;
            this.txtPHYVer.Text = "060";
            this.txtPHYVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDrg
            // 
            this.lblDrg.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDrg.Location = new System.Drawing.Point(64, 226);
            this.lblDrg.Name = "lblDrg";
            this.lblDrg.Size = new System.Drawing.Size(70, 23);
            this.lblDrg.TabIndex = 28;
            this.lblDrg.Text = "DRG";
            this.lblDrg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoDRG
            // 
            this.txtOkDnoDRG.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoDRG.Location = new System.Drawing.Point(136, 228);
            this.txtOkDnoDRG.Name = "txtOkDnoDRG";
            this.txtOkDnoDRG.ReadOnly = true;
            this.txtOkDnoDRG.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoDRG.TabIndex = 29;
            this.txtOkDnoDRG.TabStop = false;
            this.txtOkDnoDRG.Text = "999999999999999999999999999999999999";
            this.txtOkDnoDRG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDRGVer
            // 
            this.txtDRGVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtDRGVer.Location = new System.Drawing.Point(398, 228);
            this.txtDRGVer.Name = "txtDRGVer";
            this.txtDRGVer.ReadOnly = true;
            this.txtDRGVer.Size = new System.Drawing.Size(55, 21);
            this.txtDRGVer.TabIndex = 30;
            this.txtDRGVer.TabStop = false;
            this.txtDRGVer.Text = "060";
            this.txtDRGVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHemodialysis
            // 
            this.lblHemodialysis.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblHemodialysis.Location = new System.Drawing.Point(64, 248);
            this.lblHemodialysis.Name = "lblHemodialysis";
            this.lblHemodialysis.Size = new System.Drawing.Size(70, 23);
            this.lblHemodialysis.TabIndex = 31;
            this.lblHemodialysis.Text = "혈액투석";
            this.lblHemodialysis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoBlood
            // 
            this.txtOkDnoBlood.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoBlood.Location = new System.Drawing.Point(136, 250);
            this.txtOkDnoBlood.Name = "txtOkDnoBlood";
            this.txtOkDnoBlood.ReadOnly = true;
            this.txtOkDnoBlood.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoBlood.TabIndex = 32;
            this.txtOkDnoBlood.TabStop = false;
            this.txtOkDnoBlood.Text = "999999999999999999999999999999999999";
            this.txtOkDnoBlood.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBloodVer
            // 
            this.txtBloodVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBloodVer.Location = new System.Drawing.Point(398, 250);
            this.txtBloodVer.Name = "txtBloodVer";
            this.txtBloodVer.ReadOnly = true;
            this.txtBloodVer.Size = new System.Drawing.Size(55, 21);
            this.txtBloodVer.TabIndex = 33;
            this.txtBloodVer.TabStop = false;
            this.txtBloodVer.Text = "060";
            this.txtBloodVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDental
            // 
            this.lblDental.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDental.Location = new System.Drawing.Point(481, 182);
            this.lblDental.Name = "lblDental";
            this.lblDental.Size = new System.Drawing.Size(70, 23);
            this.lblDental.TabIndex = 34;
            this.lblDental.Text = "치과";
            this.lblDental.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoDENT
            // 
            this.txtOkDnoDENT.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoDENT.Location = new System.Drawing.Point(549, 184);
            this.txtOkDnoDENT.Name = "txtOkDnoDENT";
            this.txtOkDnoDENT.ReadOnly = true;
            this.txtOkDnoDENT.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoDENT.TabIndex = 35;
            this.txtOkDnoDENT.TabStop = false;
            this.txtOkDnoDENT.Text = "999999999999999999999999999999999999";
            this.txtOkDnoDENT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDENTVer
            // 
            this.txtDENTVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtDENTVer.Location = new System.Drawing.Point(811, 184);
            this.txtDENTVer.Name = "txtDENTVer";
            this.txtDENTVer.ReadOnly = true;
            this.txtDENTVer.Size = new System.Drawing.Size(55, 21);
            this.txtDENTVer.TabIndex = 36;
            this.txtDENTVer.TabStop = false;
            this.txtDENTVer.Text = "060";
            this.txtDENTVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOrientalMedicine
            // 
            this.lblOrientalMedicine.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblOrientalMedicine.Location = new System.Drawing.Point(481, 204);
            this.lblOrientalMedicine.Name = "lblOrientalMedicine";
            this.lblOrientalMedicine.Size = new System.Drawing.Size(70, 23);
            this.lblOrientalMedicine.TabIndex = 37;
            this.lblOrientalMedicine.Text = "한방";
            this.lblOrientalMedicine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoHAN
            // 
            this.txtOkDnoHAN.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoHAN.Location = new System.Drawing.Point(549, 206);
            this.txtOkDnoHAN.Name = "txtOkDnoHAN";
            this.txtOkDnoHAN.ReadOnly = true;
            this.txtOkDnoHAN.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoHAN.TabIndex = 38;
            this.txtOkDnoHAN.TabStop = false;
            this.txtOkDnoHAN.Text = "999999999999999999999999999999999999";
            this.txtOkDnoHAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHANVer
            // 
            this.txtHANVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtHANVer.Location = new System.Drawing.Point(811, 206);
            this.txtHANVer.Name = "txtHANVer";
            this.txtHANVer.ReadOnly = true;
            this.txtHANVer.Size = new System.Drawing.Size(55, 21);
            this.txtHANVer.TabIndex = 39;
            this.txtHANVer.TabStop = false;
            this.txtHANVer.Text = "060";
            this.txtHANVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNursing
            // 
            this.lblNursing.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNursing.Location = new System.Drawing.Point(481, 226);
            this.lblNursing.Name = "lblNursing";
            this.lblNursing.Size = new System.Drawing.Size(70, 23);
            this.lblNursing.TabIndex = 40;
            this.lblNursing.Text = "요양";
            this.lblNursing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoYOYANG
            // 
            this.txtOkDnoYOYANG.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoYOYANG.Location = new System.Drawing.Point(549, 228);
            this.txtOkDnoYOYANG.Name = "txtOkDnoYOYANG";
            this.txtOkDnoYOYANG.ReadOnly = true;
            this.txtOkDnoYOYANG.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoYOYANG.TabIndex = 41;
            this.txtOkDnoYOYANG.TabStop = false;
            this.txtOkDnoYOYANG.Text = "999999999999999999999999999999999999";
            this.txtOkDnoYOYANG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtYOYANGVer
            // 
            this.txtYOYANGVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtYOYANGVer.Location = new System.Drawing.Point(811, 228);
            this.txtYOYANGVer.Name = "txtYOYANGVer";
            this.txtYOYANGVer.ReadOnly = true;
            this.txtYOYANGVer.Size = new System.Drawing.Size(55, 21);
            this.txtYOYANGVer.TabIndex = 42;
            this.txtYOYANGVer.TabStop = false;
            this.txtYOYANGVer.Text = "060";
            this.txtYOYANGVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRehabilitation
            // 
            this.lblRehabilitation.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRehabilitation.Location = new System.Drawing.Point(481, 248);
            this.lblRehabilitation.Name = "lblRehabilitation";
            this.lblRehabilitation.Size = new System.Drawing.Size(70, 23);
            this.lblRehabilitation.TabIndex = 43;
            this.lblRehabilitation.Text = "완화의료";
            this.lblRehabilitation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOkDnoPaCare
            // 
            this.txtOkDnoPaCare.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOkDnoPaCare.Location = new System.Drawing.Point(549, 250);
            this.txtOkDnoPaCare.Name = "txtOkDnoPaCare";
            this.txtOkDnoPaCare.ReadOnly = true;
            this.txtOkDnoPaCare.Size = new System.Drawing.Size(261, 21);
            this.txtOkDnoPaCare.TabIndex = 44;
            this.txtOkDnoPaCare.TabStop = false;
            this.txtOkDnoPaCare.Text = "999999999999999999999999999999999999";
            this.txtOkDnoPaCare.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPaCareVer
            // 
            this.txtPaCareVer.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPaCareVer.Location = new System.Drawing.Point(811, 250);
            this.txtPaCareVer.Name = "txtPaCareVer";
            this.txtPaCareVer.ReadOnly = true;
            this.txtPaCareVer.Size = new System.Drawing.Size(55, 21);
            this.txtPaCareVer.TabIndex = 45;
            this.txtPaCareVer.TabStop = false;
            this.txtPaCareVer.Text = "060";
            this.txtPaCareVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblClaimAgency
            // 
            this.lblClaimAgency.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblClaimAgency.Location = new System.Drawing.Point(24, 303);
            this.lblClaimAgency.Name = "lblClaimAgency";
            this.lblClaimAgency.Size = new System.Drawing.Size(108, 23);
            this.lblClaimAgency.TabIndex = 46;
            this.lblClaimAgency.Text = "Ⅶ.대행청구 기관";
            this.lblClaimAgency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrintVersion
            // 
            this.lblPrintVersion.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPrintVersion.Location = new System.Drawing.Point(604, 343);
            this.lblPrintVersion.Name = "lblPrintVersion";
            this.lblPrintVersion.Size = new System.Drawing.Size(166, 23);
            this.lblPrintVersion.TabIndex = 49;
            this.lblPrintVersion.Text = "서면 청구 명세서 출력 버전";
            this.lblPrintVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPrintVersion.Visible = false;
            // 
            // cboPrtVer
            // 
            this.cboPrtVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrtVer.FormattingEnabled = true;
            this.cboPrtVer.Location = new System.Drawing.Point(771, 344);
            this.cboPrtVer.Name = "cboPrtVer";
            this.cboPrtVer.Size = new System.Drawing.Size(99, 20);
            this.cboPrtVer.TabIndex = 50;
            this.cboPrtVer.Visible = false;
            // 
            // txtREDEM
            // 
            this.txtREDEM.Location = new System.Drawing.Point(136, 304);
            this.txtREDEM.Name = "txtREDEM";
            this.txtREDEM.Size = new System.Drawing.Size(164, 21);
            this.txtREDEM.TabIndex = 51;
            // 
            // ADD9902E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 366);
            this.Controls.Add(this.txtREDEM);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblHospitalCode);
            this.Controls.Add(this.txtHosId);
            this.Controls.Add(this.txtHosId2);
            this.Controls.Add(this.lblHospitalName);
            this.Controls.Add(this.txtHosNm);
            this.Controls.Add(this.lblHospitalType);
            this.Controls.Add(this.txtHosJB);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtHosAddr);
            this.Controls.Add(this.lblRepresentative);
            this.Controls.Add(this.txtHosCEO);
            this.Controls.Add(this.txtHOSCEORID);
            this.Controls.Add(this.lblWriter);
            this.Controls.Add(this.txtWorkNM);
            this.Controls.Add(this.txtWorkRID);
            this.Controls.Add(this.lblIndustrialClaimant);
            this.Controls.Add(this.txtDemNmSanje);
            this.Controls.Add(this.lblInspection);
            this.Controls.Add(this.lblInternalMedicine);
            this.Controls.Add(this.txtOkDno);
            this.Controls.Add(this.txtEDIVer);
            this.Controls.Add(this.lblPsychiatry);
            this.Controls.Add(this.txtOkDnoPHY);
            this.Controls.Add(this.txtPHYVer);
            this.Controls.Add(this.lblDrg);
            this.Controls.Add(this.txtOkDnoDRG);
            this.Controls.Add(this.txtDRGVer);
            this.Controls.Add(this.lblHemodialysis);
            this.Controls.Add(this.txtOkDnoBlood);
            this.Controls.Add(this.txtBloodVer);
            this.Controls.Add(this.lblDental);
            this.Controls.Add(this.txtOkDnoDENT);
            this.Controls.Add(this.txtDENTVer);
            this.Controls.Add(this.lblOrientalMedicine);
            this.Controls.Add(this.txtOkDnoHAN);
            this.Controls.Add(this.txtHANVer);
            this.Controls.Add(this.lblNursing);
            this.Controls.Add(this.txtOkDnoYOYANG);
            this.Controls.Add(this.txtYOYANGVer);
            this.Controls.Add(this.lblRehabilitation);
            this.Controls.Add(this.txtOkDnoPaCare);
            this.Controls.Add(this.txtPaCareVer);
            this.Controls.Add(this.lblClaimAgency);
            this.Controls.Add(this.lblPrintVersion);
            this.Controls.Add(this.cboPrtVer);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MaximizeBox = false;
            this.Name = "ADD9902E";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "병원일반사항(ADD9902E)";
            this.Load += new System.EventHandler(this.ADD9902E_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ADD9902E_MouseClick);
            this.Activated += new System.EventHandler(this.ADD9902E_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblHospitalCode;
        private System.Windows.Forms.TextBox txtHosId;
        private System.Windows.Forms.TextBox txtHosId2;
        private System.Windows.Forms.Label lblHospitalName;
        private System.Windows.Forms.TextBox txtHosNm;
        private System.Windows.Forms.Label lblHospitalType;
        private System.Windows.Forms.TextBox txtHosJB;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtHosAddr;
        private System.Windows.Forms.Label lblRepresentative;
        private System.Windows.Forms.TextBox txtHosCEO;
        private System.Windows.Forms.TextBox txtHOSCEORID;
        private System.Windows.Forms.Label lblWriter;
        private System.Windows.Forms.TextBox txtWorkNM;
        private System.Windows.Forms.TextBox txtWorkRID;
        private System.Windows.Forms.Label lblIndustrialClaimant;
        private System.Windows.Forms.TextBox txtDemNmSanje;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.Label lblInternalMedicine;
        private System.Windows.Forms.TextBox txtOkDno;
        private System.Windows.Forms.TextBox txtEDIVer;
        private System.Windows.Forms.Label lblPsychiatry;
        private System.Windows.Forms.TextBox txtOkDnoPHY;
        private System.Windows.Forms.TextBox txtPHYVer;
        private System.Windows.Forms.Label lblDrg;
        private System.Windows.Forms.TextBox txtOkDnoDRG;
        private System.Windows.Forms.TextBox txtDRGVer;
        private System.Windows.Forms.Label lblHemodialysis;
        private System.Windows.Forms.TextBox txtOkDnoBlood;
        private System.Windows.Forms.TextBox txtBloodVer;
        private System.Windows.Forms.Label lblDental;
        private System.Windows.Forms.TextBox txtOkDnoDENT;
        private System.Windows.Forms.TextBox txtDENTVer;
        private System.Windows.Forms.Label lblOrientalMedicine;
        private System.Windows.Forms.TextBox txtOkDnoHAN;
        private System.Windows.Forms.TextBox txtHANVer;
        private System.Windows.Forms.Label lblNursing;
        private System.Windows.Forms.TextBox txtOkDnoYOYANG;
        private System.Windows.Forms.TextBox txtYOYANGVer;
        private System.Windows.Forms.Label lblRehabilitation;
        private System.Windows.Forms.TextBox txtOkDnoPaCare;
        private System.Windows.Forms.TextBox txtPaCareVer;
        private System.Windows.Forms.Label lblClaimAgency;
        private System.Windows.Forms.Label lblPrintVersion;
        private System.Windows.Forms.ComboBox cboPrtVer;
        private System.Windows.Forms.TextBox txtREDEM;
    }
}


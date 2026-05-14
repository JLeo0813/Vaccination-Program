namespace Vaccination_Program
{
    partial class Patient_Registration
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
            groupBox1 = new GroupBox();
            dtpBirthDate = new DateTimePicker();
            cmbSex = new ComboBox();
            txtChildMI = new TextBox();
            txtChildFirstName = new TextBox();
            txtChildLastName = new TextBox();
            label5 = new Label();
            label1 = new Label();
            label4 = new Label();
            label2 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            chkProtectedAtBirth = new CheckBox();
            txtAddress = new TextBox();
            txtMotherMI = new TextBox();
            txtMotherFirstName = new TextBox();
            txtMotherLastName = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            groupBox3 = new GroupBox();
            label31 = new Label();
            dtpMMR2 = new DateTimePicker();
            label32 = new Label();
            dtpMMR1 = new DateTimePicker();
            label33 = new Label();
            label28 = new Label();
            dtpIPV2 = new DateTimePicker();
            label29 = new Label();
            dtpIPV1 = new DateTimePicker();
            label30 = new Label();
            label24 = new Label();
            dtpPCV3 = new DateTimePicker();
            label25 = new Label();
            dtpPCV2 = new DateTimePicker();
            label26 = new Label();
            dtpPCV1 = new DateTimePicker();
            label27 = new Label();
            label20 = new Label();
            dtpDPT3 = new DateTimePicker();
            label21 = new Label();
            dtpDPT2 = new DateTimePicker();
            label22 = new Label();
            dtpDPT1 = new DateTimePicker();
            label23 = new Label();
            label17 = new Label();
            dtpHepB_After = new DateTimePicker();
            label18 = new Label();
            dtpHepB_Within = new DateTimePicker();
            label19 = new Label();
            label14 = new Label();
            dtpBCG_After = new DateTimePicker();
            label15 = new Label();
            dtpBCG_Within = new DateTimePicker();
            label16 = new Label();
            label13 = new Label();
            dtpOPV3 = new DateTimePicker();
            label12 = new Label();
            dtpOPV2 = new DateTimePicker();
            label11 = new Label();
            dtpOPV1 = new DateTimePicker();
            label6 = new Label();
            btnSavePatient = new Button();
            label34 = new Label();
            txtRemarks = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpBirthDate);
            groupBox1.Controls.Add(cmbSex);
            groupBox1.Controls.Add(txtChildMI);
            groupBox1.Controls.Add(txtChildFirstName);
            groupBox1.Controls.Add(txtChildLastName);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(97, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(626, 342);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Patient Information";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Location = new Point(225, 289);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(272, 27);
            dtpBirthDate.TabIndex = 10;
            // 
            // cmbSex
            // 
            cmbSex.FormattingEnabled = true;
            cmbSex.Items.AddRange(new object[] { "Male", "Female" });
            cmbSex.Location = new Point(225, 233);
            cmbSex.Name = "cmbSex";
            cmbSex.Size = new Size(151, 28);
            cmbSex.TabIndex = 9;
            // 
            // txtChildMI
            // 
            txtChildMI.Location = new Point(225, 119);
            txtChildMI.Name = "txtChildMI";
            txtChildMI.Size = new Size(272, 27);
            txtChildMI.TabIndex = 8;
            // 
            // txtChildFirstName
            // 
            txtChildFirstName.Location = new Point(225, 63);
            txtChildFirstName.Name = "txtChildFirstName";
            txtChildFirstName.Size = new Size(272, 27);
            txtChildFirstName.TabIndex = 7;
            // 
            // txtChildLastName
            // 
            txtChildLastName.Location = new Point(225, 178);
            txtChildLastName.Name = "txtChildLastName";
            txtChildLastName.Size = new Size(272, 27);
            txtChildLastName.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(67, 289);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 4;
            label5.Text = "Birth Date:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 233);
            label1.Name = "label1";
            label1.Size = new Size(35, 20);
            label1.TabIndex = 5;
            label1.Text = "Sex:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(67, 181);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 3;
            label4.Text = "Last Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 70);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 1;
            label2.Text = "First Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(67, 126);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 2;
            label3.Text = "Middle Initial:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkProtectedAtBirth);
            groupBox2.Controls.Add(txtAddress);
            groupBox2.Controls.Add(txtMotherMI);
            groupBox2.Controls.Add(txtMotherFirstName);
            groupBox2.Controls.Add(txtMotherLastName);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label10);
            groupBox2.Location = new Point(97, 417);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(626, 416);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mother Information";
            // 
            // chkProtectedAtBirth
            // 
            chkProtectedAtBirth.AutoSize = true;
            chkProtectedAtBirth.Location = new Point(67, 305);
            chkProtectedAtBirth.Name = "chkProtectedAtBirth";
            chkProtectedAtBirth.Size = new Size(415, 24);
            chkProtectedAtBirth.TabIndex = 10;
            chkProtectedAtBirth.Text = "Mother received adequate Td vaccines (Protected at Birth)";
            chkProtectedAtBirth.UseVisualStyleBackColor = true;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(225, 249);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(272, 27);
            txtAddress.TabIndex = 9;
            // 
            // txtMotherMI
            // 
            txtMotherMI.Location = new Point(225, 127);
            txtMotherMI.Name = "txtMotherMI";
            txtMotherMI.Size = new Size(272, 27);
            txtMotherMI.TabIndex = 8;
            // 
            // txtMotherFirstName
            // 
            txtMotherFirstName.Location = new Point(225, 63);
            txtMotherFirstName.Name = "txtMotherFirstName";
            txtMotherFirstName.Size = new Size(272, 27);
            txtMotherFirstName.TabIndex = 7;
            // 
            // txtMotherLastName
            // 
            txtMotherLastName.Location = new Point(225, 188);
            txtMotherLastName.Name = "txtMotherLastName";
            txtMotherLastName.Size = new Size(272, 27);
            txtMotherLastName.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(67, 249);
            label7.Name = "label7";
            label7.Size = new Size(65, 20);
            label7.TabIndex = 5;
            label7.Text = "Address:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(67, 191);
            label8.Name = "label8";
            label8.Size = new Size(82, 20);
            label8.TabIndex = 3;
            label8.Text = "Last Name:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(67, 70);
            label9.Name = "label9";
            label9.Size = new Size(83, 20);
            label9.TabIndex = 1;
            label9.Text = "First Name:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(67, 134);
            label10.Name = "label10";
            label10.Size = new Size(100, 20);
            label10.TabIndex = 2;
            label10.Text = "Middle Initial:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label31);
            groupBox3.Controls.Add(dtpMMR2);
            groupBox3.Controls.Add(label32);
            groupBox3.Controls.Add(dtpMMR1);
            groupBox3.Controls.Add(label33);
            groupBox3.Controls.Add(label28);
            groupBox3.Controls.Add(dtpIPV2);
            groupBox3.Controls.Add(label29);
            groupBox3.Controls.Add(dtpIPV1);
            groupBox3.Controls.Add(label30);
            groupBox3.Controls.Add(label24);
            groupBox3.Controls.Add(dtpPCV3);
            groupBox3.Controls.Add(label25);
            groupBox3.Controls.Add(dtpPCV2);
            groupBox3.Controls.Add(label26);
            groupBox3.Controls.Add(dtpPCV1);
            groupBox3.Controls.Add(label27);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(dtpDPT3);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(dtpDPT2);
            groupBox3.Controls.Add(label22);
            groupBox3.Controls.Add(dtpDPT1);
            groupBox3.Controls.Add(label23);
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(dtpHepB_After);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(dtpHepB_Within);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(dtpBCG_After);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(dtpBCG_Within);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(dtpOPV3);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(dtpOPV2);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(dtpOPV1);
            groupBox3.Controls.Add(label6);
            groupBox3.Location = new Point(776, 25);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(686, 955);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Completed Vaccines";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(387, 777);
            label31.Name = "label31";
            label31.Size = new Size(99, 20);
            label31.TabIndex = 50;
            label31.Text = "Second Dose:";
            // 
            // dtpMMR2
            // 
            dtpMMR2.Location = new Point(387, 810);
            dtpMMR2.Name = "dtpMMR2";
            dtpMMR2.ShowCheckBox = true;
            dtpMMR2.Size = new Size(263, 27);
            dtpMMR2.TabIndex = 49;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(387, 697);
            label32.Name = "label32";
            label32.Size = new Size(77, 20);
            label32.TabIndex = 48;
            label32.Text = "First Dose:";
            // 
            // dtpMMR1
            // 
            dtpMMR1.Location = new Point(387, 730);
            dtpMMR1.Name = "dtpMMR1";
            dtpMMR1.ShowCheckBox = true;
            dtpMMR1.Size = new Size(263, 27);
            dtpMMR1.TabIndex = 47;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(387, 677);
            label33.Name = "label33";
            label33.Size = new Size(44, 20);
            label33.TabIndex = 46;
            label33.Text = "MMR";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(387, 502);
            label28.Name = "label28";
            label28.Size = new Size(99, 20);
            label28.TabIndex = 45;
            label28.Text = "Second Dose:";
            // 
            // dtpIPV2
            // 
            dtpIPV2.Location = new Point(387, 535);
            dtpIPV2.Name = "dtpIPV2";
            dtpIPV2.ShowCheckBox = true;
            dtpIPV2.Size = new Size(263, 27);
            dtpIPV2.TabIndex = 44;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(387, 422);
            label29.Name = "label29";
            label29.Size = new Size(77, 20);
            label29.TabIndex = 43;
            label29.Text = "First Dose:";
            // 
            // dtpIPV1
            // 
            dtpIPV1.Location = new Point(387, 455);
            dtpIPV1.Name = "dtpIPV1";
            dtpIPV1.ShowCheckBox = true;
            dtpIPV1.Size = new Size(263, 27);
            dtpIPV1.TabIndex = 42;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(387, 402);
            label30.Name = "label30";
            label30.Size = new Size(30, 20);
            label30.TabIndex = 41;
            label30.Text = "IPV";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(387, 203);
            label24.Name = "label24";
            label24.Size = new Size(84, 20);
            label24.TabIndex = 40;
            label24.Text = "Third Dose:";
            // 
            // dtpPCV3
            // 
            dtpPCV3.Location = new Point(387, 236);
            dtpPCV3.Name = "dtpPCV3";
            dtpPCV3.ShowCheckBox = true;
            dtpPCV3.Size = new Size(263, 27);
            dtpPCV3.TabIndex = 39;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(387, 123);
            label25.Name = "label25";
            label25.Size = new Size(99, 20);
            label25.TabIndex = 38;
            label25.Text = "Second Dose:";
            // 
            // dtpPCV2
            // 
            dtpPCV2.Location = new Point(387, 156);
            dtpPCV2.Name = "dtpPCV2";
            dtpPCV2.ShowCheckBox = true;
            dtpPCV2.Size = new Size(263, 27);
            dtpPCV2.TabIndex = 37;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(387, 43);
            label26.Name = "label26";
            label26.Size = new Size(77, 20);
            label26.TabIndex = 36;
            label26.Text = "First Dose:";
            // 
            // dtpPCV1
            // 
            dtpPCV1.Location = new Point(387, 76);
            dtpPCV1.Name = "dtpPCV1";
            dtpPCV1.ShowCheckBox = true;
            dtpPCV1.Size = new Size(263, 27);
            dtpPCV1.TabIndex = 35;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(387, 23);
            label27.Name = "label27";
            label27.Size = new Size(35, 20);
            label27.TabIndex = 34;
            label27.Text = "PCV";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(48, 582);
            label20.Name = "label20";
            label20.Size = new Size(84, 20);
            label20.TabIndex = 33;
            label20.Text = "Third Dose:";
            // 
            // dtpDPT3
            // 
            dtpDPT3.Location = new Point(48, 615);
            dtpDPT3.Name = "dtpDPT3";
            dtpDPT3.ShowCheckBox = true;
            dtpDPT3.Size = new Size(263, 27);
            dtpDPT3.TabIndex = 32;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(48, 502);
            label21.Name = "label21";
            label21.Size = new Size(99, 20);
            label21.TabIndex = 31;
            label21.Text = "Second Dose:";
            // 
            // dtpDPT2
            // 
            dtpDPT2.Location = new Point(48, 535);
            dtpDPT2.Name = "dtpDPT2";
            dtpDPT2.ShowCheckBox = true;
            dtpDPT2.Size = new Size(263, 27);
            dtpDPT2.TabIndex = 30;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(48, 422);
            label22.Name = "label22";
            label22.Size = new Size(77, 20);
            label22.TabIndex = 29;
            label22.Text = "First Dose:";
            // 
            // dtpDPT1
            // 
            dtpDPT1.Location = new Point(48, 455);
            dtpDPT1.Name = "dtpDPT1";
            dtpDPT1.ShowCheckBox = true;
            dtpDPT1.Size = new Size(263, 27);
            dtpDPT1.TabIndex = 28;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(48, 402);
            label23.Name = "label23";
            label23.Size = new Size(109, 20);
            label23.TabIndex = 27;
            label23.Text = "DPT-HiB-HepB";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(48, 309);
            label17.Name = "label17";
            label17.Size = new Size(108, 20);
            label17.TabIndex = 26;
            label17.Text = "After 24 Hours:";
            // 
            // dtpHepB_After
            // 
            dtpHepB_After.Location = new Point(48, 342);
            dtpHepB_After.Name = "dtpHepB_After";
            dtpHepB_After.ShowCheckBox = true;
            dtpHepB_After.Size = new Size(263, 27);
            dtpHepB_After.TabIndex = 25;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(48, 229);
            label18.Name = "label18";
            label18.Size = new Size(118, 20);
            label18.TabIndex = 24;
            label18.Text = "Within 24 Hours:";
            // 
            // dtpHepB_Within
            // 
            dtpHepB_Within.Location = new Point(48, 262);
            dtpHepB_Within.Name = "dtpHepB_Within";
            dtpHepB_Within.ShowCheckBox = true;
            dtpHepB_Within.Size = new Size(263, 27);
            dtpHepB_Within.TabIndex = 23;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(48, 209);
            label19.Name = "label19";
            label19.Size = new Size(82, 20);
            label19.TabIndex = 22;
            label19.Text = "Hepatitis B";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(48, 123);
            label14.Name = "label14";
            label14.Size = new Size(108, 20);
            label14.TabIndex = 21;
            label14.Text = "After 24 Hours:";
            // 
            // dtpBCG_After
            // 
            dtpBCG_After.Location = new Point(48, 156);
            dtpBCG_After.Name = "dtpBCG_After";
            dtpBCG_After.ShowCheckBox = true;
            dtpBCG_After.Size = new Size(263, 27);
            dtpBCG_After.TabIndex = 20;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(48, 43);
            label15.Name = "label15";
            label15.Size = new Size(118, 20);
            label15.TabIndex = 19;
            label15.Text = "Within 24 Hours:";
            // 
            // dtpBCG_Within
            // 
            dtpBCG_Within.Location = new Point(48, 76);
            dtpBCG_Within.Name = "dtpBCG_Within";
            dtpBCG_Within.ShowCheckBox = true;
            dtpBCG_Within.Size = new Size(263, 27);
            dtpBCG_Within.TabIndex = 18;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(48, 23);
            label16.Name = "label16";
            label16.Size = new Size(37, 20);
            label16.TabIndex = 17;
            label16.Text = "BCG";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(48, 857);
            label13.Name = "label13";
            label13.Size = new Size(84, 20);
            label13.TabIndex = 16;
            label13.Text = "Third Dose:";
            // 
            // dtpOPV3
            // 
            dtpOPV3.Location = new Point(48, 890);
            dtpOPV3.Name = "dtpOPV3";
            dtpOPV3.ShowCheckBox = true;
            dtpOPV3.Size = new Size(263, 27);
            dtpOPV3.TabIndex = 15;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(48, 777);
            label12.Name = "label12";
            label12.Size = new Size(99, 20);
            label12.TabIndex = 14;
            label12.Text = "Second Dose:";
            // 
            // dtpOPV2
            // 
            dtpOPV2.Location = new Point(48, 810);
            dtpOPV2.Name = "dtpOPV2";
            dtpOPV2.ShowCheckBox = true;
            dtpOPV2.Size = new Size(263, 27);
            dtpOPV2.TabIndex = 13;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(48, 697);
            label11.Name = "label11";
            label11.Size = new Size(77, 20);
            label11.TabIndex = 12;
            label11.Text = "First Dose:";
            // 
            // dtpOPV1
            // 
            dtpOPV1.Location = new Point(48, 730);
            dtpOPV1.Name = "dtpOPV1";
            dtpOPV1.ShowCheckBox = true;
            dtpOPV1.Size = new Size(263, 27);
            dtpOPV1.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(48, 677);
            label6.Name = "label6";
            label6.Size = new Size(37, 20);
            label6.TabIndex = 10;
            label6.Text = "OPV";
            // 
            // btnSavePatient
            // 
            btnSavePatient.Location = new Point(1721, 915);
            btnSavePatient.Name = "btnSavePatient";
            btnSavePatient.Size = new Size(114, 55);
            btnSavePatient.TabIndex = 5;
            btnSavePatient.Text = "Save";
            btnSavePatient.UseVisualStyleBackColor = true;
            btnSavePatient.Click += btnSavePatient_Click;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(1516, 48);
            label34.Name = "label34";
            label34.Size = new Size(68, 20);
            label34.TabIndex = 6;
            label34.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            txtRemarks.Location = new Point(1516, 88);
            txtRemarks.Multiline = true;
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new Size(272, 34);
            txtRemarks.TabIndex = 11;
            // 
            // Patient_Registration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(txtRemarks);
            Controls.Add(label34);
            Controls.Add(btnSavePatient);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Patient_Registration";
            Text = "Patient_Registration";
            Load += Patient_Registration_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtChildLastName;
        private Label label5;
        private Label label1;
        private Label label4;
        private Label label2;
        private Label label3;
        private ComboBox cmbSex;
        private TextBox txtChildMI;
        private TextBox txtChildFirstName;
        private DateTimePicker dtpBirthDate;
        private GroupBox groupBox2;
        private TextBox txtMotherMI;
        private TextBox txtMotherFirstName;
        private TextBox txtMotherLastName;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtAddress;
        private GroupBox groupBox3;
        private Button btnSavePatient;
        private Label label11;
        private DateTimePicker dtpOPV1;
        private Label label6;
        private Label label12;
        private DateTimePicker dtpOPV2;
        private Label label13;
        private DateTimePicker dtpOPV3;
        private Label label17;
        private DateTimePicker dtpHepB_After;
        private Label label18;
        private DateTimePicker dtpHepB_Within;
        private Label label19;
        private Label label14;
        private DateTimePicker dtpBCG_After;
        private Label label15;
        private DateTimePicker dtpBCG_Within;
        private Label label16;
        private Label label28;
        private DateTimePicker dtpIPV2;
        private Label label29;
        private DateTimePicker dtpIPV1;
        private Label label30;
        private Label label24;
        private DateTimePicker dtpPCV3;
        private Label label25;
        private DateTimePicker dtpPCV2;
        private Label label26;
        private DateTimePicker dtpPCV1;
        private Label label27;
        private Label label20;
        private DateTimePicker dtpDPT3;
        private Label label21;
        private DateTimePicker dtpDPT2;
        private Label label22;
        private DateTimePicker dtpDPT1;
        private Label label23;
        private Label label31;
        private DateTimePicker dtpMMR2;
        private Label label32;
        private DateTimePicker dtpMMR1;
        private Label label33;
        private CheckBox chkProtectedAtBirth;
        private Label label34;
        private TextBox txtRemarks;
    }
}
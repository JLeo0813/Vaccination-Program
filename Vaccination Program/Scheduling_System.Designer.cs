namespace Vaccination_Program
{
    partial class Scheduling_System
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            monthCalendar1 = new MonthCalendar();
            label1 = new Label();
            patient_name_Textbox = new TextBox();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            address_Textbox = new TextBox();
            label4 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            confirm_Button = new Button();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(78, 120);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(862, 135);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 1;
            label1.Text = "Patient Name:";
            // 
            // patient_name_Textbox
            // 
            patient_name_Textbox.Location = new Point(1010, 128);
            patient_name_Textbox.Name = "patient_name_Textbox";
            patient_name_Textbox.Size = new Size(250, 27);
            patient_name_Textbox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(862, 202);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 3;
            label2.Text = "Birth Date:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1010, 197);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(862, 266);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 5;
            label3.Text = "Address:";
            // 
            // address_Textbox
            // 
            address_Textbox.Location = new Point(1010, 263);
            address_Textbox.Name = "address_Textbox";
            address_Textbox.Size = new Size(250, 27);
            address_Textbox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(862, 339);
            label4.Name = "label4";
            label4.Size = new Size(156, 20);
            label4.TabIndex = 7;
            label4.Text = "Vaccine to Administer:";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(1010, 392);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(92, 24);
            radioButton1.TabIndex = 8;
            radioButton1.TabStop = true;
            radioButton1.Text = "Vaccine 1";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(1010, 459);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(92, 24);
            radioButton2.TabIndex = 9;
            radioButton2.TabStop = true;
            radioButton2.Text = "Vaccine 2";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(1010, 527);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(92, 24);
            radioButton3.TabIndex = 10;
            radioButton3.TabStop = true;
            radioButton3.Text = "Vaccine 3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // confirm_Button
            // 
            confirm_Button.Location = new Point(862, 592);
            confirm_Button.Name = "confirm_Button";
            confirm_Button.Size = new Size(145, 56);
            confirm_Button.TabIndex = 11;
            confirm_Button.Text = "Confirm Schedule";
            confirm_Button.UseVisualStyleBackColor = true;
            // 
            // Scheduling_System
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(confirm_Button);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(label4);
            Controls.Add(address_Textbox);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(patient_name_Textbox);
            Controls.Add(label1);
            Controls.Add(monthCalendar1);
            Name = "Scheduling_System";
            Text = "Scheduling System";
            Load += this.Scheduling_System_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private Label label1;
        private TextBox patient_name_Textbox;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private TextBox address_Textbox;
        private Label label4;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Button confirm_Button;
    }
}

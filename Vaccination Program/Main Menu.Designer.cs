namespace Vaccination_Program
{
    partial class Main_Menu
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
            btnRegistration = new Button();
            btnInformation = new Button();
            btnCalendar = new Button();
            btnScheduling = new Button();
            SuspendLayout();
            // 
            // btnRegistration
            // 
            btnRegistration.Location = new Point(327, 222);
            btnRegistration.Name = "btnRegistration";
            btnRegistration.Size = new Size(157, 66);
            btnRegistration.TabIndex = 0;
            btnRegistration.Text = "Patient Registration";
            btnRegistration.UseVisualStyleBackColor = true;
            btnRegistration.Click += btnRegistration_Click;
            // 
            // btnInformation
            // 
            btnInformation.Location = new Point(608, 222);
            btnInformation.Name = "btnInformation";
            btnInformation.Size = new Size(157, 66);
            btnInformation.TabIndex = 1;
            btnInformation.Text = "Patient Information";
            btnInformation.UseVisualStyleBackColor = true;
            btnInformation.Click += btnInformation_Click;
            // 
            // btnCalendar
            // 
            btnCalendar.Location = new Point(889, 222);
            btnCalendar.Name = "btnCalendar";
            btnCalendar.Size = new Size(157, 66);
            btnCalendar.TabIndex = 2;
            btnCalendar.Text = "Vaccination Calendar";
            btnCalendar.UseVisualStyleBackColor = true;
            btnCalendar.Click += btnCalendar_Click;
            // 
            // btnScheduling
            // 
            btnScheduling.Location = new Point(1142, 222);
            btnScheduling.Name = "btnScheduling";
            btnScheduling.Size = new Size(157, 66);
            btnScheduling.TabIndex = 3;
            btnScheduling.Text = "Vaccination Scheduling";
            btnScheduling.UseVisualStyleBackColor = true;
            btnScheduling.Click += btnScheduling_Click;
            // 
            // Main_Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(btnScheduling);
            Controls.Add(btnCalendar);
            Controls.Add(btnInformation);
            Controls.Add(btnRegistration);
            Name = "Main_Menu";
            Text = "Main_Menu";
            Load += Main_Menu_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnRegistration;
        private Button btnInformation;
        private Button btnCalendar;
        private Button btnScheduling;
    }
}
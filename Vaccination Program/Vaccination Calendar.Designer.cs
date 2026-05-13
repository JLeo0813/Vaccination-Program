namespace Vaccination_Program
{
    partial class Vaccination_Calendar
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
            monthCalendar1 = new MonthCalendar();
            dgvSchedule = new DataGridView();
            lblSelectedDate = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(97, 91);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;
            // 
            // dgvSchedule
            // 
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSchedule.Location = new Point(704, 91);
            dgvSchedule.Name = "dgvSchedule";
            dgvSchedule.ReadOnly = true;
            dgvSchedule.RowHeadersWidth = 51;
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedule.Size = new Size(300, 188);
            dgvSchedule.TabIndex = 1;
            // 
            // lblSelectedDate
            // 
            lblSelectedDate.AutoSize = true;
            lblSelectedDate.Location = new Point(704, 59);
            lblSelectedDate.Name = "lblSelectedDate";
            lblSelectedDate.Size = new Size(203, 20);
            lblSelectedDate.TabIndex = 2;
            lblSelectedDate.Text = "Schedules for: [Select a Date]";
            // 
            // Vaccination_Calendar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(lblSelectedDate);
            Controls.Add(dgvSchedule);
            Controls.Add(monthCalendar1);
            Name = "Vaccination_Calendar";
            Text = "Vaccination_Calendar";
            Load += Vaccination_Calendar_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private DataGridView dgvSchedule;
        private Label lblSelectedDate;
    }
}
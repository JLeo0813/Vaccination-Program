namespace Vaccination_Program
{
    partial class Scheduling_System
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
            dgvPatients = new DataGridView();
            lblSelectedPatient = new Label();
            txtSearch = new TextBox();
            cmbVaccine = new ComboBox();
            cmbDose = new ComboBox();
            btnSaveSchedule = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(235, 156);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPatients.Location = new Point(682, 156);
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersWidth = 51;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(512, 311);
            dgvPatients.TabIndex = 2;
            dgvPatients.CellClick += dgvPatients_CellClick;
            // 
            // lblSelectedPatient
            // 
            lblSelectedPatient.AutoSize = true;
            lblSelectedPatient.Location = new Point(1249, 156);
            lblSelectedPatient.Name = "lblSelectedPatient";
            lblSelectedPatient.Size = new Size(158, 20);
            lblSelectedPatient.TabIndex = 3;
            lblSelectedPatient.Text = "Selected Patient: None";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(1249, 187);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 5;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // cmbVaccine
            // 
            cmbVaccine.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaccine.FormattingEnabled = true;
            cmbVaccine.Location = new Point(1249, 259);
            cmbVaccine.Name = "cmbVaccine";
            cmbVaccine.Size = new Size(300, 28);
            cmbVaccine.TabIndex = 6;
            cmbVaccine.SelectedIndexChanged += cmbVaccine_SelectedIndexChanged;
            // 
            // cmbDose
            // 
            cmbDose.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDose.FormattingEnabled = true;
            cmbDose.Location = new Point(1572, 259);
            cmbDose.Name = "cmbDose";
            cmbDose.Size = new Size(183, 28);
            cmbDose.TabIndex = 7;
            // 
            // btnSaveSchedule
            // 
            btnSaveSchedule.Location = new Point(1613, 751);
            btnSaveSchedule.Name = "btnSaveSchedule";
            btnSaveSchedule.Size = new Size(142, 64);
            btnSaveSchedule.TabIndex = 8;
            btnSaveSchedule.Text = "Set Vaccine Schedule";
            btnSaveSchedule.UseVisualStyleBackColor = true;
            btnSaveSchedule.Click += btnSaveSchedule_Click;
            // 
            // Scheduling_System
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(btnSaveSchedule);
            Controls.Add(cmbDose);
            Controls.Add(cmbVaccine);
            Controls.Add(txtSearch);
            Controls.Add(lblSelectedPatient);
            Controls.Add(dgvPatients);
            Controls.Add(monthCalendar1);
            Name = "Scheduling_System";
            Text = "Scheduling_System";
            Load += Scheduling_System_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private TextBox textBox1;
        private DataGridView dgvPatients;
        private Label lblSelectedPatient;
        private TextBox txtSearch;
        private ComboBox cmbVaccine;
        private ComboBox cmbDose;
        private Button btnSaveSchedule;
    }
}
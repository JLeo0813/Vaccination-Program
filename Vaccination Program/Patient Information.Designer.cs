namespace Vaccination_Program
{
    partial class Patient_Information
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
            patient_information_GridView = new DataGridView();
            querySelector_ComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)patient_information_GridView).BeginInit();
            SuspendLayout();
            // 
            // patient_information_GridView
            // 
            patient_information_GridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            patient_information_GridView.Location = new Point(189, 111);
            patient_information_GridView.Name = "patient_information_GridView";
            patient_information_GridView.RowHeadersWidth = 51;
            patient_information_GridView.Size = new Size(1200, 800);
            patient_information_GridView.TabIndex = 1;
            // 
            // querySelector_ComboBox
            // 
            querySelector_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            querySelector_ComboBox.FormattingEnabled = true;
            querySelector_ComboBox.Location = new Point(1489, 148);
            querySelector_ComboBox.Name = "querySelector_ComboBox";
            querySelector_ComboBox.Size = new Size(300, 28);
            querySelector_ComboBox.TabIndex = 2;
            // 
            // Patient_Information
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(querySelector_ComboBox);
            Controls.Add(patient_information_GridView);
            Name = "Patient_Information";
            Text = "Patient_Information";
            Load += Patient_Information_Load;
            ((System.ComponentModel.ISupportInitialize)patient_information_GridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView patient_information_GridView;
        private ComboBox querySelector_ComboBox;
    }
}
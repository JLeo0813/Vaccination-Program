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
            btnRefresh = new Button();
            txtSearch = new TextBox();
            label1 = new Label();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)patient_information_GridView).BeginInit();
            SuspendLayout();
            // 
            // patient_information_GridView
            // 
            patient_information_GridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            patient_information_GridView.Location = new Point(189, 111);
            patient_information_GridView.Name = "patient_information_GridView";
            patient_information_GridView.RowHeadersWidth = 51;
            patient_information_GridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            querySelector_ComboBox.SelectedIndexChanged += querySelector_ComboBox_SelectedIndexChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(1489, 710);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(150, 49);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(1489, 257);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1489, 225);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 5;
            label1.Text = "Search Bar:";
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(1489, 814);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(150, 49);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Edit Patient";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1677, 814);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 49);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete Patient";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // Patient_Information
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnRefresh);
            Controls.Add(querySelector_ComboBox);
            Controls.Add(patient_information_GridView);
            Name = "Patient_Information";
            Text = "Patient_Information";
            Load += Patient_Information_Load;
            ((System.ComponentModel.ISupportInitialize)patient_information_GridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView patient_information_GridView;
        private ComboBox querySelector_ComboBox;
        private Button btnRefresh;
        private TextBox txtSearch;
        private Label label1;
        private Button btnEdit;
        private Button btnDelete;
    }
}
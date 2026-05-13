using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Vaccination_Program
{
    public partial class Patient_Information : Form
    {
        // Update the connection string with your MySQL server credentials
        private string connectionString = "Server=localhost;Database=child_immunization_db;Uid=root;Pwd=08132003JLeo;";

        public Patient_Information()
        {
            InitializeComponent();
        }

        private void Patient_Information_Load(object sender, EventArgs e)
        {
            // The items have been reordered, making the Full Summary the default #1
            querySelector_ComboBox.Items.Add("1. Full Vaccination Status Summary");
            querySelector_ComboBox.Items.Add("2. All Children");
            querySelector_ComboBox.Items.Add("3. All Mothers");
            querySelector_ComboBox.Items.Add("4. Children with Mother's Info");
            querySelector_ComboBox.Items.Add("5. Td - Tetanus-Diphtheria Vaccine");
            querySelector_ComboBox.Items.Add("6. BCG - Bacille Calmette-Guerin Vaccine");
            querySelector_ComboBox.Items.Add("7. Hepatitis B Vaccine");
            querySelector_ComboBox.Items.Add("8. DPT-HiB-HepB Vaccine");
            querySelector_ComboBox.Items.Add("9. OPV - Oral Polio Vaccine");
            querySelector_ComboBox.Items.Add("10. IPV - Inactivated Polio Vaccine");
            querySelector_ComboBox.Items.Add("11. PCV - Pneumococcal Conjugate Vaccine");
            querySelector_ComboBox.Items.Add("12. MMR - Measles-Mumps-Rubella");
            querySelector_ComboBox.Items.Add("13. Children Protected at Birth (CPAB)");
            querySelector_ComboBox.Items.Add("14. FIC / CIC Annual Reporting");
            querySelector_ComboBox.Items.Add("15. Detailed Vaccination Status (All)");

            querySelector_ComboBox.SelectedIndex = 0;
        }

        private void querySelector_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = querySelector_ComboBox.SelectedItem?.ToString() ?? "";
            string queryToExecute = "";

            // Clear the search bar whenever the table category changes
            txtSearch.Text = "";

            switch (selectedOption)
            {
                case "1. Full Vaccination Status Summary":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name, IFNULL(CONCAT(' ', c.middle_initial, '.'), '')) AS child_full_name,
                               c.date_of_birth, TIMESTAMPDIFF(MONTH, c.date_of_birth, CURDATE()) AS age_in_months, c.sex,
                               CASE WHEN b.within_24hrs_date IS NOT NULL OR b.after_24hrs_date IS NOT NULL THEN 'Given' ELSE 'Pending' END AS bcg,
                               CASE
                                   WHEN hb.within_24hrs_date IS NOT NULL THEN 'Given <24hrs'
                                   WHEN hb.after_24hrs_date IS NOT NULL THEN 'Given >24hrs'
                                   ELSE 'Pending'
                               END AS hepb_birth,
                               CASE
                                   WHEN dpt.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN dpt.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN dpt.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS dpt_hib_hepb,
                               CASE
                                   WHEN opv.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN opv.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN opv.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS opv,
                               CASE WHEN ipv.dose1_date IS NOT NULL THEN 'Given' ELSE 'Pending' END AS ipv,
                               CASE
                                   WHEN pcv.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN pcv.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN pcv.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS pcv,
                               CASE
                                   WHEN mmr.dose2_date IS NOT NULL THEN 'Complete (2/2)'
                                   WHEN mmr.dose1_date IS NOT NULL THEN 'Incomplete (1/2)'
                                   ELSE 'Pending'
                               END AS mmr,
                               CASE WHEN cp.protected_at_birth = 1 THEN 'Protected' ELSE 'Not Protected' END AS cpab,
                               CASE WHEN (ist.fic_bcg AND ist.fic_dpt3 AND ist.fic_opv3 AND ist.fic_mmr2) THEN 'Yes' ELSE 'No' END AS is_fic,
                               CASE WHEN (ist.cic_bcg AND ist.cic_dpt3 AND ist.cic_opv3 AND ist.cic_mmr2) THEN 'Yes' ELSE 'No' END AS is_cic,
                               ist.remarks
                        FROM children c
                        LEFT JOIN vacc_bcg b ON c.child_id = b.child_id
                        LEFT JOIN vacc_hepb_birth hb ON c.child_id = hb.child_id
                        LEFT JOIN vacc_dpt_hib_hepb dpt ON c.child_id = dpt.child_id
                        LEFT JOIN vacc_opv opv ON c.child_id = opv.child_id
                        LEFT JOIN vacc_ipv ipv ON c.child_id = ipv.child_id
                        LEFT JOIN vacc_pcv pcv ON c.child_id = pcv.child_id
                        LEFT JOIN vacc_mmr mmr ON c.child_id = mmr.child_id
                        LEFT JOIN cpab cp ON c.child_id = cp.child_id
                        LEFT JOIN immunization_status ist ON c.child_id = ist.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "2. All Children":
                    queryToExecute = @"
                        SELECT child_id, registration_no, date_of_registration, family_serial_no,
                               CONCAT(last_name, ', ', first_name, IFNULL(CONCAT(' ', middle_initial, '.'), '')) AS child_full_name,
                               date_of_birth, TIMESTAMPDIFF(MONTH, date_of_birth, CURDATE()) AS age_in_months, sex, complete_address
                        FROM children
                        ORDER BY last_name, first_name;";
                    break;

                case "3. All Mothers":
                    queryToExecute = @"
                        SELECT mother_id, 
                               CONCAT(last_name, ', ', first_name, IFNULL(CONCAT(' ', middle_initial, '.'), '')) AS mother_full_name
                        FROM mothers
                        ORDER BY last_name, first_name;";
                    break;

                case "4. Children with Mother's Info":
                    queryToExecute = @"
                        SELECT c.registration_no, c.family_serial_no,
                               CONCAT(c.last_name, ', ', c.first_name, IFNULL(CONCAT(' ', c.middle_initial, '.'), '')) AS child_full_name,
                               c.date_of_birth, TIMESTAMPDIFF(MONTH, c.date_of_birth, CURDATE()) AS age_in_months,
                               c.sex, c.complete_address,
                               CONCAT(m.last_name, ', ', m.first_name, IFNULL(CONCAT(' ', m.middle_initial, '.'), '')) AS mother_full_name
                        FROM children c
                        LEFT JOIN mothers m ON c.mother_id = m.mother_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "5. Td - Tetanus-Diphtheria Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               CONCAT(m.last_name, ', ', m.first_name) AS mother_full_name,
                               td.td2_given_month_before_delivery, td.td3_to_td5_given
                        FROM maternal_td_vaccination td
                        JOIN children c ON td.child_id = c.child_id
                        JOIN mothers m ON c.mother_id = m.mother_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "6. BCG - Bacille Calmette-Guerin Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               b.within_24hrs_date, b.within_24hrs_age,
                               b.after_24hrs_date, b.after_24hrs_age,
                               CASE
                                   WHEN b.within_24hrs_date IS NOT NULL THEN 'Given within 24hrs'
                                   WHEN b.after_24hrs_date IS NOT NULL THEN 'Given after 24hrs'
                                   ELSE 'Pending'
                               END AS bcg_status
                        FROM vacc_bcg b
                        JOIN children c ON b.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "7. Hepatitis B Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               hb.within_24hrs_date, hb.within_24hrs_age,
                               hb.after_24hrs_date, hb.after_24hrs_age,
                               CASE
                                   WHEN hb.within_24hrs_date IS NOT NULL THEN 'Given within 24hrs'
                                   WHEN hb.after_24hrs_date IS NOT NULL THEN 'Given after 24hrs'
                                   ELSE 'Pending'
                               END AS hepb_birth_status
                        FROM vacc_hepb_birth hb
                        JOIN children c ON hb.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "8. DPT-HiB-HepB Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               dpt.dose1_date, dpt.dose1_age,
                               dpt.dose2_date, dpt.dose2_age,
                               dpt.dose3_date, dpt.dose3_age,
                               CASE
                                   WHEN dpt.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN dpt.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN dpt.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS dpt_status
                        FROM vacc_dpt_hib_hepb dpt
                        JOIN children c ON dpt.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "9. OPV - Oral Polio Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               opv.dose1_date, opv.dose1_age,
                               opv.dose2_date, opv.dose2_age,
                               opv.dose3_date, opv.dose3_age,
                               CASE
                                   WHEN opv.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN opv.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN opv.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS opv_status
                        FROM vacc_opv opv
                        JOIN children c ON opv.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "10. IPV - Inactivated Polio Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               ipv.dose1_date, ipv.dose1_age,
                               ipv.dose2_date, ipv.dose2_age,
                               CASE 
                                   WHEN ipv.dose2_date IS NOT NULL THEN 'Complete (2/2)'
                                   WHEN ipv.dose1_date IS NOT NULL THEN 'Incomplete (1/2)'
                                   ELSE 'Pending' 
                               END AS ipv_status
                        FROM vacc_ipv ipv
                        JOIN children c ON ipv.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "11. PCV - Pneumococcal Conjugate Vaccine":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               pcv.dose1_date, pcv.dose1_age,
                               pcv.dose2_date, pcv.dose2_age,
                               pcv.dose3_date, pcv.dose3_age,
                               CASE
                                   WHEN pcv.dose3_date IS NOT NULL THEN 'Complete (3/3)'
                                   WHEN pcv.dose2_date IS NOT NULL THEN 'Incomplete (2/3)'
                                   WHEN pcv.dose1_date IS NOT NULL THEN 'Incomplete (1/3)'
                                   ELSE 'Pending'
                               END AS pcv_status
                        FROM vacc_pcv pcv
                        JOIN children c ON pcv.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "12. MMR - Measles-Mumps-Rubella":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               mmr.dose1_date, mmr.dose1_age,
                               mmr.dose2_date, mmr.dose2_age,
                               CASE
                                   WHEN mmr.dose2_date IS NOT NULL THEN 'Complete (2/2)'
                                   WHEN mmr.dose1_date IS NOT NULL THEN 'Incomplete (1/2)'
                                   ELSE 'Pending'
                               END AS mmr_status
                        FROM vacc_mmr mmr
                        JOIN children c ON mmr.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "13. Children Protected at Birth (CPAB)":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               CONCAT(m.last_name, ', ', m.first_name) AS mother_full_name,
                               CASE WHEN cp.protected_at_birth = 1 THEN 'Protected' ELSE 'Not Protected' END AS cpab_status
                        FROM cpab cp
                        JOIN children c ON cp.child_id = c.child_id
                        JOIN mothers m ON c.mother_id = m.mother_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "14. FIC / CIC Annual Reporting":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name) AS child_full_name,
                               c.date_of_birth,
                               ist.fic_bcg, ist.fic_dpt3, ist.fic_opv3, ist.fic_mmr2, ist.fic_date,
                               CASE WHEN (ist.fic_bcg AND ist.fic_dpt3 AND ist.fic_opv3 AND ist.fic_mmr2)
                                    THEN 'FIC' ELSE 'Not FIC' END AS fic_status,
                               ist.cic_bcg, ist.cic_dpt3, ist.cic_opv3, ist.cic_mmr2, ist.cic_date,
                               CASE WHEN (ist.cic_bcg AND ist.cic_dpt3 AND ist.cic_opv3 AND ist.cic_mmr2)
                                    THEN 'CIC' ELSE 'Not CIC' END AS cic_status,
                               ist.remarks
                        FROM immunization_status ist
                        JOIN children c ON ist.child_id = c.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "15. Detailed Vaccination Status (All)":
                    queryToExecute = @"
                        SELECT c.registration_no,
                               CONCAT(c.last_name, ', ', c.first_name, IFNULL(CONCAT(' ', c.middle_initial, '.'), '')) AS child_full_name,
                               c.date_of_birth, TIMESTAMPDIFF(MONTH, c.date_of_birth, CURDATE()) AS age_in_months,
                               c.sex, c.complete_address,
                               CONCAT(m.last_name, ', ', m.first_name) AS mother_full_name,
                               b.within_24hrs_date AS bcg_within_24hrs_date, b.within_24hrs_age AS bcg_within_24hrs_age,
                               b.after_24hrs_date  AS bcg_after_24hrs_date,  b.after_24hrs_age  AS bcg_after_24hrs_age,
                               hb.within_24hrs_date AS hepb_within_24hrs_date, hb.within_24hrs_age AS hepb_within_24hrs_age,
                               hb.after_24hrs_date  AS hepb_after_24hrs_date,  hb.after_24hrs_age  AS hepb_after_24hrs_age,
                               dpt.dose1_date AS dpt_dose1_date, dpt.dose1_age AS dpt_dose1_age,
                               dpt.dose2_date AS dpt_dose2_date, dpt.dose2_age AS dpt_dose2_age,
                               dpt.dose3_date AS dpt_dose3_date, dpt.dose3_age AS dpt_dose3_age,
                               opv.dose1_date AS opv_dose1_date, opv.dose1_age AS opv_dose1_age,
                               opv.dose2_date AS opv_dose2_date, opv.dose2_age AS opv_dose2_age,
                               opv.dose3_date AS opv_dose3_date, opv.dose3_age AS opv_dose3_age,
                               ipv.dose1_date AS ipv_dose1_date, ipv.dose1_age AS ipv_dose1_age,
                               pcv.dose1_date AS pcv_dose1_date, pcv.dose1_age AS pcv_dose1_age,
                               pcv.dose2_date AS pcv_dose2_date, pcv.dose2_age AS pcv_dose2_age,
                               pcv.dose3_date AS pcv_dose3_date, pcv.dose3_age AS pcv_dose3_age,
                               mmr.dose1_date AS mmr_dose1_date, mmr.dose1_age AS mmr_dose1_age,
                               mmr.dose2_date AS mmr_dose2_date, mmr.dose2_age AS mmr_dose2_age,
                               CASE WHEN cp.protected_at_birth = 1 THEN 'Protected' ELSE 'Not Protected' END AS cpab,
                               CASE WHEN (ist.fic_bcg AND ist.fic_dpt3 AND ist.fic_opv3 AND ist.fic_mmr2) THEN 'Yes' ELSE 'No' END AS is_fic,
                               CASE WHEN (ist.cic_bcg AND ist.cic_dpt3 AND ist.cic_opv3 AND ist.cic_mmr2) THEN 'Yes' ELSE 'No' END AS is_cic,
                               ist.remarks
                        FROM children c
                        LEFT JOIN mothers m ON c.mother_id = m.mother_id
                        LEFT JOIN vacc_bcg b ON c.child_id = b.child_id
                        LEFT JOIN vacc_hepb_birth hb ON c.child_id = hb.child_id
                        LEFT JOIN vacc_dpt_hib_hepb dpt ON c.child_id = dpt.child_id
                        LEFT JOIN vacc_opv opv ON c.child_id = opv.child_id
                        LEFT JOIN vacc_ipv ipv ON c.child_id = ipv.child_id
                        LEFT JOIN vacc_pcv pcv ON c.child_id = pcv.child_id
                        LEFT JOIN vacc_mmr mmr ON c.child_id = mmr.child_id
                        LEFT JOIN cpab cp ON c.child_id = cp.child_id
                        LEFT JOIN immunization_status ist ON c.child_id = ist.child_id
                        ORDER BY c.last_name, c.first_name;";
                    break;
            }

            if (!string.IsNullOrEmpty(queryToExecute))
            {
                ExecuteGridQuery(queryToExecute);
            }
        }

        private void ExecuteGridQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Clear any broken mapping before binding new data
                            patient_information_GridView.DataSource = null;
                            patient_information_GridView.AutoGenerateColumns = true;

                            patient_information_GridView.DataSource = dataTable;

                            patient_information_GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            patient_information_GridView.ReadOnly = true;
                            patient_information_GridView.AllowUserToAddRows = false;
                            patient_information_GridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

                            if (patient_information_GridView.Columns["complete_address"] != null)
                                patient_information_GridView.Columns["complete_address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                            if (patient_information_GridView.Columns["remarks"] != null)
                                patient_information_GridView.Columns["remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (querySelector_ComboBox.SelectedItem != null)
            {
                querySelector_ComboBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                querySelector_ComboBox.SelectedIndex = 0;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Verify a row is selected
            if (patient_information_GridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Ensure we can grab the Registration Number
            if (!patient_information_GridView.Columns.Contains("registration_no"))
            {
                MessageBox.Show("Please switch to a view that contains the Registration Number to delete.", "Invalid View", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string regNoStr = patient_information_GridView.SelectedRows[0].Cells["registration_no"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(regNoStr)) return;

            // 3. Display the Warning Confirmation
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to PERMANENTLY delete the patient with Registration No: {regNoStr}?\n\nThis action cannot be undone and will erase all associated vaccination records.",
                "Confirm Permanent Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // Defaults the cursor to 'No' for safety

            // 4. Delete if confirmed
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // Because your DB uses ON DELETE CASCADE, deleting the child automatically deletes all their vaccine records!
                        using (MySqlCommand cmd = new MySqlCommand("DELETE FROM children WHERE registration_no = @regNo", conn))
                        {
                            cmd.Parameters.AddWithValue("@regNo", regNoStr);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Patient deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the table
                        btnRefresh_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting record: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (patient_information_GridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient row to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!patient_information_GridView.Columns.Contains("registration_no"))
            {
                MessageBox.Show("Please switch to a view that contains the Registration Number to edit.", "Invalid View", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string regNoStr = patient_information_GridView.SelectedRows[0].Cells["registration_no"].Value?.ToString() ?? "";

            // Open the Registration form in "Edit Mode" by passing the Registration Number
            Patient_Registration editForm = new Patient_Registration(regNoStr);
            editForm.ShowDialog(); // Use ShowDialog so the program waits for the edit to finish

            // Refresh the table automatically after the edit form closes
            btnRefresh_Click(sender, e);
        }

        // --- SEARCH BAR LOGIC ---
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Verify that the GridView is holding a loaded DataTable
            if (patient_information_GridView.DataSource is DataTable dt)
            {
                // Grab what the user typed, and safely escape single quotes to prevent crashes
                string keyword = txtSearch.Text.Trim().Replace("'", "''");

                // If the user erased the search, clear the filter and show all rows
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = string.Empty;
                    return;
                }

                // Build a dynamic search filter based on which columns currently exist in the view
                List<string> filters = new List<string>();

                // Search by Registration Number (converting the integer to string temporarily to allow partial match)
                if (dt.Columns.Contains("registration_no"))
                {
                    filters.Add($"Convert(registration_no, 'System.String') LIKE '%{keyword}%'");
                }

                // Search by Child Name
                if (dt.Columns.Contains("child_full_name"))
                {
                    filters.Add($"child_full_name LIKE '%{keyword}%'");
                }

                // Search by Mother Name (in case they are looking at the Mothers-only table)
                if (dt.Columns.Contains("mother_full_name"))
                {
                    filters.Add($"mother_full_name LIKE '%{keyword}%'");
                }

                // Apply the filters if any searchable columns exist
                if (filters.Count > 0)
                {
                    dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                }
                else
                {
                    dt.DefaultView.RowFilter = "1 = 0"; // Hide rows if the view doesn't support searching
                }
            }
        }
    }
}
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Vaccination_Program
{
    public partial class Scheduling_System : Form
    {
        private string connectionString = "Server=localhost;Database=child_immunization_db;Uid=root;Pwd=08132003JLeo;";
        private long selectedChildId = 0;

        public Scheduling_System()
        {
            InitializeComponent();
        }

        private void Scheduling_System_Load(object sender, EventArgs e)
        {
            LoadPatients("");

            // Populate the primary vaccine list
            cmbVaccine.Items.AddRange(new string[] {
                "BCG",
                "Hepatitis B",
                "DPT-HiB-HepB",
                "OPV",
                "IPV",
                "PCV",
                "MMR"
            });
        }

        // --- PATIENT SEARCH AND SELECTION ---

        private void LoadPatients(string keyword)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT child_id, registration_no AS 'Reg No.', 
                               CONCAT(last_name, ', ', first_name) AS 'Child Name', 
                               date_of_birth AS 'DOB'
                        FROM children
                        WHERE last_name LIKE @kw OR first_name LIKE @kw OR registration_no LIKE @kw
                        ORDER BY last_name, first_name LIMIT 50;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvPatients.DataSource = dt;

                            // Hide the internal ID column so the user doesn't see it
                            if (dgvPatients.Columns["child_id"] != null)
                                dgvPatients.Columns["child_id"].Visible = false;

                            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading patients: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPatients(txtSearch.Text.Trim());
        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPatients.Rows[e.RowIndex];
                selectedChildId = Convert.ToInt64(row.Cells["child_id"].Value);
                string regNo = row.Cells["Reg No."].Value.ToString();
                string name = row.Cells["Child Name"].Value.ToString();

                lblSelectedPatient.Text = $"Selected Patient: [{regNo}] {name}";
            }
        }

        //DYNAMIC DOSE DROPDOWN

        private void cmbVaccine_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the dose list whenever a new vaccine is selected
            cmbDose.Items.Clear();
            string selectedVaccine = cmbVaccine.SelectedItem?.ToString() ?? "";

            switch (selectedVaccine)
            {
                case "BCG":
                case "Hepatitis B":
                    cmbDose.Items.Add("Standard Dose");
                    break;
                case "IPV":
                case "MMR":
                    cmbDose.Items.AddRange(new string[] { "Dose 1", "Dose 2" });
                    break;
                case "DPT-HiB-HepB":
                case "OPV":
                case "PCV":
                    cmbDose.Items.AddRange(new string[] { "Dose 1", "Dose 2", "Dose 3" });
                    break;
            }

            if (cmbDose.Items.Count > 0)
                cmbDose.SelectedIndex = 0; // Auto-select the first option
        }

        //SAVE THE SCHEDULE

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            if (selectedChildId == 0)
            {
                MessageBox.Show("Please select a patient from the list first.", "No Patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbVaccine.SelectedItem == null || cmbDose.SelectedItem == null)
            {
                MessageBox.Show("Please select both a vaccine and a dose.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string vaccine = cmbVaccine.SelectedItem.ToString();
            string dose = cmbDose.SelectedItem.ToString();
            DateTime scheduledDate = monthCalendar1.SelectionStart.Date;

            string upsertQuery = GenerateUpsertQuery(vaccine, dose);
            if (string.IsNullOrEmpty(upsertQuery)) return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(upsertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@cId", selectedChildId);
                        cmd.Parameters.AddWithValue("@date", scheduledDate.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Successfully scheduled {vaccine} ({dose})\nfor Date: {scheduledDate.ToShortDateString()}",
                                    "Schedule Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving schedule: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper method that safely generates the correct SQL query depending on the dose selected
        private string GenerateUpsertQuery(string vaccine, string dose)
        {
            string table = "";
            string column = "";

            switch (vaccine)
            {
                case "BCG":
                    table = "vacc_bcg"; column = "after_24hrs_date";
                    break;
                case "Hepatitis B":
                    table = "vacc_hepb_birth"; column = "after_24hrs_date";
                    break;
                case "DPT-HiB-HepB":
                    table = "vacc_dpt_hib_hepb"; column = dose.Replace(" ", "").ToLower() + "_date";
                    break;
                case "OPV":
                    table = "vacc_opv"; column = dose.Replace(" ", "").ToLower() + "_date";
                    break;
                case "IPV":
                    table = "vacc_ipv"; column = dose.Replace(" ", "").ToLower() + "_date";
                    break;
                case "PCV":
                    table = "vacc_pcv"; column = dose.Replace(" ", "").ToLower() + "_date";
                    break;
                case "MMR":
                    table = "vacc_mmr"; column = dose.Replace(" ", "").ToLower() + "_date";
                    break;
            }

            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(column)) return "";

            // The UPSERT command: Inserts a new row if missing, or updates the specific column if it already exists
            return $"INSERT INTO {table} (child_id, {column}) VALUES (@cId, @date) ON DUPLICATE KEY UPDATE {column} = @date;";
        }
    }
}
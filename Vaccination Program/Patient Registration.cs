using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Vaccination_Program
{
    public partial class Patient_Registration : Form
    {
        private string connectionString = "Server=localhost;Database=child_immunization_db;Uid=root;Pwd=your_password;";

        public Patient_Registration()
        {
            InitializeComponent();
        }

        private void btnSavePatient_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChildLastName.Text) || string.IsNullOrWhiteSpace(txtChildFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtMotherLastName.Text) || string.IsNullOrWhiteSpace(txtMotherFirstName.Text) ||
                cmbSex.SelectedItem == null || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insert Mother
                            string insertMotherQuery = "INSERT INTO mothers (last_name, first_name, middle_initial) VALUES (@mLast, @mFirst, @mMI);";
                            long motherId;
                            using (MySqlCommand cmdMother = new MySqlCommand(insertMotherQuery, connection, transaction))
                            {
                                cmdMother.Parameters.AddWithValue("@mLast", txtMotherLastName.Text.Trim());
                                cmdMother.Parameters.AddWithValue("@mFirst", txtMotherFirstName.Text.Trim());
                                cmdMother.Parameters.AddWithValue("@mMI", string.IsNullOrWhiteSpace(txtMotherMI.Text) ? (object)DBNull.Value : txtMotherMI.Text.Trim());
                                cmdMother.ExecuteNonQuery();
                                motherId = cmdMother.LastInsertedId;
                            }

                            // 2. Generate Registration Number
                            long newRegistrationNo;
                            using (MySqlCommand cmdReg = new MySqlCommand("SELECT IFNULL(MAX(registration_no), 1000) + 1 FROM children;", connection, transaction))
                            {
                                newRegistrationNo = Convert.ToInt64(cmdReg.ExecuteScalar());
                            }

                            // 3. Insert Child
                            DateTime dob = dtpBirthDate.Value.Date;
                            string insertChildQuery = @"
                                INSERT INTO children (registration_no, date_of_registration, mother_id, last_name, first_name, middle_initial, date_of_birth, sex, complete_address) 
                                VALUES (@regNo, CURDATE(), @motherId, @cLast, @cFirst, @cMI, @dob, @sex, @address);";

                            long childId;
                            using (MySqlCommand cmdChild = new MySqlCommand(insertChildQuery, connection, transaction))
                            {
                                cmdChild.Parameters.AddWithValue("@regNo", newRegistrationNo);
                                cmdChild.Parameters.AddWithValue("@motherId", motherId);
                                cmdChild.Parameters.AddWithValue("@cLast", txtChildLastName.Text.Trim());
                                cmdChild.Parameters.AddWithValue("@cFirst", txtChildFirstName.Text.Trim());
                                cmdChild.Parameters.AddWithValue("@cMI", string.IsNullOrWhiteSpace(txtChildMI.Text) ? (object)DBNull.Value : txtChildMI.Text.Trim());
                                cmdChild.Parameters.AddWithValue("@dob", dob.ToString("yyyy-MM-dd"));
                                cmdChild.Parameters.AddWithValue("@sex", cmbSex.SelectedItem.ToString());
                                cmdChild.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                                cmdChild.ExecuteNonQuery();
                                childId = cmdChild.LastInsertedId;
                            }

                            // 4. Baseline Status
                            using (MySqlCommand cmdStatus = new MySqlCommand("INSERT INTO immunization_status (child_id) VALUES (@childId);", connection, transaction))
                            {
                                cmdStatus.Parameters.AddWithValue("@childId", childId);
                                cmdStatus.ExecuteNonQuery();
                            }

                            // 5. Process Birth Vaccines (Using Explicit Separated Inputs)
                            InsertExplicitBirthVaccine(connection, transaction, childId, "vacc_bcg", dtpBCG_Within, dtpBCG_After);
                            InsertExplicitBirthVaccine(connection, transaction, childId, "vacc_hepb_birth", dtpHepB_Within, dtpHepB_After);

                            // 6. Process Multi-Dose Vaccines
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_dpt_hib_hepb", dtpDPT1, dtpDPT2, dtpDPT3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_opv", dtpOPV1, dtpOPV2, dtpOPV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_pcv", dtpPCV1, dtpPCV2, dtpPCV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_ipv", dtpIPV1, dtpIPV2, null);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_mmr", dtpMMR1, dtpMMR2, null);

                            transaction.Commit();
                            MessageBox.Show($"Patient successfully registered!\nRegistration Number: {newRegistrationNo}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("An error occurred. No data was saved.\n\nDetails: " + ex.Message, "Transaction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection error: " + ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- HELPER METHODS ---

        // Helper for Separated Birth Vaccines (BCG, HepB)
        private void InsertExplicitBirthVaccine(MySqlConnection conn, MySqlTransaction trans, long childId, string tableName, DateTimePicker dtpWithin, DateTimePicker dtpAfter)
        {
            // If neither is checked, skip inserting
            if (!dtpWithin.Checked && !dtpAfter.Checked) return;

            string query = $"INSERT INTO {tableName} (child_id";
            string values = "VALUES (@childId";

            if (dtpWithin.Checked) { query += ", within_24hrs_date"; values += ", @d1"; }
            if (dtpAfter.Checked) { query += ", after_24hrs_date"; values += ", @d2"; }

            query += ") " + values + ");";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@childId", childId);
                if (dtpWithin.Checked) cmd.Parameters.AddWithValue("@d1", dtpWithin.Value.Date.ToString("yyyy-MM-dd"));
                if (dtpAfter.Checked) cmd.Parameters.AddWithValue("@d2", dtpAfter.Value.Date.ToString("yyyy-MM-dd"));

                cmd.ExecuteNonQuery();
            }
        }

        // Helper for Multi-Dose Vaccines
        private void InsertMultiDoseVaccine(MySqlConnection conn, MySqlTransaction trans, long childId, string tableName, DateTimePicker dtp1, DateTimePicker dtp2, DateTimePicker dtp3)
        {
            if (!dtp1.Checked && !dtp2.Checked && (dtp3 == null || !dtp3.Checked)) return;

            string query = $"INSERT INTO {tableName} (child_id";
            string values = "VALUES (@childId";

            if (dtp1.Checked) { query += ", dose1_date"; values += ", @d1"; }
            if (dtp2.Checked) { query += ", dose2_date"; values += ", @d2"; }
            if (dtp3 != null && dtp3.Checked) { query += ", dose3_date"; values += ", @d3"; }

            query += ") " + values + ");";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@childId", childId);
                if (dtp1.Checked) cmd.Parameters.AddWithValue("@d1", dtp1.Value.Date.ToString("yyyy-MM-dd"));
                if (dtp2.Checked) cmd.Parameters.AddWithValue("@d2", dtp2.Value.Date.ToString("yyyy-MM-dd"));
                if (dtp3 != null && dtp3.Checked) cmd.Parameters.AddWithValue("@d3", dtp3.Value.Date.ToString("yyyy-MM-dd"));

                cmd.ExecuteNonQuery();
            }
        }

        private void ClearForm()
        {
            txtChildLastName.Clear();
            txtChildFirstName.Clear();
            txtChildMI.Clear();
            txtMotherLastName.Clear();
            txtMotherFirstName.Clear();
            txtMotherMI.Clear();
            txtAddress.Clear();
            cmbSex.SelectedIndex = -1;
            dtpBirthDate.Value = DateTime.Now;

            // Uncheck all date pickers
            dtpBCG_Within.Checked = false; dtpBCG_After.Checked = false;
            dtpHepB_Within.Checked = false; dtpHepB_After.Checked = false;
            dtpDPT1.Checked = false; dtpDPT2.Checked = false; dtpDPT3.Checked = false;
            dtpOPV1.Checked = false; dtpOPV2.Checked = false; dtpOPV3.Checked = false;
            dtpPCV1.Checked = false; dtpPCV2.Checked = false; dtpPCV3.Checked = false;
            dtpIPV1.Checked = false; dtpIPV2.Checked = false;
            dtpMMR1.Checked = false; dtpMMR2.Checked = false;
        }

        private void Patient_Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
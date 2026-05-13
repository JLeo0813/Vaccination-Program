using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Vaccination_Program
{
    public partial class Patient_Registration : Form
    {
        private string connectionString = "Server=localhost;Database=child_immunization_db;Uid=root;Pwd=08132003JLeo;";
        private bool isEditMode = false;
        private string editRegNo = "";
        private long editChildId = 0;
        private long editMotherId = 0;

        public Patient_Registration()
        {
            InitializeComponent();
        }

        // This constructor is called by the Edit button
        public Patient_Registration(string registrationNo)
        {
            InitializeComponent();
            isEditMode = true;
            editRegNo = registrationNo;

            // Change UI elements to indicate Edit Mode
            this.Text = "Edit Patient Information";
            btnSavePatient.Text = "Update Patient";
        }

        private void btnSavePatient_Click(object sender, EventArgs e)
        {
            // 1. Basic Validation
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
                            long motherId, childId;
                            long regNoToUse;


                            // Editing an Existing Patient

                            if (isEditMode)
                            {
                                // Update Mother
                                string updateMother = "UPDATE mothers SET last_name=@mLast, first_name=@mFirst, middle_initial=@mMI WHERE mother_id=@mId;";
                                using (MySqlCommand cmd = new MySqlCommand(updateMother, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@mId", editMotherId);
                                    cmd.Parameters.AddWithValue("@mLast", txtMotherLastName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@mFirst", txtMotherFirstName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@mMI", string.IsNullOrWhiteSpace(txtMotherMI.Text) ? (object)DBNull.Value : txtMotherMI.Text.Trim());
                                    cmd.ExecuteNonQuery();
                                }

                                // Update Child
                                string updateChild = @"UPDATE children SET last_name=@cLast, first_name=@cFirst, middle_initial=@cMI, 
                                               date_of_birth=@dob, sex=@sex, complete_address=@address WHERE child_id=@cId;";
                                using (MySqlCommand cmd = new MySqlCommand(updateChild, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@cId", editChildId);
                                    cmd.Parameters.AddWithValue("@cLast", txtChildLastName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@cFirst", txtChildFirstName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@cMI", string.IsNullOrWhiteSpace(txtChildMI.Text) ? (object)DBNull.Value : txtChildMI.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dob", dtpBirthDate.Value.Date.ToString("yyyy-MM-dd"));
                                    cmd.Parameters.AddWithValue("@sex", cmbSex.SelectedItem?.ToString()?.Substring(0, 1).ToUpper() ?? "");
                                    cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                                    cmd.ExecuteNonQuery();
                                }

                                // Wipe old vaccines so we can cleanly insert the updated checkboxes
                                string[] tablesToClear = { "vacc_bcg", "vacc_hepb_birth", "vacc_dpt_hib_hepb", "vacc_opv", "vacc_pcv", "vacc_ipv", "vacc_mmr" };
                                foreach (string table in tablesToClear)
                                {
                                    using (MySqlCommand cmd = new MySqlCommand($"DELETE FROM {table} WHERE child_id=@cId", connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@cId", editChildId);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                motherId = editMotherId;
                                childId = editChildId;
                                regNoToUse = Convert.ToInt64(editRegNo);
                            }
                            // Registering a New Patient
                            else
                            {
                                // 1. Insert Mother
                                string insertMotherQuery = "INSERT INTO mothers (last_name, first_name, middle_initial) VALUES (@mLast, @mFirst, @mMI);";
                                using (MySqlCommand cmdMother = new MySqlCommand(insertMotherQuery, connection, transaction))
                                {
                                    cmdMother.Parameters.AddWithValue("@mLast", txtMotherLastName.Text.Trim());
                                    cmdMother.Parameters.AddWithValue("@mFirst", txtMotherFirstName.Text.Trim());
                                    cmdMother.Parameters.AddWithValue("@mMI", string.IsNullOrWhiteSpace(txtMotherMI.Text) ? (object)DBNull.Value : txtMotherMI.Text.Trim());
                                    cmdMother.ExecuteNonQuery();
                                    motherId = cmdMother.LastInsertedId;
                                }

                                // 2. Generate Registration Number
                                using (MySqlCommand cmdReg = new MySqlCommand("SELECT IFNULL(MAX(registration_no), 1000) + 1 FROM children;", connection, transaction))
                                {
                                    regNoToUse = Convert.ToInt64(cmdReg.ExecuteScalar());
                                }

                                // 3. Insert Child
                                DateTime dob = dtpBirthDate.Value.Date;
                                string insertChildQuery = @"
                            INSERT INTO children (registration_no, date_of_registration, mother_id, last_name, first_name, middle_initial, date_of_birth, sex, complete_address) 
                            VALUES (@regNo, CURDATE(), @motherId, @cLast, @cFirst, @cMI, @dob, @sex, @address);";

                                using (MySqlCommand cmdChild = new MySqlCommand(insertChildQuery, connection, transaction))
                                {
                                    cmdChild.Parameters.AddWithValue("@regNo", regNoToUse);
                                    cmdChild.Parameters.AddWithValue("@motherId", motherId);
                                    cmdChild.Parameters.AddWithValue("@cLast", txtChildLastName.Text.Trim());
                                    cmdChild.Parameters.AddWithValue("@cFirst", txtChildFirstName.Text.Trim());
                                    cmdChild.Parameters.AddWithValue("@cMI", string.IsNullOrWhiteSpace(txtChildMI.Text) ? (object)DBNull.Value : txtChildMI.Text.Trim());
                                    cmdChild.Parameters.AddWithValue("@dob", dob.ToString("yyyy-MM-dd"));
                                    cmdChild.Parameters.AddWithValue("@sex", cmbSex.SelectedItem?.ToString()?.Substring(0, 1).ToUpper() ?? "");
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
                            }

                            // SHARED LOGIC: Process Vaccines For Both
                            InsertExplicitBirthVaccine(connection, transaction, childId, "vacc_bcg", dtpBCG_Within, dtpBCG_After);
                            InsertExplicitBirthVaccine(connection, transaction, childId, "vacc_hepb_birth", dtpHepB_Within, dtpHepB_After);

                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_dpt_hib_hepb", dtpDPT1, dtpDPT2, dtpDPT3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_opv", dtpOPV1, dtpOPV2, dtpOPV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_pcv", dtpPCV1, dtpPCV2, dtpPCV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_ipv", dtpIPV1, dtpIPV2, null);
                            InsertMultiDoseVaccine(connection, transaction, childId, "vacc_mmr", dtpMMR1, dtpMMR2, null);

                            // Commit the transaction to save everything
                            transaction.Commit();

                            // Show appropriate success message based on mode
                            string successMsg = isEditMode ? "Patient successfully updated!" : $"Patient successfully registered!\nRegistration Number: {regNoToUse}";
                            MessageBox.Show(successMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Close the form if we were just editing, otherwise clear it for the next new patient
                            if (isEditMode)
                            {
                                this.Close();
                            }
                            else
                            {
                                ClearForm();
                            }
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
        private void InsertMultiDoseVaccine(MySqlConnection conn, MySqlTransaction trans, long childId, string tableName, DateTimePicker dtp1, DateTimePicker dtp2, DateTimePicker? dtp3)
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
            // 1. Clear all text boxes
            txtChildLastName.Clear();
            txtChildFirstName.Clear();
            txtChildMI.Clear();
            txtMotherLastName.Clear();
            txtMotherFirstName.Clear();
            txtMotherMI.Clear();
            txtAddress.Clear();

            // 2. Reset combo box and birth date
            cmbSex.SelectedIndex = -1;
            dtpBirthDate.Value = DateTime.Now;

            // 3. Uncheck all vaccine date pickers
            dtpBCG_Within.Checked = false;
            dtpBCG_After.Checked = false;

            dtpHepB_Within.Checked = false;
            dtpHepB_After.Checked = false;

            dtpDPT1.Checked = false; dtpDPT2.Checked = false; dtpDPT3.Checked = false;
            dtpOPV1.Checked = false; dtpOPV2.Checked = false; dtpOPV3.Checked = false;
            dtpPCV1.Checked = false; dtpPCV2.Checked = false; dtpPCV3.Checked = false;
            dtpIPV1.Checked = false; dtpIPV2.Checked = false;
            dtpMMR1.Checked = false; dtpMMR2.Checked = false;

            this.ActiveControl = txtChildFirstName;
        }

        private void Patient_Registration_Load(object sender, EventArgs e)
        {
            ClearForm();
            if (isEditMode)
            {
                LoadPatientDataForEdit();
            }
        }

        private void SetDateTimePicker(DateTimePicker dtp, object dbValue)
        {
            if (dbValue != null && dbValue != DBNull.Value)
            {
                dtp.Checked = true;
                dtp.Value = Convert.ToDateTime(dbValue);
            }
            else
            {
                dtp.Checked = false;
            }
        }

        private void LoadPatientDataForEdit()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Fetch Core Child and Mother Info
                    string coreQuery = @"SELECT c.child_id, c.mother_id, c.last_name AS cLast, c.first_name AS cFirst, c.middle_initial AS cMI, 
                                        c.date_of_birth, c.sex, c.complete_address,
                                        m.last_name AS mLast, m.first_name AS mFirst, m.middle_initial AS mMI
                                 FROM children c JOIN mothers m ON c.mother_id = m.mother_id 
                                 WHERE c.registration_no = @regNo";

                    using (MySqlCommand cmd = new MySqlCommand(coreQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@regNo", editRegNo);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                editChildId = Convert.ToInt64(reader["child_id"]);
                                editMotherId = Convert.ToInt64(reader["mother_id"]);

                                txtChildLastName.Text = reader["cLast"].ToString();
                                txtChildFirstName.Text = reader["cFirst"].ToString();
                                txtChildMI.Text = reader["cMI"].ToString();

                                txtMotherLastName.Text = reader["mLast"].ToString();
                                txtMotherFirstName.Text = reader["mFirst"].ToString();
                                txtMotherMI.Text = reader["mMI"].ToString();

                                dtpBirthDate.Value = Convert.ToDateTime(reader["date_of_birth"]);
                                txtAddress.Text = reader["complete_address"].ToString();

                                string sex = reader["sex"]?.ToString() ?? "";
                                cmbSex.SelectedIndex = cmbSex.FindStringExact(sex == "M" ? "Male" : (sex == "F" ? "Female" : sex));
                            }
                        }
                    }

                    // 2. Fetch Existing Vaccine Dates
                    if (editChildId > 0)
                    {
                        string vaccQuery = @"
                    SELECT
                        b.within_24hrs_date AS bcg_w, b.after_24hrs_date AS bcg_a,
                        hb.within_24hrs_date AS hepb_w, hb.after_24hrs_date AS hepb_a,
                        dpt.dose1_date AS dpt1, dpt.dose2_date AS dpt2, dpt.dose3_date AS dpt3,
                        opv.dose1_date AS opv1, opv.dose2_date AS opv2, opv.dose3_date AS opv3,
                        pcv.dose1_date AS pcv1, pcv.dose2_date AS pcv2, pcv.dose3_date AS pcv3,
                        ipv.dose1_date AS ipv1, ipv.dose2_date AS ipv2,
                        mmr.dose1_date AS mmr1, mmr.dose2_date AS mmr2
                    FROM children c
                    LEFT JOIN vacc_bcg b ON c.child_id = b.child_id
                    LEFT JOIN vacc_hepb_birth hb ON c.child_id = hb.child_id
                    LEFT JOIN vacc_dpt_hib_hepb dpt ON c.child_id = dpt.child_id
                    LEFT JOIN vacc_opv opv ON c.child_id = opv.child_id
                    LEFT JOIN vacc_pcv pcv ON c.child_id = pcv.child_id
                    LEFT JOIN vacc_ipv ipv ON c.child_id = ipv.child_id
                    LEFT JOIN vacc_mmr mmr ON c.child_id = mmr.child_id
                    WHERE c.child_id = @cId";

                        using (MySqlCommand cmdVacc = new MySqlCommand(vaccQuery, conn))
                        {
                            cmdVacc.Parameters.AddWithValue("@cId", editChildId);
                            using (MySqlDataReader reader = cmdVacc.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate Birth Vaccines
                                    SetDateTimePicker(dtpBCG_Within, reader["bcg_w"]);
                                    SetDateTimePicker(dtpBCG_After, reader["bcg_a"]);
                                    SetDateTimePicker(dtpHepB_Within, reader["hepb_w"]);
                                    SetDateTimePicker(dtpHepB_After, reader["hepb_a"]);

                                    // Populate DPT
                                    SetDateTimePicker(dtpDPT1, reader["dpt1"]);
                                    SetDateTimePicker(dtpDPT2, reader["dpt2"]);
                                    SetDateTimePicker(dtpDPT3, reader["dpt3"]);

                                    // Populate OPV
                                    SetDateTimePicker(dtpOPV1, reader["opv1"]);
                                    SetDateTimePicker(dtpOPV2, reader["opv2"]);
                                    SetDateTimePicker(dtpOPV3, reader["opv3"]);

                                    // Populate PCV
                                    SetDateTimePicker(dtpPCV1, reader["pcv1"]);
                                    SetDateTimePicker(dtpPCV2, reader["pcv2"]);
                                    SetDateTimePicker(dtpPCV3, reader["pcv3"]);

                                    // Populate IPV
                                    SetDateTimePicker(dtpIPV1, reader["ipv1"]);
                                    SetDateTimePicker(dtpIPV2, reader["ipv2"]);

                                    // Populate MMR
                                    SetDateTimePicker(dtpMMR1, reader["mmr1"]);
                                    SetDateTimePicker(dtpMMR2, reader["mmr2"]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading patient data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
        }
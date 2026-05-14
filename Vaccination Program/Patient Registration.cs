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
                            DateTime dob = dtpBirthDate.Value.Date;

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
                                using (MySqlCommand cmdStatus = new MySqlCommand("INSERT INTO immunization_status (child_id, remarks) VALUES (@childId, @remarks);", connection, transaction))
                                {
                                    cmdStatus.Parameters.AddWithValue("@childId", childId);
                                    // If the box is empty, save NULL. If it has text, trim the extra spaces and save it.
                                    cmdStatus.Parameters.AddWithValue("@remarks", string.IsNullOrWhiteSpace(txtRemarks.Text) ? (object)DBNull.Value : txtRemarks.Text.Trim());
                                    cmdStatus.ExecuteNonQuery();
                                }
                            }

                            // SHARED LOGIC: PROCESS VACCINES FOR BOTH

                            InsertExplicitBirthVaccine(connection, transaction, childId, dob, "vacc_bcg", dtpBCG_Within, dtpBCG_After);
                            InsertExplicitBirthVaccine(connection, transaction, childId, dob, "vacc_hepb_birth", dtpHepB_Within, dtpHepB_After);

                            InsertMultiDoseVaccine(connection, transaction, childId, dob, "vacc_dpt_hib_hepb", dtpDPT1, dtpDPT2, dtpDPT3);
                            InsertMultiDoseVaccine(connection, transaction, childId, dob, "vacc_opv", dtpOPV1, dtpOPV2, dtpOPV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, dob, "vacc_pcv", dtpPCV1, dtpPCV2, dtpPCV3);
                            InsertMultiDoseVaccine(connection, transaction, childId, dob, "vacc_ipv", dtpIPV1, dtpIPV2, null);
                            InsertMultiDoseVaccine(connection, transaction, childId, dob, "vacc_mmr", dtpMMR1, dtpMMR2, null);

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
        private void InsertExplicitBirthVaccine(MySqlConnection conn, MySqlTransaction trans, long childId, DateTime dob, string tableName, DateTimePicker dtpWithin, DateTimePicker dtpAfter)
        {
            if (!dtpWithin.Checked && !dtpAfter.Checked) return;

            string query = $"INSERT INTO {tableName} (child_id";
            string values = "VALUES (@childId";

            // We now add the age columns to the query!
            if (dtpWithin.Checked) { query += ", within_24hrs_date, within_24hrs_age"; values += ", @d1, @a1"; }
            if (dtpAfter.Checked) { query += ", after_24hrs_date, after_24hrs_age"; values += ", @d2, @a2"; }

            query += ") " + values + ");";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@childId", childId);
                if (dtpWithin.Checked)
                {
                    cmd.Parameters.AddWithValue("@d1", dtpWithin.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@a1", CalculateAge(dob, dtpWithin.Value.Date)); // Calculates the age
                }
                if (dtpAfter.Checked)
                {
                    cmd.Parameters.AddWithValue("@d2", dtpAfter.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@a2", CalculateAge(dob, dtpAfter.Value.Date)); // Calculates the age
                }

                cmd.ExecuteNonQuery();
            }
        }

        // Helper for Multi-Dose Vaccines
        private void InsertMultiDoseVaccine(MySqlConnection conn, MySqlTransaction trans, long childId, DateTime dob, string tableName, DateTimePicker dtp1, DateTimePicker dtp2, DateTimePicker? dtp3)
        {
            if (!dtp1.Checked && !dtp2.Checked && (dtp3 == null || !dtp3.Checked)) return;

            string query = $"INSERT INTO {tableName} (child_id";
            string values = "VALUES (@childId";

            // We now add the age columns to the query!
            if (dtp1.Checked) { query += ", dose1_date, dose1_age"; values += ", @d1, @a1"; }
            if (dtp2.Checked) { query += ", dose2_date, dose2_age"; values += ", @d2, @a2"; }
            if (dtp3 != null && dtp3.Checked) { query += ", dose3_date, dose3_age"; values += ", @d3, @a3"; }

            query += ") " + values + ");";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@childId", childId);
                if (dtp1.Checked)
                {
                    cmd.Parameters.AddWithValue("@d1", dtp1.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@a1", CalculateAge(dob, dtp1.Value.Date)); // Calculates the age
                }
                if (dtp2.Checked)
                {
                    cmd.Parameters.AddWithValue("@d2", dtp2.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@a2", CalculateAge(dob, dtp2.Value.Date)); // Calculates the age
                }
                if (dtp3 != null && dtp3.Checked)
                {
                    cmd.Parameters.AddWithValue("@d3", dtp3.Value.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@a3", CalculateAge(dob, dtp3.Value.Date)); // Calculates the age
                }

                cmd.ExecuteNonQuery();
            }
        }

        // Helper to evaluate and update the CPAB (Protected at Birth) status
        private void UpdateCpabStatus(MySqlConnection conn, MySqlTransaction trans, long childId)
        {
            // Uses an UPSERT: Inserts a new record if it doesn't exist, or updates it if it does
            string query = @"
        INSERT INTO cpab (child_id, protected_at_birth) 
        VALUES (@cId, @cpab) 
        ON DUPLICATE KEY UPDATE protected_at_birth = @cpab;";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@cId", childId);
                cmd.Parameters.AddWithValue("@cpab", chkProtectedAtBirth.Checked ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        // Helper to automatically evaluate FIC/CIC status AND save Remarks
        private void EvaluateImmunizationStatus(MySqlConnection conn, MySqlTransaction trans, long childId)
        {
            bool hasBcg = dtpBCG_Within.Checked || dtpBCG_After.Checked;
            bool hasDpt3 = dtpDPT3.Checked;
            bool hasOpv3 = dtpOPV3.Checked;
            bool hasMmr2 = dtpMMR2.Checked;

            string query = @"
        UPDATE immunization_status 
        SET fic_bcg = @bcg, fic_dpt3 = @dpt3, fic_opv3 = @opv3, fic_mmr2 = @mmr2,
            cic_bcg = @bcg, cic_dpt3 = @dpt3, cic_opv3 = @opv3, cic_mmr2 = @mmr2,
            remarks = @remarks
        WHERE child_id = @cId;";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@cId", childId);
                cmd.Parameters.AddWithValue("@bcg", hasBcg ? 1 : 0);
                cmd.Parameters.AddWithValue("@dpt3", hasDpt3 ? 1 : 0);
                cmd.Parameters.AddWithValue("@opv3", hasOpv3 ? 1 : 0);
                cmd.Parameters.AddWithValue("@mmr2", hasMmr2 ? 1 : 0);
                cmd.Parameters.AddWithValue("@remarks", string.IsNullOrWhiteSpace(txtRemarks.Text) ? (object)DBNull.Value : txtRemarks.Text.Trim());
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
            txtRemarks.Clear();

            // 2. Reset combo box and birth date
            cmbSex.SelectedIndex = -1;
            dtpBirthDate.Value = DateTime.Now;

            // 3. Uncheck all vaccine date pickers
            chkProtectedAtBirth.Checked = false;
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

        private string CalculateAge(DateTime dob, DateTime vaccDate)
        {
            int days = (vaccDate.Date - dob.Date).Days;

            if (days < 0) return "Invalid"; // Prevents negative ages if dates are entered wrong
            if (days == 0) return "At Birth";
            if (days < 7) return days + " days";
            if (days < 30) return (days / 7) + " wks";

            int months = (int)(days / 30.436875);
            return months + " mos";
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

                    // 2. Fetch Existing Vaccine Dates and CPAB Status
                    if (editChildId > 0)
                    {
                        //Fetch CPAB Status
                        string cpabQuery = "SELECT protected_at_birth FROM cpab WHERE child_id = @cId";
                        using (MySqlCommand cmdCpab = new MySqlCommand(cpabQuery, conn))
                        {
                            cmdCpab.Parameters.AddWithValue("@cId", editChildId);
                            object cpabResult = cmdCpab.ExecuteScalar();
                            // If a record exists and equals 1, check the box. Otherwise, leave it unchecked.
                            chkProtectedAtBirth.Checked = (cpabResult != null && Convert.ToInt32(cpabResult) == 1);
                        }

                        // --- Fetch Remarks ---
                        string remarksQuery = "SELECT remarks FROM immunization_status WHERE child_id = @cId";
                        using (MySqlCommand cmdRemarks = new MySqlCommand(remarksQuery, conn))
                        {
                            cmdRemarks.Parameters.AddWithValue("@cId", editChildId);
                            object remarksResult = cmdRemarks.ExecuteScalar();
                            txtRemarks.Text = remarksResult?.ToString() ?? "";
                        }

                        // --- Fetch Existing Vaccine Dates ---
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
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
            // Populate the ComboBox with all 15 display options
            querySelector_ComboBox.Items.Add("1. All Mothers");
            querySelector_ComboBox.Items.Add("2. All Children");
            querySelector_ComboBox.Items.Add("3. Children with Mother's Info");
            querySelector_ComboBox.Items.Add("4. Maternal Td Vaccination");
            querySelector_ComboBox.Items.Add("5. Children Protected at Birth (CPAB)");
            querySelector_ComboBox.Items.Add("6. BCG Vaccination");
            querySelector_ComboBox.Items.Add("7. Hepatitis B at Birth");
            querySelector_ComboBox.Items.Add("8. DPT-HiB-HepB");
            querySelector_ComboBox.Items.Add("9. OPV - Oral Polio Vaccine");
            querySelector_ComboBox.Items.Add("10. IPV - Inactivated Polio Vaccine");
            querySelector_ComboBox.Items.Add("11. PCV - Pneumococcal Conjugate Vaccine");
            querySelector_ComboBox.Items.Add("12. MMR - Measles-Mumps-Rubella");
            querySelector_ComboBox.Items.Add("13. FIC / CIC Annual Reporting");
            querySelector_ComboBox.Items.Add("14. Full Vaccination Status Summary");
            querySelector_ComboBox.Items.Add("15. Detailed Vaccination Status (All)");

            // Select the first item by default to trigger the load
            querySelector_ComboBox.SelectedIndex = 0;
        }

        private void querySelector_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (querySelector_ComboBox.SelectedItem == null) return;

            string selectedOption = querySelector_ComboBox.SelectedItem.ToString();
            string queryToExecute = "";

            switch (selectedOption)
            {
                case "1. All Mothers":
                    queryToExecute = @"
                        SELECT mother_id, 
                               CONCAT(last_name, ', ', first_name, IFNULL(CONCAT(' ', middle_initial, '.'), '')) AS mother_full_name
                        FROM mothers
                        ORDER BY last_name, first_name;";
                    break;

                case "2. All Children":
                    queryToExecute = @"
                        SELECT child_id, registration_no, date_of_registration, family_serial_no,
                               CONCAT(last_name, ', ', first_name, IFNULL(CONCAT(' ', middle_initial, '.'), '')) AS child_full_name,
                               date_of_birth, TIMESTAMPDIFF(MONTH, date_of_birth, CURDATE()) AS age_in_months, sex, complete_address
                        FROM children
                        ORDER BY last_name, first_name;";
                    break;

                case "3. Children with Mother's Info":
                    queryToExecute = @"
                        SELECT c.registration_no, c.family_serial_no,
                               CONCAT(c.last_name, ', ', c.first_name, IFNULL(CONCAT(' ', c.middle_initial, '.'), '')) AS child_full_name,
                               c.date_of_birth, TIMESTAMPDIFF(MONTH, c.date_of_birth, CURDATE()) AS age_in_months,
                               c.sex, c.complete_address,
                               CONCAT(m.last_name, ', ', m.first_name, IFNULL(CONCAT(' ', m.middle_initial, '.'), '')) AS mother_full_name
                        FROM children c
                        JOIN mothers m ON c.mother_id = m.mother_id
                        ORDER BY c.last_name, c.first_name;";
                    break;

                case "4. Maternal Td Vaccination":
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

                case "5. Children Protected at Birth (CPAB)":
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

                case "6. BCG Vaccination":
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

                case "7. Hepatitis B at Birth":
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

                case "8. DPT-HiB-HepB":
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

                case "13. FIC / CIC Annual Reporting":
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

                case "14. Full Vaccination Status Summary":
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
                        LEFT JOIN mothers             m   ON c.mother_id  = m.mother_id
                        LEFT JOIN vacc_bcg            b   ON c.child_id   = b.child_id
                        LEFT JOIN vacc_hepb_birth     hb  ON c.child_id   = hb.child_id
                        LEFT JOIN vacc_dpt_hib_hepb   dpt ON c.child_id   = dpt.child_id
                        LEFT JOIN vacc_opv            opv ON c.child_id   = opv.child_id
                        LEFT JOIN vacc_ipv            ipv ON c.child_id   = ipv.child_id
                        LEFT JOIN vacc_pcv            pcv ON c.child_id   = pcv.child_id
                        LEFT JOIN vacc_mmr            mmr ON c.child_id   = mmr.child_id
                        LEFT JOIN cpab                cp  ON c.child_id   = cp.child_id
                        LEFT JOIN immunization_status ist ON c.child_id   = ist.child_id
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

                            patient_information_GridView.DataSource = dataTable;

                            // Formatting
                            patient_information_GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            patient_information_GridView.ReadOnly = true;
                            patient_information_GridView.AllowUserToAddRows = false;
                            patient_information_GridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

                            // Stretch wide text columns if they are present in the current query
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
            // Check if an option is actually selected
            if (querySelector_ComboBox.SelectedItem != null)
            {
                // Re-trigger the ComboBox event to fetch
            }
        }
    }
}

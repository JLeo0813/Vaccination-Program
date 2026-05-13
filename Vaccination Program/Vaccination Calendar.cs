using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Vaccination_Program
{
    public partial class Vaccination_Calendar : Form
    {
        private string connectionString = "Server=localhost;Database=child_immunization_db;Uid=root;Pwd=08132003JLeo;";

        // This master list will hold every single scheduled dose from the database
        private List<ScheduleItem> allSchedules = new List<ScheduleItem>();

        public Vaccination_Calendar()
        {
            InitializeComponent();
        }

        private void Vaccination_Calendar_Load(object sender, EventArgs e)
        {
            LoadAllSchedules();

            // Default the view to show today's schedule
            UpdateDailyView(DateTime.Today);
        }

        // --- DATA FETCHING & PARSING ---

        private void LoadAllSchedules()
        {
            allSchedules.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Pull a flat table of every child and every possible vaccine date
                    string query = @"
                        SELECT c.registration_no, CONCAT(c.last_name, ', ', c.first_name) AS child_name,
                               b.after_24hrs_date AS bcg_date,
                               hb.after_24hrs_date AS hepb_date,
                               dpt.dose1_date AS dpt1, dpt.dose2_date AS dpt2, dpt.dose3_date AS dpt3,
                               opv.dose1_date AS opv1, opv.dose2_date AS opv2, opv.dose3_date AS opv3,
                               ipv.dose1_date AS ipv1, ipv.dose2_date AS ipv2,
                               pcv.dose1_date AS pcv1, pcv.dose2_date AS pcv2, pcv.dose3_date AS pcv3,
                               mmr.dose1_date AS mmr1, mmr.dose2_date AS mmr2
                        FROM children c
                        LEFT JOIN vacc_bcg b ON c.child_id = b.child_id
                        LEFT JOIN vacc_hepb_birth hb ON c.child_id = hb.child_id
                        LEFT JOIN vacc_dpt_hib_hepb dpt ON c.child_id = dpt.child_id
                        LEFT JOIN vacc_opv opv ON c.child_id = opv.child_id
                        LEFT JOIN vacc_ipv ipv ON c.child_id = ipv.child_id
                        LEFT JOIN vacc_pcv pcv ON c.child_id = pcv.child_id
                        LEFT JOIN vacc_mmr mmr ON c.child_id = mmr.child_id;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string regNo = reader["registration_no"].ToString();
                            string name = reader["child_name"].ToString();

                            // Feed every single column through our extractor to build the schedule list
                            ExtractAndAdd(reader, "bcg_date", regNo, name, "BCG", "Standard Dose");
                            ExtractAndAdd(reader, "hepb_date", regNo, name, "Hepatitis B", "Standard Dose");

                            ExtractAndAdd(reader, "dpt1", regNo, name, "DPT-HiB-HepB", "Dose 1");
                            ExtractAndAdd(reader, "dpt2", regNo, name, "DPT-HiB-HepB", "Dose 2");
                            ExtractAndAdd(reader, "dpt3", regNo, name, "DPT-HiB-HepB", "Dose 3");

                            ExtractAndAdd(reader, "opv1", regNo, name, "OPV", "Dose 1");
                            ExtractAndAdd(reader, "opv2", regNo, name, "OPV", "Dose 2");
                            ExtractAndAdd(reader, "opv3", regNo, name, "OPV", "Dose 3");

                            ExtractAndAdd(reader, "ipv1", regNo, name, "IPV", "Dose 1");
                            ExtractAndAdd(reader, "ipv2", regNo, name, "IPV", "Dose 2");

                            ExtractAndAdd(reader, "pcv1", regNo, name, "PCV", "Dose 1");
                            ExtractAndAdd(reader, "pcv2", regNo, name, "PCV", "Dose 2");
                            ExtractAndAdd(reader, "pcv3", regNo, name, "PCV", "Dose 3");

                            ExtractAndAdd(reader, "mmr1", regNo, name, "MMR", "Dose 1");
                            ExtractAndAdd(reader, "mmr2", regNo, name, "MMR", "Dose 2");
                        }
                    }

                    // Bold all dates in the calendar that have at least one appointment
                    HighlightCalendarDates();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading calendar: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper: Converts a database date into an Appointment object if the date exists
        private void ExtractAndAdd(MySqlDataReader reader, string columnName, string regNo, string name, string vaccine, string dose)
        {
            if (reader[columnName] != DBNull.Value)
            {
                allSchedules.Add(new ScheduleItem
                {
                    RegistrationNo = regNo,
                    PatientName = name,
                    Vaccine = vaccine,
                    Dose = dose,
                    ScheduledDate = Convert.ToDateTime(reader[columnName]).Date
                });
            }
        }

        // --- CALENDAR UI LOGIC ---

        private void HighlightCalendarDates()
        {
            // Extract unique dates from the list
            DateTime[] boldDates = allSchedules.Select(s => s.ScheduledDate).Distinct().ToArray();

            // Apply them to the calendar
            monthCalendar1.BoldedDates = boldDates;
        }

        // Triggered when the user clicks a date on the calendar
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            UpdateDailyView(e.Start);
        }

        private void UpdateDailyView(DateTime selectedDate)
        {
            lblSelectedDate.Text = $"Schedules for: {selectedDate.ToLongDateString()}";

            // Filter our master list to only show appointments matching the clicked date
            var dailySchedules = allSchedules
                .Where(s => s.ScheduledDate == selectedDate)
                .Select(s => new
                {
                    RegistrationNo = s.RegistrationNo,
                    PatientName = s.PatientName,
                    Vaccine = s.Vaccine,
                    Dose = s.Dose
                })
                .ToList();

            // Bind the filtered list to the grid view
            dgvSchedule.DataSource = dailySchedules;

            // Format the columns cleanly
            if (dgvSchedule.Columns.Count > 0)
            {
                dgvSchedule.Columns["RegistrationNo"].HeaderText = "Reg. No.";
                dgvSchedule.Columns["PatientName"].HeaderText = "Patient Name";
                dgvSchedule.Columns["Vaccine"].HeaderText = "Scheduled Vaccine";
                dgvSchedule.Columns["Dose"].HeaderText = "Dose";

                dgvSchedule.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    // A lightweight data class to hold appointment info in memory
    public class ScheduleItem
    {
        public string RegistrationNo { get; set; } = "";
        public string PatientName { get; set; } = "";
        public string Vaccine { get; set; } = "";
        public string Dose { get; set; } = "";
        public DateTime ScheduledDate { get; set; }
    }
}
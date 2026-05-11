using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vaccination_Program
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
            // You can leave this empty if nothing needs to load when the menu opens
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Patient_Registration form and display it
            Patient_Registration registrationForm = new Patient_Registration();
            registrationForm.Show();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Patient_Information form and display it
            Patient_Information informationForm = new Patient_Information();
            informationForm.Show();
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Vaccination_Calendar form and display it
            Vaccination_Calendar calendarForm = new Vaccination_Calendar();
            calendarForm.Show();
        }

        private void btnScheduling_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Vaccination_Scheduling form and display it
            Scheduling_System schedulingForm = new Scheduling_System();
            schedulingForm.Show();
        }
    }
}
using System;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class frmHostelPayments : Form
    {
        public frmHostelPayments()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string independence = null;
                if (checkBox2.Checked == true)
                {
                    independence = "Independent";
                }else if (checkBox1.Checked == true)
                {
                    independence = "NotIndependent";
                }
                Properties.Settings.Default["hostelpayments"] = independence;
                Properties.Settings.Default.Save();
                MessageBox.Show("Successful", "Hostel Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class frmSmsSettings : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public frmSmsSettings()
        {
            InitializeComponent();
        }

        private void frmSmsSettings_Load(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSmsSettings frm = new frmSmsSettings();
            frm.ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                string allowsettings = null;
                if (radioButton1.Checked == true)
                {
                    allowsettings = "Yes";
                }
                else
                {
                    allowsettings = "No";
                }
                if (radioButton2.Checked == true)
                {
                    allowsettings = "No";
                }
              
                Properties.Settings.Default["smsallow"] = allowsettings;
                Properties.Settings.Default.Save();
                Properties.Settings.Default["smsusername"] = username.Text;
                Properties.Settings.Default.Save();
                Properties.Settings.Default["smspassword"] = passwords.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Successful", "SMS Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

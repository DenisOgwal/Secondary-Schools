using System;
using System.Drawing;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class frmStudentAcess : Form
    {
        public frmStudentAcess()
        {
            InitializeComponent();
        }

        private void frmStudentAcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == System.Windows.Forms.DialogResult.Yes)
            {
                this.Hide();
                frmLogin frm = new frmLogin();
                frm.Show();
            }
            else
            {
                this.Hide();
                frmStudentAcess frm = new frmStudentAcess();
                frm.password.Text = password.Text;
                frm.Username.Text = Username.Text;
                frm.Show();
            }
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmStudentResults frm = new frmStudentResults();
            frm.stdno.Text = password.Text;
            frm.ShowDialog();
        }

        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmTeacherDetails frm = new frmTeacherDetails();
            frm.ShowDialog();
        }

        private void issuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmStudentComplaints frm = new frmStudentComplaints();
            frm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            this.Hide();
            //frm.cmbUsertype.Text = "";
            frm.txtUserName.Text = "";
            frm.txtPassword.Text = "";
            //frm.cmbUsertype.Focus();
            frm.Show();
        }

        private void viewFeesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmStudentFeesDetails frm = new frmStudentFeesDetails();
            frm.stdno.Text = password.Text;
            frm.ShowDialog();
        }

        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentLibrary frm = new frmStudentLibrary();
            frm.stdno.Text =password.Text;
            frm.label6.Text = password.Text;
            frm.label7.Text = Username.Text;
            frm.ShowDialog();
        }

        private void frmStudentAcess_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(@"C:\EssentialSchoolsFIles\MainImage.jpg");

            }
            catch (Exception)
            {

            }
        }
    }
    }


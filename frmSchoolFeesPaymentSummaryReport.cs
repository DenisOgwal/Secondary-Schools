using System;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class frmSchoolFeesPaymentSummaryReport : Form
    {
        public frmSchoolFeesPaymentSummaryReport()
        {
            InitializeComponent();
        }

        private void frmFeePaymentReceipt_Load(object sender, EventArgs e)
        {

        }

        private void frmStudentsFeesPaymentSummaryReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmSchoolFeesFinancialSummary frm = new  frmSchoolFeesFinancialSummary();
            frm.label1.Text = label1.Text;
            frm.label2.Text = label2.Text;
            frm.Show();
        }
    }
}

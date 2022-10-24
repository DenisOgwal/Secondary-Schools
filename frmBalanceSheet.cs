using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace College_Management_System
{
    public partial class frmBalanceSheet : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        string BarPrinter = null;
        string KitchenPrinter = null;
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        public frmBalanceSheet()
        {
            InitializeComponent();
        }

        private void frmFinancialSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.UserType.Text = label1.Text;
            frm.User.Text = label2.Text;
            frm.Show();*/
        }
        private void Autocompleteyear()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Session) FROM Batch", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                year.Items.Clear();
                year2.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    year.Items.Add(drow[0].ToString());
                    year2.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AutocompleteTerm()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Semester) FROM Batch", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                term.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    term.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmFinancialSummary_Load(object sender, EventArgs e)
        {
            //Left = Top = 0;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            Autocompleteyear();
            AutocompleteTerm();
        }

        int s1totalpaidd = 0;
        int totals1totalpaidd = 0;
     
       
        int htotals1totalpaidd = 0;
        int ts1totalpaidd = 0;
        int ttotals1totalpaidd = 0;
        int hts1totalpaidd = 0;
        int httotals1totalpaidd = 0;
       

    //
       
        //scholarship fee
       
        int stotals1totalpaidd = 0;
        int sts1totalpaidd = 0;
        //other incomes
        int incomespaidin = 0;
        int buspaymentdue = 0;
        int bushiredue = 0;
        int salarypaymentdue = 0;
        int feespaymentdue = 0;
        int scholarshpdue=0;
        int expensesdue = 0;
        int balanceforward = 0;
        int equipurchaseduepayment = 0;
        int purchaseduepayment = 0;
        int drawings = 0;
        int netincomes = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            //s.1 total paid
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from FeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "'", con);
                    shoolfeestotalpaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    shoolfeestotalpaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from FeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "'  and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from FeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "' and DueFees !=0", con);
                    feespaymentdue = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    feespaymentdue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /////end fees by year and term

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    buspaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    buspaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and DueFees !=0", con);
                    buspaymentdue = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    buspaymentdue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////////////////////////////////////////////////// end student bus payment by year and term

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    bushirepaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    bushirepaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and HireDueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(HireDueFees) from BusHireFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and HireDueFees !=0", con);
                    bushiredue = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    bushiredue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /////////////////////////////////////end of bus hire


            //s.1 total paid
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    scholarshippaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    scholarshippaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year='" + year.Text + "' and Term='" + term.Text + "' and DuePayment !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year='" + year.Text + "' and Term='" + term.Text + "' and DuePayment !=0", con);
                    scholarshpdue = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    scholarshpdue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //end of scholarship payment due

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from OtherIncomes where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    incomespaid.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    incomespaid.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "'", con);
                    label59.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    label59.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "'  and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from OtherFeePayment where Year='" + year.Text + "' and Semester='" + term.Text + "' and DueFees !=0", con);
                    label61.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    label61.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///////////////////////////////////expenditures
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from Expenses where Year='" + year.Text + "' and Months='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year='" + year.Text + "' and Months='" + term.Text + "'", con);
                    totalexpenses.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    totalexpenses.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from Expenses where Year='" + year.Text + "' and Months='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Duepayment) from Expenses where Year='" + year.Text + "' and Months='" + term.Text + "'", con);
                    expensesdue = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    expensesdue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from PurchasePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    purchasespaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    purchasespaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from PurchasePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DuePayment) from PurchasePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    purchaseduepayment = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    purchaseduepayment = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from EquipmentPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    equipment.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    equipment.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from EquipmentPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DuePayment) from EquipmentPayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    equipurchaseduepayment = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    equipurchaseduepayment = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    salarypaidsummary.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    salarypaidsummary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from EmployeePayment where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    salarypaymentdue = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    salarypaymentdue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Price from BalanceForward where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    balanceforward = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    balanceforward = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Price from Drawings where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Price) from Drawings where Year='" + year.Text + "' and Term='" + term.Text + "'", con);
                    drawings = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    drawings = 0;
                }
                totalincomespayable.Text = (Convert.ToInt32(shoolfeestotalpaidsummary.Text) + Convert.ToInt32(buspaidsummary.Text) + Convert.ToInt32(label59.Text) + Convert.ToInt32(bushirepaidsummary.Text) + Convert.ToInt32(scholarshippaidsummary.Text) + Convert.ToInt32(incomespaid.Text)).ToString();
                costofservice.Text = (Convert.ToInt32(salarypaidsummary.Text) + Convert.ToInt32(purchasespaidsummary.Text) + Convert.ToInt32(totalexpenses.Text)+ Convert.ToInt32(equipment.Text)).ToString();
                label31.Text = ((Convert.ToInt32(totalincomespayable.Text) + Convert.ToInt32(label30.Text) + balanceforward) - (drawings + Convert.ToInt32(costofservice.Text))).ToString();
                label32.Text = (Convert.ToInt32(scholarshpdue) + feespaymentdue + buspaymentdue + bushiredue + Convert.ToInt32(label61.Text)).ToString();
                label33.Text = (expensesdue + salarypaymentdue+equipurchaseduepayment+purchaseduepayment).ToString();
                label34.Text = (Convert.ToInt32(shoolfeestotalpaidsummary.Text) + Convert.ToInt32(scholarshippaidsummary.Text)).ToString();
                label39.Text = (Convert.ToInt32(equipurchaseduepayment) + Convert.ToInt32(equipment.Text)).ToString();
                label41.Text = (Convert.ToInt32(purchaseduepayment) + Convert.ToInt32(purchasespaidsummary.Text)).ToString();
                netincomes = ((Convert.ToInt32(totalincomespayable.Text) + Convert.ToInt32(label32.Text)) - (Convert.ToInt32(salarypaidsummary.Text) + Convert.ToInt32(purchasespaidsummary.Text) + Convert.ToInt32(totalexpenses.Text) + expensesdue + salarypaymentdue + purchaseduepayment));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            term.Enabled = true;
        }
        private void Reset()
        {
            year.Text = "";
            term.Text = "";
            shoolfeestotalpaidsummary.Text = "";
            buspaidsummary.Text = "";
            bushirepaidsummary.Text = "";
            scholarshippaidsummary.Text = "";
            totalincomespayable.Text = "";
            totalexpenses1.Text = "";
            purchasespaidsummary.Text = "";
            salarypaidsummary.Text = "";
            costofservice.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reset();
        }
       

        int stotals1totalpaidd1 = 0;
        int sts1totalpaidd1 = 0;
        //other incomes
        int incomespaidin1 = 0;
        int buspaymentdue1 = 0;
        int bushiredue1 = 0;
        int salarypaymentdue1 = 0;
        int feespaymentdue1 = 0;
        int scholarshpdue1 = 0;
        int expensesdue1 = 0;
        int balanceforward1 = 0;
        int equipurchaseduepayment1 = 0;
        int purchaseduepayment1 = 0;
        int drawings1 = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from FeePayment where Year='" + year2.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year='" + year2.Text + "'", con);
                    shoolfeestotalpaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    shoolfeestotalpaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from FeePayment where Year='" + year2.Text + "'  and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from FeePayment where Year='" + year2.Text + "' and DueFees !=0", con);
                    feespaymentdue1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    feespaymentdue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /////end fees by year and term

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "'", con);
                    buspaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    buspaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from BusFeePayment where Year='" + year2.Text + "' and DueFees !=0", con);
                    buspaymentdue1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    buspaymentdue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////////////////////////////////////////////////// end student bus payment by year and term

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year='" + year2.Text + "'", con);
                    bushirepaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    bushirepaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year='" + year2.Text + "' and HireDueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(HireDueFees) from BusHireFeePayment where Year='" + year2.Text + "' and HireDueFees !=0", con);
                    bushiredue1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    bushiredue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /////////////////////////////////////end of bus hire


            //s.1 total paid
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year='" + year2.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year='" + year2.Text + "' ", con);
                    scholarshippaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    scholarshippaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year='" + year2.Text + "'  and DuePayment !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year='" + year2.Text + "' and DuePayment !=0", con);
                    scholarshpdue1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    scholarshpdue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //end of scholarship payment due

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from OtherIncomes where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year='" + year2.Text + "'", con);
                    incomespaid1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    incomespaid1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year='" + year2.Text + "'  ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year='" + year2.Text + "'", con);
                    label59.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    label59.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year='" + year2.Text + "'  and DueFees !=0", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from OtherFeePayment where Year='" + year2.Text + "' and DueFees !=0", con);
                    label61.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    label61.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///////////////////////////////////expenditures
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from Expenses where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year='" + year2.Text + "'", con);
                    totalexpenses1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    totalexpenses1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from Expenses where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Duepayment) from Expenses where Year='" + year2.Text + "'", con);
                    expensesdue1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    expensesdue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from PurchasePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year='" + year2.Text + "'", con);
                    purchasespaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    purchasespaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from PurchasePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DuePayment) from PurchasePayment where Year='" + year2.Text + "'", con);
                    purchaseduepayment1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    purchaseduepayment1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from EquipmentPayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year='" + year2.Text + "'", con);
                    equipment1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    equipment1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from EquipmentPayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DuePayment) from EquipmentPayment where Year='" + year2.Text + "'", con);
                    equipurchaseduepayment1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    equipurchaseduepayment1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year='" + year2.Text + "'", con);
                    salarypaidsummary1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    salarypaidsummary1.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(DueFees) from EmployeePayment where Year='" + year2.Text + "'", con);
                    salarypaymentdue1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    salarypaymentdue1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Price from BalanceForward where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year='" + year2.Text + "' ", con);
                    balanceforward1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    balanceforward1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Price from Drawings where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Price) from Drawings where Year='" + year2.Text + "'", con);
                    drawings1 = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    drawings1 = 0;
                }
                totalincomespayable1.Text = (Convert.ToInt32(shoolfeestotalpaidsummary1.Text) + Convert.ToInt32(buspaidsummary1.Text) + Convert.ToInt32(label59.Text) + Convert.ToInt32(bushirepaidsummary1.Text) + Convert.ToInt32(scholarshippaidsummary1.Text) + Convert.ToInt32(incomespaid1.Text)).ToString();
                costofservice1.Text = (Convert.ToInt32(salarypaidsummary1.Text) + Convert.ToInt32(purchasespaidsummary1.Text) + Convert.ToInt32(totalexpenses1.Text) + Convert.ToInt32(equipment1.Text)).ToString();
                label31.Text = ((Convert.ToInt32(totalincomespayable1.Text) + Convert.ToInt32(label30.Text) + balanceforward1) - (drawings1 + Convert.ToInt32(costofservice1.Text))).ToString();
                label32.Text = (Convert.ToInt32(scholarshpdue1) + feespaymentdue1 + buspaymentdue1 + bushiredue1 + Convert.ToInt32(label61.Text)).ToString();
                label33.Text = (expensesdue1 + salarypaymentdue1 + equipurchaseduepayment1 + purchaseduepayment1).ToString();
                label34.Text = (Convert.ToInt32(shoolfeestotalpaidsummary1.Text) + Convert.ToInt32(scholarshippaidsummary1.Text)).ToString();
                label39.Text = (Convert.ToInt32(equipurchaseduepayment1) + Convert.ToInt32(equipment1.Text)).ToString();
                label41.Text = (Convert.ToInt32(purchaseduepayment1) + Convert.ToInt32(purchasespaidsummary1.Text)).ToString();
                netincomes = ((Convert.ToInt32(totalincomespayable1.Text) + Convert.ToInt32(label32.Text)) - (Convert.ToInt32(salarypaidsummary1.Text) + Convert.ToInt32(purchasespaidsummary1.Text) + Convert.ToInt32(totalexpenses1.Text) + expensesdue1 + salarypaymentdue1 + purchaseduepayment1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset2()
        {
            year2.Text = "";
            shoolfeestotalpaidsummary1.Text = "";
            buspaidsummary1.Text = "";
            bushirepaidsummary1.Text = "";
            scholarshippaidsummary1.Text = "";
            totalincomespayable1.Text = "";
            totalexpenses1.Text = "";
            purchasespaidsummary1.Text = "";
            salarypaidsummary1.Text = "";
            costofservice1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reset2();
        }
        public void company()
        {
            try
            {
                SqlDataReader rdr = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct6 = "select * from CompanyNames";
                cmd = new SqlCommand(ct6);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    companyname = rdr.GetString(1).Trim();
                    companyaddress = rdr.GetString(5).Trim();
                    companyslogan = rdr.GetString(2).Trim();
                    companycontact = rdr.GetString(4).Trim();
                    companyemail = rdr.GetString(3).Trim();
                }
                else
                {
                    BarPrinter = "no printer";
                    KitchenPrinter = "no printer";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            company();
            try
            {
                this.Hide();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBalanceSheet rpt = new rptBalanceSheet(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmFinancialSummaryReport2 frm = new frmFinancialSummaryReport2();
                frm.label1.Text = label1.Text;
                frm.label2.Text = label2.Text;
                myConnection = new SqlConnection(cs.DBConn);
                myConnection.Open();
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select  * from Purchases";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Purchases");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("year", year.Text.ToString());
                rpt.SetParameterValue("term", term.Text.ToString());
                rpt.SetParameterValue("broughtforward", label30.Text);
                rpt.SetParameterValue("cashathand", label31.Text);
                rpt.SetParameterValue("equipment", label39.Text);
                rpt.SetParameterValue("purchasespayment", 0);
                //rpt.SetParameterValue("purchasespayment", label41.Text);
                rpt.SetParameterValue("duepayments", label32.Text);
                rpt.SetParameterValue("netincomes", netincomes);
                rpt.SetParameterValue("duepaymentsliability", label33.Text);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                rpt.SetParameterValue("picpath", "logo.jpg");
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            company();
            try
            {
                this.Hide();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBalanceSheetYear rpt = new rptBalanceSheetYear(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmFinancialSummaryReport2 frm = new frmFinancialSummaryReport2();
                frm.label1.Text = label1.Text;
                frm.label2.Text = label2.Text;
                myConnection = new SqlConnection(cs.DBConn);
                myConnection.Open();
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select  * from Purchases";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Purchases");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("year", year2.Text.ToString());
                rpt.SetParameterValue("broughtforward", label30.Text);
                rpt.SetParameterValue("cashathand", label31.Text);
                rpt.SetParameterValue("netincomes", netincomes);
                rpt.SetParameterValue("equipment", label39.Text);
                rpt.SetParameterValue("purchasespayment", 0);
                //rpt.SetParameterValue("purchasespayment", label41.Text);
                rpt.SetParameterValue("duepayments", label32.Text);
                rpt.SetParameterValue("duepaymentsliability", label33.Text);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                rpt.SetParameterValue("picpath", "logo.jpg");
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox29_Enter(object sender, EventArgs e)
        {

        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year< '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year<'" + year.Text + "'", con);
                        label60.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label60.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year <'" + year.Text + "' and Semester !='2nd' and Semester !='3rd' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year <'" + year.Text + "' and Semester !='2nd' and Semester !='3rd'", con);
                        label60.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label60.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year <'" + year.Text + "' and Semester !='3rd' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year <'" + year.Text + "' and Semester !='3rd'", con);
                        label60.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label60.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (term.Text == "1st") { 
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from FeePayment where Year <'" + year.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year <'" + year.Text + "'", con);
                    label15.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    label15.Text = "0";
                }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from FeePayment where Year <='" + year.Text + "' and Semester !='2nd' and Semester !='3rd' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year <='" + year.Text + "' and Semester !='2nd' and Semester !='3rd'", con);
                        label15.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label15.Text = "0";
                    } 
                     }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from FeePayment where Year <='" + year.Text + "' and Semester !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year <='" + year.Text + "' and Semester !='3rd'", con);
                        label15.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label15.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       

       

            /////end fees by year and term

            try
            {
                if (term.Text == "1st")
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year<'" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year<'" + year.Text + "'", con);
                        label16.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label16.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where  Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where  Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label16.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label16.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                { 
                con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label16.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label16.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year < '" + year.Text + "' ", con);
                        label18.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                       label18.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label18.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label18.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label18.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label18.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
            /////////////////////////////////////end of bus hire


            //s.1 total paid
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year <'" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year <'" + year.Text + "'", con);
                        label19.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label19.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where  Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where  Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label19.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label19.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where  Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where  Year <='" + year.Text + "' and Term !='3rd'", con);
                        label19.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label19.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //end of scholarship payment due

            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from OtherIncomes where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year < '" + year.Text + "'", con);
                        label21.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label21.Text = "0";
                    }
                }else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from OtherIncomes where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label21.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label21.Text = "0";
                    }
                }else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from OtherIncomes where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label21.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label21.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ///////////////////////////////////expenditures
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from Expenses where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year < '" + year.Text + "'", con);
                        label22.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label22.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from Expenses where Year < '" + year.Text + "' and Months !='2nd' and Months !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year < '" + year.Text + "' and Months !='2nd' and Months !='3rd'", con);
                        label22.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label22.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from Expenses where Year <'" + year.Text + "' and Months !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year < '" + year.Text + "'and Months !='3rd'", con);
                        label22.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label22.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from PurchasePayment where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year < '" + year.Text + "'", con);
                        label24.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label24.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from PurchasePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label24.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label24.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from PurchasePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label24.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label24.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from EquipmentPayment where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year < '" + year.Text + "'", con);
                        label38.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label38.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from EquipmentPayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label38.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label38.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from EquipmentPayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label38.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label38.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year < '" + year.Text + "'", con);
                        label25.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label25.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label25.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label25.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label25.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label25.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            try
            {
                if (term.Text == "1st")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from BalanceForward where Year < '" + year.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year < '" + year.Text + "'", con);
                        label27.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label27.Text = "0";
                    }
                }
                else if (term.Text == "2nd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from BalanceForward where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year <='" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label27.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label27.Text = "0";
                    }
                }
                else if (term.Text == "3rd")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from BalanceForward where Year <='" + year.Text + "' and Term !='3rd'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year <='" + year.Text + "' and Term !='3rd'", con);
                        label27.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label27.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (term.Text == "1st"){
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Price from Drawings where Year < '" + year.Text + "' ", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Price) from Drawings where Year < '" + year.Text + "'", con);
                    label28.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                else
                {
                    label28.Text = "0";
                }
                }
                else if (term.Text == "2nd") {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from Drawings where Year < '" + year.Text + "' and Term !='2nd' and Term !='3rd' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from Drawings where Year < '" + year.Text + "' and Term !='2nd' and Term !='3rd'", con);
                        label28.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label28.Text = "0";
                    }
                }
                else if (term.Text == "3rd") {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from Drawings where Year < '" + year.Text + "' and Term !='3rd' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from Drawings where Year < '" + year.Text + "' and Term !='3rd'", con);
                        label28.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label28.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label30.Text = ((Convert.ToInt32(label15.Text) + Convert.ToInt32(label16.Text) + Convert.ToInt32(label18.Text) + Convert.ToInt32(label19.Text) + Convert.ToInt32(label60.Text) + Convert.ToInt32(label21.Text) + Convert.ToInt32(label27.Text)) - (Convert.ToInt32(label22.Text) + Convert.ToInt32(label24.Text) + Convert.ToInt32(label25.Text) + Convert.ToInt32(label28.Text) + Convert.ToInt32(label38.Text))).ToString();
            shoolfeestotalpaidsummary.Text = "";
            buspaidsummary.Text = "";
            bushirepaidsummary.Text = "";
            scholarshippaidsummary.Text = "";
            incomespaid.Text = "";
            totalincomespayable.Text = "";
            salarypaidsummary.Text = "";
            purchasespaidsummary.Text = "";
            costofservice.Text = "";
            totalexpenses.Text = "";
        }

        private void year2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from FeePayment where Year <'" + year2.Text + "' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from FeePayment where Year <'" + year2.Text + "'", con);
                        label15.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label15.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = new SqlCommand("select TotalPaid from OtherFeePayment where Year< '" + year2.Text + "'", con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select SUM(TotalPaid) from OtherFeePayment where Year<'" + year2.Text + "'", con);
                label60.Text = cmd.ExecuteScalar().ToString();
                con.Close();
                if ((rdr != null))
                {
                    rdr.Close();
                }
            }
            else
            {
                label60.Text = "0";
            }

            /////end fees by year and term

            try
            {
                con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year<'" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year<'" + year2.Text + "'", con);
                        label16.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label16.Text = "0";
                    }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
               
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select HireTotalPaid from BusHireFeePayment where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year < '" + year2.Text + "' ", con);
                        label18.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        label18.Text = "0";
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /////////////////////////////////////end of bus hire


            //s.1 total paid
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from ScholarshipPayment where Year <'" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year <'" + year2.Text + "'", con);
                        label19.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label19.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //end of scholarship payment due

            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from OtherIncomes where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year < '" + year2.Text + "'", con);
                        label21.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label21.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ///////////////////////////////////expenditures
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from Expenses where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from Expenses where Year < '" + year2.Text + "'", con);
                        label22.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label22.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from PurchasePayment where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year < '" + year2.Text + "'", con);
                        label24.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label24.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Amount from EquipmentPayment where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Amount) from EquipmentPayment where Year < '" + year2.Text + "'", con);
                        label38.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label38.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from EmployeePayment where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from EmployeePayment where Year < '" + year2.Text + "'", con);
                        label25.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label25.Text = "0";
                    }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from BalanceForward where Year < '" + year2.Text + "'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from BalanceForward where Year < '" + year2.Text + "'", con);
                        label27.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label27.Text = "0";
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select Price from Drawings where Year < '" + year2.Text + "' ", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(Price) from Drawings where Year < '" + year2.Text + "'", con);
                        label28.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    else
                    {
                        label28.Text = "0";
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label30.Text = ((Convert.ToInt32(label15.Text) + Convert.ToInt32(label16.Text) + Convert.ToInt32(label18.Text) + Convert.ToInt32(label19.Text) + Convert.ToInt32(label21.Text) + Convert.ToInt32(label27.Text) + Convert.ToInt32(label60.Text)) - (Convert.ToInt32(label22.Text) + Convert.ToInt32(label24.Text) + Convert.ToInt32(label25.Text) + Convert.ToInt32(label28.Text) + Convert.ToInt32(label38.Text))).ToString();
            shoolfeestotalpaidsummary1.Text = "";
            buspaidsummary1.Text = "";
            bushirepaidsummary1.Text = "";
            scholarshippaidsummary1.Text = "";
            incomespaid1.Text = "";
            totalincomespayable1.Text = "";
            salarypaidsummary1.Text = "";
            purchasespaidsummary1.Text = "";
            costofservice1.Text = "";
            totalexpenses1.Text = "";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace College_Management_System
{
    public partial class frmFinancialSummary : Form
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
        public frmFinancialSummary()
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
                    s1totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    s1totalpaidd = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            totals1totalpaidd = s1totalpaidd;
            shoolfeestotalpaidsummary.Text = totals1totalpaidd.ToString();
            //end s.1 totalpaid
             //////////////////////////////////////// end fees payment

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
                    ts1totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    ts1totalpaidd = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ttotals1totalpaidd = ts1totalpaidd;
            buspaidsummary.Text = ttotals1totalpaidd.ToString();
            //end s.1 totalpaid

            ////////////////////////////////////////////////// end student bus payment

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
                    hts1totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    hts1totalpaidd = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             httotals1totalpaidd = hts1totalpaidd;
             bushirepaidsummary.Text = httotals1totalpaidd.ToString();

            

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
                     sts1totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                     con.Close();
                 }
                 else
                 {
                     sts1totalpaidd = 0;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             stotals1totalpaidd = sts1totalpaidd;
             scholarshippaidsummary.Text = stotals1totalpaidd.ToString();
             try
             {
                 con = new SqlConnection(cs.DBConn);
                 con.Open();
                 cmd = new SqlCommand("select Amount from OtherIncomes where Year='" + year.Text + "' and Term='" + term.Text + "' and TransactionType='Credit    '", con);
                 rdr = cmd.ExecuteReader();
                 if (rdr.Read())
                 {
                     con = new SqlConnection(cs.DBConn);
                     con.Open();
                     cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year='" + year.Text + "' and Term='" + term.Text + "'and TransactionType='Credit    '", con);
                     incomespaidin = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                     con.Close();
                 }
                 else
                 {
                     incomespaidin = 0;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             int  incomespaidins = incomespaidin;
             incomespaid.Text = incomespaidins.ToString();
             totalincomespayable.Text = (totals1totalpaidd + htotals1totalpaidd + ttotals1totalpaidd + httotals1totalpaidd + stotals1totalpaidd + incomespaidins).ToString();
           
            ///////////////////////////////////expenditures
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from OtherTransaction where Year='" + year.Text + "' and Term='" + term.Text + "' and TransactionType='Credit'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from OtherTransaction where Year='" + year.Text + "' and Term='" + term.Text + "'and TransactionType='Credit'", con);
                    totalexpenses.Text =cmd.ExecuteScalar().ToString();
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
            costofservice.Text = (Convert.ToInt32(salarypaidsummary.Text) + Convert.ToInt32(purchasespaidsummary.Text)).ToString();
            GrossProfit.Text = (Convert.ToInt32(totalincomespayable.Text) - Convert.ToInt32(costofservice.Text)).ToString();
            Profitmargin.Text = ((Convert.ToInt32(GrossProfit.Text)) - (Convert.ToInt32(totalexpenses.Text))).ToString();
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
            totalexpenses.Text = "";
            purchasespaidsummary.Text = "";
            salarypaidsummary.Text = "";
            GrossProfit.Text = "";
            Profitmargin.Text = "";
            costofservice.Text = "";
            GrossProfit.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int s1payablefeessa1 = 0;
        int s1payablefeess1 = 0;
        int totals1payablefeess1 = 0;
        int s1totalpaidd1 = 0;
        int totals1totalpaidd1 = 0;
        int s1duepaymentt1 = 0;
        int s1duepaymenttt1 = 0;
        int totals1duepayment1 = 0;
        int total1 = 0;
        int s2payablefeessa1 = 0;
        int s2payablefeess1 = 0;
        int total21 = 0;
        //
        int s3payablefeessa1 = 0;
        int s3payablefeess1 = 0;
        int total31 = 0;
        //
        int s4payablefeessa1 = 0;
        int s4payablefeess1 = 0;
        int total41 = 0;
        //
        int s5payablefeessa1 = 0;
        int s5payablefeess1 = 0;
        int total51 = 0;
        //
        int s6payablefeessa1 = 0;
        int s6payablefeess1 = 0;
        int total61 = 0;
        //
        int p6payablefeessa1 = 0;
        int p6payablefeess1 = 0;
        int total71 = 0;
        //
        int p7payablefeessa1 = 0;
        int p7payablefeess1 = 0;
        int total81 = 0;
        int incomespaidin1 = 0;

        /// ///hostel
        int hs1payablefeessa1 = 0;
        int hs1payablefeess1 = 0;
        int htotals1payablefeess1 = 0;
        int hs1totalpaidd1 = 0;
        int htotals1totalpaidd1 = 0;
        int hs1duepaymentt1 = 0;
        int hs1duepaymenttt1 = 0;
        int htotals1duepayment1 = 0;
        int htotal1 = 0;

        /// transport

        int ts1payablefeess1 = 0;
        int ttotals1payablefeess1 = 0;
        int ts1totalpaidd1 = 0;
        int ttotals1totalpaidd1 = 0;
        int ts1duepaymentt1 = 0;
        int ts1duepaymenttt1 = 0;
        int ttotals1duepayment1 = 0;
        // bus hire
        int hts1payablefeess1 = 0;
        int httotals1payablefeess1 = 0;
        int hts1totalpaidd1 = 0;
        int httotals1totalpaidd1 = 0;
        int hts1duepaymentt1 = 0;
        int hts1duepaymenttt1 = 0;
        int httotals1duepayment1 = 0;
        //scholarship fee
        int stotals1payablefeess1 = 0;
        int stotals1totalpaidd1 = 0;
        int sts1payablefeess1 = 0;
        int sts1totalpaidd1 = 0;
        int sts1duepaymentt1 = 0;
        int sts1duepaymenttt1 = 0;
        int sttotals1duepayment1 = 0;
        private void button3_Click(object sender, EventArgs e)
        {

            //s.1 total paid
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
                    s1totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    s1totalpaidd1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            totals1totalpaidd1 = s1totalpaidd1;
            shoolfeestotalpaidsummary1.Text = totals1totalpaidd1.ToString();
            //end s.1 totalpaid
             //////////////////////////////////////// end fees payment

           
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
                    ts1totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    ts1totalpaidd1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ttotals1totalpaidd1 = ts1totalpaidd1;
            buspaidsummary1.Text = ttotals1totalpaidd1.ToString();
            //end s.1 totalpaid

            ////////////////////////////////////////////////// end student bus payment

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
                    cmd = new SqlCommand("select SUM(HireTotalPaid) from BusHireFeePayment where Year='" + year2.Text + "' ", con);
                    hts1totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    hts1totalpaidd1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             httotals1totalpaidd1 = hts1totalpaidd1;
             bushirepaidsummary1.Text = httotals1totalpaidd1.ToString();
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
                     cmd = new SqlCommand("select SUM(TotalPaid) from ScholarshipPayment where Year='" + year2.Text + "'", con);
                     sts1totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                     con.Close();
                 }
                 else
                 {
                     sts1totalpaidd1 = 0;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             stotals1totalpaidd1 = sts1totalpaidd1;
             scholarshippaidsummary1.Text = stotals1totalpaidd1.ToString();
             try
             {
                 con = new SqlConnection(cs.DBConn);
                 con.Open();
                 cmd = new SqlCommand("select Amount from OtherIncomes where Year='" + year2.Text + "' and TransactionType='Credit'", con);
                 rdr = cmd.ExecuteReader();
                 if (rdr.Read())
                 {
                     con = new SqlConnection(cs.DBConn);
                     con.Open();
                     cmd = new SqlCommand("select SUM(Amount) from OtherIncomes where Year='" + year2.Text + "' and TransactionType='Credit'", con);
                     incomespaidin1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                     con.Close();
                 }
                 else
                 {
                     incomespaidin1 = 0;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             int incomespaidin1s = incomespaidin;
             incomespaid1.Text = incomespaidin1s.ToString();

             totalincomespayable1.Text = (totals1totalpaidd1 + htotals1totalpaidd1 + ttotals1totalpaidd1 + httotals1totalpaidd1 + stotals1totalpaidd1 + incomespaidin1s).ToString();
           
            ///////////////////////////////////expenditures
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from OtherTransaction where Year='" + year2.Text + "' and TransactionType='Credit'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from OtherTransaction where Year='" + year2.Text + "' and TransactionType='Credit'", con);
                    totalexpenses1.Text =cmd.ExecuteScalar().ToString();
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
                cmd = new SqlCommand("select Amount from PurchasePayment where Year='" + year2.Text + "'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select SUM(Amount) from PurchasePayment where Year='" + year2.Text + "' ", con);
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

           
            
            costofservice1.Text = ((Convert.ToInt32(salarypaidsummary1.Text)) +(Convert.ToInt32(purchasespaidsummary1.Text))).ToString();
            GrossProfit1.Text = ((Convert.ToInt32(totalincomespayable1.Text)) - (Convert.ToInt32(costofservice1.Text))).ToString();
            Profitmargin1.Text = ((Convert.ToInt32(GrossProfit1.Text)) - (Convert.ToInt32(totalexpenses1.Text))).ToString();
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
            Profitmargin1.Text = "";
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
            try
            {
                company();
                this.Hide();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptschoolincomestatement rpt = new rptschoolincomestatement(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmFinancialSummaryReport frm = new frmFinancialSummaryReport();
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
                rpt.SetParameterValue("feespaid", shoolfeestotalpaidsummary.Text.ToString());
                rpt.SetParameterValue("buspaid", buspaidsummary.Text.ToString());
                rpt.SetParameterValue("hirepaid", bushirepaidsummary.Text.ToString());
                rpt.SetParameterValue("scholarshippaid", scholarshippaidsummary.Text.ToString());
                rpt.SetParameterValue("payableincomes", totalincomespayable.Text.ToString());
                rpt.SetParameterValue("salarypaid", salarypaidsummary.Text.ToString());
                rpt.SetParameterValue("grossprofit", GrossProfit.Text.ToString());
                rpt.SetParameterValue("purchasesammount", purchasespaidsummary.Text.ToString());
                rpt.SetParameterValue("expenditurespaid", costofservice.Text);
                rpt.SetParameterValue("profit", Profitmargin.Text);
                rpt.SetParameterValue("expensesammount", totalexpenses.Text.ToString());
                rpt.SetParameterValue("Othersincome", incomespaid.Text.ToString());
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                company();
                this.Hide();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptschoolincomestatement2 rpt = new rptschoolincomestatement2(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmFinancialSummaryReport frm = new frmFinancialSummaryReport();
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
                rpt.SetParameterValue("feespaid", shoolfeestotalpaidsummary1.Text.ToString());
                rpt.SetParameterValue("buspaid", buspaidsummary1.Text.ToString());
                rpt.SetParameterValue("hirepaid", bushirepaidsummary1.Text.ToString());
                rpt.SetParameterValue("scholarshippaid", scholarshippaidsummary1.Text.ToString());
                rpt.SetParameterValue("payableincomes", totalincomespayable1.Text.ToString());
                rpt.SetParameterValue("salarypaid", salarypaidsummary1.Text.ToString());
                rpt.SetParameterValue("grossprofit", GrossProfit1.Text.ToString());
                rpt.SetParameterValue("purchasesammount", purchasespaidsummary1.Text.ToString());
                rpt.SetParameterValue("expenditurespaid", costofservice1.Text);
                rpt.SetParameterValue("profit", Profitmargin1.Text);
                rpt.SetParameterValue("expensesammount", totalexpenses1.Text.ToString());
                rpt.SetParameterValue("Othersincome", incomespaid1.Text.ToString());
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }

       
    }
}

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
    public partial class frmTransportFeesFinancialSummary : Form
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
        public frmTransportFeesFinancialSummary()
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

        private void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            term.Enabled = true;
        }
        private void Reset()
        {
            year.Text = "";
            term.Text = "";
            s1payablefees.Text = "";
            s1totalpaid.Text = "";
            s1duepayment.Text = "";
            s2payablefees.Text = "";
            s2totalpaid.Text = "";
            s2duepayment.Text = "";
            s3payablefees.Text = "";
            s3totalpaid.Text = "";
            s3duepayment.Text = "";
            s4payablefees.Text = "";
            s4totalpaid.Text = "";
            s4duepayment.Text = "";
            s5payablefees.Text = "";
            s5totalpaid.Text = "";
            s5duepayment.Text = "";
            s6payablefees.Text = "";
            s6totalpaid.Text = "";
            s6duepayment.Text = "";
        }
        private void Reset2()
        {
            year2.Text = "";
            s1payablefees1.Text = "";
            s1totalpaid1.Text = "";
            s1duepayment1.Text = "";
            s2payablefees1.Text = "";
            s2totalpaid1.Text = "";
            s2duepayment1.Text = "";
            s3payablefees1.Text = "";
            s3totalpaid1.Text = "";
            s3duepayment1.Text = "";
            s4payablefees1.Text = "";
            s4totalpaid1.Text = "";
            s4duepayment1.Text = "";
            s5payablefees1.Text = "";
            s5totalpaid1.Text = "";
            s5duepayment1.Text = "";
            s6payablefees1.Text = "";
            s6totalpaid1.Text = "";
            s6duepayment1.Text = "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reset2();
        }
        int s1payablefeess = 0;
        int totals1payablefeess = 0;
        int s1totalpaidd = 0;
        int totals1totalpaidd = 0;
        int s1duepaymentt = 0;
        int s1duepaymenttt = 0;
        int totals1duepayment = 0;
        //
        int s2payablefeess = 0;
        int totals2payablefeess = 0;
        int s2totalpaidd = 0;
        int totals2totalpaidd = 0;
        int s2duepaymentt = 0;
        int s2duepaymenttt = 0;
        int totals2duepayment = 0;
        //
        int s3payablefeess = 0;
        int totals3payablefeess = 0;
        int s3totalpaidd = 0;
        int totals3totalpaidd = 0;
        int s3duepaymentt = 0;
        int s3duepaymenttt = 0;
        int totals3duepayment = 0;
        //
        int s4payablefeess = 0;
        int totals4payablefeess = 0;
        int s4totalpaidd = 0;
        int totals4totalpaidd = 0;
        int s4duepaymentt = 0;
        int s4duepaymenttt = 0;
        int totals4duepayment = 0;
        //
        int s5payablefeess = 0;
        int totals5payablefeess = 0;
        int s5totalpaidd = 0;
        int totals5totalpaidd = 0;
        int s5duepaymentt = 0;
        int s5duepaymenttt = 0;
        int totals5duepayment = 0;
        //
        //int s6payablefeessa = 0;
        int s6payablefeess = 0;
        int totals6payablefeess = 0;
        int s6totalpaidd = 0;
        int totals6totalpaidd = 0;
        int s6duepaymentt = 0;
        int s6duepaymenttt = 0;
        int totals6duepayment = 0;
        private void button5_Click(object sender, EventArgs e)
        {
           //s.1 fees payable
            
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.1'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.1'", con);
                    s1payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                    
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    s1payablefeess = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
                totals1payablefeess = s1payablefeess ;
                s1payablefees.Text = totals1payablefeess.ToString();
            
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.1'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.1'", con);
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
                s1totalpaid.Text = totals1totalpaidd.ToString();
            //end s.1 totalpaid

            //start s.1 duepayment
                s1duepaymenttt = Convert.ToInt32(s1payablefees.Text);
                s1duepaymentt = Convert.ToInt32(s1totalpaid.Text);
                totals1duepayment = s1duepaymenttt - s1duepaymentt;
                s1duepayment.Text = totals1duepayment.ToString();


            ////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.2'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.2'", con);
                        s2payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                      
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s2payablefeess = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                totals2payablefeess = s2payablefeess;
                s2payablefees.Text = totals2payablefeess.ToString();
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.2'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.2'", con);
                        s2totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s2totalpaidd = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals2totalpaidd = s2totalpaidd;
                s2totalpaid.Text = totals2totalpaidd.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s2duepaymenttt = Convert.ToInt32(s2payablefees.Text);
                s2duepaymentt = Convert.ToInt32(s2totalpaid.Text);
                totals1duepayment = s2duepaymenttt - s2duepaymentt;
                s2duepayment.Text = totals2duepayment.ToString();

            ///////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.3'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.3'", con);
                        s3payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                      
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s3payablefeess = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                totals3payablefeess = s3payablefeess;
                s3payablefees.Text = totals3payablefeess.ToString();

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.3'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.3'", con);
                        s3totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s3totalpaidd = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals3totalpaidd = s3totalpaidd ;
                s3totalpaid.Text = totals3totalpaidd.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s3duepaymenttt = Convert.ToInt32(s3payablefees.Text);
                s3duepaymentt = Convert.ToInt32(s3totalpaid.Text);
                totals3duepayment = s3duepaymenttt - s3duepaymentt;
                s3duepayment.Text = totals3duepayment.ToString();

            ////////////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.4'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.4'", con);
                        s4payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s4payablefeess = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
                totals4payablefeess = s4payablefeess;
                s4payablefees.Text = totals4payablefeess.ToString();

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.4'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.4'", con);
                        s4totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s4totalpaidd = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals4totalpaidd = s4totalpaidd ;
                s4totalpaid.Text = totals4totalpaidd.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s4duepaymenttt = Convert.ToInt32(s4payablefees.Text);
                s4duepaymentt = Convert.ToInt32(s4totalpaid.Text);
                totals4duepayment = s4duepaymenttt - s4duepaymentt;
                s4duepayment.Text = totals4duepayment.ToString();

            ///////////////////////////////////////////////////////////////

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.5'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.5'", con);
                        s5payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                       
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s5payablefeess = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                totals5payablefeess = s5payablefeess;
                s5payablefees.Text = totals5payablefeess.ToString();

          
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.5'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.5'", con);
                        s5totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s5totalpaidd = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals5totalpaidd = s5totalpaidd;
                s5totalpaid.Text = totals5totalpaidd.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s5duepaymenttt = Convert.ToInt32(s5payablefees.Text);
                s5duepaymentt = Convert.ToInt32(s5totalpaid.Text);
                totals5duepayment = s5duepaymenttt - s5duepaymentt;
                s5duepayment.Text = totals5duepayment.ToString();

            ///////////////////////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.6'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.6'", con);
                        s6payablefeess = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s6payablefeess = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                totals6payablefeess = s6payablefeess ;
                s6payablefees.Text = totals6payablefeess.ToString();

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.6'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year.Text + "' and Term='" + term.Text + "' and Class='S.6'", con);
                        s6totalpaidd = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s6totalpaidd = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals6totalpaidd = s6totalpaidd;
                s6totalpaid.Text = totals6totalpaidd.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s6duepaymenttt = Convert.ToInt32(s6payablefees.Text);
                s6duepaymentt = Convert.ToInt32(s6totalpaid.Text);
                totals6duepayment = s6duepaymenttt - s6duepaymentt;
                s6duepayment.Text = totals6duepayment.ToString();

                totalpayablefees.Text = (totals1payablefeess + totals2payablefeess + totals3payablefeess + totals4payablefeess + totals5payablefeess + totals6payablefeess).ToString();
                totalfeespaid.Text = (totals1totalpaidd + totals2totalpaidd + totals3totalpaidd + totals4totalpaidd + totals5totalpaidd + totals6totalpaidd).ToString();
                totalduepayment.Text = ((Convert.ToInt32(totalpayablefees.Text)) - (Convert.ToInt32(totalfeespaid.Text))).ToString();
        }



        int s1payablefeess1 = 0;
        int totals1payablefeess1 = 0;
        int s1totalpaidd1 = 0;
        int totals1totalpaidd1 = 0;
        int s1duepaymentt1 = 0;
        int s1duepaymenttt1 = 0;
        int totals1duepayment1 = 0;
        //
        int s2payablefeess1 = 0;
        int totals2payablefeess1 = 0;
        int s2totalpaidd1 = 0;
        int totals2totalpaidd1 = 0;
        int s2duepaymentt1 = 0;
        int s2duepaymenttt1 = 0;
        int totals2duepayment1 = 0;
        //
        int s3payablefeess1 = 0;
        int totals3payablefeess1 = 0;
        int s3totalpaidd1 = 0;
        int totals3totalpaidd1 = 0;
        int s3duepaymentt1 = 0;
        int s3duepaymenttt1 = 0;
        int totals3duepayment1 = 0;
        //
        int s4payablefeess1 = 0;
        int totals4payablefeess1 = 0;
        int s4totalpaidd1 = 0;
        int totals4totalpaidd1 = 0;
        int s4duepaymentt1 = 0;
        int s4duepaymenttt1 = 0;
        int totals4duepayment1 = 0;
        //
        int s5payablefeess1 = 0;
        int totals5payablefeess1 = 0;
        int s5totalpaidd1 = 0;
        int totals5totalpaidd1 = 0;
        int s5duepaymentt1 = 0;
        int s5duepaymenttt1 = 0;
        int totals5duepayment1 = 0;
        //
        int s6payablefeess1 = 0;
        int totals6payablefeess1 = 0;
        int s6totalpaidd1 = 0;
        int totals6totalpaidd1 = 0;
        int s6duepaymentt1 = 0;
        int s6duepaymenttt1 = 0;
        int totals6duepayment1 = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            //s.1 fees payable
            
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.1'", con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.1'", con);
                    s1payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    con.Close();
                   
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    s1payablefeess1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
                totals1payablefeess1 = s1payablefeess1 ;
                s1payablefees1.Text = totals1payablefeess1.ToString();
            
          
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.1'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.1'", con);
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
                s1totalpaid1.Text = totals1totalpaidd1.ToString();
            //end s.1 totalpaid

            //start s.1 duepayment
                s1duepaymenttt1 = Convert.ToInt32(s1payablefees1.Text);
                s1duepaymentt1 = Convert.ToInt32(s1totalpaid1.Text);
                totals1duepayment1 = s1duepaymenttt1 - s1duepaymentt1;
                s1duepayment1.Text = totals1duepayment1.ToString();


            ////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.2'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.2'", con);
                        s2payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s2payablefeess1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                totals2payablefeess1 = s2payablefeess1;
                s2payablefees1.Text = totals2payablefeess1.ToString();

             
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.2'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.2'", con);
                        s2totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s2totalpaidd1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals2totalpaidd1 = s2totalpaidd1;
                s2totalpaid1.Text = totals2totalpaidd1.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s2duepaymenttt1 = Convert.ToInt32(s2payablefees1.Text);
                s2duepaymentt1 = Convert.ToInt32(s2totalpaid1.Text);
                totals1duepayment1 = s2duepaymenttt1 - s2duepaymentt1;
                s2duepayment1.Text = totals2duepayment1.ToString();

            ///////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.3'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.3'", con);
                        s3payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s3payablefeess1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                totals3payablefeess1 = s3payablefeess1;
                s3payablefees1.Text = totals3payablefeess1.ToString();

                
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.3'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.3'", con);
                        s3totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s3totalpaidd1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals3totalpaidd1 = s3totalpaidd1 ;
                s3totalpaid1.Text = totals3totalpaidd1.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s3duepaymenttt1 = Convert.ToInt32(s3payablefees1.Text);
                s3duepaymentt1 = Convert.ToInt32(s3totalpaid1.Text);
                totals3duepayment1 = s3duepaymenttt1 - s3duepaymentt1;
                s3duepayment1.Text = totals3duepayment1.ToString();

            ////////////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.4'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.4'", con);
                        s4payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s4payablefeess1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                totals4payablefeess1 = s4payablefeess1 ;
                s4payablefees1.Text = totals4payablefeess1.ToString();

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.4'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.4'", con);
                        s4totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s4totalpaidd1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals4totalpaidd1 = s4totalpaidd1;
                s4totalpaid1.Text = totals4totalpaidd1.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s4duepaymenttt1 = Convert.ToInt32(s4payablefees1.Text);
                s4duepaymentt1 = Convert.ToInt32(s4totalpaid1.Text);
                totals4duepayment1 = s4duepaymenttt1 - s4duepaymentt1;
                s4duepayment1.Text = totals4duepayment1.ToString();

            ///////////////////////////////////////////////////////////////

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.5'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.5'", con);
                        s5payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s5payablefeess1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                totals5payablefeess1 = s5payablefeess1;
                s5payablefees1.Text = totals5payablefeess1.ToString();

               
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.5'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.5'", con);
                        s5totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s5totalpaidd1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals5totalpaidd1 = s5totalpaidd1;
                s5totalpaid1.Text = totals5totalpaidd1.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s5duepaymenttt1 = Convert.ToInt32(s5payablefees1.Text);
                s5duepaymentt1 = Convert.ToInt32(s5totalpaid1.Text);
                totals5duepayment1 = s5duepaymenttt1 - s5duepaymentt1;
                s5duepayment1.Text = totals5duepayment1.ToString();

            ///////////////////////////////////////////////////////
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "'  and Class='S.6'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select  SUM(BusCharges) from BusFeePayment where Year='" + year2.Text + "' and Class='S.6'", con);
                        s6payablefeess1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s6payablefeess1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                totals6payablefeess1 = s6payablefeess1;
                s6payablefees1.Text = totals6payablefeess1.ToString();

               
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select TotalPaid from BusFeePayment where Year='" + year2.Text + "' and Class='S.6'", con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select SUM(TotalPaid) from BusFeePayment where Year='" + year2.Text + "' and Class='S.6'", con);
                        s6totalpaidd1 = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con.Close();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                    }
                    else
                    {
                        s6totalpaidd1 = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                totals6totalpaidd1 = s6totalpaidd1;
                s6totalpaid1.Text = totals6totalpaidd1.ToString();
                //end s.1 totalpaid

                //start s.1 duepayment
                s6duepaymenttt1 = Convert.ToInt32(s6payablefees1.Text);
                s6duepaymentt1 = Convert.ToInt32(s6totalpaid1.Text);
                totals6duepayment1 = s6duepaymenttt1 - s6duepaymentt1;
                s6duepayment1.Text = totals6duepayment1.ToString();

                totalpayablefees1.Text = (totals1payablefeess1 + totals2payablefeess1 + totals3payablefeess1 + totals4payablefeess1 + totals5payablefeess1 + totals6payablefeess1).ToString();
                totalfeespaid1.Text = (totals1totalpaidd1 + totals2totalpaidd1 + totals3totalpaidd + totals4totalpaidd1 + totals5totalpaidd1 + totals6totalpaidd1).ToString();
                totalduepayment1.Text = ((Convert.ToInt32(totalpayablefees1.Text)) - (Convert.ToInt32(totalfeespaid1.Text))).ToString();
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
                rpttransportfeesfinancialsummary rpt = new rpttransportfeesfinancialsummary(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmTransportFeesPaymentSummaryReport frm = new frmTransportFeesPaymentSummaryReport();
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
                rpt.SetParameterValue("s1totalfeespayable", s1payablefees.Text.ToString());
                rpt.SetParameterValue("s1totalfeespaid", s1totalpaid.Text.ToString());
                rpt.SetParameterValue("s1feesduepayment", s1duepayment.Text.ToString());
                rpt.SetParameterValue("s2totalfeespayable", s2payablefees.Text.ToString());
                rpt.SetParameterValue("s2totalfeespaid", s2totalpaid.Text.ToString());
                rpt.SetParameterValue("s2feesduepayment", s2duepayment.Text.ToString());
                rpt.SetParameterValue("s3totalfeespayable", s3payablefees.Text.ToString());
                rpt.SetParameterValue("s3totalfeespaid", s3totalpaid.Text.ToString());
                rpt.SetParameterValue("s3feesduepayment", s3duepayment.Text.ToString());
                rpt.SetParameterValue("s4totalfeespayable", s4payablefees.Text.ToString());
                rpt.SetParameterValue("s4totalfeespaid", s4totalpaid.Text.ToString());
                rpt.SetParameterValue("s4feesduepayment", s4duepayment.Text);
                rpt.SetParameterValue("s5totalfeespayable", s5payablefees.Text.ToString());
                rpt.SetParameterValue("s5totalfeespaid", s5totalpaid.Text.ToString());
                rpt.SetParameterValue("s5feesduepayment", s5duepayment.Text);
                rpt.SetParameterValue("s6totalfeespayable", s6payablefees.Text.ToString());
                rpt.SetParameterValue("s6totalfeespaid", s6totalpaid.Text.ToString());
                rpt.SetParameterValue("s6feesduepayment", s6duepayment.Text);
                rpt.SetParameterValue("totalpayable", totalpayablefees.Text.ToString());
                rpt.SetParameterValue("totalpaid", totalfeespaid.Text.ToString());
                rpt.SetParameterValue("duepayment", totalduepayment.Text);
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
                rpttransportfeesfinancialsummary2 rpt = new rpttransportfeesfinancialsummary2(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                frmTransportFeesPaymentSummaryReport frm = new frmTransportFeesPaymentSummaryReport();
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
                rpt.SetParameterValue("s1totalfeespayable", s1payablefees1.Text.ToString());
                rpt.SetParameterValue("s1totalfeespaid", s1totalpaid1.Text.ToString());
                rpt.SetParameterValue("s1feesduepayment", s1duepayment1.Text.ToString());
                rpt.SetParameterValue("s2totalfeespayable", s2payablefees1.Text.ToString());
                rpt.SetParameterValue("s2totalfeespaid", s2totalpaid1.Text.ToString());
                rpt.SetParameterValue("s2feesduepayment", s2duepayment1.Text.ToString());
                rpt.SetParameterValue("s3totalfeespayable", s3payablefees1.Text.ToString());
                rpt.SetParameterValue("s3totalfeespaid", s3totalpaid1.Text.ToString());
                rpt.SetParameterValue("s3feesduepayment", s3duepayment1.Text.ToString());
                rpt.SetParameterValue("s4totalfeespayable", s4payablefees1.Text.ToString());
                rpt.SetParameterValue("s4totalfeespaid", s4totalpaid1.Text.ToString());
                rpt.SetParameterValue("s4feesduepayment", s4duepayment1.Text);
                rpt.SetParameterValue("s5totalfeespayable", s5payablefees1.Text.ToString());
                rpt.SetParameterValue("s5totalfeespaid", s5totalpaid1.Text.ToString());
                rpt.SetParameterValue("s5feesduepayment", s5duepayment1.Text);
                rpt.SetParameterValue("s6totalfeespayable", s6payablefees1.Text.ToString());
                rpt.SetParameterValue("s6totalfeespaid", s6totalpaid1.Text.ToString());
                rpt.SetParameterValue("s6feesduepayment", s6duepayment1.Text);
                rpt.SetParameterValue("totalpayable", totalpayablefees1.Text.ToString());
                rpt.SetParameterValue("totalpaid", totalfeespaid1.Text.ToString());
                rpt.SetParameterValue("duepayment", totalduepayment1.Text);
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
        }
    }


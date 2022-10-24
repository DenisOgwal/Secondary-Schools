using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
namespace College_Management_System
{
    public partial class frmMessaging : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public String str;


        public frmMessaging()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            messages.Text = "";
            dtp.Text = DateTime.Today.ToString();
            rball.Checked = true;
            rbtstudent.Checked = false;
            rbtclass.Checked = false;
            btnSave.Enabled = true;
            Year.Text = "";
            term.Text = "";
            classes.Text = "";
            studentno.Text = "";
        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
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
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmOtherTransaction_Load(object sender, EventArgs e)
        {
            rball.Checked = false;
            rbtstudent.Checked = false;
            AutocompleteTerm();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(course) from Student ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    classes.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        string numberphone = null;
        string messaging = null;
        string studentsnos = null;
        string numberss = null;
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Year.Text == "")
            {
                MessageBox.Show("Please enter Year ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Year.Focus();
                return;
            }
            if (term.Text == "")
            {
                MessageBox.Show("Please enter term ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                term.Focus();
                return;
            }
            string smsallow = Properties.Settings.Default.smsallow;
            if (smsallow == "Yes")
            {
                try
                {
                    if (rbtstudent.Checked)
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb = "insert into Messages(Year,Term,Date,StudentNo,Message) VALUES (@d1,@d2,@d3,@d4,@d5)";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "Year"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Term"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Date"));
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 15, "StudentNo"));
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 1000, "Message"));
                        cmd.Parameters["@d1"].Value = Year.Text;
                        cmd.Parameters["@d2"].Value = term.Text;
                        cmd.Parameters["@d3"].Value = dtp.Text;
                        cmd.Parameters["@d4"].Value = studentno.Text;
                        cmd.Parameters["@d5"].Value = messages.Text;
                        cmd.ExecuteNonQuery();
                        btnSave.Enabled = false;
                        con.Close();
                        try
                        {
                            using (var client2 = new WebClient())
                            using (client2.OpenRead("http://client3.google.com/generate_204"))
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT distinct RTRIM(Contact_No) FROM StudentRegistration where ScholarNo='" + studentno.Text + "'";
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                    numberphone = rdr.GetString(0).Trim();
                                }
                                if ((rdr != null))
                                {
                                    rdr.Close();
                                }
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                int stringcount = numberphone.Length;
                                if (stringcount == 10)
                                {
                                    string usernamess = Properties.Settings.Default.smsusername;
                                    string passwordss = Properties.Settings.Default.smspassword;
                                    numberss = numberphone;
                                    WebClient client = new WebClient();
                                    messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                    string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                    client.OpenRead(baseURL);
                                    MessageBox.Show("Successfully sent message");
                                }
                                else if (stringcount < 10 && stringcount == 9)
                                {
                                    string usernamess = Properties.Settings.Default.smsusername;
                                    string passwordss = Properties.Settings.Default.smspassword;
                                    numberss = "0" + numberphone;
                                    WebClient client = new WebClient();
                                    messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                    string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                    client.OpenRead(baseURL);
                                    MessageBox.Show("Successfully sent message");
                                }
                                else if (stringcount > 10 && stringcount == 12)
                                {
                                    string usernamess = Properties.Settings.Default.smsusername;
                                    string passwordss = Properties.Settings.Default.smspassword;
                                    numberss = numberphone;
                                    numberss = "0" + numberphone;
                                    WebClient client = new WebClient();
                                    messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                    string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                    client.OpenRead(baseURL);
                                    MessageBox.Show("Successfully sent message");
                                }
                                else
                                {

                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Could not send message because your computer is not connected to internet");
                        }
                    }
                    else if (rbtclass.Checked)
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT distinct RTRIM(ScholarNo) FROM Student where Course='" + classes.Text + "' and Session='" + Year.Text + "' and Term='" + term.Text + "'";
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            studentsnos = rdr.GetString(0);
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "insert into Messages(Year,Term,Date,StudentNo,Message) VALUES (@d1,@d2,@d3,@d4,@d5)";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "Year"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Date"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 15, "StudentNo"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 1000, "Message"));
                            cmd.Parameters["@d1"].Value = Year.Text;
                            cmd.Parameters["@d2"].Value = term.Text;
                            cmd.Parameters["@d3"].Value = dtp.Text;
                            cmd.Parameters["@d4"].Value = studentsnos;
                            cmd.Parameters["@d5"].Value = messages.Text;
                            cmd.ExecuteNonQuery();
                            btnSave.Enabled = false;
                            con.Close();
                            try
                            {
                                using (var client2 = new WebClient())
                                using (client2.OpenRead("http://client3.google.com/generate_204"))
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    cmd = con.CreateCommand();
                                    cmd.CommandText = "SELECT distinct RTRIM(Contact_No) FROM Student where ScholarNo='" + studentsnos + "'";
                                    rdr = cmd.ExecuteReader();
                                    if (rdr.Read())
                                    {
                                        numberphone = rdr.GetString(0).Trim();
                                    }
                                    if ((rdr != null))
                                    {
                                        rdr.Close();
                                    }
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    int stringcount = numberphone.Length;
                                    if (stringcount == 10)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }
                                    else if (stringcount < 10 && stringcount == 9)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = "+256" + numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }
                                    else if (stringcount > 10 && stringcount == 12)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }

                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Could not send message because your computer is not connected to internet");
                            }
                        }
                        MessageBox.Show("Successfully sent messages");
                    }
                    else if (rball.Checked)
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT distinct RTRIM(ScholarNo) FROM Student where  Session='" + Year.Text + "' and Term='" + term.Text + "'";
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            studentsnos = rdr.GetString(0);
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "insert into Messages(Year,Term,Date,StudentNo,Message) VALUES (@d1,@d2,@d3,@d4,@d5)";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "Year"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Date"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 15, "StudentNo"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 1000, "Message"));
                            cmd.Parameters["@d1"].Value = Year.Text;
                            cmd.Parameters["@d2"].Value = term.Text;
                            cmd.Parameters["@d3"].Value = dtp.Text;
                            cmd.Parameters["@d4"].Value = studentsnos;
                            cmd.Parameters["@d5"].Value = messages.Text;
                            cmd.ExecuteNonQuery();
                            btnSave.Enabled = false;
                            con.Close();
                            try
                            {
                                using (var client2 = new WebClient())
                                using (client2.OpenRead("http://client3.google.com/generate_204"))
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    cmd = con.CreateCommand();
                                    cmd.CommandText = "SELECT distinct RTRIM(Contact_No) FROM Student where ScholarNo='" + studentsnos + "'";
                                    rdr = cmd.ExecuteReader();
                                    if (rdr.Read())
                                    {
                                        numberphone = rdr.GetString(0).Trim();
                                    }
                                    if ((rdr != null))
                                    {
                                        rdr.Close();
                                    }
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    int stringcount = numberphone.Length;
                                    if (stringcount == 10)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }
                                    else if (stringcount < 10 && stringcount == 9)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = "+256" + numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }
                                    else if (stringcount > 10 && stringcount == 12)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numberss = numberphone;
                                        WebClient client = new WebClient();
                                        string numbers = numberphone;
                                        messaging = messages.Text + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messaging + "&numbers=" + numberss;
                                        client.OpenRead(baseURL);
                                    }
                                    else
                                    {

                                    }

                                    //MessageBox.Show("Successfully sent message");
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Could not send message because your computer is not connected to internet");
                            }
                        }
                        MessageBox.Show("Successfully sent messages");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rball_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                rball.Checked = true;
                rbtclass.Checked = false;
                rbtstudent.Checked = false;
                classes.Enabled = false;
                studentno.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbtclass_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                rball.Checked = false;
                rbtclass.Checked = true;
                rbtstudent.Checked = false;
                classes.Enabled = true;
                studentno.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void rbtstudent_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                rball.Checked = false;
                rbtclass.Checked = false;
                rbtstudent.Checked = true;
                classes.Enabled = false;
                studentno.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(ScholarNo) from Student where Session='" + Year.Text + "' and Term='" + term.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                studentno.Items.Clear();
                while (rdr.Read())
                {
                    studentno.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
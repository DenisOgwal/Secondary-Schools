using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmScholarshipStudent : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmScholarshipStudent()
        {
            InitializeComponent();
        }
        private void AutocompleteCourse()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Course) FROM FeePayment,Student where Student.ScholarNo=FeePayment.ScholarNo", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();
                classes.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());
                    classes.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AutocompleteScholarNo()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(ScholarNo) FROM FeePayment", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ScholarNo.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ScholarNo.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FeePaymentRecord_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            AutocompleteScholarNo();
        }

        private void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch.Items.Clear();
            Branch.Text = "";
            Branch.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct rtrim(branch) from student,Feepayment where Student.ScholarNo=FeePayment.scholarNo and course= '" + Course.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Branch.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScholarNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = DateTime.Today.ToString();
            Date_to.Text = DateTime.Today.ToString();
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
            Branch.Enabled = false;
        }

        private void FeePaymentRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmFeesPayment form2 = new frmFeesPayment();
            form2.label23.Text = label13.Text;
            form2.label24.Text = label14.Text;
            form2.Show();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmFeesPayment frm = new frmFeesPayment();
                // or simply use column name instead of index
                //dr.Cells["id"].Value.ToString();
                frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
                frm.ScholarNo.Text = dr.Cells[2].Value.ToString();
                frm.Year.Text = dr.Cells[1].Value.ToString();
                frm.StudentName.Text = dr.Cells[3].Value.ToString();
                frm.Course.Text = dr.Cells[4].Value.ToString();
                frm.Branch.Text = dr.Cells[5].Value.ToString();
                frm.FeeID.Text = dr.Cells[6].Value.ToString();
                frm.FDCourse.Text = dr.Cells[4].Value.ToString();
                frm.FDBranch.Text = dr.Cells[5].Value.ToString();
                frm.Term.Text = dr.Cells[7].Value.ToString();
                frm.PaymentDate.Text = dr.Cells[8].Value.ToString();
                frm.ModeOfPayment.Text = dr.Cells[9].Value.ToString();
                frm.PaymentModeDetails.Text = dr.Cells[10].Value.ToString();
                frm.TotalPaid.Text = dr.Cells[11].Value.ToString();
                frm.Fine.Text = dr.Cells[12].Value.ToString();
                frm.Delete.Enabled = true;
                frm.Update_record.Enabled = true;
                frm.Print.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.label23.Text = label13.Text;
                frm.label24.Text = label14.Text;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView2.SelectedRows[0];
                this.Hide();
                frmFeesPayment frm = new frmFeesPayment();
                // or simply use column name instead of index
                //dr.Cells["id"].Value.ToString();

                frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
                frm.ScholarNo.Text = dr.Cells[2].Value.ToString();
                frm.Year.Text = dr.Cells[1].Value.ToString();
                frm.StudentName.Text = dr.Cells[3].Value.ToString();
                frm.Course.Text = dr.Cells[4].Value.ToString();
                frm.Branch.Text = dr.Cells[5].Value.ToString();
                frm.FeeID.Text = dr.Cells[6].Value.ToString();
                frm.FDCourse.Text = dr.Cells[4].Value.ToString();
                frm.FDBranch.Text = dr.Cells[5].Value.ToString();
                frm.Term.Text = dr.Cells[7].Value.ToString();
                frm.PaymentDate.Text = dr.Cells[8].Value.ToString();
                frm.ModeOfPayment.Text = dr.Cells[9].Value.ToString();
                frm.PaymentModeDetails.Text = dr.Cells[10].Value.ToString();
                frm.TotalPaid.Text = dr.Cells[11].Value.ToString();
                frm.Fine.Text = dr.Cells[12].Value.ToString();
                frm.Delete.Enabled = true;
                frm.Update_record.Enabled = true;
                frm.Print.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.label23.Text = label13.Text;
                frm.label24.Text = label14.Text;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView4_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* try
             {
                 DataGridViewRow dr = dataGridView4.SelectedRows[0];
                 this.Hide();
                 frmFeesPayment frm = new frmFeesPayment();
                 // or simply use column name instead of index
                 //dr.Cells["id"].Value.ToString();
                 frm.Show();
                 frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
                 frm.ScholarNo.Text = dr.Cells[2].Value.ToString();
                 frm.Year.Text = dr.Cells[1].Value.ToString();
                 frm.StudentName.Text = dr.Cells[3].Value.ToString();
                 frm.Course.Text = dr.Cells[4].Value.ToString();
                 frm.Branch.Text = dr.Cells[5].Value.ToString();
                 frm.FeeID.Text = dr.Cells[6].Value.ToString();
                 frm.FDCourse.Text = dr.Cells[4].Value.ToString();
                 frm.FDBranch.Text = dr.Cells[5].Value.ToString();
                 frm.Term.Text = dr.Cells[7].Value.ToString();
                 frm.PaymentDate.Text = dr.Cells[8].Value.ToString();
                 frm.ModeOfPayment.Text = dr.Cells[9].Value.ToString();
                 frm.PaymentModeDetails.Text = dr.Cells[10].Value.ToString();
                 frm.TotalPaid.Text = dr.Cells[11].Value.ToString();
                 frm.Fine.Text = dr.Cells[12].Value.ToString();
                 frm.DueFees.Text = dr.Cells[13].Value.ToString();
                 if (label13.Text == "HEADMASTER")
                 {
                     frm.Delete.Enabled = true;
                     frm.Update_record.Enabled = true;
                     frm.Print.Enabled = true;
                     frm.btnSave.Enabled = false;
                     frm.label23.Text = label13.Text;
                     frm.label24.Text = label14.Text;
                 }
                 else
                 {
                     frm.Delete.Enabled = false;
                     frm.Update_record.Enabled = false;
                     frm.Print.Enabled = true;
                     frm.btnSave.Enabled = false;
                     frm.label23.Text = label13.Text;
                     frm.label24.Text = label14.Text;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/
        }

        private void dataGridView5_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Course.Text == "")
                {
                    MessageBox.Show("Please select Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(ScholarNo)[LIN],RTrim(Student_name)[Student Name],RTrim(PaymentDate)[Date], RTRIM(Year)[Year],RTRIM(Class)[Class],RTRIM(Term)[Term],RTRIM(Percentage)[Scholarship Percentage],RTRIM(AmountPayable)[Amount Payable]  from ScholarshipStudents where PaymentDate between @date1 and @date2 and Class= '" + Course.Text + "' order by ID Desc", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "PaymentDate").Value = Date_from.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "PaymentDate").Value = Date_to.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "ScholarshipStudents");
                dataGridView1.DataSource = myDataSet.Tables["ScholarshipStudents"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            dataGridView1.DataSource = null;
            Branch.Enabled = false;
        }

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;
                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView2.RowCount - 1;
                colsTotal = dataGridView2.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView2.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView2.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;
                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dataGridView4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dataGridView5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }
        string numberphone = null;
        string messages = null;
        string studentsnos = null;
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (dataGridView6.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView6.RowCount - 1;
                colsTotal = dataGridView6.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView6.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView6.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;
                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            classes.Text = "";
            terms.Text = "";
            years.Text = "";
            dataGridView6.DataSource = null;
        }

        private void classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            terms.Items.Clear();
            terms.Text = "";
            terms.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct rtrim(Semester) from FeePayment where FDCourse= '" + classes.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    terms.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void terms_SelectedIndexChanged(object sender, EventArgs e)
        {
            years.Items.Clear();
            years.Text = "";
            years.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct rtrim(Year) from FeePayment where FDCourse= '" + classes.Text + "' and Semester='" + terms.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    years.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(ScholarNo)[LIN],RTrim(Student_name)[Student Name],RTrim(PaymentDate)[Date], RTRIM(Year)[Year],RTRIM(Class)[Class],RTRIM(Term)[Term],RTRIM(Percentage)[Scholarship Percentage],RTRIM(AmountPayable)[Amount Payable]  from ScholarshipStudents where Term='" + terms.Text + "' and  Class='" + classes.Text + "' and Year='" + years.Text + "' order by ID Desc ", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "ScholarshipStudents");
                dataGridView6.DataSource = myDataSet.Tables["ScholarshipStudents"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(ScholarNo)[LIN],RTrim(Student_name)[Student Name],RTrim(PaymentDate)[Date], RTRIM(Year)[Year],RTRIM(Class)[Class],RTRIM(Term)[Term],RTRIM(Percentage)[Scholarship Percentage],RTRIM(AmountPayable)[Amount Payable]  from ScholarshipStudents where ScholarNo like '" + Search.Text + "%' OR Student_name like '" + Search.Text + "%'  order by ID Desc", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "ScholarshipStudents");
                dataGridView2.DataSource = myDataSet.Tables["ScholarshipStudents"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScholarNo_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new frmClientDetails();
            frm.ShowDialog();
            ScholarNo.Text = frm.clientnames.Text;
        }

        private void ScholarNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(ScholarNo)[LIN],RTrim(Student_name)[Student Name],RTrim(PaymentDate)[Date], RTRIM(Year)[Year],RTRIM(Class)[Class],RTRIM(Term)[Term],RTRIM(Percentage)[Scholarship Percentage],RTRIM(AmountPayable)[Amount Payable]  from ScholarshipStudents where ScholarNo= '" + ScholarNo.Text + "' order by ID Desc", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "ScholarshipStudents");
                dataGridView2.DataSource = myDataSet.Tables["ScholarshipStudents"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}













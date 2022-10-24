﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmBusFeePaymentRecord1 : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmBusFeePaymentRecord1()
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Course) FROM BusFeePayment,Student,BusHolders where BusFeePayment.ScholarNo=Student.ScholarNo and BusHolders.ScholarNo=Student.ScholarNo", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(ScholarNo) FROM BusFeePayment", CN);
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
        private void frmBusFeePaymentRecord_Load(object sender, EventArgs e)
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
                string ct = "select distinct RTRIM(branch) from BusFeePayment,BusHolders,Student where BusFeePayment.ScholarNo=Student.ScholarNo and BusHolders.ScholarNo=Student.ScholarNo and Student.course= '" + Course.Text + "'";
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

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView3.DataSource == null)
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
                rowsTotal = dataGridView3.RowCount - 1;
                colsTotal = dataGridView3.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView3.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView3.Rows[I].Cells[j].Value;
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

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(BusFeePayment.FeePaymentID)[Fee Payment ID], RTRIM(BusFeePayment.ScholarNo)[LIN],RTRIM(BusFeePayment.Student_name)[Student Name],RTRIM(BusFeePayment.Class)[Class],RTRIM(BusFeePayment.BusCharges)[Bus Charges],RTRIM(TotalPaid)[Total Paid],RTRIM(Fine)[Fine],RTRIM(BusFeePayment.DueFees)[Due Fees],RTRIM(BusFeePayment.Year)[Year] ,RTRIM(BusFeePayment.Term)[Term] from BusFeePayment INNER JOIN (SELECT ScholarNo,Class,Year,Term, Max(ID) as ID from BusFeePayment group by ScholarNo,Class,Year,Term) AS b ON BusFeePayment.ScholarNo=b.ScholarNo and BusFeePayment.Class=b.Class and BusFeePayment.Year=b.Year and BusFeePayment.Term=b.Term and BusFeePayment.ID=b.ID and DateOfPayment between @date1 and @date2 and DueFees > 0", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = DateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = DateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BusFeePayment");
                dataGridView4.DataSource = myDataSet.Tables["BusFeePayment"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView4.DataSource == null)
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
                rowsTotal = dataGridView4.RowCount - 1;
                colsTotal = dataGridView4.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView4.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView4.Rows[I].Cells[j].Value;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView5.DataSource == null)
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
                rowsTotal = dataGridView5.RowCount - 1;
                colsTotal = dataGridView5.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView5.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView5.Rows[I].Cells[j].Value;
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = DateTime.Today.ToString();
            Date_to.Text = DateTime.Today.ToString();
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
            dataGridView5.DataSource = null;
            PaymentDateFrom.Text = DateTime.Today.ToString();
            PaymentDateTo.Text = DateTime.Today.ToString();
            DateFrom.Text = DateTime.Today.ToString();
            DateTo.Text = DateTime.Today.ToString();
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
            Branch.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            dataGridView1.DataSource = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PaymentDateFrom.Text = System.DateTime.Today.ToString();
            PaymentDateTo.Text = System.DateTime.Today.ToString();
            dataGridView3.DataSource = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DateFrom.Text = System.DateTime.Today.ToString();
            DateTo.Text = System.DateTime.Today.ToString();
            dataGridView4.DataSource = null;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = System.DateTime.Today.ToString();
            dateTimePicker2.Text = System.DateTime.Today.ToString();
            dataGridView5.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Course.Text == "")
                {
                    MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(FeePaymentID)[Fee Payment ID], RTRIM(BusFeePayment.ScholarNo)[LIN],RTRIM(BusFeePayment.Student_name)[Student Name],RTRIM(BusFeePayment.Class)[Class],RTRIM(Transportation.SourceLocation)[Source Location],RTRIM(BusFeePayment.BusCharges)[Bus Charges],RTRIM(DateOfPayment)[Payment Date],RTRIM(ModeOfPayment)[Mode Of Payment],RTRIM(PaymentModeDetails)[Payment Mode Details],RTRIM(TotalPaid)[Total Paid],RTRIM(Fine)[Fine],RTRIM(DueFees)[Due Fees],RTRIM(BusFeePayment.Year)[Year],RTRIM(BusFeePayment.Term)[Term] from BusFeePayment,Transportation,BusHolders where BusFeePayment.ScholarNo=BusHolders.ScholarNo and BusFeePayment.Class=BusHolders.Class and BusFeePayment.Year=BusHolders.Year and BusFeePayment.Term=BusHolders.Term and Transportation.SourceLocation=BusHolders.SourceLocation and DateOfPayment between @date1 and @date2 and BusFeePayment.Class= '" + Course.Text + "'  order by DateOfPayment", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = Date_from.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = Date_to.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BusFeePayment");
                myDA.Fill(myDataSet, "Transportation");
                myDA.Fill(myDataSet, "BusHolders");
                dataGridView1.DataSource = myDataSet.Tables["BusFeePayment"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["Transportation"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["BusHolders"].DefaultView;
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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(FeePaymentID)[Fee Payment ID], RTRIM(BusFeePayment.ScholarNo)[LIN],RTRIM(BusFeePayment.Student_name)[Student Name],RTRIM(BusFeePayment.Class)[Class],RTRIM(Transportation.SourceLocation)[Source Location],RTRIM(BusFeePayment.BusCharges)[Bus Charges],RTRIM(DateOfPayment)[Payment Date],RTRIM(ModeOfPayment)[Mode Of Payment],RTRIM(PaymentModeDetails)[Payment Mode Details],RTRIM(TotalPaid)[Total Paid],RTRIM(Fine)[Fine],RTRIM(DueFees)[Due Fees],RTRIM(BusFeePayment.Year)[Year],RTRIM(BusFeePayment.Term)[Term] from BusFeePayment,Transportation,BusHolders where BusFeePayment.ScholarNo=BusHolders.ScholarNo and BusFeePayment.Class=BusHolders.Class and BusFeePayment.Year=BusHolders.Year and BusFeePayment.Term=BusHolders.Term and Transportation.SourceLocation=BusHolders.SourceLocation and DateOfPayment between @date1 and @date2 order by DateOfPayment", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = PaymentDateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = PaymentDateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BusFeePayment");
                myDA.Fill(myDataSet, "Transportation");
                myDA.Fill(myDataSet, "BusHolders");
                dataGridView3.DataSource = myDataSet.Tables["BusFeePayment"].DefaultView;
                dataGridView3.DataSource = myDataSet.Tables["Transportation"].DefaultView;
                dataGridView3.DataSource = myDataSet.Tables["BusHolders"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(FeePaymentID)[Fee Payment ID], RTRIM(BusFeePayment.ScholarNo)[LIN],RTRIM(BusFeePayment.Student_name)[Student Name],RTRIM(BusFeePayment.Class)[Class],RTRIM(Transportation.SourceLocation)[Source Location],RTRIM(BusFeePayment.BusCharges)[Bus Charges],RTRIM(DateOfPayment)[Payment Date],RTRIM(ModeOfPayment)[Mode Of Payment],RTRIM(PaymentModeDetails)[Payment Mode Details],RTRIM(TotalPaid)[Total Paid],RTRIM(Fine)[Fine],RTRIM(DueFees)[Due Fees],RTRIM(BusFeePayment.Year)[Year],RTRIM(BusFeePayment.Term)[Term] from BusFeePayment,Transportation,BusHolders where BusFeePayment.ScholarNo=BusHolders.ScholarNo and BusFeePayment.Class=BusHolders.Class and BusFeePayment.Year=BusHolders.Year and BusFeePayment.Term=BusHolders.Term and Transportation.SourceLocation=BusHolders.SourceLocation and DateOfPayment between @date1 and @date2 and Fine > 0 order by DateOfPayment", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "DateOfPayment").Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "DateOfPayment").Value = dateTimePicker2.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BusFeePayment");
                myDA.Fill(myDataSet, "Transportation");
                myDA.Fill(myDataSet, "BusHolders");
                dataGridView5.DataSource = myDataSet.Tables["BusFeePayment"].DefaultView;
                dataGridView5.DataSource = myDataSet.Tables["Transportation"].DefaultView;
                dataGridView5.DataSource = myDataSet.Tables["BusHolders"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView4.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView4.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView5.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView5.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            //frm.Show();
            frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
            frm.ScholarNo.Text = dr.Cells[1].Value.ToString();
            frm.StudentName.Text = dr.Cells[2].Value.ToString();
            frm.Course.Text = dr.Cells[3].Value.ToString();
            frm.Branch.Text = dr.Cells[4].Value.ToString();
            frm.txtSourceLocation.Text = dr.Cells[5].Value.ToString();
            //frm.txtBusCharges.Text = dr.Cells[6].Value.ToString();
            frm.PaymentDate.Text = dr.Cells[6].Value.ToString();
            frm.ModeOfPayment.Text = dr.Cells[7].Value.ToString();
            frm.PaymentModeDetails.Text = dr.Cells[8].Value.ToString();
            frm.Fine.Text = dr.Cells[10].Value.ToString();
            frm.TotalPaid.Text = dr.Cells[9].Value.ToString();
            //frm.DueFees.Text = dr.Cells[12].Value.ToString();
            frm.Year.Text = dr.Cells[11].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.btnSave.Enabled = false;
            frm.Print.Enabled = true;
            frm.Show();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView2.SelectedRows[0];
            this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            //frm.Show();
            frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
            frm.ScholarNo.Text = dr.Cells[1].Value.ToString();
            frm.StudentName.Text = dr.Cells[2].Value.ToString();
            frm.Course.Text = dr.Cells[3].Value.ToString();
            frm.Branch.Text = dr.Cells[4].Value.ToString();
            frm.txtSourceLocation.Text = dr.Cells[5].Value.ToString();
            //frm.txtBusCharges.Text = dr.Cells[6].Value.ToString();
            frm.PaymentDate.Text = dr.Cells[6].Value.ToString();
            frm.ModeOfPayment.Text = dr.Cells[7].Value.ToString();
            frm.PaymentModeDetails.Text = dr.Cells[8].Value.ToString();
            frm.Fine.Text = dr.Cells[10].Value.ToString();
            frm.TotalPaid.Text = dr.Cells[9].Value.ToString();
            //frm.DueFees.Text = dr.Cells[12].Value.ToString();
            frm.Year.Text = dr.Cells[11].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.btnSave.Enabled = false;
            frm.Print.Enabled = true;
            frm.Show();
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView3.SelectedRows[0];
            this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            //frm.Show();
            frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
            frm.ScholarNo.Text = dr.Cells[1].Value.ToString();
            frm.StudentName.Text = dr.Cells[2].Value.ToString();
            frm.Course.Text = dr.Cells[3].Value.ToString();
            frm.Branch.Text = dr.Cells[4].Value.ToString();
            frm.txtSourceLocation.Text = dr.Cells[5].Value.ToString();
            //frm.txtBusCharges.Text = dr.Cells[6].Value.ToString();
            frm.PaymentDate.Text = dr.Cells[6].Value.ToString();
            frm.ModeOfPayment.Text = dr.Cells[7].Value.ToString();
            frm.PaymentModeDetails.Text = dr.Cells[8].Value.ToString();
            frm.Fine.Text = dr.Cells[10].Value.ToString();
            frm.TotalPaid.Text = dr.Cells[9].Value.ToString();
            //frm.DueFees.Text = dr.Cells[12].Value.ToString();
            frm.Year.Text = dr.Cells[11].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.btnSave.Enabled = false;
            frm.Print.Enabled = true;
            frm.Show();
        }

        private void dataGridView4_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView4.SelectedRows[0];
            this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();

            frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
            frm.ScholarNo.Text = dr.Cells[1].Value.ToString();
            frm.StudentName.Text = dr.Cells[2].Value.ToString();
            frm.Course.Text = dr.Cells[3].Value.ToString();
            frm.Branch.Text = dr.Cells[4].Value.ToString();
            frm.txtSourceLocation.Text = dr.Cells[5].Value.ToString();
            //frm.txtBusCharges.Text = dr.Cells[6].Value.ToString();
            frm.PaymentDate.Text = dr.Cells[6].Value.ToString();
            frm.ModeOfPayment.Text = dr.Cells[7].Value.ToString();
            frm.PaymentModeDetails.Text = dr.Cells[8].Value.ToString();
            frm.Fine.Text = dr.Cells[10].Value.ToString();
            frm.TotalPaid.Text = dr.Cells[9].Value.ToString();
            frm.DueFees.Text = dr.Cells[11].Value.ToString();
            frm.Year.Text = dr.Cells[12].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.btnSave.Enabled = false;
            frm.Print.Enabled = true;
            frm.Show();
        }

        private void dataGridView5_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView5.SelectedRows[0];
            this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            //frm.Show();
            frm.FeePaymentID.Text = dr.Cells[0].Value.ToString();
            frm.ScholarNo.Text = dr.Cells[1].Value.ToString();
            frm.StudentName.Text = dr.Cells[2].Value.ToString();
            frm.Course.Text = dr.Cells[3].Value.ToString();
            frm.Branch.Text = dr.Cells[4].Value.ToString();
            frm.txtSourceLocation.Text = dr.Cells[5].Value.ToString();
            frm.txtBusCharges.Text = dr.Cells[6].Value.ToString();
            frm.PaymentDate.Text = dr.Cells[7].Value.ToString();
            frm.ModeOfPayment.Text = dr.Cells[8].Value.ToString();
            frm.PaymentModeDetails.Text = dr.Cells[9].Value.ToString();
            frm.Fine.Text = dr.Cells[11].Value.ToString();
            frm.TotalPaid.Text = dr.Cells[10].Value.ToString();
            frm.DueFees.Text = dr.Cells[12].Value.ToString();
            frm.Year.Text = dr.Cells[13].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.btnSave.Enabled = false;
            frm.Print.Enabled = true;
            frm.Show();
        }

        private void frmBusFeePaymentRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* this.Hide();
             frmBusFeePayment frm = new frmBusFeePayment();
             frm.label3.Text = label13.Text;
             frm.label4.Text = label14.Text;
             frm.Show();*/
        }

        private void frmBusFeePaymentRecord1_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            // AutocompleteScholarNo();
        }

        private void frmBusFeePaymentRecord1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmBusFeePayment frm = new frmBusFeePayment();
            frm.label3.Text = label13.Text;
            frm.label4.Text = label14.Text;
            frm.Show();*/
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
                cmd = new SqlCommand("select RTrim(FeePaymentID)[Fee Payment ID], RTRIM(BusFeePayment.ScholarNo)[LIN],RTRIM(BusFeePayment.Student_name)[Student Name],RTRIM(BusFeePayment.Class)[Class],RTRIM(Transportation.SourceLocation)[Source Location],RTRIM(BusFeePayment.BusCharges)[Bus Charges],RTRIM(DateOfPayment)[Payment Date],RTRIM(ModeOfPayment)[Mode Of Payment],RTRIM(PaymentModeDetails)[Payment Mode Details],RTRIM(TotalPaid)[Total Paid],RTRIM(Fine)[Fine],RTRIM(DueFees)[Due Fees],RTRIM(BusFeePayment.Year)[Year],RTRIM(BusFeePayment.Term)[Term] from BusFeePayment,Transportation,BusHolders where BusFeePayment.ScholarNo=BusHolders.ScholarNo and BusFeePayment.Class=BusHolders.Class and BusFeePayment.Year=BusHolders.Year and BusFeePayment.Term=BusHolders.Term and Transportation.SourceLocation=BusHolders.SourceLocation and BusFeePayment.ScholarNo = '" + ScholarNo.Text + "' order by DateOfPayment", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BusFeePayment");
                myDA.Fill(myDataSet, "Transportation");
                myDA.Fill(myDataSet, "BusHolders");
                dataGridView2.DataSource = myDataSet.Tables["BusFeePayment"].DefaultView;
                dataGridView2.DataSource = myDataSet.Tables["Transportation"].DefaultView;
                dataGridView2.DataSource = myDataSet.Tables["BusHolders"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}




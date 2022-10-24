﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmSalaryPaymentRecord1 : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmSalaryPaymentRecord1()
        {
            InitializeComponent();
        }

        private void cmbStaffName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbStaffName.Text = cmbStaffName.Text.Trim();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(PaymentID)[Payment ID], RTRIM(Employee.StaffID)[Staff ID],RTRIM(Employee.StaffName)[Staff Name], RTRIM(PaymentDate)[Payment Date],RTRIM(ModeOfPayment)[Payment Mode], RTRIM(PaymentModeDetails)[Payment Mode Details], RTRIM(Deduction)[Deduction],RTRIM(TotalPaid)[Total Paid],RTRIM(Months)[Months],RTRIM(Year)[Year],RTRIM(Term)[Term] from EmployeePayment,Employee where EmployeePayment.StaffID=Employee.StaffID and  Employee.StaffName= '" + cmbStaffName.Text + "' order by PaymentDate", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Employee");
                myDA.Fill(myDataSet, "EmployeePayment");
                DGV.DataSource = myDataSet.Tables["Employee"].DefaultView;
                DGV.DataSource = myDataSet.Tables["EmployeePayment"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            DGV.DataSource = null;
            dataGridView1.DataSource = null;
            cmbStaffName.Text = "";
            DateFrom.Text = DateTime.Today.ToString();
            DateTo.Text = DateTime.Today.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(PaymentID)[Payment ID], RTRIM(Employee.StaffID)[Staff ID],RTRIM(Employee.StaffName)[Staff Name], RTRIM(PaymentDate)[Payment Date],RTRIM(ModeOfPayment)[Payment Mode], RTRIM(PaymentModeDetails)[Payment Mode Details], RTRIM(Deduction)[Deduction],RTRIM(TotalPaid)[Total Paid],RTRIM(Months)[Months],RTRIM(Year)[Year],RTRIM(Term)[Term] from EmployeePayment,Employee where Employee.StaffID=EmployeePayment.StaffID and PaymentDate between @date1 and @date2 order by PaymentDate", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " PaymentDate").Value = DateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "PaymentDate").Value = DateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Employee");
                myDA.Fill(myDataSet, "EmployeePayment");
                dataGridView1.DataSource = myDataSet.Tables["Employee"].DefaultView;
                dataGridView1.DataSource = myDataSet.Tables["EmployeePayment"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DGV.DataSource = null;
            cmbStaffName.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DateFrom.Text = DateTime.Today.ToString();
            DateTo.Text = DateTime.Today.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            if (DGV.DataSource == null)
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
                rowsTotal = DGV.RowCount - 1;
                colsTotal = DGV.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DGV.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DGV.Rows[I].Cells[j].Value;
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

        private void frmSalaryPaymentRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmSalaryPayment frm = new frmSalaryPayment();
            frm.label6.Text = label1.Text;
            frm.label7.Text = label3.Text;
            frm.Show();*/
        }
        private void AutocompleteStaffName()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(StaffName) FROM EmployeePayment,Employee where EmployeePayment.StaffID=Employee.StaffID", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbStaffName.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    cmbStaffName.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void frmSalaryPaymentRecord_Load(object sender, EventArgs e)
        {
            AutocompleteStaffName();
        }

        private void DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DGV.SelectedRows[0];
                this.Hide();
                frmSalaryPayment frm = new frmSalaryPayment();
                frm.label7.Text = label3.Text;

                frm.txtPaymentID.Text = dr.Cells[0].Value.ToString();
                frm.cmbStaffID.Text = dr.Cells[1].Value.ToString();
                frm.txtStaffName.Text = dr.Cells[2].Value.ToString();
                //frm.txtBasicSalary.Text = dr.Cells[3].Value.ToString();
                frm.dtpPaymentDate.Text = dr.Cells[3].Value.ToString();
                frm.cmbModeOfPayment.Text = dr.Cells[4].Value.ToString();
                frm.txtPaymentModeDetails.Text = dr.Cells[5].Value.ToString();
                frm.txtDeduction.Text = dr.Cells[6].Value.ToString();
                frm.txtTotalPaid.Text = dr.Cells[7].Value.ToString();
                //frm.Duepayment.Text = dr.Cells[9].Value.ToString();
                //frm.months.Text = dr.Cells[10].Value.ToString();
                frm.Year.Text = dr.Cells[8].Value.ToString();
                frm.term.Text = dr.Cells[9].Value.ToString();
                frm.btnDelete.Enabled = true;
                frm.btnUpdate_record.Enabled = true;
                frm.label6.Text = label1.Text;
                frm.label7.Text = label3.Text;
                frm.btnPrint.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmSalaryPayment frm = new frmSalaryPayment();
                frm.label7.Text = label3.Text;
                frm.txtPaymentID.Text = dr.Cells[0].Value.ToString();
                frm.cmbStaffID.Text = dr.Cells[1].Value.ToString();
                frm.txtStaffName.Text = dr.Cells[2].Value.ToString();
                //frm.txtBasicSalary.Text = dr.Cells[3].Value.ToString();
                frm.dtpPaymentDate.Text = dr.Cells[3].Value.ToString();
                frm.cmbModeOfPayment.Text = dr.Cells[4].Value.ToString();
                frm.txtPaymentModeDetails.Text = dr.Cells[5].Value.ToString();
                frm.txtDeduction.Text = dr.Cells[6].Value.ToString();
                frm.txtTotalPaid.Text = dr.Cells[7].Value.ToString();
                //frm.Duepayment.Text = dr.Cells[9].Value.ToString();
                //frm.months.Text = dr.Cells[10].Value.ToString();
                frm.Year.Text = dr.Cells[8].Value.ToString();
                frm.term.Text = dr.Cells[9].Value.ToString();
                frm.btnDelete.Enabled = true;
                frm.btnUpdate_record.Enabled = true;
                frm.label6.Text = label1.Text;
                frm.label7.Text = label3.Text;
                frm.btnPrint.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DGV.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DGV.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(PaymentID)[Payment ID], RTRIM(Employee.StaffID)[Staff ID],RTRIM(Employee.StaffName)[Staff Name], RTRIM(PaymentDate)[Payment Date],RTRIM(ModeOfPayment)[Payment Mode], RTRIM(PaymentModeDetails)[Payment Mode Details], RTRIM(Deduction)[Deduction],RTRIM(TotalPaid)[Total Paid],RTRIM(Months)[Months],RTRIM(Year)[Year],RTRIM(Term)[Term],RTRIM(DueFees)[Due Payment] from EmployeePayment,Employee where Employee.StaffID=EmployeePayment.StaffID and DueFees !=0 and PaymentDate between @date1 and @date2 order by PaymentDate", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " PaymentDate").Value = datefrom1.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "PaymentDate").Value = dateTo1.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Employee");
                myDA.Fill(myDataSet, "EmployeePayment");
                dataGridView2.DataSource = myDataSet.Tables["Employee"].DefaultView;
                dataGridView2.DataSource = myDataSet.Tables["EmployeePayment"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            datefrom1.Text = DateTime.Today.ToString();
            dateTo1.Text = DateTime.Today.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
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

    }
}

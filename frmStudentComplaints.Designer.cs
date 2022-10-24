namespace College_Management_System
{
    partial class frmStudentComplaints
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudentComplaints));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.times = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.complaintdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.stdname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.description = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.department = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.staff = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.compsubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.times);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.complaintdate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.stdname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 123);
            this.panel1.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(465, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 22);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            this.label8.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(566, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // times
            // 
            this.times.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.times.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.times.ForeColor = System.Drawing.SystemColors.Desktop;
            this.times.Items.AddRange(new object[] {
            "         1st",
            "         2nd",
            "         3rd",
            "         4th",
            "         5th",
            "         6th",
            "         7th",
            "         8th",
            "         9th",
            "         10th"});
            this.times.Location = new System.Drawing.Point(230, 66);
            this.times.MaxLength = 10;
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(186, 30);
            this.times.TabIndex = 5;
            this.times.SelectedIndexChanged += new System.EventHandler(this.times_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Times Complained:";
            // 
            // complaintdate
            // 
            this.complaintdate.CustomFormat = "dd/ MM/ yyyy";
            this.complaintdate.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.complaintdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.complaintdate.Location = new System.Drawing.Point(508, 15);
            this.complaintdate.Name = "complaintdate";
            this.complaintdate.Size = new System.Drawing.Size(200, 29);
            this.complaintdate.TabIndex = 3;
            this.complaintdate.ValueChanged += new System.EventHandler(this.complaintdate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(432, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date:";
            // 
            // stdname
            // 
            this.stdname.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdname.Location = new System.Drawing.Point(230, 11);
            this.stdname.Name = "stdname";
            this.stdname.Size = new System.Drawing.Size(186, 29);
            this.stdname.TabIndex = 1;
            this.stdname.TextChanged += new System.EventHandler(this.stdname_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = " Student Name:";
            // 
            // panel2
            // 
            this.panel2.BackColor = global::College_Management_System.Properties.Settings.Default.usercolor;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.description);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.department);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.staff);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.compsubject);
            this.panel2.Controls.Add(this.label4);
            this.panel2.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::College_Management_System.Properties.Settings.Default, "usercolor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(13, 172);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 267);
            this.panel2.TabIndex = 59;
            // 
            // description
            // 
            this.description.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.Location = new System.Drawing.Point(191, 162);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(490, 89);
            this.description.TabIndex = 7;
            this.description.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 22);
            this.label7.TabIndex = 6;
            this.label7.Text = "Complaint Description:";
            // 
            // department
            // 
            this.department.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.department.FormattingEnabled = true;
            this.department.Location = new System.Drawing.Point(191, 120);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(490, 30);
            this.department.TabIndex = 5;
            this.department.SelectedIndexChanged += new System.EventHandler(this.department_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 22);
            this.label6.TabIndex = 4;
            this.label6.Text = "Department:";
            // 
            // staff
            // 
            this.staff.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staff.FormattingEnabled = true;
            this.staff.Location = new System.Drawing.Point(191, 77);
            this.staff.Name = "staff";
            this.staff.Size = new System.Drawing.Size(490, 30);
            this.staff.TabIndex = 3;
            this.staff.SelectedIndexChanged += new System.EventHandler(this.staff_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "Staff Concerned:";
            // 
            // compsubject
            // 
            this.compsubject.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compsubject.Location = new System.Drawing.Point(191, 31);
            this.compsubject.Name = "compsubject";
            this.compsubject.Size = new System.Drawing.Size(490, 29);
            this.compsubject.TabIndex = 1;
            this.compsubject.TextChanged += new System.EventHandler(this.compsubject_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(18, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "Complaint Subject:";
            // 
            // frmStudentComplaints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::College_Management_System.Properties.Settings.Default.usercolor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(746, 456);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::College_Management_System.Properties.Settings.Default, "usercolor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmStudentComplaints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Student Complaints";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStudentComplaints_FormClosing);
            this.Load += new System.EventHandler(this.frmStudentComplaints_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker complaintdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stdname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox description;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox department;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox staff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox compsubject;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox times;
        private System.Windows.Forms.Label label8;
    }
}
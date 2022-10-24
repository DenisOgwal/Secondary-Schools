namespace College_Management_System
{
    partial class frmMessaging
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessaging));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.studentno = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.classes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtclass = new System.Windows.Forms.RadioButton();
            this.term = new System.Windows.Forms.ComboBox();
            this.Year = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.rbtstudent = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rball = new System.Windows.Forms.RadioButton();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.messages = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.NewRecord = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.studentno);
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.classes);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.rbtclass);
            this.GroupBox1.Controls.Add(this.term);
            this.GroupBox1.Controls.Add(this.Year);
            this.GroupBox1.Controls.Add(this.label28);
            this.GroupBox1.Controls.Add(this.label14);
            this.GroupBox1.Controls.Add(this.rbtstudent);
            this.GroupBox1.Controls.Add(this.label5);
            this.GroupBox1.Controls.Add(this.rball);
            this.GroupBox1.Controls.Add(this.dtp);
            this.GroupBox1.Controls.Add(this.messages);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(28, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(467, 287);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Message Details";
            // 
            // studentno
            // 
            this.studentno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.studentno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.studentno.Enabled = false;
            this.studentno.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentno.FormattingEnabled = true;
            this.studentno.Location = new System.Drawing.Point(97, 148);
            this.studentno.Name = "studentno";
            this.studentno.Size = new System.Drawing.Size(338, 30);
            this.studentno.TabIndex = 77;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 151);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 22);
            this.label6.TabIndex = 76;
            this.label6.Text = "LIN";
            // 
            // classes
            // 
            this.classes.Enabled = false;
            this.classes.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classes.FormattingEnabled = true;
            this.classes.Location = new System.Drawing.Point(288, 105);
            this.classes.Name = "classes";
            this.classes.Size = new System.Drawing.Size(147, 30);
            this.classes.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(232, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 22);
            this.label2.TabIndex = 74;
            this.label2.Text = "Class";
            // 
            // rbtclass
            // 
            this.rbtclass.AutoSize = true;
            this.rbtclass.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtclass.Location = new System.Drawing.Point(255, 30);
            this.rbtclass.Name = "rbtclass";
            this.rbtclass.Size = new System.Drawing.Size(63, 26);
            this.rbtclass.TabIndex = 73;
            this.rbtclass.TabStop = true;
            this.rbtclass.Text = "Class";
            this.rbtclass.UseVisualStyleBackColor = true;
            this.rbtclass.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtclass_MouseClick);
            // 
            // term
            // 
            this.term.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.term.FormattingEnabled = true;
            this.term.Location = new System.Drawing.Point(288, 62);
            this.term.Name = "term";
            this.term.Size = new System.Drawing.Size(147, 30);
            this.term.TabIndex = 72;
            this.term.SelectedIndexChanged += new System.EventHandler(this.term_SelectedIndexChanged);
            // 
            // Year
            // 
            this.Year.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Year.Location = new System.Drawing.Point(65, 61);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(121, 29);
            this.Year.TabIndex = 71;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(6, 65);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 22);
            this.label28.TabIndex = 70;
            this.label28.Text = "Year";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(232, 65);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 22);
            this.label14.TabIndex = 69;
            this.label14.Text = "Term";
            // 
            // rbtstudent
            // 
            this.rbtstudent.AutoSize = true;
            this.rbtstudent.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtstudent.Location = new System.Drawing.Point(362, 30);
            this.rbtstudent.Name = "rbtstudent";
            this.rbtstudent.Size = new System.Drawing.Size(81, 26);
            this.rbtstudent.TabIndex = 4;
            this.rbtstudent.TabStop = true;
            this.rbtstudent.Text = "Student";
            this.rbtstudent.UseVisualStyleBackColor = true;
            this.rbtstudent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtstudent_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Transaction Type";
            // 
            // rball
            // 
            this.rball.AutoSize = true;
            this.rball.Checked = true;
            this.rball.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rball.Location = new System.Drawing.Point(179, 30);
            this.rball.Name = "rball";
            this.rball.Size = new System.Drawing.Size(47, 26);
            this.rball.TabIndex = 0;
            this.rball.TabStop = true;
            this.rball.Text = "All";
            this.rball.UseVisualStyleBackColor = true;
            this.rball.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rball_MouseClick);
            // 
            // dtp
            // 
            this.dtp.CustomFormat = "dd/MMM/yyyy";
            this.dtp.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp.Location = new System.Drawing.Point(65, 100);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(121, 29);
            this.dtp.TabIndex = 2;
            // 
            // messages
            // 
            this.messages.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messages.Location = new System.Drawing.Point(97, 195);
            this.messages.Multiline = true;
            this.messages.Name = "messages";
            this.messages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messages.Size = new System.Drawing.Size(338, 83);
            this.messages.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(6, 212);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(68, 22);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Message";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(6, 105);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(42, 22);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Date";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.NewRecord);
            this.panel1.Location = new System.Drawing.Point(519, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 135);
            this.panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(16, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 55);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&SMS";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // NewRecord
            // 
            this.NewRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NewRecord.FlatAppearance.BorderSize = 0;
            this.NewRecord.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewRecord.Location = new System.Drawing.Point(16, 14);
            this.NewRecord.Name = "NewRecord";
            this.NewRecord.Size = new System.Drawing.Size(95, 55);
            this.NewRecord.TabIndex = 0;
            this.NewRecord.Text = "&New";
            this.NewRecord.UseVisualStyleBackColor = true;
            this.NewRecord.Click += new System.EventHandler(this.NewRecord_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(562, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Information";
            // 
            // frmMessaging
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::College_Management_System.Properties.Settings.Default.usercolor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(691, 324);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GroupBox1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::College_Management_System.Properties.Settings.Default, "usercolor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMessaging";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messaging";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmOtherTransaction_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton rbtstudent;
        public System.Windows.Forms.RadioButton rball;
        public System.Windows.Forms.DateTimePicker dtp;
        public System.Windows.Forms.TextBox messages;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button NewRecord;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox Year;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.ComboBox term;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.RadioButton rbtclass;
        public System.Windows.Forms.ComboBox classes;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox studentno;
        private System.Windows.Forms.Label label6;
    }
}
using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace College_Management_System
{
    public partial class FrmMainWindowImage : Form
    {
        public FrmMainWindowImage()
        {
            InitializeComponent();
        }

        private void textBoxX1_Click(object sender, EventArgs e)
        {
            try
            {
                var _with1 = openFileDialog1;
                _with1.Filter = ("Images |*.png; *.bmp; *.jpg;*.jpeg; *.gif; *.ico");
                _with1.FilterIndex = 4;
                //Clear the file name
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxX1.Text = openFileDialog1.FileName;
                    label10.Text = System.IO.Path.GetFileName(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = textBoxX1.Text;
                string destfileFile = @"\EssentialSchoolsFIles";
                bool exists = System.IO.Directory.Exists(destfileFile);

                if (exists)
                {
                    try
                    {
                        string destFile = Path.Combine(destfileFile, "MainImage.jpg");
                        File.Copy(sourceFile, destFile, true);
                        File.SetAttributes(destFile, FileAttributes.Normal);
                        MessageBox.Show("Successful, Please restart the system to continue", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //System.Environment.Exit(1);

                    }
                    catch (Exception)
                    {

                    }

                    /*if (File.Exists(@"C:\EssentialSchoolsFIles\MainImage.jpg"))
                    {
                        File.Delete(@"C:\EssentialSchoolsFIles\MainImage.jpg");
                    }*/
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMainWindowImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}

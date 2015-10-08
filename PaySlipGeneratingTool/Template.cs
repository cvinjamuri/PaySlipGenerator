using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySlipGeneratingTool
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();
        }

        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.*";
            txtTemplate.Text = op1.FileName;
        }

        private void btnUploadTemplate_Click(object sender, EventArgs e)
        {
            if (txtTemplate.Text == "")
            {
                MessageBox.Show("Please select file to upload... ");
                txtTemplate.Focus();
            }
            else
            {
                string sourceFile = txtTemplate.Text;
                string extention = Path.GetExtension(txtTemplate.Text);
                string destinationFile = "adt" + extention;
                string destinationFilepath = "../../" + destinationFile;
                if (File.Exists(destinationFilepath))
                {
                    File.Delete(destinationFilepath);
                }
                File.Copy(sourceFile, destinationFilepath);
                lblmsg.Text = "Template uploaded successfully";
            }
        }
    }
}

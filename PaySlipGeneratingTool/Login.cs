using Ini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySlipGeneratingTool
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        public bool LogonSuccessful { get; set; }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile("../../test.ini");
            string UserName = ini.IniReadValue("User1", "UN");
            string password = ini.IniReadValue("User1", "PWD");
            if (txtUser.Text == "" && txtPassword.Text == "")
            {
                LogonSuccessful = false;
                MessageBox.Show("Please enter User and Password");
                this.ActiveControl = txtUser;
            }
            else if (txtUser.Text == UserName && txtPassword.Text == password)
            {
                LogonSuccessful = true;

                this.Close();
            }
            else
            {
                LogonSuccessful = false;
                MessageBox.Show("Invalid Credentials");
                this.ActiveControl = txtUser;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

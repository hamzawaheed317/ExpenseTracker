using System;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Expense_Tracker_dll;
namespace ExpenseTracker
{
    public partial class Login : Form
    {
       private ExpenseTrackerMain expenseTrackerMain;
        public Login(ExpenseTrackerMain expenseTrackerMain)
        {
            InitializeComponent();  
            this.expenseTrackerMain = expenseTrackerMain;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (validate_email(textBox1.Text))
                {
                    User user = new User { Email = textBox1.Text, Password = textBox2.Text };
                    bool login = user.Login();
                    if (login)
                    {
                        Form1 dashboard = new Form1(user,this.expenseTrackerMain);
                        dashboard.Show();
                        dashboard.Open();
                        dashboard.Profile_See();
                        this.expenseTrackerMain.Hide();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username not Found!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Email", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Data Fields", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool validate_email(string email)
        {
          
            if (email.Contains("@"))
            {
                int atIndex = email.IndexOf('@');
                string domain = email.Substring(atIndex + 1); 
                if (domain.StartsWith("gmail"))
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return false;
            }


        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, EventArgs e)
        {
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

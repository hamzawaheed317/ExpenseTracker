using System;
using Expense_Tracker_dll;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="" && textBox2.Text!=""  && textBox3.Text !="")
            {
                if (textBox2.Text == textBox3.Text)
                {
                    User user = new User() { Email = textBox1.Text, Password = textBox2.Text ,DateOfBirth=guna2DateTimePicker1.Text };
                    string isregister = user.Register();
                    if (isregister== "Success")
                    {
                        MessageBox.Show("Registered Succesfully!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                    else
                    {
                        MessageBox.Show($"{isregister}", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Passward Not Matched!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Please Enter Data Fiels First!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

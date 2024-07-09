using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class ExpenseTrackerMain : Form
    {
        public ExpenseTrackerMain()
        {
            InitializeComponent();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Login login = new Login(this);
            login.ShowDialog();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            SignUp signup = new SignUp();
            signup.ShowDialog();
        }
    }
}

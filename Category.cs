using Expense_Tracker_dll;
using System.Windows.Forms;
using System;

namespace ExpenseTracker
{
    public partial class Category : Form
    {
        private User user;
        private Form1 form1;
        private ExpenseTrackerMain expenseTrackerMain;

        public Category(User user, Form1 form1, ExpenseTrackerMain expenseTrackerMain)
        {
            InitializeComponent();
            this.user = user;
            this.form1 = form1;
            this.expenseTrackerMain = expenseTrackerMain;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                Expense_Tracker_dll.Category category = new Expense_Tracker_dll.Category
                {
                    UserId = user.UserId,
                    Cate = guna2TextBox2.Text
                };

                string message = category.Add();
                if (message == "insertsuccess")
                {
                    MessageBox.Show("Category saved successfully.", "Category Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form1.Open(); // Call the method to refresh categories in Form1
                    this.Close(); // Close the Category form after saving
                }
                else
                {
                    MessageBox.Show($"{message}", "Category Added", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in or user data not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

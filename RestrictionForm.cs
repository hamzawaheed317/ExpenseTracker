using System;
using Expense_Tracker_dll;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Guna.UI2.WinForms;

namespace ExpenseTracker
{
    public partial class RestrictionForm : Form
    {
        private User user;
        public List<Expense_Tracker_dll.Category> categories;
        private int c_id;
        public RestrictionForm(User user,List<Expense_Tracker_dll.Category> categories)
        {
            InitializeComponent();
            this.user = user;
            this.categories = categories;
        }
        public void populate_categories()
        {
            foreach (Expense_Tracker_dll.Category cat in categories)
            {
                guna2ComboBox1.Items.Add(cat.Cate);

            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (user != null) 
            {
                if (c_id > 0)  
                {
                    Restriction restriction = new Restriction
                    {
                        UId = user.UserId,
                        CId = c_id,
                        Status = "Restricted",
                        Note = guna2TextBox2.Text
                    };

                    string message = restriction.Add();

                    if (message == "insertsuccess")
                    {
                        MessageBox.Show("Following Category Restricted!", "Restriction Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                 
                    else
                    {
                        MessageBox.Show($"Error: {message}", "Restriction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid category ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User not logged in or user data not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = guna2ComboBox1.SelectedItem?.ToString();

            if (selectedCategory != null)
            {
                int selectedCategoryId = categories
                    .Where(cat => cat.Cate == selectedCategory)
                    .Select(cat => cat.CId)
                    .FirstOrDefault();

                if (selectedCategoryId != default)
                {
                    this.c_id= selectedCategoryId;
                 }
                else
                {
                    MessageBox.Show("No data found for the selected category.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
